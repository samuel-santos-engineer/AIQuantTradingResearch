"""Offline-only, fixed-output classification of ARM errors and bounded log tails.

No request execution, CLI adapter, logging or file access. Hints describe an
untrusted error response, never an application failure or a confirmed cause.
"""
import datetime as dt
import json
import re

MAX_BYTES = 65536
_CODES = {
    "BadRequest": "BAD_REQUEST",
    "InvalidRequest": "BAD_REQUEST",
    "InvalidArgument": "PARAMETER_REJECTED",
    "InvalidParameter": "PARAMETER_REJECTED",
    "InvalidTimeRange": "TIME_RANGE_REJECTED",
    "InvalidApiVersionParameter": "API_VERSION_REJECTED",
    "NoRegisteredProviderFound": "API_OR_PROVIDER_REJECTED",
    "MissingSubscriptionRegistration": "API_OR_PROVIDER_REJECTED",
    "Unauthorized": "ACCESS_DENIED",
    "Forbidden": "ACCESS_DENIED",
    "AuthorizationFailed": "ACCESS_DENIED",
    "NotFound": "NOT_FOUND",
    "ResourceNotFound": "NOT_FOUND",
    "TooManyRequests": "THROTTLED",
    "InternalServerError": "SERVICE_FAILURE",
    "ServiceUnavailable": "SERVICE_FAILURE",
}
_HINTS = {
    "START_TIME": re.compile(r"(?<![a-z0-9_])starttime(?![a-z0-9_])", re.I),
    "END_TIME": re.compile(r"(?<![a-z0-9_])endtime(?![a-z0-9_])", re.I),
    "API_VERSION": re.compile(r"(?<![a-z0-9_])api-version(?![a-z0-9_])", re.I),
    "TIME_GRAIN": re.compile(r"(?<![a-z0-9_])timegrain(?![a-z0-9_])", re.I),
    "DETECTOR": re.compile(r"(?<![a-z0-9_])detector(?:name)?(?![a-z0-9_])", re.I),
}


def _pairs(items):
    result = {}
    for key, value in items:
        if key in result:
            raise ValueError("duplicate")
        result[key] = value
    return result


def classify(status, body):
    """Return only fixed enums/lists; accept at most 64 KiB of UTF-8 JSON.

    A caller must enforce its own acquisition deadline/cap. This function does
    not authorize a request or retry, retain a body, or expose an error string.
    All nested error records are bounded; excessive structure fails closed.
    """
    out = {"responseClass": "UNCLASSIFIED", "serverCategories": [],
           "parameterHints": [], "causeConfirmed": False,
           "retryAuthorized": False}
    if type(status) is not int or not 400 <= status <= 599:
        out["responseClass"] = "INVALID_STATUS"
        return out
    if type(body) is not bytes:
        out["responseClass"] = "INVALID_BODY_TYPE"
        return out
    if len(body) > MAX_BYTES:
        out["responseClass"] = "BODY_CAP_EXCEEDED"
        return out
    try:
        data = json.loads(body.decode("utf-8"), object_pairs_hook=_pairs,
                          parse_constant=lambda _: (_ for _ in ()).throw(ValueError()))
    except (ValueError, UnicodeError, RecursionError):
        out["responseClass"] = "NONSTANDARD_BODY"
        return out
    if not isinstance(data, dict):
        out["responseClass"] = "UNSUPPORTED_SCHEMA"
        return out
    error = data.get("error", data)
    if not isinstance(error, dict) or not any(k in error for k in ("code", "message", "target")):
        out["responseClass"] = "UNSUPPORTED_SCHEMA"
        return out
    pending = [(error, 0)]
    categories, hints = set(), set()
    visited = 0
    while pending:
        node, depth = pending.pop()
        visited += 1
        if visited > 16 or depth > 3 or not isinstance(node, dict):
            out["responseClass"] = "UNSUPPORTED_SCHEMA"
            return out
        code = node.get("code")
        if isinstance(code, str) and code in _CODES:
            categories.add(_CODES[code])
        for field in ("target", "message"):
            value = node.get(field)
            if isinstance(value, str) and len(value) <= 8192:
                hints.update(k for k, pattern in _HINTS.items() if pattern.search(value))
        details = node.get("details", [])
        if not isinstance(details, list) or len(details) > 16:
            out["responseClass"] = "UNSUPPORTED_SCHEMA"
            return out
        pending.extend((v, depth + 1) for v in details)
        if "innererror" in node:
            inner = node["innererror"]
            # ARM DefaultErrorResponse allows opaque string innererror. Ignore
            # its contents; preserve the bounded object form for compatibility.
            if isinstance(inner, dict):
                pending.append((inner, depth + 1))
            elif not isinstance(inner, str):
                out["responseClass"] = "UNSUPPORTED_SCHEMA"
                return out
    out.update(responseClass="STRUCTURED_ERROR", serverCategories=sorted(categories),
               parameterHints=sorted(hints))
    return out


_LOG_MARKERS = {
    "PULL_FAILURE_MENTION": re.compile(r"(?:pull.{0,80}(?:failed|failure|denied)|manifest unknown|image not found)", re.I),
    "EXIT_MENTION": re.compile(r"\b(?:exited|exit code|terminated)\b", re.I),
    "STARTUP_TIMEOUT_MENTION": re.compile(r"(?:startup.{0,80}(?:timeout|timed out)|did not start within)", re.I),
    "PORT_READINESS_FAILURE_MENTION": re.compile(r"(?:did not respond to HTTP pings|not responding to HTTP pings)", re.I),
    "APP_LISTENER_MARKER": re.compile(r"^Research snapshot listener started[.]$"),
    "APP_CONFIGURATION_FAILURE_MARKER": re.compile(r"^Research snapshot service unavailable[.] stage=CONFIGURATION$"),
    "APP_STORAGE_FAILURE_MARKER": re.compile(r"^Research snapshot service unavailable[.] stage=STORAGE$"),
}
_STAMP = re.compile(r"^(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:[.]\d{1,7})?Z) +(.+)$")


def sanitize_log_tail(status, content_type, body, window_start, window_end):
    """Classify a bounded UTF-8 tail or JSON string; never attest runtime identity.

    The operation's file payload has no documented record schema. Deliberately
    accept only a small lexical subset: ISO UTC-prefixed lines. JSON objects,
    archives, missing timestamps and unknown formats are not inferred to be
    fresh. The caller must enforce request/capture/cleanup bounds independently.
    Even matched lines are untrusted mentions without main-container binding.
    """
    out = {"responseClass": "UNCLASSIFIED", "freshness": "UNKNOWN",
           "eventCategories": [], "runtimeImageIdentity": "UNVERIFIED",
           "mainProcessLiveness": "UNVERIFIED", "listener": "UNVERIFIED",
           "causeConfirmed": False, "retryAuthorized": False}
    if type(status) is not int or status not in (200, 204):
        out["responseClass"] = "UNEXPECTED_STATUS"
        return out
    if type(body) is not bytes:
        out["responseClass"] = "INVALID_BODY_TYPE"
        return out
    if len(body) > MAX_BYTES:
        out["responseClass"] = "BODY_CAP_EXCEEDED"
        return out
    if (not isinstance(window_start, dt.datetime) or not isinstance(window_end, dt.datetime)
            or window_start.tzinfo is None or window_end.tzinfo is None
            or not dt.timedelta(0) < window_end - window_start <= dt.timedelta(minutes=15)):
        out["responseClass"] = "INVALID_WINDOW"
        return out
    if status == 204:
        out["responseClass"] = "NO_CONTENT" if body == b"" else "UNSUPPORTED_SCHEMA"
        return out
    media = content_type.split(";", 1)[0].strip().lower() if isinstance(content_type, str) else ""
    if media not in ("application/octet-stream", "application/json"):
        out["responseClass"] = "UNSUPPORTED_MEDIA"
        return out
    try:
        text = body.decode("utf-8")
        if media == "application/json":
            text = json.loads(text)
            if not isinstance(text, str):
                out["responseClass"] = "UNSUPPORTED_SCHEMA"
                return out
    except (ValueError, UnicodeError, RecursionError):
        out["responseClass"] = "UNSUPPORTED_ENCODING"
        return out
    # Reject archives/control sequences; never extract or render their contents.
    if any(ord(c) < 32 and c not in "\r\n\t" or ord(c) == 127 for c in text):
        out["responseClass"] = "UNSUPPORTED_ENCODING"
        return out
    lines = text.splitlines()
    if len(lines) > 256 or any(len(line.encode("utf-8")) > 2048 for line in lines):
        out["responseClass"] = "RECORD_CAP_EXCEEDED"
        return out
    dated, fresh, markers = False, False, set()
    for line in lines:
        match = _STAMP.fullmatch(line)
        if not match:
            continue
        try:
            timestamp = dt.datetime.fromisoformat(match[1].replace("Z", "+00:00"))
        except ValueError:
            continue
        dated = True
        if not window_start <= timestamp <= window_end:
            continue
        fresh = True
        markers.update(k for k, pattern in _LOG_MARKERS.items() if pattern.search(match[2]))
    out.update(responseClass="BOUNDED_TAIL", eventCategories=sorted(markers),
               freshness="HAS_IN_WINDOW_RECORD" if fresh else "NO_IN_WINDOW_RECORD" if dated else "UNKNOWN")
    return out

"""Offline-only, fixed-output ARM error, log-tail and SCM metadata classification.

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


_SCM_FIELDS = {"status", "contentType", "transferEncoding", "contentEncoding",
               "authChallengePresent", "locationPresent", "upgradePresent", "connectionUpgrade"}
_MEDIA_CLASSES = {"text/plain": "TEXT_PLAIN", "application/octet-stream": "OCTET_STREAM",
                  "text/event-stream": "EVENT_STREAM", "text/html": "HTML",
                  "application/json": "JSON"}
_MEDIA_SYNTAX = re.compile(r'[a-z0-9!#$&^_.+*-]+/[a-z0-9!#$&^_.+*-]+(?:\s*;\s*[a-z0-9!#$&^_.+-]+\s*=\s*(?:[a-z0-9!#$&^_.+*/:-]+|"[^"\\]*"))*', re.I)


def classify_scm_metadata(metadata):
    """Pure classification of eight projected fields, never raw HTTP headers.

    A caller must project presence booleans rather than challenge, Location,
    Upgrade or Connection values. Types/lengths are bounded before matching.
    No response body, token, URL or arbitrary string can leave this function.
    Client-source evidence is not a full server contract: even candidate media
    cannot authorize a body read or attest authentication/log capture.
    """
    out = {"responseClass": "INVALID_METADATA", "mediaClass": "UNKNOWN",
           "transferClass": "UNKNOWN", "authentication": "UNVERIFIED",
           "protocolClass": "UNKNOWN", "bodyReadAuthorized": False,
           "retryAuthorized": False, "logCaptureVerified": False}
    if type(metadata) is not dict or len(metadata) != len(_SCM_FIELDS) or set(metadata) != _SCM_FIELDS:
        return out
    status = metadata["status"]
    if type(status) is not int or not 100 <= status <= 599:
        return out
    for key in ("authChallengePresent", "locationPresent", "upgradePresent", "connectionUpgrade"):
        if type(metadata[key]) is not bool:
            return out
    for key, cap in (("contentType", 256), ("transferEncoding", 64), ("contentEncoding", 64)):
        value = metadata[key]
        if value is not None and (type(value) is not str or len(value) > cap
                                  or any(ord(c) < 32 or ord(c) > 126 for c in value)):
            return out
    content = metadata["contentType"]
    if content is not None and content.strip() and not _MEDIA_SYNTAX.fullmatch(content.strip()):
        return out
    media = content.split(";", 1)[0].strip().lower() if content else ""
    out["mediaClass"] = _MEDIA_CLASSES.get(media, "OTHER" if media else "MISSING")
    transfer = (metadata["transferEncoding"] or "").strip().lower()
    encoding = (metadata["contentEncoding"] or "").strip().lower()
    out["transferClass"] = {"": "UNSPECIFIED", "chunked": "CHUNKED_DECLARED",
                            "identity": "IDENTITY_DECLARED"}.get(transfer, "OTHER")
    if status == 101:
        out["responseClass"] = "UPGRADE_UNSUPPORTED" if metadata["upgradePresent"] and metadata["connectionUpgrade"] else "CONTRADICTORY_UPGRADE"
        out["protocolClass"] = "UPGRADE_DECLARED_UNVERIFIED"
    elif metadata["upgradePresent"] or metadata["connectionUpgrade"]:
        out["responseClass"] = "CONTRADICTORY_UPGRADE"
    elif status in (301, 302, 303, 307, 308):
        out["responseClass"] = "REDIRECT_BLOCKED" if metadata["locationPresent"] else "REDIRECT_WITHOUT_LOCATION"
    elif status == 401:
        out.update(responseClass="AUTHENTICATION_REQUIRED", authentication="CHALLENGE_PRESENT" if metadata["authChallengePresent"] else "DENIED_NO_CHALLENGE")
    elif status == 403:
        out.update(responseClass="ACCESS_DENIED", authentication="DENIED_REASON_UNKNOWN")
    elif status != 200:
        out["responseClass"] = "HTTP_ERROR" if status >= 400 else "UNSUPPORTED_STATUS"
    elif metadata["authChallengePresent"] or metadata["locationPresent"]:
        out["responseClass"] = "AMBIGUOUS_AUTH_OR_LOCATION"
    elif encoding not in ("", "identity") or out["transferClass"] == "OTHER":
        out["responseClass"] = "UNSUPPORTED_ENCODING"
    elif media in ("text/plain", "application/octet-stream"):
        out.update(responseClass="HTTP_BODY_CANDIDATE_UNVERIFIED", protocolClass="HTTP_BODY_CANDIDATE")
    elif media == "text/event-stream":
        out.update(responseClass="SSE_UNSUPPORTED", protocolClass="SSE_DECLARED_UNVERIFIED")
    else:
        out["responseClass"] = "MEDIA_MISSING_UNKNOWN" if not media else "MEDIA_UNSUPPORTED"
    return out



def sanitize_sse(body, window_start, window_end):
    """Decode bounded SSE framing to fixed log categories; return no event data.

    Input is one transient bounded capture, not an iterator or live connection.
    Only blank-line-terminated events are dispatched. Event names, IDs, retry
    directives and comments are discarded, never acted upon. The only supported
    data schema is UTC-prefixed log lines, as in sanitize_log_tail. JSON objects,
    HTML and arbitrary event data are unsupported, not guessed log records.
    """
    out = sanitize_log_tail(200, "application/octet-stream", b"", window_start, window_end)
    if out["responseClass"] != "BOUNDED_TAIL":
        return out
    out["responseClass"] = "SSE_EMPTY"
    if type(body) is not bytes:
        out["responseClass"] = "INVALID_BODY_TYPE"
        return out
    if len(body) > MAX_BYTES:
        out["responseClass"] = "BODY_CAP_EXCEEDED"
        return out
    try:
        text = body.decode("utf-8-sig")
    except UnicodeError:
        out["responseClass"] = "UNSUPPORTED_ENCODING"
        return out
    if any(ord(c) < 32 and c not in "\r\n\t" or ord(c) == 127 for c in text):
        out["responseClass"] = "UNSUPPORTED_ENCODING"
        return out
    if not text:
        return out
    text = text.replace("\r\n", "\n").replace("\r", "\n")
    lines = text.split("\n")
    if text.endswith("\n"):
        lines.pop()  # A terminal line ending is not itself a blank record.
    if len(lines) > 512 or any(len(line.encode("utf-8")) > 2048 for line in lines):
        out["responseClass"] = "SSE_RECORD_CAP_EXCEEDED"
        return out
    pending, event_bytes, events, markers, freshness = [], 0, 0, set(), set()
    for line in lines:
        if line == "":
            if not pending:
                continue
            events += 1
            if events > 32:
                out["responseClass"] = "SSE_EVENT_CAP_EXCEEDED"
                return out
            payload = "\n".join(pending)
            pending, event_bytes = [], 0
            records = payload.split("\n")
            if not records or any(not _STAMP.fullmatch(record) for record in records):
                out["responseClass"] = "SSE_UNSUPPORTED_PAYLOAD"
                return out
            result = sanitize_log_tail(200, "application/octet-stream", payload.encode("utf-8"), window_start, window_end)
            if result["responseClass"] != "BOUNDED_TAIL":
                out["responseClass"] = "SSE_UNSUPPORTED_PAYLOAD"
                return out
            markers.update(result["eventCategories"])
            freshness.add(result["freshness"])
        elif not line.startswith(":"):
            field, separator, value = line.partition(":")
            if field != "data":
                continue
            if not separator:
                value = ""
            elif value.startswith(" "):
                value = value[1:]  # SSE strips at most one optional space.
            event_bytes += len(value.encode("utf-8")) + (1 if pending else 0)
            if event_bytes > 8192:
                out["responseClass"] = "SSE_EVENT_CAP_EXCEEDED"
                return out
            pending.append(value)
    if pending:
        out["responseClass"] = "SSE_INCOMPLETE_EVENT"
        return out
    out.update(responseClass="SSE_DATA_CLASSIFIED" if events else "SSE_FRAMING_ONLY",
               eventCategories=sorted(markers),
               freshness="HAS_IN_WINDOW_RECORD" if "HAS_IN_WINDOW_RECORD" in freshness else "NO_IN_WINDOW_RECORD" if "NO_IN_WINDOW_RECORD" in freshness else "UNKNOWN")
    return out



def sanitize_sse_prefix(body, window_start, window_end, *, final=False):
    """Classify the first complete data event from a bounded cumulative prefix.

    Call after each small acquisition chunk. No socket/state/reconnection is
    owned here. A stop result samples ONE event, not a complete log stream.
    Unknown payloads stop too; never search past them for a convenient match.
    The caller must stop acquisition on captureStopRequired and enforce the
    same total byte/deadline caps, including while waiting through heartbeats.
    """
    out = sanitize_sse(b"", window_start, window_end)
    out["captureStopRequired"] = True
    if out["responseClass"] != "SSE_EMPTY":
        return out
    if type(final) is not bool or type(body) is not bytes:
        out["responseClass"] = "INVALID_PREFIX_INPUT"
        return out
    if len(body) > MAX_BYTES:
        out["responseClass"] = "BODY_CAP_EXCEEDED"
        return out
    # Defer a terminal CR until the next chunk (could be CRLF) or final=True.
    view = body[:-1] if body.endswith(b"\r") and not final else body
    view = view.replace(b"\r\n", b"\n").replace(b"\r", b"\n")
    lines = view.split(b"\n")
    if len(lines) - int(view.endswith(b"\n")) > 512 or any(len(line) > 2048 for line in lines):
        out["responseClass"] = "SSE_RECORD_CAP_EXCEEDED"
        return out
    cursor = 0
    while True:
        end = view.find(b"\n\n", cursor)
        if end < 0:
            break
        result = sanitize_sse(view[cursor:end + 2], window_start, window_end)
        cursor = end + 2
        if result["responseClass"] == "SSE_FRAMING_ONLY":
            continue
        result["captureStopRequired"] = True
        return result
    if final or len(body) == MAX_BYTES:
        result = sanitize_sse(body, window_start, window_end)
        result["captureStopRequired"] = True
        return result
    out.update(responseClass="SSE_WAITING_FOR_EVENT", captureStopRequired=False)
    return out

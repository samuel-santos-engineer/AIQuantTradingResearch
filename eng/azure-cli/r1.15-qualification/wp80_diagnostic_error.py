"""Offline-only, fixed-output classification of bounded ARM diagnostic errors.

No request execution, CLI adapter, logging or file access. Hints describe an
untrusted error response, never an application failure or a confirmed cause.
"""
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

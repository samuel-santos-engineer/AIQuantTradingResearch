"""Offline-only, fixed-output ARM error, log-tail and SCM metadata classification.

No request execution, CLI adapter, logging or file access. Hints describe an
untrusted error response, never an application failure or a confirmed cause.
"""
import datetime as dt
import hashlib
import hmac
import json
import re
from urllib.parse import unquote, urlsplit

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


_INVENTORY_FIELDS = {"machineName", "lastUpdated", "size", "href", "path"}
_INVENTORY_HOST = "aiqr115researchstagingwus27f5eabb5.scm.azurewebsites.net"


def classify_log_inventory(body, observed_utc):
    """Project documented inventory metadata, never select or fetch a log file.

    lastUpdated is file modification time, not a runtime event. Source names
    and safe VFS links are validated privately, never returned or followed.
    Even an empty/recognized inventory cannot attest main-container absence.
    """
    out = {"responseClass": "UNSUPPORTED_SCHEMA", "schemaRecognized": False,
           "inventoryState": "UNKNOWN", "fileTimeClass": "UNKNOWN",
           "mainContainerRole": "UNVERIFIED", "runtimeImageIdentity": "UNVERIFIED",
           "fileSelectionAuthorized": False, "logBodyReadAuthorized": False,
           "retryAuthorized": False}
    if (not isinstance(observed_utc, dt.datetime) or observed_utc.tzinfo is None
            or observed_utc.utcoffset() != dt.timedelta(0)):
        out["responseClass"] = "INVALID_OBSERVATION_TIME"
        return out
    if type(body) is not bytes:
        out["responseClass"] = "INVALID_BODY_TYPE"
        return out
    if len(body) > 32768:
        out["responseClass"] = "BODY_CAP_EXCEEDED"
        return out
    try:
        value = json.loads(body.decode("utf-8"), object_pairs_hook=_pairs,
                           parse_constant=lambda _: (_ for _ in ()).throw(ValueError()))
    except (ValueError, UnicodeError, RecursionError):
        out["responseClass"] = "NONSTANDARD_BODY"
        return out
    if type(value) is not list:
        return out
    if len(value) > 32:
        out["responseClass"] = "ROW_CAP_EXCEEDED"
        return out
    recent = False
    for row in value:
        if type(row) is not dict or set(row) != _INVENTORY_FIELDS:
            return out
        for field, cap in (("machineName", 256), ("lastUpdated", 40), ("href", 2048), ("path", 1024)):
            text = row[field]
            if type(text) is not str or not 0 < len(text) <= cap or any(ord(c) < 32 or ord(c) == 127 for c in text):
                return out
        if type(row["size"]) is not int or not 0 <= row["size"] <= 2**63 - 1:
            return out
        stamp = row["lastUpdated"]
        if not re.fullmatch(r"\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:[.]\d{1,7})?(?:Z|[+]00:00)", stamp):
            out["responseClass"] = "INVALID_FILE_TIME"
            return out
        try:
            timestamp = dt.datetime.fromisoformat(stamp.replace("Z", "+00:00"))
        except ValueError:
            out["responseClass"] = "INVALID_FILE_TIME"
            return out
        if timestamp > observed_utc:
            out["responseClass"] = "FUTURE_FILE_METADATA"
            return out
        try:
            href = row["href"]
            url = urlsplit(href)
            decoded = unquote(url.path, errors="strict")
            path = row["path"]
            safe = (href == href.strip() and url.scheme == "https" and url.hostname == _INVENTORY_HOST
                    and url.port in (None, 443) and url.username is None and url.password is None
                    and not url.query and not url.fragment and "?" not in href and "#" not in href
                    and decoded.startswith("/api/vfs/LogFiles/") and path.startswith("/home/LogFiles/")
                    and decoded == "/api/vfs" + path[len("/home"):]
                    and all(part not in ("", ".", "..") for part in decoded.split("/")[1:]))
            if not safe or any(c in decoded or c in path for c in ("\\", "%", "?", "#")):
                raise ValueError()
        except (ValueError, UnicodeError):
            out["responseClass"] = "UNSAFE_REFERENCE"
            return out
        recent = recent or observed_utc - timestamp <= dt.timedelta(hours=24)
    out.update(responseClass="INVENTORY_PROJECTED", schemaRecognized=True,
               inventoryState="NONEMPTY" if value else "EMPTY",
               fileTimeClass="WITHIN_24H_PRESENT" if recent else "OLDER_ONLY" if value else "UNKNOWN")
    return out


_DIAGNOSTIC_CHILD = "sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c"
_BINDING_ID = re.compile(r"[A-Za-z0-9][A-Za-z0-9_.:-]{0,127}")
_BINDING_KEYS = {"instanceId", "containerId", "attemptId"}
_STARTUP_STAGES = {"CONFIGURATION", "TRANSPORT", "TLS", "STORAGE", "LISTENER", "SERVING"}
_STAGE_PREFIX = b"Research snapshot service unavailable. stage="
_STAGE_MESSAGES = {_STAGE_PREFIX + stage.encode("ascii"): stage for stage in _STARTUP_STAGES}


def _attribution_metadata(raw):
    if type(raw) is not bytes or len(raw) > 4096:
        raise ValueError("metadata bound")
    return json.loads(raw.decode("utf-8"), object_pairs_hook=_pairs)


def _attribution_utc(value):
    # Restrict to microsecond precision to avoid dropping a boundary fraction.
    if not isinstance(value, str) or len(value) > 27 or not re.fullmatch(
            r"[0-9]{4}-[0-9]{2}-[0-9]{2}T[0-9]{2}:[0-9]{2}:[0-9]{2}(?:[.][0-9]{1,6})?Z", value):
        raise ValueError("invalid UTC")
    return dt.datetime.fromisoformat(value.replace("Z", "+00:00"))


def evaluate_startup_attribution(record, *, trusted_attempts=None, trusted_record=None, observation_window=None):
    """Evaluate independently verified evidence under a synthetic offline contract.

    The trusted arguments must come from outside SCM inventory, filenames and
    message text. No current Azure adapter supplies them. The caller must verify
    origin and completeness of the attempt roster for the observation window
    and verify that the record envelope authenticates this occurrence and its
    exact payload bytes. Supply the current controller window independently.
    This function checks consistency only. It cannot certify the caller's trust.
    """
    result = dict(mainContainerBound=False, diagnosticImageBound=False,
                  startupAttemptBound=False, recordWithinAttempt=False,
                  diagnosticCaptureReady=False, diagnosticCategory="UNKNOWN",
                  reason="INVALID_METADATA")
    if type(record) is not bytes or not 0 < len(record) <= 256:
        result["reason"] = "INVALID_RECORD"
        return result
    if trusted_attempts is None or trusted_record is None or observation_window is None:
        result["reason"] = "TRUSTED_BINDING_MISSING"
        return result
    try:
        attempts = _attribution_metadata(trusted_attempts)
        envelope = _attribution_metadata(trusted_record)
    except (ValueError, RecursionError):
        return result
    if not isinstance(attempts, list):
        return result
    if len(attempts) != 1:
        result["reason"] = "ATTEMPT_ROSTER_NOT_UNIQUE"
        return result
    attempt = attempts[0]
    if (not isinstance(attempt, dict) or set(attempt) != _BINDING_KEYS | {
            "role", "imageDigest", "startedUtc", "observedThroughUtc"}
            or not isinstance(envelope, dict) or set(envelope) != _BINDING_KEYS | {
                "recordUtc", "sha256", "clockErrorMs"}):
        return result
    if any(not isinstance(item[key], str) or not _BINDING_ID.fullmatch(item[key])
           for item in (attempt, envelope) for key in _BINDING_KEYS):
        return result
    digest = envelope["sha256"]
    if (not isinstance(digest, str) or not re.fullmatch(r"[0-9a-f]{64}", digest)
            or not hmac.compare_digest(hashlib.sha256(record).hexdigest(), digest)):
        result["reason"] = "RECORD_INTEGRITY_UNVERIFIED"
        return result
    if any(attempt[key] != envelope[key] for key in ("instanceId", "containerId")):
        result["reason"] = "CONTAINER_IDENTITY_MISMATCH"
        return result
    result["mainContainerBound"] = attempt["role"] == "MAIN"
    result["diagnosticImageBound"] = attempt["imageDigest"] == _DIAGNOSTIC_CHILD
    result["startupAttemptBound"] = attempt["attemptId"] == envelope["attemptId"]
    try:
        if type(observation_window) is not tuple or len(observation_window) != 2:
            raise ValueError("invalid observation window")
        lower, upper = map(_attribution_utc, observation_window)
        started = _attribution_utc(attempt["startedUtc"])
        observed = _attribution_utc(attempt["observedThroughUtc"])
        stamp = _attribution_utc(envelope["recordUtc"])
        clock_error = envelope["clockErrorMs"]
        if (type(clock_error) is not int or not 0 <= clock_error <= 2000
                or not dt.timedelta(0) < upper - lower <= dt.timedelta(seconds=180)
                or not lower <= started < observed <= upper):
            raise ValueError("invalid window")
    except ValueError:
        result["reason"] = "INVALID_TIME_EVIDENCE"
        return result
    margin = dt.timedelta(milliseconds=clock_error)
    result["recordWithinAttempt"] = (stamp - started >= margin
                                     and observed - stamp >= margin)
    category = ("LISTENER_STARTED" if record == b"Research snapshot listener started."
                else _STAGE_MESSAGES.get(record, "UNKNOWN"))
    for satisfied, reason in (
        (result["mainContainerBound"], "MAIN_ROLE_UNVERIFIED"),
        (result["diagnosticImageBound"], "DIAGNOSTIC_IMAGE_UNVERIFIED"),
        (result["startupAttemptBound"], "ATTEMPT_IDENTITY_MISMATCH"),
        (result["recordWithinAttempt"], "RECORD_OUTSIDE_SAFE_WINDOW"),
        (category != "UNKNOWN", "UNSUPPORTED_RECORD"),
    ):
        if not satisfied:
            result["reason"] = reason
            return result
    result.update(diagnosticCaptureReady=True, diagnosticCategory=category,
                  reason="CONSISTENT_TRUSTED_EVIDENCE")
    return result


_STARTUP_FILE_BYTES = 16 * 1024
_STARTUP_FILE_LINES = 256
_STARTUP_EXIT = re.compile(
    r"State: Stopping, Action: CancellingStartup, LastError: ContainerStartupFailure, "
    r"LastErrorTimestamp: [0-9/ :]+, LastErrorDetails: Container exited with exit code "
    r"([0-9]{1,3}) during startup after [0-9]+(?:[.][0-9]+)?s[.] "
    r"Please inspect your container logs for more details[.]"
    r"(?:, Details: .+, DetailsLevel: ERR)?"
)


def collect_startup_file(metadata, chunks, *, observation_window):
    """Reduce one bounded plaintext startup file to reported evidence categories.

    The caller owns the exact-route, authentication and deadline checks and
    supplies bounded chunks. This offline collector never establishes record
    provenance, even when a line matches the diagnostic source vocabulary.
    """
    result = dict(responseClass="UNKNOWN", failureCategory="UNKNOWN",
                  exitCodeClass="UNKNOWN", vocabularyClass="UNKNOWN",
                  freshness="UNKNOWN", diagnosticCaptureReady=False)
    gate = classify_scm_metadata(metadata)
    if gate["responseClass"] != "HTTP_BODY_CANDIDATE_UNVERIFIED":
        result["responseClass"] = ("HTTP_NOT_FOUND" if gate["responseClass"] == "HTTP_ERROR"
                                   and metadata["status"] == 404 else gate["responseClass"])
        return result
    if not re.fullmatch(r'\s*text/plain\s*(?:;\s*charset\s*=\s*(?:utf-8|"utf-8")\s*)?',
                        metadata["contentType"], re.I):
        result["responseClass"] = "UNSUPPORTED_MEDIA"
        return result
    try:
        if type(observation_window) is not tuple or len(observation_window) != 2:
            raise ValueError("window")
        start, end = map(_attribution_utc, observation_window)
        if not dt.timedelta(0) < end - start <= dt.timedelta(minutes=15):
            raise ValueError("window")
    except ValueError:
        result["responseClass"] = "INVALID_WINDOW"
        return result

    payload = bytearray()
    try:
        iterator = iter(chunks)
    except TypeError:
        result["responseClass"] = "INVALID_CHUNK"
        return result
    try:
        for chunk in iterator:
            if type(chunk) is not bytes or not chunk:
                result["responseClass"] = "INVALID_CHUNK"
                return result
            # At the exact limit EOF is unverified. Stop without another pull.
            if len(chunk) >= _STARTUP_FILE_BYTES - len(payload):
                result["responseClass"] = "BODY_CAP_REACHED"
                return result
            payload.extend(chunk)
            if payload.count(b"\n") > _STARTUP_FILE_LINES:
                result["responseClass"] = "LINE_CAP_EXCEEDED"
                return result
    except OSError:
        result["responseClass"] = "TRANSPORT_FAILURE"
        return result
    if not payload or not payload.endswith(b"\n"):
        result["responseClass"] = "EMPTY_BODY" if not payload else "INCOMPLETE_BODY"
        return result
    try:
        text = payload.decode("utf-8").replace("\r\n", "\n")
    except UnicodeError:
        result["responseClass"] = "UNSUPPORTED_BODY"
        return result
    if any(ord(char) < 32 and char not in "\t\n" or ord(char) == 127 for char in text):
        result["responseClass"] = "UNSUPPORTED_BODY"
        return result

    matches = set()
    dated = False
    fresh = False
    for line in text.split("\n"):
        if not line:
            continue
        match = _STAMP.fullmatch(line)
        if match is None:
            result["responseClass"] = "UNSUPPORTED_BODY"
            return result
        stamp, message = match.groups()
        # Platform examples include seven fractional digits. Preserve the
        # seventh digit when checking bounds against a microsecond UTC window.
        remainder = int(stamp[-2]) if len(stamp) == 28 else 0
        try:
            timestamp = _attribution_utc(stamp[:-2] + "Z" if len(stamp) == 28 else stamp)
        except ValueError:
            result["responseClass"] = "UNSUPPORTED_BODY"
            return result
        dated = True
        if not (start, 0) <= (timestamp, remainder) <= (end, 0):
            continue
        fresh = True
        exit_record = _STARTUP_EXIT.fullmatch(message)
        if exit_record:
            code = int(exit_record[1])
            if 1 <= code <= 255:
                matches.add(("PLATFORM_REPORTED_STARTUP_EXIT", "ONE" if code == 1 else "OTHER_NONZERO",
                             "PLATFORM_LIFECYCLE"))
        elif message == "Research snapshot service unavailable.":
            matches.add(("WORKER_FAILURE_MENTION", "UNKNOWN", "GENERIC_WORKER_MARKER"))
        elif (stage := _STAGE_MESSAGES.get(message.encode("utf-8"))) is not None:
            matches.add((stage, "UNKNOWN", "STAGED_WORKER_MARKER_UNATTRIBUTED"))

    result["freshness"] = "IN_WINDOW" if fresh else "OUTSIDE_WINDOW" if dated else "UNKNOWN"
    if not matches:
        result["responseClass"] = "NO_RECOGNIZED_FAILURE"
        return result
    if len(matches) > 1:
        result["responseClass"] = "AMBIGUOUS_EVIDENCE"
        return result
    category, exit_code, vocabulary = next(iter(matches))
    result.update(responseClass="REPORTED_EVIDENCE", failureCategory=category,
                  exitCodeClass=exit_code, vocabularyClass=vocabulary)
    return result

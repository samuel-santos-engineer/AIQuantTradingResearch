"""Offline-testable observation and rollback sequence for the parallel API lab.

The caller supplies an exact, verified apply and image-only rollback, an
authenticated bounded HTTPS transport, and an enclosing hard deadline.
Importing or invoking this file directly cannot contact Azure.
"""

import datetime as dt
import http.client
import time

from wp80_diagnostic_error import classify_startup_show_response, collect_startup_show


LAB_HOST = "aiqr115-parallel-lab-wus2-7f5eabb5.azurewebsites.net"
LAB_SCM = "aiqr115-parallel-lab-wus2-7f5eabb5.scm.azurewebsites.net"
STARTUP_PATH = "/api/startuplogs?latest=true"
HEALTH_OFFSETS = (15, 60, 120)
HEALTH_WINDOW = 180
PROBE_TIMEOUT = 8
CONNECT_BUDGET = 3
STARTUP_BODY_CAP = 16383
_ACCEPTED_MEDIA = {"TEXT_PLAIN", "JSON_ENVELOPE"}


def _utc():
    return dt.datetime.now(dt.timezone.utc).isoformat(timespec="microseconds").replace("+00:00", "Z")


def _startup_metadata(response):
    names = {"content-type", "transfer-encoding", "content-encoding",
             "www-authenticate", "location", "upgrade", "connection", "content-length"}
    headers = {}
    for name, value in response.getheaders():
        name = name.lower()
        if name in names:
            if name in headers:
                return None
            headers[name] = value
    if "content-length" in headers and "transfer-encoding" in headers:
        return None
    return dict(
        status=response.status,
        contentType=headers.get("content-type"),
        transferEncoding=headers.get("transfer-encoding"),
        contentEncoding=headers.get("content-encoding"),
        authChallengePresent="www-authenticate" in headers,
        locationPresent="location" in headers,
        upgradePresent="upgrade" in headers,
        connectionUpgrade="upgrade" in [v.strip().lower() for v in headers.get("connection", "").split(",")],
    )


def _observe_startup(out, request, startup_token, since, utc):
    out.update(startup="LOGGING_UNAVAILABLE", startupAttempted=False, startupRecords=[])
    metadata = None

    def readable(response):
        nonlocal metadata
        out["startupHttp"] = response.status
        metadata = _startup_metadata(response)
        out["startupResponseClass"] = (classify_startup_show_response(metadata)
                                       if metadata is not None else "AMBIGUOUS_HEADERS")
        return out["startupResponseClass"] in _ACCEPTED_MEDIA

    try:
        headers = {"Authorization": "Bearer " + startup_token(),
                   "Accept": "text/plain, application/json", "Accept-Encoding": "identity"}
        out["startupAttempted"] = True
        status, body = request(LAB_SCM, "GET", STARTUP_PATH, headers, seconds=PROBE_TIMEOUT,
                               cap=STARTUP_BODY_CAP, readable=readable)
        out["startupHttp"] = status
        if body is None or out.get("startupResponseClass") not in _ACCEPTED_MEDIA:
            return
        result = collect_startup_show(metadata, [body] if body else (), observation_window=(since, utc()))
        out["startupResponseClass"] = result["responseClass"]
        out["startupRecords"] = result["records"]
        out["startup"] = "IN_WINDOW_MARKERS" if result["records"] else result["responseClass"]
    except (OSError, ValueError, RuntimeError, http.client.HTTPException):
        out["startupResponseClass"] = "TRANSPORT_OR_CONTRACT_UNVERIFIED"


def _observe_health(out, request, start, monotonic, wait, utc):
    for index, offset in enumerate(HEALTH_OFFSETS):
        # A slow apply or request must not bunch up already missed probes.
        if index + 1 < len(HEALTH_OFFSETS) and monotonic() >= start + HEALTH_OFFSETS[index + 1]:
            continue
        wait(max(0, start + offset - monotonic()))
        remaining = start + HEALTH_WINDOW - monotonic()
        if remaining < CONNECT_BUDGET:
            break
        attempt = {"utc": utc(), "status": None, "tlsVerified": False,
                   "classification": "TRANSPORT_OR_RESPONSE_UNVERIFIED"}
        out["health"].append(attempt)
        try:
            status, body = request(LAB_HOST, "GET", "/health", {"Accept-Encoding": "identity"},
                                   seconds=min(PROBE_TIMEOUT, remaining), cap=16,
                                   readable=lambda response: response.status == 200)
        except (OSError, ValueError, RuntimeError, http.client.HTTPException):
            continue
        alive = status == 200 and body == b"Alive"
        attempt.update(status=status, tlsVerified=True,
                       classification="ALIVE" if alive else "HEALTH_REJECTED")
        if alive or status == 429:
            return alive
    return False


def run_observable_attempt(out, *, apply, rollback, request, startup_token, snapshot,
                           monotonic=time.monotonic, wait=time.sleep, utc=_utc):
    """Run one configuration apply and the bounded post-apply checks.

    Apply must verify image and settings readback before returning. Rollback
    must verify current image/target first and restore only the prior image.
    Errors from either callback propagate to the caller's fail-closed handler.
    """
    start = monotonic()
    out["applyWindowStart"] = utc()
    apply()

    out["stage"] = "HEALTH"
    alive = _observe_health(out, request, start, monotonic, wait, utc)
    out["healthAccepted"] = alive
    _observe_startup(out, request, startup_token, out["applyWindowStart"], utc)

    if alive:
        for instrument in ("BTC/USD", "ETH/USD"):
            snapshot(instrument)
        out["result"] = "PASS" if out["startup"] == "IN_WINDOW_MARKERS" else "BLOCKED_STARTUP_EVIDENCE"
    else:
        out["rollback"] = "RECHECK_PENDING"
        rollback()
        out["rollback"] = "IMAGE_RESTORED"
        out["result"] = "HEALTH_FAILED"

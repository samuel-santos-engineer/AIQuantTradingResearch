"""Offline streaming qualification only. No HTTP/authentication/CLI adapter exists."""
import argparse
from dataclasses import dataclass
import json
import time
import setting_name_provenance as provenance

LIMIT = 1048576
CHUNK = 4096
REASONS = {"CANCELLED", "DEADLINE", "HTTP_STATUS", "REDIRECT", "RETRY", "ENCODING", "BUFFERING_UNQUALIFIED",
           "LENGTH_SCHEMA", "OVER_CAP", "STREAM_CONTRACT", "TRUNCATED", "IDENTITY_OR_JSON", "TRANSPORT_ERROR", "CLEANUP_ERROR"}


class Stop(Exception):
    pass


@dataclass(frozen=True)
class Request:
    role: str
    method: str
    url: str


def requests():
    return tuple(Request(role, "POST", "https://management.azure.com" + provenance.BASE + app +
                         "/config/appsettings/list?api-version=2023-12-01")
                 for role, app in provenance.APPS.items())


def require(condition, reason):
    if not condition:
        raise Stop(reason)


def collect_offline(transport, clock=time.monotonic, cancelled=lambda: False):
    """Cooperative fake-transport deadlines; not a claim of hard wall-clock HTTP enforcement."""
    require(getattr(transport, "qualification_only", None) is True and
            getattr(transport, "network_enabled", None) is False, "BUFFERING_UNQUALIFIED")
    result = {"result": "OFFLINE_QUALIFICATION", "azureCalls": 0, "openedStreams": 0,
              "roles": {role: {"state": "NOT_ATTEMPTED", "presence": provenance.unknown(role)} for role in provenance.APPS},
              "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}
    total_end = clock() + 30
    for request in requests():
        response = None
        body = bytearray()
        failure = None
        deadline = min(clock() + 15, total_end)
        def check():
            require(not cancelled(), "CANCELLED")
            require(clock() < deadline, "DEADLINE")
        try:
            check()
            result["openedStreams"] += 1
            response = transport.open(request, deadline=deadline, cancelled=cancelled)
            check()
            require(response.status == 200, "HTTP_STATUS")
            require(response.redirected is False, "REDIRECT")
            require(type(response.attempts) is int and response.attempts == 1, "RETRY")
            require(response.content_encoding in (None, "identity"), "ENCODING")
            require(response.upstream_buffering == "none", "BUFFERING_UNQUALIFIED")
            length = response.declared_length
            require(length is None or type(length) is int and length >= 0, "LENGTH_SCHEMA")
            require(length is None or length <= LIMIT, "OVER_CAP")
            while True:
                check()
                remaining = LIMIT - len(body)
                # Do not read a sentinel byte beyond the cap. Unknown-length bodies exactly at cap reject conservatively.
                if length is not None and len(body) == length:
                    break
                require(remaining > 0, "OVER_CAP")
                maximum = min(CHUNK, remaining, length - len(body) if length is not None else remaining)
                chunk = response.read(maximum, deadline=deadline, cancelled=cancelled)
                check()
                require(isinstance(chunk, bytes) and len(chunk) <= maximum, "STREAM_CONTRACT")
                if not chunk:
                    require(length is None or len(body) == length, "TRUNCATED")
                    break
                body.extend(chunk)
            check()
            presence = provenance.parse_presence(bytes(body), request.role)
            require(not any(value == "UNKNOWN" for value in presence.values()), "IDENTITY_OR_JSON")
            result["roles"][request.role] = {"state": "COLLECTED", "presence": presence}
        except Stop as error:
            failure = str(error) if str(error) in REASONS else "TRANSPORT_ERROR"
        except Exception:
            failure = "TRANSPORT_ERROR"
        finally:
            if response is not None:
                try:
                    # Additional cooperative cleanup budget, not an unbounded retry or read.
                    cleanup_end = clock() + 1
                    response.close(deadline=cleanup_end)
                    require(clock() <= cleanup_end, "CLEANUP_ERROR")
                except Exception:
                    failure = "CLEANUP_ERROR"
            body.clear()
        if failure:
            result["roles"][request.role] = {"state": "UNKNOWN", "reason": failure, "presence": provenance.unknown(request.role)}
            result["result"] = "STOPPED"
            break
    return result


def plan():
    return {"result": "PREPARATION_ONLY", "azureCalls": 0, "liveAdapterImplemented": False,
            "authentication": "DISABLED", "perRequestSeconds": 15, "totalRequestSeconds": 30,
            "bodyCapBytes": LIMIT, "cooperativeCleanupSeconds": 1, "liveTransportQualified": False,
            "order": [r.role for r in requests()], "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--apply", action="store_true")
    parser.add_argument("--execute", action="store_true")
    args = parser.parse_args(argv)
    if args.apply or args.execute:
        raise Stop("LIVE_TRANSPORT_UNQUALIFIED")
    print(json.dumps(plan(), sort_keys=True))
    return 0


if __name__ == "__main__":
    try:
        raise SystemExit(main())
    except Stop:
        print(json.dumps({"result": "LIVE_TRANSPORT_UNQUALIFIED", "azureCalls": 0,
                          "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}))
        raise SystemExit(2)

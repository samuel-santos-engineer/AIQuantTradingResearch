"""Opt-in anonymous process-health wait, never dataset/provider/deployment readiness."""
import argparse
import http.client
import json
import ssl
import time

HOST = "aiqr115researchstagingwus27f5eabb5.azurewebsites.net"
BUDGET_SECONDS = 180
MAX_ATTEMPTS = 3
ATTEMPT_SECONDS = 60


def probe_once(timeout):
    deadline = time.monotonic() + timeout
    connection = http.client.HTTPSConnection(HOST, timeout=timeout, context=ssl.create_default_context())

    def remaining():
        value = deadline - time.monotonic()
        if value <= 0:
            raise TimeoutError()
        if connection.sock is not None:
            connection.sock.settimeout(value)
        return value

    try:
        connection.connect()  # Native DNS is not hard-cancellable; parent process also has a deadline.
        remaining()
        connection.request("GET", "/health", headers={"Accept-Encoding": "identity", "Connection": "close"})
        remaining()
        response = connection.getresponse()
        if response.status != 200:
            return ("PENDING" if response.status in (502, 503, 504) else "FAILED", response.status)
        if response.getheader("Content-Encoding", "identity").lower() != "identity":
            return ("FAILED", 200)
        remaining()
        data = response.read(6)  # Five exact ASCII bytes required; never echo an error/HTML body.
        remaining()
        return ("ALIVE" if data == b"Alive" else "FAILED", 200)
    except ssl.SSLError:
        return ("FAILED", None)
    except (TimeoutError, OSError):
        return ("PENDING", None)
    except http.client.HTTPException:
        return ("FAILED", None)
    finally:
        connection.close()


def wait_for_health(probe=probe_once, now=time.monotonic, pause=time.sleep):
    started = now()
    deadline = started + BUDGET_SECONDS
    observations = []
    alive = False
    for _ in range(MAX_ATTEMPTS):
        remaining = deadline - now()
        if remaining <= 0:
            break
        outcome, status = probe(min(ATTEMPT_SECONDS, remaining))
        observations.append({"outcome": outcome, "status": status})
        if now() > deadline:
            break
        if outcome == "ALIVE":
            alive = True
            break
        if outcome != "PENDING":
            break
        if len(observations) < MAX_ATTEMPTS:
            pause(min(2, max(0, deadline - now())))
    return {"result": "PROCESS_ALIVE" if alive else "INCOMPLETE", "processAlive": alive,
            "attempts": observations, "elapsedSeconds": round(now() - started, 3),
            "datasetAcceptance": "NOT_RUN", "PUBLIC_RUNTIME_VERIFIED": False}


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--execute", action="store_true", help="Requires separate owner authority for up to three live health GETs.")
    args = parser.parse_args()
    if not args.execute:
        print(json.dumps({"result": "NOT_RUN", "processAlive": False, "PUBLIC_RUNTIME_VERIFIED": False}))
        return 0
    result = wait_for_health()
    print(json.dumps(result))
    return 0 if result["processAlive"] else 1


if __name__ == "__main__":
    raise SystemExit(main())

"""Neutral Release 1.8 JSON-over-stdio integration endpoint."""

from __future__ import annotations

import json
import sys
from typing import Any


CONTRACT_VERSION = 1
MAXIMUM_REQUEST_BYTES = 65_536
ALLOWED_OPERATIONS = {"health", "echo"}


def response(
    *,
    status: str,
    correlation_id: str,
    result: dict[str, str] | None = None,
    code: str | None = None,
    message: str | None = None,
    retryable: bool | None = None,
) -> dict[str, Any]:
    return {
        "contractVersion": CONTRACT_VERSION,
        "status": status,
        "correlationId": correlation_id,
        "result": result,
        "code": code,
        "message": message,
        "retryable": retryable,
    }


def failure(correlation_id: str, code: str, message: str) -> dict[str, Any]:
    return response(
        status="failure",
        correlation_id=correlation_id,
        code=code,
        message=message,
        retryable=False,
    )


def handle(request: Any) -> dict[str, Any]:
    if not isinstance(request, dict):
        return failure("unknown", "InvalidRequest", "Request must be a JSON object.")

    correlation_id = request.get("correlationId")
    if not isinstance(correlation_id, str) or not correlation_id or len(correlation_id) > 128:
        return failure("unknown", "InvalidRequest", "Correlation identifier is invalid.")

    if request.get("contractVersion") != CONTRACT_VERSION:
        return failure(
            correlation_id,
            "UnsupportedContractVersion",
            "Contract version is unsupported.",
        )

    operation = request.get("operation")
    if operation not in ALLOWED_OPERATIONS:
        return failure(correlation_id, "InvalidRequest", "Operation is unsupported.")

    payload = request.get("payload")
    if not isinstance(payload, dict) or any(
        not isinstance(key, str) or not isinstance(value, str)
        for key, value in payload.items()
    ):
        return failure(correlation_id, "InvalidRequest", "Payload must contain string pairs.")

    if operation == "health":
        result = {"status": "available", "contract": "1"}
    else:
        result = dict(sorted(payload.items()))

    return response(status="success", correlation_id=correlation_id, result=result)


def main() -> int:
    raw = sys.stdin.buffer.read(MAXIMUM_REQUEST_BYTES + 1)
    if len(raw) > MAXIMUM_REQUEST_BYTES:
        output = failure("unknown", "InvalidRequest", "Request exceeds the bounded size.")
    else:
        try:
            request = json.loads(raw.decode("utf-8-sig"))
        except (UnicodeDecodeError, json.JSONDecodeError):
            output = failure("unknown", "InvalidRequest", "Request JSON is malformed.")
        else:
            output = handle(request)

    sys.stdout.write(json.dumps(output, separators=(",", ":"), sort_keys=True))
    sys.stdout.write("\n")
    sys.stdout.flush()
    print("integration endpoint completed", file=sys.stderr)
    return 0


if __name__ == "__main__":
    raise SystemExit(main())

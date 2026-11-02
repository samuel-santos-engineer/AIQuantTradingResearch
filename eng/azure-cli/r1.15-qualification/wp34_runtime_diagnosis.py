"""Offline triage of owner-curated, non-secret metadata; never Azure verification."""
import json
import sys

FIELDS = (
    "targetMatches", "planMatches", "linuxF1WestUs2", "siteRunning",
    "classicContainerConfigured", "sidecarConfigured", "approvedImageConfigured",
    "startupCommandEmpty", "portMatches", "bindMatches", "transportMatches",
)


def diagnose(metadata):
    if not isinstance(metadata, dict) or set(metadata) != set(FIELDS):
        raise ValueError("INVALID_METADATA")
    if any(value is not None and type(value) is not bool for value in metadata.values()):
        raise ValueError("INVALID_METADATA")
    findings = []
    for name in ("targetMatches", "planMatches", "linuxF1WestUs2"):
        if metadata[name] is not True:
            findings.append("TARGET_OR_PLAN_UNVERIFIED")
            break
    if metadata["classicContainerConfigured"] is True and metadata["sidecarConfigured"] is True:
        raise ValueError("INVALID_METADATA")
    if metadata["sidecarConfigured"] is None:
        findings.append("HOSTING_MODE_UNVERIFIED")
    if metadata["sidecarConfigured"] is True:
        findings.append("SIDECAR_MAIN_CONTAINER_REVIEW_REQUIRED")
    elif metadata["classicContainerConfigured"] is not True:
        findings.append("EXPECTED_CLASSIC_CONTAINER_UNVERIFIED")
    for name, failure in (
        ("siteRunning", "RUNNING_STATE_UNVERIFIED"),
        ("approvedImageConfigured", "APPROVED_IMAGE_UNVERIFIED"),
        ("startupCommandEmpty", "STARTUP_OVERRIDE_REVIEW_REQUIRED"),
        ("portMatches", "PORT_52731_UNVERIFIED"),
        ("bindMatches", "BIND_ALL_INTERFACES_UNVERIFIED"),
        ("transportMatches", "AZURE_HTTP_TRANSPORT_UNVERIFIED"),
    ):
        if metadata[name] is not True:
            findings.append(failure)
    return {
        "result": "CURATED_METADATA_REVIEW_REQUIRED" if findings else "CURATED_METADATA_MATCH_RUNTIME_UNVERIFIED",
        "findings": findings,
        "azureInspected": False,
        "healthVerified": False,
        "liveAttemptReady": False,
        "AUTHENTIC_DATA_NOT_IMPORTED": True,
        "PUBLIC_RUNTIME_VERIFIED": False,
    }


def unique_object(pairs):
    result = {}
    for key, value in pairs:
        if key in result:
            raise ValueError("INVALID_METADATA")
        result[key] = value
    return result


def main(args=None, stream=None):
    args = sys.argv[1:] if args is None else args
    try:
        if args != ["--stdin"]:
            raise ValueError("CURATED_STDIN_REQUIRED")
        data = (sys.stdin.buffer if stream is None else stream).read(4097)
        if len(data) > 4096:
            raise ValueError("INVALID_METADATA")
        result = diagnose(json.loads(data.decode("utf-8"), object_pairs_hook=unique_object))
    except (ValueError, UnicodeError, TypeError, RecursionError):
        result = {
            "result": "INVALID_OR_MISSING_CURATED_METADATA", "azureInspected": False,
            "healthVerified": False, "liveAttemptReady": False,
            "AUTHENTIC_DATA_NOT_IMPORTED": True, "PUBLIC_RUNTIME_VERIFIED": False,
        }
        print(json.dumps(result))
        return 2
    print(json.dumps(result))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())

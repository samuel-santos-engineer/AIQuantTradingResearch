"""Offline projection and exact command construction; no default daemon operation."""
import argparse
import json
import re
from wp64_oci_provenance import WP61

CONTEXT = "desktop-linux"
ENDPOINT = "npipe:////./pipe/dockerDesktopLinuxEngine"
FORMAT = ('{"Id":{{json .Id}},"Os":{{json .Os}},"Architecture":{{json .Architecture}},'
          '"User":{{json .Config.User}},"ExposedPorts":{{json .Config.ExposedPorts}},'
          '"Entrypoint":{{json .Config.Entrypoint}},"Cmd":{{json .Config.Cmd}},"RepoDigests":{{json .RepoDigests}}}')
LIMIT = 1048576


def command(context):
    if context != CONTEXT: raise ValueError("CONTEXT_UNVERIFIED")
    return ["docker", "--context", context, "image", "inspect", WP61, "--format", FORMAT]


def project(raw):
    safe = {"result": "UNVERIFIED", "localIdentityKind": "LOCAL_DOCKER_IMAGE_ID",
            "configDigest": "UNKNOWN", "runtimeManifestDigest": "UNKNOWN", "ociIndexDigest": "UNKNOWN",
            "publication": "UNVERIFIED", "PUBLIC_RUNTIME_VERIFIED": False, "liveAttemptReady": False}
    try:
        if not isinstance(raw, bytes) or len(raw) > LIMIT: return safe
        def unique(pairs):
            out = {}
            for key, value in pairs:
                if key in out: raise ValueError("DUPLICATE")
                out[key] = value
            return out
        d = json.loads(raw, object_pairs_hook=unique)
        if not isinstance(d, dict): return safe
        if d.get("Id") != WP61:
            safe["reason"] = "WRONG_IMAGE_ID"; return safe
        ports = d.get("ExposedPorts")
        if not isinstance(ports, dict) or any(not re.fullmatch(r"[0-9]{1,5}/(?:tcp|udp)", name) for name in ports): return safe
        entry, cmd = d.get("Entrypoint"), d.get("Cmd") or []
        if not isinstance(entry, list) or not isinstance(cmd, list) or not all(isinstance(x, str) for x in entry + cmd): return safe
        prefix = ["/opt/venv/bin/python", "-B", "-m", "streamlit", "run", "/app/python/presentation/realtime_financial_visualization.py"]
        args = entry[6:] + cmd
        def flag(name, expected):
            values = [x.partition("=")[2] for x in args if x.startswith(name + "=")]
            return name not in args and values == [expected]
        refs = d.get("RepoDigests")
        if not isinstance(refs, list) or not all(isinstance(x, str) and re.fullmatch(r"ghcr[.]io/samuel-santos-engineer/aiquanttradingresearch@sha256:[a-f0-9]{64}", x) for x in refs): return safe
        checks = {"linuxAmd64": d.get("Os") == "linux" and d.get("Architecture") == "amd64",
                  "user12002": d.get("User") == "12002:12002", "exposes8501": "8501/tcp" in ports,
                  "streamlitEntrypoint": entry[:6] == prefix,
                  "listens8501": flag("--server.port", "8501"), "bindsAllInterfaces": flag("--server.address", "0.0.0.0")}
        safe.update(result="MATCH" if all(checks.values()) else "MISMATCH", localId=WP61,
                    os="linux" if checks["linuxAmd64"] else "OTHER", architecture="amd64" if checks["linuxAmd64"] else "OTHER",
                    user="12002:12002" if checks["user12002"] else "OTHER", portNames=sorted(ports),
                    contract=checks, safeRepoDigests=refs)
    except (ValueError, UnicodeError, TypeError, AttributeError, RecursionError):
        pass
    return safe


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__); parser.parse_args(argv)
    print(json.dumps({"result": "PLAN_ONLY", "imageId": WP61, "context": CONTEXT,
                      "dockerOperations": 0, "exportAuthorized": False, "PUBLIC_RUNTIME_VERIFIED": False,
                      "liveAttemptReady": False}, sort_keys=True))
    return 0


if __name__ == "__main__": raise SystemExit(main())

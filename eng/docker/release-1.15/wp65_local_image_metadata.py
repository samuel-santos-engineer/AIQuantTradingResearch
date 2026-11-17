"""Offline projection and exact command construction; no default daemon operation."""
import argparse
import json
import re
from wp64_oci_provenance import WP61

CONTEXT = "desktop-linux"
ENDPOINT = "npipe:////./pipe/dockerDesktopLinuxEngine"
# Docker's json-template root is modeled as decoded inspect metadata. Unsupported shapes return null fields.
# No full-object serialization, custom functions, Env, labels or filesystem access.
_ROOT_TYPE = 'map[string]interface {}'
_ROOT_FIELDS = ('Id', 'Os', 'Architecture', 'RepoDigests')
_CONFIG_FIELDS = ('User', 'ExposedPorts', 'Entrypoint', 'Cmd')
_NULL_OBJECT = '{' + ','.join('"' + key + '":null' for key in _ROOT_FIELDS + _CONFIG_FIELDS) + '}'
FORMAT = (
    '{{if eq (printf "%T" .) "' + _ROOT_TYPE + '"}}'
    '{{$config := index . "Config"}}{'
    + ','.join('"' + key + '":{{json (index . "' + key + '")}}' for key in _ROOT_FIELDS)
    + ',' + ','.join('"' + key + '":{{if eq (printf "%T" $config) "' + _ROOT_TYPE + '"}}'
                    '{{json (index $config "' + key + '")}}{{else}}null{{end}}' for key in _CONFIG_FIELDS)
    + '}{{else}}' + _NULL_OBJECT + '{{end}}'
)
TEMPLATE_GO_EVALUATED = False  # Remains false until an actual offline Go harness is run and reviewed.

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
        if not all(isinstance(d.get(name), str) for name in ("Os", "Architecture", "User")): return safe
        ports = d.get("ExposedPorts")
        if not isinstance(ports, dict) or any(not re.fullmatch(r"[0-9]{1,5}/(?:tcp|udp)", name) or not 1 <= int(name.split("/")[0]) <= 65535 for name in ports): return safe
        entry, cmd = d.get("Entrypoint"), d.get("Cmd")
        if cmd is None: cmd = []  # Nil Cmd is valid; false/empty-string/wrong types must not be coerced.
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
                      "dockerOperations": 0, "templateGoEvaluated": TEMPLATE_GO_EVALUATED, "exportAuthorized": False, "PUBLIC_RUNTIME_VERIFIED": False,
                      "liveAttemptReady": False}, sort_keys=True))
    return 0


if __name__ == "__main__": raise SystemExit(main())

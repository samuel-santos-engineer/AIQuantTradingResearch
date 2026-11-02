"""WP35 cached-input Linux packaging. No registry/Azure operation or key input."""
import argparse
import hashlib
import json
from pathlib import Path
import subprocess
import tempfile
import zipfile

BASE = "mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f"
PROJECT = "src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj"
DOCKERFILE = "container/release_1_15/research-api.Dockerfile"
BOOTSTRAP = "container/release_1_15/research-bootstrap.sh"


def plan(commit):
    if len(commit) != 40 or any(c not in "0123456789abcdef" for c in commit):
        raise ValueError("SOURCE_COMMIT_INVALID")
    return {
        "result": "PLAN_ONLY", "sourceCommit": commit, "platform": "linux/amd64",
        "base": BASE, "localTag": "aiq-wp35-research:" + commit,
        "entrypoint": ["/bin/sh", "/app/research-bootstrap.sh"],
        "servingCommand": ["dotnet", "/app/AIQuantTradingResearch.Worker.dll", "--research-snapshot-api"],
        "port": 52731, "user": "1654:1654",
        "buildRequires": ["F1_FEASIBILITY_EVIDENCE_OR_OWNER_APPROVED_REVERSIBLE_ATTEMPT", "LOCAL_DOCKER_LINUX_AMD64", "CACHED_PINNED_BASE", "CACHED_DOTNET_PACKAGES_AND_RUNTIME_PACKS"],
        "registryPublished": False, "azureDeployed": False, "health": "UNVERIFIED",
        "liveAttemptReady": False, "AUTHENTIC_DATA_NOT_IMPORTED": True, "PUBLIC_RUNTIME_VERIFIED": False,
    }


def recipe(source, context, empty_feed):
    project = str(source / PROJECT)
    return [
        ["dotnet", "restore", project, "--source", str(empty_feed), "--configfile", str(source / "wp35-offline.config"), "-p:NuGetAudit=false", "-p:SelfContained=false", "-p:UseAppHost=false"],
        ["dotnet", "publish", project, "--configuration", "Release", "--self-contained", "false", "--no-restore", "-p:ContinuousIntegrationBuild=true", "-p:UseAppHost=false", "--output", str(context / "research")],
    ]


def run(args, cwd, timeout=30):
    # Tool output is deliberately not forwarded or persisted. No raw exceptions.
    completed = subprocess.run(args, cwd=cwd, capture_output=True, timeout=timeout)
    if completed.returncode:
        raise ValueError("TOOL_FAILED")
    return completed.stdout


def build(root, record):
    # All outputs are new owned local directories; never delete existing resources.
    owned = Path(tempfile.mkdtemp(prefix="aiq-wp35-build-"))
    record["ownedOutput"] = str(owned)
    record["result"] = "LOCAL_BUILD_BLOCKED"
    stage = "DOCKER_BASE"
    try:
        meta = json.loads(run(["docker", "image", "inspect", BASE], root))[0]
        if meta["Os"] != "linux" or meta["Architecture"] != "amd64":
            raise ValueError("BASE_PLATFORM_MISMATCH")
        stage = "SOURCE_EXPORT"
        archive = owned / "source.zip"
        archive.write_bytes(run(["git", "archive", "--format=zip", record["sourceCommit"]], root))
        source = owned / "source"
        source.mkdir()
        with zipfile.ZipFile(archive) as bundle:
            for member in bundle.infolist():
                target = (source / member.filename).resolve()
                if not target.is_relative_to(source.resolve()) or (member.external_attr >> 16) & 0o170000 == 0o120000:
                    raise ValueError("UNSAFE_ARCHIVE")
            bundle.extractall(source)
        # Exact reviewed tracked source only; no owner's untracked generator/key files.
        context = owned / "context"
        (context / "research").mkdir(parents=True)
        empty_feed = owned / "empty-feed"
        empty_feed.mkdir()
        (source / "wp35-offline.config").write_text('<configuration><packageSources><clear /></packageSources></configuration>\n', encoding="utf-8", newline="\n")
        for stage, command in zip(("OFFLINE_RESTORE", "LINUX_PUBLISH"), recipe(source, context, empty_feed)):
            run(command, source, timeout=180)
        stage = "PUBLISH_VERIFY"
        published = context / "research"
        runtime = json.loads((published / "AIQuantTradingResearch.Worker.runtimeconfig.json").read_text(encoding="utf-8"))
        if runtime["runtimeOptions"]["tfm"] != "net10.0" or not (published / "AIQuantTradingResearch.Worker.dll").is_file():
            raise ValueError("PUBLISH_INVALID")
        if not list(published.rglob("libe_sqlite3.so")):
            raise ValueError("LINUX_SQLITE_MISSING")
        for name in (BOOTSTRAP, DOCKERFILE):
            destination = context / name
            destination.parent.mkdir(parents=True, exist_ok=True)
            data = (source / name).read_bytes()
            if b"\r" in data:
                raise ValueError("LF_REQUIRED")
            destination.write_bytes(data)
        manifest = {p.relative_to(context).as_posix(): hashlib.sha256(p.read_bytes()).hexdigest() for p in sorted(context.rglob("*")) if p.is_file()}
        (owned / "context-sha256.json").write_text(json.dumps(manifest, indent=2) + "\n", encoding="utf-8", newline="\n")
        stage = "DOCKER_BUILD"
        run(["docker", "build", "--platform", "linux/amd64", "--pull=false", "--network=none", "--file", str(context / DOCKERFILE), "--tag", record["localTag"], str(context)], root, timeout=300)
        stage = "IMAGE_VERIFY"
        image = json.loads(run(["docker", "image", "inspect", record["localTag"]], root))[0]
        config = image["Config"]
        if (image["Os"], image["Architecture"], config["User"], config["Entrypoint"]) != ("linux", "amd64", record["user"], record["entrypoint"]):
            raise ValueError("IMAGE_CONTRACT_MISMATCH")
        if "52731/tcp" not in config.get("ExposedPorts", {}) or "AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0" not in config.get("Env", []):
            raise ValueError("IMAGE_PORT_BIND_MISMATCH")
        record.update(result="LOCAL_IMAGE_BUILT_RUNTIME_UNQUALIFIED", imageId=image["Id"], registryDigest=None)
    except (OSError, ValueError, KeyError, IndexError, TypeError, zipfile.BadZipFile, subprocess.TimeoutExpired):
        record["failedStage"] = stage
        return 2
    finally:
        (owned / "result.json").write_text(json.dumps(record, indent=2) + "\n", encoding="utf-8", newline="\n")
    return 0


def main(argv=None):
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--repository", type=Path, required=True)
    parser.add_argument("--build", action="store_true")
    parser.add_argument("--f1-feasibility-confirmed", action="store_true")
    parser.add_argument("--prepare-approved-compatibility-attempt", action="store_true")
    args = parser.parse_args(argv)
    try:
        commit = run(["git", "rev-parse", "HEAD"], args.repository).decode("ascii").strip()
        record = plan(commit)
        code = 0
        if args.build:
            if not (args.f1_feasibility_confirmed or args.prepare_approved_compatibility_attempt):
                record["result"] = "BUILD_BLOCKED_F1_FEASIBILITY_REQUIRED"
                code = 2
            else:
                # An operator assertion is not independent Azure compatibility proof.
                record["feasibilityBasis"] = ("OWNER_APPROVED_REVERSIBLE_ATTEMPT_COMPATIBILITY_UNVERIFIED"
                    if args.prepare_approved_compatibility_attempt else "OPERATOR_CONFIRMATION_REQUIRES_REVIEWED_EVIDENCE")
                code = build(args.repository, record)
    except (OSError, ValueError, subprocess.TimeoutExpired):
        record = {"result": "LOCAL_PREFLIGHT_BLOCKED", "liveAttemptReady": False, "AUTHENTIC_DATA_NOT_IMPORTED": True, "PUBLIC_RUNTIME_VERIFIED": False}
        code = 2
    print(json.dumps(record))
    return code


if __name__ == "__main__":
    raise SystemExit(main())

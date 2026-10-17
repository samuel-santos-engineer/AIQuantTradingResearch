# C2B-26 — bounded Linux qualification evidence

2026-10-16. **Partial Linux feasibility demonstrated; Linux browser acceptance incomplete; Azure/public readiness unqualified.** No deployment, promotion, merge, Azure configuration or readiness change.

## PR separation and document preservation

Both live PRs initially OPEN, common base `33b941c28b0158810b4f406db965bf83ff92244a`:

- PR #346 documentation branch `feature/release-1.14-c2b-24-azure-feasibility`, initial head `79b175e666f17d1f7f70776516246f81e6cbc9e6`. Includes the fourteen reviewed incoming owner documents, this report and the [exact-path/byte/SHA-256 manifest](release-1.14-chart-mvp-c2b-26-document-inclusion-manifest.md), plus its original assessment. Final documentation-only inventory is seventeen Markdown paths, no source or datasets. Final head is recorded in the PR description after commit.
- PR #347 implementation branch `feature/release-1.14-c2b-25-immutable-handoff`, initial head `35e09dc4b98669111bb88cd83f6d1cf662d29f15`; final head `8240d0878dea335c08ad3216600fc7e545446140`. Same ten paths as before; only C2B-26 delta is two lines in `python/presentation/test_immutable_synthetic_chart.py` making copied fault-injection targets writable. No reader/producer/runtime/generator/calculator/E4/Dockerfile/entrypoint change.

Exact PR #347 inventory: `.dockerignore`, `.gitignore`, `src/AIQuantTradingResearch.Worker/Program.cs`, `src/AIQuantTradingResearch.Worker/ImmutableSyntheticChartHandoff.cs`, `tests/AIQuantTradingResearch.Infrastructure.Tests/ImmutableSyntheticChartHandoffTests.cs`, `python/presentation/immutable_synthetic_chart.py`, `python/presentation/test_immutable_synthetic_chart.py`, `python/presentation/realtime_financial_visualization.py`, and the C2B-25 `immutable-handoff-contract.md` / `immutable-handoff-evidence.md` documents in this prompters folder. None imported into #346. The two PR histories remain separate, open and unmerged.

Source qualification used an owned `git archive` of exact #347 initial head, then the two-line helper correction. Final image's Python source matches the final #347 head; all production code is unchanged from its initial head. No worktree reset/stash/clean or owner-file movement; no protected WP07/archive checkout. Incoming owner bytes and staged/committed blobs are hash-checked by the manifest.

## Local environment and image construction

Docker Windows/amd64 client29.7.2, desktop-linux context; Linux/amd64 server29.7.2, Docker Desktop4.88.1, kernel6.18.33.2-microsoft-standard-WSL2. Existing local runtime image `sha256:56a9bab04e7b03441f6b811fd6afbc67b18203c7e0ebf0540b69298d19f66cd5` provided .NET/ASP.NET10.0.12, CPython3.13.15, Streamlit1.61.1 and Plotly7.1.0. Its original entrypoint was bypassed; no original WP07 execution took place. No package downloads or image push.

Temporary context: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b26-04252ba963d34396b95fd152bbd15ed4`. It held archived reviewed source, offline-built binaries and sanitized qualification scripts. No host credentials/private data mounted. A separate Dockerfile in that temporary directory reused the existing image, removed its `/app` and `/runtime` paths in the new derived layer, copied reviewed Python to `/qualification/presentation` with UID/GID1000 and built DLLs to `/qualification/worker`, and used an `env -i` entrypoint. Production Dockerfile/entrypoint were not edited or tested as a deployment artifact.

Final temporary image identity: `sha256:6ba5762c70a1b0d94e7ed97272b75b3b79e1828efcb333435f223b0975ac834f`; earlier uncorrected-test image `sha256:0c6bd06d469c44c11b39bb6b2af009cbc82b89f7dbe0d248ab8f07e2ca99ef0b`. Both tags were removed at cleanup. Existing runtime image identity remained unchanged. These images were exploratory local test vehicles, not Azure delivery images or artifact promotions.

An initial bare `FROM sha256:...` was misinterpreted by BuildKit as a registry repository and failed authorization/metadata lookup; no image downloaded. Corrected to the already-present local tag, `--pull=false --network none`, then both builds succeeded. A first Python runtime-probe quoting error was corrected using a temporary script. Neither failure is counted as runtime qualification.

## Bounds and offline C# generation

Unprivileged workload containers: one CPU (`cpu.max=100000 100000`), memory512MiB with equal swap limit, PIDs64, read-only root filesystem, all capabilities dropped, no-new-privileges, network none, no published ports, 32MiB noexec/nosuid tmpfs at `/tmp` owned1000. Producer timeout60s; reader/tests/negative timeout40s; UI timeout120s. One setup-only root container with CHOWN capability initialized an empty owned scratch volume, bounded to128MiB/PIDs16/10s; no application ran privileged. These are exploratory caps, not F1 limits or guarantees.

Offline Windows SDK build of exact source with `--no-restore`, dedicated OutDir, test signing disabled: exit0, 0 errors, five existing E4 warnings,15.85s. Portable managed DLLs then executed on Linux .NET10.0.12 with no normal Worker DI. Offline producer UID/GID1000, scrubbed environment, no network, exit0, no OOM, elapsed container lifetime about1.60s. No F1 CPU inference from wall time.

Generated manifest SHA-256 `007b2c8a6ccec19ec54976b4baf735812b648d79294e6465df884c68537d3069`,1454 bytes; full thirteen-file set656176 bytes. Every member's length/hash matched C2B-25 Windows evidence. Frozen twelve selections and counts: 1h24/168/720/2160; 4h6/42/180/540; 1d1/7/30/90. Warmup250 and aiq-ta-v1 C# SMA identity preserved through existing producer/payload validation. No Python/JS SMA arithmetic, generator/oracle changes or independent numerical-proof claim.

Scratch volume's `data` subdirectory was mounted read-only at the reader's fixed path `/qualification/presentation/_immutable_chart_candidate`; no configured upstream/path override. This temporary local Docker volume/subpath mount is not an Azure artifact delivery or persistence mechanism. Generated bytes were never committed or promoted.

## Identity, permissions and read observations

Actual UID/GID1000. Artifact directory1000:1000 mode0700; manifest1000:1000 mode0400; presentation directory1000:1000 mode0755, protected by read-only root mount. Writes to artifact root, manifest, presentation directory and reader/anchor module denied with EROFS/EACCES. No Docker socket/host home/private-data mounts. `/app`, `/runtime`, `/home/aiq-private-research-cache`, `/home/aiq-market-cache` absent in the derived test filesystem. Env names only: explicit PATH/DOTNET_ROOT/HOME/TMPDIR/LANG/TZ/Python controls and candidate Development flag; no WEBSITE/provider/persistence/replay/visualization settings. This verifies declared test-container boundaries, not exhaustive Azure confidentiality or privileged forensic access to historical image layers.

Twelve full-set fixed-path acquisitions passed. Individual uninstrumented Linux wall times79.083–142.193ms (n=12, no p95 claim). Reader process CPU for those reads plus two-reader probe1.123342s; process `ru_maxrss`38540KiB. Separate cgroup memory.peak29569024 bytes, CPU usage1887757 microseconds at sample. RSS and cgroup counters have different accounting; neither is an F1 measurement. Two simultaneous readers yielded exactly one available and one immediate denial, no queued work. This is process-local behavior, not durable/global admission or multi-instance abuse qualification.

## Tests and failures

Focused Linux Python suite: **19 passed**,1.149s, exit0, no OOM. Same adapter/default shell/public-boundary group as C2B-25. Linux ownership/mode negative branch ran. Bare-mode missing-ScriptRunContext warnings occurred; PowerShell presented native stderr as NativeCommandError records despite actual container exit0. No full suite or repeated E4 proof.

Initial real-artifact semantic helper failed with PermissionError because copytree retained0400 files and the helper tried to rewrite them. This was a harness failure, not an immutable-reader failure. Exactly two chmod calls now affect only the owned scratch member/manifest copies; immutable source remains read-only. Corrected image ran the same twelve selection validations, **twelve semantic rejection cases**, plus **five real Linux filesystem faults** (missing, tampered, truncated, extra member, actual symlink): all passed, exit0, no OOM. No fault was ignored or assertion weakened. Focused19-unit suite ran before this helper-only correction; the corrected helper was directly exercised by all semantic negatives afterward.

Artifact byte caps, malformed schema/UTF8/duplicates/provenance/count/scale, disabled gates and deadline denial were covered by the focused suite and finite semantic checks. HTTP request-body sizing was not separately load-tested; this candidate has no HTTP artifact upload or dynamic-data request endpoint. The cooperative reader budget is not a kernel-enforced CPU/I/O deadline. No unbounded traffic, retry storm or request-size benchmark.

## UI startup and ingress — browser gap

Started actual `public_preview_app.py` with installed Streamlit inside the bounded container, address127.0.0.1 port8502, candidate Development mode. Docker reported network none, empty port bindings and readonly mounts. Process tree showed only timeout and Python/Streamlit, no C# Worker/generator. `/proc/net/tcp` showed the sole listening address127.0.0.1:8502.

Internal HTTP reads,700ms spacing:

| Route | Result |
|---|---|
| /_stcore/health | 200 |
| /api/v1/dataset | 503 DATASET_NOT_READY |
| /api/v1/candles | 503 DATASET_NOT_READY |
| /api/v1/indicators | 503 DATASET_NOT_READY |
| /admin | 404 |
| /_immutable_chart_candidate/manifest.json | 200 Streamlit SPA HTML,10951 bytes; no manifest schema/hash/data |
| /local-chart/data | Same200 SPA HTML; no handoff/data payload |

The two unknown paths are HTML fallback, not new data endpoints; no port was exposed to host/LAN/public ingress. These checks do not prove a future public reverse-proxy/image route model. The first temporary route script was named http.py and shadowed Python's standard library; renamed to probe_routes.py and all assertions passed afterward. Temporary scripts only, no application correction.

UI cgroup memory.peak85655552 bytes; CPU usage2333830 microseconds; eight throttled periods/683316 microseconds at sample; one docker stats sample64.6MiB, fivePIDs. This is unloaded startup/internal-route evidence, not an interactive chart load or F1 sizing pass. Owned UI stopped explicitly, exit143, no OOM; no crash-safe shutdown/guardian claim.

**Linux actual-browser smoke NOT RUN.** No browser was installed in the container; existing loopback-only candidate gate and network-none boundary prevent host Edge ingress. No port forwarding, host-network change, browser package install, new proxy or gate relaxation was introduced. Prior C2B-25 Windows1D/30D off/on/off is historical and not substituted for Linux acceptance. No new screenshots; selectors, hover, modebar and SMA toggle remain unverified in an actual Linux-container browser. Owner-accepted zoom-reset limitation unchanged.

## Reproduction and cleanup

All orchestration used Windows PowerShell5.1. Key commands (owned paths only):

```powershell
docker version --format '{{json .}}'
docker context show
docker info --format '{{.ServerVersion}}'
git archive --format=zip --output="$run\source.zip" 35e09dc4b98669111bb88cd83f6d1cf662d29f15
# Expand archive in $run; build exact source to $run\build with no restore.
docker build --pull=false --network none -f "$run\Dockerfile.qualification" -t aiq-c2b26-qualification:corrected-test "$run"
```

Actual temporary Dockerfile (not the production Dockerfile):

```dockerfile
FROM ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp07-pid-retry-7e857b38
USER root
RUN rm -rf /app /runtime
COPY --chown=1000:1000 source/python/presentation/ /qualification/presentation/
COPY build/ /qualification/worker/
COPY runtime.py /qualification/runtime.py
WORKDIR /qualification/presentation
USER 1000:1000
ENTRYPOINT ["/usr/bin/env","-i","PATH=/usr/share/dotnet:/opt/venv/bin:/usr/bin:/bin","DOTNET_ROOT=/usr/share/dotnet","PYTHONDONTWRITEBYTECODE=1","HOME=/tmp","TMPDIR=/tmp","LANG=C.UTF-8","TZ=UTC","DOTNET_PROCESSOR_COUNT=1"]
```

Check the local tag resolves to the pinned identity before reuse; do not pull a replacement silently. `runtime.py` only prints installed Python/Streamlit/Plotly versions. Source was updated solely with the tested two-line helper correction before final build. Use a newly owned labeled volume, initialized solely for UID1000 as described above; never mount host private data or credentials. Example workload argument array/commands (after owned volume initialization):

```powershell
$bounds = @('--network','none','--read-only','--cpus','1','--memory','512m','--memory-swap','512m','--pids-limit','64','--cap-drop','ALL','--security-opt','no-new-privileges','--tmpfs','/tmp:rw,noexec,nosuid,size=32m,uid=1000,gid=1000')
docker run --name aiq-c2b26-producer @bounds --mount "type=volume,source=$volume,target=/scratch" aiq-c2b26-qualification:corrected-test env DOTNET_ENVIRONMENT=Development AIQ_IMMUTABLE_EXPORT=1 timeout 60 dotnet /qualification/worker/AIQuantTradingResearch.Worker.dll --write-immutable-chart-candidate /scratch/data
$readerMount = "type=volume,source=$volume,volume-subpath=data,target=/qualification/presentation/_immutable_chart_candidate,readonly"
docker run --name aiq-c2b26-integration @bounds --mount $readerMount aiq-c2b26-qualification:corrected-test timeout 40 python -B test_immutable_synthetic_chart.py --candidate /qualification/presentation/_immutable_chart_candidate
docker run --name aiq-c2b26-ui @bounds --mount $readerMount aiq-c2b26-qualification:corrected-test env DOTNET_ENVIRONMENT=Development AIQ_IMMUTABLE_CHART_CANDIDATE=1 timeout 120 streamlit run /qualification/presentation/public_preview_app.py --server.address 127.0.0.1 --server.port 8502 --server.headless true --browser.gatherUsageStats false --server.fileWatcherType none --secrets.files []
```

Names above are examples; stop on collisions and record newly owned IDs. Reader mount is temporary proof only. Independent runtime delivery design remains unselected.

Focused command inside image:

```text
timeout 40 python -B -m unittest test_immutable_synthetic_chart test_local_synthetic_chart test_public_preview_shell test_public_preview_api.PublicApiTests.test_six_exact_routes_and_liveness_readiness_are_distinct -v
```

Temporary scripts, sanitized outputs and context retained at the owned run path for inspection; no market artifacts in Git. All six named workload containers, owned scratch volume and two temporary image tags removed. Pre-existing images preserved, no unrelated running containers existed or were changed. Build cache not broadly pruned. No registry push, Azure/Actions/configuration operation, dependency install, public promotion or merge.

## Remaining decisions

Local Linux reader/producer feasibility is supported under these explicit mounts and caps. Full Linux browser acceptance, independently reviewed complete derived parity/promotion provenance, production non-writable ownership and delivery mechanism, complete-read/send/cancellation accounting, durable abuse/health controls and measured F1/multi-session resources remain unresolved. No blanket WP07 waiver. Neither named Azure site is approved for rollout; each still needs separate publication-role, deployment, rollback and readiness review. Do not infer public health from ARM Running or these local checks.

`PUBLIC_RUNTIME_VERIFIED=False`; public activation/deployment **NO-GO**; advanced WP07 **ON HOLD**. Both PRs remain **OPEN/UNMERGED**.

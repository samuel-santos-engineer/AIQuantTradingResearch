# Release 1.15 WP01 S2 - Dual-process Docker feasibility evidence

Date: 2026-10-22. Issue #367. Base: `455b80d0e50c17791653457a4ebd3001d3629a6e`. Owner authorized S2 implementation and bounded execution, including the proposed one-child-per-role interruption procedure. This report is separate from S1.

**Verdict: PARTIAL FEASIBILITY SUPPORTED; qualification INCOMPLETE.** The two non-root child processes, fixture SQLite access boundary, loopback listener and bounded observed restart work locally. Root-supervisor trust, complete recovery, actual browser concurrency and Azure storage/capacity are not qualified. This is not a production runtime implementation.

## Exact changed paths and dependencies

Seven new files:

- `tests/feasibility/release_1_15/runtime/ResearchStub.csproj`
- `tests/feasibility/release_1_15/runtime/Program.cs`
- `tests/feasibility/release_1_15/runtime/supervisor.py`
- `tests/feasibility/release_1_15/runtime/test_supervisor.py`
- `tests/feasibility/release_1_15/runtime/public_app.py`
- `tests/feasibility/release_1_15/runtime/Dockerfile`
- `tests/feasibility/release_1_15/runtime/qualify_runtime.sh`

This evidence is the eighth S2 path. No solution/project/dependency file outside this boundary changed. The isolated project uses the existing Microsoft.Data.Sqlite 10.0.11 central pin. No production API, adapter, ingestion, classification or UI was changed.

Docker Desktop was installed but initially stopped; starting it restored engine access (29.7.2). The pinned bases were present in BuildKit even though they were not named local images. A stdin prerequisite build verified SDK 10.0.402 and ASP.NET/.NET runtime 10.0.13 from the repository's pinned digests. Host SDK 10.0.103 is not claimed as Linux evidence.

Offline build reused six cached archives: Microsoft.Data.Sqlite and Microsoft.Data.Sqlite.Core 10.0.11; SQLitePCLRaw.bundle_e_sqlite3/core/provider.e_sqlite3/lib.e_sqlite3 2.1.12. The first archive-staging check unnecessarily requested System.Memory 4.5.3 and stopped because it was absent. Inspection of the existing net10 dependency graph showed no selected System.Memory dependency; the actual six-archive offline restore and publish succeeded. No dependency was downloaded, substituted or added to repository pins.

Build command from the isolated worktree (PowerShell 5.1; `$offline` is an owned temporary directory containing those six archives):

```powershell
docker build --pull=false --network=none --progress=plain --build-context "offline=$offline" -f tests/feasibility/release_1_15/runtime/Dockerfile -t aiq-r115-wp01-runtime:local .
```

All restore/publish RUN steps had networking disabled. Registry metadata resolution is not a claim that Docker's build control plane was air-gapped. The existing Python base resolved to `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`; Streamlit 1.61.1 / Plotly 7.1.0 asserted at build. Local experiment image ID: `sha256:622528971912f004275a6469bb2901bec63de8f16549f56df5d52caa1b34a7f0`. Nothing was pushed.

`python -B -m unittest discover -s tests/feasibility/release_1_15/runtime -p test_supervisor.py -v`: **4 passed**, exit 0. Covers fixed distinct non-root child IDs, bounded exponential delays (1,2,4,8,16,30 seconds), reset-on-health and repeated-failure bound. These tests are policy checks, not independent kernel ownership proof.

## Executed profile and boundary observations

One label-owned container: `--network none --read-only --cpus=1 --memory=512m --pids-limit=64 --tmpfs /tmp:rw,nosuid,nodev,size=33554432`, with one owned disposable volume at `/private`. No capability additions, privileged mode, host PID namespace, provider credential, Azure resource or production mount. Fake canary credentials only. Default Docker capabilities remain on root PID1 and are an explicit unresolved trust issue.

Ran `docker exec aiq-r115-wp01-runtime sh /spike/qualify_runtime.sh`, a public-UID process-file probe, one public-UID Streamlit AppTest render, bounded lifecycle probes and `docker stats --no-stream`. Inline host orchestration used standard-library subprocess timeouts; no additional repository script or browser framework was installed.

| Check | Observed result | Limit |
| --- | --- | --- |
| Process identities | PID1 UID 0; research UID/GID 12001; public UID/GID 12002; child supplementary groups empty | Root PID1 remains trusted, not an accepted unprivileged-supervisor design |
| Child capabilities | Both children CapEff 0; root PID1 CapEff `a80425fb` | Bounding set retained; least-privilege production hardening not complete |
| Private files | Public UID denied SQLite, checkpoint, watermark and fake key paths | Journal path denied by parent traversal; live journal permissions were not separately sampled; WAL not used |
| Process channels | Public UID denied research environ, research fd/0 and PID1 environ; actual public-process environ had no fixture provider key | Not exhaustive diagnostics/socket/FD isolation or hostile-process containment |
| Listener | C# 127.0.0.1:52731; Streamlit 0.0.0.0:8501 inside network-disabled namespace | No host/public ingress, Azure routing or external penetration test |
| Fixture snapshots | Both instruments returned exactly 1,000 fake transformed rows; classification Unavailable | Not actual ingestion, HMAC or indicator qualification |
| SQLite | Integrity check `ok`, 2,000 stored fixture rows; post-interruption watermark/checkpoint/version 25 | Stub validates integrity only, not full lineage/recovery semantics |
| Mount | Local `stat -f` reported ext2/ext3 family | Not Azure App Service filesystem evidence |

An initial process probe attempted to read the public process environment from an external root exec and received a kernel PermissionError. It was corrected to run the check as the public UID; no capability or isolation setting was relaxed. Both the original denial and successful public-UID probe are distinguished from application faults.

## Startup, interruptions and resource samples

Three process-start cycles total, on the same image/container/volume with warm filesystem cache. They are not three independent image-cold starts. Cycle 1 reached the private health/snapshot/isolation probe in 2.518 seconds; public health was not separately timed at that first instant. Cycles 2/3 reached both local health endpoints in 2.238/2.283 seconds from host start command. Earlier graceful stops took 0.720/0.861 seconds and exited 0 without OOM.

One SIGKILL of each confirmed fixed-role child was executed inside the owned container:

| Role | Old -> replacement PID | Other process health | Replacement healthy | Reaping evidence |
| --- | --- | --- | --- | --- |
| Research | 14 -> 79 | UI 200 | 1.506 seconds | Old PID absent; log reaped(-9) precedes started |
| Public UI | 15 -> 100 | Research 200 | 2.061 seconds | Old PID absent; log reaped(-9) precedes started |

Polling observed at most one fixed-role child at a time; this and wait-before-replace logs support this fixture's no-overlap behavior, not proof against arbitrary escaped descendants or all race conditions. No fork/escape attack was performed. SQLite remained intact after these interruptions. No crash was synchronized with a transaction/checkpoint/watermark write.

Sampled cgroup memory.current was 86,933,504 bytes early and 126,242,816 bytes after the single AppTest render/interruption sequence; observed memory.peak 215,736,320 bytes. Post-fault pids.current 22 of 64. Cumulative CPU usage at that sample: 8,089,276 microseconds; throttled time 3,658,833 microseconds. These are interval observations, not steady-state throughput or F1 quota evidence. Docker working-set samples were 102.8 MiB early and 101.1 MiB after later restart; they differ from raw cgroup accounting and must not be summed with RSS.

Public-UID AppTest produced one 1,000-row Plotly figure in 0.671 seconds (server-side test/render preparation only). No real browser was used. The planned 1/3/5 concurrent browser sessions, browser latency and loaded refresh capacity are **NOT RUN**. No capacity or 5-second/2-second/200-ms performance target is marked passed. No OOM was observed under this limited fixture load.

## Remaining recovery and security obligations

The stub is intentionally not the final atomic research contract: it has no correction/reconstruction intent, generation lineage, private algorithms, research event state, bounded cache or complete error/degraded policy. Watermark/checkpoint replacement uses ordinary file replacement without a qualified flush/directory durability protocol; a watermark exception is not the required production Degraded behavior. DB-ahead classification is integrity-only and cannot establish unwitnessed-commit lineage. Combined failure cases, partial transactions, watermark-ahead, corrupt checkpoints, pruning/evidence protection and recovery under sustained load remain untested. This source must not be promoted as production code.

D1-D7 disposition based on S1/S2:

| Decision | Supported now | Remaining closure / status |
| --- | --- | --- |
| D1 supervision | Two distinct non-root children and observed bounded fixed-role restart | PROVISIONAL: root PID1/capability trust and independent descendant closure not accepted |
| D2 storage | Local DELETE-journal fixture survives tested restarts; public traversal denied | PROVISIONAL: Azure mount, flush/rename durability and complete recovery proof absent |
| D3 numerics | No new numerical evidence from these spikes | OPEN: exact normalization/percentile/rounding/replay definitions and independent vectors |
| D4 wire | Fake bounded-size fixture can be served within one C# process | OPEN: canonical schema/limits/full-unchanged negatives not qualified |
| D5 HMAC | No HMAC vector or key rotation performed | OPEN: exact derivation/precision/rotation identity |
| D6 viewport | Native selection API/server fixture verified | BLOCKED: real relayout channel/browser evidence; separate component proposal prepared |
| D7 resources | Limited local startup/idle/render sample under 512 MiB | PROVISIONAL: real concurrent sessions, authoritative research load and actual F1 evidence absent |

Only existing architecture invariants remain fixed: separate private/public children, no raw/secret exposure, atomic authoritative publication before in-memory state, truthful identity/status, and required viewport preservation. No mechanism or numerical contract is falsely frozen from missing evidence. D1-D7 cannot all close on these results; WP01 acceptance/closure and WP02 advancement remain blocked.

## Cleanup and preservation

Both label-owned S1/S2 containers stopped with exit 0/no OOM and were removed. Only their owned disposable private volume and three owned experiment/prerequisite image tags were removed; no global prune or pre-existing resource deletion. Docker build cache was not purged. Repository main stays clean at the planning merge. Temporary observations contain no real credential or market data. New repository text is UTF-8/LF; independent CR scan and `git diff --check` apply before publication.

`PUBLIC_RUNTIME_VERIFIED=False`. No provider/Azure operation, image publication, deployment, public activation, WP01 closure or WP02 implementation.

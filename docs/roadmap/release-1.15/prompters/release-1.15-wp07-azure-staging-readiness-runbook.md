# Release 1.15 WP07 - Azure staging readiness and operator runbook

Date: 2026-10-25. Implementation baseline: merged PR #398, `00e8b52bb93edfb8f418808705e414a61d9105f2`; prior Linux qualification from #395 retained in append-only evidence.

**Outcome: preparation complete; deployment BLOCKED. PUBLIC_RUNTIME_VERIFIED=False.**
This is a reviewable operator checklist, not deployment authority or Azure runtime acceptance. All new/modified text must remain UTF-8/LF. This increment supplies a loopback-only operator launcher and chart acceptance, reusing the accepted #398 SQLite qualification and preserving its storage boundary; no dependency, schema, provider, Azure or production changes are included. PR #386/#387 are not imported. Issues #368/#369/#372/#373/#374 remain open for their own disposition.

## Exact publication scope

1. New `eng/docker/release-1.15/Invoke-Wp07LocalHistoricalChart.ps1`.
2. This existing runbook.
3. Append-only `docs/roadmap/release-1.15/RELEASE_1.15_WP03_WP04_FOUNDATION_PREFLIGHT.md`.

Prior #398 boundary/fault artifacts remain unchanged; its accepted qualification is reused.

No executable deployment script is necessary. The merged local qualification runner already defaults to non-mutating preflight; `-Execute` means **local Docker only**, never Azure. Commands below with placeholders are withheld operator steps, not commands executed during this review. Do not run them before the stated prerequisites and separate owner authorization.

## Verified staging inventory

Read-only Azure CLI 2.89.1 observations using existing authentication; no login, key acquisition, credential export or publishing-profile request. Values of provider/service/HMAC settings were not read or printed. Only setting names and the explicitly non-secret port/storage/startup-limit allowlist were projected.

- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`.
- Existing shared plan: `asp-aiq-r112-wp05-wus2-7f5eabb5`, SKU F1/Free, capacity 1. Both apps' serverFarmId equals this plan. Both are Linux, West US 2.
- UI: `aiqr112wp05wus27f5eabb5`; Running; HTTPS-only true; Always On false; no health-check path; no startup override; container port 8501; App Service storage explicitly false; no custom storage mounts.
- UI has an existing Release 1.14 registry-digest image assignment, not either new qualified research/chart image. Its rollback assignment must be retained securely by the operator; do not publish its personal registry namespace.
- UI setting names include legacy `TwelveData__ApiKey` and `Vike__ApiKey`. Presence does not establish validity or use. None of `AIQ_RESEARCH_CHART`, `AIQ_RESEARCH_BASE_URL`, `AIQ_RESEARCH_API_KEY`, `AIQ_RESEARCH_CA_FILE` is configured in the inspected app settings. Future UI deployment must remove research/provider secrets from that app under explicit authority, after privately capturing rollback configuration.
- Research: `aiqr115researchstagingwus27f5eabb5`; Running shell; linuxFxVersion empty; HTTPS-only false; publicNetworkAccess Enabled; Always On false; no app settings, custom storage mounts, health-check path or startup override. No qualified image is deployed.
- Both config projections show an Allow ingress rule and no explicit default-action value. This establishes no private ingress. No effective ingress denial or application behavior was tested remotely.
- Plan API returned null for some capacity metadata; these nulls are not zero usage or spare capacity. Two app definitions are not evidence of two usable concurrent workloads.

## Readiness decision matrix

| Requirement | Existing implementation / evidence | Result | Exact remediation or operator action |
|---|---|---|---|
| Two separate non-root single-principal Linux/amd64 images | Merged Dockerfiles; repeated local pair and process inspection | PASS_LOCAL | Preserve research UID/GID 1654:1654 and UI 12002:12002; qualify exact promoted pair |
| Immutable registry pairing | New local image IDs below; no push/readback | NOT RUN | Separately authorize publication; capture each registry RepoDigest and verify pulled config/platform matches qualified image |
| Correct staging resources | Read-only app/plan inspection, same F1 plan | PASS_LOCAL (inspection only) | Keep existing resources; do not infer Azure application PASS |
| Concurrent F1 workload and cold starts | Both Always On false; no workload deployed to research; local screening only | NOT RUN | Measure both apps together, idle/cold start, 1/3/5 sessions and quotas; stop if insufficient without tier change |
| Research platform routing | Default local HTTPS preserved; explicit staging-only HTTP backend mode on52731, same authenticated handler | IMPLEMENTED_LOCAL | Verify frontend HTTPS-only and actual Azure routing; platform backend is plaintext, not end-to-end TLS |
| External HTTPS and caller authentication | Local missing/wrong key, hostname and CA rejection PASS; research shell HTTPS-only false | BLOCKED | Require HTTPS-only, validated public hostname/CA and server-side key; prove direct anonymous/wrong-key rejection before UI activation |
| Research ingress | Public DNS/network enabled; Allow rule; no remote endpoint test | NOT RUN | Verify compatible restrictions if available; public DNS is not private ingress; authenticated bounded transformed endpoint only |
| Private persistent WAL storage | App Service is now rejected before directory/lock/SQLite creation; local UID/mount checks and fault harness | BLOCKED on F1 | No supported durable WAL/fencing arrangement exists in the inspected resources; choose a SQLite-preserving hosting adjustment, not a settings override |
| D5 provisioning and initial data | Operator-only production initializer, bounded completed-observation import and exact retry checks | IMPLEMENTED_LOCAL; acquisition NOT_RUN | Use independently acquired real observations; no provider call, automatic ingestion or Azure initialization performed |
| UI secret isolation | Local image/mount/browser negatives PASS; legacy Azure UI secret setting names remain | BLOCKED | Remove provider/HMAC/private mounts from UI; provide only environment-specific service key server-side and CA as needed |
| Health versus data readiness | Research GET /health returns Alive; snapshot validity checks actual state; UI /_stcore/health | PASS_LOCAL | Probe separately; liveness 200 cannot establish 401-candle readiness or chart usability |
| Lifecycle and writer exclusion | Local graceful and quiescent SIGKILL restarts, same-volume contender denied | PASS_LOCAL | Azure replacement overlap, interrupted-write/power-loss and cross-host lease semantics remain unqualified |
| Actual Azure browser/API/no-leak acceptance | No deployment or remote runtime requests in this task | NOT RUN | Separately authorize bounded staging verification; retain PUBLIC_RUNTIME_VERIFIED=False |

PASS_LOCAL in the inventory row means the inspection completed from the workstation, not local simulation of Azure or cloud workload acceptance.

## Routing and storage findings

Research starts `dotnet AIQuantTradingResearch.Worker.dll --research-snapshot-api`. Default `AIQ_RESEARCH_TRANSPORT=local-https` (or unset) still requires a private PFX and listens with TLS on52731. Explicit `azure-app-service-http` enables HTTP on52731 only with bind0.0.0.0, exact staging WEBSITE_SITE_NAME/WEBSITE_HOSTNAME, nonempty WEBSITE_INSTANCE_ID and `AIQ_RESEARCH_PLATFORM_HTTPS_ONLY=1`. Unknown modes or incomplete settings fail startup. Production identity is deliberately not enabled. **The new storage boundary denies all App Service writer startup, including this staging mode, before writable store access.** Transport code is retained; no environment flag currently authorizes an Azure writer.

**Trust boundary:** the UI's strict HTTPS client still validates the external Azure research hostname/certificate and sends X-API-Key server-side. Azure terminates TLS; the backend HTTP hop carries that credential and transformed response in plaintext within the platform boundary. `httpsOnly` does not encrypt that hop. The application neither authenticates the frontend nor treats forwarded headers as proof of TLS. The opt-in settings are operator assertions and can be simulated locally; they are not platform attestation or independent verification of httpsOnly. Azure ingress must prevent untrusted direct backend access, and actual frontend HTTPS-only/authentication must be verified before deployment acceptance. If this platform trust boundary is unacceptable, stop; this PR adds no proxy, paid service or silent client downgrade.

Microsoft documents persistent /home separately from container-local files; persistence alone does not prove SQLite semantics. SQLite documents WAL's same-host/shared-memory requirement and incompatibility with network filesystems. Current ResearchWriterOwnership explicitly says it is not a cross-App-Service lease; FileShare.None on one local Docker volume is not a distributed replacement lease. A shared network mount is therefore not an accepted WAL solution, and ephemeral local storage is not an accepted durable solution. Record the selected mount's actual filesystem, permissions, sync/rename behavior, replacement overlap and recovery witness evidence before considering activation. No storage backend is selected by this PR.

Sources checked 2026-10-25: [App Service custom containers](https://learn.microsoft.com/en-us/azure/app-service/configure-custom-container), [Linux App Service FAQ](https://learn.microsoft.com/en-us/troubleshoot/azure/app-service/faqs-app-service-linux-new), [App Service hosting plans](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans), [SQLite WAL](https://sqlite.org/wal.html). Free compute has per-app CPU quotas on shared hosts; these documents do not certify current workload headroom. Do not enable Always On or change the tier as an implicit fix.

## Runtime configuration contract

| Role | Required configuration / mounting | Current behavior |
|---|---|---|
| Research | AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0 (image default), AIQ_RESEARCH_PRIVATE_DIRECTORY, AIQ_RESEARCH_API_KEY, AIQ_RESEARCH_TLS_PFX in default HTTPS mode; optional AIQ_RESEARCH_TLS_PASSWORD; platform mode settings above | HTTPS by default or explicit platform HTTP on52731; existing witnessed DB and active D5 tuple required; generic startup failure exits 1 |
| UI | AIQ_RESEARCH_CHART=1 (image default), AIQ_RESEARCH_BASE_URL, AIQ_RESEARCH_API_KEY; optional AIQ_RESEARCH_CA_FILE for non-system trust | HTTP 8501 behind platform frontend; server-side HTTPS client validates research certificate/hostname |
| D5/private state | Separate high-entropy HMAC material, key epoch/transform epoch, DB and outside-DB watermark, writer.lock | Operator command below provisions through private state APIs; no automatic startup provisioning or live ingestion loop in snapshot entry point |
| TLS | Current local research PFX read-only at /tls/server.pfx; UI receives only public CA at /ca.pem | Local certificate SAN wp07-research is test-only, not valid for Azure research DNS |
| Storage | Research-only writable private directory 0700; DB/WAL/SHM/witness/lock 0600 owned by 1654; no UI research mount | Actual Azure permission/durability/overlap mechanism unresolved |

Future public research base URL is `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net` on external 443, **not** :52731 and never localhost from the UI. Platform certificate trust may use the system trust store, but this must follow the resolved transport design; never disable verification or deploy the local test PFX. The API service key differs from D5 and provider secrets and from production. Do not put secrets in shell arguments, URLs, browser state, image layers, git, screenshots or transcripts. A secure settings-delivery method remains an operator prerequisite, not an implemented credential utility.

POST /v1/research/snapshot authenticates X-API-Key before reading snapshots, rejects unsupported routes/methods and bounds bodies/headers/concurrency/time. /health deliberately exposes liveness only. The current implementation verifies one configured service key; do not describe it as proven two-key rotation from older issue wording. No rotation implementation or new gate is added here; replacement/rollback must account for both apps' key compatibility.

## Reproduce the already merged local qualification

Use Windows PowerShell 5.1, existing Python/Chrome/websockets, cached pinned bases, qualified dependency image and existing portable publish. No dependency installation, pull, restore or Azure operation is necessary. Inputs are private local operator paths, deliberately omitted here.

```powershell
# Variables must reference operator-owned, existing local inputs.
# No -Execute: non-mutating Docker/input preflight.
& ./eng/docker/release-1.15/Invoke-Wp07DualContainerQualification.ps1 `
  -PublishedResearchDirectory $PublishedResearchDirectory `
  -FixtureDirectory $FixtureDirectory -Python $ExistingPython
# Explicit local-only execution; no Azure/registry action.
& ./eng/docker/release-1.15/Invoke-Wp07DualContainerQualification.ps1 `
  -PublishedResearchDirectory $PublishedResearchDirectory `
  -FixtureDirectory $FixtureDirectory -Python $ExistingPython -Execute -KeepImages
$env:PYTHONPATH='python/presentation'
& $ExistingPython -B -m unittest python.presentation.test_research_chart python.presentation.test_research_snapshot_client
```

The fixture must be closed, disposable, explicitly test-only and within certificate validity. Do not confuse reuse of old build inputs with a fresh build from all current sources: this rerun used the prior portable publish manifest, unchanged relevant merged source hashes and current merged packaging/browser files. If input provenance differs or certificate expires, stop; use the explicit test export procedure only for local tests. Never copy the fixture into registry image contexts or Azure. The runner's build contexts contain allowlisted public code and portable research binaries, not private fixture files. Qualified dependency image is an existing offline input, not a package restore.

## Withheld staged operator sequence

Every numbered deployment step below is **NOT EXECUTED** and is conditional on separate authority plus resolved technical blockers. No command here is an automatic deployment path.

1. **Resolve prerequisites first.** Review the implemented platform trust boundary and initializer, and resolve durable storage/writer semantics plus actual acquisition/provisioning operations. Requalify changed images locally. No arbitrary schema/backend substitution or test-fixture publication. If these are unresolved, stop before registry publication or App Service mutation.
2. **Capture rollback privately.** Record both existing image assignments, startup/port/settings/mount/HTTPS/access configuration and recoverable DB/witness/D5 tuple. Research currently has no image; its rollback is the prior empty-shell configuration, not a fictional old research digest. Do not print secret settings, personal registry paths or evidence roots. Database rollback must preserve lineage; do not restore an older DB independently of the watermark/tuple.
3. **Freeze build provenance.** Record source SHA, both Dockerfile hashes, pinned base digests, qualified Python dependency input, portable publish manifest and Linux native SQLite input. Match actual framework versions. Build with network disabled from allowlisted contexts; qualify both exact images together. A code or transport change invalidates promotion of the old pair.
4. **Registry publication (separate permission).** Assign approved registry names, tag the qualified local IDs and push only those two images. Read back each registry manifest/RepoDigest; verify linux/amd64 and config ID match the qualified local image. Record immutable registry digests privately/sanitized. Local image IDs below are not deployable registry references. No ACR creation or registry login is authorized here.
5. **Configure research only after storage/transport closure.** Prepare research-only state/mount permissions, explicit platform-mode settings and frontend HTTPS-only, separate D5 initialization and service-key delivery. Do not reuse legacy synthetic DB, fixtures or UI provider credentials. Assign the approved research registry digest, selected container routing port, httpsOnly=true and /health; do not enable public anonymous snapshot access. No custom startup override is needed for the existing exec entrypoint. The explicit platform mode retains52731 and /health; actual platform routing is still NOT_RUN.
6. **Verify research before UI switch.** Through the Azure hostname prove valid frontend TLS; missing/wrong X-API-Key returns 401 without sensitive details. Authenticated bounded BTC/ETH full/unchanged requests must validate D4/D5 identities, readiness, gaps and no raw fields. /health=200 alone is insufficient. Verify actual filesystem modes, private state survival and exclusive writer across replacement without publishing credentials. Stop on any denial failure, unsupported durable mount or unknown witness.
7. **Configure UI and preserve rollback.** After research verification, remove provider/HMAC/private-storage configuration from this UI app; supply only server-side service key, research HTTPS hostname and public trust material if required. Assign the compatible UI registry digest and port 8501; leave exec Streamlit entrypoint intact. UI must have no research DB, HMAC/provider keys or TLS private-key mount. Do not weaken CORS, browser isolation or certificate validation to get a chart.
8. **Staging acceptance (separate permission).** Verify /_stcore/health and real browser BTC/ETH, indicators supplied by C#, 100/300/1000, UTC gaps, hover/zoom/pan, Return to Live, refresh/session isolation and research-outage/last-good recovery. Capture redacted console/network/server logs, ensuring no key/raw/private data or browser call to research DNS. Measure cold/warm startup, memory/CPU/latency and 1/3/5 sessions on the actual shared F1 plan; record observed quotas and idling. Do not invent thresholds or convert missing telemetry to PASS. Bound requests and stop at quota/resource failure rather than escalating tier or endlessly restarting.
9. **Rollback on failed checks.** Stop rollout, restore prior UI image/configuration and remove the new UI's access to research secrets. Restore research to captured prior image/config or empty shell, coordinated with compatible service keys; stop writers before state recovery. Preserve DB/witness integrity and private diagnostics, never force an older witness/tuple to open. Verify former UI health and no unintended research exposure. No production resource is involved.

Image assignment uses `az webapp config container set --resource-group <staging-group> --name <role-app> --container-image-name <approved-registry-repository>@sha256:<verified-registry-digest>`. This is a withheld template with unresolved placeholders, **not** an executable authorization. Secure Azure delivery/mount commands are deliberately not invented: durable storage and operational credential delivery remain unresolved. The initializer command is specified below but has not run in Azure. No end-to-end Azure deploy is certified.

## Minimum next owner action

Review this readiness PR first. The next engineering decision is a supported durable storage/writer arrangement; platform transport and initialization are now implemented locally, not accepted in Azure. Once resolved and locally qualified, separately authorize: publication of exactly two qualified images to an existing registry; updates only to the two named West US 2 staging apps (image, port, HTTPS/access, service-key/CA and qualified research state/mount settings); bounded runtime/restart/browser/negative verification and rollback. Keep the existing F1 plan, no new service or tier, no provider activation and no production changes. If those constraints cannot meet storage or capacity requirements, report the exact limitation for owner disposition. F1 quotas and idle cold starts can limit availability; no recurring-cost expansion is assumed.


## Operator-only initialization and completed-observation import

**IMPLEMENTED_LOCAL, App Service execution BLOCKED by storage boundary.** Invoke the published Worker once with exactly `--research-initialize`, never through an HTTP/admin endpoint or normal snapshot startup. Set `AIQ_RESEARCH_PRIVATE_DIRECTORY` to the qualified research-only directory. Supply a UTF-8 JSON object via redirected standard input, maximum4096 bytes and30-second command deadline:

- `generation`: explicit nonzero canonical lowercase UUID; no generation reset on existing DB.
- `keyEpoch`: separate explicit nonzero canonical lowercase UUID.
- `transformEpoch`: integer1 for a new store, exact installed epoch for a retry; this command does not rotate.
- `hmacKeyBase64`: exactly32 random private bytes encoded as Base64.
- `apiKey`: independently generated high-entropy printable ASCII service key,32..256 characters; cannot equal HMAC bytes or their Base64. Supply that same API key separately to the research/UI server settings; initializer validates separation but does not store the API key.

Use an existing secure stdin-delivery mechanism or an owner-only temporary configuration file and a ProcessStartInfo.RedirectStandardInput pipeline. Never use secret command arguments, shell history, console echo, image layers, source or PR evidence. Standard input must close at EOF; the deadline rejects a hanging pipe. No secret provisioning utility, token retrieval or Azure setting change is authorized here. Linux directory0700/files0600 are enforced by the existing store; Windows tests do not certify Linux permissions.

Optional `AIQ_RESEARCH_IMPORT_FILE` names an existing private UTF-8 JSON file, maximum1MiB, no symlink/reparse ancestry; Linux rejects group/other permissions. Exact envelope fields: `source` (literal `twelvedata`), `observedAtUtc` (nonfuture UTC `yyyy-MM-ddTHH:mm:ssZ`), `BTC/USD`, `ETH/USD`. Both arrays contain1..1000 rows, ordered strictly increasing. Exact row fields are `openTimeUtc` (minute-aligned UTC), `open`, `high`, `low`, `close`, `volume` (plain nonnegative decimal strings, at most12 fractional places, existing OHLC bounds). Reject duplicates, extra fields, malformed UTF-8, incomplete/future candles and out-of-order timestamps before creating state. Gaps are retained, not fabricated. All row validation uses WP03 and calculations use the existing private C# store; no Python/JavaScript analytics.

**Acquisition remains NOT_RUN:** the operator must obtain actual completed BTC/USD and ETH/USD one-minute Twelve Data observations through separately authorized private acquisition, then map the authentic timestamp/OHLCV fields into this private envelope without filling gaps. The `source` label is an operator assertion, not cryptographic provenance. This command performs no provider request and does not prove a real export exists. Test vectors used locally are disposable fabricated test inputs, not real market acquisition. Subsequent polling, corrections and retention use the existing WP03 pipeline under separate runtime authority; the initializer is not a polling service.

Without import, it creates an empty witnessed D5 state and explicitly makes no readiness claim. Imports use existing Commit/calculation/witness logic; fewer than401 contiguous rows remain unready. Matching tuple and identical imported rows are a no-op retry. Wrong generation/key/epoch, nonidentical re-import into nonempty state, live writer or invalid witness fails closed. It never resets or deletes state. Each instrument commits separately: a failure between instruments may leave a valid BTC-only import; exact rerun can skip the matching BTC rows and finish empty ETH. There is no cross-instrument atomicity claim. A crash between initial DB creation and D5 activation can leave an inactive store that this command rejects; preserve it for explicit recovery, never erase it automatically. API startup still requires an existing active tuple.


## SQLite durability and writer-exclusion disposition

**Outcome B: fail-closed implementation; Azure durability BLOCKED.** SQLite remains the sole authoritative datastore. No schema/journal-mode change, external database, blob-backed live DB, new lease or paid service is implemented. The research API cannot simply open this store read-only: its constructor validates/reconciles the external witness and holds writer ownership. No read-only Azure bypass is provided.

Before any private-directory creation, lock acquisition or writable SQLite open, ResearchStorageBoundary rejects presence of WEBSITE_INSTANCE_ID, WEBSITE_SITE_NAME, WEBSITE_HOSTNAME or WEBSITE_ROLE_INSTANCE_ID, the staging azure-app-service-http transport, and unknown AIQ_RESEARCH_STORAGE_SCOPE values. Unset/local-qualification is only local scope, **never** an override for Azure markers. Removing markers is not a supported deployment procedure or a security boundary against a privileged operator.

On Linux local execution, it reads kernel /proc/self/status and /proc/self/mountinfo, requires real/effective/saved/filesystem UID1654, resolves the deepest backing mount, validates device/root/mountpoint/filesystem fields and writable mount options, and rejects read-only/network/unknown/tmpfs storage. ext4/xfs/btrfs and overlay are allowed only for local qualification; overlay is explicitly ephemeral and is not a durable-hosting approval. Optional AIQ_RESEARCH_EXPECTED_MOUNT_PATH must equal the kernel's resolved mountpoint; the Linux harness pins /private. Kernel device/root/type are observable identity, not a persistent attestation or distributed lease. The existing store enforces0700/0600 and validates generation, commit chain, D5 activation and external witness. A mount-path match does not prove power-loss durability or identify a disk across migrations.

Layout for local qualification: mount/private contains data/ (research.sqlite, WAL/SHM when present, watermark.json, writer.lock, pending witness) owned1654:1654; the test-only sibling control/ contains fault-ready markers and is never passed as research state. Never split live WAL/SHM/DB from the directory or restore DB independently of its witness/D5 tuple. Preserve the whole quiesced recoverable lineage for backups/moves; no backup/migration is executed here. FileShare.None only coordinates writers using one host kernel and shared volume; it does not fence different App Service workers or copies on separate volumes.

### SQLite-preserving compatibility matrix

| Option | Durability and SQLite WAL | Writer exclusion / takeover | F1/cost consequence | Decision |
|---|---|---|---|---|
| F1 container writable layer / ephemeral host disk | Local semantics may run SQLite but state can disappear on replacement | No common authority across different hosts/volumes | Existing free compute; fails required durability | REJECTED |
| App Service persistent /home or Azure Files SMB | Documented network-backed persistence; WAL shared-memory/locking assumptions unsupported | Local lockfile is not a proven distributed fence; share persistence is insufficient | No new resource assumed; current research has no configured mount | BLOCKED; do not deploy WAL there |
| NFS / shared network filesystem | WAL is not qualified over network filesystems | No demonstrated single authoritative writer/fence | Would require supported mount/resources, not present here | REJECTED as an untested workaround |
| Upgrade only the App Service compute tier | Does not by itself supply a persistent local block volume contract | Does not by itself fence replacement writers | New recurring spend with storage problem unresolved | NOT RECOMMENDED as sole fix |
| Single owner-controlled Linux host with persistent local ext4/xfs block filesystem, both replacement containers on same kernel/volume | Candidate for SQLite WAL; still needs fsync/checkpoint/backup and host-crash validation | Kernel-held local lock excludes overlapping containers; kill/reap old writer before local takeover; cross-host migration must fence the old host first | Reuse existing suitable host only if one actually exists; none established by this inventory | SMALLEST TECHNICAL CANDIDATE, not implemented/deployed |
| New small Linux VM plus explicitly non-shared managed persistent disk | Durable block-storage candidate; filesystem and full DB/WAL/witness lineage must be qualified | One authoritative host; never attach/mount for a second writer while old host can access it; verified host shutdown/detach before migration | New VM, persistent disk, backup and possible network-transfer charges; not an F1 feature or zero-cost promise | Owner resource/cost/topology decision required |

Recommendation: keep the UI image on its existing F1 app and, **only if the owner accepts a research-hosting adjustment**, qualify the research image on one suitable Linux host with persistent block-backed local storage. Prefer an already-owned suitable host if independently available; otherwise a small VM/non-shared disk is the minimal Azure candidate, not a quote or approved purchase. Keep separate images/identities, strict UI HTTPS, service authentication and private SQLite. This changes the research hosting topology, so it needs explicit owner authorization and a scoped deployment/storage plan. No monthly price is asserted: VM size, region, disk capacity, backup retention and network usage must be selected and priced before approval. Do not silently create resources or treat this proposal as cross-host automatic failover.

Sources distinguish documented capabilities from our local tests: [App Service Linux storage FAQ](https://learn.microsoft.com/en-us/troubleshoot/azure/app-service/faqs-app-service-linux-new), [SQLite WAL limits](https://sqlite.org/wal.html), [Azure managed disk types](https://learn.microsoft.com/en-us/azure/virtual-machines/disks-types). Existing F1 exposes documented network persistence and ephemeral local storage, not a verified persistent local/block-backed research volume; no compatible arrangement was established. No live Azure mount/fault experiment was authorized or performed.

### Reproducible bounded local SQLite qualification

```powershell
# ResearchImage must be the immutable local SHA256 ID from the successful dual-container run.
# Without -Execute: read-only Docker/image/input preflight; no compiler/container/volume mutation.
& ./eng/docker/release-1.15/Invoke-Wp07SqliteQualification.ps1 `
  -ResearchImage $ResearchImage -PublishedResearchDirectory $PublishedResearchDirectory
# Explicit local execution only; exact owned cleanup in finally.
& ./eng/docker/release-1.15/Invoke-Wp07SqliteQualification.ps1 `
  -ResearchImage $ResearchImage -PublishedResearchDirectory $PublishedResearchDirectory -Execute
```

The harness reuses the installed .NET10 compiler/reference assemblies and existing published dependencies, without project/package/restore changes. It compiles a test-only helper under the existing friend-test assembly name to reach already existing internal fault hooks; the helper is mounted read-only into a container for qualification and is absent from published image layers. It uses only the local Windows Docker named-pipe endpoint, immutable Linux/amd64 image1654:1654, bounded256MiB/64 tasks, network none, read-only rootfs, dropped capabilities and exact-owner resources.

Fault marker is armed **after** constructor recovery. SIGKILL is issued only after observing the marker for: before-commit (uncommitted transaction should roll back); watermark-replace (DB committed, pending witness flushed, old witness still present; existing validated chain may advance witness); transform-before-commit (old D5 remains); transform-after-commit (new D5 activation ahead of old witness must reject recovery). This is interrupted process/transaction evidence with the host and disk still running, **not host power loss**. Reopen checks generation/version/D5/integrity and runs WAL checkpoint. Same-volume distinct containers test denial while owner lives and acquisition only after owner kill/wait. Distinct-volume writers deliberately both run, demonstrating absence of a global fence. Two simulated Azure instance markers must both fail before a new directory exists, even with local-qualification set. Read-only mount, mount mismatch, wrong UID, witness tampering and secret-output negatives remain fail-closed.

All Azure deployment, registry publication, real acquisition, actual host failure, distributed lease/fencing and Azure durable storage acceptance remain BLOCKED/NOT_RUN. PUBLIC_RUNTIME_VERIFIED=False. Next owner decision is whether to authorize a SQLite-preserving research-hosting/storage adjustment; there is no F1 environment-variable workaround in this code.


## Current chart-delivery increment: single writer, controlled replacement

**Owner decision:** PR #398 local SQLite/WAL, process-crash, witness and same-volume exclusion evidence is accepted for this milestone. Exactly one research writer is required. Brief stop-before-start downtime is acceptable. Distributed coordination, pooling, cross-host fencing, automatic failover and host-power-loss tests are **not work for this increment**. Earlier compatibility/fault sections above explain the retained F1 limitation; they do not impose another general SQLite investigation or new gate.

**Working local path:** the existing owned Windows workstation's Docker Desktop Linux/amd64 engine, existing qualified research/UI images and an existing default local Docker named volume. This uses existing local infrastructure without provisioning or additional cloud resources. The volume persists across normal container stops/replacements; host disk loss and public availability are not claimed. App Service F1 remains unsuitable for the current writable store, and ResearchStorageBoundary is not bypassed. No other suitable always-reachable Linux research host was established by this inspection.

### Minimal launcher

`eng/docker/release-1.15/Invoke-Wp07LocalHistoricalChart.ps1` is Windows PowerShell5.1 compatible. Default Action=Validate and absent -Execute are nonmutating Docker preflight. Start requires explicit -Execute, immutable local Linux/amd64 image IDs with1654:1654/12002:12002 users, an **existing** default local Docker volume, existing TLS material and an already supplied API key environment value. It never initializes, creates, deletes or overwrites the volume. It refuses a running container using that volume and refuses Start over its running session. No registry pull/build/provider request occurs.

Prepare variables from operator-owned existing inputs. These commands were tested using disposable fixture state; no real export is currently available or authorized for acquisition:

```powershell
$ResearchImage='sha256:284736e3d0c8b7cc1755141192eecd8611ffbd165f5c4b966cc8bbaa6e325fd5'
$UiImage='sha256:828bc0a1236d378a40acff5bd5c74537b818cc0427418640f37ec6f1c90cae3a'
# Supply $StateVolume and $TlsDirectory privately, and choose an unused loopback port.
# API key must already be in the process environment through a secure operator channel.
# Do not type secret values in shell history, commit them, or retrieve Azure secrets here.
& ./eng/docker/release-1.15/Invoke-Wp07LocalHistoricalChart.ps1 `
  -ResearchImage $ResearchImage -UiImage $UiImage -StateVolume $StateVolume `
  -TlsDirectory $TlsDirectory -Session aiq-r115-local-chart -Port 8501
& ./eng/docker/release-1.15/Invoke-Wp07LocalHistoricalChart.ps1 -Action Start -Execute `
  -ResearchImage $ResearchImage -UiImage $UiImage -StateVolume $StateVolume `
  -TlsDirectory $TlsDirectory -Session aiq-r115-local-chart -Port 8501
```

On success open `http://127.0.0.1:8501` (or the chosen port). Research has no published host port. UI server calls `https://wp07-research:52731` on an internal Docker network with validated CA/hostname and X-API-Key. TLS directory must contain existing server.pfx and public ca.pem; the certificate must be current with SAN wp07-research, and this minimal launcher uses the existing unpassworded-PFX local profile. Research alone receives the PFX directory; UI receives only the CA file and service key. No provider/HMAC/private-state mount is given to UI. Provider acquisition is offline/separate; research has no external network in this local profile.

Startup authenticates and validates both instrument snapshots using the **existing strict Python HTTPS client**, requires nonempty candles and records only public identities/counts. It then checks Streamlit health. Failed startup removes only resources created by that invocation, retaining the supplied state volume. Inspection projects only ownership labels/running state and non-secret image/volume metadata; no container environments or volume option values are dumped. Generic failure category/status may be recorded, never headers, keys or raw rows.

### Real observations and one-time initialization

**Actual acquisition NOT_RUN / not authorized.** Existing TwelveDataMinuteCandleSource requests UTC ascending1min windows, at most1000 observations per instrument, authenticates through an Authorization header, validates symbol/interval/timestamps/decimals and filters forming candles using completion grace. ResearchIngestionService is the existing bounded polling/commit path. Neither this launcher nor normal --research-snapshot-api startup invokes a live provider or polling loop. No legacy UI key is retrieved/reused and no test candle is represented as a real observation.

Shortest real-data bootstrap: obtain an independently authorized private completed-minute export for BTC/USD and ETH/USD; normalize authentic rows into the existing initializer envelope documented above, preserving gaps, bounds and UTC. The source label is an operator assertion, not acquisition evidence. Do not fabricate data or mark classification ready below401 consecutive candles. Authorization for real acquisition must specify the existing account/secret channel and bounded requests/credits; no request is made through this increment.

Run the **production** operator initializer once against a new protected private host directory (or retry the exact installed tuple). Configuration contains only the documented generation/keyEpoch/transformEpoch and distinct API/HMAC material. Input/export/config paths remain private, outside the repository. The following Windows PowerShell5.1 skeleton sends secrets through redirected stdin, never arguments:

```powershell
$psi=New-Object System.Diagnostics.ProcessStartInfo
$psi.FileName='dotnet'
$psi.Arguments='"'+(Join-Path $PublishedResearchDirectory 'AIQuantTradingResearch.Worker.dll')+'" --research-initialize'
$psi.UseShellExecute=$false
$psi.CreateNoWindow=$true
$psi.WindowStyle=[System.Diagnostics.ProcessWindowStyle]::Hidden
$psi.RedirectStandardInput=$true
$psi.RedirectStandardError=$true
$psi.EnvironmentVariables['AIQ_RESEARCH_PRIVATE_DIRECTORY']=$ProtectedPrivateDirectory
$psi.EnvironmentVariables['AIQ_RESEARCH_IMPORT_FILE']=$AuthorizedCompletedObservationFile
$p=[System.Diagnostics.Process]::Start($psi)
$p.StandardInput.Write([IO.File]::ReadAllText($ProtectedConfigurationFile,[Text.Encoding]::UTF8))
$p.StandardInput.Close()
if(-not $p.WaitForExit(35000)){$p.Kill();throw 'Owned initializer deadline exceeded; preserve state for review.'}
if($p.ExitCode -ne 0){throw 'Initialization rejected; preserve state and inspect generic diagnostics.'}
$p.Dispose()
```

Use owner-restricted host directories/config files. Close the initializer before transferring state; do not copy a live DB. D5 is persisted in the private store/witness, not the UI; same API key must independently be supplied to both server roles. The initializer does not acquire data, store the API key or start polling. A partial import retries exactly or fails closed, as previously documented.

For **first seeding only**, daemon-copy the entire closed initialized data directory into a **fresh** named volume with data/ owner1654 and modes0700/0600. Never seed over an existing volume or treat copy as a restart procedure. This bounded local recipe does not create an image layer or start a writer:

```powershell
$env:AIQ_SEED_VOLUME=$NewStateVolume
$env:AIQ_SEED_IMAGE=$ResearchImage
$env:AIQ_SEED_DIRECTORY=$ProtectedPrivateDirectory
# Python below uses existing standard library; no dependencies or provider/network calls.
```

Execute this Python through the existing interpreter (stdin or an owner-local script), leaving all file content private:

```python
import os,subprocess,uuid,io,tarfile,json,re
from pathlib import Path
def docker(*args,data=None):
    p=subprocess.run(['docker',*args],input=data,capture_output=True,timeout=30)
    if p.returncode: raise RuntimeError('Owned seed operation failed; preserve volume for review')
    return p.stdout
volume=os.environ['AIQ_SEED_VOLUME']; image=os.environ['AIQ_SEED_IMAGE']
assert re.fullmatch(r'[A-Za-z0-9][A-Za-z0-9_.-]{0,127}',volume)
assert re.fullmatch(r'sha256:[0-9a-f]{64}',image)
context=json.loads(docker('context','inspect'))[0]
assert context['Endpoints']['docker']['Host'].startswith('npipe://')
folder=Path(os.environ['AIQ_SEED_DIRECTORY']); seed='aiq-private-seed-'+uuid.uuid4().hex
if subprocess.run(['docker','volume','inspect','--format','{{.Name}}',volume],capture_output=True).returncode==0:
    raise RuntimeError('Refusing to seed an existing volume')
allowed={'research.sqlite','research.sqlite-wal','research.sqlite-shm','watermark.json','writer.lock','watermark.json.pending'}
assert folder.is_dir() and not folder.is_symlink()
assert (folder/'research.sqlite').is_file() and (folder/'watermark.json').is_file()
assert all(p.is_file() and not p.is_symlink() and p.name in allowed for p in folder.iterdir())
docker('volume','create','--label','aiq.local.research=true',volume)
docker('create','--name',seed,'--label','aiq.private.seed='+seed,
       '--mount','type=volume,source='+volume+',target=/private',image)
try:
    data=io.BytesIO()
    with tarfile.open(fileobj=data,mode='w') as archive:
        for p in [folder,*folder.iterdir()]:
            item=archive.gettarinfo(str(p),arcname='data' if p==folder else 'data/'+p.name)
            item.uid=item.gid=1654; item.uname=item.gname=''
            item.mode=0o700 if p.is_dir() else 0o600
            if p.is_file():
                with p.open('rb') as f: archive.addfile(item,f)
            else: archive.addfile(item)
    docker('cp','-',seed+':/private',data=data.getvalue())
finally:
    # Exact random name created above; no volume/data deletion, even after failure.
    docker('rm',seed)
```

This recipe is for the same already-selected local Docker Desktop endpoint; validate context before use, and do not target a remote daemon. It must not be run with WP07 fixture/test data as real public staging bootstrap. Failed seeding leaves its volume intact for operator review. After first seeding, the named volume is the sole active authority; the closed host directory is an offline backup only. Never launch a second researcher against that host copy. Existing qualified state requires **no copying or re-initialization** during normal restart.

### Replacement, rollback and browser verification

```powershell
# Stop UI and research; success is returned only after both are confirmed stopped.
& ./eng/docker/release-1.15/Invoke-Wp07LocalHistoricalChart.ps1 `
  -Action Stop -Session aiq-r115-local-chart -Execute
# Then Start with the same volume/tuple/key/CA and a qualified compatible image pair.
# Repeat the Start command above; it removes only owned stopped containers/networks.
```

No parallel researcher, automatic restart/failover or copied second authority. An unrelated running user of the volume causes refusal. Keep the brief outage honest: existing UI retains last-known data when only research is interrupted; full pair replacement reloads the UI. Verify BTC/USD and ETH/USD, historical completed-minute UTC candles, C# SMA/RSI, gaps, selectors/hover/zoom/pan/modebar, truthful stale/readiness states and reconnect. Browser must not call the provider/research API or receive keys/raw DB. Stop-before-start and return of the original public snapshot identities/row counts were exercised locally.

Rollback is stop/verify-stopped/start of the prior **compatible** image pair with the same authoritative volume, D5 tuple, certificate and service key. Never restore an older DB/witness independently, delete the volume, reset generation or silently change epochs. On mismatch, stop and preserve state for review. No real-data migration/rollback was executed here.

### Remaining online decision, not another durability workstream

The owned workstation now has a demonstrated loopback-only chart path; it is not an always-reachable research host for Azure staging. Existing F1 research remains blocked by the retained storage boundary. To get a real chart online, the owner must (1) authorize bounded real completed-observation acquisition or supply an already authorized export, and (2) designate an existing always-reachable Linux research host with persistent local/block storage, certificate/DNS and operator-controlled stop/start. None was established by the authorized staging inspection. If no such host exists, explicitly approve the minimal single-instance Linux compute/persistent-disk cost/topology adjustment already described above before provisioning. No distributed coordination, host-power-loss study, paid-resource purchase, public endpoint, registry push or deployment is authorized by this runbook. After that decision, separately authorize exact staging image/secret/config/endpoint changes and actual staging browser verification. PUBLIC_RUNTIME_VERIFIED=False.


## Staging-specific delivery checkpoint - 2026-10-25

Baseline: merged PR #399, `654274a3aca706008ea82bb2852b11d19200c092`. This checkpoint is read-only Azure inventory and deployment preparation, not runtime qualification. Reuse the accepted local results in #395-#399; no fault matrix or browser suite was repeated. All changes in this increment use UTF-8/LF.

| Item | Status | Actual evidence / remaining action |
| --- | --- | --- |
| Existing staging resource inventory | VERIFIED_AZURE_STAGING | Management plane only: the exact resource group contains one F1 plan and the two named Microsoft.Web/sites; no separate compute or disk resource is present there. No unrelated resource groups or subscriptions were searched. |
| UI staging configuration | VERIFIED_AZURE_STAGING | `aiqr112wp05wus27f5eabb5`: Running, Linux/container, assigned existing image, HTTPS-only true, public access enabled, Allow-all ingress, port8501, App Service storage false, no custom mounts, no AIQ_RESEARCH_* settings. AlwaysOn false; health-check path unset. Image namespace and credentials are deliberately omitted. |
| Research staging shell | VERIFIED_AZURE_STAGING | `aiqr115researchstagingwus27f5eabb5`: Running shell, Linux, no assigned image, HTTPS-only false, public access enabled, Allow-all ingress, no custom mounts, no port/storage or AIQ_RESEARCH_* settings. AlwaysOn false; health-check path unset. Running does not mean a working research service. |
| Plan association | VERIFIED_AZURE_STAGING | Both apps belong to `asp-aiq-r112-wp05-wus2-7f5eabb5`, West US2, F1/Free, capacity1. This is configuration evidence, not startup, throughput or concurrent-session evidence. |
| Strict public research HTTPS client | IMPLEMENTED_LOCAL | Existing ResearchSnapshotClient accepts an HTTPS hostname, uses ssl.create_default_context with system trust or an explicit CA, and sends X-API-Key server-side. Direct HTTPSConnection does not inherit proxies or follow redirects. No client change required. Accepted local evidence remains local. |
| Authorized authentic completed-minute input | BLOCKED_OWNER_INPUT | No approved real export was supplied or identified in the scoped operator inputs. Repository filename inventory found no candidate export; private disks were not searched. Existing disposable fixture rows are not real data and cannot bootstrap staging. No provider secrets or live calls accessed. |
| Persistent research host assigned to these staging apps | BLOCKED_OWNER_INPUT | No suitable host exists in the inspected group. An already-owned host elsewhere may be designated explicitly by the owner; its existence, persistence, TLS and reachability have not been established. Existing App Service writer rejection remains unchanged. |
| Research initialization, image publication and configuration/deployment | NOT_RUN | Require separate precise staging authority, real input and assigned host. No Azure/registry mutations executed. |
| Azure routing, TLS/auth negatives, startup, persistence and Chrome acceptance | NOT_RUN | Management-plane values do not establish behavior. Execute only against the named staging UI and explicitly assigned staging research host after authorization. |

### Two concrete owner inputs

1. Supply a private authorized Twelve Data completed-minute export for both BTC/USD and ETH/USD, in the existing initializer envelope: source, observedAtUtc, and ascending UTC minute rows with decimal-string OHLCV. Preserve gaps; at most1000 rows per instrument. At least401 consecutive rows are needed for the existing classification readiness. Alternatively, authorize bounded acquisition for these two symbols (one request of at most1000 rows each, explicit credit allowance/account and secure credential delivery). No live acquisition is authorized or executed here; no new licensing gate is introduced.
2. Designate an existing always-reachable Linux research host explicitly serving these staging apps, with a persistent local/block filesystem, protected state, strict HTTPS hostname/certificate and controlled stop/start. If none exists, the smallest topology proposal is one Linux VM with one persistent managed disk for the single SQLite authority, keeping UI on the existing F1 app. This is not established as feasible or free: compute, disk, networking and any backup costs need a region/size/retention-specific quote and explicit approval before provisioning. No paid resource, subscription expansion or hypothetical host is evaluated as Azure qualification.

Stop at these inputs. Do not build speculative infrastructure. The local launcher is a loopback development tool, not a remote Azure deployment tool. Do not remove App Service identity markers, use ephemeral storage as durable storage, enable unsupported F1 WAL, copy a live database, or run two writers.

### Staging command preparation - do not execute mutations yet

The following PowerShell5.1-compatible templates target the existing staging UI only. Variables are unresolved operator inputs, not verified image digests or endpoints. The registry publication, resource deployment and secret/config changes each require precise separate authorization. Existing local image SHA256 IDs are not pullable registry manifest digests. Research-host-specific commands cannot be made executable until the owner designates that host; no replacement host is assumed.

Before mutation, capture the current UI image and needed settings privately for rollback without printing them; freeze an immutable qualified UI registry digest. Obtain the server-side API key through the approved secure channel, never from browser input or shell arguments. Prepare a protected external JSON settings file containing WEBSITES_PORT=8501, AIQ_RESEARCH_CHART=1, AIQ_RESEARCH_BASE_URL pointing to the assigned HTTPS hostname, and AIQ_RESEARCH_API_KEY. Omit AIQ_RESEARCH_CA_FILE for system-trusted CA; a private CA needs separately approved delivery of its public certificate only. The UI receives no provider key, HMAC key, database or server private certificate. Preserve unrelated settings; do not change the existing F1 plan.

```powershell
# PREPARATION ONLY: NOT EXECUTED. Supply only owner-reviewed staging inputs.
$StagingGroup='rg-aiq-r112-wp05-wus2-7f5eabb5'
$StagingUi='aiqr112wp05wus27f5eabb5'
# $UiRegistryDigest: qualified pullable image reference, pinned by manifest digest.
# $ProtectedUiSettingsFile: protected external JSON object; never commit or print.
# $RollbackUiRegistryDigest / $ProtectedRollbackSettingsFile: privately captured baseline.
# Execute ONLY under new authority for these exact mutations:
az webapp config container set --resource-group $StagingGroup --name $StagingUi --container-image-name $UiRegistryDigest --output none
az webapp config appsettings set --resource-group $StagingGroup --name $StagingUi --settings "@$ProtectedUiSettingsFile" --output none
az webapp restart --resource-group $StagingGroup --name $StagingUi --output none
# Rollback, if separately authorized: restore captured compatible image/settings.
az webapp config container set --resource-group $StagingGroup --name $StagingUi --container-image-name $RollbackUiRegistryDigest --output none
az webapp config appsettings set --resource-group $StagingGroup --name $StagingUi --settings "@$ProtectedRollbackSettingsFile" --output none
az webapp restart --resource-group $StagingGroup --name $StagingUi --output none
```

Rollback settings must explicitly restore every overwritten setting, including the chart-mode gate; setting a subset does not delete introduced keys. If a newly introduced setting must be absent, use a separately authorized exact-name appsettings delete with output none after rollback. Review that deletion list against the captured baseline before execution. Do not expose rollback registry namespaces or private settings in public evidence. No registry authentication changes are prepared or assumed.

Research sequence after host assignment and separate authority: verify actual persistent mount and UID1654 boundary on that host; securely initialize authentic input through existing --research-initialize with redirected stdin; close initializer; keep the same authoritative SQLite directory, D5 tuple and witness; start exactly one --research-snapshot-api with strict HTTPS. Configure host-specific bounded stop/verify/start and compatible-image rollback before deployment. Never switch to the rejected App Service HTTP writer path. Frontend TLS termination alone does not establish end-to-end HTTPS. No concrete research deployment command is asserted without the host's service/container/storage identifiers.

### Actual staging acceptance sequence after separate authority

Use only `https://aiqr112wp05wus27f5eabb5.azurewebsites.net` and the explicitly assigned staging research hostname. First verify actual image digest and strict certificate/hostname/auth behavior using bounded requests, then real completed BTC/ETH candles, C# indicators, hidden warmup, gaps and truthful stale state. Verify one writer and identity continuity through ordinary sequential stop/verify/start of the research instance using unchanged storage; never redo general power-loss or distributed-fencing work. In actual Chrome check both instruments, selectors, SMA/RSI, hover/modebar/zoom/pan and reconnect while retaining last good data. Check F1 cold start and bounded1/3/5 sessions if practical; record elapsed times and failures without inferring capacity from plan metadata. Confirm the browser receives neither API keys nor raw/private state and makes no direct provider request. Report every not-run criterion explicitly. No browser, network reachability, TLS-negative, cold-start or session test against Azure was executed in this checkpoint.

PUBLIC_RUNTIME_VERIFIED=False. Azure chart acceptance, production changes and WP closure remain unapproved.

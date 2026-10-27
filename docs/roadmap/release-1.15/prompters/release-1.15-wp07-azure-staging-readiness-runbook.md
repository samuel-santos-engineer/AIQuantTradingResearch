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


## Executable import increment and single staging host proposal - 2026-10-25

Baseline PR #400: `2957c01d03fb48c306bd8d343bdcc424c44878ba`. This increment adds operator automation, not application changes. Existing source already bounds Twelve Data to1000 ascending UTC1min observations per request and rejects invalid source/provenance/volume semantics; ingestion commits through the existing store. Existing production initializer validates the entire private export before opening the store and accepts exact matching retry only. Existing research image/HTTPS client need no code change. No provider polling starts by default.

### Operator-controlled private import

New `eng/docker/release-1.15/Invoke-Wp07ResearchImport.ps1` wraps the existing published initializer on Windows PowerShell5.1. Default Validate reads only bounded private inputs and verifies plain paths/published-worker presence; it does not claim C# semantic validation or start any process. It refuses reparse paths, repository-contained private inputs/state, BOM/malformed UTF-8, config over4096 bytes, export over1MiB, and Azure App Service identity markers. Initialize requires explicit -Execute. No provider, Azure, Docker or registry command is present. Configuration is written as UTF-8 bytes to redirected stdin, never arguments; child diagnostics are suppressed. A35-second deadline kills/waits for the exact owned initializer, with5 seconds for termination verification. No background server, second writer, database reset or automatic retry is started. C# remains the semantic authority, including1..1000 rows per symbol, complete UTC minutes, OHLCV and D5 validation. A preflight pass is not import acceptance or provenance attestation.

Use private operator-owned configuration/export paths outside Git; restrict their Windows ACLs before use. No secrets are retrieved by this wrapper. On Linux the existing initializer additionally rejects group/other permissions. Use unchanged generation/keyEpoch/transformEpoch for exact retries. Never initialize over incompatible or active state. The retained C# storage boundary still rejects App Service. This Windows preparation wrapper does not qualify or deploy the proposed Linux host.

```powershell
# Already supplied private inputs; no literal secrets or candle rows in shell history.
$ImportScript='./eng/docker/release-1.15/Invoke-Wp07ResearchImport.ps1'
& $ImportScript -PublishedResearchDirectory $PublishedResearchDirectory `
  -ImportFile $AuthorizedCompletedObservationFile -ConfigurationFile $ProtectedConfigurationFile `
  -PrivateDirectory $ProtectedPrivateDirectory
# ONLY after approved authentic input and operator execution authority:
& $ImportScript -Action Initialize -Execute -PublishedResearchDirectory $PublishedResearchDirectory `
  -ImportFile $AuthorizedCompletedObservationFile -ConfigurationFile $ProtectedConfigurationFile `
  -PrivateDirectory $ProtectedPrivateDirectory
```

The export is exactly the existing initializer envelope: source=twelvedata, observedAtUtc UTC seconds, BTC/USD and ETH/USD arrays; each row has openTimeUtc and decimal-string open/high/low/close/volume. Preserve gaps and authentic provenance. No fixture conversion or Python indicator calculation. Do not copy a running SQLite database; use the existing closed-state first-seed procedure once, then preserve the authoritative volume unchanged across ordinary stop/start. Host-specific import/transfer commands follow host assignment, not an assumed remote Docker endpoint.

Acquisition alternative pending owner decision A: permit at most two requests total (one BTC/USD, one ETH/USD), outputsize1000, interval1min, UTC ascending, no retries, maximum16 account credits total and only if the account's applicable rate fits that cap. Use existing TwelveDataMinuteCandleSource with completed-minute filtering and confirmed volume semantics; do not bypass its validation. Owner supplies the designated account's key through an approved private process environment/secret channel, never an argument, URL, log or browser. Do not retrieve legacy Azure/provider settings. Export privately into the initializer envelope; if source/account rate or base-volume semantics cannot be established, stop without a request. This is a precise request for authority, not authority to acquire now. An already authorized export avoids acquisition entirely.

### One proposed staging host, not provisioned or Azure-qualified

Propose **one Standard_B1s Linux x64 VM (1vCPU/1GiB), Ubuntu24.04 LTS**, in **West US2 within the existing staging resource group**, with a32GiB StandardSSD_LRS E4 OS disk and a separate32GiB StandardSSD_LRS E4 research data disk. Mount the data disk ext4 at `/srv/aiq-research`, private owner1654, directory0700/files0600; never use the temporary disk or a network share. Run the existing qualified research image as UID1654, one container, private data bind mount, no automatic parallel replacement. B1s is the smallest candidate, not proven adequate: CPU credits, memory, SKU capacity, subscription quota and actual staging behavior remain NOT_RUN. UI stays on existing F1. No VM, disk, VNet, IP, DNS or RBAC change was executed.

Microsoft's public [Retail Prices API](https://prices.azure.com/api/retail/prices) returned these USD list rates on2026-10-25 for armRegionName=westus2, Consumption: Standard_B1s Linux $0.0104/hour; E4 LRS Disk $2.40/month each; E4 LRS Disk Operations $0.002/10K; E4 LRS Disk Mount meter $0.26/month; Standard IPv4 Static Public IP $0.005/hour. At730 hours: VM $7.59 + two disks $4.80 + IP $3.65 = **about$16.04/month**, or **$16.56 including a conservative allowance for both separately listed disk-mount meters if applicable**. Disk operations, outbound transfer, taxes, certificate/domain costs and any chosen backups are additional; no backup service is included. This is an approximate retail budget, not an account quote, zero-cost claim, offer eligibility or provisioning guarantee. Deallocation stops VM compute billing, not retained disk/IP charges. [Managed disk pricing](https://azure.microsoft.com/en-us/pricing/details/managed-disks/) explains account/region variation.

Reproduce the public price lookup without account credentials: GET Retail Prices API with `$filter` equal to `armRegionName eq 'westus2' and armSkuName eq 'Standard_B1s' and priceType eq 'Consumption'`; exclude Windows/Spot/Low Priority. For disks filter Storage and meterName containing E4 LRS; for IP filter Virtual Network and productName containing IP Addresses. All three responses had no next page. Pricing lookup is not an Azure runtime test. Reconfirm rates and applicable meters before purchase.

Network/TLS proposal: one staging-only static Standard IPv4 address with a unique regional Azure DNS label and a current hostname-valid public CA certificate provisioned privately as PFX for the existing C# TLS endpoint. Publish only443 to the research TLS port52731; deny direct52731 and database ports. Operator SSH22 only from an explicitly supplied owner CIDR; certificate issuance/renewal access must be specified before deployment (no assumption of an existing certificate). No load balancer, gateway, HA or reverse-proxy service is proposed. NSG allowlisting should constrain443 to staging UI outbound addresses where practical; API authentication and strict TLS remain mandatory. No claim is made that frontend TLS termination encrypts the backend. UI uses the HTTPS DNS hostname and server-side X-API-Key with system CA trust; browser never receives service credentials. Outbound connectivity, DNS/certificate issuance and image transfer are pending exact deployment authority, not performed here.

Stop/start: stop the old research service/container; verify that exact process/container is stopped and no writer owns the volume; then start exactly one compatible image against the unchanged data mount, witness and D5 tuple. Verify public identity continuity and readiness through the existing staging UI. Rollback stops/verifies the new instance before starting the previous compatible image with the same state and secrets; never restore DB/witness independently or reset epochs. Keep a brief honest outage. No restart/failover manager or new coordination subsystem is proposed. Exact VM/service/image/DNS/CIDR/certificate identifiers remain owner inputs within decision B; do not invent executable provisioning commands until authorized.

### Current evidence and next executable action

| Status | Evidence |
| --- | --- |
| PASS_LOCAL | New wrapper: eight focused offline tests passed, including default-no-process, missing -Execute, size/encoding/BOM/missing input negatives and actual published initializer rejection of invalid config without state creation. No authentic import was executed. |
| VERIFIED_AZURE_STAGING_MANAGEMENT | Fresh exact-group inventory still contains only F1 plan + two apps. UI Running, image assigned, HTTPS-only true,8501, storage false, no custom mounts. Research Running shell, no image/mounts/port settings, HTTPS-only false. Both remain on the named WestUS2 F1 plan. No other environment was scanned. |
| NOT_RUN | Provider acquisition, host provisioning, registry publication, deployment, authentic import, Azure HTTPS/auth/runtime/browser/restart/cold-start/session tests. No paid resource or secret retrieval. |

**Next executable owner step:** choose A (supply private completed-minute export, or approve the exact bounded acquisition above) and B (designate an already-owned persistent host explicitly serving existing staging, or approve this WestUS2 single-instance proposal/budget for a separately scoped provisioning sequence). All subsequent runtime qualification targets only the existing staging URL and that explicitly assigned host. Existing staged UI config/rollback command templates above remain applicable after host/digest/secret inputs are resolved. Stop before unapproved calls or provisioning. PUBLIC_RUNTIME_VERIFIED=False.


## Authentic chart assignment: current operational disposition - 2026-10-25

Baseline merged PR #401 `f19d63647efcda5fe734363d87c430ffe8208ae2`. **The prior VM/disk/IP proposal and its budget are withdrawn.** Earlier proposal text is historical, not an approved dependency. No VM, disk, IP, network, paid service, governance change or storage-policy change is part of this increment. Persistent local/block hosting work is deferred. The existing App Service writer safeguard remains unchanged; no marker stripping, fixture authority, ephemeral/network SQLite workaround or second writer is permitted.

### Acquisition result: no requests sent

Bounded acquisition authority permits one request per symbol,1000 rows maximum each, at most16 account credits total, no retries. Actual execution: **0 requests,0 credits consumed**. Presence-only checks found no TwelveData__ApiKey, TWELVE_DATA_API_KEY or AIQ_TWELVE_DATA_API_KEY in the current process environment. No alternative approved private credential channel or export was supplied. No Azure provider-key setting, user secret store, unrelated app setting or private credential file was retrieved. No provider request was used to discover account information.

The account-specific credit cost and base-asset-volume semantics are also unestablished. Existing source requests UTC ascending1min outputsize1000 and requires baseAssetVolumeConfirmed; that boolean does not prove the account's volume units. ResearchIngestionService's configured credit counter is not evidence of provider-account pricing. Needed input is the designated private credential channel (or authorized export), account confirmation of actual credits per request for these two requests within the16-credit cap, and confirmation of base-asset volume semantics. This is the existing source's technical precondition, not a new licensing gate. Do not call the provider until those facts are supplied without an extra discovery request. Once available, use the unchanged source/initializer and merged Invoke-Wp07ResearchImport.ps1; preserve UTC completion, provenance and gaps. Private rows/config/state remain outside Git.

### Executable staging-only read-only preflight

New `eng/docker/release-1.15/Test-Wp07StagingChartReadiness.ps1` targets only the named existing staging apps and fixed UI routes. Default invocation makes no network/process calls and reports NOT_RUN. -InspectAzure uses the installed Azure CLI with30-second child deadlines and safe management projections; settings **names only**, no values, images, CLI diagnostics or account identifiers are printed. -ProbeUi makes one bounded nonredirecting HTTPS GET each to the existing staging root and Streamlit health route with standard certificate validation, no proxy inheritance,10-second network/read deadlines and64KiB body limit; bodies are discarded. It never launches a browser, submits credentials or calls research/provider APIs. No mutation action exists.

```powershell
# Windows PowerShell5.1; default does nothing operationally.
& ./eng/docker/release-1.15/Test-Wp07StagingChartReadiness.ps1
# Explicit read-only checks against the designated staging apps only:
& ./eng/docker/release-1.15/Test-Wp07StagingChartReadiness.ps1 -InspectAzure -ProbeUi
```

Exit0 means the requested observations were reported, not that chart readiness passed. Examine uiConfigurationPresent, transport status and chartAcceptance. Setting-name presence cannot prove correct values, TLS, authentication or a compatible research endpoint. The script always retains appServiceWriterSupported=false, compatibleResearchEndpointVerified=false, chartAcceptance=NOT_RUN and PUBLIC_RUNTIME_VERIFIED=false. It deliberately cannot award browser acceptance. Its pure assessment tests use metadata fixtures only; they are not substitute Azure tests.

| Status | Actual result |
| --- | --- |
| PASS_LOCAL | Eight focused offline tests, exit0, final2.781s; default no-network behavior, missing image/HTTPS/settings, stopped UI, rejection of an unapproved route, PowerShell5.1 parsing and no false acceptance from configuration. No fault matrix, builds, package installs or broad test suite. |
| VERIFIED_AZURE_STAGING_MANAGEMENT | Exact group still contains only the existing F1 plan and two apps. UI Running, HTTPS-only, image assigned, no research settings/custom mounts. Research Running shell, no image, no research settings/mounts, HTTPS-only false. New preflight independently reports3 missing required UI research settings and uiConfigurationPresent=false. No unrelated resource search. |
| VERIFIED_AZURE_STAGING_HTTP | First bounded root/health probes returned UNAVAILABLE without an HTTP status; cause was not established and is not called an application outage. After adding safe transport categories, one bounded follow-up observed200 on both routes, without deployment/restart. Four total public GET attempts. This proves HTTP observations only, not authentic data, indicators, cold start, capacity or browser behavior. |
| BLOCKED_INPUT | No available authorized provider credential/export, account-specific credit-rate/volume evidence or designated compatible research endpoint. Process-environment research URL/key also absent. No private file search. |
| NOT_RUN | Authentic acquisition/import; image publication; Azure mutation/deployment; research TLS/auth negatives; SQLite restart continuity in Azure; real chart/browser/indicator/stale/reconnect/cold-start/session acceptance. |

### Exact remaining decision, not another hosting proposal

The unchanged ResearchStorageBoundary rejects App Service before SQLite writer initialization. Both designated Azure apps are App Service. No compatible research endpoint was configured, supplied or established within the authorized scope; none is assumed elsewhere. The existing F1 UI can serve its current legacy application and could host the already implemented Release1.15 client after authorization, but that client cannot display authentic research snapshots without a compatible endpoint. A UI-only config/image update cannot remove this blocker. Local accepted fixture evidence does not change it.

The smallest governance-preserving next option is **owner designation of an already-owned, reachable, compatible research snapshot endpoint explicitly serving this staging UI**, retaining existing SQLite/D5, single-writer and strict HTTPS/API-key semantics; do not provision anything or search other environments. If no such endpoint can be designated while hosting is deferred, **defer actual staging chart delivery**. Enabling the F1 writer would require changing a boundary explicitly prohibited by this assignment, so it is not offered as an implementation trick. No mutation approval alone can fix absence of the endpoint.

Once a compatible endpoint and authentic input are supplied, request one narrowly scoped staging action approval for the existing UI's immutable image/config and subsequent acceptance. Earlier exact-target UI commands remain templates only: `az webapp config container set`, `az webapp config appsettings set` using a protected external settings JSON, and `az webapp restart`, all restricted to `rg-aiq-r112-wp05-wus2-7f5eabb5` / `aiqr112wp05wus27f5eabb5` and reviewed exact digest/hostname/settings. No actual digest or endpoint is invented. Do not execute until explicitly authorized; preserve rollback settings privately and never expose secrets in argv/logs. Do not deploy research into the F1 shell.

Acceptance after that authority targets only `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/` and the expressly assigned research endpoint: actual completed BTC/ETH candles, C# indicators/SMA/RSI, retained gaps/hidden warmup, selectors/hover/zoom/pan, truthful stale/reconnect, hostname/certificate/API-key negatives, no browser/provider secrets, unchanged SQLite/D5 through controlled stop/verify/start, and accurately measured cold-start/session results. None was inferred from HTTP200 or reused local fixtures.

Stop here pending two concrete inputs: (A) secure authentic-data/account prerequisites above; (B) designate the compatible existing endpoint, or explicitly defer staging integration. No withdrawn budget proposal or new infrastructure approval gate. PUBLIC_RUNTIME_VERIFIED=False.


## WP09 implementation: exact F1 non-durable exception (2026-10-25)

This section supersedes the prior assumption that any App Service writer must be rejected: default rejection remains, but the owner now authorizes one exact-site ephemeral exception. No VM, paid resource, durable-volume acceptance, image publication or Azure mutation is authorized in this PR. See the existing engineering contract's narrow WP09 amendment. All new/modified text is UTF-8/LF.

Code requires BOTH scope=`azure-f1-staging-ephemeral` and allow flag=`1`, exact research WEBSITE_SITE_NAME and WEBSITE_HOSTNAME, nonempty WEBSITE_INSTANCE_ID, WEBSITE_ROLE_INSTANCE_ID=`0`, WEBSITES_ENABLE_APP_SERVICE_STORAGE=`false`, AIQ_RESEARCH_EXPECTED_MOUNT_PATH=`/`, Linux UID1654 and fixed AIQ_RESEARCH_PRIVATE_DIRECTORY=`/tmp/aiq-research`. Only the writable container-root overlay (root/mount `/`, major0, no shared/master propagation or nested state-file mount) passes. `/home`, Azure Files/CIFS/NFS, ext4/XFS mounted data volumes, tmpfs, bind/subdirectory roots, symlinks, group/other permissions, wrong UID or unverifiable kernel evidence reject. A private0700 directory and real create/write/delete probe are required before store ownership/SQLite open. Files remain0600 through existing ownership logic. Neither image markers nor local-qualification can substitute for the exception.

The path is a **conditional implementation candidate**, verified locally only. Microsoft's [custom container documentation](https://learn.microsoft.com/azure/app-service/configure-custom-container) describes writes outside home/storage mounts as nonpersistent across restart; it does not prove our named F1 app's mount type. Actual mountinfo/UID/permissions in that app are NOT_RUN and must be checked by the implementation itself after later deployment. Unknown actual layouts reject. Mountinfo cannot prove backing-disk lifetime, detect a dishonest operator or eliminate TOCTOU against a privileged local adversary. No such co-tenant/capabilities are permitted; no durability or global writer-fence claim follows from these checks.

### Reproduce local evidence without Azure actions

```powershell
# Offline restore from the existing package cache, then focused tests:
dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source "$env:USERPROFILE/.nuget/packages" -p:NuGetAudit=false --ignore-failed-sources
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter "FullyQualifiedName~ResearchStorageBoundaryTests|FullyQualifiedName~ResearchSnapshotEndpointTests|FullyQualifiedName~ResearchStateInitializationTests"
# Publish outside Git, no restore/network. The container helper is never packaged.
dotnet publish src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --no-restore -c Release -p:UseAppHost=false -o $OwnedPublishDirectory
& ./eng/docker/release-1.15/Invoke-Wp09EphemeralQualification.ps1 -ResearchImage $ExistingImmutableResearchImage -PublishedResearchDirectory $OwnedPublishDirectory
# Explicit local execution only: existing Docker Desktop named pipe, cached Linux image,
# no network, owned containers, test-only publish copy, offline compiler, bounded deadlines.
& ./eng/docker/release-1.15/Invoke-Wp09EphemeralQualification.ps1 -ResearchImage $ExistingImmutableResearchImage -PublishedResearchDirectory $OwnedPublishDirectory -Execute
```

The Linux helper is compiled as a friend-test assembly and bind-mounted with a disposable copy of the changed production publish. It simulates Azure marker strings; it is **not Azure identity/qualification**. Positive case creates an empty offline test store/D5 (no candles), confirms exclusion of a second writer and reopens the same container-local state; no fixture is imported or represented as real. Negatives include default denial, wrong site, missing instance, wrong UID, read-only root, tmpfs, real symlinks/broad modes and missing/malformed database startup rejection. Every exact labeled test container is removed; no volume, registry layer or Azure resource is created. The initial harness failed with Docker exit125 due to nested read-only bind setup; using an owned test-only publish copy corrected the harness, without weakening the boundary.

### Later exact staging deployment sequence - NOT AUTHORIZED / NOT EXECUTED

First merge/review this implementation under separate lifecycle approval. Build and qualify the updated **existing research-api Dockerfile** from a clean production publish, excluding the friend-test helper. Do not reuse the old cached image as a changed release image. Publish only under separate authority, capture real immutable registry manifest digests privately, and verify image UID1654 / UI UID12002. No digest is fabricated here.

Prepare protected external JSON settings files. Research settings include the scope/flag/disabled-storage/fixed-directory/expected-mount above, WEBSITES_PORT=52731, AIQ_RESEARCH_TRANSPORT=azure-app-service-http, AIQ_RESEARCH_PLATFORM_HTTPS_ONLY=1, AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0 and an independently supplied API key. Do **not** set WEBSITE_* identity variables: require genuine platform values. UI settings include WEBSITES_PORT=8501, AIQ_RESEARCH_CHART=1, AIQ_RESEARCH_BASE_URL=`https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net`, and the same service key; system CA trust, no provider/HMAC/private files in UI. The existing C# App Service transport relies on frontend HTTPS terminating into platform HTTP; it is not end-to-end TLS. Azure HTTPS-only must actually be enabled, not merely asserted in an environment variable.

These PowerShell5.1 commands target only the existing staging resources. They are templates requiring real qualified digest and private input files plus explicit mutation authority; none ran here:

```powershell
$Group='rg-aiq-r112-wp05-wus2-7f5eabb5'
$Research='aiqr115researchstagingwus27f5eabb5'
$Ui='aiqr112wp05wus27f5eabb5'
az webapp stop -g $Group -n $Ui --output none
if($LASTEXITCODE -ne 0){throw 'Staging operation failed; stop for review'}
az webapp stop -g $Group -n $Research --output none
if($LASTEXITCODE -ne 0){throw 'Staging operation failed; stop for review'}
foreach($App in @($Ui,$Research)) {
    $Stopped=$false
    for($Attempt=0;$Attempt -lt 15;$Attempt++) {
        $State=az webapp show -g $Group -n $App --query state -o tsv
        if($LASTEXITCODE -ne 0){throw 'Stop verification failed'}
        if($State -eq 'Stopped'){$Stopped=$true;break}
        Start-Sleep -Seconds 2
    }
    if(-not $Stopped){throw 'Stop not verified; do not start another writer'}
}
az webapp update -g $Group -n $Research --https-only true --output none
if($LASTEXITCODE -ne 0){throw 'Staging operation failed; stop for review'}
az webapp config container set -g $Group -n $Research --container-image-name $QualifiedResearchRegistryDigest --output none
if($LASTEXITCODE -ne 0){throw 'Staging operation failed; stop for review'}
az webapp config appsettings set -g $Group -n $Research --settings "@$ProtectedResearchSettingsFile" --output none
if($LASTEXITCODE -ne 0){throw 'Staging operation failed; stop for review'}
az webapp config container set -g $Group -n $Ui --container-image-name $QualifiedUiRegistryDigest --output none
if($LASTEXITCODE -ne 0){throw 'Staging operation failed; stop for review'}
az webapp config appsettings set -g $Group -n $Ui --settings "@$ProtectedUiSettingsFile" --output none
if($LASTEXITCODE -ne 0){throw 'Staging operation failed; stop for review'}
# Do NOT start until the controlled private bootstrap delivery described below is approved.
# After that input and exact bootstrap/startup mechanism have been reviewed:
az webapp start -g $Group -n $Research --output none
if($LASTEXITCODE -ne 0){throw 'Staging operation failed; stop for review'}
# Require authenticated valid snapshots for both symbols before starting UI:
az webapp start -g $Group -n $Ui --output none
if($LASTEXITCODE -ne 0){throw 'Staging operation failed; stop for review'}
```

Operational commands must stop on any failed Azure CLI exit; do not continue a partial configuration. Capture previous image/config/HTTPS state privately before mutations. No settings output, credentials, registry namespaces or row data in evidence. App stop status alone is management evidence; before new research startup verify the old application instance/writer is gone using the later authorized application-container diagnostic path. No deployment slots, overlap or scale-out; role0 is not a distributed lock.

**Controlled bootstrap is a remaining operational input, not implemented auto-seeding.** Existing ENTRYPOINT starts snapshot service only and refuses missing/invalid DB/D5. It cannot initialize a fresh ephemeral instance by itself. In an expressly authorized application-container operator session, deliver authentic export plus protected initializer configuration privately outside Git; run `dotnet /app/AIQuantTradingResearch.Worker.dll --research-initialize` with redirected stdin and AIQ_RESEARCH_IMPORT_FILE pointing to the protected export, AIQ_RESEARCH_PRIVATE_DIRECTORY fixed as above and genuine platform markers intact. Then close initializer and `exec dotnet /app/AIQuantTradingResearch.Worker.dll --research-snapshot-api` in the **same container instance**. Existing --research-initialize and wrapper remain unchanged; Windows wrapper is not an Azure remote-console mechanism. No arbitrary public bootstrap/admin route is added. Secure delivery/session/startup sequencing must be authorized and established before a deployment can actually serve; no unverified SCM-to-application filesystem equivalence is assumed. If such a session/delivery mechanism is unavailable on the actual app, report BLOCKED rather than inventing credentials/fixtures or claiming deployment executable end-to-end.

Restart/replacement may discard DB, WAL, witness, D5 and input files. The service must then remain unavailable until controlled reimport is possible. Keep authorized original export/config privately with the operator for reinitialization; do not fabricate observations, independently restore witness/DB, rotate epochs silently or start a second authority. On ordinary same-instance reopen with retained files, preserve the existing tuple/identity. On total loss, original tuple/import reuse follows existing initializer validation and does not claim preserved revision history or durable continuity.

Rollback: stop UI/research, verify old process stopped; restore previously captured **compatible** image/config/HTTPS settings with the same exact commands, then start only after valid private state or controlled authentic reinitialization exists. If rolling back to the former research shell, leave it stopped and restore the previous legacy UI image/config; do not imply that the shell is a research backend. Remove only specifically introduced opt-in settings absent from the private baseline through an explicitly reviewed `az webapp config appsettings delete --setting-names ... --output none` list. Never enable the exception for any other app.

Two apps still share the existing Free plan. Idle unload/cold start, image pull/startup time, competing CPU/memory quotas and loss of ephemeral state can make availability intermittent; no AlwaysOn, SLA or spare capacity is assumed. See [App Service configuration](https://learn.microsoft.com/azure/app-service/configure-common) for idle/AlwaysOn behavior. This PR did not restart, load-test or measure those apps. Later authorized testing must observe cold startup, actual resource/availability behavior and bounded1/3/5 sessions, recording failures; HTTP200 alone is never browser acceptance.

PASS_LOCAL: focused .NET tests and11 local Linux cases,8 existing diagnostics regressions. VERIFIED_AZURE_STAGING: none newly performed in WP09. BLOCKED_INPUT: designated secure real-data channel/export, account-specific credits and base-volume confirmation, and practical private bootstrap delivery for the existing app. Acquisition stays0 requests/0 credits; no credential retrieval. NOT_RUN: Azure filesystem verification, image publication/config/start/restart, import of real observations, public research TLS/auth and browser acceptance for BTC/ETH/indicators/gaps/stale/reconnect. PUBLIC_RUNTIME_VERIFIED=False. No paid resources, merge or WP closure.


## WP10 same-container bootstrap and diagnostic image increment (2026-10-25)

Baseline PR403 squash `894b075e65863c042e9c39167fdbd71612020c90`. No C# contract/initializer/endpoint, WP09 boundary, governance, provider or schema change. The research image now enters `research-bootstrap.sh`; serve remains default. All new/modified text is UTF-8/LF. The old Linux qualification runner copies the new shell into its allowlisted build context, preserving reproducibility.

Modes:
- `serve`: exec the existing snapshot endpoint. Missing/lost/corrupt state fails unchanged; no fetch or automatic initialization.
- `operator-stdin`: require an already privately delivered readable export via AIQ_RESEARCH_IMPORT_FILE, send bounded initializer configuration through stdin, run the unchanged initializer to completion, then unset bootstrap/import path variables and exec the endpoint in the **same container**. Initializer exit releases the single-writer lock before the server opens it. Invalid/missing input returns REIMPORT_REQUIRED and never starts the server. No background writer, public import route, shell tracing or secret arguments.
- `diagnose`: read-only one-shot UID fields, selected root/tmp mount metadata (no host backing paths), private-directory mode/existence and known cgroup limits. No mkdir, write probe, initializer, database access, server or export handling. It is not a guard-pass verdict. Local diagnostic was tested with read-only rootfs. Default serve is not silently replaced by diagnostics.

### Actual access findings and private-channel limit

Fresh read-only management queries against the exact research staging app show Running Linux shell, no assigned image, no startup command, AlwaysOn false and one listed instance. This is not its future research container's UID/mount proof. No runtime command was executed in Azure.

Microsoft now documents a newer Linux app-container terminal/`az webapp exec` channel without an image SSH daemon; it requires Azure CLI2.90 or later, SCM access and a running app container. Its detached mode returns acceptance without remote stdout/exit, and it has no file-transfer feature. See [official app-container access documentation](https://learn.microsoft.com/en-us/azure/app-service/configure-linux-open-ssh-session). App and Kudu remain different environments. Our installed CLI does not recognize `webapp exec`; local help exited1. No CLI/extension upgrade, SSH daemon/password, RBAC/network change or publishing credential retrieval was performed. Initial sandbox help hit Azure-profile permissions; escalated help confirmed the unsupported command. A supported owner portal terminal/new CLI may be investigated later against the designated app; neither access nor private stdin/file transfer was demonstrated here. Do not claim the channel exists merely from documentation.

Thus **actual Azure bootstrap delivery is still BLOCKED**. Local Docker stdin plus daemon-mediated private file copy demonstrated the wrapper sequencing, not an App Service transport. No direct Azure Docker-daemon access, SCM/application file equivalence, input mount, stdin attachment to PID1 or endpoint for import is assumed. An authenticated HTTP import surface would require separate design review/authority and is not added. If no supported private operator path is feasible, stop at that design boundary; this PR does not claim to finish Azure bootstrap.

### Reproduce the bounded local image/bootstrap checks

```powershell
# Publish only clean approved source, using cached packages and no provider credentials.
dotnet restore src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj --source "$env:USERPROFILE/.nuget/packages" -p:NuGetAudit=false --ignore-failed-sources
dotnet publish src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj -c Release --no-restore -p:UseAppHost=false -o $OwnedPublishDirectory
& ./eng/docker/release-1.15/Invoke-Wp10BootstrapQualification.ps1 -PublishedResearchDirectory $OwnedPublishDirectory
& ./eng/docker/release-1.15/Invoke-Wp10BootstrapQualification.ps1 -PublishedResearchDirectory $OwnedPublishDirectory -Execute
```

Default is read-only cached-input preflight. -Execute builds the existing two Dockerfiles from pinned cached ASP.NET/Python bases and the accepted local dependency image; no pulls, installs, provider requests or registry publication. Context is an allowlisted public source/publish copy; fake configuration/export are generated **after builds**, outside the context. No keys/exports in image layers/Git. Both images retain expected nonroot identities1654/12002. Context, fake input and environment file are removed only from the exact owned temporary root; exact labeled containers are removed. Only local candidate images remain. No shell/app/provider source is substituted.

Eleven actual local cases passed: read-only diagnostic; unknown mode refusal; missing private export; missing state with configured private path; invalid stdin never starts server; initializer then endpoint in same container; missing API key denied; BTC and ETH full C# snapshots from **offline test fixtures**; no service/HMAC secrets in diagnostics; UI image health without network/credentials. The fixture uses401 constant candles per symbol and is never represented as authentic acquisition. Existing C# calculations/integrity execute; no Python indicators. Local HTTP is the existing explicitly selected platform-HTTP transport with simulated Azure metadata, not Azure frontend TLS or browser acceptance. Loopback-only host publication and bounded memory/pids/timeouts. Initial nine cases passed; added invalid-input/read-only checks yielded ten cases, all passed. A final run added isolated UI-image health, yielding11/11.

Latest tested local IDs (not pullable registry manifest digests):
- Research: `sha256:b1a70c7f3e9be8ca9db178ae3d8a0b2a9663c8d20f20ccd932569c1b7596187b`
- UI: `sha256:d7eece4918fba6897aaf6570f080fd198d8d6c33e2b09dc890f3d2c7b978e639`

### Narrow next mutation request: research diagnostic only

Request owner approval to publish **only that research candidate** into the already approved registry destination, read back its real manifest digest, temporarily configure only `rg-aiq-r112-wp05-wus2-7f5eabb5` / `aiqr115researchstagingwus27f5eabb5` to that immutable digest with AIQ_RESEARCH_BOOTSTRAP_MODE=diagnose, start briefly, collect only allowlisted diagnostic output, then stop and restore its prior shell/configuration. UI `aiqr112wp05wus27f5eabb5` stays unchanged. No provider/data import, new resource, container SSH installation, readiness promotion or production action. No app identity markers are set. This is a request, not executed authority.

After approval, privately capture the baseline image/startup/settings/state; never print full settings or registry namespace. Pin the actual registry digest after publication; a local config ID cannot substitute. Templates target only the research staging app:

```powershell
$Group='rg-aiq-r112-wp05-wus2-7f5eabb5'
$Research='aiqr115researchstagingwus27f5eabb5'
# $ReviewedRegistryTag is the existing approved registry destination, privately supplied.
# $LocalResearchImage is exactly the tested ID above; no UI image publication here.
docker tag $LocalResearchImage $ReviewedRegistryTag
if($LASTEXITCODE -ne 0){throw 'Tag failed'}
docker push $ReviewedRegistryTag
if($LASTEXITCODE -ne 0){throw 'Publication failed'}
# Read back registry manifest, bind its actual digest to the local candidate and record privately.
# STOP unless $QualifiedResearchRegistryDigest is the verified immutable registry reference.
az webapp stop -g $Group -n $Research --output none
if($LASTEXITCODE -ne 0){throw 'Stop failed'}
$State=az webapp show -g $Group -n $Research --query state -o tsv
if($LASTEXITCODE -ne 0 -or $State -ne 'Stopped'){throw 'Stop unverified'}
az webapp config container set -g $Group -n $Research --container-image-name $QualifiedResearchRegistryDigest --output none
if($LASTEXITCODE -ne 0){throw 'Image config failed'}
az webapp config appsettings set -g $Group -n $Research --settings AIQ_RESEARCH_BOOTSTRAP_MODE=diagnose --output none
if($LASTEXITCODE -ne 0){throw 'Diagnostic config failed'}
az webapp start -g $Group -n $Research --output none
if($LASTEXITCODE -ne 0){throw 'Start failed'}
# Bound observation window to <=60s; use approved application log capture, sanitize platform lines.
# Diagnostic exits without a server: no health/startup-success claim, and retries are not imports.
az webapp stop -g $Group -n $Research --output none
if($LASTEXITCODE -ne 0){throw 'Diagnostic stop failed'}
# Verify stopped, restore privately captured baseline image/startup/settings exactly.
# If mode was absent, delete only AIQ_RESEARCH_BOOTSTRAP_MODE with output none.
# Prior research shell had no image; do not invent a rollback image or enable a research server.
```

The prior shell has no digest, so rollback cannot be an invented previous image. Prepare the exact baseline linuxFxVersion/appCommandLine restoration from the privately captured config (App Service config set with protected generic-configurations JSON), remove only newly introduced mode, verify config equality, and restore its prior Running/Stopped shell state after diagnostic stop. Registry rollback/temporary-tag retention also needs the approved publication disposition; do not delete another image/version. Exact rollback application commands after verifying diagnostic stopped (protected config contains only captured linuxFxVersion/appCommandLine; protected mode settings contain only the prior bootstrap mode if it existed):

```powershell
az webapp config set -g $Group -n $Research --generic-configurations "@$ProtectedBaselineContainerConfigFile" --output none
if($LASTEXITCODE -ne 0){throw 'Baseline config restoration failed'}
if($BaselineModeWasPresent) {
    az webapp config appsettings set -g $Group -n $Research --settings "@$ProtectedBaselineModeFile" --output none
} else {
    az webapp config appsettings delete -g $Group -n $Research --setting-names AIQ_RESEARCH_BOOTSTRAP_MODE --output none
}
if($LASTEXITCODE -ne 0){throw 'Mode restoration failed'}
# Verify image/startup/mode equality to baseline privately, then restore initial state:
if($BaselineState -eq 'Running') {
    az webapp start -g $Group -n $Research --output none
    if($LASTEXITCODE -ne 0){throw 'Original shell state restoration failed'}
}
```

No publication credentials are obtained by these templates. Missing registry access, baseline rollback or bounded safe log capture stops execution before mutation. No fallback deployment or repeated failure loop is authorized.

The requested diagnostic is only phase1. It cannot prove writable-state initialization, ingress TLS or secure bootstrap. Evaluate its actual UID/overlay/tmp/cgroup evidence against unchanged WP09; mismatch requires a separate evidence-backed change, not broadening guards here. Only after real input and private channel are established request exact research/UI rollout and actual Chrome acceptance. Prior target-specific two-app commands remain templates, not executed steps. The UI's HTTPS hostname/API-key setup remains server-side; no browser credential exposure. Shared F1 capacity/idle/cold-start remains to measure, never solved by an HTTP200 probe.

### Current result

PASS_LOCAL: both images built, eleven local bootstrap cases; no actual private Azure channel proof. VERIFIED_AZURE_STAGING: management metadata only, no container/runtime/browser claim. BLOCKED_INPUT: absent approved private provider credential/export and account-specific credit/base-volume evidence; private Azure import channel not demonstrated. Acquisition0 requests/0 credits. NOT_RUN: registry publication, Azure mutations/restarts/import, actual research filesystem/TLS/API/browser acceptance, cold-start/session measurements. PUBLIC_RUNTIME_VERIFIED=False. One focused review PR, OPEN/UNMERGED; no production/paid infrastructure or WP closure.


## WP11 executed diagnostic: INCOMPLETE, exact rollback verified (2026-10-25)

Owner approval on PR404 authorized research-only publication, one <=60s diagnostic and mandatory baseline restoration. Baseline PR404 squash `c300087aa250d86de24a64ed4bfe0805d3716c9d` was verified. The UI app was never configured, restarted or published. No provider request/credit, observation export/import/bootstrap, production or paid-resource operation occurred.

### Outcome and timing

**Compatibility is NOT ESTABLISHED.** Neither accepted nor rejected WP09 runtime compatibility can be claimed: the collected archives and one bounded retained-logstream check returned no attributable candidate UID/mount/guard payload. The single existing startup-failure indicator was already present in the baseline archive; it is not attributed to this attempt. No guard was broadened and no second candidate run was made to regenerate evidence. A/B acceptance remains unmet; C (no mutation) would be inaccurate because publication and research-app mutations did occur.

The app was restored, independently verified: original empty linuxFxVersion/startup, all app settings, logging configuration, original shell Running state, kind, plan and HTTPS/public-access properties. Configurable values compare exactly; only Azure-generated systemData/ETag audit metadata is excluded because it cannot be restored. Protected raw before/after captures remain private. **No unresolved rollback remains.**

| Event | UTC evidence |
| --- | --- |
| Local candidate start request | 2026-10-25T23:44:37.522609Z |
| Azure start succeeded | 2026-10-25T23:44:39.4309906Z |
| First Azure candidate stop succeeded | 2026-10-25T23:45:03.555938Z |
| Independent watchdog stop succeeded | 2026-10-25T23:45:08.4815716Z |
| Recovery stop succeeded | 2026-10-25T23:45:43.0526432Z |
| Independent full rollback verification | 2026-10-25T23:47:38.711405Z |

Azure start-success to first stop-success spans **24.125s** on the same server clock. Local request to first Azure stop completion was26.033s. These are control-plane timing observations, not kernel process-lifetime telemetry. Later recovery operations did not constitute another candidate run. The initial controller failed short stop confirmation and returned criticalRollbackUnconfirmed before applying restoration; the independent stop watchdog had also requested stop. The agent continued mandatory recovery, verified Stopped, restored recorded config/logging/settings, returned the original shell to Running, then independently verified all baseline fields. The initial failure report is retained privately rather than overwritten by success.

### Artifact identity and preflight

One research artifact was uploaded once to the existing approved registry package; no UI image or existing tag was overwritten. Verified anonymous readback and config identity; candidate retained for review, not permanently deployed.
- Published Linux/amd64 leaf digest used for the attempt: `sha256:5b48f5d703acd7dca7e722ab9bc0028cbe8b80a3526c7a60800699785f6abed3`.
- Qualified local/published OCI index: `sha256:a09670d76db34c54fdcc4978c8156a57b493d4d8a43a87ef591ad3cc5e6d9858`.
- Independently matched config: `sha256:d116ba82b37b4bfbff322e9cab98e4fc2ca7dec37d42b075c3a40ba5818a3a7c`.

Initial readback compared index ID to config digest and blocked before any Azure mutation. Docker's containerd store exposes an OCI index as Image.Id here. Independent local image-save config/rootfs/UID verification plus exact published/local index equality proved identity; negative tests reject different index/config/rootfs. Publication resume skipped the completed push. No expected digest was changed to conceal mismatch.

Before mutation: recorded approval, exact-app metadata/recent activity/protected log archive, no prior diagnostic marker, empty shell/zero settings, scoped wildcard write rights, known existing registry tag access and anonymous pull verified. A latest-tag lookup was absent, not an auth denial; the actual existing tag succeeded. Rollback state was captured privately with verified user/System-only DACL. Initial ACL identity translation/owner-audit privilege errors were corrected by DACL-only application and verified before publication or Azure changes. Baseline registry references, identities, settings, credentials, evidence roots and raw logs/payloads are not public.

### Narrow implementation and safety fixes

The merged WP10 shell only reported metadata, so the required actual guard answer needed a diagnostic-only CLI call. `--research-storage-probe` now returns fixed booleans from the unchanged real host/kernel/private-write guard, creates/removes only a new empty probe directory when needed, and never opens SQLite, initializes D5, imports or starts a service/provider. Infrastructure exposes only this bounded probe; validation methods/defaults remain unchanged. Shell metadata continues to omit backing paths, sources and environment values. Local denied diagnostic remains read-only; a complete explicit staging opt-in permits the zero-data write probe only.

New fixed-target PS5.1/Python controller has nonmutating default, protected-state checks, immutable/anonymous publication verification, no repeat push, existing-execution refusal, a separate30s stop watchdog, and finally-style restoration of image/startup/settings/logging/state. Temporary filesystem container logging is restored; no logging service or storage resource was created. After the live stop-confirmation failure, bounded retry handling was added so transient metadata errors do not abandon rollback, and stop acknowledgment versus later confirmation timing is distinguished. These corrected controller behaviors are **locally tested, not rerun in Azure**. Exact baseline and runtime-output recovery are still mandatory; no accepted CLI submission or HTTP200 substitutes for proof.

```powershell
# Offline tests; no cloud requests.
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter FullyQualifiedName~ResearchStorageBoundaryTests
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp11_research_diagnostic.py -v
# Nonmutating preflight only. Private state must have user/System-only DACL.
& ./eng/azure-cli/r1.15-qualification/Invoke-Wp11ResearchDiagnostic.ps1 -ProtectedStateDirectory $ProtectedStateDirectory
# The already-executed state is intentionally refused, including publication: do not rerun.
```

PASS_LOCAL:50 focused .NET tests,13 Linux smoke cases including admitted/denied probe and empty-directory cleanup,14 rollback/provenance/log-sanitization fault tests; PS5.1 syntax, UTF-8/LF and diff checks. A local probe smoke initially expected REJECTED for a successful PROBE_PASS marker; corrected the assertion only, then13 passed. No candle fixture/import/bootstrap or general crash/fault matrix was run in WP11.

VERIFIED_AZURE_STAGING: only the actual publication/configuration/start/stop/rollback and protected log-retrieval observations above. Runtime UID, mounts, write eligibility and guard acceptance remain **UNVERIFIED**. No native/runtime/browser readiness inference. NOT_RUN: authentic data, service deployment, TLS/auth/browser/session acceptance. PUBLIC_RUNTIME_VERIFIED=False. Review the focused code/failure-evidence PR; any follow-up diagnostic requires a new narrow disposition and must first establish attributable application-output capture. Do not repeat the completed mutation, weaken WP09 or request paid infrastructure.


### WP11 attributable-evidence amendment ? offline qualification only

The original 24.125-second Azure control-plane window remains inconclusive: no
attributable runtime frame was recovered. Delayed or empty platform log delivery,
a container startup failure, and an early exit are hypotheses, not established
causes. Control-plane start success and downloaded historical markers do not prove
that the candidate entrypoint executed. Exact baseline rollback was independently
verified after manual recovery; that history is unchanged.

The diagnostic entrypoint now emits one bounded JSON frame containing version 1,
a unique 128-bit attempt nonce, immutable candidate leaf digest, UTC observation
second, the fixed `research-bootstrap/diagnose` entrypoint identifier, four numeric
UID fields, root overlay/writability booleans, and the six fixed storage-probe
booleans. Missing or malformed correlation is rejected before probing. No mount
sources, environment values, database, API, imports or provider calls are emitted
or initiated. The existing production storage guard and exact-site policy remain
unchanged. Historical archive reduction is retained solely for inspection; the
controller no longer accepts archive markers as runtime evidence.

The validator requires strict UTF-8, at most 4,096 bytes, exact fields/types, no
duplicate keys, matching nonce/digest, consistent guard/UID/mount conclusions,
and `attempt start <= observation <= receipt` within 60 seconds. Receipt requires
an independently authenticated private channel bound to the actual candidate
execution and registry-qualified immutable leaf. The payload's digest is only
self-reported correlation, not attestation. Clock skew or delayed receipt fails
closed; a nonce must never be reused. No currently verified F1 private retrieval
channel supplies this binding. Therefore an unconditional preflight blocks
publication and Azure execution; no setting or operator claim can unlock it.
There is no new public diagnostic endpoint or persistent evidence artifact.

Offline verification:

- `python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp11_research_diagnostic.py -v`:
  **24 passed**, exit 0. All 14 prior tests remain. New cases cover malformed,
  missing, partial, oversized, replayed, mismatched, future/delayed and inconsistent
  evidence; duplicate/private fields; preflight blocking; early exit without a
  frame; startup failure after watchdog arming; watchdog stop success/failure.
  Existing stop-confirmation and exact rollback failure tests remain passing.
- Two actual Linux executions used only the cached research image, a read-only
  bind of the amended shell, UID 1654, read-only root, dropped capabilities,
  no-new-privileges and no network. Missing correlation: rejected, exit 1, empty
  stdout. Valid correlation: exit 0, frame parsed by the strict validator, all
  UID fields 1654, default storage denial and probe cleanup confirmed. These
  prove local shell serialization/default denial only, not Azure source identity
  or the fully opted-in Azure write boundary. No image was built or published.
- The existing WP10 bootstrap fixture was adjusted to consume the new frame;
  its import/endpoint suite was not rerun under this no-import amendment.
- PowerShell wrapper unchanged; no new PowerShell syntax. Windows PowerShell
  5.1.26100.9444 was not available for exact-build execution qualification.

A later retry needs separate owner authority for the exact corrected image
publication, named research staging target, at-most-60-second run, independent
watchdog and exact rollback, plus a concrete private retrieval facility. Before
removing the preflight block, that facility must demonstrate authenticated
application-container source identity, independent immutable-image binding,
bounded retrieval and timeout behavior, and observation/receipt freshness.
A successful control-plane operation, HTTP 200, configuration echo or log marker
alone is insufficient. Missing collection continues to require stop/restoration
and an INCOMPLETE result; a failed stop/restore remains critical, never acceptance.
The watchdog deadline is not proof of kernel termination or a hard cloud stop SLA.
No such channel or Azure qualification was performed by this amendment.

`AZURE_DIAGNOSTIC_NOT_RUN`; `F1_COMPATIBILITY_UNVERIFIED`;
`AUTHENTIC_DATA_NOT_IMPORTED`; `PUBLIC_RUNTIME_VERIFIED=False`.
PR #405 remains OPEN/UNMERGED; UI, production and provider operations untouched.


### WP11 private-channel capability qualification ? 2026-10-26

PR #405 was rechecked at head `7e0d9a9493f285fe8518b070b9e2b71920da20b3`
and main/base `c300087aa250d86de24a64ed4bfe0805d3716c9d`. GitHub reported
MERGEABLE/CLEAN, with no reported status checks. No conflicts were reproduced,
so no merge/rebase/force push or unrelated branch change was necessary. The
previous false report cannot be assigned a cause from this observation alone.

Actual read-only facts: exact research app enabled, Running, Linux; installed
Azure CLI 2.89.1 lacks `webapp exec`. The cached candidate is linux/amd64,
UID/GID 1654:1654, entrypoint `/bin/sh /app/research-bootstrap.sh`, no default
command. `research-api.Dockerfile` matches this model; `research.Dockerfile` is
an older fixture, not the research API image. The amended shell is not in the
historically published image. No replacement image was built or published.

Implemented `-InspectChannel` / `--inspect-channel` and a fixed-source SCM GET
adapter using only the existing operator Entra session. It accepts only the
exact research SCM HTTPS log-index/logstream URLs, validates TLS using system
trust, disables proxies and redirects, limits reads to 32 KiB, uses two-second
socket timeouts and an eight-second read budget checked between reads (one
in-flight read can add up to two seconds). No raw body/token is printed or saved.
The inspection fetches the log index only, never application log contents,
publishing credentials, settings or process commands. Inspection and execution
flags are mutually exclusive. Only a bounded allowlisted capability summary is
returned. Offline frame extraction rejects missing/duplicate/truncated/wrong-
source/stale/mismatched data and never returns execution qualification.

One initial capability request failed without an established cause. Subsequent
bounded inspections succeeded. The final actual observation was: existing
operator session available, authenticated SCM index GET succeeded with recognized
schema, anonymous GET explicitly denied (401/403), execution binding unqualified.
This establishes a protected management read path at inspection time only. It
proves neither logstream delivery reliability nor candidate execution. No stream
or historical application log body was fetched in this task.

| Candidate channel | Supported conclusion | Remaining obligation |
|---|---|---|
| Entra-authenticated SCM log index | Protected read access actually observed | Index is not execution output or immutable-image evidence |
| SCM logstream/container logs | Bounded read adapter and extraction tested offline | Live delivery, exact container/attempt identity and resolved image binding unverified; SCM TLS is not container attestation |
| Supported container terminal | Official docs distinguish main app from SCM | Installed CLI lacks command; no upgrade or remote command performed |
| Detached exec output | Not selected | Request acceptance is not completion/output retrieval |

The hard publication/execution block remains unconditional. Fixed source URLs,
HTTP 200, a nonce frame, configured image digest, or a mock transport cannot
qualify execution. Watchdog/finally rollback/critical failure behavior, production
storage guard, WAL/D5/single-writer, TLS/auth and non-durable limits are unchanged.

Verification: **40/40** focused Python tests passed on Windows and **40/40** in
actual cached Linux Python, network disabled, non-root, read-only source/root and
bounded disposable tmpfs. These are the same offline suite on two platforms,
not 80 independent cases. **50/50** focused `ResearchStorageBoundaryTests` passed
with `dotnet test ... --no-restore -c Release --filter FullyQualifiedName~ResearchStorageBoundaryTests`.
Existing unrelated synthetic-test analyzer warnings remain. PowerShell parser
passed on 5.1.26100.9711; exact 5.1.26100.9444 execution remains unqualified.
UTF-8/no-BOM/LF and `git diff --check` passed; evidence docs append-only.
Owner checkout HEAD/status and all 31 dirty/untracked file hashes preserved.

**Single next action:** obtain renewed owner authority for one bounded channel-
qualification experiment on the exact research staging app, including corrected
image publication, maximum 60-second candidate window, independent watchdog and
exact rollback. That experiment must attempt to collect independently sourced
platform container/instance identity and resolved immutable-image evidence bound
to the private nonce frame. If the platform cannot expose that association, the
contract remains unmet; no acceptance or bypass follows. Reviewed integration
of that evidence source is required before removing the hard block. No claim
is made that a live retry alone will close the proof gap.

Sources: [App Service container access](https://learn.microsoft.com/en-us/azure/app-service/configure-linux-open-ssh-session),
[authentication methods](https://learn.microsoft.com/en-us/azure/app-service/deploy-authentication-types),
[custom-container logs](https://learn.microsoft.com/en-us/azure/app-service/configure-custom-container).

`AZURE_DIAGNOSTIC_NOT_RUN`; `F1_COMPATIBILITY_UNVERIFIED`;
`AUTHENTIC_DATA_NOT_IMPORTED`; `PUBLIC_RUNTIME_VERIFIED=False`.
No Azure writes, UI operations, provider calls, imports, deployment or merge.


### WP12 decision and live-qualification preparation ? 2026-10-26

**Disposition: BLOCKED for live qualification; preparation complete.** Rechecked
PR #405 OPEN, MERGEABLE/CLEAN, exact head
`1e420a7219d7886f5d7303db880a33945790ce8b`, base
`c300087aa250d86de24a64ed4bfe0805d3716c9d`; isolated worktree clean.
No provenance bypass, speculative transport implementation or further Azure
inspection was justified: the preceding authenticated-index observation already
establishes access, and the restored shell cannot qualify the amended candidate.
No new Azure/SCM operation or candidate build was performed in WP12.

Engineering decision: retain the bounded Entra-authenticated SCM adapter and hard
execution/publication block. The preferred experiment would collect private
container output together with independently platform-originated execution
records, rather than introduce a public diagnostic route or an external service.
Its trust model relies on Azure TLS/platform/operator integrity and registry
content addressing; it is operational corroboration, not hardware/cryptographic
runtime attestation. The following proof chain is required, not yet demonstrated:

1. Registry-qualified linux/amd64 leaf manifest and independently matched local
   config/rootfs establish the candidate artifact before publication/deployment.
2. Platform-originated pull/start records must identify the **resolved leaf
   digest**, exact instance and application-container execution ID. A desired
   image setting or successful start acknowledgment is insufficient.
3. The authenticated retrieval source must associate the diagnostic frame with
   that **same execution ID**. A generic site log index or timestamped aggregate
   stream does not establish this association. SCM's own container is distinct
   from the main application container.
4. The existing validator must verify a fresh, non-reused nonce, exact digest,
   entrypoint, schema, size and observation/receipt window. A frame echoing those
   values proves correlation only; it cannot replace steps 2 and 3.

No currently observed facility supplies steps 2 and 3. If a bounded download or
stream cannot expose them, return INCOMPLETE and keep the block. Using authenticated
output plus configuration echo would be a narrower check of deployment intent,
not the required execution/image provenance, and is not selected for acceptance.
The current adapter supports exact raw JSON lines only; platform-prefixed output
would require independently characterized, reviewed parsing, not guessed stripping.

#### Preflight and rollback checklist for a separately approved experiment

- Target only `aiqr115researchstagingwus27f5eabb5`, resource group
  `rg-aiq-r112-wp05-wus2-7f5eabb5`, existing F1 plan
  `asp-aiq-r112-wp05-wus2-7f5eabb5`. Verify resource IDs/plan/Linux and no concurrent
  operator change. UI `aiqr112wp05wus27f5eabb5` is excluded from every operation.
- Capture baseline image/startup, complete app settings including slot metadata,
  full logging/configurable web configuration, original state and relevant site
  metadata into access-controlled private state. Hash those captures; do not
  publish their values or local paths. Re-read equality immediately before mutation.
- Record exact source commit, corrected image config/rootfs, linux/amd64 leaf
  digest, unique proposed tag, existing registry destination and anonymous pull
  verification. Do not reuse the historical digest for changed code. All artifact
  identifiers must be filled and reviewed **before** approval is executable.
- Prequalify exact retrieval URLs, operator authorization, anonymous denial,
  redirect/proxy/TLS rules, time and byte bounds, and platform execution-ID/digest
  record format. Never grant a flag based solely on authenticated SCM index access.
- Allocate one fresh attempt nonce and protected single-execution journal; refuse
  reuse/retry. Check the independent watchdog can launch before start, and record
  the exact stop deadline and watchdog failure signal. No background data task.
- Proposed Azure writes: stop/confirm only research baseline; set exact immutable
  candidate and diagnostic-only correlation/guard opt-ins; enable only required
  bounded logging; start once. Poll bounded evidence. No API, provider, database,
  import, UI, public endpoint, storage resource or paid infrastructure operation.
- Stop immediately on absent/mismatched/stale/partial/duplicate output, startup or
  collection failure, watchdog failure, or deadline. Independent watchdog requests
  stop at 30 seconds; controller requests stop by its earlier collection deadline.
  Verify stopped with bounded retries; never infer termination from request success.
- In finally-style recovery, cancel/join watchdog only after stop confirmation;
  restore exact image/startup, complete baseline settings and logging, and every
  other changed configurable field. Restore original Running/Stopped state.
  Independently re-read and compare against captured baseline; report sanitized
  field names/booleans only. Audit timestamps/etags are not configurable rollback
  fields. Persist failures privately; report critical rollback-unconfirmed when
  equality or stopped state cannot be confirmed. Do not restart a candidate again.
- The requested maximum is **60 seconds**, but asynchronous Azure stop plus a
  client-side watchdog does not prove a hard kernel execution-time cap. Client
  host/session loss and delayed control-plane completion remain unresolved risks;
  neither guaranteed termination nor guaranteed rollback is established. Under
  WP12's stop condition, this is an additional preflight blocker, not an implicit
  waiver. No live execution is armed by this preparation.

#### Exact proposed owner authorization ? NOT READY TO EXECUTE

The following request is a review template, not an approval and not executable
with unresolved fields: "Authorize publication of exactly corrected candidate
`<reviewed immutable linux/amd64 leaf digest>` from `<reviewed source commit>`
under `<reviewed unique tag in existing registry>`, separately from Azure writes.
Authorize one research-only diagnostic attempt on the exact app/group/plan above,
with a maximum 60-second window, pre-start independent stop watchdog, bounded
private collection and mandatory exact rollback/independent equality verification
as specified above. No UI/provider/import/production/new-resource activity. No
second start or retry. Missing execution-ID/resolved-image association must remain
INCOMPLETE; rollback uncertainty is critical."

**Single next action:** resolve the live-experiment preflight with the owner using
this concrete request, including the exact new artifact and retrieval association,
and the unresolved hard-stop/rollback feasibility. Until those prerequisites are
established and fresh explicit authority is received, do not publish, arm, mutate
or remove the block. This report does not imply that approval alone establishes
technical feasibility; failure to establish provenance or bounded stop remains a
reason not to execute. No additional governance/licensing approval is requested.

WP12 verification: existing focused controller suite **40 passed, exit 0** with
`python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp11_research_diagnostic.py -q`.
No application/controller/test code changed; prior Windows/Linux/.NET results
remain historical evidence, not newly repeated qualification. Both documentation
changes append-only, strict UTF-8/no BOM/LF and `git diff --check` passed. Owner
HEAD/status and all 32 dirty/untracked file hashes preserved. PR left OPEN/UNMERGED.

`AZURE_DIAGNOSTIC_NOT_RUN`; `F1_COMPATIBILITY_UNVERIFIED`;
`AUTHENTIC_DATA_NOT_IMPORTED`; `PUBLIC_RUNTIME_VERIFIED=False`.


### WP14 HTTPS application integration ? 2026-10-27 (offline only)

Owner direction: use the existing authenticated HTTPS/JSON application path,
independently of diagnostic attestation. Diagnostic fail-closed behavior is unchanged.
Existing Streamlit opt-in -> server-side `ResearchSnapshotClient` -> HTTPS POST
`/v1/research/snapshot` -> committed C# research state/analytics -> validated public
DTO -> existing Plotly chart. No new endpoint, dependency, provider adapter,
synthetic fallback or Python indicator computation.

Missing database previously stopped the process. Startup now applies the complete
unchanged host/kernel/private-directory guard first, then opens only an existing
database. Genuinely absent state creates no SQLite/schema/tuples/candles and yields
503 `DATASET_NOT_READY` for authenticated, valid snapshot requests. Invalid paths,
permissions, corrupt state and guard rejection remain fail-closed. Existing state
still requires its D5/HMAC publication tuple and single-writer/WAL protection.

| Observation | Meaning |
|---|---|
| GET `/health`: 200 `Alive` | Process only; not dataset readiness |
| Authenticated snapshot: 503 `DATASET_NOT_READY` | Database absent at startup |
| Valid snapshot, zero candles | No committed completed candles for instrument |
| Valid snapshot, retained rows | Validated history; freshness is the DTO's own status |
| Valid 503 `DATA_UNAVAILABLE` envelope | Reachable API, validated state unavailable |
| Valid 401 envelope | Authentication rejected |
| Timeout / transport error / invalid response | Distinct fixed client/UI states |

Only strictly validated bounded error DTOs become UI explanations. Error responses
are capped at 4,096 bytes; existing success limit, TLS verification, direct connection,
no redirects/proxy, timeout and no-retry behavior remain. Native DNS is not hard-
cancellable. Failures retain last-good history but mark it disconnected. Existing
freshness and transformed-educational-value disclosure remain: authentic source
candles are not a claim that transformed public prices are raw market quotes.
A missing-state process does not hot-open later initialization; stop it, initialize
privately under separate authority, then start it. Non-durable F1 replacement may
lose all state; no fabricated auto-seeding or public initialization route exists.

#### Future manual deployment/smoke sequence (redacted; NOT EXECUTED)

Exact existing targets and non-secret setting values are retained in a separate
private owner checklist. No credentials were accessed. All identifiers/artifacts
below must be reviewed and supplied privately; do not execute placeholder commands.
Use Windows PowerShell 5.1-compatible commands and UTF-8/LF for saved text.

1. Separately approve the exact source commit and both immutable linux/amd64 image
   digests. Reuse `research-api.Dockerfile` and `research-chart.Dockerfile` with their
   existing offline publish/dependency contexts. No image build/publication here.
2. Privately capture both apps' full configurable image/startup/settings/slot metadata,
   logging, HTTPS-only and original state with restricted user/System access. Confirm
   exact existing resource IDs and plan. Define rollback owner before any mutation.
3. Research configuration must retain exact-site ephemeral guards, fixed private
   directory, required UID/mount/role, explicit Azure transport opt-in and ingress
   HTTPS-only enforcement. Backend HTTP is the existing explicit platform TLS-
   termination mode, not end-to-end TLS into Kestrel. Failed guards stop deployment.
4. UI opts into the existing research chart with an HTTPS base URL and shared API
   key delivered privately to both server environments. No key in browser requests,
   arguments, screenshots or public evidence. Preserve unrelated existing settings.
5. Only after separate approval, apply reviewed images and protected settings files:

   ```powershell
   # Variables identify only reviewed existing staging targets/artifacts/private files.
   az webapp config container set -g $rg -n $research --container-image-name $researchImage --output none
   az webapp update -g $rg -n $research --https-only true --output none
   az webapp config appsettings set -g $rg -n $research --settings "@$researchSettingsFile" --output none
   az webapp config container set -g $rg -n $ui --container-image-name $uiImage --output none
   az webapp config appsettings set -g $rg -n $ui --settings "@$uiSettingsFile" --output none
   ```

   These may recycle apps. Never run the diagnostic controller as a prerequisite.
6. Bounded smoke: one research health GET, one unauthenticated snapshot POST (401),
   one authenticated snapshot per supported instrument with schema 1.0, epoch 1,
   null known identity. Use the existing strict server-side HTTPS client; no keys
   in CLI arguments or browser. Missing state should yield 503 and the matching UI
   warning; that verifies missing-state behavior, not public chart acceptance.
7. Authentic private initialization is a separate unresolved delivery/authorization
   prerequisite. WP14 makes no provider request or import, and does not invent an
   Azure private-input channel. Prior acquisition limits are not permission to run.
8. After separately authorized authentic initialization/restart, browser acceptance
   must verify both instruments, <=1,000 completed minute candles, UTC/volume,
   existing indicators/null warmup, refresh/retention, instrument isolation, zoom/
   pan/Return to Live, disclosure, stale/disconnected states and no secret exposure.
   A health response, empty chart or deterministic test fixture cannot pass acceptance.
9. Rollback: stop affected named app(s) only; restore exact captured image/startup,
   settings/slot metadata/logging/HTTPS-only and original app state; remove only
   newly added settings. Independently re-read/compare. Configuration restoration
   cannot restore lost ephemeral SQLite data. Report residual differences; no retry
   acquisition/import or automatic redeployment.

Next authority needed: reviewed exact artifacts, publication and deployment only to
existing staging targets, protected configuration handling, exact rollback, the
bounded health/401/two authenticated snapshot checks and browser observation.
No provider/import/production/diagnostic activity. Private initialization must be
separately available before authentic chart acceptance. No terminal/image-attestation
or hard diagnostic stop gate is added to the application integration.

`AZURE_DEPLOYMENT_NOT_RUN`; `AUTHENTIC_DATA_NOT_IMPORTED`;
`PUBLIC_RUNTIME_VERIFIED=False`.

WP14 executed validation:
- `dotnet restore tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --source <existing-local-package-cache> -p:NuGetAudit=false --ignore-failed-sources`: reused cached packages only; no network restore or dependency changes.
- `dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-restore -c Release --filter "FullyQualifiedName~ResearchSnapshotEndpointTests|FullyQualifiedName~ResearchStorageBoundaryTests" --logger "console;verbosity=minimal"`: **73 passed**, 0 failed/skipped. Includes actual local TLS C# -> Python missing-state checks for both instruments and existing full/unchanged C# analytics checks. Initial sandbox run had seven certificate-import failures; normal-user rerun passed with no TLS-validation weakening. Existing unrelated synthetic-test analyzer warnings remain.
- `python -B -m unittest python.presentation.test_research_snapshot_client -q`: **34 passed**. Error/authentication/body bounds, malformed data, timeout/unreachable distinction and retained disconnected cache covered.
- Installed project virtual environment, `PYTHONPATH=python/presentation`, `python -B -m unittest discover -s python/presentation -p test_research_chart.py -q`: **25 passed**, including Streamlit AppTest missing-state rendering/no fallback plus existing chart/viewport/indicator tests. Expected bare-mode ScriptRunContext warnings only.
- Strict UTF-8/no BOM/LF and `git diff --check`: passed. No deployment script changed; no script execution, Docker build/Linux runtime qualification, actual browser acceptance or live staging HTTPS probe was performed. These local tests are not public runtime verification.
- Owner checkout HEAD/status and all 34 dirty/untracked file hashes preserved; isolated branch only. Exact operational checklist retained privately with restricted ACLs and no actual secret values.

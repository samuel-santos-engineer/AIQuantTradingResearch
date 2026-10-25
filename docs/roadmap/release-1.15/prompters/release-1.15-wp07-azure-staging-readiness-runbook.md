# Release 1.15 WP07 - Azure staging readiness and operator runbook

Date: 2026-10-25. Baseline: merged PR #395, `71e4f8ccce877547d11c965ded92280fb02e65de`.

**Outcome: preparation complete; deployment BLOCKED. PUBLIC_RUNTIME_VERIFIED=False.**
This is a reviewable operator checklist, not deployment authority or Azure runtime acceptance. All new/modified text must remain UTF-8/LF. No application, dependency, schema, provider, Azure or production changes are included. PR #386/#387 are not imported. Issues #368/#369/#372/#373/#374 remain open for their own disposition.

## Exact publication scope

1. This new runbook: `docs/roadmap/release-1.15/prompters/release-1.15-wp07-azure-staging-readiness-runbook.md`.
2. Append only: `docs/roadmap/release-1.15/RELEASE_1.15_WP03_WP04_FOUNDATION_PREFLIGHT.md`.

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
| Research HTTPS platform routing | Code UseHttps only on 52731; App Service documents HTTP backend routing | BLOCKED | Resolve explicit transport-contract/image change in a separately scoped proposal; port assignment or HTTPS-only alone cannot fix backend protocol |
| External HTTPS and caller authentication | Local missing/wrong key, hostname and CA rejection PASS; research shell HTTPS-only false | BLOCKED | Require HTTPS-only, validated public hostname/CA and server-side key; prove direct anonymous/wrong-key rejection before UI activation |
| Research ingress | Public DNS/network enabled; Allow rule; no remote endpoint test | NOT RUN | Verify compatible restrictions if available; public DNS is not private ingress; authenticated bounded transformed endpoint only |
| Private persistent WAL storage | No research mount/settings; current writer explicitly local-process ownership only | BLOCKED | Identify actual durable backend, UID permissions, WAL/locking/replacement guarantees; no shared-filesystem or ephemeral-disk substitution |
| D5 provisioning and initial data | Read service requires existing DB/active tuple; test exporter is test-only; no operational initializer | BLOCKED | Separately specify secure provisioning/recovery procedure and authoritative state acquisition; never deploy disposable fixture/fake key |
| UI secret isolation | Local image/mount/browser negatives PASS; legacy Azure UI secret setting names remain | BLOCKED | Remove provider/HMAC/private mounts from UI; provide only environment-specific service key server-side and CA as needed |
| Health versus data readiness | Research GET /health returns Alive; snapshot validity checks actual state; UI /_stcore/health | PASS_LOCAL | Probe separately; liveness 200 cannot establish 401-candle readiness or chart usability |
| Lifecycle and writer exclusion | Local graceful and quiescent SIGKILL restarts, same-volume contender denied | PASS_LOCAL | Azure replacement overlap, interrupted-write/power-loss and cross-host lease semantics remain unqualified |
| Actual Azure browser/API/no-leak acceptance | No deployment or remote runtime requests in this task | NOT RUN | Separately authorize bounded staging verification; retain PUBLIC_RUNTIME_VERIFIED=False |

PASS_LOCAL in the inventory row means the inspection completed from the workstation, not local simulation of Azure or cloud workload acceptance.

## Routing and storage findings

Research starts `dotnet AIQuantTradingResearch.Worker.dll --research-snapshot-api`, binds 0.0.0.0:52731 and unconditionally calls `UseHttps(certificate)`. Setting WEBSITES_PORT=52731 does not change the incoming protocol. Microsoft documents frontend TLS termination and HTTP routing to the container. **Inference from code plus platform documentation:** the unchanged research image is not ready for standard App Service routing. No HTTP listener, proxy, sidecar, combined image, certificate bypass or transport exception is introduced here. This needs a narrow owner-reviewed resolution before image promotion. Setting httpsOnly=true is still necessary externally, but is insufficient to repair the backend mismatch.

Microsoft documents persistent /home separately from container-local files; persistence alone does not prove SQLite semantics. SQLite documents WAL's same-host/shared-memory requirement and incompatibility with network filesystems. Current ResearchWriterOwnership explicitly says it is not a cross-App-Service lease; FileShare.None on one local Docker volume is not a distributed replacement lease. A shared network mount is therefore not an accepted WAL solution, and ephemeral local storage is not an accepted durable solution. Record the selected mount's actual filesystem, permissions, sync/rename behavior, replacement overlap and recovery witness evidence before considering activation. No storage backend is selected by this PR.

Sources checked 2026-10-25: [App Service custom containers](https://learn.microsoft.com/en-us/azure/app-service/configure-custom-container), [Linux App Service FAQ](https://learn.microsoft.com/en-us/troubleshoot/azure/app-service/faqs-app-service-linux-new), [App Service hosting plans](https://learn.microsoft.com/en-us/azure/app-service/overview-hosting-plans), [SQLite WAL](https://sqlite.org/wal.html). Free compute has per-app CPU quotas on shared hosts; these documents do not certify current workload headroom. Do not enable Always On or change the tier as an implicit fix.

## Runtime configuration contract

| Role | Required configuration / mounting | Current behavior |
|---|---|---|
| Research | AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0 (image default), AIQ_RESEARCH_PRIVATE_DIRECTORY, AIQ_RESEARCH_API_KEY, AIQ_RESEARCH_TLS_PFX; optional AIQ_RESEARCH_TLS_PASSWORD | HTTPS 52731; existing witnessed DB and active D5 tuple required; generic startup failure exits 1 |
| UI | AIQ_RESEARCH_CHART=1 (image default), AIQ_RESEARCH_BASE_URL, AIQ_RESEARCH_API_KEY; optional AIQ_RESEARCH_CA_FILE for non-system trust | HTTP 8501 behind platform frontend; server-side HTTPS client validates research certificate/hostname |
| D5/private state | Separate high-entropy HMAC material, key epoch/transform epoch, DB and outside-DB watermark, writer.lock | Stored/provisioned through private state APIs; no automatic startup provisioning or live ingestion loop in snapshot entry point |
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

1. **Resolve prerequisites first.** Approve the smallest exact-path transport resolution, durable storage/writer design and real D5/state initialization procedure. Requalify changed images locally. No arbitrary schema/backend substitution or test-fixture publication. If these are unresolved, stop before registry publication or App Service mutation.
2. **Capture rollback privately.** Record both existing image assignments, startup/port/settings/mount/HTTPS/access configuration and recoverable DB/witness/D5 tuple. Research currently has no image; its rollback is the prior empty-shell configuration, not a fictional old research digest. Do not print secret settings, personal registry paths or evidence roots. Database rollback must preserve lineage; do not restore an older DB independently of the watermark/tuple.
3. **Freeze build provenance.** Record source SHA, both Dockerfile hashes, pinned base digests, qualified Python dependency input, portable publish manifest and Linux native SQLite input. Match actual framework versions. Build with network disabled from allowlisted contexts; qualify both exact images together. A code or transport change invalidates promotion of the old pair.
4. **Registry publication (separate permission).** Assign approved registry names, tag the qualified local IDs and push only those two images. Read back each registry manifest/RepoDigest; verify linux/amd64 and config ID match the qualified local image. Record immutable registry digests privately/sanitized. Local image IDs below are not deployable registry references. No ACR creation or registry login is authorized here.
5. **Configure research only after storage/transport closure.** Prepare research-only state/mount permissions, TLS configuration dictated by the resolved design, separate D5 initialization and service-key delivery. Do not reuse legacy synthetic DB, fixtures or UI provider credentials. Assign the approved research registry digest, selected container routing port, httpsOnly=true and /health; do not enable public anonymous snapshot access. No custom startup override is needed for the existing exec entrypoint. Any changed transport may alter this port/health plan and must be documented before execution.
6. **Verify research before UI switch.** Through the Azure hostname prove valid frontend TLS; missing/wrong X-API-Key returns 401 without sensitive details. Authenticated bounded BTC/ETH full/unchanged requests must validate D4/D5 identities, readiness, gaps and no raw fields. /health=200 alone is insufficient. Verify actual filesystem modes, private state survival and exclusive writer across replacement without publishing credentials. Stop on any denial failure, unsupported durable mount or unknown witness.
7. **Configure UI and preserve rollback.** After research verification, remove provider/HMAC/private-storage configuration from this UI app; supply only server-side service key, research HTTPS hostname and public trust material if required. Assign the compatible UI registry digest and port 8501; leave exec Streamlit entrypoint intact. UI must have no research DB, HMAC/provider keys or TLS private-key mount. Do not weaken CORS, browser isolation or certificate validation to get a chart.
8. **Staging acceptance (separate permission).** Verify /_stcore/health and real browser BTC/ETH, indicators supplied by C#, 100/300/1000, UTC gaps, hover/zoom/pan, Return to Live, refresh/session isolation and research-outage/last-good recovery. Capture redacted console/network/server logs, ensuring no key/raw/private data or browser call to research DNS. Measure cold/warm startup, memory/CPU/latency and 1/3/5 sessions on the actual shared F1 plan; record observed quotas and idling. Do not invent thresholds or convert missing telemetry to PASS. Bound requests and stop at quota/resource failure rather than escalating tier or endlessly restarting.
9. **Rollback on failed checks.** Stop rollout, restore prior UI image/configuration and remove the new UI's access to research secrets. Restore research to captured prior image/config or empty shell, coordinated with compatible service keys; stop writers before state recovery. Preserve DB/witness integrity and private diagnostics, never force an older witness/tuple to open. Verify former UI health and no unintended research exposure. No production resource is involved.

Image assignment uses `az webapp config container set --resource-group <staging-group> --name <role-app> --container-image-name <approved-registry-repository>@sha256:<verified-registry-digest>`. This is a withheld template with unresolved placeholders, **not** an executable authorization. Secret, mount and initialization commands are deliberately not invented: they depend on the unresolved supported mechanisms. There is no safe honest end-to-end deploy command for the unchanged research image today.

## Minimum next owner action

Review this readiness PR first. The immediate next engineering action is a narrowly scoped transport/storage/provisioning proposal, not an Azure deploy. Once resolved and locally qualified, separately authorize: publication of exactly two qualified images to an existing registry; updates only to the two named West US 2 staging apps (image, port, HTTPS/access, service-key/CA and qualified research state/mount settings); bounded runtime/restart/browser/negative verification and rollback. Keep the existing F1 plan, no new service or tier, no provider activation and no production changes. If those constraints cannot meet storage or capacity requirements, report the exact limitation for owner disposition. F1 quotas and idle cold starts can limit availability; no recurring-cost expansion is assumed.

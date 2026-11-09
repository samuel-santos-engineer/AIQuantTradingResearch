# Release 1.15 WP58 - public runtime qualification plan

Date: 2026-11-08. **PHASE A COMPLETE; LIVE OPERATIONS NOT AUTHORIZED/NOT RUN.** Source main `4a6bc2b901d6f6ff0081dad4597064017acb86f9` contains PR #444 squash `cfe60efc4d8317dbf3c27df0ecd126117e19f31e` first, then PR #443 squash. Verified Git ancestry/merge metadata; clean isolated branch `docs/release-1.15-wp58-public-readiness`. Owner checkout/untracked files preserved; no private preparation/bundle/database/TLS/credential inspected.

WP57's actual BTC->ETH->BTC local acceptance remains historical **LOCAL_CHART_VERIFIED=True**. This pass did not probe localhost, Docker, Azure, registry, public URLs or provider endpoints, and did not independently confirm continued runtime availability. Public documentation links already in repository were reviewed through recorded evidence only; no new external documentation/network check was made. Reported Azure facts below are historical, never current observations.

## Smallest existing architecture and exact candidate targets

The effective [dual-container amendment](RELEASE_1.15_EXECUTION_PLAN.md#release-115-dual-container-execution-plan-amendment--2026-10-23) supersedes the initial one-image supervisor proposal: two Linux images/single processes, two existing apps per environment. Keep private C# research/SQLite separate from public Streamlit. Streamlit server calls the research app over strict HTTPS with a server-only service key; browser receives only transformed validated DTO/component data, not API/provider keys/raw observations. A public research DNS name is not private ingress: reject unauthenticated snapshots and expose no raw/admin/initialization routes. No paid network features, local-machine tunnel, supervisor revival or combined-image workaround.

Repository-recorded candidate is the **existing staging pair**, not production:

| Resource | Recorded identity / intended route |
| --- | --- |
| Subscription | 7f5eabb5-80f5-4369-88be-b14a6de763f1 |
| Resource group | rg-aiq-r112-wp05-wus2-7f5eabb5 |
| Shared existing plan | asp-aiq-r112-wp05-wus2-7f5eabb5; historical Linux F1 West US 2 |
| Staging UI | aiqr112wp05wus27f5eabb5; https://aiqr112wp05wus27f5eabb5.azurewebsites.net/ |
| Research API | aiqr115researchstagingwus27f5eabb5; HTTPS /health and POST /v1/research/snapshot |
| Production, excluded | aiqr112wp035ec325382770; no read/write/probe proposed. |

README labels West US 2 staging and West Central US production. No assumption about current images/configuration, health, quota or plan membership. Only this exact research site is accepted by the current platform-HTTP/storage exception; do not retarget another app or weaken its guards.

## Prioritized gap matrix

| Priority / gate | Current implementation and repository evidence | Public requirement | Minimal next action/correction | Acceptance check |
| --- | --- | --- | --- | --- |
| P0 deployment blocker: research availability | [WP37](prompters/release-1.15-wp37-image-deployment-preflight.md) records recurring stop-request quota exhaustion/HTTP403 on2026-11-02; root cause unresolved. Local constrained startup passed but is not F1 evidence. | Both staging apps can run concurrently within existing plan limits. | Bounded metadata inventory first; stop on unhealthy/unknown quota state. Any further attributable diagnostic separately scoped; no restart loop/upgrade. | Actual stable API/UI health and bounded authorized cold-start/resource evidence, not Running metadata alone. |
| P0 deployment blocker: cloud data availability | Authentic observations/SQLite exist **locally**; `research-bootstrap.sh` serve opens existing state and does not auto-seed. Missing state may be Alive while authenticated snapshots return DATASET_NOT_READY. | Research service already has authorized authentic state for both instruments. | Determine whether separately hosted research already has authorized initialized state. If absent/unverified, **stop**: WP58 forbids uploading raw observations/DB, reacquisition/import or migration. | Authenticated transformed snapshots1000/1000 and provenance/generation checks, only under explicit later approval. Never use local bundle/image/private mount as a deployment payload. |
| P0 deployment blocker: persistence/lifecycle | [Storage boundary](../../../src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStorageBoundary.cs) allows only exact staging ephemeral root overlay opt-in; private WAL/HMAC/witness/single-writer guards remain. | Loss/restart behavior understood; no durability claim, safe no-data response after loss. | Preserve existing strict staging exception. No Azure Files/shared filesystem substitution or secret/database baking. Resolve safe recovery outside current no-import/no-upload scope if state is absent. | Approved restart shows valid retained state or truthful unavailable response; no auto-fetch/fake history. A restart that loses data does not pass historical-chart availability. |
| P0 deployment blocker: artifact pairing | WP57 local UI ID sha256:3270ce6ec33ba8cb7da27738ea6d9dc27a811a3270e7ecdef83f32e757a83ae1 and research ID sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028 are **local image IDs**, not verified public OCI digests. Old WP35/37 registry artifact predates nullable-volume changes. | Immutable compatible API/UI digests, anonymous/pull access as approved, no private contents. | Later approved source-only allowlisted build/publication/readback; record both real OCI digests. Do not invent a digest or deploy the stale helper's hardcoded old image. | Exact manifest/digest/platform/startup/code identity and public artifact content exclusion proof. |
| P0 deployment blocker: correct UI startup | Root Dockerfile/entrypoint default launches synthetic public_preview_app and sanitizes environment. Dedicated `research-chart.Dockerfile` launches realtime_financial_visualization with AIQ_RESEARCH_CHART=1. | Existing historical research UI, correct app port, unchanged disclosure/viewport. | Deploy the dedicated chart image, not merely set a flag on synthetic root entrypoint. Preserve old public app until all staging prerequisites pass. | Actual staging BTC->ETH->BTC graph mounts and counts; no synthetic substitution. |
| P0 deployment blocker: service auth/HTTPS configuration | Client requires https, validates TLS with default trust if no CA override, uses server-only X-API-Key; C# endpoint whitelists transformed data. Local CA/Docker DNS are not cloud endpoints. | Public UI HTTPS -> research app HTTPS, matching service auth, no browser credentials; exact platform-HTTPS opt-in. | Separately approve least-change settings and existing private key handoff; no provider credential in UI, no /ca.pem/local DNS on Azure, no secrets in CLI/logs. Presence/equality currently unverified. | Wrong/missing key denied; unauthorized public API rejected; no key/raw/provider material in browser/assets/errors. |
| P0 deployment safety: rollback | Existing helper is fixed to old research image and old operation assumptions; no general paired UI promotion workflow or current private rollback baseline. | Complete exact per-app image/startup/HTTPS/settings rollback with unrelated settings preserved. | Capture a separately authorized private rollback baseline before any write; restrict changed keys; stop if original settings cannot be preserved. No speculative whole-settings replacement. | Readback matches approved digest/config; at most one specifically approved rollback, no automatic retry or production promotion. |
| P1 qualification gate: cross-session and resource behavior | Offline sessions/viewport validation and WP57 local browser pass; no public session/resource acceptance. | Separate public browser sessions cannot share instrument/viewport state; bounded page/API costs and fail-closed stale/unavailable behavior. | Test existing controls/caches before adding infrastructure; record cold start/restart limitations and shared F1 pressure. | Two independent contexts, BTC/ETH/BTC, alignment/disclosures, local-only keys, no unexpected provider requests/errors. |
| Optional automation | No tracked .github/workflows deployment pipeline in this commit; root docker-compose is not an Azure paired-image deployment. Recorded CLI/manual helpers and qualification scripts exist. | Reviewable repeatable promotion, eventually automated. | Use existing bounded manual/executable approval process first; CI/CD creation is optional, not a new prerequisite. | Do not claim a GitHub check, automated deployment or broader suite passed without execution. |

The source-relative storage link above should be read with `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStorageBoundary.cs`; current checks remain authoritative. No new licensing/provider approval gate. Twelve Data sole upstream, BTC/USD/ETH/USD and strict default/explicit OHLC-only null semantics unchanged; C# analytics authoritative. No raw quote relabeling or volume fabrication.

## Configuration and artifact proposal - NOT EXECUTED

UI candidate from exact merged source above, using `container/release_1_15/research-chart.Dockerfile` and a secret-free allowlisted context. Image already opts into research UI; planned nonsecret platform config is WEBSITES_PORT=8501 and AIQ_RESEARCH_BASE_URL=https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net. AIQ_RESEARCH_API_KEY is server-only, redacted/unknown; no provider key and no private DB/TLS mount in UI. Use normal public certificate trust rather than local CA. Current UI settings/startup/actual image must be inventoried before choosing any write.

Research candidate uses existing separate `research-api.Dockerfile` and compiled nullable-volume-compatible Worker, bootstrap serve, WEBSITES_PORT=52731, existing exact-site `azure-app-service-http` transport plus real platform HTTPS-only. Reviewed prior nonsecret settings include AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0, AIQ_RESEARCH_PRIVATE_DIRECTORY=/tmp/aiq-research, AIQ_RESEARCH_STORAGE_SCOPE=azure-f1-staging-ephemeral, AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING=1, AIQ_RESEARCH_EXPECTED_MOUNT_PATH=/ and WEBSITES_ENABLE_APP_SERVICE_STORAGE=false. They are a proposal, not observed current configuration. No operator-stdin/initializer mode, raw state transfer or new provider bootstrap is permitted here. Keep API credentials distinct from Twelve Data/provider credentials.

**No deployment image digest is available from this offline pass.** Expected code identity is merged main4a6bc2b with the WP56 fix; local WP57 image IDs are recorded candidates only, not registry attestations. Do not substitute image ID for OCI manifest digest or use ROOT Dockerfile synthetic-candidate as authentic history. Later publication requires its own artifact build/inspection/registry approval and digest readback before an Azure-write approval can be concrete.

Two existing apps on the already allocated historical F1 plan are the smallest topology. No new plan/app, paid tier, paid storage, private endpoint, service or resource requested. That implies no proposed purchased-capacity increase, **not** guaranteed free bandwidth/registry/storage/compute headroom or zero usage. Current plan SKU/limits/remaining quota are unverified. If F1 cannot serve the pair, stop and preserve the verified local dual-container chart; do not upgrade or invent a tunnel/alternate provider. Owner decision is whether to investigate the existing staging pair further, not whether to waive confidentiality or availability evidence.

## Next concrete owner-approved operation: metadata only

Propose **five bounded ARM GETs only**, using existing authenticated CLI (no login if unavailable), explicit subscription/group and API2023-12-01:

1. Exact staging UI site.
2. Exact staging research site.
3. UI /config/web.
4. Research /config/web.
5. Exact shared serverfarms plan.

Site prefix is `/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/`; append sites plus the exact names above or serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5. Timeout20 seconds/request,100-second total request budget, no retry. In-memory projections only: expected resource identity, plan association, region, SKU/Linux, state/usage/availability flags and HTTPS-only; configuration classified as classic image/code/sidecar and startup-empty boolean. Do not emit raw command lines/image credentials, environment collections, app settings, connection strings, tokens or error bodies. No /config/appsettings/list, credentials, registry, health/public URL, API-key probe, Docker or data access. Report absent fields as unknown; stop on timeout/error, identity/plan drift, non-F1 or unhealthy state. Running is not quota/health/readiness proof; quota counters and current serving state remain NOT_READ/NOT_PROBED in this minimal scope.

Mutation set: **none**, so rollback is not applicable to this inventory; no app restart/warm-up requested. No expected new artifact/configuration change/cost-incurring resource allocation. All subsequent publication/deployment/configuration, authenticated API/browser tests and any protected-state/bootstrap decision need fresh explicit scopes. If the five reads cannot establish a safe direction, report the precise blocker instead of deploying.

## Subsequent conditional execution sequence

Only if the owner elects to continue and prerequisites become evidenced:

1. Resolve exact staging capacity/health and allowed data-residency path without uploading local raw observations/database or using synthetic substitutes. If Azure lacks already authorized state, deployment is blocked under WP58; keep working local runtime. No reimport or migration authorized by this plan.
2. Separately approve build/publication of source-only paired images and anonymous manifest/digest readback; no private state/key/TLS in context/image. Lock real digests. Cache-only/no-network builds where qualified; do not assume local cached inputs remain present.
3. Separately approve private rollback-baseline capture and minimal paired staging image/settings operation with exact digests/keys to change. Preserve unrelated settings, key values and slot metadata. No production change, scaleout, upgrade, new resources or stop/start loops. If settings-preserving restoration cannot be established, do not mutate.
4. Apply API first only after its data/startup readiness gates pass; bounded actual health (not just Alive) and authenticated transformed datasets must pass before UI replacement. Apply UI, read back exact identities and settings, then one bounded public Chrome session BTC->ETH->BTC plus separate-session isolation. Inspect only counts/alignment/labels/error categories, never raw prices/keys/screenshots. Verify volume null/no bars, 981 non-null SMA where1000 contiguous candles, C# identity/provenance and educational labels. Set no public-ready flag on HTTP200 alone.
5. Within explicitly approved restart scope test one bounded cold-start/restart and truthful data loss handling. On first failure stop; run only the specifically authorized rollback to saved image/config, then report. Ephemeral loss is a risk, not automatic reimport permission. PUBLIC_RUNTIME_VERIFIED can become true only after all real public acceptance checks pass, with recorded candidate identities and limitations.

## Actual offline checks and disposition

Existing venv/cached dependencies, .NET untouched. Commands:

```text
PYTHONPATH=python/presentation
<existing-venv-python> -B -m unittest test_research_chart test_research_snapshot_client -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp36_research_deploy.py -q
```

| Check | Run | Pass | Fail | Skip | Exit |
| --- | ---: | ---: | ---: | ---: | ---: |
| Chart/client | 66 | 66 | 0 | 0 | 0 |
| Workflow | 39 | 38 | 0 | 1 | 0 |
| Mocked deployment helper | 42 | 42 | 0 | 0 | 0 |
| Total | 147 | 146 | 0 | 1 | 0 |

Windows unprivileged symlink skip retained; no new Linux/browser/CI/cloud claim. Existing analyzer/bare-mode warnings not repaired. No application/config correction justified before live facts and the state-hosting blocker are resolved; dedicated separate images and strict HTTPS client already implement the minimal architecture. This focused PR changes this plan and an execution-plan append only, UTF-8/LF/no BOM and git diff --check verified.

**Verdict:** WP58 Phase A implementation-ready conditional plan complete; **public deployment BLOCKED / NOT_AUTHORIZED**, current Azure feasibility unknown. Historical F1 failure is neither current proof of impossibility nor waived. Specific within-architecture alternative is retain WP57 local deployment while verifying the existing separate research app's supported state/health; no raw upload/paid-resource workaround. No new provider/credential/private-state/Docker/Azure/registry/public probe/login/build/migration/import/deployment operation occurred. Git metadata reads only are not public-runtime checks.

`AUTHENTIC_DATA_NOT_IMPORTED=False` (local historical import); `LOCAL_CHART_VERIFIED=True` (historical WP57 only); `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Subsequently approved five-read inventory - actual result

Owner explicitly approved **only** the five metadata GETs described above: two exact staging sites, their config/web resources and the shared plan,20 seconds/read/100 seconds total,no retries. No login, settings/credential/quota-counter API, registry, public/health/API probe, Docker/data access, writes/restart/deployment. The preceding Phase A NOT_RUN statements are historical; this section records the narrowly approved execution, not expanded runtime authority.

Observed at 2026-11-08T22:25:00.828194+00:00 (UTC), five projected CLI requests invoked, all exit0; total elapsed 8.484 seconds. Used existing installed CLI's direct Python entrypoint, explicit subscription, GET/API2023-12-01 and in-command projections. Raw config/startup/image strings, full responses and error bodies were not emitted or persisted. Outer retries absent and SDK retry-total disabled; no independent wire trace or application request-count claim. No account listing or login, no token/configuration-file inspection. Authenticated CLI credential use is normal approved ARM access, not research/provider credential retrieval.

| Exact read | Observed sanitized result |
| --- | --- |
| Staging UI site | Identity/plan MATCH; Linux; Running, enabled; usage Normal, availability Normal; HTTPS-only true. |
| Research site | Identity/plan MATCH; Linux; Running, enabled; usage Normal, availability Normal; HTTPS-only true. |
| Staging UI config/web | Identity MATCH; CLASSIC_IMAGE; mode known; startup string known and empty. No raw image/startup values captured. |
| Research config/web | Identity MATCH; CLASSIC_IMAGE; mode known; startup string known and empty. No raw image/startup values captured. |
| Shared plan | Identity MATCH; Linux F1, West US 2. |

**Current inference:** the targeted resource associations, current normal site metadata, HTTPS-only and classic-container configuration are confirmed. Historical WP37 QuotaExceeded is **not** the state observed in these reads. This does not explain/fix the historical recurring startup stops or prove capacity/health now; quota counters/CPU headroom, actual serving processes, image digest contents, anonymous health, service auth, dataset initialization and public browser acceptance remain unverified. Do not claim F1 impossibility from the old failure or public readiness from today's Running flags.

No identity/plan/health-state drift requiring early stop was observed in this inventory, and no mutation/rollback was needed. Production was excluded. No local/public URL was visited, no image built/published/inspected, no settings/service-key presence/equality or database examined, no acquisition/import/state transfer. Existing147 offline test executions remain146 pass1 skip; no test was rerun just for this documentation update.

Next smallest decision is whether to separately authorize bounded serving/artifact qualification: exact projected image-digest readback and fixed anonymous staging health probes with explicit cold-start/timeout/stop safeguards. Neither is executed or approved by these five reads. Authenticated dataset checks need their own explicit service-auth channel/scope; if Azure has no authorized initialized state, current no-raw-upload/no-import boundary still blocks deploying the working historical chart. A paired UI deployment is **not yet reviewable for Apply**, because real registry digests, cloud dataset/auth availability and complete rollback evidence are still missing. Preserve local WP57 acceptance; no new licensing/provider gate or paid-resource workaround.

`AUTHENTIC_DATA_NOT_IMPORTED=False` locally; `LOCAL_CHART_VERIFIED=True` historically; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. Review PR remains OPEN/UNMERGED.

## Phase B0 - offline plan and approval boundaries

Source/clean local branch verified at `e5481e0eb2c8664d3ed307587c9382d95af2db7f` on 2026-11-08. This matches the last recorded PR #445 head; live GitHub status has not been rechecked because Phase B0 authorizes offline inspection only. Owner checkout and untracked files remain untouched. No application changes or test reruns are needed for this planning addition.

`ResearchSnapshotEndpoint.cs` handles anonymous GET `/health` with 200 `Alive` before service-key checks. The protected snapshot remains POST `/v1/research/snapshot`, excluded. Existing Streamlit launchers serve the UI page; propose GET `/` to check the HTML shell only, with no assets/WebSocket/browser and no inference that a chart renders. No UI `/health` assumption. WP37 records historical recurring quota/startup failures; the later five metadata reads showed Normal/Running but did not establish serving.

### B1 proposed anonymous serving operation - NOT_RUN

Exactly two HTTPS GETs, sequential research first then UI, at most 15 seconds each / 30 seconds total. Research: `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/health`; UI: `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`. Python standard-library HTTP client in an isolated bounded worker, certificate/hostname verification enabled, proxy credentials/environment not inherited, no Authorization/Cookie, redirects disabled, no retries, cache-busting, assets or browser. Normal DNS/TLS connection required by these exact URLs only. Read at most 64 bytes for exact research `Alive` classification and 64 KiB for HTML-shell classification in memory; never emit/store bodies, cookies, raw headers or exception text. Publish only endpoint role, UTC/time, status code/class, normalized content-type, TLS outcome and constrained classification. Require research 200/Alive and UI 200/text-html/HTML shell; do not infer Streamlit application/data readiness. Stop before the next request on timeout, TLS/host mismatch, redirect, non-200, malformed/oversized/unexpected content or suspected quota/startup failure. GET may trigger Azure cold start and consume F1 capacity; no warm-up/restart loop. No authenticated API request or dataset/key inspection.

### B2 proposed configured identity operation - NOT_RUN

Exactly two ARM GETs, one `/config/web` per allowlisted staging site in subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`, group `rg-aiq-r112-wp05-wus2-7f5eabb5`, API `2023-12-01`; 20 seconds each / 40 seconds total, no retries/redirects. Existing installed authenticated CLI only, no login/token retrieval or credential APIs. In-memory output capture, select only resource id and linuxFxVersion via CLI query, validate id and strict `DOCKER|registry/repository:tag` or `DOCKER|registry/repository@sha256:<64 lowercase hex>` grammar before any output/storage. Reject embedded userinfo, URLs, query/fragment/whitespace or unknown syntax with sanitized category only. Publish validated registry/repository and tag-versus-digest identity; never full config, startup or raw CLI/errors. Stop on first failure/drift/unsafe reference. No Azure mutation or settings access; normal ARM read traffic only.

A configured tag is mutable. A configured sha256 reference is an immutable reference string, not independently verified registry content. A local Docker image ID is neither. Existing `wp36_research_deploy.py` resolves a hardcoded older image using a registry token endpoint and platform-manifest reads; it must not be reused here. Registry access is NOT proposed for execution until exact references are known. `B2_IMMUTABLE_OCI_DIGEST=UNKNOWN` until a separate no-credential exact-manifest scope can be specified; if registry requires credentials/token handling, stop UNKNOWN without login/challenge-token acquisition/pull. No registry enumeration or unrelated repository access.

B1 and B2 require separate explicit owner approvals; neither is granted by the Phase B prompt. Current outcomes: `B1_ANONYMOUS_SERVING=NOT_RUN`, `B2_CONFIGURED_ARTIFACT_IDENTITY=NOT_RUN`, `B2_IMMUTABLE_OCI_DIGEST=UNKNOWN`. No Azure/registry/public URL/DNS/browser/Docker/credential/private-state operation occurred in Phase B0. No GitHub network call or PR publication occurred in this offline pass; append is pending publication after live scope clarification. Cloud state/auth and rollback remain blockers; Phase C needs separate authority and cannot acquire/import/upload/migrate data.

`AUTHENTIC_DATA_NOT_IMPORTED=False` (local); `LOCAL_CHART_VERIFIED=True` (historical WP57); `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. UTF-8/LF/no BOM required and checked with separate byte validation plus `git diff --check`.

## Phase B approved execution - stopped at first serving request

Observed UTC `2026-11-08T22:49:28.994305+00:00`. Owner separately approved B1 and B2 exactly as proposed. Invoked one bounded anonymous research `/health` worker, with TLS verification enabled, no credentials, proxy inheritance, redirect following or retries. It returned no usable projected response before the guarded operation ended: `DEADLINE_OR_WORKER_FAILURE`. The sanitized evidence does not distinguish a deadline from another worker failure; no HTTP status, content type, validated TLS result or response classification was captured. Do not infer an Azure quota error or application root cause. At most one research GET was attempted; completion on the wire is unknown. The outer worker had a 15-second cap and the request a 14-second socket timeout.

Stopped immediately under the approved failure rule. UI GET NOT_RUN, both ARM config/web reads NOT_RUN, registry NOT_RUN; no automatic retry, cold-start loop, authenticated probe, Docker, data access, settings/key inspection, mutation, restart or deployment. Approving both categories did not waive the first-failure stop rule.

| Outcome | Status | Evidence / limitation |
| --- | --- | --- |
| B1_ANONYMOUS_SERVING | FAIL | Qualification failed at first research worker; HTTP/TLS and root cause UNKNOWN. UI not requested. |
| B2_CONFIGURED_ARTIFACT_IDENTITY | NOT_RUN | Zero ARM reads in Phase B; stopped before this category. |
| B2_IMMUTABLE_OCI_DIGEST | UNKNOWN | Zero registry calls; configured references not newly read. |

PR #445 was independently checked OPEN/UNMERGED at expected head e5481e0 before evidence publication; base 4a6bc2b901d6f6ff0081dad4597064017acb86f9. This GitHub metadata check is repository evidence publication, not an Azure/runtime probe. The B0 pending-publication statements above describe the earlier checkpoint. Only the same two documentation files changed. UTF-8/LF/no BOM and git diff --check verified; no tests rerun for documentation, earlier 147 run/146 pass/one skip remains historical.

Smallest next owner decision: whether to authorize one replacement research-only anonymous `/health` request, with stage-specific sanitized timeout/transport reporting and the same 15-second cap/no redirects/no retries, to distinguish controller/worker failure from an observed HTTP result. This is proposed only; no further serving or B2 operation will run automatically. Public deployment, cloud-state/authentication and rollback remain blocked/unverified.

`AUTHENTIC_DATA_NOT_IMPORTED=False` (local); `LOCAL_CHART_VERIFIED=True` (historical WP57); `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Separately authorized single replacement research probe

Owner explicitly authorized the proposed research-only replacement with stage-specific sanitized reporting; no renewed UI/ARM/registry scope. Source and live PR #445 head matched `512bd9fbdab9bd2bef68b1233092c133dd01da7d`, clean worktree; PR OPEN before publication.

Observed UTC `2026-11-08T22:59:08.987689+00:00`. One worker / one anonymous GET to exact staging research `/health`; no redirects/retries/proxy inheritance/credentials, certificate and hostname verification enabled; request timeout 14 seconds, outer cap 15 seconds. Actual elapsed 1.185 seconds. Stages: WORKER_STARTED -> REQUEST_OPEN -> RESPONSE_HEADERS -> COMPLETE; worker exit 0. HTTP **403**, class 4xx, TLS **VALIDATED**, normalized content type **text/html**, classification **HTTP_REJECTED**. No error body read or stored; raw headers/errors not emitted. This establishes an actual HTTP rejection, not its origin or root cause. Do not label it quota exhaustion, authentication failure or application failure without separately authorized evidence. It does not explain the earlier worker/deadline failure.

`B1_RESEARCH_ONLY=FAIL`; overall anonymous serving remains unqualified. UI NOT_RUN; B2 configured identity NOT_RUN; immutable OCI digest UNKNOWN. Stopped after this single response. No additional network/runtime checks, mutation, restart, deployment, dataset/key inspection or provider operation. Earlier offline test counts unchanged; documentation-only UTF-8/LF/no BOM and diff checks passed.

Next smallest decision is whether to authorize a bounded projected research-site ARM metadata read (state/usage/availability only, exact resource), to distinguish current platform rejection metadata from the prior Normal observation; no permission inferred and no read executed. Cloud data/auth, artifact identity and rollback remain unresolved. `AUTHENTIC_DATA_NOT_IMPORTED=False` locally; `LOCAL_CHART_VERIFIED=True` historically; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Owner-requested quota-first then research state reads

Owner explicitly requested quota first, then state/usage/availability. At UTC `2026-11-08T23:14:30.268281+00:00`, performed exactly two projected ARM GETs in order: existing shared plan `/usages`, then exact research site metadata, API 2023-12-01; 20 seconds/read, 40 seconds total, no retries. Both passed in 4.308 seconds. No settings, credentials, HTTP serving probes, registry, Docker or mutations. PR #445 OPEN at matching clean head cc56c18 before publication.

| Shared-plan counter | Current | Limit | Unit |
| --- | ---: | ---: | --- |
| WPStopRequests | 0 | 15 | Count |
| CpuTime | 0 | 3600000 | Milliseconds |
| CpuTimeShortPeriod | 0 | 180000 | Milliseconds |
| BytesSent | 0 | 173015040 | Bytes |
| MaxMemoryWorkingSetShortPeriod | 0 | 1073741824 | Bytes |
| FileSystemStorage | 2178048 | 1073741824 | Bytes |

Research site exact identity MATCH: **Running / usage Normal / availability Normal**. Quota API reported nextResetTime 2026-11-09T00:00:00 for the ordinary counters, 2026-11-08T23:19:00 for short CPU and sentinel 9999-12-31T23:59:59.9999999Z for filesystem. Non-sentinel reset strings contain no timezone offset; preserve them as reported, do not assign a timezone or promise a reset fixes serving. No measurement-interval timestamp was supplied by this projection. Zero counters are Azure-reported values, not proof of no activity or telemetry freshness. Plan-wide counters cannot attribute a stop or consumption to a particular app.

Current reported finite quotas are below limits; no current quota-exhaustion evidence in this snapshot. The prior 403 was observed at a different time and remains unexplained; Running/Normal does not establish HTTP health. No new probe was authorized or performed. B2 image-reference reads remain NOT_RUN, immutable registry identity UNKNOWN; cloud dataset/auth and rollback remain unresolved. No tests rerun for documentation; UTF-8/LF/no BOM and diff checks passed.

`AUTHENTIC_DATA_NOT_IMPORTED=False` locally; `LOCAL_CHART_VERIFIED=True` historically; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. Next decision is whether to authorize a bounded research-only serving diagnostic; no retry, detector query or body inspection is inferred from this quota/state request.

## Phase C0 - research 403 origin diagnostic design, C1 NOT_RUN

Verified PR #445 OPEN and clean isolated branch at expected `2c1224f8bbb054ff46777a651f1c9ea6965aa22a`, base `4a6bc2b901d6f6ff0081dad4597064017acb86f9`. Only GitHub metadata was queried for this required repository verification. No Azure, registry, public endpoint or local runtime call occurred in C0. Preserve owner checkout and all local acceptance as historical.

Source `ResearchSnapshotEndpoint.cs:59` returns anonymous GET `/health` 200/Alive before auth. Missing/invalid service-key response is 401, not 403. This source has no demonstrated health-403 branch; actual deployed artifact identity is still UNKNOWN, so this does not prove the response originated outside the application. Historical platform site-disabled classification and later Normal quota/state snapshots occurred at different times and do not attribute today's rejection. Plausible unproven alternatives include platform/front-end restriction or rejection, different deployed runtime/routing, or transient platform lifecycle state. No source defect established; no code fix justified.

### Exact C1 proposal - requires new explicit approval

One anonymous GET only to `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/health`. System TLS hostname/certificate verification; direct connection with proxy inheritance disabled; no Authorization/Cookie, redirects, retries, cache-busting, request body, assets or browser. Socket timeout 14 seconds, total worker cap 15 seconds including startup; kill only the owned worker at deadline, no replacement attempt. GET may cold-start the F1 app and consume capacity; no warm-up loop or restart. No HEAD substitution, ARM/CLI, quota, logs, SCM/Kudu, registry, credentials, data or UI/production probes.

Response-body read budget **zero bytes**: no read(), decode, error-body inspection or storage; close response after headers. HTTP library may internally buffer transport data while parsing headers; no application body access is permitted. Do not publish raw exception/header objects. Fixed sanitized output schema: observedUtc, elapsedSeconds, worker/request stage, TLS {VALIDATED,FAILED,UNVERIFIED}, numeric HTTP status/class when observed, normalized contentType {text/plain,text/html,application/json,application/octet-stream,OTHER,MISSING}, booleans serverPresent/viaPresent/xMsHeaderPresent/xArrHeaderPresent/locationPresent, result and origin. Header values, names of individual x-ms/x-arr fields, request/correlation IDs, cookies/auth challenges, internal addresses, URLs from Location and error bodies remain unrecorded. Header-presence booleans do not prove origin.

Fail-closed decision tree: deadline/transport/TLS failure -> FAIL with sanitized stage/category, origin UNKNOWN; any redirect/location -> STOP without following, origin UNDETERMINED; 403 -> HTTP_REJECTED / ORIGIN_UNDETERMINED; other 4xx/5xx or unexpected status/content -> STOP/FAIL; 200 with expected content category -> HEADERS_ONLY_200, never Alive/dataset/chart acceptance because body is not read. No generic header or text/html alone attributes platform versus application. Stop after the one request regardless of outcome. Sensitive-output concern -> suppress result and stop. C1 approved=false/executed=false; no timestamped live result yet.

If unresolved, propose C2 separately: exact research `/config/web` GET API 2023-12-01, 20 seconds, one request/no retries; project only resource identity, publicNetworkAccess if exposed, ipSecurityRestrictionsDefaultAction and bounded rule counts/actions/priority ranges, scm-use-main flag only if diagnostically necessary. No IP/subnet/header restriction values, names/descriptions, full rules, settings or unrelated configuration. Exact field availability and query must be confirmed offline before an approval request; missing fields remain UNKNOWN, not unrestricted. This proposal is NOT authorized/executed. No diagnostic-log enablement; existing log queries would need a separate exact UTC-window/cardinality/schema proposal, and unavailable logs mean NOT_AVAILABLE.

Only the existing two evidence documents changed; UTF-8/LF/no BOM plus git diff --check verified. No tests rerun for documentation; no new CI/runtime claim. `AUTHENTIC_DATA_NOT_IMPORTED=False` locally; `LOCAL_CHART_VERIFIED=True` historically; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR remains OPEN/UNMERGED. Smallest next decision: approve or decline the exact one-shot C1 request above.

## Phase C1 - approved single request, timeout before headers

Owner explicitly approved the exact C1 headers-only request. Observed UTC `2026-11-09T00:17:03.572354+00:00`; one worker, at most one anonymous research `/health` GET. Stages WORKER_STARTED -> REQUEST_OPEN -> FAILED/TIMEOUT. Controller outcome OUTER_DEADLINE_15_SECONDS, measured elapsed 15.096 seconds including termination/collection overhead. The socket timeout was 14 seconds; worker reported TIMEOUT before the controller's 15-second limit ended the worker, so it did not complete normally. No HTTP status/content type/header-presence booleans or validated TLS outcome were captured. TLS verification was configured, but successful validation is UNKNOWN. Application response-body bytes read **0**. No raw error/body/header material recorded.

| C1 field | Actual result |
| --- | --- |
| Approved / executed | Yes / once |
| Request stage | REQUEST_OPEN; TIMEOUT |
| Completion | Outer deadline enforced; no normal completion |
| HTTP/TLS/header classification | UNKNOWN; no response headers captured |
| Origin | UNKNOWN; earlier 403 remains ORIGIN_UNDETERMINED |
| Body reads | Zero |

No second request, redirect, retry, UI/production probe, ARM/CLI/quota/log/registry/SCM access, credential/private-data inspection, Docker operation, mutation, restart or deployment. This timeout does not identify the network/TLS/platform/container failure stage within request-open, and does not erase or explain the earlier 403. No public-readiness conclusion follows.

PR/clean local head rechecked at expected 7efd6d16cbfee280f596eab1e977fd988c65d873, OPEN, before publishing this evidence. Only two documentation paths changed; UTF-8/LF/no BOM and git diff --check passed. No tests rerun for this documentation-only record.

Smallest proposed C2 operation, NOT_AUTHORIZED/NOT_RUN: one existing authenticated ARM GET to exact research `/config/web`, API 2023-12-01, 20-second outer cap/no retries. In-command projection only resource id, main-site ipSecurityRestrictionsDefaultAction, total main-site rule count and counts of Allow/Deny actions (no rule values, names, addresses, headers, descriptions or startup/image/settings). Null/missing or over-256 rule cardinality -> UNKNOWN/stop; allow only exact identity, action enum Allow/Deny/UNSPECIFIED and integer counts. No live logs, historical window or retention applicable: current config snapshot only, not proof of restrictions at the earlier request time and not proof of causation. A Deny/default-restriction result supports only a possible access-policy rejection; absence of restrictions cannot prove app origin. No C2 call follows automatically; owner must separately approve the exact query.

`AUTHENTIC_DATA_NOT_IMPORTED=False` locally; `LOCAL_CHART_VERIFIED=True` historically; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR remains OPEN/UNMERGED.

## Phase C2 - separately authorized restriction-summary read

Owner approved the proposed single projected research config/web read. Observed UTC `2026-11-09T00:19:22.051367+00:00`: exactly one ARM GET to the exact research config/web, API 2023-12-01, 20-second subprocess cap/no retries. Existing authenticated CLI, no login/token retrieval/settings access. In-command projection limited to identity, default action, rule-array known flag and total/Allow/Deny counts. Exact identity MATCH; rule array known, cardinality within 256; counts consistent.

| Field | Observed |
| --- | --- |
| Default action | UNSPECIFIED (null) |
| Total rules | 1 |
| Allow rules | 1 |
| Deny rules | 0 |

No rule addresses, names, descriptions, priorities, header filters, full config, image/startup, credentials or error bodies were emitted or persisted. No request to a serving URL, quota/log/registry/SCM/Docker/data operation, mutation or restart. Only the one approved read occurred.

**Interpretation:** counts alone cannot establish the scope of the Allow rule or effective unmatched-request behavior when the explicit default is unspecified. Therefore this does not exclude access restrictions or establish why the prior request returned 403. Origin remains UNDETERMINED; current configuration is not a historical request trace. No additional probe/query is inferred.

PR #445 OPEN at matching clean head 1e46cb3d055c9745ef2397bd841f08dfa8fd4b7a before evidence publication. Same two documentation paths only, UTF-8/LF/no BOM and diff checks passed; no tests rerun. Smallest possible next scope is a separately approved boolean-only classification of the existing main-site rule as an unconditional universal Allow versus a scoped/conditional rule, without exposing address/header values; exact projection and semantics must be reviewed before that read. Public-network/auth/platform lifecycle or artifact questions remain outside this completed scope.

`AUTHENTIC_DATA_NOT_IMPORTED=False` locally; `LOCAL_CHART_VERIFIED=True` historically; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR stays OPEN/UNMERGED.

## Separately authorized boolean-only main-site rule classification

Owner explicitly approved the proposed single boolean-only follow-up. Observed UTC `2026-11-09T00:38:18.322663+00:00`. One exact research config/web ARM GET, API 2023-12-01, 20-second cap, no retries. CLI in-command projection returned booleans only: exact identity match, exactly one rule, Allow action, universal address/Any sentinel, default tag, no header filters, no subnet mask, no VNet subnet and no subnet/VNet traffic-tag conditions. **Every projected check true.** No addresses, raw header/config/rule values, names, descriptions, image/startup, settings or credentials emitted/persisted.

Classification: `UNCONDITIONAL_UNIVERSAL_ALLOW_FOR_PROJECTED_FIELDS`. The current main-site IP rule does not support attributing the earlier 403 to a restrictive rule within these checked fields. This is not an exhaustive platform-policy audit, historical trace or deployed-runtime proof. Public-network access, platform authentication/front-end behavior, actual configured artifact and current serving remain unverified; origin still UNDETERMINED. No HTTP retry, auth/network-config read, log/detector/registry/SCM operation or mutation occurred. Do not infer another live authorization from this result.

PR #445 OPEN at matching clean head 2dd7650ffaa3bfaf6bf6753c3335fbd9123dc8ff before publication. Same two documentation paths, UTF-8/LF/no BOM and diff checks passed; no test reruns. Next concrete decision may separately scope nonsecret public-network and platform-auth policy enums for the exact research app, with no credentials/provider configuration or raw objects; the exact query must be proposed before execution. No further diagnostic is run automatically.

`AUTHENTIC_DATA_NOT_IMPORTED=False` locally; `LOCAL_CHART_VERIFIED=True` historically; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR remains OPEN/UNMERGED.

## Separately authorized public-network/platform-auth policy checks

Owner approved the proposed nonsecret research-only policy scope. Before live access, inspected installed SDK request-builder constants offline: `build_web_apps_get_auth_settings_v2_request` uses GET `/sites/{name}/config/authsettingsV2/list`; no speculative endpoint request was made. Observed UTC `2026-11-09T00:42:41.466102+00:00`: exactly two projected ARM GETs, site first then that authsettingsV2/list endpoint, API 2023-12-01, 20 seconds each/no retries. Both identities MATCH; only id plus allowlisted policy fields captured, not identity-provider configuration, credentials or complete objects.

| Policy field | Observed |
| --- | --- |
| Site publicNetworkAccess | null / UNSPECIFIED |
| Platform authentication enabled | false |
| Require authentication | null / UNSPECIFIED |
| Unauthenticated client action | null / UNSPECIFIED |
| Auth HTTP settings requireHttps | null / UNSPECIFIED |

Platform authentication is disabled in this current snapshot. Null publicNetworkAccess is not an explicit Enabled/Disabled observation; null subordinate auth fields are not inferred defaults. Auth requireHttps null does not contradict the separately observed site HTTPS-only flag; these are different settings. This evidence does not support enabled platform authentication as the observed 403 cause. Together with the earlier universal-Allow rule and within-limit quota snapshot, it still does not establish response origin or explain timeout/startup behavior. Current deployed artifact/routing/lifecycle remain unverified. No health retry, UI/production access, provider/data/credential inspection, log/registry/Docker call, mutation or restart occurred.

PR #445 OPEN at matching clean head d3589ce0b867ece62e4ddaab98e0405134fbadb7 before evidence publication. Two evidence documents only; UTF-8/LF/no BOM and diff checks passed, no tests rerun. Smallest useful next decision is a separately bounded read of the research configured image reference (safe reference grammar and identity validation, no startup/full config), before attributing behavior to merged source; no renewed B2 authority assumed. Registry content resolution remains separate and UNKNOWN. No further operation executed.

`AUTHENTIC_DATA_NOT_IMPORTED=False` locally; `LOCAL_CHART_VERIFIED=True` historically; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR remains OPEN/UNMERGED.

## Separately authorized research configured-image read

Owner approved the proposed single research image-reference read. Observed UTC `2026-11-09T00:48:00.635308+00:00`: one research config/web ARM GET, API 2023-12-01, 20-second cap/no retries; CLI query selected only resource id and linuxFxVersion, held in memory, exact id and credential-free reference grammar validated before output. Identity MATCH; classic configured digest reference in the existing GHCR application repository: `sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f`. No mutable tag. No startup/settings/full config, credentials, registry call, manifest/blob/image download, Docker or HTTP health probe.

Offline cross-check: this exact digest is hardcoded in `wp36_research_deploy.py:23` and recorded as the old WP35/WP37 assigned image in the WP37 preflight. It predates the later nullable-volume changes and WP57 accepted local image pair. **Configured artifact is still the older recorded artifact, not qualified as the accepted local chart implementation.** This is management configuration evidence, not proof of successful pull, running process/image, current registry content, architecture/platform resolution or request attribution. Configured digest string is not independently verified OCI content, and local Docker image IDs are not registry manifest digests. Historical index/platform mapping is not a fresh readback. The 403/timeout origin remains UNKNOWN; do not attribute it to artifact age alone.

PR #445 OPEN at matching clean head a094c2176e11994c271b640497a489b931190d74 before publication. Only same two evidence documents; UTF-8/LF/no BOM and diff checks passed; no test reruns. No Azure mutation/restart/deployment/provider/data/auth operation occurred. Staging UI remains unprobed; earlier B2 two-app/registry scope is not revived.

Next decision should separate remediation planning from incident attribution: prepare an offline exact-artifact promotion/rollback and cloud-state availability proposal for the accepted pair, or authorize a narrowly specified existing startup diagnostic if the old runtime's failure cause is still required. Neither image publication/deployment nor log access is authorized here. Cloud authentic state cannot be assumed or uploaded/imported under WP58. Public readiness remains blocked even if an artifact is later changed.

`AUTHENTIC_DATA_NOT_IMPORTED=False` locally; `LOCAL_CHART_VERIFIED=True` historically; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR remains OPEN/UNMERGED.

## Phase D - OFFLINE ONLY - 2026-11-09 UTC

### Baseline, source pair and actual scope

Verified clean existing branch and live PR #445 OPEN at expected `aa169c42b550f791ea1e10f7906b2312e6f071f5`, base main `4a6bc2b901d6f6ff0081dad4597064017acb86f9`. Git ancestry confirms #444 squash cfe60efc4d8317dbf3c27df0ecd126117e19f31e precedes #443 squash/base. No owner checkout/untracked edits. Only GitHub documentation workflow network activity is permitted/executed in D; no Azure/registry/HTTP/Docker/browser/provider/secret/private-data operation.

| Role | Accepted historical local identity | Source/startup mapping | Promotion evidence still needed |
| --- | --- | --- | --- |
| UI | local image ID sha256:3270ce6ec33ba8cb7da27738ea6d9dc27a811a3270e7ecdef83f32e757a83ae1 | WP57 built #444 source be6e7a21ce04edf5fa45883383bfaf385228afbd; merged correction in cfe60ef. Dedicated research-chart.Dockerfile; UID12002; port8501; Streamlit realtime_financial_visualization.py, AIQ_RESEARCH_CHART=1 | New build source/input hashes and real registry manifest/platform digests; newly built bytes are not assumed identical to WP57 |
| API | local image ID sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028 | WP55 original retained in WP57, not the unused WP57 rebuilt API; research-api.Dockerfile, framework-dependent Worker, UID1654, bootstrap serve, port52731 | Retained image-to-source provenance not re-inspected; merged source is an inspectable build candidate, not a claim that both active images were rebuilt together |
| Azure research | configured digest sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f | Older WP35/WP37 configured artifact; hardcoded in old helper | Not running-image/content proof or accepted local pair; 403 cause still unknown |

Historical acceptance is in prompters/release-1.15-wp57-corrected-ui-local-browser-qualification.md and WP55 note. Local image IDs identify image configuration objects, not OCI manifest/index digests. Do not compare their unequal hashes as proof of a source difference. Current main contains accepted source functionality; byte-identical reproducibility of .NET publication/image timestamps is not established.

Source inspected: both dedicated Dockerfiles; research-bootstrap.sh; wp39_local_history.py prepare; ResearchSnapshotEndpoint.cs; ResearchSnapshotSource.cs; ResearchStateInitialization.cs; ResearchStorageBoundary.cs; snapshot contracts/client; old wp36 helper/tests. Root public entrypoint defaults to synthetic public_preview_app; do not toggle it into the research UI. Two separate single-process apps stay mandatory; no supervisor/tunnel/new resources.

### Source-only artifact preparation and publication proposal - NOT EXECUTED

1. Pin merged source SHA and every build input. Preserve accepted local runtime/data. A fresh secret-free staging context copies only reviewed UI source allowlist/favicon, prebuilt Linux/amd64 framework-dependent Worker publication, and bootstrap. Existing prepare is a reference, not the public release command: it also creates local private configuration and publishes without an explicit Linux RID in the shown command. Do not run whole Prepare merely to publish; explicitly qualify Linux native SQLite assets and their ABI before release. Pinned ASP.NET/Python bases are recorded in the dedicated Dockerfiles; qualified_python named context currently references mutable aiq-c2b36:candidate, whose inspected immutable ID must be pinned and qualified under later Docker authority. Do not invent its ID or download missing inputs.
2. Proposed build uses reviewed Dockerfiles, linux/amd64, --pull=false and --network=none, offline cached dependencies only. Verify allowlisted paths and content before build; output source/build/input hashes and role association. Missing cached SDK/base/wheel/native asset stops, not silent restore/pull/install. Rebuild is a new candidate requiring targeted offline Linux and bounded browser qualification; WP57 acceptance alone does not transfer to new bytes. Deterministic input closure is required; bit-reproducible output is not claimed without comparison.
3. Exclude from context and every final layer: provider observations/raw payload/export, SQLite/WAL/SHM/journal/witness/private state, service/provider/HMAC secrets, env/private manifests, TLS PFX/PEM/private keys, mounts, owner home/cache/logs and test fixtures. Never copy then delete secrets in a later layer. Review the public source allowlist (including legacy adapter source) for files only; no runtime provider key. Image-content inspection requires separate approval and reports only exclusion results, not private values. Public images are source/runtime only, never datasets.
4. Only after content/compatibility checks, request separate exact registry tag/push authority for each candidate, no implicit login/token discovery. Publish source-and-role-specific references in existing repository, then separately bounded exact-reference manifest readback. Hash OCI bytes and reconcile registry digest, platform index/runtime-manifest digest and linux/amd64 config; verify startup/user/ports/source provenance. Record a compatible immutable pair. Stop if private/credential-dependent resolution lacks an approved channel. No enumeration/pull/publication occurs now.

Minimal future deployment-tool implementation proposal, NOT authorized: existing `eng/azure-cli/r1.15-qualification/wp36_research_deploy.py`, `eng/docker/release-1.15/Invoke-Wp36ResearchDeploy.ps1`, and `tests/runtime/release_1_15/test_wp36_research_deploy.py`. Replace hardcoded old single-research IMAGE/HEALTH and empty-Code-mode/HTTPS-false assumptions with explicit exact role/digest allowlist, current baseline drift validation, private per-key reversible deltas, API-first/UI-second state machine and one bounded approved rollback. Preserve old behavior through explicit version/action separation if needed. Tests: reject unapproved roles/digests/production, baseline absence/drift/unknown outcome, redaction, unrelated setting preservation, API failure blocks UI, partial timeout rollback, rollback failure stop, no automatic imports/retries. These are exact candidate paths for later owner review, not a blanket mutation allowlist or code edit. New paired helper paths, if needed, need their own literal approval.

### Cloud dataset feasibility - the priority blocker

`research-bootstrap.sh` serve execs the API; no automatic acquisition/import. operator-stdin initialization requires a previously delivered private export plus private config and is explicitly outside D/WP58. ResearchStateInitialization validates/imports completed observations and activates transform state; initialization alone is not proof of committed rows. Endpoint OpenExistingStore returns null for missing research.sqlite, never seeds; invalid storage/state can instead prevent startup. A missing store may still yield /health Alive while snapshots fail DATASET_NOT_READY. ResearchSnapshotSource uses protected reader/transform configuration and serializes transformed output; /health proves neither auth nor dataset.

ResearchStorageBoundary permits only exact staging UID1654/private directory /tmp/aiq-research on writable container-root overlay, with exact scope/site/host/instance/mount guards and storage disabled. It deliberately excludes durable/network/bind alternatives. Process restart may retain the same writable layer, but container replacement/reimage/recycle can discard it: neither restart survival nor Azure durability is guaranteed. Promoting an API image therefore threatens preexisting cloud state. Rollback to an old image restores code/config only, not lost ephemeral data. No persistent state or shared F1 capacity guarantee follows from local volumes.

| Cloud state | Honest user-visible outcome | Next separately approvable evidence | Stop / promotion consequence |
| --- | --- | --- | --- |
| Verified initialized | Authentic transformed chart only while both protected datasets/API/auth remain available; last-known status must be truthful | Bounded authenticated two-symbol check below; repeat after any separately approved lifecycle change | Do not replace API until state survival/recovery is resolved; prechange readiness cannot prove postchange readiness |
| Verified missing | Dataset unavailable, no synthetic/zero-volume fallback | Existing API status may establish absence without private DB inspection; no import follows | STOP: current boundary supplies no authorized way to populate cloud state |
| Unverified (current) | No claim of working authentic public chart | Owner-authorized service channel + two transformed snapshot checks, only after serving/auth preconditions | STOP promotion; local data is not evidence of cloud data |
| Lost after restart/replacement | Unavailable or explicitly last-known/disconnected UI; never silently stale-as-live | Separately approved lifecycle then bounded status checks; no auto-recovery acquisition | STOP; image/config rollback cannot recover lost observations; do not loop restarts |

**Feasibility conclusion:** a temporary demo can work only if authorized initialized cloud state already exists and survives the required process/container lifetime. No such evidence exists. If state is absent/lost, producing a working authentic public historical chart is impossible under the current no-acquire/no-import/no-upload/no-migration boundary. Restart-safe availability cannot be promised on the accepted ephemeral-only store. An image update does not solve this. Required owner architectural decision: retain the boundary and keep authentic chart local/public delivery blocked, OR separately amend a narrowly scoped cloud initialization/recovery and compatible storage/lifecycle contract. That would be a new authorization/architecture task, not an action hidden in promotion; do not add resources/paid storage, raw upload or new provider under D. No new licensing gate.

Future readiness proposal, NOT executed: strict system-trust HTTPS POST to exact research `/v1/research/snapshot`; maximum two requests, BTC then ETH only after BTC passes, 5 seconds each/10 total, no retries/redirects/proxy credential inheritance. JSON {schemaVersion:"1.0",instrument:<symbol>,requestEpoch:"1",knownIdentity:null}; X-API-Key through an independently owner-approved private transient channel, never CLI args/log/file/browser. No channel inspection now; clear newly introduced variable in finally, preserve unrelated environment. Read at most contract 4 MiB in memory; use existing strict validator, publish only symbol, status/error category, count, generation/version coherence booleans, Twelve Data provenance/educational transform checks, gap count, analytics alignment, volumeAvailable=false/null semantics, SMA20 row/non-null/warmup counts (1000/981/19 where contiguous). Never publish prices, raw payload, timestamps/generation IDs/keys/hashes. A full snapshot can inspect transformed values in memory without publishing them; it is not raw-observation export. Wrong/missing key or 429/503, malformed identity/provenance, gaps/count mismatch, stale/invalid transform, timeout or any uncertainty stops before ETH. Fresh health success is a separate prerequisite; this is no permission to repeat probes. Unauthorized/wrong-key negative checks need their own bounded authority. Success establishes only state at that time, not durability.

### Dependency-ordered staging promotion and reversible rollback proposal

1. Resolve cloud-state architecture and lifecycle FIRST. Confirm whether existing authorized state can support a code replacement; if not, stop before build/publication expense. If owner preserves no-state-transfer/no-init boundary, do not manufacture a deployment path.
2. Establish compatible source-only OCI pair and content exclusions; authenticate service configuration through separately approved owner channel. Neither old helper's fixed digest nor local IDs are promotion refs. Resolve actual serving/403 enough to bound a change; do not assume an old-image replacement fixes it.
3. Before writes, capture a separately authorized **private** exact per-app rollback baseline: exact identity/plan/region/state, image reference and known manifest identity, startup, HTTPS-only, logging configuration and narrowly enumerated changed setting values/existence plus slot metadata. Reconcile immediately against live state. No public baseline paths or values; keys protected in approved private owner storage, no body/CLI transcript. Read settings only through separately approved private flow; otherwise stop. Preserve every unrelated setting without enumeration in public evidence. If unsupported safe per-key mutation/preservation cannot be verified, use reviewed owner portal deltas, not full-settings replacement. Unknown previous secret cannot be overwritten. Baseline capture is a proposal, not performed.
4. API settings delta proposal, only if baseline differs: WEBSITES_PORT=52731, AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0, AIQ_RESEARCH_BOOTSTRAP_MODE=serve, AIQ_RESEARCH_TRANSPORT=azure-app-service-http, AIQ_RESEARCH_PLATFORM_HTTPS_ONLY=1, AIQ_RESEARCH_PRIVATE_DIRECTORY=/tmp/aiq-research, AIQ_RESEARCH_STORAGE_SCOPE=azure-f1-staging-ephemeral, AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING=1, AIQ_RESEARCH_EXPECTED_MOUNT_PATH=/, WEBSITES_ENABLE_APP_SERVICE_STORAGE=false; server-only AIQ_RESEARCH_API_KEY must match approved UI channel without disclosure. Exact storage guard remains. Immutable API digest/empty startup override, site HTTPS-only true. No initializer/import file/provider key/local CA/PFX in Azure serving proposal.
5. UI delta proposal: dedicated research-chart immutable digest/empty startup; WEBSITES_PORT=8501; AIQ_RESEARCH_BASE_URL=https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net; AIQ_RESEARCH_API_KEY server-only; AIQ_RESEARCH_CHART=1 is already baked into dedicated UI image, not a reason to alter synthetic root image. Site HTTPS-only true; normal public CA trust, no local CA/Docker DNS/provider secret. Preserve unrelated settings, production, logging/plan/network/auth controls.
6. One future explicitly approved apply window: maximum 10 minutes for the pair, API first with its individually approved settings/image delta/restart (at most one), readback plus at most 180 seconds bounded readiness observation; proceed to UI only after authenticated dataset passes. UI then its approved delta/restart (at most one), configuration and bounded actual UI acceptance. Request budget and exact per-write commands must be fixed in executable plan before live approval, not invented during Apply. Root cause/quota/timeout/identity drift/unknown outcome stops progression. Configured digest readback is insufficient: require independently attributable running artifact/version/manifest evidence; if existing diagnostics cannot supply it, record UNKNOWN and stop instead of adding a public diagnostic endpoint silently.
7. One preapproved rollback sequence on any failure/uncertain write, in reverse touched-app order UI then API; restore only verified previous image/startup/HTTPS and changed keys, delete only newly added keys whose prior absence is proven, restore slot metadata. No settings collection overwrite; no new restart/retry loop. Include rollback restarts and request budget explicitly in that later approval. If baseline incomplete, quota blocks rollback, or rollback result uncertain, stop/manual owner recovery; never claim successful rollback. Restored old research code may still be unhealthy and cannot restore ephemeral DB. Old UI is preserved until API/data acceptance; retain local WP57 runtime/images/data untouched.
8. Actual public acceptance, separately approved: exact staging HTTPS UI in real browser BTC->ETH->BTC, 1000 authentic candles each, C# SMA20 981 non-null/19 hidden warmup where contiguous, provenance/educational transform, null unavailable volume/no bars, original controls/zoom behavior, two isolated sessions, no browser keys/provider/raw material, no failed assets/JS errors. Only approved sanitized counts/state evidence, no private screenshots/prices. Bounded F1 quota/lifecycle observations, explicit caps and no headroom/cost guarantee. Separate restart/loss check must prove truthful no-data handling and establish limitations; temporary success is not restart-safe availability or production acceptance. Production remains excluded.

### Outcome matrix and next owner decision

| Outcome | Status | Meaning |
| --- | --- | --- |
| SOURCE_PAIR_IDENTIFIED | TRUE, source-level/historical mapping | Dedicated merged source and accepted mixed local pair mapped; not new binary equivalence |
| REGISTRY_DIGEST_PAIR_VERIFIED | FALSE / UNKNOWN | No current compatible pair content readback |
| CLOUD_DATASET_READY | UNKNOWN | No authorized cloud state proof; local import only |
| ROLLBACK_BASELINE_VERIFIED | FALSE | No private paired reversible baseline |
| HTTP_403_ORIGIN_KNOWN | FALSE | Prior rejection/timeouts unexplained |
| PUBLIC_RUNTIME_VERIFIED | False | No public API/browser acceptance |
| liveAttemptReady | false | Data, artifacts, baseline and serving prerequisites incomplete |

**One prioritized next decision (architectural, not a live operation):** choose whether to keep the no-cloud-initialization/recovery boundary and retain local-only authentic delivery, or authorize a separate offline amendment defining a secure cloud-state lifecycle compatible with the existing F1/no-new-resource constraint. No calls, methods, timeouts or rollback writes are associated with this decision: operational request count zero; no secret/data access. If amended, exact implementation paths, a state-source/delivery policy and restart recovery must be reviewed before any operational approval. No broad deployment request, state transfer or provider acquisition is included. This is the actual feasibility fork, not a fictional readiness gate.

### Validation and boundaries

Documentation only, same two paths. Executed Git status/rev-parse/merge-base ancestry, gh pr view, targeted Get-Content/rg source inspection; GitHub is the sole permitted network category. A wildcard rg attempt and a guessed transport filename lookup were corrected using tracked-file/source reads; no live fallback occurred. No tests/builds/restores/Docker/browser/Azure/HTTP/registry/provider/private-file/credential operations in D; tests not rerun because no code changed. Existing test evidence stays historical, not current full-suite/CI proof. git diff --check and separate UTF-8/LF/no BOM byte validation performed before commit. PR #445 remains OPEN/UNMERGED; no merge/deployment.

`AUTHENTIC_DATA_NOT_IMPORTED=False` (local historical import); `LOCAL_CHART_VERIFIED=True` (historical WP57); `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

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

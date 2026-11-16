# Release 1.15 ? Linux-first Docker-to-Azure deployment preparation

Date: 2026-11-15 (America/Sao_Paulo). Status: OFFLINE PREPARATION; NO LIVE EXECUTION.

**ZERO AZURE OPERATIONS.** No ARM/SCM/authentication, staging/production HTTP, registry read/write, Docker build/pull/run, provider, key/TLS/retained DB access or configuration mutation in this task. Ordinary GitHub repository reads/delivery and explicitly allowed local image metadata inspection only. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Baseline, provenance and decisions

Verified main `7ddcbe8f4bff4ea2fbfac8b9728b32051c5c690d` includes #455 squash `2ab325e9e0a1fbdb8b3a8a4266de6ee30f0a6fa0` and #456. New isolated branch preserves the owner's dirty checkout and all merged evidence. Existing Windows helper remains unchanged and live blocked; Linux does not require its Windows ACL qualification.

Sources: [WP61 local acceptance](release-1.15-wp61-post-merge-local-browser-qualification.md), [Phase G publication/deployment record](release-1.15-wp58-phase-g-staging-deployment-evidence.md), [WP62-B chronological evidence](release-1.15-wp62b-bounded-quota-event-observation.md), [API Dockerfile](../../../../container/release_1_15/research-api.Dockerfile), [UI Dockerfile](../../../../container/release_1_15/research-chart.Dockerfile), [bootstrap](../../../../container/release_1_15/research-bootstrap.sh), [API startup/health/auth](../../../../src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs), [HTTPS client](../../../../python/presentation/research_snapshot_client.py). Historical records are not current Azure configuration or runtime attestation.

| Role | Local immutable image identifier, freshly inspected without execution | Registry/runtime identity from recorded evidence only | Source and qualification |
| --- | --- | --- | --- |
| Locally accepted research API | sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028 | ghcr.io/samuel-santos-engineer/aiquanttradingresearch index1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028, linux/amd64 child2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 from Phase G. Matching safe local RepoDigest exists, not fresh registry readback. | Exact source commit UNKNOWN from inspected evidence; retain historical accepted bytes. WP61 used this API with imported data and local TLS. |
| Latest locally accepted chart UI | sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 | Current registry index/platform digest UNKNOWN; local RepoDigests empty. Older published WP57 UI3270ce6e is NOT this WP61 UI. | Built from merged source cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b;90 fake tests and final BTC/ETH local browser checks recorded. |
| Diagnostic API candidate, not latest full chart acceptance | sha256:153d2b1ae5e2b856c1ca502849c05fe04ec09950b453b7b8b7d4b47f1dc7205f | Recorded published index153d2b1ae5e2b856c1ca502849c05fe04ec09950b453b7b8b7d4b47f1dc7205f, linux/amd64 child d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c; config descriptor1a919126ffa680a4c588e92603d9a412fbd4f993008aabeb53a0f5c0661d7a53, config blob not independently read. | Source c4513aebfb69afac9ecbc7633e79e69cae9e950a;57 focused Windows tests and two fake Linux candidate smoke checks recorded. Adds stage diagnostics; no authentic dual-chart acceptance of this new image recorded. |

Local image inspection confirmed Linux/amd64, accepted/diagnostic API UID1654:1654, port52731, API_BOOTSTRAP; accepted UI UID12002:12002, port8501, STREAMLIT entrypoint contract. No environment/history/mount/private content shown. An attempted combined retained-container inspection was rejected by automatic approval review before execution; safe image-only inspection followed. No retained containers inspected or changed. Which images are currently running locally remains UNKNOWN; do not relabel historical acceptance as fresh runtime verification. No new image contents audit or registry authentication check in this task.

**Owner decision, not silently selected:** deploy previously accepted API2744... to preserve accepted behavior, or diagnostic API d336... to obtain future startup stages. The latter needs its own bounded acceptance, not assumed equality. Do not rebuild either merely to obtain a new tag. Latest UI3e323... should be audited/published unchanged under separate authority if that exact accepted artifact is chosen; do not substitute older published UI. No new registry tag invented here. Exact API source can be resolved only from original public build receipt/source manifest or later separately authorized image/source hash audit; local ID alone does not provide it.

## Minimal topology and source contract

Keep API and UI as **two separate existing Linux web apps** on the existing shared F1 plan, not one combined container, not Docker Compose or a new sidecar architecture. This matches accepted separate-container/source behavior and historical Azure resources. Current two-app hosting mode, F1 SKU, region, capacity and platform support MUST be verified by proposed metadata preflight, not asserted from this offline review. No new resource, paid plan or infrastructure proposed.

Exact historical targets:

- Subscription7f5eabb5-80f5-4369-88be-b14a6de763f1; resource group rg-aiq-r112-wp05-wus2-7f5eabb5; plan asp-aiq-r112-wp05-wus2-7f5eabb5, West US2/F1.
- Research aiqr115researchstagingwus27f5eabb5, HTTPS host aiqr115researchstagingwus27f5eabb5.azurewebsites.net.
- UI aiqr112wp05wus27f5eabb5, HTTPS host aiqr112wp05wus27f5eabb5.azurewebsites.net.
- Production aiqr112wp035ec325382770 explicitly excluded. Current quota/state/availability are UNKNOWN.

API image exposes52731 and binds0.0.0.0; classic App Service container port routing needs WEBSITES_PORT=52731. Empty startup preserves image bootstrap; default/explicit serve must be confirmed, not operator-stdin/diagnose. No Docker HEALTHCHECK/restart loop added. UI exposes8501 and Streamlit binds0.0.0.0; route WEBSITES_PORT=8501, empty startup preserves existing Streamlit flags and AIQ_RESEARCH_CHART=1 baked into image. Neither image must be given a one-shot initialization command as normal web startup.

API may use the existing azure-app-service-http opt-in: App Service terminates public TLS, API listens internal HTTP52731, site HTTPS-only must be independently true. The source requires exact WEBSITE identity/instance, AIQ_RESEARCH_PLATFORM_HTTPS_ONLY=1 and bind0.0.0.0; this assertion is not proof of ingress policy. UI server-side requests go to research **public HTTPS443**, never Docker DNS/local hostnames or internal port52731. Browser talks to UI, not the API key. Existing key must match privately; missing/malformed key can prevent API startup, mismatch can reject authenticated calls, neither is established here. No key creation/rotation/transfer/read authorized. UI AIQ_RESEARCH_BASE_URL must name exact research HTTPS host; local AIQ_RESEARCH_CA_FILE/localhost CA mounts must not be carried to Azure. Default public CA trust must work; never disable TLS validation. Existing provider keys are not needed to display already retained history.

No tracked .github/workflows publication jobs at this main; .github contains templates and root docker-compose.yml is empty. Publication/deployment are manual governed scripts, not evidence of automatic current Azure configuration. Historical Phase G anonymous registry manifest access passed for old artifacts only; latest UI registry availability/auth UNKNOWN. Before apply, verify exact chosen index/child/config chain and platform anonymous pull compatibility, or confirm pre-existing approved registry access without printing/getting credentials. A401/unknown pull policy stops; no new credentials/infrastructure silently introduced.

## State and readiness boundary

Images contain application/dependency files, not the retained observations/SQLite/key/TLS state. API source permits missing DB with a null snapshot source after directory/storage validation, so GET /health can be Alive while authenticated data requests return503. Existing storage scope azure-f1-staging-ephemeral, private directory /tmp/aiq-research, UID1654, expected writable root overlay, role0 and App Service storage false are strict source contracts, not durable storage. Do not mount or mutate retained local volumes or copy a DB into an image.

**Critical stop:** this task forbids new import/data transfer. Current Azure BTC/ETH state is UNKNOWN. If missing/lost/incompatible, F cannot pass; stop with honest dataset unavailable and request separate narrowly scoped owner disposition. Do not auto-acquire providers, auto-initialize, replace retention or invent a transport. Image rollback does not restore ephemeral state. Locally working data cannot be assumed present in Azure.

Linux private baseline primitive uses effective-UID ownership,0700 new directory, dirfd/O_NOFOLLOW and new0600 file checks before writing sensitive content; no inherited Windows requirements. Keep baseline/recovery/uncertainty ledger only in owner-private storage, never repo/CLI secret arguments/logs. Proposed payload files passed as @file paths; no values in command arguments. Preserve full original touched configuration privately, including logging and slot metadata where relevant, before changes; never overwrite the entire app-settings collection. No local private files were inspected/created by this task's default plan/tests on Windows.

## Bounded operator sequence ? PROPOSED, NOT EXECUTED

All commands below are future actions requiring separately scoped authority. Use Linux Bash argument arrays and existing CLI authentication, no eval/unquoted URLs, login, token dumps, new packages or credential exchange assumptions. Each management request:15-second wall timeout and combined1MiB stdout/stderr cap before storage; subprocess/session-group ownership and cleanup must be Linux-qualified first. `timeout` alone is NOT a byte-cap collector. The new helper emits/validates plans; it has no live adapter and must not be represented as a safe live runner yet. No polling, automatic retry or pagination. Abort first drift/unsafe data/auth/TLS/429/quota uncertainty/error. Offset-free reset time and freshness remain UNKNOWN, under prior approved amendment, not ignored or assigned UTC.

### A ? artifact verification

Chosen API decision first. Already recorded candidate/accepted API identities above are historical; latest accepted UI lacks verified publication. Separate proposed registry scope per artifact: at most3 readbacks (index, exact linux/amd64 child, config blob),10seconds/1MiB each30seconds total, hash exact bytes and validate config platform/user/port/entrypoint/source labels if present. Unknown source label remains UNKNOWN. No tag movement inferred as exact image. Existing registry protocol may require an anonymous token exchange; that extra request is NOT hidden inside the3-call budget and requires an explicit exact amendment before execution. No credential retrieval. No push/pull under this task; publishing latest UI requires exact owner-approved payload/repository/tag after exclusion audit. Record child digest only after fresh chain verification; no fabricated UI digest.

### B ? read-only Azure preflight

Exact first proposed operation is solely this GET, **not executed**:

```bash
quota_url='https://management.azure.com/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5/usages?api-version=2023-12-01'
quota_args=(az rest --method get --url "$quota_url" --only-show-errors --output json)
# Pass quota_args to a separately qualified 15s/1MiB Linux capture adapter; do not run it here.
```

Expected200 quota collection; exactly one integer WPStopRequests current/positive limit, Count unit; current<limit required. Missing/malformed/negative/boolean counter, quota exceeded, response failure or unsafe output stops. Record CPU values only if supplied. No probe or implicit reset/restart. Current safe quota for startup remains UNKNOWN until that future approved read.

Only after this first scope passes and separate remaining preflight authority: at most8 further management GETs,15seconds/1MiB each120seconds total: plan, API site+config/web+config/logs, UI site+config/web+config/logs, then final quota (9 GETs maximum across complete preflight). Exact URLs ROOT/serverfarms/<plan> and ROOT/sites/<app>[/config/web|/config/logs], API2023-12-01. Verify exact resource IDs, Linux classic Docker hosting, same plan/F1/freeSKU/WestUS2, state/enabled/usage/availability, HTTPS-only, configured immutable image and startup presence (no raw startup), health-check/alwaysOn classifications and touched logging fields. Slot metadata may not be completely represented by these reads; if incomplete for a proposed delta, stop rather than add calls. Config-only readback never attests running bytes. No raw logs/connection strings.

Settings-name-only verification is a DISTINCT proposed read-only operation: one POST per exact site's /config/appsettings/list,15seconds/1MiB each30seconds total, CLI projection keys(properties). Record only presence booleans for required names, never values. API names: WEBSITES_PORT, AIQ_RESEARCH_BOOTSTRAP_MODE, AIQ_RESEARCH_BIND_ADDRESS, AIQ_RESEARCH_TRANSPORT, AIQ_RESEARCH_PLATFORM_HTTPS_ONLY, AIQ_RESEARCH_PRIVATE_DIRECTORY, AIQ_RESEARCH_STORAGE_SCOPE, AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING, AIQ_RESEARCH_EXPECTED_MOUNT_PATH, WEBSITES_ENABLE_APP_SERVICE_STORAGE, AIQ_RESEARCH_API_KEY. UI: WEBSITES_PORT, AIQ_RESEARCH_BASE_URL, AIQ_RESEARCH_API_KEY; local CA setting presence must be reviewed, not echoed. Name presence does not prove value/equality. The ARM service returns a settings collection to CLI before projection; owner must approve this read footprint explicitly or use a private portal/operator check. It is not covered by permission for metadata GETs. No output of values, no automatic correction. Existing registry-auth name presence similarly cannot prove working pull access. If actual values need private validation, propose exact allowlisted equality checks separately; never silently broaden.

Preflight app health is separate, not inferred from Running: after quota/source routing pass, at most1 anonymous API GET /health, connect3s/total8s/body4KiB/no redirects/no cookies or credentials. Expected TLS-valid200 exact Alive; no retries.429/5xx/timeout/TLS/auth stops. UI shell read can wait until E. New health calls may cold-start/consume F1 capacity; no zero-cost claim.

### C ? API-only exact image change

Only after owner selects accepted API or diagnostic API and approves exact child digest plus baseline/rollback: capture complete reversible API image/startup/HTTPS/logging/context privately with0700/0600 checks; preserve unmodified settings/slot/image baseline and recovery JSON. Fresh config/web GET15seconds immediately before mutation must match baseline. One PATCH /sites/aiqr115researchstagingwus27f5eabb5/config/web?api-version=2023-12-01 with properties linuxFxVersion=DOCKER|<owner-selected repository@verified child digest>, appCommandLine empty. Use private @payload file; no copied secret settings.15seconds/one attempt. One GET readback15seconds. No explicit restart, HTTPS/settings/plan/log changes; PATCH itself may recycle/start. Failed/uncertain write marks touched in ledger; never repeat. Exact command template (non-executable until a verified private payload exists): `az rest --method patch --url "$api_web_url" --body @"$private_payload" --only-show-errors --output none` through bounded adapter. Both variables are owner-private paths/verified exact target, not new destinations.

### D ? bounded API startup observation

Prefer existing attributable platform startup event, not another passive unclassified detector. Proposed at most1 exact research startup-record read15seconds/1MiB; exact supported source must be established offline before live scope. No endpoint is invented here. It must provide timestamp/role/container identity and explicit actual pulled digest/config identity matching chosen child. Generic logs, configured image, API Alive or source hash alone do not establish this. **Current supported attribution channel UNKNOWN**; owner must decide whether to pursue one read-only channel qualification or accept a bounded diagnostic rollout with runtime identity UNKNOWN (no readiness claim), not waive full acceptance silently.

Then, only if separately authorized and quota permits, one post-apply GET /health as above; no startup polling. One post-observation quota GET15seconds. If quota exceeded, stop with ledger retained; do not trigger another restart/image write to force recovery. Capture only allowlisted literal startup stages if diagnostic image selected and an approved log channel exists. No automatic log enable/tail or publishing credentials; SCM authentication remains unresolved, and Windows helper stays live blocked.

### E ? UI deployment only after research acceptance

No UI apply while research process identity/health/authenticated BTC+ETH prerequisites fail or are UNKNOWN. Latest accepted UI publication first; exact runtime digest UNKNOWN so no executable UI PATCH payload yet. On qualified state and exact owner approval: private UI baseline, quota/drift checks; one UI config/web PATCH only to verified UI child+empty startup,15seconds; one readback15seconds; one anonymous HEAD / (or documented small route) connect3s/total8s/no body/no redirects. HTTP200 shell means reachable only, not chart ready. If API URL/shared key/public TLS wiring needs change, prepare separate exact nonsecret deltas/private same-key procedure, preserve unrelated settings and slot metadata, no full collection overwrite or real-key transfer under this task. Original chart/controls and research selection must remain; no root synthetic substitute.

### F ? bounded end-to-end acceptance, no import

Requires separately approved existing service-key channel and existing Azure state; no key echo/fingerprint/new generation. Two authenticated research POST /v1/research/snapshot requests maximum (BTC/USD then ETH/USD),5seconds each/10seconds total, response4MiB each in memory only, no redirects/retry. Use exact existing schema/session/request identities from supported consumer, no invented request JSON. Verify source/API identity, generation/epoch,1,000 completed contiguous1minute rows each if that is retained accepted dataset, null volume and C# SMA20 warmup19/computed981; no raw prices/timestamps/DTO or private identifiers in evidence. Coverage/freshness must be reported honestly. Non200,missing/incompatible data, unauthorized or uncertainty stops; no provider fallback/import or readiness toggle. Key equality cannot be inferred from setting names.

Only if both API checks pass, separately budget real browser: one30second session, exact UI URL, BTC->ETH->BTC once, no refresh loop, bounded active browser cleanup and explicit request/response byte caps enforced by a reviewed driver. Browser naturally triggers multiple assets/API calls; a simple two-HTTP-request authorization does not cover it. Qualify original candles/SMA/null-volume disclosure, WP59 navigation, WP60 inspection, WP61 comparison and isolation as required, never count clicks without resulting state. Record private evidence only; if cap cannot be enforced, do not launch. Browser driver implementation/live authority is not supplied here. PUBLIC_RUNTIME_VERIFIED remains false until separately reviewed actual full acceptance, not automatically changed by helper.

### G ? recovery, finite and quota-aware

Before writes, durable owner-private baseline/ledger bind exact target and chosen immutable delta. Append intention before each write (including uncertain timeout), retain known/unknown state and precise pending recovery. No privileged cleanup/volume deletion, no reset of DB, no bootstrap change to force import. If quota safe and baseline complete/drift absent, propose one image/startup rollback per touched app in reverse order UI then API; each PATCH15seconds + one readback15seconds, no retry/explicit restart. No appsettings/logging rollback unless those exact changes were separately approved and captured. Unknown/exhausted quota or stale/drifting baseline means manual recovery, candidate may remain configured. If logging changed under later explicit scope, restore exact captured payload once and verify full properties; mismatch stays UNKNOWN. Reverting config cannot restore ephemeral state, former application health or lost observations. Do not delete private recovery evidence after uncertain restoration. No automatic waiting for reset or loop.

## Offline preparation and validation

New [plan-only helper](../../../../eng/docker/release-1.15/linux_deployment_preparation.py) has no live adapter, no apply option and no default subprocess/network calls. It validates exact-target operation proposals, fake quota/drift snapshots, exact manifest bytes/platform selection, recovery uncertainty, and provides Linux-only restrictive-file and owned-session-group primitives. No Windows porting/privilege qualification gate. Linux process-group checks conservatively fail if a group remains (including unreaped descendant zombies); no silent success. This is NOT yet a qualified live capture runner and does not add new infrastructure.

Commands actually run on Windows:

```text
python -B -m unittest discover -s eng/docker/release-1.15 -p test_linux_deployment_preparation.py -v
python -B eng/docker/release-1.15/linux_deployment_preparation.py
```

14 tests discovered: **10 passed,4 native Linux tests skipped**, exit0. Default plan exit0; mocked Popen raises if any child attempted in plan test. Native Linux0700/0600 ownership, unsafe-directory rejection and process-group cleanup tests NOT RUN because host is Windows and no container/test-run authority inferred from image inspection. No claim of Linux runtime/private-baseline qualification. No dependencies installed, tests/builds/provider/Docker runtime operations beyond image inspect. Python compile-only syntax, UTF-8/LF/no BOM, Markdown links/tables, git diff --check and exact three-file scope validated before publication.

## Owner decisions and handoff

1. Confirm accepted API2744... versus diagnostic API d336... for any proposed staging change. Do not confuse newer diagnostic source with full locally accepted chart runtime.
2. Approve exact latest UI3e323... audit/publication scope only if that accepted image is intended; registry runtime digest currently UNKNOWN.
3. Resolve current F1/two-app/registry/wiring/state unknowns via the bounded staged preflight, not old quota readings.
4. Decide how to establish runtime-image attribution and handle missing Azure data without violating no-import boundary. No presently verified transport/channel assumed.

Proposed exact FIRST live action: one quota GET in B,15seconds/1MiB/no retry, stop on exhausted/malformed/unsafe/failed response; no follow-up implicitly authorized. All remaining A?G live/registry/data/auth/browser operations await exact separate scope. This plan aims to use the existing two images/resources with the smallest delta, but full historical chart acceptance is BLOCKED if Azure data is absent or runtime attribution remains UNKNOWN. No application fix, rebuild, new platform, licensing gate or deployment occurred. PR OPEN/UNMERGED for review.

**ZERO AZURE OPERATIONS.** `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

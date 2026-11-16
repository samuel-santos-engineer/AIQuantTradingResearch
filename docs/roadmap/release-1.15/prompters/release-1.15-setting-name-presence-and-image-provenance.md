# Release 1.15 ? setting-name presence and accepted-image provenance

Date: 2026-11-15 (America/Sao_Paulo). **PREPARATION ONLY ? ZERO AZURE OPERATIONS.** No registry calls/publication, image build/pull/retag, Docker or retained-runtime/data inspection, public probe, log/SCM access, key/credential retrieval, deployment or mutation. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Verified repository baseline

Fetched main `7ddcbe8f4bff4ea2fbfac8b9728b32051c5c690d`: merged #455 at2ab325e9e0a1fbdb8b3a8a4266de6ee30f0a6fa0 and #456 at7ddcbe8. PR #457 remained OPEN at7b3fdabf620619139ea26ac53dbabdb40bea3d6d; it is a separate Linux-first plan, unchanged by this PR. Isolated clean branch, no history rewrite. Windows helper unchanged; LIVE_ADAPTER_QUALIFIED=False. Linux work has no Windows ACL prerequisite.

Read [WP61 acceptance](release-1.15-wp61-post-merge-local-browser-qualification.md), [Phase G publication](release-1.15-wp58-phase-g-staging-deployment-evidence.md), [WP62 chronology](release-1.15-wp62b-bounded-quota-event-observation.md), [WP58 configured image evidence](../RELEASE_1.15_WP58_PUBLIC_RUNTIME_QUALIFICATION_PLAN.md), [Release1.14 publication](../../release-1.14/prompters/release-1.14-c2b-37-production-publication-evidence.md), [API Dockerfile](../../../../container/release_1_15/research-api.Dockerfile), [UI Dockerfile](../../../../container/release_1_15/research-chart.Dockerfile), [bootstrap](../../../../container/release_1_15/research-bootstrap.sh), [API startup](../../../../src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs), [storage guards](../../../../src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStorageBoundary.cs), [UI consumer](../../../../python/presentation/research_snapshot_client.py). No tracked GitHub publication workflow at this main; root docker-compose.yml empty, no deployment definition inferred.

## Artifact inventory ? kinds must not be equated

Exact repository in existing publication records: ghcr.io/samuel-santos-engineer/aiquanttradingresearch. Safe full digests below copied from authorized evidence, not abbreviated values expanded by guess. No fresh registry/runtime attestation in this task.

| Role and provenance | Local image ID / index | Linux/amd64 runtime child | Source / status |
| --- | --- | --- | --- |
| Accepted research API used in WP61 | sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028; Phase G records same OCI index | sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 | Exact source commit UNKNOWN from reviewed public receipt; immutable accepted bytes recorded. WP61 local authenticated BTC/ETH acceptance used this API. Phase G exact index/child/config chain verified historically, not freshly. |
| Accepted WP61 chart UI | sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 | UNKNOWN | Source cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b;90 fake tests/local browser acceptance recorded. Registry mapping/publication UNKNOWN; no recorded RepoDigest in earlier authorized image metadata. |
| Separately published WP62 diagnostic API | OCI index sha256:153d2b1ae5e2b856c1ca502849c05fe04ec09950b453b7b8b7d4b47f1dc7205f | sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c | Source c4513aebfb69afac9ecbc7633e79e69cae9e950a. Fixed stage diagnostics,57 focused tests/two fake container checks; not substituted for authentic dual-chart acceptance. |
| Last authorized configured Azure UI reference | sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07 | Descriptor kind/runtime mapping to latest WP61 UI not verified here | Full value in tracked WP58 E1 evidence and earlier Release1.14 publication; also prior authorized UI config read at2026-11-16T02:10:30Z (Nov15 23:10:30 Sao Paulo). CONFIGURED_ONLY. |

Tags wp58-wp57-api-1edebb818aff and wp62-startup-c4513ae are publication labels, not substitutes for verified immutable children. Old published WP57 UI3270ce6e is not latest accepted UI3e323b0d. Diagnostic config descriptor1a919126ffa680a4c588e92603d9a412fbd4f993008aabeb53a0f5c0661d7a53 was read from child manifest; config blob independently hashed UNKNOWN. Accepted API Phase G config descriptor c2ef542e32f10badc5f9b4f82ca1e67463b685cdf380ffee48f6a31d6aad8096.

**Configured UI versus accepted WP61 result: UNVERIFIED.** The full a06b... value MATCHES older synthetic publication evidence, not a verified WP61 index-to-child chain. Different numeric local/index/child values alone cannot prove binary mismatch. Strong chronology suggests the older showcase remains configured, but latest accepted UI runtime child is UNKNOWN, so do not claim cryptographically verified MATCH/MISMATCH. Smallest verification method under future explicit scope: resolve chosen accepted UI index and exact linux/amd64 child/config hashes, then compare the configured platform reference to that chain; no registry calls now. This does not prove which bytes actually run.

API contract: Linux/amd64, UID1654:1654,52731, /bin/sh /app/research-bootstrap.sh, serve execs Worker --research-snapshot-api. UI: Linux/amd64, UID12002:12002,8501, existing Streamlit entrypoint binds0.0.0.0. Local WP61 API had private data volume/local TLS and UI trusted CA; those mounts are not Azure transports. Cloud UI should use exact research public HTTPS hostname and standard CA trust; never copy local host paths, Docker DNS or private TLS into images. App Service platform HTTP API opt-in/storage guard configuration must remain; no automatic initialization/import. Data absent in Azure prevents chart readiness even if health is Alive.

## Accepted-versus-diagnostic API owner decision

| Choice | Recommendation and evidence | Remaining requirement |
| --- | --- | --- |
| Accepted API2744... | **Preferred for reproducing accepted chart behavior** once exact current registry/hosting compatibility is freshly verified. Recorded Phase G publication and WP61 use bind it to accepted1edebb... image. No rebuild/publication is indicated merely to reuse those bytes. | Existing registry availability/access, current Azure config, shared key, strict staging guards and existing data readiness UNKNOWN. Does not carry new startup-stage code. |
| Diagnostic APId336... | Only for a separately approved finite investigation, not silent production/acceptance replacement. Already recorded publication exists; no new push indicated. | Additional runtime/accepted-chart qualification, fresh quota/baseline and conditional rollback. Generic marker not image attestation. |
| Latest UI3e323... | Preserve exact accepted WP61 artifact; do not replace with old UI by similar tag. | Publication may be required; current mapping UNKNOWN. Exact audit/publish/readback authorization needed before new registry action, not granted here. |

No artifact selection, configuration write, new publication or deployment performed. Owner should select accepted API as default behavioral baseline, or explicitly choose diagnostic-only investigation; this document recommends, not silently chooses.

## Minimal name allowlist and meaning

R=required effective setting for proposed Azure mode; O=optional/defaulted or conditional; U=platform/effective-source uncertainty. PRESENT means name exists in the App Service settings collection only, including null/blank values; it does not prove validity, equality or effective process environment. ABSENT does not always mean missing at runtime (image ENV/platform injection may supply it). UNKNOWN means identity/schema/cap/collection failure. Do not output value hashes or secret identifiers.

| App | Name(s) | Classification/source |
| --- | --- | --- |
| API | WEBSITES_PORT | R for historical classic container routing52731; deployment evidence/wp36 setting contract, not read by C# itself. |
| API | AIQ_RESEARCH_PRIVATE_DIRECTORY, AIQ_RESEARCH_API_KEY | R; ResearchSnapshotEndpoint.RunAsync. Key presence cannot establish validity/equality. |
| API | AIQ_RESEARCH_TRANSPORT, AIQ_RESEARCH_PLATFORM_HTTPS_ONLY | R for azure-app-service-http opt-in; ResearchSnapshotTransport.FromSettings; source default transport is local HTTPS. |
| API | AIQ_RESEARCH_BIND_ADDRESS | R effective0.0.0.0; image ENV already supplies it, so explicit App Service name O if verified image supplies effective value. |
| API | AIQ_RESEARCH_STORAGE_SCOPE, AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING, AIQ_RESEARCH_EXPECTED_MOUNT_PATH, WEBSITES_ENABLE_APP_SERVICE_STORAGE | R for existing ephemeral staging exception; ResearchStorageBoundary.ValidateHost/ValidateLinux. Presence alone cannot attest kernel storage. |
| API | AIQ_RESEARCH_BOOTSTRAP_MODE | O; bootstrap defaults serve if absent. A present invalid/one-shot mode is not detected by names alone. |
| API | AIQ_RESEARCH_TLS_PFX, AIQ_RESEARCH_TLS_PASSWORD | O/conditional for local-https only; not required for Azure platform-HTTP mode. Names only, no paths/passwords/file reads. Unexpected presence needs private mode review, no automatic deletion. |
| UI | WEBSITES_PORT | R classic container routing8501; image expose/listen alone does not prove Azure routing. |
| UI | AIQ_RESEARCH_BASE_URL, AIQ_RESEARCH_API_KEY | R for research chart consumer; exact HTTPS server URL/shared-key equality remain private effective-value checks outside this scope. |
| UI | AIQ_RESEARCH_CHART | R effective research selection, O as app setting if baked image ENV supplies1. Old synthetic image may not honor it. |
| UI | AIQ_RESEARCH_CA_FILE | O local private CA override; no override expected for public Azure TLS/default trust, but name presence cannot prove wrong value; never read path/content. |

Platform-injected WEBSITE_SITE_NAME, WEBSITE_HOSTNAME, WEBSITE_INSTANCE_ID and WEBSITE_ROLE_INSTANCE_ID are required effective runtime guard inputs, **U for this app-settings list**; not included because list absence would not prove missing injection. No provider keys, registry-password/user names, HMAC/tuple secrets, import-file/diagnostic-nonce fields or unrelated settings collected. Registry access remains UNKNOWN, not inferred from key names. Minimal allowlist totals13 API names and5 UI names.

## Exact proposed reads ? PREPARED, NOT EXECUTED

No prior continuation is treated as this prompt's separate read approval. This prompt explicitly authorizes preparation only. Before live calls obtain explicit approval of these **two read-only POST list operations**, strictly sequential API then UI, one invocation each/no retries/pagination/fallback. POST is the ARM list method, not a settings write.

- Subscription7f5eabb5-80f5-4369-88be-b14a6de763f1/group rg-aiq-r112-wp05-wus2-7f5eabb5, API2023-12-01.
- Research endpoint: `https://management.azure.com/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/sites/aiqr115researchstagingwus27f5eabb5/config/appsettings/list?api-version=2023-12-01`.
- UI endpoint: `https://management.azure.com/subscriptions/7f5eabb5-80f5-4369-88be-b14a6de763f1/resourceGroups/rg-aiq-r112-wp05-wus2-7f5eabb5/providers/Microsoft.Web/sites/aiqr112wp05wus27f5eabb5/config/appsettings/list?api-version=2023-12-01`.
- Each15-second hard request deadline, combined response1MiB maximum;30seconds total request budget, explicitly bounded owned cleanup separately. Stop before UI on any first-read failure, timeout, cap, auth/TLS/error or unexpected resource ID/schema. No quota request, setting-value check, SCM/publishing-credential call or health probe added.
- Only validated resource identity in memory, and PRESENT/ABSENT/UNKNOWN for the18 allowlisted names may leave the parser. Raw service response contains values even though only names are needed; discard values immediately in memory, no raw file/log/stdout/debug. Do not enumerate/display unknown setting names. No response/caller/token headers, usernames, connection strings, secret identifiers or private paths output.

The offline parser validates returned /sites/<exact-app>/config/appsettings ID (not the /list operation suffix), JSON schema/duplicate fields, and only dictionary key membership. A names-only CLI projection could be constructed as a fixed object containing id plus contains(keys(properties), '<allowlisted-name>') booleans, but CLI --query runs AFTER its internal HTTP response is consumed. Bounded stdout does not prove a1MiB bound on the upstream settings response or immediate value discard inside the CLI. **CLI-only hard response cap has not been established; no live collector implemented or calls issued.** Do not run an unbounded az rest and truncate later. Safe alternative is a separately reviewed existing-auth streaming read-only collector with wire/body cap before materialization and in-memory parser, qualified offline first; otherwise an owner-private portal names-only check may be proposed. No login/token retrieval/export or unsupported transport assumed. This is a precise technical limitation, not a new licensing/governance gate. Separate read approval is necessary but not sufficient to waive these bounds.

Proposed command skeleton, NOT RUN and NOT a qualified runner: `az rest --method post --url "$exact_allowlisted_list_url" --query "$fixed_allowlisted_presence_projection" --only-show-errors --output json` through a future reviewed collector. Never use an unquoted batch-shell URL. This skeleton alone does not satisfy upstream capture bounds, so it is not an operator execution instruction.

## Offline artifact/parser delivery and tests

New [parser](../../../../eng/docker/release-1.15/setting_name_provenance.py) and [tests](../../../../eng/docker/release-1.15/test_setting_name_provenance.py), no live mode/adapter, no Azure SDK or credential imports. Default emits preparation metadata only. Tests use synthetic fake secrets and manifests, never real values; malformed/duplicate/wrong-ID/oversize responses become UNKNOWN, not ABSENT. Input-cap check is for supplied parser bytes, not proof of live wire bounds. Local/index-versus-child mapping requires exact hashes, platform cardinality and complete digest; missing mapping remains UNVERIFIED. No generic local-ID comparison treated as runtime proof.

Actually run on Windows:

```text
python -B -m unittest discover -s eng/docker/release-1.15 -p test_setting_name_provenance.py -v
python -B eng/docker/release-1.15/setting_name_provenance.py
```

**12 tests passed,0 failed/skipped**, exit0. Cases: no-value/unallowlisted-name leakage; present-null/blank is presence only; absent keys; malformed/duplicate/oversize response; wrong resource; incomplete digest; unknown provenance; known child comparison; index-to-child exact-byte verification; no network/CLI default; no live mode/production role. Default plan exit0. Python compile-only syntax, UTF-8/LF/no BOM, Markdown links/tables, git diff --check and three-file inventory validated before publication. No actual live/parser-data qualification claimed.

## Deployment and recovery outline ? still not authorized

Recommend accepted API once exact present compatibility is verified. Before any future change: fresh quota/identities/current image/startup/HTTPS/logging/nonsecret effective-name/value boundaries, private Linux0700/0600 baseline/uncertainty ledger, exact immutable child approval. One image/startup PATCH, readback and one bounded attributable startup observation, no retry/automatic restart. No UI apply until API identity/health/authenticated BTC+ETH pass. Missing cloud data stops; no import/volume deletion/provider fallback. UI exact registry digest must be verified first. One conditional rollback only with fresh quota/completed baseline/no drift; uncertainty may leave candidate configured/manual recovery. Image rollback cannot restore prior health or ephemeral data. Keep Windows helper blocked and unchanged; Linux permissions are independently qualified, not gated on Windows.

Next action: owner reviews accepted API recommendation, approves exact name-only footprint separately, and commissions only the minimal bounded collector qualification needed to honor that footprint. No deployment implied. PR OPEN/UNMERGED; **ZERO AZURE OPERATIONS**; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Offline Linux-compatible stream collector qualification ? 2026-11-15

Verified new main5217e1959a3a5ed5283c9874f2fd79b3d1a51ec3 contains merged #458. Concurrent #457 remains OPEN at7b3fdabf620619139ea26ac53dbabdb40bea3d6d against older main7ddcbe8; unchanged. New dedicated collector/test files and only this focused evidence addition; existing parser and all12 tests preserved byte-for-byte. No real Linux/Windows private baseline, token, key/TLS/data or retained runtime accessed. **ZERO AZURE/REGISTRY/DOCKER OPERATIONS** in this qualification task.

[Collector](../../../../eng/docker/release-1.15/setting_name_collector.py) defines an injected offline-only transport interface. Fixed frozen requests are POST exact API appsettings/list then exact UI appsettings/list, API2023-12-01,13/5 parser allowlists unchanged. No target override, live transport, HTTP client, CLI, Azure SDK or authentication import. CLI default prints preparation only; --apply/--execute fail LIVE_TRANSPORT_UNQUALIFIED before constructing any adapter. Existing Windows LIVE_ADAPTER_QUALIFIED=False untouched. No Windows ACL/Job prerequisite for Linux.

Fake interface contract: open(request, absolute deadline, cancellation predicate) returns a response with status, redirected, attempts, content_encoding, upstream_buffering and declared_length; read(maximum, deadline, cancellation) yields bytes; close(deadline) cancels/closes the response. Injected transport must declare qualification_only=True/network_enabled=False. These declarations are test contracts, not authentication or a proof that an arbitrary caller-supplied object cannot perform I/O. No real implementation is included or authorized.

The application reads at most4096bytes per call and never requests beyond remaining1MiB or known declared length. Declared length above1MiB rejects BEFORE reading. Unknown-length/chunked body hitting exactly1MiB rejects conservatively without reading a sentinel byte; no1MiB+1 overflow probe. A fake response returning more than requested fails STREAM_CONTRACT before accumulation. Complete bounded bytes are parsed only after the stream's declared end/EOF; values never leave parser, raw buffers cleared after close, nothing persisted. Duplicate/malformed/wrong-ID/truncated/error/cap uncertainty yields UNKNOWN for every allowlisted name of that role; it never becomes ABSENT. API failure prevents UI open. Known HTTP non200/redirect/multiple-attempt/encoded-body or unqualified upstream-buffering metadata rejects without reading. A valid API result may remain collected if UI later fails; no blanket two-role success claim.

Absolute deadlines use min(request start+15s,total start+30s). Cancellation checked before open/read and after return; cleanup uses an additional cooperative1second budget and reports CLEANUP_ERROR on failure/overrun. Synthetic clock tests prove accounting/shared budgets, early termination and no retries. **These are cooperative contracts, NOT proof of hard preemption of a blocking real read/open/close.** No threads/background HTTP workers or real client are started. A real transport could prebuffer beyond the application cap, decompress, redirect, refresh auth or retry before metadata becomes available. Those wire/client/auth/time-limit behaviors remain UNKNOWN and must be independently qualified with a reviewed existing-auth adapter before any live read. No claim that CLI stdout/query bounding fixes upstream buffering. Whole-body in-memory parser materialization is bounded by the bytes accumulated, not universal network/library memory. TLS/header/body/auth logging behavior unqualified. No live-mode release merely from command flags.

Actual Windows-host commands:

```text
python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -v
python -B eng/docker/release-1.15/setting_name_collector.py
```

Initial28-test run27passed/1failed due to an incorrect synthetic deadline expectation: after14s first request, the second per-request deadline is29s, not30s. Corrected expectation and added an explicit total-budget cap test plus cleanup/stream-contract cases; no production assertion weakened. Final **31 tests passed,0 failed/skipped**, exit0:19 collector tests plus12 unchanged parser tests. Default preparation exit0. Cases cover exact order/URLs/allowlists, chunked/oversize/declared/truncated bodies, over-returning stream, malformed/duplicate/wrong-ID JSON, absent versus UNKNOWN, redirect/retry/encoding rejection, cancellation, per-request/shared deadlines, failed/late cleanup, sanitized transport errors, no-network/CLI default and blocked live flags. Synthetic strings only; no genuine HTTP transport or Linux-network qualification performed/claimed. Python syntax, UTF-8/LF/no BOM, Markdown links/tables, exact three-path inventory and git diff --check validated before commit.

## WP61 UI chain review and minimum future registry footprint ? NOT EXECUTED

Tracked WP61 receipt supplies local image identifier sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69, Linux/amd64/UI12002/8501, source cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b and accepted browser evidence. Reviewed Phase G publication chain belongs to older WP57 UI3270ce6e, not this image. No exact latest UI index/child/config chain or published tag found in tracked receipts. Accepted API2744c4dc...75efc3 remains preferred behavioral baseline; diagnostic APId33626a9...65ba8c separate. No image or Azure configuration changed.

The full configured UI reference remains sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07; it is not proved equivalent to or a cryptographic mismatch of the WP61 local ID. Relationship **UNVERIFIED** until an exact chain is available. Local ID is not silently declared an OCI index.

One minimal separately approvable registry lookup footprint, no layer fetches, pulls, builds, tags or publication:

1. One anonymous metadata GET `https://ghcr.io/v2/samuel-santos-engineer/aiquanttradingresearch/manifests/sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`, Accept OCI/Docker manifest/index media types. This is explicitly a **candidate lookup by the known local identifier**, not proof that identifier names a published index. Require raw exact SHA256 equality and recognized schema/media type before further lookup.404 means no manifest found at this exact candidate reference, NOT proof the UI was never published elsewhere.401/auth challenge, redirect, wrong hash/schema or unknown platform stops; no token/login/fallback/tag search or pagination.
2. Only if first response is a valid index with one linux/amd64 child: one GET same repository /manifests/<exact returned child digest>, validate exact hash/schema. If first response is already a runtime manifest, skip this child GET; do not invent a child.
3. One GET same repository /blobs/<exact config descriptor digest> from the verified runtime manifest, validate hash/Linux/amd64/UID12002/port8501/entrypoint and source-revision label if supplied. Never print config ENV/history/raw content; source remains UNKNOWN if absent. A config redirect to a storage host is NOT followed under this scope. Metadata only, no layer/blob other than this specific config. Maximum3 GETs,10seconds/1MiB each30seconds total; fewer if direct runtime manifest or first failure. Hashes/platform/safe contract booleans only output. An anonymous-token challenge would need its own explicit amendment; no hidden auth requests.

This footprint may stop at candidate lookup; then smallest next action is a separate offline local OCI metadata export/receipt review to establish an actual index/config mapping, not unauthorized registry discovery or rebuilding. No registry request occurred here. Publication, if needed, requires separate exact payload approval.

## Next separately approvable scope

After review: (1) offline qualification of a genuine existing-auth streaming transport enforcing upstream byte/deadline/no-redirect/no-retry/cleanup limits without credential output; then explicit approval of the exact two ARM name-only POST reads already listed. This PR qualifies only the bounded injected-stream interface, not that real adapter. (2) Separate exact registry metadata lookup above if owner accepts candidate-reference uncertainty, or choose offline local chain resolution first. No authority to call either service now.

After names/provenance are genuinely established, preserve accepted-API-first and UI-second deployment: fresh quota/identity/wiring/state checks, exact immutable child and Linux private baseline/ledger, API52731/UI8501, public HTTPS/default trust and existing shared-key boundary; single conditional rollback only while quota/baseline permit. No setting changes, import, volume deletion or private key/TLS replacement. Unknown running-image identity/data readiness remains a stop, not automatic promotion. Windows helper remains blocked. PR OPEN/UNMERGED; PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.

# WP58 Phase G - accepted-image staging deployment evidence

Date: 2026-11-14 (America/Sao_Paulo). **PARTIAL DEPLOYMENT; API SERVING FAILED; UI NOT PROMOTED.** Owner explicitly authorized continuing until staging deployment, superseding repeated per-step approval pauses, within the exact two-app staging scope. No success claim from configuration alone. PR #445 and #446 remain OPEN/UNMERGED and unchanged by Phase G; #446 initializer was NOT substituted.

## Scope and artifacts

Targets only existing API `aiqr115researchstagingwus27f5eabb5` and UI `aiqr112wp05wus27f5eabb5`, existing group `rg-aiq-r112-wp05-wus2-7f5eabb5` / shared West US 2 Linux F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5`. Production excluded. No new Azure resource, paid upgrade, provider request, acquisition/import/state transfer, private bundle/DB/key read, service-key rotation or application-code change. Normal Azure/Docker authentication used without credential-store inspection or output.

| Role | Accepted local OCI index digest | Verified linux/amd64 runtime manifest | Config digest |
| --- | --- | --- | --- |
| UI | sha256:3270ce6ec33ba8cb7da27738ea6d9dc27a811a3270e7ecdef83f32e757a83ae1 | sha256:9eda4e3feaef1c019b552e0a7e75f0460e95ca55d610d8278d42f5b9193ba3f6 | sha256:b2fc7c0702c9931dfd968f9c8555a17716736cd4d2433cde49f8d0ad8b4e0af6 |
| API | sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028 | sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 | sha256:c2ef542e32f10badc5f9b4f82ca1e67463b685cdf380ffee48f6a31d6aad8096 |

Exact accepted bytes retained and published to the existing application GHCR repository, tags `wp58-wp57-ui-3270ce6ec33b` and `wp58-wp57-api-1edebb818aff`. No builds/pulls/substitutions. Local alias RepoDigests were not assumed to be registry manifests. Metadata confirms Linux/amd64, UID12002 UI/1654 API, ports8501/52731 and intended entrypoints. Archive root IDs proved to be OCI indexes rather than image-config hashes; complete selected index/runtime/config hash chain verified. Initial serializer and overly strict ID-equality checks failed locally, then were corrected; those failures did not prove missing/corrupt images.

Publication audit: all9 layers per image checked for suspected private application paths/state/TLS artifacts; UI11/API41 application-file occurrences; nonempty secret-named config variables and suspect history checked. UI chart/component/client hashes matched WP57 accepted source and API bootstrap matched merged source. An initial history keyword stop was the exact public ENTRYPOINT flag `--secrets.files=[]`; a strict source-token comparison established that benign exclusion, with no secret assignment. No suspected private file contents opened; archive inspection without extraction/execution, owned archives removed. This is exclusion evidence within checked paths/fields, not a universal proof of every possible embedded secret. No raw content/config/credential values published.

At UTC19:22:03 publication began. Docker tag/push plus exact registry index/runtime/config readback passed for both. Anonymous registry protocol read of both runtime manifests also passed; short-lived anonymous protocol token was held only in memory, not an owner credential or output. No registry access-policy changes or login.

## Private baseline and exact mutation

First baseline attempt failed to complete a logging projection; no write followed it. Fresh required baseline subsequently captured exact site/plan identities, original image/startup/HTTPS and nonsecret changed-setting context for both apps, in owner/SYSTEM-only private storage. No private location, original image name, secret value or fingerprint published. Logging/settings/slot metadata/plan/network controls remained unchanged and are outside rollback deltas. API nonsecret transport/storage guards and port52731 matched expected values. UI port8501 matched; research API URL match false and research service-key setting absent. Presence-only check, no key value retrieval. UI service wiring therefore remains outstanding; no claim of authenticated compatibility.

At UTC19:23:52 API apply began: exactly one PATCH of research config/web to the accepted **runtime** digest above with empty startup. No app-setting collection write, key/HTTPS/logging/slot/plan change or explicit restart. Azure image configuration itself can recycle/start containers. PATCH response accepted requested image/startup. First object-equality readback check disagreed; independent required-field readback confirmed exact resource, accepted image, empty startup and old image no longer configured. The original whole-object discrepancy was not attributed; do not call it actual image drift. No repeated PATCH.

Private baseline and apply ledger retained for rollback/review. No rollback executed: old artifact was already unqualified and code/config rollback cannot recover ephemeral data or prove quota recovery. Rollback remains a separately concrete bounded operation, not successful merely because baseline exists.

## Actual serving and post-apply diagnosis

One bounded180-second API startup observation, maximum9 requests/no redirects or explicit restarts, stopped at attempt8: first no response, then repeated TLS-validated503, finally TLS-validated403 at UTC19:26:26. Only fixed Alive classification permitted on200; no raw error bodies or secret headers. No Alive response observed. UI promotion/browser/API dataset checks NOT_RUN.

At UTC19:27:12, exact shared-plan usage projection and research metadata confirmed:

| Field | Observed |
| --- | --- |
| WPStopRequests | **26 / 15** |
| Daily CPU | 0 / 3600000 ms, as reported |
| Short-period CPU | 0 / 180000 ms, as reported |
| Research state / usage / availability | **QuotaExceeded / Exceeded / Normal** |
| Reported stop-counter nextResetTime | 2026-11-14T20:00:00 (no offset in response; no timezone/fix guarantee inferred) |

The post-apply403 coincides with this later quota-exceeded snapshot; counters do not identify why stops recur, prove every stop was caused by this deployment, or establish individual app crash cause. CPU zero is reported telemetry, not proof of no work/freshness. No additional health loop or restart against quota. Did not wait for or promise automatic reset recovery.

One bounded LinuxLogViewer detector classification for apply window yielded no attributable startup error/exception (one selected row; freshness/runtime binding unknown). A documented container-log read operation was unavailable; safe follow-up error classification returned NotFound. No logs enabled, broad dumps, raw stack paths, SCM credentials or shell access. No Azure IOException established. Local SDK-test IOException remains separate #446 evidence.

A single owned no-network/no-private-mount local container ran the accepted API's existing --research-storage-probe with **fake** staging markers and UID1654: exit0, Linux/host-opt-in/kernel/private-writable/accepted/cleanup flags alltrue. It created/removed only its test probe in the disposable container; --rm configured. This does not prove the Azure kernel/UID/env/state, API HTTP serving or dataset readiness. No real service key, data import or provider call. It narrows the issue without asserting the accepted image is universally Azure compatible.

## Outcome matrix and boundaries

| Gate | Result |
| --- | --- |
| Accepted local metadata / source-specific audit | PASS within checked scope |
| Publication / anonymous OCI readback | PASS, both exact accepted images |
| Private reversible image/startup baseline | CAPTURED, both apps; unrelated settings unchanged |
| API configured accepted immutable runtime image | PASS |
| Actual running API image attestation | UNKNOWN |
| API HTTP Alive | FAIL,503 then403 |
| Current F1 stop quota | EXCEEDED,26/15 at observation |
| UI accepted-image promotion / serving | NOT_RUN / unchanged from baseline |
| Cloud authentic dataset / authenticated snapshots | UNKNOWN / NOT_RUN |
| Browser chart acceptance | NOT_RUN |
| Rollback execution | NOT_RUN; private original config retained |

Deployment is NOT complete. Immediate external blocker is exceeded F1 stop quota; recurring stop/startup root cause and unavailable attributable diagnostics remain unresolved. Do not advance UI while API is not serving or add a paid tier/new service. Resume only with current quota/serving evidence and a bounded attributable startup plan, without blind reset/restart loops. UI URL/key wiring and cloud initialization transport/state need separate secure completion; no secret or dataset transfer is inferred here. Ephemeral state may be lost during replacement; images did not carry it.

This new one-file deployment-evidence PR preserves existing #445/#446. No application tests rerun for documentation; prior Windows133/fake-Linux failures remain historical. Actual fake storage probe result above only. UTF-8/LF/no BOM and git diff --check verified before commit. No production action or merge.

LOCAL_CHART_VERIFIED=True (historical WP57); AUTHENTIC_DATA_NOT_IMPORTED=False (local historical import only); PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.

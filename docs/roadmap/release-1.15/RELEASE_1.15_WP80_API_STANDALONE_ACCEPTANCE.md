# Release 1.15 WP80: Research API standalone acceptance

## Verdict

BLOCKED: the first fresh API site observation reports `QuotaExceeded`, usage `Exceeded`, and not Running. WP80 stopped immediately. No configuration correction is justified by this observation.

Observation: 2026-11-21 21:50:48.452602 UTC / 2026-11-21 18:50:48.452602 America/Sao_Paulo.

## Repository and historical evidence

Base: `632e636b9c39c94130aa1b58336f13a6d15471a2` (merged PR #477). WP79 [PR #478](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/478) remained open/unmerged at reviewed head `7a79de30e18e9c0851b2d2f8ece832d30b887e3d`; its branch and evidence were preserved. WP78 configuration evidence and WP79 quota recovery, subsequent transport failure, inconclusive detector and 20-second health-header timeout remain historical observations, not current runtime proof.

## Offline contract review

- `container/release_1_15/research-api.Dockerfile`: Linux image, user 1654:1654, bind address `0.0.0.0`, exposed port 52731, shell bootstrap entrypoint.
- `container/release_1_15/research-bootstrap.sh`: executes the Worker with `--research-snapshot-api`.
- `src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs`: explicit Kestrel listener uses port 52731 and the configured bind address; the listener does not depend on `ASPNETCORE_URLS`. Anonymous GET `/health` returns HTTP 200 `Alive` before service-key validation. Snapshot authentication is a separate gate.

These source contracts do not attest an Azure process, listener, deployed image or dataset. Public HTTPS remains port 443; no port changes were made.

## Live inventory and evidence

Exactly one read-only ARM GET targeted research staging app `aiqr115researchstagingwus27f5eabb5` in the established resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`. The projected response verified expected plan, West US 2, Linux, HTTPS-only and availability `Normal`; site state was `QuotaExceeded`, usage was `Exceeded`, and Running was false. Availability Normal does not override the quota blocker.

The owned worker used a 15-second capture bound, 3-second connect/10-second read timeouts, a 1 MiB capture cap and no retries or redirects. Observed execution was 1.437 seconds; owned worker/pipe cleanup completed. No raw response or credentials are included here.

| Acceptance item | WP80 result | Interpretation |
| --- | --- | --- |
| Configured accepted image | NOT_RUN | Historical WP78 configuration only; config/web was not reread after the stop. |
| Actual running-image identity | UNVERIFIED | No runtime digest witness. |
| Azure routing port | NOT_RUN | No settings collection or port projection read. |
| Actual listener | UNKNOWN | Source expects `0.0.0.0:52731`; no runtime witness. |
| Lifecycle/site state | BLOCKED | Fresh `QuotaExceeded`, usage `Exceeded`, not Running. |
| Anonymous `/health` | NOT_RUN | Stopped before serving probes. Prior timeout remains separate. |
| Authenticated BTC/USD snapshot | NOT_RUN | Health prerequisite unmet; cloud data availability UNKNOWN. |
| Authenticated ETH/USD snapshot | NOT_RUN | Health prerequisite unmet; cloud data availability UNKNOWN. |

No quota/usages endpoint, config/web, app-settings, startup detector, UI, production, registry, Docker, provider, health or authenticated API operation followed. No secret access, deployment, restart, configuration mutation or import occurred. Current counter values and the cause of quota recurrence are UNKNOWN.

## Focused offline validation

- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q`: 10 distinct tests passed, exit 0.
- `python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py --phase API`: offline plan-only invocation; no live adapter or Azure operation.
- In-memory Python syntax compilation of the existing bounded capture helper and planner: PASS.
- Changed-file UTF-8/LF/no-BOM check and `git diff --check`: PASS.
- Documentation-only delta: this file. No application or helper changes; owner checkout and unrelated prompts preserved.

Offline tests establish neither Azure health nor dataset availability.

## Next smallest action

Owner review of this blocker. A separately authorized, single bounded API `LinuxAppDown` startup diagnostic could investigate termination evidence; it must preserve UNKNOWN when timestamps, role, image or cause are absent. Do not automatically poll quota, repeat health or change configuration. Resume dependent acceptance only after fresh evidence supports recovery. WP81/WP82 were not started.

`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. Historical local acceptance is unchanged.


## Continuation: quota recovery, configuration and HTTP 503 diagnosis

The earlier quota blocker above is preserved as a historical finding. Subsequent owner-authorized reads observed recovery; no retrospective claim is made about the earlier failure.

All times below are UTC on 2026-11-22; America/Sao_Paulo is three hours earlier, on 2026-11-21. Each management read used an owned 15-second worker, 3-second connect/10-second read bounds, 1 MiB capture cap, exact target validation, and no redirects or retries. Configuration port reads were read-only POST appsettings-list requests: the collection remained transient in worker memory, only port-presence/equality booleans were emitted, and no service-key value was selected or displayed.

| Observation UTC | Operation | Duration | Sanitized result |
| --- | --- | --- | --- |
| 01:18:17 completion | One plan usages GET, separately requested by owner | Bounded worker completed | CPU 0/3,600,000 ms; stop requests 0/15. Only returned counters established; no zero-consumption claim after this read. |
| 01:21:16 completion | One API site GET | 1.532 s worker command | Running, usage/availability Normal, expected plan/region/Linux/HTTPS-only. |
| 01:24:15 completion | API config/web GET | Within 15 s cap | Exact accepted API child configured; startup empty. |
| 01:24:16 completion | API port-only projection | Within 15 s cap | WEBSITES_PORT present and equals 52731. |
| 01:26:22 completion | One LinuxAppDown GET; window 01:11:21-01:26:21 | Within 15 s cap | HTTP 503; nonstandard error-body classification; no startup evidence obtained. Stopped. |
| 01:27:49 request start | One anonymous API GET /health, separately approved | 21.888 s | TLS verified, HTTP 503 text/html, not exact Alive. No retry. |
| 01:38:00 completion | Fresh API config/web GET under attached diagnostic authority | 1.676 s | Exact accepted API child configured; startup empty. |
| 01:38:01 completion | Fresh API port-only projection | 1.047 s | WEBSITES_PORT present and equals 52731. |
| 01:38:02 completion | One LinuxLogViewer GET; window 01:23:01-01:38:01 | 1.529 s | HTTP 400; unclassified HTTP error. Stopped without fallback or retry. |

The health worker used direct anonymous HTTPS, certificate validation, a 3-second connection bound, 30-second owned-worker total cap and 4 KiB response cap. No cookies, authorization, proxy inheritance, redirect following or response body disclosure. No second health request occurred during the subsequent diagnosis.

The existing quota worker was initially rejected by automatic approval review because it contained unreachable broader settings-reading code. That rejected execution made no request. A temporary copy removed those branches and allowed only the exact plan usages GET; its one successful request is recorded above. Repository GitHub metadata refresh initially encountered sandbox network denial; the permitted network execution then confirmed unchanged main and PR heads. These were not Azure retries.

### Latest repository and source reconciliation

Fresh origin/main remains `632e636b9c39c94130aa1b58336f13a6d15471a2`. WP80 [PR #479](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/479) remains OPEN on `docs/release-1.15-wp80-api-standalone`; pre-continuation head was `72a0aa3283500bd2f8c8e2cd2a6ff3b214d8f201`. WP79 #478 remains OPEN at `7a79de30e18e9c0851b2d2f8ece832d30b887e3d`; no history or findings were overwritten. Owner changes remain in the original checkout.

Source review reconfirmed shell bootstrap to the Worker research API mode, explicit Kestrel binding and anonymous health routing before authentication. Offline review found prior LinuxLogViewer evidence, but no guaranteed timestamped main-container/image schema. This continuation selected that source once, rather than repeating the uninformative running-containers table. Its HTTP 400 prevented projection; no raw body or log text was output. No claim is made about why that source rejected the request.

### Current evidence limits and disposition

| Layer | Latest evidence | Classification |
| --- | --- | --- |
| Site | Running / Normal at 01:21 UTC | Management state only; not a process witness. |
| Image/startup | Exact accepted child and empty startup at 01:38 UTC | CONFIGURED_ONLY. |
| Routing setting | WEBSITES_PORT equals 52731 | Matches expected classic-container setting; actual ingress-to-process delivery UNVERIFIED. |
| Main container / process | No qualified startup or lifecycle record | UNVERIFIED. |
| Listener 0.0.0.0:52731 | Source contract only | UNVERIFIED at runtime. |
| External health | TLS success, HTTP 503 text/html | FAIL; origin and root cause UNKNOWN. |
| Authenticated snapshots / cloud historical data | Not requested | NOT_RUN / UNKNOWN. |

Historical 403, header timeouts, detector 503 and endpoint 503 remain distinct observations. No diagnostic result proves container exit, startup timeout, pull failure, wrong port, key failure, application exception or ingress provenance. The verified routing setting provides no demonstrated mismatch to correct. No speculative fix is proposed.

This diagnostic continuation made exactly three read-only management requests (config/web, port projection, LinuxLogViewer), zero health requests, zero quota queries and zero mutations. The preceding separately approved operations are individually inventoried above. No UI/production access, Docker/registry/provider operation, data import, authenticated snapshot, restart, logging change or configuration change occurred.

Validation: focused WP75 staging-plan offline suite passed 10 tests; temporary diagnostic syntax and empty-schema fail-closed projection passed. UTF-8/LF/no BOM, exact one-document diff and git diff --check passed. No application code or dependency changes. Tests do not prove Azure runtime readiness.

Single next smallest action: offline review of the existing LinuxLogViewer request contract and sanitized error-classification capability to determine whether a corrected, separately bounded source request is defensible. Do not retry it, poll health or change Azure configuration automatically. No acceptance pass until readiness evidence justifies one.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Offline diagnostic-error refinement (2026-11-22 UTC)

Owner approved the smallest offline classifier improvement after the diagnostic-request review. This pass made ZERO Azure requests or other runtime probes and did not inspect historical raw error bodies. The original HTTP 400 cause remains UNKNOWN.

Two narrow paths added:

- `eng/azure-cli/r1.15-qualification/wp80_diagnostic_error.py`: pure byte-to-fixed-category classifier; no CLI, adapter, network/process execution, persistence or output.
- `eng/azure-cli/r1.15-qualification/test_wp80_diagnostic_error.py`: synthetic-only regression tests.

The classifier accepts only 400-599 integer status and at most 64 KiB UTF-8 JSON. Duplicate keys, nonstandard JSON constants, excessive depth/records, malformed schemas and oversized bodies fail closed. It examines bounded ARM error records, including details/innererror. Only fixed server categories and fixed parameter hints (start/end time, API version, time grain, detector) can leave the function; raw codes/messages/targets, identifiers and bodies cannot. Unrecognized codes remain unclassified. Message/target keyword hints are untrusted mentions, not proof that a parameter is invalid. A service-failure code does not establish application failure. Every output retains `causeConfirmed=false` and `retryAuthorized=false`.

No request automatically invokes this classifier. Any future integration must enforce the existing exact-app request, deadline/capture/cleanup and no-retry contracts separately, pass the bounded response bytes in memory, emit only the classifier projection, and discard the response. The module authorizes no live request, fallback, API-version change or diagnostic-source change.

Offline request review: current GET `/sites/{siteName}/detectors/{detectorName}` and optional startTime/endTime match the documented operation shape. Synthetic UTC percent-encoding round-trip and 15-minute arithmetic passed; the observed HTTP 400 preceded local identity/projection validation. The accessible Microsoft operation reference describes version 2025-03-01; it does not independently prove the selected 2023-12-01 LinuxLogViewer contract or detector-specific requirements. No supported correction to those parameters has yet been demonstrated.

[Microsoft custom-container routing](https://learn.microsoft.com/en-us/azure/app-service/configure-custom-container#configure-port-number) documents WEBSITES_PORT for a custom listener. Port 52731 requires no separate public-port permission for this classic App Service arrangement: public HTTPS remains 443, routing setting remains 52731, runtime listener remains UNVERIFIED. No port or firewall change is proposed. [Microsoft detector-response contract](https://learn.microsoft.com/en-us/rest/api/appservice/diagnostics/get-site-detector-response?view=rest-appservice-2025-03-01) supports the operation-shape review, subject to the version limitation above.

Validation: `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp80_diagnostic_error.py -v` passed 15 distinct tests (parameter boundaries, nested codes, access/service/throttle categories, unknown-code withholding, non-disclosure, no network/process/output, equal-output synthetic noninterference, exact output keys, malformed/duplicate/UTF-8/schema failures, byte/record/depth caps). The existing focused WP75 suite passed 10 tests separately: 25 total distinct tests, zero skips. In-memory syntax compilation, UTF-8/LF/no BOM and git diff --check passed. This is synthetic qualification, not evidence about Azure's discarded HTTP 400 body or the API's HTTP 503 cause.

The existing WP80 PR remains the review vehicle. Exact delta now comprises these two offline files and this append-only evidence document. No application, port, startup, dependency, provider, image, auth, TLS or storage changes. Next smallest separately approvable step: integrate this tested classifier into one bounded exact-API diagnostic request, with a concrete documented request contract and no automatic retry. No live request is authorized by this appendix.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Owner-approved classified diagnostic observation

At 2026-11-22 02:15:44.501078 UTC (2026-11-21 23:15:44.501078 America/Sao_Paulo), one newly approved API LinuxLogViewer GET completed in 2.054 seconds. Fixed window: 02:00:42-02:15:42 UTC. Same exact API target, API version 2023-12-01, 15-second owned worker, 3-second connect/10-second read bounds, 1 MiB capture limit, no redirects/retries. No request parameters or source were changed to force acceptance.

Before execution, two fake end-to-end worker cases verified classified HTTP 400 output, exact one-request behavior and withholding of synthetic private message text. The temporary worker imports the reviewed repository classifier; its only functional refinement is replacing the old HTTP error-body projection with `classify(status, body)`. This integration was tested without real Azure HTTP calls or credential access.

Observed result: HTTP 400, `responseClass=UNSUPPORTED_SCHEMA`, empty `serverCategories` and `parameterHints`, `causeConfirmed=false`, `retryAuthorized=false`. The response was rejected by the conservative structural classifier; no fields, raw keys, body excerpts or messages were exposed or retained. This does not establish malformed query parameters, detector absence, a permission failure or the source of the API health 503. No historical response was reclassified.

Stopped after exactly one management GET. No fallback, health/UI/production request, quota query, secret access, configuration write, restart, deployment or data operation. Main-container identity, process/listener readiness and 503 provenance remain UNVERIFIED/UNKNOWN. No correction to Azure configuration is justified.

Next smallest step: offline review of a supported alternative startup-evidence source and its response contract, with a precise bounded proposal before any new live read. Do not repeat the same LinuxLogViewer request or weaken the schema classifier to expose arbitrary response content.

Validation for this continuation: two fake end-to-end worker cases passed; the existing 15 classifier regression tests passed; UTF-8/LF/no BOM and git diff --check passed. Only this evidence document changed in this continuation. PR #479 remains OPEN/UNMERGED.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Offline alternative-source review and documented error-schema correction

This pass made ZERO Azure/SCM/registry/provider/runtime requests and accessed no retained log/error bodies or credentials. Public Microsoft documentation and installed SDK inspection were used offline; no logging configuration or dependency changes.

| Candidate | Source-backed capability | Limit / disposition |
| --- | --- | --- |
| Direct ARM container log tail | Microsoft Get Web Site Container Logs documents POST `/sites/{name}/containerlogs`; returns last Docker log lines, 200 file or 204 no content. | Better candidate for startup/pull/exit/readiness messages than generic detector tables; availability and runtime attribution are unproven. Historical unavailable/timeout/NotFound findings remain. Not executed. |
| ZIP log archive | Broad Docker/container console archive. | Rejected for this narrow pass: excess capture and unrelated historical content. |
| SCM log stream / exec | Requires separate endpoint/access and safe protocol review. | Not selected; no token acquisition, process access, shell or stream operation. |
| Repeated LinuxLogViewer / generic running-container detector | Already rejected or lacked qualified main/image evidence. | No unchanged repeat justified. |

[Microsoft container-log-tail contract](https://learn.microsoft.com/en-us/rest/api/appservice/web-apps/get-web-site-container-logs?view=rest-appservice-2025-03-01) specifies POST, not GET, and media types application/octet-stream or application/json. It documents no server-side start/end-time or line-count parameter: any recent-window selection must happen locally, and a cap may prevent a complete window. A fake installed-SDK pipeline independently generated exactly one POST to the fake app's `/containerlogs`, API version 2025-05-01, without invoking any transport/authentication policy. The documentation reviewed uses 2025-03-01; these versions must not be silently substituted or confused with prior 2023-12-01 observations. A future proposal can explicitly pin the documented 2025-03-01 operation; successful availability is not assumed.

### Concrete classifier limitation found and corrected

The same Microsoft DefaultErrorResponse contract defines `innererror` as a string. A synthetic valid error containing a string innererror reproduced UNSUPPORTED_SCHEMA in our original classifier. This is a confirmed local schema-coverage limitation, NOT a finding about any actual discarded Azure response. No historical HTTP 400 is retrospectively attributed to this shape.

Within the existing approved classifier/test paths, string innererror is now accepted as opaque and ignored entirely; its content cannot contribute categories/hints. Bounded nested-object compatibility remains. Other types fail closed. A regression verifies that different synthetic innererror strings, even mentioning different parameters, produce equal fixed output. No request scope, retry authority, size/depth caps, image/port/runtime behavior or Azure configuration changed.

Validation: 16 classifier tests plus 10 existing WP75 tests passed (26 distinct tests, zero skips), with one separate fake SDK-route check. Syntax, UTF-8/LF/no BOM and git diff --check passed. Changes remain confined to the existing two classifier/test paths and this append-only evidence document; PR #479 stays OPEN/UNMERGED.

### Bounded candidate, not live authorization

Candidate target is ONLY research staging API in the existing subscription/group, one POST with no request body to `/sites/aiqr115researchstagingwus27f5eabb5/containerlogs?api-version=2025-03-01`. This is a documented read operation despite POST. Proposed owned-worker cap 15 seconds, connect 3/read 10 seconds, capture at most 64 KiB in transient memory, no redirects/retries/fallback, no ZIP extraction or persistence. Accept only documented media types and statuses; unknown binary/archive/schema content must be rejected. Non-2xx can use the fixed error classifier. Stop after the single outcome, including 204/404/timeout/cap breach. No health, UI, quota, setting, credential, restart or configuration requests.

The current error classifier does not parse successful log tails. Before requesting this candidate, the next smallest step is a fake-tested bounded success-payload sanitizer, with fixed categories for pull failure, process exit, startup timeout and listener readiness, and strict UTC/recent-window/role/image attribution limits. Output no raw lines, paths, arbitrary image/host/container identifiers or payloads; missing attribution remains UNKNOWN. No log stream, new process adapter or broad diagnostic framework is needed. Any implementation should use only existing `wp80_diagnostic_error.py`, `test_wp80_diagnostic_error.py` and this evidence document. No live operation has been approved or run by this review.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Offline successful-tail sanitizer qualification

Owner approved preparation of a fake-tested success-payload sanitizer, not a live container-log read. This pass made ZERO Azure/SCM/runtime requests, accessed no retained log bodies or credentials, and changed only the existing classifier, its test file and this evidence document.

Added pure `sanitize_log_tail(status, content_type, body, window_start, window_end)` alongside the error classifier. It accepts HTTP 200 or empty 204, at most 64 KiB bytes, at most 256 lines, and at most 2048 UTF-8 bytes per line. The requested local window must be timezone-aware, positive and no greater than 15 minutes. Unsupported status/media/encoding, control/archive content, invented JSON object/array schemas and cap violations fail closed. Documented media types application/octet-stream and application/json are accepted; JSON is limited to a string holding a tail. No ZIP extraction, raw output, persistence, execution, CLI or network adapter exists.

Because the file payload has no documented record schema, support is deliberately narrow: ISO UTC Z-prefixed lines only. Unknown line shapes remain unclassified, not presumed fresh. Only records within the caller's fixed window can contribute fixed event categories: pull-failure, exit, startup-timeout and HTTP-ping-failure mentions, or exact application listener/configuration/storage markers confirmed against ResearchSnapshotEndpoint.cs. Such matches are untrusted mentions, not attributed failures. The original payload could be partial, historical, contain quoted messages, or lack app/main-container binding. Empty 204, no markers and no fresh records do not prove healthy startup.

Every output retains runtimeImageIdentity, mainProcessLiveness and listener UNVERIFIED; causeConfirmed=false and retryAuthorized=false. No raw line, timestamp, arbitrary exit code, image/host/path/identifier, message substring, secret or payload can be emitted. No claim of a qualified runtime image, listening socket or 503 origin can follow from these categories alone.

Validation:

- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp80_diagnostic_error.py -v`: 30 distinct tests passed (16 existing error tests, 14 success-tail tests), zero skips.
- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q`: 10 tests passed; total 40 distinct tests.
- New coverage includes recent/stale/future/undated records, exact source markers, inclusive window endpoints, no-content semantics, JSON-string-only handling, malformed timestamps/media/types/encoding, control/archive rejection, byte/line/record cap boundaries, non-disclosure and equal-output noninterference across different synthetic values/order, exact output keys and no network/process/output side effects.
- In-memory syntax compilation, UTF-8/LF/no BOM and git diff --check passed. These checks establish only synthetic sanitizer behavior.

The previously proposed exact-API POST `/containerlogs?api-version=2025-03-01` remains NOT_RUN. A next separately approved read would need a small temporary, fake-tested adapter using the existing bounded worker: one request, 15-second worker deadline, 3-second connect/10-second read timeout, 64 KiB streaming cap before materialization, documented media/status handling, no redirects/retries/fallback, transient memory only, success sanitizer or fixed error classifier only, then stop. It must not enable logging, retrieve SCM credentials, probe health or call any UI/production/provider/data endpoint. Prior operation-unavailable/NotFound history remains a limitation, not a reason for silent fallback.

PR #479 remains OPEN/UNMERGED. No application behavior or Azure configuration changed; 503 root cause remains UNKNOWN.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Owner-approved direct container-log observation (2026-11-22)

Owner separately approved one bounded direct API container-log read using the tested sanitizers. Exactly one read-only POST (empty body) targeted the established research API `/containerlogs?api-version=2025-03-01`. No detector/API-version fallback or health request followed.

Observation completed at 2026-11-22 03:08:04.938769 UTC / 2026-11-22 00:08:04.938769 America/Sao_Paulo. Local classification window was fixed before execution: 02:52:53-03:07:53 UTC; it was NOT supplied as an unsupported server-side query parameter. Owned worker cap 15 seconds; per-request connect 3/read 10 seconds; streaming capture cap 64 KiB; no redirects/retries. Worker elapsed 11.765 seconds, with one ARM request counted. Result BLOCKED, ReadTimeout at ARM_TRANSPORT; no HTTP status or successful log projection was obtained. Owned worker completed and bounded capture cleanup returned. No raw body/log values were emitted or persisted.

The temporary adapter was fake-tested before the sole live attempt: five cases passed for HTTP 200 tail, 204 no content, 400 structured error, cap breach and redirect rejection. Assertions verified exact POST target/version, empty body, one request, configured timeouts, redirects disabled and raw synthetic message withholding. An initial fake assertion incorrectly required Python None for the empty body; the requests library represented it as empty bytes. The test expectation was corrected to accept either empty representation and all five cases passed; no live request preceded that correction. No repository source change was necessary.

This read timeout is a diagnostic transport failure, not an application HTTP 503 and not proof of a container crash, key problem or port mismatch. It yielded no fresh runtime-image, process/listener, pull/exit/startup or ingress witness. Prior observations and source/configuration contracts remain historical; current quota was not queried. Root cause remains UNKNOWN.

Zero Azure mutations; no logging enablement, settings/secret readback, restarts, deployments, quota checks, health/API snapshots, UI/production/SCM/registry/provider/Docker/data operations in this continuation. Exactly one direct ARM log-tail read, then STOP.

Validation: five fake adapter cases passed; existing 40-test offline qualification remains the source baseline (not rerun for this documentation-only append). UTF-8/LF/no BOM and git diff --check passed. Only this evidence document changed; PR #479 remains OPEN/UNMERGED.

Next smallest action: owner review of this diagnostic-access blocker and a manual portal inspection of existing research API startup/container logs, without enabling logging or changing configuration. Share only a sanitized fixed error category or an explicitly approved narrow follow-up method; do not request raw logs, keys, screenshots containing identifiers or private data. Do not automatically retry the same timeout or compensate with a deployment/configuration change.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Owner portal evidence and guarded restart/capture attempt

Owner-provided platform records at 2026-11-22 01:30:21 UTC report that the research site container terminated during startup, ContainerStartupFailure, exit code 1 after approximately 0.3 seconds, followed by startup cancellation/site stop. This confirms an owner-reported rapid startup exit for that event; it does not identify the inner application/bootstrap cause, attest the running digest, or prove the exact provenance of the earlier health 503. No raw records, addresses or identifiers are republished here.

Later owner-provided excerpts showed Kudu platform container lifecycle output, with no Research API listener/configuration/storage marker or application exception. Kudu/SCM lifecycle is separate from main application health. Owner reported enabling File System application logging; the screenshot showed quota 35 MB and retention one day. This is owner-performed configuration, not a Codex mutation or independently read-back setting. Earlier statements of unchanged logging apply only to the earlier observations. The owner portal source selector subsequently listed only Kudu.

Owner expressly approved one research-only restart plus at most 60 seconds of sanitized capture, with no UI/settings/image/port changes or retries. A temporary adapter was prepared with a safer precondition: establish capture first, otherwise skip restart. Installed CLI offline review showed the ordinary log-tail wrapper can ping the site, perform extra reads and choose publishing-credential authentication; that wrapper was not invoked. The adapter instead uses the existing sign-in's transient App Service bearer authentication for exactly one fixed SCM `/logstream` connection; no publishing credentials or research/provider service keys are read or output. Authentication token renewal, if required by the existing sign-in, is not an application settings operation.

Two fake orchestration cases passed before execution: failed capture skips restart; established capture permits exactly one exact-app POST restart. Both verify request order, redirects disabled, no duplicate operations and no synthetic token/log disclosure. No raw logs are persisted. The existing owned Windows Job bounds the worker to 60 seconds; worker logic ends within 54 seconds, with 64 KiB stream capture and fixed success projections. Stream output stays untrusted and cannot attest main-container identity.

Actual observation completed 2026-11-22 03:36:41.261166 UTC / 2026-11-22 00:36:41.261166 America/Sao_Paulo; worker elapsed 2.069 seconds. Exactly ONE SCM GET was issued. It returned HTTP 200 but an unsupported media type (outside the adapter's text/plain and application/octet-stream allowlist). Media value/body were not printed; the body was not consumed and the response was closed. `streamFailure=UNSUPPORTED_MEDIA`; `restartSkippedReason=CAPTURE_NOT_AVAILABLE`; restartAttempted=false, restartAccepted=false, restartRequests=0. The empty tail projection has UNKNOWN freshness and no event categories; it is not startup or health evidence. Owned worker and pipe cleanup completed.

Stopped without fallback, replay or restart. ZERO Azure mutations by Codex in this pass; no ARM site/config/quota read, health probe, UI/production/data/provider/registry/Docker operation. HTTP 200 on SCM is not proof of authenticated log access or application health; an unexpected response type does not establish a particular authentication failure either.

Current blocker: no usable main-application console capture channel. Rapid API exit remains owner-reported; root cause UNKNOWN. No image/port/settings correction is justified. Next smallest action is owner-assisted capture of one separately coordinated startup in the portal's main application log source, or offline preparation of a fixed status/media/auth-response classifier before any newly approved SCM read. Do not restart without an established capture channel or silently accept arbitrary HTML as logs. The previously approved one-restart operation was not spent, but this failed capture attempt must not be automatically retried.

Validation: two fake orchestration cases passed; existing 40-test classifier/plan baseline unchanged and not rerun for this documentation-only update. UTF-8/LF/no BOM and git diff --check passed. Only this evidence document changed; PR #479 remains OPEN/UNMERGED.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## SCM response-contract review (offline only, 2026-11-22)

Authority: owner-supplied WP80 SCM response-contract review; ZERO live Azure/SCM/health/quota/browser/registry/provider/data requests and ZERO mutations in this pass. No credential reads, authentication execution, retained logs or discarded response bytes were accessed. GitHub main/PR metadata was refreshed as authorized. Base/main remained `632e636b9c39c94130aa1b58336f13a6d15471a2`; existing branch `docs/release-1.15-wp80-api-standalone` was clean at pre-review head `112f4afa479ac7af1333ab0e8b06a57ccceeef2e`. PR #479 OPEN; owner checkout and historical observations preserved.

### Local authoritative client-source evidence and limits

The installed Azure CLI's App Service custom module was inspected locally through Python signatures/bytecode, without constructing the CLI profile or invoking any command:

- `get_streaming_log` appends `/logstream` to the SCM origin and calls `_get_log` in a daemon thread. It also calls ping_site and a source/auth helper; this wrapper is not safe to invoke blindly within a single-read budget.
- `_get_log` performs an ordinary HTTP GET using urllib3 with preload_content=False, requires HTTP 200, and iterates response.stream(). It has no media-type allowlist, SSE parser or WebSocket handshake/upgrade implementation. Its permissive acceptance of any HTTP 200 is NOT proof that arbitrary 200 bodies are valid log output.
- `get_scm_site_headers` may select basic authentication and fetch publishing credentials, or use the CLI's bearer helper. None of those authentication functions was invoked in this pass.
- `get_bearer_token` obtains an existing-sign-in credential and uses the cloud App Service resource audience. SCM authentication is separate from the Research API's AIQ_RESEARCH_API_KEY; no API key can be inferred from or substituted for SCM authentication.

No locally available authoritative SCM server implementation or current server media-type specification was found. Existing repository WP11 code assumes `/api/logstream`, whereas the installed CLI uses `/logstream`; that difference is unresolved, not authorization to try both. Existing repository references to Microsoft documentation remain useful historical pointers but were not fetched in this offline-only review. Therefore the client route/GET streaming mechanism is established; server media type, actual framing (chunked vs other), SSE status, authentication completion, and a reliable main-container log source are NOT established. HTTP 200 alone does not determine any of them. No live endpoint, package download or external documentation fetch was used to fill the gap.

### Exact prior adapter audit

The previous temporary adapter used GET to the exact research SCM `/logstream` route, Accept text/plain, Accept-Encoding identity, transient App Service bearer authentication, TLS verification and no stream proxy inheritance. Redirects/retries were disabled. It accepted status 200 and only text/plain or application/octet-stream; it checked those gates before iterating any stream body. Function-scoped AST/source audit confirmed that unsupported media returns before body iteration and closes the response in finally. The other body iterator in the worker belongs to the separately gated restart response; it is not a log-body bypass.

It did NOT retain the unsupported media type, auth-challenge presence, redirect Location presence or upgrade/connection indicators. No raw challenge/header/body was inspected now. The exact historical media and why the server selected it cannot be recovered. Confirmed local reason for rejection is only failure of the adapter's two-media allowlist; the server explanation remains UNKNOWN. No retrospective claim of HTML, SSE, login redirection or authentication success/failure is justified.

The stream had connect 3/read 45-second socket bounds inside a 60-second owned Windows Job, a 64 KiB transient cap, main logic ending within 54 seconds, and a 10-second capture-availability wait before any restart. A successful HTTP/media gate alone would not bind output to the main app, image or process. The observed gate failed: body unread, zero restart requests. Future metadata classification must not loosen that body-read guard or reuse the previous restart approval automatically.

### Pure metadata classifier

Added `classify_scm_metadata` in the existing WP80 classifier path. Input is exactly eight projected fields: status, contentType, transferEncoding, contentEncoding, authChallengePresent, locationPresent, upgradePresent, connectionUpgrade. Unknown/missing keys, wrong types, non-boolean presence indicators, non-ASCII/control characters, malformed media syntax, or caps exceeded fail closed. Status is an integer 100-599; content-type is at most 256 characters and each encoding field at most 64 (maximum 384 string characters). No raw challenge, cookie, Location/Upgrade value, token, URL, hostname, identifier or body field is accepted. Header presence booleans must be projected by a separately reviewed caller; this function performs no request or header acquisition.

Output is a fixed eight-key enum/boolean projection only. It distinguishes 401/403, redirects/missing Location, unsupported or contradictory 101 upgrades, contradictory upgrade indicators on ordinary responses, 200 with challenge/Location ambiguity, missing/unknown media, declared SSE, HTML/JSON, transfer/content encoding and candidate plain/octet-stream bodies. Common charset/quoted parameters normalize safely. Declared chunked or event-stream metadata is not protocol verification. Even a candidate remains `HTTP_BODY_CANDIDATE_UNVERIFIED`, authentication UNVERIFIED, bodyReadAuthorized=false, retryAuthorized=false and logCaptureVerified=false. Every branch withholds arbitrary values. No cookie/token parsing, redirect following, body read, authentication action or live adapter was added.

Validation commands actually run:

- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp80_diagnostic_error.py -v`: 46 distinct tests passed (30 prior + 16 new SCM metadata tests), zero skips.
- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q`: 10 tests passed; total 56 distinct tests.
- New cases cover supported candidate/unsupported media, common parameters, missing/contradictory indicators, 401/403, redirect/101 handling, encoding, exact input/output keys and metadata caps/boundaries, malformed/control input, confidentiality/noninterference and no network/process/file/output side effects.
- In-memory syntax compilation, UTF-8/LF/no BOM, Markdown structure/local link review and git diff --check passed. Only the existing classifier, tests and this append-only evidence changed.

### Recommendation and exact next authorization

A reliable body-capture contract is not established, so NO new successful-body capture route is proposed yet. Missing evidence: actual fixed media class, presence-only authentication/redirect/upgrade indicators on this route, authoritative server framing requirements, and a qualified main-application log source. No restart or route substitution is justified.

The next separately approvable operation would be METADATA ONLY, not a capture: one authenticated GET with no response-body reads to `https://aiqr115researchstagingwus27f5eabb5.scm.azurewebsites.net/logstream`, same existing-sign-in App Service bearer class held only in process memory, Accept text/plain and Accept-Encoding identity, TLS verified, connect 3 seconds/total owned-worker 15 seconds, no proxy inheritance, redirects, retries or fallback, ZERO body bytes/lines consumed or persisted. Project only the eight bounded input fields (booleans for challenge/Location/upgrade indicators) and emit only the new classifier's fixed output plus safe status/timing. Stop and close on every outcome, including HTTP 200; no token retrieval in this offline pass. Oversized/malformed or duplicated relevant headers must be rejected by the future adapter, not concatenated into a guessed contract. Authentication token refresh, if needed, would require that future scope to explicitly cover the existing CLI sign-in channel; no publishing/service/API/provider credentials should be read.

This is a proposed single exact-route metadata observation requiring explicit future authorization and fake-tested adapter integration, not permission to execute it now. It cannot establish container health or justify accepting unsupported content. No /api/logstream alternate, restart, health, UI, production, settings or log-body read may follow automatically. PR #479 remains OPEN/UNMERGED; startup exit-code-1 and API 503 causes remain UNKNOWN.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Separately approved SCM metadata-only observation

Owner approved exactly one metadata-only research SCM GET, with no body read or restart. A temporary adapter used the reviewed classifier and the same exact `/logstream` route, existing-sign-in App Service bearer class, TLS validation, Accept text/plain and Accept-Encoding identity. No publishing credentials or research/provider API keys were requested. The response was streamed only to acquire headers, never materialized as content/text.

Before execution, eight fake adapter cases passed: 200 plain candidate, 200 event-stream, 200 HTML rejection, 401 challenge, redirect, 101 upgrade, duplicate relevant header rejection and metadata-cap enforcement. Fake response body properties/iteration raise on access; assertions verified exact one GET, zero body access/restarts, closed response and no synthetic credential/challenge/URL/header-value disclosure. Relevant duplicate header values are rejected rather than concatenated. Indicator values are bounded transiently and only presence booleans enter the pure classifier.

Actual completion: 2026-11-22 03:47:23.899907 UTC / 2026-11-22 00:47:23.899907 America/Sao_Paulo. Worker elapsed 2.185 seconds. One SCM GET, HTTP 200. Fixed projection: mediaClass=EVENT_STREAM; transferClass=CHUNKED_DECLARED; protocolClass=SSE_DECLARED_UNVERIFIED; responseClass=SSE_UNSUPPORTED; authentication=UNVERIFIED; bodyReadAuthorized=false; logCaptureVerified=false; retryAuthorized=false. Body bytes read: ZERO. Response closed, owned Windows Job/process/pipe cleanup completed. Connect 3/read 10-second bounds inside 15-second owned deadline; no proxy inheritance, redirects/retries/fallback. Restart requests: ZERO. No settings, health, quota, UI, production, ARM management, registry/provider/data/Docker operations or Azure mutations.

This establishes the declared media/framing class of THIS response only. It gives a concrete reason why this current response is outside the earlier adapter's plain/octet-stream allowlist. It does not retrospectively recover the discarded response's media type or prove that all responses share this contract. No SSE body/event was inspected, so neither actual SSE framing, authentication completion, main-container source, fresh application output nor runtime identity is attested. Chunked transfer declaration alone is not a record schema. HTTP 200 here is not API health acceptance.

The smallest next step is OFFLINE preparation of a bounded SSE decoder and synthetic tests before any separately authorized body capture. It must distinguish SSE framing (data fields, blank-line event delimiters, comments/heartbeats and multi-line data) from application-log payloads, cap total bytes/line/event counts and sizes, discard non-data fields without following retry instructions or event IDs, never emit raw data/headers/identifiers, and pass decoded payload only through a known fixed sanitizer. Unknown event payload schemas must remain UNSUPPORTED; the main-container identity and root cause must remain UNKNOWN without a qualified witness. Do not simply broaden media acceptance and feed framed bytes to the plain-tail sanitizer. No new connection, restart or live capture is approved by this result.

Evidence-only repository change; the eight fake integration checks were actually run. Existing 56-test offline classifier/plan baseline remains unchanged (not rerun for this append). UTF-8/LF/no BOM and git diff --check passed. PR #479 remains OPEN/UNMERGED.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Offline bounded SSE preparation

Owner approved offline SSE decoder preparation following the EVENT_STREAM metadata observation. This pass made ZERO Azure/SCM/runtime/credential requests and ZERO mutations. No historical event body was available or inspected; the recorded response remains media/framing declaration only. Existing three-file WP80 scope preserved.

Added pure `sanitize_sse(body, window_start, window_end)` in the existing classifier. It accepts one already bounded transient byte capture, not a socket, iterator or request adapter. UTF-8 (optional initial BOM), LF/CRLF/CR line endings, exact lowercase data fields and at most one optional post-colon space are handled. Multiple data fields join with newline; only a blank line dispatches an event. Comments/heartbeats, event names, IDs, retry directives and unknown SSE fields are discarded and never cause authentication, replay, delay, redirection, reconnection or output. No external dependency or live protocol implementation was installed.

Caps: 64 KiB input, 512 framing lines, 2048 UTF-8 bytes per framing line, 32 data events and 8192 UTF-8 bytes per joined event. Invalid encoding/control/archive bytes, cap breach, incomplete data event or unsupported event payload fail closed with no partial category output. An EOF/terminal newline alone does not dispatch pending data. Comment-only/empty input is not log evidence. The caller still must enforce acquisition timeout/caps before materialization; complete-event parsing does not authorize another request or extend the capture deadline.

The only supported data payload is the existing narrow UTC-prefixed text-log subset. JSON objects/envelopes, HTML, arbitrary text, blank data and unknown payloads remain unsupported; no undocumented SCM schema is invented. Recognized payloads pass through `sanitize_log_tail`, so stale/future records cannot contribute current event markers. Exact endpoint SSE framing and payload schema remain unverified because this is synthetic-only work. A real response might contain only comments, use an unknown envelope, end mid-event, or contain Kudu output. No declaration of event-stream media alone allows the parser to claim successful main-application capture.

Fixed output contains responseClass, freshness, eventCategories, runtimeImageIdentity, mainProcessLiveness, listener, causeConfirmed and retryAuthorized only. No raw event fields/data, IDs, retry values, timestamps, private values, paths/hosts or identifying derivatives leave the function. Runtime image/process/listener remain UNVERIFIED; causeConfirmed=false and retryAuthorized=false throughout. Category matches remain untrusted mentions with no main-app/image attribution.

Validation commands actually run:

- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp80_diagnostic_error.py -q`: 64 distinct tests passed (46 prior, 18 new SSE tests), zero skips.
- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q`: 10 tests passed; 74 total distinct tests, zero skips.
- New coverage includes multiline events, comments/IDs/retry noninterference, strict delimiters and case/space semantics, BOM/line endings, empty/heartbeat/incomplete input, stale/future records, unsupported JSON/HTML/text, exact and exceeded byte/line/record/event boundaries, prior-marker withholding on later failure, output keys and no network/process/file/output side effects.
- In-memory syntax, UTF-8/LF/no BOM, Markdown links/fences and git diff --check passed. No live authentication, request adapter or token access was involved.

The next separately approvable step is one exact research SCM `/logstream` GET with bounded SSE-body capture, using the existing sign-in App Service bearer class, TLS validation and fixed metadata precheck, then this decoder. Any proposal must specify a hard owned deadline, 64 KiB streaming cap, no retry/redirect/fallback/restart, no raw persistence/output, and STOP on unsupported status/media/auth/upgrade/encoding, incomplete/unknown payload or transport failure. A short capture may end mid-event; report that rather than extending or reconnecting. Main-container/image binding remains a separate requirement even if framing and log markers parse. No live capture or restart is authorized by this offline qualification.

PR #479 remains OPEN/UNMERGED. The API startup exit and health 503 cause remain UNKNOWN.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Separately approved bounded SSE capture: cap stop

Owner approved one exact research SCM `/logstream` SSE capture, no restart/retry/fallback. A temporary adapter reused the strict metadata projection, existing-sign-in App Service bearer class and pure SSE sanitizer. It made no publishing/service/API/provider key read. TLS verification, no stream proxy inheritance, no redirects/retries, 3-second connect/8-second socket read timeout, 12-second internal timer and 15-second owned Windows Job deadline were applied. Accumulated transient body buffer was limited to 64 KiB; no raw event/body was printed or persisted.

Before execution, seven fake adapter cases passed: supported SSE data, HTML/401/redirect body blocking, unknown payload, incomplete event and capture-cap rejection. Tests asserted exact one GET, response closure, no body reads on rejected metadata, fixed-output/no-token/no-raw-event behavior and zero restarts. They ran against fake transports, not Azure. Existing 74-test repository baseline was unchanged and not rerun for this evidence-only append.

Actual completion: 2026-11-22 10:47:59.610406 UTC / 2026-11-22 07:47:59.610406 America/Sao_Paulo. Worker elapsed 2.251 seconds. One SCM GET returned HTTP 200; fixed metadata remained EVENT_STREAM, CHUNKED_DECLARED, SSE_DECLARED_UNVERIFIED, authentication UNVERIFIED. While reading 512-byte chunks, the next chunk would exceed the 64 KiB accumulated buffer cap. The worker cleared the buffer, returned CAPTURE_CAP_EXCEEDED and closed the response. A cap-detection chunk can be received transiently beyond the accumulated-buffer limit; no oversized body is retained or sanitized. Underlying HTTP transport buffering is not proof of an exact wire-byte ceiling.

No eventProjection was emitted: all captured event data was discarded on cap breach. The output's captureStarted=false after cap rejection means acceptance was invalidated; it does NOT mean zero body bytes were read. This differs from the earlier metadata-only request. No raw payload schema, event content, timestamp, message, identifier or private value was inspected or retrospectively classified after discard. The worker and owned Windows Job/pipe cleanup completed.

Stopped after this one outcome. ZERO restart requests or Azure mutations; no health, quota, UI/production, ARM management, alternate stream, credential/settings, provider/registry/Docker/data operations. No increased cap, extended deadline, reconnect or automatic fallback. It remains unknown whether the response contained main-app, platform/Kudu, replay/history, heartbeat or another SSE payload schema. High response volume does not establish process health, crash cause or ingress readiness.

Current blocker is bounded capture acceptance, not a newly demonstrated application cause. Next smallest step: offline review of streaming/cap semantics to determine whether complete events within the same fixed budget could be classified incrementally without retaining or exposing raw data. Do not silently raise the cap, claim success from a partial body, or request another live capture without separate authority. This observation provides no justification for image/port/settings changes or a restart.

Only this append-only evidence document changed. Seven fake adapter cases passed; UTF-8/LF/no BOM and git diff --check passed. PR #479 remains OPEN/UNMERGED; main-container runtime identity, listener and health acceptance remain unverified.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Offline same-budget incremental review and first-event refinement

Owner requested continuation of offline incremental/cap review. ZERO live Azure/SCM/authentication/credential/runtime operations or mutations occurred. No discarded capture bytes were recovered or examined. Existing three-path scope and PR #479 were retained.

Source audit of the previous temporary adapter confirmed that sanitize_sse was called only from finalization. It accumulated chunks until EOF/deadline/cap, and cap rejection cleared the buffer before decoding. This is a confirmed local batching limitation: a synthetic complete early event can be classified below the cap, while an otherwise ongoing capture can later invalidate the whole batch. It is NOT proof that the actual discarded response contained any complete or relevant event.

Added pure `sanitize_sse_prefix` in the existing classifier, reusing sanitize_sse unchanged. A future caller passes its cumulative bounded prefix after each small chunk. Heartbeat/control-only frames wait; the FIRST complete data event, including an unknown payload, returns a fixed projection with captureStopRequired=true. The caller must close immediately on that signal and never scan past an unsupported first payload for a convenient message. A positive result is a one-event sample only, NOT acceptance of subsequent events or the whole stream. Subsequent content in an already received chunk is not claimed to be verified.

The helper owns no socket, iterator, process, authentication, timer or reconnect behavior. It preserves the 64 KiB cumulative input budget, 512 framing-line and 2048-byte-line guards, the original per-event/payload limits and all UNVERIFIED runtime flags. Exact-cap input requires stopping even if it contains only heartbeats. Oversized input fails closed. End-of-window final=True handles pending/incomplete framing conservatively; no EOF-triggered partial data acceptance. A trailing CR is deferred until the next chunk or finalization to avoid splitting CRLF incorrectly, and incomplete trailing UTF-8 is not decoded before a complete event. Returned data remains fixed enums/booleans only; no event bytes, offsets, raw identifiers or contents leave the helper.

A fake bounded-capture regression yields a heartbeat plus a split complete event and raises if the next chunk is requested. It passed, demonstrating that a caller honoring captureStopRequired can stop below the same cap without waiting for stream exhaustion. This is an offline design proof, not a test of a real SCM payload or a qualified live adapter. The old all-events sanitizer remains strict and retains its prior behavior; it was not weakened to claim successful partial-batch acceptance.

Validation actually run:

- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp80_diagnostic_error.py -q`: 76 tests passed (64 previous + 12 prefix/caller regressions), zero skips.
- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q`: 10 tests passed; total 86 distinct tests, zero skips.
- Added coverage includes every byte boundary, split CRLF/multibyte data, heartbeat waiting, first unknown-payload stop, one-event scope, final incomplete/CR handling, exact/exceeded budgets, invalid windows/types, fake iterator early closure, exact output keys and no network/process/file/output side effects.
- Syntax, UTF-8/LF/no BOM, Markdown review and git diff --check passed. Only the existing classifier/test/evidence paths changed.

Next smallest separately approvable step: fake-test a small temporary adapter using this helper after each chunk, with the SAME exact research SCM route, 15-second owned deadline, 64 KiB cumulative budget, strict metadata gate and no restart/retry/fallback. If subsequently authorized for one live read, request only up to the remaining budget, close on the first complete data-event classification (including unsupported schema), and report the limited sample without extrapolation. Cap/deadline/unknown payload must still stop. No larger download or schema invention is proposed. No live adapter was executed in this pass; the API exit-code-1 and HTTP 503 causes remain UNKNOWN.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Separately approved first-event sampling observation

Owner approved one same-budget exact research SCM `/logstream` sample using early-stop handling, without restart/retry/fallback. The temporary adapter reused strict metadata validation and sanitize_sse_prefix after each chunk. It requested at most the lesser of 512 bytes and the remaining 64 KiB cumulative budget with read1(decode_content=False); any over-return is rejected. No raw event/body/header/credential value was printed or persisted.

Before the live request, eight fake adapter cases passed: first supported event, unknown first payload, heartbeat then event, incomplete EOF, HTML/403 body rejection, oversized transport return and split UTF-8/comment framing. They verify one exact GET, immediate stop, blocked bodies unread, response closure, raw/token withholding and zero restarts. An initial defensive test exposed that the adapter compared a returned chunk only against remaining total budget, not the smaller requested chunk size. The adapter added the requested-size guard and all eight cases passed before the sole live operation. No live retry occurred.

Actual completion: 2026-11-22 10:58:24.739581 UTC / 2026-11-22 07:58:24.739581 America/Sao_Paulo. Worker elapsed 2.136 seconds. Exactly one SCM GET returned HTTP 200, with EVENT_STREAM and CHUNKED_DECLARED metadata, authentication UNVERIFIED. Capture started under the explicit owner approval; this does not alter the metadata classifier's bodyReadAuthorized=false default. The sample returned PREFIX_STOP and event responseClass=SSE_FRAMING_ONLY, captureStopRequired=true, freshness UNKNOWN, eventCategories empty. Runtime image/process/listener UNVERIFIED, causeConfirmed=false and retryAuthorized=false. Under the helper's non-final path, framing-only stop occurs at the fixed cumulative budget; it is NOT a first accepted application event.

No recognized data event or application console failure was established. SSE_FRAMING_ONLY is a conservative decoder category, not proof that the response contained only legitimate heartbeat messages: ignored fields/unknown framing can also yield no recognized data. Actual data schema, protocol semantics, main-container source and authentication completion remain UNKNOWN. No discarded event bytes were later inspected or reclassified.

Limits remained 3-second connect/8-second socket read, 12-second internal timer inside 15-second owned Windows Job, 64 KiB cumulative application-level budget, no redirects/retries/proxy inheritance or fallback. Response and owned worker/pipe cleanup completed. Restart requests ZERO; Azure mutations ZERO. No health/quota/ARM settings/UI/production/provider/registry/Docker/data operations. No larger budget, replay or automatic reconnection.

This channel has still yielded no usable main-application log event. Another unchanged stream sample or speculative parser expansion is not justified by this result. Next smallest step is offline review of the existing SCM log-file metadata listing contract (repository WP11 references `/api/logs/docker`) to establish whether a bounded inventory can distinguish main-app and Kudu files without exposing names, addresses, paths or content. Any inventory request or file-body read would require separate exact authority; neither occurred here. A main-container console failure, not another generic stream success, is the missing diagnostic evidence.

Only this evidence document changed. Eight fake adapter cases passed; existing 86-test repository qualification unchanged and not rerun for this append. UTF-8/LF/no BOM and git diff --check passed. PR #479 remains OPEN/UNMERGED; API exit-code-1/503 root cause remains UNKNOWN.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Offline log-file inventory contract review: no new parser justified

Owner approved the proposed offline inventory review only. ZERO Azure/SCM/credential/runtime operations, file listings, log-body reads, restarts or other mutations occurred. No discarded response or retained log content was accessed. Existing WP80 classifier/test code is unchanged in this pass.

Source reviewed: existing `wp11_research_diagnostic.py` fixes SCM_INDEX to the research app's `/api/logs/docker`, uses an authenticated GET with Accept application/json, rejects redirects, applies a 32 KiB body cap and bounded private-memory reading, then checks a list-of-dictionaries shape. The legacy helper also obtains a management-resource token and performs a second anonymous enforcement probe. It must NOT be invoked as a one-request inventory adapter: those extra operations/auth assumptions are outside a new exact single-listing scope. No helper function or CLI command was executed here.

The reviewed source does not establish inventory row field names, a reliable main-container role, payload freshness, exact running-image binding, or safe file selection. Its indexSchemaRecognized predicate accepts both an empty list and objects with no relevant fields. Four synthetic checks evaluated ONLY that extracted AST expression (not the surrounding function): empty list true, one empty object true, unrelated synthetic object true, non-list object false. All four matched the source predicate. This demonstrates a local evidence limit, not a defect or a finding about the actual Azure inventory.

Installed CLI bytecode inspection found `download_historical_logs` referencing `/dump`, an archive operation; it was not invoked and is not a narrow inventory substitute. A site-container log function also exists, but no verified classic-container inventory contract follows from its name or signature. No sidecar/classic mode assumptions, filename suffix heuristics or automatic href following were added. No raw filenames, paths, addresses, IDs, timestamps, links or credential values were acquired or emitted.

Decision: NO production/classifier code change. A new role classifier or single-file fetch cannot yet be source-backed using available offline material. Guessing that a particular filename suffix proves the main application would risk confusing Kudu/platform logs with API console output, the same attribution gap already observed. An empty or generically recognized index is not proof that main-app logs are absent.

The exact historical GET `/api/logs/docker` remains a candidate, not an executed or qualified new request. Missing prerequisites are an authoritative Microsoft/Kudu inventory row schema, file-role/freshness semantics and a supported exact body-read selection contract. The current offline-only scope did not fetch external source documentation or use a live listing to reverse-engineer those fields.

Next smallest action: separately authorize review of PUBLIC Microsoft/Kudu source/documentation to establish that contract (not Azure/SCM access, no credentials or live resource requests). Only after that review should a fake-tested fixed-output inventory projection and one precise listing proposal be considered. No additional stream capture, broad archive, filename-based guess or restart is recommended on the present evidence.

Validation: four synthetic AST shape checks passed; no application/helper source changed, so the existing 86-test qualification was not rerun for this documentation-only append. UTF-8/LF/no BOM, Markdown links/fences and git diff --check passed. Only this evidence document changed; PR #479 remains OPEN/UNMERGED. API exit-code-1 and health 503 cause remain UNKNOWN.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Public Microsoft/Kudu contract research and interpretation correction

Owner approved PUBLIC documentation/source review after the offline material proved insufficient. ZERO Azure/SCM/credential/runtime/log/data operations or mutations occurred. Public GitHub documentation and source were fetched through the GitHub connector and public web search; these are source-research operations, not reads of the deployed app. No actual inventory, href, log filename or retained response was accessed.

Authoritative references reviewed:

- [Microsoft custom-container documentation source](https://github.com/MicrosoftDocs/azure-docs/blob/main/articles/app-service/configure-custom-container.md), Kudu API section: `/api/logs/docker` returns Docker-log metadata; href can retrieve an individual file; ZIP is a different broader operation.
- [Azure App Service Linux documentation](https://github.com/Azure/app-service-linux-docs/blob/master/Things_You_Should_Know/things_you_should_know.md), Docker-log discovery section, supporting the inventory route.
- [KuduLite DiagnosticsController](https://github.com/Azure-App-Service/KuduLite/blob/3854066bb50c3a1933aebbc4143c06c47535be68/Kudu.Services/Diagnostics/DiagnosticsController.cs#L100), exact reviewed commit `3854066bb50c3a1933aebbc4143c06c47535be68` (public dev-branch snapshot dated 2021-09-10).
- [KuduLite Startup routing](https://github.com/Azure-App-Service/KuduLite/blob/3854066bb50c3a1933aebbc4143c06c47535be68/Kudu.Services.Web/Startup.cs#L644), inventory route plus separate ZIP route.
- [KuduLite LogStreamManager](https://github.com/Azure-App-Service/KuduLite/blob/3854066bb50c3a1933aebbc4143c06c47535be68/Kudu.Services/Diagnostics/LogStreamManager.cs#L115), media setting and body writer behavior.

The archived master snapshot was checked first; repository metadata identified dev as the default branch, and the findings above were verified against that pinned dev snapshot. These public versions are historical and do NOT establish which implementation/version the current Azure SCM service runs. Mutable documentation links are supporting references, not a runtime attestation.

### Inventory contract now supported by source

The controller returns a JSON sequence of objects with exactly these authored fields: machineName (filename-derived source string), lastUpdated (file LastWriteTimeUtc), size (file byte length), href (VFS link), path (full file path). It enumerates matching non-rolled Docker log names and uses source/date grouping with a historical threshold relative to the latest discovered date. This is not a guaranteed recent runtime event window or proof that every returned record belongs to the current main app. The controller opens up to ten files to refresh cached metadata but does not stream those file bodies to the inventory client. The separate ZIP operation is not required for listing.

There is NO explicit main-container role, container ID binding, process state or image digest in this row contract. machineName includes a filename-derived log source and must not be treated as a typed role or printed as an identity. lastUpdated means file modification time, not event or container startup time. href/path must remain private and must not be followed automatically. The source supports a narrow metadata-only projection design, not blind newest-file selection or a claim of main-app absence when the list is empty. No guessed filename role classifier was added.

### Important correction: event-stream media did not establish SSE framing

The reviewed LogStreamManager sets Content-Type to event-stream media but writes initial messages, tailed log lines, heartbeats and updates directly through Response.WriteAsync. Its inspected writers do NOT wrap each log line in standard data-field/blank-line SSE events. Thus a standard SSE-data parser can ignore useful raw lines and report SSE_FRAMING_ONLY for this implementation. The installed CLI's direct streamed-byte reader is consistent with that behavior.

Our earlier SSE parser direction was over-conservative and not source-backed as a match for this server implementation. Preserve its fail-closed behavior and prior results, but correct their interpretation: SSE_FRAMING_ONLY does NOT prove an empty log channel, valid heartbeat-only traffic, or absence of API console output. The actual discarded captures cannot be reclassified; current deployed body format remains UNKNOWN. This finding identifies a client-side contract mismatch hypothesis, NOT the API's startup crash/503 cause. No live parser mode was switched or media gate loosened in this pass.

A new synthetic regression demonstrates the distinction: one fake UTC raw log line yields SSE_FRAMING_ONLY under the SSE parser and EXIT_MENTION under the existing text-tail sanitizer, with runtime identity UNVERIFIED and causeConfirmed=false in both. It tests the limitation without using any real captured bytes. No production/classifier implementation changed.

### Next precise candidate: inventory metadata only

The source review supports preparing one exact authenticated GET to the research SCM `/api/logs/docker`, using existing-sign-in App Service bearer authentication (not publishing/research/provider keys), TLS verification, Accept application/json, Accept-Encoding identity, no proxy/redirect/retry/fallback, 3-second connect/10-second read bounds inside a 15-second owned-worker deadline, at most 32 KiB transient response and 32 rows. Validate JSON duplicate keys, bounded source-backed row types, UTC file-time syntax and safe same-origin VFS references locally without printing or following them. Reject changed/unknown schemas, cap violations, unsafe links or access/transport errors and STOP. Output only fixed inventory/schema/freshness/selection classifications; no filenames, source identities, paths, URLs, exact file sizes/timestamps or log content. Runtime/main-role/image binding must remain UNVERIFIED.

No row should be selected or file body retrieved in that same operation. A subsequent exact single-file read needs separately reviewed role selection and explicit authority. The legacy WP11 function must not be executed unchanged because of its extra anonymous/auth operations. Before any live inventory request, prepare and fake-test the narrow projection/temporary adapter in the existing approved WP80 paths. This paragraph is a proposal, not live authority.

Validation: `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp80_diagnostic_error.py -q` passed 77 distinct tests (one new source-contract regression); focused WP75 suite passed 10, total 87, zero skips. Syntax, UTF-8/LF/no BOM, Markdown links/fences and git diff --check passed. Changed only this append-only evidence and the existing WP80 test file. The existing classifier is unchanged; no new parser/dependency/framework. PR #479 remains OPEN/UNMERGED.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Offline inventory projection preparation

Owner approved preparing and fake-testing the source-backed inventory projection. This pass made ZERO Azure/SCM/authentication/credential/runtime operations and ZERO mutations. No inventory/file listing, href, retained response or log body was accessed. Changes remain in the existing classifier/test paths and this append-only evidence.

Added pure `classify_log_inventory(body, observed_utc)`. It accepts at most 32 KiB UTF-8 JSON and 32 rows, rejects duplicate keys/nonstandard constants, and requires the exact five documented row fields. Source identifier, file-time, href and path strings have explicit caps (256/40/2048/1024 characters), no controls; size is an integer within a nonnegative signed-64-bit range, excluding booleans. Caller observation time must be UTC-aware. File times require UTC syntax/calendar validity; future metadata fails closed. The output's WITHIN_24H_PRESENT/OLDER_ONLY is an explicit local file-modification-age policy, NOT startup/event freshness or health.

References are validated privately: HTTPS to only the exact research SCM origin, no credentials/query/fragment, only default/443 port, VFS under `/api/vfs/LogFiles/`, matching the row's `/home/LogFiles/` path. Traversal, empty path components, backslashes, double encoding and mismatched paths are rejected. These are deliberately narrow local safety rules; a different valid server deployment/root/schema may be rejected, not silently generalized. No href is returned or followed. No filename/source/path, exact size/time, URL, hash or derived identity is emitted.

Output is exactly nine fixed enum/boolean fields: responseClass, schemaRecognized, inventoryState, fileTimeClass, mainContainerRole, runtimeImageIdentity, fileSelectionAuthorized, logBodyReadAuthorized and retryAuthorized. Empty inventory does not prove absence of main-app logs. A recent record does not establish current application output. Role/image remain UNVERIFIED; file selection/log-body read/retry remain false. Synthetic `_kudu` and `_default` source labels deliberately produce equal output; neither label becomes a runtime role assertion. Later invalid rows withhold earlier partial schema/freshness conclusions.

Validation actually run:

- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp80_diagnostic_error.py -q`: 93 distinct tests passed (77 prior + 16 inventory tests), zero skips.
- `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q`: 10 tests passed; total 103 distinct tests, zero skips.
- New coverage includes documented/empty inventories, 24-hour boundary/mixed/old/future timestamps, strict keys/types/caps, duplicate JSON/invalid UTF-8, unsafe cross-origin/auth/query/fragment links, traversal/double-encoding/path mismatch, valid encoded names/default HTTPS port, no filename-role inference, equal-output noninterference across private values/order, row/body cap boundaries, all-or-nothing conclusions and no network/process/file/output side effects.
- The first local run failed to import because the new expression lacked a closing parenthesis. It was corrected locally and the complete suite then passed. No live operation occurred before/after that correction.
- Syntax, UTF-8/LF/no BOM, Markdown links/fences and git diff --check passed. No application/dependency/port/image/auth/storage changes or new framework.

The next separately approvable operation is ONE exact authenticated GET to research SCM `/api/logs/docker`, with existing-sign-in App Service bearer class, TLS, strict JSON/no-auth-challenge/no-redirect/upgrade metadata checks, 3-second connect/10-second read inside a 15-second owned-worker deadline, 32 KiB streaming cap and this fixed inventory projection. Fake-test a small temporary adapter before the live operation. No publishing/service/provider key access, body persistence or raw output; no automatic href following, log-file body read, stream request, restart, retries/fallback, UI/production/settings/health/quota/provider/registry/data operation. Metadata JSON is read only within that explicitly approved inventory scope; logBodyReadAuthorized=false does not mean zero inventory bytes. Stop on any failure or unknown schema and leave main-role/runtime binding UNVERIFIED. No such request ran in this preparation pass.

PR #479 remains OPEN/UNMERGED. The API startup exit and health 503 cause remain UNKNOWN.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Separately approved inventory listing: recognized metadata

Owner approved one exact research SCM inventory GET, not a log download or restart. A temporary adapter was fake-tested against eight cases before execution: recognized/empty JSON inventories, unknown schema, unsafe reference, HTML/401/duplicate-header body blocking and exact 32 KiB acquisition stop. They verify one exact GET, closed response, requested-size/cumulative budget, fixed output, no private identifiers/token disclosure, no href following and zero restarts. No real requests were used in those tests.

Actual completion: 2026-11-22 11:25:03.609534 UTC / 2026-11-22 08:25:03.609534 America/Sao_Paulo. Worker elapsed 2.923 seconds. Exactly ONE authenticated GET targeted research SCM `/api/logs/docker`. HTTP 200 passed the strict JSON/header gate. Fixed projection: responseClass=INVENTORY_PROJECTED, schemaRecognized=true, inventoryState=NONEMPTY, fileTimeClass=WITHIN_24H_PRESENT. Main-container role and runtime-image identity remain UNVERIFIED; fileSelectionAuthorized=false, logBodyReadAuthorized=false, retryAuthorized=false. This confirms accessible recognized inventory metadata with at least one recent file modification, not a selected application log, current event freshness or API health.

Limits: TLS validation, existing-sign-in App Service bearer held transiently, no stream proxy inheritance, no redirect/retry/fallback; connect 3/read 10-second bounds within 15-second owned Windows Job; exact same research origin; 32 KiB response budget and 32-row classifier limit. The adapter requests at most the remaining application-level budget; reaching the exact cap without EOF fails closed rather than reading beyond it or accepting an unverified truncated JSON body. Actual observation completed below the cap. Unknown/changed/unsafe schemas would stop. Owned worker/process/pipe cleanup completed and response closed.

Inventory metadata was inspected only in transient worker memory by the pure projection. No raw metadata, filename/source, path, href, exact file timestamp/size, credential or body excerpt was printed or persisted. No file was selected, link followed, log body fetched or historical metadata retained for a later operation. Publishing/research/provider service keys were not requested. Restart requests ZERO; Azure mutations ZERO. No stream, health/quota, ARM settings, UI/production/provider/registry/Docker/data operations followed.

The prior root-cause gap is unchanged: this list has no typed main-container role or runtime digest. Recent file modification may concern Kudu/platform or application logs and is not evidence of a successful listener. A single-file selection must be explicitly reviewed/owner-assisted; do not infer main-app identity from a filename/source suffix or blindly download the latest entry. Any subsequent retrieval requires separate exact authority and bounded same-origin VFS validation; no file-body read is authorized by this listing result.

Next smallest step: define an owner-reviewed application-console file selection and one bounded sanitized single-file read, using the documented inventory/VFS contract. The successfully listed hrefs were not retained or exposed; no later selection may pretend to know their values. If a fresh inventory is required to obtain an approved selector, that additional metadata read must be included explicitly in the next scope. Do not automatically repeat the stream, list, follow all hrefs, request an archive or restart.

Evidence-only append. Eight fake adapter cases passed; the existing 103-test repository qualification was unchanged and not rerun. UTF-8/LF/no BOM and git diff --check passed. PR #479 remains OPEN/UNMERGED. API startup/503 cause remains UNKNOWN.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Separately approved application-output candidate selection and read

Owner approved defining a specific application-log selection and one bounded sanitized file read. Public [Microsoft employee troubleshooting guidance](https://learn.microsoft.com/en-gb/answers/questions/1139705/how-to-deploy-custom-docker-image-%28blazor-server-w) identifies `_default_docker.log` as an application-output diagnostic candidate. This is a filename convention, not a typed role/image attestation. The selector therefore kept role/runtime identity UNVERIFIED, required a direct-child dated default-output filename with file modification within 24 hours, excluded Kudu/SCM-labelled source strings, required a unique latest timestamp and nonempty size, and stopped on missing/empty/ambiguous candidates. It did not blindly follow every inventory href or assert main-app identity.

The operation used one fresh validated research SCM `/api/logs/docker` inventory because earlier hrefs had not been retained, then at most one selected same-origin VFS GET. Entire inventory schema/links were validated by the existing pure projection before selection. Source paths, identities and links remained transient and were not emitted/persisted. Cookies were cleared before the file request; the same existing-sign-in App Service bearer class was used, never publishing/research/provider keys. The file body required a strict plain/octet-stream HTTP 200 gate with no auth challenge, redirect, upgrade or unsupported encoding. No range support was assumed: the adapter read at most a 16 KiB PREFIX, not an unverified tail.

Eight fake adapter cases passed before execution: unique selection, Kudu-only/no-candidate, tied ambiguity, empty candidate, unsafe inventory link, file access denial, capped prefix and empty inventory. They verify at most two exact GETs, no cookie/raw/token disclosure, rejected bodies unread, no extra link following/restarts, and fixed unattributed stage extraction. No live request occurred in those fake cases.

Actual completion: 2026-11-22 11:32:50.965527 UTC / 2026-11-22 08:32:50.965527 America/Sao_Paulo. Worker elapsed 2.011 seconds. Exactly TWO SCM GETs: inventory HTTP 200, then one selected file HTTP 200. Selection classification RECENT_DEFAULT_OUTPUT_CANDIDATE_ROLE_UNVERIFIED. FileComplete=true: EOF arrived within the 16 KiB limit; no extra file or larger read followed.

Fixed file projection: BOUNDED_TAIL, freshness NO_IN_WINDOW_RECORD for the final 15-minute event window, eventCategories empty, runtimeImageIdentity/mainProcessLiveness/listener UNVERIFIED, causeConfirmed=false, retryAuthorized=false. The existing fixed diagnostic matcher also found no recognized startup-stage or listener marker; candidateBound=false. These are matcher results, not proof that the file was empty, contained no error, or belonged to the current accepted image. File-modification freshness from inventory does not imply fresh log-event timestamps. No raw content was subsequently inspected or retained for another parser pass.

Bounds: connect 3/read 10-second per-request socket timeouts, 30-second total owned Windows Job for both requests, inventory 32 KiB/32 rows and file 16 KiB. File reads used at most the remaining budget; an incomplete capped final line would be dropped before matching. The file happened to complete. Only bounded supported UTF-8 tail input could reach the existing source-fixed stage matcher, whose stages are explicitly unattributed and not event freshness proof. Owned worker/pipe and response cleanup completed.

ZERO restart requests or Azure mutations. No settings/key/config/logging change, deployment, health/quota, stream, UI/production, provider/registry/Docker/import/data operation. The selection/read relied on this NEW explicit owner authority; the pure inventory classifier's fileSelectionAuthorized/logBodyReadAuthorized=false defaults were not weakened or treated as self-authorization.

The file is accessible, but a current recognized API startup error remains missing. Do not automatically fetch another file, repeat the inventory or restart. Next smallest step is offline reconciliation of the accepted image's recorded source revision and emitted diagnostic vocabulary before proposing any additional live read or fresh startup capture. A missing match against current source markers must not be interpreted as absence of an error in an older image. The deployment/exit-code-1/503 cause remains UNKNOWN.

Only this append-only evidence changed. Eight fake adapter cases passed; existing 103-test repository qualification unchanged and not rerun. UTF-8/LF/no BOM and git diff --check passed. PR #479 remains OPEN/UNMERGED.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Offline accepted-image source and diagnostic-vocabulary reconciliation

Owner approved offline receipt/source review only. ZERO Azure/SCM/registry/Docker/image-inspection/credential/log/runtime operations or mutations occurred. No retained preparation, private image/archive, dataset or discarded log bytes were inspected. Review used tracked receipts and local Git history; source and artifact identity remain separate.

The [setting-name/provenance receipt](prompters/release-1.15-setting-name-presence-and-image-provenance.md) explicitly records accepted research local/index `1edebb818...` and runtime child `2744c4dc...` with exact source commit UNKNOWN. [WP55 qualification](prompters/release-1.15-wp55-ohlc-acquisition-qualification.md), [WP57 qualification](prompters/release-1.15-wp57-corrected-ui-local-browser-qualification.md), [WP61 qualification](prompters/release-1.15-wp61-post-merge-local-browser-qualification.md) and [Phase G publication](prompters/release-1.15-wp58-phase-g-staging-deployment-evidence.md) preserve the original API identifier. The Phase G byte audit records bootstrap correspondence, not a complete managed-assembly-to-source attestation. A source revision for the separately rebuilt UI does not supply an API revision.

Exact accepted API source revision therefore remains UNKNOWN from this public repository evidence. No new hash, tag or chronology inference is substituted for the missing link. We cannot establish which startup diagnostic vocabulary the accepted API assembly contains, or infer that the configured artifact is wrong merely because current-source messages were not matched.

Git source change `92da836bf36ffdb790549a1fe30f1e4665203f5e` introduced fixed startup stage labels and listener-start output. Its parent source has a generic catch message only: `Research snapshot service unavailable.` followed by return code 1, with no listener-start marker. The later source emits `Research snapshot service unavailable. stage=<fixed stage>` and `Research snapshot listener started.`. Supported stages are CONFIGURATION, TRANSPORT, TLS, STORAGE, LISTENER and SERVING. The provenance receipt attributes those diagnostics to source `c4513aebfb69afac9ecbc7633e79e69cae9e950a` and separately published diagnostic child `d33626a9...`, not automatically to the accepted `2744c4dc...` child. That diagnostic artifact remains a distinct unpromoted alternative, not a drop-in substitute authorized by this review.

Three local Git-content assertions passed: parent has the generic catch, parent lacks listener marker, and the recorded diagnostic source contains staged/listener markers. They did not execute C# or inspect either image. The bootstrap's fixed reimport/unavailable messages were also reviewed as source, not attested accepted-image bytes.

Conclusion: missing current-source stage/listener matches are not evidence of successful startup or absence of errors. Even observing the older generic failure message would not identify configuration, TLS, storage or bind cause because that source intentionally suppresses exception details. Re-reading the same old file only to recognize the generic phrase would add little beyond the owner's already established startup exit code 1. The actual discarded file content cannot be retrospectively reclassified, and this pass does not assert that it contained the generic phrase.

One synthetic regression documents the vocabulary gap without changing classifier behavior: a fake generic UTC failure line yields no stage marker; the staged STORAGE variant yields the fixed storage marker, with causeConfirmed=false and identity UNVERIFIED in both. No application/helper implementation was modified and no new parser was added.

Validation: WP80 offline suite 94 distinct tests passed (one new source-vocabulary regression), WP75 focused suite 10 passed; total 104, zero skips. Source-content assertions 3 passed separately and are not counted as unit tests. Syntax, UTF-8/LF/no BOM, Markdown link/fence review and git diff --check passed. Changed only the existing WP80 test and append-only evidence paths.

Next smallest decision: owner review of a SOURCE-BOUND diagnostic startup plan if richer startup evidence is required. Such a plan must preserve the accepted-image baseline, explicitly choose and qualify any diagnostic artifact/source, establish a usable log retrieval channel and rollback safeguards, and obtain separate build/publication/deployment/restart authority as applicable. No automatic substitution of the diagnostic child, configuration fix or further blind stream/file read is authorized here. A source-bound diagnostic startup or a directly supplied application exception/category is needed to resolve cause; present source/receipt evidence alone cannot do so. API-first/UI-second boundaries remain intact; no UI or production work.

PR #479 remains OPEN/UNMERGED; API startup exit-code-1/503 cause remains UNKNOWN.

`API_HEALTH_ACCEPTED=false`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Accepted API image source-provenance reconciliation

`ACCEPTED_API_SOURCE_REVISION=UNKNOWN`. Historical receipts link the accepted Linux/amd64 child to the original WP55 preparation and Phase G publication. The missing link is a trustworthy receipt that binds the full Worker build inputs and exact Git tree to that child.

| Source | Immutable identifier | What it proves | What remains unknown | Confidence |
| --- | --- | --- | --- | --- |
| [WP55 Phase B receipt](prompters/release-1.15-wp55-ohlc-acquisition-qualification.md) and [PR #443](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/443) | Merge `4a6bc2b901d6f6ff0081dad4597064017acb86f9`; Worker DLL hash `3b5dac87c3966931ad909c7f165a7cec65fa7b89a2a4f4604b57b262a0024a48`; publication-map hash `3d7ac2251abfa9aa5b9ed2019b2cf5ae15c76cf7bd9cd8b56587b5215276b221` | Records local Prepare, Release Worker publish, and original Research image identifier. The inspected helper stages published Worker files under `research/` and the bootstrap under `container/release_1_15/` in the generated context and builds using `container/release_1_15/research-api.Dockerfile` for Linux/amd64. | Exact checkout tree and complete compiler inputs at publish time. GitHub PR base/head/merge are document and code history, without a build-commit receipt. | Recorded output identity, source UNKNOWN |
| [Phase G publication receipt](prompters/release-1.15-wp58-phase-g-staging-deployment-evidence.md) | OCI index `sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028` ? Linux/amd64 child `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3` ? config `sha256:c2ef542e32f10badc5f9b4f82ca1e67463b685cdf380ffee48f6a31d6aad8096` | Records historical hash verification of the publication chain, alongside the bootstrap source comparison. | Exact managed assembly source commit. Publication tag and bootstrap correspondence cannot bind all Worker inputs. | Historical artifact mapping recorded, source UNKNOWN |
| GitHub Actions, releases and Git tags | Main `632e636b9c39c94130aa1b58336f13a6d15471a2`, WP80 pre-update head `5213e8c9f53013bef1ec46be2283213276ea1b0b` | GitHub returned zero workflows, runs and artifacts. Four releases and four Git tags were examined with no accepted digest or publication-label match. No workflow definitions were found in the checked main history. | No run ID, attempt, triggering SHA or artifact attestation was available from these inventories. They do not establish whether a receipt exists elsewhere. | Inventories verified, build linkage UNKNOWN |
| [Separate WP62 diagnostic build receipt](prompters/release-1.15-wp62a-stop-quota-offline-investigation.md) and [publication receipt](prompters/release-1.15-wp62b-bounded-quota-event-observation.md) | Source `c4513aebfb69afac9ecbc7633e79e69cae9e950a`, diagnostic child `sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c` | Records a distinct source-bound diagnostic build and child. | Accepted child source and Azure runtime state. | Separate candidate |

The recorded Prepare script SHA-256 `62471759b4a8d858616cdf19a92c6391b0f1211a8c2a0e2b4f6570a69f1a6c25` matches the script blob at the WP55 PR base, head and merge commit. This confirms script correspondence across three revisions without uniquely binding the Worker source to any of them. WP57 retained the original API. Its PR metadata supplies no new API build receipt.

The conditional Git comparison uses the parent of `92da836bf36ffdb790549a1fe30f1e4665203f5e` and the recorded WP62 diagnostic source. The earlier Dockerfile invokes the shell bootstrap, which in serve mode execs the Worker in research snapshot API mode. Both endpoint versions use port 52731. The earlier catch emits the generic startup failure and returns 1. The diagnostic source retains exit 1 and adds fixed stage labels and a listener-start marker. The accepted child's message vocabulary remains unverified. Prior captures retain their original classifications, including the absence of recognized markers. The discarded bytes cannot be reclassified.

If an existing immutable receipt surfaces, check for a full source SHA and complete build-input identity bound to the accepted child. In its absence, the smallest actionable proposal is a separately reviewed diagnostic-build plan from exact commit `c4513aebfb69afac9ecbc7633e79e69cae9e950a`. Use the existing tracked-source build with `container/release_1_15/research-api.Dockerfile`, pinned cached inputs, a manifest of the Worker publish tree and bootstrap, and Linux/amd64. Record the source tree, input hashes, builder versions, platform, OCI index/child/config hashes and an immutable provenance receipt binding those fields. Qualify the generic/staged failure behavior and listener marker with synthetic cases and check port 52731 and bootstrap parity. Treat the resulting artifact as diagnostic evidence for that build. Build execution and any publication or staging observation are separate scopes.

This pass used local Git, tracked public receipts and GitHub metadata. Azure, SCM, runtime, health, quota, logs, registry, image builds and credential reads for Azure/SCM/registry each had zero operations. Azure mutations were zero. Actual runtime identity, listener and startup/503 root cause remain unverified.

Offline verification passed 94 WP80 tests and 10 WP75 tests with zero skips. Python syntax, UTF-8/LF/no BOM and Git whitespace checks passed. `API_HEALTH_ACCEPTED=false`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.


## Existing WP62 diagnostic API image qualification

`DIAGNOSTIC_IMAGE_SOURCE_BOUND=VERIFIED_BY_HISTORICAL_RECEIPTS`. `DIAGNOSTIC_CAPTURE_READY=false`. Reuse of the existing diagnostic child is justified for a future staging observation once capture and recovery are qualified. The application failure remains unexplained. Fresh GitHub metadata showed main at `632e636b9c39c94130aa1b58336f13a6d15471a2` and the open PR branch at `9650ae16ad91be2ad47757863adc6775406006da` before editing. Owner work and earlier evidence were preserved.

| Evidence | Recorded or inspected | Limits |
| --- | --- | --- |
| Source and build inputs | WP62 records a tracked-source Git archive of `c4513aebfb69afac9ecbc7633e79e69cae9e950a` prepared with `eng/azure-cli/r1.15-qualification/wp35_research_image.py` and `container/release_1_15/research-api.Dockerfile`. Release framework-dependent .NET 10 Worker publish used a cache-only restore, then no restore, UseAppHost=false and ContinuousIntegrationBuild=true. Docker used Linux/amd64, no pull and no network. The pinned ASP.NET base digest is `sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f`. | Historical build receipt. The full SDK/builder version set and complete per-file input hashes are unavailable in the tracked receipt. |
| Worker context and bootstrap | The receipt records a 42-file context, Worker/runtimeconfig, Linux native SQLite asset and a retained local context hash manifest. Direct Git inspection at the exact commit confirms the Dockerfile copies the Worker publish from `research/` and the shell bootstrap, runs as UID/GID 1654, and sets bind `0.0.0.0` and port 52731. The serve branch execs the Worker with `--research-snapshot-api`. | The retained hash manifest and image layers were not reopened. Fresh inspection verified Git source only. |
| Published identity | WP62 links the audited local candidate and publication to OCI index `sha256:153d2b1ae5e2b856c1ca502849c05fe04ec09950b453b7b8b7d4b47f1dc7205f`. Historical raw manifest hashing verified the index and its single Linux/amd64 child `sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c`. The child references config `sha256:1a919126ffa680a4c588e92603d9a412fbd4f993008aabeb53a0f5c0661d7a53`. | Config descriptor only. No historical independent config-blob hash or fresh registry verification in this pass. |
| Offline image qualification | WP62 records 57 focused source tests and two local fake-only Linux runs using the exact candidate. Missing configuration emitted CONFIGURATION and exited 1. Fake staging reached the listener-start marker before an owned stop. Captured output withheld the synthetic key. | These checks support the diagnostic role. They do not establish how the candidate will start or serve on Azure. |

Build, publication and local test evidence comes from the [tracked WP62 receipts](prompters/release-1.15-wp62b-bounded-quota-event-observation.md). The earlier log-capture blocker and the owner-reported main-container startup exit 1 remain part of the evidence.

At the diagnostic commit, the fixed catch line reports CONFIGURATION for initial required-setting reads, TRANSPORT for transport validation, TLS for certificate loading, STORAGE for opening or validating existing state, LISTENER for bind validation and app construction, and SERVING for failures after entering RunAsync. That last label can include startup failures. The catch returns exit 1 and suppresses exception details. The listener-start line comes from ApplicationStarted and shows that callback ran if the line can be attributed to the diagnostic process. It cannot establish sustained liveness or successful public ingress. A stage narrows investigation, with the exact failing condition and exception still unknown. Earlier source emitted a generic failure line and returned 1. The accepted child `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3` retains `ACCEPTED_API_SOURCE_REVISION=UNKNOWN` and its message vocabulary remains unverified.

The proposed capture uses one exact research SCM inventory GET and at most one validated same-origin VFS GET for a unique application-output candidate. This is the bounded route that previously delivered file bytes. Generic streaming yielded no attributable startup record and the ARM log-tail request timed out. Before using the route for staging, fake-test a narrow projector that recognizes all six source-fixed stages on complete UTC-stamped lines inside the deployment window. Reject stale, incomplete or conflicting records. Use a private, explicitly reviewed role and attempt witness independent of the filename, file time, configured child and stage text. If the witness is absent, report only an unattributed category and keep capture readiness false. Keep the existing 32 KiB/32-row inventory and 16 KiB file limits, 30 seconds total, and fixed output. Unknown/ambiguous schemas or files stop the read. WP62's generic matcher has candidateBound=false and its live adapter remains disabled. The next separately approvable action is that offline capture-and-attribution qualification, with a concrete witness contract as its prerequisite.

The following substitution and rollback sequence is future-only and requires separate exact authorization for any deployment, restart or publication. Confirm the capture witness and a private recovery baseline before the first write. The only target is `aiqr115researchstagingwus27f5eabb5`. Check current quota and ARM site/config readbacks for exact identity, accepted child `2744c4dc...75efc3`, empty startup override, HTTPS-only ingress on 443 and routing to 52731. Record the baseline and prepare rollback before proceeding. Gate on drift, incomplete readback or quota pressure. Preserve existing authentication, TLS, storage, logging and other settings, with UI and production excluded.

Apply one research API image-only update to `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c`, allowing the configuration-triggered startup and zero explicit restarts in this proposal. Read back ARM site/config and confirm the configured child and preserved startup/routing/HTTPS fields. Within one 180-second observation window, use the qualified startup capture and, if it remains safe, one anonymous HTTPS /health observation with an eight-second request bound. Accept health evidence only on exact 200 Alive. ARM Running and configured digest are management metadata. An unavailable, ambiguous or unattributed capture, failed health, drift or deadline triggers the separately authorized single restore of `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`. Verify the rollback with bounded ARM site/config readbacks. Report uncertainty and stop if restoration cannot be verified. Candidate retention, if ever requested, needs an explicit decision. No authenticated data probes are part of this plan.

Offline validation used `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p <suite> -q`. WP80 diagnostic error ran 94 passed, WP75 staging plan ran 10 passed, and WP62 staging observation ran 20 passed with four Windows opt-in cases skipped. Existing tests cover the offline behavior reviewed here. Only this evidence document changed. Syntax, UTF-8/LF/no BOM, reference links and Git diff checks passed. There were zero Azure, SCM, registry, Docker, provider, health, quota, logging, data, credential or runtime operations, builds, publications, deployments, restarts or mutations in this pass.

`API_HEALTH_ACCEPTED=false`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.


## Offline diagnostic log attribution qualification

`DIAGNOSTIC_CAPTURE_READY=false`. The evaluator is qualified with synthetic inputs. The actual SCM inventory/VFS contract cannot by itself attribute a diagnostic record to the main API container, the running WP62 child and one startup attempt. Historical WP62 receipts still bind the diagnostic artifact to its source. The accepted image source, runtime listener and cause of the reported exit 1 and HTTP 503 remain unverified.

| Evidence class | Available fields and limits |
| --- | --- |
| Documented and inspected | Microsoft documents the Docker log inventory and individual VFS links. The previously reviewed KuduLite source returns `machineName`, `lastUpdated`, `size`, `href` and `path`. Source names derive from filenames, and last-write time is file metadata. VFS provides file bytes. Neither response supplies a trusted role, running child or attempt binding. |
| Historically observed | One bounded inventory and VFS read succeeded. The selected output candidate lacked a recognized fresh startup marker and its role remained unverified. Historical ARM configuration identified a configured image only. Prior results and discarded bytes remain as recorded. |
| Synthetic contract only | A trusted complete attempt roster supplies instance, container and attempt IDs, main role, observed runtime child, start and observed-through UTC. A separately trusted record envelope supplies the same IDs, the occurrence timestamp, combined relative clock uncertainty and SHA-256 of the exact complete message. The caller supplies the independently established current observation window. No verified Azure source for this envelope has been identified. |

The pure evaluator checks consistency of inputs whose origin and completeness the caller has already verified. It accepts one attempt only and withholds attribution for overlapping or multiple attempts or instances. Each metadata document is capped at 4 KiB with duplicate keys rejected. The message is capped at 256 bytes and must exactly match one of the six source-fixed stage lines or the listener-start line. A trusted envelope must authenticate the actual record occurrence and its exact bytes. Matching a hash of a repeated stage phrase alone cannot distinguish occurrences across restarts. The clock margin must be supported independently, capped at 2 seconds of combined relative uncertainty, and fit on both sides of the timestamp within the attempt bounds. The independently supplied observation window is capped at 180 seconds. The trusted attempt upper bound must be clamped to the actual termination time when known. Unknown attempt bounds or clock uncertainty cannot be filled from file time.

The evaluator exposes separate main-container, diagnostic-image, startup-attempt and in-window flags. It emits a fixed category only when all four bindings and the exact message are supported. Outputs contain no raw records, IDs, digests, filenames, URLs or exception details. A positive synthetic result establishes consistency under the fixture's trust assumptions. It grants no live authority, health acceptance or running-image attestation for Azure.

The missing prerequisite is one independently verifiable platform or log-collector witness that binds an exact record occurrence to the main container, canonical running Linux/amd64 child, and a unique startup attempt with UTC bounds and clock uncertainty. The witness must correlate the particular occurrence to the selected VFS source or delivery event. An app-only marker would need separate trusted attempt and runtime identity binding. Neither the historic exit-code record nor SCM file metadata currently provides this witness.

### PROPOSED / NOT AUTHORIZED

1. Qualify that witness contract before a staging write. For the exact research staging app `aiqr115researchstagingwus27f5eabb5`, verify quota, exact target, and a private recovery baseline. Record one bounded pre-change runtime and attempt snapshot via the qualified witness. Require the configured rollback child `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`, empty startup override, routing port 52731 and HTTPS ingress 443. Abort on drift, missing baseline or inadequate quota.
2. Apply one image-only change to `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c`. Preserve settings, startup, auth, TLS, storage and logging. Verify the ARM readback against the baseline and requested image. Obtain one bounded post-change runtime witness for the main container's actual child and the new attempt, with the observation window anchored to this operation. Stop on ambiguity, drift or reused attempt identity. Use at most 15 seconds per management or witness read within the approved window.
3. Within one 180-second observation window, use one exact SCM inventory GET and at most one same-origin VFS GET for the uniquely witness-linked file. Use TLS, a 3-second connect and 10-second read timeout per request, a 30-second total worker deadline, 32 KiB/32 inventory rows, and a 16 KiB file cap. Enforce the budgets before accumulation and discard any truncated record. Abort on malformed metadata, conflicting records, rotation without binding, missing witness, transport failure or cap exhaustion. Return fixed categories only. If safe, make one anonymous HTTPS /health observation capped at 8 seconds, with exact 200 Alive as the health criterion.
4. On failed health, unavailable attribution, drift or deadline, restore the exact accepted baseline once under separate rollback authorization and verify with bounded readbacks. Report unresolved restoration if verification fails. No automated restart, retries, alternate log selection, UI or production operations are included. The image substitution, witness read, capture, health check and rollback all require an explicit future scope.

Next separately approvable action is an evidence-only review of one specific platform or collector witness against the proposed occurrence-binding contract. If it cannot supply the required fields and provenance, keep staging substitution blocked. No additional generic log read is proposed.

Validation used `python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p <suite> -q`. WP80 diagnostic error passed 107 tests, including 13 new attribution tests. WP75 staging plan passed 10. WP62 staging observation passed 20 with four Windows opt-in skips. Total 141 discovered, 137 passed, four skipped. Syntax, UTF-8/LF/no BOM, Markdown and Git diff checks passed. The scope was the offline helper, its tests and this append. Azure, SCM, registry, Docker, credential, health, quota, log, build, deployment and runtime operations each numbered zero. No mutations to Azure occurred.

`API_HEALTH_ACCEPTED=false`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.


## Azure Monitor startup attribution evidence review

`DIAGNOSTIC_CAPTURE_READY=false`. The published App Service console and platform log schemas can support container-level correlation. They do not document the full main-container, running-image and startup-attempt binding required for WP80.

| Required evidence | Documented Azure Monitor evidence | Qualification |
| --- | --- | --- |
| Exact record and container | AppServiceConsoleLogs provides ResultDescription, ContainerId, _ResourceId, Host and TimeGenerated. | A console entry can be associated with its reported resource, container and event time. The schema gives no occurrence link to a particular SCM VFS file record. |
| Main role and actual Linux/amd64 child | Both schemas provide ContainerId and Host. | Neither schema documents a typed main role, immutable running image digest or a cross-table guarantee that the ID distinguishes successive process starts. |
| Unique attempt and bounds | AppServicePlatformLogs adds ActivityId, DeploymentId, OperationName and TimeGenerated. | ActivityId is documented for event correlation and DeploymentId identifies a deployment. Neither is documented as a unique startup-attempt identifier or a complete start/end roster. The schemas provide no relative clock uncertainty bound. |

Sources: [AppServiceConsoleLogs schema](https://learn.microsoft.com/en-us/azure/azure-monitor/reference/tables/appserviceconsolelogs) and [AppServicePlatformLogs schema](https://learn.microsoft.com/en-us/azure/azure-monitor/reference/tables/appserviceplatformlogs). A join on resource and container could help narrow an investigation if both streams were available. Joining them, parsing free-form messages, or substituting deployment time for attempt bounds would leave essential attribution unverified. Workspace availability, logging configuration and actual row contents were outside this review.

One next evidence-only action is to obtain a Microsoft App Service capability statement or documented contract answering this question: Can an existing platform record or collector bind one console occurrence to the main container, actual Linux/amd64 image digest, unique process startup attempt, and authoritative UTC bounds with stated timestamp uncertainty? That contract must state how the record can be correlated without guessing from filenames, timestamps or message text. The proposal requires no diagnostic setting change, workspace provisioning or live log query. The separate staging substitution remains blocked pending that evidence.

Only this evidence document changed. Append integrity, Markdown table and link structure, UTF-8/LF/no BOM and Git diff checks passed. The existing 137-pass, four-skip offline test baseline was left as historical evidence for this documentation-only pass. Azure resource, SCM, registry, Docker, credential, runtime, log-query and mutation operation counts were all zero. The API exit-code-1 and HTTP 503 cause remain unknown.

`API_HEALTH_ACCEPTED=false` ? `PUBLIC_RUNTIME_VERIFIED=False` ? `liveAttemptReady=false`

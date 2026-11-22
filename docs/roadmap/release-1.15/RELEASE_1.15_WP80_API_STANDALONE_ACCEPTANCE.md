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

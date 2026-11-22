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

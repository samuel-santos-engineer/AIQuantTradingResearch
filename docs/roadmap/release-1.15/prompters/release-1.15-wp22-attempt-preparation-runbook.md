# Release 1.15 WP22 - Disabled named attempt and read-only baseline validation

Status: implementation for owner review; not live deployment authority.
Baseline: main `386be2a0a6758522f2c73c138df0bff10a61d943` (merged PR #411).
All authored text is UTF-8 with LF line endings. Windows PowerShell 5.1 only.

## Boundary

WP22 adds no deployment mode. The default entry runs only offline unit tests.
Preparation delegates to WP21's existing fresh UUID/owner-and-System ACL path.
Approval flags remain false. No historical attempt is reused or reset. The live
controller and independent recovery files are unchanged.

Explicit read-only inspection requires an existing authorized Azure CLI session
and a newly prepared private attempt. It uses the installed Azure CLI REST bridge,
with a second allowlist permitting exactly eight ARM operations per capture:

| Method | Exact resource suffix | Resource |
| --- | --- | --- |
| GET | empty | fixed research site |
| GET | empty | fixed existing F1 plan |
| GET | /config/web | fixed research site |
| POST | /config/appsettings/list | fixed research site |
| POST | /config/connectionstrings/list | fixed research site |
| GET | /config/logs | fixed research site |
| GET | /config/slotConfigNames | fixed research site |
| GET | /slots | fixed research site |

POST list operations read secrets but do not modify Azure. Request bodies,
stop/start, PUT, PATCH, substituted resources and pagination are rejected before
transport. No automatic login, role changes, provider requests, application HTTPS
probes, registry activity or UI operations occur. Authentication/RBAC failure
blocks the operation with a fixed sanitized failure category.

## Coverage and evidence limits

Two captures must match completely. They include original image/startup and full
web properties, full settings, connection values and types, slot flags, logging,
HTTPS-only and public-network state, empty slot inventory, plan identity/full SKU,
site kind/region and original Running state. Plan location is also checked. Missing,
malformed, paginated, wrong-target, non-F1, non-running or drifting captures block.
Strict equality can also reject benign changing web properties; do not weaken it
without reviewing actual sanitized failure evidence and the required contract.

Full configuration remains in memory. Only WP21 management-worker ownership records
and new-only sanitized `wp22-observation-<UUID>.json` records are stored under the
private ACL directory. No WP21 `baseline.json` or `consumed.json` is written. No
configuration hash or secret-bearing raw exception is published. Observation IDs
are separate from the single-use live attempt. A present printable 32-256 character
service key in the captured baseline or existing process environment produces only
a boolean; key material is neither copied nor persisted. It does not prove live
authentication and does not enable approval.

Each of 16 sequential ARM calls inherits WP21's 40-second worker watchdog and
45-second parent timeout; inspection is bounded by those limits, ACL/preflight
limits and the disabled template's 15-minute initial freshness window. Freshness,
code identities, disabled approval and absence of consumed/live baseline files are
rechecked after reads. A partially completed inspection is not a live attempt;
there has been no configuration mutation to roll back. Do not delete old evidence.

Real ARM schemas have NOT been observed in this Codex session. Microsoft REST
reference retrieval was unavailable during implementation; no schema compatibility
claim is based solely on fixtures. WP22 reuses the existing WP21 capture schema and
fails closed on any mismatch. Live service-key availability and full rollback
sufficiency remain UNVERIFIED until the operator returns actual read-only evidence.
This preparation observation is not the live immutable baseline: WP21 must capture
and compare a fresh baseline again immediately before any separately approved write.

## Operator commands from repository root

Use the reviewed PR checkout, not a different checkout with missing WP21 files.
Do not use execution-policy switches or change policy, profile, registry or identity.
If policy refuses the entry, stop and report the block in your authorized session.

Offline WP22 checks:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1
```

WP21 controller qualification (offline tests and real operator preflight):

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1
```

Optional fresh disabled attempt preparation (no Azure operation):

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -PreparePrivateAttempt
```

Keep the returned UUID. Set the existing WP21 directory variable locally without
printing its resolved value. Replace the placeholder with that UUID:

```powershell
$attemptId='<returned UUID>'
$env:AIQ_WP21_ATTEMPT_DIRECTORY=Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $attemptId
```

In the already authenticated, owner-authorized Azure session, explicitly request
read-only control-plane validation within the template freshness window:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -InspectAzureReadOnly
```

Do not print approval/configuration files or tokens. Return only sanitized JSON.
A failure reason covers prerequisite/auth/schema/drift failures without revealing
settings names or raw Azure errors. If expired, prepare a new disabled attempt;
do not silently update approval/code identities or recycle a consumed UUID. If the
service-key availability boolean is false, leave it false and request owner review;
never generate or paste a key into command arguments, reports or source files.

Even a successful read-only report has `approvalEnabled=false`,
`liveControllerInvoked=false` and `liveAttemptReady=false`. A later owner decision
must approve one specific UUID, exact image and research app, bounded health and
unauthenticated checks, and unconditional independent rollback. The owner may then
privately arm it following the WP21 runbook. No such live command is authorized here.
Independent recovery must be available before any future attempt.

## Recorded offline evidence

- WP22: 19 no-network tests passed; every Azure and operator-policy boundary mocked.
- WP21: 38 tests passed, including owned local Worker termination qualification.
- WP19: 19 tests passed. WP18: 19 tests passed. Total: 95 focused tests.
- Windows PowerShell 5.1.26100.9711 parser: zero new-entry parse errors.
  Requested build 5.1.26100.9444 was not available for execution.
- Actual new PowerShell `-File` execution was refused by this Codex session's
  execution policy. No bypass or alternate identity/interpreter was attempted.
- No private UUID was prepared and no real Azure read was performed by Codex.
- Owner-provided WP21 operator result: 38 tests and real preflight passed under
  Process RemoteSigned; this is historical operator evidence, not WP22 execution.
- No application code, dependencies, WP21 files, Azure state or public gates changed.
- Preservation: implementation lives in an isolated worktree; the original dirty
  checkout was neither switched nor edited. Its pre-existing untracked WP22 prompt
  remains there and is not swept into this PR.

Tests cover read-only inventory, body/mutation/substitution rejection, pagination,
plan region, disabled/fresh template, code mismatch, consumed/missing prerequisites,
policy/auth failures, malformed/missing capture categories, drift, post-read expiry,
boolean-only service-key channels, secret redaction/report bounds, new-only evidence
and absence of live baseline/consumed creation. The existing WP21 suite retains the
fresh UUID and ACL-preparation tests; WP22 delegates to that implementation.

Remaining obligations: real operator WP22 preflight and preparation, authenticated
read-only schema/baseline validation, secure key-channel evidence, PR review and
separate named live approval. Mocked captures do not prove Azure rollback behavior,
process health, authentic data, filesystem durability or production readiness.

`RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED=True`;
`PUBLIC_RUNTIME_VERIFIED=False`; live deployment/public activation remain NO-GO.


## WP24 diagnostic correction - owner review required

Implementation baseline: merged WP22 `5cc708a9a2035e95988edf1a2ad4104460ba202b`.
The WP23 operator passed offline/preflight gates and prepared a disabled UUID, but
Azure inspection failed with an undifferentiated category. That historical attempt
is preserved: do not inspect it again, extend it, arm it or reuse its records.
`serviceKeyChannelAvailable=false` in that earlier failure meant unverified.

WP24 changes only the read-only inspector, its tests and this runbook. The WP21
runtime/transport, live controller, recovery, identities, image, time limits,
read-only operation allowlist and two-capture equality remain unchanged. No Azure
request, private-attempt file access, policy change or live command was performed.

### Diagnostic schema

Reports add `failedStage`, `failureCategory` and `serviceKeyChannelChecked`.
Success has null diagnostics. All flags indicating approval/live readiness remain
false. A missing usable key after the check has `serviceKeyChannelChecked=true`
and `serviceKeyChannelAvailable=false`; an earlier failure has both false. Presence
and shape do not prove authenticated validity. Observation-write failure changes the
result to `AZURE_BASELINE_UNVERIFIED`, sets baseline verification false and returns
`OBSERVATION_WRITE` / `OBSERVATION_PERSISTENCE_FAILED`; no write retry occurs.
A failed filesystem write may leave an incomplete observation; stdout failure is
not overridden by an earlier success value. Historical evidence is never rewritten.

Stages are fixed enums:

- `PRIVATE_ATTEMPT_TEMPLATE`, `OPERATOR_PREFLIGHT`.
- `FIRST_CAPTURE_` or `SECOND_CAPTURE_`, followed by `SITE`, `PLAN`, `WEB`,
  `APP_SETTINGS`, `CONNECTION_STRINGS`, `LOGS`, `SLOT_FLAGS` or `SLOTS` for the
  actual transport boundary. The same suffix plus `_RESPONSE_VALIDATION` means
  transport returned and the response is being decoded/validated locally.
- `FIRST_CAPTURE_VALIDATION`, `SECOND_CAPTURE_VALIDATION` for full baseline checks.
- `BASELINE_COMPARISON`, `POST_CAPTURE_TEMPLATE_RECHECK`, `SERVICE_KEY_CHANNEL_CHECK`,
  `OBSERVATION_WRITE`.
- Entry setup failures outside inspection use `PRIVATE_ATTEMPT_PREPARATION` or
  `OFFLINE_QUALIFICATION` as appropriate.

Closed categories: `LOCAL_PREREQUISITE_UNVERIFIED`, `AZURE_CLI_UNAVAILABLE`,
`AZURE_TRANSPORT_FAILURE`, `AZURE_RESOURCE_RESPONSE_INVALID`,
`AZURE_RESPONSE_SCHEMA_MISMATCH`, `AZURE_TARGET_OR_PLAN_MISMATCH`,
`AZURE_BASELINE_DRIFT`, `AZURE_READ_TIMEOUT`, `AZURE_BASELINE_INCOMPLETE`,
`OBSERVATION_PERSISTENCE_FAILED`, `UNCLASSIFIED_FAILURE`.

Classification uses exception types and exact existing internal Blocked codes.
It never parses raw Azure errors, subprocess stderr, response bodies or exception
strings. The unchanged WP21 bridge discards HTTP status; 401/403 cannot be reliably
distinguished. `AZURE_TRANSPORT_FAILURE` may include authentication, authorization,
HTTP or worker failures; do not infer which. A parent-observed timeout is reported
as `AZURE_READ_TIMEOUT`; a child watchdog exit without typed status remains a
transport failure. No new request or retry is added to diagnose these cases.

### Offline evidence

Command from repository root:

```powershell
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp22_preparation.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp21_controller.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp19_operator_handoff.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp18_research_launcher.py -q
```

Results: 33 WP24/WP22 tests, 38 WP21, 19 WP19 and 19 WP18 passed: 109 total.
The 14 new test methods include parameterized failures at all 16 request boundaries
and all 16 response-validation boundaries, both aggregate validation stages, schema
and target/region/F1 failures, drift, template and preflight stages, key checked vs
unchecked, conservative classification of fabricated 401/403 text, timeout, unknown
errors, observation-write failure and secret-canary stdout/file redaction. The
original 19 WP22 tests remain intact. Exact successful operation order/count and
capture equivalence with WP21 are checked. Azure/HTTP boundaries are mocked.

No PowerShell file changed; no actual operator -File run is claimed. UTF-8/LF and
`git diff --check` apply to all three changed files. Existing owner checkout and
private historical attempts remain untouched in an isolated worktree.

### Operator handoff - only after PR review and merge

Use a clean checkout of the reviewed merged code, in the existing authorized
Windows PowerShell 5.1/Azure session. Do not use the old WP23 checkout or the
untracked WP23 wrapper: it suppresses the new diagnostic fields. No wrapper or
PowerShell modification is necessary; call the existing WP22 entry directly.
Do not change execution policy or identity if a command is blocked.

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1
if ($LASTEXITCODE -ne 0) { throw 'Offline checks failed' }
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1
if ($LASTEXITCODE -ne 0) { throw 'Operator preflight failed' }
$prepared = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -PreparePrivateAttempt | ConvertFrom-Json
if ($LASTEXITCODE -ne 0 -or $prepared.attemptPrepared -ne $true -or $prepared.approvalEnabled -ne $false) { throw 'Disabled preparation failed' }
$env:AIQ_WP21_ATTEMPT_DIRECTORY = Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $prepared.attemptId
$inspection = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -InspectAzureReadOnly | ConvertFrom-Json
$inspectionExit = $LASTEXITCODE
$inspection | Select-Object result,attemptId,failedStage,failureCategory,azureBaselineVerified,serviceKeyChannelChecked,serviceKeyChannelAvailable,approvalEnabled,liveControllerInvoked,liveAttemptReady,RESEARCH_PROCESS_ALIVE,AUTHENTIC_DATA_NOT_IMPORTED,PUBLIC_RUNTIME_VERIFIED | ConvertTo-Json
if ($inspectionExit -ne 0) { throw 'Read-only inspection blocked; return sanitized JSON only' }
```

Prepare one NEW disabled UUID and perform one inspection within its 15-minute
window. Stop on failure; do not retry or inspect the historical UUID. Share only
that final sanitized JSON, never private paths, approval/observation contents,
worker journals, tokens, configuration values, environment dumps or raw errors.
No live approval is granted by this handoff or a successful read-only result.

`liveAttemptReady=false`; `liveControllerInvoked=false`;
`RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED=True`;
`PUBLIC_RUNTIME_VERIFIED=False`.


## WP26 - conservative worker transport diagnostics

Baseline: merged WP24 `0ac12a38b034ab4bf64fdcb0d8ca3d566773d1b9`.
The WP25 operator reached `FIRST_CAPTURE_SITE` / `AZURE_TRANSPORT_FAILURE` after
passing offline/preflight and disabled preparation. Preserve that historical attempt
and all earlier private records; do not inspect, reuse, arm, extend or reset them.
The service-key check was not reached, so presence is unverified.

### Fields and trust boundary

Reports add `transportFailureCategory` and `httpStatus`. A successful full capture
has null diagnostics. Existing failed-stage/category and key-checked semantics stay
unchanged. Non-transport local/schema failures have null transport metadata. An
untyped exception at a request boundary yields `UNKNOWN`. An observation write
failure clears transport metadata and overrides any previous verification success.

The closed transport enum is:

| Value | Proven evidence |
| --- | --- |
| CLI_EXECUTABLE_UNAVAILABLE | Fixed installed CLI interpreter file is unavailable |
| WORKER_LAUNCH_FAILURE | OS raises OSError while launching the fixed interpreter |
| PARENT_TIMEOUT | communicate raises TimeoutExpired at the unchanged 45-second limit |
| WORKER_NONZERO_EXIT | Worker process return code is nonzero or unverified |
| INVALID_WORKER_OUTPUT | Zero-exit output is oversized, not valid JSON or not an object |
| WORKER_IO_FAILURE | OSError during worker communication |
| UNKNOWN | No narrower typed cause, ownership/journal failure or uncertain cleanup |

`httpStatus` is ALWAYS null in this implementation. The unchanged WP21 bridge
returns baseline JSON on success and exits on failure without a status envelope.
It cannot provide reliable status metadata to the parent. Neither arbitrary body
fields named httpStatus, stderr, exit codes 401/403, nor exception text are HTTP
status evidence. Code 124 alone does not prove the watchdog fired. No authentication
versus RBAC claim is made. No extra ARM request or protocol change is added to
obtain status. A future protocol change would need separate implementation review.

### Compatibility and bounds

The only shared WP21 change is an overridable `Azure.transport(arguments)` method;
its default delegates to the original `cli(arguments)` unchanged. Existing request
signatures and response shapes remain intact. Live/recovery continue to use the
old CLI, old bridge and old exceptions. WP21 controller/recovery source files are
unchanged. The read-only subclass overrides only the transport dispatch, retaining
its exact eight-operation allowlist and capture validation. The runtime file hash
necessarily changes; newly prepared templates use the current identity. Historical
identities/approvals must never be updated to match it.

The diagnostic adapter reuses the fixed CLI interpreter, unchanged bridge and
40-second watchdog, ownership record before stdin/request dispatch, 45-second parent
wait, owned-child kill and five-second cleanup wait. It sends payloads through stdin,
never OS arguments or temporary secret files. It does not print worker streams,
raw exceptions or response values. Failure cleanup is not a request retry. No code
path adds requests beyond the original two captures; strict equality, freshness,
F1/target/region/Running/slots, disabled approval and persistence checks remain.

### Offline evidence and limits

Use the four unittest commands in the WP24 section above. Results: 43 combined
WP26/WP24/WP22 tests, 38 WP21, 19 WP19 and 19 WP18: 119 tests passed.
Ten new mocked tests cover supported worker categories, null status fallback,
ambiguous exit/status values, parent timeout/owned cleanup, malformed/bounded output,
ownership-before-dispatch, stdin-only payloads, sanitized report/file propagation,
exact operation order and default WP21 transport compatibility. Existing strict
comparison/redaction/freshness/write-failure regressions remain in place.

One initial status-spoofing test accidentally mocked its own diagnostic helper
recursively; the test was corrected to retain the real function reference. No
product validation or expected result was weakened. Azure, HTTP and CLI worker
boundaries were mocked; no real Azure request or private historical file was read.
No PowerShell file changed or operator -File execution is claimed. All four changed
text files pass UTF-8/LF/no-BOM and diff checks. Host policy must not be bypassed.

### One operator inspection - only after PR review and merge

From the clean reviewed merged checkout in the already authorized Windows
PowerShell 5.1/Azure CLI session, use the existing entries directly. Do not use old
WP23/WP25 wrappers that omit the new transport fields. Stop if a gate fails; do not
retry, change policy/identity or inspect the old UUID.

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1
if ($LASTEXITCODE -ne 0) { throw 'Offline qualification failed; stop.' }
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1
if ($LASTEXITCODE -ne 0) { throw 'Operator preflight failed; stop.' }
$prepared = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -PreparePrivateAttempt | ConvertFrom-Json
if ($LASTEXITCODE -ne 0 -or $prepared.attemptPrepared -ne $true -or $prepared.approvalEnabled -ne $false) { throw 'Disabled preparation failed; stop.' }
$env:AIQ_WP21_ATTEMPT_DIRECTORY = Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $prepared.attemptId
$inspection = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -InspectAzureReadOnly | ConvertFrom-Json
$inspectionExit = $LASTEXITCODE
$inspection | Select-Object result,attemptId,failedStage,failureCategory,transportFailureCategory,httpStatus,azureBaselineVerified,serviceKeyChannelChecked,serviceKeyChannelAvailable,approvalEnabled,liveControllerInvoked,liveAttemptReady,RESEARCH_PROCESS_ALIVE,AUTHENTIC_DATA_NOT_IMPORTED,PUBLIC_RUNTIME_VERIFIED | ConvertTo-Json
if ($inspectionExit -ne 0) { throw 'Inspection blocked; stop; share sanitized JSON only.' }
```

Only prepare one new disabled UUID when ready to perform the single inspection
within its 15-minute window. Return only the final selected JSON; never private
paths, streams, approval/observation files, environment dumps, tokens or key values.
No live approval, mutation, provider/import, health/browser probe, deployment or UI
change is authorized. A read-only result is not an immutable live rollback baseline.

`approvalEnabled=false`; `liveControllerInvoked=false`; `liveAttemptReady=false`;
`RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED=True`;
`PUBLIC_RUNTIME_VERIFIED=False`.


## Structured read-only worker-stage follow-up

Owner authorized this correction after WP27 returned `FIRST_CAPTURE_SITE`,
`AZURE_TRANSPORT_FAILURE`, `WORKER_NONZERO_EXIT`, null HTTP status and an unchecked
key channel. Preserve that historical disabled attempt and every earlier record;
no historical private attempt is accessed or reused by this implementation.

The read-only worker now uses a separate, versioned internal envelope with exactly
`protocol`, `ok`, `workerStage` and `payload`. Successful payloads remain ARM objects
in process memory. A handled failure returns no payload and one fixed stage:

| workerStage | Operation at failure |
| --- | --- |
| INPUT | Decode/extract the fixed worker request |
| INITIALIZATION | Import CLI modules or construct the CLI context |
| REQUEST | Execute send_raw_request; includes its internal authentication/transport behavior |
| RESPONSE_VALIDATION | Check returned status type/range and existing response-size bound |
| RESPONSE_DECODING | Decode response JSON and require an object |

The public report adds nullable `workerStage`. A valid failure envelope produces
`transportFailureCategory=WORKER_REPORTED_FAILURE`, the fixed stage and unchanged
`failureCategory=AZURE_TRANSPORT_FAILURE`. Success has null diagnostics. Unknown
stages, extra envelope fields, wrong protocol, non-boolean ok, secret-bearing failure
payloads and contradictory success/stage combinations are rejected as invalid
worker output. Nonzero/unstructured process exits retain their prior categories
and null workerStage. Observation-write failure still clears diagnostics and fails
closed. No raw exception, response body or stream enters sanitized failure evidence.

`httpStatus` stays null. REQUEST includes possible CLI authentication/RBAC/HTTP
errors without distinguishing them. RESPONSE_VALIDATION does not by itself prove an
HTTP rejection: malformed status metadata or response size may also fail there.
The report identifies the executing stage, not an independently proven root cause.

WP21's original BRIDGE, runtime, controller and recovery files are unchanged.
Only the read-only subclass selects this separate envelope-speaking worker; the
shared default CLI behavior remains intact. The same fixed interpreter, stdin
payload, ownership journal before dispatch, 40-second watchdog, 45-second parent
wait and five-second cleanup apply. No retry, additional ARM operation or weaker
baseline/target/plan/freshness/approval check was introduced. A fresh live rollback
baseline remains the responsibility of a separately authorized live controller.

Offline verification: 47 inspector tests, 38 WP21, 19 WP19 and 19 WP18 passed
(123 total). The four new methods exercise the exact worker string with fake CLI
modules and network connections blocked, every fixed worker stage, successful
payload preservation, strict parent-envelope rejection and sanitized report/file
propagation. Prior timeout, inventory, strict comparison, redaction and recovery
regressions remain. UTF-8/LF/no-BOM, Python syntax and diff checks passed. No actual
Azure request, operator -File run or private-attempt access is claimed.

### Operator handoff after review and merge only

Use the reviewed merged checkout and the existing direct PowerShell entries, not
older wrappers that omit workerStage or reject the new transport enum. In the
already authorized Windows PowerShell 5.1/Azure session, perform the offline and
preflight commands first. Prepare one NEW disabled UUID only when ready for one
read-only inspection within its 15-minute window. Do not retry on failure, change
policy/identity, reuse the WP27 attempt or arm the template.

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1
if ($LASTEXITCODE -ne 0) { throw 'Offline qualification failed; stop.' }
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1
if ($LASTEXITCODE -ne 0) { throw 'Operator preflight failed; stop.' }
$prepared = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -PreparePrivateAttempt | ConvertFrom-Json
if ($LASTEXITCODE -ne 0 -or $prepared.attemptPrepared -ne $true -or $prepared.approvalEnabled -ne $false) { throw 'Disabled preparation failed; stop.' }
$env:AIQ_WP21_ATTEMPT_DIRECTORY = Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $prepared.attemptId
$inspection = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -InspectAzureReadOnly | ConvertFrom-Json
$inspectionExit = $LASTEXITCODE
$inspection | Select-Object result,attemptId,failedStage,failureCategory,transportFailureCategory,workerStage,httpStatus,azureBaselineVerified,serviceKeyChannelChecked,serviceKeyChannelAvailable,approvalEnabled,liveControllerInvoked,liveAttemptReady,RESEARCH_PROCESS_ALIVE,AUTHENTIC_DATA_NOT_IMPORTED,PUBLIC_RUNTIME_VERIFIED | ConvertTo-Json
if ($inspectionExit -ne 0) { throw 'Inspection blocked; stop; share sanitized JSON only.' }
```

Return only that selected JSON. Never print private paths, raw worker output,
credential values, configuration or approval/observation files. No deployment or
live approval is granted. `approvalEnabled=false`; `liveControllerInvoked=false`;
`liveAttemptReady=false`; `RESEARCH_PROCESS_ALIVE=UNVERIFIED`;
`AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.

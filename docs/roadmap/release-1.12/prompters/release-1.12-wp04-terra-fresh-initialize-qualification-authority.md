# GPT-5.6 Terra — Release 1.12 WP04 Fresh Initialize Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: execute exactly one fresh Azure initialize qualification using the published helper with complete internal terminal telemetry.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current source commit:

```text
047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

Parent:

```text
d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Current deployed Azure image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Image source commit:

```text
d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Helper-only publication commit:

```text
047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

No image rebuild is required because the helper change is confined to `eng/**`.

Current repository state:

```text
tracked unstaged paths = 0
staged paths = 0
unrelated untracked files preserved
```

Current Azure state before this authority must be:

```text
App Service = Running/Normal
temporary qualification settings = none
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
deployed exact digest = sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

## 2. Qualification objective

Execute exactly one fresh **initialize** qualification attempt using:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

from source commit:

```text
047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

The purpose is to obtain one of two governed outcomes:

1. exact D3 initialize evidence attributable to the fresh RunId; or
2. exactly one sanitized helper terminal failure block identifying the internal terminal class.

A third outcome remains possible only if the residual external T6 boundary terminates the process/session before helper completion.

No retry is authorized outside the helper's existing 404/503 retry policy.

## 3. Binding runtime

Execute under:

```text
Windows PowerShell 5.1.26100.9444
```

Required helper provenance:

```text
committed helper source = local helper source
source commit = 047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

Do not run an uncommitted helper copy.

## 4. Binding retry and timeout policy

Preserve exactly:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
evidence-poll outer wall-clock budget <= 180 seconds
listener lifetime <= 180 seconds
```

No broadening.

No generic 5xx retry.

No transport retry.

No second initialize attempt.

## 5. Helper terminal telemetry contract

For every internally completed helper execution, require exactly one terminal block containing:

```text
WP04_HELPER_TERMINAL_RESULT
WP04_HELPER_TERMINAL_CLASS
WP04_HELPER_TERMINAL_PHASE
WP04_HELPER_TERMINAL_RUN_ID
WP04_HELPER_TERMINAL_EXIT_CODE
WP04_HELPER_TERMINAL_SETTINGS_RESTORATION
WP04_HELPER_TERMINAL_ELAPSED_MS
```

Expected internal terminal classes include fixed safe values such as:

```text
Success
HttpFailure
TransportFailure
Timeout
SemanticFailure
AzCommandFailure
SettingsApplicationFailure
RestartFailure
RestorationFailure
UnhandledHelperFailure
```

No raw exception messages.

No stack traces.

No evidence token.

No request headers.

No query string.

No raw response body.

## 6. Residual T6 boundary

The helper now covers all internally controlled T5 paths.

Residual risk remains:

```text
T6 = external process/session termination
```

If the outer execution environment terminates the child PowerShell process before helper completion, classify the run as:

```text
EXTERNAL_T6_TERMINATION_SUSPECTED
```

only if supported by wrapper/process evidence.

Do not classify it as Azure Timeout.

Do not retry.

## 7. Preflight — read-only

Before any Azure mutation, prove:

```text
local HEAD = 047e2ce8e3c614495f8c05cbe01d0b75617d56a7
remote governed branch tip = same commit
helper source matches committed source
PowerShell version = 5.1.26100.9444
App Service = Running/Normal
deployed digest = sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
temporary qualification settings = none
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
WEBSITES_PORT = 8501
plan = F1 / Free
region = West Central US
```

If any identity/security fact differs, STOP.

## 8. Fresh RunId

Generate exactly one fresh initialize RunId:

```text
initialize-<fresh-guid-or-equivalent>
```

Requirements:

```text
new
unique
recorded exactly
never reused after this authority
```

Forbidden prior failed RunIds:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
```

The malformed historical identifier remains non-reusable if encountered:

```text
initialize-50a6837f6cb849...
```

Do not reuse any prior failed RunId.

## 9. Temporary qualification settings

Apply only the established six temporary settings required by the committed helper/runtime contract.

Do not add new settings.

Do not alter persistent application configuration.

The evidence token must be high entropy and must never be printed.

Required state transition:

```text
pre-attempt temporary settings = none
temporary qualification settings applied = exactly established six
```

## 10. Restart authorization

Authorize exactly one explicit App Service restart for this fresh initialize qualification if the helper's established flow requires it.

Maximum:

```text
qualification explicit restarts = 1
```

No second restart for another attempt.

A restoration restart is authorized only if the helper's established cleanup path requires it to return the app to normal mode.

## 11. Initialize execution

Run the committed helper exactly once.

Required evidence capture includes:

```text
helper start time
helper end time or outer termination time
elapsed wall-clock
process exit code if available
stdout
stderr
terminal seven-field block if emitted
sanitized HTTP/failure telemetry
```

Do not expose secrets.

If executed through a child PowerShell process, also capture read-only:

```text
child process start success
child PID if available
child exit code if available
whether Start-Process -Wait returned normally
whether outer session/tool terminated first
```

This is evidence capture only; do not modify wrapper behavior under this authority.

## 12. D3 initialize acceptance

If the helper succeeds, accept only a target-attributable record for the exact fresh RunId.

Required fields:

```text
RecordVersion
Phase
RunId
DatabasePathIdentity
SchemaVersion
JournalMode
AcceptedEvidenceIdentity
AcceptedEvidenceCount
IntegrityCheck
QuickCheck
PersistenceContinuity
```

Require:

```text
Phase = initialize
RunId = exact fresh RunId
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
AcceptedEvidenceCount = 1
IntegrityCheck = ok
QuickCheck = ok
PersistenceContinuity = true
```

No target-attributable D3 record means no initialize acceptance credit.

Do not infer acceptance from:

```text
App Service Running
HTTP reachability alone
settings application
restart success
image identity
```

## 13. Terminal failure classification

If the helper emits a terminal failure block, capture exactly:

```text
RESULT
CLASS
PHASE
RUN_ID
EXIT_CODE
SETTINGS_RESTORATION
ELAPSED_MS
```

Then STOP.

No second initialize.

No reopen.

No redeploy continuity.

Return for Luna reconciliation unless the failure itself is an already-governed terminal class whose next step has explicit standing authority, which currently it does not.

## 14. External T6 handling

If no terminal block is emitted:

1. capture whether the child process exit code exists;
2. capture whether the outer execution/session terminated first;
3. capture elapsed time;
4. verify whether temporary settings were restored;
5. restore manually only if helper cleanup did not complete;
6. do not retry.

Return:

```text
HELPER_INTERNAL_TERMINAL_RESULT = NOT_OBSERVED
T6_EXTERNAL_TERMINATION = PROVEN | SUSPECTED | NOT_PROVEN
```

Do not relabel as `Timeout` absent a helper terminal class of `Timeout`.

## 15. Restoration contract

The helper owns restoration.

At completion require:

```text
temporary qualification settings = none
App Service returned to normal mode
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

If helper restoration fails or external termination bypasses cleanup, perform only the minimum manual restoration necessary to return to the exact known empty temporary-setting state.

Record whether restoration was:

```text
HELPER_OWNED
MANUAL_AFTER_T6
```

No broader Azure change is authorized.

## 16. Azure diagnostic logging preservation

Do not enable or modify:

```text
application logging
filesystem logging
HTTP logging
detailed errors
failed-request tracing
SCM basic auth
FTP basic auth
```

The source `WP04_DIAG_*` runtime instrumentation remains present in the image, but Azure capture/retrieval is not assumed.

This authority is about helper terminal telemetry and D3 evidence, not logging reconfiguration.

## 17. Image preservation

Do not change the deployed image.

Preserve exactly:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

No image redeployment is required.

No Docker build.

No GHCR publication.

## 18. Repository/Git preservation

Required zero:

```text
repository edits = 0
staged paths = 0
commits = 0
pushes = 0
```

Do not modify helper or wrapper.

## 19. GitHub lifecycle preservation

Required zero:

```text
PR creation = 0
PR merge = 0
issue mutation = 0
Project #2 mutation = 0
milestone mutation = 0
tag/release mutation = 0
WP05 start = 0
```

Issue `#263` remains open.

Milestone `#63` remains open.

## 20. Mutation accounting

Maximum authorized Azure mutations:

```text
temporary qualification settings application = 1
explicit qualification restart = 1
temporary qualification settings restoration = 1
restoration restart = 0 or 1 only if required by established cleanup
```

Required zero:

```text
image configuration updates = 0
logging mutations = 0
SCM policy mutations = 0
FTP policy mutations = 0
registry credential mutations = 0
plan/SKU mutations = 0
region mutations = 0
health-check mutations = 0
Always On mutations = 0
source/Git mutations = 0
Docker/GHCR mutations = 0
GitHub lifecycle mutations = 0
```

Count actual operations precisely.

## 21. Stop conditions

STOP if:

- helper provenance does not match commit `047e2ce8...`;
- deployed image digest differs;
- temporary settings are non-empty before attempt;
- PowerShell is not 5.1.26100.9444;
- SCM/FTP basic auth differs from false;
- registry credentials appear;
- a second initialize would be required;
- retry policy would need widening;
- helper terminal result indicates failure;
- helper terminal result is absent after process/session termination;
- D3 payload is malformed or wrong RunId;
- cleanup cannot return Azure to known normal state.

Do not widen scope.

## 22. Required return evidence

Return:

- preflight identity/security facts;
- exact fresh RunId;
- helper provenance result;
- PowerShell version;
- temporary settings application result;
- restart count;
- helper process/wrapper execution facts;
- elapsed wall-clock;
- helper terminal seven-field block, if emitted;
- sanitized client telemetry;
- D3 record, if emitted;
- helper/outer process exit evidence;
- T6 classification if applicable;
- restoration owner and result;
- final temporary-setting count;
- final App Service state;
- final deployed image digest;
- SCM/FTP state;
- registry credential state;
- repository tracked/staged state;
- exact mutation audit;
- next-authority classification.

Never return the evidence token.

## 23. Terminal markers

Always require:

`RELEASE 1.12 WP04 — FRESH INITIALIZE QUALIFICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — HELPER SOURCE COMMIT: 047e2ce8e3c614495f8c05cbe01d0b75617d56a7`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 QUALIFICATION RUNTIME: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — CURRENT INSTRUMENTED AZURE IMAGE: PRESERVED`

`RELEASE 1.12 WP04 — NORMAL RUNTIME RESTORATION: PASS`

`RELEASE 1.12 WP04 — FRESH INITIALIZE QUALIFICATION MUTATION AUDIT: PASS`

### If D3 initialize succeeds

`RELEASE 1.12 WP04 — FRESH INITIALIZE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — HELPER TERMINAL RESULT: SUCCESS`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: PROVEN`

`RELEASE 1.12 WP04 — TERRA REOPEN/CONTINUITY QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA FRESH INITIALIZE QUALIFICATION COMPLETE`

### If helper emits terminal failure

`RELEASE 1.12 WP04 — FRESH INITIALIZE QUALIFICATION: FAIL`

`RELEASE 1.12 WP04 — HELPER TERMINAL RESULT: FAILURE`

`RELEASE 1.12 WP04 — HELPER TERMINAL CLASS: <SAFE_FIXED_CLASS>`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — LUNA TERMINAL FAILURE RECONCILIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA FRESH INITIALIZE QUALIFICATION COMPLETE`

### If no terminal helper block because of external boundary

`RELEASE 1.12 WP04 — FRESH INITIALIZE QUALIFICATION: FAIL`

`RELEASE 1.12 WP04 — HELPER TERMINAL RESULT: NOT_OBSERVED`

`RELEASE 1.12 WP04 — EXTERNAL T6 TERMINATION: <PROVEN|SUSPECTED|NOT_PROVEN>`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — LUNA EXTERNAL T6 RECONCILIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA FRESH INITIALIZE QUALIFICATION COMPLETE`

### If preflight blocks

`RELEASE 1.12 WP04 — FRESH INITIALIZE QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

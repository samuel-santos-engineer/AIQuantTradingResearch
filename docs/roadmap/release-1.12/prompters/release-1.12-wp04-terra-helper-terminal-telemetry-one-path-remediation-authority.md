# GPT-5.6 Terra — Release 1.12 WP04 Helper Terminal Telemetry One-Path Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the approved one-path helper terminal-telemetry correction.
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
d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Current deployed Azure image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current App Service state:

```text
Running/Normal
temporary qualification settings = none
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

Selected Luna decision:

```text
D1 — H1_HELPER_TERMINAL_TELEMETRY_CORRECTION
```

Binding PowerShell runtime:

```text
Windows PowerShell 5.1.26100.9444
```

## 2. Exact authorized mutation allowlist

Exactly one tracked path may be modified:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Required counts:

```text
tracked mutation paths = 1
operations = 1 MODIFY
```

No other tracked path is authorized.

Preserve all unrelated untracked files untouched.

## 3. Remediation objective

Correct all **internally controlled T5 helper termination paths** so that every helper-owned completion/failure path emits a deterministic sanitized terminal result before process exit.

The remediation must cover, at minimum:

```text
terminating PowerShell exceptions
explicit throw paths
native az failures observed through $LASTEXITCODE
Invoke-WebRequest failures
semantic-response failures
timeout classification
restoration failures
unexpected helper-owned failures
```

The helper must not silently terminate through any internally controlled path.

This remediation does **not** claim to eliminate externally imposed T6 termination such as host/session/process kill.

## 4. Required terminal telemetry contract

Define one and only one terminal helper result per internally completed invocation.

Use a fixed machine-readable terminal vocabulary.

Required terminal fields:

```text
WP04_HELPER_TERMINAL_RESULT=<SUCCESS|FAILURE>
WP04_HELPER_TERMINAL_CLASS=<SAFE_FIXED_CLASS>
WP04_HELPER_TERMINAL_PHASE=<initialize|reopen|unknown>
WP04_HELPER_TERMINAL_RUN_ID=<sanitized-run-id-or-NONE>
WP04_HELPER_TERMINAL_EXIT_CODE=<0|1>
WP04_HELPER_TERMINAL_SETTINGS_RESTORATION=<PASS|FAIL|NOT_REQUIRED|NOT_ATTEMPTED>
```

Optional safe field:

```text
WP04_HELPER_TERMINAL_ELAPSED_MS=<integer>
```

Do not emit more than one terminal result block.

Do not emit a terminal success result before cleanup/restoration obligations are resolved.

## 5. Approved sanitized terminal classes

Use only fixed safe classes.

At minimum support:

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
ExternalTerminationNotObservable
```

`ExternalTerminationNotObservable` is descriptive metadata only and must not be emitted as if the helper observed its own external kill.

Do not print raw exception messages.

Do not print stack traces.

## 6. Top-level control-flow requirement

Refactor only as much as necessary to guarantee deterministic internal completion.

Preferred structure:

```text
initialize terminal-state variables

try {
    perform governed helper workflow
    set terminal state, but do not exit prematurely
}
catch {
    classify into fixed safe terminal class
    set terminal failure state
}
finally {
    perform required restoration if helper owns restoration
    fold restoration outcome into final terminal state
    emit exactly one terminal result block
}

exit <final exit code>
```

Binding requirements:

- no `throw` after the terminal state has been established unless it is caught by the same top-level control flow;
- no internal `exit` before terminal telemetry emission;
- no early `return` that bypasses terminal telemetry;
- native `az` nonzero exit must become sanitized helper terminal failure;
- restoration must execute from the helper-owned finalization path where applicable;
- restoration failure must not suppress terminal telemetry;
- terminal telemetry emission itself must be simple and non-throwing where practical;
- final process exit code remains `0` on governed success and `1` on governed failure.

## 7. Restoration contract

The helper currently owns restoration.

Preserve that ownership.

Required behavior:

### Success

```text
qualification result successful
temporary settings restored
terminal result = SUCCESS
terminal class = Success
exit code = 0
```

### Qualification failure with successful restoration

```text
qualification result failed
temporary settings restored
terminal result = FAILURE
terminal class = exact sanitized failure class
settings restoration = PASS
exit code = 1
```

### Restoration failure

```text
terminal result = FAILURE
terminal class = RestorationFailure
settings restoration = FAIL
exit code = 1
```

If an earlier failure occurred before restoration also fails, do not print raw nested errors. Preserve only a deterministic safe classification.

If policy requires retaining the primary failure classification, define a second safe field only if already compatible with the one-path contract; otherwise `RestorationFailure` is authoritative because cleanup is incomplete.

## 8. Existing client failure policy preservation

Preserve exactly:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
poll outer wall-clock budget <= 180 seconds
```

Do not broaden retry behavior.

Do not change request timeout semantics except where needed to preserve the existing hard deadline implementation.

Do not change 404/503 handling.

## 9. Windows PowerShell 5.1 compatibility

All syntax/APIs must work under:

```text
Windows PowerShell 5.1.26100.9444
```

Do not introduce:

```text
PowerShell 7-only syntax
ternary operators
null-coalescing operators
PS7-only cmdlets
unsupported newer .NET APIs
```

Run a PowerShell AST parse check and require:

```text
parse errors = 0
```

## 10. Secret-hygiene boundary

Terminal telemetry and all intermediate diagnostics must never emit:

```text
evidence token
X-WP04-Evidence-Token value
request headers
query string
full authenticated URL
response body
raw evidence JSON
SQLite contents
connection strings
registry credentials
Azure credentials
environment dumps
raw exception messages
stack traces
```

Safe to emit:

```text
attempt number
HTTP status
fixed failure class
phase
sanitized RunId
elapsed milliseconds
restoration status
exit code
```

## 11. Local validation matrix

Validate locally without Azure mutation.

At minimum prove:

### V1 — Successful governed path

Expected:

```text
exactly one terminal result
RESULT=SUCCESS
CLASS=Success
RESTORATION=PASS or NOT_REQUIRED according to fixture
EXIT_CODE=0
process exit = 0
```

### V2 — Explicit terminating throw path

Inject/simulate an internal terminating exception.

Expected:

```text
exactly one terminal result
RESULT=FAILURE
CLASS=UnhandledHelperFailure or narrower approved class
EXIT_CODE=1
raw exception text absent
process exit = 1
```

### V3 — Native az failure

Simulate nonzero native exit.

Expected:

```text
exactly one terminal result
RESULT=FAILURE
CLASS=AzCommandFailure or stage-specific approved class
EXIT_CODE=1
```

### V4 — HTTP Timeout

Expected:

```text
exactly one terminal result
CLASS=Timeout
no retry
EXIT_CODE=1
```

### V5 — HTTP 404 retry

Expected:

```text
retry occurs within existing policy
terminal telemetry emitted exactly once at eventual completion
```

### V6 — HTTP 503 retry

Expected:

```text
retry occurs within existing policy
terminal telemetry emitted exactly once at eventual completion
```

### V7 — Non-retryable HTTP failure

Examples:

```text
400
401
403
409
500
502
504
```

Expected:

```text
no retry
exactly one terminal failure result
fixed safe class
EXIT_CODE=1
```

### V8 — Semantic 200 failure

Expected:

```text
exactly one terminal failure result
CLASS=SemanticFailure
EXIT_CODE=1
```

### V9 — Restoration failure

Inject/simulate restoration failure.

Expected:

```text
exactly one terminal result
RESULT=FAILURE
CLASS=RestorationFailure
RESTORATION=FAIL
EXIT_CODE=1
terminal telemetry still emitted
```

### V10 — Hard deadline

Use accelerated local timing fixture.

Expected:

```text
outer bound respected
request cap bounded by remaining time
sleep bounded by remaining time
terminal telemetry emitted exactly once
```

### V11 — Secret hygiene

Assert zero token/header/query/raw exception leakage across all failure fixtures.

## 12. T6 external termination boundary

This remediation cannot guarantee terminal output if the PowerShell process is externally killed.

Therefore preserve the reconciliation conclusion:

```text
T6 external termination remains possible
```

But after this remediation, all helper-owned paths must be covered.

Required post-remediation classification:

```text
T5 internally controlled uncovered paths = 0
T6 externally imposed termination = residual external boundary
```

Do not attempt to solve T6 in this authority.

## 13. No Docker/image requirement

This path is under:

```text
eng/**
```

and is excluded from the runtime Docker context under the established repository contract.

Therefore:

```text
new source commit required later = YES
new runtime image required = NO
current deployed instrumented image remains valid
```

Do not build Docker.

Do not publish GHCR.

## 14. Azure preservation

No Azure action is authorized.

Preserve:

```text
deployed digest =
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f

App Service = Running/Normal
temporary qualification settings = none
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
logging configuration unchanged
```

No initialize attempt is authorized.

## 15. Git/worktree gate

Before completion require:

```text
tracked changed paths = exactly 1
tracked changed path = exact allowlist
staged paths = 0
unauthorized tracked paths = 0
```

Preserve unrelated untracked files.

Do not stage anything.

## 16. Static/security validation

Require:

```text
Windows PowerShell AST parse errors = 0
git diff --check = PASS
Gitleaks = PASS
terminal telemetry appears exactly once per local fixture
raw exception leakage = 0
token/header/query leakage = 0
retry policy change = 0
timeout policy change = 0
Azure mutation = 0
```

## 17. Explicitly forbidden

Do not:

```text
modify any second tracked path
stage
commit
push
build Docker image
publish GHCR
mutate Azure
restart/redeploy App Service
enable Azure logging
change SCM/FTP policy
add registry credentials
rerun initialize
create PR
mutate issue
mutate Project #2
mutate milestone
create tag/release
begin WP05
```

## 18. Stop conditions

STOP if:

- a second tracked path is required;
- wrapper/caller changes are required;
- terminal telemetry cannot be guaranteed for helper-owned paths;
- restoration cannot be covered without changing helper semantics materially;
- retry policy must change;
- timeout policy must change;
- secret-bearing output appears necessary;
- PowerShell 5.1 compatibility cannot be maintained;
- Azure mutation appears necessary.

Return for Luna re-governance instead of widening scope.

## 19. Mutation accounting

Authorized local repository mutation:

```text
1 tracked path modified
```

Required zero:

```text
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
PR mutations = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
```

## 20. Failed RunIds

All failed RunIds remain forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
```

## 21. Required return evidence

Return:

- exact modified path;
- exact top-level control-flow change;
- exact terminal field vocabulary;
- exact terminal class vocabulary;
- internal T5 path audit before/after;
- residual T6 statement;
- V1–V11 results;
- AST parse result;
- Windows PowerShell 5.1 validation result;
- retry-policy preservation result;
- hard-deadline preservation result;
- secret-hygiene result;
- `git diff --check`;
- Gitleaks result;
- tracked/staged path counts;
- Azure preservation confirmation;
- exact mutation audit.

## 22. Terminal markers

On success:

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY REMEDIATION: PASS`

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY COVERAGE: COMPLETE_FOR_INTERNAL_PATHS`

`RELEASE 1.12 WP04 — HELPER INTERNAL T5 UNCOVERED PATH COUNT: 0`

`RELEASE 1.12 WP04 — HELPER EXTERNAL T6 BOUNDARY: PRESERVED_AS_EXTERNAL_RISK`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 TERMINAL TELEMETRY VALIDATION: PASS`

`RELEASE 1.12 WP04 — TERMINAL RESULT EXACTLY-ONCE VALIDATION: PASS`

`RELEASE 1.12 WP04 — HELPER RESTORATION TELEMETRY: PASS`

`RELEASE 1.12 WP04 — HELPER TERMINAL SECRET-HYGIENE: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: YES`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — CURRENT INSTRUMENTED AZURE IMAGE: PRESERVED`

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA HELPER TERMINAL TELEMETRY SOURCE PUBLICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA HELPER TERMINAL TELEMETRY REMEDIATION COMPLETE`

On block:

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

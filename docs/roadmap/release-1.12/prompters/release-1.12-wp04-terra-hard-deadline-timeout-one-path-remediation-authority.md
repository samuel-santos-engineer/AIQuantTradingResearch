# GPT-5.6 Terra — Release 1.12 WP04 Hard Deadline Timeout One-Path Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the approved one-path hard-deadline timeout correction.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current governed source commit:

```text
31c7fed07d556fbba693c348a338d7d35d5202a4
```

Current source finality:

```text
SUPERSEDED_AFTER_REMEDIATION
```

Runtime image remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Luna decision:

```text
D2 — T2_EXPLICIT_REQUEST_TIMEOUT_ONLY
```

Current helper timing defect:

```text
Invoke-WebRequest -TimeoutSec 20
36 fixed attempts
5-second sleeps between attempts
maximum potential duration ≈ 36*20 + 35*5 = 895 seconds
```

Binding governed timing contract:

```text
total evidence-poll wall-clock budget <= 180 seconds
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
```

Forbidden failed RunIds:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
```

Azure state must remain unchanged under this authority.

## 2. Exact implementation allowlist

Exactly one tracked path may be modified:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Required path count:

```text
1
```

No other tracked path is authorized.

Preserve unrelated untracked files untouched.

## 3. Required implementation objective

Replace the fixed-attempt timing model with hard wall-clock deadline accounting.

The helper must enforce:

```text
OUTER_TOTAL_BUDGET = 180 seconds maximum
```

The outer budget begins immediately before the first HTTP evidence request and ends no later than 180 seconds later.

The helper must not permit:

```text
request timeout * attempt count
+ sleeps
```

to exceed the governed outer budget.

## 4. Per-request timeout contract

Retain an explicit per-request timeout compatible with Windows PowerShell 5.1.

The effective per-request timeout for each attempt must be bounded by the **remaining outer deadline**.

The helper may choose a fixed nominal request timeout smaller than 20 seconds, but the actual timeout for any request must satisfy:

```text
effective_request_timeout <= remaining_outer_budget
```

and must never extend the total evidence-poll operation beyond the 180-second hard deadline.

The implementation must not rely on a fixed 36-attempt loop as the governing bound.

Preferred design:

```text
deadline = start_time + 180 seconds

before each request:
  remaining = deadline - current_time
  if remaining <= 0:
      terminal bounded-timeout failure

effective request timeout =
  min(nominal request timeout, remaining rounded safely for TimeoutSec)

after retryable 404/503:
  recompute remaining
  sleep no longer than min(5 seconds, remaining)
```

Equivalent implementation is allowed if it proves the same hard wall-clock property.

## 5. Retry policy preservation

Do not broaden retry behavior.

Remain exactly:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
```

Therefore:

```text
Timeout -> terminal
NameResolutionFailure -> terminal
ConnectFailure -> terminal
ConnectionClosed -> terminal
UnknownError -> terminal
all other transport classes -> terminal
```

Do not retry `Timeout`.

Do not add generic transport retries.

Do not add generic 5xx retries.

## 6. Failure diagnostics preservation

Preserve the published sanitized diagnostics:

```text
WP04_HTTP_EVIDENCE_POLL_ATTEMPT
WP04_HTTP_EVIDENCE_STATUS
WP04_HTTP_EVIDENCE_FAILURE_CLASS
```

Preserve secret-hygiene prohibitions:

```text
no token output
no authorization-header output
no request URL output
no response-body output
no raw exception-message output
no stack-trace output
no credentials output
```

## 7. Windows PowerShell 5.1 requirement

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Do not use PowerShell 7-only syntax, cmdlets, or runtime assumptions.

Timing APIs must be available under Windows PowerShell 5.1 / .NET Framework.

Prefer monotonic elapsed-time measurement where practical, such as:

```text
[System.Diagnostics.Stopwatch]
```

Do not depend solely on mutable wall-clock time if a monotonic timer is available.

The approved RNG implementation must remain intact:

```text
RandomNumberGenerator.Create()
GetBytes()
Dispose()
```

## 8. Required local validation

Run locally only.

### Parse gate

Require:

```text
Windows PowerShell 5.1 AST errors = 0
```

### Timing-model validation

Prove with synthetic/local validation that:

```text
outer qualification evidence-poll duration cannot exceed 180 seconds
fixed 36-attempt bound is no longer the governing timing mechanism
request timeout is capped by remaining deadline
retry sleep is capped by remaining deadline
```

Use accelerated/synthetic timing if needed rather than waiting a real 180 seconds, provided the same deadline arithmetic/code path is exercised.

At minimum prove:

```text
large configured/nominal request timeout cannot overrun outer deadline
final request receives no more than remaining budget
retry sleep cannot push elapsed time beyond deadline
deadline expiry terminates boundedly
```

### HTTP retry validation

Require:

```text
404 -> retry
503 -> retry
400 -> terminal
401 -> terminal
403 -> terminal
409 -> terminal
500 -> terminal
502 -> terminal
other 5xx -> terminal
```

### Transport validation

Require:

```text
Timeout -> terminal
NameResolutionFailure -> terminal
ConnectFailure -> terminal
ConnectionClosed -> terminal
UnknownError -> terminal
```

### Semantic validation

Require:

```text
malformed 200 -> terminal
wrong RunId -> terminal
invalid evidence record -> terminal
```

### Restoration validation

Prove final restoration logic still executes after:

```text
deadline exhaustion
Timeout
terminal HTTP failure
semantic failure
```

No Azure mutation may be used.

### Security/static gates

Require:

```text
retryable HTTP statuses exactly {404,503}
retryable transport classes exactly {NONE}
generic 5xx retry logic absent
transport retry logic absent
token disclosure = 0
authorization-header disclosure = 0
request-URL disclosure = 0
response-body disclosure = 0
raw exception-message disclosure = 0
stack-trace disclosure = 0
direct SQLite evidence access = 0
active Kudu-VFS evidence retrieval = 0
Gitleaks = pass
git diff --check = pass
```

### RNG regression gate

Require:

```text
RandomNumberGenerator.Fill active references = 0
RandomNumberGenerator.Create present
GetBytes present
```

## 9. Exact diff gate

Before completion require:

```text
tracked changed paths = exactly 1
changed path =
  eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
staged paths = 0
unauthorized tracked paths = 0
```

Do not stage anything.

## 10. Explicitly forbidden mutations

This authority does **not** authorize:

```text
git add
git commit
git push
Docker build
Docker image publication
GHCR mutation
Azure mutation
App Service settings mutation
initialize qualification
restart qualification
redeploy qualification
PR creation
PR merge
issue mutation
Project #2 mutation
milestone mutation
tag/release mutation
```

## 11. Source/image implications

After successful local remediation:

```text
new source commit required later = YES
new image required = NO
```

Reason:

```text
only eng/ helper changes
eng/ remains excluded from Docker image
runtime image remains unchanged
```

Runtime digest remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

## 12. Azure preservation

Require zero Azure mutation.

Preserve:

```text
SCM basic auth = false
FTP basic auth = false
runtime image digest unchanged
registry credentials absent
temporary D3 settings absent
temporary evidence token absent
normal runtime restored
```

Do not rerun initialize.

## 13. Stop conditions

STOP if:

- more than one tracked path needs modification;
- Timeout must become retryable;
- any other transport class must become retryable;
- HTTP retryable set would exceed `{404,503}`;
- hard 180-second bound cannot be proven;
- Windows PowerShell 5.1 compatibility fails;
- restoration semantics regress;
- runtime/container/Azure change appears necessary;
- Docker/image rebuild appears necessary;
- direct SQLite/Kudu evidence would be required.

Do not widen scope.

## 14. Mutation audit

Expected mutation:

```text
Repository working tree:
  exactly 1 tracked path modified
```

Required zero:

```text
staging: 0
commits: 0
pushes: 0
Docker: 0
GHCR: 0
Azure: 0
PR: 0
issue: 0
Project #2: 0
milestone: 0
tags/releases: 0
```

## 15. Required return evidence

Return:

- exact modified path;
- concise timing implementation summary;
- exact nominal per-request timeout;
- exact deadline mechanism;
- Windows PowerShell 5.1 AST result;
- timing-model validation results;
- HTTP retry/terminal matrix;
- transport terminal matrix;
- semantic-failure validation;
- restoration-path proof;
- secret-hygiene proof;
- RNG regression proof;
- direct SQLite scan;
- active Kudu-VFS scan;
- Gitleaks result;
- `git diff --check`;
- tracked/staged path counts;
- exact mutation accounting.

## 16. Required terminal markers

Full success requires:

`RELEASE 1.12 WP04 — HARD DEADLINE TIMEOUT ONE-PATH REMEDIATION: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — REQUEST TIMEOUT DEADLINE CAP: PASS`

`RELEASE 1.12 WP04 — RETRY SLEEP DEADLINE CAP: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 HARD-DEADLINE COMPATIBILITY: PASS`

`RELEASE 1.12 WP04 — SANITIZED FAILURE DIAGNOSTICS PRESERVATION: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE RESTORATION PATH: PASS`

`RELEASE 1.12 WP04 — RNG COMPATIBILITY REGRESSION: PASS`

`RELEASE 1.12 WP04 — HARD DEADLINE PATH GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — HARD DEADLINE REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA TIMEOUT REMEDIATION COMPLETE`

`RELEASE 1.12 WP04 — TERRA TIMEOUT REMEDIATION PUBLICATION AUTHORITY: READY`

Blocked:

`RELEASE 1.12 WP04 — HARD DEADLINE TIMEOUT ONE-PATH REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

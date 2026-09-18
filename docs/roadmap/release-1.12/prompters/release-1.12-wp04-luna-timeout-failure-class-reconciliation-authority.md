# GPT-5.6 Luna — Release 1.12 WP04 Timeout Failure-Class Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: classify the observed `Timeout` transport failure and define the narrowest safe next remediation.
- **GPT-5.6 Terra** — implementation, local validation, publication, and any resumed Azure qualification only under later explicit authority.
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

Runtime image remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Binding operator shell:

```text
Windows PowerShell 5.1.26100.9444
```

Current retry contract:

```text
retryable HTTP statuses = {404,503}
transport exceptions = terminal pending classification
poll interval = 5 seconds
maximum qualification window = 180 seconds
```

## 2. Newly classified governed failure

Fresh failed RunId:

```text
initialize-50a6837f6cb649ed98481a3c26831961
```

Observed terminal evidence:

```text
poll attempt = 1
HTTP status = NONE
failure class = Timeout
helper termination = exception
evidence record returned = no
temporary settings restored = yes
final temporary-setting count = 0
App Service final state = Running/Normal
runtime digest changed = no
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

Mutation audit:

```text
temporary settings applications = 1
restart actions = 1
temporary settings restorations = 1
redeploy actions = 0
Git mutations = 0
Docker/GHCR mutations = 0
PR/issue/Project/milestone mutations = 0
```

Disposition:

```text
NOT ACCEPTED
FORBIDDEN TO REUSE
```

This failed run earns no qualification acceptance credit.

## 3. Reconciliation question

Determine whether `Timeout` should become a narrowly retryable transport condition during the existing bounded 180-second evidence-poll window, or whether the timeout instead indicates a request-level timeout/configuration defect that must be corrected differently.

Do not assume that every timeout is safe to retry.

The observed facts support only:

```text
the first HTTP evidence attempt did not produce an HTTP response before the client-side operation timed out
```

They do not yet prove:

```text
application failure
Azure failure
endpoint absence
authentication failure
network policy failure
or a successful-but-late response
```

## 4. Required read-only inspection

Inspect the committed helper at:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

at source commit:

```text
31c7fed07d556fbba693c348a338d7d35d5202a4
```

Determine exactly:

- which command/API performs the HTTP request;
- whether an explicit per-request timeout is configured;
- the effective per-request timeout under Windows PowerShell 5.1;
- whether one request can consume most/all of the 180-second total budget;
- whether `Timeout` is emitted by `System.Net.WebExceptionStatus.Timeout`;
- whether retry timing is measured from wall-clock deadline or fixed attempt count;
- whether final restoration runs after a timed-out request;
- whether retrying `Timeout` can preserve a hard total wall-clock bound.

No mutations are authorized.

## 5. Candidate decisions

Evaluate exactly these options.

### T1 — Retry `Timeout` within the existing bounded poll loop

Classify only sanitized transport class:

```text
Timeout
```

as retryable, alongside HTTP:

```text
404
503
```

All other transport classes remain terminal.

This is acceptable only if:

- request timeout is itself reasonably short relative to the 180-second total budget;
- retry behavior is bounded by a hard wall-clock deadline;
- a timeout cannot silently extend the qualification beyond the governed maximum;
- secret hygiene and restoration remain preserved.

### T2 — Add an explicit short per-request timeout, but keep `Timeout` terminal

Use when the current request can block too long and the timeout itself is not yet safe to treat as retryable.

This changes request mechanics but not retry policy.

### T3 — Add an explicit short per-request timeout and retry only `Timeout`

Use when both are required:

- the current request-level timeout is too long or uncontrolled;
- `Timeout` is reasonably classified as transient startup/readiness behavior;
- total elapsed time remains hard-bounded at 180 seconds.

### T4 — No helper change; investigate Azure/runtime

Select only if committed helper behavior is already correctly bounded and retrying/shortening request timeout would be unjustified.

### T5 — Broaden transport retries generically

Reject unless evidence proves a broader set is safe.

Do not select generic transport retrying merely because `Timeout` was observed.

## 6. Required decision

Select exactly one:

```text
D1 — T1_RETRY_TIMEOUT_ONLY
D2 — T2_EXPLICIT_REQUEST_TIMEOUT_ONLY
D3 — T3_EXPLICIT_REQUEST_TIMEOUT_AND_RETRY_TIMEOUT
D4 — T4_AZURE_RUNTIME_INVESTIGATION
D5 — T5_GENERIC_TRANSPORT_RETRY
```

Preferred decision should be the narrowest one supported by the committed helper's actual timing semantics.

## 7. Retry-policy constraints

Whatever decision is selected:

```text
HTTP retryable statuses must remain exactly {404,503}
```

unless Luna separately and explicitly changes them.

If `Timeout` becomes retryable:

```text
retryable transport classes = {Timeout}
```

All other transport classes must remain terminal, including:

```text
NameResolutionFailure
ConnectFailure
ConnectionClosed
KeepAliveFailure
PipelineFailure
ProxyNameResolutionFailure
ReceiveFailure
RequestCanceled
SecureChannelFailure
SendFailure
TrustFailure
UnknownError
ProtocolError
```

No generic transport retry policy is authorized by default.

## 8. Timing contract

If any retry of `Timeout` is selected, define a hard timing contract:

```text
total qualification evidence-poll wall-clock budget <= 180 seconds
poll cadence target = 5 seconds
per-request timeout = explicit and bounded
deadline accounting = wall-clock based
```

The implementation must not allow:

```text
180-second request timeout + another 180-second loop
```

or any equivalent budget multiplication.

The total evidence retrieval budget is a hard outer bound.

## 9. Windows PowerShell 5.1 compatibility

Binding environment:

```text
Windows PowerShell 5.1.26100.9444
```

Any selected approach must use syntax/APIs available under Windows PowerShell 5.1 / .NET Framework.

Do not rely on PowerShell 7-only `Invoke-WebRequest`/`Invoke-RestMethod` behavior.

## 10. Exact repository scope if helper remediation is required

If `D1`, `D2`, or `D3` is selected, exact allowlist:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Path count:

```text
1
```

Operation count:

```text
1 MODIFY
```

No other tracked path is authorized.

## 11. Validation contract for later Terra authority

If helper remediation is selected, later Terra implementation must prove locally/synthetically:

```text
Windows PowerShell 5.1 AST errors = 0
```

Always:

```text
404 -> retry
503 -> retry
400/401/403/409/500/502 -> terminal
malformed 200 -> terminal
wrong RunId -> terminal
```

If `D1` or `D3`:

```text
Timeout -> retry
NameResolutionFailure -> terminal
ConnectFailure -> terminal
ConnectionClosed -> terminal
UnknownError -> terminal
```

If `D2`:

```text
Timeout -> terminal
```

Timing tests must prove:

```text
hard total wall-clock bound <= 180 seconds
request timeout cannot multiply outer qualification budget
restoration still executes on final failure
```

Security/static gates:

```text
token disclosure = 0
header disclosure = 0
URL disclosure = 0
response-body disclosure = 0
raw exception-message disclosure = 0
direct SQLite evidence access = 0
active Kudu-VFS retrieval = 0
Gitleaks = pass
git diff --check = pass
```

RNG compatibility must remain unchanged.

## 12. Source/image implications

If repository remediation is required:

```text
NEW SOURCE COMMIT REQUIRED = YES
NEW IMAGE REQUIRED = NO
CURRENT SOURCE FINALITY = SUPERSEDED_AFTER_REMEDIATION
```

Reason:

```text
only eng/ helper changes
eng/ remains excluded from Docker image
```

Runtime digest remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

If `D4` is selected:

```text
NEW SOURCE COMMIT REQUIRED = NO
CURRENT SOURCE FINALITY = PRESERVED
```

unless later evidence changes that decision.

## 13. Azure state preservation

This Luna authority is read-only.

No Azure mutation.

Preserve:

```text
SCM basic auth = false
FTP basic auth = false
runtime digest unchanged
registry credentials absent
temporary qualification settings absent
temporary evidence token absent
normal runtime restored
```

Do not rerun initialize.

Do not run restart/redeploy continuity.

## 14. Failed RunId disposition

All of these failed RunIds remain forbidden for reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
```

The next governed initialize attempt must use a fresh RunId.

## 15. Mutation prohibition

Required zero mutations under this authority:

```text
Repository = 0
Git = 0
Docker/GHCR = 0
Azure = 0
Provider = 0
PR = 0
Issue = 0
Project #2 = 0
Milestone = 0
```

## 16. Required output

Return:

- exact committed HTTP request mechanism;
- effective current per-request timeout;
- exact outer-loop timing semantics;
- whether one timeout can consume the full 180-second window;
- T1–T5 comparison;
- exact D1–D5 decision;
- retryable HTTP set;
- retryable transport set;
- explicit timing contract;
- one-path allowlist/count if applicable;
- Windows PowerShell 5.1 compatibility statement;
- source/image finality decision;
- failed RunId disposition;
- Azure preservation statement;
- next Terra scope if executable;
- zero-mutation confirmation.

## 17. Terminal markers

Required:

`RELEASE 1.12 WP04 — TIMEOUT FAILURE-CLASS RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — TIMEOUT DECISION: <D1|D2|D3|D4|D5>`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: <EXACT_SET>`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 COMPATIBILITY: PRESERVED`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — TIMEOUT REMEDIATION PATH COUNT: <N>`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — CURRENT SOURCE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_GOOD_STATE`

`RELEASE 1.12 WP04 — TIMEOUT RECONCILIATION MUTATION AUDIT: PASS`

If executable helper remediation is selected:

`RELEASE 1.12 WP04 — TERRA TIMEOUT REMEDIATION AUTHORITY: READY`

If Azure/runtime investigation is selected:

`RELEASE 1.12 WP04 — TERRA AZURE TIMEOUT INVESTIGATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA TIMEOUT FAILURE-CLASS RECONCILIATION COMPLETE`

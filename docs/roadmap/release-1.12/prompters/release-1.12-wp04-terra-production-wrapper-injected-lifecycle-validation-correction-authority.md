# GPT-5.6 Terra — Release 1.12 WP04 Production-Wrapper Injected Lifecycle Validation Correction Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — owns lifecycle contract, validation standard, acceptance criteria, and publication readiness.
- **GPT-5.6 Terra** — PRIMARY: make the narrow wrapper-only testability correction needed to execute the actual production orchestration path under mocked callbacks in Windows PowerShell 5.1.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed baseline

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

PowerShell target:

```text
Windows PowerShell 5.1.26100.9444
```

Current deployed runtime image remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Retained diagnostic evidence must remain preserved:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

## 2. Binding reconciliation state

Already proven:

```text
FULL TRACKED DIFF SCOPE = PASS
H1 HELPER CONTRACT = PASS
ARCHIVE/EXTRACTION STATE MACHINE = PASS
RETAINED + PASS|EMPTY = eligible
RETAINED + FAIL|NOT_APPLICABLE = fail
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
PRE-RESTORATION EVIDENCE CHECKPOINT = PROVEN_COMPLETE
CHECKPOINT/RESTORATION ORDER = PASS
ERROR PRECEDENCE = PROVEN
RESTORE-ONLY EXACTLY ONCE = PASS
RB2 = PASS
EXPLICIT RESTART REMEDIATION = PASS
SECRET HYGIENE = PASS
GIT DIFF CHECK = PASS
```

Still not proven:

```text
PRODUCTION WRAPPER CONTROL FLOW MOCKED = NOT_PROVEN
WINDOWS POWERSHELL 5.1 FULL WRAPPER LIFECYCLE W1-W8 = NOT_PROVEN
PUBLICATION READINESS = BLOCKED
```

## 3. Concrete remaining defect

The existing W1–W8 fixture suite validates an external/mock lifecycle model.

It does **not** execute:

```text
initialize-qualification.ps1
```

through its actual production orchestration path with external side effects replaced by injected/mocked callbacks.

Therefore the lifecycle validation is not authoritative for the production wrapper.

## 4. Correction objective

Refactor only enough to make the actual production wrapper orchestration test-injectable while preserving normal production behavior exactly.

The same production control flow must execute in both:

```text
normal production mode
local validation mode
```

Only external side effects may be replaced in validation mode.

Do not create a second lifecycle implementation.

## 5. Exact tracked allowlist

Authorize exactly one tracked path:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Mutation counts:

```text
CREATE = 0
MODIFY = 1
DELETE = 0
TRACKED PATH COUNT = 1
```

Do not modify:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

If helper changes become necessary, STOP and return to Luna.

## 6. Required production callback seams

Introduce narrow dependency seams around external effects only.

Equivalent names are allowed, but the production wrapper must support injectable behavior for at least:

```text
InvokeDeferredQualification
RetrieveRawArchive
ExtractFreshWindow
DeriveDeepestBoundary
PersistEvidenceCheckpoint
InvokeRestoreOnly
```

Additional safe seams may be used for:

```text
UTC clock
hashing
filesystem writes
sleep/poll timing
```

only if needed.

## 7. Production-default behavior

The wrapper must preserve existing production behavior when no test callbacks are supplied.

Binding:

```text
default execution invokes existing real helper/Azure/file behavior
no caller must pass validation-only parameters in production
no validation mode is enabled implicitly
```

Validation injection must be explicit.

## 8. Injection design constraints

Preferred PowerShell 5.1-compatible patterns:

```text
optional [scriptblock] parameters
or
a hashtable/object of scriptblock callbacks
```

Avoid:

```text
PowerShell 7 classes/features
runspaces
dynamic module tricks
global mutable test hooks
environment-variable test switches that could accidentally affect production
```

All validation callbacks must be local-process-only.

## 9. Single orchestration path requirement

This is the central acceptance criterion.

There must be exactly one production lifecycle orchestration path implementing:

```text
Deferred qualification
→ RunId binding
→ transcript/poll retention
→ archive retrieval
→ extraction
→ boundary derivation
→ checkpoint persistence
→ finally RestoreOnly
→ final precedence
```

Tests must call that same path.

Forbidden:

```text
copying the lifecycle into a test function
reimplementing checkpoint logic in fixtures
reimplementing precedence logic in fixtures
validating only helper functions while skipping wrapper orchestration
```

## 10. Suggested structure

A valid shape is:

```powershell
param(
    ...production parameters...,
    [scriptblock]$InvokeDeferredQualificationCallback,
    [scriptblock]$RetrieveRawArchiveCallback,
    [scriptblock]$ExtractFreshWindowCallback,
    [scriptblock]$DeriveDeepestBoundaryCallback,
    [scriptblock]$PersistEvidenceCheckpointCallback,
    [scriptblock]$InvokeRestoreOnlyCallback
)
```

Then internally:

```text
if callback supplied -> invoke callback
else -> invoke production implementation
```

The lifecycle sequencing itself must remain outside these callbacks.

## 11. Callback contract safety

Callbacks may return only sanitized test records.

Do not allow callbacks to override:

```text
checkpoint truth table
error precedence
RestoreOnly exactly-once rule
RB2 attribution rules
final lifecycle result selection
```

Those remain production wrapper logic.

## 12. W1–W8 authoritative wrapper tests

Execute the actual `initialize-qualification.ps1` orchestration under Windows PowerShell 5.1 using injected callbacks.

### W1 — complete success

Injected results:

```text
Deferred success
actual synthetic RunId returned
archive RETAINED
extraction PASS
boundary B9
checkpoint persistence PASS
RestoreOnly PASS
```

Prove:

```text
same production wrapper path executed
checkpoint PASS
RestoreOnly invocation count = 1
final lifecycle result = qualification success
```

### W2 — retained archive + EMPTY

Injected:

```text
archive RETAINED
extraction EMPTY
```

Prove:

```text
checkpoint remains eligible/PASS if all other gates pass
RestoreOnly count = 1
```

### W3 — retained archive + FAIL

Injected:

```text
archive RETAINED
extraction FAIL
```

Prove:

```text
checkpoint FAIL
FinalLifecycleResult = EVIDENCE_PRESERVATION_FAILED
RestoreOnly count = 1
```

### W4 — retained archive + NOT_APPLICABLE

Force invalid combination.

Prove:

```text
checkpoint FAIL
inconsistent state retained
RestoreOnly count = 1
```

### W5 — archive retrieval failed

Injected:

```text
RAW_ARCHIVE_STATE=RETRIEVAL_FAILED
FRESH_EXTRACTION_STATE=NOT_APPLICABLE
durable retrieval-failure metadata supplied
```

Prove:

```text
checkpoint may remain reconcilable/PASS if all other gates pass
RestoreOnly count = 1
```

### W6 — qualification terminal failure

Injected:

```text
Deferred qualification failure
```

Prove:

```text
original qualification result retained
evidence path executes according to production logic
RestoreOnly count = 1
final result preserves correct precedence
```

### W7 — checkpoint persistence exception

Injected callback throws during checkpoint persistence.

Prove:

```text
wrapper catches/classifies as evidence preservation failure
RestoreOnly still executes exactly once
final result = EVIDENCE_PRESERVATION_FAILED unless restoration also fails
```

### W8 — RestoreOnly failure

Injected:

```text
qualification/evidence result present
RestoreOnly fails
```

Prove:

```text
FinalLifecycleResult = RESTORATION_FAILED
original qualification/evidence state still retained
RestoreOnly count = 1
```

## 13. Invocation-count proof

The harness must record callback invocation counts.

At minimum return counts for:

```text
Deferred qualification
archive retrieval
fresh extraction
boundary derivation
checkpoint persistence
RestoreOnly
```

Required:

```text
RestoreOnly = exactly 1 in W1-W8
```

Other counts must match scenario expectations.

## 14. Production-path proof

Return direct evidence that W1–W8 invoked the actual script path, for example:

```text
script invocation command line
validation callback mode marker
wrapper-produced terminal markers
callback invocation ledger
```

Do not rely solely on fixture code assertions.

Required marker:

```text
PRODUCTION_WRAPPER_CONTROL_FLOW_MOCKED=PASS
```

## 15. PowerShell 5.1 validation

Mandatory:

```text
parser errors = 0
parameter binding = PASS
all callback signatures bind in Windows PowerShell 5.1
W1-W8 execute under Windows PowerShell 5.1.26100.9444
```

No PS7-only syntax or APIs.

## 16. Production non-injection regression

Run a local no-side-effect parameter/parse inspection proving:

```text
callbacks omitted -> production defaults selected
```

Do not invoke Azure.

Required:

```text
PRODUCTION_DEFAULT_CALLBACK_SELECTION = PASS
```

## 17. H1/helper carry-forward

Helper remains untouched.

Confirm:

```text
Immediate unchanged
Deferred unchanged
RestoreOnly unchanged
LifecycleAction=None unchanged
RB2 unchanged
```

## 18. Secret hygiene

Use synthetic values only.

Scan changed script and validation output.

Expected:

```text
real secret findings = 0
```

Do not emit:

```text
HttpEvidenceToken
TwelveData__ApiKey
Authorization header
registry credentials
connection strings
cookies
```

## 19. Local-only authority

Forbidden:

```text
Azure mutation
Azure restart
Azure logging changes
new diagnostic run
acceptance retry
Twelve Data secret configuration
git add
git commit
git push
PR
merge
Docker build
GHCR publication
GitHub/Project/issue/milestone mutation
```

## 20. Tracked mutation audit

At completion prove:

```text
this correction modified exactly 1 authorized tracked path
full working tree still has exactly 2 governed modified tracked paths total
0 unauthorized tracked paths
0 staged paths
```

Run:

```text
git diff --check
```

Expected:

```text
PASS
```

## 21. Publication blocker resolution

The authority succeeds only if both become proven:

```text
PRODUCTION WRAPPER CONTROL FLOW MOCKED = PASS
WINDOWS POWERSHELL 5.1 FULL WRAPPER LIFECYCLE W1-W8 = PASS
```

If either remains unproven:

```text
PUBLICATION READINESS remains BLOCKED
```

Do not self-authorize publication.

## 22. Required output

Return:

- exact changed path;
- callback seam design;
- confirmation production default behavior is unchanged;
- confirmation single orchestration path;
- W1–W8 results;
- callback invocation ledger;
- production-wrapper invocation proof;
- PowerShell 5.1 parse/binding results;
- RestoreOnly exactly-once proof;
- secret scan;
- full two-path diff scope;
- mutation audit;
- whether publication blockers are fully resolved;
- readiness for Luna final pre-publication reconciliation.

## 23. Terminal markers

`RELEASE 1.12 WP04 — PRODUCTION-WRAPPER INJECTED LIFECYCLE VALIDATION CORRECTION: PASS`

`RELEASE 1.12 WP04 — CORRECTED TRACKED PATH COUNT: 1`

`RELEASE 1.12 WP04 — HELPER CONTRACT MODIFIED: NO`

`RELEASE 1.12 WP04 — PRODUCTION CALLBACK SEAMS: IMPLEMENTED`

`RELEASE 1.12 WP04 — PRODUCTION DEFAULT CALLBACK SELECTION: PASS`

`RELEASE 1.12 WP04 — SINGLE ORCHESTRATION PATH: PASS`

`RELEASE 1.12 WP04 — PRODUCTION WRAPPER CONTROL FLOW MOCKED: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 FULL WRAPPER LIFECYCLE W1-W8: PASS`

`RELEASE 1.12 WP04 — RESTORE-ONLY EXACTLY-ONCE VALIDATION: PASS`

`RELEASE 1.12 WP04 — ERROR PRECEDENCE: PROVEN`

`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: PROVEN_COMPLETE`

`RELEASE 1.12 WP04 — ARCHIVE/EVIDENCE CONTRACT: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE VALIDATION: PASS`

`RELEASE 1.12 WP04 — FULL TRACKED DIFF SCOPE: PASS`

`RELEASE 1.12 WP04 — IMPLEMENTED TRACKED PATH COUNT: 2`

`RELEASE 1.12 WP04 — UNAUTHORIZED TRACKED PATH COUNT: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — PRODUCTION-WRAPPER INJECTED LIFECYCLE VALIDATION CORRECTION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA PRODUCTION-WRAPPER INJECTED LIFECYCLE VALIDATION CORRECTION COMPLETE`

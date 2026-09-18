# GPT-5.6 Terra — Release 1.12 WP04 Single-Orchestration-Path Structural Refactor Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — owns contract, architecture, reconciliation, acceptance criteria, and publication readiness.
- **GPT-5.6 Terra** — PRIMARY: perform the narrow structural refactor required to make the production wrapper and validation suite execute one shared lifecycle orchestration path.
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

Current deployed image remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Retained diagnostic evidence must remain preserved:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

Current publication readiness:

```text
BLOCKED
```

Reason:

```text
production wrapper still owns inline lifecycle orchestration
tests still execute a separate mock lifecycle function
single orchestration path is NOT_PROVEN
```

## 2. Binding correction objective

Refactor:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

so there is exactly one lifecycle orchestration function used by both:

```text
normal production execution
local injected-callback validation
```

No duplicate lifecycle implementation may remain.

## 3. Exact tracked allowlist

Authorize exactly:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Do not modify:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Counts:

```text
CREATE = 0
MODIFY = 1
DELETE = 0
```

If another tracked path becomes necessary, STOP and return to Luna.

## 4. Required architecture

Extract the production lifecycle into one internal function, for example:

```powershell
Invoke-QualificationLifecycle
```

or semantically equivalent.

That one function must own all lifecycle sequencing:

```text
Deferred qualification
→ actual RunId binding
→ helper transcript retention
→ poll observation retention
→ D3 state capture
→ archive retrieval
→ archive metadata
→ fresh extraction
→ deepest-boundary derivation
→ checkpoint calculation
→ checkpoint persistence
→ exactly-one RestoreOnly in finally
→ final lifecycle precedence
```

The top-level script must call this function.

The W1–W8 harness must call the same function.

## 5. External operation injection

The orchestration function may accept optional PowerShell-5.1-compatible scriptblock callbacks for external effects only:

```text
InvokeDeferredQualification
RetrieveRawArchive
ExtractFreshWindow
DeriveDeepestBoundary
PersistEvidenceCheckpoint
InvokeRestoreOnly
```

Equivalent names are allowed.

Additional narrow callbacks are permitted only where necessary for:

```text
UTC time
sleep
hash/file operations
```

Do not move lifecycle decision logic into callbacks.

## 6. Production defaults

When no callbacks are supplied:

```text
real production operations are selected
existing helper behavior is used
existing archive retrieval behavior is used
existing extraction logic is used
existing boundary derivation is used
existing checkpoint persistence is used
existing RestoreOnly behavior is used
```

The production invocation must require no special testing parameter.

Required:

```text
PRODUCTION DEFAULT CALLBACK SELECTION = PASS
```

## 7. Single-path invariant

After the refactor, there must not remain:

```text
an inline top-level lifecycle implementation
a separate test lifecycle function
a copied precedence implementation
a copied checkpoint implementation
a copied finally/restoration implementation
```

Tests may only configure callbacks and invoke the shared orchestration function.

Required:

```text
SINGLE ORCHESTRATION PATH = PASS
```

## 8. Shared orchestration function responsibilities

The shared function must itself own:

```text
checkpoint truth table
archive/extraction state consistency
RB2 attribution
error precedence
RestoreOnly exactly-once semantics
final lifecycle result
```

Callbacks may supply results, not policy.

## 9. Archive/extraction truth table

Preserve exactly:

| Raw archive state | Fresh extraction state | Checkpoint eligibility |
|---|---|---|
| RETAINED | PASS | eligible |
| RETAINED | EMPTY | eligible |
| RETAINED | FAIL | fail |
| RETAINED | NOT_APPLICABLE | fail |
| RETRIEVAL_FAILED | NOT_APPLICABLE | eligible if retrieval failure is durably recorded |
| RETRIEVAL_FAILED | PASS | fail |
| RETRIEVAL_FAILED | EMPTY | fail |
| RETRIEVAL_FAILED | FAIL | fail |
| FAIL/UNKNOWN | any | fail |

No regression is permitted.

## 10. Error precedence

Preserve:

```text
RESTORATION_FAILED
> EVIDENCE_PRESERVATION_FAILED
> QualificationResult
```

while retaining all three underlying result classes separately.

## 11. RestoreOnly exactly-once rule

The shared orchestration function must contain the authoritative:

```powershell
try { ... }
finally { ... }
```

structure.

Required:

```text
RestoreOnly callback/operation invocation count = exactly 1
```

for every W1–W8 scenario.

Do not let top-level script and orchestration function both restore.

## 12. RB2

Preserve:

```text
helper owns/generates actual qualification RunId
wrapper/orchestration captures actual emitted RunId
metadata follows actual RunId
no competing outer qualification RunId
```

## 13. W1–W8 authoritative validation

Run all scenarios by invoking the same extracted production orchestration function with callbacks.

### W1 — full success

```text
Deferred success
archive RETAINED
extraction PASS
boundary B9
checkpoint persistence PASS
RestoreOnly PASS
```

Expected:

```text
checkpoint PASS
RestoreOnly count 1
final lifecycle result = qualification success
```

### W2 — retained + EMPTY

Expected:

```text
checkpoint eligible/PASS
RestoreOnly count 1
```

### W3 — retained + FAIL

Expected:

```text
checkpoint FAIL
final = EVIDENCE_PRESERVATION_FAILED
RestoreOnly count 1
```

### W4 — retained + NOT_APPLICABLE

Expected:

```text
checkpoint FAIL
inconsistent evidence state
RestoreOnly count 1
```

### W5 — retrieval failed + NOT_APPLICABLE

Expected:

```text
retrieval failure durably represented
checkpoint may remain reconcilable/PASS
RestoreOnly count 1
```

### W6 — qualification failure

Expected:

```text
qualification result retained
evidence preservation path still executes as governed
RestoreOnly count 1
precedence correct
```

### W7 — checkpoint persistence throws

Expected:

```text
evidence preservation failure
RestoreOnly count 1
final = EVIDENCE_PRESERVATION_FAILED unless restoration also fails
```

### W8 — restoration failure

Expected:

```text
RestoreOnly count 1
final = RESTORATION_FAILED
qualification/evidence results retained
```

## 14. Validation proof requirements

For each W1–W8, retain:

```text
scenario id
actual shared orchestration function invoked
callback invocation counts
checkpoint result
final lifecycle result
RestoreOnly count
```

Required global proof:

```text
all W1-W8 invoked the same production orchestration function
```

## 15. PowerShell 5.1 requirements

Mandatory:

```text
parser errors = 0
callback binding = PASS
shared orchestration function invocation = PASS
W1-W8 = PASS
```

Target:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7-only syntax or APIs.

## 16. Production non-injection regression

Without invoking Azure, prove from local inspection/binding that:

```text
top-level normal script execution selects production callbacks/defaults
```

Required:

```text
PRODUCTION DEFAULT CALLBACK SELECTION = PASS
```

## 17. Helper contract carry-forward

Helper remains untouched.

Reconfirm:

```text
Immediate unchanged
Deferred unchanged
RestoreOnly unchanged
LifecycleAction=None unchanged
null/absent semantics unchanged
RB2 unchanged
```

## 18. Secret hygiene

Synthetic values only.

Expected:

```text
secret findings = 0
```

Do not emit or persist real:

```text
HttpEvidenceToken
TwelveData__ApiKey
Authorization headers
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
Docker
GHCR
GitHub issue/project/milestone mutation
```

## 20. Mutation audit

At completion prove:

```text
this authority changed exactly 1 authorized tracked path
full working tree still contains exactly 2 governed modified tracked paths total
0 unauthorized tracked paths
0 staged paths
git diff --check = PASS
```

Unrelated untracked content remains untouched.

## 21. Success condition

This authority succeeds only if all are true:

```text
single orchestration function exists
top-level production path calls it
W1-W8 call it
no duplicate lifecycle implementation remains
production defaults are preserved
PowerShell 5.1 validation passes
RestoreOnly exactly once passes
```

If not, do not claim completion.

## 22. Required output

Return:

- extracted orchestration function name;
- exact changed path;
- callback seam design;
- proof top-level production path calls the shared function;
- proof W1–W8 call the same function;
- proof no duplicate lifecycle implementation remains;
- W1–W8 results and callback ledger;
- PowerShell 5.1 parse/binding results;
- RestoreOnly exactly-once results;
- helper carry-forward;
- secret scan;
- full two-path diff scope;
- mutation audit;
- whether publication blockers are fully resolved;
- readiness for Luna final pre-publication reconciliation.

## 23. Terminal markers

`RELEASE 1.12 WP04 — SINGLE-ORCHESTRATION-PATH STRUCTURAL REFACTOR: PASS`

`RELEASE 1.12 WP04 — CORRECTED TRACKED PATH COUNT: 1`

`RELEASE 1.12 WP04 — HELPER CONTRACT MODIFIED: NO`

`RELEASE 1.12 WP04 — SHARED LIFECYCLE ORCHESTRATION FUNCTION: IMPLEMENTED`

`RELEASE 1.12 WP04 — TOP-LEVEL PRODUCTION PATH USES SHARED ORCHESTRATION: PASS`

`RELEASE 1.12 WP04 — W1-W8 USE SHARED ORCHESTRATION: PASS`

`RELEASE 1.12 WP04 — DUPLICATE LIFECYCLE IMPLEMENTATION: ABSENT`

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

`RELEASE 1.12 WP04 — SINGLE-ORCHESTRATION-PATH STRUCTURAL REFACTOR MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA SINGLE-ORCHESTRATION-PATH STRUCTURAL REFACTOR COMPLETE`

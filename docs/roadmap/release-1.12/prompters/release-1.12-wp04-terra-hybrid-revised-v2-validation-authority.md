# GPT-5.6 Terra — Release 1.12 WP04 Hybrid Revised-V2 Validation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — owns the hybrid validation contract, acceptance criteria, reconciliation, and publication decision.
- **GPT-5.6 Terra** — PRIMARY: execute local-only hybrid validation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never replaces Luna/Terra.

## 1. Binding reconciled state

W4 is:

```text
R1_UNREACHABLE_DEFENSIVE
```

The actual production policy function already proves:

```text
RETAINED + NOT_APPLICABLE
Eligible = False
Classification = EVIDENCE_STATE_INCONSISTENT
```

Prior W4 defect finding is retracted. No W4 source correction occurred.

Hybrid partition is binding:

```text
W1,W2,W3,W5,W6,W7,W8
  -> exact-byte sandbox execution of current wrapper

W4
  -> direct Windows PowerShell 5.1 invocation of actual
     Test-Wp04ArchiveExtractionState production policy function
```

## 2. Absolute mutation boundary

This authority permits:

```text
tracked source changes = 0
staged paths = 0
commits/pushes = 0
PRs/merges = 0
Azure mutations = 0
Docker/GHCR mutations = 0
GitHub lifecycle mutations = 0
```

Do not modify either governed tracked path.

## 3. Windows PowerShell baseline

All validation must use:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7-only syntax, APIs, or behavior.

## 4. Exact-byte sandbox for reachable scenarios

Create a unique non-repository temporary sandbox:

```text
$env:TEMP\AIQuantTradingResearch\wp04-hybrid-v2\<validation-id>
```

Copy the current tracked:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

byte-for-byte.

Before any wrapper execution require:

```text
SHA256(tracked wrapper) == SHA256(sandbox wrapper)
```

Recheck after all reachable scenarios; sandbox bytes must remain identical.

If identity fails, STOP.

## 5. External-operation interception

Prepend sandbox shims and intercept all external surfaces used by the wrapper, including at minimum:

```text
git
az
helper invocation
```

Inspect the wrapper for any additional external commands and intercept them too.

Rules:

```text
no pass-through to real external commands
unexpected invocation -> fail closed
safe invocation ledger retained
real external invocation count = 0
```

## 6. Synthetic helper contract

The mock helper may simulate only external behavior needed by the real wrapper:

```text
Deferred
RestoreOnly
LifecycleAction=None
terminal telemetry
poll observations
D3 state
wrapper-owned synthetic actual RunId
```

Record:

```text
Deferred count
RestoreOnly count
LifecycleAction
synthetic RunId
terminal result
```

Preserve RB2. Use fresh synthetic IDs; never reuse historical Azure RunIds.

Mocks must not implement or decide the wrapper's archive/extraction truth table, checkpoint eligibility, error precedence, RestoreOnly ordering/count, or final lifecycle result.

## 7. Reachable exact-byte scenarios

Execute the actual sandbox wrapper for:

### W1 — retained/pass success
Require:

```text
RETAINED + PASS
checkpoint success
RestoreOnly exactly once
final qualification success
```

### W2 — retained/empty success
Require:

```text
RETAINED + EMPTY
checkpoint eligible/success
RestoreOnly exactly once
```

### W3 — retained/extraction failure
Induce a real extraction exception through permitted synthetic external/filesystem inputs such that production wrapper normalization yields:

```text
RETAINED + FAIL
```

Require:

```text
checkpoint/evidence failure
final = EVIDENCE_PRESERVATION_FAILED
RestoreOnly exactly once
```

Do not inject internal state directly.

### W5 — retrieval failure
Induce retrieval failure so the real wrapper produces:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE
```

Require durable retrieval-failure representation, correct checkpoint eligibility, and RestoreOnly exactly once.

### W6 — qualification failure
Require original qualification failure retained, evidence path executed as designed, correct precedence, RestoreOnly exactly once.

### W7 — checkpoint persistence failure
Use only:

```text
PersistEvidenceCheckpointCallback
```

with a throwing synthetic scriptblock.

Require:

```text
all earlier evidence writes succeed
callback invocation count = 1
final = EVIDENCE_PRESERVATION_FAILED
original qualification result retained
RestoreOnly exactly once
no global Set-Content override
```

### W8 — restoration failure
Synthetic RestoreOnly fails.

Require:

```text
RestoreOnly invocation count = 1
final = RESTORATION_FAILED
underlying qualification/evidence result retained
```

## 8. W4 direct production-policy validation

Do not attempt W4 through the wrapper process.

Under Windows PowerShell 5.1 invoke the actual production:

```text
Test-Wp04ArchiveExtractionState
```

from the current tracked wrapper logic.

Required:

```text
input ArchiveState = RETAINED
input FreshExtraction = NOT_APPLICABLE
Eligible = False
Classification = EVIDENCE_STATE_INCONSISTENT
```

Also rerun the complete governed state table:

```text
RETAINED + PASS                    => eligible
RETAINED + EMPTY                   => eligible
RETAINED + FAIL                    => fail
RETAINED + NOT_APPLICABLE          => fail / EVIDENCE_STATE_INCONSISTENT
RETRIEVAL_FAILED + NOT_APPLICABLE  => eligible
representative inconsistent values => fail closed
```

No duplicated truth-table implementation may substitute for the actual production function.

## 9. Scenario evidence ledger

Durably retain sufficient local evidence for Luna reconciliation.

For W1,W2,W3,W5,W6,W7,W8 record:

```text
scenario
tracked wrapper SHA256
sandbox wrapper SHA256
PowerShell version
synthetic actual RunId
helper ledger
az ledger
git ledger
archive state
extraction state
checkpoint state
deepest boundary
S2 callback count
RestoreOnly count
qualification result
evidence result
restoration result
final lifecycle result
exit code
unexpected real external invocation count
```

For W4 record:

```text
actual production function identity
inputs
Eligible
Classification
complete truth-table result
PowerShell version
```

## 10. Cross-scenario invariants

Require:

```text
RestoreOnly exactly once = PASS for W1,W2,W3,W5,W6,W7,W8
W7 S2 callback count = 1
S2 callback count W1,W2,W3,W5,W6,W8 = 0/default
unexpected real external invocations = 0
```

W4 has no lifecycle/RestoreOnly requirement because it is policy-function validation, not wrapper execution.

## 11. Error precedence

Prove using actual wrapper control flow:

```text
evidence preservation failure dominates successful qualification when applicable
restoration failure dominates prior qualification/evidence result
original underlying result remains retained for diagnostics
```

Required:

```text
ERROR PRECEDENCE = PROVEN
```

## 12. Evidence checkpoint contract

For reachable wrapper scenarios, validate the actual pre-restoration checkpoint contract and archive/extraction state handling.

Required:

```text
PRE-RESTORATION EVIDENCE CHECKPOINT = PROVEN_COMPLETE
ARCHIVE/EVIDENCE CONTRACT = PASS
```

## 13. Secret hygiene

Use synthetic values only.

Do not access, print, or persist real:

```text
TwelveData__ApiKey
HttpEvidenceToken
Authorization headers
registry credentials
connection strings
cookies
```

Required:

```text
REAL SECRET FINDINGS = 0
```

## 14. Working-tree proof

Before and after:

```text
git status --short
```

Expected full tracked modified scope remains exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Require:

```text
new tracked mutations = 0
unauthorized tracked paths = 0
staged paths = 0
git diff --check = PASS
```

## 15. Cleanup

After durable validation evidence is retained:

```text
remove only the temporary hybrid-V2 sandbox
```

Preserve all previously retained diagnostic evidence.

## 16. Publication boundary

If every hybrid gate passes:

```text
PUBLICATION BLOCKER = RESOLVED_PENDING_LUNA_FINAL_RECONCILIATION
```

Do not publish yet.

## 17. Acceptance boundary

This validation does not grant Azure D3 acceptance.

Preserve:

```text
NEW IMAGE REQUIRED = NO
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
```

## 18. Required output

Return:
- sandbox path and cleanup;
- tracked/sandbox wrapper hashes before/after;
- interception inventory and zero-real-call proof;
- W1,W2,W3,W5,W6,W7,W8 exact-byte ledgers;
- W4 actual production-policy result and truth-table result;
- RestoreOnly counts;
- W7 callback count;
- precedence/checkpoint/archive results;
- PowerShell 5.1 result;
- secret scan;
- before/after working-tree audit;
- publication-blocker state;
- readiness for Luna final pre-publication reconciliation.

## 19. Terminal markers

`RELEASE 1.12 WP04 — HYBRID REVISED-V2 VALIDATION: PASS`

`RELEASE 1.12 WP04 — SELECTED VALIDATION MODEL: HYBRID_REVISED_V2`

`RELEASE 1.12 WP04 — TRACKED SOURCE CHANGES DURING VALIDATION: 0`

`RELEASE 1.12 WP04 — EXACT-BYTE REACHABLE-SCENARIO WRAPPER IDENTITY: PASS`

`RELEASE 1.12 WP04 — EXTERNAL OPERATION INTERCEPTION: PASS`

`RELEASE 1.12 WP04 — UNEXPECTED REAL EXTERNAL INVOCATIONS: 0`

`RELEASE 1.12 WP04 — W1 RESULT: PASS`

`RELEASE 1.12 WP04 — W2 RESULT: PASS`

`RELEASE 1.12 WP04 — W3 RESULT: PASS`

`RELEASE 1.12 WP04 — W4 RESULT: PASS`

`RELEASE 1.12 WP04 — W5 RESULT: PASS`

`RELEASE 1.12 WP04 — W6 RESULT: PASS`

`RELEASE 1.12 WP04 — W7 RESULT: PASS`

`RELEASE 1.12 WP04 — W8 RESULT: PASS`

`RELEASE 1.12 WP04 — W4 VALIDATION LAYER: ACTUAL_PRODUCTION_POLICY_FUNCTION`

`RELEASE 1.12 WP04 — W4 PRODUCTION REACHABILITY: R1_UNREACHABLE_DEFENSIVE`

`RELEASE 1.12 WP04 — W4 ELIGIBLE: FALSE`

`RELEASE 1.12 WP04 — W4 CLASSIFICATION: EVIDENCE_STATE_INCONSISTENT`

`RELEASE 1.12 WP04 — ARCHIVE/EXTRACTION TRUTH TABLE: PASS`

`RELEASE 1.12 WP04 — W7 CHECKPOINT CALLBACK INVOCATION COUNT: 1`

`RELEASE 1.12 WP04 — RESTORE-ONLY EXACTLY-ONCE VALIDATION: PASS`

`RELEASE 1.12 WP04 — ERROR PRECEDENCE: PROVEN`

`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: PROVEN_COMPLETE`

`RELEASE 1.12 WP04 — ARCHIVE/EVIDENCE CONTRACT: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 HYBRID VALIDATION: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE VALIDATION: PASS`

`RELEASE 1.12 WP04 — FULL TRACKED DIFF SCOPE: PASS`

`RELEASE 1.12 WP04 — IMPLEMENTED TRACKED PATH COUNT: 2`

`RELEASE 1.12 WP04 — UNAUTHORIZED TRACKED PATH COUNT: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — SANDBOX CLEANUP: PASS`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: RESOLVED_PENDING_LUNA_FINAL_RECONCILIATION`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — HYBRID REVISED-V2 VALIDATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA HYBRID REVISED-V2 VALIDATION COMPLETE`

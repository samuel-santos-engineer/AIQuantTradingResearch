# GPT-5.6 Terra — Release 1.12 WP04 Second Narrow Lifecycle Remediation Correction Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — owns contract, architecture, reconciliation, acceptance criteria, and the D2 correction boundary.
- **GPT-5.6 Terra** — PRIMARY: implement the final wrapper-only correction for archive/extraction checkpoint semantics and prove the complete Windows PowerShell 5.1 wrapper lifecycle with mocked callbacks.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

PowerShell target:

```text
Windows PowerShell 5.1.26100.9444
```

Current uncommitted tracked remediation is based on source anchor:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Current deployed image remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Retained diagnostic evidence must remain untouched:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

## 2. Binding reconciliation result

The prior post-correction reconciliation established:

```text
FULL TRACKED DIFF SCOPE = PASS
H1 HELPER CONTRACT CARRY-FORWARD = PASS
CHECKPOINT/RESTORATION ORDER = PASS
ERROR PRECEDENCE = PASS
RB2 IMPLEMENTATION = PASS
EXPLICIT RESTART REMEDIATION = PASS
SECRET HYGIENE = PASS
GIT DIFF CHECK = PASS
```

Still blocking:

```text
PRE-RESTORATION EVIDENCE CHECKPOINT = INCOMPLETE
ARCHIVE/EVIDENCE CONTRACT = INCOMPLETE
WINDOWS POWERSHELL 5.1 FULL LIFECYCLE GATE = NOT_PROVEN
PUBLICATION READINESS = REMEDIATION_REQUIRED
POST-CORRECTION DECISION = D2
```

## 3. Concrete remaining defects

### Defect A — invalid extraction state acceptance

When a raw archive is retained, extraction failure can leave:

```text
FreshExtraction=NOT_APPLICABLE
```

and current checkpoint logic may still accept that state.

This violates the governed contract.

Binding correction:

```text
IF archive retained
THEN FreshExtraction MUST be exactly PASS | EMPTY | FAIL
```

`NOT_APPLICABLE` is valid only when no archive exists because archive retrieval itself reached a durable terminal failure.

### Defect B — incomplete lifecycle test coverage

Current L1–L6 validation exercises fixture logic and precedence calculations but does not execute the complete wrapper control flow with mocked:

```text
Deferred helper
archive retrieval
fresh extraction
boundary derivation
checkpoint write
RestoreOnly callback
```

Therefore the full Windows PowerShell 5.1 lifecycle gate remains not proven.

## 4. Correction objective

Make the canonical wrapper fail closed on extraction state and prove the entire wrapper lifecycle under Windows PowerShell 5.1 using mocked callbacks.

Required final invariants:

```text
archive retained + extraction PASS  -> checkpoint may pass
archive retained + extraction EMPTY -> checkpoint may pass
archive retained + extraction FAIL  -> checkpoint must fail
archive retained + extraction NOT_APPLICABLE -> invalid state / checkpoint fail
archive retrieval failed durably -> FreshExtraction=NOT_APPLICABLE allowed
```

and:

```text
complete wrapper orchestration executes under mocks
RestoreOnly invoked exactly once
error precedence preserved
no Azure mutation
```

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

The helper contract is already accepted.

If the helper must change, STOP and return to Luna.

## 6. Extraction-state contract

Implement an explicit state machine.

### Archive retrieval success

If:

```text
RAW_ARCHIVE_STATE=RETAINED
```

then:

```text
FRESH_EXTRACTION_STATE ∈ {PASS, EMPTY, FAIL}
```

and never:

```text
NOT_APPLICABLE
```

### Archive retrieval terminal failure

If:

```text
RAW_ARCHIVE_STATE=RETRIEVAL_FAILED
```

then:

```text
FRESH_EXTRACTION_STATE=NOT_APPLICABLE
```

is valid because there is no archive to extract.

### Any inconsistent combination

Examples:

```text
RETAINED + NOT_APPLICABLE
RETAINED + missing extraction state
RETRIEVAL_FAILED + PASS
RETRIEVAL_FAILED + EMPTY
```

must force:

```text
EVIDENCE_CHECKPOINT=FAIL
```

and a durable classification such as:

```text
EVIDENCE_STATE_INCONSISTENT
```

or semantically equivalent.

## 7. Checkpoint truth table

Implement or validate this exact checkpoint relation:

| Raw archive state | Fresh extraction state | Checkpoint eligibility |
|---|---|---|
| RETAINED | PASS | eligible |
| RETAINED | EMPTY | eligible |
| RETAINED | FAIL | fail |
| RETAINED | NOT_APPLICABLE | fail |
| RETRIEVAL_FAILED | NOT_APPLICABLE | eligible if failure durably recorded |
| RETRIEVAL_FAILED | PASS/EMPTY/FAIL | fail |
| FAIL/UNKNOWN | any | fail |

"Eligible" means this dimension does not itself fail the checkpoint; all other checkpoint requirements must still pass.

## 8. Deepest-boundary semantics

If extraction fails after archive retention:

```text
DEEPEST_BOUNDARY
```

may be:

```text
BX
```

or a boundary proven from already-retained helper/transcript evidence.

Do not infer a deeper boundary from an extraction that failed.

Persist the failure distinctly.

## 9. Complete wrapper callback seams

For local validation only, the wrapper must expose or support safe mocked seams for the full lifecycle without Azure mutation.

Equivalent implementation approaches are allowed:

```text
scriptblock parameters
internal functions overridden in harness
command shims/mocks
dependency-injected callbacks
```

Do not weaken production behavior merely to facilitate tests.

The mocked seams must cover at minimum:

```text
InvokeDeferredQualification
RetrieveRawArchive
ExtractFreshWindow
DeriveDeepestBoundary
WriteEvidenceCheckpoint
InvokeRestoreOnly
```

Names may differ; behavior must be equivalent.

## 10. Full wrapper lifecycle validation

Execute the actual wrapper control flow under Windows PowerShell 5.1 with mocks/stubs.

The test must exercise:

```text
try
{
    Deferred helper
    RunId binding
    transcript/poll retention
    archive retrieval
    extraction
    deepest-boundary derivation
    checkpoint write
}
finally
{
    RestoreOnly exactly once
}
```

This must be the same production control-flow path, not a duplicate reimplementation of the precedence algorithm.

## 11. Required mocked lifecycle cases

### W1 — complete success

Mock:

```text
Deferred = success
RunId = actual synthetic RunId
archive = RETAINED
extraction = PASS
boundary = B9
checkpoint = PASS
RestoreOnly = PASS
```

Prove:

```text
final lifecycle result = qualification success
RestoreOnly count = 1
```

### W2 — archive retained, extraction EMPTY

Mock:

```text
archive = RETAINED
extraction = EMPTY
```

Prove:

```text
checkpoint may remain PASS
RestoreOnly count = 1
```

### W3 — archive retained, extraction FAIL

Mock:

```text
archive = RETAINED
extraction = FAIL
```

Prove:

```text
checkpoint = FAIL
FinalLifecycleResult = EVIDENCE_PRESERVATION_FAILED
RestoreOnly count = 1
```

### W4 — archive retained, extraction NOT_APPLICABLE

Force this invalid state.

Prove:

```text
checkpoint = FAIL
classification = inconsistent/invalid evidence state
RestoreOnly count = 1
```

### W5 — archive retrieval terminal failure

Mock:

```text
archive = RETRIEVAL_FAILED
retrieval failure durably recorded
extraction = NOT_APPLICABLE
```

Prove:

```text
checkpoint may remain reconcilable
RestoreOnly count = 1
```

### W6 — qualification/poll failure

Mock:

```text
Deferred helper terminal failure
```

Prove:

```text
failure retained
evidence workflow still attempts preservation as designed
RestoreOnly count = 1
```

### W7 — checkpoint-writing exception

Mock exception during checkpoint persistence.

Prove:

```text
best-effort failure state retained if possible
RestoreOnly count = 1
```

### W8 — RestoreOnly failure

Mock:

```text
qualification/evidence result present
RestoreOnly = FAIL
```

Prove:

```text
FinalLifecycleResult = RESTORATION_FAILED
original qualification/evidence result still retained
RestoreOnly count = 1
```

## 12. Windows PowerShell 5.1 proof

All W1–W8 must execute under:

```text
Windows PowerShell 5.1.26100.9444
```

Required:

```text
parser errors = 0
parameter binding = PASS
production wrapper control-flow path exercised = YES
RestoreOnly invocation counting = PASS
```

No PowerShell 7-only features.

## 13. No test-only bypass of production guards

Mocks must not bypass:

```text
RB2 RunId handling
checkpoint evaluation
finally restoration
error precedence
secret hygiene
```

The harness may replace external side effects, not the lifecycle decision logic being tested.

## 14. H1 helper carry-forward

Reconfirm no helper changes.

Required:

```text
Immediate = unchanged
Deferred = unchanged
RestoreOnly = unchanged
LifecycleAction=None = unchanged
RB2 = unchanged
```

## 15. Secret hygiene

Synthetic fixtures only.

Expected scan:

```text
real secret findings = 0
```

Do not emit or persist:

```text
HttpEvidenceToken
TwelveData__ApiKey
Authorization
registry credentials
connection strings
cookies
```

## 16. Local-only authority

Forbidden:

```text
Azure mutation
Azure restart
Azure logging change
new diagnostic run
acceptance retry
Twelve Data secret configuration
git add
git commit
git push
PR/merge
Docker build
GHCR publication
GitHub/Project/issue/milestone mutation
```

## 17. Tracked mutation audit

At completion prove:

```text
exactly 1 authorized tracked path changed by this correction
full working tree still contains exactly the two previously governed modified paths total
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

## 18. Required output

Return:

- exact changed path;
- extraction-state correction summary;
- checkpoint truth-table validation;
- W1–W8 results;
- proof production wrapper control flow was exercised;
- PowerShell 5.1 parse/binding result;
- RestoreOnly exactly-once proof;
- secret scan;
- full two-path diff scope;
- tracked mutation audit;
- whether all publication blockers are now resolved;
- readiness for Luna reconciliation.

## 19. Terminal markers

`RELEASE 1.12 WP04 — SECOND NARROW LIFECYCLE REMEDIATION CORRECTION: PASS`

`RELEASE 1.12 WP04 — CORRECTED TRACKED PATH COUNT: 1`

`RELEASE 1.12 WP04 — HELPER CONTRACT MODIFIED: NO`

`RELEASE 1.12 WP04 — RETAINED ARCHIVE EXTRACTION STATE CONTRACT: ENFORCED`

`RELEASE 1.12 WP04 — RETAINED+NOT_APPLICABLE CHECKPOINT RESULT: FAIL`

`RELEASE 1.12 WP04 — RETAINED+FAIL CHECKPOINT RESULT: FAIL`

`RELEASE 1.12 WP04 — RETRIEVAL_FAILED+NOT_APPLICABLE CHECKPOINT RESULT: ELIGIBLE`

`RELEASE 1.12 WP04 — CHECKPOINT TRUTH TABLE: PASS`

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

`RELEASE 1.12 WP04 — SECOND NARROW LIFECYCLE REMEDIATION CORRECTION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA SECOND NARROW LIFECYCLE REMEDIATION CORRECTION COMPLETE`

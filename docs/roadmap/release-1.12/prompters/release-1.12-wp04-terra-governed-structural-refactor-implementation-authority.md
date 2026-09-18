# GPT-5.6 Terra — Release 1.12 WP04 Governed Structural Refactor Implementation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — owns the refactor contract, architecture, acceptance criteria, scope, and reconciliation.
- **GPT-5.6 Terra** — PRIMARY: implement the S1/P1 single-path structural refactor and validate it locally.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed baseline

Release:

```text
Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery
```

Issue:

```text
#263
```

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

## 2. Binding Luna decisions

Selected refactor scope:

```text
S1 — one-path refactor
```

Selected implementation phasing:

```text
P1 — single implementation authority
```

Authorized tracked path:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Explicitly untouched:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

No additional tracked path may be modified.

## 3. Required target architecture

Introduce one authoritative orchestration function:

```text
Invoke-Wp04QualificationLifecycle
```

Name may vary only if semantically equivalent and clearly identifiable.

It must become the single owner of lifecycle policy and sequencing.

It must own:

```text
Deferred helper invocation
actual RunId adoption/binding
helper transcript retention
poll observation retention
D3 state capture
raw archive retrieval
archive metadata capture
fresh extraction
deepest-boundary derivation
archive/extraction truth-table evaluation
durable checkpoint calculation/persistence
exactly one RestoreOnly call in finally
result/error precedence
terminal lifecycle result
```

## 4. Single lifecycle owner invariant

After refactor:

```text
top-level wrapper MUST NOT retain lifecycle sequencing
Invoke-Wp04MockLifecycle MUST NOT remain as a second lifecycle implementation
test code MUST NOT duplicate checkpoint policy
test code MUST NOT duplicate precedence policy
test code MUST NOT duplicate restoration/finally policy
```

`Invoke-Wp04MockLifecycle` must either:

```text
be removed
```

or:

```text
be reduced to callback fixtures/assertions only, with no lifecycle policy
```

## 5. Callback seam contract

The shared orchestration function must support optional Windows PowerShell 5.1-compatible scriptblock callbacks for external effects only:

```text
InvokeDeferredQualification
RetrieveRawArchive
ExtractFreshWindow
DeriveDeepestBoundary
PersistEvidenceCheckpoint
InvokeRestoreOnly
```

Equivalent parameter names are allowed.

Callbacks supply effects/results only.

Callbacks MUST NOT own or override:

```text
RB2 RunId policy
checkpoint truth table
archive/extraction consistency
error precedence
RestoreOnly invocation count
final lifecycle result
terminal classification
```

## 6. Production defaults

When callbacks are omitted:

```text
existing real helper invocation executes
existing Azure archive retrieval executes
existing filesystem extraction executes
existing boundary derivation executes
existing checkpoint persistence executes
existing RestoreOnly behavior executes
```

No environment-variable test switch.

No global mutable test hook.

No PowerShell 7-only features.

Required:

```text
PRODUCTION DEFAULT CALLBACK SELECTION = PASS
```

## 7. Top-level wrapper after refactor

Top-level responsibilities must be reduced to:

```text
parse/validate inputs
perform existing preflight/provenance/image checks
construct/resolve production adapters/defaults
invoke Invoke-Wp04QualificationLifecycle exactly once
emit final result
set exit code
```

The prior inline qualification `try/finally` lifecycle must be removed from top level.

## 8. Helper invocation equivalence

Production default Deferred invocation must preserve the previously accepted helper contract exactly:

```text
RestorationMode=Deferred
LifecycleAction=None
helper owns actual RunId
same governed six-setting scope
same evidence path semantics
same safe transcript behavior
```

No helper file edit is authorized.

## 9. RB2 preservation

The orchestration function must preserve:

```text
helper-generated actual RunId is authoritative
wrapper/orchestration adopts actual emitted RunId
metadata follows actual RunId
no competing outer qualification RunId
```

Any local correlation identifier must remain semantically distinct from qualification RunId.

## 10. Archive/extraction truth table

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

## 11. Pre-restoration checkpoint contract

The shared lifecycle function must durably determine before restoration:

```text
ACTUAL_RUN_ID
HELPER_TERMINAL_RETAINED
HELPER_TRANSCRIPT_RETAINED
POLL_OBSERVATIONS_RETAINED
D3_PAYLOAD_STATE
RAW_ARCHIVE_STATE
ARCHIVE_METADATA_STATE
FRESH_EXTRACTION_STATE
DEEPEST_BOUNDARY
EVIDENCE_CHECKPOINT
```

Then, and only then, `finally` executes one RestoreOnly call.

On unexpected evidence-path exceptions:

```text
best-effort EVIDENCE_CHECKPOINT=FAIL is persisted where possible
RestoreOnly still executes exactly once
```

## 12. Error precedence

The shared function must preserve independent results:

```text
QualificationResult
EvidenceCheckpointResult
RestorationResult
```

Final precedence:

```text
RESTORATION_FAILED
> EVIDENCE_PRESERVATION_FAILED
> QualificationResult
```

Original qualification/evidence classifications must remain retained even when a later class dominates.

## 13. RestoreOnly exactly-once contract

The shared function is the only owner of:

```powershell
try {
    # lifecycle/evidence work
}
finally {
    # exactly one RestoreOnly invocation
}
```

Required:

```text
RestoreOnly invocation count = 1
```

for every W1–W8 scenario.

Top-level code must not perform a second restoration.

## 14. Production equivalence proof

Before claiming PASS, compare refactored production-default behavior against the pre-refactor implementation for:

```text
helper parameters
RestorationMode=Deferred
LifecycleAction=None
RB2 actual RunId behavior
evidence-root semantics
transcript handling
poll retention
archive retrieval behavior
archive metadata
fresh extraction behavior
checkpoint truth table
deepest-boundary behavior
RestoreOnly behavior
error precedence
terminal markers
exit-code behavior
```

Return any intentional differences. Expected:

```text
behavioral differences = NONE
except structural testability refactor
```

## 15. Authoritative W1–W8 validation

The W1–W8 suite must invoke the exact shared orchestration function used by production.

Tests may inject callbacks but may not reimplement lifecycle decisions.

For every scenario record:

```text
shared function invoked
callback invocation ledger
actual synthetic RunId
archive state
extraction state
checkpoint state
final lifecycle result
RestoreOnly invocation count
```

### W1 — full success

Expected:

```text
checkpoint PASS
final result = qualification success
RestoreOnly = 1
```

### W2 — RETAINED + EMPTY

Expected:

```text
checkpoint eligible/PASS
RestoreOnly = 1
```

### W3 — RETAINED + FAIL

Expected:

```text
checkpoint FAIL
final = EVIDENCE_PRESERVATION_FAILED
RestoreOnly = 1
```

### W4 — RETAINED + NOT_APPLICABLE

Expected:

```text
checkpoint FAIL
evidence-state inconsistency retained
RestoreOnly = 1
```

### W5 — RETRIEVAL_FAILED + NOT_APPLICABLE

Expected:

```text
retrieval failure durably represented
checkpoint may remain PASS if all other requirements satisfied
RestoreOnly = 1
```

### W6 — qualification failure

Expected:

```text
qualification failure retained
evidence preservation still follows shared production policy
RestoreOnly = 1
precedence correct
```

### W7 — checkpoint persistence failure

Expected:

```text
final = EVIDENCE_PRESERVATION_FAILED unless restoration also fails
RestoreOnly = 1
```

### W8 — restoration failure

Expected:

```text
final = RESTORATION_FAILED
underlying qualification/evidence result retained
RestoreOnly = 1
```

## 16. Callback invocation proof

Produce an invocation ledger for all six callback classes across W1–W8.

At minimum show:

```text
Deferred qualification count
archive retrieval count
fresh extraction count
boundary derivation count
checkpoint persistence count
RestoreOnly count
```

Required:

```text
W1-W8 all invoke Invoke-Wp04QualificationLifecycle
RestoreOnly count = exactly 1 per scenario
```

## 17. PowerShell 5.1 gate

Run under exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Required:

```text
parser errors = 0
callback parameter binding = PASS
production-default binding = PASS
shared orchestration invocation = PASS
W1-W8 = PASS
```

Do not rely on PowerShell 7.

## 18. Secret hygiene

Synthetic fixtures only.

Expected:

```text
real secret findings = 0
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
Docker build
GHCR publication
GitHub issue/project/milestone mutation
```

## 20. Tracked mutation audit

At completion prove:

```text
this authority modifies exactly 1 authorized tracked path
full working tree contains exactly 2 governed modified tracked paths total
0 unauthorized tracked paths
0 staged paths
0 tracked creates/deletes
```

Run:

```text
git diff --check
```

Expected:

```text
PASS
```

Unrelated untracked user content remains untouched.

## 21. Publication boundary

Even if implementation passes:

```text
do not stage
do not commit
do not push
do not create PR
do not merge
```

Publication requires a later Luna reconciliation.

## 22. Acceptance boundary

Preserve:

```text
PUBLICATION READINESS = BLOCKED pending Luna reconciliation
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
NEW IMAGE REQUIRED = NO
```

## 23. Required output

Return:

- exact changed path;
- final orchestration function name;
- pre/post structural map;
- confirmation top-level lifecycle was removed;
- treatment/removal of `Invoke-Wp04MockLifecycle`;
- callback seam design;
- production-default adapter mapping;
- production-equivalence comparison;
- W1–W8 results;
- callback invocation ledger;
- PowerShell 5.1 parse/binding results;
- RestoreOnly exactly-once proof;
- secret scan;
- full two-path diff scope;
- mutation audit;
- whether all structural publication blockers are now resolved;
- readiness for Luna final pre-publication reconciliation.

## 24. Terminal markers

`RELEASE 1.12 WP04 — GOVERNED STRUCTURAL REFACTOR IMPLEMENTATION: PASS`

`RELEASE 1.12 WP04 — SELECTED REFACTOR SCOPE: S1`

`RELEASE 1.12 WP04 — SELECTED IMPLEMENTATION PHASING: P1`

`RELEASE 1.12 WP04 — CORRECTED TRACKED PATH COUNT: 1`

`RELEASE 1.12 WP04 — HELPER CONTRACT MODIFIED: NO`

`RELEASE 1.12 WP04 — SHARED LIFECYCLE ORCHESTRATION FUNCTION: IMPLEMENTED`

`RELEASE 1.12 WP04 — TOP-LEVEL INLINE LIFECYCLE: REMOVED`

`RELEASE 1.12 WP04 — DUPLICATE MOCK LIFECYCLE POLICY: ABSENT`

`RELEASE 1.12 WP04 — PRODUCTION CALLBACK SEAMS: IMPLEMENTED`

`RELEASE 1.12 WP04 — PRODUCTION DEFAULT CALLBACK SELECTION: PASS`

`RELEASE 1.12 WP04 — PRODUCTION BEHAVIORAL EQUIVALENCE: PASS`

`RELEASE 1.12 WP04 — SINGLE ORCHESTRATION PATH: PASS`

`RELEASE 1.12 WP04 — PRODUCTION WRAPPER CONTROL FLOW MOCKED: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 FULL WRAPPER LIFECYCLE W1-W8: PASS`

`RELEASE 1.12 WP04 — RESTORE-ONLY EXACTLY-ONCE VALIDATION: PASS`

`RELEASE 1.12 WP04 — ERROR PRECEDENCE: PROVEN`

`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: PROVEN_COMPLETE`

`RELEASE 1.12 WP04 — ARCHIVE/EVIDENCE CONTRACT: PASS`

`RELEASE 1.12 WP04 — RB2 IMPLEMENTATION: PASS`

`RELEASE 1.12 WP04 — EXPLICIT RESTART REMEDIATION: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE VALIDATION: PASS`

`RELEASE 1.12 WP04 — FULL TRACKED DIFF SCOPE: PASS`

`RELEASE 1.12 WP04 — IMPLEMENTED TRACKED PATH COUNT: 2`

`RELEASE 1.12 WP04 — UNAUTHORIZED TRACKED PATH COUNT: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — PUBLICATION READINESS: BLOCKED_PENDING_LUNA_RECONCILIATION`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — GOVERNED STRUCTURAL REFACTOR IMPLEMENTATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA GOVERNED STRUCTURAL REFACTOR IMPLEMENTATION COMPLETE`

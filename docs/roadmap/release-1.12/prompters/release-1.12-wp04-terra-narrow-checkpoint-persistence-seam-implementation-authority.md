# GPT-5.6 Terra — Release 1.12 WP04 Narrow Checkpoint-Persistence Seam Implementation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — owns the validation contract, seam design, acceptance criteria, and later reconciliation/publication decision.
- **GPT-5.6 Terra** — PRIMARY: implement the S2 checkpoint-persistence seam in exactly one tracked path and validate it locally.
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

Re-governance established:

```text
FUNCTIONAL PRODUCTION DEFECT PROVEN = NO
GLOBAL SET-CONTENT OVERRIDE = REJECTED
STRUCTURAL REFACTOR REQUIRED BY DEFAULT = NO
SELECTED CHECKPOINT SEAM DESIGN = S2
TRACKED SOURCE CHANGE COUNT = 1
HELPER MODIFICATION REQUIRED = NO
W1-W6/W8 V2 VALIDATION MODEL = PRESERVE
W7 ISOLATED FAULT INJECTION = PROVABLE
RE-GOVERNANCE DECISION = D1
```

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

No other tracked path may change.

## 4. Seam objective

Introduce one optional PowerShell 5.1-compatible scriptblock seam that wraps **only** final evidence-checkpoint persistence.

Preferred conceptual parameter:

```powershell
[scriptblock]$PersistEvidenceCheckpointCallback
```

Equivalent naming is acceptable.

The seam exists solely so W7 can inject a failure at the final checkpoint write without altering any other filesystem operation.

## 5. Production default

If no callback is supplied:

```text
execute the exact current checkpoint persistence behavior
```

Required semantic equivalence:

```text
same checkpoint path
same checkpoint content
same encoding
same overwrite behavior
same ordering
same exception propagation
same caller-visible result
```

No environment-variable switch.

No global mutable test hook.

No PowerShell 7-only behavior.

## 6. Isolation boundary

The new seam must wrap only:

```text
final evidence checkpoint persistence
```

It must not wrap or alter:

```text
helper transcript writes
poll-observation writes
D3-state writes
raw archive writes
archive metadata writes
fresh extraction writes
deepest-boundary writes
final lifecycle-result writes
other Set-Content calls
```

## 7. Lifecycle policy remains unchanged

Do not change:

```text
RB2 actual RunId adoption
archive/extraction truth table
checkpoint eligibility logic
checkpoint/restoration ordering
RestoreOnly exactly-once behavior
error precedence
LifecycleAction=None
Deferred helper semantics
null/absent restoration semantics
```

This is a testability seam only.

## 8. W7 fault-injection contract

Under local validation, supplying the callback must permit:

```text
all transcript/poll/archive/extraction/result writes = success
checkpoint persistence only = injected failure
```

Expected production-wrapper behavior:

```text
checkpoint persistence failure is classified as evidence preservation failure
RestoreOnly still executes exactly once
final lifecycle result = EVIDENCE_PRESERVATION_FAILED
unless RestoreOnly separately fails, in which case RESTORATION_FAILED dominates
original qualification result remains retained
```

## 9. Local seam validation

Under Windows PowerShell 5.1, validate at minimum:

### S2-T1 — production default

```text
callback omitted
default checkpoint writer selected
semantic output matches pre-seam behavior
```

### S2-T2 — injected checkpoint success

```text
callback invoked exactly once
checkpoint success path preserved
```

### S2-T3 — injected checkpoint failure

```text
callback fails
failure propagates/classifies exactly as W7 requires
unrelated writes unaffected
```

### S2-T4 — isolation

Prove the callback is not invoked for:

```text
transcript
poll observations
archive
extraction
final result
```

### S2-T5 — exception behavior

Injected exception from callback must follow the same caller-visible failure path as native checkpoint-persistence failure.

## 10. PowerShell 5.1 gate

Run under exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Required:

```text
parser errors = 0
parameter binding = PASS
callback omission/default binding = PASS
callback success binding = PASS
callback failure binding = PASS
```

## 11. Production-equivalence check

Compare the default path against the pre-seam checkpoint writer.

Expected:

```text
PRODUCTION CHECKPOINT PERSISTENCE EQUIVALENCE = PASS
```

No intentional runtime behavior difference is allowed.

## 12. Secret hygiene

Use synthetic values only.

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

## 13. Local-only authority

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

## 14. Tracked mutation audit

At completion prove:

```text
this authority changed exactly 1 authorized tracked path
full working tree still contains exactly the two governed modified tracked paths total
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

## 15. Publication boundary

Even if this seam passes:

```text
do not publish yet
```

Next required step is Luna reconciliation of the seam, followed by a revised V2 exact-byte sandbox authority using the new current tracked wrapper bytes.

## 16. Acceptance boundary

Preserve:

```text
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
NEW IMAGE REQUIRED = NO
```

## 17. Required output

Return:

- exact changed path;
- exact callback parameter/function name;
- exact checkpoint write now wrapped;
- proof unrelated writes are not wrapped;
- S2-T1 through S2-T5 results;
- PowerShell 5.1 parse/binding results;
- production-equivalence result;
- secret scan;
- full working-tree scope;
- mutation audit;
- readiness for Luna seam reconciliation.

## 18. Terminal markers

`RELEASE 1.12 WP04 — NARROW CHECKPOINT-PERSISTENCE SEAM IMPLEMENTATION: PASS`

`RELEASE 1.12 WP04 — SELECTED CHECKPOINT SEAM DESIGN: S2`

`RELEASE 1.12 WP04 — CORRECTED TRACKED PATH COUNT: 1`

`RELEASE 1.12 WP04 — HELPER CONTRACT MODIFIED: NO`

`RELEASE 1.12 WP04 — CHECKPOINT-PERSISTENCE CALLBACK SEAM: IMPLEMENTED`

`RELEASE 1.12 WP04 — CHECKPOINT-PERSISTENCE SEAM SCOPE: FINAL_CHECKPOINT_ONLY`

`RELEASE 1.12 WP04 — PRODUCTION DEFAULT CHECKPOINT WRITER: PRESERVED`

`RELEASE 1.12 WP04 — PRODUCTION CHECKPOINT PERSISTENCE EQUIVALENCE: PASS`

`RELEASE 1.12 WP04 — W7 ISOLATED CHECKPOINT FAILURE INJECTION: PASS`

`RELEASE 1.12 WP04 — UNRELATED FILESYSTEM WRITES AFFECTED: NO`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 PARSE VALIDATION: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 CALLBACK BINDING: PASS`

`RELEASE 1.12 WP04 — LOCAL SEAM VALIDATION S2-T1-T5: PASS`

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

`RELEASE 1.12 WP04 — NARROW CHECKPOINT-PERSISTENCE SEAM IMPLEMENTATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA CHECKPOINT-PERSISTENCE SEAM RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA NARROW CHECKPOINT-PERSISTENCE SEAM IMPLEMENTATION COMPLETE`

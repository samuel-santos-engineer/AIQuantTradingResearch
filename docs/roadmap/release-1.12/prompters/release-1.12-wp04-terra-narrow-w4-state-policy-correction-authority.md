# GPT-5.6 Terra — Release 1.12 WP04 Narrow W4 State-Policy Correction Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — owns the W4 truth-table contract, reconciliation, acceptance criteria, and publication decision.
- **GPT-5.6 Terra** — PRIMARY: correct the proven W4 defensive state-policy defect in exactly one tracked path and validate locally.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed finding

Read-only reconciliation proved a real policy defect in:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

The actual production policy function:

```text
Test-Wp04ArchiveExtractionState
```

currently accepts:

```text
RETAINED + NOT_APPLICABLE
```

with:

```text
Eligible = $true
```

This contradicts the governed fail-closed truth table.

Production reachability remains:

```text
R1_UNREACHABLE_DEFENSIVE
```

because the archive path naturally produces only:

```text
RETAINED + PASS
RETAINED + EMPTY
RETAINED + FAIL
RETRIEVAL_FAILED + NOT_APPLICABLE
```

Therefore the defect is defensive policy correctness, not evidence that the invalid state occurs naturally.

## 2. Exact authority

Authorize exactly:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Counts for this authority:

```text
CREATE = 0
MODIFY = 1
DELETE = 0
```

Do not modify:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

No other tracked path.

## 3. Required correction

Correct `Test-Wp04ArchiveExtractionState` so the governed state table is exactly:

```text
RETAINED + PASS            => ELIGIBLE
RETAINED + EMPTY           => ELIGIBLE
RETAINED + FAIL            => FAIL
RETAINED + NOT_APPLICABLE  => FAIL
RETRIEVAL_FAILED + NOT_APPLICABLE => ELIGIBLE
all other combinations     => FAIL / inconsistent
```

The function must remain fail-closed for unknown or inconsistent values.

## 4. Narrowness

Change only what is required to correct the state-policy defect and its directly associated local validation fixture(s) if those fixtures are embedded in the same authorized script.

Do not:
- add a W4 injection seam;
- refactor lifecycle orchestration;
- change archive retrieval/extraction mechanics;
- change S2 checkpoint seam behavior;
- change helper behavior;
- change RB2;
- change checkpoint/restoration ordering;
- change error precedence;
- change RestoreOnly semantics;
- change Azure behavior.

## 5. W4 direct policy validation

Under Windows PowerShell 5.1, invoke the **actual production policy function** `Test-Wp04ArchiveExtractionState`.

Required W4:

```text
ArchiveState = RETAINED
FreshExtraction = NOT_APPLICABLE
Eligible = false
```

No duplicated mock truth-table implementation may be used to determine the expected production result.

## 6. Complete state-table regression

Validate all governed combinations, including at minimum:

```text
RETAINED + PASS
RETAINED + EMPTY
RETAINED + FAIL
RETAINED + NOT_APPLICABLE
RETRIEVAL_FAILED + NOT_APPLICABLE
```

Also validate representative inconsistent/unknown combinations fail closed.

Required:

```text
ARCHIVE/EXTRACTION TRUTH TABLE = PASS
```

## 7. Carry-forward contracts

Reconfirm no regression to:

```text
PersistEvidenceCheckpointCallback isolation
production default checkpoint writer
Deferred helper behavior
LifecycleAction=None
RB2
pre-restoration checkpoint ordering
RestoreOnly exactly once policy
error precedence
null/absent restoration semantics
```

Local proof is sufficient; do not execute Azure.

## 8. PowerShell 5.1

Binding target:

```text
Windows PowerShell 5.1.26100.9444
```

Require:

```text
parser errors = 0
local state-policy validation = PASS
existing local validation = PASS
```

No PowerShell 7-only syntax/APIs.

## 9. Mutation audit

Before/after record working-tree state.

Expected full governed working tree remains exactly two modified tracked paths:

```text
initialize-qualification.ps1
verify-persistent-sqlite-webapp.ps1
```

This authority itself modifies only the first.

Require:

```text
unauthorized tracked paths = 0
staged paths = 0
git diff --check = PASS
```

## 10. Secret hygiene

No real secrets may be accessed, introduced, printed, or persisted.

Expected:

```text
SECRET HYGIENE = PASS
```

## 11. External mutation boundary

Forbidden:

```text
Azure
Docker/GHCR
GitHub issue/project/milestone
git add
git commit
git push
PR/merge
new diagnostic run
acceptance retry
reopen
Twelve Data configuration
```

## 12. Validation architecture after correction

Do not attempt W4 exact-byte process injection.

If this correction passes and Luna reconciles it, the intended later validation partition is:

```text
W1,W2,W3,W5,W6,W7,W8 -> exact-byte sandbox wrapper
W4 -> actual production Test-Wp04ArchiveExtractionState policy function
```

That later hybrid validation requires separate authority.

## 13. Publication boundary

Even if this correction passes:

```text
PUBLICATION READINESS = BLOCKED_PENDING_LUNA_RECONCILIATION_AND_HYBRID_VALIDATION
```

Do not publish.

## 14. Acceptance boundary

Preserve:

```text
NEW IMAGE REQUIRED = NO
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
RETAINED LOCAL EVIDENCE = PRESERVE
```

## 15. Required output

Return:
- exact changed logic;
- actual W4 policy-function result;
- complete state-table results;
- PowerShell 5.1 results;
- S2/lifecycle carry-forward result;
- secret hygiene;
- full tracked diff scope;
- exact mutation audit;
- readiness for Luna post-W4 correction reconciliation.

## 16. Terminal markers

`RELEASE 1.12 WP04 — NARROW W4 STATE-POLICY CORRECTION: PASS`

`RELEASE 1.12 WP04 — W4 PRODUCTION REACHABILITY: R1_UNREACHABLE_DEFENSIVE`

`RELEASE 1.12 WP04 — W4 POLICY DEFECT: CORRECTED`

`RELEASE 1.12 WP04 — RETAINED+NOT_APPLICABLE POLICY RESULT: FAIL`

`RELEASE 1.12 WP04 — ARCHIVE/EXTRACTION TRUTH TABLE: PASS`

`RELEASE 1.12 WP04 — W4 ACTUAL POLICY FUNCTION VALIDATION: PASS`

`RELEASE 1.12 WP04 — NEW W4 SOURCE SEAM ADDED: NO`

`RELEASE 1.12 WP04 — CHECKPOINT-PERSISTENCE S2 SEAM CARRY-FORWARD: PASS`

`RELEASE 1.12 WP04 — LIFECYCLE CONTRACT CARRY-FORWARD: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 PARSE VALIDATION: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 STATE-POLICY VALIDATION: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE VALIDATION: PASS`

`RELEASE 1.12 WP04 — CORRECTED TRACKED PATH COUNT: 1`

`RELEASE 1.12 WP04 — IMPLEMENTED TRACKED PATH COUNT: 2`

`RELEASE 1.12 WP04 — UNAUTHORIZED TRACKED PATH COUNT: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — PUBLICATION READINESS: BLOCKED_PENDING_LUNA_RECONCILIATION_AND_HYBRID_VALIDATION`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — NARROW W4 STATE-POLICY CORRECTION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA POST-W4 STATE-POLICY CORRECTION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA NARROW W4 STATE-POLICY CORRECTION COMPLETE`

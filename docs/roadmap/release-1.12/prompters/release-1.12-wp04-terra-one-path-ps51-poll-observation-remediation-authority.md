# GPT-5.6 Terra — Release 1.12 WP04 One-Path PowerShell 5.1 Poll-Observation Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — owns the reconciled defect contract, acceptance criteria, and post-implementation reconciliation.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the one-path Windows PowerShell 5.1 compatibility remediation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never replaces Luna/Terra.

## 1. Binding defect

The production wrapper function:

```text
Get-Wp04PollObservations
```

uses:

```powershell
return @($observations)
```

where:

```text
$observations = System.Collections.Generic.List[object]
```

Windows PowerShell 5.1 reproducibly throws:

```text
System.ArgumentException: Argument types do not match
```

The Luna-selected remediation is exactly:

```powershell
return $observations.ToArray()
```

## 2. Exact tracked authority

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

## 3. Exact implementation

Inside `Get-Wp04PollObservations`, replace only the incompatible collection return:

```powershell
return @($observations)
```

with:

```powershell
return $observations.ToArray()
```

Do not alter:
- observation construction;
- parsing;
- filtering;
- ordering;
- record shape;
- caller logic;
- archive/extraction handling;
- checkpoint logic;
- S2 callback seam;
- Deferred/RestoreOnly behavior;
- RB2;
- error precedence;
- lifecycle orchestration.

## 4. Windows PowerShell 5.1 validation

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Require parser validation:

```text
PARSER_ERRORS = 0
```

Directly validate the actual production `Get-Wp04PollObservations` function with:

```text
P1 — zero valid poll observations
P2 — exactly one valid poll observation
P3 — multiple valid poll observations
P4 — representative ignored/non-poll lines
P5 — mixed valid/ignored lines
```

For each require:
- no `ArgumentException`;
- expected count;
- expected ordering;
- expected record values;
- returned sequence is compatible with caller consumption.

For non-empty materialized results, confirm intended `System.Object[]` semantics. For empty-result PowerShell pipeline behavior, validate the caller-visible semantics rather than inventing a new contract.

## 5. Regression validation

Re-run local checks sufficient to prove no regression to:

```text
Test-Wp04ArchiveExtractionState truth table
W4 = RETAINED + NOT_APPLICABLE -> Eligible=False / EVIDENCE_STATE_INCONSISTENT
PersistEvidenceCheckpointCallback isolation
Deferred helper contract
LifecycleAction=None
RestoreOnly exactly-once policy
restoration-over-evidence precedence
RB2
```

W4 may carry forward; this check is regression protection, not re-governance.

## 6. PowerShell compatibility

Do not use PowerShell 7.

Do not introduce APIs/syntax unsupported by Windows PowerShell 5.1.

Required:

```text
WINDOWS POWERSHELL 5.1 POLL-OBSERVATION P1-P5 = PASS
```

## 7. Working-tree scope

Before and after, record tracked status.

Expected full governed modified tracked paths after implementation remain exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

This authority contributes exactly one narrow edit in the first path.

Require:

```text
implemented tracked path count = 2
unauthorized tracked path count = 0
staged path count = 0
git diff --check = PASS
```

## 8. Secret hygiene

No real secrets may be accessed, introduced, printed, or persisted.

Require:

```text
SECRET HYGIENE = PASS
```

## 9. External mutation boundary

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

## 10. Image decision

No image rebuild:

```text
NEW IMAGE REQUIRED = NO
```

This is a local deployment/qualification PowerShell wrapper correction.

## 11. Hybrid validation boundary

Do not run Hybrid Revised-V2 under this implementation authority.

If implementation passes, next authority is Luna post-remediation reconciliation.

After Luna acceptance, Hybrid Revised-V2 must restart at:

```text
W1
```

W1 prior attempt has no acceptance credit.

W2/W3/W5/W6/W7/W8 remain unproven.

W4 carry-forward remains authorized unless this edit unexpectedly affects its production policy function.

## 12. Publication boundary

Even after implementation passes:

```text
PUBLICATION BLOCKER = UNRESOLVED_PENDING_LUNA_RECONCILIATION_AND_HYBRID_VALIDATION
```

No publication.

## 13. Acceptance boundary

Preserve:

```text
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
RETAINED LOCAL EVIDENCE = PRESERVE
```

## 14. Required output

Return:
- exact one-line production change;
- P1–P5 results;
- actual return types/caller-visible semantics;
- parser result;
- W4/state-policy regression result;
- S2/lifecycle carry-forward;
- secret hygiene;
- full tracked diff scope;
- mutation audit;
- readiness for Luna reconciliation.

## 15. Terminal markers

`RELEASE 1.12 WP04 — ONE-PATH POWERSHELL 5.1 POLL-OBSERVATION REMEDIATION: PASS`

`RELEASE 1.12 WP04 — DEFECT FUNCTION: Get-Wp04PollObservations`

`RELEASE 1.12 WP04 — IMPLEMENTED PS5.1-SAFE RETURN FORM: return $observations.ToArray()`

`RELEASE 1.12 WP04 — POLL OBSERVATION P1: PASS`

`RELEASE 1.12 WP04 — POLL OBSERVATION P2: PASS`

`RELEASE 1.12 WP04 — POLL OBSERVATION P3: PASS`

`RELEASE 1.12 WP04 — POLL OBSERVATION P4: PASS`

`RELEASE 1.12 WP04 — POLL OBSERVATION P5: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 POLL-OBSERVATION VALIDATION: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 PARSE VALIDATION: PASS`

`RELEASE 1.12 WP04 — ARGUMENTEXCEPTION REPRODUCTION AFTER REMEDIATION: NOT_REPRODUCED`

`RELEASE 1.12 WP04 — W4 POLICY REGRESSION: NONE`

`RELEASE 1.12 WP04 — CHECKPOINT-PERSISTENCE S2 SEAM CARRY-FORWARD: PASS`

`RELEASE 1.12 WP04 — LIFECYCLE CONTRACT CARRY-FORWARD: PASS`

`RELEASE 1.12 WP04 — W4 CARRY-FORWARD: AUTHORIZED`

`RELEASE 1.12 WP04 — CORRECTED TRACKED PATH COUNT: 1`

`RELEASE 1.12 WP04 — IMPLEMENTED TRACKED PATH COUNT: 2`

`RELEASE 1.12 WP04 — UNAUTHORIZED TRACKED PATH COUNT: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE VALIDATION: PASS`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — HYBRID VALIDATION RESTART POINT: W1`

`RELEASE 1.12 WP04 — W1 PRIOR ACCEPTANCE CREDIT: NO`

`RELEASE 1.12 WP04 — W2/W3/W5/W6/W7/W8: NOT_PROVEN`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED_PENDING_LUNA_RECONCILIATION_AND_HYBRID_VALIDATION`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — ONE-PATH PS5.1 REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA POST-PS5.1 POLL-OBSERVATION REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA ONE-PATH POWERSHELL 5.1 POLL-OBSERVATION REMEDIATION COMPLETE`

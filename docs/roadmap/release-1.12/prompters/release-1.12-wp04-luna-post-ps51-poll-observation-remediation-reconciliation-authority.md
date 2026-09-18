# GPT-5.6 Luna — Release 1.12 WP04 Post-PowerShell-5.1 Poll-Observation Remediation Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: reconcile the completed PowerShell 5.1 remediation and decide whether Hybrid Revised-V2 may restart at W1.
- **GPT-5.6 Terra** — executes the later hybrid validation only after Luna authorization.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never replaces Luna/Terra.

## 1. Binding implementation evidence

The governed wrapper now contains the Luna-selected one-line compatibility remediation in:

```text
Get-Wp04PollObservations
```

Current return:

```powershell
return $observations.ToArray()
```

The prior Windows PowerShell 5.1 failure:

```text
System.ArgumentException: Argument types do not match
```

is no longer reproduced.

The implementation authority is complete. Do not duplicate the implementation.

## 2. Required reconciliation

Read-only verify that the implementation is exactly the governed narrow change and that its validation evidence is sufficient.

Required conclusions if supported:

```text
PS5.1 POLL-OBSERVATION DEFECT = REMEDIATED
SOURCE REMEDIATION SCOPE = ONE PATH
HELPER CHANGE = NONE
NEW IMAGE = NO
```

## 3. PowerShell 5.1 validation reconciliation

Confirm supplied P1-P5 validation covered:

```text
P1 zero valid poll observations
P2 one valid poll observation
P3 multiple valid poll observations
P4 ignored/non-poll lines
P5 mixed valid/ignored lines
```

Require:

```text
P1-P5 = PASS
parser = PASS
ArgumentException after remediation = NOT_REPRODUCED
ordering/record semantics = PRESERVED
```

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

## 4. Carry-forward reconciliation

Confirm no regression to:

```text
W4 actual policy behavior
PersistEvidenceCheckpointCallback S2 isolation
Deferred helper behavior
LifecycleAction=None
RB2
archive/extraction truth table
pre-restoration evidence checkpoint
RestoreOnly exactly-once policy
restoration-over-evidence error precedence
null/absent restoration semantics
```

W4 expected carry-forward:

```text
RETAINED + NOT_APPLICABLE
Eligible = False
Classification = EVIDENCE_STATE_INCONSISTENT
```

## 5. Working-tree reconciliation

Expected full governed modified tracked scope remains exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Require:

```text
implemented tracked path count = 2
unauthorized tracked path count = 0
staged path count = 0
git diff --check = PASS
```

Do not stage, commit, or publish.

## 6. Mutation accounting

This Luna reconciliation is read-only.

Carry forward implementation mutation truth:

```text
authorized remediation contribution = one edit in initialize-qualification.ps1
duplicate authority mutation = 0
```

Current reconciliation mutations:

```text
repository = 0
Azure = 0
Docker/GHCR = 0
Git/GitHub lifecycle = 0
```

## 7. Hybrid validation restart decision

If remediation reconciliation passes, authorize the next validation to restart at:

```text
W1
```

Do not carry acceptance credit from the failed pre-remediation W1 attempt.

Preserve:

```text
W1 prior acceptance credit = NO
W2/W3/W5/W6/W7/W8 = NOT_PROVEN
```

W4 may carry forward as already proven because the poll-observation return edit does not alter `Test-Wp04ArchiveExtractionState`, provided read-only diff inspection confirms no W4 change.

## 8. Next Hybrid Revised-V2 contract

If D1 is selected, the next Terra authority must use the previously reconciled hybrid partition:

```text
W1,W2,W3,W5,W6,W7,W8
    -> exact-byte sandbox execution of current post-remediation wrapper

W4
    -> carry-forward/direct actual production policy validation
```

The exact-byte hash must be taken from the **current post-remediation wrapper bytes**, not an earlier wrapper version.

No source change is authorized during hybrid validation.

## 9. Publication boundary

Even if this reconciliation passes:

```text
PUBLICATION BLOCKER = UNRESOLVED_PENDING_HYBRID_VALIDATION
```

Do not publish.

## 10. Acceptance boundary

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

## 11. Decision

Select exactly one:

### D1 — REMEDIATION ACCEPTED; HYBRID RESTART READY

Next:

```text
TERRA POST-REMEDIATION HYBRID REVISED-V2 VALIDATION AUTHORITY
```

### D2 — REMEDIATION EVIDENCE INCOMPLETE

Next:

```text
TERRA NARROW PS5.1 REMEDIATION VALIDATION CORRECTION AUTHORITY
```

### D3 — REMEDIATION CONTRACT VIOLATED

Next:

```text
LUNA PS5.1 REMEDIATION RE-GOVERNANCE AUTHORITY
```

## 12. Required output

Return:
- exact reconciled source change;
- P1-P5 status;
- parser/ArgumentException status;
- caller semantics status;
- W4/S2/lifecycle carry-forward;
- working-tree scope;
- mutation audit;
- W4 carry-forward decision;
- D1/D2/D3;
- exact next authority.

## 13. Terminal markers

`RELEASE 1.12 WP04 — POST-PS5.1 POLL-OBSERVATION REMEDIATION RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — PS5.1 POLL-OBSERVATION DEFECT: REMEDIATED`

`RELEASE 1.12 WP04 — RECONCILED PS5.1-SAFE RETURN FORM: return $observations.ToArray()`

`RELEASE 1.12 WP04 — POLL OBSERVATION P1-P5: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 PARSE VALIDATION: PASS`

`RELEASE 1.12 WP04 — ARGUMENTEXCEPTION AFTER REMEDIATION: NOT_REPRODUCED`

`RELEASE 1.12 WP04 — POLL-OBSERVATION ORDERING/RECORD SEMANTICS: PRESERVED`

`RELEASE 1.12 WP04 — W4 POLICY REGRESSION: NONE`

`RELEASE 1.12 WP04 — CHECKPOINT-PERSISTENCE S2 SEAM CARRY-FORWARD: PASS`

`RELEASE 1.12 WP04 — LIFECYCLE CONTRACT CARRY-FORWARD: PASS`

`RELEASE 1.12 WP04 — W4 CARRY-FORWARD: <AUTHORIZED|REVALIDATE>`

`RELEASE 1.12 WP04 — IMPLEMENTED TRACKED PATH COUNT: 2`

`RELEASE 1.12 WP04 — UNAUTHORIZED TRACKED PATH COUNT: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — DUPLICATE AUTHORITY ADDITIONAL MUTATIONS: 0`

`RELEASE 1.12 WP04 — RECONCILIATION REPOSITORY MUTATIONS: 0`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — HYBRID VALIDATION RESTART POINT: W1`

`RELEASE 1.12 WP04 — W1 PRIOR ACCEPTANCE CREDIT: NO`

`RELEASE 1.12 WP04 — W2/W3/W5/W6/W7/W8: NOT_PROVEN`

`RELEASE 1.12 WP04 — HYBRID RESTART READINESS: <READY|BLOCKED>`

`RELEASE 1.12 WP04 — RECONCILIATION DECISION: <D1|D2|D3>`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED_PENDING_HYBRID_VALIDATION`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — POST-PS5.1 REMEDIATION RECONCILIATION MUTATION AUDIT: PASS`

Then exactly one:

`RELEASE 1.12 WP04 — TERRA POST-REMEDIATION HYBRID REVISED-V2 VALIDATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA NARROW PS5.1 REMEDIATION VALIDATION CORRECTION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA PS5.1 REMEDIATION RE-GOVERNANCE AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA POST-PS5.1 POLL-OBSERVATION REMEDIATION RECONCILIATION COMPLETE`

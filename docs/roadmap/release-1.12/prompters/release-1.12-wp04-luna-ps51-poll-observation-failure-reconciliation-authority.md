# GPT-5.6 Luna — Release 1.12 WP04 PowerShell 5.1 Poll-Observation Failure Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: reconcile the newly proven Windows PowerShell 5.1 production-source failure and define the narrow remediation contract.
- **GPT-5.6 Terra** — may implement only the remediation explicitly authorized by this reconciliation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never replaces Luna/Terra.

## 1. Binding evidence

Hybrid Revised-V2 stopped at W1 before archive/checkpoint handling.

Exact-byte sandbox and interception preflight passed:

```text
EXACT-BYTE REACHABLE-SCENARIO WRAPPER IDENTITY = PASS
EXTERNAL OPERATION INTERCEPTION = PASS
UNEXPECTED REAL EXTERNAL INVOCATIONS = 0
```

The actual current wrapper fails under Windows PowerShell 5.1 in:

```text
Get-Wp04PollObservations
return @($observations)
```

with:

```text
System.ArgumentException: Argument types do not match
```

Direct Windows PowerShell 5.1 invocation reproduced the same failure with valid poll lines.

Therefore this is no longer validation-only.

## 2. Required classification

Reconcile as:

```text
FUNCTIONAL PRODUCTION DEFECT PROVEN = YES
DEFECT CLASS = WINDOWS_POWERSHELL_5_1_COLLECTION_RETURN_COMPATIBILITY
DEFECT LOCATION = Get-Wp04PollObservations
```

The failure prevents W1 from reaching archive retrieval/checkpoint handling.

W2/W3/W5/W6/W7/W8 remain unexecuted and unproven.

## 3. Root-cause review

Read-only inspect:

```text
Get-Wp04PollObservations
$observations construction/type
Add/append operations
return @($observations)
all call sites
expected consumer shape
```

Determine the narrowest PowerShell 5.1-safe return form that preserves the intended caller-visible sequence.

Do not infer PowerShell 7 behavior as authority.

## 4. Remediation constraints

Preferred remediation must:

```text
modify only initialize-qualification.ps1
preserve observation ordering
preserve observation values/records
preserve empty/single/multiple observation behavior
preserve caller-visible collection semantics required by existing callers
work under Windows PowerShell 5.1.26100.9444
```

Do not alter poll parsing semantics unless required by the proven defect.

Do not refactor lifecycle orchestration.

Do not modify the helper.

Do not change S2 checkpoint seam.

## 5. Candidate remediation

Luna must identify the exact PowerShell 5.1-safe replacement after source inspection.

Potential forms may include a deliberate typed/untyped array materialization or enumerator conversion, but **do not authorize a guessed syntax** without confirming the actual `$observations` type and caller contract.

Select one exact implementation form.

## 6. Required local remediation validation contract

Terra's later implementation must validate the actual production function under Windows PowerShell 5.1 for:

```text
P1 = zero valid poll observations
P2 = exactly one valid poll observation
P3 = multiple valid poll observations
P4 = representative ignored/non-poll lines
P5 = mixed valid/ignored lines
```

Required:

```text
no ArgumentException
correct count
correct ordering
correct record values
```

Also run parser validation and existing local lifecycle/state-policy checks.

## 7. Hybrid-validation consequence

The failed W1 run grants no W1 acceptance credit.

After remediation and Luna reconciliation, the full hybrid validation must restart from:

```text
W1
```

Do not resume at W2.

W4 direct production-policy validation may be carried forward only if the remediation cannot affect `Test-Wp04ArchiveExtractionState`; Luna must explicitly decide this later.

## 8. Tracked scope

Current governed modified tracked paths remain:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Expected remediation authority:

```text
MODIFY initialize-qualification.ps1
```

only.

No helper modification.

## 9. Image decision

This defect is in the deployment/qualification PowerShell wrapper, not the deployed container runtime.

Expected:

```text
NEW IMAGE REQUIRED = NO
```

Confirm read-only.

## 10. Mutation boundary

This Luna authority is zero-mutation:

```text
repository edits = 0
staging/commit/push = 0
PR/merge = 0
Azure = 0
Docker/GHCR = 0
GitHub lifecycle = 0
```

## 11. Acceptance/publication boundary

Preserve:

```text
PUBLICATION BLOCKER = UNRESOLVED
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
RETAINED LOCAL EVIDENCE = PRESERVE
```

## 12. Decision

Select exactly one:

### D1 — NARROW ONE-PATH PS5.1 REMEDIATION

Expected next authority:

```text
TERRA ONE-PATH POWERSHELL 5.1 POLL-OBSERVATION REMEDIATION AUTHORITY
```

### D2 — BROADER SOURCE CONTRACT ISSUE

Only if source inspection proves callers require a broader redesign.

Next:

```text
LUNA POLL-OBSERVATION CONTRACT RE-GOVERNANCE AUTHORITY
```

### D3 — FAILURE NOT ATTRIBUTABLE TO PRODUCTION FUNCTION

Only if direct reproduction evidence is invalidated.

Next:

```text
LUNA ADDITIONAL READ-ONLY POLL-OBSERVATION INVESTIGATION AUTHORITY
```

## 13. Required output

Return:
- exact `$observations` runtime/source type;
- exact cause of the PS5.1 `ArgumentException`;
- caller-visible required return contract;
- selected narrow replacement;
- P1–P5 validation contract;
- whether W4 can carry forward;
- image decision;
- D1/D2/D3;
- exact next authority;
- zero-mutation audit.

## 14. Terminal markers

`RELEASE 1.12 WP04 — POWERSHELL 5.1 POLL-OBSERVATION FAILURE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — FUNCTIONAL PRODUCTION DEFECT PROVEN: YES`

`RELEASE 1.12 WP04 — DEFECT CLASS: WINDOWS_POWERSHELL_5_1_COLLECTION_RETURN_COMPATIBILITY`

`RELEASE 1.12 WP04 — DEFECT FUNCTION: Get-Wp04PollObservations`

`RELEASE 1.12 WP04 — POLL-OBSERVATION ROOT CAUSE: <value>`

`RELEASE 1.12 WP04 — SELECTED PS5.1-SAFE RETURN FORM: <value>`

`RELEASE 1.12 WP04 — TRACKED SOURCE CHANGE COUNT IF IMPLEMENTED: 1`

`RELEASE 1.12 WP04 — HELPER MODIFICATION REQUIRED: NO`

`RELEASE 1.12 WP04 — HYBRID VALIDATION RESTART POINT: W1`

`RELEASE 1.12 WP04 — W1 ACCEPTANCE CREDIT: NO`

`RELEASE 1.12 WP04 — W2/W3/W5/W6/W7/W8: NOT_PROVEN`

`RELEASE 1.12 WP04 — W4 CARRY-FORWARD: <AUTHORIZED|REVALIDATE>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — RECONCILIATION DECISION: <D1|D2|D3>`

`RELEASE 1.12 WP04 — TRACKED SOURCE MUTATIONS: 0`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — POLL-OBSERVATION FAILURE RECONCILIATION MUTATION AUDIT: PASS`

Then exactly one:

`RELEASE 1.12 WP04 — TERRA ONE-PATH POWERSHELL 5.1 POLL-OBSERVATION REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA POLL-OBSERVATION CONTRACT RE-GOVERNANCE AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA ADDITIONAL READ-ONLY POLL-OBSERVATION INVESTIGATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA POWERSHELL 5.1 POLL-OBSERVATION FAILURE RECONCILIATION COMPLETE`

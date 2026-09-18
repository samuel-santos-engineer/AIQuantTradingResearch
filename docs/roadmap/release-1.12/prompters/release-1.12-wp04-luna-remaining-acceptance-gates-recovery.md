# Release 1.12 WP04 — Luna Remaining Acceptance Gates Recovery

**Selected execution model: GPT-5.6 Luna**

## Mission

Perform a single exhaustive **read-only evidence-recovery pass** for the remaining WP04 acceptance classes after final acceptance of hybrid W1–W8.

The previous reconciliation established:

```text
W5 fresh reconciliation=PASS
W6/W7/W8 evidence preserved
HybridW1W8Result=ACCEPTED
WP04FullAcceptanceEvidenceState=NOT_READY
PublicationAuthorizationState=NOT_AUTHORIZED
```

The remaining problem is no longer hybrid validation.

Determine exactly which substantive WP04 acceptance classes are:
1. already proven by retained evidence but were not located/reconciled;
2. genuinely unproven and require a new governed execution;
3. publication/lifecycle-only and therefore not substantive runtime blockers.

Do not mutate anything.

---

## 1. Model roles

```text
GPT-5.6 Luna:
contract recovery, evidence reconciliation, acceptance classification,
governance sequencing, read-only analysis.

GPT-5.6 Terra:
later implementation/validation/external mutation only under a separate
explicit authority.

GPT-5.6 Sol:
supporting analysis only; never substitutes for Luna/Terra authority.
```

Selected model for this authority: **GPT-5.6 Luna**.

---

## 2. Accepted boundaries — do not reopen

Treat these as accepted unless retained evidence proves actual corruption:

```text
W1-W8 hybrid validation=ACCEPTED
Fresh W5 RunId=initialize-23b2c577ce474c48aae995bae1d77f41
Fresh W5 root=
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\w5\8d47708d095f407dbd15c32885b16c63

W6=PASS
W7=PASS
W8=PASS
```

Do not rerun W5-W8.

Do not revisit already-settled architecture unless an actual contradiction is found.

---

## 3. Canonical WP04 acceptance contract

Recover the exact WP04 acceptance contract from retained Release 1.12/WP04 authorities, issue text, implementation plan, and accepted Luna decisions.

At minimum reconcile these classes:

```text
PersistentInitialization
GovernedDataUpdate
FidelityIdempotencyConflictSemantics
RestartRedeployPersistence
Recovery
Integrity
SchemaV4
DeleteJournal
EvidenceReuse
SecretHygiene
ZeroCostNoBypass
RequiredValidations
PRMergePostMerge
IssueProjectLifecycle
```

Also recover any canonical acceptance class omitted from this list.

Do not silently add new acceptance requirements that were never part of WP04 governance.

Print:

```text
CanonicalWP04AcceptanceClasses
CanonicalAcceptanceSources
```

---

## 4. Search retained evidence exhaustively

Search the repository-local and durable WP04 evidence corpus before classifying any substantive class as unproven.

Include, where available:

```text
WP04 planning/authority Markdown
Luna decisions
Terra execution reports
D3 qualification records
local Docker qualification evidence
HTTP qualification evidence
Azure diagnostic evidence
H1/S2 evidence
W1-W8 durable roots
wrapper/helper evidence
cost/no-bypass evidence
schema/journal/integrity evidence
restart/redeploy/recovery evidence
Git history/local branch state
previous PR/publication planning artifacts
```

Important evidence distinctions:

```text
historical D3 02:02 record = candidate capability only
diagnostic RunId initialize-53207... = diagnostic, no acceptance credit
normal-root 503 missing TwelveData key = expected downstream WP05 blocker
Twelve Data secret configuration = forbidden in WP04
```

Do not grant acceptance credit to evidence explicitly classified as diagnostic/candidate-only.

---

## 5. Acceptance matrix

For every canonical WP04 acceptance class return exactly:

```text
AcceptanceClass
CanonicalRequirement
State=PASS|FAIL|NOT_PROVEN|PENDING_GOVERNANCE
EvidenceReference
EvidenceCreditClass=ACCEPTANCE|CARRY_FORWARD|DIAGNOSTIC_ONLY|CANDIDATE_ONLY|NONE
MissingProof
NextOwner=LUNA|TERRA|GITHUB_LIFECYCLE|NONE
```

### Classification rules

Use `PASS` only when retained acceptance-grade evidence directly proves the canonical requirement.

Use `NOT_PROVEN` when evidence is absent, diagnostic-only, candidate-only, ambiguous, or cannot be attributed to the required target/run.

Use `PENDING_GOVERNANCE` only for actions that are intentionally impossible before final acceptance/publication, such as PR/merge/post-merge or issue/project lifecycle.

Do not classify publication/lifecycle as a missing substantive runtime proof.

---

## 6. Restart/redeploy persistence

This class requires special care.

Recover the exact canonical meaning of:

```text
restart persistence
redeploy persistence
continuity identity
target attribution
```

Determine whether retained acceptance-grade evidence already proves it.

Do not use:
- local container restart alone as Azure restart/redeploy proof if the contract requires Azure;
- historical candidate-only D3;
- diagnostic D3 without acceptance credit;
- hybrid W1-W8 synthetic evidence.

If genuinely unproven, specify the **minimum exact new governed target execution** required.

Return:

```text
RestartRedeployPersistenceState
RestartRedeployMissingProof
MinimumGovernedExecution
RequiresAzure=true|false
RequiresFreshRunId=true|false
RequiresImageChange=true|false
RequiresProductionSourceChange=true|false
```

---

## 7. Recovery

Recover the exact canonical recovery requirement.

Distinguish:

```text
SQLite/application recovery semantics
evidence retrieval recovery
deployment restart/redeploy continuity
configuration restoration
```

Do not merge these into one vague gate.

If recovery remains unproven, specify the smallest exact proof required and whether it can be combined with restart/redeploy persistence in one Terra authority.

Return:

```text
RecoveryState
RecoveryMissingProof
CanCombineWithRestartRedeploy=true|false
```

---

## 8. Persistent initialization / data update / fidelity

Reconcile whether existing accepted evidence proves:

```text
application-owned initialization
configured /home/data/aiquant.db identity
schema v4
DELETE journal
integrity/quick-check
governed data update
accepted evidence identity/count
idempotency/conflict/fidelity semantics
reopen continuity
```

Do not substitute direct SQLite shell/Python evidence.

Application-owned D3/qualification evidence is the expected proof surface.

---

## 9. Zero-cost / no-bypass / secret hygiene

Reconcile accepted Initiative-1.11 and WP04 evidence without overclaiming.

Require consistency with:

```text
Azure App Service Linux F1
West Central US
custom Docker
public/free GHCR
persistent /home
SQLite DELETE
strict $0 reference/demo
no Azure SQL
no Azure Files
no mandatory ACR
no paid service
no direct SQL deployment bypass
no Twelve Data secret configured in WP04
```

Normal root 503 due missing TwelveData key is not a WP04 failure.

---

## 10. Required validations

Recover the canonical validation inventory.

Distinguish:

```text
already accepted local validations
hybrid W1-W8 accepted validation
target Azure acceptance validation
publication/post-merge validation
```

Do not mark post-merge validation as a pre-publication substantive blocker if governance explicitly sequences it after merge.

---

## 11. External governance verification

The previous Luna session lacked safe read-only GitHub project tooling.

For this authority:

- If safe read-only GitHub/project inspection is available, verify current #263, Project #2 Status, and milestone #63 without mutation.
- If unavailable, return `NOT_PROVEN` and classify the check as a governance verification requirement, not a runtime defect.
- Do not mutate GitHub.

Expected pre-publication state remains:

```text
#263 OPEN
milestone #63 OPEN
WP05 NOT_STARTED
```

Project #2 status must be reported from evidence, not guessed.

---

## 12. Consolidate remaining substantive executions

If more than one substantive class is genuinely `NOT_PROVEN`, determine whether they can safely be proven in **one** Terra governed execution.

Optimize for:

```text
one authority per governance boundary
not one authority per defect
```

Prefer a combined target-validation authority when restart/redeploy persistence, recovery, integrity, evidence reuse, or related target checks share the same governed Azure lifecycle.

Do not split them unnecessarily.

Return:

```text
RemainingSubstantiveAcceptanceGates
CanConsolidateIntoOneTerraAuthority=true|false
ProposedCombinedExecutionScope
RequiredFreshRunIds
RequiredExternalSurfaces
ExpectedMutations
ForbiddenMutations
```

---

## 13. Final sequencing state

Select exactly one:

### STATE A — substantive target evidence remains

```text
WP04FullAcceptanceEvidenceState=NOT_READY
PublicationAuthorizationState=NOT_AUTHORIZED
NextAuthorizedAction=GPT-5.6 Terra combined remaining WP04 acceptance validation
```

List the exact substantive gates.

### STATE B — substantive evidence complete; only publication/lifecycle remains

```text
WP04FullAcceptanceEvidenceState=READY_FOR_FINAL_ACCEPTANCE_AUTHORITY
PublicationAuthorizationState=REQUIRES_SEPARATE_TERRA_AUTHORITY
NextAuthorizedAction=GPT-5.6 Terra WP04 final validation/publication/lifecycle authority
```

### STATE C — architecture/policy ambiguity discovered

```text
WP04FullAcceptanceEvidenceState=BLOCKED_BY_POLICY_DECISION
PublicationAuthorizationState=NOT_AUTHORIZED
NextAuthorizedAction=GPT-5.6 Luna narrow policy decision
```

Do not perform the next action.

---

## 14. Required terminal markers

Always emit:

```text
RELEASE 1.12 WP04 — HYBRID W1-W8 VALIDATION: ACCEPTED
RELEASE 1.12 WP04 — REMAINING ACCEPTANCE RECOVERY: COMPLETE
RELEASE 1.12 WP04 — PUBLICATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — WP05: NOT_STARTED
```

Then emit one truthful state:

```text
RELEASE 1.12 WP04 — SUBSTANTIVE ACCEPTANCE EVIDENCE: COMPLETE
```

or

```text
RELEASE 1.12 WP04 — SUBSTANTIVE ACCEPTANCE EVIDENCE: INCOMPLETE
```

If incomplete:

```text
RELEASE 1.12 WP04 — REMAINING SUBSTANTIVE GATES: <exact list>
```

If only lifecycle remains:

```text
RELEASE 1.12 WP04 — FINAL ACCEPTANCE/PUBLICATION AUTHORITY: READY_NEXT
```

No mutations.

---

## 15. Required handoff

Return:

```text
CanonicalWP04AcceptanceClasses
CanonicalAcceptanceSources
WP04AcceptanceClassMatrix

PersistentInitializationState
GovernedDataUpdateState
FidelityIdempotencyConflictSemanticsState
RestartRedeployPersistenceState
RecoveryState
IntegrityState
SchemaV4State
DeleteJournalState
EvidenceReuseState
SecretHygieneState
ZeroCostNoBypassState
RequiredValidationsState
PRMergePostMergeState
IssueProjectLifecycleState

RestartRedeployMissingProof
MinimumGovernedExecution
RequiresAzure
RequiresFreshRunId
RequiresImageChange
RequiresProductionSourceChange

RecoveryMissingProof
CanCombineWithRestartRedeploy

RemainingSubstantiveAcceptanceGates
CanConsolidateIntoOneTerraAuthority
ProposedCombinedExecutionScope
RequiredFreshRunIds
RequiredExternalSurfaces
ExpectedMutations
ForbiddenMutations

WP04IssueState
Project2WP04Status
Milestone63State
WP05State

WP04FullAcceptanceEvidenceState
PublicationAuthorizationState
BoundaryBlocker
NextAuthorizedAction
```

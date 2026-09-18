# Release 1.12 WP04 — Luna Canonical WP04/WP07 Ownership Policy Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Mission

Resolve one narrow canonical planning contradiction before any further WP04 target execution.

No implementation. No repository mutation. No Azure. No RunId allocation. No GitHub mutation. No publication.

The contradiction is:

```text
RELEASE_1.12_DEFINITION.md
  includes /home SQLite initialization/update/recovery
  and restart/recycle/redeploy recovery in Release 1.12 scope/acceptance

RELEASE_1.12_EXECUTION_PLAN.md
  WP04 = Persistent SQLite Initialization, Data Update & Recovery
  WP07 = Deployment Stability, Recovery, restarts, recycles, redeployments
  persistence acceptance area also mentions restart/recycle/redeploy recovery
```

Prior Luna attempted:

```text
WP04 application/SQLite recovery
WP07 deployment recovery
```

but correctly refused to apply it because canonical wording is not sufficiently unambiguous.

This authority must establish the binding ownership rule from the planning hierarchy and dependency sequence, without silently rewriting either document.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract/policy/architecture/governance/acceptance reconciliation

GPT-5.6 Terra
  implementation, governed validation, approved mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Luna**.

---

## 2. Fixed facts

Preserve:

```text
Hybrid W1-W8=ACCEPTED
WP04 target-validation contract discovery found no canonical redeploy operation
WP04 helper LifecycleAction=Restart|None
WP04 qualification lifecycle=initialize/reopen + H1 restoration
no new update/conflict product semantics are needed
no source/image change is currently authorized
WP07 is not started
WP05 is not started
publication is not authorized
```

No RunId was consumed by the blocked Terra attempt.

---

## 3. Canonical sources

Read the complete relevant sections, not isolated lines, from:

```text
docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md
docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md
docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md
WP04 definition/issue text
WP07 definition/issue text
Release 1.12 dependency/acceptance sequencing
any earlier Luna release-definition authority that created these planning artifacts
```

Report exact source references for every conclusion.

Do not infer issue content if unavailable.

---

## 4. Separate release-level scope from WP ownership

Explicitly answer:

```text
A. Is restart/recycle/redeploy persistence required somewhere in Release 1.12?
B. Which WP is assigned to execute/prove it?
C. Is it a prerequisite for closing WP04?
D. Is it instead a later Release 1.12 acceptance prerequisite owned by WP07?
E. What does “Recovery” in the WP04 title mean when read together with WP07?
```

Do not treat:

```text
Release 1.12 acceptance requirement
```

as automatically equivalent to:

```text
WP04 closure requirement
```

unless the canonical documents explicitly require that.

Likewise, do not remove restart/redeploy recovery from Release 1.12 merely because WP07 owns execution.

---

## 5. Apply dependency logic

Canonical sequence:

```text
WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08
```

Use this sequencing as evidence.

Test both interpretations:

### Interpretation 1

```text
WP04 must prove Azure restart/recycle/redeploy before closure
```

Evaluate whether that would make WP07's explicitly assigned deployment-stability/recovery work duplicative or prematurely completed.

### Interpretation 2

```text
WP04 proves application-owned persistence/update/reopen/restoration;
WP07 later proves deployment lifecycle persistence/recovery for Release 1.12.
```

Evaluate whether this preserves both the release-level acceptance requirement and the WP07 assignment.

Do not select based merely on convenience. Select the interpretation best supported by the full canonical planning set.

---

## 6. Resolve the line-31 persistence acceptance wording

The execution plan reportedly lists restart/recycle/redeploy recovery under a “persistence acceptance area.”

Determine what that section represents:

```text
WP04-specific exit criteria
release-wide acceptance coverage
cross-WP capability acceptance
WP07 criteria
ambiguous wording
```

This is critical.

Quote/paraphrase only enough context to classify its governance role.

Return:

```text
PersistenceAcceptanceAreaClassification
PersistenceAcceptanceAreaOwner
PersistenceAcceptanceAreaClosureEffectOnWP04
```

---

## 7. Resolve WP04 “Recovery”

Define the narrowest source-supported canonical meaning of WP04 `Recovery`.

Choose only from evidence-supported semantics, such as:

```text
application-owned reopen/readback recovery
SQLite initialization/update recovery
temporary qualification-setting restoration
deployment lifecycle recovery
combination
```

Return:

```text
WP04RecoveryDefinition
WP04RecoveryRequiredPredicates
```

Do not invent corruption repair.

---

## 8. Resolve WP07 deployment recovery

Define:

```text
WP07DeploymentRecoveryDefinition
WP07DeferredAcceptanceGates
WP07ReleaseAcceptanceEffect
```

Expected candidate gates include, only if source-supported:

```text
restart persistence
recycle persistence
redeploy persistence
deployment-level recovery
```

WP07 must not be executed now.

---

## 9. Governed data/fidelity/idempotency/conflict

Preserve the prior source-grounded finding unless canonical planning contradicts it:

```text
GovernedDataUpdate=EXISTING_SEMANTICS
Fidelity=EXISTING_SEMANTICS
Idempotency=EXISTING_SEMANTICS
Conflict=EXISTING_SEMANTICS
```

Existing application/domain sources:

```text
PersistentSqliteQualificationExecution.cs
PersistHistoricalObservationsUseCase.cs
PersistHistoricalObservationsUseCaseTests.cs
PersistenceContractTests.cs
ExperimentPersistenceTests.cs
```

Determine whether WP04 needs a fresh Azure target run to close these predicates or whether accepted existing qualification evidence plus contract tests already supplies acceptance-grade proof.

Return for each:

```text
Gate
EvidenceClass=TARGET_RUNTIME|CARRY_FORWARD_TESTS|TARGET_PLUS_CARRY_FORWARD
FreshTargetExecutionRequired=true|false
ExactRequiredEvidence
```

---

## 10. Decision — choose exactly one

### POLICY A — WP04 application persistence; WP07 deployment lifecycle

Use only if canonical evidence supports:

```text
Release 1.12 still requires restart/recycle/redeploy persistence.
WP07 owns execution/proof of those deployment lifecycle gates.
They are NOT prerequisites for WP04 closure.
WP04 “Recovery” is limited to its application/SQLite/qualification restoration contract.
```

Markers:

```text
CANONICAL_OWNERSHIP_DECISION=WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
WP04_DEPLOYMENT_RECOVERY_REQUIRED_FOR_CLOSURE=false
WP07_DEPLOYMENT_RECOVERY_REQUIRED_FOR_RELEASE_ACCEPTANCE=true
```

### POLICY B — WP04 must also prove deployment recovery

Use only if canonical evidence explicitly makes restart/recycle/redeploy a WP04 exit criterion despite WP07's later assignment.

Markers:

```text
CANONICAL_OWNERSHIP_DECISION=WP04_INCLUDES_DEPLOYMENT_RECOVERY
WP04_DEPLOYMENT_RECOVERY_REQUIRED_FOR_CLOSURE=true
```

Then explain the non-duplicative remaining role of WP07. If no coherent non-duplicative role exists, that is evidence against Policy B but not dispositive by itself.

### POLICY C — canonical documents genuinely cannot resolve ownership

Use only if full-context evidence remains irreducibly ambiguous:

```text
CANONICAL_OWNERSHIP_DECISION=PLANNING_AMENDMENT_REQUIRED
```

Define the minimum planning amendment required and exact replacement wording. Do not mutate files.

---

## 11. Determine whether a planning amendment is needed

If Policy A or B can be derived by reconciliation without changing the intended roadmap:

```text
PlanningAmendmentRequired=false
RoadmapArchitectureChange=false
```

If Policy C:

```text
PlanningAmendmentRequired=true
```

Specify exact files/lines/concepts requiring correction.

---

## 12. Next WP04 executable contract

If Policy A:

Define exact remaining WP04 predicates after excluding WP07 deployment lifecycle gates.

Start from:

```text
D01 initialize invokes application-owned persistence use case
D02 canonical observation accepted/idempotently recognized
D03 canonical observation read back unchanged
D04 existing conflict semantics non-overwriting
D05 /home/data/aiquant.db
D06 schema v4
D07 DELETE
D08 integrity ok
D09 quick-check ok
D10 evidence identity/count correct
D11 HTTP evidence exact RunId
D12 checkpoint before RestoreOnly
D13 RestoreOnly exactly once
D14 temporary settings restored
D15 token not persisted/disclosed
D16 no direct bypass
D17 F1/West Central US/$0 unchanged
D18 mutation boundaries clean
D19 durable evidence reopen/references
```

Classify every D01-D19 as:

```text
ALREADY_ACCEPTED
FRESH_TARGET_REQUIRED
CARRY_FORWARD_REQUIRED
```

If no fresh target execution is actually required, say so; do not authorize redundant Azure activity.

If Policy B, define the additional deployment lifecycle predicates mechanically and identify whether existing helper/source support is insufficient.

---

## 13. Final sequencing

Choose the narrowest truthful next action.

Possible outputs:

```text
GPT-5.6 Terra existing-contract WP04 target validation
GPT-5.6 Luna final WP04 acceptance reconciliation
GPT-5.6 Terra planning clarification implementation
GPT-5.6 Luna define missing deployment mechanics
```

Do not authorize publication/lifecycle yet unless the evidence proves the workflow has reached that boundary; this authority itself performs no mutation.

---

## 14. Required terminal markers

Always:

```text
RELEASE 1.12 WP04 — CANONICAL OWNERSHIP RECONCILIATION: COMPLETE
RELEASE 1.12 WP04 — RELEASE-LEVEL DEPLOYMENT RECOVERY REQUIREMENT: PRESERVED
RELEASE 1.12 WP04 — AZURE MUTATIONS: 0
RELEASE 1.12 WP04 — RUNIDS ALLOCATED: 0
RELEASE 1.12 WP04 — PUBLICATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — WP05: NOT_STARTED
RELEASE 1.12 WP04 — WP07: NOT_STARTED
```

Then emit the exact Policy A/B/C marker.

---

## 15. Required handoff

Return:

```text
CanonicalSources
ReleaseLevelRestartRecycleRedeployRequired

PersistenceAcceptanceAreaClassification
PersistenceAcceptanceAreaOwner
PersistenceAcceptanceAreaClosureEffectOnWP04

WP04RecoveryDefinition
WP04RecoveryRequiredPredicates

WP07DeploymentRecoveryDefinition
WP07DeferredAcceptanceGates
WP07ReleaseAcceptanceEffect

CanonicalOwnershipDecision
WP04DeploymentRecoveryRequiredForClosure
WP07DeploymentRecoveryRequiredForReleaseAcceptance

GovernedDataUpdateEvidenceClass
GovernedDataUpdateFreshTargetExecutionRequired
FidelityEvidenceClass
FidelityFreshTargetExecutionRequired
IdempotencyEvidenceClass
IdempotencyFreshTargetExecutionRequired
ConflictEvidenceClass
ConflictFreshTargetExecutionRequired

D01D19ClassificationMatrix
RemainingWP04AcceptanceGates

PlanningAmendmentRequired
PlanningAmendmentScope
RoadmapArchitectureChange

BoundaryBlocker
FinalDecision
NextAuthorizedAction
```

No mutations.

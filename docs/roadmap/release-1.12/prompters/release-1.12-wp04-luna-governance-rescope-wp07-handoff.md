# Release 1.12 WP04 — Luna Governance Rescope + WP07 Handoff

**Selected execution model: GPT-5.6 Luna**

## Mission

Apply the accepted **governance/acceptance interpretation** from the target-validation contract decision, read-only unless the project has a canonical local planning artifact whose update is explicitly part of this Luna governance step.

The decision is binding:

```text
TARGET_VALIDATION_CONTRACT=RESCOPED
GovernedDataUpdate=EXISTING_SEMANTICS
Fidelity=EXISTING_SEMANTICS
Idempotency=EXISTING_SEMANTICS
Conflict=EXISTING_SEMANTICS

Azure restart persistence=WP07
Azure recycle/redeploy persistence=WP07
deployment-level recovery=WP07

ImplementationDeltaCategory=NO_SOURCE_CHANGE
CurrentImageCanValidateFinalContract=true
```

The objective is to make the WP04/WP07 boundary explicit and produce the exact Terra WP04 target-validation contract that follows.

Do not execute Azure validation.
Do not allocate RunIds.
Do not publish.
Do not mutate GitHub.
Do not begin WP05.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract, policy, architecture, governance, acceptance definition

GPT-5.6 Terra
  implementation/validation and explicitly authorized mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Luna**.

---

## 2. Binding source-grounded decision

Preserve exactly:

```text
WP04 substantive acceptance:

1. Application-owned SQLite initialization and governed observation update.
2. Durable readback fidelity for the canonical qualification observation.
3. Existing persistence idempotency and conflict semantics, without defining new product behavior.
4. SQLite database identity /home/data/aiquant.db.
5. Schema version 4.
6. DELETE journal mode.
7. IntegrityCheck=ok and QuickCheck=ok.
8. Evidence identity/count and application-owned HTTP evidence retrieval.
9. H1 temporary qualification-setting restoration.
10. Secret hygiene, zero-cost architecture, and no-bypass constraints.

Not WP04:
Azure restart/recycle/redeploy persistence and deployment-level recovery.

Those are WP07 acceptance gates.
```

Do not broaden either WP.

---

## 3. Recover canonical planning/governance locations

Read-only locate the canonical statements in:

```text
RELEASE_1.12_DEFINITION.md
RELEASE_1.12_EXECUTION_PLAN.md
RELEASE_1.12_FILE_MANIFEST.md
WP04 issue/authority text if locally retained
WP07 definition/authority text if locally retained
```

Determine whether the repository planning documents already assign restart/recycle/redeploy recovery to WP07.

If yes, classify this as **reconciliation/correction of WP04 acceptance interpretation**, not a roadmap architecture change.

If any local canonical document actually and unambiguously assigns those deployment-recovery gates to WP04, report the contradiction rather than silently rewriting history.

Return:

```text
PlanningBoundaryResult=ALREADY_WP07|CONTRADICTION|NOT_PROVEN
PlanningBoundaryEvidence
```

---

## 4. Governance update semantics

If `PlanningBoundaryResult=ALREADY_WP07`:

```text
WP04AcceptanceInterpretation=RESCOPED_TO_EXISTING_PLAN
WP07AcceptanceInterpretation=UNCHANGED_EXISTING_PLAN
RoadmapArchitectureChange=false
```

No planning-file byte change is necessary merely to restate what the canonical execution plan already says.

If a durable clarification artifact is required by the project's governance convention, specify the exact artifact and wording for a later Terra documentation mutation; do not invent a source change unless needed.

If `CONTRADICTION`, stop with a narrow Luna blocker.

---

## 5. WP07 handoff contract

Explicitly preserve for future WP07:

```text
Azure restart persistence
Azure recycle/redeploy persistence
deployment-level recovery
target recovery after lifecycle operations
persistence continuity across those operations
```

Do not execute or pre-accept WP07.

Return:

```text
WP07DeferredAcceptanceGates
WP07Status
WP07ExecutionAuthorized=false
```

WP07 remains sequenced after WP04→WP05→WP06 unless the canonical Release 1.12 plan says otherwise.

---

## 6. WP04 executable target predicates

Ratify exactly these predicates for the next Terra target validation:

```text
D01 initialize invokes application-owned persistence use case
D02 canonical observation is accepted or idempotently recognized
D03 canonical observation is read back unchanged
D04 conflicting existing evidence is rejected/non-overwriting under the existing persistence contract
D05 database identity is /home/data/aiquant.db
D06 schema version is 4
D07 journal mode is DELETE
D08 integrity check is ok
D09 quick check is ok
D10 accepted evidence identity and count are correct
D11 HTTP evidence is attributable to the exact fresh RunId
D12 evidence checkpoint completes before RestoreOnly
D13 RestoreOnly occurs exactly once
D14 temporary qualification settings are restored
D15 no qualification token is persisted or disclosed
D16 no direct SQLite/Kudu/Python deployment bypass is used
D17 F1/West Central US/$0 architecture remains unchanged
D18 repository and external mutation boundaries remain clean
D19 durable evidence reopens with all references resolved
```

For D04, reconcile how the existing persistence tests provide acceptance credit.

Because the current target qualification mode has no new conflict payload and `ImplementationDeltaCategory=NO_SOURCE_CHANGE`, do not require Terra to invent a target conflict operation.

Define whether D04 is:

```text
TARGET_RUNTIME
CARRY_FORWARD_EXISTING_CONTRACT_TESTS
TARGET_PLUS_CARRY_FORWARD
```

and state the exact evidence required.

Likewise distinguish target runtime evidence from accepted carry-forward tests for D02 idempotency if appropriate.

---

## 7. Exact next Terra scope

Define a single existing-contract WP04 target validation.

Expected lifecycle:

```text
initialize
reopen
H1 Deferred → durable evidence checkpoint → RestoreOnly
```

No redeploy.
No deployment recovery.
No new helper action.
No source change.
No image rebuild.

RunId rules:

```text
fresh helper-owned RunId(s)
historical RunIds never reused
exact target attribution
```

Evidence endpoint:

```text
GET /internal/wp04/persistence-qualification?runId=<exact RunId>
X-WP04-Evidence-Token
```

Token temporary, never persisted/printed.

Return:

```text
TerraTargetValidationPredicates
CarryForwardPredicateEvidence
FreshTargetPredicateEvidence
RequiredFreshRunIds
RequiredAzureMutations
RequiredRestoration
```

---

## 8. Publication/lifecycle boundary

This authority does not authorize:

```text
commit
push
PR
merge
WP04 issue closure
Project #2 Done
milestone mutation
WP05
```

After successful Terra target validation, the next step must be a final Luna read-only WP04 substantive acceptance reconciliation.

Only after exact final acceptance may Terra receive publication/lifecycle authority.

---

## 9. Required decision states

If planning boundary is confirmed:

```text
WP04_WP07_GOVERNANCE_RESCOPE=PASS
WP04AcceptanceInterpretation=RESCOPED_TO_EXISTING_PLAN
WP07AcceptanceInterpretation=UNCHANGED_EXISTING_PLAN
WP04TargetValidationContract=READY
NextAuthorizedAction=GPT-5.6 Terra existing-contract WP04 target validation
```

If contradiction:

```text
WP04_WP07_GOVERNANCE_RESCOPE=BLOCKED
NextAuthorizedAction=GPT-5.6 Luna resolve planning contradiction
```

---

## 10. Required terminal markers

On PASS:

```text
RELEASE 1.12 WP04 — WP04/WP07 GOVERNANCE BOUNDARY: PASS
RELEASE 1.12 WP04 — WP04 ACCEPTANCE INTERPRETATION: RESCOPED_TO_EXISTING_PLAN
RELEASE 1.12 WP04 — WP07 DEPLOYMENT RECOVERY GATES: PRESERVED
RELEASE 1.12 WP04 — ROADMAP ARCHITECTURE CHANGE: 0
RELEASE 1.12 WP04 — TARGET VALIDATION CONTRACT: READY
RELEASE 1.12 WP04 — SOURCE CHANGES REQUIRED: 0
RELEASE 1.12 WP04 — IMAGE REBUILD REQUIRED: 0
RELEASE 1.12 WP04 — AZURE MUTATIONS: 0
RELEASE 1.12 WP04 — RUNIDS ALLOCATED: 0
RELEASE 1.12 WP04 — PUBLICATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — WP05: NOT_STARTED
```

---

## 11. Required handoff

Return:

```text
PlanningBoundaryResult
PlanningBoundaryEvidence
WP04AcceptanceInterpretation
WP07AcceptanceInterpretation
RoadmapArchitectureChange

WP07DeferredAcceptanceGates
WP07Status
WP07ExecutionAuthorized

D02EvidenceClass
D04EvidenceClass
D02CarryForwardEvidence
D04CarryForwardEvidence

TerraTargetValidationPredicates
CarryForwardPredicateEvidence
FreshTargetPredicateEvidence
RequiredFreshRunIds
RequiredAzureMutations
RequiredRestoration

SourceChangesRequired
ImageRebuildRequired
BoundaryBlocker
FinalDecision
NextAuthorizedAction
```

No mutations.

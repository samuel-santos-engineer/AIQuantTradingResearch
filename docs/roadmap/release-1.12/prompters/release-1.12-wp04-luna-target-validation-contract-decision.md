# Release 1.12 WP04 — Luna Target-Validation Contract Decision

**Selected execution model: GPT-5.6 Luna**

## Mission

Resolve the architecture/policy blocker discovered by Terra **without Azure mutation**.

Terra correctly stopped at:

```text
TARGET_VALIDATION_CONTRACT=NOT_PROVEN
AuthorizedAzureMutationCount=0
AllocatedRunIds=0
BoundaryBlocker=missing canonical target update/conflict/redeploy/recovery contract
```

The existing executable contract proves only:

```text
Phase=initialize
LifecycleAction=Restart|None
Deferred → durable evidence checkpoint → RestoreOnly
application-owned HTTP evidence retrieval
```

It does **not** define acceptance-grade executable mechanics for:

```text
GovernedDataUpdate
FidelityIdempotencyConflictSemantics
AzureRedeployPersistence
TargetRecoveryBeyondTemporarySettingRestoration
```

Luna must now determine the **minimum truthful WP04 contract** for these gates and whether each gate:
1. is genuinely required by the already-approved WP04 scope;
2. can be mapped to an existing application/domain operation without new product semantics;
3. requires a narrow new qualification/test seam;
4. must be reclassified because the prior acceptance wording overreached the executable WP04 scope.

Do not invent product behavior merely to satisfy an acceptance label.

No mutations.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract, architecture, policy, acceptance semantics, governance, read-only decision

GPT-5.6 Terra
  later implementation and governed target validation under explicit authority

GPT-5.6 Sol
  supporting analysis only; never substitutes for Luna/Terra
```

Selected model: **GPT-5.6 Luna**.

---

## 2. Fixed accepted boundaries

Do not reopen:

```text
Hybrid W1-W8=ACCEPTED
PersistentInitialization=PASS
Integrity=PASS
SchemaV4=PASS
DeleteJournal=PASS
EvidenceReuse=PASS
SecretHygiene=PASS
ZeroCostNoBypass=PASS
RequiredValidations=PASS
```

Pending governance:

```text
PRMergePostMerge
IssueProjectLifecycle
```

Current source anchor:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Wrapper SHA256:

```text
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

Windows PowerShell target:

```text
5.1.26100.9444
```

---

## 3. Evidence and architecture sources to inspect

Read-only inspect the canonical project sources necessary to answer the contract question, including:

```text
WP04 issue/definition/implementation authorities
Release 1.12 definition/execution plan/file manifest
Domain/Application persistence interfaces and implementations
SQLite Infrastructure implementation
Worker composition/modes
qualification mode implementation
qualification HTTP endpoint
initialize-qualification.ps1
verify-persistent-sqlite-webapp.ps1
accepted D3/H1/S2 authorities
accepted W1-W8 contract/evidence
relevant persistence unit/integration tests
Initiative-1.11 feasibility constraints
```

Search for existing application/domain semantics for:

```text
insert/update/upsert/append
accepted evidence identity
idempotency
duplicate handling
conflict handling
overwrite/rejection
reopen/readback
recovery
restart
redeploy
container/config restart semantics
```

Do not assume these operations exist.

---

## 4. Decision principle

For every disputed gate, choose among exactly these classifications:

### EXISTING_SEMANTICS

The behavior already exists in Domain/Application and only needs a narrow qualification/validation surface.

### NARROW_QUALIFICATION_EXTENSION

No new product/domain semantics are required, but existing behavior needs a new application-owned qualification command/evidence field or Azure helper lifecycle action to make it acceptance-testable.

### NEW_PRODUCT_SEMANTICS

Satisfying the gate would require defining behavior the product does not currently own. Do not invent it inside a validation authority.

### ACCEPTANCE_RESCOPING_REQUIRED

The gate wording is broader than WP04's approved implementation scope and should be narrowed/reframed to the actual product contract.

For each classification cite the exact retained source/code evidence.

---

# 5. GovernedDataUpdate decision

Determine what “GovernedDataUpdate” canonically means for WP04.

Answer:

```text
Does an application-owned data-write/update operation already exist?
What entity/evidence is written?
What input identifies it?
What operation is permitted?
What constitutes acceptance?
What durable readback proves it?
Does D3 initialize already perform the governed write required by WP04?
```

Important: the historical D3 record contains:

```text
AcceptedEvidenceIdentity
AcceptedEvidenceCount
```

Determine whether that is already a governed application update/write or only initialization fixture seeding.

Do not create a second SQLite ownership path.

Return:

```text
GovernedDataUpdateClassification
GovernedDataUpdateCanonicalOperation
GovernedDataUpdateInputContract
GovernedDataUpdateExpectedEvidence
GovernedDataUpdateImplementationNeeded=true|false
```

---

# 6. Fidelity / idempotency / conflict decision

Recover actual existing semantics.

Determine separately:

```text
Fidelity
Idempotency
Conflict
```

For each, answer:

```text
What existing Domain/Application rule defines it?
Which current tests prove the rule?
Can target qualification exercise it without defining new behavior?
What exact before/after evidence is required?
```

Do not infer that all three must use one synthetic “update” operation.

In particular, determine whether:
- idempotency already follows from accepted evidence identity semantics;
- conflict behavior already has a defined rejection/precedence rule;
- “conflict semantics” was intended only as preservation of existing application behavior rather than creation of a new target mutation API.

Return:

```text
FidelityClassification
IdempotencyClassification
ConflictClassification

FidelityCanonicalExpectation
IdempotencyCanonicalExpectation
ConflictCanonicalExpectation

FidelityImplementationNeeded
IdempotencyImplementationNeeded
ConflictImplementationNeeded
```

If conflict has no existing product rule, explicitly classify it `NEW_PRODUCT_SEMANTICS` or `ACCEPTANCE_RESCOPING_REQUIRED`; do not invent a rule.

---

# 7. Azure “redeploy” decision

Define exactly what lifecycle action counts as WP04 redeploy persistence.

Inspect Release 1.12 deployment architecture and WP03 automation.

Choose an existing Azure operation, if available, that:
- exercises replacement/recreation/reapplication of the running deployment sufficiently to test `/home` persistence;
- uses the same already-published image;
- creates no paid resource;
- requires no image rebuild;
- is distinguishable from a simple restart;
- preserves the strict $0 architecture.

Possible mechanisms must be derived from project/Azure deployment architecture, not selected arbitrarily.

Return:

```text
RedeployClassification
CanonicalRedeployOperation
WhyThisQualifiesAsRedeploy
ExpectedAzureMutation
ExpectedImageIdentityBeforeAfter
ExpectedPersistenceEvidence
RedeployHelperExtensionNeeded=true|false
```

If no existing approved operation is canonically implied, define the **narrowest architecture-consistent lifecycle operation** and explicitly mark it as a Luna decision for WP04.

---

# 8. Recovery decision

Disambiguate “Recovery”.

Determine whether WP04 requires:

```text
A. recovery after process restart
B. recovery after deployment/redeploy
C. restoration of temporary qualification settings
D. database corruption recovery/repair
E. evidence retrieval recovery
F. some combination
```

Do not silently expand “Recovery” into corruption repair if the approved scope never required that.

Use existing WP04 authorities to identify the intended meaning.

Return:

```text
RecoveryClassification
CanonicalRecoveryDefinition
RequiredRecoveryScenario
RequiredRecoveryEvidence
RecoveryImplementationNeeded=true|false
```

If H1 restoration plus restart/redeploy continuity fully represents the approved recovery requirement, say so explicitly.

---

# 9. Decide the minimum implementation delta

After sections 5-8, produce an exact implementation plan.

Allowed plan categories:

```text
NO_SOURCE_CHANGE
QUALIFICATION_ONLY_SOURCE_CHANGE
HELPER_ONLY_SOURCE_CHANGE
QUALIFICATION_AND_HELPER_SOURCE_CHANGE
POLICY_RESCOPING_ONLY
OTHER_REQUIRES_NEW_LUNA_DECISION
```

If source change is needed, enumerate exact allowed tracked paths/classes/functions.

Prefer the smallest application-owned qualification extension over a new production API.

No Python/Streamlit SQLite ownership.
No direct SQL validation path.
No Azure-specific second persistence implementation.

Return:

```text
ImplementationDeltaCategory
AllowedTrackedPaths
NewQualificationModesOrArguments
NewEvidenceFields
NewHelperLifecycleActions
RequiredTests
SchemaChangeRequired=true|false
ImageRebuildRequired=true|false
```

A schema change should be `false` unless unavoidable and separately justified.

---

# 10. Define exact acceptance predicates

Create the executable acceptance contract Terra must later implement/run.

For each remaining substantive gate define:

```text
PredicateId
Gate
Setup
Action
ExpectedApplicationResult
ExpectedDurableState
ExpectedEvidenceFields
ExpectedAzureLifecycle
FailureCondition
```

No vague phrases such as “prove fidelity.”

Every predicate must be mechanically testable.

---

# 11. Fresh RunId / evidence rules

Define which phases require fresh RunIds and how identity continuity is demonstrated.

Return:

```text
RequiredLifecyclePhases
RunIdAllocationRules
PersistentIdentityCorrelationRules
EvidenceEndpointUsage
EvidenceCheckpointRules
RestorationRules
```

Preserve RB2 helper ownership if still applicable.

Never authorize reuse of historical RunIds.

---

# 12. Publication impact

Determine whether the selected contract requires tracked source changes.

If yes:

```text
CurrentImageCanValidateFinalContract=false
NewImageBuildRequiredAfterImplementation=true
```

If no:

```text
CurrentImageCanValidateFinalContract=true
```

Do not build/publish anything in this authority.

Explain whether any implementation PR must precede target validation.

---

# 13. Governance sequencing decision

Select exactly one:

### DECISION A — existing contract sufficient

```text
TARGET_VALIDATION_CONTRACT=DEFINED
ImplementationDeltaCategory=NO_SOURCE_CHANGE
NextAuthorizedAction=GPT-5.6 Terra combined Azure substantive validation under exact Luna contract
```

### DECISION B — narrow implementation required

```text
TARGET_VALIDATION_CONTRACT=DEFINED
ImplementationDeltaCategory=<source-change category>
NextAuthorizedAction=GPT-5.6 Terra implement/test/publish required WP04 qualification/helper delta, then execute combined Azure validation under the same defined contract
```

Prefer one Terra authority that can implement, validate, build/publish the changed image if required, and execute target validation **only if** all those actions lie within one clearly defined governance boundary and can be safely mutation-accounted.

If publication of a new candidate image or source commit requires a distinct governance boundary under existing project rules, state the required split explicitly.

### DECISION C — acceptance rescoping required

```text
TARGET_VALIDATION_CONTRACT=RESCOPED
NextAuthorizedAction=<exact next authority>
```

Provide replacement canonical acceptance wording and justification from prior WP04 scope.

---

# 14. Required terminal markers

On successful decision:

```text
RELEASE 1.12 WP04 — TARGET VALIDATION CONTRACT DECISION: PASS
RELEASE 1.12 WP04 — GOVERNED DATA UPDATE CONTRACT: DEFINED
RELEASE 1.12 WP04 — FIDELITY CONTRACT: DEFINED
RELEASE 1.12 WP04 — IDEMPOTENCY CONTRACT: DEFINED
RELEASE 1.12 WP04 — CONFLICT CONTRACT: DEFINED_OR_TRUTHFULLY_RESCOPED
RELEASE 1.12 WP04 — AZURE REDEPLOY CONTRACT: DEFINED
RELEASE 1.12 WP04 — RECOVERY CONTRACT: DEFINED
RELEASE 1.12 WP04 — TARGET_VALIDATION_CONTRACT: DEFINED
RELEASE 1.12 WP04 — AZURE MUTATIONS: 0
RELEASE 1.12 WP04 — RUNIDS ALLOCATED: 0
RELEASE 1.12 WP04 — PUBLICATION: NOT_AUTHORIZED_BY_LUNA
RELEASE 1.12 WP04 — WP05: NOT_STARTED
```

---

# 15. Required handoff

Return:

```text
GovernedDataUpdateClassification
GovernedDataUpdateCanonicalOperation
GovernedDataUpdateInputContract
GovernedDataUpdateExpectedEvidence
GovernedDataUpdateImplementationNeeded

FidelityClassification
IdempotencyClassification
ConflictClassification
FidelityCanonicalExpectation
IdempotencyCanonicalExpectation
ConflictCanonicalExpectation
FidelityImplementationNeeded
IdempotencyImplementationNeeded
ConflictImplementationNeeded

RedeployClassification
CanonicalRedeployOperation
WhyThisQualifiesAsRedeploy
ExpectedAzureMutation
ExpectedImageIdentityBeforeAfter
ExpectedPersistenceEvidence
RedeployHelperExtensionNeeded

RecoveryClassification
CanonicalRecoveryDefinition
RequiredRecoveryScenario
RequiredRecoveryEvidence
RecoveryImplementationNeeded

ImplementationDeltaCategory
AllowedTrackedPaths
NewQualificationModesOrArguments
NewEvidenceFields
NewHelperLifecycleActions
RequiredTests
SchemaChangeRequired
ImageRebuildRequired

ExecutableAcceptancePredicateMatrix
RequiredLifecyclePhases
RunIdAllocationRules
PersistentIdentityCorrelationRules
EvidenceEndpointUsage
EvidenceCheckpointRules
RestorationRules

CurrentImageCanValidateFinalContract
NewImageBuildRequiredAfterImplementation

BoundaryBlocker
FinalDecision
NextAuthorizedAction
```

No mutations.

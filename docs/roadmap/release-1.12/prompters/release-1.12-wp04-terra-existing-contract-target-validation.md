# Release 1.12 WP04 — Terra Existing-Contract Target Validation

**Selected execution model: GPT-5.6 Terra**

## Mission

Execute the final **existing-contract WP04 Azure target validation** after canonical ownership reconciliation.

Binding policy:

```text
CanonicalOwnershipDecision=WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
WP04_DEPLOYMENT_RECOVERY_REQUIRED_FOR_CLOSURE=false
WP07_DEPLOYMENT_RECOVERY_REQUIRED_FOR_RELEASE_ACCEPTANCE=true
PlanningAmendmentRequired=false
RoadmapArchitectureChange=false
```

This authority proves only the remaining WP04 predicates:

```text
D01-D03
D05-D15
D17
D19
```

Carry forward:

```text
D04 conflict semantics
D16 no direct bypass
D18 mutation-boundary acceptance
```

Do not execute WP07 restart/recycle/redeploy/deployment-recovery gates.

No source change. No image rebuild. No GitHub mutation. No publication. No WP05.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract/policy/architecture/governance/acceptance

GPT-5.6 Terra
  governed execution/validation and explicitly authorized Azure mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Terra**.

---

## 2. Canonical target contract

WP04 recovery means:

```text
application-owned SQLite initialization/update/reopen/readback recovery
durable evidence checkpointing
H1 temporary qualification-setting restoration
integrity/schema/journal continuity
```

WP07, not WP04, owns:

```text
Azure restart persistence
Azure recycle persistence
Azure redeploy persistence
deployment-level recovery
persistence continuity across those lifecycle operations
```

Do not perform those WP07 actions.

---

## 3. Fixed identities and invariants

Expected repository/source anchor at authority entry:

```text
HEAD=2532f6abd4677edfb205c26c083a534783038979
branch=release/1.12-wp04-persistent-sqlite
```

Expected wrapper SHA256:

```text
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

Frozen runner SHA256:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Expected deployed image digest:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Target architecture:

```text
Azure App Service Linux F1
West Central US
custom Docker
public/free GHCR
persistent /home
database=/home/data/aiquant.db
schema=4
journal=DELETE
strict recurring infrastructure cost=$0.00
```

No source/image mutation is authorized.

---

## 4. PowerShell and repository preflight

Require:

```text
Windows PowerShell=5.1.26100.9444
parser errors=0
HEAD exact
branch exact
wrapper SHA exact
frozen runner SHA exact
staged paths=0
git diff --check=PASS
```

The only tracked modifications permitted at entry are the two pre-existing WP04 paths:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

This authority must not alter their bytes.

If any preflight identity fails, STOP before RunId allocation or Azure mutation.

---

## 5. Read-only Azure pre-state

Before RunId allocation, capture sanitized target pre-state:

```text
target identity
resource group
region
plan/SKU
web app state
configured image identity
relevant H1 six-setting pre-state
qualification-setting presence/absence
WEBSITES_PORT
SCM basic-auth state
FTP state
persistent-storage configuration
```

Never persist or print secret values.

Record only canonical safe presence/absence/identity information for secret-bearing settings.

Require:

```text
PreStateCapture=PASS
SecretHygiene=PASS
F1=true
WestCentralUS=true
RecurringInfrastructureCost=$0.00
```

Read-only calls do not count as mutations.

---

## 6. Fresh RunId discipline

Only after all pre-RunId gates PASS, allocate fresh helper-owned RunId(s) according to RB2.

Historical RunIds must never be reused.

Required lifecycle phases:

```text
initialize
reopen
```

Use the minimum number of fresh RunIds permitted by the canonical helper/qualification contract.

Every allocated RunId is permanently consumed even if an attempt fails.

Return all allocated and failed/consumed identities.

---

## 7. Authorized Azure mutations

Only:

```text
temporary canonical qualification App Settings
canonical H1 Deferred lifecycle application
canonical H1 RestoreOnly restoration
```

No explicit restart beyond what the accepted qualification/settings lifecycle inherently triggers.

M1 remains binding: do not add a redundant restart.

Forbidden:

```text
redeploy
recycle experiment
WP07 lifecycle validation
resource creation
paid-resource change
registry credential change
Twelve Data secret configuration
diagnostic logging experiment
source/image mutation
```

Every mutation must have exact before/after/restoration evidence.

---

## 8. Application-owned qualification execution

Use only the existing application-owned qualification path.

Expected mode:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__HttpEvidenceEnabled=true
```

Evidence endpoint:

```text
GET /internal/wp04/persistence-qualification?runId=<exact RunId>
X-WP04-Evidence-Token
```

Token must be temporary and never persisted/printed.

Do not use:

```text
direct SQLite shell
Python direct DB access
Kudu /home retrieval
deployment-side SQL bypass
```

---

## 9. D01 — application-owned persistence use case

Prove from target-attributable execution evidence that `initialize` invokes the existing application-owned persistence path represented by:

```text
PersistentSqliteQualificationExecution
→ PersistHistoricalObservationsUseCase.Execute(...)
→ IHistoricalObservationStore.Persist(...)
```

Do not require a new API or new product behavior.

Expected:

```text
D01=PASS
```

---

## 10. D02 — accepted/idempotently recognized observation

Prove the canonical qualification observation is:

```text
NewlyAccepted
```

or, when canonically repeating the same observation:

```text
Idempotent
```

Use the existing persistence result contract.

Target runtime evidence is required and may be combined with accepted carry-forward contract tests.

Do not invent a new idempotency expectation.

Expected:

```text
D02=PASS
```

---

## 11. D03 — durable readback fidelity

Prove application-owned reopen/readback returns the canonical observation unchanged, with the expected accepted evidence identity.

Required target-attributable correlation includes:

```text
RunId
Phase
DatabasePathIdentity
AcceptedEvidenceIdentity
AcceptedEvidenceCount
```

Expected:

```text
D03=PASS
```

---

## 12. D04 — conflict semantics carry-forward

Do not invent or execute a new Azure conflict payload.

D04 acceptance is carry-forward from the existing application/domain persistence contract tests proving conflicting evidence is non-overwriting/rejected according to the established contract.

Recover and reference:

```text
PersistHistoricalObservationsUseCaseTests
PersistenceContractTests
ExperimentPersistenceTests
```

Require tests/evidence remain valid against current source.

Expected:

```text
D04=CARRY_FORWARD_PASS
FreshAzureConflictOperationCount=0
```

If current source invalidates the carry-forward tests, STOP; do not invent replacement semantics.

---

## 13. D05-D10 — persistence identity and integrity

From application-owned target evidence require:

```text
D05 DatabasePathIdentity=/home/data/aiquant.db
D06 SchemaVersion=4
D07 JournalMode=delete
D08 IntegrityCheck=ok
D09 QuickCheck=ok
D10 AcceptedEvidenceIdentity/count=canonical expected values
```

Do not use direct DB inspection to fill missing fields.

---

## 14. D11 — exact target attribution

Application-owned HTTP evidence must be attributable to the exact fresh RunId.

Require:

```text
requested RunId == returned RunId/correlation identity
durable qualification artifact == requested correlation
no stale/foreign record accepted
```

A diagnostic or historical D3 record receives zero acceptance credit.

Expected:

```text
D11=PASS
```

---

## 15. D12-D14 — H1 checkpoint/restoration

Preserve exact ordering:

```text
Deferred
→ durable evidence checkpoint
→ RestoreOnly in finally
```

Require:

```text
D12 checkpoint completed before RestoreOnly
D13 RestoreOnlyCount=1
D14 temporary qualification settings restored to captured pre-state
```

Restoration failure overrides otherwise-successful validation.

Do not claim PASS unless final restoration is proven.

---

## 16. D15 — token/secret hygiene

Require:

```text
qualification evidence token not persisted
token not printed
token not present in durable artifacts/logs
TwelveData__ApiKey not configured by WP04
no secret-bearing value included in evidence
```

Expected:

```text
D15=PASS
```

---

## 17. D16 — no-bypass carry-forward

Carry forward accepted no-bypass evidence and prove this authority does not contradict it.

Require actual execution paths contain no:

```text
direct SQLite shell
Python DB manipulation
Kudu /home retrieval
Azure-specific second persistence implementation
```

Expected:

```text
D16=CARRY_FORWARD_PASS
```

---

## 18. D17 — architecture remains $0

Read-only target evidence plus execution mutation ledger must prove:

```text
App Service Linux F1
West Central US
same custom-Docker/public-GHCR architecture
no paid resource created
no Azure SQL
no Azure Files
no mandatory ACR
recurring infrastructure cost=$0.00
```

Expected:

```text
D17=PASS
```

---

## 19. D18 — mutation boundary carry-forward + current check

D18 is already accepted but must not be contradicted.

At end require:

```text
AuthorityIntroducedTrackedProductionMutations=0
StagedPaths=0
CommitCount=0
PushCount=0
GitHubMutationCount=0
DockerBuildCount=0
GhcrPublicationCount=0
WP07LifecycleActionCount=0
```

The two pre-existing tracked WP04 paths must have identical entry/exit hashes.

Expected:

```text
D18=PASS
```

---

## 20. D19 — durable evidence reopen

Create a fresh durable evidence root for this target validation.

Retain/index:

```text
authority/contract identity
preflight
target pre-state
RunIds
script/helper/frozen identities
sanitized Azure mutation ledger
qualification terminal output
HTTP poll observations
application-owned qualification record
archive/retrieval state
checkpoint evidence
RestoreOnly evidence
target post-state
D01-D19 predicate records
carry-forward evidence references
secret hygiene
repository invariants
manifest/index/hashes
```

Then reopen independently.

Require:

```text
DurableReopen=PASS
PredicateCount=19
PassedOrAcceptedCarryForward=19
Failed=0
UnresolvedPredicateEvidenceReferences=0
UnresolvedEvidenceReferences=0
MissingRequiredEvidenceClasses=0
```

Expected:

```text
D19=PASS
```

---

## 21. Predicate record schema

Every D01-D19 record must contain:

```text
PredicateId
EvidenceClass=TARGET_RUNTIME|CARRY_FORWARD_TESTS|TARGET_PLUS_CARRY_FORWARD|ALREADY_ACCEPTED
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
ReferenceResolved
```

No aggregate-only PASS.

---

## 22. Archive/retrieval truth policy

Preserve the accepted policy:

```text
RETAINED + PASS|EMPTY                 eligible
RETAINED + FAIL|NOT_APPLICABLE       fail
RETRIEVAL_FAILED + NOT_APPLICABLE    eligible
other combinations                    inconsistent/fail
```

No Kudu `/home` retry.

Application-owned HTTP evidence is the authoritative target evidence surface where required.

---

## 23. Convergence loop

For every defect correctable entirely within disposable local validation/evidence machinery:

```text
evaluate ALL D01-D19
collect ALL defects
fix ALL in-scope disposable defects
rehash
invalidate affected evidence
consume any allocated failed RunIds
fresh durable root
fresh RunId(s) as required
rerun the complete affected target cycle
reopen evidence
reevaluate ALL D01-D19
```

Continue internally until PASS.

Do not return after each local defect.

### Stop immediately if correction requires

```text
tracked/production source mutation
frozen runner mutation
new image build/publication
new product semantics
new architecture/policy decision
WP07 restart/recycle/redeploy action
paid Azure/resource mutation
Twelve Data secret
GitHub mutation
PR/merge
WP05
```

Attempt canonical H1 restoration before returning from any post-mutation failure.

---

## 24. Final target-state proof

At completion prove:

```text
temporary qualification settings restored exactly
target architecture unchanged
no WP07 lifecycle action executed
no secret disclosed
repository unchanged by authority
```

Normal root health may remain blocked by the missing Twelve Data key; that is the accepted WP05 downstream configuration boundary and not a WP04 failure.

Do not configure the key to make root health green.

---

## 25. Final self-reconciliation

After durable reopen evaluate all sections exhaustively:

```text
A PreflightAndIdentity
B GovernedUpdate
C FidelityAndIdempotency
D ConflictCarryForward
E PersistenceIdentityIntegrity
F TargetAttribution
G CheckpointAndRestoration
H SecretHygieneNoBypass
I ZeroCostArchitecture
J MutationBoundary
K DurableEvidenceReopen
L FinalTargetState
```

All A-L must PASS.

---

## 26. Success markers

Only on complete success:

```text
RELEASE 1.12 WP04 — EXISTING-CONTRACT TARGET VALIDATION: PASS
RELEASE 1.12 WP04 — D01-D19: 19/19 PASS_OR_ACCEPTED_CARRY_FORWARD
RELEASE 1.12 WP04 — GOVERNED DATA UPDATE: PASS
RELEASE 1.12 WP04 — FIDELITY: PASS
RELEASE 1.12 WP04 — IDEMPOTENCY: PASS
RELEASE 1.12 WP04 — CONFLICT SEMANTICS: CARRY_FORWARD_PASS
RELEASE 1.12 WP04 — DATABASE IDENTITY: PASS
RELEASE 1.12 WP04 — SCHEMA V4: PASS
RELEASE 1.12 WP04 — SQLITE DELETE JOURNAL: PASS
RELEASE 1.12 WP04 — INTEGRITY / QUICK CHECK: PASS
RELEASE 1.12 WP04 — HTTP RUNID ATTRIBUTION: PASS
RELEASE 1.12 WP04 — H1 CHECKPOINT / RESTORATION: PASS
RELEASE 1.12 WP04 — SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — ZERO COST / NO BYPASS: PASS
RELEASE 1.12 WP04 — DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — EVIDENCE REFERENCES: ALL_RESOLVED
RELEASE 1.12 WP04 — WP07 LIFECYCLE ACTIONS: 0
RELEASE 1.12 WP04 — AUTHORITY TRACKED SOURCE MUTATIONS: 0
RELEASE 1.12 WP04 — FINAL TARGET STATE: RESTORED
RELEASE 1.12 WP04 — SUBSTANTIVE ACCEPTANCE CANDIDATE: READY_FOR_FINAL_LUNA
RELEASE 1.12 WP04 — PUBLICATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — WP05: NOT_STARTED
RELEASE 1.12 WP04 — WP07: NOT_STARTED
```

Do not claim final WP04 acceptance under Terra.

---

## 27. Required handoff

Return:

```text
TargetValidationRoot
AllocatedRunIds
ConsumedFailedRunIds

Head
Branch
WindowsPowerShellVersion
ParserErrorCount
RunnerSHA256
WrapperSourceSHA256
WrapperEntrySHA256
WrapperExitSHA256
HelperEntrySHA256
HelperExitSHA256
ImageIdentity

TargetIdentity
AzureRegion
AppServiceSku
RecurringInfrastructureCost

PreStateCaptureResult
AuthorizedAzureMutationCount
MutationLedger

D01Result
D02Result
D03Result
D04Result
D05Result
D06Result
D07Result
D08Result
D09Result
D10Result
D11Result
D12Result
D13Result
D14Result
D15Result
D16Result
D17Result
D18Result
D19Result

PredicateCount
PassedPredicateCount
CarryForwardPredicateCount
FailedPredicateCount
UnresolvedPredicateEvidenceReferenceCount

QualificationResult
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
WrapperExitCode

DurableReopenResult
UnresolvedEvidenceReferenceCount
MissingRequiredEvidenceClassCount

AuthorityIntroducedTrackedMutationCount
StagedPathCount
GitDiffCheckResult
GitHubMutationCount
DockerBuildCount
GhcrPublicationCount
WP07LifecycleActionCount
TwelveDataSecretConfigured
SecretHygieneResult
FinalTargetStateRestored

SectionAResult
SectionBResult
SectionCResult
SectionDResult
SectionEResult
SectionFResult
SectionGResult
SectionHResult
SectionIResult
SectionJResult
SectionKResult
SectionLResult

BoundaryBlocker
FinalAcceptance
NextAuthorizedAction
```

On success:

```text
FinalAcceptance=READY_FOR_FINAL_LUNA
NextAuthorizedAction=GPT-5.6 Luna final read-only WP04 substantive acceptance reconciliation; publication/lifecycle remain unauthorized
```

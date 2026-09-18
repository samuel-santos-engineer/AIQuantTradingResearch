# Release 1.12 WP04 — Terra Clean-Anchor Existing-Contract Target Validation

**Selected execution model: GPT-5.6 Terra**

## Mission

Execute the final remaining WP04 target validation from the now-clean governed release-branch anchor:

```text
bf91221ec51baa18e878d2aad1184431ca235cdd
```

Binding reconciliations:

```text
CanonicalOwnershipDecision=WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
WP04_DEPLOYMENT_RECOVERY_REQUIRED_FOR_CLOSURE=false
WP07_DEPLOYMENT_RECOVERY_REQUIRED_FOR_RELEASE_ACCEPTANCE=true

PR_SCOPE_DECISION=
INTERMEDIATE_PR_NOT_REQUIRED__TARGET_VALIDATION_FROM_COMMITTED_RELEASE_BRANCH

CleanHeadRequiresLocalCommit=true
CleanHeadRequiresRemoteBranchPublication=false
CleanHeadRequiresMainMerge=false
CurrentReleaseBranchCleanHeadProvenance=PASS
CurrentReleaseBranchReadyForTargetValidation=true
```

The 17-path accumulated WP04 PR is deferred until final WP04 acceptance.

This authority does NOT authorize PR creation, merge, issue/project lifecycle completion, WP05, or WP07.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract/policy/architecture/governance/final acceptance

GPT-5.6 Terra
  governed target validation and explicitly authorized temporary Azure mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected execution model: **GPT-5.6 Terra**.

---

## 2. Exact execution anchor

Require before any RunId allocation or Azure mutation:

```text
HEAD=bf91221ec51baa18e878d2aad1184431ca235cdd
remote governed release-branch tip=bf91221ec51baa18e878d2aad1184431ca235cdd
working tree clean
staged paths=0
git diff --check=PASS
```

Expected wrapper SHA256:

```text
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

Expected helper SHA256:

```text
5ED81167F81287AC2614546C063C938F8F747DE664190CF95BF0E2D125C9B690
```

Require:

```text
wrapper working-tree SHA == wrapper HEAD SHA == expected wrapper SHA
helper working-tree SHA == helper HEAD SHA == expected helper SHA

git diff --quiet HEAD -- eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1 = PASS

git diff --quiet HEAD -- eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1 = PASS
```

Frozen runner expected SHA256:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

If any identity/preflight gate fails, STOP before allocating a RunId or mutating Azure.

---

## 3. PowerShell / parser gate

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

AST parser errors:

```text
initialize-qualification.ps1=0
verify-persistent-sqlite-webapp.ps1=0
```

No PowerShell 7 assumptions.

---

## 4. Accepted carry-forward evidence

Do not rerun W5-W8.

Preserve:

```text
Hybrid W1-W8=ACCEPTED
D04 conflict semantics=CARRY_FORWARD_REQUIRED
D16 no direct bypass=CARRY_FORWARD_REQUIRED
D18 mutation boundary=ALREADY_ACCEPTED, with current-authority non-contradiction check
```

D04 carry-forward sources include the established current-source tests:

```text
PersistHistoricalObservationsUseCaseTests
PersistenceContractTests
ExperimentPersistenceTests
```

No Azure conflict payload is authorized or required.

---

## 5. Fresh target predicates

Fresh target execution is required for:

```text
D01 D02 D03
D05 D06 D07 D08 D09 D10 D11
D12 D13 D14 D15
D17
D19
```

Canonical meanings:

```text
D01 initialize invokes application-owned persistence use case
D02 canonical observation accepted or idempotently recognized
D03 canonical observation read back unchanged
D05 database identity=/home/data/aiquant.db
D06 schema version=4
D07 journal mode=DELETE
D08 integrity check=ok
D09 quick check=ok
D10 accepted evidence identity/count correct
D11 HTTP evidence attributable to exact fresh RunId
D12 durable evidence checkpoint before RestoreOnly
D13 RestoreOnly exactly once
D14 temporary qualification settings restored
D15 token/secret hygiene
D17 F1 / West Central US / $0 architecture unchanged
D19 durable evidence reopens with all references resolved
```

---

## 6. Azure pre-state before RunId allocation

Capture sanitized read-only target pre-state before any RunId allocation:

```text
target identity
resource group
region
App Service plan/SKU
web app state
configured image identity
H1 six-setting pre-state
qualification-setting presence/absence
WEBSITES_PORT
SCM basic-auth state
FTP state
persistent-storage configuration
```

Never persist or print secret values.

Require:

```text
PreStateCapture=PASS
AppServiceSku=F1
AzureRegion=West Central US
RecurringInfrastructureCost=$0.00
SecretHygiene=PASS
```

Expected deployed image digest remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

No image rebuild/publication.

---

## 7. Fresh RunId rules

Only after every pre-RunId gate passes:

- allocate the minimum fresh helper-owned RunId(s) required by the canonical initialize/reopen qualification contract;
- use RB2 helper ownership/generation;
- never reuse any historical RunId;
- permanently consume every allocated RunId, including failed attempts.

Return:

```text
AllocatedRunIds
ConsumedFailedRunIds
```

---

## 8. Authorized Azure mutations

Only the canonical existing-contract qualification lifecycle is authorized:

```text
temporary qualification App Settings
H1 Deferred application
durable evidence checkpoint
RestoreOnly in finally
```

M1 remains binding:

```text
no redundant explicit restart
```

Forbidden:

```text
Azure redeploy
recycle experiment
WP07 restart/recycle/redeploy validation
resource creation
paid-resource mutation
registry credential mutation
Twelve Data secret configuration
diagnostic logging experiment
```

Every actual Azure mutation must be ledgered exactly.

---

## 9. Qualification execution

Use only the application-owned qualification path:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__HttpEvidenceEnabled=true
```

HTTP evidence:

```text
GET /internal/wp04/persistence-qualification?runId=<exact fresh RunId>
X-WP04-Evidence-Token
```

Token:

```text
temporary
never persisted
never printed
never present in durable artifacts
```

No:

```text
direct SQLite shell
Python direct DB access
Kudu /home retrieval
deployment-side SQL bypass
```

---

## 10. D01-D03 application semantics

Prove target-attributably:

```text
D01
PersistentSqliteQualificationExecution
→ PersistHistoricalObservationsUseCase.Execute(...)
→ IHistoricalObservationStore.Persist(...)

D02
canonical observation is NewlyAccepted or canonically Idempotent

D03
application-owned reopen/readback returns canonical observation unchanged
```

Required correlation:

```text
RunId
Phase
DatabasePathIdentity
AcceptedEvidenceIdentity
AcceptedEvidenceCount
```

Do not invent new update/idempotency behavior.

---

## 11. D04 conflict carry-forward

No fresh Azure conflict operation.

Revalidate that current source still supports the accepted contract-test evidence:

```text
conflicting evidence is rejected/non-overwriting
```

Expected:

```text
D04=CARRY_FORWARD_PASS
FreshAzureConflictOperationCount=0
```

---

## 12. D05-D10 persistence truth

Application-owned target evidence must prove:

```text
D05 DatabasePathIdentity=/home/data/aiquant.db
D06 SchemaVersion=4
D07 JournalMode=delete
D08 IntegrityCheck=ok
D09 QuickCheck=ok
D10 AcceptedEvidenceIdentity/count=canonical expected values
```

Do not fill evidence gaps with direct DB inspection.

---

## 13. D11 exact RunId attribution

Require exact fresh RunId attribution across request, helper, endpoint evidence, and durable artifact.

No historical/diagnostic D3 record receives acceptance credit.

Expected:

```text
D11=PASS
```

---

## 14. D12-D14 H1 lifecycle

Require exact ordering:

```text
Deferred
→ durable evidence checkpoint
→ RestoreOnly in finally
```

Require:

```text
D12 checkpoint completed before RestoreOnly
D13 RestoreOnlyCount=1
D14 settings restored exactly to pre-state
```

Restoration failure overrides otherwise-successful validation.

---

## 15. D15 secret hygiene

Require:

```text
qualification token not persisted
qualification token not printed
TwelveData__ApiKey not configured
no secret value in durable evidence/logs
```

Expected:

```text
D15=PASS
```

Normal root 503 from missing Twelve Data key remains an accepted WP05 downstream blocker, not a WP04 failure.

Do not configure the key.

---

## 16. D16 no-bypass carry-forward

Carry forward accepted no-bypass evidence and prove this execution introduces no contradiction:

```text
no SQLite shell
no Python DB manipulation
no Kudu /home
no second Azure-specific persistence implementation
```

Expected:

```text
D16=CARRY_FORWARD_PASS
```

---

## 17. D17 zero-cost architecture

Prove throughout:

```text
Linux App Service F1
West Central US
same custom Docker/public-free GHCR architecture
no paid resource
no Azure SQL
no Azure Files
no mandatory ACR
RecurringInfrastructureCost=$0.00
```

Expected:

```text
D17=PASS
```

---

## 18. D18 mutation boundary

D18 remains accepted but this authority must not contradict it.

At final state require:

```text
AuthorityIntroducedTrackedMutationCount=0
StagedPathCount=0
CommitCount=0
PushCount=0
PRCount=0
MergeCount=0
GitHubMutationCount=0
DockerBuildCount=0
GhcrPublicationCount=0
WP07LifecycleActionCount=0
```

Repository HEAD must remain:

```text
bf91221ec51baa18e878d2aad1184431ca235cdd
```

unless an external/user mutation occurs, in which case report it and stop rather than attributing it to Terra.

---

## 19. D19 durable evidence root

Create one fresh durable evidence root for this target-validation authority.

Retain/index:

```text
authority identity
clean-anchor preflight
wrapper/helper/frozen hashes
PowerShell/parser evidence
target pre-state
fresh RunIds
sanitized mutation ledger
qualification terminal output
HTTP poll observations
application-owned qualification record
archive/retrieval state
checkpoint evidence
RestoreOnly evidence
post-state
D01-D19 predicate records
carry-forward evidence references
secret hygiene
zero-cost/no-bypass evidence
repository invariants
manifest/index/hashes
```

Then independently reopen it.

Require:

```text
DurableReopen=PASS
PredicateCount=19
FailedPredicateCount=0
UnresolvedPredicateEvidenceReferenceCount=0
UnresolvedEvidenceReferenceCount=0
MissingRequiredEvidenceClassCount=0
```

---

## 20. Predicate schema

Every D01-D19 predicate record must contain:

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

No generic aggregate PASS is sufficient.

---

## 21. Evidence retrieval truth policy

Preserve:

```text
RETAINED + PASS|EMPTY                 eligible
RETAINED + FAIL|NOT_APPLICABLE       fail
RETRIEVAL_FAILED + NOT_APPLICABLE    eligible
other combinations                    inconsistent/fail
```

No Kudu `/home` retry.

Application-owned HTTP evidence remains the target evidence surface where required.

---

## 22. Bounded convergence

Evaluate **all D01-D19 and all final sections on every attempt**.

For defects correctable entirely within disposable local validation/evidence machinery:

```text
collect all defects
fix all in-scope disposable defects
rehash affected disposable artifacts
invalidate affected candidate evidence
consume failed RunIds
allocate fresh RunIds where required
create a fresh durable root
rerun the complete affected target cycle
reevaluate all predicates
```

Continue internally until PASS.

Do not return after each local defect.

### Stop boundary

STOP if correction requires:

```text
tracked/production source mutation
frozen runner mutation
new product semantics
image rebuild/publication
PR/merge
GitHub lifecycle mutation
WP07 lifecycle operation
paid Azure/resource change
Twelve Data secret
new Luna policy/architecture decision
```

On every post-mutation failure, attempt canonical H1 restoration before returning.

Do not blindly repeat external Azure lifecycle mutations outside the canonical qualification contract.

---

## 23. Final target/repository state

Require:

```text
temporary qualification settings restored exactly
target architecture unchanged
no WP07 lifecycle action
no secret disclosure
HEAD=bf91221ec51baa18e878d2aad1184431ca235cdd
working tree clean
staged paths=0
git diff --check=PASS
wrapper/helper clean-HEAD provenance still PASS
```

---

## 24. Final exhaustive reconciliation

Evaluate:

```text
A CleanAnchorAndIdentity
B AzurePreState
C GovernedUpdate
D FidelityAndIdempotency
E ConflictCarryForward
F PersistenceIdentityIntegrity
G RunIdAttribution
H CheckpointAndRestoration
I SecretHygieneNoBypass
J ZeroCostArchitecture
K MutationBoundary
L DurableEvidenceReopen
M FinalTargetAndRepositoryState
```

All A-M must PASS.

---

## 25. Success markers

Only on complete success:

```text
RELEASE 1.12 WP04 — CLEAN-ANCHOR TARGET VALIDATION: PASS
RELEASE 1.12 WP04 — EXECUTION ANCHOR: bf91221ec51baa18e878d2aad1184431ca235cdd
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
RELEASE 1.12 WP04 — PR/MERGE: DEFERRED_UNTIL_FINAL_ACCEPTANCE
RELEASE 1.12 WP04 — WP04 #263: OPEN
RELEASE 1.12 WP04 — PUBLICATION: FINAL_PUBLICATION_NOT_AUTHORIZED
RELEASE 1.12 WP04 — WP05: NOT_STARTED
RELEASE 1.12 WP04 — WP07: NOT_STARTED
```

Terra must not claim final WP04 acceptance.

---

## 26. Required handoff

Return:

```text
ExecutionAnchor
RemoteReleaseBranchTip
WorkingTreeClean
StagedPathCount
GitDiffCheckResult

WindowsPowerShellVersion
WrapperParserErrorCount
HelperParserErrorCount
RunnerSHA256
WrapperSHA256
HelperSHA256
WrapperCleanHeadResult
HelperCleanHeadResult

TargetValidationRoot
AllocatedRunIds
ConsumedFailedRunIds

TargetIdentity
AzureRegion
AppServiceSku
ImageIdentity
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
SectionMResult

BoundaryBlocker
FinalAcceptance
NextAuthorizedAction
```

On success:

```text
FinalAcceptance=READY_FOR_FINAL_LUNA
NextAuthorizedAction=GPT-5.6 Luna final read-only WP04 substantive acceptance reconciliation; final PR/merge/lifecycle remain unauthorized
```

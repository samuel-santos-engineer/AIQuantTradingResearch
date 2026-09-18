# Release 1.12 WP04 — Terra Combined Azure Final Substantive Validation

**Selected execution model: GPT-5.6 Terra**

## Mission

Execute one governed Azure target-validation authority that closes, if evidence supports it, the four remaining substantive WP04 gates:

```text
GovernedDataUpdate
FidelityIdempotencyConflictSemantics
RestartRedeployPersistence
Recovery
```

This is **one governance boundary and one convergence authority**, not one authority per defect.

The accepted hybrid W1–W8 chain is frozen carry-forward evidence and MUST NOT be rerun.

This authority may perform only the Azure lifecycle/settings mutations explicitly necessary for the canonical validation, with exact pre-state capture and restoration. It does **not** authorize source changes, image publication, PR/merge, GitHub issue/project/milestone mutations, or WP05.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract/policy/architecture/acceptance/governance/read-only reconciliation

GPT-5.6 Terra
  governed implementation/validation execution and explicitly authorized mutations

GPT-5.6 Sol
  supporting analysis only; never substitutes for Luna/Terra
```

Selected model: **GPT-5.6 Terra**.

---

## 2. Accepted predecessor boundary

Treat as accepted:

```text
PersistentInitialization=PASS
Integrity=PASS
SchemaV4=PASS
DeleteJournal=PASS
EvidenceReuse=PASS
SecretHygiene=PASS
ZeroCostNoBypass=PASS
RequiredValidations=PASS
HybridW1W8=ACCEPTED
```

Still substantive and unproven:

```text
GovernedDataUpdate
FidelityIdempotencyConflictSemantics
RestartRedeployPersistence
Recovery
```

Pending governance only:

```text
PRMergePostMerge
IssueProjectLifecycle
```

Current sequencing:

```text
WP04FullAcceptanceEvidenceState=NOT_READY
PublicationAuthorizationState=NOT_AUTHORIZED
WP05=NOT_STARTED
```

Do not reopen accepted gates unless this execution produces contradictory evidence.

---

## 3. Fixed architecture and constraints

Preserve:

```text
Azure App Service Linux F1
West Central US
custom Docker
public/free GHCR
persistent /home
SQLite database=/home/data/aiquant.db
SQLite journal=DELETE
schema v4
strict $0 reference/demo
application-owned persistence semantics
HTTP qualification evidence endpoint
no ephemeral DB fallback
no Azure SQL
no Azure Files
no mandatory ACR
no direct SQL/Python deployment bypass
no Twelve Data secret configuration in WP04
```

Normal root 503 caused by missing `TwelveData__ApiKey` is an accepted downstream WP05 configuration blocker and is NOT a WP04 failure.

Do not configure that secret.

---

## 4. Source/image invariants

No image rebuild or publication is required or authorized.

Governed deployed image digest remains expected:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Wrapper provenance source anchor:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Wrapper expected SHA256:

```text
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

Frozen runner SHA256:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Do not change source to make target validation pass.

---

## 5. Windows PowerShell compatibility

All PowerShell execution must target exactly:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7 assumptions.

Require parser error count `0` for every governed script used.

---

## 6. Recover exact target-validation contract before mutation

Before allocating any new RunId or mutating Azure, recover the exact canonical mechanics from retained WP04 authorities/evidence for:

```text
application-owned qualification mode
D3 qualification record
HTTP evidence endpoint
H1 Deferred / RestoreOnly lifecycle
RB2 helper-owned actual RunId
archive/evidence retrieval policy
accepted Azure target identity
governed data-update semantics
idempotency/conflict semantics
restart semantics
redeploy semantics
recovery/restoration semantics
```

Print:

```text
RecoveredTargetValidationContract
RecoveredContractSources
TargetIdentity
CurrentImageIdentity
RequiredQualificationSettings
RequiredLifecyclePhases
RequiredEvidenceFields
```

If any acceptance-critical semantic is ambiguous or conflicting, STOP before Azure mutation:

```text
TARGET_VALIDATION_CONTRACT=NOT_PROVEN
```

Do not invent a new policy.

---

## 7. Pre-mutation target snapshot

Capture durable, sanitized, read-only pre-state before the first mutation.

At minimum:

```text
target identity
resource group identity
region
App Service plan/SKU
web app state
configured container image identity
relevant six-setting pre-state used by H1
qualification settings presence/absence
WEBSITES_PORT
SCM basic auth state
FTP state
persistent storage-related configuration
current deployment/container identity
current time
repository HEAD/branch/status
tracked changed paths
staged paths
git diff --check
```

Do not persist secret values.

For secret-bearing settings, record only safe presence/absence or sanitized identity as canonically allowed.

Require:

```text
PreStateCapture=PASS
SecretHygiene=PASS
```

---

## 8. Fresh identity discipline

Every governed lifecycle phase requiring a qualification identity must use a fresh never-used RunId generated/owned according to the accepted RB2 helper contract.

Never reuse any historical WP04 RunId.

At minimum preserve distinct correlation for:

```text
baseline/update phase
idempotent-repeat phase
conflict/fidelity phase if canonically separate
post-restart phase
post-redeploy phase
recovery/restoration verification phase
```

If canonical mechanics permit fewer RunIds because one application-owned record safely proves multiple assertions, follow the recovered contract rather than inventing extra calls.

Every allocated RunId is permanently consumed, including failed attempts.

---

## 9. GovernedDataUpdate gate

Prove a governed target update through the **application-owned** WP04 qualification/update surface.

No direct SQLite shell.
No Python direct DB manipulation.
No Kudu `/home` retrieval.
No deployment-side SQL bypass.

The evidence must establish, as required by the canonical contract:

```text
target-attributable RunId
DatabasePathIdentity=aiquant.db
SchemaVersion=4
JournalMode=delete
AcceptedEvidenceIdentity
AcceptedEvidenceCount
IntegrityCheck=ok
QuickCheck=ok
PersistenceContinuity=true where applicable
```

Prove the update was accepted by the domain/application semantics rather than merely that bytes appeared in SQLite.

Return:

```text
GovernedDataUpdateResult=PASS|FAIL
GovernedUpdateRunId
GovernedUpdateEvidenceReference
```

---

## 10. Fidelity / idempotency / conflict semantics

Use the canonical application-owned update semantics to prove all required behavior.

At minimum recover and prove:

### Fidelity

The accepted target record/evidence identity and semantic payload remain faithful after durable reopen.

### Idempotency

Repeating the canonical same update does not create an incorrect duplicate or alter accepted semantics.

Prove the canonical expected accepted-evidence count/identity behavior.

### Conflict semantics

Exercise the canonical conflict case, if the contract requires one, and prove the application/domain layer produces the expected rejection/non-overwrite behavior.

Do not manufacture a conflict rule not present in the accepted contract.

Retain before/after application-owned evidence sufficient to independently verify all three.

Return:

```text
FidelityResult
IdempotencyResult
ConflictSemanticsResult
FidelityEvidenceReference
IdempotencyEvidenceReference
ConflictEvidenceReference
```

All three must PASS for the acceptance class to PASS.

---

## 11. Restart persistence

After an accepted governed update has durable identity, perform the canonical Azure **restart** lifecycle action.

Do not add an explicit restart if the canonical lifecycle operation itself already supplies the required restart; preserve the accepted M1 rule against redundant restart.

After target recovery, use a fresh qualification/evidence correlation and prove:

```text
same persistent database identity
same accepted semantic evidence identity/count as canonically expected
schema v4
journal delete
integrity ok
quick-check ok
persistence continuity true
```

Evidence must be target-attributable.

Return:

```text
RestartLifecycleAction
PostRestartRunId
PostRestartPersistenceResult
PostRestartEvidenceReference
```

---

## 12. Redeploy persistence

Perform the minimum canonical Azure redeploy/recycle operation that qualifies as `redeploy` under the recovered WP04 contract.

Constraints:

```text
same already-published image identity
no image rebuild
no GHCR publication
no source mutation
no paid resource
no architecture change
```

After redeploy recovery, use fresh correlation and prove the same persistence/semantic continuity fields.

Return:

```text
RedeployLifecycleAction
PostRedeployRunId
PostRedeployPersistenceResult
PostRedeployEvidenceReference
ImageIdentityBefore
ImageIdentityAfter
```

Require image identity continuity unless the recovered contract explicitly says otherwise.

---

## 13. Recovery gate

Prove the canonical target recovery lifecycle and restoration behavior.

This must distinguish:

```text
application persistence recovery
Azure lifecycle recovery
qualification evidence retrieval
configuration restoration
```

Use the accepted H1 ordering:

```text
Deferred
→ durable evidence checkpoint
→ RestoreOnly in finally
```

Preserve restoration-failure precedence.

Do not use blind restart/logging retries.

Do not use Kudu `/home`.

Do not alter registry credentials.

Prove:

```text
target returns to the canonical pre-authority configuration state
temporary qualification settings are removed/restored exactly
durable database continuity remains valid
restoration result is explicit
no secret is exposed
```

Return:

```text
RecoveryResult
ConfigurationRestorationResult
DatabaseContinuityAfterRecovery
RecoveryEvidenceReference
```

---

## 14. HTTP evidence endpoint

Where the canonical target-validation contract uses the application-owned endpoint, preserve:

```text
GET /internal/wp04/persistence-qualification
runId=<exact fresh run id>
X-WP04-Evidence-Token
PersistentSqliteQualification__HttpEvidenceEnabled=true
```

Token requirements:

```text
temporary
never persisted
never printed
never included in durable evidence
```

Poll behavior remains bounded according to the accepted helper contract.

Do not weaken HTTP/evidence authentication.

---

## 15. Evidence retrieval policy

Preserve accepted archive policy:

```text
RETAINED + PASS|EMPTY                 eligible
RETAINED + FAIL|NOT_APPLICABLE       fail
RETRIEVAL_FAILED + NOT_APPLICABLE    eligible
other combinations                    inconsistent/fail
```

No retry of Kudu `/home`.

A target-attributable application-owned evidence record is required for acceptance credit.

Diagnostic/candidate-only evidence receives no acceptance credit.

---

## 16. Integrity/schema/journal regression checks

Although these classes are already accepted, the combined Azure execution must prove no regression at each target lifecycle checkpoint:

```text
IntegrityCheck=ok
QuickCheck=ok
SchemaVersion=4
JournalMode=delete
DatabasePathIdentity=aiquant.db
```

Any contradiction is a governance blocker; do not conceal it behind prior PASS evidence.

---

## 17. Zero-cost / no-bypass regression checks

Prove throughout:

```text
App Service plan remains Linux F1
region remains West Central US
no paid service created
no Azure SQL
no Azure Files
no ACR requirement introduced
same public/free GHCR model
no direct SQLite bypass
no Twelve Data secret configured
```

Expected recurring infrastructure cost must remain:

```text
$0.00
```

No resource creation is authorized.

---

## 18. Mutation authority

Authorized Azure mutations are only those strictly required by the recovered validation contract, such as:

```text
temporary qualification App Settings
canonical restart/redeploy lifecycle action
canonical restoration of captured settings/state
```

Every mutation must be recorded:

```text
MutationId
Operation
Target
Before
After
Reason
Result
Restored
EvidenceReference
```

Do not count read-only calls as mutations.

Do not attribute user/manual mutations to Terra.

---

## 19. Repository and external mutation boundaries

Require throughout and at end:

```text
tracked production/source mutation introduced by authority=0
README mutation=0
staged paths=0
commit=0
push=0
PR creation=0
merge=0
GitHub issue/project/milestone mutation=0
Docker build=0
GHCR publication=0
Twelve Data secret configuration=0
```

The two pre-existing WP04 tracked modifications may remain but must not be changed by this authority.

---

## 20. Convergence behavior

Evaluate **all** gates on every attempt.

For defects correctable only in disposable local validation/evidence machinery:

```text
collect all defects
fix all authorized disposable defects
rehash changed disposable artifacts
invalidate affected evidence
consume failed RunIds
allocate fresh RunIds as required
restart the complete affected target-validation cycle
```

Continue internally until PASS.

Do not return one local harness defect at a time.

### Absolute stop boundaries

STOP if correction requires:

```text
production/tracked source mutation
frozen runner mutation
image rebuild/publication
new architecture/policy decision
paid Azure resource
new Azure resource
Twelve Data secret
GitHub mutation
PR/merge
WP05
```

Also STOP on a real target contradiction that cannot be corrected solely by restoring the explicitly authorized temporary Azure validation state.

Report all known defects exhaustively.

---

## 21. Failure restoration

Whether validation PASSes or FAILs, attempt canonical restoration of all temporary Azure validation state.

Restoration failure has precedence over otherwise-successful validation.

Do not report success if final target state is not proven restored.

Retain durable evidence before restoration where required by H1, then complete restoration and final evidence.

---

## 22. Final durable evidence root

Create a fresh durable root for this combined target-validation authority.

It must retain/index:

```text
recovered contract + sources
target identity
pre-state snapshot
all fresh RunIds
all helper/script hashes
mutation ledger
governed update evidence
fidelity evidence
idempotency evidence
conflict evidence
pre-restart continuity evidence
post-restart evidence
pre-redeploy continuity evidence
post-redeploy evidence
recovery/restoration evidence
schema/journal/integrity evidence
zero-cost/no-bypass evidence
secret-hygiene evidence
external call ledger
repository invariants
post-state snapshot
all individual acceptance predicates
manifest/index/hashes
durable reopen result
```

After finalization reopen the durable root and resolve every evidence reference.

Require:

```text
DurableReopen=PASS
UnresolvedEvidenceReferences=0
MissingRequiredEvidenceClasses=0
```

---

## 23. Individual acceptance predicates

Construct an explicit predicate matrix for this authority.

At minimum include individual predicates for:

```text
PowerShell/parser
source/helper/frozen identities
pre-state capture
target identity
image identity
governed update
fidelity
idempotency
conflict semantics
pre-restart continuity
post-restart continuity
pre-redeploy continuity
post-redeploy continuity
recovery
configuration restoration
database continuity after recovery
schema v4 at each required phase
DELETE journal at each required phase
integrity/quick-check at each required phase
evidence retrieval validity
secret hygiene
zero-cost
no bypass
authorized mutation accounting
repository invariants
final target-state restoration
durable reopen
all evidence references resolved
```

Each predicate:

```text
PredicateId
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

## 24. Final self-reconciliation

After durable reopen, evaluate all sections even after failures:

```text
A ContractAndIdentity
B GovernedDataUpdate
C FidelityIdempotencyConflict
D RestartPersistence
E RedeployPersistence
F RecoveryAndRestoration
G IntegritySchemaJournal
H EvidenceDurabilityAndReuse
I SecretHygieneZeroCostNoBypass
J MutationAndRepositoryBoundary
K FinalTargetState
```

Only A-K ALL_PASS permits an acceptance candidate.

---

## 25. Required success markers

Only if all predicates and A-K PASS:

```text
RELEASE 1.12 WP04 — COMBINED AZURE SUBSTANTIVE VALIDATION: PASS
RELEASE 1.12 WP04 — GOVERNED DATA UPDATE: PASS
RELEASE 1.12 WP04 — FIDELITY: PASS
RELEASE 1.12 WP04 — IDEMPOTENCY: PASS
RELEASE 1.12 WP04 — CONFLICT SEMANTICS: PASS
RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS
RELEASE 1.12 WP04 — AZURE REDEPLOY PERSISTENCE: PASS
RELEASE 1.12 WP04 — RECOVERY: PASS
RELEASE 1.12 WP04 — CONFIGURATION RESTORATION: PASS
RELEASE 1.12 WP04 — INTEGRITY: PASS
RELEASE 1.12 WP04 — SCHEMA V4: PASS
RELEASE 1.12 WP04 — SQLITE DELETE JOURNAL: PASS
RELEASE 1.12 WP04 — EVIDENCE REFERENCES: ALL_RESOLVED
RELEASE 1.12 WP04 — DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — ZERO COST / NO BYPASS: PASS
RELEASE 1.12 WP04 — AUTHORITY TRACKED SOURCE MUTATIONS: 0
RELEASE 1.12 WP04 — GITHUB MUTATIONS: 0
RELEASE 1.12 WP04 — FINAL TARGET STATE: RESTORED
RELEASE 1.12 WP04 — SUBSTANTIVE ACCEPTANCE CANDIDATE: READY_FOR_FINAL_LUNA
RELEASE 1.12 WP04 — PUBLICATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — WP05: NOT_STARTED
```

Do not claim final WP04 acceptance under Terra.

---

## 26. Required handoff

Return:

```text
RecoveredTargetValidationContract
RecoveredContractSources
TargetIdentity
AzureRegion
AppServiceSku
CurrentImageIdentity

CombinedValidationRoot
AllocatedRunIds
ConsumedFailedRunIds

RunnerSHA256
WrapperSourceSHA256
HelperSHA256
ValidatorSHA256
WindowsPowerShellVersion
ParserErrorCount

GovernedDataUpdateResult
GovernedUpdateRunId
GovernedUpdateEvidenceReference

FidelityResult
IdempotencyResult
ConflictSemanticsResult
FidelityEvidenceReference
IdempotencyEvidenceReference
ConflictEvidenceReference

RestartLifecycleAction
PostRestartRunId
PostRestartPersistenceResult
PostRestartEvidenceReference

RedeployLifecycleAction
PostRedeployRunId
PostRedeployPersistenceResult
PostRedeployEvidenceReference
ImageIdentityBefore
ImageIdentityAfter

RecoveryResult
ConfigurationRestorationResult
DatabaseContinuityAfterRecovery
RecoveryEvidenceReference

IntegrityResult
SchemaV4Result
DeleteJournalResult
EvidenceReuseResult
SecretHygieneResult
ZeroCostNoBypassResult

AuthorizedAzureMutationCount
MutationLedger
FinalTargetStateRestored

AuthorityIntroducedTrackedMutationCount
StagedPathCount
GitDiffCheckResult
GitHubMutationCount
DockerBuildCount
GhcrPublicationCount
TwelveDataSecretConfigured

PredicateCount
PassedPredicateCount
FailedPredicateCount
UnresolvedPredicateEvidenceReferenceCount

DurableReopenResult
UnresolvedEvidenceReferenceCount
MissingRequiredEvidenceClassCount

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

BoundaryBlocker
FinalAcceptance
NextAuthorizedAction
```

On complete success:

```text
FinalAcceptance=READY_FOR_FINAL_LUNA
NextAuthorizedAction=GPT-5.6 Luna final read-only substantive WP04 acceptance reconciliation; publication/lifecycle remain unauthorized
```

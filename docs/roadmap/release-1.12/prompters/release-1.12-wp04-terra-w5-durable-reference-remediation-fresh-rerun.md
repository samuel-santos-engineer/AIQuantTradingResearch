# Release 1.12 WP04 — Terra W5 Durable-Reference Remediation + Fresh Rerun

**Selected execution model: GPT-5.6 Terra**

## Mission

Repair the sole hybrid-chain blocker: W5's durable P01–P04 references. Then perform a **fresh W5 rerun** with a new single-use RunId and complete durable self-reconciliation.

Do not return after merely creating `metadata` or `hashes` files. Continue internally until the fresh W5 root independently proves all P01–P20 predicates with zero unresolved references, or until a true governance boundary is encountered.

W6, W7, and W8 are already accepted and MUST NOT be rerun or mutated.

---

## 1. Reconciled blocker

Final W8 reconciliation established:

```text
W8 A-F=PASS
W8Result=PASS
G HybridChainIntegrity=FAIL
H WP04CoverageTruthfulness=PASS
I GovernanceSequencing=NOT_PROVEN

HybridW1W8Result=BLOCKED
FinalAcceptance=NOT_GRANTED
PublicationAuthorizationState=NOT_AUTHORIZED
```

Sole hybrid blocker:

```text
W5Root=
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\w5\42cdd0171dae4757b2b3dec8ec9944d0

W5PredicateCount=20
W5PredicatePassCount=20
W5UnresolvedEvidenceReferenceCount=4

P01 -> metadata
P02 -> metadata
P03 -> hashes
P04 -> hashes
```

The retained W5 root has no corresponding durable `metadata` or `hashes` evidence files.

Therefore the old W5 aggregate cannot supply independent hybrid-chain acceptance.

This is an evidence-retention defect, not a production-behavior defect.

---

## 2. Historical W5 identity discipline

The old accepted-candidate W5 RunId is permanently consumed:

```text
initialize-ee04814e110848b28095069cd0a009e1
```

Also retain all earlier historical W5 RunIds as consumed. Never reuse any.

A byte-changing disposable evidence fix requires:

```text
fresh disposable hashes
fresh W5 durable root
fresh never-used W5 RunId
complete W5 rerun
```

Do not patch the old W5 root and claim fresh execution acceptance.

---

## 3. Canonical W5 contract — unchanged

Recover and verify the canonical W5 contract from retained governing sources before the fresh attempt.

The accepted W5 scenario is expected to remain:

```text
ArchiveRetrieval=RETRIEVAL_FAILED
FreshExtraction=NOT_APPLICABLE
EvidenceCheckpoint=PASS
FinalLifecycle=SUCCESS
RestoreOnlyCount=1
RealExternalCalls=0
```

Do not broaden or alter W5 semantics.

If the retained governing sources conflict, stop before allocating a fresh W5 RunId:

```text
W5_CONTRACT_RECOVERY=NOT_PROVEN
```

---

## 4. Authorized scope

Authorized byte-changing scope:

```text
disposable/local-only W5 runtime harness
disposable/local-only W5 evidence serializer
disposable/local-only W5 validator
disposable child/interception probe only if strictly required
```

Forbidden:

```text
frozen runner mutation
tracked/production source mutation
production wrapper/helper mutation
W6/W7/W8 rerun or mutation
architecture/policy change
real external invocation
Azure call/mutation
GitHub call/mutation
Docker/GHCR call/mutation
Twelve Data call/configuration
secret-contract change
README mutation
publication
PR creation
WP04 issue/project/milestone lifecycle
WP05
staging/commit/push
```

---

## 5. P01/P02 durable metadata evidence

The fresh W5 durable root must contain an actual durable metadata record/file that survives finalization and reopen.

At minimum:

```text
RecordId
WindowsPowerShellVersion
ParserErrorCount
ParserTargets
ObservationPhase
Result
```

Require:

```text
WindowsPowerShellVersion=5.1.26100.9444
ParserErrorCount=0
```

P01 and P02 must reference this retained evidence by a path/key that resolves from the finalized W5 root after reopen.

Do not use a symbolic `metadata` reference unless a corresponding durable artifact actually exists and resolves.

---

## 6. P03/P04 durable hash evidence

The fresh W5 root must contain an actual durable hash record/file:

```text
RecordId
WrapperSourceIdentity
WrapperSourceSHA256
WrapperCopyIdentity
WrapperCopyPreSHA256
WrapperCopyPostSHA256
SourceEqualsPre
PreEqualsPost
ObservationPhase
Result
```

Canonical wrapper SHA:

```text
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

Require:

```text
SourceEqualsPre=true
PreEqualsPost=true
```

P03 and P04 must reference retained hash evidence that resolves after reopen.

---

## 7. Preserve complete W5 evidence accounting

Do not regress the already-correct W5 accounting.

Fresh root must again retain exactly 20 individual P01–P20 records, including:

```text
P01 WinPS
P02 parser
P03 source/pre exact byte identity
P04 pre/post exact byte identity
P05 wrapper execution complete
P06 ArchiveRetrieval RETRIEVAL_FAILED
P07 FreshExtraction NOT_APPLICABLE
P08 checkpoint PASS
P09 final SUCCESS
P10 RestoreOnly=1
P11 real external calls=0
P12 governed interception ledger
P13 repository scope unchanged
P14 staged=0
P15 git diff --check
P16 secret hygiene
P17 durable sanitized evidence
P18 complete durable W5 ledger
P19 post-cleanup cleanup PASS
P20 error precedence PASS
```

Every record must contain:

```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
ObservationPhase
ReferenceResolved
```

Require after reopen:

```text
PredicateCount=20
Passed=20
Failed=0
UnresolvedPredicateEvidenceReferences=0
```

---

## 8. Preserve full W5 interception evidence

Retain the complete approved/negative/escape/interception evidence required by the previously accepted W5 contract, including child inheritance where applicable.

Require no regression from the prior corrected W5 harness:

```text
RealExternalCalls=0
UnexpectedExternalCalls=0
all approved governed shapes accepted
all negative/escape shapes rejected
child interception inheritance proven
```

Do not substitute the later W6/W7/W8 evidence for W5's own durable evidence.

---

## 9. Preflight before fresh RunId

Before allocating the fresh W5 RunId require:

```text
Windows PowerShell=5.1.26100.9444
ParserErrorCount=0
frozen runner hash exact
production wrapper/helper bytes unchanged
accepted W6/W7/W8 durable roots readable and unchanged
tracked modified scope remains the same two pre-existing WP04 paths
StagedPaths=0
GitDiffCheck=PASS
RealExternalCalls=0
```

The two pre-existing tracked paths must be derived from Git:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

---

## 10. Fresh W5 execution

Allocate exactly one fresh never-used W5 RunId only after all pre-RunId gates pass.

Execute the byte-exact production wrapper under the canonical governed local W5 interception environment.

Require production-derived observations:

```text
ArchiveRetrieval=RETRIEVAL_FAILED
FreshExtraction=NOT_APPLICABLE
EvidenceCheckpoint=PASS
FinalLifecycle=SUCCESS
RestoreOnlyCount=1
WrapperExitCode=0
RealExternalCalls=0
UnexpectedExternalCalls=0
```

No real external invocation is authorized.

---

## 11. Durable finalization and reopen

The final W5 root must retain/index at least:

```text
canonical W5 contract + sources
fresh RunId
artifact hashes
actual metadata evidence file/record
actual hashes evidence file/record
authority-entry repository state
20 predicates
lifecycle observations
RestoreOnly evidence
governed interception/call matrix
child inheritance evidence
secret hygiene
cleanup
error precedence
post-run repository state
manifest/index/hashes
reopen validation
```

After reopen explicitly resolve:

```text
P01 EvidenceReference
P02 EvidenceReference
P03 EvidenceReference
P04 EvidenceReference
P05-P20 EvidenceReferences
```

Require:

```text
UnresolvedPredicateEvidenceReferences=0
UnresolvedEvidenceReferences=0
MissingRequiredEvidenceClasses=0
DurableReopen=PASS
```

---

## 12. Cleanup ordering

Preserve required evidence before deleting disposable sandbox state.

Require:

```text
wrapper complete
→ required evidence preserved durably
→ sandbox cleanup attempted
→ sandbox cleanup complete
→ P19 evaluated
→ final ledger/index serialized
→ durable root reopened
→ all references resolved
```

---

## 13. Mutation/hygiene invariants

Require:

```text
RealExternalCalls=0
UnexpectedExternalCalls=0
ExternalMutations=0
AuthorityIntroducedTrackedProductionMutations=0
StagedPaths=0
GitDiffCheck=PASS
SecretHygiene=PASS
README mutation=0
```

W6/W7/W8 retained evidence must remain unchanged.

---

## 14. Single-converge retry loop

For every defect correctable entirely within disposable W5 harness/evidence scope:

```text
evaluate ALL W5 gates
collect ALL defects
repair ALL local defects
rehash
consume any allocated failed W5 RunId
fresh root
fresh never-used RunId after pre-RunId gates
rerun complete W5
cleanup/finalize/reopen
resolve ALL references
self-reconcile ALL gates
```

Repeat internally until ALL_PASS.

Do not return for another local missing-file/reference/serializer/accounting defect.

---

## 15. Final self-reconciliation

Before returning, independently reopen the fresh W5 root and evaluate all:

```text
A ContractRecovery
B IdentityFreshness
C P01-P20
D LifecycleSemantics
E RuntimeInterception
F DurableEvidenceAndReopen
G BoundaryMutationHygiene
H LaterAcceptedEvidencePreservation
```

Require all A-H PASS.

### H requires

```text
W6 accepted root unchanged
W7 accepted root unchanged
W8 accepted root unchanged
no W6/W7/W8 rerun
no later accepted RunId reused
```

---

## 16. Stop boundaries

STOP if completion requires:

```text
frozen runner mutation
tracked/production source mutation
new architecture/policy decision
real external invocation
Azure/GitHub/Docker/GHCR/Twelve Data action
W6/W7/W8 rerun
secret-contract change
README mutation
publication
PR creation
WP04 issue/project/milestone lifecycle
WP05
staging/commit/push
```

Report all blockers.

---

## 17. Success markers

Only after fresh W5 + reopen + A-H ALL_PASS:

```text
RELEASE 1.12 WP04 — W5 DURABLE-REFERENCE REMEDIATION: PASS
RELEASE 1.12 WP04 — W5 FRESH RERUN: PASS
RELEASE 1.12 WP04 — W5 P01-P20: 20/20 PASS
RELEASE 1.12 WP04 — W5 P01-P04 DURABLE REFERENCES: ALL_RESOLVED
RELEASE 1.12 WP04 — W5 DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — W5 REAL EXTERNAL CALLS: 0
RELEASE 1.12 WP04 — W5 TRACKED PRODUCTION MUTATIONS: 0
RELEASE 1.12 WP04 — W5 EXTERNAL MUTATIONS: 0
RELEASE 1.12 WP04 — W6/W7/W8 ACCEPTED EVIDENCE: PRESERVED
RELEASE 1.12 WP04 — W5 ACCEPTANCE CANDIDATE: READY_FOR_FINAL_LUNA
RELEASE 1.12 WP04 — HYBRID W1-W8: READY_FOR_RECONCILIATION
RELEASE 1.12 WP04 — PUBLICATION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — WP04 #263: OPEN
RELEASE 1.12 WP04 — MILESTONE #63: OPEN
RELEASE 1.12 WP04 — WP05: NOT_STARTED
```

Do not claim final hybrid or WP04 acceptance under Terra authority.

---

## 18. Required handoff

Return:

```text
RecoveredW5Contract
RecoveredW5ContractSources

W5RetryAttemptCount
ConsumedW5RunIds
FreshCandidateW5RunId
FreshW5DurableRoot

RunnerSHA256
RuntimeHarnessSHA256
ValidatorSHA256
ChildProbeSHA256
WrapperSourceSHA256
WrapperCopyPreSHA256
WrapperCopyPostSHA256

WindowsPowerShellVersion
ParserErrorCount

P01EvidenceReference
P02EvidenceReference
P03EvidenceReference
P04EvidenceReference
P01ReferenceResolved
P02ReferenceResolved
P03ReferenceResolved
P04ReferenceResolved

W5PredicateCount
W5PassedPredicateCount
W5FailedPredicateCount
W5UnresolvedPredicateEvidenceReferenceCount

ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
WrapperExitCode

DurableReopenResult
UnresolvedEvidenceReferenceCount
MissingRequiredEvidenceClassCount

RealExternalCallCount
UnexpectedExternalCallCount
ExternalMutationCount
AuthorityIntroducedTrackedMutationCount
StagedPathCount
GitDiffCheckResult
SecretHygieneResult
CleanupResult
ErrorPrecedenceResult

W6EvidencePreserved
W7EvidencePreserved
W8EvidencePreserved

SectionAResult
SectionBResult
SectionCResult
SectionDResult
SectionEResult
SectionFResult
SectionGResult
SectionHResult

BoundaryBlocker
NextAuthorizedAction
```

On complete success:

```text
NextAuthorizedAction=GPT-5.6 Luna final read-only W5 + hybrid W1-W8 reconciliation; publication/lifecycle remain unauthorized
```

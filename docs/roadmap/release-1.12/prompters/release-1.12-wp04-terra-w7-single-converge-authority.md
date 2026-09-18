# Release 1.12 WP04 — Terra W7 Single-Converge Authority

**Selected execution model: GPT-5.6 Terra**

## Mission

Execute canonical WP04 hybrid **W7** from the now-finally-accepted W6 restart point.

Use one message-efficient convergence cycle:

```text
recover canonical W7 contract
→ evaluate ALL W7 gates
→ collect ALL local defects
→ repair ALL authorized disposable defects
→ invalidate affected evidence
→ fresh hashes/root/attempt
→ rerun complete W7
→ durable reopen
→ self-reconcile ALL gates
→ repeat internally until ALL_PASS
```

Do not return after the first locally correctable defect.

Do not execute W8.

---

## 1. Accepted predecessor boundary

W6 final Luna reconciliation is authoritative:

```text
W6RunId=
initialize-a440137eb7c8468189a68f771d901f96

W6DurableRoot=
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\w6\ba8f79dfa8134d74afe51424a301bd2f

RunnerSHA256=
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

RuntimeHarnessSHA256=
D42D8EADBECCF36332FBAFAE2186AF6496A8E9141E34498E67716ED4E9FD8224

ValidatorSHA256=
0AD15BCC30E1AF5010903E43707A5A948D3AC0F5291D9047EF151A1C9653D281

ChildProbeSHA256=
9199F6660EA1EE54FE3213E124FA96356DC7182816D323B6AD60F170B6F9ADB5

WrapperSourceSHA256=
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C

WrapperCopySHA256=
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

Final W6:

```text
A-G=PASS
P01-P20=20/20 PASS
UnresolvedPredicateEvidenceReferences=0
DurableReopen=PASS
QualificationResult=FAILURE
ArchiveRetrieval=RETRIEVAL_FAILED
FreshExtraction=NOT_APPLICABLE
EvidenceCheckpoint=PASS
FinalLifecycle=FAILURE
RestoreOnlyCount=1
S2CallbackConfigured=false
S2CallbackInvocationCount=0
RealExternalCalls=0
UnexpectedExternalCalls=0
TrackedProductionMutations=0
ExternalMutations=0
StagedPaths=0
```

W1-W6 are accepted or authorized carry-forward.

All prior W5/W6 RunIds remain permanently consumed. Do not reuse any historical RunId.

---

## 2. Recover canonical W7 contract before execution

The exact W7 scenario details are not to be invented by this authority.

Before any W7 attempt identity/RunId allocation, independently recover the canonical W7 contract from retained project-local WP04 governing artifacts and durable validation material, including the existing hybrid W1-W8 plan/fixtures.

At minimum search/reconcile:

```text
release-1.12-wp04-terra-hybrid-revised-v2-validation-authority.md
release-1.12-wp04-terra-exact-byte-sandbox-validation-authority.md
initialize-qualification.ps1 W7 fixture/contract
retained W1-W8 hybrid scenario definitions
retained accepted S2 checkpoint-seam authority/evidence
```

Print:

```text
RecoveredW7Contract
RecoveredW7ContractSources
RecoveredW7ExpectedLifecycle
RecoveredW7ExpectedS2Behavior
RecoveredW7AcceptancePredicates
```

Require one unambiguous canonical contract.

### Critical known W7 architectural constraint

W7 is the scenario historically associated with the **isolated S2 checkpoint callback fault-injection seam**.

That fact may be used to locate/reconcile the canonical contract, but **do not infer missing expected lifecycle/error-precedence semantics from this sentence**. Recover them from retained authority/fixture bytes.

If canonical sources conflict or exact W7 expected observations cannot be recovered:

```text
W7_CONTRACT_RECOVERY=NOT_PROVEN
```

STOP before allocating an attempt identity. Enumerate all conflicts/missing evidence. Do not guess.

---

## 3. Authority scope

Authorized byte-changing scope is strictly disposable/local validation material:

```text
W7 disposable runtime harness
W7 disposable validator/evidence serializer
disposable child/interception probe if W7 requires it
other disposable local harness material strictly required to converge W7
```

Forbidden:

```text
frozen runner mutation
tracked/production source mutation
production wrapper/helper mutation
architecture/policy change
real external call
Azure mutation/call
GitHub mutation/call
Docker/GHCR mutation/call
Twelve Data call/configuration
secret-contract change
W8
publication
WP04 lifecycle
staging
commit
push
README mutation
```

The production S2 seam is already accepted. W7 may exercise it only through the canonical isolated local validation mechanism; this authority does not authorize changing it.

---

## 4. Preflight before W7 identity allocation

Require:

```text
Windows PowerShell=5.1.26100.9444
parser errors=0
frozen runner SHA exact
accepted W6 root readable and unchanged
accepted W5 root readable and unchanged
production wrapper source hash exact
production helper/source bytes unchanged from accepted boundary
tracked modified scope remains exactly the two pre-existing WP04 paths
staged paths=0
git diff --check=PASS
real external calls=0
```

The two pre-existing tracked WP04 paths are:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Verify from Git; do not merely assume.

---

## 5. W7 S2 fault-injection discipline

After recovering the canonical contract, verify the W7 harness exercises the accepted narrow seam only.

The accepted seam is:

```text
Write-Wp04EvidenceCheckpoint
optional PersistEvidenceCheckpointCallback
omission defaults to existing writer
only final successful checkpoint uses the seam
other writes remain direct
W7 uses isolated callback fault injection
no global Set-Content override
```

Require W7 evidence to identify:

```text
whether callback was configured
expected callback invocation count
observed callback invocation count
injected fault identity
exact checkpoint operation affected
all checkpoint operations not affected
production lifecycle/error result
restoration behavior
error-precedence behavior
```

No global I/O interception or architecture broadening.

---

## 6. Exhaustive W7 validation

Execute the complete recovered W7 predicate matrix.

Do not use fail-fast diagnostics for ordinary predicate failures. Evaluate every safely evaluable gate.

Every final predicate record must contain:

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

No aggregate/literal PASS can substitute for individual evidence.

If the canonical W7 contract defines a 20-predicate accounting model, retain exactly that model. If it defines a different canonical count, preserve that exact count and report it—do not force W6's count onto W7.

---

## 7. Durable evidence requirements

The final W7 durable root must independently support Luna reconciliation after reopen.

Retain/index at least:

```text
canonical W7 contract + sources
attempt identity
artifact hashes
WinPS/parser evidence
authority-entry repository state
wrapper/helper source/copy identity evidence
complete individual W7 predicate records
S2 callback configuration evidence
S2 callback invocation evidence
fault-injection evidence
checkpoint observations
archive/extraction observations where canonical W7 requires them
final lifecycle observation
RestoreOnly/restoration observation where canonical W7 requires it
error-precedence evidence
governed interception/call ledger
real/unexpected external-call accounting
secret-hygiene evidence
cleanup evidence
post-run repository state
derived aggregate
manifest/index/hashes
durable reopen validation
```

Every acceptance-relevant `EvidenceReference` must resolve after reopen.

Require:

```text
UnresolvedEvidenceReferences=0
MissingRequiredEvidenceClasses=0
```

---

## 8. Cleanup and error precedence

Preserve all reconciliation evidence before deleting disposable sandbox state.

Require explicit ordering:

```text
scenario execution complete
→ required evidence durably preserved
→ cleanup attempted
→ cleanup complete
→ cleanup predicate evaluated
→ final durable serialization/index
→ reopen validation
```

Error-precedence validation must remain observation-only and must prove the harness/fault-injection evidence machinery does not replace or mask the canonical production-derived result.

---

## 9. Mutation and hygiene accounting

Final W7 evidence must prove:

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

The two pre-existing tracked WP04 paths must remain pre-existing and unchanged by this authority unless the bytes were already modified before authority entry.

No production mutation is authorized.

---

## 10. Single-converge retry loop

For any defect correctable entirely within authorized disposable W7 harness/validator/evidence scope:

```text
evaluate ALL gates
collect ALL defects
repair ALL local defects
rehash changed disposable artifacts
invalidate affected candidate evidence
consume any allocated failed W7 identity
create fresh W7 durable root
allocate a fresh never-used W7 identity only after pre-identity gates pass
rerun complete W7
cleanup
durable finalize/reopen
self-reconcile all gates
```

Repeat internally until ALL_PASS.

Never reuse an allocated W7 identity.

Do not return merely because a serializer/reference/fixture assertion defect was found.

---

## 11. Absolute stop boundaries

STOP and report all blockers if completion requires:

```text
frozen runner change
tracked/production source change
new Luna architecture/policy decision
real external invocation
Azure/GitHub/Docker/GHCR/Twelve Data action
secret-contract change
W8 execution
publication
issue/project/milestone lifecycle
staging/commit/push
```

---

## 12. Final self-reconciliation

Before returning, independently reopen the final W7 root and evaluate:

```text
A CanonicalContractRecovery
B IdentityFreshness
C IndividualW7Predicates
D S2FaultInjectionSemantics
E LifecycleAndErrorPrecedence
F DurableEvidenceAndReopen
G BoundaryMutationHygiene
H CarryForwardAndSequencing
```

Evaluate all A-H even if one is non-PASS.

### H requires

```text
W1-W6 accepted/carry-forward remains valid
W5/W6 accepted evidence unchanged
W8 NOT_RUN
publication blocker unresolved
WP04 #263 OPEN
```

---

## 13. Success markers

Only if A-H are PASS and the complete canonical W7 predicate matrix is ALL_PASS:

```text
RELEASE 1.12 WP04 — W7 SINGLE-CONVERGE EXECUTION: PASS
RELEASE 1.12 WP04 — W7 CANONICAL CONTRACT: RECOVERED_AND_SATISFIED
RELEASE 1.12 WP04 — W7 INDIVIDUAL ACCEPTANCE PREDICATES: ALL_PASS
RELEASE 1.12 WP04 — W7 S2 FAULT-INJECTION SEMANTICS: PASS
RELEASE 1.12 WP04 — W7 ERROR PRECEDENCE: PASS
RELEASE 1.12 WP04 — W7 DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — W7 EVIDENCE REFERENCES: ALL_RESOLVED
RELEASE 1.12 WP04 — W7 REAL EXTERNAL CALLS: 0
RELEASE 1.12 WP04 — W7 TRACKED PRODUCTION MUTATIONS: 0
RELEASE 1.12 WP04 — W7 EXTERNAL MUTATIONS: 0
RELEASE 1.12 WP04 — W7 ACCEPTANCE CANDIDATE: READY_FOR_FINAL_LUNA
RELEASE 1.12 WP04 — W1/W2/W3/W4/W5/W6: ACCEPTED_OR_AUTHORIZED_CARRY_FORWARD
RELEASE 1.12 WP04 — W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Terra must not claim final Luna acceptance.

Do not execute W8.

---

## 14. Required handoff

Return:

```text
RecoveredW7Contract
RecoveredW7ContractSources
RecoveredW7ExpectedLifecycle
RecoveredW7ExpectedS2Behavior

W7RetryAttemptCount
ConsumedFailedW7AttemptIds
FreshCandidateW7AttemptId
W7DurableEvidenceRoot

RunnerSHA256
RuntimeHarnessSHA256
ValidatorSHA256
ChildProbeSHA256
WrapperSourceSHA256
WrapperCopyPreSHA256
WrapperCopyPostSHA256

WindowsPowerShellVersion
ParserErrorCount

W7PredicateResults
W7PredicateCount
W7PassedPredicateCount
W7FailedPredicateCount
W7UnresolvedEvidenceReferenceCount

S2CallbackConfigured
ExpectedS2CallbackInvocationCount
ObservedS2CallbackInvocationCount
InjectedFaultIdentity
AffectedCheckpointOperation
UnaffectedCheckpointOperations

QualificationResult
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
ErrorPrecedenceResult

DurableReopenResult
MissingRequiredEvidenceClassCount
CleanupResult

RealExternalCallCount
UnexpectedExternalCallCount
ExternalMutationCount
AuthorityIntroducedTrackedMutationCount
StagedPathCount
GitDiffCheckResult
SecretHygieneResult

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
NextAuthorizedAction=GPT-5.6 Luna final read-only W7 reconciliation / W8 sequencing; W8 remains unexecuted
```

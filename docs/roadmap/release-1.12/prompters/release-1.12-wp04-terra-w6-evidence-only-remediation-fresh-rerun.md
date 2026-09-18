# Release 1.12 WP04 — Terra W6 Evidence-Only Remediation + Fresh Rerun

**Selected execution model: GPT-5.6 Terra**

## Mission

Perform the bounded W6 evidence-only remediation identified by Luna, then execute a **fresh W6 rerun with a new single-use attempt identity** and self-reconcile all A–G gates before returning.

Do not stop after fixing P01–P04 references or adding S2 evidence. Continue through the fresh rerun, durable reopen, and complete A–G self-reconciliation.

## Reconciled starting state

Prior W6 candidate root:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\w6\706784e5d99549c9ab6b9003f701e92b
```

Prior accepted-candidate attempt, now consumed and NOT reusable:

```text
initialize-90a69ef92a3e41d8899552856d29b9f8
```

Earlier incomplete attempt, also permanently consumed:

```text
initialize-226388ce686645fcab00efc81c9840bb
```

Luna reconciliation:

```text
A ContractRecovery          PASS
B IdentityFreshness         PASS
C W6Predicates              NOT_PROVEN
D LifecycleSemantics        NOT_PROVEN
E DurableEvidence           FAIL
F BoundaryMutationHygiene   PASS
G CarryForwardIntegrity     PASS
```

Exact defects:

```text
P01 PASS observation exists, but metadata EvidenceReference unresolved
P02 PASS observation exists, but metadata EvidenceReference unresolved
P03 PASS observation exists, but hash EvidenceReference unresolved
P04 PASS observation exists, but hash EvidenceReference unresolved

S2 callback absence/count is not explicitly retained.

No production-behavior defect was identified.
```

Known artifact identities from the blocked candidate:

```text
Runner=
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

RuntimeHarness=
D42D8EADBECCF36332FBAFAE2186AF6496A8E9141E34498E67716ED4E9FD8224

Validator=
789B2A46B4855063D6AAA9ADF0751D05591742EAF78C8C6DDBDFC085372E38B9

ChildProbe=
9199F6660EA1EE54FE3213E124FA96356DC7182816D323B6AD60F170B6F9ADB5

Wrapper=
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

## Canonical W6 semantics — unchanged

Do not change the recovered W6 contract:

```text
qualification failure retained
ArchiveRetrieval=RETRIEVAL_FAILED
FreshExtraction=NOT_APPLICABLE
EvidenceCheckpoint=PASS
FinalLifecycle=FAILURE
RestoreOnlyCount=1
S2CallbackInvocationCount=0
```

Contract sources remain:

```text
release-1.12-wp04-terra-hybrid-revised-v2-validation-authority.md
release-1.12-wp04-terra-exact-byte-sandbox-validation-authority.md
initialize-qualification.ps1 W6 lifecycle fixture
```

No new architecture/policy decision is authorized.

---

## 1. Authorized scope

Byte-changing correction is permitted only in disposable/local-only W6 harness/evidence serializer/validator artifacts needed to retain and resolve the missing evidence.

No tracked/production source mutation.

No frozen-runner mutation.

No real external call or mutation.

No W7/W8.

No publication/lifecycle.

No staging/commit/push.

---

## 2. Fix P01/P02 metadata evidence references

The fresh W6 durable root must contain explicit durable metadata evidence sufficient to resolve P01 and P02 after reopen.

At minimum retain a metadata record containing:

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

P01 and P02 `EvidenceReference` values must point to this retained record or to separate equally durable records.

After finalization/reopen, both references must resolve from the fresh W6 root alone.

---

## 3. Fix P03/P04 hash evidence references

Retain explicit durable hash/identity evidence containing at least:

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

Require exact wrapper SHA:

```text
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

Require:

```text
SourceEqualsPre=true
PreEqualsPost=true
```

P03 and P04 must reference resolvable retained hash evidence after reopen.

Do not derive acceptance merely from a console summary.

---

## 4. Explicit S2 absence evidence

Retain a dedicated W6 S2 record proving absence/non-invocation:

```text
RecordId
Scenario=W6
S2CallbackConfigured
S2CallbackInvocationCount
ExpectedInvocationCount
ObservedInvocationCount
Result
EvidenceReference
ValidationMethod
```

Require:

```text
S2CallbackConfigured=false
S2CallbackInvocationCount=0
ExpectedInvocationCount=0
ObservedInvocationCount=0
Result=PASS
```

This must be derived from the actual W6 fixture/harness execution state, not inserted as an unconnected literal PASS.

The record must resolve after durable reopen.

---

## 5. Preserve all existing W6 evidence requirements

Fresh root must again retain exactly 20 individually evidenced W6 predicates and all other evidence classes already required.

Do not regress P05–P20.

Every predicate record must contain:

```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
ObservationPhase
```

After reopen require:

```text
PredicateCount=20
Passed=20
Failed=0
UnresolvedPredicateEvidenceReferences=0
```

P01–P20 acceptance is invalid if any evidence reference is unresolved.

---

## 6. Fresh rerun required

Evidence cannot be retroactively patched into the old accepted-candidate attempt.

After byte-changing remediation:

```text
rehash changed disposable artifacts
→ allocate fresh W6 durable root
→ execute complete W6 preflight
→ allocate fresh never-used W6 attempt identity
→ execute complete canonical W6 scenario
→ cleanup
→ finalize durable evidence
→ reopen
→ resolve every acceptance reference
→ self-reconcile A-G
```

Both previous W6 identities are permanently consumed:

```text
initialize-226388ce686645fcab00efc81c9840bb
initialize-90a69ef92a3e41d8899552856d29b9f8
```

Never reuse either.

---

## 7. Preflight

Before fresh W6 attempt allocation require:

```text
Windows PowerShell=5.1.26100.9444
parser errors=0
frozen runner hash exact
production wrapper/helper unchanged
accepted W5 evidence unchanged/readable
tracked modified scope=the same two pre-existing WP04 paths
staged paths=0
git diff --check=PASS
real external calls=0
```

---

## 8. Canonical fresh W6 execution

The production-derived W6 result must again be:

```text
qualification failure retained
ArchiveRetrieval=RETRIEVAL_FAILED
FreshExtraction=NOT_APPLICABLE
EvidenceCheckpoint=PASS
FinalLifecycle=FAILURE
RestoreOnlyCount=1
S2CallbackInvocationCount=0
RealExternalCalls=0
UnexpectedExternalCalls=0
```

No hard-coded acceptance substitution.

---

## 9. Durable finalization/reopen

The fresh W6 root must contain/index:

```text
contract identity/source
fresh attempt identity
artifact hashes
WinPS/parser metadata record
wrapper source/pre/post hash record
authority-entry repository state
20 predicate records
scenario/lifecycle observations
explicit S2 absence record
governed call/interception ledger
cleanup evidence
post-run repository state
secret-hygiene evidence
error-precedence evidence
derived aggregate
manifest/index/hashes
reopen validation
```

After reopen independently resolve:

```text
20/20 predicate EvidenceReferences
P01 metadata reference
P02 metadata reference
P03 hash reference
P04 hash reference
S2 absence reference
all lifecycle references
all mutation/hygiene references
```

Require unresolved count `0`.

---

## 10. Single-converge retry behavior

Do not return for another disposable evidence/harness defect.

For every locally correctable failure:

```text
collect ALL current defects
→ repair ALL local defects
→ consume any allocated attempt identity
→ rehash
→ fresh root
→ fresh never-used attempt identity
→ rerun complete W6
→ reopen
→ self-reconcile A-G
```

Repeat until ALL_PASS.

Stop only if correction crosses a forbidden governance boundary.

---

## 11. A–G self-reconciliation

Before returning evaluate all:

```text
A ContractRecovery
B IdentityFreshness
C W6Predicates
D LifecycleSemantics
E DurableEvidence
F BoundaryMutationHygiene
G CarryForwardIntegrity
```

Require:

```text
A=PASS
B=PASS
C=PASS
D=PASS
E=PASS
F=PASS
G=PASS
```

Evaluate every section even if another fails.

### C requires

```text
20 predicates
20 PASS
0 FAIL
0 unresolved references
```

### D requires

```text
RETRIEVAL_FAILED
NOT_APPLICABLE
checkpoint PASS
final FAILURE
RestoreOnly 1
S2CallbackInvocationCount 0
```

### E requires

```text
DurableReopen=PASS
UnresolvedEvidenceReferences=0
MissingRequiredEvidenceClasses=0
```

### F requires

```text
RealExternalCalls=0
UnexpectedExternalCalls=0
ExternalMutations=0
AuthorityIntroducedTrackedProductionMutations=0
StagedPaths=0
GitDiffCheck=PASS
SecretHygiene=PASS
Cleanup=PASS
ErrorPrecedence=PASS
```

### G requires

```text
W1-W5 carry-forward remains valid
W7/W8 NOT_RUN
publication unresolved
WP04 #263 OPEN
```

---

## 12. Absolute stop conditions

STOP if correction/action requires:

```text
frozen runner mutation
tracked/production source mutation
new architecture/policy decision
real external invocation
Azure call/mutation
GitHub mutation
Docker/GHCR call/mutation
Twelve Data call/configuration
secret-contract change
W7/W8
publication/lifecycle mutation
```

Report all blockers.

---

## 13. Success markers

Only after fresh W6 + reopen + A-G ALL_PASS:

```text
RELEASE 1.12 WP04 — W6 EVIDENCE-ONLY REMEDIATION: PASS
RELEASE 1.12 WP04 — W6 FRESH RERUN: PASS
RELEASE 1.12 WP04 — W6 P01-P20: 20/20 PASS
RELEASE 1.12 WP04 — W6 P01-P04 EVIDENCE REFERENCES: RESOLVED
RELEASE 1.12 WP04 — W6 S2 CALLBACK ABSENCE: PROVEN
RELEASE 1.12 WP04 — W6 DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — W6 A-G SELF-RECONCILIATION: ALL_PASS
RELEASE 1.12 WP04 — W6 REAL EXTERNAL CALLS: 0
RELEASE 1.12 WP04 — W6 TRACKED PRODUCTION MUTATIONS: 0
RELEASE 1.12 WP04 — W6 EXTERNAL MUTATIONS: 0
RELEASE 1.12 WP04 — W6 ACCEPTANCE CANDIDATE: READY_FOR_FINAL_LUNA
RELEASE 1.12 WP04 — W7/W8: NOT_RUN
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Do not claim final Luna acceptance under Terra authority.

---

## 14. Required handoff

Return:

```text
W6RetryAttemptCount
ConsumedW6AttemptIds
FreshCandidateW6AttemptId
FreshW6DurableEvidenceRoot

RunnerSHA256
RuntimeHarnessSHA256
ValidatorSHA256
ChildProbeSHA256
WrapperSourceSHA256
WrapperCopyPreSHA256
WrapperCopyPostSHA256

WindowsPowerShellVersion
ParserErrorCount

P01-P20Results
PredicateCount
FailedPredicateCount
UnresolvedPredicateEvidenceReferenceCount

P01EvidenceReference
P02EvidenceReference
P03EvidenceReference
P04EvidenceReference
S2EvidenceReference

ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
S2CallbackConfigured
S2CallbackInvocationCount

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

SectionAResult
SectionBResult
SectionCResult
SectionDResult
SectionEResult
SectionFResult
SectionGResult

BoundaryBlocker
NextAuthorizedAction
```

On complete success:

```text
NextAuthorizedAction=GPT-5.6 Luna final read-only W6 reconciliation; W7 remains unexecuted
```

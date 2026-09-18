# Release 1.12 WP04 — Terra W5 Evidence-Retention Remediation + Fresh Replay Converge

**Selected execution model: GPT-5.6 Terra**

## Mission

Repair only the disposable W5 harness/evidence-retention defect identified by final Luna reconciliation, then perform the complete required revalidation and a **fresh W5 replay with a new single-use RunId** in this same Codex execution.

Do not stop after adding evidence fields. Continue until either:

```text
fresh C2 structural acceptance = ALL_PASS
AND
fresh W5 P01-P20 durable evidence = ALL_PASS
```

or a genuine governance-boundary blocker is encountered.

## Reconciled starting state

Previous W5 behavior executed successfully but final acceptance was NOT_PROVEN because durable evidence was incomplete.

Verified prior identities:

```text
StructuralRoot =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\7ddd4714fb8046c684a4f68d22a980e9

PriorW5Root =
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\w5\069bfd0cd6ec4c99aeb4ea1cea66b3ed

PriorW5RunId =
initialize-87d3ffe2ffc74ae48a8a8c55152ee2ec

RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

C2HarnessSHA256 =
F0A69A2AF8DD112FABBFA85CCB6BA0855BAE648058F4CF46981ED8D10E741EAA

ValidatorSHA256 =
32FC1234F3BF34AE5E2F7D06E875FC18E066C03356292205F7C7A946D031AC17

PriorW5RuntimeHarnessSHA256 =
C03785722D51541DA7C34733C493006A5040168517F8107742B2728EF1F507D9

WrapperSHA256 =
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

The prior W5 RunId is permanently consumed and MUST NOT be reused.

## Luna result

```text
A Identity/Freshness   PASS
B Fresh C2 Structural  PASS
C Runtime Interception NOT_PROVEN
D W5 P01-P20           NOT_PROVEN
E Lifecycle Semantics  NOT_PROVEN
F Mutation/Hygiene     NOT_PROVEN
```

Exact evidence defects:

```text
C:
W5 durable root retained only the 10 approved runtime calls.
Missing full negative/escape matrix and child-inheritance proof.

D:
No individual durable P01-P20 records.
P06/P07 absent.
P16/P18/P19/P20 not independently establishable.

E:
ArchiveRetrieval and FreshExtraction states absent from durable W5 root.

F:
No durable secret-hygiene record.
No durable authority-entry repository-scope record.
```

Defect owner:

```text
Disposable W5 harness / evidence accounting
```

No production behavior change is authorized or required.

---

# 1. Scope

Authorized byte-changing scope:

```text
disposable/local-only W5 runtime harness
its disposable/local-only evidence serializer/validator
C2 harness/validator/child only if strictly necessary to bind/revalidate the changed W5 runtime harness
```

Forbidden:

```text
frozen runner change
tracked/production source change
production wrapper/helper change
architecture/policy change
real external call
Azure mutation/call
GitHub mutation
Docker/GHCR mutation/call
Twelve Data call/configuration
secret-contract change
W6/W7/W8
publication/lifecycle
```

No staging/commit/push.

---

# 2. Evidence principle

The W5 durable root must be independently sufficient for final reconciliation.

Do not rely on console summaries, transient objects, a prior structural root, or knowledge held only by the producer.

The fresh W5 root must retain:

```text
identity/freshness evidence
complete runtime approved-call records
complete runtime negative-call records
complete escape-probe records
child interception-inheritance proof
individual P01-P20 records
archive/extraction lifecycle observations
checkpoint/final lifecycle/RestoreOnly/exit observations
authority-entry repository-scope evidence
post-run repository-scope evidence
secret-hygiene evidence
post-cleanup P19 evidence
error-precedence P20 evidence
complete sanitized W5 ledger
manifest/hash/index sufficient to reopen and resolve every record
```

---

# 3. Runtime interception durable evidence

Retain the full runtime matrix in the fresh W5 root.

Require individually durable records for:

```text
Git approved = 4
Git rejected = 5
Azure approved = 4
Azure rejected = 10
Helper approved = 2
Helper rejected = 10
Escape probes rejected = 5
```

Each record must include at least:

```text
RecordId
Surface
Contract
InvocationShape
ExpectedDisposition
ObservedDisposition
RealProcessSelected
RealExternalCallDelta
Result
EvidenceReference
```

Sanitize arguments where required.

Also retain a dedicated child-inheritance proof containing:

```text
ParentInterceptionIdentity
ChildPowerShellIdentity
InheritedPathOrResolutionIdentity
GitShadowResolved
AzShadowResolved
HelperShadowResolved
RealFallbackAvailableToGovernedRoute
Result
EvidenceReference
```

Acceptance:

```text
all approved shapes accepted
all negative/escape shapes rejected
RealProcessSelected=false for rejected/escape cases
RealExternalCallDelta=0 throughout
UnexpectedCalls=0
```

---

# 4. Production-derived lifecycle observations

Capture from the production wrapper's actual governed execution and retain in the same fresh W5 root:

```text
ArchiveRetrieval=RETRIEVAL_FAILED
FreshExtraction=NOT_APPLICABLE
EvidenceCheckpoint=PASS
FinalLifecycle=SUCCESS
RestoreOnlyCount=1
WrapperExitCode=0
```

These must be evidence-derived from wrapper/runtime output and ledger events, not literal acceptance constants.

Retain the Deferred descriptor correlation and the exact valid RestoreOnly consumption evidence, sanitized.

---

# 5. Individual P01-P20 records

Create exactly 20 durable predicate records in the fresh W5 root.

Each record must contain:

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

Require:

```text
P01 WinPS version 5.1.26100.9444
P02 parser 0 errors
P03 source/pre exact-byte
P04 pre/post exact-byte
P05 wrapper execution completed
P06 ArchiveRetrieval RETRIEVAL_FAILED
P07 FreshExtraction NOT_APPLICABLE
P08 EvidenceCheckpoint PASS
P09 Final SUCCESS
P10 RestoreOnly 1
P11 real external calls 0
P12 external ledger only governed intercepted calls
P13 repo modified scope unchanged
P14 staged 0
P15 git diff --check PASS
P16 secret hygiene PASS
P17 durable sanitized evidence retention
P18 complete durable W5 scenario ledger retention
P19 disposable sandbox cleanup PASS
P20 error precedence PASS
```

No predicate may be a literal/hard-coded PASS disconnected from underlying evidence.

Every `EvidenceReference` must resolve after durable root finalization/reopen.

---

# 6. P16 secret hygiene

Retain an explicit durable secret-hygiene record.

It must show what evidence surfaces were inspected and the sanitized result without persisting secret values.

At minimum inspect the fresh W5 durable artifacts/ledger and relevant intercepted invocation records for forbidden secret disclosure.

Require:

```text
SecretValuesPersisted=0
ForbiddenCredentialMaterialPersisted=0
Result=PASS
```

Never print or retain actual secret material.

---

# 7. P13/F authority-entry repository scope

Before any authorized byte-changing local remediation, retain a durable authority-entry repository snapshot sufficient to distinguish:

```text
pre-existing tracked WP04 modifications
authority-introduced tracked modifications
staged paths
```

The known pre-existing WP04 modified paths must be identified from Git rather than guessed.

At finalization retain the post-run repository snapshot and derive:

```text
AuthorityIntroducedTrackedProductionMutations=0
StagedPaths=0
RepoModifiedScopeUnchanged=true
GitDiffCheck=PASS
```

Do not mutate tracked files.

---

# 8. P18 complete durable W5 ledger

The W5 ledger must include/index every acceptance-relevant event category:

```text
authority-entry scope
identity/hashes
parser/version
interception installation
approved probes
negative probes
escape probes
child inheritance
RunId allocation
wrapper invocation
archive retrieval
fresh extraction
checkpoint
Deferred/RestoreOnly
wrapper exit
external-call count
repo post-state
git diff check
secret hygiene
cleanup
error precedence
P01-P20
finalization/reopen
```

After finalization, reopen the root and independently resolve all required records.

P18 cannot PASS before that completeness/reopen check succeeds.

---

# 9. P19 ordering

P19 must be generated/evaluated only **after disposable sandbox cleanup has completed**.

Required durable ordering evidence:

```text
WRAPPER_COMPLETE
→ evidence preservation needed before cleanup
→ SANDBOX_CLEANUP_ATTEMPT
→ SANDBOX_CLEANUP_COMPLETE
→ P19_EVALUATION
```

Require:

```text
SandboxExistsAfterCleanup=false
P19=PASS
```

If evidence needed for final reconciliation exists only in the sandbox, copy/sanitize it into the durable W5 root before cleanup.

---

# 10. P20 error precedence

Retain explicit observation-only evidence proving the harness's evidence/finalization logic did not replace the production wrapper's governing error/lifecycle result.

For the canonical success scenario require the observed precedence chain and:

```text
ErrorPrecedenceAltered=false
P20=PASS
```

Do not introduce new production error policy.

---

# 11. Byte-change invalidation

Because the W5 runtime harness will change, prior W5-runtime structural binding is invalidated.

After every byte-changing correction:

```text
recompute hashes
create fresh structural durable root
rerun complete C2
evaluate all R01-R14
rerun adversarial reconciliation
```

Require again:

```text
R01-R14=ALL_PASS
SV01-SV36=36/0
53/53 event-specific evidence
G01-G11=11/11
RunId/wrapper sites uncovered=0
11/11 corruption fixtures
runtime interception structural/probe gates pass
structural W5 RunIds=0
structural wrapper invocations=0
structural real external calls=0
tracked authority mutations=0
staged paths=0
git diff --check=PASS
```

Do not compose fresh acceptance from the old structural root.

---

# 12. Single-converge retry behavior

Do not return for ordinary disposable harness/evidence defects.

For each iteration:

```text
evaluate ALL gates
collect ALL defects
repair ALL local defects
rehash
fresh root
rerun complete structural cycle
reconcile ALL R01-R14
```

Only after structural ALL_PASS may W5 proceed.

If a fresh W5 attempt fails because of another disposable evidence/harness defect:

```text
retain failed W5 root
permanently consume that RunId
repair locally
invalidate structural acceptance
fresh structural root
full C2 R01-R14
new fresh W5 RunId
fresh W5 replay
```

Repeat internally until acceptance evidence is complete.

---

# 13. Fresh W5 RunId

The prior RunId:

```text
initialize-87d3ffe2ffc74ae48a8a8c55152ee2ec
```

is forbidden for reuse.

Allocate no new RunId until all fresh structural and runtime pre-RunId gates pass.

Then allocate exactly one fresh never-used RunId for that attempt.

Every allocated RunId is permanently consumed.

---

# 14. Fresh W5 replay

Execute the byte-exact copied production wrapper under the proven interception environment.

Require:

```text
ArchiveRetrieval=RETRIEVAL_FAILED
FreshExtraction=NOT_APPLICABLE
EvidenceCheckpoint=PASS
FinalLifecycle=SUCCESS
RestoreOnlyCount=1
WrapperExitCode=0
RealExternalCalls=0
```

Then complete cleanup, P19, P20, ledger finalization, serialization, publication to the local durable root, and reopen validation.

---

# 15. Self-reconciliation before returning

Before returning, perform an independent read-only acceptance pass over the **fresh W5 durable root**.

Require sections:

```text
A Identity/Freshness = PASS
B Fresh C2 Structural = PASS
C Runtime Interception = PASS
D W5 P01-P20 = PASS
E Lifecycle Semantics = PASS
F Mutation/Hygiene = PASS
```

Explicitly verify P01-P20 individually from reopened durable records.

If a section is locally correctable, do not return—continue convergence.

---

# 16. Success markers

Only after the fresh replay and self-reconciliation pass:

```text
RELEASE 1.12 WP04 — W5 EVIDENCE RETENTION REMEDIATION: PASS
RELEASE 1.12 WP04 — FRESH C2 R01-R14: ALL_PASS
RELEASE 1.12 WP04 — W5 RUNTIME INTERCEPTION DURABLE MATRIX: PASS
RELEASE 1.12 WP04 — W5 CHILD INTERCEPTION INHERITANCE: PASS
RELEASE 1.12 WP04 — W5 LIFECYCLE OBSERVATIONS DURABLE: PASS
RELEASE 1.12 WP04 — W5 P01-P20 DURABLE RECORDS: 20/20
RELEASE 1.12 WP04 — W5 SECRET HYGIENE EVIDENCE: PASS
RELEASE 1.12 WP04 — W5 AUTHORITY-ENTRY/FINAL REPOSITORY SCOPE: PASS
RELEASE 1.12 WP04 — W5 POST-CLEANUP P19: PASS
RELEASE 1.12 WP04 — W5 ERROR PRECEDENCE P20: PASS
RELEASE 1.12 WP04 — W5 DURABLE ROOT REOPEN VALIDATION: PASS
RELEASE 1.12 WP04 — W5 REAL EXTERNAL CALLS: 0
RELEASE 1.12 WP04 — W5 P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE CANDIDATE: READY_FOR_FINAL_LUNA
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Do not claim final Luna acceptance under Terra authority.

---

# 17. Absolute stop conditions

STOP if any correction/action requires:

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
W6/W7/W8
publication/lifecycle mutation
```

Report all blockers, not merely the first.

---

# 18. Consolidated handoff

Return:

```text
StructuralRetryAttemptCount
StructuralSupersededRoots
FinalStructuralDurableRoot
RunnerSHA256
C2HarnessSHA256
ValidatorSHA256
ChildProbeSHA256
W5RuntimeHarnessSHA256

W5AttemptCount
ConsumedW5RunIds
FreshAcceptedCandidateW5RunId
FreshW5DurableRoot
WrapperSourceSHA256
WrapperPreSHA256
WrapperPostSHA256

R01-R14FinalMatrix
SVPassCount
NamedEvidencePassCount

RuntimeApprovedRecords
RuntimeNegativeRecords
EscapeProbeRecords
ChildInheritanceResult
UnexpectedCallCount
RealExternalCallCount

P01-P20IndividualResults
P01-P20DurableRecordCount
P01-P20ResolvedEvidenceCount

ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
WrapperExitCode

SecretHygieneResult
AuthorityEntryRepoScopeRecord
FinalRepoScopeRecord
AuthorityIntroducedTrackedMutationCount
StagedPathCount
GitDiffCheckResult

SandboxCleanupResult
P19ObservationPhase
ErrorPrecedenceResult
P20ObservationPhase

W5LedgerCompletenessResult
W5DurableReopenResult

SelfReconciliationA
SelfReconciliationB
SelfReconciliationC
SelfReconciliationD
SelfReconciliationE
SelfReconciliationF

BoundaryBlocker
NextAuthorizedAction
```

On complete success:

```text
NextAuthorizedAction=GPT-5.6 Luna final read-only W5 reconciliation; W6 remains unexecuted
```

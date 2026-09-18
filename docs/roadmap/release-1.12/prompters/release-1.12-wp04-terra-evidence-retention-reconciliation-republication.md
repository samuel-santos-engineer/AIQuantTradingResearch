# Release 1.12 WP04 — Terra Evidence-Retention Reconciliation & Republication

**Selected execution model: GPT-5.6 Terra**

## Authority type

Evidence-only remediation at the existing committed target boundary.

This authority exists because Luna correctly returned:

```text
FinalAcceptance=NOT_READY
```

The blocker is not a demonstrated persistence/runtime failure. It is incomplete durable acceptance evidence/provenance plus read-only repository-scope verification.

No new W5/WP04 qualification runtime execution is authorized by default.

---

## 1. Roles

```text
GPT-5.6 Luna
  contract, policy, architecture, acceptance, governance

GPT-5.6 Terra
  evidence reconstruction/validation and specifically authorized publication
  of durable evidence artifacts if tracked evidence publication is required

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Terra**.

---

## 2. Known accepted execution identities

Use these as required exact identities, subject to read-only verification:

```text
LocalHEAD=e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff

WrapperSHA256=
2DF9173EAE15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A

HelperSHA256=
7916106C11BEF92251EE49C7E0C0650744BE728CD42EE4071BF9D33E013F0541

DeployedImageDigest=
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00

InitializeRunId=
initialize-c13b371cfb8443cfb8289dd426c13bc4

ReopenRunId=
reopen-c471772b41b942ecb0ee9bc043122fde
```

Both RunIds are permanently consumed and MUST NOT be reused.

Prior diagnostic RunId also remains consumed:

```text
initialize-41d15357bc4747428f94a9e245d7a138
```

---

## 3. Luna blockers to close

Reconcile all blockers in one pass:

```text
B01 D01-D19 not individually/durably complete
B02 D19 lacks complete reopen + mutation-ledger references
B03 final remote tip not independently verified
B04 durable manifest uses abbreviated commit e163c2a
B05 accumulated release→main scope not reconciled
```

Do not stop after the first evidence defect. Inventory the entire retained evidence root and repository topology, produce one complete defect matrix, and repair every correctable evidence-only defect within this authority.

---

## 4. Existing durable root

Existing retained root:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\target-validation\e163c2ab3bc9a5dc20d486c1a5560c36bcac0738-eef354ed478249f9a61f524d95a64121
```

Treat it as historical retained evidence.

Do NOT overwrite or destructively mutate it.

Create a NEW reconciliation root, for example:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\target-validation-reconciliation\<fresh-guid>
```

The new root must preserve provenance to the original root and clearly state:

```text
RuntimeReexecution=false
SourceRuntimeMutation=false
EvidenceReconciliation=true
```

---

## 5. No new qualification by default

Explicitly forbidden unless a later Luna authority grants it:

```text
new initialize RunId
new reopen RunId
temporary qualification App Settings
H1 Deferred execution
RestoreOnly execution
new HTTP qualification request
new W5 runtime execution
restart/recycle/redeploy
```

The purpose is to reconstruct/reconcile durable evidence from already-retained artifacts, transcripts, Git state, and accepted carry-forward evidence.

If a required substantive fact cannot be proven without a new governed runtime execution, STOP and report exactly which predicate requires rerun. Do not allocate a RunId.

---

## 6. Full retained-artifact inventory

Recursively inventory the original durable root and all legitimate existing WP04 evidence roots referenced by its artifacts.

For each file record:

```text
RelativePath
ByteLength
SHA256
EvidenceClass
RunIdIfAny
PhaseIfAny
ReferencedBy
RetentionStatus
```

Search specifically for:
- initialize transcript/artifacts;
- reopen transcript/artifacts;
- HTTP poll observations;
- terminal exact-RunId payloads;
- persistence qualification JSON;
- H1 Deferred/RestoreOnly records;
- pre-state/post-state;
- evidence checkpoint records;
- mutation ledgers;
- Git provenance;
- wrapper/helper hashes;
- image identity;
- D predicate records;
- carry-forward references;
- archive/retrieval truth;
- manifests/hashes.

Do not invent missing evidence.

---

## 7. Reopen evidence reconciliation

For:

```text
reopen-c471772b41b942ecb0ee9bc043122fde
```

Locate and durably index every existing artifact that proves:

```text
503 → Timeout → 200
terminal HTTP 200
exact reopen RunId attribution
schema=4
journal=delete
integrity=ok
quick-check=ok
accepted count=1
persistence continuity=true
token disclosure=false
Deferred → RestoreOnly
final lifecycle=SUCCESS
```

If these facts exist in transcripts/logs but were omitted from the prior manifest, copy or reference them into the NEW reconciliation root without altering their contents.

For copied evidence record:

```text
OriginalPath
OriginalSHA256
ReconciledPath
ReconciledSHA256
ByteIdentityPreserved=true
```

If byte identity differs, fail that evidence item.

---

## 8. Mutation-ledger reconstruction

Reconstruct the final committed-helper cycle mutation ledger from retained logs/transcripts and Git/Azure evidence.

At minimum account for:

```text
Git helper stage/commit/non-force push
canonical temporary qualification settings for initialize
initialize restoration
canonical temporary qualification settings for reopen
reopen restoration
```

And prove zero for:

```text
Docker build
GHCR publication
image deployment
WP07 restart/recycle/redeploy acceptance
PR
merge
GitHub lifecycle
Twelve Data secret configuration
```

Every ledger row:

```text
MutationId
Category
Operation
Target
RunIdOrCommit
ExpectedAuthorization
Observed
Result
EvidenceReference
ReferenceResolved
```

Do not infer an external mutation solely from the desired contract. It must have retained support.

---

## 9. D01-D19 complete evidence ledger

Create a new complete 19-row ledger.

Every row MUST contain:

```text
PredicateId
EvidenceClass
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
ReferenceResolved
FreshOrCarryForward
```

Definitions:

```text
D01 initialize invokes application-owned persistence use case
D02 canonical observation accepted/idempotently recognized
D03 canonical observation read back unchanged
D04 conflicting evidence rejected/non-overwriting
D05 database identity=/home/data/aiquant.db
D06 schema version=4
D07 journal mode=DELETE
D08 integrity check=ok
D09 quick check=ok
D10 accepted evidence identity/count correct
D11 HTTP evidence attributable to exact fresh RunId
D12 evidence checkpoint before RestoreOnly
D13 RestoreOnly exactly once
D14 temporary qualification settings restored
D15 token/secret hygiene
D16 no direct SQLite/Kudu/Python deployment bypass
D17 F1/West Central US/$0 architecture unchanged
D18 repository/external mutation boundaries clean/exactly governed
D19 durable evidence reopens/all references resolved
```

Expected classification:

```text
D01-D03 = existing final committed-helper target evidence
D04 = accepted carry-forward
D05-D15 = existing final committed-helper target evidence
D16 = accepted carry-forward + current non-contradiction
D17 = existing architecture/read-only evidence
D18 = accepted carry-forward + final-cycle mutation ledger
D19 = NEW reconciliation-root durable reopen
```

No aggregate marker may substitute for a row.

---

## 10. Full commit provenance

The new manifest MUST use full 40-character commit IDs.

Required:

```text
FinalReleaseBranchCommit=
e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
```

Forbidden as authoritative identity:

```text
e163c2a
```

Abbreviated values may appear only as non-authoritative display aliases.

Require:

```text
AuthoritativeCommitIdentityLength=40
ManifestCommitIdentityExact=true
```

Record full commit identities for all referenced Git evidence where available.

---

## 11. Remote-tip verification

Attempt read-only verification:

```text
git rev-parse HEAD
git rev-parse origin/release/1.12-wp04-persistent-sqlite
git ls-remote origin refs/heads/release/1.12-wp04-persistent-sqlite
```

Expected:

```text
e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
```

If live GitHub connectivity is unavailable:
1. do not fail the entire evidence reconstruction immediately;
2. record the exact command/error;
3. use the locally cached `origin/release/...` ref as secondary evidence;
4. classify:

```text
LiveRemoteVerification=UNAVAILABLE
CachedRemoteTipVerification=<PASS|FAIL>
```

A later Luna reconciliation decides whether live remote verification remains blocking.

No push is authorized merely to test connectivity.

---

## 12. Repository topology and scope reconciliation

Read-only recompute immediately; do not reuse historical counts.

Determine:

```text
CurrentMain
CurrentOriginMain
CurrentReleaseTip
MergeBase
ReleaseOnlyCommitCount
MainOnlyCommitCount
ReleaseOnlyCommitList
ChangedPathCount
ChangedPathList
```

Use the exact current repository state.

For every release→main changed path classify:

```text
Path
WP04Relation
Reason
READMEPath
WP05Path
WP07Path
Unrelated
```

Require a complete scope matrix.

Do NOT assume the reported `17 release-only commits / 13 paths` is either correct or incorrect; recompute it.

Acceptance target:

```text
UnrelatedContaminationCount=0
UnauthorizedREADMEInformationLoss=0
WP05ImplementationIncluded=0
WP07ImplementationIncluded=0
```

README preservation policy remains binding:

```text
EXISTING README INFORMATION → PRESERVE
PRESERVE INFORMATION; UPDATE STATUS
SUMMARY ≠ AUTHORITY TO DELETE DETAIL
```

No README mutation is authorized.

---

## 13. Hybrid W1-W8

Do not rerun.

Binding prior status:

```text
HybridW1W8=ACCEPTED
```

Perform non-contradiction review only.

Require:

```text
HybridW1W8CurrentNonContradiction=PASS
```

---

## 14. Policy A ownership

Binding:

```text
CanonicalOwnershipDecision=WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
WP04_DEPLOYMENT_RECOVERY_REQUIRED_FOR_CLOSURE=false
WP07_DEPLOYMENT_RECOVERY_REQUIRED_FOR_RELEASE_ACCEPTANCE=true
```

No WP07 execution.

---

## 15. Evidence republication mechanics

"Republication" in this authority means producing a complete NEW durable reconciliation package from already-existing evidence.

Default location is local durable evidence storage, not tracked repository source.

Create:

```text
manifest.json
hashes.json
d01-d19-ledger.json
mutation-ledger.json
repository-scope.json
remote-verification.json
reopen-evidence-index.json
reconciliation-summary.json
```

Equivalent established project-local evidence formats are acceptable if they preserve all required fields.

Do not modify original evidence bytes.

If existing project policy requires evidence artifacts to be committed to the repository, STOP before Git mutation and report the exact required tracked paths. This authority does NOT authorize a new evidence commit unless such publication was already part of the established WP04 evidence mechanism and can be proven read-only from repository policy.

---

## 16. Independent validation and durable reopen

After constructing the reconciliation root:

1. close all writer handles;
2. independently reopen the package;
3. verify all manifest entries;
4. recompute every SHA256;
5. resolve every D01-D19 reference;
6. resolve every mutation-ledger reference;
7. resolve every reopen reference;
8. resolve repository-scope references;
9. verify exact full commit identity.

Require:

```text
PredicateCount=19
FailedPredicateCount=0
UnresolvedPredicateEvidenceReferenceCount=0
UnresolvedMutationEvidenceReferenceCount=0
UnresolvedReopenEvidenceReferenceCount=0
UnresolvedEvidenceReferenceCount=0
MissingRequiredEvidenceClassCount=0
ManifestHashMismatchCount=0
DurableReopen=PASS
```

Any missing evidence must remain missing; never manufacture it.

---

## 17. Bounded evidence convergence

Evaluate all evidence gates exhaustively.

Terra may internally repair:
- manifest omissions;
- incorrect evidence references;
- missing copied/indexed retained artifacts;
- abbreviated commit identities;
- reconciliation-root formatting/schema defects;
- repository-scope classification errors;
- hash/index defects;

provided the underlying evidence already exists and no tracked source/runtime mutation is required.

Each byte-changing reconciliation-root correction invalidates the reconciliation package hashes and requires:
- fresh package hashes;
- complete independent reopen;
- complete D01-D19/reference validation restart.

Continue until PASS or until a true missing-evidence/governance boundary is found.

---

## 18. Hard-stop boundaries

STOP if completion requires:

```text
new qualification RunId
new initialize/reopen execution
temporary Azure qualification settings
new Azure mutation
runtime/source/helper/wrapper edit
frozen runner edit
Docker/GHCR/image mutation
WP07 operation
Twelve Data secret
paid resource
PR/merge
issue/project/milestone lifecycle
new tracked evidence commit not already governed
new Luna architecture/policy decision
```

---

## 19. Mutation accounting

Expected authorized mutations:

```text
Local durable evidence filesystem:
  create NEW reconciliation root/package only
```

Expected zero:

```text
TrackedSourceMutation=0
GitCommit=0
GitPush=0
AzureMutation=0
DockerMutation=0
GhcrMutation=0
ImageDeploymentMutation=0
PR=0
Merge=0
GitHubLifecycleMutation=0
WP07Action=0
```

Read-only Git/network commands are not mutations.

---

## 20. Success markers

Only if evidence reconciliation succeeds:

```text
RELEASE 1.12 WP04 — EVIDENCE RETENTION RECONCILIATION: PASS
RELEASE 1.12 WP04 — REOPEN EVIDENCE INDEX: PASS
RELEASE 1.12 WP04 — MUTATION LEDGER: PASS
RELEASE 1.12 WP04 — D01-D19 DURABLE LEDGER: 19/19 COMPLETE
RELEASE 1.12 WP04 — FULL COMMIT PROVENANCE: PASS
RELEASE 1.12 WP04 — REPOSITORY SCOPE RECONCILIATION: PASS
RELEASE 1.12 WP04 — DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — NEW RUNTIME EXECUTION: 0
RELEASE 1.12 WP04 — NEW RUNID ALLOCATION: 0
RELEASE 1.12 WP04 — WP07 ACTIONS: 0
RELEASE 1.12 WP04 — READY_FOR_FINAL_LUNA_RECONCILIATION: YES
```

If live remote verification alone is unavailable but all local/cached evidence passes, report that separately and still return the package to Luna rather than inventing remote success.

---

## 21. Required handoff

Return:

```text
SelectedModel

EntryHEAD
EntryCachedRemoteTip
LiveRemoteVerification
LiveRemoteVerificationError
CachedRemoteTipVerification

OriginalDurableRoot
ReconciliationDurableRoot
OriginalArtifactCount
ReconciledArtifactCount

InitializeRunId
ReopenRunId
NewRunIdAllocationCount
RuntimeReexecutionCount

ReopenEvidenceArtifactCount
ReopenTerminalEvidenceResolved
ReopenLifecycleEvidenceResolved

MutationLedgerRowCount
UnresolvedMutationEvidenceReferenceCount

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
FailedPredicateCount
UnresolvedPredicateEvidenceReferenceCount

ManifestAuthoritativeCommit
AuthoritativeCommitIdentityLength
ManifestCommitIdentityExact

CurrentMain
CurrentOriginMain
CurrentReleaseTip
MergeBase
ReleaseOnlyCommitCount
MainOnlyCommitCount
ChangedPathCount
ChangedPathList
UnrelatedContaminationCount
UnauthorizedREADMEInformationLoss
WP05ImplementationIncluded
WP07ImplementationIncluded

HybridW1W8CurrentNonContradiction
CanonicalOwnershipDecision

UnresolvedReopenEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
MissingRequiredEvidenceClassCount
ManifestHashMismatchCount
DurableReopenResult

TrackedSourceMutationCount
GitCommitCount
GitPushCount
AzureMutationCount
DockerMutationCount
GhcrMutationCount
ImageDeploymentMutationCount
PRCount
MergeCount
GitHubLifecycleMutationCount
WP07ActionCount

BoundaryBlocker
EvidenceReconciliationResult
NextAuthorizedAction
```

On complete success:

```text
EvidenceReconciliationResult=PASS
NextAuthorizedAction=GPT-5.6 Luna final read-only WP04 substantive acceptance reconciliation using the new durable reconciliation package
```

If underlying evidence is genuinely absent:

```text
EvidenceReconciliationResult=NOT_READY
BoundaryBlocker=<exact missing substantive evidence>
NextAuthorizedAction=GPT-5.6 Luna narrow decision on whether fresh governed runtime evidence is required
```

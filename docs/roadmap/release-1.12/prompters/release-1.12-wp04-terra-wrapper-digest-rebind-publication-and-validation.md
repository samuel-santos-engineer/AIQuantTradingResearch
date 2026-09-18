# Release 1.12 WP04 — Terra Wrapper Digest Rebind, Publication & Fresh Qualification

**Selected execution model: GPT-5.6 Terra**

## Mission

Resolve the single governed-path blocker created by publication/deployment of the authentication-path image.

Current committed/runtime state:

```text
SourceCommit=13b085fec8af4b746b8ff0579b611451e8429a27
RemoteReleaseBranchTip=13b085fec8af4b746b8ff0579b611451e8429a27
DeployedImage=
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
Build=PASS
Tests=375 passed, 0 failed
WorkingTreeTrackedModifications=0
StagedPaths=0
GitDiffCheck=PASS
QualificationRunIdsAllocated=0
TemporaryQualificationSettingsApplied=0
WP07Actions=0
```

Blocker:

```text
initialize-qualification.ps1 still expects the superseded image digest
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

The canonical new immutable image digest is:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

This authority authorizes exactly one tracked source change:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Change only the wrapper's expected image digest from the superseded digest to the new exact immutable digest.

Then validate, commit, non-force push, and execute fresh WP04 qualification from the new clean committed anchor.

No new image build is required or authorized because the wrapper is deployment-side orchestration and is not part of the runtime image.

---

## 1. Roles

```text
GPT-5.6 Luna
  contract/policy/architecture/governance/final acceptance

GPT-5.6 Terra
  implementation, validation, approved Git/Azure mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Terra**.

---

## 2. Entry preflight

Before editing require:

```text
HEAD=13b085fec8af4b746b8ff0579b611451e8429a27
origin/release/1.12-wp04-persistent-sqlite=13b085fec8af4b746b8ff0579b611451e8429a27
working tree clean
staged paths=0
git diff --check=PASS
```

Verify read-only that the deployed image identity is exactly:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

Verify the wrapper currently contains the superseded digest and identify every occurrence.

Require:

```text
OldDigestOccurrenceCount=1
```

If the old digest occurs in additional semantic locations, STOP and report them rather than bulk-replacing.

---

## 3. Exact authorized edit

Allowed path only:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Allowed semantic change only:

```text
OLD:
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f

NEW:
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

No other byte/semantic change is authorized.

After edit require:

```text
ChangedPathCount=1
ChangedPath=<wrapper>
OldDigestOccurrenceCount=0
NewDigestOccurrenceCount=1
```

Inspect the diff and prove:

```text
OnlyExpectedDigestLiteralChanged=true
PersistenceSemanticsChanged=false
AuthenticationSemanticsChanged=false
AzureMutationSurfaceChanged=false
WP04WP07OwnershipChanged=false
```

If false, STOP before commit.

---

## 4. Wrapper validation

Binding:

```text
Windows PowerShell=5.1.26100.9444
```

Require:
- wrapper AST parser errors = 0;
- helper AST parser errors = 0;
- existing wrapper/helper local validation PASS;
- source/hash/provenance validation PASS;
- `git diff --check=PASS`.

Run applicable established tests/build needed to ensure no contradiction with the accepted WP04 implementation. Do not rebuild the Docker image.

Record:

```text
PreviousWrapperSHA256
NewWrapperSHA256
HelperSHA256
ValidationMatrix
```

Any byte-changing correction beyond the exact digest literal is outside authority and requires STOP.

---

## 5. Commit and push

After all local gates PASS:

```text
git add eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
git commit
git push origin release/1.12-wp04-persistent-sqlite
```

Push must be non-force.

Commit purpose:

```text
Bind WP04 qualification wrapper to authentication-fix image digest.
```

Require post-push:

```text
LocalHEAD=<new wrapper-digest commit>
RemoteReleaseBranchTip=<same SHA>
CommitChangedPathCount=1
CommitChangedPath=<wrapper>
working tree clean
staged paths=0
git diff --check=PASS
```

No PR/merge.

---

## 6. No image rebuild / redeployment

Require:

```text
DockerBuildCount=0
GhcrPublicationCount=0
ImageDeploymentMutationCount=0
```

The already deployed image must remain:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

Read-only verify it after the wrapper push.

If deployed image differs, STOP before RunId allocation.

---

## 7. Fresh qualification preflight

From the new wrapper-digest commit require:

```text
HEAD == remote release branch tip
working tree clean
staged paths=0
git diff --check=PASS
wrapper working-tree SHA == wrapper HEAD SHA == NewWrapperSHA256
helper working-tree SHA == helper HEAD SHA
wrapper clean-HEAD=PASS
helper clean-HEAD=PASS
wrapper expected digest == deployed immutable digest
```

Frozen runner remains:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Only after all these gates PASS may a fresh RunId be allocated.

---

## 8. Canonical WP04 boundary

Binding:

```text
WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
```

Do not execute WP07 restart/recycle/redeploy acceptance.

The prior image update already occurred under the preceding authority and receives no WP07 acceptance credit.

---

## 9. Fresh qualification

Allocate fresh helper-owned RB2 RunId(s). Never reuse any historical RunId; consume failures permanently.

Execute only:

```text
initialize/reopen
temporary qualification App Settings
H1 Deferred
application-owned HTTP evidence
durable evidence checkpoint
RestoreOnly in finally
```

M1: no redundant explicit restart.

Endpoint:

```text
GET /internal/wp04/persistence-qualification?runId=<fresh RunId>
X-WP04-Evidence-Token
```

No token persistence/printing.

No direct SQLite/Python/Kudu `/home`.

No Twelve Data secret configuration.

---

## 10. D01-D19

Fresh:
D01-D03, D05-D15, D17, D19.

Carry-forward with non-contradiction:
D04, D16, D18.

```text
D01 app-owned persistence use case invoked
D02 canonical observation accepted/idempotently recognized
D03 app-owned reopen/readback unchanged
D04 conflict rejected/non-overwriting
D05 /home/data/aiquant.db
D06 schema v4
D07 journal DELETE
D08 integrity ok
D09 quick-check ok
D10 accepted identity/count correct
D11 HTTP evidence exact fresh RunId
D12 checkpoint before RestoreOnly
D13 RestoreOnly exactly once
D14 temporary settings restored
D15 token/secret hygiene
D16 no direct SQLite/Kudu/Python bypass
D17 F1/West Central US/$0 unchanged
D18 exact authorized mutation boundaries
D19 durable evidence reopen/all refs resolved
```

D11 must specifically prove the authentication/request-path fix works against the new deployed image.

D15 must prove the fix did not weaken token hygiene.

No fresh Azure conflict operation for D04.

---

## 11. Durable evidence

Create a fresh durable root tied to:
- new wrapper-digest commit;
- NewWrapperSHA256;
- helper SHA;
- frozen runner SHA;
- deployed image digest `aad23c8...`;
- fresh RunId(s).

Persist/index preflight, mutation ledger, HTTP observations, application record, checkpoint/restoration, D01-D19, carry-forward refs, post-state, repository invariants, manifest/hashes.

Each predicate:

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
```

Independently reopen.

Require:

```text
PredicateCount=19
FailedPredicateCount=0
UnresolvedPredicateEvidenceReferenceCount=0
UnresolvedEvidenceReferenceCount=0
MissingRequiredEvidenceClassCount=0
DurableReopen=PASS
```

---

## 12. Evidence truth policy

Preserve:

```text
RETAINED + PASS|EMPTY                 eligible
RETAINED + FAIL|NOT_APPLICABLE       fail
RETRIEVAL_FAILED + NOT_APPLICABLE    eligible
other combinations                    fail/inconsistent
```

No Kudu `/home` retry.

---

## 13. Bounded convergence

Evaluate all gates exhaustively.

For disposable local evidence/harness defects only:

```text
collect all failures
fix all in-scope disposable defects
invalidate affected evidence
consume failed RunIds
allocate fresh RunIds
create fresh durable root
rerun full affected qualification cycle
```

Continue internally until PASS.

Hard STOP if correction requires:
- another tracked source edit;
- helper/endpoint/wrapper semantic edit beyond the exact digest replacement already committed;
- frozen-runner change;
- image rebuild/publication;
- image deployment mutation;
- new Azure mutation category;
- WP07 action;
- paid resource;
- Twelve Data secret;
- PR/merge/lifecycle;
- Luna policy decision.

After Azure-mutating failure, restore canonical temporary settings before returning.

---

## 14. Mutation accounting

Authorized new mutations in this authority:

```text
Git:
  one wrapper edit
  stage
  commit
  non-force push

Azure:
  canonical temporary qualification settings/H1 lifecycle only
```

No:
- Docker/GHCR mutation;
- image deployment mutation;
- PR/merge;
- README mutation;
- issue/project/milestone mutation.

Ledger every actual mutation.

---

## 15. Final state

Require:
- local HEAD == remote release tip at new wrapper commit;
- working tree clean/staged 0/diff-check PASS;
- deployed image still `sha256:aad23c8...fe07f00`;
- wrapper expected digest equals deployed digest;
- qualification settings restored exactly;
- D01-D19 19/19 PASS/carry-forward;
- WP07 acceptance actions 0;
- PR/merge 0;
- #263 remains OPEN;
- WP05/WP07 NOT_STARTED.

---

## 16. Success markers

Only on full success:

```text
RELEASE 1.12 WP04 — WRAPPER IMAGE-DIGEST REBIND: PASS
RELEASE 1.12 WP04 — WRAPPER DIGEST COMMIT/PUSH: PASS
RELEASE 1.12 WP04 — DEPLOYED IMAGE DIGEST MATCH: PASS
RELEASE 1.12 WP04 — IMAGE REBUILD: NOT_REQUIRED
RELEASE 1.12 WP04 — FRESH QUALIFICATION: PASS
RELEASE 1.12 WP04 — D01-D19: 19/19 PASS_OR_ACCEPTED_CARRY_FORWARD
RELEASE 1.12 WP04 — HTTP AUTHENTICATION/RUNID ATTRIBUTION: PASS
RELEASE 1.12 WP04 — SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — FINAL TARGET STATE: RESTORED
RELEASE 1.12 WP04 — WP07 ACCEPTANCE ACTIONS: 0
RELEASE 1.12 WP04 — PR/MERGE: DEFERRED_UNTIL_FINAL_ACCEPTANCE
RELEASE 1.12 WP04 — WP04 #263: OPEN
RELEASE 1.12 WP04 — SUBSTANTIVE ACCEPTANCE CANDIDATE: READY_FOR_FINAL_LUNA
RELEASE 1.12 WP04 — WP05: NOT_STARTED
RELEASE 1.12 WP04 — WP07: NOT_STARTED
```

---

## 17. Required handoff

Return:

```text
EntryHEAD
EntryRemoteTip
OldDigestOccurrenceCount
PreviousWrapperSHA256
NewWrapperSHA256
HelperSHA256
OnlyExpectedDigestLiteralChanged
ValidationMatrix
WindowsPowerShellVersion
WrapperParserErrorCount
HelperParserErrorCount

WrapperDigestCommit
FinalRemoteTip
CommitChangedPathCount
CommitChangedPaths
PushResult

DeployedImageDigest
WrapperExpectedDigest
DigestMatchResult
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount

TargetValidationRoot
AllocatedRunIds
ConsumedFailedRunIds
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
FailedPredicateCount
UnresolvedPredicateEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
MissingRequiredEvidenceClassCount
DurableReopenResult

QualificationResult
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
WrapperExitCode

FinalWorkingTreeClean
FinalStagedPathCount
FinalGitDiffCheck
FinalTargetStateRestored
PRCount
MergeCount
GitHubLifecycleMutationCount
WP07AcceptanceActionCount
TwelveDataSecretConfigured

BoundaryBlocker
FinalAcceptance
NextAuthorizedAction
```

On success:

```text
FinalAcceptance=READY_FOR_FINAL_LUNA
NextAuthorizedAction=GPT-5.6 Luna final read-only WP04 substantive acceptance reconciliation; final PR/merge/lifecycle remain unauthorized
```

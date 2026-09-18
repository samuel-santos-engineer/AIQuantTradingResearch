# Release 1.12 WP04 — Terra Resume Final Publication, Merge & Lifecycle Completion

**Selected execution model: GPT-5.6 Terra**

## 0. Binding authority state

WP04 substantive acceptance remains valid:

```text
RELEASE 1.12 WP04 — FINAL SUBSTANTIVE ACCEPTANCE: PASS
RELEASE 1.12 WP04 — READY_FOR_PUBLICATION_AND_LIFECYCLE: YES
```

The publication hash blocker has been reconciled by GPT-5.6 Luna:

```text
Decision=WRAPPER_HASH_RECORDING_ERROR
PublicationAuthorized=true
SourceByteChangeRequired=false
RuntimeRerunRequired=false
ImageRebuildRequired=false
SubstantiveAcceptanceRevoked=false
CorrectionSupplementRequired=false
OriginalPackageMutationRequired=false
TrackedCorrectionPublicationRequired=false
```

Canonical wrapper SHA-256:

```text
2DF9173EAEF15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
```

Superseded mistyped record — NEVER use as an acceptance/publication identity:

```text
2DF9173EAE15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
```

Helper SHA-256:

```text
7916106C11BEF92251EE49C7E0C0650744BE728CD42EE4071BF9D33E013F0541
```

Accepted release-branch commit:

```text
e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
```

Expected deployed image:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

No correction supplement or package mutation is required.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract/policy/architecture/final substantive acceptance and hash reconciliation

GPT-5.6 Terra
  selected:
  final publication validation
  PR creation
  merge
  post-merge verification
  WP04 lifecycle completion

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Terra**.

---

## 2. Mission

Resume the previously blocked final publication authority from the cleared hash boundary.

Do NOT rerun:
- WP04 runtime qualification;
- D01-D19;
- W1-W8;
- Azure qualification;
- Docker build;
- GHCR publication;
- image deployment.

Do NOT modify source to “correct” the hash. The committed bytes are canonical and already accepted.

Proceed through:
1. publication preflight;
2. current release→main scope reconciliation;
3. final validation;
4. final WP04 PR;
5. merge;
6. post-merge verification;
7. issue #263 closure;
8. Project #2 Done;
9. milestone #63 remains Open.

Continue internally through correctable administrative/transient defects until lifecycle COMPLETE or a true governance/source boundary is reached.

---

## 3. Publication preflight

Fetch/read remote state.

Require:

```text
HEAD=e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
LiveRemoteReleaseTip=e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
TrackedModifications=0
StagedPaths=0
git diff --check=PASS
```

Existing untracked disposable artifacts:
- preserve;
- do not stage;
- do not delete merely for cleanliness.

Recompute actual wrapper SHA and require:

```text
WrapperSHA256=
2DF9173EAEF15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
```

Require helper exact SHA above.

If the superseded mistyped hash appears in an execution authority/check variable, replace it only in ephemeral execution context; do not mutate tracked source or the accepted durable package.

---

## 4. Current publication topology — recompute, never assume

Fetch current `origin/main`.

Compute:

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

Historical `17 commits / 13 paths` is NOT authoritative.

For every release→main changed path:

```text
Path
ChangeType
WP04Relation
Reason
READMEPath
WP05Path
WP07Path
Unrelated
```

Require:

```text
UnrelatedContaminationCount=0
WP05ImplementationIncluded=0
WP07ImplementationIncluded=0
UnauthorizedREADMEInformationLoss=0
```

README preservation policy remains binding:

```text
EXISTING README INFORMATION → PRESERVE
PRESERVE INFORMATION; UPDATE STATUS
SUMMARY ≠ AUTHORITY TO DELETE DETAIL
README PRESERVATION — ZERO UNAUTHORIZED INFORMATION LOSS: PASS
```

No README mutation is authorized.

If contamination exists, HARD STOP before PR creation with the full path matrix.

---

## 5. Final validation

The previous publication attempt already reported:

```text
Build=PASS, 0 warnings, 0 errors
Tests=375 passed, 0 failed
WP04 PowerShell parsing=22 scripts, 0 errors
Helper identity=PASS
```

Because no source bytes changed and Luna proved the wrapper mismatch was metadata-only, Terra may carry these immediately preceding validation results forward if their provenance is attributable to the same exact commit and working tree remains clean.

At minimum revalidate cheap identity/cleanliness gates:

```text
HEAD
remote tip
wrapper SHA
helper SHA
git diff --check
tracked/staged cleanliness
```

If repository policy requires build/tests/checks again for PR publication, run them.

Signing contract remains binding:
- Release build 0 warnings/errors; no Release Authenticode requirement.
- Debug local signing required where applicable.
- expected signer `CN=AIQuantTradingDev`.
- never bypass signing.

A source defect requiring tracked edits is a HARD STOP.

---

## 6. PR discovery / duplicate prevention

Read-only check whether an equivalent final WP04 PR from the accepted release branch to `main` already exists.

The prior attempt reported no PR creation, so expected:

```text
EquivalentExistingPR=false
```

If an equivalent open PR exists due external/manual action:
- reuse it;
- verify exact head/base/scope;
- do not create a duplicate.

If an equivalent merged PR already exists:
- reconcile its merge SHA and proceed to post-merge/lifecycle verification;
- do not merge again.

---

## 7. Create final WP04 PR

If no equivalent PR exists, create exactly one PR:

```text
Base=main
Head=accepted WP04 release branch
Title=Release 1.12 WP04 — Persistent SQLite Initialization, Data Update & Recovery
```

Body must summarize:
- WP04 mission;
- application-owned SQLite initialization/update/reopen/readback;
- `/home/data/aiquant.db`;
- schema v4;
- DELETE journal;
- governed evidence/qualification;
- D01-D19 19/19;
- final substantive Luna PASS;
- canonical wrapper SHA `2DF9173EAEF15D3...`;
- immutable image digest;
- Policy A: restart/recycle/redeploy belongs to WP07;
- Twelve Data belongs to WP05;
- validation results;
- no production-SLA claim.

Reference #263 according to repository convention without violating lifecycle ordering.

Record:

```text
PRNumber
PRUrl
PRHeadSHA
PRBaseSHA
PRChangedPathCount
PRChangedPaths
```

Require PR changed paths exactly match the reconciled accepted WP04 scope.

---

## 8. Checks and merge readiness

Evaluate ALL applicable checks.

Require:
- no unexpected path;
- no conflict;
- required checks PASS or repository policy explicitly permits merge;
- PR head still equals accepted release tip;
- no new release-branch commit;
- base movement understood.

If `main` advances:
- fetch;
- recompute merge base/delta/scope;
- continue if accepted WP04 semantics and scope remain intact;
- HARD STOP if conflict/semantic contamination requires source change or new Luna decision.

Do not force through failed required checks.

---

## 9. Merge

Merge using the repository's established merge method.

Record:

```text
PRNumber
MergeMethod
MergeSHA
MergedAt
```

No force push.
No tag/version/release publication.
No user/manual mutation may be attributed to Terra.

---

## 10. Post-merge verification

Fetch origin and verify:

```text
PRState=MERGED
MergeSHA=<exact>
origin/main contains merged accepted WP04 content
canonical wrapper bytes/hash present as applicable
helper identity/content present
README preservation=PASS
WP05 contamination=0
WP07 contamination=0
```

Run any established post-merge repository smoke validation required by policy.

Do NOT rerun Azure qualification.

Require:

```text
PostMergeVerification=PASS
```

Lifecycle mutation is forbidden until this gate passes.

---

## 11. Close WP04 lifecycle

After post-merge verification:

### Issue #263

Close #263.

Add/retain a concise completion record containing:
- PR number;
- merge SHA;
- final substantive acceptance PASS;
- D01-D19 19/19;
- canonical wrapper SHA;
- WP07 deferral;
- WP05 next.

Record exact issue mutation count.

### Project #2

After issue closure, refresh Project #2.

If issue closure automatically changes WP04 Status to Done:

```text
AutoProjectStatusTransition=true
ExplicitProjectStatusMutationCount=0
```

Do NOT redundantly update it.

Otherwise explicitly set WP04 Project #2 Status to `Done` exactly once.

### Milestone #63

Require:

```text
Milestone63=OPEN
Milestone63MutationCount=0
```

Do not close the milestone.

---

## 12. WP05 boundary

After lifecycle completion:

```text
WP04Lifecycle=COMPLETE
WP05State=NOT_STARTED_READY_FOR_NEXT_AUTHORITY
WP07State=NOT_STARTED
```

Do not begin WP05 implementation.
Do not configure Twelve Data secrets.

---

## 13. Administrative convergence

Do not return after a correctable administrative/transient failure.

Internally retry/reconcile:
- GitHub connectivity;
- PR lookup;
- PR metadata;
- API transient errors;
- required-check polling;
- Project item lookup/refresh;
- issue state refresh;
- local disposable validation-environment problems.

Continue until lifecycle COMPLETE.

HARD STOP only if completion requires:
- tracked source mutation;
- accepted-content rewrite/rebase;
- force push;
- source fix for required check;
- unrelated contamination needing source mutation;
- new Luna decision;
- Azure/runtime/image mutation;
- WP05/WP07 implementation.

On hard stop return an exhaustive blocker matrix, not first failure only.

---

## 14. Mutation accounting

Record actual counts:

```text
PRCreateMutationCount
PRMergeMutationCount
IssueCommentMutationCount
IssueCloseMutationCount
ExplicitProjectStatusMutationCount
AutoProjectStatusTransition
MilestoneMutationCount
GitCommitCount
GitPushCount
ForcePushCount
AzureMutationCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
TwelveDataSecretMutationCount
WP07ActionCount
READMETrackedMutationCount
```

Expected:
- PR create 1 unless equivalent PR exists;
- merge 1 unless already merged;
- issue close 1 if initially open;
- explicit Project update 0 or 1;
- all source/runtime/image/WP07/README mutations 0.

Count only actual actions.

---

## 15. Completion gate

Success requires:

```text
CanonicalWrapperSHA256=
2DF9173EAEF15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A

FinalPR=MERGED
PostMergeVerification=PASS
Issue263=CLOSED
Project2WP04Status=DONE
Milestone63=OPEN
WP05=NOT_STARTED
WP07=NOT_STARTED
TrackedSourceMutationCount=0
AzureMutationCount=0
READMETrackedMutationCount=0
```

Emit:

```text
RELEASE 1.12 WP04 — WRAPPER HASH BLOCKER: CLEARED
RELEASE 1.12 WP04 — FINAL PUBLICATION: PASS
RELEASE 1.12 WP04 — PR MERGE: PASS
RELEASE 1.12 WP04 — POST-MERGE VERIFICATION: PASS
RELEASE 1.12 WP04 — ISSUE #263: CLOSED
RELEASE 1.12 WP04 — PROJECT #2 STATUS: DONE
RELEASE 1.12 WP04 — MILESTONE #63: OPEN
RELEASE 1.12 WP04 — LIFECYCLE: COMPLETE
RELEASE 1.12 WP04 — WP05: READY_FOR_NEXT_AUTHORITY
```

---

## 16. Required handoff

Return:

```text
SelectedModel

CanonicalWrapperSHA256
SupersededWrapperHashRecord
WrapperHashReconciliationResult

PrePublicationMain
PrePublicationOriginMain
PrePublicationReleaseTip
MergeBase
ReleaseOnlyCommitCount
MainOnlyCommitCount
ChangedPathCount
ChangedPathList
UnrelatedContaminationCount
UnauthorizedREADMEInformationLoss
WP05ImplementationIncluded
WP07ImplementationIncluded

BuildResult
BuildWarningCount
BuildErrorCount
TestResult
TestPassedCount
TestFailedCount
PowerShellVersion
PowerShellValidationResult
SigningValidationResult
GitDiffCheckResult

EquivalentExistingPR
PRNumber
PRUrl
PRHeadSHA
PRBaseSHA
PRChangedPathCount
PRChangedPaths
PRChecksResult
PRMergeability

MergeMethod
MergeSHA
MergedAt

PostMergeOriginMain
PostMergeVerification
PostMergeReadmePreservation
PostMergeWP05Contamination
PostMergeWP07Contamination

Issue263InitialState
Issue263FinalState
IssueCommentMutationCount
Issue263CloseMutationCount

Project2WP04InitialStatus
Project2WP04FinalStatus
ExplicitProjectStatusMutationCount
AutoProjectStatusTransition

Milestone63InitialState
Milestone63FinalState
Milestone63MutationCount

PRCreateMutationCount
PRMergeMutationCount
GitCommitCount
GitPushCount
ForcePushCount
AzureMutationCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
TwelveDataSecretMutationCount
WP07ActionCount
READMETrackedMutationCount

FinalLocalHEAD
FinalOriginMain
FinalWorkingTreeTrackedClean
FinalStagedPathCount
FinalGitDiffCheck

WP04Lifecycle
WP05State
WP07State
BoundaryBlocker
NextAuthorizedAction
```

On success:

```text
WP04Lifecycle=COMPLETE
WP05State=NOT_STARTED_READY_FOR_NEXT_AUTHORITY
WP07State=NOT_STARTED
NextAuthorizedAction=Begin Release 1.12 WP05 under its separate governed authority
```

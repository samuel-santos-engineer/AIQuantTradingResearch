# Release 1.12 WP04 — Terra Final Publication, Merge & Lifecycle Completion

**Selected execution model: GPT-5.6 Terra**

## 0. Binding Luna acceptance

This authority executes the publication/lifecycle consequence of:

```text
RELEASE 1.12 WP04 — FINAL SUBSTANTIVE ACCEPTANCE: PASS
RELEASE 1.12 WP04 — D01-D19: 19/19 PASS_OR_ACCEPTED_CARRY_FORWARD
RELEASE 1.12 WP04 — D14 RESTORATION EVIDENCE: PASS
RELEASE 1.12 WP04 — D18 MUTATION ACCOUNTING: PASS
RELEASE 1.12 WP04 — D19 DURABLE EVIDENCE: PASS
RELEASE 1.12 WP04 — HYBRID W1-W8: ACCEPTED
RELEASE 1.12 WP04 — CANONICAL OWNERSHIP: WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
RELEASE 1.12 WP04 — WP07 DEPLOYMENT RECOVERY: DEFERRED_TO_WP07
RELEASE 1.12 WP04 — TWELVE DATA CONFIGURATION: DEFERRED_TO_WP05
RELEASE 1.12 WP04 — FINAL TARGET STATE: RESTORED
RELEASE 1.12 WP04 — READY_FOR_PUBLICATION_AND_LIFECYCLE: YES
FinalAcceptance=PASS
```

No substantive WP04 runtime validation is to be repeated.

---

## 1. Model authority

```text
GPT-5.6 Luna
  contract/policy/architecture/final substantive acceptance

GPT-5.6 Terra
  selected:
  publication validation
  PR creation
  merge
  post-merge verification
  WP04 lifecycle completion

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Terra**.

---

## 2. Final accepted WP04 identities

Expected release-branch tip before publication:

```text
e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
```

Expected wrapper SHA256:

```text
2DF9173EAE15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
```

Expected helper SHA256:

```text
7916106C11BEF92251EE49C7E0C0650744BE728CD42EE4071BF9D33E013F0541
```

Expected deployed image:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

Accepted durable evidence root:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\target-validation-final\890ac450f6324238b3dcc21c03bf677e
```

Successful consumed RunIds:

```text
initialize-69fcf93472fe44c586d8905f72cdf36f
reopen-5aded8b772a048a49aebca52327ee571
```

Failed consumed/no-credit RunId:

```text
initialize-d827cb88d67c4d6f93868090924342b2
```

Never reuse any WP04 RunId.

---

## 3. Publication preflight

Before mutation, fetch remote refs read-only and establish exact current state.

Require:

```text
CurrentReleaseTip=e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
LiveRemoteReleaseTip=e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
TrackedModifications=0
StagedPaths=0
git diff --check=PASS
```

Preserve existing untracked disposable artifacts. Do not stage/delete/modify them.

Read-only verify:
- WP04 #263 is OPEN;
- milestone #63 is OPEN;
- no already-open equivalent WP04 final PR exists;
- Project #2 WP04 item is not already Done.

If an equivalent PR already exists, reconcile/reuse it rather than creating a duplicate.

---

## 4. Recompute publication topology — mandatory

Historical counts are NOT authoritative.

Immediately recompute:

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

Use the current live `origin/main`.

For every release→main changed path produce:

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

README policy is binding:

```text
EXISTING README INFORMATION → PRESERVE
PRESERVE INFORMATION; UPDATE STATUS
SUMMARY ≠ AUTHORITY TO DELETE DETAIL
README PRESERVATION — ZERO UNAUTHORIZED INFORMATION LOSS: PASS
```

Do not modify README in this authority.

If any unrelated path exists, HARD STOP before PR creation with the complete path matrix.

---

## 5. Accepted WP04 scope

The accumulated branch may contain multiple historical WP04 commits. That is expected.

The final PR must publish the complete accepted WP04 delta, including the persistence implementation and its governed deployment/qualification tooling.

Do not squash/rewrite/rebase merely to reduce commit count unless repository policy explicitly requires it. No force push.

Do not include WP05 or WP07 implementation.

---

## 6. Final local validation

From the exact accepted branch tip, run the repository's established validation appropriate for final WP publication.

At minimum:
- required .NET build under the established Release/Debug signing contract;
- complete test suite appropriate to current repository baseline;
- PowerShell parser/validation for WP04 scripts under Windows PowerShell 5.1.26100.9444;
- `git diff --check`;
- repository cleanliness;
- accepted wrapper/helper hashes unchanged.

Signing contract:

```text
Release build:
  Authenticode signature not required
  0 warnings
  0 errors

Debug local signing:
  required
  expected signer CN=AIQuantTradingDev
  existing Debug-only AutoSignTestBinaries
```

Never bypass signing.

If a validation failure is due to a tracked-source defect, HARD STOP. This publication authority does not authorize new substantive source edits after Luna acceptance.

Disposable local validation-environment issues may be corrected without tracked mutation and validation retried.

---

## 7. PR creation

Create exactly one final WP04 PR from the accepted release branch to `main`.

PR title should clearly identify:

```text
Release 1.12 WP04 — Persistent SQLite Initialization, Data Update & Recovery
```

PR body must summarize:
- WP04 mission;
- application-owned SQLite initialization/update/reopen/readback;
- `/home/data/aiquant.db`;
- schema v4;
- DELETE journal;
- governed qualification/evidence;
- final D01-D19 acceptance;
- Policy A: deployment restart/recycle/redeploy remains WP07;
- Twelve Data remains WP05;
- immutable deployed image identity;
- validation/build/test results;
- Luna final substantive acceptance;
- no production-SLA claim.

Reference issue #263 according to repository convention, but do NOT allow PR creation itself to prematurely close #263 before merge/post-merge verification.

If using a closing keyword would close only on merge, it is acceptable if lifecycle ordering remains:
`merge → post-merge verification → issue closure`.
Prefer explicit lifecycle control if there is ambiguity.

Record:

```text
PRNumber
PRUrl
PRHeadSHA
PRBaseSHA
PRChangedPathCount
PRChangedPaths
```

Verify PR changed paths exactly equal the reconciled accepted release→main WP04 scope.

---

## 8. PR checks / merge readiness

Read-only inspect required checks and mergeability.

Require:
- no unexpected changed path;
- no merge conflict;
- required checks PASS or repository policy permits merge;
- head SHA still equals accepted release tip;
- base SHA/current main relationship understood;
- no new commits appeared on the branch.

If main advanced after preflight, recompute the exact delta and scope before merge. If the advancement creates conflict or changes accepted semantics, HARD STOP for reconciliation.

Do not force merge through failed required checks.

---

## 9. Merge

When merge-ready, merge the final WP04 PR using the repository's established merge method.

Do not attribute any user/manual mutation to Terra.

Record exactly:

```text
PRNumber
MergeMethod
MergeSHA
MergedAt
```

No force push.

No release tag/version publication in this WP authority.

---

## 10. Post-merge verification

After merge:

```text
fetch origin
verify origin/main == expected merged state
verify PR state = MERGED
verify merge SHA
verify accepted WP04 paths are present on main
verify wrapper/helper/source identities as applicable
verify no accidental README information loss
verify no WP05/WP07 implementation contamination
```

Run appropriate post-merge smoke/repository validation if established by project policy.

Do not rerun governed Azure qualification merely because the PR merged.

The accepted deployed image remains intentionally deployed.

Require:

```text
PostMergeVerification=PASS
```

Only after this PASS may WP04 lifecycle close.

---

## 11. GitHub lifecycle completion

Binding lifecycle order:

```text
Substantive acceptance PASS
→ PR merge
→ post-merge verification PASS
→ close #263
→ Project #2 Status Done
```

Milestone #63 MUST remain OPEN.

WP05 must remain NOT_STARTED until #263 is closed and Project status is Done.

### Issue #263

After post-merge verification, close #263 with a concise completion comment/record containing:
- PR number;
- merge SHA;
- final acceptance marker;
- D01-D19 19/19;
- WP07 deferral;
- WP05 next.

### Project #2

After issue closure, inspect Project #2 status.

If closing #263 automatically changed the Project item to Done:

```text
ExplicitProjectStatusMutationCount=0
AutoProjectStatusTransition=true
```

Do NOT perform a redundant status mutation.

If it did not auto-update, explicitly set the WP04 Project #2 item Status to `Done` exactly once.

Record actual mutation accounting.

### Milestone

Require:

```text
Milestone63State=OPEN
Milestone63CloseMutationCount=0
```

---

## 12. WP05 transition boundary

After lifecycle completion, establish only:

```text
WP04Lifecycle=COMPLETE
WP05Next=AUTHORIZED_FOR_PLANNING/EXECUTION_BY_SEPARATE_AUTHORITY
```

Do NOT begin WP05 implementation in this authority.

Do not configure Twelve Data secrets.

---

## 13. Mutation accounting

Record exact actual counts for:

```text
PRCreate
PRMerge
IssueComment
IssueClose
ProjectStatusExplicitUpdate
ProjectStatusAutomaticTransition
MilestoneMutation
GitCommit
GitPush
ForcePush
AzureMutation
DockerBuild
GhcrPublication
ImageDeployment
TwelveDataSecretMutation
WP07Action
READMETrackedMutation
```

Expected:
- PR create = 1 unless an equivalent PR already existed;
- PR merge = 1;
- issue close = 1 if issue initially open;
- Project explicit update = 0 or 1 depending on automation;
- milestone mutation = 0;
- Git commit/push = 0;
- force push = 0;
- Azure/Docker/GHCR/image mutation = 0;
- WP07 action = 0;
- Twelve Data secret mutation = 0;
- README mutation = 0.

Count only actions actually performed.

---

## 14. Convergence behavior

Within this publication/lifecycle authority, do not return after a correctable non-governance administrative defect.

Terra may internally retry/reconcile:
- transient GitHub connectivity;
- read-only API failures;
- PR metadata formatting;
- duplicate PR detection;
- Project item lookup;
- lifecycle state refresh;
- local disposable validation-environment issues.

Continue until publication/lifecycle completes.

HARD STOP if completion requires:
- tracked source change;
- rebase/rewrite that changes accepted content;
- force push;
- unrelated path removal by source mutation;
- failed required check that needs source correction;
- new Luna acceptance/policy decision;
- Azure/runtime/image mutation;
- WP05/WP07 implementation.

Return an exhaustive blocker matrix on hard stop.

---

## 15. Completion gate

Success requires:

```text
FinalPR=Merged
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

Then emit:

```text
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

ReleaseBuildResult
ReleaseBuildWarningCount
ReleaseBuildErrorCount
TestResult
TestPassedCount
TestFailedCount
PowerShellVersion
PowerShellValidationResult
SigningValidationResult
GitDiffCheckResult

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
Issue263CloseMutationCount
IssueCompletionCommentResult

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

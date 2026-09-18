# Release 1.12 WP04 — Luna Intermediate PR Scope Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Mission

Resolve the narrow publication-scope blocker discovered after the accepted WP04 orchestration bytes were committed and pushed to the governed release branch.

Current state:

```text
Commit/remote branch tip=bf91221ec51baa18e878d2aad1184431ca235cdd
Push=PASS, non-force

Wrapper SHA256=
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C

Helper SHA256=
5ED81167F81287AC2614546C063C938F8F747DE664190CF95BF0E2D125C9B690

Local tracked modifications=0
Staged paths=0
git diff --check=PASS

Release build=PASS, 0 warnings, 0 errors
Application tests=136 PASS
Domain tests=11 PASS
Architecture tests=27 PASS
Infrastructure tests=201 PASS
Targeted qualification tests=5 PASS
```

Terra correctly stopped before PR creation/merge because the prospective release-branch → `main` PR contains **17 paths**, while the prior intermediate-publication authority permitted only the two orchestration paths.

No PR was created.
No merge occurred.
No Azure mutation occurred.
No RunId was allocated.
No image/GHCR mutation occurred.
WP04 final acceptance has not occurred.

This authority must decide the correct Git/PR publication boundary without mutating anything.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract, policy, architecture, provenance, Git/PR scope governance

GPT-5.6 Terra
  implementation/validation and explicitly authorized Git/GitHub/Azure mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Luna**.

---

## 2. Preserve the successful commit/push

The commit:

```text
bf91221ec51baa18e878d2aad1184431ca235cdd
```

is an authorized intermediate implementation/provenance mutation already performed by Terra.

Do not characterize it as unauthorized merely because PR creation was subsequently blocked.

Determine:

```text
CommitPublicationCredit=VALID|INVALID
RemoteBranchPublicationCredit=VALID|INVALID
```

The expected result is VALID if the commit contains only the two authorized paths and exact validated bytes.

Verify read-only.

Do not rewrite, revert, cherry-pick, or amend it in this authority.

---

## 3. Recover the 17-path PR delta

Read-only compute the exact prospective PR delta:

```text
release branch → main
```

Return all 17 paths and classify each:

```text
Path
OriginCommit
WP04Relationship
PreviouslyAuthorized=true|false
PreviouslyValidated=true|false
AlreadyPublishedElsewhere=true|false
IntendedForWP04FinalPR=true|false|NOT_PROVEN
RiskIfIncluded
```

Do not assume that “17 paths” means contamination.

Determine whether the additional 15 paths are:
- prior authorized WP04 implementation commits accumulated on the governed release branch;
- unrelated release work;
- already merged to main through another PR;
- or a mixture.

---

## 4. Recover branch topology

Read-only report:

```text
main HEAD
origin/main HEAD
release branch HEAD
merge-base(main, release branch)
commits reachable from release branch but not main
commits reachable from main but not release branch
```

For every release-only commit, classify:

```text
CommitSHA
Purpose
Paths
PriorAuthority
WP04Scope
PublicationState
```

Use retained project history/authorities to identify whether those commits were authorized WP04 implementation work.

Do not infer authorization solely from commit messages.

---

## 5. Distinguish commit scope from PR scope

The prior Terra authority authorized **publishing the two accepted dirty paths**, but the release branch may legitimately contain earlier WP04 implementation commits.

Determine whether the correct policy is:

### S1 — PR may contain the full accumulated authorized WP04 implementation delta

Use only if all 17 paths are already-authorized WP04 implementation/provenance work intended to reach main as one WP04 implementation PR.

### S2 — create a two-path publication branch/PR from main

Use only if the clean-HEAD target-validation requirement can be satisfied on a branch based on main containing just commit `bf91221...`'s exact two-file change, without losing required WP04 source dependencies or falsifying provenance.

This may require cherry-picking the exact commit onto a clean publication branch. Treat that as a new Git mutation requiring Terra authority.

### S3 — first publish/merge earlier authorized WP04 implementation commits, then publish the two-file provenance commit

Use only if the 17-path delta consists of multiple valid but separately governed publication units that must remain distinct.

### S4 — branch topology needs another explicit correction

Use only if none of S1-S3 preserves authority and provenance.

Do not select based on smallest diff alone.

---

## 6. Critical clean-HEAD requirement

The target-validation wrapper requires:

```text
git diff --quiet HEAD -- <wrapper> = PASS
```

Clarify whether this requires:
- the wrapper merely committed at the execution branch HEAD; or
- the wrapper merged to `main`.

Inspect the actual script.

Return:

```text
CleanHeadRequiresLocalCommit=true|false
CleanHeadRequiresRemoteBranchPublication=true|false
CleanHeadRequiresMainMerge=true|false
```

This is critical.

If the executable provenance check only requires the wrapper to be committed at the execution branch HEAD, then the already-pushed commit `bf91221...` may have fully resolved the target-execution blocker **without any intermediate PR/merge**.

Do not require a main merge unless the actual provenance contract or project governance requires it.

---

## 7. Determine whether target validation can proceed now

Evaluate the current release branch at:

```text
bf91221ec51baa18e878d2aad1184431ca235cdd
```

against the exact target-validation preflight:

```text
wrapper HEAD SHA == accepted SHA
wrapper working-tree SHA == HEAD
helper HEAD SHA == accepted SHA
helper working-tree SHA == HEAD
git diff --quiet HEAD wrapper=PASS
git diff --quiet HEAD helper=PASS
staged paths=0
git diff --check=PASS
required branch/provenance conditions
```

Return:

```text
CurrentReleaseBranchCleanHeadProvenance=PASS|FAIL
CurrentReleaseBranchReadyForTargetValidation=true|false
```

If true, explicitly determine whether PR/merge can be deferred until final WP04 publication after substantive acceptance.

---

## 8. Publication sequencing options

Evaluate:

### Q1 — proceed to target validation from governed release branch now

```text
bf91221... is committed/pushed
clean-HEAD provenance passes
PR/merge deferred to final WP04 publication
```

This avoids broadening the intermediate PR authority.

### Q2 — authorize the full 17-path WP04 implementation PR now

Only if every path is proven authorized and intended for WP04 publication.

### Q3 — create a narrow two-file PR topology

Only if main merge is actually required before target execution.

For each return:

```text
Option
GovernanceValidity
ProvenanceValidity
MutationRequirement
EvidenceImpact
FinalPublicationImpact
```

---

## 9. Preferred governance principle

Prefer the narrowest action that satisfies the **actual executable provenance contract** without:
- weakening provenance;
- inventing a main-merge requirement;
- broadening PR scope without evidence;
- duplicating/cherry-picking unnecessarily;
- losing already-authorized WP04 implementation history.

If `bf91221...` on the governed release branch already makes clean-HEAD provenance true, the default should be to preserve that branch and defer PR/merge until final WP04 acceptance, unless another canonical rule requires earlier main publication.

---

## 10. No mutation authority

This Luna authority performs no:

```text
Git mutation
GitHub mutation
Azure mutation
RunId allocation
Docker/GHCR mutation
source edit
branch creation
cherry-pick
PR creation
merge
issue/project/milestone mutation
```

---

## 11. Decision

Choose exactly one:

```text
PR_SCOPE_DECISION=INTERMEDIATE_PR_NOT_REQUIRED__TARGET_VALIDATION_FROM_COMMITTED_RELEASE_BRANCH
```

or

```text
PR_SCOPE_DECISION=FULL_17_PATH_WP04_INTERMEDIATE_PR_AUTHORIZED
```

or

```text
PR_SCOPE_DECISION=NARROW_PUBLICATION_BRANCH_REQUIRED
```

or

```text
PR_SCOPE_DECISION=SEPARATE_PRIOR_WP04_PUBLICATION_REQUIRED
```

or

```text
PR_SCOPE_DECISION=POLICY_AMBIGUITY_REMAINS
```

---

## 12. Required terminal markers

Always:

```text
RELEASE 1.12 WP04 — INTERMEDIATE PR SCOPE RECONCILIATION: COMPLETE
RELEASE 1.12 WP04 — COMMIT bf91221: PRESERVED_PENDING_DECISION
RELEASE 1.12 WP04 — AZURE MUTATIONS: 0
RELEASE 1.12 WP04 — RUNIDS ALLOCATED: 0
RELEASE 1.12 WP04 — WP04 FINAL ACCEPTANCE: NOT_YET
RELEASE 1.12 WP04 — WP05: NOT_STARTED
RELEASE 1.12 WP04 — WP07: NOT_STARTED
```

Then emit the exact PR scope decision.

---

## 13. Required handoff

Return:

```text
CommitPublicationCredit
RemoteBranchPublicationCredit

ProspectivePRPathCount
ProspectivePRPathMatrix

MainHead
OriginMainHead
ReleaseBranchHead
MergeBase
ReleaseOnlyCommitMatrix

CleanHeadRequiresLocalCommit
CleanHeadRequiresRemoteBranchPublication
CleanHeadRequiresMainMerge

CurrentReleaseBranchCleanHeadProvenance
CurrentReleaseBranchReadyForTargetValidation

Q1GovernanceValidity
Q1ProvenanceValidity
Q2GovernanceValidity
Q2ProvenanceValidity
Q3GovernanceValidity
Q3ProvenanceValidity

SelectedResolution
RequiredNextMutations
ForbiddenNextMutations
TargetValidationAnchor
FinalPublicationPlan

BoundaryBlocker
FinalDecision
NextAuthorizedAction
```

No mutations.

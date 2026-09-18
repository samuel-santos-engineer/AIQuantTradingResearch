# Release 1.12 WP04 — Luna Wrapper Provenance Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Mission

Resolve one narrow governance contradiction blocking the already-defined WP04 target validation:

```text
Authority entry permits two pre-existing tracked WP04 modifications.

But initialize-qualification.ps1 requires:
git diff --quiet HEAD -- eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Current facts:

```text
HEAD=2532f6abd4677edfb205c26c083a534783038979
wrapper working-tree SHA256=
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C

wrapper is an uncommitted tracked modification relative to HEAD
AllocatedRunIds=0
AzureMutations=0
AuthorityIntroducedTrackedMutationCount=0
StagedPathCount=0
```

Terra correctly refused to:
- bypass the wrapper;
- invoke the helper directly;
- alter/copy orchestration to evade provenance;
- commit the wrapper without authority.

This is a **source-provenance/governance reconciliation only**.

No Azure.
No RunIds.
No source mutation.
No staging/commit/push.
No GitHub mutation.
No image build/publication.
No WP05/WP07 execution.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract, policy, architecture, provenance, governance, acceptance reconciliation

GPT-5.6 Terra
  implementation/validation and explicitly authorized Git/GitHub/Azure mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Luna**.

---

## 2. Fixed accepted history

Preserve the accepted history that led to the current wrapper bytes.

The wrapper working-tree identity:

```text
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

has already been used and accepted throughout the final W5/W6/W7/W8 chain and final hybrid W1-W8 reconciliation.

Hybrid W1-W8 remains:

```text
ACCEPTED
```

Do not reopen W5-W8 merely because the accepted wrapper is not yet committed at HEAD.

The current source anchor:

```text
2532f6abd4677edfb205c26c083a534783038979
```

predates the uncommitted lifecycle-remediation wrapper bytes.

No image rebuild occurred after those wrapper changes.

---

## 3. Inspect the exact provenance contract

Read-only inspect:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

and the retained authorities/evidence that introduced and accepted:

```text
H1 Deferred → checkpoint → RestoreOnly
S2 checkpoint seam
PowerShell 5.1 fixes
W3 fix
W5 final remediation
W6
W7
W8
hybrid W1-W8 final reconciliation
```

Identify:

```text
where git diff --quiet HEAD -- <wrapper> is enforced
what security/provenance property it was intended to prove
whether the check assumes the approved wrapper is already committed
whether exact-byte SHA/source-copy checks independently prove the approved wrapper identity
```

Return:

```text
CleanHeadCheckLocation
CleanHeadCheckPurpose
CleanHeadAssumption
ApprovedWrapperIdentitySource
IndependentExactByteControls
```

---

## 4. Reconcile the two provenance concepts

Distinguish:

### Repository cleanliness

```text
working-tree wrapper bytes == HEAD wrapper bytes
```

from:

### Approved execution identity

```text
working-tree wrapper bytes == explicitly accepted authority SHA
```

Determine which property is actually required **before executing the final WP04 target validation**.

Do not weaken provenance merely for convenience.

Specifically determine whether the existing clean-HEAD check is:

```text
A. a mandatory security invariant that requires publication/commit before target execution;

B. an obsolete/over-constrained assumption because the governed wrapper was intentionally developed and accepted as an uncommitted WP04 modification, while exact SHA/copy/hash gates are the actual execution identity controls;

C. valid in principle but requiring a canonical publication boundary before any further Azure execution;

D. irreducibly ambiguous.
```

---

## 5. Consider publication sequencing

Canonical current workflow expected:

```text
substantive WP04 evidence
→ final Luna acceptance
→ Terra publication/PR/merge/lifecycle
```

But executable wrapper currently requires clean HEAD before substantive target evidence.

Determine whether this creates a legitimate need for an **intermediate source publication/commit boundary**.

Explicitly answer:

```text
MustWrapperBeCommittedBeforeTargetValidation=true|false
CanWP04PublicationOccurBeforeFinalAcceptance=true|false
If true, what exact limited publication is allowed?
Would that publication itself falsely imply WP04 acceptance?
Would image rebuild/publication be required?
```

Do not silently collapse implementation publication into WP04 final lifecycle closure.

---

## 6. Evaluate safe resolution options

Evaluate all of these, without executing them:

### Option P1 — publish accepted wrapper bytes first

A Terra authority would:
- validate exact accepted wrapper/helper bytes;
- stage only explicitly authorized WP04 paths;
- commit/push/PR/merge as an **implementation/provenance publication**, not WP04 acceptance;
- keep #263 Open and Project status non-Done;
- keep milestone #63 Open;
- perform no Azure validation in that publication authority unless separately authorized;
- then target validation runs from a clean committed anchor.

Determine whether this is governance-valid.

### Option P2 — narrowly change provenance check

A Terra authority would modify the wrapper so execution identity is based on accepted exact SHA/copy controls rather than `git diff --quiet HEAD`.

This is a source change and would invalidate the currently accepted wrapper SHA/evidence chain unless explicitly handled.

Determine whether this is necessary or inferior to P1.

### Option P3 — sanctioned clean execution copy

Run exact accepted wrapper bytes from a separately frozen clean execution location while preserving source identity checks.

Determine whether existing governance already permits this. Do not invent permission.

### Option P4 — bypass/helper direct execution

This is presumed forbidden. Confirm.

Return for each:

```text
Option
GovernanceValidity
EvidenceImpact
RequiredMutations
RecommendedForNextBoundary=true|false
Reason
```

Do not rank by convenience; choose the policy-consistent resolution.

---

## 7. Image relationship

Determine whether committing the accepted PowerShell orchestration changes the deployed application image contract.

Explicitly answer:

```text
WrapperIncludedInRuntimeImage=true|false
HelperIncludedInRuntimeImage=true|false
CommitRequiresImageRebuildForTargetValidation=true|false
```

Use repository/Dockerfile evidence.

Do not assume a rebuild is needed simply because source is committed.

If the scripts are deployment-side only and not part of the runtime image, say so.

---

## 8. Exact mutation boundary if publication is required

If Luna selects P1, define the exact Terra publication authority.

At minimum specify:

```text
AllowedTrackedPaths
ExpectedEntrySHA256
RequiredTests
RequiredPowerShellVersion
RequiredParserResult
CommitPurpose
PRPurpose
MergeRequirement
PostMergeVerification
IssueStateAfterMerge
ProjectStatusAfterMerge
Milestone63StateAfterMerge
WP05StateAfterMerge
AzureMutationCount
RunIdAllocationCount
ImageBuildCount
GhcrPublicationCount
```

Publication must not be represented as final WP04 acceptance.

README preservation remains binding; README mutation should be zero unless separately authorized.

Exact mutation accounting required.

If issue close automatically changes Project status, do not redundantly mutate it—but under this intermediate publication, issue closure is expected to remain forbidden.

---

## 9. Exact next validation anchor

If P1 is selected, define what Terra must prove after merge before the target-validation authority can execute:

```text
main/origin state if relevant
branch state
new committed source anchor
wrapper HEAD SHA
wrapper working-tree SHA
git diff --quiet HEAD wrapper=PASS
helper identity
staged paths=0
git diff --check=PASS
```

The later target validation must use the newly committed exact accepted wrapper bytes.

No historical RunId reuse.

---

## 10. Decision

Choose exactly one:

```text
PROVENANCE_DECISION=PUBLISH_ACCEPTED_WRAPPER_BEFORE_TARGET_VALIDATION
```

or

```text
PROVENANCE_DECISION=NARROW_PROVENANCE_CHECK_CHANGE_REQUIRED
```

or

```text
PROVENANCE_DECISION=SANCTIONED_CLEAN_EXECUTION_COPY
```

or

```text
PROVENANCE_DECISION=POLICY_AMBIGUITY_REMAINS
```

Do not authorize a bypass.

---

## 11. Required terminal markers

Always:

```text
RELEASE 1.12 WP04 — WRAPPER PROVENANCE RECONCILIATION: COMPLETE
RELEASE 1.12 WP04 — HYBRID W1-W8: ACCEPTED
RELEASE 1.12 WP04 — AZURE MUTATIONS: 0
RELEASE 1.12 WP04 — RUNIDS ALLOCATED: 0
RELEASE 1.12 WP04 — WP04 FINAL ACCEPTANCE: NOT_YET
RELEASE 1.12 WP04 — WP05: NOT_STARTED
RELEASE 1.12 WP04 — WP07: NOT_STARTED
```

Then emit the exact provenance decision.

---

## 12. Required handoff

Return:

```text
CleanHeadCheckLocation
CleanHeadCheckPurpose
CleanHeadAssumption
ApprovedWrapperIdentitySource
IndependentExactByteControls

MustWrapperBeCommittedBeforeTargetValidation
CanWP04PublicationOccurBeforeFinalAcceptance
IntermediatePublicationSemantics

P1GovernanceValidity
P1EvidenceImpact
P2GovernanceValidity
P2EvidenceImpact
P3GovernanceValidity
P3EvidenceImpact
P4GovernanceValidity

WrapperIncludedInRuntimeImage
HelperIncludedInRuntimeImage
CommitRequiresImageRebuildForTargetValidation

SelectedResolution
AllowedTrackedPaths
RequiredValidationBeforePublication
RequiredPublicationMutations
ForbiddenPublicationMutations
RequiredPostMergeState
TargetValidationAnchorRule

BoundaryBlocker
FinalDecision
NextAuthorizedAction
```

No mutations.

# Release 1.12 WP04 — Terra Intermediate Implementation / Provenance Publication

**Selected execution model: GPT-5.6 Terra**

## Mission

Publish the **already accepted exact WP04 deployment-orchestration bytes** so the governed wrapper's clean-HEAD provenance preflight can become true before final Azure target validation.

This is an **intermediate implementation/provenance publication only**.

It is NOT:
- final WP04 acceptance;
- WP04 lifecycle completion;
- Azure target validation;
- image publication;
- WP05 or WP07 execution.

Binding Luna decision:

```text
PROVENANCE_DECISION=PUBLISH_ACCEPTED_WRAPPER_BEFORE_TARGET_VALIDATION
MustWrapperBeCommittedBeforeTargetValidation=true
CanWP04PublicationOccurBeforeFinalAcceptance=true
IntermediatePublicationSemantics=implementation/provenance publication only
```

No new source edits are authorized. The only byte changes to publish are the already-existing accepted working-tree bytes in the two explicitly allowed paths.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract, policy, architecture, provenance, governance, acceptance

GPT-5.6 Terra
  validation and explicitly authorized Git/GitHub publication mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Terra**.

---

## 2. Fixed entry state

Expected entry source anchor:

```text
HEAD=2532f6abd4677edfb205c26c083a534783038979
```

Allowed pre-existing tracked modifications only:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Expected accepted wrapper working-tree SHA256:

```text
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

Frozen runner remains accepted:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Hybrid W1-W8 remains:

```text
ACCEPTED
```

Do not rerun W5-W8.

---

## 3. Absolute byte-preservation rule

This authority does **not** authorize editing either allowed tracked path.

Before staging, capture:

```text
EntryWrapperSHA256
EntryHelperSHA256
EntryDiffForWrapper
EntryDiffForHelper
```

The bytes staged and committed must be exactly the bytes present at authority entry.

For the wrapper require:

```text
EntryWrapperSHA256=61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
```

If not exact, STOP with zero publication mutation.

For the helper, recover its accepted current identity from retained final WP04 evidence/authority and prove the working-tree helper bytes equal that accepted identity before staging.

If the helper accepted identity cannot be proven, STOP. Do not infer or normalize it.

No formatter.
No newline normalization.
No generated rewrite.
No comment edit.
No version bump.
No README edit.

---

## 4. Repository preflight

Before staging require:

```text
Windows PowerShell=5.1.26100.9444
HEAD exact
expected branch confirmed
staged paths=0
only the two allowed tracked modifications exist
git diff --check=PASS
wrapper SHA exact
helper SHA accepted/exact
PowerShell AST parser errors=0 for both scripts
```

Also capture:

```text
git status --short
git diff --name-only
git diff --stat
git diff -- <allowed paths>
```

If any unrelated tracked/untracked condition would contaminate publication, fail closed unless it is demonstrably irrelevant and the canonical project publication rules permit leaving it untouched.

Never stage unrelated files.

---

## 5. Required local validation before publication

Run the existing applicable WP04 local validation suite without changing source.

At minimum:

```text
PowerShell AST parse for both scripts
existing wrapper/helper local validation
accepted source/hash/provenance checks that are meaningful pre-publication
git diff --check
relevant .NET tests for persistence contract if part of the established WP04 publication gate
Release build if required by the canonical WP04 signing/build contract
```

Preserve signing contract:

```text
Release build required
0 warnings
0 errors
Release Authenticode signature not required
Debug local signing required where Debug tests are invoked
Expected signer CN=AIQuantTradingDev
No signing bypass
```

Do not manufacture a clean-HEAD PASS before commit. The purpose of this authority is to make that predicate true by legitimate publication.

Return a complete validation matrix, not first-failure-only diagnostics.

If a validation failure requires byte changes, STOP. This authority does not authorize remediation edits.

---

## 6. README preservation

Binding:

```text
AIQUANTTRADINGRESEARCH — FRONT-DOOR README INFORMATION PRESERVATION POLICY: ACTIVE
README PRESERVATION — ZERO UNAUTHORIZED INFORMATION LOSS: PASS
```

README mutation count must be:

```text
0
```

Do not stage or edit README.

---

## 7. Publication mutation authority

Only after all pre-publication gates PASS, authorize:

```text
stage exactly the two allowed paths
commit exactly those staged bytes
push the publication branch
create/update the WP04 implementation/provenance PR as required
merge that PR after required checks pass
```

The commit/PR purpose must be described truthfully as:

```text
Publish accepted WP04 persistence qualification/orchestration implementation bytes required for governed source provenance before final target validation.
```

Do not describe WP04 as accepted/complete.

If an existing open PR is the canonical vehicle, use it rather than creating a duplicate, provided its scope and head are correct.

Do not force-push unless separately and explicitly allowed by canonical repository governance.

---

## 8. GitHub lifecycle restrictions

Forbidden:

```text
close #263
set Project #2 WP04 Status=Done
close milestone #63
change milestone state
begin/modify WP05
begin/modify WP07
```

After merge require:

```text
#263 remains OPEN
Project #2 WP04 status is NOT Done
milestone #63 remains OPEN
WP05 remains NOT_STARTED
WP07 remains NOT_STARTED
```

Use read-only GitHub verification where safely available.

Do not mutate status merely to verify it.

If issue close or project automation occurs unexpectedly, STOP and report the external governance mutation exactly; do not hide or counter-mutate without authority.

---

## 9. Azure/image restrictions

Exactly:

```text
AzureMutationCount=0
RunIdsAllocated=0
DockerBuildCount=0
GhcrPublicationCount=0
TwelveDataSecretConfigured=false
```

The wrapper/helper are deployment-side scripts and are not part of the deployed runtime image.

No image rebuild is authorized or required.

Expected deployed image digest remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

---

## 10. PR merge gate

Before merge require all canonical repository checks applicable to this implementation publication.

Record:

```text
CommitSHA
PushResult
PRNumber
PRHeadSHA
PRBase
PRChecks
MergeAuthorizationBasis
```

Do not merge if:
- PR contains paths outside the two allowed paths;
- wrapper/helper bytes differ from validated entry bytes;
- required checks fail;
- README appears in diff;
- publication would imply unauthorized lifecycle changes.

---

## 11. Post-merge clean-anchor verification

After merge, synchronize the canonical local branch/state according to project workflow without discarding unrelated user work.

Determine and record the new committed source anchor.

Require the committed accepted bytes to be provable at the new anchor:

```text
CommittedWrapperSHA256=
61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C

CommittedHelperSHA256=<accepted entry helper SHA>
```

Then require, in the execution checkout/branch that will be used for target validation:

```text
wrapper working-tree SHA == committed wrapper SHA
helper working-tree SHA == committed helper SHA
git diff --quiet HEAD -- <wrapper> = PASS
git diff --quiet HEAD -- <helper> = PASS
staged paths=0
git diff --check=PASS
```

Also prove no unauthorized path was introduced by this authority.

Return:

```text
MergeCommitSHA
PostMergeHead
PostMergeWrapperSHA256
PostMergeHelperSHA256
WrapperCleanHeadResult
HelperCleanHeadResult
```

---

## 12. Exact mutation accounting

Produce a ledger for every explicit mutation:

```text
MutationId
System=Git|GitHub
Operation
Target
Before
After
Reason
Result
```

At minimum distinguish:

```text
stage
commit
push
PR create/update
merge
```

Count only mutations actually performed.

Do not count read-only queries.

Do not attribute user/manual actions to Terra.

If GitHub automatically performs a secondary state change, report it as automation/external effect, not as an explicit Terra mutation.

---

## 13. Convergence behavior

Evaluate all local/pre-publication gates exhaustively.

No byte-changing correction is authorized in this authority.

Therefore:
- failures caused by environment/disposable validation harness state may be corrected only if they do not alter tracked source or governance state;
- after any disposable correction, rerun the complete affected validation set;
- if a source-byte correction is needed, STOP with the full defect matrix.

Do not consume another authority exchange for each local non-source defect when safe convergence is possible.

---

## 14. Success state

Only if publication and post-merge verification completely PASS:

```text
RELEASE 1.12 WP04 — INTERMEDIATE IMPLEMENTATION/PROVENANCE PUBLICATION: PASS
RELEASE 1.12 WP04 — ACCEPTED WRAPPER BYTES: PUBLISHED_UNCHANGED
RELEASE 1.12 WP04 — ACCEPTED HELPER BYTES: PUBLISHED_UNCHANGED
RELEASE 1.12 WP04 — WRAPPER CLEAN-HEAD PROVENANCE: PASS
RELEASE 1.12 WP04 — HELPER CLEAN-HEAD PROVENANCE: PASS
RELEASE 1.12 WP04 — IMAGE REBUILD: NOT_REQUIRED
RELEASE 1.12 WP04 — AZURE MUTATIONS: 0
RELEASE 1.12 WP04 — RUNIDS ALLOCATED: 0
RELEASE 1.12 WP04 — README MUTATIONS: 0
RELEASE 1.12 WP04 — WP04 FINAL ACCEPTANCE: NOT_YET
RELEASE 1.12 WP04 — WP04 #263: OPEN
RELEASE 1.12 WP04 — MILESTONE #63: OPEN
RELEASE 1.12 WP04 — WP05: NOT_STARTED
RELEASE 1.12 WP04 — WP07: NOT_STARTED
RELEASE 1.12 WP04 — TARGET VALIDATION: READY
```

---

## 15. Required handoff

Return:

```text
EntryHead
EntryBranch
EntryWrapperSHA256
EntryHelperSHA256
AcceptedHelperSHA256
EntryTrackedModifiedPaths
EntryStagedPathCount

WindowsPowerShellVersion
WrapperParserErrorCount
HelperParserErrorCount
ValidationMatrix
ReleaseBuildResult
ReleaseBuildWarnings
ReleaseBuildErrors
SigningContractResult
GitDiffCheckResult

StagedPaths
CommitSHA
PushResult
PRNumber
PRHeadSHA
PRBase
PRChecksResult
MergeResult
MergeCommitSHA

PostMergeHead
PostMergeWrapperSHA256
PostMergeHelperSHA256
WrapperCleanHeadResult
HelperCleanHeadResult
PostMergeStagedPathCount
PostMergeGitDiffCheckResult

ExplicitGitMutationCount
ExplicitGitHubMutationCount
MutationLedger

ReadmeMutationCount
AzureMutationCount
RunIdsAllocated
DockerBuildCount
GhcrPublicationCount
TwelveDataSecretConfigured

WP04IssueState
Project2WP04Status
Milestone63State
WP05State
WP07State

BoundaryBlocker
FinalAcceptance
NextAuthorizedAction
```

On success:

```text
FinalAcceptance=READY_FOR_EXISTING_CONTRACT_TARGET_VALIDATION
NextAuthorizedAction=GPT-5.6 Terra WP04 existing-contract target validation from the new clean committed anchor
```

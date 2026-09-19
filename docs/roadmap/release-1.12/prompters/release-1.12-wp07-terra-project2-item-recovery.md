# Release 1.12 WP07 --- Terra Project #2 Item Recovery / Identification

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract/governance/final substantive acceptance
GPT-5.6 Terra = selected GitHub Project evidence recovery/repair authority
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Context

Fresh Luna reconciliation found:

``` text
AcceptanceFailCount=0
AcceptanceNotProvenCount=1
DefectCount=1
WP07SubstantiveAcceptanceResult=NOT_READY
TerraLifecycleAuthorized=false
```

The sole gap is:

``` text
Issue266PreAcceptanceState=OPEN
ProjectWP07PreAcceptanceState=NOT_PROVEN
Milestone63State=OPEN
Defect=Project #2 has no readable WP07/#266 item in the current 210-item export
```

All technical, Azure, persistence, schema-v4, recovery, cost, no-bypass,
image, System Health, secret-hygiene, and predecessor-preservation gates
PASS.

Canonical current `origin/main` reported by Luna:

``` text
48ade16c26799553986b892facd7d24dfa9d6a73
```

Verifier correction:

``` text
PR=284
MergeSHA=48ade16c26799553986b892facd7d24dfa9d6a73
```

## Mission

Resolve only the Project #2 WP07/#266 evidence gap.

First exhaustively identify whether issue #266 already has a Project #2
item that was omitted/missed by the prior export/query.

If an existing item is found, make **zero Project mutations** and report
its item ID/status.

If and only if exhaustive evidence proves issue #266 is genuinely absent
from Project #2, restore the missing Project item using the repository's
established Release 1.12 Project conventions and set its pre-acceptance
status to the canonical non-Done state used by sibling WPs (normally
`Todo`).

Do not close #266.

Do not set WP07 to Done.

Do not close milestone #63.

Do not begin WP08.

## Identification before mutation --- mandatory

Use authoritative GitHub Project #2 queries, not a single
truncated/default export.

Inspect: - issue #266 identity/URL/node ID; - all Project #2 items with
pagination sufficient to exhaust the project; - item content issue
number/URL/node ID; - title only as a secondary check; - sibling Release
1.12 WP items #260--#267 and their Project field conventions.

Record:

``` text
Project2TotalItemsObserved
Project2PaginationExhausted
Issue266NodeId
ExistingWP07ProjectItemFound
ExistingWP07ProjectItemId
ExistingWP07ProjectStatus
```

Do not infer absence merely because one 210-item export lacks the item.

## Existing-item branch

If an authoritative Project #2 item for issue #266 exists:

``` text
ProjectRepairRequired=false
ProjectMutationCount=0
```

Require its Status to be non-Done for pre-acceptance.

If it is `Todo` or another tracked pre-acceptance non-Done state,
preserve it unchanged.

If it is unexpectedly `Done`, do not silently change it. Report the
inconsistency as a governance boundary for Luna reconciliation.

## Missing-item repair branch

Only if exhaustive pagination/query proves #266 genuinely absent:

1.  Verify #266 is the canonical WP07 issue under milestone #63.
2.  Verify sibling WP07 planning metadata and Project taxonomy.
3.  Add existing issue #266 to Project #2 exactly once.
4.  Set only the fields necessary to restore canonical Release 1.12 WP07
    Project representation.
5.  Set Status to the canonical pre-acceptance non-Done state (`Todo`)
    if the add operation does not already produce it.
6.  Re-read Project #2 and prove exactly one item maps to #266.

This is governance-state repair only. It does not authorize substantive
acceptance or lifecycle completion.

## Mutation minimization

If adding #266 automatically supplies the correct Status, do not issue a
redundant Status mutation.

Count separately:

``` text
ProjectItemAddMutationCount
ProjectStatusExplicitMutationCount
ProjectAutomationStatusObserved
ProjectMutationCount
```

## Prohibited mutations

``` text
RepositoryContentMutationCount=0
GitCommitCount=0
GitPushCount=0
PRCreateCount=0
PRMergeCount=0
AzureMutationCount=0
DockerBuildCount=0
GhcrPublicationCount=0
ImageDeploymentMutationCount=0
AzureAppSettingMutationCount=0
ExplicitRestartCount=0
SecretDisclosureCount=0
IssueCloseMutationCount=0
MilestoneMutationCount=0
WP08MutationCount=0
```

Do not alter technical WP07 evidence.

## Required post-state

Require:

``` text
Issue266After=OPEN
ProjectWP07ItemPresent=true
ProjectWP07Status != Done
Milestone63After=OPEN
DuplicateWP07ProjectItemCount=0
```

Prefer the canonical state:

``` text
ProjectWP07Status=Todo
```

## Required output

``` text
SelectedModel
EntryOriginMain
ActualOriginMain
MainAdvanceClassification

Issue266NodeId
Issue266Before
Milestone63Before

Project2TotalItemsObserved
Project2PaginationExhausted
ExistingWP07ProjectItemFound
ExistingWP07ProjectItemId
ExistingWP07ProjectStatus

ProjectRepairRequired
ProjectItemAddMutationCount
ProjectStatusExplicitMutationCount
ProjectAutomationStatusObserved
ProjectMutationCount

ProjectWP07ItemIdAfter
ProjectWP07ItemPresentAfter
ProjectWP07StatusAfter
DuplicateWP07ProjectItemCount
Issue266After
Milestone63After

RepositoryContentMutationCount
GitCommitCount
GitPushCount
PRCreateCount
PRMergeCount
AzureMutationCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
AzureAppSettingMutationCount
ExplicitRestartCount
SecretDisclosureCount
IssueCloseMutationCount
MilestoneMutationCount
WP08MutationCount

WP07ProjectEvidenceResult
WP07TechnicalEvidenceInvalidated
FreshLunaReconciliationRequired
NextAuthorizedAction
```

## PASS gate

``` text
RELEASE 1.12 WP07 — PROJECT #2 ITEM EVIDENCE: PASS
RELEASE 1.12 WP07 — PROJECT #2 PRE-ACCEPTANCE STATUS: NON_DONE
RELEASE 1.12 WP07 — ISSUE #266: OPEN
RELEASE 1.12 WP07 — MILESTONE #63: OPEN
RELEASE 1.12 WP07 — TECHNICAL EVIDENCE PRESERVATION: PASS

WP07ProjectEvidenceResult=PASS
WP07TechnicalEvidenceInvalidated=false
FreshLunaReconciliationRequired=true
NextAuthorizedAction=GPT-5.6 Luna performs a fresh complete read-only WP07 substantive acceptance reconciliation using the proven Project #2 WP07 item/status
```

Do not perform WP07 lifecycle completion in this authority even if the
Project evidence gap is repaired.

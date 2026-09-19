# Release 1.12 --- Luna Publication Definition & Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority roles

``` text
GPT-5.6 Luna = selected read-only publication contract, reconciliation, and governance authority
GPT-5.6 Terra = release publication/lifecycle mutation authority only after Luna PASS
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Mode

STRICTLY READ-ONLY.

No repository edits, commits, pushes, PRs, issue/Project/milestone
mutations, version changes, tags, GitHub Release creation, Azure
changes, Docker/GHCR actions, runtime execution, RunIds, or secret
access.

## Entry state

Binding predecessor evidence:

``` text
Release=1.12
WP01ThroughWP08IssuesClosed=true
WP01ThroughWP08ProjectStatusesDone=true
Release112WorkPackageLifecycleComplete=true

WP08DocumentationCommit=d7453349b8b570ce2d2f38a5df563c814621573e
WP08MergeCommit=4e9a5dfa285b4d0d28a292efc7e669af6e69bb86
origin/main=4e9a5dfa285b4d0d28a292efc7e669af6e69bb86

Milestone63=OPEN
VersionMutationCount=0
TagMutationCount=0
GitHubReleaseMutationCount=0
MilestoneCloseMutationCount=0
```

Accepted deployment state remains:

``` text
Region=West US 2
Plan=Linux F1/Free
ImageDigest=sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
SQLiteSchemaVersion=4
SQLiteJournalMode=delete
DatabasePath=/home/data/aiquant.db
RecurringInfrastructureCostTarget=$0.00
```

## Mission

Define and reconcile the exact Release 1.12 publication contract before
Terra performs any publication mutation.

Independently verify: 1. all Release 1.12 WPs are Closed/Done; 2.
milestone #63 is still Open; 3. merged `origin/main` contains the
accepted WP08 documentation; 4. repository version/release metadata
expected by project convention; 5. existing tags and GitHub Releases; 6.
Release 1.12 planning/acceptance documents; 7. historical publication
conventions from completed Release 1.10 where relevant; 8. exact release
candidate commit that should be tagged; 9. exact version/tag naming
required for Release 1.12; 10. whether any tracked repository version
mutation is required before tagging; 11. whether a publication PR is
required; 12. exact GitHub Release title/body/source requirements; 13.
exact milestone-close ordering; 14. post-publication verification
requirements.

Evaluate the complete publication gate set, not first-failure only.

## Historical boundary

Preserve:

``` text
Initiative-1.11 != Product Release 1.11
```

Do not create a `1.11.0` product release or `v1.11.0` tag.

Preserve release sequence:

``` text
1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3
```

## Candidate identity

Start from:

``` text
CandidateOriginMain=4e9a5dfa285b4d0d28a292efc7e669af6e69bb86
```

Fetch and verify current `origin/main`.

If main advanced: - inspect every intervening commit/path; - classify
whether it is release-compatible; - determine the correct immutable
publication candidate; - do not silently tag a different commit.

Report:

``` text
PublicationCandidateSHA
CandidateIdentityResult
MainAdvanceClassification
```

## Version and tag reconciliation

Determine from tracked project conventions whether Release 1.12
requires:

``` text
Version=1.12.0
Tag=v1.12.0
```

Inspect all authoritative version-bearing files and existing tags.

Report:

``` text
ExpectedVersion
ExpectedTag
ExistingVersionState
ExistingTagState
VersionMutationRequired
VersionMutationPaths
```

Do not mutate them.

If a version bump is required, define the minimum Terra mutation and
whether it requires a focused PR before tagging.

If no tracked version mutation is required, say so explicitly.

## GitHub Release reconciliation

Inspect prior release convention and determine:

``` text
GitHubReleaseRequired
ExpectedReleaseTitle
ReleaseTargetCommitish
ReleaseNotesSource
Prerelease
Draft
```

Release notes must remain truthful and grounded in accepted Release 1.12
documentation/evidence.

Do not claim: - production SLA/HA; - paid fallback; - universal billing
guarantee; - Product Release 1.11; - unsupported architecture; - Release
2.0 work.

## Milestone lifecycle

Milestone #63 must remain Open until publication requirements are
satisfied.

Define exact ordering between:

``` text
version mutation/PR if required
tag creation
tag push
GitHub Release publication
post-publication verification
milestone #63 closure
```

Prefer fail-closed ordering that does not close the milestone before
release publication is proven.

Determine whether Project #2 requires any release-level mutation beyond
already-Done WP items.

## Publication verification

Define exact Terra verification gates, including: - local/main
synchronization; - candidate SHA identity; - version value; - tag points
to exact intended commit; - remote tag exists exactly once; - GitHub
Release exists and is non-draft/non-prerelease unless contract says
otherwise; - Release URL/title/tag/target consistent; - milestone #63
Closed only after successful publication; - WP01--WP08 remain
Closed/Done; - no unauthorized repository/Azure/image/runtime/secret
mutations; - README/acceptance documentation remains truthful after
publication.

## Mutation budget

Define the smallest authorized Terra mutation set.

Possible categories:

``` text
RepositoryContentMutationCount
VersionMutationCount
CommitCount
PushCount
PRCreateCount
PRMergeCount
TagCreateCount
TagPushCount
GitHubReleaseCreateCount
MilestoneCloseMutationCount
ProjectMutationCount
IssueMutationCount
AzureMutationCount
DockerBuildCount
GhcrPublicationCount
RuntimeMutationCount
SecretAccessCount
Release2WorkMutationCount
```

All non-required categories must be zero.

## Governance boundary

Ordinary publication mechanics should be fully specified in this one
Luna authority.

Return `NOT_READY` only for a genuine unresolved
contract/policy/identity boundary that Terra cannot safely resolve from
tracked conventions.

Do not create unnecessary Luna↔Terra micro-handoffs.

## Required output

``` text
SelectedModel
EntryHEAD
EntryOriginMain
CurrentOriginMain
MainAdvanceClassification

WP01ThroughWP08IssuesClosed
WP01ThroughWP08ProjectStatusesDone
Release112WorkPackageLifecycleComplete
Milestone63PrePublicationState

PublicationPlanningContractResult
AcceptanceDocumentResult
HistoricalReleaseConventionResult
Initiative111IdentityPreservationResult
ReleaseSequencePreservationResult

PublicationCandidateSHA
CandidateIdentityResult

ExpectedVersion
ExpectedTag
ExistingVersionState
ExistingTagState
VersionMutationRequired
VersionMutationPaths
PublicationPRRequired

GitHubReleaseRequired
ExpectedReleaseTitle
ReleaseTargetCommitish
ReleaseNotesSource
Prerelease
Draft

PublicationOrdering
ProjectReleaseLevelMutationRequired

TerraAuthorizedMutations
TerraProhibitedMutations
TerraRequiredVerification

AcceptanceGateCount
AcceptancePassCount
AcceptanceFailCount
AcceptanceNotProvenCount
DefectCount
Defects

Release112PublicationDefinitionResult
TerraPublicationAuthorized
NextAuthorizedAction
```

## PASS semantics

If publication mechanics are fully determined and no unresolved
governance boundary exists:

``` text
AcceptanceFailCount=0
AcceptanceNotProvenCount=0
DefectCount=0
Release112PublicationDefinitionResult=PASS
TerraPublicationAuthorized=true
NextAuthorizedAction=GPT-5.6 Terra performs the bounded Release 1.12 publication sequence exactly as defined: any required version-only mutation/PR, immutable tag creation/push, GitHub Release publication, post-publication verification, and milestone #63 closure; no Release 2.0 work
```

## Exact Luna markers

``` text
RELEASE 1.12 — WORK-PACKAGE LIFECYCLE RECONCILIATION: PASS
RELEASE 1.12 — PUBLICATION CANDIDATE IDENTITY: PASS
RELEASE 1.12 — VERSION/TAG CONTRACT: PASS
RELEASE 1.12 — GITHUB RELEASE CONTRACT: PASS
RELEASE 1.12 — MILESTONE PUBLICATION ORDERING: PASS
RELEASE 1.12 — HISTORICAL IDENTITY PRESERVATION: PASS
RELEASE 1.12 — PUBLICATION DEFINITION: PASS
```

Do not perform any publication mutation in this Luna authority.

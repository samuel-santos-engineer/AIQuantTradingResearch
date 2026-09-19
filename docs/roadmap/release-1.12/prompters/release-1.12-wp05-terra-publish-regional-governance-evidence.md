# Release 1.12 WP05 --- Terra Regional Governance Artifact Publication

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract/policy/architecture/governance/final acceptance
GPT-5.6 Terra = selected Git/GitHub publication authority
GPT-5.6 Sol = supporting analysis only; non-authoritative
```

## Purpose

Close exactly one Luna-identified governance evidence gap:

``` text
WP05RegionalGovernanceResult=NOT_PROVEN
WP05SubstantiveAcceptanceResult=NOT_READY
TerraLifecycleAuthorized=false
```

Luna reported that this required artifact exists locally but is
untracked and absent from `origin/main`:

``` text
docs/roadmap/release-1.12/prompters/release-1.12-wp05-luna-regional-f1-recovery-reconciliation.md
```

No implementation/runtime defect exists in the accepted evidence. Do not
modify source, tests, image, Azure, settings, or runtime merely to
publish this document.

## Entry anchor

Expected entry:

``` text
EntryHEAD=a49f0c5fe0a9dd9969b42a2f034f44829a010cf7
EntryOriginMain=a49f0c5fe0a9dd9969b42a2f034f44829a010cf7
SourcePR=282
SourcePRMergeSHA=a49f0c5fe0a9dd9969b42a2f034f44829a010cf7
```

Fetch first and reconcile actual `origin/main`.

If main advanced, inspect the delta. Continue only if the publication
remains non-conflicting and WP05 governance semantics are unchanged.

## Critical filename reconciliation

The project has previously used the canonical release-specific
regional-governance path:

``` text
docs/roadmap/release-1.12/prompters/release-1.12-wp05-regional-f1-quota-recovery-governance.md
```

Luna now identified the required local artifact as:

``` text
docs/roadmap/release-1.12/prompters/release-1.12-wp05-luna-regional-f1-recovery-reconciliation.md
```

Before mutation, inspect both paths and all Release 1.12 planning
references.

Do NOT silently rename, substitute, delete, or overwrite either
artifact.

Determine:

``` text
CanonicalGovernanceArtifactPath
ReconciliationEvidenceArtifactPath
AreArtifactsSemanticallyDistinct
PlanningReferencesToEachArtifact
PublicationPathRequiredByLunaGap
```

If the Luna-named local file is a distinct read-only
reconciliation/evidence artifact whose publication is consistent with
the tracked canonical governance contract, publish it exactly at the
Luna-named path.

If publishing it would contradict, duplicate incompatibly, or replace
the canonical governance contract, stop with:

``` text
PublicationResult=NOT_READY
BoundaryBlocker=REGIONAL_GOVERNANCE_ARTIFACT_IDENTITY_CONFLICT
NextAuthorizedAction=GPT-5.6 Luna reconciles canonical governance artifact identity before publication
```

Do not invent content to resolve an identity conflict.

## Content verification

Before staging, inspect the complete untracked file.

Require: - no secret material; - no API key value; - no machine-specific
sensitive material; - no stale statement that contradicts the accepted
West US 2 runtime target; - historical primary remains West Central
US; - accepted recovery/runtime-validation target remains West US 2; -
deterministic topology remains:
`West Central US → West US 2 → Central US → South Central India`; -
quota attribution remains required for target transition; - historical
evidence preservation remains required; - no automatic paid upgrade; -
no unlisted region; - no unbounded resource creation; - no secret
copying between targets; - WP07 acceptance is not implied; -
recurring-cost target remains `$0.00`; - no production HA/SLA claim.

The document must remain evidence/governance documentation only. Do not
turn it into new architecture.

## Mutation scope

Authorized tracked mutation count:

``` text
ExpectedChangedPathCount=1
```

Authorized path only:

``` text
docs/roadmap/release-1.12/prompters/release-1.12-wp05-luna-regional-f1-recovery-reconciliation.md
```

No README mutation.

No source/test/configuration mutation.

No edits to the already tracked canonical regional governance artifact
unless a new Luna authority explicitly requires them.

## Publication sequence

If all verification gates pass:

``` text
fetch/reconcile main
→ create dedicated docs branch
→ git add ONLY the authorized artifact
→ verify staged path count = 1
→ secret scan
→ git diff --cached --check
→ commit
→ push
→ create PR
→ verify PR changed path count = 1
→ verify exact path
→ merge through normal governance
→ fetch origin/main
→ verify merge commit contains exact artifact
→ verify repository state
```

Do not close #264 and do not set Project #2 WP05 Done.

Do not close milestone #63.

## No unrelated mutation

Explicitly prohibited:

``` text
AzureMutationCount > 0
DockerBuildCount > 0
GhcrPublicationCount > 0
ImageDeploymentMutationCount > 0
SourceMutationCount > 0
TestMutationCount > 0
READMETrackedMutationCount > 0
IssueMutationCount > 0
ProjectMutationCount > 0
MilestoneMutationCount > 0
```

## Required output

``` text
SelectedModel
EntryHEAD
EntryOriginMain
ActualStartHEAD
ActualStartOriginMain
MainAdvanceClassification

CanonicalGovernanceArtifactPath
CanonicalGovernanceArtifactTracked
ReconciliationEvidenceArtifactPath
ReconciliationEvidenceArtifactTrackedBefore
AreArtifactsSemanticallyDistinct
PlanningReferencesToEachArtifact
PublicationPathRequiredByLunaGap
ArtifactIdentityResult

ArtifactContentResult
SecretScanResult
HistoricalPrimaryResult
WestUS2TargetResult
DeterministicTopologyResult
QuotaAttributionResult
HistoricalEvidencePreservationResult
CostBoundaryResult
SecretBoundaryResult
WP07OwnershipResult
ProductionHAClaimResult

ChangedPathCount
ChangedPaths
StagedPathCount
StagedPaths
GitDiffCheckResult

PublicationBranch
PublicationCommit
PublicationPR
PublicationPRChangedPathCount
PublicationPRChangedPaths
PublicationPRMerged
PublicationMergeSHA
PostMergeOriginMain
ArtifactTrackedOnOriginMain

GitCommitCount
GitPushCount
PRCreateCount
PRMergeCount
AzureMutationCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
SourceMutationCount
TestMutationCount
READMETrackedMutationCount
IssueMutationCount
ProjectMutationCount
MilestoneMutationCount
SecretDisclosureCount

PublicationResult
BoundaryBlocker
NextAuthorizedAction
```

## PASS gate

Only if the Luna-required artifact is safely published and verifiably
tracked on `origin/main`:

``` text
RELEASE 1.12 WP05 — REGIONAL GOVERNANCE EVIDENCE PUBLICATION: PASS
RELEASE 1.12 WP05 — PUBLICATION PATH SCOPE: PASS
RELEASE 1.12 WP05 — REGIONAL GOVERNANCE CONSISTENCY: PASS
RELEASE 1.12 WP05 — SECRET HYGIENE: PASS
RELEASE 1.12 WP05 — $0 ARCHITECTURE BOUNDARY: PASS

ArtifactTrackedOnOriginMain=true
PublicationResult=PASS
BoundaryBlocker=NONE
NextAuthorizedAction=GPT-5.6 Luna performs fresh read-only WP05 final substantive acceptance reconciliation from the new origin/main anchor
```

Do not perform WP05 lifecycle completion in this authority.

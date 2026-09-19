# Release 1.12 WP05 --- Terra Publish Planning and Provision West US 2 Recovery Target

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract, policy, architecture, governance, acceptance
GPT-5.6 Terra = selected implementation, validation, Git/GitHub/Azure mutation authority
GPT-5.6 Sol = supporting analysis only; non-authoritative
```

## Luna acceptance --- binding entry gate

Luna accepted candidate:

``` text
CandidateBranch=origin/docs/wp05-regional-recovery-reconciliation
CandidateTip=84af2f411cd1946b41f963f489d1261ec72b57ac
CandidateBase=cfd8000e099ed82c2ea8a2edef58ff6bd4aff661
CandidateChangedPathCount=3
CandidatePlanningDecision=ACCEPT
DefectCount=0
ApplicationDefectAttribution=false
LunaGovernanceResult=PASS
TerraRegionalRecoveryAuthorized=true
AuthorizedNextRegion=West US 2
```

Required accepted markers:

``` text
RELEASE 1.12 WP05 — REGIONAL RECOVERY PLANNING RECONCILIATION: PASS
RELEASE 1.12 WP05 — 503 ATTRIBUTION: PASS
RELEASE 1.12 WP05 — HISTORICAL EVIDENCE PRESERVATION: PASS
RELEASE 1.12 WP05 — $0 ARCHITECTURE BOUNDARY: PASS
RELEASE 1.12 WP05 — SECRET BOUNDARY: PASS
RELEASE 1.12 WP05 — WEST US 2 RECOVERY TARGET: AUTHORIZED
```

## Mission

Execute the accepted next action in one bounded Terra authority:

1.  publish the accepted three-document planning candidate through
    normal PR governance;
2.  after successful merge and fresh-main reconciliation, provision the
    minimum Release 1.12 Azure App Service Linux F1 recovery target in
    **West US 2**;
3.  reuse the accepted image --- no rebuild solely for regional
    recovery;
4.  reproduce the canonical non-secret deployment/runtime configuration;
5.  stop at the user-controlled `TwelveData__ApiKey` boundary if the new
    target lacks it;
6.  otherwise perform the authorized WP05 target-dependent validation
    and converge until PASS or a genuine governance boundary.

## Canonical documents

Merged project policy:

``` text
docs/architecture/resilience/CONSTRAINED_INFRASTRUCTURE_QUOTA_RECOVERY_GOVERNANCE.md
```

Accepted candidate paths:

``` text
docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md
docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md
docs/roadmap/release-1.12/prompters/release-1.12-wp05-regional-f1-quota-recovery-governance.md
```

No other path is authorized in the planning PR.

## Phase A --- candidate publication

Fetch and verify: - `origin/main`; - candidate branch; - candidate tip
exactly `84af2f411cd1946b41f963f489d1261ec72b57ac`; - legitimate base; -
exactly three changed paths; - no secret material; - no README/source
contamination; - `git diff --check` PASS.

If main advanced after Luna acceptance, do not silently merge stale
semantics. Reconcile whether the three-path candidate remains
conflict-free and semantically unchanged. Stop for Luna only if the
advancement creates a substantive governance contradiction.

Create a dedicated PR for the accepted candidate.

Suggested title:

``` text
docs: reconcile WP05 regional F1 quota recovery
```

PR body must state: - project-wide policy is already merged; - this PR
contains exactly the three Luna-accepted Release 1.12/WP05 planning
paths; - West Central US remains historical primary; - West US 2 is the
authorized next recovery target; - no production source, secret, Azure,
Docker/GHCR, README, WP07 acceptance, or paid-resource change is
contained in the PR.

Merge only after exact candidate/path verification.

After merge: - capture PR number; - merge SHA; - fetch `origin/main`; -
require local/remote main reconciliation; - verify the three accepted
documents are present; - do not close #264 or set WP05 Done.

## Phase B --- bounded West US 2 live-capacity attempt

Only after Phase A publication succeeds.

Target:

``` text
Region=West US 2
Tier=F1
OS=Linux
Architecture=custom Docker App Service
CostBoundary=$0.00
```

West Central US remains preserved. Do not delete or repurpose it.

Before creation: - verify current Azure subscription/account context; -
verify West US 2 F1/Linux eligibility; - record eligibility proof; -
determine whether an existing Release 1.12 recovery target in West US 2
can safely be reused.

If no suitable target exists, perform **one bounded F1 provisioning
attempt** as the accepted live-capacity proof.

No paid fallback.

If provisioning fails due to capacity/quota: - capture sanitized Azure
failure evidence; - clean any partial resources if safe and created
solely by this attempt; - do not loop repeatedly; - do not jump to
Central US in this authority; - return the governed blocker for
next-target reconciliation/execution.

If provisioning succeeds, record: - resource group; - App Service
plan; - Web App; - region; - F1 SKU; - Linux state; - public
network/HTTPS state; - actual resource counts; - cost-boundary evidence.

Use deterministic project naming consistent with existing Release 1.12
conventions. Do not overwrite historical WCUS resources.

## Phase C --- deploy accepted image

Regional recovery does not authorize rebuild.

Require:

``` text
TargetRecoveryRequiresSourceChange=false
TargetRecoveryRequiresArtifactRebuild=false
AcceptedArtifactIdentityPreserved=true
DockerBuildCount=0
```

Reuse the canonical accepted image identity established by the current
Release 1.12 deployment evidence.

Verify the image identity before deployment.

Configure the new App Service to use that accepted public/free GHCR
image under the canonical Linux/custom-container architecture.

Do not publish a new image solely for regional recovery.

## Phase D --- reproduce authorized configuration

Derive values from canonical tracked configuration/evidence where safe.

Required WP05 setting names accepted by Luna:

``` text
TwelveData__ApiKey
Dataset__Target
Dataset__From
Dataset__To
Worker__Replay__ReplayIdentity
Worker__Replay__Target
Worker__Replay__StartingTick
Worker__Replay__RequestedObservationCount
```

For non-secret settings: - reproduce only canonical values required by
existing WP05/release contracts; - do not invent values; - record names
and sanitized values only where they are non-secret and evidence-safe.

For:

``` text
TwelveData__ApiKey
```

NEVER retrieve/copy it from the WCUS target, print it, echo it, persist
it, commit it, or expose it in evidence.

The approved credential mechanism on the new target remains:

``` text
Azure App Service application setting
Name=TwelveData__ApiKey
```

If absent after the user-independent configuration steps:

``` text
SettingName=TwelveData__ApiKey
Present=false
NonEmpty=false
SecretValueDisclosed=false

WP05Result=NOT_READY
BoundaryBlocker=USER_CONTROLLED_TWELVE_DATA_CREDENTIAL_REQUIRED
NextAuthorizedAction=User configures a non-empty TwelveData__ApiKey directly on the new West US 2 App Service through the approved secure mechanism, then resumes this same Terra authority
```

This is a successful governed pause, not an implementation failure.

Do not ask the user to paste the value into chat/Codex.

## Phase E --- target-dependent validation

If the credential is already securely present, or after this same
authority is resumed following user configuration, validate only the
gates invalidated by the target transition.

Preserve: - WP03 accepted historical evidence; - WP04 accepted
historical/application-persistence evidence except target-specific
runtime facts that the active WP explicitly requires; - all
target-independent accepted provenance.

Replay as required:

``` text
target identity
West US 2 / F1 runtime
accepted image identity
public HTTPS/network boundary
required non-secret runtime configuration
TwelveData__ApiKey presence/non-empty metadata
bounded Twelve Data runtime behavior
deterministic failure isolation
active WP05 target-dependent persistence/runtime assumptions
```

Do NOT claim: - WP06 acceptance; - WP07 restart/recycle/redeploy
stability acceptance; - production HA/SLA.

## Convergence loop

For all correctable defects inside this authority:

``` text
evaluate ALL gates
→ collect ALL failures
→ fix ALL in-scope defects
→ refresh hashes/evidence
→ rerun the complete affected cycle
→ repeat until PASS
```

Do not return after each local/configuration defect.

A byte-changing source correction is NOT authorized merely to make
regional recovery work. If source/image changes become necessary, stop
at the governance boundary.

Do not repeatedly deploy/restart/probe in a way that unnecessarily
consumes F1 quota.

## PowerShell baseline

Binding:

``` text
Windows PowerShell 5.1.26100.9444
```

Any PowerShell introduced/changed by authorized existing WP05 scope must
parse and run under that baseline. Do not use PS7-only syntax.

## Mutation boundaries

Authorized: - accepted planning PR create/merge; - minimum West US 2 F1
resource creation after PR merge; - accepted-image
deployment/configuration; - canonical non-secret App Settings required
by WP05; - target-dependent WP05 validation.

Not authorized: - README mutation; - WP06 implementation; - WP07
acceptance execution; - WP08 acceptance; - paid resources; - Key Vault
introduction; - Azure SQL/Container Apps/Azure Files/mandatory ACR; -
source/image rebuild solely for regional recovery; - deletion of
historical WCUS resources; - issue #264 closure; - Project #2 WP05
Done; - milestone closure; - secret disclosure/copy.

## Mutation accounting

Return exact counts:

``` text
PRCreateCount
PRMergeCount
GitCommitCount
GitPushCount
IssueMutationCount
ProjectMutationCount
MilestoneMutationCount

AzureResourceGroupCreateCount
AzureAppServicePlanCreateCount
AzureWebAppCreateCount
AzureResourceDeleteCount
AzureAppSettingMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount

DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
PaidResourceMutationCount
READMETrackedMutationCount
ProductionSourceMutationCount
SecretDisclosureCount
```

Count user-controlled secret configuration separately from Terra
mutations if performed manually by the user.

## Required handoff

Return:

``` text
SelectedModel

PlanningCandidateTip
PlanningPR
PlanningPRMerged
PlanningMergeSHA
PostMergeOriginMain
PlanningPublishedPathCount
PlanningPublishedPaths

HistoricalPrimaryRegion
AuthorizedRecoveryRegion
WestUS2EligibilityResult
WestUS2LiveCapacityAttempted
WestUS2LiveCapacityResult

RecoveryResourceGroup
RecoveryAppServicePlan
RecoveryWebApp
RecoveryRegion
RecoverySku
RecoveryLinuxResult
RecoveryPublicNetworkResult
RecoveryHttpsOnlyResult
RecoveryCostBoundaryResult

AcceptedImageIdentity
DeployedImageIdentity
ImageIdentityMatch
DockerBuildPerformed

RequiredSettingNames
NonSecretSettingsConfigured
TwelveDataSettingPresent
TwelveDataSettingNonEmpty
SecretValueDisclosed

TargetIdentityValidationResult
RuntimeValidationPerformed
RuntimeValidationResult
BoundedAutomationValidationResult
FailureIsolationResult
PersistenceBoundaryResult

WP03HistoricalEvidencePreserved
WP04HistoricalEvidencePreserved
WP06AcceptanceClaimed
WP07AcceptanceClaimed
ProductionHAClaimed

PRCreateCount
PRMergeCount
GitCommitCount
GitPushCount
IssueMutationCount
ProjectMutationCount
MilestoneMutationCount
AzureResourceGroupCreateCount
AzureAppServicePlanCreateCount
AzureWebAppCreateCount
AzureResourceDeleteCount
AzureAppSettingMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
PaidResourceMutationCount
READMETrackedMutationCount
ProductionSourceMutationCount
SecretDisclosureCount

FinalHEAD
FinalOriginMain
FinalTrackedClean
FinalStagedPathCount
FinalGitDiffCheck

RegionalRecoveryResult
WP05Result
BoundaryBlocker
NextAuthorizedAction
```

## Success --- planning + recovery target

If West US 2 provisions successfully and all user-independent recovery
gates pass:

``` text
RELEASE 1.12 WP05 — REGIONAL RECOVERY PLANNING PUBLICATION: PASS
RELEASE 1.12 WP05 — WEST US 2 F1 LIVE CAPACITY: PASS
RELEASE 1.12 WP05 — ACCEPTED IMAGE REUSE: PASS
RELEASE 1.12 WP05 — $0 ARCHITECTURE BOUNDARY: PASS
RELEASE 1.12 WP05 — HISTORICAL TARGET PRESERVATION: PASS
RELEASE 1.12 WP05 — SECRET HYGIENE: PASS
```

If the credential is absent, stop with the user-controlled credential
blocker and preserve the successfully created target.

If credential is present and full target-dependent WP05 runtime
validation passes:

``` text
RELEASE 1.12 WP05 — WEST US 2 REGIONAL RECOVERY: PASS
RELEASE 1.12 WP05 — BOUNDED AUTOMATION: PASS
RELEASE 1.12 WP05 — RUNTIME VALIDATION: PASS
RegionalRecoveryResult=PASS
WP05Result=READY_FOR_LUNA_ACCEPTANCE
NextAuthorizedAction=GPT-5.6 Luna final read-only WP05 substantive acceptance reconciliation
```

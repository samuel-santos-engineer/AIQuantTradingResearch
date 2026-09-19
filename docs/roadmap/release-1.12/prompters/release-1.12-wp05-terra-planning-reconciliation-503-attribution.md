# Release 1.12 WP05 --- Terra Planning Reconciliation and 503 Attribution Authority

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract/policy/architecture/governance and acceptance
GPT-5.6 Terra = selected executor for authorized documentation reconciliation and diagnostic validation
GPT-5.6 Sol = supporting analysis only; non-authoritative
```

## Entry decision

Luna returned:

``` text
LunaGovernanceResult=NOT_READY
TerraRegionalRecoveryAuthorized=false
ConstraintAttribution=CONFIRMED
ApplicationDefectAttribution=NOT_PROVEN_FALSE
NextRecoveryTarget=West US 2
```

Therefore **West US 2 provisioning is NOT authorized in this
authority**.

This authority has two bounded objectives:

1.  implement the Luna-required documentation/planning reconciliation;
2.  exhaustively diagnose the existing West Central US HTTP 503
    sufficiently to determine whether an application/container defect
    exists independently of the confirmed F1 quota condition.

Do not provision an alternate region.

## Canonical entry

Canonical merged main entering reconciliation:

``` text
cfd8000e099ed82c2ea8a2edef58ff6bd4aff661
```

Fetch/reconcile `origin/main`. If it legitimately advanced, record the
actual SHA and preserve the merged project-wide policy.

Merged project policy:

``` text
docs/architecture/resilience/CONSTRAINED_INFRASTRUCTURE_QUOTA_RECOVERY_GOVERNANCE.md
```

Local proposed specialization:

``` text
docs/roadmap/release-1.12/prompters/release-1.12-wp05-regional-f1-quota-recovery-governance.md
```

## Luna-required semantic amendments

Implement the following without expanding scope:

``` text
- Preserve West Central US as historical primary target.
- Add bounded recovery order:
  West Central US → West US 2 → Central US → South Central India.
- Regional recovery does not invalidate accepted WP03/WP04 historical evidence.
- Reuse the same accepted image; no regional-recovery source change/rebuild.
- Require fresh F1 capacity proof before provisioning each alternate.
- Require user-controlled TwelveData__ApiKey configuration independently per target.
- Keep recovery distinct from WP07 acceptance and production HA/SLA.
```

Required classifications:

``` text
RELEASE_1.12_DEFINITION.md      = SEMANTIC_AMENDMENT_REQUIRED
RELEASE_1.12_EXECUTION_PLAN.md  = SEMANTIC_AMENDMENT_REQUIRED
RELEASE_1.12_FILE_MANIFEST.md   = NO_CHANGE
WP05 / #264 contract            = SEMANTIC_AMENDMENT_REQUIRED
WP06 / #265                     = REFERENCE_ONLY
WP07 / #266                     = REFERENCE_ONLY
WP08 / #267                     = REFERENCE_ONLY
```

## Documentation mutation scope

Authorized tracked documentation scope is limited to: - the local WP05
regional specialization document; - Release 1.12 Definition; - Release
1.12 Execution Plan; - only the minimum project-local WP05 contract
artifact if such a tracked contract exists and Luna's semantic amendment
requires it.

Do not modify the file manifest unless contradiction proves Luna's
`NO_CHANGE` classification impossible.

For GitHub issues #264--#267, do not mutate them in this authority
unless a separately existing governance rule explicitly requires issue
text synchronization. Instead return exact proposed issue reconciliation
text for later publication.

README preservation policy remains binding. README mutation count must
be zero.

## 503 diagnostic objective

Current West Central US target has: - confirmed historical/current
`QuotaExceeded` evidence; - later HTTP 503 behavior; - no demonstrated
access restriction cause; - public networking enabled; - accepted image
intended to remain unchanged.

Luna requires determination of:

``` text
ApplicationDefectAttribution=false
```

before alternate-target provisioning.

Diagnose exhaustively but non-destructively first.

Inspect safe Azure/control-plane/runtime evidence including, where
available: - App Service state and quota status; - App Service Plan
quota counters/reset information; - Activity Log; - Site/availability
diagnostics; - container startup/platform logs; - container exit/restart
evidence; - startup/port binding evidence; - sanitized application
logs; - accepted deployed image identity; - required non-secret runtime
setting names/presence metadata; - filesystem/startup configuration
where observable without bypass.

Do NOT disclose secret values.

## Diagnostic classification

Evaluate all plausible classes:

``` text
F1_QUOTA_OR_PLATFORM_CONSTRAINT
CONTAINER_STARTUP_DEFECT
PORT_BINDING_DEFECT
APPLICATION_EXCEPTION
MISSING_REQUIRED_SETTING
PERSISTENCE_OR_FILESYSTEM_DEFECT
ACCESS_OR_NETWORK_DEFECT
IMAGE_IDENTITY_MISMATCH
UNKNOWN
```

Return evidence for every class, not only the first apparent cause.

The goal is to determine whether the continuing 503 demonstrates an
independent application/container defect.

If evidence supports only quota/platform unavailability and no
independent application defect:

``` text
ApplicationDefectAttribution=false
```

If an application/container/configuration defect is proven, fix it ONLY
if the correction is already within canonical WP05 scope and does not
require source/image architecture changes outside existing authority.
Use bounded convergence for such in-scope local corrections.

If a correction requires source/image changes that would violate the
accepted-image/no-rebuild regional recovery invariant, stop and return
the exact governance boundary rather than rebuilding.

## No destructive quota probing

Do not repeatedly restart, redeploy, rebuild, or hammer the endpoint
merely to test F1 recovery.

``` text
RedundantRestartAllowed=false
RedundantRedeployAllowed=false
QuotaConsumingProbeLoopAllowed=false
```

Use bounded read-only diagnostics.

## Fresh alternate capacity proof contract

Do not provision West US 2, but define the exact Terra pre-provisioning
proof that the next authority should use.

At minimum distinguish:

``` text
ServiceEligibilityProof
LiveProvisioningCapacityProof
```

A CLI region/SKU listing is eligibility evidence only.

Define a safe, authoritative Azure method for proving live F1
provisioning capacity immediately before resource creation. If Azure
exposes no non-mutating guarantee of live capacity, state that
explicitly and define a bounded create-attempt semantics where failed
provisioning creates no accepted target and is recorded before moving to
the next authorized region.

Do not perform that create attempt now.

## WP05 alternate-target settings contract

Derive from existing tracked WP05/release contracts the exact **setting
names and safe metadata** required on an alternate target.

Never output secret values.

At minimum reconcile:

``` text
TwelveData__ApiKey
```

and any other required non-secret settings already established by
canonical deployment/runtime composition.

Do not invent new settings.

## Convergence behavior

For documentation defects and local validators within this authority:

``` text
evaluate ALL gates
→ collect ALL failures
→ correct ALL in-scope defects
→ fresh hashes
→ rerun complete structural validation
→ repeat until PASS
```

Do not stop after each correctable local defect.

Stop immediately only for a true governance/security boundary.

## Validation

Require: - Windows PowerShell `5.1.26100.9444` compatibility for any
changed PowerShell (none expected); - `git diff --check` PASS; - no
secret material tracked; - no README mutation; - no production/runtime
source mutation unless explicitly justified as already-authorized WP05
correction; - no Azure resource creation; - no West US 2 provisioning; -
no paid resource; - no Docker build; - no GHCR publication; - no image
deployment mutation; - no WP07 acceptance action.

## Git behavior

After all documentation reconciliation gates pass, Terra MAY create a
dedicated branch and commit/push the accepted documentation candidate if
that is consistent with the project's normal candidate workflow.

Do NOT merge, close #264, or mark WP05 Done.

If the 503 attribution remains unresolved, the documentation candidate
may still be prepared, but regional provisioning remains blocked.

## Required output

Return:

``` text
SelectedModel
EntryMain
EntryOriginMain
ProjectGovernancePath
WP05SpecializationPath

DefinitionReconciled
ExecutionPlanReconciled
FileManifestChanged
WP05SpecializationReconciled
Issue264ProposedReconciliation
Issue265ReferenceText
Issue266ReferenceText
Issue267ReferenceText

ChangedPathCount
ChangedPaths
CandidateCommit
RemoteCandidateTip
GitDiffCheckResult
SecretScanResult
READMETrackedMutationCount

CurrentRegion
CurrentResourceGroup
CurrentWebApp
CurrentAppServiceState
CurrentQuotaState
CurrentHttpResult
CurrentImageIdentity

QuotaEvidenceResult
PlatformEvidenceResult
ContainerStartupEvidenceResult
PortBindingEvidenceResult
ApplicationExceptionEvidenceResult
RequiredSettingEvidenceResult
PersistenceEvidenceResult
AccessNetworkEvidenceResult
ImageIdentityEvidenceResult

F1QuotaOrPlatformConstraintClassification
ContainerStartupDefectClassification
PortBindingDefectClassification
ApplicationExceptionClassification
MissingRequiredSettingClassification
PersistenceFilesystemDefectClassification
AccessNetworkDefectClassification
ImageIdentityMismatchClassification

ApplicationDefectAttribution
ApplicationDefectEvidence
503AttributionResult

ServiceEligibilityProofContract
LiveProvisioningCapacityProofContract
BoundedProvisioningAttemptRequired
AlternateTargetRequiredSettingNames
SecretValueDisclosed

AzureResourceCreateCount
AzureResourceDeleteCount
AzureAppSettingMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
PaidResourceMutationCount
SourceMutationCount
GitCommitCount
GitPushCount
PRCount
MergeCount
IssueMutationCount
ProjectMutationCount
MilestoneMutationCount

PlanningReconciliationResult
RegionalRecoveryGateResult
NextAuthorizedAction
```

## Success paths

If planning is reconciled and diagnostics prove no independent
application defect:

``` text
RELEASE 1.12 WP05 — REGIONAL RECOVERY PLANNING RECONCILIATION: PASS
RELEASE 1.12 WP05 — 503 ATTRIBUTION: PASS
ApplicationDefectAttribution=false
PlanningReconciliationResult=PASS
RegionalRecoveryGateResult=READY_FOR_LUNA_FINAL_RECONCILIATION
NextAuthorizedAction=GPT-5.6 Luna read-only final reconciliation of the corrected WP05 specialization/planning and 503 attribution before West US 2 provisioning
```

If planning passes but attribution remains unresolved:

``` text
PlanningReconciliationResult=PASS
ApplicationDefectAttribution=UNRESOLVED
RegionalRecoveryGateResult=BLOCKED
NextAuthorizedAction=Resolve the reported diagnostic evidence gap without provisioning an alternate target
```

If an independent application defect is proven:

``` text
ApplicationDefectAttribution=true
RegionalRecoveryGateResult=BLOCKED
```

and return the exact defect, ownership, and correction boundary.

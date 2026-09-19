# Release 1.12 WP05 --- Luna Final Regional Recovery Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Roles

``` text
GPT-5.6 Luna = selected read-only contract/policy/architecture/governance acceptance authority
GPT-5.6 Terra = implementation/validation/mutation authority after Luna acceptance
GPT-5.6 Sol = supporting analysis only; non-authoritative
```

## Mode

READ-ONLY. Do not mutate Git, GitHub, Azure, Docker/GHCR, secrets,
issues, Project #2, or milestones.

## Canonical inputs

Merged project governance anchor:

``` text
origin/main historical reconciliation anchor:
cfd8000e099ed82c2ea8a2edef58ff6bd4aff661

docs/architecture/resilience/CONSTRAINED_INFRASTRUCTURE_QUOTA_RECOVERY_GOVERNANCE.md
```

Terra reconciliation candidate:

``` text
Branch: origin/docs/wp05-regional-recovery-reconciliation
Candidate tip: 84af2f411cd1946b41f963f489d1261ec72b57ac
```

The candidate is reported to contain exactly three WP05
regional-recovery planning documents. No PR or merge has occurred.

Verify these facts independently.

## Mission

Perform the final Luna reconciliation required before any West US 2
provisioning.

Compare candidate tip `84af2f411cd1946b41f963f489d1261ec72b57ac` against
its legitimate main base and inspect every changed path.

Require:

``` text
CandidateChangedPathCount=3
UnexpectedChangedPathCount=0
READMEChanged=false
ProductionSourceChanged=false
AzureMutationEvidence=false
SecretMaterialTracked=false
```

Identify the exact three changed paths and classify their semantic
purpose.

## Governance reconciliation

Verify that the candidate correctly implements the prior Luna-required
semantics:

``` text
Primary historical target = West Central US
Next recovery target = West US 2
Emergency reserve #1 = Central US
Emergency reserve #2 = South Central India

Recovery order:
West Central US → West US 2 → Central US → South Central India
```

Require: - WP03 evidence preserved; - WP04 historical evidence
preserved; - regional recovery does not reopen completed WPs; - same
accepted image reused unless independently invalidated; - no
regional-recovery rebuild/source-change requirement; - fresh F1
eligibility/capacity check before each alternate; - user-controlled
`TwelveData__ApiKey` configured independently on each target; - no
secret copying/disclosure; - no paid upgrade; - no production HA/SLA
claim; - WP07 retains restart/recycle/redeploy stability ownership.

## 503 attribution reconciliation

Read the Terra diagnostic evidence produced by the immediately preceding
authority.

Luna must decide whether the evidence now supports:

``` text
ApplicationDefectAttribution=false
```

Evaluate the complete diagnostic matrix, including:

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

Do not infer absence of an application defect merely from
`QuotaExceeded`.

If an independent application/container defect remains plausible because
evidence is incomplete, return NOT_READY and state the exact missing
proof.

## Alternate-target capacity semantics

Reconcile Terra's proposed distinction between:

``` text
ServiceEligibilityProof
LiveProvisioningCapacityProof
```

A region/SKU listing alone is not live capacity proof.

If Azure provides no non-mutating guarantee of F1 live provisioning
capacity, Luna may accept a bounded create-attempt as the live-capacity
test, provided: - it occurs only after this governance gate passes; - it
targets the next canonical region; - failure is recorded; - no paid
fallback occurs; - partial resources are reconciled/cleaned safely; -
the next reserve is used only according to project governance.

## WP05 settings contract

Verify the exact alternate-target setting-name contract derived by
Terra.

Secret values must never appear.

At minimum:

``` text
TwelveData__ApiKey
```

must remain user-controlled and only safe presence/non-empty metadata
may be recorded.

Do not authorize invented settings.

## Candidate disposition

Classify:

``` text
CandidatePlanningDecision=
  ACCEPT
  ACCEPT_WITH_REQUIRED_CORRECTIONS
  REJECT
```

If corrections are required, return the complete defect matrix in one
response.

## Required output

``` text
SelectedModel
EntryOriginMain
CandidateBranch
CandidateTip
CandidateBase
CandidateChangedPathCount
CandidateChangedPaths
UnexpectedChangedPathCount
SecretMaterialTracked
READMEChanged
ProductionSourceChanged

ProjectGovernanceResult
PlanningReconciliationResult
WP05SpecializationResult
HistoricalEvidencePreservationResult
ArchitectureInvariantResult
CostBoundaryResult
SecretBoundaryResult
WP07OwnershipResult

QuotaAttributionResult
ApplicationDefectAttribution
ApplicationDefectEvidence
503AttributionResult

ServiceEligibilityProofResult
LiveProvisioningCapacityProofRule
BoundedCreateAttemptAuthorizedAfterAcceptance

AlternateTargetRequiredSettingNames
SouthCentralIndiaGovernanceStatus

CandidatePlanningDecision
DefectCount
Defects

LunaGovernanceResult
TerraRegionalRecoveryAuthorized
AuthorizedNextRegion
NextAuthorizedAction
```

## PASS gate

Only if both planning reconciliation and 503/application-defect
attribution are resolved may Luna emit:

``` text
RELEASE 1.12 WP05 — REGIONAL RECOVERY PLANNING RECONCILIATION: PASS
RELEASE 1.12 WP05 — 503 ATTRIBUTION: PASS
RELEASE 1.12 WP05 — HISTORICAL EVIDENCE PRESERVATION: PASS
RELEASE 1.12 WP05 — $0 ARCHITECTURE BOUNDARY: PASS
RELEASE 1.12 WP05 — SECRET BOUNDARY: PASS
RELEASE 1.12 WP05 — WEST US 2 RECOVERY TARGET: AUTHORIZED

CandidatePlanningDecision=ACCEPT
ApplicationDefectAttribution=false
LunaGovernanceResult=PASS
TerraRegionalRecoveryAuthorized=true
AuthorizedNextRegion=West US 2
NextAuthorizedAction=GPT-5.6 Terra publishes the accepted three-document planning candidate through normal PR governance, then provisions and validates the West US 2 F1 recovery target under the accepted bounded recovery policy
```

If the 503 attribution is not resolved:

``` text
LunaGovernanceResult=NOT_READY
TerraRegionalRecoveryAuthorized=false
```

Do not authorize West US 2 merely because the planning candidate itself
is correct.

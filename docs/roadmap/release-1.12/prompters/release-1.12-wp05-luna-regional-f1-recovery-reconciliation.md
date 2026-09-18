# Release 1.12 WP05 --- Luna Regional F1 Recovery Governance Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Model authority

``` text
GPT-5.6 Luna = selected authority for contract, policy, architecture, governance,
               planning reconciliation, and acceptance criteria
GPT-5.6 Terra = implementation, validation execution, and approved mutations only
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Mode

**READ-ONLY GOVERNANCE RECONCILIATION.**

Do not create Azure resources.\
Do not mutate Azure.\
Do not mutate Docker/GHCR.\
Do not expose or request secret values.\
Do not commit, push, create/merge PRs, close issues, or mutate Project
#2.\
Do not implement WP05 source/runtime changes.

## Canonical entry anchor

Start from canonical `origin/main`:

``` text
cfd8000e099ed82c2ea8a2edef58ff6bd4aff661
```

This is PR #280's merged commit.

Verify `origin/main` before analysis. If live `origin/main` differs,
record the actual SHA and reconcile whether the governance document from
PR #280 is still present and unchanged before continuing.

## Established PR #280 state

PR #280 merged exactly:

``` text
docs/architecture/resilience/CONSTRAINED_INFRASTRUCTURE_QUOTA_RECOVERY_GOVERNANCE.md
```

No Azure, Docker/GHCR, secret, or WP05 implementation mutation was part
of PR #280.

Four other local/untracked WP05 documents were preserved and were not
part of PR #280.

## Regional eligibility evidence already obtained

Read-only CLI eligibility evidence:

``` text
West US 2          = F1/Linux eligible
Central US         = F1/Linux eligible
West Central US    = F1/Linux eligible
South Central India = not returned by the prior CLI availability query
```

Interpretation is strictly:

``` text
EligibilityEvidence != LiveCapacityEvidence
```

Absence of South Central India from that query MUST NOT be silently
interpreted as either eligible or ineligible.

## Documents to read

Read the merged project-wide authority:

``` text
docs/architecture/resilience/CONSTRAINED_INFRASTRUCTURE_QUOTA_RECOVERY_GOVERNANCE.md
```

Read the local proposed WP05 specialization if present:

``` text
docs/roadmap/release-1.12/prompters/release-1.12-wp05-regional-f1-quota-recovery-governance.md
```

Read canonical Release 1.12 planning:

``` text
docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md
docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md
docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md
```

Read issue #264 / WP05 contract and, where target identity or recovery
ownership is relevant, inspect WP06--WP08 contracts (#265--#267).

## Required reconciliation

Determine whether the proposed WP05 specialization is consistent with
the merged project-wide governance and existing Release 1.12
architecture.

Reconcile these proposed target roles:

``` text
Primary historical target     West Central US
Alternate validation target   West US 2
Emergency reserve #1          Central US
Emergency reserve #2          South Central India
```

Reconcile the deterministic recovery sequence:

``` text
West Central US
→ West US 2
→ Central US
→ South Central India
```

The primary historical target MUST remain historical truth for
already-accepted WP03/WP04 evidence.

Do not rewrite Initiative-1.11 feasibility history or accepted WP03/WP04
evidence merely because WP05 requires an alternate runtime-validation
target.

## Current recovery event

The existing West Central US F1 deployment has demonstrated quota
exhaustion during Release 1.12 work.

Determine whether the available evidence satisfies the project policy's
cause-attribution gate for:

``` text
CurrentTargetState=CONSTRAINED
ConstraintAttribution=CONFIRMED
ApplicationDefectAttribution=false
RecoveryAuthorized=true
```

If the evidence is insufficient, identify the exact missing evidence and
stop recovery authorization.

If sufficient, determine whether the next governed target is:

``` text
West US 2
```

Do not create it. Luna only authorizes/reconciles.

## Cost and architecture invariants

The reconciliation MUST preserve:

``` text
Azure App Service Linux F1
custom Docker container
public/default HTTPS/DNS
persistent /home
SQLite DELETE
public/free GHCR
bounded Twelve Data connectivity
truthful Streamlit diagnostics
actual recurring infrastructure cost target = $0.00
no production SLA claim
```

No automatic paid upgrade.

No Azure SQL, Container Apps, Azure Files, mandatory ACR, paid App
Service tier, production failover service, or new paid dependency.

Regional recovery is validation/recovery topology, NOT production high
availability.

## Artifact identity

Determine whether WP05 regional recovery requires reuse of the currently
accepted image rather than rebuild.

Default project policy is:

``` text
TargetRecoveryRequiresSourceChange=false
TargetRecoveryRequiresArtifactRebuild=false
AcceptedArtifactIdentityPreserved=true
```

If Release 1.12 planning contradicts this, cite the exact contract
language and classify the contradiction.

## Secret boundary

WP05 approved runtime credential mechanism:

``` text
Azure App Service application setting
Name=TwelveData__ApiKey
```

Regional recovery MUST NOT authorize copying or retrieving the secret
from another target.

For a new target, the user remains responsible for secure secret
configuration.

Permitted evidence only:

``` text
SettingName=TwelveData__ApiKey
Present=true|false
NonEmpty=true|false
SecretValueDisclosed=false
```

## Evidence ownership

Classify which existing evidence is:

``` text
PRESERVED
TARGET_DEPENDENT_REPLAY_REQUIRED
SUPERSEDED_BY_NEW_TARGET_EVIDENCE
CONTRADICTED
```

Specifically determine:

-   WP03 evidence preserved vs target-dependent.
-   WP04 application-owned SQLite evidence preserved vs
    target-dependent.
-   WP05 gates requiring replay on West US 2.
-   WP06 implications, if any.
-   WP07 ownership of restart/recycle/redeploy stability and whether
    regional recovery must remain distinct from WP07 acceptance.
-   WP08 documentation/release-acceptance implications.

Do not reopen completed WPs solely because target identity changes.

## Planning reconciliation

Produce an exact amendment matrix for:

``` text
RELEASE_1.12_DEFINITION.md
RELEASE_1.12_EXECUTION_PLAN.md
RELEASE_1.12_FILE_MANIFEST.md
WP05 / #264
WP06 / #265
WP07 / #266
WP08 / #267
```

For each, classify:

``` text
NO_CHANGE
REFERENCE_ONLY
SEMANTIC_AMENDMENT_REQUIRED
```

If an amendment is required, state the exact intended semantic change.
Do not edit files in this Luna authority.

Also determine whether the WP05 specialization document itself is:

``` text
ACCEPT_AS_WRITTEN
ACCEPT_WITH_REQUIRED_CORRECTIONS
REJECT
```

If corrections are required, provide a complete defect matrix in one
response rather than stopping at the first issue.

## South Central India treatment

Because the prior availability query did not return South Central India,
Luna MUST explicitly decide only the governance semantics, not live
eligibility.

Recommended fail-closed interpretation to evaluate:

``` text
SouthCentralIndiaRole=AUTHORIZED_RESERVE_CANDIDATE
ProvisioningRequiresFreshEligibilityAndF1AvailabilityProof=true
AbsenceFromPriorQueryDoesNotAuthorizeProvisioning=true
```

Do not silently remove or promote the region without a governance basis.

## Required output

Return:

``` text
SelectedModel
EntryOriginMain
MergedProjectGovernancePath
MergedProjectGovernanceResult

WP05SpecializationPath
WP05SpecializationPresence
WP05SpecializationDecision
WP05SpecializationDefectCount
WP05SpecializationDefects

QuotaAttributionEvidenceResult
CurrentTargetState
ConstraintAttribution
ApplicationDefectAttribution
RecoveryAuthorized

PrimaryHistoricalTarget
NextRecoveryTarget
EmergencyReserve1
EmergencyReserve2
SouthCentralIndiaGovernanceStatus

ArchitectureInvariantResult
CostBoundaryResult
AcceptedArtifactIdentityRule
SecretBoundaryResult

WP03EvidenceClassification
WP04EvidenceClassification
WP05ReplayRequirements
WP06Implications
WP07OwnershipResult
WP08Implications

DefinitionAmendmentClassification
ExecutionPlanAmendmentClassification
FileManifestAmendmentClassification
WP05ContractAmendmentClassification
WP06ContractAmendmentClassification
WP07ContractAmendmentClassification
WP08ContractAmendmentClassification

RequiredSemanticAmendments
Contradictions
UnresolvedGovernanceQuestions

LunaGovernanceResult
TerraRegionalRecoveryAuthorized
NextAuthorizedAction
```

## Acceptance

Only if the project-wide governance, WP05 specialization, Release 1.12
contracts, historical evidence preservation, \$0 boundary, secret
boundary, and WP ownership reconcile without unresolved contradiction
may Luna emit:

``` text
RELEASE 1.12 WP05 — REGIONAL F1 RECOVERY GOVERNANCE RECONCILIATION: PASS
RELEASE 1.12 WP05 — HISTORICAL TARGET PRESERVATION: PASS
RELEASE 1.12 WP05 — $0 ARCHITECTURE BOUNDARY: PASS
RELEASE 1.12 WP05 — SECRET BOUNDARY: PASS
RELEASE 1.12 WP05 — WEST US 2 RECOVERY TARGET: AUTHORIZED
LunaGovernanceResult=PASS
TerraRegionalRecoveryAuthorized=true
NextAuthorizedAction=GPT-5.6 Terra implements the accepted planning/document reconciliation and provisions/validates the West US 2 F1 recovery target under the bounded recovery policy
```

If any contradiction remains, emit:

``` text
LunaGovernanceResult=NOT_READY
TerraRegionalRecoveryAuthorized=false
```

and provide the complete reconciliation/defect matrix required to reach
PASS.

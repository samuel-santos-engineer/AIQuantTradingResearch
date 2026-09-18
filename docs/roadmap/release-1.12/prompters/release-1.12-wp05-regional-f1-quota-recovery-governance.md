# Release 1.12 --- Regional F1 Quota Recovery and Alternate Target Governance

**Document class:** Release governance / deployment topology contract\
**Applies to:** Phase 4 --- Release 1.12 Public Reference Deployment
Implementation & Stabilization\
**Status:** Binding upon acceptance and merge into the canonical
repository\
**Governance authority:** User-approved policy; GPT-5.6 Luna
reconciliation required before implementation\
**Implementation authority:** GPT-5.6 Terra\
**Supporting analysis:** GPT-5.6 Sol only; non-authoritative

## 1. Purpose

Release 1.12 uses Azure App Service Linux F1 as a strict `$0.00` public
reference deployment target. F1 quota exhaustion is an expected platform
constraint and MUST NOT be treated as an application defect or
authorization to upgrade to a paid tier.

Release 1.12 therefore adopts a governed multi-region F1 topology. After
confirmed quota exhaustion, work MAY recover on the next authorized
regional target without changing application architecture or the `$0.00`
boundary.

## 2. Canonical regional topology

``` text
Primary historical target     West Central US
Alternate validation target   West US 2
Emergency reserve #1          Central US
Emergency reserve #2          South Central India
```

Deterministic recovery order:

``` text
WEST CENTRAL US
→ WEST US 2
→ CENTRAL US
→ SOUTH CENTRAL INDIA
```

A later target MUST NOT be selected merely for convenience while the
current target is healthy.

## 3. Historical-target preservation

West Central US remains the primary historical target. Existing accepted
WP03/WP04 evidence remains valid historical evidence unless
independently contradicted. Recovery MUST NOT rewrite, relabel, destroy,
or invalidate prior accepted evidence solely because a later work
package uses another authorized region.

``` text
HistoricalEvidencePreservation=REQUIRED
HistoricalTarget=West Central US
QuotaRecoveryInvalidatesPriorAcceptedEvidence=false
```

## 4. Quota attribution gate

Regional recovery is authorized only when Azure evidence confirms
quota/capacity exhaustion attributable to the current F1 target.

The following alone do NOT authorize failover: application HTTP failure
without quota attribution, missing `TwelveData__ApiKey`,
application/container/configuration/source/persistence defects, access
restrictions, test failures, or ordinary transient request failures.

Required classification:

``` text
CurrentTargetHealth=QUOTA_BLOCKED
QuotaAttribution=CONFIRMED
ApplicationDefectAttribution=false
RecoveryStrategy=REGIONAL_F1_TARGET_FAILOVER
```

If attribution is uncertain, diagnose first. Do not consume a reserve
region to hide an application defect.

## 5. Automatic recovery rule

After `QuotaAttribution=CONFIRMED`, GPT-5.6 Terra is authorized to use
the next unused/available target in canonical order without requiring a
new architecture decision for each occurrence.

"Automatic" means deterministic recovery under this contract; it does
NOT authorize uncontrolled resource creation.

``` text
1. Confirm current F1 quota exhaustion.
2. Preserve current target and evidence.
3. Select next authorized region.
4. Verify F1 Linux availability/eligibility.
5. Create/reuse only minimum canonical $0 F1 resources.
6. Deploy the same accepted image/artifact required by the active WP.
7. Reapply only authorized non-secret configuration.
8. Require user-controlled secret configuration where needed.
9. Validate target identity, architecture, $0 tier and active-WP gates.
10. Resume from the earliest gate invalidated by target change.
11. Record complete mutation and transition evidence.
```

If the next region cannot provision F1 before deployment, Terra MAY
continue to the next reserve and MUST record the failed availability
check.

## 6. Resource ceiling

``` text
AuthorizedRegionCount=4
DefaultTargetPerRegion=1
PaidTierAllowed=false
AutomaticPaidUpgradeAllowed=false
UnlistedRegionAllowed=false
UnboundedResourceCreationAllowed=false
```

If all four targets are unavailable:

``` text
BoundaryBlocker=ALL_AUTHORIZED_F1_REGIONAL_TARGETS_UNAVAILABLE
NextAuthorizedAction=GPT-5.6 Luna architecture/governance reconciliation
```

## 7. Architecture invariants

Every target preserves:

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

Recovery MUST NOT introduce Azure SQL, Container Apps, Azure Files,
mandatory ACR, Key Vault as a paid Release 1.12 dependency, paid App
Service, production failover services, schema migration without
authority, or architecture bypasses.

This is validation/recovery topology, not production high availability.

## 8. Image/provenance invariants

Region change alone does not authorize rebuild or source change.

``` text
SameAcceptedImageRequired=true
RegionalFailoverRequiresImageRebuild=false
RegionalFailoverRequiresSourceChange=false
```

Each target records region, resource group, plan, Web App, F1 tier,
Linux/container configuration, image identity, HTTPS/public-network
state, persistence configuration, active-WP configuration, and sanitized
quota/recovery state.

## 9. Secret governance

For WP05 the approved credential store remains:

``` text
Azure App Service application setting
Name=TwelveData__ApiKey
```

The user configures the credential through the approved secure mechanism
on each new target. Terra MUST NOT copy, print, retrieve for display,
persist, commit, or disclose the secret.

Permitted evidence:

``` text
SettingName=TwelveData__ApiKey
Present=true|false
NonEmpty=true|false
SecretValueDisclosed=false
```

If absent:

``` text
BoundaryBlocker=USER_CONTROLLED_TWELVE_DATA_CREDENTIAL_REQUIRED
```

## 10. Work-package evidence semantics

Regional failover invalidates only evidence whose truth depends on the
changed target. It does not automatically reopen completed WPs.

For the active WP, Terra MUST identify preserved evidence,
target-invalidated evidence, gates requiring replay, and gates not
requiring replay.

WP07 retains ownership of deployment stability/restart/recycle/redeploy
continuity acceptance. Failover during WP05 MUST NOT be labeled WP07
acceptance.

## 11. Quota-conservation policy

``` text
Reuse accepted image where permitted
Avoid redundant deploys and restarts
Use bounded polling
Collect exhaustive diagnostics before mutation
Fix all locally correctable defects before redeployment
Use one governed runtime cycle where safe
Preserve successful evidence
Do not consume reserve regions for ordinary defects
```

The convergence policy is one authority per governance boundary, not one
authority per defect.

## 12. Regional transition ledger

Every transition records:

``` text
TransitionSequence
PreviousRegion
PreviousResourceGroup
PreviousWebApp
PreviousState
QuotaAttribution
QuotaEvidenceReference
SelectedRegion
SelectionReason
NewResourceGroup
NewAppServicePlan
NewWebApp
Tier
ImageIdentity
SecretPresent
SecretValueDisclosed
AzureResourceCreateCount
AzureResourceDeleteCount
AppSettingMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount
DockerBuildCount
ImagePublicationCount
SourceMutationCount
GitMutationCount
PaidResourceMutationCount
ValidationGatesReplayed
ValidationResult
```

`SecretValueDisclosed` MUST always be `false`.

## 13. Recovery acceptance

``` text
REGIONAL F1 QUOTA RECOVERY — QUOTA ATTRIBUTION: PASS
REGIONAL F1 QUOTA RECOVERY — TARGET SELECTION: PASS
REGIONAL F1 QUOTA RECOVERY — ARCHITECTURE INVARIANTS: PASS
REGIONAL F1 QUOTA RECOVERY — $0 BOUNDARY: PASS
REGIONAL F1 QUOTA RECOVERY — SECRET HYGIENE: PASS
REGIONAL F1 QUOTA RECOVERY — MUTATION ACCOUNTING: PASS
RegionalRecoveryResult=PASS
```

The active WP then resumes under its own acceptance contract.

## 14. Governance boundaries and model roles

**GPT-5.6 Luna:** contract, policy, architecture, governance, planning
reconciliation, acceptance.\
**GPT-5.6 Terra:** implementation, deterministic regional recovery,
validation, authorized Azure/Git/GitHub mutations.\
**GPT-5.6 Sol:** supporting analysis/synthesis only; never silently
replaces Luna/Terra.

Luna reconciliation is mandatory if all four regions are unavailable, a
paid/unlisted target is proposed, topology/order changes, recovery needs
an architecture change, WP ownership becomes ambiguous, or production
HA/SLA is proposed.

## 15. Canonical policy markers

``` text
RELEASE_1_12_REGIONAL_F1_QUOTA_RECOVERY_POLICY=ACTIVE
PRIMARY_HISTORICAL_TARGET=WEST_CENTRAL_US
ALTERNATE_VALIDATION_TARGET=WEST_US_2
EMERGENCY_RESERVE_1=CENTRAL_US
EMERGENCY_RESERVE_2=SOUTH_CENTRAL_INDIA
QUOTA_EXCEEDED_RECOVERY=NEXT_AUTHORIZED_REGIONAL_F1_TARGET
QUOTA_ATTRIBUTION_REQUIRED=true
HISTORICAL_EVIDENCE_PRESERVATION=REQUIRED
AUTOMATIC_PAID_UPGRADE=false
UNLISTED_REGION_ALLOWED=false
UNBOUNDED_RESOURCE_CREATION=false
SECRET_COPY_BETWEEN_TARGETS=false
WP07_ACCEPTANCE_IMPLIED_BY_FAILOVER=false
ACTUAL_RECURRING_INFRASTRUCTURE_COST_TARGET=$0.00
```

## 16. Required planning reconciliation

Upon adoption, GPT-5.6 Luna MUST reconcile this policy into the Release
1.12 planning set so the earlier West Central US assumption is not
interpreted as an exclusive runtime-validation target.

At minimum review/reconcile:

``` text
RELEASE_1.12_DEFINITION.md
RELEASE_1.12_EXECUTION_PLAN.md
RELEASE_1.12_FILE_MANIFEST.md (only if path ownership changes)
WP05 / issue #264 where necessary
future WP06/WP07/WP08 acceptance language where target identity matters
```

Historical truth remains unchanged: Initiative-1.11 proved West Central
US feasibility and Release 1.12 began there. This policy adds bounded
quota-recovery targets; it does not rewrite that history. The Definition
and Execution Plan must state this bounded recovery topology, accepted
image reuse, fresh capacity proof, independent target secret setup, and
the distinction from WP07 acceptance before recovery implementation.

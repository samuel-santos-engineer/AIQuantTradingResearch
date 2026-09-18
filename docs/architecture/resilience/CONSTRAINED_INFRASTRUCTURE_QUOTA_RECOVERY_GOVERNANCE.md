# Project Governance --- Constrained Infrastructure Quota Recovery and Alternate Target Policy

**Document class:** Project-wide architecture/resilience governance\
**Canonical location:**
`docs/architecture/resilience/CONSTRAINED_INFRASTRUCTURE_QUOTA_RECOVERY_GOVERNANCE.md`\
**Scope:** AIQuantTradingResearch project-wide\
**Status:** Binding upon governance acceptance and merge\
**Policy/architecture authority:** GPT-5.6 Luna\
**Implementation/validation authority:** GPT-5.6 Terra\
**Supporting analysis:** GPT-5.6 Sol only; non-authoritative

## 1. Purpose

AIQuantTradingResearch may deliberately use free-tier, trial-tier,
quota-limited, capacity-limited, or otherwise constrained infrastructure
for reference deployments, demonstrations, qualification environments,
validation, CI/CD, or other governed project activities.

Quota exhaustion or provider capacity restriction MUST be treated as an
infrastructure constraint, not automatically as an application defect.

When a release or initiative explicitly opts into this policy and
defines an authorized recovery topology, confirmed quota/capacity
exhaustion MAY trigger deterministic recovery to an alternate target
while preserving architecture, provenance, cost, security, and evidence
boundaries.

This document defines the project-wide governance model.
Release-specific documents define concrete providers, regions, resource
names, target order, and acceptance gates.

## 2. Core principle

``` text
CONSTRAINED_INFRASTRUCTURE_FAILURE
        ↓
ATTRIBUTE_CAUSE
        ↓
QUOTA_OR_CAPACITY_CONFIRMED
        ↓
PRESERVE_EXISTING_TARGET_AND_EVIDENCE
        ↓
SELECT_NEXT_PREAUTHORIZED_TARGET
        ↓
RECREATE_EQUIVALENT_GOVERNED_RUNTIME
        ↓
REPLAY_ONLY_TARGET_DEPENDENT_GATES
        ↓
CONTINUE_ACTIVE_WORK
```

Automatic recovery is permitted only after cause attribution. Recovery
MUST NOT be used to conceal application, configuration, persistence,
networking, security, data, or test defects.

## 3. Policy activation

This policy does not automatically authorize alternate infrastructure
for every release.

A release/initiative MUST explicitly activate it and define a **Target
Recovery Profile** containing at least:

``` text
Provider
ServiceClass
CostBoundary
PrimaryTarget
OrderedAlternateTargets
MaximumAuthorizedTargets
ArchitectureInvariants
ArtifactIdentityRules
SecretDeliveryRules
EvidenceReplayRules
ExhaustionBoundary
```

Without an accepted Target Recovery Profile:

``` text
AutomaticAlternateTargetRecovery=false
```

## 4. Target roles

A Target Recovery Profile may define:

``` text
PRIMARY_HISTORICAL_TARGET
ALTERNATE_VALIDATION_TARGET
EMERGENCY_RESERVE_TARGET_1..N
```

The primary historical target remains the provenance anchor for work
already accepted there.

Alternate/reserve targets are recovery capacity, not replacements for
historical truth.

``` text
HistoricalEvidencePreservation=REQUIRED
TargetFailoverRewritesHistory=false
```

## 5. Deterministic target order

Alternate targets MUST have a canonical order before recovery execution.

Implementers MUST choose the next eligible target in that order rather
than selecting opportunistically.

Skipping a healthy eligible target requires an explicit recorded reason
permitted by the release profile, such as provider-side unavailability,
service-class unavailability, quota/capacity denial, or a
governance/security constraint.

This prevents uncontrolled region/account/resource shopping.

## 6. Cause-attribution gate

Before consuming an alternate target, evidence MUST distinguish
quota/capacity failure from other defects.

Qualifying evidence may include provider control-plane state, quota
counters, activity/event logs, service diagnostics, capacity responses,
or equivalent authoritative provider evidence.

The following are insufficient by themselves:

``` text
HTTP 4xx/5xx without quota attribution
application exception
container/process crash
missing secret
invalid configuration
network/access-policy failure
persistence failure
schema failure
test failure
source defect
ordinary transient request failure
```

Required transition state:

``` text
CurrentTargetState=CONSTRAINED
ConstraintAttribution=CONFIRMED
ApplicationDefectAttribution=false
RecoveryAuthorized=true
```

If attribution is ambiguous, diagnose before failover.

## 7. Bounded automatic recovery

"Automatic recovery" means execution of a pre-approved deterministic
policy. It does not mean unlimited autonomous infrastructure mutation.

Once the attribution gate passes, the implementation authority MAY:

``` text
1. Preserve the constrained target and durable evidence.
2. Select the next authorized target.
3. Verify required service/tier/capacity availability.
4. Create or reuse the minimum authorized resources.
5. Deploy the accepted artifact/image as required.
6. Apply only authorized non-secret configuration.
7. Stop for user-controlled secret material when required.
8. Verify architecture/cost/security invariants.
9. Replay only evidence invalidated by target identity.
10. Resume the active work package/release gate.
11. Record a complete recovery ledger.
```

Locally correctable defects discovered during recovery SHOULD converge
within the same implementation authority when they remain inside the
authorized boundary.

## 8. Resource and cost boundaries

Every Target Recovery Profile MUST define a hard resource ceiling and
cost policy.

Default project rule:

``` text
UnboundedResourceCreationAllowed=false
AutomaticPaidUpgradeAllowed=false
AutomaticCostBoundaryExpansionAllowed=false
```

A quota failure MUST NOT silently authorize a paid tier, different paid
service, additional commercial dependency, or materially different
architecture.

If no authorized target remains:

``` text
BoundaryBlocker=ALL_AUTHORIZED_RECOVERY_TARGETS_UNAVAILABLE
NextAuthorizedAction=GPT-5.6 Luna governance/architecture reconciliation
```

## 9. Architecture equivalence

Alternate targets MUST preserve the release-defined architecture
invariants.

Target recovery alone MUST NOT authorize: - source redesign; -
persistence redesign; - database substitution; - security weakening; -
networking bypass; - artifact rebuild without independent need; - schema
migration; - observability bypass; - secret-handling downgrade; -
production HA/SLA claims; - unrelated infrastructure additions.

Release-specific profiles may add stricter invariants.

## 10. Artifact and provenance preservation

Changing infrastructure target does not itself change the accepted
software artifact.

Default:

``` text
TargetRecoveryRequiresSourceChange=false
TargetRecoveryRequiresArtifactRebuild=false
AcceptedArtifactIdentityPreserved=true
```

If an independent defect requires byte-changing correction, normal
candidate hashing, build, test, provenance, and acceptance rules apply.

Already-accepted evidence remains valid unless its truth depends on the
changed target or is independently contradicted.

## 11. Evidence invalidation and replay

Recovery MUST use selective evidence replay, not indiscriminate
revalidation.

For every transition classify prior evidence as:

``` text
PRESERVED
TARGET_DEPENDENT_REPLAY_REQUIRED
SUPERSEDED_BY_NEW_TARGET_EVIDENCE
CONTRADICTED
```

Completed work packages MUST NOT be automatically reopened solely
because a later work package changes targets.

Ownership boundaries between work packages/releases remain binding.

## 12. Secret governance

Secrets MUST NOT be copied between targets through logs, prompts,
evidence, source files, Git history, or model-visible plaintext.

Each release profile defines its approved secret-delivery mechanism.

The implementation authority may verify safe metadata such as:

``` text
SecretSettingName=<name>
Present=true|false
NonEmpty=true|false
SecretValueDisclosed=false
```

When user-controlled secret material is required:

``` text
BoundaryBlocker=USER_CONTROLLED_CREDENTIAL_REQUIRED
```

Recovery pauses until the user supplies/configures it through the
approved secure mechanism.

## 13. Quota conservation

Constrained infrastructure is a scarce validation resource.

Authorities MUST minimize unnecessary consumption:

``` text
Reuse accepted artifacts
Avoid redundant deployments
Avoid redundant restarts
Bound polling and retries
Collect exhaustive diagnostics before mutation
Converge local defects before remote execution
Preserve valid evidence
Replay only invalidated gates
Do not consume reserve targets for ordinary defects
```

Project execution principle:

``` text
ONE_AUTHORITY_PER_GOVERNANCE_BOUNDARY
NOT_ONE_AUTHORITY_PER_DEFECT
```

## 14. Recovery ledger

Every target transition MUST create durable sanitized evidence
containing, as applicable:

``` text
TransitionSequence
Provider
ServiceClass
PreviousTarget
PreviousTargetState
ConstraintType
ConstraintAttribution
ConstraintEvidenceReference
SelectedTarget
SelectionReason
ArtifactIdentity
ArchitectureInvariantResult
CostBoundaryResult
SecretPresence
SecretValueDisclosed
ResourceCreateCount
ResourceDeleteCount
ConfigurationMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount
ArtifactBuildCount
ArtifactPublicationCount
SourceMutationCount
GitMutationCount
PaidResourceMutationCount
PreservedEvidence
ReplayedGates
RecoveryValidationResult
```

Secret values MUST never be included.

## 15. Failure and exhaustion semantics

The following are distinct states and MUST NOT be conflated:

``` text
APPLICATION_FAILURE
CONFIGURATION_FAILURE
CREDENTIAL_BOUNDARY
NETWORK_OR_ACCESS_FAILURE
PERSISTENCE_FAILURE
QUOTA_EXHAUSTION
PROVIDER_CAPACITY_UNAVAILABLE
AUTHORIZED_TARGETS_EXHAUSTED
```

Only confirmed quota/capacity states invoke alternate-target recovery.

When all authorized targets are exhausted, automatic recovery stops
fail-closed.

## 16. Production-availability boundary

This policy governs constrained-environment recovery and validation
continuity.

It MUST NOT be represented as production high availability, disaster
recovery, geographic redundancy, SLA capability, or business-continuity
certification unless a separate architecture explicitly establishes and
validates those properties.

``` text
RecoveryTopologyImpliesProductionHA=false
RecoveryTopologyImpliesSLA=false
```

## 17. Model authority

### GPT-5.6 Luna

Owns: - policy; - architecture; - Target Recovery Profile definition; -
governance reconciliation; - acceptance criteria; - boundary changes; -
exhaustion decisions.

### GPT-5.6 Terra

Owns: - implementation; - validation execution; - deterministic recovery
under an accepted profile; - authorized infrastructure/Git/GitHub
mutations; - mutation accounting.

### GPT-5.6 Sol

May provide: - supporting analysis; - synthesis; - alternatives; -
exploratory review.

Sol MUST NOT silently replace Luna or Terra authority.

## 18. Release-specific specialization

A release-specific recovery document MUST reference this project-wide
policy and define concrete values rather than redefining the general
governance model.

Example specialization:

``` text
Project policy
docs/architecture/resilience/CONSTRAINED_INFRASTRUCTURE_QUOTA_RECOVERY_GOVERNANCE.md

        ↓ specialized by

Release-specific Target Recovery Profile
docs/architecture/resilience/<release-specific-governance>.md
```

If a release-specific rule intentionally differs from this policy, the
exception MUST be explicit, justified, and accepted by Luna. Silence
does not override project governance.

## 19. Current Release 1.12 specialization

Release 1.12 specializes this policy with an Azure App Service Linux F1
profile whose governed topology is:

``` text
Primary historical target     West Central US
Alternate validation target   West US 2
Emergency reserve #1          Central US
Emergency reserve #2          South Central India
```

The Release 1.12 document remains the authority for its concrete Azure
topology, `$0.00` constraints, work-package evidence semantics, and
Twelve Data configuration.

The project-wide policy does not rewrite Release 1.12 history.

## 20. Canonical project markers

``` text
PROJECT_CONSTRAINED_INFRASTRUCTURE_RECOVERY_GOVERNANCE=ACTIVE
QUOTA_ATTRIBUTION_REQUIRED=true
DETERMINISTIC_TARGET_ORDER_REQUIRED=true
HISTORICAL_EVIDENCE_PRESERVATION=REQUIRED
AUTOMATIC_ALTERNATE_TARGET_RECOVERY=PROFILE_GOVERNED
UNBOUNDED_RESOURCE_CREATION=false
AUTOMATIC_PAID_UPGRADE=false
AUTOMATIC_COST_BOUNDARY_EXPANSION=false
SECRET_DISCLOSURE_ALLOWED=false
SELECTIVE_EVIDENCE_REPLAY=REQUIRED
PRODUCTION_HA_IMPLIED=false
AUTHORIZED_TARGET_EXHAUSTION=GOVERNANCE_BOUNDARY
```

## 21. Acceptance requirement

Before this becomes binding project governance, GPT-5.6 Luna MUST
perform a read-only reconciliation against existing project
architecture/resilience governance and identify any contradictions,
required cross-references, or superseded language.

After Luna acceptance, GPT-5.6 Terra may publish the accepted document
and approved cross-references through normal repository governance.

# Release 1.12 WP07 --- Terra Deployment Stability, Recovery, Cost & No-Bypass Validation

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract/policy/architecture/governance/final substantive acceptance
GPT-5.6 Terra = selected validation execution, bounded Azure mutation, evidence, and corrective implementation authority
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Work package

``` text
Release=1.12
Phase=4
WP=07
Issue=#266
Title=Deployment Stability, Recovery, Cost & No-Bypass Validation
Milestone=#63
```

## Dependency state

Binding predecessor state:

``` text
WP01=#260 CLOSED/DONE
WP02=#261 CLOSED/DONE
WP03=#262 CLOSED/DONE
WP04=#263 CLOSED/DONE
WP05=#264 CLOSED/DONE
WP06=#265 CLOSED/DONE
WP07=#266 OPEN
Milestone63=OPEN
WP07DependencyResult=PASS
```

Canonical repository anchor:

``` text
origin/main=f216e46245452e60cc5ea87584c490273f907b65
```

Accepted runtime target:

``` text
Region=West US 2
ResourceGroup=rg-aiq-r112-wp05-wus2-7f5eabb5
WebApp=aiqr112wp05wus27f5eabb5
ImageDigest=sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
State=Running
Availability=Normal
PublicHTTPS=PASS
Streamlit/SystemHealth=WP06 ACCEPTED
```

## Mission

Execute and converge WP07 only:

**Deployment Stability, Recovery, Cost & No-Bypass Validation**

WP07 is the Release 1.12 owner of deployment-level
restart/recycle/redeploy continuity and stability acceptance that was
deliberately excluded from WP04/WP05/WP06.

Validate the existing accepted West US 2 F1 deployment under bounded,
reversible, truthful recovery scenarios while preserving the \$0
architecture and no-bypass invariants.

Do not create a new architecture.

## First action: derive the exact tracked WP07 contract

Before any mutation, inspect: - issue #266; -
`docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md`; -
`docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md`; -
`docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md`; -
project-wide constrained-infrastructure recovery governance; - WP05
regional F1 governance and Luna reconciliation evidence; - WP03
deployment automation/evidence; - WP04 application persistence
evidence; - WP05 bounded provider evidence; - WP06 public
Streamlit/System Health evidence; - current Azure/container deployment
scripts and tests.

Produce the complete WP07 acceptance matrix before mutation.

If the tracked contract requires a new Luna architecture/policy
decision, stop at that genuine boundary. Otherwise execute all accepted
gates in one authority.

## Binding architecture

Preserve:

``` text
Azure App Service Linux F1
West US 2 accepted runtime target
custom Docker
public/default HTTPS/DNS
persistent /home
SQLite DELETE
public/free GHCR
bounded Twelve Data
.NET pipeline
JSON handoff
Python/Streamlit boundary
schema v3
truthful provenance/System Health
recurring infrastructure cost target=$0.00
no production SLA/HA claim
```

Prohibited: - paid App Service tier/resource; - Azure SQL; - Container
Apps; - Azure Files; - mandatory/private ACR; - paid monitoring; - Front
Door/Traffic Manager/load-balancer failover; - unlisted region; -
automatic paid upgrade; - unbounded resource creation; - secret
copying/disclosure; - schema migration; - architecture bypass.

## Stability scenarios

Derive exact scenarios from tracked WP07 planning. Unless the tracked
contract narrows them, evaluate the deployment-level continuity boundary
across:

``` text
S1 = ordinary steady-state observation
S2 = explicit App Service restart
S3 = platform/container recycle or equivalent bounded restart condition that can be safely and truthfully induced/observed
S4 = redeploy the SAME accepted immutable image digest to the SAME West US 2 target
```

Do not substitute historical platform-triggered restarts from WP05 for
WP07 acceptance unless the tracked WP07 contract explicitly allows
carried evidence.

Avoid destructive resource deletion/recreation unless explicitly
required by tracked WP07 planning. A redeploy should normally preserve
the existing F1 Web App and `/home`.

Do not create a second target for stability testing.

## Pre-mutation checkpoint --- mandatory

Before each mutating scenario capture authoritative baseline:

``` text
timestamp
resource group
web app
region
App Service plan/tier
state/availability
configured image digest
observed running image digest where available
public HTTPS result
Streamlit health result
System Health truth state
persistent SQLite path/identity metadata
schema version
journal mode
integrity/quick-check status where safely observable
authoritative persisted continuity sentinel/checkpoint
TwelveData credential presence/non-empty metadata only
required non-secret settings
recurring-cost evidence
```

Never expose the API key.

Use a new WP07-specific continuity sentinel/checkpoint if the tracked
contract requires one. Never reuse historical consumed validation
RunIds.

## Persistence/recovery contract

WP04 proved application-level SQLite
initialization/update/reopen/readback.

WP07 must now prove deployment-level continuity across its owned
scenarios.

For each applicable restart/recycle/redeploy event, establish after
recovery:

``` text
same persistent /home-backed database
schema remains expected
journal mode remains DELETE
integrity_check=ok
quick_check=ok
pre-event continuity sentinel remains readable
no unintended reinitialization/data loss
application returns to usable state
public Streamlit returns successfully
System Health remains truthful during and after recovery
```

If transient unavailability occurs during an authorized
restart/redeploy, record it truthfully; do not treat expected bounded
downtime itself as a production-SLA violation.

No production SLA is being claimed.

## Image/no-bypass contract

For every scenario:

``` text
ExpectedImageDigest=sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
```

Unless a correctable WP07 implementation defect genuinely requires
source/image changes, the final configured/running digest must remain
that accepted immutable digest.

Require: - no mutable-tag-only acceptance; - no local image bypass; - no
alternate container image; - no direct host process bypassing the
governed container; - no architecture path bypassing .NET → JSON →
Python/Streamlit; - no SQLite path moved off the accepted persistent
`/home` boundary; - no secret copied into image/source/evidence.

If a source/image correction becomes genuinely necessary, treat it as a
byte-changing candidate: full local validation, PR/merge, new immutable
digest, deploy, restart entire affected WP07 cycle. Do not silently
mutate the accepted image.

## Recovery timing/evidence

Measure, but do not invent SLA thresholds.

Record for each scenario: - mutation start; - first observed
unavailable/restarting state if any; - first App Service Running/Normal
observation; - first successful public HTTPS/Streamlit health; - first
successful persistence continuity verification; - total recovery
observation duration.

Classify recovery against the tracked WP07 contract. If no numeric SLA
exists, report observed durations descriptively rather than creating a
pass threshold.

## Cost validation

Prove the deployed reference architecture remains inside the strict
target:

``` text
ACTUAL_RECURRING_INFRASTRUCTURE_COST_TARGET=$0.00
```

Use authoritative Azure resource/SKU/configuration evidence.

Require no paid resources introduced by WP07.

Do not infer a billing guarantee beyond what the tracked reference
architecture and current resource configuration prove.

Return a resource/cost inventory sufficient to show: - App Service plan
is F1/free; - no paid companion resource was introduced; - registry
architecture remains public/free GHCR; - no paid recovery/HA service was
added.

## No-bypass validation

Evaluate all tracked no-bypass gates exhaustively, including as
applicable:

``` text
F1 tier preserved
West US 2 target preserved
immutable GHCR digest preserved
public/default HTTPS/DNS preserved
custom Docker preserved
persistent /home preserved
SQLite DELETE preserved
.NET pipeline preserved
JSON handoff preserved
Python/Streamlit boundary preserved
bounded Twelve Data preserved
System Health truth preserved
secret boundary preserved
no paid fallback
no architecture substitution
no WP08 final-acceptance claim
```

## Public/System Health behavior during recovery

WP06 acceptance must remain preserved.

System Health must not fabricate healthy status from
missing/malformed/stale evidence during recovery.

If the UI is unavailable because the container is restarting, record
endpoint unavailability rather than claiming an application health state
that cannot be observed.

After recovery, revalidate the accepted System Health truthfulness
contract.

## Secret hygiene

Never retrieve/display/copy the value of:

``` text
TwelveData__ApiKey
```

Evidence only:

``` text
SettingName=TwelveData__ApiKey
Present=true|false
NonEmpty=true|false
SecretValueDisclosed=false
```

No secret copying between targets is authorized.

## Bounded convergence --- mandatory

Evaluate ALL WP07 gates on each cycle.

For correctable defects within WP07 scope:

``` text
collect all failures
→ diagnose all failures
→ fix all in-scope defects
→ if bytes change, invalidate prior hashes/image evidence
→ rerun complete affected local validation
→ publish/merge as required
→ deploy exact immutable candidate
→ restart complete WP07 scenario cycle
→ repeat until PASS
```

Do not return after each correctable defect.

Stop only for: - new Luna architecture/policy decision; -
paid/cost-boundary expansion; - secret/user-controlled input; - unlisted
regional recovery; - schema/database architecture change; - WP08
ownership; - destructive action not authorized by tracked WP07 contract.

## Mutation minimization

Do not issue redundant restarts.

Each explicit restart/redeploy must correspond to a distinct required
WP07 scenario.

Count platform-triggered restarts separately from explicit restarts.

Do not modify App Settings unless a tracked WP07 requirement makes it
necessary.

## PowerShell compatibility

All scripts/commands must remain compatible with:

``` text
Windows PowerShell 5.1.26100.9444
```

## README preservation

README mutation is not authorized unless explicitly required by the
tracked WP07 contract.

If required, preserve all substantive existing information under the
project-wide README preservation policy.

## Publication

If WP07 requires no source/docs/configuration changes, do not create a
gratuitous PR.

If evidence documents are explicitly tracked deliverables under WP07
planning, publish only those required artifacts after validation.

If source/test/docs bytes change: - dedicated WP07 branch; - exact path
scope; - full relevant validation; - secret scan; - PR/merge; - fresh
origin/main; - new image only if runtime bytes changed.

Do not close #266 in this authority.

## Required scenario matrix

Return:

``` text
Scenario
MutationPerformed
ExpectedImageDigest
ConfiguredImageDigestAfter
ObservedImageDigestAfter
AppServiceStateAfter
AvailabilityAfter
PublicHttpsAfter
StreamlitHealthAfter
PersistenceSentinelBefore
PersistenceSentinelAfter
ContinuityResult
SchemaResult
JournalModeResult
IntegrityResult
QuickCheckResult
SystemHealthTruthResult
RecoveryDurationObserved
SecretBoundaryResult
CostBoundaryResult
NoBypassResult
ScenarioResult
```

## Mutation accounting

Return exact counts:

``` text
SourceChangedPathCount
TestChangedPathCount
DocumentationChangedPathCount
EvidenceChangedPathCount
READMETrackedMutationCount
GitCommitCount
GitPushCount
PRCreateCount
PRMergeCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
AzureAppSettingMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount
RecycleScenarioMutationCount
AzureResourceCreateCount
AzureResourceDeleteCount
PaidResourceMutationCount
SecretDisclosureCount
IssueMutationCount
ProjectMutationCount
MilestoneMutationCount
```

## Required output

``` text
SelectedModel
EntryHEAD
EntryOriginMain
ActualStartHEAD
ActualStartOriginMain
MainAdvanceClassification

WP07IssueContractResult
WP07PlanningContractResult
WP07AcceptanceMatrix
ArchitectureBoundaryResult
WP08OwnershipResult

PreValidationResourceInventory
CostInventoryResult
RecurringCostTargetResult

ContinuitySentinelIdentity
ContinuitySentinelCreationResult
PreScenarioPersistenceResult

ScenarioMatrix

SteadyStateResult
ExplicitRestartResult
RecycleResult
SameDigestRedeployResult
DeploymentContinuityResult
PersistenceContinuityResult
RecoveryResult
CostValidationResult
NoBypassValidationResult

FinalConfiguredImageDigest
FinalObservedImageDigest
FinalImageIdentityMatch
FinalWestUS2State
FinalWestUS2Availability
FinalPublicHttpsResult
FinalStreamlitHealthResult
FinalSystemHealthTruthfulnessResult

WP03HistoricalEvidencePreserved
WP04HistoricalEvidencePreserved
WP05AcceptancePreserved
WP06AcceptancePreserved
WP07AcceptanceClaimed
WP08AcceptanceClaimed

SourceChangedPathCount
TestChangedPathCount
DocumentationChangedPathCount
EvidenceChangedPathCount
READMETrackedMutationCount
GitCommitCount
GitPushCount
PRCreateCount
PRMergeCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
AzureAppSettingMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount
RecycleScenarioMutationCount
AzureResourceCreateCount
AzureResourceDeleteCount
PaidResourceMutationCount
SecretDisclosureCount
IssueMutationCount
ProjectMutationCount
MilestoneMutationCount

FinalHEAD
FinalOriginMain
FinalTrackedClean
FinalStagedPathCount

WP07ValidationResult
WP07Result
BoundaryBlocker
NextAuthorizedAction
```

## PASS gate

Only when every tracked WP07 gate is proven:

``` text
RELEASE 1.12 WP07 — DEPLOYMENT STABILITY: PASS
RELEASE 1.12 WP07 — RESTART CONTINUITY: PASS
RELEASE 1.12 WP07 — RECYCLE CONTINUITY: PASS
RELEASE 1.12 WP07 — REDEPLOY CONTINUITY: PASS
RELEASE 1.12 WP07 — PERSISTENT SQLITE CONTINUITY: PASS
RELEASE 1.12 WP07 — RECOVERY VALIDATION: PASS
RELEASE 1.12 WP07 — COST VALIDATION: PASS
RELEASE 1.12 WP07 — NO-BYPASS VALIDATION: PASS
RELEASE 1.12 WP07 — SYSTEM HEALTH TRUTH PRESERVATION: PASS
RELEASE 1.12 WP07 — SECRET HYGIENE: PASS
RELEASE 1.12 WP07 — HISTORICAL ACCEPTANCE PRESERVATION: PASS
RELEASE 1.12 WP07 — $0 ARCHITECTURE BOUNDARY: PASS

WP07ValidationResult=PASS
WP07Result=READY_FOR_LUNA_ACCEPTANCE
BoundaryBlocker=NONE
NextAuthorizedAction=GPT-5.6 Luna performs final read-only WP07 substantive acceptance reconciliation
```

Do not close issue #266 or set Project #2 WP07 Status to Done in this
authority.

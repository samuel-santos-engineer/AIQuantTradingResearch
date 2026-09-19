# Release 1.12 WP06 — Terra Public Streamlit / System Health Deployment & Truthful Diagnostics

**Selected execution model: GPT-5.6 Terra**

## Authority roles

```text
GPT-5.6 Luna = contract/policy/architecture/governance/final substantive acceptance
GPT-5.6 Terra = selected implementation, validation, Git/GitHub, image, deployment, and approved Azure mutation authority
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Work package and entry

```text
Release=1.12
WP=06
Issue=#265
Title=Public Streamlit/System Health Deployment & Truthful Diagnostics
Milestone=#63
origin/main=f216e46245452e60cc5ea87584c490273f907b65
WP01..WP05=CLOSED/DONE
WP06=#265 OPEN
```

Accepted WP05 runtime target:

```text
Region=West US 2
ResourceGroup=rg-aiq-r112-wp05-wus2-7f5eabb5
WebApp=aiqr112wp05wus27f5eabb5
CandidateImageDigest=sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
State=Running
Availability=Normal
HTTPS=200
```

## Mission

Implement and converge WP06 only: **Public Streamlit/System Health Deployment & Truthful Diagnostics**. Use the existing accepted Release 1.12 architecture and West US 2 target. Make the minimum source/configuration/documentation/deployment changes necessary for the public Streamlit UI and System Health surface to truthfully represent the deployed system.

Before editing, inspect issue #265, Release 1.12 definition/execution plan/file manifest, Release 1.10 observability/System Health contracts and current implementation, Streamlit entry/configuration, .NET→JSON→Python handoff, container composition, tests, and WP03/WP04/WP05 provenance. Derive the exact tracked WP06 acceptance matrix. Stop only if a genuinely new Luna architecture/policy decision is required.

## Binding architecture

Preserve App Service Linux F1, West US 2, custom Docker, public/default HTTPS/DNS, persistent `/home`, SQLite DELETE, public/free GHCR, bounded Twelve Data, .NET pipeline, one-shot JSON handoff, Python/Streamlit boundary, schema v3 unless separately authorized, truthful provenance/System Health, recurring-cost target `$0.00`, and no production SLA/HA claim.

Excluded: Azure SQL, Container Apps, Azure Files, mandatory/private ACR, paid monitoring/services, paid App Service, load-balancing/failover infrastructure, schema migration, ML/backtesting/live trading, WP07 stability acceptance, WP08 final release acceptance.

## System Health truthfulness

Preserve Release 1.10 System Health semantics unless Release 1.12 explicitly extends them. Display only facts supported by authoritative evidence. Reconcile applicable fields such as application/UI reachability, pipeline/provenance state, persisted dataset/database availability, schema version, SQLite journal mode, latest bounded data-update/run state, provider configuration presence/status only, deployment/version/image identity when authoritative, and timestamps with explicit meaning/source.

Never fabricate Azure platform health from inside Streamlit. Never convert unknown evidence into healthy. Use the project's existing state semantics or truthful equivalents such as `HEALTHY`, `DEGRADED`, `UNAVAILABLE`, and `UNKNOWN/NOT_PROVEN`.

Public diagnostics must not expose API keys, connection secrets, environment dumps, Azure/GHCR credentials, sensitive absolute paths, or raw exceptions containing sensitive configuration.

## Provenance

Displayed run IDs, timestamps, dataset targets, provider identity, image/version identity, schema/journal state must come from authoritative evidence. Preserve WP03/WP04/WP05 historical evidence. Never reuse consumed historical validation RunIds as new execution identities.

## Implementation / image rule

Determine whether source changes are actually required. If not, do not rebuild gratuitously. If bytes change: implement locally → complete affected validation → dedicated PR/merge → build/publish new immutable GHCR digest → deploy exact digest to existing West US 2 Web App → verify observed running digest. A source/image-changing correction invalidates the WP05 digest as the final WP06 candidate, while preserving it as historical evidence.

## Public/runtime validation

Require successful public HTTPS and usable Streamlit behavior, not merely a generic upstream response. Verify no access restriction contradicts the public-reference contract. Validate System Health field-by-field against authoritative backend/deployment facts with a truth matrix:

```text
DisplayedField
DisplayedValueOrState
AuthoritativeEvidenceSource
ExpectedValueOrState
Match
Sensitive
TruthfulnessResult
```

Use deterministic tests for degraded/unavailable/unknown rendering where destructive live manipulation or provider-quota consumption is unnecessary.

## Testing

Run all canonical gates applicable to changed paths, including as applicable: Release build, Domain/Application/Architecture/Infrastructure tests, Python/Streamlit tests, System Health/provenance tests, container/deployment contract tests, full canonical baseline, `git diff --check`, secret scan, and public-diagnostics sensitive-data scan. Do not weaken tests.

## Mandatory bounded convergence

```text
evaluate ALL gates
→ collect ALL failures
→ fix ALL in-scope defects
→ rerun COMPLETE affected local cycle
→ fresh commit/image digest after byte changes
→ deploy only after local PASS
→ evaluate ALL runtime gates
→ fix ALL in-scope runtime defects
→ repeat until PASS
```

Do not return after each correctable defect. Stop only for a real governance/security/user-controlled boundary, paid-cost expansion, out-of-scope schema/database architecture, WP07/WP08 ownership, or unauthorized regional transition.

All PowerShell must remain compatible with `Windows PowerShell 5.1.26100.9444`.

README preservation policy remains binding. Do not modify README unless WP06 requires it; if required, inventory existing information and preserve all substantive information.

## Git/GitHub and Azure boundaries

If changes are required, use a dedicated WP06 branch/PR and verify exact path scope before merge. Do not close #265 or set WP06 Done in this authority.

Azure mutations are limited to the existing West US 2 target and only what WP06 requires: accepted immutable-image deployment and minimal non-secret configuration. Do not create a new target, copy/disclose secrets, upgrade F1, add paid resources, delete historical resources, or perform WP07 stability qualification. Avoid redundant explicit restarts.

## Required output

```text
SelectedModel
EntryHEAD
EntryOriginMain
ActualStartHEAD
ActualStartOriginMain
MainAdvanceClassification
WP06IssueContractResult
WP06PlanningContractResult
WP06AcceptanceMatrix
ArchitectureBoundaryResult
CostBoundaryResult
WP07OwnershipResult
WP08OwnershipResult
SourceChangeRequired
SourceChangedPathCount
SourceChangedPaths
TestChangedPathCount
TestChangedPaths
DocumentationChangedPathCount
DocumentationChangedPaths
ConfigurationChangedPathCount
ConfigurationChangedPaths
READMEPreservationResult
ReleaseBuildResult
DomainTestsResult
ApplicationTestsResult
ArchitectureTestsResult
InfrastructureTestsResult
PythonStreamlitTestsResult
SystemHealthTestsResult
FullBaselineResult
GitDiffCheckResult
SecretScanResult
PublicDiagnosticsSensitiveDataScanResult
PublicationRequired
PublicationBranch
PublicationCommit
PublicationPR
PublicationPRMerged
PublicationMergeSHA
PostMergeOriginMain
PreviousImageDigest
WP06CandidateImageDigest
ImageBuildResult
GhcrPublicationResult
ConfiguredImageDigest
ObservedRunningImageDigest
ImageIdentityMatch
WestUS2ResourceGroup
WestUS2WebApp
WestUS2State
WestUS2Availability
PublicHttpsResult
PublicStreamlitUsabilityResult
SystemHealthRouteOrViewResult
SystemHealthTruthMatrix
SystemHealthTruthfulnessResult
ProvenanceTruthResult
UnknownStateHandlingResult
DegradedStateHandlingResult
UnavailableStateHandlingResult
CredentialDisclosureResult
SensitiveDiagnosticsResult
WP03HistoricalEvidencePreserved
WP04HistoricalEvidencePreserved
WP05AcceptancePreserved
WP06AcceptanceClaimed
WP07AcceptanceClaimed
WP08AcceptanceClaimed
SourceChangedPathCount
TestChangedPathCount
DocumentationChangedPathCount
ConfigurationChangedPathCount
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
WP06ImplementationResult
WP06RuntimeValidationResult
WP06Result
BoundaryBlocker
NextAuthorizedAction
```

## PASS gate

```text
RELEASE 1.12 WP06 — PUBLIC STREAMLIT DEPLOYMENT: PASS
RELEASE 1.12 WP06 — SYSTEM HEALTH TRUTHFULNESS: PASS
RELEASE 1.12 WP06 — PROVENANCE TRUTH: PASS
RELEASE 1.12 WP06 — PUBLIC DIAGNOSTICS SECRET HYGIENE: PASS
RELEASE 1.12 WP06 — IMMUTABLE IMAGE IDENTITY: PASS
RELEASE 1.12 WP06 — WEST US 2 RUNTIME VALIDATION: PASS
RELEASE 1.12 WP06 — HISTORICAL ACCEPTANCE PRESERVATION: PASS
RELEASE 1.12 WP06 — $0 ARCHITECTURE BOUNDARY: PASS
WP06ImplementationResult=PASS
WP06RuntimeValidationResult=PASS
WP06Result=READY_FOR_LUNA_ACCEPTANCE
BoundaryBlocker=NONE
NextAuthorizedAction=GPT-5.6 Luna performs final read-only WP06 substantive acceptance reconciliation
```

Do not close issue #265 or set Project #2 WP06 Status to Done in this authority.

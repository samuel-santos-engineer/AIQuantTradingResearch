# Release 1.12 WP05 --- Luna Source-Level Bounded Provider Automation Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority roles

``` text
GPT-5.6 Luna = selected contract/policy/architecture/governance acceptance authority
GPT-5.6 Terra = implementation, tests, image publication, Azure deployment, approved mutations
GPT-5.6 Sol = supporting analysis only; non-authoritative
```

## Mode

READ-ONLY. No source, Git/GitHub, Docker/GHCR, Azure, settings, restart,
deployment, secret, issue, Project, or milestone mutations.

## Established evidence

``` text
West US 2 App Service=Running / Normal
HTTPS=200
Accepted image identity=MATCH
Required non-secret settings=present
TwelveData__ApiKey=present and non-empty
Secret disclosed=false
BoundedAutomationValidationResult=FAIL
FailureIsolationResult=NOT_PROVEN
WP05Result=BLOCKED
BoundaryBlocker=WP05_SOURCE_LEVEL_BOUNDED_PROVIDER_AUTOMATION_REQUIRED
```

No setting, source, image, restart, or deployment mutation occurred
during the rerun.

Inspect current source, especially:

``` text
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataClient.cs
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataObservationSource.cs
```

Reported: provider request lacks explicit timeout/retry/bounded
cancellation deadline, and the observation source blocks synchronously.
Inspect the full call chain, DI/configuration, interfaces, worker
boundary, tests, and existing cancellation/timeout abstractions rather
than assuming this is complete.

## Mission

Define the minimum source-level contract required for WP05 bounded
Twelve Data automation. Reconcile the newly proven WP05 source defect
with the earlier regional-recovery image-reuse rule. Evaluate all gates
in one pass.

Determine: 1. authoritative provider deadline location; 2. required
end-to-end cancellation propagation and whether synchronous blocking
must be removed; 3. whether retries are required, optional, or
prohibited; 4. if retries are allowed, exact finite attempt/backoff
bounds and retryable failures; 5. isolation/surfacing of timeout, caller
cancellation, HTTP/provider errors, malformed payloads, and rate limits;
6. HttpClient timeout vs explicit linked cancellation deadline; 7.
configurable timeout/retry parameters and bounded defaults/maxima; 8.
deterministic tests proving no unbounded wait; 9. interface signature
changes needed for cancellation; 10. remaining `.Result`, `.Wait()`, or
equivalent blocking on provider path; 11. schema/database/provenance
impact; 12. whether correction requires a new WP05 candidate image.

## Constraints

Preserve .NET pipeline, JSON handoff, Python/Streamlit boundaries,
schema v3 unless separately authorized, SQLite DELETE, public/free GHCR,
App Service Linux F1, recurring-cost target \$0.00, truthful
provenance/System Health, and no production SLA/HA claim. No paid
dependency/new external service.

Do not assume retries are desirable. A finite deadline with zero
automatic retries may satisfy boundedness and avoid quota/rate-limit
amplification. Any authorized retry policy must be finite and
deterministic.

`TwelveData__ApiKey` remains an App Service setting. Never
retrieve/display/copy/commit the value. Tests require no real
credential.

Evaluate this image semantic reconciliation:

``` text
RegionalRecoveryAloneRequiresRebuild=false
WP05SourceDefectCorrectionRequiresRebuild=true
OldAcceptedImageRemainsHistoricalEvidence=true
NewWP05CandidateImageRequired=true
```

## Test contract

Require deterministic tests without live Twelve Data for: success within
deadline; provider exceeding deadline; caller cancellation; non-success
response; malformed response where relevant; retry behavior if
authorized; maximum request/attempt bound; no synchronous provider
blocking; no secret leakage. Prefer controllable fake handlers/tokens
over timing-race tests.

Classify each change:

``` text
WP05_IN_SCOPE
REQUIRES_PLANNING_AMENDMENT
REQUIRES_NEW_ARCHITECTURE_AUTHORITY
OUT_OF_SCOPE
```

Avoid planning churn if existing WP05 "bounded automation" already owns
the correction.

## Required output

``` text
SelectedModel
EntryHEAD
EntryOriginMain
WP05PlanningAuthorityResult
TwelveDataClientFinding
ObservationSourceFinding
ProviderCallChainFinding
SynchronousBlockingFinding
ExistingCancellationSupport
ExistingTimeoutSupport
ExistingRetrySupport
BoundedProviderContract
AuthoritativeDeadlineLocation
CancellationPropagationContract
RetryPolicyDecision
RetryAttemptLimit
RetryBackoffContract
RetryableFailureClasses
NonRetryableFailureClasses
TimeoutConfigurationContract
TimeoutDefault
TimeoutMaximum
RequiredSourceChanges
RequiredInterfaceChanges
RequiredConfigurationChanges
RequiredTestChanges
RequiredDocumentationChanges
SchemaChangeRequired
DatabaseChangeRequired
PythonChangeRequired
StreamlitChangeRequired
RegionalRecoveryAloneRequiresRebuild
WP05SourceDefectCorrectionRequiresRebuild
OldAcceptedImageHistoricalStatus
NewWP05CandidateImageRequired
SecretBoundaryResult
CostBoundaryResult
ArchitectureInvariantResult
WP07OwnershipResult
ChangeScopeMatrix
PlanningAmendmentRequired
NewArchitectureAuthorityRequired
DefectCount
Defects
LunaSourceReconciliationResult
TerraSourceCorrectionAuthorized
NextAuthorizedAction
```

## PASS gate

If existing WP05 scope owns the defect and the minimum correction is
precise:

``` text
RELEASE 1.12 WP05 — SOURCE-LEVEL BOUNDED PROVIDER CONTRACT: PASS
RELEASE 1.12 WP05 — CANCELLATION/DEADLINE CONTRACT: PASS
RELEASE 1.12 WP05 — FAILURE ISOLATION CONTRACT: PASS
RELEASE 1.12 WP05 — SECRET BOUNDARY: PASS
RELEASE 1.12 WP05 — $0 ARCHITECTURE BOUNDARY: PASS
PlanningAmendmentRequired=false
NewArchitectureAuthorityRequired=false
LunaSourceReconciliationResult=PASS
TerraSourceCorrectionAuthorized=true
NextAuthorizedAction=GPT-5.6 Terra implements and converges the accepted WP05 bounded-provider source/test correction, publishes a new candidate image, deploys it to the accepted West US 2 target, and reruns the complete affected WP05 validation gates
```

Otherwise return NOT_READY with the complete defect/scope matrix and do
not authorize mutation.

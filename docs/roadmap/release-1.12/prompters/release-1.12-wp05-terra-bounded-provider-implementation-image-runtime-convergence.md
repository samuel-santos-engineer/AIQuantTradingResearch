# Release 1.12 WP05 --- Terra Bounded Provider Implementation, Image Publication & West US 2 Validation

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract, policy, architecture, governance, acceptance
GPT-5.6 Terra = selected implementation, validation, Git/GitHub, Docker/GHCR, and authorized Azure mutation authority
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Binding Luna decision

Entry reconciliation:

``` text
EntryHEAD=1bbc8362dd3c3bcf56bb9d14f5efb1ec4450802d
EntryOriginMain=1bbc8362dd3c3bcf56bb9d14f5efb1ec4450802d
WP05PlanningAuthorityResult=PASS

LunaSourceReconciliationResult=PASS
TerraSourceCorrectionAuthorized=true
PlanningAmendmentRequired=false
NewArchitectureAuthorityRequired=false

RegionalRecoveryAloneRequiresRebuild=false
WP05SourceDefectCorrectionRequiresRebuild=true
OldAcceptedImageHistoricalStatus=true
NewWP05CandidateImageRequired=true
```

Accepted defects:

``` text
D1 = TwelveData provider request/content retrieval lacks an authoritative finite deadline.
D2 = TwelveDataObservationSource synchronously blocks on async provider work.
```

Accepted provider contract:

``` text
finite end-to-end deadline
caller cancellation preserved
zero automatic retries
exactly one request attempt
deterministic failure mapping
no synchronous provider blocking
```

## Mission

In one bounded convergence authority:

1.  implement the Luna-accepted source/configuration/interface
    correction;
2.  add deterministic tests and documentation;
3.  run the complete affected structural/test gate set until PASS;
4.  create/publish the corrected source candidate through normal Git/PR
    governance;
5.  build and publish a new WP05 candidate image only after source/test
    acceptance;
6.  deploy that exact image to the already accepted West US 2 F1 target;
7.  rerun all affected WP05 runtime/bounded-automation/failure-isolation
    gates;
8.  converge through correctable in-scope defects until PASS or a
    genuine governance/security/user-controlled boundary.

Do NOT stop after each ordinary source/test/configuration defect.

## Entry verification

Require local main and `origin/main` to reconcile to the expected entry
unless main legitimately advanced.

If main advanced, inspect the delta. Continue only if the Luna contract
remains semantically valid and no conflicting provider implementation
has landed. Otherwise stop at a Luna boundary.

Record:

``` text
EntryHEAD
EntryOriginMain
ActualStartHEAD
ActualStartOriginMain
MainAdvanceClassification
```

## Implementation contract

Inspect the full production provider call path before editing.

Expected principal paths include:

``` text
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataClient.cs
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataObservationSource.cs
```

and the actual current paths defining: - `IObservationSource`; -
application/use-case callers; - worker caller; - Twelve Data
options/configuration/DI; - relevant unit/integration tests; - relevant
deployment/runtime documentation.

Do not invent paths.

### Async/cancellation contract

Production Twelve Data observation retrieval must be asynchronous
end-to-end.

Add/propagate an async contract equivalent in semantics to:

``` text
GetObservationsAsync(ResearchRequest, CancellationToken)
```

through the ordinary provider path.

No production Twelve Data adapter may bridge async work with:

``` text
.Result
.Wait()
.GetAwaiter().GetResult()
```

or equivalent synchronous blocking.

If compatibility requires a synchronous API elsewhere, it must not be
the Twelve Data provider implementation and must not reintroduce
blocking into this path.

### Authoritative deadline

The authoritative deadline belongs in `TwelveDataClient` around the
complete provider request + response-content read/parse operation.

Use a linked token preserving caller cancellation while adding a finite
provider deadline.

Caller cancellation and provider deadline expiry must remain
distinguishable.

Do not rely on an unbounded/default external wait.

### Retry contract

``` text
AutomaticRetryCount=0
MaximumRequestAttempts=1
RetryBackoff=None
```

Do not add Polly or another retry dependency merely for WP05.

Do not retry timeout, cancellation, transport failures, HTTP/provider
failures, malformed payloads, normalization failures, or rate limits.

This avoids quota/rate-limit amplification.

## Timeout configuration contract

Luna requires a bounded default and maximum but intentionally left
numeric values to the WP05 implementation contract.

Terra must derive reasonable finite values from existing project runtime
expectations and establish them as explicit constants/options with
validation.

Requirements:

``` text
TimeoutDefault > 0
TimeoutMaximum >= TimeoutDefault
ConfiguredTimeout > 0
ConfiguredTimeout <= TimeoutMaximum
```

Prefer seconds-based configuration with a clear Twelve Data-specific
name consistent with the project's .NET configuration conventions.

The default must be short enough to prove bounded automation on F1 and
long enough for a normal single Twelve Data request under ordinary
network conditions.

Do not make timeout unlimited/zero-as-infinite.

Record the chosen values and rationale.

A timeout configuration choice within these constraints is
implementation detail, not a new Luna round-trip.

## Failure mapping

Deterministically distinguish at least:

``` text
CALLER_CANCELLED
PROVIDER_DEADLINE_EXCEEDED
TRANSPORT_FAILURE
HTTP_OR_PROVIDER_FAILURE
MALFORMED_PROVIDER_PAYLOAD
NORMALIZATION_FAILURE
SUCCESS
```

Preserve existing domain exception conventions where available; do not
create unnecessary exception hierarchies.

Never include the API key in exception messages/logs/evidence.

## Secret hygiene

`TwelveData__ApiKey` remains configuration-only.

Never: - print it; - retrieve it into evidence; - copy it between
targets; - commit it; - place it in test fixtures; - include it in
command lines or logs where it can be exposed.

Tests must use fake/non-secret placeholder credentials only.

Azure evidence is limited to:

``` text
SettingName=TwelveData__ApiKey
Present=true|false
NonEmpty=true|false
SecretValueDisclosed=false
```

## Deterministic test contract

Add tests covering all applicable cases:

``` text
success within deadline
provider deadline expiry
caller cancellation
transport failure
non-success HTTP/provider response
malformed payload
normalization failure where independently testable
exactly one request attempt
zero retry behavior
bounded timeout validation
timeout maximum validation
absence of synchronous provider blocking
secret not leaked in errors/log evidence
```

Prefer controllable fake `HttpMessageHandler`, completion sources, and
cancellation tokens over sleep-heavy timing races.

A test proving deadline expiry may use a tightly controlled bounded
timing mechanism only if deterministic cancellation cannot otherwise be
simulated.

Search the production provider path for forbidden synchronous blocking
patterns as an explicit structural gate.

## Scope

Authorized source scope is the minimum set required to implement this
WP05 contract: - Twelve Data client/provider implementation; -
observation-source interface and callers; - timeout
options/configuration/validation/DI; - deterministic tests; - directly
related WP05/provider documentation.

Not authorized: - schema/database changes; - Python changes; - Streamlit
changes; - README deletion/compression or unrelated README mutation; -
WP06/WP07/WP08 implementation; - unrelated refactoring; - new paid
dependency/service; - retry framework introduction; - production HA/SLA
changes.

README preservation policy remains binding.

## Structural convergence

For every source/test/document correction inside this authority:

``` text
evaluate ALL gates
→ collect ALL failures
→ fix ALL in-scope failures
→ fresh candidate hashes
→ rerun the COMPLETE affected structural/test cycle
→ repeat until all gates PASS
```

Do not return after the first correctable defect.

Every byte-changing correction invalidates previous candidate
hashes/PASS evidence.

Stop only if correction requires: - architecture/planning change beyond
Luna contract; - schema/database/Python/Streamlit mutation; - paid
resource/service; - secret disclosure; - WP06/WP07/WP08 ownership; -
unrelated source scope.

## Required local validation

Run the repository's canonical build/test/lint/format/contract gates
applicable to the changed paths.

At minimum require:

``` text
dotnet restore/build = PASS
all relevant existing tests = PASS
all new bounded-provider tests = PASS
full repository test baseline = PASS
git diff --check = PASS
secret scan = PASS
forbidden provider synchronous-blocking scan = PASS
retry attempt bound = PASS
timeout bound = PASS
```

Do not weaken or delete existing tests to obtain PASS.

PowerShell baseline remains:

``` text
Windows PowerShell 5.1.26100.9444
```

## Source publication

After complete local PASS: - create a dedicated WP05 source branch; -
commit only authorized changed paths; - push; - create PR through normal
governance; - verify PR diff exactly matches accepted source/test/docs
scope; - merge only after all required gates pass; - record PR number
and merge SHA; - fetch/reconcile main; - do not close #264 or mark WP05
Done yet.

No WP05 lifecycle completion is authorized until runtime acceptance.

## New image

Only after source PR merge and fresh-main verification:

Build a new candidate image from the accepted merged source.

Requirements:

``` text
OldAcceptedImageHistoricalStatus=PRESERVED
NewWP05CandidateImageRequired=true
```

Use the existing public/free GHCR publication architecture.

Capture immutable digest.

The new digest, not a mutable tag alone, becomes the candidate identity
for runtime validation.

Do not delete the historical image.

## West US 2 deployment

Deploy the new candidate digest to the already accepted West US 2 F1 App
Service.

Do not create another regional target unless the existing accepted
target has been independently lost.

Preserve: - F1; - Linux/custom Docker; - HTTPS/public front door; -
persistent `/home`; - SQLite DELETE; - canonical non-secret settings; -
existing user-controlled API-key setting; - strict \$0 boundary.

An App Service container-setting/image change may platform-trigger a
restart. Count that truthfully. Do not issue a redundant explicit
restart unless Azure requires it and evidence shows the
platform-triggered restart did not occur.

## Runtime validation

After deployment, validate exhaustively:

``` text
App Service state = Running / Normal
HTTPS = 200
deployed digest = new candidate digest
required non-secret settings = present/correct
TwelveData__ApiKey present = true
TwelveData__ApiKey non-empty = true
SecretValueDisclosed = false
provider request bounded by configured deadline
maximum provider request attempts = 1
caller cancellation isolated
provider deadline isolated
provider/HTTP failure isolated
malformed payload failure isolated where runtime-safe
no unbounded synchronous provider wait
bounded automation = PASS
failure isolation = PASS
```

Do not deliberately consume provider quota merely to manufacture every
negative live-runtime condition when deterministic tests already prove
it. Use live validation only where it adds necessary target evidence.

Preserve historical WP03/WP04 evidence. Do not claim WP07
restart/recycle/redeploy stability acceptance from this deployment.

## Runtime convergence

If runtime validation reveals a correctable WP05 defect within this
accepted source/configuration contract:

``` text
diagnose ALL failures
→ correct all in-scope defects
→ rerun full local structural/test cycle
→ new commit/PR as required
→ build a NEW image digest
→ deploy
→ rerun complete affected runtime cycle
```

Continue until PASS.

Do not reuse a superseded candidate digest as final evidence.

Avoid unnecessary F1 restarts/deployments; aggregate local fixes before
each deployment.

## Mutation accounting

Return exact counts:

``` text
SourceChangedPathCount
TestChangedPathCount
DocumentationChangedPathCount
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
READMETrackedMutationCount
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

TimeoutSettingName
TimeoutDefault
TimeoutMaximum
ConfiguredTimeout
AutomaticRetryCount
MaximumRequestAttempts

AsyncProviderContractResult
CancellationPropagationResult
ProviderDeadlineResult
CallerCancellationIsolationResult
ProviderTimeoutIsolationResult
FailureMappingResult
SynchronousBlockingScanResult
RetryBoundResult
TimeoutValidationResult
SecretHygieneResult

NewTestCount
NewTestCases
RelevantTestsResult
FullRepositoryTestsResult
BuildResult
GitDiffCheckResult
SecretScanResult

SourceChangedPathCount
SourceChangedPaths
TestChangedPathCount
TestChangedPaths
DocumentationChangedPathCount
DocumentationChangedPaths

SourcePR
SourcePRMerged
SourceMergeSHA
PostMergeOriginMain

OldAcceptedImageDigest
OldAcceptedImageHistoricalStatus
NewCandidateImageDigest
ImageBuildResult
GhcrPublicationResult

WestUS2ResourceGroup
WestUS2WebApp
WestUS2State
WestUS2Availability
WestUS2HttpsResult
WestUS2DeployedImageDigest
ImageIdentityMatch

TwelveDataSettingPresent
TwelveDataSettingNonEmpty
SecretValueDisclosed
RequiredNonSecretSettingsResult

LiveProviderBoundaryResult
BoundedAutomationValidationResult
FailureIsolationResult
WP03HistoricalEvidencePreserved
WP04HistoricalEvidencePreserved
WP07AcceptanceClaimed

SourceChangedPathCount
TestChangedPathCount
DocumentationChangedPathCount
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
READMETrackedMutationCount
IssueMutationCount
ProjectMutationCount
MilestoneMutationCount

FinalHEAD
FinalOriginMain
FinalTrackedClean
FinalStagedPathCount

WP05ImplementationResult
WP05RuntimeValidationResult
WP05Result
BoundaryBlocker
NextAuthorizedAction
```

## Final PASS gate

Only when implementation, tests, immutable image publication/deployment,
and affected runtime gates all pass:

``` text
RELEASE 1.12 WP05 — BOUNDED PROVIDER IMPLEMENTATION: PASS
RELEASE 1.12 WP05 — ASYNC/CANCELLATION PROPAGATION: PASS
RELEASE 1.12 WP05 — PROVIDER DEADLINE: PASS
RELEASE 1.12 WP05 — ZERO-RETRY / ONE-ATTEMPT BOUND: PASS
RELEASE 1.12 WP05 — FAILURE ISOLATION: PASS
RELEASE 1.12 WP05 — NEW CANDIDATE IMAGE: PASS
RELEASE 1.12 WP05 — WEST US 2 DEPLOYMENT: PASS
RELEASE 1.12 WP05 — BOUNDED AUTOMATION: PASS
RELEASE 1.12 WP05 — RUNTIME VALIDATION: PASS
RELEASE 1.12 WP05 — SECRET HYGIENE: PASS
RELEASE 1.12 WP05 — $0 ARCHITECTURE BOUNDARY: PASS

WP05ImplementationResult=PASS
WP05RuntimeValidationResult=PASS
WP05Result=READY_FOR_LUNA_ACCEPTANCE
BoundaryBlocker=NONE
NextAuthorizedAction=GPT-5.6 Luna performs final read-only WP05 substantive acceptance reconciliation
```

Do not close #264 or set Project #2 WP05 Done in this authority.
Lifecycle follows only after exact Luna substantive acceptance.

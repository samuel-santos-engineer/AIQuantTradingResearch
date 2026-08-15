# Release 1.0 WP11 --- Worker Market Data Execution --- Codex Prompt

## Role

Act as the **WP11 Worker Market Data Execution Executor** for Release
1.0 of `AIQuantTradingResearch`.

WP02--WP10 established the provider-independent research contracts, the
Twelve Data transport/normalization/validation boundary, and the runtime
dependency graph. WP11 owns the **composition-root handoff and
executable Worker market-data flow** only.

The accepted runtime dependency chain is:

``` text
Worker
  ↓
IResearchUseCase
  ↓
ResearchUseCase
  ↓
IObservationSource
  ↓
TwelveDataObservationSource
  ↓
TwelveDataClient
  ↓
HttpClient
  ↓
Twelve Data /time_series
```

WP11 must make the Worker supply the accepted external provider
configuration and execute the existing research use case through that
graph. It must not redesign Domain, Application, Infrastructure provider
semantics, or pull WP12/WP13 permanent test work forward.

Do not start WP12.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before mutation:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md

docs/roadmap/release-1.0/prompts/02-market-data-provider-discovery-codex-prompt.md
docs/roadmap/release-1.0/prompts/03-market-data-domain-evolution-codex-prompt.md
docs/roadmap/release-1.0/prompts/04-market-data-application-contracts-codex-prompt.md
docs/roadmap/release-1.0/prompts/05-historical-market-data-use-case-integration-codex-prompt.md
docs/roadmap/release-1.0/prompts/06-provider-transport-model-codex-prompt.md
docs/roadmap/release-1.0/prompts/07-provider-http-client-codex-prompt.md
docs/roadmap/release-1.0/prompts/08-market-data-normalization-codex-prompt.md
docs/roadmap/release-1.0/prompts/08-market-data-normalization-semantic-unblock-codex-prompt.md
docs/roadmap/release-1.0/prompts/09-market-data-validation-failure-mapping-codex-prompt.md
docs/roadmap/release-1.0/prompts/10-dependency-registration-configuration-codex-prompt.md
docs/roadmap/release-1.0/prompts/11-worker-market-data-execution-codex-prompt.md

docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read the accepted WP10 execution report from the current context.

Read GitHub issues:

``` text
#96 — WP11 Worker Market Data Execution
#97 — WP12 Domain & Application Tests
#98 — WP13 Infrastructure & Provider Tests
#99 — WP14 Architecture Evolution
```

Inspect repository truth, especially:

``` text
src/AIQuantTradingResearch.Worker/
src/AIQuantTradingResearch.Application/
src/AIQuantTradingResearch.Infrastructure/
```

and all configuration/composition files authorized by the Release 1.0
manifest.

Authority precedence:

1.  `RELEASE_1.0_EXECUTION_PLAN.md`
2.  `RELEASE_1.0_FILE_MANIFEST.md`
3.  Accepted WP02 provider decision
4.  Accepted WP03--WP10 cumulative implementation
5.  GitHub issue #96
6.  Existing architecture/composition conventions
7.  This prompt

If a material conflict exists, stop and return `BLOCKED`.

------------------------------------------------------------------------

## 2. WP10 Predecessor Gate

Before mutation prove:

``` text
WP01–WP10 = complete
provider = Twelve Data
WP03 Domain delta = zero
IResearchUseCase exists
ResearchUseCase consumes IObservationSource
TwelveDataObservationSource implements IObservationSource
TwelveDataClient exists
TwelveDataTimeSeriesNormalizer exists
WP09 six-outcome mapping is complete
WP09 cancellation semantics are preserved
TwelveDataConfiguration exists
TwelveData:ApiKey is the accepted external configuration path
IObservationSource runtime implementation = TwelveDataObservationSource
IObservationSource runtime registration count = 1
TwelveDataClient / HttpClient graph resolves offline
deterministic source remains compiled but is not the configured runtime source
Worker still uses the pre-WP11 composition path
WP11 issue = #96
WP12 has not started
```

Verify exact constructors, method signatures, configuration names,
registrations, visibility, and Worker baseline from code. Do not infer
them from prior reports alone.

------------------------------------------------------------------------

## 3. Objective

Implement the minimum authorized Worker changes required to:

1.  read the accepted Twelve Data API-key configuration through the
    existing .NET host configuration system;
2.  construct/pass the accepted `TwelveDataConfiguration` to the
    existing Infrastructure registration boundary;
3.  resolve the existing `IResearchUseCase`;
4.  execute one historical market-data research request through the real
    configured Twelve Data observation source;
5.  present the existing provider-independent research outcome
    deterministically;
6.  terminate cleanly.

WP11 is the first executable integration of the accepted Release 1.0
vertical slice.

Do not redesign any lower layer.

------------------------------------------------------------------------

## 4. Ownership Boundary

Preserve:

``` text
Domain
  owns financial/domain values and invariants

Application
  owns research contracts, use case, provider-independent failures

Infrastructure
  owns Twelve Data HTTP, DTOs, normalization, provider validation/failure mapping,
  provider configuration type, and dependency registrations

Worker
  owns host configuration handoff, request construction, use-case invocation,
  user/process output, and process exit behavior
```

Worker must not absorb Infrastructure provider mechanics.

------------------------------------------------------------------------

## 5. WP10 Configuration Handoff

WP10 established:

``` text
section = TwelveData
mandatory key = ApiKey
canonical external path = TwelveData:ApiKey
configuration type = TwelveDataConfiguration
base URI = Infrastructure-owned
```

WP11 must use that contract exactly.

Do not rename the section/key, duplicate the base URI in Worker, or
introduce an alternate secret name.

Use normal .NET configuration precedence already available to the
Worker.

------------------------------------------------------------------------

## 6. Secret Handling

The API key is a runtime secret.

Forbidden:

``` text
hardcoded real API key
committed real API key
API key in command output
API key in exception/report output
API key in URI/query
API key in logs
API key in test snapshots
API key copied into Domain/Application
custom plaintext secret file
```

Do not add a real secret to `appsettings.json`, launch settings,
scripts, documentation, prompts, or source.

If a committed configuration artifact is authorized, it may express
structure only and must contain no credential.

------------------------------------------------------------------------

## 7. Missing Configuration

Missing or blank `TwelveData:ApiKey` must fail deterministically and
safely.

Reuse the accepted WP10 configuration validation behavior rather than
inventing a second validation contract.

The Worker may translate the configuration/composition failure into
appropriate process-level output/exit behavior if the existing execution
plan explicitly requires it, but must not expose the secret or a stack
trace as normal user output.

Do not silently fall back to `DeterministicObservationSource`.

------------------------------------------------------------------------

## 8. Infrastructure Registration Invocation

Replace the pre-WP11 parameterless Infrastructure registration usage
with the accepted configured registration path.

Conceptually:

``` text
configuration
  ↓
TwelveDataConfiguration
  ↓
AddInfrastructure(config)
```

Use the actual accepted WP10 API, not this pseudocode if signatures
differ.

Do not modify Infrastructure merely to make Worker code more convenient
unless the manifest explicitly authorizes such a correction. If the
accepted WP10 API cannot be consumed from Worker within the manifest,
stop as `BLOCKED`.

------------------------------------------------------------------------

## 9. Application Registration

Continue using the existing Application registration boundary.

Do not manually construct `ResearchUseCase`.

Do not change:

``` text
IResearchUseCase → ResearchUseCase
```

or its accepted lifetime.

------------------------------------------------------------------------

## 10. Research Request

Use the exact Release 1.0 Worker request semantics authorized by the
execution plan/file manifest.

Do not invent a CLI, interactive prompt, command-line parser,
symbol-selection framework, date-range framework, or configurable
research workflow unless explicitly authorized.

Inspect the existing Release 0.9 Worker request baseline and Release 1.0
authority to determine the smallest required change.

The Worker must create an existing `ResearchRequest`; it must not create
provider-specific request DTOs.

------------------------------------------------------------------------

## 11. Provider Independence

Worker may know that provider configuration must be supplied to
Infrastructure because it is the composition root.

Worker execution logic must remain provider-independent after
composition.

Forbidden in research-flow logic:

``` text
Twelve Data response DTOs
/time_series parsing
HTTP status codes
JSON
provider error codes
exchange_timezone
close field
adjust=splits mechanics
Authorization header mechanics
normalizer invocation
provider-specific failure branches
```

Those remain Infrastructure concerns.

------------------------------------------------------------------------

## 12. Use-Case Execution

Resolve `IResearchUseCase` from DI and invoke its existing operation
exactly once for the authorized one-shot Worker flow.

Do not bypass the use case and call `IObservationSource` directly.

Do not manually resolve `TwelveDataClient`.

Do not create a second service provider.

Do not use a service locator.

------------------------------------------------------------------------

## 13. Cancellation

Preserve existing cancellation semantics.

If the current Worker has no long-running host lifecycle and Release 1.0
authority does not add one, do not introduce hosted/background services
merely for cancellation.

Pass the appropriate cancellation token through the existing use-case
operation.

Do not convert cancellation into a provider failure or generic research
failure.

------------------------------------------------------------------------

## 14. Successful Output

Reuse the existing `ResearchResult`/outcome contract.

Output must be deterministic and provider-independent.

Do not print raw provider JSON, provider DTOs, request URLs,
authentication information, or Infrastructure internals.

Preserve existing canonical output semantics unless Release 1.0
authority explicitly evolves them.

If the real market-data result necessarily changes values from the
Release 0.9 deterministic sample, that is expected; the **shape and
ownership of output** should remain governed by the existing research
contract.

------------------------------------------------------------------------

## 15. Failure Output

Handle the existing Application research failure vocabulary, including:

``` text
InvalidRequest
UnsupportedTarget
InsufficientObservations
SourceUnavailable
AccessDenied
UsageLimitReached
InvalidSourceResponse
```

Use actual enum/type names from repository truth.

Do not create provider-specific Worker failures.

Do not parse Infrastructure/provider error details in Worker.

Keep output concise and deterministic.

------------------------------------------------------------------------

## 16. Process Exit Semantics

Follow existing Worker conventions and Release 1.0 authority for
success/failure exit codes.

At minimum distinguish successful completion from unsuccessful execution
if the existing Worker already does so or the execution plan requires
it.

Do not invent a broad exit-code taxonomy without authority.

Report the exact final behavior.

------------------------------------------------------------------------

## 17. Live Provider Boundary

WP11 is the executable real-provider vertical slice, but validation must
remain safe.

Separate:

``` text
A. mandatory offline repository validation
B. optional/authorized live execution evidence
```

Never require a real credential for canonical build/test/verification.

If the user/environment has not supplied a valid Twelve Data key, do not
fabricate one and do not treat absence of a live call as a code defect
if all offline criteria pass and authority permits offline validation.

If the WP11 authority explicitly requires live-provider proof for
completion and no credential is available, return `BLOCKED` rather than
weakening the gate.

------------------------------------------------------------------------

## 18. Live Call Safety

If a live provider call is authorized and a credential is already
externally available:

``` text
perform only the minimum call needed
do not print the key
do not print authenticated URI/header material
do not persist response payload unless explicitly authorized
do not loop/retry
do not consume unnecessary quota
```

Record only non-secret evidence needed for acceptance.

Do not change provider code based on transient live data unless a
genuine implementation defect is proven and the correction is within
WP11 authority.

------------------------------------------------------------------------

## 19. Offline Validation Strategy

Canonical validation must work with no Twelve Data connectivity and no
credential.

At minimum prove:

``` text
Worker builds
DI/configuration wiring compiles
missing configuration follows deterministic behavior
Domain/Application/Infrastructure permanent suites remain green
Architecture.Tests remain green
eng/verify.ps1 remains green
```

If running Worker without a key intentionally returns the accepted
missing-configuration result, record that as an offline Worker probe.

------------------------------------------------------------------------

## 20. Temporary Probe

A temporary offline probe is allowed only if needed to prove Worker
composition without network access.

It may use safe placeholder configuration and test-owned/fake outer
behavior only if this can be done without modifying production contracts
or adding permanent test infrastructure.

Remove all temporary artifacts before completion.

Do not add `InternalsVisibleTo` or broaden production visibility without
separate authority.

------------------------------------------------------------------------

## 21. No Test-Scope Pull-Forward

WP12 owns Domain & Application tests.

WP13 owns Infrastructure & Provider tests.

WP11 must not add their comprehensive permanent tests.

Only add a permanent Worker test if the Release 1.0 file manifest
explicitly assigns one to WP11. Otherwise use inspection/offline
execution/temporary proof and defer systematic coverage.

------------------------------------------------------------------------

## 22. No Infrastructure Redesign

Do not change accepted behavior of:

``` text
TwelveDataClient
TwelveDataTransportResult
TwelveDataTimeSeriesNormalizer
TwelveDataNormalizationResult
TwelveDataObservationSource
TwelveDataConfiguration
Infrastructure DependencyInjection
```

except for a narrowly proven WP11 integration correction explicitly
authorized by the manifest.

Do not add retries, resilience, caching, storage, provider selection,
telemetry redesign, or new abstractions.

------------------------------------------------------------------------

## 23. Domain / Application Protection

Target:

``` text
Domain changes = 0
Application changes = 0
```

Do not change research contracts merely to make Worker output easier.

Do not add provider concepts to Domain/Application.

If WP11 requires such a change, stop as `BLOCKED`.

------------------------------------------------------------------------

## 24. Package / Project Protection

Maintain:

``` text
new packages = 0
new project references = 0
solution membership changes = 0
```

unless the Release 1.0 file manifest explicitly authorizes otherwise.

Do not add command-line/configuration libraries. Use existing framework
capabilities.

------------------------------------------------------------------------

## 25. Authorized Files

`RELEASE_1.0_FILE_MANIFEST.md` is exact authority.

Create or modify only files assigned to WP11.

Likely scope is Worker composition/execution/configuration, but this
prompt does not itself authorize filenames.

If a necessary file is outside the manifest, stop as `BLOCKED`.

------------------------------------------------------------------------

## 26. Worker Lifecycle

Preserve the one-shot Release 0.9 lifecycle unless Release 1.0 authority
explicitly changes it:

``` text
compose
resolve
construct request
execute once
write result/failure
exit
```

Do not convert the Worker into a daemon, polling service, scheduler,
hosted background service, API, or interactive shell.

------------------------------------------------------------------------

## 27. Configuration Source Evidence

Report the actual configuration flow without exposing values:

``` text
Host/application configuration source(s)
    ↓
TwelveData:ApiKey
    ↓
TwelveDataConfiguration
    ↓
Infrastructure registration
    ↓
TwelveDataClient header authentication
```

State how a developer/operator can supply the secret using existing .NET
configuration precedence, but do not create a real secret artifact.

------------------------------------------------------------------------

## 28. Runtime Graph Evidence

After WP11 prove:

  Stage                        Runtime type/boundary
  ---------------------------- -------------------------------
  Worker composition           actual
  Research service             `IResearchUseCase`
  Application implementation   `ResearchUseCase`
  Observation seam             `IObservationSource`
  Runtime source               `TwelveDataObservationSource`
  Transport                    `TwelveDataClient`
  HTTP                         `HttpClient`

Also prove `DeterministicObservationSource` is not selected by runtime
DI.

------------------------------------------------------------------------

## 29. Failure-Mapping Evidence

Worker must consume Application outcomes only.

Provide a matrix showing how each Application-level result is
rendered/handled at the process boundary without provider-specific
branching.

Do not claim permanent behavioral test coverage that belongs to
WP12/WP13 unless it actually exists.

------------------------------------------------------------------------

## 30. Security Evidence

Perform targeted scans proving:

``` text
hardcoded real API key = 0
committed credential = 0
API key in URI/query = 0
credential logging/output = 0
provider response dump = 0
```

Do not include any secret discovered during execution in the report.

------------------------------------------------------------------------

## 31. Provider Leakage Evidence

Prove:

``` text
Domain Twelve Data references = 0
Application Twelve Data references = 0
ResearchUseCase provider branches = 0
Worker transport DTO references = 0
Worker JSON references for provider handling = 0
Worker HTTP-status handling = 0
Worker provider-error-code handling = 0
```

Composition-root references required to instantiate accepted
Infrastructure configuration are allowed only to the minimum extent
authorized by architecture.

------------------------------------------------------------------------

## 32. Regression Protection

Prove preservation of:

``` text
WP03 zero Domain delta
WP04 failure vocabulary
WP05 source→research failure propagation
WP06 transport DTO model
WP07 endpoint, interval=1day, outputsize, adjust=splits, header authentication
WP08 close-price normalization and timestamp semantics
WP09 validation/failure mapping and cancellation
WP10 single runtime IObservationSource registration
WP10 Infrastructure-owned base URI
WP10 TwelveData:ApiKey contract
WP10 offline resolution behavior
```

Do not opportunistically clean up prior work.

------------------------------------------------------------------------

## 33. Architecture Protection

Production graph must remain:

``` text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Prove:

``` text
cycles = 0
new project references = 0
Application → Infrastructure = 0
Domain → Infrastructure = 0
Infrastructure → Worker = 0
Architecture.Tests = PASS
```

------------------------------------------------------------------------

## 34. Build and Validation

At minimum run the commands required by repository authority, including:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo

dotnet test tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo

powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1

git diff --check
git diff --cached --check
```

Also execute the safe Worker validation authorized by this prompt/plan:

``` text
without real credential → deterministic missing-configuration behavior
with externally supplied credential → only if authorized/available and required
```

Record exact commands/results while redacting secret values.

Established `NU1900` feed-connectivity warnings remain non-blocking only
if all mandatory validation succeeds.

------------------------------------------------------------------------

## 35. Live Execution Decision

The report must explicitly state one of:

``` text
LIVE PROVIDER EXECUTION REQUIRED AND PASSED
LIVE PROVIDER EXECUTION REQUIRED BUT UNAVAILABLE — BLOCKED
LIVE PROVIDER EXECUTION NOT REQUIRED FOR WP11 ACCEPTANCE
LIVE PROVIDER EXECUTION OPTIONAL AND PERFORMED
LIVE PROVIDER EXECUTION OPTIONAL AND NOT PERFORMED
```

Derive this from the authoritative Release 1.0 plan, not preference.

Never claim live execution if none occurred.

------------------------------------------------------------------------

## 36. Diff / Formatting Validation

Require:

``` text
git diff --check = PASS
git diff --cached --check = PASS
staged files = 0
temporary artifacts = 0
format verification = PASS
```

Investigate unexpected line-ending or encoding changes rather than
normalizing unrelated files.

------------------------------------------------------------------------

## 37. Git / GitHub Protection

Do not:

``` text
stage
commit
create branch
push
create PR
merge
stash
discard cumulative work
close issue #96
modify milestone #41
modify Project status/fields
create Release 1.1 planning
```

WP11 ends as a validated local cumulative Release 1.0 candidate.

------------------------------------------------------------------------

## 38. Working-Tree Classification

Classify every visible change:

``` text
EXPECTED GOVERNANCE
WP02 AUTHORIZED
WP03 AUTHORIZED
WP04 AUTHORIZED
WP05 AUTHORIZED
WP06 AUTHORIZED
WP07 AUTHORIZED
WP08 AUTHORIZED
WP08 SEMANTIC UNBLOCK AUTHORIZED
WP09 AUTHORIZED
WP10 AUTHORIZED
WP11 AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

List exact WP11 files.

Expected:

``` text
staged = 0
unexpected = 0
```

Do not hide unexplained changes.

------------------------------------------------------------------------

## 39. Scope Protection

Explicitly prove WP11 did **not** start:

``` text
WP12 Domain/Application test expansion
WP13 Infrastructure/provider test expansion
WP14 architecture evolution
WP15 documentation alignment
WP16 full acceptance/integration
Release 1.0 closure
Release 1.1
```

Also prove no storage, caching, streaming, multi-provider selection,
AI/ML, plugin, retry/resilience, or production hosting expansion was
introduced.

------------------------------------------------------------------------

## 40. Acceptance / Exit Criteria

WP11 is complete only if all mandatory criteria pass:

``` text
WP10 predecessor gate = PASS

Worker consumes existing .NET configuration
Worker uses exact TwelveData:ApiKey contract
real credential committed = 0
hardcoded real credential = 0
credential logging/output = 0

Worker uses configured Infrastructure registration
parameterless pre-WP11 registration path no longer used by executable Worker
IResearchUseCase resolved through DI
ResearchUseCase manually constructed = NO
IObservationSource directly invoked by Worker = NO
TwelveDataClient directly invoked by Worker = NO

runtime IObservationSource = TwelveDataObservationSource
runtime observation-source registration count = 1
DeterministicObservationSource selected at runtime = NO

research request uses existing Application contract
provider DTOs in Worker execution logic = 0
provider HTTP handling in Worker = 0
provider failure-code handling in Worker = 0

success output remains provider-independent
failure output uses Application failure vocabulary only
cancellation semantics preserved
one-shot lifecycle preserved

Domain changes = 0
Application changes = 0
unauthorized Infrastructure behavior changes = 0
new packages = 0 unless explicitly authorized
new project references = 0 unless explicitly authorized
architecture graph preserved

canonical validation requires live provider = NO unless authority explicitly says otherwise
required live-provider gate = satisfied if authority requires it

build = PASS with zero errors
Domain.Tests = PASS
Application.Tests = PASS
Infrastructure.Tests = PASS
Architecture.Tests = PASS
eng/verify.ps1 = PASS
git diff --check = PASS
git diff --cached --check = PASS
staged files = 0
temporary artifacts = 0
unexpected mutations = 0

WP12 started = NO
```

If a mandatory real-provider execution is required but no credential is
externally available, return `BLOCKED`.

If Worker integration requires changing Domain/Application contracts or
unauthorized Infrastructure semantics, return `BLOCKED`.

------------------------------------------------------------------------

## 41. Required Execution Report

Return:

``` text
# Release 1.0 WP11 — Worker Market Data Execution Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP10 Predecessor Gate
## 5. Existing Worker Baseline
## 6. Configuration Handoff Design
## 7. TwelveData Configuration Binding
## 8. Infrastructure Registration Invocation
## 9. Application Registration / Resolution
## 10. Research Request
## 11. One-Shot Execution Flow
## 12. Success Output Behavior
## 13. Failure Output Behavior
## 14. Process Exit Behavior
## 15. Cancellation Behavior
## 16. Runtime Dependency Graph
## 17. Deterministic Source Exclusion
## 18. Provider-Independence Evidence
## 19. Types Added or Modified
## 20. Files Changed
## 21. Configuration Evidence
## 22. Secret / Security Evidence
## 23. Offline Worker Evidence
## 24. Live Provider Execution Decision / Evidence
## 25. Failure Handling Matrix
## 26. WP03–WP10 Regression Evidence
## 27. Application / Domain Preservation
## 28. Dependency / Architecture Evidence
## 29. Build Evidence
## 30. Test / Temporary-Probe Evidence
## 31. Canonical Verification
## 32. Diff / Formatting Validation
## 33. Working-Tree Classification
## 34. Scope Protection
## 35. Findings / Observations
## 36. Exit-Criteria Assessment
## 37. Final Repository State
## 38. Final Decision
## 39. Next Authorized Action
```

The report must include exact non-secret configuration names, actual
Worker composition calls, runtime graph, request semantics,
success/failure output behavior, exit behavior, live-execution decision,
test counts, and complete validation evidence.

Clearly distinguish:

``` text
WP07 = provider HTTP transport
WP08 = provider normalization
WP09 = provider validation/failure mapping
WP10 = DI/configuration graph
WP11 = Worker configuration handoff + executable research flow
WP12 = Domain/Application permanent tests — NOT STARTED
WP13 = Infrastructure/provider permanent tests — NOT STARTED
```

------------------------------------------------------------------------

## 42. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP11 WORKER MARKET DATA EXECUTION COMPLETE
RELEASE 1.0 WP11 WORKER MARKET DATA EXECUTION COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP11 WORKER MARKET DATA EXECUTION BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when every mandatory criterion
passes and remaining findings are non-blocking.

------------------------------------------------------------------------

## 43. Next Authorized Action

If WP11 completes successfully:

``` text
WP12 — Domain & Application Tests
GitHub issue #97
```

Do not execute WP12.

Stop after the WP11 report.

------------------------------------------------------------------------

## Execution Instruction

Read all Release 1.0 authorities and the cumulative accepted WP02--WP10
implementation; prove the WP10 predecessor gate from repository truth;
modify only WP11-authorized Worker/composition/configuration files;
replace the obsolete parameterless Infrastructure registration usage
with the accepted `TwelveDataConfiguration` handoff sourced from
`TwelveData:ApiKey`; resolve and execute the existing `IResearchUseCase`
exactly once through the configured `TwelveDataObservationSource`; keep
Worker execution provider-independent; preserve the one-shot lifecycle,
cancellation, Application contracts, Domain, WP07 transport, WP08
normalization, WP09 mapping, and WP10 DI semantics; introduce no real
secret, package, project reference, provider mechanics, or later-WP test
scope; determine the authoritative live-provider validation requirement
without inventing credentials; run all mandatory offline Worker,
security, leakage, regression, architecture, build, test, verification,
and diff checks; classify the complete working tree; return the full
WP11 execution report; and stop before WP12.

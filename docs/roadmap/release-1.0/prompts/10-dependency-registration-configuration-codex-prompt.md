# Release 1.0 WP10 --- Dependency Registration & Configuration --- Codex Prompt

## Role

Act as the **WP10 Dependency Registration & Configuration Executor** for
Release 1.0 of `AIQuantTradingResearch`.

WP06--WP09 established the Infrastructure market-data acquisition
boundary:

``` text
TwelveDataClient                  ← WP07 transport
        ↓
TwelveDataTransportResult
        ↓
TwelveDataObservationSource       ← WP09 IObservationSource adapter
        ↓
TwelveDataTimeSeriesNormalizer    ← WP08 normalization
        ↓
ObservationSourceResult
```

WP10 owns **composition and configuration only**. It must make the
accepted Twelve Data implementation resolvable through the existing
dependency-injection boundary while keeping credentials, provider
mechanics, and configuration concerns in the outer layers.

WP10 must not redesign WP06--WP09 behavior, execute the market-data flow
in Worker, or pull WP13 permanent provider-test scope forward. Do not
start WP11.

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
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read the accepted WP09 execution report from the current context. Read
GitHub issues #95, #96, #98, and #99.

Inspect repository truth, especially Infrastructure dependency
registration, Worker composition, configuration files, project/package
manifests, and the accepted WP07--WP09 Twelve Data types.

Authority precedence:

1.  `RELEASE_1.0_EXECUTION_PLAN.md`
2.  `RELEASE_1.0_FILE_MANIFEST.md`
3.  Accepted WP02 provider decision
4.  Accepted WP03--WP09 cumulative implementation
5.  GitHub issue #95
6.  Existing architecture/configuration conventions
7.  This prompt

If a material conflict exists, stop.

## 2. Predecessor Gate

Before mutation prove:

``` text
WP01–WP09 = complete
provider = Twelve Data
WP03 Domain delta = zero
IObservationSource exists in Application
ResearchUseCase consumes IObservationSource
TwelveDataClient exists
TwelveDataTimeSeriesNormalizer exists
TwelveDataObservationSource exists
TwelveDataObservationSource implements IObservationSource
WP09 six-outcome mapping is complete
WP09 cancellation behavior is preserved
existing deterministic observation source still exists
WP10 issue = #95
WP11 has not started
```

Verify actual names, constructors, visibility, and registration
boundaries from code.

## 3. Objective

Implement the minimum authorized configuration and
dependency-registration changes needed to compose:

``` text
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
```

Configuration supplies only settings required by the accepted WP07
client. Exact registration APIs and file locations are controlled by
repository truth and the file manifest.

## 4. Composition Ownership

Preserve:

``` text
Application registration → Application-owned services only
Infrastructure registration → Infrastructure implementations/provider mechanics
Worker → composition root only
```

Do not register provider implementations from Application. Do not move
Twelve Data types into Worker. Do not make Domain aware of
DI/configuration.

## 5. Application Registration Preservation

Inspect the accepted Application registration. Preserve
`IResearchUseCase → ResearchUseCase` behavior and lifetime unless
Release 1.0 authority explicitly changes it.

Target: zero Application production changes.

If composition requires an unauthorized Application redesign, return
`BLOCKED`.

## 6. Runtime Observation Source

Release 0.9 used the deterministic Infrastructure source. Release 1.0
must compose:

``` text
IObservationSource → TwelveDataObservationSource
```

The deterministic source may remain in the codebase unless the manifest
explicitly authorizes deletion.

After WP10 prove exactly one runtime `IObservationSource` registration.
Avoid ambiguous multiple registrations.

## 7. Infrastructure Registration

Use the existing Infrastructure registration extension/boundary. Do not
create a parallel registration mechanism.

Register only dependencies required by the accepted implementation:

``` text
TwelveDataClient
TwelveDataObservationSource
IObservationSource → TwelveDataObservationSource
HttpClient required by TwelveDataClient
provider configuration required by TwelveDataClient
```

Exact registrations must follow actual constructors and existing
conventions. Do not use a service locator or resolve services during
registration.

## 8. HttpClient Composition

WP07 owns HTTP behavior; WP10 only composes it.

Use the narrowest built-in .NET DI/HTTP mechanism compatible with the
accepted `TwelveDataClient`. Prefer existing packages.

Do not change `/time_series`, `interval=1day`, `adjust=splits`,
output-size semantics, `ResponseHeadersRead`, JSON behavior, header
authentication, or transport-result behavior.

Do not add retry/resilience handlers or live connectivity validation.

## 9. Base Address

Use the exact provider base URI already established by WP02/WP07
authority.

Do not duplicate `/time_series` in configuration if WP07 owns the
relative path. Do not let Worker construct provider URLs.

If `TwelveDataClient` expects `HttpClient.BaseAddress`, configure it
through DI. Otherwise preserve its accepted design. Do not create a
second source of truth.

## 10. API-Key Configuration

The Twelve Data API key is a runtime secret.

``` text
hardcoded real credential = forbidden
committed secret = forbidden
API key in query string = forbidden
credential logging = forbidden
credential in Domain/Application = forbidden
```

Use the configuration mechanism authorized by the execution plan/file
manifest and repository conventions. Do not invent aliases if authority
names the key.

The runtime must be able to receive the key externally without
repository secret material.

## 11. Configuration Binding

Use the minimum configuration model authorized by the manifest.

If an options type is authorized, bind provider configuration and
validate only mandatory structural settings. If no options type is
authorized, do not invent one merely for abstraction.

Do not create multi-provider selection or a general market-data
configuration framework.

## 12. Missing Configuration Behavior

Missing mandatory provider configuration must fail deterministically and
safely.

Do not fall back to a fake key, deterministic source, disabled provider,
or secret logging.

Prefer startup/composition failure for a missing mandatory secret when
compatible with authority and repository conventions.

Do not change Worker execution semantics merely to demonstrate the
failure.

## 13. Authentication Preservation

WP07 established header-based authentication. WP10 must supply the
configured key without changing that mechanism.

Prove:

``` text
API key remains header-based
API key in query = 0
hardcoded key = 0
```

Never place the key in URL, query, console output, exception text, logs,
snapshots, or the report.

## 14. Service Lifetimes

Choose lifetimes from actual service semantics and existing conventions.

Requirements:

``` text
IResearchUseCase lifetime preserved
IObservationSource registration unambiguous
TwelveDataObservationSource lifetime compatible with dependencies
TwelveDataClient lifetime compatible with HttpClient composition
no captive dependency
no unnecessary mutable singleton
```

Do not change lifetimes speculatively. Report the final lifetime graph.

## 15. Configuration Leakage

After WP10 prove:

``` text
Domain provider configuration references = 0
Application Twelve Data references = 0
Application configuration-key references = 0
Application API-key references = 0
```

Infrastructure may own provider configuration. Worker may only
participate as composition root where explicitly authorized.

## 16. Worker Boundary

WP11 owns **Worker Market Data Execution**.

WP10 may make only composition-root/configuration changes explicitly
authorized by the manifest.

Do not change Worker research request, target, requested count, output,
execution lifecycle, or provider invocation behavior.

If `Program.cs` must pass `IConfiguration` to Infrastructure
registration and the manifest authorizes it, that is WP10 scope.
Anything beyond composition is WP11.

## 17. Deterministic Source Disposition

Preserve the deterministic source unless deletion is explicitly
authorized.

Report:

``` text
deterministic source file retained? yes/no
deterministic source registered at runtime? yes/no
Twelve Data source registered at runtime? yes/no
IObservationSource runtime registration count = N
```

Expected runtime count: 1.

## 18. Predecessor Behavior Protection

Do not modify accepted behavior of:

``` text
TwelveDataClient
TwelveDataTransportResult
TwelveDataTimeSeriesNormalizer
TwelveDataNormalizationResult
TwelveDataObservationSource
ObservationSourceFailure
ResearchFailure
ObservationSourceResult
ResearchUseCase
```

Do not opportunistically refactor.

## 19. Domain / Package / Project Protection

Maintain:

``` text
WP10 Domain delta = zero
new packages = 0
new project references = 0
```

If required DI/configuration APIs are unavailable without an
unauthorized package/reference change, return `BLOCKED`.

## 20. Authorized Files

`RELEASE_1.0_FILE_MANIFEST.md` is exact file authority.

Create/modify only WP10-authorized files. Likely categories may include
Infrastructure registration, provider configuration/options, and Worker
composition/configuration files only where explicitly authorized. This
list does not itself grant authority.

If implementation requires a file outside the manifest, stop.

## 21. Configuration Artifacts

Any committed configuration file must contain no real secret.

Do not create or commit a real `.env`, personal secret file, or
secret-bearing `appsettings` value. Do not modify `.gitignore` unless
explicitly authorized.

Use normal .NET configuration precedence rather than a custom
environment-variable parser.

## 22. Startup Validation

If configuration validation is authorized, validate only locally
knowable structure, such as a mandatory non-empty API key or absolute
base URI if configurable.

Do not make network calls, consume quota, or attempt credential
verification during startup.

## 23. Testability / Temporary Probe

WP13 owns comprehensive Infrastructure/provider tests.

Do not broaden visibility solely for WP10.

A temporary offline composition probe is allowed. It may prove:

``` text
service collection builds
intended services resolve with safe placeholder configuration
IObservationSource resolves to TwelveDataObservationSource
resolution emits no HTTP request
missing required configuration fails as authorized
runtime registration count/lifetimes are correct
```

Use no live provider and no real credential. Remove the probe before
completion.

Do not add `InternalsVisibleTo` without separate authority.

## 24. Required Composition Evidence

The report must provide the actual runtime graph:

  Service                  Implementation/source           Lifetime
  ------------------------ ------------------------------- --------------------
  `IResearchUseCase`       existing `ResearchUseCase`      actual
  `IObservationSource`     `TwelveDataObservationSource`   actual
  `TwelveDataClient`       accepted WP07 client            actual composition
  `HttpClient`             built-in HTTP composition       actual
  provider configuration   actual source/binding           n/a

Also state whether the deterministic source remains registered.

## 25. Required Configuration Evidence

Report only non-secret configuration metadata:

``` text
section/key names
mandatory values
binding location
where API key is applied
base-address ownership
how external configuration supplies the key
missing-config behavior
```

Never report a credential.

## 26. Offline Composition Proof

WP10 validation must not require Twelve Data connectivity.

Prove:

``` text
service collection builds
required services resolve under safe placeholder configuration
service resolution emits provider request = NO
missing mandatory configuration follows authorized behavior
```

Do not call `/time_series`.

## 27. Security Scan

Perform targeted scans for API-key query use, credential logging,
hardcoded secret-like values, and accidental committed credentials.

Prove:

``` text
real credential introduced by WP10 = 0
API key query use = 0
credential output/logging = 0
```

## 28. Leakage Inspection

Prove:

``` text
Domain Twelve Data references = 0
Domain configuration references introduced by WP10 = 0
Application Twelve Data references = 0
Application provider configuration references = 0
Application API-key references = 0
Worker provider behavior changes = 0
ResearchUseCase provider branches = 0
```

## 29. Architecture Protection

Production graph remains:

``` text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Prove cycles = 0, new project references = 0, Application →
Infrastructure = 0, Domain → Infrastructure = 0, and Architecture.Tests
remain green.

## 30. Regression Protection

Prove:

``` text
WP03 zero Domain delta preserved
WP04 failure vocabulary unchanged
WP05 propagation unchanged
WP06 transport model unchanged
WP07 endpoint/authentication/adjust=splits unchanged
WP08 close/timestamp/order/duplicate semantics unchanged
WP09 mapping matrix/cancellation unchanged
deterministic source not deleted without authority
Worker execution behavior unchanged
```

## 31. Build and Validation

At minimum run:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
git diff --cached --check
```

Record actual counts. Remove any temporary probe.

Canonical verification must remain offline.

Established `NU1900` feed-connectivity warnings are non-blocking only if
all mandatory validation passes.

## 32. Git / GitHub Protection

Do not stage, commit, branch, push, create/merge a PR, stash, or discard
cumulative work.

Do not mutate issue #95, milestone #41, Project fields/status, labels,
or Release 1.1 planning.

WP10 ends as a validated local cumulative candidate.

## 33. Working-Tree Classification

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
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

List exact WP10 files. Staged files must be zero. Investigate unexpected
changes.

## 34. Acceptance / Exit Criteria

WP10 is complete only if:

``` text
WP09 predecessor gate = PASS
existing Application registration preserved
IObservationSource runtime implementation = TwelveDataObservationSource
IObservationSource runtime registration count = 1
TwelveDataClient composed through accepted HttpClient boundary
provider base-address ownership explicit
API-key configuration contract explicit
real credential committed = 0
API key in query = 0
credential logging = 0
missing mandatory configuration behavior deterministic
service resolution under safe placeholder configuration = PASS
service resolution emits provider request = NO
live provider required = NO
deterministic source not removed without authority
WP07 behavior changed = NO
WP08 behavior changed = NO
WP09 behavior changed = NO
Application contracts changed = NO
Domain changes = 0
Worker execution behavior changed = NO
new packages = 0 unless explicitly authorized
new project references = 0
architecture graph preserved
build = PASS with zero errors
required tests = PASS
architecture tests = PASS
canonical verification = PASS
git diff --check = PASS
git diff --cached --check = PASS
unexpected mutations = 0
WP11 started = NO
```

Return `BLOCKED` if the manifest does not authorize required
configuration artifacts, HttpClient composition requires an unauthorized
dependency, accepted WP09 semantics must change, or Worker execution
behavior is required.

## 35. Required Execution Report

Return:

``` text
# Release 1.0 WP10 — Dependency Registration & Configuration Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP09 Predecessor Gate
## 5. Existing Composition Baseline
## 6. Registration Design
## 7. Application Registration Preservation
## 8. IObservationSource Runtime Registration
## 9. TwelveDataClient / HttpClient Composition
## 10. Provider Configuration Contract
## 11. API-Key Configuration
## 12. Missing-Configuration Behavior
## 13. Service Lifetimes
## 14. Deterministic Source Disposition
## 15. Worker Scope Boundary
## 16. Types Added or Modified
## 17. Files Changed
## 18. Composition Evidence Matrix
## 19. Offline Resolution Evidence
## 20. Security Evidence
## 21. Provider / Configuration Leakage Scan
## 22. Application / Domain Preservation
## 23. WP07–WP09 Regression Evidence
## 24. Dependency / Architecture Evidence
## 25. Build Evidence
## 26. Test / Temporary-Probe Evidence
## 27. Canonical Verification
## 28. Diff / Formatting Validation
## 29. Working-Tree Classification
## 30. Scope Protection
## 31. Findings / Observations
## 32. Exit-Criteria Assessment
## 33. Final Repository State
## 34. Final Decision
## 35. Next Authorized Action
```

Include exact registration method/file, lifetimes, configuration
section/key names, API-key application point, base-address source,
missing-config behavior, deterministic-source status, temporary-probe
evidence, test counts, and validation results.

Clearly distinguish:

``` text
WP07 = HTTP behavior
WP08 = normalization
WP09 = validation/failure mapping
WP10 = composition/configuration
WP11 = executable Worker market-data flow — NOT STARTED
```

## 36. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP10 DEPENDENCY REGISTRATION AND CONFIGURATION COMPLETE
RELEASE 1.0 WP10 DEPENDENCY REGISTRATION AND CONFIGURATION COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP10 DEPENDENCY REGISTRATION AND CONFIGURATION BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when every mandatory exit
criterion passes.

## 37. Next Authorized Action

If WP10 completes successfully:

``` text
WP11 — Worker Market Data Execution
GitHub issue #96
```

Do not execute WP11. Stop after the WP10 report.

## Execution Instruction

Read all Release 1.0 authorities and cumulative WP02--WP09
implementation, prove the WP09 predecessor gate, implement only the
minimum authorized dependency registration and provider configuration
needed to compose
`IObservationSource → TwelveDataObservationSource → TwelveDataClient → HttpClient`,
preserve the existing Application registration and all WP07--WP09
behavior, keep credentials external and header-based, establish
deterministic missing-configuration behavior without live provider
access, retain the deterministic source unless deletion is explicitly
authorized, do not change Worker execution behavior, do not add
unauthorized packages/project references, run offline
composition/security/leakage/architecture/test/verification/diff checks,
classify the complete working tree, return the full WP10 execution
report, and stop before WP11.

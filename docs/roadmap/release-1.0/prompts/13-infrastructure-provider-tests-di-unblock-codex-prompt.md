# Release 1.0 WP13 --- Infrastructure & Provider Tests DI Unblock --- Codex Prompt

## Role

Act as the **WP13 Infrastructure & Provider Tests DI Unblock Executor**
for Release 1.0 of `AIQuantTradingResearch`.

WP13 is blocked solely by:

``` text
B13-01 — Infrastructure.Tests cannot compile Microsoft's concrete dependency-injection container APIs required to prove real service-provider build and resolution.
```

The blocked WP13 report established that all 101 permanent tests pass
and canonical verification succeeds, but
`AIQuantTradingResearch.Infrastructure.Tests` currently references only
`Microsoft.Extensions.DependencyInjection.Abstractions`, so it cannot
compile `BuildServiceProvider(...)` or `ServiceProviderOptions`.

This prompt authorizes only the minimum test-project dependency and
permanent DI-test changes required to close B13-01. Do not modify
production code. Do not begin WP14.

## 1. Authorities

Read completely:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
docs/roadmap/release-1.0/prompts/10-dependency-registration-configuration-codex-prompt.md
docs/roadmap/release-1.0/prompts/11-worker-market-data-execution-codex-prompt.md
docs/roadmap/release-1.0/prompts/12-domain-application-tests-codex-prompt.md
docs/roadmap/release-1.0/prompts/13-infrastructure-provider-tests-codex-prompt.md
docs/roadmap/release-1.0/prompts/13-infrastructure-provider-tests-di-unblock-codex-prompt.md
```

Read the blocked WP13 execution report from the current context.

Inspect:

``` text
Directory.Packages.props
Directory.Build.props
global.json
tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
tests/AIQuantTradingResearch.Infrastructure.Tests/DependencyInjectionTests.cs
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/TwelveDataConfiguration.cs
```

Inspect repository package references for
`Microsoft.Extensions.DependencyInjection` and
`Microsoft.Extensions.DependencyInjection.Abstractions`.

Authority precedence:

1.  This DI-unblock prompt
2.  Release 1.0 execution plan
3.  Release 1.0 file manifest
4.  Accepted WP10--WP13 implementation/results
5.  Existing central package-management conventions

## 2. Reproduce B13-01

Before mutation prove:

``` text
Infrastructure.Tests can compile DI abstractions
Infrastructure.Tests cannot compile BuildServiceProvider(...)
Infrastructure.Tests cannot compile ServiceProviderOptions
missing concrete API source = Microsoft.Extensions.DependencyInjection
existing WP13 suite otherwise passes
```

If the blocker no longer reproduces, stop and report repository drift.

## 3. Narrow Human Authorization

This prompt explicitly authorizes adding:

``` text
Microsoft.Extensions.DependencyInjection
```

to `AIQuantTradingResearch.Infrastructure.Tests` only.

This is a test-support dependency. Do not add it to Domain, Application,
Infrastructure, Worker, Domain.Tests, Application.Tests, or
Architecture.Tests.

Do not add a Worker project reference. Do not add a `FrameworkReference`
as an alternative. If this exact package cannot be integrated
consistently with repository package governance, stop as `BLOCKED`.

## 4. Central Package Governance

Inspect `Directory.Packages.props`.

If `Microsoft.Extensions.DependencyInjection` is already centrally
governed, reuse its existing central version and add only the
Infrastructure.Tests `PackageReference`.

If it is absent and the repository uses Central Package Management, add
one `PackageVersion` entry using the repository-consistent
Microsoft.Extensions dependency-family version, then add a versionless
`PackageReference` to Infrastructure.Tests.

Do not invent a version arbitrarily. Do not put a `Version` attribute in
the `.csproj` when Central Package Management governs versions.

If a repository-consistent version cannot be determined safely, return
`BLOCKED`.

## 5. Authorized Files

Only these files may change:

``` text
Directory.Packages.props
tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
tests/AIQuantTradingResearch.Infrastructure.Tests/DependencyInjectionTests.cs
```

Modify `Directory.Packages.props` only if central version governance is
required. No production file may change.

## 6. Prohibited Workarounds

Do not solve B13-01 by:

``` text
adding Worker project reference
adding FrameworkReference
making internal types public
adding InternalsVisibleTo
using reflection
using third-party DI
writing a fake service provider
adding production helpers only for tests
```

The goal is to prove the actual Microsoft DI container.

## 7. Concrete Container Proof

Add the minimum permanent test coverage needed to prove the WP10 graph
through the real Microsoft container.

Using actual repository signatures, perform the equivalent of:

``` text
var services = new ServiceCollection();
services.AddInfrastructure(new TwelveDataConfiguration("wp13-placeholder-key"));

using var provider = services.BuildServiceProvider(
    new ServiceProviderOptions
    {
        ValidateOnBuild = true,
        ValidateScopes = true
    });
```

Prove:

``` text
BuildServiceProvider compiles
ServiceProviderOptions compiles
ValidateOnBuild = true
ValidateScopes = true
container builds successfully
IObservationSource resolves
resolved implementation = TwelveDataObservationSource
IObservationSource singleton behavior is preserved
HttpClient resolves if registered
HttpClient.BaseAddress = https://api.twelvedata.com/
provider HTTP calls during build/resolution = 0
DeterministicObservationSource is not selected
```

Do not invoke the observation source. This unblock proves composition,
not live acquisition.

## 8. Registration Count

Retain existing descriptor-level assertions and supplement them with
runtime proof:

``` text
IObservationSource descriptors = 1
resolved type = TwelveDataObservationSource
two resolutions return the same instance if singleton
```

## 9. Missing Configuration Regression

Preserve the existing permanent test:

``` text
AddInfrastructure() → InvalidOperationException
```

and prove the error identifies `TwelveData:ApiKey`.

## 10. Security

Use only an obvious placeholder key. Prove:

``` text
real credential = 0
credential committed = 0
credential in URI/query = 0
credential logging/output = 0
live provider access = 0
```

## 11. Production Protection

Required:

``` text
Domain production changes = 0
Application production changes = 0
Infrastructure production changes = 0
Worker production changes = 0
```

If concrete container resolution reveals a production DI defect, stop
and report it. Do not fix production behavior under this unblock.

## 12. Dependency Scope

Expected dependency delta:

``` text
Microsoft.Extensions.DependencyInjection → Infrastructure.Tests only
```

Expected:

``` text
new project references = 0
production package changes = 0
framework references = 0
```

If broader changes are required, return `BLOCKED`.

## 13. Restore and Validation

Run:

``` text
dotnet restore AIQuantTradingResearch.slnx --nologo
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo

dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo

powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1

git diff --check
git diff --cached --check
```

Record final counts. Infrastructure.Tests may increase above 65. Known
`NU1900` feed-connectivity warnings remain non-blocking only if all
mandatory validation passes.

## 14. DI Acceptance Matrix

Report:

  Requirement                                       Result
  ------------------------------------------------- -----------
  `ServiceCollection` instantiated                  PASS/FAIL
  configured `AddInfrastructure` invoked            PASS/FAIL
  `BuildServiceProvider` compiled                   PASS/FAIL
  `ServiceProviderOptions` compiled                 PASS/FAIL
  `ValidateOnBuild` used                            PASS/FAIL
  `ValidateScopes` used                             PASS/FAIL
  provider built successfully                       PASS/FAIL
  `IObservationSource` resolved                     PASS/FAIL
  implementation = `TwelveDataObservationSource`    PASS/FAIL
  singleton behavior proven                         PASS/FAIL
  `HttpClient.BaseAddress` correct                  PASS/FAIL
  provider HTTP calls during build/resolution = 0   PASS/FAIL
  deterministic source not selected                 PASS/FAIL

Every row must pass.

## 15. Architecture / Diff Protection

Production graph remains:

``` text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Prove:

``` text
new production project references = 0
new test project references = 0
cycles = 0
Architecture.Tests = PASS
```

Expected tracked changes are limited to the authorized three files.

Run both diff checks. Nothing should be staged.

## 16. Working-Tree Classification

Preserve all cumulative Release 1.0 work and classify:

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
WP12 AUTHORIZED
WP13 AUTHORIZED
WP13 DI UNBLOCK AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

Expected:

``` text
staged = 0
unexpected = 0
temporary artifacts = 0
```

## 17. Git / GitHub Protection

Do not stage, commit, branch, push, create/merge a PR, stash, discard
cumulative work, close issue #98, modify milestone #41, change Project
fields/status, or create Release 1.1 planning.

Do not start WP14.

## 18. Acceptance Criteria

The unblock completes only if:

``` text
B13-01 reproduced
Microsoft.Extensions.DependencyInjection added only for Infrastructure.Tests
central package governance preserved
repository-consistent version proven
Worker project reference added = NO
FrameworkReference added = NO
production package changes = 0
production code changes = 0

BuildServiceProvider compiles
ServiceProviderOptions compiles
concrete provider validation = PASS
IObservationSource resolves
runtime implementation = TwelveDataObservationSource
singleton behavior = PASS
HttpClient base address = https://api.twelvedata.com/
provider requests during build/resolution = 0
deterministic source selected = NO

missing-config regression = PASS
all WP13 tests = PASS
all four permanent suites = PASS
eng/verify.ps1 = PASS
build errors = 0
git diff --check = PASS
git diff --cached --check = PASS
staged files = 0
temporary artifacts = 0
unexpected mutations = 0
WP14 started = NO
```

If concrete DI resolution exposes a production defect, return `BLOCKED`.

## 19. Required Execution Report

Return:

``` text
# Release 1.0 WP13 DI Unblock Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. B13-01 Reproduction
## 5. Package-Governance Inspection
## 6. Authorized Dependency Decision
## 7. Package / Project Changes
## 8. Concrete DI Test Design
## 9. ServiceProvider Build Evidence
## 10. IObservationSource Resolution Evidence
## 11. Lifetime Evidence
## 12. HttpClient / Base-Address Evidence
## 13. Provider-Call Absence Evidence
## 14. Deterministic Source Exclusion
## 15. Missing-Configuration Regression
## 16. Files Changed
## 17. Production-Code Preservation
## 18. Architecture / Dependency Evidence
## 19. Restore Evidence
## 20. Build Evidence
## 21. Test Count Delta
## 22. Test Evidence
## 23. Canonical Verification
## 24. Security Evidence
## 25. Diff / Formatting Validation
## 26. Working-Tree Classification
## 27. Scope Protection
## 28. Findings / Observations
## 29. Acceptance Assessment
## 30. Final Repository State
## 31. Final Decision
## 32. Next Authorized Action
```

## 20. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP13 DI UNBLOCK COMPLETE
RELEASE 1.0 WP13 DI UNBLOCK COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP13 DI UNBLOCK BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only if every mandatory criterion
passes.

## 21. Next Authorized Action

If this unblock completes successfully, rerun the existing authoritative
WP13 prompt:

``` text
docs/roadmap/release-1.0/prompts/13-infrastructure-provider-tests-codex-prompt.md
```

Do not create a replacement WP13 prompt.

The resumed WP13 run must recognize B13-01 as resolved, retain the
permanent concrete-container proof, rerun the complete provider suite
and all four permanent suites, rerun `eng/verify.ps1`, and reassess
every WP13 exit criterion.

Do not proceed to WP14 until the resumed WP13 run completes
successfully.

## Execution Instruction

Read the blocked WP13 report and repository package/test configuration;
reproduce B13-01; inspect Central Package Management and existing
Microsoft.Extensions dependency versions; add only repository-consistent
`Microsoft.Extensions.DependencyInjection` test support to
`AIQuantTradingResearch.Infrastructure.Tests`; add only the minimum
permanent concrete Microsoft container proof in
`DependencyInjectionTests`; build a real validated `ServiceProvider`,
resolve `IObservationSource`, prove `TwelveDataObservationSource`,
singleton behavior, correct `HttpClient` base address, zero provider
calls, and deterministic-source exclusion; preserve all production code
and existing WP13 tests; run restore, build, all permanent suites,
canonical verification, security/architecture/diff/working-tree checks;
return the complete DI-unblock report; and stop so the existing WP13
authority can be rerun separately.

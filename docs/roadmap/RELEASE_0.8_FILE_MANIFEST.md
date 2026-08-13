# Release 0.8 — Solution Skeleton File Manifest

## Purpose

This document defines the exact file and directory manifest for **Phase 2 — Release 0.8: Solution Skeleton** of AIQuantTradingResearch.

It identifies files to create, existing files to inspect, files that may require modification, forbidden additions, project references, solution membership, and validation expectations.

The manifest is designed for both human engineers and repository-aware coding agents.

---

## Manifest Principles

- Create the smallest physical structure required by approved architecture.
- Preserve existing repository assets.
- Do not recreate authoritative files blindly.
- Do not add speculative directories or placeholder frameworks.
- Do not implement later-release business capabilities.
- Keep generated output reviewable.
- Keep dependencies explicit.
- Prefer repository conventions over agent preference.

---

## Target Repository Structure

```text
AIQuantTradingResearch/
│
├── AIQuantTradingResearch.slnx
├── global.json
├── Directory.Build.props
├── Directory.Packages.props
├── .editorconfig
├── .gitattributes
├── .gitignore
├── README.md
├── ENGINEERING.md
├── ARCHITECTURE.md
│
├── src/
│   ├── AIQuantTradingResearch.Domain/
│   │   └── AIQuantTradingResearch.Domain.csproj
│   │
│   ├── AIQuantTradingResearch.Application/
│   │   ├── AIQuantTradingResearch.Application.csproj
│   │   └── DependencyInjection.cs
│   │
│   ├── AIQuantTradingResearch.Infrastructure/
│   │   ├── AIQuantTradingResearch.Infrastructure.csproj
│   │   └── DependencyInjection.cs
│   │
│   └── AIQuantTradingResearch.Worker/
│       ├── AIQuantTradingResearch.Worker.csproj
│       ├── Program.cs
│       ├── appsettings.json
│       └── appsettings.Development.json
│
├── tests/
│   ├── AIQuantTradingResearch.Domain.Tests/
│   │   └── AIQuantTradingResearch.Domain.Tests.csproj
│   │
│   ├── AIQuantTradingResearch.Application.Tests/
│   │   └── AIQuantTradingResearch.Application.Tests.csproj
│   │
│   ├── AIQuantTradingResearch.Infrastructure.Tests/
│   │   └── AIQuantTradingResearch.Infrastructure.Tests.csproj
│   │
│   └── AIQuantTradingResearch.Architecture.Tests/
│       ├── AIQuantTradingResearch.Architecture.Tests.csproj
│       └── DependencyRulesTests.cs
│
├── eng/
│   ├── build.ps1
│   ├── build.sh
│   ├── clean.ps1
│   ├── format.ps1
│   ├── test.ps1
│   └── verify.ps1
│
├── docs/
│   └── existing documentation
│
└── .github/
    └── existing governance assets
```

The exact solution extension may be `.sln` instead of `.slnx` if repository or SDK compatibility requires it. Any deviation must be documented.

---

## Files to Create

### Root

```text
AIQuantTradingResearch.slnx
```

### Domain Project

```text
src/AIQuantTradingResearch.Domain/
└── AIQuantTradingResearch.Domain.csproj
```

No placeholder domain class is required. Remove template files such as `Class1.cs` unless repository conventions explicitly require otherwise.

### Application Project

```text
src/AIQuantTradingResearch.Application/
├── AIQuantTradingResearch.Application.csproj
└── DependencyInjection.cs
```

### Infrastructure Project

```text
src/AIQuantTradingResearch.Infrastructure/
├── AIQuantTradingResearch.Infrastructure.csproj
└── DependencyInjection.cs
```

### Worker Project

```text
src/AIQuantTradingResearch.Worker/
├── AIQuantTradingResearch.Worker.csproj
├── Program.cs
├── appsettings.json
└── appsettings.Development.json
```

Do not create artificial worker/service classes solely to make the host non-empty.

### Test Projects

```text
tests/AIQuantTradingResearch.Domain.Tests/
└── AIQuantTradingResearch.Domain.Tests.csproj

tests/AIQuantTradingResearch.Application.Tests/
└── AIQuantTradingResearch.Application.Tests.csproj

tests/AIQuantTradingResearch.Infrastructure.Tests/
└── AIQuantTradingResearch.Infrastructure.Tests.csproj

tests/AIQuantTradingResearch.Architecture.Tests/
├── AIQuantTradingResearch.Architecture.Tests.csproj
└── DependencyRulesTests.cs
```

---

## Existing Files to Inspect

```text
global.json
Directory.Build.props
Directory.Packages.props
.editorconfig
.gitattributes
.gitignore
README.md
ENGINEERING.md
ARCHITECTURE.md
```

Also inspect relevant assets under:

```text
docs/architecture/
docs/architecture/design/
docs/architecture/implementation/
docs/architecture/resilience/
```

The actual repository structure remains authoritative.

---

## Existing Files That May Require Modification

### global.json

Modify only when an approved repository decision requires it. Do not change it merely because the coding agent has a different SDK installed.

### Directory.Build.props

Modify only when required to support common project settings, nullable configuration, analyzers, warnings, or shared build behavior.

### Directory.Packages.props

Modify when new package versions are required. Package versions should be centrally governed.

### README.md

Update only if needed to expose the new solution, build/test commands, or skeleton status.

### ENGINEERING.md

Update if engineering workflow commands become concrete rather than conceptual.

### ARCHITECTURE.md

Update only if physical structure descriptions become inaccurate. Do not redesign architecture here.

---

## Existing Engineering Scripts to Validate

```text
eng/build.ps1
eng/build.sh
eng/clean.ps1
eng/format.ps1
eng/test.ps1
eng/verify.ps1
```

Modify them only when necessary to support the new solution. Avoid Release-0.8-specific duplicates.

---

## Project Type Manifest

### AIQuantTradingResearch.Domain

Type: Class Library.

Release 0.8 content should be the project file only unless validation needs a minimal assembly marker.

### AIQuantTradingResearch.Application

Type: Class Library.

Release 0.8 content:

```text
.csproj
DependencyInjection.cs
```

### AIQuantTradingResearch.Infrastructure

Type: Class Library.

Release 0.8 content:

```text
.csproj
DependencyInjection.cs
```

### AIQuantTradingResearch.Worker

Type: Worker / Generic Host executable.

Release 0.8 content:

```text
.csproj
Program.cs
appsettings.json
appsettings.Development.json
```

---

## Test Project References

### Domain.Tests

```text
AIQuantTradingResearch.Domain
```

### Application.Tests

```text
AIQuantTradingResearch.Application
AIQuantTradingResearch.Domain (only if tests require it)
```

### Infrastructure.Tests

```text
AIQuantTradingResearch.Infrastructure
```

Additional references should be introduced only when actual tests require them.

### Architecture.Tests

May reference or inspect all four production projects because its responsibility is architectural verification.

---

## Production Project References

### Application.csproj

```text
ProjectReference → AIQuantTradingResearch.Domain
```

### Infrastructure.csproj

```text
ProjectReference → AIQuantTradingResearch.Application
ProjectReference → AIQuantTradingResearch.Domain (only if required)
```

### Worker.csproj

```text
ProjectReference → AIQuantTradingResearch.Application
ProjectReference → AIQuantTradingResearch.Infrastructure
```

Worker should normally not require a direct Domain reference.

---

## Forbidden Production References

```text
Domain → Application
Domain → Infrastructure
Domain → Worker
Application → Infrastructure
Application → Worker
Infrastructure → Worker
```

Architecture tests must protect these rules.

---

## Package Manifest

Release 0.8 should keep dependencies minimal.

### Worker

Prefer normal .NET host/shared-framework capabilities.

### Test Projects

Use the repository-approved test framework only.

### Architecture Tests

Before adding an architecture testing package:

1. Inspect existing dependencies.
2. Determine whether robust checks can be implemented without a package.
3. If a package materially improves validation, justify it.
4. Centralize its version in `Directory.Packages.props`.
5. Do not hardcode versions in project files.

---

## DependencyInjection.cs Contract

### Application

Conceptual API:

```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        return services;
    }
}
```

Exact namespace and style should follow repository conventions.

Do not add fake services merely to populate registration.

### Infrastructure

Conceptual API:

```csharp
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}
```

Configuration may be omitted if not needed yet.

---

## Program.cs Contract

The Worker should conceptually perform:

```text
Create Builder
  ↓
AddApplication
  ↓
AddInfrastructure
  ↓
Build Host
  ↓
Run
```

Example shape:

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var host = builder.Build();

await host.RunAsync();
```

Use repository conventions for namespaces and top-level statements.

---

## Configuration File Contract

### appsettings.json

Keep minimal. Do not add provider credentials, connection strings, market data configuration, trading configuration, or cloud configuration.

A minimal valid JSON object is acceptable.

### appsettings.Development.json

May also remain minimal. Do not commit local secrets.

---

## Architecture Test File Contract

`DependencyRulesTests.cs` should validate actual assembly or project relationships.

Required conceptual tests:

```text
Domain_Should_Not_Depend_On_Application
Domain_Should_Not_Depend_On_Infrastructure
Domain_Should_Not_Depend_On_Worker
Application_Should_Not_Depend_On_Infrastructure
Application_Should_Not_Depend_On_Worker
Infrastructure_Should_Not_Depend_On_Worker
```

Naming may follow repository conventions.

---

## Files Not to Create

Release 0.8 must not introduce feature-specific files for:

```text
MarketData
Storage
Trading
Backtesting
Strategies
Exchanges
Brokers
MachineLearning
AI inference
Plugins
```

Also do not create new deployment or data infrastructure such as:

```text
database/
migrations/
docker/
k8s/
terraform/
deploy/
pipelines/
```

unless those directories already exist from previous governance work.

---

## Placeholder Policy

Remove template-generated examples such as:

```text
Class1.cs
SampleService.cs
WeatherForecast.cs
Todo.cs
```

The skeleton should be minimal, not sample-driven.

---

## Namespace Policy

Base namespaces should follow project names:

```text
AIQuantTradingResearch.Domain
AIQuantTradingResearch.Application
AIQuantTradingResearch.Infrastructure
AIQuantTradingResearch.Worker
```

Do not create deep empty namespace/folder structures in advance.

---

## Folder Policy

Do not pre-create speculative folders such as:

```text
Commands/
Queries/
Repositories/
Services/
Entities/
ValueObjects/
Events/
Providers/
```

These should emerge when actual responsibilities appear.

---

## Solution Membership

The root solution should contain exactly the Release 0.8 projects plus any pre-existing approved projects:

```text
AIQuantTradingResearch.Domain
AIQuantTradingResearch.Application
AIQuantTradingResearch.Infrastructure
AIQuantTradingResearch.Worker
AIQuantTradingResearch.Domain.Tests
AIQuantTradingResearch.Application.Tests
AIQuantTradingResearch.Infrastructure.Tests
AIQuantTradingResearch.Architecture.Tests
```

No AI Engineering Toolkit project should be added to the application solution.

---

## Build Contract

From repository root:

```text
dotnet restore
dotnet build
```

must succeed without machine-local configuration.

---

## Test Contract

From repository root:

```text
dotnet test
```

must succeed, including architecture tests.

---

## Format Contract

Where repository standards use `dotnet format`:

```text
dotnet format --verify-no-changes
```

must succeed after implementation.

If repository scripts wrap formatting, those scripts remain authoritative.

---

## Verification Contract

The primary repository quality gate should succeed:

```text
eng/verify.ps1
```

The Unix equivalent should also be validated when officially supported.

---

## File Change Report Contract

The coding agent should provide a final manifest:

```text
Created:
- ...

Modified:
- ...

Deleted:
- ...

Dependencies added:
- ...

Validation:
- restore
- build
- test
- format
- architecture tests
- verify
```

Unexpected changes must be called out explicitly.

---

## Git Contract

Use a dedicated branch.

Recommended:

```text
feature/release-0.8-solution-skeleton
```

Repository governance may define a different naming convention.

---

## Pull Request Contract

The pull request should include:

- Release objective.
- Link to Release 0.8 issue.
- Summary of created projects.
- Dependency graph.
- Validation evidence.
- Architecture test result.
- Known observations.
- Explicit statement that business capabilities remain out of scope.

---

## Acceptance Checklist

### Structure

- [ ] Root solution exists.
- [ ] Domain project exists.
- [ ] Application project exists.
- [ ] Infrastructure project exists.
- [ ] Worker project exists.
- [ ] Four test projects exist.

### Architecture

- [ ] Application references Domain.
- [ ] Infrastructure references allowed projects only.
- [ ] Worker acts as composition root.
- [ ] Forbidden references do not exist.
- [ ] Architecture tests enforce dependency rules.

### Configuration

- [ ] Existing `global.json` remains valid.
- [ ] Existing `Directory.Build.props` remains valid.
- [ ] Package versions are centralized.
- [ ] No secrets are committed.

### Host

- [ ] Worker builds.
- [ ] Worker uses Application registration.
- [ ] Worker uses Infrastructure registration.
- [ ] No feature-specific runtime behavior is introduced.

### Testing

- [ ] All test projects build.
- [ ] Architecture tests pass.
- [ ] `dotnet test` passes.

### Engineering

- [ ] `dotnet restore` passes.
- [ ] `dotnet build` passes.
- [ ] Formatting validation passes.
- [ ] Repository verify script passes.

### Scope

- [ ] No market data implementation.
- [ ] No storage implementation.
- [ ] No trading implementation.
- [ ] No AI/ML implementation.
- [ ] No plugin framework implementation.
- [ ] No production infrastructure implementation.

### Documentation

- [ ] Physical project structure is documented where required.
- [ ] Commands are current.
- [ ] No architecture document was unnecessarily rewritten.

---

## Final Expected Delta

A successful Release 0.8 should primarily add:

```text
1 solution file
4 production project files
2 dependency registration files
1 Program.cs
2 minimal appsettings files
4 test project files
1 architecture test file
```

plus only the smallest required updates to existing configuration, engineering scripts, and documentation where repository evidence shows they are necessary.

---

# Conclusion

The Release 0.8 file manifest defines the smallest physical .NET structure needed to convert AIQuantTradingResearch from an architecture-first repository into an executable platform foundation.

The manifest intentionally avoids speculative folders, sample code, business features, and infrastructure implementations.

The governing model is:

```text
Existing Engineering Repository
  ↓
Minimal Solution Structure
  ↓
Explicit Project Graph
  ↓
Minimal Composition Root
  ↓
Executable Architecture Tests
  ↓
Repository Validation
  ↓
Ready for Feature Implementation
```

The central principle is:

> **The Solution Skeleton should create exactly enough physical structure to make the approved architecture executable and enforceable, while leaving later business capabilities to emerge in the releases where they actually belong.**

# Codex Execution Prompt — Release 0.8 / 07 Dependency Registration

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 07 — Dependency Registration |
| Issue Type | Feature |
| Execution Mode | Controlled repository modification |
| Primary Agent | Codex |
| Prerequisite | 06 — Minimal Worker Host accepted as `COMPLETE` |
| Primary Projects | Application, Infrastructure, Worker |
| Expected Outcome | Explicit `AddApplication` and `AddInfrastructure` registration boundaries wired into the Worker with no feature/service implementation |

---

## Purpose

Establish the dependency-registration boundaries for the Release 0.8 Solution Skeleton.

WP06 created a valid minimal Worker host. WP07 now introduces the composition contracts that allow the Worker to delegate registration responsibility to Application and Infrastructure.

The goal is not to register real application or infrastructure services yet.

The goal is to establish these explicit boundaries:

```text
Worker
   ↓
AddApplication(...)
   ↓
AddInfrastructure(...)
   ↓
Build
   ↓
Run
```

Application and Infrastructure should own their registration extension points.

Worker should orchestrate those extension points and remain the composition root.

No business behavior, providers, storage, hosted services, tests, architecture tests, CI, engineering scripts, or later Release 0.8 work may be introduced.

---

## Objective

Create:

```text
src/AIQuantTradingResearch.Application/DependencyInjection.cs

src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
```

and update:

```text
src/AIQuantTradingResearch.Worker/Program.cs
```

so that the Worker invokes:

```text
AddApplication(...)
AddInfrastructure(...)
```

using the minimum valid registration contract.

The final state must satisfy:

- Application registration exists.
- Infrastructure registration exists.
- Worker calls both registrations.
- Application registration does not reference Infrastructure.
- Infrastructure registration may reference Application contracts through the existing WP04 dependency graph.
- No actual application service is registered.
- No actual infrastructure service/provider is registered.
- No hosted/background service is registered.
- Worker still builds and runs.
- Existing project-reference graph remains unchanged.
- Root solution membership remains unchanged.

---

# 1. Authority and Preconditions

Before modifying anything, read completely:

```text
docs/roadmap/release-0.8/prompts/07-dependency-registration-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Read current implementation files:

```text
src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
src/AIQuantTradingResearch.Worker/Program.cs
```

Read dependency/DI guidance:

```text
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
docs/architecture/implementation/NAMING_CONVENTIONS.md
docs/architecture/implementation/CONFIGURATION_MODEL.md
```

If any exact listed file does not exist, do not create or rename documentation. Record the absence and use the available authoritative sources.

Consult relevant Toolkit guidance when materially useful:

```text
AI-Engineering-Toolkit/docs/AI_ASSISTED_ENGINEERING_WORKFLOW.md
AI-Engineering-Toolkit/playbooks/dotnet/01-solution-architecture.md
AI-Engineering-Toolkit/playbooks/dotnet/04-dependency-management.md
AI-Engineering-Toolkit/playbooks/dotnet/05-coding-standards.md
AI-Engineering-Toolkit/playbooks/dotnet/06-error-handling.md
AI-Engineering-Toolkit/playbooks/dotnet/12-project-review.md
```

Repository-specific Release 0.8 guidance takes precedence over generic examples.

---

# 2. Accepted Baseline from WP06

Expected production graph:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Expected package/runtime state:

```text
Worker PackageReference:
  Microsoft.Extensions.Hosting

Worker Program.cs:
  Host.CreateApplicationBuilder(args)
  builder.Build()
  host.RunAsync()
```

Expected build state:

```text
Domain          = PASS
Application     = PASS
Infrastructure  = PASS
Worker          = PASS
```

Expected solution state:

```text
AIQuantTradingResearch.slnx exists
Project count = 0
```

Expected feature state:

```text
No application services
No infrastructure implementations
No hosted service
No feature behavior
No tests
```

Verify actual current state before mutation.

If the baseline materially changed, do not guess.

---

# 3. Scope

## In Scope

You may:

- Inspect repository and Git state.
- Inspect Application, Infrastructure, and Worker project/source files.
- Determine the minimum DI abstractions needed.
- Add the minimum framework/package references required to compile registration extension methods.
- Create `Application/DependencyInjection.cs`.
- Create `Infrastructure/DependencyInjection.cs`.
- Update `Worker/Program.cs` to invoke both registration methods.
- Use `IServiceCollection`.
- Use `IConfiguration` only if repository authority and the approved infrastructure registration contract require it.
- Build all production projects.
- Run bounded Worker smoke validation.
- Revalidate the dependency graph.
- Inspect final Git state.
- Produce an evidence-based report.

## Out of Scope

Do not:

- Register actual application services.
- Register actual infrastructure services.
- Add repositories.
- Add market-data providers.
- Add storage/database providers.
- Add HTTP clients.
- Add resilience policies.
- Add telemetry.
- Add hosted/background services.
- Add logging providers.
- Add options/configuration classes.
- Add feature-specific configuration.
- Add tests or architecture tests.
- Change the WP04 project graph.
- Add projects to the solution.
- Modify root build policy.
- Modify `global.json`.
- Modify `.editorconfig`.
- Modify documentation.
- Modify engineering scripts.
- Modify CI/GitHub workflows.
- Modify Docker assets.
- Stage, commit, push, or open a pull request.
- Begin WP08.

---

# 4. Authorized Change Set

Primary authorized files:

```text
src/AIQuantTradingResearch.Application/DependencyInjection.cs
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
src/AIQuantTradingResearch.Worker/Program.cs
```

Conditionally authorized project files:

```text
src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
```

Conditionally authorized central package file:

```text
Directory.Packages.props
```

These conditional files may be modified only when a framework/package reference is strictly required to compile the DI extension contracts.

Do not modify:

```text
src/AIQuantTradingResearch.Domain/**
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

unless an explicit, evidence-backed dependency requirement makes it unavoidable.

If such a requirement emerges unexpectedly, stop and reassess rather than broadening scope silently.

---

# 5. Registration Contract

## 5.1 Application Registration

Create a registration extension owned by Application.

Conceptual contract:

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

Requirements:

- Namespace follows repository conventions.
- Extension is publicly callable by Worker.
- No Infrastructure dependency.
- No service registration yet.
- Returns the same `IServiceCollection`.
- No side effects.

---

## 5.2 Infrastructure Registration

Create a registration extension owned by Infrastructure.

Conceptual contract:

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

Use `IConfiguration` only if repository authority and the Release 0.8 contract support it.

If configuration is not required yet, prefer the smaller contract:

```csharp
public static IServiceCollection AddInfrastructure(
    this IServiceCollection services)
```

Do not include a parameter merely because future infrastructure may need it.

Requirements:

- Namespace follows repository conventions.
- Extension is publicly callable by Worker.
- No actual provider/service registration.
- Returns the same `IServiceCollection`.
- No side effects.

---

## 5.3 Worker Composition

Update `Program.cs` so the host lifecycle becomes conceptually:

```text
Create Builder
   ↓
AddApplication
   ↓
AddInfrastructure
   ↓
Build
   ↓
Run
```

Example shape:

```csharp
var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var host = builder.Build();

await host.RunAsync();
```

If `AddInfrastructure` requires configuration by authoritative contract:

```csharp
builder.Services.AddInfrastructure(builder.Configuration);
```

Do not add any other registrations.

---

# 6. DI Dependency Resolution

Before mutation, determine whether Application and Infrastructure can compile `IServiceCollection` extension methods using their current framework/package references.

Create this decision:

```text
Application IServiceCollection source:
Application package/framework change required: YES/NO

Infrastructure IServiceCollection source:
Infrastructure IConfiguration required: YES/NO
Infrastructure package/framework change required: YES/NO

Central package change required: YES/NO
```

Use repository/tooling evidence.

Do not add `Microsoft.Extensions.*` packages to projects blindly.

Prefer the smallest supported dependency surface.

If a framework reference or existing transitive framework surface already provides required types, do not add a redundant package.

---

# 7. Dependency Boundary Rules

WP07 must preserve the accepted project graph exactly:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

This means:

- Application must not reference Infrastructure.
- Infrastructure must not reference Worker.
- Domain must remain independent.
- Worker remains the only composition root.
- No new `ProjectReference` may be added.

Package references do not authorize architectural boundary changes.

---

# 8. Execution Procedure

## Step 1 — Read the Contract

Read this prompt, Release plan, manifest, current project/source files, and DI/dependency architecture completely.

Do not execute WP08.

## Step 2 — Record Initial Git State

Run:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Record all pre-existing changes.

Do not clean/reset/restore user work.

## Step 3 — Verify WP06 Baseline

Confirm:

```text
All four projects build
Worker host builds/runs
No AddApplication
No AddInfrastructure
No DependencyInjection.cs
No hosted service
```

If the baseline differs materially, record it before proceeding.

## Step 4 — Verify Project Graph

Confirm exact WP04 graph and zero cycles.

Do not alter it.

## Step 5 — Verify Empty Solution Membership

Run:

```text
dotnet sln AIQuantTradingResearch.slnx list
```

Required:

```text
Project count = 0
```

## Step 6 — Resolve DI Type Dependencies

Determine the minimum compile-time dependency needed for:

```text
IServiceCollection
IConfiguration (only if required)
```

Inspect existing package/framework references first.

Avoid unnecessary package additions.

## Step 7 — Resolve Registration Signatures

From authoritative repository guidance, decide:

```text
AddApplication(IServiceCollection)

AddInfrastructure(IServiceCollection)
or
AddInfrastructure(IServiceCollection, IConfiguration)
```

If repository sources conflict materially, return `BLOCKED`.

Do not select the larger signature simply for future-proofing.

## Step 8 — Add Required Framework/Package References if Necessary

If Application or Infrastructure requires a package reference:

- Use supported .NET CLI tooling.
- Use Central Package Management conventions.
- Add only the minimum required package.
- Add a central version only when missing.
- Do not change unrelated package versions.

Typical package identities may be under `Microsoft.Extensions.*`, but determine actual requirements from evidence.

## Step 9 — Create Application DependencyInjection.cs

Create:

```text
src/AIQuantTradingResearch.Application/DependencyInjection.cs
```

Implement only the extension-point contract.

No service registrations.

No scanning.

No MediatR.

No validators.

No mapping.

No options.

No logging.

No reflection-based auto-registration.

## Step 10 — Create Infrastructure DependencyInjection.cs

Create:

```text
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
```

Implement only the extension-point contract.

No database.

No HTTP clients.

No providers.

No resilience.

No telemetry.

No options binding.

No storage.

## Step 11 — Update Worker Program.cs

Invoke both extension methods between builder creation and host build.

Do not add unrelated logic.

Do not add hosted services.

Do not wrap startup in custom exception handling unless explicitly required by repository authority.

## Step 12 — Build Application

Run:

```text
dotnet build src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
```

Required:

```text
Exit Status = 0
Errors = 0
```

## Step 13 — Build Infrastructure

Run:

```text
dotnet build src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
```

Required:

```text
Exit Status = 0
Errors = 0
```

## Step 14 — Build Worker

Run:

```text
dotnet build src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Required:

```text
Exit Status = 0
Errors = 0
```

## Step 15 — Rebuild Domain

Run:

```text
dotnet build src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj
```

Required: no regression.

## Step 16 — Runtime Smoke Validation

Run the Worker in a bounded manner.

Confirm:

```text
Process starts
No immediate DI exception
No missing service exception
No background process left running
```

Do not add code merely to facilitate validation.

## Step 17 — Verify Registration Purity

Inspect both extension methods.

Required:

```text
No service registrations
No AddHostedService
No AddHttpClient
No AddDbContext
No AddOptions
No feature-specific registration
```

The methods may simply return `services`.

## Step 18 — Revalidate Project Graph

Required:

```text
Graph matches WP04 = Yes
Cycles = 0
New ProjectReference count = 0
```

## Step 19 — Revalidate Solution State

Required:

```text
Root solution project count = 0
```

## Step 20 — Inspect Final Git State

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
```

Expected WP07-owned changes are limited to:

```text
Application/DependencyInjection.cs
Infrastructure/DependencyInjection.cs
Worker/Program.cs
Application.csproj       (only if required)
Infrastructure.csproj    (only if required)
Directory.Packages.props (only if required)
```

Nothing staged.

## Step 21 — Final Scope Validation

Confirm:

```text
AddApplication exists
AddInfrastructure exists
Worker invokes both
No service implementations registered
No hosted service
No feature behavior
WP04 graph unchanged
Root solution membership unchanged
All production projects build
Worker smoke run succeeds
No tests
No docs/eng/CI/Docker changes
No staged changes
```

---

# 9. Registration Invariants

## Application Boundary

Application owns `AddApplication`.

Infrastructure does not define it.

Worker does not define it.

## Infrastructure Boundary

Infrastructure owns `AddInfrastructure`.

Worker does not directly instantiate Infrastructure services.

## Composition Root

Worker calls registration extensions and remains the sole runtime composition root.

## Empty Registration

At WP07 completion, the extension methods establish boundaries, not functionality.

## No Circular Coupling

No registration implementation may create a project reference cycle.

---

# 10. Failure and Ambiguity Handling

## IServiceCollection Dependency Ambiguity

If Application or Infrastructure cannot compile the extension method without additional framework/package support:

- Determine the minimum supported dependency from current .NET/SDK evidence.
- Avoid large meta-packages when a smaller dependency is sufficient.
- Use Central Package Management.
- If package choice/version is ambiguous, return `BLOCKED`.

## IConfiguration Ambiguity

If repository guidance does not clearly require configuration in `AddInfrastructure`:

- Prefer the smaller signature without `IConfiguration`.
- Record the decision and evidence.

If sources explicitly conflict, return `BLOCKED`.

## Build Failure

If build fails after minimal registration changes:

- Capture exact errors.
- Do not add feature packages.
- Do not implement real services.
- Do not modify root build policy.
- Return `BLOCKED` if the registration boundary cannot compile within scope.

## Runtime Failure

If Worker throws at startup because of registration wiring:

- Capture exact failure.
- Fix only WP07-owned registration contract defects.
- Do not implement future services.

---

# 11. Validation and Acceptance

WP07 is accepted only when:

- [ ] Prompt, Release plan, manifest, and DI/dependency guidance were reviewed.
- [ ] Initial Git state was recorded.
- [ ] WP06 baseline was verified.
- [ ] WP04 dependency graph was verified.
- [ ] Root solution contains zero projects.
- [ ] DI type dependencies were resolved from evidence.
- [ ] `AddApplication` signature was resolved.
- [ ] `AddInfrastructure` signature was resolved.
- [ ] Only minimum required framework/package references were added.
- [ ] Central package changes occurred only when strictly required.
- [ ] `Application/DependencyInjection.cs` exists.
- [ ] `Infrastructure/DependencyInjection.cs` exists.
- [ ] Worker invokes `AddApplication`.
- [ ] Worker invokes `AddInfrastructure`.
- [ ] Application registration contains no service implementation.
- [ ] Infrastructure registration contains no service implementation.
- [ ] No hosted/background service was registered.
- [ ] No feature behavior was introduced.
- [ ] Application builds.
- [ ] Infrastructure builds.
- [ ] Worker builds.
- [ ] Domain does not regress.
- [ ] Worker smoke validation succeeds.
- [ ] WP04 project graph remains unchanged.
- [ ] Dependency cycles remain zero.
- [ ] Root solution membership remains unchanged.
- [ ] No tests were created.
- [ ] Root build policy was not modified.
- [ ] No documentation, engineering script, CI, or Docker asset was modified.
- [ ] Nothing was staged, committed, or pushed.
- [ ] Final Git state and exact diff were inspected.
- [ ] Validation evidence and final decision were recorded.

Any failed mandatory criterion must affect the final decision.

---

# 12. Expected Output Contract

Return one complete **Dependency Registration Execution Report** in the Codex response.

Do not create a report file unless separately authorized.

Use this structure.

# Dependency Registration Execution Report

## 1. Executive Summary

State:

- What WP07 authorized.
- Registration boundaries created.
- Dependency/package decisions.
- Build/runtime result.
- Final decision.

## 2. Execution Context

```text
Repository:
Branch:
Starting Commit:
Initial Working Tree:
Configured SDK:
Effective SDK:
```

## 3. Authoritative Sources Reviewed

List exact paths materially used.

## 4. WP06 Baseline Verification

```text
Production dependency graph:
Production build state:
Worker host state:
Root solution project count:
Material pre-existing changes:
```

## 5. DI Dependency Decision

```text
Application IServiceCollection source:
Application package/framework change required:
Infrastructure IServiceCollection source:
Infrastructure IConfiguration required:
Infrastructure package/framework change required:
Central package change required:
Evidence:
```

## 6. Registration Signature Decision

```text
AddApplication signature:
AddInfrastructure signature:
Reason:
Authority:
```

## 7. Changes Applied

| File | Change | Reason | Authority |
| --- | --- | --- | --- |

## 8. Application Registration Assessment

```text
File:
Namespace:
Method:
Service registrations present:
Side effects:
Assessment:
```

## 9. Infrastructure Registration Assessment

```text
File:
Namespace:
Method:
Configuration parameter:
Service registrations present:
Side effects:
Assessment:
```

## 10. Worker Composition Assessment

```text
Builder creation:
AddApplication called:
AddInfrastructure called:
Host build:
Host run:
Hosted service present:
Feature behavior present:
Assessment:
```

## 11. Package and Project Dependency State

```text
Application PackageReferences:
Infrastructure PackageReferences:
Worker PackageReferences:
Project graph:
Cycles:
New ProjectReference count:
```

## 12. Build Validation

| Project | Command | Exit Status | Warnings | Errors | Assessment |
| --- | --- | ---: | ---: | ---: | --- |

## 13. Runtime Smoke Validation

```text
Command:
Process started:
Immediate DI/runtime failure:
Graceful/bounded stop:
Process remaining:
Assessment:
```

## 14. Validation Evidence

| Command | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 15. Scope Compliance

| Scope Check | Result | Evidence |
| --- | --- | --- |
| AddApplication boundary created | PASS/FAIL | ... |
| AddInfrastructure boundary created | PASS/FAIL | ... |
| Worker invokes both | PASS/FAIL | ... |
| No service implementations registered | PASS/FAIL | ... |
| No hosted service | PASS/FAIL | ... |
| No feature behavior | PASS/FAIL | ... |
| WP04 graph unchanged | PASS/FAIL | ... |
| Solution membership unchanged | PASS/FAIL | ... |
| No tests created | PASS/FAIL | ... |
| No root build-policy changes | PASS/FAIL | ... |
| No docs/eng/CI/Docker changes | PASS/FAIL | ... |
| No staging/commit/push | PASS/FAIL | ... |

## 16. Final Git State

Report:

```text
git status --short
```

Distinguish:

- Pre-existing changes.
- WP07-owned changes.
- Ignored/generated build outputs.
- Unexpected changes.

## 17. Findings

When necessary:

| ID | Classification | Finding | Evidence | Required Action | Owner |
| --- | --- | --- | --- | --- | --- |

Allowed:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

## 18. Acceptance Criteria

Reproduce applicable WP07 criteria with PASS/FAIL.

## 19. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

Use `COMPLETE` when the registration boundaries build/run and no unresolved WP07-specific action remains.

Use `COMPLETE WITH ACTIONS` only for a valid result with a non-blocking later-owned finding.

Use `BLOCKED` when the registration boundary cannot be established safely within WP07 scope.

## 20. Next Action

If complete:

```text
Proceed to:
08 — Test Projects
```

If blocked, identify the minimum human decision/remediation required.

Do not begin WP08.

---

# 13. Prohibited Behaviors

Do not:

- Register concrete application services.
- Register concrete infrastructure services.
- Add repositories.
- Add storage/database integration.
- Add HTTP clients.
- Add resilience.
- Add telemetry.
- Add hosted/background workers.
- Add logging providers.
- Add options/configuration models.
- Add market-data/trading/AI/plugin/API behavior.
- Add project references.
- Add projects to the solution.
- Modify root build policy.
- Add tests.
- Modify documentation.
- Modify engineering scripts.
- Modify CI.
- Modify Docker.
- Reformat unrelated files.
- Stage.
- Commit.
- Push.
- Open a pull request.
- Begin WP08.

---

# 14. Completion Model

```text
Inspect
   ↓
Verify WP06 Baseline
   ↓
Resolve DI Type Dependencies
   ↓
Resolve Registration Signatures
   ↓
Create AddApplication
   ↓
Create AddInfrastructure
   ↓
Wire Worker Composition
   ↓
Build All Production Projects
   ↓
Smoke Run Worker
   ↓
Verify Empty Registration Boundaries
   ↓
Preserve Project Graph + Empty Solution
   ↓
Inspect Git Diff
   ↓
Report Evidence
   ↓
COMPLETE | COMPLETE WITH ACTIONS | BLOCKED
```

---

# 15. Final Instruction

Execute **Phase 2 — Release 0.8 / Work Package 07 — Dependency Registration** against the actual current `AIQuantTradingResearch` repository.

Read all authoritative sources before mutation.

Create only the Application and Infrastructure registration boundaries required by Release 0.8.

Resolve the minimum compile-time dependency needed for `IServiceCollection` and, only if justified, `IConfiguration`.

Implement `AddApplication` with no real service registrations.

Implement `AddInfrastructure` with no real service registrations.

Update Worker `Program.cs` to invoke both registration methods.

Do not implement any actual application or infrastructure behavior.

Do not add hosted services.

Do not change the WP04 project-reference graph.

Do not add projects to the root solution.

Build all production projects and run a bounded Worker smoke validation.

Inspect final Git state and prove scope preservation.

Return the complete **Dependency Registration Execution Report**.

Finish with exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If complete, identify:

```text
08 — Test Projects
```

as the next work package, but do not begin it.

---

# Conclusion

Work Package 07 turns the Worker from a standalone host into an explicit composition root while preserving empty application and infrastructure boundaries.

The intended transition is:

```text
Minimal Worker Host
        ↓
Application Registration Boundary
        ↓
Infrastructure Registration Boundary
        ↓
Worker Invokes Both
        ↓
No Real Services Yet
        ↓
Build + Runtime Validation
        ↓
Controlled Handoff to WP08
```

The value of WP07 is architectural ownership.

Application should own how Application services are registered.

Infrastructure should own how Infrastructure services are registered.

Worker should orchestrate those boundaries rather than know implementation details.

By establishing the registration seams before adding real services, later releases can extend composition without turning the Worker into a service-registration dumping ground.

The central principle is:

> **Create composition boundaries before composition complexity: let each layer own its registration contract, keep the Worker as the orchestrator, and defer real service wiring until real capabilities exist.**

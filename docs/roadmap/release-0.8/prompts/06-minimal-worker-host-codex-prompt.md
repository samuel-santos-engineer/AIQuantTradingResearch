# Codex Execution Prompt — Release 0.8 / 06 Minimal Worker Host

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.8 — Solution Skeleton |
| Work Package | 06 — Minimal Worker Host |
| Issue Type | Feature |
| Execution Mode | Controlled repository modification |
| Primary Agent | Codex |
| Prerequisite | 05 — Root Build Configuration accepted as `COMPLETE WITH ACTIONS` |
| Primary Project | `src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj` |
| Primary Source | `src/AIQuantTradingResearch.Worker/Program.cs` |
| Expected Outcome | Minimal, buildable .NET Worker host baseline with no application behavior and no dependency-registration implementation |

---

## Purpose

Establish the minimal executable Worker host for the Release 0.8 Solution Skeleton.

WP03 created the Worker project boundary. WP05 confirmed that the project configuration is valid but the Worker does not compile because its SDK-generated global usings require hosting assemblies that are not yet available.

WP06 owns resolving that specific runtime-host baseline.

The goal is intentionally narrow:

```text
Worker project
    ↓
Required hosting dependency
    ↓
Minimal generic host bootstrap
    ↓
Buildable executable boundary
```

WP06 must not yet implement Application or Infrastructure dependency registration. That belongs to WP07.

It must not introduce business features, hosted background work, configuration models, providers, tests, engineering scripts, CI, or solution organization.

---

## Objective

Make `AIQuantTradingResearch.Worker` compile as the minimal executable host.

The final Worker should:

- Use the approved .NET Worker/hosting model.
- Have the minimum required hosting dependency.
- Build successfully under SDK `10.0.103`.
- Preserve the existing WP04 project-reference graph.
- Contain a minimal `Program.cs`.
- Create and run the host.
- Register no application services.
- Register no infrastructure services.
- Register no background worker service.
- Introduce no feature-specific runtime behavior.
- Add no test code.
- Add no unrelated configuration.
- Preserve all repository state outside the authorized WP06 change set.

---

# 1. Authority and Preconditions

Before modifying anything, read completely:

```text
docs/roadmap/release-0.8/prompts/06-minimal-worker-host-codex-prompt.md
docs/roadmap/release-0.8/RELEASE_0.8_EXECUTION_PLAN.md
docs/roadmap/release-0.8/RELEASE_0.8_FILE_MANIFEST.md
```

Read the current Worker project/source:

```text
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
src/AIQuantTradingResearch.Worker/Program.cs
```

Read current central build/package configuration:

```text
Directory.Build.props
Directory.Packages.props
global.json
```

Read applicable host/DI architecture guidance:

```text
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/CONFIGURATION_MODEL.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
```

If an exact listed file does not exist, do not create or rename documentation. Record the absence and use available authoritative sources.

Consult relevant Toolkit guidance when materially useful:

```text
AI-Engineering-Toolkit/docs/AI_ASSISTED_ENGINEERING_WORKFLOW.md
AI-Engineering-Toolkit/playbooks/dotnet/01-solution-architecture.md
AI-Engineering-Toolkit/playbooks/dotnet/04-dependency-management.md
AI-Engineering-Toolkit/playbooks/dotnet/05-coding-standards.md
AI-Engineering-Toolkit/playbooks/dotnet/06-error-handling.md
AI-Engineering-Toolkit/playbooks/dotnet/09-security.md
AI-Engineering-Toolkit/playbooks/dotnet/12-project-review.md
```

Release-specific repository authority takes precedence over generic examples.

---

# 2. Accepted Baseline from WP05

Expected production graph:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Expected project state:

```text
Domain build: PASS
Application build: PASS
Infrastructure build: PASS
Worker build: FAIL due to missing Microsoft.Extensions hosting assemblies
```

Expected repository state:

```text
Root solution exists
Root solution project count = 0
No test projects
No dependency registration files
No feature code
```

WP05 finding to resolve:

```text
Worker lacks the hosting dependency/runtime setup needed by its SDK-generated global usings.
Owner: WP06
```

Verify actual current state before mutation.

If the baseline materially changed, do not guess.

---

# 3. Scope

## In Scope

You may:

- Inspect repository/Git state.
- Inspect the Worker `.csproj` and `Program.cs`.
- Inspect central package versions.
- Determine the minimum hosting dependency required by the Worker SDK/runtime model.
- Modify the Worker `.csproj` if needed.
- Modify `Directory.Packages.props` only if a centrally governed package version is required specifically to make the minimal Worker host valid.
- Modify `Program.cs` to establish the minimal host lifecycle.
- Build and run the Worker for validation where safe.
- Revalidate all production project references.
- Inspect final Git state.
- Produce an evidence-based execution report.

## Out of Scope

Do not:

- Add or remove production projects.
- Change the WP04 dependency graph.
- Add projects to `AIQuantTradingResearch.slnx`.
- Create `DependencyInjection.cs`.
- Add `AddApplication`.
- Add `AddInfrastructure`.
- Register application services.
- Register infrastructure services.
- Add any hosted service/background worker implementation.
- Add business/domain/application/infrastructure behavior.
- Create appsettings unless the chosen minimal host requires them for correctness.
- Add feature configuration.
- Add logging providers beyond the default host behavior.
- Add telemetry.
- Add database/storage providers.
- Add HTTP/API behavior.
- Add tests.
- Add architecture tests.
- Modify root build policy.
- Modify `global.json`.
- Modify `.editorconfig`.
- Modify engineering scripts.
- Modify documentation.
- Modify CI/GitHub workflows.
- Modify Docker assets.
- Stage, commit, push, or open a pull request.
- Begin WP07.

---

# 4. Authorized Change Set

Primary authorized files:

```text
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
src/AIQuantTradingResearch.Worker/Program.cs
```

Conditional authorized file:

```text
Directory.Packages.props
```

`Directory.Packages.props` may be modified only if:

1. The minimal Worker host requires a package reference.
2. The repository uses Central Package Management.
3. The required package version is not already declared.
4. The package/version is directly justified by the installed .NET template/runtime and authoritative repository guidance.

Do not modify any other file.

If no package-version change is required, leave `Directory.Packages.props` unchanged.

---

# 5. Minimal Host Contract

The Worker should conceptually become:

```text
Create application builder
        ↓
Build host
        ↓
Run host
```

The preferred minimal shape is equivalent to:

```csharp
var builder = Host.CreateApplicationBuilder(args);

var host = builder.Build();

await host.RunAsync();
```

Use repository coding conventions and SDK-appropriate syntax.

This example defines the intended behavior, not mandatory exact formatting.

Do not add:

```text
builder.Services.AddApplication(...)
builder.Services.AddInfrastructure(...)
builder.Services.AddHostedService(...)
```

Those are not WP06 responsibilities.

---

# 6. Hosting Dependency Contract

Determine the actual package/framework dependency required by the current Worker SDK and generated global usings.

Inspect:

- Worker project SDK.
- Current `PackageReference` state.
- Central package versions.
- Installed SDK/template behavior.
- WP05 build errors.

Prefer the smallest supported dependency that restores a normal .NET Worker hosting baseline.

Use supported .NET tooling when adding the reference.

If Central Package Management is active:

- Do not specify package version in the Worker `.csproj`.
- Add a central version only when missing and required.
- Do not change unrelated package versions.

Do not add multiple packages when one is sufficient.

Do not add third-party hosting libraries.

---

# 7. Package Resolution Procedure

Before mutation, produce a small dependency decision:

```text
Current Worker SDK:
Missing namespace/assembly:
Required hosting package/framework:
Already centrally versioned: YES/NO
Worker PackageReference required: YES/NO
Directory.Packages.props change required: YES/NO
```

If authoritative repository guidance and the installed SDK/template disagree materially, stop and return `BLOCKED`.

Do not choose a package solely from memory when repository/tooling evidence can determine it.

---

# 8. Execution Procedure

## Step 1 — Read the Contract

Read this prompt, Release plan, manifest, Worker source/project, central package configuration, and host/DI guidance completely.

Do not implement WP07.

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

## Step 3 — Verify WP05 Baseline

Confirm:

```text
Domain buildable
Application buildable
Infrastructure buildable
Worker project parses
Worker currently fails compilation for missing hosting assemblies
```

If Worker now builds or fails for a materially different reason, record the changed baseline and reassess scope.

## Step 4 — Verify Production Graph

Confirm exact graph:

```text
Domain          → none
Application     → Domain
Infrastructure  → Application
Worker          → Application, Infrastructure
```

Do not change it.

## Step 5 — Verify Empty Solution Membership

Run:

```text
dotnet sln AIQuantTradingResearch.slnx list
```

Required:

```text
Project count = 0
```

Do not add projects.

## Step 6 — Inspect Worker and Central Package State

Inspect:

```text
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
src/AIQuantTradingResearch.Worker/Program.cs
Directory.Packages.props
```

Determine the exact minimal dependency needed.

## Step 7 — Resolve Hosting Dependency

Produce the dependency decision described in Section 7.

If the required package is already centrally versioned, use it.

If not, add exactly one central version only when required by the approved hosting baseline.

## Step 8 — Add Minimal Worker Package Reference if Required

Use supported .NET CLI tooling.

Typical pattern:

```text
dotnet add src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj package Microsoft.Extensions.Hosting
```

When Central Package Management applies, do not hardcode a project-level version.

If installed SDK tooling uses updated syntax, use the supported equivalent.

Do not add any other package unless the first dependency is demonstrably insufficient and authoritative evidence requires another.

## Step 9 — Implement Minimal Program.cs

Replace the current placeholder:

```text
return;
```

with the minimal host lifecycle.

Conceptual target:

```csharp
var builder = Host.CreateApplicationBuilder(args);
var host = builder.Build();
await host.RunAsync();
```

Do not register services.

Do not add configuration sections.

Do not add logging statements.

Do not add exception handling wrappers unless repository authority explicitly requires them for the minimal host.

## Step 10 — Inspect Effective Package State

Confirm:

```text
Worker PackageReference count = expected minimal count
No other production project package references changed
No ProjectReference changed
```

## Step 11 — Build Worker

Run:

```text
dotnet build src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Required:

```text
Exit Status = 0
Errors = 0
```

Record warnings.

Do not broaden scope to fix unrelated repository warnings.

## Step 12 — Rebuild Other Production Projects if Appropriate

Validate:

```text
dotnet build src/AIQuantTradingResearch.Domain/AIQuantTradingResearch.Domain.csproj
dotnet build src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
dotnet build src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
```

Required: no regression attributable to WP06.

## Step 13 — Runtime Smoke Validation

Run the Worker only if safe.

Because a minimal host with no hosted services may run indefinitely, use a bounded validation approach.

Suitable approaches include:

- Start the process.
- Confirm successful startup/no immediate failure.
- Stop it gracefully after a short bounded observation.

Do not leave background processes running.

Do not introduce code merely to make the process exit.

Record:

```text
Process started:
Immediate startup failure:
Graceful stop:
Assessment:
```

## Step 14 — Revalidate Dependency Graph

Confirm WP04 graph is unchanged and acyclic.

Required:

```text
Graph changed = No
Cycles = 0
```

## Step 15 — Revalidate Solution State

Run:

```text
dotnet sln AIQuantTradingResearch.slnx list
```

Required:

```text
Project count = 0
```

## Step 16 — Inspect Final Diff

Run:

```text
git status --short
git diff -- .
git diff --cached -- .
```

Expected WP06-owned modifications are limited to:

```text
Worker.csproj
Program.cs
Directory.Packages.props  (only if required)
```

Nothing staged.

## Step 17 — Final Scope Validation

Confirm:

```text
Worker builds successfully
Minimal host lifecycle present
No hosted service registered
No AddApplication
No AddInfrastructure
No DI extension created
No feature code added
WP04 graph unchanged
Root solution membership unchanged
No tests created
No docs/eng/CI/Docker changes
No staged changes
```

---

# 9. Worker Host Invariants

At completion:

## Buildable

```text
Worker build exit = 0
```

## Minimal

Only required host bootstrap exists.

## No Application Registration

```text
AddApplication = absent
```

## No Infrastructure Registration

```text
AddInfrastructure = absent
```

## No Hosted Work

```text
AddHostedService = absent
Worker background service = absent
```

## No Feature Behavior

No market data, storage, trading, AI/ML, plugin, API, or provider behavior.

## Dependency Graph Preserved

WP04 graph remains exact.

---

# 10. Failure and Ambiguity Handling

## Package Conflict

If the minimal required hosting package is absent from central package management:

- Verify package identity and compatible version using installed SDK/template evidence and current repository conventions.
- Add only the necessary central version if confidently resolved.
- If version choice is ambiguous or conflicts with repository policy, return `BLOCKED`.

## Worker Build Still Fails

If Worker still fails after the minimal required hosting dependency and host bootstrap:

- Capture exact errors.
- Do not add speculative packages.
- Do not modify root build policy.
- Do not implement WP07.
- Determine whether the failure is WP06-owned.
- Return `BLOCKED` if the minimal host cannot be made valid within scope.

## Host Requires Additional Files

If the SDK/runtime requires a configuration file for the minimal host:

- Prove the requirement.
- Add only the minimum file required.
- Report the scope expansion explicitly.
- If not mandatory, do not add it.

## Unexpected Existing Runtime Work

If `Program.cs` or Worker project already contains meaningful uncommitted human changes:

- Preserve them.
- Do not overwrite.
- Return `BLOCKED` if safe integration is ambiguous.

---

# 11. Validation and Acceptance

WP06 is accepted only when:

- [ ] Prompt, Release plan, manifest, and host/DI guidance were reviewed.
- [ ] Initial Git state was recorded.
- [ ] WP05 baseline was verified.
- [ ] WP04 dependency graph was verified.
- [ ] Root solution contains zero projects.
- [ ] Worker hosting dependency requirement was resolved from evidence.
- [ ] Only the minimum required hosting package/reference was added.
- [ ] Central package version was changed only if strictly required.
- [ ] `Program.cs` contains a minimal host lifecycle.
- [ ] No Application registration was added.
- [ ] No Infrastructure registration was added.
- [ ] No hosted/background service was added.
- [ ] No feature behavior was added.
- [ ] Worker builds successfully.
- [ ] Domain/Application/Infrastructure do not regress because of WP06.
- [ ] Runtime smoke validation was performed safely or explicitly justified as not applicable.
- [ ] WP04 project-reference graph remains unchanged.
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

Return one complete **Minimal Worker Host Execution Report** in the Codex response.

Do not create a report file unless separately authorized.

Use this structure.

# Minimal Worker Host Execution Report

## 1. Executive Summary

State:

- What WP06 authorized.
- Hosting dependency resolved.
- Worker host behavior implemented.
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

## 4. WP05 Baseline Verification

```text
Production dependency graph:
Domain build:
Application build:
Infrastructure build:
Worker initial build:
Root solution project count:
Material pre-existing changes:
```

## 5. Hosting Dependency Decision

```text
Worker SDK:
Observed missing namespace/assembly:
Required hosting dependency:
Already centrally versioned:
Worker PackageReference required:
Directory.Packages.props change required:
Decision evidence:
```

## 6. Changes Applied

| File | Change | Reason | Authority |
| --- | --- | --- | --- |

## 7. Final Worker Project State

```text
Project SDK:
TargetFramework:
ProjectReferences:
PackageReferences:
Hosting package:
Assessment:
```

## 8. Program.cs Assessment

```text
Builder creation:
Host build:
Host run:
Application registration present:
Infrastructure registration present:
Hosted service present:
Feature behavior present:
Assessment:
```

## 9. Build Validation

| Project | Command | Exit Status | Warnings | Errors | Assessment |
| --- | --- | ---: | ---: | ---: | --- |
| Domain | ... | ... | ... | ... | ... |
| Application | ... | ... | ... | ... | ... |
| Infrastructure | ... | ... | ... | ... | ... |
| Worker | ... | ... | ... | ... | ... |

## 10. Runtime Smoke Validation

```text
Command:
Process started:
Immediate failure:
Graceful stop:
Assessment:
```

If not executed, explain why.

## 11. Dependency and Solution Preservation

```text
Dependency graph matches WP04:
Cycles:
Root solution project count:
Solution membership changed:
```

## 12. Validation Evidence

| Command | Exit Status | Result | Interpretation |
| --- | ---: | --- | --- |

## 13. Scope Compliance

| Scope Check | Result | Evidence |
| --- | --- | --- |
| Minimal hosting dependency only | PASS/FAIL | ... |
| Minimal Program.cs only | PASS/FAIL | ... |
| No AddApplication | PASS/FAIL | ... |
| No AddInfrastructure | PASS/FAIL | ... |
| No hosted service | PASS/FAIL | ... |
| No feature behavior | PASS/FAIL | ... |
| WP04 graph unchanged | PASS/FAIL | ... |
| Solution membership unchanged | PASS/FAIL | ... |
| No tests created | PASS/FAIL | ... |
| No root build-policy changes | PASS/FAIL | ... |
| No docs/eng/CI/Docker changes | PASS/FAIL | ... |
| No staging/commit/push | PASS/FAIL | ... |

## 14. Final Git State

Report:

```text
git status --short
```

Distinguish:

- Pre-existing changes.
- WP06-owned changes.
- Ignored/generated build outputs.
- Unexpected changes.

## 15. Findings

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

## 16. Acceptance Criteria

Reproduce applicable WP06 criteria with PASS/FAIL.

## 17. Final Decision

State exactly one:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

Use `COMPLETE` when the minimal Worker host builds/runs and no unresolved WP06-specific action remains.

Use `COMPLETE WITH ACTIONS` only when the Worker host is valid but a non-blocking finding has a clear later owner.

Use `BLOCKED` when the minimal host cannot be made valid safely within WP06 scope.

## 18. Next Action

If complete:

```text
Proceed to:
07 — Dependency Registration
```

If blocked, identify the minimum human decision/remediation required.

Do not begin WP07.

---

# 13. Prohibited Behaviors

Do not:

- Add `AddApplication`.
- Add `AddInfrastructure`.
- Create `DependencyInjection.cs`.
- Register `IHostedService` or background workers.
- Add market-data/storage/trading/AI/plugin/API behavior.
- Add logging providers or telemetry packages beyond the minimal host requirement.
- Change the WP04 project graph.
- Add projects to the solution.
- Modify root build policy.
- Add tests.
- Modify documentation.
- Modify engineering scripts.
- Modify CI.
- Modify Docker.
- Add configuration files without demonstrated necessity.
- Reformat unrelated files.
- Stage.
- Commit.
- Push.
- Open a pull request.
- Begin WP07.

---

# 14. Completion Model

```text
Inspect
   ↓
Verify WP05 Baseline
   ↓
Resolve Missing Hosting Dependency
   ↓
Add Minimum Package Reference
   ↓
Implement Minimal Host Lifecycle
   ↓
Build Worker
   ↓
Smoke Run
   ↓
Verify No WP07 Behavior
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

Execute **Phase 2 — Release 0.8 / Work Package 06 — Minimal Worker Host** against the actual current `AIQuantTradingResearch` repository.

Read all authoritative sources before mutation.

Resolve the WP05 Worker build failure using the minimum evidence-backed .NET hosting dependency.

Implement only the minimal Worker host lifecycle in `Program.cs`.

Do not add Application or Infrastructure dependency registration.

Do not add hosted/background services.

Do not add feature behavior.

Preserve the WP04 dependency graph and empty solution membership.

Build the Worker successfully and perform bounded runtime smoke validation where safe.

Inspect final Git state and prove scope preservation.

Return the complete **Minimal Worker Host Execution Report**.

Finish with exactly one evidence-based decision:

```text
COMPLETE
COMPLETE WITH ACTIONS
BLOCKED
```

If complete, identify:

```text
07 — Dependency Registration
```

as the next work package, but do not begin it.

---

# Conclusion

Work Package 06 converts the Worker from an empty executable boundary into the smallest valid runtime host.

The intended transition is:

```text
Worker Project Boundary
        ↓
Resolve Hosting Dependency
        ↓
Create Minimal Host Builder
        ↓
Build Host
        ↓
Run Host
        ↓
Validate Build + Startup
        ↓
No Application/Infrastructure Registration Yet
        ↓
Controlled Handoff to WP07
```

The purpose is not to create application behavior. It is to establish a valid executable composition boundary that later work packages can extend deliberately.

By separating host creation from dependency registration, Release 0.8 keeps runtime composition incremental, testable, and reviewable.

The central principle is:

> **Make the host real before making it smart: establish the minimum valid runtime boundary first, then add composition responsibilities only in the work package that explicitly owns them.**

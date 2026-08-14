# Codex Execution Prompt — Release 0.9 / WP07 Dependency Registration

## Metadata

| Field | Value |
| --- | --- |
| Project | AIQuantTradingResearch |
| Phase | Phase 2 |
| Release | 0.9 — Research Platform |
| Work Package | 07 — Dependency Registration |
| Type | Implementation |
| Milestone | #40 — Phase 2 - Release 0.9: Research Platform |
| Prerequisite | WP06 — Research Infrastructure Adapter = `ADAPTER COMPLETE` |
| Execution Mode | Narrowly scoped DI registration |
| Expected Outcome | Register the existing Release 0.9 Application use case and deterministic Infrastructure adapter through the established `AddApplication(...)` and `AddInfrastructure(...)` boundaries, with justified lifetimes and no Worker execution, tests, or future-scope registration |

## 1. Purpose

Execute:

```text
Phase 2 — Release 0.9
WP07 — Dependency Registration
```

WP07 wires:

```text
IResearchUseCase -> ResearchUseCase
IObservationSource -> DeterministicObservationSource
```

through the existing Application and Infrastructure dependency-registration boundaries.

WP07 does not execute the research workflow, modify Worker behavior, or create tests.

Do not begin WP08.

## 2. Authoritative Sources

Read completely before mutation:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/07-dependency-registration-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Inspect:

```text
src/AIQuantTradingResearch.Application/Research/IResearchUseCase.cs
src/AIQuantTradingResearch.Application/Research/ResearchUseCase.cs
src/AIQuantTradingResearch.Application/Research/IObservationSource.cs
src/AIQuantTradingResearch.Application/DependencyInjection.cs

src/AIQuantTradingResearch.Infrastructure/Research/DeterministicObservationSource.cs
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs

src/AIQuantTradingResearch.Worker/**
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
```

Use existing registration conventions; do not invent a new DI architecture.

## 3. Accepted WP06 Baseline

Expected:

```text
ResearchUseCase implements IResearchUseCase
ResearchUseCase depends only on IObservationSource

DeterministicObservationSource implements IObservationSource

Application registration boundary exists and is empty
Infrastructure registration boundary exists and is empty

Worker references Application + Infrastructure
```

No new project relationship is required.

## 4. Registration Ownership

### Application

Register:

```text
IResearchUseCase -> ResearchUseCase
```

inside the existing Application registration boundary.

### Infrastructure

Register:

```text
IObservationSource -> DeterministicObservationSource
```

inside the existing Infrastructure registration boundary.

### Worker

Worker remains responsible only for calling:

```text
AddApplication(...)
AddInfrastructure(...)
```

WP07 must not add research execution to Worker. That belongs to WP08.

## 5. Lifetime Analysis

Choose lifetimes from actual semantics.

Current types are stateless and own no external resource:

```text
ResearchUseCase:
  stateless orchestration
  depends only on IObservationSource

DeterministicObservationSource:
  immutable deterministic fixture
  no per-request mutable state
  no network/database/file resource
```

Select the smallest consistent lifetime model that follows repository DI guidance and avoids captive dependencies.

Record:

```text
IResearchUseCase lifetime
IObservationSource lifetime
rationale
captive-dependency assessment
```

Do not invent scoped semantics without a scoped resource.

## 6. Registration API Discipline

Use explicit registrations through the existing extension methods.

Do not create:

```text
service locator
global IServiceProvider
reflection/assembly scanning
Scrutor registration
plugin scanning
registration convention frameworks
```

## 7. Visibility

`ResearchUseCase` and `DeterministicObservationSource` are internal implementation types.

Register each from within its owning assembly, where the type is visible.

Do not make implementation types public merely to support Worker.

Worker should consume public abstractions only.

## 8. Authorized Scope

WP07 may modify only the existing registration boundaries:

```text
src/AIQuantTradingResearch.Application/DependencyInjection.cs
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
```

Minimal local code strictly required for those registrations is allowed.

WP07 may run temporary container-resolution validation, build, formatting, Architecture.Tests, and canonical verification.

## 9. Prohibited Scope

Do not:

- modify Domain;
- modify Application contracts;
- modify `ResearchUseCase`;
- modify `DeterministicObservationSource`;
- modify Worker behavior;
- execute the research workflow from Worker;
- create tests;
- modify Architecture.Tests;
- add packages;
- add project references;
- add configuration binding;
- create service locators;
- create scanning/registration frameworks;
- add hosted/background services;
- register future services/providers;
- modify CI;
- modify GitHub planning;
- stage, commit, push, or create a PR unless separately authorized;
- begin WP08.

## 10. Dependency Rules

Preserve exactly:

```text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application + Infrastructure
```

No new project reference is authorized.

Confirm:

```text
Application refs = Domain only
Infrastructure refs = Application only
Worker refs = Application + Infrastructure
cycles = 0
```

## 11. Working-Tree Protection

Before mutation record:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git status --short
```

Classify and preserve all cumulative Release 0.9 artifacts, including WP01–WP07 prompt/chat pairs, the research model, WP03 Domain source, WP04 contracts, WP05 use case, and WP06 adapter.

Do not clean, delete, or stage pre-existing artifacts.

## 12. Implementation Procedure

1. Read all authority and current registration boundaries.
2. Confirm WP06 = `ADAPTER COMPLETE`.
3. Record lifetime decisions before coding.
4. Register `IResearchUseCase -> ResearchUseCase` in Application.
5. Register `IObservationSource -> DeterministicObservationSource` in Infrastructure.
6. Confirm no unused/future service was registered.
7. If useful, create a temporary validation harness:
   - create `ServiceCollection`;
   - call `AddApplication(...)`;
   - call `AddInfrastructure(...)`;
   - build provider;
   - resolve `IResearchUseCase`;
   - resolve `IObservationSource`;
   - prove `ResearchUseCase` constructor dependency is satisfied.
8. Remove the harness.
9. Confirm Worker remains unchanged.
10. Run formatting verification and full solution build.
11. Run Architecture.Tests.
12. Run:
    `powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1`
13. Inspect project/package references and scope.
14. Run:
    `git status --short`
    `git diff -- .`
    `git diff --cached -- .`
    `git diff --check`

## 13. Resolution Validation

Prove:

```text
IResearchUseCase resolves successfully
IObservationSource resolves successfully
ResearchUseCase dependency graph resolves
Worker does not manually construct implementations
```

Do not claim end-to-end Worker execution; WP08 owns that.

## 14. Scope-Leakage Review

Inspect the WP07 delta for:

```text
ServiceProvider.GetService outside temporary validation
global provider/container
reflection/assembly scanning
Scrutor
plugin/provider selection
hosted service
BackgroundService
Worker research execution
real provider
database
HTTP
```

Remove any actual leakage before completion.

## 15. Decision Model

Return exactly one:

```text
REGISTRATION COMPLETE
REGISTRATION COMPLETE WITH ACTIONS
REGISTRATION BLOCKED
```

Use `REGISTRATION COMPLETE` when:

```text
IResearchUseCase registration exists
IObservationSource registration exists
ownership is correct
lifetimes are justified
both abstractions resolve
Worker remains unchanged
no service locator/scanning/future registration exists
dependency graph remains exact
build/Architecture.Tests/verify pass
WP08 can proceed
```

## 16. Acceptance Criteria

- [ ] WP07 prompt read completely.
- [ ] Release 0.9 plan/manifest read completely.
- [ ] DI guidance inspected.
- [ ] WP05 use case inspected.
- [ ] WP06 adapter inspected.
- [ ] WP06 `ADAPTER COMPLETE` confirmed.
- [ ] Initial Git state classified.
- [ ] Lifetime decision made before coding.
- [ ] `IResearchUseCase -> ResearchUseCase` registered in Application.
- [ ] `IObservationSource -> DeterministicObservationSource` registered in Infrastructure.
- [ ] Ownership boundaries preserved.
- [ ] No future service registered.
- [ ] No scanning framework introduced.
- [ ] No service locator/global provider introduced.
- [ ] Implementation types not made public merely for Worker.
- [ ] `IResearchUseCase` resolves.
- [ ] `IObservationSource` resolves.
- [ ] Constructor graph resolves.
- [ ] Worker unchanged.
- [ ] No research execution added to Worker.
- [ ] No tests created.
- [ ] No package added.
- [ ] No project reference added.
- [ ] Production graph exact.
- [ ] Cycles = 0.
- [ ] Build succeeds with zero errors.
- [ ] Architecture.Tests pass.
- [ ] `eng/verify.ps1` passes.
- [ ] `git diff --check` passes.
- [ ] Final Git state inspected.
- [ ] No GitHub planning mutation.
- [ ] WP08 not started.

## 17. Expected Output Contract

Return one complete **Release 0.9 WP07 Dependency Registration Execution Report** containing:

1. Executive Summary
2. Execution Context
3. Authoritative Sources Reviewed
4. Registration Mapping
5. Source Changes
6. Application Registration
7. Infrastructure Registration
8. Lifetime Analysis
9. Resolution Validation
10. Registration Ownership Assessment
11. Dependency Assessment
12. Scope-Leakage Assessment
13. Build / Architecture Validation
14. Canonical Verification
15. Test Boundary
16. Validation Evidence
17. Final Git State
18. Scope Compliance
19. Findings
20. Acceptance Criteria Matrix
21. Final Decision
22. Next Authorized Work Package

Final decision must be exactly one:

```text
REGISTRATION COMPLETE
REGISTRATION COMPLETE WITH ACTIONS
REGISTRATION BLOCKED
```

If progression is permitted, identify:

```text
WP08 — Worker Research Execution
```

as next.

Do not execute WP08.

## 18. Final Instruction

Execute Release 0.9 / WP07 — Dependency Registration.

Wire only:

```text
IResearchUseCase -> ResearchUseCase
IObservationSource -> DeterministicObservationSource
```

through the existing Application/Infrastructure registration boundaries.

Choose and justify lifetimes.

Do not modify Worker, create tests, introduce scanning/service-locator infrastructure, register future services, or add packages/project references.

Validate container resolution with a temporary harness if useful, remove it, then build, run Architecture.Tests, run `eng/verify.ps1`, inspect dependencies/scope/Git state, and return the full WP07 report.

Finish with exactly one:

```text
REGISTRATION COMPLETE
REGISTRATION COMPLETE WITH ACTIONS
REGISTRATION BLOCKED
```

If complete, identify `WP08 — Worker Research Execution` as next.

Do not execute WP08.

# Conclusion

WP07 completes the dependency graph without executing the workflow:

```text
ResearchUseCase
        +
DeterministicObservationSource
        ↓
Application Registration
        +
Infrastructure Registration
        ↓
Container Resolution
        ↓
REGISTRATION COMPLETE
        ↓
WP08 Worker Research Execution
```

> **Dependency injection should explicitly compose approved boundaries without becoming an architecture of its own.**

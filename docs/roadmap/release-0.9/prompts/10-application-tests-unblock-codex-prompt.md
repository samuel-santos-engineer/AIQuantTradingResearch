# Codex Execution Prompt — Release 0.9 / WP10 Testability Unblock

## Purpose

Unblock **WP10 — Application Tests** by adding the smallest explicit testability boundary required for `AIQuantTradingResearch.Application.Tests` to directly access the internal `ResearchUseCase`.

This is a narrow governance correction, not a new work package.

Do not implement WP10 tests in this run.

Do not begin WP11.

## Authority

Read completely:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/10-application-tests-codex-prompt.md
```

Also inspect:

```text
src/AIQuantTradingResearch.Application/Research/ResearchUseCase.cs
src/AIQuantTradingResearch.Application/AIQuantTradingResearch.Application.csproj
tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj
```

The previous WP10 execution established this blocker:

```text
ResearchUseCase is internal.
Application.Tests has no InternalsVisibleTo access.
Direct construction required by WP10 is therefore impossible.
```

## Authorized Change

Authorize exactly one production testability boundary:

```csharp
[assembly: InternalsVisibleTo("AIQuantTradingResearch.Application.Tests")]
```

Implement it using the smallest repository-consistent mechanism.

Preferred direction when no existing convention says otherwise:

```text
src/AIQuantTradingResearch.Application/Properties/AssemblyInfo.cs
```

containing only the required `InternalsVisibleTo` declaration and necessary import.

Do not make `ResearchUseCase` public.

Do not change its constructor or behavior.

Do not modify Application contracts.

Do not modify Domain, Infrastructure, or Worker.

Do not add packages or project references.

Do not add any other friend assembly.

## Validation

After the change:

1. Confirm `ResearchUseCase` remains `internal`.
2. Confirm only `AIQuantTradingResearch.Application.Tests` receives friend access.
3. Prove the test project can compile code that directly names/constructs `ResearchUseCase`.
   - A temporary compile-only probe is allowed.
   - Remove the probe before completion.
   - Do not create actual WP10 behavioral tests.
4. Run:

```text
dotnet build
dotnet test tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
git status --short
```

5. Verify no unexpected scope was introduced.

Known `NU1900` vulnerability-feed connectivity warnings may remain observations if validation succeeds.

## Prohibited Scope

Do not:

```text
implement WP10 tests
change ResearchUseCase visibility to public
use DI/reflection as a workaround
modify Infrastructure or Worker
modify DI registrations
add packages
add project references
modify GitHub planning
stage, commit, push, or create a PR
begin WP11
```

## Expected Output

Return a concise:

```text
WP10 Testability Unblock Execution Report
```

including:

```text
Authorized change
Actual file changed/created
Friend assembly granted
ResearchUseCase visibility
Compile-access validation
Build result
Domain.Tests result
Architecture.Tests result
eng/verify.ps1 result
Final Git state
Unexpected changes
```

Finish with exactly one:

```text
UNBLOCK COMPLETE
UNBLOCK BLOCKED
```

Use `UNBLOCK COMPLETE` only when `Application.Tests` can directly access `ResearchUseCase`, all validation passes, and no WP10 tests were implemented.

If complete, state:

```text
Resume WP10 using the existing authoritative:
docs/roadmap/release-0.9/prompts/10-application-tests-codex-prompt.md
```

Do not execute WP10 in this run.

# Codex Execution Prompt — Release 0.9 / WP11 Testability Unblock

## Purpose

Unblock **WP11 — Infrastructure Tests** by adding the smallest explicit testability boundary required for `AIQuantTradingResearch.Infrastructure.Tests` to directly access the internal `DeterministicObservationSource`.

This is a narrow governance correction, not a new work package.

Do not implement WP11 tests in this run.

Do not begin WP12.

## Authority

Read completely:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/11-infrastructure-tests-codex-prompt.md
```

Also inspect:

```text
src/AIQuantTradingResearch.Infrastructure/Research/DeterministicObservationSource.cs
src/AIQuantTradingResearch.Infrastructure/AIQuantTradingResearch.Infrastructure.csproj
tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
```

The previous WP11 execution established this blocker:

```text
DeterministicObservationSource is internal.
Infrastructure.Tests has no InternalsVisibleTo access.
Direct construction required by WP11 is therefore impossible.
```

## Authorized Change

Authorize exactly one production testability boundary:

```csharp
[assembly: InternalsVisibleTo("AIQuantTradingResearch.Infrastructure.Tests")]
```

Implement it using the smallest repository-consistent mechanism.

Preferred direction when no existing convention says otherwise:

```text
src/AIQuantTradingResearch.Infrastructure/Properties/AssemblyInfo.cs
```

containing only the required `InternalsVisibleTo` declaration and necessary import.

Do not make `DeterministicObservationSource` public.

Do not change its constructor or behavior.

Do not modify Application contracts.

Do not modify Domain, Application, or Worker behavior.

Do not modify DI registrations.

Do not add packages or project references.

Do not add any other friend assembly.

## Validation

After the change:

1. Confirm `DeterministicObservationSource` remains `internal sealed`.
2. Confirm only `AIQuantTradingResearch.Infrastructure.Tests` receives friend access.
3. Prove the Infrastructure test project can compile code that directly names and constructs `DeterministicObservationSource`.
   - A temporary compile-only probe is allowed.
   - The probe may construct a minimal `ResearchRequest` only as required by the adapter contract.
   - Remove the probe before completion.
   - Do not create actual WP11 behavioral tests.
4. Run:

```text
dotnet build
dotnet test tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj
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
implement WP11 tests
change DeterministicObservationSource visibility to public
use DI/reflection as a workaround
modify ResearchUseCase
modify Worker
modify Application/Infrastructure registrations
add packages
add project references
modify GitHub planning
stage, commit, push, or create a PR
begin WP12
```

## Expected Output

Return a concise:

```text
WP11 Testability Unblock Execution Report
```

including:

```text
Authorized change
Actual file changed/created
Friend assembly granted
DeterministicObservationSource visibility
Compile-access validation
Build result
Domain.Tests result
Application.Tests result
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

Use `UNBLOCK COMPLETE` only when `Infrastructure.Tests` can directly access `DeterministicObservationSource`, all validation passes, and no WP11 tests were implemented.

If complete, state:

```text
Resume WP11 using the existing authoritative:
docs/roadmap/release-0.9/prompts/11-infrastructure-tests-codex-prompt.md
```

Do not execute WP11 in this run.

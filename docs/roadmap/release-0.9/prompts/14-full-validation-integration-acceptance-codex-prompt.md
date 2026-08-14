# Codex Execution Prompt — Release 0.9 / WP14 Full Validation, Integration & Acceptance

## Purpose

Execute the final Release 0.9 gate:

```text
WP14 — Full Validation, Integration & Acceptance
```

Treat the complete cumulative uncommitted WP01–WP13 working tree as the Release 0.9 release candidate.

This is a read-mostly acceptance gate. Do not implement new features or silently correct defects.

Do not stage, commit, push, create or merge a PR, close issues, mutate the milestone, or otherwise perform Git/GitHub integration in this run.

## Authorities

Read completely:

```text
docs/roadmap/release-0.9/RELEASE_0.9_EXECUTION_PLAN.md
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
docs/roadmap/release-0.9/prompts/14-full-validation-integration-acceptance-codex-prompt.md
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

Review all Release 0.9 Codex prompts, prompt-chat reference artifacts, and WP10/WP11 unblock prompts under:

```text
docs/roadmap/release-0.9/prompts/
```

Review relevant current architecture authority:

```text
docs/architecture/solution/SOLUTION_ARCHITECTURE.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/TESTING_STRATEGY.md
```

Inspect all cumulative Release 0.9 production, test, architecture, documentation, governance, and testability-boundary changes.

## Accepted Release 0.9 Architecture

Validate from project files and solution state:

```text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application + Infrastructure
Cycles          -> 0
```

Worker must have no direct Domain project reference.

Distinguish transitive visibility of Domain values through Application contracts from a direct project dependency.

## Accepted Research Slice

Validate the implemented deterministic vertical slice:

```text
Worker
  ↓
IResearchUseCase
  ↓
ResearchUseCase
  ↓
IObservationSource
  ↓
DeterministicObservationSource
  ↓
Domain observation / series / mean behavior
  ↓
Research outcome
```

Canonical fixture:

```text
Target: SAMPLE-USD
Count: 3

2024-01-01T00:00:00+00:00 -> 100.00
2024-01-02T00:00:00+00:00 -> 110.00
2024-01-03T00:00:00+00:00 -> 120.00

Expected mean: 110.00
```

Release 0.9 must remain deterministic, offline, bounded, research-oriented, single-process, and non-production.

No unauthorized live market data, HTTP providers, persistence, databases, brokers, plugin implementation, AI/ML models, trading execution, cloud deployment, secrets, or production scheduling may have entered the release.

## Accepted Testability Boundaries

Validate exactly the intended narrow friend relationships:

```text
Application
  InternalsVisibleTo("AIQuantTradingResearch.Application.Tests")

Infrastructure
  InternalsVisibleTo("AIQuantTradingResearch.Infrastructure.Tests")
```

Confirm no additional unauthorized friend assembly exists.

Confirm concrete research implementations remain non-public.

## Accepted Test Baseline

Expected entering WP14:

```text
Domain.Tests           11
Application.Tests      12
Infrastructure.Tests    9
Architecture.Tests      9
Total                  41
```

Treat counts as release-candidate evidence, not permanent architecture. Investigate any difference rather than blindly failing on count alone.

## Architecture Enforcement

Confirm executable coverage for the accepted Release 0.9 rules, including:

```text
forbidden production dependency directions
production graph acyclicity
Application ownership of IObservationSource
non-public concrete Application/Infrastructure research implementations
```

Do not require brittle rules deliberately rejected in WP12.

## Documentation Acceptance

Verify the WP13-aligned documents match actual implementation regarding:

```text
dependency graph
layer responsibilities
research execution flow
public contracts
internal implementations
DI/composition
Worker one-shot behavior
test responsibilities
architecture enforcement
friend-assembly testability
bounded Release 0.9 scope
```

Check changed Markdown local links when feasible.

## Working-Tree Classification

Capture before validation:

```text
git rev-parse --show-toplevel
git branch --show-current
git rev-parse HEAD
git rev-parse origin/main
git rev-list --left-right --count origin/main...HEAD
git status --short
git diff --stat
git diff --name-status
git diff --cached -- .
```

Inspect untracked files too.

Classify every changed/untracked file as:

```text
AUTHORIZED RELEASE 0.9
AUTHORIZED PROMPT/CHAT GOVERNANCE
AUTHORIZED TESTABILITY UNBLOCK
PRE-EXISTING / OUTSIDE RELEASE
UNEXPECTED
```

The established `*-codex-prompt-chat.md` files are intentional governance/reference artifacts and must not be treated as accidental merely because they contain saved chat/bootstrap material.

Any genuinely `UNEXPECTED` file must be investigated and may block acceptance.

Do not delete unexpected files automatically.

## File Manifest Reconciliation

Reconcile the complete actual delta against:

```text
docs/roadmap/release-0.9/RELEASE_0.9_FILE_MANIFEST.md
```

For every expected artifact determine:

```text
present
missing
modified as expected
not applicable
```

For every actual changed/untracked file identify its authority.

No unexplained changed file is allowed in an accepted release candidate.

## Full Scope Matrix

Evaluate at minimum:

| Area | Authorized Outcome | Actual State | Result |
| --- | --- | --- | --- |
| Research domain model | Release 0.9 bounded model | | |
| Application contracts | Public research contracts/port | | |
| Application use case | Internal orchestration | | |
| Infrastructure adapter | Internal deterministic source | | |
| Dependency registration | Approved composition | | |
| Worker execution | One-shot research execution | | |
| Domain tests | Domain behavior | | |
| Application tests | Orchestration | | |
| Infrastructure tests | Adapter behavior | | |
| Architecture evolution | Stable structural rules | | |
| Documentation alignment | Implemented truth | | |
| Testability boundaries | Two narrow friend assemblies | | |
| Engineering validation | Green | | |
| Git/GitHub mutation | None during WP14 | | |

## Production Review

Inspect all Release 0.9 production changes for:

```text
scope compliance
obvious correctness defects
accidental complexity
future-scope leakage
unexpected public surface
TODO/FIXME/HACK markers
network/filesystem/database dependencies
nondeterminism
credentials/secrets
```

Do not redesign correct code.

If a mandatory defect is found, report and block instead of silently fixing it.

## Test Review

Inspect Release 0.9 tests for:

```text
meaningful assertions
determinism
correct layer ownership
no network/time/randomness
no inappropriate DI/reflection workaround
canonical fixture protection
alternate-data protection where required
narrow use of friend-assembly access
```

## Repository Hygiene

Perform a narrow hygiene/security review over the Release 0.9 delta for obvious:

```text
credentials/tokens
connection strings
private keys
machine-specific runtime secrets
tracked bin/obj
coverage output
temporary probes
backup files
unexpected generated artifacts
```

Never print secret values. Report only safe path/classification information.

Historical absolute paths inside prompt-chat reference material are not automatically runtime configuration defects.

## Validation Commands

Run restore:

```text
dotnet restore AIQuantTradingResearch.slnx
```

Run repository-authorized format verification.

Run build:

```text
dotnet build AIQuantTradingResearch.slnx --no-restore
```

Require zero errors.

Run each suite directly:

```text
dotnet test tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj --no-build
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build
```

Require all discovered tests to pass.

Run canonical verification:

```text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Require exit 0.

Known `NU1900` vulnerability-feed connectivity warnings may remain non-blocking when restore/build succeed.

## Worker Acceptance Run

Run the Worker using the repository-supported command.

Capture:

```text
exit code
target
requested count
meaningful result/output
mean
```

Expected behavior must be consistent with:

```text
SAMPLE-USD
3 observations
110.00 mean
```

Do not modify code to manufacture the result.

## Final Diff Validation

Run:

```text
git diff --check
git status --short
git diff --stat
git diff --name-status
git diff --cached -- .
```

Validation must not create unexpected persistent artifacts.

## Mutation Policy

Persistent WP14 mutation expected:

```text
none
```

Do not edit files merely to make acceptance pass.

If a mandatory defect, authority conflict, unexplained file, or failed validation is found, stop and report it.

## Findings Classification

Use only:

```text
BLOCKER
REQUIRED ACTION
RISK
OBSERVATION
```

`BLOCKER` and unresolved mandatory `REQUIRED ACTION` findings prevent clean acceptance.

## Decision

Finish with exactly one:

```text
RELEASE 0.9 ACCEPTED
RELEASE 0.9 ACCEPTED WITH ACTIONS
RELEASE 0.9 ACCEPTANCE BLOCKED
```

Use `RELEASE 0.9 ACCEPTED` only when:

```text
the complete cumulative delta is understood
manifest reconciliation passes
scope is compliant
architecture is compliant
testability boundaries are narrow
production review is clean
tests are correctly layered and green
documentation matches implementation
hygiene is acceptable
restore/format/build pass
all direct tests pass
eng/verify.ps1 passes
Worker acceptance run passes
git diff --check passes
no unexpected persistent validation artifact exists
the release candidate is ready for Git/GitHub integration
```

## Required Execution Report

Return:

```text
Release 0.9 WP14 Full Validation, Integration & Acceptance Report
```

with these sections:

1. Executive Summary
2. Repository Context
3. Authorities Reviewed
4. Cumulative Working-Tree Classification
5. File Manifest Reconciliation
6. Release 0.9 Scope Matrix
7. Production Architecture Validation
8. Testability Boundary Validation
9. Production Code Review
10. Test Review
11. Architecture Enforcement Review
12. Documentation Acceptance Review
13. Repository Hygiene / Security Review
14. Restore
15. Format Verification
16. Build
17. Direct Test Results
18. Canonical Verification
19. Worker Acceptance Run
20. Diff / Whitespace Validation
21. Final Git State
22. Validation Evidence Matrix
23. Findings
24. Acceptance Criteria Matrix
25. Release Readiness Assessment
26. Final Decision
27. Next Authorized Action

In the readiness assessment explicitly answer:

```text
Is Release 0.9 technically coherent?
Is Release 0.9 scope compliant?
Is Release 0.9 architecture compliant?
Is Release 0.9 fully tested?
Is Release 0.9 documentation aligned?
Is Release 0.9 repository state understood?
Is Release 0.9 ready for Git/GitHub integration?
```

If and only if acceptance permits progression, state:

```text
Release 0.9 is ready for a separately authorized Git/GitHub integration step.
```

Do not perform that integration.

## Acceptance Criteria

Before returning `RELEASE 0.9 ACCEPTED`, confirm:

- [ ] All required authorities were read.
- [ ] WP13 `DOCUMENTATION ALIGNMENT COMPLETE` baseline is confirmed.
- [ ] Repository root, branch, HEAD, origin/main, and ahead/behind are captured.
- [ ] Every changed/untracked file is classified.
- [ ] Prompt-chat artifacts are recognized as intentional where applicable.
- [ ] File manifest is reconciled.
- [ ] Full cumulative diff is reviewed.
- [ ] Release scope matrix passes.
- [ ] Production graph matches authority.
- [ ] Cycles are zero.
- [ ] No unauthorized project/reference exists.
- [ ] Exactly the authorized friend-assembly boundaries exist.
- [ ] Concrete research implementations remain non-public.
- [ ] Production review finds no mandatory defect or future-scope leakage.
- [ ] Tests are deterministic and correctly layered.
- [ ] Architecture enforcement matches accepted WP12 outcome.
- [ ] Documentation matches implementation.
- [ ] No stale contradictory current-state claim remains in WP13 scope.
- [ ] Hygiene/security review is acceptable.
- [ ] No temporary testability probe remains.
- [ ] Restore passes.
- [ ] Format verification passes.
- [ ] Build passes with zero errors.
- [ ] Domain.Tests pass.
- [ ] Application.Tests pass.
- [ ] Infrastructure.Tests pass.
- [ ] Architecture.Tests pass.
- [ ] Canonical verification passes.
- [ ] Worker acceptance run passes with canonical behavior.
- [ ] `git diff --check` passes.
- [ ] Final Git state contains no unexpected validation artifacts.
- [ ] Nothing is staged by WP14.
- [ ] No commit/push/PR/merge occurred.
- [ ] No GitHub issue/milestone mutation occurred.
- [ ] Git/GitHub integration was not performed.
- [ ] Final decision is evidence-based.

# Conclusion

WP14 is the Release 0.9 acceptance gate:

```text
WP01–WP13 cumulative delta
        ↓
Manifest reconciliation
        ↓
Scope + architecture review
        ↓
Production + test review
        ↓
Documentation + hygiene review
        ↓
Restore / format / build
        ↓
Direct test suites
        ↓
Canonical verification
        ↓
Worker canonical acceptance
        ↓
Final Git inspection
        ↓
RELEASE 0.9 ACCEPTED
        ↓
Separately authorized Git/GitHub integration
```

> Acceptance is evidence that the complete Release 0.9 delta is coherent, bounded, reproducible, and ready to integrate—not another implementation step.

# Release 1.0 WP14 --- Architecture Evolution --- Codex Prompt

## Role

Act as the **WP14 Architecture Evolution Executor** for Release 1.0 of
`AIQuantTradingResearch`.

WP14 begins only after the accepted completion of WP12 and WP13.

The accepted WP13 result establishes:

``` text
Domain.Tests: 11/11
Application.Tests: 16/16
Infrastructure.Tests: 65/65
Architecture.Tests: 9/9
Total permanent tests: 101/101

eng/verify.ps1: PASS
Build warnings/errors: 0/0
WP13 production changes: 0
WP13 blocker B13-01: resolved
WP14 issue: #99
WP14 implementation: not started
```

WP14 exists to evolve **executable architecture enforcement** so that
the Release 1.0 Market Data Foundation boundaries proven by WP02--WP13
are protected by architecture tests.

This is an architecture-enforcement work package, not a production
refactoring package.

Do not begin WP15.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before mutation:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md

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
docs/roadmap/release-1.0/prompts/12-domain-application-tests-codex-prompt.md
docs/roadmap/release-1.0/prompts/13-infrastructure-provider-tests-codex-prompt.md
docs/roadmap/release-1.0/prompts/13-infrastructure-provider-tests-di-unblock-codex-prompt.md
docs/roadmap/release-1.0/prompts/14-architecture-evolution-codex-prompt.md
```

Read the accepted WP13 execution report from the current context.

Inspect all current Architecture.Tests source completely.

Inspect the current production projects and references completely enough
to prove repository truth:

``` text
src/AIQuantTradingResearch.Domain/
src/AIQuantTradingResearch.Application/
src/AIQuantTradingResearch.Infrastructure/
src/AIQuantTradingResearch.Worker/

tests/AIQuantTradingResearch.Architecture.Tests/
```

Inspect:

``` text
AIQuantTradingResearch.slnx
Directory.Build.props
Directory.Packages.props
```

Inspect relevant project files for actual project references.

Inspect GitHub issue #99 read-only if available.

Authority precedence:

1.  `RELEASE_1.0_EXECUTION_PLAN.md`
2.  `RELEASE_1.0_FILE_MANIFEST.md`
3.  This WP14 prompt
4.  Accepted WP03--WP13 repository truth
5.  Existing executable architecture conventions
6.  Existing architecture documentation

If documentation conflicts with implemented repository truth, do not
silently rewrite production to match stale documentation. Record the gap
for WP15.

------------------------------------------------------------------------

## 2. WP13 Predecessor Gate

Before mutation prove:

``` text
WP13 final decision = COMPLETE or COMPLETE WITH OBSERVATIONS
B13-01 = resolved
Infrastructure.Tests = 65/65
Architecture.Tests = 9/9
total permanent tests = 101/101
eng/verify.ps1 = PASS
WP14 implementation was not started
```

If WP13 is not accepted, stop.

------------------------------------------------------------------------

## 3. Objective

Evolve Architecture.Tests from the Release 0.9 baseline so Release 1.0's
implemented market-data architecture becomes executable policy.

WP14 must protect **actual architectural boundaries**, especially:

``` text
Domain independence
Application independence from Infrastructure/Worker
Infrastructure independence from Worker
accepted production graph
production graph acyclicity
Application ownership of provider-independent acquisition contracts
provider-specific implementation confinement to Infrastructure
provider-specific transport/HTTP mechanics confinement to Infrastructure
Worker composition-root role
non-public concrete implementation boundaries where already authoritative
```

Do not invent architecture rules merely because they sound desirable.

Every new rule must be supported by:

``` text
Release 1.0 authority
AND
implemented repository truth
AND
a stable architectural boundary worth enforcing
```

------------------------------------------------------------------------

## 4. Preserve Existing Release 0.9 Architecture Enforcement

The existing suite previously enforced nine rules, including:

``` text
six forbidden production dependency directions
production graph acyclicity
Application ownership of IObservationSource
non-public concrete Application/Infrastructure implementations
```

Read the actual current tests and identify the exact nine rules before
changing anything.

Do not delete, weaken, replace, or silently reinterpret an existing
valid rule.

If any existing rule no longer matches accepted Release 1.0
architecture, stop and report the conflict rather than weakening it.

------------------------------------------------------------------------

## 5. Establish Current Production Graph

Derive the graph from project references, not assumptions.

Expected accepted graph:

``` text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Expected forbidden direct production directions:

``` text
Domain → Application
Domain → Infrastructure
Domain → Worker

Application → Infrastructure
Application → Worker

Infrastructure → Worker
```

Prove:

``` text
cycles = 0
solution production projects = 4
```

Do not add project references.

------------------------------------------------------------------------

## 6. Release 1.0 Architectural Boundary Inventory

Before adding tests, create an evidence matrix covering at least:

  ----------------------------------------------------------------------------
  Boundary               Repository        Existing test?    New test
                         evidence                            justified?
  ---------------------- ----------------- ----------------- -----------------
  Domain has no          project/source    yes/no            yes/no
  outer-layer dependency truth

  Application owns       source truth      yes/no            yes/no
  `IObservationSource`

  Application owns       source truth      yes/no            yes/no
  provider-independent
  failures/contracts

  Twelve Data types      source truth      yes/no            yes/no
  remain
  Infrastructure-owned

  HTTP mechanics remain  source truth      yes/no            yes/no
  Infrastructure-owned

  Worker is composition  project/source    yes/no            yes/no
  root                   truth

  Production graph       project graph     yes/no            yes/no
  acyclic

  Concrete               source truth      yes/no            yes/no
  implementations retain
  intended visibility
  ----------------------------------------------------------------------------

Use this matrix to determine the minimum architecture-test delta.

------------------------------------------------------------------------

## 7. Provider-Independence Enforcement

Release 1.0 selected Twelve Data only for the Infrastructure boundary.

Add executable enforcement proving that provider-specific concepts do
not leak into provider-independent layers.

At minimum evaluate whether stable tests can prove:

``` text
Domain contains no Twelve Data-specific type/reference
Application contains no Twelve Data-specific type/reference
Domain contains no provider HTTP mechanics
Application contains no provider HTTP mechanics
```

Prefer structural/type/assembly dependency tests over brittle raw-text
scans.

If the current architecture-test technology cannot express a stable rule
without adding a package or building a fragile source-string test, do
not improvise. Report the limitation.

Do not add a package unless explicitly required by the Release 1.0
manifest/authority.

------------------------------------------------------------------------

## 8. Contract Ownership Enforcement

The accepted architecture keeps provider-independent acquisition
contracts in Application.

Evaluate and, where stable, enforce:

``` text
IObservationSource is owned by Application
ObservationSourceResult is owned by Application
ObservationSourceFailure is owned by Application
ResearchFailure remains provider-independent
```

Do not require Infrastructure implementation names in Application.

Do not make contracts public/internal solely to satisfy a test.

------------------------------------------------------------------------

## 9. Provider Implementation Confinement

Evaluate stable executable rules proving Twelve Data implementation
types remain in Infrastructure.

Relevant implemented concepts may include:

``` text
TwelveDataClient
TwelveDataObservationSource
TwelveDataTimeSeriesNormalizer
TwelveDataConfiguration
Twelve Data transport DTOs/results/failures
```

The architectural intent is:

``` text
provider-specific implementation → Infrastructure
provider-independent contract → Application
Domain → provider agnostic
Worker → composition/configuration only
```

Do not enforce incidental folder paths if assembly/type ownership
already expresses the boundary.

Do not enforce exact counts of provider-specific types unless count
itself is architectural policy.

------------------------------------------------------------------------

## 10. HTTP / Transport Confinement

Evaluate executable enforcement that provider HTTP/transport mechanics
do not enter Domain or Application.

Architectural concepts include:

``` text
HttpClient
HTTP status handling
provider request construction
provider DTOs
JSON transport representation
provider authentication mechanics
```

Prefer assembly/type dependency evidence.

Avoid broad bans on BCL namespaces that could reject legitimate future
provider-independent code without architectural justification.

------------------------------------------------------------------------

## 11. Worker Boundary

The Worker is the outer composition/execution boundary.

Preserve:

``` text
Worker → Application
Worker → Infrastructure
```

and forbid inner layers from depending on Worker.

Evaluate whether any additional stable rule is justified by Release 1.0
truth, such as preventing Worker-owned provider implementations.

Do not test current `Program.cs` line structure, console wording, or
incidental local-variable choices.

WP11 behavioral execution is not an architecture-test concern.

------------------------------------------------------------------------

## 12. Visibility Enforcement

Inspect existing visibility rules.

Preserve authoritative non-public implementation boundaries already
established for:

``` text
ResearchUseCase
TwelveDataObservationSource
other implementation types only where visibility is explicitly architectural
```

Do not blindly assert that every Infrastructure type must be internal;
public configuration/extension surfaces may be intentional.

Do not alter production visibility to make architecture tests pass.

If current production visibility contradicts authority, report a
blocker.

------------------------------------------------------------------------

## 13. Friend-Assembly Boundary

The existing Infrastructure friend assembly is:

``` text
AIQuantTradingResearch.Infrastructure.Tests
```

Its purpose is testability.

WP14 must not:

``` text
add another friend assembly
make internal provider types public
treat friend-assembly access as public runtime API
```

Architecture.Tests should not require new production friend access
merely to inspect internal types.

Use reflection/assembly metadata only if that is already consistent with
the architecture-test approach and does not require production changes.

------------------------------------------------------------------------

## 14. Architecture-Test Design Principles

New tests must be:

``` text
deterministic
offline
credential-free
provider-call-free
stable across ordinary implementation refactoring
focused on architectural boundaries
clear when failing
```

Avoid:

``` text
network access
provider availability
environment-dependent behavior
source-code formatting assumptions
exact method-body assertions
exact test-count assertions inside production policy
brittle namespace rules without authority
feature implementation tests
duplicating WP12/WP13 behavioral tests
```

------------------------------------------------------------------------

## 15. Minimal Delta

Target the smallest architecture-test change that closes WP14.

Expected production delta:

``` text
0 files
```

Expected test delta:

``` text
Architecture.Tests only
```

Expected package/project-reference delta:

``` text
0
```

If a production change appears necessary, stop and report why.

If a new package appears necessary, stop unless the Release 1.0 manifest
explicitly authorizes it.

------------------------------------------------------------------------

## 16. Candidate Architecture Rules

Do not treat this list as automatic authorization. Reconcile each
candidate against repository truth first.

Potential Release 1.0 rules include:

``` text
A10-01 — Existing six forbidden production dependency directions remain enforced
A10-02 — Production graph remains acyclic
A10-03 — IObservationSource remains Application-owned
A10-04 — Provider-independent observation-source failure contract remains Application-owned
A10-05 — Twelve Data provider types are confined to Infrastructure
A10-06 — Domain remains free of Twelve Data/provider transport dependencies
A10-07 — Application remains free of Twelve Data/provider transport dependencies
A10-08 — Worker remains an outer layer; inner production assemblies do not depend on it
A10-09 — Intended concrete implementations remain non-public
```

Merge candidates with existing tests when they express the same
invariant.

Do not create duplicate tests merely to increase coverage count.

------------------------------------------------------------------------

## 17. Rule-to-Authority Matrix

Before finalizing tests, produce:

  -----------------------------------------------------------------------------
  Rule ID        Architectural   Authority      Repository     Test
                 invariant                      evidence       implementation
  -------------- --------------- -------------- -------------- ----------------

  -----------------------------------------------------------------------------

Every new architecture test must appear in this matrix.

If a proposed rule has no clear authority, omit it.

------------------------------------------------------------------------

## 18. No Behavioral Duplication

WP14 must not retest:

``` text
HTTP request parameter values
API-key header values
JSON deserialization
normalization price/date calculations
HTTP/provider failure mappings
observation counts
cancellation behavior
Worker output text
live DI provider calls
```

Those are already covered by WP12/WP13 or earlier work.

Architecture tests should enforce boundaries, ownership, dependency
direction, and visibility.

------------------------------------------------------------------------

## 19. Architecture Test Count

Baseline:

``` text
Architecture.Tests = 9
```

The final count may increase.

Do not target a predetermined count.

Report:

``` text
baseline
tests added
tests modified
final total
```

Quality and non-duplication matter more than count.

------------------------------------------------------------------------

## 20. Production Leakage Scan

Perform a targeted architecture-oriented inspection and report whether
these occur outside Infrastructure:

``` text
TwelveData
twelvedata
/time_series
exchange_timezone
adjust=splits
provider DTO names
provider transport-result names
provider normalization-result names
```

Interpret results carefully.

Governance documentation/tests may legitimately mention provider names.
The architecture claim concerns production-layer ownership.

Do not rewrite documentation in WP14.

------------------------------------------------------------------------

## 21. Dependency Evidence

After changes, prove the production graph remains exactly:

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
new packages = 0
solution membership unchanged
```

------------------------------------------------------------------------

## 22. Build Validation

Run:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
```

If restore is required because repository state demands it, use the
canonical repository restore path first.

Required:

``` text
exit = 0
errors = 0
```

Report warnings exactly; do not hide them.

------------------------------------------------------------------------

## 23. Architecture Test Validation

Run Architecture.Tests directly.

Required:

``` text
all Architecture.Tests pass
```

Record:

``` text
baseline = 9
added = actual
modified = actual
final = actual
failed = 0
skipped = actual
```

If an architecture rule fails because production violates accepted
authority, do not weaken the test. Stop and report the production
conflict.

------------------------------------------------------------------------

## 24. Full Regression Validation

Run all permanent suites:

``` text
Domain.Tests
Application.Tests
Infrastructure.Tests
Architecture.Tests
```

Accepted pre-WP14 baseline:

``` text
Domain.Tests = 11
Application.Tests = 16
Infrastructure.Tests = 65
Architecture.Tests = 9
Total = 101
```

Record actual final counts.

No existing test may be deleted merely to accommodate WP14.

------------------------------------------------------------------------

## 25. Canonical Verification

Run:

``` text
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
```

Required:

``` text
PASS
```

Record restore, format, build, test, warning/error, and exit evidence.

No network/provider request or credential should be needed by
architecture tests.

------------------------------------------------------------------------

## 26. Diff / Formatting Validation

Run:

``` text
git diff --check
git diff --cached --check
```

Required:

``` text
PASS
staged files = 0
```

Review the complete WP14 diff.

Expected WP14-owned changes:

``` text
tests/AIQuantTradingResearch.Architecture.Tests/**
```

No production source, package manifest, project reference, engineering
script, or GitHub workflow should change.

------------------------------------------------------------------------

## 27. Working-Tree Classification

Preserve the cumulative uncommitted Release 1.0 candidate.

Classify all visible changes as:

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
WP14 AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

Expected:

``` text
staged = 0
unexpected = 0
temporary artifacts = 0
```

Do not discard or normalize unrelated cumulative changes.

------------------------------------------------------------------------

## 28. Documentation Protection

WP15 owns documentation alignment.

Do not modify architecture documentation during WP14.

If current documentation is stale relative to the executable
architecture, record exact gaps in the WP14 report for WP15.

Do not claim documentation alignment is complete.

------------------------------------------------------------------------

## 29. Git / GitHub Protection

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
close or edit issue #99
modify milestone #41
modify Project fields/status
create tags/releases
create Release 1.1 planning
```

GitHub inspection, if used, is read-only.

------------------------------------------------------------------------

## 30. Scope Protection

WP14 must not begin:

``` text
WP15 — Documentation Alignment
WP16 — Full Validation, Integration & Acceptance
Release 1.0 Git/GitHub integration
Release 1.0 closure
Release 1.1
```

Do not implement:

``` text
storage
caching
streaming
retry/fallback frameworks
multi-provider support
AI/ML
plugins
production hosting expansion
new market-data capabilities
```

------------------------------------------------------------------------

## 31. Findings Classification

Classify findings as:

``` text
OBSERVATION
BLOCKER
```

A blocker includes:

``` text
accepted architecture violated by production
required architecture rule cannot be enforced without unauthorized production/package/reference change
existing valid architecture rule must be weakened to pass
unexpected repository mutation
canonical verification failure attributable to WP14
```

Do not downgrade blockers to observations.

------------------------------------------------------------------------

## 32. Exit Criteria

WP14 completes only if:

``` text
WP13 predecessor gate = PASS

existing architecture rules preserved
existing valid rules weakened = 0

current production graph proven
cycles = 0

Release 1.0 architectural boundaries inventoried
new rules have explicit authority
new rules have repository evidence
duplicate/incidental rules = 0

provider-specific implementation remains Infrastructure-owned
Domain provider-specific leakage = 0
Application provider-specific leakage = 0
Application ownership of provider-independent acquisition contract preserved
inner-layer dependency on Worker = 0

production files changed by WP14 = 0
production visibility changes = 0
friend-assembly additions = 0
new packages = 0
new project references = 0
solution membership changes = 0

Architecture.Tests = PASS
Domain.Tests = PASS
Application.Tests = PASS
Infrastructure.Tests = PASS
eng/verify.ps1 = PASS
build errors = 0
git diff --check = PASS
git diff --cached --check = PASS

staged files = 0
temporary artifacts = 0
unexpected mutations = 0

WP15 started = NO
WP16 started = NO
Release 1.1 started = NO
```

------------------------------------------------------------------------

## 33. Required Execution Report

Return:

``` text
# Release 1.0 WP14 — Architecture Evolution Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP13 Predecessor Gate
## 5. Existing Architecture-Test Baseline
## 6. Current Production Dependency Graph
## 7. Release 1.0 Boundary Inventory
## 8. Existing Rule Preservation
## 9. Architecture Gap Analysis
## 10. Rule-to-Authority Matrix
## 11. Provider-Independence Enforcement
## 12. Contract-Ownership Enforcement
## 13. Provider-Confinement Enforcement
## 14. HTTP / Transport Confinement
## 15. Worker-Boundary Enforcement
## 16. Visibility Enforcement
## 17. Friend-Assembly Protection
## 18. Architecture Tests Added or Modified
## 19. Files Changed
## 20. Production-Code Preservation
## 21. Package / Project / Solution Preservation
## 22. Provider Leakage Scan
## 23. Dependency / Cycle Evidence
## 24. Architecture Test Count Delta
## 25. Architecture Test Evidence
## 26. Full Regression Test Evidence
## 27. Build Evidence
## 28. Canonical Verification
## 29. Diff / Formatting Validation
## 30. Working-Tree Classification
## 31. Documentation Gaps Deferred to WP15
## 32. Git / GitHub Protection
## 33. Scope Protection
## 34. Findings / Observations
## 35. Exit-Criteria Assessment
## 36. Final Repository State
## 37. Final Decision
## 38. Next Authorized Action
```

Be precise about which architecture rules existed before WP14 and which
were added by WP14.

Do not claim enforcement that is not actually executable.

------------------------------------------------------------------------

## 34. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP14 ARCHITECTURE EVOLUTION COMPLETE
RELEASE 1.0 WP14 ARCHITECTURE EVOLUTION COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP14 ARCHITECTURE EVOLUTION BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when every mandatory exit
criterion passes.

------------------------------------------------------------------------

## 35. Next Authorized Action

If WP14 completes successfully, the next separately authorized work
package is:

``` text
WP15 — Documentation Alignment
GitHub issue #100
```

Do not start WP15.

A separate human-authorized WP15 Codex prompt is required.

------------------------------------------------------------------------

## Execution Instruction

Read all Release 1.0 authorities and the accepted WP13 result; prove the
WP13 gate; inspect the complete existing Architecture.Tests suite and
actual production project/type boundaries; reconstruct the current
dependency graph; inventory Release 1.0 architectural invariants;
preserve all valid Release 0.9 rules; add only the minimum deterministic
executable architecture tests justified by Release 1.0 authority and
repository truth, especially provider independence, Application contract
ownership, Infrastructure provider confinement, inner-layer independence
from Worker, acyclicity, and intended visibility; do not modify
production code, packages, project references, solution membership,
documentation, or Git/GitHub state; run Architecture.Tests, all
permanent suites, build, canonical verification,
leakage/dependency/diff/working-tree checks; return the complete WP14
execution report; and stop before WP15.

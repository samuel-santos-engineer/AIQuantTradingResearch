# Release 1.0 WP12 --- Domain & Application Tests --- Codex Prompt

## Role

Act as the **WP12 Domain & Application Tests Executor** for Release 1.0
of `AIQuantTradingResearch`.

WP12 owns permanent behavioral coverage for the provider-independent
Domain and Application behavior established by the cumulative Release
1.0 implementation. It must test the accepted contracts and behavior
without pulling Twelve Data transport, normalization,
Infrastructure-provider testing, Worker execution, or architecture
evolution into this package.

WP11 is accepted. WP13 has not started.

Do not start WP13.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before mutation:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md

docs/roadmap/release-1.0/prompts/03-market-data-domain-evolution-codex-prompt.md
docs/roadmap/release-1.0/prompts/04-market-data-application-contracts-codex-prompt.md
docs/roadmap/release-1.0/prompts/05-historical-market-data-use-case-integration-codex-prompt.md
docs/roadmap/release-1.0/prompts/09-market-data-validation-failure-mapping-codex-prompt.md
docs/roadmap/release-1.0/prompts/10-dependency-registration-configuration-codex-prompt.md
docs/roadmap/release-1.0/prompts/11-worker-market-data-execution-codex-prompt.md
docs/roadmap/release-1.0/prompts/12-domain-application-tests-codex-prompt.md
```

Read the accepted WP03--WP11 execution results available in the current
context, especially:

``` text
WP03 = zero Domain production delta
WP04 = expanded provider-independent failure vocabulary
WP05 = complete source-failure → research-failure propagation
WP09 = provider outcomes mapped into Application source failures
WP11 = Worker integrated without changing Domain/Application behavior
```

Read GitHub issues:

``` text
#97 — WP12 Domain & Application Tests
#98 — WP13 Infrastructure & Provider Tests
#99 — WP14 Architecture Evolution
#100 — WP15 Documentation Alignment
```

Inspect repository truth in:

``` text
src/AIQuantTradingResearch.Domain/
src/AIQuantTradingResearch.Application/
tests/AIQuantTradingResearch.Domain.Tests/
tests/AIQuantTradingResearch.Application.Tests/
tests/AIQuantTradingResearch.Architecture.Tests/
```

Authority precedence:

1.  `RELEASE_1.0_EXECUTION_PLAN.md`
2.  `RELEASE_1.0_FILE_MANIFEST.md`
3.  Accepted cumulative WP03--WP11 implementation
4.  GitHub issue #97
5.  Existing repository test conventions
6.  This prompt

If these authorities materially conflict, stop and return `BLOCKED`.

------------------------------------------------------------------------

## 2. WP11 Predecessor Gate

Before mutation prove:

``` text
WP01–WP11 = complete
WP03 Domain production delta = 0
WP04 Application failure contracts exist
WP05 ResearchUseCase handles all authorized source failures
WP09 Infrastructure maps provider outcomes into those source failures
WP10/WP11 composition changes do not alter Domain/Application contracts
Domain.Tests baseline = 11 passing
Application.Tests baseline = 12 passing
Architecture.Tests baseline = 9 passing
WP12 issue = #97
WP13 not started
```

Verify actual current counts and behavior from repository execution. Do
not rely solely on prior reports.

------------------------------------------------------------------------

## 3. Objective

Add the minimum permanent Domain and Application tests required to make
Release 1.0 provider-independent behavior executable and
regression-protected.

WP12 must prove:

1.  existing Domain invariants/calculations remain valid under the
    Release 1.0 slice;
2.  Application request validation remains deterministic;
3.  the successful `ResearchUseCase` path remains correct with a
    test-owned `IObservationSource`;
4.  every authorized `ObservationSourceFailure` maps one-to-one to the
    corresponding `ResearchFailure`;
5.  invalid requests do not call the source;
6.  provider-specific concepts do not enter Domain/Application tests;
7.  no Infrastructure/provider mechanics are tested here.

Use permanent tests, not temporary probes, for behavior assigned to
WP12.

------------------------------------------------------------------------

## 4. Scope Boundary

WP12 owns:

``` text
Domain.Tests
Application.Tests
```

WP12 does not own:

``` text
TwelveDataClient tests
HTTP tests
JSON/DTO tests
normalizer tests
timezone/provider parsing tests
TwelveDataObservationSource tests
DI/configuration tests
Worker tests
architecture-rule additions
documentation
```

Those belong to other work packages, primarily WP13--WP15.

------------------------------------------------------------------------

## 5. Domain Test Strategy

WP03 intentionally introduced no Domain production change.

Therefore do **not** manufacture Domain changes merely to justify WP12.

Inspect the existing 11 Domain tests and the Release 1.0 execution plan.
Add Domain tests only where Release 1.0 authority identifies a real
behavioral gap in existing Domain values used by the market-data slice.

Expected Domain subjects may include existing:

``` text
PriceObservation
ObservationSeries
MeanPrice
```

Use repository truth for exact names and invariants.

If the existing 11 tests already exhaustively satisfy the Release 1.0
Domain acceptance requirements, an accepted **zero Domain-test delta**
is valid. Prove it explicitly.

------------------------------------------------------------------------

## 6. Application Test Strategy

Application.Tests must permanently cover the Release 1.0 Application
behavior introduced/evolved by WP04 and WP05.

Use a test-owned fake/stub implementation of `IObservationSource`.

Do not use Infrastructure.

Do not use a mocking package unless one already exists and repository
convention clearly requires it. Prefer the existing lightweight
test-double pattern.

------------------------------------------------------------------------

## 7. Required ResearchUseCase Success Coverage

Prove the successful path using deterministic test-owned observations:

``` text
ResearchRequest
    ↓
IObservationSource test double
    ↓
PriceObservation values
    ↓
ObservationSeries
    ↓
MeanPrice
    ↓
ResearchResult
```

Assert only stable provider-independent behavior, including the relevant
existing result fields.

Do not assert Twelve Data details.

------------------------------------------------------------------------

## 8. Required Request-Validation Coverage

Permanently prove the existing request rules, including at minimum the
repository-authoritative equivalents of:

``` text
blank/whitespace target → InvalidRequest
non-positive requested count → InvalidRequest
invalid request → source not invoked
```

Do not invent new validation rules.

If these are already permanently covered, retain them and report the
coverage rather than duplicating tests.

------------------------------------------------------------------------

## 9. Required Failure-Mapping Coverage

Every authorized source failure must have permanent Application
coverage:

  ObservationSourceFailure     Expected ResearchFailure
  ---------------------------- ----------------------------
  `UnsupportedTarget`          `UnsupportedTarget`
  `InsufficientObservations`   `InsufficientObservations`
  `SourceUnavailable`          `SourceUnavailable`
  `AccessDenied`               `AccessDenied`
  `UsageLimitReached`          `UsageLimitReached`
  `InvalidSourceResponse`      `InvalidSourceResponse`

Use actual repository names.

The test suite must fail if a mapping is removed, swapped, or falls into
the defensive unknown-failure branch.

Prefer a compact parameterized/theory test if consistent with the
existing test framework and style.

------------------------------------------------------------------------

## 10. Defensive Unknown-Failure Behavior

Inspect `ResearchUseCase`.

If it contains a defensive branch for an invalid/future enum value and
repository conventions permit deterministic testing of that branch
without weakening production visibility or using undefined behavior,
test it.

Otherwise do not force coverage by changing production code.

Report the decision.

------------------------------------------------------------------------

## 11. Source Invocation Semantics

Prove:

``` text
valid request → source invoked exactly once
invalid request → source invoked zero times
```

Do not introduce a mocking framework solely to count calls. A simple
test double with an invocation counter is sufficient.

If exact-once semantics are already covered, do not duplicate
unnecessarily.

------------------------------------------------------------------------

## 12. Observation Count / Result Semantics

Assert the accepted Application result behavior only.

Do not add requested-count truncation, sorting, provider validation, or
no-observation policy to Application tests unless those behaviors are
actually Application-owned in repository truth.

Provider normalization and source validation belong to Infrastructure
and WP13.

------------------------------------------------------------------------

## 13. Cancellation

Do not invent asynchronous/cancellation tests if the Application
contract is synchronous.

If the current `IResearchUseCase` or `IObservationSource` contract
exposes cancellation, test only Application-owned propagation semantics
supported by repository truth.

Do not test provider cancellation behavior; that belongs to WP13.

------------------------------------------------------------------------

## 14. Test Naming and Structure

Follow existing repository conventions for:

``` text
namespace
test class naming
method naming
Arrange / Act / Assert style
xUnit attributes
test doubles
file placement
```

Do not reorganize unrelated tests.

Do not introduce a new testing style across the repository.

------------------------------------------------------------------------

## 15. Production-Code Protection

Target:

``` text
Domain production changes = 0
Application production changes = 0
Infrastructure production changes = 0
Worker production changes = 0
```

WP12 is a test package.

If a test exposes a genuine production defect, stop and report it as a
blocker unless the Release 1.0 manifest explicitly authorizes the narrow
correction within WP12.

Do not silently fix production code.

------------------------------------------------------------------------

## 16. Visibility / Testability Protection

Release 0.9 already established the Application friend-assembly boundary
where needed.

Do not:

``` text
make internal production types public
add another InternalsVisibleTo
broaden API visibility
use reflection to bypass intended boundaries
```

Use the existing authorized testability seam.

If current test access is insufficient, stop as `BLOCKED` and identify
the minimum separate unblock required.

------------------------------------------------------------------------

## 17. Infrastructure Exclusion

Domain.Tests and Application.Tests must contain zero provider-specific
dependencies.

Target scans:

``` text
TwelveData references = 0
HttpClient references = 0
HTTP status-code handling = 0
JSON serializer references = 0
provider DTO references = 0
/time_series references = 0
exchange_timezone references = 0
adjust=splits references = 0
API-key/configuration references = 0
```

Application tests may implement only `IObservationSource`, not
`TwelveDataObservationSource`.

------------------------------------------------------------------------

## 18. Package / Project Protection

Maintain:

``` text
new packages = 0
new project references = 0
solution membership changes = 0
```

Use the existing test framework and assertion libraries.

Do not add mocking, fixture, snapshot, HTTP, or data-generation
packages.

------------------------------------------------------------------------

## 19. Authorized Files

`RELEASE_1.0_FILE_MANIFEST.md` is exact authority.

Create/modify only WP12-authorized test files.

Do not infer authorization from this prompt.

If the necessary permanent test file is outside the manifest, stop as
`BLOCKED`.

------------------------------------------------------------------------

## 20. Existing Test Preservation

Preserve all accepted Release 0.9 tests.

Do not delete or weaken existing assertions to make new tests pass.

If existing tests need only a minimal data/name update because an
accepted Release 1.0 contract intentionally evolved, verify that the
manifest authorizes it and report the exact reason.

------------------------------------------------------------------------

## 21. Domain Coverage Reconciliation

Create a Domain coverage matrix:

  Domain behavior                        Existing coverage   WP12 delta Result
  ------------------------------------ ------------------- ------------ -----------
  `PriceObservation` invariants                     actual       actual PASS/FAIL
  `ObservationSeries` invariants                    actual       actual PASS/FAIL
  `MeanPrice` calculation/invariants                actual       actual PASS/FAIL

Add rows for other Release 1.0-relevant Domain behavior if repository
truth requires it.

Do not use code-coverage percentage as a substitute for behavioral
evidence.

------------------------------------------------------------------------

## 22. Application Coverage Reconciliation

Create an Application coverage matrix including:

``` text
valid success path
blank target
whitespace target if distinct
zero requested count
negative requested count if distinct
source not called for invalid request
source called once for valid request
UnsupportedTarget mapping
InsufficientObservations mapping
SourceUnavailable mapping
AccessDenied mapping
UsageLimitReached mapping
InvalidSourceResponse mapping
defensive unknown failure — tested or explicitly not forced
```

Report whether each case was pre-existing or added by WP12.

------------------------------------------------------------------------

## 23. Test Double Design

Any test-owned `IObservationSource` double must be:

``` text
small
deterministic
provider-independent
network-free
credential-free
owned by Application.Tests
```

It may expose only the minimal state needed to configure
observations/failure and count invocations.

Do not create a reusable production fake.

------------------------------------------------------------------------

## 24. Determinism

Tests must not depend on:

``` text
network
Twelve Data
credentials
current clock
machine timezone
current culture unless explicitly controlled
randomness
test execution order
filesystem state
```

All test data must be deterministic.

------------------------------------------------------------------------

## 25. Architecture Preservation

Production graph remains:

``` text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Test project dependencies must remain within existing project
references.

Run Architecture.Tests but do not add WP14 architecture rules.

------------------------------------------------------------------------

## 26. WP03--WP11 Regression Protection

Prove that WP12 does not change accepted behavior from:

``` text
WP03 zero Domain delta
WP04 failure vocabulary
WP05 one-to-one failure propagation
WP06 transport model
WP07 HTTP/authentication/adjustment behavior
WP08 normalization
WP09 validation/failure mapping
WP10 configured runtime graph
WP11 Worker configuration handoff/execution
```

WP12 tests should strengthen confidence without changing these
implementations.

------------------------------------------------------------------------

## 27. Test Counts

Record:

``` text
baseline Domain.Tests count
final Domain.Tests count
Domain tests added

baseline Application.Tests count
final Application.Tests count
Application tests added

Infrastructure.Tests count unchanged
Architecture.Tests count unchanged unless execution itself reports otherwise
total permanent test count
```

Do not hardcode expected final totals before execution.

------------------------------------------------------------------------

## 28. Build and Validation

Run at minimum:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo

dotnet test tests/AIQuantTradingResearch.Domain.Tests/AIQuantTradingResearch.Domain.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo

powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1

git diff --check
git diff --cached --check
```

Canonical validation must require no network/provider credential.

Established `NU1900` vulnerability-feed connectivity warnings remain
non-blocking only if all mandatory validation passes.

------------------------------------------------------------------------

## 29. Targeted Test Execution

During implementation, run the narrow Domain/Application suites as
needed.

Before final acceptance, run all four permanent suites and canonical
verification.

Do not claim WP13 provider coverage from the existing Infrastructure
test suite.

------------------------------------------------------------------------

## 30. Diff / Formatting Validation

Require:

``` text
format verification = PASS
git diff --check = PASS
git diff --cached --check = PASS
staged files = 0
temporary artifacts = 0
unexpected formatting changes = 0
```

Do not normalize unrelated files.

------------------------------------------------------------------------

## 31. Git / GitHub Protection

Do not:

``` text
stage
commit
create branch
push
create PR
merge
close issue #97
modify milestone #41
modify Project fields/status
create Release 1.1 planning
```

Preserve the cumulative uncommitted Release 1.0 candidate.

------------------------------------------------------------------------

## 32. Working-Tree Classification

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
WP11 AUTHORIZED
WP12 AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

List exact WP12 files.

Expected:

``` text
staged = 0
unexpected = 0
```

------------------------------------------------------------------------

## 33. Scope Protection

Explicitly prove WP12 did not begin:

``` text
WP13 Infrastructure & Provider Tests
WP14 Architecture Evolution
WP15 Documentation Alignment
WP16 Full Validation, Integration & Acceptance
Release 1.0 closure
Release 1.1
```

Also prove no production/provider/configuration/Worker behavior was
changed.

------------------------------------------------------------------------

## 34. Acceptance / Exit Criteria

WP12 completes only if all mandatory criteria pass:

``` text
WP11 predecessor gate = PASS

Domain production changes = 0
Application production changes = 0
Infrastructure production changes = 0
Worker production changes = 0

Release 1.0 Domain behavior reconciled against existing tests
unnecessary Domain test duplication = 0

Application success path permanently covered
request validation permanently covered
invalid request source invocation = 0
valid request source invocation = exactly 1

UnsupportedTarget → UnsupportedTarget covered
InsufficientObservations → InsufficientObservations covered
SourceUnavailable → SourceUnavailable covered
AccessDenied → AccessDenied covered
UsageLimitReached → UsageLimitReached covered
InvalidSourceResponse → InvalidSourceResponse covered

provider-specific references in Domain.Tests = 0
provider-specific references in Application.Tests = 0
network access = 0
credential dependency = 0

new packages = 0
new project references = 0
visibility broadening = 0
unauthorized friend assembly changes = 0

Domain.Tests = PASS
Application.Tests = PASS
Infrastructure.Tests = PASS
Architecture.Tests = PASS
eng/verify.ps1 = PASS
build errors = 0
git diff --check = PASS
git diff --cached --check = PASS
staged files = 0
temporary artifacts = 0
unexpected mutations = 0

WP13 started = NO
```

If a genuine production defect prevents correct tests and the manifest
does not authorize fixing it, return `BLOCKED`.

If testability requires new production visibility, return `BLOCKED` and
request a narrow unblock rather than weakening boundaries.

------------------------------------------------------------------------

## 35. Required Execution Report

Return:

``` text
# Release 1.0 WP12 — Domain & Application Tests Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP11 Predecessor Gate
## 5. Existing Domain Test Baseline
## 6. Domain Coverage Reconciliation
## 7. Domain Test Delta
## 8. Existing Application Test Baseline
## 9. Application Behavioral Gap Analysis
## 10. Test Double Design
## 11. Success-Path Coverage
## 12. Request-Validation Coverage
## 13. Source Invocation Coverage
## 14. Failure-Mapping Coverage
## 15. Defensive Unknown-Failure Coverage Decision
## 16. Domain Coverage Matrix
## 17. Application Coverage Matrix
## 18. Provider / Infrastructure Exclusion Evidence
## 19. Production-Code Preservation
## 20. Visibility / Testability Evidence
## 21. Files Changed
## 22. Test Counts
## 23. Determinism Evidence
## 24. WP03–WP11 Regression Evidence
## 25. Dependency / Architecture Evidence
## 26. Build Evidence
## 27. Test Evidence
## 28. Canonical Verification
## 29. Diff / Formatting Validation
## 30. Working-Tree Classification
## 31. Scope Protection
## 32. Findings / Observations
## 33. Exit-Criteria Assessment
## 34. Final Repository State
## 35. Final Decision
## 36. Next Authorized Action
```

The report must clearly distinguish pre-existing tests from WP12
additions and must not overclaim Infrastructure/provider coverage.

------------------------------------------------------------------------

## 36. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP12 DOMAIN AND APPLICATION TESTS COMPLETE
RELEASE 1.0 WP12 DOMAIN AND APPLICATION TESTS COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP12 DOMAIN AND APPLICATION TESTS BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when every mandatory criterion
passes.

------------------------------------------------------------------------

## 37. Next Authorized Action

If WP12 completes successfully:

``` text
WP13 — Infrastructure & Provider Tests
GitHub issue #98
```

Do not execute WP13.

Stop after the WP12 report.

------------------------------------------------------------------------

## Execution Instruction

Read all Release 1.0 authorities and the accepted cumulative WP03--WP11
implementation; prove the WP11 predecessor gate from repository truth;
reconcile existing Domain tests against Release 1.0 requirements without
manufacturing Domain changes; add only the permanent Domain/Application
behavioral tests authorized by WP12; use a deterministic test-owned
`IObservationSource` double; permanently prove the Application success
path, request validation, source invocation semantics, and all six
one-to-one source-failure mappings; keep Twelve Data, HTTP, JSON,
provider DTOs, configuration, credentials, Infrastructure mechanics,
Worker behavior, and WP13 provider tests out of scope; do not broaden
production visibility or add packages/references; run all four test
suites, canonical verification, architecture, formatting, diff,
determinism, leakage, regression, and working-tree checks; return the
complete WP12 execution report; and stop before WP13.

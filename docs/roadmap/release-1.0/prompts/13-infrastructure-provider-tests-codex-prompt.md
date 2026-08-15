# Release 1.0 WP13 --- Infrastructure & Provider Tests --- Codex Prompt

## Role

Act as the **WP13 Infrastructure & Provider Tests Executor** for Release
1.0 of `AIQuantTradingResearch`.

WP06--WP11 established the full Twelve Data Infrastructure path and
executable Worker composition:

``` text
TwelveData transport model           ← WP06
TwelveDataClient                     ← WP07
TwelveData normalization             ← WP08
TwelveDataObservationSource          ← WP09
DI/configuration                     ← WP10
Worker executable composition        ← WP11
```

WP12 owns Domain/Application permanent tests.

WP13 owns **permanent deterministic Infrastructure/provider tests** for
the Twelve Data transport, HTTP, normalization,
validation/failure-mapping, and composition behavior already
implemented.

Do not redesign production behavior.

Do not start WP14.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before mutation:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md

docs/roadmap/release-1.0/prompts/06-provider-transport-model-codex-prompt.md
docs/roadmap/release-1.0/prompts/07-provider-http-client-codex-prompt.md
docs/roadmap/release-1.0/prompts/08-market-data-normalization-codex-prompt.md
docs/roadmap/release-1.0/prompts/08-market-data-normalization-semantic-unblock-codex-prompt.md
docs/roadmap/release-1.0/prompts/09-market-data-validation-failure-mapping-codex-prompt.md
docs/roadmap/release-1.0/prompts/10-dependency-registration-configuration-codex-prompt.md
docs/roadmap/release-1.0/prompts/11-worker-market-data-execution-codex-prompt.md
docs/roadmap/release-1.0/prompts/12-domain-application-tests-codex-prompt.md
docs/roadmap/release-1.0/prompts/13-infrastructure-provider-tests-codex-prompt.md

docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read the accepted WP12 execution report from the current context or
repository evidence.

Read GitHub issues:

``` text
#97 — WP12 Domain & Application Tests
#98 — WP13 Infrastructure & Provider Tests
#99 — WP14 Architecture Evolution
#100 — WP15 Documentation Alignment
```

Inspect repository truth in:

``` text
src/AIQuantTradingResearch.Infrastructure/
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/
tests/AIQuantTradingResearch.Infrastructure.Tests/
tests/AIQuantTradingResearch.Architecture.Tests/
```

Authority precedence:

1.  `RELEASE_1.0_EXECUTION_PLAN.md`
2.  `RELEASE_1.0_FILE_MANIFEST.md`
3.  Accepted cumulative WP06--WP12 implementation/results
4.  WP08 semantic-unblock authority
5.  GitHub issue #98
6.  Existing repository test conventions
7.  This prompt

If a material conflict exists, stop and return `BLOCKED`.

------------------------------------------------------------------------

## 2. WP12 Predecessor Gate

Before mutation prove:

``` text
WP01–WP12 = complete
WP12 final decision = successful
WP12 Domain/Application permanent test coverage is present
WP13 issue = #98
WP14 has not started
```

Also prove the cumulative Infrastructure production baseline exists:

``` text
TwelveDataTimeSeriesResponse and related WP06 transport records
TwelveDataClient
TwelveDataTransportResult
TwelveDataTimeSeriesNormalizer
TwelveDataNormalizationResult
TwelveDataNormalizationFailure
TwelveDataObservationSource
TwelveDataConfiguration
Infrastructure DependencyInjection configured graph
```

If WP12 cannot be proven complete, stop before mutation with:

``` text
RELEASE 1.0 WP13 INFRASTRUCTURE AND PROVIDER TESTS BLOCKED
```

Do not infer WP12 completion from the existence of its prompt alone.

------------------------------------------------------------------------

## 3. Objective

Add permanent, deterministic, offline Infrastructure/provider behavioral
coverage for the accepted Release 1.0 Twelve Data implementation.

WP13 must convert the temporary proof evidence used during WP07--WP10
into a durable regression suite.

The suite must prove, at minimum:

``` text
transport request construction
authentication header behavior
success deserialization
error deserialization
transport exception preservation
normalization semantics
timezone/offset semantics
culture-invariant parsing
ordering/duplicate behavior
provider/HTTP failure classification
observation sufficiency behavior
cancellation propagation
configured DI graph
missing provider configuration behavior
no live provider dependency
```

Do not test Domain/Application behavior already owned by WP12 except
where necessary to assert the Infrastructure adapter returns the
existing Application contract.

------------------------------------------------------------------------

## 4. Permanent Test Ownership

WP13 owns permanent tests under:

``` text
tests/AIQuantTradingResearch.Infrastructure.Tests/
```

Use exact paths authorized by `RELEASE_1.0_FILE_MANIFEST.md`.

Do not add provider tests to Application.Tests or Domain.Tests.

Do not add architecture-rule tests here unless explicitly authorized by
WP13 manifest; WP14 owns Architecture Evolution.

------------------------------------------------------------------------

## 5. Testability Boundary

The Infrastructure assembly already has the Release 0.9 friend-assembly
boundary for:

``` text
AIQuantTradingResearch.Infrastructure.Tests
```

Use that existing boundary.

Do not:

``` text
make internal provider types public
add another InternalsVisibleTo
use reflection to bypass visibility
move provider types into public contracts
```

If the existing friend-assembly boundary is absent or insufficient
because of repository drift, stop and report the exact testability
blocker rather than weakening production visibility.

------------------------------------------------------------------------

## 6. No Live Provider Dependency

All WP13 tests must be deterministic and offline.

Forbidden:

``` text
live Twelve Data calls
real API keys
provider quota consumption
internet requirement
DNS dependency
wall-clock-sensitive external behavior
```

Use test-owned HTTP handlers, in-memory DTOs, and safe placeholder keys.

Canonical verification must remain green without internet/provider
credentials.

------------------------------------------------------------------------

## 7. HTTP Test Strategy

Test `TwelveDataClient` using a deterministic test-owned
`HttpMessageHandler` or equivalent accepted BCL seam.

Do not introduce a third-party HTTP mocking package.

The fake handler may capture:

``` text
request URI
query parameters
headers
cancellation token behavior
number of calls
```

and return deterministic `HttpResponseMessage` instances.

Do not use an actual network listener/server unless the manifest
explicitly requires it.

------------------------------------------------------------------------

## 8. Request Construction Coverage

Permanently prove the accepted WP07 + semantic-unblock request contract:

``` text
endpoint = /time_series
symbol = correctly encoded requested target
interval = 1day
outputsize = requested observation count
adjust = splits
```

Prove there are no unauthorized provider parameters.

Do not encode API key in the URI.

------------------------------------------------------------------------

## 9. Authentication Coverage

Permanently prove:

``` text
Authorization header exists
scheme/value shape matches accepted WP07 behavior
configured placeholder key is carried only in the header
API key query parameter = absent
```

Use a non-secret placeholder value.

Do not snapshot or output any real credential.

------------------------------------------------------------------------

## 10. HttpClient Invocation Coverage

Prove:

``` text
eligible request → exactly one HTTP call
HttpClient.BaseAddress combines correctly with /time_series
ResponseHeadersRead behavior is preserved if testable without implementation coupling
cancellation is propagated
```

Avoid assertions on incidental framework implementation details that are
not part of the Release 1.0 contract.

------------------------------------------------------------------------

## 11. Success Deserialization Coverage

Provide a minimal representative successful Twelve Data payload fixture
in test code or manifest-authorized fixture file.

Prove deserialization preserves:

``` text
status
metadata needed downstream
exchange_timezone
datetime
open
high
low
close
volume
```

Do not normalize in the client test.

The test should prove wire preservation, not Domain semantics.

------------------------------------------------------------------------

## 12. Error Deserialization Coverage

Provide deterministic structured provider error payloads and prove
accepted error DTO preservation.

At minimum cover the fields actually used by WP09, such as:

``` text
status
code
message
```

Use exact repository truth.

Do not perform Application classification in the client-deserialization
test; classification belongs to adapter tests.

------------------------------------------------------------------------

## 13. Unreadable Payload Coverage

Permanently prove WP07 transport evidence for malformed/unreadable
payloads.

Cover at least:

``` text
eligible success HTTP status + malformed body
error HTTP status + malformed body where WP07 preserves unreadable evidence
```

Do not duplicate WP09 classification assertions here unless testing the
adapter boundary separately.

------------------------------------------------------------------------

## 14. Transport Exception Coverage

Using the test-owned HTTP handler, force the accepted transport
exception path.

Prove `TwelveDataClient` preserves the `HttpRequestException` evidence
exactly as designed by WP07.

Do not inspect exception-message text for classification.

------------------------------------------------------------------------

## 15. Cancellation Coverage

Prove caller cancellation remains cancellation through the accepted
client/adapter boundary.

Do not convert cancellation to:

``` text
SourceUnavailable
InvalidSourceResponse
```

Use deterministic cancellation without sleeping or timing races.

------------------------------------------------------------------------

## 16. Normalizer Success Coverage

Permanently reproduce the high-value WP08 temporary-probe semantics.

At minimum cover:

``` text
canonical price uses close only
valid close parses using InvariantCulture
yyyy-MM-dd exact date parsing
exchange_timezone drives offset
winter DST offset example
summer DST offset example
output sorted ascending by absolute instant
```

Use deterministic known dates/timezones.

Do not depend on machine local timezone.

------------------------------------------------------------------------

## 17. Culture Independence Coverage

At least one test must execute normalization under a culture whose
decimal/date conventions differ from invariant formatting, such as
`pt-BR`, while safely restoring culture afterward.

Prove valid provider wire values still normalize identically.

Avoid test pollution across parallel tests.

If manipulating global culture risks parallel interference, use a
scoped/culture-safe technique consistent with repository test
conventions.

------------------------------------------------------------------------

## 18. Normalizer Failure Coverage

Permanently cover the Infrastructure-local normalization failure cases
required by the accepted WP08 semantics, including as authorized:

``` text
missing metadata
missing/blank exchange timezone
unresolvable timezone
missing values collection
null row if representable
blank datetime
malformed datetime
invalid local anchor
ambiguous local anchor
blank close
malformed close
zero close
negative close
duplicate normalized instant
Domain invariant rejection if distinct and constructible
```

Do not add artificial cases impossible under the actual DTO/type system.

Report any listed case that is structurally impossible and why.

------------------------------------------------------------------------

## 19. Ordering / Duplicate Coverage

Prove:

``` text
provider descending rows normalize to chronological ascending observations
duplicate normalized instants fail
rows are not silently dropped or merged
```

Do not test provider-count policy in the normalizer tests.

------------------------------------------------------------------------

## 20. Adapter Success Coverage

Test `TwelveDataObservationSource` as the Infrastructure anti-corruption
boundary.

Use the accepted HTTP/client seams or deterministic client setup.

Prove a successful provider payload:

``` text
passes transport validation
normalizes through WP08
returns ObservationSourceResult success
preserves canonical observations
```

Do not call `ResearchUseCase`; WP12 owns Application orchestration
tests.

------------------------------------------------------------------------

## 21. Adapter HTTP Mapping Coverage

Permanently cover WP09 mappings:

``` text
401 → AccessDenied
403 → AccessDenied
404 → UnsupportedTarget
429 → UsageLimitReached
5xx → SourceUnavailable
```

Use exact `ObservationSourceFailure` names.

HTTP status classification must take precedence over malformed body
evidence where WP09 defines that precedence.

------------------------------------------------------------------------

## 22. Adapter Provider-Code Mapping Coverage

Where WP09 implemented structured provider-code mapping, permanently
prove:

``` text
401/403 → AccessDenied
404 → UnsupportedTarget
429 → UsageLimitReached
500–599 → SourceUnavailable
other/unclassifiable → InvalidSourceResponse
```

Do not add message-substring heuristics.

------------------------------------------------------------------------

## 23. Adapter Transport Failure Coverage

Permanently prove:

``` text
HttpRequestException → SourceUnavailable
```

and caller cancellation remains cancellation.

Do not collapse cancellation into a source failure.

------------------------------------------------------------------------

## 24. Adapter Invalid-Payload Coverage

Permanently prove:

``` text
unreadable eligible payload → InvalidSourceResponse
missing/inconsistent success payload → InvalidSourceResponse
structured unclassifiable provider error → InvalidSourceResponse
WP08 normalization failure → InvalidSourceResponse
```

Follow actual WP09 precedence.

------------------------------------------------------------------------

## 25. Observation Sufficiency Coverage

Permanently prove:

``` text
normalized count < requested count → InsufficientObservations
empty normalized collection for positive request → InsufficientObservations
normalized count >= requested count → success
```

Do not manufacture, repeat, or silently truncate observations unless
actual production behavior explicitly does so.

------------------------------------------------------------------------

## 26. Unsupported Direct Input Coverage

If WP09 has defensive direct-call request validation:

``` text
blank target → UnsupportedTarget
non-positive direct count → actual accepted outcome
```

test it according to repository truth.

Do not confuse Application request validation (WP12) with Infrastructure
defensive validation.

------------------------------------------------------------------------

## 27. DI Composition Coverage

Permanently prove WP10 composition behavior.

Using the existing public `AddInfrastructure` boundary and safe
placeholder configuration, verify:

``` text
service provider validates
IObservationSource resolves to TwelveDataObservationSource
IObservationSource registration count = 1
TwelveDataClient resolves
HttpClient base address = https://api.twelvedata.com/
resolution emits zero provider HTTP requests
deterministic source remains unregistered
```

Do not access internals unnecessarily if service collection descriptors
can prove a point.

------------------------------------------------------------------------

## 28. Missing Configuration Coverage

Permanently prove the WP10 missing-configuration contract:

``` text
parameterless AddInfrastructure() → deterministic InvalidOperationException
message identifies TwelveData:ApiKey path
no fallback source
no fake key
```

Do not assert an overly brittle full exception string unless repository
conventions make it contractual.

------------------------------------------------------------------------

## 29. Configuration Security Coverage

Tests must use only obvious placeholder credentials.

Prove the placeholder is not present in the request URI/query.

Do not write it to console/log output.

Do not add secret fixture files.

------------------------------------------------------------------------

## 30. Test Organization

Prefer focused test files/classes by production responsibility, for
example conceptually:

``` text
TwelveDataClientTests
TwelveDataTimeSeriesNormalizerTests
TwelveDataObservationSourceTests
DependencyInjectionTests
```

Use exact manifest-authorized paths and repository naming conventions.

Do not create one giant provider test file if the manifest/conventions
support focused classes.

Do not create unnecessary abstract test bases.

------------------------------------------------------------------------

## 31. Fixture Strategy

Prefer small inline JSON constants/builders where readable.

If the manifest authorizes fixture files, keep them minimal and
synthetic.

Do not commit actual Twelve Data payloads copied from live responses
unless licensing/governance explicitly authorizes that.

Synthetic payloads should match documented wire shape without
representing real proprietary market history.

------------------------------------------------------------------------

## 32. Package / Project Protection

Maintain:

``` text
new packages = 0
new project references = 0
solution changes = 0
```

Use existing xUnit/assertion/runtime capabilities.

Do not add mocking, HTTP-stub, timezone, JSON, or fixture libraries.

------------------------------------------------------------------------

## 33. Production-Code Protection

WP13 is a test package.

Target:

``` text
Domain production changes = 0
Application production changes = 0
Infrastructure production changes = 0
Worker production changes = 0
```

If a permanent test reveals a real production defect, stop and report it
rather than silently changing production code unless the Release 1.0
manifest explicitly authorizes that exact correction within WP13.

If the defect requires a production correction outside WP13, return
`BLOCKED` and identify the minimum unblock.

------------------------------------------------------------------------

## 34. No Architecture Evolution

WP14 owns Architecture Evolution.

Do not add/modify architecture rules merely because new provider types
now exist.

Run existing Architecture.Tests as regression evidence only.

------------------------------------------------------------------------

## 35. No Documentation / GitHub Mutation

Do not modify:

``` text
architecture documentation
Release 1.0 authorities
prompts other than the pre-existing WP13 prompt pair
GitHub issues/milestone/Project
```

Do not close issue #98.

------------------------------------------------------------------------

## 36. Test Count Accounting

Record:

``` text
Infrastructure.Tests baseline count
Infrastructure.Tests final count
WP13 tests added
Domain.Tests final count
Application.Tests final count
Architecture.Tests final count
total permanent test count
```

Do not predeclare the final count.

Clearly distinguish permanent tests from any temporary exploratory
probe. WP13 should normally need no temporary probe after permanent
coverage is established.

------------------------------------------------------------------------

## 37. Determinism Evidence

Prove the final WP13 suite requires:

``` text
network = NO
real credential = NO
provider availability = NO
machine current culture = NO
machine local timezone = NO
wall clock = NO
randomness = NO
test-order dependency = NO
```

If global culture is temporarily changed, restore it deterministically.

------------------------------------------------------------------------

## 38. Security / Leakage Scan

Prove:

``` text
real credentials in tests = 0
API-key query use = 0
Domain Twelve Data references = 0
Application Twelve Data references = 0
Application HttpClient references = 0
ResearchUseCase provider branches = 0
Worker provider-test hooks = 0
```

WP13 tests may reference Infrastructure provider internals through the
existing friend assembly.

------------------------------------------------------------------------

## 39. Regression Protection

Prove WP13 does not alter accepted behavior from:

``` text
WP03 zero Domain delta
WP04 failure vocabulary
WP05 mappings
WP06 transport types
WP07 endpoint/authentication/adjust=splits
WP08 close/timestamp/order/duplicate semantics
WP09 classification/cancellation
WP10 DI/configuration
WP11 Worker execution/configuration handoff
WP12 Domain/Application tests
```

------------------------------------------------------------------------

## 40. Build and Validation

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

All permanent provider tests must execute offline.

Established `NU1900` warnings remain non-blocking only if mandatory
validation passes.

------------------------------------------------------------------------

## 41. Working-Tree Classification

Classify all visible changes:

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
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

List exact WP13 files.

Expected:

``` text
staged = 0
unexpected = 0
temporary artifacts = 0
```

------------------------------------------------------------------------

## 42. Git / GitHub Protection

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
close issue #98
modify milestone #41
modify Project fields/status
create Release 1.1 planning
```

WP13 ends as a validated local cumulative Release 1.0 candidate.

------------------------------------------------------------------------

## 43. Scope Protection

Prove WP13 did not start:

``` text
WP14 Architecture Evolution
WP15 Documentation Alignment
WP16 Full Validation, Integration & Acceptance
Release 1.0 closure
Release 1.1
```

Also prove no storage, caching, streaming, retries, provider fallback,
AI/ML, plugin, production hosting, or multi-provider framework was
introduced.

------------------------------------------------------------------------

## 44. Acceptance / Exit Criteria

WP13 completes only if:

``` text
WP12 predecessor gate = PASS

permanent TwelveDataClient request tests = present
/time_series covered
symbol encoding covered
interval=1day covered
outputsize covered
adjust=splits covered
header authentication covered
API key absent from query covered

success deserialization covered
error deserialization covered
unreadable payload covered
HttpRequestException preservation covered
cancellation covered

close-only normalization covered
InvariantCulture behavior covered
exchange timezone winter/summer offsets covered
ascending ordering covered
duplicate rejection covered
malformed normalization cases covered

401 → AccessDenied covered
403 → AccessDenied covered
404 → UnsupportedTarget covered
429 → UsageLimitReached covered
5xx → SourceUnavailable covered
unclassifiable provider error → InvalidSourceResponse covered
normalization failure → InvalidSourceResponse covered
insufficient count → InsufficientObservations covered
eligible success covered
cancellation remains cancellation

configured DI graph covered
IObservationSource runtime implementation covered
registration count = 1 covered
deterministic source unregistered covered
missing configuration failure covered
resolution emits provider request = NO

live network in tests = 0
real credentials = 0
new packages = 0
new project references = 0
production changes = 0
architecture-rule changes = 0

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

WP14 started = NO
```

If permanent testing requires production visibility beyond the existing
friend-assembly boundary, return `BLOCKED`.

If tests prove an existing production defect, return `BLOCKED` with the
minimal corrective authority needed unless the manifest explicitly
authorizes the fix.

------------------------------------------------------------------------

## 45. Required Execution Report

Return:

``` text
# Release 1.0 WP13 — Infrastructure & Provider Tests Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP12 Predecessor Gate
## 5. Existing Infrastructure Test Baseline
## 6. Permanent Test Design
## 7. HTTP Test Harness
## 8. Request Construction Coverage
## 9. Authentication Coverage
## 10. Success Deserialization Coverage
## 11. Error / Unreadable Payload Coverage
## 12. Transport Exception / Cancellation Coverage
## 13. Normalizer Success Coverage
## 14. Culture / Timezone Coverage
## 15. Normalizer Failure Coverage
## 16. Ordering / Duplicate Coverage
## 17. Observation Source Success Coverage
## 18. HTTP / Provider Failure Mapping Coverage
## 19. Invalid Payload / Normalization Mapping Coverage
## 20. Observation Sufficiency Coverage
## 21. DI Composition Coverage
## 22. Missing Configuration Coverage
## 23. Test Organization / Fixtures
## 24. Production-Code Preservation
## 25. Visibility / Friend-Assembly Evidence
## 26. Files Changed
## 27. Test Count Delta
## 28. Determinism Evidence
## 29. Security / Leakage Evidence
## 30. WP06–WP12 Regression Evidence
## 31. Dependency / Architecture Evidence
## 32. Build Evidence
## 33. Test Evidence
## 34. Canonical Verification
## 35. Diff / Formatting Validation
## 36. Working-Tree Classification
## 37. Scope Protection
## 38. Findings / Observations
## 39. Exit-Criteria Assessment
## 40. Final Repository State
## 41. Final Decision
## 42. Next Authorized Action
```

Report exact test files/classes, key test cases, baseline/final counts,
fixtures, HTTP harness, permanent/offline status, and validation
results.

Do not claim provider-live coverage.

------------------------------------------------------------------------

## 46. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP13 INFRASTRUCTURE AND PROVIDER TESTS COMPLETE
RELEASE 1.0 WP13 INFRASTRUCTURE AND PROVIDER TESTS COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP13 INFRASTRUCTURE AND PROVIDER TESTS BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when all mandatory criteria pass.

------------------------------------------------------------------------

## 47. Next Authorized Action

If WP13 completes successfully:

``` text
WP14 — Architecture Evolution
GitHub issue #99
```

Do not execute WP14.

Stop after the WP13 report.

------------------------------------------------------------------------

## Execution Instruction

Read all Release 1.0 authorities and cumulative WP06--WP12
implementation/results; prove WP12 completion before mutation; add only
permanent deterministic offline Infrastructure/provider tests authorized
by WP13; convert the temporary WP07--WP10 proofs into durable coverage
for request/authentication/deserialization, transport
exceptions/cancellation, WP08
normalization/timezone/culture/order/failure semantics, WP09
HTTP/provider/normalization/count mappings, and WP10 DI/configuration
behavior; use the existing Infrastructure friend-assembly boundary
without broadening visibility; require no live provider or real
credential; make no production, architecture-rule, package,
project-reference, Worker, Domain, or Application changes; run all
permanent suites, canonical verification,
determinism/security/leakage/regression/diff checks; classify the
cumulative working tree; return the full WP13 execution report; and stop
before WP14.

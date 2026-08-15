# Release 1.0 WP08 --- Market Data Normalization --- Codex Prompt

## Role

Act as the **WP08 Market Data Normalization Executor** for Release 1.0
of `AIQuantTradingResearch`.

WP06 established the internal Twelve Data transport model.

WP07 established the internal Twelve Data `/time_series` HTTP boundary
and preserves successful transport payloads, structured provider errors,
HTTP status, malformed-payload state, transport exceptions, and
cancellation semantics.

WP08 now owns the **successful-response normalization boundary**:
converting a structurally usable Twelve Data time-series payload into
the existing provider-independent Domain observation values required by
the research flow.

WP08 must not classify HTTP/provider failures into Application failures.
That belongs to WP09.

WP08 must not register dependencies/configuration or change Worker
behavior. Those belong to WP10 and WP11.

Do not start WP09.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before acting:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
docs/roadmap/release-1.0/prompts/02-market-data-provider-discovery-codex-prompt.md
docs/roadmap/release-1.0/prompts/03-market-data-domain-evolution-codex-prompt.md
docs/roadmap/release-1.0/prompts/04-market-data-application-contracts-codex-prompt.md
docs/roadmap/release-1.0/prompts/05-historical-market-data-use-case-integration-codex-prompt.md
docs/roadmap/release-1.0/prompts/06-provider-transport-model-codex-prompt.md
docs/roadmap/release-1.0/prompts/07-provider-http-client-codex-prompt.md
docs/roadmap/release-1.0/prompts/08-market-data-normalization-codex-prompt.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read GitHub issues:

``` text
#92 — WP07 — Provider HTTP Client
#93 — WP08 — Market Data Normalization
#94 — WP09 — Market Data Validation & Failure Mapping
#95 — WP10 — Dependency Registration & Configuration
#96 — WP11 — Worker Market Data Execution
```

Read the accepted WP07 execution report from the current context.

Inspect the cumulative implementation, especially:

``` text
src/AIQuantTradingResearch.Domain/
src/AIQuantTradingResearch.Application/
src/AIQuantTradingResearch.Infrastructure/
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/
tests/AIQuantTradingResearch.Infrastructure.Tests/
tests/AIQuantTradingResearch.Architecture.Tests/
```

Authority precedence:

``` text
1. RELEASE_1.0_EXECUTION_PLAN.md
2. RELEASE_1.0_FILE_MANIFEST.md
3. WP02 provider assessment/decision
4. Accepted WP03–WP07 results
5. GitHub issue #93
6. Existing architecture/engineering conventions
7. This prompt
```

If authorities materially conflict, stop.

------------------------------------------------------------------------

## 2. Predecessor Gate

Before mutation prove:

``` text
WP01–WP07 = complete
selected provider = Twelve Data
WP03 Domain delta = zero
WP06 four internal transport records remain intact
WP07 TwelveDataClient exists
WP07 TwelveDataTransportResult exists
WP07 preserves raw datetime/OHLCV text
WP07 performs no Domain normalization
WP07 performs no Application failure mapping
WP08 issue = #93
```

Verify the actual Domain observation/value types and Application
acquisition contract before designing normalization.

Do not assume names or constructors when repository truth can be
inspected.

------------------------------------------------------------------------

## 3. Objective

Implement the minimum Infrastructure-owned normalization capability
required to convert an **already acquired, successful Twelve Data
time-series payload** into the provider-independent Domain observation
representation expected by the existing research path.

The intended conceptual transformation is:

``` text
TwelveDataTimeSeriesResponse
        ↓
validate only normalization prerequisites
        ↓
parse provider datetime
parse selected provider price field
        ↓
PriceObservation values
        ↓
provider-independent normalized observations
```

Use the exact types and return shape authorized by
`RELEASE_1.0_FILE_MANIFEST.md`.

WP08 is a deterministic transformation boundary.

It does not own HTTP execution.

It does not own transport/provider failure classification.

------------------------------------------------------------------------

## 4. Canonical Price Semantics

Determine the Release 1.0 canonical historical price field strictly from
the execution plan, provider decision, existing research model, and
manifest.

If the authority establishes Twelve Data `close` as the canonical price
for the Release 1.0 vertical slice, normalize **only `close`**.

Do not average OHLC values.

Do not use `open`, `high`, or `low` as fallback values.

Do not derive VWAP.

Do not introduce adjusted-close semantics unless explicitly authorized.

Record the exact selected field and authority in the execution report.

If the governing authorities do not establish which provider field
becomes the Domain price, stop rather than inventing semantics.

------------------------------------------------------------------------

## 5. Timestamp Semantics

Determine the expected Domain timestamp semantics from existing Domain
code and Release 1.0 authority.

Normalize Twelve Data's raw `datetime` text using:

``` text
explicit culture
explicit parsing rules
explicit timezone/DateTimeKind treatment consistent with authority
```

Do not silently depend on the machine's current culture.

Do not silently depend on the machine's local timezone.

Do not invent timezone conversion not required by the provider
decision/execution plan.

If the provider response is intentionally requested in a specific
timezone by a higher authority, preserve/normalize according to that
contract.

If timezone semantics are materially ambiguous and cannot be resolved
from authority, stop.

------------------------------------------------------------------------

## 6. Numeric Semantics

Parse the canonical provider price using deterministic
culture-independent rules.

Use the exact numeric type expected by the existing Domain value.

Normally this means invariant parsing of the provider's decimal text
into the Domain's decimal representation.

Do not:

``` text
use current culture
round provider values unnecessarily
convert through double if Domain uses decimal
accept NaN/infinity semantics inappropriate to the Domain
replace malformed values with zero
```

Preserve Domain invariants.

------------------------------------------------------------------------

## 7. Input Boundary

WP08 operates on successful provider transport data.

Do not make HTTP calls.

Do not accept or depend on `HttpClient`.

Do not construct authenticated requests.

Do not read API keys.

Do not bind configuration.

Do not inspect environment variables.

Do not make a live Twelve Data call.

The normalizer should be independently deterministic from an in-memory
WP06 success DTO.

------------------------------------------------------------------------

## 8. Structural Preconditions vs WP09 Classification

WP08 may need to detect that a supposedly successful transport payload
cannot be normalized, for example because required fields are absent or
malformed.

Preserve a strict distinction:

``` text
WP08 detects normalization success/failure
WP09 decides which Application failure that evidence means
```

WP08 must not return `ObservationSourceFailure`.

Use the exact Infrastructure-owned normalization result/error
representation authorized by the manifest.

If no explicit result type is authorized, choose the smallest
Infrastructure-only representation consistent with the execution plan
and existing error-handling conventions.

Do not create a general-purpose validation framework.

------------------------------------------------------------------------

## 9. Required Malformed-Value Handling

Normalization must never silently manufacture Domain values.

At minimum reconcile how the authorized design represents cases such as:

``` text
missing values collection
null observation entry if representable
missing/blank datetime
unparseable datetime
missing/blank canonical price
unparseable canonical price
Domain constructor/invariant rejection
```

The normalizer may fail deterministically and preserve the
reason/evidence needed by WP09.

Do not map these conditions to `InvalidSourceResponse` in WP08.

That semantic mapping belongs to WP09.

------------------------------------------------------------------------

## 10. Ordering Semantics

Inspect the Domain/Application expectations for observation order.

Twelve Data transport order must not be assumed blindly.

If Release 1.0 authority requires normalized observations in
chronological order, enforce that deterministically in WP08.

If existing Domain `ObservationSeries` or research behavior establishes
ordering semantics, follow them.

Do not introduce arbitrary sorting if the authority explicitly preserves
provider order.

Record:

``` text
provider order observed/expected
normalized order required
whether sorting is performed
sort key
```

If sorting is required, sort only after successful timestamp parsing.

------------------------------------------------------------------------

## 11. Duplicate Semantics

Do not invent deduplication behavior.

If duplicate timestamps are governed by Domain invariants or Release 1.0
authority, follow those rules.

Otherwise preserve duplicates through normalization and leave
higher-level validity decisions to the proper boundary.

Do not silently drop observations.

Do not silently merge observations.

------------------------------------------------------------------------

## 12. Count Semantics

Do not implement Application requested-count failure mapping in WP08.

WP08 may normalize all provider values present in the successful
response.

Do not map:

``` text
fewer rows than requested -> InsufficientObservations
```

That belongs to the observation-source validation/failure boundary
defined for WP09 and the existing Application contract.

Do not truncate valid provider data unless the execution plan explicitly
requires normalization to the requested count.

------------------------------------------------------------------------

## 13. Domain Construction

Use existing Domain types.

Do not create provider-specific Domain types.

Do not modify Domain solely to accommodate Twelve Data.

WP03 explicitly accepted zero Domain delta; preserve that result unless
a higher authority explicitly authorizes otherwise.

Expected provider-independent output should use the established Domain
observation value, such as the existing `PriceObservation`, according to
actual repository code.

Do not construct `ObservationSeries` or calculate `MeanPrice` unless the
execution plan explicitly assigns that to WP08.

Those remain part of the existing Application research flow.

------------------------------------------------------------------------

## 14. Infrastructure Ownership

All Twelve Data normalization implementation remains in Infrastructure.

Provider-specific types must not escape into Application or Domain.

Application continues to know only its provider-independent acquisition
seam and Domain values.

Do not add:

``` text
TwelveData
JSON DTO
HTTP status
provider code/message
API key
transport result
```

to Application or Domain contracts.

------------------------------------------------------------------------

## 15. Relationship to TwelveDataTransportResult

WP07 created `TwelveDataTransportResult` to preserve:

``` text
HttpStatusCode?
TwelveDataTimeSeriesResponse?
TwelveDataErrorResponse?
IsPayloadUnreadable
HttpRequestException?
```

WP08 must not become the owner of HTTP status/provider-error
interpretation.

Prefer normalization of the successful response payload itself, or the
exact manifest-authorized input boundary.

Do not make the normalizer classify:

``` text
non-success status
provider error envelope
transport exception
unreadable JSON
```

Those are WP09 concerns.

If the manifest explicitly makes the normalizer consume a transport
result, it must still normalize only the success-payload branch and
preserve all other evidence without Application mapping.

------------------------------------------------------------------------

## 16. No Failure Mapping

WP09 owns the provider/transport validation and failure mapping matrix.

WP08 must not reference or produce:

``` text
ObservationSourceFailure.UnsupportedTarget
ObservationSourceFailure.InsufficientObservations
ObservationSourceFailure.SourceUnavailable
ObservationSourceFailure.AccessDenied
ObservationSourceFailure.UsageLimitReached
ObservationSourceFailure.InvalidSourceResponse
```

unless a higher authority explicitly requires a compile-time type
reference without semantic mapping.

Target expectation: **zero `ObservationSourceFailure` references in WP08
normalization implementation.**

Do not classify:

``` text
401
403
429
5xx
HttpRequestException
provider error code/message
malformed JSON
```

WP09 owns that.

------------------------------------------------------------------------

## 17. No HTTP Expansion

WP07 HTTP behavior is accepted.

Do not modify:

``` text
endpoint
query parameters
Authorization header behavior
HttpClient ownership
response acquisition
exception capture
```

unless a proven WP08 compile requirement and file manifest explicitly
authorize the change.

WP08 must not add another provider endpoint.

------------------------------------------------------------------------

## 18. No DI / Worker Work

WP10 owns dependency registration/configuration.

WP11 owns Worker execution.

Do not modify:

``` text
DependencyInjection.cs
Worker composition
Worker output
appsettings
environment-variable binding
HttpClient registration
options registration
```

Do not start WP10/WP11.

------------------------------------------------------------------------

## 19. Authorized Files

`RELEASE_1.0_FILE_MANIFEST.md` is the exact file authority.

Create/modify only WP08-authorized files.

Expected changes should remain under the Twelve Data Infrastructure
boundary unless the manifest says otherwise.

Use exact manifest paths/names when specified.

Do not modify:

``` text
Domain
Application
Worker
project manifests
package manifests
solution
build policy
GitHub workflows
```

unless the file manifest explicitly authorizes the exact change.

Do not modify WP06/WP07 files merely for stylistic cleanup.

------------------------------------------------------------------------

## 20. Test-Scope Discipline

WP13 owns comprehensive Infrastructure/provider tests.

Do not pull WP13 forward.

WP08 may use a temporary deterministic probe if needed to prove
normalization behavior, provided:

``` text
it uses only in-memory transport DTOs
it performs no live HTTP
it uses no real credential
it is removed before completion
it does not become a permanent WP13 test
```

Permanent tests may be added only if explicitly authorized by the
Release 1.0 manifest.

Existing test suites must remain green.

------------------------------------------------------------------------

## 21. Determinism Matrix

Prove normalization is independent of:

``` text
internet access
provider availability
API credential
machine current culture
machine local timezone
wall clock
randomness
```

Where practical, a temporary probe should exercise normalization under a
non-default culture to prove invariant numeric/date behavior if that can
be done without expanding scope.

Do not change global repository test behavior.

------------------------------------------------------------------------

## 22. Normalization Evidence Matrix

Produce an explicit matrix covering the actual implementation:

  Transport input                          Expected normalization result
  ---------------------------------------- -------------------------------------------
  valid datetime + valid canonical price   Domain observation
  blank datetime                           deterministic normalization failure
  invalid datetime                         deterministic normalization failure
  blank canonical price                    deterministic normalization failure
  invalid canonical price                  deterministic normalization failure
  Domain-invariant-invalid value           deterministic normalization failure
  multiple valid values                    normalized collection in authorized order

Add other cases only if actual DTO/Domain structure requires them.

Do not claim permanent test coverage where only inspection or a
temporary probe was used.

------------------------------------------------------------------------

## 23. Provider Field Exclusion

If `close` is the authorized canonical field, prove the normalizer does
not derive Domain price from:

``` text
open
high
low
volume
metadata
```

Other WP06 fields remain transport evidence only.

Do not delete them from the transport DTO.

------------------------------------------------------------------------

## 24. Culture Inspection

Search the WP08 implementation for accidental culture-sensitive
operations.

Expected:

``` text
InvariantCulture or equivalent explicit culture behavior
explicit DateTime parsing semantics
explicit decimal parsing semantics
```

Reject implicit:

``` text
decimal.Parse(raw)
DateTime.Parse(raw)
Convert.ToDecimal(raw)
Convert.ToDateTime(raw)
```

when they rely on current culture/default timezone semantics.

Use repository conventions and analyzers.

------------------------------------------------------------------------

## 25. Dependency / Package Protection

No new package should be necessary.

Prove:

``` text
new NuGet packages = 0
new project references = 0
```

Do not add a date/time or mapping library for this narrow boundary.

Use framework-native parsing and existing Domain types.

------------------------------------------------------------------------

## 26. Architecture Protection

Production dependency graph must remain:

``` text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

Prove:

``` text
cycles = 0
Domain → Infrastructure = 0
Application → Infrastructure = 0
provider-specific Domain types = 0
provider-specific Application types = 0
```

Architecture tests must remain green.

------------------------------------------------------------------------

## 27. Targeted Static Inspection

Prove after implementation:

``` text
Twelve Data normalization implementation exists only in Infrastructure
Domain Twelve Data references = 0
Application Twelve Data references = 0
WP08 HttpClient references = 0
WP08 API-key/configuration references = 0
WP08 ObservationSourceFailure mapping = 0
WP08 HTTP-status classification = 0
WP08 provider-error classification = 0
WP08 ResearchFailure references = 0
WP08 Worker references = 0
WP08 DI registration changes = 0
```

Also prove the selected canonical field and invariant parsing are
explicit.

------------------------------------------------------------------------

## 28. Regression Protection

Prove:

``` text
WP03 Domain delta remains zero
WP04 failure vocabulary remains intact
WP05 six source-to-research mappings remain intact
WP06 four transport records remain intact
WP07 client/result behavior remains intact
WP07 authentication remains header-based
WP07 endpoint remains /time_series only
existing deterministic observation source unchanged
Worker unchanged
existing permanent test baseline remains green unless authorized tests legitimately increase it
```

Do not alter accepted predecessors to make WP08 easier.

------------------------------------------------------------------------

## 29. Validation

Run the exact validation required by Release 1.0 authority.

At minimum, unless the execution plan requires stronger commands:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
git diff --cached --check
```

Record actual counts.

If a temporary normalization probe is used, report:

``` text
cases
results
culture/timezone conditions if varied
whether it was removed
```

Canonical verification must not require network/provider access.

Known `NU1900` warnings remain non-blocking only under the established
repository condition and when mandatory checks pass.

------------------------------------------------------------------------

## 30. Git / GitHub Protection

Do not:

``` text
stage
commit
branch
push
create PR
merge
stash
discard cumulative Release 1.0 work
```

Do not mutate:

``` text
issue #93
milestone #41
Project fields/status
labels
Release 1.1 planning
```

WP08 ends as a validated local cumulative candidate.

------------------------------------------------------------------------

## 31. Working-Tree Classification

At completion classify all visible state:

``` text
EXPECTED GOVERNANCE
PRE-EXISTING AUTHORIZED
WP02 AUTHORIZED
WP03 AUTHORIZED
WP04 AUTHORIZED
WP05 AUTHORIZED
WP06 AUTHORIZED
WP07 AUTHORIZED
WP08 AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

List exact WP08 files separately.

Nothing should be staged.

Unexpected changes must be investigated before completion.

------------------------------------------------------------------------

## 32. Acceptance / Exit Criteria

WP08 is complete only if:

``` text
WP07 predecessor gate = PASS
canonical provider price field resolved from authority
timestamp semantics resolved from authority/repository truth
normalizer implemented only in Infrastructure
normalizer consumes authorized successful Twelve Data transport data
canonical price parsed deterministically
timestamp parsed deterministically
existing Domain observation type reused
machine current culture dependence = 0
machine local timezone dependence = 0 unless explicitly authorized
silent default/zero substitution = 0
malformed required value yields deterministic normalization failure
provider-specific Domain/Application leakage = 0
HTTP calls in normalization = 0
API-key/configuration behavior in normalization = 0
HTTP/provider failure classification = 0
ObservationSourceFailure mapping = 0
ResearchFailure mapping = 0
DI changes = 0
Worker changes = 0
new packages = 0
new project references = 0
build = PASS with zero errors
required Infrastructure tests = PASS
architecture tests = PASS
canonical verification = PASS
git diff --check = PASS
git diff --cached --check = PASS
unexpected mutations = 0
WP09 started = NO
```

If canonical field or timestamp semantics cannot be established from
authority, return `BLOCKED`.

If correct normalization requires changing Domain semantics contrary to
WP03, return `BLOCKED`.

------------------------------------------------------------------------

## 33. Required Execution Report

Return:

``` text
# Release 1.0 WP08 — Market Data Normalization Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP07 Predecessor Gate
## 5. Existing Transport and Domain Baseline
## 6. Normalization Design Reconciliation
## 7. Canonical Price-Field Decision
## 8. Timestamp Semantics
## 9. Numeric Parsing Semantics
## 10. Ordering / Duplicate / Count Semantics
## 11. Normalization Result / Failure Representation
## 12. Valid Observation Normalization
## 13. Malformed-Value Handling
## 14. Normalization Evidence Matrix
## 15. Types Added or Modified
## 16. Files Changed
## 17. Provider-Field Exclusion Evidence
## 18. Culture / Timezone Determinism Evidence
## 19. HTTP / Authentication Exclusion Evidence
## 20. Failure-Mapping Exclusion Evidence
## 21. DI / Worker Exclusion Evidence
## 22. Application / Domain Leakage Scan
## 23. Dependency / Architecture Evidence
## 24. Build Evidence
## 25. Test / Temporary-Probe Evidence
## 26. Canonical Verification
## 27. Diff / Formatting Validation
## 28. Regression Evidence
## 29. Working-Tree Classification
## 30. Scope Protection
## 31. Findings / Observations
## 32. Exit-Criteria Assessment
## 33. Final Repository State
## 34. Final Decision
## 35. Next Authorized Action
```

Include exact file paths, type names, visibility, method shape,
input/output types, canonical provider field, parsing formats/cultures,
ordering semantics, failure representation, validation commands/results,
and actual permanent test counts.

Clearly distinguish:

``` text
normalization implemented
HTTP transport unchanged
Application failure mapping not implemented
DI/Worker not implemented
live provider access not performed
```

------------------------------------------------------------------------

## 34. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP08 MARKET DATA NORMALIZATION COMPLETE
RELEASE 1.0 WP08 MARKET DATA NORMALIZATION COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP08 MARKET DATA NORMALIZATION BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only if every mandatory exit criterion
passes.

------------------------------------------------------------------------

## 35. Next Authorized Action

If WP08 completes successfully:

``` text
WP09 — Market Data Validation & Failure Mapping
GitHub issue #94
```

Do not execute WP09.

WP09 will own interpretation of preserved
transport/provider/normalization evidence and mapping it to the
provider-independent Application acquisition failures introduced in
WP04.

Stop after the WP08 report.

------------------------------------------------------------------------

## Execution Instruction

Read all Release 1.0 authorities and the cumulative implementation,
prove WP07 completion, determine the authoritative canonical price and
timestamp semantics, implement only the minimum Infrastructure-owned
deterministic Twelve Data successful-response normalization into
existing provider-independent Domain observation values, preserve
explicit normalization failure evidence without mapping it to
Application failures, keep HTTP/authentication/DI/Worker behavior
unchanged, use no live provider or real credential, run all mandatory
validation and targeted architecture/culture/leakage inspections,
classify the cumulative working tree, return the complete WP08 execution
report, and stop before WP09.

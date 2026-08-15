# Release 1.0 WP07 --- Provider HTTP Client --- Codex Prompt

## Role

Act as the **WP07 Provider HTTP Client Executor** for Release 1.0 of
`AIQuantTradingResearch`.

WP06 established the minimum internal Twelve Data transport model for
`/time_series`.

WP07 now owns the **Infrastructure HTTP transport boundary** needed to
request and deserialize that provider response.

This package must remain transport-focused.

Do not normalize provider values into Domain observations, do not
implement provider/HTTP failure-to-Application mapping, do not register
the provider in DI, and do not change Worker behavior.

Do not start WP08.

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
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read GitHub issues:

``` text
#91 — WP06 — Provider Transport Model
#92 — WP07 — Provider HTTP Client
#93 — WP08 — Market Data Normalization
#94 — WP09 — Market Data Validation & Failure Mapping
#95 — WP10 — Dependency Registration & Configuration
```

Read the accepted WP06 execution report from the current context.

Inspect:

``` text
src/AIQuantTradingResearch.Infrastructure/
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/
src/AIQuantTradingResearch.Application/
tests/AIQuantTradingResearch.Infrastructure.Tests/
tests/AIQuantTradingResearch.Architecture.Tests/
```

Inspect project/package configuration only as necessary to prove whether
framework-native HTTP/JSON capabilities are already available.

Authority precedence:

``` text
1. RELEASE_1.0_EXECUTION_PLAN.md
2. RELEASE_1.0_FILE_MANIFEST.md
3. WP02 provider decision/evidence
4. Accepted WP03–WP06 results
5. GitHub issue #92
6. Existing architecture/engineering conventions
7. This prompt
```

If authorities materially conflict, stop.

------------------------------------------------------------------------

## 2. Predecessor Gate

Before mutation prove:

``` text
WP01 = complete
WP02 = complete
WP03 = complete with zero Domain delta
WP04 = complete
WP05 = complete
WP06 = complete
selected provider = Twelve Data
WP06 four transport records exist and remain internal
WP07 issue = #92
```

Verify the WP06 model includes the actual transport concepts needed by
WP07.

Do not redesign WP06 DTOs unless an actual compile/runtime transport
requirement proves a minimal manifest-authorized correction is
necessary.

If such a correction is not explicitly authorized, stop.

------------------------------------------------------------------------

## 3. Objective

Implement the minimum Twelve Data HTTP client boundary required to:

``` text
construct the approved /time_series request
supply the required provider authentication transport
execute the HTTP request through HttpClient
read the HTTP response
deserialize a successful provider payload into the WP06 transport model
deserialize or otherwise preserve the structured provider error payload needed by WP09
return a transport-level result that preserves enough information for WP08 and WP09
```

The client must remain Infrastructure-owned and provider-specific.

WP07 ends at the provider transport boundary.

------------------------------------------------------------------------

## 4. Endpoint Scope

The only Release 1.0 provider endpoint authorized by this work package
is the WP02-approved historical time-series endpoint:

``` text
/time_series
```

Do not implement additional Twelve Data endpoints.

Do not add:

``` text
quote
price
exchange schedule
symbol search
websocket
streaming
technical indicators
batch APIs
```

unless a higher authority explicitly requires them.

------------------------------------------------------------------------

## 5. Request Construction

Construct only the query parameters required by the approved Release 1.0
vertical slice.

Reconcile the exact provider parameters against WP02 evidence and the
Release 1.0 execution plan.

Likely transport concerns include provider equivalents of:

``` text
symbol
interval
outputsize
timezone or related time semantics, only if explicitly required
authentication credential
```

Do not blindly model or send every Twelve Data option.

Application-owned `ResearchRequest` semantics must not be replaced by a
provider request contract.

Any translation from Application request semantics needed merely to
construct the provider request must remain Infrastructure-owned and
minimal.

Do not add provider parameters to Application contracts.

------------------------------------------------------------------------

## 6. Authentication Transport

WP07 may implement the **mechanism by which the HTTP request carries the
Twelve Data API credential**, because that is transport behavior.

However:

``` text
do not hardcode a real credential
do not commit secrets
do not log credentials
do not include credentials in exception/report text
do not add local secret files
do not modify user/system environment
```

Configuration binding and DI ownership remain WP10 unless the manifest
explicitly assigns a narrow configuration type earlier.

If WP07 needs a credential value to construct a request, design the
client so the value can later be supplied by WP10 without requiring
architectural redesign.

Do not implement full WP10 registration/configuration.

------------------------------------------------------------------------

## 7. HttpClient Ownership

Use `HttpClient` according to existing repository/.NET conventions.

Do not instantiate a new disposable `HttpClient` per request if the
intended design is DI-managed.

WP07 may define a constructor dependency on `HttpClient` when authorized
by the manifest.

Do not register that client yet; WP10 owns registration.

Do not add `IHttpClientFactory` infrastructure unless the authority
requires it.

Keep the concrete provider client as restrictive as practical, normally
`internal`.

------------------------------------------------------------------------

## 8. Transport Result Boundary

WP07 must preserve a clean distinction between:

``` text
transport acquisition
normalization
failure classification
```

The HTTP client needs a transport-level return shape sufficient for
later packages to distinguish:

``` text
successful HTTP/provider payload
structured provider error payload
HTTP status information required by WP09
malformed/unreadable payload condition required by WP09
transport exception condition required by WP09
```

But WP07 must **not** convert these conditions into:

``` text
ObservationSourceFailure.SourceUnavailable
ObservationSourceFailure.AccessDenied
ObservationSourceFailure.UsageLimitReached
ObservationSourceFailure.InvalidSourceResponse
```

That mapping belongs to WP09.

Use the exact return type/file design authorized by the Release 1.0
manifest.

If the manifest already specifies a provider-client result type, follow
it exactly.

Do not invent a broad generic networking abstraction.

------------------------------------------------------------------------

## 9. Successful Deserialization

Use the WP06 `TwelveDataTimeSeriesResponse`.

Successful deserialization must preserve the raw WP06 semantics:

``` text
metadata remains transport metadata
datetime remains raw provider text
OHLCV remains raw provider text
nullable/missing fields remain observable
```

Do not parse decimals.

Do not parse timestamps.

Do not select `close` as canonical price in WP07.

Do not construct `PriceObservation`, `ObservationSeries`, or
`MeanPrice`.

WP08 owns normalization.

------------------------------------------------------------------------

## 10. Provider Error Deserialization

Use the WP06 `TwelveDataErrorResponse` where applicable.

WP07 may deserialize the provider's structured error envelope so WP09
can classify it later.

Do not interpret provider `code`, `status`, or `message` into
Application semantics.

Do not create provider-message substring heuristics in WP07.

Do not map HTTP 401/403/429/5xx here.

Preserve evidence; classify later.

------------------------------------------------------------------------

## 11. HTTP Status Preservation

WP09 needs enough transport evidence to distinguish access denial, rate
limiting, unavailability, and invalid provider responses.

Therefore WP07 may preserve HTTP status information in an Infrastructure
transport result when authorized.

Do not expose `HttpResponseMessage` itself beyond the provider client
boundary unless the manifest explicitly requires it.

Prefer a minimal immutable transport result over leaking disposable
framework objects.

Do not let HTTP framework types cross into Application or Domain.

------------------------------------------------------------------------

## 12. Exception Boundary

Do not swallow arbitrary programming errors.

For expected network/transport exceptions that WP09 must later classify,
preserve a transport-level failure condition according to the manifest
and existing error-handling policy.

Do not convert those exceptions directly into Application failure enums
in WP07.

Do not catch `Exception` broadly unless repository policy explicitly
requires it and semantics are preserved.

Cancellation must not be accidentally converted into an ordinary
provider failure if cancellation semantics exist in the authorized
method signature.

------------------------------------------------------------------------

## 13. Cancellation / Async Semantics

Follow the exact method signature and async model required by the
execution plan/file manifest.

HTTP execution should use asynchronous .NET APIs where the authority
requires them.

If a `CancellationToken` is part of the authorized client contract:

``` text
accept it
pass it through to HTTP operations
preserve cancellation semantics
```

Do not invent a background service or retry loop.

Retry/circuit-breaker behavior is outside WP07 unless explicitly
authorized by Release 1.0 authority.

------------------------------------------------------------------------

## 14. URI Safety

Construct request URIs using safe framework mechanisms.

Avoid fragile manual concatenation when encoding is required.

Ensure symbol/query values are correctly escaped.

Do not log the full authenticated URI if it contains the API key.

If authentication is query-based per the selected provider contract,
reporting/diagnostic code must redact or avoid exposing it.

No credential may appear in the WP07 execution report.

------------------------------------------------------------------------

## 15. No Retry / Resilience Expansion

Release 1.0 WP07 is not authorization to implement:

``` text
retry policies
Polly
circuit breakers
backoff
rate-limit waiting
caching
fallback providers
multi-provider routing
telemetry pipelines
```

Do not add packages for those concerns.

Existing architecture documentation may describe future resilience; that
does not authorize implementation here.

------------------------------------------------------------------------

## 16. No Normalization

WP08 owns normalization.

WP07 must not:

``` text
parse datetime
convert timezone
parse decimal price
choose close/open/high/low
sort observations
deduplicate observations
validate requested count
validate response symbol/interval
construct Domain observations
```

The client returns provider transport evidence only.

------------------------------------------------------------------------

## 17. No Failure Mapping

WP09 owns validation and failure mapping.

WP07 must not produce Application-owned `ObservationSourceFailure`.

It must not decide:

``` text
401/403 -> AccessDenied
429 -> UsageLimitReached
5xx/network -> SourceUnavailable
malformed payload -> InvalidSourceResponse
```

WP07 only preserves the evidence needed for those later decisions.

------------------------------------------------------------------------

## 18. No Dependency Registration

WP10 owns dependency registration and configuration.

Do not modify `AddInfrastructure` or Worker composition in WP07 unless
the manifest explicitly assigns a compile-only change.

Do not add:

``` text
AddHttpClient
options binding
environment-variable reading
API-key configuration
base-address registration
```

as runtime composition.

The WP07 concrete client may be constructible manually for later tests
and WP10 registration.

------------------------------------------------------------------------

## 19. Visibility / Layering

Provider client and provider transport result types should remain
Infrastructure-owned and as restrictive as practical.

Do not make provider types public for convenience.

Do not add provider types to Application.

Do not add Infrastructure references to Domain/Application.

Do not add a new project.

------------------------------------------------------------------------

## 20. Authorized Files

`RELEASE_1.0_FILE_MANIFEST.md` is the exact file authority.

Create/modify only WP07-authorized files.

Expected changes should be concentrated under the Twelve Data
Infrastructure boundary.

If the manifest authorizes specific files, use those exact paths/names.

Do not modify WP06 records unless explicitly necessary and authorized.

Do not modify Application, Domain, Worker, test projects, project
manifests, package manifests, or build policy unless the manifest
explicitly authorizes the exact change.

If a required compile dependency would exceed the manifest, stop.

------------------------------------------------------------------------

## 21. Test-Scope Discipline

WP13 owns comprehensive Infrastructure/provider tests.

Do not pull WP13 forward.

WP07 may perform only manifest-authorized narrow tests or compile
validation.

Existing Infrastructure and Architecture tests must remain green.

Do not make a live Twelve Data request as canonical validation.

Do not require a real API key for repository verification.

Build/test/verify must remain deterministic and offline-compatible.

------------------------------------------------------------------------

## 22. Determinism Requirement

The repository's canonical validation must not depend on:

``` text
internet access
Twelve Data availability
real API credentials
current market data
wall-clock-sensitive provider behavior
```

WP07 production code may implement real HTTP capability, but canonical
validation must not execute the live provider.

If a test is authorized, use a deterministic test-owned HTTP
handler/fixture rather than the live network.

------------------------------------------------------------------------

## 23. Dependency / Package Protection

Prefer framework-native:

``` text
System.Net.Http
System.Text.Json
```

No new package should be necessary.

Prove:

``` text
new NuGet packages = 0
new project references = 0
```

unless the Release 1.0 manifest explicitly authorizes otherwise.

Do not weaken warnings/analyzers.

------------------------------------------------------------------------

## 24. Security Inspection

Before completion prove:

``` text
hardcoded real API keys = 0
committed secret files = 0
credential logging = 0
credential exposure in exceptions/report = 0
Domain/Application provider credential references = 0
```

If any credential is discovered in the cumulative working tree, do not
reproduce it in the report. Treat it according to repository security
policy and stop if necessary.

------------------------------------------------------------------------

## 25. Transport Inventory

Report every WP07 production type added/modified with:

``` text
path
type
visibility
constructor dependencies
method signatures
transport responsibility
downstream evidence preserved for WP08/WP09
```

Also report the exact `/time_series` query parameters implemented.

Do not include actual secret values.

------------------------------------------------------------------------

## 26. Validation

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

Record actual test counts.

Do not claim live-provider validation unless explicitly authorized and
actually performed; live calls are not required for WP07 acceptance.

Known `NU1900` connectivity warnings remain non-blocking only under the
established repository condition and when all mandatory checks pass.

------------------------------------------------------------------------

## 27. Targeted Static Inspection

Prove:

``` text
Twelve Data HTTP implementation exists only in Infrastructure
/time_series is the only implemented provider endpoint
Domain Twelve Data references = 0
Application Twelve Data references = 0
Application HttpClient references = 0
Domain HttpClient references = 0
Application JSON transport references = 0
Domain JSON transport references = 0
ObservationSourceFailure mapping in WP07 = 0
Domain observation construction in WP07 = 0
DI registration changes in WP07 = 0
Worker changes in WP07 = 0
```

Also inspect for accidental secret literals.

------------------------------------------------------------------------

## 28. Regression Protection

Prove cumulative accepted behavior remains intact:

``` text
WP03 Domain delta remains zero
WP04 failure vocabulary remains intact
WP05 six failure mappings remain intact
WP06 four transport records remain intact except authorized correction if any
existing deterministic observation source behavior unchanged
existing Worker behavior unchanged
existing 41-test baseline remains green unless authorized tests legitimately increase it
```

Do not present an increased test count unless tests were actually
authorized and added.

------------------------------------------------------------------------

## 29. Git / GitHub Protection

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
issue #92
milestone #41
Project fields/status
labels
Release 1.1 planning
```

WP07 ends as a validated local cumulative candidate.

------------------------------------------------------------------------

## 30. Working-Tree Classification

At completion classify non-clean state as:

``` text
EXPECTED GOVERNANCE
PRE-EXISTING AUTHORIZED
WP02 AUTHORIZED
WP03 AUTHORIZED
WP04 AUTHORIZED
WP05 AUTHORIZED
WP06 AUTHORIZED
WP07 AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

List exact WP07 files separately.

Nothing should be staged.

Unexpected changes must be investigated.

------------------------------------------------------------------------

## 31. Acceptance / Exit Criteria

WP07 is complete only if:

``` text
WP06 predecessor gate = PASS
Twelve Data /time_series HTTP boundary implemented
required query construction implemented
authentication transport implemented without hardcoded secrets
HttpClient execution implemented
successful response acquisition implemented
successful payload deserialization uses WP06 model
structured provider error payload preserved/deserialized as authorized
HTTP/transport evidence needed by WP09 preserved
transport result remains Infrastructure-owned
normalization implemented = NO
Domain observation construction = NO
Application failure mapping = NO
DI registration = NO
Worker behavior changes = NO
live network required for verification = NO
real credential required for verification = NO
new packages = 0 unless explicitly authorized
new project references = 0
Domain/Application provider leakage = 0
secret leakage = 0
build = PASS with zero errors
required Infrastructure tests = PASS
architecture tests = PASS
canonical verification = PASS
git diff --check = PASS
git diff --cached --check = PASS
unexpected mutations = 0
WP08 started = NO
```

If the approved HTTP boundary cannot be implemented without prematurely
performing WP08/WP09/WP10 responsibilities, return `BLOCKED`.

------------------------------------------------------------------------

## 32. Required Execution Report

Return:

``` text
# Release 1.0 WP07 — Provider HTTP Client Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP06 Predecessor Gate
## 5. Existing Twelve Data Transport Baseline
## 6. HTTP Client Design Reconciliation
## 7. Request / Endpoint Construction
## 8. Authentication Transport
## 9. HttpClient Execution
## 10. Success Payload Handling
## 11. Error Payload Preservation
## 12. HTTP / Transport Evidence Preservation
## 13. Cancellation / Exception Semantics
## 14. Transport Types Added or Modified
## 15. Files Changed
## 16. Security / Secret Inspection
## 17. Normalization Exclusion Evidence
## 18. Failure-Mapping Exclusion Evidence
## 19. DI / Worker Exclusion Evidence
## 20. Application / Domain Leakage Scan
## 21. Dependency / Architecture Evidence
## 22. Build Evidence
## 23. Test Evidence
## 24. Canonical Verification
## 25. Diff / Formatting Validation
## 26. Regression Evidence
## 27. Working-Tree Classification
## 28. Scope Protection
## 29. Findings / Observations
## 30. Exit-Criteria Assessment
## 31. Final Repository State
## 32. Final Decision
## 33. Next Authorized Action
```

Include exact file paths, type names, visibility, constructor/method
shape, query parameters, transport result semantics, validation
commands/results, and actual test counts.

Never include an actual API key or authenticated request URI.

Clearly distinguish:

``` text
HTTP transport implemented
live provider call performed or not performed
normalization not implemented
failure mapping not implemented
```

------------------------------------------------------------------------

## 33. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP07 PROVIDER HTTP CLIENT COMPLETE
RELEASE 1.0 WP07 PROVIDER HTTP CLIENT COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP07 PROVIDER HTTP CLIENT BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only if every mandatory exit criterion
passes.

------------------------------------------------------------------------

## 34. Next Authorized Action

If WP07 completes successfully, the next package is:

``` text
WP08 — Market Data Normalization
GitHub issue #93
```

Do not execute WP08.

WP08 will own conversion of the preserved Twelve Data transport
representation into canonical provider-independent values required by
the existing research boundary.

Stop after the WP07 report.

------------------------------------------------------------------------

## Execution Instruction

Read all authorities and current cumulative Release 1.0 code, prove WP06
completion, implement only the minimum Infrastructure-owned Twelve Data
`/time_series` HTTP client and transport-result boundary required to
construct authenticated requests, execute `HttpClient`,
deserialize/preserve successful and error transport evidence, keep
normalization/failure mapping/DI/Worker behavior out of scope, require
no live provider or real credential for canonical verification, run all
mandatory validation and security/leakage inspections, classify the
cumulative working tree, return the complete WP07 execution report, and
stop before WP08.

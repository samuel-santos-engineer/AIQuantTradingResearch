# Release 1.0 WP09 --- Market Data Validation & Failure Mapping --- Codex Prompt

## Role

Act as the **WP09 Market Data Validation & Failure Mapping Executor**
for Release 1.0 of `AIQuantTradingResearch`.

WP06 established the internal Twelve Data transport model.

WP07 established the `/time_series` HTTP client and preserves
transport/provider evidence.

The WP08 semantic unblock established:

``` text
canonical price = close
adjustment = splits
daily timestamp = exchange-local date anchored at 00:00 with resolved exchange offset
normalized order = ascending
duplicate instant = normalization failure
```

WP08 then established deterministic successful-response normalization
into existing `PriceObservation` values while preserving
Infrastructure-local normalization failures.

WP09 now owns the missing integration boundary:

``` text
Application IObservationSource
        ↓
Infrastructure Twelve Data adapter
        ↓
TwelveDataClient
        ↓
TwelveDataTransportResult
        ↓
transport/provider validation
        ↓
TwelveDataTimeSeriesNormalizer
        ↓
normalized observations / normalization failure
        ↓
ObservationSourceResult
```

WP09 must interpret provider, HTTP, transport, payload, normalization,
target, and observation-count evidence and map it into the **existing
provider-independent Application acquisition vocabulary** introduced in
WP04.

Do not change the Application failure vocabulary.

Do not implement DI/configuration; that belongs to WP10.

Do not change Worker execution; that belongs to WP11.

Do not pull permanent WP13 provider-test scope forward unless the file
manifest explicitly authorizes a test.

Do not start WP10.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before mutation:

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
docs/roadmap/release-1.0/prompts/08-market-data-normalization-semantic-unblock-codex-prompt.md
docs/roadmap/release-1.0/prompts/09-market-data-validation-failure-mapping-codex-prompt.md

docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read the accepted WP08 semantic-unblock and resumed WP08 execution
reports from the current context.

Read GitHub issues:

``` text
#94 — WP09 — Market Data Validation & Failure Mapping
#95 — WP10 — Dependency Registration & Configuration
#96 — WP11 — Worker Market Data Execution
#98 — WP13 — Infrastructure & Provider Tests
```

Inspect the actual cumulative code, especially:

``` text
src/AIQuantTradingResearch.Application/
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/
tests/AIQuantTradingResearch.Architecture.Tests/
```

Authority precedence:

``` text
1. RELEASE_1.0_EXECUTION_PLAN.md
2. RELEASE_1.0_FILE_MANIFEST.md
3. WP08 semantic-unblock authority for resolved market semantics
4. WP02 provider assessment/decision
5. Accepted WP03–WP08 cumulative implementation
6. GitHub issue #94
7. Existing architecture/engineering conventions
8. This prompt
```

If a material conflict remains, stop.

------------------------------------------------------------------------

## 2. Predecessor Gate

Before mutation prove:

``` text
WP01–WP08 = complete
provider = Twelve Data
WP03 Domain delta = zero
WP04 Application acquisition failure vocabulary exists
WP05 ResearchUseCase propagates all authorized source failures
WP06 transport records exist
WP07 client and transport result exist
WP07 request includes adjust=splits
WP08 normalizer exists
WP08 normalization result/failure evidence exists
WP08 does not map to ObservationSourceFailure
WP09 issue = #94
WP10 has not started
```

Verify actual names, members, visibility, and signatures from repository
truth.

Do not assume them from this prompt when code can be inspected.

------------------------------------------------------------------------

# 3. Objective

Implement the minimum Infrastructure adapter that fulfills the existing
Application `IObservationSource` contract through Twelve Data.

The adapter must:

``` text
receive the existing provider-independent acquisition request
validate provider-independent target/count prerequisites assigned to Infrastructure
invoke TwelveDataClient exactly once when acquisition is eligible
interpret transport/provider evidence deterministically
invoke TwelveDataTimeSeriesNormalizer only for an eligible successful payload
interpret normalization evidence deterministically
validate normalized observation sufficiency
return the existing provider-independent ObservationSourceResult
```

The adapter is the anti-corruption boundary between:

``` text
Twelve Data mechanics
```

and:

``` text
Application acquisition semantics
```

Provider-specific evidence must stop here.

------------------------------------------------------------------------

# 4. Existing Application Contract Is Authoritative

Reuse the exact WP04 Application contracts.

Do not add a new failure enum.

Do not add Twelve Data concepts to Application.

The authorized source failures are:

``` text
UnsupportedTarget
InsufficientObservations
SourceUnavailable
AccessDenied
UsageLimitReached
InvalidSourceResponse
```

The existing success/result shape must be reused exactly.

Do not modify `ResearchFailure` or `ResearchUseCase` unless a proven
compile defect caused by an authority conflict requires stopping.

Target outcome: **zero Application source-contract changes in WP09**.

------------------------------------------------------------------------

# 5. Adapter Ownership

The Twelve Data observation-source adapter belongs in Infrastructure.

Use the exact file/type name and path authorized by
`RELEASE_1.0_FILE_MANIFEST.md`.

Expected conceptual shape:

``` text
internal sealed class <TwelveDataObservationSource> : IObservationSource
```

but repository manifest truth controls the exact name.

The adapter should depend on the already accepted WP07 client boundary,
not create its own `HttpClient`.

The adapter should reuse the WP08 normalizer, not duplicate parsing.

------------------------------------------------------------------------

# 6. End-to-End Adapter Flow

The implementation must make the flow explicit and reviewable:

``` text
Application request
      ↓
provider target compatibility
      ↓
TwelveDataClient request
      ↓
TwelveDataTransportResult
      ↓
transport/provider classification
      ↓
successful TwelveDataTimeSeriesResponse
      ↓
TwelveDataTimeSeriesNormalizer
      ↓
normalization result
      ↓
count validation
      ↓
ObservationSourceResult
```

Do not merge HTTP, normalization, and Application failure semantics into
one opaque method if small private classification helpers make the
boundary clearer.

Do not introduce a general mapping framework.

------------------------------------------------------------------------

# 7. Target Compatibility

Resolve the exact target semantics from the existing Application request
and WP02 provider decision.

The adapter must not silently transform arbitrary target formats.

If Release 1.0 authority establishes a bounded target/symbol format,
enforce only that boundary.

Map a target that is definitively outside the selected provider/release
capability to:

``` text
ObservationSourceFailure.UnsupportedTarget
```

Do not use `UnsupportedTarget` for:

``` text
authentication failure
rate limiting
network failure
server failure
malformed payload
normalization failure
insufficient returned rows
```

If provider response evidence, rather than local syntax, is required to
determine unsupported symbol semantics, use the provider-error mapping
rules below.

Do not add market-symbol lookup or discovery.

------------------------------------------------------------------------

# 8. Requested Count

Preserve the Application request semantics.

Do not reinterpret a non-positive requested count if Application already
rejects it before calling the source.

The Infrastructure adapter may defensively reject impossible direct
calls only if the existing contract requires it.

For a successful normalized response:

``` text
normalized count >= requested count
    → eligible success

normalized count < requested count
    → InsufficientObservations
```

Do not manufacture observations.

Do not repeat observations.

Do not lower the requested count.

Do not classify an empty but structurally valid normalized collection as
`InvalidSourceResponse` merely because it is empty; count policy owns
that outcome unless authority says otherwise.

If more observations are returned than requested, follow the execution
plan/contract. Do not invent truncation if it is not authorized.

------------------------------------------------------------------------

# 9. Failure-Mapping Principle

Map from **evidence**, not guesses.

Classification priority must be deterministic.

A condition must not be classified differently depending on incidental
exception text, current culture, machine state, or dictionary ordering.

Provider-specific evidence must not escape in the returned Application
result.

Preserve diagnostics only through existing Infrastructure-local
mechanisms if already authorized.

Do not add logging merely to expose provider messages.

------------------------------------------------------------------------

# 10. Transport Exception Mapping

For a preserved `HttpRequestException` or equivalent accepted WP07
transport-unavailability evidence:

``` text
→ ObservationSourceFailure.SourceUnavailable
```

This includes inability to complete the HTTP exchange where no
authoritative provider response exists.

Do not map transport exceptions to:

``` text
InvalidSourceResponse
AccessDenied
UsageLimitReached
```

based solely on exception-message text.

Cancellation must preserve established cancellation semantics.

Do not convert caller cancellation into `SourceUnavailable` unless the
existing WP07/Application authority explicitly requires it.

------------------------------------------------------------------------

# 11. HTTP Status Mapping

Use the actual `HttpStatusCode?` evidence preserved by WP07.

At minimum:

``` text
401 Unauthorized
403 Forbidden
    → AccessDenied

429 TooManyRequests
    → UsageLimitReached

5xx server responses
    → SourceUnavailable
```

For other non-success HTTP statuses, combine status and structured
provider evidence according to the provider decision and rules below.

Do not make a 2xx status sufficient for success if the payload is an
error envelope, unreadable, missing required structure, or fails
normalization.

Do not make a non-2xx response successful merely because a success DTO
happened to deserialize.

------------------------------------------------------------------------

# 12. Unreadable Payload Mapping

If WP07 reports that the response body could not be interpreted as the
expected success/error JSON representation:

``` text
→ InvalidSourceResponse
```

unless a stronger preserved transport status already authoritatively
determines:

``` text
AccessDenied
UsageLimitReached
SourceUnavailable
```

Define and implement the precedence explicitly.

Example principle:

``` text
429 + unreadable body → UsageLimitReached
500 + unreadable body → SourceUnavailable
200 + unreadable body → InvalidSourceResponse
```

Do not require provider error JSON when the HTTP status itself is
sufficient.

------------------------------------------------------------------------

# 13. Structured Provider Error Mapping

Inspect the actual WP06 `TwelveDataErrorResponse` fields and WP02
provider evidence.

Do not invent provider codes.

Build a narrow mapping only from provider error evidence actually
established by the accepted provider research or current official Twelve
Data documentation where the Release 1.0 authority requires it.

The desired Application meanings are:

``` text
unsupported/invalid requested symbol or unsupported target capability
    → UnsupportedTarget

credential/authentication/permission failure
    → AccessDenied

quota/rate-limit/credits exhaustion
    → UsageLimitReached

provider temporary/server/service availability failure
    → SourceUnavailable

provider error not safely classifiable into the above
    → InvalidSourceResponse
```

HTTP status remains authoritative where it directly establishes the
category.

Do not classify based on broad substring matching such as:

``` text
message.Contains("limit")
message.Contains("key")
message.Contains("symbol")
```

unless exact provider documentation/accepted evidence authorizes that
mechanism.

Prefer stable provider status/code fields.

If the accepted provider evidence is insufficient to distinguish a
required provider-error category safely, stop as `BLOCKED` rather than
inventing heuristics.

------------------------------------------------------------------------

# 14. Success-Payload Eligibility

The normalizer may run only when transport/provider validation
establishes a success-payload candidate.

At minimum ensure:

``` text
no transport exception
HTTP response is eligible for success
payload is readable
structured provider error does not represent failure
success response exists
```

Then call:

``` text
TwelveDataTimeSeriesNormalizer.Normalize(...)
```

exactly once.

Do not normalize error DTOs.

Do not normalize a null success payload.

Do not reconstruct the success DTO from raw JSON in WP09.

------------------------------------------------------------------------

# 15. Normalization Failure Mapping

WP08 intentionally preserves Infrastructure-local normalization
failures.

WP09 now owns their Application classification.

The general Release 1.0 rule is:

``` text
a provider response that reached the success-payload path
but cannot be converted into the authoritative Domain observation semantics
    → ObservationSourceFailure.InvalidSourceResponse
```

Therefore map WP08 structural/semantic normalization failures such as:

``` text
missing metadata
missing exchange timezone
unresolvable exchange timezone
missing values collection
null row
missing/malformed date
invalid/ambiguous local anchor
missing/malformed/non-positive close
duplicate normalized instant
Domain invariant rejection caused by provider value
```

to:

``` text
InvalidSourceResponse
```

unless the accepted WP08 result includes a distinct condition explicitly
assigned to count policy.

Do not leak `TwelveDataNormalizationFailure` to Application.

Do not create one Application enum member per normalization failure.

------------------------------------------------------------------------

# 16. Empty Collection and Sufficiency

WP08 deliberately treats an empty values collection as a successful
empty normalized result so WP09 owns no-observation policy.

Therefore:

``` text
normalized observations count < requested count
    → InsufficientObservations
```

This includes zero observations for a positive requested count.

This count check occurs **after successful normalization**.

Do not map a valid empty collection to `InvalidSourceResponse`.

A missing values collection is structurally different from an empty
values collection and remains `InvalidSourceResponse`.

------------------------------------------------------------------------

# 17. Failure Precedence

Implement a deterministic precedence consistent with the evidence model.

The exact code structure may differ, but the semantic order should be
equivalent to:

``` text
1. caller cancellation semantics
2. transport exception
3. authoritative HTTP status categories
4. unreadable payload
5. structured provider error
6. missing/inconsistent success payload
7. normalization failure
8. normalized observation sufficiency
9. success
```

If current WP07 transport-result invariants make some states mutually
exclusive, document that.

If contradictory transport evidence can be constructed, choose a safe
deterministic classification and report the invariant assumption.

Do not let malformed provider payload override a clearly authoritative
401/403/429/5xx classification.

------------------------------------------------------------------------

# 18. Cancellation

Inspect WP07 behavior.

If cancellation is propagated by `TwelveDataClient`, preserve it.

Do not catch `OperationCanceledException` merely to convert it into an
acquisition failure unless Release 1.0 authority explicitly says so.

Target:

``` text
caller cancellation remains cancellation
```

and is not represented as provider unavailability.

------------------------------------------------------------------------

# 19. Provider Error Evidence Verification

Because WP09 is where provider-specific error evidence becomes
Application semantics, verify the required mapping against:

``` text
accepted WP02 provider research
and, if needed, current official Twelve Data documentation
```

Use web/provider documentation only to confirm stable error/status
semantics needed for implementation.

Do not perform live authenticated API experiments.

Do not expose credentials.

If web documentation contradicts the accepted provider assessment
materially, stop and report the conflict.

------------------------------------------------------------------------

# 20. No Retry / Resilience Expansion

WP09 does not own retries, backoff, circuit breakers, caching, fallback
providers, or resilience pipelines.

Do not add:

``` text
Polly
retry loops
sleep/delay
fallback HTTP calls
provider failover
cache
```

A source-unavailable result is enough for this release boundary.

------------------------------------------------------------------------

# 21. No Configuration / DI

WP10 owns registration and configuration.

Do not modify:

``` text
DependencyInjection.cs
Program.cs
appsettings*.json
launch settings
environment binding
options classes
HttpClient registration
base-address registration
API-key registration
```

unless the Release 1.0 file manifest explicitly assigns a WP09 file
there, in which case stop and reconcile with WP10 authority before
changing it.

The WP09 adapter may accept dependencies through its constructor.

Registration waits for WP10.

------------------------------------------------------------------------

# 22. No Worker Work

WP11 owns the executable market-data path.

Do not modify Worker code.

Do not change console output.

Do not make the Worker invoke Twelve Data.

Do not read a credential from Worker.

------------------------------------------------------------------------

# 23. Authorized Files

`RELEASE_1.0_FILE_MANIFEST.md` is exact file authority.

Create/modify only WP09-authorized files.

Expected WP09 production scope is the Infrastructure Twelve Data
observation-source adapter and any narrowly authorized
Infrastructure-local mapper/helper required by the manifest.

Do not modify accepted WP06--WP08 types for cleanup.

If an actual compile need requires modifying a predecessor file not
authorized for WP09, stop and report the need.

Do not modify:

``` text
Domain
Application
Worker
project files
package files
solution
engineering scripts
GitHub workflows
```

unless explicitly authorized.

------------------------------------------------------------------------

# 24. Visibility

Provider-specific concrete implementation should remain non-public
according to the existing architecture policy.

Prefer:

``` text
internal sealed
```

for the concrete adapter and provider-specific mapping helpers when
compatible with the manifest.

Do not broaden visibility for future tests during WP09.

WP13 owns Infrastructure/provider test coverage and may require a
separate explicit testability unblock if necessary.

------------------------------------------------------------------------

# 25. Permanent Test Scope

WP13 owns comprehensive Infrastructure/provider tests.

Do not add the full mapping matrix to permanent tests during WP09 unless
the manifest explicitly assigns tests here.

A temporary deterministic probe is allowed to validate WP09 behavior if
necessary.

It must:

``` text
use no live network
use no real API key
use deterministic fake/stub HTTP behavior or accepted transport seams
exercise only WP09 behavior
be removed before completion
```

Do not weaken visibility or production design solely for a temporary
probe.

Report exactly what was proven by permanent tests versus temporary
evidence versus inspection.

------------------------------------------------------------------------

# 26. Required Mapping Matrix

The execution report must provide the **actual implemented matrix**,
including at least:

  -----------------------------------------------------------------------
  Evidence                            Application outcome
  ----------------------------------- -----------------------------------
  unsupported target/provider         `UnsupportedTarget`
  unsupported-symbol evidence

  normalized count below requested    `InsufficientObservations`
  count

  `HttpRequestException`              `SourceUnavailable`

  HTTP 401                            `AccessDenied`

  HTTP 403                            `AccessDenied`

  HTTP 429                            `UsageLimitReached`

  HTTP 5xx                            `SourceUnavailable`

  readable but unclassifiable         `InvalidSourceResponse`
  provider error

  unreadable eligible payload         `InvalidSourceResponse`

  missing/inconsistent success        `InvalidSourceResponse`
  payload

  WP08 normalization failure          `InvalidSourceResponse`

  eligible normalized count           success
  -----------------------------------------------------------------------

Add exact provider-code/status rows if the accepted evidence supports
them.

Do not claim mappings not present in code.

------------------------------------------------------------------------

# 27. Application Propagation Compatibility

WP05 already maps all six `ObservationSourceFailure` values one-to-one
into `ResearchFailure`.

Prove WP09 does not require any Application changes.

The final chain must be:

``` text
Twelve Data evidence
    ↓
WP09 ObservationSourceFailure
    ↓
existing WP05 ResearchFailure mapping
```

No provider-specific branching belongs in `ResearchUseCase`.

------------------------------------------------------------------------

# 28. Security

Prove:

``` text
hardcoded API key = 0
API key in query = 0
API key in Application/Domain = 0
credential logging = 0
provider response logging containing secrets = 0
live authenticated request = NO
```

Do not add error messages that echo request headers or secret-bearing
configuration.

------------------------------------------------------------------------

# 29. Static Leakage Inspection

After implementation prove:

``` text
Domain Twelve Data references = 0
Application Twelve Data references = 0
Application HttpClient references = 0
Application HTTP-status references = 0
Application provider-code references = 0
ResearchUseCase provider-specific branches = 0
Worker Twelve Data references introduced by WP09 = 0
WP09 DI changes = 0
WP09 configuration changes = 0
```

Provider mechanics remain Infrastructure-only.

------------------------------------------------------------------------

# 30. Architecture / Dependency Protection

Production graph must remain:

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
Application → Infrastructure = 0
Domain → Infrastructure = 0
```

Architecture.Tests must remain green.

------------------------------------------------------------------------

# 31. Regression Protection

Prove all accepted predecessor behavior remains intact:

``` text
WP03 Domain delta remains zero
WP04 six source failures unchanged
WP05 six source-to-research mappings unchanged
WP06 transport records unchanged
WP07 /time_series behavior unchanged
WP07 adjust=splits unchanged
WP07 header authentication unchanged
WP08 close normalization unchanged
WP08 timestamp semantics unchanged
WP08 ascending ordering unchanged
WP08 duplicate behavior unchanged
existing deterministic observation source unchanged unless manifest explicitly authorizes replacement in WP09
Worker unchanged
```

Do not remove the deterministic source merely because a real adapter now
exists.

WP10 decides composition.

------------------------------------------------------------------------

# 32. Build and Validation

Run exact Release 1.0 validation.

At minimum:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
git diff --cached --check
```

Record actual counts.

Canonical verification must remain independent of live provider access.

Known `NU1900` vulnerability-feed connectivity warnings are non-blocking
only under the established repository condition and when mandatory
validation passes.

------------------------------------------------------------------------

# 33. Temporary Probe Expectations

If a temporary probe is needed, aim to prove the highest-risk
classification branches without making a permanent test suite.

Useful deterministic cases include:

``` text
valid success
count insufficient
401
403
429
500
HttpRequestException
unreadable 200 payload
provider unsupported-symbol error if exact stable evidence exists
unclassifiable provider error
normalization failure
```

Do not fabricate provider error codes merely to make a probe pass.

If exact unsupported-symbol provider evidence cannot be established,
that is a potential blocker because `UnsupportedTarget` must be mapped
safely.

Remove the probe before final state.

------------------------------------------------------------------------

# 34. Diff / Formatting Validation

Run:

``` text
git diff --check
git diff --cached --check
```

Nothing should be staged.

Do not normalize unrelated files.

Do not modify line-ending policy.

------------------------------------------------------------------------

# 35. Git / GitHub Protection

Do not:

``` text
stage
commit
branch
push
create PR
merge
stash
discard cumulative work
```

Do not mutate:

``` text
issue #94
milestone #41
Project fields/status
labels
Release 1.1 planning
```

WP09 ends as a validated local cumulative candidate.

------------------------------------------------------------------------

# 36. Working-Tree Classification

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
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

List exact WP09 files separately.

Staged files must be zero.

Unexpected changes must be investigated before completion.

------------------------------------------------------------------------

# 37. Acceptance / Exit Criteria

WP09 is complete only if:

``` text
WP08 predecessor gate = PASS
existing IObservationSource contract reused
existing ObservationSourceResult reused
existing six ObservationSourceFailure values reused unchanged
Twelve Data adapter implemented in Infrastructure
TwelveDataClient reused
TwelveDataTimeSeriesNormalizer reused
live HTTP required for validation = NO
transport exception → SourceUnavailable
401/403 → AccessDenied
429 → UsageLimitReached
5xx → SourceUnavailable
unreadable eligible payload → InvalidSourceResponse
unclassifiable provider error → InvalidSourceResponse
normalization failure → InvalidSourceResponse
normalized count below requested → InsufficientObservations
unsupported target mapping based on authoritative evidence
eligible normalized response → success
caller cancellation semantics preserved
provider-specific Application leakage = 0
provider-specific Domain leakage = 0
ResearchUseCase provider branches = 0
DI changes = 0
Worker changes = 0
new packages = 0
new project references = 0
build = PASS with zero errors
required tests = PASS
architecture tests = PASS
canonical verification = PASS
git diff --check = PASS
git diff --cached --check = PASS
unexpected mutations = 0
WP10 started = NO
```

If authoritative evidence is insufficient to map
unsupported-target/provider error semantics without heuristics, return
`BLOCKED`.

If implementation requires changing the WP04 failure vocabulary, return
`BLOCKED`.

If implementation requires beginning WP10 registration/configuration,
return `BLOCKED`.

------------------------------------------------------------------------

# 38. Required Execution Report

Return:

``` text
# Release 1.0 WP09 — Market Data Validation & Failure Mapping Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP08 Predecessor Gate
## 5. Existing Application / Transport / Normalization Baseline
## 6. Adapter Design
## 7. Target Compatibility
## 8. Transport Exception Classification
## 9. HTTP Status Classification
## 10. Provider Error Classification
## 11. Unreadable / Inconsistent Payload Classification
## 12. Normalization Failure Classification
## 13. Observation Sufficiency Classification
## 14. Cancellation Semantics
## 15. Failure Precedence
## 16. Implemented Mapping Matrix
## 17. Success Path
## 18. Types Added or Modified
## 19. Files Changed
## 20. Application Contract Preservation
## 21. Provider Leakage Scan
## 22. Security Evidence
## 23. DI / Worker Exclusion Evidence
## 24. Dependency / Architecture Evidence
## 25. Build Evidence
## 26. Test / Temporary-Probe Evidence
## 27. Canonical Verification
## 28. Diff / Formatting Validation
## 29. Regression Evidence
## 30. Working-Tree Classification
## 31. Scope Protection
## 32. Findings / Observations
## 33. Exit-Criteria Assessment
## 34. Final Repository State
## 35. Final Decision
## 36. Next Authorized Action
```

Include exact:

``` text
adapter file/type
visibility
constructor dependencies
interface implemented
request/result types
mapping helpers if any
provider error fields/codes actually used
classification precedence
normalization failure treatment
count rule
validation commands/results
per-suite permanent test counts
temporary-probe cases/results if used
```

Clearly distinguish:

``` text
transport acquired by WP07
normalization performed by WP08
failure interpretation performed by WP09
DI/configuration not implemented
Worker integration not implemented
```

------------------------------------------------------------------------

# 39. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP09 MARKET DATA VALIDATION AND FAILURE MAPPING COMPLETE
RELEASE 1.0 WP09 MARKET DATA VALIDATION AND FAILURE MAPPING COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP09 MARKET DATA VALIDATION AND FAILURE MAPPING BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when every mandatory exit
criterion passes.

------------------------------------------------------------------------

# 40. Next Authorized Action

If WP09 completes successfully:

``` text
WP10 — Dependency Registration & Configuration
GitHub issue #95
```

Do not execute WP10.

WP10 will own composition/configuration of the accepted Application and
Twelve Data Infrastructure boundaries.

Stop after the WP09 execution report.

------------------------------------------------------------------------

## Execution Instruction

Read all Release 1.0 authorities and cumulative WP02--WP08
implementation, prove the WP08 gate, implement only the minimum
Infrastructure Twelve Data `IObservationSource` adapter and
deterministic validation/failure-classification boundary, reuse the
accepted WP07 client and WP08 normalizer, map preserved
transport/provider/normalization/count evidence into the existing six
provider-independent `ObservationSourceFailure` values without changing
Application contracts, preserve cancellation and security semantics, do
not add DI/configuration or Worker behavior, do not rely on live
provider access, run all mandatory validation and targeted
leakage/mapping inspections, classify the complete working tree, return
the full WP09 execution report, and stop before WP10.

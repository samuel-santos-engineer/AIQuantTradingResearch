# Release 1.0 WP05 --- Historical Market Data Use-Case Integration --- Codex Prompt

## Role

Act as the **WP05 Historical Market Data Use-Case Integration Executor**
for Release 1.0 of `AIQuantTradingResearch`.

WP04 established that the existing Release 0.9 Application
request/acquisition seam is already sufficient for the approved
historical market-data slice. WP04 therefore added no parallel
market-data abstraction and evolved only provider-independent failure
vocabulary.

Your job is now to integrate those approved historical-source semantics
into the existing Application use case with the **smallest behavior
change possible**.

This is Application orchestration work.

It is **not** provider transport work.

Do not start WP06, WP07, WP08, WP09, or WP10.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before acting:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
docs/roadmap/release-1.0/prompts/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.0/prompts/02-market-data-provider-discovery-codex-prompt.md
docs/roadmap/release-1.0/prompts/03-market-data-domain-evolution-codex-prompt.md
docs/roadmap/release-1.0/prompts/04-market-data-application-contracts-codex-prompt.md
docs/roadmap/release-1.0/prompts/05-historical-market-data-use-case-integration-codex-prompt.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read GitHub issues:

``` text
#89 — WP04 — Market Data Application Contracts
#90 — WP05 — Historical Market Data Use-Case Integration
#91 — WP06 — Provider Transport Model
#94 — WP09 — Market Data Validation & Failure Mapping
#95 — WP10 — Dependency Registration & Configuration
```

Read the accepted WP04 execution report from the current context.

Inspect completely enough to understand the actual current
implementation:

``` text
src/AIQuantTradingResearch.Application/
tests/AIQuantTradingResearch.Application.Tests/
relevant Domain contracts used by ResearchUseCase
Application project file/references
architecture tests relevant to visibility/dependencies
```

Use actual repository names and locations.

Authority precedence:

``` text
1. RELEASE_1.0_EXECUTION_PLAN.md
2. RELEASE_1.0_FILE_MANIFEST.md
3. Accepted WP04 contract result
4. WP02 provider decision as factual Infrastructure evidence
5. GitHub issue #90
6. Existing architecture/engineering authorities
7. This prompt
```

If authorities materially conflict, stop.

------------------------------------------------------------------------

## 2. Predecessor Gate

Before mutation, prove:

``` text
WP01 = complete
WP02 = complete
WP03 = complete with zero Domain delta
WP04 = complete
issue #89 reflects completed predecessor state as applicable
issue #90 is the current authorized package
```

Prove the current Application contract baseline includes the existing
provider-independent acquisition seam and the WP04 failure vocabulary.

WP04 reported these source-level meanings:

``` text
UnsupportedTarget
InsufficientObservations
SourceUnavailable
AccessDenied
UsageLimitReached
InvalidSourceResponse
```

and these research-level meanings:

``` text
InvalidRequest
UnsupportedTarget
InsufficientObservations
SourceUnavailable
AccessDenied
UsageLimitReached
InvalidSourceResponse
```

Verify repository truth rather than trusting this list blindly.

If WP04 changes are absent, inconsistent, or outside authority, stop.

------------------------------------------------------------------------

## 3. Objective

Evolve the existing `ResearchUseCase` behavior so the Application layer
can correctly consume the historical observation-source contract
established by WP04.

The use case must:

``` text
validate its Application-owned request according to existing rules
call the existing IObservationSource boundary
preserve successful Release 0.9 research behavior
propagate/map all authorized provider-independent source failures into the corresponding research-level failure outcomes
continue constructing canonical Domain values through the existing Domain model
continue producing the existing successful ResearchResult semantics
```

The behavior must remain independent of the concrete provider.

No Twelve Data logic belongs in WP05.

------------------------------------------------------------------------

## 4. Central WP05 Behavior

The main authorized behavioral evolution is the Application-level
propagation of the WP04 failure vocabulary.

Reconcile actual enum names and current code, then establish a total
intentional mapping equivalent to:

``` text
ObservationSourceFailure.UnsupportedTarget
    -> ResearchFailure.UnsupportedTarget

ObservationSourceFailure.InsufficientObservations
    -> ResearchFailure.InsufficientObservations

ObservationSourceFailure.SourceUnavailable
    -> ResearchFailure.SourceUnavailable

ObservationSourceFailure.AccessDenied
    -> ResearchFailure.AccessDenied

ObservationSourceFailure.UsageLimitReached
    -> ResearchFailure.UsageLimitReached

ObservationSourceFailure.InvalidSourceResponse
    -> ResearchFailure.InvalidSourceResponse
```

Do not map transport details.

Do not introduce provider-specific branches.

Do not silently collapse distinct approved failure meanings into a
generic error.

The mapping must be exhaustive for the authorized source failure enum.

If repository design already has a safer equivalent mechanism, preserve
the established style while proving equivalent behavior.

------------------------------------------------------------------------

## 5. Existing Success Path Protection

The Release 0.9 successful path must remain semantically stable.

Inspect the actual `ResearchUseCase`.

Preserve the accepted sequence, conceptually:

``` text
ResearchRequest
    -> IObservationSource
    -> canonical PriceObservation values
    -> ObservationSeries
    -> MeanPrice
    -> ResearchResult / successful ResearchOutcome
```

Do not redesign the Domain calculation.

Do not introduce OHLCV aggregation.

Do not change the meaning of `MeanPrice`.

Do not add provider metadata to the research result.

Do not change Worker output in WP05.

Do not add asynchronous behavior unless the Release 1.0 execution plan
explicitly requires it at WP05.

------------------------------------------------------------------------

## 6. Request Validation

Preserve existing Application request validation.

Inspect actual rules for:

``` text
Target
RequestedObservationCount
```

Do not add provider query constraints to Application validation.

Forbidden WP05 validation additions unless explicitly required by
authority:

``` text
Twelve Data symbol syntax
/time_series constraints
outputsize limits as provider numbers
exchange/MIC requirements
provider interval strings
API-key validation
HTTP URI validation
provider date formatting
rate-limit rules
```

Application validates Application semantics; Infrastructure will own
provider mechanics.

------------------------------------------------------------------------

## 7. Failure Ownership

WP05 owns **use-case propagation**, not external failure detection.

The use case may consume only provider-independent
`ObservationSourceFailure` values.

It must not inspect:

``` text
HTTP status codes
HTTP response objects
exceptions from HttpClient
JSON
provider error DTOs
Twelve Data status/code/message
rate-limit headers
provider URLs
API credentials
```

Those belong to WP07--WP09.

WP09 will later translate external/provider failures into the
Application-owned source failure vocabulary.

WP05 must be ready to consume those meanings without knowing how they
were discovered.

------------------------------------------------------------------------

## 8. Exhaustiveness and Defensive Behavior

The source-failure-to-research-failure mapping must not accidentally
allow a newly added enum value to become a false success.

Use the repository's established coding conventions.

If an exhaustive switch expression is appropriate and consistent, prefer
it.

Do not add speculative `Unknown`, `Other`, or generic failure values
unless a governing authority explicitly requires one.

Do not catch broad exceptions merely to convert them into an Application
failure.

Unexpected programming/invariant failures should follow existing
error-handling policy.

------------------------------------------------------------------------

## 9. Contract Stability

WP05 should not create new public contracts unless the execution
plan/file manifest explicitly requires them.

Prefer:

``` text
existing IResearchUseCase
existing IObservationSource
existing ResearchRequest
existing ObservationSourceResult
existing ObservationSourceFailure
existing ResearchOutcome
existing ResearchFailure
existing ResearchResult
```

Do not introduce:

``` text
IHistoricalMarketDataUseCase
IMarketDataService
HistoricalMarketDataRequest
HistoricalMarketDataResult
ProviderResult
TwelveDataResult
```

unless a higher authority explicitly requires it.

WP04 already established that parallel abstractions were unnecessary.

Do not reopen that decision.

------------------------------------------------------------------------

## 10. Authorized Files

`RELEASE_1.0_FILE_MANIFEST.md` is the exact file authority.

Modify/create only WP05-authorized files.

Expected work should be narrowly concentrated in the existing
Application use-case implementation and only other files explicitly
authorized by the manifest.

Do not pull WP12 behavioral-test scope forward unless the manifest
explicitly assigns tests to WP05.

If compile validation requires an unauthorized file change, stop and
report the authority gap.

Fewer files than the maximum manifest allowance are acceptable when
sufficient.

------------------------------------------------------------------------

## 11. Test-Scope Discipline

Release 1.0 reserves dedicated Domain/Application behavioral testing for
WP12.

Therefore:

``` text
do not create broad WP12 test coverage early
do not restructure Application.Tests
do not change test packages
do not create new test infrastructure
```

However, if the WP05 manifest explicitly authorizes a narrowly scoped
test adjustment required to prove this work package, follow the manifest
exactly.

Existing tests must continue to pass.

Later WP12 must be able to test every new WP05 failure-propagation
branch deterministically through the Application-owned
`IObservationSource` seam.

Do not make the design harder to test.

------------------------------------------------------------------------

## 12. No Infrastructure Work

WP05 must produce zero Infrastructure/provider implementation.

Do not:

``` text
create Twelve Data transport models
create HttpClient code
create provider response DTOs
create JSON converters
parse timestamps
select provider price fields
normalize provider observations
map HTTP/provider errors
register provider services
add provider configuration
read environment variables
call the network
```

WP06--WP10 own those concerns.

------------------------------------------------------------------------

## 13. No Worker Work

Do not modify Worker behavior.

WP11 owns Worker market-data execution.

Do not change:

``` text
Worker composition
console output
host lifecycle
configuration loading
runtime provider selection
exit behavior
```

unless a higher authority explicitly makes a compile-only adjustment
unavoidable and the manifest authorizes it.

If not authorized, stop.

------------------------------------------------------------------------

## 14. Dependency Protection

Production graph must remain:

``` text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application, Infrastructure
```

WP05 must not add project references or packages.

Application references to Infrastructure/Worker must remain zero.

Twelve Data references in Application must remain zero.

No cycles.

------------------------------------------------------------------------

## 15. Implementation Quality

Follow repository standards for:

``` text
naming
nullability
immutability
switching/mapping
error handling
warnings
formatting
visibility
documentation comments where convention requires them
```

Do not over-engineer.

Do not introduce a mapping framework/library for a small enum mapping.

Do not introduce reflection.

Do not add extension classes unless the existing style and manifest
clearly justify them.

The smallest explicit behavior is preferred.

------------------------------------------------------------------------

## 16. Validation

Run the exact validation required by Release 1.0 authority.

At minimum, unless the execution plan specifies stronger commands:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
```

Record actual results and test counts.

Known `NU1900` vulnerability-feed connectivity warnings may be reported
as non-blocking only when consistent with the established repository
condition and all required validation passes.

Do not weaken or skip canonical verification.

------------------------------------------------------------------------

## 17. Targeted Behavioral Inspection

Prove the resulting `ResearchUseCase` intentionally handles every
current `ObservationSourceFailure`.

Record a mapping matrix from repository truth.

Expected shape:

  Source failure               Research failure             Covered intentionally
  ---------------------------- ---------------------------- -----------------------
  `UnsupportedTarget`          `UnsupportedTarget`          yes
  `InsufficientObservations`   `InsufficientObservations`   yes
  `SourceUnavailable`          `SourceUnavailable`          yes
  `AccessDenied`               `AccessDenied`               yes
  `UsageLimitReached`          `UsageLimitReached`          yes
  `InvalidSourceResponse`      `InvalidSourceResponse`      yes

If actual authority differs, report the actual authoritative mapping.

Prove:

``` text
unhandled authorized source failures = 0
provider-specific branches = 0
transport-specific branches = 0
new public contracts = 0 unless explicitly authorized
```

------------------------------------------------------------------------

## 18. Regression Inspection

Prove:

``` text
existing successful research behavior remains intact
existing invalid-request behavior remains intact
existing UnsupportedTarget behavior remains intact
existing InsufficientObservations behavior remains intact
Domain source delta remains zero
Infrastructure source delta remains zero
Worker source delta remains zero
```

Do not claim runtime provider integration; it does not exist yet.

------------------------------------------------------------------------

## 19. Architecture Inspection

Targeted scan must show within Application:

``` text
Twelve Data references = 0
HttpClient references = 0
HTTP status-code mapping = 0
JSON/serialization references = 0
provider DTO references = 0
provider API-key/configuration references = 0
provider rate-limit mechanics = 0
```

Architecture tests must remain green.

Do not modify architecture rules unless explicitly authorized by WP05
manifest---which should not be necessary for this Application-only
behavior.

------------------------------------------------------------------------

## 20. Git / GitHub Protection

Do not:

``` text
stage
commit
create a branch
push
create a PR
merge
stash cumulative work
discard cumulative WP01–WP04 work
```

Do not mutate GitHub planning:

``` text
do not close issue #90
do not edit issue bodies
do not change milestone #41
do not change Project fields/status
do not create Release 1.1 planning
```

WP05 ends in a validated local cumulative candidate.

------------------------------------------------------------------------

## 21. Working-Tree Classification

At the end classify non-clean state as:

``` text
EXPECTED GOVERNANCE
PRE-EXISTING AUTHORIZED
WP02 AUTHORIZED
WP03 AUTHORIZED
WP04 AUTHORIZED
WP05 AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

WP03 has no Domain source delta.

WP04 currently owns the two Application failure-enum changes unless
repository truth has legitimately evolved under authority.

List exact WP05 files separately.

No staging.

Unexpected changes must be investigated.

------------------------------------------------------------------------

## 22. Acceptance / Exit Criteria

WP05 is complete only if:

``` text
WP04 predecessor gate = PASS
existing ResearchUseCase inspected
existing Application contracts reused
all authorized ObservationSourceFailure values intentionally handled
new WP04 failures propagate to corresponding ResearchFailure values
existing success path preserved
existing invalid-request behavior preserved
provider-specific logic in Application = 0
HTTP/JSON/auth/provider DTO logic in Application = 0
Domain changes = 0
Infrastructure changes = 0
Worker behavior changes = 0
new packages/project references = 0
manifest scope respected
build = PASS with zero errors
required Application tests = PASS
architecture tests = PASS
canonical verification = PASS
git diff --check = PASS
unexpected mutations = 0
WP06 started = NO
WP07 started = NO
WP08 started = NO
WP09 started = NO
WP10 started = NO
```

If complete behavior requires provider transport or provider failure
detection, the package is incorrectly scoped: stop and report `BLOCKED`.

------------------------------------------------------------------------

## 23. Required Execution Report

Return:

``` text
# Release 1.0 WP05 — Historical Market Data Use-Case Integration Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP04 Predecessor Gate
## 5. Existing ResearchUseCase Baseline
## 6. Behavioral Gap Analysis
## 7. Implementation
## 8. Failure Propagation Matrix
## 9. Success-Path Preservation
## 10. Request-Validation Preservation
## 11. Provider / Transport Exclusion Evidence
## 12. Files Changed
## 13. Dependency / Architecture Evidence
## 14. Build Evidence
## 15. Test Evidence
## 16. Canonical Verification
## 17. Diff / Formatting Validation
## 18. Regression Evidence
## 19. Working-Tree Classification
## 20. Scope Protection
## 21. Findings / Observations
## 22. Exit-Criteria Assessment
## 23. Final Repository State
## 24. Final Decision
## 25. Next Authorized Action
```

Include exact changed paths, useful behavior summary, complete failure
mapping, commands/results, actual test counts, and any non-blocking
observations.

Do not claim tests for new WP05 branches if those tests were not
actually added/run; distinguish implementation inspection from
behavioral test evidence.

------------------------------------------------------------------------

## 24. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP05 USE-CASE INTEGRATION COMPLETE
RELEASE 1.0 WP05 USE-CASE INTEGRATION COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP05 USE-CASE INTEGRATION BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when all mandatory exit criteria
pass.

------------------------------------------------------------------------

## 25. Next Authorized Action

The other branch from WP04 remains:

``` text
WP06 — Provider Transport Model
GitHub issue #91
```

WP06 is the next logical work package after WP05 in the chosen execution
sequence.

**Do not execute WP06.**

Also do not skip directly to WP07 or later packages.

The human must separately authorize the next Codex prompt.

Stop after the WP05 report.

------------------------------------------------------------------------

## Execution Instruction

Read all authorities and current Application code, prove WP04
completion, evolve only the manifest-authorized `ResearchUseCase`
behavior needed to consume the WP04 provider-independent failure
vocabulary, preserve the successful Release 0.9 research path and
existing request validation, keep all Twelve Data/HTTP/provider
mechanics outside Application, perform the required validation and
targeted mapping inspection, classify the cumulative working tree,
return the required execution report, and stop before WP06.

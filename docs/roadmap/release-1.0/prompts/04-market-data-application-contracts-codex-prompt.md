# Release 1.0 WP04 --- Market Data Application Contracts --- Codex Prompt

## Role

Act as the **WP04 Market Data Application Contracts Executor** for
Release 1.0 of `AIQuantTradingResearch`.

This is a narrowly scoped Application-boundary work package. Its purpose
is to define the **minimum provider-independent Application contracts**
required to connect the existing research workflow to historical
market-data acquisition in later work packages.

WP03 proved that the existing Domain already contains the required
canonical observation semantics and therefore required zero Domain
changes.

**Twelve Data is the selected Infrastructure provider, but Twelve Data
must not appear in Application contracts.**

Do not start WP05 or WP06.

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
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read GitHub issues:

``` text
#88 — WP03 — Market Data Domain Evolution
#89 — WP04 — Market Data Application Contracts
#90 — WP05 — Historical Market Data Use-Case Integration
#91 — WP06 — Provider Transport Model
```

Read the completed WP03 execution evidence available in the current
context.

Inspect completely enough to understand the accepted Release 0.9/1.0
baseline:

``` text
Application production source
Domain production source consumed by Application
existing Application tests relevant to contracts/use cases
Application project references
existing public contracts
dependency/boundary documentation
configuration/error-handling/testing guidance relevant to Application
```

Authority precedence:

``` text
1. RELEASE_1.0_EXECUTION_PLAN.md
2. RELEASE_1.0_FILE_MANIFEST.md
3. WP02 provider decision as factual evidence
4. WP03 accepted zero-change Domain result
5. GitHub issue #89 as the GitHub projection of WP04
6. Existing repository architecture/engineering authorities
7. This execution prompt
```

If authorities materially conflict, stop rather than silently
redesigning the release.

------------------------------------------------------------------------

## 2. Predecessor Gate

Before mutation, prove:

``` text
WP01 = complete
WP02 = complete
WP03 = complete
WP03 Domain delta = zero
existing Domain canonical types remain PriceObservation, ObservationSeries, MeanPrice
selected provider = Twelve Data
provider mechanics ownership = Infrastructure
WP04 issue #89 is the current authorized package
```

Do not reopen WP03 design.

Do not add Domain types to compensate for an Application-contract design
preference.

If the predecessor evidence is missing or contradictory, stop.

------------------------------------------------------------------------

## 3. Objective

Define the **smallest stable provider-independent Application contract
surface** that downstream work can implement.

The contracts must allow Application to express what historical
observations it needs without knowing:

``` text
which provider supplies them
how HTTP requests are formed
how authentication works
how provider symbols are encoded
how JSON is shaped
how timestamps are parsed
how provider failures are transported
how rate limits work
```

The contracts should use existing Domain vocabulary where Domain meaning
already exists.

The result must provide:

``` text
a provider-independent historical market-data acquisition boundary
the minimum request/input semantics required by the approved slice
the minimum success/result semantics required by Application
the minimum failure/outcome semantics explicitly authorized by Release 1.0
```

Do not implement the real historical use case in WP04. That belongs to
WP05.

Do not implement Twelve Data transport types. That belongs to WP06.

------------------------------------------------------------------------

## 4. Existing Application Baseline

Before editing, inspect and reconcile the Release 0.9 Application model,
including concepts such as:

``` text
IResearchUseCase
IObservationSource
ResearchRequest
research outcome/result contracts
ResearchUseCase
Application dependency registration
```

Use actual repository names and structure; do not assume this list is
exhaustive.

Determine:

``` text
what can be reused unchanged
what must evolve
whether IObservationSource already represents the required provider-independent seam
whether a new market-data-specific contract is actually required by the Release 1.0 authority
whether request/result semantics can be evolved without duplicating existing contracts
```

**Do not create parallel abstractions when an existing Application
boundary can correctly evolve.**

A zero-change or smaller-than-expected result is valid if the
authorities and existing code prove it sufficient.

------------------------------------------------------------------------

## 5. Contract Design Rules

Application contracts must remain provider-independent.

They may depend on Domain.

They must not depend on Infrastructure, Worker, HTTP libraries, provider
SDKs, or serialization concerns.

Forbidden in Application contract names, members, attributes, defaults,
or documentation:

``` text
TwelveData
Twelve Data
/time_series
api.twelvedata.com
HttpClient
HttpRequestMessage
HttpResponseMessage
HTTP status codes as transport contracts
JSON property names
JSON serialization attributes
Authorization
apikey
API key values
provider query parameter names
outputsize
start_date
end_date
provider status/code/message payloads
credit headers
provider rate-limit numbers
provider DTOs
provider timezone strings
provider MIC/symbol encoding mechanics
```

A generic provider-independent target identity such as a
symbol/instrument identifier is not forbidden merely because
Infrastructure later maps it to provider syntax. Follow the execution
plan and existing contract model.

------------------------------------------------------------------------

## 6. Ownership Boundaries

### Domain owns

Existing canonical business/research values and invariants, including
the accepted observation/series/mean semantics.

### Application owns

Provider-independent orchestration contracts and use-case-facing
request/outcome semantics.

### Infrastructure owns

Provider transport and integration mechanics, including:

``` text
Twelve Data endpoint mechanics
HTTP
authentication
provider configuration
transport DTOs
JSON
provider-specific symbol mapping
date/time parsing
price-field selection
normalization into Domain values
provider failure interpretation
rate-limit mechanics
```

### Worker owns

Composition-root/host interaction and user-visible execution behavior in
later authorized work.

Do not move responsibilities across these boundaries in WP04.

------------------------------------------------------------------------

## 7. Request Semantics

Use only request semantics required by the approved Release 1.0 slice
and exact authority.

WP02 established a bounded daily historical equity slice.

Reconcile the existing `ResearchRequest` and related contracts before
creating anything new.

Potential provider-independent semantics include:

``` text
target identity
bounded observation count
historical acquisition intent
```

Do not add start/end dates, intervals, exchanges, currencies, adjustment
modes, provider names, or arbitrary options unless the Release 1.0
authority explicitly requires them at the Application boundary.

Do not model provider query parameters as Application properties.

Prefer the narrowest contract that can drive WP05 and allow WP06--WP09
to remain Infrastructure-specific.

------------------------------------------------------------------------

## 8. Success Semantics

Reuse Domain types for canonical observations rather than inventing
Application transport models.

If the acquisition boundary returns canonical observations, use the
exact shape authorized by the execution plan and existing design.

Do not duplicate:

``` text
PriceObservation
ObservationSeries
MeanPrice
```

with Application DTO equivalents unless a higher authority explicitly
requires a distinct contract.

Do not expose raw provider responses.

Do not expose JSON.

Do not expose provider metadata merely because it exists.

------------------------------------------------------------------------

## 9. Failure / Outcome Semantics

Follow the Release 1.0 execution plan and existing Application
error/outcome conventions.

The Application boundary may need provider-independent failure
categories that allow later Infrastructure mapping without leaking
transport details.

If authorized, categories should describe meaning to Application, not
Twelve Data mechanics.

Examples of provider-independent meaning that may be relevant **only if
supported by authority**:

``` text
invalid request
source unavailable
access denied
usage limit reached
no data
invalid source response
```

Do not copy this list mechanically.

Do not introduce a generic failure framework beyond WP04 scope.

Do not expose:

``` text
HTTP 401
HTTP 429
provider numeric error codes
provider error strings as typed Application semantics
exceptions from HTTP/JSON libraries
```

If the current Application contract style already handles outcomes
differently, preserve that style unless Release 1.0 explicitly evolves
it.

------------------------------------------------------------------------

## 10. Public vs Internal Surface

Inspect existing visibility conventions.

Keep the contract surface as small as possible.

Application abstractions intended for Infrastructure implementation may
need to be public because Infrastructure depends on Application.

Concrete Application implementations should remain non-public unless
explicitly required otherwise.

Do not broaden visibility simply to make testing convenient.

If testability later requires friend-assembly access, that belongs to
the appropriate authorized test package/unblock---not speculative WP04
expansion.

------------------------------------------------------------------------

## 11. Authorized Files

`RELEASE_1.0_FILE_MANIFEST.md` is the exact file authority.

Create or modify **only** the WP04 Application files explicitly
authorized by the manifest.

If the manifest authorizes tests in WP04, use only those exact test
files. If behavioral tests are reserved for WP12, do not pull them
forward.

Do not invent helper files or alternate paths outside the manifest.

If the correct minimal design requires fewer files than the manifest
permits, fewer is acceptable unless the manifest explicitly requires
each artifact.

If the correct implementation requires a file not authorized by the
manifest, stop and report the authority gap.

------------------------------------------------------------------------

## 12. No Downstream Implementation

WP04 must not implement WP05--WP11 behavior.

Specifically do not:

``` text
implement historical retrieval orchestration
modify ResearchUseCase behavior unless explicitly part of WP04 manifest
implement Twelve Data DTOs
implement HTTP client behavior
parse JSON
normalize provider values
map provider errors
register HttpClient
add provider configuration
change Worker execution
perform live provider calls
```

Contracts only, plus the minimum compile-preserving adjustments
explicitly authorized by the manifest.

------------------------------------------------------------------------

## 13. Dependency Protection

The accepted production graph must remain:

``` text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application, Infrastructure
```

WP04 must not add project references.

Application must not gain Infrastructure or Worker dependencies.

No cycles.

No provider package is required for Application contracts.

Do not add NuGet packages unless the execution plan and manifest
explicitly authorize one for WP04; otherwise treat such a need as a
design failure/blocker.

------------------------------------------------------------------------

## 14. Compile-Preserving Evolution

If an existing contract must evolve and current implementations no
longer compile, make only the minimum WP04-authorized compatibility
adjustment.

Do not silently implement WP05 or WP06 to satisfy compilation.

If compile preservation would require unauthorized downstream behavioral
changes, stop and report the dependency/authority issue rather than
crossing package boundaries.

Preserve cumulative WP01--WP03 state.

------------------------------------------------------------------------

## 15. Validation

Run the validation required by the Release 1.0 WP04 authority.

At minimum, unless the execution plan specifies a stronger exact
sequence:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Application.Tests/AIQuantTradingResearch.Application.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
```

Use the repository's canonical commands where they differ.

Record actual test counts.

Known `NU1900` vulnerability-feed connectivity warnings may remain
non-blocking only if they match the established repository condition and
all required validation succeeds.

Do not weaken validation.

------------------------------------------------------------------------

## 16. Targeted Contract Inspection

Prove after implementation:

``` text
Application references Domain only
Application references Infrastructure = 0
Application references Worker = 0
Twelve Data references in Application = 0
HTTP transport types in Application contracts = 0
JSON/serialization attributes in Application contracts = 0
provider auth/query/rate-limit mechanics in Application = 0
provider DTOs in Application = 0
new project/package references = 0 unless explicitly authorized
```

Also inspect whether any new contract duplicates existing Domain or
Application semantics.

If it does, simplify before accepting WP04.

------------------------------------------------------------------------

## 17. Regression Protection

Release 0.9 accepted research behavior must remain intact unless WP04
authority explicitly changes a contract in preparation for WP05.

Prove:

``` text
existing Domain behavior preserved
existing tests still pass
architecture tests still pass
no Worker behavior changed
no Infrastructure provider implementation started
```

Do not update expected tests merely to accommodate an accidental
behavioral regression.

------------------------------------------------------------------------

## 18. Repository / Git Protection

Do not:

``` text
stage files
commit
create a branch
push
create a PR
merge
stash cumulative work
delete cumulative Release 1.0 work
```

Do not mutate GitHub planning:

``` text
do not close issue #89
do not edit issues
do not change milestone #41
do not change Project status/fields
do not create Release 1.1 planning
```

The Project automation observation remains outside WP04.

------------------------------------------------------------------------

## 19. Working-Tree Classification

At the end classify all non-clean state as:

``` text
EXPECTED GOVERNANCE
PRE-EXISTING AUTHORIZED
WP02 AUTHORIZED
WP03 AUTHORIZED
WP04 AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

Remember that WP03 legitimately produced no Domain source delta.

List exact WP04-authorized changed/created paths.

Do not stage them.

Unexpected changes must be investigated and reported.

------------------------------------------------------------------------

## 20. Acceptance / Exit Criteria

WP04 may be complete only if:

``` text
WP03 predecessor gate = PASS
existing Application contract surface inspected
existing contracts reused/evolved where appropriate
minimum provider-independent contract surface established
manifest file scope respected
Application -> Domain dependency only
Twelve Data references in Application = 0
HTTP/JSON/auth/quota/provider transport mechanics in Application = 0
provider DTOs in Application = 0
Domain duplication = 0
WP05 behavior not implemented
WP06 transport model not implemented
build = PASS with zero errors
required Application tests = PASS
architecture tests = PASS
canonical verification = PASS
git diff --check = PASS
unexpected mutations = 0
WP05 started = NO
WP06 started = NO
```

If compile preservation requires unauthorized downstream implementation,
return `BLOCKED`.

------------------------------------------------------------------------

## 21. Required Execution Report

Return:

``` text
# Release 1.0 WP04 — Market Data Application Contracts Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP03 Predecessor Gate
## 5. Existing Application Baseline
## 6. Contract Design Reconciliation
## 7. Existing Contracts Reused
## 8. Contracts Added or Evolved
## 9. Request Semantics
## 10. Success / Result Semantics
## 11. Failure / Outcome Semantics
## 12. Provider-Specific Concepts Explicitly Excluded
## 13. Visibility / Ownership Assessment
## 14. Files Changed
## 15. Dependency / Architecture Evidence
## 16. Build Evidence
## 17. Test Evidence
## 18. Canonical Verification
## 19. Diff / Formatting Validation
## 20. Working-Tree Classification
## 21. Scope Protection
## 22. Findings / Observations
## 23. Exit-Criteria Assessment
## 24. Final Repository State
## 25. Final Decision
## 26. Next Authorized Action
```

Report exact contract names, signatures at a useful summary level,
visibility, files, command results, and test counts.

If zero Application source changes are the correct result, explain
precisely why the existing contracts satisfy WP04 and prove that
downstream WP05/WP06 have a sufficient boundary.

Do not claim validation not performed.

------------------------------------------------------------------------

## 22. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP04 APPLICATION CONTRACTS COMPLETE
RELEASE 1.0 WP04 APPLICATION CONTRACTS COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP04 APPLICATION CONTRACTS BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when every mandatory criterion
passes.

------------------------------------------------------------------------

## 23. Next Authorized Actions

The Release 1.0 dependency graph branches after WP04:

``` text
WP05 — Historical Market Data Use-Case Integration
GitHub issue #90

WP06 — Provider Transport Model
GitHub issue #91
```

WP05 and WP06 both depend on WP04.

**Do not execute either work package.**

The next prompt must be separately authorized by the human.

Stop after the WP04 execution report.

------------------------------------------------------------------------

## Execution Instruction

Read all authorities and the existing Application boundary, prove the
WP03 predecessor gate, reconcile existing Release 0.9 contracts against
the approved Release 1.0 slice, implement only the minimum
manifest-authorized provider-independent Application contract evolution,
keep all Twelve Data and transport mechanics in future Infrastructure
work, validate build/tests/architecture/canonical verification, classify
the cumulative working tree, return the required report, and stop before
WP05 and WP06.

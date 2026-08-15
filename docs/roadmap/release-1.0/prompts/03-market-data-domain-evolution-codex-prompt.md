# Release 1.0 WP03 --- Market Data Domain Evolution --- Codex Prompt

## Role

Act as the **WP03 Market Data Domain Evolution Executor** for Release
1.0 of `AIQuantTradingResearch`.

This is a narrowly scoped Domain implementation work package. Its
purpose is to evolve the existing provider-independent research Domain
only as much as required to represent the Release 1.0 historical
market-data slice established by WP02.

**Twelve Data is an Infrastructure provider decision. The Domain must
not know that Twelve Data exists.**

Do not start WP04.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before acting:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
docs/roadmap/release-1.0/prompts/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.0/prompts/02-market-data-provider-discovery-codex-prompt.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read GitHub issues:

``` text
#87 — WP02 — Market Data Provider Discovery
#88 — WP03 — Market Data Domain Evolution
```

Read the completed WP02 execution evidence available in the current
context.

Inspect the existing Domain source and Domain tests completely enough to
understand the accepted Release 0.9 research model before changing it.

Read existing Domain/solution/design authorities relevant to:

``` text
domain boundaries
dependency rules
public contracts
naming
error handling
testing
project structure
data quality
data lifecycle
provider abstraction
```

Authority precedence:

``` text
1. RELEASE_1.0_EXECUTION_PLAN.md
2. RELEASE_1.0_FILE_MANIFEST.md
3. WP02 provider decision as factual provider evidence
4. GitHub issue #88 as the GitHub projection of WP03
5. Existing architecture/domain/engineering authorities
6. This execution prompt
```

If these materially conflict, stop rather than redesigning the release.

------------------------------------------------------------------------

## 2. Predecessor Gate

Before mutation, prove WP02 is complete enough for WP03.

At minimum establish:

``` text
WP01 gate passed
WP02 provider discovery completed
selected provider = Twelve Data
selected Release 1.0 slice = bounded daily historical equity observations
provider-specific mechanics are assigned to Infrastructure
WP03 has provider-independent evidence for identity, observation date/time, price, optional volume, ordering, and validation concerns
```

Do not re-run provider selection.

Do not broaden or overturn the WP02 decision.

If WP02 evidence is missing or contradictory, stop.

------------------------------------------------------------------------

## 3. Objective

Implement the **minimum provider-independent Domain evolution** required
by the Release 1.0 market-data slice.

The Domain should express business/research meaning, invariants, and
value semantics---not HTTP, JSON, vendor contracts, authentication,
quotas, or transport mechanics.

Use the Release 0.9 Domain as the baseline and preserve existing
research behavior unless the Release 1.0 authorities explicitly require
evolution.

The resulting Domain must give WP04 a stable vocabulary for
provider-independent Application contracts.

------------------------------------------------------------------------

## 4. Domain Design Principles

Apply these constraints strictly:

``` text
Domain -> no production project dependencies
no provider SDK types
no HttpClient
no JSON attributes
no configuration APIs
no Twelve Data names
no endpoint/query/header names
no API-key concepts
no rate-limit concepts
no transport DTOs
no Infrastructure references
no Application references
no persistence concerns
no storage schema
no market-data provider interface
```

Model only concepts that have clear provider-independent meaning in the
approved Release 1.0 slice.

Prefer small immutable/value-oriented types and explicit invariants
consistent with the existing Domain style.

Do not introduce abstractions merely because they might be useful in
later releases.

------------------------------------------------------------------------

## 5. Required Design Reconciliation

Before editing, inspect the existing Release 0.9 Domain model and answer
internally:

``` text
Which existing types already express reusable research semantics?
Which existing types can remain unchanged?
Which concepts genuinely need new Domain representation?
Which WP02 facts are transport/provider details and therefore excluded?
Which invariants belong in Domain versus Application/Infrastructure?
```

Avoid parallel duplicate concepts.

If an existing Domain type already represents a required concept
correctly, reuse/evolve it rather than introducing a synonymous
replacement.

Do not rename stable Release 0.9 concepts without explicit authority.

------------------------------------------------------------------------

## 6. Provider-Independent Evidence Available from WP02

WP02 established evidence including:

``` text
instrument identity
bounded historical request concept
ordered observations
observation timestamp or trading date
decimal price
optional volume
provider-independent source outcomes/failures
missing/invalid/duplicate/unordered observation concerns
daily equity observations carry exchange-local market context
```

These are **inputs to design reasoning**, not a mandate to create one
class per bullet.

Determine which belong in Domain under the Release 1.0 execution plan.

Provider-specific facts must remain excluded, including:

``` text
Twelve Data
/time_series
api.twelvedata.com
Authorization header syntax
apikey
outputsize
start_date/end_date parameter names
Twelve Data symbol/MIC mechanics
transport numeric strings
provider DTO field names
provider status/code/message payloads
credit headers
8 credits/minute
800 credits/day
HTTP 429
adjustment modes as vendor mechanics
```

------------------------------------------------------------------------

## 7. Authorized Files

Use `RELEASE_1.0_FILE_MANIFEST.md` as the exact file authority.

Modify/create only the WP03 Domain files explicitly authorized by the
manifest.

If the manifest authorizes corresponding Domain tests in WP03,
create/modify only those exact tests. If Domain behavioral tests are
explicitly reserved for WP12, do **not** pull them forward into WP03.

Do not invent alternate paths or additional helper files merely for
convenience.

If a necessary implementation cannot be expressed within the
manifest-authorized WP03 file set, stop and report the authority gap.

------------------------------------------------------------------------

## 8. Domain Invariants

Implement only invariants supported by the authorities and existing
Domain conventions.

Potential evidence to reconcile includes:

``` text
required/non-empty instrument identity
valid observation date/time representation
valid decimal market value semantics
non-negative volume when volume is represented
coherent historical observation value construction
ordering/duplicate semantics only if explicitly Domain-owned
```

Do not assume every listed candidate invariant belongs in Domain.

Do not encode exchange calendars, provider entitlements, API limits,
transport parsing, HTTP failures, or provider payload completeness as
Domain behavior.

Avoid arbitrary numeric bounds not supported by authority.

------------------------------------------------------------------------

## 9. Time Semantics

WP02 established that daily equity data has exchange-local context.

WP03 must choose only the minimum provider-independent temporal
representation authorized by the execution plan and existing
architecture.

Do not embed:

``` text
America/New_York
NASDAQ
provider timezone strings
Twelve Data timestamp formats
```

unless an authority explicitly defines those as Domain data rather than
provider mechanics.

If the Domain only needs a trading date for daily observations, do not
introduce unnecessary instant/time-zone complexity.

If the execution plan explicitly requires a timestamp abstraction,
implement exactly that requirement.

Document the reasoning in code structure/naming rather than adding
speculative infrastructure.

------------------------------------------------------------------------

## 10. Price and Volume Semantics

WP02 observed OHLCV transport data, but WP03 must follow the Release 1.0
Domain scope exactly.

Do not automatically model full OHLCV merely because the provider
returns it.

Determine from the execution plan/file manifest whether the Domain slice
requires:

``` text
a canonical price
OHLC
OHLCV
volume
optional volume
```

Implement only what the release authority requires.

Use decimal/value semantics consistent with financial data and the
existing Domain.

Do not preserve provider transport strings in Domain.

------------------------------------------------------------------------

## 11. Failure Semantics

Do not move provider/transport failures into Domain.

Examples that remain outside Domain unless higher authority explicitly
says otherwise:

``` text
HTTP status
authentication failure
quota exhaustion
provider entitlement failure
malformed JSON
provider error code/message
network timeout
DNS/connectivity
```

Domain may reject invalid Domain values through its existing
invariant/error style.

Application-level source outcomes and Infrastructure failure mapping
belong to downstream packages.

------------------------------------------------------------------------

## 12. Backward Compatibility with Release 0.9

Preserve accepted Release 0.9 research behavior.

Before mutation capture:

``` text
existing Domain types
existing public Domain API
existing Domain tests
current architecture dependency baseline
```

After mutation prove that WP03 did not accidentally remove or change
unrelated research semantics.

If Release 1.0 intentionally evolves an existing type, keep the change
minimal and explain the authority.

Do not refactor unrelated Release 0.9 code.

------------------------------------------------------------------------

## 13. Implementation Quality

Follow repository conventions for:

``` text
namespace style
nullable annotations
immutability
constructor/factory style
validation
exception/error style
naming
file layout
analyzers
warnings
formatting
```

Avoid:

``` text
premature generic frameworks
base-class hierarchies without demonstrated need
provider abstraction in Domain
extension points for hypothetical providers
reflection
dynamic
serialization annotations
comments that restate obvious code
```

The target is a small, explicit, reviewable Domain delta.

------------------------------------------------------------------------

## 14. Validation Strategy

Run the validation required by the WP03 authority.

At minimum, unless the execution plan specifies a stronger exact
sequence:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore
Domain.Tests
Architecture.Tests
eng/verify.ps1
git diff --check
```

If restore is required because inputs changed or the repository state
requires it, use the canonical restore path.

Run all existing tests necessary to prove no regression if the canonical
verification script does so.

Do not weaken tests or verification.

Record exact test counts from the run.

Known `NU1900` vulnerability-feed connectivity warnings may remain
non-blocking only if they match the established repository condition and
validation otherwise succeeds.

------------------------------------------------------------------------

## 15. Architecture Validation

Prove after WP03:

``` text
Domain production dependencies = none
Application dependency direction unchanged
Infrastructure dependency direction unchanged
Worker dependency direction unchanged
cycles = 0
Twelve Data references in Domain = 0
HTTP/JSON/configuration references in Domain = 0
provider-specific concepts in Domain = 0
```

Use existing architecture tests plus targeted inspection where
necessary.

Do not modify architecture tests in WP03 unless the file manifest
explicitly authorizes it.

------------------------------------------------------------------------

## 16. Scope Protection

WP03 must not modify:

``` text
Application production code
Infrastructure production code
Worker production code
Application tests
Infrastructure tests
Architecture tests unless explicitly authorized by manifest
provider assessment/decision except read-only
architecture documentation unless explicitly authorized by WP03 manifest
project files
package references
solution membership
eng scripts
.github
Release 1.0 authorities/prompts
GitHub planning
```

Do not stage, commit, push, create a branch, or create a PR.

Do not close issue #88.

------------------------------------------------------------------------

## 17. WP04 Boundary

WP04 owns the provider-independent **Application contracts**.

Therefore WP03 must not create:

``` text
market-data provider interfaces
observation-source interfaces in Application
request DTO/contracts for provider retrieval
Application outcomes
use-case interfaces
dependency-registration code
HttpClient abstractions
provider configuration contracts
```

WP03 may establish Domain vocabulary that WP04 will consume.

Stop before WP04.

------------------------------------------------------------------------

## 18. Working-Tree Classification

At completion classify all non-clean items as:

``` text
EXPECTED GOVERNANCE
PRE-EXISTING AUTHORIZED
WP02 AUTHORIZED
WP03 AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

List every WP03-authorized path.

Preserve all cumulative Release 1.0 uncommitted work from WP01/WP02.

Do not stage or commit cumulative work.

If unexpected changes appear, investigate whether WP03 caused them. Do
not discard them automatically.

------------------------------------------------------------------------

## 19. Acceptance / Exit Criteria

WP03 may be complete only if:

``` text
WP02 predecessor gate = PASS
existing Release 0.9 Domain baseline was inspected
minimum provider-independent Domain evolution implemented
manifest-authorized file scope respected
Domain remains provider-independent
Twelve Data references in Domain = 0
transport/API/auth/quota concepts in Domain = 0
Domain dependency graph remains valid
existing research behavior preserved unless explicitly evolved
required build = PASS with zero errors
required tests = PASS
architecture validation = PASS
canonical verification = PASS
git diff --check = PASS
unexpected mutations = 0
WP04 started = NO
```

If mandatory criteria fail, return `BLOCKED`.

------------------------------------------------------------------------

## 20. Required Execution Report

Return:

``` text
# Release 1.0 WP03 — Market Data Domain Evolution Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP02 Predecessor Gate
## 5. Existing Domain Baseline
## 6. Domain Design Reconciliation
## 7. Provider-Independent Concepts Adopted
## 8. Provider-Specific Concepts Explicitly Excluded
## 9. Domain Changes Implemented
## 10. Domain Invariants
## 11. Time / Price / Volume Semantics
## 12. Files Changed
## 13. Backward-Compatibility Assessment
## 14. Dependency / Architecture Evidence
## 15. Build Evidence
## 16. Test Evidence
## 17. Canonical Verification
## 18. Diff / Formatting Validation
## 19. Working-Tree Classification
## 20. Scope Protection
## 21. Findings / Observations
## 22. Exit-Criteria Assessment
## 23. Final Repository State
## 24. Final Decision
## 25. Next Authorized Action
```

Report actual paths, test counts, command results, and exact Domain
concepts introduced/evolved.

Do not claim validation not performed.

------------------------------------------------------------------------

## 21. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP03 DOMAIN EVOLUTION COMPLETE
RELEASE 1.0 WP03 DOMAIN EVOLUTION COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP03 DOMAIN EVOLUTION BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when all mandatory exit criteria
pass.

------------------------------------------------------------------------

## 22. Next Authorized Action

If and only if WP03 completes successfully:

``` text
WP04 — Market Data Application Contracts
GitHub issue #89
```

Do not execute WP04.

Do not create Application contracts.

Stop after the WP03 execution report.

------------------------------------------------------------------------

## Execution Instruction

Read all authorities and the existing Domain, prove the WP02 predecessor
gate, reconcile the provider-independent Release 1.0 semantics against
the Release 0.9 model, implement only the minimum manifest-authorized
Domain evolution, validate dependencies/build/tests/canonical
verification, classify the cumulative working tree, return the required
report, and stop before WP04.

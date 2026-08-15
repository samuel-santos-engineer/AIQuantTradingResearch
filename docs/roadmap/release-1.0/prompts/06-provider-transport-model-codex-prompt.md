# Release 1.0 WP06 --- Provider Transport Model --- Codex Prompt

## Role

Act as the **WP06 Provider Transport Model Executor** for Release 1.0 of
`AIQuantTradingResearch`.

This is the first work package that may introduce **Twelve Data-specific
production types**, and those types must remain confined to
Infrastructure.

WP06 is a transport-model package only.

Do not implement HTTP calls, normalization into Domain values, provider
failure mapping, dependency registration, Worker behavior, or downstream
tests beyond what the manifest explicitly authorizes.

Do not start WP07.

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
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read GitHub issues:

``` text
#90 — WP05 — Historical Market Data Use-Case Integration
#91 — WP06 — Provider Transport Model
#92 — WP07 — Provider HTTP Client
#93 — WP08 — Market Data Normalization
#94 — WP09 — Market Data Validation & Failure Mapping
```

Read the accepted WP05 execution evidence available in the current
context.

Inspect existing Infrastructure source and project structure completely
enough to preserve repository conventions, including current
namespace/file organization, internal visibility style,
JSON/serialization usage if any, and dependency-registration boundaries.

Authority precedence:

``` text
1. RELEASE_1.0_EXECUTION_PLAN.md
2. RELEASE_1.0_FILE_MANIFEST.md
3. WP02 provider assessment/decision as factual provider evidence
4. Accepted WP03–WP05 results
5. GitHub issue #91
6. Existing architecture/engineering conventions
7. This execution prompt
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
WP05 = complete
selected provider = Twelve Data
WP06 issue = #91
```

Verify the current Application seam still uses provider-independent
contracts and that no Twelve Data types already exist outside
Infrastructure.

Do not reopen WP02 provider selection.

Do not redesign Application contracts in WP06.

------------------------------------------------------------------------

## 3. Objective

Create the **minimum Infrastructure-owned transport model** required to
represent the selected Twelve Data historical response and provider
error envelope for Release 1.0.

The transport model must enable later packages to:

``` text
deserialize Twelve Data response payloads
inspect provider metadata required for normalization
inspect historical values required for canonical observation creation
inspect provider error payloads required for failure mapping
```

without leaking provider-specific transport types into Domain or
Application.

WP06 should model the external wire contract, not the canonical Domain
model.

------------------------------------------------------------------------

## 4. Selected Provider Contract

WP02 selected Twelve Data.

The approved endpoint family is:

``` text
/time_series
```

The relevant successful response evidence includes:

``` text
status
meta
values
```

and per-observation fields such as:

``` text
datetime
open
high
low
close
volume
```

WP02 also established provider metadata relevant to later normalization,
potentially including:

``` text
symbol
exchange
mic_code
currency
exchange_timezone
interval
```

Use the actual current Twelve Data official contract documented by WP02
artifacts.

Do not invent fields merely because other providers expose them.

------------------------------------------------------------------------

## 5. Error Envelope

WP02 established that Twelve Data uses structured error semantics.

WP06 may model only the provider transport error representation needed
for later WP09 mapping.

Potential provider transport fields may include provider-specific
equivalents of:

``` text
status
code
message
```

Use the exact fields established by current provider evidence.

Do not map these to `ObservationSourceFailure` in WP06.

That belongs to WP09.

Do not interpret HTTP status codes here.

------------------------------------------------------------------------

## 6. Provider-Specific Ownership

All Twelve Data transport types must remain under Infrastructure.

They may include provider-specific names because they represent an
external wire contract.

Application and Domain must remain unaware of these types.

The preferred ownership shape is conceptually:

``` text
Infrastructure
└── MarketData
    └── TwelveData
        ├── transport request/response models
        ├── metadata transport models
        ├── observation/value transport models
        └── error transport models
```

Use the exact path conventions already defined by the Release 1.0 file
manifest and existing repository structure.

Do not create an additional project.

------------------------------------------------------------------------

## 7. Authorized Files

`RELEASE_1.0_FILE_MANIFEST.md` is the exact file authority.

Create/modify only WP06-authorized Infrastructure files.

If the manifest allows a provider directory such as:

``` text
src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/
```

use it only if consistent with actual repository conventions.

Do not create files in Application, Domain, Worker, or tests unless the
manifest explicitly assigns a narrow WP06 file there.

If exact file naming is not mandated, choose names that clearly indicate
provider transport ownership and do not imply canonical Domain
ownership.

Fewer files are preferable when sufficient.

------------------------------------------------------------------------

## 8. Serialization Strategy

Use the repository's existing .NET/runtime capabilities and conventions.

Prefer framework-native serialization when sufficient.

Do not add a NuGet package merely to model JSON unless the higher
authority explicitly allows it.

If using `System.Text.Json`, keep serialization attributes confined to
Infrastructure transport types.

Do not place serialization attributes on Domain/Application types.

Do not introduce custom converters unless the actual Twelve Data payload
requires them and the manifest permits the relevant implementation.

WP06 may define the transport types necessary for later deserialization;
actual transport execution remains WP07.

------------------------------------------------------------------------

## 9. Nullability / Optional Fields

Model the provider wire contract truthfully.

If a provider field is optional, conditionally present, or
asset-dependent, represent that fact without pretending it is always
available.

For example, WP02 established that volume is applicable where provided.

Do not strengthen provider guarantees beyond the evidence.

Do not silently convert missing/invalid values into defaults such as:

``` text
0
DateTime.MinValue
empty string
```

Transport models may preserve raw string/null values for later WP08/WP09
validation and normalization.

Canonical validation belongs downstream.

------------------------------------------------------------------------

## 10. Numeric Representation

WP02 established that Twelve Data transport numeric fields are
represented as strings.

Therefore WP06 must not prematurely parse them into Domain decimals
unless the file authority explicitly defines transport parsing here.

Prefer faithful transport representation.

Later WP08 owns normalization from provider representation into
canonical Domain values.

Do not collapse:

``` text
open
high
low
close
volume
```

into a Domain price inside the transport model.

------------------------------------------------------------------------

## 11. Time Representation

The provider response supplies date/time data in provider-specific
textual form and exchange-local context.

WP06 should represent the wire value faithfully.

Do not convert to `DateTimeOffset` here unless the authoritative
transport contract explicitly requires it and the boundary remains
transport-only.

WP08 owns canonical time normalization.

Do not embed exchange calendar logic.

Do not hardcode `America/New_York` into Domain/Application.

------------------------------------------------------------------------

## 12. Request Representation

The Release 1.0 execution plan permits provider request representation
in Infrastructure.

However, WP07 owns HTTP request construction.

WP06 may introduce a provider-specific request model only if the
manifest explicitly requires it and it represents provider transport
parameters rather than Application semantics.

Do not duplicate `ResearchRequest`.

Do not create an Application-facing Twelve Data request.

A provider request transport model, if needed, may capture provider
names such as:

``` text
symbol
interval
outputsize
start_date
end_date
timezone
```

only when justified by the approved Release 1.0 slice and WP02 decision.

Do not model every Twelve Data query option.

------------------------------------------------------------------------

## 13. Visibility

Transport types should be as restrictive as practical.

Prefer `internal` for provider DTOs and provider-specific models unless
a higher authority requires public visibility.

Do not make DTOs public merely for testing convenience.

WP13 will own Infrastructure behavioral tests and may use the existing
Infrastructure friend-assembly boundary if already established.

Do not add a new `InternalsVisibleTo` unless separately authorized.

------------------------------------------------------------------------

## 14. No Behavior Beyond Transport Modeling

WP06 must not:

``` text
call HTTP endpoints
construct HttpRequestMessage
inject HttpClient
perform authentication
read API keys
parse provider response into Domain values
choose which price field becomes canonical
sort observations
reject duplicates
map provider failures to Application failures
register services
change Worker behavior
perform live API calls
```

Those belong to WP07--WP11.

If implementing the transport model seems to require any of the above,
stop and report the authority issue.

------------------------------------------------------------------------

## 15. No Application / Domain Leakage

After implementation, targeted inspection must prove:

``` text
Twelve Data types in Domain = 0
Twelve Data types in Application = 0
Infrastructure transport types referenced by Domain = 0
Infrastructure transport types referenced by Application = 0
JSON attributes outside Infrastructure transport files = 0
provider-specific strings outside Infrastructure/provider docs = 0, except previously authorized governance/docs
```

Do not change the accepted production dependency graph.

------------------------------------------------------------------------

## 16. Backward Compatibility

Preserve all WP01--WP05 work.

Expected cumulative state before WP06 includes:

``` text
WP02 provider assessment/decision docs
WP03 zero Domain delta
WP04 two Application failure-enum changes
WP05 ResearchUseCase failure mappings
Release 1.0 governance/prompt artifacts
```

Do not modify those artifacts unless a higher authority explicitly
requires a compile-only adjustment, and only if the manifest authorizes
it.

Transport-model introduction should not alter existing behavior.

------------------------------------------------------------------------

## 17. Validation

Run the validation required by the Release 1.0 WP06 authority.

At minimum, unless the execution plan defines a stronger sequence:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
```

Record actual test counts.

If WP06 introduces only compile-time transport models and WP13 owns
their detailed fixture/deserialization tests, do not pull broad WP13
test implementation forward unless the manifest explicitly allows it.

Known `NU1900` warnings may remain non-blocking only if all required
validation succeeds.

------------------------------------------------------------------------

## 18. Targeted Transport Contract Inspection

Produce a transport model inventory with:

``` text
type name
visibility
purpose
wire fields represented
optional/nullable fields
serialization mapping if any
provider-specific ownership confirmation
```

Also classify each provider field as one of:

``` text
needed for later normalization
needed for later provider failure mapping
needed for request transport
not modeled because outside Release 1.0 slice
```

Do not create fields without a downstream reason.

------------------------------------------------------------------------

## 19. Architecture / Dependency Inspection

Prove:

``` text
Domain -> none
Application -> Domain
Infrastructure -> Application
Worker -> Application, Infrastructure
cycles = 0
new project references = 0
new packages = 0 unless explicitly authorized
Application -> Infrastructure = 0
Domain -> Infrastructure = 0
provider types remain Infrastructure-owned
```

Architecture.Tests must remain green.

Do not modify architecture tests in WP06 unless explicitly authorized by
manifest.

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
discard cumulative work
```

Do not mutate GitHub:

``` text
do not close issue #91
do not edit issues
do not change milestone #41
do not change Project status/fields
do not create Release 1.1 planning
```

------------------------------------------------------------------------

## 21. Working-Tree Classification

At completion classify visible non-clean state as:

``` text
EXPECTED GOVERNANCE
PRE-EXISTING AUTHORIZED
WP02 AUTHORIZED
WP03 AUTHORIZED
WP04 AUTHORIZED
WP05 AUTHORIZED
WP06 AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

List exact WP06 paths.

Do not stage them.

Unexpected files must be investigated and reported.

------------------------------------------------------------------------

## 22. Acceptance / Exit Criteria

WP06 is complete only if:

``` text
WP05 predecessor gate = PASS
Twelve Data transport contract inspected
minimum provider-specific transport model implemented
manifest scope respected
transport model confined to Infrastructure
provider DTOs not exposed to Application/Domain
wire numeric values modeled faithfully
wire date/time values modeled faithfully
optional/provider-conditional fields modeled honestly
provider error envelope represented if required by evidence
no HTTP execution implemented
no authentication behavior implemented
no normalization implemented
no failure mapping implemented
no DI/Worker behavior implemented
new project references = 0
new packages = 0 unless explicitly authorized
build = PASS with zero errors
required Infrastructure tests = PASS
architecture tests = PASS
canonical verification = PASS
git diff --check = PASS
unexpected mutations = 0
WP07 started = NO
```

If the transport contract cannot be represented within authorized
Infrastructure files without pulling downstream behavior forward, return
`BLOCKED`.

------------------------------------------------------------------------

## 23. Required Execution Report

Return:

``` text
# Release 1.0 WP06 — Provider Transport Model Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP05 Predecessor Gate
## 5. Selected Provider Contract Evidence
## 6. Existing Infrastructure Baseline
## 7. Transport Model Design Reconciliation
## 8. Transport Types Added
## 9. Success Response Model
## 10. Metadata Model
## 11. Observation / Value Model
## 12. Error Envelope Model
## 13. Request Model, If Any
## 14. Serialization / Nullability Semantics
## 15. Files Changed
## 16. Provider-Specific Ownership Evidence
## 17. Application / Domain Leakage Scan
## 18. Dependency / Architecture Evidence
## 19. Build Evidence
## 20. Test Evidence
## 21. Canonical Verification
## 22. Diff / Formatting Validation
## 23. Working-Tree Classification
## 24. Scope Protection
## 25. Findings / Observations
## 26. Exit-Criteria Assessment
## 27. Final Repository State
## 28. Final Decision
## 29. Next Authorized Action
```

Report exact type names, paths, visibility, represented fields, command
results, test counts, and any intentionally omitted provider fields.

Do not claim deserialization/normalization behavior unless it was
actually implemented and authorized.

------------------------------------------------------------------------

## 24. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP06 PROVIDER TRANSPORT MODEL COMPLETE
RELEASE 1.0 WP06 PROVIDER TRANSPORT MODEL COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP06 PROVIDER TRANSPORT MODEL BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when every mandatory criterion
passes.

------------------------------------------------------------------------

## 25. Next Authorized Action

If WP06 completes successfully:

``` text
WP07 — Provider HTTP Client
GitHub issue #92
```

Do not execute WP07.

Do not start HTTP transport implementation.

Stop after the WP06 execution report.

------------------------------------------------------------------------

## Execution Instruction

Read all authorities and the current Infrastructure baseline, prove WP05
completion, model only the minimum Twelve Data `/time_series` transport
contract and provider error envelope required by the approved Release
1.0 slice, keep the model internal to Infrastructure, preserve raw
provider representation for later normalization/failure mapping, avoid
HTTP/DI/Worker/downstream behavior, run the required validation,
classify the cumulative working tree, return the complete WP06 execution
report, and stop before WP07.

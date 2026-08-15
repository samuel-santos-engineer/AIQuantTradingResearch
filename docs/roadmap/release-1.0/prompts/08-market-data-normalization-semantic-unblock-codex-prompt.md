# Release 1.0 WP08 --- Market Data Normalization Semantic Unblock --- Codex Prompt

## Role

Act as the **WP08 Market Data Normalization Semantic Unblock Executor**
for Release 1.0 of `AIQuantTradingResearch`.

WP08 correctly stopped because three financial/temporal semantics were
not authoritative:

``` text
B08-01 — canonical OHLC field
B08-02 — daily exchange-local date → Domain DateTimeOffset semantics
B08-03 — Twelve Data adjustment mode
```

This prompt is the narrow human-governance authority that resolves
exactly those blockers.

It also authorizes the **minimum WP07 request correction** necessary to
make the selected adjustment policy explicit.

This is not a redesign of WP08.

Do not implement the WP08 normalizer during this unblock.

Do not begin WP09.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
docs/roadmap/release-1.0/prompts/02-market-data-provider-discovery-codex-prompt.md
docs/roadmap/release-1.0/prompts/06-provider-transport-model-codex-prompt.md
docs/roadmap/release-1.0/prompts/07-provider-http-client-codex-prompt.md
docs/roadmap/release-1.0/prompts/08-market-data-normalization-codex-prompt.md
docs/roadmap/release-1.0/prompts/08-market-data-normalization-semantic-unblock-codex-prompt.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

Read the blocked WP08 execution report from the current context.

Inspect current WP06/WP07 Infrastructure code and current Domain
`PriceObservation`.

Authority precedence for the three blocked semantics:

``` text
1. This explicit human semantic-unblock authority
2. RELEASE_1.0_EXECUTION_PLAN.md
3. RELEASE_1.0_FILE_MANIFEST.md
4. WP02 provider evidence/decision
5. Accepted WP03–WP07 implementation
6. Original WP08 prompt
```

This prompt overrides only the previously unresolved semantics and the
one narrowly authorized WP07 request correction described below.

All unrelated Release 1.0 authorities remain unchanged.

------------------------------------------------------------------------

## 2. Preflight Gate

Before mutation prove:

``` text
WP08 final decision = BLOCKED
B08-01 = unresolved canonical OHLC field
B08-02 = unresolved daily timestamp conversion
B08-03 = unresolved adjustment mode
WP08 implementation delta = 0
WP02–WP07 candidate remains preserved
WP07 endpoint = /time_series
WP07 interval = 1day
WP07 currently omits explicit adjust parameter
WP09 has not started
```

If the repository has materially moved beyond that state, stop and
report the drift.

------------------------------------------------------------------------

# 3. AUTHORITATIVE SEMANTIC DECISIONS

The following decisions are now explicit Release 1.0 authority.

They are not suggestions.

------------------------------------------------------------------------

## 3.1 B08-01 Resolution --- Canonical Price = `close`

For the Release 1.0 historical market-data vertical slice:

``` text
Domain PriceObservation.Price
    ← Twelve Data time_series value.close
```

`close` is the **only** canonical provider price field for this release.

Do not use:

``` text
open
high
low
volume
OHLC average
typical price
VWAP
previous_close
```

as the Domain price.

Do not use another field as fallback when `close` is missing, blank,
malformed, non-positive, or otherwise invalid.

Such a row is a normalization failure whose later Application
classification belongs to WP09.

### Rationale

Release 1.0 needs one deterministic scalar historical price series for
the existing `PriceObservation` Domain model.

The closing value is selected explicitly as the release's scalar
observation semantics.

This does not redefine the meaning of the other OHLCV transport fields;
they remain provider transport evidence for future releases.

### Scope

This decision applies only to the Release 1.0 Twelve Data daily
historical vertical slice.

It does not establish a universal price policy for future intraday,
futures, crypto, multi-field bar, or analytics models.

------------------------------------------------------------------------

## 3.2 B08-03 Resolution --- Adjustment Mode = `splits`

For Release 1.0, Twelve Data historical daily prices must use:

``` text
adjust=splits
```

The parameter must be **explicitly present** in the WP07 `/time_series`
request.

Do not rely on the provider's implicit default.

Do not use:

``` text
adjust=all
adjust=dividends
adjust=none
```

for Release 1.0.

### Rationale

The selected policy makes split adjustment explicit while avoiding
dividend-adjusted total-return semantics that the current Domain model
does not represent.

It also matches the provider behavior already underlying the accepted
WP07 request while removing dependence on an implicit provider default.

### Narrow WP07 Correction Authorized

This unblock explicitly authorizes one production behavior correction in
the existing WP07 request construction:

``` text
add query parameter:
adjust=splits
```

Nothing else in WP07 is authorized to change.

The resulting authorized request parameter set is:

``` text
symbol=<requested target>
interval=1day
outputsize=<requested count>
adjust=splits
```

Authentication remains header-based exactly as accepted in WP07.

Do not change:

``` text
endpoint
authentication mechanism
HttpClient ownership
transport result
deserialization
exception semantics
response handling
```

unless compilation mechanically requires a trivial change caused solely
by adding the parameter.

No new request DTO is required merely for this correction.

------------------------------------------------------------------------

## 3.3 B08-02 Resolution --- Daily Timestamp = Exchange-Local Date Anchor at 00:00

For the Release 1.0 daily historical vertical slice, Twelve Data's daily
`datetime` is treated as a **market-date identifier** in the exchange
timezone.

The Domain requires an absolute `DateTimeOffset`, so Release 1.0 defines
the canonical instant as:

``` text
provider daily date
    + 00:00:00
    in meta.exchange_timezone
    → DateTimeOffset carrying the exchange UTC offset for that local date
```

Example conceptually:

``` text
provider datetime:      2026-08-14
exchange_timezone:      America/New_York
local date anchor:      2026-08-14 00:00:00
resolved offset:        offset applicable in America/New_York at that local date/time
Domain instant:         2026-08-14T00:00:00<resolved-offset>
```

### Important Semantic Meaning

This timestamp is a **canonical daily-bar date anchor**.

It is **not** a claim that the exchange opened at midnight.

It is **not** the actual market-open timestamp.

It is **not** the actual market-close timestamp.

It exists because the current Domain requires an absolute
`DateTimeOffset` while the selected daily provider representation
supplies an exchange-local market date.

Future releases may introduce explicit bar-period/session semantics
without changing this Release 1.0 decision retrospectively.

### Timezone Source

Use:

``` text
TwelveDataTimeSeriesResponse.Meta.ExchangeTimezone
```

as the authoritative timezone identifier for equity daily data.

Do not use:

``` text
machine local timezone
hardcoded America/New_York
UTC by assumption
symbol-specific hardcoded timezone
```

The provider metadata must drive the conversion.

### Timezone Identifier

The provider supplies an IANA timezone identifier.

Use .NET `TimeZoneInfo` resolution supported by the repository target
runtime/platform.

Do not add a timezone NuGet package unless a higher authority separately
authorizes it.

If the timezone identifier cannot be resolved, normalization must fail
deterministically.

WP08 must not substitute UTC.

### Parsing

For the authorized `1day` slice, parse the provider daily date using an
explicit exact format consistent with actual provider evidence, expected
to be:

``` text
yyyy-MM-dd
```

If actual accepted WP02/WP06 evidence establishes a different exact
daily wire form, use that established form and document it.

Do not use culture-dependent parsing.

Do not infer a time component from the current clock.

### Invalid Local Time

If the canonical local midnight is invalid in the resolved timezone:

``` text
normalization fails deterministically
```

Do not shift the timestamp forward automatically.

### Ambiguous Local Time

If the canonical local midnight is ambiguous in the resolved timezone:

``` text
normalization fails deterministically
```

Do not arbitrarily select one offset.

### Offset / UTC Representation

Construct the Domain `DateTimeOffset` with the resolved exchange offset.

Do **not** normalize it to UTC before constructing `PriceObservation`.

Equivalent instants may later be compared chronologically by
`DateTimeOffset`, but the value produced by WP08 should preserve the
exchange-local date anchor and its resolved offset.

### Ordering

After all timestamps are successfully parsed/resolved:

``` text
sort observations by absolute DateTimeOffset instant ascending
```

The normalized sequence must therefore be chronological oldest → newest.

Do not depend on Twelve Data's default descending response order.

### Duplicates

Duplicate normalized instants are invalid for the current Domain series
semantics.

Do not silently drop, merge, or choose between duplicate rows.

WP08 should return deterministic normalization failure evidence; WP09
will later classify that evidence.

------------------------------------------------------------------------

# 4. Resolved WP08 Normalization Contract

After this unblock, the resumed WP08 authority is:

``` text
input:
  successful TwelveDataTimeSeriesResponse

for each value:
  parse datetime as exchange-local daily date
  resolve meta.exchange_timezone
  anchor local date at 00:00:00
  reject invalid/ambiguous local anchor
  resolve exchange offset
  construct DateTimeOffset preserving exchange-local anchor/offset
  parse value.close as decimal using invariant culture
  reject missing/blank/malformed/non-positive close
  construct existing PriceObservation

collection:
  normalize all supplied values
  sort ascending by absolute instant
  reject duplicate instants
```

WP08 still must not map these normalization failures to
`ObservationSourceFailure`.

WP09 remains responsible for provider-independent failure
classification.

------------------------------------------------------------------------

# 5. Numeric Rule

The resumed WP08 must use deterministic decimal parsing:

``` text
decimal
CultureInfo.InvariantCulture
explicit NumberStyles appropriate to provider decimal strings
no conversion through double
no current-culture dependence
no zero/default substitution
```

A valid `close` must also satisfy the existing `PriceObservation` Domain
invariant.

Do not pre-round a provider value unless the Domain itself requires it.

------------------------------------------------------------------------

# 6. Missing / Malformed Metadata

The following are deterministic WP08 normalization failures:

``` text
missing meta
blank/missing exchange_timezone
unresolvable exchange_timezone
missing values collection
blank/malformed datetime
blank/malformed close
non-positive close
invalid local date anchor
ambiguous local date anchor
duplicate normalized instant
Domain PriceObservation construction rejection
```

Do not manufacture defaults.

Do not map these to Application failure enums during this unblock or
WP08.

------------------------------------------------------------------------

# 7. Requested Count

This unblock does not change count semantics.

WP08 should normalize all supplied values.

Do not map:

``` text
returned count < requested count
```

to `InsufficientObservations` in WP08.

WP09/Application owns that semantic boundary.

------------------------------------------------------------------------

# 8. Provider Documentation Evidence

Before changing WP07, confirm current official Twelve Data evidence
supports:

``` text
adjust supports: all, splits, dividends, none
daily data is returned in exchange-local time
exchange_timezone identifies the exchange timezone
```

The human decision in this prompt selects `splits`; provider
documentation is used to confirm the parameter/value exists, not to
choose project semantics.

If current official documentation no longer supports `adjust=splits`,
stop as `BLOCKED` rather than silently substituting another value.

Do not use live API calls as the authority check.

------------------------------------------------------------------------

# 9. Authorized Mutation for This Unblock

This unblock authorizes only:

``` text
1. Minimum WP07 production correction:
   add adjust=splits to the existing /time_series query construction.

2. Any mechanically necessary compile-only change directly caused by #1,
   only if already within Release 1.0 file authority.
```

It does **not** authorize creation of the WP08 normalizer.

The semantic decisions are supplied by this governance artifact;
implementation resumes under the original WP08 prompt afterward.

Do not modify Domain or Application.

Do not add tests unless the existing Release 1.0 manifest explicitly
authorizes a narrow WP07 test at this stage.

Do not start WP09.

------------------------------------------------------------------------

# 10. Exact WP07 Request Invariant

After the correction, targeted inspection must prove the Twelve Data
request includes exactly the authorized Release 1.0 semantic parameters:

``` text
symbol
interval=1day
outputsize
adjust=splits
```

plus the already accepted header-based authentication transport.

Do not add:

``` text
timezone
start_date
end_date
previous_close
order
format
dp
```

unless already present and explicitly authorized by predecessor
authority.

If any extra current parameter exists, report it rather than removing it
without authority.

------------------------------------------------------------------------

# 11. Security Protection

Preserve WP07 security properties:

``` text
hardcoded real API keys = 0
API key in query string = 0
API key logging = 0
authenticated URI reporting = 0
secret-file creation = 0
```

`adjust=splits` is not secret.

Authentication remains header-based.

------------------------------------------------------------------------

# 12. Architecture Protection

The production graph remains:

``` text
Domain → none
Application → Domain
Infrastructure → Application
Worker → Application, Infrastructure
```

This unblock must introduce:

``` text
Domain changes = 0
Application changes = 0
Worker changes = 0
new project references = 0
new packages = 0
architecture-test changes = 0
```

unless a higher file authority explicitly requires otherwise.

------------------------------------------------------------------------

# 13. Validation

After the narrow WP07 correction run, at minimum:

``` text
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj --no-build --nologo
dotnet test tests/AIQuantTradingResearch.Architecture.Tests/AIQuantTradingResearch.Architecture.Tests.csproj --no-build --nologo
powershell -NoProfile -ExecutionPolicy Bypass -File eng/verify.ps1
git diff --check
git diff --cached --check
```

Record actual permanent test counts.

Canonical validation must remain offline and credential-independent.

A temporary test-owned HTTP probe may be used only if necessary to prove
the request contains `adjust=splits`; remove it before completion.

Do not perform a live Twelve Data request.

------------------------------------------------------------------------

# 14. Static Inspection

Prove:

``` text
/time_series endpoint unchanged
interval=1day unchanged
outputsize behavior unchanged
symbol behavior unchanged
adjust=splits explicitly present
header authentication unchanged
normalization implementation added = NO
ObservationSourceFailure mapping added = NO
DI changes = NO
Worker changes = NO
Domain changes = NO
Application changes = NO
new packages = 0
new project references = 0
```

------------------------------------------------------------------------

# 15. Working-Tree Protection

Preserve all cumulative Release 1.0 work.

Do not:

``` text
stage
commit
branch
push
create PR
merge
stash
discard prior work
```

Classify final state as:

``` text
EXPECTED GOVERNANCE
WP02 AUTHORIZED
WP03 AUTHORIZED
WP04 AUTHORIZED
WP05 AUTHORIZED
WP06 AUTHORIZED
WP07 AUTHORIZED
WP08 SEMANTIC UNBLOCK AUTHORIZED
EXPECTED GENERATED/IGNORED
UNEXPECTED
```

The only production delta owned by this unblock should be the minimum
WP07 `adjust=splits` request correction.

------------------------------------------------------------------------

# 16. GitHub Protection

Do not mutate:

``` text
issue #93
issue #94
milestone #41
Project status/fields
labels
Release 1.1 planning
```

The original WP08 issue remains the active work package.

------------------------------------------------------------------------

# 17. Acceptance Criteria

This semantic unblock is complete only if:

``` text
B08-01 resolved explicitly as close
B08-02 resolved explicitly as exchange-local 00:00 date anchor
B08-03 resolved explicitly as adjust=splits
WP07 request explicitly sends adjust=splits
WP07 endpoint remains /time_series
WP07 interval remains 1day
WP07 authentication remains header-based
WP07 transport/deserialization behavior otherwise unchanged
WP08 normalizer implemented during unblock = NO
Application failure mapping added = NO
Domain changes = 0
Application changes = 0
Worker changes = 0
DI changes = 0
new packages = 0
new project references = 0
live provider access = NO
secret leakage = 0
build = PASS with zero errors
required tests = PASS
architecture tests = PASS
eng/verify.ps1 = PASS
git diff --check = PASS
git diff --cached --check = PASS
unexpected mutations = 0
WP09 started = NO
```

If `adjust=splits` is no longer supported by current official Twelve
Data documentation, return `BLOCKED`.

If adding the explicit parameter requires a broader WP07 redesign,
return `BLOCKED`.

------------------------------------------------------------------------

# 18. Required Execution Report

Return:

``` text
# Release 1.0 WP08 Semantic Unblock Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. Blocked WP08 State Reconciliation
## 5. B08-01 Canonical Price Resolution
## 6. B08-02 Daily Timestamp Resolution
## 7. B08-03 Adjustment Resolution
## 8. Provider Documentation Confirmation
## 9. Authorized WP07 Correction
## 10. Final Request Parameter Evidence
## 11. WP07 Regression Evidence
## 12. WP08 Semantic Contract Now Established
## 13. Files Changed
## 14. Security Evidence
## 15. Architecture / Dependency Evidence
## 16. Build Evidence
## 17. Test / Temporary-Probe Evidence
## 18. Canonical Verification
## 19. Diff / Formatting Validation
## 20. Working-Tree Classification
## 21. Scope Protection
## 22. Findings / Observations
## 23. Acceptance Assessment
## 24. Final Repository State
## 25. Final Decision
## 26. Next Authorized Action
```

The report must state explicitly:

``` text
canonical price = close
adjustment = splits
daily timestamp = exchange-local date anchored at 00:00 with resolved exchange offset
ambiguous/invalid local anchor = normalization failure
normalized ordering = ascending
duplicates = normalization failure
```

Do not claim the WP08 normalizer was implemented.

------------------------------------------------------------------------

# 19. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP08 SEMANTIC UNBLOCK COMPLETE
RELEASE 1.0 WP08 SEMANTIC UNBLOCK COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP08 SEMANTIC UNBLOCK BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only when every mandatory criterion
passes.

------------------------------------------------------------------------

# 20. Next Authorized Action

After successful unblock, resume the existing authoritative WP08 prompt:

``` text
docs/roadmap/release-1.0/prompts/08-market-data-normalization-codex-prompt.md
```

The resumed WP08 must consume these decisions as authoritative:

``` text
price = close
adjust = splits
daily date anchor = 00:00 exchange local with resolved exchange offset
invalid/ambiguous anchor = normalization failure
ascending normalized order
duplicates = normalization failure
```

Do not create a replacement WP08 prompt.

Do not begin WP09 until the resumed WP08 returns a successful completion
decision.

------------------------------------------------------------------------

## Execution Instruction

Read all authorities and the blocked WP08 report, confirm the three
blockers exactly, treat this prompt as explicit human authority
selecting `close`, `adjust=splits`, and the exchange-local midnight
daily-date anchor semantics, verify official Twelve Data documentation
still supports the selected adjustment parameter, make only the minimum
authorized WP07 request correction to send `adjust=splits`, preserve all
other WP07 behavior and all Domain/Application/Worker boundaries, run
mandatory offline validation and request/security inspections, return
the complete semantic-unblock report, and stop before implementing WP08
normalization or beginning WP09.

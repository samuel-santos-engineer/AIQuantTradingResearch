# Market Data Provider Decision

## Status

Accepted for Release 1.0 on 2026-08-14.

## Decision

Select **Twelve Data** as the single external historical market-data provider
for Release 1.0.

The bounded vertical slice will use the documented REST `/time_series`
capability for a small daily historical series of one US-listed equity. It will
use ordinary HTTPS from Infrastructure, normalize provider transport values
behind the Application-owned observation-source boundary, execute the existing
research use case, and terminate.

This decision does not authorize implementation; WP06 and later packages own
provider mechanics. It does not authorize a second provider, storage, caching,
streaming, failover, or a provider-selection framework.

## Decision drivers

Twelve Data was selected because official evidence establishes:

1. A free Basic plan for personal/internal, non-commercial use.
2. A documented allowance of 8 API credits/minute and 800/day.
3. A one-credit-per-symbol `/time_series` request.
4. Historical OHLCV through a stable HTTPS endpoint.
5. Daily, weekly, monthly, and documented intraday intervals.
6. Bounded `outputsize`, date-range, ordering, and timezone mechanics.
7. Explicit exchange/timezone metadata and a compact JSON values array.
8. Structured error responses plus documented 401 and 429 behavior.
9. Authentication that can use an HTTP header and remain outside source.
10. Low .NET integration complexity without a required SDK.

The detailed evidence and candidate comparison are recorded in
[MARKET_DATA_PROVIDER_ASSESSMENT.md](MARKET_DATA_PROVIDER_ASSESSMENT.md).

## Alternatives

### Alpha Vantage

Technically viable, but not selected. Its standard free tier is limited to 25
requests/day and the free daily compact response to the latest 100 points;
full-length daily history is premium. Its endpoint and error representation are
also less explicit for the selected normalization boundary.

### Marketstack

Technically viable for end-of-day equities, but not selected. Its current free
plan is limited to 100 requests/month and one year of history, and the bounded
official-source review left more uncertainty around detailed current error and
free-tier redistribution terms.

## Release 1.0 usage boundary

Authorized provider usage is limited to:

- one evidence-selected provider: Twelve Data;
- a small daily historical equity request;
- local/internal, personal, non-commercial research execution;
- deterministic offline tests using sanitized, justified fixtures only when
  WP13 authorizes them; and
- an optional live request when credentials, network, entitlement, and limits
  permit.

The repository must not publish an API key, commit a raw live response, display
or redistribute provider data to third parties, or assume storage/caching rights.

## Architecture consequences

### Provider-independent boundary

Domain and Application may depend only on canonical concepts such as instrument
identity, a bounded historical request, ordered observations, timestamp/date,
selected price, and provider-independent outcomes or failures.

WP03 remains responsible for deciding whether existing Domain concepts need
minimal evolution. This decision does not prescribe concrete Domain type names.

### Infrastructure boundary

Infrastructure owns all Twelve Data identifiers and mechanics, including URL,
endpoint, API-key header/query handling, query parameters, provider symbols,
metadata and DTO names, OHLCV string parsing, exchange-local daily dates,
adjustment selection, ordering, quotas, and provider errors.

No Twelve Data type, name, or transport assumption may cross into Domain or
Application.

## Authentication and configuration consequence

A personal API key is required for normal access. The key must be supplied at
runtime through a repository-approved secret-safe mechanism and must never be
committed, logged, placed in Markdown, or embedded in a request fixture.

The credential-free `demo` key may support bounded discovery/schema checks but
is not the production configuration contract.

## Data semantics requiring explicit downstream treatment

- The selected endpoint returns OHLCV values as strings.
- Daily equity dates use the exchange's local timezone context; they must not be
  silently treated as UTC instants.
- Transport order defaults to descending unless explicitly requested.
- The provider exposes an `adjust` mode; later Infrastructure work must choose
  and test a mode rather than accepting an implicit semantic accidentally.
- Volume can be absent or inapplicable for some instruments; Release 1.0 must
  not make it a Domain invariant without WP03/WP04 authority.
- Null, malformed, duplicate, unordered, empty, short, unauthorized, and
  rate-limited responses require intentional boundary behavior.

## Licensing and redistribution consequence

Twelve Data's individual Basic terms permit internal use and prohibit free-tier
commercial use and unauthorized redistribution/external display. Third-party
exchange restrictions may also apply.

Therefore:

- publishing provider-integration source code is separated from publishing
  retrieved market data;
- live payloads must not be committed or reproduced in documentation;
- later deterministic fixtures must be sanitized, minimal, justified, and
  reviewed against provider/third-party terms before inclusion;
- Release 1.0 does not authorize public data display, caching, persistence, or
  redistribution; and
- any future commercial or external-display use requires separate rights review.

This records provider terms; it is not legal advice.

## Known limitations and open questions

- Exact free-tier historical depth for every US equity is not stated in one
  definitive official table; Release 1.0 requires only a bounded recent series.
- Market availability and entitlements may vary by instrument and plan.
- Exchange holidays and daily-bar completion timing require later deterministic
  handling.
- Adjustment mode must be chosen during the authorized transport/normalization
  work.
- Fixture redistribution suitability must be checked before WP13 commits any
  provider-shaped example.
- Provider availability, quotas, account status, and internet access can make a
  live demonstration unavailable even when offline acceptance passes.

These limitations do not prevent WP03 from evolving provider-independent Domain
semantics.

## WP03 handoff

WP03 may proceed using this factual basis:

- selected provider: Twelve Data;
- selected shape: bounded daily historical equity observations;
- minimum observed canonical facts: instrument identity, ordered date/timestamp,
  price values, and optional volume;
- prices and volume arrive as external strings and require validation;
- daily equity dates carry exchange-local context rather than an assumed UTC
  instant;
- missing, invalid, duplicate, unordered, empty, or insufficient observations
  are boundary concerns; and
- every Twelve Data name, endpoint, DTO, credential, quota, adjustment, and error
  remains Infrastructure-specific.

WP03 must not begin provider transport or encode Twelve Data concepts in Domain.

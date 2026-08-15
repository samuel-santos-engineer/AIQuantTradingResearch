# Market Data Provider Assessment

## Scope

Research date: 2026-08-14

This assessment selects one real external historical market-data provider for
the bounded Release 1.0 vertical slice. It does not select a future platform,
authorize multiple providers, or begin implementation.

The required slice is deliberately small: retrieve a bounded series of daily
historical observations for one US-listed equity over HTTPS, normalize the
provider response behind the Application-owned observation boundary, execute
the existing research use case, and terminate. Storage, caching, streaming,
provider failover, and redistribution of retrieved market data are outside the
release.

## Hard constraints

The selected provider must support:

- zero-cost personal/non-commercial development of the required slice;
- documented historical HTTP retrieval without scraping or private APIs;
- an account/API-key model compatible with secret-safe configuration;
- daily historical prices with timestamps and enough observations for the
  existing research operation;
- an official, usable response contract and documented failures or status
  behavior;
- ordinary .NET `HttpClient` integration without a mandatory provider SDK;
- provider-specific transport and authentication mechanics remaining entirely
  in Infrastructure; and
- a public source repository that does not publish credentials or redistribute
  retrieved provider data.

## Candidate set

The bounded viable set is Twelve Data, Alpha Vantage, and Marketstack. Each has
an official HTTP API, a zero-cost entry tier, historical equity data, and
documentation sufficient for a credible comparison. Candidates optimized for
streaming, institutional feeds, scraping, or paid-only access were excluded as
irrelevant to Release 1.0.

## Assessment matrix

| Criterion | Twelve Data | Alpha Vantage | Marketstack |
|---|---|---|---|
| Official API/docs | Official REST documentation and support knowledge base | Official API documentation and support pages | Official API documentation redirects to APILayer; official pricing and service agreement available |
| Zero-cost feasibility | Basic plan is free | Majority of endpoints available free; standard limit applies | Free plan is free forever |
| Account required | Yes | Yes for a personal key; a limited `demo` key exists | Yes |
| Authentication | API key; recommended `Authorization: apikey ...` header or `apikey` query parameter | Required `apikey` query parameter for documented examples | Access key; documented examples use `access_key` query parameter |
| Historical data | `/time_series` | `TIME_SERIES_DAILY` | `/v2/eod` end-of-day data |
| Relevant coverage | US equities and ETFs are included in Basic; forex and crypto also listed | Global equities plus ETFs, forex, crypto, and other datasets | End-of-day equities across listed exchanges |
| Intervals | `1min`, `5min`, `15min`, `30min`, `45min`, `1h`, `2h`, `4h`, `8h`, `1day`, `1week`, `1month` | Daily endpoint selected; separate weekly/monthly and premium intraday endpoints exist | Free tier is end-of-day only |
| Range/history limit | `outputsize` 1–5000; date-range parameters documented; plan-specific depth can vary and complete free-tier depth is not stated in one definitive table | Free daily compact output is the latest 100 points; full 20+ year output requires premium | Free tier permits up to one year of history |
| Rate/usage limit | 8 API credits/minute and 800/day on Basic; `/time_series` costs one credit per symbol | 25 API requests/day standard limit | 100 requests/month on the current pricing page |
| Pagination/range mechanics | `start_date`, `end_date`, `outputsize`; no page token for the selected request | Compact/full output selection; no page token documented for daily series | Offset/limit pagination appears in official response examples; bounded free history |
| Response format | JSON default; CSV optional | JSON default; CSV optional | JSON |
| Timestamp semantics | Metadata includes exchange timezone; daily/weekly/monthly data is returned in exchange-local time and ignores the timezone override | Daily keys are trading dates; precise timezone is provider metadata/response dependent and must not be assumed without a captured contract | Official example uses an ISO-like date with numeric UTC offset |
| Price fields | `open`, `high`, `low`, `close` strings | Daily open, high, low, close | Open, high, low, close plus adjusted variants in official example |
| Volume | Present for applicable instruments as a string | Daily volume | Volume and adjusted volume in official example |
| Error model | JSON error object with `code`, `message`, and `status`; HTTP 401 and 429 documented | Success/error payloads must be preserved; a single normalized error contract was not established in this bounded review | Detailed current error schema was not established from accessible official documentation: **UNKNOWN** |
| Terms/licensing | Basic/individual plans are personal/internal; free-tier commercial use and redistribution are prohibited without added rights | Personal non-commercial license; open-source wrappers are expressly welcomed, but commercial classification requires care | Free-plan service terms exist; explicit data-redistribution rights were not established: **UNKNOWN** |
| Operational constraints | Account/key, minute and daily credits, plan/market entitlements, third-party exchange terms, no SLA on Basic | Account/key, very low daily quota, premium full history/intraday, provider-specific informational payloads | Account/key, low monthly quota, one-year history, no free support |
| .NET complexity | Low: ordinary HTTPS and compact JSON schema | Low: ordinary HTTPS, but JSON property names/time-series object shape are less conventional | Low: ordinary HTTPS and conventional JSON |
| Release 1.0 fit | **Best fit** | Viable alternative | Viable but weaker |
| Evidence confidence | High for selected endpoint/schema/limits; medium for exact Basic historical depth beyond requested bounded output | High for daily endpoint, compact limit, and rate limit; medium for some error/terms implications | High for pricing limits; medium-low for current detailed API/error terms |

No numeric score is used. The selection follows the visible facts and hard
constraints.

## Official evidence register

### Twelve Data

- [API quickstart](https://twelvedata.com/docs/introduction/quickstart):
  account/key requirement, authentication methods, base URL, JSON default,
  null-handling guidance, and secret-handling guidance.
- [API documentation — time series](https://twelvedata.com/docs):
  `/time_series`, supported intervals, `outputsize` 1–5000, JSON/CSV,
  ordering, date parameters, timezone behavior, metadata, OHLCV fields, and
  structured error examples.
- [Individual pricing](https://twelvedata.com/pricing): Basic is free, with
  8 API credits/minute and 800/day; the plan is for personal, internal,
  non-commercial use.
- [Credits](https://support.twelvedata.com/en/articles/5615854-credits):
  `/time_series` costs one credit per symbol, Basic daily quota resets at
  midnight UTC, and exhausted API credits return HTTP 429.
- [Getting historical data](https://support.twelvedata.com/en/articles/5214728-getting-historical-data):
  interaction of `start_date`, `end_date`, and `outputsize`.
- [Timezones](https://support.twelvedata.com/en/articles/5745849-timezones):
  default timezone varies by asset; equities use the primary exchange's local
  timezone.
- [Terms of Use](https://twelvedata.com/terms): internal-use license,
  credential protection, prohibition on free-tier commercial use and
  unauthorized redistribution, third-party data conditions, and the need to
  respect plan-specific caching limits.
- [Commercial and personal usage](https://support.twelvedata.com/en/articles/5332349-commercial-and-personal-usage):
  individual plans are personal/internal and do not permit redistribution or
  commercial display.

### Alpha Vantage

- [API documentation](https://www.alphavantage.co/documentation/):
  `TIME_SERIES_DAILY`, daily OHLCV, JSON/CSV, latest 100 compact points for
  free keys, and premium full-length history.
- [Premium/API limits](https://www.alphavantage.co/premium/): the standard
  free API usage limit is 25 requests/day.
- [Free API-key support](https://www.alphavantage.co/support/): account/key
  acquisition and explicit support for open-source language wrappers.
- [Terms of Service](https://www.alphavantage.co/terms_of_service/): personal,
  non-commercial license and provider-specific usage restrictions.
- [Market-data policies](https://www.alphavantage.co/realtime_data_policy/):
  exchange-entitlement implications for realtime and delayed data; those
  capabilities are not required by this release.

### Marketstack

- [Pricing](https://marketstack.com/pricing/): free plan, 100 requests/month,
  end-of-day data, and one year of history.
- [Official EOD example](https://marketstack.com/find-ticker-symbol):
  `/v2/eod`, `access_key`, symbol request, pagination object, OHLCV, adjusted
  fields, exchange MIC, and dated JSON records.
- [Service agreement](https://marketstack.com/agreement): free-plan contract
  framework and service limitations. The bounded review did not establish an
  explicit free-tier redistribution grant.

## Candidate analysis

### Twelve Data

Twelve Data meets every hard constraint for a small, local research slice. Its
free limit is large enough for development and a one-shot Worker demonstration,
and the selected endpoint exposes explicit metadata, order, date-range,
timezone, OHLCV, and error semantics. The API key can be passed via the
recommended header and kept outside source control.

The principal limitations are legal and operational rather than architectural:
Basic is for personal/internal, non-commercial use; raw responses must not be
published or redistributed; some exchange/market access is plan-specific; and
free-tier depth beyond the bounded requested output should be verified against
the account entitlement rather than assumed.

### Alpha Vantage

Alpha Vantage also meets the minimum technical slice. Its daily endpoint is
simple and the free compact response supplies 100 observations. It is rejected
because the 25-request daily quota is materially tighter, full daily history is
premium, and its response/error conventions provide less explicit normalized
metadata than Twelve Data for the same narrow objective. This is a comparative
choice, not a claim that Alpha Vantage is unusable.

### Marketstack

Marketstack supports free end-of-day data and a conventional JSON response. It
is rejected because its current free plan is limited to 100 requests/month and
one year of history, while accessible official documentation left more
uncertainty around the detailed current error and free-tier redistribution
contract. Its technical integration would still be feasible for a narrower
equity-only use case.

## Representative retrieval feasibility

The documented credential-free Twelve Data demo request was executed on
2026-08-14:

```text
GET https://api.twelvedata.com/time_series
    ?symbol=AAPL
    &interval=1day
    &outputsize=3
    &apikey=demo
```

Observed, without storing a response file:

```text
status = ok
symbol = AAPL
interval = 1day
exchange = NASDAQ
exchange_timezone = America/New_York
values = 3 records
record fields = datetime, open, high, low, close, volume
```

This probe confirms current reachability and representative schema only. The
official documentation remains the contract authority. No personal credential
was requested, printed, or persisted.

## Boundary classification

### Provider-independent semantics

Later Domain/Application work may reason about:

- a provider-independent instrument identity;
- a bounded historical observation request;
- an observation timestamp or trading date;
- a decimal price value with explicit selected-price semantics;
- ordered observations;
- optional volume only if later authority proves it belongs in the canonical
  boundary;
- absence, invalidity, duplicates, and ordering as validation concerns; and
- provider-independent success/failure outcomes.

This list is evidence, not a WP03 Domain design.

### Infrastructure-owned Twelve Data mechanics

Infrastructure must own:

- base URL `https://api.twelvedata.com`;
- `/time_series` path;
- `symbol`, `interval`, `outputsize`, `start_date`, `end_date`, `order`,
  `timezone`, `adjust`, and `apikey` parameter names;
- recommended `Authorization: apikey ...` header mechanics;
- Twelve Data symbol, exchange, and MIC conventions;
- `meta`, `values`, `status`, `datetime`, OHLCV string field names;
- exchange-local daily timestamp semantics;
- adjustment mode and plan entitlement behavior;
- credit headers, minute/daily quotas, and HTTP 429 handling;
- provider error payloads and third-party data limitations; and
- transport ordering and normalization into canonical observations.

None of these names or mechanics may leak into Domain or Application.

## Validation implications for later work

Later packages should treat these as evidence-backed external-boundary cases:

- missing or empty `values`;
- a top-level error status or documented HTTP error;
- HTTP 401 and 429;
- null/missing fields;
- non-numeric OHLCV strings;
- invalid or ambiguous timestamps;
- unexpected ordering or duplicate timestamps;
- symbol/exchange mismatch;
- unsupported interval or entitlement;
- adjustment-mode mismatch; and
- fewer records than the requested research window.

## Assessment conclusion

Twelve Data is the strongest bounded fit and satisfies every Release 1.0 hard
constraint when used for personal/internal, non-commercial research, with the
API key held outside source control and no raw market data redistributed.

# Release 1.13 WP01 Contract and Engineering Selection

Status: candidate contract for separate Luna substantive acceptance. This record freezes governance and architecture only; it authorizes no application, test, package, schema, deployment, or provider-runtime mutation.

## Product contract

Release 1.13 is **Historical Market Visualization & Provider Abstraction**. The public surface is research/demo only, not a trading terminal. It presents `BTC/USD` and `ETH/USD` in `Market Research`, with the default `BTC/USD`, `1h`, and `30D` state. Navigation remains `Market Research | ML & Automation Studies | System Health`, and the chart is the dominant first-screen element. No trade controls, order execution, predictions, or live-trading path are included.

The complete supported matrix is the 2-symbol × 3-interval × 4-range set:

| Symbols | Intervals | Ranges |
| --- | --- | --- |
| `BTC/USD`, `ETH/USD` | `1h`, `4h`, `1d` | `1D`, `7D`, `30D`, `90D` |

Every combination is supported unless a later governed validation authority documents a bounded technical reason to narrow it. No additional symbols, intervals, or ranges are authorized here.

## Canonical candle contract

The provider-independent record contains `CanonicalSymbol`, `Interval`, `OpenTimeUtc`, `Open`, `High`, `Low`, `Close`, and `Volume`. Prices and volume preserve decimal precision; binary floating-point is not the canonical representation. Timestamps are UTC and identify the candle's left/open boundary. Records are strictly ascending, unique by symbol/interval/open time, non-null, and rejected when malformed, incomplete, or inconsistent with OHLC semantics. Closed-candle status is explicit in the acquisition/read-model metadata and is not inferred from presentation timing.

Provider field names, symbols, transport envelopes, URLs, cursors, and authentication details stay outside this contract. Provider metadata is separate from canonical candles and includes provider identity, adapter version, acquisition time in UTC, last validation time, and freshness/staleness information. The UI consumes canonical records and provenance, never raw provider payloads.

## Provider abstraction

The governed chain is:

`MarketDataProvider -> provider adapter -> canonical OHLCV -> cache/read model -> Streamlit`

WP02/WP03 must implement the smallest repository-native application-owned provider contract, with exact source paths frozen by their own authority before mutation. A request accepts only canonical symbol, interval, range, caller cancellation, and a bounded deadline. A response contains canonical candles plus provenance/freshness metadata. Typed failure categories are invalid request, unavailable, rate limited, authentication/configuration, malformed response, timeout, and cancellation. The UI performs no provider calls and no retries; adapter code owns provider mechanics and translation. No microservice, broker, plugin ecosystem, parallel pipeline, or direct browser/provider path is authorized.

Vike is the intended public historical source. Its first-party OHLCV documentation describes BTC/ETH historical candles, intervals from minutes through weeks, `/api/ohlcv`, cursor pagination, NDJSON export, `X-API-KEY`, and UTC left-aligned candle semantics ([Vike OHLCV API](https://vike.io/data/ohlcv-api)). The adapter maps canonical `BTC/USD` and `ETH/USD` to Vike's documented `BTC` and `ETH` symbols; WP03 must empirically validate that mapping without leaking it into the UI contract. Vike documents CC BY 4.0 attribution requirements; public presentation must retain attribution ([Vike access](https://vike.io/data/access)).

The first-party pages state different request limits: the OHLCV page states 2,000 requests/hour per key, while the access page states a shared 20,000 requests/hour limit and HTTP 429 behavior. This is an unresolved first-party evidence conflict, not a license to choose a value. WP03 must re-confirm the enforced limit before runtime implementation and must retain bounded request behavior regardless.

## Twelve Data preservation

The existing Twelve Data integration remains intact for private/internal research, development, integration tests, provider comparison, and future expansion. Twelve Data is not an anonymous public-chart fallback and is not removed, rewritten, or migrated by WP01.

## Cache-first and persistence boundary

The contract is `UI -> governed read model/service -> cache -> provider when required`. Cache keys include canonical symbol, interval, range, provider identity, and adapter/contract version. Metadata records acquisition time, last validation, age, freshness, and stale status. A cache hit serves without a provider call; refresh occurs only through the governed service. Stale-but-usable data remains visible with truthful disclosure when refresh fails. With no usable cache, the UI shows `Historical market data is temporarily unavailable. Please try again later.` No hover, zoom, pan, rerun, or client-side chart action may trigger acquisition.

Retention and refresh are bounded for Azure Linux F1 and `$0.00` recurring infrastructure. Existing atomic read-model/file handoff is the preferred presentation boundary. WP01 performs no schema migration, database rewrite, cache implementation, managed database, broker, paid service, or always-on infrastructure change.

## UI acceptance contract

Market Research must provide BTC/USD and ETH/USD selection, the supported interval/range controls, historical candlesticks, volume, OHLCV hover inspection, bounded zoom/pan, responsive layout, truthful freshness, and provenance substantially equivalent to:

`Data source: Vike • Historical OHLCV • Last updated: <UTC timestamp>`

Loading, empty, stale, and unavailable states are controlled and do not reveal provider details. Public failures never expose secrets, raw errors, sensitive URLs, stack traces, SQLite paths, HTTP traces, machine paths, or exception text. ML & Automation Studies remains informational and points toward later technical features/Release 2.0 without implementing models or signals. System Health remains a truthful secondary surface. The footer retains `Research and demonstration application • No trade execution`.

## Engineering selection

The current Streamlit dependency set exposes `st.line_chart` but no direct candlestick/volume chart. Plotly with `st.plotly_chart` is the selected charting approach because it supports candlesticks, volume, OHLCV hover, zoom/pan, and bounded Streamlit integration. WP01 does not install it or change a package manifest. WP05 must separately authorize the exact compatible package/version, license review, bundle impact, and F1 validation before any dependency mutation.

## Future literal path ownership

The only WP01 mutation paths are:

- `docs/roadmap/release-1.13/RELEASE_1.13_WP01_CONTRACT.md`
- `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
- `docs/roadmap/release-1.13/prompters/release-1.13-wp01-luna-contract-architecture-ui-acceptance-engineering-selection.md`

WP02/WP03 must later freeze exact files for the application abstraction and Vike adapter. Existing `src/AIQuantTradingResearch.Application/Research/IObservationSource.cs` and `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/*` are inspected boundaries, not WP01 mutation authority. WP04 must later enumerate cache/read-model files, WP05/WP06 must enumerate `python/presentation/realtime_financial_visualization.py` and any new presentation/test files, WP07 must enumerate deployment-validation files, and WP08 must enumerate documentation/runbook/acceptance files. No wildcard or implied source authority is granted by this record.

## Acceptance and governance boundary

Later authorities must prove the full matrix, canonical semantics, provider isolation, cache-first behavior, UI states, provenance/attribution, secret hygiene, no-bypass rules, F1/$0.00 constraints, and preserved Twelve Data behavior. WP01 requires documentation validation, exact path scope, clean diff, unchanged root `README.md`, no package/source/schema/runtime mutation, and a separate Luna substantive acceptance. WP02 must not begin until WP01 is substantively accepted and its lifecycle is complete.

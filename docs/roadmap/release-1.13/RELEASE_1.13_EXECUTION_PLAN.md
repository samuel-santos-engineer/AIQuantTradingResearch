# Release 1.13 Execution Plan — Historical Market Visualization & Provider Abstraction

## Status

This is a dependency-ordered planning graph. Each work package requires its own authority before any mutation. No package is authorized merely by appearing here.

`WP01 -> WP02 -> WP03 -> WP04 -> WP05 -> WP06 -> WP07 -> WP08`

| WP | Selected role | Dependencies | Purpose |
| --- | --- | --- | --- |
| WP01 | Luna | none | Freeze release contract, UI acceptance contract, supported interval/range matrix, architecture, engineering selections, and literal implementation allowlists. |
| WP02 | Terra | WP01 | Implement the provider-independent historical market-data abstraction while preserving canonical ownership and Twelve Data. |
| WP03 | Terra | WP02 | Implement and validate the server-side Vike historical-OHLCV adapter, canonical symbol translation, provider boundary, and verified attribution/licensing evidence. |
| WP04 | Terra | WP03 | Implement the cache-first historical-data/read-model path, freshness semantics, and outage-with-cache behavior. |
| WP05 | Terra | WP04 | Implement the public Market Research UI: governed selectors, candlestick/volume view, hover, bounded zoom/pan, and responsive layout. |
| WP06 | Terra | WP05 | Add public provenance, Twelve Data research-boundary messaging, ML & Automation Studies information surface, System Health relationship, and controlled public states. |
| WP07 | Terra | WP06 | Validate Azure F1 integration, stability, bounded resource behavior, no-bypass architecture, security, and zero-recurring-cost constraints. |
| WP08 | Luna then Terra only under separate authority | WP07 | Complete documentation, runbook, release-level reconciliation, and explicitly authorized lifecycle actions. |

## Common requirements

Every WP authority must state its model role, dependencies, exact purpose, literal path allowlist, mutation boundary, tests/validation, acceptance marker, lifecycle rule, and exclusions before execution. Exact changed-path proof, secret scanning, `git diff --check`, preserved Twelve Data, and no bypass of the governed provider/read-model/Streamlit boundaries are required.

WP implementation must not add a paid data feed, paid Azure service, or unnecessary persistent service. It must not expose Vike or Twelve Data secrets to the browser or public UI.

## Frozen UI acceptance topics

WP01 must define verifiable criteria for first load/defaults; navigation; instrument, interval, and range controls; candlesticks; volume; OHLCV hover; zoom/pan; loading, empty, stale/cache, and provider-unavailable states; provenance; freshness; Vike attribution; Twelve Data explanatory text; ML & Automation Studies; System Health; responsive bounded layout; forbidden public information; and all Release 1.14/2.0 deferrals.

## Lifecycle rules and exclusions

A WP issue stays open and Project Status remains non-Done until its own acceptance authority succeeds. Later WPs cannot consume unaccepted work. A release-level PR, merge, tag, GitHub Release, or milestone closure requires separate authority. Release 1.14 receives no detailed WP issues under this plan; Release 2.0 implementation is not authorized.

This plan does not authorize application code, packages, provider calls, database/schema changes, Azure/Docker/GHCR actions, live trading, ML, technical indicators, backtesting, tags, releases, or merges.

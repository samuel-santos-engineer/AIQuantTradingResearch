# Release 1.13 Definition — Historical Market Visualization & Provider Abstraction

## Status and authority

**Status:** Planning complete; implementation is not authorized by this document.

Release 1.13 is a bounded historical-market visualization release built on the accepted Release 1.12 public-reference deployment. GPT-5.6 Luna owns contract, architecture, governance, and final acceptance; GPT-5.6 Terra performs only separately authorized implementation and empirical validation; GPT-5.6 Sol provides supporting analysis only.

## Product and first-screen contract

The default surface is **Market Research**, with `BTC/USD`, `1h`, and `30D` selected. The chart dominates the first screen and presents historical-OHLCV candlesticks and volume. It provides bounded instrument, interval, and range controls; hover inspection; local zoom/pan where supported; visible provenance; a freshness timestamp; and a clear research/demo boundary.

Initial public instruments are `BTC/USD` and `ETH/USD`. Candidate intervals are `1h`, `4h`, and `1d`; candidate ranges are `1D`, `7D`, `30D`, and `90D`. WP01 must freeze any narrower supported combination matrix before implementation.

The top-level conceptual navigation is `Market Research | ML & Automation Studies | System Health`. ML & Automation Studies is informational only and explains the path from historical data through technical features to Release 2.0 ML evaluation. System Health remains a secondary truthful engineering surface.

## Provider and data boundary

The intended path is `MarketDataProvider -> provider adapter -> canonical historical OHLCV -> cache/persistence -> read model -> Streamlit`.

The public UI must not know provider API semantics, access a provider directly, open SQLite, or supervise the Worker. Provider symbol translation is adapter owned. Provider selection is internal configuration, never a public control.

Vike is the intended server-side public historical OHLCV source for the two canonical symbols. Public visualization must show Vike attribution/provenance. Provider licensing/access claims must be independently confirmed from current first-party evidence during implementation and acceptance; this definition makes no unverified licensing claim.

Twelve Data is retained for private/internal research, development, integration testing, comparison, real-time research, and future expansion. It is not the anonymous public historical-chart source and must not become an automatic public-chart fallback. The public interface must explain this boundary concisely without exposing values, credentials, or private configuration.

## Cache-first and public-state contract

Prefer `UI -> governed service/read model -> cache -> provider when required`. Ordinary Streamlit reruns, hover, zoom, and pan must not require provider round trips. Valid cached historical data remains visible during a provider outage with truthful freshness/staleness information. Without valid cache or provider data, show a controlled unavailable state.

The public interface must provide controlled loading, empty, stale/cache, and provider-unavailable states. It must never disclose stack traces, API keys, secrets, raw provider errors, internal paths/endpoints, HTTP traces, or machine-local information.

## Preserved and excluded scope

Preserved: the .NET-owned pipeline and canonical read-model handoff, Streamlit read-only/System Health boundary, existing Twelve Data integration, Azure App Service Linux F1 constraints, and the `$0.00` recurring-cost objective.

Excluded: order entry, brokerage integration, portfolio/P&L, live or automated trading, recommendations, buy/sell markers, ML training/inference, technical indicators, backtesting, production HA/SLA claims, paid services, and a microservice or generalized plugin framework.

Technical indicators and reusable quantitative features are reserved for Release 1.14. Lightweight ML evaluation remains Release 2.0.

## Acceptance boundary

Future acceptance must prove the frozen UI contract, adapter isolation, cache-first behavior, provenance/freshness, no-bypass architecture, secret hygiene, bounded resource behavior, and truthful controlled public states. This document creates no implementation, provider request, Azure mutation, schema migration, package addition, tag, release, or deployment authorization.

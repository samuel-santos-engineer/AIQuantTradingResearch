# Release 1.13 WP05 — Luna Extended F1 Historical Request / Read-Model Bridge Architecture Authority

Selected model: GPT-5.6 Luna.

This authority freezes the additive bounded one-shot local Worker/stdio
bridge described in `RELEASE_1.13_WP05_HISTORICAL_BRIDGE_ARCHITECTURE.md`.

The bridge path is:

`Streamlit -> bounded local process -> HistoricalMarketDataReadService -> cache -> provider -> Vike`

It preserves the existing Worker modes, legacy visualization handoff,
System Health, Twelve Data boundary, WP04 cache/freshness policy, F1
zero-cost topology, and `/home` persistence. It rejects file polling,
loopback HTTP, Python/provider acquisition, schema locks, managed
services, and new dependencies.

The request contract is exactly versioned canonical symbol, interval,
range, and bounded deadline. The response is bounded UTF-8 JSON carrying
canonical OHLCV, state, freshness, provenance, and safe timestamps. No
provider URL, credential, raw error, internal path, or stack trace may
cross stdout or reach the browser.

This is architecture-only. No production implementation, UI change,
Plotly pin, Docker/Azure/GHCR action, schema migration, merge, issue
closure, or WP06 work is authorized. A separate GPT-5.6 Terra authority
must freeze its own exact implementation allowlist and prove all 24
selection paths, timeout/cancellation, cross-process bounded acquisition,
legacy compatibility, and no-bypass behavior.

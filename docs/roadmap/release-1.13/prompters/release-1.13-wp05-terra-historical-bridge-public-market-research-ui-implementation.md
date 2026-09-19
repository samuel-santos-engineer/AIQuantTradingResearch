# Release 1.13 WP05 — Terra Historical Bridge and Public Market Research UI Implementation

Execution model: GPT-5.6 Terra.

This authority implements the accepted additive one-shot local Worker/stdio
historical-query bridge and the bounded Market Research surface. It keeps
WP04 as the sole cache/freshness/provider authority, uses a schema-free
per-cache-key lock, invokes the fixed Worker DLL without shell expansion,
and keeps provider credentials server-side.

The public surface is restricted to BTC/USD and ETH/USD; 1h, 4h, and 1d;
and 1D, 7D, 30D, and 90D, with BTC/USD / 1h / 30D defaults. Plotly is
pinned only for candlestick plus volume rendering. Legacy Worker and
System Health behavior remain preserved behind navigation.

No Docker, Azure, GHCR, schema, provider adapter, root README, merge,
issue lifecycle, WP06, trading, ML, or release mutation is authorized.

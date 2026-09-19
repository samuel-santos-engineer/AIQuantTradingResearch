# Release 1.13 WP05 Historical Request / Read-Model Bridge Architecture

Status: Architecture frozen by GPT-5.6 Luna. This record authorizes no implementation.

## Decision

Select an additive, bounded one-shot local process/stdio bridge. Streamlit invokes the existing packaged .NET Worker executable with an explicit historical-query mode. The mode constructs the existing `HistoricalMarketDataReadService`, which owns cache lookup, freshness, provider acquisition, stale fallback, and unavailable semantics. It writes exactly one presentation-safe JSON response to stdout and diagnostics only to stderr, then exits.

The legacy Worker modes, legacy `visualization-read-model.json` publication, System Health behavior, and existing one-shot stdio responsibilities remain unchanged. The bridge is a new mode, not a replacement pipeline.

## Why this mechanism

The current container already packages the Worker and Streamlit together, and the Worker is already a one-shot executable. A bounded invocation adds no listener, port, broker, managed service, schema, or always-on companion. It reuses `/home`-backed cache storage and the accepted WP04 service. File request/response orchestration was rejected because it requires polling, correlation files, races, and cleanup. Loopback HTTP was rejected because it adds a persistent process, port supervision, memory lifetime, and a new service boundary. A Python acquisition path was rejected because it bypasses WP04 and would expose provider policy to presentation.

## Process topology and lifecycle

For initial load, an allowed selector change, or an explicit governed refresh, Streamlit starts the fixed local Worker executable using an argument-safe process API. It passes a bounded request through stdin or a fixed argument contract; no shell command string is constructed. The child starts, validates one request, resolves the governed service from composition, emits one bounded response, closes stdout, and exits. Hover, zoom, pan, ordinary reruns, and chart rendering never invoke it. Streamlit may cache the presentation response briefly, but WP04 remains the only freshness authority.

The bridge must enforce one invocation timeout no greater than the existing bounded request deadline plus startup margin, terminate the child on timeout, cap stdin/stdout/stderr bytes, and never reuse an expired response as fresh. Cancellation terminates the child and is mapped to a safe cancelled/unavailable state.

## Request contract

The versioned request contains only:

```json
{"contractVersion":"aiq-historical-query-v1","symbol":"BTC/USD","interval":"1h","range":"30D","deadlineSeconds":10}
```

The only accepted symbols are `BTC/USD` and `ETH/USD`; intervals are `1h`, `4h`, and `1d`; ranges are `1D`, `7D`, `30D`, and `90D`. The deadline is bounded to the WP04 one-to-thirty-second range. Correlation identity is optional and must be opaque, bounded, and non-authoritative if included. Provider symbols, URLs, keys, SQL, paths, executable names, command fragments, and chart settings are forbidden.

## Response contract

The versioned UTF-8 response contains the canonical selection, state (`Fresh`, `Stale`, `Empty`, or `Unavailable`), an array of canonical candles with UTC open times and decimal-safe values, provider display identity, acquired/validated UTC timestamps, and stale/empty/recoverability metadata where applicable. Decimal values use JSON strings to avoid binary precision loss. Payload size and candle count are bounded. No API key, auth header, provider URL, raw body, exception, stack trace, database path, or machine path may appear in stdout. Diagnostics are stderr-only and are never parsed as response data.

## Ownership and failure mapping

The bridge maps canonical values to `HistoricalMarketDataRequest` and calls only `HistoricalMarketDataReadService.ReadAsync`. It does not call `IHistoricalMarketDataProvider`, parse Vike, or recreate cache/freshness policy. Fresh cache reads must suppress provider calls; missing/corrupt cache, stale refresh, stale fallback, provider failures, and empty success retain WP04 semantics.

Malformed request/response, unavailable executable, timeout, cancellation, cache failure, provider unavailable/rate-limited/authentication/malformed/timeout, and internal faults map to stable public-safe categories. Raw details remain internal. Empty successful data is distinct from unavailable; stale usable data remains renderable and marked stale.

## Cross-process concurrency

The WP04 `SemaphoreSlim` remains effective inside each child but is not a cross-process lock. Therefore every bridge implementation must acquire a minimal local filesystem lock keyed by the canonical cache fingerprint before a miss/stale provider acquisition. The lock is a schema-free exclusive-create file beside the existing cache record, containing only a random owner id and UTC acquisition timestamp. Acquisition is bounded by the request deadline; a holder rechecks the cache after acquiring the lock; only the holder may call the governed service for that key. A lock older than the bounded lease (the implementation must choose a lease no longer than the maximum bridge timeout plus startup margin) may be reclaimed after an exclusive-create failure, and a finally block removes a lock owned by the current process. A process death therefore cannot deadlock indefinitely, and atomic cache replacement prevents partial records. Lock acquisition failure maps to a safe unavailable/timeout result; it never triggers a provider bypass. Tests must prove same-key serialization, different-key concurrency, abandoned-lock recovery, cache recheck, and no duplicate acquisition after a successful recheck.

## F1 and deployment fit

The design is compatible with Linux F1: no listener, background poller, managed cache, database, broker, CDN, or paid service is added. Startup cost occurs only on the three governed interaction classes, memory is released at child exit, cache reuse suppresses provider calls, and the existing `/home` persistence boundary remains authoritative. The current image already contains the .NET runtime and `/app/worker/AIQuantTradingResearch.Worker.dll`; the fixed server-side invocation is `dotnet /app/worker/AIQuantTradingResearch.Worker.dll` with the explicit historical-query mode. The existing entrypoint continues starting Worker and Streamlit. Docker/startup changes are not authorized by this ADR; if the implementation cannot invoke that already-packaged DLL or share the configured `/home` cache without Docker mutation, implementation stops for a separate deployment authority.

## Security and legacy compatibility

Use `ProcessStartInfo.ArgumentList` or equivalent argument-safe APIs. Never interpolate selectors into a shell. The browser sees only the response projection. Twelve Data remains private/internal and is not a fallback. Existing Worker modes, legacy handoff, System Health, tests, and WP04 contracts remain intact.

## Expected implementation categories

The next Terra authority must enumerate exact literal paths before editing. Expected categories are a new Worker historical-query configuration/execution/serialization path, minimal Worker composition registration, a focused Streamlit bridge consumer and chart/UI files, focused .NET/Python tests, and only the specifically approved Plotly pin. No adapter, WP04 service/cache, schema, Docker, Azure, or README path is implied by this ADR.

## Acceptance tests

Tests must cover all 24 selections, canonical mapping, request/response validation, decimal/UTC serialization, stdout discipline, secret/path/URL exclusion, size and timeout bounds, cache-hit provider suppression, stale fallback, empty versus unavailable, malformed child output, cancellation, executable failure, concurrent sessions, atomic cache integrity, no provider/browser bypass, no invocation from chart interactions, and preservation of legacy Worker/presentation behavior.

This ADR freezes architecture only. It does not authorize bridge implementation, UI implementation, dependency installation, merge, WP06, deployment, or lifecycle mutation.

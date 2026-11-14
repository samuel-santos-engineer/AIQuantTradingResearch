# Release 1.15 WP60 - local historical candle inspection

Date: 2026-11-14. Status: IMPLEMENTED; BROWSER_QUALIFICATION_PENDING.

## Baseline and implementation

Fetched/verified current main `322d21f717ac010136f378c437ab600cf79b7251`, the squash merge of PR #448. No tracked WP60 naming collision found. Isolated branch preserves the operator checkout/untracked documents. No code from open PRs #445/#446/#447 is incorporated or changed.

Exactly three paths:

- `python/presentation/research_chart.py`: native UTC candle selector and read-only table, separate per-instrument selection state, explicit reset notice when selection disappears, exact supplied OHLC/volume/SMA20 projection. Inspection does not mutate the DTO or viewport, compute indicators, round decimal strings or acquire data. Values remain transformed educational values, not actual quotes; null fields say Unavailable.
- `python/presentation/test_research_chart.py`: six fake-only cases for both instruments, first/last/warmup/populated values, null/present volume, missing/empty snapshots, invalid/pruned selection, refresh correction, independent instrument state and analytic alignment rejection. WP59 regressions retained.
- This local runbook/evidence. All changed text must be UTF-8/LF/no BOM.

## Operator use

Open **http://127.0.0.1:18555**, choose BTC/USD or ETH/USD, then select an available timestamp under **Candle inspection / Inspect UTC candle**. The exact UTC candle start, OHLC strings, volume and supplied SMA20 appear in the table. Null volume/warmup say Unavailable. The selector is scoped to the complete validated snapshot, independently of the chart viewport; an inspected candle can intentionally lie outside the zoomed range. No gap candle is invented. If retained history drops a selected timestamp, inspection resets to the latest available candle with an explicit notice. Instrument selection is retained independently within the Streamlit session; no disk persistence.

Use WP59's native range form, presets, pan/zoom and Return to Live normally. A selection does not request additional history; only the existing explicit/opt-in refresh mechanism fetches snapshots. Empty/unavailable snapshots retain the existing truthful no-data warnings instead of displaying a stale inspection table.

## Local preflight and rollback

Preflight PASS: exact running WP59 UI and accepted API images/session ownership, imported named volume ownership, matching server/UI read-only CA mounts, recovered public certificate validity/SAN `wp07-research`, private key equality check (values never printed), loopback mapping and authenticated BTC1000/ETH1000 snapshots. A protected private ledger retains original container/network identities, image IDs, volume/labels, current TLS reference and environment names only. No new keys, TLS, import, acquisition or private dataset file reads.

Baseline API local image `sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028`; rollback UI `sha256:1aa45d94772bb8ca485647a2c61d891bf735dc55c204738c8a2ac06b5bf3d2d3`. Earlier accepted historical pair remains retained. The supported launcher has no UI-only replacement, so its owned Stop/Start recreated the pair/networks using the identical API image, existing imported volume/key and recovered TLS, selecting only the new UI. Both exited0; authenticated readiness returned1000 rows per symbol then UI readiness passed. No rollback needed. This does not claim the API container ID or all SQLite bytes are unchanged.

Do not start from the old preparation manifest: it still references expired TLS/original UI. Use the supported explicit launcher configuration with current `AIQ_CHART_TLS`, immutable UI/API IDs, existing volume and owner-held service channel. Private ledger references remain outside Git. TLS is temporary; check validity before a future start. Do not regenerate keys or initialize replacement data as a shortcut.

## Build and tests executed

Existing venv, no installations:

```text
PYTHONPATH=python/presentation
python -B -m unittest test_research_chart test_research_snapshot_client -q
```

**76 passed, exit0, 20.593 seconds, no skips**. Existing bare-mode Streamlit ScriptRunContext warnings occurred; no browser result inferred from tests.

```text
docker build --pull=false --network=none --platform=linux/amd64 --build-context qualified_python=docker-image://aiq-c2b36:candidate -f <owned-build-directory>/research-chart.Dockerfile -t aiq-wp60:local-candle-inspection <owned-build-directory>/context
```

Build exit0. New **local image ID** `sha256:0b0570140a6d40e2863e18498b4ed240d695683089ddd0025232b5f60b95439c`; Linux/amd64, UID/GID12002:12002, port8501. Unmodified supported Dockerfile and entrypoint: `/opt/venv/bin/python -B -m streamlit run /app/python/presentation/realtime_financial_visualization.py` with its fixed headless/loopback-launcher/telemetry/watcher/secrets-file flags. Cached qualified dependencies `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`, pinned Python base `sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024`. No API build, pulls or pushes.

The audited context contains exactly the existing ten allowlisted presentation source/assets plus favicon. Private preparation/data/SQLite/keys/TLS and test fixtures excluded. A disposable --rm, no-network, read-only container matched all eleven image `/app` file hashes to that context. This is source/application-file verification, not an exhaustive new audit of inherited OS/dependency layers. Historical images/tags were not overwritten.

## Browser acceptance

NOT_RUN at initial draft publication. A bounded actual Chrome pass will compare first/warmup/populated/last inspection rows to authenticated DTO fields in memory, emitting only equality booleans/counts. No raw observations, timestamps, screenshots, private paths or keys will be published. Existing fake tests cover missing/empty/pruned refresh cases without damaging retained authentic state.

PR initially draft and OPEN/UNMERGED. No merge, Azure/quota/registry/production/provider/acquisition/schema/API/dependency/storage operations. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

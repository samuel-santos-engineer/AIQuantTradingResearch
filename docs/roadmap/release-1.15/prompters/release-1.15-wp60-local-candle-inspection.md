# Release 1.15 WP60 - local historical candle inspection

Date: 2026-11-14. Status: LOCAL_BROWSER_QUALIFICATION_PASS; OWNER_REVIEW_PENDING.

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

## Final actual browser and preservation evidence

The PR was opened initially as draft **#449**, after the implementation/tests/build/readiness evidence and before browser execution. Two bounded actual installed-Chrome sessions used the existing venv/websockets and qualified WP59 browser harness, with owned profiles, fixed1280x1200 viewport, loopback CDP and disabled background networking/component updates/sync. Owned Chrome was closed after each run. No new dependency, app event channel or application JavaScript change.

First pass: BTC first/index0, first populated SMA/index19 and last/index999 rows matched authenticated DTO fields exactly; ETH baseline/last also matched. It stopped at ETH's first-candle selection using keyboard navigation of the filtered dropdown. This was reported as partial, not accepted. The temporary driver then used an explicit real mouse click on the exact filtered native option, with focus/option/result stages. The second complete pass succeeded; no application correction or rebuild was necessary. This establishes successful explicit selection, not a diagnosis of every keyboard behavior in the underlying widget.

Reference data came through the existing authenticated local DTO client in a captured in-memory pipe; no reference prices/timestamps or DTO JSON were saved/printed. Browser table UTC/OHLC/volume/SMA20 strings were compared in memory against the selected DTO row. Only equality results and relative indices/counts were emitted. Browser input used native focus/typing/option clicks, never value assignment or application-state injection. Actual command:

```text
<existing-project-venv-python> -B <owned-qualification-directory>/browser.py
```

Reproduce manually at the loopback URL: select BTC, inspect first/20th/last UTC options, then ETH first/20th/last, then return BTC and confirm its prior selection. Inspect outside a selected range intentionally; change range/preset, pan, Return to Live and Refresh, confirming the table remains selected independently. No additional provider history is acquired.

| Final measured browser check | BTC/USD | ETH/USD |
| --- | --- | --- |
| Correct mounted chart,1,000 loaded candles | PASS | PASS |
| First candle exact DTO timestamp/OHLC; null SMA unavailable | PASS | PASS |
| Index19 exact DTO values and supplied populated SMA20 | PASS | PASS |
| Last candle exact DTO fields and SMA20 | PASS | PASS |
| Null volume displayed Unavailable | PASS | PASS |
| Full chart SMA20 aligned;19 null/981 computed | PASS | PASS |
| No volume bars; unavailable-volume disclosure; modebar | PASS | PASS |

BTC -> ETH -> BTC **PASS**, including restored BTC first-candle selection independent of ETH's last-candle selection. Navigation coexistence **PASS**: bounded indices5..20/16-candle viewport, single-candle0..0, full1,000, preset100, Return to Live restoring100, real pan followed by explicit range and settled preservation, explicit Refresh retaining inspection, final preset1000. The inspection table retained the chosen first candle throughout navigation; loaded arrays remained1,000. No claim of forced obsolete-event replay or exhaustive race testing.

Post-browser authenticated DTO check **PASS**: both symbols1,000 rows, all volume fields null, supplied SMA20 null19/computed981. Post-browser resource inspection **PASS**: same API image, same imported volume identity/ownership labels, matching existing shared service key, new WP60 UI running, loopback port, WP59 rollback UI and accepted historical pair still present. The API container was recreated by the supported launcher; no database byte audit or claim of unchanged every SQLite byte. No rollback needed; ledger retained privately. No new initialization, data transfer, acquisition or key/TLS regeneration.

Browser empty/missing/pruned dataset fault injection **NOT_RUN**, to preserve retained authentic state; the focused fake-only cases verify these conditions and reset disclosure. Refresh retention was browser-tested; a real historical pruning/reset was not induced. The inspection panel intentionally displays transformed educational API values, not raw provider quotes; no rounding/recomputation. Selection is session-only, independent of viewport and per instrument. TLS remains the existing temporary seven-day pair and needs validity checks before future startup.

Only the final evidence append changed after the built source; image source still matches the implementation commit. All76 focused tests passed on that source. UTF-8/LF/no BOM checks (working files and staged blobs) and git diff --check passed before publication. No API/schema/dependency/JavaScript changes. **Local acceptance recommended; PR #449 may be marked ready after these passes and must remain OPEN/UNMERGED.** Local URL remains **http://127.0.0.1:18555**. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`; Azure remains deferred and no Azure/provider/registry/production operation occurred.

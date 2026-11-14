# Release 1.15 WP61 - local historical candle comparison

Date: 2026-11-14. Status: IMPLEMENTED_AND_BUILT; LOCAL_BROWSER_PENDING.

## Baseline, scope and operator use

Fetched/verified main `0cfb4886db626aab7487ce3ac7794e6d818e5fac`, the squash merge of PR #449. No tracked WP61 naming collision. Isolated branch preserves operator checkout/untracked documents. No unmerged code or edits to PRs #445/#446/#447.

Exactly three changed paths: `python/presentation/research_chart.py`, `python/presentation/test_research_chart.py`, and this evidence document. No backend/API/schema, Dockerfile, JavaScript, dependencies, provider or architecture changes. All changed text uses UTF-8/LF/no BOM.

At **http://127.0.0.1:18555**, choose an instrument and use **Two-candle comparison / Candle A UTC / Candle B UTC**. Default A is first and B last available candle. One-candle snapshots explicitly compare the same candle; empty/unavailable snapshots retain existing truthful no-data states. Exact supplied UTC/OHLC/volume/SMA20 strings appear side by side. Nulls display Unavailable, including volume and SMA warmup. These are transformed educational API values, not raw quotes or signals.

A/B selection is separate from the chart viewport and WP60 inspection, per instrument and per Streamlit session. Removed timestamps reset only unavailable selections to first/last valid bounds with notice. Incompatible instrument/generation/display-date/transform-epoch changes reset both with notice. Compatible version refresh retains available timestamps but reads both rows again from the same current snapshot; no cached comparison values or cross-snapshot rows. No invented gap candles, acquisition, DTO mutation or research calculation.

## Decimal presentation policy

Close difference is signed **B minus A** (price units); percent is **(B minus A) / A close * 100**. Only validated close strings enter Decimal; no float input or arithmetic. Working precision100 covers DTO magnitudes and twelve fractional digits. Display difference to12 decimal places, percent to6, **ROUND_HALF_EVEN**, fixed notation and normalized zero (no negative zero). Original supplied values remain exact strings without display rounding. Both computed fields say Unavailable if either close is missing/nonfinite/invalid or A close is zero. This is presentation arithmetic only, not an added research indicator.

## Tests and build

Fresh final source command, existing venv and dependencies:

```text
PYTHONPATH=python/presentation
python -B -m unittest test_research_chart test_research_snapshot_client -q
```

**84 passed, exit0, 21.654 seconds, no skips**. Includes all WP59/WP60 cases and eight new fake-only comparison cases: both symbols, first/last/same/reversed rows, exact projections, null SMA/volume, zero/missing inputs, precise Decimal arithmetic/half-even/negative-zero/extreme magnitudes, missing/empty/single data, invalid/pruned refresh, current-version corrections, incompatible identity, instrument independence, no DTO/viewport/inspection mutation and no gap fabrication. Existing Streamlit bare-mode ScriptRunContext warnings occurred. An initial84-pass run preceded the final widget reconciliation/string-input guard adjustment; the above is the final rerun after those changes.

Preflight PASS: exact running API/WP60 image IDs, owned volume/network/container labels, matching service keys (values not printed), consistent read-only CA/server mounts, current recovered public TLS validity/SAN `wp07-research`, loopback18555 and authenticated BTC1000/ETH1000 snapshots. A private protected rollback ledger retains exact resource identities, current TLS reference and environment names only; no stored secret values.

UI-only build command:

```text
docker build --pull=false --network=none --platform=linux/amd64 --build-context qualified_python=docker-image://aiq-c2b36:candidate -f <owned-build-directory>/research-chart.Dockerfile -t aiq-wp61:local-candle-comparison <owned-build-directory>/context
```

Build exit0. Immutable **local image ID** `sha256:79ed4ff5d92e11866d6d01d70f4b8fff8b597e4533c94103915f4796a18c0163`; Linux/amd64, UID/GID12002:12002, port8501. Unmodified qualified Dockerfile/entrypoint `/opt/venv/bin/python -B -m streamlit run /app/python/presentation/realtime_financial_visualization.py` and its fixed runtime flags. Cached qualified Python dependency image `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`; pinned Python base `sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024`. No pulls, network builds, API build or registry publication. Distinct tag preserves earlier images.

Context contains exactly the supported ten allowlisted presentation source/assets plus favicon. No private preparation, SQLite, observations, keys, TLS or fixtures. A disposable --rm/no-network/read-only/capability-dropped container matched all eleven `/app` hashes to that context, including the changed module. This is application-source/context verification, not an exhaustive new audit of inherited OS/dependency layers.

## Runtime handoff and pending browser qualification

Rollback API image: `sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028`; baseline WP60 UI: `sha256:0b0570140a6d40e2863e18498b4ed240d695683089ddd0025232b5f60b95439c`. Accepted historical and WP59 images remain rollback assets. Use the supported launcher with explicit new UI, existing API/volume/key and recovered TLS; it has no UI-only action, so owned paired Stop/Start recreates container/network identities. Never silently use expired historical preparation/TLS manifests, regenerate credentials or initialize replacement data. Current private ledger retains references without publishing paths.

Initial draft publication: runtime switch/browser comparison NOT_RUN. Later results must distinguish API container recreation from retained image/volume identity, and compare rendered exact values plus independently calculated changes in memory without raw DTOs/screenshots in evidence. Fake-only destructive empty/pruned/zero-denominator cases must not be induced in retained data. Current temporary seven-day TLS needs rechecking before future start.

No Azure/quota/production/registry/provider/acquisition/import/storage migration/dependency actions. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. PR initially draft, no merge.

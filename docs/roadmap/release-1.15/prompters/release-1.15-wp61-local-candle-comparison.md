# Release 1.15 WP61 - local historical candle comparison

Date: 2026-11-14. Status: SELECTOR_RECONCILIATION_TESTS_PASS; REVISED_RUNTIME_NOT_RUN; OWNER_REVIEW_PENDING. Earlier build/browser results below are historical.

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

## Final runtime/browser qualification

Implementation/source commit **`c87093e6f0d9255d42f8d1b3df545f743d83a0eb`**. The committed chart-module SHA256 was independently matched to the audited image context after testing; no application source changed during browser diagnosis. Only this evidence append follows the built source. PR **#450** opened initially draft before runtime/browser acceptance.

Supported owned Stop/Start each exited0. The launcher requires paired recreation, so API and UI container/network IDs changed, while the exact API image, existing imported volume, shared key and recovered TLS pair were retained. Authenticated local TLS snapshot readiness returned BTC1000/ETH1000, followed by Streamlit readiness. No rollback, data initialization, reimport, key regeneration, TLS replacement or API build.

Actual installed Chrome and existing venv/websockets used owned profiles and loopback CDP, fixed1280x1200 window, with browser background network/component updates/sync disabled. Native button opening, search typing and exact visible/hit-testable option clicks were used; no injected Streamlit state, value assignment or application workaround. Source DTO records were read through the existing authenticated local client into comparison-only memory. No DTO values/timestamps, prices, screenshots, secrets or private paths were printed or persisted in the evidence.

Expected deltas were calculated **independently using Fraction and integer quotient/remainder half-even rounding**, not the production Decimal helper. Both exact supplied table fields and formatted changes were compared in memory; output consisted only of equality categories and relative indices. Actual command:

```text
<existing-project-venv-python> -B <owned-qualification-directory>/browser.py
```

There were **11 bounded browser sessions**: seven partial/failed acceptance or result-diagnosis runs, three passing narrow structural/transition diagnostics, and one final complete passing run. Failures occurred at native dropdown option/result interaction; result diagnostics showed the previous selection/table unchanged, not incorrect values for a proven new selection. Native control/idle-state checks and visible hit-target filtering resolved the driver interactions, including the unchanged WP60 inspection control. Earlier failures are not reclassified as passes. No application correction/rebuild/runtime restart resulted from these attempts. The exact timing/root cause of each earlier failed interaction was not independently proven; final success does not assert exhaustive widget/race reliability.

| Final complete browser check | Result |
| --- | --- |
| BTC -> ETH -> BTC, correct instrument and1,000 loaded rows | PASS |
| BTC A0/B999, A0/B0, A0/B19, A999/B19, A19/B19, A19/B999 | Exact DTO fields + independent changes PASS |
| ETH A0/B999, A0/B0, A0/B19, A999/B19, A999/B0 | Exact DTO fields + independent changes PASS |
| Restored independent BTC A19/B999 after ETH A999/B0 | PASS |
| Null SMA warmup and volume say Unavailable | PASS |
| Populated SMA20 side-by-side exact supplied values | PASS |
| Independent signed differences and A-denominator percentages | PASS |
| WP60 inspection selection/values independent of comparison | PASS |
| WP59 bounded16, single1 and full1,000 viewport controls | PASS |
| Preset100 and Return to Live restoring100 | PASS |
| Real pan then explicit range; inspection/comparison unchanged | PASS |
| Explicit Refresh retaining A/B and inspection | PASS |
| Final full1,000 preset restored | PASS |

Post-browser authenticated DTO invariants **PASS** for each instrument:1,000 candles; all volume fields null; SMA20 null19/computed981. Plotly arrays remained aligned with the supplied series, no volume bars, unavailable-volume disclosure/modebar present. No new provider history was acquired and historical freshness was not relabeled as current.

Post-browser preservation **PASS**: same API image, same named volume identity/ownership labels, existing API/UI key equality, new WP61 UI running on loopback18555; accepted historical, WP59 and WP60 images still available. Private rollback ledger retained; rollback NOT_NEEDED. No database byte audit or assertion that every SQLite byte is unchanged. Browser/profile processes were closed within bounded owned cleanup; the working application remains available at **http://127.0.0.1:18555**.

Empty/single/pruned/zero-denominator/incompatible-generation cases are **fake-only tested**, not forced against retained authentic data. Browser tested real refresh retention, not destructive pruning or a live identity rotation. Comparison rows always come from one current validated snapshot; no raw-provider quote claim. Session state is temporary; seven-day TLS remains temporary and must be rechecked before future start.

### External GitHub status drift

Final read-only verification found PR **#445 MERGED** (observed head `fdb4f423f04744402e2bcf3d7d9b906b574d0187`) and **#447 MERGED** (observed head `e6b4b8b571f904d6c61b35dc75023dad7f89a0e1`); #446 remains OPEN at `187e6584571ed4d978511f0abfc7caa3021ea481`. These were external changes, not WP61 actions. The owner's requested open status cannot be preserved retroactively; no attempt to reopen/revert or modify those PRs was made. WP61 remains based on the explicitly verified `0cfb4886db626aab7487ce3ac7794e6d818e5fac`, with no rebase, new main incorporation or unmerged initializer.

**Local acceptance recommended; mark #450 ready for owner review after final checks, leave OPEN/UNMERGED.** Final evidence uses UTF-8/LF/no BOM and git diff --check; no source/dependency changes after the84-test pass. No Azure/quota/registry/production/provider/acquisition/import/data-transfer operations. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## PR #450 selector reconciliation and upstream integration - 2026-11-15 UTC

Original reviewed PR head `20c9cb9b4f9f67534c63283946e15bf0565c2736`, original base `0cfb4886db626aab7487ce3ac7794e6d818e5fac`. Clean isolated worktree and live OPEN/UNMERGED/MERGEABLE state were verified before editing; initial GitHub check rollup was empty (not a green-CI claim). Remote branch head was independently verified with git ls-remote. Operator checkout/private assets were not modified.

Fetched current main `92aff046f24b1353670f57a79576e495f4dd0394`. Its two upstream commits are merged #445 (`34f2600`) and #447 (`92aff04`), documentation/evidence only. Upstream introduces the WP58 plan and deployment evidence plus execution-plan append. These are preserved through a normal merge, not copied into this PR's diff or overwritten. No #446 initializer integration or changes.

### Reproduced defect and narrow correction

The prior renderer first reconciled model selections, but then trusted an existing keyed selectbox value whenever that value was still in the snapshot. After pruning removed model A, the keyed widget could hold a different surviving timestamp and silently replace the model's authoritative reset. It also called compare_candles a second time, clearing reset-cause state.

A lightweight Streamlit fake models keyed values, widget creation and pre-rerun on_change ordering. The existing panel was extracted without behavioral changes to expose a deterministic render test. An initial seam extraction omitted its local instrument variable and errored; that test-seam issue was corrected before measuring the product regression. The intended pre-fix pruning regression then **failed with an assertion, exit1**: model A retained the stale-but-valid widget timestamp rather than the first available candle. No real dataset/UTC value was used; all fixtures are fake.

Fix commit **`fa7083d30f2af0519c55e778e976d2409c5a10b4`**:

- Supported native selectbox on_change callbacks record explicit user intent in the instrument model before rerun.
- The comparison renderer reconciles exactly once against the current validated snapshot; resulting model selections are authoritative.
- Both keyed widget values are synchronized before selectbox creation, including still-valid stale values after reset. Widget return values no longer overwrite that reconciliation.
- Reset notices survive display/state inspection; incompatible generation/date/transform changes reset both, while valid intentional choices and compatible version refresh remain intact.
- Empty/missing comparison state clears its applicable widget keys and returns before creating invalid controls; one-candle snapshots use the same available UTC for A/B truthfully.

No DTO, viewport, inspection, Decimal policy, API, schema or dependency changes. No new event channel or JavaScript. The callback/session handshake is isolated per instrument and session.

### Actual tests and scope

```text
PYTHONPATH=python/presentation
python -B -m unittest test_research_chart.ComparisonWidgetHandshakeTests.test_pruning_authoritative_reset_overrides_stale_but_valid_widget_value -v
python -B -m unittest test_research_chart.ComparisonWidgetHandshakeTests -v
python -B -m unittest test_research_chart test_research_snapshot_client -q
```

| Execution | Actual result |
| --- | --- |
| Intended old-behavior pruning regression | FAIL,1 case, exit1 (expected pre-fix failure) |
| Post-fix handshake suite | 6 passed, exit0,0.017 seconds |
| Final focused chart/client suite | **90 passed, exit0,19.389 seconds, no skips** |
| Intermediate suite before sixth isolation case | 89 passed, exit0; superseded by90 |
| UTF-8/LF/no BOM and git diff --check | PASS |

Six new deterministic cases cover both symbols: stale-but-valid keyed state after pruning, removed widget values, generation/displayDate/transformEpoch resets and persistent notice/single reconciliation, ordinary A/B callback changes (including a valid user choice before a pruned rerun), compatible version refresh with current corrected values, missing/empty/single candles, and instrument/two-session callback independence. Tests preserve DTO, viewport and inspection state and retain all Decimal/null/no-gap/WP59/WP60 regressions. Existing bare-mode Streamlit ScriptRunContext warnings occurred; no product failure or runtime evidence is inferred from those warnings.

### Safe branch reconciliation

Committed the tested fix, then ran **git merge --no-edit origin/main**. Clean conflict-free merge commit **`0715ba6ad3c32968e48c947e6f4c0d57794d5596`** includes verified main `92aff046...`. Hash checks proved both tested Python files byte-identical across this documentation-only upstream merge, so the90-test run remains the applicable source result; no duplicate test-count claim or unjustified broad rerun. No rebase or force-push. The final evidence commit/head is reported in the PR handoff.

The effective diff against reconciled main remains exactly the same three authorized paths: chart module, chart tests and this existing WP61 evidence document. Upstream #445/#447 documents remain present with their merged content. #446 remains separate and untouched. Final push is a normal fast-forward of the existing PR branch; GitHub mergeability/check status is inspected after publication.

### Qualification limits and recommendation

**Revised image build, Docker/runtime and browser qualification: NOT_RUN.** This continuation's boundaries prohibit touching retained SQLite, observations, service key, TLS or rollback images. The supported launcher would recreate the retained pair and use these assets, so it was not invoked. No Docker, credential, private artifact, TLS or existing-container inspection/mutation occurred in this reconciliation. The prior local image/browser passes and earlier failed attempts remain recorded above, but do **not** qualify this revised callback/render code. Existing loopback URL is historical here; no fresh availability claim.

Recommendation: approve the deterministic selector correction and reconciled PR for merge after owner review, with post-fix actual-browser qualification explicitly outstanding rather than silently waived or represented as passed. No merge is performed by this task. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. No Azure/public/production/provider/acquisition/import/data-transfer/registry/dependency operations.

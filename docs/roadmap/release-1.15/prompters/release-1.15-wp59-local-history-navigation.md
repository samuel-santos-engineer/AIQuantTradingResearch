# Release 1.15 WP59 - local historical navigation and runtime handoff

Date: 2026-11-14. Status: LOCAL_UI_BUILD_AND_BROWSER_QUALIFICATION_PASS; OWNER_REVIEW_PENDING. Original blocker and subsequent recovery are recorded separately below.

## Baseline and scope

Live main verified at `4a6bc2b901d6f6ff0081dad4597064017acb86f9`. No tracked WP59 work-package collision was found on that baseline. Owner WP59 prompt is preserved in the original checkout, not copied into this PR. Work uses an isolated branch; the owner checkout and its untracked files remain untouched. PRs #445, #446 and #447 were independently verified OPEN at their recorded heads. No changes or implementation from those PRs were incorporated. In particular, #446's initializer is absent.

Exactly three changed paths:

- `python/presentation/research_chart.py`: inclusive UTC candle-bound navigation using the existing supported Streamlit form/select-slider. It sends an explicit viewport command, invalidates pending gestures, and clears saved y ranges for rescaling. All loaded candles and API-supplied analytics remain intact. Full snapshot, single-candle and subset bounds are supported; arbitrary provider history is not.
- `python/presentation/test_research_chart.py`: fake-only tests for bounds, ordering, empty state, no mutation on rejection, per-instrument SMA alignment/warmup, null volume, stale gestures and disclosed gaps.
- This runbook/evidence file. UTF-8/LF/no BOM is mandatory for all changed text.

No API, schema, dependencies, indicators, instruments, authentication, provider acquisition or Docker packaging changes. Existing data status, gap and availability messages remain. The existing snapshot is capped at 1,000 completed one-minute candles per instrument. The selected range is a viewport, not a new API query or recalculated indicator window. Return to Live and presets restore their existing behavior. Historical navigation does not make data current.

## Current local inventory and stop condition

Executed read-only Docker inventory: Linux engine; both accepted IDs remain Linux/amd64:

- UI `sha256:3270ce6ec33ba8cb7da27738ea6d9dc27a811a3270e7ecdef83f32e757a83ae1`
- API `sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028`

Existing UI container is running with loopback mapping `127.0.0.1:18555->8501`. This is process/port evidence, not fresh functional acceptance. Inside that UI, a bounded nonsecret check confirmed the presence of `AIQ_RESEARCH_BASE_URL`, `AIQ_RESEARCH_API_KEY` and `AIQ_RESEARCH_CA_FILE` (names/presence only). The public CA's current validity interval check returned **false**. No key values, certificate contents, private paths or observations were printed. This check alone does not distinguish expired from not-yet-valid material.

Stopped operational qualification at that TLS prerequisite failure, as WP59 requires. No serving probe, browser session, data read, rebuild, pull, new container/network/volume, restart, credential handoff or certificate replacement followed. Accepted images and the retained runtime/data were not modified. Current image-content re-audit and current authentic data acceptance are NOT_RUN; historical Phase G audit is not a new WP59 audit. No claim that an arbitrary local image ID establishes a registry/runtime manifest identity.

## Repeatable existing two-container operator path

Prerequisites: Windows PowerShell 5.1, existing project Python/dependencies and .NET 10, Linux Docker, exact locally verified images, an existing protected preparation/import receipt, matching image/source identity, a retained authentic import in the session-owned volume, a valid local TLS configuration, and an unused loopback port. Do not disclose private preparation paths or secrets. No automatic provider acquisition or data transfer is supplied by this change.

The existing `eng/docker/release-1.15/wp39_local_history.py` remains the supported workflow. It dispatches to `wp07_local_historical_chart.py`; that launcher explicitly connects UI to `https://wp07-research:52731`, publishes UI port 8501 only on `127.0.0.1`, uses the private network and CA, checks authenticated research readiness before UI readiness, and uses the existing bounded deadlines/fixed failure summaries. Inspect failures rather than looping Start.

After the TLS blocker is resolved and the preparation/image identities are verified, use the same protected prepared directory and an available loopback port in PowerShell 5.1:

```powershell
# $preparedDirectory is an owner-selected protected preparation, never a Git path.
# $projectPython is the existing project venv interpreter; no package installation.
& $projectPython -B eng/docker/release-1.15/wp39_local_history.py --action Plan
& $projectPython -B eng/docker/release-1.15/wp39_local_history.py --action Start --directory $preparedDirectory --port 18555
if ($LASTEXITCODE -ne 0) { throw 'Local Start failed; preserve state and inspect the fixed failure category.' }
& $projectPython -B eng/docker/release-1.15/wp39_local_history.py --action Verify --directory $preparedDirectory
if ($LASTEXITCODE -ne 0) { throw 'Local Verify failed; do not claim browser acceptance.' }
```

These commands are documentation, NOT operations executed in WP59. Start can create/replace the session containers/networks and seed a new volume if absent; verify existing ownership/resources first. Do not run it against the historical preparation as a shortcut: WP57 records that its original manifest selects the old UI, whereas the corrected pair was selected using explicit supported launcher parameters. The new WP59 source also needs a separately qualified local UI image before this startup procedure can exercise it. No historical manifest is edited to disguise a build identity.

Private initialization/import is a **separate local-only operator step**, never startup magic. Existing Import requires the retained verified acquisition bundle/provenance/receipt and a stopped owned session; Start requires the matching receipt/state. This PR adds no mechanism to move retained data between preparations, repair expired certificates, or reuse private configuration in a fresh preparation. Do not run Acquire, regenerate history, initialize an empty replacement, or copy SQLite to bypass that gap.

Single next local action: review a bounded TLS/configuration recovery and corrected-UI qualification plan that preserves the accepted images and imported volume. Do not replace only the CA file: server TLS and UI trust must remain consistent. After safe recovery, build only the needed local UI image with cached dependencies and an allowlisted public context, then qualify BTC -> ETH -> BTC and navigation against that image. No Azure or registry publication.

## Verification matrix

| Check | New WP59 result | Limit |
| --- | --- | --- |
| Live main and three open PR heads | PASS | No merges/rebases |
| Accepted local pair present; Linux/amd64 | PASS | Metadata only; images untouched |
| UI process/loopback port | PASS | Does not prove data serving |
| Required configuration names present | PASS | No values/equality checked |
| Public local TLS validity now | FAIL | Operational stop; no retry/repair |
| Offline chart/client regression | See final test result below | Fake data only |
| New bounded-range regression | 4 passed, exit 0 | Includes real figure projection, no browser |
| Fresh image build/content audit | NOT_RUN | Runtime qualification stopped |
| Current authentic data/SMA/full-range browser | BLOCKED | TLS prerequisite; no PASS inferred |
| New navigation actual browser acceptance | BLOCKED | Source not deployed to retained runtime |
| Missing-data actual runtime check | NOT_RUN | Existing offline availability tests only |

Initial combined chart/client command ran 69 tests, exit 0, before adding the fourth gap test and extending the stale-event assertion. Focused final range command ran all four cases, exit 0. Existing Streamlit bare-mode ScriptRunContext warnings occurred during the combined offline suite; they are not browser evidence. Final combined chart/client suite: **70 tests passed, exit 0**, 40.167 seconds, no skips. Separate byte checks confirmed UTF-8/LF/no BOM/final newline on all three changed files; git diff --check passed.

Commands (existing installed venv, no dependencies installed):

```text
PYTHONPATH=python/presentation
<existing-project-venv-python> -B -m unittest test_research_chart test_research_snapshot_client -q
<existing-project-venv-python> -B -m unittest test_research_chart.HistoricalRangeTests -v
git diff --check
```

Historical WP57 BTC -> ETH -> BTC acceptance remains historical: 1,000 candles per instrument, SMA20 19 null/981 computed, null volume/no bars. It does not qualify this new navigation source. `LOCAL_CHART_VERIFIED=True` is historical only. New WP59 local acceptance is BLOCKED, not complete.

Azure staging is deferred, last observed quota-blocked; no Azure/registry/provider/production operation or quota monitoring occurred in WP59. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. No new claim about authentic import or public readiness is made. Review PR remains OPEN/UNMERGED, with runtime/browser qualification outstanding.

## Owner-authorized local TLS recovery - 2026-11-14

Owner instruction: "continue local tls recovery". This continuation authorizes recovery of the retained local session, not Azure operations or a claim that the WP59 navigation source is deployed.

The original public certificate was subsequently confirmed **expired**. Existing .NET preparation generates one-day TLS plus unrelated service/HMAC keys; it was deliberately not rerun. Instead, an isolated temporary .NET 10 program using the same RSA2048/SHA256, DNS SAN `wp07-research`, CA extension and passwordless PFX/PEM format generated **only** a new TLS pair. Validity is seven days from generation (one-minute not-before tolerance). Temporary output is under a new Windows owner/SYSTEM-protected directory. Framework-only restore used an empty local feed and disabled NuGet auditing; no dependency installation, provider or registry request. No service/HMAC key generation, database initialization or private observation access.

Before mutation, ownership labels, exact accepted image IDs, imported volume ownership/default driver, loopback mapping, matching read-only CA/server mounts, and existing API/UI service-key equality were checked privately. No key values, fingerprints, private paths or certificate contents were published. The original service key was inherited only by the bounded launcher subprocess and cleared from temporary environment dictionaries in finally; plaintext process memory remains inherent to the existing workflow and was not claimed securely erased.

Two recovery-driver issues were diagnosed explicitly:

1. Initial preflight compared Docker Desktop's Linux host-mount path representation to its Windows bind path. It stopped before any container mutation. Corrected normalization proved both referenced the same certificate and were read-only.
2. Owned Stop succeeded, but the driver attempted JSON parsing of the launcher's text summary. Inspection confirmed both owned containers stopped; Start had not run. Source review corrected parsing to read the launcher's newly created task-owned structured result. The pending Start was executed once; no repeated Stop or restart loop.

The supported launcher replaced only this session's two containers and two networks, using the same accepted API/UI image IDs and existing imported state volume, with the new TLS directory. Original TLS file size/modification metadata remained unchanged; private PFX contents were not read or hashed. The historical preparation and identity manifests were not edited. No reimport, copied database, provider acquisition, image build/pull/push, unrelated resource change or Azure action.

| Recovery verification executed now | Result |
| --- | --- |
| New public certificate valid with expected DNS SAN | PASS |
| Existing API/UI shared-key equality, no disclosure | PASS |
| Owned Stop | PASS |
| Owned Start with consistent server TLS/UI CA | PASS |
| Authenticated TLS snapshot BTC/USD | PASS - 1,000 candles |
| Authenticated TLS snapshot ETH/USD | PASS - 1,000 candles |
| Loopback Streamlit readiness | PASS |
| Same accepted API/UI images and imported volume retained | PASS |
| Original TLS metadata preserved | PASS |
| New WP59 navigation image/browser qualification | NOT_RUN |
| Current browser SMA/controls acceptance | NOT_RUN |

The accepted historical chart is available again at **http://127.0.0.1:18555**. Startup snapshot/readiness evidence establishes recovered TLS transport and snapshot availability, not a fresh browser acceptance or raw-data/database byte audit. No raw candle contents or identity timestamps were reported. This recovery does not refresh or acquire historical observations.

New protected TLS material and private recovery metadata are retained outside Git. The old preparation still references its expired TLS and original UI manifest: **do not restart from that historical manifest blindly**. The active pair uses the new directory through the supported launcher's explicit `AIQ_CHART_TLS` parameter; private operational metadata records the exact target without publishing it. A future startup must use the current consistent TLS pair and recheck validity. The seven-day certificate remains temporary, not an automatic renewal feature.

No application/test code changed during recovery, so the earlier 70-test offline result is retained without rerunning or presenting it as a new test execution. Documentation UTF-8/LF/no BOM and git diff checks were rerun. PR #448 remains draft/open/unmerged. Next local action is a bounded build and browser qualification of the new WP59 UI navigation against retained data; neither is implied by TLS recovery. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Owner-authorized UI build and browser qualification - 2026-11-14

Continuation authority explicitly permits one local UI build, same-session runtime replacement required by the supported launcher, bounded browser qualification and this evidence append. PR #448 was verified draft/OPEN at `a8ce55ba0d1a81429f88df66436b97f2283631d4`, with the expected three paths and clean isolated worktree. No source changes or unmerged initializer incorporated during this continuation. Earlier NOT_RUN/BLOCKED entries above describe the earlier pass; this section supersedes those runtime qualifications only as explicitly measured below.

### Preflight, build and source identity

Read-only preflight confirmed both exact accepted images, running container source IDs/session labels, existing imported volume ownership/default driver, matching read-only server/UI CA mounts, recovered certificate current validity/DNS SAN, matching existing service keys and loopback port. Authenticated snapshots returned exactly 1,000 BTC/USD and 1,000 ETH/USD candles. A protected private rollback ledger retained exact original container/network/volume identities, images, port, environment **names only**, and current TLS references; no keys stored in that ledger. Private operational directories were not copied into Git or the build context.

Exact cached build inputs were verified: qualified dependencies `aiq-c2b36:candidate` at `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`, and pinned Python base `sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024` (Linux/amd64). Public-only context contained ten existing presentation source/assets plus favicon, copied by the existing workflow's literal allowlist. No tests, private preparation, SQLite, observation bundle, keys or TLS were included. A separate copy of the supported Dockerfile was used without modification.

Build command (private directory placeholders intentionally omit actual host paths):

```text
docker build --pull=false --network=none --platform=linux/amd64 --build-context qualified_python=docker-image://aiq-c2b36:candidate -f <owned-build-directory>/research-chart.Dockerfile -t aiq-wp59:local-a8ce55b <owned-build-directory>/context
```

UI build exit **0**. New immutable **local image ID**:

`sha256:1aa45d94772bb8ca485647a2c61d891bf735dc55c204738c8a2ac06b5bf3d2d3`

Linux/amd64; UID/GID `12002:12002`; port8501. Entrypoint remains `/opt/venv/bin/python -B -m streamlit run /app/python/presentation/realtime_financial_visualization.py` with the supported Dockerfile's fixed address/port/headless/no-watcher/no-telemetry/no-welcome/hidden-error/no-secrets-file flags. A disposable no-network, read-only, capability-dropped container hashed the eleven `/app` files in memory and matched the audited public context exactly, including the new navigation source. It was run with `--rm`. This verifies application-file equality and context exclusion, not a new universal audit of every inherited OS/dependency layer. No API build, pull, registry publication or accepted tag overwrite.

Fresh focused regression command (existing venv, `PYTHONPATH=python/presentation`):

```text
python -B -m unittest test_research_chart test_research_snapshot_client -q
```

**70 passed, exit0, 46.789 seconds, no skips.** Existing bare-mode Streamlit ScriptRunContext warnings occurred. No dependency installation or broad-suite claim.

### Supported runtime switch and preservation

The existing launcher has no UI-only replacement action. Following the explicit continuation allowance, its Stop then Start replaced the two owned containers and their two session networks once, retaining the exact API image, imported volume, service key and recovered TLS pair while selecting the new UI. No unsupported manual UI swap, manifest rewrite, seeding, reimport or certificate/key generation occurred. Both launcher commands exited0; authenticated TLS readiness returned 1,000 rows for each symbol, then loopback Streamlit readiness passed. No recovery/rollback attempt was needed.

Post-browser inspection independently verified the same API image, same named volume identity and complete ownership labels, matching API/UI key equality, both running source IDs, loopback-only `127.0.0.1:18555`, and continued availability of both accepted historical images. The API **container** was recreated because the supported launcher requires it; the API source/image and imported volume were retained. Database bytes were not audited; normal startup/reads can operate on existing state. No claim that every database byte remained unchanged.

### Actual browser evidence

Installed Chrome with owned profiles, existing venv/websockets, loopback CDP and existing browser harness. No new browser dependency. Browser networking extras/updates/sync were disabled. Only normal local UI snapshot consumption; no provider, Azure or direct private-data probes. No screenshot or raw figure/price/time dump was published. Compact observations report only counts, booleans and relative candle indices.

There were **eight bounded browser sessions**: three partial acceptance runs stopped at navigation locators; three structural diagnosis sessions; one diagnosis stopped on a temporary helper NameError; one final complete acceptance pass. Structural diagnosis established no application exception, a working expander, and native `input type="range"` controls (implicit slider semantics, no literal role attribute). The final driver used a fixed1280x1200 viewport, immediate scrolling, native input focus and actual CDP keyboard events, plus mouse clicks on real controls. It never assigned slider values, injected candle data or patched the application/DOM to simulate success. The temporary-driver write initially needed the ordinary sandbox escalation for its protected task-owned directory; no browser started for that filesystem error. No application correction or rebuild was needed for these driver errors.

Reproducible interaction: visit loopback chart; BTC -> ETH -> BTC; open the native navigation summary; use Home/End and ArrowRight on the two native range inputs; submit the real Apply button and check the resulting graph viewport. Bounded selection used retained indices5..20 inclusive (**16 candles**); single-candle used0..0; full selection used0..999. Full arrays remained loaded. Test code compared actual graph ranges to the retained candle timestamps in memory and emitted only relative indices/counts. This is result-state evidence, not a claim that a click alone passed.

| Final actual browser case | Result |
| --- | --- |
| BTC -> ETH -> BTC: correct mounted instrument | PASS |
| Loaded candlesticks each instrument | PASS - 1,000 |
| Supplied SMA20 arrays aligned with candles | PASS - 1,000 entries |
| Full-snapshot SMA20 warmup/computed counts | PASS - 19 null / 981 computed |
| Volume-unavailable annotation; no volume bars | PASS |
| Existing Plotly modebar | PASS |
| Inclusive bounded UTC viewport | PASS - indices5..20, 16 visible candle starts |
| Single-candle UTC viewport | PASS - one candle start |
| Full-range selection | PASS - all1,000 candle starts |
| Retained-candle preset100 | PASS - 100 visible candle starts |
| Return to Live after explicit range | PASS - restored selected100-candle preset |
| Real pan/zoom followed by explicit range command | PASS - resulting range stayed5..20 after settling |
| Final preset1000 / full-snapshot restoration | PASS |
| Existing research/freshness status caption | PASS |

The pan/command check establishes the observed command won after a real gesture; it does not assert a forced obsolete-event replay or exhaustively characterize every debounce race. The focused offline stale-command rejection test separately passed. Gap preservation is covered by fake regression; a new live gap or empty dataset was not fabricated or induced in the retained authentic runtime. Actual missing-dataset/empty-state fault injection is NOT_RUN; existing offline availability tests remain the evidence for those states. Freshness/status display does not make the retained historical data current or prove new acquisition. Browser profile/process cleanup was bounded and limited to owned browsers; the application pair remains running for owner review.

### Disposition

**Recommend marking PR #448 ready for owner review** based on passing focused tests, new local image identity, authenticated readiness and final real-browser acceptance. PR remains **draft, OPEN and UNMERGED**; no automatic readiness transition or merge. Local URL: **http://127.0.0.1:18555**. Rollback ledger and accepted images retained; rollback NOT_NEEDED. Current TLS remains the previously recovered seven-day pair; restart through the old preparation is still unsafe because it references historical TLS/UI metadata. Use the explicit retained configuration, never silently rewrite historical manifests.

Only this existing evidence document changed in this continuation; application/test source remains the built PR implementation. UTF-8/LF/no BOM and diff checks apply before committing. PRs #445/#446/#447 remain untouched. No Azure, cloud/production, provider/acquisition, registry push, data import/transfer or dependency operations. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`; local success is not Azure proof.

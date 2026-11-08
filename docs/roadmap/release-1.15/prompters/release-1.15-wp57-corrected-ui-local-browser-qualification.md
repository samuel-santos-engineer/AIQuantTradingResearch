# Release 1.15 WP57 - corrected UI local runtime and browser acceptance

Date: 2026-11-08. Exact corrected source PR #444 OPEN/UNMERGED head `be6e7a21ce04edf5fa45883383bfaf385228afbd`; based directly on main `61a1f804526b145e8a3d47f1daf4795f3fbec375`, not stacked on evidence PR #443. #444's four reviewed paths and clean worktree verified before preparation. Evidence PR #443 separately verified OPEN/UNMERGED at prior head `470561539372adde0ec0452cfc8a930f06b6e755`; this new note and execution-plan append preserve its earlier acquisition history. Owner checkout remains on its existing dirty branch, untouched; no owner documents/secrets read.

## Authority and offline preflight

WP57 initially allowed offline inspection/tests only. Source inspection established that the existing launcher has no UI-only action: Stop stops both session containers, Start replaces both and the two owned session networks. Its supported explicit image/volume/TLS parameters permit retaining the original research image, auth/TLS and imported volume while selecting a new UI. No unsupported UI-only shortcut, preparation-manifest edit or new application workflow was introduced.

Offline chart/client66 passed, workflow38 passed/1 Windows unprivileged symlink skip of39, all exits0 (105 run104 passed1 skip). Existing project venv/dependencies only; no installations. Commands:

```text
PYTHONPATH=python/presentation
<existing-venv-python> -B -m unittest test_research_chart test_research_snapshot_client -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp39_local_history.py -q
```

No C# change or broad-suite rerun/green claim. Prior29 baseline-matched Infrastructure harness prerequisites remain open and separate. Existing Streamlit bare-mode warnings do not affect the reported results.

Owner then **explicitly approved** the concrete plan: selected Docker/session/input identity and metadata checks; one fresh cached Prepare from exact #444; paired-container/two-network replacement while preserving original research image/data/auth/TLS; one rollback if switch failed; one bounded BTC->ETH->BTC Chrome session. Approval explicitly covered the container/network replacements because the current workflow cannot switch only UI, and normal private local service-key handoff without output/logging. No old images, volume, database, bundle, TLS files, manifests or unrelated resources authorized for deletion/edit. No acquisition/import/provider credentials/requests, pulls, dependencies, registry/Azure/public deployment. Approved scope is consumed as recorded below; no blanket future operational authority.

## Executed identities and fresh preparation

Docker context desktop-linux/linux engine, expected owned research/UI containers both running. Filtered metadata, not container environment values or database contents, verified exact old images, session labels, internal/private and ingress networks, default-local state volume ownership and original research /private mount; UI loopback binding127.0.0.1:18555. Owner ACL protected for owner/SYSTEM only; public certificate valid/DNS wp07-research. Actual old preparation full publication/context/workflow identity passed. No assumption that the previous URL was still active.

Cached inputs checked by exact image ID: dependency `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`, ASP.NET `sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f`, Python `sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024`; all matched, no pull.

Ran existing `python -B eng/docker/release-1.15/wp39_local_history.py --action Prepare --directory <new-owned-temporary-directory>` **once**, timeout240 seconds, exit0, exact PREPARED_NOT_IMPORTED fixed output and empty stderr. Nonsecret tool/cache environment allowlist only; no provider channel. Cache-only restore/publish, private normal local configuration creation, two --pull=false/--network=none builds; new outputs retained. Full40-file publish/context/workflow identity passed. New UI context bytes for the chart Python/JS and snapshot client matched exact reviewed source; generated preparation owner ACL passed. New generated credentials/TLS private contents were not inspected. Both old manifests remain byte-for-byte unchanged. Fresh preparation's generated research image is retained but deliberately unused in the active runtime.

| Runtime/build identity | Value |
| --- | --- |
| Active corrected UI image | sha256:3270ce6ec33ba8cb7da27738ea6d9dc27a811a3270e7ecdef83f32e757a83ae1 |
| Active original research image | sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028 |
| Original UI image retained for rollback | sha256:bb093dcd2b866044adb5002a8916909f0cc5aa896778e030dc89c7a25bb748c1 |
| New prepared research image, unused/retained | sha256:886e4c52fbb17f928214b102d8ed942a566a661e2638bbd91e48481b1406c194 |
| research_chart.py SHA-256 | 12d51ed1797df402f5d121d011c9e77997a44926641c333b4ce27549a33d54ea |
| research_chart_component.js SHA-256 | 9d02c9e0af6b4cd887241f04d706c01b979cb3afa805f612f71bd97616c7ec4a |
| research_snapshot_client.py SHA-256, unchanged | 68ddf6e4cfe091c61b5015dd84395d0d4e7e10fc65523ee3f85e411fdf80515e |

No secret fingerprints or private locations published.

## Controlled switch and preservation

Used the existing `eng/docker/release-1.15/wp07_local_historical_chart.py` engine behind the supported `Invoke-Wp07LocalHistoricalChart.ps1` image/volume/TLS/session parameter interface. A bounded Python driver supplied the same documented environment parameters (no policy bypass), selected original research image + corrected UI image + original state volume/TLS + existing session + port18555. Existing generated service key entered only its isolated transient environment and was cleared in finally; no key argument/value/log or provider key.

Stop exit0/STOPPED_STATE_PRESERVED; Start exit0/RUNNING_LOCAL_ONLY. Only the two approved owned session containers and their two networks replaced by existing launcher behavior. No state seed, database regeneration, reimport or new data volume. Start's bounded authenticated local HTTPS probe returned both symbols1000 rows. Post-switch filtered Docker inspection independently matched active UI/research image IDs and original research volume mount/owner label. No database contents inspected. Old `local.json` and preparation identity manifest hashes recomputed unchanged; no guard bypass. No new provider request, Acquire/Import or old artifact update.

One original-image-pair rollback was authorized in case of switch failure, but **not needed/not executed**. The active corrected runtime remains available at **http://127.0.0.1:18555**. Existing images/data/TLS/volume and fresh artifacts retained. The old preparation manifest intentionally still identifies its original UI; restarting via that old high-level Start would select the old UI. Reuse the recorded supported explicit image parameters for this corrected runtime; do not edit the old manifest to pretend it built the new UI. Future runtime operations need applicable owner authorization.

## One actual browser qualification

Existing installed Chrome155.0.8059.86/project venv; owned fresh profile, loopback CDP and nonsecret environment only, background networking/component updates/sync disabled. Driver Python syntax checked offline before launch. One visit, BTC->ETH->BTC sequence; each graph wait bounded30 seconds, exact stage/category on failure. Used normal page local snapshot consumption, no direct API/provider probes. Chrome closed successfully after the test; no unrelated browser/process touched. No screenshots/raw prices/timestamps/figure dumps/private paths/keys/configuration published.

| Actual rendered check | Initial BTC | ETH | Return BTC |
| --- | --- | --- | --- |
| Correct checked radio and data-bearing graph instrument | PASS | PASS | PASS |
| Candlestick rows | 1000 | 1000 | 1000 |
| Aligned supplied SMA20 rows | 1000 | 1000 | 1000 |
| Non-null SMA20 values | 981 | 981 | 981 |
| Initial19 SMA warmup entries remain null | PASS | PASS | PASS |
| SMA timestamps align with candles | PASS | PASS | PASS |
| No volume bars; Volume unavailable annotation | PASS | PASS | PASS |
| Hover/page educational transformed-value disclosure | PASS | PASS | PASS |
| Existing observed-provider provenance caption | PASS | PASS | PASS |
| Connected/1000-retained-candles captions | PASS | PASS | PASS |
| Plotly modebar present | PASS | PASS | PASS |
| No page exception, captured JS exception or console error | PASS | PASS | PASS |

Failed same-origin assets:0 captured. No retries, additional selection attempt or forced refresh needed. SMA values are supplied by the C# snapshot path; Python/JS do not recompute indicators. Modebar presence is reported honestly; this scope did not requalify every pan/zoom/control or a long resource-stability soak. This closes the observed ETH component-mount failure on the tested corrected UI, not public runtime acceptance.

## Acceptance and remaining boundaries

| Gate | Result and evidence basis |
| --- | --- |
| Offline focused tests | PASS,104 pass1 skip/105, new WP57 execution. |
| Corrected UI build | PASS, exact #444 source/new identity-valid preparation. |
| Local switch | PASS, actual launcher exits and active image postchecks. |
| BTC browser / ETH browser / BTC return | PASS / PASS / PASS, one actual Chrome session. |
| Both-symbol API | Prior WP55 authenticated match PASS; new Start authenticated probe also1000/1000. No new acquisition. |
| Imported data preservation | Same original volume/owner labels/mount and research image, no seed/import; new probe and actual browser show both histories. Not a new byte-for-byte database-content audit. |
| LOCAL_CHART_VERIFIED | **True**, for tested corrected local runtime. |
| PUBLIC_RUNTIME_VERIFIED | **False**. |

Current head under test is unmerged #444; no PR merged. Active composition uses an explicit supported image selection; the historical manifests remain immutable. Public certificate has its originally limited validity; localhost success does not remove certificate lifetime, host availability, F1/public deployment, quota or broad-suite obligations. Earlier WP55/WP56 LOCAL_CHART_VERIFIED=False entries remain valid historical states, superseded only for this tested runtime by this new evidence. Both review PRs remain OPEN/UNMERGED. Owner decision is review/disposition of code and evidence; public activation requires separate qualification/authority.

Only this new WP57 note and execution-plan append modify the evidence branch; no production code changes. UTF-8/LF/no BOM and git diff --check verified before commit. No provider calls/credential inspection, acquisition/import, raw historical content/database inspection, registry/Azure/public deployment, unrelated resource deletion or public-readiness claim.

`AUTHENTIC_DATA_NOT_IMPORTED=False`; `LOCAL_CHART_VERIFIED=True`; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

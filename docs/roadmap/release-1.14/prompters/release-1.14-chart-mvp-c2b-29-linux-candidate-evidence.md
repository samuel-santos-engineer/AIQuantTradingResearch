# C2B-29 ? Intended Linux candidate evidence

2026-10-16. **PASS for bounded local showcase qualification; GO for owner review of future registry-promotion authority only.** No promotion is authorized or performed. PUBLIC_RUNTIME_VERIFIED=False; advanced WP07 ON HOLD; public activation/deployment NO-GO. No Azure F1 certification or public runtime gate passage is claimed.

## Provenance and layout

See [contract/gate map](release-1.14-chart-mvp-c2b-29-linux-candidate-contract.md) and [complete sanitized measurements, hashes and descriptors](release-1.14-chart-mvp-c2b-29-linux-candidate-evidence.json).

Main: 612483e570d4b8e1f99d512681ca26b1bc1320cd. PR #347 dependency commits 35e09dc4b98669111bb88cd83f6d1cf662d29f15 and 8240d0878dea335c08ad3216600fc7e545446140 incorporated with `cherry-pick -x` as 3ee9331 and 83822b4. PRs #346/#347/#349 were verified open at their authorized heads and are unchanged. An isolated worktree preserves the original branch/HEAD/two untracked owner documents; fourteen earlier owner-document raw hashes match C2B-26 inventory. Protected WP07/archive, frozen generator/calculator/E4 reference and local HTTP source were not edited.

Fresh context: git archive of 83822b4 plus enumerated Dockerfile, entrypoint, immutable reader/test and requirements-linux.lock edits; 2,054 finite source/document files inventoried. Existing dockerignore excludes local credentials, signing props, build outputs and candidate assets. Only explicit COPY inputs enter the final image; no arbitrary untracked files or generated market assets are committed. This is the repository Dockerfile's intended final `synthetic-candidate` target, not a temporary overlay. Private packaging is retained as explicit `private-runtime` target, unqualified here; changing the Dockerfile default is not an implicit private deployment migration.

SDK Linux/amd64 pin: `sha256:3a48280a9a7c6276c3eeaba1efa6a08820f18c84fb9e6274c187719ecfaf9fbc`. Python pin: `sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024`. Python 3.13.16, Streamlit 1.61.1, Plotly 7.1.0. Existing five direct dependencies unchanged; 44 resolved versions constrained, downloaded wheel hashes recorded by pip inside the image and evidence JSON. NuGet versions remain centrally declared. Restore/build/install use package networking; C# generation is explicitly `RUN --network=none`, with producer deadline 60s, observed ~2s. This does not claim bit-reproducible future rebuilds or hash-locked NuGet restoration. Linux Release build uses no Windows signing exception.

Runtime UID/GID 1000:1000, root-owned 0444 assets, 0555 application directories; root-owned ancestors, no public-writable ancestors or links. Fixed asset path and external-to-artifact code pin unchanged. Public acquisition has no caller root/pin. No .NET runtime, Worker directory or C# process in the final image. Read-only root, only 32 MiB /tmp tmpfs writable. Default chart unavailable. Isolated local qualification checks inherited environment before sanitization; never manufactures Development or strips Azure markers to pass eligibility.

## Image identity and retention

Platform manifest: `sha256:55d142b57d0df9953f01350263369ee2ecd9a66019d1844d1f424ea91e0999d5`.

OCI config: `sha256:27ec5f7297205ccc4d0ba5c7987effc2c8aa710904f6850ea63d16d51e69f56d`. Twelve compressed layer descriptors in JSON. Build uses `--provenance=false`, single Linux/amd64 platform manifest; no published registry index/attestation. Saved archive index hash is separately labeled and is not a registry/platform digest.

Local `qualified-image.tar`: 250,158,592 bytes; SHA-256 `700b49d2f0bacb7db74b5520faabed3b94519355cd54daabc3f6be11bf0ea7a0`. Exact platform manifest blob exists in the archive and independently rehashes correctly, as do config and all twelve layers. No rebuild after this image's acceptance tests. Runtime source commit is `346841f6478d21d145b850272a5e0d3af3dcc1fb`; subsequent changes are evidence-only. All 2,054 raw context files match the preserved source worktree. Git archive on this Windows checkout converts four LF files to CRLF (Dockerfile, reader, reader test, constraints); committed source content matches after line-ending reconciliation. Raw context inventory hashes and exact saved image remain authoritative for the tested bytes; do not equate a new archive/rebuild with this artifact. Docker 29.7.2 / Desktop 4.88.1 / buildx 0.36.1 / Linux kernel 6.18.33.2-microsoft-standard-WSL2.

## Acceptance matrix

| Category | Result | Evidence and limits |
|---|---|---|
| Finite artifact/reference | PASS | 13 files, 656176 bytes; 1454-byte manifest pin unchanged; every member hash matches C2B-25. Unchanged independent E4 fixture derives its own complete data, receives no production candles; all three complete dataset hashes, twelve calculation/visible identities, every visible OHLCV byte and every SMA string match. |
| Startup/layout/lifecycle | PASS | Non-root, read-only, root-owned fixed assets; write attempts denied; default health starts with networking completely disabled and chart remains unavailable. Both modes exit 0, restart health passes; no OOM or C# process. |
| Functional/negative | PASS | Twelve selection counts/alignment/warmup exclusion; 28 focused unittest cases; twelve semantic mutations after test-only repinning; five actual filesystem faults; ownership, links, bounds, duplicate/UTF-8/integrity/deadline and concurrency checks. |
| Browser | PASS | Corrected exact image: 1D 24 rows, 30D 720; visible SMA false ? true ? false via native popover/checkbox, screenshots inspected; candles/volume, UTC dates, range and interval selectors (4h/1D six rows), hover, modebar, x/y zoom and pan. All 744 browser SMA float values exactly match immutable string-to-float conversion, no indicator recomputation. |
| Routes/security | PASS locally | Root 200 HTML, health 200 text; dataset/candles/indicators 503 JSON DATASET_NOT_READY/no-store; unknown API 404 JSON NOT_FOUND; admin 404. Candidate/member/source probes expose no files (SPA HTML or 400). Eight inherited-marker/conflict cases exit 64 before sanitization. Not complete WP07 confidentiality/network-isolation acceptance. |
| Resource/session/disconnect | PASS bounded screen | 1 CPU/512 MiB/64 task limit, swap extension disabled, capabilities dropped, no-new-privileges. No OOM; details below. This is not Azure capacity/abuse certification. |
| Cleanup/preservation | PASS | Owned runtime resources and scratch build context removed after exact archive validation; evidence/archive retained. No shared prune, registry push, Azure action, merge or protected-worktree modification. |

Default cold startup: 5.012s including Docker launch/exec probe overhead; ~4.544 CPU seconds including probes; peak 81,539,072 bytes/11 tasks. Browser scenario: peak 96,960,512 bytes/21 tasks; 13.238 CPU seconds across recorded scenario. Reader peak RSS 38,408 KiB; twenty repeated 90D reads: retained RSS 37,609,472 ? 37,146,624 bytes. Eight parallel readers: one admitted, seven denied without queue. Two concurrent tabs: one chart and one safe unavailable; no automatic retries. A third loading tab closed after 50ms; health remains ready. At ~172s after disconnect, memory 92,729,344 versus 93,278,208 bytes before; no OOM; default disconnected-session TTL 120s. Qualification restart polling: 5.959s including stop overhead.

Cancellation evidence is a native-browser disconnect/health and bounded-retention screen. Synchronous validation checks a cooperative two-second budget between finite steps; immediate preemptive cancellation is not established. No hard CPU ceiling, exact session-expiry instrumentation, packet-level outbound audit, durable global abuse/quota qualification or F1 guarantee is inferred. These limitations must remain visible in owner review. Owner-accepted zoom/pan reset on SMA toggle remains unchanged.

## Complete byte inventory

Manifest SHA-256 `007b2c8a6ccec19ec54976b4baf735812b648d79294e6465df884c68537d3069`. Frozen seed 114/version/coverage and 250 hidden warmup unchanged. Test-only read-only reference mount uses `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`. It generates independently before the comparison harness reads packaged data; comparisons cover complete visible serializations and every SMA string, not sampled rows or tolerance checks.

| Member | Bytes | SHA-256 |
|---|---:|---|
| 1h-1D.json | 4463 | `1678f42a584e17b0bd6ee9f4d66958ab1d8e8e09d02b80a5d38b016349cc749e` |
| 1h-7D.json | 27950 | `f3adcf98555e9b4461c267afb9a51d7747288d20cd35320f9d186b49cfa47d5b` |
| 1h-30D.json | 117984 | `5373a8b1525a14185e81f59fdc71e7546f877825a984810ddca099059335aa53` |
| 1h-90D.json | 352844 | `3699a1a7a619257e12c5c30f7010f2ab3810c2d18f516c14b9ec7e437d3971e2` |
| 4h-1D.json | 1535 | `b7b3c5eaa3fae8a349736ef993bc2cea876bbefdbf379b67624b76c4a0aa025d` |
| 4h-7D.json | 7439 | `ba03fe99008a3f60643301bf09a32593a1c4864c3ed7dcaa84a4e664b381fe23` |
| 4h-30D.json | 30072 | `67c1c105631a57fde57e226ce7ccb7e24e50bd6a26747001bb815ce71a4aab32` |
| 4h-90D.json | 89109 | `a064e9a21a50500d2a340eda685856ff7b680f860c07a377b75694b7974997aa` |
| 1d-1D.json | 716 | `22e702dc5ebde1cb19c699869e1196fc94aaacd2709ca6678742539a66710baa` |
| 1d-7D.json | 1706 | `f9cb186409b8cb625a74f50751772766a053a8854b71515d87e791d2f9d0e33f` |
| 1d-30D.json | 5502 | `bd1826063754f399939c4737460f12384cdf14e20c3d41da91d9bf0b181660af` |
| 1d-90D.json | 15402 | `9130b027a3d4c46900ca36263e84ddf69a404fad0f03fe31f2ef04ad8bfc3ba5` |

## Commands and evidence

Evidence root: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b29-d76483136fbf46e79029e45d38c83807`. Screenshot files: `1D-off.png`, `1D-on.png`, `1D-off-again.png`, `30D-off.png`, `30D-on.png`, `30D-off-again.png` and hover/control captures; hashes in JSON. Owned Edge CDP loopback 9226, UI published only at 127.0.0.1:8506; separate bridge has IP masquerading disabled. No relay, new runtime service, application JavaScript or DOM patch. Automation reads chart state and uses native focus/key/mouse events. Bridge configuration is not claimed to prove kernel egress isolation. Default startup also tested with network=none.

```powershell
# Windows PowerShell 5.1-compatible host commands; $taskRoot = evidence root.
docker buildx build --platform linux/amd64 --target synthetic-candidate --provenance=false --load --metadata-file "$taskRoot/qualified-build-metadata.json" -t aiq-c2b29:qualified "$taskRoot/context"
# Final-image Linux test execution shares:
# --network none --read-only --tmpfs /tmp:rw,nosuid,nodev,noexec,size=32m,mode=1777
# --cpus 1 --memory 512m --memory-swap 512m --pids-limit 64
# --cap-drop ALL --security-opt no-new-privileges
# /proof is a test-only read-only mount, not image code. In /app/python/presentation:
python -B -m unittest test_immutable_synthetic_chart test_public_preview_api -v
python -B test_immutable_synthetic_chart.py --candidate /app/python/presentation/_immutable_chart_candidate
python -B /proof/check.py
python -B /proof/oracle_compare.py
python -B /proof/compare_browser.py
# Host browser scripts: .venv/Scripts/python.exe -B "$taskRoot/<script>"
# browser-clean.py, browser-controls.py, capture-lines.py, sessions.py
# Final archive, no rebuild:
docker image save -o "$taskRoot/qualified-image.tar" aiq-c2b29:qualified
```

Unit result: 28 passed, zero failures/skips, exit 0, 16.817s. Semantic integration: twelve selections + twelve rejected mutations, exit 0. Independent E4 comparison: three datasets + twelve full visible/SMA windows, exit 0. Reader proof: twelve selections, five faults, write/trust denial, concurrency and bounded retention, exit 0. Logs/scripts retained locally with hashes in JSON. No broad .NET requalification: Linux C# build and independent exact-output comparison cover this unchanged numerical implementation.

Pre-final findings: Streamlit welcome URL reporting invoked external-IP discovery; supported `--logger.hideWelcomeMessage true` prevents that path without address spoofing or gate changes. Earlier images excluded; all final checks rerun against the corrected image. A route-helper assertion initially confused permitted dataset metadata `generatorVersion` with asset leakage; restricted asset-content assertions to asset/source probes and reran successfully. A fixed two-second restart probe was premature; running state and eventual health verified, then bounded readiness polling passed. Existing `use_container_width` deprecation warnings and AppTest bare ScriptRunContext warning recorded, not silently fixed. No final application exception/OOM.

## Exact PR inventory

Relative to main, 16 paths (includes unmerged #347 dependency, whose two historical evidence docs are not substituted for this image's evidence):

- `.dockerignore`
- `.gitignore`
- `Dockerfile`
- `container/entrypoint.sh`
- `requirements-linux.lock`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- `src/AIQuantTradingResearch.Worker/ImmutableSyntheticChartHandoff.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ImmutableSyntheticChartHandoffTests.cs`
- `python/presentation/immutable_synthetic_chart.py`
- `python/presentation/test_immutable_synthetic_chart.py`
- `python/presentation/realtime_financial_visualization.py`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-25-immutable-handoff-contract.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-25-immutable-handoff-evidence.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-29-linux-candidate-contract.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-29-linux-candidate-evidence.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-29-linux-candidate-evidence.json`

## Future owner disposition

GO **only for owner review of a future registry-promotion authority**, with the bounded screening limitations above. Review the open/unmerged PR and retained exact archive first. Separate future authority must promote this manifest/config/layers unchanged, verify registry readback and prohibit rebuild/substitution. If the archive is missing or hashes differ, stop; do not rebuild and claim continuity. Temporary retention is not a durable release artifact store.

Azure's earlier 200-HTML API mismatch remains observed with unproven root cause and unresolved on Azure. No new Azure probes/writes, registry push, deployment, merge or activation occurred. Public eligibility, per-site digest/startup/route/browser/resource/rollback evidence and applicable G-B0?G-B7 predicates still require distinct authority. Current deployed digest is not asserted to be a qualified rollback. Private-runtime target is unqualified here. PR remains open/unmerged; public activation and deployment NO-GO.

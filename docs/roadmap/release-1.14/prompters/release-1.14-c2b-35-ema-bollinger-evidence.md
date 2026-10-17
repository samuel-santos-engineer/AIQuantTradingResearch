> FINAL C2B-35: staging PASS. PR #355 merged at 2faa7dca5ab14e2cb17d45208c197685253cbed2. Exact certified image promoted without rebuild; staging browser/API/health/startup checks passed; production unchanged. Historical failures below are retained.

> Continuation: owner superseded the stop-on-failure instruction and authorized iterative local/staging repair. The final local gate below now PASS; initial failure evidence is retained. Staging promotion pending. Production remains protected.

# C2B-35 - EMA20 and Bollinger overlays: blocked local browser gate

**Verdict: BLOCKED - OWNER DECISION REQUIRED.** C2B-36/C2B-37 not started. No phase PR, merge, registry publication or Azure mutation. Production was not modified. PUBLIC_RUNTIME_VERIFIED=False; public data APIs unchanged; advanced WP07 remains ON HOLD.

## Baseline and deployment isolation

Baseline main `52a096fd0232ca2650a076faeed38581dd6d357d` (PR #354). Owner authority C2B-35 through C2B-37 requires sequential local Linux acceptance, then independent PR/merge and staging-only promotion. Staging is `aiqr112wp05wus27f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2. Production is distinct: `aiqr112wp035ec325382770`, resource group `rg-aiq-r112-wp03-wcus-5ec325382770`, West Central US. GitHub workflows and repository hooks were empty; production source deployment had no repository or branch, scmType None and no DOCKER_ENABLE_CI setting. Both apps are digest-pinned. The CLI can generate a container webhook URL; its existence is not evidence that a trigger is enabled, and it was not invoked. Reference: https://learn.microsoft.com/en-us/azure/app-service/deploy-ci-cd-custom-container . No trigger was changed.

Captured staging rollback reference and production baseline: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:8cd59fb3760b5bf3d34284f4f035c41dda25fa899dd271b3eb0468c353815090`. Nonsecret runtime/storage/port settings and startup command captured in local before.json. Final readback compared both apps' selected configuration to that snapshot and found no changes (after.json). No rollback required because neither app was mutated.

## Bounded implementation

Reuse the existing independently tested C# TechnicalIndicatorCalculator, preserving aiq-ta-v1 and frozen OHLCV fingerprints. EMA20: alpha 2/21, SMA20 seed at sample 20, then recurrence; first 19 samples null. Bollinger: SMA20 midpoint, population standard deviation over 20 closes, upper/lower midpoint plus/minus twice sigma; first 19 samples null. Fixed 1e-12 scale and symmetric ties-to-even rounding remain unchanged. Flat prices yield zero band width; negative lower bands are valid. Each calculation uses the full window including 250 hidden warmup candles; visible rows are projected afterward. Recursively seeded EMA is not assumed identical across distinct windows.

The versioned immutable manifest is now aiq-chart-handoff-candidate-v2. Each visible row transports C# EMA and three Bollinger values alongside unchanged SMA/OHLCV. Strict signed decimal, ordering and midpoint checks reject malformed transport; Python only validates/projects supplied values. Native Plotly overlays and Technical Analysis checkboxes preserve existing candles, volume, synthetic title/hover and SMA. No indicator arithmetic runs in Python/JavaScript; no provider integration, new runtime dependency, governance change or public API activation.

Exact implementation paths (8):

- src/AIQuantTradingResearch.Worker/LocalSyntheticChart.cs
- src/AIQuantTradingResearch.Worker/ImmutableSyntheticChartHandoff.cs
- tests/AIQuantTradingResearch.Infrastructure.Tests/LocalSyntheticChartTests.cs
- python/presentation/local_synthetic_chart.py
- python/presentation/immutable_synthetic_chart.py
- python/presentation/test_immutable_synthetic_chart.py
- python/presentation/realtime_financial_visualization.py
- python/presentation/test_local_synthetic_chart.py

This report is the ninth changed path. Local implementation commits fdeae22 and 3fc29dc; original checkout/owner prompt and archived WP07 material preserved.

## Local Linux evidence

Actual repository Dockerfile built linux/amd64 immutable-handoff then synthetic-candidate targets. Candidate manifest `sha256:b519a450ab6fed2be2878d0bc59a288aeb2658433b84186c496f631f8be1711e`, config `sha256:34fa8ce0c5b209370852c84b948fae7755f2d5d459ae8ff423b9e1e4bdf01b54`. Bundle manifest SHA256 `2af51eff5f1d96eff63d1ad96905f6d4f1a4c112b08f3baffe0ad25c2fc66588`; 12 members totaling 1,226,114 bytes plus manifest. This is a local candidate, NOT certified for promotion.

- Existing independent canonical C# formula vectors: 38 passed, 0 skipped, exit 0. `dotnet vstest .../AIQuantTradingResearch.Application.Tests.dll --TestCaseFilter:FullyQualifiedName~TechnicalIndicatorCalculatorTests`.
- C# twelve-selection warmed projection/export tests: 14 passed, 0 skipped, exit 0. `dotnet vstest .../AIQuantTradingResearch.Infrastructure.Tests.dll --TestCaseFilter:FullyQualifiedName~ImmutableSyntheticChartHandoffTests|FullyQualifiedName~LocalSyntheticChartTests`.
- Actual candidate Python tests: 38 passed, exit 0. `python -B -m unittest test_local_synthetic_chart test_immutable_synthetic_chart test_public_preview_api -v`.
- Actual pinned root-owned assets: all twelve selections validated with aligned EMA/Bollinger values and existing OHLCV fingerprints. Existing semantic fault suite rejected wrong selection/count/time/OHLC/volume, null/wrong-scale SMA, warmup/version/provenance and extra members.
- Runtime screening: read-only root, UID/GID 1000 after fixed bootstrap, zero effective capabilities, no-new-privileges, 1 CPU, 512 MiB memory/swap, 64 PID cap, 32 MiB tmpfs, loopback ingress 127.0.0.1:8510. Observed about 96.57 MiB and 14 PIDs; OOMKilled=False. Screening is not proof of Azure capacity.

## Failed required browser gate

Real Edge/CDP native interactions against the actual container proved SMA20, EMA20 and Bollinger off/on/off on 30D and 1D. C#-supplied timestamp/value arrays matched for all four 1h ranges. However the next transition from 1h/90D to 4h/1D failed: the page displayed temporarily unavailable and no Plotly chart. The helper waited for the expected timestamps then raised `AssertionError: ('selection not rendered', '4h', '1D', None)`, exit 1. Read-only page inspection confirmed the selectors actually showed 4h/1D and the unavailable message; screenshot visually reviewed. No click or partial pass was substituted for acceptance.

Cause unresolved. The harness changes interval and range in quick succession; overlapping reruns and the existing fail-closed nonqueuing reader admission are a possibility, not established root cause. No cooldown, ownership, hash, deadline or admission check was weakened, and no post-failure retry or implementation correction was made. Existing Streamlit use_container_width deprecation warnings were observed; they are separate from the failed acceptance.

Authority says to stop on failed gates, so no PR/merge/promotion and no later phase. Proposed bounded follow-up for owner decision: diagnose this exact transition, distinguish a browser synchronization issue from an application rerun/admission defect, make only the necessary correction, then rerun the complete local gate before resuming staging-only lifecycle. Preserve the failed run as evidence.

Local reproducible helpers, Docker test harness, commands and outputs: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b35-79d00ec45d`. Relevant files: before.json, after.json, Dockerfile.tests, application-tests.txt, infrastructure-tests.txt, python-tests.txt, artifact-validation.txt, image-metadata.json, browser-acceptance.py, local-browser.txt, blocked-page.json and blocked-page.png. Local test resources stopped/removed; candidate/test images and source worktree retained for review. Recorded UTC: 2026-10-17T02:30:21.290750+00:00

## Continued local qualification PASS

Owner explicitly authorized continued local Docker and staging repair. Rapid superseded Streamlit reruns exposed transient reader contention: the reader still rejects concurrent admission immediately, now distinguishes internal ImmutableCandidateBusy, and UI retries only that category up to eight 250ms waits (two seconds waiting plus the unchanged two-second validation). Integrity failures are never retried; no hash, permission, admission or deadline guard was relaxed. Two tests prove successful contention retry, finite retry bound and no integrity retry. Same original rapid selection sequence now passes all twelve combinations without serializing/weakening it. The subsequent helper-only SVG innerText error was corrected to supported textContent inspection; full acceptance reran from a fresh browser tab and passed, including actual hover disclosure, zoom and pan.

Final local candidate digest `sha256:ae6dfaafba7c7b23544ffe062bcf23df803ff4c1f0e2cdea0718356319d14f7e`; bundle pin unchanged from this phase's v2 artifact. Tests now 38 C# formula + 14 C# projection/export + 40 Python passed, all exit 0. All twelve C# timestamp/indicator arrays exactly matched browser traces. SMA/EMA/Bollinger controls actually changed traces off/on/off on 1D and 30D. Final local HTTP root/health 200, data APIs 503 JSON, unknown API 404 JSON and inspected asset/admin boundaries passed. Runtime remains constrained/unprivileged/read-only. Final chart screenshot visually reviewed. Failed original run, helper error and later passing runs are retained separately.

Current proof files: python-tests-retry.txt, local-browser-final.txt, local-browser-proof.json, local-all-overlays.png, local-hover.png, local-routes.txt and image-metadata-retry.json. Source commit 56896ac plus this evidence-only update; production remains at original digest. Staging rollback captured in before.json; do not rebuild the certified image after this point.

## Staging acceptance PASS

Exact certified image `sha256:ae6dfaafba7c7b23544ffe062bcf23df803ff4c1f0e2cdea0718356319d14f7e`, config `sha256:1970d75c7f875f3b16c230b9ccc9d821339862d58f8270f2e15f0042a93f424f`. Registry and staging readback matched. Initial registry helper comparison mistook Docker Desktop Id (manifest) for OCI config; corrected to recorded build config before any deployment.

Promoted only to existing staging aiqr112wp05wus27f5eabb5, West US2 F1. Native staging browser passed all 12 selections with exact C# timestamp/value comparisons, SMA/EMA/Bollinger off/on/off on 1D/30D, synthetic label/hover, zoom/pan. System Health passed, root/health 200, public APIs 503 JSON, unknown API 404 JSON, inspected asset/admin boundaries passed. Corrected the startup-log reader to inspect Azure StartupLogs success files rather than only *_docker.log. Logs show a startup cancelled by the explicit authorized restart at 02:40:31Z, followed by new-image successful probe and Site started at 02:41:17Z; no unexplained restart loop. Retain original log archive locally, commit only sanitized findings.

Rollback image/config captured: sha256:8cd59fb3760b5bf3d34284f4f035c41dda25fa899dd271b3eb0468c353815090. No rollback needed. PowerShell 5.1 verification script automatically rolls back staging upon failed browser/API/digest checks. Production readback remained original 8cd59fb digest, empty command and scmType None; no production mutation. PUBLIC_RUNTIME_VERIFIED=False; WP07 ON HOLD. All local tests: 38 C# formula + 14 C# projection/export + 40 Python. Final evidence final.json, staging-browser-proof.json, staging-routes.txt, health screenshot and staging-startup-summary.json. Only now may C2B-36 begin.

# C2B-36 - C# Wilder RSI and MACD lower panels

**Local Linux qualification PASS; protected PR merge and staging promotion pending.** Production unchanged; PUBLIC_RUNTIME_VERIFIED=False; public APIs remain unavailable; WP07 ON HOLD.

## Preflight and rollback

Baseline PR355 merge 2faa7dca5ab14e2cb17d45208c197685253cbed2; phase35 staging fully passed before this phase. Staging aiqr112wp05wus27f5eabb5, West US2 F1, distinct from production aiqr112wp035ec325382770, West Central US. Repeated checks: no workflows/repository hooks/source deployment, no DOCKER_ENABLE_CI, production pinned at original8cd59fb. Identities, startup commands and nonsecret settings captured in before.json. Staging rollback image/config: sha256:ae6dfaafba7c7b23544ffe062bcf23df803ff4c1f0e2cdea0718356319d14f7e. No new resource/production mutation.

## Deterministic formulas and boundaries

Reuse unchanged aiq-ta-v1 C# calculator. RSI14 averages first14 gains/losses (first valid sample15), then Wilder(13*previous+current)/14. RSI=100*gain/(gain+loss): flat50, gains-only100, losses-only0. MACD fast12/slow26 use SMA seeds and alpha2/(period+1); line first valid26; signal EMA9 seeds from first9 MACD values at sample34; histogram=line-signal. Fixed1e-12 ties-to-even seed/recurrence arithmetic; null alignment retained. Compute full250-warmup window, project visible timestamps only. Recursive values need not match across distinct range windows.

Internal manifest candidate-v3 and new pin bind signed fields. OHLCV fingerprints and public contracts unchanged. Python validates decimal/bounds/histogram consistency, never computes indicators. Separate conditional Plotly lower panels: RSI fixed0-100; MACD line/signal/histogram on its own axis. Shared UTC timestamps; all prior overlays, volume and controls preserved. Previous bounded busy-only retry retained.

## Exact changed paths

- docs/roadmap/release-1.14/prompters/release-1.14-c2b-35-ema-bollinger-evidence.md
- docs/roadmap/release-1.14/prompters/release-1.14-c2b-36-rsi-macd-evidence.md
- python/presentation/immutable_synthetic_chart.py
- python/presentation/local_synthetic_chart.py
- python/presentation/realtime_financial_visualization.py
- python/presentation/test_immutable_synthetic_chart.py
- python/presentation/test_local_synthetic_chart.py
- src/AIQuantTradingResearch.Worker/ImmutableSyntheticChartHandoff.cs
- src/AIQuantTradingResearch.Worker/LocalSyntheticChart.cs
- tests/AIQuantTradingResearch.Infrastructure.Tests/LocalSyntheticChartTests.cs

Phase35 document changes record its completed post-merge staging acceptance only.

## Actual image and qualification

Certified linux/amd64 image `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`, OCI config `sha256:0980cab206741d9c7805a0e52945b3030d4c27a31209c33b2c029226463b8e07`. Bundle pin1249324488a22b8b0f743fd2d6f0cc751b751d074de030414a3f47ba259b4f5c. Twelve data members total1,681,190 bytes; largest911,396, existing caps unchanged. Actual repository synthetic-candidate built; C# artifact generated offline. Existing restored dependency test image reused; no package changes. No rebuild after certification.

- 38 independent C# formula tests PASS, no skips, exit0: dotnet vstest Application.Tests.dll filter TechnicalIndicatorCalculatorTests.
- 14 C# all-selection projection/export tests PASS, no skips, exit0: dotnet vstest Infrastructure.Tests.dll filter ImmutableSyntheticChartHandoffTests|LocalSyntheticChartTests.
- 41 Python tests PASS, exit0: python -B -m unittest test_local_synthetic_chart test_immutable_synthetic_chart test_public_preview_api -v.
- All12 actual pinned asset selections validate aligned RSI/MACD and prior indicators; semantic fault suite additionally rejects RSI101, inconsistent histogram and null MACD.
- Native Edge against actual Linux candidate: SMA/EMA/Bollinger/RSI/MACD off/on/off on1D/30D; all12 selections with exact C# arrays; RSI0-100 and separate MACD shared lower axis; labels, hover, zoom/pan PASS. All-panel screenshot visually reviewed for usability. Null-alignment and panel tests passed.
- Local root/health200, data APIs503 JSON, unknown API404 JSON and inspected asset/admin boundaries PASS.
- Read-only, UID1000 after fixed bootstrap, zero effective capabilities, 1CPU/512MiB/64PID/32MiB tmpfs; observed95.19MiB/19PIDs, no OOM. Screening is not Azure capacity certification.

Existing Streamlit use_container_width warning remains, no functional failure. Report-generation helper initially assumed UTF8 for a PowerShell5.1 UTF16 log; corrected BOM detection, with source/image unaffected. No test was weakened.

## Staging-only plan and retained evidence

After protected merge, publish exact certified image to existing GHCR, verify manifest/config, deploy only staging, run real browser/API/health/startup checks and rollback on a failed staging gate before repair. Never production. Proceed C2B37 only after full phase36 staging success.

Evidence directory: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b36-4a803f2f77`. before.json, image-metadata.json, application-tests.txt, infrastructure-tests.txt, python-tests.txt, artifact-validation.txt, local-browser.txt/proof.json, local-all-overlays.png, local-hover.png and local-routes.txt. Original checkout/owner prompt and archived WP07 preserved.

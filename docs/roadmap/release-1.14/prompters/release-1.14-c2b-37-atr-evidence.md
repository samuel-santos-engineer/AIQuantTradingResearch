# C2B-37 ? ATR14 staging qualification evidence

## Authority and scope

Owner-authorized third sequential synthetic indicator PR, following accepted PR355/356. Existing staging only: aiqr112wp05wus27f5eabb5, rg-aiq-r112-wp05-wus2-7f5eabb5, West US2/F1. No production mutation, new dependencies/services, provider access or frozen generator/calculator changes. PUBLIC_RUNTIME_VERIFIED=False; raw public data APIs disabled; advanced WP07 ON HOLD.

## Formula and alignment

C# aiq-atr-wilder-v1 uses true range TR0=high-low, then max(high-low,abs(high-previousClose),abs(low-previousClose)). Seed arithmetic mean of first14 TRs including TR0: first valid sample14, first13 null. Wilder recurrence (13*previousATR+TR)/14. BigInteger fixed1e12, symmetric ties-to-even at seed/each recurrence. Bounded2410 inputs and canonical immutable OHLCV/grid validation; public entry validates synthetic full-window provenance first. No lookahead. Calculate full250 warmup plus visible rows and publish visible rows only; distinct range windows need not have identical recursive values.

Existing aiq-ta-v1 and OHLCV fingerprints remain unchanged. Separate ATR version in candidate-v4 internal handoff. Python validates finite canonical nonnegative decimal values, never computes authoritative indicators. Separate conditional ATR volatility panel preserves RSI/MACD/volume/overlays. Health uses the same existing bounded busy-only acquisition as chart; integrity failures still fail closed without retry.

Independent expectations authored before production execution using CPython Decimal precision90 / ROUND_HALF_EVEN1e-12: gap seed145/14=10.357142857143, next10.403061224490; expanding high-low seed15, next16.071428571429, then17.209183673470. Explicit .5/1.5 seed and2.5/3.5 recurrence midpoint ties; constant/zero, bounded large inputs, null alignment, missing/gapped/reordered/precision/provenance/cancellation rejection.

## Local evidence

Evidence directory: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b37-eab5ddc968`.

49 focused Application tests (38 prior+11 ATR),14 all-selection Infrastructure projection/export tests PASS, no skips, actual exit0. Linux existing restored test image, --network none, --no-restore.43 focused Python tests PASS, including health contention regression. All12 actual image assets validate, nine malformed ATR/analytics/metadata mutations reject. Bundle1,784,714 bytes total, largest967,484 under unchanged1MiB/member and4MiB total caps; pin0c7b3a415d551dde753382c741f581d8aab4683880228bcfd904e53a0823cf84.

Candidate digest sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07; OCI config sha256:50358fb5caf32a149c4129261b864e441a72d01bfce4ee748fa6013a5d550b04. Actual Docker synthetic-candidate linux/amd64; offline C# artifact generated with network disabled. Final image build actual exit0 captured. No image rebuild after completed local certification.

Resource constraints: read-only root,32MiB tmpfs,1CPU/512MiB/64PIDs, no-new-privileges, fixed bootstrap then actual StreamlitUID/GID1000, CapEff0. Docker exec defaults root from image startup configuration and is not application identity. No external provider or runtime C# process. Local resource screening is not Azure capacity certification.

Final native Edge acceptance PASS: all6 controls off/on/off on1D/30D, all12 selections with exact C# arrays/timestamps, ATR separate nonnegative volatility axis, RSI0-100, MACD line/signal/histogram, original candle/volume/hover/modebar/zoom/pan and synthetic/UTC labels. Screenshot visually reviewed. Fresh-session System Health PASS. Root/liveness200, public data503 DATASET_NOT_READY JSON, unknown API404 JSON, reserved admin/asset access boundaries PASS. Final logs: local-browser.txt/proof.json, local-all-overlays.png, local-hover.png, local-health-final.txt, local-routes-final.txt, python-tests-final.txt and artifact-validation-final.txt. Two initial browser helper targeting failures used a small default viewport; supported desktop viewport1400x2600 resolved control targeting. Third attempt demonstrated all toggles/selections/hover/zoom/pan but screenshot timed out; screenshot now foregrounds its owned page with bounded50sec CDP deadline. Failures retained. Concurrent health check exposed busy-reader reporting; changed health to existing bounded retry, added test and rebuilt before requalification. No failed functional assertion waived. Existing Streamlit use_container_width deprecation and four existing Infrastructure analyzer warnings remain.

## Deployment isolation and rollback

No GitHub workflows/hooks or App Service source deployment; scmTypeNone, empty startup overrides. Main protections enforced normally; no bypass. Existing production remains8cd59fb3760b5bf3d34284f4f035c41dda25fa899dd271b3eb0468c353815090. Before mutation capture staging rollback98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed and nonsecret configuration in before.json. Publish only exact locally accepted digest to existing GHCR after normal protected merge. Staging browser/API/health/startup/digest required; rollback before repair on a failed gate. No new recurring cost.

## Exact changed paths

- docs/roadmap/release-1.14/prompters/release-1.14-c2b-36-rsi-macd-evidence.md
- docs/roadmap/release-1.14/prompters/release-1.14-c2b-37-atr-evidence.md
- python/presentation/immutable_synthetic_chart.py
- python/presentation/local_synthetic_chart.py
- python/presentation/realtime_financial_visualization.py
- python/presentation/test_immutable_synthetic_chart.py
- python/presentation/test_local_synthetic_chart.py
- src/AIQuantTradingResearch.Application/MarketData/WilderAverageTrueRangeCalculator.cs
- src/AIQuantTradingResearch.Worker/ImmutableSyntheticChartHandoff.cs
- src/AIQuantTradingResearch.Worker/LocalSyntheticChart.cs
- tests/AIQuantTradingResearch.Application.Tests/WilderAverageTrueRangeCalculatorTests.cs
- tests/AIQuantTradingResearch.Infrastructure.Tests/LocalSyntheticChartTests.cs

Phase36 document records its completed post-merge staging evidence. Owner-untracked authority in primary checkout and archived WP07 preserved. Public activation not implied.

# C2B-25 — immutable handoff candidate evidence

2026-10-16. Local candidate implemented and tested; Linux/container qualification **BLOCKED**, public promotion/activation **NO-GO**. Not a production-ready release. Contract: [candidate format and bounds](release-1.14-chart-mvp-c2b-25-immutable-handoff-contract.md).

## Baseline, scope and provenance

Fetched origin/main: `33b941c28b0158810b4f406db965bf83ff92244a`, PR #345 merged. Read-only live check confirmed planning PR #346 OPEN at `79b175e666f17d1f7f70776516246f81e6cbc9e6`; no merge or planning-file cherry-pick. New focused C2B-25 branch from main. Thirteen incoming owner documents hash-preserved, excluded. Protected WP07/archive and unrelated processes untouched.

MIT repository license inspected; no provider input or third-party dataset acquired. Candidate data is generated only under C2B-25 offline authority, retained in owned temporary storage, not committed or promoted. Source-code licensing does not override governance export/promotion rules. The reviewed generator, calculator, E4 oracle, local HTTP adapter and local HTTP source are unchanged. New producer calls existing LocalSyntheticChart.CreatePayloadAsync, which validates canonical synthetic provenance and uses TechnicalIndicatorCalculator.CalculatePublic before Skip(250). No Python/JavaScript indicator calculation.

Exact changed paths:

- `.dockerignore` — exclude unpromoted candidate directory from image build contexts.
- `.gitignore` — exclude generated candidate data from normal staging.
- `src/AIQuantTradingResearch.Worker/Program.cs` — explicit offline command dispatch before normal/private DI; no deployed startup change.
- `src/AIQuantTradingResearch.Worker/ImmutableSyntheticChartHandoff.cs` — gated offline producer.
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ImmutableSyntheticChartHandoffTests.cs` — deterministic twelve-member producer and C# value tests.
- `python/presentation/immutable_synthetic_chart.py` — fixed-path bounded reader and candidate-only gate.
- `python/presentation/test_immutable_synthetic_chart.py` — unit negatives and explicit real-artifact offline qualification command.
- `python/presentation/realtime_financial_visualization.py` — opt-in immutable acquisition; existing Plotly layout/popover untouched.
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-25-immutable-handoff-contract.md`.
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-25-immutable-handoff-evidence.md` (this report).

## Artifact bytes and trust

Manifest: 1454 bytes, SHA-256 `007b2c8a6ccec19ec54976b4baf735812b648d79294e6465df884c68537d3069`. Reader pins this digest outside the artifact tree. Whole set: **656176 bytes**, exactly 13 files. Candidate hashes establish reproducibility/integrity of these bytes; they are not independent production numerical qualification or a promotion signature.

| File | Visible rows | Bytes | SHA-256 |
|---|---:|---:|---|
| 1h-1D.json | 24 | 4463 | 1678f42a584e17b0bd6ee9f4d66958ab1d8e8e09d02b80a5d38b016349cc749e |
| 1h-7D.json | 168 | 27950 | f3adcf98555e9b4461c267afb9a51d7747288d20cd35320f9d186b49cfa47d5b |
| 1h-30D.json | 720 | 117984 | 5373a8b1525a14185e81f59fdc71e7546f877825a984810ddca099059335aa53 |
| 1h-90D.json | 2160 | 352844 | 3699a1a7a619257e12c5c30f7010f2ab3810c2d18f516c14b9ec7e437d3971e2 |
| 4h-1D.json | 6 | 1535 | b7b3c5eaa3fae8a349736ef993bc2cea876bbefdbf379b67624b76c4a0aa025d |
| 4h-7D.json | 42 | 7439 | ba03fe99008a3f60643301bf09a32593a1c4864c3ed7dcaa84a4e664b381fe23 |
| 4h-30D.json | 180 | 30072 | 67c1c105631a57fde57e226ce7ccb7e24e50bd6a26747001bb815ce71a4aab32 |
| 4h-90D.json | 540 | 89109 | a064e9a21a50500d2a340eda685856ff7b680f860c07a377b75694b7974997aa |
| 1d-1D.json | 1 | 716 | 22e702dc5ebde1cb19c699869e1196fc94aaacd2709ca6678742539a66710baa |
| 1d-7D.json | 7 | 1706 | f9cb186409b8cb625a74f50751772766a053a8854b71515d87e791d2f9d0e33f |
| 1d-30D.json | 30 | 5502 | bd1826063754f399939c4737460f12384cdf14e20c3d41da91d9bf0b181660af |
| 1d-90D.json | 90 | 15402 | 9130b027a3d4c46900ca36263e84ddf69a404fad0f03fe31f2ef04ad8bfc3ba5 |

Fresh final producer output in a scrubbed environment matched every initial file hash (zero mismatches). A provider-setting-name environment was explicitly denied, exit1, with no output directory created. The first local trial inherited a legacy provider setting name; no provider call or credential value logging occurred. The new producer/reader gates were tightened, and browser/producer checks repeated with child environments scrubbed. Parent setting values were restored after owned child launch.

## Actual commands and results

PowerShell 5.1, repository root. `$run` below denotes the owned temporary working directory; actual run was `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b25-e388798d520f4f839017003e8a96400a`.

```powershell
dotnet build tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj -c Release --no-restore --disable-build-servers -p:AutoSignTestBinaries=false "-p:OutDir=$run\build\" -m:1
dotnet vstest "$run\build\AIQuantTradingResearch.Infrastructure.Tests.dll" '/TestCaseFilter:FullyQualifiedName~ImmutableSyntheticChartHandoffTests|FullyQualifiedName~LocalSyntheticChartTests'
# Dedicated scrubbed Development environment, AIQ_IMMUTABLE_EXPORT=1:
dotnet "$run\build\AIQuantTradingResearch.Worker.dll" --write-immutable-chart-candidate "$run\data-clean"
.\.venv\Scripts\python.exe -B python/presentation/test_immutable_synthetic_chart.py --candidate "$run\data"
```

Final build: exit0, 0 errors, five existing E4 warnings, 7.51 seconds. A new CA1861 warning during an intermediate build was corrected before final validation. No package install or restore. Focused .NET: **14 passed**, 0 failed/skipped, about 2 seconds, including all twelve existing C# selection/warmup/SMA checks and new producer reproducibility/member-byte checks. Tests re-execute qualified C# behavior; they do not constitute a separately authored mathematical oracle.

From `python/presentation`:

```powershell
..\..\.venv\Scripts\python.exe -B -m unittest test_immutable_synthetic_chart test_local_synthetic_chart test_public_preview_shell test_public_preview_api.PublicApiTests.test_six_exact_routes_and_liveness_readiness_are_distinct -v
```

**19 passed**, exit0, 0.816 seconds. Existing bare-mode Streamlit missing-ScriptRunContext warnings. Twelve new reader tests cover disabled/missing artifacts, gate/environment exclusions, manifest/size/inventory/traversal/duplicate/UTF-8/nonfinite/extra-member errors, anchor/member tampering, no-wait concurrency, cooperative budget and link/reparse rejection. Structure-only fixtures mock the payload parser only for filesystem/manifest tests; the real validator rejection test does not. Actual Linux permission branch was not run on Windows; no real Windows symlink creation was claimed.

The explicit `--candidate` integration command used real generated C# files and no mocked parser: all twelve selections passed with exact visible counts and aligned SMA. Twelve rehashed/test-repinned semantic faults rejected: wrong interval, missing row, bad timestamp, negative price, invalid OHLC, negative volume, null SMA, wrong scale, wrong warmup, unknown calculation version, wrong provenance and extra field. Test-only repinning intentionally reaches semantic checks; runtime acquisition accepts no caller-supplied path/pin.

Under Python tracemalloc, twelve complete-set reads took 8.005 seconds total; individual calls 652–690ms, traced peak 9008035 bytes. These are instrumented Windows observations, not RSS, F1 performance, hard execution limits or concurrency capacity. No measured F1 budget is invented. No broad suite/E4 requalification.

## Browser smoke — immutable files, no C# service

Installed Edge headless, existing Streamlit 1.61.1/Plotly 7.1.0, loopback8502; provider/private setting names scrubbed for owned child processes. No listener on8766; no request-time C# process, generator, provider or network data acquisition. Reader does not use subprocess/HTTP. Browser automation used CDP native keyboard/mouse and browser focus commands, with read-only trace/DOM inspection; no injected UI behavior, JavaScript calculations, DOM workaround or library patch. Focusing the accessible checkbox with the browser's native focus command and pressing Space reliably changed the actual trace state.

| Browser criterion | 1D | 30D |
|---|---|---|
| Candles / volume / SMA rows | 24 / 24 / 24 | 720 / 720 / 720 |
| SMA off -> on -> off | false -> true -> false; gold line appears/disappears | false -> true -> false; gold line appears/disappears |
| Existing native popover / modebar | PASS | PASS |
| Zoom and native pan | PASS: X changed from full day to 12h then panned; Y changed | PASS: X changed from 30d to 15d then panned; Y changed |
| Hover | Sep30 10:00 OHLC displayed | Sep14 04:00 OHLC displayed |

Range selector changed 30D ->1D and back. Interval selector changed 1h ->4h (six 1D rows) and back. White existing interface, simulation disclosure and original candlestick/bar composition preserved. UTC input/time grid was validated against the frozen contract; no timezone conversion introduced. The owner-accepted zoom reset on toggle remains documented, not fixed or requalified as retention.

Screenshots captured and inspected locally under the run directory: `30D-off.png`, `30D-on.png`, `30D-off-again.png`, `1D-off.png`, `1D-on.png`, `1D-off-again.png`, `1D-controls.png`, `30D-controls.png`. On-state screenshots visibly show the gold line; trace state, not a click alone, establishes success. Screenshots are local evidence, not committed/uploaded market-data exports.

Actual candidate-mode `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` each returned **503 DATASET_NOT_READY**, spaced700ms apart. Default-disabled shell/API unit tests also passed. All owned UI/browser process trees stopped, ports8502/9223/8766 released. Only the hash-verified owned repository copy of 13 candidate files was removed; temporary source evidence retained. No unrelated process stopped.

## Linux/container gap and readiness

`docker context show` returned `desktop-linux`; `docker info --format '{{.ServerVersion}}'` failed: Docker Desktop Linux engine named pipe not found. No image pull/build/run, registry push or Azure mutation. Thus Linux startup, effective UID, mounted/ancestor permissions, actual read-only image assets, synthetic environment and Linux real-link behavior remain unverified. Platform-neutral Python reader code is a candidate, not proof of Linux F1 operation.

Current Dockerfile chowns `/app` to the runtime principal. Candidate checks reject links and Linux group/other write permissions, but do not establish production non-writability by the public principal. Read-only code/anchor/artifact ownership and ancestor/mount policy require separate qualified image design. No candidate data enters the existing Docker context. Environment gates reject Azure entirely. Default public startup and readiness are unchanged.

Remaining promotion requirements: independently reviewed complete derived numerical/byte parity; accepted trust anchor/release identity and operator/promotion/revocation provenance; read-only ownership/mounts; coherent release and rollback/revocation semantics; complete-read/send/cancellation admission; durable abuse/health accounting; measured F1 CPU/RSS/latency and retained-session/concurrency limits; explicit applicable G-B0–G-B7 mapping. The semaphore is process-local, not durable/global abuse protection. Byte/deadline caps are candidate limits, not measured safe F1 capacity. No blanket WP07 waiver.

## Local reproduction and separate site decisions

Use dedicated Windows PowerShell5.1 terminals, no Azure WEBSITE markers. First scrub inherited provider/private settings by **name only** from that dedicated terminal, never from Azure configuration:

```powershell
if (@(Get-ChildItem Env: | Where-Object { $_.Name -like 'WEBSITE*' }).Count) { throw 'Azure environment prohibited' }
$privateNames = @(Get-ChildItem Env: | Where-Object { $_.Name -match '^(TWELVEDATA|VIKE|Persistence__|Worker__Replay__|Visualization__)' } | Select-Object -ExpandProperty Name)
foreach ($name in $privateNames) { Remove-Item -LiteralPath ('Env:' + $name) }
$env:DOTNET_ENVIRONMENT = 'Development'
$env:AIQ_IMMUTABLE_EXPORT = '1'
$run = Join-Path $env:TEMP ('aiq-candidate-' + [guid]::NewGuid().ToString('N'))
# Build using the command above with this $run, then:
dotnet "$run\build\AIQuantTradingResearch.Worker.dll" --write-immutable-chart-candidate "$run\data"
if ($LASTEXITCODE -ne 0) { throw 'Candidate export failed' }
$destination = '.\python\presentation\_immutable_chart_candidate'
if (Test-Path -LiteralPath $destination) { throw 'Existing candidate directory; preserve and review' }
Copy-Item -LiteralPath "$run\data" -Destination $destination -Recurse
$env:AIQ_IMMUTABLE_CHART_CANDIDATE = '1'
Remove-Item Env:AIQ_LOCAL_CHART -ErrorAction SilentlyContinue
.\.venv\Scripts\python.exe -B -m streamlit run python/presentation/public_preview_app.py --server.address 127.0.0.1 --server.port 8502 --server.headless true --browser.gatherUsageStats false --secrets.files '[]'
```

Run from repo root; use installed offline dependencies. Stop if port8502 is owned by another process; do not terminate it. No C# source server is needed after export. Open `http://127.0.0.1:8502/`; Ctrl+C stops the foreground UI. Generated files are ignored and explicitly excluded from image builds; do not force-add them.

| Site | Recommended owner decision | Future rollback prerequisite |
|---|---|---|
| `aiqr112wp035ec325382770` | No public rollout yet. Authorize bounded local Linux/container qualification when Docker is available, then separately review independently qualified artifact/image promotion and this site's publication role. | Under future authority, capture this site's then-current digest and safe settings; restore that exact unavailable image/config if qualification/activation fails; verify public APIs unavailable. |
| `aiqr112wp05wus27f5eabb5` | No public rollout yet. Independently decide whether this site should also host the chart; WP03 qualification does not qualify this resource. Same promotion/readiness gates apply. | Independently capture/restore this site's own digest/config; do not copy provider settings or credentials across sites. Verify unavailable routes and health separately. |

C2B-24's ARM inventory is historical evidence for these decisions, not a fresh C2B-25 deployment check. Earlier public HTTPS probes timed out; do not infer public health from ARM Running. No Azure/Actions/slot/configuration/RBAC operation in this phase. Source PR is reviewable; artifacts remain unpromoted; leave PR OPEN/UNMERGED.

`PUBLIC_RUNTIME_VERIFIED=False`; public activation/deployment **NO-GO**; advanced WP07 **ON HOLD**.

# C2B-27 — approved local Linux browser acceptance

2026-10-16 continuation after owner explicitly approved the temporary test relay. **LOCAL LINUX BROWSER ACCEPTED / AZURE NOT YET AUTHORIZED.** This supersedes only the browser-connectivity blocker in the earlier [C2B-27 report](release-1.14-chart-mvp-c2b-27-linux-browser-and-azure-readiness-evidence.md). Its per-site publication checklist, observed Azure API route mismatch and other production qualification gaps remain effective.

## Scope and refs

Before execution, GitHub confirmed PR #346 OPEN at `f59a4f1ecde1124954ae72af82f5ab5c8595526e`; PR #347 OPEN at unchanged `8240d0878dea335c08ad3216600fc7e545446140`. Common base remains `33b941c28b0158810b4f406db965bf83ff92244a`. Exact reviewed candidate source and existing offline C# binaries reused. No implementation, tests, Dockerfile, entrypoint, dataset algorithms, dependencies, readiness or Azure settings edited.

Only this new document is added to #346, making twenty Markdown paths relative to base. Original fourteen owner records remain byte-identical and their inventory unchanged. PR #347 remains the same ten-path implementation candidate; no new commit. Both PRs OPEN/UNMERGED. Final documentation head is recorded in the PR description.

## Approved method and bounded execution

Owner approval resolved the prior automatic approval-review rejection; no relay was attempted before that approval. The approved harness used existing host Edge/CDP, a temporary standard-library TCP relay and an owned Docker network. Application server stayed on container127.0.0.1:8502; relay forwarded bytes only, with no application/DOM/Streamlit patch or indicator arithmetic. No browser/package download or new production infrastructure.

An initial `--internal` Docker network retained the requested HostConfig binding but Docker did not actually publish it (`docker port` empty). Edge showed connection refused; empty-chart automation errors are recorded as harness failures, not application/toggle evidence. That container was stopped after about127s without a working host-published service.

One bounded retry used an owned private bridge with `com.docker.network.bridge.enable_ip_masquerade=false` and `com.docker.network.bridge.host_binding_ipv4=127.0.0.1`. Explicit Docker publication was **127.0.0.1:8504:8504**; `docker port` confirmed it. Relay bound only its private container address and forwarded to application loopback. No host wildcard/LAN binding, host-network mode, gate relaxation or Azure operation. Disabling masquerading is an observed network option, not an exhaustive egress-security qualification.

Retry capped at60s (shorter than the approved180s); actual host-exposed container lifetime about55.7s before explicit stop. Relay caps: eight concurrent connections,32MiB per connection, three-second socket operations,60s idle; outer process timeout70s. Workloads UID/GID1000, readonly root and artifact mount, oneCPU,512MiB memory/equal swap cap,64PIDs,32MiB noexec/nosuid tmpfs, capabilities dropped/no-new-privileges, env-i synthetic environment. Only the temporary relay and Python/Streamlit ran during UI testing; no request-time C# service or provider.

An owned offline C# producer ran separately with network none and60s timeout; exit0. Manifest1454 bytes and SHA256 `007b2c8a6ccec19ec54976b4baf735812b648d79294e6465df884c68537d3069`, whole set656176 bytes. This is the same content-bound twelve-member candidate as C2B-25/26/27. Dataset remained a readonly Docker volume subpath at the fixed reader path, outside Git and image build context. No promotion/export into a production image.

## Actual browser results

Installed host Edge rendered the application served by the **Linux container**, not a Windows Streamlit process. CDP used native keyboard/mouse and native browser focus commands plus read-only inspection; no JavaScript behavior injection or DOM workaround. Screenshots were captured and visually inspected, alongside resulting Plotly trace states. Initial navigation was repeated after the unsuccessful internal-network attempt; only the successful retry counts below.

| Criterion | 1D | 30D |
|---|---|---|
| Visible candles / SMA points | 24 / 24 | 720 / 720 |
| SMA off -> on -> off | false -> true -> false; gold line appeared/disappeared | false -> true -> false; gold line appeared/disappeared |
| Existing chart composition | Candlestick, volume bar and SMA scatter traces present | Same traces; volume panel visible in controls screenshot |
| Native popover, selectors, toolbar | PASS | PASS |
| Native zoom/pan | X full-day ->12h ->panned; Y changed | X30d ->15d ->panned; Y changed |
| Actual hover | Sep30 10:00 OHLC tooltip | Sep14 04:00 OHLC tooltip |
| Rendered SMA value comparison | All24 display floats matched artifact conversion exactly | All720 display floats matched artifact conversion exactly |

Native range selection changed30D ->1D and back. Interval selection changed1h ->4h (six1D candles), then back1h. Original white appearance, synthetic label/disclosure, volume composition and Plotly modebar retained. The 1D screenshot viewport crops the lower volume panel; bar-trace presence was verified in the actual rendered Plotly state. No volume redesign or provider data. The owner-accepted zoom reset remains documented; no retention fix or retention-pass claim.

SMA provenance check: captured actual browser trace arrays for24 and720 points, then compared each number inside the container to `float(row['sma20'])` from its readonly corresponding C# artifact. Both arrays matched exactly, with no tolerance. This checks existing display conversion/provenance, not a newly independent mathematical oracle or Python SMA recomputation. All250 warmup rows remain calculation-only; visible counts unchanged.

Evidence directory: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b27-browser-e084b72c333b4823a71ddbfae563f235`. Screenshots: `30D-off.png`, `30D-on.png`, `30D-off-again.png`, `1D-off.png`, `1D-on.png`, `1D-off-again.png`, `1D-controls.png`, `30D-controls.png`. The initial failed connection screenshot was overwritten by the successful30D-off capture; failure remains in command output/logs. Temporary `line-observations.json` holds only the observed synthetic display arrays. No screenshots/data committed or uploaded.

## Actual ingress and resource observations

Via the same host127.0.0.1:8504 path used by Edge, requests spaced700ms: health200; dataset/candles/indicators each **503 DATASET_NOT_READY**; admin404. Artifact/local-source paths returned only10951-byte Streamlit SPA HTML, no manifest schema/data. HostConfig showed only127.0.0.1 publication; read-only root and UID1000. Source/private paths were not served. The C2B-27 network-none missing/tampered/unreadable reader denials remain prior evidence; their rendered UI errors were not retested in this browser continuation.

Interactive-container cgroup memory.peak170008576 bytes, CPU usage4860382 microseconds,17 throttled periods/927708 microseconds at sample. Includes relay/UI/test activity, excludes host Edge resource use; not a p95/load benchmark, peakRSS or Azure F1 capacity. No broad unit/.NET/E4 rerun; browser and display-array comparisons are the new checks. Successful producer exit0; both explicitly stopped UI containers exit143, no OOM. No guardian/crash-safety qualification inferred.

## Reproduction and cleanup

Windows PowerShell5.1 orchestration, existing C2B-26 temporary qualification Dockerfile and exact #347 source. Initial internal-network attempt failed as described; successful test differed only in the temporary network and60s harness deadline. Key successful commands, after owned image/volume initialization from C2B-26 procedure:

```powershell
docker network create --label aiq.owner=c2b27-browser --opt com.docker.network.bridge.enable_ip_masquerade=false --opt com.docker.network.bridge.host_binding_ipv4=127.0.0.1 aiq-c2b27-browser-loopback
# @bounds: readonly root, oneCPU,512MiB,64PIDs,cap-drop ALL,no-new-privileges,tmpfs; @mounts: readonly fixed candidate + owned proof scripts.
docker run -d --name aiq-c2b27-browser-ui-retry --network aiq-c2b27-browser-loopback --publish 127.0.0.1:8504:8504 @bounds @mounts aiq-c2b27-browser:8240d08 env PYTHONPATH=/qualification/presentation DOTNET_ENVIRONMENT=Development AIQ_IMMUTABLE_CHART_CANDIDATE=1 timeout 70 python -u -B /proof/browser_relay.py
docker port aiq-c2b27-browser-ui-retry
# Owned Edge headless profile, CDP127.0.0.1:9224; native browser scripts:
.\.venv\Scripts\python.exe -u -B "$run\browser-clean.py"
.\.venv\Scripts\python.exe -u -B "$run\browser-controls.py"
.\.venv\Scripts\python.exe -u -B "$run\capture-lines.py"
Get-Content "$run\line-observations.json" -Raw | docker exec -i aiq-c2b27-browser-ui-retry /usr/bin/env -i PATH=/opt/venv/bin:/usr/bin:/bin timeout 5 python -B /proof/compare_browser.py
```

Temporary scripts and logs retained in the owned evidence directory; no executable script added to either PR. An intermediate PowerShell quoting error in a script-edit command was corrected using Python; no product changes. All owned Edge processes, three containers, two networks, scratch volume and temporary image tag removed. Ports8504/9224 released; existing images, unrelated processes and protected archives untouched. No broad cache prune.

## Remaining Azure decision

**Local Linux browser acceptance is now supported. Azure publication remains unauthorized and unqualified.** The earlier fresh Azure observation is not repaired or re-probed here: both sites returned HTML200 on data API paths rather than required503 JSON. The minimal image-bundled delivery, root-owned trust/gate changes needing new authority, independent promotion proof, F1/durable-admission qualification and separate per-site rollback/deployment/exposure checklists remain in the earlier report. No future rollout is implied by this local pass.

`PUBLIC_RUNTIME_VERIFIED=False`; public activation/deployment **NO-GO**; advanced WP07 **ON HOLD**. Both PRs remain **OPEN/UNMERGED**.

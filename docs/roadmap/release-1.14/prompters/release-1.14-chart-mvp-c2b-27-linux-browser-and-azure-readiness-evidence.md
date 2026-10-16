# C2B-27 — Linux browser and Azure publication readiness

2026-10-16. **INCOMPLETE / BLOCKED.** No Linux real-browser acceptance, public promotion, merge, registry push, Azure configuration or deployment. `PUBLIC_RUNTIME_VERIFIED=False`; public activation/deployment NO-GO; advanced WP07 ON HOLD.

## Verified baseline and changes

Live GitHub: PR #345 MERGED at `33b941c28b0158810b4f406db965bf83ff92244a`; PR #346 OPEN at initial head `0998f001afd5ffe505886695a0c6b7f686abe435`; PR #347 OPEN at unchanged head `8240d0878dea335c08ad3216600fc7e545446140`. Both bases and their merge-base equal the #345 merge. No branch overwrite, rebase, merge or source import between PRs.

Qualification context used `git archive` of exact #347 head. Its production code and existing candidate tests were not changed. Portable C# binaries reused the C2B-26 offline build; production C# source is identical between the original #347 head and the two-line Python-test correction. No rebuild/restore or package download this phase.

Only two new paths on #346: this evidence document and the original `release-1.14-chart-mvp-c2b-27-linux-browser-and-azure-readiness-codex.md` authority record, preserved without rewriting. Existing fourteen owner records, their hash manifest and all prior #346 documents remain unchanged. #346 stays documentation-only (nineteen Markdown paths total relative to base). #347 stays at its ten-path inventory recorded in C2B-26, with no C2B-27 delta. Final #346 SHA and original authority hash are recorded in the PR description. No generated dataset or temporary test scripts committed.

## Local Linux work actually executed

Docker Linux29.7.2; existing runtime image `sha256:56a9bab04e7b03441f6b811fd6afbc67b18203c7e0ebf0540b69298d19f66cd5`, .NET10.0.12 / Python3.13.15 / Streamlit1.61.1 / Plotly7.1.0. Derived temporary image `sha256:d56612b5ac27e4200ee3658ec352330dc8a0240071133e22897b3f06801cc120`, never pushed. Same isolated construction as C2B-26, not the production image or startup. Existing base image and protected WP07/archive material unchanged.

Network-disabled workloads: UID/GID1000, readonly root, all capabilities dropped, no-new-privileges, one CPU, memory/swap cap512MiB, PIDs64,32MiB noexec/nosuid tmpfs. Initial scratch-volume ownership setup only used a short bounded root helper with CHOWN, not a privileged workload. Explicit env-i synthetic environment, no inherited WEBSITE/provider/private settings. Fixed-path dataset is a read-only volume subpath, not a host private mount or Azure delivery mechanism. No published ports, relay or private test network ran.

Offline C# producer exit0: manifest1454 bytes, SHA256 `007b2c8a6ccec19ec54976b4baf735812b648d79294e6465df884c68537d3069`; thirteen-file set656176 bytes. Every member size/hash matches C2B-25/C2B-26. Frozen coverage/end-exclusive timestamps, generator seed114,250 hidden calculation rows and aiq-ta-v1 C# SMA unchanged. No Python/JS indicator calculation. Twelve fixed-path loads passed: 1h24/168/720/2160,4h6/42/180/540,1d1/7/30/90. Per-member hashes remain the previously recorded immutable set; no new production trust qualification implied.

Reader measurements, n=12 complete-set reads:80.101–87.992ms wall time,1.068645 CPU-seconds including the two-reader probe, process peakRSS37724KiB; separate cgroup memory.peak40136704 bytes, usage1797885 microseconds. Two simultaneous reads yielded one available and one immediate denial. Artifact/code write attempts denied by kernel read-only mounts or permissions. These are local exploratory measurements under caps, not F1 quotas, p95, durable/global admission or session-load guarantees.

Four actual fixed-path reader cases ran in separate network-none containers: valid30D ->720 rows; missing, tampered and unreadable manifests/members -> generic `ImmutableCandidateUnavailable`; all exit0, no OOM. Fault variants were copied into owned scratch volume locations only. This proves reader denial, not rendered browser startup/failure behavior.

Four initial Streamlit AppTest attempts failed before rendering because its harness did not apply the CLI server address from the provided environment (`st.get_option('server.address')` was None). All four exit1; no assertion bypass, Streamlit patch or source correction. They count as **failed harness attempts**, not product failure or acceptance. No unit-suite rerun; historical C2B-26 nineteen passing tests are not reported as newly run here.

Actual network-none Streamlit server, CLI address127.0.0.1:8502: an initial probe raced startup and got connection refused. A bounded readiness wait was added to the temporary probe, then health200; dataset/candles/indicators each503 DATASET_NOT_READY; admin404. Unknown artifact/local-source URLs returned only the10951-byte SPA HTML, no manifest schema/hash. No public/host port bindings; only loopback TCP listener; tree contained timeout and Python/Streamlit, no C# service. UI cgroup peak85504000 bytes, usage1641449 microseconds,11 throttled periods/754546 microseconds at sample; not interactive-browser/F1 load. Explicit owned shutdown exit143, no OOM; no guardian/crash-safe shutdown claim.

## Real-browser acceptance blocker and concrete next decision

| Required browser evidence | 1D | 30D |
|---|---|---|
| Candles/volume, selectors, native popover and toolbar | NOT RUN | NOT RUN |
| Visible SMA off -> on -> off, values bound to artifact | NOT RUN | NOT RUN |
| Hover, zoom and pan | NOT RUN | NOT RUN |

No new screenshots. Prior Windows screenshots and reader tests are not substituted. Owner-accepted zoom reset remains a known UX limitation, not a new failure or compatibility task.

The prepared method was a temporary standard-library TCP relay from a private Docker interface to the unchanged127.0.0.1:8502 UI, on an owned internal Docker network, published only to host127.0.0.1:8504 for existing Edge. Proposed caps:180 seconds,8 concurrent connections,32MiB per connection,3-second socket operations,60-second idle; container CPU/memory/PID/read-only bounds retained. No package download, application modification or readiness relaxation. Local processes would be able to access the synthetic test UI during that interval; it is still a new test-service exposure requiring an explicit exception.

**Automatic approval review rejected the relay/network/host-port launch:** C2B-27 prohibits a network-exposed test service without separate owner approval. The grouped launch command did not execute. No workaround or indirect execution followed. No relay/private network/host8504 listener/browser/CDP was started. The harmless prepared relay script remains only in owned temporary storage for review.

Minimal next owner decision: separately authorize that exact bounded test-only loopback relay/private network and local-process exposure, or retain INCOMPLETE with no browser connectivity. Do not broaden this to production routing or install a browser package without authority. A Linux browser binary/image already present could be another future method; none was used here.

## Fresh Azure observations — independently per site

Read-only ARM config/plan and eight bounded public HTTPS probes. Each request timeout8s,700ms spacing; no timeout this run. No appsetting credential values or application-console/private filesystem access.

| Evidence | WP03: aiqr112wp035ec325382770 | WP05: aiqr112wp05wus27f5eabb5 |
|---|---|---|
| Resource group | rg-aiq-r112-wp03-wcus-5ec325382770 | rg-aiq-r112-wp05-wus2-7f5eabb5 |
| ARM state / plan | Running, Linux custom container, F1 Free | Running, Linux custom container, F1 Free |
| Configured image / rollback reference | Shared digest below | Shared digest below, independently queried |
| Startup override / Always On / health path | empty / false / null | empty / false / null |
| Nonsecret settings | WEBSITES_PORT8501; storage=true; no AIQ_RUNTIME_MODE entry | WEBSITES_PORT8501; storage=true; no AIQ_RUNTIME_MODE entry |
| Actual public /_stcore/health | 200 ok,0.77s | 200 ok,0.91s |
| Actual dataset/candles/indicators paths | Each200 with Streamlit HTML prefix, about0.67–0.70s | Each200 with Streamlit HTML prefix, about0.75–0.83s |
| Required503 JSON route boundary | **NOT MET** | **NOT MET** |
| Effective container UID/rootfs/readonly paths | UNKNOWN; no console execution authorized/performed | UNKNOWN; independently unverified |
| Immutable artifacts/anchor available in deployed image | UNKNOWN; local candidate files excluded, no promotion | UNKNOWN; local candidate files excluded, no promotion |
| Effective startup code / actual container runtime version | UNKNOWN; configured image is not proof of exact entrypoint/binaries | UNKNOWN; same digest does not establish site runtime behavior |
| Private credential/file isolation | UNKNOWN; no private inspection; historical setting-name inventory includes provider/replay names | UNKNOWN; same limitation, no inference from WP03 |
| Deployment permission / registry pull credentials | Not established by successful read access; no credentials acquired | Independently not established |

Configured pinned image on both: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:144de4a9c393f07d2249f450b9d5ab2af34f36b941897a31bd20d72ac325959a`.

These successful health reads supersede prior timeout/unknown observations only for this probe. HTML200 is not the required API503 contract, and does not establish synthetic publication: only the HTML prefix was sampled, no chart/WebSocket inspection of Azure performed. No rollout/routing repair was attempted. Effective image/entrypoint or ingress routing must be reconciled; cause remains unproven. The current digest is an observed restoration reference, **not a qualified contract-compliant rollback target**.

## Minimal proposed artifact delivery — approval required, no implementation

Choose a single reviewed image with the complete immutable set baked in at build time, rather than a new service, Azure share/download path, mutable /home handoff or request-time C# generator. Exactly13 files and the independently reviewed anchor/release record enter an owner-controlled staging context only after separate export/promotion authority. Existing `_immutable_chart_candidate` remains excluded from current Git/Docker contexts. No automatic copy of local files into production is implied.

Concrete future sequence:

1. Complete Linux browser evidence using separately authorized connectivity. Review handoff numerical/byte mapping against existing independent C# proof; accept a production release schema/context, trusted external anchor, provenance and eligible release identity. Candidate-v1 and its pin are not promotion approval.
2. Separately authorize offline export and operator/reviewer promotion of exact whole-set bytes; record all hashes/sizes, source/toolchain, revocation/rollback eligibility. Place eligible release in a finite staging context, never an anonymous upload or mutable runtime cache.
3. Propose narrowly reviewed source/image changes under **new implementation authority**: explicit production immutable reader/gate; Docker COPY from that approved staging source to its fixed app path; read-only root-owned code/anchor/artifacts/ancestors and writable scratch only; preserve public env-i credential stripping and no Worker. A root-owned asset is currently rejected by reader UID-equality checks, and Azure/public bind is rejected by the candidate gate. Do not simply toggle Development or remove WEBSITE markers. Production ownership and gate semantics therefore require approval before any edit. Current Dockerfile's app ownership pattern and startup are not an accepted production immutable delivery design.
4. Build locally against a documented runtime/dependency identity; prove immutable image contents, root/ancestor non-writability by public UID, missing/tamper failure, no private data/settings, complete-read bounds and public-boundary routing. Then obtain separate image build/push authority and repository/registry least-privilege publishing/pull credentials. No registry push now.
5. Accept complete-read/send/cancellation, health/abuse/admission and per-session/multi-instance accounting; measure CPU/RSS/startup/concurrent sessions under actual F1 constraints before promotion/rollout. Local semaphore is not durable/global protection. One validation at a time/no queue is only a candidate process-local limit. Verified F1 daily CPU allowance, memory/headroom and loaded latency remain unknown;1CPU/512MiB are test caps, not Azure entitlements. Always On=false means cold/idle behavior also needs evidence.
6. Separately approve image promotion and each site's desired UI role, exact startup/port/settings delta, pull authentication, rollback plan and deployment permissions. Only Streamlit port8501 should be publicly routed; no internal data source port/service. Artifact storage needs no new service or recurring resource. Validate $0 resource assumptions rather than infer capacity from F1 SKU.
7. Obtain explicit public chart exposure authorization after all applicable readiness obligations. Local preview visibility and PUBLIC_RUNTIME_VERIFIED=False do not authorize public UI data. Public API publication is a separate contract/gate decision; keep503 until explicitly approved. Do not smuggle chart activation through a new environment flag or waived API state.

Anticipated production changes involve reader/ownership/gate tests plus Dockerfile/entrypoint/presentation wiring, all outside this qualification-only phase. No such paths edited. No private replay/provider or held WP07 shortcut.

## Per-site deployment/rollback checklist — neither authorized

| Step | WP03 | WP05 |
|---|---|---|
| Role decision | Owner confirm first public showcase target | Independently confirm replica/UI role; do not assume API-only or infer approval from WP03 |
| Baseline route prerequisite | Reconcile current HTML200 API responses with reviewed ASGI PublicBoundary; prove503 on an approved unavailable image before rollout | Independently establish the same contract; current identical digest is insufficient |
| Trust/delivery/permissions | Review eligible staged set and image anchor, root-owned readonly assets, actual UID and private isolation; no /home fallback | Independently verify image/mount/UID/artifact evidence on this resource |
| Deployment authorization | Explicit registry build/push/pull authorization and this site's config/deployment/restart permissions; settings credentials handled outside PR | Separate site/resource-group authorization and pull configuration; never copy owner/provider secrets from WP03 |
| Pre-change capture | Secure operator capture of this site's exact digest and configuration; safe diff in review, no secret values in PR | Independently capture WP05 configuration; do not reuse WP03 snapshot |
| Rollback selection | Observed digest144de4... is only historical reference and currently fails503 contract; select/qualify an unavailable rollback image before deployment | Same digest independently observed, likewise not a qualified rollback target |
| Future rollout | Only after new explicit authority: immutable image digest, approved startup/settings, one exposed Streamlit port; validate health/API/asset exclusion/approved chart behavior | Only after its own authority and verification; no automatic second-site rollout |
| Future rollback/verify | On any failure, stop activation and restore preapproved unavailable digest/settings under authorized procedure; verify health,503 JSON, no data exposure, artifact revocation/session handling | Independently restore and verify WP05; if HTTPS times out classify UNKNOWN and stop, never declare success |

No per-site safe rollout can be approved while route contract, immutable trust/delivery, Linux browser and F1/readiness evidence remain unresolved. No mutations to Azure, RBAC, startup, registry, CI/CD, resources or release flags.

## Commands, scope of evidence and cleanup

All orchestration Windows PowerShell5.1. Owned run: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b27-6d3f4562373b4768bff555c6df05c26a`. Reused C2B-26 temporary Dockerfile construction with fresh #347 archive. Commands executed include:

```powershell
git archive --format=zip --output="$run\source.zip" 8240d0878dea335c08ad3216600fc7e545446140
docker build --pull=false --network none -f "$run\Dockerfile.qualification" -t aiq-c2b27-qualification:8240d08 "$run"
# Network-none resource array from C2B-26; newly owned scratch volume and readonly fixed-path mount:
docker run --name aiq-c2b27-producer @bounds --mount "type=volume,source=$volume,target=/scratch" aiq-c2b27-qualification:8240d08 env DOTNET_ENVIRONMENT=Development AIQ_IMMUTABLE_EXPORT=1 timeout 60 dotnet /qualification/worker/AIQuantTradingResearch.Worker.dll --write-immutable-chart-candidate /scratch/data
# Reader/failure scripts were temporary only; no browser command executed.
```

Sanitized temporary scripts: qualify.py (twelve reads/metrics/two-reader probe), variants.py (owned fault copies), reader_failures.py (four fixed-path cases), startup.py (failed AppTest), probe_routes.py (bounded readiness/internal HTTP). Their paths/outputs are retained locally; no artifacts or scripts enter the PR. Public probes used urllib with8s timeout and700ms spacing; ARM queried image/startup/F1 and only three nonsecret setting values.

Eleven recorded containers removed, owned scratch volume removed, temporary image tag removed; no active UI, host8504/9224 listener, relay or test network. No package/browser downloaded, no unrelated process/container/image/port changed, no broad cache prune. Four harness containers exit1, successful producer/reader cases exit0, stopped internal UI exit143; no OOM. No browser/SMA interaction test counted, no Windows evidence substituted, no broader unit/E4 suite run.

Outcome: **INCOMPLETE / BLOCKED** pending explicit test-connectivity exception and resolution of the separately observed Azure route-contract mismatch. Both PRs remain OPEN/UNMERGED. No public activation, deployment or WP07 restart.

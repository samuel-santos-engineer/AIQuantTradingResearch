# Release 1.15 WP62-A ? F1 stop-quota recurrence, offline investigation

Date: 2026-11-15 (America/Sao_Paulo). Documentation only; root cause UNKNOWN.

**NO_AZURE_OPERATIONS_PERFORMED** in WP62-A. No staging probe, restart, redeploy, Azure read, log stream, configuration change, credential access, provider request, Docker operation or retained-runtime/data access occurred. Earlier operations below are historical session evidence, not actions under this task. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Baseline and evidence provenance

Remote main verified through ordinary GitHub/Git repository reads as `fcbb54201e951a8dec5fe71a798aece8e5e22aa8`; PR #446 MERGED at that squash commit. PR #451's `ca874bb7afb4a79011f621cbf5ca314af0f8018a` is an ancestor. An isolated clean documentation branch starts at current main; the owner's older checkout, modified manifest and untracked documents are preserved. No existing evidence rewritten.

Targets in prior evidence: shared West US 2 F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5`, research `aiqr115researchstagingwus27f5eabb5`, UI `aiqr112wp05wus27f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`. README Platform identifies `aiqr112wp035ec325382770` as production, excluded here. No current deployed-build identity is established by source main or configured image alone.

Sources:

- [Phase G evidence](release-1.15-wp58-phase-g-staging-deployment-evidence.md), sections Private baseline and exact mutation / Actual serving and post-apply diagnosis / Reconciliation with current main.
- Owner-provided Phase H authority (not tracked on current main), recorded quota snapshot and H1 constraints; corroborating historical snapshot is tracked in Phase G.
- [Phase F runbook](release-1.15-wp58-phase-f-staging-initialization-runbook.md), Bounded Linux failure diagnostics / Authorized compatible-state Linux qualification.
- [WP61 acceptance](release-1.15-wp61-post-merge-local-browser-qualification.md), Final retained-assets verification and cleanup / Handoff.
- WP62/continuation session's sanitized tool results and owner WP62-A authority. These recent measurements were not previously committed repository evidence; this document records their provenance as session observations, without raw responses or private artifacts.

## Known-facts timeline

All UTC values below explicitly include UTC; Sao Paulo is UTC-03:00. No timezone inferred for Azure's offset-free reset fields.

| UTC | Sao Paulo | Recorded observation and limits |
| --- | --- | --- |
| Nov 14 19:23:52 | Nov 14 16:23:52 | Phase G API image PATCH began; accepted published API runtime digest selected, empty startup. No explicit restart. Image configuration may recycle containers. UI not promoted. Source: Phase G Private baseline. |
| Nov 14 19:26:26 | Nov 14 16:26:26 | Phase G bounded API observation stopped after timeout/503 responses followed by403; no Alive. Source: Phase G Actual serving. |
| Nov 14 19:27:12 | Nov 14 16:27:12 | Shared plan stop counter26/15; research QuotaExceeded/Exceeded/Normal. Root cause of accumulated stops not established. Source: Phase G quota table / Phase H. |
| Nov 15 02:13:59 | Nov 14 23:13:59 | WP61 retained LOCAL chart/data verification passed; not Azure acceptance. Source: WP61 Final retained-assets verification. |
| Nov 15 03:05:31 | Nov 15 00:05:31 | Prior two-read quota snapshot: stop0/15, daily CPU0/3600000ms, short CPU0/180000ms; research Running/Normal/Normal. Owner repeats this recovery baseline. Reset/freshness semantics not independently established. Source: session quota check. |
| Nov 15 03:09:22 | Nov 15 00:09:22 | WP62 UI HEAD / timed out8.034s, no response. One request, no retry. Source: session WP62 probe. |
| Nov 15 03:26:52; 03:27:51 | Nov 15 00:26:52; 00:27:51 | Two separately approved UI detector attempts failed locally/unclassified; no usable Azure startup evidence. Later offline inspection found an unquoted ampersand hazard in az.cmd invocation. It is not proof these requests reached ARM or caused container activity. Source: session continuation. |
| Nov 15 03:29:47; 03:30:37; 03:31:25 | Nov 15 00:29:47; 00:30:37; 00:31:25 | CLI Python-module reads succeeded; UI LinuxRunningContainersPerHost supplied summary schema and informational NO_DATA, not runtime digest/startup proof. Source: session continuation. |
| Nov 15 03:34:42 | Nov 15 00:34:42 | Research GET /health timed out8.035s, zero body bytes, no HTTP status. Source: session health probe. |
| Nov 15 03:35:47; 03:39:37 | Nov 15 00:35:47; 00:39:37 | Research LinuxAppDown timed out10.028s; later LinuxLogViewer returned one unclassified row, no recognized exception. Absence of classification is not absence of error. Source: session continuation. |
| Nov 15 03:50:25 | Nov 15 00:50:25 | Existing log config read: HTTP filesystem false, app filesystem Off, detailed errors/tracing false. Source: session logging preflight. |
| Nov 15 03:54:52 (operation start) | Nov 15 00:54:52 | Temporary container logging enabled; one detector read unclassified; logging switched off. Full-object equality check false; later visible logging flags restored. Exact full-config restoration remains unverified. No explicit restart. Configuration saves can affect lifecycle; contribution to stop count UNKNOWN. Source: session temporary logging operation. |
| Nov 15 04:13:11 (operation start) | Nov 15 01:13:11 | CLI log tail yielded0bytes over20.033s; owned CLI terminated. No explicit restart or logging change in this attempt. Source: session log stream. |
| Nov 15 04:30:33 | Nov 15 01:30:33 | Controlled observation aborted at first quota read: stop57/15. Restart count0; logging/baseline steps not entered. No health/stream/configuration operation in that aborted attempt. Source: session tool result and WP62-A authority. |

Recovery observation and recurrence are separated by approximately85 minutes, but no intervening counter samples establish when stops accumulated, whether a reset intervened, or which app contributed. Do not equate57 counter units with57 API crashes, or compute an application crash rate. Shared-plan observations do not attribute all units to research rather than UI/another consumer. Reported zero CPU does not prove zero work.

## Tracked startup and automation inventory

| Exact path | Offline finding and implication |
| --- | --- |
| `container/release_1_15/research-api.Dockerfile` | UID1654, port52731, bootstrap entrypoint, bind0.0.0.0; no HEALTHCHECK/restart scheduler. Framework-dependent publish inputs are supplied separately. Source is not proof of deployed bytes. |
| `container/release_1_15/research-bootstrap.sh` | Default serve execs Worker --research-snapshot-api, no child restart loop. diagnose emits a bounded result then exits0; malformed diagnostic inputs exit1. operator-stdin/operator-chart-stdin reject missing private input or failed initialization then exit1; only success transitions to serve. Unknown mode exits1. A one-shot mode selected on a web app could therefore terminate, but current mode is unknown. |
| `src/AIQuantTradingResearch.Worker/Program.cs` | Explicit research CLI branches dispatch directly to initializer/probe/API; no platform restart API in these dispatch paths. |
| `src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs` | RunAsync validates directory, service key, transport/TLS and existing storage before listener52731 starts. Exceptions collapse to fixed unavailable message and return1. Logging providers cleared. GET /health precedes request-key authentication once the listener exists. Missing DB returns null source after boundary checks; this can still serve Alive and honest dataset-unavailable responses. Wrong request key alone should return401 after startup, not establish why health times out. |
| `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ResearchStorageBoundary.cs` | Exact staging opt-in/identity, directory, UID, mount and writable checks can reject startup. Default local behavior differs from explicit staging. No guard weakened. Local tmpfs test mismatch was fixed in harness and25 Linux tests passed; neither that failure nor SDK mutex IOException proves an Azure exception. |
| `eng/azure-cli/r1.15-qualification/wp36_research_deploy.py` | Plan default; explicit Apply approval/key-presence/settings-confirmation gates. Apply can PATCH image/HTTPS and POST /restart. Not a scheduled service; file presence is not execution evidence. |
| `eng/azure-cli/r1.15-qualification/wp11_research_diagnostic.py` | Explicit --execute flow spawns watchdog; --watchdog may issue stop after deadline absent cancellation; recovery can start app. This is a real stop-capable repository path, not evidence it was active during recurrence. No retained process/private attempt inspection performed. |
| `eng/azure-cli/r1.15-qualification/wp21_runtime.py`, `wp21_controller.py`, `wp21_recovery.py`, `wp22_preparation.py` | Explicit operator/controller entry points and private approval flow; runtime includes stop/start/config mutation adapters. No automatic current execution inferred. |
| `.github/` tracked inventory | Templates only; no tracked `.github/workflows` files at this main. No GitHub Actions push/cron/workflow_dispatch deployment trigger found. Repository-wide tracked YAML inventory contains issue template config and empty docker-compose.yml; no Azure Pipelines/Jenkins schedule definition found in that inventory. |
| `eng/azure-cli/r1.11-qualification/wp04-azure-cli-docker-conn-12-data/check-conn-12.ps1`, `check-conn-14.ps1`; `eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/http-investigation.ps1`, `http-investigation-02.ps1` | Historical manually invoked helpers include configuration/restart actions, often for older targets. They are not Release1.15 recurrence evidence. Do not run or disable them. |

Tracked `eng`, `container`, `.github` searches for Register-ScheduledTask/schtasks/New-ScheduledTask/cron/TimerTrigger/schedule/workflow_dispatch returned no scheduling definitions. This search cannot exclude external GitHub automation, operator terminals, Windows tasks outside the repo, Azure automation, platform maintenance or pre-existing detached helpers. No external task/process inventory authorized here.

## Ranked, falsifiable hypothesis matrix

Confidence concerns cause of the counter recurrence, not merely existence of a code path.

| Rank / hypothesis | Support | Contrary evidence / limits | Missing observation | Confidence | Minimal falsification check (future only) |
| --- | --- | --- | --- | --- | --- |
| 1. Startup/configuration/storage rejection, followed by platform lifecycle handling | API source can exit1 before binding; historical API never Alive; fixed generic diagnostics limit attribution. | No app-bound error/exit code captured; configured source/digest does not attest actual runtime. Missing DB alone need not prevent health. | Fresh app-bound process exit reason/stage and running artifact. | Low-moderate plausibility; unproven | One existing, timestamped startup record showing listener started and no process termination in the recurrence window would weaken this mechanism. |
| 2. Platform startup/health recycling or port mismatch | Historical503/403 and current timeouts; F1 platform mediates startup; code binds52731 only after validation. | HEAD/GET timeouts do not prove a health-check failure; no current port/health configuration read. | Platform stop reason and startup/port metadata, independently correlated with container lifecycle. | Low | One attributable platform event distinguishing probe failure from another stop reason; port mismatch requires configuration evidence, not guesswork. |
| 3. Configuration-save/operator or external automation activity | Session includes logging saves; explicit stop/restart-capable helpers exist, including watchdog. | Controlled restart aborted; no explicit restart in recovery interval documented. No tracked CI scheduler. Cannot account for57 units from known saves. | Management event timestamps/action/status/caller category, plus any external schedule. | Low | Bounded activity events containing no successful lifecycle/config writes weakens management-trigger hypothesis, but does not exclude platform recycles or data-plane actions. |
| 4. Bootstrap diagnose/operator mode or missing private stdin/input | Explicit one-shot exit and fail-closed branches exist; Azure input transport remains unqualified. | Source default is serve; earlier configured startup empty; no current mode read or authenticated runtime identity. | Nonsecret active mode and categorized boot result bound to deployed image. | Low | Attributable startup record identifying serve mode and successful listener initialization weakens wrong-mode explanation. No private input access needed. |
| 5. Key/transport configuration startup failure | Required AIQ_RESEARCH_API_KEY and strict transport validation can reject before listen. | No missing-key evidence; earlier owner reported API key present; UI/API mismatch affects authenticated reads after startup, not unauthenticated health directly. Provider key is not required to display retained history. | Fixed missing/invalid configuration category; presence alone cannot prove valid key. | Low | Validated configuration-stage success weakens this hypothesis; never request/print key values or rotate on speculation. |
| 6. Platform maintenance/scale/shared-plan activity | Counter is shared-plan evidence; Azure-managed lifecycle or UI contribution not ruled out. | No maintenance/scale/other-app event evidence; fixed F1 plan in historical baseline. | Existing platform/service events and app attribution. | Low | Existing bounded maintenance/resource-health event metadata; absence in management activity alone is inconclusive. |
| 7. Counter semantics/window/freshness misinterpretation | 0 then57 with offset-free reset times; exact WPStopRequests unit/reset/scope mapping not established by repo. | Both numeric snapshots valid as recorded; cannot dismiss exceeded status as harmless. | Authoritative metric semantics and freshness/reset attribution. | Medium uncertainty; not a root-cause claim | Compare one fresh metadata snapshot with supplied unit/reset fields and independently documented semantics before calling units crashes. |

No evidence supports a specific code/config fix yet. The smallest conditional improvement, if attributable failures expose only a generic unavailable message, is a separate proposal for fixed secret-free startup-stage diagnostics in the existing API path. Do not print exceptions/settings or weaken security/storage guards. No implementation under WP62-A.

## Smallest future observation proposal ? NOT EXECUTED

Separate explicit approval required. Metadata only; no log stream, HTTP health probe, restart or configuration change. Use the installed CLI Python-module entry point with an argument list, avoiding az.cmd URL ampersand splitting. Do not login, upgrade or acquire new credentials. Authentication remains ordinary existing CLI behavior without token output.

Freeze exact subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1` and resource group above. API version2023-12-01 for Web usage;2015-04-01 for Activity Log. At most **two GETs**, sequential,15seconds each/30seconds total, no retries/pagination/nextLink traversal. Cap each collected response1MiB; use a streaming bounded collector rather than unbounded CLI capture before execution. Unknown schema or missing required fields stops. No current approval to execute this proposal.

1. GET `https://management.azure.com/subscriptions/<subscription>/resourceGroups/<group>/providers/Microsoft.Web/serverfarms/asp-aiq-r112-wp05-wus2-7f5eabb5/usages?api-version=2023-12-01`. Project only WPStopRequests and CPU name/current/limit/unit/nextResetTime plus observation UTC. Missing/stale/ambiguous fields remain UNKNOWN. If stop current >= limit, stop after this ONE read; no subsequent call and no claim of recovery. Do not interpret offset-free nextResetTime as a timezone-qualified guarantee.
2. Only if quota metadata permits: one GET `https://management.azure.com/subscriptions/<subscription>/providers/microsoft.insights/eventtypes/management/values?api-version=2015-04-01&$filter=<encoded-filter>&$top=20`. Fixed encoded filter: `eventTimestamp ge '2026-11-15T03:00:00Z' and eventTimestamp le '2026-11-15T04:31:00Z' and resourceUri eq '/subscriptions/<subscription>/resourceGroups/<group>/providers/Microsoft.Web/sites/aiqr115researchstagingwus27f5eabb5'`. Validate returned resource IDs in memory against exact app or its config children; exclude unrelated records. Output only UTC, allowlisted operation class (stop/start/restart/config-write/deploy/other), status enum and coarse caller category (identity withheld). Never output caller identity, IPs, correlation IDs, claims, raw properties/messages, values or private payloads. Record presence of nextLink only: truncation means INCOMPLETE, no follow-on request. Invalid filter/resource/schema/authorization or quota pressure stops; no fallback discovery.

Management lifecycle/config writes would support operator/external automation contribution and provide timestamps for later narrowly approved attribution. No events would NOT prove no platform restart/crash, particularly with20-record cap. UI/shared-plan maintenance and data-plane lifecycle remain outside this exact app query. The event filter/query shape must be validated offline against installed client/public schema before execution; unsupported filter must stop, not broaden scope. A later UI or platform event read would need its own exact bounded proposal, not automatic continuation.

Stop on first timeout, TLS/auth error, target mismatch, unsafe output, uncertain quota or schema. Do not trigger a cold start to collect evidence. This proposal does not prove running-image identity, authenticated API readiness or chart readiness. No guarantee of zero quota/CPU consumption.

## Validation and handoff

One new Markdown file only. UTF-8/LF/no BOM; Markdown table structure, relative source links, changed-path inventory and git diff --check validated before publication. No tests/builds/runtime qualification warranted for this documentation-only change. Historical Windows133 and Linux25 passing fake tests remain separate from current Azure health. Open documentation PR only, unmerged for owner review.

**NO_AZURE_OPERATIONS_PERFORMED** in this task. Root cause UNKNOWN; no claim of quota recovery, endpoint health or public readiness. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

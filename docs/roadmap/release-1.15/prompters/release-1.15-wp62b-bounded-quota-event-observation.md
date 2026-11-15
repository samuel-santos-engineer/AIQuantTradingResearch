# Release 1.15 WP62-B ? bounded quota and event observation

Date: 2026-11-15, America/Sao_Paulo. Result: **AMBIGUOUS_RESET_TIME_STOP**. GET2 NOT_RUN.

## Preflight and authority

Owner WP62-B authority permits only two conditional management GETs. Clean isolated branch based on current main `c4513aebfb69afac9ecbc7633e79e69cae9e950a`, including merged #452 and #454. Read [merged WP62-A](release-1.15-wp62a-stop-quota-offline-investigation.md); exact subscription/group/plan/research-app IDs and API versions matched. New startup-stage code present in source main is not proof of deployed Azure bytes. Existing work and evidence preserved.

Exact GET1 resource: subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`, group `rg-aiq-r112-wp05-wus2-7f5eabb5`, Microsoft.Web/serverfarms/`asp-aiq-r112-wp05-wus2-7f5eabb5`/usages, API2023-12-01. GET2 candidate remains research app `aiqr115researchstagingwus27f5eabb5` only, Activity Log API2015-04-01, fixed03:00?04:31UTC window and exact resourceUri equality, top20. Its URL-encoding round trip matched the merged filter; a prior separately authorized identical query had succeeded, without new discovery/network verification in this preflight. No GET2 issued.

Used installed CLI Python-module entry point and argument arrays, never az.cmd. One CLI GET invocation, no retry/fallback/pagination/discovery/login command. Ordinary existing CLI authentication used without token output; automatic credential-refresh internals were not instrumented, so no claim of observing internal authentication traffic. Bounded concurrent stdout/stderr drains enforced a combined1MiB retained-response cap before appending a chunk that would exceed it; owned CLI terminated on overflow. Fifteen-second wall-clock request deadline; actual2.764seconds. No raw bodies persisted: only allowlisted scalar results retained. No quota identity broadened; absent per-record resource identity is not independently attested beyond the exact requested endpoint. No update-time field supplied, so source telemetry freshness is unverified.

## Actual observations

Observation **2026-11-15T12:20:53.969003Z / 09:20:53.969003-03:00 Sao Paulo**. Exactly **1 GET attempted**; second read **SKIPPED**. Collection completed successfully; conditional progression stopped on ambiguous reset-time metadata.

| Counter | Current | Limit | Unit | nextResetTime exactly as supplied |
| --- | --- | --- | --- | --- |
| CpuTime | 1299 | 3600000 | Milliseconds | 2026-11-16T00:00:00 |
| CpuTimeShortPeriod | 0 | 180000 | Milliseconds | 2026-11-15T12:25:00 |
| WPStopRequests | 0 | 15 | Count | 2026-11-15T13:00:00 |

All reset timestamps lack a UTC offset/timezone. WP62-B explicitly requires immediate stop on ambiguous reset time, even with a below-limit stop counter. Therefore no app-event request, no events/count/nextLink evidence, and no attribution possible from this task. No missing field was invented as zero. Counter scope remains the shared plan; reported0/15 is a point observation, not a guarantee of durable recovery or a claim that prior57/15 meant57 research crashes. Daily CPU1299ms is reported telemetry, not proof of zero work or freshness.

## Interpretation and smallest next action

Observed: reported stop counter below limit at collection. UNKNOWN: reset timezone/window, telemetry freshness, durable recovery, cause/app attribution of prior recurrence, running artifact, process health, key validity, authenticated API or chart readiness. Earlier hypothesis matrix and separate prior event observations are not replaced. No statement that management-event absence excludes platform recycling; GET2 was skipped entirely.

Smallest separately approvable next action: **offline-only review of authoritative quota-metadata documentation/schema for nextResetTime timezone and freshness semantics**, with no Azure calls, probes or runtime operations. If semantics remain unspecified, propose a narrow owner amendment explicitly allowing raw reset fields to remain UNKNOWN while using a valid current/limit snapshot to permit exactly the previously scoped event read. Do not silently relax WP62-B's stop rule or issue that event read now. No new infrastructure/licensing/governance gate.

No app HTTP requests, start/stop/restart, deployment, configuration edit, log stream, app settings/key/private-data/provider/registry/Docker operation occurred. Only one read-only quota GET. No source/test/runtime changes. Documentation-only PR left OPEN/UNMERGED. UTF-8/LF/no BOM, relative link, Markdown table structure, one-file inventory and git diff --check validated before publication; no application tests warranted.

`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Public documentation review ? 2026-11-15

Separate continuation reviewed public Microsoft Learn documentation only; zero Azure management/staging requests. The [exact API2023-12-01 List Usages reference](https://learn.microsoft.com/en-us/rest/api/appservice/app-service-plans/list-usages?view=rest-appservice-2023-12-01), CsmUsageQuota definition, types nextResetTime as string(date-time) and describes its next-reset purpose. It does not specify an assumed timezone for an offset-free returned value, nor a last-updated field or freshness guarantee for the counter in the listed quota schema. This bounded reference review does not establish that no other Microsoft documentation defines these semantics. No UTC assumption or freshness promise derived from it. A direct public raw-spec URL lookup returned404 and supplied no additional schema evidence.

Consequently the ambiguity stop remains justified under WP62-B as written; no event read or new quota query followed. Practical proposed owner amendment: allow supplied reset timestamps to remain UNKNOWN without blocking only the already bounded research-app event observation, when a newly approved quota snapshot reports valid current/limit below threshold. Keep freshness uncertainty explicit; stop for exceeded/missing/malformed counters, unsafe data or request failure. This is a proposed policy disposition for this task, not an implemented rule or a technical fix. Until explicitly approved, no further live operation. PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## Owner-approved amendment and separate observation ? 2026-11-15

Owner explicitly approved retaining reset timezone and counter freshness as UNKNOWN while allowing the fixed event read after valid below-limit quota metadata. This supersedes only the ambiguity progression rule for this separate attempt; original one-read stop evidence remains unchanged. No broad discovery, probes or mutation authority inferred.

Observation start2026-11-15T12:24:39.164645Z /09:24:39.164645-03:00 Sao Paulo. Exactly **2 GETs**, sequential, one invocation each, no retries/pagination/follow-up, combined elapsed3.214seconds. Same installed CLI Python-module invocation, exact targets/API versions/filter as above. Streaming stdout/stderr combined cap1MiB per response, per-request15-second deadline and overall30-second budget; no raw responses retained. No login command or credential output.

GET1: WPStopRequests0/15 Count, CpuTime1299/3600000Milliseconds, CpuTimeShortPeriod0/180000Milliseconds. Raw nextResetTime values respectively2026-11-15T13:00:00,2026-11-16T00:00:00,2026-11-15T12:29:00. Offset timezone and telemetry freshness UNKNOWN, not inferred. Below-limit snapshot is not durable recovery or application health.

GET2 fixed research-app window2026-11-15T03:00:00Z through04:31:00Z, exact resourceUri equality, top20. Returned resource IDs validated against exact app/config children; four records, nextLink absent, bounded result COMPLETE for this query. Caller presence classified only as IDENTITY_WITHHELD; no identifiers/IPs/claims/properties or correlation values published.

| Event UTC | Sao Paulo | Operation class | Status |
| --- | --- | --- | --- |
| 03:54:55.831076Z | 00:54:55.831076 | config-write | started |
| 03:54:57.3623811Z | 00:54:57.3623811 | config-write | succeeded |
| 03:55:01.65443Z | 00:55:01.65443 | config-write | started |
| 03:55:03.7638351Z | 00:55:03.7638351 | config-write | succeeded |

Four lifecycle records represent two completed writes. Their times align with prior temporary logging changes, but this projection does not establish exact payload/caller or full restoration equality. No start/stop/restart operation classified in this bounded research-app result. This cannot exclude automatic platform recycling, process failure, UI/shared-plan contribution, external data-plane activity or counter/window semantics. It does not explain57/15 or establish keys caused any failure. Running image identity, process health, authenticated API/data/chart readiness remain UNKNOWN. No Azure/staging operations beyond these two GETs.

Smallest next step is offline review of whether merged fixed-stage diagnostics are included in the exact currently configured API artifact, using already recorded source/artifact evidence only. Do not rebuild, inspect retained runtime, query registry, deploy or probe automatically. If no attributable startup channel exists, prepare a separate exact bounded candidate-image/observation plan for owner review rather than repeat quota/event reads. Root cause UNKNOWN. PR OPEN/UNMERGED; PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## Repository-only artifact reconciliation ? 2026-11-15

No Azure/registry/Docker/serving operations in this follow-up. Ordinary repository fetch confirms main c4513aebfb69afac9ecbc7633e79e69cae9e950a includes #454 diagnostic source commit92da836bf36ffdb790549a1fe30f1e4665203f5e. ResearchSnapshotEndpoint source history identifies that commit as the addition of fixed startup stages; older source lacks them.

Tracked [Phase G artifact evidence](release-1.15-wp58-phase-g-staging-deployment-evidence.md#scope-and-artifacts) binds published retained WP57 API bytes to runtime digest sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3, local OCI index1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028, tag wp58-wp57-api-1edebb818aff. Publication reused old accepted bytes without build. The separately recorded06:06:51UTC configuration read still selected that runtime digest. No later diagnostic-image build/publication/deployment evidence exists in the reviewed history. Thus the recorded configured artifact predates and has no evidence of containing the newly authored stage labels; source main changes cannot update immutable image bytes. This is provenance/chronology evidence, not a fresh image-layer/DLL audit or actual-running-digest attestation. Do not expect #454 markers from the existing old configured artifact or infer its startup outcome.

Smallest next proposal: authorize local-only candidate preparation from exact main c4513ae, using existing pinned cached Linux inputs, offline/no-restore publication and no-network/no-pull build, source-only context plus exclusion audit and fake regression tests. Confirm exact cached inputs and output paths before execution; missing inputs stop, no fallback downloads. New candidate must preserve API UID1654/port52731/bootstrap/auth/storage guards. Do not package retained DB/observations/key/TLS. No registry push, Azure read/write, restart or local retained-runtime replacement under that phase. Report candidate hashes and qualification evidence first, then seek separate exact publication/staging-observation authority with fresh quota/baseline/rollback plan. This is NOT RUN and not proof that a new diagnostic image fixes the failure. PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## Separately authorized local diagnostic candidate ? 2026-11-15

Owner continued the local-only candidate phase. Initial Docker preflight failed because the Linux engine pipe was unavailable; no build/pull/run followed that failure. Owner subsequently confirmed Docker running and requested retry. Retry verified Linux engine and exact cached pinned ASP.NET10.0 base Linux/amd64. No image pulls.

Existing eng/azure-cli/r1.15-qualification/wp35_research_image.py builder invoked programmatically with exact merged source c4513aebfb69afac9ecbc7633e79e69cae9e950a and local tag aiq-wp62:startup-c4513ae. Git archive exported tracked source only to a new owned output; restore used an empty local feed/config with package sources cleared and NuGetAudit=false; framework-dependent Release publish used --no-restore, UseAppHost=false, ContinuousIntegrationBuild=true. Build used existing research-api.Dockerfile, --platform linux/amd64 --pull=false --network=none. Exact base mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f. No source changes or dependency upgrades.

Candidate local image identifier **sha256:153d2b1ae5e2b856c1ca502849c05fe04ec09950b453b7b8b7d4b47f1dc7205f**; no registry digest/publication claimed. Builder verified Linux/amd64, UID1654:1654, port52731, bootstrap entrypoint and bind0.0.0.0; worker/runtimeconfig net10.0 and Linux native SQLite asset present. Context42 files restricted to research publish and two container source paths; checked names/extensions found0 private observations/watermark/config/DB/key/PFX/PEM matches. Builder retained context-file SHA256 manifest locally without private content. This is source-only context exclusion evidence, not a full image-layer/base-secret audit or universal secret proof. No retained source data/key/TLS included or inspected.

| Owned fake-only case | Result |
| --- | --- |
| Missing configuration | Exit1; fixed CONFIGURATION failure marker present; listener-started marker absent. |
| Fake staging configuration | ApplicationStarted marker present; after12-second bounded observation, owned docker stop completed and process exit0. No published ports or health/API requests. |
| Non-disclosure | Fake service-key string absent in both captured outputs. Raw output held in memory only. |
| Cleanup | Exact ownership label aiq.wp62.qualification=wp62-candidate-qualification verified before stop/removal; both container removals returned0. |

Both runs --pull=never --network=none, no mounts/private files/provider/network access; candidate's configured nonroot user1654 retained. Fake staging run used only new disposable container writable root (required by unchanged staging overlay guard), fake instance/service-key settings and missing state; no real observations or SQLite import. Names aiq-wp62-candidate-missing-configuration and aiq-wp62-candidate-fake-staging-startup;12-second caps plus bounded owned stop/cleanup. Container exit0 after requested stop is not spontaneous long-term stability. No browser, public or authenticated dataset acceptance. Earlier57 focused Windows tests remain prior source qualification, not rerun.

Candidate image and source-only outputs retained; existing chart containers/data/volume/TLS/key/rollback images untouched. No Azure or registry action in this candidate phase. Docker base cache use is not registry publication. No claim the candidate fixes the unknown F1 failure. Smallest next separate authority: exact candidate image audit/publication/readback plan, followed only later by research-only staging baseline/quota/rollback/one-observation plan. Do not deploy automatically. PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false. Evidence PR OPEN/UNMERGED.


## Exact-image audit and separately approved publication ? 2026-11-15

Local archive audit of candidate153d2b1ae5e2b856c1ca502849c05fe04ec09950b453b7b8b7d4b47f1dc7205f checked9 layers,87 application-path occurrences,0 suspect checked application DB/observations/key/TLS paths and0 nonempty secret-named config variables; selected provider/service-key assignment history patterns absent. Linux/amd64/UID1654/bootstrap contract matched. Archive inspected without extraction/execution and unique owned audit directory removed. Scope-limited exclusion evidence, not universal absence of sensitive derived content or full base-file content audit.

Initial registry attempt was rejected before execution by automatic approval review for lack of exact payload/tag/destination authorization and layer-export risk. Owner then explicitly approved the exact candidate publication and readback, acknowledging the limited audit. Approved image ID verified locally, tagged and pushed only to ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp62-startup-c4513ae. Push exit0. Registry raw manifest readback SHA256 matched **sha256:153d2b1ae5e2b856c1ca502849c05fe04ec09950b453b7b8b7d4b47f1dc7205f**. Media type application/vnd.oci.image.index.v1+json: this is the OCI INDEX digest, not a separately verified linux/amd64 child manifest/config digest. Equality to the recorded local identifier does not generally equate image IDs and registry manifests; here registry hashing explicitly verified this particular value. No credentials/raw manifest/private content output.

Registry publication COMPLETE; Azure deployment NOT_RUN. Exact platform-child/config-chain readback and a fresh research-only staging quota/baseline/rollback/one-observation plan remain before any separate apply request. No Azure read/write/restart, UI/production action or retained-runtime/data/key/TLS change in this publication phase. Evidence PR OPEN/UNMERGED; PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.

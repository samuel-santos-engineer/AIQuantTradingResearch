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

# Release 1.13 WP07 --- Terra Historical Vike Runtime Root-Cause Diagnosis Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns technical diagnosis under this authority. GPT-5.6
Luna retains governance and final substantive acceptance authority.
GPT-5.6 Sol may support analysis only.

## Bound candidate

Canonical predecessor:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

WP07 PR:

`#304`

Current corrected candidate head:

`7eafa22e890b120278187c6e9f06033f1038f6ac`

Deployed image:

`ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp07-7eafa22e890b120278187c6e9f06033f1038f6ac`

Immutable digest:

`sha256:83fde5ebda8135bbe654293257270c1970130982cd19bc05f17d2f9df0da3e2c`

App Service:

`aiqr112wp035ec325382770`

PR #304 remains Open and unmerged.

## Interactive evidence

The corrected System Health defect is resolved:

-   System Health renders;
-   `Refresh now` works;
-   no `st.autorefresh` failure;
-   no traceback/internal path;
-   public information-safety checks pass.

The remaining release-blocking defect is historical data:

-   BTC/USD / 1h / 30D -\> controlled unavailable state;
-   Retry historical data -\> same unavailable state;
-   ETH/USD -\> same unavailable state;
-   no candles;
-   no volume;
-   Vike explanatory provenance text appears;
-   no UTC last-updated value appears because no dataset reaches
    presentation.

Controls, ML informational surface, Twelve Data boundary, footer, and
public leakage checks otherwise pass.

## Purpose

Determine the exact root cause of the deployed Vike historical-data
failure.

This is a **diagnosis-first authority**.

Do not modify tracked source code during diagnosis.

Do not guess that the problem is the API key, Vike, cache, Worker,
configuration binding, request mapping, parsing, or networking.
Establish evidence.

The goal is to identify the smallest corrective governance expansion, if
one is required.

## Architecture under diagnosis

Trace the actual deployed path:

`Streamlit -> python historical bridge -> one-shot Worker HistoricalQuery -> HistoricalMarketDataReadService -> cache -> VikeHistoricalMarketDataProvider -> safe response -> Streamlit`

Preserve this architecture.

## Required diagnostic sequence

### 1. Candidate/runtime identity

Prove the running App Service is using the expected corrected
image/digest.

Record only safe deployment metadata.

### 2. Worker/runtime viability

Inside the deployed/runtime-equivalent environment, safely establish:

-   `/app/worker/AIQuantTradingResearch.Worker.dll` exists;
-   `dotnet` is available;
-   the app user can execute the Worker;
-   the Worker process exits/reaps;
-   stdout remains protocol-safe.

### 3. Cache viability

Safely establish:

-   `/home/aiq-market-cache` exists;
-   ownership/permissions permit `aiq` read/write;
-   no path traversal or malformed cache condition is blocking both
    instruments;
-   cache state is classified without dumping sensitive/internal content
    into public evidence.

Do not delete production cache unless strictly necessary and separately
justified. Prefer non-destructive inspection.

### 4. Configuration binding

Safely establish whether the running .NET process receives the Vike
configuration expected by `VikeConfiguration`.

The App Service contains one setting named:

`Vike__ApiKey`

Never retrieve, print, echo, serialize, screenshot, log, or expose its
value.

Use only boolean/redacted/name-only evidence.

Verify the exact .NET configuration binding convention expected by the
implementation.

If a different setting name is required, report it; do not silently
create/change App Service settings under this authority.

### 5. Direct governed Worker query

Invoke the deployed one-shot Worker using its existing `HistoricalQuery`
contract, not a new provider bypass.

Exercise at minimum:

-   BTC/USD, 1h, 30D;
-   ETH/USD, 1h, 30D.

Capture only safe protocol/result metadata sufficient to identify:

-   success vs failure;
-   typed failure category;
-   whether failure occurs before provider acquisition, during provider
    acquisition, parsing, validation, cache write/read, or response
    presentation;
-   process exit status.

Do not expose raw provider bodies or secrets.

### 6. Vike outbound viability

If the Worker evidence points to provider acquisition, safely establish:

-   DNS resolution viability;
-   outbound HTTPS viability;
-   HTTP status/failure class through the governed provider path;
-   whether authentication/configuration is accepted;
-   whether rate limiting is involved;
-   whether request endpoint/query mapping is accepted.

Do not build a Python Vike client.

Do not use Twelve Data as fallback.

Do not print raw provider responses.

### 7. Provider contract inspection

Inspect the existing repository implementation necessary to explain the
observed failure, including as needed:

-   `src/AIQuantTradingResearch.Infrastructure/MarketData/Vike/VikeConfiguration.cs`
-   `src/AIQuantTradingResearch.Infrastructure/MarketData/Vike/VikeHistoricalMarketDataProvider.cs`
-   relevant Vike tests;
-   Worker HistoricalQuery execution;
-   dependency injection/configuration registration;
-   Application historical read/cache contracts.

Inspection does not authorize mutation.

Compare implementation expectations to safe runtime evidence.

## No tracked mutation

This authority authorizes **zero tracked source/document mutation**.

Do not push a new commit to PR #304.

Do not modify the seven existing WP07 paths.

Do not add an eighth path.

Do not alter tests merely for diagnosis.

If temporary local diagnostic commands/files are unavoidable, they must
remain untracked, contain no secret, and be removed before completion.

## Azure/configuration boundary

Do not:

-   add/change/delete App Service settings;
-   rotate Vike credential;
-   change plan/tier;
-   restart solely to hide a defect;
-   create resources;
-   modify deployment configuration;
-   publish another image;
-   deploy another revision.

Safe read-only metadata/runtime diagnosis is authorized.

If the root cause is a configuration-name mismatch requiring an App
Service setting change, stop and report the exact safe setting
name/action required without revealing values.

## Secret handling

`Vike__ApiKey` is secret.

Never retrieve or disclose its value.

A diagnostic operation that would necessarily print the value is
forbidden.

Prefer checks such as:

-   setting-name inventory;
-   boolean presence;
-   configuration option validity without value;
-   typed authentication/configuration result.

## Retry-until-governance-boundary rule

Do not stop merely because a diagnostic command, Worker invocation, cold
start, DNS lookup, HTTPS request through the governed path, or tooling
step initially fails.

Retry and refine diagnosis within read-only authority until the failure
is localized.

Stop only when further diagnosis/correction requires mutation or another
governance boundary.

## Required root-cause classification

Conclude with one evidence-backed primary classification, such as:

-   App Service configuration-name/binding mismatch;
-   credential rejected/missing at runtime;
-   outbound network/DNS failure;
-   Vike endpoint/request-shape mismatch;
-   Vike authentication/rate-limit/provider failure;
-   provider response parsing/validation defect;
-   Worker HistoricalQuery/DI defect;
-   cache/lock/permission defect;
-   Python bridge/presentation defect;
-   another specifically evidenced cause.

Do not classify based only on inference.

## Corrective-path determination

After establishing root cause, identify the **minimum exact corrective
action**.

If source mutation is required, name every exact repository path that
must be added to the next literal allowlist.

If only governed App Service configuration is required, name the
setting/action but never its value.

If the problem is external/transient and no code/config change is
appropriate, specify the evidence and the bounded retry/requalification
action.

Do not perform the correction under this diagnosis authority.

## Protected lifecycle

Do not:

-   merge PR #304;
-   close issue #294;
-   mark WP07 Done;
-   begin WP08;
-   close milestone #64;
-   create tag;
-   create GitHub Release;
-   modify root README;
-   modify dependencies/schema;
-   implement indicators/ML/trading.

## Required final report

Return:

-   candidate full head;
-   deployed image/digest identity;
-   Worker runtime/path/exit evidence;
-   cache ownership/state evidence;
-   safe Vike configuration-binding evidence;
-   BTC/USD Worker HistoricalQuery result and typed failure;
-   ETH/USD Worker HistoricalQuery result and typed failure;
-   outbound/provider evidence if applicable;
-   exact failure layer;
-   evidence-backed root cause;
-   whether the failure is configuration, infrastructure,
    provider-contract, application, Worker, cache, bridge, or
    presentation;
-   minimum corrective action;
-   exact additional repository paths required, if any;
-   whether an App Service configuration action is required;
-   confirmation no secret value was retrieved/displayed;
-   confirmation no tracked mutation occurred;
-   PR #304/issue #294/#295/milestone state;
-   next narrow authority required.

End with exactly one of:

`RELEASE 1.13 WP07 HISTORICAL VIKE RUNTIME ROOT-CAUSE DIAGNOSIS: PASS`

or

`RELEASE 1.13 WP07 HISTORICAL VIKE RUNTIME ROOT-CAUSE DIAGNOSIS: BLOCKED`

A diagnostic PASS means the root cause and minimum correction are
proven. It does not mean WP07 runtime acceptance has passed.

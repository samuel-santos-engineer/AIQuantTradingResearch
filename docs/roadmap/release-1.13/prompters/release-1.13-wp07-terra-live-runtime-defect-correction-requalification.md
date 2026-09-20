# Release 1.13 WP07 --- Terra Live Runtime Defect Correction and Requalification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns technical diagnosis and correction within this
authority. GPT-5.6 Luna retains final substantive acceptance authority.
GPT-5.6 Sol may support analysis only and may not assume Luna governance
authority.

## Bound candidate

Canonical Release 1.13 predecessor:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

WP07 PR:

`#304`

Branch:

`feature/release-1.13-wp07-azure-f1-validation`

Interactive-tested candidate head:

`5625f8a89cc3605fbfab6bc19b833fc7c0da9b72`

Published candidate image:

`ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp07-5625f8a`

Deployed immutable digest:

`sha256:805eceed370155ad4ae1f53f049d71446be6391c1a23bc82fe390ea19b4545b7`

Target App Service:

`aiqr112wp035ec325382770`

The interactive runtime validation established that the app is reachable
and several Release 1.13 surfaces work, but WP07 remains technically
incomplete.

## Observed live failures

Treat the following as authoritative reproduction evidence to diagnose,
not as assumptions about root cause.

### Historical market-data failure

With the public application running:

-   default `BTC/USD`, `1h`, `30D` rendered the controlled message:
    `Historical market data is temporarily unavailable. Please try again later.`
-   selecting `ETH/USD` produced the same unavailable state;
-   no candlestick chart rendered;
-   no volume rendered;
-   Vike public-source explanatory text was present;
-   no UTC last-updated value rendered because no historical dataset
    reached the UI.

Therefore real deployed Vike success through the governed path is not
yet proven.

### System Health failure

The System Health surface visibly exposed an `AttributeError` indicating
that `streamlit` has no `autorefresh`.

The public page also exposed a traceback and the internal path:

`/app/python/presentation/realtime_financial_visualization.py`

This violates the controlled-public-error boundary.

### Already-passing interactive evidence

Preserve these behaviors:

-   deployed UI reachable;
-   defaults `BTC/USD`, `1h`, `30D`;
-   ETH/USD selection;
-   1h/4h/1d control matrix advertised;
-   1D/7D/30D/90D range matrix advertised;
-   ML & Automation Studies content;
-   Twelve Data public/private research-boundary messaging;
-   no-trade footer;
-   no `Vike__ApiKey` exposure was observed.

## Purpose

Diagnose and correct the two demonstrated live runtime defects:

1.  historical Vike data unavailable for BTC/USD and ETH/USD;
2.  System Health public traceback/internal-path leakage caused by the
    `streamlit.autorefresh` failure.

Then rebuild, redeploy, and requalify the same WP07 candidate.

Do not redesign the release or broaden product scope.

## Secret handling

`Vike__ApiKey` is governed server-side configuration.

You may verify only what is necessary to determine whether the running
process receives the expected configuration, using mechanisms that do
not reveal the value.

Never print, echo, retrieve into evidence, log, persist, commit,
screenshot, or disclose the key.

If diagnosis requires proving presence, use a boolean/name-only or
safely redacted check.

Do not rotate, replace, delete, or invent the credential.

## Mandatory diagnosis before mutation

Establish the root cause of the Vike failure before changing code.

Trace the deployed path:

`Streamlit -> bounded local Worker invocation -> HistoricalQuery -> HistoricalMarketDataReadService -> cache -> Vike when governed -> presentation-safe JSON -> Streamlit`

Inspect evidence for at least:

-   deployed container has the intended candidate revision;
-   Worker DLL remains at
    `/app/worker/AIQuantTradingResearch.Worker.dll`;
-   `dotnet` remains available;
-   Worker process can be invoked as the app user;
-   `/home/aiq-market-cache` exists and is writable by `aiq`;
-   `Vike__ApiKey` setting name is present to the running application
    without displaying its value;
-   .NET configuration binding expects exactly the deployed setting
    convention;
-   Vike base URL/request construction is valid;
-   outbound HTTPS/DNS from the F1 container is possible;
-   Worker HistoricalQuery produces a controlled response;
-   provider failures are typed rather than leaked;
-   cache/lock permissions do not prevent acquisition;
-   one-shot Worker exits/reaps correctly.

Use the smallest safe diagnostic surface. Do not create a
provider-bypass Python implementation.

## System Health diagnosis

Determine why the pinned Streamlit runtime does not provide the invoked
`autorefresh` member.

The current dependency boundary remains:

-   `streamlit==1.61.1`
-   `plotly==7.1.0`

Do not solve this by adding an unapproved dependency.

Correct System Health using repository-native functionality compatible
with the pinned Streamlit version, while preserving bounded F1 behavior.

The public UI must never display a traceback, source path, raw
exception, raw stderr, provider response, cache path, or secret.

## Mutation boundary

Start from PR #304 and preserve its frozen five-path scope.

Before mutation, print the exact existing five-path allowlist from the
candidate and verify it against the PR.

Corrections are authorized only when they fit inside those exact five
paths.

If either defect requires changing a sixth tracked path, a dependency,
schema, provider contract, architecture contract, or an unapproved Azure
resource/configuration mutation, stop with a governance restriction and
identify the exact additional path/action required.

Do not silently expand the allowlist.

Any correction changes the candidate head. Commit and push corrections
to the existing WP07 branch/PR #304 and report the new full head.

## Retry-until-governance-boundary rule

Do not stop merely because an implementation, container, deployment,
test, Vike query, Worker invocation, cold start, cache, build, lint,
validation, or tooling step fails.

Diagnose the failure, make corrections permitted by this authority,
rerun the relevant checks, and continue iterating until the authorized
acceptance conditions pass.

Stop only if the next corrective action would require exceeding this
authority or crossing an explicit governance restriction.

When blocked, report the exact restriction, failed evidence, and
corrective action required; do not perform that action without new
authority.

Classify outcomes as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

or:

`GOVERNANCE RESTRICTION — STOPPED`

## Required local/container regression

After correction, rerun the affected and baseline checks, including:

-   Release build with 0 warnings / 0 errors target;
-   .NET tests affected by Worker/cache/Vike behavior;
-   Python presentation suite;
-   Python compile;
-   `pip check`;
-   PowerShell parser;
-   `git diff --check`;
-   secret scan;
-   no-bypass scan;
-   README unchanged;
-   dependencies unchanged;
-   Docker/container build;
-   Worker DLL fixed-path proof;
-   `dotnet` availability;
-   entrypoint syntax;
-   app-user cache write proof;
-   one-shot HistoricalQuery smoke;
-   process exit/reaping.

Add or strengthen tests for the demonstrated defects where possible
within the frozen allowlist.

Do not weaken tests merely to obtain PASS.

## Deployment authority

After local/container correction passes, publish a new immutable
candidate image through the existing repository/GHCR mechanism and
deploy it only to the existing F1 App Service.

Record:

-   new full Git head;
-   image tag;
-   immutable digest;
-   deployment target/revision;
-   deployment timestamp/evidence.

No merge is authorized.

No new Azure resource, plan, service, slot, paid feature, DNS change, or
capacity upgrade is authorized.

Preserve Linux F1/Free and HTTPS-only topology.

## Live requalification

After deployment and cold-start recovery, re-run interactive runtime
validation.

The live qualification must establish all of the following.

### Historical Market Research

-   application reachable over HTTPS;
-   default screen Market Research;
-   default BTC/USD;
-   default 1h;
-   default 30D;
-   BTC/USD renders historical candlesticks;
-   BTC/USD renders volume;
-   ETH/USD renders historical candlesticks;
-   ETH/USD renders volume;
-   supported interval controls work;
-   supported range controls work;
-   Vike provenance appears;
-   UTC last-updated appears;
-   no public Twelve Data fallback;
-   normal hover/zoom/pan does not trigger a provider architecture
    bypass.

### Informational surfaces

-   ML & Automation Studies renders;
-   Twelve Data research-boundary message renders;
-   System Health renders without traceback/internal implementation
    leakage;
-   no-trade footer renders.

### Public safety

The browser must not expose:

-   `Vike__ApiKey`;
-   any secret value;
-   stack trace;
-   `/app/...` internal source path;
-   Worker filesystem path;
-   cache filesystem path;
-   raw stderr;
-   raw provider HTTP body;
-   raw internal exception.

## F1/resource invariants

Reconfirm:

-   Linux F1/Free unchanged;
-   HTTPS-only;
-   no new Azure resource/service;
-   no paid-resource mutation;
-   no persistent companion service;
-   Worker remains one-shot;
-   cache remains under governed persistent `/home`;
-   bounded cache retention;
-   no high-frequency background polling;
-   no provider call merely from hover/zoom/pan;
-   no unnecessary process/provider fanout;
-   recurring infrastructure target remains `$0.00`.

Do not overstate Azure SLA or performance.

## Forbidden scope

Do not:

-   merge PR #304;
-   close issue #294;
-   mark WP07 Done;
-   begin WP08;
-   close milestone #64;
-   create a Release 1.13 tag;
-   create a GitHub Release;
-   modify root README;
-   add dependencies;
-   change schema;
-   add indicators;
-   implement ML;
-   add signals/forecasting/backtesting/trading;
-   expose Twelve Data publicly;
-   add a Python Vike client;
-   bypass `HistoricalMarketDataReadService`;
-   reveal or mutate the Vike credential;
-   create or upgrade Azure resources.

## Lifecycle state after technical PASS

Even after successful requalification:

-   PR #304 remains Open and unmerged;
-   issue #294 remains Open/non-Done;
-   issue #295 remains Open;
-   milestone #64 remains Open;
-   no tag/GitHub Release exists.

The corrected head becomes the candidate for separate GPT-5.6 Luna final
substantive acceptance.

## Required final report

Return:

-   canonical predecessor;
-   PR #304 state;
-   old candidate head;
-   exact five-path allowlist;
-   diagnosed root cause of historical-data failure;
-   diagnosed root cause of System Health failure;
-   technical corrections;
-   new full candidate head;
-   exact changed paths versus canonical predecessor;
-   image tag;
-   immutable image digest;
-   deployed target;
-   safe setting-presence evidence with no secret value;
-   Worker/path/process evidence;
-   cache permission/persistence evidence;
-   local/container validation results;
-   BTC/USD live candle + volume result;
-   ETH/USD live candle + volume result;
-   Vike provenance result;
-   UTC last-updated result;
-   controls/defaults result;
-   System Health result;
-   ML/Twelve Data/footer result;
-   secret/internal-leakage result;
-   F1/zero-cost topology result;
-   README/dependency/schema results;
-   issue #294/Project status;
-   #295/milestone #64 status;
-   confirmation PR remains unmerged;
-   confirmation no tag/GitHub Release;
-   any remaining governance restriction;
-   next authority.

End with exactly one of:

`RELEASE 1.13 WP07 LIVE RUNTIME DEFECT CORRECTION AND REQUALIFICATION: PASS`

or

`RELEASE 1.13 WP07 LIVE RUNTIME DEFECT CORRECTION AND REQUALIFICATION: BLOCKED`

A PASS authorizes only progression to GPT-5.6 Luna WP07 final
substantive acceptance. It does not authorize merge or lifecycle
completion.

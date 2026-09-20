# Release 1.13 WP07 --- Terra Narrow Presentation Defect Correction and Live Requalification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this narrowly expanded technical correction and
requalification. GPT-5.6 Luna retains final substantive acceptance
authority. GPT-5.6 Sol may provide supporting analysis only.

## Bound state

Canonical Release 1.13 predecessor:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

WP07 PR:

`#304`

Branch:

`feature/release-1.13-wp07-azure-f1-validation`

Current candidate head:

`5625f8a89cc3605fbfab6bc19b833fc7c0da9b72`

PR #304 must remain Open and unmerged throughout this authority.

The prior correction authority stopped correctly because the confirmed
System Health defect is outside the original five-path allowlist.

## Confirmed defect

The deployed System Health failure originates at:

`python/presentation/realtime_financial_visualization.py:374`

with the unsupported call:

`st.autorefresh(interval=interval * 1000, key="wp05_refresh")`

The public runtime exposed an `AttributeError`, traceback, and internal
`/app/...` path.

The application is pinned to:

`streamlit==1.61.1`

No dependency addition or upgrade is authorized.

## Expanded literal allowlist

This authority expands the prior five-path allowlist by exactly two
presentation paths.

The complete seven-path allowlist is:

1.  `container/entrypoint.sh`
2.  `eng/azure-cli/r1.13-deployment/wp07-azure-f1/validate-wp07-container.ps1`
3.  `docs/roadmap/release-1.13/WP07_AZURE_F1_VALIDATION.md`
4.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
5.  `docs/roadmap/release-1.13/prompters/release-1.13-wp07-terra-azure-f1-integration-stability-security-zero-cost-validation.md`
6.  `python/presentation/realtime_financial_visualization.py`
7.  `python/presentation/test_realtime_financial_visualization.py`

No eighth tracked path is authorized.

Before mutation, verify the first five paths are the existing PR #304
scope and that paths 6--7 are the only newly authorized paths.

## Purpose

Perform the smallest pinned-Streamlit-compatible correction necessary
to:

-   eliminate the unsupported `st.autorefresh` runtime failure;
-   prevent System Health from exposing traceback/internal
    implementation details;
-   add focused regression coverage;
-   preserve existing System Health behavior as closely as practical
    without a new dependency;
-   rebuild and redeploy the candidate;
-   resume the still-incomplete live Vike qualification for BTC/USD and
    ETH/USD;
-   determine and correct the Vike failure only if correction fits
    within these seven paths.

This authority does not authorize architectural redesign.

## System Health correction requirements

Replace the unsupported refresh behavior with functionality compatible
with the existing pinned Streamlit version and current architecture.

Prefer the simplest bounded behavior.

Do not:

-   add `streamlit-autorefresh` or another package;
-   upgrade Streamlit;
-   add JavaScript polling;
-   add a persistent companion process;
-   introduce high-frequency background polling;
-   create a new service;
-   expose internal exceptions.

If automatic refresh cannot be preserved safely with the pinned
dependency and existing primitives, a user-driven refresh mechanism is
acceptable provided System Health remains usable and truthful.

Any public failure must be converted to a controlled presentation state
without stack trace, source path, raw stderr, cache path, Worker path,
secret, or raw internal exception.

## Required focused tests

Use:

`python/presentation/test_realtime_financial_visualization.py`

to add behavioral regression coverage for the correction.

Tests must establish at least:

-   rendering System Health does not depend on unavailable
    `st.autorefresh`;
-   expected refresh/control behavior is compatible with the pinned
    Streamlit surface;
-   an internal System Health failure does not leak traceback/path/raw
    exception to the public presentation;
-   existing required System Health content remains available;
-   no regression to Market Research navigation/defaults caused by the
    correction.

Do not weaken existing tests.

## Historical Vike failure

The previous interactive validation also established:

-   BTC/USD / 1h / 30D -\> controlled historical-data-unavailable state;
-   ETH/USD -\> same unavailable state;
-   no candles;
-   no volume;
-   no UTC last-updated value.

After fixing System Health, continue diagnosis of the historical-data
path.

Trace:

`Streamlit -> bounded local Worker -> HistoricalQuery -> HistoricalMarketDataReadService -> cache -> Vike when governed -> safe JSON -> Streamlit`

Safely establish:

-   deployed candidate revision;
-   Worker DLL at `/app/worker/AIQuantTradingResearch.Worker.dll`;
-   `dotnet` available;
-   Worker executable by app user;
-   `/home/aiq-market-cache` exists and is writable by `aiq`;
-   `Vike__ApiKey` setting name/presence reaches the application without
    revealing its value;
-   configuration binding matches the expected environment-setting
    convention;
-   outbound Vike HTTPS/DNS viability;
-   Worker HistoricalQuery controlled result;
-   typed provider failure behavior;
-   one-shot Worker exit/reaping.

Diagnose before changing code.

## Vike correction boundary

If the Vike root cause can be corrected inside these seven paths without
changing provider/application contracts, dependencies, schema, or Azure
topology, correct it and revalidate.

If Vike correction requires any path outside the seven-path allowlist
--- for example a Vike adapter, Worker, Application contract/read
service, dependency injection source, or another
configuration/deployment source --- stop and report the exact path and
required correction.

Do not work around a provider-layer defect in the presentation layer.

Do not add a Python Vike client.

Do not bypass `HistoricalMarketDataReadService`.

Do not introduce Twelve Data as public fallback.

## Secret handling

`Vike__ApiKey` remains server-side secret configuration.

Never print, echo, retrieve into evidence, persist, commit, screenshot,
log, or disclose its value.

Never rotate, replace, delete, or invent it.

Use only safe setting-name/presence/redacted evidence.

## Retry-until-governance-boundary rule

Do not stop merely because an implementation, test, build, container,
deployment, cold start, Worker invocation, cache operation, Vike
request, HTTP request, validation, or tooling step fails.

Diagnose, correct within the seven-path authority, rerun relevant
checks, and continue until acceptance conditions pass.

Stop only when the next corrective action requires an eighth path or
another explicit governance expansion.

When blocked, report the exact restriction, failed evidence, root cause
if known, and required next action without performing it.

## Local/container validation

After correction, run/re-run as applicable:

-   Release build, target 0 warnings / 0 errors;
-   relevant .NET regression tests;
-   full Python presentation suite;
-   focused `test_realtime_financial_visualization.py`;
-   Python compile;
-   `pip check`;
-   PowerShell parser;
-   `git diff --check`;
-   secret scan;
-   no-bypass scan;
-   README unchanged;
-   dependencies unchanged;
-   schema unchanged;
-   Docker/container build;
-   Worker fixed-path proof;
-   `dotnet` proof;
-   entrypoint syntax;
-   cache ownership/write proof;
-   HistoricalQuery smoke;
-   process exit/reaping.

## Commit and PR rules

Commit corrections to the existing WP07 branch and push them to PR #304.

Record:

-   old head `5625f8a89cc3605fbfab6bc19b833fc7c0da9b72`;
-   new full candidate head;
-   exact changed paths versus canonical predecessor;
-   exact incremental changed paths introduced by this authority.

PR #304 remains Open/unmerged.

Do not merge.

## Deployment authority

After local/container validation succeeds, publish a new candidate image
through the existing governed GHCR mechanism and deploy only to the
existing App Service:

`aiqr112wp035ec325382770`

Record the new image tag and immutable digest.

Preserve:

-   existing Linux F1/Free plan;
-   HTTPS-only;
-   existing resource topology;
-   recurring infrastructure target `$0.00`.

Do not create or upgrade Azure resources.

## Interactive live requalification

After deployment and cold-start recovery, repeat browser-based
interactive qualification.

### Market Research

Require:

-   HTTPS app reachable;
-   Market Research default;
-   BTC/USD default;
-   1h default;
-   30D default;
-   BTC/USD candlesticks render;
-   BTC/USD volume renders;
-   ETH/USD candlesticks render;
-   ETH/USD volume renders;
-   supported interval controls work;
-   supported range controls work;
-   Vike provenance visible;
-   UTC last-updated visible;
-   no public Twelve Data fallback.

### Other surfaces

Require:

-   System Health renders without unsupported refresh error;
-   System Health exposes no traceback/internal path;
-   ML & Automation Studies renders;
-   Twelve Data boundary message renders;
-   no-trade footer renders.

### Public information safety

Browser-visible content must not expose:

-   `Vike__ApiKey` or another secret;
-   stack trace;
-   internal `/app/...` source path;
-   Worker filesystem path;
-   cache filesystem path;
-   raw stderr;
-   raw provider response;
-   raw internal exception.

## F1 invariants

Reconfirm:

-   Linux F1/Free unchanged;
-   no paid service/resource;
-   no new Azure resource;
-   no persistent companion service;
-   Worker remains one-shot;
-   cache remains governed and bounded;
-   no high-frequency polling;
-   no provider acquisition from chart hover/zoom/pan;
-   no unnecessary fanout;
-   zero recurring infrastructure cost target preserved.

## Protected and forbidden work

Do not modify root `README.md`.

Do not add/change dependencies.

Do not change schema.

Do not implement Release 1.14 indicators.

Do not implement ML, signals, forecasting, strategy, backtesting, or
trading.

Do not merge PR #304.

Do not close #294 or mark WP07 Done.

Do not begin WP08.

Do not close milestone #64.

Do not create a Release 1.13 tag or GitHub Release.

## Lifecycle state after PASS

A PASS creates a corrected, deployed, interactively qualified WP07
candidate only.

Required state:

-   PR #304 Open/unmerged;
-   issue #294 Open/non-Done;
-   issue #295 Open;
-   milestone #64 Open;
-   no Release 1.13 tag/GitHub Release.

The new head then proceeds to separate **GPT-5.6 Luna WP07 final
substantive acceptance**.

## Required final report

Return:

-   canonical predecessor;
-   PR #304 state;
-   old head;
-   new full head;
-   complete seven-path allowlist;
-   actual changed paths;
-   System Health root cause;
-   System Health correction;
-   focused regression tests/results;
-   Vike failure root cause;
-   whether Vike correction was possible inside seven paths;
-   safe Vike setting-presence evidence;
-   Worker/cache/process evidence;
-   build/test/compile/pip/PowerShell/whitespace/security results;
-   image tag;
-   immutable digest;
-   deployed target;
-   BTC/USD candle/volume live result;
-   ETH/USD candle/volume live result;
-   provenance/UTC result;
-   defaults/control matrix result;
-   System Health live result;
-   ML/Twelve Data/footer result;
-   public leakage result;
-   F1/zero-cost result;
-   README/dependency/schema status;
-   issue #294/Project status;
-   #295/milestone #64 status;
-   confirmation PR remains unmerged;
-   confirmation no tag/GitHub Release;
-   any remaining governance restriction;
-   next authority.

End with exactly one of:

`RELEASE 1.13 WP07 NARROW PRESENTATION DEFECT CORRECTION AND LIVE REQUALIFICATION: PASS`

or

`RELEASE 1.13 WP07 NARROW PRESENTATION DEFECT CORRECTION AND LIVE REQUALIFICATION: BLOCKED`

A PASS authorizes progression only to GPT-5.6 Luna WP07 final
substantive acceptance.

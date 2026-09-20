# Release 1.13 WP07 --- Terra Corrected Candidate Build, Deploy, and Live Requalification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this deployment and runtime requalification step.
GPT-5.6 Luna retains final substantive acceptance authority. GPT-5.6 Sol
may support analysis only.

## Bound state

Canonical Release 1.13 predecessor:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

WP07 PR:

`#304`

Branch:

`feature/release-1.13-wp07-azure-f1-validation`

Old deployed candidate:

`5625f8a89cc3605fbfab6bc19b833fc7c0da9b72`

Corrected candidate reported head:

`7eafa22`

Resolve and record the full immutable SHA for `7eafa22` before
deployment.

PR #304 must remain Open and unmerged.

## Completed correction

The System Health root cause was confirmed:

`streamlit==1.61.1` does not expose `st.autorefresh`.

The authorized correction:

-   removed unsupported automatic refresh;
-   retained the existing bounded user-driven `Refresh now` control;
-   added regression coverage using a Streamlit double without
    `autorefresh`;
-   preserved controlled System Health output.

Reported local evidence:

-   focused tests: 12/12 passed;
-   Python compile: passed;
-   whitespace: passed;
-   no dependency/schema/README/secret/Azure-resource/plan/lifecycle
    mutation.

Do not reopen this design unless deployment/runtime evidence
demonstrates a defect.

## Seven-path boundary

The complete WP07 allowlist remains exactly:

1.  `container/entrypoint.sh`
2.  `eng/azure-cli/r1.13-deployment/wp07-azure-f1/validate-wp07-container.ps1`
3.  `docs/roadmap/release-1.13/WP07_AZURE_F1_VALIDATION.md`
4.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
5.  `docs/roadmap/release-1.13/prompters/release-1.13-wp07-terra-azure-f1-integration-stability-security-zero-cost-validation.md`
6.  `python/presentation/realtime_financial_visualization.py`
7.  `python/presentation/test_realtime_financial_visualization.py`

No eighth tracked path is authorized.

This authority is primarily build, publish, deployment, diagnosis, and
runtime validation. Do not mutate source unless a technical defect is
found and its correction fits inside these seven paths.

## Pre-deployment integrity gate

Before building:

-   resolve `7eafa22` to full SHA;
-   verify it is the current PR #304 head;
-   verify PR #304 is Open, non-draft, and unmerged;
-   verify exact changed paths against canonical predecessor are within
    the seven-path allowlist;
-   verify the incremental correction is limited to the two presentation
    paths;
-   verify README unchanged;
-   verify dependency pins unchanged;
-   verify schema unchanged;
-   verify issue #294 remains Open/non-Done;
-   verify #295 Open;
-   verify milestone #64 Open;
-   verify no Release 1.13 tag/GitHub Release.

Stop if candidate drift exceeds authority.

## Build and local/container qualification

Build the corrected candidate from the exact full PR head.

Run/reconfirm:

-   Release build with target 0 warnings / 0 errors;
-   relevant .NET regression tests;
-   full Python presentation suite;
-   focused System Health tests, including the no-`autorefresh`
    regression;
-   Python compile;
-   `pip check`;
-   PowerShell parser;
-   `git diff --check`;
-   secret scan;
-   no-bypass scan;
-   Docker/container build;
-   `/app/worker/AIQuantTradingResearch.Worker.dll` exists;
-   `dotnet` available;
-   entrypoint syntax valid;
-   `/home/aiq-market-cache` ownership/write behavior for `aiq`;
-   one-shot HistoricalQuery smoke;
-   Worker process exits/reaps.

Do not weaken tests.

## GHCR publication

Publish a new candidate image from the exact corrected full head using
the existing governed public/free GHCR pattern.

Use a candidate-specific tag tied to the corrected SHA.

Record:

-   exact full Git SHA;
-   image tag;
-   immutable digest.

Do not overwrite evidence by representing the old digest as the
corrected candidate.

## Deployment

Deploy the newly published corrected image only to the existing App
Service:

`aiqr112wp035ec325382770`

Preserve:

-   existing Linux F1/Free topology;
-   HTTPS-only;
-   existing Azure resources;
-   recurring infrastructure target `$0.00`.

Do not:

-   create resources;
-   upgrade plan/tier;
-   add services;
-   create slots;
-   change DNS;
-   merge PR #304.

Allow for ordinary F1 cold start and retry within bounded validation.

## Secret boundary

`Vike__ApiKey` is already governed server-side configuration.

Never print, echo, retrieve into evidence, persist, commit, screenshot,
log, rotate, replace, or disclose its value.

Safe name/presence/redacted evidence is permitted only where necessary.

## Live System Health requalification

After the corrected deployment is running, interactively validate System
Health.

Require:

-   System Health screen renders;
-   user-driven `Refresh now` behavior is available/usable as designed;
-   no `st.autorefresh` failure;
-   no traceback;
-   no `/app/...` internal path;
-   no raw exception;
-   no Worker/cache path leakage;
-   no secret leakage.

## Live historical-market requalification

Interactively exercise the public Market Research UI.

Require:

-   HTTPS app reachable;
-   Market Research default;
-   BTC/USD default;
-   1h default;
-   30D default;
-   BTC/USD historical candlesticks render;
-   BTC/USD volume renders;
-   ETH/USD historical candlesticks render;
-   ETH/USD volume renders;
-   supported interval controls work;
-   supported range controls work;
-   Vike provenance visible;
-   UTC last-updated visible;
-   no public Twelve Data fallback.

Also verify:

-   ML & Automation Studies renders;
-   Twelve Data boundary messaging renders;
-   no-trade footer renders.

## Vike failure diagnosis

If BTC/USD or ETH/USD remains unavailable after the corrected
deployment, diagnose the deployed path before any source mutation:

`Streamlit -> bounded local Worker -> HistoricalQuery -> HistoricalMarketDataReadService -> cache -> Vike when governed -> safe JSON -> Streamlit`

Safely establish:

-   corrected image/revision is actually running;
-   Worker DLL fixed path;
-   `dotnet`;
-   app-user Worker execution;
-   cache existence/write permission;
-   safe `Vike__ApiKey` presence/configuration binding evidence without
    value;
-   outbound HTTPS/DNS viability;
-   one-shot HistoricalQuery result;
-   typed provider failure category;
-   process exit/reaping.

If the root cause is operational/transient and can be corrected without
source or governance expansion, correct and retry.

If correction requires an eighth tracked path --- including Vike
adapter, Worker, DI, Application contracts/read service, or other source
--- stop at governance and identify the exact required path/action.

Do not patch a provider defect in presentation code.

Do not create a Python Vike client.

Do not bypass the read service.

Do not add Twelve Data public fallback.

## Retry-until-governance-boundary rule

Do not stop merely because a build, test, image publication, deployment,
cold start, Worker invocation, cache operation, Vike request, HTTP
request, UI session, or validation step fails.

Diagnose, correct/retry within this authority, and rerun relevant
checks.

Stop only when the next corrective action requires an eighth path or
another explicit governance expansion.

When blocked, report the exact restriction and required next action
without performing it.

## Public information-safety gate

The deployed browser surface must not expose:

-   `Vike__ApiKey` or any secret;
-   stack trace;
-   internal source path;
-   Worker filesystem path;
-   cache filesystem path;
-   raw stderr;
-   raw provider HTTP body;
-   raw internal exception.

## F1 boundedness gate

Reconfirm:

-   Linux F1/Free unchanged;
-   HTTPS-only;
-   no new Azure resource/service;
-   no paid-resource mutation;
-   no persistent companion service;
-   Worker remains one-shot;
-   cache remains bounded/persistent under governed `/home`;
-   no high-frequency background polling;
-   removal of unsupported autorefresh does not introduce replacement
    polling;
-   no provider call from hover/zoom/pan;
-   no unnecessary fanout;
-   recurring infrastructure target remains `$0.00`.

## Forbidden work

Do not:

-   modify an eighth tracked path;
-   modify root README;
-   add/change dependencies;
-   change schema;
-   change provider/application architecture;
-   implement Release 1.14 indicators;
-   implement ML/signals/forecasting/strategy/backtesting/trading;
-   merge PR #304;
-   close #294;
-   mark WP07 Done;
-   begin WP08;
-   close milestone #64;
-   create a Release 1.13 tag;
-   create a GitHub Release.

## PASS boundary

A PASS means the corrected candidate has been:

-   built from the exact corrected PR head;
-   published immutably;
-   deployed to the existing F1 app;
-   locally/container validated;
-   interactively validated for System Health;
-   interactively validated for BTC/USD and ETH/USD historical
    visualization;
-   validated for public information safety and F1 boundedness.

PR #304 remains Open/unmerged and #294 remains Open/non-Done.

A PASS advances only to **GPT-5.6 Luna WP07 final substantive
acceptance**.

## Required final report

Return:

-   canonical predecessor;
-   PR #304 state;
-   corrected full head SHA;
-   seven-path allowlist;
-   exact changed paths;
-   local/container validation results;
-   image tag;
-   immutable digest;
-   deployed target/revision;
-   cold-start/HTTPS result;
-   System Health live result;
-   `Refresh now` result;
-   traceback/internal-path leakage result;
-   BTC/USD candle + volume result;
-   ETH/USD candle + volume result;
-   Vike provenance result;
-   UTC last-updated result;
-   defaults/control matrix result;
-   ML/Twelve Data/footer result;
-   safe secret-presence evidence;
-   Worker/cache/process evidence;
-   F1/zero-cost evidence;
-   README/dependency/schema status;
-   issue #294/Project status;
-   #295/milestone #64 status;
-   PR still unmerged;
-   no tag/GitHub Release;
-   any remaining governance restriction;
-   next authority.

End with exactly one of:

`RELEASE 1.13 WP07 CORRECTED CANDIDATE BUILD DEPLOY AND LIVE REQUALIFICATION: PASS`

or

`RELEASE 1.13 WP07 CORRECTED CANDIDATE BUILD DEPLOY AND LIVE REQUALIFICATION: BLOCKED`

A PASS does not authorize merge or lifecycle completion.

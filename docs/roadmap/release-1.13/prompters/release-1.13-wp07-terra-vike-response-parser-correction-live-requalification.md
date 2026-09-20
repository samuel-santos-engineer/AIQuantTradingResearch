# Release 1.13 WP07 --- Terra Vike Response Parser Correction and Live Requalification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this narrow provider-adapter correction, regression
validation, candidate rebuild/deployment, and live requalification.
GPT-5.6 Luna retains final substantive acceptance authority. GPT-5.6 Sol
may support analysis only.

## Bound state

Canonical predecessor:

`f4a2c20364b96d0861d4a03ffe6fb5cffe07fb18`

WP07 PR:

`#304`

Current candidate:

`7eafa22e890b120278187c6e9f06033f1038f6ac`

Current deployed digest:

`sha256:83fde5ebda8135bbe654293257270c1970130982cd19bc05f17d2f9df0da3e2c`

The prior local real-credential differential diagnosis established,
using fresh isolated caches on both host and the exact Linux candidate
container:

-   BTC/USD 1h 30D -\> Worker exit 0, `Unavailable`, typed
    `MalformedResponse`;
-   ETH/USD 1h 30D -\> Worker exit 0, `Unavailable`, typed
    `MalformedResponse`;
-   the real credential was consumed without being displayed;
-   the same result occurs outside Azure and inside the exact candidate
    container;
-   `MalformedResponse` occurs only after a successful HTTP response
    reaches `VikeHistoricalMarketDataProvider.ParseResponse`.

Therefore the primary defect is localized to Vike response
parsing/validation, not Azure-only configuration delivery, credential
rejection, cache permissions, app-user permissions, or container
runtime.

## Purpose

Correct the Vike historical OHLCV response parser to accept the **actual
documented/observed Vike response shape** while preserving canonical
candle validation and all existing security/provider boundaries.

Then:

1.  prove BTC/USD and ETH/USD succeed locally with the real credential
    through the governed Worker path using fresh isolated caches;
2.  rebuild the candidate;
3.  publish a new immutable GHCR image;
4.  deploy it to the existing F1 App Service;
5.  interactively requalify the public UI;
6.  leave PR #304 unmerged for separate Luna final substantive
    acceptance.

## Literal mutation allowlist

This authority adds exactly two provider paths to the already accepted
WP07 candidate scope.

The **only newly authorized source/test mutations** are:

1.  `src/AIQuantTradingResearch.Infrastructure/MarketData/Vike/VikeHistoricalMarketDataProvider.cs`
2.  `tests/AIQuantTradingResearch.Infrastructure.Tests/VikeHistoricalMarketDataProviderTests.cs`

Existing WP07 candidate paths may remain as already committed, but do
not modify them unless strictly necessary to update the existing WP07
evidence/manifest within their prior authority.

No additional implementation/test path is authorized.

If the parser correction requires another source path, dependency,
contract, DI change, Worker change, schema change, or presentation
workaround, stop at governance.

## Diagnose exact response shape before coding

Do not loosen the parser speculatively.

Using Vike first-party OHLCV documentation and the already governed
real-credential path, determine the response structure needed for the
correction.

You may inspect a real successful response **only in the narrowest safe
way necessary to understand its structural schema**.

Never persist or publish raw provider payloads unnecessarily.

Do not include the API key, request headers, secrets, or large raw
market-data payloads in logs, commits, Markdown, or final evidence.

Prefer structural evidence such as:

-   top-level JSON kind/properties;
-   candle container property name/kind;
-   one redacted/synthetic representative candle shape;
-   timestamp/value representation types;
-   pagination metadata shape if relevant.

If first-party documentation fully establishes the correct shape, prefer
it over retaining raw response material.

## Parser correction requirements

The corrected adapter must:

-   parse the actual Vike OHLCV response shape used by the governed
    endpoint;
-   preserve BTC and ETH canonical mapping;
-   preserve 1h/4h/1d interval behavior;
-   preserve `include_partial=false` or equivalent closed-candle
    behavior already governed;
-   preserve decimal precision;
-   preserve UTC candle-open semantics;
-   return strict ascending canonical candles;
-   reject duplicate timestamps;
-   reject malformed/missing required OHLCV values;
-   reject inconsistent candle structures;
-   preserve typed failure mapping;
-   preserve cancellation/deadline behavior;
-   preserve provider provenance;
-   preserve safe handling of pagination/cursor semantics under the
    existing bounded request contract;
-   never expose raw provider payloads to the UI.

Do not weaken validation merely to make the live response pass.

## Regression tests

Strengthen `VikeHistoricalMarketDataProviderTests.cs` with
representative fixtures matching the real/documented response schema.

Cover at least:

-   valid BTC response;
-   valid ETH response where useful;
-   expected timestamp representation;
-   expected OHLCV numeric representation;
-   malformed top-level shape;
-   missing candle collection;
-   malformed candle;
-   missing required field/value;
-   duplicate/nonascending timestamp behavior as applicable;
-   unexpected pagination/cursor behavior under the current one-request
    contract;
-   authentication/non-success mapping remains unchanged;
-   cancellation/deadline behavior remains unchanged where existing
    tests cover it.

Fixtures must contain synthetic/non-sensitive market values where
practical.

Do not commit a captured credential or sensitive raw response.

## Local real-credential proof

After correction, rerun the real-credential differential test with a
**fresh isolated cache**.

Use the existing governed path:

`HistoricalQuery -> HistoricalMarketDataReadService -> VikeHistoricalMarketDataProvider`

Require:

-   BTC/USD / 1h / 30D -\> success with canonical candles;
-   ETH/USD / 1h / 30D -\> success with canonical candles;
-   provenance = Vike;
-   Worker exit 0;
-   provider-backed acquisition is demonstrated rather than an old-cache
    hit.

The application may consume `Vike__ApiKey`; Codex must never display,
echo, hash, serialize, log, persist, or inspect its value.

## Full regression gate

Run/re-run:

-   Release build, target 0 warnings / 0 errors;
-   Domain tests;
-   Application tests;
-   Architecture tests;
-   full Infrastructure tests or the strongest repository-native
    equivalent;
-   focused Vike provider tests;
-   Python presentation suite;
-   Python compile;
-   `pip check`;
-   PowerShell parse;
-   `git diff --check`;
-   candidate-diff secret scan;
-   no-bypass checks;
-   README unchanged;
-   dependencies unchanged;
-   schema unchanged.

Preserve the already corrected System Health behavior.

## Candidate/PR handling

Commit the provider parser correction to the existing WP07 branch and PR
#304.

Record:

-   old head `7eafa22e890b120278187c6e9f06033f1038f6ac`;
-   new full head;
-   exact incremental paths;
-   complete candidate changed-path set versus canonical predecessor.

PR #304 must remain Open and unmerged.

## Build, publish, and deploy

After local proof and regressions pass:

1.  build the exact corrected head;
2.  publish a candidate-specific image through the existing public/free
    GHCR mechanism;
3.  record immutable digest;
4.  deploy only to existing App Service `aiqr112wp035ec325382770`;
5.  preserve Linux F1/Free, HTTPS-only, existing topology, and `$0.00`
    recurring-infrastructure target.

Do not create or upgrade Azure resources.

## Interactive live requalification

After cold-start recovery, validate the rendered Streamlit application.

Require all of:

-   HTTPS app reachable;
-   Market Research default;
-   BTC/USD default;
-   1h default;
-   30D default;
-   BTC/USD candlesticks render;
-   BTC/USD volume renders;
-   ETH/USD candlesticks render;
-   ETH/USD volume renders;
-   alternate supported interval/range controls work;
-   Vike provenance visible;
-   UTC last-updated visible;
-   System Health renders cleanly;
-   `Refresh now` remains controlled;
-   ML & Automation Studies renders;
-   Twelve Data private/internal boundary renders;
-   no-trade footer renders.

## Public information-safety gate

The deployed UI must not expose:

-   `Vike__ApiKey` or any secret;
-   raw provider response;
-   traceback;
-   internal `/app/...` path;
-   Worker path;
-   cache path;
-   raw stderr;
-   raw internal exception.

## F1 boundedness

Reconfirm:

-   Linux F1/Free unchanged;
-   no paid/new Azure service;
-   Worker remains one-shot;
-   cache remains bounded under governed `/home`;
-   no persistent companion;
-   no high-frequency polling;
-   no provider call caused merely by chart hover/zoom/pan;
-   no unnecessary provider/process fanout.

## Retry-until-governance-boundary rule

Do not stop merely because implementation, tests, parsing, local Vike
acquisition, container build, image publication, deployment, cold start,
or interactive validation initially fails.

Diagnose and correct within the authorized provider/test paths and
existing WP07 deployment authority, rerun checks, and continue until
acceptance conditions pass.

Stop only if the next correction requires another source/test path,
dependency, schema, architecture/provider-contract expansion, secret
exposure, new Azure resource, or another explicit governance boundary.

## Forbidden work

Do not:

-   modify root README;
-   add/change dependencies;
-   change schema;
-   redesign the provider/application contract;
-   add a Python Vike client;
-   add Twelve Data public fallback;
-   bypass `HistoricalMarketDataReadService`;
-   expose/rotate/replace the credential;
-   implement indicators/ML/signals/forecasting/backtesting/trading;
-   merge PR #304;
-   close #294;
-   mark WP07 Done;
-   begin WP08;
-   close milestone #64;
-   create a Release 1.13 tag or GitHub Release.

## PASS boundary

A PASS requires:

-   evidence-backed parser correction;
-   regression coverage;
-   real-credential fresh-cache local BTC and ETH success;
-   clean full regression suite;
-   new immutable candidate image;
-   deployment to existing F1 app;
-   interactive BTC and ETH candle/volume success;
-   Vike provenance and UTC freshness;
-   clean System Health;
-   public information-safety PASS;
-   F1/zero-cost invariants preserved.

PR #304 remains Open/unmerged and issue #294 remains Open/non-Done.

A PASS advances only to **GPT-5.6 Luna WP07 final substantive
acceptance**.

## Required final report

Return:

-   canonical predecessor;
-   old head;
-   new full head;
-   exact incremental paths;
-   complete candidate changed paths;
-   documented/observed response-shape finding;
-   parser root cause;
-   parser correction;
-   focused Vike test results;
-   full .NET/Python regression results;
-   real-credential fresh-cache BTC result;
-   real-credential fresh-cache ETH result;
-   confirmation secret value never displayed;
-   image tag;
-   immutable digest;
-   deployed target;
-   HTTPS/cold-start result;
-   live BTC candles/volume result;
-   live ETH candles/volume result;
-   Vike provenance/UTC result;
-   controls/defaults result;
-   System Health/Refresh result;
-   ML/Twelve Data/footer result;
-   public leakage result;
-   F1/zero-cost result;
-   README/dependency/schema status;
-   PR #304 state;
-   #294/#295/milestone #64 state;
-   no tag/GitHub Release;
-   any remaining governance restriction;
-   next authority.

End with exactly one of:

`RELEASE 1.13 WP07 VIKE RESPONSE PARSER CORRECTION AND LIVE REQUALIFICATION: PASS`

or

`RELEASE 1.13 WP07 VIKE RESPONSE PARSER CORRECTION AND LIVE REQUALIFICATION: BLOCKED`

A PASS does not authorize merge or lifecycle completion.

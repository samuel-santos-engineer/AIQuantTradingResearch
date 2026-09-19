# Release 1.13 WP06 --- Terra Public Research Messaging, Informational Surfaces, and Controlled States Implementation Authority

## Selected model

**Execution model: GPT-5.6 Terra**

-   **GPT-5.6 Luna** owns the frozen Release 1.13 contract and later
    substantive acceptance.
-   **GPT-5.6 Terra** owns implementation under this bounded authority.
-   **GPT-5.6 Sol** may support analysis only and may not alter
    governance or expand scope.

## Canonical predecessor

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Required canonical `origin/main` before implementation:

`79486313f3e01a5004e0d983e95b57ce8020926a`

This SHA contains the completed WP05 historical bridge and Market
Research UI.

Expected lifecycle state:

-   WP01-WP05 accepted, merged, lifecycle-complete;
-   issue #292 Closed;
-   Project WP05 Done;
-   issue #293 Open;
-   Project WP06 non-Done;
-   milestone #64 Open;
-   issues #294-#295 Open;
-   no Release 1.13 tag or GitHub Release.

Verify live truth before mutation.

## Work package

**WP06 --- Public research messaging, provenance, ML & Automation
Studies informational surface, System Health relationship, and
controlled public states**

Canonical execution-plan purpose:

> Add provenance, Twelve Data research-boundary messaging, ML &
> Automation Studies informational surface, System Health relationship,
> controlled public states.

WP06 is presentation/information work. It does not change historical
acquisition architecture.

## Purpose

Complete the public-facing Release 1.13 presentation contract around the
WP05 functional Market Research surface.

Implement:

-   final visible Vike provenance;
-   final last-updated presentation;
-   explicit Twelve Data public/private research boundary;
-   ML & Automation Studies informational surface;
-   System Health relationship/navigation consistency;
-   public-safe controlled states;
-   research/no-trade footer;
-   first-screen wording consistent with WP01.

Do not implement new market-data acquisition, ML, indicators,
strategies, deployment, or release lifecycle.

## Mandatory inspection before mutation

Inspect at minimum:

1.  `docs/roadmap/release-1.13/RELEASE_1.13_WP01_CONTRACT.md`;
2.  `docs/roadmap/release-1.13/RELEASE_1.13_EXECUTION_PLAN.md`;
3.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`;
4.  canonical WP05 bridge architecture;
5.  `python/presentation/realtime_financial_visualization.py`;
6.  `python/presentation/historical_market_bridge.py`;
7.  WP05 presentation tests;
8.  existing legacy/System Health presentation tests;
9.  current navigation and Streamlit state behavior;
10. issue #293 / Project / milestone state.

Record findings.

## Exact-literal allowlist gate

**Before the first edit**, report and freeze the smallest exact literal
mutation allowlist.

No directory wildcard is permitted.

Expected categories, not automatic authorization:

-   `python/presentation/realtime_financial_visualization.py`;
-   focused WP06 presentation tests, preferably a new or existing exact
    test file;
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`;
-   this WP06 Terra authority record.

Do not modify bridge/provider/cache/Worker/dependency code unless a true
governance issue is discovered; if so, stop rather than broadening WP06.

`requirements.txt` is not expected to change.

## Frozen first-screen contract

The public first screen remains **Market Research**.

Required high-level presentation:

-   application identity: `AI Quant Trading Research`;
-   context: `Historical Market Research`;
-   navigation:
    -   Market Research
    -   ML & Automation Studies
    -   System Health
-   market controls from WP05 remain unchanged;
-   historical candlestick + volume remains unchanged;
-   research/no-trade boundary remains visible.

Do not redesign the functional WP05 chart/control architecture.

## Market Research provenance

For usable historical data, present:

`Data source: Vike • Historical OHLCV • Last updated: <UTC timestamp>`

Requirements:

-   source must be truthful;
-   timestamp must come from governed bridge metadata;
-   timestamp visibly communicates UTC;
-   do not expose provider URL, API key, request details, cache path, or
    internal timestamps not needed publicly;
-   stale data remains usable when governed by WP04/WP05 and must not be
    falsely described as freshly acquired.

If stale-state wording is shown, keep it concise and truthful.

## Research Data Architecture messaging

Add the accepted public explanation:

`Public historical visualization uses Vike market data.`

`Twelve Data is retained for private/internal research, including real-time market-data studies. Twelve Data values from that research are not exposed through this public interface under the project's current data-access/licensing boundary.`

Preserve the intended architectural distinction:

-   Vike: public historical visualization;
-   Twelve Data: private/internal research;
-   no anonymous public Twelve Data fallback.

Do not make new legal claims beyond the accepted project wording.

Do not claim all Vike API responses are CC BY 4.0.

## ML & Automation Studies informational surface

Implement the accepted informational-only surface substantially
equivalent to:

`ML & Automation Studies`

`AI Quant Trading Research is evolving from historical market-data visualization toward reproducible quantitative research and machine-learning evaluation.`

Research roadmap:

`Historical Market Data` `↓` `Feature Engineering` `↓`
`Machine Learning Evaluation       Release 2.0+` `↓`
`Strategy / Signal Research` `↓` `Governed Automation Studies`

Current status:

-   Historical visualization --- Release 1.13
-   ML evaluation --- Begins Release 2.0
-   Automated trading --- Not enabled

Required final statement:

`This environment does not execute trades.`

This is informational only.

Do not add:

-   ML dependency;
-   model;
-   training;
-   feature engineering implementation;
-   prediction;
-   signal;
-   strategy;
-   backtest;
-   automation execution;
-   new Twelve Data acquisition.

## System Health relationship

Preserve System Health as a secondary navigation destination.

WP06 may improve headings/context so the relationship is understandable,
but must preserve required legacy operational information and tests.

Do not turn System Health into a new monitoring backend.

Do not add polling or external telemetry.

## Footer

Render:

`Research and demonstration application • No trade execution`

The footer should be visible across the main public presentation
surfaces where practical without causing duplicate/noisy rendering.

## Controlled public states

Verify and, where necessary, improve public presentation for:

### Fresh data

Chart usable; provenance shown.

### Stale usable data

Chart remains usable; truthful stale indication and last-updated
timestamp shown.

### Empty successful data

Controlled empty-state text, distinct from provider/bridge failure.

### Unavailable

Use exactly:

`Historical market data is temporarily unavailable. Please try again later.`

### Local bridge/malformed response failure

Public UI must use controlled safe text; never raw internals.

### System Health/legacy handoff unavailable

Preserve existing controlled behavior; do not expose stack traces or
internal paths.

## Public information prohibition

Never render:

-   stack traces;
-   exception messages;
-   provider HTTP response bodies;
-   provider URLs;
-   credentials;
-   API keys;
-   authorization headers;
-   cache/database filesystem paths;
-   Worker command details;
-   raw stderr;
-   internal configuration.

## WP05 behavior preservation

Do not regress:

-   default Market Research;
-   BTC/USD + ETH/USD only;
-   1h/4h/1d;
-   1D/7D/30D/90D;
-   BTC/USD / 1h / 30D defaults;
-   candlestick + volume;
-   OHLCV hover;
-   zoom/pan;
-   responsive chart;
-   transient bridge retry;
-   no provider selector;
-   no direct provider acquisition from Python/browser;
-   client-side chart interactions.

WP06 should add messaging, not alter acquisition semantics.

## Provider no-bypass boundary

Do not add Vike/Twelve Data network calls in presentation code.

Provider names may appear only in approved explanatory/provenance text.

No API keys in Python/browser.

WP04/WP05 remain the sole historical acquisition path.

## Dependency boundary

No dependency changes are authorized.

Preserve exactly the current Python pins, including:

-   numpy==2.5.1
-   pandas==3.0.5
-   scikit-learn==1.9.0
-   streamlit==1.61.1
-   plotly==7.1.0

If WP06 appears to require a new package, stop at governance.

## Required tests

Add/update focused tests proving:

-   Market Research remains default;
-   all three navigation labels remain;
-   Vike provenance line is rendered for usable data;
-   UTC last-updated information is represented;
-   stale state remains truthful;
-   accepted Twelve Data research-boundary text is present;
-   ML & Automation Studies content is informational only;
-   Release 2.0+ / begins Release 2.0 direction is represented
    consistently;
-   automated trading is explicitly not enabled;
-   `This environment does not execute trades.` is present;
-   footer exactly communicates research/demonstration and no trade
    execution;
-   exact unavailable text remains;
-   empty state remains distinct;
-   no raw internal errors;
-   System Health remains reachable;
-   legacy presentation regression passes;
-   no provider acquisition exists in presentation code;
-   no indicators/ML/trading controls were introduced.

Prefer semantic tests over brittle full-page snapshots.

## Documentation and authority record

Update the Release 1.13 file manifest only as necessary to record the
exact WP06 implementation set.

If repository precedent commits the executed authority, use:

`docs/roadmap/release-1.13/prompters/release-1.13-wp06-terra-public-research-messaging-informational-surfaces-controlled-states-implementation.md`

This Markdown file must explicitly state **GPT-5.6 Terra**.

## Root README protection

`README.md` remains byte-for-byte protected.

Do not modify it.

## Forbidden scope

Do not perform:

-   provider/cache/bridge redesign;
-   Vike adapter changes;
-   Twelve Data adapter changes;
-   Worker changes;
-   schema changes;
-   dependency changes;
-   Docker/Azure/GHCR/deployment;
-   technical indicators;
-   Release 1.14 features;
-   ML implementation;
-   trading execution;
-   WP07;
-   WP08;
-   tag;
-   GitHub Release;
-   milestone closure;
-   root README.

## Retry-until-governance-boundary rule

Do not stop merely because an implementation, test, lint, UI,
validation, or tooling step fails.

Diagnose the failure, correct it within the frozen exact allowlist,
rerun relevant checks, and continue until acceptance conditions pass.

Distinguish:

`TECHNICAL FAILURE — CONTINUE FIXING`

from:

`GOVERNANCE RESTRICTION — STOPPED`

Stop only if correction requires crossing authority, such as:

-   provider/bridge/cache/Worker mutation;
-   new dependency;
-   unexpected path that cannot be justified as the minimal WP06
    presentation set;
-   README;
-   schema;
-   deployment;
-   WP07/WP08;
-   Release 1.14/2.0 implementation;
-   merge/tag/release.

Report exact restriction and required authority.

## Git workflow

Create a dedicated WP06 branch from:

`79486313f3e01a5004e0d983e95b57ce8020926a`

Create a bounded PR against `main`.

Do not merge it.

Keep issue #293 Open/non-Done during implementation.

The candidate requires separate GPT-5.6 Luna substantive acceptance.

## Validation before PASS

At minimum run/report:

### Scope

-   predecessor SHA;
-   branch/base/head;
-   exact changed paths;
-   README unchanged;
-   `git diff --check`;
-   secret scan;
-   clean staging/working-state accounting.

### Python/presentation

-   focused WP06 tests;
-   full relevant presentation suite;
-   compile/import check;
-   no-bypass/static scan.

### Regression

-   WP05 bridge/UI tests;
-   legacy visualization/System Health tests;
-   confirm no dependency diff.

### Content

Verify exact or semantically frozen wording for: - Vike provenance; -
Twelve Data research boundary; - ML roadmap/status; - no-trade
statements; - unavailable state; - footer.

### Governance

Verify: - issue #293 Open; - Project WP06 non-Done; - milestone #64
Open; - #294-#295 Open; - no tag/GitHub Release; - no deployment
mutation.

## Candidate acceptance boundary

A PASS creates an **unmerged WP06 candidate PR only**.

It does not authorize merge.

Next sequence:

1.  GPT-5.6 Luna substantive WP06 acceptance bound to exact candidate
    head;
2.  after Luna PASS, separate Terra merge/post-merge lifecycle
    authority;
3.  only after WP06 lifecycle completion may WP07 begin.

## Required final report

Return:

-   canonical predecessor;
-   branch;
-   issue #293/Project state;
-   files inspected;
-   exact pre-mutation allowlist;
-   exact changed paths;
-   Market Research messaging changes;
-   provenance implementation;
-   Twelve Data boundary implementation;
-   ML & Automation Studies implementation;
-   System Health preservation;
-   controlled-state behavior;
-   footer/no-trade implementation;
-   dependency proof;
-   no-bypass/security proof;
-   Python/focused/regression test results;
-   whitespace/secret results;
-   README proof;
-   implementation head;
-   PR number;
-   confirmation PR unmerged;
-   milestone/#294-#295 states;
-   technical failures corrected;
-   governance restrictions;
-   next authority.

End with exactly one of:

`RELEASE 1.13 WP06 PUBLIC RESEARCH MESSAGING AND INFORMATIONAL SURFACES IMPLEMENTATION: PASS`

or

`RELEASE 1.13 WP06 PUBLIC RESEARCH MESSAGING AND INFORMATIONAL SURFACES IMPLEMENTATION: BLOCKED`

A PASS creates a candidate only and does not authorize merge, WP07,
deployment, tag, or release.

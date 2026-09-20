# Release 1.13 WP06 --- Luna Public Research Messaging and Informational Surfaces Final Substantive Acceptance

## Selected model

**Execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns substantive acceptance. GPT-5.6 Terra owns later
merge/lifecycle only under separate authority. GPT-5.6 Sol may support
analysis but may not override Luna.

## Acceptance target

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

PR: `#303 — Release 1.13 WP06: public research messaging`

Required canonical base:

`79486313f3e01a5004e0d983e95b57ce8020926a`

Expected branch:

`feature/release-1.13-wp06-public-research-messaging`

Initial candidate head:

`e3a5b2bc773f1fba64976dd960507e0b1886f001`

Issue #293 must remain Open/non-Done and PR #303 must remain unmerged
during acceptance.

## Independently verified candidate facts

GitHub inspection before creating this authority confirmed:

-   PR #303 is Open, clean/mergeable, non-draft, and unmerged.
-   Base is `main` at the required predecessor.
-   Head is `e3a5b2bc773f1fba64976dd960507e0b1886f001`.
-   Exactly four paths are changed.

Recheck live state during acceptance.

## Exact candidate path set

1.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
2.  `docs/roadmap/release-1.13/prompters/release-1.13-wp06-terra-public-research-messaging-informational-surfaces-controlled-states-implementation.md`
3.  `python/presentation/realtime_financial_visualization.py`
4.  `python/presentation/test_market_research_ui.py`

No fifth path is authorized by this acceptance.

## Purpose

Perform substantive, adversarial acceptance of WP06 against the frozen
WP01 UI contract and canonical WP05 behavior.

Do not accept merely because Terra reported PASS. Inspect the actual
implementation and tests, correct defects only within the four-path
scope, rerun validation, and establish a final Luna-accepted head.

## Mandatory inspection

Inspect:

-   WP01 contract;
-   Release 1.13 execution plan;
-   WP05 bridge architecture;
-   canonical WP05 implementation and tests;
-   all four candidate paths;
-   `historical_market_bridge.py`;
-   existing legacy/System Health presentation tests;
-   current requirements;
-   issue #293/Project/milestone state.

## Gate 1 --- Exact scope

Verify exactly four changed paths and no changes to:

-   README;
-   requirements/dependencies;
-   bridge;
-   Worker;
-   provider;
-   cache;
-   schema;
-   Docker;
-   Azure/GHCR/deployment.

## Gate 2 --- First-screen contract

Verify the public first screen remains Market Research and communicates:

-   `AI Quant Trading Research`;
-   `Historical Market Research`;
-   navigation exactly containing:
    -   Market Research
    -   ML & Automation Studies
    -   System Health.

Verify WP05 controls/chart/defaults are unchanged.

## Gate 3 --- Provenance correctness

For usable Vike data, verify the UI renders a truthful line equivalent
to:

`Data source: Vike • Historical OHLCV • Last updated: <UTC timestamp>`

The timestamp must originate from governed bridge metadata and visibly
communicate UTC.

Adversarially inspect whether appending `(UTC)` to an arbitrary bridge
string could mislabel a non-UTC timestamp. WP05 hardened bridge
validation should make this safe; verify that invariant rather than
assuming it.

For stale data, ensure the wording does not imply fresh acquisition.

## Gate 4 --- Twelve Data boundary

Verify the accepted wording is present and semantically unchanged:

`Public historical visualization uses Vike market data.`

`Twelve Data is retained for private/internal research, including real-time market-data studies. Twelve Data values from that research are not exposed through this public interface under the project's current data-access/licensing boundary.`

No new legal/license claim may be introduced.

Do not claim all Vike API responses are CC BY 4.0.

## Gate 5 --- ML & Automation Studies

Verify informational content covers:

-   historical market data;
-   feature engineering;
-   machine-learning evaluation at Release 2.0+;
-   strategy/signal research as roadmap only;
-   governed automation studies as roadmap only;
-   Historical visualization --- Release 1.13;
-   ML evaluation --- Begins Release 2.0;
-   Automated trading --- Not enabled;
-   `This environment does not execute trades.`

Ensure this surface contains no executable ML, signal, strategy,
trading, backtesting, automation, or acquisition behavior.

## Gate 6 --- Footer/no-trade boundary

Verify:

`Research and demonstration application • No trade execution`

appears consistently on the public surfaces required by the contract
without excessive duplication or omission.

Check early-return states, especially unavailable and empty Market
Research and unavailable System Health.

## Gate 7 --- Controlled states

Verify:

-   fresh -\> chart + provenance;
-   stale -\> chart + truthful stale indication + provenance;
-   empty -\> controlled distinct message;
-   unavailable -\> exact governed message:
    `Historical market data is temporarily unavailable. Please try again later.`
-   bridge/malformed failure -\> no raw internals;
-   System Health integrity failure -\> controlled safe text;
-   legacy handoff unavailable -\> controlled behavior.

No stack trace, exception text, filesystem path, provider URL, HTTP
body, raw stderr, credential, Worker command, or internal configuration
may be rendered.

## Gate 8 --- System Health preservation

Verify System Health remains reachable and required legacy operational
information remains intact.

The candidate changes `FrameIntegrityError` rendering from detailed
exception text to controlled public text. Confirm this improves public
safety without destroying necessary internal diagnostics elsewhere.

No new polling or telemetry.

## Gate 9 --- WP05 regression

Verify no regression to:

-   BTC/USD and ETH/USD only;
-   1h/4h/1d;
-   1D/7D/30D/90D;
-   BTC/USD / 1h / 30D defaults;
-   candlestick + volume;
-   hover;
-   zoom/pan;
-   responsive chart;
-   retry behavior;
-   no provider selector;
-   no direct provider calls;
-   unchanged-selection behavior.

## Gate 10 --- Test quality

The candidate reportedly passes 35 Python presentation tests and 20
focused WP06/legacy tests.

Inspect test quality, not just counts.

The new WP06 test currently relies partly on `inspect.getsource(ui)` and
string-presence assertions. Determine whether this is sufficient to
prove rendered behavior. Prefer focused behavioral tests of rendering
helpers/surfaces where practical.

Correct brittle or insufficient tests within the existing test path if
they could allow a substantive UI-contract regression to pass unnoticed.

At minimum prove:

-   provenance formatting behavior;
-   accepted research-boundary text;
-   ML surface content;
-   footer behavior;
-   stale/empty/unavailable messages;
-   safe System Health error behavior;
-   navigation/default preservation;
-   no provider acquisition.

## Gate 11 --- No-bypass/security

Search presentation code for:

-   HTTP/provider acquisition;
-   Vike/Twelve Data URLs;
-   API-key access;
-   raw errors;
-   internal paths;
-   forbidden trading/ML controls.

Provider names are permitted only for approved display/explanatory text.

## Gate 12 --- Dependencies

Verify `requirements.txt` and package declarations are unchanged.

Expected Python pins remain:

-   numpy==2.5.1
-   pandas==3.0.5
-   scikit-learn==1.9.0
-   streamlit==1.61.1
-   plotly==7.1.0

No dependency addition is authorized.

## Correction authority

Luna may correct substantive WP06 defects only within the exact four
candidate paths.

If corrections are made:

-   remain on PR #303 branch;
-   commit/push only the four paths;
-   record the new head;
-   rerun complete acceptance;
-   use the corrected head as the sole final Luna-accepted SHA.

Report:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

when applicable.

## Governance stop conditions

Stop with:

`GOVERNANCE RESTRICTION — STOPPED`

only if correction requires:

-   a fifth path;
-   bridge/Worker/provider/cache mutation;
-   dependency change;
-   schema;
-   Docker/Azure/GHCR/deployment;
-   README;
-   Release 1.14 implementation;
-   Release 2.0 implementation;
-   WP07/WP08;
-   merge/tag/release;
-   closing #293 during acceptance.

## Retry-until-governance-boundary rule

Do not stop for ordinary test, lint, rendering, wording, validation, or
tooling failures. Correct within the exact four paths, rerun, and
continue until acceptance passes or a true governance restriction is
reached.

## Validation before PASS

Run/report at minimum:

-   live PR base/head/state;
-   exact four paths;
-   final accepted head;
-   focused WP06 tests;
-   full Python presentation suite;
-   legacy/System Health regression;
-   WP05 bridge/UI regression;
-   Python compile/import check;
-   dependency unchanged proof;
-   no-bypass scan;
-   secret/public-information scan;
-   forbidden-control scan;
-   `git diff --check`;
-   README unchanged;
-   issue #293 Open/non-Done;
-   milestone #64 Open;
-   #294-#295 Open;
-   no Release 1.13 tag/GitHub Release;
-   PR #303 unmerged.

## Lifecycle boundary

A PASS accepts the WP06 candidate only.

It does not authorize merge.

After PASS, create a separate **GPT-5.6 Terra PR #303 merge/post-merge
lifecycle authority** bound to the final Luna-accepted head.

Only after WP06 lifecycle completion may WP07 begin.

## Required final report

Return:

-   initial head;
-   final accepted head;
-   exact paths;
-   files inspected;
-   first-screen/navigation finding;
-   provenance/UTC finding;
-   Twelve Data boundary finding;
-   ML informational-surface finding;
-   footer finding;
-   controlled-state finding;
-   System Health finding;
-   WP05 regression finding;
-   test-quality review and corrections;
-   dependency/no-bypass/security findings;
-   test results;
-   whitespace/README proof;
-   issue/milestone states;
-   PR state;
-   corrections;
-   governance restrictions;
-   next authority.

End with exactly one of:

`RELEASE 1.13 WP06 PUBLIC RESEARCH MESSAGING AND INFORMATIONAL SURFACES FINAL SUBSTANTIVE ACCEPTANCE: PASS`

or

`RELEASE 1.13 WP06 PUBLIC RESEARCH MESSAGING AND INFORMATIONAL SURFACES FINAL SUBSTANTIVE ACCEPTANCE: BLOCKED`

A PASS does not authorize merge, WP07, deployment, tag, milestone
closure, or release.

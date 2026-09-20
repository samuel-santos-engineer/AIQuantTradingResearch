# Release 1.13 WP05 --- Luna Historical Bridge and Public Market Research UI Final Substantive Acceptance

## Selected model

**Execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns substantive acceptance. GPT-5.6 Terra owns later
merge/lifecycle only under separate authority. GPT-5.6 Sol may support
analysis but may not override Luna.

## Acceptance target

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

PR: `#302 — Release 1.13 WP05: historical market research bridge`

Canonical predecessor/base:

`10b3c006be16bb4a26bbf41a2a8e295984fd3c05`

Expected branch:

`feature/release-1.13-wp05-historical-market-research`

Initial candidate head:

`3ef552be3b5d928aba212577bccc63d2aa0cf673`

Issue #292 must remain Open/non-Done. Milestone #64 must remain Open. PR
must remain unmerged throughout substantive acceptance.

## Independently verified candidate facts

GitHub inspection before creating this authority confirmed:

-   PR #302 is Open, non-draft, unmerged, and currently mergeable.
-   Base is `main` at `10b3c006be16bb4a26bbf41a2a8e295984fd3c05`.
-   Head is `3ef552be3b5d928aba212577bccc63d2aa0cf673`.
-   The PR contains exactly 13 changed paths matching the bounded
    implementation set below.

Do not rely on these observations instead of rechecking live state
during acceptance.

## Exact candidate path set

1.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
2.  `docs/roadmap/release-1.13/prompters/release-1.13-wp05-terra-historical-bridge-public-market-research-ui-implementation.md`
3.  `python/presentation/historical_market_bridge.py`
4.  `python/presentation/realtime_financial_visualization.py`
5.  `python/presentation/test_historical_market_bridge.py`
6.  `python/presentation/test_market_research_ui.py`
7.  `requirements.txt`
8.  `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs`
9.  `src/AIQuantTradingResearch.Infrastructure/MarketData/Cache/AtomicFileHistoricalMarketDataQueryLock.cs`
10. `src/AIQuantTradingResearch.Worker/HistoricalMarketDataQueryExecution.cs`
11. `src/AIQuantTradingResearch.Worker/Program.cs`
12. `tests/AIQuantTradingResearch.Infrastructure.Tests/AtomicFileHistoricalMarketDataQueryLockTests.cs`
13. `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalMarketDataQueryExecutionTests.cs`

No fourteenth path is authorized by this acceptance.

## Purpose

Perform adversarial substantive acceptance of the actual WP05
implementation against the canonical ADR and WP01-WP04 contracts.

Do not accept based on Terra's PASS report alone.

Inspect implementation and tests, run validation, correct defects only
within the exact 13-path scope, and establish a final Luna-accepted
head.

## Mandatory inspection

Inspect the PR diff and relevant canonical files, including:

-   `RELEASE_1.13_WP05_HISTORICAL_BRIDGE_ARCHITECTURE.md`;
-   WP01 contract;
-   WP02 historical contracts;
-   WP04 cache/read service;
-   atomic historical cache;
-   Vike adapter/configuration;
-   all 13 changed paths;
-   Worker project/composition and legacy invocation;
-   existing presentation/read-model code and regression tests;
-   Dockerfile/runtime packaging;
-   architecture/no-bypass tests;
-   requirements and Python version assumptions.

## Gate 1 --- Scope and architecture

Prove the implementation is the accepted additive one-shot Worker/stdio
mechanism:

`Streamlit -> fixed local Worker invocation -> HistoricalQuery mode -> HistoricalMarketDataReadService -> cache -> Vike when governed -> safe JSON -> Streamlit`

Reject any direct Python/browser provider acquisition, second freshness
policy, persistent API/poller, schema change, or replacement of legacy
Worker behavior.

## Gate 2 --- Worker composition

Review `DependencyInjection.cs`, `Program.cs`, and
`HistoricalMarketDataQueryExecution.cs`.

Verify:

-   `HistoricalQuery` mode is explicit;
-   legacy/default Worker mode remains unchanged;
-   historical mode composes WP04's read service;
-   Vike remains Infrastructure-owned;
-   cache path is server-controlled;
-   provider credentials remain server-side;
-   historical mode emits one protocol response and exits;
-   malformed input cannot fall through to legacy output;
-   diagnostics cannot corrupt stdout.

Pay particular attention to whether constructing `HttpClient`/Vike
composition here preserves existing provider timeout/cancellation and
resource semantics.

## Gate 3 --- Request/response protocol

Verify exact contract version and 2×3×4 matrix, bounded request size,
1--30 second deadline, deterministic mappings, invariant decimal
serialization, UTC timestamps, controlled unavailable/failure
categories, empty-success behavior, and no internal leakage.

Verify Python validates enough of the response to prevent malformed
canonical data from reaching the chart. In particular inspect:

-   state consistency;
-   canonical selection consistency with the request;
-   timestamp validity/UTC semantics;
-   candle ordering/uniqueness if required at this boundary;
-   OHLC/volume invariants;
-   bounded response size;
-   unexpected or missing fields.

If a missing validation can cause incorrect or misleading public chart
data, correct it within scope.

## Gate 4 --- Cross-process lock: critical review

Adversarially inspect `AtomicFileHistoricalMarketDataQueryLock`.

The canonical ADR requires bounded per-cache-key coordination, lease
expiry, abandoned-lock recovery, and cache recheck after lock
acquisition.

Verify with implementation-level evidence:

-   lock key uses canonical cache fingerprint;
-   unrelated keys do not serialize;
-   acquisition wait is bounded by the intended deadline;
-   lease duration and acquisition deadline are not accidentally
    conflated;
-   an actively owned lock cannot be deleted merely because a contender
    has waited long enough;
-   abandoned/dead-owner recovery actually works;
-   owner-safe release;
-   malformed/partial lock-file handling cannot create an indefinite
    lock;
-   process crash cannot permanently block the key;
-   recovery does not permit uncontrolled simultaneous owners;
-   cache is rechecked by WP04 after lock acquisition;
-   no duplicate freshness policy exists.

**Important:** the current candidate diff appears to derive the
lock-file recovery age from each caller's `deadline`. Luna must
specifically determine whether this correctly implements the ADR's lease
semantics under contenders with different deadlines. Do not accept this
by assumption.

Add/correct tests for expiry/abandonment and differing contender
deadlines if existing tests do not prove the contract.

## Gate 5 --- Timeout/cancellation/process lifecycle

Verify:

-   Python uses direct subprocess invocation with `shell=False`;
-   fixed deployed Worker assembly defaults to
    `/app/worker/AIQuantTradingResearch.Worker.dll`;
-   configuration cannot be browser-controlled;
-   finite subprocess timeout;
-   timed-out process is terminated/reaped by the selected Python API;
-   Worker deadline/cancellation is bounded;
-   lock waits are bounded;
-   provider cancellation remains connected;
-   no orphan process/polling loop.

Check the relationship between Python's 15-second process timeout and
the request's 10-second default deadline.

## Gate 6 --- Python bridge security and correctness

Review `historical_market_bridge.py` for:

-   no HTTP/provider client;
-   no credential handling;
-   no shell interpolation;
-   absolute/fixed server-side assembly path policy;
-   safe environment handling;
-   response-size enforcement;
-   safe stderr behavior;
-   no raw error surfaced to UI;
-   canonical request/response matching.

Determine whether permitting an environment override for the Worker
assembly is consistent with the ADR's fixed server-controlled
invocation. Server-side configuration is acceptable only if it cannot be
influenced by browser/user selection.

## Gate 7 --- Market Research UI contract

Verify:

-   Market Research is default;
-   navigation exactly includes Market Research, ML & Automation
    Studies, System Health;
-   BTC/USD and ETH/USD only;
-   default BTC/USD;
-   intervals exactly 1h, 4h, 1d; default 1h;
-   ranges exactly 1D, 7D, 30D, 90D; default 30D;
-   no provider selector;
-   candlestick + aligned volume;
-   OHLCV hover;
-   zoom and pan;
-   responsive width;
-   fresh/stale/empty/unavailable states;
-   exact governed unavailable text;
-   provenance/update metadata survives;
-   no indicators/signals/predictions/trading controls.

## Gate 8 --- Streamlit rerun behavior

Verify unchanged selection does not repeatedly invoke the Worker on
ordinary reruns.

Hover/zoom/pan must be client-side and must not call the bridge.

Review failure caching behavior: determine whether storing `None` for a
failed selection prevents reasonable recovery indefinitely in the same
session. WP05 must avoid high-frequency retry/polling, but the UI must
remain usable after a transient bridge/provider failure. If recovery
requires a bounded explicit refresh or another governed mechanism,
correct within WP05 scope without implementing WP06.

## Gate 9 --- Legacy presentation

Verify System Health and required legacy Release 1.10-1.12 presentation
behavior remain reachable and semantically intact.

Ensure historical-query changes do not overwrite the legacy
visualization envelope.

Run existing presentation regression/no-bypass tests, not only new
tests.

## Gate 10 --- Plotly

Verify `plotly==7.1.0` is an exact pin and no other existing pin
changed.

Confirm from authoritative/package metadata:

-   Python 3.13 compatibility;
-   license;
-   no paid/runtime service;
-   Streamlit 1.61.1 interoperability;
-   F1 bundle/install impact is reasonable.

Do not upgrade numpy, pandas, scikit-learn, or Streamlit.

If Plotly 7.1.0 is not supportable, this becomes a governance/dependency
issue rather than permission to choose arbitrary packages.

## Gate 11 --- F1 and Docker compatibility

Verify current Docker/runtime really makes:

`dotnet /app/worker/AIQuantTradingResearch.Worker.dll`

available to Streamlit.

Confirm the cache directory `/home/aiq-market-cache` is compatible with
current persistent `/home` assumptions and no Docker mutation is
required for the candidate to function.

Assess subprocess startup, bounded concurrency, lock polling, payload
size, Plotly footprint, and no persistent companion process against F1
constraints.

Do not claim live performance measurements unless actually measured.

## Gate 12 --- Tests

The reported baseline is:

-   Release build: 0 warnings / 0 errors;
-   Domain: 11;
-   Application: 168;
-   Architecture: 27;
-   Infrastructure: 233;
-   focused bridge/lock/Vike: 25;
-   Python presentation: 32.

Re-run relevant suites.

Do not treat counts alone as sufficient. Inspect whether tests prove the
critical semantics.

At minimum acceptance must have focused proof for:

-   valid request;
-   malformed request;
-   legacy mode preservation;
-   stdout purity;
-   contract version;
-   safe failure serialization;
-   lock same-key exclusion;
-   different-key independence;
-   lease expiry;
-   abandoned lock recovery;
-   differing contender deadlines;
-   post-lock cache recheck / bounded duplicate acquisition;
-   subprocess fixed/shell-free invocation;
-   timeout;
-   malformed/truncated/oversized response;
-   request/response selection match;
-   UTC/candle validation;
-   UI defaults/matrix;
-   fresh/stale/empty/unavailable;
-   transient failure recovery;
-   candlestick/volume/hover/pan/zoom/responsiveness;
-   no provider bypass;
-   legacy/System Health regression.

If missing tests expose a substantive defect, correct
implementation/tests within the 13 paths and rerun.

## Gate 13 --- Security/no-bypass

Search changed Python and public-facing code for provider URLs,
Vike/Twelve Data acquisition, API keys, HTTP clients, shell invocation,
raw exception rendering, internal paths, and secrets.

Twelve Data remains private/internal and unchanged.

Vike calls must remain behind the .NET governed service.

## Gate 14 --- Exact scope

Verify exactly the 13 candidate paths and root README unchanged.

No Docker, Azure, GHCR, schema, deployment, provider-adapter, Release
1.14, ML, trading, tag, release, or issue lifecycle mutation.

## Correction authority

Luna may correct substantive defects only within the exact 13 candidate
paths.

Permitted corrections include bridge validation, Worker mode/protocol
behavior, lock correctness, tests, Python bridge behavior, UI behavior,
and Plotly-related code/requirements only within the already-authorized
dependency decision.

No new path may be added.

If corrections occur:

-   stay on PR #302 branch;
-   commit/push;
-   record new head;
-   rerun complete acceptance;
-   the new head becomes the only final Luna-accepted candidate.

Report corrections as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

## Governance stop conditions

Stop with:

`GOVERNANCE RESTRICTION — STOPPED`

only if correction requires:

-   a 14th path;
-   architecture redesign;
-   new Python dependency beyond Plotly;
-   new .NET package;
-   schema change;
-   Docker/Azure/GHCR/deployment mutation;
-   provider adapter contract change outside scope;
-   README;
-   WP06/1.14/2.0 work;
-   merge/tag/release;
-   closing #292 or marking WP05 Done.

## Retry-until-governance-boundary rule

Do not stop for ordinary implementation, test, build, lint, validation,
locking, subprocess, or UI failures. Diagnose, correct within the exact
13-path authority, rerun, and continue until acceptance passes or a true
governance restriction is reached.

## Validation before PASS

Verify and report:

-   live base/head/state;
-   exact 13 paths;
-   final accepted head;
-   Release build;
-   relevant full/focused .NET suites;
-   Python presentation suites;
-   Plotly import/version;
-   dependency diff;
-   whitespace;
-   secret scan;
-   no-bypass scan;
-   README unchanged;
-   Worker malformed-request smoke test;
-   lock expiry/abandonment/differing-deadline evidence;
-   post-lock cache recheck evidence;
-   subprocess timeout/security evidence;
-   Docker path evidence;
-   F1 compatibility;
-   issue #292 remains Open/non-Done;
-   milestone #64 Open;
-   #293-#295 Open;
-   PR #302 remains unmerged;
-   no tag/GitHub Release.

## Lifecycle boundary

A PASS does not authorize merge.

After PASS create a separate **GPT-5.6 Terra WP05 PR #302
merge/post-merge lifecycle authority** bound to the final accepted head.

Only after merge/lifecycle PASS may issue #292 be closed/Done and WP06
become eligible, subject to its own authority.

## Required final report

Return the initial and final head, exact paths, inspected files,
architecture/no-bypass findings, Worker/protocol findings, lock analysis
and corrections, cache-recheck proof, timeout/cancellation findings,
Python bridge findings, UI findings, rerun/recovery findings, legacy
compatibility, Plotly metadata and dependency diff, F1/Docker findings,
test results, security/secret/whitespace results, README proof,
issue/milestone states, corrections, governance restrictions, PR state,
and next authority.

End with exactly one of:

`RELEASE 1.13 WP05 HISTORICAL BRIDGE AND PUBLIC MARKET RESEARCH UI FINAL SUBSTANTIVE ACCEPTANCE: PASS`

or

`RELEASE 1.13 WP05 HISTORICAL BRIDGE AND PUBLIC MARKET RESEARCH UI FINAL SUBSTANTIVE ACCEPTANCE: BLOCKED`

A PASS accepts the implementation candidate only. It does not authorize
merge, WP06, deployment, tag, or release.

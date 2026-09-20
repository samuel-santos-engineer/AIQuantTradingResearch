# Release 1.13 WP05 --- PR #302 Merge and Post-Merge Lifecycle Authority

## Selected model

**Execution model: GPT-5.6 Terra**

GPT-5.6 Luna has completed final substantive implementation acceptance.
GPT-5.6 Terra owns only this bounded merge and post-merge lifecycle
operation. GPT-5.6 Sol may support analysis but may not alter accepted
implementation or lifecycle governance.

## Lifecycle target

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Pull request:

`#302 — Release 1.13 WP05: historical market research bridge`

Expected branch:

`feature/release-1.13-wp05-historical-market-research`

Required pre-merge canonical `origin/main`:

`10b3c006be16bb4a26bbf41a2a8e295984fd3c05`

Initial implementation candidate:

`3ef552be3b5d928aba212577bccc63d2aa0cf673`

**Final Luna-accepted head:**

`5613f601b2fa944af236e16bb0bfee4eb652139d`

The final accepted head supersedes all earlier WP05 implementation
heads.

## Acceptance evidence

Luna reported:

`RELEASE 1.13 WP05 HISTORICAL BRIDGE AND PUBLIC MARKET RESEARCH UI FINAL SUBSTANTIVE ACCEPTANCE: PASS`

Accepted corrections remained within the 13-path scope and included:

-   fixed 35-second cross-process lock lease;
-   caller deadline bounds acquisition only;
-   abandoned-lock and differing-deadline coverage;
-   hardened bridge response validation;
-   explicit retry for transient UI bridge failures.

Do not reinterpret or redesign these accepted decisions during lifecycle
execution.

## Exact accepted path set

Exactly these 13 paths may land:

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

No fourteenth path is authorized.

`README.md` remains byte-for-byte protected.

## Purpose

Perform the established Release 1.13 lifecycle:

1.  verify PR #302 still exactly matches the Luna-accepted candidate;
2.  rerun bounded pre-merge checks;
3.  merge normally using repository precedent;
4.  capture actual merge commit/new canonical main;
5.  prove accepted-head ancestry;
6.  prove exactly 13 accepted paths landed;
7.  run post-merge validation;
8.  complete WP05 issue/Project lifecycle only after canonical
    implementation is verified.

This authority does not authorize WP06 implementation, deployment, tag,
or release.

## Pre-merge verification

Verify live truth:

-   PR #302 exists;
-   Open;
-   unmerged;
-   non-draft;
-   base `main`;
-   base lineage remains the required canonical predecessor;
-   branch matches expected branch;
-   head exactly `5613f601b2fa944af236e16bb0bfee4eb652139d`;
-   mergeable under normal protections;
-   exact 13 changed paths;
-   README absent;
-   no Docker/Azure/GHCR/schema/deployment/tag/release path;
-   issue #292 Open/non-Done;
-   milestone #64 Open;
-   #293-#295 Open;
-   no Release 1.13 tag/GitHub Release.

## Head-drift rule

If PR head differs from the accepted SHA, do not merge automatically.

Inspect drift. Any substantive code/test/dependency/documentation change
requires renewed GPT-5.6 Luna substantive acceptance.

Do not solve lifecycle drift by modifying accepted implementation.

## Accepted implementation preservation check

Before merge confirm the final head still contains the accepted
behavior:

-   additive explicit `HistoricalQuery` Worker mode;
-   fixed local Worker invocation;
-   WP04 sole cache/freshness/provider authority;
-   safe versioned stdio protocol;
-   stdout protocol isolation;
-   shell-free Python subprocess invocation;
-   finite timeout;
-   fixed 35-second lock lease;
-   acquisition bounded by caller deadline;
-   abandoned-lock recovery;
-   differing-deadline safety;
-   post-lock governed cache recheck;
-   hardened response validation;
-   transient UI retry;
-   Market Research default;
-   BTC/USD + ETH/USD;
-   1h/4h/1d;
-   1D/7D/30D/90D;
-   BTC/USD / 1h / 30D defaults;
-   candlestick + volume;
-   hover/zoom/pan/responsive rendering;
-   Plotly exactly `7.1.0`;
-   no provider/browser bypass;
-   legacy Worker/System Health behavior preserved.

## Pre-merge validation

Run/report at minimum:

-   exact changed-path proof;
-   `git diff --check`;
-   secret scan;
-   README proof;
-   dependency diff proof;
-   no-bypass scan;
-   repository cleanliness/staging state;
-   mergeability/protection state.

Use the accepted validation baseline as a minimum confidence reference:

-   Release build: 0 warnings/errors;
-   Domain 11;
-   Application 168;
-   Architecture 27;
-   Infrastructure 235;
-   Python presentation 33;
-   Plotly 7.1.0;
-   Python 3.13.15.

Rerun the relevant build/tests before merge if repository lifecycle
precedent requires it or if live state creates doubt. Do not mutate
accepted code to obtain different test counts.

## Merge method

Use the normal merge-commit method established for Release 1.13
WP01-WP04 and architecture PR #301.

Do not:

-   direct-push `main`;
-   force push;
-   bypass branch protection;
-   squash/rebase unless repository policy demonstrably requires it;
-   amend accepted head;
-   merge another PR as part of this authority.

Capture the actual merge timestamp and merge commit.

## Post-merge verification

After merge:

-   fetch canonical `origin/main`;
-   record resulting SHA;
-   prove `5613f601b2fa944af236e16bb0bfee4eb652139d` is reachable from
    canonical main;
-   prove exactly the 13 accepted paths landed relative to pre-merge
    main;
-   prove README unchanged;
-   run `git diff --check`/whitespace verification;
-   rerun secret scan over landed change;
-   rerun no-bypass verification;
-   verify Plotly remains exactly 7.1.0 and the prior four pins remain
    unchanged;
-   verify architecture and implementation authority records are
    canonical;
-   verify no Docker/Azure/GHCR/schema/deployment/tag/release mutation.

Where practical, run a post-merge Release build and relevant focused
smoke/tests to establish canonical-main integrity.

## WP05 lifecycle completion

Unlike architecture PR #301, PR #302 is the accepted WP05 implementation
candidate.

After successful post-merge verification, complete WP05 lifecycle
according to established project precedent:

-   close issue #292;
-   allow/verify issue-close automation moves Project WP05 to Done, or
    set the established equivalent only if separately permitted by
    repository precedent;
-   verify Project Release remains `1.13`;
-   milestone #64 remains Open because Release 1.13 is not complete;
-   #293-#295 remain Open.

Do not close #293, #294, or #295.

If Project automation fails, retry ordinary permitted lifecycle
operations. Do not alter unrelated Project fields.

## WP06 dependency gate

WP06 (#293) becomes eligible for a **separate authority** only after:

-   PR #302 is merged;
-   accepted head ancestry passes;
-   post-merge validation passes;
-   issue #292 is Closed;
-   Project WP05 is Done.

Do not begin WP06 under this authority.

## Deployment boundary

Do not deploy or modify:

-   Azure;
-   Docker;
-   GHCR;
-   App Service configuration;
-   secrets;
-   live Vike configuration.

WP07 owns F1 integration/deployment validation.

## Tag/release boundary

Do not create:

-   Release 1.13 tag;
-   GitHub Release;
-   milestone closure;
-   final release acceptance.

## Root README protection

`README.md` remains byte-for-byte unchanged.

## Retry-until-governance-boundary rule

Do not stop merely because fetch, mergeability, build, test, whitespace,
secret scan, no-bypass scan, merge, issue-close automation, or ordinary
lifecycle tooling initially fails.

Diagnose and correct/retry operations permitted by this authority.

Do not mutate accepted implementation to solve lifecycle/tooling
failures.

Stop only if the next action requires crossing governance, including:

-   head drift requiring substantive acceptance;
-   extra path;
-   accepted code change;
-   README;
-   dependency change;
-   Docker/Azure/GHCR/schema/deployment mutation;
-   branch-protection bypass;
-   force/direct push;
-   WP06 implementation;
-   closing later WP issues;
-   milestone closure;
-   tag/GitHub Release.

When blocked report:

`GOVERNANCE RESTRICTION — STOPPED`

with exact evidence and required authority.

## Final verification before PASS

Confirm:

-   PR #302 merged normally;
-   actual merge commit captured;
-   new canonical `origin/main` captured;
-   accepted head reachable;
-   exactly 13 paths landed;
-   README unchanged;
-   whitespace clean;
-   secret scan clean;
-   no-bypass clean;
-   dependency diff remains only Plotly 7.1.0 addition;
-   no Docker/Azure/GHCR/schema/deployment mutation;
-   issue #292 Closed;
-   Project WP05 Done;
-   milestone #64 Open;
-   #293-#295 Open;
-   no Release 1.13 tag;
-   no GitHub Release;
-   WP06 not implemented.

## Required final report

Return:

-   pre-merge canonical main;
-   PR state/base/branch;
-   final Luna-accepted head;
-   exact pre-merge paths;
-   pre-merge validation;
-   merge method;
-   merge timestamp;
-   actual merge commit;
-   resulting canonical `origin/main`;
-   accepted-head reachability;
-   exact landed paths;
-   post-merge build/test/smoke results;
-   whitespace;
-   secret scan;
-   no-bypass;
-   dependency proof;
-   README proof;
-   issue #292 final state;
-   Project WP05 final state;
-   milestone #64 state;
-   #293-#295 states;
-   tag/GitHub Release state;
-   confirmation no deployment/WP06 work;
-   technical lifecycle failures corrected;
-   governance restrictions;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP05 PR #302 MERGE AND POST-MERGE VERIFICATION: PASS`

or

`RELEASE 1.13 WP05 PR #302 MERGE AND POST-MERGE VERIFICATION: BLOCKED`

A PASS completes WP05 lifecycle only. It does not authorize WP06
implementation, deployment, tag, milestone closure, or Release 1.13
publication.

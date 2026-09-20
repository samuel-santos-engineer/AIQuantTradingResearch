# Release 1.13 WP04 --- PR #300 Merge and Post-Merge Verification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

-   **GPT-5.6 Luna** owns the accepted WP04 substantive result.
-   **GPT-5.6 Terra** owns this bounded merge and empirical post-merge
    lifecycle operation.
-   **GPT-5.6 Sol** is supporting analysis only.

## Target

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

PR: `#300 — Release 1.13 WP04: cache-first historical read model`

Expected pre-merge `main`:

`26e497d74c654f99a3430266b00facaaee948acd`

Final Luna-accepted head:

`a2b80325ced683d1d561446b3ba0754250a548fe`

Expected branch:

`feature/release-1.13-wp04-cache-read-model`

Acceptance marker:

`RELEASE 1.13 WP04 LUNA FINAL SUBSTANTIVE ACCEPTANCE: PASS`

Verify live repository truth before acting.

## Exact accepted path set

Exactly these seven paths:

1.  `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataCacheContracts.cs`
2.  `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataReadService.cs`
3.  `src/AIQuantTradingResearch.Infrastructure/MarketData/Cache/AtomicFileHistoricalMarketDataCache.cs`
4.  `tests/AIQuantTradingResearch.Application.Tests/HistoricalMarketDataReadServiceTests.cs`
5.  `tests/AIQuantTradingResearch.Infrastructure.Tests/AtomicFileHistoricalMarketDataCacheTests.cs`
6.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
7.  `docs/roadmap/release-1.13/prompters/release-1.13-wp04-terra-cache-first-historical-data-read-model-implementation.md`

Root `README.md` remains protected.

## Purpose

This authority permits only:

1.  final verification that PR #300 still equals the Luna-accepted
    candidate;
2.  bounded pre-merge validation;
3.  normal merge of that accepted candidate;
4.  canonical post-merge verification;
5.  issue #291 / Project #2 WP04 lifecycle reconciliation using
    established WP precedent.

This authority does **not** authorize WP05 implementation.

## Pre-merge gates

Before merge prove:

-   PR #300 is open and unmerged;
-   base is `main`;
-   base lineage remains the expected WP03 canonical predecessor;
-   head is exactly `a2b80325ced683d1d561446b3ba0754250a548fe`;
-   branch is the expected WP04 branch;
-   PR is normally mergeable;
-   exactly seven changed paths;
-   root README absent;
-   no unreviewed substantive commit exists after Luna acceptance;
-   Release build passes with zero warnings/errors;
-   focused WP04 Application tests pass;
-   atomic-cache tests pass;
-   full Application tests pass;
-   full Infrastructure tests pass;
-   Domain tests pass;
-   Architecture tests pass;
-   focused Vike/Twelve Data tests pass;
-   `git diff --check` passes;
-   isolated secret scan passes;
-   no Vike/Twelve Data behavior change;
-   no
    package/schema/Worker/Python/Streamlit/Plotly/UI/Azure/Docker/GHCR/deployment/tag/GitHub
    Release change;
-   issue #291 is still open/non-Done before lifecycle completion;
-   Project Release remains `1.13`;
-   milestone #64 remains open.

If the PR head differs from the Luna-accepted SHA, do not merge unless
the difference is proven to be a non-substantive lifecycle-only
artifact. Any substantive implementation change requires renewed Luna
acceptance.

## Accepted WP04 behavior to preserve

Post-merge verification must confirm the accepted implementation still
provides:

-   cache key dimensions: symbol, interval, range, provider identity,
    cache-contract version;
-   cache-first governed read service;
-   fresh cache -\> zero provider calls;
-   missing/corrupt cache -\> bounded acquisition;
-   stale cache -\> governed refresh;
-   refresh failure with usable stale cache -\> truthful stale data;
-   no usable cache + provider failure -\> controlled unavailable
    result;
-   empty successful dataset distinct from unavailable;
-   future validation timestamps not treated as fresh;
-   ordinary cache I/O failures contained;
-   identical concurrent misses coalesced;
-   atomic file replacement;
-   bounded 32-snapshot retention;
-   no background polling;
-   no Twelve Data public fallback.

Do not modify these semantics under lifecycle authority.

## Retry-until-governance-boundary rule

Do not stop merely because a merge, Git operation, test, build, scan, or
lifecycle step fails.

Diagnose and retry/correct ordinary operational failures that do not
alter the Luna-accepted implementation or expand authority.

Examples include transient compiler/test-runner locks, the previously
observed unrelated observability flake, Git fetch races, GitHub API
transient failures, whitespace tooling invocation errors, or issue-close
automation delay.

Report corrected ordinary failures as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

Stop if the next corrective action requires:

-   substantive change to the Luna-accepted seven-path candidate;
-   an eighth path;
-   WP01/WP02 contract change;
-   Vike/Twelve Data change;
-   package/schema change;
-   Worker/Python/Streamlit/Plotly/UI implementation;
-   Azure/Docker/GHCR/deployment;
-   root README;
-   Release 1.14/2.0;
-   secret exposure;
-   protection bypass;
-   WP05 implementation;
-   tag/GitHub Release.

Report:

`GOVERNANCE RESTRICTION — STOPPED`

State the exact restriction and required authority.

## Merge authority

When every pre-merge gate passes, merge PR #300 using the repository's
normal GitHub merge method.

Do not bypass protections.

Do not push directly to `main`.

Do not squash/rebase if doing so conflicts with established repository
lifecycle precedent; use the same normal merge pattern used for
preceding Release 1.13 WPs.

Record:

-   pre-merge main SHA;
-   accepted PR head;
-   merge method;
-   resulting merge commit SHA.

## Canonical post-merge verification

After merge:

1.  fetch/update remote state;
2.  record canonical `origin/main`;
3.  prove `a2b80325ced683d1d561446b3ba0754250a548fe` is reachable from
    canonical main;
4.  prove exactly the seven accepted paths landed;
5.  prove no unexpected path landed;
6.  prove root README remains unchanged;
7.  rerun Release build;
8.  rerun focused WP04 Application tests;
9.  rerun atomic-cache tests;
10. rerun full Application tests;
11. rerun full Infrastructure tests;
12. rerun Domain tests;
13. rerun Architecture tests;
14. rerun focused Vike/Twelve Data tests;
15. rerun `git diff --check`/equivalent post-merge whitespace evidence;
16. rerun isolated secret scanning;
17. verify accepted cache-first semantics remain present;
18. verify Vike remains Infrastructure-owned and unchanged;
19. verify Twelve Data remains unchanged;
20. verify no
    package/schema/Worker/Python/Streamlit/Plotly/UI/Azure/Docker/GHCR/deployment
    work landed;
21. verify no tag contains the merge commit as a new Release 1.13
    lifecycle action;
22. verify no Release 1.13 GitHub Release was created.

The resulting canonical `origin/main` SHA becomes the required
predecessor for WP05.

## Issue / Project lifecycle

After successful merge and post-merge technical verification, follow the
established WP01-WP03 lifecycle precedent.

Close issue `#291` exactly once if still open.

If issue-close automation sets Project #2 WP04 to `Done`, accept and
record that automated result. Do not perform a redundant explicit
Project status mutation.

Preserve Project Release `1.13`.

Milestone #64 must remain open.

Issues #292-#295 must remain open and must not be executed under this
authority.

If Project automation is delayed, verify again before deciding a manual
mutation is needed. Do not invent new lifecycle policy.

## Protected boundaries

No mutation or execution of:

-   WP05 implementation;
-   Market Research Streamlit UI;
-   Plotly installation;
-   chart rendering;
-   Python presentation code;
-   Worker composition;
-   Vike adapter;
-   Twelve Data adapter;
-   package manifests;
-   SQLite/schema;
-   Azure;
-   Docker/GHCR;
-   deployment;
-   root README;
-   Release 1.14;
-   Release 2.0;
-   tags;
-   GitHub Releases.

Local unrelated untracked authority records must remain untouched.

## WP05 boundary

A successful lifecycle PASS establishes WP04 on canonical main.

It does not itself authorize WP05 implementation.

The next step must be a separate `release-1.13-...` Markdown authority
for WP05 using **GPT-5.6 Terra**, bound to the new canonical main SHA.

Before freezing WP05's mutation allowlist, that authority must inspect
the merged WP04 presentation-safe read model, existing Python/Streamlit
presentation architecture, current dependency manifests, and the
accepted WP01 UI contract.

Plotly dependency mutation, if needed, must be explicitly governed in
WP05 with exact version/license/bundle/F1 review as required by WP01.

## Required final report

Return:

-   pre-merge canonical main SHA;
-   PR #300 state/base/head;
-   proof head matched Luna acceptance;
-   exact seven changed paths;
-   README proof;
-   pre-merge build/test/security results;
-   technical failures corrected, if any;
-   governance restrictions, if any;
-   merge method;
-   merge commit SHA;
-   canonical post-merge `origin/main`;
-   accepted-head reachability;
-   post-merge build/test/security results;
-   accepted cache-first semantic verification;
-   Vike preservation;
-   Twelve Data preservation;
-   confirmation no package/schema/Worker/UI/deployment scope landed;
-   issue #291 final state;
-   Project #2 WP04 final status and Release field;
-   milestone #64 state;
-   issues #292-#295 states;
-   tag/GitHub Release verification;
-   local unrelated authority-file preservation;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP04 PR #300 MERGE AND POST-MERGE VERIFICATION: PASS`

or

`RELEASE 1.13 WP04 PR #300 MERGE AND POST-MERGE VERIFICATION: BLOCKED`

A PASS establishes WP04 on canonical main. It does not authorize WP05.

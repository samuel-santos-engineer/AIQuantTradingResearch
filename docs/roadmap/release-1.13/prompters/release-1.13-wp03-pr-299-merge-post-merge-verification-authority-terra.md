# Release 1.13 WP03 --- PR #299 Merge and Post-Merge Verification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

This is a bounded lifecycle operation following successful GPT-5.6 Luna
substantive acceptance of WP03.

Model roles:

-   **GPT-5.6 Luna** --- owns the accepted Release 1.13 architecture and
    WP03 substantive acceptance.
-   **GPT-5.6 Terra** --- owns this explicitly authorized merge and
    empirical post-merge verification.
-   **GPT-5.6 Sol** --- supporting analysis only.

## Target

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

PR: `#299 — Release 1.13 WP03: Vike historical OHLCV adapter`

Expected pre-merge main:

`b6a779ae0eebdd60d9b688029f19cf0afa6407f5`

Expected Luna-accepted head:

`671326d78bdb2c14d4df75fab7b29b9263a24718`

Expected branch:

`feature/release-1.13-wp03-vike-ohlcv-adapter`

The candidate has passed:

`RELEASE 1.13 WP03 LUNA FINAL SUBSTANTIVE ACCEPTANCE: PASS`

Verify live repository truth before acting.

## Accepted paths

Exactly:

-   `src/AIQuantTradingResearch.Infrastructure/MarketData/Vike/VikeConfiguration.cs`
-   `src/AIQuantTradingResearch.Infrastructure/MarketData/Vike/VikeHistoricalMarketDataProvider.cs`
-   `tests/AIQuantTradingResearch.Infrastructure.Tests/VikeHistoricalMarketDataProviderTests.cs`
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
-   `docs/roadmap/release-1.13/prompters/release-1.13-wp03-terra-vike-historical-ohlcv-adapter-implementation-validation.md`

Root `README.md` remains protected.

No additional source, Twelve Data behavior, cache/read-model, UI,
package, schema, Azure, Docker/GHCR, deployment, tag, or GitHub Release
mutation is authorized.

## Purpose

This authority permits Terra to:

1.  verify PR #299 still exactly matches the Luna-accepted candidate;
2.  rerun bounded pre-merge validation;
3.  correct only ordinary lifecycle/technical defects that do not alter
    the accepted substantive implementation or cross the five-path
    boundary;
4.  merge PR #299 normally when all gates pass;
5.  verify canonical post-merge `origin/main`;
6.  reconcile issue #290 / Project #2 lifecycle according to established
    precedent;
7.  establish the predecessor for a separately authorized WP04.

This authority does not authorize WP04 implementation.

## Pre-merge gates

Verify:

-   PR #299 is open/unmerged;
-   base is `main`;
-   head is the Luna-accepted
    `671326d78bdb2c14d4df75fab7b29b9263a24718`;
-   no unreviewed substantive commit was added;
-   PR remains normally mergeable;
-   exact five-path diff;
-   README absent;
-   Release build passes;
-   21 focused Vike tests pass;
-   Infrastructure suite passes;
-   Application suite passes;
-   Domain suite passes;
-   Architecture suite passes;
-   Twelve Data tests pass;
-   `git diff --check` passes;
-   isolated secret scan passes;
-   no live credential/provider evidence has been falsely added;
-   no cache/UI/package/schema/deployment mutation;
-   issue #290 remains Release `1.13`;
-   milestone #64 remains open.

If the PR head changed after Luna acceptance, inspect it. A substantive
change requires renewed Luna acceptance.

## Continue-until-governance-boundary rule

Do not stop for an ordinary technical or lifecycle failure.

Correct the smallest permitted defect and rerun checks until the
lifecycle operation passes.

Examples include transient Git/GitHub failures, compiler locks, test
runner contention, command invocation errors, whitespace issues, and
lifecycle metadata update failures.

Report:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

Stop if further progress requires changing the Luna-accepted
implementation, adding a sixth path, changing WP01/WP02 contracts,
modifying Twelve Data, implementing cache/UI, adding dependencies,
schema/database work, Azure/Docker/GHCR/deployment, README mutation,
Release 1.14/2.0 work, secrets, protection bypass, or starting WP04.

Report:

`GOVERNANCE RESTRICTION — STOPPED`

## Merge authority

If all gates pass, this document explicitly authorizes a normal
repository merge of PR #299 into `main`.

Do not bypass protections.

Do not create a tag or GitHub Release.

Do not close milestone #64.

Do not start WP04.

## Issue / Project lifecycle

After successful merge and verification, follow the established
WP01/WP02 precedent.

Close issue #290 if that is the established accepted-WP lifecycle
action.

If issue closure automation sets Project #2 WP03 to `Done`, accept and
record that result; do not perform a redundant explicit Project status
mutation.

Preserve Project Release `1.13`.

Do not mutate issues #291--#295.

If lifecycle behavior unexpectedly differs and correction would require
inventing policy, leave the ambiguous state unchanged and report it.

## Post-merge verification

After merge:

1.  fetch/update `origin`;
2.  record canonical `origin/main`;
3.  prove accepted head reachability;
4.  verify all five accepted paths landed;
5.  verify no unexpected path landed;
6.  verify README unchanged;
7.  rerun Release build;
8.  rerun focused Vike tests;
9.  rerun Infrastructure, Application, Domain, Architecture, and Twelve
    Data suites;
10. rerun whitespace and secret checks;
11. verify canonical WP02 contracts remain intact;
12. verify Vike remains Infrastructure-owned;
13. verify no credential was committed/exposed;
14. verify no cache/read-model implementation exists from WP04;
15. verify no Streamlit/Plotly/UI work exists from WP05/WP06;
16. verify no package/schema/Azure/Docker/GHCR/deployment mutation;
17. verify issue #290 / Project state;
18. verify milestone #64 remains open;
19. verify issues #291--#295 remain open/unexecuted;
20. verify no tag/GitHub Release.

The resulting canonical main SHA becomes the WP04 predecessor.

## WP04 boundary

A PASS does not authorize WP04.

WP04 requires a separate Markdown authority using the `release-1.13-`
prefix and **GPT-5.6 Terra**.

That authority must inspect the live repository before freezing its
exact literal allowlist and must govern only the cache-first
historical-data/read-model path, freshness semantics, and
outage-with-cache behavior.

It must preserve:

-   canonical WP02 contracts;
-   accepted Vike adapter;
-   Twelve Data behavior;
-   no direct Streamlit/provider access;
-   no UI implementation;
-   no Plotly;
-   no new package unless separately governed;
-   no schema migration unless separately governed;
-   Azure F1 / `$0.00` constraints;
-   retry-until-governance-boundary behavior;
-   separate substantive acceptance and lifecycle.

## Required final report

Report:

-   pre-merge main SHA;
-   PR #299 state/base/head;
-   confirmation head matched Luna acceptance;
-   exact changed paths;
-   pre-merge validation;
-   technical failures corrected;
-   governance restrictions;
-   merge method;
-   merge commit SHA;
-   canonical post-merge `origin/main`;
-   accepted-head reachability;
-   README unchanged;
-   post-merge build/test results;
-   Vike adapter presence/integrity;
-   Twelve Data preservation;
-   secret-safety result;
-   confirmation no WP04/WP05/WP06/package/schema/deployment work
    occurred;
-   issue #290 final state;
-   Project #2 WP03 status / Release;
-   milestone #64 state;
-   issues #291--#295 state;
-   no tag/GitHub Release;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP03 PR #299 MERGE AND POST-MERGE VERIFICATION: PASS`

or

`RELEASE 1.13 WP03 PR #299 MERGE AND POST-MERGE VERIFICATION: BLOCKED`

A PASS establishes WP03 on canonical main. It does not authorize WP04.

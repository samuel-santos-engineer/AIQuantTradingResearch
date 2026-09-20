# Release 1.13 WP02 --- PR #298 Merge and Post-Merge Verification Authority

## Selected model

**Execution model: GPT-5.6 Terra**

This is a bounded lifecycle operation following successful GPT-5.6 Luna
substantive acceptance of WP02.

Model roles:

-   **GPT-5.6 Luna** --- owns the accepted Release 1.13/WP01
    architecture and WP02 substantive acceptance.
-   **GPT-5.6 Terra** --- owns this explicitly authorized merge and
    empirical post-merge verification operation.
-   **GPT-5.6 Sol** --- supporting analysis only; no merge or governance
    authority.

Terra must not reinterpret or expand the accepted WP02 contract.

## Repository and target

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Target:

`PR #298 — Release 1.13 WP02 provider-independent market data contracts`

Expected pre-merge base:

`main @ dc0f227bedad2135d8f41cbcd913948251e1a945`

Expected Luna-accepted head:

`a97693d32ff4976d923ce34379a25a4f11a2a472`

Expected branch:

`feature/release-1.13-wp02-market-data-contract`

The candidate has passed:

`RELEASE 1.13 WP02 LUNA FINAL SUBSTANTIVE ACCEPTANCE: PASS`

Verify live repository truth before acting.

## Purpose

This authority permits Terra to:

1.  verify PR #298 still exactly represents the Luna-accepted WP02
    candidate;
2.  rerun bounded pre-merge validation;
3.  correct only ordinary technical/lifecycle defects when doing so does
    not alter the accepted substantive contract or cross the frozen WP02
    mutation boundary;
4.  merge PR #298 through the normal repository mechanism when all gates
    pass;
5.  verify the resulting canonical `origin/main`;
6.  reconcile issue #289 / Project #2 lifecycle according to established
    repository precedent;
7.  establish the canonical predecessor for a separately authorized WP03
    implementation authority.

This authority does not authorize WP03 implementation.

## Accepted mutation boundary

Expected changed paths are exactly:

-   `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataContracts.cs`
-   `tests/AIQuantTradingResearch.Application.Tests/HistoricalMarketDataContractsTests.cs`
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
-   `docs/roadmap/release-1.13/prompters/release-1.13-wp02-terra-provider-independent-historical-market-data-abstraction-implementation.md`

Root `README.md` remains protected.

No additional application source, provider adapter, Twelve Data
behavior, cache/read-model, UI, package/dependency, schema/database,
Azure, Docker/GHCR, deployment, tag, or GitHub Release mutation is
authorized.

## Pre-merge verification

Before merge verify:

-   PR #298 is still open and unmerged;
-   base is `main`;
-   head still equals `a97693d32ff4976d923ce34379a25a4f11a2a472`, unless
    a later explicitly Luna-accepted correction superseded it;
-   no unreviewed substantive commit was added after Luna acceptance;
-   PR is mergeable and repository protections/checks permit normal
    merge;
-   diff remains exactly the four accepted paths;
-   root README is absent;
-   `git diff --check` passes;
-   repository-standard isolated secret scan passes;
-   affected Release build passes;
-   focused WP02 tests pass;
-   application tests pass;
-   existing Twelve Data tests pass;
-   domain and architecture suites pass where applicable;
-   no Vike runtime/provider-call work exists;
-   no cache/UI/Streamlit/Plotly work exists;
-   no package/schema/deployment mutation exists;
-   issue #289 remains the WP02 issue under Release `1.13`;
-   milestone #64 remains open.

If the head changed after Luna acceptance, do not assume it remains
accepted. Any substantive change requires renewed Luna acceptance.

## Continue-until-governance-boundary rule

Do not stop because of an ordinary technical or lifecycle failure.

Diagnose, make the smallest permitted correction, rerun the affected
checks, and continue until the authorized merge/post-merge operation
passes or the next required action crosses governance.

Correctable examples include:

-   transient Git/GitHub failures;
-   branch synchronization mechanics that do not change the accepted
    contract;
-   whitespace defects;
-   validation-command invocation mistakes;
-   test execution/tooling mistakes;
-   lifecycle metadata update failures;
-   other mechanical defects that leave the Luna-accepted implementation
    unchanged.

Report corrected ordinary failures as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

Stop if further progress requires:

-   changing the Luna-accepted WP02 source contract;
-   modifying a path outside the accepted four-path mutation set;
-   adding provider/Vike behavior;
-   modifying Twelve Data behavior;
-   implementing cache/UI;
-   changing dependencies/packages;
-   schema/database mutation;
-   Azure/Docker/GHCR/deployment mutation;
-   root README mutation;
-   Release 1.14/2.0 implementation;
-   force-pushing/rewrite outside established workflow;
-   bypassing repository protection;
-   exposing secrets;
-   prohibited cost;
-   starting WP03.

Report such a stop as:

`GOVERNANCE RESTRICTION — STOPPED`

## Merge authority

If all gates pass, this document explicitly authorizes normal merge of
PR #298 into `main`.

Use repository-standard merge behavior.

Do not bypass protections.

Do not create a tag or GitHub Release.

Do not close milestone #64.

Do not start WP03.

## WP02 issue / Project lifecycle

After successful merge and post-merge verification, apply the
established WP01/Release 1.12 lifecycle precedent.

If normal issue closure automation sets the Project #2 WP02 item to
`Done`, accept that result and record that no explicit Project status
mutation was necessary.

Preserve Project Release `1.13`.

Do not modify issues #290--#295.

If lifecycle behavior differs unexpectedly from precedent and correction
would require inventing a new policy, leave the state unchanged and
report the discrepancy.

## Post-merge verification

After merge:

1.  fetch/update `origin`;
2.  record canonical `origin/main`;
3.  prove the accepted head is reachable from canonical main;
4.  verify all four accepted paths landed;
5.  verify no unexpected path landed;
6.  verify root README unchanged;
7.  rerun appropriate whitespace, secret, build, focused-test,
    application-test, Twelve Data, domain, and architecture validation;
8.  verify the canonical historical market-data contracts compile from
    `main`;
9.  verify Twelve Data remains unchanged and its tests pass;
10. verify no Vike adapter/runtime/provider call exists;
11. verify no cache/UI/Plotly implementation exists;
12. verify no package/schema/Azure/Docker/GHCR/deployment mutation
    occurred;
13. verify issue #289 / Project #2 lifecycle result;
14. verify milestone #64 remains open;
15. verify issues #290--#295 remain open/unexecuted;
16. verify no tag or GitHub Release was created.

The resulting canonical `origin/main` SHA becomes the predecessor for
WP03.

## WP03 boundary

A successful WP02 merge establishes the provider-independent application
contract on `main`.

It does not authorize WP03.

WP03 requires a separate authority with:

-   `release-1.13-` filename prefix;
-   explicit **GPT-5.6 Terra** selection;
-   dependency on accepted/merged WP02;
-   exact literal source/test/config documentation allowlist declared
    before mutation;
-   Vike historical OHLCV adapter implementation;
-   server-side secret handling;
-   canonical `BTC/USD`/`ETH/USD` translation;
-   `1h`/`4h`/`1d` translation;
-   response parsing and canonical validation;
-   bounded pagination/request behavior;
-   empirical resolution/documentation of the first-party rate-limit
    discrepancy;
-   verified attribution/licensing evidence;
-   preserved Twelve Data behavior;
-   no WP04 cache implementation;
-   no WP05/WP06 UI implementation;
-   continue-until-governance-boundary rule;
-   separate substantive acceptance and lifecycle.

## Required final report

Report:

-   pre-merge `main` SHA;
-   PR #298 state/base/head;
-   confirmation head matched the Luna-accepted candidate;
-   exact changed paths;
-   pre-merge validation results;
-   technical failures encountered/corrected;
-   governance restrictions encountered;
-   merge method;
-   merge commit SHA;
-   canonical post-merge `origin/main` SHA;
-   proof accepted head is reachable;
-   root README unchanged;
-   post-merge build/test results;
-   Twelve Data preservation result;
-   confirmation no Vike/cache/UI/package/schema/deployment work
    occurred;
-   issue #289 final state;
-   Project #2 WP02 status and Release field;
-   milestone #64 state;
-   issues #290--#295 state;
-   confirmation no tag/GitHub Release;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP02 PR #298 MERGE AND POST-MERGE VERIFICATION: PASS`

or

`RELEASE 1.13 WP02 PR #298 MERGE AND POST-MERGE VERIFICATION: BLOCKED`

A PASS establishes WP02 on canonical `main`. It does not authorize WP03
implementation.

# Release 1.13 WP01 --- Luna Final Substantive Acceptance of PR #297

## Selected model

**Execution model: GPT-5.6 Luna**

This is a substantive governance/architecture acceptance operation. Luna
owns Release 1.13 WP01 contract, architecture, engineering selections,
and final substantive acceptance.

Model roles:

-   **GPT-5.6 Luna** --- sole substantive acceptance authority for this
    operation.
-   **GPT-5.6 Terra** --- no substantive acceptance authority here;
    Terra may perform a later separately authorized lifecycle/merge
    operation.
-   **GPT-5.6 Sol** --- supporting analysis only; no acceptance
    authority.

## Repository and candidate

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Candidate:

`PR #297 — Release 1.13 WP01 contract and engineering selection`

Expected base:

`main @ 2621a3355aa5b28f1541c482d8053358b4ea814e`

Expected head:

`02d9025477a65282aca2f8fbf953488f9ac1d41d`

Expected branch:

`docs/release-1.13-wp01-contract`

Verify all values against live repository truth before deciding
acceptance.

## Purpose

Review PR #297 as the Release 1.13 WP01 architecture/governance
candidate and determine whether it substantively satisfies the canonical
Release 1.13 definition, execution plan, WP01 authority, Release 1.12
governance pattern, and inherited architecture constraints.

This authority permits Luna to correct defects in the WP01 documentation
candidate when those corrections remain inside the existing WP01
documentation mutation boundary.

This authority does **not** authorize merging PR #297.

It does **not** authorize WP02 implementation.

## Required review

Inspect at minimum:

-   PR #297 metadata, commits, changed paths, and complete diff;
-   `docs/roadmap/release-1.13/RELEASE_1.13_DEFINITION.md`;
-   `docs/roadmap/release-1.13/RELEASE_1.13_EXECUTION_PLAN.md`;
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`;
-   candidate `docs/roadmap/release-1.13/RELEASE_1.13_WP01_CONTRACT.md`;
-   candidate WP01 Luna authority;
-   relevant Release 1.12 Luna substantive-acceptance/lifecycle
    precedents;
-   relevant current architecture/source boundaries where needed to test
    whether the contract is implementable without bypass;
-   current issue #288 and Project #2 state.

Verify root `README.md` is absent from the candidate diff.

## Exact candidate mutation boundary

Expected changed paths are exactly:

-   `docs/roadmap/release-1.13/RELEASE_1.13_WP01_CONTRACT.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
-   `docs/roadmap/release-1.13/prompters/release-1.13-wp01-luna-contract-architecture-ui-acceptance-engineering-selection.md`

Do not accept unexplained additional paths.

Corrections under this acceptance authority are limited to these same
WP01 governance/documentation paths unless repository truth demonstrates
that a different documentation-only path was already explicitly
authorized by the canonical WP01 authority.

`README.md` remains forbidden.

## Substantive acceptance gates

### Product/UI contract

Confirm the candidate freezes:

-   public symbols exactly `BTC/USD` and `ETH/USD`;
-   default Market Research surface;
-   default `BTC/USD`, `1h`, `30D`;
-   supported intervals `1h`, `4h`, `1d`;
-   supported ranges `1D`, `7D`, `30D`, `90D`;
-   full 2 × 3 × 4 matrix unless a later governed validation proves a
    bounded technical incompatibility;
-   candlesticks and volume;
-   OHLCV hover;
-   bounded zoom/pan;
-   provenance and UTC freshness;
-   controlled loading/empty/stale/unavailable states;
-   informational ML & Automation Studies;
-   truthful secondary System Health;
-   no trading controls/execution.

### Canonical candle contract

Confirm the candidate is sufficiently precise for later implementation
without provider leakage, including:

-   canonical symbol;
-   interval;
-   UTC open-time semantics;
-   OHLCV;
-   decimal precision expectations;
-   ordering and uniqueness;
-   malformed/incomplete rejection;
-   provider metadata separation;
-   freshness/acquisition semantics.

Identify ambiguities that could cause WP02/WP03/WP04 to implement
incompatible representations.

### Provider abstraction

Confirm the contract preserves:

`MarketDataProvider -> provider adapter -> canonical OHLCV -> cache/read model -> Streamlit`

Confirm it does not authorize a microservice, broker, generalized plugin
system, direct UI/provider access, browser/provider access, or parallel
uncontrolled pipeline.

Confirm the request/response/failure semantics are sufficiently bounded
for WP02 and WP03.

### Vike evidence

Review the first-party Vike evidence cited by the candidate.

Do not overstate license/access claims.

The known first-party rate-limit discrepancy must remain explicitly
unresolved for WP03 empirical validation rather than being silently
resolved by assumption.

If first-party evidence now materially contradicts the candidate
architecture or intended public use, treat that as a governance
restriction requiring reconciliation rather than accepting stale
assumptions.

### Twelve Data preservation

Confirm existing Twelve Data integration remains preserved and outside
anonymous public historical-chart fallback behavior.

### Cache-first contract

Confirm ordinary Streamlit reruns, hover, zoom, pan, and client-side
chart interactions do not imply provider requests.

Confirm cache key/freshness/stale/provider-unavailable semantics are
sufficiently defined for WP04 while avoiding premature schema
implementation.

### Plotly engineering selection

Confirm Plotly is a defensible architecture selection for the frozen UI
contract.

Acceptance of Plotly as an engineering selection is **not** authority to
install it.

Verify the contract correctly defers exact
package/version/license/bundle/F1 validation and package-manifest
mutation to a later explicit implementation authority.

If the existing dependency set already provides an equivalent governed
capability, reconcile that before accepting an unnecessary new
dependency.

### Architecture/no-bypass

Confirm:

-   canonical ownership remains governed;
-   Streamlit remains a presentation/read-model consumer;
-   Streamlit does not supervise Worker;
-   Streamlit does not call providers directly;
-   secrets do not reach browser/public UI;
-   System Health remains truthful;
-   no second uncontrolled acquisition pipeline is created.

### F1 / zero-cost boundary

Confirm the contract remains implementable under Azure App Service Linux
F1 constraints and preserves:

`ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`

No paid service/feed or unnecessary persistent service may be implied.

### Scope and future ownership

Confirm the candidate does not itself grant broad source mutation
authority.

WP02 and later work packages must still receive literal path allowlists
and separate authority before implementation.

## Continue-until-governance-boundary rule

Do not stop on an ordinary correctable documentation/governance defect.

If review finds a defect that can be repaired within the three-path WP01
candidate boundary, correct it on the existing PR branch, rerun
validation, and continue review.

Examples include inconsistent wording, an incomplete acceptance
criterion, an internal contradiction, Markdown/whitespace defects,
incorrect cross-references, or an architecture statement that can be
clarified without changing release scope.

After each correction, review the resulting candidate rather than
accepting the pre-correction state.

Stop only when the necessary correction would cross governance, such as
requiring:

-   application/test source mutation;
-   dependency installation;
-   schema/database change;
-   provider market-data acquisition not already authorized;
-   Azure/Docker/GHCR mutation;
-   root README mutation;
-   scope expansion;
-   a different public provider;
-   Release 1.14/2.0 implementation;
-   prohibited cost;
-   unavailable secrets;
-   an owner decision outside Luna's delegated Release 1.13 architecture
    authority.

Report such a stop as:

`GOVERNANCE RESTRICTION — STOPPED`

Technical failures that are corrected within authority should be
reported as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

## Validation

Before substantive acceptance, rerun or verify:

-   exact changed-path scope;
-   `git diff --check`;
-   root `README.md` unchanged;
-   `release-1.13-` prompter naming;
-   repository-standard secret scan;
-   no source/test/package/schema/Azure/Docker/GHCR/runtime mutation;
-   no Release 1.14 or 2.0 implementation;
-   preserved Release 1.11 status;
-   preserved Twelve Data boundary;
-   internal consistency among Definition, Execution Plan, File
    Manifest, and WP01 Contract;
-   no hidden authorization of WP02.

## GitHub lifecycle state

Issue #288 must remain open during substantive review unless established
repository governance explicitly dictates another state.

Do not mark it Done merely because substantive acceptance passes.

Do not merge PR #297.

Do not start WP02.

Do not close milestone #64.

A PASS authorizes the next separately governed lifecycle step: Terra
merge/post-merge verification of the accepted WP01 PR.

## Required final report

Report:

-   inspected `main` SHA;
-   PR #297 state/head/base;
-   exact changed paths;
-   substantive findings for each acceptance gate;
-   corrections made during review, if any;
-   final reviewed head SHA;
-   validation results;
-   root README unchanged confirmation;
-   issue #288 / Project state;
-   any technical failures corrected;
-   any governance restriction encountered;
-   explicit acceptance or rejection of the WP01 candidate;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP01 LUNA FINAL SUBSTANTIVE ACCEPTANCE: PASS`

or

`RELEASE 1.13 WP01 LUNA FINAL SUBSTANTIVE ACCEPTANCE: BLOCKED`

A PASS does not merge PR #297 and does not authorize WP02
implementation.

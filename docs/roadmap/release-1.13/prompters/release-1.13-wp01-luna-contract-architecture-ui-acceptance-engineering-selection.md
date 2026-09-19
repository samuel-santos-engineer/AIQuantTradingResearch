# Release 1.13 WP01 --- Contract, Architecture, UI Acceptance and Engineering Selection Authority

## Selected model

**Execution model: GPT-5.6 Luna**

WP01 is explicitly assigned to GPT-5.6 Luna by the canonical Release
1.13 execution plan because this work package freezes the release
contract, architecture, UI acceptance contract, supported interval/range
matrix, engineering selections, and literal implementation allowlists.

Model roles for this work remain:

-   **GPT-5.6 Luna** --- owns WP01 contract, architecture, governance
    decisions, engineering selections, and substantive acceptance.
-   **GPT-5.6 Terra** --- no WP01 implementation authority under this
    file; Terra is reserved for separately authorized implementation
    beginning with later work packages.
-   **GPT-5.6 Sol** --- supporting analysis/reconciliation only; Sol may
    inform decisions but does not independently redefine or accept the
    WP01 contract.

Do not delegate Luna's WP01 decision authority to Terra or Sol.

## Repository

`samuel-santos-engineer/AIQuantTradingResearch`

## Canonical predecessor

Before doing any work, verify current repository truth.

Expected canonical predecessor after the Release 1.13 planning merge:

`origin/main = 2621a3355aa5b28f1541c482d8053358b4ea814e`

PR #296 established Release 1.13 planning governance.

Do not blindly trust the expected SHA. Fetch current `origin/main`,
inspect intervening changes if any, and stop only if they create a
governance conflict that cannot be reconciled within this authority.

## Work package identity

**Release 1.13 WP01 --- Release Contract, Architecture, UI Acceptance
Contract & Engineering Selections**

Expected GitHub issue:

`#288`

Dependency:

`none`, other than accepted Release 1.13 planning governance on `main`.

WP02 must not begin until WP01 has passed its own substantive acceptance
and lifecycle requirements.

## Purpose

WP01 converts the Release 1.13 planning definition into a sufficiently
precise implementation contract that later Terra work packages can
execute without inventing product or architecture decisions.

WP01 must freeze:

1.  public product behavior;
2.  provider-independent architecture boundaries;
3.  canonical historical-candle semantics;
4.  supported symbol/interval/range matrix;
5.  cache/read-model responsibilities at contract level;
6.  UI acceptance criteria;
7.  engineering selections required before implementation;
8.  literal implementation path allowlists for later WPs, to the extent
    repository inspection makes them determinable;
9.  validation expectations;
10. no-bypass and security boundaries.

WP01 is architecture/governance work.

It does **not** implement the application.

## Required repository inspection

Inspect current `main` before creating or modifying WP01 artifacts.

At minimum inspect:

-   `docs/roadmap/release-1.13/RELEASE_1.13_DEFINITION.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
-   `docs/project/ROADMAP.md`
-   relevant Release 1.12 definition/execution/acceptance records
-   relevant Release 1.12 WP architecture and UI authorities
-   `docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md`
-   relevant Twelve Data acquisition/integration documentation
-   current Python/Streamlit presentation structure
-   current .NET market-data/pipeline/read-model structure
-   current tests covering the .NET/Python/read-model boundaries
-   current package/dependency manifests and exact pins
-   current container/runtime composition where it constrains design
-   current SQLite/persistence boundaries where they constrain caching
-   current System Health implementation
-   current GitHub issue #288 and Project #2 metadata

Inspect actual source paths to avoid inventing future allowlists.

Do not modify source while inspecting it.

## Protected root README

`README.md` remains protected.

Do not modify, reformat, regenerate, stage, or include root `README.md`
in WP01.

If WP01 reveals a future README update that would be useful, record it
as a later owner-reviewed documentation consideration only.

Success requires:

`README.md = UNCHANGED`

## Release 1.13 product contract

Preserve the canonical release identity:

**Release 1.13 --- Historical Market Visualization & Provider
Abstraction**

Release 1.13 demonstrates historical market-data visualization and
provider abstraction, not a trading terminal.

Public instruments:

-   `BTC/USD`
-   `ETH/USD`

Default first-screen state:

-   surface: `Market Research`
-   instrument: `BTC/USD`
-   interval: `1h`
-   range: `30D`

Top-level conceptual navigation:

`Market Research | ML & Automation Studies | System Health`

The chart must dominate the Market Research first-screen hierarchy.

## Freeze the supported interval/range matrix

The planning candidates are:

Intervals:

-   `1h`
-   `4h`
-   `1d`

Ranges:

-   `1D`
-   `7D`
-   `30D`
-   `90D`

WP01 must determine and document the exact supported matrix after
inspecting:

-   expected candle counts;
-   Vike historical OHLCV semantics and first-party API documentation;
-   cache/resource implications;
-   Azure F1 constraints;
-   chart readability;
-   provider request limits;
-   current architecture.

Prefer the full candidate matrix if it is technically coherent and
bounded.

Do not narrow it without a documented reason.

Do not add additional symbols, intervals, or ranges under WP01.

## Canonical candle contract

Define the provider-independent candle representation required by later
work.

At minimum freeze semantics for:

-   canonical symbol;
-   interval/timeframe;
-   candle open timestamp;
-   timezone;
-   open;
-   high;
-   low;
-   close;
-   volume;
-   ordering;
-   duplicate handling;
-   missing/null handling;
-   numerical representation/precision expectations;
-   malformed candle rejection;
-   provider metadata separation;
-   freshness/acquisition timestamp semantics.

The canonical representation must not leak Vike-specific field names
into the UI contract.

Provider-specific translation remains adapter-owned.

Do not perform a database migration in WP01.

## Provider abstraction contract

Freeze a lightweight provider abstraction appropriate to the current
repository.

The conceptual boundary remains:

`MarketDataProvider -> provider adapter -> canonical OHLCV -> cache/read model -> Streamlit`

Do not introduce:

-   a microservice architecture;
-   a generalized plugin ecosystem;
-   a message broker;
-   a second independent pipeline;
-   direct Streamlit-to-provider access;
-   direct browser-to-provider access.

Determine the smallest repository-native interface/Protocol/ABC or
equivalent contract suitable for the current codebase.

Specify method semantics, inputs, outputs, error categories, and
cancellation/timeout expectations sufficiently for WP02/WP03
implementation.

Do not implement the abstraction in WP01.

## Vike engineering-selection evidence

Vike remains the intended public historical OHLCV source.

WP01 must verify current first-party Vike documentation relevant to the
architecture, including:

-   historical OHLCV availability for BTC and ETH;
-   supported intervals needed by the selected matrix;
-   request/response limits;
-   pagination/export semantics where relevant;
-   authentication/API-key handling;
-   attribution requirements;
-   licensing/access language relevant to the intended public
    research/demo use.

Do not overstate licensing.

Clearly distinguish:

-   explicit first-party statements;
-   architecture assumptions;
-   matters that WP03 must empirically validate.

If current first-party evidence materially contradicts the Release 1.13
plan, this is a governance issue: document it and stop rather than
silently selecting a different public provider.

Do not call the Vike market-data API in WP01 unless the canonical
planning authority explicitly permits provider calls. Documentation
inspection is allowed; market-data acquisition is not.

## Twelve Data preservation contract

The existing Twelve Data integration must remain intact.

WP01 must document how the provider abstraction can preserve Twelve Data
for:

-   private/internal research;
-   development;
-   integration testing;
-   provider comparison;
-   real-time market-data research;
-   future expansion.

Twelve Data must not become the automatic anonymous public-chart
fallback.

Do not remove, rewrite, or migrate the existing Twelve Data
implementation in WP01.

## Cache-first architecture contract

Freeze the intended responsibility chain:

`UI -> governed read model/service -> cache -> provider when required`

The contract must prevent ordinary:

-   Streamlit reruns;
-   hover;
-   zoom;
-   pan;
-   purely client-side chart interactions

from producing provider requests.

Define, at contract level:

-   cache key dimensions;
-   freshness metadata;
-   cache-hit behavior;
-   provider-refresh behavior;
-   stale-but-usable behavior;
-   no-cache/provider-unavailable behavior;
-   bounded retention expectations;
-   atomic/read-safe presentation handoff;
-   resource constraints appropriate to Azure F1.

Do not implement cache persistence or schema changes in WP01.

If a schema change appears necessary, record it as a governance decision
requiring explicit later authority rather than performing it now.

## UI acceptance contract

WP01 must freeze verifiable acceptance criteria for the public UI.

### Market Research

Required:

-   default `BTC/USD`;
-   default `1h`;
-   default `30D`;
-   BTC/USD and ETH/USD selector;
-   supported interval control;
-   supported range control;
-   historical candlestick chart;
-   volume;
-   hover OHLCV inspection;
-   bounded zoom/pan;
-   provenance;
-   last-updated/freshness timestamp;
-   responsive bounded layout;
-   clear research/demo framing;
-   no trade controls.

### Data provenance

Public market data must visibly identify Vike as the historical data
source when Vike-backed data is presented.

Use a concise provenance line substantially equivalent to:

`Data source: Vike • Historical OHLCV • Last updated: <UTC timestamp>`

Freeze timestamp semantics and UTC presentation.

### Loading state

Define a controlled loading state that does not expose internal/provider
details.

The architecture should favor cache/local-data-first presentation rather
than making every first render visibly wait on a provider request.

### Empty state

Define behavior for a valid request that returns no usable candles.

### Cached/stale state

If valid cached data is available while refresh/provider access is
unavailable, keep the chart usable and communicate truthful
freshness/staleness without presenting an alarming internal error.

### Provider-unavailable/no-cache state

Use controlled public wording substantially equivalent to:

`Historical market data is temporarily unavailable. Please try again later.`

Do not expose internal exception details.

### Forbidden public information

Never expose:

-   API keys;
-   secrets;
-   raw provider errors;
-   provider request URLs containing sensitive information;
-   stack traces;
-   SQLite/internal file paths;
-   HTTP traces;
-   machine-local paths;
-   internal exception messages.

### ML & Automation Studies

This surface is informational only in Release 1.13.

It must explain the research progression toward technical features and
Release 2.0 ML evaluation without implementing models, signals,
predictions, or trading automation.

### System Health

Preserve existing truthful System Health behavior as a secondary
technical surface.

Do not allow Market Research work to bypass or falsify existing health
semantics.

### Footer/research boundary

Retain clear wording substantially equivalent to:

`Research and demonstration application • No trade execution`

## Chart engineering selection

WP01 must inspect the current Python/Streamlit dependency set and
determine the charting approach for WP05.

The selection must support, at minimum:

-   candlesticks;
-   volume;
-   hover OHLCV;
-   zoom/pan;
-   BTC/USD and ETH/USD historical data;
-   the selected interval/range matrix;
-   acceptable Streamlit integration;
-   bounded Azure F1 resource use.

Prefer existing dependencies/capabilities when they satisfy the
contract.

Do not add a dependency in WP01.

If a new chart package is genuinely required, document:

-   candidate;
-   exact reason;
-   license;
-   expected pinned version selection process;
-   bundle/runtime implications;
-   why existing capabilities are insufficient;
-   which later WP requires explicit package-addition authority.

A package decision in WP01 is not package installation authority.

## Architecture/no-bypass rules

WP01 must preserve these boundaries unless current repository truth
demonstrates a conflict requiring Luna-level reconciliation:

-   canonical pipeline ownership remains governed;
-   Streamlit remains a presentation/read-model consumer;
-   Streamlit does not supervise the Worker;
-   Streamlit does not open provider secrets in the browser;
-   UI does not call Vike directly;
-   UI does not bypass the governed historical-data service/read-model;
-   existing System Health remains truthful;
-   existing Twelve Data integration remains preserved;
-   no parallel uncontrolled acquisition pipeline;
-   no live-trading/order-execution path.

## Zero-cost and constrained-runtime requirements

Preserve:

`ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`

Architecture must remain appropriate for the existing Azure App Service
Linux F1 reference deployment.

Do not select a design that requires:

-   paid Azure services;
-   paid market-data feeds;
-   always-on background infrastructure outside the accepted deployment;
-   a new managed database;
-   a message broker;
-   high-memory processing;
-   unnecessary high-frequency provider polling.

## WP01 artifact strategy

Follow the Release 1.12 governance pattern rather than creating
arbitrary documentation.

Before mutation, inspect how comparable Release 1.12 WP
contract/selection work was recorded.

Create only the minimum Release 1.13 WP01 governance/architecture
records necessary to freeze the contract.

Every new Codex control/prompter Markdown filename must begin:

`release-1.13-`

The WP01 authority itself should be retained under:

`docs/roadmap/release-1.13/prompters/release-1.13-wp01-luna-contract-architecture-ui-acceptance-engineering-selection.md`

If additional WP01 architecture/selection documents are required,
determine their exact repository-native locations before mutation and
add those exact paths to the WP01 mutation manifest/report.

Do not create `RELEASE_1.13_ACCEPTANCE.md` yet.

## Mutation boundary

WP01 may mutate governance/architecture documentation only.

It may not mutate:

-   application source;
-   tests that execute application behavior;
-   dependency/package manifests;
-   lock files;
-   database/schema/migrations;
-   Azure configuration;
-   Dockerfiles/container composition;
-   GitHub Actions/runtime workflows;
-   provider credentials/configuration;
-   deployed resources;
-   tags/releases;
-   root `README.md`.

Before editing, produce the exact literal path allowlist derived from
repository inspection.

Do not use broad directory wildcards as implementation authority.

If the necessary WP01 contract cannot be recorded without modifying a
path outside documentation/governance surfaces, stop at the governance
boundary and report the required additional authority.

## GitHub issue / Project governance

Verify issue #288 corresponds to WP01.

During active WP01 work, preserve its Release `1.13` identity.

Follow the established Project #2 status lifecycle.

Do not mark WP01 Done merely because a draft contract exists.

Do not start or mutate WP02--WP08 implementation.

Do not close milestone #64.

Do not alter milestone #65 beyond its existing Release 1.14 reservation.

## Continue-until-governance-boundary rule

Codex must continue diagnosing and correcting ordinary failures within
this authority.

A technical failure is not, by itself, a reason to stop.

For failures such as:

-   Markdown formatting;
-   broken internal links;
-   inconsistent contract wording;
-   failed documentation validation;
-   `git diff --check`;
-   secret-scan false positives that can be safely resolved;
-   branch/PR synchronization issues;
-   missing governance cross-references;
-   other correctable defects within the literal WP01 documentation
    allowlist,

identify the root cause, make the smallest authorized correction, rerun
validation, and continue until the WP01 governance result is coherent.

Stop only when the next necessary action crosses an explicit governance
boundary.

Examples include needing to:

-   modify application source;
-   add/install a package;
-   modify tests outside authorized governance validation;
-   change schema/database;
-   perform provider market-data calls not authorized here;
-   perform Azure/Docker/GHCR mutation;
-   modify root `README.md`;
-   expand Release 1.13 scope;
-   implement Release 1.14 or Release 2.0;
-   expose/use unavailable secrets;
-   incur prohibited cost;
-   make a decision explicitly reserved for the owner or a different
    authority;
-   bypass repository protections.

When stopped for such a reason, report:

`GOVERNANCE RESTRICTION — STOPPED`

Do not disguise a governance boundary as a technical failure.

## Git workflow

Use a dedicated WP01 branch.

Do not push directly to `main`.

Do not merge the WP01 PR under this authority unless an existing
repository rule explicitly grants that lifecycle action; default to
stopping with the WP01 PR open for review.

Do not create tags or GitHub Releases.

Keep commits bounded and reviewable.

## Validation

Before declaring WP01 ready for substantive acceptance, verify at
minimum:

-   current `main` predecessor;
-   exact changed paths;
-   root `README.md` unchanged;
-   all new prompter names begin `release-1.13-`;
-   `git diff --check`;
-   documentation/internal-link consistency where applicable;
-   secret scan using the repository's established mechanism;
-   no source/test/package/schema/Azure/Docker/provider-runtime
    mutation;
-   no Release 1.14 implementation;
-   no Release 2.0 implementation;
-   no Product Release 1.11 resurrection;
-   preserved Twelve Data integration;
-   selected interval/range matrix is explicit;
-   canonical candle semantics are explicit;
-   provider abstraction contract is explicit;
-   cache-first semantics are explicit;
-   chart engineering selection is explicit;
-   UI acceptance states are explicit;
-   literal future WP path ownership/allowlists are documented as far as
    repository inspection permits;
-   zero-cost/F1 constraints remain explicit.

## WP01 acceptance boundary

This authority may produce a WP01 candidate contract and PR.

It does not self-certify final substantive acceptance merely because the
files were written.

Follow the Release 1.12 pattern: substantive acceptance must be explicit
and model-appropriate.

If repository convention requires a separate Luna
final-substantive-acceptance authority after the WP01 PR is prepared,
stop with the candidate PR ready and report that as the next action.

Do not allow WP02 to begin before WP01 acceptance/lifecycle completion
is established.

## Required final report

Return:

-   inspected `main` SHA;
-   WP01 issue #288 state;
-   branch name;
-   exact files inspected;
-   exact files created;
-   exact files modified;
-   exact literal mutation allowlist used;
-   architecture decisions frozen;
-   supported symbol/interval/range matrix;
-   canonical candle contract summary;
-   provider abstraction decision;
-   Vike first-party evidence reviewed;
-   Twelve Data preservation decision;
-   cache-first contract summary;
-   chart engineering selection;
-   UI acceptance contract summary;
-   future WP path ownership/allowlists established;
-   validation performed;
-   technical failures encountered and corrected;
-   governance restrictions encountered;
-   explicit confirmation root `README.md` is unchanged;
-   commit SHA(s);
-   PR number/URL if created;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP01 CONTRACT / ARCHITECTURE / UI ACCEPTANCE: PASS`

or

`RELEASE 1.13 WP01 CONTRACT / ARCHITECTURE / UI ACCEPTANCE: BLOCKED`

A PASS means the WP01 candidate contract is ready for the next governed
lifecycle/acceptance step. It does not authorize WP02 implementation.

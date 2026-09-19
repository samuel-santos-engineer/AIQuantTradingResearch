# AIQuantTradingResearch --- Release 1.13 / 1.14 Roadmap Reconciliation and Release 1.13 Planning Governance Authority

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

## Role and authority

Act as the repository governance/planning agent for the next bounded
roadmap transition after Release 1.12.

This authority is for governance and planning only.

Do not implement Release 1.13 application functionality.

Do not implement Release 1.14 functionality.

Do not implement Release 2.0 functionality.

Do not perform Azure deployment changes, provider API calls,
database/schema migrations, package additions, production configuration
changes, Docker/GHCR publication, tags, GitHub Releases, or live-trading
work.

Before mutation, inspect current `main` and reconcile all claims against
repository truth.

Preserve the project's established governance discipline and model-role
conventions. Where existing governance assigns final
architecture/governance authority to GPT-5.6 Luna, do not silently
weaken or redefine that ownership.

## Critical protected path

The repository root:

`README.md`

is explicitly protected.

DO NOT modify, stage, rewrite, reformat, regenerate, or include
`README.md` in any planning/governance PR created under this authority.

The owner will manually review the front-door README after the roadmap
milestones and governance have been established.

Any apparent README inconsistency caused by the new roadmap is
temporarily accepted and must be reported rather than repaired.

Success requires:

`README.md = UNCHANGED`

## Existing repository facts to verify

Verify these facts against current `main` rather than blindly assuming
them:

-   Release 1.12 is the current bounded public-reference-deployment
    release.
-   Product Release 1.11 remains abandoned/nonexistent.
-   Initiative-1.11 remains historical feasibility work and must not be
    reclassified as Product Release 1.11.
-   Release 2.0 currently represents Lightweight Machine Learning
    Evaluation.
-   Release 2.1 remains Machine Learning.
-   Release 2.2 remains Explainable AI.
-   Release 2.3 remains Backtesting.
-   Existing Twelve Data integration must be preserved.
-   Existing Streamlit/System Health architecture must be preserved
    unless explicitly extended by Release 1.13.
-   Existing Azure App Service Linux F1 /
    zero-recurring-infrastructure-cost constraints remain inherited.
-   Existing canonical architecture and no-bypass rules remain
    authoritative unless Release 1.13 explicitly and narrowly extends
    them.

Inspect at minimum:

-   `docs/project/ROADMAP.md`
-   `docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md`
-   `docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md`
-   relevant Release 1.12 acceptance/governance evidence
-   relevant market-data architecture documentation
-   relevant Twelve Data documentation
-   relevant Streamlit/System Health implementation/governance
-   existing Release 2.0 milestone/roadmap definitions
-   GitHub milestones and Project release taxonomy, where accessible

Do not modify historical Release 1.12 governance merely to make its
historical sequence statement match the new future roadmap unless a
separate explicit authority requires such historical amendment.

Historical documents should remain truthful records of the authority
under which they were created.

## Canonical forward release sequence

Establish the forward roadmap as:

`1.10 → 1.12 → 1.13 → 1.14 → 2.0 → 2.1 → 2.2 → 2.3`

Release 1.11 remains abandoned/nonexistent.

Initiative-1.11 remains a non-release feasibility predecessor.

Do not renumber or repurpose existing Release 2.0, 2.1, 2.2, or 2.3
identities.

## Release 1.13 identity

Use the release identity:

**Release 1.13 --- Historical Market Visualization & Provider
Abstraction**

Release 1.13 is a bounded historical-market visualization release built
on the Release 1.12 deployment foundation.

Its purpose is to establish a useful public market-research interface
while introducing a clean provider-independent market-data boundary.

## Release 1.13 product scope

The public market-research interface is limited to:

-   BTC/USD
-   ETH/USD
-   historical OHLCV
-   candlestick visualization
-   volume visualization
-   bounded interval selection
-   bounded historical-range selection
-   hover inspection
-   zoom/pan where supported without provider round trips
-   data provenance
-   data freshness timestamp
-   controlled loading/empty/error states
-   public research/demo framing

Release 1.13 is not a trading terminal.

Explicitly exclude:

-   order entry
-   brokerage integration
-   portfolio management
-   P&L
-   live trading
-   automated trading
-   strategy recommendations
-   buy/sell markers
-   ML predictions
-   ML model execution
-   technical indicators reserved for Release 1.14
-   backtesting
-   production SLA/HA claims

## Release 1.13 first-screen contract

The default public experience must be governed as:

-   primary surface: `Market Research`
-   default instrument: `BTC/USD`
-   default interval: `1h`
-   default historical range: `30D`
-   primary visualization: candlestick chart
-   volume displayed with the historical market series
-   visible OHLCV inspection on hover
-   bounded zoom/pan interaction
-   data provenance visible
-   last-updated/freshness information visible
-   research/demo boundary visible
-   no trading controls

Initial governed interval candidates:

-   `1h`
-   `4h`
-   `1d`

Initial governed range candidates:

-   `1D`
-   `7D`
-   `30D`
-   `90D`

If implementation feasibility or provider semantics require a narrower
combination matrix, WP01 must freeze the valid combinations before
implementation rather than silently inventing behavior.

The chart must dominate the first-screen information hierarchy.

Infrastructure details must not dominate the visitor experience.

## Primary navigation contract

Release 1.13 should establish three conceptual top-level surfaces:

`Market Research | ML & Automation Studies | System Health`

### Market Research

This is the default visitor surface.

It contains the governed historical BTC/USD and ETH/USD market
visualization.

### ML & Automation Studies

In Release 1.13 this is a roadmap/research-information surface only.

It must communicate the intended progression:

`Historical Market Data → Technical Analysis / Quantitative Features → ML Evaluation → Governed Automation Studies`

It may describe future work but MUST NOT implement:

-   ML training
-   inference
-   feature engineering belonging to future releases
-   strategy signals
-   automated decisions
-   automated trading
-   order execution

It should clearly identify that ML evaluation begins with Release 2.0.

### System Health

System Health remains available as the secondary technical/engineering
surface.

Preserve truthful existing health semantics and no-bypass architecture.

Market Research, not System Health, becomes the default visitor-facing
product surface.

## Provider architecture

Introduce a lightweight provider-independent market-data boundary.

The intended conceptual architecture is:

`MarketDataProvider` → provider-specific adapter → canonical historical
candles/OHLCV → cache/persistence boundary → presentation/read-model
boundary → Streamlit Market Research UI

Do not build an unnecessary microservice architecture or generalized
plugin framework.

The UI must not know provider-specific API semantics.

Provider-specific symbol translation belongs behind provider adapters.

Canonical public symbols are:

-   `BTC/USD`
-   `ETH/USD`

Provider selection is internal configuration, not a public visitor
control.

## Vike public historical-data role

Release 1.13 selects Vike as the intended public historical OHLCV source
for BTC/USD and ETH/USD.

The implementation must be server-side.

No API key may be exposed to the browser/public UI.

Prefer a cache-first historical-data model so ordinary Streamlit reruns,
hover, zoom, pan, and other presentation interactions do not generate
unnecessary provider calls.

The implementation should minimize:

-   provider requests
-   CPU usage
-   memory usage
-   persistent storage usage
-   unnecessary background processing

This is required because the project remains constrained by the existing
Azure F1 environment and zero-cost governance.

Visible Vike attribution/provenance must be present where the public
historical market data is shown.

Do not overstate a provider license in repository documentation. Record
only licensing/access claims supported by current first-party provider
evidence at implementation/acceptance time.

## Twelve Data boundary

Preserve the existing Twelve Data integration.

Twelve Data remains useful for:

-   private/internal research
-   development
-   integration testing
-   provider comparison
-   real-time market-data research
-   future research expansion

Under the project's current public-display boundary, Twelve Data market
values are not selected as the anonymous public historical-chart source.

Do not remove Twelve Data merely because Vike is introduced.

Do not make Twelve Data an automatic public-chart fallback.

The public interface should contain concise explanatory text
substantially equivalent to:

> Public historical visualization uses Vike market data. Twelve Data is
> retained for private/internal research, including real-time
> market-data studies. Twelve Data values from that research are not
> exposed through this public interface under the project's current
> data-access/licensing boundary.

Final wording may be polished during UI implementation, but the semantic
distinction is part of the Release 1.13 contract.

Provider/license claims must be verified against current first-party
provider terms before final acceptance.

## Cache-first behavior

Release 1.13 must prefer:

`UI → governed market-data service/read model → cache → provider when required`

over:

`UI → provider on every interaction`

Historical candles should be reusable from persistent/local cache where
practical.

A provider outage must not destroy an otherwise valid cached historical
visualization.

If valid cached data exists, the UI may continue presenting it with
truthful freshness information.

If neither valid cached data nor provider data is available, show a
controlled public unavailable state.

Never expose:

-   stack traces
-   API keys
-   secrets
-   raw provider errors
-   internal database paths
-   private endpoints
-   HTTP traces
-   machine-local information

through the public market interface.

## Release 1.14 reservation

Create/reserve the next roadmap milestone as:

**Release 1.14 --- Technical Analysis & Quantitative Feature
Foundation**

Release 1.14 follows 1.13 and precedes 2.0.

This authority reserves its purpose but DOES NOT authorize detailed
Release 1.14 implementation planning.

Its intended purpose is:

-   add selected technical-analysis indicators to the public
    market-research chart;
-   establish deterministic canonical technical-indicator calculations;
-   make those calculations reusable as quantitative features by later
    ML research;
-   avoid separate inconsistent UI-only and ML-only indicator
    calculations.

Conceptual future architecture:

`canonical OHLCV → Technical Analysis / Feature Layer → UI + ML research`

Candidate indicator families may include:

-   SMA
-   EMA
-   RSI
-   MACD
-   Bollinger Bands

These candidates are NOT a frozen Release 1.14 implementation list under
this authority.

The detailed indicator set, formulas, warm-up behavior, numerical
precision, libraries, testing, persistence, and UI behavior require
separate Release 1.14 planning authority.

Release 1.14 must not implicitly authorize trading recommendations or
execution.

## Release 2.0 relationship

Preserve Release 2.0 as:

**Lightweight Machine Learning Evaluation**

Release 2.0 follows Release 1.14.

Its existing deterministic/narrow ML-evaluation character must be
preserved unless separately redefined.

The intended progression is now:

`1.13 historical market visualization` →
`1.14 technical-analysis / quantitative-feature foundation` →
`2.0 lightweight ML evaluation` → later ML/explainability/backtesting
releases

Release 2.0 may eventually consume governed market and technical
features established by predecessor releases.

Do not turn this planning authority into authorization for ML
implementation.

Do not imply that ML output automatically becomes live trading.

## Required Release 1.13 governance artifacts

Create the Release 1.13 planning directory and canonical planning
artifacts:

-   `docs/roadmap/release-1.13/RELEASE_1.13_DEFINITION.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`

Preserve the established prompt naming convention. All Release 1.13
Codex control/prompter Markdown files created under
`docs/roadmap/release-1.13/prompters/` must use the lowercase hyphenated
prefix:

`release-1.13-`

The authority file for this planning operation is expected to use:

`docs/roadmap/release-1.13/prompters/release-1.13-historical-market-visualization-provider-abstraction-governance-authority-luna.md`

Related bootstrap, WP, acceptance, lifecycle, reconciliation, and later
authority prompts must likewise begin with `release-1.13-`, consistent
with the repository's established prompter pattern.

Do NOT create `RELEASE_1.13_ACCEPTANCE.md` as if acceptance already
exists.

Acceptance evidence belongs to executed work.

## Release 1.13 execution-plan decomposition

Design a dependency-ordered WP graph appropriate to the repository.

At minimum, the plan must distinctly govern:

1.  release contract / architecture / UI acceptance contract;
2.  provider-independent market-data abstraction;
3.  Vike historical OHLCV adapter and provider-boundary validation;
4.  cache-first historical data/read-model path;
5.  public Market Research UI implementation;
6.  provider attribution, Twelve Data research-boundary messaging, ML &
    Automation Studies navigation, and controlled public states;
7.  Azure F1 integration/stability/performance/no-bypass validation;
8.  documentation and final release acceptance.

You may refine WP boundaries after repository inspection.

Every WP must define:

-   selected model role;
-   dependencies;
-   exact purpose;
-   mutation boundary;
-   literal file/path allowlist before implementation;
-   tests/validation;
-   acceptance marker;
-   lifecycle rule;
-   explicit exclusions.

No implementation WP is authorized merely because it appears in the
execution plan.

Each requires separate authority.

## UI acceptance contract requirement

The Release 1.13 governance must contain enough UI detail that
implementation cannot redefine the product opportunistically.

Freeze at least:

-   first-load/default state;
-   navigation hierarchy;
-   instrument selector;
-   interval controls;
-   range controls;
-   candlestick presentation;
-   volume presentation;
-   OHLCV hover behavior;
-   zoom/pan expectations;
-   loading state;
-   empty state;
-   stale/cache state;
-   provider-unavailable state;
-   provenance;
-   freshness timestamp;
-   Vike attribution;
-   Twelve Data internal-research explanation;
-   ML & Automation Studies future-roadmap surface;
-   System Health relationship;
-   responsive/bounded layout expectations;
-   forbidden public information;
-   features explicitly deferred to Release 1.14 and 2.0.

Do not select a new chart dependency without a governed engineering
selection decision if repository policy requires one.

## Roadmap reconciliation

Update the authoritative project roadmap/governance surfaces necessary
to establish:

`1.12 → 1.13 → 1.14 → 2.0`

while preserving historical truth.

Do not mechanically rewrite every historical document containing the old
sequence.

Differentiate:

-   historical governance records;
-   current/future roadmap authority.

Update current/future authoritative roadmap surfaces only where
appropriate.

Again:

`README.md` is forbidden.

## GitHub milestone / Project governance

Inspect live GitHub milestone and Project state before mutation.

Establish milestone identities for:

-   Release 1.13 --- Historical Market Visualization & Provider
    Abstraction
-   Release 1.14 --- Technical Analysis & Quantitative Feature
    Foundation

Preserve existing Release 2.0+ milestone identities.

Do not repurpose an unrelated milestone number.

Do not create detailed Release 1.14 work-package issues.

Do not create Release 2.0 implementation issues.

For Release 1.13, create only the governance/project objects justified
by the planning pattern and keep implementation WPs unexecuted until
separately authorized.

If GitHub Project release taxonomy must be extended for 1.13 and 1.14,
do so only after inspecting the existing field/options and preserving
all existing values.

Do not duplicate existing milestone or Project identities.

## Zero-cost and constrained-runtime boundary

Release 1.13 inherits the existing near-term governance objective:

`ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`

Do not introduce a paid market-data feed, paid Azure service, paid
monitoring dependency, or unnecessary persistent service.

The design must remain appropriate for the existing constrained Azure
App Service Linux F1 reference environment.

## Git workflow

Use the repository's established governed branch/PR workflow.

Do not directly push release-planning changes to `main`.

Before mutation:

-   verify current `main`;
-   verify working state;
-   identify the exact planning mutation set;
-   verify no duplicate Release 1.13/1.14 governance already exists.

Create a dedicated governance/planning branch.

Keep the mutation bounded.

Before proposing merge, prove:

-   exact changed paths;
-   `README.md` unchanged;
-   all Release 1.13 prompter/control Markdown filenames use the
    `release-1.13-` prefix;
-   no implementation source mutation unless explicitly required for
    governance tooling itself;
-   no package/dependency mutation;
-   no secrets;
-   no Azure mutation;
-   no provider calls;
-   no Release 1.14 implementation;
-   no Release 2.0 implementation;
-   no historical Release 1.11 resurrection;
-   preserved Release 2.0/2.1/2.2/2.3 identities;
-   canonical forward sequence is coherent.

Do not merge unless the repository's applicable governance permits it
and all required gates pass.

If merge authority is not explicit, stop with the PR ready for review.

## Required final report

Return a concise governance report containing:

-   inspected `main` SHA;
-   repository inconsistencies found;
-   files created;
-   files modified;
-   explicit confirmation that root `README.md` was untouched;
-   Release 1.13 milestone identity/number;
-   Release 1.14 milestone identity/number;
-   Project taxonomy changes, if any;
-   Release 1.13 WP graph;
-   branch name;
-   commit SHA(s);
-   PR number/URL if created;
-   tests/validation performed;
-   blockers requiring owner decision;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 GOVERNANCE AND ROADMAP RECONCILIATION: PASS`

or

`RELEASE 1.13 GOVERNANCE AND ROADMAP RECONCILIATION: BLOCKED`

A PASS does not authorize Release 1.13 implementation.

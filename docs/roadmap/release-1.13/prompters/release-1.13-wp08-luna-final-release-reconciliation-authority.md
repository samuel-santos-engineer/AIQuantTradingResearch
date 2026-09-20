# Release 1.13 WP08 --- Luna Final Release Reconciliation Authority

## Selected model

**Execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns Release 1.13 final contract reconciliation, governance
review, and substantive release acceptance. GPT-5.6 Terra may execute
only implementation or lifecycle work explicitly authorized by a later,
separate authority. GPT-5.6 Sol may support analysis or reconciliation
but may not silently assume Luna governance authority.

## Canonical predecessor

WP08 begins only from canonical `main`:

`128bbb4e9462990f6edd1a0e068828db95be1da8`

This is the merge commit produced by the completed WP07 lifecycle.

WP07 accepted candidate:

`6c2664e2e3a75e72538a89f5e77d9fa1978bd80b`

Accepted deployed image digest:

`sha256:18f60a172d51ff3d4c2b1bdf1b8089ba989cb5ef149adc3e5cf9488c9482f829`

WP07 final substantive acceptance:

`RELEASE 1.13 WP07 AZURE F1 INTEGRATION STABILITY SECURITY AND ZERO-COST FINAL SUBSTANTIVE ACCEPTANCE: PASS`

WP07 merge/lifecycle:

`RELEASE 1.13 WP07 MERGE AND POST-MERGE LIFECYCLE: PASS`

## Purpose

WP08 is the **final Release 1.13 reconciliation and acceptance work
package**.

Its purpose is to independently reconcile the completed WP01--WP07
implementation against the canonical Release 1.13 definition, execution
plan, file manifest, accumulated governance authorities, accepted
runtime evidence, and repository lifecycle state.

This is not a presumption that Release 1.13 is complete. Luna must look
for contradictions, omissions, unauthorized drift, stale governance
text, missing acceptance evidence, or release-contract requirements that
have not actually been satisfied.

The default mode of this authority is **inspection, reconciliation, and
governance-document completion**, not implementation.

## Release identity

**Release 1.13 --- Historical Market Visualization & Provider
Abstraction**

The release demonstrates provider-independent historical market-data
visualization, not a trading terminal.

Public historical instruments:

-   `BTC/USD`
-   `ETH/USD`

Supported public intervals:

-   `1h`
-   `4h`
-   `1d`

Supported public ranges:

-   `1D`
-   `7D`
-   `30D`
-   `90D`

Public historical provider boundary:

-   Vike/cache for public historical OHLCV;
-   Twelve Data preserved for private/internal research and provider
    comparison;
-   Twelve Data must not silently become the anonymous public historical
    fallback.

Explicit Release 1.13 exclusions include:

-   technical indicators;
-   strategy signals;
-   forecasts;
-   buy/sell markers;
-   portfolio widgets;
-   order controls;
-   ML predictions;
-   automated trade execution.

Technical-analysis feature work remains reserved for Release 1.14. ML
implementation remains 2.0+.

## Frozen public acceptance contract

Luna must reconcile the implementation and evidence against the settled
first-screen contract:

-   application identity: `Market Research`;
-   default instrument: `BTC/USD`;
-   default interval: `1h`;
-   default range: `30D`;
-   BTC/USD and ETH/USD selectable;
-   intervals `1h`, `4h`, `1d`;
-   ranges `1D`, `7D`, `30D`, `90D`;
-   candlestick visualization;
-   distinct volume visualization;
-   hover OHLCV;
-   zoom and pan;
-   browser/client-side interaction where practical;
-   cache/local-first initial behavior;
-   valid cached data remains usable during provider unavailability;
-   no-cache/provider-unavailable public message exactly:
    `Historical market data is temporarily unavailable. Please try again later.`;
-   public provenance identifies Vike, historical OHLCV, and
    last-updated UTC timestamp when data is available;
-   footer:
    `Research and demonstration application • No trade execution`;
-   navigation:
    -   Market Research
    -   ML & Automation Studies
    -   System Health
-   System Health remains secondary;
-   ML & Automation Studies remains informational only.

## Canonical architecture to preserve

Reconcile that the accepted implementation still represents:

`Streamlit selection -> bounded local Worker invocation -> HistoricalQuery -> HistoricalMarketDataReadService -> cache -> permitted provider -> safe JSON -> Streamlit`

The provider-independent application abstraction is the
repository-native C# contract established by WP01/WP02.

The UI must not own provider-specific request logic.

Historical reads must remain cache-first rather than
API-per-interaction.

The Vike API key remains server-side only.

## WP01--WP07 reconciliation

Luna must inspect the canonical Release 1.13 governance and repository
evidence and reconcile each completed work package.

### WP01 --- Contract and architecture

Confirm the final implementation still conforms to the
provider-independent architecture and release boundary established by
WP01.

### WP02 --- Provider-independent contracts

Confirm canonical market-data contracts remain provider-independent and
no later WP leaked Vike/Twelve Data specifics into the application/UI
contract improperly.

### WP03 --- Vike adapter

Confirm the final adapter behavior, including the WP07 response-parser
correction, remains consistent with the accepted provider contract and
canonical candle validation.

### WP04 --- Cache/read model

Confirm the accepted cache-first behavior remains intact, including
fresh-cache zero-provider behavior, bounded acquisition, stale fallback,
empty-vs-unavailable distinction, concurrency controls, atomic
persistence, retention, and freshness policy.

### WP05 --- Public historical UI

Confirm the accepted Worker bridge and public visualization architecture
remain intact and no later work introduced an ungoverned persistent
companion service or API-per-rerun behavior.

### WP06 --- Messaging and informational surfaces

Confirm Vike provenance, UTC freshness, Twelve Data private/internal
boundary, ML informational surface, safe controlled states, System
Health relationship, and no-trade messaging remain consistent.

### WP07 --- Azure F1 qualification

Reconcile the completed WP07 evidence:

-   canonical merge commit `128bbb4e9462990f6edd1a0e068828db95be1da8`;
-   accepted deployed digest unchanged;
-   Azure App Service Linux F1/Free boundary;
-   HTTPS-only;
-   writable persistent cache path;
-   bounded process behavior;
-   successful BTC and ETH historical acquisition;
-   successful public BTC/ETH rendered-browser evidence;
-   alternate `4h` / `90D` interaction;
-   restored defaults;
-   Vike/UTC provenance;
-   corrected System Health behavior;
-   safe public disclosure;
-   zero recurring infrastructure cost target;
-   no secret exposure.

Do not reopen settled WP07 engineering work without contradictory
evidence.

## Governance-document reconciliation

Inspect the canonical Release 1.13 roadmap directory and determine
whether the final release state is accurately represented by:

-   `RELEASE_1.13_DEFINITION.md`
-   `RELEASE_1.13_EXECUTION_PLAN.md`
-   `RELEASE_1.13_FILE_MANIFEST.md`
-   existing Release 1.13 prompters/control artifacts
-   WP completion/validation documents already committed.

Prepare the canonical final acceptance artifact:

`docs/roadmap/release-1.13/RELEASE_1.13_ACCEPTANCE.md`

The acceptance document must state the selected model:

**GPT-5.6 Luna**

It must distinguish:

-   planned contract;
-   implemented evidence;
-   deployed evidence;
-   accepted limitations;
-   exclusions/deferred work;
-   lifecycle state;
-   final substantive acceptance conclusion.

Do not rewrite history or imply evidence that was not actually obtained.

If the definition, execution plan, or file manifest requires a small
reconciliation update to describe the final accepted implementation
accurately, Luna may update only those canonical Release 1.13 governance
documents when the change is documentary reconciliation and does not
alter implementation scope.

## Markdown naming convention

All newly created Release 1.13 Codex control/prompter Markdown files
must use the lowercase hyphenated prefix:

`release-1.13-...`

Canonical release documents retain the established uppercase/underscore
naming convention.

Every Markdown artifact created or materially updated under this
authority must explicitly identify the GPT-5.6 model role used for that
artifact.

## Root README protection

The repository root/front-door:

`README.md`

is **protected and must remain byte-for-byte unchanged**.

The user will review it separately after the roadmap milestones are
established.

No WP08 acceptance result authorizes a root README update.

## Permitted mutation scope

Luna may create/update only the smallest Release 1.13 governance
artifacts required for final reconciliation.

Expected canonical target:

-   `docs/roadmap/release-1.13/RELEASE_1.13_ACCEPTANCE.md`

Potential reconciliation targets, only if demonstrably necessary:

-   `docs/roadmap/release-1.13/RELEASE_1.13_DEFINITION.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
-   the WP08 Luna authority/prompter path using the required
    `release-1.13-...` prefix.

Before mutation, freeze the exact literal file allowlist actually
required.

Do not add a file merely because it is listed as a potential target.

If reconciliation requires any
implementation/source/test/deployment/configuration path, stop and
report the governance restriction rather than expanding authority
implicitly.

## Repository and lifecycle state to verify

Before final acceptance, independently verify current GitHub state,
including:

-   canonical `main` equals or descends correctly from
    `128bbb4e9462990f6edd1a0e068828db95be1da8`;
-   PR #304 is merged;
-   WP07 issue #294 is closed/completed;
-   WP08 issue #295 remains Open while reconciliation is being
    performed;
-   the Release 1.13 milestone #64 remains Open;
-   no Release 1.13 tag exists;
-   no Release 1.13 GitHub Release exists;
-   root README remains unchanged;
-   no unexpected Release 1.14 or 2.0 implementation has been pulled
    into 1.13.

If `main` has advanced since the WP07 boundary, reconcile every
intervening commit relevant to Release 1.13 before accepting the
release. Do not assume later commits are harmless.

## Validation expectations

Use repository-native checks proportionate to final release
reconciliation.

At minimum, inspect or obtain reliable evidence for:

-   release build health;
-   relevant .NET test suites;
-   Python presentation tests;
-   architecture tests;
-   Vike/provider tests;
-   static/whitespace checks;
-   secret scanning;
-   governance diff;
-   exact changed-path reconciliation.

Do not rerun expensive or redundant checks solely for ceremony when
immutable, accepted evidence remains applicable and no relevant code
changed. Conversely, do not reuse stale evidence if canonical code
affecting that evidence has changed.

## Security and disclosure reconciliation

Confirm the final public behavior does not expose:

-   Vike API key or other credentials;
-   raw provider payloads;
-   raw stderr;
-   stack traces;
-   internal exceptions;
-   `/app/...` paths;
-   Worker filesystem path;
-   cache filesystem path;
-   database/internal storage paths.

Confirm the public UI continues to communicate controlled unavailable
states rather than implementation details.

## Azure F1 / zero-cost reconciliation

Release 1.13 must remain compatible with the accepted constrained
deployment:

-   Azure App Service Linux F1;
-   shared/free capacity;
-   no new paid recurring infrastructure;
-   persistent `/home`;
-   bounded CPU/process/provider behavior;
-   cache-first operation;
-   no unnecessary background service.

Do not change Azure resources or configuration under this authority.

## Adversarial acceptance requirement

This is not a documentation rubber stamp.

Luna must actively search for:

-   release-definition requirements with no implementation evidence;
-   implementation behavior absent from or conflicting with governance;
-   stale manifest entries;
-   accidental scope creep;
-   hidden dependency/schema changes;
-   provider-boundary violations;
-   UI behavior that contradicts the frozen acceptance contract;
-   acceptance claims supported only by inference;
-   lifecycle inconsistencies;
-   release artifacts that claim completion prematurely.

If a material defect exists, return BLOCKED even if all prior WPs
individually passed.

## Correction boundary

Luna may correct ordinary documentary reconciliation defects only inside
the final frozen WP08 governance allowlist.

Luna may **not** correct implementation defects under this authority.

If final reconciliation discovers a
source/test/runtime/provider/cache/UI/deployment defect, report:

1.  exact defect;
2.  evidence;
3.  minimum files or systems requiring correction;
4.  recommended execution model;
5.  required new authority.

Do not silently grant Terra authority.

## Retry-until-governance-boundary rule

Do not stop merely because a governance inspection, validation,
repository query, documentation generation, formatting, lint, or
reconciliation step fails.

Diagnose the failure, make corrections permitted by this authority,
rerun the relevant checks, and continue until the authorized acceptance
conditions pass.

Stop only if the next corrective action would require exceeding this
authority or crossing an explicit governance restriction.

Distinguish:

`TECHNICAL FAILURE — CONTINUE FIXING`

from:

`GOVERNANCE RESTRICTION — STOPPED`

When blocked, report the exact restriction, failed evidence, corrective
action required, and do not perform that action without new authority.

## Final substantive PASS standard

WP08 substantive PASS requires Luna to conclude, based on independently
reconciled evidence, that:

-   WP01--WP07 collectively satisfy the Release 1.13 definition;
-   canonical implementation matches the accepted architecture;
-   public BTC/USD and ETH/USD historical visualization contract is
    satisfied;
-   cache-first/provider boundaries remain intact;
-   Vike public provenance and Twelve Data private/internal boundary are
    correct;
-   public failure/disclosure behavior is controlled;
-   Azure F1/zero-cost constraints remain satisfied;
-   Release 1.13 exclusions remain excluded;
-   governance documents accurately represent the final implementation;
-   no material unresolved implementation or governance defect remains;
-   root README remains untouched;
-   no Release 1.14/2.0 scope was pulled forward improperly.

## WP08 completion boundary

Even if substantive acceptance passes, Luna must **not**:

-   merge its own WP08 governance PR unless separately authorized;
-   close issue #295;
-   move #295 to Done;
-   close milestone #64;
-   create a Release 1.13 tag;
-   create a GitHub Release;
-   modify root README;
-   begin Release 1.14.

A PASS authorizes only creation/execution of a separate Terra final
merge/release-lifecycle authority.

## Required completion report

Report:

-   starting canonical `main` SHA;
-   current canonical `main` SHA and any intervening commits;
-   WP01--WP07 reconciliation outcome;
-   exact WP08 changed-path allowlist;
-   governance documents created/updated;
-   validation evidence relied upon and rerun;
-   Release 1.13 contract reconciliation;
-   architecture/provider/cache/UI/security/F1 conclusions;
-   exclusions/deferred-work reconciliation;
-   README protection result;
-   issue #294 state;
-   issue #295 state;
-   milestone #64 state;
-   tag/GitHub Release state;
-   any defects found and corrections made;
-   any governance restrictions encountered;
-   exact next lifecycle authority if PASS.

End with exactly one of:

`RELEASE 1.13 WP08 FINAL RELEASE RECONCILIATION AND SUBSTANTIVE ACCEPTANCE: PASS`

or

`RELEASE 1.13 WP08 FINAL RELEASE RECONCILIATION AND SUBSTANTIVE ACCEPTANCE: BLOCKED`

A PASS is substantive release acceptance only. It does not itself
authorize merge, issue closure, milestone closure, tag creation, GitHub
Release publication, root README modification, or Release 1.14
execution.

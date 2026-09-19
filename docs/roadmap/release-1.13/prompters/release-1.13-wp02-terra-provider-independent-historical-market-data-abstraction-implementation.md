# Release 1.13 WP02 --- Provider-Independent Historical Market-Data Abstraction Implementation Authority

## Selected model

**Execution model: GPT-5.6 Terra**

WP02 is assigned to GPT-5.6 Terra by the canonical Release 1.13
execution plan.

Model roles:

-   **GPT-5.6 Luna** --- owns the canonical Release 1.13/WP01 contract
    and any substantive architecture change.
-   **GPT-5.6 Terra** --- owns this bounded implementation and empirical
    validation work.
-   **GPT-5.6 Sol** --- supporting analysis only; no authority to
    redefine the WP01 contract.

Terra must implement the accepted contract, not redesign it.

## Repository and predecessor

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Expected canonical predecessor:

`origin/main = dc0f227bedad2135d8f41cbcd913948251e1a945`

This is the merged WP01 baseline.

Verify current live repository truth before mutation. If `main` has
advanced, inspect intervening commits and continue only if they do not
conflict with WP02 governance.

## Work package identity

**Release 1.13 WP02 --- Provider-Independent Historical Market-Data
Abstraction**

Expected issue:

`#289`

Dependency:

WP01 is substantively accepted, merged, and lifecycle-complete.

WP03 must not begin under this authority.

## Purpose

Implement and test the smallest repository-native, application-owned
historical market-data abstraction required by the accepted WP01
contract.

WP02 must establish provider-independent types/contracts for:

-   canonical historical candle;
-   canonical symbol;
-   interval;
-   range/request semantics;
-   provider request;
-   provider response;
-   provenance/freshness metadata;
-   typed failure categories;
-   cancellation;
-   bounded deadline/timeout semantics.

The implementation must be sufficient for WP03 to add a Vike adapter
without changing the public abstraction contract merely to accommodate
Vike transport details.

## Canonical contract to implement

Preserve the merged WP01 semantics.

The provider-independent candle includes:

-   `CanonicalSymbol`
-   `Interval`
-   `OpenTimeUtc`
-   `Open`
-   `High`
-   `Low`
-   `Close`
-   `Volume`

Requirements:

-   decimal precision for price/volume;
-   UTC open/left-boundary timestamp;
-   strict ascending ordering in successful result sets;
-   uniqueness by canonical symbol + interval + open time;
-   non-null complete OHLCV records;
-   rejection of malformed/inconsistent OHLC semantics;
-   no provider-specific field names or transport envelopes;
-   closed-candle status represented in acquisition/read-model metadata,
    not inferred by UI timing.

Provider metadata remains separate and includes the contractually
required provider/provenance/freshness information.

## Supported public domain

The abstraction must support exactly the Release 1.13 public matrix:

Symbols:

-   `BTC/USD`
-   `ETH/USD`

Intervals:

-   `1h`
-   `4h`
-   `1d`

Ranges:

-   `1D`
-   `7D`
-   `30D`
-   `90D`

Do not add public symbols, intervals, or ranges.

Do not encode Vike's provider-specific `BTC`/`ETH` symbol mapping into
canonical types.

## Provider abstraction semantics

Implement the smallest repository-native application-owned boundary
consistent with:

`MarketDataProvider -> provider adapter -> canonical OHLCV -> cache/read model -> Streamlit`

A request accepts only provider-independent concepts needed by WP01:

-   canonical symbol;
-   interval;
-   range;
-   caller cancellation;
-   bounded deadline/timeout semantics.

A successful response returns canonical candles plus separate
provenance/freshness metadata.

Typed failures must cover the accepted categories:

-   invalid request;
-   unavailable;
-   rate limited;
-   authentication/configuration;
-   malformed response;
-   timeout;
-   cancellation.

Choose repository-native C# naming/type shapes after inspecting current
conventions.

Do not expose provider URLs, cursors, API-key concepts, raw HTTP
responses, or Vike field names through the application abstraction.

## Mandatory literal allowlist discovery before mutation

The merged WP01 contract deliberately did not grant broad source
authority.

Before editing any source or test file:

1.  inspect the current application/infrastructure/test layout;
2.  inspect
    `src/AIQuantTradingResearch.Application/Research/IObservationSource.cs`;
3.  inspect current market-data domain/application abstractions;
4.  inspect
    `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/`
    as a preservation boundary;
5.  inspect project files only to understand compilation ownership;
6.  inspect tests and naming conventions;
7.  determine the **minimum exact literal source/test path set**
    required for WP02.

Then print/report that exact allowlist before first mutation.

The allowlist may include:

-   new application-owned source files required for the abstraction;
-   new/modified test files directly testing those abstractions;
-   the WP02 authority prompter retained under the Release 1.13
    prompters directory;
-   `RELEASE_1.13_FILE_MANIFEST.md` only if needed to record the
    now-frozen WP02 literal ownership.

Do not use directory wildcards.

Do not treat inspection of a file as mutation authority.

### Escalation rule for uncertain paths

If implementation requires modifying an existing production source file
whose mutation was not clearly implied by the accepted WP02 purpose,
stop before editing that file and report the exact required path and
reason.

If the minimum implementation can be achieved through new
application-owned contract files plus focused tests without changing
existing production behavior, prefer that narrower approach.

## Twelve Data preservation

The existing Twelve Data integration is a protected compatibility
boundary.

WP02 must not:

-   remove it;
-   rewrite it into Vike;
-   change its provider behavior;
-   change its endpoint/configuration;
-   change its credential handling;
-   make it anonymous-public-chart fallback;
-   alter its runtime request behavior merely to fit the new
    abstraction.

WP02 may add provider-independent contracts that Twelve Data could use
in later governed work, but do not force a migration of Twelve Data
unless the current WP01/WP02 contract unambiguously requires it.

Existing Twelve Data tests must continue to pass.

## Vike boundary

WP03 owns the Vike historical OHLCV adapter.

WP02 must not implement:

-   Vike HTTP calls;
-   `/api/ohlcv`;
-   Vike authentication;
-   `X-API-KEY`;
-   Vike pagination;
-   Vike NDJSON;
-   Vike symbol translation;
-   Vike response parsing;
-   Vike rate-limit behavior;
-   empirical Vike provider calls.

A test double/fake provider used solely to validate the
provider-independent abstraction is allowed if it contains no Vike
transport behavior.

## Cache/UI boundary

WP04 owns cache-first historical-data/read-model implementation.

WP05/WP06 own public UI/presentation work.

WP02 must not implement:

-   cache persistence;
-   cache freshness policy execution;
-   SQLite changes;
-   read-model persistence changes;
-   Streamlit chart changes;
-   Plotly;
-   public provenance rendering;
-   navigation/UI controls.

The abstraction may define provenance/freshness metadata needed by later
WPs, but must not implement their presentation/persistence behavior.

## Package/schema/deployment boundary

No new package/dependency is authorized.

Do not modify package manifests or lock files unless compilation proves
an unavoidable requirement. If a new dependency is genuinely required,
stop at governance and report it rather than installing it.

No database/schema/migration changes.

No Azure mutation.

No Docker/GHCR mutation.

No deployment.

No tags or GitHub Releases.

Root `README.md` remains protected and unchanged.

## Test requirements

Add focused deterministic tests for the WP02 abstraction.

At minimum test the implemented invariants appropriate to the chosen
repository-native design:

-   accepted canonical symbols;
-   rejected unsupported/invalid symbols;
-   accepted intervals;
-   rejected unsupported/invalid intervals;
-   accepted ranges;
-   rejected unsupported/invalid ranges;
-   UTC timestamp requirement/normalization policy;
-   decimal OHLCV representation;
-   OHLC consistency validation;
-   malformed/incomplete candle rejection where representable;
-   ordering/duplicate validation at the correct abstraction boundary;
-   provider metadata separation;
-   typed failure representation;
-   cancellation semantics;
-   bounded deadline semantics;
-   absence of provider-specific transport concepts from the application
    contract.

Do not create brittle tests that assert incidental implementation
details.

Run the relevant existing application/infrastructure tests needed to
prove Twelve Data remains intact.

Run the broader repository test suite when feasible under current
repository practice.

## No-bypass requirements

The implementation must not create:

-   direct Streamlit/provider access;
-   direct browser/provider access;
-   a second uncontrolled market-data pipeline;
-   a microservice;
-   a broker;
-   a generalized plugin framework;
-   a Worker-supervision path from Streamlit;
-   a live trading/order path.

Keep the abstraction lightweight and repository-native.

## Documentation/control artifact

Retain this authority under:

`docs/roadmap/release-1.13/prompters/release-1.13-wp02-terra-provider-independent-historical-market-data-abstraction-implementation.md`

Every new Release 1.13 control/prompter Markdown filename must begin:

`release-1.13-`

If the File Manifest is updated, record only the exact WP02 paths
actually authorized/used. Do not convert it into wildcard authority.

Do not create `RELEASE_1.13_ACCEPTANCE.md`.

## Continue-until-governance-boundary rule

Do not stop because an implementation, build, test, lint, formatting, or
validation step initially fails.

Diagnose the failure, make the smallest correction permitted by the
literal WP02 authority, rerun the relevant checks, and continue
iterating until acceptance conditions pass.

Examples of technical failures to keep fixing within authority:

-   compilation errors in newly authorized WP02 types;
-   failing focused WP02 tests;
-   naming/namespace mistakes;
-   deterministic validation bugs;
-   formatting/whitespace defects;
-   test-discovery problems;
-   correctable project-reference assumptions that do not require
    dependency/package mutation;
-   secret-scan false positives that can be safely corrected;
-   branch synchronization issues that do not expand scope.

Report these, when relevant, as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

Stop only when the next corrective action would cross governance,
including:

-   modifying a non-allowlisted production path;
-   expanding the literal path set beyond the minimum authorized WP02
    implementation;
-   changing the Luna-owned architecture contract;
-   installing/changing a package;
-   changing schema/database;
-   implementing Vike runtime behavior;
-   implementing cache/UI work;
-   modifying Twelve Data behavior;
-   Azure/Docker/GHCR mutation;
-   modifying root README;
-   exposing secrets;
-   incurring prohibited recurring cost;
-   implementing Release 1.14/2.0;
-   bypassing repository protections;
-   merging without separate lifecycle authority.

Report a true boundary as:

`GOVERNANCE RESTRICTION — STOPPED`

State the exact restriction, failed evidence, and corrective action that
would require new authority.

## Git workflow

Use a dedicated WP02 branch.

Do not push directly to `main`.

Create a bounded PR when implementation and validation pass.

Do not merge the WP02 PR under this authority.

Do not close issue #289 merely because implementation is ready for
review.

Do not mark Project #2 WP02 Done.

Do not begin WP03.

## Validation

Before declaring the WP02 candidate ready:

-   verify predecessor `main`;
-   prove exact changed-path scope;
-   prove root `README.md` unchanged;
-   run `git diff --check`;
-   run repository-standard secret scanning;
-   build affected .NET projects;
-   run focused WP02 tests;
-   run relevant existing Twelve Data/application/infrastructure tests;
-   run broader test suite where repository convention requires/permits;
-   verify no package/dependency change;
-   verify no schema/database change;
-   verify no Vike runtime implementation/provider call;
-   verify no cache/UI implementation;
-   verify no Azure/Docker/GHCR mutation;
-   verify no Release 1.14/2.0 implementation;
-   verify all new prompter names use `release-1.13-`;
-   verify issue #289 remains Release `1.13`;
-   verify milestone #64 remains open.

## Acceptance boundary

A successful WP02 implementation authority produces a candidate PR only.

It does not self-certify substantive acceptance.

Following the Release 1.12/1.13 lifecycle pattern, a separate
model-appropriate acceptance authority must review the candidate before
merge.

WP03 remains unauthorized until WP02 substantive acceptance and
lifecycle completion.

## Required final report

Return:

-   inspected canonical `main` SHA;
-   issue #289 / Project state;
-   branch name;
-   exact files inspected;
-   exact literal mutation allowlist declared before editing;
-   exact files created;
-   exact files modified;
-   provider-independent types/contracts implemented;
-   validation/invariant design;
-   Twelve Data preservation evidence;
-   tests added;
-   build/test results;
-   broader regression results;
-   secret/whitespace results;
-   technical failures encountered and corrected;
-   governance restrictions encountered;
-   root README unchanged confirmation;
-   confirmation no Vike/cache/UI/package/schema/deployment work
    occurred;
-   commit SHA(s);
-   PR number/URL if created;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP02 PROVIDER ABSTRACTION IMPLEMENTATION: PASS`

or

`RELEASE 1.13 WP02 PROVIDER ABSTRACTION IMPLEMENTATION: BLOCKED`

A PASS means the WP02 implementation candidate is ready for separate
substantive acceptance. It does not authorize merge or WP03.

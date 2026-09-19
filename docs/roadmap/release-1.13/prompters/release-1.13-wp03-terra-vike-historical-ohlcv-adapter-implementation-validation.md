# Release 1.13 WP03 --- Vike Historical OHLCV Adapter Implementation and Validation Authority

## Selected model

**Execution model: GPT-5.6 Terra**

WP03 is assigned to GPT-5.6 Terra by the canonical Release 1.13
execution plan.

Model roles:

-   **GPT-5.6 Luna** --- owns the accepted Release 1.13/WP01
    architecture and any substantive contract change.
-   **GPT-5.6 Terra** --- owns this bounded provider-adapter
    implementation and empirical validation.
-   **GPT-5.6 Sol** --- supporting analysis only; no authority to
    redefine or accept architecture.

## Repository and predecessor

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Expected canonical predecessor:

`origin/main = b6a779ae0eebdd60d9b688029f19cf0afa6407f5`

Verify live repository truth before mutation.

## Work package identity

**Release 1.13 WP03 --- Vike Historical OHLCV Adapter, Canonical
Translation & Provider Validation**

Expected issue:

`#290`

Dependency:

WP02 is accepted, merged, and lifecycle-complete.

WP04 must not begin under this authority.

## Purpose

Implement and validate the server-side Vike historical-OHLCV adapter
behind the canonical WP02 `IHistoricalMarketDataProvider` contract.

WP03 owns:

-   Vike HTTP adapter mechanics;
-   server-side API-key handling;
-   canonical `BTC/USD` / `ETH/USD` translation;
-   canonical `1h` / `4h` / `1d` interval translation;
-   bounded range acquisition for `1D`, `7D`, `30D`, `90D`;
-   response parsing;
-   pagination when required;
-   conversion to canonical decimal OHLCV candles;
-   provider failure mapping;
-   timeout/cancellation behavior;
-   empirical validation of documented Vike behavior;
-   reconciliation of the first-party rate-limit discrepancy;
-   first-party attribution/access/licensing evidence needed by later
    public presentation;
-   deterministic tests and bounded provider integration validation.

WP03 does not own cache/read-model persistence or public UI.

## Mandatory evidence refresh before implementation

Before coding provider assumptions, review current first-party Vike
documentation, including at minimum the historical OHLCV and
access/licensing pages already referenced by WP01.

Verify:

-   endpoint shape;
-   BTC/ETH symbols;
-   required intervals;
-   timestamp semantics;
-   response format;
-   pagination/cursor semantics;
-   authentication header/key behavior;
-   documented request limits;
-   429 behavior;
-   attribution requirements;
-   licensing/access wording relevant to the public research/demo use.

Distinguish:

1.  explicit first-party documentation;
2.  empirical behavior observed under this authority;
3.  assumptions that remain unresolved.

Do not silently invent provider behavior.

If current first-party evidence materially invalidates the accepted
provider selection or requires a different public provider, stop at
governance.

## Rate-limit discrepancy

WP01 recorded a conflict between first-party statements describing 2,000
requests/hour per key for OHLCV and a broader 20,000 requests/hour
shared limit.

WP03 must re-check current first-party documentation and, where safe and
practical, inspect response/status/header behavior from bounded
authenticated calls.

Do not deliberately generate enough traffic to hit a rate limit.

Do not stress-test the provider.

Resolve the discrepancy only to the level justified by evidence. If the
enforced value cannot safely be established, document the conservative
operational assumption and evidence rather than fabricating certainty.

Runtime behavior must remain bounded regardless.

## Mandatory literal allowlist discovery before mutation

Before editing:

1.  inspect the merged WP02 contracts;
2.  inspect current infrastructure/provider conventions;
3.  inspect
    `src/AIQuantTradingResearch.Infrastructure/MarketData/TwelveData/`;
4.  inspect HTTP-client/configuration/DI patterns;
5.  inspect relevant infrastructure tests;
6.  inspect package/project files without mutating them;
7.  inspect configuration templates and secret-handling patterns;
8.  determine the minimum exact source/test/config-documentation path
    set required by WP03.

Then declare the exact literal mutation allowlist before first mutation.

Do not use directory wildcards.

Expected categories may include:

-   new Vike infrastructure adapter source files;
-   focused Vike adapter tests;
-   the minimum existing composition/registration source path if
    registration is required to compile/validate the adapter;
-   a safe non-secret configuration template/documentation path only if
    needed;
-   `RELEASE_1.13_FILE_MANIFEST.md`;
-   this WP03 authority prompter.

Inspection does not imply mutation authority.

If an existing production path must be changed and its necessity is not
clearly inherent to WP03, stop before editing and report the exact
path/reason.

## Canonical translation

Provider-specific translation remains adapter-owned.

Canonical symbols:

-   `BtcUsd` -\> Vike's verified BTC symbol representation;
-   `EthUsd` -\> Vike's verified ETH symbol representation.

Canonical intervals:

-   `OneHour`;
-   `FourHours`;
-   `OneDay`;

must map to the exact verified Vike interval representation.

Canonical ranges:

-   `OneDay`;
-   `SevenDays`;
-   `ThirtyDays`;
-   `NinetyDays`;

must produce bounded acquisition behavior appropriate to the requested
interval and range.

Do not leak Vike symbols, URLs, cursor values, or transport fields into
application contracts or UI-facing types.

## Parsing and canonical validation

Parse provider values into the existing canonical WP02 types.

Requirements include:

-   decimal OHLCV;
-   UTC open/left-boundary timestamps;
-   canonical request alignment;
-   strictly ascending successful output;
-   uniqueness;
-   malformed/incomplete response rejection;
-   OHLC consistency;
-   nonnegative volume;
-   truthful provenance metadata;
-   closed-candle semantics supported consistently with WP01/WP02.

Do not weaken WP02 invariants to accommodate provider payloads.

If Vike behavior exposes a genuine incompatibility with the accepted
WP02 contract, stop for Luna-level reconciliation rather than silently
changing the contract.

## Failure mapping

Map provider/runtime outcomes into the existing typed failures:

-   `InvalidRequest`;
-   `Unavailable`;
-   `RateLimited`;
-   `AuthenticationOrConfiguration`;
-   `MalformedResponse`;
-   `Timeout`;
-   `Cancelled`.

Do not expose raw provider errors, secrets, sensitive URLs, response
dumps, or internal exception details through the application contract.

## Authentication and secret handling

Vike credentials must remain server-side.

Use the repository's existing secure configuration pattern where
possible.

Never:

-   hard-code a real API key;
-   commit a key;
-   print a key;
-   include a key in test fixtures;
-   expose a key to Streamlit/browser output;
-   put a key in a URL;
-   persist credentials into generated artifacts.

If a real Vike key is unavailable in the execution environment,
deterministic adapter implementation/tests should still proceed. Report
live authenticated validation as unavailable only if it genuinely cannot
be performed without owner-provided secret access.

Do not weaken tests or substitute fabricated live evidence.

## Provider calls authorized by WP03

Unlike WP01/WP02, WP03 may perform bounded Vike market-data calls
required for empirical validation.

Calls must be:

-   minimal;
-   read-only;
-   limited to BTC/ETH historical OHLCV;
-   limited to the accepted interval/range matrix;
-   free-tier/zero-cost;
-   non-stressful;
-   non-looping beyond pagination required for a bounded test;
-   free of credential leakage.

Do not repeatedly call the provider to prove facts already established
by one or a few bounded requests.

Record what was empirically checked without recording secrets.

## Twelve Data preservation

Existing Twelve Data integration remains protected.

Do not:

-   modify its runtime behavior;
-   change credentials/configuration;
-   remove it;
-   replace it;
-   make it public historical fallback;
-   force it through the new abstraction unless separately required and
    authorized.

Run existing Twelve Data tests.

Vike must be an additional bounded provider adapter, not a destructive
provider migration.

## WP04/WP05/WP06 exclusions

Do not implement:

-   cache persistence;
-   freshness refresh policy execution;
-   SQLite changes;
-   historical read-model persistence;
-   stale-cache fallback;
-   Streamlit historical chart;
-   Plotly;
-   Market Research selectors;
-   public provenance rendering;
-   ML & Automation Studies UI;
-   System Health UI changes.

WP04 owns cache/read-model behavior. WP05/WP06 own presentation.

## Dependency boundary

Prefer the existing .NET HTTP/JSON stack and repository-native
capabilities.

No new package is authorized by default.

If implementation genuinely requires a new dependency, stop and report
the package, exact reason, license, proposed pinned version, and why
existing platform/repository capabilities are insufficient.

Do not install it without new authority.

## Zero-cost/F1 boundary

Preserve:

`ACTUAL RECURRING INFRASTRUCTURE COST: $0.00`

No paid Vike plan, paid feed, Azure service, managed database, broker,
or always-on new service is authorized.

Adapter behavior must remain appropriate for Azure App Service Linux F1
constraints.

## Test requirements

Add deterministic tests using mocked/fake HTTP behavior, not live
network dependency, for at least:

-   BTC symbol translation;
-   ETH symbol translation;
-   1h translation;
-   4h translation;
-   1d translation;
-   range/request bounding;
-   valid OHLCV parsing;
-   decimal precision;
-   UTC timestamp handling;
-   ascending output;
-   duplicate/out-of-order/malformed rejection;
-   authentication/configuration failure;
-   429 -\> `RateLimited`;
-   timeout -\> `Timeout`;
-   caller cancellation -\> `Cancelled` or the exact accepted contract
    semantics;
-   non-success/provider outage -\> appropriate typed failure;
-   malformed JSON/payload -\> `MalformedResponse`;
-   provenance provider identity and timestamps;
-   no secret in surfaced failures.

Where pagination is needed, test bounded cursor traversal and
termination deterministically.

Live provider validation, if credentials are available, supplements but
never replaces deterministic tests.

## Documentation/control artifacts

Retain this authority under:

`docs/roadmap/release-1.13/prompters/release-1.13-wp03-terra-vike-historical-ohlcv-adapter-implementation-validation.md`

Every new Release 1.13 control/prompter Markdown filename must begin:

`release-1.13-`

Update `RELEASE_1.13_FILE_MANIFEST.md` with only the exact WP03 mutation
paths actually frozen.

If provider evidence needs a durable repository record, use the minimum
existing architecture/provider documentation surface after inspecting
current conventions. Add its exact path to the declared allowlist before
mutation.

Do not create `RELEASE_1.13_ACCEPTANCE.md`.

## Continue-until-governance-boundary rule

Do not stop merely because implementation, HTTP parsing, tests, build,
lint, validation, or a bounded provider check fails.

Diagnose, correct within the declared literal authority, rerun, and
continue until WP03 acceptance conditions pass.

Examples of technical failures to keep fixing:

-   wrong endpoint construction;
-   wrong verified symbol/interval mapping;
-   JSON parsing mistakes;
-   timestamp conversion defects;
-   decimal parsing defects;
-   pagination termination bugs;
-   typed failure mapping defects;
-   cancellation/timeout bugs;
-   mock/test fixture defects;
-   compilation failures;
-   transient bounded provider/network errors where retry remains safe;
-   formatting/secret-scan issues correctable without scope expansion.

Report corrected failures as:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

Stop only when the next corrective action crosses governance, including:

-   changing the accepted WP01/WP02 contract;
-   modifying a non-allowlisted path;
-   adding a dependency;
-   changing Twelve Data behavior;
-   implementing cache/UI;
-   schema/database mutation;
-   changing public provider;
-   requiring a paid plan/service;
-   Azure/Docker/GHCR/deployment mutation;
-   root README mutation;
-   Release 1.14/2.0 implementation;
-   secret exposure;
-   bypassing repository protections;
-   merging without separate lifecycle authority.

Report:

`GOVERNANCE RESTRICTION — STOPPED`

State the exact restriction, evidence, and corrective action requiring
new authority.

Absence of a Vike credential is not permission to fabricate validation.
Complete all deterministic work possible within authority and report the
bounded live-validation limitation precisely.

## Git workflow

Use a dedicated WP03 branch.

Do not push directly to `main`.

Create a bounded PR after implementation and validation pass.

Do not merge under this authority.

Do not close issue #290 or mark it Done merely because the candidate is
ready.

Do not begin WP04.

## Validation

Before declaring WP03 ready for substantive acceptance:

-   verify canonical predecessor;
-   prove exact changed-path scope;
-   root README unchanged;
-   `git diff --check`;
-   repository-standard secret scan;
-   Release build;
-   focused Vike adapter tests;
-   application/infrastructure tests affected by the adapter;
-   existing Twelve Data tests;
-   domain/architecture tests;
-   broader solution tests where feasible;
-   no package change;
-   no schema/database change;
-   no cache/UI work;
-   no Azure/Docker/GHCR/deployment work;
-   no Release 1.14/2.0 work;
-   Vike first-party evidence refreshed;
-   rate-limit discrepancy addressed truthfully;
-   attribution/access evidence recorded;
-   provider calls, if made, were bounded/read-only/free;
-   no secrets in diff/logs/test artifacts;
-   issue #290 remains Release `1.13`;
-   milestone #64 remains open.

## Acceptance boundary

A successful WP03 implementation authority produces a candidate PR only.

It does not self-certify substantive acceptance.

A separate model-appropriate substantive acceptance authority must
review the adapter and evidence before merge.

WP04 remains unauthorized until WP03 acceptance and lifecycle
completion.

## Required final report

Return:

-   inspected `main` SHA;
-   issue #290 / Project state;
-   branch;
-   exact files inspected;
-   literal mutation allowlist declared before editing;
-   exact files created/modified;
-   Vike first-party documentation/evidence reviewed;
-   symbol/interval/range mappings implemented;
-   endpoint/auth/pagination behavior implemented;
-   parsing/canonical-validation behavior;
-   failure mapping;
-   rate-limit evidence/conclusion;
-   attribution/access/licensing evidence;
-   live provider validation performed or precise credential limitation;
-   deterministic tests added/results;
-   Twelve Data preservation results;
-   broader build/regression results;
-   provider calls made, summarized without secrets;
-   secret/whitespace validation;
-   technical failures corrected;
-   governance restrictions encountered;
-   README unchanged;
-   confirmation no cache/UI/package/schema/deployment work occurred;
-   commit SHA(s);
-   PR number/URL;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP03 VIKE ADAPTER IMPLEMENTATION / VALIDATION: PASS`

or

`RELEASE 1.13 WP03 VIKE ADAPTER IMPLEMENTATION / VALIDATION: BLOCKED`

A PASS means the WP03 candidate is ready for separate substantive
acceptance. It does not authorize merge or WP04.

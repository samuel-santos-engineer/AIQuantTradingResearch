# Release 1.13 WP04 --- Cache-First Historical Data / Read-Model Implementation Authority

## Selected model

**Execution model: GPT-5.6 Terra**

WP04 is assigned to GPT-5.6 Terra by the canonical Release 1.13
execution plan.

-   **GPT-5.6 Luna** owns the accepted WP01 architecture and substantive
    contract changes.
-   **GPT-5.6 Terra** owns this bounded implementation and empirical
    validation.
-   **GPT-5.6 Sol** is supporting analysis only.

## Repository and predecessor

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Expected canonical predecessor:

`origin/main = 26e497d74c654f99a3430266b00facaaee948acd`

Verify live repository truth before mutation.

## Work package

**Release 1.13 WP04 --- Cache-First Historical Data / Read-Model Path,
Freshness & Outage Behavior**

Expected issue: `#291`

Dependencies WP01-WP03 are accepted, merged, and lifecycle-complete.

WP05 must not begin under this authority.

## Purpose

Implement the smallest repository-native cache/read-model service
required to enforce the accepted chain:

`UI -> governed read model/service -> cache -> provider when required`

WP04 owns:

-   cache keys;
-   cached canonical candle snapshots;
-   acquisition/validation/freshness metadata;
-   deterministic freshness policy;
-   cache-hit-without-provider-call behavior;
-   refresh-through-governed-service behavior;
-   stale-but-usable fallback after provider refresh failure;
-   no-cache/provider-failure result state;
-   a presentation-safe historical read model/result contract;
-   bounded resource/retention behavior appropriate for Azure Linux F1;
-   deterministic tests proving no-bypass acquisition behavior.

WP04 does not implement the Streamlit chart or public presentation.

## Accepted architecture to preserve

The merged WP01 contract requires cache keys to include:

-   canonical symbol;
-   interval;
-   range;
-   provider identity;
-   adapter/contract version.

Metadata must represent:

-   acquisition time UTC;
-   last validation time UTC;
-   age/freshness;
-   stale state;
-   closed-candle status/provenance needed by later presentation.

A valid cache hit serves without a provider call.

Refresh occurs only through the governed service.

If refresh fails and stale-but-usable cache exists, return the cached
candles with truthful stale/freshness state.

If no usable cache exists and acquisition fails, return a controlled
unavailable result suitable for WP05/WP06 to render as:

`Historical market data is temporarily unavailable. Please try again later.`

Do not embed that UI text into infrastructure unless current repository
layering clearly requires it.

## Mandatory repository inspection and literal allowlist

Before first mutation inspect:

1.  merged WP02 historical market-data contracts;
2.  merged Vike adapter;
3.  current Application service/repository patterns;
4.  current Infrastructure persistence/read-model/file patterns;
5.  existing atomic read-model/file handoff used by Streamlit;
6.  SQLite usage and schema ownership;
7.  `/home` persistence assumptions from Release 1.12;
8.  Worker/runtime composition and DI patterns;
9.  relevant application/infrastructure tests;
10. `python/presentation/realtime_financial_visualization.py` only as a
    protected future consumer boundary;
11. package/project files only to understand ownership.

Then print/report the **minimum exact literal mutation allowlist before
editing**.

No directory wildcards.

Expected categories may include:

-   new application-owned historical read-model/cache-service contracts;
-   new infrastructure cache implementation;
-   minimum composition/registration path if genuinely required;
-   focused tests;
-   `RELEASE_1.13_FILE_MANIFEST.md`;
-   this WP04 prompter.

Do not mutate the Python presentation path under WP04.

If an existing production path is required but its mutation is not
clearly inherent to WP04, stop before editing and report the exact
path/reason.

## Persistence decision gate

Do not assume a new SQLite schema is required.

First determine whether WP04 can satisfy the accepted contract using the
repository's existing atomic read-model/file-handoff architecture or
another existing persistence primitive.

Prefer the smallest solution that:

-   survives ordinary Streamlit reruns;
-   prevents provider calls on every UI interaction;
-   is compatible with the existing deployment architecture;
-   remains bounded on F1;
-   requires no new package;
-   avoids schema migration.

A new database table/migration/schema change is **not authorized** by
this authority.

If correct implementation genuinely requires schema mutation, stop at
governance and report the proposed schema/path/reason.

## Cache key/version semantics

The cache key must distinguish at least:

`symbol + interval + range + provider identity + adapter/contract version`

Provider identity/version must not leak into the public canonical candle
type.

Choose a stable repository-native representation.

A change in adapter/contract version must not silently reuse
incompatible cached content.

## Freshness policy

Define an explicit deterministic freshness policy before implementing
it.

The policy must be:

-   interval-aware where appropriate;
-   bounded;
-   testable with an injected/fake clock;
-   conservative for historical visualization;
-   independent of hover/zoom/pan/UI reruns;
-   appropriate for F1's 60 CPU-minute/day and `$0.00` constraints.

Do not invent a high-frequency polling system.

Do not add background polling or an always-on service merely to keep
candles fresh.

The policy should favor cached data and acquire only when the governed
service determines refresh is required.

Record the chosen thresholds/rationale in code/tests or the minimum
existing Release 1.13 governance surface, without expanding into WP05
presentation policy.

## Read behavior

The governed service should support these semantic outcomes:

### Fresh cache

Return cached canonical candles and metadata.

Provider call count: zero.

### Missing cache

Perform at most the bounded acquisition required for the request.

On provider success, validate/store the snapshot and return it.

On provider failure, return controlled unavailable/no-data state.

### Stale usable cache

Attempt a bounded refresh only when policy requires it.

If refresh succeeds, replace/update cache atomically and return fresh
data.

If refresh fails, return stale cached data with truthful stale metadata
and the last valid update/validation information.

Do not discard usable stale data merely because refresh failed.

### Corrupt/incompatible cache

Do not surface malformed cached candles.

Treat incompatible/corrupt cache as unusable and follow the governed
missing-cache acquisition path.

Never pass raw cache exceptions or file/database paths to later
presentation.

## Provider selection boundary

WP04 consumes `IHistoricalMarketDataProvider`.

For the public historical path, Vike is the accepted provider.

Do not modify Vike adapter behavior in WP04.

Do not make Twelve Data a fallback.

Do not add multi-provider racing, provider selection UI, or automatic
provider failover.

If DI/composition is needed, use the minimum server-side registration
consistent with current architecture.

## Atomicity and concurrency

Inspect existing repository atomic-write/read-model conventions and
reuse them where practical.

Cache writes must not leave a partially readable snapshot.

Concurrent identical requests must not create an unbounded provider-call
storm.

Implement the smallest repository-native coordination needed for the
expected single-container/F1 environment.

Do not add Redis, queues, distributed locks, brokers, or managed
services.

Do not over-engineer for multi-region/high-availability scenarios
outside Release 1.13.

## Retention and storage

Keep retention bounded for:

-   2 symbols;
-   3 intervals;
-   4 ranges;
-   the accepted public provider/version;
-   1 GB F1 storage.

Avoid uncontrolled append-only growth.

Prefer replacement/compaction of canonical snapshots rather than
accumulating provider payload history.

Do not persist raw Vike HTTP payloads unless an accepted existing
repository pattern makes that strictly necessary; canonical validated
candles are the governed cache content.

No credential may enter cached data.

## Presentation-safe result

WP04 may define the minimum read-model/result state required by later UI
work, such as:

-   data available;
-   empty successful dataset;
-   stale-but-usable;
-   unavailable/no usable cache;
-   canonical candles;
-   provider/provenance identity;
-   last updated/validated UTC;
-   freshness/stale indication.

Do not add UI labels, Plotly types, Streamlit objects, HTML, CSS,
navigation, chart options, or technical indicators.

## Twelve Data preservation

Twelve Data remains unchanged.

Run relevant Twelve Data tests.

Do not:

-   modify its runtime behavior;
-   migrate it;
-   use it as public fallback;
-   alter its credentials/configuration;
-   route the public cache path to it.

## WP05/WP06 boundary

Do not implement:

-   `python/presentation/realtime_financial_visualization.py` changes;
-   Streamlit Market Research navigation;
-   BTC/ETH controls;
-   interval/range controls;
-   Plotly;
-   candlesticks;
-   volume;
-   hover;
-   zoom/pan;
-   public provenance text;
-   ML & Automation Studies presentation;
-   System Health presentation changes.

WP05/WP06 own those concerns.

## Dependency/schema/deployment boundary

No new package/dependency is authorized.

No database/schema migration is authorized.

No Azure mutation.

No Docker/GHCR mutation.

No deployment.

No tags or GitHub Releases.

Root `README.md` remains protected.

Preserve recurring infrastructure cost:

`$0.00`

## Test requirements

Add deterministic tests proving at least:

-   cache key includes symbol;
-   interval;
-   range;
-   provider identity;
-   contract/adapter version;
-   fresh cache causes zero provider calls;
-   missing cache causes bounded provider acquisition;
-   successful acquisition populates cache;
-   stale cache triggers only governed refresh;
-   failed refresh with usable stale cache returns stale data;
-   failed acquisition with no usable cache returns controlled
    unavailable state;
-   empty successful provider result remains distinguishable from
    unavailable;
-   corrupt/incompatible cache is not surfaced;
-   malformed candles cannot be persisted/surfaced;
-   metadata timestamps remain UTC;
-   stale/fresh state is truthful;
-   provider exceptions/failures do not leak raw details;
-   repeated read-model calls do not cause acquisition when cache
    remains fresh;
-   concurrent identical requests do not create an unbounded
    provider-call storm;
-   bounded retention/no uncontrolled append behavior where applicable;
-   no Twelve Data fallback;
-   no presentation-layer dependency.

Use a deterministic/fake clock.

Do not require live Vike network calls for WP04 tests.

## No-bypass requirements

Prove the intended path remains:

`future Streamlit -> governed read model/service -> cache -> Vike provider`

There must be no direct Streamlit/provider path, direct browser/provider
path, alternate uncontrolled market-data pipeline, or provider call
triggered by chart interaction.

Do not create a microservice, broker, generalized plugin framework, or
trading path.

## Continue-until-governance-boundary rule

Do not stop merely because implementation, persistence, concurrency,
build, test, lint, or validation initially fails.

Diagnose, make the smallest correction inside the declared literal WP04
authority, rerun checks, and continue until acceptance conditions pass.

Examples:

-   serialization defects;
-   atomic-write defects;
-   cache-key mistakes;
-   stale/fresh calculation defects;
-   fake-clock mistakes;
-   concurrency-test defects;
-   provider-call-count defects;
-   compilation errors;
-   corrupt-cache handling defects;
-   test fixture errors;
-   transient file-lock/test-runner problems.

Report:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

Stop only if the next correction requires:

-   changing WP01/WP02 contracts;
-   changing Vike adapter behavior;
-   changing Twelve Data;
-   adding a package;
-   schema/database migration;
-   modifying a non-allowlisted path;
-   implementing Streamlit/Plotly/UI;
-   Azure/Docker/GHCR/deployment;
-   root README;
-   Release 1.14/2.0;
-   paid service;
-   secrets;
-   merge without lifecycle authority.

Report:

`GOVERNANCE RESTRICTION — STOPPED`

State the exact restriction and required new authority.

## Git workflow

Use a dedicated WP04 branch.

Do not push directly to `main`.

Create a bounded PR after implementation/validation pass.

Do not merge under this authority.

Do not close issue #291 or mark WP04 Done merely because the candidate
is ready.

Do not begin WP05.

## Validation

Before declaring WP04 ready:

-   verify canonical predecessor;
-   prove exact changed paths;
-   README unchanged;
-   `git diff --check`;
-   isolated secret scan;
-   Release build;
-   focused WP04 tests;
-   Application tests;
-   Infrastructure tests;
-   Vike tests;
-   Twelve Data tests;
-   Domain/Architecture tests;
-   broader solution tests where feasible;
-   no package change;
-   no schema migration;
-   no Python/Streamlit/Plotly/UI change;
-   no Vike/Twelve Data behavior change;
-   no Azure/Docker/GHCR/deployment;
-   no Release 1.14/2.0 work;
-   bounded cache storage behavior;
-   exact cache-first provider-call-count evidence;
-   issue #291 remains Release `1.13`;
-   milestone #64 remains open.

## Acceptance boundary

A successful WP04 implementation produces a candidate PR only.

It does not self-certify substantive acceptance.

A separate substantive acceptance authority must review cache semantics,
freshness, persistence, concurrency, outage behavior, and no-bypass
evidence before merge.

WP05 remains unauthorized until WP04 acceptance and lifecycle
completion.

## Required final report

Return:

-   inspected `main` SHA;
-   issue #291 / Project state;
-   branch;
-   exact files inspected;
-   exact literal allowlist declared before mutation;
-   persistence mechanism selected and why;
-   freshness policy selected and why;
-   cache-key/version design;
-   atomicity/concurrency design;
-   exact files created/modified;
-   fresh/missing/stale/corrupt-cache behavior;
-   provider call-count evidence;
-   stale-on-refresh-failure evidence;
-   no-cache/provider-failure evidence;
-   retention/storage evidence;
-   tests/results;
-   Vike preservation;
-   Twelve Data preservation;
-   build/regression results;
-   secret/whitespace results;
-   technical failures corrected;
-   governance restrictions encountered;
-   README unchanged;
-   confirmation no UI/package/schema/deployment work occurred;
-   commit SHA(s);
-   PR number/URL;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP04 CACHE-FIRST READ-MODEL IMPLEMENTATION: PASS`

or

`RELEASE 1.13 WP04 CACHE-FIRST READ-MODEL IMPLEMENTATION: BLOCKED`

A PASS means the WP04 candidate is ready for separate substantive
acceptance. It does not authorize merge or WP05.

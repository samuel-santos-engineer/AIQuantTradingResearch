# Release 1.13 WP04 --- Luna Final Substantive Acceptance of PR #300

## Selected model

**Execution model: GPT-5.6 Luna**

-   **GPT-5.6 Luna** owns substantive acceptance of WP04.
-   **GPT-5.6 Terra** produced the implementation candidate.
-   **GPT-5.6 Sol** may support analysis only.

## Candidate

Repository: `samuel-santos-engineer/AIQuantTradingResearch`

PR: `#300 — Release 1.13 WP04: cache-first historical read model`

Expected base:

`26e497d74c654f99a3430266b00facaaee948acd`

Expected head:

`e968a9623f9031f314aa6a53370f715e2ab8ff37`

Expected branch:

`feature/release-1.13-wp04-cache-read-model`

Verify live repository truth before review.

## Expected exact mutation set

1.  `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataCacheContracts.cs`
2.  `src/AIQuantTradingResearch.Application/MarketData/HistoricalMarketDataReadService.cs`
3.  `src/AIQuantTradingResearch.Infrastructure/MarketData/Cache/AtomicFileHistoricalMarketDataCache.cs`
4.  `tests/AIQuantTradingResearch.Application.Tests/HistoricalMarketDataReadServiceTests.cs`
5.  `tests/AIQuantTradingResearch.Infrastructure.Tests/AtomicFileHistoricalMarketDataCacheTests.cs`
6.  `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
7.  `docs/roadmap/release-1.13/prompters/release-1.13-wp04-terra-cache-first-historical-data-read-model-implementation.md`

Root `README.md` remains forbidden.

## Purpose

Determine whether PR #300 faithfully implements the accepted cache-first
architecture and is safe to advance to separate merge/lifecycle
authority.

Do not merge PR #300.

Do not begin WP05.

## Core architecture gate

Verify the resulting path remains:

`future Streamlit -> governed HistoricalMarketDataReadService -> cache -> IHistoricalMarketDataProvider -> Vike`

Verify there is no direct UI/provider path, Twelve Data public fallback,
provider racing, polling service, microservice, broker, or alternate
market-data pipeline.

## Cache-key gate

Verify the key actually distinguishes:

-   symbol;
-   interval;
-   range;
-   provider identity;
-   cache-contract/adapter compatibility version.

Review fingerprint construction for deterministic, collision-resistant,
filesystem-safe behavior.

Verify incompatible versions cannot silently reuse an older snapshot.

## Canonical cache-entry validation

Verify every stored/retrieved entry preserves WP02 canonical invariants:

-   request/key agreement;
-   provider identity agreement;
-   UTC timestamps;
-   valid acquisition/validation ordering;
-   valid canonical candles;
-   strict ascending/unique candles;
-   OHLC consistency;
-   nonnegative volume;
-   closed-candle/provenance semantics.

A corrupt or incompatible persisted entry must never be surfaced as
valid data.

## Freshness semantics

Review the selected windows:

-   1h -\> 15 minutes;
-   4h -\> 30 minutes;
-   1d -\> 2 hours.

Acceptance does not require these exact numbers merely because Terra
selected them. Determine whether they are internally coherent,
deterministic, bounded, and appropriate for a historical visualization
under F1 constraints.

### Critical time-arithmetic gate

Inspect `IsFresh` carefully.

The candidate computes freshness using current UTC time minus
`LastValidatedAtUtc`.

Verify behavior when:

-   validation time equals now;
-   age is exactly the threshold;
-   validation time is slightly in the future because of clock
    skew/corrupt cache;
-   age is well beyond threshold.

A future validation timestamp must not accidentally create an
indefinitely or incorrectly fresh cache entry.

If correction is needed and remains within the seven authorized paths,
correct it and add deterministic boundary tests.

## Stale fallback truthfulness

On failed refresh with usable stale data, verify:

-   cached candles are returned;
-   result state is stale;
-   provider failure may be retained safely for internal result
    semantics;
-   last-updated/validated timestamps remain those of the cached
    snapshot;
-   result does not mutate old provenance into a false new validation;
-   raw provider/cache exception details are not surfaced.

## Missing/corrupt cache behavior

Verify:

-   missing cache -\> one governed acquisition;
-   corrupt/incompatible cache -\> treated as unusable;
-   acquisition success -\> validated atomic snapshot;
-   acquisition failure -\> controlled unavailable state;
-   successful empty dataset remains distinct from unavailable.

### Critical cache-I/O failure gate

Inspect how `RetrieveAsync` and `StoreAsync` behave for ordinary
filesystem failures beyond malformed JSON, such as:

-   directory/file permission errors;
-   transient IO errors;
-   file disappearing between existence check and open;
-   concurrent replacement/read races;
-   retention deletion failure.

Determine whether these can escape the governed read service and violate
the controlled-unavailable/stale-fallback contract.

Do not require blanket swallowing of serious process failures. Ordinary
cache I/O failure should have a deliberate, safe semantic treatment.

Correct within the frozen seven paths if necessary.

## Concurrency gate

The candidate uses a per-key `SemaphoreSlim` dictionary.

Verify:

-   concurrent identical cache misses coalesce to one provider
    acquisition;
-   the cache is rechecked after lock acquisition;
-   failed acquisition releases the gate;
-   cancellation while waiting behaves correctly;
-   different keys do not unnecessarily serialize;
-   gate storage cannot grow without practical bound during normal use.

The supported matrix is small, but acceptance must explicitly reason
about the lifetime of `gates`.

If cleanup is needed, implement it safely and test it.

## Cancellation semantics

This is a critical gate.

Inspect cancellation at each stage:

-   cache retrieval;
-   waiting for the per-key gate;
-   provider acquisition;
-   cache store;
-   retention cleanup.

The read service currently maps provider-thrown cancellation inside
`AcquireAsync`, but cancellation thrown by cache operations or
`gate.WaitAsync` may follow different semantics.

Determine the intended application contract and ensure caller
cancellation is handled consistently rather than leaking unexpectedly or
being mislabeled as provider unavailability.

Correct/test within WP04 authority if required.

## Atomic-file gate

Review the write sequence:

temporary file -\> serialized canonical snapshot -\> flush -\> atomic
replacement.

Verify:

-   temporary files are unique;
-   target filename derives only from safe fingerprint;
-   interrupted writes do not replace valid snapshots with partial JSON;
-   cleanup does not erase the valid target;
-   reads during replacement remain safe for the target OS/filesystem
    assumptions;
-   no credentials/raw Vike payloads are persisted.

Do not claim cross-filesystem/distributed guarantees that the
implementation does not provide.

## Retention gate

The candidate defaults to 32 JSON snapshots.

Verify this is bounded and sufficient for the frozen 24 symbol ×
interval × range combinations plus reasonable version turnover.

Review retention ordering based on filesystem `LastWriteTimeUtc`.

Determine whether replacement and pruning can accidentally evict an
actively required current snapshot under the expected single-container
workload.

No unbounded append history is allowed.

## Exception containment

The provider wrapper currently catches broad `Exception` and maps it to
`Unavailable`.

Review whether this is too broad for application semantics.

Do not swallow process-critical exceptions merely to make tests green.

Use repository/.NET conventions to distinguish expected provider
operational failures from exceptions that should propagate.

Any correction must preserve controlled provider failure behavior and
stay within WP04's seven paths.

## Test adequacy

Do not accept by test count alone.

Existing tests must be reviewed against the authority requirements. Add
deterministic tests where coverage is materially absent.

Pay particular attention to:

-   all cache-key dimensions, including provider identity independently;
-   freshness threshold boundary;
-   future timestamp behavior;
-   corrupt/incompatible cache followed by acquisition;
-   stale refresh success;
-   stale refresh failure;
-   missing-provider failure;
-   empty success;
-   cache read I/O failure;
-   cache write I/O failure;
-   cancellation while waiting/acquiring/storing;
-   concurrent identical reads;
-   different-key concurrency where useful;
-   retention;
-   canonical round trip;
-   provider identity mismatch;
-   no raw exception/secret leakage.

A requirement need not have one test per bullet if behavior is
convincingly covered, but acceptance must identify evidence.

## F1/resource gate

Verify the implementation is appropriate for:

-   Azure App Service Linux F1;
-   persistent `/home`;
-   1 GB storage;
-   60 CPU minutes/day;
-   `$0.00` recurring infrastructure.

No background polling or always-on acquisition may have been introduced.

## Protected boundaries

Verify unchanged:

-   Vike adapter behavior;
-   Twelve Data behavior;
-   WP02 contracts unless already part of the authorized WP04
    application additions;
-   Worker composition;
-   Python/Streamlit/UI;
-   Plotly/packages;
-   SQLite/schema;
-   Azure;
-   Docker/GHCR;
-   deployment;
-   root README;
-   Release 1.14/2.0.

## Validation

Run/re-run:

-   Release build;
-   focused WP04 Application tests;
-   focused atomic-cache tests;
-   full Application tests;
-   full Infrastructure tests;
-   Domain tests;
-   Architecture tests;
-   focused Vike/Twelve Data tests;
-   broader solution tests where feasible;
-   `git diff --check`;
-   isolated secret scan;
-   exact seven-path proof;
-   README unchanged proof.

The previously observed transient Release 1.10 observability-test
failure is not itself a blocker if it remains demonstrably unrelated and
passes on repeat/full rerun.

## Continue-until-governance-boundary rule

Do not stop for an ordinary WP04 defect.

Correct defects inside the frozen seven-path set, rerun validation, and
continue reviewing the new PR head.

Examples include:

-   freshness boundary/time-skew defect;
-   cancellation handling defect;
-   cache I/O containment defect;
-   semaphore/gate lifecycle defect;
-   atomic-file defect;
-   retention defect;
-   missing deterministic test;
-   exception-mapping defect.

Report:

`TECHNICAL FAILURE — CORRECTED WITHIN AUTHORITY`

If a correction changes the PR head, record the new final head SHA as
the Luna-accepted head.

Stop only if correction requires:

-   changing accepted WP01/WP02 architecture/contracts;
-   modifying Vike/Twelve Data behavior;
-   an eighth mutation path;
-   new dependency/package;
-   schema/database migration;
-   Streamlit/Plotly/UI;
-   Azure/Docker/GHCR/deployment;
-   root README;
-   Release 1.14/2.0;
-   paid service;
-   secrets;
-   merge/lifecycle action.

Report:

`GOVERNANCE RESTRICTION — STOPPED`

## Lifecycle boundary

On PASS:

-   leave PR #300 open/unmerged;
-   leave issue #291 open/non-Done;
-   keep milestone #64 open;
-   do not begin WP05.

The only next authorized action is a separate Terra merge/post-merge
lifecycle authority bound to the final Luna-accepted head.

## Required final report

Report:

-   inspected base/head;
-   exact seven changed paths;
-   cache-key/fingerprint finding;
-   canonical-entry validation finding;
-   freshness/time-boundary finding;
-   stale-fallback finding;
-   missing/corrupt behavior;
-   cache-I/O failure finding;
-   concurrency/gate-lifetime finding;
-   cancellation finding;
-   atomic-write finding;
-   retention finding;
-   exception-containment finding;
-   test-adequacy finding;
-   F1/resource finding;
-   corrections made, if any;
-   final accepted head SHA;
-   complete validation results;
-   Vike/Twelve Data preservation;
-   protected-boundary results;
-   README unchanged;
-   issue #291 / Project state;
-   milestone #64 state;
-   governance restrictions encountered;
-   next separately authorized action.

End with exactly one of:

`RELEASE 1.13 WP04 LUNA FINAL SUBSTANTIVE ACCEPTANCE: PASS`

or

`RELEASE 1.13 WP04 LUNA FINAL SUBSTANTIVE ACCEPTANCE: BLOCKED`

A PASS does not merge PR #300 and does not authorize WP05.

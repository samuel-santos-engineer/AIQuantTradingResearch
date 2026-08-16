# Release 1.1 WP05 --- Persistence Use-Case Integration --- Codex Execution Authority

## 1. Authority and purpose

This document is the authoritative execution contract for Release 1.1:

**WP05 --- Persistence Use-Case Integration**

GitHub planning identity:

-   Release: `1.1`
-   Milestone:
    `Phase 3 - Release 1.1: Market Data Persistence Foundation`
-   Work package: `WP05`
-   GitHub issue: `#107`
-   Predecessor: `WP04 — Application Persistence Contracts` / issue
    `#106`
-   Successor: `WP06 — Storage Physical Model` / issue `#108`

Execute WP05 only.

The purpose of WP05 is to integrate the accepted WP04 persistence
boundary into the Application use-case layer so normalized market-data
observations can be persisted through a provider-independent and
storage-independent Application workflow.

WP05 is an Application orchestration work package. It does **not**
authorize a physical storage model, SQLite, SQL, connection management,
Infrastructure persistence implementation, dependency registration,
Worker persistence execution, or broad test implementation.

------------------------------------------------------------------------

## 2. Authority precedence

Use this precedence order when evidence conflicts:

1.  Explicit current human instruction.
2.  This authoritative WP05 prompt.
3.  Accepted Release 1.1 execution plan.
4.  Accepted Release 1.1 file manifest.
5.  Accepted WP04 result and repository truth produced by WP04.
6.  Accepted WP03 persistence semantics.
7.  Accepted WP02 technology decision and discovery evidence.
8.  Accepted WP01 preflight result.
9.  Current repository architecture and engineering governance.
10. Current GitHub planning truth.

Do not silently reconcile a material contradiction.

If a mandatory requirement cannot be satisfied without exceeding WP05
authority, stop and report the blocker with the minimum additional
authority required.

------------------------------------------------------------------------

## 3. Mandatory authorities to review before mutation

Read completely before changing repository content:

-   `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
-   the authoritative WP01 prompt/result and relevant preflight evidence
-   the authoritative WP02 prompt/result and persistence technology
    decision artifacts
-   the authoritative WP03 prompt/result and accepted
    historical-observation persistence semantics
-   `04-application-persistence-contracts-codex-prompt.md`
-   the accepted WP04 execution result/evidence available in
    repository/GitHub context
-   current Domain and Application source
-   current Application tests and Architecture tests
-   architecture/design documentation relevant to Application boundaries
    and persistence
-   GitHub issue `#107`
-   its predecessor issue `#106`
-   milestone and Project state for Release 1.1

Do not infer accepted WP04 APIs from this prompt if repository truth
differs. Inspect the actual WP04 files.

------------------------------------------------------------------------

## 4. Accepted WP04 baseline

WP04 established the Application persistence contract with these
accepted semantics:

-   one capability-specific `IHistoricalObservationStore`
-   no generic repository abstraction
-   write operation conceptually equivalent to:
    `Persist(string target, IReadOnlyList<PriceObservation> observations)`
-   retrieval operation conceptually equivalent to:
    `Retrieve(string target)`
-   write outcomes:
    -   `NewlyAccepted`
    -   `Idempotent`
    -   `Conflict`
-   failures distinguish at least:
    -   `Unavailable`
    -   `InvalidData`
-   successful retrieval may contain an empty read-only collection
-   successful emptiness is not failure
-   target identity remains the exact opaque target string
-   observation identity uses the Domain observation semantic instant
-   `DateTimeOffset` and `decimal` fidelity are preserved
-   retrieved observations are unique and strictly increasing by
    semantic instant
-   Domain delta is `0`
-   the boundary is synchronous under current Application conventions
-   SQLite/package delta is `0`

Repository truth is authoritative for exact type names, members,
namespaces, signatures, and validation rules.

------------------------------------------------------------------------

## 5. WP05 objective

Implement the minimum Application use-case integration required to make
persistence a first-class Application workflow while preserving clean
separation between:

1.  market-data acquisition,
2.  Domain-normalized observations,
3.  Application persistence orchestration,
4.  storage implementation.

The resulting Application design must allow a caller to request
persistence of already normalized observations through the WP04 store
contract and receive a deterministic Application-level result.

Where the existing Application acquisition use case provides the correct
composition point, integrate persistence without coupling Application to
Twelve Data, HTTP, SQLite, SQL, files, connection strings, or
Infrastructure implementation details.

Prefer the smallest coherent design supported by current repository
conventions.

------------------------------------------------------------------------

## 6. Mandatory design principles

WP05 must satisfy all of the following.

### 6.1 Application owns orchestration

The use case belongs in `AIQuantTradingResearch.Application`.

Application may depend on:

-   Domain values
-   existing Application contracts
-   the WP04 persistence contracts

Application must not depend on Infrastructure or Worker.

### 6.2 Acquisition and persistence remain separate capabilities

Do not collapse provider acquisition and storage into one
infrastructure-shaped abstraction.

The acquisition source remains responsible for obtaining observations.

The persistence store remains responsible for historical
persistence/retrieval.

Application orchestration may coordinate those capabilities where
explicitly justified, but the contracts must remain independently
replaceable.

### 6.3 Persist normalized Domain observations

Persistence orchestration must consume the accepted Domain observation
representation.

Do not introduce:

-   provider DTOs
-   HTTP response types
-   SQLite rows
-   SQL parameters
-   serialization models
-   transport-specific values

into Application.

### 6.4 Preserve target identity

Do not normalize, rewrite, lowercase, trim, alias, canonicalize, or
otherwise reinterpret the opaque target unless an already accepted
repository invariant explicitly requires it.

### 6.5 Preserve persistence semantics

Application orchestration must not erase the accepted distinction among:

-   newly accepted persistence
-   idempotent duplicate persistence
-   deterministic semantic conflict
-   storage unavailability
-   invalid persistence data

Do not turn all outcomes into a generic boolean.

### 6.6 Successful empty retrieval remains valid

If WP05 exposes or composes retrieval, empty successful history must
remain distinguishable from failure.

Do not synthesize an invalid Domain `ObservationSeries` merely to
represent empty persistence history.

### 6.7 No physical-storage knowledge

Application must not know:

-   SQLite
-   database files
-   tables
-   columns
-   indexes
-   SQL
-   connection strings
-   transactions
-   migrations
-   provider-specific persistence mechanics

Those concerns begin in later work packages.

------------------------------------------------------------------------

## 7. Required repository reconnaissance

Before design, inspect current Application workflows and determine:

-   current market-data acquisition use-case types
-   current request/result conventions
-   validation conventions
-   error/failure representation
-   constructor dependency style
-   synchronous execution style
-   whether existing use cases are narrow command/query-like services or
    another pattern
-   existing naming conventions
-   whether an acquisition workflow already produces the exact
    normalized observations needed for persistence
-   whether WP05 should add a dedicated persistence use case, extend an
    existing use case, or introduce a minimal coordinating use case

Record the decision and evidence in the execution report.

Do not introduce a new architectural pattern merely because it is common
elsewhere.

------------------------------------------------------------------------

## 8. Required WP05 capability

At minimum, repository truth after WP05 must provide an
Application-level operation that:

1.  accepts a target and normalized observations, or consumes an
    existing Application result that contains them;
2.  invokes the accepted `IHistoricalObservationStore` boundary;
3.  preserves the store's semantic outcomes;
4.  does not call any provider or storage implementation directly;
5.  performs no physical-storage transformation;
6.  has deterministic behavior for all accepted persistence outcomes.

If the Release 1.1 manifest names exact WP05 files, those paths are
mandatory unless a genuine repository contradiction makes them
impossible.

Do not add speculative APIs beyond the manifest and minimum acceptance
need.

------------------------------------------------------------------------

## 9. Acquisition/persistence integration decision

Explicitly inspect whether WP05 is intended to:

### Model A --- dedicated persistence use case

A use case receives an already normalized target/observation set and
persists it.

or:

### Model B --- Application coordinator

A use case coordinates the existing observation source and the
persistence store in sequence.

Select the model supported by the execution plan, file manifest,
existing Application design, and predecessor evidence.

Do **not** automatically choose Model B merely because WP12 later
performs persistent Worker execution.

WP12 owns Worker persistent market-data execution. WP05 must not
prematurely implement Worker orchestration.

If a dedicated persistence use case is sufficient to satisfy WP05,
prefer it over premature end-to-end host behavior.

Document the selected model and rejected alternative.

------------------------------------------------------------------------

## 10. Input validation

Follow existing Application validation conventions.

At minimum, determine how the use case handles:

-   null target where representable
-   empty target
-   null observation collection where representable
-   empty observation collection
-   observations that violate ordering or uniqueness expectations
-   Domain-invalid observations, if construction already prevents them

Do not duplicate Domain validation unnecessarily.

Do not invent physical-storage validation.

If WP04 result types already enforce relevant invariants, reuse them.

------------------------------------------------------------------------

## 11. Empty write semantics

Inspect the accepted authorities and repository conventions to determine
whether persisting an empty observation collection is:

-   a valid idempotent/no-op Application request,
-   invalid Application input,
-   or otherwise explicitly defined.

Do not guess.

If predecessor authorities do not settle the question and the
implementation cannot proceed deterministically without deciding it,
choose the narrowest behavior consistent with existing WP04 contract
validation and report the evidence. If that would alter an accepted
semantic contract, stop instead.

------------------------------------------------------------------------

## 12. Result design

The WP05 use-case result must remain capability-focused.

Prefer reuse/composition of accepted WP04 result types rather than
duplicating persistence vocabulary.

Do not introduce:

-   generic `Result<T>` frameworks
-   exception hierarchies unrelated to repository conventions
-   broad persistence status taxonomies
-   database-specific failure values
-   HTTP-specific failures

If a new Application use-case result is necessary, keep it minimal and
explain why WP04 types alone are insufficient.

------------------------------------------------------------------------

## 13. Conflict handling

A semantic persistence conflict is an expected deterministic
business/application outcome, not storage unavailability.

WP05 must preserve that distinction.

Do not:

-   retry conflict automatically
-   convert conflict to idempotent success
-   overwrite accepted history
-   hide conflict behind a generic failure
-   invoke provider reacquisition to "fix" conflict

Later Infrastructure work may implement mechanics, but the Application
semantic distinction is already authoritative.

------------------------------------------------------------------------

## 14. Failure handling

Preserve the accepted persistence failure boundary.

WP05 must not:

-   inspect SQLite error codes
-   inspect SQL exception types
-   expose Infrastructure exceptions
-   infer provider failure from persistence failure
-   retry storage internally unless an accepted Application convention
    explicitly requires it

The storage implementation is responsible for mapping mechanics to the
WP04 failure vocabulary in later work.

------------------------------------------------------------------------

## 15. Retrieval integration

Only add retrieval orchestration in WP05 if required by the Release 1.1
execution plan/file manifest or necessary for the explicitly defined
WP05 use case.

Do not expand scope merely because `IHistoricalObservationStore` exposes
retrieval.

WP09 owns historical observation retrieval implementation.

If an Application retrieval use case is part of WP05 authority, it must:

-   depend only on the store abstraction
-   preserve target scoping
-   preserve ascending unique semantic instants
-   preserve exact `DateTimeOffset` and `decimal` values
-   preserve successful empty results
-   avoid physical-storage knowledge

------------------------------------------------------------------------

## 16. Domain protection

Expected Domain production delta:

`0`

Do not modify Domain unless an unavoidable contradiction is proven.

Do not add persistence concepts to Domain solely to facilitate storage.

The accepted Domain model already owns observation invariants.

Any proposed Domain change is a blocker requiring explicit authority
unless the Release 1.1 manifest expressly authorizes it for WP05.

------------------------------------------------------------------------

## 17. Infrastructure protection

Expected Infrastructure production delta:

`0`

WP05 must not implement:

-   SQLite
-   storage engine selection
-   connection factories
-   schema creation
-   repositories
-   SQL statements
-   migrations
-   persistence adapters
-   failure mapping from concrete storage exceptions

Those responsibilities belong to WP06--WP10.

------------------------------------------------------------------------

## 18. Worker protection

Expected Worker production delta:

`0`

Do not:

-   register the store
-   configure a database path
-   change hosted-service execution
-   persist observations from Worker
-   alter startup behavior

WP11 owns dependency registration/configuration.

WP12 owns Worker persistent market-data execution.

------------------------------------------------------------------------

## 19. Package and project-reference protection

Expected package delta:

`0`

Expected project-reference delta:

`0`

Do not add a SQLite package, ORM, database library, resilience package,
test package, or other dependency in WP05.

If compilation proves a new dependency is unavoidable, stop and report
the exact blocker rather than expanding scope.

------------------------------------------------------------------------

## 20. Test authority

Follow the Release 1.1 file manifest exactly.

If the manifest authorizes WP05-specific tests, add only the minimum
tests required by that manifest and current testing conventions.

If the manifest reserves comprehensive Domain/Application testing for
WP13 and does not authorize WP05 test-file changes, do not modify tests
merely to increase coverage.

Regardless of test-file mutation authority, all existing permanent
suites must remain green.

Do not alter tests to weaken accepted behavior.

------------------------------------------------------------------------

## 21. Architecture tests

Do not modify architecture tests unless the Release 1.1 file manifest
explicitly assigns an architecture-test change to WP05.

Existing architecture tests must continue to prove:

-   Domain has no project dependency
-   Application depends only on Domain
-   Infrastructure depends on Application
-   Worker depends only on Application and Infrastructure
-   no prohibited cycles or reverse dependencies exist

------------------------------------------------------------------------

## 22. File-scope discipline

Before mutation, derive the exact authorized WP05 file set from:

1.  Release 1.1 file manifest,
2.  current repository truth,
3.  this prompt.

Classify every changed/untracked path as:

-   PRE-EXISTING AUTHORIZED CUMULATIVE ARTIFACT
-   WP05 AUTHORIZED
-   UNEXPECTED

Do not modify earlier cumulative artifacts unless this WP05 authority
explicitly requires it.

Do not stage any file.

If an unexpected path appears because of execution tooling, do not
silently include it in WP05.

------------------------------------------------------------------------

## 23. Cumulative untracked-artifact preservation

The repository may contain accepted but not yet integrated Release 1.1
governance/research/implementation artifacts from earlier work packages.

Preserve them.

Do not:

-   delete them
-   normalize them
-   stage them
-   rewrite them
-   fold them into WP05
-   treat them as unexpected merely because they are untracked

Reconcile them against predecessor evidence and the Release 1.1
manifest.

WP05 may add only its authorized files to this cumulative working set.

------------------------------------------------------------------------

## 24. GitHub issue lifecycle

Before implementation, verify:

-   issue `#107` exists
-   issue `#106` is Closed/Done
-   WP05 dependencies match authoritative planning
-   issue `#107` is Open
-   Project status is `Backlog`
-   milestone is the authoritative Release 1.1 milestone
-   Release field is `1.1`
-   Priority is `P1`
-   Area and label match planning
-   WP06+ remain not started

After all preconditions pass and immediately before repository mutation,
move issue `#107` to `In Progress` using existing Project conventions.

After all mandatory WP05 validation passes:

-   close issue `#107`
-   ensure Project status becomes `Done` through existing automation or
    the minimum authorized status mutation if required by established
    workflow
-   verify issue `#108` remains Open/Backlog

Do not mutate unrelated issues.

------------------------------------------------------------------------

## 25. Git/GitHub mutation prohibition

WP05 does **not** authorize:

-   branch creation
-   staging
-   commit
-   push
-   pull request
-   merge
-   tag
-   GitHub Release
-   milestone closure
-   Release 1.2 planning

The only GitHub mutations authorized are the normal lifecycle
transitions for issue `#107` required by Section 24.

------------------------------------------------------------------------

## 26. Pre-mutation technical baseline

Before editing production content, run the repository's canonical
baseline required by current engineering governance.

At minimum capture:

-   branch
-   HEAD
-   `origin/main`
-   ahead/behind
-   staged count
-   tracked modification count
-   untracked classification
-   restore result
-   format verification
-   build warnings/errors
-   each permanent test suite count
-   Architecture.Tests count
-   `git diff --check`
-   `git diff --cached --check`

If the baseline is red for a reason unrelated to WP05, stop unless an
existing authority explicitly permits continuing.

------------------------------------------------------------------------

## 27. Implementation constraints

Implementation must be production-quality but minimal.

Required qualities:

-   clear capability-oriented naming
-   immutable/read-only result data where consistent with repository
    conventions
-   deterministic behavior
-   no hidden provider call
-   no hidden storage implementation
-   no global mutable state
-   no service locator
-   no static mutable persistence state
-   no unnecessary abstractions
-   no generic repository
-   no premature async conversion
-   no speculative cancellation-token introduction
-   no logging changes unless explicitly required by existing use-case
    conventions and manifest scope

------------------------------------------------------------------------

## 28. Dependency-call evidence

After implementation, prove from source that the WP05 Application use
case depends on abstractions only.

Report:

-   Application dependencies used
-   whether `IObservationSource` is involved
-   whether `IHistoricalObservationStore` is involved
-   exact invocation order if both are coordinated
-   provider-specific references: expected `0`
-   SQLite/storage-engine references: expected `0`
-   Worker references: expected `0`

If both acquisition and persistence are coordinated, prove persistence
receives the normalized Domain observations produced by the Application
flow, not provider DTOs.

------------------------------------------------------------------------

## 29. Semantic evidence required

The execution report must explicitly explain the resulting behavior for:

-   newly accepted persistence
-   idempotent duplicate persistence
-   conflicting duplicate persistence
-   storage unavailable
-   invalid persistence data
-   empty input, according to the accepted/reconciled contract
-   successful empty retrieval, if retrieval is in WP05 scope

Do not merely state "handled"; identify the result path/type used.

------------------------------------------------------------------------

## 30. Validation after implementation

Run all repository-required validation, including at minimum:

1.  restore
2.  format verification
3.  build
4.  Domain.Tests
5.  Application.Tests
6.  Infrastructure.Tests
7.  Architecture.Tests
8.  canonical `eng/verify.ps1`
9.  `git diff --check`
10. `git diff --cached --check`

Required final technical state:

-   restore: PASS
-   format verification: PASS
-   build: PASS
-   warnings: `0`
-   errors: `0`
-   all permanent tests: PASS
-   Architecture.Tests: PASS
-   canonical verification: PASS
-   both diff checks: PASS

Report exact test counts from repository truth. Do not assume the WP04
count remains 105 if WP05 authority legitimately changes tests.

------------------------------------------------------------------------

## 31. Architecture validation

Reconfirm the production dependency graph after WP05:

``` text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Report:

-   unexpected project-reference delta
-   cycles
-   Infrastructure references from Application
-   Worker references from Application
-   persistence-engine references from Application

Expected values are zero unless an accepted authority says otherwise.

------------------------------------------------------------------------

## 32. Security and data-safety validation

Verify WP05 introduces no:

-   credentials
-   API keys
-   connection strings
-   database paths
-   SQL
-   provider secrets
-   secret logging
-   sensitive query parameters

No live provider call is required to accept WP05.

No real persistence engine call is required to accept WP05.

------------------------------------------------------------------------

## 33. Diff and mutation review

Before declaring success, inspect all repository changes.

Report:

-   exact WP05 files added/modified
-   predecessor artifacts preserved
-   tracked modifications
-   staged files
-   untracked files/classification
-   package delta
-   project-reference delta
-   Domain delta
-   Infrastructure delta
-   Worker delta
-   test delta
-   unexpected paths

Staged files must remain `0`.

Do not clean up accepted predecessor artifacts merely to make the
working tree smaller.

------------------------------------------------------------------------

## 34. WP06 protection

Do not begin WP06.

Specifically do not define or implement:

-   SQLite table schema
-   columns
-   indexes
-   uniqueness constraints
-   database file layout
-   DDL
-   migrations
-   physical timestamp representation
-   physical decimal representation

WP06 --- Storage Physical Model remains separately gated.

------------------------------------------------------------------------

## 35. WP05 acceptance criteria

WP05 is accepted only if all applicable criteria pass:

-   predecessor WP04 is accepted and issue #106 is Closed/Done
-   issue #107 lifecycle is correctly managed
-   exact WP05 file scope is respected
-   Application persistence orchestration exists
-   orchestration uses WP04 contracts rather than concrete storage
-   acquisition/persistence separation remains explicit
-   provider-independent: PASS
-   storage-independent: PASS
-   Domain delta: `0`
-   Infrastructure delta: `0`
-   Worker delta: `0`
-   SQLite/package delta: `0`
-   project-reference delta: `0`
-   accepted persistence outcomes remain distinguishable
-   accepted failures remain distinguishable
-   conflict is not converted to idempotent success
-   target and observation fidelity are preserved
-   no physical storage model is introduced
-   no unauthorized tests/files are changed
-   restore/build/tests/canonical verification pass
-   build warnings/errors: `0/0`
-   architecture remains valid
-   both diff checks pass
-   staged files: `0`
-   WP06 not started
-   no unauthorized Git/GitHub integration occurs

Any failed mandatory criterion blocks the success terminal.

------------------------------------------------------------------------

## 36. Required execution report

Produce a detailed report with numbered sections covering at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor / Planning Gates
7.  Issue #107 Lifecycle
8.  Initial Technical Baseline
9.  Application Convention Inspection
10. WP04 Contract Reconciliation
11. WP05 Integration Model Decision
12. Rejected Alternative
13. Input / Validation Semantics
14. Empty-Input Semantics
15. Persistence Outcome Handling
16. Failure Handling
17. Conflict Handling
18. Retrieval Scope, if applicable
19. Target / Observation Fidelity
20. Files Added / Modified
21. Domain Delta
22. Infrastructure Delta
23. Worker Delta
24. Package / Project-Reference Delta
25. Test Delta
26. Dependency / Architecture Evidence
27. Security Evidence
28. Restore Evidence
29. Build Evidence
30. Test Evidence
31. Canonical Verification
32. Diff / Formatting Validation
33. Mutation Accounting
34. WP06 Protection
35. Findings / Blockers
36. Acceptance Matrix
37. Final GitHub State
38. Final Repository State
39. Final Decision
40. Next Authorized Work Package

Use actual repository/GitHub evidence rather than expected values when
reporting results.

------------------------------------------------------------------------

## 37. Success terminal

Only when every mandatory WP05 criterion passes, end exactly with:

``` text
RELEASE 1.1 WP05 COMPLETE

PERSISTENCE USE-CASE INTEGRATION:
Application orchestration: PASS
Provider-independent: PASS
Storage-independent: PASS
Domain delta: 0
Infrastructure delta: 0
Worker delta: 0
SQLite/package delta: 0
Persistence outcomes preserved: PASS
Acquisition/persistence separation: PASS

NEXT AUTHORIZED WORK PACKAGE:
WP06 — Storage Physical Model
GitHub issue #108
```

------------------------------------------------------------------------

## 38. Blocked terminal

If a mandatory gate fails or additional authority is required, do not
emit the success terminal.

End with:

``` text
RELEASE 1.1 WP05 BLOCKED
```

and report:

-   exact blocker
-   evidence
-   affected authority
-   minimum corrective scope
-   whether issue #107 was returned to or left in the appropriate
    non-terminal Project state
-   confirmation that WP06 was not started

------------------------------------------------------------------------

## 39. Final execution instruction

Proceed autonomously through inspection, reconciliation, implementation,
validation, and reporting within this authority.

Do not ask for confirmation for ordinary WP05 implementation decisions
that are already resolved by repository conventions and accepted
predecessor evidence.

Stop rather than exceed authority.

Do not begin WP06.

# Release 1.1 WP04 --- Application Persistence Contracts --- Codex Execution Authority

## 1. Authority

This document is the authoritative execution contract for Release 1.1
WP04 --- **Application Persistence Contracts** (GitHub issue #106).

Execute only WP04. Do not begin WP05 or any later work package.

The accepted Release 1.1 execution plan, file manifest, accepted
WP01--WP03 results, WP02 persistence technology decision, WP03
persistence semantics, current repository truth, and GitHub planning
state are controlling inputs. When they conflict, stop and report the
conflict rather than inventing a reconciliation.

## 2. Objective

Design and implement the minimum provider-independent and
storage-independent **Application-layer persistence contracts** required
to represent the persistence semantics accepted by WP03.

WP04 must make the accepted persistence operations and outcomes
expressible at the Application boundary while preserving the existing
architecture:

``` text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

WP04 owns contract design only. It does not own persistence
orchestration, SQLite implementation, schema design, connection
management, repository implementation, Worker integration, or
persistence tests belonging to later work packages.

## 3. Mandatory Starting State

Before mutation, reconcile all of the following.

### Git / repository

Expected baseline:

-   Branch: `main`
-   `main` synchronized with `origin/main`
-   No staged tracked changes
-   No unexpected tracked modifications
-   Only previously authorized cumulative Release 1.1
    governance/research artifacts may be untracked
-   No WP04 implementation already present unless it can be proven to be
    authorized current work

Do not discard, overwrite, stage, commit, or delete prior authorized
artifacts merely to make the tree clean.

### Release planning

Required:

-   Release 1.0 remains closed
-   Milestone #52 remains OPEN
-   WP01 / issue #103: CLOSED / Done
-   WP02 / issue #104: CLOSED / Done
-   WP03 / issue #105: CLOSED / Done
-   WP04 / issue #106: OPEN / Backlog at start
-   WP05--WP16: OPEN / Backlog
-   Active Release 1.2 planning: 0
-   Dependency drift: 0
-   WP04 dependency: exactly issue #105

If these conditions do not reconcile, stop before implementation.

## 4. Authorities to Read Completely

Before design or mutation, read:

1.  `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
2.  `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`
3.  Accepted WP01 authority/result
4.  Accepted WP02 authority/result
5.  `MARKET_DATA_PERSISTENCE_ASSESSMENT.md`
6.  `MARKET_DATA_PERSISTENCE_DECISION.md`
7.  Accepted WP03 authority/result
8.  Current Domain and Application production code
9.  Current Domain and Application tests
10. Architecture tests and architecture documentation relevant to
    Application contracts
11. GitHub issue #106 and its Project metadata

Do not infer repository facts from this prompt when current source code
can establish them directly.

## 5. Accepted WP03 Semantic Contract

WP04 must preserve the following decisions exactly.

### Historical identity

Persistence identity is:

``` text
exact validated target/instrument context + semantic observation instant
```

The semantic instant is the absolute point in time represented by
`DateTimeOffset`.

Do not introduce provider IDs, database row IDs, SQLite keys, or
physical storage encoding into Application identity.

Do not invent target canonicalization beyond current repository
semantics.

### Timestamp fidelity

Contracts must permit later implementation to prove:

-   Same absolute observation instant after round trip
-   Exact original offset-aware `DateTimeOffset` representation after
    round trip
-   No machine-local, database-local, unspecified, or provider-local
    conversion

### Price fidelity

Contracts must preserve the normalized Domain `decimal` value exactly.

Do not introduce floating-point persistence values, provider transport
strings, rounding rules, SQL affinity, or converter mechanics.

### Ordering

Historical retrieval semantics are:

``` text
observation instant ascending
```

Insertion order, provider order, database order, row IDs, and arbitrary
tie-breakers are non-semantic.

### Equivalent duplicate

Equivalent duplicate means same:

-   Exact target/instrument context
-   Semantic observation instant
-   Exact offset-aware timestamp representation
-   Normalized decimal price

Outcome:

``` text
IDEMPOTENT
```

Repeated persistence leaves semantic stored history unchanged.

### Conflicting duplicate

Same persistence identity with a non-equivalent observation value is:

``` text
DETERMINISTIC CONFLICT
```

It must not silently overwrite, correct, mutate, or masquerade as
idempotent success.

### Historical mutation

Release 1.1 history is semantically immutable:

-   New identity → may be added
-   Equivalent identity → idempotent
-   Conflicting identity → deterministic conflict
-   Update/correction/delete → out of scope

### Empty retrieval

A valid query with no persisted observations is:

``` text
SUCCESSFUL EMPTY RESULT
```

It is not null, not an `ObservationSeries`, not a storage failure, and
not an invalid request.

### Boundary ownership

-   Domain owns observation value meaning and invariants.
-   Application owns provider-independent persistence/retrieval
    contracts and outcome vocabulary.
-   Infrastructure will later own SQLite mapping, schema, transactions,
    durability, retrieval implementation, and failure mapping.

## 6. Required Design Investigation

Before creating types, inspect existing Application conventions and
answer from repository truth:

-   How Application contracts/interfaces are named and organized
-   How requests/results/failures are represented today
-   Whether `CancellationToken` is part of current asynchronous
    boundaries
-   Existing namespace/folder conventions
-   Existing validation ownership
-   Existing use of records, enums, discriminated result-like types, or
    exceptions
-   Whether Application currently exposes Infrastructure mechanics
    anywhere
-   How target context is represented and validated
-   How `PriceObservation` and `ObservationSeries` are consumed

Prefer the smallest design consistent with existing conventions.

Do not introduce a generic repository abstraction merely because
persistence is involved.

## 7. Required Application Capabilities

WP04 must make exactly these semantic capabilities representable.

### A. Persist normalized historical observations

Application must expose a contract through which later use-case
orchestration can request persistence of normalized Domain observations
for an exact validated target/instrument context.

The contract must not expose:

-   SQLite
-   SQL
-   Tables
-   Connections
-   Transactions
-   Files
-   Provider DTOs
-   Twelve Data
-   Database-generated IDs

### B. Represent persistence outcome

The Application boundary must be able to distinguish at minimum:

1.  Newly accepted observation/history contribution
2.  Equivalent/idempotent persistence
3.  Deterministic conflicting duplicate
4.  Persistence/runtime failure when later mapped by Infrastructure

Do not prescribe row counts, SQL conflict modes, upsert semantics, or
database exception types.

Use the minimum outcome vocabulary necessary to preserve WP03 semantics
and support WP05.

### C. Retrieve persisted historical observations

Application must expose a target-scoped historical retrieval contract
suitable for later orchestration.

It must support:

-   Valid target context
-   Historical observations returned in semantic instant ascending order
-   Successful empty result without constructing an invalid empty
    `ObservationSeries`
-   Failure distinct from successful empty retrieval

Do not invent cross-target history semantics.

### D. Preserve Domain invariants

Do not weaken or duplicate Domain rules.

In particular:

-   `PriceObservation` remains the normalized observation value
-   `ObservationSeries` remains non-empty
-   WP04 must not modify Domain merely to make persistence contracts
    convenient

## 8. Contract Shape Constraints

Choose concrete type shapes only after repository inspection.

The final design must satisfy all of these:

-   Contracts live in Application
-   Domain remains unchanged unless an unavoidable contradiction is
    proven; expected Domain delta is `0`
-   Infrastructure types do not appear in Application public signatures
-   Provider types do not appear in Application public signatures
-   Persistence contracts are technology-independent
-   Empty retrieval is representable without null
-   Conflict is representable deterministically
-   Idempotency is distinguishable where required by later orchestration
-   Runtime/storage failure is distinguishable from semantic conflict
    and successful empty retrieval
-   Async/cancellation behavior follows existing Application conventions
-   No speculative CRUD surface
-   No update/delete contract
-   No generic repository base interface
-   No schema or physical model concepts

Avoid unnecessary abstractions, marker interfaces, inheritance
hierarchies, or framework dependencies.

## 9. Failure Vocabulary Boundary

WP04 may define provider-independent Application persistence
failure/outcome vocabulary only to the extent required for the contract.

It must not map:

-   SQLite result codes
-   SQLite exceptions
-   File-system errors
-   Locking modes
-   Busy timeouts
-   Connection failures
-   SQL constraint names

Those belong to later Infrastructure work, especially WP10.

If current Application conventions already provide a suitable failure
model, reuse or extend it minimally rather than creating a parallel
framework.

## 10. Validation Boundary

Preserve current ownership.

WP04 may require already-valid Application input and may express
contract-level preconditions consistent with repository conventions.

Do not:

-   Add SQLite validation
-   Add physical storage validation
-   Invent target normalization
-   Reimplement Domain price/timestamp invariants
-   Move provider validation into persistence contracts

Invalid requests must remain distinguishable from valid empty retrieval
and persistence/runtime failure.

## 11. Authorized Production Scope

WP04 may modify or add only the minimum Application production files
necessary for the accepted contracts.

Potential scope may include, if justified by repository conventions:

-   Persistence contract interfaces
-   Persistence request/value types owned by Application
-   Persistence/retrieval result types
-   Persistence outcome/failure vocabulary

The exact file set must be discovered and justified before mutation.

Do not modify Domain unless the required semantics are impossible to
express otherwise. If that occurs, stop and report rather than expanding
scope automatically.

## 12. Authorized Test Scope

WP04 may add or modify the minimum Application tests needed to prove the
new contracts' semantic shape where executable behavior exists.

Do not steal WP13, whose responsibility is the dedicated Domain &
Application persistence test work package.

Therefore:

-   Test concrete behavior introduced by WP04 when needed
-   Test contract/value invariants introduced by WP04 when needed
-   Do not build the comprehensive persistence test matrix reserved for
    WP13
-   Do not add Infrastructure persistence tests

If interfaces/types contain no executable behavior requiring tests,
explain why no new tests are necessary and rely on build/architecture
validation rather than creating meaningless tests.

## 13. Explicitly Forbidden Scope

WP04 must not implement or modify:

-   WP05 persistence use-case orchestration
-   WP06 storage physical model
-   WP07 SQLite engine/connection boundary
-   WP08 observation persistence implementation
-   WP09 historical retrieval implementation
-   WP10 storage failure mapping
-   WP11 DI/configuration
-   WP12 Worker persistent execution
-   WP13 comprehensive Domain/Application persistence tests
-   WP14 Infrastructure persistence tests
-   WP15 documentation/architecture alignment
-   WP16 full integration/acceptance
-   SQLite package references
-   SQL
-   Schema/migrations
-   Database files
-   Connection strings
-   Infrastructure persistence classes
-   Worker behavior
-   Provider acquisition behavior
-   Release 1.2 work

## 14. Package / Project Protection

Expected package delta:

``` text
0
```

Expected project-reference delta:

``` text
0
```

Do not add `Microsoft.Data.Sqlite` in WP04. WP02 selected it only as the
future minimum Infrastructure dependency.

If a package or project-reference change appears necessary, stop and
report the reason.

## 15. Architecture Protection

After implementation, prove:

``` text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Also prove:

-   Domain contains no persistence technology
-   Application contains no SQLite/SQL/provider transport mechanics
-   Infrastructure has not begun persistence implementation
-   Worker has not begun persistence orchestration
-   No dependency cycle exists

Architecture tests must continue to pass.

## 16. Issue #106 Lifecycle

Only after all starting gates pass:

1.  Move issue #106 from `Backlog` to `In Progress`.
2.  Perform WP04.
3.  Add a concise evidence comment summarizing the accepted Application
    contract design, files changed, validation evidence, and explicit
    WP05 handoff.
4.  Close issue #106 only after every WP04 acceptance gate passes.
5.  Confirm Project automation results in `Done`.

Do not progress any other issue.

If WP04 blocks after entering `In Progress`, leave #106 open and report
the blocker truthfully.

## 17. Whitespace Handling

Whitespace findings must not create another authority recursion.

For files **authorized and actually modified by WP04**, you are
authorized to remove whitespace violations reported by:

``` text
git diff --check
git diff --cached --check
```

provided that:

-   Correction is whitespace-only
-   Semantic content is unchanged
-   No unauthorized file is edited
-   No additional governance authority file is created

For pre-existing findings in files outside the WP04 authorized mutation
set, stop and classify them rather than silently expanding scope.

Do not normalize line endings or whitespace globally.

## 18. Git Protection

WP04 is an implementation work-package execution, not an integration
step.

Unless a higher explicit authority says otherwise:

-   Do not create a branch
-   Do not stage files
-   Do not commit
-   Do not amend
-   Do not push
-   Do not create a PR
-   Do not merge
-   Do not tag
-   Do not create a GitHub Release

Leave the authorized WP04 repository delta available for later governed
integration.

## 19. Mandatory Validation

Run before mutation to establish baseline and again after implementation
as applicable:

``` text
dotnet restore AIQuantTradingResearch.slnx --nologo
eng/verify.ps1
git diff --check
git diff --cached --check
```

Record:

-   Restore result
-   Format verification
-   Build warnings/errors
-   Domain.Tests
-   Application.Tests
-   Infrastructure.Tests
-   Architecture.Tests
-   Total permanent tests
-   Diff-check results

Expected pre-WP04 baseline from accepted WP03:

``` text
Domain.Tests:          11
Application.Tests:     16
Infrastructure.Tests:  65
Architecture.Tests:    13
Total:                 105
```

Do not hard-code the final test total; report repository truth after
WP04.

## 20. Required Contract Evidence

The final report must explicitly show the resulting Application contract
model, including:

-   Exact production files added/modified
-   Contract/interface names
-   Request/input types, if any
-   Persistence outcome/result types
-   Retrieval result shape
-   Empty-result representation
-   Conflict representation
-   Failure representation
-   Target identity representation
-   Timestamp/price fidelity preservation
-   Ordering contract
-   Async/cancellation shape
-   Why the design is storage-independent
-   Why the design is provider-independent
-   Why Domain delta remained zero, or a blocker if it could not

Do not merely state "contracts added"; explain their semantic
responsibilities.

## 21. WP05 Handoff

WP04 must finish with a precise handoff to WP05 --- **Persistence
Use-Case Integration**.

The handoff must identify what WP05 may now orchestrate using the
accepted contracts, without implementing it during WP04.

At minimum, state how WP05 can:

-   Invoke persistence through Application-owned abstractions
-   Distinguish newly accepted, idempotent, conflict, and failure
    outcomes
-   Retrieve target-scoped history
-   Distinguish successful empty retrieval from failure
-   Preserve acquisition/persistence separation

Do not prescribe WP05 implementation beyond what WP04 contracts make
possible.

## 22. Acceptance Gates

WP04 succeeds only if all are true:

### Governance

-   WP01 CLOSED / Done
-   WP02 CLOSED / Done
-   WP03 CLOSED / Done
-   WP04 is the only progressed work package
-   WP05--WP16 remain unstarted
-   Milestone #52 remains OPEN
-   Active Release 1.2 planning remains 0

### Scope

-   Application persistence contracts are explicit
-   Provider-independent
-   Storage-independent
-   Domain delta = 0
-   SQLite package delta = 0
-   Project-reference delta = 0
-   No SQL/schema/connection mechanics
-   No Infrastructure persistence implementation
-   No Worker persistence behavior
-   No WP05 orchestration

### Semantics

-   Target + semantic instant identity preserved
-   Exact offset-aware timestamp fidelity preserved by contract
-   Exact normalized decimal fidelity preserved by contract
-   Ascending historical ordering explicit
-   Equivalent duplicate → idempotent outcome
-   Conflicting duplicate → deterministic conflict
-   No silent overwrite/update/delete semantics
-   Successful empty retrieval representable
-   Runtime/storage failure distinct from empty and conflict

### Technical

-   Restore PASS
-   Format verification PASS
-   Build PASS
-   Build warnings = 0
-   Build errors = 0
-   All permanent tests PASS
-   Architecture.Tests PASS
-   `git diff --check` PASS
-   `git diff --cached --check` PASS
-   Architecture graph preserved
-   Unauthorized tracked paths = 0

### Lifecycle

-   Evidence comment added to #106
-   Issue #106 CLOSED
-   Project #106 Done
-   WP05 remains OPEN / Backlog

If any mandatory gate fails, do not emit the success terminal.

## 23. Required Execution Report

Produce a detailed report with at least these sections:

1.  Executive Summary
2.  Authorities Reviewed
3.  Authentication / Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  WP01--WP03 Completion Reconciliation
7.  Release 1.1 Planning Reconciliation
8.  WP04 Issue-State Handling
9.  Initial Technical Baseline
10. Application Convention Inspection
11. Accepted WP03 Semantic Inputs
12. Contract Design Decision
13. Persistence Write Contract
14. Persistence Outcome Vocabulary
15. Historical Retrieval Contract
16. Empty Retrieval Representation
17. Conflict Representation
18. Failure Representation
19. Target / Identity Representation
20. Timestamp / Price Fidelity
21. Ordering Semantics
22. Domain Delta
23. Application Production Changes
24. Application Test Changes
25. Package / Project Delta
26. Infrastructure / Worker Protection
27. Whitespace Evidence
28. Security / Credential Safety
29. Repository Mutation Accounting
30. Final Technical Validation
31. Architecture Boundary Validation
32. Git / GitHub Protection
33. WP05 Protection
34. WP05 Handoff Requirements
35. Findings / Observations
36. WP04 Acceptance Matrix
37. Final GitHub State
38. Final Repository State
39. Final Decision
40. Next Authorized Work Package

## 24. Success Terminal

Only if every WP04 acceptance gate passes, end exactly with:

``` text
RELEASE 1.1 WP04 COMPLETE

APPLICATION PERSISTENCE CONTRACTS:
Provider-independent: PASS
Storage-independent: PASS
Domain delta: 0
SQLite/package delta: 0
Empty retrieval: SUCCESSFUL EMPTY RESULT
Equivalent duplicate: IDEMPOTENT
Conflicting duplicate: DETERMINISTIC CONFLICT

NEXT AUTHORIZED WORK PACKAGE:
WP05 — Persistence Use-Case Integration
GitHub issue #107
```

## 25. Blocked Terminal

If execution cannot satisfy the authority, end with:

``` text
RELEASE 1.1 WP04 BLOCKED

BLOCKER:
<precise blocker>

REPOSITORY STATE:
<truthful concise state>

GITHUB STATE:
<truthful concise state>

NEXT AUTHORIZED ACTION:
<minimum additional authority or corrective action required>
```

Do not claim WP04 completion when any mandatory gate is unresolved.

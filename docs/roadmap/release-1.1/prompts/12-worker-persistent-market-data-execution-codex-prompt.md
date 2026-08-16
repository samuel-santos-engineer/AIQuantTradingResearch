# Release 1.1 WP12 --- Worker Persistent Market-Data Execution --- Codex Prompt

## 1. Authority

This document is the authoritative execution contract for:

**Release 1.1 --- WP12: Worker Persistent Market-Data Execution**\
**GitHub issue:** #114\
**Milestone:** #52 --- Phase 3 - Release 1.1: Market Data Persistence
Foundation

Execute only WP12.

This authority is subordinate to the accepted Release 1.1 governance
baseline and must be reconciled with the current repository and GitHub
truth before mutation.

The authoritative Release 1.1 planning documents are:

-   `docs/roadmap/release-1.1/RELEASE_1.1_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.1/RELEASE_1.1_FILE_MANIFEST.md`

Also treat the accepted WP01--WP11 authorities and execution results as
predecessor evidence. Where repository truth and an execution-report
statement differ, stop and report the conflict unless this prompt
explicitly authorizes reconciliation.

This prompt authorizes implementation, validation, and GitHub lifecycle
mutation for **WP12 only**. It does **not** authorize Git integration,
commits, pushes, pull requests, merges, tags, GitHub Releases, Release
1.1 closure, or any WP13+ implementation.

------------------------------------------------------------------------

## 2. Objective

Implement the first real Release 1.1 production execution path that
acquires normalized market observations and persists them through the
accepted Application persistence boundary.

The intended architectural flow is:

``` text
Worker
  → Application acquisition/use-case boundary
  → IObservationSource
  → Twelve Data Infrastructure provider
  → normalized Domain PriceObservation values
  → Application persistence use-case boundary
  → IHistoricalObservationStore
  → SQLite Infrastructure implementation
```

The Worker remains the composition/execution host. Application remains
responsible for use-case semantics. Infrastructure remains responsible
for Twelve Data and SQLite mechanics.

WP12 must connect the already accepted Release 1.0 acquisition
capability to the accepted Release 1.1 persistence capability without
redesigning either subsystem.

------------------------------------------------------------------------

## 3. Mandatory Starting-State Gate

Before changing source code or GitHub lifecycle state, inspect and
record the actual state.

Require all of the following:

1.  Repository is `samuel-santos-engineer/AIQuantTradingResearch`.
2.  Current branch is `main`.
3.  Local `main` equals `origin/main`, with ahead/behind `0/0`.
4.  No staged changes exist.
5.  Existing tracked/untracked Release 1.1 work can be fully classified
    as authorized cumulative WP01--WP11 work plus the WP12 prompt pair.
6.  Unexpected paths are zero.
7.  Release 1.0 is closed.
8.  Issues #103--#113 are Closed/Done.
9.  Issue #114 is Open/Backlog.
10. Issue #115 remains Open/Backlog.
11. Milestone #52 is OPEN.
12. Legacy milestones #42 and #43 are CLOSED and empty.
13. Active Release 1.2 planning is zero.
14. No WP13+ implementation has begun.
15. The accepted WP04--WP11 persistence contracts and implementation are
    present.

If any mandatory condition fails, **do not begin WP12**. Do not perform
a compensating governance mutation unless explicitly authorized here.
Report the smallest corrective authority required.

Do not repeat the earlier Release 1.2 legacy-milestone reconciliation
automatically if milestone #43 has drifted again; that is outside WP12
authority.

------------------------------------------------------------------------

## 4. Baseline Validation Gate

Before moving issue #114 to `In Progress`, run and record the
repository's canonical baseline.

At minimum:

``` powershell
dotnet restore AIQuantTradingResearch.slnx --nologo
dotnet format AIQuantTradingResearch.slnx --verify-no-changes --no-restore
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
.\eng\verify.ps1
git diff --check
git diff --cached --check
```

Record the permanent test totals by suite.

Expected predecessor baseline from WP11:

-   Domain.Tests: 11/11
-   Application.Tests: 16/16
-   Infrastructure.Tests: 65/65
-   Architecture.Tests: 13/13
-   Total: 105/105
-   Build warnings: 0
-   Build errors: 0

These numbers are evidence, not permission to fabricate results. If
current truth differs, investigate and report it.

Only after the starting-state and baseline gates pass may issue #114
move:

``` text
Backlog → In Progress
```

Do not progress any other issue.

------------------------------------------------------------------------

## 5. Accepted Predecessor Semantics

### 5.1 Release 1.0 acquisition

Preserve the existing Release 1.0 market-data acquisition behavior and
its provider abstraction.

Do not:

-   move Twelve Data mechanics into Worker or Application;
-   introduce provider DTOs outside Infrastructure;
-   bypass `IObservationSource`;
-   duplicate provider HTTP logic;
-   redesign normalized `PriceObservation` semantics;
-   weaken existing mandatory `TwelveData:ApiKey` handling.

### 5.2 WP03 persistence semantics

Preserve:

-   exact opaque target identity;
-   semantic-instant observation identity;
-   exact timestamp/offset fidelity;
-   exact decimal fidelity;
-   immutable historical observations;
-   equivalent duplicate → idempotent;
-   conflicting duplicate → deterministic conflict;
-   ascending historical retrieval;
-   valid no-history retrieval → successful empty result.

### 5.3 WP04 persistence contracts

Reuse the accepted Application persistence contracts unchanged unless a
genuine contradiction makes WP12 impossible.

In particular preserve:

-   `IHistoricalObservationStore`;
-   `ObservationPersistenceOutcome`;
-   `PersistenceFailure`;
-   `ObservationPersistenceResult`;
-   `HistoricalObservationResult`.

WP12 must not redesign the store contract.

### 5.4 WP05 persistence use case

The accepted model is a dedicated Application persistence use case
receiving already normalized observations.

Reuse:

-   `IPersistHistoricalObservationsUseCase`;
-   `PersistHistoricalObservationsRequest`;
-   `PersistHistoricalObservationsFailure`;
-   `PersistHistoricalObservationsResult`;
-   `PersistHistoricalObservationsUseCase`.

Acquisition and persistence remain separate Application capabilities.
WP12 may orchestrate them at the host boundary, but must not collapse
them into a generic repository or move orchestration into
Infrastructure.

### 5.5 WP06--WP10 SQLite implementation

Preserve the accepted physical model, connection/bootstrap boundary,
store behavior, retrieval semantics, atomicity, and failure mapping.

WP12 must not introduce new SQL, schema, migrations, SQLite failure
categories, retry policy, repair behavior, or persistence semantics
unless required solely to correct an objectively broken predecessor
implementation. If such a contradiction is discovered, stop and report
it rather than silently redesigning WP06--WP10.

### 5.6 WP11 DI/configuration

WP11 established:

-   `IHistoricalObservationStore → SqliteHistoricalObservationStore`;
-   `ISqliteConnectionFactory → SqliteConnectionFactory`;
-   `Persistence:DatabasePath`;
-   deterministic rejection of missing/blank persistence path;
-   no hidden default database path;
-   no in-memory production fallback;
-   operation-owned SQLite connections;
-   Release 1.0 provider DI preserved.

WP11 deliberately did **not** register the WP05 persistence use case
because production did not yet invoke it.

WP12 owns the minimum composition change necessary for the real Worker
execution graph to resolve the persistence use case.

------------------------------------------------------------------------

## 6. Required Design Discovery

Before implementation, inspect the actual current code and document:

1.  Worker execution/lifecycle structure.
2.  Existing Release 1.0 Application acquisition/use-case entry point.
3.  Existing request/result/failure vocabulary used by the Worker.
4.  Existing `AddApplication` registration convention.
5.  Existing `AddInfrastructure` registration convention.
6.  Current Worker configuration handoff.
7.  Current logging conventions.
8.  Existing provider-call lifecycle.
9.  The exact WP05 persistence use-case constructor and registration
    requirements.
10. Whether the Worker currently performs one bounded execution or uses
    a hosted/background lifecycle.

Prefer the smallest design consistent with existing conventions.

Do not invent a scheduler, daemon loop, queue, background pipeline,
hosted-service framework, retry loop, cache, batch framework, or new
architectural abstraction merely because one might be useful later.

------------------------------------------------------------------------

## 7. Required Application Composition Reconciliation

WP12 must ensure the persistence use case can be resolved through the
production DI graph.

Prefer registering the Application implementation through the existing
Application composition root/convention rather than registering an
Application implementation from Infrastructure.

Expected semantic mapping:

``` text
IPersistHistoricalObservationsUseCase
    → PersistHistoricalObservationsUseCase
```

Use a lifetime consistent with the existing Application use-case
conventions and its dependencies.

Requirements:

-   exactly one effective production registration;
-   no service-locator pattern;
-   no manual construction in Worker if existing DI conventions can
    express the graph cleanly;
-   no Infrastructure ownership of Application implementation
    registration unless repository conventions make that unavoidable;
-   no SQLite types in Application registration;
-   no database creation merely from container construction or service
    resolution.

If registration requires a minimal modification to the existing
Application DI file, that is authorized.

------------------------------------------------------------------------

## 8. Worker Execution Contract

Implement the narrowest production flow that:

1.  obtains the configured market-data target through the existing
    Release 1.0 configuration/Worker model;
2.  executes the existing Application acquisition capability;
3.  receives normalized Domain `PriceObservation` values;
4.  if acquisition succeeds, passes the exact target and normalized
    observations to the WP05 persistence use case;
5.  evaluates the persistence result explicitly;
6.  terminates/reports according to the repository's existing Worker
    conventions.

The Worker may coordinate the two Application use cases. It must not:

-   call Twelve Data HTTP APIs directly;
-   use provider DTOs;
-   open SQLite connections;
-   issue SQL;
-   instantiate `SqliteHistoricalObservationStore`;
-   classify SQLite exceptions;
-   bypass `IPersistHistoricalObservationsUseCase`;
-   transform timestamp or decimal values for storage;
-   normalize the opaque target;
-   implement retrieval orchestration;
-   introduce Release 1.2 pipeline behavior.

------------------------------------------------------------------------

## 9. Acquisition-to-Persistence Handoff

The handoff must preserve the exact successful acquisition output.

Requirements:

-   target identity remains exact;
-   normalized `PriceObservation` values are forwarded without
    storage-specific conversion;
-   observation ordering must not be arbitrarily changed;
-   no duplicate suppression in Worker;
-   no conflict resolution in Worker;
-   no overwrite behavior;
-   no persistence attempt after an acquisition failure;
-   no provider re-fetch solely to persist;
-   no persistence-specific DTO.

If the acquisition result can legitimately contain no observations,
reconcile the existing Release 1.0 semantics with WP05's rule that an
empty persistence request is invalid. Do not guess. Use current
contracts to choose a deterministic host behavior and document the
decision.

------------------------------------------------------------------------

## 10. Persistence Outcome Handling

WP12 must explicitly distinguish the accepted persistence outcomes.

At minimum:

### NewlyAccepted

Treat as successful persistence execution.

### Idempotent

Treat as a successful non-mutating persistence execution. Do not convert
it to a conflict or failure.

### Conflict

Treat as a deterministic semantic conflict. Do not overwrite, delete,
retry automatically, or misclassify it as infrastructure unavailability.

### Persistence failure --- Unavailable

Treat as a persistence execution failure according to existing Worker
exit/result conventions.

Do not add retry/resilience policy in WP12.

### Persistence failure --- InvalidData

Treat as a persistence execution failure according to existing Worker
exit/result conventions.

Do not repair or discard stored/input data in Worker.

The exact exit codes/messages should follow current repository
conventions rather than inventing an unrelated scheme.

------------------------------------------------------------------------

## 11. Acquisition Failure Preservation

Existing acquisition/provider failure behavior must remain correct.

Requirements:

-   acquisition failure prevents persistence invocation;
-   missing/invalid Twelve Data configuration remains deterministic;
-   provider errors are not converted into SQLite/persistence errors;
-   persistence configuration validation does not cause a provider call
    when execution cannot be composed safely;
-   no credentials are printed;
-   no API key is included in exception or log output.

Where both `TwelveData:ApiKey` and `Persistence:DatabasePath` are
mandatory for the real WP12 flow, define a deterministic
startup/configuration-validation order and prove it without making
external calls.

------------------------------------------------------------------------

## 12. Persistence Configuration Behavior

The real Worker path must use:

``` text
Persistence:DatabasePath
```

Preserve WP11 rules:

-   no default production path;
-   no hard-coded personal/machine path;
-   no implicit in-memory fallback;
-   no connection string in source;
-   blank/missing value rejected deterministically;
-   path is handed to Infrastructure without semantic rewriting.

Do not commit a real database file.

If repository configuration templates/examples require a key placeholder
to make the new mandatory setting discoverable, a non-secret
placeholder/documentation-only change is authorized only if consistent
with existing configuration conventions. Do not introduce a
machine-specific value.

------------------------------------------------------------------------

## 13. Logging and Observability

Use existing logging conventions only.

Logging may identify:

-   execution phase;
-   target where existing conventions permit it;
-   acquisition success/failure;
-   persistence success/idempotency/conflict/failure;
-   counts where meaningful.

Logging must not expose:

-   API keys;
-   secrets;
-   connection strings;
-   sensitive database contents;
-   full exception data if it contains protected configuration;
-   provider raw payloads merely for WP12 diagnostics.

Do not introduce a new telemetry stack, metrics system, tracing
framework, or persistence observability subsystem.

------------------------------------------------------------------------

## 14. Worker Lifecycle / Bounded Execution

Preserve the existing Worker lifecycle unless a minimal adjustment is
required to insert persistence after successful acquisition.

WP12 does not authorize:

-   recurring scheduling;
-   timers;
-   cron behavior;
-   continuous ingestion;
-   message queues;
-   multi-target pipeline orchestration;
-   retry loops;
-   parallel ingestion;
-   checkpointing;
-   incremental cursor state;
-   backfill orchestration.

Those are later concerns.

The WP12 validation path should be bounded and deterministic.

------------------------------------------------------------------------

## 15. Offline Validation Requirement

Do not require a real Twelve Data credential or live provider call to
prove WP12.

Build a focused temporary validation/probe if permanent WP13/WP14 tests
are not yet authorized.

The probe must use real production DI/composition where practical and
controlled/fake Application/provider boundaries as necessary to remain
offline.

Prove at minimum:

1.  the full production service graph can be constructed with valid
    configuration;
2.  persistence use case resolves through DI;
3.  successful normalized acquisition is handed to persistence;
4.  target is preserved exactly;
5.  timestamp/offset and decimal values survive the complete persistence
    path;
6.  persisted data can be observed through the accepted retrieval
    boundary for proof only;
7.  newly accepted data is persisted;
8.  equivalent re-execution remains idempotent;
9.  conflicting duplicate is deterministic and non-destructive;
10. acquisition failure causes zero persistence operations;
11. persistence `Unavailable` is distinguishable from acquisition
    failure;
12. persistence `InvalidData` is distinguishable;
13. missing/blank `Persistence:DatabasePath` fails deterministically;
14. missing `TwelveData:ApiKey` remains deterministic;
15. no external network call occurs during offline validation;
16. no temporary database/probe residue remains.

A focused temporary probe is allowed. Remove it completely before final
acceptance.

Do not add the comprehensive permanent WP13/WP14 test suites in WP12.

------------------------------------------------------------------------

## 16. Real Provider Validation Safety

A real Twelve Data API call is **not mandatory** for WP12 acceptance.

If a credential is already available through the user's normal
environment and the existing repository validation convention safely
supports a bounded provider call, do not use it unless the current
authority and repository practice clearly permit it.

Never:

-   print the credential;
-   commit the credential;
-   write it into a prompt/report;
-   infer or fabricate a credential;
-   require the user to expose it in chat.

Offline proof is sufficient for WP12's orchestration contract.

------------------------------------------------------------------------

## 17. Architecture Requirements

After WP12, preserve the production dependency graph:

``` text
Domain         → none
Application    → Domain
Infrastructure → Application
Worker         → Application, Infrastructure
```

Requirements:

-   no new project-reference edge unless strictly necessary and already
    permitted by architecture;
-   no cycles;
-   no SQLite reference in Domain or Application;
-   no Twelve Data mechanics in Domain/Application/Worker;
-   no Infrastructure reference from Application;
-   Worker remains composition/execution only;
-   persistence orchestration does not migrate into Infrastructure.

Architecture tests must continue to pass.

------------------------------------------------------------------------

## 18. Authorized Mutation Surface

Modify only the minimum files necessary to implement WP12.

Likely authorized categories include:

-   existing Application DI registration/composition file, if needed for
    `IPersistHistoricalObservationsUseCase`;
-   existing Worker execution/orchestration file(s);
-   Worker configuration example/template only if necessary to expose
    `Persistence:DatabasePath` consistently;
-   minimal new Worker-owned orchestration type only if current
    structure clearly justifies it.

Do not treat this list as permission to change all such files.

Every changed production path must be justified in the execution report.

Do not modify WP06--WP10 persistence implementation merely for
cleanup/refactoring.

Permanent tests belong primarily to WP13 and WP14. WP12 may use
temporary focused probes and must remove them.

------------------------------------------------------------------------

## 19. Explicitly Forbidden Scope

WP12 does **not** authorize:

-   new persistence contracts;
-   generic repository abstractions;
-   schema changes;
-   migrations;
-   new tables/columns/indexes;
-   new SQL behavior;
-   retrieval redesign;
-   new SQLite failure categories;
-   retry/resilience policy;
-   storage repair;
-   update/delete/upsert behavior;
-   provider redesign;
-   new market-data provider;
-   scheduling/pipeline framework;
-   continuous ingestion;
-   WP13 permanent Domain/Application test implementation;
-   WP14 permanent Infrastructure/persistence test implementation;
-   WP15 architecture/documentation alignment;
-   WP16 full acceptance/integration;
-   Git integration;
-   branch creation;
-   staging;
-   commits;
-   pushes;
-   PRs;
-   merges;
-   tags;
-   GitHub Releases;
-   Release 1.1 closure;
-   Release 1.2 work.

------------------------------------------------------------------------

## 20. Package and Reference Discipline

Prefer package delta `0` and project-reference delta `0`.

A new package is not expected for WP12.

If implementation appears to require a new package or project reference:

1.  stop;
2.  prove why existing platform/repository facilities cannot satisfy the
    requirement;
3.  report the blocker rather than adding it opportunistically.

Do not upgrade `Microsoft.Data.Sqlite` or unrelated dependencies.

------------------------------------------------------------------------

## 21. Whitespace and Line-Ending Handling

Whitespace findings must not create a recursive authority chain.

Before final acceptance run:

``` powershell
git diff --check
git diff --cached --check
```

If either reports whitespace violations in files legitimately modified
by WP12:

1.  classify each finding;
2.  correct only the reported whitespace defect;
3.  do not perform broad formatting or line-ending normalization;
4.  prove semantic equivalence for whitespace-only corrections;
5.  rerun both checks.

Zero or more such narrow whitespace corrections are authorized by this
prompt.

Benign Git LF/CRLF informational notices are not themselves authority to
normalize entire files.

Whitespace findings outside the WP12-authorized mutation surface are
blockers unless they pre-existed and do not affect WP12's diff.

------------------------------------------------------------------------

## 22. Final Validation Gate

After implementation, run the full canonical validation again.

At minimum:

``` powershell
dotnet restore AIQuantTradingResearch.slnx --nologo
dotnet format AIQuantTradingResearch.slnx --verify-no-changes --no-restore
dotnet build AIQuantTradingResearch.slnx --no-restore --nologo
.\eng\verify.ps1
git diff --check
git diff --cached --check
```

Record:

-   restore result;
-   format result;
-   build warnings/errors;
-   every permanent test suite count;
-   architecture-test count;
-   canonical verification result;
-   focused offline WP12 probe result;
-   package/reference delta;
-   changed-file accounting;
-   temporary residue;
-   working-tree classification.

All temporary probe files and temporary SQLite databases must be removed
before acceptance.

------------------------------------------------------------------------

## 23. Regression Gates

Explicitly prove that WP12 does not regress:

### Release 1.0

-   provider abstraction;
-   Twelve Data registration;
-   provider configuration validation;
-   normalized Domain observations;
-   existing acquisition behavior.

### WP03--WP05

-   exact target identity;
-   persistence semantics;
-   Application contracts;
-   persistence use-case semantics.

### WP06--WP10

-   physical schema;
-   connection/bootstrap behavior;
-   write atomicity;
-   idempotency;
-   conflict behavior;
-   retrieval ordering/fidelity;
-   successful empty retrieval;
-   failure mapping.

### WP11

-   DI graph;
-   explicit database path;
-   no resolution-time database mutation;
-   operation-owned connections.

------------------------------------------------------------------------

## 24. GitHub Lifecycle

Only after every WP12 acceptance gate passes:

1.  add concise execution evidence to issue #114;
2.  close issue #114;
3.  ensure its Project status is `Done` (allow existing automation to
    perform the transition if applicable);
4.  verify issue #115 remains Open/Backlog;
5.  verify no other WP progressed;
6.  verify milestone #52 remains OPEN.

If validation fails, leave #114 open/in progress and report the blocker.

Do not mutate issue #115 or later issues except read-only verification.

------------------------------------------------------------------------

## 25. Git Protection

WP12 is a working-tree implementation package, not an integration
package.

Do not:

``` text
git add
git commit
git push
git merge
git rebase
git reset
git checkout <new branch>
git switch -c
gh pr create
gh pr merge
```

Do not create or delete tags.

Do not create a GitHub Release.

Leave accepted cumulative Release 1.1 work uncommitted exactly as
required by the Release 1.1 execution model.

------------------------------------------------------------------------

## 26. WP13 Protection

WP13 is:

**Domain & Application Tests --- issue #115**

WP12 must not begin WP13.

Do not create the comprehensive permanent Domain/Application persistence
test suite reserved for WP13.

Temporary focused validation is allowed only when removed before WP12
completion.

At final acceptance verify:

``` text
Issue #115: OPEN / Backlog
WP13 implementation started: NO
```

------------------------------------------------------------------------

## 27. Required Acceptance Matrix

The final report must truthfully include at least:

  Requirement                                    Required final state
  ---------------------------------------------- ---------------------------
  WP11 predecessor                               PASS
  Issue #114 lifecycle                           Closed / Done
  Issue #115                                     Open / Backlog
  Existing acquisition boundary reused           PASS
  Existing persistence use case reused           PASS
  Persistence use case resolvable through DI     PASS
  Worker coordinates acquisition → persistence   PASS
  Worker direct provider HTTP                    NO
  Worker direct SQLite/SQL                       NO
  Exact target preserved                         PASS
  Normalized observations preserved              PASS
  NewlyAccepted handling                         PASS
  Idempotent handling                            PASS
  Conflict handling                              PASS
  Persistence Unavailable handling               PASS
  Persistence InvalidData handling               PASS
  Acquisition failure → persistence calls        0
  Missing API key behavior                       deterministic
  Missing database path behavior                 deterministic
  Hidden DB-path fallback                        NO
  External network required for acceptance       NO
  SQLite schema delta                            0
  Persistence contract delta                     0 unless blocker reported
  Package delta                                  0
  Project-reference delta                        0
  Architecture tests                             PASS
  Canonical verification                         PASS
  Diff checks                                    PASS
  Temporary residue                              0
  Git integration mutation                       0
  WP13 started                                   NO
  Release 1.2 active planning                    0

------------------------------------------------------------------------

## 28. Required Execution Report

Produce a detailed report titled:

``` text
Release 1.1 WP12 Execution Report
```

Use numbered sections and include, at minimum:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor/Lifecycle Gates
7.  Issue Lifecycle
8.  Initial Baseline
9.  Release 1.0 Acquisition Reconciliation
10. WP03--WP05 Semantic/Contract Reconciliation
11. WP06--WP10 Persistence Reconciliation
12. WP11 Composition Reconciliation
13. Worker Design Discovery
14. Persistence Use-Case Registration
15. Worker Execution Design
16. Acquisition-to-Persistence Handoff
17. Target/Fidelity Preservation
18. NewlyAccepted Handling
19. Idempotent Handling
20. Conflict Handling
21. Persistence Failure Handling
22. Acquisition Failure Protection
23. Configuration Validation
24. Worker Lifecycle Decision
25. Logging/Security
26. Exact Files Added/Modified
27. Package/Reference Delta
28. Test/Probe Delta
29. WP13/WP14 Protection
30. Whitespace/Diff Evidence
31. Restore/Build Evidence
32. Permanent Test Evidence
33. Canonical Verification
34. Architecture Validation
35. Offline End-to-End Validation Matrix
36. Regression Evidence
37. Mutation Accounting
38. Git/GitHub Protection
39. Planning Protection
40. Findings/Blockers
41. Final Repository/GitHub State
42. WP13 Handoff
43. Final Decision
44. Next Authorized Work Package

Add further sections if needed for material evidence. Do not omit a
required topic merely to match the numbering.

------------------------------------------------------------------------

## 29. Success Terminal

Emit the following terminal only if every mandatory gate passes:

``` text
RELEASE 1.1 WP12 COMPLETE

WORKER PERSISTENT MARKET-DATA EXECUTION:
Release 1.0 acquisition preserved: PASS
Application persistence boundary reused: PASS
Acquisition → persistence orchestration: PASS
Persistence use-case DI resolution: PASS
Exact target/fidelity preservation: PASS
NewlyAccepted handling: PASS
Idempotent handling: PASS
Conflict handling: PASS
Persistence failure distinction: PASS
Acquisition failure persistence calls: 0
Worker direct provider HTTP: NO
Worker direct SQLite/SQL: NO
Hidden database fallback: NO
External network required for acceptance: NO
Package/reference delta: 0/0
WP13 started: NO

NEXT AUTHORIZED WORK PACKAGE:
WP13 — Domain & Application Tests
GitHub issue #115
```

------------------------------------------------------------------------

## 30. Blocked Terminal

If any mandatory gate cannot pass, do not claim partial completion.

Emit:

``` text
RELEASE 1.1 WP12 BLOCKED
```

Then identify:

-   exact failed gate;
-   evidence;
-   repository/GitHub mutations already performed, if any;
-   whether issue #114 remains Open/In Progress;
-   smallest corrective authority required;
-   confirmation that WP13 was not started.

Do not begin WP13 while WP12 is blocked.

------------------------------------------------------------------------

## 31. Final Instruction

Implement only the smallest production orchestration necessary to make
the accepted Release 1.0 acquisition capability persist normalized
observations through the accepted Release 1.1 Application and SQLite
boundaries.

Preserve every predecessor semantic boundary.

Validate the real composition graph offline.

Do not use Git integration operations.

Do not begin WP13.

If all gates pass, close #114 as Done and stop.

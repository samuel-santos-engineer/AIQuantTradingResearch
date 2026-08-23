# Release 1.7 WP02 --- Durable Evidence Discovery Semantics --- Codex Authority

## 1. Mission

Execute Release 1.7 WP02 --- **Durable Evidence Discovery Semantics**
for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#198`

Authoritative predecessor baseline:

`95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Authoritative Release 1.7 milestone:

`#55 — Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`

WP01 is complete:

-   #197: CLOSED / Done;
-   #198--#209: OPEN / Backlog;
-   milestone #55: OPEN, 12 open / 1 closed;
-   canonical baseline: 250/250;
-   schema: v3;
-   Release 1.7 production implementation delta: 0.

WP02 freezes the **domain/application-independent semantic contract**
for durable Experiment evidence discovery.

WP02 must not implement Application contracts, orchestration, SQLite
access, DI, Worker routing, schema/index/migration changes, or permanent
tests reserved for later work packages.

------------------------------------------------------------------------

## 2. Authoritative Inputs

Read completely:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`
-   GitHub issue #198
-   relevant accepted Release 1.6 identity/evidence semantics and
    current architecture/design documentation identified by the
    manifest.

Treat the three Release 1.7 planning files as authoritative.

Do not redesign Release 1.7.

------------------------------------------------------------------------

## 3. Execution-Authority Classification

This WP02 prompt pair is execution authority.

Determine its repository lifecycle from the accepted Release 1.7
manifest and established WP conventions before mutation.

Do not:

-   stage it merely because it exists;
-   classify it as production/test content;
-   allow its presence to create a false starting-state blocker.

Preserve the WP01-established classification unless the authoritative
Release 1.7 file manifest explicitly governs WP prompt retention
differently.

Report the classification in the final execution report.

------------------------------------------------------------------------

## 4. Mandatory Starting Gate

Before any WP02 mutation verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no active merge/rebase/cherry-pick/revert;
-   no conflict markers;
-   WP01 #197: CLOSED / Done;
-   WP02 #198: OPEN / Backlog;
-   #199--#209: OPEN / Backlog;
-   milestone #55: OPEN, 12 open / 1 closed;
-   Project #2 planning state remains reconciled;
-   schema: v3;
-   canonical baseline: 250 tests;
-   Release 1.7 implementation: absent.

Expected untracked governed planning artifacts remain exactly the
accepted Release 1.7 planning files, plus execution-authority files
according to their established classification.

Unexpected repository state blocks execution.

------------------------------------------------------------------------

## 5. WP02 Semantic Objective

Freeze the semantics of a bounded read-only query over already-durable
Experiment Result evidence.

The accepted query dimensions are exactly:

1.  **Snapshot Identity**
2.  **Experiment Definition Identity**

The query asks:

> Return durable Experiment Result evidence whose stored provenance is
> bound to the exact requested Snapshot Identity and exact requested
> Experiment Definition Identity, subject to a caller-supplied positive
> bounded maximum.

WP02 must make this semantic contract explicit without choosing a
storage query implementation.

------------------------------------------------------------------------

## 6. Query Identity Semantics

Freeze:

-   Snapshot Identity matching is exact;
-   Experiment Definition Identity matching is exact;
-   both dimensions are mandatory;
-   both dimensions participate in the query predicate;
-   neither dimension may be broadened, omitted, wildcarded,
    prefix-matched, normalized into a different identity, or inferred
    from another field;
-   the query itself has no new durable identity;
-   existing Experiment Result identity remains governed by
    `aiq-experiment-identity-v1`;
-   discovery does not redefine Experiment Result identity.

No registry key, history key, search token, pagination token, cursor
identity, or discovery identity is introduced.

------------------------------------------------------------------------

## 7. Cardinality Semantics

Freeze a caller-supplied **positive bounded maximum**.

The semantic contract must require:

-   maximum \> 0;
-   maximum is mandatory;
-   maximum is finite and bounded;
-   returned cardinality is `0..maximum`;
-   no unbounded "all results" operation exists;
-   no implicit default maximum is invented unless already explicitly
    established by the accepted Release 1.7 planning authorities;
-   requesting more than the supported upper bound is invalid rather
    than silently converted into an unbounded query.

If the authoritative planning files specify an exact maximum ceiling,
preserve it.

If they intentionally defer the numeric ceiling, WP02 may freeze only
the requirement that a deterministic supported upper bound exists and
leave its concrete configuration/contract placement to the authorized
later WP. Do not invent a number.

------------------------------------------------------------------------

## 8. Ordering Semantics

Freeze deterministic ordering:

**Experiment Result Identity ascending**

Require:

-   ordering is total and deterministic for the returned durable
    evidence;
-   truncation to the bounded maximum occurs consistently with this
    ordering;
-   storage enumeration order must never become observable semantics;
-   insertion time, rowid, acceptance time, snapshot order, Feature Set
    identity, or aggregate values do not define discovery order.

WP02 must not add a database index merely to express this semantic rule.

Physical access is WP06/WP07 authority.

------------------------------------------------------------------------

## 9. Empty-Result Semantics

Freeze:

-   a valid query with zero matching durable Experiment Results is
    successful;
-   result is an empty collection;
-   empty discovery is **not** `NotFound`;
-   no provider/network fallback occurs;
-   no generation occurs;
-   no durable mutation occurs;
-   no synthetic evidence is created.

This differs intentionally from exact-identity retrieval of one required
Experiment Result, where a valid missing identity can map to `NotFound`.

Document that distinction clearly where the manifest authorizes semantic
documentation changes.

------------------------------------------------------------------------

## 10. Evidence Fidelity Semantics

Discovery returns existing immutable durable Experiment Result evidence.

Freeze that discovery must preserve the accepted Release 1.6 evidence
semantics, including:

-   Experiment Result identity;
-   Snapshot Identity and Snapshot Version binding;
-   Experiment Definition identity/version/provenance as represented by
    the accepted model;
-   Feature Set provenance;
-   canonical decimal representation;
-   signed-zero canonical behavior;
-   count;
-   aggregate presence/absence;
-   mean/minimum/maximum when present;
-   empty/non-empty fidelity;
-   immutable durable evidence semantics.

Discovery must not:

-   recompute Experiment Results;
-   regenerate Feature Sets;
-   normalize evidence into a different identity;
-   repair malformed evidence;
-   overwrite evidence;
-   deduplicate by deleting rows;
-   collapse semantically distinct results.

------------------------------------------------------------------------

## 11. Failure Semantics

Reuse the accepted Release 1.6 bounded failure vocabulary.

WP02 must preserve ownership boundaries and avoid inventing a
discovery-specific failure taxonomy.

Semantically distinguish:

### InvalidRequest

Application-owned invalid query intent, including invalid mandatory
query values or invalid bounded maximum once represented by the
Application contract.

WP02 freezes the semantic condition but does not implement Application
validation.

### InvalidEvidence

Malformed durable evidence reconstructed from storage.

### DependencyUnavailable

Deterministic persistence dependency unavailability.

### IntegrityConflict

Contradictory durable evidence where existing accepted lower-layer
semantics identify an integrity conflict.

### NotFound

Do **not** use `NotFound` merely because a valid discovery query matches
zero rows.

`NotFound` remains available to operations whose contract requires one
exact durable object, such as Release 1.6 exact retrieval.

Unknown defects must propagate.

Do not add broad exception normalization, retry, recovery, repair,
fallback, or provider substitution.

------------------------------------------------------------------------

## 12. Read-Only Semantics

Freeze discovery as strictly read-only.

A discovery request must not:

-   insert;
-   update;
-   delete;
-   accept;
-   generate;
-   retry;
-   repair;
-   migrate;
-   create an index;
-   create a registry/history record;
-   call a market-data provider;
-   call a network product dependency;
-   mutate Worker state.

No query side effect is part of the semantic contract.

------------------------------------------------------------------------

## 13. Explicit Exclusions

WP02 must preserve Release 1.7 exclusions:

-   broad search;
-   free-text search;
-   registry;
-   history;
-   mutation;
-   deletion;
-   evidence repair;
-   pagination/cursors unless separately planned;
-   scheduling;
-   background processing;
-   provider acquisition;
-   network fallback;
-   Experiment generation;
-   Feature generation;
-   schema v4;
-   new table;
-   new column;
-   new index;
-   migration;
-   Release 1.8 implementation;
-   Backtesting implementation.

Do not turn "discovery" into a generalized query/search subsystem.

------------------------------------------------------------------------

## 14. Ownership Boundary

WP02 defines semantics only.

Do not introduce:

-   Application interfaces;
-   Application request/result records;
-   use cases;
-   Infrastructure store methods;
-   SQL;
-   schema artifacts;
-   DI registrations;
-   Worker configuration;
-   Worker output;
-   Worker routing;
-   permanent tests.

Those belong to later WPs.

If the accepted file manifest authorizes a semantic documentation
artifact for WP02, changes must remain confined to that
manifest-authorized documentation surface.

Do not mutate production code to encode WP02 semantics.

------------------------------------------------------------------------

## 15. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only mutate paths explicitly authorized for WP02.

If the manifest specifies zero repository-content mutation for WP02,
perform WP02 as a validation/governance freeze only and make no content
change.

If it authorizes one or more semantic documentation/planning paths:

-   edit only those paths;
-   make the smallest changes needed to record the frozen semantics;
-   do not broaden into WP03+ concerns;
-   preserve existing document structure and terminology.

Any required path outside the manifest blocks WP02.

------------------------------------------------------------------------

## 16. WP03 Boundary Protection

WP03 owns detailed **Discovery Identity, Provenance & Fidelity**
formalization.

WP02 may freeze the high-level invariants needed to define discovery
behavior, but must not consume WP03 by designing new identity algorithms
or broad provenance models.

In particular:

-   reuse `aiq-experiment-identity-v1`;
-   no discovery identity;
-   no new hashing/canonicalization algorithm;
-   no new durable provenance record.

If detailed identity representation needs change, stop rather than
absorbing WP03.

------------------------------------------------------------------------

## 17. WP06 Boundary Protection

WP02 must not decide whether the schema-v3 physical query is efficient
enough.

Preserve the accepted WP06 stop gate:

-   schema v3 is the planned baseline;
-   WP06 proves the bounded access pattern;
-   if table/column/index/migration structural mutation is required,
    execution stops for separate authority.

Do not add or recommend an index as a WP02 implementation change.

------------------------------------------------------------------------

## 18. Process-Level Validation Boundary

Confirm the Release 1.7 planning authorities still contain the
pre-resolved future process fixture:

-   `TemporaryDatabase`;
-   deterministic `DatasetSnapshotCandidate`;
-   `SqliteDatasetSnapshotStore.Store(...)`;
-   production durable acceptance;
-   existing `--no-build` Worker runner;
-   friend-assembly boundary;
-   deterministic evidence;
-   full process/database cleanup.

WP02 does not execute or modify this fixture mechanism.

------------------------------------------------------------------------

## 19. Validation

After any manifest-authorized WP02 documentation mutation, run the
repository's canonical validation required for a governed WP.

Require:

-   Domain.Tests: 11/11;
-   Application.Tests: 111/111;
-   Infrastructure.Tests: 117/117;
-   Architecture.Tests: 13/13;
-   total: 250/250;
-   skipped: 0;
-   Release build warnings/errors: 0/0;
-   formatting: PASS;
-   Gitleaks: PASS;
-   `git diff --check`: PASS;
-   `git diff --cached --check`: PASS;
-   direct whitespace/final-newline checks: PASS;
-   provider/network product activity: 0;
-   real credentials: 0;
-   residue: 0.

If WP02's accepted manifest explicitly makes canonical verification
unnecessary for a zero-content read-only semantic gate, still run enough
verification to prove the frozen baseline and report why no test delta
occurred. Prefer canonical verification when practical.

Expected permanent test delta:

`0`

Expected Architecture.Tests delta:

`0`

Expected schema delta:

`0`

Expected package/project/reference delta:

`0`

------------------------------------------------------------------------

## 20. GitHub Lifecycle

Only after all WP02 acceptance gates pass:

1.  move #198 Backlog → In Progress if not already transitioned;
2.  post concise completion evidence to #198;
3.  close #198;
4.  set #198 Project Status to Done.

Final required GitHub state:

-   #197--#198: CLOSED / Done;
-   #199--#209: OPEN / Backlog;
-   milestone #55: OPEN, 11 open / 2 closed;
-   Project membership remains 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #199 automatically.

------------------------------------------------------------------------

## 21. Mutation Budget

### Production code

`0`

### Permanent tests

`0`

### Schema

`0`

### Packages/projects/references

`0`

### Git transport

`0`

### Repository documentation

Only manifest-authorized WP02 semantic documentation, if any.

### GitHub

Only #198 lifecycle mutations:

-   Backlog → In Progress;
-   completion evidence comment;
-   close;
-   Project Status → Done.

No milestone, dependency, label, assignee, Release, Area, Priority,
predecessor, or later-WP mutation.

------------------------------------------------------------------------

## 22. Stop Conditions

Stop with:

`RELEASE 1.7 WP02 BLOCKED`

if:

-   baseline or WP01 completion state differs;
-   Release 1.7 planning state drifts;
-   the three authoritative planning files conflict;
-   WP02 requires production code;
-   WP02 requires Application contracts or orchestration;
-   WP02 requires SQLite/SQL implementation;
-   WP02 requires schema/index/migration mutation;
-   a numeric maximum ceiling would need to be invented;
-   a new identity algorithm would need to be invented;
-   empty-result semantics cannot be distinguished from exact-retrieval
    `NotFound` without changing predecessor behavior;
-   a required repository path lies outside the WP02 manifest;
-   package/project/reference changes appear necessary;
-   canonical validation fails;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 23. Required Execution Report

Report:

1.  baseline and working-tree state;
2.  WP01/GitHub starting state;
3.  authoritative planning inputs read;
4.  manifest-authorized WP02 paths;
5.  exact query dimensions;
6.  bounded-cardinality semantics;
7.  deterministic ordering;
8.  empty-result semantics;
9.  identity/provenance/fidelity invariants;
10. failure semantics and `NotFound` distinction;
11. read-only guarantees;
12. exclusions;
13. WP03/WP06 boundary preservation;
14. process-validation prerequisite preservation;
15. changed paths, if any;
16. test/schema/package/project/reference deltas;
17. canonical validation;
18. offline/security/residue evidence;
19. GitHub lifecycle;
20. final milestone counts;
21. next authorized action.

------------------------------------------------------------------------

## 24. Completion Markers

On success end exactly:

`RELEASE 1.7 WP02 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP03 — Discovery Identity, Provenance & Fidelity — GitHub issue #199`

Do not execute WP03 automatically.

If blocked end exactly:

`RELEASE 1.7 WP02 BLOCKED`

and identify the smallest corrective authority required.

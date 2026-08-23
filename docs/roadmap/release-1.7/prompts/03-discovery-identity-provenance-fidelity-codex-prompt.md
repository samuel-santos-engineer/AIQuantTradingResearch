# Release 1.7 WP03 --- Discovery Identity, Provenance & Fidelity --- Codex Authority

## 1. Mission

Execute Release 1.7 WP03 --- **Discovery Identity, Provenance &
Fidelity** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#199`

Frozen predecessor baseline:

`95745fc2289ea855af39ba5e7bc0236a67f1c48b`

Authoritative Release 1.7 milestone:

`#55 — Phase 4 - Release 1.7: Durable Experiment Evidence Discovery`

Accepted predecessor work:

-   WP01 #197: CLOSED / Done;
-   WP02 #198: CLOSED / Done;
-   WP03 #199: OPEN / Backlog;
-   #200--#209: OPEN / Backlog;
-   milestone #55: OPEN, 11 open / 2 closed;
-   canonical baseline: 250/250;
-   schema: v3;
-   production/test/schema/package/project/reference delta through WP02:
    0.

WP03 formalizes the **identity, provenance, and fidelity invariants** of
discovery results.

It must not implement Application contracts, orchestration, persistence
queries, SQL, DI, Worker routing, schema changes, or permanent tests
reserved for later WPs.

------------------------------------------------------------------------

## 2. Authoritative Inputs

Read completely before mutation:

-   `docs/roadmap/release-1.7/RELEASE_1.7_DEFINITION.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.7/RELEASE_1.7_FILE_MANIFEST.md`
-   `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md`
-   GitHub issue #199
-   relevant accepted Release 1.6 Experiment
    identity/provenance/evidence documentation and implementation needed
    to verify existing semantics.

Treat WP02 semantics as frozen predecessor authority.

Do not reopen WP02 decisions.

------------------------------------------------------------------------

## 3. Execution-Authority Lifecycle

The WP03 prompt pair is execution authority.

Preserve the established Release 1.7 authority lifecycle:

-   do not stage merely because the files exist;
-   do not count authority files as production/test/schema changes;
-   do not allow their expected untracked presence to create a false
    blocker;
-   report their final classification.

WP01/WP02 prompt pairs may remain untracked under the same established
lifecycle.

Do not clean them up unless separately authorized.

------------------------------------------------------------------------

## 4. Mandatory Starting Gate

Before any WP03 repository or GitHub mutation verify:

-   branch: `main`;
-   HEAD: `95745fc2289ea855af39ba5e7bc0236a67f1c48b`;
-   `origin/main`: same SHA;
-   ahead/behind: `0/0`;
-   staged paths: 0;
-   tracked mutations: 0;
-   no active merge/rebase/cherry-pick/revert;
-   no conflict markers;
-   #197--#198: CLOSED / Done;
-   #199: OPEN / Backlog;
-   #200--#209: OPEN / Backlog;
-   milestone #55: OPEN, 11 open / 2 closed;
-   Project #2 state remains reconciled;
-   schema remains v3;
-   canonical baseline remains 250 tests;
-   Release 1.7 production implementation remains absent.

Expected untracked Release 1.7 planning/documentation/authority content
must reconcile with the accepted manifest and completed WP01/WP02 state.

Any unexplained tracked or staged state blocks execution.

------------------------------------------------------------------------

## 5. WP03 Objective

Formalize what makes each discovered durable Experiment Result the
**same accepted immutable evidence** established in Release 1.6.

WP03 must establish that discovery:

-   selects existing evidence;
-   does not create a new evidence identity;
-   does not reinterpret existing identity;
-   preserves all provenance bindings required to understand the
    evidence;
-   reconstructs evidence without semantic loss;
-   cannot substitute recomputed or regenerated evidence for stored
    durable evidence.

This is a semantic/design alignment WP, not an implementation WP.

------------------------------------------------------------------------

## 6. Experiment Result Identity

Freeze the following:

-   discovered evidence retains its existing Experiment Result Identity;
-   identity continues to use the accepted `aiq-experiment-identity-v1`
    semantics;
-   discovery does not introduce `aiq-discovery-*` or any equivalent
    identity scheme;
-   discovery does not hash the query to produce a result identity;
-   collection membership does not alter individual result identity;
-   ordering does not alter identity;
-   maximum cardinality does not alter identity;
-   repeated discovery of the same durable row yields the same
    Experiment Result Identity.

Do not modify the accepted identity algorithm.

Do not create a new canonicalization algorithm.

------------------------------------------------------------------------

## 7. Query Identity vs Evidence Identity

Make the distinction explicit:

### Query dimensions

The discovery predicate is exactly:

-   Snapshot Identity;
-   Experiment Definition Identity.

### Evidence identity

Each returned durable item retains its own existing Experiment Result
Identity.

The pair `(Snapshot Identity, Experiment Definition Identity)` is
**not** a new durable evidence identity.

It is also not:

-   a registry key;
-   a history key;
-   a collection identity;
-   a cursor;
-   a page identity;
-   a replacement for Experiment Result Identity.

No query-level identity is persisted.

------------------------------------------------------------------------

## 8. Snapshot Provenance

For every discovered Experiment Result, preserve and validate the
accepted Snapshot provenance.

At minimum:

-   exact Snapshot Identity;
-   exact Snapshot Version/binding where represented by the accepted
    durable model;
-   no substitution with a different snapshot having similar
    observations;
-   no latest-version resolution;
-   no prefix/fuzzy matching;
-   no recomputation of snapshot identity;
-   no provider reacquisition.

The discovery query's Snapshot Identity must correspond to the stored
evidence provenance rather than an inferred external relationship.

------------------------------------------------------------------------

## 9. Experiment Definition Provenance

For every discovered Experiment Result, preserve and validate the
accepted Experiment Definition provenance.

At minimum:

-   exact Experiment Definition Identity;
-   accepted definition/version binding where represented by the current
    model;
-   no substitution with a semantically similar definition;
-   no "latest definition" resolution;
-   no registry lookup introduced by WP03;
-   no definition regeneration.

The query predicate must match the durable provenance representation
exactly.

Do not redesign Experiment Definition identity.

------------------------------------------------------------------------

## 10. Feature Set Provenance

Preserve the accepted Feature Set lineage carried by the durable
Experiment Result.

Require:

-   Feature Set Identity remains unchanged;
-   any accepted Feature Set version/definition provenance remains
    unchanged;
-   discovery does not regenerate Feature Sets;
-   discovery does not infer a replacement Feature Set;
-   discovery does not discard Feature Set lineage merely because the
    query predicate does not include Feature Set Identity.

Feature Set provenance is returned evidence fidelity, not an additional
Release 1.7 query dimension.

------------------------------------------------------------------------

## 11. Canonical Numeric Fidelity

Preserve Release 1.6 canonical numeric evidence exactly.

Require fidelity for:

-   count;
-   mean;
-   minimum;
-   maximum;
-   canonical decimal representation;
-   canonical signed-zero behavior;
-   aggregate presence/absence.

Discovery must not:

-   convert durable decimals through floating-point representation;
-   round differently;
-   normalize values using a new algorithm;
-   recompute aggregates from source observations;
-   infer missing aggregates.

The reconstructed evidence must represent the same semantic values
accepted durably.

------------------------------------------------------------------------

## 12. Empty and Non-Empty Fidelity

Preserve the accepted aggregate invariants.

### Non-empty result

When count is positive, aggregate evidence must satisfy the accepted
Release 1.6 non-empty invariants.

### Empty result

When count is zero:

-   aggregate presence remains absent according to the accepted model;
-   mean is absent;
-   minimum is absent;
-   maximum is absent.

Do not convert absent aggregates into zero-valued aggregates.

Do not confuse an **empty Experiment Result** with an **empty discovery
collection**.

These are distinct:

-   empty Experiment Result = one durable Experiment Result whose
    experiment count is zero;
-   empty discovery collection = no durable Experiment Results matched
    the valid discovery query.

WP03 must make that distinction explicit.

------------------------------------------------------------------------

## 13. Immutable Evidence Semantics

Discovery must expose immutable accepted evidence.

Freeze:

-   no overwrite;
-   no repair;
-   no normalization write-back;
-   no acceptance mutation;
-   no delete;
-   no regeneration;
-   no provenance rewriting;
-   no identity rewriting.

If malformed durable evidence is encountered, later implementation must
use the accepted failure vocabulary rather than silently repairing it.

------------------------------------------------------------------------

## 14. Deterministic Collection Fidelity

WP02 already froze:

-   ordering by Experiment Result Identity ascending;
-   bounded maximum;
-   successful empty collection.

WP03 must preserve that collection semantics do not alter item fidelity.

Specifically:

-   truncation chooses which existing items are returned; it does not
    alter them;
-   ordering compares existing Experiment Result identities; it does not
    recalculate them;
-   collection materialization cannot change provenance or numeric
    representation;
-   repeated reads over unchanged durable state are semantically stable.

Do not introduce cursor/pagination semantics.

------------------------------------------------------------------------

## 15. Failure-Boundary Preservation

WP03 does not implement failure mapping.

It must preserve these semantic distinctions for later WPs:

-   malformed reconstructed durable evidence → `InvalidEvidence`;
-   deterministic storage dependency failure → `DependencyUnavailable`;
-   contradictory accepted evidence → `IntegrityConflict`;
-   invalid Application discovery intent → `InvalidRequest`;
-   valid zero-match discovery → successful empty collection, not
    `NotFound`;
-   unknown defects propagate.

Do not create new identity/provenance-specific failure values.

------------------------------------------------------------------------

## 16. Release 1.6 Compatibility

Inspect the accepted Release 1.6 durable Experiment evidence model and
prove WP03 does not require changing it.

Expected result:

-   `aiq-experiment-identity-v1` unchanged;
-   existing durable evidence identity unchanged;
-   existing provenance fields sufficient;
-   existing decimal representation sufficient;
-   existing empty/non-empty invariants sufficient;
-   schema v3 sufficient at the semantic level;
-   exact Release 1.6 retrieval remains unchanged;
-   acceptance semantics remain unchanged.

If WP03 discovers that existing durable evidence lacks a provenance
field required by the authoritative Release 1.7 definition, stop.

Do not silently add a field or schema column.

------------------------------------------------------------------------

## 17. Explicit Exclusions

WP03 does not authorize:

-   new identity algorithms;
-   new discovery identity;
-   new provenance table;
-   provenance registry/history;
-   broad search;
-   pagination/cursor identity;
-   Application request/result contracts;
-   Application use cases;
-   Infrastructure interfaces or implementations;
-   SQL;
-   schema/index/migration;
-   DI;
-   Worker configuration/routing/output;
-   provider/network calls;
-   generation;
-   mutation;
-   scheduling;
-   permanent tests;
-   Release 1.8 implementation;
-   Backtesting implementation.

------------------------------------------------------------------------

## 18. WP04 Boundary Protection

WP04 owns **Application Discovery Contracts**.

WP03 must not introduce:

-   discovery request records;
-   result collection contracts;
-   Application interfaces;
-   validation implementation;
-   use-case signatures;
-   dependency interfaces.

WP03 may state invariants that WP04 must preserve, but must not design
or implement WP04's contract surface.

------------------------------------------------------------------------

## 19. WP06/WP07 Boundary Protection

WP03 must remain storage-agnostic.

Do not decide:

-   SQL shape;
-   query plan;
-   index requirements;
-   scan strategy;
-   SQLite API;
-   persistence interface shape;
-   schema changes.

WP06 remains the structural access-pattern gate.

WP07 remains the SQLite implementation authority.

Schema remains v3.

------------------------------------------------------------------------

## 20. Manifest-Bounded Mutation

Read `RELEASE_1.7_FILE_MANIFEST.md` before editing.

Only mutate WP03-authorized paths.

Prefer the smallest documentation alignment necessary to formalize
identity/provenance/fidelity.

If WP03 is authorized to extend:

`docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE_DISCOVERY.md`

do so without duplicating unrelated architecture content.

If the manifest authorizes another dedicated semantic/design document,
follow the manifest exactly.

Do not invent a new governed path outside the manifest.

If required semantics cannot be captured within authorized paths, stop.

------------------------------------------------------------------------

## 21. Expected Deltas

Expected production-code delta:

`0`

Expected permanent-test delta:

`0`

Expected Architecture.Tests delta:

`0`

Expected schema delta:

`0`

Expected package/project/reference delta:

`0`

Expected provider/network activity:

`0`

Only manifest-authorized documentation changes are expected.

------------------------------------------------------------------------

## 22. Validation

Run canonical verification after WP03 changes.

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
-   conflict markers: 0;
-   provider/network product activity: 0;
-   real credentials: 0;
-   temporary database/process/probe residue: 0.

Also validate changed documentation links if applicable.

------------------------------------------------------------------------

## 23. GitHub Lifecycle

Only after every WP03 gate passes:

1.  move #199 Backlog → In Progress if necessary;
2.  post concise completion evidence to #199;
3.  close #199;
4.  set #199 Project Status to Done.

Final required state:

-   #197--#199: CLOSED / Done;
-   #200--#209: OPEN / Backlog;
-   milestone #55: OPEN, 10 open / 3 closed;
-   Project membership: 13/13;
-   dependencies unchanged;
-   Priority/Release/Area unchanged.

Do not transition #200 automatically.

------------------------------------------------------------------------

## 24. Mutation Budget

### Repository production/test/schema

`0`

### Repository documentation

Only WP03 manifest-authorized paths.

### Git transport

`0`

### GitHub

Only #199 lifecycle:

-   Backlog → In Progress;
-   completion evidence;
-   close;
-   Project Status → Done.

Do not mutate milestone metadata, dependencies, labels, assignees,
predecessor releases, or later WPs.

------------------------------------------------------------------------

## 25. Stop Conditions

Stop with:

`RELEASE 1.7 WP03 BLOCKED`

if:

-   baseline/start state differs materially;
-   WP02 semantic document is missing or conflicts with authoritative
    planning;
-   existing Release 1.6 identity semantics would need modification;
-   existing durable evidence lacks required provenance;
-   a new identity/canonicalization algorithm appears necessary;
-   schema/table/column/index/migration mutation appears necessary;
-   Application contract implementation appears necessary;
-   Infrastructure/SQL/Worker work appears necessary;
-   required documentation path is outside the manifest;
-   canonical validation fails;
-   unexpected package/project/reference drift exists;
-   provider/network execution or real credentials would be required.

Report the exact blocker and smallest corrective authority required.

------------------------------------------------------------------------

## 26. Required Execution Report

Report:

1.  baseline and starting state;
2.  authoritative inputs read;
3.  WP03 manifest-authorized paths;
4.  Experiment Result identity preservation;
5.  query identity vs evidence identity distinction;
6.  Snapshot provenance;
7.  Experiment Definition provenance;
8.  Feature Set provenance;
9.  numeric/canonical decimal fidelity;
10. empty vs non-empty Experiment Result fidelity;
11. empty Experiment Result vs empty discovery collection distinction;
12. immutable evidence semantics;
13. deterministic collection fidelity;
14. failure-boundary preservation;
15. Release 1.6 compatibility;
16. WP04/WP06/WP07 boundary preservation;
17. changed paths;
18. production/test/schema/package/project/reference deltas;
19. canonical validation;
20. offline/security/residue validation;
21. GitHub lifecycle;
22. final milestone counts;
23. next authorized action.

------------------------------------------------------------------------

## 27. Completion Markers

On success end exactly:

`RELEASE 1.7 WP03 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP04 — Application Discovery Contracts — GitHub issue #200`

Do not execute WP04 automatically.

If blocked end exactly:

`RELEASE 1.7 WP03 BLOCKED`

and identify the smallest corrective authority required.

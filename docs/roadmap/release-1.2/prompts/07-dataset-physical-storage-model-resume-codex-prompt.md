# Release 1.2 WP07 --- Dataset Physical Storage Model --- Corrective Resume Authority

## 1. Authority

You are resuming **Release 1.2 WP07 --- Dataset Physical Storage Model**
for:

-   Repository: `samuel-santos-engineer/AIQuantTradingResearch`
-   Release: `Phase 3 - Release 1.2: Research Dataset Foundation`
-   GitHub issue: `#127`
-   Next work package: WP08 --- Dataset Snapshot Persistence, issue
    `#128`

This is a **narrow corrective/resumption authority** for the previously
blocked WP07 execution.

The original authority remains controlling:

-   `07-dataset-physical-storage-model-codex-prompt.md`

The accepted blocked execution report remains historical evidence:

-   `RELEASE 1.2 WP07 BLOCKED`

This resume authority changes **exactly one scope rule** from the
original WP07 authority: it permits test-count-neutral alignment of the
existing permanent SQLite bootstrap tests where they encode Release 1.1
schema version 1 as permanently current and schema version 2 as
permanently unsupported.

All other original WP07 requirements, prohibitions, semantic
authorities, validation gates, lifecycle rules, Git protections,
WP08/WP09 protections, and terminal-report requirements remain in force.

Read this file and the complete original WP07 authority before any
mutation.

## 2. Reason for corrective authority

The first WP07 execution correctly stopped before mutation because it
discovered a direct authority conflict:

-   Release 1.1 SQLite currently uses `PRAGMA user_version = 1`.
-   WP07 must evolve the repository-owned SQLite schema to the next
    version in order to add Release 1.2 dataset physical structures.
-   Therefore the target schema version is `2`.
-   An existing permanent Infrastructure test asserts that the current
    schema is exactly version `1`.
-   Another existing permanent Infrastructure test uses version `2` as
    the unsupported-future-version fixture.
-   The original WP07 authority prohibited permanent-test changes.

Those assertions were correct for Release 1.1 but necessarily become
stale when the authoritative repository schema advances to version 2.

This authority resolves only that contradiction.

## 3. Explicit corrective authorization

WP07 is now authorized to modify the **existing permanent Infrastructure
SQLite bootstrap tests only where required to align their schema-version
expectations with the accepted v1 → v2 evolution**.

The permitted alignment is:

1.  Change the existing current-schema assertion from:

    -   `PRAGMA user_version = 1` to:
    -   `PRAGMA user_version = 2`

2.  Rename the affected existing test if its name explicitly states
    `VersionOne`, `Version1`, or equivalent current-version wording, so
    the name accurately reflects schema version 2.

3.  Change the existing unsupported-future-version fixture from:

    -   version `2` to:
    -   version `3`

4.  Update only directly coupled expected values/messages in those same
    existing tests if the production bootstrap reports the current or
    unsupported schema version and the assertion must change
    mechanically from 1/2 to 2/3.

No other permanent-test semantic change is authorized by this
correction.

## 4. Test-count-neutral requirement

This correction MUST be test-count neutral.

Required:

-   tests added: `0`
-   tests deleted: `0`
-   existing test cases converted into new coverage areas: `0`
-   Infrastructure permanent test count remains `79`
-   total permanent test count remains `145`

This is **maintenance alignment of existing bootstrap assertions**, not
WP14 dataset-test implementation.

Do not add permanent tests for:

-   dataset schema
-   snapshot mappings
-   dataset persistence
-   catalog persistence
-   v1 → v2 migration
-   empty dataset snapshots
-   identity round trips
-   dataset fidelity
-   WP08/WP09 behavior

Use temporary focused offline probes, removed before final accounting,
for WP07-specific physical-model evidence not already covered by
existing permanent tests.

## 5. Existing Release 1.1 behavioral protection

Changing the schema-version expectations does not authorize weakening
Release 1.1 persistence regression coverage.

The existing Infrastructure test suite must continue to prove all
pre-existing Release 1.1 behavior it proved before WP07, including as
applicable:

-   `historical_observations` existence and accepted structure
-   strict / without-rowid characteristics
-   exact target behavior
-   composite observation identity
-   timestamp/offset fidelity
-   decimal fidelity
-   connection/bootstrap behavior
-   repeated bootstrap behavior
-   unsupported future-version rejection
-   incompatible-schema rejection
-   observation persistence/retrieval
-   idempotency/conflict behavior
-   failure mapping
-   DI/configuration

Do not delete, skip, relax, broadly rewrite, or disable Release 1.1
assertions merely to make WP07 pass.

After schema evolution, Release 1.1 behavior must remain valid under
schema version 2.

## 6. Starting-state reconciliation

Resume from current repository truth rather than assuming the exact
state captured by the blocked report.

Before mutation:

1.  Confirm repository identity.
2.  Confirm branch `main`.
3.  Fetch/reconcile `origin/main`.
4.  Report HEAD, origin SHA, ahead/behind, staged, tracked, and
    untracked state.
5.  Classify all working-tree paths.
6.  Confirm the cumulative accepted Release 1.2 WP01--WP06 artifacts
    remain present.
7.  Confirm the original WP07 authority and this resume authority are
    present.
8.  Confirm:
    -   #125 Closed / Done
    -   #126 Closed / Done
    -   #127 Open / Backlog
    -   #128 Open / Backlog
    -   #127 dependency exactly #125 and #126
    -   milestone #53 Open
    -   no Release 1.3 implementation active
9.  Confirm no partial WP07 production mutation exists from the blocked
    run.
10. Re-run the unchanged baseline:
    -   restore
    -   format verification
    -   build
    -   all permanent tests
    -   architecture tests
    -   `eng/verify.ps1`
    -   `git diff --check`
    -   `git diff --cached --check`

If repository truth has materially changed or a new unrelated blocker
exists, stop and report it.

## 7. Lifecycle resumption

Because the prior run performed zero GitHub lifecycle mutation, issue
#127 should still be Open / Backlog.

Only after the resumed starting-state and baseline gates pass:

-   move #127 Backlog → In Progress;
-   execute WP07 under the original authority plus this narrow
    correction.

Do not modify #128.

## 8. Schema-version decision now authorized

The accepted target is:

-   Release 1.1 schema: version `1`
-   Release 1.2 WP07 schema: version `2`
-   first unsupported future-version fixture for the existing bootstrap
    test: version `3`

This does not predetermine the physical table design. GPT-5.6 Sol must
still reason through and implement the smallest correct WP07 physical
model under the original authority.

Do not create an independent dataset schema-version mechanism if
repository truth continues to use `PRAGMA user_version` as the
authoritative SQLite schema marker.

## 9. Required v1 → v2 evolution behavior

The completed WP07 implementation must still satisfy the original
authority's schema-evolution requirements.

At minimum:

-   a new empty database reaches schema version 2;
-   the Release 1.1 `historical_observations` schema remains present;
-   an existing valid version-1 database upgrades to version 2;
-   accepted Release 1.1 observation rows survive the upgrade;
-   Release 1.1 observation retrieval remains semantically equivalent
    after upgrade;
-   version-2 bootstrap is idempotent;
-   version 3 is rejected as unsupported by the current implementation;
-   incompatible expected schema is rejected rather than destructively
    reset;
-   no downgrade logic is added;
-   no generalized migration framework is introduced;
-   schema transition is atomic according to the existing bootstrap
    strategy;
-   no Release 1.1 table/data is dropped or recreated destructively.

## 10. Physical-model authority remains unchanged

After applying this correction, continue the original WP07 design work
completely.

The original authority still governs:

-   snapshot/catalog physical representation
-   snapshot-observation physical representation
-   four identity representations
-   Dataset Version / Snapshot Identity consistency
-   exact target representation
-   requested boundaries
-   coverage
-   provenance
-   lineage
-   successful empty snapshots
-   timestamp/offset fidelity
-   decimal fidelity
-   deterministic observation ordering
-   keys
-   constraints
-   foreign keys where appropriate
-   indexes
-   immutability compatibility
-   Infrastructure-owned physical records/mappers
-   exact Snapshot Identity lookup support
-   schema bootstrap/evolution

Do not treat this resume authority as permission to simplify or bypass
those requirements.

## 11. WP08 protection remains unchanged

WP08 --- issue #128 --- owns Dataset Snapshot Persistence.

WP07 still MUST NOT implement production:

-   `IDatasetSnapshotStore` concrete behavior
-   snapshot persistence orchestration
-   snapshot insertion workflow
-   runtime persistence transactions
-   idempotent persistence classification
-   persistence conflict classification
-   durable snapshot reconstruction
-   persistence result mapping

Physical constraints and disposable direct-SQL probes remain allowed
only as defined by the original authority.

WP08 must remain Open / Backlog.

## 12. WP09 protection remains unchanged

WP09 owns Dataset Catalog Persistence & Lookup.

WP07 still MUST NOT implement production:

-   `IDatasetCatalog` concrete persistence
-   catalog registration
-   exact lookup runtime queries
-   found/not-found behavior
-   catalog failure mapping
-   generalized dataset discovery/search

WP07 may create only the physical structures/indexes necessary to
support the already accepted exact lookup semantics.

## 13. WP10--WP12 and Release 1.3 protection

No change from the original authority.

Do not implement:

-   materialization/persistence integration
-   final dataset validation/failure mapping
-   DI registration
-   configuration binding
-   Worker dataset execution
-   scheduling
-   refresh
-   streaming
-   pipeline orchestration
-   Release 1.3 behavior

## 14. Architecture and production scope

Expected final production deltas remain:

-   Domain: `0`
-   Application: `0`
-   Infrastructure: authorized WP07 changes
-   Worker: `0`
-   packages: `0`
-   project references: `0`

Additional authorized test delta:

-   existing Infrastructure test files may be modified only for the
    schema-version alignment in Section 3
-   permanent test count delta: `0`

SQLite/SQL/filesystem concepts remain confined to Infrastructure.

## 15. Temporary proof strategy

Because WP14 owns comprehensive permanent Infrastructure/Dataset tests,
use temporary focused offline probes for new WP07 evidence where
required.

Temporary probes may validate:

-   v1 → v2 upgrade
-   preservation of Release 1.1 observations
-   v2 idempotent bootstrap
-   v3 rejection
-   dataset physical schema
-   mapper round trips
-   empty snapshot representation
-   identity fidelity
-   target fidelity
-   boundary/coverage fidelity
-   provenance/lineage reconstruction
-   timestamp/offset fidelity
-   decimal fidelity
-   observation ordering constraints
-   immutable/conflicting physical evidence behavior

All temporary test files, databases, WAL/SHM/journal files, directories,
and other probe residue must be removed before final acceptance.

## 16. Validation after implementation

Run all original WP07 final gates plus explicit test-count verification.

Required:

-   restore: PASS
-   format verification: PASS
-   build: PASS
-   build warnings/errors: `0/0`
-   Domain.Tests: `11/11`
-   Application.Tests: `42/42`
-   Infrastructure.Tests: `79/79`
-   Architecture.Tests: `13/13`
-   total permanent tests: `145/145`
-   skipped: `0`
-   `eng/verify.ps1`: PASS
-   canonical secret scan: PASS
-   `git diff --check`: PASS
-   `git diff --cached --check`: PASS
-   direct whitespace checks for authorized untracked files: PASS
-   package/security verification: PASS
-   provider/network calls: `0`
-   temporary SQLite residue: `0`
-   temporary probe residue: `0`
-   Domain/Application SQLite leakage: `0`
-   Release 1.1 persistence regression: PASS

Explicitly verify the existing test modifications are limited to the
authorized schema-version alignment.

## 17. Mutation accounting

The final report must separately account for:

### Production

-   files added
-   files modified
-   files deleted
-   Domain/Application/Infrastructure/Worker delta
-   packages/references

### Existing permanent-test alignment

-   exact existing test file(s) modified
-   exact test names/assertions changed
-   old expected schema version
-   new expected schema version
-   old unsupported fixture
-   new unsupported fixture
-   tests added: 0
-   tests deleted: 0
-   Infrastructure count: 79
-   total count: 145

### Temporary evidence

-   probe files created/removed
-   temporary databases created/removed
-   final residue

No authorized change may be hidden inside aggregate counts.

## 18. Git/GitHub protection

The original protection remains.

Do NOT:

-   stage
-   commit
-   push
-   create/switch an integration branch
-   create a PR
-   merge
-   tag
-   release
-   rewrite history

Only issue #127 lifecycle/evidence may be changed.

After every acceptance gate passes:

1.  post concise completion evidence to #127;
2.  close #127;
3.  set Project status to Done;
4.  leave #128 Open / Backlog.

## 19. Required final execution report

Use the original WP07 report structure, but add a dedicated section:

**Corrective Test Alignment**

That section must state:

-   why the original run blocked;
-   that this resume authority permitted the narrow correction;
-   exact existing test file(s) changed;
-   exact current-version assertion/name change;
-   exact unsupported-version fixture change;
-   test count before/after;
-   confirmation that no WP14 dataset coverage was added;
-   confirmation that Release 1.1 behavior remains protected.

The report must also explicitly distinguish:

-   original blocked execution: historical / no mutation;
-   resumed execution: authoritative completed attempt.

## 20. Acceptance criteria

In addition to every original WP07 acceptance criterion, completion now
requires:

-   the prior authority conflict is resolved only through the permitted
    test alignment;
-   schema version 2 is the current accepted SQLite schema;
-   version 3 is the unsupported-future-version test fixture;
-   no existing permanent test is deleted;
-   no permanent test is added;
-   Infrastructure.Tests remains 79;
-   total permanent tests remains 145;
-   Release 1.1 behavioral coverage is not weakened;
-   WP14 coverage is not pulled forward;
-   all original WP07 physical-model and schema-evolution gates pass;
-   #127 ends Closed / Done;
-   #128 remains Open / Backlog.

## 21. WP08 handoff

The original WP07 handoff requirement remains fully applicable.

The completed report must give WP08 exact repository truth for:

-   schema version 2
-   exact table names
-   columns/types/nullability
-   primary/foreign/unique/check constraints
-   indexes
-   identity representation
-   Dataset Version representation
-   target collation
-   requested boundaries
-   coverage
-   provenance/lineage representation
-   timestamp/offset representation
-   decimal representation
-   observation ordering/membership representation
-   empty snapshot representation
-   Infrastructure records/mappers
-   bootstrap guarantees
-   v1 → v2 upgrade guarantees

WP08 must consume this model rather than redesign it.

## 22. Terminal marker

On successful resumed completion, use the original WP07 success marker:

RELEASE 1.2 WP07 COMPLETE

DATASET PHYSICAL STORAGE MODEL: WP02 dataset semantics preserved: PASS
WP03 identity/version/provenance semantics preserved: PASS WP04
Application contracts preserved: PASS WP05 materialization candidate
representable: PASS WP06 catalog metadata representable: PASS Release
1.1 historical_observations preserved: PASS SQLite storage engine
preserved: PASS Schema version: 2 Schema evolution: PASS Clean
bootstrap: PASS Release 1.1 → Release 1.2 upgrade: PASS Snapshot/catalog
physical model: PASS Snapshot observation physical model: PASS Four
identity representations: PASS Dataset Version / Snapshot Identity
consistency: PASS Exact target representation: PASS Requested boundary
representation: PASS Coverage representation: PASS Provenance
representation: PASS Lineage representation: PASS Successful empty
snapshot representation: PASS Timestamp/offset fidelity: PASS Decimal
fidelity: PASS Deterministic observation-order representation: PASS
Immutable-history compatibility: PASS Exact Snapshot Identity lookup
support: PASS Existing bootstrap-test alignment: PASS Tests
added/deleted: 0/0 Infrastructure.Tests: 79/79 Permanent tests: 145/145
Generalized migration framework introduced: NO Snapshot persistence
implemented: NO Catalog persistence/lookup implemented: NO Domain
SQLite/SQL leakage: 0 Application SQLite/SQL leakage: 0 Domain delta: 0
Application delta: 0 Worker delta: 0 Package/reference delta: 0/0
Permanent test count delta: 0 Temporary SQLite residue: 0 WP08 started:
NO Release 1.3 implementation started: NO Issue #127: CLOSED / DONE

NEXT AUTHORIZED WORK PACKAGE: WP08 --- Dataset Snapshot Persistence
GitHub issue #128

If a new blocker is discovered, do not weaken this or the original
authority. End with:

RELEASE 1.2 WP07 BLOCKED

and state the smallest new corrective authority required.

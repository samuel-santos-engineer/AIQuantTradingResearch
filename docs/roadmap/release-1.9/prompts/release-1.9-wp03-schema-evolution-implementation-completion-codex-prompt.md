# Release 1.9 — WP03 Schema Evolution Implementation/Completion — Codex Authority

## Authority

This document grants fresh execution authority to complete:

**Release 1.9 WP03 — canonical GitHub issue #228**

This authority begins from a fully defined normative schema-evolution contract and an already-validated WP03 Worker/pipeline implementation.

It explicitly permits only the minimal schema/persistence changes required to support truthful persisted Replay provenance and then revalidate the full WP03 production path.

This authority is for WP03 only.

It does not authorize WP04 or later work.

---

# Accepted Current State

Proven current state includes:

- WP01 #226: Closed / Done;
- WP02 #227: Closed / Done;
- WP03 #228: Open / Backlog;
- WP04 #229 and later WPs remain unstarted;
- canonical `ExecuteCanonical` five-stage executor implemented;
- historical and explicit-observation materialization converge on it;
- historical acquisition remains through `IHistoricalObservationStore`;
- Replay uses the WP03 additive observation-input seam;
- Worker Historical/Replay mode configuration implemented;
- Replay Dataset-boundary validation implemented;
- WP02 Replay dispatch/composition implemented;
- source authority semantics established:
  - `0 = AcceptedRelease11HistoricalObservations`
  - `1 = Release19SimulatedLiveReplay`;
- current SQLite schema = v3;
- current snapshot and experiment-result persistence constrain `source_authority = 0`;
- Release 1.9 requires persisted Replay snapshot/catalog evidence;
- no alternate governed Replay persistence path exists;
- therefore schema evolution to v4 is required;
- Application focused suite: 122/122 passed;
- WP02 replay suite: 142/142 passed;
- full regression baseline: 288/288 passed;
- build: 0 errors / 0 warnings;
- no WP03 GitHub lifecycle mutation has yet occurred.

Preserve all validated WP03 code unless a concrete schema-integration defect requires a minimal correction.

---

# Fixed Normative Schema Contract

## Versions

Current:

`SQLite schema v3`

Target:

`SQLite schema v4`

Version rules:

- v3 upgrades once to v4;
- v4 validates normally without mutation;
- older versions follow existing sequential bootstrap path through v3, then v4;
- unexpected/newer versions fail using existing schema-validation convention.

## Affected schema objects

Only these persisted columns require authority-domain expansion:

- `dataset_snapshots.source_authority`
- `experiment_results.source_authority`

No other indexes, triggers, views, foreign keys, or uniqueness constraints require semantic change.

## Source-authority domain

Persisted values:

- `0 = AcceptedRelease11HistoricalObservations`
- `1 = Release19SimulatedLiveReplay`

Target constraint for both columns:

`source_authority INTEGER NOT NULL CHECK (source_authority IN (0, 1))`

No other persisted authority values are permitted.

## Migration contract

The v3→v4 migration must:

1. begin the repository's governed SQLite transaction with foreign keys enabled;
2. rebuild each affected table using the v4 authority constraint;
3. copy all existing rows without semantic transformation;
4. verify row counts, keys, relationships, and preserved values;
5. recreate required indexes/triggers/views if the physical rebuild requires it;
6. validate complete schema and foreign-key integrity;
7. set `PRAGMA user_version = 4` only after successful validation;
8. commit atomically.

Any failure must roll back and leave no partially applied v4 schema.

## Existing-data preservation

All v3 data must remain unchanged:

- source authority `0` remains `0`;
- no historical row becomes Replay;
- row counts preserved;
- primary keys preserved;
- timestamps/content preserved;
- relationships preserved;
- no Replay history invented.

## Query/persistence compatibility

- Historical-only queries may continue selecting authority `0`.
- General persisted-evidence/catalog paths required by Release 1.9 must accept and distinguish `0` and `1`.
- Historical insert with `0` succeeds.
- Replay insert with `1` succeeds.
- Invalid authority values fail.
- No path may coerce Replay authority `1` to Historical authority `0`.
- Provenance remains immutable unless an existing governed update rule explicitly permits changes.

## Downgrade

Repository uses forward-only schema advancement.

No automatic v4→v3 downgrade is authorized.

Failed in-progress migration must roll back transactionally.

Any downgrade that would discard authority-1 Replay evidence requires separate governance.

Do not implement downgrade logic here.

---

# Fixed WP03 Runtime Contracts

The following are also fixed and must not be redesigned.

## Worker mode

- `Worker:Mode`
- accepted values: `Historical`, `Replay`
- case-insensitive ordinal match
- missing mode defaults to Historical
- unknown mode fails before execution

## Replay settings

- ReplayIdentity
- Target
- StartingTick
- RequestedObservationCount

## Dataset Replay boundary

Replay requires existing:

- `Dataset:Target`
- `Dataset:From`
- `Dataset:To`

Rules:

- Dataset Target and Worker Replay Target both required;
- exact ordinal case-sensitive equality;
- no normalization;
- From inclusive;
- To exclusive;
- From < To;
- every selected replay observation must lie in `[From,To)`.

## Replay provenance

Replay persists authority `1`.

Historical persists authority `0`.

Replay must never be relabeled Historical.

---

# Objective

Implement schema v4 exactly as defined, make the minimal persistence/query adjustments required for authority `1`, then prove the real WP03 Replay production flow can:

1. run through the existing Worker Replay path;
2. use WP02 Replay acquisition;
3. pass through the WP03 seam;
4. execute the canonical five-stage pipeline;
5. persist required snapshot/result/catalog evidence with authority `1`;
6. preserve Historical behavior with authority `0`;
7. complete finitely;
8. pass all focused and full regression gates;
9. finalize #228 only after technical acceptance.

---

# Permitted Scope

This authority may modify only the minimum files required for:

- schema v4 definition;
- v3→v4 migration/bootstrap logic;
- schema version validation;
- table rebuild logic;
- affected persistence insert/read/query behavior;
- Replay-aware catalog/evidence reads where #228 requires it;
- source-authority handling required for persisted Replay;
- migration tests;
- schema tests;
- persistence tests;
- WP03 Worker Replay production-flow tests;
- minimal integration corrections directly caused by schema v4;
- #228 lifecycle finalization after all gates pass.

---

# Explicitly Forbidden

Do not:

- redesign persistence broadly;
- change unrelated tables/columns;
- expand authority domain beyond 0 and 1;
- relabel Replay as Historical;
- change Worker configuration contract;
- change Dataset-boundary contract;
- redesign WP02 Replay contracts;
- redesign `ExecuteCanonical`;
- create a parallel Replay pipeline;
- alter package pins;
- alter Python;
- alter Streamlit;
- alter JSON-over-stdio protocol;
- implement WP04+;
- alter Release 1.9 planning/dependencies;
- modify #225;
- modify protected milestones #59/#60/#50/#51/#61;
- close #228 before all technical acceptance gates pass.

---

# Phase 0 — Fresh Pre-Mutation Proof

Before mutation:

1. Read #228 completely.
2. Read WP03 manifest/definition.
3. Read schema v1/v2/v3 migration/bootstrap logic.
4. Read current `PRAGMA user_version` handling.
5. Read exact DDL for:
   - dataset_snapshots
   - experiment_results
6. Read indexes/triggers/views tied to those tables.
7. Read persistence repositories and queries involving source_authority.
8. Read current WP03 Worker Replay production flow.
9. Read existing schema/migration/persistence tests.
10. Record Git state and existing WP03 diff.
11. Prove no unrelated/uncertain changes exist.

Stop if repository migration conventions materially conflict with the fixed v4 contract.

---

# Phase 1 — Implement Schema v4 Definition

Implement the target schema exactly.

For:

`dataset_snapshots.source_authority`

and:

`experiment_results.source_authority`

target constraint must be semantically:

`INTEGER NOT NULL CHECK (source_authority IN (0, 1))`

Do not broaden valid values.

Do not alter unrelated columns or constraints.

Update target schema version to v4 using the repository's existing versioning mechanism.

---

# Phase 2 — Implement v3→v4 Migration

Follow existing SQLite migration conventions.

Required migration semantics:

1. transactional;
2. foreign-key handling consistent with repository conventions;
3. rebuild only affected tables;
4. copy all rows unchanged;
5. preserve primary keys;
6. preserve all non-authority values exactly;
7. preserve relationships;
8. recreate physical indexes/triggers/views if required by table replacement;
9. run schema/foreign-key integrity checks;
10. set user_version=4 only after validation;
11. commit only after full success.

If any step fails:

- rollback;
- leave database at valid v3 state;
- do not expose partial v4.

Do not create destructive fallback behavior.

---

# Phase 3 — Version-Gating Behavior

Implement/verify:

## v3
Upgrade exactly once to v4.

## v4
Validate and continue without mutation.

## older versions
Follow existing sequential migrations to v3 then v4.

## newer/unexpected
Use existing governed failure behavior.

Do not invent skip-level migration behavior inconsistent with repository conventions.

---

# Phase 4 — Data-Preservation Validation

Add tests proving v3→v4 preserves:

- snapshot row counts;
- experiment-result row counts;
- primary keys;
- timestamps;
- payload/content;
- foreign-key relationships;
- authority `0` unchanged.

No Replay rows may be synthesized.

Where practical compare complete logical row sets pre/post migration.

---

# Phase 5 — Constraint-Domain Tests

Add tests proving post-v4 behavior.

For dataset snapshots:

- authority 0 insert succeeds;
- authority 1 insert succeeds;
- invalid negative/other authority fails;
- invalid >1 authority fails.

For experiment results:

- authority 0 insert succeeds;
- authority 1 insert succeeds;
- invalid values fail.

Verify exact v4 user_version.

---

# Phase 6 — Migration Atomicity Tests

Add tests proving:

- failed migration rolls back;
- user_version does not advance on failure;
- original v3 data/schema remains usable after failed attempt;
- successful migration is atomic.

Use repository-supported failure injection/testing patterns.

Do not build a new migration framework.

---

# Phase 7 — Query/Persistence Compatibility

Inspect all relevant reads/writes.

Implement only the minimum required changes so that:

## Historical

- historical persistence writes authority 0;
- historical-only reads continue to mean Historical where intended.

## Replay

- Replay persistence writes authority 1;
- Replay evidence remains distinguishable;
- general persisted-evidence/catalog paths required by #228 can surface authority 1;
- no code coerces 1→0.

Do not change business semantics of unrelated historical queries.

If a query was intentionally historical-only, leave it authority-0-specific.

If a query is supposed to support general persisted evidence for WP03, make it Replay-aware.

Document each changed query's semantic category.

---

# Phase 8 — Replay Persistence Integration

Re-run the real WP03 Replay flow against schema v4.

Prove:

`Worker Replay`
→ validated Worker config
→ validated Dataset config
→ WP02 Replay source
→ replay observations
→ WP03 seam
→ `ExecuteCanonical`
→ stages 1–5
→ snapshot/result/catalog persistence
→ authority `1`
→ finite completion

Required proofs:

- persisted snapshot authority = 1;
- persisted experiment-result authority = 1;
- catalog/evidence retains Replay distinction;
- no replay row written as authority 0;
- no replay observation routed through `IHistoricalObservationStore`.

---

# Phase 9 — Historical Compatibility Integration

Prove Historical mode remains:

`Worker Historical/default`
→ `IHistoricalObservationStore`
→ `ExecuteCanonical`
→ persistence
→ authority `0`

Verify:

- existing historical persistence still succeeds;
- historical catalog semantics remain intact;
- no requirement for Replay config in Historical mode;
- no historical row unexpectedly gets authority 1.

---

# Phase 10 — Required Focused Test Suites

Run/add focused suites covering:

## Schema/migration
- bootstrap to v4;
- v3→v4;
- v4 idempotent startup;
- older-version sequencing;
- newer-version rejection;
- user_version exactness;
- preservation;
- constraints;
- atomic rollback;
- FK/index/trigger/view integrity.

## Persistence
- historical authority 0;
- Replay authority 1;
- invalid authority rejection;
- catalog distinction;
- historical-only vs Replay-aware reads.

## WP03 production flow
- real Replay persistence;
- finite completion;
- same `ExecuteCanonical`;
- no historical-store misuse.

---

# Phase 11 — Predecessor Regression Guards

Rerun:

- Application focused suite
  - predecessor baseline: 122/122;
- WP02 Replay suite
  - predecessor baseline: 142/142;
- Worker/configuration/Dataset-boundary suites.

Higher counts are acceptable if explained.

No predecessor semantics may regress.

---

# Phase 12 — Build and Full Regression

Run established repository build.

Require:

- 0 errors;
- report warning count exactly.

Then run:

`dotnet test AIQuantTradingResearch.slnx --no-restore --nologo`

Immediate predecessor baseline:

**288/288 passed**

A higher count is expected due to migration/persistence tests.

An unexplained lower count is a blocker.

---

# Phase 13 — Production-Flow Acceptance Proof

Before lifecycle mutation, prove the actual Replay production path now succeeds with persistence.

Evidence must include:

- Worker Replay branch selected;
- WP02 source resolved;
- Dataset validation performed;
- WP03 seam invoked;
- `ExecuteCanonical` invoked;
- five stages executed;
- persistence succeeded;
- source authority 1 stored;
- catalog/evidence distinguishes Replay;
- finite completion succeeds.

Also prove Historical production flow still succeeds with authority 0.

If environment blocks process launch again, use only already-governed equivalent evidence rules if they remain valid and can prove actual production composition.

Do not weaken the gate.

---

# Phase 14 — Diff and Scope Audit

Classify every WP03 changed file as:

- canonical-stage extraction;
- observation seam;
- Worker config/validation/composition;
- Dataset-boundary validation;
- schema v4 definition;
- v3→v4 migration;
- persistence/query compatibility;
- WP03/migration/persistence test;
- minimal required config/docs artifact.

Prove:

- only two schema columns' authority domains changed;
- no unrelated schema redesign;
- authority domain exactly {0,1};
- no Replay→Historical coercion;
- no extra Worker/Replay fields;
- no parallel pipeline;
- no WP04+ work;
- no package/Python/schema-adjacent unrelated changes;
- planning/protected objects preserved.

Anything unexplained blocks acceptance.

---

# Phase 15 — WP03 Technical Acceptance Gate

Before GitHub mutation, enumerate every #228 criterion and report:

- implementation evidence;
- test evidence;
- PASS/FAIL.

Additionally require PASS for:

- schema v4 contract implemented exactly;
- v3→v4 migration;
- v4 idempotent validation;
- data preservation;
- atomic rollback;
- authority 0 and 1 acceptance;
- invalid authority rejection;
- Replay snapshot persistence authority 1;
- Replay experiment-result persistence authority 1;
- historical persistence authority 0;
- catalog distinction;
- real Worker Replay production persistence;
- finite completion;
- canonical five-stage reuse;
- no historical-store Replay misuse;
- Application suite;
- WP02 Replay suite;
- build;
- full regression;
- diff/scope audit.

If any fails, keep #228 Open / Backlog.

---

# Phase 16 — WP03 GitHub Lifecycle Finalization

Only after all technical acceptance passes:

1. read #228 current state;
2. confirm established completion convention;
3. add one concise completion/evidence comment if required;
4. transition Project Status from Backlog to authoritative completed state;
5. preserve:
   - Priority = P1;
   - Release = 1.9;
   - authoritative Area;
6. close #228;
7. keep milestone #58 open;
8. read back every mutation.

Do not modify #229.

---

# Expected Post-Completion State

After success:

- #226 Closed / Done;
- #227 Closed / Done;
- #228 Closed / Done or current authoritative completed state;
- #229–#237 remain Open and untouched;
- milestone #58 remains Open;
- canonical milestone counts:
  - 9 open
  - 3 closed;
- raw GitHub closed count may additionally include #225;
- dependency chain remains 11/11;
- schema baseline becomes **v4**;
- successful final WP03 regression count becomes WP04 predecessor baseline;
- WP04 #229 becomes next eligible;
- WP04 remains unstarted.

---

# Stop Conditions

Stop immediately if:

- fixed schema v4 contract conflicts with repository migration architecture;
- affected schema objects differ materially from the proven two tables;
- migration requires broad persistence redesign;
- data preservation cannot be proven;
- rollback cannot be made atomic under repository conventions;
- authority domain would need values beyond 0 and 1;
- Replay persistence still requires historical coercion;
- WP02/WP03 semantics regress;
- WP04+ work becomes necessary;
- focused migration/persistence tests fail;
- build fails;
- full regression fails;
- production Replay persistence cannot be proven;
- diff audit reveals unexplained scope;
- GitHub lifecycle mutation fails or cannot be proven.

On stop:

- preserve validated existing WP03 work;
- do not broaden authority;
- report exact blocker and last proven state;
- leave #228 open unless technical acceptance fully passed and lifecycle mutation alone failed.

---

# Success Criteria

WP03 succeeds only when:

- schema v4 is implemented exactly;
- only `dataset_snapshots.source_authority` and `experiment_results.source_authority` expand to allow 0 and 1;
- v3 data migrates unchanged;
- migration is atomic;
- user_version advances only after success;
- Historical authority 0 remains correct;
- Replay authority 1 persists truthfully;
- invalid authorities fail;
- relevant general reads/catalog paths distinguish Replay;
- historical-only reads remain historical-only where intended;
- real Worker Replay production flow persists required evidence;
- finite completion succeeds;
- canonical five-stage pipeline remains shared;
- all predecessor suites pass;
- build passes;
- full regression passes;
- final diff remains narrowly WP03-scoped;
- #228 is completed and closed;
- milestone #58 remains open;
- #229–#237 remain untouched;
- dependency chain remains intact;
- WP04 remains unstarted.

---

# Required Completion Report

Return:

## Schema implementation
- target version;
- affected tables;
- exact target constraints;
- migration mechanics;
- version-gating behavior.

## Migration proof
- v3→v4 result;
- data preservation;
- atomicity;
- rollback;
- user_version behavior.

## Persistence/query proof
- Historical authority 0;
- Replay authority 1;
- invalid-authority rejection;
- catalog distinction;
- query compatibility.

## WP03 production proof
- actual Historical flow;
- actual Replay flow;
- persistence evidence;
- finite completion;
- `ExecuteCanonical` reuse;
- no Replay historical-store usage.

## Validation
Report exact:
- migration/schema tests;
- persistence tests;
- Worker/WP03 production-flow tests;
- Application suite count;
- WP02 Replay suite count;
- build errors/warnings;
- full regression command and exact counts.

## Scope proof
- full diff classification;
- no unrelated schema redesign;
- authority domain exactly {0,1};
- no provenance coercion;
- no WP04+ work;
- no unauthorized foundation/planning changes.

## GitHub lifecycle
- #228 before/after;
- Project Status before/after;
- completion comment;
- milestone #58 canonical counts;
- #229–#237 untouched.

## Next eligibility

State:

`NEXT ELIGIBLE WORK PACKAGE: WP04 — #229`

Do not authorize or execute WP04.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP03 SCHEMA EVOLUTION IMPLEMENTATION AND COMPLETION COMPLETE`

On blocker:

`RELEASE 1.9 WP03 SCHEMA EVOLUTION IMPLEMENTATION AND COMPLETION BLOCKED`

Do not emit success unless every technical and lifecycle requirement is freshly proven.

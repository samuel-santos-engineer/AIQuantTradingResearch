# Release 1.6 WP12 — Application & Infrastructure Persistence Tests — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP12 — Application & Infrastructure Persistence Tests — GitHub issue #193**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP12 converts the accepted temporary/offline validation evidence from WP07–WP11 into permanent regression protection.

WP12 must add the minimum permanent Application and Infrastructure tests needed to protect:

- durable Experiment use-case orchestration;
- schema-v3 persistence and migration behavior;
- `NewlyAccepted`;
- `EquivalentExisting`;
- `IntegrityConflict`;
- exact retrieval;
- `NotFound`;
- `DependencyUnavailable`;
- invalid durable evidence handling;
- exact decimal and empty/non-empty fidelity;
- DI composition;
- one-shot Durable Experiment Worker process behavior;
- Durable Experiment routing precedence and no-fallback behavior;
- predecessor Release 1.1–1.5 preservation.

WP12 also incorporates one narrowly bounded engineering-methodology improvement discovered during WP11:

**Process-Level Validation Prerequisites**

This must be added to `ENGINEERING_PLAYBOOK.md` without unrelated playbook restructuring or cleanup.

WP12 must not redesign production semantics, schema v3, persistence/retrieval behavior, DI, Worker routing, or the Release 1.6 failure model unless a permanent test exposes a genuine defect within already accepted Release 1.6 scope.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`
- accepted WP01–WP11 execution evidence
- accepted WP07 predecessor Infrastructure test reconciliation evidence
- accepted WP10 composition-scope reconciliation evidence
- accepted WP11 A1/A2/A3 validation evidence
- accepted WP11 Slice B failure/no-fallback evidence
- accepted WP11 Slice C predecessor-preservation evidence
- accepted WP11 final acceptance evidence
- existing Application test conventions
- existing Infrastructure test conventions
- existing process-level Worker test conventions
- `ENGINEERING_PLAYBOOK.md`
- this WP12 authority and its five-line companion

Do not reinterpret accepted Release 1.6 production semantics.

---

## 3. Starting Gate

Before mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- Release 1.5 authoritative baseline remains `18dfb01bf3503d91415b081b11fcdd7249094373`;
- cumulative Release 1.6 candidate work remains expected and uncommitted/un-staged;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182–#192: CLOSED / Done;
- #193: OPEN / Backlog;
- #194–#195: OPEN / Backlog;
- milestone #47: OPEN, 3 open / 11 closed;
- Project #2 fields remain correct;
- schema v3 is implemented;
- WP11 Worker behavior is complete and accepted;
- temporary WP11 validation databases/probes are removed;
- permanent baseline is 238/238;
- no premature WP13+ implementation exists;
- no Release 1.7 work exists.

Expected Release 1.6 governance/candidate paths are not blockers.

If a mandatory gate fails, stop before moving #193 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #193 `Backlog → In Progress`;
2. implement only WP12;
3. validate;
4. post concise completion evidence to #193;
5. close #193;
6. set #193 `In Progress → Done`.

Required final lifecycle:

- #182–#193: CLOSED / Done;
- #194–#195: OPEN / Backlog;
- milestone #47: OPEN, 2 open / 12 closed.

No other GitHub lifecycle mutation is authorized.

---

## 5. Manifest Is Binding

Use `RELEASE_1.6_FILE_MANIFEST.md` as exact path authority.

Modify/create only WP12-authorized permanent test paths, plus the single explicitly authorized playbook path:

`ENGINEERING_PLAYBOOK.md`

If the manifest assigns one Application test file and one Infrastructure test file, use those exact files.

Do not create additional permanent test files merely for convenience.

If the minimum coherent permanent coverage cannot fit the manifest-authorized test paths, stop and report the smallest corrective authority required.

---

## 6. Additional WP12 Playbook Authority

WP12 explicitly authorizes one narrow update to:

`ENGINEERING_PLAYBOOK.md`

Add one subsection:

`### Process-Level Validation Prerequisites`

The subsection must capture this engineering rule:

> Process-level validation work packages must identify a repository-native fixture or seeding path during planning.
>
> When validation depends on synthetic durable state, the execution authority must explicitly select one of these mechanisms:
> - reuse existing permanent test helpers;
> - use a removable probe hosted by an existing test project with the required internal access; or
> - use a dedicated, supported validation-fixture mechanism already established by the repository.
>
> The authority must also define prerequisite construction, cleanup, residue checks, and whether any temporary artifact must survive across validation checkpoints.
>
> External ad hoc probes should not bypass repository visibility boundaries or require production types to be made public solely for validation.

This update is engineering-methodology hardening discovered during WP11.

It has:

- production behavior delta: 0;
- architecture semantic delta: 0;
- schema delta: 0;
- runtime delta: 0.

Do not perform unrelated `ENGINEERING_PLAYBOOK.md` cleanup, wording modernization, section reordering, formatting sweep, or additional governance changes.

---

## 7. WP11 Lesson Preservation

Permanent WP12 process tests must reuse repository-native fixture/seeding mechanisms rather than recreating the failed external-probe pattern from WP11.

Prefer, as applicable:

- existing Infrastructure test helpers;
- test-host-local helpers;
- production stores through the Infrastructure test assembly;
- temporary databases with deterministic cleanup.

Do not make internal production types public solely for tests.

Do not add `InternalsVisibleTo` for an ad hoc probe project.

Do not add a generic fixture framework unless the manifest explicitly authorizes it.

---

## 8. Permanent Test Philosophy

WP12 should convert accepted temporary proofs into durable regression tests, not reproduce every exploratory probe verbatim.

Tests must be:

- deterministic;
- offline;
- isolated;
- bounded in runtime;
- cleanup-safe;
- explicit about semantic evidence;
- non-flaky;
- independent of real credentials;
- independent of provider/network access.

Prefer the smallest permanent matrix that protects distinct behaviors without redundant duplication.

---

## 9. Application Test Coverage

The manifest-authorized Application test surface must permanently cover the Release 1.6 Application-owned durable use-case behavior.

At minimum cover the applicable distinct cases:

1. valid durable request invokes Release 1.5 Experiment generation exactly once;
2. successful generation projects reduced durable evidence without Feature Values;
3. store acceptance invoked exactly once;
4. `NewlyAccepted` success;
5. `EquivalentExisting` success;
6. generation bounded failure prevents store invocation;
7. store bounded failure propagates through accepted Release 1.6 vocabulary;
8. first-failure short-circuit behavior;
9. unknown defect propagation;
10. exact Experiment Result identity preservation;
11. exact Feature Set / snapshot / provenance binding;
12. empty result projection with count 0 and aggregates absent;
13. non-empty exact decimal fidelity;
14. no fabricated Release 1.5 Feature Value/object graph.

Do not duplicate Release 1.5 Experiment tests unnecessarily.

---

## 10. Application Test Boundaries

Application tests must remain storage-independent.

Do not:

- open SQLite;
- seed schema;
- invoke Worker;
- call provider/network;
- depend on real files.

Use fakes/stubs consistent with existing Application tests.

No package/reference change.

---

## 11. Infrastructure Permanent Coverage

The manifest-authorized Infrastructure test surface must protect the completed schema-v3/storage/Worker-composition behavior.

Permanent coverage should include the minimum non-redundant matrix across:

- fresh schema-v3 bootstrap;
- v2→v3 migration;
- predecessor data preservation;
- `experiment_results` physical invariants;
- `NewlyAccepted`;
- `EquivalentExisting`;
- `IntegrityConflict`;
- no duplicate logical row;
- exact retrieval;
- `NotFound`;
- `DependencyUnavailable`;
- malformed durable evidence classification where constructible safely;
- exact decimal round-trip;
- signed-zero fidelity where applicable;
- empty/non-empty aggregate fidelity;
- provenance/reference fidelity;
- restrictive snapshot foreign key;
- no Feature Set persistence;
- DI registration cardinality/lifetime;
- side-effect-free DI resolution;
- Durable Experiment Worker process behavior;
- routing precedence;
- malformed/partial no-fallback;
- predecessor Experiment/Feature/pipeline preservation.

Do not create redundant test cases for behavior already permanently covered elsewhere unless Release 1.6 requires a new regression assertion.

---

## 12. Schema-v3 Permanent Tests

Permanent Infrastructure tests must establish accepted current state:

- `PRAGMA user_version = 3`;
- exactly one accepted Release 1.6 Experiment Result persistence table: `experiment_results`;
- no Feature Set persistence table;
- no generalized experiment registry/history/search tables;
- accepted STRICT/WITHOUT ROWID behavior as applicable;
- exact key/constraint behavior required by WP06;
- restrictive FK behavior;
- predecessor tables preserved.

Do not add schema-v4 expectations.

---

## 13. Migration Permanent Tests

At minimum preserve/prove:

- valid v2 → v3 migration succeeds;
- observation/snapshot predecessor evidence remains unchanged;
- no Experiment Result backfill;
- no Feature Set backfill;
- target version = 3;
- failure does not falsely report v3;
- future unsupported version remains >3.

Use existing migration-test conventions.

Do not rewrite predecessor tests wholesale.

---

## 14. Persistence Acceptance Tests

Permanent Infrastructure tests should distinguish:

### NewlyAccepted
- first complete evidence persists;
- exact identity;
- one row.

### EquivalentExisting
- repeat equivalent acceptance succeeds;
- row count remains one;
- evidence unchanged.

### IntegrityConflict
- same identity + contradictory evidence fails;
- no overwrite;
- no delete;
- no `EquivalentExisting`.

Do not weaken constraints to construct conflicts.

---

## 15. Retrieval Tests

Permanent exact retrieval coverage should prove:

- exact typed identity Found;
- complete durable evidence reconstruction;
- identity/provenance/count/presence/decimal fidelity;
- empty result retrieval;
- non-empty result retrieval;
- exact missing identity → `NotFound`;
- no provider/generation fallback;
- retrieval is read-only.

Do not add list/search/history APIs.

---

## 16. Failure-Mapping Tests

Permanent tests should cover bounded storage classifications where deterministic and safe:

- `DependencyUnavailable`;
- `InvalidEvidence`;
- `IntegrityConflict`;
- `NotFound`.

Preserve Application-owned `InvalidRequest`.

Unknown defects must still propagate.

Do not add broad exception normalization merely to make a test possible.

---

## 17. DI Composition Tests

Permanent Infrastructure composition tests must prove:

- `IDurableExperimentUseCase` registered exactly once;
- `IDurableExperimentEvidenceStore` registered exactly once;
- expected lifetimes;
- predecessor registrations remain singular;
- real graph resolution succeeds;
- DI resolution has no schema/evidence/provider side effects.

Do not duplicate every WP10 temporary assertion if one permanent test can cover multiple structural invariants cleanly.

---

## 18. Worker Process Tests

Convert the highest-value WP11 temporary process proofs into permanent process-level tests.

At minimum protect:

1. valid non-empty Durable Experiment first run → exit 0 / `NewlyAccepted`;
2. independent second process → exit 0 / `EquivalentExisting`;
3. same Experiment Result identity across processes;
4. durable logical row remains one;
5. empty Durable Experiment → exit 0 / count 0 / aggregates absent;
6. malformed Durable intent → exit 1 / no fallback;
7. partial Durable intent → exit 1 / no fallback;
8. exact NotFound → exit 1;
9. DependencyUnavailable → exit 1;
10. lower Release 1.5 Experiment routing preserved;
11. Release 1.4 Feature routing preserved;
12. Release 1.3 pipeline routing preserved;
13. valid conflicting selectors choose Durable Experiment only.

Use existing `ExperimentCompositionTests.RunWorker(...)` / process conventions where appropriate.

Do not create an external temporary console host.

---

## 19. Process-Level Fixture Rule Applied

For all new process tests, explicitly choose and document in code structure the repository-native fixture path.

Preferred pattern:

- Infrastructure test assembly as host;
- existing `Snapshot(...)` / `Seed(...)` style helpers or manifest-authorized equivalent;
- `TemporaryDatabase.Factory`;
- `SqliteDatasetSnapshotStore.Store(...)`;
- isolated temporary database;
- deterministic cleanup.

Do not bypass internal visibility.

This WP12 implementation should itself demonstrate the new `ENGINEERING_PLAYBOOK.md` rule.

---

## 20. Test Count Discipline

WP12 is expected to increase permanent test count.

Do not target an arbitrary number.

Add only tests required for non-redundant Release 1.6 regression protection.

Report exact deltas:

- Domain delta;
- Application delta;
- Infrastructure delta;
- Architecture delta;
- total delta.

Domain and Architecture test deltas are expected to remain 0 unless the manifest explicitly authorizes otherwise.

---

## 21. No Production Change by Default

WP12 is test/documentation work.

Expected production delta:

`0`

If a new permanent test exposes a genuine defect:

1. determine whether the fix is clearly within already accepted Release 1.6 semantics and manifest authority;
2. if production correction is not explicitly authorized by WP12, stop;
3. report the smallest corrective authority required.

Do not silently patch production to make tests pass.

---

## 22. No Schema Change

Schema remains v3.

Do not modify:

- schema version;
- tables;
- columns;
- indexes;
- migrations;
- physical decimal representation;
- persistence/retrieval semantics.

Tests may create/migrate disposable databases using production code.

---

## 23. No DI / Worker Redesign

Tests must validate current accepted WP10/WP11 behavior.

Do not change:

- DI registrations;
- routing precedence;
- configuration keys;
- output semantics;
- exit codes.

If tests reveal a defect, stop unless correction is separately authorized.

---

## 24. Provider / Network Isolation

All WP12 tests must run offline.

Require:

- provider calls: 0;
- network calls: 0;
- real credentials: 0.

If a syntactic provider key is required by predecessor composition, use a dummy value only and assert no provider call occurs.

---

## 25. Cleanup and Residue

Every database/process test must clean:

- SQLite DBs;
- WAL;
- SHM;
- journal;
- temporary directories;
- captured output;
- temporary process artifacts.

No retained validation databases after permanent tests complete.

Residue after test suite:

`0`

---

## 26. Concurrency / Runtime Discipline

Do not create timing-sensitive tests.

Use deterministic timeouts consistent with existing process-test conventions.

Avoid unnecessary repeated full Worker processes if one test can assert the relevant distinct behavior.

Keep the suite practical for canonical verification.

---

## 27. Predecessor Preservation

All Releases 1.1–1.5 behavior remains protected.

Require existing tests remain green for:

- Release 1.1 observation persistence;
- Release 1.2 dataset/snapshot behavior;
- Release 1.3 pipeline;
- Release 1.4 Feature generation/Worker;
- Release 1.5 Experiment generation/Worker.

WP12 must not weaken predecessor assertions.

---

## 28. Architecture / Package / Reference Preservation

Production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Require:

- unexpected edges: 0;
- cycles: 0;
- package delta: 0;
- project delta: 0;
- reference delta: 0.

No new testing package unless already present and manifest-authorized.

---

## 29. Playbook Validation

For `ENGINEERING_PLAYBOOK.md` require:

- exactly one new Process-Level Validation Prerequisites subsection;
- wording materially equivalent to the authority;
- no unrelated section edits;
- no broken repository-relative links;
- no trailing whitespace;
- terminal newline present.

Report playbook changed lines/section only.

---

## 30. Targeted Validation

Run targeted Application WP12 tests.

Run targeted Infrastructure WP12 tests.

Run Worker process tests as needed.

Require all new/modified tests pass before canonical verification.

Record:

- targeted Application count/result;
- targeted Infrastructure count/result;
- Worker process subset result;
- exact permanent test delta.

---

## 31. Canonical Validation

After all targeted tests pass run:

`eng/verify.ps1 -Configuration Release`

Require:

- Restore: PASS;
- Formatting: PASS;
- Gitleaks: PASS;
- Release build: PASS;
- warnings/errors: 0/0;
- all Domain tests pass;
- all Application tests pass;
- all Infrastructure tests pass;
- all Architecture tests pass;
- skipped: 0.

Also require:

- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- direct untracked-governance whitespace/final-newline checks: PASS;
- staged paths: 0;
- schema v3;
- database/WAL/SHM/journal/probe residue: 0;
- provider/network activity: 0;
- real credentials: 0;
- package/project/reference delta: 0/0/0;
- production graph unchanged.

---

## 32. Structural Acceptance

Require:

- only manifest-authorized WP12 test paths plus `ENGINEERING_PLAYBOOK.md`;
- production delta 0;
- Application permanent coverage added;
- Infrastructure permanent coverage added;
- WP11 temporary validation scenarios represented by durable non-redundant tests;
- no external probe project;
- no new package/project/reference;
- schema v3 unchanged;
- no WP13+ implementation;
- no Release 1.7 work.

---

## 33. Mutation Budget

Authorized repository mutations:

- exact manifest-authorized WP12 permanent test path(s);
- `ENGINEERING_PLAYBOOK.md` only for the Process-Level Validation Prerequisites subsection.

Authorized GitHub mutations:

1. #193 Backlog → In Progress;
2. completion evidence comment;
3. close #193;
4. #193 In Progress → Done.

Not authorized:

- staging;
- commit;
- branch;
- push;
- PR;
- tag/release;
- milestone closure;
- #194–#195 mutation;
- production redesign;
- schema change;
- package/project/reference changes;
- Release 1.7 work.

---

## 34. Stop Conditions

Stop with #193 OPEN / In Progress if:

- manifest test-path authority is ambiguous;
- permanent coverage requires additional test files beyond authority;
- a production defect is exposed and correcting it is not WP12-authorized;
- a schema/DI/Worker semantic change is required;
- a new package/reference is required;
- process tests require making internal production types public;
- external probe architecture becomes necessary;
- canonical verification fails for a genuine production regression;
- provider/network activity occurs;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

---

## 35. Completion Evidence

Post concise #193 evidence including:

- exact changed paths;
- Application test delta;
- Infrastructure test delta;
- total permanent test delta;
- durable use-case coverage;
- schema-v3/migration coverage;
- persistence/retrieval/failure coverage;
- DI coverage;
- Worker process coverage;
- predecessor routing preservation;
- process-level fixture mechanism used;
- `ENGINEERING_PLAYBOOK.md` rule added;
- production/schema/package/reference delta 0;
- canonical full test count;
- no provider/network/residue;
- next WP13/#194.

Do not paste the full test inventory into GitHub.

---

## 36. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact changed paths;
5. Application test inventory;
6. Infrastructure test inventory;
7. schema-v3/migration coverage;
8. persistence acceptance coverage;
9. exact retrieval coverage;
10. failure mapping coverage;
11. DI composition coverage;
12. Worker process coverage;
13. process-level fixture/seeding mechanism;
14. WP11 lesson converted to permanent methodology;
15. `ENGINEERING_PLAYBOOK.md` exact subsection;
16. predecessor preservation;
17. production delta;
18. test-count delta by project;
19. targeted validation;
20. canonical validation;
21. formatting/Gitleaks/whitespace;
22. schema/residue/provider isolation;
23. architecture/package/reference checks;
24. repository mutation accounting;
25. GitHub lifecycle;
26. findings/blockers;
27. next authorized WP.

---

## 37. Completion Marker

On success end exactly:

`RELEASE 1.6 WP12 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP13 — Architecture & Documentation Alignment — GitHub issue #194`

Required final lifecycle:

- #182–#193: CLOSED / Done
- #194–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked end:

`RELEASE 1.6 WP12 BLOCKED`

and identify the smallest corrective authority required.

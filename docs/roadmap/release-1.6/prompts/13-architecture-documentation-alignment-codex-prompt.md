# Release 1.6 WP13 — Architecture & Documentation Alignment — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP13 — Architecture & Documentation Alignment — GitHub issue #194**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP13 aligns architecture and current-state documentation with the accepted Release 1.6 implementation and permanent regression evidence.

WP13 must reconcile documentation to the actual implemented state now established by WP01–WP12, including:

- durable Experiment Result evidence;
- schema v3;
- `experiment_results`;
- `aiq-experiment-identity-v1` reuse;
- exact acceptance/retrieval semantics;
- `NewlyAccepted`, `EquivalentExisting`, `NotFound`, `DependencyUnavailable`, `InvalidEvidence`, `IntegrityConflict`;
- explicit Durable Experiment Worker mode;
- routing precedence;
- Application/Infrastructure/Worker ownership;
- no Feature Set persistence;
- no generalized registry/history;
- permanent Release 1.6 test coverage;
- process-level validation prerequisite methodology where current-state documentation references engineering validation practice.

WP13 is documentation/architecture alignment only.

It must not change production behavior, schema, tests, packages, projects, references, DI, Worker execution, or Git integration state.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`
- accepted WP01–WP12 execution evidence
- accepted WP10 composition-scope reconciliation
- accepted WP11 validation/final acceptance evidence
- accepted WP12 permanent-test evidence
- `docs/handbook/ENGINEERING_PLAYBOOK.md`
- existing Release 1.5 architecture/current-state documentation
- all manifest-authorized WP13 documentation paths
- this WP13 authority and its five-line companion

Repository truth plus accepted Release 1.6 authorities govern current-state wording.

Do not weaken accepted semantics to preserve stale documentation.

---

## 3. Starting Gate

Before mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- Release 1.5 authoritative baseline remains `18dfb01bf3503d91415b081b11fcdd7249094373`;
- cumulative Release 1.6 work remains expected and uncommitted/un-staged;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182–#193: CLOSED / Done;
- #194: OPEN / Backlog;
- #195: OPEN / Backlog;
- milestone #47: OPEN, 2 open / 12 closed;
- Project #2 fields remain correct;
- schema v3 is implemented;
- permanent baseline is 250/250;
- WP12 production/schema/package/reference delta was 0;
- no premature WP14 implementation exists;
- no Release 1.7 work exists.

Expected Release 1.6 governance/candidate paths are not blockers.

If a mandatory gate fails, stop before moving #194 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #194 `Backlog → In Progress`;
2. perform architecture/documentation alignment only;
3. validate;
4. post concise completion evidence to #194;
5. close #194;
6. set #194 `In Progress → Done`.

Required final lifecycle:

- #182–#194: CLOSED / Done;
- #195: OPEN / Backlog;
- milestone #47: OPEN, 1 open / 13 closed.

No other GitHub lifecycle mutation is authorized.

---

## 5. Manifest Is Binding

Use `RELEASE_1.6_FILE_MANIFEST.md` as exact path authority.

Modify only the WP13-authorized documentation/current-state paths.

Do not create additional documentation files unless the manifest explicitly authorizes them.

Do not edit roadmap definition/plan/manifest except where the manifest explicitly assigns a WP13 reconciliation line and authority is unambiguous.

Do not modify governance prompts merely to reflect implementation.

If a required current-state correction falls outside manifest-authorized paths, stop and report the smallest corrective authority required.

---

## 6. Architecture Test Decision

Review the existing 13 Architecture.Tests against Release 1.6.

Determine whether Release 1.6 introduced any new structural dependency rule not already executable through the existing architecture test suite.

Expected outcome:

- Architecture test delta = 0.

Do not add an Architecture test merely because Release 1.6 added a capability.

Add one only if:
- a new structural graph rule exists;
- it is not already enforced;
- the manifest explicitly authorizes the path;
- the test is non-redundant.

If no such rule exists, document why 13/13 remains sufficient.

---

## 7. Current Production Graph

All aligned documentation must preserve:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Also preserve:

- cycles = 0;
- unexpected edges = 0.

Do not imply a direct Application → Infrastructure dependency.

Do not imply Worker owns persistence semantics.

---

## 8. Release 1.6 Capability Statement

Current-state docs should describe Release 1.6 as:

**Durable Experiment Evidence Foundation**

The implemented capability is:

- persist accepted Release 1.5 Experiment Result evidence;
- exact lookup by typed Experiment Result Identity;
- distinguish `NewlyAccepted` and `EquivalentExisting`;
- reject same-identity contradictory evidence as `IntegrityConflict`;
- preserve exact empty/non-empty aggregate fidelity;
- use schema v3;
- execute an explicit one-shot Durable Experiment Worker mode.

Do not describe deferred registry/history/search functionality as implemented.

---

## 9. Identity Semantics

Documentation must preserve:

- `aiq-experiment-identity-v1`;
- no new persistence identity;
- Experiment Result Identity remains the exact semantic durable lookup key;
- exact Feature Set/definition/snapshot/provenance binding;
- identity equality is not equivalent to complete evidence equivalence;
- contradictory same-identity evidence is an integrity conflict.

Do not introduce row IDs or storage-generated semantic identities into architecture docs.

---

## 10. Durable Evidence Boundary

Documentation must distinguish:

- Release 1.5 in-memory Experiment Result model;
- Release 1.6 reduced durable Experiment evidence.

State explicitly that Feature Values are not persisted and are not fabricated during durable retrieval.

Do not imply full Release 1.5 object-graph reconstruction from storage.

---

## 11. Persistence Semantics

Current-state docs must reflect:

- schema version 3;
- exactly one Release 1.6 Experiment Result persistence table: `experiment_results`;
- immutable acceptance semantics;
- no update/delete;
- no overwrite;
- exact first acceptance → `NewlyAccepted`;
- exact equivalent reacceptance → `EquivalentExisting`;
- contradictory same-identity evidence → `IntegrityConflict`.

Do not describe generic CRUD.

---

## 12. Retrieval Semantics

Document exact retrieval only:

`ExperimentResultIdentity → Found durable evidence / NotFound`

Also preserve:

- read-only retrieval;
- exact canonical decimal reconstruction;
- exact provenance/reference fidelity;
- empty-result fidelity;
- no provider fallback;
- no recomputation;
- no list/search/history.

---

## 13. Schema-v3 Alignment

Where relevant, update docs to current state:

- implemented schema is v3;
- v2→v3 migration is atomic/non-destructive;
- predecessor observation/snapshot evidence preserved;
- fresh databases create complete v3;
- no Feature Set backfill;
- no Experiment Result backfill during migration;
- future unsupported versions are >3;
- `experiment_results` is `STRICT, WITHOUT ROWID` if documented in the accepted WP06 authority;
- restrictive snapshot FK remains current state.

Do not introduce v4 plans.

---

## 14. Decimal / Aggregate Fidelity

Documentation must preserve the accepted durable representation semantics:

- decimal-only;
- canonical sign/coefficient/scale physical representation as frozen by WP06;
- exact round-trip fidelity;
- signed-zero preservation where identity semantics require it;
- empty result: count 0, aggregates absent;
- non-empty result: positive count, all aggregates present.

Do not describe SQLite REAL storage.

---

## 15. Failure Model

Current-state docs must preserve the Release 1.6 bounded vocabulary:

1. `InvalidRequest`
2. `NotFound`
3. `DependencyUnavailable`
4. `InvalidEvidence`
5. `IntegrityConflict`

Also document, where relevant:

- unknown programming defects propagate;
- no broad exception normalization;
- no retry/recovery/repair/fallback;
- `EquivalentExisting` is success, not failure.

Do not add failure categories.

---

## 16. Application Ownership

Documentation must describe Application as owner of:

- durable persistence contracts;
- reduced durable evidence semantics;
- durable use-case orchestration;
- semantic outcomes/failures;
- exact predecessor binding.

Application remains storage-independent.

Do not describe SQLite/SQL ownership in Application.

---

## 17. Infrastructure Ownership

Documentation must describe Infrastructure as owner of:

- SQLite schema v3;
- migration/bootstrap;
- Experiment Result acceptance persistence;
- exact durable retrieval;
- storage mapping;
- bounded storage validation/classification;
- connection/transaction mechanics.

Do not imply Infrastructure owns experiment computation semantics.

---

## 18. Worker Ownership

Current-state docs must describe Worker ownership of:

- Durable Experiment configuration;
- explicit Durable Experiment intent;
- routing precedence;
- one-shot invocation;
- bounded semantic output;
- exit behavior.

Required precedence:

`Durable Experiment → Experiment → Feature → five-stage pipeline`

Malformed/partial Durable intent must not fall back.

Do not imply Durable Experiment is the default mode.

---

## 19. Existing Mode Preservation

Docs must preserve:

- Release 1.5 Experiment mode remains explicit and non-durable;
- Release 1.4 Feature mode remains explicit;
- Release 1.3 five-stage pipeline remains default when higher selectors are absent;
- Durable Experiment is additive.

Do not collapse modes into one generalized pipeline.

---

## 20. DI / Composition Alignment

Document current composition where relevant:

- `IDurableExperimentUseCase → DurableExperimentUseCase`;
- `IDurableExperimentEvidenceStore → SqliteExperimentResultStore`;
- both registered exactly once;
- accepted lifetimes preserved;
- DI resolution is side-effect-free.

Do not document a separate durable database.

---

## 21. Testing Strategy Alignment

Where the manifest includes testing documentation, update it to reflect permanent Release 1.6 evidence:

- Application permanent tests: 111;
- Infrastructure permanent tests: 117;
- Domain: 11;
- Architecture: 13;
- total permanent tests: 250;
- skipped: 0.

Also describe high-value permanent coverage:

- durable use-case orchestration;
- schema-v3/migration;
- acceptance/retrieval/failure mapping;
- DI;
- Worker NewlyAccepted/EquivalentExisting;
- empty result;
- no-fallback;
- predecessor routing preservation.

Do not overstate coverage beyond WP12 evidence.

---

## 22. Engineering Playbook Alignment

`ENGINEERING_PLAYBOOK.md` was already updated under WP12.

WP13 should verify it is consistent with current engineering methodology but must not modify it unless the manifest explicitly assigns it to WP13.

The existing `Process-Level Validation Prerequisites` rule should remain intact.

Do not duplicate the rule into multiple documentation locations unless the manifest explicitly requires cross-reference wording.

---

## 23. Observability Alignment

Where the manifest includes observability documentation, distinguish:

- bounded semantic Worker output;
- existing logging/observability conventions;
- no new telemetry backend;
- no durable execution-history backend;
- no timestamps/random metadata as semantic identity evidence.

Do not describe deferred telemetry systems as implemented.

---

## 24. Configuration Alignment

Where configuration docs are authorized, document:

- `DurableExperiment:SnapshotIdentity`;
- `DurableExperiment:SnapshotVersion`;
- exact typed/invariant parsing;
- code-owned `simple-return-descriptive-summary-v1`;
- partial Durable intent fails;
- Durable Experiment intent does not borrow lower-mode configuration;
- existing SQLite configuration reused.

Do not add configuration keys.

---

## 25. Public Contracts Alignment

Where public-contract documentation is authorized, include only existing implemented contracts/seams.

Do not invent APIs.

Describe the actual durable interfaces/types now present in Application.

Keep storage implementation details out of Application contract documentation.

---

## 26. Module Interaction Alignment

Where module-interaction documentation is authorized, current flow should be represented as:

`Worker Durable Experiment intent`
→ `IDurableExperimentUseCase`
→ existing `IExperimentGenerationUseCase`
→ reduced durable evidence
→ `IDurableExperimentEvidenceStore.Accept`
→ schema-v3 persistence
→ bounded acceptance outcome

Exact retrieval remains a separate read path through the same store abstraction.

Do not imply Worker calls store/SQL directly.

---

## 27. Data Pipeline Alignment

Where data-pipeline documentation is authorized, preserve predecessor flow and add durable Experiment as a bounded downstream path.

Do not alter Release 1.3 five-stage pipeline semantics.

Do not present Experiment persistence as part of provider acquisition.

Durable evidence is downstream of already persisted snapshot evidence.

---

## 28. Explicit Deferrals

Documentation must continue to defer:

- Feature Set persistence;
- generalized experiment registry/history;
- list/search/comparison;
- update/delete;
- additional experiment families;
- strategies/signals/backtesting;
- scheduling/retries/checkpoints;
- provider acquisition orchestration;
- workspace/UI/API;
- AI/ML;
- Release 1.7 work.

Do not make roadmap promises beyond accepted authorities.

---

## 29. No Stale v2 Claims

Search all manifest-authorized WP13 documentation paths for current-state claims that incorrectly state:

- schema is v2;
- Experiment Results are never persisted;
- no Experiment Result table exists;
- Durable Experiment Worker mode does not exist;
- permanent test total is 238.

Correct only material current-state claims.

Historical statements explicitly scoped to Releases 1.1–1.5 may remain if accurate.

Do not mechanically replace every `v2` occurrence.

---

## 30. Markdown Link Validation

Validate repository-relative Markdown links in all modified WP13 docs.

Require:

- broken links = 0;
- malformed anchors introduced = 0.

Do not rewrite valid unrelated links.

---

## 31. Documentation Style

Preserve repository documentation conventions:

- headings;
- terminology;
- concise declarative architecture prose;
- explicit current-state vs deferred-state distinctions;
- relative links where established.

Avoid marketing language.

Avoid implementation-detail dumps.

---

## 32. No Production / Test Mutation

Expected WP13 deltas:

- production: 0;
- tests: 0;
- schema: 0;
- packages: 0;
- projects: 0;
- references: 0.

Do not change code to make documentation true.

If documentation reveals a genuine implementation contradiction, stop and report the smallest corrective authority required.

---

## 33. Targeted Documentation Validation

Before canonical verification:

- inspect all modified docs;
- run Markdown link checks;
- search for stale Release 1.6 current-state claims;
- verify no contradictory schema/version wording;
- verify test counts if referenced;
- verify no unsupported Release 1.7 claims;
- verify no accidental broad edits.

Record exact changed documentation paths.

---

## 34. Canonical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Require:

- Restore: PASS;
- Formatting: PASS;
- Gitleaks: PASS;
- Release build: PASS;
- warnings/errors: 0/0;
- Domain.Tests: 11/11;
- Application.Tests: 111/111;
- Infrastructure.Tests: 117/117;
- Architecture.Tests: 13/13;
- Permanent total: 250/250;
- Skipped: 0.

Also require:

- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- direct expected-untracked whitespace/final-newline checks: PASS;
- staged paths: 0;
- schema v3;
- provider/network activity: 0;
- database/WAL/SHM/journal/probe residue: 0;
- package/project/reference delta: 0/0/0;
- production graph unchanged.

---

## 35. Structural Acceptance

Require:

- only manifest-authorized WP13 documentation paths changed;
- Architecture test delta = 0 unless explicitly justified and authorized;
- production delta = 0;
- permanent test delta = 0;
- schema remains v3;
- package/project/reference delta = 0/0/0;
- Release 1.6 current-state documentation coherent;
- no material stale claims;
- no WP14 implementation;
- no Release 1.7 work.

---

## 36. Mutation Budget

Authorized repository mutations:

- exact manifest-authorized WP13 documentation paths only.

Authorized GitHub mutations:

1. #194 Backlog → In Progress;
2. completion evidence comment;
3. close #194;
4. #194 In Progress → Done.

Not authorized:

- staging;
- commit;
- branch;
- push;
- PR;
- tag/release;
- milestone closure;
- #195 mutation;
- production/test/schema/package/reference changes;
- Release 1.7 work.

---

## 37. Stop Conditions

Stop with #194 OPEN / In Progress if:

- manifest documentation authority is ambiguous;
- a material implementation/documentation contradiction is discovered;
- production/test/schema changes would be required;
- a new Architecture test appears necessary but is not manifest-authorized;
- package/project/reference change is required;
- canonical verification fails for a genuine implementation regression;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

---

## 38. Completion Evidence

Post concise #194 evidence including:

- exact changed documentation paths;
- Architecture test decision/delta;
- Release 1.6 capability alignment;
- schema-v3 alignment;
- identity/provenance alignment;
- persistence/retrieval/failure semantics;
- Durable Experiment Worker/configuration/routing alignment;
- test-count alignment;
- engineering-playbook consistency;
- stale-current-state findings corrected;
- Markdown link result;
- production/test/schema/package/reference delta 0;
- canonical 250/250;
- next WP14/#195.

---

## 39. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact changed paths;
5. Architecture test decision;
6. capability/current-state alignment;
7. identity/provenance alignment;
8. persistence/retrieval alignment;
9. schema-v3 alignment;
10. failure-model alignment;
11. Application ownership;
12. Infrastructure ownership;
13. Worker/configuration/routing alignment;
14. predecessor-mode preservation;
15. DI/composition alignment;
16. testing-strategy alignment;
17. playbook consistency;
18. observability/configuration/public-contract/module-interaction alignment as applicable;
19. deferrals preserved;
20. stale-current-state findings;
21. Markdown link validation;
22. production/test/schema/package/reference delta;
23. canonical validation;
24. whitespace/security/residue;
25. repository mutation accounting;
26. GitHub lifecycle;
27. findings/blockers;
28. next authorized WP.

---

## 40. Completion Marker

On success end exactly:

`RELEASE 1.6 WP13 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP14 — Full Validation, Integration & Acceptance — GitHub issue #195`

Required final lifecycle:

- #182–#194: CLOSED / Done
- #195: OPEN / Backlog
- milestone #47: OPEN

If blocked end:

`RELEASE 1.6 WP13 BLOCKED`

and identify the smallest corrective authority required.

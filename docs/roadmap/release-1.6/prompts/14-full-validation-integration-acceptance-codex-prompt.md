# Release 1.6 WP14 — Full Validation, Integration & Acceptance — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP14 — Full Validation, Integration & Acceptance — GitHub issue #195**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP14 is the final Release 1.6 acceptance gate.

Its purpose is to prove that the cumulative WP01–WP13 candidate state is coherent, complete, reproducible, secure, offline, architecture-preserving, documentation-aligned, and ready for governed integration.

WP14 is an **integration and acceptance work package, not a development work package**.

Default repository content mutation budget:

**0**

Do not add capability, refactor production code, expand documentation, add tests, change schema, alter DI/Worker behavior, or perform opportunistic cleanup.

If any material defect or contradiction is discovered, stop and request the smallest corrective authority required.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`
- accepted WP01–WP13 execution evidence
- accepted WP07 predecessor-test reconciliation evidence
- accepted WP10 scope reconciliation evidence
- accepted WP11 process-validation evidence and final acceptance
- accepted WP12 permanent regression evidence
- accepted WP13 architecture/documentation alignment evidence
- Release 1.6 GitHub planning/restoration/reconciliation authorities and accepted evidence
- current `README.md`
- current architecture/design/implementation documentation aligned by WP13
- current `docs/handbook/ENGINEERING_PLAYBOOK.md`
- this WP14 authority and its five-line companion

Do not reinterpret accepted Release 1.6 semantics.

---

## 3. Starting Gate

Before any GitHub lifecycle mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `main == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind: `0/0`;
- staged paths: 0;
- cumulative Release 1.6 candidate paths are expected and uncommitted;
- unexpected repository paths: 0;
- #182–#194: CLOSED / Done;
- #195: OPEN / Backlog;
- milestone #47: OPEN, 1 open / 13 closed;
- #182–#195 each exists exactly once in Project #2;
- predecessor Project Release-field restoration remains intact;
- schema implementation is v3;
- permanent baseline is 250/250;
- no Release 1.6 branch or PR exists;
- no Release 1.7 implementation/issues/branch/PR work exists.

Expected Release 1.6 governance/candidate artifacts are not blockers if they match the accepted planning state.

If any mandatory starting gate fails, stop before moving #195 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #195 `Backlog → In Progress`;
2. perform validation/reconciliation only;
3. if and only if every acceptance gate passes:
   - post concise final acceptance evidence to #195;
   - close #195;
   - set #195 `In Progress → Done`;
   - close milestone #47.

No issue beyond #195 may be created or mutated.

No Release 1.7 planning is authorized.

---

## 5. Zero-Development Rule

WP14 must not change repository content merely to make validation pass.

Not authorized:

- production code changes;
- permanent test changes;
- documentation changes;
- schema changes;
- migration changes;
- DI changes;
- Worker changes;
- package changes;
- project/reference changes;
- formatting sweeps;
- generated artifacts added to the repository;
- cleanup unrelated to disposable validation residue.

If validation exposes a defect, contradiction, stale material claim, missing permanent coverage, or integration problem, stop with #195 OPEN / In Progress and request narrow corrective authority.

---

## 6. Release 1.6 Capability Acceptance

Prove the implemented release remains exactly:

**Durable Experiment Evidence Foundation**

Accepted capability:

- accepted Release 1.5 Experiment Result evidence can be durably persisted;
- existing `aiq-experiment-identity-v1` is reused;
- no new persistence identity exists;
- exact typed Experiment Result Identity lookup exists;
- first acceptance produces `NewlyAccepted`;
- equivalent reacceptance produces `EquivalentExisting`;
- contradictory same-identity evidence produces `IntegrityConflict`;
- empty/non-empty evidence fidelity is preserved;
- restart-safe retrieval/persistence is supported;
- explicit one-shot Durable Experiment Worker mode exists;
- schema is v3.

No generalized registry/history/search capability may be present.

---

## 7. Explicit Exclusion Acceptance

Search repository/current-state docs and implementation evidence to confirm Release 1.6 did not introduce:

- Feature Set persistence;
- Feature Value persistence;
- generalized experiment registry;
- experiment history/search/comparison;
- update/delete semantics;
- overwrite/repair semantics;
- additional experiment families;
- strategies/signals;
- backtesting;
- scheduling/retries/checkpoints;
- provider acquisition orchestration;
- workspace/UI/API;
- AI/ML;
- Release 1.7 capability.

Material violations block acceptance.

---

## 8. Identity / Provenance Acceptance

Verify accepted identity/provenance semantics remain coherent across:

- Application contracts;
- use-case orchestration;
- Infrastructure persistence/retrieval;
- Worker output;
- architecture documentation;
- permanent tests.

Require:

- `aiq-experiment-identity-v1`;
- exact Experiment Definition binding;
- exact Feature Set binding;
- exact snapshot/version binding;
- dataset/source provenance preserved;
- operational metadata excluded from identity;
- no fabricated identity;
- no persistence-generated semantic identity.

---

## 9. Evidence Fidelity Acceptance

Verify permanent evidence protects:

- exact count;
- aggregate presence;
- exact decimal values;
- signed-zero behavior where applicable;
- empty result: count 0, aggregates absent;
- non-empty result: positive count, aggregates present;
- deterministic durable reconstruction.

Feature Values must remain unpersisted.

---

## 10. Schema-v3 Acceptance

Prove current implementation:

- `PRAGMA user_version = 3`;
- fresh bootstrap reaches v3;
- v2→v3 migration is atomic/non-destructive;
- predecessor observation/snapshot data is preserved;
- `experiment_results` exists exactly as accepted;
- no Feature Set persistence table exists;
- no registry/history table exists;
- no Experiment Result backfill occurs during migration;
- restrictive snapshot FK is preserved;
- unsupported future schema version remains >3.

Do not mutate a governed database.

Use disposable offline databases only.

---

## 11. Persistence Acceptance

Use permanent tests and, only if necessary, bounded disposable verification to prove:

### NewlyAccepted
- first acceptance persists exactly one logical row.

### EquivalentExisting
- equivalent reacceptance succeeds;
- no duplicate logical row;
- persisted evidence remains unchanged.

### IntegrityConflict
- contradictory evidence under same identity fails;
- no overwrite;
- no delete;
- no repair;
- no false `EquivalentExisting`.

Do not corrupt production/repository fixtures to manufacture additional proof if permanent evidence already establishes the case.

---

## 12. Retrieval Acceptance

Verify:

- exact typed identity retrieval;
- exact durable evidence reconstruction;
- exact missing identity → `NotFound`;
- read-only behavior;
- no provider fallback;
- no recomputation;
- no search/list/history behavior.

---

## 13. Failure-Model Acceptance

The bounded Release 1.6 vocabulary remains exactly:

1. `InvalidRequest`
2. `NotFound`
3. `DependencyUnavailable`
4. `InvalidEvidence`
5. `IntegrityConflict`

Require:

- `EquivalentExisting` remains success;
- unknown programming defects propagate;
- no broad exception normalization;
- no retry;
- no recovery;
- no repair;
- no fallback.

---

## 14. Application Boundary Acceptance

Verify Application owns:

- durable contracts;
- reduced durable evidence semantics;
- orchestration;
- semantic failure/outcome vocabulary.

Application must remain storage-independent.

No SQLite/SQL dependency may appear in Application.

---

## 15. Infrastructure Boundary Acceptance

Verify Infrastructure owns:

- SQLite schema/bootstrap/migration;
- Experiment Result persistence;
- exact retrieval;
- transaction/storage mechanics;
- storage validation/failure classification.

Infrastructure must not redefine experiment computation semantics.

---

## 16. Worker Boundary Acceptance

Verify Worker owns only:

- Durable Experiment configuration;
- intent detection;
- routing;
- one-shot execution;
- bounded presentation;
- exit code.

Required routing precedence:

`Durable Experiment → Experiment → Feature → five-stage pipeline`

Require:

- partial/malformed Durable intent fails;
- no fallback to lower mode;
- Worker does not implement direct SQL/store semantics.

---

## 17. DI Acceptance

Verify exactly once:

- `IDurableExperimentUseCase → DurableExperimentUseCase`;
- `IDurableExperimentEvidenceStore → SqliteExperimentResultStore`.

Require accepted lifetimes and side-effect-free graph resolution.

Predecessor registrations must remain intact.

---

## 18. Production Dependency Graph Acceptance

Require:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Also require:

- cycles: 0;
- unexpected production edges: 0;
- package delta from accepted baseline: 0;
- project delta: 0;
- reference delta: 0.

---

## 19. Predecessor Release Acceptance

Release 1.6 must preserve established behavior for:

- Release 1.1 observation persistence;
- Release 1.2 dataset/snapshot persistence;
- Release 1.3 five-stage pipeline;
- Release 1.4 Feature mode;
- Release 1.5 Experiment mode.

Durable Experiment must remain additive.

---

## 20. Permanent Test Inventory Acceptance

Expected permanent suite:

- Domain.Tests: 11
- Application.Tests: 111
- Infrastructure.Tests: 117
- Architecture.Tests: 13
- Total: 250
- Skipped: 0

Require exact inventory unless an already accepted corrective authority changed it after WP13.

No test may be added, removed, skipped, disabled, renamed to evade discovery, or weakened during WP14.

---

## 21. High-Value Permanent Coverage Read-Back

Confirm permanent tests cover, at minimum:

- Application durable orchestration;
- exact reduced evidence projection;
- `NewlyAccepted`;
- `EquivalentExisting`;
- bounded failure short-circuiting;
- unknown-defect propagation;
- schema-v3 persistence;
- exact retrieval;
- `IntegrityConflict`;
- `NotFound`;
- DI cardinality/resolution;
- Worker first-process `NewlyAccepted`;
- second-process `EquivalentExisting`;
- stable identity;
- partial Durable intent no-fallback;
- predecessor mode preservation.

Do not create new tests in WP14.

---

## 22. Process-Level Validation Methodology Acceptance

Verify `docs/handbook/ENGINEERING_PLAYBOOK.md` contains the accepted:

`### Process-Level Validation Prerequisites`

Confirm it establishes:

- repository-native fixture/seeding discovery during planning;
- explicit synthetic durable-state mechanism selection;
- cleanup/residue requirements;
- checkpoint retention decision;
- visibility-boundary protection;
- no production visibility weakening merely for validation.

No modification is authorized.

---

## 23. Documentation Acceptance

Read back the WP13-aligned current-state documentation.

Require no material contradiction across:

- `README.md`;
- data pipeline architecture;
- configuration model;
- module interactions;
- public contracts;
- dependency injection;
- observability model;
- testing strategy;
- Release 1.6 data architecture documents.

Historical Release 1.1–1.5 statements may remain when explicitly historical.

No material current-state claim may incorrectly state schema v2, absence of Experiment Result persistence, absence of Durable Experiment mode, or 238 permanent tests.

---

## 24. Markdown / Repository Hygiene Acceptance

Require:

- modified/current Release 1.6 Markdown links valid;
- no malformed repository-relative links introduced;
- trailing whitespace findings: 0;
- required terminal newlines present;
- no conflict markers;
- no temporary probe source;
- no temporary project;
- no retained validation log;
- no accidental binary/database artifact.

---

## 25. Security Acceptance

Run canonical Gitleaks verification.

Also inspect cumulative Release 1.6 candidate paths for:

- real API keys;
- tokens;
- credentials;
- connection secrets;
- private material.

Require findings: 0.

Dummy/syntactic test values must not be real credentials.

---

## 26. Offline / Provider Isolation

WP14 validation must remain offline with respect to market-data providers.

Require:

- provider calls: 0;
- external network calls for product execution: 0;
- real credentials: 0.

GitHub read/write operations authorized by lifecycle governance are not product-provider activity.

---

## 27. Disposable Database / Process Validation

Prefer permanent tests as acceptance evidence.

Run additional disposable process/database probes only if required to resolve an acceptance ambiguity not already proven by WP11/WP12.

If used:

- construct state using repository-native mechanisms;
- use temporary paths outside governed repository content;
- do not weaken internal visibility;
- remove all DB/WAL/SHM/journal/process artifacts;
- residue after validation = 0.

Do not repeat WP11's exploratory external-probe failure pattern.

---

## 28. Git / Working-Tree Reconciliation

Before final acceptance record:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- tracked modifications;
- untracked paths;
- staged paths.

Classify every cumulative Release 1.6 candidate path against:

- definition;
- execution plan;
- file manifest;
- accepted corrective authorities;
- WP execution evidence.

Require:

- unexpected paths: 0;
- staged paths: 0;
- unrelated modifications: 0.

Do not stage or commit in WP14.

---

## 29. Cumulative File-Manifest Reconciliation

Perform exact cumulative Release 1.6 path accounting.

For every manifest-authorized path classify:

- created;
- modified;
- intentionally unchanged;
- not applicable under accepted corrective authority.

For every actual Release 1.6 candidate path identify its authority.

Require:

- unauthorized production path: 0;
- unauthorized test path: 0;
- unauthorized documentation path: 0;
- unexplained path: 0.

Historical/out-of-band governance artifacts must be classified separately and not silently treated as product implementation.

---

## 30. GitHub Issue Reconciliation

Read back #182–#195.

Require:

- exactly 14 WP issues;
- WP01–WP13 CLOSED before WP14 closure;
- #195 In Progress during validation;
- correct milestone #47 association;
- correct assignee/labels;
- exact linear dependency chain preserved;
- WP15+: 0;
- duplicate WP identities: 0.

Do not rewrite issue bodies.

---

## 31. Project #2 Reconciliation

Read back Release 1.6 Project items.

Require:

- #182–#195 membership: 14/14;
- duplicate Release 1.6 items: 0;
- Release = `1.6`: 14/14;
- Priority = `P1`: 14/14;
- authoritative Area: 14/14;
- #182–#194 = Done before #195 completion;
- #195 = In Progress during validation, then Done on success.

Also verify predecessor Release restoration remains intact:

- predecessor restored rows: 89/89 exact;
- predecessor Release-field drift: 0.

Do not mutate predecessor Project fields.

---

## 32. Milestone Acceptance

Before final closure require milestone #47:

- title: `Phase 4 - Release 1.6: Durable Experiment Evidence Foundation`;
- state: OPEN;
- #182–#195 only as Release 1.6 WP inventory;
- immediately before #195 closure: 1 open / 13 closed.

After successful #195 closure require:

- 0 open / 14 closed;
- then close milestone #47.

Do not change due date unless separately authorized.

---

## 33. Canonical Verification

Run:

`eng/verify.ps1 -Configuration Release`

Require:

- Restore: PASS;
- Formatting: PASS;
- Gitleaks: PASS;
- Release build: PASS;
- warnings: 0;
- errors: 0;
- Domain.Tests: 11/11;
- Application.Tests: 111/111;
- Infrastructure.Tests: 117/117;
- Architecture.Tests: 13/13;
- total: 250/250;
- skipped: 0.

A failure blocks acceptance.

Do not fix it under WP14 unless a separate corrective authority is issued.

---

## 34. Additional Repository Gates

Require:

- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- direct whitespace checks for expected untracked candidate/governance files: PASS;
- final-newline checks: PASS;
- conflict-marker scan: PASS;
- Markdown link validation: PASS;
- database/WAL/SHM/journal residue: 0;
- temporary validation project/probe residue: 0;
- staged paths: 0.

---

## 35. No Release 1.7 Work

Search repository and GitHub planning state for premature Release 1.7 execution.

Require:

- Release 1.7 implementation: 0;
- Release 1.7 WP issues: 0;
- Release 1.7 branch: 0;
- Release 1.7 PR: 0.

Historical roadmap mentions are allowed.

Do not create Release 1.7 planning artifacts.

---

## 36. Integration Readiness Decision

After all gates, explicitly decide:

`Release 1.6 integration-ready: YES/NO`

YES requires every mandatory gate to pass.

NO requires:

- #195 remains OPEN / In Progress;
- milestone #47 remains OPEN;
- no integration/transport mutation;
- exact blocker;
- smallest corrective authority required.

Do not use conditional acceptance.

---

## 37. Git Integration Boundary

WP14 does **not** authorize:

- `git add`;
- commit;
- branch creation;
- push;
- PR creation;
- merge;
- tag;
- GitHub Release.

"Integration & Acceptance" means proving the cumulative candidate is ready for the separately authorized integration step.

Do not infer Git transport authority from the WP title.

---

## 38. Successful GitHub Completion

Only after `Release 1.6 integration-ready: YES`:

1. post final evidence to #195;
2. close #195;
3. set #195 Project Status → Done;
4. verify #182–#195 = CLOSED / Done;
5. verify milestone #47 = 0 open / 14 closed;
6. close milestone #47;
7. read back final Project/milestone state.

No other GitHub mutation is authorized.

---

## 39. Final Repository State

On success require:

- repository content remains cumulative accepted Release 1.6 candidate state;
- WP14 repository content delta: 0;
- staged paths: 0;
- no commit/branch/push/PR/tag/release;
- schema v3;
- 250/250 tests;
- production graph unchanged;
- packages/projects/references unchanged;
- residue 0;
- provider/network execution 0;
- Release 1.7 work 0.

---

## 40. Stop Conditions

Stop with:

`RELEASE 1.6 WP14 BLOCKED`

if any of the following occurs:

- unexpected repository path;
- manifest reconciliation ambiguity;
- material documentation/implementation contradiction;
- test failure;
- build warning/error;
- formatting/Gitleaks failure;
- schema mismatch;
- architecture drift;
- package/project/reference drift;
- provider/network execution;
- residue cannot be removed;
- GitHub issue/Project/milestone drift cannot be safely reconciled under this authority;
- predecessor Release-field restoration is no longer exact;
- Release 1.7 work is detected;
- any corrective repository mutation would be required.

Report the smallest corrective authority required.

Do not close #195 or milestone #47.

---

## 41. Completion Evidence

Post concise #195 evidence including:

- baseline HEAD/origin/ahead-behind;
- cumulative path reconciliation;
- capability/exclusion acceptance;
- identity/provenance/fidelity acceptance;
- schema-v3/migration acceptance;
- persistence/retrieval/failure acceptance;
- Application/Infrastructure/Worker/DI boundary acceptance;
- predecessor preservation;
- permanent suite 250/250;
- architecture graph;
- documentation/playbook alignment;
- security/offline/residue results;
- Project #2 reconciliation;
- predecessor Release restoration 89/89;
- Release 1.7 absence;
- integration-ready decision;
- statement that WP14 repository content delta = 0.

Keep the GitHub comment concise.

---

## 42. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting repository/GitHub state;
4. cumulative manifest/path reconciliation;
5. capability acceptance;
6. explicit exclusions;
7. identity/provenance/fidelity acceptance;
8. schema-v3/bootstrap/migration acceptance;
9. persistence acceptance;
10. retrieval acceptance;
11. failure-model acceptance;
12. Application boundary;
13. Infrastructure boundary;
14. Worker/configuration/routing boundary;
15. DI acceptance;
16. dependency graph/package/project/reference acceptance;
17. predecessor Release 1.1–1.5 preservation;
18. permanent test inventory and high-value coverage;
19. process-level validation methodology;
20. documentation alignment;
21. Markdown/hygiene/security checks;
22. offline/provider isolation;
23. disposable validation/residue accounting;
24. Git working-tree accounting;
25. GitHub issue reconciliation;
26. Project #2 reconciliation;
27. predecessor Release-field restoration;
28. milestone reconciliation;
29. Release 1.7 absence;
30. canonical verification;
31. WP14 mutation accounting;
32. integration-readiness decision;
33. final GitHub lifecycle;
34. blockers/findings;
35. next authorized action.

---

## 43. Completion Marker

On full success end exactly:

`RELEASE 1.6 WP14 COMPLETE`

Then:

`RELEASE 1.6 ACCEPTED FOR INTEGRATION`

Then:

`NEXT AUTHORIZED ACTION: Human authorization of the separate Release 1.6 Git integration / commit / push / PR workflow.`

Required final GitHub state:

- #182–#195: CLOSED / Done
- milestone #47: CLOSED, 0 open / 14 closed
- Release 1.6 integration-ready: YES

If blocked end exactly:

`RELEASE 1.6 WP14 BLOCKED`

and identify the smallest corrective authority required.

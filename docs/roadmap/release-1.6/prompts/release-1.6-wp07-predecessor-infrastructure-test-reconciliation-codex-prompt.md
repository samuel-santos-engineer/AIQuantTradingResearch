# Release 1.6 WP07 Predecessor Infrastructure Test Reconciliation Authority

## 1. Purpose

This prompt is the sole corrective authority for the blocked execution of:

**Release 1.6 WP07 — Experiment Result Persistence — GitHub issue #188**

WP07 correctly implemented the accepted Release 1.6 schema-v3 persistence boundary, but canonical verification now fails because existing predecessor Infrastructure tests still assert the historical schema-v2/no-Experiment-Result-table state.

This corrective authority authorizes only the minimum permanent predecessor test expectation updates needed to reconcile those tests with the already-accepted Release 1.6 WP06 schema-v3 model and the uncommitted WP07 implementation.

It does not authorize new Release 1.6 test families, production redesign, retrieval implementation, storage-failure redesign, DI, Worker behavior, packages, references, or Release 1.7 work.

Issue #188 must remain OPEN / In Progress during this corrective run.

---

## 2. Authoritative Blocked State

Reconcile rather than assume.

Expected state:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- authoritative predecessor baseline remains `18dfb01bf3503d91415b081b11fcdd7249094373`;
- cumulative Release 1.6 work is uncommitted/un-staged;
- #182–#187: CLOSED / Done;
- #188: OPEN / In Progress;
- #189–#195: OPEN / Backlog;
- milestone #47: OPEN;
- staged paths: 0;
- WP07 production changes remain present and uncommitted;
- temporary WP07 probe has been removed;
- residue: 0;
- no DI, Worker, package, reference, provider, or Release 1.7 changes.

If this state materially differs, stop before test mutation.

---

## 3. Governing Authorities

Read completely:

- Release 1.6 definition;
- Release 1.6 execution plan;
- Release 1.6 file manifest;
- `DURABLE_EXPERIMENT_EVIDENCE.md`;
- `EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`;
- `EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`;
- WP04 Application contracts;
- WP05 durable use-case implementation;
- WP07 full authority;
- WP07 blocked execution report;
- this corrective authority and its five-line companion.

WP06 physical-model authority and the already-implemented WP07 schema-v3 behavior govern the corrected test expectations.

---

## 4. Exact Authorized Test Files

This authority permits modifications only to these existing predecessor Infrastructure test files:

- `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/SqliteDatasetTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/FeatureCompositionTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentCompositionTests.cs`

No other permanent test file may change.

The already-authorized unsupported-future-schema fixture adjustment from v3 to v4 remains part of WP07 and is not broadened by this corrective authority.

---

## 5. Exact Correction Scope

Update only assertions/fixtures whose expected state became obsolete solely because Release 1.6 now intentionally implements schema v3 and exactly one `experiment_results` table.

Permitted expectation reconciliation includes:

- expected schema version:
  - historical `2` → accepted current `3`;
- assertions that explicitly require absence of Experiment Result persistence:
  - replace only with the accepted Release 1.6 expectation that the schema contains exactly the WP06-defined `experiment_results` table;
- table-count/schema-object expectations that must increase only because that one accepted table now exists;
- fresh-bootstrap expectations now targeting schema v3;
- existing composition/process assertions that inspect database schema and previously required “no experiment persistence table”.

Do not change assertions unrelated to the schema-v3 transition.

---

## 6. Preserve Non-Schema Assertions

All predecessor behavioral assertions must remain intact.

Do not weaken or remove coverage for:

- Release 1.1 observation persistence;
- idempotency;
- fidelity;
- atomicity;
- retrieval;
- connection ownership;
- Release 1.2 dataset/snapshot semantics;
- exact snapshot lookup;
- migration preservation;
- Release 1.4 Feature behavior;
- Release 1.4 Feature composition/Worker behavior;
- Release 1.5 Experiment composition/Worker behavior;
- provider/network isolation;
- side-effect-free DI resolution;
- no Feature Set persistence;
- no provider fallback;
- residue cleanup;
- architecture/package/reference invariants.

The only semantic expectation being updated is that schema v3 and one durable Experiment Result table are now accepted current state.

---

## 7. Preserve Test Count

This authority does not permit adding or deleting permanent tests.

Required test-count delta:

- Domain: 0
- Application: 0
- Infrastructure: 0
- Architecture: 0
- Total: 0

Expected permanent total remains:

`238`

Do not split, merge, add, remove, skip, or disable tests.

Do not change `[Fact]`, `[Theory]`, data rows, traits, collections, skip metadata, or test discovery semantics unless a purely mechanical version expectation is embedded in existing test data and the test count remains exactly unchanged.

---

## 8. No Weakening by Broadening Assertions

Do not replace exact assertions with weaker generic assertions merely to make tests pass.

Examples of prohibited weakening:

- “schema version >= 2” instead of exact v3;
- “table exists somewhere” instead of exact accepted table name;
- removing table-count assertions without replacement;
- ignoring extra tables;
- skipping tests;
- catching failures in test code;
- reducing exact equality to non-null checks.

Where an assertion changes, the new expectation must be at least as precise under the new accepted schema.

---

## 9. Exact Experiment Table Expectation

Where predecessor tests inspect schema objects, reconcile them to the accepted WP06 model:

- exactly one Release 1.6 Experiment Result persistence table:
  `experiment_results`;
- no Feature Set persistence table;
- no experiment registry/history/search table;
- no additional Release 1.7+ table.

If a test can cheaply assert this exact boundary without expanding its original purpose, preserve that precision.

Do not duplicate WP12’s future comprehensive schema tests.

---

## 10. Fresh Database Expectations

Existing tests that initialize a fresh database must now expect:

- complete predecessor schema;
- schema version 3;
- exactly the accepted `experiment_results` table added;
- no backfilled Experiment Result rows unless the test explicitly persisted one through WP07 behavior;
- no Feature Set persistence.

Do not change unrelated fresh-bootstrap expectations.

---

## 11. Migration Expectations

Existing migration tests must preserve predecessor data assertions while updating target schema state to v3.

Require:

- predecessor observation/snapshot evidence unchanged;
- target version 3;
- accepted Experiment Result table available after successful migration;
- no synthetic Experiment Result rows;
- no Feature Set backfill.

Do not add a new migration test family under this authority.

---

## 12. Composition Test Expectations

`FeatureCompositionTests.cs` and `ExperimentCompositionTests.cs` previously asserted absence of experiment persistence as a predecessor guarantee.

Reconcile only the schema-observation portions that are now obsolete.

Preserve their principal responsibilities:

- DI registration/lifetime behavior;
- resolution side effects;
- Worker/process behavior;
- predecessor feature/experiment routing;
- provider isolation;
- cleanup.

Do not turn these files into Release 1.6 persistence semantic tests.

WP12 remains responsible for comprehensive permanent Release 1.6 persistence validation.

---

## 13. No Production Mutation

This corrective run authorizes no additional production change.

Do not modify:

- Application;
- Infrastructure production;
- schema/migration implementation;
- Worker;
- Domain;
- DI;
- packages/projects/references.

The uncommitted WP07 production changes are preserved unchanged.

If a corrected predecessor test exposes a genuine WP07 production defect, stop and report it. Do not repair production under this corrective authority.

---

## 14. No WP08/WP09 Leakage

Do not use test reconciliation to implement or force:

- public exact retrieval behavior owned by WP08;
- broader storage failure mapping owned by WP09;
- NotFound retrieval semantics beyond predecessor tests;
- storage corruption classification;
- retries/repair/fallback.

Only update historical schema expectations.

---

## 15. Direct Diff Review

After mutation, inspect the diff for each of the four authorized files.

For each file report:

- exact assertions/fixtures changed;
- old expectation;
- new expectation;
- why the change is strictly caused by accepted schema v3;
- non-schema assertions changed: 0.

Required:

`Unauthorized test-file paths = 0`

---

## 16. Targeted Validation

Run the affected Infrastructure tests first.

At minimum execute the tests from:

- `SqlitePersistenceTests.cs`
- `SqliteDatasetTests.cs`
- `FeatureCompositionTests.cs`
- `ExperimentCompositionTests.cs`

Require all affected tests to pass.

Do not treat a passing subset as completion if another corrected file fails.

---

## 17. Canonical Validation

After targeted tests pass, run:

`eng/verify.ps1 -Configuration Release`

Require:

- Domain.Tests: 11/11
- Application.Tests: 102/102
- Infrastructure.Tests: 112/112
- Architecture.Tests: 13/13
- Total: 238/238
- Skipped: 0
- Build warnings/errors: 0/0
- Formatting: PASS
- Gitleaks: PASS

Also require:

- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- direct expected-untracked whitespace/final-newline checks: PASS;
- staged paths: 0;
- package/project/reference delta: 0/0/0;
- production graph unchanged;
- schema implementation remains the current uncommitted WP07 v3 implementation;
- residue: 0;
- provider/network execution: 0.

---

## 18. GitHub Mutation Budget

GitHub mutations authorized by this corrective run:

`0`

Preserve:

- #188 OPEN / In Progress;
- #189–#195 OPEN / Backlog;
- milestone #47 OPEN.

Do not post completion evidence or close #188 under this corrective authority.

WP07 must be resumed under its existing authority after reconciliation succeeds.

---

## 19. Repository Mutation Budget

Authorized mutation:

- schema-v3 expectation reconciliation in exactly the four named predecessor Infrastructure test files.

Not authorized:

- production;
- new tests;
- deleted tests;
- renamed tests;
- test-count change;
- staging;
- commit;
- branch;
- push;
- PR;
- governance artifact changes;
- Release 1.7 work.

---

## 20. Resume Boundary

This corrective authority does not complete WP07.

On successful reconciliation:

- leave #188 OPEN / In Progress;
- preserve all uncommitted WP07 production changes;
- return to the existing `07-experiment-result-persistence-codex-prompt.md`;
- resume WP07 from the targeted/canonical validation gate;
- rerun full WP07 structural acceptance before closing #188.

Do not jump directly to issue closure based only on this corrective run.

---

## 21. Stop Conditions

Stop if:

- any affected predecessor test fails for a reason other than obsolete schema-v2/no-experiment-table expectation;
- production changes would be required;
- test count would need to change;
- a fifth permanent test file would need modification;
- an assertion unrelated to schema transition would need weakening;
- WP06 physical model and WP07 implementation differ materially;
- canonical validation exposes a genuine production defect;
- provider/network activity or residue appears;
- Release 1.7 work exists.

Report the smallest corrective authority required.

---

## 22. Required Execution Report

Report:

1. executive summary;
2. starting state;
3. authorities reviewed;
4. four authorized files;
5. exact affected test/assertion inventory;
6. old schema expectations;
7. new schema-v3 expectations;
8. test-count preservation;
9. non-schema assertion preservation;
10. exact `experiment_results` expectation;
11. fresh-bootstrap reconciliation;
12. migration reconciliation;
13. FeatureComposition reconciliation;
14. ExperimentComposition reconciliation;
15. production mutation count;
16. targeted test results;
17. canonical 238/238 validation;
18. formatting/Gitleaks/diff/whitespace;
19. package/project/reference/graph checks;
20. residue/provider isolation;
21. repository mutation accounting;
22. GitHub mutation accounting;
23. findings/blockers;
24. WP07 resume state.

---

## 23. Completion Marker

On success end exactly:

`RELEASE 1.6 WP07 PREDECESSOR INFRASTRUCTURE TEST RECONCILIATION COMPLETE`

Then:

`NEXT AUTHORIZED ACTION: Resume WP07 — Experiment Result Persistence from the validation gate. Issue #188 remains OPEN / In Progress.`

If blocked end:

`RELEASE 1.6 WP07 PREDECESSOR INFRASTRUCTURE TEST RECONCILIATION BLOCKED`

and identify the smallest corrective authority required.

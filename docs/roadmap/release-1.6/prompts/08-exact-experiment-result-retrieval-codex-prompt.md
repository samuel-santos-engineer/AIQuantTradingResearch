# Release 1.6 WP08 — Exact Experiment Result Retrieval — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP08 — Exact Experiment Result Retrieval — GitHub issue #189**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP08 implements the minimum exact durable Experiment Result retrieval capability over the schema-v3 `experiment_results` representation accepted and implemented by WP07.

The retrieval boundary is:

`exact ExperimentResultIdentity → durable lookup → complete evidence reconstruction/validation → Found or NotFound`

WP08 is read-only. It must not accept, insert, update, delete, overwrite, recompute, regenerate, backfill, search, list, or otherwise mutate Experiment Result evidence.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_SCHEMA_V3.md`
- Release 1.5 Experiment identity/provenance authorities
- current `ExperimentPersistenceContracts.cs`
- current `DurableExperimentUseCase.cs`
- current WP07 schema-v3 and Experiment Result persistence implementation
- accepted WP07 predecessor Infrastructure test reconciliation evidence
- existing SQLite exact-retrieval conventions from predecessor releases
- this WP08 authority and its five-line companion

WP02/WP03 own durable evidence semantics.
WP06 owns the physical schema-v3 model.
WP07 owns persistence/acceptance and schema implementation.
Do not reopen those decisions.

---

## 3. Starting Gate

Before mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- Release 1.5 authoritative baseline remains `18dfb01bf3503d91415b081b11fcdd7249094373`;
- cumulative Release 1.6 work remains expected and uncommitted/un-staged;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182–#188: CLOSED / Done;
- #189: OPEN / Backlog;
- #190–#195: OPEN / Backlog;
- milestone #47: OPEN, 7 open / 7 closed;
- Project #2 fields remain correct;
- predecessor Project Release restoration remains preserved;
- schema v3 is the implemented current schema;
- `experiment_results` exists exactly as accepted by WP06/WP07;
- WP07 canonical verification is 238/238;
- no premature WP09+ implementation exists;
- no Release 1.7 work exists.

Expected Release 1.6 governance/candidate paths are not blockers.

If a mandatory gate fails, stop before moving #189 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #189 `Backlog → In Progress`;
2. implement only WP08;
3. validate;
4. post concise completion evidence to #189;
5. close #189;
6. set #189 `In Progress → Done`.

Required final lifecycle:

- #182–#189: CLOSED / Done;
- #190–#195: OPEN / Backlog;
- milestone #47: OPEN, 6 open / 8 closed.

No other GitHub mutation is authorized.

---

## 5. Manifest Is Binding

Use `RELEASE_1.6_FILE_MANIFEST.md` as exact path authority.

Modify/create only the WP08 paths authorized by that manifest.

Expected scope is the minimum Infrastructure/Application seam required to fulfill the already-defined exact retrieval contract.

Do not create convenience repositories, query services, generic data-access abstractions, new projects, packages, or permanent WP12 test suites.

If the required implementation cannot fit the manifest-authorized surface, stop.

---

## 6. Exact Retrieval Contract

Implement the existing Release 1.6 exact retrieval contract, not a new API family.

Input:

- exact typed `ExperimentResultIdentity`.

Successful found result:

- one immutable reduced durable Experiment Result evidence object representing exactly the persisted semantic evidence.

Missing result:

- bounded `NotFound`.

The operation must not accept approximate identity, prefix, case-insensitive alternatives, aliases, semantic search, or alternate lookup keys.

---

## 7. Read-Only Semantics

WP08 retrieval must be observational only.

During retrieval do not:

- insert;
- update;
- delete;
- overwrite;
- accept evidence;
- run equivalence acceptance;
- create a new result identity;
- run experiment generation;
- run Feature generation;
- query providers;
- backfill;
- repair;
- migrate data as a side effect of the retrieval operation.

Normal database bootstrap/schema validation required by existing Infrastructure connection conventions is not semantic Experiment Result mutation and must remain bounded to established behavior.

---

## 8. Exact Identity Lookup

Use the existing typed Experiment Result identity and exact schema-v3 primary-key representation.

Require:

- exact 64-character lowercase fingerprint semantics remain enforced by the existing identity model;
- lookup uses the exact stored result fingerprint;
- at most one row can match;
- no surrogate key;
- no row ID;
- no alternate persistence identity;
- no definition-only or Feature-Set-only retrieval.

Do not introduce another identity scheme.

---

## 9. Complete Durable Evidence Reconstruction

A found row must reconstruct the complete immutable durable evidence represented by WP04/WP06/WP07.

Preserve exactly the accepted fields, including as applicable:

- Experiment Result identity;
- Experiment Definition identity;
- built-in experiment definition/name;
- Feature Set identity;
- Feature Definition identity;
- Dataset Snapshot identity;
- Dataset Definition identity;
- Research Dataset identity;
- Source State identity;
- source authority/reference;
- dataset observation count;
- summary count;
- aggregate-presence state;
- mean;
- minimum;
- maximum;
- provenance;
- lineage.

Do not fabricate Feature Values or source observations.

Do not reconstruct the full Release 1.5 in-memory Feature Set object graph.

---

## 10. Identity / Evidence Validation

A database row is not trusted merely because it exists.

Before returning Found evidence, validate enough to prove that the persisted representation is coherent with accepted durable semantics.

Require:

- persisted Experiment Result identity is syntactically valid;
- all persisted predecessor identities are valid;
- count/presence/aggregate invariants hold;
- canonical decimal text parses exactly;
- provenance/reference fields are complete and coherent;
- no partial durable evidence is returned.

Where the accepted Application/Infrastructure contract already defines validation ownership, preserve it.

Do not broaden into generalized WP09 storage validation.

---

## 11. Canonical Identity Integrity

For a found row, verify that the reconstructed semantic evidence remains bound to the persisted Experiment Result identity according to the accepted `aiq-experiment-identity-v1` result identity semantics wherever the existing contracts provide enough information to do so.

If the stored identity and complete reconstructed evidence contradict one another:

- do not return Found;
- surface the accepted integrity contradiction boundary available to WP08;
- do not repair or rewrite the row.

Do not invent a new identity.

If the current contract cannot express the required contradiction without WP09 changes, stop and report the smallest authority required rather than broadening the failure model.

---

## 12. Exact Decimal Reconstruction

Read WP06/WP07 canonical decimal representation exactly.

Require:

- no SQLite REAL conversion;
- invariant parsing;
- exact sign/coefficient/scale interpretation;
- exact .NET decimal reconstruction;
- no rounding;
- no culture dependence;
- signed-zero semantics preserved to the extent represented by the durable evidence contract and canonical identity machinery.

Malformed canonical decimal storage must not silently normalize into valid evidence.

---

## 13. Empty Result Fidelity

For a valid empty Experiment Result:

- count = 0;
- aggregate presence = absent;
- mean/minimum/maximum remain absent;
- retrieval succeeds;
- exact provenance/identity evidence remains present.

Do not substitute numeric zero aggregates.

---

## 14. Non-Empty Result Fidelity

For valid non-empty Experiment Result evidence:

- count > 0;
- aggregate presence = present;
- mean/minimum/maximum all reconstruct exactly;
- persisted canonical decimal semantics are preserved.

Partial aggregate states must never be returned as valid Found evidence.

---

## 15. NotFound Semantics

For a valid exact Experiment Result identity with no persisted row:

- return the accepted bounded `NotFound` retrieval result/failure;
- do not query any other identity;
- do not generate/recompute the Experiment Result;
- do not invoke the Release 1.5 experiment-generation use case;
- do not invoke the WP05 durable acceptance use case;
- do not call providers;
- do not return an empty/sentinel result.

NotFound is a successful bounded lookup outcome, not permission for fallback.

---

## 16. Persistence Boundary Preservation

WP07 acceptance semantics remain unchanged:

- `NewlyAccepted`;
- `EquivalentExisting`;
- `IntegrityConflict`.

WP08 must not modify acceptance behavior.

Internal reuse of common row mapping is permitted only if manifest-authorized and does not alter WP07 semantics.

Do not combine acceptance and retrieval into generic CRUD.

---

## 17. Schema Boundary Preservation

WP08 must not change the accepted physical schema.

Require:

- `PRAGMA user_version = 3`;
- `experiment_results` unchanged;
- no new table;
- no new column;
- no new index;
- no migration;
- no Feature Set persistence;
- no registry/history/search schema.

If retrieval requires a schema change, stop.

---

## 18. WP09 Failure-Mapping Boundary

WP09 owns broader Storage Validation & Failure Mapping.

WP08 may implement only failure handling strictly necessary to fulfill the already-defined retrieval contract.

Do not:

- introduce new public failure values;
- broadly catch/normalize SQLite exceptions;
- classify every corruption mode;
- implement retry;
- implement repair;
- implement fallback;
- hide unknown defects.

Unknown programming defects must continue to propagate.

If an expected storage condition cannot be represented without WP09 design, stop rather than preempt WP09.

---

## 19. Connection Ownership

Reuse existing Infrastructure connection/path conventions.

Require:

- no connection leakage;
- no Application-owned SQLite connection;
- no Worker-owned retrieval transaction;
- no long-lived mutable retrieval state;
- deterministic disposal.

A simple exact read should not introduce unnecessary write transactions.

---

## 20. Concurrency

Retrieval must remain correct if another process has already durably accepted the row.

Do not add locks/retries beyond existing SQLite/concurrency conventions.

The primary-key uniqueness and committed-state behavior established by WP07 remain authoritative.

Do not return partially committed evidence.

---

## 21. Provider / Network Isolation

Exact durable retrieval is offline.

Require:

- provider calls: 0;
- network calls: 0;
- real credentials: 0;
- Feature generation calls: 0;
- Experiment generation calls: 0.

No provider fallback is authorized for NotFound or invalid stored evidence.

---

## 22. No DI / Configuration / Worker Changes

WP10 owns Dependency Registration & Configuration.
WP11 owns the one-shot Durable Experiment Worker.

WP08 must not modify:

- DI registration;
- Worker configuration;
- Worker routing;
- `Program.cs`;
- Worker output;
- exit codes.

Use direct construction in temporary probes if necessary.

---

## 23. Permanent Test Boundary

WP12 owns the Release 1.6 permanent persistence/retrieval test suite.

WP08 must not add a new permanent test file or expand permanent test count.

Existing tests may be modified only if the Release 1.6 manifest explicitly assigns an unavoidable compatibility update to WP08. Otherwise stop.

Expected permanent count remains 238.

Use removable offline probes for WP08-specific proof.

---

## 24. Temporary Offline Proof

Use a removable offline probe if needed.

At minimum prove:

1. accept a non-empty durable Experiment Result using the WP07 store;
2. dispose/reopen as appropriate;
3. exact lookup returns complete equivalent durable evidence;
4. exact identity is preserved;
5. canonical decimals round-trip exactly;
6. provenance/lineage/reference evidence is preserved;
7. empty evidence round-trips with aggregates absent;
8. valid missing identity returns NotFound;
9. no Feature/Experiment generation occurs during retrieval;
10. no provider/network activity occurs;
11. retrieval does not add/update/delete rows;
12. no database/WAL/SHM/journal/probe residue remains.

Where practical, prove a contradictory/malformed stored row is not returned as valid evidence without broadening WP09.

Remove all temporary probe artifacts before canonical validation.

---

## 25. Restart-Safe Retrieval

The release definition requires durable evidence to survive process/database reopening.

WP08 must prove retrieval from committed schema-v3 storage after reopening the relevant connection/process boundary.

Do not satisfy restart safety solely by reading from the same in-memory object used for acceptance.

No Worker process test is required here unless explicitly manifest-authorized; a direct Infrastructure reopen is sufficient for WP08 if it proves durability.

---

## 26. Equivalence of Retrieved Evidence

For evidence accepted by WP07 and retrieved by WP08, compare the complete durable semantic evidence, not merely the result identity.

Require equivalence of:

- identities;
- provenance/reference fields;
- count;
- presence;
- aggregates;
- decimal canonical meaning.

No Feature Values are expected.

---

## 27. Predecessor Preservation

All Releases 1.1–1.5 behavior must remain unchanged.

Preserve:

- observation persistence;
- dataset/snapshot persistence/retrieval;
- schema migration behavior;
- Release 1.3 pipeline;
- Release 1.4 Feature behavior;
- Release 1.5 Experiment generation;
- WP07 acceptance/persistence.

No predecessor data rewrite.

---

## 28. Architecture / Package / Reference Preservation

Production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Require:

- cycles: 0;
- unexpected edges: 0;
- package delta: 0;
- project delta: 0;
- reference delta: 0.

No new package/project/reference.

---

## 29. Targeted Validation

Before canonical verification, run targeted offline proof of the WP08 retrieval behavior.

Evidence should explicitly distinguish:

- Found;
- NotFound;
- invalid/integrity contradiction if applicable under existing contract;
- no writes;
- no generation/provider fallback;
- exact round-trip fidelity;
- restart-safe retrieval.

Remove temporary probes afterward.

---

## 30. Canonical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Expected permanent counts:

- Domain.Tests: 11/11
- Application.Tests: 102/102
- Infrastructure.Tests: 112/112
- Architecture.Tests: 13/13
- Total: 238/238
- Skipped: 0

Require:

- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- direct expected-untracked whitespace/final-newline checks: PASS;
- staged paths: 0;
- database/WAL/SHM/journal/probe residue: 0;
- provider/network activity: 0;
- real credentials: 0;
- schema remains v3.

---

## 31. Structural Acceptance

Require:

- exact manifest-authorized WP08 production path delta only;
- public exact durable retrieval implemented;
- no schema delta;
- no acceptance semantic delta;
- no DI/configuration delta;
- no Worker delta;
- no new permanent test suite;
- no package/project/reference delta;
- no WP09+ implementation;
- no Release 1.7 work.

---

## 32. Mutation Budget

Authorized repository mutations:

- exact WP08 manifest-authorized retrieval implementation path(s), and only any minimal contract compatibility path explicitly allowed by the manifest.

Authorized GitHub mutations:

1. #189 Backlog → In Progress;
2. completion evidence comment;
3. close #189;
4. #189 In Progress → Done.

Not authorized:

- staging;
- commit;
- branch;
- push;
- PR;
- tag/release;
- milestone closure;
- #190–#195 mutation;
- schema change;
- DI/Worker;
- new permanent test suite;
- Release 1.7 work.

---

## 33. Stop Conditions

Stop with #189 OPEN / In Progress if:

- manifest authority is ambiguous;
- retrieval requires schema change;
- retrieval requires Feature Set persistence;
- retrieval requires Feature/Experiment recomputation;
- exact durable evidence cannot be reconstructed from WP07 storage;
- identity contradiction cannot be represented without preempting WP09;
- a new public failure vocabulary is required;
- production changes outside manifest are required;
- a new permanent test suite is required;
- package/project/reference change is required;
- canonical verification fails;
- provider/network fallback occurs;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

---

## 34. Completion Evidence

Post concise #189 evidence including:

- exact changed paths;
- exact typed Experiment Result lookup;
- Found behavior;
- NotFound behavior;
- complete durable evidence reconstruction;
- identity integrity;
- decimal fidelity;
- empty/non-empty fidelity;
- provenance/lineage/reference fidelity;
- restart-safe retrieval;
- no writes;
- no generation/provider fallback;
- schema remains v3;
- no WP09 broad mapping;
- no DI/Worker/new permanent test suite;
- canonical 238/238;
- next WP09/#190.

---

## 35. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact changed paths;
5. retrieval contract implementation;
6. exact identity lookup;
7. complete evidence reconstruction;
8. identity/evidence validation;
9. canonical identity integrity;
10. decimal reconstruction;
11. empty fidelity;
12. non-empty fidelity;
13. NotFound;
14. read-only proof;
15. WP07 acceptance preservation;
16. schema preservation;
17. WP09 boundary preservation;
18. connection/concurrency behavior;
19. provider/network isolation;
20. restart-safe retrieval;
21. temporary probe evidence;
22. predecessor regression;
23. canonical validation;
24. whitespace/security/residue;
25. architecture/package/reference checks;
26. repository mutation accounting;
27. GitHub lifecycle;
28. findings/blockers;
29. next authorized WP.

---

## 36. Completion Marker

On success end exactly:

`RELEASE 1.6 WP08 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP09 — Storage Validation & Failure Mapping — GitHub issue #190`

Required final lifecycle:

- #182–#189: CLOSED / Done
- #190–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked end:

`RELEASE 1.6 WP08 BLOCKED`

and identify the smallest corrective authority required.

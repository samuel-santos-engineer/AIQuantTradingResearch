# Release 1.6 WP04 — Application Persistence Contracts — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP04 — Application Persistence Contracts — GitHub issue #185**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP04 introduces the minimum Application-owned contracts and semantic types required to persist and retrieve accepted Release 1.5 Experiment Result evidence without leaking SQLite, schema, transaction, or Infrastructure mechanics into Application.

WP04 must not implement durable orchestration, SQLite schema v3, persistence/retrieval repositories, storage failure translation, DI, Worker behavior, or permanent tests.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- `docs/architecture/data/EXPERIMENT_PERSISTENCE_IDENTITY_PROVENANCE_FIDELITY.md`
- accepted Release 1.6 GitHub planning/restoration state
- accepted Release 1.6 definition-state reconciliation
- accepted WP01, WP02, and WP03 execution evidence
- current Release 1.5 Experiment contracts/models and identity implementation
- this WP04 authority and its five-line companion

Treat WP02 and WP03 as semantic authority. Do not weaken or reinterpret them.

---

## 3. Starting Gate

Before mutation verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind: `0/0`;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182–#184: CLOSED / Done;
- #185: OPEN / Backlog;
- #186–#195: OPEN / Backlog;
- milestone #47: OPEN, 11 open / 3 closed;
- Project #2 Release 1.6 fields remain correct;
- predecessor Release restoration remains 89/89 exact;
- implemented SQLite schema remains v2;
- no premature WP05+ implementation exists;
- no Release 1.7 work exists.

Expected untracked governed/out-of-band Release 1.6 artifacts are not blockers.

If a mandatory gate fails, stop before moving #185 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #185 `Backlog → In Progress`;
2. implement only WP04;
3. validate;
4. post concise completion evidence to #185;
5. close #185;
6. set #185 `In Progress → Done`.

Required final lifecycle:

- #182–#185: CLOSED / Done;
- #186–#195: OPEN / Backlog;
- milestone #47: OPEN, 10 open / 4 closed.

No other GitHub lifecycle mutation is authorized.

---

## 5. Manifest Is Binding

Use `RELEASE_1.6_FILE_MANIFEST.md` as the exact path authority.

Modify/create only the WP04 Application paths explicitly assigned there.

Do not invent substitute filenames, aliases, additional abstractions, helper files, DTO files, or convenience paths.

If the manifest cannot express the minimum coherent WP04 contract set, stop and report the smallest manifest corrective authority required.

---

## 6. Core Design Boundary

Application owns semantic persistence contracts.

Infrastructure will later implement those contracts.

Therefore WP04 contracts must describe:

- what accepted Experiment Result evidence is handed to durability;
- how acceptance outcomes are represented;
- how exact identity retrieval is requested;
- how successful retrieval returns complete durable semantic evidence;
- how bounded failures are represented at the Application boundary.

They must not describe:

- SQLite;
- SQL;
- tables;
- columns;
- indexes;
- migrations;
- connections;
- transactions;
- row IDs;
- storage DTOs;
- provider/network behavior.

---

## 7. Do Not Fabricate the Release 1.5 Object Graph

WP03 explicitly established that Feature Values remain unpersisted and persistence contracts must not fabricate the existing in-memory object graph.

Design the durable evidence contract around the complete semantic evidence actually required for persistence equivalence and reconstruction.

Do not require retrieval to recreate unavailable Feature Values merely to instantiate a Release 1.5 runtime object.

Do not weaken the evidence required by WP03.

The durable representation must remain semantically tied to the exact accepted Experiment Result identity, definition, Feature Set, snapshot/dataset/source provenance, count, aggregate presence, and decimal evidence required by the accepted authorities.

---

## 8. Persistence Acceptance Contract

Introduce the minimum Application-owned abstraction for accepting one complete valid durable Experiment Result evidence unit.

The contract must support semantic outcomes:

- `NewlyAccepted`;
- `EquivalentExisting`;
- bounded failure.

It must not expose storage implementation status such as inserted-row counts, SQL result codes, transaction handles, connection state, or database identifiers.

One invocation represents one atomic semantic acceptance request.

---

## 9. Exact Retrieval Contract

Introduce the minimum Application-owned abstraction for exact retrieval by typed `ExperimentResultIdentity`.

Requirements:

- exact identity only;
- no list/search/latest/history/query API;
- no definition lookup;
- no Feature Set lookup;
- no provider fallback;
- no recomputation;
- no fuzzy/equivalent aggregate lookup.

Success returns complete immutable durable Experiment Result evidence sufficient for WP03 semantic reconstruction/validation.

---

## 10. Durable Evidence Model

If the manifest authorizes a durable evidence model/type, it must contain only semantic evidence required by WP02/WP03.

Preserve, as required by accepted authorities:

- Experiment Result Identity;
- Experiment Definition identity/reference;
- exact Feature Set identity/reference;
- exact snapshot identity/version evidence;
- dataset/source provenance represented by the accepted result;
- count;
- aggregate-presence state;
- mean;
- minimum;
- maximum;
- lineage/provenance evidence required for fidelity.

Do not add operational metadata such as:

- persisted-at time;
- machine/process identity;
- row ID;
- retry count;
- storage version;
- provider-call metadata;
- mutable status.

---

## 11. Empty and Non-Empty States

Contract invariants must make valid states representable and contradictory states difficult or impossible to construct.

Successful empty evidence:

- count = 0;
- aggregates absent.

Successful non-empty evidence:

- accepted non-empty count;
- mean/minimum/maximum present exactly.

Do not use numeric zero, NaN, empty strings, magic decimals, or other sentinels for aggregate absence.

---

## 12. Decimal Semantics

Use `decimal` semantics only for aggregate evidence.

Do not introduce:

- `double`;
- `float`;
- culture-sensitive numeric strings as Application semantics;
- rounding;
- precision reduction.

Physical storage encoding remains WP06 authority.

---

## 13. Identity Semantics

Reuse existing typed Release 1.5 identity types and `aiq-experiment-identity-v1`.

Do not introduce:

- persistence identity;
- repository identity;
- row identity;
- new fingerprint scheme;
- new canonical encoding.

Contracts must preserve identity, not redefine it.

---

## 14. Acceptance Outcome

Represent `NewlyAccepted` and `EquivalentExisting` explicitly and unambiguously.

Do not represent them as:

- boolean where meaning is implicit;
- storage row count;
- nullable result;
- exception for equivalent existing evidence.

`EquivalentExisting` is successful idempotent acceptance, not failure.

---

## 15. Failure Vocabulary

Preserve the Release 1.6 bounded semantic vocabulary:

- `InvalidRequest`
- `NotFound`
- `DependencyUnavailable`
- `InvalidEvidence`
- `IntegrityConflict`

WP04 may introduce only the minimum contract types required by the manifest to carry these semantics.

Do not add SQLite-specific or repository-specific public failure values.

Unknown programming defects remain exceptions and propagate.

---

## 16. Responsibility Split

WP04 contracts must allow downstream responsibilities to remain clean:

### WP05
Orchestrates generation/acceptance/retrieval semantics.

### WP06
Defines SQLite schema-v3 physical representation.

### WP07
Implements acceptance/persistence.

### WP08
Implements exact retrieval.

### WP09
Implements storage validation and failure mapping.

Do not absorb those responsibilities into WP04.

---

## 17. No Persistence Implementation

WP04 must contain no:

- SQL;
- SQLite API usage;
- file/database creation;
- migration;
- transaction;
- serialization implementation tied to storage;
- repository implementation;
- storage exception mapping;
- retry/recovery.

Application project must remain independent of Infrastructure.

---

## 18. No Orchestration

Do not implement the durable experiment use case in WP04.

In particular, do not:

- invoke Release 1.5 experiment generation;
- call persistence after generation;
- retrieve results;
- choose acceptance/retrieval flow;
- map upstream generation failures;
- define Worker execution sequencing.

WP05 owns orchestration.

---

## 19. Immutability

All semantic evidence/result types introduced by WP04 must follow existing Application immutability conventions.

Require:

- constructor-established valid state;
- read-only semantic evidence;
- no public mutation;
- no partial initialization;
- no storage-owned mutable lifecycle.

Avoid general-purpose mutable DTO design.

---

## 20. Validation Boundary

WP04 may enforce intrinsic constructor invariants necessary to prevent impossible contract states.

Do not prematurely implement WP09 storage-validation policy.

Distinguish:

- type/model invariants needed for coherent objects;
- orchestration validation owned by WP05;
- storage reconstruction/integrity validation owned downstream.

---

## 21. Naming

Use repository vocabulary consistently:

- Experiment Result;
- Durable Experiment Evidence;
- Experiment Result Identity;
- NewlyAccepted;
- EquivalentExisting;
- IntegrityConflict.

Avoid generalized names such as:

- artifact repository;
- experiment registry;
- research store;
- history service;
- generic persistence framework.

Release 1.6 is intentionally narrow.

---

## 22. Dependency Rules

Production graph must remain:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

WP04 must add no project reference or package.

Application must not reference Infrastructure, Worker, SQLite, or provider-specific code.

---

## 23. Schema Boundary

Implemented schema must remain v2 after WP04.

No schema-v3 code, SQL, migration, schema constant update, table definition, or test fixture change is authorized.

WP06 owns physical schema v3.

---

## 24. Provider/Network Boundary

No provider or network behavior is authorized.

Do not:

- fetch missing evidence;
- reacquire Feature Sets;
- call Twelve Data;
- use credentials;
- introduce provider abstractions.

Durability operates on accepted evidence only.

---

## 25. Predecessor Preservation

Preserve Releases 1.1–1.5 unchanged except for the minimum Application contract delta explicitly authorized by the Release 1.6 manifest.

Particularly preserve:

- SQLite v2 behavior;
- dataset/snapshot semantics;
- Release 1.3 pipeline;
- Release 1.4 Feature generation;
- Release 1.5 in-memory Experiment generation and Worker behavior.

Existing Release 1.5 Experiment execution must not become implicitly durable.

---

## 26. Explicit Deferrals

Do not implement:

- Feature Set persistence;
- experiment registry/history;
- list/search/comparison;
- update/delete;
- additional experiments;
- strategies/signals/backtesting;
- scheduling/retries;
- provider acquisition;
- workspace/UI/API;
- AI/ML;
- Release 1.7 work.

---

## 27. Validation Strategy

Because permanent WP04 tests are not authorized, use only a removable offline probe if necessary to prove contract construction/invariants.

Any probe must:

- be offline;
- use no real credentials;
- perform no database/provider/network activity;
- be completely removed before completion;
- leave no package/project/reference delta;
- leave no generated residue.

Do not add permanent tests before WP12.

---

## 28. Canonical Verification

Run:

`eng/verify.ps1 -Configuration Release`

Expected permanent counts remain:

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
- real credentials: 0.

---

## 29. Structural Acceptance

Verify after implementation:

- only manifest-authorized WP04 production paths changed;
- Domain delta: 0 unless manifest explicitly authorizes otherwise;
- Infrastructure delta: 0;
- Worker delta: 0;
- permanent-test delta: 0;
- package delta: 0;
- project delta: 0;
- reference delta: 0;
- schema remains v2;
- production graph unchanged and acyclic;
- no WP05+ implementation;
- no Release 1.7 work.

---

## 30. Mutation Budget

Authorized repository mutations:

- only manifest-defined WP04 Application contract/model paths.

Authorized GitHub mutations:

1. #185 Backlog → In Progress;
2. completion evidence comment;
3. close #185;
4. #185 In Progress → Done.

Not authorized:

- staging;
- commits;
- branches;
- pushes;
- PRs;
- tags/releases;
- milestone closure;
- mutation of #186–#195;
- execution plan/manifest/definition edits;
- WP02/WP03 semantic-authority edits.

---

## 31. Stop Conditions

Stop with #185 OPEN / In Progress if:

- manifest path authority is ambiguous;
- WP03 semantics cannot be represented without fabricating Feature Values;
- a contract requires SQLite/Infrastructure leakage;
- a new identity scheme appears necessary;
- a package/reference change appears necessary;
- physical schema decisions become necessary;
- more files than the manifest authorizes are required;
- canonical verification fails;
- schema changes from v2;
- unexpected provider/network/database activity occurs;
- Release 1.7 work is detected.

Report the smallest corrective authority required.

---

## 32. Completion Evidence

Post concise evidence to #185 including:

- exact changed paths;
- Application-owned acceptance/retrieval contracts;
- durable evidence shape;
- explicit NewlyAccepted/EquivalentExisting semantics;
- bounded failure vocabulary;
- no fabricated Feature Values/object graph;
- no SQLite/Infrastructure leakage;
- schema remains v2;
- no orchestration/DI/Worker/persistence implementation;
- permanent tests remain 238/238;
- graph/packages/references unchanged;
- next WP05/#186.

---

## 33. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact changed paths;
5. contract inventory;
6. durable evidence representation;
7. identity reuse;
8. definition/Feature Set/provenance binding;
9. empty/non-empty invariants;
10. decimal fidelity;
11. acceptance contract;
12. exact retrieval contract;
13. NewlyAccepted/EquivalentExisting representation;
14. failure vocabulary;
15. immutability;
16. no fabricated Release 1.5 object graph;
17. Application/Infrastructure separation;
18. WP05–WP09 responsibility preservation;
19. schema-v2 preservation;
20. predecessor preservation;
21. temporary probe evidence, if used;
22. canonical validation;
23. whitespace/security/residue;
24. package/project/reference/graph checks;
25. repository mutation accounting;
26. GitHub lifecycle;
27. findings/blockers;
28. next authorized WP.

---

## 34. Completion Marker

On success, end exactly:

`RELEASE 1.6 WP04 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP05 — Durable Experiment Use-Case Integration — GitHub issue #186`

Required final lifecycle:

- #182–#185: CLOSED / Done
- #186–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked, end:

`RELEASE 1.6 WP04 BLOCKED`

and identify the smallest corrective authority required.

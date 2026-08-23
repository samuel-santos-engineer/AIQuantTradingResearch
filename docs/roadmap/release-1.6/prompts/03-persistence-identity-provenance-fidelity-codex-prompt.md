# Release 1.6 WP03 — Persistence Identity, Provenance & Fidelity — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP03 — Persistence Identity, Provenance & Fidelity — GitHub issue #184**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP03 is a semantic-authority work package. It must freeze the exact persistence-boundary invariants for identity preservation, evidence equivalence, provenance/lineage preservation, decimal and aggregate fidelity, contradiction detection, and round-trip reconstruction.

WP03 must not implement persistence, retrieval, schema v3, Application persistence contracts, DI, Worker behavior, or permanent tests.

The sole authorized repository-content artifact is the WP03 semantic document identified by the accepted Release 1.6 file manifest.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- `docs/architecture/data/DURABLE_EXPERIMENT_EVIDENCE.md`
- accepted Release 1.6 GitHub planning authority
- accepted Project #2 Release-field restoration/reconciliation authority
- accepted Release 1.6 definition-state reconciliation authority
- accepted WP01 execution evidence
- accepted WP02 execution evidence
- this WP03 authority
- its five-line companion

Also inspect the accepted Release 1.5 identity/provenance/evidence authority and current Experiment Result model needed to preserve existing semantics exactly.

Do not redefine `aiq-experiment-identity-v1`.

---

## 3. Starting Gate

Before repository or GitHub mutation, verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind: `0/0`;
- staged paths: 0;
- unexpected tracked modifications: 0;
- #182: CLOSED / Done;
- #183: CLOSED / Done;
- #184: OPEN / Backlog;
- #185–#195: OPEN / Backlog;
- milestone #47: OPEN, 12 open / 2 closed;
- Project #2 fields remain correct;
- predecessor Release restoration remains 89/89 exact;
- `DURABLE_EXPERIMENT_EVIDENCE.md` exists and is coherent with definition/plan/manifest;
- implemented SQLite schema remains v2;
- no premature Release 1.6 implementation exists;
- no Release 1.7 work exists.

Expected untracked Release 1.6 governance/candidate artifacts are not blockers.

If a mandatory gate fails, stop before moving #184 to In Progress.

---

## 4. Authorized Lifecycle

After all starting gates pass:

1. move #184 Project Status `Backlog → In Progress`;
2. perform WP03 semantic analysis;
3. create exactly the manifest-authorized WP03 artifact;
4. validate;
5. post concise completion evidence to #184;
6. close #184;
7. set #184 Project Status `In Progress → Done`.

Required final lifecycle:

- #182–#184: CLOSED / Done;
- #185: OPEN / Backlog;
- #186–#195: OPEN / Backlog;
- milestone #47: OPEN, 11 open / 3 closed.

No other issue or Project field mutation is authorized.

---

## 5. Sole Authorized Artifact

Create exactly the WP03 artifact specified by:

`docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`

Its role must be:

**Persistence Identity, Provenance & Fidelity**

Use the manifest filename/path exactly.

WP03 repository-content delta:

- semantic documentation: +1;
- production: 0;
- permanent tests: 0;
- schema implementation: 0;
- package/project/reference: 0/0/0.

If the manifest is ambiguous about the WP03 artifact, stop.

---

## 6. Core Objective

Freeze the rules by which an accepted Release 1.5 Experiment Result can cross a durable persistence boundary and later be reconstructed without semantic loss.

The artifact must establish enough authority for WP04–WP09 to implement contracts, orchestration, schema, persistence, retrieval, validation, and failure mapping without guessing.

It must not prescribe physical SQLite table, column, index, constraint, transaction API, or SQL details owned by WP06+.

---

## 7. Identity Preservation

Freeze:

- persisted semantic identity is the existing typed Experiment Result Identity;
- scheme remains `aiq-experiment-identity-v1`;
- no Release 1.6 persistence identity exists;
- no row ID, database key, timestamp, sequence, storage locator, or operational identifier participates in semantic identity;
- persistence must not recompute identity using a different scheme;
- retrieval must return evidence whose computed/validated Experiment Result Identity is exactly the requested persisted identity;
- persistence/retrieval must preserve the 64-lowercase-hex SHA-256 identity semantics established by Release 1.5;
- storage representation must not alter canonical identity inputs.

The Release 1.5 canonical identity encoding remains authoritative and unchanged.

---

## 8. Definition Identity Preservation

Freeze that the persisted result must preserve the exact Experiment Definition identity/reference represented by the accepted Experiment Result.

Require:

- no definition substitution;
- no definition aliasing;
- no “latest definition” resolution;
- no storage-owned definition identity;
- no registry lookup dependency;
- no mutation of definition identity during retrieval.

The built-in Release 1.5 experiment remains:

`simple-return-descriptive-summary-v1`

WP03 does not introduce another experiment definition.

---

## 9. Feature Set Binding

Freeze that durable Experiment Result evidence remains bound to the exact accepted Feature Set identity.

Require:

- exact Feature Set Identity preservation;
- no Feature Set persistence requirement;
- no reconstruction from feature values;
- no identity substitution based on equivalent summary values;
- no latest-feature lookup;
- no provider reacquisition;
- no recomputation requirement during retrieval.

Two Experiment Results with equal aggregate values but different Feature Set identities remain semantically distinct.

---

## 10. Persistence Equivalence

Define semantic persistence equivalence rigorously.

Two durable Experiment Result representations are equivalent only when all required semantic evidence reconstructs the same accepted Experiment Result.

Equivalence must include, as applicable from the accepted model:

- Experiment Result Identity;
- Experiment Definition identity/reference;
- Feature Set identity/reference;
- count;
- aggregate-presence state;
- mean;
- minimum;
- maximum;
- provenance;
- lineage;
- exact snapshot identity/version evidence;
- dataset/source evidence represented by the accepted result.

Do not reduce equivalence to:

- same result identity alone;
- same aggregates alone;
- same count alone;
- same definition alone.

The document must distinguish identity equality from evidence equivalence.

---

## 11. Same-Identity Contradiction

Freeze:

If two evidence representations claim the same Experiment Result Identity but differ materially in required semantic evidence, they are contradictory.

Outcome:

`IntegrityConflict`

Require:

- fail-stop;
- no overwrite;
- no merge;
- no repair;
- no replacement identity;
- no “last write wins”;
- no normalization that hides contradiction;
- no `EquivalentExisting`;
- no partial acceptance.

This rule applies whether contradiction is discovered during acceptance or retrieval validation.

---

## 12. Round-Trip Fidelity

Define the round-trip invariant:

For any valid accepted Experiment Result `R`:

`persist(R) → retrieve(R.Identity) → R'`

must yield `R'` semantically equivalent to `R`.

Require preservation of all evidence required by the accepted model.

Round-trip equivalence must be:

- deterministic;
- culture-independent;
- process-independent;
- restart-independent;
- provider-independent;
- independent of in-memory object identity.

Do not require byte-for-byte equality of storage representation; require semantic equivalence.

---

## 13. Count Fidelity

Freeze:

- count is a non-negative semantic value;
- exact count must round-trip unchanged;
- no truncation;
- no floating representation;
- no locale formatting;
- no alternate semantic meaning;
- count zero is valid for successful empty evidence.

Physical integer type/constraint belongs to WP06.

---

## 14. Aggregate Presence Fidelity

Freeze an explicit semantic distinction between:

- aggregates absent;
- aggregates present.

For empty successful evidence:

- count = 0;
- mean absent;
- minimum absent;
- maximum absent.

For non-empty successful evidence:

- aggregate evidence is present according to accepted Release 1.5 invariants.

Persistence must preserve presence/absence exactly.

Do not allow absence to collapse into numeric zero, empty text, NaN, or a fabricated sentinel at the semantic level.

Physical nullability/encoding belongs to WP06.

---

## 15. Decimal Fidelity

Freeze exact decimal semantic preservation.

Require:

- no binary floating-point semantic conversion;
- no precision loss;
- no scale-induced value change;
- no culture dependence;
- no scientific-format ambiguity;
- no rounding introduced by persistence;
- retrieved mean/minimum/maximum must equal the accepted decimal values exactly.

Reconcile with the Release 1.5 canonical decimal semantics where identity validation requires it.

Do not choose a SQLite physical decimal encoding in WP03.

---

## 16. Empty Evidence Fidelity

Freeze successful empty evidence as a first-class durable Experiment Result.

Require:

- exact identity preserved;
- exact Feature Set binding preserved;
- count zero preserved;
- aggregate absence preserved;
- provenance/lineage preserved;
- `NewlyAccepted` allowed;
- later exact retrieval succeeds;
- equivalent repeat acceptance returns `EquivalentExisting`;
- empty evidence is never converted to NotFound.

---

## 17. Non-Empty Evidence Fidelity

Freeze:

- exact identity;
- exact definition identity/reference;
- exact Feature Set identity/reference;
- exact count;
- exact decimal mean/minimum/maximum;
- exact aggregate presence;
- exact required provenance/lineage.

Equivalent storage/retrieval must reconstruct accepted immutable evidence without semantic normalization.

---

## 18. Provenance Preservation

Reconcile the exact provenance carried by the Release 1.5 Experiment Result.

Freeze that persistence must preserve every provenance component required to establish the accepted result's origin and evidence chain.

At minimum preserve references/evidence connecting the result to:

- experiment definition;
- Feature Set;
- exact snapshot;
- dataset;
- source evidence,

to the extent represented by the accepted Experiment Result.

Do not invent operational provenance fields.

Do not require Feature Set values to be persisted.

---

## 19. Lineage Preservation and Acyclicity

Freeze:

- existing lineage remains acyclic;
- persistence does not become a semantic lineage parent;
- database row identity is not a lineage node;
- retrieval does not add a lineage edge;
- no backward edge from predecessor evidence to Experiment Result;
- no registry/history node is introduced.

Durability records the accepted result; it does not transform research semantics.

---

## 20. Immutable Reconstruction

Retrieved evidence must reconstruct an immutable accepted Experiment Result.

Require:

- no mutable storage DTO leaking as the semantic result;
- no post-retrieval mutation requirement;
- no lazy provider/storage lookup required for semantic completeness;
- no storage session required after reconstruction;
- no partial object exposed before validation completes.

Implementation shape is deferred, but semantic completeness at the boundary is mandatory.

---

## 21. Validation on Acceptance

Freeze that persistence acceptance operates only on valid accepted Experiment Result evidence.

The Application boundary must not intentionally persist:

- malformed identity;
- contradictory evidence;
- invalid aggregate presence;
- invalid count/aggregate state;
- invalid provenance/lineage;
- evidence inconsistent with accepted Release 1.5 invariants.

WP04/WP05/WP09 own contracts/orchestration/mapping.

WP03 freezes the invariant only.

---

## 22. Validation on Retrieval

Freeze that exact retrieval is not permitted to trust contradictory durable evidence blindly.

Retrieved evidence must be sufficient to validate/reconstruct the accepted semantic result.

If the exact identity exists but required evidence contradicts that identity or accepted invariants:

`IntegrityConflict`

If the exact identity is absent:

`NotFound`

If storage cannot be accessed:

`DependencyUnavailable`

Do not broadly map unknown defects.

---

## 23. NewlyAccepted Fidelity

For `NewlyAccepted`, freeze:

- complete valid evidence becomes durable atomically;
- identity is unchanged;
- all required semantic evidence is persisted as one acceptance unit;
- later retrieval reconstructs equivalent evidence;
- no duplicate logical result is created;
- no Feature Set persistence is implied;
- no registry/history semantics are implied.

---

## 24. EquivalentExisting Fidelity

For `EquivalentExisting`, freeze:

- exact identity already exists;
- existing durable evidence validates;
- existing evidence is semantically equivalent to the candidate;
- no semantic mutation occurs;
- no overwrite occurs;
- no duplicate logical result is created;
- returned/observed outcome is successful idempotence.

EquivalentExisting must not mask a same-identity contradiction.

---

## 25. Atomic Semantic Unit

Freeze the atomic semantic unit as the complete durable Experiment Result evidence required for equivalent reconstruction.

A failed acceptance must not expose a newly accepted partial semantic result.

A successful acceptance must not require subsequent writes to become semantically complete.

Do not specify SQLite transaction implementation.

---

## 26. Storage-Neutral Semantic Rules

Although SQLite is the selected Release 1.6 persistence technology, this WP03 artifact must express identity/provenance/fidelity invariants independently of incidental SQL layout.

Do not freeze:

- table names;
- column names;
- index names;
- foreign-key names;
- SQL types;
- SQL statements;
- migration statement ordering;
- repository class names;
- connection APIs.

Those belong downstream.

---

## 27. Schema Boundary

State explicitly:

- current implemented schema remains v2;
- Release 1.6 plans v3;
- WP03 introduces no schema implementation;
- WP06 owns the schema-v3 physical model;
- v1/v2 evidence must remain preserved by future migration;
- Feature Set persistence remains excluded;
- generalized experiment registry/history remains excluded.

No schema file/code mutation is authorized.

---

## 28. Failure Semantics

Preserve the Release 1.6 bounded vocabulary:

1. `InvalidRequest`
2. `NotFound`
3. `DependencyUnavailable`
4. `InvalidEvidence`
5. `IntegrityConflict`

Clarify semantic boundaries relevant to persistence fidelity:

- malformed persistence/retrieval request → `InvalidRequest`;
- exact identity absent → `NotFound`;
- durable storage unavailable → `DependencyUnavailable`;
- candidate evidence invalid before acceptance → `InvalidEvidence`;
- same-identity contradictory durable evidence or retrieval contradiction → `IntegrityConflict`.

Unknown programming defects propagate.

Do not introduce a storage-specific public failure vocabulary in WP03.

---

## 29. Ownership Boundaries

Preserve:

### Domain
Expected delta: 0.

### Application
Owns semantic contracts, validation, orchestration, identity/evidence requirements, and bounded failure vocabulary.

### Infrastructure
Later owns SQLite mapping, migration, transactions, persistence/retrieval mechanics, and storage-specific exception translation.

### Worker
Later owns explicit one-shot durable mode and bounded presentation.

Production graph must remain unchanged.

---

## 30. Predecessor Preservation

Explicitly preserve Releases 1.1–1.5.

Particularly:

- Release 1.1 SQLite durability semantics remain intact;
- Release 1.2 snapshot/dataset identity semantics remain intact;
- Release 1.3 pipeline remains unchanged;
- Release 1.4 Feature Set identity/generation remains unchanged;
- Release 1.5 Experiment generation and identity/provenance/evidence semantics remain unchanged.

Release 1.6 persistence must not recompute or redefine predecessor evidence.

---

## 31. Explicit Deferrals

Defer:

- Feature Set persistence;
- feature catalog;
- generalized experiment registry;
- experiment history;
- list/search/query/comparison;
- update/delete/retention;
- additional experiment definitions;
- provider acquisition/fallback;
- strategy/signal/backtesting;
- portfolio/risk;
- scheduling/retry/recovery;
- distributed execution;
- notebooks/workspaces/UI/API;
- AI/ML/explainability/MLOps;
- Release 1.7 implementation.

---

## 32. Downstream Authority Protection

Do not implement or prematurely decide:

### WP04
Application persistence contracts.

### WP05
Durable Experiment use-case integration.

### WP06
Schema-v3 physical model.

### WP07
Experiment Result persistence implementation.

### WP08
Exact Experiment Result retrieval implementation.

### WP09
Storage validation/failure mapping implementation.

### WP10
DI/configuration.

### WP11
Durable Worker.

### WP12
Permanent tests.

### WP13
Architecture/current-state documentation alignment.

### WP14
Integration/staging/commit/push/PR.

If a downstream physical decision is not necessary to freeze semantic fidelity, defer it explicitly.

---

## 33. Documentation Quality Gate

The artifact must:

- preserve repository terminology;
- clearly separate identity from evidence equivalence;
- clearly separate semantic fidelity from physical representation;
- distinguish acceptance from retrieval;
- distinguish NotFound from IntegrityConflict;
- define empty/non-empty round-trip requirements;
- define provenance/lineage preservation;
- define immutability;
- identify downstream ownership;
- contain no speculative generalization;
- contain no broken repository-relative links;
- contain terminal newline;
- contain no trailing whitespace.

---

## 34. Validation

After creating the sole authorized artifact, run:

`eng/verify.ps1 -Configuration Release`

Require:

- Domain.Tests: 11/11;
- Application.Tests: 102/102;
- Infrastructure.Tests: 112/112;
- Architecture.Tests: 13/13;
- total: 238/238;
- skipped: 0;
- warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also require:

- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- direct expected-untracked whitespace/final-newline checks: PASS;
- staged paths: 0;
- production delta: 0;
- test delta: 0;
- package/project/reference delta: 0/0/0;
- implemented schema: v2;
- dependency graph unchanged;
- database/WAL/SHM/journal residue: 0;
- provider/network execution: 0;
- real credentials: 0;
- Release 1.7 work: 0.

---

## 35. Mutation Budget

Authorized repository mutation:

- exactly one manifest-authorized WP03 semantic document.

Authorized GitHub mutation:

- #184 Backlog → In Progress;
- concise completion evidence;
- close #184;
- #184 In Progress → Done.

Not authorized:

- edits to WP02 artifact unless the manifest explicitly says otherwise;
- production code;
- tests;
- schema implementation;
- packages/projects/references;
- definition/plan/manifest changes;
- staging;
- commits;
- branches;
- pushes;
- PRs;
- tags/releases;
- mutation of #185–#195;
- milestone closure.

---

## 36. Stop Conditions

Stop and preserve #184 OPEN / In Progress if:

- manifest artifact path is ambiguous;
- WP02 and Release 1.5 authorities materially contradict;
- exact required Experiment Result evidence cannot be determined from accepted authorities;
- resolving fidelity requires prematurely choosing physical schema;
- unexpected implementation exists;
- schema is not v2;
- Project restoration has drifted;
- Release 1.7 work exists;
- canonical verification fails;
- whitespace/security/residue checks fail;
- a second repository-content artifact becomes necessary.

Report the smallest corrective authority required.

---

## 37. Completion Evidence

Post concise #184 evidence including:

- created artifact path;
- identity preservation;
- definition and Feature Set binding;
- persistence equivalence;
- same-identity contradiction rule;
- round-trip invariant;
- empty/non-empty fidelity;
- decimal/presence fidelity;
- provenance/lineage preservation;
- retrieval validation;
- schema remains v2;
- WP06 physical schema deferred;
- production/test/package/reference deltas zero;
- canonical 238/238;
- security/format/residue PASS;
- next WP04/#185.

---

## 38. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting state;
4. exact artifact;
5. identity preservation;
6. definition identity;
7. Feature Set binding;
8. persistence equivalence;
9. same-identity contradiction;
10. round-trip invariant;
11. count fidelity;
12. aggregate-presence fidelity;
13. decimal fidelity;
14. empty fidelity;
15. non-empty fidelity;
16. provenance;
17. lineage/acyclicity;
18. immutable reconstruction;
19. acceptance validation;
20. retrieval validation;
21. NewlyAccepted;
22. EquivalentExisting;
23. atomic semantic unit;
24. schema boundary;
25. failure semantics;
26. ownership;
27. predecessor preservation;
28. explicit deferrals;
29. downstream authority preservation;
30. canonical validation;
31. whitespace/security/residue;
32. repository mutation accounting;
33. GitHub mutation accounting;
34. #184 lifecycle;
35. #185 preservation;
36. findings/blockers;
37. next authorized WP.

---

## 39. Completion Marker

On success, end exactly:

`RELEASE 1.6 WP03 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP04 — Application Persistence Contracts — GitHub issue #185`

Required final lifecycle:

- #182–#184: CLOSED / Done
- #185: OPEN / Backlog
- #186–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked, end:

`RELEASE 1.6 WP03 BLOCKED`

and identify the smallest corrective authority required.

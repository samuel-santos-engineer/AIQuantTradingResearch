# Release 1.6 WP02 — Durable Experiment Evidence Discovery — Codex Authority

## 1. Mission

Execute only:

**Release 1.6 WP02 — Durable Experiment Evidence Discovery — GitHub issue #183**

Release:

**Phase 4 — Release 1.6: Durable Experiment Evidence Foundation**

WP02 is a semantic-discovery work package. Its purpose is to freeze the durable Experiment Result evidence boundary before persistence contracts, schema-v3 design, Infrastructure storage, Worker durability, or permanent Release 1.6 tests are implemented.

The sole authorized repository-content artifact is the semantic discovery document defined by the accepted Release 1.6 file manifest.

No production implementation is authorized.

---

## 2. Required Authorities

Read completely before execution:

- `docs/roadmap/release-1.6/RELEASE_1.6_DEFINITION.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`
- accepted Release 1.6 GitHub planning authority
- accepted Project #2 Release-field restoration/reconciliation authority
- accepted Release 1.6 definition-state reconciliation authority
- `01-release-repository-preflight-codex-prompt.md`
- accepted WP01 execution evidence
- this WP02 authority
- its five-line companion

Also inspect the accepted Release 1.5 semantic and implementation authorities needed to describe the existing Experiment Result accurately, including:

- Release 1.5 experiment semantics;
- Release 1.5 experiment identity/provenance/evidence semantics;
- Application Experiment Result model/contracts;
- Release 1.5 validation/failure semantics;
- Release 1.5 Experiment generation integration;
- current Worker experiment behavior where relevant.

Do not redefine accepted Release 1.5 semantics.

---

## 3. Starting Gate

Before changing repository content or issue #183, verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main == 18dfb01bf3503d91415b081b11fcdd7249094373`;
- ahead/behind: `0/0`;
- no staged tracked paths;
- no unexpected tracked modifications;
- #182: CLOSED / Done;
- #183: OPEN / Backlog;
- #184–#195: OPEN / Backlog;
- milestone #47: OPEN, 13 open / 1 closed;
- Project #2 fields remain correct;
- predecessor Release restoration remains 89/89 exact;
- implemented SQLite schema remains v2;
- no Release 1.6 implementation has started;
- no Release 1.7 work exists.

Existing accepted Release 1.6 governance artifacts are expected and are not blockers.

If any mandatory starting condition materially fails, stop before moving #183 to In Progress.

---

## 4. Authorized Lifecycle

Only after the starting gate passes:

1. move #183 Project Status:
   `Backlog → In Progress`;
2. perform WP02 semantic discovery;
3. create only the manifest-authorized WP02 semantic document;
4. validate the repository;
5. post concise completion evidence to #183;
6. close #183;
7. set #183 Project Status:
   `In Progress → Done`.

Required final lifecycle:

- #182: CLOSED / Done;
- #183: CLOSED / Done;
- #184: OPEN / Backlog;
- #185–#195: OPEN / Backlog;
- milestone #47: OPEN, 12 open / 2 closed.

Do not mutate any other issue lifecycle or Project field.

---

## 5. Sole Authorized Artifact

Create exactly the WP02 semantic-discovery artifact named by:

`docs/roadmap/release-1.6/RELEASE_1.6_FILE_MANIFEST.md`

Use the manifest path and filename exactly.

Expected semantic role:

**Durable Experiment Evidence discovery/semantics**

Do not invent an alternate filename if the manifest specifies one.

Repository-content delta for WP02 must be:

- semantic documentation: +1;
- production: 0;
- permanent tests: 0;
- schema implementation: 0;
- package/project/reference: 0.

If the manifest does not unambiguously identify the WP02 artifact, stop and report the smallest corrective authority required.

---

## 6. Discovery Objective

Freeze precisely what Release 1.6 means by **durable Experiment Result evidence**.

The document must answer, without implementation guessing:

- what exact accepted Release 1.5 object is eligible for persistence;
- what evidence must survive persistence and retrieval;
- what evidence is excluded;
- what constitutes a newly accepted durable result;
- what constitutes an equivalent existing durable result;
- what constitutes an integrity conflict;
- what exact lookup means;
- what restart-safe retrieval means;
- what fidelity is required for empty and non-empty results;
- how identity, provenance, and lineage are preserved;
- what atomicity means semantically;
- what failures are bounded;
- what behavior remains deferred.

The document must be sufficient authority for WP03–WP14 without prematurely specifying physical SQLite design owned by WP06.

---

## 7. Persisted Semantic Object

Freeze that the persisted semantic object is:

**the accepted immutable Release 1.5 Experiment Result evidence**

Persistence must not create a new research result or recompute semantics.

The durable boundary receives an already-valid Experiment Result produced under the accepted Release 1.5 model.

Explicitly distinguish:

- Experiment Result generation;
- Experiment Result durability.

Generation remains Release 1.5 Application behavior.

Durability is Release 1.6 behavior.

Durability must not silently become an implicit side effect of ordinary Release 1.5 Experiment execution.

---

## 8. Identity Semantics

Freeze:

- identity scheme remains `aiq-experiment-identity-v1`;
- no new Release 1.6 persistence identity;
- no storage-generated semantic identity;
- exact durable lookup key is the typed Experiment Result Identity;
- the persisted identity must equal the accepted in-memory Experiment Result identity;
- retrieval must preserve that exact identity;
- operational/storage metadata must not alter identity;
- equivalent persistence attempts must not generate replacement identities;
- persistence cannot repair or normalize contradictory evidence into identity agreement.

WP02 may describe these semantic requirements.

WP02 must not redefine the canonical byte encoding already owned by Release 1.5.

---

## 9. Evidence Fidelity

Define the minimum evidence that must round-trip without semantic loss.

Reconcile this from the accepted Release 1.5 Experiment Result model and authorities.

At minimum reason explicitly about preservation of:

- Experiment Result Identity;
- Experiment Definition identity/reference;
- exact Feature Set identity/reference;
- summary count;
- aggregate-presence state;
- mean;
- minimum;
- maximum;
- provenance;
- lineage;
- snapshot/dataset/source evidence represented by the accepted result;
- definition/version semantics represented by the accepted result.

Do not add evidence fields merely because they might be useful later.

The durable representation must preserve the semantic content required to reconstruct an equivalent accepted Experiment Result.

---

## 10. Empty Result Fidelity

Freeze exact empty-result durability semantics.

Expected:

- count = 0;
- aggregates absent;
- absence remains absence after persistence/retrieval;
- absent aggregates must not become zero;
- absent aggregates must not become empty strings or fabricated numeric values;
- exact Experiment Result identity is preserved;
- exact Feature Set binding is preserved;
- empty results are persistable successful evidence;
- an empty result is not a NotFound condition.

Do not authorize storage-specific sentinel values at this semantic layer.

---

## 11. Non-Empty Result Fidelity

Freeze exact non-empty durability semantics.

Expected:

- count is preserved exactly;
- mean is preserved exactly;
- minimum is preserved exactly;
- maximum is preserved exactly;
- decimal semantics remain exact;
- no binary floating-point conversion is permitted semantically;
- no culture-dependent representation is permitted;
- retrieved evidence must reconstruct the same accepted semantic result;
- persisted/retrieved identity must remain the same.

Physical decimal encoding belongs to later design/implementation authority unless already fixed by predecessor semantics.

---

## 12. NewlyAccepted

Define:

`NewlyAccepted`

as the successful outcome when no durable Experiment Result exists under the exact Experiment Result Identity and the accepted evidence is atomically stored.

Required semantics:

- one complete accepted result becomes durable;
- no partial durable result is observable;
- the result remains retrievable after process restart;
- success does not alter the Experiment Result identity;
- success does not create Feature Set persistence;
- success does not imply registry/history/search capabilities.

Do not define timestamps, sequence IDs, database row IDs, or operational metadata as semantic identity.

---

## 13. EquivalentExisting

Define:

`EquivalentExisting`

as the successful idempotent outcome when durable evidence already exists under the exact Experiment Result Identity and is semantically equivalent to the candidate accepted Experiment Result.

Freeze:

- no duplicate logical result;
- no overwrite required;
- no update semantics;
- no new semantic identity;
- no contradiction;
- repeat acceptance is successful;
- equivalent reruns remain restart-safe;
- equivalence must compare all identity-relevant and required durable evidence, not only aggregate values.

Equal summary statistics alone are insufficient if identity/provenance/evidence differs.

WP03 may later formalize exact persistence fidelity/equivalence mechanics, but WP02 must freeze the semantic rule.

---

## 14. IntegrityConflict

Define:

`IntegrityConflict`

for contradictory durable evidence under the same exact Experiment Result Identity.

Expected semantic rule:

If the same Experiment Result Identity resolves to evidence that is not semantically equivalent to the candidate or violates accepted identity/evidence invariants, the system must fail with an integrity conflict.

It must not:

- overwrite;
- merge;
- repair silently;
- assign a new identity;
- return `EquivalentExisting`;
- accept partial evidence;
- fall back to provider acquisition;
- delete the contradictory record.

Unknown corruption/defects beyond the bounded model must not be broadly normalized.

---

## 15. Exact Lookup Semantics

Freeze exact retrieval as:

**lookup by exact typed Experiment Result Identity**

Expected:

- exact identity match only;
- no fuzzy lookup;
- no definition-only lookup;
- no Feature Set-only lookup;
- no latest-result lookup;
- no list/search/history query;
- no range query;
- no experiment registry query.

Successful retrieval returns semantically equivalent immutable Experiment Result evidence.

Absent exact identity maps to the accepted bounded NotFound semantics.

Do not design SQL or indexes in WP02.

---

## 16. Restart-Safe Retrieval

Freeze restart-safe semantics:

After `NewlyAccepted` succeeds and the process/storage connection is disposed normally, a later independent process/context using the same durable store must be able to retrieve the exact Experiment Result by identity with equivalent evidence.

Restart safety must not depend on:

- in-memory caches;
- retained DI scope;
- retained process state;
- provider access;
- recomputation of the Experiment Result;
- Feature Set persistence;
- network access.

This is a semantic durability requirement, not yet a process-test implementation.

---

## 17. Atomicity Semantics

Freeze the semantic atomicity requirement:

A durable acceptance operation must expose either:

- the complete accepted Experiment Result evidence; or
- no newly accepted Experiment Result evidence.

No partially persisted semantic result may be observable after failure.

Atomicity applies to the complete Release 1.6 durable Experiment Result unit.

Do not prescribe transaction APIs or SQLite statements in WP02.

WP06/WP07 own physical realization.

---

## 18. Provenance and Lineage Preservation

Preserve the accepted acyclic lineage established by Release 1.5.

Durability must not create a new semantic parent or rewrite predecessor identities.

The persisted/retrieved result must preserve its accepted evidence chain through:

- experiment definition;
- Feature Set;
- exact snapshot;
- dataset;
- source evidence,

to the extent those references/evidence are part of the accepted Release 1.5 Experiment Result.

Freeze that durability is a storage boundary, not a new research-transformation node.

Do not persist Feature Set values under WP02 semantics.

---

## 19. Immutability

Freeze:

- accepted durable Experiment Results are immutable;
- no update semantics;
- no delete semantics;
- no replacement semantics;
- no mutable status lifecycle;
- no correction-in-place;
- no upsert that changes contradictory evidence.

Idempotent equivalent acceptance is not mutation of semantic evidence.

Any future lifecycle/retention/delete capability is deferred.

---

## 20. Failure Vocabulary

Reconcile WP02 semantic failures with the accepted Release 1.6 definition.

Expected bounded failures:

1. `InvalidRequest`
2. `NotFound`
3. `DependencyUnavailable`
4. `InvalidEvidence`
5. `IntegrityConflict`

Describe their semantic role without implementing mappings.

At minimum distinguish:

- malformed/missing durable request → `InvalidRequest`;
- exact lookup absence → `NotFound`;
- unavailable durable storage → `DependencyUnavailable`;
- invalid evidence crossing the durability boundary → `InvalidEvidence`;
- contradictory same-identity evidence → `IntegrityConflict`.

Unknown programming defects must continue to propagate.

Do not introduce retries, repair, fallback, partial success, or broad exception normalization.

---

## 21. Application / Infrastructure Boundary

Freeze ownership at the semantic level:

### Application

Owns:

- durable Experiment Result use-case semantics;
- contracts;
- validation expectations;
- semantic outcomes/failures;
- orchestration boundaries;
- identity/evidence requirements.

### Infrastructure

Owns later:

- SQLite schema/migration;
- transactions;
- physical representation;
- exact persistence/retrieval;
- storage-specific failure translation.

### Worker

Owns later:

- explicit durable mode configuration;
- one-shot invocation;
- bounded presentation;
- exit behavior.

### Domain

Expected Release 1.6 delta:

`0`

Do not implement any of these layers in WP02.

---

## 22. Existing Release Preservation

Explicitly preserve:

### Release 1.1
Existing SQLite persistence behavior and historical observation durability.

### Release 1.2
Dataset/snapshot semantics and exact snapshot identity/version behavior.

### Release 1.3
Five-stage pipeline behavior.

### Release 1.4
Feature generation and Feature Set identity semantics.

### Release 1.5
Experiment generation, `simple-return-descriptive-summary-v1`, `aiq-experiment-identity-v1`, in-memory Experiment Result semantics, DI, and explicit one-shot Experiment Worker behavior.

Release 1.6 durability must be additive and explicit.

---

## 23. Schema Boundary

WP02 must state:

- implemented schema at WP02 remains v2;
- Release 1.6 plans a non-destructive v2→v3 migration;
- WP02 does not define physical schema-v3 layout;
- WP02 does not implement migration;
- WP02 does not authorize table/column/index names unless the accepted manifest/definition already requires them;
- v1/v2 data must remain preserved by future migration;
- Feature Set persistence remains absent.

Physical schema authority belongs to WP06.

---

## 24. Worker Boundary

Freeze only the semantic Worker requirement already accepted by the Release 1.6 definition:

- durability is an explicit one-shot mode;
- it is not an implicit side effect of Release 1.5 Experiment mode;
- existing Experiment, Feature, and pipeline behavior must remain preserved;
- no provider fallback;
- bounded success/failure presentation;
- no scheduling/retry daemon behavior.

Do not define final CLI/configuration key names unless already frozen by accepted planning authorities.

WP11 owns implementation.

---

## 25. Explicit Deferrals

The semantic document must explicitly defer at least:

- Feature Set persistence;
- feature catalog;
- generalized experiment registry;
- experiment history;
- comparison/search/query APIs;
- update/delete/retention;
- additional experiment definitions;
- strategies;
- signals;
- backtesting;
- portfolio/risk;
- notebooks/workspaces;
- visualization;
- public APIs;
- provider acquisition;
- network fallback;
- scheduling;
- retries/recovery/checkpoints;
- distributed execution;
- AI/ML;
- explainability/MLOps;
- Release 1.7 implementation.

Do not accidentally create requirements for these areas.

---

## 26. No Premature WP03+ Decisions

WP02 must not steal downstream authority.

In particular, do not prematurely implement or physically freeze:

- WP03 exact persistence identity/provenance/fidelity mechanics beyond semantic requirements;
- WP04 Application persistence contracts;
- WP05 durable use-case implementation;
- WP06 SQLite schema-v3 physical model;
- WP07 persistence implementation;
- WP08 retrieval implementation;
- WP09 storage-specific validation/failure mapping;
- WP10 DI/configuration;
- WP11 Worker implementation;
- WP12 permanent tests;
- WP13 architecture/current-state documentation alignment;
- WP14 integration/staging/commit/PR.

If a downstream detail is necessary to make WP02 semantics coherent, state the semantic invariant and defer the physical/mechanical choice.

---

## 27. Documentation Quality Gate

The WP02 artifact must:

- use repository terminology consistently;
- distinguish accepted current behavior from Release 1.6 planned behavior;
- distinguish semantic requirements from physical implementation;
- be deterministic and implementation-actionable;
- avoid speculative generalization;
- avoid provider-specific semantics;
- avoid storage-engine leakage except the already-selected SQLite durability context;
- contain explicit invariants and exclusions;
- preserve predecessor terminology;
- contain repository-relative links where links are used;
- contain no broken repository-relative links;
- have terminal newline;
- contain no trailing whitespace.

---

## 28. Validation

After creating the sole authorized artifact, run:

`eng/verify.ps1 -Configuration Release`

Expected:

- Domain.Tests: 11/11;
- Application.Tests: 102/102;
- Infrastructure.Tests: 112/112;
- Architecture.Tests: 13/13;
- permanent total: 238/238;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also require:

- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- direct whitespace/final-newline check for untracked governed artifacts: PASS;
- staged paths: 0;
- implemented schema remains v2;
- production delta: 0;
- test delta: 0;
- package/project/reference delta: 0/0/0;
- dependency graph unchanged;
- database/WAL/SHM/journal residue: 0;
- provider/network execution: 0;
- real credentials: 0.

---

## 29. Mutation Budget

Authorized repository-content mutation:

- exactly one WP02 semantic document.

Not authorized:

- production code;
- permanent tests;
- architecture tests;
- schema implementation;
- packages;
- projects;
- references;
- existing Release 1.5 files unless the manifest explicitly names the WP02 artifact there;
- execution plan changes;
- manifest changes;
- definition changes;
- other prompt changes;
- staging;
- commits;
- branches;
- pushes;
- PRs;
- tags;
- releases.

Authorized GitHub mutations:

- #183 Backlog → In Progress;
- concise completion evidence on #183;
- close #183;
- #183 In Progress → Done.

Nothing else.

---

## 30. Stop Conditions

Stop and leave #183 OPEN / In Progress if, after it has started:

- the manifest-authorized artifact cannot be identified;
- Release 1.5 Experiment Result semantics are contradictory or insufficient to define durability;
- definition/plan/manifest contradiction is discovered;
- physical schema design is required to resolve an unresolved semantic question;
- unexpected implementation exists;
- schema is no longer v2;
- predecessor Project restoration drifted;
- Release 1.7 work exists;
- canonical verification fails;
- whitespace/security/residue gates fail;
- completing WP02 would require a second repository-content artifact.

Do not improvise around a blocker.

Report the smallest corrective authority required.

---

## 31. Completion Evidence

On success, post concise evidence to #183 covering:

- semantic artifact path;
- durable object frozen as accepted Release 1.5 Experiment Result;
- identity reuse;
- exact lookup;
- `NewlyAccepted`;
- `EquivalentExisting`;
- `IntegrityConflict`;
- empty/non-empty fidelity;
- restart-safe retrieval;
- atomicity;
- provenance/lineage;
- immutability;
- schema remains v2;
- physical schema deferred to WP06;
- production/test/package/reference deltas zero;
- canonical 238/238;
- security/format/residue PASS;
- next authorized WP03/#184.

Do not paste the full semantic document or full execution report into the issue.

---

## 32. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. exact created artifact;
5. durable semantic object;
6. identity semantics;
7. evidence fidelity;
8. empty-result fidelity;
9. non-empty fidelity;
10. `NewlyAccepted`;
11. `EquivalentExisting`;
12. `IntegrityConflict`;
13. exact lookup;
14. restart-safe retrieval;
15. atomicity;
16. provenance/lineage;
17. immutability;
18. failure vocabulary;
19. Application/Infrastructure/Worker ownership;
20. predecessor release preservation;
21. schema boundary;
22. explicit deferrals;
23. downstream authority preserved;
24. canonical validation;
25. whitespace/security/residue;
26. repository mutation accounting;
27. GitHub mutation accounting;
28. #183 lifecycle;
29. #184 preservation;
30. findings/blockers;
31. next authorized work package.

---

## 33. Completion Marker

On success, end exactly:

`RELEASE 1.6 WP02 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP03 — Persistence Identity, Provenance & Fidelity — GitHub issue #184`

Required final lifecycle:

- #182: CLOSED / Done
- #183: CLOSED / Done
- #184: OPEN / Backlog
- #185–#195: OPEN / Backlog
- milestone #47: OPEN

If blocked, end:

`RELEASE 1.6 WP02 BLOCKED`

and identify the smallest corrective authority required.

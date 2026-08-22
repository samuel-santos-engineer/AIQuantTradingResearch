# Release 1.5 WP02 — Experiment Semantic Discovery

## GitHub Issue
`#169 — Release 1.5 WP02 — Experiment Semantic Discovery`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP02 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Selected capability:

`simple-return-descriptive-summary-v1`

Planned identity scheme:

`aiq-experiment-identity-v1`

WP02 is a semantic-discovery work package. Its purpose is to freeze the exact experiment semantics before production model, identity encoding, computation, validation, integration, DI, Worker, or permanent-test implementation begins.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- accepted Release 1.4 feature semantics and identity/provenance/evidence documents
- relevant Release 1.1–1.4 data lifecycle, dataset, pipeline, public-contract, configuration, observability, and architecture authorities
- WP01 authority and completion evidence
- this WP02 authority and its five-line companion

Repository truth and accepted release authorities take precedence over assumptions.

If an accepted authority materially contradicts another authority, stop rather than silently choosing a new semantic rule.

---

## 2. Objective

Create the manifest-authorized Release 1.5 experiment-semantics artifact that freezes the exact behavior of:

`simple-return-descriptive-summary-v1`

The semantic document must define, at minimum:

- exact accepted input evidence;
- exact experiment definition;
- deterministic count semantics;
- deterministic arithmetic-mean semantics;
- deterministic minimum and maximum semantics;
- empty Feature Set behavior;
- non-empty Feature Set behavior;
- decimal arithmetic rules;
- ordering implications;
- timestamp/provenance implications;
- determinism and equivalence;
- immutable result semantics;
- failure distinctions and fail-stop expectations;
- Application ownership;
- provider/storage independence;
- relationship to Release 1.4 feature generation;
- preservation of Release 1.3 pipeline semantics;
- persistence/schema decision;
- Release 1.6+ deferrals.

WP02 must freeze semantics only. Exact canonical experiment identity encoding remains WP03-owned.

---

## 3. Expected Starting State

Reconcile rather than assume:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- expected baseline SHA: `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`;
- tracked modifications: `0`.

Expected lifecycle:

- WP01 #168: CLOSED / Done;
- WP02 #169: OPEN / Backlog;
- WP03 #170: OPEN / Backlog;
- WP04–WP13: OPEN / Backlog;
- milestone #46: OPEN;
- Release 1.5 implementation branch/PR: none;
- Release 1.6 implementation: none.

Accepted untracked Release 1.5 governance artifacts and manifest-defined out-of-band execution inputs are not blockers. Classify them from repository truth and the file manifest.

If #168 is not Closed/Done, or #170 has already started, stop before mutation.

---

## 4. WP02 Lifecycle Start

After the starting-state gates pass:

- move only #169 Project #2 Status from Backlog to In Progress.

Read back the state.

If #169 is already In Progress solely because this exact WP02 execution partially started, continue idempotently if no unauthorized mutation occurred.

Do not mutate #170.

WP03 #170 must remain OPEN / Backlog throughout WP02.

---

## 5. Required Repository Discovery

Before writing the semantic artifact, inspect the accepted implementation and semantic foundations that constrain Release 1.5.

At minimum reconcile:

### Release 1.1
- historical-observation fidelity;
- decimal price evidence;
- timestamp/offset fidelity;
- persistence/retrieval semantics;
- failure vocabulary that remains relevant.

### Release 1.2
- immutable research datasets;
- exact snapshot identity/version;
- ordered snapshot evidence;
- exact lookup semantics;
- `NotFound` versus existing empty evidence;
- provenance/lineage and integrity-conflict behavior.

### Release 1.3
- fixed five-stage pipeline remains unchanged;
- experiment execution is not a sixth pipeline stage;
- fail-stop semantics and unknown-defect propagation;
- identity/provenance boundaries that must not be redefined.

### Release 1.4
- exact `simple-return-lag-1-v1` Feature Set semantics;
- Feature Set immutability;
- Feature Definition and Feature Set identity distinction;
- exact snapshot/version binding;
- feature-value ordering;
- decimal-only feature computation;
- empty/single-observation Feature Set behavior;
- feature provenance and acyclic lineage;
- bounded feature failures;
- one-shot Worker feature execution;
- absence of feature persistence;
- SQLite schema v2.

Do not duplicate or redefine predecessor semantics unnecessarily. Reference and preserve them.

---

## 6. Exact Experiment Definition

Freeze exactly one built-in experiment definition:

`simple-return-descriptive-summary-v1`

It consumes one accepted immutable Feature Set produced under the Release 1.4 `simple-return-lag-1-v1` definition.

No other feature definition or experiment definition is in scope.

The experiment produces descriptive evidence only. It is not:

- a strategy;
- a signal;
- a forecast;
- a prediction;
- a backtest;
- a risk model;
- a portfolio model;
- an ML model;
- an optimization;
- a statistical-inference engine.

Do not generalize the definition into configurable formulas or an experiment plugin framework.

---

## 7. Accepted Input Evidence

Define the experiment input as an accepted immutable Release 1.4 Feature Set.

The semantic document must state that the experiment is bound to the exact Feature Set evidence supplied through the accepted Application boundary.

The input must preserve predecessor evidence, including as applicable:

- Feature Definition identity;
- Feature Set identity;
- exact dataset snapshot identity/version;
- ordered feature values;
- value timestamps and preserved offsets;
- provenance;
- lineage.

The experiment must not independently reacquire market data, reread provider data, reconstruct a dataset from external sources, or bypass the accepted feature-generation boundary.

Exact orchestration for obtaining Feature Set evidence is WP07-owned.

---

## 8. Count Semantics

Freeze count as:

- the exact cardinality of the accepted Feature Set values;
- a non-negative integer;
- independent of invocation time, process, machine, culture, logging, or persistence disposition.

For an empty Feature Set:

`count = 0`

For a non-empty Feature Set containing `N` values:

`count = N`

No filtering, sampling, deduplication, missing-value removal, or silent value exclusion is allowed.

---

## 9. Empty Feature Set Semantics

An accepted empty Feature Set is a successful experiment input.

The experiment result must represent:

- count = 0;
- arithmetic mean = absent;
- minimum = absent;
- maximum = absent.

Do not use:

- zero as a synthetic aggregate;
- NaN;
- infinity;
- sentinel decimals;
- fabricated feature values;
- failure merely because the Feature Set is empty.

The empty result is valid immutable semantic evidence and must remain bound to its exact Feature Set.

WP03 will define exact identity encoding for this result.

---

## 10. Non-Empty Summary Semantics

For accepted ordered feature values:

`x[0], x[1], ..., x[N-1]`

where `N >= 1`, freeze:

### Count

`count = N`

### Arithmetic Mean

`mean = (x[0] + x[1] + ... + x[N-1]) / N`

### Minimum

`minimum = min(x[0], x[1], ..., x[N-1])`

### Maximum

`maximum = max(x[0], x[1], ..., x[N-1])`

For exactly one feature value:

- count = 1;
- mean = that exact value;
- minimum = that exact value;
- maximum = that exact value.

No median, variance, standard deviation, quantile, cumulative return, annualization, Sharpe ratio, confidence interval, or other statistic is part of Release 1.5.

---

## 11. Decimal Arithmetic

Freeze decimal-only arithmetic.

The experiment must not convert accepted decimal feature evidence to binary floating point for computation.

Do not introduce convenience rounding.

Do not make results culture-dependent.

The semantic artifact must explicitly address the possibility that arithmetic operations required for a summary cannot be represented by the accepted decimal computation boundary.

Such numeric failure must not produce:

- partial summary evidence;
- rounded fallback evidence;
- floating-point fallback;
- saturation;
- NaN/infinity;
- skipped values.

Exact implementation/failure mapping belongs to WP05/WP06, but the semantic rule must be frozen here.

---

## 12. Ordering Semantics

The Feature Set remains ordered evidence inherited from Release 1.4.

Count, arithmetic mean, minimum, and maximum are mathematically insensitive to permutation of equal accepted values, but Release 1.5 MUST NOT use that fact to erase source evidence identity or provenance.

The experiment consumes the accepted Feature Set as a whole.

Do not sort, reorder, deduplicate, or canonicalize feature values as a new semantic input transformation.

Different accepted Feature Set identities remain distinct experiment inputs even if their computed summary values are numerically identical.

Exact experiment-result identity binding remains WP03-owned.

---

## 13. Timestamp and Offset Semantics

The descriptive summary does not invent a synthetic market timestamp.

Individual feature timestamps and offsets remain predecessor evidence belonging to the accepted Feature Set.

The experiment result must remain traceable to that Feature Set rather than collapsing provenance into an arbitrary invocation timestamp.

Operational timestamps such as execution start/end time, logging time, process time, or wall-clock time are not semantic experiment evidence.

Do not define a new summary timestamp unless an accepted authority already requires one. If repository truth reveals such a requirement, reconcile it explicitly or stop on contradiction.

---

## 14. Determinism

Equivalent accepted Feature Set evidence under the same experiment definition must produce equivalent semantic summary evidence.

Determinism must be independent of:

- current culture;
- UI culture;
- timezone of the executing machine;
- process identity;
- machine identity;
- invocation time;
- duration;
- correlation ID;
- filesystem path;
- database path;
- logging configuration;
- provider credentials;
- retry/scheduling state.

No randomness is allowed.

No clock dependency is allowed for semantic computation.

---

## 15. Equivalence and Distinctness

Freeze these semantic rules:

- equivalent accepted Feature Set evidence + same experiment definition → equivalent experiment summary evidence;
- an equivalent recomputation is not a new semantic result merely because it occurred in another process or at another time;
- different Feature Set identities remain experiment-distinct even when count/mean/min/max are numerically identical;
- a changed experiment definition must be separately governed and identity-distinct;
- operational execution metadata must not determine semantic equivalence.

Exact canonical identity construction is deferred to WP03.

---

## 16. Immutability

Experiment result evidence must be immutable once established.

A successful result must not expose mutable collections or mutable semantic state that can alter the meaning of:

- count;
- mean;
- minimum;
- maximum;
- definition reference;
- Feature Set reference;
- provenance;
- lineage.

WP02 defines the semantic requirement; WP04 owns the concrete immutable model.

---

## 17. Provenance and Lineage

Freeze the semantic provenance chain without defining WP03's exact canonical encoding.

At minimum, an experiment result must remain traceable to:

- the exact experiment definition;
- the exact accepted Feature Set;
- the Feature Set's predecessor snapshot/dataset/source-state provenance already established by Releases 1.2–1.4.

Lineage must remain acyclic.

Conceptually:

`source state → dataset definition/research dataset → snapshot/version → feature definition → feature set → experiment definition → experiment result`

Release 1.5 must not rewrite or fabricate predecessor provenance.

No downstream experiment identity/evidence may feed back into dataset, pipeline, or feature identities.

---

## 18. Failure Semantic Categories

Reconcile the accepted Release 1.5 definition and predecessor failure vocabulary, then freeze the semantic distinctions required for later implementation.

At minimum distinguish conceptually:

- invalid experiment request;
- unsupported experiment definition;
- feature-generation/input dependency unavailable;
- requested predecessor evidence not found, where applicable;
- invalid Feature Set evidence;
- invalid numeric evidence/computation;
- integrity contradiction;
- successful empty result;
- successful non-empty result;
- unknown programming/system defect propagation.

Do not invent broad catch-all normalization.

Unknown defects must remain distinguishable from governed bounded failures.

Exact enum/type names and validation precedence are WP04/WP06-owned unless already fixed by accepted authorities.

---

## 19. Fail-Stop and Evidence-Established-Only Rules

Freeze:

- first governed failure stops downstream semantic construction;
- no partial summary is a successful result;
- no Experiment Result identity may be fabricated after a failure that prevents valid result evidence;
- no mean/min/max may be emitted if the required successful evidence cannot be established;
- unknown defects propagate rather than being converted into misleading semantic success/failure evidence.

The successful empty result is not a partial result and must not be classified as failure.

---

## 20. Application Ownership

Release 1.5 experiment semantics belong to Application.

Expected architecture remains:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure.

WP02 must not move experiment semantics into Infrastructure or Worker.

Domain remains zero-delta-first unless a later accepted authority proves a stable domain abstraction is necessary.

No new project/reference edge is authorized.

---

## 21. Relationship to Release 1.4 Feature Generation

Release 1.5 builds on, but does not redefine, Release 1.4.

The semantic artifact must explicitly preserve:

- `simple-return-lag-1-v1`;
- `aiq-feature-identity-v1`;
- exact snapshot/version binding;
- feature ordering/cardinality;
- timestamp/offset fidelity;
- Feature Set immutability;
- equivalent feature recomputation;
- bounded feature failures;
- one-shot feature execution semantics.

Release 1.5 experiment execution is a separate bounded use case.

Do not mutate Release 1.4 feature generation into a generalized multi-stage research DAG.

---

## 22. Release 1.3 Pipeline Protection

The Release 1.3 research pipeline remains exactly the accepted fixed five-stage sequential one-shot pipeline.

The experiment is:

- not a sixth pipeline stage;
- not automatically appended to the pipeline;
- not a configurable pipeline node;
- not a scheduler target introduced by WP02.

Any future pipeline/experiment composition requires separate authority.

---

## 23. Provider and Storage Independence

Experiment semantics must be independent of:

- Twelve Data;
- HTTP;
- provider credentials;
- provider response formats;
- SQL;
- SQLite APIs;
- database paths;
- connection strings;
- filesystem paths.

Release 1.5 consumes accepted Application feature evidence.

No provider fallback or live acquisition is allowed.

---

## 24. Persistence and Schema Decision

Freeze:

- experiment result persistence: absent;
- experiment registry/history: absent;
- experiment cache: absent;
- feature persistence expansion: absent;
- SQLite schema remains version 2.

Experiment output is deterministic immutable in-memory evidence for Release 1.5.

Do not introduce schema v3.

---

## 25. Observability Boundary

Semantic result evidence may later be presented through safe structured Worker events/output, but observability must not redefine experiment identity or equivalence.

Do not introduce:

- durable experiment history;
- metrics backend;
- tracing backend;
- dashboard;
- telemetry persistence.

Operational observability data is not semantic experiment evidence.

---

## 26. Explicit Release 1.6+ Deferrals

The semantic document must explicitly defer at least:

- experiment persistence;
- experiment registry/history;
- research workspace;
- notebooks;
- visualization;
- APIs;
- additional descriptive/inferential experiments;
- configurable statistics;
- broader feature libraries;
- feature persistence/catalog expansion;
- strategies/signals;
- backtesting;
- portfolio/risk;
- AI/ML;
- explainability;
- MLOps;
- live acquisition orchestration;
- scheduling;
- retries/recovery/checkpoints;
- plugins;
- expressions;
- generalized DAGs;
- distributed execution;
- durable execution history;
- telemetry backends.

Do not create implementation placeholders for deferred work.

---

## 27. Authorized Repository Mutation

WP02 may create exactly the semantic artifact assigned to WP02 by:

`docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`

Use the exact manifest path and filename. Do not guess a substitute.

If the manifest does not unambiguously assign exactly one WP02 semantic artifact, stop and report the authority conflict.

WP02 may not modify any existing production, test, Worker, Infrastructure, Domain, package, project, reference, schema, current-state documentation, execution-plan, definition, manifest, or other governance file.

Expected WP02 deltas:

- semantic documentation: exactly 1 manifest-authorized file;
- production: 0;
- tests: 0;
- packages: 0;
- projects: 0;
- references: 0;
- schema: 0.

Do not stage or commit the artifact.

---

## 28. Semantic Artifact Quality Gate

The created semantic document must:

- be normative rather than speculative;
- distinguish Release 1.5 rules from future possibilities;
- use predecessor terminology consistently;
- avoid implementation-detail leakage where later WPs own the mechanism;
- state exact empty/non-empty behavior;
- state exact decimal summary formulas;
- state deterministic/equivalence rules;
- state provenance/lineage expectations;
- state failure/fail-stop expectations;
- preserve architecture/schema/provider boundaries;
- explicitly defer WP03 canonical identity encoding.

Do not silently introduce configurable behavior.

---

## 29. Technical Validation

After creating the semantic artifact, run canonical verification:

`eng/verify.ps1 -Configuration Release`

Expected baseline remains:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also run:

- `git diff --check`;
- `git diff --cached --check`;
- direct trailing-whitespace inspection of the new untracked WP02 semantic file and relevant governance companions.

Require:

- database/generated residue: 0;
- provider calls: 0;
- real credentials: 0.

No permanent tests are added in WP02.

---

## 30. Semantic Reconciliation Gate

Before closing WP02, prove the artifact does not contradict:

- Release 1.5 definition;
- Release 1.5 execution plan;
- Release 1.5 file manifest;
- Release 1.4 feature semantics;
- Release 1.4 identity/provenance/evidence semantics;
- Release 1.3 fixed pipeline;
- Release 1.2 snapshot/dataset semantics;
- Release 1.1 persistence/fidelity foundations;
- architecture dependency rules;
- schema v2.

If a contradiction is discovered, do not hide it by editing predecessor authorities. Stop and report the smallest corrective authority required.

---

## 31. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch an integration branch;
- push;
- create a PR;
- merge;
- tag;
- create a release;
- clean/delete accepted governance artifacts;
- mutate schema/packages/projects/references;
- implement WP03+ behavior.

Git transport mutation budget:

`0`

Repository mutation budget:

exactly the one WP02 semantic artifact.

---

## 32. Authorized GitHub Mutation Budget

At WP02 start, after starting gates pass:

1. #169 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP02 completion-evidence comment to #169;
3. close #169 as completed;
4. set #169 Project Status to Done.

Do not mutate #170.

Milestone #46 must remain OPEN.

---

## 33. Completion Gate

WP02 may close only if:

- WP01 #168 is Closed/Done;
- #169 was correctly In Progress during execution;
- #170 remains Open/Backlog;
- exactly one manifest-authorized semantic artifact was created;
- experiment semantics are fully frozen within WP02 scope;
- exact identity encoding remains deferred to WP03;
- production/test/package/project/reference/schema deltas are zero;
- Release 1.3/1.4 behavior is preserved;
- SQLite remains v2;
- canonical verification passes;
- permanent tests remain 214/214;
- Architecture.Tests remain 13/13;
- build warnings/errors are 0/0;
- Gitleaks/format/whitespace pass;
- residue is 0;
- no provider execution occurred;
- no Release 1.6 work began.

If any gate fails, do not close #169 or mark it Done.

---

## 34. Completion Evidence Comment

On success, post concise evidence to #169 covering:

- semantic artifact path;
- frozen `simple-return-descriptive-summary-v1`;
- accepted Feature Set input boundary;
- count/mean/min/max semantics;
- empty-result semantics;
- decimal-only deterministic computation;
- equivalence/distinctness rules;
- provenance/lineage;
- failure/fail-stop boundary;
- Application ownership;
- schema v2 / no persistence;
- Release 1.3 pipeline unchanged;
- production/test/package/reference/schema delta 0;
- 214/214 tests and 13/13 Architecture.Tests;
- canonical verification/Gitleaks/whitespace PASS;
- #170 preserved Open/Backlog.

---

## 35. Final Read-Back

After successful closure verify:

- #169: CLOSED / Done;
- #170: OPEN / Backlog;
- #171–#180: unchanged Open / Backlog;
- milestone #46: OPEN;
- milestone counts: 11 open / 2 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative untracked Release 1.5 governance/semantic artifacts accurately.

---

## 36. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- WP01 is not Closed/Done;
- #170 or later WPs started unexpectedly;
- manifest ownership of the WP02 semantic file is ambiguous;
- accepted authorities materially conflict;
- premature Release 1.5 implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail;
- satisfying WP02 would require production/test/schema/package/reference changes.

Do not use WP02 to repair unrelated repository or governance problems.

---

## 37. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. WP01/WP02/WP03 lifecycle reconciliation;
5. predecessor semantic foundations reviewed;
6. exact semantic artifact created;
7. experiment definition;
8. input Feature Set boundary;
9. count/mean/min/max semantics;
10. empty/non-empty semantics;
11. decimal/ordering/timestamp rules;
12. determinism/equivalence;
13. provenance/lineage;
14. failures/fail-stop;
15. Application/architecture boundary;
16. pipeline/provider/persistence/schema protection;
17. Release 1.6+ deferrals;
18. repository delta;
19. canonical validation/test counts;
20. security/whitespace/residue;
21. GitHub lifecycle mutations;
22. final #169/#170/milestone state;
23. findings/blockers;
24. next authorized WP.

---

## 38. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP02 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP03 — Experiment Identity, Provenance & Evidence — GitHub issue #170`

Do not begin WP03.

If blocked, end:

`RELEASE 1.5 WP02 BLOCKED`

and identify the smallest corrective authority required.

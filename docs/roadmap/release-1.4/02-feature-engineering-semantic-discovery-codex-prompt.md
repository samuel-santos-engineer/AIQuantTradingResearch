# Release 1.4 WP02 — Feature Engineering Semantic Discovery — Codex Authority

## Mission

Execute **WP02 — Feature Engineering Semantic Discovery** for **Phase 4 — Release 1.4: Deterministic Feature Engineering Foundation**.

GitHub issue: **#154**

WP02 freezes the exact semantics of the single Release 1.4 built-in transformation:

`simple-return-lag-1-v1`

This is semantic discovery only. Do not implement identities, contracts, computation, DI, Worker behavior, persistence, schema changes, or permanent feature tests.

## Governing Authorities

Read completely before mutation:

1. `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2. `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4. Accepted Release 1.4 GitHub-planning authority/result.
5. WP01 authority/result.
6. Release 1.3 closure and relevant Release 1.1–1.3 semantic/architecture artifacts.
7. Current Domain, Application, Infrastructure, Worker, and test code relevant to observations, datasets, snapshots, catalog lookup, pipeline semantics, identity, provenance, fidelity, ordering, and failures.

Repository truth wins over assumptions. Stop rather than silently redefining accepted predecessor behavior.

## Starting-State Gates

Verify:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- ahead/behind `0/0`;
- staged paths `0`;
- Release 1.3 milestone #54: Closed;
- Release 1.4 milestone #45: Open;
- WP01/#153: Closed/Done;
- WP02/#154: Open/Backlog;
- WP03/#155: Open/Backlog;
- WP03 not started;
- no WP15+;
- no Release 1.5 implementation.

Classify the working tree and preserve accepted Release 1.4 planning/governance artifacts.

## Authorized Repository Mutation

Create exactly one file:

`docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`

Repository delta otherwise must be zero.

Do not modify production code, tests, project/package/reference files, schema, Worker configuration, Release 1.4 planning artifacts, predecessor artifacts, or architecture tests.

Do not stage, commit, push, branch, create a PR, merge, tag, or release.

## Required Predecessor Discovery

Reconcile repository truth for:

### Release 1.1
- persisted historical observations;
- target semantics;
- timestamp and offset fidelity;
- decimal fidelity;
- deterministic ordering;
- empty retrieval;
- provider/network separation;
- SQLite ownership.

### Release 1.2
- `DatasetDefinition`;
- exact target and `[from,to)` semantics;
- deterministic materialization;
- dataset/source-state identities;
- immutable snapshot identity/version;
- provenance and lineage;
- snapshot ordering;
- empty snapshots;
- timestamp-offset and decimal fidelity;
- exact catalog lookup and `NotFound`;
- equivalence and integrity-conflict semantics.

### Release 1.3
- fixed five-stage one-shot pipeline;
- Application ownership;
- structured semantic evidence;
- semantic vs operational information;
- first-failure behavior;
- no scheduler/retry/DAG/run-history behavior.

Feature generation must **not** become a sixth Release 1.3 pipeline stage.

Also inventory existing repository vocabulary for feature engineering, return, lag, signal, transformation, feature set/value, enrichment, feature-ready, and experiment-ready. Separate implemented truth from future roadmap language.

## Release 1.4 Semantic Boundary

Freeze this conceptual flow:

```text
Explicit feature request
  → exact immutable dataset snapshot lookup
  → snapshot/evidence validation
  → deterministic simple-return-lag-1-v1 transformation
  → immutable in-memory feature evidence
  → structured result
```

Release 1.4 consumes an already accepted immutable dataset snapshot.

It must not:

- call a provider;
- acquire live observations;
- implicitly rematerialize a dataset;
- modify the Release 1.3 pipeline;
- create mutable feature state;
- persist feature output.

## Built-In Feature Definition

Freeze exactly one built-in definition:

`simple-return-lag-1-v1`

Formula:

```text
r[i] = (p[i] / p[i-1]) - 1
```

for each valid ordered observation position `i > 0`.

Define precisely:

- what `p[i]` means using existing repository dataset vocabulary;
- how snapshot order determines adjacent values;
- that the feature belongs to current observation `i`;
- that its timestamp and offset come from current observation `i`;
- which predecessor evidence/provenance is semantically carried forward;
- that the semantic transform name/version is `simple-return-lag-1-v1`.

Do not add log returns, rolling returns, configurable lag, alternative formulas, or multiple feature definitions.

## Arithmetic Semantics

Freeze exact decimal semantics:

- preserve predecessor decimal fidelity;
- use decimal arithmetic semantically;
- no binary floating-point conversion;
- no culture-dependent computation;
- no locale/timezone-dependent result;
- no convenience rounding rule.

If division produces a result that cannot be represented by the platform's supported decimal semantics, treat it as bounded invalid numeric evidence for later WP07 classification. Do not silently round, clamp, convert to floating point, or substitute a value.

Do not select implementation exception types in WP02.

## Zero Prior Value

If:

```text
p[i-1] = 0
```

the return is undefined.

Freeze this as invalid numeric input/evidence for the requested feature computation.

Do not emit infinity, NaN, zero, a sentinel, silently skip the pair, or return partial success.

Unknown programming defects remain distinct from expected invalid numeric evidence.

## Ordering

Use the immutable snapshot's accepted deterministic order.

Do not reorder by culture, local timezone, provider order, hash/dictionary enumeration, execution time, or filesystem state.

Do not invent a new deduplication policy. Existing accepted snapshot semantics remain authoritative.

For `N >= 2` valid observations, output order follows adjacent input pairs.

## Timestamp and Offset

For `r[i]`, the produced feature belongs to current observation `i`.

Use the current observation's timestamp and preserve its accepted offset fidelity.

Do not use the prior observation's timestamp, execution time, or a synthetic timestamp.

Do not introduce UTC normalization if it would destroy accepted offset fidelity.

## Cardinality

Freeze:

- empty accepted snapshot → successful empty feature set;
- one-observation snapshot → successful empty feature set;
- `N >= 2` valid observations → exactly `N - 1` feature values.

Adjacent mapping:

```text
(p[0], p[1]) → r[1]
(p[1], p[2]) → r[2]
...
(p[N-2], p[N-1]) → r[N-1]
```

`NotFound` is distinct from an existing empty snapshot.

No partial-success behavior is authorized.

## Determinism and Immutability

Equivalent accepted input evidence plus the same semantic feature definition must produce semantically equivalent immutable feature evidence.

Semantic results must not depend on:

- invocation/wall-clock time;
- duration;
- machine/process identity;
- random values;
- correlation IDs;
- paths;
- connection strings;
- logging configuration;
- current culture;
- local timezone.

Do not freeze WP03 fingerprint byte encoding here.

Do not introduce mutable feature versions, overwrite semantics, feature persistence, feature catalog persistence, or cache persistence.

## Provenance and Lineage

Freeze the semantic requirement that a feature set is traceable to:

- the exact feature definition;
- the exact accepted dataset snapshot/version;
- predecessor dataset/source-state evidence already represented by accepted snapshot provenance.

Lineage must remain acyclic.

Do not create a second dataset identity scheme, operational run identity, mutable feature version, or durable feature-run history.

WP03 owns exact identity/provenance/evidence representation.

## Success Semantics

Successful semantic cases include:

- non-empty feature set;
- empty feature set from empty snapshot;
- empty feature set from one observation;
- equivalent recomputation.

Persistence is not required for success.

Do not import Release 1.2 `NewlyAccepted` / `EquivalentExisting` persistence dispositions into feature generation merely to distinguish recomputation.

## Failure Semantic Inventory

Identify, but do not yet encode final contracts for:

- invalid request;
- unsupported feature definition;
- snapshot `NotFound`;
- unavailable dependency;
- invalid predecessor evidence;
- invalid numeric input/evidence;
- integrity contradiction;
- unknown/unrelated defect propagation.

Keep these distinctions available for WP05/WP07.

Do not add retries, fallback data, recovery orchestration, or partial success.

## Snapshot Lookup

Freeze exact accepted snapshot identity lookup:

- `NotFound` is distinct from empty;
- empty snapshot is an existing accepted snapshot;
- lookup failure does not trigger provider acquisition;
- lookup failure does not trigger dataset rematerialization;
- no implicit "latest snapshot" selection unless already required by accepted authority.

## Provider/Storage Boundary

Feature semantics must remain provider- and storage-independent.

Do not depend semantically on Twelve Data, HTTP, SQLite APIs, SQL details, database paths, credentials, or connection lifetimes.

Infrastructure production delta remains zero by default.

## Release 1.3 Pipeline Protection

Release 1.3 remains exactly:

1. historical observation retrieval;
2. dataset materialization;
3. immutable snapshot persistence;
4. catalog registration;
5. structured pipeline result/evidence.

Release 1.4 begins from an accepted immutable snapshot.

Do not add a sixth pipeline stage, alter pipeline identity/evidence/topology, or automatically generate features during pipeline execution.

## Schema/Persistence Decision

SQLite remains exactly **schema version 2**.

Feature output is reproducible in memory.

Do not add schema v3, feature tables, feature catalog/history, run history, checkpoints, scheduler state, or caches.

## Release 1.5+ Deferrals

Explicitly defer:

- feature persistence/catalog;
- multiple indicators;
- configurable lag;
- arbitrary formulas;
- rolling indicators;
- plugins;
- configurable feature DAGs;
- live acquisition orchestration;
- scheduling/refresh loops;
- retries/circuit breakers/fallback;
- checkpoints/resume;
- durable execution history;
- notebooks/workspaces;
- strategies;
- portfolio/risk behavior;
- backtesting;
- model training;
- ML/MLOps;
- distributed/streaming execution;
- metrics/tracing backends.

Do not create placeholders for deferred behavior.

## Required Semantic Artifact Structure

`FEATURE_ENGINEERING_SEMANTICS.md` must include at minimum:

1. Purpose
2. Release boundary
3. Predecessor foundations
4. Feature-engineering vocabulary
5. Input snapshot boundary
6. `simple-return-lag-1-v1`
7. Ordering semantics
8. Timestamp/offset semantics
9. Decimal/numeric semantics
10. Empty and single-observation semantics
11. Multi-observation semantics
12. Determinism
13. Immutability
14. Provenance and lineage
15. Success semantics
16. Failure semantic inventory
17. Provider/storage boundary
18. Relationship to Release 1.3 pipeline
19. Schema/persistence decision
20. Explicit deferrals
21. Ownership and WP03+ handoff

Use established repository terminology.

## WP03 Protection

WP03 owns **Feature Identity, Provenance & Evidence Semantics**.

Do not prematurely freeze:

- canonical fingerprint byte encoding;
- exact length-delimited representation;
- exact fingerprint composition;
- final Feature Definition/Feature Set identity types;
- exact provenance/lineage record shapes;
- exact evidence DTOs;
- implementation APIs.

WP02 may freeze semantic identity requirements only.

## WP04+ Protection

Do not implement:

- Domain/Application feature models;
- request/result contracts;
- computation;
- validators;
- integration use cases;
- DI/configuration;
- Worker execution;
- permanent feature tests;
- architecture-test changes.

## Validation

Run canonical Release verification after creating the artifact:

```powershell
./eng/verify.ps1 -Configuration Release
```

Expected WP01 baseline:

- Domain.Tests: 11/11;
- Application.Tests: 77/77;
- Infrastructure.Tests: 96/96;
- Architecture.Tests: 13/13;
- permanent total: 197/197;
- skipped: 0;
- build warnings/errors: 0/0;
- Gitleaks: PASS.

Also require:

- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- new artifact trailing whitespace: 0;
- generated/database residue: 0;
- SQLite/WAL/SHM/journal residue: 0;
- provider execution: 0;
- real credentials: 0.

If repository truth legitimately changed since WP01, reconcile rather than falsifying counts.

## Mutation Accounting

Expected WP02 delta:

- semantic documentation: exactly 1 new file;
- Domain/Application/Infrastructure/Worker production: 0;
- permanent tests: 0;
- architecture tests: 0;
- packages/references/schema: 0/0/0;
- staging/commit/branch/push/PR/merge/tag/release: 0.

## GitHub Lifecycle

After starting gates pass:

1. move #154 Backlog → In Progress;
2. execute WP02;
3. validate;
4. post concise completion evidence;
5. close #154;
6. set Project #2 status to Done.

Do not mutate #155 except read-only verification.

Required final state:

- #154: Closed/Done;
- #155: Open/Backlog;
- milestone #45: Open.

Do not start WP03.

## Stop Conditions

Stop with `RELEASE 1.4 WP02 BLOCKED` if:

- predecessor semantics materially conflict;
- Release 1.4 planning state is invalid;
- WP01 is not Closed/Done;
- WP03 already started;
- unexpected feature implementation exists;
- semantic definition requires predecessor-contract mutation;
- schema evolution appears necessary;
- formula cannot be coherently defined from accepted snapshot values;
- WP02 introduces verification failure;
- unauthorized files would need mutation.

State the smallest corrective authority required.

## Acceptance Matrix

WP02 passes only if:

- starting Git/GitHub state valid;
- predecessor foundations reconciled;
- exactly one semantic artifact created;
- one built-in feature only;
- formula frozen exactly;
- accepted snapshot input boundary frozen;
- ordering deterministic;
- feature timestamp belongs to current observation;
- offset fidelity preserved;
- decimal semantics preserved;
- zero prior value is invalid numeric evidence;
- empty snapshot succeeds empty;
- one observation succeeds empty;
- valid `N` observations produce `N-1` values;
- no partial success;
- immutable/deterministic feature evidence established;
- provenance/lineage requirements established without WP03 encoding;
- `NotFound` distinct from empty;
- failure situations remain distinct;
- provider/storage independence preserved;
- Release 1.3 pipeline unchanged;
- schema remains v2;
- feature persistence absent;
- Release 1.5+ deferrals explicit;
- production/test delta 0/0;
- package/reference/schema delta 0/0/0;
- canonical verification PASS;
- security/offline validation PASS;
- whitespace checks PASS;
- residue 0;
- #154 Closed/Done;
- #155 Open/Backlog.

## Required Final Report

Report:

1. executive summary;
2. authorities reviewed;
3. repository/Git baseline;
4. working-tree classification;
5. predecessor/lifecycle gates;
6. initial baseline;
7. Release 1.1–1.3 reconciliation;
8. feature-vocabulary inventory;
9. selected boundary;
10. formula;
11. snapshot input semantics;
12. ordering;
13. timestamp/offset;
14. decimal and zero-prior semantics;
15. empty/single/multi-observation semantics;
16. determinism/immutability;
17. provenance/lineage requirements;
18. success/failure inventory;
19. `NotFound` distinction;
20. provider/storage independence;
21. Release 1.3 pipeline protection;
22. schema/persistence decision;
23. Release 1.5+ deferrals;
24. artifact created;
25. production/test/package/reference/schema deltas;
26. build/test/canonical verification;
27. security/offline/whitespace/residue evidence;
28. mutation accounting;
29. Git/GitHub protection;
30. final #154/#155 states;
31. findings/blockers;
32. final decision;
33. WP03 handoff.

Successful terminal marker:

`RELEASE 1.4 WP02 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP03 — Feature Identity, Provenance & Evidence Semantics — GitHub issue #155`

Do not start WP03.

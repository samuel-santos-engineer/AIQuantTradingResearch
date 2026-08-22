# Release 1.5 WP05 — Deterministic Summary Computation

## GitHub Issue
`#172 — Release 1.5 WP05 — Deterministic Summary Computation`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP05 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Selected experiment:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

WP05 implements the deterministic Application-owned descriptive-summary computation over accepted Release 1.4 Feature Set evidence using the immutable Release 1.5 model/contracts and canonical identity capability established by WP04.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- WP04 Application experiment model/contracts and identity implementation
- accepted Release 1.4 feature semantics, model, identity, and generation contracts
- relevant Application coding/testing/architecture authorities
- WP01–WP04 completion evidence
- this WP05 authority and its five-line companion

Repository truth and accepted authorities take precedence over assumptions.

If the accepted WP04 contract cannot support WP05 without changing frozen semantics or crossing into WP06/WP07 ownership, stop rather than redesigning the release boundary.

---

## 2. Objective

Implement exactly the manifest-authorized deterministic computation for:

`simple-return-descriptive-summary-v1`

The implementation must:

- consume the accepted immutable Feature Set evidence required by the WP04 computation seam;
- compute exact count;
- compute decimal arithmetic mean;
- compute decimal minimum;
- compute decimal maximum;
- produce successful empty evidence for an empty Feature Set;
- produce coherent single-value evidence;
- produce coherent non-empty evidence;
- preserve exact Feature Set identity/provenance binding;
- use the WP04 canonical experiment identity capability to construct valid identity-bearing successful experiment evidence if that is the accepted computation seam;
- remain deterministic, synchronous, offline, provider-independent, storage-independent, and fail-stop.

WP05 must not implement orchestration/lookup, validation precedence, DI, Worker behavior, or permanent tests assigned to later work packages.

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

- #168–#171: CLOSED / Done;
- #172 WP05: OPEN / Backlog;
- #173 WP06: OPEN / Backlog;
- #174–#180: OPEN / Backlog;
- milestone #46: OPEN with 9 open / 4 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected technical baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- SQLite schema: v2.

Expected accepted WP04 production artifacts include:

- `src/AIQuantTradingResearch.Application/Experiments/ExperimentIdentity.cs`
- `src/AIQuantTradingResearch.Application/Experiments/ExperimentDefinition.cs`
- `src/AIQuantTradingResearch.Application/Experiments/ExperimentEvidence.cs`
- `src/AIQuantTradingResearch.Application/Experiments/ExperimentGenerationContracts.cs`

Reconcile exact paths from repository truth and the manifest.

If #171 is not Closed/Done or #173 has started, stop before mutation.

---

## 4. WP05 Lifecycle Start

After starting-state gates pass:

- move only #172 Project #2 Status from Backlog to In Progress.

Read back the state.

If #172 is already In Progress solely because this exact WP05 execution partially started, continue idempotently if no unauthorized mutation occurred.

Do not mutate #173.

#173 must remain OPEN / Backlog throughout WP05.

---

## 5. Mandatory WP04 Contract Reconciliation

Before writing code, inspect the actual accepted WP04 implementation.

Explicitly determine:

- the exact computation interface/seam;
- the exact input type;
- whether computation receives a complete `FeatureSet` or another accepted immutable feature-evidence type;
- the exact output type;
- the canonical experiment identity API exposed by WP04;
- how Experiment Definition Identity is obtained;
- how Experiment Result Identity is computed;
- how summary evidence is constructed;
- how provenance/lineage references are represented;
- how bounded failures are represented.

Record these findings in the execution report.

Do not change WP04 contracts merely because another API shape would be more convenient.

If the seam is impossible to implement while honoring WP02/WP03 semantics, stop and request the smallest corrective authority.

---

## 6. Identity Ownership

WP04 established:

**Canonical `aiq-experiment-identity-v1` computation is Application-owned in WP04.**

WP05 must consume that accepted capability rather than introducing a second canonical identity algorithm.

Do not duplicate:

- domain strings;
- canonical field ordering;
- byte framing;
- SHA-256 implementation;
- decimal canonicalization;
- external fingerprint validation.

WP05 may invoke the WP04 identity capability as required to produce successful immutable result evidence.

If the actual WP04 implementation contradicts its accepted completion report on identity ownership, stop.

---

## 7. Application Ownership

WP05 production changes must remain Application-owned and limited to exact manifest-authorized paths.

Expected architecture remains:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure.

Expected WP05 deltas:

- Application production: manifest-authorized computation path(s);
- Domain: 0;
- Infrastructure: 0;
- Worker: 0;
- permanent tests: 0;
- packages/projects/references/schema: 0/0/0/0.

No new dependency edge is authorized.

---

## 8. Exact Experiment Definition

The computation implementation supports exactly:

`simple-return-descriptive-summary-v1`

Do not implement:

- configurable aggregate sets;
- arbitrary formulas;
- additional statistics;
- plugins;
- expressions;
- strategies;
- generalized experiment dispatch.

If the computation seam can receive an unsupported definition, do not invent WP06 validation behavior. Preserve the contract and fail according to already-frozen bounded semantics only if WP05 owns that check; otherwise leave validation to WP06.

---

## 9. Accepted Feature Evidence

Compute only from the accepted immutable Release 1.4 Feature Set evidence supplied through the WP04 seam.

Preserve:

- exact Feature Set Identity;
- ordered feature values;
- exact decimal values;
- feature timestamps/offsets as predecessor evidence;
- Feature Set provenance/lineage.

Do not:

- acquire market data;
- query a provider;
- query SQLite;
- reconstruct a Feature Set from storage;
- filter values;
- deduplicate values;
- reorder values;
- sample values;
- silently skip values.

WP07 owns feature-to-experiment orchestration.

---

## 10. Count

For a Feature Set containing `N` values:

`count = N`

Count must be exact and include every accepted feature value exactly once.

Rules:

- empty Feature Set → count 0;
- one value → count 1;
- `N` values → count `N`.

No filtering or deduplication is allowed.

---

## 11. Successful Empty Computation

For an accepted empty Feature Set, return successful immutable summary evidence with:

- count = 0;
- mean absent;
- minimum absent;
- maximum absent.

Construct a deterministic Experiment Result Identity bound to:

- exact Experiment Definition Identity;
- exact Feature Set Identity;
- count zero;
- canonical aggregate-absence evidence.

Do not use:

- decimal zero as an aggregate substitute;
- NaN;
- infinity;
- sentinel values;
- global empty-result identity;
- failure merely because input is empty.

No feature values means no arithmetic operation is required.

---

## 12. Single-Value Computation

For exactly one feature value `x`:

- count = 1;
- mean = `x`;
- minimum = `x`;
- maximum = `x`.

Preserve exact decimal evidence.

Do not round.

The successful result identity must be computed through the accepted WP04 identity capability.

---

## 13. Non-Empty Computation

For accepted values:

`x[0], x[1], ..., x[N-1]`

where `N >= 1`, compute exactly:

- count = `N`;
- sum = decimal accumulation of every value exactly once;
- mean = `sum / N`;
- minimum = the exact minimum accepted value;
- maximum = the exact maximum accepted value.

Use deterministic iteration.

Do not sort merely to compute min/max.

Do not mutate input evidence.

Do not introduce parallel aggregation.

---

## 14. Decimal-Only Arithmetic

All summary arithmetic must use .NET `decimal` semantics consistent with the frozen Release 1.5 authorities.

Do not convert values or intermediate evidence to:

- `double`;
- `float`;
- binary floating point;
- strings for arithmetic.

Do not apply convenience rounding.

Do not use current culture for computation.

The semantic result must be independent of current culture/UI culture.

---

## 15. Arithmetic Mean and Intermediate Overflow

The arithmetic mean is semantically:

`(x[0] + x[1] + ... + x[N-1]) / N`

The implementation must honor this exact decimal semantic boundary while handling representability honestly.

Do not silently:

- fall back to binary floating point;
- saturate;
- round to avoid overflow;
- drop values;
- reorder values to manufacture a different arithmetic path unless an accepted authority explicitly permits it;
- emit partial successful evidence.

If decimal accumulation or division cannot produce valid governed decimal evidence, classify/propagate according to the WP04 contract and the exact WP05/WP06 ownership in the execution plan.

If numeric failure mapping belongs to WP06, WP05 must expose the natural deterministic failure in the form expected by the accepted seam without broad exception normalization.

Do not catch unrelated unknown exceptions.

---

## 16. Minimum and Maximum

For non-empty accepted evidence:

- initialize from accepted evidence rather than synthetic sentinels;
- compare exact decimal values;
- include every value;
- preserve exact selected decimal evidence.

Do not use floating-point comparison.

For one value, minimum and maximum are that value.

For empty input, both are absent.

---

## 17. Ordering

Release 1.4 Feature Set ordering remains accepted evidence.

WP05 must iterate deterministically over that evidence.

Although count/min/max and the mathematical mean are summary statistics, WP05 must not erase or rewrite predecessor identity/provenance based on permutation insensitivity.

Do not canonicalize input by sorting.

The exact Feature Set Identity remains the semantic predecessor and distinguishes different accepted Feature Sets.

---

## 18. Result Construction

On successful complete computation, construct exactly the immutable successful evidence required by WP04.

The result must bind:

- Experiment Definition Identity;
- exact Feature Set Identity;
- coherent summary evidence;
- Experiment Result Identity;
- required provenance/lineage references.

Construct Experiment Result Identity only after complete valid summary evidence exists.

Do not produce partial identity-bearing success.

Use accepted factories/constructors rather than bypassing invariants.

---

## 19. Determinism

Equivalent accepted Feature Set evidence under the same experiment definition must produce equivalent:

- count;
- mean;
- minimum;
- maximum;
- Experiment Result Identity;
- provenance/lineage semantic evidence.

The computation must be independent of:

- current culture;
- UI culture;
- machine timezone;
- invocation time;
- wall clock;
- process/machine identity;
- correlation IDs;
- filesystem/database path;
- provider credentials;
- logging configuration;
- scheduling/retry state.

No randomness.

No clock dependency.

---

## 20. Equivalent Recomputations

A second computation over equivalent accepted semantic input must yield the same semantic Experiment Result Identity.

Do not introduce execution-instance identity.

Do not include operational metadata in identity inputs.

Different Feature Set identities must remain result-identity-distinct even when their numerical summaries match.

This behavior should follow automatically from the WP04 identity capability; do not reimplement it.

---

## 21. Provenance and Lineage

Preserve the WP03 lineage:

`source state → dataset definition/research dataset → snapshot/version → feature definition → feature set → experiment definition → experiment result`

WP05 may construct only the minimum downstream provenance/lineage evidence required by the WP04 result model.

Do not rewrite predecessor provenance.

Do not introduce generalized graph infrastructure.

Do not introduce cycles.

---

## 22. Failure and Fail-Stop Boundary

WP05 must preserve:

- first failure stops successful result construction;
- no partial summary is success;
- no fabricated Experiment Result Identity after numeric/evidence failure;
- unknown defects are not broadly normalized.

Do not add retry/fallback behavior.

Do not convert all exceptions into one bounded failure.

WP06 owns the complete experiment validation/failure-semantics boundary; do not preempt its precedence rules.

---

## 23. No Orchestration

WP05 must not:

- perform exact snapshot lookup;
- invoke feature generation as an upstream use case;
- resolve Feature Set targets from configuration;
- access `IDatasetSnapshotStore`;
- access SQLite;
- call providers;
- implement the Release 1.5 use-case orchestration assigned to WP07.

The computation implementation operates only on evidence supplied through its accepted seam.

---

## 24. No DI or Worker Changes

Do not modify:

- Application DI registration;
- Infrastructure DI;
- Worker configuration;
- Worker execution;
- `Program.cs`;
- process exit behavior.

Those belong to WP08/WP09.

---

## 25. Release 1.3/1.4 Protection

Preserve Release 1.3:

- fixed five-stage pipeline;
- sequential one-shot semantics;
- no sixth experiment stage.

Preserve Release 1.4:

- `simple-return-lag-1-v1`;
- `aiq-feature-identity-v1`;
- Feature Set immutability;
- exact snapshot/version binding;
- feature computation and validation;
- feature Worker mode;
- no feature persistence.

Do not refactor predecessor behavior.

---

## 26. Persistence and Schema Protection

WP05 introduces no:

- experiment persistence;
- feature persistence expansion;
- registry/history/cache;
- SQL;
- migration;
- table;
- checkpoint;
- run history.

SQLite remains schema v2.

Schema delta: 0.

---

## 27. Explicit Deferrals

Do not implement placeholders for:

- additional experiments/statistics;
- variance/standard deviation;
- median/quantiles;
- cumulative/annualized return;
- Sharpe or other ratios;
- configurable summaries;
- persistence/registry/history;
- workspace/notebooks;
- visualization/API;
- strategies/signals/backtesting;
- portfolio/risk;
- AI/ML/MLOps;
- acquisition orchestration;
- scheduling/retries/recovery/checkpoints;
- plugins/DAGs/distributed execution;
- telemetry backends;
- Release 1.6 work.

---

## 28. Authorized File Mutation

Use `RELEASE_1.5_FILE_MANIFEST.md` as the exact path authority.

Before mutation:

1. enumerate exact WP05-authorized paths;
2. verify none belongs to WP06+;
3. verify no unexpected WP05 implementation already exists.

Modify/create only those paths.

Do not assume a filename from this prompt if the manifest differs.

If the manifest does not unambiguously assign the computation implementation, stop.

Expected category accounting:

- Application production: exact WP05 manifest delta;
- Domain production: 0;
- Infrastructure production: 0;
- Worker production: 0;
- permanent tests: 0;
- documentation/governance: 0 for WP05 implementation;
- package/project/reference/schema: 0/0/0/0.

Do not stage or commit.

---

## 29. No Permanent Tests in WP05

Do not add permanent tests.

Permanent Application experiment tests belong to WP10.

If necessary, use a removable offline probe to validate:

- empty computation;
- single-value computation;
- non-empty computation;
- exact decimal evidence;
- stable equivalent identity;
- different Feature Set identity distinctness;
- numeric-failure behavior consistent with the accepted seam.

Any probe must:

- be offline;
- use synthetic immutable evidence;
- make zero provider/network calls;
- use no real credentials;
- be removed before final validation;
- leave zero generated/database residue.

Do not change the permanent test count.

---

## 30. Targeted Semantic Validation

Before canonical verification, prove at minimum:

### Empty
- count 0;
- aggregates absent;
- successful identity-bearing result;
- identity bound to exact Feature Set.

### Single
- count 1;
- mean/min/max equal exact input.

### Non-empty
Use a small deterministic decimal sequence whose expected count/mean/min/max can be independently verified.

Confirm:

- all values included;
- no rounding;
- exact min/max;
- deterministic identity.

### Equivalence
Equivalent recomputation yields the same Experiment Result Identity.

### Distinct predecessor
Different Feature Set Identity with numerically identical summary evidence yields a distinct Experiment Result Identity.

### Culture
Computation and identity remain equivalent under at least one alternate culture if a removable probe is used.

### Failure
Numeric failure cannot produce partial successful evidence or fabricated result identity.

Do not retain temporary test/probe artifacts.

---

## 31. Technical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Expected final baseline:

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
- direct trailing-whitespace inspection of new/modified WP05 files and relevant untracked governance artifacts.

Require:

- temporary probe residue: 0;
- database/WAL/SHM/journal residue: 0;
- provider/network calls: 0;
- real credentials: 0.

---

## 32. Architecture Validation

Confirm:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure;
- unexpected edges: 0;
- cycles: 0.

Confirm:

- no Infrastructure experiment computation;
- no provider dependency;
- no new package/reference;
- no schema change.

---

## 33. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch an integration branch;
- push;
- create/merge a PR;
- tag;
- release;
- mutate packages/projects/references/schema;
- modify predecessor semantic authorities;
- begin WP06;
- begin Release 1.6.

Git transport mutation budget:

`0`

Repository mutation budget:

only exact manifest-authorized WP05 Application computation paths.

---

## 34. Authorized GitHub Mutation Budget

At WP05 start after gates pass:

1. #172 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP05 completion-evidence comment to #172;
3. close #172 as completed;
4. set #172 Project Status to Done.

Do not mutate #173.

Milestone #46 remains OPEN.

---

## 35. Completion Gate

WP05 may close only if:

- #171 is Closed/Done;
- #172 was In Progress during execution;
- #173 remains Open/Backlog;
- exact manifest-authorized WP05 path accounting passes;
- WP04 identity capability is reused rather than duplicated;
- empty/single/non-empty computation semantics pass;
- decimal-only arithmetic is preserved;
- equivalent recomputation identity is stable;
- different Feature Set identities remain result-distinct;
- no partial identity-bearing success occurs after failure;
- no WP06 validation/orchestration behavior was prematurely implemented;
- Domain/Infrastructure/Worker/test deltas are zero;
- package/project/reference/schema deltas are zero;
- SQLite remains v2;
- permanent tests remain 214/214;
- Architecture.Tests remain 13/13;
- build warnings/errors are 0/0;
- canonical verification passes;
- formatting/Gitleaks/whitespace pass;
- residue is zero;
- provider/network execution is zero;
- no Release 1.6 work exists.

If any gate fails, do not close #172 or mark it Done.

---

## 36. Completion Evidence Comment

On success, post concise evidence to #172 covering:

- exact Application computation files;
- accepted computation seam;
- WP04 identity capability reuse;
- `simple-return-descriptive-summary-v1`;
- empty/single/non-empty behavior;
- exact decimal count/mean/min/max;
- equivalent recomputation identity;
- different Feature Set identity distinctness;
- numeric fail-stop/no partial identity;
- no orchestration/DI/Worker/persistence;
- zero Domain/Infrastructure/Worker/test/package/reference/schema delta;
- SQLite v2;
- 214/214 tests and 13/13 Architecture.Tests;
- canonical verification/Gitleaks/whitespace PASS;
- #173 preserved Open/Backlog.

---

## 37. Final Read-Back

After successful closure verify:

- #172: CLOSED / Done;
- #173: OPEN / Backlog;
- #174–#180: unchanged Open / Backlog;
- milestone #46: OPEN;
- milestone counts: 8 open / 5 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted Release 1.5 candidate/governance artifacts accurately.

---

## 38. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #171 is not Closed/Done;
- #173+ started unexpectedly;
- WP05 file-manifest ownership is ambiguous;
- WP04 identity ownership contradicts accepted completion evidence;
- WP04 computation seam cannot support frozen semantics;
- implementation would require changing WP04 semantics/contracts beyond manifest authority;
- implementation requires WP06/WP07 behavior;
- premature later-WP implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail;
- a package/project/reference/schema change is required.

Report the smallest corrective authority required.

---

## 39. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. exact WP05 manifest paths;
6. WP04 contract/computation-seam reconciliation;
7. identity capability reuse;
8. computation implementation;
9. empty behavior;
10. single-value behavior;
11. non-empty count/mean/min/max;
12. decimal arithmetic and numeric failure behavior;
13. equivalent recomputation;
14. Feature Set identity distinctness;
15. provenance/lineage;
16. fail-stop/no-partial-success;
17. orchestration/DI/Worker/persistence exclusions;
18. predecessor/schema protection;
19. repository delta;
20. targeted semantic validation;
21. permanent test/build/canonical validation;
22. architecture/security/whitespace/residue;
23. GitHub lifecycle mutations;
24. final #172/#173/milestone state;
25. findings/blockers;
26. next authorized WP.

---

## 40. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP05 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP06 — Experiment Validation & Failure Semantics — GitHub issue #173`

Do not begin WP06.

If blocked, end:

`RELEASE 1.5 WP05 BLOCKED`

and identify the smallest corrective authority required.

# Release 1.4 --- WP06 Deterministic Feature Computation --- Codex Execution Authority

## Authority

You are executing **Release 1.4 --- WP06: Deterministic Feature
Computation** for:

-   Repository: `samuel-santos-engineer/AIQuantTradingResearch`
-   GitHub issue: **#158**
-   Milestone: **Phase 4 --- Release 1.4: Deterministic Feature
    Engineering Foundation**
-   Recommended model: **GPT-5.6 Terra**

This prompt is the authoritative execution contract for WP06. Read it
completely before any mutation.

The governing Release 1.4 artifacts are:

-   `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
-   `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
-   `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
-   `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`
-   accepted WP04 feature model
-   accepted WP05 feature-generation contracts

Also reconcile Releases 1.1--1.3 implementation, architecture, and
tests.

If repository truth or accepted predecessor semantics conflict
materially with this prompt, stop and report the conflict rather than
silently redefining behavior.

------------------------------------------------------------------------

## 1. Objective

Implement the exact deterministic Application-owned computation for the
single Release 1.4 built-in feature:

`simple-return-lag-1-v1`

WP06 must implement the pure transformation required by WP02 and exposed
through the WP05 `IFeatureComputer` seam.

The computation must:

-   consume already accepted ordered snapshot observations;
-   use exact .NET `decimal` arithmetic;
-   preserve current-observation `DateTimeOffset` timestamp and original
    offset;
-   emit one feature value for each adjacent valid pair;
-   produce valid empty output for zero or one input observation;
-   reject invalid numeric evidence rather than inventing values;
-   remain deterministic, culture-independent, timezone-independent,
    provider-independent, and storage-independent.

WP06 must not perform snapshot lookup, feature-generation orchestration,
DI registration, Worker execution, persistence, schema evolution, or
permanent feature testing.

------------------------------------------------------------------------

## 2. Mandatory Starting-State Gates

Before changing files, verify and report:

### Git/repository

-   branch is `main`;
-   `HEAD == origin/main`;
-   ahead/behind is `0/0`;
-   staged paths are `0`;
-   cumulative Release 1.4 candidate is preserved and classified;
-   no unexpected generated SQLite/WAL/SHM/journal or temporary residue
    exists.

Do not reset, clean, stash, stage, commit, or discard accepted
cumulative Release 1.4 work.

### Release lifecycle

Verify:

-   Release 1.3 remains closed;
-   WP01/#153 Closed/Done;
-   WP02/#154 Closed/Done;
-   WP03/#155 Closed/Done;
-   WP04/#156 Closed/Done;
-   WP05/#157 Closed/Done;
-   WP06/#158 Open/Backlog before execution;
-   WP07/#159 Open/Backlog and unstarted;
-   milestone #45 Open.

Only after all gates pass may #158 move Backlog → In Progress.

If any gate fails, stop with:

`RELEASE 1.4 WP06 BLOCKED`

------------------------------------------------------------------------

## 3. Accepted WP05 Contract Baseline

Reconcile the actual accepted contract surface before implementation.

Expected WP05 concepts include:

-   immutable feature-generation request;
-   exact `DatasetSnapshotIdentity`;
-   exact `DatasetVersion`;
-   `IFeatureGenerationUseCase`;
-   `IFeatureComputer`;
-   immutable success result carrying `FeatureSet`;
-   bounded failure vocabulary;
-   no computation implementation.

WP06 implements only the computation seam assigned by the accepted
contract.

Do not redesign WP05 contracts unless a genuine contradiction is found.
If a contract defect blocks correct implementation, stop and request the
smallest corrective authority.

------------------------------------------------------------------------

## 4. Frozen Computation Semantics

The sole authorized algorithm is:

`simple-return-lag-1-v1`

For accepted ordered prices:

``` text
p[0], p[1], ..., p[N-1]
```

compute:

``` text
r[i] = (p[i] / p[i-1]) - 1
```

for each `i` from `1` through `N-1`.

No alternative formula is authorized.

Do not implement:

-   log return;
-   percentage multiplication by 100;
-   rolling return;
-   configurable lag;
-   annualization;
-   normalization;
-   rounding to presentation precision;
-   other indicators.

------------------------------------------------------------------------

## 5. Input Boundary

WP06 must operate over accepted immutable ordered snapshot evidence
supplied through Application contracts/models.

It must not:

-   query SQLite;
-   call a catalog;
-   call a provider;
-   acquire live observations;
-   rematerialize a dataset;
-   inspect Worker configuration;
-   infer "latest" snapshot;
-   reorder source evidence.

Input ordering is authoritative.

------------------------------------------------------------------------

## 6. Decimal Arithmetic

The computation must use `decimal`.

Do not convert source or intermediate values to:

-   `double`;
-   `float`;
-   binary floating point of any kind.

Do not apply convenience rounding.

Do not apply culture-specific parsing or formatting inside semantic
computation.

Do not use string round-tripping as the arithmetic mechanism.

If a decimal operation cannot be represented under .NET decimal
semantics, treat that as invalid numeric evidence through the narrow
behavior authorized by WP05/WP07 boundaries. Do not silently clamp,
round, or convert.

WP07 owns the final failure-mapping hardening, so WP06 should not create
a broad new failure taxonomy.

------------------------------------------------------------------------

## 7. Zero and Invalid Numeric Evidence

The semantic rule requires a valid predecessor price.

At minimum:

``` text
p[i-1] = 0
```

must not produce a feature value.

Do not emit:

-   infinity;
-   NaN;
-   zero;
-   a sentinel;
-   a skipped row with partial success.

If accepted WP02 semantics or current model invariants also require
rejection of non-positive source prices, preserve that authority
exactly.

Do not broaden numeric validity beyond what is already frozen.

Invalid numeric evidence must fail the computation atomically: no
successful partial FeatureSet may be returned.

------------------------------------------------------------------------

## 8. Timestamp and Offset Fidelity

Each output feature value corresponds to current observation `i`.

Therefore use:

-   current observation `i` timestamp;
-   current observation `i` original offset.

Do not use:

-   predecessor timestamp;
-   execution time;
-   local system time;
-   UTC-only replacement that discards original offset;
-   synthetic timestamp.

The semantic instant and original offset must remain exactly
representable in the output model.

------------------------------------------------------------------------

## 9. Ordering and Cardinality

Input order is the accepted canonical snapshot order.

For valid input:

-   `N = 0` → 0 feature values;
-   `N = 1` → 0 feature values;
-   `N >= 2` → exactly `N - 1` feature values.

Output ordering corresponds exactly to:

``` text
(p[0], p[1]) → r[1]
(p[1], p[2]) → r[2]
...
(p[N-2], p[N-1]) → r[N-1]
```

Do not sort or deduplicate inside WP06.

Do not use hash/dictionary enumeration for semantic order.

------------------------------------------------------------------------

## 10. Determinism

Equivalent accepted ordered input and the same built-in definition must
produce equivalent output values.

Computation must not depend on:

-   current culture;
-   current UI culture;
-   local timezone;
-   wall-clock time;
-   random values;
-   process or machine identity;
-   filesystem paths;
-   provider ordering;
-   database natural order;
-   thread timing.

Where useful, a temporary probe may change culture/timezone context to
demonstrate invariance, but permanent test ownership remains WP11.

------------------------------------------------------------------------

## 11. Identity Responsibilities

WP03 froze feature identity semantics.

WP06 may participate in identity creation only to the extent explicitly
assigned by the accepted execution plan/manifest and existing code
decomposition.

Do not redesign identity semantics.

If a canonical identity computer is already expected to live in the same
Application feature area and is needed to construct valid `FeatureSet`
output, implement only the minimal deterministic identity computation
required by WP03.

If identity computation is clearly assigned to another later work
package by repository authority, do not implement it here.

In either case preserve:

-   `aiq-feature-identity-v1`;
-   Feature Definition Identity distinct from Feature Set Identity;
-   exact snapshot identity/version binding;
-   ordered timestamp/offset/decimal evidence;
-   empty FeatureSet identity;
-   equivalent recomputation identity;
-   different snapshot identity distinction;
-   culture-independent SHA-256 fingerprint semantics.

Do not include operational metadata.

If authority is ambiguous, stop rather than guessing.

------------------------------------------------------------------------

## 12. Provenance and Lineage

When constructing output evidence, reuse the accepted WP04 model and
WP03 semantics.

Do not:

-   redefine dataset provenance;
-   create cyclic lineage;
-   introduce run history;
-   introduce operational invocation identity;
-   persist provenance.

The produced FeatureSet must remain traceable to the exact accepted
snapshot/version and feature definition.

------------------------------------------------------------------------

## 13. Atomic Computation

The computation is all-or-nothing semantically.

If any adjacent pair contains invalid numeric evidence or another
bounded expected computation failure occurs:

-   do not return a successful partial FeatureSet;
-   do not silently omit the failing pair;
-   do not retain values computed before the failure as a successful
    result.

WP07 owns generalized validation/failure hardening, but WP06 must not
violate fail-stop semantics.

------------------------------------------------------------------------

## 14. Application Ownership

Expected WP06 production delta is Application-only under:

`src/AIQuantTradingResearch.Application/Features/`

Expected zero deltas:

-   Domain;
-   Infrastructure;
-   Worker.

Do not introduce new project references.

Production graph remains:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

No cycles.

------------------------------------------------------------------------

## 15. Explicitly Out of Scope

WP06 must not implement or modify:

-   snapshot/catalog lookup;
-   `IFeatureGenerationUseCase` orchestration;
-   WP07 validation redesign;
-   DI registration;
-   configuration;
-   Worker execution;
-   persistence;
-   schema migration;
-   feature table/catalog/cache;
-   provider/HTTP behavior;
-   Release 1.3 pipeline;
-   permanent tests;
-   architecture tests;
-   documentation alignment;
-   packages;
-   project references;
-   scheduling;
-   retries;
-   circuit breakers;
-   fallback;
-   checkpoints/resume;
-   DAGs/plugins;
-   arbitrary formulas;
-   configurable lag;
-   rolling indicators;
-   strategies;
-   backtesting;
-   ML/MLOps;
-   Release 1.5 work.

Do not broaden WP06 to solve later work.

------------------------------------------------------------------------

## 16. File-Manifest Discipline

Use `RELEASE_1.4_FILE_MANIFEST.md` as the path authority.

Before mutation:

1.  identify WP06-authorized paths;
2.  ensure all code changes remain within the authorized Application
    feature area;
3.  do not start WP07+ files;
4.  do not modify predecessor prompt/semantic artifacts.

If a required implementation path is outside manifest authority, stop.

Report exact files added/modified.

------------------------------------------------------------------------

## 17. Implementation Quality

Follow repository conventions.

Required properties:

-   nullable-safe;
-   warning-free;
-   deterministic;
-   immutable outputs;
-   explicit logic;
-   no hidden I/O;
-   no provider/storage dependency;
-   no clock/random/environment dependency;
-   no static mutable state;
-   no unnecessary abstraction.

Prefer one small explicit implementation over a generic
feature-engineering engine.

------------------------------------------------------------------------

## 18. Temporary Probe Authorization

WP06 may use temporary offline probes to demonstrate:

-   exact formula;
-   empty/single behavior;
-   multi-value cardinality;
-   timestamp/offset fidelity;
-   culture independence;
-   invalid numeric fail-stop;
-   identity stability if identity computation is legitimately
    WP06-owned.

Temporary probes must:

-   not become permanent tests;
-   use no provider/network;
-   use no real credentials;
-   use no persistent repository database;
-   be removed before final validation;
-   leave zero residue.

Permanent test delta remains `0`.

------------------------------------------------------------------------

## 19. Required Validation

After implementation run canonical Release verification.

At minimum prove:

-   restore PASS;
-   build PASS;
-   warnings `0`;
-   errors `0`;
-   Domain.Tests PASS;
-   Application.Tests PASS;
-   Infrastructure.Tests PASS;
-   Architecture.Tests PASS;
-   permanent total unchanged from accepted baseline;
-   Gitleaks PASS;
-   format verification PASS;
-   `git diff --check` PASS;
-   `git diff --cached --check` PASS;
-   direct trailing-whitespace checks for untracked WP06 files PASS;
-   temporary probe residue `0`;
-   database/WAL/SHM/journal residue `0`;
-   provider/network calls `0`;
-   real credentials `0`;
-   package delta `0`;
-   project-reference delta `0`;
-   schema delta `0`;
-   permanent-test delta `0`;
-   production graph unchanged and acyclic.

Expected baseline entering WP06:

-   Domain.Tests: `11`
-   Application.Tests: `77`
-   Infrastructure.Tests: `96`
-   Architecture.Tests: `13`
-   Permanent total: `197`

Repository truth remains authoritative if an earlier accepted change
legitimately altered counts.

------------------------------------------------------------------------

## 20. Regression Protection

Confirm no behavior change to Releases 1.1--1.3 and WP02--WP05.

In particular preserve:

-   observation fidelity;
-   dataset semantics;
-   snapshot/version semantics;
-   catalog behavior;
-   pipeline topology;
-   pipeline identity/evidence;
-   existing Worker behavior;
-   WP02 feature semantics;
-   WP03 feature identity/provenance semantics;
-   WP04 immutable model;
-   WP05 contracts.

------------------------------------------------------------------------

## 21. Git and GitHub Protection

WP06 is not an integration work package.

Do not:

-   stage;
-   commit;
-   push;
-   branch;
-   create/modify PR;
-   merge;
-   tag;
-   create release;
-   mutate unrelated issues;
-   start WP07.

Authorized lifecycle for #158 only:

1.  Backlog → In Progress after starting gates pass;
2.  completion evidence after all acceptance gates pass;
3.  close #158;
4.  set Project #2 status Done.

Verify #159 remains Open/Backlog and untouched.

------------------------------------------------------------------------

## 22. Acceptance Criteria

WP06 completes only if:

1.  starting gates pass;
2.  accepted WP05 seam is reused;
3.  exact `simple-return-lag-1-v1` computation is implemented;
4.  decimal arithmetic only;
5.  no convenience rounding;
6.  current-observation timestamp/offset preserved;
7.  empty input yields empty output;
8.  single input yields empty output;
9.  valid `N` input yields exactly `N-1` ordered values;
10. invalid numeric evidence fails atomically;
11. no partial success;
12. deterministic/culture-independent behavior;
13. identity semantics preserved;
14. provenance/lineage preserved;
15. Application-only production change;
16. no snapshot lookup/orchestration;
17. no persistence/schema evolution;
18. no DI/Worker change;
19. no permanent-test change;
20. no package/reference change;
21. Release 1.3 pipeline unchanged;
22. canonical verification PASS;
23. Gitleaks/format/whitespace PASS;
24. residue `0`;
25. WP07 remains unstarted;
26. #158 Closed/Done only after successful completion.

------------------------------------------------------------------------

## 23. Stop Conditions

Stop immediately with:

`RELEASE 1.4 WP06 BLOCKED`

if:

-   predecessor lifecycle is invalid;
-   WP05 contract cannot support correct computation;
-   manifest does not authorize required files;
-   accepted semantic authorities conflict;
-   identity ownership is materially ambiguous;
-   correct computation requires persistence/schema changes;
-   correct computation requires provider/storage coupling;
-   a generalized feature framework appears necessary;
-   a package/project reference is required;
-   WP07 or Release 1.5 work has already started;
-   canonical validation fails for a reason that cannot be corrected
    within WP06.

Do not broaden scope.

------------------------------------------------------------------------

## 24. Required Final Report

Produce an evidence-rich report containing:

1.  Executive summary.
2.  Authorities reviewed.
3.  Repository/Git baseline.
4.  Working-tree classification.
5.  Predecessor/lifecycle gates.
6.  Initial canonical baseline.
7.  WP05 contract reconciliation.
8.  Computation design.
9.  Exact formula implementation.
10. Decimal arithmetic evidence.
11. Ordering behavior.
12. Timestamp/offset fidelity.
13. Empty-input behavior.
14. Single-input behavior.
15. Multi-input cardinality.
16. Zero/invalid numeric behavior.
17. Atomic fail-stop behavior.
18. Determinism/culture/timezone evidence.
19. Identity responsibility decision.
20. Identity stability evidence if applicable.
21. Provenance/lineage preservation.
22. Release 1.3 pipeline protection.
23. Explicit orchestration exclusion.
24. Persistence/schema exclusion.
25. DI/Worker exclusion.
26. Exact files added/modified.
27. Layer deltas.
28. Package/reference/schema delta.
29. Permanent-test delta.
30. Temporary probe evidence.
31. Restore/build evidence.
32. Permanent test counts.
33. Canonical verification.
34. Architecture validation.
35. Release 1.1--1.3 regression evidence.
36. WP02--WP05 regression evidence.
37. Security/offline evidence.
38. Whitespace/diff evidence.
39. Database/generated residue.
40. Mutation accounting.
41. Git/GitHub protection.
42. Final #158/#159 lifecycle state.
43. Findings/blockers.
44. Final decision.
45. Next authorized work package.

On success terminate exactly with:

`RELEASE 1.4 WP06 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP07 — Feature Validation & Failure Mapping — GitHub issue #159`

Do not start WP07.

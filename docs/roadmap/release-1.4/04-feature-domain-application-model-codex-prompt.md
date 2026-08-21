# Release 1.4 --- WP04 Feature Domain/Application Model --- Codex Execution Authority

## Authority

You are executing **Release 1.4 --- WP04: Feature Domain/Application
Model** for:

-   Repository: `samuel-santos-engineer/AIQuantTradingResearch`
-   GitHub issue: **#156**
-   Milestone: **Phase 4 --- Release 1.4: Deterministic Feature
    Engineering Foundation**
-   Model recommendation: **GPT-5.6 Terra**

This prompt is the authoritative execution contract for WP04. Read it
completely before making any mutation.

The governing Release 1.4 artifacts are:

-   `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
-   `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
-   `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
-   `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`

Also reconcile the accepted Release 1.1--1.3 implementation,
architecture, tests, and current GitHub lifecycle state.

If this prompt conflicts with the Release 1.4 definition, execution
plan, file manifest, WP02 semantics, or WP03
identity/provenance/evidence semantics, **stop and report the conflict
rather than inventing a resolution**.

------------------------------------------------------------------------

## 1. Objective

Implement the **minimum immutable Domain/Application model** required to
represent the Release 1.4 deterministic feature-engineering semantics
already frozen by WP02 and WP03.

WP04 is a modeling work package.

It must translate the accepted semantic authorities into narrow,
strongly typed, immutable code concepts without implementing feature
computation, orchestration, persistence, dependency injection, Worker
execution, or permanent tests.

The model must be sufficient for later work packages to express:

1.  the single supported feature definition `simple-return-lag-1-v1`;
2.  canonical Feature Definition Identity;
3.  canonical Feature Set Identity;
4.  immutable feature values;
5.  exact timestamp/offset and decimal evidence;
6.  immutable feature-set evidence;
7.  provenance/lineage references to the accepted dataset
    snapshot/version;
8.  empty feature sets;
9.  construction-time invariants that are already unambiguously frozen
    by WP02/WP03.

Do not design a general feature framework.

------------------------------------------------------------------------

## 2. Mandatory Starting-State Gates

Before changing files, verify and report:

### Git/repository

-   current branch is `main`;
-   `HEAD == origin/main`;
-   ahead/behind is `0/0`;
-   staged paths are `0`;
-   cumulative Release 1.4 work is classified against the authoritative
    file manifest;
-   no unexpected generated SQLite/WAL/SHM/journal or temporary residue
    exists.

Do not discard, reset, clean, stage, commit, stash, or otherwise alter
accepted cumulative Release 1.4 work.

### Release lifecycle

Verify:

-   Release 1.3 is closed;
-   WP01/#153 is Closed/Done;
-   WP02/#154 is Closed/Done;
-   WP03/#155 is Closed/Done;
-   WP04/#156 is Open/Backlog before execution;
-   WP05 remains Open/Backlog and unstarted;
-   the Release 1.4 milestone remains Open.

Only after all starting gates pass may #156 move from Backlog to In
Progress.

If any mandatory gate fails, stop with `RELEASE 1.4 WP04 BLOCKED`.

------------------------------------------------------------------------

## 3. Semantic Authorities to Preserve

### 3.1 Release 1.1--1.3

Preserve without modification:

-   persisted historical-observation semantics;
-   exact decimal values;
-   exact `DateTimeOffset` timestamp and offset fidelity;
-   deterministic `[from,to)` dataset selection;
-   immutable dataset snapshots and versions;
-   dataset catalog behavior;
-   `aiq-dataset-identity-v1`;
-   `aiq-pipeline-identity-v1`;
-   Release 1.3 fixed five-stage pipeline;
-   Release 1.3 one-shot Worker boundary;
-   existing failure and integrity semantics;
-   SQLite schema version `2`.

Feature engineering is **not** a sixth Release 1.3 pipeline stage.

### 3.2 WP02 feature semantics

Model exactly one built-in feature definition:

`simple-return-lag-1-v1`

Its later computation is:

`r[i] = (p[i] / p[i-1]) - 1`

WP04 must represent the model required by that semantic rule, but **must
not implement the calculation**.

Frozen semantics include:

-   adjacency is defined only by accepted snapshot ordering;
-   output belongs to current observation `i`;
-   output preserves current observation `i` timestamp and original
    offset;
-   feature numeric values use `decimal`;
-   empty snapshot → valid empty feature set;
-   one-observation snapshot → valid empty feature set;
-   `N >= 2` → later computation yields exactly `N-1` ordered values;
-   zero predecessor is invalid numeric evidence;
-   no NaN/infinity/sentinel/skipped-pair/partial-success semantics;
-   output is immutable and deterministic;
-   feature generation remains provider/storage independent.

### 3.3 WP03 identity semantics

Preserve exactly:

-   identity scheme: `aiq-feature-identity-v1`;
-   SHA-256 fingerprint semantics;
-   fingerprint representation: exactly 64 lowercase hexadecimal
    characters;
-   distinct **Feature Definition Identity** and **Feature Set
    Identity**;
-   Feature Definition Identity represents only the semantics of
    `simple-return-lag-1-v1`;
-   Feature Set Identity binds:
    -   Feature Definition Identity;
    -   exact dataset snapshot identity/version;
    -   feature-set cardinality;
    -   ordered timestamp/offset/decimal feature evidence;
-   decimal identity semantics are exact and culture independent;
-   timestamp identity semantics preserve UTC instant plus original
    offset;
-   identity derivation remains acyclic;
-   empty feature sets have deterministic identities bound to their
    exact snapshot;
-   equivalent recomputation produces equivalent Feature Set Identity;
-   equal numeric outputs from different snapshots remain
    identity-distinct;
-   operational invocation metadata is non-semantic;
-   contradictory canonical content under an equal identity remains an
    integrity contradiction.

**WP04 must not compute SHA-256 fingerprints or implement canonical
identity serialization.** That belongs to later work unless the
execution plan explicitly assigns it elsewhere.

------------------------------------------------------------------------

## 4. Required Design Investigation

Before coding, inspect current Domain and Application conventions,
especially existing Release 1.2/1.3 models for:

-   typed identities;
-   immutable records/classes;
-   constructor/factory validation;
-   snapshot/version representation;
-   dataset provenance and lineage;
-   pipeline definition/result/evidence types;
-   namespace organization;
-   visibility conventions;
-   argument validation;
-   collection immutability;
-   exception conventions.

Prefer established repository patterns over introducing new
abstractions.

Document in the final report which existing patterns were reused.

------------------------------------------------------------------------

## 5. Layer Ownership Decision

Apply **zero-delta-first** reasoning to Domain.

Do not add feature concepts to Domain merely because the work-package
title says "Domain/Application Model."

The Release 1.4 definition expects feature semantics primarily in
Application. If current architecture shows that feature concepts are
research/application concerns, keep them in Application.

A Domain change is authorized only if repository truth proves that an
invariant is genuinely domain-owned and cannot be represented correctly
at the Application boundary.

If no Domain change is justified, explicitly report:

`Domain delta: 0`

Do not weaken architecture boundaries to force a Domain delta.

------------------------------------------------------------------------

## 6. Minimum Model Surface

Implement only the smallest model justified by the authorities and
repository conventions.

The resulting model should be capable of representing the following
concepts, with exact names chosen according to repository conventions:

### Feature definition

An immutable representation of the supported definition.

Requirements:

-   exactly one supported semantic definition in Release 1.4;
-   stable identifier/name corresponding to `simple-return-lag-1-v1`;
-   no arbitrary formula text;
-   no configurable lag;
-   no plugin metadata;
-   no mutable version field;
-   no runtime/provider/storage configuration.

### Typed feature identities

Represent:

-   Feature Definition Identity;
-   Feature Set Identity.

They must:

-   be non-interchangeable types;
-   enforce the already-frozen fingerprint representation where
    consistent with existing identity types;
-   not compute their own fingerprints unless that behavior is already
    the repository's established pure model pattern and is explicitly
    within WP04 manifest authority;
-   remain independent of pipeline/dataset identity types while allowing
    explicit provenance references.

### Feature value

Represent one immutable feature value with:

-   timestamp;
-   preserved timestamp offset;
-   exact `decimal` value.

Do not use `double` or `float`.

Do not add rounded/display values to the semantic model.

### Feature set

Represent immutable ordered feature evidence associated with:

-   Feature Definition Identity;
-   Feature Set Identity;
-   exact source Dataset Snapshot Identity;
-   exact source Dataset Snapshot Version;
-   ordered feature values;
-   cardinality implied by the collection.

The model must permit an empty feature set.

It must not use a global empty identity or sentinel value.

### Provenance / lineage

Represent only the minimum references required by WP03.

Do not duplicate or redefine accepted Release 1.2 dataset provenance.

Do not create cyclic lineage.

Do not introduce operational invocation identity, durable run identity,
timestamps for execution, logging correlation, paths, provider
information, or persistence metadata.

------------------------------------------------------------------------

## 7. Construction Invariants

Enforce only invariants already frozen by authority or clearly required
for internally valid immutable objects.

Examples that should be considered where applicable:

-   null arguments rejected;
-   identity fingerprints conform to the accepted representation;
-   collections cannot be mutated through the public surface;
-   ordered values are retained in supplied semantic order;
-   feature-set cardinality is derived rather than independently
    mutable;
-   feature values use valid timestamp/offset representation;
-   source snapshot identity/version must be present;
-   definition identity and feature-set identity are distinct typed
    concepts.

Do **not** prematurely enforce invariants that require feature
computation or canonical identity recomputation.

In particular, WP04 must not attempt to prove that:

-   `N` source observations correspond to `N-1` feature values;
-   a feature value equals the lag-1 formula;
-   a Feature Set Identity matches canonical content;
-   snapshot contents match the supplied snapshot identity/version.

Those require later computation/validation seams.

------------------------------------------------------------------------

## 8. Explicitly Out of Scope

WP04 must not implement or modify:

-   feature computation;
-   the lag-1 formula execution;
-   feature-generation use cases;
-   orchestration;
-   snapshot lookup;
-   catalog lookup;
-   feature identity hashing/canonical serialization unless explicitly
    assigned by the manifest;
-   feature validation workflow beyond model construction invariants;
-   failure mapping/use-case result semantics;
-   persistence;
-   SQLite tables or migrations;
-   feature catalog/cache;
-   schema version 3;
-   Infrastructure feature implementations;
-   dependency injection;
-   configuration;
-   Worker execution;
-   Release 1.3 pipeline stages;
-   permanent tests;
-   architecture tests;
-   documentation alignment beyond code comments strictly required by
    repository convention;
-   new packages;
-   project references;
-   scheduling;
-   retries;
-   circuit breakers;
-   DAGs;
-   plugins;
-   arbitrary formulas;
-   configurable lags;
-   rolling indicators;
-   strategies;
-   backtesting;
-   notebooks/workspaces;
-   ML/MLOps;
-   Release 1.5 implementation.

If correct modeling appears to require any item above, stop and report
the authority conflict.

------------------------------------------------------------------------

## 9. File-Manifest Discipline

Use `RELEASE_1.4_FILE_MANIFEST.md` as the exact path authority.

Before mutation:

1.  identify the WP04-authorized paths;
2.  verify whether each is create/modify/inspect-only;
3.  do not mutate any path outside WP04 authority;
4.  do not rename manifest-governed files without separate authority.

If repository truth requires a path not authorized by the manifest, stop
rather than expanding scope.

At completion, report every added or modified path.

------------------------------------------------------------------------

## 10. Implementation Quality

Follow current repository engineering standards.

Required characteristics:

-   nullable-safe;
-   warning-free;
-   immutable public semantic surface;
-   deterministic;
-   culture independent;
-   provider independent;
-   storage independent;
-   no hidden I/O;
-   no static mutable state;
-   no clock/random/environment dependency;
-   no secret-bearing fields;
-   no unnecessary abstractions;
-   no speculative extensibility.

Prefer small explicit types over a generic framework.

Do not add comments that merely restate code. Add documentation only
where it protects a non-obvious semantic invariant and matches
repository conventions.

------------------------------------------------------------------------

## 11. Temporary Validation

WP04 has no permanent-test mandate.

You may use a **temporary, offline, deterministic probe** only if needed
to establish model construction behavior that cannot be proven
adequately by build/static inspection.

Any temporary probe must:

-   stay outside the permanent candidate;
-   use no provider/network access;
-   use no real credentials;
-   create no durable database state;
-   be removed before final validation;
-   leave zero residue.

Do not convert WP04 into WP11 semantic-test work.

Permanent test delta should remain `0`.

------------------------------------------------------------------------

## 12. Required Validation

After implementation, run the repository's canonical validation in
Release configuration.

At minimum prove:

-   restore passes;
-   build passes;
-   warnings: `0`;
-   errors: `0`;
-   Domain.Tests retain the accepted baseline unless independently
    changed by authorized prior work;
-   Application.Tests retain the accepted baseline;
-   Infrastructure.Tests retain the accepted baseline;
-   Architecture.Tests retain the accepted baseline;
-   all permanent tests pass;
-   Gitleaks passes;
-   formatting verification passes;
-   `git diff --check` passes;
-   `git diff --cached --check` passes;
-   no SQLite/WAL/SHM/journal residue;
-   no provider/network calls;
-   no real credentials;
-   package delta `0`;
-   project-reference delta `0`;
-   schema delta `0`;
-   permanent-test delta `0`;
-   production dependency graph remains unchanged and acyclic.

Also directly inspect new untracked files for trailing whitespace
because ordinary `git diff --check` may not report untracked files.

------------------------------------------------------------------------

## 13. Regression Protection

Confirm that WP04 did not alter:

### Release 1.1

-   historical observation contracts/persistence;
-   timestamp and decimal fidelity;
-   idempotency/conflict semantics.

### Release 1.2

-   dataset definition;
-   dataset identities;
-   snapshot/version identity;
-   immutable snapshot persistence;
-   catalog behavior;
-   provenance/lineage.

### Release 1.3

-   pipeline identity;
-   five-stage topology;
-   pipeline orchestration;
-   evidence;
-   failure semantics;
-   DI/configuration;
-   one-shot Worker behavior.

No predecessor behavior should require modification for WP04.

------------------------------------------------------------------------

## 14. Git and GitHub Protection

WP04 is not an integration work package.

Do not:

-   stage files;
-   commit;
-   push;
-   create a branch;
-   create or modify a PR;
-   merge;
-   tag;
-   create a GitHub Release;
-   alter the milestone except through explicitly authorized issue
    lifecycle;
-   modify unrelated issues;
-   start WP05.

The only GitHub lifecycle mutation authorized after starting gates pass
is for issue #156:

1.  Backlog → In Progress when work actually begins;
2.  post bounded completion evidence after all acceptance gates pass;
3.  close #156;
4.  set Project #2 status to Done.

Verify WP05 remains Open/Backlog and untouched.

If WP04 blocks, leave #156 open unless the established governance
authority explicitly requires a different blocked-state action.

------------------------------------------------------------------------

## 15. Acceptance Criteria

WP04 is complete only if all applicable statements are true:

1.  Starting-state gates passed.
2.  WP02 and WP03 authorities were preserved.
3.  The model represents exactly one Release 1.4 feature definition.
4.  Feature Definition Identity and Feature Set Identity are typed and
    non-interchangeable.
5.  Feature values preserve exact timestamp/offset and `decimal`
    evidence.
6.  Feature sets are immutable and ordered.
7.  Empty feature sets are valid.
8.  Feature sets bind to exact snapshot identity/version.
9.  Provenance/lineage remains acyclic and reuses accepted dataset
    evidence.
10. No computation was implemented.
11. No generalized feature engine was introduced.
12. No persistence/schema evolution was introduced.
13. No DI/configuration/Worker behavior was introduced.
14. No permanent tests were added or modified by WP04.
15. No package/project-reference changes occurred.
16. Production dependency graph is unchanged and acyclic.
17. Canonical verification passes.
18. Gitleaks passes.
19. Whitespace/diff checks pass.
20. No database/generated residue remains.
21. No provider/network activity occurred.
22. WP05 remains unstarted.
23. #156 is Closed/Done only after successful completion.

------------------------------------------------------------------------

## 16. Stop Conditions

Immediately stop and report `RELEASE 1.4 WP04 BLOCKED` if:

-   a predecessor lifecycle gate is not satisfied;
-   the manifest does not authorize a required path;
-   WP02 and WP03 conflict materially;
-   correct modeling requires schema evolution;
-   correct modeling requires a new package/project reference;
-   correct modeling requires modifying Release 1.1--1.3 semantics;
-   a generalized feature engine appears necessary;
-   canonical validation fails for a reason caused by WP04 and cannot be
    corrected within WP04 scope;
-   an unexpected repository/GitHub mutation is detected;
-   a secret or credential is discovered;
-   WP05 or Release 1.5 implementation has already started unexpectedly.

Do not broaden authority to solve a blocker.

------------------------------------------------------------------------

## 17. Required Final Report

Produce a concise but evidence-rich execution report containing:

1.  Executive summary.
2.  Authorities reviewed.
3.  Repository/Git baseline.
4.  Working-tree classification.
5.  Predecessor/lifecycle gates.
6.  Initial test/verification baseline.
7.  Existing model-pattern inventory.
8.  Domain ownership decision and delta.
9.  Application ownership decision.
10. Feature definition model.
11. Typed identity model.
12. Feature-value model.
13. Feature-set model.
14. Empty feature-set behavior.
15. Snapshot identity/version binding.
16. Provenance/lineage representation.
17. Construction invariants.
18. WP02 semantic preservation.
19. WP03 identity preservation.
20. Explicit computation exclusion.
21. Persistence/schema exclusion.
22. DI/Worker exclusion.
23. Files added/modified.
24. Layer deltas.
25. Package/reference/schema delta.
26. Permanent-test delta.
27. Temporary probe evidence, if any.
28. Release 1.1--1.3 regression evidence.
29. Restore/build evidence.
30. Permanent test counts.
31. Canonical verification result.
32. Architecture validation.
33. Security/offline evidence.
34. Whitespace/diff evidence.
35. Database/generated residue.
36. Mutation accounting.
37. Git/GitHub protection.
38. Final #156/#157 lifecycle state.
39. Findings/blockers.
40. Final decision.
41. Next authorized work package.

On success, terminate with exactly:

`RELEASE 1.4 WP04 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP05 — Feature Application Contracts — GitHub issue #157`

Do not start WP05.

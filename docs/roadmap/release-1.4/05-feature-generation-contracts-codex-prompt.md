# Release 1.4 --- WP05 Feature Generation Contracts --- Codex Execution Authority

## Authority

You are executing **Release 1.4 --- WP05: Feature Generation Contracts**
for:

-   Repository: `samuel-santos-engineer/AIQuantTradingResearch`
-   GitHub issue: **#157**
-   Milestone: **Phase 4 --- Release 1.4: Deterministic Feature
    Engineering Foundation**
-   Recommended model: **GPT-5.6 Terra**

This prompt is the authoritative execution contract for WP05. Read it
completely before making any mutation.

The governing Release 1.4 artifacts are:

-   `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
-   `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
-   `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
-   `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
-   `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`
-   the accepted WP04 Application feature model

Also reconcile the accepted Release 1.1--1.3 implementation,
architecture, tests, and current GitHub lifecycle state.

If this prompt conflicts with the Release 1.4 definition, execution
plan, file manifest, WP02/WP03 semantics, or accepted WP04 model, stop
and report the conflict rather than inventing a resolution.

------------------------------------------------------------------------

## 1. Objective

Define the **minimum Application-owned contract surface** required for
deterministic feature generation in later work packages.

WP05 establishes contracts only.

The contracts must allow a caller to request generation of the single
Release 1.4 feature:

`simple-return-lag-1-v1`

from an exact accepted dataset snapshot/version, and must provide a
narrow result/failure vocabulary suitable for later deterministic
computation and integration.

WP05 must not implement the lag-1 calculation, snapshot lookup
orchestration, identity hashing, dependency injection, Worker behavior,
persistence, or permanent tests.

------------------------------------------------------------------------

## 2. Mandatory Starting-State Gates

Before changing files, verify and report:

### Git/repository

-   current branch is `main`;
-   `HEAD == origin/main`;
-   ahead/behind is `0/0`;
-   staged paths are `0`;
-   cumulative Release 1.4 work is preserved and classified against
    `RELEASE_1.4_FILE_MANIFEST.md`;
-   no unexpected generated SQLite/WAL/SHM/journal or temporary residue
    exists.

Do not reset, clean, stash, stage, commit, discard, or rewrite accepted
cumulative Release 1.4 work.

### Release lifecycle

Verify:

-   Release 1.3 remains closed;
-   WP01/#153 is Closed/Done;
-   WP02/#154 is Closed/Done;
-   WP03/#155 is Closed/Done;
-   WP04/#156 is Closed/Done;
-   WP05/#157 is Open/Backlog before execution;
-   WP06 remains Open/Backlog and unstarted;
-   milestone #45 remains Open.

Only after all starting gates pass may #157 move Backlog → In Progress.

If any mandatory gate fails, stop with:

`RELEASE 1.4 WP05 BLOCKED`

------------------------------------------------------------------------

## 3. Accepted WP04 Model Baseline

Reconcile the actual accepted WP04 implementation before designing
contracts.

Expected WP04 model includes:

-   `FeatureDefinitionIdentity`;
-   `FeatureSetIdentity`;
-   the `aiq-feature-identity-v1` scheme;
-   the sole built-in `simple-return-lag-1-v1` definition;
-   immutable feature values;
-   immutable feature sets;
-   exact dataset snapshot/version binding;
-   provenance/lineage references;
-   Application ownership;
-   Domain delta `0`.

Do not duplicate these concepts in a second contract hierarchy.

Prefer direct reuse of the accepted model.

If actual repository truth differs materially from this handoff, stop
and reconcile against the authoritative WP04 result and Release 1.4
manifest before mutation.

------------------------------------------------------------------------

## 4. Semantic Authorities to Preserve

### 4.1 Feature semantics

Preserve exactly:

-   one supported built-in transformation: `simple-return-lag-1-v1`;
-   formula reserved for WP06: `r[i] = (p[i] / p[i-1]) - 1`;
-   adjacency derives only from accepted snapshot order;
-   result belongs to current observation `i`;
-   timestamp and original offset of observation `i` are preserved;
-   arithmetic is decimal-only;
-   empty snapshot → successful empty feature set;
-   one-observation snapshot → successful empty feature set;
-   `N >= 2` → later computation produces exactly `N-1` values;
-   zero predecessor is invalid numeric evidence;
-   no NaN, infinity, sentinel, skipped pair, convenience rounding, or
    partial success.

### 4.2 Identity/provenance semantics

Preserve exactly:

-   `aiq-feature-identity-v1`;
-   distinct Feature Definition Identity and Feature Set Identity;
-   canonical SHA-256 identity semantics frozen by WP03;
-   feature-set identity binds definition, exact snapshot
    identity/version, cardinality, and ordered feature evidence;
-   empty feature sets remain identity-bearing and snapshot-specific;
-   equivalent recomputation remains identity-equivalent;
-   equal numeric outputs from different snapshots remain
    identity-distinct;
-   lineage remains acyclic;
-   operational invocation metadata is non-semantic;
-   contradictory canonical content under equal identity is an integrity
    contradiction.

WP05 must not implement canonical hashing or identity computation unless
the execution plan/file manifest explicitly assigns a narrow contract
seam for later computation. A contract for identity computation may be
defined only if required by the authoritative plan; its implementation
remains out of scope.

### 4.3 Predecessor preservation

Do not alter:

-   Release 1.1 historical persistence;
-   Release 1.2 dataset/snapshot/catalog semantics;
-   Release 1.3 fixed five-stage pipeline;
-   Release 1.3 pipeline identities/evidence;
-   existing schema v2;
-   existing production dependency graph.

Feature generation remains separate from the Release 1.3 pipeline and is
not a sixth pipeline stage.

------------------------------------------------------------------------

## 5. Required Design Investigation

Before coding, inspect current Application conventions for:

-   use-case interfaces;
-   request/result contracts;
-   failure/result modeling;
-   `NotFound` representation;
-   unavailable dependency representation;
-   invalid evidence/integrity conflict representation;
-   typed identities;
-   snapshot/catalog lookup contracts;
-   construction invariants;
-   namespace and file organization;
-   visibility and immutability conventions.

Reuse established vocabulary whenever it is semantically correct.

Do not invent a new generic Result framework or exception hierarchy.

Report the patterns reused.

------------------------------------------------------------------------

## 6. Required Contract Boundary

Define the minimum contracts needed so later work can express:

**explicit feature-generation request** → **exact snapshot/version input
boundary** → **deterministic feature computation** → **immutable feature
evidence/result**

The contract surface should make the following distinctions explicit
where the authoritative plan assigns them to WP05:

### Request

A request must identify, directly or through accepted typed objects:

-   the supported feature definition;
-   the exact dataset snapshot identity;
-   the exact dataset snapshot version.

It must not contain:

-   provider configuration;
-   SQL or database paths;
-   connection strings;
-   arbitrary formulas;
-   configurable lag;
-   scheduler data;
-   retry policy;
-   invocation timestamps;
-   correlation IDs;
-   persistence disposition;
-   pipeline execution identity.

### Use-case seam

Provide the narrow Application seam required for later feature
generation.

It should represent a one-shot synchronous Application operation unless
existing repository conventions and the execution plan explicitly
require otherwise.

Do not introduce:

-   background execution;
-   streaming;
-   callbacks;
-   event buses;
-   plugin discovery;
-   DAG execution;
-   scheduling.

### Result

A successful result must be able to carry the accepted immutable
feature-set evidence from WP04.

Success must support:

-   non-empty feature set;
-   empty feature set.

Do not introduce a special empty sentinel.

### Failure vocabulary

Preserve the Release 1.4 definition's required distinctions without
over-modeling:

-   invalid request;
-   unsupported feature definition;
-   `NotFound`;
-   unavailable dependency;
-   invalid evidence;
-   invalid numeric input;
-   integrity contradiction/conflict;
-   successful empty/non-empty result;
-   unknown defects propagate rather than being silently normalized.

Use existing repository vocabulary where equivalent semantics already
exist.

WP05 defines contract semantics; WP07 owns validation/failure hardening.
Do not pre-implement WP07.

------------------------------------------------------------------------

## 7. Snapshot Boundary

The feature-generation contract must operate against an **exact accepted
immutable dataset snapshot/version**.

Do not make live historical acquisition part of this contract.

Do not make a broad dataset query substitute for exact snapshot
identity/version.

Do not introduce feature persistence.

If a lookup seam is required by the authoritative execution plan, prefer
reuse of the existing Release 1.2 catalog/snapshot abstractions rather
than creating a duplicate storage contract.

Infrastructure details must not leak into Application contracts.

------------------------------------------------------------------------

## 8. Immutability and Construction Invariants

Contracts must be immutable or expose immutable semantic state according
to repository conventions.

Enforce only construction invariants already established by authority,
such as:

-   required typed identities are present;
-   required definitions are present;
-   invalid null inputs are rejected;
-   success cannot exist without valid feature evidence;
-   failure cannot masquerade as success;
-   bounded failure categories are internally coherent;
-   collections are not externally mutable.

Do not perform WP06 computation or WP07 semantic validation inside
constructors.

Do not recompute snapshot identity, feature identity, formula output, or
canonical evidence merely to construct a request/result.

------------------------------------------------------------------------

## 9. Layer Ownership

WP05 is expected to be **Application-only**.

Apply zero-delta-first reasoning to:

-   Domain;
-   Infrastructure;
-   Worker.

Expected:

-   Domain delta: `0`;
-   Infrastructure delta: `0`;
-   Worker delta: `0`.

If correct contracts require another production layer to change, verify
that the manifest explicitly authorizes it. Otherwise stop as blocked.

The production graph must remain:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

No cycles or new project references.

------------------------------------------------------------------------

## 10. Explicitly Out of Scope

WP05 must not implement or modify:

-   lag-1 computation;
-   decimal division logic;
-   feature identity hashing;
-   canonical serialization implementation;
-   snapshot retrieval orchestration;
-   feature-generation orchestration implementation;
-   persistence;
-   feature tables;
-   feature catalog/cache;
-   SQLite schema evolution;
-   Infrastructure feature implementation;
-   DI registration;
-   configuration;
-   Worker execution;
-   Release 1.3 pipeline topology;
-   permanent tests;
-   architecture tests;
-   documentation alignment;
-   packages;
-   project references;
-   live acquisition;
-   HTTP/provider behavior;
-   scheduling;
-   retries;
-   circuit breakers;
-   durable run history;
-   checkpoints/resume;
-   plugins;
-   arbitrary formulas;
-   configurable lags;
-   rolling indicators;
-   DAGs;
-   strategies;
-   backtesting;
-   ML/MLOps;
-   Release 1.5 work.

If any of these appears necessary, stop rather than expanding WP05.

------------------------------------------------------------------------

## 11. File-Manifest Discipline

`RELEASE_1.4_FILE_MANIFEST.md` is the exact path authority.

Before mutation:

1.  identify all WP05-authorized paths;
2.  classify each as create/modify/inspect-only;
3.  verify no WP06+ path is being started;
4.  do not mutate paths outside WP05 authority.

If the correct contract requires an unauthorized file/path, stop with a
manifest blocker.

At completion, enumerate every added/modified file.

------------------------------------------------------------------------

## 12. Implementation Quality

Follow current repository engineering standards.

Required characteristics:

-   nullable-safe;
-   warning-free;
-   strongly typed;
-   immutable;
-   deterministic;
-   provider independent;
-   storage independent;
-   culture independent;
-   no hidden I/O;
-   no clock/random/environment dependency;
-   no mutable static state;
-   no secret-bearing fields;
-   no speculative generic abstractions.

Prefer a small explicit feature contract surface over extensibility.

------------------------------------------------------------------------

## 13. Temporary Validation

WP05 should not add permanent tests.

A temporary offline probe is allowed only when necessary to prove
construction invariants or contract usability.

Any probe must:

-   be deterministic;
-   use no provider/network;
-   use no real credential;
-   create no persistent database state;
-   remain outside the candidate;
-   be removed before final verification;
-   leave zero residue.

Permanent test delta must remain `0`.

------------------------------------------------------------------------

## 14. Required Validation

Run canonical Release verification after implementation.

Prove:

-   restore PASS;
-   build PASS;
-   warnings `0`;
-   errors `0`;
-   Domain.Tests PASS;
-   Application.Tests PASS;
-   Infrastructure.Tests PASS;
-   Architecture.Tests PASS;
-   permanent total remains the accepted baseline unless changed by an
    earlier authorized WP;
-   Gitleaks PASS;
-   formatting PASS;
-   `git diff --check` PASS;
-   `git diff --cached --check` PASS;
-   direct trailing-whitespace inspection of untracked WP05 files PASS;
-   database/WAL/SHM/journal residue `0`;
-   provider/network calls `0`;
-   real credentials `0`;
-   package delta `0`;
-   project-reference delta `0`;
-   schema delta `0`;
-   permanent-test delta `0`;
-   production graph unchanged and acyclic.

Expected current baseline from WP04:

-   Domain.Tests: `11`
-   Application.Tests: `77`
-   Infrastructure.Tests: `96`
-   Architecture.Tests: `13`
-   Permanent total: `197`

Repository truth is authoritative if the accepted baseline has
legitimately changed.

------------------------------------------------------------------------

## 15. Regression Protection

Confirm no behavior change to:

### Release 1.1

-   historical observation persistence/retrieval;
-   decimal/timestamp fidelity;
-   isolation/idempotency/conflict semantics.

### Release 1.2

-   dataset definition and identities;
-   immutable snapshots/versions;
-   catalog behavior;
-   provenance/lineage;
-   schema v2.

### Release 1.3

-   pipeline definition/execution identities;
-   fixed five-stage topology;
-   orchestration;
-   evidence/failure semantics;
-   DI/configuration;
-   one-shot Worker behavior.

### Release 1.4 WP02--WP04

-   frozen feature semantics;
-   frozen identity/provenance semantics;
-   accepted immutable feature model.

------------------------------------------------------------------------

## 16. Git and GitHub Protection

WP05 is not an integration work package.

Do not:

-   stage;
-   commit;
-   push;
-   create/switch integration branches;
-   create or modify PRs;
-   merge;
-   tag;
-   create a release;
-   alter unrelated issues;
-   alter milestone #45 except through already-established lifecycle
    accounting;
-   start WP06.

Authorized lifecycle for #157 only:

1.  Backlog → In Progress after starting gates pass;
2.  post bounded completion evidence after all acceptance gates pass;
3.  close #157;
4.  set Project #2 status to Done.

Verify WP06 remains Open/Backlog and unchanged.

------------------------------------------------------------------------

## 17. Acceptance Criteria

WP05 is complete only if:

1.  all starting gates pass;
2.  WP04 model is reused rather than duplicated;
3.  the request contract binds the supported feature definition to an
    exact snapshot identity/version;
4.  a narrow Application feature-generation use-case seam exists;
5.  success can carry immutable WP04 feature evidence;
6.  empty success is naturally representable;
7.  bounded failure distinctions required by Release 1.4 are
    representable;
8.  unknown defects are not converted into a catch-all semantic failure;
9.  no feature computation is implemented;
10. no feature persistence/schema evolution is introduced;
11. no Infrastructure/Worker/Domain behavior is added unless explicitly
    manifest-authorized;
12. no DI/configuration is implemented;
13. no permanent tests are added or modified;
14. no package/project-reference changes occur;
15. Release 1.3 pipeline remains five stages;
16. canonical verification passes;
17. architecture remains unchanged and acyclic;
18. Gitleaks/format/whitespace checks pass;
19. no generated/database residue remains;
20. WP06 remains unstarted;
21. #157 reaches Closed/Done only after successful completion.

------------------------------------------------------------------------

## 18. Stop Conditions

Stop immediately with:

`RELEASE 1.4 WP05 BLOCKED`

if:

-   predecessor lifecycle is invalid;
-   candidate/manifest reconciliation fails;
-   required contract files are not authorized;
-   accepted WP02/WP03/WP04 semantics materially conflict;
-   a new package/project reference is required;
-   schema evolution is required;
-   correct contracts require feature persistence;
-   correct contracts require changing predecessor semantics;
-   a generalized feature framework appears necessary;
-   WP06 or Release 1.5 work has unexpectedly started;
-   canonical validation fails due to an issue that cannot be corrected
    within WP05;
-   an unexpected Git/GitHub mutation is detected.

Do not broaden scope to resolve a blocker.

------------------------------------------------------------------------

## 19. Required Final Report

Produce an evidence-rich WP05 execution report containing:

1.  Executive summary.
2.  Authorities reviewed.
3.  Repository/Git baseline.
4.  Working-tree classification.
5.  Predecessor/lifecycle gates.
6.  Initial canonical baseline.
7.  Existing Application contract inventory.
8.  WP04 model reconciliation.
9.  Request-contract design.
10. Exact snapshot/version binding.
11. Feature-definition binding.
12. Use-case seam.
13. Success-result contract.
14. Empty-success representation.
15. Failure vocabulary.
16. Unknown-defect propagation decision.
17. Immutability/construction invariants.
18. WP02 semantic preservation.
19. WP03 identity/provenance preservation.
20. WP04 model preservation.
21. Release 1.3 five-stage pipeline protection.
22. Explicit computation exclusion.
23. Persistence/schema exclusion.
24. DI/configuration/Worker exclusion.
25. Files added/modified.
26. Layer deltas.
27. Package/reference/schema delta.
28. Permanent-test delta.
29. Temporary probe evidence, if any.
30. Restore/build evidence.
31. Permanent test counts.
32. Canonical verification.
33. Architecture validation.
34. Release 1.1--1.3 regression evidence.
35. Security/offline evidence.
36. Whitespace/diff evidence.
37. Database/generated residue.
38. Mutation accounting.
39. Git/GitHub protection.
40. Final #157/#158 lifecycle state.
41. Findings/blockers.
42. Final decision.
43. Next authorized work package.

On success, terminate with exactly:

`RELEASE 1.4 WP05 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP06 — Deterministic Feature Computation — GitHub issue #158`

Do not start WP06.

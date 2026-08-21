# Release 1.4 --- WP13 Architecture & Documentation Evolution --- Codex Authority

## Mission

Execute **Release 1.4 --- WP13: Architecture & Documentation Evolution
--- GitHub issue #165**.

WP13 reconciles the accepted Release 1.4 deterministic
feature-engineering foundation with the repository's enforceable
architecture rules and current-state documentation.

This is an **architecture-and-documentation alignment package**, not a
production implementation package.

Recommended model: **GPT-5.6 Terra**.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Before any mutation, read completely and reconcile:

1.  `docs/roadmap/release-1.4/RELEASE_1.4_DEFINITION.md`
2.  `docs/roadmap/release-1.4/RELEASE_1.4_EXECUTION_PLAN.md`
3.  `docs/roadmap/release-1.4/RELEASE_1.4_FILE_MANIFEST.md`
4.  `docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`
5.  `docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`
6.  WP01--WP12 authorities and execution results.
7.  Release 1.3 post-merge closure evidence and current Release 1.3
    documentation.
8.  Current production implementation under:
    -   `src/AIQuantTradingResearch.Domain/**`
    -   `src/AIQuantTradingResearch.Application/**`
    -   `src/AIQuantTradingResearch.Infrastructure/**`
    -   `src/AIQuantTradingResearch.Worker/**`
9.  Current permanent tests, especially:
    -   Application feature semantic tests;
    -   Infrastructure feature composition/Worker tests;
    -   Architecture.Tests.
10. Current architecture documentation, including relevant data, design,
    implementation, solution, configuration, DI, testing, observability,
    public-contract, module-interaction, and pipeline documents.
11. Current README/front-door documentation.
12. Current GitHub state for #165 and successor #166.

Repository truth and accepted Release 1.4 semantic authorities govern.

Do not infer future capabilities from roadmap intent.

------------------------------------------------------------------------

## 2. Starting-State Gates

Before mutation verify and report:

-   branch `main`;
-   `HEAD == origin/main`;
-   ahead/behind `0/0`;
-   staged paths `0`;
-   cumulative Release 1.4 paths are expected and manifest-authorized;
-   #153--#164 are Closed/Done;
-   #165 is OPEN / Backlog;
-   #166 is OPEN / Backlog and untouched;
-   milestone #45 is OPEN;
-   SQLite schema is exactly version `2`;
-   no Release 1.5 implementation has started;
-   production graph remains:
    -   Domain → none
    -   Application → Domain
    -   Infrastructure → Application
    -   Worker → Application, Infrastructure.

Run the canonical Release verification before mutation.

Expected accepted pre-WP13 permanent baseline:

-   Domain.Tests: `11`
-   Application.Tests: `86`
-   Infrastructure.Tests: `104`
-   Architecture.Tests: `13`
-   Total: `214`
-   skipped: `0`.

Repository truth wins only if an accepted predecessor delta explains a
difference.

Only after all starting gates pass may #165 move Backlog → In Progress.

------------------------------------------------------------------------

## 3. WP13 Objectives

WP13 has two ordered responsibilities:

### A. Architecture evolution

Determine whether Release 1.4 introduced any **new stable architectural
invariant** that is not already enforced by the existing
Architecture.Tests.

Prefer **zero architecture-test delta** when existing rules already
enforce the stable boundaries.

Add a new architecture rule only when all of the following are true:

1.  it expresses a durable architectural boundary rather than
    implementation detail;
2.  Release 1.4 makes the boundary materially new;
3.  existing architecture tests do not already enforce it;
4.  the rule can be expressed without coupling to incidental class/file
    names;
5.  it is within the Release 1.4 file manifest and WP13 authority.

### B. Documentation evolution

Align current-state documentation with the accepted Release 1.4
implementation and permanent evidence.

Documentation must describe what exists now, not what a later release
might add.

------------------------------------------------------------------------

## 4. Architecture Inventory First

Before editing Architecture.Tests, inventory the existing rules and
classify their coverage.

At minimum reconcile whether existing rules already protect:

-   Domain independence;
-   Application → Domain only;
-   Infrastructure → Application;
-   Worker → Application/Infrastructure;
-   production graph acyclicity;
-   provider/HTTP confinement to Infrastructure;
-   Twelve Data/provider implementation visibility;
-   Application ownership of research contracts/semantics;
-   non-public Infrastructure implementations where governed;
-   Release 1.3 pipeline boundaries;
-   Release 1.4 feature semantics remaining outside Domain and
    Infrastructure.

Report the exact existing Architecture.Tests count and rule inventory.

------------------------------------------------------------------------

## 5. Candidate Architecture Rules

Evaluate possible Release 1.4-specific rules, including:

-   feature semantic contracts remain Application-owned;
-   feature computation remains provider/storage independent;
-   feature generation does not become part of the Release 1.3 pipeline
    topology;
-   Infrastructure does not own feature semantic types;
-   Worker does not own feature semantic computation;
-   no feature persistence/schema layer has appeared;
-   dependency graph remains unchanged;
-   no new outward dependency from Application.

Do **not** add these mechanically.

For each candidate, classify it as:

-   already enforced;
-   behavioral rather than architectural;
-   documentation-only;
-   unstable/implementation-specific;
-   genuinely new stable rule.

------------------------------------------------------------------------

## 6. Preferred Architecture Decision

The preferred result is:

`Architecture.Tests delta: 0`

if the existing 13 rules already cover all stable Release 1.4
boundaries.

A zero-delta decision is a successful architecture-evolution outcome
when supported by explicit reconciliation evidence.

Do not add redundant tests merely to create a WP13 code delta.

------------------------------------------------------------------------

## 7. If a New Architecture Rule Is Required

Only if a genuine uncovered stable invariant exists:

-   add the minimum Architecture.Tests change authorized by the
    manifest;
-   preserve existing architecture-test style and naming;
-   avoid source-text scanning when a structural dependency/type rule
    can express the invariant;
-   avoid hard-coding incidental implementation names unless the name
    itself is an architectural contract;
-   do not change production code to satisfy a new rule unless
    separately authorized.

If a justified architecture rule exposes an accepted production
contradiction requiring production mutation, stop with
`RELEASE 1.4 WP13 BLOCKED`.

------------------------------------------------------------------------

## 8. Production and Functional-Test Protection

Expected deltas:

-   Domain production: `0`
-   Application production: `0`
-   Infrastructure production: `0`
-   Worker production: `0`
-   Domain.Tests: `0`
-   Application.Tests: `0`
-   Infrastructure.Tests: `0`
-   Packages: `0`
-   Project references: `0`
-   Schema: `0`

Only Architecture.Tests may change, and only if justified by Section 7.

Do not modify WP11/WP12 functional tests merely to update counts or
wording.

------------------------------------------------------------------------

## 9. Documentation Inventory

Inventory current-state documentation before editing.

Use the Release 1.4 file manifest as hard authority for files WP13 may
modify.

Classify each candidate document:

-   stale and requires alignment;
-   incomplete and requires Release 1.4 addition;
-   already accurate;
-   future/planned and should remain unchanged;
-   outside WP13 authority.

Do not edit a document merely because it mentions features.

------------------------------------------------------------------------

## 10. Required Current-State Documentation Truth

Where applicable and manifest-authorized, documentation should
accurately reflect the following accepted state.

### Release progression

-   Release 1.1: persisted historical-observation foundation.
-   Release 1.2: deterministic immutable dataset/snapshot/catalog
    foundation.
-   Release 1.3: fixed deterministic one-shot research pipeline over
    persisted history.
-   Release 1.4: deterministic feature-engineering foundation over an
    exact accepted immutable dataset snapshot.

Do not rewrite predecessor releases as if Release 1.4 replaced them.

------------------------------------------------------------------------

## 11. Feature Boundary

Document exactly one built-in feature definition:

`simple-return-lag-1-v1`

Formula:

`r[i] = (p[i] / p[i-1]) - 1`

Document accepted semantics:

-   decimal-only arithmetic;
-   accepted snapshot order defines adjacency;
-   result belongs to current observation `i`;
-   current observation timestamp and original offset are preserved;
-   no convenience rounding;
-   empty snapshot → successful empty feature set;
-   one observation → successful empty feature set;
-   `N >= 2` → `N-1` ordered values;
-   zero predecessor / invalid numeric evidence is a bounded failure;
-   equivalent accepted evidence recomputes equivalent feature identity.

Do not generalize this into a feature engine.

------------------------------------------------------------------------

## 12. Feature Identity / Provenance

Where relevant document:

-   `aiq-feature-identity-v1`;
-   distinct Feature Definition Identity and Feature Set Identity;
-   SHA-256 / 64 lowercase hexadecimal fingerprints;
-   canonical deterministic representation;
-   exact snapshot identity/version binding;
-   ordered semantic feature evidence;
-   preserved timestamp/offset and decimal semantics;
-   deterministic empty Feature Set identities bound to their exact
    snapshots;
-   acyclic lineage;
-   operational metadata excluded from semantic identity;
-   contradictory content under equal identity is an integrity conflict.

Do not duplicate the complete canonical encoding outside its semantic
authority unless the target document genuinely requires it.

Prefer links to the authoritative semantic documents.

------------------------------------------------------------------------

## 13. Feature Generation Flow

Document the accepted Release 1.4 one-shot boundary:

exact snapshot identity/version request → request validation → exact
snapshot lookup → returned snapshot/evidence validation → deterministic
`simple-return-lag-1-v1` computation → immutable feature
identity/provenance/evidence → structured result → bounded Worker
presentation/exit.

This is a **separate feature-generation use case**.

It is **not a sixth Release 1.3 pipeline stage**.

The Release 1.3 pipeline remains exactly five stages.

------------------------------------------------------------------------

## 14. Ownership

Current documentation should consistently state:

### Domain

No Release 1.4 feature implementation ownership was added.

### Application

Owns:

-   feature definitions and typed identities;
-   immutable feature model/evidence;
-   generation contracts;
-   deterministic feature computation;
-   validation/failure mapping;
-   exact snapshot integration orchestration;
-   semantic feature result behavior.

### Infrastructure

Reuses existing SQLite dataset snapshot storage and lookup. It does not
own feature semantics or feature persistence.

### Worker

Owns composition/configuration selection and one-shot presentation/exit
behavior. It does not own feature computation semantics.

------------------------------------------------------------------------

## 15. Persistence / Schema

Document explicitly:

-   SQLite remains schema version `2`;
-   Release 1.4 feature output is in-memory/reproducible evidence;
-   no feature table;
-   no feature catalog;
-   no feature cache;
-   no feature execution/run-history persistence;
-   no scheduler/checkpoint/pipeline-state persistence.

Do not imply schema v3 exists or is committed.

------------------------------------------------------------------------

## 16. Configuration / DI

Where applicable document:

-   `Feature:SnapshotIdentity`;
-   `Feature:SnapshotVersion`;
-   exact snapshot/version selection;
-   built-in definition is code-owned and not configurable;
-   no `Feature:Formula`;
-   no configurable lag;
-   no configurable rounding;
-   feature use case/computer/validator are registered with accepted
    transient lifetimes;
-   existing snapshot store is reused;
-   DI graph resolution is side-effect-free;
-   resolving services does not execute feature generation or create the
    database solely because of resolution.

Do not expose credentials or machine-specific paths.

------------------------------------------------------------------------

## 17. Worker Behavior

Document:

-   Feature configuration selects bounded feature mode;
-   feature mode invokes `IFeatureGenerationUseCase` exactly once per
    process;
-   success returns exit `0`;
-   bounded failures return non-zero according to accepted Worker
    behavior;
-   unknown defects are not silently normalized;
-   absence of Feature configuration preserves the Release 1.3 pipeline
    path;
-   no loop, timer, scheduler, retry, recurrence, or feature
    persistence.

------------------------------------------------------------------------

## 18. Failure Semantics

Where relevant document the accepted distinctions without inventing new
categories:

-   invalid request;
-   unsupported definition;
-   snapshot NotFound;
-   dependency unavailable;
-   invalid snapshot/evidence;
-   invalid numeric input;
-   integrity conflict;
-   successful empty/non-empty result;
-   unknown defects propagate rather than being broadly reclassified.

Use exact repository vocabulary when it differs from prose labels above.

------------------------------------------------------------------------

## 19. Provider / Network Boundary

Document that feature generation is provider-independent and operates
over accepted persisted snapshot evidence.

No Twelve Data/HTTP call is part of feature computation.

Do not imply Release 1.4 orchestrates acquisition.

Controlled acquisition orchestration remains deferred.

------------------------------------------------------------------------

## 20. Observability / Evidence

Document only accepted evidence:

-   structured semantic result evidence;
-   safe local Worker presentation/events;
-   deterministic identities/provenance.

Do not claim:

-   metrics backend;
-   distributed tracing backend;
-   dashboards;
-   durable feature execution history;
-   production monitoring system.

Future observability capabilities remain deferred.

------------------------------------------------------------------------

## 21. Testing Documentation

Update current test counts where the manifest-authorized documents
expose them.

Accepted post-WP12 baseline:

-   Domain.Tests: `11`
-   Application.Tests: `86`
-   Infrastructure.Tests: `104`
-   Architecture.Tests: `13`
-   Permanent total: `214`
-   skipped: `0`

If WP13 legitimately adds an Architecture.Test, use the resulting final
count instead.

Document responsibilities accurately:

-   Application tests: feature semantics, identities, formula, fidelity,
    validation, failure behavior, exact lookup integration.
-   Infrastructure tests: SQLite/persistence plus DI/composition and
    black-box Worker feature execution.
-   Architecture tests: stable dependency/boundary rules.
-   all feature tests remain deterministic/offline.

Do not claim process coverage lives in Application.Tests.

------------------------------------------------------------------------

## 22. Architecture Documentation

Where manifest-authorized, reconcile architecture documents to show:

-   unchanged production dependency graph;
-   Application ownership of feature semantics;
-   Infrastructure reuse of snapshot persistence;
-   Worker as composition/one-shot boundary;
-   feature generation separate from pipeline topology;
-   no new architectural cycle;
-   no feature persistence/schema expansion.

Avoid diagrams or claims that imply a generalized DAG or feature plugin
architecture exists.

------------------------------------------------------------------------

## 23. README / Front Door

If README is authorized by the manifest and currently stale, make the
smallest current-state update needed to represent Release 1.4
accurately.

Prefer concise statements and links to authoritative documentation.

Do not turn README into the complete Release 1.4 specification.

Preserve existing Release 1.1--1.3 showcase/history where still
accurate.

------------------------------------------------------------------------

## 24. Deferrals

Documentation must keep future capabilities explicitly deferred where
relevant:

-   feature persistence/catalog/cache;
-   multiple indicators/features;
-   configurable formulas/lags;
-   feature plugins;
-   DAGs;
-   live acquisition orchestration;
-   scheduling;
-   retries/resilience automation;
-   durable run histories;
-   notebooks/workspaces;
-   strategies;
-   backtesting;
-   ML;
-   MLOps.

Do not assign a future release number unless an accepted authority
already does.

------------------------------------------------------------------------

## 25. Link and Stale-Claim Audit

For all WP13-modified Markdown:

-   validate local relative links;
-   ensure no broken links introduced;
-   search for stale current-state claims contradicted by Release 1.4;
-   search for stale test counts;
-   search for statements implying features are persisted;
-   search for statements implying feature generation is pipeline stage
    six;
-   search for statements implying live provider acquisition occurs
    during feature generation;
-   search for generalized feature-engine/plugin/DAG claims.

Report findings and final remaining focused matches.

------------------------------------------------------------------------

## 26. Whitespace / Markdown Hygiene

Directly inspect all modified/untracked WP13 Markdown for trailing
whitespace because ordinary `git diff --check` may not inspect untracked
files.

Requirements:

-   trailing whitespace findings `0`;
-   `git diff --check` PASS;
-   `git diff --cached --check` PASS;
-   local Markdown links PASS.

Do not perform unrelated prose reformatting.

------------------------------------------------------------------------

## 27. Validation

After WP13 mutation:

1.  run targeted Architecture.Tests if changed;
2.  run complete Architecture.Tests;
3.  run full permanent suite;
4.  run canonical `eng/verify.ps1 -Configuration Release`;
5.  run both Git diff checks;
6.  run direct whitespace inspection for new/untracked governed files;
7.  validate local links in modified documentation;
8.  audit database/generated residue.

Confirm:

-   restore PASS;
-   format PASS;
-   build warnings/errors `0/0`;
-   Domain.Tests pass;
-   Application.Tests pass;
-   Infrastructure.Tests pass;
-   Architecture.Tests pass;
-   permanent total accurate;
-   Gitleaks PASS;
-   production graph unchanged and acyclic;
-   package/reference/schema delta `0/0/0`;
-   SQLite schema v2;
-   provider/network calls `0`;
-   real credentials `0`;
-   database/WAL/SHM/journal residue `0`.

------------------------------------------------------------------------

## 28. Regression Acceptance

Explicitly confirm:

### Release 1.1

Historical observation persistence/retrieval behavior remains passing.

### Release 1.2

Dataset/snapshot identity, immutable persistence, exact lookup, catalog
semantics, and schema v2 remain passing.

### Release 1.3

Fixed five-stage pipeline, pipeline identity/evidence, DI/configuration,
and one-shot Worker behavior remain passing.

### Release 1.4 WP11

Feature semantic permanent tests remain passing.

### Release 1.4 WP12

Feature composition/Worker permanent tests remain passing.

------------------------------------------------------------------------

## 29. Release 1.5 Protection

WP13 must not implement:

-   feature persistence;
-   additional features;
-   configurable formulas/lags;
-   plugins;
-   DAGs;
-   scheduling;
-   retries;
-   acquisition orchestration;
-   durable feature history;
-   notebooks;
-   backtesting;
-   strategies;
-   ML/MLOps.

Documentation may mention these only as clearly deferred future work
when appropriate.

------------------------------------------------------------------------

## 30. Git Protection

Do not:

-   stage;
-   commit;
-   create branch;
-   push;
-   create/modify PR;
-   merge;
-   tag;
-   release;
-   rewrite history.

Preserve cumulative Release 1.4 work.

------------------------------------------------------------------------

## 31. GitHub Lifecycle

Only issue #165 may receive lifecycle mutation.

After starting gates pass:

1.  move #165 Backlog → In Progress;
2.  execute architecture reconciliation;
3.  execute documentation alignment;
4.  validate completely;
5.  post completion evidence only after all gates pass;
6.  close #165;
7.  set Project #2 Status to Done;
8.  read back #165 as CLOSED / Done;
9.  verify #166 remains OPEN / Backlog unchanged;
10. verify milestone #45 remains OPEN.

Do not start WP14.

If #165 lifecycle writes fail to persist, reconcile only #165.

------------------------------------------------------------------------

## 32. Stop Conditions

Stop with:

`RELEASE 1.4 WP13 BLOCKED`

if:

-   production code must change;
-   functional tests must change;
-   packages/references/schema must change;
-   a proposed architecture rule exposes a production contradiction
    requiring implementation mutation;
-   required documentation lies outside the authorized manifest;
-   repository truth materially contradicts Release 1.4 accepted
    semantics;
-   Release 1.3 pipeline semantics must change;
-   Release 1.5 behavior is required;
-   canonical verification cannot be restored within
    architecture/documentation-only scope.

Report the smallest corrective authority required. Do not guess.

------------------------------------------------------------------------

## 33. Required Execution Report

Report at least:

1.  executive summary;
2.  authorities reviewed;
3.  repository/Git baseline;
4.  working-tree classification;
5.  lifecycle gates;
6.  initial canonical baseline;
7.  architecture-test inventory;
8.  existing architecture-rule coverage;
9.  Domain reconciliation;
10. Application reconciliation;
11. Infrastructure reconciliation;
12. Worker reconciliation;
13. production graph/cycle result;
14. provider/storage leakage reconciliation;
15. candidate architecture rules evaluated;
16. classification of each candidate;
17. architecture-test decision;
18. architecture files changed;
19. production delta;
20. functional-test delta;
21. architecture-test count/delta;
22. package/reference/schema delta;
23. documentation inventory/classification;
24. Release 1.1 documentation alignment;
25. Release 1.2 documentation alignment;
26. Release 1.3 documentation alignment;
27. Release 1.4 feature boundary alignment;
28. feature identity/provenance alignment;
29. feature-generation flow alignment;
30. ownership alignment;
31. DI/configuration alignment;
32. Worker alignment;
33. failure-semantics alignment;
34. persistence/schema alignment;
35. provider/network boundary alignment;
36. observability/evidence alignment;
37. testing/count alignment;
38. deferral alignment;
39. exact documentation files modified;
40. local-link validation;
41. stale-claim audit;
42. restore/build evidence;
43. permanent test counts;
44. canonical verification;
45. architecture validation;
46. security/Gitleaks evidence;
47. whitespace/diff evidence;
48. database residue;
49. predecessor regressions;
50. mutation accounting;
51. Git/GitHub protection;
52. findings/blockers;
53. final GitHub state;
54. WP14 handoff;
55. final decision.

On success end exactly with:

`RELEASE 1.4 WP13 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP14 — Full Validation, Integration & Acceptance — GitHub issue #166`

Do not start WP14.

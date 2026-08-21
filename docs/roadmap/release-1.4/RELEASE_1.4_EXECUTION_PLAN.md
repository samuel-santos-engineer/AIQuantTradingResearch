# Release 1.4 Execution Plan

## 1. Release Identity

**Phase 4 — Release 1.4: Deterministic Feature Engineering Foundation**

Release 1.4 establishes exactly one provider-independent, storage-independent, deterministic feature transformation over an accepted immutable Release 1.2 dataset snapshot.

The release computes the built-in feature:

`simple-return-lag-1-v1`

using exact .NET `decimal` arithmetic and returns immutable in-memory feature values with canonical identity, provenance, lineage, classified outcomes, and structured semantic evidence.

This plan is subordinate to `RELEASE_1.4_DEFINITION.md`. If this plan conflicts with the accepted definition, the definition is authoritative and execution must stop for human reconciliation.

## 2. Accepted Starting Baseline

Execution begins from the formally closed Release 1.3 baseline:

- Release 1.3 PR #152 merged.
- Release 1.3 milestone #54 closed.
- Issues #138–#151 Closed/Done.
- Merged `main` baseline: `0c981bb5765bb519bca3542c745f9282beb7b0d5`.
- Permanent tests: 197/197.
  - Domain.Tests: 11.
  - Application.Tests: 77.
  - Infrastructure.Tests: 96.
  - Architecture.Tests: 13.
- SQLite schema version: 2.
- Production dependency graph:
  - Domain → none.
  - Application → Domain.
  - Infrastructure → Application.
  - Worker → Application, Infrastructure.
- Release 1.3 fixed pipeline remains unchanged.
- Release 1.4 implementation has not started.

WP01 must independently reconcile the actual repository and GitHub state before relying on these values.

## 3. Release Invariants

Every work package must preserve these invariants unless its authority explicitly requires a compatible refinement:

1. Feature generation remains separate from the Release 1.3 fixed research pipeline.
2. Input is an already accepted immutable dataset snapshot identified by exact snapshot identity.
3. Output is an immutable in-memory feature set; Release 1.4 does not persist feature output.
4. Exactly one built-in feature is authorized: `simple-return-lag-1-v1`.
5. For ordered prices `p[i-1]` and `p[i]`, the feature value is `(p[i] / p[i-1]) - 1`.
6. Computation uses exact .NET `decimal` arithmetic.
7. The output observation retains the current input observation's exact `DateTimeOffset`, including offset.
8. Input canonical ordering is preserved.
9. Empty and single-observation snapshots produce valid empty feature sets.
10. Invalid numeric evidence, including non-positive price evidence, is rejected rather than normalized.
11. `aiq-feature-identity-v1` is the only Release 1.4 feature identity scheme.
12. Feature Definition Identity and Feature Set Identity remain distinct.
13. Semantic identities are canonical, culture invariant, deterministic, and SHA-256 based with exactly 64 lowercase hexadecimal fingerprint characters.
14. Operational data does not affect semantic identity.
15. Existing Release 1.1–1.3 identities, provenance, persistence, catalog, failure, pipeline, and Worker semantics are not reinterpreted.
16. Expected failures remain provider- and storage-independent at the Application boundary.
17. Unknown programming defects propagate.
18. SQLite remains schema version 2.
19. Production dependency graph remains unchanged.
20. No new package, project, or project reference is expected.
21. All permanent acceptance tests remain deterministic and offline.
22. No Release 1.5 implementation or planning is authorized.

## 4. Explicit Exclusions

No work package may introduce:

- a feature stage into the Release 1.3 pipeline;
- feature plugins, arbitrary formulas, expressions, or dynamic feature graphs;
- rolling indicators, joins, resampling, aggregation, labels, leakage policy, scaling, normalization, imputation, or enrichment;
- feature persistence, feature catalog, cache, or schema v3;
- provider calls, live acquisition, or credential-dependent feature execution;
- scheduling, recurring refresh, retries, circuit breakers, fallback, compensation, checkpoint, or resume;
- parallel, streaming, distributed, or GPU feature execution;
- durable feature/pipeline execution history;
- metrics or tracing backends;
- notebooks, workspace, visualization, experiment tracking;
- strategies, signals, backtesting, portfolio analytics, ML, explainability, or MLOps.

Discovery of a genuine prerequisite that requires one of these capabilities is a stop condition, not permission to expand Release 1.4.

## 5. Work-Package Dependency Graph

```text
Release 1.3 CLOSED
        |
       WP01
        |
       WP02
        |
       WP03
        |
       WP04
        |
       WP05
        |
       WP06
      /    \
   WP07    WP11 prerequisites continue
      |
     WP08
      |
     WP09
      |
     WP10
      |
     WP12

WP03 + WP04 + WP06 + WP07 + WP08 -> WP11
WP09 + WP10                       -> WP12
WP11 + WP12                       -> WP13
WP11 + WP12 + WP13                -> WP14
```

Authoritative dependency declarations:

| WP | Title | Depends on | Baseline model |
| --- | --- | --- | --- |
| WP01 | Release & Repository Preflight | Release 1.3 CLOSED | Luna |
| WP02 | Feature Engineering Semantic Discovery | WP01 | Sol |
| WP03 | Feature Identity, Provenance & Evidence Semantics | WP02 | Sol |
| WP04 | Feature Domain/Application Model | WP03 | Terra |
| WP05 | Feature Generation Contracts | WP04 | Terra |
| WP06 | Deterministic Simple-Return Computation | WP05 | Terra |
| WP07 | Feature Validation & Failure Mapping | WP03, WP06 | Sol |
| WP08 | Feature Generation Integration | WP05, WP06, WP07 | Terra |
| WP09 | Dependency Registration & Configuration | WP08 | Terra |
| WP10 | One-Shot Worker Feature Execution | WP09 | Terra |
| WP11 | Domain & Application Feature Tests | WP03, WP04, WP06, WP07, WP08 | Luna |
| WP12 | Composition & Worker Validation | WP09, WP10 | Terra |
| WP13 | Architecture & Documentation Alignment | WP11, WP12 | Terra |
| WP14 | Full Validation, Integration & Acceptance | WP11, WP12, WP13 | Sol |

Model assignments are planning guidance, not a semantic requirement. They may be reassessed before execution without changing release scope.

## 6. Common Work-Package Starting Gate

Before mutating repository content, every WP must:

1. Read its full authoritative prompt and five-line chat companion.
2. Read `RELEASE_1.4_DEFINITION.md`, this execution plan, and `RELEASE_1.4_FILE_MANIFEST.md`.
3. Read all predecessor artifacts required by its dependency declaration.
4. Verify repository identity and current branch.
5. Verify expected predecessor issues are Closed/Done and the current issue is Open/Backlog unless its prompt explicitly defines another starting state.
6. Verify no successor WP has started.
7. Classify the working tree against the cumulative accepted Release 1.4 candidate.
8. Reject unexpected production, test, governance, generated, database, or temporary residue.
9. Verify staged paths are zero before ordinary WP execution.
10. Run the WP-required baseline validation before mutation.
11. Stop on a material contradiction between repository truth and governing authorities.

## 7. Common Work-Package Completion Gate

Unless a WP is explicitly evidence-only, semantic-only, or final integration, completion requires:

- authorized scope only;
- no unauthorized package/reference/schema changes;
- no provider/network execution unless an authority explicitly permits it; Release 1.4 feature execution itself never does;
- deterministic/offline validation;
- zero unexpected generated/database residue;
- `git diff --check` PASS;
- `git diff --cached --check` PASS;
- canonical `eng/verify.ps1 -Configuration Release` PASS;
- build warnings/errors 0/0;
- Gitleaks PASS;
- all permanent tests PASS;
- production dependency graph preserved;
- issue completion evidence posted;
- current WP issue Closed and Project status Done;
- successor issue unchanged;
- no staging, commit, push, branch, PR, merge, tag, or release unless explicitly authorized by WP14.

A WP must report exact test counts rather than assuming the planning baseline.

## 8. WP01 — Release & Repository Preflight

### Objective

Prove that Release 1.4 begins from the accepted Release 1.3 closure and from a coherent GitHub/repository baseline.

### Scope

- Verify Release 1.3 merge, milestone closure, issue closure, synchronized `main`, schema v2, architecture graph, permanent tests, security baseline, and repository cleanliness/classification.
- Verify the authoritative Release 1.4 milestone and exactly WP01–WP14 issues exist after separate GitHub-planning authority has established them.
- Verify Project membership/fields and dependency declarations.
- Verify legacy milestone #45 is in the state established by the separate planning authority.
- Establish the exact Release 1.4 starting baseline.

### Repository mutation

None expected.

### Acceptance

- Release 1.3 closure independently proven.
- Release 1.4 planning baseline reconciled with zero dependency drift.
- Canonical verification passes.
- No premature Release 1.4 implementation exists.
- No Release 1.5 work exists.
- WP01 lifecycle alone may advance.

### Stop conditions

Missing/ambiguous authoritative milestone, missing WP issues, Project drift, predecessor closure mismatch, unexpected repository mutation, or failed canonical baseline.

## 9. WP02 — Feature Engineering Semantic Discovery

### Objective

Freeze the technology-independent semantics for the single built-in feature before implementation.

### Required artifact

`docs/architecture/data/FEATURE_ENGINEERING_SEMANTICS.md`

### Required decisions

- precise `simple-return-lag-1-v1` vocabulary;
- accepted input snapshot boundary;
- exact lag-1 formula;
- price field/input evidence used by the transformation;
- canonical ordering;
- timestamp association;
- exact decimal semantics;
- empty/single-observation behavior;
- invalid numeric evidence;
- reproducibility/equivalence;
- feature-generation boundary versus Release 1.3 pipeline;
- explicit exclusions.

### Constraints

No production/test/schema/DI/Worker implementation.

### Acceptance

The artifact must remove semantic ambiguity needed by WP03–WP14 without designing a general feature engine.

## 10. WP03 — Feature Identity, Provenance & Evidence Semantics

### Objective

Freeze feature identity, provenance, lineage, and evidence semantics.

### Required artifact

`docs/architecture/data/FEATURE_IDENTITY_PROVENANCE_EVIDENCE.md`

### Required decisions

- `aiq-feature-identity-v1`;
- Feature Definition Identity canonical inputs;
- Feature Set Identity canonical inputs;
- unambiguous canonical encoding;
- SHA-256 lowercase hexadecimal fingerprint rules;
- exact linkage to existing dataset/snapshot identities;
- lineage direction and acyclicity;
- equivalent evaluation semantics;
- changed-input/changed-definition distinction;
- empty feature-set identity;
- evidence establishment on failures;
- semantic versus operational evidence exclusions.

### Constraints

No implementation and no persistence design.

### Acceptance

Identity derivation must be deterministic, acyclic, culture invariant, and sufficient for implementation without reinterpretation.

## 11. WP04 — Feature Domain/Application Model

### Objective

Establish the minimum immutable feature value/model surface required by WP05–WP08.

### Design rule

Prefer Domain delta zero. Domain changes are permitted only if WP02/WP03 prove a technology-independent invariant genuinely belongs there.

### Expected Application surface

A cohesive feature namespace under:

`src/AIQuantTradingResearch.Application/Features/`

The model may establish typed identities, built-in definition representation, feature observations, feature set values, provenance/lineage value objects, and construction invariants as justified by WP02/WP03.

### Constraints

- no orchestration;
- no snapshot lookup;
- no DI;
- no Worker;
- no persistence;
- no schema change;
- no feature-plugin abstraction.

### Acceptance

The model is immutable, provider/storage independent, non-interchangeable where identities differ, and contains no operational metadata.

## 12. WP05 — Feature Generation Contracts

### Objective

Define the provider/storage-independent Application boundary for one feature-generation request.

### Expected surface

Within the existing Application feature area, define the minimum:

- request contract;
- success/failure result contract;
- bounded failure vocabulary;
- structured semantic evidence contract;
- feature-generation use-case seam;
- any narrow computation seam required for testability and WP08 composition.

### Required failure distinctions

At minimum preserve the definition's distinctions for:

- invalid request;
- unsupported definition;
- snapshot NotFound;
- dependency unavailable;
- invalid/contradictory snapshot evidence;
- invalid numeric input;
- successful empty feature set;
- successful non-empty feature set.

Unknown defects must remain unhandled by broad normalization.

### Constraints

No implementation orchestration, DI, Worker, persistence, or schema change.

## 13. WP06 — Deterministic Simple-Return Computation

### Objective

Implement the exact pure transformation for `simple-return-lag-1-v1`.

### Required behavior

- consume accepted ordered input observations;
- calculate `(p[i] / p[i-1]) - 1` with `decimal`;
- preserve the current observation timestamp/offset;
- emit exactly `max(0, n - 1)` observations;
- produce valid empty output for zero/one input observation;
- reject invalid numeric evidence;
- remain independent of culture, timezone, clock, randomness, machine, path, provider, and storage;
- compute canonical feature identities according to WP03, if identity computation is owned here by the accepted design.

### Constraints

No snapshot retrieval, persistence, DI, Worker, provider call, or general transform framework.

## 14. WP07 — Feature Validation & Failure Mapping

### Objective

Harden canonical request/evidence validation and first-failure classification without broad exception normalization.

### Required behavior

- validate request/definition/identity consistency;
- reject unsupported definitions;
- distinguish NotFound, unavailable dependency, invalid evidence, invalid numeric evidence, and integrity contradictions as defined by accepted contracts;
- preserve only established upstream evidence on failure;
- preserve successful empty/non-empty behavior;
- propagate unknown programming defects;
- introduce no retry, repair, fallback, compensation, or normalization.

### Acceptance

A complete validation/failure matrix is demonstrated with temporary probes or existing permanent coverage, but permanent feature semantic tests remain WP11-owned.

## 15. WP08 — Feature Generation Integration

### Objective

Compose exact immutable snapshot lookup with deterministic feature computation and structured result evidence.

### Required flow

```text
explicit request
  → exact snapshot identity lookup
  → validate accepted snapshot evidence
  → deterministic simple-return computation
  → feature identity/provenance/lineage construction
  → structured terminal result/evidence
```

### Required behavior

- use existing Application snapshot/catalog seams as appropriate;
- never execute the Release 1.3 pipeline;
- never call a provider;
- never persist feature output;
- fail stop at the first failed boundary;
- preserve snapshot evidence and Release 1.2 semantics;
- return deterministic success evidence for empty and non-empty outputs.

### Constraints

Application-owned integration only. Infrastructure production delta remains zero unless a separately proven composition necessity exists.

## 16. WP09 — Dependency Registration & Configuration

### Objective

Make the bounded feature graph resolvable without execution side effects.

### Required behavior

- register the feature-generation use case and required stateless collaborators;
- reuse existing snapshot/catalog Infrastructure registrations;
- choose lifetimes that capture no connection or execution state in singletons;
- define/reuse explicit Worker input configuration sufficient to identify the accepted snapshot and built-in feature;
- parse values culture invariantly;
- reject missing/invalid configuration before feature execution;
- prove DI resolution creates no database and executes no feature generation/provider call.

### Configuration rule

Do not create configurable semantic knobs that reinterpret the built-in feature. Algorithm/version/lag semantics remain code-owned authority.

### Constraints

No Worker execution yet; that is WP10.

## 17. WP10 — One-Shot Worker Feature Execution

### Objective

Expose one bounded offline Worker execution path for the feature use case.

### Required behavior

- create exactly one canonical feature request from explicit configuration;
- resolve the Application use case;
- invoke it exactly once;
- present safe structured evidence;
- return deterministic process status;
- terminate;
- preserve unknown-exception visibility;
- avoid logging secrets, database paths, machine-specific values, or full feature payloads.

### Required process semantics

- successful empty/non-empty generation exits 0;
- expected bounded/configuration failure exits non-zero;
- no loop, retry, timer, scheduler, refresh, host recurrence, or durable run history;
- no provider call.

Release 1.2/1.3 Worker paths may be preserved but must not be semantically rewritten outside manifest authority.

## 18. WP11 — Domain & Application Feature Tests

### Objective

Create permanent deterministic semantic coverage for Release 1.4.

### Required coverage

At minimum:

- canonical typed identities;
- malformed identity rejection;
- exact formula;
- decimal fidelity;
- timestamp/offset fidelity;
- canonical ordering;
- zero/one observation empty output;
- multi-observation cardinality;
- equivalent identity stability;
- changed input identity distinction;
- changed definition identity distinction where the accepted model exposes a valid comparison;
- culture/timezone independence;
- provenance/lineage;
- invalid request;
- unsupported definition;
- NotFound;
- unavailable dependency;
- invalid snapshot evidence;
- invalid numeric evidence;
- successful empty/non-empty result;
- unknown exception propagation;
- no provider/storage implementation dependency in pure Application tests.

### Test design

Use hand-written doubles and deterministic data. Do not use live provider/network access or repository-persistent databases.

## 19. WP12 — Composition & Worker Validation

### Objective

Create permanent offline proof of real composition and bounded Worker behavior.

### Required coverage

- exactly one intended feature-use-case registration;
- accepted service lifetimes;
- graph resolution without feature execution;
- graph resolution without database creation;
- deterministic configuration parsing;
- culture/offset preservation;
- real one-shot Worker success;
- empty success;
- bounded failure;
- invalid configuration;
- first-failure evidence;
- zero provider calls;
- deterministic cleanup of temporary SQLite files and sidecars;
- no new production package/reference/schema behavior.

Prefer the existing Infrastructure test project and black-box Worker process pattern established by Release 1.3.

## 20. WP13 — Architecture & Documentation Alignment

### Objective

Enforce only stable non-redundant architecture boundaries and align current-state documentation.

### Architecture rule

Zero architecture-test delta is acceptable and preferred if the existing rules already enforce all stable Release 1.4 boundaries.

Potential new rules must be rejected if they merely duplicate broad dependency tests, encode filenames/class names, or test behavior better owned by WP11/WP12.

### Documentation scope

Align only current-state documents that are stale after Release 1.4. Likely candidates include:

- `README.md`;
- data lifecycle/pipeline or feature-related architecture documents;
- public contracts/module interactions;
- configuration/DI;
- testing strategy;
- observability/current evidence boundaries.

Do not rewrite future architecture documents merely to mention Release 1.4.

### Required documentation truth

- one built-in feature only;
- feature generation separate from Release 1.3 pipeline;
- input accepted snapshot;
- in-memory immutable output;
- `aiq-feature-identity-v1`;
- schema v2;
- no feature persistence;
- current test counts;
- exact deferrals.

## 21. WP14 — Full Validation, Integration & Acceptance

### Objective

Reconcile the exact Release 1.4 candidate, prove it from a clean checkout, and create one review-ready integration PR under explicit WP14 authority.

### Starting state

- WP01–WP13 Closed/Done.
- WP14 Open/Backlog.
- authoritative Release 1.4 milestone open.
- `main` synchronized with `origin/main`.
- zero staged paths.
- cumulative candidate matches the manifest.
- no competing Release 1.4 integration PR.

### Mandatory reconciliation before integration

Derive candidate truth from the repository, not from assumed counts.

Validate:

- every governed path belongs to the accepted Release 1.4 candidate;
- no missing/unexpected/duplicate path;
- no generated/database residue;
- every governed `*-codex-prompt.md` has exactly one corresponding `*-codex-prompt-chat.md`;
- every governed chat companion has exactly five non-empty logical lines;
- no out-of-band authority/correction files are accidentally included;
- direct whitespace checks include untracked candidate files;
- package/reference/schema deltas match authority.

Malformed governance or candidate ambiguity is a stop condition. Do not stage around it.

### Full acceptance

Prove:

- Release 1.4 semantics;
- exact feature formula/fidelity/order;
- identity/provenance/evidence;
- empty results;
- failure mapping and unknown propagation;
- Application ownership;
- schema v2;
- zero feature persistence;
- DI/configuration;
- one-shot Worker;
- Release 1.1–1.3 regressions;
- Release 1.5 exclusions;
- documentation;
- architecture;
- security/offline behavior;
- canonical verification;
- exact permanent test counts;
- zero residue.

### Integration policy

Only after all pre-integration gates pass:

1. Create one integration branch named consistently with established release conventions, recommended:
   `release/1.4-deterministic-feature-engineering-foundation`.
2. Stage exactly the reconciled candidate.
3. Require staged `git diff --check` PASS.
4. Create exactly one integration commit, recommended message:
   `feat: establish Release 1.4 deterministic feature engineering foundation`.
5. Re-run full validation after commit.
6. Prove the exact commit in a detached fresh worktree.
7. Push normally; never force push.
8. Create one non-draft review-ready PR to `main`.
9. Do not enable auto-merge.
10. Post WP14 evidence, close the WP14 issue, and set it Done only after the integration result is proven.
11. Leave the Release 1.4 milestone open.
12. Do not merge the PR.
13. Do not create a tag or GitHub Release.
14. Do not delete the integration branch.

### Final WP14 terminal state

Expected:

- one clean integration commit;
- one pushed integration branch;
- one open review-ready PR;
- all WP01–WP14 issues Closed/Done;
- Release 1.4 milestone still Open;
- PR unmerged;
- working tree clean;
- no Release 1.5 work.

Human review and explicit merge authorization are required after WP14.

## 22. Post-Merge Closure Boundary

Post-merge closure is not WP14.

After a human merges the Release 1.4 integration PR, a separate post-merge closure authority must independently:

- verify the merge and accepted candidate tree;
- synchronize local `main` safely;
- reconcile the merged candidate;
- rerun canonical validation and permanent tests;
- prove a fresh checkout;
- verify issues WP01–WP14 Closed/Done;
- close only the authoritative Release 1.4 milestone if every gate passes;
- make no repository-content mutation;
- create no tag or GitHub Release unless separately authorized;
- leave Release 1.5 unstarted.

## 23. GitHub Lifecycle Rules

GitHub planning is governed separately from this execution plan.

After planning is accepted:

- exactly one authoritative Release 1.4 milestone should govern WP01–WP14;
- exactly 14 work-package issues should exist;
- no WP15+, closure issue, or lifecycle-gate issue is part of the release;
- Project #2 should have one `Release = 1.4` option if explicitly authorized;
- initial WP status is Backlog;
- dependencies must match Section 5 exactly;
- only the active WP lifecycle may change during ordinary WP execution;
- milestone closure occurs only under post-merge closure authority.

Legacy milestone #45 must not be mutated except under explicit GitHub-planning authority.

## 24. Governance Prompt Rules

The governed Release 1.4 execution set should contain:

- one full Codex prompt per authorized planning/execution action that is intentionally part of the candidate;
- exactly one matching chat companion for each governed full prompt;
- each chat companion exactly five non-empty logical lines;
- no duplicate logical companion;
- no temporary correction/resume authority included in the final candidate unless a later explicit authority incorporates it.

Prompt authorities are governance artifacts, not implementation evidence. Their presence does not authorize scope beyond the accepted definition, this plan, the manifest, and the active WP prompt.

## 25. Validation Standard

The canonical repository command is:

```powershell
./eng/verify.ps1 -Configuration Release
```

If PowerShell execution policy prevents invocation, use the already established safe process-scoped execution-policy procedure; do not weaken machine-wide policy as part of Release 1.4.

Every report must state actual observed:

- restore/build result;
- warnings/errors;
- Domain/Application/Infrastructure/Architecture test counts;
- total permanent tests;
- skipped tests;
- Gitleaks result;
- diff/whitespace result;
- residue result;
- branch/HEAD/upstream state;
- staged paths;
- current/successor issue state;
- milestone state.

## 26. Release Completion Definition

Release 1.4 is not complete when WP14 finishes.

It is complete only after:

1. WP14 produces a validated review-ready integration PR.
2. A human explicitly reviews and merges that PR.
3. Separate post-merge closure authority validates merged `main`.
4. Fresh-checkout verification passes.
5. All WP01–WP14 issues are Closed/Done.
6. The authoritative Release 1.4 milestone is closed.
7. Repository state is clean and synchronized.
8. Release 1.5 remains unstarted.

Until then, status must distinguish `WP14 COMPLETE`, `PR MERGED`, and `RELEASE 1.4 CLOSED`.

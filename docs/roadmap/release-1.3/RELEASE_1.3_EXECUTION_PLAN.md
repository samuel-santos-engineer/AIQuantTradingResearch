# Release 1.3 Execution Plan

## Phase 3 --- Release 1.3: Research Pipeline Foundation

**Status:** Definition accepted; implementation not started\
**Predecessor:** Release 1.2 --- Research Dataset Foundation --- CLOSED\
**Authoritative definition:**
`docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`\
**Execution model:** Governed work packages, WP01--WP14\
**Release boundary:** Deterministic, explicit, one-shot research
pipeline over accepted persisted historical observations.

------------------------------------------------------------------------

## 1. Purpose

Release 1.3 establishes the first bounded research-pipeline capability
in AIQuantTradingResearch.

The release composes already accepted Release 1.1 historical-observation
persistence and Release 1.2 deterministic dataset materialization,
immutable snapshot persistence, and catalog registration into a fixed
Application-owned pipeline.

The release does **not** create a general workflow engine. It must
remain deterministic, sequential, explicit, offline-capable, and
one-shot.

Canonical flow:

``` text
Explicit request
  → historical observation retrieval
  → deterministic dataset materialization
  → immutable snapshot persistence
  → catalog registration
  → structured execution result/evidence
```

## 2. Release invariants

All work packages must preserve these invariants:

1.  Release 1.1 historical observations remain the accepted source
    truth.
2.  Release 1.2 dataset semantics remain authoritative and are reused,
    not redesigned.
3.  Dataset identity scheme `aiq-dataset-identity-v1` remains unchanged
    unless a separately authorized correction is required.
4.  SQLite remains schema version 2; Release 1.3 does not require schema
    evolution.
5.  The production dependency graph remains:
    -   Domain → none
    -   Application → Domain
    -   Infrastructure → Application
    -   Worker → Application, Infrastructure
6.  Pipeline orchestration belongs to Application.
7.  Infrastructure continues to implement existing persistence
    boundaries; it does not own pipeline semantics.
8.  Worker remains a bounded composition root and one-shot trigger.
9.  No pipeline-managed live provider acquisition is introduced.
10. No scheduling, refresh loop, retry framework, circuit breaker,
    fallback provider, configurable DAG, plugin pipeline, parallel
    execution, streaming execution, distributed execution,
    checkpoint/resume, feature engineering, model training, or MLOps is
    introduced.
11. No secret, credential, connection string, or sensitive configuration
    value may be emitted in execution evidence.
12. Every WP must preserve repository cleanliness and run the canonical
    validation required by its authority.

## 3. Baseline acceptance state

The starting baseline for WP01 is the accepted Release 1.2 post-merge
closure state:

-   PR #137 merged.
-   Milestone #53 closed.
-   Issues #121--#136 Closed/Done.
-   `main` synchronized with `origin/main`.
-   SQLite schema version 2.
-   Permanent tests: 171.
    -   Domain.Tests: 11
    -   Application.Tests: 60
    -   Infrastructure.Tests: 87
    -   Architecture.Tests: 13
-   Build warnings/errors: 0/0.
-   Canonical verification: PASS.
-   Gitleaks: PASS.
-   Working tree expected clean except for explicitly authorized
    out-of-band Release 1.3 governance artifacts.
-   Release 1.3 implementation not started.

WP01 must independently verify this baseline before any implementation
work is authorized.

## 4. Work-package graph

  -----------------------------------------------------------------------------------------------
  WP             Work package    Depends on     Recommended    Primary outcome
                                                model
  -------------- --------------- -------------- -------------- ----------------------------------
  WP01           Release &       Release 1.2    **Luna**       Verify closure, repository,
                 Repository      CLOSED                        governance, toolchain,
                 Preflight                                     architecture, tests, schema v2,
                                                               and scope

  WP02           Research        WP01           **Sol**        Freeze pipeline vocabulary,
                 Pipeline                                      boundaries, determinism, stage
                 Semantic                                      model, and exclusions
                 Discovery

  WP03           Pipeline        WP02           **Sol**        Define pipeline definition
                 Identity,                                     identity, semantic run identity,
                 Provenance &                                  provenance, and evidence semantics
                 Evidence
                 Semantics

  WP04           Application     WP03           **Terra**      Introduce
                 Pipeline                                      provider/storage-independent
                 Contracts                                     Application contracts

  WP05           Fixed Pipeline  WP04           **Terra**      Implement deterministic sequential
                 Orchestration                                 Application orchestration

  WP06           Pipeline        WP03, WP05     **Sol**        Define and implement validation,
                 Validation &                                  stage attribution, and bounded
                 Failure                                       failure semantics
                 Semantics

  WP07           Structured      WP03, WP05,    **Terra**      Produce bounded structured local
                 Execution       WP06                          execution evidence without durable
                 Evidence                                      run-history storage

  WP08           Dependency      WP04--WP07     **Terra**      Register pipeline services and
                 Registration &                                explicit configuration without
                 Configuration                                 resolution side effects

  WP09           One-Shot Worker WP08           **Terra**      Execute exactly one bounded
                 Pipeline                                      pipeline invocation from Worker
                 Execution

  WP10           Application     WP03--WP06     **Luna**       Add permanent deterministic
                 Pipeline Tests                                Application coverage

  WP11           Composition &   WP07--WP09     **Terra**      Add permanent
                 Worker                                        composition/configuration/Worker
                 Validation                                    validation where justified

  WP12           Architecture    WP09--WP11     **Terra**      Reconcile executable architecture
                 Evolution                                     rules; zero rule/test delta is
                                                               valid if no new rule is justified

  WP13           Documentation   WP12           **Terra**      Align current-state architecture
                 Alignment                                     and navigation documentation

  WP14           Full            WP10--WP13     **Sol**        Reconcile exact candidate,
                 Validation,                                   validate, integrate, push, and
                 Integration &                                 create review-ready PR
                 Acceptance
  -----------------------------------------------------------------------------------------------

### Dependency edges

``` text
Release 1.2 CLOSED
  ↓
WP01
  ↓
WP02
  ↓
WP03
  ├──────────────→ WP06
  ↓                 ↑
WP04                │
  ↓                 │
WP05 ───────────────┘
  ↓
WP07  ← WP03, WP06
  ↓
WP08  ← WP04–WP07
  ↓
WP09
  ├────────────→ WP11 ← WP07, WP08
  │                 ↓
  │               WP12
  │                 ↓
  │               WP13
  │
WP10 ← WP03–WP06
  │
  └──────────────┐
                 ↓
               WP14 ← WP11–WP13
```

The issue dependency representation must use the exact dependency list
in each WP section below, not infer additional edges from the diagram.

## 5. Model-selection authority

The model recommendations are cost/complexity guidance, not semantic
authority.

-   **Sol** --- use for open semantic decisions, identity/failure
    design, candidate reconciliation, and final release acceptance.
-   **Terra** --- use for bounded implementation, composition,
    architecture, and documentation work after semantics are fixed.
-   **Luna** --- use for structured preflight and deterministic test
    expansion where the design is already established.

If a WP encounters an authority contradiction or genuinely open semantic
decision beyond its assigned tier, it must stop rather than silently
broaden scope. A human may explicitly choose a stronger model without
changing WP authority.

## 6. Work-package authorities

### WP01 --- Release & Repository Preflight

**Dependency:** Release 1.2 CLOSED\
**Model:** Luna\
**Issue target:** Release 1.3 WP01

Must verify:

-   Release 1.2 post-merge closure.
-   Release 1.3 definition and governance artifacts.
-   authoritative GitHub planning state after separately authorized
    planning exists.
-   `main` synchronization and working-tree classification.
-   SDK/toolchain and project graph.
-   schema version 2.
-   171-test baseline or explain any legitimate accepted baseline
    advancement.
-   canonical verification and secret scan.
-   no Release 1.3 implementation before WP01.
-   no Release 1.4+ behavior.

**Production/test delta:** 0.\
**Completion:** evidence-only lifecycle closure.

### WP02 --- Research Pipeline Semantic Discovery

**Dependency:** WP01\
**Model:** Sol

Create the semantic authority for:

-   Research Pipeline.
-   Pipeline Definition.
-   Pipeline Stage.
-   Pipeline Execution.
-   Pipeline Result.
-   deterministic stage ordering.
-   fixed sequential topology.
-   accepted source boundary.
-   relationship to Release 1.2 dataset materialization.
-   success, equivalence, empty-dataset, and fail-stop semantics.
-   replay/re-execution semantics.
-   explicit one-shot trigger.
-   operational vs semantic data.
-   Release 1.4+ exclusions.

No production implementation, test expansion, DI, Worker execution,
persistence schema, or Git integration.

### WP03 --- Pipeline Identity, Provenance & Evidence Semantics

**Dependency:** WP02\
**Model:** Sol

Define, without storage redesign:

-   Pipeline Definition Identity.
-   semantic Pipeline Run Identity.
-   distinction between semantic identity and ephemeral
    invocation/correlation identity.
-   canonical representation/digest strategy if required.
-   provenance linking pipeline definition, dataset
    definition/snapshot/version, and execution semantics.
-   evidence semantics and required fields.
-   equivalence and distinguishability rules.
-   failure-stage attribution semantics.
-   empty dataset behavior.
-   identity evolution/versioning rules.
-   prohibited operational inputs to semantic identity.

No durable operational run-history subsystem.

### WP04 --- Application Pipeline Contracts

**Dependency:** WP03\
**Model:** Terra

Introduce the minimum Application-owned contract surface necessary for
later orchestration:

-   pipeline request/definition.
-   typed identities required by WP03.
-   stage vocabulary.
-   result/outcome.
-   bounded failure representation.
-   provenance/evidence descriptors where required.
-   pipeline execution use-case seam.

Contracts must remain provider-independent and storage-independent. Do
not add Infrastructure or Worker behavior.

### WP05 --- Fixed Pipeline Orchestration

**Dependency:** WP04\
**Model:** Terra

Implement the deterministic Application-owned fixed pipeline by
composing accepted Release 1.2 capabilities.

Required properties:

-   explicit request in.
-   fixed sequential stage order.
-   existing historical-observation/dataset materialization semantics
    reused.
-   snapshot persistence and catalog registration reused through
    existing seams.
-   no live provider acquisition.
-   deterministic result for equivalent semantic inputs/source state.
-   fail-stop behavior.
-   no retry, fallback, checkpoint, resume, scheduling, loop, DAG,
    plugin, parallelism, or durable run history.

### WP06 --- Pipeline Validation & Failure Semantics

**Dependencies:** WP03, WP05\
**Model:** Sol

Reconcile and harden:

-   request/definition validation.
-   identity consistency.
-   stage-transition validity.
-   pipeline-level stage attribution.
-   preservation of existing dataset/store failure distinctions.
-   invalid semantic evidence.
-   unknown failure propagation.
-   no broad exception swallowing.
-   no automatic retry/recovery.
-   non-destructive failure behavior.

Do not redesign Release 1.2 failure vocabulary unless strictly required
and explicitly justified.

### WP07 --- Structured Execution Evidence

**Dependencies:** WP03, WP05, WP06\
**Model:** Terra

Implement bounded structured local evidence sufficient to explain an
invocation:

-   pipeline identity.
-   semantic run identity.
-   dataset/snapshot/version identity where available.
-   stage.
-   outcome.
-   failure classification.
-   non-secret correlation information.

Evidence must not:

-   become semantic identity merely because it is logged.
-   expose secrets or connection strings.
-   create persisted operational run history.
-   introduce metrics/tracing backends.
-   require schema evolution.

### WP08 --- Dependency Registration & Configuration

**Dependencies:** WP04, WP05, WP06, WP07\
**Model:** Terra

Register only the accepted pipeline abstractions and configuration.

Must preserve:

-   existing Release 1.2 dataset registrations.
-   operation-safe lifetimes.
-   no database/provider activity from graph resolution.
-   explicit deterministic configuration validation.
-   no secret logging.
-   schema v2.
-   no scheduler/background-service registration.

### WP09 --- One-Shot Worker Pipeline Execution

**Dependency:** WP08\
**Model:** Terra

Extend the bounded Worker composition root to:

-   accept one explicit pipeline request/configuration.
-   execute the pipeline once.
-   emit bounded structured evidence.
-   return deterministic process success/failure.
-   remain offline-capable when source observations already exist.

Must not introduce recurring execution, scheduling, polling, refresh
loops, background processing, or live acquisition orchestration.

### WP10 --- Application Pipeline Tests

**Dependencies:** WP03, WP04, WP05, WP06\
**Model:** Luna

Add permanent pure Application tests covering missing WP03--WP06
semantics, including as applicable:

-   pipeline definition validation.
-   identity/equivalence.
-   deterministic stage order.
-   successful non-empty and empty flows.
-   equivalent re-execution.
-   relevant semantic change distinguishability.
-   fail-stop behavior.
-   stage-attributed failures.
-   existing dataset failure propagation.
-   unknown failure behavior.

No SQLite, Worker, network, provider, or credentials.

### WP11 --- Composition & Worker Validation

**Dependencies:** WP07, WP08, WP09\
**Model:** Terra

Add the minimum permanent tests justified for:

-   DI registration.
-   lifetime correctness.
-   resolution without database/provider side effects.
-   configuration rejection.
-   one-shot Worker/composition behavior.
-   structured evidence safety.
-   offline execution.
-   temporary database cleanup if a file-backed SQLite proof is
    required.

Do not duplicate Application semantics already owned by WP10.

### WP12 --- Architecture Evolution

**Dependencies:** WP09, WP10, WP11\
**Model:** Terra

Reconcile Release 1.3 against executable architecture rules.

This WP **must not assume new architecture tests are required**.

It must:

1.  inspect the accepted production graph and existing 13 architecture
    tests;
2.  identify any genuinely new enforceable Release 1.3 boundary;
3.  add or modify architecture tests only when the rule is stable,
    repository-wide, and not already enforced;
4.  accept a zero architecture-test delta when the existing rules fully
    protect the new capability;
5.  never change the production graph merely to justify an
    architecture-test change.

### WP13 --- Documentation Alignment

**Dependency:** WP12\
**Model:** Terra

Align only documentation that is stale or materially incomplete after
implementation.

Must document current truth for:

-   fixed one-shot pipeline.
-   Application ownership.
-   Release 1.1/1.2 reuse.
-   identity/provenance/evidence.
-   failure semantics.
-   DI/configuration.
-   Worker execution.
-   test responsibilities.
-   schema v2 unchanged.
-   Release 1.4+ deferrals.

No production/test behavior changes.

### WP14 --- Full Validation, Integration & Acceptance

**Dependencies:** WP10, WP11, WP12, WP13\
**Model:** Sol

WP14 is the sole Release 1.3 integration authority.

It must:

1.  verify WP01--WP13 lifecycle completion.
2.  derive the exact candidate from repository truth and the file
    manifest.
3.  verify every governed prompt has one standard exactly-five-line
    companion.
4.  reconcile missing/unexpected/duplicate paths.
5.  validate no Release 1.4+ implementation.
6.  run restore, format, build, all permanent tests, architecture tests,
    canonical verification, Gitleaks, whitespace, documentation/link,
    residue, and architecture checks.
7.  perform focused offline one-shot pipeline acceptance.
8.  preserve Release 1.1 and Release 1.2 regressions.
9.  create exactly one integration branch.
10. stage exactly the accepted candidate.
11. create exactly one integration commit unless an authority-approved
    correction is required before staging.
12. rerun post-commit validation.
13. validate the exact commit from a clean detached/fresh worktree.
14. push without force.
15. create one review-ready PR to `main`.
16. close WP14/mark Done only after every gate passes.
17. leave the authoritative Release 1.3 milestone OPEN until human merge
    and separate post-merge closure authority.

WP14 must not merge its own PR, close the milestone, create a tag/GitHub
Release, or begin Release 1.4.

## 7. Prompt governance

Each standard Release 1.3 execution authority consists of:

``` text
<authority>-codex-prompt.md
<authority>-codex-prompt-chat.md
```

Every standard chat companion must contain **exactly five non-empty
logical lines**.

The full prompt is authoritative. The companion is only a bootstrap and
must direct Codex to read the full authority completely.

Out-of-band corrective/resume authorities are excluded from the release
candidate unless a later authority explicitly incorporates them.

No WP may silently normalize, rewrite, delete, stage, or commit another
WP's authority.

## 8. GitHub planning lifecycle

GitHub planning requires a separate authority before WP01.

Recommended planning state:

-   preserve legacy milestone #44 (`Phase 3 - Release 1.3: Pipelines`)
    as historical/legacy and empty;
-   do not rename, reopen, repurpose, or attach new WP issues to #44;
-   create exactly one authoritative milestone:
    `Phase 3 - Release 1.3: Research Pipeline Foundation`;
-   create exactly 14 authoritative WP issues;
-   add exactly one Project #2 `Release = 1.3` option if absent;
-   add all 14 WP issues to Project #2;
-   initial status: Backlog;
-   priority: P1 unless separately changed by human authority;
-   populate Area consistently with repository conventions;
-   preserve exact dependency graph;
-   WP01 remains unstarted until GitHub planning is accepted.

WP completion lifecycle:

``` text
Backlog → In Progress → Closed / Done
```

A WP may move to In Progress only after its starting-state gates pass.
It may close only after all acceptance gates pass.

## 9. Repository mutation policy

Unless a WP explicitly authorizes otherwise:

-   no staging;
-   no commit;
-   no push;
-   no branch;
-   no PR;
-   no merge;
-   no tag;
-   no GitHub Release;
-   no history rewrite;
-   no unrelated GitHub mutation.

WP14 is the integration exception.

A blocked WP must stop with zero unauthorized mutation and report the
smallest corrective authority required.

## 10. Validation standard

Every implementation WP must run the strongest applicable subset of:

``` text
dotnet restore
dotnet format --verify-no-changes
dotnet build
dotnet test
eng/verify.ps1
git diff --check
git diff --cached --check
```

The repository's canonical scripts remain authoritative over ad-hoc
substitutes.

Additional required checks where applicable:

-   Gitleaks/secret scanning.
-   architecture tests.
-   documentation link validation.
-   temporary SQLite/WAL/SHM/journal residue scan.
-   offline execution proof.
-   exact test-count accounting.
-   package/reference delta accounting.
-   production dependency graph.
-   source/provider/network access accounting.

Temporary probes may be used only when a WP authority permits them. They
must be removed before completion and must not replace permanent
coverage assigned to WP10/WP11.

## 11. Security and offline policy

Release 1.3 must remain safe to validate without real provider
credentials.

-   No real API key is required for pipeline semantics.
-   No live provider call is required for acceptance.
-   Dummy configuration may be used only when necessary to satisfy
    unrelated composition validation and when the executed path does not
    call the provider.
-   Secrets must never be printed.
-   Gitleaks must pass.
-   Temporary database files must be removed.

## 12. Schema and persistence policy

Release 1.3 has **no authorized schema evolution**.

Expected state:

``` text
PRAGMA user_version = 2
```

WP01--WP14 must treat any need for schema version 3, new run-history
tables, checkpoint tables, scheduler tables, or pipeline-definition
persistence as an authority conflict and stop.

Existing Release 1.2 dataset snapshot/catalog persistence may be reused
but not redesigned outside a separately authorized correction.

## 13. Release 1.4+ exclusion gate

The following are forbidden in Release 1.3:

-   pipeline-managed live provider acquisition;
-   scheduler/cron/background refresh;
-   configurable DAGs;
-   pipeline plugin architecture;
-   parallel/streaming/distributed pipeline execution;
-   automatic retries;
-   circuit breakers;
-   fallback-provider orchestration;
-   durable checkpoints;
-   partial-run resume;
-   persisted operational run history;
-   external metrics backend;
-   distributed tracing backend;
-   feature-generation/enrichment pipeline;
-   model training/evaluation;
-   MLOps.

Finding any of these in the candidate without explicit corrective
authority blocks WP14 acceptance.

## 14. Release integration and closure sequence

The lifecycle is intentionally separated:

1.  Definition accepted.
2.  Execution plan and file manifest accepted.
3.  GitHub planning separately authorized and accepted.
4.  WP01--WP13 executed and accepted.
5.  WP14 validates and creates the review-ready integration PR.
6.  Human reviews and explicitly merges the PR.
7.  A separate post-merge closure authority verifies merged-main
    equivalence and fresh-checkout reproducibility.
8.  Only that closure authority may close the authoritative Release 1.3
    milestone.
9.  Release 1.4 remains separately governed.

## 15. Final Release 1.3 acceptance criteria

Release 1.3 is implementation-complete only when WP14 proves:

-   exact candidate reconciliation: PASS;
-   governance prompt pairs: PASS;
-   build warnings/errors: 0/0;
-   all permanent tests: PASS;
-   architecture tests: PASS;
-   canonical verification: PASS;
-   Gitleaks: PASS;
-   deterministic fixed pipeline: PASS;
-   identity/provenance/evidence semantics: PASS;
-   fail-stop validation/failure semantics: PASS;
-   structured evidence safety: PASS;
-   DI/configuration: PASS;
-   one-shot Worker execution: PASS;
-   schema remains v2: PASS;
-   Release 1.1 regression: PASS;
-   Release 1.2 regression: PASS;
-   documentation acceptance: PASS;
-   offline validation: PASS;
-   fresh-checkout reproducibility: PASS;
-   Release 1.4+ implementation: 0;
-   review-ready PR: OPEN;
-   milestone: OPEN pending merge.

The release is **closed** only after the separately authorized
post-merge closure verifies the merged candidate and closes the
milestone.

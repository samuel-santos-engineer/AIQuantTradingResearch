# Release 1.2 WP12 --- Dependency Registration & Bounded Dataset Execution

## Codex Execution Authority

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`\
**Release:** 1.2 --- Research Dataset Foundation\
**Work package:** WP12 --- Dependency Registration & Bounded Dataset
Execution\
**GitHub issue:** #132\
**Recommended model:** GPT-5.6 Terra

------------------------------------------------------------------------

## 1. Mission

Execute WP12 only.

Complete the minimum dependency-registration, configuration, and bounded
host-execution work required to make the accepted Release 1.2 dataset
flow resolvable and deliberately executable through the existing
composition root.

WP12 must connect the already accepted WP04--WP11 dataset abstractions
and implementations. It must **not** redesign dataset semantics,
identity, storage, persistence, catalog behavior, validation, or failure
mapping.

This is bounded Release 1.2 execution, **not** a Release 1.3 pipeline.

------------------------------------------------------------------------

## 2. Authoritative Inputs

Before mutation, read completely and reconcile:

1.  `RELEASE_1.2_EXECUTION_PLAN.md`
2.  `RELEASE_1.2_FILE_MANIFEST.md`
3.  `12-dependency-registration-bounded-dataset-execution-codex-prompt.md`
4.  `12-dependency-registration-bounded-dataset-execution-codex-prompt-chat.md`
5.  Accepted WP02 dataset-definition artifact.
6.  Accepted WP03 identity/version/provenance artifact.
7.  Accepted WP04 Application dataset contracts.
8.  Accepted WP05 materialization use case and identity computer.
9.  Accepted WP06 catalog model.
10. Accepted WP07 SQLite schema-v2 physical model and mapper/bootstrap
    work.
11. Accepted WP08 snapshot persistence.
12. Accepted WP09 catalog persistence and lookup.
13. Accepted WP10 materialization integration.
14. Accepted WP11 validation/failure mapping.
15. Existing Release 1.0/1.1 Application and Infrastructure DI
    conventions.
16. Current Worker composition and bounded Release 1.1 execution
    behavior.
17. Current permanent tests and architecture tests.
18. GitHub issue #132, milestone #53, Project #2, and predecessor
    lifecycle state.

If repository truth conflicts with this authority, stop and report the
conflict. Do not silently reinterpret the authority.

------------------------------------------------------------------------

## 3. Starting-State Gates

Before changing anything, prove and report:

-   Repository is `samuel-santos-engineer/AIQuantTradingResearch`.
-   Current branch and HEAD.
-   Local branch relationship to `origin/main`.
-   Ahead/behind counts.
-   Staged paths.
-   Tracked/untracked working-tree classification.
-   Every pre-existing path is attributable to accepted cumulative
    Release 1.2 work or this WP12 authority.
-   Unexpected/ambiguous paths = 0.
-   Release 1.1 remains closed.
-   Milestone #53 is open.
-   WP11 issue #131 is Closed/Done.
-   WP12 issue #132 is Open/Backlog.
-   WP13 issue #133 remains Open/Backlog.
-   WP12 dependencies match the authoritative planning graph.
-   WP13 has not started.
-   Release 1.3 implementation has not started.

Run the unchanged baseline before mutation:

-   restore;
-   format verification;
-   build;
-   all permanent tests;
-   architecture tests;
-   canonical `eng/verify.ps1`;
-   `git diff --check`;
-   `git diff --cached --check`.

Prefer the repository's accepted canonical configuration. If the
previously observed local Windows Application Control behavior recurs
for Debug artifacts, do not work around system policy or mutate
repository configuration merely to bypass it. Record the observation and
use the accepted Release verification path where appropriate.

Only after all starting gates pass may issue #132 move from Backlog to
In Progress.

------------------------------------------------------------------------

## 4. Accepted Architecture That Must Not Be Redesigned

Preserve:

-   Domain → none
-   Application → Domain
-   Infrastructure → Application
-   Worker → Application, Infrastructure

Domain and Application remain independent of:

-   SQLite;
-   SQL;
-   filesystem paths;
-   provider HTTP mechanics;
-   host configuration APIs.

Infrastructure owns SQLite implementation.

Worker remains the composition root and bounded host.

No new project-reference edge is authorized unless the Release 1.2
manifest explicitly requires it.

------------------------------------------------------------------------

## 5. Accepted Dataset Flow

WP12 must compose the existing accepted flow rather than recreate it.

Conceptually:

`DatasetDefinition` → materialization → immutable snapshot persistence →
catalog registration → bounded integrated result

Use the existing WP10 Application orchestration seam as the
authoritative integration boundary.

Do not bypass WP10 by manually reproducing its sequence in Worker.

Do not recompute identities in Worker or Infrastructure composition.

Do not create a second orchestration use case.

------------------------------------------------------------------------

## 6. Dependency Registration Requirements

Inspect existing `AddApplication`, `AddInfrastructure`, and Worker
registration conventions first.

Register only what is necessary for the accepted dataset graph.

At minimum, reconcile and make resolvable as required by the actual
contracts:

-   WP05 materialization use-case abstraction/implementation;
-   WP10 materialization-integration use-case
    abstraction/implementation, if an abstraction exists;
-   `IDatasetSnapshotStore` → accepted SQLite snapshot-store
    implementation;
-   `IDatasetCatalog` → accepted SQLite catalog implementation;
-   existing `IHistoricalObservationStore` dependency from Release 1.1;
-   existing `ISqliteConnectionFactory`;
-   accepted SQLite configuration.

Do not duplicate an existing registration.

Do not introduce a service locator.

Do not register live `SqliteConnection` instances.

Connections must remain operation-owned and deterministically disposed
by Infrastructure.

Choose lifetimes from actual object ownership:

-   immutable/stateless configuration/factories may retain their
    accepted lifetime;
-   use cases/stores should use the narrowest lifetime consistent with
    existing conventions and dependencies;
-   no singleton may capture a live connection or mutable execution
    state.

Document every registration and lifetime decision in the execution
report.

------------------------------------------------------------------------

## 7. Configuration Boundary

Reuse the accepted Release 1.1 persistence configuration model wherever
possible.

`Persistence:DatabasePath` remains the authoritative SQLite
database-path input unless repository authority explicitly establishes
otherwise.

Requirements:

-   no hidden default database path;
-   no silent in-memory fallback;
-   no personal/local absolute path committed;
-   no connection string or path logged as sensitive diagnostic data;
-   missing/blank required configuration fails deterministically;
-   DI container construction/service resolution must not create or
    mutate the database merely as a side effect.

Do not introduce Release 1.3 pipeline configuration.

------------------------------------------------------------------------

## 8. Bounded Dataset Execution

Add only the minimum Worker/composition-root behavior needed to
deliberately execute one bounded dataset-materialization integration.

The execution must:

1.  obtain the accepted integrated Application use case from DI;
2.  construct or obtain one explicit `DatasetDefinition` using existing
    host configuration conventions;
3.  invoke the integrated use case once per bounded host execution;
4.  handle the existing integrated success/failure result without
    redefining its semantics;
5.  terminate or return control according to the existing bounded Worker
    lifecycle.

The definition must preserve WP02 semantics:

-   exact target;
-   explicit `[from, to)` boundaries;
-   no trimming/case folding/normalization;
-   deterministic semantics independent of local culture/timezone.

Prefer explicit external configuration for definition inputs if the
current Worker architecture already uses configuration for bounded
execution. Do not invent a generalized dataset-job configuration
framework.

If exact configuration keys are not already dictated by the execution
plan/manifest, choose the smallest coherent Release 1.2-specific shape
and document it. Do not build a future pipeline DSL.

------------------------------------------------------------------------

## 9. Bounded Means Bounded

WP12 is expressly prohibited from introducing:

-   continuous loops for dataset refresh;
-   recurring scheduling;
-   cron semantics;
-   timers for dataset refresh;
-   streaming materialization;
-   polling;
-   queues;
-   DAGs/workflow engines;
-   automatic re-materialization;
-   background refresh;
-   change detection;
-   retries/backoff/circuit breakers for dataset execution;
-   pipeline monitoring;
-   pipeline checkpoints;
-   multi-dataset orchestration;
-   concurrency framework;
-   distributed transactions;
-   Release 1.3 pipeline abstractions.

One deliberate execution path is sufficient.

If the existing Worker itself has a lifecycle loop from earlier
releases, do not broaden that loop into a dataset pipeline. Keep the
dataset action explicitly bounded according to the Release 1.2
authority.

------------------------------------------------------------------------

## 10. Result and Failure Handling

Reuse the accepted WP10/WP11 result vocabulary exactly.

Preserve distinctions including, where represented by the current
contracts:

-   newly accepted;
-   equivalent existing;
-   integrity conflict;
-   source-history unavailable;
-   snapshot-store unavailable;
-   invalid persisted evidence mapped through the accepted integration
    boundary.

Do not:

-   add a new public failure category without authority;
-   convert conflicts into success;
-   convert `NotFound` into a storage failure;
-   catch arbitrary `Exception`;
-   swallow unknown SQLite/programming failures;
-   add retryability policy;
-   repair malformed evidence;
-   overwrite immutable evidence.

Worker may translate accepted results into bounded host control/logging
behavior, but must not redefine their semantic meaning.

------------------------------------------------------------------------

## 11. Logging

Use existing logging conventions only if logging is already part of the
Worker pattern.

Logs may communicate bounded execution status using non-sensitive
semantic facts.

Do not log:

-   API keys;
-   connection strings;
-   secrets;
-   personal filesystem paths;
-   raw sensitive configuration.

Avoid creating a new observability framework.

------------------------------------------------------------------------

## 12. WP11 Regression Protection

WP12 must preserve all accepted WP11 behavior:

-   `DatasetStoreFailure.Unavailable`;
-   `DatasetStoreFailure.InvalidData`;
-   SQLite known-unavailable classification;
-   malformed schema/evidence containment;
-   unknown failures propagating;
-   integrity conflicts remaining non-destructive;
-   `NotFound` remaining distinct;
-   equivalent existing remaining success;
-   no `catch (Exception)`;
-   schema version remains 2.

Do not move SQLite exception mapping into Worker.

------------------------------------------------------------------------

## 13. Release 1.1 Regression Protection

Preserve:

-   historical observation persistence;
-   historical retrieval;
-   exact target semantics;
-   immutable/idempotent/conflict behavior;
-   timestamp/offset/decimal fidelity;
-   Release 1.1 failure mapping;
-   `Persistence:DatabasePath`;
-   existing provider composition;
-   existing Worker behavior not explicitly superseded by Release 1.2.

No Release 1.1 contract redesign is authorized.

------------------------------------------------------------------------

## 14. Testing Policy for WP12

WP12 is implementation/composition work. Permanent test ownership
remains governed by the Release 1.2 manifest, especially WP13 and WP14.

Do not pre-empt WP13/WP14 by adding broad permanent dataset test suites
unless the manifest explicitly assigns a specific WP12 test file.

You may create minimal temporary offline probes to prove:

-   concrete Microsoft DI container build;
-   dataset graph resolution;
-   no database mutation from service resolution alone;
-   exact configuration handoff;
-   one bounded successful execution;
-   equivalent rerun if necessary to validate composition;
-   missing/blank required configuration rejection;
-   zero provider/network activity during purely composition-focused
    probes where applicable.

Temporary probes and databases must be removed before completion.

Do not use live credentials or external provider calls for WP12
validation.

------------------------------------------------------------------------

## 15. Expected Scope

Prefer the smallest manifest-authorized mutation.

Likely surfaces include only:

-   existing Application DI registration;
-   existing Infrastructure DI registration;
-   existing Worker composition/configuration/bounded execution;
-   narrowly necessary configuration files if explicitly authorized by
    the manifest.

Do not create speculative abstractions.

Before editing, compare the exact intended paths with
`RELEASE_1.2_FILE_MANIFEST.md`.

Any required file outside the authorized WP12 scope is a stop condition
unless the authority explicitly permits it.

------------------------------------------------------------------------

## 16. Explicitly Forbidden Scope

Do not implement WP13, WP14, WP15, or WP16.

Do not implement Release 1.3.

Do not:

-   change schema version 2;
-   redesign dataset identity/canonicalization;
-   alter SHA-256 identity semantics;
-   alter WP02 selection semantics;
-   alter WP04 public contracts except if an absolutely necessary
    manifest-authorized composition seam is missing;
-   redesign WP05 materialization;
-   redesign WP06 catalog metadata;
-   redesign WP08 snapshot persistence;
-   redesign WP09 catalog persistence;
-   redesign WP10 orchestration;
-   redesign WP11 failure mapping;
-   add packages unless explicitly unavoidable and authorized;
-   add project references unless explicitly authorized;
-   stage;
-   commit;
-   push;
-   create a branch;
-   create a PR;
-   merge;
-   tag;
-   create a GitHub release.

------------------------------------------------------------------------

## 17. Validation Requirements

After mutation run, at minimum:

1.  restore;
2.  format verification;
3.  build with zero warnings/errors;
4.  Domain tests;
5.  Application tests;
6.  Infrastructure tests;
7.  Architecture tests;
8.  canonical `eng/verify.ps1`;
9.  secret scan through canonical verification;
10. `git diff --check`;
11. `git diff --cached --check`;
12. direct whitespace checks for relevant untracked WP12 files when Git
    diff does not include them;
13. temporary SQLite residue check;
14. production dependency-graph reconciliation.

Expected permanent baseline before WP13/WP14 remains the accepted
current count unless the manifest explicitly assigns WP12 permanent
tests.

Report exact suite counts rather than assuming them.

------------------------------------------------------------------------

## 18. Acceptance Matrix

WP12 is complete only if all applicable rows pass:

  Requirement                                        Expected
  -------------------------------------------------- -------------------------------------
  WP11 predecessor                                   PASS
  Issue #132 lifecycle                               Backlog → In Progress → Closed/Done
  Existing DI conventions reused                     PASS
  Materialization use case resolvable                PASS
  WP10 integration use case resolvable               PASS
  `IDatasetSnapshotStore` concrete registration      PASS
  `IDatasetCatalog` concrete registration            PASS
  Release 1.1 historical store preserved             PASS
  SQLite connection factory preserved                PASS
  `Persistence:DatabasePath` handoff                 PASS
  Missing/blank required configuration               deterministic rejection
  Resolution-time DB mutation                        NO
  Hidden in-memory fallback                          NO
  Operation-owned connections                        PASS
  One bounded dataset execution                      PASS
  WP10 result semantics preserved                    PASS
  WP11 failure semantics preserved                   PASS
  Equivalent evidence preserved                      PASS
  Integrity conflict non-destructive                 PASS
  Schema version                                     2
  Provider/storage leakage into Domain/Application   0
  New pipeline/scheduling behavior                   0
  Provider/network calls in offline proof            0
  Temporary DB/probe residue                         0
  Package/reference delta                            0/0 unless explicitly authorized
  WP13 started                                       NO
  Release 1.3 started                                NO

------------------------------------------------------------------------

## 19. GitHub Lifecycle

After baseline gates pass, move issue #132:

`Backlog → In Progress`

Only after every WP12 acceptance gate passes:

-   post concise completion evidence to #132;
-   close #132;
-   set Project #2 status to Done.

Do not mutate #133 except to inspect it.

At completion verify:

-   #132 = Closed/Done;
-   #133 = Open/Backlog;
-   milestone #53 remains Open.

------------------------------------------------------------------------

## 20. Stop Conditions

Stop without speculative repair if any of the following occurs:

-   predecessor state is invalid;
-   unexpected working-tree path exists;
-   manifest conflicts with required mutation;
-   WP12 requires schema redesign;
-   accepted WP10 orchestration cannot be composed without redesign;
-   required configuration semantics are contradictory;
-   permanent-test authority conflicts with required implementation;
-   architecture would require an unauthorized dependency edge;
-   baseline is failing for a repository reason;
-   completion would require Release 1.3 behavior.

Report the smallest corrective authority required.

------------------------------------------------------------------------

## 21. Required Execution Report

Produce a numbered execution report covering at least:

1.  Executive Summary
2.  Authorities Reviewed
3.  Repository Context
4.  Initial Git State
5.  Working-Tree Classification
6.  Predecessor/Lifecycle Gates
7.  Issue Lifecycle
8.  Initial Baseline
9.  WP02--WP11 reconciliation
10. Existing composition reconciliation
11. Registration design
12. Application use-case registration
13. Snapshot-store registration
14. Catalog registration
15. Connection-factory/configuration reconciliation
16. Lifetime decisions
17. Configuration keys and validation
18. Resolution side-effect evidence
19. Bounded execution design
20. Dataset-definition handoff
21. Integrated-result handling
22. WP11 failure preservation
23. Release 1.1 regression protection
24. Exact files added/modified
25. Domain/Application/Infrastructure/Worker delta
26. Package/reference delta
27. Permanent test delta
28. Temporary probe evidence
29. WP13 protection
30. Release 1.3 protection
31. Security/offline evidence
32. Whitespace/diff evidence
33. Restore/build evidence
34. Permanent test evidence
35. Canonical verification
36. Architecture validation
37. Composition acceptance matrix
38. Bounded-execution acceptance matrix
39. Mutation accounting
40. Git/GitHub protection
41. Planning protection
42. Findings/blockers
43. Final repository/GitHub state
44. WP13 handoff
45. Final decision
46. Next authorized work package

End with:

`RELEASE 1.2 WP12 COMPLETE`

and:

`NEXT AUTHORIZED WORK PACKAGE: WP13 — Domain & Application Dataset Tests — GitHub issue #133`

Do not claim completion unless every mandatory gate passes.

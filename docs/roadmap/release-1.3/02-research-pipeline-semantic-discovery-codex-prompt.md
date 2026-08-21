# Release 1.3 WP02 — Research Pipeline Semantic Discovery — Codex Execution Authority

## Phase 3 — Release 1.3: Research Pipeline Foundation

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`
**GitHub issue:** `#139`
**Recommended model:** GPT-5.6 Sol
**Work-package type:** Semantic architecture / definition
**Production implementation:** NOT AUTHORIZED

## 1. Mission

Execute **WP02 — Research Pipeline Semantic Discovery** only.

The purpose of WP02 is to discover, reconcile, and freeze the semantic model for the Release 1.3 Research Pipeline Foundation before any pipeline contracts or implementation are introduced.

WP02 must define what the pipeline **means**, not how later work packages encode or implement it.

The resulting semantics become binding input to WP03–WP14. Do not start WP03 or later work.

## 2. Authorities

Read completely before mutation:

1. `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
2. `RELEASE_1.3_EXECUTION_PLAN.md`
3. `RELEASE_1.3_FILE_MANIFEST.md`
4. Release 1.3 GitHub-planning authority and accepted planning result
5. WP01 authority and accepted WP01 result
6. Release 1.2 dataset semantic architecture
7. Release 1.1 persistence architecture
8. current Domain/Application/Infrastructure/Worker source
9. current architecture, pipeline, resilience, observability, and configuration documentation
10. current permanent tests and architecture tests
11. GitHub issue #139, milestone #54, Project #2, and predecessor state

If these authorities materially conflict, STOP before mutation and report the conflict and smallest corrective authority required.

## 3. Starting-State Gates

Before editing:

- verify repository identity;
- verify branch `main`;
- verify local `main` equals `origin/main`;
- report HEAD and ahead/behind;
- verify staged paths = 0;
- classify every existing tracked/untracked path;
- preserve all accepted Release 1.3 governance artifacts;
- verify WP01 #138 is `Closed / Done`;
- verify WP02 #139 is `Open / Backlog`;
- verify WP03 #140 is `Open / Backlog`;
- verify milestone #54 remains open;
- verify WP02 dependency is exactly WP01;
- verify dependency drift = 0;
- verify no WP03+ implementation exists;
- verify no Release 1.4 implementation has started.

Run the accepted canonical baseline before mutation.

Expected baseline unless current accepted repository truth legitimately differs:

- Domain.Tests: 11
- Application.Tests: 60
- Infrastructure.Tests: 87
- Architecture.Tests: 13
- Total: 171
- Build warnings/errors: 0/0
- SQLite schema: version 2
- Canonical verification: PASS
- Gitleaks: PASS

Only after all starting-state and technical baseline gates pass may #139 move `Backlog → In Progress`.

## 4. Semantic Discovery Objective

WP02 must define the minimum coherent semantic model for a **fixed, deterministic, one-shot Research Pipeline** that composes already accepted platform capabilities.

Canonical Release 1.3 flow to evaluate and freeze:

```text
Explicit pipeline request
  → historical observation retrieval
  → deterministic dataset materialization
  → immutable snapshot persistence
  → catalog registration
  → structured pipeline result/evidence
```

The pipeline starts from accepted persisted historical observations.

**Live provider acquisition is outside the Release 1.3 pipeline boundary.**

Do not broaden this into a generic workflow engine.

## 5. Required Vocabulary

Define, clearly and non-overlappingly:

- Research Pipeline
- Pipeline Definition
- Pipeline Request
- Pipeline Stage
- Pipeline Execution
- Pipeline Result
- Pipeline Success
- Pipeline Failure
- Pipeline Stage Outcome
- Pipeline Input
- Pipeline Output
- Pipeline Re-execution
- Equivalent Pipeline Execution
- Pipeline Evidence
- Operational Invocation / Correlation identifier
- Semantic pipeline identity boundary
- Fail-stop execution
- Empty-dataset pipeline result

Do not define WP03's final identity encoding/fingerprint representation here.

## 6. Pipeline Topology

Freeze the Release 1.3 topology.

The pipeline must be:

- fixed;
- sequential;
- deterministic;
- Application-owned;
- explicitly triggered;
- one-shot;
- bounded;
- offline-capable when accepted historical observations exist.

Define the authoritative stage order.

At minimum reconcile these semantic stages:

1. accepted historical observation retrieval;
2. deterministic dataset materialization;
3. immutable snapshot persistence;
4. catalog registration;
5. final structured result/evidence.

Determine whether retrieval is best modeled as an explicit pipeline stage or as the first semantic input-resolution step inside dataset materialization.

Choose one authoritative model and explain why.

Do not permit stage reordering in Release 1.3.

## 7. Acquisition Boundary

Freeze:

**Pipeline-managed live market-data acquisition: OUT OF SCOPE.**

The pipeline consumes accepted historical observations already owned by the Release 1.1 persistence foundation.

Document why:

- deterministic replay;
- isolation from provider/network uncertainty;
- preservation of Release 1.1 acquisition boundary;
- simpler failure semantics;
- clearer reproducibility.

## 8. Relationship to Release 1.2 Dataset Materialization

Preserve and reuse:

- `DatasetDefinition`;
- exact target;
- `[from,to)` selection;
- deterministic ascending semantic-instant ordering;
- successful empty materialization;
- exact timestamp/offset fidelity;
- exact decimal fidelity;
- dataset identity/version/provenance/lineage;
- immutable snapshot persistence;
- catalog registration;
- equivalent-existing behavior;
- integrity conflicts.

Do not introduce competing dataset semantics.

## 9. Pipeline Definition Semantics

Define what semantically determines a Pipeline Definition.

At minimum evaluate:

- fixed stage topology;
- dataset definition;
- semantic execution parameters;
- ordering rules;
- pipeline semantic-model version;
- any configuration that changes deterministic output.

Explicitly exclude non-semantic operational values such as:

- wall-clock invocation time;
- process ID;
- machine identity;
- local filesystem path;
- connection string;
- random value;
- log correlation ID;
- environment-specific operational metadata.

WP03 will decide exact identity representation.

## 10. Pipeline Request Semantics

Define the request boundary independently from physical Worker/configuration concerns.

Prefer reuse of accepted `DatasetDefinition` rather than duplicating target/from/to concepts.

Do not define command-line/configuration key names; WP08/WP09 own those physical host decisions.

## 11. Execution Semantics

Define Pipeline Execution as a bounded attempt to apply one Pipeline Definition/Request against one accepted source state.

Clarify:

- execution is one-shot;
- execution terminates success or failure;
- no background lifecycle continues after terminal result;
- runtime timing does not alter semantic identity;
- repeated equivalent execution is valid;
- equivalent execution preserves accepted immutable dataset evidence;
- relevant source-state change may lead to distinct downstream dataset snapshot/version evidence.

Distinguish semantic execution from an ephemeral operational invocation.

## 12. Re-execution and Equivalence

For equivalent semantic pipeline request/definition + equivalent relevant persisted source state:

- selected dataset semantics remain equivalent;
- dataset identities/version remain equivalent;
- immutable snapshot/catalog evidence remains equivalent;
- pipeline semantic result must be equivalent;
- no new semantic version is created solely because the pipeline ran again;
- no overwrite occurs.

Define which semantic changes make executions distinguishable. Operational timing alone must not.

WP03 owns identity encoding.

## 13. Empty Dataset Semantics

A valid pipeline request may yield a valid empty dataset materialization.

Define:

- this is a successful semantic pipeline result if all stages complete successfully;
- snapshot/catalog evidence may represent zero observations according to Release 1.2;
- empty does not mean failure;
- no sentinel observation is introduced;
- repeated equivalent execution remains equivalent.

## 14. Success Semantics

Define the minimum pipeline success model.

At minimum distinguish whether success carries:

- newly accepted downstream evidence;
- equivalent existing downstream evidence.

Choose whether these are distinct successful outcomes or one success with a disposition concept. WP04 owns contract representation.

Pipeline success means every required stage completed under accepted semantics and final dataset/snapshot/catalog evidence is semantically consistent.

## 15. Failure and Fail-Stop Semantics

When any required stage fails:

- later stages must not execute unless they are already semantically satisfied under an accepted lower-level equivalence boundary;
- the pipeline terminates as failed;
- the failing stage must be attributable;
- earlier accepted immutable evidence remains valid;
- no rollback of independently accepted immutable evidence is implied;
- no repair/overwrite behavior is introduced.

WP06 owns detailed validation/failure vocabulary and mapping.

## 16. Stage Outcome Semantics

Define conceptual stage outcomes sufficiently for WP03/WP04, including:

- completed with newly accepted evidence;
- completed with equivalent existing evidence;
- failed with semantic/integrity failure;
- failed with unavailable dependency/storage;
- failed with invalid evidence/input.

Do not force every stage to expose the same lower-level result type.

## 17. Determinism and Reproducibility

Define deterministic pipeline behavior as depending only on semantic inputs and relevant accepted persisted source state.

Explicitly exclude:

- current time;
- machine/process identity;
- culture;
- local timezone;
- database natural row order;
- provider ordering;
- filesystem path;
- connection string;
- random state;
- mutable logging metadata.

Do not design byte-level canonicalization here; WP03 owns it.

## 18. Pipeline Evidence Boundary

Define what semantic evidence must be knowable after execution.

At minimum evaluate:

- pipeline definition semantics;
- pipeline semantic run identity concept;
- target/dataset definition;
- produced dataset snapshot/version identity;
- stage sequence;
- terminal outcome;
- failing stage when failed;
- bounded failure category;
- provenance relation to source/dataset evidence.

Distinguish this from operational evidence such as wall-clock timestamps, duration, process ID, and log correlation ID.

No durable run-history persistence is authorized.

## 19. Worker Boundary

Define only semantic host expectations:

- one explicit invocation;
- one pipeline execution;
- one terminal result;
- no recurring behavior.

Do not specify exact configuration keys, DI registrations, process code, or logging implementation.

## 20. Architecture Ownership

### Domain
Expected delta: 0.

### Application
Owns pipeline semantics, contracts, fixed orchestration, semantic validation/failure model, and structured semantic evidence contracts.

### Infrastructure
Continues to own historical persistence, dataset snapshot/catalog persistence, and provider/storage mechanics.

### Worker
Owns composition, explicit configuration, one-shot trigger, and process-level result handling.

Production dependency graph remains unchanged.

## 21. Schema Decision

Freeze:

**Release 1.3 schema evolution: NOT REQUIRED.**

Expected:

`PRAGMA user_version = 2`

No durable pipeline-definition, run-history, checkpoint, or scheduler tables.

If semantic discovery finds durable pipeline persistence unavoidable, STOP and report an authority contradiction.

## 22. Explicit Release 1.4+ Deferrals

Reaffirm out of scope:

- live acquisition inside pipeline;
- scheduler/cron;
- recurring/background refresh;
- configurable DAG;
- plugin workflow engine;
- parallel/streaming/distributed execution;
- automatic retries;
- circuit breakers;
- provider fallback;
- durable checkpoints;
- partial-run resume;
- persisted operational run history;
- metrics backend;
- distributed tracing backend;
- enrichment/feature generation;
- model training/evaluation;
- MLOps.

Do not introduce preparatory abstractions solely for these future capabilities.

## 23. Authorized Artifact

Preferred WP02 artifact:

`docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`

Create exactly one semantic architecture artifact unless repository conventions require an equivalent canonical path.

The artifact must cover:

1. Purpose
2. Release boundary
3. Vocabulary
4. Pipeline topology
5. Stage semantics
6. Source/acquisition boundary
7. Dataset relationship
8. Definition/request semantics
9. Execution semantics
10. Re-execution/equivalence
11. Empty dataset semantics
12. Success semantics
13. Fail-stop semantics
14. Stage outcomes
15. Determinism/reproducibility
16. Evidence boundary
17. Architecture ownership
18. Schema decision
19. Release 1.4+ deferrals
20. WP03 handoff

## 24. Prohibited Mutations

Do not modify production code, tests, packages, references, schema/bootstrap, DI/configuration, build/scripts, execution plan, file manifest, or GitHub planning authority.

Do not stage, commit, push, branch, create PR, merge, tag, release, or start WP03.

## 25. Validation

After creating the semantic artifact:

- validate repository-relative links;
- validate trailing whitespace;
- run `git diff --check`;
- run `git diff --cached --check`;
- restore;
- format verification;
- build;
- all permanent tests;
- architecture tests;
- canonical `eng/verify.ps1 -Configuration Release`;
- Gitleaks;
- confirm database residue = 0.

Expected baseline remains 171/171 with 13/13 architecture tests and 0/0 build warnings/errors.

Production/test/package/reference delta must remain zero.

## 26. GitHub Lifecycle

After baseline gates pass:

- move #139 Backlog → In Progress.

After every WP02 acceptance gate passes:

- post concise evidence to #139;
- close #139;
- set Project #2 status to Done.

Verify #140 remains Open/Backlog and milestone #54 remains Open.

## 27. Acceptance Matrix

WP02 completes only if:

- WP01 predecessor: PASS
- pipeline vocabulary: PASS
- fixed topology: PASS
- stage ordering: PASS
- acquisition outside pipeline: PASS
- persisted observations as source boundary: PASS
- Release 1.2 dataset semantics reused: PASS
- one-shot execution semantics: PASS
- re-execution/equivalence semantics: PASS
- empty dataset semantics: PASS
- success semantics: PASS
- fail-stop semantics: PASS
- stage attribution concept: PASS
- determinism/reproducibility: PASS
- semantic vs operational separation: PASS
- evidence boundary: PASS
- architecture ownership: PASS
- schema evolution required: NO
- Release 1.4+ deferrals: PASS
- production delta: 0
- permanent test delta: 0
- package/reference delta: 0/0
- WP03 started: NO
- Release 1.4 implementation started: NO
- canonical validation: PASS

## 28. Required Execution Report

Report at least:

1. Executive Summary
2. Authorities Reviewed
3. Repository Context
4. Initial Git State
5. Working-Tree Classification
6. Predecessor/Lifecycle Gates
7. Issue Lifecycle
8. Initial Baseline
9. Existing Pipeline/Architecture Documentation Reconciliation
10. Release 1.1 Source Foundation Reconciliation
11. Release 1.2 Dataset Foundation Reconciliation
12. Pipeline Vocabulary
13. Pipeline Topology Decision
14. Retrieval/Stage Boundary Decision
15. Acquisition Boundary Decision
16. Dataset Relationship
17. Pipeline Definition Semantics
18. Pipeline Request Semantics
19. Execution Semantics
20. Re-execution/Equivalence Semantics
21. Empty Dataset Semantics
22. Success Semantics
23. Failure/Fail-Stop Semantics
24. Stage Outcome Semantics
25. Determinism/Reproducibility
26. Evidence Boundary
27. Worker Boundary
28. Architecture Ownership
29. Schema Decision
30. Release 1.4+ Deferrals
31. Artifact Added
32. Production Delta
33. Permanent Test Delta
34. Package/Reference Delta
35. Validation Evidence
36. Architecture Validation
37. Security/Offline Evidence
38. Mutation Accounting
39. Git/GitHub Protection
40. Planning Protection
41. Findings/Blockers
42. Acceptance Matrix
43. Final Repository/GitHub State
44. WP03 Handoff
45. Final Decision
46. Next Authorized Work Package

On success end exactly with:

RELEASE 1.3 WP02 COMPLETE

NEXT AUTHORIZED WORK PACKAGE:
WP03 — Pipeline Identity, Provenance & Evidence Semantics
GitHub issue #140

If blocked, end with:

RELEASE 1.3 WP02 BLOCKED

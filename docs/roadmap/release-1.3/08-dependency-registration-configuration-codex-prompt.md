# Release 1.3 WP08 — Dependency Registration & Configuration — Codex Execution Prompt

## 1. Role and objective

Execute **Release 1.3 — WP08: Dependency Registration & Configuration** for:

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`
- GitHub issue: **#145**
- Milestone: **#54 — Phase 3 - Release 1.3: Research Pipeline Foundation**
- Recommended model: **GPT-5.6 Terra**

Your objective is to establish the **minimum composition and configuration boundary** required to make the accepted Release 1.3 pipeline services resolvable for later bounded Worker execution.

WP08 is a composition package. It must register existing Application pipeline capabilities and define/validate only the configuration required to construct an explicit pipeline request.

WP08 must **not execute the pipeline**. Actual one-shot Worker execution belongs to WP09.

---

## 2. Mandatory authority

Before any mutation, read completely and reconcile at minimum:

1. `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
2. `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
4. `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
5. `docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
6. WP01–WP07 authoritative prompt pairs and accepted execution results.
7. Current Release 1.3 Application pipeline files, including:
   - `PipelineIdentity.cs`
   - `PipelineDefinition.cs`
   - `PipelineEvidence.cs`
   - `PipelineExecutionResult.cs`
   - `PipelineExecutionUseCase.cs`
   - `PipelineIdentityComputer.cs`
   - `PipelineValidation.cs`
   - `PipelineExecutionEvidence.cs`
8. Existing dependency-registration/composition code in Application, Infrastructure, and Worker.
9. Existing Release 1.2 dataset registration/configuration and bounded Worker conventions.
10. Current configuration, dependency-injection, logging, observability, testing, and architecture documentation.
11. Current source, tests, project references, package references, SQLite schema implementation, and GitHub planning state.

Repository truth wins over assumptions.

Do not redesign WP02–WP07 semantics to simplify composition.

---

## 3. Starting-state gates

Before implementation, prove and report:

- Release 1.2 remains closed.
- Release 1.3 milestone #54 is open.
- Issues #138–#144 are Closed/Done.
- WP08 issue #145 is Open/Backlog.
- WP09 issue #146 is Open/Backlog and unchanged.
- #145 dependencies exactly match the authoritative Release 1.3 graph.
- No WP09+ implementation has started.
- No Release 1.4 implementation has started.
- Current branch is `main`.
- `HEAD == origin/main`.
- Ahead/behind is `0/0`.
- Staged paths are `0`.
- Existing cumulative Release 1.3 working-tree changes are expected and manifest-authorized.
- No unexpected production/test/governance paths exist.
- SQLite schema remains version `2`.
- No temporary SQLite/WAL/SHM/journal residue exists.
- Canonical Release verification passes before mutation.

If any mandatory starting-state condition fails, stop and report the exact blocker.

Only after the starting-state gate passes may issue #145 move from Backlog to In Progress.

---

## 4. Accepted Release 1.3 baseline

WP08 must preserve all accepted semantics.

### Fixed topology

The pipeline remains exactly:

1. Historical observation retrieval
2. Dataset materialization
3. Immutable snapshot persistence
4. Catalog registration
5. Structured result/evidence

It remains deterministic, sequential, explicit, one-shot, offline-capable, and fail-stop.

### Identity and evidence

Preserve:

- `aiq-pipeline-identity-v1`
- deterministic Pipeline Definition Identity
- deterministic Semantic Pipeline Execution Identity
- Release 1.2 dataset/source/snapshot identities
- disposition-independent semantic identity
- first-failure semantics
- evidence-prefix validity
- established-identities-only rules
- structured `PipelineExecutionEvidence`
- provenance and acyclic lineage
- operational metadata excluded from semantic identity

### Failure semantics

Preserve the accepted bounded categories and propagation rules from WP06.

Do not create a new configuration-driven failure taxonomy for semantic pipeline failures.

Configuration errors may fail composition/startup deterministically, but must not be disguised as semantic execution evidence.

---

## 5. WP08 implementation objective

Implement the minimum dependency registration and configuration needed so that a later WP09 Worker can:

1. obtain explicit pipeline input configuration;
2. construct or obtain the accepted pipeline definition/request using existing Application semantics;
3. resolve `IPipelineExecutionUseCase`;
4. execute it exactly once in WP09.

WP08 itself stops before step 4.

The final service graph must be resolvable without executing the pipeline and without creating unintended database/provider side effects merely because the graph was resolved.

---

## 6. Dependency-registration requirements

Inspect existing registration conventions first.

Register only the minimum accepted Release 1.3 Application pipeline services required by the current implementation.

At minimum reconcile whether registration is required for:

- `IPipelineExecutionUseCase`
- `PipelineExecutionUseCase`
- existing Release 1.2 dependencies consumed by the pipeline:
  - `IMaterializeDatasetUseCase`
  - `IDatasetSnapshotStore`
  - `IDatasetCatalog`
- any stateless identity/evidence helper only if it is actually represented as an injectable dependency by repository truth

Do not convert static/internal deterministic helpers into services merely for stylistic consistency.

Do not duplicate existing Release 1.2 registrations.

Use lifetimes consistent with existing repository conventions and ownership:

- execution/use-case services should not become singleton execution state;
- database connections must not be captured by singleton services;
- immutable configuration/factory ownership may retain existing lifetimes;
- no service may retain mutable per-execution pipeline state across invocations.

Document the lifetime reasoning in the execution report.

---

## 7. Configuration boundary

Derive the minimum configuration from the accepted pipeline request and existing Worker/configuration conventions.

Prefer reusing existing Release 1.2 dataset configuration where semantically identical.

Do not create duplicate keys for values already represented by accepted configuration unless repository truth requires separation.

At minimum reconcile the existing keys introduced for bounded dataset execution:

- `Dataset:Target`
- `Dataset:From`
- `Dataset:To`
- existing persistence configuration such as `Persistence:DatabasePath`

Determine whether Release 1.3 requires any genuinely new `Pipeline:*` semantic configuration.

Default decision: **do not introduce new pipeline semantic configuration if the fixed pipeline definition can be deterministically derived from accepted constants plus the explicit dataset request.**

The fixed topology is code/semantic authority, not user-configurable configuration.

Do not make stage order configurable.

Do not make identity schemes configurable.

Do not make failure policy configurable.

Do not make retries/scheduling configurable.

---

## 8. Configuration parsing and validation

Configuration handling must be:

- deterministic
- invariant-culture
- explicit
- fail-fast
- non-secret
- independent of local timezone
- consistent with existing Release 1.2 conventions

Preserve exact target semantics.

Preserve `[from,to)` boundaries.

Timestamp parsing must preserve the accepted `DateTimeOffset` semantics and should continue using invariant round-trip representation where that is the existing convention.

Reject:

- missing required values;
- malformed timestamps;
- invalid intervals;
- semantically inconsistent request values.

Do not silently normalize, repair, truncate, round, infer, or substitute required semantic inputs.

Configuration failure must occur before pipeline execution.

Where feasible within existing composition conventions, invalid semantic configuration should fail before database/provider activity.

---

## 9. Resolution-side-effect protection

A mandatory WP08 property is:

**Resolving the Release 1.3 service graph must not itself execute the pipeline.**

Also prove, to the extent supported by existing Release 1.2 ownership:

- no provider call occurs during resolution;
- no market-data acquisition occurs during resolution;
- no pipeline stage executes during resolution;
- no snapshot/catalog mutation occurs during resolution;
- no durable pipeline run evidence is written;
- no database should be created merely because pipeline services are resolved, if existing connection-factory semantics allow that guarantee.

Do not eagerly open SQLite connections in DI registration.

Do not instantiate operation-owned connections as singleton state.

---

## 10. Application / Infrastructure / Worker boundary

Expected ownership:

### Application

May contain/refine dependency registration for Application-owned pipeline use cases if this matches existing repository conventions.

### Infrastructure

May register existing Infrastructure implementations required by the accepted pipeline seams.

Do not add new pipeline persistence.

### Worker

WP08 may add/refine **configuration/composition support only if explicitly authorized by the file manifest**.

WP08 must not invoke `IPipelineExecutionUseCase.Execute(...)` or equivalent runtime execution.

If the manifest reserves all Worker changes for WP09, do not touch Worker in WP08.

### Domain

Expected delta: `0`.

---

## 11. Schema and persistence protection

SQLite must remain **schema version 2**.

WP08 must not add:

- tables
- columns
- indexes
- migrations
- run-history persistence
- pipeline evidence persistence
- new snapshot/catalog persistence
- new SQL
- filesystem persistence for pipeline runs

Reuse the accepted Release 1.1/1.2 persistence boundaries.

---

## 12. Provider/network boundary

The Release 1.3 pipeline starts from persisted Release 1.1 historical observations.

WP08 must not introduce:

- live acquisition
- provider orchestration
- HTTP execution
- Twelve Data calls
- provider retries
- provider fallback
- provider-specific pipeline configuration

Existing provider composition required by legacy Worker startup may remain intact, but WP08 must not expand it.

No real API key may be required for a DI-resolution proof if a safe offline path exists.

---

## 13. Explicit out of scope

WP08 MUST NOT implement:

- WP09 one-shot Worker pipeline execution
- WP10 permanent Application pipeline tests
- WP11 composition/Worker validation package
- WP12 architecture evolution
- WP13 documentation alignment
- WP14 integration/acceptance
- pipeline execution loops
- scheduling
- timers
- recurring refresh
- retries
- circuit breakers
- fallback providers
- configurable DAGs
- plugins
- parallel stages
- streaming
- distributed execution
- checkpoints
- resume/recovery
- durable operational run history
- metrics backends
- dashboards
- distributed tracing backends
- feature engineering
- model training/evaluation
- MLOps
- Release 1.4 work

Do not opportunistically refactor unrelated code.

---

## 14. Expected file boundary

Use `RELEASE_1.3_FILE_MANIFEST.md` as the exact mutation authority.

Likely relevant existing composition files may include:

- `src/AIQuantTradingResearch.Application/DependencyInjection.cs`
- `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs`

Worker configuration/composition files may only be modified if WP08 is explicitly authorized for them by the manifest.

Do not create additional files when a narrow modification to an authorized existing composition file is sufficient.

If a required change is not manifest-authorized, stop as BLOCKED and identify the smallest manifest correction required.

---

## 15. Validation strategy

WP08 should not add permanent tests unless explicitly authorized by the manifest.

Temporary offline probes are allowed when needed and must be removed before completion.

At minimum validate:

1. Application registration resolves `IPipelineExecutionUseCase`.
2. Required Release 1.2 dependencies resolve through the accepted graph.
3. Lifetimes are compatible and do not capture operation state improperly.
4. Resolution does not execute the pipeline.
5. Resolution does not call a provider.
6. Resolution does not persist snapshot/catalog evidence.
7. Resolution does not create pipeline run history.
8. Resolution does not create a database merely from graph resolution where existing ownership permits this.
9. Valid configuration can be parsed into the exact accepted semantic inputs needed for WP09.
10. Missing target fails deterministically.
11. Missing boundaries fail deterministically.
12. Malformed timestamp fails deterministically.
13. Invalid `[from,to)` interval fails deterministically.
14. Parsing is invariant-culture/local-timezone independent.
15. No topology/identity/failure-policy configuration is introduced.
16. SQLite remains schema v2.
17. Existing Release 1.2 DI/composition remains functional.

Do not execute the actual pipeline as the WP08 acceptance proof. That belongs to WP09.

---

## 16. Mandatory technical validation

Before completion run the repository canonical validation, including:

- restore
- format verification
- Release build
- Domain.Tests
- Application.Tests
- Infrastructure.Tests
- Architecture.Tests
- `eng/verify.ps1 -Configuration Release`
- Gitleaks
- `git diff --check`
- `git diff --cached --check`
- direct whitespace checks for authorized untracked WP08 paths where needed
- database-residue scan

Expected permanent baseline unless repository truth legitimately changed before WP08:

- Domain.Tests: `11`
- Application.Tests: `60`
- Infrastructure.Tests: `87`
- Architecture.Tests: `13`
- Permanent total: `171`
- Skipped: `0`

Expected WP08 permanent test delta: `0`.

Build warnings/errors: `0/0`.

---

## 17. Architecture protection

Prove the production graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

No cycles.

Prove no Domain/Application leakage of:

- SQLite
- SQL
- filesystem
- provider/HTTP
- Worker
- Infrastructure implementation types

No package/reference changes are expected.

---

## 18. Security and cleanup

Do not expose or log:

- provider credentials
- connection strings
- sensitive local paths
- secrets

Do not make provider/network calls.

Gitleaks must pass.

Remove all temporary probes and temporary SQLite/WAL/SHM/journal artifacts before completion.

---

## 19. Git and GitHub mutation policy

Allowed GitHub mutations for WP08 only:

1. after all starting-state gates pass, move #145 to In Progress;
2. after all WP08 acceptance gates pass, post bounded completion evidence;
3. close #145;
4. set Project #2 Status to Done.

WP09 issue #146 and later issues are read-only.

Do not modify legacy milestone #44.

Do not stage, commit, push, create branches, create PRs, merge, tag, or create releases.

Do not rewrite history.

---

## 20. Stop conditions

Stop and report **BLOCKED** if:

- starting-state governance is invalid;
- manifest authority conflicts with a required mutation;
- composition requires redesign of accepted pipeline semantics;
- pipeline execution is necessary to prove WP08;
- schema evolution is required;
- new provider/network behavior is required;
- WP09 Worker execution must be started;
- permanent tests must be changed contrary to authority;
- a new package/project reference is required without authority;
- canonical validation fails for a reason outside WP08 scope;
- an unexpected repository/GitHub mutation is discovered.

Report the smallest corrective authority required. Do not broaden WP08 yourself.

---

## 21. Required execution report

Return a detailed numbered report covering at least:

1. Executive summary
2. Authorities reviewed
3. Repository/Git baseline
4. Working-tree classification
5. Predecessor/lifecycle gates
6. Initial canonical baseline
7. WP02–WP07 reconciliation
8. Existing composition inventory
9. Application registration design
10. Infrastructure registration reconciliation
11. Pipeline service registrations
12. Service lifetime decisions
13. Existing Release 1.2 registration reuse
14. Configuration inventory
15. Configuration-key decision
16. Dataset input reuse
17. Pipeline-specific configuration decision
18. Parsing/validation rules
19. Missing/invalid configuration behavior
20. Culture/timezone determinism
21. Resolution-side-effect proof
22. Database-creation-on-resolution proof
23. Provider/network isolation
24. Pipeline-not-executed proof
25. Snapshot/catalog non-mutation proof
26. Schema-v2 preservation
27. Files added/modified
28. Layer deltas
29. Package/reference/schema delta
30. Permanent test delta
31. Temporary probe evidence
32. WP09 protection
33. Release 1.4 protection
34. Security/offline evidence
35. Whitespace/diff evidence
36. Restore/build evidence
37. Permanent test evidence
38. Canonical verification
39. Architecture validation
40. Release 1.1/1.2 regression
41. WP05–WP07 regression
42. Composition/configuration acceptance matrix
43. Mutation accounting
44. Git/GitHub protection
45. Planning protection
46. Findings/blockers
47. Final repository/GitHub state
48. WP09 handoff
49. Final decision
50. Next authorized work package

End with exactly one terminal marker:

`RELEASE 1.3 WP08 COMPLETE`

or

`RELEASE 1.3 WP08 BLOCKED`

If complete, also state:

`NEXT AUTHORIZED WORK PACKAGE: WP09 — One-Shot Worker Pipeline Execution — GitHub issue #146`

Do not start WP09.

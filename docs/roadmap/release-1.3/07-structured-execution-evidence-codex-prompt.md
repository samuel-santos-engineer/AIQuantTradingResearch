# Release 1.3 WP07 — Structured Execution Evidence — Codex Execution Prompt

## 1. Role and objective

You are executing **Release 1.3 — WP07: Structured Execution Evidence** for:

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`
- GitHub issue: **#144**
- Release milestone: **#54 — Phase 3 - Release 1.3: Research Pipeline Foundation**
- Expected model tier: GPT-5.6 Terra

Your objective is to implement the **minimum Application-owned structured execution-evidence capability** required by the accepted Release 1.3 semantic model.

WP07 must make completed pipeline execution evidence suitable for bounded structured consumption while preserving the deterministic semantic evidence already established by WP03–WP06.

This work package is **not** an observability platform, durable run-history subsystem, Worker execution package, DI/configuration package, logging redesign, metrics implementation, tracing implementation, or persistence feature.

---

## 2. Mandatory authority

Before making any mutation, read completely and reconcile at minimum:

1. `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
2. `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
4. `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
5. `docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
6. WP01–WP06 authoritative prompt pairs and accepted execution results available in repository/governance state.
7. Current Application pipeline implementation, especially:
   - `PipelineIdentity.cs`
   - `PipelineDefinition.cs`
   - `PipelineEvidence.cs`
   - `PipelineExecutionResult.cs`
   - `PipelineExecutionUseCase.cs`
   - `PipelineIdentityComputer.cs`
   - `PipelineValidation.cs`
8. Release 1.2 dataset contracts and integration semantics used by the pipeline.
9. Current architecture, observability, logging, configuration, and testing documentation relevant to the boundary.
10. Current source, tests, project references, solution structure, and GitHub planning state.

Repository truth wins over assumptions. Do not redesign accepted semantics merely because another representation appears preferable.

---

## 3. Starting-state gates

Before implementation, prove and report:

- Release 1.2 remains closed.
- Release 1.3 milestone #54 is open.
- WP01–WP06 issues #138–#143 are Closed/Done.
- WP07 issue #144 is Open/Backlog.
- WP08 issue #145 remains Open/Backlog.
- Dependencies for #144 match the authoritative execution graph.
- No WP08+ implementation has started.
- No Release 1.4 implementation has started.
- Current branch is `main`.
- `HEAD == origin/main`.
- Ahead/behind is `0/0`.
- Staged paths are `0`.
- Existing cumulative Release 1.3 working-tree paths are classified against the manifest.
- No unexpected generated SQLite/WAL/SHM/journal residue exists.
- Canonical Release verification passes before mutation.

If any mandatory starting-state condition fails, stop and report the exact blocker. Do not mutate #144 lifecycle before the starting-state gate passes.

After all starting gates pass, move #144 from Backlog to In Progress.

---

## 4. Accepted semantic baseline

WP07 must preserve all accepted semantics.

### WP02 — Research Pipeline semantics

The pipeline is fixed, deterministic, sequential, explicit, one-shot, offline-capable, and fail-stop.

The topology remains exactly:

1. Historical observation retrieval
2. Dataset materialization
3. Immutable snapshot persistence
4. Catalog registration
5. Structured result/evidence

No stage may be added, removed, reordered, retried, parallelized, or made configurable.

### WP03 — Identity, provenance, and evidence

Preserve:

- `aiq-pipeline-identity-v1`
- Pipeline Definition Identity
- Semantic Pipeline Execution Identity
- Dataset Definition Identity
- Dataset Snapshot Identity / Dataset Version
- Relevant Source State Identity
- separation from Operational Invocation identity/correlation
- deterministic canonical identity representation
- SHA-256 / 64 lowercase hexadecimal fingerprints
- acyclic lineage
- equivalent semantic reruns producing equivalent execution identity
- dispositions not changing semantic identity
- evidence only for facts established by execution
- first-failure stage attribution
- operational timestamps/correlation/runtime facts excluded from semantic identity
- no mutable Pipeline Version
- no durable operational run history

### WP04–WP06

Reuse the existing Application pipeline contracts and orchestration.

WP06 is authoritative for:

- canonical request/definition validation
- valid evidence prefixes
- first-failure termination
- `InvalidEvidence`
- `DependencyUnavailable`
- `IntegrityConflict`
- unknown exception propagation
- established-evidence-only semantics

Do not introduce a competing failure model.

---

## 5. WP07 objective

Implement the minimum structured execution-evidence surface that allows a completed `PipelineExecutionResult` to be represented as a stable, bounded, machine-consumable Application value suitable for later one-shot Worker presentation.

The structured evidence must communicate the accepted semantic execution result without introducing operational persistence or changing identity semantics.

At minimum, determine and implement the smallest representation required to expose:

- pipeline definition identity
- semantic pipeline execution identity when established
- fixed pipeline topology/version semantics already represented by accepted contracts
- terminal success/failure state
- success disposition when successful
- first failing stage when failed
- bounded failure classification when failed
- ordered stage evidence
- established dataset definition identity
- established source-state identity
- established snapshot identity/version
- relevant provenance/lineage references already carried by accepted evidence
- successful empty-dataset evidence without special sentinel semantics

Prefer composition/reuse of existing immutable contracts over duplicating their data.

---

## 6. Semantic versus operational evidence

Maintain an explicit boundary between **semantic execution evidence** and **operational invocation information**.

Semantic evidence may contain only deterministic facts justified by the accepted pipeline result.

Operational information such as the following must not become semantic identity inputs:

- wall-clock start/end timestamps
- elapsed duration
- process or machine identity
- thread identity
- filesystem/database paths
- connection strings
- environment-specific values
- random IDs
- provider/network metadata
- mutable status
- “latest” pointers
- logging backend identifiers

If a non-secret correlation identifier is necessary for future presentation, it must be clearly operational, optional/non-semantic, and must not alter pipeline definition or semantic execution identity. Do not add it unless repository truth demonstrates it is required by WP07.

---

## 7. Structured representation constraints

The representation must be:

- Application-owned
- immutable
- deterministic for equivalent semantic results
- provider independent
- storage independent
- SQLite/SQL/filesystem independent
- Worker independent
- DI/configuration independent
- culture independent
- local-timezone independent
- explicit about stage order
- explicit about terminal outcome
- incapable of inventing evidence for stages that did not execute

Do not introduce JSON serialization policy unless the accepted execution plan/manifest explicitly assigns it to WP07 and it is necessary to satisfy the work package. A structured Application model is sufficient unless authority requires a serialized wire/text format.

Do not introduce database persistence for pipeline evidence.

---

## 8. Success evidence requirements

For a successful pipeline result, structured evidence must preserve enough accepted information to distinguish and explain:

- semantic pipeline execution identity
- pipeline definition identity
- complete ordered five-stage evidence
- resulting dataset identities
- source-state identity
- snapshot identity/version
- success disposition:
  - `NewlyAccepted`, or
  - `EquivalentExisting`
- valid empty dataset output

`NewlyAccepted` versus `EquivalentExisting` must remain a disposition difference only and must not change semantic execution identity for equivalent semantic execution.

---

## 9. Failure evidence requirements

For failure:

- evidence must form the valid prefix established by WP06;
- the first failing stage must be explicit;
- later stages must have no evidence;
- failure classification must reuse the accepted bounded vocabulary;
- semantic execution identity may only be present when the accepted WP03/WP06 rules say it is established;
- dataset/source/snapshot identities may only appear after the stage that establishes them;
- no fabricated placeholder/sentinel identity is permitted;
- unknown unrelated exceptions must continue to propagate rather than being converted into structured success/failure evidence.

Do not turn operational exceptions into a new generalized error envelope.

---

## 10. Provenance and lineage

Reuse the existing provenance and lineage concepts.

The structured evidence must not create a second provenance graph.

Where evidence references provenance/lineage, preserve the accepted acyclic relationship among:

- pipeline definition
- semantic execution
- dataset definition
- relevant source state
- dataset snapshot/version

Do not introduce durable run IDs, mutable run records, parent/child DAG execution, retry attempts, checkpoint lineage, or scheduling lineage.

---

## 11. Expected implementation boundary

Prefer the smallest change set under:

`src/AIQuantTradingResearch.Application/Pipelines/`

Use names consistent with current repository conventions and the Release 1.3 manifest.

Before creating a new type, inspect whether `PipelineEvidence` / `PipelineExecutionResult` can already express part of the requirement and whether a narrow structured descriptor/projector is enough.

Do not modify Domain unless the manifest explicitly authorizes it and repository truth proves it unavoidable.

Expected layer deltas:

- Domain: `0`
- Application: bounded WP07 evidence implementation only
- Infrastructure: `0`
- Worker: `0`

No package or project-reference changes are expected.

SQLite must remain schema version `2`.

---

## 12. Explicit out of scope

WP07 MUST NOT implement:

- WP08 dependency registration
- WP08 configuration
- WP09 Worker pipeline execution
- permanent WP10 tests
- WP11 composition/Worker validation
- WP12 architecture evolution
- WP13 documentation alignment beyond any manifest-authorized WP07 artifact
- WP14 integration/acceptance
- live provider acquisition
- HTTP/provider calls
- scheduling
- background execution
- refresh loops
- retries
- circuit breakers
- fallback providers
- configurable DAGs
- plugins
- parallel execution
- streaming execution
- distributed execution
- checkpoints
- resume/recovery orchestration
- durable pipeline run history
- new SQLite tables/schema
- metrics backends
- dashboards
- distributed tracing backends
- feature engineering
- model training/evaluation
- MLOps
- Release 1.4 implementation

Do not opportunistically refactor unrelated Release 1.1/1.2 code.

---

## 13. Validation strategy

WP07 has no authority to add permanent WP10 test coverage unless the manifest explicitly says otherwise.

You may create temporary, offline, deterministic probes when needed to prove the structured evidence model. Remove all probes and generated data before completion.

At minimum validate applicable cases for:

1. Newly accepted successful non-empty execution.
2. Equivalent-existing successful rerun.
3. Equivalent semantic rerun retains equivalent execution identity.
4. Successful empty dataset.
5. Historical-retrieval failure.
6. Materialization invalid evidence.
7. Snapshot unavailable.
8. Snapshot integrity conflict.
9. Catalog unavailable.
10. Catalog integrity conflict.
11. Correct first-failure stage.
12. No evidence after first failure.
13. Identities only when established.
14. Ordered stage evidence.
15. Dataset/source/snapshot fidelity.
16. No operational metadata affects semantic evidence.
17. Culture/timezone independence where applicable.
18. Unknown exception propagation remains unchanged.

Temporary probes must not call live providers.

---

## 14. Mandatory technical validation

Before completion run the repository's canonical validation, including:

- restore
- format verification
- build in Release configuration
- Domain.Tests
- Application.Tests
- Infrastructure.Tests
- Architecture.Tests
- `eng/verify.ps1 -Configuration Release`
- Gitleaks/secret scanning
- `git diff --check`
- `git diff --cached --check`
- direct whitespace checks for authorized untracked WP07 files where necessary
- database-residue scan

Expected permanent baseline unless repository truth has legitimately changed before WP07:

- Domain.Tests: `11`
- Application.Tests: `60`
- Infrastructure.Tests: `87`
- Architecture.Tests: `13`
- Total: `171`

WP07 permanent test delta should be `0` unless the authoritative manifest explicitly requires otherwise.

Build warnings/errors must be `0/0`.

---

## 15. Architecture protection

Prove the production dependency graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

No cycles.

Prove Domain/Application do not gain SQLite, SQL, filesystem, HTTP/provider, Worker, or Infrastructure leakage.

No new project references or packages without explicit authority.

---

## 16. Security and offline protection

Do not use or expose real provider credentials.

Do not make market-data/provider network calls.

Do not log secrets, connection strings, or local sensitive paths.

Gitleaks must pass.

Any temporary SQLite files must be disposable and removed with WAL/SHM/journal residue before completion.

---

## 17. Git and GitHub mutation policy

Allowed GitHub lifecycle mutation for this work package only:

1. after starting-state gates pass, move #144 to In Progress;
2. after all acceptance gates pass, post bounded completion evidence;
3. close #144;
4. set its Project #2 Status to Done.

Do not modify #145 or later issues except read-only verification.

Do not modify milestone #54 except through normal issue count resulting from #144 closure.

Do not mutate legacy milestone #44.

Do not stage, commit, push, create branches, create PRs, merge, tag, or create releases.

Do not rewrite history.

---

## 18. Stop conditions

Stop immediately and report BLOCKED if:

- authority conflicts materially;
- manifest does not authorize a required file;
- predecessor/lifecycle state is invalid;
- WP07 requires schema evolution;
- WP07 would require Infrastructure or Worker implementation;
- accepted WP03/WP06 evidence semantics cannot represent the required result without redesign;
- permanent tests must be changed contrary to authority;
- canonical validation fails for a reason that cannot be corrected within WP07 scope;
- an unexpected repository/GitHub mutation is discovered.

When blocked, identify the smallest corrective authority required. Do not broaden scope yourself.

---

## 19. Required execution report

Return a detailed numbered execution report covering at least:

1. Executive summary
2. Authorities reviewed
3. Repository/Git baseline
4. Working-tree classification
5. Predecessor/lifecycle gates
6. Initial validation baseline
7. WP02 reconciliation
8. WP03 reconciliation
9. WP04 reconciliation
10. WP05 reconciliation
11. WP06 reconciliation
12. Existing evidence-surface inventory
13. Structured-evidence design decision
14. Semantic/operational evidence boundary
15. Success evidence
16. Equivalent-rerun evidence
17. Empty-result evidence
18. Failure evidence
19. First-failure/prefix preservation
20. Identity establishment rules
21. Stage ordering
22. Provenance/lineage reuse
23. Operational metadata exclusions
24. Files added/modified
25. Layer deltas
26. Package/reference/schema delta
27. Permanent test delta
28. Temporary probe evidence
29. WP08/WP09 protection
30. Release 1.4 protection
31. Security/offline evidence
32. Whitespace/diff evidence
33. Restore/build evidence
34. Permanent test evidence
35. Canonical verification
36. Architecture validation
37. Release 1.1/1.2 regression
38. WP05/WP06 regression
39. Structured-evidence acceptance matrix
40. Mutation accounting
41. Git/GitHub protection
42. Planning protection
43. Findings/blockers
44. Final repository/GitHub state
45. WP08 handoff
46. Final decision
47. Next authorized work package

End with exactly one terminal marker:

`RELEASE 1.3 WP07 COMPLETE`

or

`RELEASE 1.3 WP07 BLOCKED`

If complete, also state:

`NEXT AUTHORIZED WORK PACKAGE: WP08 — Dependency Registration & Configuration — GitHub issue #145`

Do not start WP08.

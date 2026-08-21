# Release 1.3 WP05 — Fixed Pipeline Orchestration — Codex Execution Authority

## Role

You are executing **Release 1.3 WP05 — Fixed Pipeline Orchestration** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue: **#142**

Execute this work package only. Treat the repository, accepted Release 1.2 implementation, Release 1.3 authorities, and current GitHub planning state as authoritative evidence. Do not broaden scope.

## Required authorities

Before mutation, read completely and reconcile:

- `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
- `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
- `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
- `docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
- WP01–WP04 Release 1.3 execution authorities and accepted results
- Release 1.2 dataset definition, identity/version/provenance, materialization, catalog, persistence, integration, validation, DI, Worker, and test evidence
- Current Application pipeline contracts under `AIQuantTradingResearch.Application.Pipelines`
- Current Application dataset contracts/use cases under `AIQuantTradingResearch.Application.Datasets`
- Current source, tests, architecture rules, engineering scripts, and GitHub issue/project state

If any authority materially conflicts with this prompt or repository truth, stop without mutation and report the conflict.

## Starting-state gates

Verify before implementation:

1. Release 1.2 remains closed and intact.
2. Release 1.3 milestone #54 is OPEN.
3. WP01–WP04 issues #138–#141 are Closed/Done.
4. WP05 issue #142 is Open/Backlog.
5. WP06 issue #143 remains Open/Backlog.
6. Issue #142 dependencies match the authoritative execution plan.
7. Project #2 fields for #142 remain correct.
8. Legacy milestone #44 remains empty/unchanged.
9. Local branch is `main`, synchronized with `origin/main`, with no staged paths.
10. Existing untracked Release 1.3 governance/accepted artifacts are classified before mutation.
11. Canonical Release verification passes before mutation.
12. SQLite schema remains version 2.
13. No WP06+, Release 1.4, scheduler, retry, DAG, streaming, or durable run-history implementation has started.

Only after all starting gates pass may #142 move Backlog → In Progress.

## Objective

Implement the minimum **Application-owned fixed pipeline orchestration** that executes the accepted Release 1.3 five-stage topology using existing Release 1.2 capabilities and the WP04 pipeline contracts.

The orchestration must be deterministic, sequential, one-shot, offline-capable, and fail-stop.

WP05 owns orchestration mechanics only. It does **not** own the generalized validation/failure design reserved for WP06, the finalized structured execution-evidence behavior reserved for WP07, DI/configuration reserved for WP08, Worker execution reserved for WP09, or permanent tests reserved for WP10/WP11.

## Authoritative fixed topology

Preserve exactly this semantic stage order:

1. Historical observation retrieval
2. Dataset materialization
3. Immutable snapshot persistence
4. Catalog registration
5. Structured result/evidence

Do not add, remove, reorder, configure, parallelize, retry, loop, or dynamically discover stages.

Where an existing Release 1.2 use case already encapsulates part of this sequence, reuse it deliberately rather than duplicating persistence/catalog semantics. The resulting Release 1.3 pipeline boundary must still expose and preserve the accepted five-stage semantic model.

## Required implementation behavior

Create the smallest Application implementation necessary to:

- accept an explicit WP04 `PipelineRequest`;
- use the fixed WP04 `PipelineDefinition`;
- reuse existing Release 1.2 dataset contracts and use cases;
- execute the fixed pipeline once;
- preserve exact dataset target and `[from,to)` semantics;
- preserve deterministic dataset identity/version/provenance behavior;
- preserve `NewlyAccepted` and `EquivalentExisting` as dispositions, not identity-bearing differences;
- stop immediately at the first failed stage;
- never execute a later stage after failure;
- return a terminal WP04 pipeline execution result;
- carry only evidence already established by executed stages;
- preserve successful empty dataset behavior;
- preserve equivalent re-execution semantics;
- avoid any provider/network acquisition behavior.

The orchestration must not use wall-clock time, random values, machine/process identity, local paths, culture-sensitive formatting, mutable “latest” state, or operational correlation data as semantic inputs.

## Identity boundary

WP03 froze `aiq-pipeline-identity-v1`; WP04 introduced typed pipeline identities but intentionally did not compute them.

WP05 may implement only the deterministic identity computation strictly necessary for orchestration if the execution plan/manifest assigns that representation to WP05. If implemented:

- follow WP03 canonical semantics exactly;
- use explicit type/domain separation;
- use deterministic length-delimited UTF-8 representation;
- use SHA-256;
- emit exactly 64 lowercase hexadecimal characters;
- keep Pipeline Definition Identity distinct from Semantic Pipeline Execution Identity;
- preserve an acyclic derivation graph;
- exclude operational invocation data;
- ensure equivalent semantic reruns produce equivalent semantic execution identity.

Do not redesign the scheme or introduce a Pipeline Version concept.

If the manifest does not authorize an identity-computation artifact in WP05, stop and report the authority gap rather than creating an ungoverned file.

## Evidence boundary

WP05 must produce only the minimum evidence required to satisfy the existing WP04 terminal result contracts and prove orchestration correctness.

Do not turn WP05 into WP07.

Specifically, do not introduce:

- logging frameworks;
- durable execution history;
- metrics;
- tracing backends;
- timestamps as semantic evidence;
- new public evidence taxonomies beyond WP04;
- broad evidence formatting/presentation policy.

If WP04 contracts require stage evidence for a terminal result, populate that evidence minimally from facts established during execution. WP07 remains responsible for hardening structured execution evidence as assigned by the plan.

## Failure boundary

Preserve existing Release 1.2 failures and WP04 bounded pipeline failure categories.

WP05 must:

- fail-stop at the first failure;
- attribute the terminal failure to the first failing semantic stage where the existing contracts permit it;
- preserve integrity conflicts as non-destructive failures;
- preserve unavailable/invalid-data distinctions where already exposed;
- propagate unknown failures according to existing boundaries rather than swallowing them.

Do not implement the generalized pipeline validation/failure-mapping policy reserved for WP06. Do not add retries, fallback, recovery, checkpointing, compensation, repair, or exception swallowing.

## Architecture constraints

Expected production ownership:

- Domain: no change.
- Application: WP05 implementation only.
- Infrastructure: no change.
- Worker: no change.

Preserve:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Application must not reference SQLite, SQL, filesystem persistence, HTTP/provider mechanics, Worker types, or Infrastructure types.

No package or project-reference changes are authorized unless the manifest explicitly says otherwise. Default expected delta: `0/0`.

SQLite schema must remain version `2`.

## File/scope discipline

Use the Release 1.3 file manifest as the exact file authority.

Do not modify WP04 contracts merely for convenience unless the manifest explicitly authorizes a WP05 refinement and it is semantically necessary. If a contract defect prevents implementation, stop and report the smallest corrective authority required.

Do not create permanent tests in WP05. Temporary offline probes are allowed only when necessary for proof; remove all probe source, binaries, databases, WAL/SHM/journal files, and other residue before completion.

Do not modify documentation in WP05 unless explicitly authorized by the manifest.

## Required validation scenarios

Using compilation, existing permanent tests, and narrowly scoped temporary offline probes if needed, prove at minimum:

1. A valid request executes the fixed topology once.
2. A successful non-empty flow reaches terminal success.
3. A successful empty materialization reaches terminal success.
4. Equivalent rerun preserves equivalent semantic pipeline identity/evidence and disposition semantics.
5. A relevant source-state change can produce distinguishable dataset/execution semantic evidence.
6. Failure at the earliest materialization/source boundary prevents persistence/catalog completion.
7. Snapshot-store conflict/unavailability prevents catalog registration.
8. Catalog conflict/unavailability produces terminal failure without overwrite.
9. No stage after the first failed stage executes.
10. Dataset target, boundaries, snapshot identity/version, source-state identity, provenance, lineage, timestamp offset, and decimal fidelity remain unchanged.
11. No provider/network call occurs.
12. No schema mutation occurs.
13. No durable pipeline-run history is created.
14. No scheduling, retry, DAG, streaming, parallel, or recurring behavior exists.

Do not add permanent WP10 tests to satisfy this matrix.

## Canonical validation

Before completion run the repository-standard validation, including:

- restore;
- format verification;
- build;
- Domain tests;
- Application tests;
- Infrastructure tests;
- Architecture tests;
- `eng/verify.ps1 -Configuration Release`;
- Gitleaks through the canonical verification;
- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace checks for authorized untracked WP05 files when normal Git diff checks cannot see them;
- scan for temporary SQLite/WAL/SHM/journal residue.

Expected pre-WP05 permanent baseline:

- Domain.Tests: 11
- Application.Tests: 60
- Infrastructure.Tests: 87
- Architecture.Tests: 13
- Total: 171

Permanent test count must remain unchanged in WP05.

## GitHub lifecycle

After starting gates and baseline pass:

1. Move issue #142 Backlog → In Progress.
2. Implement and validate WP05.
3. Post concise completion evidence to #142.
4. Close #142.
5. Set Project #2 status to Done.
6. Verify #143 remains Open/Backlog and unchanged.
7. Keep milestone #54 open.

Do not mutate other issue fields, dependencies, milestone metadata, Project schema/options, or later work packages.

## Git protection

Do not stage, commit, push, create branches, open PRs, merge, tag, release, rebase, reset, rewrite history, or perform unrelated Git transport.

## Stop conditions

Stop immediately without claiming completion if:

- starting-state governance is inconsistent;
- baseline verification fails for a reason not attributable to an authorized WP05 change;
- WP04 contracts cannot support the fixed orchestration without unauthorized redesign;
- the manifest does not authorize a required file;
- identity computation would require contradicting WP03;
- schema evolution appears necessary;
- permanent tests would need modification;
- Infrastructure/Worker mutation appears necessary;
- provider/network access appears necessary;
- a Release 1.4 capability becomes necessary;
- canonical final validation fails.

Report the exact blocker and smallest corrective authority required.

## Required execution report

Return a numbered report covering at least:

1. Executive summary
2. Authorities reviewed
3. Repository/Git baseline
4. Working-tree classification
5. Predecessor/lifecycle gates
6. Initial canonical baseline
7. WP02 semantic reconciliation
8. WP03 identity/provenance/evidence reconciliation
9. WP04 contract reconciliation
10. Release 1.2 capability reuse
11. Fixed topology implementation
12. Pipeline orchestration boundary
13. Identity-computation decision and evidence
14. Success flow
15. Empty flow
16. Equivalent rerun
17. Changed-source-state behavior
18. Fail-stop behavior
19. Materialization/source failure behavior
20. Snapshot failure/conflict behavior
21. Catalog failure/conflict behavior
22. Stage-evidence behavior
23. Dataset fidelity
24. Operational-input exclusions
25. Exact files added/modified
26. Layer deltas
27. Package/reference/schema delta
28. Permanent test delta
29. Temporary probe evidence and cleanup
30. WP06/WP07 protection
31. WP08/WP09 protection
32. Release 1.4 protection
33. Security/offline evidence
34. Whitespace/diff evidence
35. Restore/build evidence
36. Permanent test evidence
37. Canonical verification
38. Architecture validation
39. Release 1.1/1.2 regression
40. Orchestration acceptance matrix
41. Mutation accounting
42. Git/GitHub protection
43. Planning protection
44. Findings/blockers
45. Final repository/GitHub state
46. WP06 handoff
47. Final decision
48. Next authorized work package

End successful execution with exactly:

`RELEASE 1.3 WP05 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP06 — Pipeline Validation & Failure Semantics — GitHub issue #143`

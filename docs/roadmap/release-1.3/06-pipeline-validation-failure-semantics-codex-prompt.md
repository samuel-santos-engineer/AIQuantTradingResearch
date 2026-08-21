# Release 1.3 WP06 — Pipeline Validation & Failure Semantics — Codex Execution Authority

## Role

You are executing **Release 1.3 WP06 — Pipeline Validation & Failure Semantics** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue: **#143**

Execute WP06 only. Preserve all accepted Release 1.1, Release 1.2, and Release 1.3 WP01–WP05 behavior unless this authority explicitly requires a narrow WP06 refinement.

## Required authorities

Before mutation, read completely and reconcile:

- `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
- `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
- `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
- `docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
- Release 1.3 WP01–WP05 authority pairs and accepted execution results
- Current Application pipeline contracts and WP05 implementation
- Release 1.2 dataset contracts, materialization, persistence, catalog, integration, validation/failure mapping, DI, Worker, and tests
- Current architecture rules, source, tests, engineering scripts, and GitHub planning state

Repository truth wins over assumptions. If a material conflict exists between authorities or the file manifest does not authorize a required change, stop without mutation and report the smallest corrective authority required.

## Starting-state gates

Verify before implementation:

1. Release 1.2 remains closed and intact.
2. Release 1.3 milestone #54 is OPEN.
3. Issues #138–#142 are Closed/Done.
4. WP06 issue #143 is Open/Backlog.
5. WP07 issue #144 is Open/Backlog and unchanged.
6. #143 dependencies match the authoritative graph: WP03 and WP05.
7. Project #2 membership/fields for #143 remain correct.
8. Legacy milestone #44 remains OPEN / EMPTY / UNCHANGED.
9. Local branch is `main`, synchronized with `origin/main`.
10. Staged paths are zero.
11. Existing cumulative Release 1.3 governance and implementation paths are fully classified.
12. Canonical Release verification passes before mutation.
13. Permanent baseline is 171 tests: Domain 11, Application 60, Infrastructure 87, Architecture 13.
14. SQLite schema remains version 2.
15. No WP07+, Release 1.4, retry, scheduling, DAG, streaming, checkpoint/resume, durable run-history, or distributed-execution work has started.

Only after all starting gates pass may #143 move Backlog → In Progress.

## Objective

Harden the **Application-owned pipeline validation and failure semantics** around the accepted WP05 fixed orchestration.

WP06 must define and enforce the minimum deterministic rules necessary to ensure that:

- invalid pipeline requests/definitions/evidence fail predictably;
- the first failing semantic stage is preserved;
- Release 1.2 storage/materialization failure distinctions remain meaningful;
- integrity conflicts remain non-destructive;
- unknown failures are not silently converted into success or generic availability failures;
- no later stage executes after failure;
- failure evidence contains only facts established before or at the failure boundary.

WP06 is a semantic hardening package. It must not redesign the fixed pipeline topology or introduce resilience/recovery behavior.

## Preserve accepted topology

The WP05 fixed semantic topology remains exactly:

1. Historical observation retrieval
2. Dataset materialization
3. Immutable snapshot persistence
4. Catalog registration
5. Structured result/evidence

Do not add, remove, reorder, retry, parallelize, dynamically configure, or loop stages.

## Validation responsibilities

Inspect the existing WP04/WP05 constructors and contracts before adding validation.

Prefer existing construction invariants. Do not duplicate checks merely to create a new validator abstraction.

Validate only where a real gap exists, including as applicable:

- pipeline identity fingerprints and scheme;
- fixed definition/topology consistency;
- request/definition consistency;
- dataset-definition consistency;
- stage ordering;
- stage-evidence uniqueness and prefix semantics;
- success/failure terminal-state consistency;
- provenance/lineage consistency;
- definition/execution identity relationships;
- source-state identity availability only after it is established;
- dataset snapshot/version identity availability only after it is established;
- disposition validity;
- first-failure attribution;
- successful empty-result consistency.

Do not normalize, repair, reinterpret, or silently replace invalid semantic values.

If current constructors already make an invalid state unrepresentable, document/reuse that boundary rather than adding redundant production code.

## Failure taxonomy

Reconcile and preserve the existing Release 1.2 and WP04/WP05 failure vocabulary.

The resulting pipeline-level semantics must distinguish, where the current contracts support them:

- invalid semantic input/evidence;
- source history unavailable;
- source/dataset invalid data;
- snapshot-store unavailable;
- snapshot integrity conflict;
- catalog unavailable;
- catalog integrity conflict;
- exact catalog `NotFound` where relevant to an operation that performs lookup;
- unknown/unclassified failures that must propagate rather than be swallowed.

Do not collapse integrity conflict into availability.

Do not treat `EquivalentExisting` as failure.

Do not fabricate a failure category solely for presentation convenience.

If a missing pipeline failure category is genuinely required to represent an already-established semantic distinction, add the smallest Application-owned refinement permitted by the manifest.

## First-failure semantics

For every terminal failure:

- identify the first failing semantic stage;
- retain evidence only for completed stages plus the failing stage as permitted by WP04;
- do not claim later stages were executed;
- do not synthesize source-state or dataset identities that were never established;
- do not change Pipeline Definition Identity because execution failed;
- derive/retain Semantic Pipeline Execution Identity only according to WP03’s accepted failure-identity semantics and evidence actually established.

Failure identity derivation must remain acyclic and deterministic.

## Unknown failure policy

Do not add `catch (Exception)` merely to convert unknown exceptions into a bounded pipeline result.

Known failures may be mapped only at the boundary that owns enough information to classify them.

Unknown SQLite behavior remains Infrastructure-owned under Release 1.2 rules. WP06 must not leak SQLite exception types into Application.

Unknown unrelated exceptions should propagate unless an existing authoritative Application contract explicitly classifies them.

No exception swallowing.

## Integrity and immutability

Preserve:

- immutable accepted dataset snapshots;
- non-destructive catalog evidence;
- no overwrite, repair, reassignment, or mutation after conflict;
- same-fingerprint contradictory content as integrity conflict;
- equivalent reruns as successful semantic equivalence.

WP06 must not create compensation logic for a previously completed stage.

## Operational exclusions

The following must not affect semantic validation, identity, or failure classification:

- wall-clock timestamps;
- logging timestamps;
- correlation IDs;
- process/machine identity;
- filesystem paths;
- connection strings;
- random values;
- local culture/timezone;
- provider ordering;
- database natural row ordering.

Operational invocation identity remains distinct from semantic pipeline execution identity.

## Explicitly out of scope

Do not implement:

- retries;
- exponential backoff;
- circuit breakers;
- fallback providers;
- checkpoints;
- partial-run resume;
- compensation/rollback across completed semantic stages;
- scheduling;
- recurring refresh;
- configurable DAGs;
- plugins;
- streaming/parallel/distributed execution;
- durable pipeline-run history;
- metrics/tracing backends;
- feature engineering;
- model training/evaluation/MLOps;
- live provider acquisition.

These remain Release 1.4+ or otherwise later-governed capabilities.

## Architecture and storage constraints

Expected WP06 ownership:

- Domain delta: 0.
- Application: minimum validation/failure refinements only.
- Infrastructure: 0 unless the authoritative file manifest explicitly assigns a narrowly necessary WP06 change; do not assume it does.
- Worker: 0.

Preserve dependency graph:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Application must remain free of SQLite, SQL, filesystem, provider/HTTP, and Worker mechanics.

SQLite schema remains version 2.

Expected package/reference delta: `0/0`.

## File authority

Use `RELEASE_1.3_FILE_MANIFEST.md` as exact mutation authority.

Do not create a generalized validation framework unless the manifest explicitly authorizes it and repository truth demonstrates it is necessary.

Prefer narrow refinements to existing WP04/WP05 files over parallel competing models when authorized.

If a necessary file is not authorized, stop and report the authority gap.

## Permanent-test protection

WP10 owns Application pipeline permanent tests.

WP06 must add **zero permanent tests**.

Temporary offline probes are permitted only to establish WP06 semantics. Remove all probe code, build artifacts attributable to the probe, SQLite files, WAL/SHM/journal files, and other temporary residue before completion.

## Required validation matrix

Prove, with existing tests plus temporary offline probes where necessary:

1. Valid success remains successful.
2. Valid empty success remains successful.
3. Equivalent rerun remains successful and semantically equivalent.
4. Invalid pipeline identity fingerprint is rejected.
5. Invalid request/definition relationship is rejected if representable.
6. Invalid stage ordering/evidence prefix is rejected if representable.
7. Source history unavailable terminates at the first applicable stage.
8. Invalid source/dataset evidence remains distinct from unavailability where existing contracts expose it.
9. Snapshot unavailability terminates before catalog registration.
10. Snapshot integrity conflict terminates before catalog registration and does not overwrite evidence.
11. Catalog unavailability terminates at catalog registration.
12. Catalog integrity conflict remains non-destructive.
13. Unknown/unclassified exceptions are not swallowed or misclassified.
14. Failure evidence contains no identities/evidence that were not established.
15. No later stage executes after the first failure.
16. Pipeline Definition Identity remains stable across success/failure for the same definition.
17. Semantic execution identity obeys WP03 failure semantics.
18. `NewlyAccepted` and `EquivalentExisting` remain dispositions, not identity-bearing differences.
19. Dataset target, boundaries, timestamp offsets, decimals, provenance, lineage, snapshot identity/version, and source-state identity remain faithful.
20. No provider/network call occurs.
21. SQLite schema remains version 2.
22. No retry/recovery/scheduling/run-history behavior exists.

Where an invalid scenario is structurally impossible because WP04 constructors already reject it, record that as validation evidence instead of weakening the contracts to manufacture a test.

## Canonical validation

Before completion run:

- restore;
- format verification;
- build;
- Domain.Tests;
- Application.Tests;
- Infrastructure.Tests;
- Architecture.Tests;
- `eng/verify.ps1 -Configuration Release`;
- canonical Gitleaks;
- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace checks for authorized untracked WP06 files when required;
- temporary SQLite/WAL/SHM/journal residue scan.

Final permanent counts must remain:

- Domain.Tests: 11
- Application.Tests: 60
- Infrastructure.Tests: 87
- Architecture.Tests: 13
- Total: 171
- Skipped: 0

Build warnings/errors must remain `0/0`.

## GitHub lifecycle

After all starting gates pass:

1. Move #143 Backlog → In Progress.
2. Execute WP06.
3. Post concise completion evidence.
4. Close #143.
5. Set #143 Project status to Done.
6. Verify #144 remains Open/Backlog unchanged.
7. Keep milestone #54 open.

Do not mutate other planning fields, dependencies, milestone metadata, Project schema/options, or later issues.

## Git protection

Do not stage, commit, push, branch, open PRs, merge, tag, release, rebase, reset, rewrite history, or perform unrelated Git transport.

## Stop conditions

Stop without claiming completion if:

- starting-state governance is inconsistent;
- canonical baseline fails unexpectedly;
- WP05 topology must be redesigned;
- WP03 identity semantics would need revision;
- a required change falls outside the manifest;
- Infrastructure/Worker/schema/package/reference mutation becomes necessary without explicit authority;
- permanent tests must change;
- retries/recovery/scheduling are required;
- provider/network access is required;
- Release 1.4 work becomes necessary;
- final canonical validation fails.

Report the exact blocker and smallest corrective authority required.

## Required execution report

Return a numbered report covering at least:

1. Executive summary
2. Authorities reviewed
3. Repository/Git state
4. Working-tree classification
5. Predecessor/lifecycle gates
6. Initial baseline
7. WP02 semantic reconciliation
8. WP03 identity/provenance/evidence reconciliation
9. WP04 contract reconciliation
10. WP05 orchestration reconciliation
11. Existing validation inventory
12. Existing failure inventory
13. Validation-boundary decision
14. Failure-taxonomy decision
15. First-failure semantics
16. Pipeline identity validation
17. Request/definition validation
18. Stage evidence/prefix validation
19. Success semantics
20. Empty-success semantics
21. Equivalent-rerun semantics
22. Source-unavailable semantics
23. Invalid-data semantics
24. Snapshot-unavailable semantics
25. Snapshot-conflict semantics
26. Catalog-unavailable semantics
27. Catalog-conflict semantics
28. Unknown-failure propagation
29. Evidence-established-only rule
30. Immutability/non-destructive behavior
31. Dataset fidelity
32. Operational exclusions
33. Exact files added/modified
34. Layer deltas
35. Package/reference/schema delta
36. Permanent test delta
37. Temporary probe evidence/cleanup
38. WP07 protection
39. WP08/WP09 protection
40. Release 1.4 protection
41. Security/offline evidence
42. Whitespace/diff evidence
43. Restore/build evidence
44. Permanent test evidence
45. Canonical verification
46. Architecture validation
47. Release 1.1/1.2 regression
48. WP05 orchestration regression
49. Validation/failure acceptance matrix
50. Mutation accounting
51. Git/GitHub protection
52. Planning protection
53. Findings/blockers
54. Final repository/GitHub state
55. WP07 handoff
56. Final decision
57. Next authorized work package

Successful execution must end exactly with:

`RELEASE 1.3 WP06 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP07 — Structured Execution Evidence — GitHub issue #144`

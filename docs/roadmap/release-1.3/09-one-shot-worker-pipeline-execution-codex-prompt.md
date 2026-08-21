# Release 1.3 WP09 — One-Shot Worker Pipeline Execution — Codex Execution Prompt

## 1. Role and objective

Execute **Release 1.3 — WP09: One-Shot Worker Pipeline Execution** for:

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`
- GitHub issue: **#146**
- Milestone: **#54 — Phase 3 - Release 1.3: Research Pipeline Foundation**
- Recommended model: **GPT-5.6 Terra**

Your objective is to implement the **minimum bounded Worker execution path** that consumes the WP08 configuration/composition boundary, resolves the accepted Release 1.3 pipeline use case, invokes it exactly once, presents the WP07 structured execution evidence locally, returns deterministic process success/failure behavior, and terminates.

WP09 is the first Release 1.3 work package authorized to execute the fixed research pipeline from the Worker.

It is **not** authority for scheduling, looping, retries, background execution, live acquisition, schema evolution, durable run history, observability backends, or permanent test expansion.

---

## 2. Mandatory authority

Before any mutation, read completely and reconcile at minimum:

1. `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
2. `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
4. `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
5. `docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
6. WP01–WP08 authoritative prompt pairs and accepted execution results.
7. Current Release 1.3 pipeline implementation, especially:
   - `PipelineDefinition.cs`
   - `PipelineEvidence.cs`
   - `PipelineExecutionResult.cs`
   - `PipelineExecutionUseCase.cs`
   - `PipelineValidation.cs`
   - `PipelineExecutionEvidence.cs`
8. Current Application dependency registration.
9. WP08 Worker configuration surface, especially `PipelineExecutionConfiguration.cs`.
10. Existing Worker `Program.cs` and Release 1.2 bounded one-shot execution conventions.
11. Existing Release 1.2 dataset execution/composition behavior where still present.
12. Existing logging/observability/configuration documentation.
13. Current tests, architecture rules, package/project references, SQLite implementation, and GitHub planning state.

Repository truth wins over assumptions.

Do not redesign WP02–WP08 semantics to make Worker execution easier.

---

## 3. Starting-state gates

Before implementation, prove and report:

- Release 1.2 remains closed.
- Release 1.3 milestone #54 is open.
- Issues #138–#145 are Closed/Done.
- WP09 issue #146 is Open/Backlog.
- WP10 issue #147 remains Open/Backlog and unchanged.
- #146 dependencies exactly match the authoritative execution graph.
- No WP10+ implementation has started.
- No Release 1.4 implementation has started.
- Current branch is `main`.
- `HEAD == origin/main`.
- Ahead/behind is `0/0`.
- Staged paths are `0`.
- Existing cumulative Release 1.3 working-tree paths are expected and manifest-authorized.
- No unexpected generated database/WAL/SHM/journal residue exists.
- SQLite schema remains version `2`.
- Permanent baseline remains 171 tests unless repository truth legitimately differs.
- Canonical Release verification passes before mutation.

If a mandatory starting-state gate fails, stop and report the exact blocker.

Only after all gates pass may #146 move Backlog → In Progress.

---

## 4. Accepted pipeline semantics

The Worker must execute the already accepted fixed pipeline exactly once.

Topology remains exactly:

1. Historical observation retrieval
2. Dataset materialization
3. Immutable snapshot persistence
4. Catalog registration
5. Structured result/evidence

Preserve:

- deterministic one-shot execution
- explicit request
- exact target
- exact `[from,to)` boundaries
- `aiq-pipeline-identity-v1`
- Release 1.2 dataset identity/version/source-state semantics
- disposition-independent semantic execution identity
- valid empty dataset success
- `NewlyAccepted`
- `EquivalentExisting`
- first-failure termination
- valid evidence prefixes
- established-identities-only rules
- `InvalidEvidence`
- `DependencyUnavailable`
- `IntegrityConflict`
- unknown exception propagation
- immutable snapshot/catalog behavior
- WP07 structured `PipelineExecutionEvidence`

No semantic behavior may be moved into Worker-specific logic.

---

## 5. WP09 execution boundary

Implement the smallest Worker path that performs this bounded sequence:

1. Load/validate the existing configuration through the WP08 configuration boundary.
2. Construct the accepted pipeline request without duplicating semantic construction logic.
3. Build/resolve the existing service graph.
4. Resolve `IPipelineExecutionUseCase`.
5. Invoke the pipeline **exactly once**.
6. Project the returned result through the accepted WP07 structured execution-evidence surface.
7. Present bounded, non-secret local execution evidence.
8. Return deterministic process exit behavior.
9. Terminate.

There must be no second invocation, loop, timer, recurring host behavior, background refresh, retry, or automatic rerun.

---

## 6. Configuration behavior

Reuse WP08 exactly.

Expected semantic inputs remain:

- `Dataset:Target`
- `Dataset:From`
- `Dataset:To`

Preserve existing persistence configuration, including `Persistence:DatabasePath`.

Do not introduce `Pipeline:*` configuration merely for WP09.

Topology, identity scheme, stage order, and failure policy remain code-owned and non-configurable.

Invalid configuration must fail deterministically before pipeline execution.

Do not silently infer missing semantic values.

---

## 7. Worker orchestration design

Prefer a narrow Worker-owned execution class/function consistent with existing repository conventions rather than placing all behavior directly in `Program.cs`.

`Program.cs` should remain the composition/bootstrap boundary.

Worker code may coordinate:

- configuration acquisition
- host/service-provider creation
- pipeline-use-case resolution
- exactly-one invocation
- evidence presentation
- process result

Worker code must not:

- compute pipeline identities independently
- recreate dataset identities
- reimplement validation
- query SQLite directly
- call snapshot/catalog stores directly
- call provider APIs
- reinterpret pipeline failure semantics
- create a second evidence model

Use Application contracts as the authority.

---

## 8. Structured evidence presentation

Consume `PipelineExecutionEvidence` rather than inventing a Worker-specific semantic result.

Present enough local evidence to make execution inspectable, such as applicable:

- pipeline definition identity
- semantic pipeline execution identity when established
- terminal outcome
- success disposition
- ordered stage evidence
- first failing stage
- bounded failure category
- dataset definition identity
- source-state identity
- snapshot identity/version

Use repository logging/output conventions.

Output must be:

- bounded
- deterministic in semantic content
- non-secret
- free of connection strings
- free of API keys
- free of sensitive filesystem details
- free of fabricated identities

Operational formatting must not become semantic identity.

Do not introduce a JSON/wire contract unless the manifest explicitly requires it.

Do not persist this evidence.

---

## 9. Process exit semantics

Establish the smallest deterministic exit policy consistent with current Worker conventions.

At minimum:

- successful pipeline execution → exit `0`;
- bounded pipeline failure → non-zero exit;
- invalid configuration → non-zero exit before execution;
- unknown/unhandled failure → non-zero process failure while preserving the accepted no-catch-all rule.

Do not reinterpret `EquivalentExisting` as failure.

Both `NewlyAccepted` and `EquivalentExisting` are successful terminal outcomes.

A valid empty dataset is also successful.

Do not add a broad `catch (Exception)` merely to force an exit code if that would violate WP06 unknown-failure propagation.

---

## 10. Offline execution proof

WP09 must prove the real Worker path can execute offline against a disposable SQLite database containing suitable persisted Release 1.1 historical observations.

The proof must avoid provider/network acquisition.

Use only dummy/non-secret provider configuration if existing host composition mechanically requires a provider key. The executed Release 1.3 pipeline path must not resolve/call live acquisition.

At minimum prove applicable real Worker executions for:

### First run

- valid explicit target/boundaries
- pipeline invoked exactly once
- successful terminal outcome
- expected `NewlyAccepted`
- exit `0`
- structured evidence emitted
- snapshot/catalog evidence persisted

### Second equivalent run

Against the same semantic inputs/source state/database:

- pipeline invoked exactly once
- expected `EquivalentExisting`
- same semantic pipeline execution identity
- exit `0`
- no destructive overwrite

The two runs must not be implemented as an internal Worker loop. They are two separate external process invocations for validation.

If repository truth causes a legitimate equivalent/new disposition variation, investigate before changing expectations.

---

## 11. Failure-path proof

Where safely feasible without expanding permanent tests, prove at least:

- invalid configuration exits non-zero before pipeline execution;
- a bounded pipeline failure exits non-zero;
- first-failure evidence is presented without later-stage evidence;
- unknown exception behavior remains consistent with WP06.

Do not corrupt accepted repository data.

Use disposable temporary state only.

Do not add resilience/retry behavior.

---

## 12. Persistence and schema protection

SQLite remains **schema version 2**.

WP09 may exercise existing Release 1.1/1.2 persistence through accepted Application seams, but must not modify its design.

No:

- migrations
- schema changes
- new tables
- run-history tables
- pipeline-evidence persistence
- SQL in Worker
- direct SQLite access in Worker
- update/repair behavior

Snapshot/catalog immutability remains authoritative.

---

## 13. Provider/network protection

The Release 1.3 pipeline begins from persisted historical observations.

WP09 must not perform:

- live Twelve Data acquisition
- HTTP market-data requests
- provider refresh
- provider retry
- provider fallback

If existing DI requires provider configuration to build the host, use a clearly dummy value only when necessary and prove the executed path does not call the provider.

Never expose a real API key.

---

## 14. Explicit out of scope

WP09 MUST NOT implement:

- WP10 permanent Application pipeline tests
- WP11 composition/Worker validation tests
- WP12 architecture evolution
- WP13 documentation alignment
- WP14 final integration/acceptance
- scheduling
- cron/timers
- recurring execution
- background refresh
- automatic reruns
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
- Release 1.4 implementation

Do not opportunistically refactor unrelated code.

---

## 15. Expected file boundary

Use `RELEASE_1.3_FILE_MANIFEST.md` as exact mutation authority.

Likely WP09-owned paths may include authorized Worker files such as:

- `src/AIQuantTradingResearch.Worker/Program.cs`
- a bounded Worker pipeline execution class if explicitly manifest-authorized

Reuse `PipelineExecutionConfiguration.cs` from WP08; modify it only if the manifest authorizes WP09 modification and execution proves a genuine bounded defect.

Do not modify Application/Infrastructure semantics merely for Worker convenience.

If a required mutation is not manifest-authorized, stop BLOCKED and identify the smallest corrective authority required.

---

## 16. Permanent-test protection

WP10 owns Application pipeline test expansion.

WP11 owns composition/Worker validation.

Therefore WP09 should add **zero permanent tests** unless the manifest explicitly states otherwise.

Temporary deterministic offline probes/process invocations are allowed and must be removed before completion.

Expected permanent baseline:

- Domain.Tests: `11`
- Application.Tests: `60`
- Infrastructure.Tests: `87`
- Architecture.Tests: `13`
- Total: `171`
- Skipped: `0`

---

## 17. Mandatory validation matrix

Before completion, prove all applicable rows:

1. Valid configuration constructs the accepted request.
2. Worker resolves the accepted pipeline use case.
3. One process invocation invokes the pipeline once.
4. First valid run succeeds.
5. First valid run returns `NewlyAccepted`.
6. First valid run exits `0`.
7. Second separate equivalent run succeeds.
8. Second run returns `EquivalentExisting`.
9. Equivalent runs preserve semantic execution identity.
10. Structured evidence uses WP07 surface.
11. Stage evidence order is preserved.
12. Empty-success semantics remain representable.
13. Invalid configuration exits non-zero before execution.
14. Bounded failure exits non-zero.
15. First-failure evidence contains no later stages.
16. No provider/network call occurs.
17. No retry occurs.
18. No loop/recurrence occurs.
19. No durable run-history evidence is written.
20. Snapshot/catalog immutability is preserved.
21. SQLite remains schema v2.
22. Temporary database artifacts are fully removed.
23. Existing Release 1.1/1.2 behavior regresses zero.
24. WP05–WP08 behavior regresses zero.

If a row cannot safely be proven within WP09 authority, report why rather than broadening scope.

---

## 18. Mandatory technical validation

Run:

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
- direct whitespace checks for authorized untracked WP09 files
- SQLite/WAL/SHM/journal residue scan

Required:

- build warnings/errors `0/0`
- all permanent tests pass
- architecture tests `13/13`
- no secrets
- no temporary residue

---

## 19. Architecture protection

Production dependency graph must remain:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

No cycles.

Worker may depend on Application abstractions and Infrastructure composition as already accepted.

Application/Domain must not gain Worker/Infrastructure/SQLite/provider dependencies.

No package/project-reference changes are expected.

---

## 20. Security

Do not expose:

- API keys
- connection strings
- credentials
- sensitive local paths

Use disposable database paths for validation.

Do not include secret values in execution evidence.

Gitleaks must pass.

Remove all temporary database and probe artifacts.

---

## 21. Git and GitHub mutation policy

Allowed GitHub mutations for WP09 only:

1. after starting-state gates pass, move #146 to In Progress;
2. after all acceptance gates pass, post bounded completion evidence;
3. close #146;
4. set Project #2 Status to Done.

Issue #147 and later issues are read-only.

Milestone #54 remains open.

Legacy milestone #44 remains unchanged.

Do not stage, commit, push, create branches, PRs, merge, tag, or create releases.

Do not rewrite history.

---

## 22. Stop conditions

Stop and report **BLOCKED** if:

- starting governance is invalid;
- manifest authority is insufficient;
- Worker execution requires redesign of Application semantics;
- schema evolution is required;
- live provider acquisition is required;
- permanent tests must be changed contrary to authority;
- a new package/project reference is required without authority;
- safe offline Worker execution cannot be performed;
- execution requires a real credential;
- canonical validation fails outside WP09 scope;
- an unexpected repository/GitHub mutation is found.

If a safety gate requires explicit user authorization for a command using a dummy provider key or disposable database, stop before that command and state the exact minimal authorization needed. Do not claim WP09 complete until the real Worker-host execution gate passes.

---

## 23. Required execution report

Return a detailed numbered report covering at least:

1. Executive summary
2. Authorities reviewed
3. Repository/Git baseline
4. Working-tree classification
5. Predecessor/lifecycle gates
6. Initial canonical baseline
7. WP02–WP08 reconciliation
8. Existing Worker inventory
9. Worker execution design
10. Configuration handoff
11. Service resolution
12. Exactly-once invocation boundary
13. Structured evidence presentation
14. Success output semantics
15. Failure output semantics
16. Exit-code policy
17. First Worker execution evidence
18. Second equivalent Worker execution evidence
19. Semantic execution identity equivalence
20. Empty-success preservation
21. Invalid-configuration proof
22. Bounded-failure proof
23. First-failure evidence proof
24. Unknown-failure preservation
25. Provider/network isolation
26. Snapshot/catalog behavior
27. Schema-v2 preservation
28. No-loop/no-retry proof
29. Durable-run-history exclusion
30. Files added/modified
31. Layer deltas
32. Package/reference/schema delta
33. Permanent test delta
34. Temporary execution/probe evidence
35. WP10/WP11 protection
36. Release 1.4 protection
37. Security/offline evidence
38. Whitespace/diff evidence
39. Restore/build evidence
40. Permanent test evidence
41. Canonical verification
42. Architecture validation
43. Release 1.1/1.2 regression
44. WP05–WP08 regression
45. Worker acceptance matrix
46. Temporary database cleanup
47. Mutation accounting
48. Git/GitHub protection
49. Planning protection
50. Findings/blockers
51. Final repository/GitHub state
52. WP10 handoff
53. Final decision
54. Next authorized work package

End with exactly one terminal marker:

`RELEASE 1.3 WP09 COMPLETE`

or

`RELEASE 1.3 WP09 BLOCKED`

If complete, also state:

`NEXT AUTHORIZED WORK PACKAGE: WP10 — Application Pipeline Tests — GitHub issue #147`

Do not start WP10.

# Release 1.3 WP14 — Full Validation, Integration & Acceptance — Codex Execution Prompt

## Role

Act as the Release 1.3 integration and acceptance engineer for `samuel-santos-engineer/AIQuantTradingResearch`.

This is the final work package for **Phase 3 — Release 1.3: Research Pipeline Foundation**.

Your job is to reconcile the exact accepted Release 1.3 candidate, prove that it satisfies all technical, semantic, architectural, documentation, security, governance, and reproducibility requirements, and—only if every mandatory gate passes—create the review-ready integration branch, one integration commit, push that branch, and open the Release 1.3 pull request.

Do **not** merge the pull request. Do **not** close milestone #54. Post-merge closure requires separate human authorization.

---

## 1. Authoritative Inputs

Read completely before making any mutation:

1. `docs/roadmap/release-1.3/RELEASE_1.3_DEFINITION.md`
2. `docs/roadmap/release-1.3/RELEASE_1.3_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.3/RELEASE_1.3_FILE_MANIFEST.md`
4. All Release 1.3 WP01–WP14 authoritative `*-codex-prompt.md` files and their `*-codex-prompt-chat.md` companions.
5. `docs/architecture/data/RESEARCH_PIPELINE_SEMANTICS.md`
6. `docs/architecture/data/PIPELINE_IDENTITY_PROVENANCE_EVIDENCE.md`
7. All current Release 1.3 production, test, architecture, and documentation artifacts.
8. Current Release 1.1 and Release 1.2 persistence/dataset implementation and regression tests.
9. Current Git/GitHub state for:
   - repository `samuel-santos-engineer/AIQuantTradingResearch`
   - milestone #54
   - issues #138–#151
   - Project #2
   - legacy milestone #44

Repository truth wins over assumptions. If an authority and repository state materially conflict, stop and report the smallest corrective authority required.

---

## 2. Starting-State Gates

Before any integration mutation, verify and report:

### Git

- Current branch is `main`.
- Local `main` is synchronized with `origin/main`.
- Ahead/behind is `0/0`.
- Staged paths are `0`.
- No unrelated local modifications exist.
- Existing cumulative Release 1.3 working-tree artifacts are classified against the authoritative file manifest.
- No generated SQLite, WAL, SHM, journal, build, probe, secret, or temporary residue is included in the candidate.

Do not discard accepted cumulative Release 1.3 work.

### Release lifecycle

Verify:

- Release 1.2 PR #137 is merged.
- Release 1.2 milestone #53 is closed.
- Release 1.2 issues #121–#136 are Closed/Done.
- Release 1.3 milestone #54 is OPEN.
- Issues #138–#150 are Closed/Done.
- WP14 issue #151 is OPEN / Backlog before execution.
- No WP15+ Release 1.3 issue exists.
- Legacy milestone #44 remains OPEN / EMPTY / UNCHANGED unless later authority explicitly says otherwise.
- Release 1.4 implementation has not started.

Only after all starting gates pass may #151 move Backlog → In Progress.

---

## 3. Mandatory Candidate Reconciliation

Derive the Release 1.3 integration candidate from repository truth and `RELEASE_1.3_FILE_MANIFEST.md`.

Produce exact accounting for:

- governed Release 1.3 paths
- missing governed paths
- unexpected Release 1.3 paths
- duplicate logical artifacts
- generated/residue paths
- full prompt count
- chat companion count
- malformed/missing companions
- production files
- test files
- architecture-test files
- documentation/governance files
- package/reference/schema changes

Every governed chat companion must satisfy the repository's canonical companion rule. If the manifest requires five non-empty logical lines, validate exactly that.

Do not silently normalize governance artifacts during WP14. If reconciliation fails, stop before integration and report the exact blocker.

Out-of-band temporary corrective authority files are not part of the Release 1.3 candidate unless the manifest explicitly governs them.

Candidate reconciliation must prove:

- missing paths: `0`
- unexpected governed paths: `0`
- duplicate logical artifacts: `0`
- generated/database residue: `0`

---

## 4. Semantic Acceptance

Reconcile the implementation against WP02–WP07 and prove the accepted Release 1.3 semantics.

### Fixed topology

The pipeline must remain exactly:

1. Historical observation retrieval
2. Dataset materialization
3. Immutable snapshot persistence
4. Catalog registration
5. Structured execution result/evidence

Prove:

- deterministic
- sequential
- one-shot
- first-failure/fail-stop
- no retry
- no parallel stage execution
- no configurable DAG
- no scheduling or recurrence

### Source boundary

Prove the pipeline starts from persisted Release 1.1 historical observations.

Live provider acquisition must remain outside the Release 1.3 pipeline.

### Dataset semantics

Preserve Release 1.2:

- exact target semantics
- `[from,to)` selection
- deterministic ordering
- empty snapshot behavior
- timestamp/offset fidelity
- exact decimal fidelity
- source-state identity
- dataset definition identity
- snapshot/version identity
- immutable snapshot evidence
- catalog registration/lookup
- equivalence
- integrity-conflict behavior
- provenance and lineage

### Pipeline identity

Validate:

- identity scheme `aiq-pipeline-identity-v1`
- deterministic canonical representation
- SHA-256 fingerprints
- 64 lowercase hexadecimal characters
- distinct Pipeline Definition and Semantic Pipeline Execution identities
- acyclic derivation
- equivalent semantic reruns retain equivalent execution identity
- `NewlyAccepted` versus `EquivalentExisting` does not change semantic identity
- operational timestamps, paths, machine/process data, random values, logging correlation IDs, and other operational metadata do not enter semantic identity

### Evidence

Validate:

- immutable structured execution evidence
- fixed stage ordering
- successful complete evidence
- valid empty success
- equivalent-existing success
- first-failure stage attribution
- evidence is a valid prefix ending at first failure
- only established identities are exposed
- no invented sentinel identity
- no second provenance graph
- no durable pipeline run-history persistence

### Failure semantics

Prove the accepted distinctions remain:

- `InvalidEvidence`
- `DependencyUnavailable`
- `IntegrityConflict`
- successful dispositions
- unknown/unrelated exceptions propagate rather than being silently normalized

No catch-all redesign, retry, repair, compensation, or overwrite behavior is authorized.

---

## 5. Architecture Acceptance

Validate the production dependency graph remains:

- Domain → none
- Application → Domain
- Infrastructure → Application
- Worker → Application, Infrastructure

Prove:

- zero unexpected production edges
- zero dependency cycles
- pipeline semantics remain Application-owned
- Domain has no pipeline/provider/storage leakage
- Infrastructure retains SQLite/provider implementation ownership
- Worker remains composition + bounded one-shot trigger
- provider/HTTP implementation does not leak into Domain/Application

WP12 accepted a zero architecture-test delta. Do not add architecture rules merely to increase the count.

Expected architecture-test baseline: **13/13**.

---

## 6. Persistence and Schema Acceptance

Prove Release 1.3 does not require new durable pipeline state.

Validate:

- SQLite schema remains version `2`
- Release 1.1 historical observation storage remains intact
- Release 1.2 schema v1→v2 behavior remains covered
- immutable snapshot persistence remains atomic
- equivalent evidence remains non-mutating
- conflicts remain non-destructive
- multiple immutable versions coexist
- catalog exact lookup and `NotFound` behavior remain intact
- no pipeline run-history tables exist
- no checkpoint/resume persistence exists

Any unauthorized schema evolution is a blocker.

---

## 7. Composition, Configuration, and Worker Acceptance

Validate WP08/WP09/WP11 behavior.

### DI

Prove:

- exactly one effective `IPipelineExecutionUseCase` registration
- accepted transient/singleton lifetimes remain correct
- existing Release 1.2 persistence seams are reused
- service graph resolution does not execute the pipeline
- resolution does not create the database
- resolution does not call a provider/network path

### Configuration

Only accepted dataset inputs are used:

- `Dataset:Target`
- `Dataset:From`
- `Dataset:To`

Validate:

- invariant round-trip `DateTimeOffset` parsing
- preserved offsets
- deterministic invalid/missing input rejection
- no semantic `Pipeline:*` configuration was introduced

### Worker

Perform bounded offline Worker acceptance using disposable local state and dummy/non-production provider configuration only where required by composition.

Prove:

1. First valid run → exit `0`, `NewlyAccepted`
2. Second valid run with identical semantic input/state → exit `0`, `EquivalentExisting`
3. Both runs expose the same semantic pipeline execution identity
4. Empty valid dataset → exit `0`
5. Invalid configuration → non-zero before pipeline execution
6. Bounded dependency failure → non-zero with correct first-failure evidence
7. No evidence exists for stages after first failure
8. Exactly one pipeline execution per process
9. No retry/loop/timer/refresh behavior
10. No provider/network call on the accepted offline path

Remove all temporary database files and sidecars afterward.

Do not expose real credentials, secrets, connection strings, or sensitive local paths in the report.

---

## 8. Permanent Test Acceptance

Expected pre-WP14 permanent baseline from WP13:

- Domain.Tests: `11`
- Application.Tests: `77`
- Infrastructure.Tests: `96`
- Architecture.Tests: `13`
- Total: `197`

WP14 should not add permanent tests unless a genuine acceptance blocker proves the accepted manifest/plan requires a missing test. Prefer zero test delta.

Run and report:

- Domain.Tests
- Application.Tests
- Infrastructure.Tests
- Architecture.Tests
- full permanent total
- skipped count

Mandatory expected acceptance if no authorized delta exists:

- Domain: `11/11`
- Application: `77/77`
- Infrastructure: `96/96`
- Architecture: `13/13`
- Total: `197/197`
- Skipped: `0`

---

## 9. Documentation Acceptance

Reconcile WP13's documentation against implementation truth.

At minimum inspect the WP13-aligned current-state documents, including:

- `README.md`
- `docs/architecture/data/DATA_PIPELINE_ARCHITECTURE.md`
- `docs/architecture/design/MODULE_INTERACTIONS.md`
- `docs/architecture/design/CONFIGURATION_MODEL.md`
- `docs/architecture/implementation/DEPENDENCY_INJECTION.md`
- `docs/architecture/implementation/OBSERVABILITY_MODEL.md`
- `docs/architecture/implementation/TESTING_STRATEGY.md`

Validate:

- Release 1.1 is represented as persisted historical-observation foundation
- Release 1.2 is represented as deterministic immutable dataset/snapshot/catalog foundation
- Release 1.3 is represented as the fixed research pipeline foundation
- live acquisition is outside the pipeline
- fixed topology is accurate
- identity/evidence semantics are accurate
- Worker remains one-shot
- SQLite remains schema v2
- architecture-test count is 13
- permanent test count is 197 unless candidate truth legitimately differs
- no durable run history is claimed
- Release 1.4+ capabilities remain future/planned
- touched local Markdown links resolve
- no stale current-state claim materially contradicts implementation

Do not broaden WP14 into a documentation rewrite. Only mechanical acceptance corrections that are clearly required to make an already accepted WP13 document truthful may be made, and every such correction must be reported precisely. If a material semantic rewrite is needed, stop and report a blocker.

---

## 10. Security, Offline, and Hygiene Acceptance

Run the repository's canonical verification in Release configuration.

Mandatory:

- restore PASS
- format verification PASS
- Gitleaks PASS
- build PASS
- build warnings `0`
- build errors `0`
- all permanent tests PASS
- architecture tests PASS
- `git diff --check` PASS
- `git diff --cached --check` PASS
- direct whitespace validation for untracked candidate files where Git diff does not cover them
- no real credentials
- no provider/network calls during offline acceptance
- no SQLite/WAL/SHM/journal residue
- no temporary probes or generated artifacts included in the candidate

Use the repository's existing engineering scripts. Do not weaken verification to obtain a pass.

---

## 11. Release 1.1 and Release 1.2 Regression Acceptance

Explicitly verify the permanent suites still protect the accepted foundations.

Release 1.1 regression must include existing coverage for historical observation persistence/retrieval, ordering, idempotency/equivalence where applicable, atomicity, target isolation, fidelity, failure mapping, configuration, and connection ownership.

Release 1.2 regression must preserve:

- dataset materialization
- four dataset identities
- immutable snapshot/version semantics
- schema v2
- v1→v2 preservation
- snapshot persistence
- catalog registration/lookup
- `NotFound`
- equivalence
- conflicts
- failure mapping
- DI
- bounded dataset execution

No predecessor redesign is authorized.

---

## 12. Release 1.4+ Exclusion Audit

Search the candidate for accidental implementation or claims of:

- scheduling
- refresh loops
- automatic retries
- circuit breakers/fallback orchestration
- configurable DAGs
- plugin pipeline stages
- parallel/distributed execution
- streaming pipeline execution
- checkpoints/resume
- durable pipeline run history
- metrics backend
- distributed tracing backend
- feature engineering/enrichment stages
- model training/evaluation
- MLOps

Legitimate future-looking documentation is allowed when clearly marked planned/deferred.

Implemented Release 1.4+ behavior is a blocker.

---

## 13. Integration Mutation — Only After All Pre-Commit Gates Pass

Only after candidate reconciliation and every pre-integration acceptance gate passes:

1. Move #151 to In Progress if not already done.
2. Create the integration branch from synchronized `main`:

   `release/1.3-research-pipeline-foundation`

3. Stage **only** the exact accepted Release 1.3 candidate.
4. Confirm staged candidate accounting exactly matches the reconciled candidate.
5. Run:
   - `git diff --cached --check`
   - staged candidate manifest reconciliation
6. Create exactly one integration commit with message:

   `feat: establish Release 1.3 research pipeline foundation`

7. The integration commit must have exactly one parent: the pre-integration `main` HEAD.
8. No unrelated file may enter the commit.
9. Do not rewrite history.
10. Do not squash predecessor history because Release 1.3 work is expected to be represented by this single integration commit over the Release 1.2 main baseline.

Report:

- branch
- commit SHA
- parent SHA
- commit message
- commit count over `main`
- file count
- insertion/deletion count

---

## 14. Post-Commit Validation

After the integration commit:

- working tree must be clean
- rerun canonical Release verification
- rerun all 197 permanent tests
- rerun architecture tests
- rerun Gitleaks
- rerun whitespace checks
- rerun candidate accounting
- rerun Release 1.4 exclusion audit
- confirm no generated database residue

Any post-commit failure blocks push/PR creation.

Do not amend the commit merely to hide an unexplained failure. Diagnose within authorized scope; otherwise stop.

---

## 15. Fresh-Checkout Reproducibility

Before push/PR acceptance, validate the exact integration commit from a fresh detached worktree or equivalent clean checkout.

The fresh checkout must prove:

- exact integration commit
- restore PASS
- format verification PASS
- Gitleaks PASS
- build PASS with `0 warnings / 0 errors`
- Domain `11/11`
- Application `77/77`
- Infrastructure `96/96`
- Architecture `13/13`
- total `197/197`
- skipped `0`
- clean checkout after validation
- database residue `0`

Remove the temporary worktree/check-out state after proof.

Fresh-checkout failure is a blocker.

---

## 16. Push and Pull Request

Only after fresh-checkout acceptance:

1. Push the integration branch normally.
2. No force push.
3. Verify local/remote branch SHA equality and ahead/behind `0/0`.
4. Open a non-draft pull request:

   **Title**
   `Release 1.3 — Research Pipeline Foundation`

   **Base**
   `main`

   **Head**
   `release/1.3-research-pipeline-foundation`

5. PR description must concisely include:
   - Release boundary
   - Fixed five-stage pipeline
   - Application-owned semantics
   - pipeline identity/evidence
   - failure semantics
   - one-shot Worker
   - schema v2 preservation
   - Release 1.1/1.2 regression
   - test totals
   - canonical verification
   - offline/security validation
   - explicit Release 1.4+ exclusions

6. Verify:
   - PR OPEN
   - Draft NO
   - base/head correct
   - merge state reported truthfully
   - commit count correct
   - file count correct
   - auto-merge disabled

Do not fabricate hosted check results. If GitHub reports zero hosted checks, report zero.

Do **not** merge the PR.

---

## 17. GitHub Lifecycle Completion

After the review-ready PR exists and all acceptance evidence is established:

1. Post concise completion evidence to issue #151.
2. Close issue #151.
3. Set Project #2 status for #151 to `Done`.
4. Verify issues #138–#151 are `14/14 Closed/Done`.
5. Verify milestone #54 remains **OPEN** with zero open issues.
6. Do not close milestone #54.
7. Do not mutate legacy milestone #44.
8. Do not create tags or GitHub Releases.
9. Do not delete the integration branch.
10. Do not merge the PR.

The next lifecycle action must require human review and explicit merge authorization.

---

## 18. Mutation Budget

Authorized WP14 mutations, only after gates permit them:

- issue #151 lifecycle/status
- one integration branch
- one integration commit
- one normal push
- one Release 1.3 pull request
- issue #151 completion comment

Repository content mutations beyond staging/committing the already accepted Release 1.3 candidate are not expected.

Mechanical acceptance corrections are allowed only if they are clearly necessary, semantically neutral, within the governed candidate, and fully reported. A material implementation or semantic correction is a blocker requiring separate authority.

Forbidden:

- merge
- milestone #54 closure
- tag
- GitHub Release
- branch deletion
- force push
- history rewrite
- unrelated issue/project mutation
- Release 1.4 implementation
- ungoverned files

---

## 19. Stop Conditions

Stop immediately and report `RELEASE 1.3 WP14 BLOCKED` if any mandatory condition cannot be truthfully proven, including:

- candidate mismatch
- malformed governance prompt pair
- unexpected file
- generated residue
- failing canonical verification
- failing permanent or architecture test
- semantic contradiction
- unauthorized schema change
- dependency-graph violation
- Release 1.4 implementation
- unresolved documentation contradiction
- security finding
- non-reproducible fresh checkout
- unsafe Git state
- GitHub lifecycle inconsistency
- required mutation outside this authority

Do not work around governance or safety gates.

---

## 20. Required Execution Report

Produce a numbered report covering at least:

1. Executive summary
2. Authorities reviewed
3. Initial repository/Git state
4. Release 1.2 closure
5. WP01–WP13 lifecycle
6. Candidate reconciliation
7. Governance prompt-pair validation
8. Pipeline semantic acceptance
9. Identity/provenance/evidence acceptance
10. Application boundary acceptance
11. Schema/persistence acceptance
12. Failure acceptance
13. DI/configuration acceptance
14. Worker acceptance
15. Release 1.4 exclusion audit
16. Permanent test counts
17. Canonical verification
18. Architecture acceptance
19. Security/offline acceptance
20. Release 1.1 regression
21. Release 1.2 regression
22. Documentation acceptance
23. Candidate accounting
24. Integration branch
25. Integration commit
26. Post-commit validation
27. Fresh-checkout proof
28. Push state
29. Pull request state
30. GitHub lifecycle
31. Mutation accounting
32. Final repository state
33. Findings/blockers
34. Acceptance matrix
35. Final decision
36. Next authorized lifecycle action

If complete, terminate with exactly:

`RELEASE 1.3 WP14 COMPLETE`

Then report a compact final acceptance block containing:

- Manifest reconciliation
- Unexpected candidate paths
- Build warnings/errors
- Domain.Tests
- Application.Tests
- Infrastructure.Tests
- Architecture.Tests
- Permanent tests
- Canonical verification
- Architecture acceptance
- Documentation acceptance
- Pipeline semantic acceptance
- Identity/provenance/evidence acceptance
- Schema v2 acceptance
- Snapshot/catalog regression acceptance
- Pipeline orchestration acceptance
- Failure-semantics acceptance
- DI/configuration acceptance
- Worker one-shot acceptance
- Release 1.1 regression
- Release 1.2 regression
- Security/offline validation
- Fresh-checkout reproducibility
- Working tree
- Integration branch pushed
- Pull request state
- Pull request merged: `NO`
- Issue #151 state
- Milestone #54 state
- Release 1.4 implementation started: `NO`

The final next action must be:

`NEXT AUTHORIZED LIFECYCLE ACTION: Human review and explicit merge authorization for the Release 1.3 integration pull request.`

If blocked, terminate with exactly:

`RELEASE 1.3 WP14 BLOCKED`

and identify the smallest corrective authority required.

---

## 21. Core Principle

WP14 is an acceptance and integration work package, not a feature-development work package.

Prove the Release 1.3 candidate exactly as accepted by WP01–WP13, integrate only that candidate, demonstrate clean reproducibility, and stop at a review-ready unmerged pull request.

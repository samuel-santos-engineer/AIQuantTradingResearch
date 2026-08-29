# Release 1.10 WP08 — Full Validation, Acceptance & PR Readiness Authority

## Model assignment

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY full validation execution, acceptance verification, approved repository/Git/GitHub mutations, freeze, PR preparation/publication, and WP lifecycle completion.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, exploratory/non-authoritative review; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Terra.**

---

# Authority identity

Release: **1.10**

Work package:

**WP08 — Full Validation, Acceptance & PR Readiness**

Issue: **#249**

Milestone: **#59**

Project: **#2**

Predecessor:

**WP07 #248 — Closed / Done**

Dependency:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08`

This is the final Release 1.10 work package before release-level merge/publication closure.

---

# Accepted entry state

Treat as accepted unless live authoritative inspection directly contradicts it:

- #242–#248 Closed/Done.
- #249 Open/Backlog.
- milestone #59 Open: **1 open / 7 closed**.
- Release taxonomy on Project #2 remains Release=1.10 for #249.
- pre-existing dirty WP01–WP07 implementation/documentation work is expected and must be preserved.

Accepted carried-forward validation baseline after WP07:

- Application: **136/136**
- Infrastructure: **191/191**
- Architecture: **27/27**
- Domain: **11/11**
- total .NET: **365/365**
- Python: **25/25**
- Streamlit: **1.61.1**
- `pip check`: clean
- build: 0 errors
- Gitleaks 8.30.1: clean across 112 commits
- two local `AIQuantTradingDev` selector warnings: documented environment-only.

Emit:

`RELEASE 1.10 WP08 ENTRY: PASS`

---

# Governing source hierarchy

Before mutation read:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. WP07 documentation:
   - `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
   - `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
   - `docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`
6. all WP01–WP07 production/test changes.
7. the four dedicated WP06 permanent test paths.
8. #249.
9. milestone #59.
10. Project #2 item for #249.
11. current branch/worktree/status/diff.
12. repository contribution/PR conventions.
13. current `main` / `origin/main` relationship.
14. existing release/tag conventions read-only.

Runtime/test evidence outranks stale prose; definition/governance controls scope.

Emit:

`RELEASE 1.10 WP08 CONTRACT/HANDOFF CONSUMPTION: PASS`

---

# WP08 scope

WP08 is authorized to:

- validate the complete Release 1.10 candidate;
- audit all Release 1.10 changed paths;
- repair only validation/readiness defects that are already within frozen Release 1.10 contracts and exact manifest ownership;
- update explicitly authorized WP08 readiness/acceptance documentation paths if the manifest names them;
- execute full build/test/security/environment/residue gates;
- verify deterministic provenance/capability boundaries;
- establish a frozen candidate commit;
- create a release branch only if repository workflow requires one and the authority's Git gate below passes;
- stage/commit the accepted Release 1.10 candidate;
- push an approved branch;
- create/update the Release 1.10 PR;
- close #249 and ensure Project Done after exact WP08 acceptance.

WP08 is NOT automatically authorized to:

- merge the PR;
- close milestone #59;
- create version/tag;
- publish a GitHub Release;
- change Release taxonomy;
- add packages;
- change schema/migrations;
- introduce a live provider;
- add an exporter;
- add trading/ML/backtesting/parallel pipeline capability.

Those require explicit release-completion authority unless already unambiguously delegated by binding Release 1.10 planning.

---

# Phase 0 — Repository and GitHub entry audit

Verify:

- current branch;
- HEAD;
- `origin/main`;
- ahead/behind state;
- complete dirty path set;
- untracked paths;
- #249 Open;
- #249 Backlog;
- Release=1.10;
- milestone #59;
- unique Project #2 item;
- #242–#248 Closed/Done;
- milestone #59 Open 1/7.

Classify every dirty/untracked path as:

A. authorized Release 1.10 path;
B. pre-existing non-Release-1.10 path;
C. unexpected path requiring BLOCK or explicit reconciliation.

Do not discard user work.

Emit:

`RELEASE 1.10 WP08 REPOSITORY/GITHUB ENTRY AUDIT: PASS`

---

# Phase 1 — Exact Release 1.10 path audit

Consume the reconciled file manifest.

Produce a complete table:

| Path | WP owner | Production/Test/Docs/Planning | Expected status | Actual status | Authorized |
| --- | --- | --- | --- | --- | --- |

Require:

- every Release 1.10 mutation is manifest-owned;
- no unexpected project/package/schema/signing mutation;
- no WP09/future-release path;
- no unrelated cleanup/refactor;
- no generated/transient artifacts.

If the manifest itself is insufficient to determine the complete release mutation set, BLOCK for narrow Luna reconciliation rather than inventing ownership.

Emit:

`RELEASE 1.10 WP08 RELEASE PATH AUDIT: PASS`

---

# Phase 2 — Architecture acceptance audit

Verify Release 1.10 preserves:

## Core ownership
- .NET pipeline ownership.
- canonical visualization read model.
- atomic JSON handoff.
- Python parser/frame/presentation ownership.
- Streamlit presentation-only role.

## WP02
- `pipeline.execute`.
- exact governed stage activities.
- truthful retrieval/materialization boundary.
- parent topology.

## WP03
Exact instrumentation owners only:
- `SqliteHistoricalObservationStore.Retrieve(string target)`
- `SqliteDatasetSnapshotStore.Store(DatasetSnapshotCandidate)`
- `SqliteDatasetSnapshotStore.Retrieve(DatasetSnapshotIdentity)`

Non-owners remain:
- `SqliteDatasetCatalog`
- `SqliteHistoricalObservationStore.Persist(...)`

## WP04
- Worker lifecycle ownership.
- no external exporter.
- no exporter package/project/configuration/lifecycle.
- Python invoker bounded.
- Streamlit independent.

## WP05
- canonical .NET System Health.
- optional nested `systemHealth`.
- v1 compatibility.
- schema v4.
- exact health state vocabulary:
  - `ready`
  - `warmup`
  - `empty`
  - `failed`
  - `stale`
  - `unavailable`
- `degraded` absent.
- frozen precedence/reasons/freshness/malformed/absent behavior.
- deterministic Streamlit mapping.

## WP06
- permanent architecture/no-bypass/security enforcement exists and passes.

Emit:

`RELEASE 1.10 WP08 ARCHITECTURE ACCEPTANCE: PASS`

---

# Phase 3 — Capability/no-bypass audit

Prove no Release 1.10 path introduces:

- live provider capability;
- trading;
- ML;
- backtesting;
- parallel pipeline;
- direct Streamlit SQLite access;
- Streamlit provider access;
- Streamlit Worker supervision;
- Python second health authority;
- second System Health channel;
- schema migration;
- external telemetry exporter.

Verify Release 1.8 JSON-over-stdio remains separate.

Emit:

`RELEASE 1.10 WP08 CAPABILITY/NO-BYPASS ACCEPTANCE: PASS`

---

# Phase 4 — Provenance/truthfulness audit

Verify deterministic/replay/simulated provenance remains explicit through the governed presentation path.

System Health must not imply:

- live market connectivity;
- production provider health;
- exporter/backend availability;
- trading readiness.

Verify health is bounded to authoritative Release 1.10 source facts.

Emit:

`RELEASE 1.10 WP08 PROVENANCE/TRUTHFULNESS ACCEPTANCE: PASS`

---

# Phase 5 — Schema/handoff compatibility

Verify:

- SQLite schema remains v4.
- no Release 1.10 migration.
- canonical visualization schema remains `aiq-visualization-read-model-v1`.
- optional System Health extension preserves compatible absence.
- atomic JSON handoff remains canonical.
- Python consumes canonical handoff rather than direct persistence/provider paths.

Emit:

`RELEASE 1.10 WP08 SCHEMA/HANDOFF COMPATIBILITY: PASS`

---

# Phase 6 — Package/dependency audit

Verify exact package/project-file diff.

Require:

- no unauthorized package.
- no OpenTelemetry external package if Release 1.10 contract remains BCL-only.
- no exporter package.
- no project-reference expansion outside frozen contract.
- Python dependency set remains governed.
- Streamlit remains 1.61.1.

Report exact package/project mutations, expected to be ZERO unless the frozen manifest explicitly says otherwise.

Emit:

`RELEASE 1.10 WP08 PACKAGE/DEPENDENCY AUDIT: PASS`

---

# Phase 7 — Focused permanent tests

Run all four WP06 dedicated permanent test paths using the exact frozen focused commands.

Report actual counts.

Also run any predecessor focused suites required by the execution plan.

Require all pass.

Emit:

`RELEASE 1.10 WP08 PERMANENT OBSERVABILITY/NO-BYPASS TESTS: PASS`

---

# Phase 8 — Full .NET regression

Run the repository's complete governed .NET suites:

- Application
- Infrastructure
- Architecture
- Domain

Report actual counts individually and total.

Carried-forward expected baseline is 365/365; do not fake counts if legitimate WP08 readiness-only tests change them.

Require zero failures.

Emit:

`RELEASE 1.10 WP08 FULL DOTNET REGRESSION: PASS`

---

# Phase 9 — Full Python regression

Run the complete governed Python suites including permanent WP06 tests.

Report actual count.

Carried-forward expected baseline: 25/25.

Require zero failures.

Emit:

`RELEASE 1.10 WP08 FULL PYTHON REGRESSION: PASS`

---

# Phase 10 — Build gate

Run the canonical release build.

Report:

- warnings;
- errors;
- signing warnings separately.

Require:

- 0 errors.
- no new repository-caused warnings.

The known duplicate local `AIQuantTradingDev` selector warnings may remain only if they match the documented environment-only condition and do not indicate a tracked signing defect.

Emit:

`RELEASE 1.10 WP08 BUILD GATE: PASS`

---

# Phase 11 — Python environment gate

Verify:

- required Python version from repository configuration;
- Streamlit version;
- dependency environment;
- `pip check`.

Require:

- Streamlit 1.61.1.
- `pip check` clean.

Emit:

`RELEASE 1.10 WP08 PYTHON ENVIRONMENT GATE: PASS`

---

# Phase 12 — Security gate

Use:

**Gitleaks 8.30.1**

Run:

`gitleaks git . --redact --verbose`

Report commit count scanned and result.

Require no unresolved leaks.

Also verify permanent security/cardinality tests pass.

Do not weaken execution policy or scanner behavior.

Emit:

`RELEASE 1.10 WP08 GITLEAKS SECURITY GATE: PASS`

---

# Phase 13 — Process/listener/UI residue gate

After validation verify no Release 1.10-owned:

- Worker process;
- testhost;
- Python child;
- Streamlit server;
- observability listener;
- temporary listener/server;
- locked canonical handoff temp file;
- validation residue.

Do not kill unrelated user processes.

Emit:

`RELEASE 1.10 WP08 PROCESS/LISTENER/UI RESIDUE: CLEAN`

---

# Phase 14 — Documentation acceptance

Verify WP07 documentation against current implementation/tests:

- architecture topology;
- System Health;
- no-bypass;
- developer setup;
- operational runbook;
- permanent test map;
- security/cardinality;
- provenance;
- no-exporter statement;
- local signing caveat.

Run:

`git diff --check`

Require clean.

Verify internal relative links/references named by WP07.

Emit:

`RELEASE 1.10 WP08 DOCUMENTATION ACCEPTANCE: PASS`

---

# Phase 15 — Release acceptance matrix

Map every Release 1.10 definition acceptance criterion and every WP01–WP08 acceptance criterion to concrete evidence.

Produce:

| Criterion | Source | Evidence | Result |
| --- | --- | --- | --- |

No criterion may be silently waived.

If a criterion is obsolete/contradictory, BLOCK for Luna reconciliation rather than self-amending policy.

Emit:

`RELEASE 1.10 WP08 RELEASE ACCEPTANCE MATRIX: PASS`

---

# Phase 16 — Candidate freeze

Once all validation gates pass, freeze the exact candidate path set.

Record:

- branch;
- pre-commit HEAD;
- exact Release 1.10 path list;
- exact diff summary;
- test counts;
- build result;
- Gitleaks result;
- environment versions;
- known environment-only warnings.

No further content mutation after freeze except an explicitly authorized readiness metadata correction that forces revalidation.

Emit:

`RELEASE 1.10 WP08 CANDIDATE FREEZE: PASS`

---

# Phase 17 — WP08 acceptance

Evaluate #249 plus Release 1.10 definition/plan/manifest.

Only when all gates pass emit:

`RELEASE 1.10 WP08 ACCEPTANCE: PASS`

No #249 closure or Git publication before this marker.

---

# Phase 18 — Mandatory WP08 GitHub lifecycle completion

Immediately after acceptance:

1. re-read #249;
2. verify Release=1.10;
3. verify milestone #59;
4. verify unique Project #2 item;
5. close #249 if Open;
6. ensure Project Status=Done only if not already Done.

If issue-close automation sets Done, do not redundantly mutate Status.

Count only explicit mutations.

Do not close milestone #59 in this phase.

Expected post-WP08 milestone issue state, absent independent changes:

**0 open / 8 closed**, milestone still Open.

Emit:

`RELEASE 1.10 WP08 GITHUB WORK-PACKAGE COMPLETION: PASS`

---

# Phase 19 — Git publication authority gate

Before any Git mutation, determine from binding planning/repository workflow whether WP08 itself is authorized to create the candidate commit/branch/push/PR.

## If explicitly authorized

Perform only the minimum workflow:

1. stage exact frozen Release 1.10 paths;
2. verify staged diff equals frozen candidate;
3. create one governed Release 1.10 candidate commit using repository convention;
4. record full commit SHA;
5. push only the approved branch;
6. create/update the Release 1.10 PR against the correct base;
7. record PR number/URL/state.

Do not merge.

Do not tag.

Do not publish GitHub Release.

Emit:

`RELEASE 1.10 WP08 GIT CANDIDATE PUBLICATION: PASS`

`RELEASE 1.10 WP08 PR READINESS: PASS`

## If NOT explicitly authorized

Do not invent Git authority.

Keep Git mutations ZERO.

Produce a complete PR-readiness handoff containing:

- exact frozen path set;
- proposed commit scope;
- proposed PR title/body facts;
- validation evidence;
- acceptance evidence;
- required next authority.

Emit:

`RELEASE 1.10 WP08 GIT CANDIDATE PUBLICATION: DEFERRED — EXPLICIT RELEASE PUBLICATION AUTHORITY REQUIRED`

`RELEASE 1.10 WP08 PR READINESS: PASS — PUBLICATION DEFERRED`

This is a successful WP08 outcome if #249's binding acceptance criteria require readiness rather than publication.

---

# Phase 20 — GitHub post-verification

Re-read:

- #249;
- #242–#248;
- milestone #59;
- Project #2.

Require:

- #242–#249 Closed/Done;
- #249 Release=1.10;
- milestone #59 still Open unless a separate explicit release-completion authority has already changed it;
- expected issue count 0 open / 8 closed absent independent changes.

Emit:

`RELEASE 1.10 WP08 GITHUB COMPLETION POST-VERIFY: PASS`

---

# Phase 21 — Final mutation ledger

Report exact mutation accounting.

## Repository
List every content path changed by WP08 itself, if any.

Separate pre-existing WP01–WP07 changes from WP08-created changes.

## Production/test/docs/planning
Report exact categories.

## Project/package/schema/signing
Report exact counts; expected ZERO unless frozen manifest explicitly authorizes otherwise.

## Git
Report exact:
- stage;
- commit;
- branch;
- push;
or ZERO/deferred.

## GitHub
Report exact:
- #249 close;
- Project Status mutation only if explicit;
- PR creation/update only if explicitly authorized.

Do not count automation as an explicit mutation.

Emit:

`RELEASE 1.10 WP08 MUTATION ACCOUNTING: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Phase 22 — Release-level downstream handoff

WP08 must end with a precise handoff for the next release-level authority.

If PR was published:

- candidate commit SHA;
- branch;
- PR number;
- base;
- validation evidence;
- #242–#249 lifecycle state;
- milestone state;
- explicit remaining actions:
  - PR acceptance/merge;
  - milestone closure;
  - version/tag;
  - GitHub Release;
  - post-merge verification,
  only as applicable to frozen Release 1.10 governance.

If publication was deferred:

- identify the exact next publication authority needed.

Emit:

`RELEASE 1.10 WP08 DOWNSTREAM RELEASE HANDOFF: PASS`

---

# Required success markers

`RELEASE 1.10 WP08 ENTRY: PASS`

`RELEASE 1.10 WP08 CONTRACT/HANDOFF CONSUMPTION: PASS`

`RELEASE 1.10 WP08 REPOSITORY/GITHUB ENTRY AUDIT: PASS`

`RELEASE 1.10 WP08 RELEASE PATH AUDIT: PASS`

`RELEASE 1.10 WP08 ARCHITECTURE ACCEPTANCE: PASS`

`RELEASE 1.10 WP08 CAPABILITY/NO-BYPASS ACCEPTANCE: PASS`

`RELEASE 1.10 WP08 PROVENANCE/TRUTHFULNESS ACCEPTANCE: PASS`

`RELEASE 1.10 WP08 SCHEMA/HANDOFF COMPATIBILITY: PASS`

`RELEASE 1.10 WP08 PACKAGE/DEPENDENCY AUDIT: PASS`

`RELEASE 1.10 WP08 PERMANENT OBSERVABILITY/NO-BYPASS TESTS: PASS`

`RELEASE 1.10 WP08 FULL DOTNET REGRESSION: PASS`

`RELEASE 1.10 WP08 FULL PYTHON REGRESSION: PASS`

`RELEASE 1.10 WP08 BUILD GATE: PASS`

`RELEASE 1.10 WP08 PYTHON ENVIRONMENT GATE: PASS`

`RELEASE 1.10 WP08 GITLEAKS SECURITY GATE: PASS`

`RELEASE 1.10 WP08 PROCESS/LISTENER/UI RESIDUE: CLEAN`

`RELEASE 1.10 WP08 DOCUMENTATION ACCEPTANCE: PASS`

`RELEASE 1.10 WP08 RELEASE ACCEPTANCE MATRIX: PASS`

`RELEASE 1.10 WP08 CANDIDATE FREEZE: PASS`

`RELEASE 1.10 WP08 ACCEPTANCE: PASS`

`RELEASE 1.10 WP08 GITHUB WORK-PACKAGE COMPLETION: PASS`

One of:

`RELEASE 1.10 WP08 GIT CANDIDATE PUBLICATION: PASS`

or

`RELEASE 1.10 WP08 GIT CANDIDATE PUBLICATION: DEFERRED — EXPLICIT RELEASE PUBLICATION AUTHORITY REQUIRED`

One of:

`RELEASE 1.10 WP08 PR READINESS: PASS`

or

`RELEASE 1.10 WP08 PR READINESS: PASS — PUBLICATION DEFERRED`

Then:

`RELEASE 1.10 WP08 GITHUB COMPLETION POST-VERIFY: PASS`

`RELEASE 1.10 WP08 MUTATION ACCOUNTING: PASS`

`RELEASE 1.10 WP08 DOWNSTREAM RELEASE HANDOFF: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 WP08 — FULL VALIDATION, ACCEPTANCE & PR READINESS AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- Release 1.10 changed-path ownership is non-deterministic;
- an acceptance criterion is unresolved/contradictory;
- validation fails and repair exceeds frozen scope;
- package/schema/architecture changes would be needed;
- security gate fails;
- provenance/capability truthfulness cannot be established;
- #249 lifecycle cannot be safely resolved;
- PR readiness cannot be truthfully established.

Do not use WP08 to redesign Release 1.10.

If blocked before acceptance:

- #249 remains Open/Backlog;
- do not perform Git publication;
- do not close milestone;
- preserve valid evidence and authorized repairs;
- report minimum next authority.

If blocked after #249 acceptance/lifecycle completion but before publication, preserve truthful lifecycle state and report the publication blocker without reopening #249 unless governance explicitly requires it.

Exact blocked terminal:

`RELEASE 1.10 WP08 — FULL VALIDATION, ACCEPTANCE & PR READINESS AUTHORITY BLOCKED`

# AIQuantTradingResearch — Release 1.9 GitHub Planning — Codex Authority

## 1. Mission

Materialize the already human-accepted Release 1.9 definition into GitHub planning state for:

`samuel-santos-engineer/AIQuantTradingResearch`

Release:

**Release 1.9 — Real-Time Financial Data Visualization**

This authority is strictly a **GitHub planning/reconciliation authority**.

It must translate the accepted Release 1.9 definition and execution plan into:

- milestone #58 issue membership;
- exactly 12 governed work-package issues;
- Project #2 membership;
- governed Project field values;
- an exact linear dependency chain.

It does NOT authorize implementation.

---

## 2. Authoritative Predecessor

Roadmap Reconciliation / Release Sequencing immutable `main` boundary:

`3a02f035a253e4e16f479e1866c9a5195f5cfbdb`

Before mutation, verify this boundary is an ancestor of current `main` and reconcile current repository state with the accepted Release 1.9 planning artifacts.

Do not substitute the older Release 1.8 boundary.

---

## 3. Human-Accepted Release 1.9 Definition

The following planning artifacts are authoritative and must be read completely before any GitHub mutation:

- `docs/roadmap/release-1.9/RELEASE_1.9_DEFINITION.md`
- `docs/roadmap/release-1.9/RELEASE_1.9_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.9/RELEASE_1.9_FILE_MANIFEST.md`

The execution plan defines exactly 12 linear work packages.

Do not redesign, merge, split, rename, reorder, or reinterpret those WPs except for mechanical GitHub title/body formatting that preserves their exact semantics.

If these three artifacts disagree materially, stop.

---

## 4. Accepted Release 1.9 Scope

Preserve the frozen scope:

- deterministic simulated/replayed observations behind `IObservationSource`;
- reuse of the existing five-stage pipeline;
- no parallel pipeline;
- Application-owned bounded presentation read model;
- Streamlit as outer presentation adapter;
- no Streamlit direct SQLite/provider access;
- financial visualization;
- `simple-return-lag-1-v1` visualization including warm-up semantics;
- supported snapshot/data-quality visualization;
- schema v3 preserved;
- exact governed dependencies preserved;
- existing Python/.NET interoperability governance preserved;
- no ML;
- no OpenTelemetry/System Health;
- no Explainable AI;
- no Backtesting.

This authority may not change these decisions.

---

## 5. Mandatory Starting-State Gate

Before mutation verify:

### Repository

- repository is `samuel-santos-engineer/AIQuantTradingResearch`;
- current branch and HEAD;
- `origin/main`;
- roadmap predecessor boundary is present;
- accepted Release 1.9 definition artifacts are present/readable;
- no merge/rebase/cherry-pick/revert in progress;
- staged paths: 0;
- no unexplained tracked changes.

Planning/authority Markdown may be untracked if it is intentionally being used as execution input. Report it explicitly.

Do not mutate repository content under this authority.

### GitHub

Verify:

- authenticated account;
- required `project` scope;
- milestone #58 exists and is OPEN;
- #58 title/scope is Release 1.9 Real-Time Financial Data Visualization;
- #58 has no unexpected existing issues;
- #59, #60, #50, #51, #61 preserve the canonical future sequence;
- #49 and #56 remain unchanged;
- Project #2 is accessible;
- Project #2 has required fields:
  - Status;
  - Priority;
  - Release;
  - Area;
- Release field contains exactly one usable `1.9` option;
- no existing Release 1.9 WP set conflicts with the accepted execution plan.

If GitHub API rate limiting prevents authoritative read-back, stop before mutation.

---

## 6. Milestone Authority

Use existing milestone:

**#58 — Release 1.9: Real-Time Financial Data Visualization**

Do not create another Release 1.9 milestone.

Do not modify milestone #58 title/description unless a purely mechanical discrepancy prevents faithful use and is explicitly proven safe. Otherwise stop for corrective authority.

Do not mutate:

- #49;
- #50;
- #51;
- #56;
- #59;
- #60;
- #61.

---

## 7. Work-Package Source of Truth

Derive the exact WP titles, objectives, scope, non-goals, dependencies, artifacts, verification, completion evidence, and stop conditions from:

`RELEASE_1.9_EXECUTION_PLAN.md`

Expected concern sequence, subject to exact authoritative wording in that file:

1. WP01 — Release & Repository Preflight
2. WP02 — Simulated Live Provider / Replay Semantics
3. WP03 — Incremental Pipeline Orchestration
4. WP04 — Presentation Read-Model Contract
5. WP05 — Streamlit Application Foundation
6. WP06 — Financial Visualization
7. WP07 — Feature & Data-Quality Visualization
8. WP08 — Lifecycle, Resilience & Determinism
9. WP09 — Permanent Automated Tests
10. WP10 — Architecture, Documentation & Developer Execution Alignment
11. WP11 — Full Validation, Integration & Acceptance
12. WP12 — Closure & PR Readiness

These labels are expectations only.

If the accepted execution plan uses different exact titles, the execution plan wins.

Do not force these expected titles over the authoritative document.

---

## 8. Exactly 12 Issues

After reconciliation, milestone #58 must contain exactly 12 governed Release 1.9 WP issues corresponding one-to-one with WP01–WP12.

Do not create:

- umbrella issue;
- planning issue;
- release checklist issue;
- bug placeholder;
- Release 1.10 issue;
- implementation issue outside the 12 accepted WPs.

If unexpected issues already exist in #58, inspect them before mutation.

Never delete or repurpose an unexpected issue without separate authority.

---

## 9. Issue Title Rule

Use the exact WP number and authoritative title from `RELEASE_1.9_EXECUTION_PLAN.md`.

Format consistently with established repository WP conventions, for example:

`WP01 — <authoritative title>`

Do not add Release 1.9 redundantly to every title unless repository convention requires it.

WP identity is the combination of:

- release;
- WP number;
- authoritative title.

---

## 10. Issue Body Rule

Each issue body must faithfully translate the corresponding accepted WP.

Include, using repository conventions where possible:

- release;
- WP number;
- objective;
- scope;
- explicit non-goals;
- predecessor/dependency;
- affected architectural areas;
- expected artifacts;
- verification;
- completion evidence;
- stop conditions;
- reference to the accepted Release 1.9 planning artifacts.

Do not invent implementation details absent from the accepted plan.

Do not weaken stop conditions.

---

## 11. Assignment

Assign all 12 WP issues to:

`samuel-santos-engineer`

unless live repository governance proves another established assignment rule.

Do not assign additional users.

---

## 12. Project #2 Membership

Every Release 1.9 WP issue must be present exactly once in Project #2.

Before adding an issue:

1. search Project #2 for that exact issue;
2. if already present, reuse the existing Project item;
3. never call add blindly;
4. if add reports `Content already exists in this project`, recover by reading the existing item rather than treating that alone as fatal;
5. prove exactly one Project item represents the issue.

This rule is specifically intended to make partial-state recovery idempotent.

---

## 13. Required Project Fields

For all 12 Release 1.9 WP items reconcile:

- `Status = Backlog`
- `Priority = P1`
- `Release = 1.9`
- `Area = <authoritative evidence-derived area>`

If the accepted execution plan explicitly governs a different Priority or Status, the accepted plan wins.

Do not create new Status/Priority/Area options.

Do not edit Release taxonomy unless `1.9` is unexpectedly absent; if absent, stop for narrow corrective authority.

---

## 14. Area Reconciliation

Determine Area from the accepted WP semantics and existing Project #2 Area taxonomy.

Do not invent Area options.

Read existing Area values first.

Use the narrowest existing authoritative Area matching each WP.

Examples of likely mappings, not mandates:

- repository/preflight → Engineering or Architecture;
- provider/replay → Data or Infrastructure;
- pipeline orchestration → Application/Data;
- read model → Application/Architecture;
- Streamlit foundation → AI or Presentation if such governed Area exists;
- visualization → AI/Presentation/Data according to existing taxonomy;
- lifecycle/resilience → Infrastructure;
- tests → Testing/Engineering if available;
- architecture/docs → Architecture;
- acceptance/closure → Engineering/Architecture.

The actual existing Project taxonomy and accepted WP ownership win.

Report the final Area matrix.

If an WP has no defensible existing Area, stop rather than create one.

---

## 15. Exact Dependency Chain

The accepted Release 1.9 execution plan is linear.

After reconciliation, establish exactly these semantic dependencies:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12`

Interpret:

- WP02 depends on WP01;
- WP03 depends on WP02;
- ...
- WP12 depends on WP11.

Required Release 1.9 dependency edge count:

**11**

Do not create:

- cross-release dependencies;
- fan-out/fan-in edges;
- duplicate edges;
- dependencies to milestone #59 or later releases.

If GitHub's dependency mechanism cannot be safely verified, stop.

---

## 16. Idempotent Partial-State Recovery

This authority must be safe to resume after interruption.

Before every issue create:

- search by issue number if known;
- search milestone #58;
- search exact WP identity/title;
- inspect Project #2.

Classify each WP as one of:

- ABSENT;
- ISSUE_ONLY;
- ISSUE_AND_PROJECT_PARTIAL;
- FULLY_RECONCILED;
- CONFLICTING.

Actions:

### ABSENT

Create issue, then reconcile Project membership/fields/dependency.

### ISSUE_ONLY

Reuse issue; do not create duplicate. Add/reconcile Project state.

### ISSUE_AND_PROJECT_PARTIAL

Reuse both issue and Project item. Reconcile missing/incorrect fields/dependency.

### FULLY_RECONCILED

Do nothing except verify.

### CONFLICTING

Stop for corrective authority.

Never delete/recreate a WP merely to obtain a clean state.

---

## 17. Mutation Order Per WP

Process WP01 through WP12 sequentially.

For each WP:

1. read authoritative WP definition;
2. discover existing issue;
3. create only if absent;
4. verify issue title/body/assignee/milestone/state;
5. discover existing Project item;
6. add only if absent;
7. reconcile Status;
8. reconcile Priority;
9. reconcile Release;
10. reconcile Area;
11. create predecessor dependency if WP02+ and absent;
12. read back issue;
13. read back Project fields;
14. read back dependency;
15. only then advance.

If mandatory read-back fails because of API rate limiting, stop immediately and preserve partial state.

---

## 18. Issue State

After GitHub planning completes:

- all WP01–WP12 issues must be OPEN;
- all Project Status values must be Backlog;
- milestone #58 must remain OPEN;
- no WP is Done;
- no implementation has started.

Planning does not imply execution.

---

## 19. Repository Mutation Prohibition

Under this authority, do not:

- modify production code;
- modify tests;
- modify Release 1.9 definition artifacts;
- modify schema;
- modify package declarations;
- modify `.venv`;
- install packages;
- create implementation files;
- create release branch implementation changes;
- commit/push repository changes.

The planning authority files themselves are execution inputs and may remain untracked if that is the repository's established authority-input workflow.

If repository governance now requires committing these authority files before GitHub planning, stop and report the conflict rather than silently expanding scope.

---

## 20. Release 1.9 Implementation Invariant

Preserve:

> Release implementation must occur on a dedicated release/working branch. Completion and acceptance do not authorize direct integration into `main`. After acceptance, all governed release artifacts—including documentation—must be committed to the release branch, a PR must be opened against `main`, required verification must pass on the PR candidate, and only then may the PR be merged. The resulting `main` merge SHA becomes the immutable release repository boundary.

This GitHub planning authority does not create or execute the implementation branch.

WP01 execution will later require its own explicit authority.

---

## 21. Foundational Technology Governance

Preserve:

> Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.

Do not introduce a new foundational technology during GitHub planning.

---

## 22. Scope Leakage Gates

No WP issue may authorize:

### Release 1.10

- OpenTelemetry implementation;
- System Health dashboard;
- telemetry backend/exporter/collector.

### Release 2.0+

- model training;
- Logistic Regression;
- prediction/confidence;
- Explainable AI;
- Backtesting.

### Other

- paid market data;
- broker/order execution;
- cloud deployment;
- schema evolution without separate authority.

If the accepted WP text appears to leak scope, stop instead of silently editing the definition.

---

## 23. GitHub Read-Back Acceptance

After WP12 is reconciled, perform a complete authoritative read-back.

Prove:

### Milestone #58

- OPEN;
- exactly 12 open governed issues;
- 0 closed;
- WP01–WP12 each exactly once.

### Issues

For each:

- OPEN;
- assigned correctly;
- milestone #58;
- exact authoritative title;
- body consistent with accepted plan.

### Project #2

Exactly 12 Release 1.9 WP items corresponding to those issues.

Every item:

- Status Backlog;
- Priority P1 unless explicitly governed otherwise;
- Release 1.9;
- authoritative Area.

### Dependencies

Exactly 11 Release 1.9 WP-chain edges with no missing/duplicate/unexpected edge.

---

## 24. Preservation Read-Back

Verify no mutation to:

- milestone #49;
- milestone #50;
- milestone #51;
- milestone #56;
- milestone #59;
- milestone #60;
- milestone #61;
- historical Release Project assignments;
- Project Release taxonomy;
- repository/Git state;
- Python environment;
- packages;
- schema;
- implementation.

Report any unavoidable GitHub metadata timestamps as metadata, not semantic mutation.

---

## 25. Planning Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE:

- GP1 — repository/predecessor state reconciled;
- GP2 — three accepted Release 1.9 planning artifacts read completely;
- GP3 — #58 identity/state verified;
- GP4 — future/historical milestones preserved;
- GP5 — Project #2 required fields/options verified;
- GP6 — exactly 12 authoritative WPs derived without redesign;
- GP7 — WP01 issue/project fields reconciled;
- GP8 — WP02 issue/project fields/dependency reconciled;
- GP9 — WP03 reconciled;
- GP10 — WP04 reconciled;
- GP11 — WP05 reconciled;
- GP12 — WP06 reconciled;
- GP13 — WP07 reconciled;
- GP14 — WP08 reconciled;
- GP15 — WP09 reconciled;
- GP16 — WP10 reconciled;
- GP17 — WP11 reconciled;
- GP18 — WP12 reconciled;
- GP19 — exact 12-item / 11-edge final state proven;
- GP20 — zero implementation/repository/package/schema/Release-1.10 mutation proven.

All applicable gates must PASS.

---

## 26. API Rate-Limit Discipline

Before beginning mutation, inspect relevant GitHub API rate-limit capacity if available.

During execution:

- minimize redundant GraphQL calls;
- cache stable Project field IDs/options within the execution;
- do not repeatedly rediscover unchanged taxonomy;
- use targeted read-backs;
- preserve correctness over call minimization.

If remaining capacity becomes insufficient to perform the next mutation plus mandatory verification, stop before that mutation.

Never trade away authoritative read-back merely to finish more WPs.

---

## 27. Stop Conditions

Stop with:

`RELEASE 1.9 GITHUB PLANNING BLOCKED`

if:

- predecessor boundary cannot be reconciled;
- planning artifacts are absent/inconsistent;
- #58 conflicts with accepted definition;
- unexpected #58 issues cannot be safely classified;
- exact 12-WP mapping cannot be proven;
- Project `1.9` Release option is absent;
- required Area cannot be mapped;
- duplicate/conflicting WP issue exists;
- dependency state is ambiguous;
- GitHub API capacity prevents mandatory read-back;
- repository mutation becomes necessary;
- implementation would be required;
- scope leaks into later releases.

Report:

- exact blocker;
- mutations already completed;
- current WP partial-state classifications;
- preserved state;
- smallest corrective authority.

Do not clean up partial state automatically.

---

## 28. Required Execution Report

Report:

### Starting State

- repository/branch/HEAD/origin;
- predecessor reconciliation;
- Git status;
- milestone #58;
- Project #2 fields/options;
- API capacity.

### WP Matrix

For WP01–WP12 report:

- issue number;
- title;
- state;
- assignee;
- milestone;
- Project membership;
- Status;
- Priority;
- Release;
- Area;
- predecessor dependency;
- final classification.

### Final GitHub State

- #58 open/closed counts;
- exact issue set;
- Project item count;
- dependency edge count.

### Preservation

- #49/#50/#51/#56/#59/#60/#61;
- Project taxonomy;
- historical assignments;
- repository/Git;
- schema/packages/Python.

### GP1–GP20

Report every gate.

---

## 29. Completion Boundary

Successful GitHub planning authorizes only:

**WP01 — Release & Repository Preflight**

It does not authorize WP02+ automatically.

WP01 must receive its own explicit execution authority.

The implementation branch policy must be applied before Release 1.9 implementation mutation.

---

## 30. Success Markers

On complete success end exactly:

`RELEASE 1.9 GITHUB PLANNING COMPLETE`

`MILESTONE #58: OPEN — 12 WORK PACKAGES PLANNED`

`PROJECT #2 RELEASE 1.9: 12/12 ITEMS RECONCILED`

`RELEASE 1.9 DEPENDENCY CHAIN: 11/11 EDGES VERIFIED`

`RELEASE 1.9 IMPLEMENTATION: NOT STARTED`

`NEXT AUTHORIZED WORK PACKAGE: WP01 — <exact authoritative WP01 title>`

Do not begin WP01 automatically.

If blocked end exactly:

`RELEASE 1.9 GITHUB PLANNING BLOCKED`

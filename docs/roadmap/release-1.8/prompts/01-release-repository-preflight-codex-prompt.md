# Release 1.8 WP01 — Release & Repository Preflight — Codex Authority

## 1. Mission

Execute Release 1.8 WP01 — **Release & Repository Preflight** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#211`

Authoritative Release 1.8 planning baseline:

`651c45bd0df0b717b2bb5ad272ec8c890612fb6d`

Historical closed predecessor:

`f8e521af2c5262d6cc173d0731b5e915dbceac0a`

Authoritative milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Release 1.8 planning state at authorization:

- WP01–WP13: issues #211–#223;
- all 13 issues: OPEN;
- all 13 assigned to `samuel-santos-engineer`;
- all 13 assigned to milestone #56;
- Project #2 membership: 13/13;
- duplicate Project items: 0;
- Status: Backlog 13/13;
- Priority: P1 13/13;
- Release: 1.8 13/13;
- Areas: reconciled;
- dependency chain: exact 12-edge linear chain;
- milestone #56: OPEN, 13 open / 0 closed.

WP01 is a **preflight-only** work package.

It must establish the exact Release 1.8 starting state before Python compatibility research or machine/runtime changes begin.

---

## 2. Authoritative Inputs

Read completely before any mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- current repository engineering/governance documentation relevant to build/test/security/environment setup;
- current `eng/` scripts;
- current `.gitignore`;
- current VS Code workspace configuration if repository-owned;
- current Python-related repository files, if any;
- GitHub issue #211;
- milestone #56;
- Project #2 Release 1.8 items and dependency chain.

Treat the three Release 1.8 planning files as human-accepted authority.

Do not reinterpret their scope.

---

## 3. Frozen Starting Baselines

### Release 1.7 Historical Predecessor

Release 1.7 remains closed at:

- commit:
  `f8e521af2c5262d6cc173d0731b5e915dbceac0a`
- tree:
  `880f7fff6a9b946a310d32e17c1c803ca6c1a286`
- schema: v3
- permanent test baseline: 268/268.

### Release 1.8 Planning Baseline

Ongoing Release 1.8 execution begins from:

`651c45bd0df0b717b2bb5ad272ec8c890612fb6d`

Require:

- branch: `main`;
- local HEAD: same SHA;
- `origin/main`: same SHA;
- ahead/behind: `0/0`.

Do not reset to the Release 1.7 SHA.

---

## 4. WP01 Scope

WP01 SHALL verify:

1. repository identity and Git synchronization;
2. Release 1.8 planning artifacts and GitHub planning state;
3. Release 1.7 predecessor closure;
4. canonical .NET build/test/security baseline;
5. schema v3 preservation;
6. dependency/package/project/reference baseline;
7. current Python-related repository state;
8. current workstation Python visibility only as an observation, not as an installation step;
9. absence of premature Release 1.8 implementation;
10. absence of Release 1.9 implementation;
11. process/environment cleanliness;
12. readiness for WP02.

WP01 must not install, upgrade, configure, or remove Python.

---

## 5. Explicit Non-Goals

WP01 does not authorize:

- Python version selection;
- Python installation;
- Python PATH changes;
- Python launcher changes;
- pip upgrades;
- virtual environment creation;
- Python package installation;
- NumPy/pandas/SciPy/scikit-learn/Streamlit installation;
- VS Code Python extension installation or configuration;
- `.NET ↔ Python` architecture decisions;
- interoperability implementation;
- Streamlit application creation;
- production/test implementation changes;
- dependency-file creation;
- `.gitignore` changes;
- Release 1.9 work.

Those belong to later WPs.

---

## 6. Mandatory Starting-State Gate

Before any GitHub lifecycle mutation verify:

### Repository

- remote repository is correct;
- branch: `main`;
- HEAD:
  `651c45bd0df0b717b2bb5ad272ec8c890612fb6d`;
- `origin/main`: same;
- ahead/behind: `0/0`;
- staged paths: 0;
- unstaged tracked paths: 0;
- merge/rebase/cherry-pick/revert in progress: none;
- conflict markers: 0.

### Planning Artifacts

- all three Release 1.8 planning files exist;
- all are readable;
- human-accepted status is present;
- WP01–WP13 sequence is consistent;
- manifest and execution plan are consistent.

### GitHub

- milestone #56: OPEN, 13 open / 0 closed;
- issues #211–#223 exist;
- Project #2 membership: 13/13;
- duplicates: 0;
- Backlog/P1/Release 1.8/Area: 13/13 correct;
- dependency chain: exactly 12 edges;
- #211: OPEN / Backlog;
- #212–#223: OPEN / Backlog.

### Historical State

- milestone #49 remains CLOSED and empty;
- milestone #55 remains CLOSED;
- issues #197–#209 remain closed/done;
- Release 1.9 and 2.0 planning milestones remain unchanged.

If any state differs materially, stop before mutation and report the smallest corrective authority required.

---

## 7. Canonical .NET Verification

Run the repository's canonical verification from the Release 1.8 planning baseline.

Expected baseline from Release 1.7:

- Domain.Tests: 11;
- Application.Tests: 119;
- Infrastructure.Tests: 125;
- Architecture.Tests: 13;
- total: 268;
- skipped: 0.

Require:

- restore: PASS;
- build: PASS;
- warnings/errors: 0/0;
- Domain.Tests: 11/11;
- Application.Tests: 119/119;
- Infrastructure.Tests: 125/125;
- Architecture.Tests: 13/13;
- permanent total: 268/268;
- skipped: 0;
- formatting: PASS;
- Gitleaks: PASS;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS.

If the current executed test counts differ, stop and reconcile the actual repository state rather than assuming 268.

---

## 8. Schema and Structural Baseline

Verify:

- schema version: v3;
- `experiment_results` remains present and valid;
- table delta from Release 1.7 accepted state: 0;
- column delta: 0;
- index delta: 0;
- migration delta: 0;
- package delta: 0;
- project delta: 0;
- production project-reference delta: 0;
- dependency graph remains acyclic.

WP01 does not authorize structural changes.

---

## 9. Python Repository-State Inventory

Inspect the repository for Python-related content without modifying it.

Inventory at minimum:

- `.py` files;
- Python dependency manifests/configuration;
- `.venv`/`venv` references;
- Python ignore rules;
- Python-related `eng/` scripts;
- Streamlit files/configuration;
- VS Code repository-owned Python settings;
- Python-related CI/workflow content;
- ML/scientific-library references;
- any Python interoperability code.

Classify each as:

- pre-existing accepted repository content;
- Release 1.8 planning content;
- unexpected/premature implementation;
- local-only residue.

Expected result:

- no premature Release 1.8 production implementation;
- no Release 1.9 ML implementation.

Do not create or delete Python content in WP01.

---

## 10. Workstation Python Observation

WP01 may perform **read-only environment discovery** only.

If commands are available, record:

- whether `python` resolves;
- whether `py` resolves;
- resolved executable path(s);
- reported version(s);
- whether `pip` is visible;
- whether `venv` appears available.

This is observational evidence only.

Do not:

- choose the authoritative Python version;
- install Python;
- upgrade Python;
- modify PATH;
- modify launcher configuration;
- install packages;
- create a venv.

A missing Python runtime is not a WP01 failure unless the planning artifacts explicitly require it before WP02. It should be reported as an expected prerequisite for later WPs.

---

## 11. VS Code / PowerShell Observation

Inventory only:

- current PowerShell version/context;
- whether VS Code repository settings exist;
- whether any repository-owned interpreter path is hard-coded;
- whether Python-related extension recommendations/settings are already present.

Do not modify user settings, workspace settings, extensions, PATH, or shell profiles.

---

## 12. Security and Isolation Baseline

Require:

- real credentials in repository: 0;
- secret findings: 0;
- provider/network product calls during WP01: 0;
- Python package/network installation activity: 0;
- temporary databases/process residue: 0;
- untracked virtual environments/caches inside the repository: 0 unless already expected and ignored, in which case report them precisely.

Do not run external market-data provider acquisition.

---

## 13. Premature Implementation Detection

Search for Release 1.8/1.9 implementation that should not exist yet.

Specifically verify absence of newly introduced:

- production Python capability layer;
- Streamlit product interface;
- `.NET ↔ Python` adapter;
- ML training code;
- trained models;
- model registry;
- ML persistence/schema;
- prediction/inference;
- Release 1.9 branch/PR/implementation.

If found, stop and report exact paths/state.

Do not delete or repair under WP01.

---

## 14. Process-Level Validation Prerequisite

Read the engineering playbook's process-level validation prerequisite.

For Release 1.8, confirm planning has identified or deferred appropriately:

- repository-native fixture approach for later interop validation;
- cleanup/residue expectations;
- use of existing deterministic evidence where possible;
- no uncontrolled temporary external project creation.

WP01 does not invent the WP10/WP11 fixture mechanism.

It only verifies the planning prerequisite is represented and no blocker is already visible.

---

## 15. Release 1.8 Planning Integrity

Reconcile the authoritative sequence:

`WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08 → WP09 → WP10 → WP11 → WP12 → WP13`

Verify:

- exactly 13 WPs;
- no WP14+;
- dependency edges: 12;
- issue numbering: #211–#223;
- all issues belong to milestone #56;
- WP02 remains blocked by WP01;
- no later WP has been transitioned to In Progress prematurely.

---

## 16. Repository Mutation Budget

Expected WP01 repository-content delta:

`0`

Expected test delta:

`0`

Expected schema delta:

`0`

Expected package/project/reference delta:

`0/0/0`

Expected Python environment mutation:

`0`

Expected Git transport mutation:

`0`

Disposable read-only diagnostics are allowed if they leave residue 0.

---

## 17. GitHub Lifecycle

Only after every WP01 gate passes:

1. transition #211 from Backlog to In Progress if needed;
2. post concise completion evidence to #211;
3. close #211;
4. set #211 Project Status to Done.

Final required GitHub state:

- #211: CLOSED / Done;
- #212–#223: OPEN / Backlog;
- milestone #56: OPEN, 12 open / 1 closed;
- Project membership: 13/13;
- duplicates: 0;
- Priority/Release/Area: unchanged;
- dependency chain: unchanged.

Do not transition #212 automatically.

---

## 18. Stop Conditions

Stop with:

`RELEASE 1.8 WP01 BLOCKED`

if:

- repository baseline differs materially;
- planning artifacts are inconsistent;
- GitHub planning state is inconsistent;
- Release 1.7 predecessor state is unexpectedly altered;
- canonical .NET verification fails;
- test counts differ unexpectedly;
- schema/package/project/reference drift exists;
- premature Release 1.8 implementation exists;
- Release 1.9 implementation exists;
- security/isolation baseline fails;
- WP01 would require Python installation or configuration to pass.

Report the exact blocker and smallest corrective authority required.

---

## 19. Required Execution Report

Report:

### Starting State

- repository/remote;
- branch;
- HEAD/origin SHA;
- ahead/behind;
- staged/unstaged state.

### Planning/GitHub

- milestone #56;
- #211–#223;
- Project #2 membership;
- fields;
- dependency edges;
- predecessor/historical milestones.

### Canonical Verification

- restore;
- build;
- warnings/errors;
- Domain/Application/Infrastructure/Architecture counts;
- total;
- skipped;
- formatting;
- Gitleaks;
- diff checks.

### Structural Baseline

- schema;
- tables/columns/indexes/migrations;
- packages/projects/references;
- dependency graph.

### Python/Environment Inventory

- repository Python artifacts;
- machine Python visibility;
- PowerShell/VS Code observations;
- premature implementation findings.

### Isolation

- provider/network product calls;
- credentials;
- residue.

### Mutation Accounting

- repository;
- Git;
- GitHub;
- Python environment.

### Final State

- #211 lifecycle;
- milestone counts;
- next authorized WP.

---

## 20. Completion Markers

On success end exactly:

`RELEASE 1.8 WP01 COMPLETE`

`NEXT AUTHORIZED WORK PACKAGE: WP02 — Python Runtime Compatibility & Version Selection — GitHub issue #212`

Do not execute WP02 automatically.

If blocked end exactly:

`RELEASE 1.8 WP01 BLOCKED`

# Release 1.8 WP06 — Python Dependency Governance & Environment Rules — Codex Authority

## 1. Mission

Execute Release 1.8 WP06 — **Python Dependency Governance & Environment Rules** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#216`

Milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established state:

- #211–#215: CLOSED / Done;
- #216: OPEN / Backlog;
- #217–#223: OPEN / Backlog;
- milestone #56: OPEN, 8 open / 5 closed;
- official machine runtime: CPython 3.13.15 amd64;
- repository environment: `.venv`, Python 3.13.15, isolated;
- `.venv/` is Git-ignored;
- Microsoft VS Code Python tooling is governed and installed;
- no project third-party Python packages are installed;
- NumPy, pandas, scikit-learn, and Streamlit have individual engineering selection records;
- exact package versions were previously deferred pending dependency governance;
- canonical .NET baseline: 268/268;
- schema: v3.

WP06 establishes the authoritative rules and reproducible dependency mechanism that all Release 1.8 Python packages must obey.

WP06 is governance first. Install scientific/ML/UI libraries only if the accepted Release 1.8 planning artifacts explicitly assign concrete dependency selection/installation to WP06. Otherwise define and validate the mechanism without prematurely populating the environment.

---

## 2. Foundational Technology Rule

Preserve the standing rule:

> Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.

No dependency may enter the project merely because it is popular or transitively convenient.

Existing selection records for Python, NumPy, pandas, scikit-learn, Streamlit, and VS Code Python tooling remain authoritative within their scopes.

---

## 3. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`
- `docs/architecture/implementation/VSCODE_PYTHON_EXTENSION_SELECTION.md`
- individual NumPy selection record;
- individual pandas selection record;
- individual scikit-learn selection record;
- individual Streamlit selection record;
- WP05 environment-foundation evidence;
- `.gitignore`;
- existing dependency/versioning/security/engineering documentation;
- GitHub issue #216;
- Project #2 state.

Use actual canonical filenames if the selection-record filenames differ.

---

## 4. Mandatory Starting-State Gate

Verify before mutation:

### Repository

- correct repository/remote;
- branch `main`;
- local HEAD equals `origin/main`;
- ahead/behind `0/0`;
- staged paths 0;
- no unexplained tracked changes;
- WP05-governed `.gitignore` state exists;
- `.venv` is ignored.

### GitHub

- #211–#215: CLOSED / Done;
- #216: OPEN / Backlog;
- #217–#223: OPEN / Backlog;
- milestone #56: OPEN, 8 open / 5 closed;
- Project membership 13/13;
- duplicates 0;
- WP05→WP06 dependency exists;
- authoritative fields/dependency chain preserved.

### Python

Inside `.venv`, prove:

- Python 3.13.15;
- x64;
- isolation active;
- base prefix points to governed machine runtime;
- local pip works;
- system site packages are not inherited;
- NumPy/pandas/scikit-learn/Streamlit are not installed;
- no unexplained dependency manifests/lock files already exist.

Stop on ambiguous pre-existing dependency state.

---

## 5. Dependency Ownership Model

Establish explicit ownership boundaries:

### Machine scope

May contain only the governed Python runtime and normal runtime/bootstrap tooling.

Project libraries must not be installed globally.

### Repository scope

Tracked dependency declarations and governance belong to the repository.

### `.venv` scope

Installed project dependencies belong only inside the repository-local ignored `.venv`.

### Developer tooling scope

VS Code extensions remain development tooling and are not Python application dependencies.

### Production/application architecture

Python packages must not silently become .NET package dependencies or alter the existing .NET project/reference graph.

Document these boundaries durably.

---

## 6. Installation Command Rule

Establish the canonical project installation form as interpreter-qualified execution:

`.venv\Scripts\python.exe -m pip ...`

or a portable activated-venv equivalent whose interpreter provenance is proven.

Prefer `python -m pip` over ambiguous bare `pip`.

Explicitly prohibit for project dependencies:

- machine/global `pip install`;
- `py -m pip install` when it targets the machine interpreter;
- user-site installation;
- `--user`;
- `sudo`-style elevation;
- installation outside `.venv`.

Document how a developer proves the active interpreter before dependency mutation.

---

## 7. Dependency Declaration Strategy

Determine the declaration mechanism already governed by the Release 1.8 planning artifacts.

Do not introduce Poetry, uv, Conda, pipenv, Hatch, PDM, or another dependency manager unless explicitly selected under separate foundational-tool governance.

If the plan uses standard pip-compatible files, establish a simple, transparent model.

A reasonable model may distinguish:

- direct/top-level project dependencies;
- exact reproducible resolved environment;
- optional development/test dependencies if later required.

Do not create unnecessary layers.

Follow `RELEASE_1.8_FILE_MANIFEST.md` exactly for filenames.

---

## 8. Direct vs Transitive Dependencies

Establish the rule:

- direct dependencies are explicitly selected capabilities used by project code/tooling;
- transitive dependencies are implementation details of selected direct packages unless they independently cross an architectural/foundational boundary;
- transitive packages are captured for reproducibility where the chosen mechanism requires it;
- transitive presence alone does not create architectural permission to use that package directly.

Project code must not begin depending directly on a transitive library without explicit declaration and, where foundational, a selection record.

---

## 9. Version Policy

Define explicit version semantics for Python dependencies.

The policy must address:

- compatibility with Python 3.13;
- reproducibility;
- security/maintenance updates;
- intentional upgrades;
- direct versus transitive version constraints;
- prevention of accidental floating upgrades;
- how dependency changes are reviewed;
- when selection records must be revisited.

If WP06 is the planning-authorized point for exact versions, determine exact versions from authoritative compatibility evidence and reconcile each existing selection record.

If exact version selection belongs to a later WP, preserve deferral and define the rule that later selection must follow.

Never invent package versions.

---

## 10. Reproducibility Rule

A clean developer environment must ultimately be reconstructable from:

1. governed Python runtime policy;
2. repository dependency declarations;
3. documented commands;
4. no reliance on global scientific/ML/UI packages.

Define how reproducibility will be proven.

If package installation is not authorized in WP06, validate the declaration mechanism structurally without pretending an installation reproduction test occurred.

If installation is authorized, use a controlled clean-venv recreation proof and report exact resolved versions.

---

## 11. Dependency Integrity and Inspection

Define canonical inspection commands such as:

- `.venv\Scripts\python.exe -m pip --version`
- `.venv\Scripts\python.exe -m pip list`
- `.venv\Scripts\python.exe -m pip freeze`
- `.venv\Scripts\python.exe -m pip check`

Use only commands appropriate to the current WP state.

`pip freeze` output is evidence; do not automatically treat an arbitrary environment freeze as the authoritative dependency design.

Require `pip check` after future package installation.

---

## 12. Security Governance

Establish dependency-security rules:

- use official Python Package Index/trusted configured sources unless separately governed;
- do not embed credentials or private-index tokens;
- do not disable TLS/certificate validation;
- do not use arbitrary package mirrors;
- avoid dependency confusion through explicit package identity/source discipline;
- package installation must be reproducible and reviewable;
- Gitleaks remains mandatory;
- new foundational tools/libraries require selection records.

Do not introduce a new vulnerability scanner in WP06 unless already governed.

---

## 13. Global-vs-Project Enforcement

Prove current cleanliness:

### Machine interpreter

NumPy, pandas, scikit-learn, and Streamlit remain absent globally.

### Project `.venv`

At WP06 start they remain absent.

If WP06 does not authorize installation, they must remain absent at completion.

Document commands developers can use to distinguish:

- machine interpreter;
- `.venv` interpreter;
- machine package state;
- project package state.

The policy must make accidental global installation recognizable.

---

## 14. VS Code Environment Rule

Preserve WP04/WP05 policy:

- machine Python is the bootstrap/base runtime;
- `.venv` is the preferred repository interpreter once present;
- VS Code should use `.venv` for project Python execution;
- dependency installation from VS Code terminals must still target `.venv`;
- no absolute user-specific interpreter path is committed.

Do not modify global/user VS Code settings.

---

## 15. Environment Recreation Rule

Document the supported lifecycle:

1. verify governed machine Python;
2. create `.venv`;
3. verify venv interpreter;
4. install dependencies from authoritative repository declarations when available;
5. run integrity checks;
6. run Python validation/use cases when later governed;
7. delete/recreate `.venv` when proving reproducibility.

`.venv` remains disposable and untracked.

Do not treat the environment directory itself as the dependency record.

---

## 16. Upgrade Governance

Define an intentional upgrade workflow:

- identify requested dependency change;
- check Python/runtime compatibility;
- check upstream release/security information;
- review selection record/reconsideration triggers;
- update direct declaration;
- regenerate/reconcile reproducible resolution as governed;
- recreate/validate environment;
- run Python and .NET verification;
- inspect diff;
- document material decision changes.

No uncontrolled `pip install --upgrade` against the project environment.

---

## 17. Removal Governance

Define dependency removal:

- remove direct declaration;
- identify no-longer-needed transitives;
- reconstruct/reconcile environment rather than relying on residue;
- run integrity/validation;
- update selection/governance records when architectural capability is removed.

This prevents stale transitive packages from masquerading as intentional dependencies.

---

## 18. Required Governance Artifact

Create a durable engineering document using the exact manifest-owned path.

If the manifest does not already name it, the preferred canonical artifact is:

`docs/architecture/implementation/PYTHON_DEPENDENCY_GOVERNANCE.md`

It must concisely cover:

- scope;
- ownership model;
- declaration mechanism;
- install command rules;
- direct/transitive distinction;
- version policy;
- reproducibility;
- security/source policy;
- global-vs-project rule;
- VS Code rule;
- recreation;
- upgrades;
- removals;
- integrity verification;
- enforcement/review expectations.

If introducing this file requires manifest reconciliation, make the smallest authoritative manifest update.

---

## 19. Selection-Record Reconciliation

Review the existing NumPy, pandas, scikit-learn, and Streamlit selection records.

Ensure each is consistent with WP06 dependency governance concerning:

- dependency scope;
- version policy;
- `.venv` installation;
- global-install prohibition;
- reproducibility;
- upgrade/revisit policy.

Make only minimal governance-alignment changes.

Do not rewrite previously accepted rationale without a defect.

If exact versions remain deferred, say so consistently.

If exact versions are selected under WP06 authority, record evidence and exact governed versions consistently in each record.

---

## 20. Dependency Files

Create or modify dependency declaration/resolution files only when explicitly authorized by the Release 1.8 manifest/execution plan.

Before creating any file, classify it as one of:

- direct dependency declaration;
- resolved reproducibility artifact;
- development/test dependency declaration;
- documentation only.

Do not create duplicate competing mechanisms such as simultaneous unmanaged `requirements.txt`, `pyproject.toml`, and arbitrary freeze files without an architectural reason.

If the planning artifacts do not resolve which mechanism WP06 owns, stop rather than inventing one.

---

## 21. Explicit Non-Goals

Unless explicitly assigned by authoritative WP06 planning, do not:

- install NumPy;
- install pandas;
- install scikit-learn;
- install Streamlit;
- install SciPy directly;
- install Jupyter;
- implement Python application code;
- implement .NET↔Python integration;
- implement Streamlit UI;
- implement ML;
- create datasets/features/models;
- change schema v3;
- add .NET packages/projects/references;
- modify machine Python/PATH;
- modify VS Code extensions/settings;
- execute WP07+;
- begin Release 1.9;
- stage, commit, push, branch, PR, merge, tag, or release.

---

## 22. Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE:

- DG1 — starting repository/GitHub state reconciled;
- DG2 — `.venv` provenance/isolation preserved;
- DG3 — machine/project dependency ownership documented;
- DG4 — global project-package installation explicitly prohibited;
- DG5 — interpreter-qualified pip rule established;
- DG6 — dependency declaration mechanism is authoritative/non-duplicative;
- DG7 — direct/transitive distinction established;
- DG8 — Python 3.13 compatibility policy established;
- DG9 — reproducibility policy established;
- DG10 — dependency source/security policy established;
- DG11 — VS Code `.venv` dependency rule preserved;
- DG12 — recreation lifecycle documented;
- DG13 — upgrade governance documented;
- DG14 — removal governance documented;
- DG15 — integrity inspection/check policy documented;
- DG16 — selection records reconciled;
- DG17 — exact-version decision/deferral matches planning authority;
- DG18 — machine global dependency cleanliness preserved;
- DG19 — tracked changes match manifest authority exactly;
- DG20 — no unauthorized implementation/package/tool residue.

---

## 23. Canonical Verification

Run repository-native verification after tracked documentation/governance changes.

Expected governed baseline:

- Domain.Tests: 11/11;
- Application.Tests: 119/119;
- Infrastructure.Tests: 125/125;
- Architecture.Tests: 13/13;
- total: 268/268;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS;
- Markdown links: PASS;
- terminal newlines: PASS;
- trailing whitespace: 0;
- conflict markers: 0;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- schema: v3;
- dependency graph unchanged/acyclic;
- .NET package/project/reference delta: 0/0/0;
- `.venv` remains ignored/untracked.

Reconcile legitimate governed baseline changes rather than falsifying counts.

---

## 24. Mutation Accounting

Report exact deltas for:

- governance documentation;
- selection records;
- dependency declarations/resolution files;
- `.gitignore`;
- `.venv`;
- project Python packages;
- machine Python packages;
- VS Code;
- production code;
- permanent tests;
- schema;
- .NET packages/projects/references;
- Git;
- GitHub.

No hidden package installation is permitted.

---

## 25. GitHub Lifecycle

Only after WP06 passes:

1. transition #216 to In Progress if needed;
2. add concise governance evidence;
3. close #216;
4. set #216 Project Status to Done.

Expected final state:

- #211–#216: CLOSED / Done;
- #217–#223: OPEN / Backlog;
- milestone #56: OPEN, 7 open / 6 closed;
- Project membership: 13/13;
- duplicates: 0;
- fields/dependency chain unchanged.

Do not transition #217 automatically.

---

## 26. Stop Conditions

Stop with:

`RELEASE 1.8 WP06 BLOCKED`

if:

- starting state is inconsistent;
- dependency mechanism is ambiguous in authoritative planning;
- a new foundational dependency manager/tool would be required;
- existing package state is unexplained;
- exact-version authority is ambiguous and concrete selection would be required;
- global/project isolation cannot be preserved;
- selection records materially conflict with governance;
- canonical verification fails;
- unauthorized implementation/package/schema/.NET drift exists;
- WP07+ work is required to claim success.

Report exact state and smallest corrective authority required.

---

## 27. Required Execution Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- GitHub lifecycle;
- Python/.venv provenance;
- machine and venv package state.

### Governance
- ownership model;
- declaration mechanism;
- installation rule;
- direct/transitive rule;
- version policy;
- reproducibility;
- source/security policy;
- VS Code rule;
- recreation;
- upgrades/removals.

### Selection Records
- NumPy;
- pandas;
- scikit-learn;
- Streamlit;
- exact-version decision or explicit continued deferral.

### Dependency Artifacts
- files created/modified;
- purpose/classification;
- whether packages were installed.

### Validation
- DG1–DG20;
- 268/268 tests;
- build;
- formatting;
- Gitleaks;
- links/diff;
- schema/graph;
- global/project cleanliness;
- residue.

### Mutation Accounting
- all repository/workstation/GitHub deltas.

### Final State
- #216 lifecycle;
- milestone #56;
- next authorized WP.

---

## 28. Completion Markers

On success end exactly:

`RELEASE 1.8 WP06 COMPLETE`

`PYTHON DEPENDENCY GOVERNANCE & ENVIRONMENT RULES: ESTABLISHED`

`NEXT AUTHORIZED WORK PACKAGE: WP07 — Governed Python Dependency Installation — GitHub issue #217`

If the authoritative title of #217 differs, use the exact live GitHub issue title in the final marker without changing its scope.

Do not execute WP07 automatically.

If blocked end exactly:

`RELEASE 1.8 WP06 BLOCKED`

# Release 1.8 WP07 — Scientific & Machine Learning Library Foundation — Codex Authority

## 1. Mission

Execute Release 1.8 WP07 — **Scientific & Machine Learning Library Foundation** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#217`

Milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established Release 1.8 state:

- #211–#216: CLOSED / Done;
- #217: OPEN / Backlog;
- #218–#223: OPEN / Backlog;
- milestone #56: OPEN, 7 open / 6 closed;
- official machine runtime: CPython 3.13.15 amd64;
- project environment: repository-root `.venv`, isolated and Git-ignored;
- dependency governance: established;
- authoritative dependency declaration: repository-root `requirements.txt`;
- `requirements.txt` is intentionally empty pending WP07;
- NumPy, pandas, scikit-learn, and Streamlit have individual engineering selection records;
- exact versions were intentionally deferred to WP07;
- no third-party project packages are currently installed in `.venv`;
- no scientific/ML/UI project packages are installed globally;
- canonical .NET baseline: 268/268;
- schema: v3.

WP07 selects, records, installs, and validates the first governed Python scientific/ML/UI library foundation.

The four governed direct dependencies are:

- NumPy;
- pandas;
- scikit-learn;
- Streamlit.

WP07 establishes dependency availability only. It does not implement machine-learning models, application integration, dashboards, or Release 1.9 functionality.

---

## 2. Foundational Technology Governance

Preserve the standing project rule:

> Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.

The existing selection records for NumPy, pandas, scikit-learn, and Streamlit satisfy the selection-record requirement structurally, but WP07 must reconcile them with the exact versions actually selected.

Do not introduce another foundational direct dependency without a governed selection record and explicit authority.

---

## 3. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`
- `docs/architecture/implementation/PYTHON_DEPENDENCY_GOVERNANCE.md`
- NumPy engineering selection record;
- pandas engineering selection record;
- scikit-learn engineering selection record;
- Streamlit engineering selection record;
- `requirements.txt`;
- `.gitignore`;
- WP05/WP06 completion evidence;
- GitHub issue #217;
- Project #2 Release 1.8 state.

Use actual canonical filenames where selection-record names differ.

---

## 4. Mandatory Starting-State Gate

Before dependency selection or installation verify:

### Repository

- correct repository and remote;
- branch `main`;
- local HEAD equals `origin/main`;
- ahead/behind `0/0`;
- staged paths: 0;
- no unexplained tracked changes;
- `.venv/` remains ignored;
- `requirements.txt` exists and is intentionally empty as governed by WP06;
- dependency governance document exists.

### GitHub

- #211–#216: CLOSED / Done;
- #217: OPEN / Backlog;
- #218–#223: OPEN / Backlog;
- milestone #56: OPEN, 7 open / 6 closed;
- Project membership: 13/13;
- duplicates: 0;
- WP06→WP07 dependency exists;
- fields/dependency chain remain authoritative.

### Python

Inside `.venv` prove:

- Python 3.13.15;
- x64;
- `sys.prefix != sys.base_prefix`;
- system site packages are not inherited;
- local pip works;
- NumPy/pandas/scikit-learn/Streamlit are absent.

For the machine interpreter prove those four packages remain absent globally.

Stop on unexplained package state.

---

## 5. Current Compatibility Evidence Is Mandatory

Exact versions must not be guessed from model memory.

Before selecting versions, obtain current authoritative upstream evidence for Python 3.13 support and currently supported/stable releases.

Prefer primary sources:

- Python Package Index project/release metadata where appropriate;
- official NumPy documentation/release notes;
- official pandas documentation/release notes;
- official scikit-learn documentation/release notes;
- official Streamlit documentation/release notes;
- official package metadata from the configured trusted package index.

For each direct dependency record:

- candidate stable version;
- release/status evidence;
- declared Python requirement;
- Python 3.13 compatibility evidence;
- relevant compatibility constraints with the other selected direct dependencies;
- evidence URL/source and observation date.

Do not use pre-release, release-candidate, beta, alpha, nightly, or development builds unless the accepted Release 1.8 authority explicitly requires one.

If current authoritative evidence cannot be accessed, stop rather than selecting versions from memory.

---

## 6. Exact Version Selection

Select one exact stable version for each:

- `numpy`;
- `pandas`;
- `scikit-learn`;
- `streamlit`.

Selection priorities, in order:

1. explicit Python 3.13 compatibility;
2. stable supported upstream release;
3. mutual dependency compatibility;
4. broad ecosystem support;
5. security/maintenance posture;
6. reproducibility;
7. avoidance of unnecessary bleeding-edge risk.

Do not automatically choose the numerically newest version if authoritative evidence identifies a compatibility reason not to.

Do not intentionally select obsolete versions merely for conservatism.

Report the reasoning concisely.

---

## 7. Selection Record Reconciliation

Update each existing selection record with:

- exact selected version;
- authoritative compatibility evidence;
- Python requirement;
- selection date/evidence date;
- version policy under WP06 governance;
- accepted compatibility trade-offs, if any;
- explicit `.venv`-only installation boundary;
- upgrade/revisit triggers.

Preserve previously accepted rationale, alternatives, architectural boundaries, and trade-offs unless new evidence requires a narrow correction.

Do not rewrite the records wholesale without cause.

---

## 8. requirements.txt

Populate the authoritative repository-root:

`requirements.txt`

with exactly the governed direct dependencies and exact versions using standard pip-compatible exact pins:

`package==version`

The direct dependency set must be limited to the four explicitly governed libraries unless authoritative planning explicitly names another direct dependency.

Do not manually add transitive dependencies merely because pip resolves them.

Keep the file deterministic, minimal, readable, and terminated by exactly one newline.

Do not add environment-specific absolute paths, hashes copied without a governed hash policy, editable installs, private indexes, credentials, or local wheels.

---

## 9. Installation Boundary

Install only into the existing `.venv`.

Use interpreter-qualified installation, semantically:

`.venv\Scripts\python.exe -m pip install -r requirements.txt`

Before installation prove that this interpreter is the governed `.venv` interpreter.

Do not use:

- global `pip`;
- machine `python -m pip` targeting the base interpreter;
- `--user`;
- administrator elevation;
- system-site-packages;
- arbitrary mirrors;
- TLS bypass;
- dependency resolver bypass.

Use the configured trusted/default package source consistent with WP06 governance.

---

## 10. Resolver Outcome

Allow pip to install legitimate transitive dependencies required by the four governed direct dependencies.

After installation, capture:

- direct versions;
- resolved transitive dependency set;
- `pip list`;
- `pip freeze`;
- `pip check`.

Classify every installed package as:

- governed direct dependency;
- legitimate transitive dependency;
- unexpected/unexplained.

Unexpected/unexplained packages are a blocker.

Do not create selection records for ordinary transitive implementation dependencies unless they independently become a foundational/direct architectural choice.

---

## 11. Import and Identity Validation

Using `.venv\Scripts\python.exe`, verify imports and exact runtime identities.

At minimum:

- `import numpy`;
- `import pandas`;
- `import sklearn`;
- `import streamlit`.

Record each library's runtime-reported version.

Require runtime versions to match the governed direct pins.

Also perform minimal non-domain smoke checks that prove packages are functional without implementing Release 1.9 behavior.

Examples of acceptable bounded checks:

- NumPy: create a tiny in-memory array and verify deterministic shape/value behavior;
- pandas: create a tiny in-memory DataFrame and verify columns/row count;
- scikit-learn: import a stable public module/class without training a model;
- Streamlit: import the package and verify version/CLI availability without launching a persistent server.

Do not create production ML models or dashboards.

---

## 12. Streamlit Process Safety

If Streamlit CLI functionality is checked:

- prefer non-server commands such as version/help;
- do not launch a persistent dashboard server unless the accepted plan explicitly requires it;
- do not leave listening ports/processes;
- retained Streamlit processes: 0.

WP07 proves library availability, not UI behavior.

---

## 13. Global Cleanliness

After project installation, prove the machine-wide base Python remains free of the four project direct dependencies.

Use the machine interpreter explicitly.

Require global absence of:

- NumPy;
- pandas;
- scikit-learn;
- Streamlit.

Do not confuse `.venv` package visibility with global installation.

If any direct project package appears globally after WP07, stop and report provenance.

---

## 14. Reproducibility Proof

Prove that `requirements.txt` can reconstruct the governed direct dependency foundation.

Use a controlled clean-environment sequence consistent with WP05/WP06:

1. capture the accepted installed state;
2. remove `.venv` safely;
3. recreate `.venv` using governed CPython 3.13.15;
4. install from `requirements.txt`;
5. run `pip check`;
6. re-run exact import/version checks;
7. compare governed direct versions;
8. confirm no unexpected direct-dependency drift.

The final state should retain a valid ignored `.venv` populated from `requirements.txt`.

Do not use an arbitrary pre-existing freeze as the source of truth.

If environment removal is blocked by retained processes, stop rather than force destructive cleanup.

---

## 15. Transitive Reproducibility Decision

WP06 established `requirements.txt` as the transparent direct-dependency mechanism.

Do not invent a second lock mechanism unless the Release 1.8 plan explicitly authorizes it.

If exact transitive locking is not part of the accepted plan, report that:

- direct dependencies are exactly pinned;
- transitive resolution is governed by pip/upstream constraints;
- the resolved transitive set is captured as validation evidence;
- stronger transitive locking may be reconsidered if reproducibility requirements demand it.

Do not create `requirements.lock`, `constraints.txt`, `poetry.lock`, `uv.lock`, or similar without authority.

---

## 16. Dependency Conflict Validation

Run:

`.venv\Scripts\python.exe -m pip check`

Require zero broken requirements.

Also inspect warnings/errors from installation for:

- incompatible Python requirements;
- dependency conflicts;
- deprecated/unsupported package state;
- resolver anomalies.

Do not suppress resolver errors.

---

## 17. Security and Source Integrity

Require:

- trusted package source;
- TLS validation intact;
- no credentials in commands/files;
- no private index tokens;
- no arbitrary third-party wheel downloads;
- no local package substitution;
- no `--trusted-host` workaround;
- Gitleaks PASS.

Record package source/provenance sufficiently for engineering evidence without storing secrets.

---

## 18. Repository File Ownership

Follow `RELEASE_1.8_FILE_MANIFEST.md` exactly.

Expected WP07 tracked mutations may include only:

- `requirements.txt`;
- four existing selection records;
- manifest/documentation reconciliation explicitly assigned to WP07;
- a WP07 evidence document only if manifest-authorized.

Do not invent source code or test projects.

`.venv/` remains ignored local state.

---

## 19. No Production Architecture Mutation

Expected WP07 deltas:

- .NET production code: 0;
- Python production/application code: 0;
- permanent tests: 0 unless explicitly manifest-authorized;
- schema: 0;
- .NET packages: 0;
- .NET projects: 0;
- .NET references: 0;
- provider/network product calls: 0;
- real credentials: 0.

The only package installation is into `.venv`.

---

## 20. Explicit Non-Goals

Do not:

- train a machine-learning model;
- create train/validation/test splits;
- implement features;
- implement dataset adapters;
- implement .NET↔Python integration;
- create Streamlit application pages;
- run persistent Streamlit servers;
- change SQLite schema;
- modify existing durable evidence semantics;
- introduce Jupyter;
- introduce MLflow;
- introduce another dependency/environment manager;
- execute WP08+;
- begin Release 1.9;
- stage, commit, push, branch, PR, merge, tag, or release.

---

## 21. Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE:

- LIB1 — starting repository/GitHub state reconciled;
- LIB2 — `.venv` is governed Python 3.13.15 x64 and isolated;
- LIB3 — authoritative current compatibility evidence collected;
- LIB4 — exact stable NumPy version selected;
- LIB5 — exact stable pandas version selected;
- LIB6 — exact stable scikit-learn version selected;
- LIB7 — exact stable Streamlit version selected;
- LIB8 — all four selection records reconciled;
- LIB9 — `requirements.txt` contains exactly four governed direct pins;
- LIB10 — installation targets `.venv` only;
- LIB11 — resolved installed packages are direct or explained transitive dependencies;
- LIB12 — `pip check` passes;
- LIB13 — NumPy import/version/smoke check passes;
- LIB14 — pandas import/version/smoke check passes;
- LIB15 — scikit-learn import/version/smoke check passes;
- LIB16 — Streamlit import/version/CLI smoke check passes;
- LIB17 — global machine interpreter remains free of all four direct packages;
- LIB18 — clean `.venv` recreation from `requirements.txt` reproduces direct versions;
- LIB19 — tracked repository mutations match manifest authority exactly;
- LIB20 — no unauthorized implementation/process/package residue exists.

---

## 22. Canonical Repository Verification

After final environment reconstruction and tracked changes, run repository-native verification.

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
- `.venv` source-control leakage: 0.

If the legitimate governed baseline has changed, reconcile rather than falsifying expected counts.

---

## 23. GitHub Lifecycle

Only after WP07 passes:

1. transition #217 to In Progress if needed;
2. add concise evidence including selected versions and validation outcome;
3. close #217;
4. set #217 Project Status to Done.

Expected final state:

- #211–#217: CLOSED / Done;
- #218–#223: OPEN / Backlog;
- milestone #56: OPEN, 6 open / 7 closed;
- Project membership: 13/13;
- duplicates: 0;
- fields/dependency chain unchanged.

Do not transition #218 automatically.

---

## 24. Stop Conditions

Stop with:

`RELEASE 1.8 WP07 BLOCKED`

if:

- current authoritative compatibility evidence cannot be obtained;
- a stable Python-3.13-compatible version cannot be established for any direct dependency;
- the four selected versions cannot resolve together;
- an unexpected package/source is introduced;
- installation escapes `.venv`;
- global package leakage occurs;
- `pip check` fails;
- runtime versions differ from governed pins;
- clean recreation cannot reproduce the governed direct versions;
- a new foundational tool/direct dependency is required;
- canonical verification fails;
- WP08+/Release 1.9 work would be required.

Report exact partial state and the smallest corrective authority required.

Do not silently substitute package versions after a governed selection-record mutation without reconciling the evidence.

---

## 25. Required Execution Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- GitHub lifecycle;
- `.venv` provenance;
- initial project/global package state.

### Compatibility Evidence
For each of NumPy, pandas, scikit-learn, Streamlit:
- selected exact version;
- Python requirement;
- authoritative sources;
- compatibility rationale;
- evidence date.

### Dependency Declaration
- exact `requirements.txt` contents;
- direct dependency count;
- selection-record changes.

### Installation
- interpreter used;
- package source;
- direct packages;
- transitive packages;
- unexpected packages;
- `pip check`.

### Functional Validation
- NumPy;
- pandas;
- scikit-learn;
- Streamlit;
- exact runtime versions.

### Reproducibility
- environment deletion;
- recreation;
- reinstall from `requirements.txt`;
- post-recreation direct versions;
- integrity result.

### Validation
- LIB1–LIB20;
- 268/268 tests;
- build;
- formatting;
- Gitleaks;
- docs/diff;
- schema/graph;
- global cleanliness;
- residue.

### Mutation Accounting
- tracked files;
- `.venv`;
- global packages;
- project packages;
- production/tests/schema;
- .NET packages/projects/references;
- VS Code/machine Python;
- Git;
- GitHub.

### Final State
- #217 lifecycle;
- milestone #56;
- next authorized WP.

---

## 26. Completion Markers

On success end exactly:

`RELEASE 1.8 WP07 COMPLETE`

`SCIENTIFIC & MACHINE LEARNING LIBRARY FOUNDATION: VERIFIED`

`NEXT AUTHORIZED WORK PACKAGE: WP08 — Python Scientific Stack Validation Use Cases — GitHub issue #218`

If the authoritative live title of #218 differs, use that exact issue title in the final marker without changing scope.

Do not execute WP08 automatically.

If blocked end exactly:

`RELEASE 1.8 WP07 BLOCKED`

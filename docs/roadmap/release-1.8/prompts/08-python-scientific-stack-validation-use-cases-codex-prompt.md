# Release 1.8 WP08 — Python Scientific Stack Validation Use Cases — Codex Authority

## 1. Mission

Execute Release 1.8 WP08 — **Python Scientific Stack Validation Use Cases** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#218`

Milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established Release 1.8 state:

- #211–#217: CLOSED / Done;
- #218: OPEN / Backlog;
- #219–#223: OPEN / Backlog;
- milestone #56: OPEN, 6 open / 7 closed;
- machine runtime: official CPython 3.13.15 amd64;
- repository environment: `.venv`, isolated and Git-ignored;
- dependency governance: established;
- authoritative dependency declaration: `requirements.txt`;
- NumPy: 2.5.1;
- pandas: 3.0.5;
- scikit-learn: 1.9.0;
- Streamlit: 1.61.1;
- all four direct dependencies installed only in `.venv`;
- clean `.venv` recreation from `requirements.txt`: proven;
- `pip check`: PASS;
- machine-global Python remains free of all four project packages;
- canonical .NET baseline: 268/268;
- schema: v3.

WP08 proves that the governed Python scientific stack is operational through small, deterministic, repository-owned validation use cases.

WP08 is validation evidence, not Release 1.9 machine-learning product implementation.

---

## 2. Architectural Intent

The purpose of WP08 is to move beyond package-presence checks and prove that the selected stack can execute representative operations required by future AI/ML engineering.

Validation must remain:

- deterministic;
- bounded;
- offline;
- fast;
- reproducible;
- repository-owned;
- free of external datasets;
- free of real market-provider calls;
- free of durable model/product behavior;
- clearly separated from Release 1.9.

The use cases should demonstrate capability without prematurely defining the ML architecture.

---

## 3. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`
- `docs/architecture/implementation/PYTHON_DEPENDENCY_GOVERNANCE.md`
- NumPy selection record;
- pandas selection record;
- scikit-learn selection record;
- Streamlit selection record;
- `requirements.txt`;
- existing Python-related validation/test conventions if present;
- GitHub issue #218;
- Project #2 Release 1.8 state.

Use the exact live filenames from the repository.

---

## 4. Mandatory Starting-State Gate

Before mutation verify:

### Repository

- correct repository and remote;
- branch `main`;
- local HEAD equals `origin/main`;
- ahead/behind `0/0`;
- staged paths: 0;
- no unexplained tracked changes;
- `.venv/` ignored;
- tracked dependency files match WP07 accepted state.

### GitHub

- #211–#217: CLOSED / Done;
- #218: OPEN / Backlog;
- #219–#223: OPEN / Backlog;
- milestone #56: OPEN, 6 open / 7 closed;
- Project membership: 13/13;
- duplicates: 0;
- WP07→WP08 dependency exists;
- fields/dependency chain remain authoritative.

### Python

Inside `.venv` prove exact direct versions:

- NumPy 2.5.1;
- pandas 3.0.5;
- scikit-learn 1.9.0;
- Streamlit 1.61.1.

Also require:

- Python 3.13.15 x64;
- isolated environment;
- `pip check` PASS;
- machine-global copies of all four direct packages absent.

Stop on unexplained drift.

---

## 5. Manifest Authority First

Before choosing file locations, read `RELEASE_1.8_FILE_MANIFEST.md`.

Use only WP08-authorized paths.

Do not invent a new Python package architecture merely for validation.

If the manifest specifies validation scripts/tests/docs, follow it exactly.

If the manifest is ambiguous about whether WP08 validation should be implemented as tests, scripts, or both, stop rather than establishing an accidental long-term project structure.

Any necessary manifest reconciliation must be minimal and within accepted Release 1.8 planning authority.

---

## 6. Validation Use Case — NumPy

Create a tiny deterministic NumPy validation proving real numerical functionality.

The use case should:

- construct data in memory;
- use no randomness unless an explicit fixed seed is required;
- perform at least one meaningful vectorized numerical transformation;
- verify exact or safely deterministic expected output;
- avoid external files/network/data providers.

Suitable examples include:

- vectorized return/delta calculation;
- normalization of a tiny fixed numeric series;
- matrix/array transformation with known result.

Do not turn this into production feature engineering.

---

## 7. Validation Use Case — pandas

Create a tiny deterministic pandas validation proving tabular manipulation.

The use case should:

- construct a DataFrame in memory;
- use stable explicit column names;
- perform a representative tabular transformation;
- verify resulting shape/columns/values;
- avoid filesystem/network dependencies unless the manifest explicitly requires a bounded temporary-file proof.

Suitable examples include:

- filtering;
- sorting;
- derived column;
- grouping/aggregation;
- conversion between simple tabular structures.

Do not introduce a durable data pipeline.

---

## 8. Validation Use Case — scikit-learn

Create a tiny deterministic scikit-learn validation proving the ML library can execute an end-to-end bounded operation.

This is a capability proof only.

Use:

- tiny in-memory synthetic/fixed data;
- a stable, simple public estimator;
- deterministic configuration;
- fixed `random_state` wherever the chosen API can be stochastic;
- fit and predict/transform behavior;
- explicit assertions on output shape/type and deterministic result where appropriate.

Avoid:

- real market data;
- hyperparameter search;
- cross-validation architecture;
- model persistence;
- feature stores;
- experiment evidence integration;
- production metrics;
- large datasets;
- GPU dependencies.

The test must not establish Release 1.9 modeling policy by accident.

---

## 9. Validation Use Case — Streamlit

Prove Streamlit can execute enough application/runtime behavior to establish that the selected UI library is functional.

Prefer the narrowest automated mechanism supported by the installed Streamlit version and repository plan.

Acceptable validation may include:

- importing Streamlit;
- creating/loading a minimal validation app;
- using supported Streamlit testing facilities if already available through the selected package;
- verifying a small deterministic rendered element/state;
- CLI version/help verification.

If a temporary server launch is required by the authoritative plan:

- bind only locally;
- use a bounded timeout;
- do not open external network access;
- terminate the server deterministically;
- prove no retained process/listening port.

Do not create the future portfolio dashboard in WP08.

---

## 10. Unified Validation Shape

Prefer a coherent validation suite rather than four unrelated ad hoc scripts.

Where the accepted manifest permits, make the use cases independently executable and collectively executable.

The validation suite should clearly identify failures by technology:

- NumPy;
- pandas;
- scikit-learn;
- Streamlit.

Do not require manual inspection to determine success.

Exit status must communicate failure for automated execution where scripts are used.

---

## 11. Python Test Framework Boundary

Do not introduce pytest or another new test framework unless already explicitly selected and governed.

If no Python test framework is currently governed, use Python standard-library facilities such as `unittest` or manifest-authorized executable validation scripts.

A new test framework is a foundational external tool/library decision and requires its own engineering selection record and authority.

Do not silently add it to `requirements.txt`.

---

## 12. Determinism

All WP08 validations must be deterministic.

Require:

- fixed input data;
- stable ordering;
- explicit seeds for stochastic APIs;
- no wall-clock-dependent assertions;
- no current-date-dependent results;
- no network data;
- no machine-specific absolute paths;
- no dependency on prior `.venv` execution state.

Repeated runs must produce equivalent pass/fail outcomes.

---

## 13. Offline Boundary

After dependencies are already installed, validation use cases must execute without requiring provider/network access.

Do not call:

- Twelve Data;
- GitHub APIs as part of the use cases;
- external datasets;
- package indexes during validation;
- remote model services;
- AI APIs.

Repository/GitHub lifecycle operations remain separate from product validation.

---

## 14. Data Boundary

Use only tiny synthetic/fixed in-memory validation data unless the accepted manifest already defines a repository fixture.

Do not:

- acquire market data;
- modify SQLite product databases;
- mutate `experiment_results`;
- create durable ML datasets;
- add training/validation/test repository channels;
- alter schema v3.

Temporary test artifacts, if any, must be cleaned completely.

---

## 15. scikit-learn Randomness Rule

Any scikit-learn operation with stochastic behavior must explicitly specify a deterministic seed.

If the chosen estimator is deterministic without randomness, document that fact.

Do not create a general Release 1.9 experiment-seed architecture in WP08.

This WP proves library behavior; future experiment-definition semantics remain separately governed.

---

## 16. Streamlit Process/Residue Rule

Because WP07 exposed interaction with VS Code/Python processes, be especially strict about process ownership.

Before and after Streamlit validation:

- inspect relevant Python/Streamlit processes when applicable;
- distinguish legitimate VS Code language-server processes from validation processes;
- never kill unrelated Python processes broadly;
- terminate only processes created by WP08;
- retained WP08 processes: 0;
- retained listening ports: 0.

Do not treat a legitimate VS Code Jedi language-server process as WP08 residue.

---

## 17. Documentation

Create/update only manifest-authorized WP08 documentation.

The documentation should explain:

- what each validation use case proves;
- why it remains intentionally smaller than Release 1.9 behavior;
- how to execute the suite with `.venv`;
- deterministic/offline guarantees;
- expected pass criteria;
- what WP08 explicitly does not prove.

Keep the documentation concise and engineering-focused.

---

## 18. requirements.txt Stability

WP08 should not change the four governed direct pins:

- `numpy==2.5.1`
- `pandas==3.0.5`
- `scikit-learn==1.9.0`
- `streamlit==1.61.1`

If validation reveals a genuine incompatibility requiring a version change, stop and request corrective authority.

Do not opportunistically upgrade dependencies.

Do not add a Python testing package without separate governance.

---

## 19. Validation Execution

Execute all WP08 validation use cases using the explicit `.venv` interpreter.

Prefer:

`.venv\Scripts\python.exe ...`

or a proven activated `.venv` equivalent.

Record:

- interpreter path/provenance;
- Python version;
- direct package versions;
- each validation result;
- total validation count if represented as tests;
- execution exit code.

Run the suite more than once where practical to prove deterministic repeatability.

---

## 20. Existing Platform Regression Protection

WP08 must not regress the existing .NET platform.

Run canonical repository verification after Python validation implementation.

Expected governed baseline:

- Domain.Tests: 11/11;
- Application.Tests: 119/119;
- Infrastructure.Tests: 125/125;
- Architecture.Tests: 13/13;
- permanent .NET total: 268/268;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS;
- Markdown links: PASS;
- whitespace/conflict-marker checks: PASS;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- schema: v3;
- .NET dependency graph unchanged/acyclic;
- .NET package/project/reference delta: 0/0/0.

If legitimate governed counts have changed, reconcile rather than falsify.

---

## 21. Python Environment Integrity

After WP08 execution require:

- `.venv` remains isolated;
- `pip check`: PASS;
- direct versions unchanged;
- machine-global direct packages absent;
- no unauthorized package additions;
- no package removal;
- no `.venv` source-control leakage.

Compare installed package state with the accepted WP07 state sufficiently to detect unexpected dependency mutation.

---

## 22. Explicit Non-Goals

Do not:

- implement Release 1.9 ML capabilities;
- use real market data;
- integrate Python with .NET;
- create IPC/subprocess/application bridges;
- define model-serving architecture;
- persist trained models;
- persist feature matrices;
- modify experiment evidence semantics;
- modify SQLite schema;
- create the final Streamlit dashboard;
- introduce pytest/Jupyter/MLflow or other new foundational tools;
- change dependency versions;
- execute WP09+;
- begin Release 1.9;
- stage, commit, push, branch, PR, merge, tag, or release.

---

## 23. Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE:

- USE1 — starting repository/GitHub/Python state reconciled;
- USE2 — exact WP07 direct dependency versions preserved;
- USE3 — NumPy validation is deterministic and passes;
- USE4 — pandas validation is deterministic and passes;
- USE5 — scikit-learn bounded fit/predict-or-transform validation passes;
- USE6 — stochastic scikit-learn behavior has explicit deterministic seed where applicable;
- USE7 — Streamlit bounded runtime/testing validation passes;
- USE8 — no persistent Streamlit server/process/port remains;
- USE9 — validation uses only governed `.venv`;
- USE10 — validation is offline after dependency installation;
- USE11 — validation uses only fixed/synthetic bounded data;
- USE12 — no new ungoverned Python test framework/tool introduced;
- USE13 — repeated execution produces equivalent outcomes;
- USE14 — `requirements.txt` pins remain unchanged;
- USE15 — `pip check` remains PASS;
- USE16 — global machine Python remains free of the four direct project packages;
- USE17 — no schema/product database/experiment evidence mutation;
- USE18 — tracked mutations match WP08 manifest authority exactly;
- USE19 — canonical .NET 268/268 regression suite passes;
- USE20 — no unauthorized package/process/file/network residue remains.

---

## 24. Security and Cleanliness

Require:

- real credentials: 0;
- provider product calls: 0;
- remote AI calls: 0;
- persistent local servers: 0;
- unexpected listening ports: 0;
- global Python package mutations: 0;
- `.venv` direct-version drift: 0;
- schema mutations: 0;
- database residue: 0;
- temporary-file residue: 0;
- WP08-owned process residue: 0;
- Gitleaks: PASS.

Do not broadly terminate unrelated machine processes.

---

## 25. Mutation Accounting

Report exact deltas for:

- Python validation source/scripts/tests;
- validation documentation;
- Release 1.8 manifest;
- `requirements.txt`;
- selection records;
- `.venv` packages;
- machine-global packages;
- production .NET code;
- production Python code;
- permanent .NET tests;
- schema/database;
- .NET packages/projects/references;
- VS Code;
- machine Python/PATH;
- Git;
- GitHub.

Distinguish repository-owned validation code from future production Python implementation.

---

## 26. GitHub Lifecycle

Only after every WP08 gate passes:

1. transition #218 to In Progress if needed;
2. add concise evidence describing the four validation use cases and results;
3. close #218;
4. set #218 Project Status to Done.

Expected final state:

- #211–#218: CLOSED / Done;
- #219–#223: OPEN / Backlog;
- milestone #56: OPEN, 5 open / 8 closed;
- Project membership: 13/13;
- duplicates: 0;
- Release/Priority/Area fields unchanged;
- dependency chain unchanged.

Do not transition #219 automatically.

---

## 27. Stop Conditions

Stop with:

`RELEASE 1.8 WP08 BLOCKED`

if:

- starting state is inconsistent;
- manifest ownership for validation artifacts is ambiguous;
- a new external test/tool dependency is required;
- a governed direct package version must change;
- deterministic validation cannot be achieved;
- Streamlit validation leaves unowned process/port residue;
- validation requires network/provider access;
- Python environment integrity fails;
- canonical .NET verification fails;
- schema/product behavior changes;
- WP09+/Release 1.9 behavior would be required.

Report exact partial state and smallest corrective authority required.

---

## 28. Required Execution Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- GitHub lifecycle;
- Python/.venv provenance;
- exact direct dependency versions;
- `pip check`;
- global cleanliness.

### Validation Artifacts
- exact files created/modified;
- manifest ownership;
- execution mechanism;
- whether standard library `unittest` or scripts were used.

### NumPy
- operation;
- deterministic input;
- assertion/result.

### pandas
- operation;
- deterministic input;
- assertion/result.

### scikit-learn
- estimator/API;
- input;
- seed/determinism;
- fit/predict-or-transform result.

### Streamlit
- validation mechanism;
- rendered/runtime evidence;
- process/port cleanup.

### Repeatability
- repeated execution evidence;
- offline behavior;
- residue.

### Validation
- USE1–USE20;
- Python suite result;
- 268/268 .NET tests;
- build;
- formatting;
- Gitleaks;
- links/diff;
- schema/graph;
- environment integrity.

### Mutation Accounting
- all repository/workstation/GitHub deltas.

### Final State
- #218 lifecycle;
- milestone #56;
- next authorized WP.

---

## 29. Completion Markers

On success end exactly:

`RELEASE 1.8 WP08 COMPLETE`

`PYTHON SCIENTIFIC STACK VALIDATION USE CASES: PASS`

`NEXT AUTHORIZED WORK PACKAGE: WP09 — .NET ↔ Python Integration Boundary — GitHub issue #219`

If the authoritative live title of #219 differs, use the exact live GitHub issue title in the final marker without changing its scope.

Do not execute WP09 automatically.

If blocked end exactly:

`RELEASE 1.8 WP08 BLOCKED`

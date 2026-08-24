# Release 1.8 WP05 — Python Virtual Environment Foundation — Codex Authority

## 1. Mission

Execute Release 1.8 WP05 — **Python Virtual Environment Foundation** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#215`

Authoritative milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established Release 1.8 state:

- WP01/#211: CLOSED / Done;
- WP02/#212: CLOSED / Done;
- WP03/#213: CLOSED / Done;
- WP04/#214: CLOSED / Done;
- WP05/#215: OPEN / Backlog;
- #216–#223: OPEN / Backlog;
- milestone #56: OPEN, 9 open / 4 closed;
- machine runtime: official PSF CPython 3.13.15 amd64;
- machine installation: `C:\Program Files\Python313\`;
- PowerShell current/fresh/neutral verification: PASS;
- VS Code 1.134.0 x64 verified;
- governed Microsoft Python extension: `ms-python.python@2026.4.0`;
- project-local virtual environment: absent;
- NumPy, pandas, scikit-learn, and Streamlit: absent globally;
- canonical .NET baseline: 268/268;
- schema: v3.

WP05 establishes the repository-local Python virtual-environment foundation and proves that AIQuantTradingResearch Python dependencies can be isolated from the machine-wide runtime.

WP05 does not yet populate the environment with the scientific/ML/UI dependency set unless the accepted Release 1.8 plan explicitly assigns such installation to WP05.

---

## 2. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md`
- `docs/architecture/implementation/VSCODE_PYTHON_EXTENSION_SELECTION.md`
- NumPy, pandas, scikit-learn, and Streamlit selection records;
- relevant repository environment/tooling/security/version-control documentation;
- `.gitignore`;
- repository-owned `.vscode/` state if present;
- WP04 completion evidence;
- GitHub issue #215;
- Project #2 Release 1.8 state.

Preserve all standing engineering governance rules.

---

## 3. Mandatory Starting-State Gate

Before mutation verify:

### Repository

- correct repository and remote;
- branch: `main`;
- local HEAD equals `origin/main`;
- ahead/behind: `0/0`;
- staged paths: 0;
- no unexplained tracked changes;
- only governed Release 1.8 documentation/authority inputs are present.

Do not assume the original Release 1.8 planning SHA if legitimate governed documentation has advanced the repository state.

### GitHub

- #211–#214: CLOSED / Done;
- #215: OPEN / Backlog;
- #216–#223: OPEN / Backlog;
- milestone #56: OPEN, 9 open / 4 closed;
- Project #2 membership: 13/13;
- duplicates: 0;
- WP04→WP05 dependency exists;
- fields and dependency chain remain authoritative.

### Python

Require:

- `python --version` = Python 3.13.15;
- real executable resolves to the governed machine CPython;
- x64 runtime;
- `py -3.13` works where applicable;
- `python -m pip --version` works;
- `python -m venv --help` works;
- no existing repository-local `.venv`, `venv`, or equivalent project environment;
- no global NumPy/pandas/scikit-learn/Streamlit;
- no premature Python implementation or Release 1.9 ML work.

If a repository-local environment already exists, stop before replacing/deleting it and reconcile its provenance.

---

## 4. Virtual Environment Location Decision

Use the repository-local conventional environment path:

`.venv`

unless the accepted Release 1.8 planning artifacts explicitly govern another path.

The environment must:

- live at repository root as `.venv/`;
- be local developer/runtime state, not source;
- never be committed;
- be reproducible from governed machine Python plus future dependency declarations;
- remain disposable.

Do not create multiple competing virtual environments.

---

## 5. Version-Control Protection Gate

Before creating `.venv`, prove that repository version-control rules exclude it.

Inspect `.gitignore` and related ignore mechanisms.

Require `.venv/` to be ignored explicitly or through an unambiguous repository-native rule.

If `.venv/` is not safely ignored and the Release 1.8 file manifest authorizes the necessary `.gitignore` change for WP05, add the smallest explicit rule.

Preferred rule:

`.venv/`

If such a change is not manifest-authorized, stop rather than creating an unprotected environment.

After creation prove:

- `git check-ignore -v .venv/...` identifies the governing ignore rule;
- `.venv` content does not appear as untracked repository content;
- no venv files are staged.

---

## 6. Environment Creation

Create `.venv` using the governed machine CPython 3.13.15.

Use the explicit governed interpreter or an equivalently proven command so WindowsApps ambiguity cannot create the environment with the wrong runtime.

Expected semantic operation:

`python -m venv .venv`

but verify the `python` executable before execution.

Do not use Conda, Poetry, uv, pipenv, virtualenv, pyenv, or another environment manager unless already explicitly selected and governed. The standard-library `venv` capability is sufficient for WP05.

No new foundational environment-management tool is authorized.

---

## 7. Interpreter Provenance

After creation prove:

- `.venv\Scripts\python.exe` exists;
- `.venv\Scripts\python.exe --version` reports Python 3.13.15;
- `sys.prefix` points inside `.venv`;
- `sys.base_prefix` points to the governed machine Python base;
- `sys.prefix != sys.base_prefix`;
- architecture remains x64;
- environment resides under the repository root;
- base runtime remains `C:\Program Files\Python313\` or canonical equivalent.

Record evidence without committing machine-specific paths to portable repository configuration.

---

## 8. PowerShell Activation

Validate standard PowerShell activation behavior using:

`.venv\Scripts\Activate.ps1`

or the supported equivalent.

Prove in a controlled PowerShell process:

- activation succeeds under the existing security policy;
- `python` resolves to `.venv\Scripts\python.exe`;
- Python version remains 3.13.15;
- `pip`/`python -m pip` resolve inside the venv;
- deactivation restores normal machine-runtime resolution.

Do not weaken PowerShell execution policy globally.

If the existing policy blocks activation, prefer a process-scoped safe resolution consistent with existing repository guidance. Do not make permanent security-policy changes without separate authority.

Also prove that direct invocation of `.venv\Scripts\python.exe` works without activation; activation must not be the only supported execution model.

---

## 9. Environment Isolation

Prove isolation from the base interpreter.

At minimum compare:

- `sys.executable`;
- `sys.prefix`;
- `sys.base_prefix`;
- `python -m pip --version`;
- site-package locations.

Require:

- project venv has its own site-packages;
- machine-global project packages are not required;
- the environment does not inherit arbitrary global site-packages unless explicitly governed;
- no `PYTHONHOME`/`PYTHONPATH` contamination breaks isolation.

Do not enable system-site-packages.

---

## 10. Initial Package State

WP05 establishes the environment, not the application dependency stack.

Inspect the initial venv package state.

Allow only bootstrap components created by the standard Python venv mechanism.

Do not install:

- NumPy;
- pandas;
- SciPy;
- scikit-learn;
- Streamlit;
- Jupyter;
- notebook tooling;
- ML frameworks;
- AIQuantTradingResearch Python dependencies.

Do not upgrade pip merely because a newer version is available unless the accepted WP05 plan explicitly requires it.

Record the initial pip version as evidence.

---

## 11. Reproducibility / Re-Creation Proof

Prove `.venv` is disposable and reproducible without losing repository state.

Use a controlled validation sequence consistent with the accepted plan:

1. create `.venv`;
2. verify provenance/isolation;
3. remove `.venv` completely;
4. prove repository cleanliness;
5. recreate `.venv` from the governed Python 3.13.15 runtime;
6. re-run essential provenance/isolation checks.

The final accepted state should contain a valid ignored `.venv` unless the Release 1.8 plan explicitly says environments must not persist between WPs.

Do not delete unrelated files.

If Windows file locks/processes prevent safe recreation, stop and report the exact process/residue rather than forcing deletion.

---

## 12. VS Code Discoverability

Verify that the governed Microsoft Python extension can discover the repository-local `.venv` or that repository-native configuration makes it naturally discoverable.

Required semantic result:

- `.venv` is the preferred project interpreter once present;
- machine Python remains the bootstrap/base runtime;
- no user-specific absolute path is committed.

Do not fabricate a UI selection.

Use CLI/configuration/extension evidence where available.

If a manual UI confirmation remains necessary, document the exact boundary.

Do not modify global/user VS Code settings.

---

## 13. Portable Repository Policy

Establish or preserve these rules in the manifest-authorized WP05 evidence/documentation:

- machine Python 3.13 is installed independently of the repository;
- `.venv` is project-local and disposable;
- `.venv` is ignored by Git;
- project Python commands should use the venv interpreter once the environment exists;
- project libraries are installed only into `.venv`;
- global `pip install` is prohibited for project dependencies;
- absolute user/machine interpreter paths are not committed;
- environment recreation must be possible from governed runtime/dependency declarations;
- VS Code should prefer `.venv` after creation.

Do not create dependency pins before their governing WP.

---

## 14. File Manifest Ownership

Follow `RELEASE_1.8_FILE_MANIFEST.md` exactly.

Only mutate tracked repository files explicitly assigned to WP05.

Possible legitimate WP05 tracked changes, only when manifest-authorized:

- `.gitignore` — smallest `.venv/` protection if missing;
- WP05 environment-foundation documentation/evidence;
- portable repository-owned VS Code configuration if explicitly assigned to WP05.

Do not invent convenience files.

The `.venv/` directory itself is intentionally untracked local state and is not a manifest source artifact.

---

## 15. No Foundational Tool Expansion

WP05 uses standard-library `venv`.

Do not introduce:

- Poetry;
- uv;
- Conda;
- pipenv;
- virtualenv;
- pyenv;
- environment managers;
- dependency managers beyond already-governed tooling.

If a new foundational external tool appears necessary, stop and apply the project's selection-record governance rule under separate authority.

---

## 16. Explicit Non-Goals

Do not:

- select exact NumPy/pandas/scikit-learn/Streamlit versions;
- install those libraries;
- create requirements/dependency lock files unless WP05 manifest authority explicitly requires them;
- implement Python application code;
- implement `.NET ↔ Python`;
- create Streamlit UI;
- implement ML;
- modify schema v3;
- add .NET packages/projects/references;
- modify machine Python;
- modify machine PATH;
- modify global/user VS Code settings;
- execute WP06+;
- begin Release 1.9;
- stage, commit, push, branch, PR, merge, tag, or release.

---

## 17. Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE for:

- VENV1 — starting Python is governed CPython 3.13.15 x64;
- VENV2 — `.venv/` is protected by Git ignore before creation;
- VENV3 — `.venv` created using governed machine runtime;
- VENV4 — venv interpreter is Python 3.13.15;
- VENV5 — `sys.prefix` is project venv;
- VENV6 — `sys.base_prefix` is governed machine runtime;
- VENV7 — venv isolation is active (`prefix != base_prefix`);
- VENV8 — venv architecture is x64;
- VENV9 — PowerShell activation resolves venv interpreter;
- VENV10 — deactivation restores machine interpreter;
- VENV11 — direct venv interpreter invocation works without activation;
- VENV12 — venv pip resolves locally;
- VENV13 — global site-packages are not inherited;
- VENV14 — no contaminating `PYTHONHOME`/`PYTHONPATH`;
- VENV15 — NumPy/pandas/scikit-learn/Streamlit are not installed into the venv;
- VENV16 — delete/recreate proof succeeds;
- VENV17 — `.venv` remains ignored after recreation;
- VENV18 — VS Code `.venv` discoverability/preference evidence is truthful;
- VENV19 — tracked repository mutations match manifest authority exactly;
- VENV20 — no unauthorized residue/processes remain.

---

## 18. Canonical Repository Verification

After final `.venv` creation and any manifest-authorized tracked change, run canonical repository validation.

Expected:

- Domain.Tests: 11/11;
- Application.Tests: 119/119;
- Infrastructure.Tests: 125/125;
- Architecture.Tests: 13/13;
- permanent total: 268/268;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS;
- Markdown links: PASS;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- conflict markers: 0;
- trailing whitespace: 0;
- schema: v3;
- dependency graph unchanged/acyclic;
- .NET package/project/reference delta: 0/0/0;
- `.venv` source-control leakage: 0.

If the governed permanent-test baseline has legitimately changed, reconcile it rather than falsifying 268.

---

## 19. Security and Isolation

Require:

- real credentials: 0;
- provider/network product calls: 0;
- global Python package installation: 0;
- project third-party package installation: 0;
- machine Python/PATH mutation: 0;
- unauthorized VS Code mutation: 0;
- `.venv` tracked/staged files: 0;
- temporary validation residue: 0;
- retained unexpected Python processes: 0.

Do not weaken PowerShell, Windows, Git, VS Code, or repository security.

---

## 20. GitHub Lifecycle

Only after WP05 passes:

1. transition #215 to In Progress if needed;
2. add concise environment-foundation evidence;
3. close #215;
4. set #215 Project Status to Done.

Final expected state:

- #211–#215: CLOSED / Done;
- #216–#223: OPEN / Backlog;
- milestone #56: OPEN, 8 open / 5 closed;
- Project membership: 13/13;
- duplicates: 0;
- Priority/Release/Area unchanged;
- dependency chain unchanged.

Do not transition #216 automatically.

---

## 21. Stop Conditions

Stop with:

`RELEASE 1.8 WP05 BLOCKED`

if:

- starting repository/GitHub/runtime state is inconsistent;
- an existing `.venv` has unknown provenance;
- `.venv` cannot be safely ignored before creation;
- venv is created from the wrong interpreter;
- PowerShell activation would require unsafe/global policy weakening;
- environment isolation cannot be proven;
- delete/recreate cannot complete cleanly;
- VS Code requires unauthorized settings/tool changes;
- third-party package installation would be required;
- canonical validation fails;
- unexpected schema/package/project/reference drift exists;
- WP06+ work is required to claim success.

Report exact partial state and the smallest corrective authority required.

Do not blindly delete an ambiguous pre-existing environment.

---

## 22. Required Execution Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- GitHub state;
- machine Python;
- `.venv` pre-state;
- ignore pre-state.

### Creation
- creation command/semantic operation;
- interpreter provenance;
- Python version/architecture;
- `sys.prefix`/`sys.base_prefix`;
- pip/site-package state.

### PowerShell
- activation;
- interpreter resolution;
- deactivation;
- direct invocation;
- execution-policy impact.

### Isolation
- system-site-packages;
- environment variables;
- project/global package state.

### Re-Creation
- first environment validation;
- deletion;
- cleanup;
- recreation;
- final environment state.

### VS Code
- `.venv` discoverability/preference evidence;
- any manual UI boundary;
- portability.

### Validation
- VENV1–VENV20;
- 268/268 tests;
- build;
- formatting;
- Gitleaks;
- docs/diff;
- schema/graph;
- source-control leakage;
- residue.

### Mutation Accounting
- `.venv`;
- tracked repository files;
- machine Python/PATH;
- global packages;
- project packages;
- VS Code;
- production/tests/schema;
- .NET packages/projects/references;
- Git;
- GitHub.

### Final State
- #215 lifecycle;
- milestone #56;
- next authorized WP.

---

## 23. Completion Markers

On success end exactly:

`RELEASE 1.8 WP05 COMPLETE`

`PROJECT PYTHON VIRTUAL ENVIRONMENT: .venv / PYTHON 3.13.15 / ISOLATED`

`NEXT AUTHORIZED WORK PACKAGE: WP06 — Python Dependency Governance & Environment Rules — GitHub issue #216`

Do not execute WP06 automatically.

If blocked end exactly:

`RELEASE 1.8 WP05 BLOCKED`

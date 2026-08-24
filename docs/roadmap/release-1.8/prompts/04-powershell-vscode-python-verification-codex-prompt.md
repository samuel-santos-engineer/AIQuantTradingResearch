# Release 1.8 WP04 — PowerShell & VS Code Python Verification — Codex Authority

## 1. Mission

Execute Release 1.8 WP04 — **PowerShell & VS Code Python Verification** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#214`

Authoritative milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established Release 1.8 state:

- WP01/#211: CLOSED / Done;
- WP02/#212: CLOSED / Done;
- WP03/#213: CLOSED / Done;
- WP04/#214: OPEN / Backlog;
- #215–#223: OPEN / Backlog;
- milestone #56: OPEN, 10 open / 3 closed;
- machine-wide runtime: **official PSF CPython 3.13.15 amd64**;
- installation path: `C:\Program Files\Python313\`;
- real Python precedes the WindowsApps alias;
- `py`, base `pip`, and `venv` capability were verified;
- NumPy, pandas, scikit-learn, and Streamlit are absent globally;
- project-local virtual environment has not been created;
- canonical .NET baseline: 268/268;
- schema: v3.

WP04 proves that the governed machine-wide Python runtime is usable and deterministic from PowerShell and VS Code without prematurely creating the project environment or installing project dependencies.

---

## 2. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md`
- relevant environment/tooling/VS Code engineering documentation;
- WP03 completion evidence;
- GitHub issue #214;
- Project #2 Release 1.8 state.

Preserve the standing foundational-technology selection-record governance rule.

If WP04 introduces no new foundational external tool, do not create an unnecessary selection record.

If a foundational external VS Code extension/tool must be introduced rather than merely observed, stop unless its selection/governance is already authorized.

---

## 3. Mandatory Starting-State Gate

Before configuration or GitHub lifecycle mutation verify:

### Repository

- correct repository;
- branch: `main`;
- local HEAD equals `origin/main`;
- ahead/behind: `0/0`;
- staged paths: 0;
- no unexplained tracked changes;
- governed Release 1.8 documentation state is present.

Reconcile legitimate WP02/WP03 documentation state rather than assuming the original planning SHA.

### GitHub

- #211–#213: CLOSED / Done;
- #214: OPEN / Backlog;
- #215–#223: OPEN / Backlog;
- milestone #56: OPEN, 10 open / 3 closed;
- Project #2 membership: 13/13;
- duplicates: 0;
- WP03→WP04 dependency exists;
- fields/dependency chain remain authoritative.

### Python

Verify without mutation:

- `python --version` reports `Python 3.13.15`;
- `python -c "import sys; print(sys.executable)"` resolves the governed real interpreter;
- expected executable is `C:\Program Files\Python313\python.exe` or path-equivalent canonical form;
- `py -3.13 --version` resolves Python 3.13.15 where the launcher is supported;
- `python -m pip --version` succeeds;
- `python -m venv --help` succeeds;
- WindowsApps does not win normal `python` resolution;
- no project `.venv` exists;
- NumPy/pandas/scikit-learn/Streamlit remain absent globally.

Stop on unexplained divergence.

---

## 4. PowerShell Verification

Prove deterministic Python behavior from PowerShell.

Use at least:

1. the current PowerShell session;
2. a newly spawned/fresh PowerShell process;
3. a neutral working directory outside the repository.

For each relevant context verify:

- `python --version`;
- `python -c "import sys; print(sys.executable)"`;
- `python -c "import platform; print(platform.architecture())"`;
- `where.exe python`;
- `Get-Command python -All`;
- `py -3.13 --version` where supported;
- base pip visibility;
- venv capability.

Require:

- Python 3.13.15;
- x64 runtime;
- real CPython path is deterministic;
- repository working directory is not required;
- no WindowsApps alias ambiguity affects execution.

Do not modify PowerShell profiles merely to make validation pass.

---

## 5. PowerShell Environment Integrity

Inspect relevant environment state:

- User PATH;
- Machine PATH;
- process PATH;
- Python-related environment variables;
- `PYTHONHOME`;
- `PYTHONPATH`;
- launcher-related configuration where safely observable.

Expected:

- no repository-specific Python path is globally injected;
- no unexpected `PYTHONHOME`;
- no unexpected global `PYTHONPATH`;
- machine Python installation is discoverable normally;
- project dependencies are not exposed globally.

If environment variables could contaminate future project isolation, stop and report them rather than deleting unrelated user configuration automatically.

---

## 6. VS Code Installation and CLI Discovery

Verify the installed VS Code environment without installing or upgrading VS Code.

Where available, record:

- VS Code version;
- architecture;
- `code --version`;
- executable path;
- whether `code` is available from PowerShell;
- current workspace/repository settings relevant to Python;
- installed extensions relevant to Python discovery.

Do not modify unrelated VS Code settings.

Do not install extensions automatically unless the authoritative Release 1.8 plan explicitly assigns that action to WP04.

---

## 7. VS Code Python Extension Boundary

Determine whether an official Microsoft Python extension is already installed and usable.

If installed:

- record its extension identifier and version;
- verify it can discover or be directed to the governed Python 3.13.15 runtime;
- do not upgrade it merely because a newer version exists.

If not installed:

- inspect the accepted Release 1.8 plan/file manifest.

If WP04 explicitly authorizes installing it, apply the standing foundational-tool governance rule before installation and create/update the manifest-authorized selection evidence.

If installation is not explicitly authorized, stop and report the missing prerequisite rather than silently introducing a new foundational external tool.

Do not install arbitrary Python/ML/Notebook extensions.

---

## 8. VS Code Interpreter Verification

Prove that VS Code can use the governed machine interpreter as the current pre-venv Python runtime.

Acceptance evidence should establish, using repository-native/CLI/configuration mechanisms where possible:

- Python 3.13.15 is discoverable;
- interpreter provenance points to `C:\Program Files\Python313\python.exe`;
- WindowsApps is not selected as the effective interpreter;
- no stale interpreter from another environment is selected;
- opening an integrated PowerShell terminal can resolve the governed Python runtime;
- no project-local virtual environment is required yet.

Prefer observable, reproducible evidence over assumptions about VS Code UI state.

If a manual UI action is inherently required, provide the exact smallest human action and stop before claiming automated proof that did not occur.

---

## 9. Repository Interpreter Policy

WP04 may establish the **policy** for interpreter selection that later WPs will implement:

### Before project venv exists

The machine-wide Python 3.13 runtime is the bootstrap interpreter.

### After project venv exists

The repository/project-local venv becomes the preferred interpreter for AIQuantTradingResearch Python development and execution.

The machine-wide runtime remains the underlying runtime source, not the location for project packages.

Do not hard-code `C:\Program Files\Python313\python.exe` into repository settings if that would make the repository non-portable.

Do not commit user-specific absolute paths.

Prefer portable project semantics.

---

## 10. VS Code Settings Mutation Rule

Inspect `.vscode/` and existing repository conventions before any change.

Only make a repository-owned VS Code settings/recommendations change if:

- `RELEASE_1.8_FILE_MANIFEST.md` explicitly authorizes it for WP04; and
- it is portable; and
- it does not encode user-specific absolute paths; and
- it does not prematurely reference a project venv that does not yet exist unless the accepted plan explicitly defines a future portable path.

If no WP04 settings file is manifest-owned, make no repository settings mutation.

Never modify global/user VS Code settings without explicit human authorization.

---

## 11. Integrated Terminal Verification

Where VS Code CLI capabilities permit deterministic validation, prove that an integrated-terminal-equivalent environment will inherit the correct machine runtime.

At minimum reconcile:

- shell = PowerShell where repository conventions expect it;
- process PATH contains the governed machine Python path;
- `python` resolution is not dependent on the Codex sandbox path;
- no Codex-specific environment injection is being mistaken for normal VS Code behavior.

If true VS Code integrated-terminal execution cannot be programmatically proven, distinguish:

- CLI/process evidence;
- configuration evidence;
- any remaining manual UI verification.

Do not fabricate UI verification.

---

## 12. Project-Venv Non-Creation Gate

WP04 must prove it did not preempt later project-environment work.

Require absence of newly created repository-local:

- `.venv/`;
- `venv/`;
- `env/`;
- Python dependency installation directories;
- Python caches attributable to implementation;
- dependency manifests not authorized by WP04;
- Streamlit configuration;
- ML implementation.

A disposable external environment is unnecessary because WP03 already proved `venv`.

Do not create one unless the authoritative plan specifically requires a fresh verification; if used, it must be outside the repository and removed completely.

---

## 13. Global Dependency Cleanliness

Reconfirm that the base machine runtime has not gained project dependencies.

Verify absence of:

- NumPy;
- pandas;
- scikit-learn (`sklearn`);
- Streamlit.

Use non-mutating inspection.

Do not install or uninstall them in WP04.

---

## 14. Repository Evidence

Follow `RELEASE_1.8_FILE_MANIFEST.md` exactly.

If WP04 owns a durable environment/verification document, create or update only that artifact with:

- PowerShell verification evidence;
- VS Code verification evidence;
- interpreter policy;
- observed extension state;
- any manual verification boundary;
- environment cleanliness.

If no WP04 artifact is authorized, do not invent one.

Do not duplicate the responsibilities of `PYTHON_RUNTIME_SELECTION.md` or `PYTHON_RUNTIME_COMPATIBILITY.md`.

---

## 15. Explicit Non-Goals

Do not:

- install another Python runtime;
- change the governed Python 3.13 minor;
- upgrade Python without separate authority;
- create the project venv;
- install NumPy/pandas/scikit-learn/Streamlit;
- install Jupyter or notebook tooling;
- implement `.NET ↔ Python` integration;
- create Python use cases;
- create Streamlit UI;
- implement ML;
- change schema;
- add .NET packages;
- change project references;
- modify global/user VS Code settings;
- modify PowerShell profiles;
- execute WP05+;
- begin Release 1.9;
- stage, commit, push, branch, PR, merge, tag, or release.

---

## 16. Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE for:

- V1 — current PowerShell resolves Python 3.13.15;
- V2 — fresh PowerShell resolves Python 3.13.15;
- V3 — neutral-directory PowerShell resolves Python 3.13.15;
- V4 — resolved interpreter provenance is official machine CPython;
- V5 — runtime architecture is x64;
- V6 — WindowsApps alias does not win;
- V7 — `py -3.13` resolves correctly where applicable;
- V8 — base pip visible;
- V9 — venv capability visible;
- V10 — no contaminating `PYTHONHOME`;
- V11 — no contaminating `PYTHONPATH`;
- V12 — VS Code installation/CLI identified;
- V13 — Python extension state identified;
- V14 — VS Code can discover/use governed interpreter or exact manual boundary is reported;
- V15 — no user-specific absolute interpreter path committed;
- V16 — integrated-terminal environment evidence is truthful and bounded;
- V17 — project venv remains absent;
- V18 — NumPy/pandas/scikit-learn/Streamlit remain absent globally;
- V19 — repository mutations match file-manifest authority exactly;
- V20 — temporary residue is zero.

---

## 17. Canonical Repository Verification

Run repository-native verification after any manifest-authorized documentation/configuration change.

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
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- schema: v3;
- dependency graph unchanged/acyclic;
- package/project/reference delta: 0/0/0.

Reconcile a legitimately changed governed count rather than falsifying 268.

---

## 18. Security and Isolation

Require:

- provider/network product calls: 0;
- real credentials: 0;
- global project-package installation: 0;
- project venv creation: 0;
- unauthorized extension installation: 0;
- unauthorized global/user settings mutation: 0;
- temporary process/file residue: 0.

Do not weaken Windows, PowerShell, VS Code, or repository security settings.

---

## 19. GitHub Lifecycle

Only after all WP04 gates pass:

1. transition #214 to In Progress if needed;
2. add concise PowerShell/VS Code verification evidence;
3. close #214;
4. set #214 Project Status to Done.

Final expected state:

- #211–#214: CLOSED / Done;
- #215–#223: OPEN / Backlog;
- milestone #56: OPEN, 9 open / 4 closed;
- Project membership: 13/13;
- duplicates: 0;
- Priority/Release/Area unchanged;
- dependency chain unchanged.

Do not transition #215 automatically.

---

## 20. Stop Conditions

Stop with:

`RELEASE 1.8 WP04 BLOCKED`

if:

- Python 3.13.15 no longer resolves deterministically;
- WindowsApps wins effective resolution;
- contaminating environment state cannot be safely reconciled;
- VS Code verification requires an unauthorized foundational extension/tool installation;
- required VS Code state cannot be truthfully verified;
- repository settings would require user-specific absolute paths;
- project venv/package installation would be required;
- canonical verification fails;
- unexpected schema/package/project/reference drift exists;
- WP05+ work would be required to claim success.

Report the exact blocker, proven state, and smallest corrective authority required.

---

## 21. Required Execution Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- ahead/behind;
- GitHub lifecycle;
- Python machine state.

### PowerShell
- current shell;
- fresh shell;
- neutral directory;
- version/path/architecture;
- PATH/alias behavior;
- launcher/pip/venv;
- Python environment variables.

### VS Code
- version/path;
- CLI state;
- Python extension state;
- interpreter discovery/selection evidence;
- integrated-terminal evidence;
- any manual verification boundary.

### Policy
- bootstrap machine-interpreter policy;
- future project-venv preference;
- portability safeguards.

### Validation
- V1–V20;
- 268/268 tests;
- build;
- formatting;
- Gitleaks;
- links/diff;
- schema/graph;
- global dependency cleanliness;
- residue.

### Mutation Accounting
- repository;
- VS Code repository settings;
- VS Code user/global settings;
- PowerShell;
- machine Python;
- project venv/packages;
- Git;
- GitHub.

### Final State
- #214 lifecycle;
- milestone #56;
- next authorized WP.

---

## 22. Completion Markers

On success end exactly:

`RELEASE 1.8 WP04 COMPLETE`

`POWERSHELL & VS CODE PYTHON VERIFICATION: PASS`

`NEXT AUTHORIZED WORK PACKAGE: WP05 — Python Virtual Environment Foundation — GitHub issue #215`

Do not execute WP05 automatically.

If blocked end exactly:

`RELEASE 1.8 WP04 BLOCKED`

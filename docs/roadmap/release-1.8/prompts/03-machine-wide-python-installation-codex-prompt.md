# Release 1.8 WP03 — Machine-Wide Python Installation — Codex Authority

## 1. Mission

Execute Release 1.8 WP03 — **Machine-Wide Python Installation** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#213`

Authoritative milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established Release 1.8 decisions:

- Python runtime target: **Python 3.13**;
- patch policy: install the latest secure/current supported patch available within the Python 3.13 minor line at execution time;
- Python runtime ownership: Windows machine scope;
- project package ownership: project-local isolated environment in a later WP;
- NumPy, pandas, scikit-learn, and Streamlit must not be installed globally;
- exact package versions remain deferred to later dependency governance.

WP01 and WP02 are complete. The WP02 documentation-completion correction is complete.

WP03 is the first Release 1.8 work package explicitly authorized to mutate the developer workstation.

Its responsibility is limited to installing and proving the selected CPython runtime at machine scope.

---

## 2. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md`
- relevant repository engineering/environment/security documentation;
- GitHub issue #213;
- WP01/WP02 completion evidence;
- the WP02 documentation-completion evidence.

Also read the selection records for NumPy, pandas, scikit-learn, and Streamlit sufficiently to preserve their project-local dependency boundaries.

Do not reinterpret Python 3.13 selection.

---

## 3. Mandatory Starting-State Gate

Before workstation mutation verify:

### Repository

- repository identity is correct;
- branch is `main`;
- local HEAD equals `origin/main`;
- ahead/behind is `0/0`;
- staged paths: 0;
- no unexplained tracked changes;
- expected governed Release 1.8 documentation state is present.

Do not require the original Release 1.8 planning SHA if accepted WP02 documentation has legitimately advanced the working repository state. Reconcile the actual governed state without performing Git integration.

### GitHub

- #211: CLOSED / Done;
- #212: CLOSED / Done;
- #213: OPEN / Backlog;
- #214–#223: OPEN / Backlog;
- milestone #56: OPEN, 11 open / 2 closed;
- Project #2 Release 1.8 membership/fields/dependencies remain valid;
- WP02→WP03 dependency exists.

### Technical

- schema remains v3;
- permanent .NET baseline remains 268;
- no project Python environment exists unless explicitly expected by authority;
- no NumPy/pandas/scikit-learn/Streamlit installation has been introduced for this project;
- no Release 1.9 implementation exists.

Stop on unexplained state.

---

## 4. Pre-Installation Machine Inventory

Before installing anything, record read-only evidence for:

- Windows edition/version/architecture;
- current user context;
- PowerShell version;
- `where.exe python`;
- `Get-Command python -All`;
- `python --version`, if resolvable;
- `where.exe py`;
- `Get-Command py -All`;
- `py --version`, if resolvable;
- `where.exe pip`;
- `Get-Command pip -All`;
- current relevant User PATH entries;
- current relevant Machine PATH entries;
- WindowsApps Python aliases visible in command resolution;
- installed Python entries discoverable through safe Windows mechanisms;
- installed-package-manager state relevant to Python, if a package manager is considered.

Expected WP01 observation was:

- WindowsApps alias only for `python`;
- no usable `py`;
- no usable `pip`.

If a real CPython installation now exists, stop before installing another copy and reconcile whether it already satisfies the governed Python 3.13 requirement.

---

## 5. Installation Source and Provenance

Install only an official/trusted CPython distribution consistent with `PYTHON_RUNTIME_SELECTION.md`.

Preferred source hierarchy:

1. official Python Windows distribution/install mechanism documented by Python;
2. a trusted Windows package-manager package whose publisher/source can be proven to map to the official Python distribution.

Do not download installers from third-party mirrors, blogs, file-hosting sites, or unofficial repackagers.

Before installation, determine the exact Python 3.13 patch version that will be installed.

Verify it belongs to the governed 3.13 minor line.

Record:

- exact patch version;
- source;
- publisher/provenance;
- installation mechanism;
- architecture.

Do not silently select Python 3.14+ or downgrade to 3.12 or earlier.

If the latest Python 3.13 patch cannot be safely established or installed, stop.

---

## 6. Machine-Scope Requirement

The user's intent is a Python runtime available at Windows machine scope, not an AIQuantTradingResearch-only runtime.

Install Python 3.13 so it is available independently of this repository.

Prefer a true all-users/machine-scope installation when supported safely by the selected official installation mechanism.

If administrative elevation is required:

- use the normal Windows elevation/approval mechanism;
- do not bypass OS security;
- do not disable UAC;
- do not weaken execution/security policies globally.

After installation, prove the actual installation scope from filesystem/registry/installer evidence where possible.

If the available official mechanism cannot satisfy machine scope without unsafe actions, stop and report the constraint.

---

## 7. PATH and Command Resolution

The installed real CPython must be discoverable predictably.

After installation, validate command resolution in a **fresh PowerShell process**, not only the shell that launched the installer.

Require evidence for:

- `python --version`;
- `python -c "import sys; print(sys.executable)"`;
- `python -c "import sys; print(sys.version)"`;
- `where.exe python`;
- `Get-Command python -All`.

The real CPython installation must take precedence over or otherwise avoid accidental use of the WindowsApps Store alias.

Do not delete unrelated WindowsApps infrastructure.

If Windows App Execution Aliases require a user-controlled UI change that cannot be safely automated, report it explicitly and prove whether PATH ordering already makes the real interpreter deterministic.

Do not make broad unrelated PATH changes.

---

## 8. Python Launcher

Determine whether the selected official Python 3.13 installation supplies or supports the Windows Python launcher.

If `py` is available after installation, verify:

- `py --version`;
- `py -0p` or the current supported equivalent;
- `py -3.13 --version`;
- interpreter path/provenance.

If the current official Python installation model no longer supplies the traditional launcher by default, do not install unrelated legacy tooling merely to satisfy an obsolete assumption. Record the official behavior and use the supported mechanism.

The acceptance requirement is deterministic access to Python 3.13; `py` is required only where appropriate to the official selected installation model.

---

## 9. Base pip Verification

Verify whether the official installation provides usable base `pip`.

If present, run read-only commands such as:

- `python -m pip --version`;
- launcher-equivalent `-m pip --version` where appropriate.

Do not upgrade pip unless the authoritative Release 1.8 plan explicitly requires it in WP03.

Do not install any project package globally.

Specifically prohibit global installation of:

- NumPy;
- pandas;
- SciPy;
- scikit-learn;
- Streamlit;
- Jupyter;
- ML libraries;
- AIQuantTradingResearch-specific dependencies.

The machine-wide Python runtime is infrastructure; application libraries remain project-owned.

---

## 10. Standard-Library venv Capability

Verify that the installed runtime can create virtual environments without creating the AIQuantTradingResearch project environment yet.

Preferred proof:

- verify `python -m venv --help` or equivalent non-mutating capability evidence.

If stronger proof is required by the accepted execution plan, create only a disposable temporary venv outside the repository, validate it, and remove it completely.

A disposable proof must:

- be outside the repository;
- install no third-party packages;
- prove its interpreter is Python 3.13;
- prove isolation;
- be deleted completely;
- leave residue 0.

Do not create `.venv` or `venv` inside AIQuantTradingResearch in WP03.

---

## 11. Fresh-Shell Verification

Because environment-variable propagation can differ between existing and new processes, launch a fresh PowerShell process after installation and verify at minimum:

- Python command resolution;
- exact Python version;
- executable path;
- pip module visibility;
- venv capability.

If the current parent shell retains stale PATH state, do not misclassify the installation as failed until a fresh process is tested.

Record any shell-restart or VS Code-restart requirement for later WP04.

Do not modify VS Code configuration in WP03.

---

## 12. Installation-Scope Verification

Prove as far as safely possible:

- Python is not installed under the repository;
- Python is not dependent on the repository working directory;
- installation location is consistent with machine/all-users scope;
- interpreter can be invoked from a neutral directory;
- Python 3.13 remains available when launched outside the repository.

Use a neutral directory for at least one verification command.

Do not create persistent test files.

---

## 13. Global Dependency Cleanliness

After installation, inspect the base interpreter package state.

The base installation may contain packaging/bootstrap components supplied by Python.

Require that project-specific scientific/UI dependencies were not introduced by WP03.

Specifically determine whether these are absent from the base environment unless supplied unexpectedly by the official distribution:

- `numpy`;
- `pandas`;
- `sklearn`;
- `streamlit`.

Use non-mutating inspection.

If any are unexpectedly present, report their provenance. Do not uninstall automatically unless explicitly authorized.

---

## 14. Repository Isolation

WP03 workstation mutation must not mutate repository product content.

Expected repository deltas:

- production code: 0;
- permanent tests: 0;
- schema: 0;
- .NET packages: 0;
- projects: 0;
- project references: 0;
- Python project manifests: 0;
- project venv: 0;
- Streamlit code: 0;
- ML code: 0.

Follow `RELEASE_1.8_FILE_MANIFEST.md` exactly if it assigns a WP03 evidence artifact.

If a manifest-authorized WP03 documentation artifact exists, update only that artifact with installation evidence. Otherwise do not invent a repository file.

---

## 15. Security Rules

Do not:

- expose credentials;
- paste secrets into commands/files;
- disable antivirus;
- disable firewall;
- disable UAC;
- weaken system security globally;
- bypass certificate validation;
- use unofficial binary mirrors;
- run arbitrary remote scripts without provenance;
- use `--dangerously-bypass-approvals-and-sandbox` merely to avoid legitimate elevation/security boundaries.

Normal administrator elevation for a governed machine-wide official Python installation is allowed.

---

## 16. Canonical Repository Verification

After workstation installation, prove the existing platform remains unaffected.

Run repository-native canonical verification.

Expected:

- Domain.Tests: 11/11;
- Application.Tests: 119/119;
- Infrastructure.Tests: 125/125;
- Architecture.Tests: 13/13;
- total: 268/268;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS;
- Markdown links: PASS where governed;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- schema: v3;
- dependency graph unchanged and acyclic;
- package/project/reference delta: 0/0/0.

If current governed test counts differ legitimately from 268, reconcile rather than falsifying the result.

---

## 17. Environment Validation Matrix

Report explicit PASS/FAIL/NOT-APPLICABLE for:

- E1 — official/trusted Python provenance;
- E2 — exact Python 3.13 patch selected;
- E3 — machine/all-users installation scope;
- E4 — x64 architecture;
- E5 — real interpreter command resolution;
- E6 — WindowsApps alias does not win resolution;
- E7 — fresh PowerShell resolves Python 3.13;
- E8 — neutral-directory invocation succeeds;
- E9 — base pip visibility;
- E10 — venv capability;
- E11 — launcher behavior documented/verified;
- E12 — NumPy absent globally;
- E13 — pandas absent globally;
- E14 — scikit-learn absent globally;
- E15 — Streamlit absent globally;
- E16 — repository mutation limited to manifest authority;
- E17 — project venv absent;
- E18 — schema/packages/projects/references unchanged;
- E19 — canonical .NET verification passes;
- E20 — temporary installation-validation residue is zero.

---

## 18. GitHub Lifecycle

Only after WP03 passes:

1. transition #213 to In Progress if needed;
2. add concise installation/validation evidence;
3. close #213;
4. set #213 Project Status to Done.

Final expected GitHub state:

- #211: CLOSED / Done;
- #212: CLOSED / Done;
- #213: CLOSED / Done;
- #214–#223: OPEN / Backlog;
- milestone #56: OPEN, 10 open / 3 closed;
- Project membership: 13/13;
- duplicates: 0;
- Priority/Release/Area unchanged;
- dependency chain unchanged.

Do not transition #214 automatically.

---

## 19. Stop Conditions

Stop with:

`RELEASE 1.8 WP03 BLOCKED`

if:

- the starting repository/GitHub state is materially inconsistent;
- a conflicting real Python installation exists and cannot be safely reconciled;
- official/trusted Python 3.13 provenance cannot be established;
- the governed 3.13 line is unavailable;
- machine-wide installation would require unsafe security bypass;
- deterministic command resolution cannot be established;
- Python installation unexpectedly mutates repository/product state;
- project dependencies would need global installation;
- canonical .NET verification fails;
- WP04+ work is required to claim WP03 success.

Report exact partial machine state and the smallest corrective authority required.

Do not uninstall or roll back automatically after an ambiguous partial installation unless the authority clearly proves rollback is safe.

---

## 20. Required Execution Report

Report:

### Starting State

- repository/branch/HEAD/origin;
- ahead/behind;
- GitHub lifecycle;
- pre-install Python/launcher/pip resolution.

### Installation

- exact Python version;
- source/provenance;
- architecture;
- installation mechanism;
- installation path;
- installation scope;
- elevation used.

### Resolution

- `python` result;
- interpreter executable;
- `where.exe python`;
- WindowsApps interaction;
- launcher result;
- fresh-shell result;
- neutral-directory result.

### Base Tooling

- pip result;
- venv result;
- global NumPy/pandas/scikit-learn/Streamlit state.

### Validation

- E1–E20;
- .NET test counts;
- build;
- formatting;
- Gitleaks;
- links/diff;
- schema;
- dependency graph;
- residue.

### Mutation Accounting

- machine Python;
- PATH/environment;
- repository;
- production/tests;
- schema;
- packages/projects/references;
- project Python dependencies;
- Git;
- GitHub.

### Final State

- #213 lifecycle;
- milestone #56;
- #214 next authorization.

---

## 21. Completion Markers

On success end exactly:

`RELEASE 1.8 WP03 COMPLETE`

`MACHINE-WIDE PYTHON RUNTIME: 3.13.<patch>`

`NEXT AUTHORIZED WORK PACKAGE: WP04 — PowerShell & VS Code Python Verification — GitHub issue #214`

Do not execute WP04 automatically.

If blocked end exactly:

`RELEASE 1.8 WP03 BLOCKED`

# Release 1.8 WP04 — VS Code Python Extension Selection & Installation — Corrective Codex Authority

## 1. Mission

Perform the narrow corrective action required to unblock Release 1.8 WP04 — **PowerShell & VS Code Python Verification**.

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Blocked work package:

`#214 — WP04 — PowerShell & VS Code Python Verification`

Authoritative milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established state:

- WP01/#211: CLOSED / Done;
- WP02/#212: CLOSED / Done;
- WP03/#213: CLOSED / Done;
- WP04/#214: OPEN / Backlog and BLOCKED;
- #215–#223: OPEN / Backlog;
- milestone #56: OPEN, 10 open / 3 closed;
- official PSF CPython 3.13.15 amd64 is installed machine-wide;
- Python resolves deterministically from PowerShell;
- VS Code 1.134.0 x64 and its CLI are available;
- Microsoft Python VS Code extension is not installed;
- no repository workspace Python settings currently exist;
- no project-local venv exists;
- NumPy, pandas, scikit-learn, and Streamlit are absent globally.

WP04 stopped because introducing a new foundational VS Code extension requires explicit engineering selection governance.

This authority provides exactly that corrective authority.

It SHALL NOT complete or close WP04. After this correction succeeds, the existing WP04 authority must be resumed from its blocked extension gate.

---

## 2. Standing Governance Rule

Apply the project-wide rule:

> Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.

The Microsoft Python VS Code extension is treated as a foundational development tool and therefore requires an explicit selection record before installation.

---

## 3. Mandatory Starting-State Gate

Before mutation, read completely:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`
- existing foundational technology/tool selection records;
- relevant repository engineering/tooling/VS Code documentation;
- WP03 completion evidence;
- WP04 blocked execution evidence;
- GitHub issue #214 and Project #2 state.

Verify:

### Repository
- correct repository;
- branch `main`;
- local HEAD equals `origin/main`;
- ahead/behind `0/0`;
- staged paths `0`;
- no unexplained tracked changes;
- governed Release 1.8 documentation state is present.

### GitHub
- #211–#213: CLOSED / Done;
- #214: OPEN / Backlog;
- #215–#223: OPEN / Backlog;
- milestone #56: OPEN, 10 open / 3 closed;
- Project #2 membership/fields/dependency chain remain authoritative.

### Runtime/Tooling
- Python 3.13.15 still resolves to the governed machine installation;
- VS Code 1.134.0 x64 remains available;
- `code` CLI is available;
- `ms-python.python` is not currently installed;
- no project `.venv` exists;
- no project scientific/UI dependencies have been installed globally.

If `ms-python.python` is already installed when this authority starts, do not reinstall it. Reconcile its exact publisher/version/provenance and stop if its state cannot be safely governed.

---

## 4. Selection Target

The candidate tool is the official **Python extension for Visual Studio Code published by Microsoft**.

Expected extension identifier:

`ms-python.python`

Do not assume identity solely from the display name.

Before installation, verify through authoritative Microsoft/Visual Studio Marketplace/VS Code extension metadata that:

- extension identifier is `ms-python.python`;
- publisher is Microsoft;
- purpose includes Python language/development support in VS Code;
- it is appropriate for interpreter discovery/selection for the governed CPython runtime;
- source/provenance is trusted.

Do not install similarly named third-party extensions.

---

## 5. Required Engineering Selection Record

Create:

`docs/architecture/implementation/VSCODE_PYTHON_EXTENSION_SELECTION.md`

unless repository-native conventions require an equivalent canonical filename/location.

The record must include:

1. purpose;
2. selected tool and exact extension identifier;
3. publisher/provenance;
4. reasons for selection;
5. alternatives considered;
6. reasons alternatives are not selected now;
7. accepted trade-offs;
8. version policy;
9. architectural/tooling boundaries;
10. installation scope;
11. privacy/security considerations at an appropriate engineering level;
12. validation expectations;
13. reconsideration triggers;
14. relationship to Python 3.13 and Release 1.8;
15. official upstream references.

Keep it concise and decision-oriented.

---

## 6. Selection Rationale Requirements

The selection record must explain why `ms-python.python` is appropriate for this repository, including:

- official Microsoft VS Code Python tooling;
- interpreter discovery/selection;
- Python editing/development integration;
- compatibility with the machine-wide Python bootstrap model;
- compatibility with the future project-local venv model;
- suitability for PowerShell/VS Code verification;
- avoidance of repository-specific proprietary runtime coupling.

Do not claim the extension owns Python itself.

The machine-wide PSF CPython installation remains the runtime authority.

---

## 7. Alternatives and Trade-Offs

Evaluate reasonable alternatives at a concise engineering level, such as:

- no Python extension / terminal-only Python workflow;
- manually managed interpreter invocation;
- other Python editor/IDE tooling;
- additional VS Code Python ecosystem extensions where relevant.

Do not turn this correction into an IDE survey.

Accepted trade-offs should include, where applicable:

- additional VS Code extension dependency;
- extension lifecycle/version drift;
- Microsoft-specific development-tool integration;
- potential extension behavior changes across releases;
- distinction between developer tooling and runtime/application architecture.

The extension must not become a production dependency.

---

## 8. Version Policy

Do not invent an arbitrary permanent exact extension pin unless existing repository policy requires one.

Record the installed version as evidence.

Adopt a governed policy appropriate to developer tooling, such as:

- use a current stable official Microsoft release compatible with the installed VS Code;
- upgrades are intentional tooling maintenance, not automatic architecture changes;
- major behavioral/security/compatibility changes require reevaluation;
- repository documentation should record material compatibility constraints rather than unnecessarily pinning a user-global extension forever.

If the accepted repository governance already defines a stricter tool-version policy, follow it.

---

## 9. Installation Scope

Install the extension through the installed VS Code CLI using the verified official identifier.

Expected mechanism:

`code --install-extension ms-python.python`

or the current supported equivalent.

Installation is authorized at the normal VS Code user-extension scope used by the installed VS Code instance.

This authority does NOT authorize:

- modifying unrelated VS Code extensions;
- installing Jupyter;
- installing Pylance separately unless it is an official dependency automatically managed by the selected extension and its provenance is reported;
- installing arbitrary extension packs;
- changing VS Code globally beyond what the official extension installation itself requires.

If the official extension automatically installs dependencies, enumerate them after installation and distinguish automatic trusted dependencies from separately selected foundational tools.

Do not independently install additional dependencies merely to satisfy assumptions.

---

## 10. Extension Provenance Verification

After installation verify at minimum:

- `code --list-extensions --show-versions` contains `ms-python.python@<version>`;
- publisher/identifier remains Microsoft/`ms-python.python`;
- extension location is consistent with the installed VS Code user-extension model;
- no similarly named unexpected Python extension was introduced.

Where available, inspect local extension metadata to verify publisher and identifier.

Record the exact installed extension version.

---

## 11. Python Discovery Verification

After installation, perform only the minimum verification needed to prove the blocker is removed.

Establish that the extension/VS Code environment can discover or support selection of:

`C:\Program Files\Python313\python.exe`

Python:

`3.13.15`

Do not claim UI selection if only CLI/configuration evidence exists.

If a manual VS Code UI action is required to select/confirm the interpreter, report the exact smallest human action rather than fabricating success.

The correction succeeds when the extension is governed, installed, provenance-verified, and the prior missing-extension prerequisite is removed. Full interpreter/terminal verification remains WP04 responsibility.

---

## 12. No Interpreter Hard-Coding

Do not commit:

- user-specific interpreter paths;
- `C:\Program Files\Python313\python.exe` as a repository-wide permanent setting;
- machine-specific extension paths.

The existing policy remains:

- machine Python is the bootstrap interpreter before project venv creation;
- future project-local venv becomes the preferred project interpreter;
- repository configuration must remain portable.

---

## 13. File Manifest Reconciliation

Update `RELEASE_1.8_FILE_MANIFEST.md` with the smallest necessary change to govern:

- `VSCODE_PYTHON_EXTENSION_SELECTION.md`;
- any manifest-authorized documentation evidence required by this correction.

Preserve:

- WP01–WP13 sequence;
- issue numbering;
- dependency chain;
- Release 1.8 scope;
- WP04 ownership;
- WP05+ scope.

Classify this as a WP04 corrective governance artifact.

If repository navigation conventions require a reference to the new selection record, update only the minimum navigation surface.

---

## 14. Explicit Non-Goals

Do not:

- close #214;
- set #214 Done;
- execute the remaining WP04 verification authority;
- transition #215;
- create a project venv;
- install NumPy;
- install pandas;
- install scikit-learn;
- install Streamlit;
- install Jupyter/notebook tooling;
- install another Python runtime;
- modify Python 3.13.15;
- change PATH unless an unexpected defect requires separate authority;
- modify PowerShell profiles;
- hard-code interpreter paths;
- create Python production code;
- implement `.NET ↔ Python`;
- create Streamlit UI;
- implement ML;
- change schema;
- add .NET packages/projects/references;
- execute WP05+;
- begin Release 1.9;
- stage, commit, push, branch, PR, merge, tag, or GitHub Release.

---

## 15. Mutation Budget

Authorized mutations are limited to:

### Workstation
- install the verified official `ms-python.python` extension at normal VS Code user scope;
- automatic official extension dependencies only when introduced by the normal extension installation and explicitly reported.

### Repository
- `VSCODE_PYTHON_EXTENSION_SELECTION.md`;
- smallest necessary `RELEASE_1.8_FILE_MANIFEST.md` update;
- minimum repository-native navigation/governance reference if required.

### GitHub
- one concise corrective evidence comment on #214 after successful correction.

Expected:

- production code delta: 0;
- permanent test delta: 0;
- schema delta: 0;
- .NET package/project/reference delta: 0/0/0;
- Python runtime delta: 0;
- Python project-package delta: 0;
- project venv delta: 0;
- PowerShell delta: 0;
- Git transport delta: 0.

Do not stage repository changes.

---

## 16. Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE:

- C1 — starting repository/GitHub state reconciled;
- C2 — official extension identifier verified;
- C3 — Microsoft publisher/provenance verified;
- C4 — selection record created;
- C5 — rationale/alternatives/trade-offs documented;
- C6 — version policy documented;
- C7 — boundaries/reconsideration triggers documented;
- C8 — manifest reconciled;
- C9 — extension installed through trusted VS Code mechanism;
- C10 — exact installed version recorded;
- C11 — installed identifier/provenance read back;
- C12 — automatic extension dependencies enumerated;
- C13 — Python 3.13.15 machine runtime unchanged;
- C14 — governed interpreter remains discoverable;
- C15 — no user-specific interpreter path committed;
- C16 — project venv remains absent;
- C17 — NumPy/pandas/scikit-learn/Streamlit remain absent globally;
- C18 — production/schema/package/project/reference deltas remain zero;
- C19 — repository validation passes;
- C20 — temporary residue is zero.

---

## 17. Canonical Repository Validation

Run repository-native validation after the documentation correction/extension installation.

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
- terminal newlines: PASS;
- trailing whitespace: 0;
- conflict markers: 0;
- schema: v3;
- dependency graph unchanged/acyclic;
- .NET package/project/reference delta: 0/0/0.

If the governed repository baseline has legitimately changed, reconcile rather than falsifying expected counts.

---

## 18. GitHub Corrective Lifecycle

Issue #214 must remain OPEN / Backlog after this correction because WP04 itself has not yet completed.

After all correction gates pass:

- add one concise comment to #214 recording:
  - the selection record;
  - official extension identifier;
  - installed extension version;
  - provenance verification;
  - blocker removal;
  - instruction that WP04 must resume under its existing authority.

Do not close #214.
Do not set #214 Done.
Do not transition #215.

Expected final milestone state remains:

- milestone #56: OPEN, 10 open / 3 closed.

---

## 19. Stop Conditions

Stop with:

`RELEASE 1.8 WP04 VS CODE PYTHON EXTENSION CORRECTION BLOCKED`

if:

- the extension identity/publisher cannot be authoritatively verified;
- an unofficial or conflicting Python extension is present;
- installation requires unsafe security bypass;
- installation unexpectedly requires unrelated foundational tools without governance;
- automatic dependencies cannot be safely attributed;
- repository state cannot be reconciled;
- selection record would conflict with established architecture/governance;
- project venv/package work would be required;
- canonical validation fails;
- WP05+/Release 1.9 work would be required.

If installation partially succeeds before a blocker appears, do not blindly uninstall it. Report the exact partial state and request the smallest corrective authority.

---

## 20. Required Execution Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- GitHub state;
- Python state;
- VS Code state;
- extension absence/presence.

### Selection
- selected extension;
- identifier;
- publisher;
- official evidence;
- rationale;
- alternatives;
- trade-offs;
- version policy;
- boundaries;
- reconsideration triggers.

### Installation
- command/mechanism;
- exact installed version;
- extension path/scope;
- automatic dependencies.

### Blocker Removal
- interpreter discovery evidence;
- Python 3.13.15 preservation;
- any remaining manual UI boundary.

### Repository
- selection-record path;
- manifest/navigation changes;
- exact changed paths.

### Validation
- C1–C20;
- 268/268 tests;
- build;
- formatting;
- Gitleaks;
- links/diff;
- schema/graph;
- global dependency cleanliness;
- residue.

### Mutation Accounting
- VS Code;
- repository;
- Python runtime/packages;
- project venv;
- production/tests;
- schema;
- .NET packages/projects/references;
- Git;
- GitHub.

### Final State
- #214 remains OPEN / Backlog;
- #215 remains OPEN / Backlog;
- milestone #56 remains OPEN, 10 open / 3 closed.

---

## 21. Completion Markers

On success end exactly:

`RELEASE 1.8 WP04 VS CODE PYTHON EXTENSION CORRECTION COMPLETE`

`FOUNDATIONAL TOOL SELECTION RECORD: VSCODE_PYTHON_EXTENSION`

`NEXT AUTHORIZED ACTION: Resume WP04 — PowerShell & VS Code Python Verification from the blocked extension gate using the existing WP04 authority.`

Do not resume WP04 automatically.

If blocked end exactly:

`RELEASE 1.8 WP04 VS CODE PYTHON EXTENSION CORRECTION BLOCKED`

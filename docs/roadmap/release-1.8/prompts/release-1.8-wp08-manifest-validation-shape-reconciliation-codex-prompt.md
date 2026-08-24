# Release 1.8 WP08 — Manifest Path & Validation Shape Reconciliation — Corrective Codex Authority

## 1. Mission

Perform one narrowly scoped corrective reconciliation to unblock Release 1.8 WP08 — **Python Scientific Stack Validation Use Cases**.

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Blocked work package:

`#218 — WP08 — Python Scientific Stack Validation Use Cases`

Milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

WP08 stopped correctly because `RELEASE_1.8_FILE_MANIFEST.md` did not establish:

- one exact repository-owned Python validation root;
- whether WP08 validation must use scripts, tests, or both.

This authority resolves only that manifest ambiguity.

It does not execute WP08, implement Release 1.9 behavior, introduce a test framework, or authorize .NET↔Python integration.

---

## 2. Established Release 1.8 State

Preserve the accepted state:

- #211–#217: CLOSED / Done;
- #218: OPEN / Backlog;
- #219–#223: OPEN / Backlog;
- milestone #56: OPEN, 6 open / 7 closed;
- CPython 3.13.15 amd64 machine runtime;
- `.venv` isolated and Git-ignored;
- governed direct dependencies:
  - NumPy 2.5.1;
  - pandas 3.0.5;
  - scikit-learn 1.9.0;
  - Streamlit 1.61.1;
- `pip check`: PASS;
- global machine Python free of the four direct project packages;
- canonical .NET baseline: 268/268;
- schema: v3.

No WP08 validation files currently exist.

---

## 3. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_DEPENDENCY_GOVERNANCE.md`
- NumPy selection record;
- pandas selection record;
- scikit-learn selection record;
- Streamlit selection record;
- the existing WP08 full authority;
- WP08 blocked execution evidence;
- GitHub issue #218.

Do not reinterpret accepted Release 1.8 scope.

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
- governed WP07 dependency state is intact;
- `.venv` remains ignored.

### GitHub

- #211–#217: CLOSED / Done;
- #218: OPEN / Backlog;
- #219–#223: OPEN / Backlog;
- milestone #56: OPEN, 6 open / 7 closed;
- Project #2 membership and dependency chain remain correct.

### Python

- Python 3.13.15;
- `.venv` isolated;
- exact WP07 pins preserved;
- `pip check` passes;
- no ungoverned package additions.

If any of these fail, stop before mutation.

---

## 5. Authorized Manifest Decision

Freeze the following exact WP08 repository structure:

```text
python/
└── validation/
    ├── scientific_stack_validation.py
    └── streamlit_validation_app.py
```

This is a **validation-only** Python area for Release 1.8.

It must not be interpreted as a production Python package root, application architecture, or Release 1.9 ML module structure.

---

## 6. Validation Shape Decision

WP08 SHALL use:

**deterministic executable validation scripts**

and SHALL NOT introduce a Python test framework.

Specifically:

- no pytest;
- no unittest package structure unless the existing WP08 authority later proves a standard-library assertion helper is needed within the scripts;
- no Python test-discovery architecture;
- no production package layout;
- no Jupyter notebooks.

The scripts themselves may use explicit assertions and exit codes to provide automated PASS/FAIL behavior.

Permanent Python test architecture remains deferred to the later dedicated test work package.

---

## 7. scientific_stack_validation.py Ownership

`python/validation/scientific_stack_validation.py` is authorized to validate:

- NumPy 2.5.1;
- pandas 3.0.5;
- scikit-learn 1.9.0.

It may contain only bounded deterministic validation use cases authorized by WP08.

It must not:

- use real market data;
- access network/providers;
- persist models;
- define production ML architecture;
- modify databases/schema;
- become a reusable production library by accident.

---

## 8. streamlit_validation_app.py Ownership

`python/validation/streamlit_validation_app.py` is authorized only as the smallest deterministic Streamlit validation surface needed by WP08.

It must remain:

- local;
- bounded;
- offline;
- deterministic;
- non-production;
- free of business/domain logic;
- free of real credentials;
- free of provider/network data.

It is not the future portfolio dashboard.

---

## 9. Architectural Boundary

Freeze these boundaries:

- `python/validation/` is Release 1.8 validation evidence;
- Domain remains Python-agnostic;
- Application remains free of Python runtime/library mechanics;
- no .NET project reference changes;
- no IPC/process integration in this correction;
- no Python production module structure is authorized here;
- WP09 remains owner of .NET↔Python boundary design.

Do not infer broader Python architecture from this validation path.

---

## 10. Manifest Reconciliation

Update:

`docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`

with the smallest necessary change to record:

- exact root: `python/validation/`;
- exact WP08 files:
  - `python/validation/scientific_stack_validation.py`
  - `python/validation/streamlit_validation_app.py`
- validation mechanism: executable deterministic scripts;
- no external Python test framework;
- validation-only/non-production classification;
- later permanent Python tests remain owned by their dedicated work package;
- WP09 retains ownership of .NET↔Python integration architecture.

Do not alter:

- WP count/order;
- issue numbering;
- dependency chain;
- Release 1.8 objectives;
- WP08 semantic scope;
- WP09+ ownership.

---

## 11. No New Foundational Tool

This correction explicitly chooses **no new external validation/test tool**.

The existing Python standard library plus already governed dependencies are sufficient.

Therefore no new foundational technology selection record is required.

If Codex determines a new external tool is necessary, stop and request separate authority.

---

## 12. Repository Mutation Budget

Authorized tracked mutation:

- `RELEASE_1.8_FILE_MANIFEST.md` only.

Do not create the two validation scripts under this corrective authority.

Their creation belongs to resumed WP08.

Expected deltas:

- production code: 0;
- validation code: 0;
- permanent tests: 0;
- `requirements.txt`: 0;
- package versions: 0;
- `.venv` packages: 0;
- schema: 0;
- .NET packages/projects/references: 0/0/0;
- Git transport: 0.

---

## 13. Explicit Prohibitions

Do not:

- create `python/validation/` yet;
- create either WP08 validation script;
- execute WP08;
- introduce pytest/unittest project structure;
- create production Python packages;
- change dependency versions;
- install packages;
- modify `.venv`;
- implement .NET↔Python integration;
- create Streamlit dashboard behavior;
- implement ML product logic;
- modify schema;
- stage/commit/push/branch/PR/merge/tag/release;
- begin WP09 or Release 1.9.

---

## 14. Validation

After manifest reconciliation require:

- manifest path/shape is unambiguous;
- exactly two WP08 validation files are authorized;
- script-vs-test decision is explicit;
- no production Python architecture is implied;
- WP09 ownership remains intact;
- no new foundational tool introduced;
- `requirements.txt` unchanged;
- direct versions unchanged;
- `.venv` untouched;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- terminal newline: PASS;
- trailing whitespace: 0;
- conflict markers: 0;
- Gitleaks: PASS;
- Markdown links: PASS where governed;
- schema: v3;
- package/project/reference delta: 0/0/0.

Canonical 268/268 .NET verification is required if repository governance requires it for manifest-only changes; otherwise report why it was not required. If run, it must pass.

---

## 15. GitHub Lifecycle

Issue #218 must remain OPEN / Backlog.

After successful correction:

- add one concise comment to #218 explaining that the manifest ambiguity was resolved;
- record exact path/shape:
  - `python/validation/scientific_stack_validation.py`
  - `python/validation/streamlit_validation_app.py`
- record executable-script validation model;
- record that no external test framework was introduced;
- state that WP08 must resume under its existing authority.

Do not close #218.
Do not set #218 Done.
Do not transition #219.

Milestone #56 remains OPEN, 6 open / 7 closed.

---

## 16. Stop Conditions

Stop with:

`RELEASE 1.8 WP08 MANIFEST VALIDATION SHAPE RECONCILIATION BLOCKED`

if:

- live repository state differs materially;
- the manifest already contains a conflicting exact structure;
- accepted planning requires a different WP08 path/mechanism;
- a new foundational tool would be required;
- manifest reconciliation would alter WP09+ ownership;
- validation fails.

Report exact blocker and smallest corrective authority required.

---

## 17. Required Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- #218/milestone state;
- current manifest ambiguity.

### Decision
- exact validation root;
- exact two files;
- executable-script validation model;
- no external test framework;
- non-production classification.

### Manifest
- exact lines/sections reconciled;
- WP08 ownership;
- WP09 boundary preservation.

### Validation
- diff/whitespace/newline;
- Gitleaks;
- links;
- schema;
- package/project/reference deltas;
- whether canonical .NET verification ran.

### Mutation Accounting
- manifest;
- validation code;
- Python packages;
- `.venv`;
- production/tests/schema;
- Git;
- GitHub.

### Final State
- #218 remains OPEN / Backlog;
- milestone #56 remains OPEN, 6 open / 7 closed.

---

## 18. Completion Markers

On success end exactly:

`RELEASE 1.8 WP08 MANIFEST VALIDATION SHAPE RECONCILIATION COMPLETE`

`WP08 VALIDATION ROOT: python/validation/`

`NEXT AUTHORIZED ACTION: Resume WP08 — Python Scientific Stack Validation Use Cases from the manifest-authority gate using the existing WP08 authority.`

Do not resume WP08 automatically.

If blocked end exactly:

`RELEASE 1.8 WP08 MANIFEST VALIDATION SHAPE RECONCILIATION BLOCKED`

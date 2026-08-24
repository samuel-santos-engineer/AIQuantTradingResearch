# Release 1.8 WP02 — Python Runtime Compatibility & Version Selection — Codex Authority

## 1. Mission

Execute Release 1.8 WP02 — **Python Runtime Compatibility & Version Selection** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#212`

Authoritative Release 1.8 planning baseline:

`651c45bd0df0b717b2bb5ad272ec8c890612fb6d`

Authoritative milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

WP01 is complete:

- #211: CLOSED / Done;
- canonical .NET baseline: 268/268;
- schema: v3;
- Python repository implementation/manifests: none;
- workstation Python observation: WindowsApps alias only;
- `py`: unavailable;
- `pip`: unavailable;
- no premature Release 1.8 or Release 1.9 implementation.

WP02 is a **research, compatibility-evidence, and version-decision** work package.

Its purpose is to establish the authoritative Python runtime target that later Release 1.8 work packages will install and validate.

WP02 SHALL NOT install Python or Python packages.

---

## 2. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- GitHub issue #212;
- WP01 completion evidence;
- current repository engineering/environment/versioning documentation relevant to runtime selection.

Treat the three Release 1.8 planning artifacts as human-accepted authority.

Do not broaden WP02 into installation, integration, ML implementation, or UI implementation.

---

## 3. Mandatory Starting-State Gate

Before research or GitHub lifecycle mutation verify:

### Repository

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- HEAD: `651c45bd0df0b717b2bb5ad272ec8c890612fb6d`;
- `origin/main`: same;
- ahead/behind: `0/0`;
- staged paths: 0;
- tracked repository diff: 0.

### Release 1.8

- #211: CLOSED / Done;
- #212: OPEN / Backlog;
- #213–#223: OPEN / Backlog;
- milestone #56: OPEN, 12 open / 1 closed;
- Project #2 membership: 13/13;
- duplicate Project items: 0;
- dependency chain remains exact;
- WP01→WP02 dependency is present.

### Technical Baseline

- schema remains v3;
- canonical permanent test baseline remains 268;
- no Python implementation/manifests have appeared since WP01;
- no Release 1.9 implementation has begun.

If any material state differs, stop before mutation and report the smallest corrective authority required.

---

## 4. Research Requirement — Current Evidence Only

WP02 must determine the **current supported Python versions at execution time** for the intended Release 1.8 ecosystem.

Research authoritative upstream sources for:

1. Python for Windows;
2. NumPy;
3. pandas;
4. scikit-learn;
5. Streamlit.

Prefer official project documentation, release documentation, package metadata, compatibility/install documentation, and official Python documentation.

Do not rely solely on model memory, search snippets, blogs, Stack Overflow, Reddit, or secondary compatibility tables.

Record source URLs and the date/time of research in the execution evidence.

Where official sources distinguish between:

- minimum supported Python;
- maximum/tested Python;
- currently published wheels;
- source-build possibility;
- OS/platform support;

preserve those distinctions rather than collapsing them into an unsupported claim.

---

## 5. Compatibility Matrix

Build a concise compatibility matrix containing at minimum:

| Component | Current version/release considered | Minimum Python | Maximum/upper support evidence | Windows x64 evidence | Notes |
|---|---|---|---|---|---|
| Python | — | — | — | required | runtime candidate |
| NumPy | researched | researched | researched | researched | scientific foundation |
| pandas | researched | researched | researched | researched | data manipulation |
| scikit-learn | researched | researched | researched | researched | Release 1.9 ML |
| Streamlit | researched | researched | researched | researched | interface foundation |

Do not invent an upper bound when upstream documentation does not state one.

If package metadata and documentation differ, record the discrepancy and prefer the most authoritative current evidence.

---

## 6. Candidate Python Versions

Identify practical current Python candidates supported by the intersection of the four required libraries and Windows 11 x64.

Evaluate candidates using:

1. NumPy compatibility;
2. pandas compatibility;
3. scikit-learn compatibility;
4. Streamlit compatibility;
5. availability of normal Windows x64 installation;
6. availability of binary wheels for the required scientific stack where evidence exists;
7. ecosystem maturity;
8. broad third-party support;
9. suitability for later machine-wide installation;
10. suitability for per-project `venv`;
11. suitability for VS Code;
12. suitability for Streamlit Community Cloud where authoritative compatibility information is available;
13. suitability for future `.NET ↔ Python` integration without prematurely selecting the interop mechanism.

Do not select a Python version merely because it is the newest.

Prefer a version with **broad ecosystem support and low compatibility risk**.

---

## 7. Version Selection Decision

Select exactly one Python feature/minor version as the authoritative Release 1.8 target.

Example shape only:

`Python 3.x`

Do not pre-assume the value.

The decision must be evidence-driven from the live compatibility research.

Record:

- selected major/minor version;
- rationale;
- rejected viable alternatives;
- specific compatibility/risk trade-offs;
- whether later installation should track the latest available patch within the selected minor line.

Unless the accepted Release 1.8 plan explicitly requires an exact patch pin at this stage, prefer governing the **minor line** while allowing the later installation WP to choose the current secure patch release within that line.

---

## 8. Machine-Wide vs Project Dependency Rule Decision

WP02 may establish the runtime/dependency ownership principle, but not implement it.

Required conceptual decision:

### Machine scope

The Windows machine owns:

- the selected CPython runtime;
- the Python launcher if supplied/appropriate;
- base runtime tooling supplied by the official installation.

### Project scope

AIQuantTradingResearch owns its Python package dependencies through an isolated project environment and repository dependency declaration in later WPs.

NumPy, pandas, scikit-learn, Streamlit, and project-specific packages SHALL NOT be treated as machine-global application dependencies.

Do not create the venv or dependency manifest in WP02.

---

## 9. Global-vs-Project Guardrails

Document the following decision evidence:

- machine-wide Python runtime is intentional;
- project libraries belong in a project-local virtual environment;
- avoid global `pip install` for project dependencies;
- later validation must prove PowerShell and VS Code can resolve the intended runtime/environment;
- local environment directories must not be committed;
- reproducible project dependency declaration will be governed by a later WP;
- Streamlit is a project dependency even though it provides a CLI;
- scikit-learn is a project dependency for Release 1.9 readiness;
- NumPy/pandas are project dependencies;
- no ML.NET dependency is introduced by this decision.

This is architecture/environment policy only.

---

## 10. .NET Integration Boundary Constraint

WP02 must not choose or implement the final `.NET ↔ Python` transport.

Record only the compatibility constraints that future interop work must preserve:

- existing .NET architecture remains authoritative;
- Python must not leak into Domain contracts merely because Python is selected;
- Python package mechanics remain outside the Domain layer;
- future integration must preserve deterministic evidence/provenance expectations;
- failure semantics and process boundaries must be explicitly designed before implementation;
- Python must remain replaceable at the integration boundary;
- Release 1.9 ML capabilities must consume governed platform data/evidence rather than bypassing established boundaries.

Do not add Python.NET, embedded interpreters, subprocess adapters, HTTP services, gRPC, notebooks, or any other interop mechanism in WP02.

---

## 11. Streamlit Constraint

Streamlit is included in compatibility research because Release 1.8 is also preparing Python for interface purposes.

WP02 may confirm runtime compatibility only.

Do not:

- create a Streamlit app;
- create Streamlit configuration;
- deploy to Streamlit Community Cloud;
- create dashboard data exports;
- modify persistence for Streamlit;
- expose production interfaces.

---

## 12. Repository Evidence Artifact

Follow `RELEASE_1.8_FILE_MANIFEST.md` exactly for WP02 file ownership.

Create or update only the manifest-authorized WP02 evidence/documentation artifact(s).

The artifact must contain enough durable evidence to answer:

- what Python versions were considered;
- what official compatibility evidence was found;
- which version was selected;
- why;
- what machine-vs-project dependency policy was chosen;
- what remains deferred to later WPs.

Do not create extra convenience documentation outside manifest ownership.

If the manifest does not authorize a durable WP02 repository artifact, do not invent one; retain the evidence in the GitHub issue/report as governed by the accepted plan.

---

## 13. No Installation Gate

During WP02, prove that no environment mutation occurred.

Expected:

- Python installation delta: 0;
- PATH delta: 0;
- launcher delta: 0;
- pip installation delta: 0;
- venv delta: 0;
- NumPy installation delta: 0;
- pandas installation delta: 0;
- scikit-learn installation delta: 0;
- Streamlit installation delta: 0;
- VS Code extension/settings delta: 0.

A research command must not install or upgrade packages as a side effect.

Do not use package-manager commands that mutate the machine.

---

## 14. Repository and Product Mutation Budget

Unless the authoritative file manifest explicitly assigns a WP02 documentation artifact:

- production code delta: 0;
- permanent test delta: 0;
- schema delta: 0;
- package/project/reference delta: 0/0/0;
- provider/network product calls: 0;
- real credentials: 0;
- Git transport mutation: 0.

Web access for compatibility research is authorized and is not a product/provider call.

---

## 15. Validation

After any manifest-authorized documentation change, run the repository-required validation appropriate for documentation-only work.

At minimum:

- canonical build/test baseline remains valid;
- 268/268 permanent tests unless the repository itself proves a different governed count;
- 0 skipped;
- 0 build warnings/errors;
- formatting: PASS;
- Gitleaks: PASS;
- Markdown links: PASS where repository tooling supports it;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- schema v3 preserved;
- dependency graph unchanged;
- package/project/reference deltas: 0/0/0;
- environment installation delta: 0;
- residue: 0.

If the manifest-authorized WP02 artifact intentionally creates a tracked diff, account for it exactly.

Do not stage or commit it.

---

## 16. GitHub Lifecycle

Only after WP02 evidence and all gates pass:

1. transition #212 to In Progress if needed;
2. post concise evidence including the selected Python minor version and authoritative compatibility rationale;
3. close #212;
4. set #212 Project Status to Done.

Final required state:

- #211: CLOSED / Done;
- #212: CLOSED / Done;
- #213–#223: OPEN / Backlog;
- milestone #56: OPEN, 11 open / 2 closed;
- Project membership: 13/13;
- duplicates: 0;
- Priority/Release/Area unchanged;
- dependency chain unchanged.

Do not transition WP03 automatically.

---

## 17. Stop Conditions

Stop with:

`RELEASE 1.8 WP02 BLOCKED`

if:

- authoritative upstream compatibility evidence cannot establish a safe intersection;
- required official sources materially conflict and cannot be reconciled;
- Windows support for the selected candidate cannot be established;
- the selected version would require installing software to prove the decision;
- repository/GitHub baseline differs materially;
- WP02 would require implementation outside manifest ownership;
- Release 1.9 work would be required;
- unexpected schema/package/project/reference drift exists.

Report the exact blocker and smallest corrective authority required.

---

## 18. Required Execution Report

Report:

### Starting State

- branch;
- HEAD/origin;
- ahead/behind;
- tracked/staged state;
- milestone and issue state.

### Authoritative Research

For Python, NumPy, pandas, scikit-learn, and Streamlit:

- official source;
- version/release considered;
- Python support evidence;
- Windows/platform evidence where relevant;
- research timestamp.

### Compatibility Matrix

Report the complete evaluated intersection.

### Decision

- selected Python major/minor;
- patch policy;
- rationale;
- viable alternatives rejected;
- compatibility risks.

### Environment Policy

- machine-wide runtime rule;
- project-local dependency rule;
- global pip rule;
- later venv/dependency ownership.

### Deferred Decisions

Explicitly identify what remains for later WPs:

- installation mechanics;
- PowerShell/VS Code verification;
- venv creation;
- dependency declaration/installation;
- `.NET ↔ Python` interop design;
- Python use cases/tests;
- Streamlit interface implementation;
- Release 1.9 ML.

### Validation

- tests;
- build;
- security;
- links;
- diff checks;
- schema;
- dependency graph;
- installation delta;
- residue.

### Mutation Accounting

- repository;
- Git;
- GitHub;
- Python environment;
- packages;
- schema.

### Final GitHub State

- #212;
- milestone #56;
- next authorized WP.

---

## 19. Completion Markers

On success end exactly:

`RELEASE 1.8 WP02 COMPLETE`

`PYTHON RUNTIME TARGET: <selected major.minor>`

`NEXT AUTHORIZED WORK PACKAGE: WP03 — Machine-Wide Python Installation — GitHub issue #213`

Do not execute WP03 automatically.

If blocked end exactly:

`RELEASE 1.8 WP02 BLOCKED`

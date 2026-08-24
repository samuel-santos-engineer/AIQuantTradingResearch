# Release 1.8 WP02 — Engineering Selection Records Documentation-Completion Correction — Codex Authority

## 1. Mission

Perform one narrowly scoped documentation-completion correction after successful Release 1.8 WP02 — Python Runtime Compatibility & Version Selection.

Repository:

`samuel-santos-engineer/AIQuantTradingResearch`

Release 1.8 planning baseline:

`651c45bd0df0b717b2bb5ad272ec8c890612fb6d`

WP02 GitHub issue:

`#212 — Python Runtime Compatibility & Version Selection`

Established WP02 result:

- #212: CLOSED / Done;
- selected runtime: Python 3.13;
- patch policy: latest secure patch within the Python 3.13 minor line;
- `PYTHON_RUNTIME_COMPATIBILITY.md` exists as compatibility evidence;
- canonical .NET baseline: 268/268;
- schema: v3;
- no Python/package installation occurred;
- WP03/#213 remains OPEN / Backlog.

This correction exists to apply the newly adopted project-wide engineering governance rule:

> Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.

This correction is documentation/governance only.

It does not reopen the technical WP02 compatibility decision and does not authorize WP03 execution.

## 2. Mandatory Starting-State Gate

Before mutation, read completely:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`
- current repository governance/architecture/implementation documentation governing external dependencies, technology selection, versioning, and documentation conventions;
- GitHub issue #212 and its completion evidence;
- GitHub issue #213;
- milestone #56 and relevant Project #2 state.

Verify:

- repository is correct;
- branch is `main`;
- local HEAD equals `origin/main`;
- ahead/behind is `0/0`;
- no staged paths;
- only expected post-WP02 governed/untracked authority or documentation state exists;
- #211 and #212 are CLOSED / Done;
- #213–#223 are OPEN / Backlog;
- milestone #56 is OPEN, 11 open / 2 closed;
- schema remains v3;
- no WP03 installation has begun;
- no Release 1.9 implementation has begun.

Do not require HEAD to equal the original planning baseline if the accepted WP02 documentation has legitimately advanced repository content locally but has not yet been integrated; instead reconcile the actual governed WP02 state without performing Git integration.

Stop on unexplained tracked or GitHub state.

## 3. Governance Rule to Establish

Add the smallest repository-native durable governance statement necessary to make the following rule explicit and reusable:

**Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.**

Place the rule in the most appropriate existing engineering/governance document according to repository conventions.

Do not create a new general governance document if an existing authoritative document naturally owns this rule.

Preserve existing principles and terminology.

## 4. Required Selection Records

Create exactly these five engineering selection records unless repository-native naming/location authority requires an equivalent canonical filename:

- `docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md`
- `docs/architecture/implementation/NUMPY_SELECTION.md`
- `docs/architecture/implementation/PANDAS_SELECTION.md`
- `docs/architecture/implementation/SCIKIT_LEARN_SELECTION.md`
- `docs/architecture/implementation/STREAMLIT_SELECTION.md`

Do not merge all five decisions into one document.

`PYTHON_RUNTIME_COMPATIBILITY.md` remains compatibility evidence and must not be repurposed as the selection record.

## 5. Common Selection-Record Contract

Each record must explicitly contain:

1. purpose and platform need;
2. selected runtime/library/framework/tool;
3. version decision or version-selection state;
4. reasons for selection;
5. alternatives considered;
6. reasons alternatives were not selected now;
7. accepted trade-offs;
8. version policy;
9. architectural boundaries;
10. dependency ownership and installation scope where applicable;
11. risks/limitations;
12. validation expectations;
13. conditions that trigger reconsideration;
14. relationship to Release 1.8 and, where appropriate, Release 1.9;
15. references to authoritative compatibility evidence and official upstream sources already established by WP02 where applicable.

Keep these records concise, engineering-oriented, and decision-focused.

Do not write marketing material.

## 6. Python Runtime Selection Record

`PYTHON_RUNTIME_SELECTION.md` must record the already-authorized decision:

`Python 3.13`

with policy:

- track the latest secure patch within the 3.13 minor line unless later governed evidence requires otherwise.

Explain at minimum:

- why Python is introduced alongside the existing .NET platform rather than replacing it;
- why Python is appropriate for the planned ML/data/interface ecosystem;
- why 3.13 was selected from the compatibility intersection established by WP02;
- broad ecosystem support versus newest-version adoption;
- Windows machine-wide runtime requirement;
- project-local dependency isolation;
- compatibility with intended NumPy, pandas, scikit-learn, and Streamlit usage;
- accepted dual-runtime operational complexity;
- boundary rule that Python mechanics do not leak into Domain contracts;
- reconsideration triggers.

Do not change the selected Python minor version.

## 7. NumPy Selection Record

`NUMPY_SELECTION.md` must explain why NumPy is selected as the foundational numerical-array library for Python-side quantitative/ML work.

Cover:

- numerical array/vectorized computation role;
- ecosystem interoperability;
- relationship to pandas/scikit-learn;
- alternatives considered at an appropriate level;
- accepted trade-offs such as additional native/binary dependency surface and memory/layout concerns;
- architectural boundary: NumPy representations are Python implementation concerns and must not become Domain contracts;
- project-local dependency ownership;
- reconsideration triggers.

Do not claim an exact NumPy package version was selected unless WP02 authoritative evidence explicitly selected one.

If no exact version was governed, record:

`Technology selected; exact package version deferred to the Release 1.8 dependency-governance work package.`

## 8. pandas Selection Record

`PANDAS_SELECTION.md` must explain why pandas is selected for Python-side tabular data manipulation and analytical preparation.

Cover:

- DataFrame/tabular transformation role;
- compatibility with NumPy/scikit-learn;
- productivity/readability advantages;
- alternatives considered;
- accepted trade-offs including memory overhead, mutable transformation risks, and suitability limits for larger-than-memory/distributed workloads;
- deterministic/evidence boundaries;
- no replacement of authoritative durable persistence;
- project-local dependency ownership;
- reconsideration triggers.

Do not invent an exact package version if it was not governed.

## 9. scikit-learn Selection Record

`SCIKIT_LEARN_SELECTION.md` must explain why scikit-learn is selected as the initial conventional machine-learning library for Release 1.9 readiness.

Cover:

- supervised/unsupervised classical ML suitability;
- preprocessing/model-selection/evaluation ecosystem;
- mature Python scientific-stack integration;
- why this project is not selecting ML.NET as its primary ML foundation;
- alternatives such as ML.NET and heavier deep-learning frameworks at an appropriate architectural level;
- accepted trade-offs, including Python/.NET interoperability and limitations for deep-learning/GPU-centric workloads;
- requirement that ML consumes governed platform evidence/data rather than bypassing established boundaries;
- deterministic/reproducibility expectations;
- project-local dependency ownership;
- reconsideration triggers.

Do not implement ML and do not select algorithms in this correction.

Do not invent an exact scikit-learn version if it was not governed.

## 10. Streamlit Selection Record

`STREAMLIT_SELECTION.md` must explain why Streamlit is selected as the initial Python-side interface/dashboard tool.

Cover:

- rapid engineering/research visualization role;
- suitability for exposing governed analytical evidence;
- low-friction integration with pandas/scientific Python;
- alternatives considered, including maintaining UI entirely in .NET and more general web frameworks;
- accepted trade-offs around UI customization, production scaling, process/runtime topology, and separation from core platform responsibilities;
- boundary rule that Streamlit is an interface/adapter concern and does not own Domain/Application business rules or persistence;
- project-local dependency ownership;
- reconsideration triggers.

Do not create a Streamlit application.

Do not invent an exact Streamlit version if it was not governed.

## 11. Version-Decision Integrity

The selection records must distinguish between:

- **technology selection**, and
- **exact package-version selection**.

Python 3.13 is already selected and must be recorded as such.

For NumPy, pandas, scikit-learn, and Streamlit:

- use exact versions only if an authoritative Release 1.8 decision already selected them;
- otherwise explicitly defer exact package-version selection to the appropriate later Release 1.8 dependency-governance WP;
- do not silently convert the versions observed during WP02 compatibility research into dependency pins.

This correction must not preempt later dependency-governance authority.

## 12. Release 1.8 Manifest Reconciliation

Update `RELEASE_1.8_FILE_MANIFEST.md` with the smallest necessary change to govern the five selection records and any existing governance document modified to establish the standing rule.

Preserve existing WP ownership.

Classify these five records as the WP02 documentation-completion correction associated with the already-completed runtime/library selection foundation.

Do not change the WP sequence, issue numbering, dependencies, implementation scope, or Release 1.8 acceptance semantics.

If repository conventions require corresponding navigation/index references, update only the minimum authoritative navigation surface.

## 13. Existing Compatibility Evidence

Preserve:

`docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`

Its responsibility remains:

- compatibility research;
- supported-version evidence;
- upstream references;
- Python 3.13 compatibility basis.

Selection records may reference it rather than duplicating its full compatibility matrix.

Do not create conflicting compatibility claims.

## 14. Explicit Non-Goals

Do not:

- install Python;
- change PATH;
- install or upgrade pip;
- create a venv;
- install NumPy;
- install pandas;
- install scikit-learn;
- install Streamlit;
- create dependency manifests unless separately authorized by the existing manifest;
- change VS Code configuration;
- implement `.NET ↔ Python` integration;
- create Python production code;
- create Streamlit UI;
- implement ML;
- change schema v3;
- add .NET packages;
- change project references;
- execute WP03;
- begin Release 1.9;
- stage, commit, push, branch, create a PR, merge, tag, or create a GitHub Release.

## 15. Mutation Budget

Authorized tracked documentation delta is limited to:

- five selection records;
- smallest necessary standing-governance-rule update;
- smallest necessary `RELEASE_1.8_FILE_MANIFEST.md` reconciliation;
- minimum navigation/index update only if repository conventions require it.

Expected:

- production code delta: 0;
- permanent test-code delta: 0;
- schema delta: 0;
- package delta: 0;
- project delta: 0;
- project-reference delta: 0;
- Python environment delta: 0;
- VS Code delta: 0;
- Git transport delta: 0.

Do not stage authorized documentation changes.

## 16. Validation

Run repository-native validation appropriate for the documentation correction.

Require:

- canonical .NET tests: 268/268;
- skipped: 0;
- build: 0 warnings / 0 errors;
- formatting: PASS;
- Gitleaks: PASS;
- Markdown links: PASS;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- terminal-newline checks: PASS;
- trailing whitespace: 0;
- conflict markers: 0;
- schema: v3;
- package/project/reference delta: 0/0/0;
- dependency graph unchanged and acyclic;
- Python installation/package activity: 0;
- provider/network product activity: 0;
- real credentials: 0;
- temporary residue: 0.

Reconcile all changed paths exactly against this authority.

## 17. GitHub Lifecycle Correction

Issue #212 is already CLOSED / Done.

Do not reopen it.

After successful documentation correction:

- add one concise correction/evidence comment to #212 explaining that the engineering selection records and standing selection-record governance rule were added after human authorization;
- preserve #212 CLOSED / Done;
- preserve #213–#223 OPEN / Backlog;
- preserve milestone #56 OPEN with 11 open / 2 closed;
- preserve Project #2 membership/fields/dependencies.

Do not transition #213.

Expected GitHub mutation count:

- exactly one #212 evidence comment, unless GitHub is unavailable.

If GitHub is unavailable, do not block otherwise-valid repository documentation solely to retry aggressively; report the missing lifecycle comment and stop before claiming full correction completion if the accepted repository governance requires the comment.

## 18. Stop Conditions

Stop with:

`RELEASE 1.8 WP02 DOCUMENTATION-COMPLETION CORRECTION BLOCKED`

if:

- current repository state cannot be reconciled to completed WP02;
- exact library versions would need to be invented;
- selection records conflict with authoritative WP02 evidence;
- the file manifest cannot be reconciled without changing Release 1.8 scope;
- unexpected production/schema/package/reference changes exist;
- validation fails;
- completing the correction would require WP03 or Release 1.9 work.

Report the exact blocker and smallest corrective authority required.

## 19. Required Report

Report:

### Baseline
- branch/HEAD/origin;
- ahead/behind;
- staged paths;
- WP02/WP03 GitHub state.

### Governance
- location of the standing selection-record rule;
- exact wording/meaning preserved.

### Selection Records
For each of Python, NumPy, pandas, scikit-learn, and Streamlit report:
- created path;
- selected technology;
- version state;
- rationale;
- principal alternatives;
- principal accepted trade-offs;
- architectural boundary;
- reconsideration triggers.

### Manifest
- exact manifest/navigation reconciliation.

### Validation
- 268/268 tests;
- build;
- formatting;
- Gitleaks;
- links;
- diff/whitespace;
- schema;
- graph;
- environment/residue.

### Mutation Accounting
- documentation;
- production;
- tests;
- schema;
- packages/projects/references;
- Python environment;
- Git;
- GitHub.

### Final State
- #212 remains CLOSED / Done;
- #213 remains OPEN / Backlog;
- milestone #56 remains OPEN, 11 open / 2 closed.

## 20. Completion Markers

On success end exactly:

`RELEASE 1.8 WP02 DOCUMENTATION-COMPLETION CORRECTION COMPLETE`

`FOUNDATIONAL TECHNOLOGY SELECTION RECORDS: 5/5`

`NEXT AUTHORIZED WORK PACKAGE: WP03 — Machine-Wide Python Installation — GitHub issue #213`

Do not execute WP03 automatically.

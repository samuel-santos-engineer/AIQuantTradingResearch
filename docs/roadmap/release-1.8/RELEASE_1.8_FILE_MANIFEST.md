# Release 1.8 --- Python & AI Engineering Foundation --- File Manifest

## Purpose

This manifest defines Release 1.8 file ownership expectations and
candidate boundaries before implementation.

It is intentionally conservative. Exact filenames for new Python
runtime, interoperability, test, and documentation assets may be refined
by the owning work package after repository inspection. A work package
must not invent broad unrelated structure merely to satisfy this
manifest.

## Authoritative Predecessor

-   Release 1.7 commit: `f8e521af2c5262d6cc173d0731b5e915dbceac0a`
-   Release 1.7 tree: `880f7fff6a9b946a310d32e17c1c803ca6c1a286`
-   Schema: v3
-   Tests: 268/268

## Planning Artifacts

The following three files are the authoritative Release 1.8 planning set
once human-accepted and integrated:

``` text
docs/roadmap/release-1.8/
├── RELEASE_1.8_DEFINITION.md
├── RELEASE_1.8_EXECUTION_PLAN.md
└── RELEASE_1.8_FILE_MANIFEST.md
```

## Prompt Artifacts

Expected governed WP prompt pairs:

``` text
docs/roadmap/release-1.8/prompts/
├── 01-release-repository-preflight-codex-prompt.md
├── 01-release-repository-preflight-codex-prompt-chat.md
├── 02-python-runtime-compatibility-version-selection-codex-prompt.md
├── 02-python-runtime-compatibility-version-selection-codex-prompt-chat.md
├── 03-windows-machine-wide-python-foundation-codex-prompt.md
├── 03-windows-machine-wide-python-foundation-codex-prompt-chat.md
├── 04-powershell-vscode-python-validation-codex-prompt.md
├── 04-powershell-vscode-python-validation-codex-prompt-chat.md
├── 05-virtual-environment-dependency-isolation-codex-prompt.md
├── 05-virtual-environment-dependency-isolation-codex-prompt-chat.md
├── 06-python-dependency-governance-codex-prompt.md
├── 06-python-dependency-governance-codex-prompt-chat.md
├── 07-scientific-machine-learning-library-foundation-codex-prompt.md
├── 07-scientific-machine-learning-library-foundation-codex-prompt-chat.md
├── 08-streamlit-visualization-foundation-codex-prompt.md
├── 08-streamlit-visualization-foundation-codex-prompt-chat.md
├── 09-dotnet-python-interoperability-architecture-codex-prompt.md
├── 09-dotnet-python-interoperability-architecture-codex-prompt-chat.md
├── 10-dotnet-python-integration-proof-codex-prompt.md
├── 10-dotnet-python-integration-proof-codex-prompt-chat.md
├── 11-python-foundation-interoperability-tests-codex-prompt.md
├── 11-python-foundation-interoperability-tests-codex-prompt-chat.md
├── 12-architecture-documentation-developer-environment-alignment-codex-prompt.md
├── 12-architecture-documentation-developer-environment-alignment-codex-prompt-chat.md
├── 13-full-validation-integration-acceptance-codex-prompt.md
└── 13-full-validation-integration-acceptance-codex-prompt-chat.md
```

Companion bootstrap format should follow the established repository
convention: exactly five non-empty physical lines, matching the
authoritative full prompt, with one terminal newline.

Planning-definition, GitHub-planning, corrective, integration, and
post-merge authorities are execution-governance artifacts and require
explicit lifecycle classification by their own authority. They are not
automatically part of the final Release 1.8 governed candidate.

## WP01 --- Release & Repository Preflight

### Expected new production files

None.

### Expected modifications

None unless repository inspection discovers an actual defect requiring
separate corrective authority.

### Evidence

Execution report only; GitHub issue lifecycle when authorized.

## WP02 --- Python Runtime Compatibility & Version Selection

### Expected new documentation

A repository-native compatibility/decision artifact is expected.
Preferred location:

``` text
docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md
```

The post-WP02 documentation-completion correction also owns these separate
engineering selection records:

``` text
docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md
docs/architecture/implementation/NUMPY_SELECTION.md
docs/architecture/implementation/PANDAS_SELECTION.md
docs/architecture/implementation/SCIKIT_LEARN_SELECTION.md
docs/architecture/implementation/STREAMLIT_SELECTION.md
```

These records document technology selection and the WP07 exact pins: NumPy
2.5.1, pandas 3.0.5, scikit-learn 1.9.0, and Streamlit 1.61.1. The standing
selection-record rule is governed in
`docs/architecture/implementation/IMPLEMENTATION_GUIDELINES.md`.

The WP04 VS Code Python extension corrective governance also owns:

``` text
docs/architecture/implementation/VSCODE_PYTHON_EXTENSION_SELECTION.md
```

This record governs the official `ms-python.python` development extension
only; it does not authorize repository settings, project environments, Python
packages, or unrelated extensions.

It should contain:

-   evaluated Python versions;
-   NumPy compatibility;
-   pandas compatibility;
-   SciPy compatibility where relevant;
-   scikit-learn compatibility;
-   Streamlit compatibility;
-   Windows 11 x64 compatibility;
-   selected version;
-   decision rationale;
-   date/source context.

### Expected production code

None.

## WP03 --- Windows Machine-Wide Python Foundation

### Expected documentation

A developer/environment guide may be introduced or extended. Preferred
candidate:

``` text
docs/guides/PYTHON_WINDOWS_ENVIRONMENT.md
```

### Machine-only artifacts --- prohibited from repository

-   machine Python installation directories;
-   user-specific PATH dumps;
-   local installer binaries;
-   local pip cache;
-   credentials;
-   machine-specific absolute-path configuration.

## WP04 --- PowerShell & VS Code Python Validation

### Expected documentation ownership

Prefer extending:

``` text
docs/guides/PYTHON_WINDOWS_ENVIRONMENT.md
```

rather than creating fragmented guides unless repository evidence
requires separation.

### Local-only artifacts

VS Code user settings that are workstation-specific must not be
committed.

Repository workspace settings may be committed only if portable and
explicitly justified.

## WP05 --- Virtual Environment & Dependency Isolation

### Expected repository modifications

Likely:

``` text
.gitignore
```

only if existing rules do not already protect required Python artifacts.

Potential repository-owned environment bootstrap/verification scripts
may be introduced under the repository's existing engineering-script
convention after inspection.

### Must remain untracked

``` text
.venv/
venv/
__pycache__/
*.pyc
.pytest_cache/
.mypy_cache/
.ruff_cache/
.streamlit/        # if it contains local/user runtime state rather than governed config
```

The exact ignore rules must be evidence-driven and must not accidentally
exclude governed source/configuration.

## WP06 --- Python Dependency Governance

### Expected dependency declaration

Exact format is owned by WP06.

Candidate forms include, but are not pre-authorized as final names:

``` text
requirements.txt
requirements-dev.txt
constraints.txt
pyproject.toml
```

WP06 must choose the smallest repository-appropriate model.

WP06 selects `requirements.txt` as the single direct-dependency declaration
and owns `docs/architecture/implementation/PYTHON_DEPENDENCY_GOVERNANCE.md`.
WP07 owns its four exact direct pins; no competing dependency manager or
resolution artifact is authorized.

### Expected documentation

Dependency governance should be documented in an existing
engineering/governance location where possible.

### Prohibited

Globally installed project dependencies as the authoritative dependency
source.

## WP07 --- Scientific & Machine Learning Library Foundation

### Expected Python source location

WP07 must inspect repository conventions before freezing a path.

Preferred conceptual ownership is a clearly isolated Python capability
area, for example:

``` text
python/
```

or another repository-native equivalent.

The manifest intentionally does not pre-authorize a broad Python package
tree before WP07/WP09 determine the appropriate structure.

### Expected dependencies

-   NumPy
-   pandas
-   scikit-learn
-   SciPy where required/governed

### Prohibited durable artifacts

-   trained model binaries;
-   pickles/joblib model artifacts;
-   durable prediction outputs;
-   ML result databases.

## WP08 --- Streamlit Visualization Foundation

### Expected source and validation shape

WP08 owns exactly this validation-only Python area and these two executable
scripts:

- `python/validation/scientific_stack_validation.py`
- `python/validation/streamlit_validation_app.py`

``` text
python/
└── validation/
    ├── scientific_stack_validation.py
    └── streamlit_validation_app.py
```

The scripts use deterministic fixed/synthetic offline data and explicit
assertions/exit codes. No pytest, unittest project structure, Python test
discovery architecture, notebook, or other external validation framework is
introduced. Permanent Python tests remain owned by their later dedicated work
package.

`python/validation/` is Release 1.8 validation evidence only. It is not a
production Python package root, application architecture, ML module, or
portfolio dashboard.

A minimal Streamlit entry point and only the supporting code required
for the foundation proof.

Preferred conceptual separation:

``` text
<python-capability-root>/
└── visualization/
```

Exact path/name must be reconciled with the structure selected by
earlier WPs.

### Not owned by WP08

-   complete hiring-manager dashboard;
-   Streamlit Community Cloud deployment;
-   README live-demo marketing integration;
-   production visualization product.

WP09 retains ownership of .NET ↔ Python integration-boundary architecture and
all IPC/process integration. No .NET project reference or production Python
structure is implied by this validation path.

## WP09 --- .NET ↔ Python Interoperability Architecture

### Expected documentation

Preferred architecture artifact:

``` text
docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md
```

or the closest repository-native design location discovered during
execution.

It must define the boundary before WP10 implementation.

### Expected production code

None.

## WP10 --- .NET ↔ Python Integration Proof

### Expected .NET ownership

The exact files must be derived from the existing solution boundaries.

Likely ownership:

``` text
src/AIQuantTradingResearch.Application/
src/AIQuantTradingResearch.Infrastructure/
src/AIQuantTradingResearch.Worker/   # only if an explicit proof mode is justified
```

Rules:

-   Domain should have zero Python-specific types.
-   Application may own technology-independent capability contracts if
    required.
-   Infrastructure should own Python process/runtime mechanics.
-   Worker may own explicit composition/execution only if required by
    the accepted proof.
-   Existing modes must remain unchanged except for explicit
    deterministic routing additions.

### Expected Python ownership

A small repository-owned capability entry point under the Python root
selected by WP07/WP09.

### Serialization assets

Schemas/examples may be introduced only if they materially improve
contract governance.

## WP11 --- Python Foundation & Interoperability Tests

### Expected .NET test ownership

Potential additions under:

``` text
tests/AIQuantTradingResearch.Application.Tests/
tests/AIQuantTradingResearch.Infrastructure.Tests/
tests/AIQuantTradingResearch.Architecture.Tests/
```

Only add Architecture tests for a new stable rule that cannot be
adequately protected by existing tests.

### Expected Python tests

A Python test location may be introduced under the selected Python
capability root or a repository-native test root.

Exact test framework is not predetermined.

### Test-count rule

WP11 must report actual deltas. No target number of new tests is
authorized by this manifest.

## WP12 --- Architecture, Documentation & Developer Environment Alignment

### Expected current-state documentation review

Potentially relevant files include:

``` text
README.md
ENGINEERING.md
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
docs/guides/
```

Only files that are actually made stale by Release 1.8 should change.

Do not perform unrelated retrospective cleanup.

## WP13 --- Full Validation, Integration & Acceptance

### Expected production/content delta

Zero.

WP13 validates the cumulative candidate and may remove disposable
probes/residue. It must not introduce new product behavior to make
acceptance pass.

## Engineering Scripts

Release 1.8 may require repository-owned Python environment/verification
scripts.

Before adding scripts, inspect existing `eng/` conventions.

Potential responsibilities include:

-   Python runtime verification;
-   isolated environment restoration;
-   Python test execution;
-   cross-runtime acceptance verification;
-   cleanup.

Exact filenames are not frozen by planning because they should align
with current repository script naming after inspection.

Existing `eng/verify.ps1` may be modified only if the accepted
verification model requires it and the owning WP explicitly authorizes
the change.

## Configuration Files

Potential Release 1.8 configuration ownership includes:

``` text
.gitignore
requirements*.txt / constraints.txt / pyproject.toml  # WP06 decision
portable Streamlit configuration                      # only if required
portable VS Code workspace configuration              # only if justified
```

Do not commit:

-   local secrets;
-   local interpreter absolute paths;
-   virtual environments;
-   caches;
-   local Streamlit credentials;
-   machine-specific state.

## Schema Manifest

Release 1.8 expected schema delta:

``` text
schema version: v3 → v3
new tables: 0
new columns: 0
new indexes: 0
new migrations: 0
```

Any structural database requirement blocks the owning WP and requires
explicit corrective authority.

## Package / Project / Reference Expectations

### .NET packages

Zero-delta-first.

A new .NET package requires explicit justification and WP authority.

### Python packages

Governed by WP02/WP06/WP07/WP08.

### .NET projects

Zero new project first.

A new `.csproj` requires explicit architectural justification.

### Project references

Preserve the existing dependency graph unless WP09/WP10 proves a minimal
reference change is necessary and architecture validation passes.

## Release 1.8 Candidate Categories

The final candidate may contain only reconciled files from these
categories:

1.  three authoritative Release 1.8 planning files;
2.  governed WP01--WP13 prompt pairs if retained by the established
    prompt lifecycle;
3.  Python compatibility/environment documentation;
4.  dependency declarations/configuration;
5.  Python foundation source;
6.  minimal Streamlit foundation source;
7.  `.NET ↔ Python` architecture documentation;
8.  minimal interoperability production source;
9.  permanent Python/.NET interoperability tests;
10. engineering verification scripts;
11. current-state documentation made stale by Release 1.8;
12. narrowly necessary ignore/configuration changes.

Anything outside these categories is unexpected until explicitly
reconciled.

## Explicitly Excluded Content

The Release 1.8 candidate must not contain:

-   production ML models;
-   durable ML model artifacts;
-   model registry;
-   ML evidence persistence;
-   ML training product workflows;
-   hyperparameter tuning;
-   production prediction/inference;
-   Explainable AI;
-   backtesting;
-   complete Streamlit portfolio dashboard;
-   Streamlit Community Cloud deployment;
-   cloud ML infrastructure;
-   provider credentials;
-   real secrets;
-   virtual environments;
-   caches;
-   installer binaries;
-   unrelated Release 1.1--1.7 refactoring;
-   Release 1.9 implementation.

## Final Candidate Reconciliation Rule

WP13 must enumerate the exact candidate rather than assuming every
working-tree file belongs to Release 1.8.

The final integration authority must derive its staged path contract
from the WP13-accepted candidate plus its own explicitly classified
integration-authority files.

No execution-only corrective or planning-definition artifacts may be
committed accidentally.

## Expected Post-Release Boundary

After merge and post-merge closure, the merged Release 1.8 SHA and tree
will be separately frozen as the authoritative Release 1.8 baseline.

Only after that freeze may Release 1.9 definition/planning begin.

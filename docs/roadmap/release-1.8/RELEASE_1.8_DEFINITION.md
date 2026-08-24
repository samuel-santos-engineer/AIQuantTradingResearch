# Release 1.8 --- Python & AI Engineering Foundation

## Status

**Human-accepted Release 1.8 planning authority.**

Release 1.8 is a forward-looking platform-enablement release. It does
not reopen, revise, or retrospectively review completed Release 1.1--1.7
architecture or implementation.

## Authoritative Predecessor

Release 1.7 is the frozen predecessor.

-   Authoritative commit: `f8e521af2c5262d6cc173d0731b5e915dbceac0a`
-   Authoritative tree: `880f7fff6a9b946a310d32e17c1c803ca6c1a286`
-   Schema: v3
-   Permanent test baseline: 268/268
    -   Domain: 11
    -   Application: 119
    -   Infrastructure: 125
    -   Architecture: 13
-   Release 1.7 is closed.
-   No Release 1.7 mutation is authorized unless an actual defect is
    discovered.

## Mission

Establish a reproducible, isolated, tested, documented, and
operationally safe Python engineering foundation for
AIQuantTradingResearch, including:

1.  Windows workstation Python readiness.
2.  Scientific and machine-learning library readiness.
3.  Streamlit visualization readiness.
4.  Project-local Python dependency isolation and governance.
5.  A controlled `.NET ↔ Python` interoperability boundary.
6.  Executable proof that the resulting foundation is suitable for
    consumption by Release 1.9.

Release 1.8 answers:

> **Can AIQuantTradingResearch safely, reproducibly, and verifiably use
> Python as a first-class platform runtime?**

Release 1.9 will separately answer:

> **Can the platform train and preserve reproducible machine-learning
> results?**

## Product and Engineering Rationale

Python will serve two future platform purposes:

-   scientific and machine-learning capabilities; and
-   visualization/interface capabilities, including Streamlit.

Python itself is a workstation-level runtime and is not owned
exclusively by AIQuantTradingResearch. Python packages used by
AIQuantTradingResearch are project-owned dependencies and must remain
isolated from the machine-wide installation.

The intended environment model is:

``` text
Windows 11 workstation
├── .NET SDK                       machine scope
├── Git                            machine scope
├── VS Code                        machine scope
├── Python                         machine scope
│
└── AIQuantTradingResearch
    └── project Python environment
        ├── isolated virtual environment
        ├── NumPy
        ├── pandas
        ├── SciPy as required by the selected ML stack
        ├── scikit-learn
        └── Streamlit
```

## Architectural Position

Python is a first-class platform runtime, but it does not replace the
existing C#/.NET architecture.

Release 1.8 must preserve the existing Domain, Application,
Infrastructure, Worker, persistence, evidence, identity, and dependency
boundaries unless a change is strictly necessary to introduce the new
interoperability capability.

Python runtime or library mechanics must not leak into Domain contracts.

The preferred conceptual boundary is:

``` text
Existing C#/.NET platform
        │
        │ controlled interoperability contract
        ▼
Python capability boundary
        │
        ├── scientific / ML libraries
        └── visualization libraries
```

Release 1.8 must determine the exact interoperability mechanism from
repository evidence during WP09. A process boundary is the default
design candidate, but the planning definition does not pre-authorize an
implementation before WP09 establishes the contract.

## Release Objectives

Release 1.8 SHALL:

1.  Verify the frozen Release 1.7 baseline and absence of premature
    Release 1.8/1.9 implementation.
2.  Research current supported Python versions across the intended
    library stack and select a broadly supported version.
3.  Establish Python as a machine-wide Windows development runtime.
4.  Verify the selected runtime through PowerShell and VS Code.
5.  Establish project-local virtual-environment isolation.
6.  Establish explicit global-versus-project dependency ownership rules.
7.  Establish reproducible dependency declaration and restoration.
8.  Install and validate the selected scientific/ML library stack in the
    governed project environment.
9.  Establish Streamlit as a verified visualization capability.
10. Define a controlled `.NET ↔ Python` interoperability architecture.
11. Implement the smallest deterministic interoperability proof using
    existing platform evidence or deterministic repository-owned input.
12. Add permanent tests for stable repository-owned Python and
    interoperability behavior.
13. Align documentation and developer guidance.
14. Perform full release acceptance without changing Release 1.7
    semantics.

## Runtime Compatibility Decision

The Python version SHALL NOT be predetermined by this definition.

WP02 owns the decision and must evaluate the current compatibility
intersection for at least:

-   Python
-   NumPy
-   pandas
-   SciPy where required
-   scikit-learn
-   Streamlit
-   Windows 11 x64
-   the intended Streamlit deployment environment where relevant

The selected version must favor broad ecosystem support and
reproducibility over novelty.

WP03 is blocked until WP02 freezes the selected Python version and
rationale.

## Machine Scope vs Project Scope

### Machine scope

The workstation may own:

-   selected Python interpreter;
-   Python launcher where applicable;
-   base package-management tooling required to create isolated
    environments;
-   `venv` capability.

### Project scope

AIQuantTradingResearch owns:

-   dependency declarations;
-   approved dependency versions or constraints;
-   environment creation/restoration instructions;
-   Python source owned by the repository;
-   Python test assets owned by the repository;
-   `.NET ↔ Python` interoperability contracts and adapters;
-   Python verification scripts;
-   Streamlit foundation code introduced by this release.

### Prohibited shared-state dependency model

Release 1.8 must not depend on globally installed NumPy, pandas, SciPy,
scikit-learn, or Streamlit packages.

The repository must not silently install or mutate machine-wide Python
during normal build, test, or application execution.

## Interoperability Requirements

WP09 must define, and WP10 must prove where applicable:

-   interpreter/environment resolution;
-   invocation ownership;
-   request serialization;
-   response serialization;
-   deterministic input/output behavior;
-   standard output ownership;
-   standard error ownership;
-   exit-code semantics;
-   timeout behavior;
-   cancellation/termination behavior;
-   malformed-response behavior;
-   unavailable-runtime behavior;
-   unavailable-dependency behavior;
-   process cleanup;
-   temporary-file cleanup if files are used;
-   credential and network isolation;
-   no provider fallback;
-   no accidental durable ML artifact.

The interoperability proof must remain bounded and deterministic.

## Library Validation Requirements

Release 1.8 must prove more than successful imports.

At minimum:

-   NumPy: deterministic numerical operation.
-   pandas: deterministic DataFrame construction/transformation.
-   scikit-learn: tiny deterministic disposable operation sufficient to
    prove the library executes correctly.
-   Streamlit: minimal local application startup/render capability and
    controlled termination.
-   SciPy: direct validation if the selected dependency strategy makes
    it an explicitly governed dependency.

Any scikit-learn fit/predict proof is infrastructure/environment
evidence only. It is not a Release 1.9 model-training capability.

## Permanent Verification Model

Release 1.8 must distinguish:

1.  existing canonical .NET verification;
2.  repository-owned Python verification;
3.  environment/integration acceptance verification.

The release must not make ordinary repository verification dependent on
undocumented workstation state.

WP11/WP13 must define the final canonical verification contract and
document prerequisites explicitly.

## Schema and Persistence Position

Release 1.8 is zero-schema-delta-first.

-   Schema v3 remains authoritative.
-   No ML model tables are authorized.
-   No model registry is authorized.
-   No training-evidence persistence is authorized.
-   No migration is authorized unless a separately identified blocking
    defect makes it unavoidable and human corrective authority is
    obtained.

The `.NET ↔ Python` proof should prefer transient or existing evidence
and must not create durable ML product state.

## Security and Offline Position

Release 1.8 must preserve the repository's existing security discipline.

-   No real credentials.
-   No provider/network product calls.
-   No secrets in Python configuration.
-   No virtual environments committed.
-   No Python caches committed.
-   No generated local environment state committed.
-   Gitleaks remains mandatory.
-   Network access used solely to install documented development
    dependencies is environment provisioning, not product/provider
    activity, and must be clearly separated from offline product
    verification.

## Hard Exclusions

Release 1.8 explicitly excludes:

-   production ML training;
-   durable trained-model persistence;
-   ML model identity;
-   ML training-result identity;
-   ML experiment semantics;
-   model registry;
-   hyperparameter optimization;
-   automated feature selection;
-   production inference;
-   prediction APIs;
-   model serving;
-   automated retraining;
-   MLOps pipelines;
-   drift detection;
-   Explainable AI;
-   backtesting;
-   portfolio optimization;
-   trading decisions or signals;
-   broker/exchange integration;
-   cloud ML infrastructure;
-   complete Streamlit portfolio dashboard;
-   Streamlit Community Cloud deployment;
-   retrospective Release 1.1--1.7 architecture/design review.

## Work Packages

Release 1.8 consists of exactly 13 planned work packages:

1.  WP01 --- Release & Repository Preflight
2.  WP02 --- Python Runtime Compatibility & Version Selection
3.  WP03 --- Windows Machine-Wide Python Foundation
4.  WP04 --- PowerShell & VS Code Python Validation
5.  WP05 --- Virtual Environment & Dependency Isolation
6.  WP06 --- Python Dependency Governance
7.  WP07 --- Scientific & Machine Learning Library Foundation
8.  WP08 --- Streamlit Visualization Foundation
9.  WP09 --- .NET ↔ Python Interoperability Architecture
10. WP10 --- .NET ↔ Python Integration Proof
11. WP11 --- Python Foundation & Interoperability Tests
12. WP12 --- Architecture, Documentation & Developer Environment
    Alignment
13. WP13 --- Full Validation, Integration & Acceptance

The intended dependency graph is linear:

``` text
WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07
     → WP08 → WP09 → WP10 → WP11 → WP12 → WP13
```

A work package may validate predecessor behavior, but it must not
consume authority belonging to a later package.

## Acceptance Boundary

Release 1.8 is complete only when:

-   all 13 work packages are complete;
-   the selected Python runtime is explicitly documented;
-   machine/project dependency ownership is explicit;
-   the project Python environment is reproducible;
-   the governed scientific/ML libraries are functional;
-   Streamlit capability is functional;
-   `.NET ↔ Python` interoperability is defined and proven;
-   permanent verification is established;
-   existing .NET canonical verification remains passing;
-   schema v3 remains valid;
-   predecessor behavior remains intact;
-   security, whitespace, dependency, residue, and cleanliness gates
    pass;
-   no excluded Release 1.9 capability has been introduced;
-   GitHub lifecycle state is reconciled;
-   the candidate is accepted for a separate Git integration workflow.

Release acceptance does not itself authorize commit, push, PR, merge,
tag, GitHub Release, or Release 1.9 work.

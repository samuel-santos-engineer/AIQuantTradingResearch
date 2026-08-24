# Release 1.8 --- Python & AI Engineering Foundation --- Execution Plan

## Purpose

This plan decomposes Release 1.8 into 13 governed work packages. It is
subordinate to `RELEASE_1.8_DEFINITION.md` and must preserve the frozen
Release 1.7 baseline except for explicitly authorized Release 1.8
changes.

## Predecessor Baseline

-   Commit: `f8e521af2c5262d6cc173d0731b5e915dbceac0a`
-   Tree: `880f7fff6a9b946a310d32e17c1c803ca6c1a286`
-   Schema: v3
-   Permanent tests: 268/268

## Execution Principles

-   Execute one WP at a time.
-   Read the full Release 1.8 definition, execution plan, file manifest,
    and current WP authority before mutation.
-   Preserve predecessor behavior.
-   Prefer zero production delta until a WP explicitly owns production
    changes.
-   Prefer zero schema/package/project/reference delta unless explicitly
    owned.
-   Do not stage, commit, branch, push, create PRs, merge, tag, or
    create releases during WP execution unless separately authorized.
-   GitHub issue lifecycle changes may occur only when the WP authority
    explicitly permits them.
-   Temporary probes must be removed before WP completion.
-   Execution-only Codex prompt artifacts are not automatically governed
    product content.
-   Release 1.9 implementation is forbidden.

## WP01 --- Release & Repository Preflight

### Objective

Establish the exact Release 1.8 starting state.

### Activities

-   Verify `main` and `origin/main` at the frozen Release 1.7 baseline.
-   Verify clean Git state and ahead/behind 0/0.
-   Verify Release 1.7 closure state.
-   Verify schema v3.
-   Verify permanent test baseline 268/268.
-   Verify build warnings/errors baseline.
-   Inspect existing Python-related repository files, scripts, ignore
    rules, CI configuration, documentation, and accidental runtime
    dependencies.
-   Verify no premature Release 1.8 or 1.9 implementation.
-   Reconcile Release 1.8 GitHub planning state when that state has been
    separately authorized.

### Expected production delta

Zero.

### Exit criteria

Repository and GitHub state are suitable for WP02 with no unexplained
mutations.

------------------------------------------------------------------------

## WP02 --- Python Runtime Compatibility & Version Selection

### Objective

Select the Python version to govern Release 1.8.

### Activities

Research current official compatibility/support for:

-   Python;
-   NumPy;
-   pandas;
-   SciPy where relevant;
-   scikit-learn;
-   Streamlit;
-   Windows 11 x64;
-   intended Streamlit deployment compatibility where relevant.

Produce a compatibility matrix recording:

-   candidate Python versions;
-   supported library versions;
-   availability of appropriate Windows distributions/wheels;
-   known incompatibilities;
-   selected Python version;
-   rationale.

### Decision rule

Select a broadly supported version across the complete intended stack.
Do not select a version merely because it is newest.

### Expected production delta

Zero.

Documentation/planning evidence only.

### Stop gate

If no common supported Python version exists for the required stack,
stop Release 1.8 and request corrective planning authority.

------------------------------------------------------------------------

## WP03 --- Windows Machine-Wide Python Foundation

### Objective

Establish the selected Python runtime as a workstation-level capability.

### Activities

-   Install the WP02-selected Python version using an approved Windows
    installation method.
-   Verify x64 architecture.
-   Verify interpreter version.
-   Verify command/launcher resolution.
-   Verify base package-management tooling.
-   Verify `venv` capability.
-   Record machine-scope configuration needed for reproducibility.
-   Ensure scientific/ML/Streamlit packages are not installed globally
    as Release 1.8 project dependencies.

### Repository mutation

Documentation/scripts only where explicitly required. The machine
installation itself is not repository content.

### Stop gate

Any interpreter ambiguity, unsupported architecture, broken `venv`, or
requirement for uncontrolled global project packages blocks WP03.

------------------------------------------------------------------------

## WP04 --- PowerShell & VS Code Python Validation

### Objective

Prove the selected Python runtime is discoverable and usable through the
actual developer workflow.

### Activities

-   Verify PowerShell interpreter resolution.
-   Verify VS Code interpreter selection.
-   Verify VS Code integrated terminal behavior.
-   Verify selected interpreter consistency.
-   Document selection/recovery procedure.
-   Detect and document conflicting interpreters if present.

### Expected production delta

Zero.

### Exit criteria

PowerShell and VS Code can intentionally target the selected runtime
without ambiguity.

------------------------------------------------------------------------

## WP05 --- Virtual Environment & Dependency Isolation

### Objective

Establish disposable project-local Python isolation.

### Activities

-   Define the repository-local virtual-environment location.
-   Create environment from the selected machine runtime.
-   Activate/deactivate through PowerShell.
-   Verify interpreter identity inside the environment.
-   Verify project package isolation from global Python.
-   Define `.gitignore` protection for environment/cache/generated
    state.
-   Delete and recreate the environment.
-   Prove cleanup leaves no governed residue.

### Acceptance proof

A fresh environment can be recreated from documented repository
instructions without relying on globally installed project libraries.

------------------------------------------------------------------------

## WP06 --- Python Dependency Governance

### Objective

Define reproducible ownership, declaration, restore, and update rules
for Python dependencies.

### Activities

Decide and document:

-   dependency declaration format;
-   version pinning/constraint policy;
-   transitive-dependency treatment;
-   restore command;
-   environment recreation command;
-   update procedure;
-   machine-scope versus project-scope ownership;
-   cache policy;
-   generated-file policy;
-   security scanning implications;
-   Streamlit deployment dependency compatibility where relevant.

### Expected implementation

Repository dependency declaration/configuration may be introduced here.

### Exit criteria

Another developer can determine exactly what to install and how to
recreate the project Python environment.

------------------------------------------------------------------------

## WP07 --- Scientific & Machine Learning Library Foundation

### Objective

Establish the governed scientific/ML Python stack without introducing
product ML.

### Minimum capability

-   NumPy
-   pandas
-   scikit-learn
-   SciPy when governed by the selected dependency strategy

### Activities

-   Restore approved versions into the isolated project environment.
-   Verify versions.
-   Execute deterministic NumPy proof.
-   Execute deterministic pandas proof.
-   Execute tiny deterministic disposable scikit-learn proof.
-   Verify no durable model artifact is created.
-   Verify no provider/network product activity.
-   Remove temporary proof residue.

### Exclusion

The scikit-learn proof is not a production training use case.

------------------------------------------------------------------------

## WP08 --- Streamlit Visualization Foundation

### Objective

Establish Streamlit as a verified interface capability.

### Activities

-   Restore approved Streamlit dependency.
-   Introduce or execute the smallest governed Streamlit foundation.
-   Prove local application startup.
-   Prove deterministic data rendering.
-   Prove controlled termination.
-   Prove no dependency on live provider/network access.
-   Prove no complete portfolio dashboard has been introduced.

### Exit criteria

The repository has a reproducible, bounded Streamlit capability suitable
for future interface work.

------------------------------------------------------------------------

## WP09 --- .NET ↔ Python Interoperability Architecture

### Objective

Define the controlled cross-runtime boundary before implementation.

### Required decisions

-   boundary ownership;
-   invocation mechanism;
-   interpreter/environment resolution;
-   request contract;
-   response contract;
-   serialization format;
-   standard output/error contract;
-   exit-code contract;
-   timeout;
-   cancellation/termination;
-   malformed response;
-   runtime unavailable;
-   dependency unavailable;
-   temporary file policy;
-   process cleanup;
-   security/offline behavior;
-   observability expectations;
-   Domain/Application leakage prohibition.

### Design preference

Evaluate a process boundary first. Do not embed Python into the .NET
process without evidence and explicit authority.

### Expected production delta

Zero unless a documentation-only contract artifact is considered
production-adjacent. No interop implementation yet.

------------------------------------------------------------------------

## WP10 --- .NET ↔ Python Integration Proof

### Objective

Implement the smallest deterministic vertical proof of the WP09
architecture.

### Intended flow

``` text
existing deterministic platform evidence/input
        ↓
.NET interoperability adapter
        ↓
governed serialization
        ↓
project Python runtime
        ↓
NumPy/pandas and optional disposable sklearn operation
        ↓
structured deterministic response
        ↓
.NET validation/presentation
```

### Requirements

-   no Python mechanics in Domain;
-   no product ML semantics;
-   bounded invocation;
-   deterministic response;
-   explicit failure behavior;
-   timeout/termination where applicable;
-   zero real credentials;
-   zero provider/network product calls;
-   zero durable ML artifacts;
-   complete process/temp cleanup.

### Exit criteria

The existing .NET platform can invoke the governed Python capability and
receive a validated deterministic response.

------------------------------------------------------------------------

## WP11 --- Python Foundation & Interoperability Tests

### Objective

Convert stable Release 1.8 assumptions into permanent executable
verification.

### Coverage

Where appropriate and repository-owned:

-   dependency/configuration validation;
-   Python capability contract;
-   deterministic Python operation;
-   request serialization;
-   response deserialization;
-   successful interop;
-   malformed response;
-   non-zero exit;
-   unavailable runtime/capability boundary;
-   timeout/cleanup;
-   deterministic repeated result;
-   Streamlit foundation verification where automatable;
-   existing .NET regressions.

### Test architecture requirement

Tests must clearly distinguish:

-   ordinary .NET unit/architecture verification;
-   Python-capability tests;
-   environment-dependent acceptance tests.

Do not hide undocumented machine prerequisites inside ordinary canonical
tests.

### Test count

No numeric test delta is predetermined. WP11 must report exact
before/after counts truthfully.

------------------------------------------------------------------------

## WP12 --- Architecture, Documentation & Developer Environment Alignment

### Objective

Align current-state documentation with the implemented Release 1.8
foundation.

### Required topics

-   selected Python version and rationale;
-   Windows installation;
-   PowerShell validation;
-   VS Code interpreter selection;
-   `.venv` lifecycle;
-   dependency restoration;
-   global/project ownership;
-   scientific/ML library verification;
-   Streamlit startup;
-   `.NET ↔ Python` architecture;
-   failure/cleanup behavior;
-   canonical verification;
-   security;
-   troubleshooting;
-   exclusions and Release 1.9 boundary.

### Constraint

Do not use WP12 as a retrospective redesign of Releases 1.1--1.7.

------------------------------------------------------------------------

## WP13 --- Full Validation, Integration & Acceptance

### Objective

Prove Release 1.8 is complete and integration-ready.

### Reconciliation

-   reconcile the exact governed Release 1.8 candidate;
-   reconcile prompt companions if governed;
-   identify execution-only exclusions;
-   verify no unexplained paths;
-   verify no staged paths before integration authority.

### .NET verification

-   restore;
-   build;
-   all permanent tests;
-   architecture tests;
-   formatting;
-   Gitleaks;
-   dependency graph;
-   schema v3;
-   predecessor regressions.

### Python verification

-   selected interpreter;
-   environment recreation;
-   dependency restoration;
-   NumPy;
-   pandas;
-   SciPy where governed;
-   scikit-learn;
-   Streamlit.

### Interoperability verification

-   `.NET → Python`;
-   deterministic input/output;
-   failure behavior;
-   cleanup;
-   zero retained processes;
-   zero temporary residue;
-   zero provider/network product calls;
-   zero real credentials.

### GitHub lifecycle

When separately authorized by WP execution authority:

-   WP01--WP13 complete;
-   Project membership/fields correct;
-   dependency chain correct;
-   milestone closed only when all issues are complete;
-   predecessor state preserved;
-   no Release 1.9 implementation objects.

### Final markers

Successful WP13 should report:

`RELEASE 1.8 WP13 COMPLETE`

`RELEASE 1.8 ACCEPTED FOR INTEGRATION`

The next action is a separate human-authorized Git integration workflow.

## Release 1.8 Dependency Graph

``` text
WP01 Release & Repository Preflight
 ↓
WP02 Python Runtime Compatibility & Version Selection
 ↓
WP03 Windows Machine-Wide Python Foundation
 ↓
WP04 PowerShell & VS Code Python Validation
 ↓
WP05 Virtual Environment & Dependency Isolation
 ↓
WP06 Python Dependency Governance
 ↓
WP07 Scientific & Machine Learning Library Foundation
 ↓
WP08 Streamlit Visualization Foundation
 ↓
WP09 .NET ↔ Python Interoperability Architecture
 ↓
WP10 .NET ↔ Python Integration Proof
 ↓
WP11 Python Foundation & Interoperability Tests
 ↓
WP12 Architecture, Documentation & Developer Environment Alignment
 ↓
WP13 Full Validation, Integration & Acceptance
```

## Post-Acceptance Lifecycle

WP13 acceptance does not mutate Git history.

After explicit human authorization:

1.  separate Git integration authority;
2.  governed branch/commit/push;
3.  PR;
4.  human merge authorization;
5.  post-merge closure;
6.  freeze merged SHA as authoritative Release 1.8 baseline;
7.  only then begin Release 1.9 definition/planning.

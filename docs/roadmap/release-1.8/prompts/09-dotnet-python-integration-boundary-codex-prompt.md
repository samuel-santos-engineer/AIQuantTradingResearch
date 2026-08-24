# Release 1.8 WP09 — .NET ↔ Python Integration Boundary — Codex Authority

## 1. Mission

Execute Release 1.8 WP09 — **.NET ↔ Python Integration Boundary** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#219`

Milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established Release 1.8 state:

- #211–#218: CLOSED / Done;
- #219: OPEN / Backlog;
- #220–#223: OPEN / Backlog;
- milestone #56: OPEN, 5 open / 8 closed;
- official machine runtime: CPython 3.13.15 amd64;
- project environment: `.venv`, isolated and Git-ignored;
- dependency governance: established;
- governed direct dependencies:
  - NumPy 2.5.1;
  - pandas 3.0.5;
  - scikit-learn 1.9.0;
  - Streamlit 1.61.1;
- WP08 validation root: `python/validation/`;
- WP08 validation scripts:
  - `python/validation/scientific_stack_validation.py`;
  - `python/validation/streamlit_validation_app.py`;
- WP08 repeated validation: 3×, all 4/4 PASS;
- canonical .NET baseline: 268/268;
- schema: v3.

WP09 defines and governs the architectural boundary by which the existing .NET platform may interact with Python.

WP09 is architecture and contract authority first. It must not silently implement the Release 1.9 ML system.

---

## 2. Foundational Selection Rule

Preserve the standing project rule:

> Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.

The .NET↔Python integration mechanism is a foundational architectural choice and therefore requires an explicit engineering selection record.

Do not choose an integration mechanism merely because it is convenient to implement.

---

## 3. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_SELECTION.md`
- `docs/architecture/implementation/PYTHON_RUNTIME_COMPATIBILITY.md`
- `docs/architecture/implementation/PYTHON_DEPENDENCY_GOVERNANCE.md`
- NumPy selection record;
- pandas selection record;
- scikit-learn selection record;
- Streamlit selection record;
- existing solution architecture, dependency rules, boundary definitions, public contracts, error handling, resilience, timeout, logging, observability, configuration, and testing documentation;
- WP08 validation artifacts;
- GitHub issue #219;
- Project #2 Release 1.8 state.

Use exact live canonical filenames.

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
- WP08 files exist exactly where governed;
- `.venv/` remains ignored;
- `requirements.txt` retains exact WP07 pins.

### GitHub

- #211–#218: CLOSED / Done;
- #219: OPEN / Backlog;
- #220–#223: OPEN / Backlog;
- milestone #56: OPEN, 5 open / 8 closed;
- Project membership: 13/13;
- duplicates: 0;
- WP08→WP09 dependency exists;
- fields/dependency chain remain authoritative.

### Platform

Require:

- Python 3.13.15 x64;
- `.venv` isolated;
- exact four direct pins preserved;
- `pip check`: PASS;
- WP08 validation passes;
- canonical .NET 268/268 baseline passes.

Stop on unexplained drift.

---

## 5. WP09 Decision Question

WP09 must answer:

**What is the governed architectural mechanism by which AIQuantTradingResearch .NET components may invoke Python capabilities while preserving isolation, portability, testability, resilience, observability, and future evolution?**

The answer must be explicit and durable.

Do not leave multiple mechanisms equally authoritative.

---

## 6. Candidate Integration Mechanisms

Evaluate at minimum:

### A. Out-of-process local Python worker/process

.NET starts or communicates with a separately executing Python process using an explicit contract.

Potential characteristics:

- runtime isolation;
- no Python interpreter embedded into the .NET process;
- process lifecycle can be controlled;
- failures can be classified at a boundary;
- serialization contract required;
- startup/process overhead accepted.

### B. Embedded Python/runtime hosting

Python is hosted inside the .NET process through an embedding/interoperability library.

Evaluate:

- native/runtime coupling;
- deployment complexity;
- process failure blast radius;
- GIL/runtime lifecycle implications where relevant;
- library compatibility;
- testing;
- additional foundational dependency requirements.

### C. Local HTTP/service boundary

Python runs as a separately hosted local service and .NET communicates through HTTP.

Evaluate:

- contract clarity;
- process/service lifecycle;
- local networking;
- ports;
- security;
- deployment/operational complexity;
- future remote-service evolution.

### D. Other credible mechanism already supported by authoritative project architecture

Evaluate only if evidence supports it.

Do not introduce speculative alternatives solely to enlarge the document.

---

## 7. Selection Criteria

Compare candidates against explicit criteria:

- alignment with existing architecture;
- .NET domain/application purity;
- Python runtime isolation;
- deterministic environment selection;
- failure isolation;
- cancellation;
- timeout enforcement;
- process lifecycle;
- serialization complexity;
- observability;
- testability;
- local development ergonomics;
- Windows support;
- future Linux/container portability;
- security;
- deployment complexity;
- dependency/tooling footprint;
- performance expectations;
- suitability for future Release 1.9 ML workloads;
- reversibility/evolution cost.

Use concise evidence-based reasoning.

---

## 8. Preferred Architectural Direction

Unless authoritative repository evidence establishes a better choice, prefer an **out-of-process integration boundary** over embedding Python directly into the .NET process.

This preference is not permission to skip evaluation.

The final selection must be justified against the criteria above.

If the selected mechanism requires a new external foundational runtime/library/framework/tool not already governed, do not install it in WP09. Record the dependency requirement and stop for separate selection authority if it is necessary to complete the decision safely.

---

## 9. Layer Ownership

The integration boundary must preserve existing dependency rules.

At minimum define:

### Domain

Must remain independent of:

- Python;
- process APIs;
- serialization implementation;
- filesystem interpreter discovery;
- ML libraries;
- Streamlit.

### Application

May define technology-neutral ports/contracts/use-case abstractions if the accepted architecture permits.

It must not own:

- `Process` mechanics;
- Python executable paths;
- OS-specific launch details;
- concrete JSON/process transport implementation.

### Infrastructure

Owns concrete Python integration mechanics, including where applicable:

- interpreter resolution;
- process invocation/transport;
- serialization;
- timeout enforcement;
- cancellation propagation;
- stdout/stderr handling;
- exit-code handling;
- process cleanup;
- environment construction;
- failure translation.

### Composition root/host

Owns configuration and registration/wiring according to existing DI rules.

Do not mutate project references in WP09 unless explicitly manifest-authorized.

---

## 10. Python Ownership

Define the corresponding Python-side boundary.

Future production Python integration code must be distinct from:

`python/validation/`

WP08 validation scripts are evidence only and must not become production integration endpoints.

WP09 must define the intended ownership/location conceptually or exactly as authorized by the manifest, but must not invent implementation paths that conflict with future work-package ownership.

If the manifest does not authorize an exact production Python path yet, document the architectural ownership and defer exact file creation.

---

## 11. Interpreter Resolution Policy

Define how .NET will identify the project Python runtime.

Requirements:

- no hard-coded user profile path;
- no assumption that bare `python` resolves correctly;
- project `.venv` is the governed dependency environment;
- machine CPython is bootstrap/base runtime, not the project dependency execution target;
- Windows behavior is defined;
- future non-Windows portability is considered;
- missing/interpreter-invalid conditions are explicit failures.

Prefer deterministic repository-relative `.venv` resolution where compatible with existing architecture.

Do not modify PATH in WP09.

---

## 12. Invocation Contract

Define a technology-neutral logical request/response contract suitable for future implementation.

At minimum specify:

### Request

- operation identifier;
- contract/schema version;
- correlation/request identifier if consistent with existing observability conventions;
- bounded input payload;
- optional execution metadata only where justified.

### Success response

- contract/schema version;
- success status;
- structured result payload;
- diagnostic metadata only where safe/useful.

### Failure response

- failure category/code;
- safe message;
- retryability semantics where existing failure vocabulary permits;
- no secret leakage;
- diagnostic correlation.

Do not define Release 1.9 model-specific features or predictions.

Use neutral validation-oriented examples if examples are required.

---

## 13. Serialization Boundary

Select/document a serialization approach appropriate to the chosen integration mechanism.

For an out-of-process boundary, JSON is a reasonable default if consistent with project conventions.

Define:

- encoding;
- schema/version field;
- nullability expectations;
- number handling;
- date/time handling if applicable;
- invariant/culture-independent representation;
- maximum/bounded payload expectations;
- stdout protocol discipline.

If stdout carries protocol messages, Python diagnostic logging must not corrupt the protocol stream.

A common safe rule is:

- stdout: structured protocol output;
- stderr: diagnostics.

Document the chosen rule explicitly.

---

## 14. Contract Versioning

The .NET↔Python boundary must be versionable independently of implementation details.

Define:

- initial contract version semantics;
- compatibility expectations;
- unknown-version behavior;
- additive vs breaking changes;
- when version increment is required.

Do not reuse SQLite schema version as the integration contract version unless architecture explicitly requires it.

---

## 15. Process Lifecycle

For a process-based selection define:

- process creation;
- working directory;
- interpreter path;
- script/entrypoint resolution;
- environment variables;
- stdin/stdout/stderr ownership;
- exit code semantics;
- normal completion;
- timeout termination;
- cancellation termination;
- abnormal exit;
- cleanup;
- no orphan processes.

Do not use broad process-kill operations.

Only processes created/owned by the integration boundary may be terminated.

The legitimate VS Code Jedi language-server process is outside platform ownership.

---

## 16. Timeout and Cancellation

Align with existing platform timeout/cancellation architecture.

Define:

- caller-provided cancellation propagation;
- bounded execution timeout;
- precedence between cancellation and timeout;
- process termination responsibility;
- failure classification;
- cleanup guarantee.

Do not invent an independent resilience vocabulary if the repository already governs one.

Reuse existing failure concepts where applicable.

---

## 17. Failure Taxonomy

Map integration failures into existing platform failure semantics without leaking Python/process implementation details upward.

Consider:

- interpreter missing;
- environment invalid;
- entrypoint missing;
- launch failure;
- malformed request;
- unsupported contract version;
- malformed response;
- Python exception/failure response;
- non-zero exit;
- timeout;
- cancellation;
- serialization failure;
- unexpected process termination.

Distinguish transient/retryable from permanent/non-retryable only according to existing resilience policy.

Do not make all Python failures retryable.

---

## 18. Observability

Define boundary-level observability consistent with existing logging/observability standards.

Include where appropriate:

- operation;
- contract version;
- correlation identifier;
- duration;
- outcome;
- failure category;
- process exit code;
- timeout/cancellation indicator.

Never log:

- credentials;
- secret environment variables;
- unrestricted raw payloads;
- sensitive future model inputs by default.

Do not create a parallel logging framework.

---

## 19. Security Boundary

Define security expectations:

- no shell-string command construction when argument-safe APIs are available;
- no untrusted executable path;
- no arbitrary script execution;
- no user-controlled interpreter selection without validation;
- no credentials on command line;
- environment variables minimized;
- working directory deterministic;
- payload bounded;
- protocol input validated;
- no external network requirement for local integration by default.

Do not introduce remote execution.

---

## 20. Performance Boundary

Document expected trade-offs rather than optimizing prematurely.

For out-of-process invocation consider:

- process startup overhead;
- serialization overhead;
- isolation benefit;
- potential future persistent-worker evolution if profiling proves necessary.

Do not introduce a persistent daemon/service solely for theoretical performance.

Define the condition under which the decision should be revisited, such as measured process-start overhead becoming material to product requirements.

---

## 21. Testing Strategy

Define future test layers without implementing WP10+ work unless explicitly assigned.

At minimum distinguish:

- .NET unit tests using a technology-neutral abstraction/fake;
- infrastructure contract/process tests;
- Python-side contract tests;
- end-to-end local integration tests;
- failure-path tests;
- timeout/cancellation/process-cleanup tests.

WP08 validation scripts remain separate scientific-stack evidence.

Do not convert WP08 scripts into integration tests.

---

## 22. Development and Deployment Boundary

Document:

### Development

- machine Python bootstraps `.venv`;
- `.venv` contains governed dependencies;
- .NET resolves the governed project interpreter deterministically;
- no global scientific packages required.

### Future deployment

Avoid assuming Windows-only absolute paths.

Describe how the abstraction could support:

- Linux;
- containers;
- CI;
- separately packaged Python runtime/environment.

Do not implement deployment/containerization in WP09.

---

## 23. Selection Record

Create the manifest-authorized engineering selection record.

If the manifest does not already specify an exact filename, preferred canonical name:

`docs/architecture/implementation/DOTNET_PYTHON_INTEGRATION_SELECTION.md`

It must contain:

- decision;
- context;
- evaluated alternatives;
- criteria;
- selected mechanism;
- rationale;
- accepted trade-offs;
- rejected alternatives and reasons;
- runtime/dependency implications;
- architectural boundaries;
- security/resilience/observability implications;
- portability;
- version/evolution policy;
- reconsideration triggers.

If exact path ownership is ambiguous, stop instead of inventing a file location.

---

## 24. Boundary Architecture Document

If the Release 1.8 manifest assigns a separate WP09 architecture artifact, create/update it exactly as governed.

It should define:

- .NET ownership;
- Python ownership;
- interpreter resolution;
- logical contract;
- serialization;
- lifecycle;
- timeout/cancellation;
- failure mapping;
- observability;
- security;
- testing;
- portability.

Do not duplicate the selection record unnecessarily. The selection record answers **why this mechanism**; the boundary document answers **how the architectural boundary is governed**.

If only one document is manifest-authorized, combine responsibilities cleanly rather than inventing another artifact.

---

## 25. Implementation Boundary

WP09 must remain architecture-first.

Unless the accepted Release 1.8 execution plan explicitly assigns a minimal executable spike to WP09, expected implementation deltas are:

- .NET production code: 0;
- Python production code: 0;
- permanent tests: 0;
- .NET packages: 0;
- Python direct packages: 0;
- project references: 0;
- schema: 0.

If the planning authority explicitly requires a bounded proof/spike, follow it exactly and clearly classify it as validation—not Release 1.9 implementation.

Do not infer implementation authority from this document alone.

---

## 26. Dependency Consequences

If the selected mechanism can be implemented later using only existing .NET BCL/process/serialization capabilities and Python standard-library capabilities, record that no new foundational integration library is required.

If it requires something such as:

- Python.NET;
- gRPC framework;
- web framework;
- message broker;
- external IPC library;
- another runtime/tool;

then apply the foundational selection-record rule before introduction.

WP09 may evaluate such options but may not install them without authority.

---

## 27. Existing Platform Regression Protection

Run canonical repository verification after documentation/architecture changes.

Expected baseline:

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
- terminal newline: PASS;
- trailing whitespace: 0;
- conflict markers: 0;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- schema: v3;
- existing .NET dependency graph unchanged/acyclic;
- .NET package/project/reference delta: 0/0/0 unless explicitly authorized;
- Python direct dependency pins unchanged.

Reconcile legitimate governed baseline changes rather than falsifying counts.

---

## 28. Python Regression Protection

Require:

- Python 3.13.15;
- `.venv` isolated;
- NumPy 2.5.1;
- pandas 3.0.5;
- scikit-learn 1.9.0;
- Streamlit 1.61.1;
- `pip check`: PASS;
- WP08 scientific validation: PASS;
- machine-global direct packages remain absent;
- no unexpected Python package mutations.

WP09 architecture work must not destabilize the proven scientific foundation.

---

## 29. Explicit Non-Goals

Do not:

- train or persist ML models;
- define Release 1.9 model features;
- consume real market data from Python;
- implement final .NET↔Python production integration unless explicitly assigned by accepted WP09 planning;
- create Streamlit product UI;
- alter experiment evidence semantics;
- alter schema v3;
- introduce Python.NET or another interoperability package without selection authority;
- introduce HTTP/gRPC frameworks without selection authority;
- introduce pytest/Jupyter/MLflow;
- modify machine Python/PATH;
- modify VS Code;
- execute WP10+;
- begin Release 1.9;
- stage, commit, push, branch, PR, merge, tag, or release.

---

## 30. Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE:

- INT1 — starting repository/GitHub/platform state reconciled;
- INT2 — candidate mechanisms evaluated explicitly;
- INT3 — exactly one integration mechanism selected authoritatively;
- INT4 — foundational integration selection record created/reconciled;
- INT5 — Domain remains Python-agnostic;
- INT6 — Application boundary remains technology-neutral;
- INT7 — Infrastructure ownership is explicit;
- INT8 — Python production boundary is distinct from `python/validation/`;
- INT9 — interpreter resolution policy is deterministic and portable;
- INT10 — logical request/response contract is defined;
- INT11 — serialization/protocol discipline is defined;
- INT12 — independent contract versioning is defined;
- INT13 — lifecycle/process ownership is defined;
- INT14 — timeout/cancellation semantics align with platform governance;
- INT15 — failure mapping aligns with existing vocabulary;
- INT16 — observability/security boundaries are defined;
- INT17 — testing strategy separates unit/contract/integration/end-to-end concerns;
- INT18 — portability/evolution/reconsideration triggers are documented;
- INT19 — tracked mutations match manifest authority exactly;
- INT20 — no unauthorized implementation/dependency/schema/process residue exists.

---

## 31. Mutation Accounting

Report exact deltas for:

- selection records;
- architecture/boundary documentation;
- Release 1.8 manifest;
- .NET production code;
- Python production code;
- WP08 validation files;
- permanent tests;
- `requirements.txt`;
- `.venv` packages;
- machine-global packages;
- schema/database;
- .NET packages/projects/references;
- processes/ports;
- VS Code/machine Python;
- Git;
- GitHub.

---

## 32. GitHub Lifecycle

Only after WP09 passes:

1. transition #219 to In Progress if needed;
2. add concise evidence including selected mechanism and architectural boundary;
3. close #219;
4. set #219 Project Status to Done.

Expected final state:

- #211–#219: CLOSED / Done;
- #220–#223: OPEN / Backlog;
- milestone #56: OPEN, 4 open / 9 closed;
- Project membership: 13/13;
- duplicates: 0;
- fields/dependency chain unchanged.

Do not transition #220 automatically.

---

## 33. Stop Conditions

Stop with:

`RELEASE 1.8 WP09 BLOCKED`

if:

- starting state is inconsistent;
- manifest ownership for WP09 artifacts is ambiguous;
- accepted architecture conflicts materially with all evaluated mechanisms;
- selecting the mechanism requires introducing an ungoverned foundational dependency;
- layer ownership cannot be reconciled with existing dependency rules;
- contract/failure/resilience semantics conflict with existing governance;
- completing WP09 would require unauthorized production implementation;
- canonical .NET/Python verification fails;
- WP10+/Release 1.9 work would be required.

Report exact blocker and smallest corrective authority required.

---

## 34. Required Execution Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- GitHub lifecycle;
- .NET/Python baseline;
- manifest ownership.

### Alternatives
For each evaluated mechanism:
- strengths;
- weaknesses;
- dependency implications;
- portability;
- failure/isolation characteristics.

### Decision
- selected mechanism;
- rationale;
- accepted trade-offs;
- rejected alternatives;
- reconsideration triggers.

### Architecture
- Domain;
- Application;
- Infrastructure;
- host/composition;
- Python-side ownership;
- separation from WP08 validation.

### Contract
- request;
- success;
- failure;
- serialization;
- versioning.

### Runtime
- interpreter resolution;
- process/service lifecycle;
- timeout;
- cancellation;
- cleanup;
- portability.

### Cross-Cutting
- failure classification;
- observability;
- security;
- testing.

### Validation
- INT1–INT20;
- 268/268 .NET;
- WP08 Python validation;
- `pip check`;
- build/format/Gitleaks/docs/diff;
- schema/graph;
- dependency cleanliness.

### Mutation Accounting
- all repository/workstation/GitHub deltas.

### Final State
- #219 lifecycle;
- milestone #56;
- next authorized WP.

---

## 35. Completion Markers

On success end exactly:

`RELEASE 1.8 WP09 COMPLETE`

`.NET ↔ PYTHON INTEGRATION BOUNDARY: ESTABLISHED`

`NEXT AUTHORIZED WORK PACKAGE: WP10 — Python Integration Infrastructure Foundation — GitHub issue #220`

If the authoritative live title of #220 differs, use the exact live GitHub issue title in the final marker without changing its scope.

Do not execute WP10 automatically.

If blocked end exactly:

`RELEASE 1.8 WP09 BLOCKED`

# Release 1.8 WP10 — Python Integration Infrastructure Foundation — Codex Authority

## 1. Mission

Execute Release 1.8 WP10 — **Python Integration Infrastructure Foundation** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#220`

Milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established Release 1.8 state:

- #211–#219: CLOSED / Done;
- #220: OPEN / Backlog;
- #221–#223: OPEN / Backlog;
- milestone #56: OPEN, 4 open / 9 closed;
- machine runtime: official CPython 3.13.15 amd64;
- project environment: `.venv`, isolated and Git-ignored;
- governed direct dependencies:
  - NumPy 2.5.1;
  - pandas 3.0.5;
  - scikit-learn 1.9.0;
  - Streamlit 1.61.1;
- WP08 scientific-stack validation: PASS;
- WP09 architectural decision:
  - **local one-shot out-of-process Python invocation**;
  - **versioned JSON-over-stdio contract**;
  - governed project `.venv` interpreter;
- authoritative interoperability record:
  - `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`;
- canonical .NET baseline: 268/268;
- schema: v3.

WP10 implements only the infrastructure foundation required to make the selected WP09 boundary executable.

WP10 does not implement Release 1.9 ML behavior, real model workflows, Streamlit product UI, or production quantitative analysis.

---

## 2. Architectural Authority

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
- relevant dependency rules;
- boundary definitions;
- public contracts;
- error handling;
- failure classification;
- timeout strategy;
- resilience model;
- logging strategy;
- observability model;
- dependency injection;
- testing strategy;
- Python dependency governance;
- WP08 validation artifacts;
- GitHub issue #220;
- Project #2 Release 1.8 state.

Treat WP09 as authoritative.

Do not reopen the selected mechanism.

---

## 3. Mandatory Starting-State Gate

Before mutation verify:

### Repository

- correct repository and remote;
- branch `main`;
- local HEAD equals `origin/main`;
- ahead/behind `0/0`;
- staged paths: 0;
- no unexplained tracked changes;
- governed Release 1.8 documentation state exists;
- WP08 validation artifacts are unchanged;
- WP09 interoperability record exists.

### GitHub

- #211–#219: CLOSED / Done;
- #220: OPEN / Backlog;
- #221–#223: OPEN / Backlog;
- milestone #56: OPEN, 4 open / 9 closed;
- Project membership: 13/13;
- duplicates: 0;
- WP09→WP10 dependency exists;
- fields/dependency chain remain authoritative.

### Python

Require:

- Python 3.13.15 x64;
- `.venv` isolated;
- NumPy 2.5.1;
- pandas 3.0.5;
- scikit-learn 1.9.0;
- Streamlit 1.61.1;
- `pip check`: PASS;
- WP08 validation: PASS;
- no machine-global leakage of the four direct packages.

### .NET

Require canonical baseline:

- Domain.Tests: 11;
- Application.Tests: 119;
- Infrastructure.Tests: 125;
- Architecture.Tests: 13;
- total: 268.

Stop on unexplained drift.

---

## 4. WP10 Scope

WP10 may implement only the infrastructure needed for:

1. deterministic `.venv` Python interpreter resolution;
2. deterministic Python entrypoint resolution;
3. one-shot process launch;
4. JSON request serialization;
5. stdin request delivery;
6. stdout response capture;
7. stderr diagnostic capture;
8. contract-version validation;
9. response deserialization;
10. exit-code handling;
11. timeout enforcement;
12. cancellation propagation;
13. owned-process termination/cleanup;
14. bounded failure translation;
15. observability consistent with existing platform standards;
16. DI registration only if explicitly required by the manifest/execution plan;
17. a minimal Python protocol endpoint only if manifest-owned by WP10.

Do not broaden beyond this boundary.

---

## 5. Layer Ownership

Preserve WP09 ownership exactly.

### Domain

Expected WP10 delta:

`0`

Domain must remain independent of:

- Python;
- process APIs;
- JSON transport;
- interpreter paths;
- filesystem/process execution;
- Python package types.

### Application

May contain only technology-neutral abstractions/contracts explicitly assigned by the manifest.

Do not leak:

- `System.Diagnostics.Process`;
- Python executable paths;
- concrete JSON serialization concerns;
- OS-specific launch details.

### Infrastructure

Own concrete implementation mechanics:

- `.venv` interpreter resolution;
- process creation;
- stdin/stdout/stderr;
- serialization implementation;
- timeout/cancellation enforcement;
- process cleanup;
- exit codes;
- failure classification;
- environment/working-directory setup.

### Worker/composition root

May receive minimal DI/configuration changes only if explicitly owned by WP10.

Do not invoke Python automatically from normal Worker execution unless the accepted plan assigns such invocation to WP10.

---

## 6. Manifest Authority First

Before creating files, read `RELEASE_1.8_FILE_MANIFEST.md`.

Use only WP10-authorized paths.

If exact infrastructure/application/Python entrypoint paths are ambiguous, stop rather than inventing a long-lived project structure.

Do not use WP08 `python/validation/` scripts as production integration endpoints.

The WP10 Python endpoint must be distinct if one is authorized.

---

## 7. Contract Preservation

Implement only the contract governed by:

`DOTNET_PYTHON_INTEROPERABILITY.md`

Do not invent a new protocol.

Preserve:

- initial contract version;
- request shape;
- success shape;
- failure shape;
- operation identifier semantics;
- bounded payload model;
- stdout/stderr discipline;
- unknown-version behavior;
- failure categories.

If WP09 defines conceptual contract names but not exact code types/files, follow the manifest.

If code ownership remains ambiguous, stop.

---

## 8. JSON Serialization

Use existing .NET platform/BCL serialization capability where sufficient.

Prefer no new .NET package.

Require:

- UTF-8;
- deterministic/culture-independent serialization;
- explicit property naming consistent with WP09;
- bounded payload handling;
- predictable null behavior;
- no arbitrary polymorphic deserialization;
- no secret-bearing payload logging.

Do not add Newtonsoft.Json or another serializer unless separately governed and required.

---

## 9. Interpreter Resolution

The production integration path must resolve the repository-local project interpreter deterministically.

Windows expected semantic target:

`.venv\Scripts\python.exe`

Requirements:

- repository-relative;
- no user-profile hard-coding;
- no use of WindowsApps alias;
- no reliance on bare `python`;
- existence/file validation;
- executable provenance validation sufficient for the governed boundary;
- missing interpreter maps to bounded failure;
- future portability remains possible.

Do not modify PATH.

---

## 10. Python Entrypoint Resolution

If WP10 owns a Python protocol entrypoint, resolve it through a deterministic repository-relative path governed by the manifest.

Requirements:

- distinct from `python/validation/`;
- no arbitrary script path supplied by caller;
- no path traversal;
- missing entrypoint produces bounded failure;
- working directory is deterministic.

Do not allow the integration layer to execute arbitrary Python files.

---

## 11. Safe Process Construction

Use argument-safe process APIs.

Do not build shell command strings.

Require:

- `UseShellExecute = false` or platform-equivalent safe semantics;
- stdin redirected;
- stdout redirected;
- stderr redirected;
- no visible shell/window where repository conventions require headless execution;
- deterministic working directory;
- explicit interpreter executable;
- explicit entrypoint argument;
- environment minimized/controlled;
- no credentials on arguments.

Do not invoke `cmd.exe` or PowerShell as an intermediary unless the architecture explicitly requires it.

---

## 12. One-Shot Lifecycle

Exactly one Python process per invocation.

Lifecycle:

1. resolve interpreter;
2. resolve entrypoint;
3. construct request;
4. start process;
5. write exactly one request;
6. close/complete stdin;
7. asynchronously capture stdout/stderr;
8. wait for completion under timeout/cancellation;
9. validate exit/protocol;
10. deserialize response;
11. translate failure if needed;
12. dispose all process resources;
13. prove no orphan process remains.

Do not create a persistent worker/daemon.

---

## 13. Stdout / Stderr Discipline

Preserve WP09 protocol rule.

Expected:

- stdout = structured protocol response only;
- stderr = diagnostics only.

Python endpoint must not emit human-readable logging to stdout.

.NET must not parse stderr as success protocol data.

If stdout contains malformed/multiple protocol frames when exactly one is expected, fail closed.

Do not silently skip garbage lines.

---

## 14. Process I/O Deadlock Safety

Avoid classic redirected-process deadlocks.

Read stdout and stderr asynchronously/concurrently where appropriate.

Do not:

- write stdin and block incorrectly while buffers fill;
- read only stdout while stderr can block;
- use unsafe synchronous sequencing that can deadlock.

Implementation must be demonstrably bounded.

---

## 15. Timeout

Use existing platform timeout governance.

Requirements:

- timeout is bounded;
- timeout ownership is explicit;
- timeout causes only the owned child process to be terminated;
- process tree handling is deliberate where supported;
- cleanup is awaited/verified;
- timeout maps to existing failure semantics;
- no orphan process remains.

Do not invent retry behavior.

---

## 16. Cancellation

Honor caller cancellation where the Application boundary provides it.

Define/implement precedence consistent with WP09:

- cancellation;
- timeout;
- process completion.

On cancellation:

- stop only the owned process;
- clean resources;
- propagate/translate according to existing platform policy;
- do not normalize unknown defects incorrectly.

---

## 17. Exit Codes

Define implementation behavior for:

- exit code 0 + valid success response;
- exit code 0 + valid failure response if protocol permits;
- non-zero exit;
- no response;
- malformed response;
- response plus stderr diagnostics;
- process start failure.

Use WP09 failure semantics.

Do not expose raw process implementation details above the infrastructure boundary unless explicitly part of safe diagnostics.

---

## 18. Failure Translation

Reuse existing application/infrastructure failure vocabulary where WP09 mapped it.

Do not create a parallel Python-specific failure universe unless WP09 explicitly authorized one.

At minimum handle the governed equivalents of:

- runtime/interpreter unavailable;
- entrypoint unavailable;
- invalid request;
- unsupported contract version;
- malformed response;
- Python-reported bounded failure;
- non-zero process failure;
- timeout;
- cancellation;
- unexpected defect.

Unknown defects must not be silently normalized.

---

## 19. Python Protocol Endpoint

If manifest-owned, implement the smallest Python protocol endpoint necessary to prove the infrastructure.

It must:

- use Python standard library wherever sufficient;
- read exactly one JSON request from stdin;
- validate contract version;
- validate operation;
- return exactly one JSON response on stdout;
- send diagnostics to stderr;
- return deterministic exit status;
- contain no Release 1.9 ML logic;
- not import NumPy/pandas/scikit-learn/Streamlit unless the protocol proof explicitly requires a capability check authorized by the plan;
- not access network/providers;
- not persist data;
- not mutate SQLite.

Prefer a neutral operation such as a bounded integration health/echo/capability proof if authorized.

Do not turn WP10 into scientific integration.

---

## 20. Operation Scope

Use only neutral Release 1.8 infrastructure operations.

Examples, if consistent with WP09:

- contract handshake;
- health/capability response;
- bounded echo of a safe scalar/structured payload.

Do not implement:

- model training;
- prediction;
- feature generation;
- experiment evidence discovery;
- dataset transformation;
- market-data acquisition;
- Streamlit behavior.

Those belong elsewhere.

---

## 21. Configuration

If configuration is required, follow existing configuration governance.

Prefer repository-relative/configurable integration settings without user-specific absolute paths.

Possible concerns:

- integration enabled/disabled;
- relative Python environment path;
- relative entrypoint path;
- timeout.

Do not add configuration values that expose implementation mechanics to Domain.

Do not add secrets.

---

## 22. Dependency Injection

If WP10 owns registration, follow existing DI conventions.

Requirements:

- exact registration cardinality;
- appropriate lifetime;
- no side effects during service resolution;
- no process start during DI resolution;
- no Python environment mutation during DI resolution.

Do not wire automatic Worker execution unless separately owned.

---

## 23. Observability

Implement only existing logging/observability abstractions.

Record safely where appropriate:

- operation;
- contract version;
- duration;
- outcome;
- failure class;
- exit code where useful;
- timeout/cancellation state.

Do not log full payloads by default.

Do not log secrets or raw environment variables.

Do not introduce another logging library.

---

## 24. Process Ownership Safety

Because VS Code's Python extension legitimately runs Python processes, process cleanup must be ownership-safe.

WP10 may terminate only the exact child process/process tree it created.

Never:

- kill all `python.exe`;
- kill by process name;
- kill VS Code Jedi/Pylance/helper processes;
- terminate unrelated user Python workloads.

Verification must distinguish owned from unrelated processes.

---

## 25. Security

Require:

- no shell injection;
- no caller-controlled executable;
- no arbitrary script execution;
- no path traversal;
- bounded input size;
- safe JSON parsing;
- no remote network requirement;
- no credentials;
- no disabling TLS/security;
- no environment-variable dumping.

If process environment inheritance is used, evaluate/minimize it according to WP09.

---

## 26. Portability

Implement Windows behavior needed for Release 1.8 without making the abstraction Windows-only.

Avoid:

- committed absolute `C:\...` paths;
- Windows-specific types leaking into Application;
- assumptions impossible to adapt to Linux/container execution later.

OS-specific path resolution may live in Infrastructure.

---

## 27. Implementation Validation Matrix

Create removable/targeted validation sufficient to prove implementation before permanent test ownership if the manifest assigns permanent tests to WP11.

Report PASS/FAIL/NOT-APPLICABLE:

- INF1 — starting state reconciled;
- INF2 — Application contracts remain technology-neutral;
- INF3 — Domain delta is zero;
- INF4 — `.venv` interpreter resolves deterministically;
- INF5 — missing interpreter fails boundedly;
- INF6 — entrypoint resolves deterministically;
- INF7 — arbitrary script execution is impossible through the normal API;
- INF8 — one-shot process launches successfully;
- INF9 — JSON request reaches Python correctly;
- INF10 — valid response deserializes correctly;
- INF11 — stderr diagnostics do not corrupt stdout protocol;
- INF12 — unsupported version fails correctly;
- INF13 — malformed response fails closed;
- INF14 — non-zero exit maps correctly;
- INF15 — timeout terminates only owned process and leaves no orphan;
- INF16 — cancellation terminates only owned process and leaves no orphan;
- INF17 — stdout/stderr handling has no deadlock in governed proof;
- INF18 — no provider/network/database/schema mutation;
- INF19 — package/project/reference changes match authority exactly;
- INF20 — temporary validation/process/file residue is zero.

If WP11 owns permanent tests, temporary probes must be removed completely.

---

## 28. Python Regression Validation

After implementation require:

- Python 3.13.15;
- exact four direct pins unchanged;
- `pip check`: PASS;
- WP08 validation: PASS;
- machine-global package cleanliness: PASS;
- `.venv` remains ignored;
- no unexpected Python dependencies added.

Do not modify `requirements.txt` in WP10 unless explicitly authorized.

---

## 29. .NET Regression Validation

Run canonical repository verification.

Expected governed baseline:

- Domain.Tests: 11/11;
- Application.Tests: 119/119;
- Infrastructure.Tests: 125/125;
- Architecture.Tests: 13/13;
- total permanent baseline before any WP10 manifest-owned tests: 268/268;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS;
- Markdown links: PASS;
- terminal newlines: PASS;
- trailing whitespace: 0;
- conflict markers: 0;
- `git diff --check`: PASS;
- `git diff --cached --check`: PASS;
- schema: v3;
- dependency graph remains acyclic;
- package/project/reference delta only if explicitly authorized.

If WP10 owns no permanent tests, test-count delta must be zero.

---

## 30. File Manifest Ownership

Follow `RELEASE_1.8_FILE_MANIFEST.md` exactly.

Only create/modify files assigned to WP10.

Possible categories include:

- technology-neutral Application integration contracts;
- Infrastructure process/invocation implementation;
- Python protocol endpoint;
- DI/configuration wiring if explicitly owned;
- WP10 documentation/evidence if manifest-owned.

Do not invent extra convenience abstractions.

---

## 31. Explicit Non-Goals

Do not:

- implement Release 1.9 ML;
- train models;
- perform predictions;
- add model/feature contracts;
- call market providers;
- mutate SQLite;
- alter experiment evidence semantics;
- create final Streamlit UI;
- introduce Python.NET;
- introduce HTTP/gRPC framework;
- introduce new Python packages;
- introduce new .NET packages unless explicitly selected/governed;
- create persistent Python services;
- execute WP11+;
- begin Release 1.9;
- stage, commit, push, branch, PR, merge, tag, or release.

---

## 32. Mutation Accounting

Report exact deltas for:

- Domain;
- Application;
- Infrastructure;
- Worker/composition;
- Python production integration files;
- WP08 validation;
- permanent tests;
- temporary probes;
- `requirements.txt`;
- Python packages;
- schema/database;
- .NET packages/projects/references;
- configuration;
- processes/ports;
- VS Code/machine Python;
- Git;
- GitHub.

---

## 33. GitHub Lifecycle

Only after every WP10 gate passes:

1. transition #220 to In Progress if needed;
2. add concise implementation/validation evidence;
3. close #220;
4. set #220 Project Status to Done.

Expected final state:

- #211–#220: CLOSED / Done;
- #221–#223: OPEN / Backlog;
- milestone #56: OPEN, 3 open / 10 closed;
- Project membership: 13/13;
- duplicates: 0;
- fields/dependency chain unchanged.

Do not transition #221 automatically.

---

## 34. Stop Conditions

Stop with:

`RELEASE 1.8 WP10 BLOCKED`

if:

- starting state is inconsistent;
- manifest ownership is ambiguous;
- WP09 contract is insufficiently concrete to implement safely;
- implementation requires an ungoverned foundational package/tool;
- Application would need to leak process/Python mechanics;
- interpreter/entrypoint resolution cannot be deterministic;
- failure/timeout/cancellation semantics conflict with existing governance;
- process cleanup cannot be proven safely;
- implementation requires Release 1.9 behavior;
- canonical .NET/Python validation fails;
- unexplained package/schema/reference drift occurs.

Report exact partial state and smallest corrective authority required.

---

## 35. Required Execution Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- GitHub lifecycle;
- .NET/Python baseline;
- manifest-owned files.

### Implementation
- Application abstractions/contracts;
- Infrastructure implementation;
- Python endpoint;
- DI/configuration;
- interpreter/entrypoint resolution;
- request/response protocol.

### Lifecycle
- process construction;
- stdin/stdout/stderr;
- exit handling;
- timeout;
- cancellation;
- cleanup.

### Failures
- missing interpreter;
- missing entrypoint;
- invalid version;
- malformed response;
- non-zero exit;
- timeout;
- cancellation;
- unknown defects.

### Validation
- INF1–INF20;
- temporary/permanent test ownership;
- WP08 Python validation;
- `pip check`;
- global package cleanliness;
- .NET 268/268 or governed updated count;
- build/format/Gitleaks/docs/diff;
- schema/graph;
- residue.

### Mutation Accounting
- all repository/workstation/GitHub deltas.

### Final State
- #220 lifecycle;
- milestone #56;
- next authorized WP.

---

## 36. Completion Markers

On success end exactly:

`RELEASE 1.8 WP10 COMPLETE`

`PYTHON INTEGRATION INFRASTRUCTURE FOUNDATION: VERIFIED`

`NEXT AUTHORIZED WORK PACKAGE: WP11 — Python Integration Tests & Contract Verification — GitHub issue #221`

If the authoritative live title of #221 differs, use the exact live GitHub issue title without changing its scope.

Do not execute WP11 automatically.

If blocked end exactly:

`RELEASE 1.8 WP10 BLOCKED`

# Release 1.8 WP11 — Python Foundation & Interoperability Tests — Codex Authority

## 1. Mission

Execute Release 1.8 WP11 — **Python Foundation & Interoperability Tests** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#221`

Milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

Established Release 1.8 state:

- #211–#220: CLOSED / Done;
- #221: OPEN / Backlog;
- #222–#223: OPEN / Backlog;
- milestone #56: OPEN, 3 open / 10 closed;
- CPython 3.13.15 amd64;
- isolated project `.venv`;
- governed direct Python dependencies:
  - NumPy 2.5.1;
  - pandas 3.0.5;
  - scikit-learn 1.9.0;
  - Streamlit 1.61.1;
- WP08 scientific-stack validation: PASS;
- WP09 selected boundary:
  - local one-shot out-of-process Python;
  - versioned JSON-over-stdio;
  - deterministic project `.venv`;
- WP10 implemented:
  - technology-neutral Application invocation contracts;
  - Infrastructure-owned `.venv`/entrypoint resolution;
  - one-shot process adapter;
  - concurrent bounded stdout/stderr handling;
  - timeout/cancellation;
  - exit handling;
  - failure translation;
  - owned-process-only cleanup;
  - neutral standard-library Python health/echo endpoint outside `python/validation/`;
- WP10 removable probes passed and were deleted;
- permanent .NET baseline remains 268/268;
- schema remains v3.

WP11 converts the already-proven WP10 infrastructure behaviors into **permanent, deterministic, offline, repository-owned automated tests**.

WP11 must test the established foundation. It must not redesign WP09, broaden WP10, or implement Release 1.9 ML behavior.

---

## 2. Governing Principle

Permanent tests are executable engineering governance.

They must prove the architectural and behavioral invariants established by WP09 and WP10 without:

- depending on external network services;
- depending on real market data;
- introducing flaky timing assumptions;
- broadly terminating Python processes;
- leaking implementation mechanics into Domain/Application;
- becoming Release 1.9 ML tests.

Prefer narrow deterministic tests over large end-to-end scenarios.

---

## 3. Authoritative Inputs

Read completely before mutation:

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`
- `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
- relevant Application contracts created by WP10;
- relevant Infrastructure implementation created by WP10;
- WP10 Python protocol endpoint;
- existing Application test conventions;
- existing Infrastructure test conventions;
- architecture test conventions;
- `TESTING_STRATEGY.md`;
- error/failure/resilience/timeout/observability documentation;
- Python dependency governance;
- WP08 validation artifacts;
- GitHub issue #221;
- Project #2 Release 1.8 state.

Treat WP09 and WP10 as frozen predecessor authority unless an actual defect is discovered.

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
- WP09 interoperability record present;
- WP10 implementation present;
- WP10 Python endpoint present at its governed path;
- WP08 validation files unchanged;
- `.venv/` ignored;
- `requirements.txt` exact.

### GitHub

- #211–#220: CLOSED / Done;
- #221: OPEN / Backlog;
- #222–#223: OPEN / Backlog;
- milestone #56: OPEN, 3 open / 10 closed;
- Project membership: 13/13;
- duplicates: 0;
- WP10→WP11 dependency exists;
- fields/dependency chain remain authoritative.

### Python

Require:

- Python 3.13.15 x64;
- `.venv` isolated;
- exact four direct pins;
- `pip check`: PASS;
- WP08 validation: PASS;
- no global leakage of the four direct packages.

### .NET

Require permanent baseline:

- Domain.Tests: 11;
- Application.Tests: 119;
- Infrastructure.Tests: 125;
- Architecture.Tests: 13;
- total: 268;
- skipped: 0.

If legitimate committed WP10 changes altered a non-test baseline detail, reconcile it from live authority rather than assuming stale information.

Stop on unexplained drift.

---

## 5. Manifest Authority Gate

Before creating tests, read `RELEASE_1.8_FILE_MANIFEST.md` completely.

Determine exactly:

- which test project(s) WP11 owns;
- whether Python-side permanent test files are authorized;
- whether test-support fixtures/scripts are authorized;
- whether architecture tests are expected;
- exact or governed file locations.

Do not invent a new test project, Python test framework, test folder, fixture hierarchy, or helper architecture if the manifest does not authorize it.

If test ownership/path is ambiguous, stop with the exact ambiguity.

---

## 6. Test Ownership

Use existing test projects wherever the manifest assigns ownership.

Expected conceptual split:

### Application tests

Test technology-neutral contract semantics without starting Python.

Potential concerns:

- request/response model invariants;
- contract version semantics exposed at Application level;
- cancellation propagation at abstraction boundaries where appropriate;
- no Infrastructure/process details.

### Infrastructure tests

Own concrete integration behavior:

- `.venv` interpreter resolution;
- entrypoint resolution;
- process launch;
- JSON-over-stdio;
- stdout/stderr discipline;
- exit handling;
- malformed responses;
- timeout;
- cancellation;
- owned-process cleanup;
- failure translation.

### Architecture tests

Only if manifest/planning assigns permanent architecture enforcement to WP11.

Potential invariant:

- Domain/Application do not acquire forbidden Python/process implementation dependencies.

Do not add architecture tests merely to increase coverage.

---

## 7. No New Python Test Framework by Default

Do not introduce:

- pytest;
- unittest project architecture;
- tox;
- nox;
- hypothesis;
- another Python testing package/framework;

unless explicitly required by accepted planning and governed by the foundational selection rule.

The primary permanent interoperability coverage should live in the existing .NET test infrastructure if that matches the manifest.

The WP10 Python endpoint may be exercised as a subprocess fixture by Infrastructure tests.

WP08 executable validation scripts remain separate scientific-stack evidence.

---

## 8. Test Determinism

Every permanent WP11 test must be:

- offline;
- deterministic;
- bounded;
- repeatable;
- independent of provider availability;
- independent of market hours;
- independent of external credentials;
- independent of user-specific absolute paths;
- safe under repeated local execution.

Do not use arbitrary long sleeps as synchronization.

Prefer explicit process/stream completion signals and bounded cancellation/timeouts.

---

## 9. Interpreter Resolution Tests

Where manifest-owned, permanently verify:

1. repository-local `.venv` interpreter resolves correctly;
2. Windows resolution targets the governed `.venv` interpreter rather than WindowsApps/bare `python`;
3. missing interpreter produces the exact bounded governed failure;
4. invalid/unusable interpreter conditions are handled according to WP10 semantics;
5. caller cannot supply an arbitrary executable through the normal API.

Tests must not modify machine PATH.

If destructive/missing-path scenarios require isolation, use a temporary controlled filesystem fixture or injectable resolver seam already authorized by WP10 architecture.

Do not rename/delete the real `.venv` to force failures if a safer deterministic seam exists.

---

## 10. Entrypoint Resolution Tests

Permanently verify:

- governed endpoint resolves deterministically;
- missing endpoint maps correctly;
- arbitrary script execution is impossible through the normal API;
- path traversal is rejected/not representable;
- WP08 `python/validation/` scripts are not treated as production integration endpoints.

Do not mutate the real governed endpoint unnecessarily.

---

## 11. Successful Invocation Tests

Permanently verify the neutral WP10 protocol endpoint.

At minimum, where supported by the governed contract:

- health/handshake succeeds;
- bounded echo succeeds;
- request contract version is transmitted;
- response contract version is validated;
- structured payload round-trips correctly;
- UTF-8/JSON behavior is deterministic;
- process exits cleanly;
- no owned process remains.

Do not add ML/scientific behavior to these tests.

---

## 12. Stdout / Stderr Tests

Permanently prove the protocol discipline:

- stdout contains protocol data only;
- stderr diagnostics do not corrupt a valid stdout response;
- stderr is not parsed as success data;
- malformed/extra stdout protocol data fails closed where WP10 requires exactly one response;
- diagnostic content is handled safely.

Do not assert brittle full diagnostic strings unless they are public governed contracts.

Prefer failure categories/codes and stable semantics.

---

## 13. Contract Version Tests

Permanently verify:

- supported initial version succeeds;
- unsupported version is rejected deterministically;
- response version mismatch is rejected;
- unknown/breaking version does not silently downgrade;
- version handling remains independent of SQLite schema v3.

Do not introduce Release 1.9 contract versions.

---

## 14. Malformed Protocol Tests

Permanently verify bounded handling of representative invalid endpoint behavior, as allowed by the manifest/test fixture model:

- empty stdout;
- invalid JSON;
- incomplete JSON;
- structurally invalid response;
- unexpected multiple protocol frames/data;
- required-field absence;
- wrong response shape.

Tests must prove fail-closed behavior without leaking raw implementation exceptions above the governed boundary.

Use controlled test fixtures, not modifications to the production endpoint, unless planning explicitly says otherwise.

---

## 15. Exit-Code Tests

Permanently verify:

- zero exit + valid response;
- non-zero exit;
- non-zero exit + stderr;
- zero exit + malformed response;
- process start failure where deterministically simulatable.

Assert governed failure semantics rather than OS-specific incidental text.

---

## 16. Timeout Tests

Create permanent deterministic timeout coverage.

Require proof that:

- invocation exceeds a deliberately short bounded test timeout;
- timeout is classified according to WP09/WP10;
- only the owned child/process tree is terminated;
- cleanup completes;
- no owned process remains;
- unrelated Python processes are untouched.

The test fixture must be purpose-built and bounded.

Do not use production scientific scripts to manufacture a timeout.

Avoid flaky wall-clock thresholds. Assert generous upper bounds only where necessary.

---

## 17. Cancellation Tests

Create permanent deterministic cancellation coverage.

Require proof that:

- caller cancellation is observed;
- owned child process is terminated/cleaned;
- no orphan remains;
- cancellation maps/propagates according to existing policy;
- unrelated processes remain untouched.

Use synchronization that proves the child has started before cancellation where necessary.

Do not rely on arbitrary sleeps when a deterministic signal can be used.

---

## 18. Process Ownership Tests

This is mandatory.

Prove that cleanup logic targets only processes created by the integration adapter.

Do not kill:

- all `python.exe`;
- processes by executable name;
- VS Code Jedi/Pylance/helper processes;
- unrelated user Python workloads.

If a legitimate unrelated Python process exists during test execution, preserve it.

Do not create dangerous tests that intentionally target arbitrary external PIDs.

Prefer asserting tracked child identity/ownership and post-completion absence.

---

## 19. Deadlock / Concurrent I/O Tests

Where practical and manifest-owned, prove that bounded stdout and stderr production does not deadlock the adapter.

Use controlled output volumes sufficient to exercise concurrent redirected reads without creating excessive test cost.

Require:

- completion within bounded time;
- stdout response handled correctly;
- stderr captured according to policy;
- no orphan process.

Do not generate huge resource-consuming payloads.

---

## 20. Failure Translation Tests

Permanently verify mapping for governed failure classes, including as applicable:

- runtime/interpreter unavailable;
- entrypoint unavailable;
- launch failure;
- unsupported contract version;
- malformed response;
- Python-reported bounded failure;
- non-zero exit;
- timeout;
- cancellation.

Do not invent new error vocabulary unless an actual WP10 defect requires correction.

Unknown/unexpected defects must remain distinguishable according to existing policy.

---

## 21. Security-Oriented Tests

Where supported by the implementation seams and manifest, verify durable invariants such as:

- arbitrary executable cannot be selected;
- arbitrary script path cannot be selected;
- path traversal cannot escape governed entrypoint ownership;
- shell command construction is not part of the integration path;
- credentials are not required;
- protocol tests require no network.

Do not perform offensive/security exploitation.

Use architecture or unit-level assertions where safer than process-level tests.

---

## 22. Application Contract Tests

If WP10 added Application contracts and the manifest assigns WP11 Application coverage, add only tests that validate stable technology-neutral behavior.

Do not assert Infrastructure implementation details from Application tests.

Application tests must not:

- resolve `.venv`;
- start Python;
- inspect process IDs;
- know Python filesystem layout.

Maintain clean test-layer boundaries.

---

## 23. Architecture Tests

If authorized, enforce only long-lived architectural invariants.

Examples:

- Domain has no dependency on Infrastructure/Python/process implementation;
- Application does not reference Infrastructure;
- Application integration contracts do not depend on `System.Diagnostics.Process` or concrete Infrastructure adapter types.

Do not use fragile source-text searching if existing architecture-test mechanisms can prove dependencies structurally.

---

## 24. Test Fixtures and Helpers

Any permanent helper must:

- be manifest-authorized;
- have a clear test-only owner;
- not become production code;
- remain deterministic;
- not require external packages;
- clean temporary files/processes;
- avoid absolute user paths.

If small Python fixture endpoints are authorized, they must be clearly test-only and use the standard library.

Do not pollute the production Python endpoint with branches solely for tests when a test fixture is the cleaner boundary.

---

## 25. Production Defect Rule

WP11 is a testing work package, not an implementation expansion.

If a permanent test exposes an actual WP10 defect:

1. capture the failing evidence;
2. determine whether the fix is narrow and within the already-authorized WP10 semantics;
3. if clearly within existing behavior and file ownership, make the smallest corrective production change;
4. add the regression test;
5. report the production delta explicitly.

Stop instead of changing production code if:

- the required behavior was not governed by WP09/WP10;
- a contract change is needed;
- a new dependency is needed;
- architectural ownership changes;
- scope expands materially.

Do not weaken a test to preserve defective behavior.

---

## 26. WP08 Separation

Preserve:

`python/validation/scientific_stack_validation.py`

and:

`python/validation/streamlit_validation_app.py`

as Release 1.8 scientific-stack validation evidence.

WP11 may execute WP08 regression validation as a gate, but must not:

- convert those scripts into interoperability fixtures;
- move them;
- import production integration into them unnecessarily;
- collapse scientific validation and interoperability tests into one architecture.

---

## 27. Test Count Governance

Before mutation record exact test counts.

After WP11 report exact permanent additions by project.

Expected:

- Domain delta: 0 unless manifest explicitly requires otherwise;
- Application delta: only contract tests actually justified;
- Infrastructure delta: primary WP11 permanent coverage;
- Architecture delta: only if explicitly justified/authorized.

Do not target an arbitrary number of tests.

Coverage quality and invariant ownership matter more than count.

No tests may be skipped to obtain a passing result.

---

## 28. Repeated Reliability Validation

After all permanent WP11 tests pass once, run the relevant new interoperability test set repeatedly.

Minimum:

- 3 consecutive runs of the new WP11 interoperability test subset.

Require:

- 100% pass;
- 0 skipped;
- no orphan processes;
- no port listeners introduced;
- no temporary-file residue;
- no mutation of `.venv`;
- no unrelated-process termination.

Then run the complete canonical repository verification.

---

## 29. Python Regression Protection

Require after WP11:

- CPython 3.13.15;
- `.venv` isolated;
- NumPy 2.5.1;
- pandas 3.0.5;
- scikit-learn 1.9.0;
- Streamlit 1.61.1;
- `pip check`: PASS;
- WP08 validation: PASS;
- global direct-package cleanliness: PASS;
- `requirements.txt` unchanged;
- `.venv` ignored;
- no new Python dependency.

If WP11 introduces a Python test dependency, stop unless separately governed.

---

## 30. .NET Regression Protection

Run full canonical verification after permanent tests.

Require:

- all pre-WP11 tests pass;
- all new WP11 tests pass;
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
- dependency graph remains acyclic;
- package/project/reference delta matches authority exactly.

Report the new total rather than assuming 268 after permanent tests are added.

---

## 31. Residue Validation

At completion prove:

- no owned Python process remains;
- no temporary test fixture process remains;
- no new listener port remains;
- no temporary file/directory remains outside normal test output;
- `.venv` contents were not mutated;
- no global package mutation occurred;
- no provider/network call occurred;
- database/schema unchanged.

Do not treat legitimate VS Code Python helper processes as residue.

---

## 32. Explicit Non-Goals

Do not:

- redesign WP09;
- broaden WP10;
- implement Release 1.9;
- train models;
- predict;
- engineer ML features;
- use real market data;
- call Twelve Data or another provider;
- alter experiment evidence semantics;
- alter schema v3;
- introduce pytest/Jupyter/MLflow;
- add Python packages;
- add .NET packages unless an actual governed defect requires separate authority;
- create persistent Python service;
- create product Streamlit UI;
- execute WP12+;
- stage, commit, push, branch, PR, merge, tag, or release.

---

## 33. Validation Matrix

Report PASS/FAIL/NOT-APPLICABLE:

- TST1 — starting repository/GitHub/Python/.NET state reconciled;
- TST2 — test files/helpers match manifest ownership exactly;
- TST3 — Application tests remain technology-neutral;
- TST4 — deterministic `.venv` interpreter resolution permanently covered;
- TST5 — missing/invalid interpreter failure permanently covered;
- TST6 — deterministic entrypoint resolution and missing-entrypoint failure covered;
- TST7 — successful health/echo JSON round-trip covered;
- TST8 — stdout/stderr protocol separation covered;
- TST9 — supported/unsupported contract version behavior covered;
- TST10 — malformed/empty/invalid response behavior covered;
- TST11 — non-zero exit behavior covered;
- TST12 — timeout behavior and owned-process cleanup covered;
- TST13 — cancellation behavior and owned-process cleanup covered;
- TST14 — unrelated Python-process safety preserved;
- TST15 — concurrent stdout/stderr path is bounded and deadlock-safe;
- TST16 — governed failure translation permanently covered;
- TST17 — WP08 scientific validation remains separate and passes;
- TST18 — new interoperability test subset passes 3 consecutive runs with zero residue;
- TST19 — full repository verification passes with 0 skipped and no unauthorized dependency/schema drift;
- TST20 — no Release 1.9 or WP12+ behavior introduced.

---

## 34. Mutation Accounting

Report exact deltas for:

- Domain tests;
- Application tests;
- Infrastructure tests;
- Architecture tests;
- test fixtures/helpers;
- production Application;
- production Infrastructure;
- Python production endpoint;
- WP08 validation;
- Python test files;
- `requirements.txt`;
- `.venv` packages;
- machine-global packages;
- schema/database;
- .NET packages/projects/references;
- processes/ports/temp files;
- VS Code/machine Python;
- Git;
- GitHub.

Any production-code delta must be justified under the Production Defect Rule.

---

## 35. GitHub Lifecycle

Only after every WP11 gate passes:

1. transition #221 to In Progress if needed;
2. add concise evidence:
   - permanent test additions by project;
   - new total test count;
   - 3× repeated interoperability subset result;
   - timeout/cancellation/cleanup proof;
   - WP08 regression;
   - package/schema cleanliness;
3. close #221;
4. set #221 Project Status to Done.

Expected final state:

- #211–#221: CLOSED / Done;
- #222–#223: OPEN / Backlog;
- milestone #56: OPEN, 2 open / 11 closed;
- Project membership: 13/13;
- duplicates: 0;
- fields/dependency chain unchanged.

Do not transition #222 automatically.

---

## 36. Stop Conditions

Stop with:

`RELEASE 1.8 WP11 BLOCKED`

if:

- starting state is inconsistent;
- manifest test ownership/path is ambiguous;
- permanent testing requires a new ungoverned framework/dependency;
- safe failure fixtures cannot be created within authority;
- timeout/cancellation/process ownership cannot be tested deterministically;
- an actual defect requires a contract/architecture change;
- production correction would materially exceed WP10 semantics;
- canonical Python/.NET validation fails;
- tests are flaky across the required repeated runs;
- process/temp residue cannot be eliminated;
- WP12+/Release 1.9 behavior would be required.

Report exact blocker, partial state, and smallest corrective authority required.

---

## 37. Required Execution Report

Report:

### Starting State
- repository/branch/HEAD/origin;
- GitHub lifecycle;
- Python baseline;
- .NET test baseline;
- manifest test ownership.

### Permanent Tests
For each project/file:
- tests added;
- invariant/behavior covered;
- why the layer owns the test.

### Process/Protocol Coverage
- interpreter;
- entrypoint;
- success;
- stdout/stderr;
- versioning;
- malformed response;
- non-zero exit;
- timeout;
- cancellation;
- cleanup/deadlock/process ownership.

### Defects
- production defects discovered;
- smallest corrections, if any;
- regression tests;
- authority justification.

### Reliability
- 3 consecutive WP11 subset results;
- residue checks.

### Regression
- WP08 validation;
- `pip check`;
- exact pins/global cleanliness;
- complete .NET test counts by project;
- build/format/Gitleaks/docs/diff;
- schema/graph/dependencies.

### TST1–TST20
Report every gate.

### Mutation Accounting
Report all repository/workstation/GitHub deltas.

### Final State
- #221 lifecycle;
- milestone #56;
- next authorized WP.

---

## 38. Completion Markers

On success end exactly:

`RELEASE 1.8 WP11 COMPLETE`

`PYTHON FOUNDATION & INTEROPERABILITY TESTS: VERIFIED`

`NEXT AUTHORIZED WORK PACKAGE: WP12 — <exact live GitHub issue #222 title> — GitHub issue #222`

Use the exact authoritative live title of #222 in the final marker.

Do not execute WP12 automatically.

If blocked end exactly:

`RELEASE 1.8 WP11 BLOCKED`

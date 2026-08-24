# Release 1.8 WP13 — Full Validation, Integration & Acceptance — Codex Authority

## 1. Mission

Execute Release 1.8 WP13 — **Full Validation, Integration & Acceptance** for:

`samuel-santos-engineer/AIQuantTradingResearch`

GitHub issue:

`#223`

Milestone:

`#56 — Phase 4 - Release 1.8: Python & AI Engineering Foundation`

WP13 is the final Release 1.8 acceptance gate.

It must prove that the complete Release 1.8 foundation is internally consistent, reproducible, governed, integrated, regression-safe, and ready to close without introducing new feature scope.

WP13 is validation and acceptance work, not a new implementation work package.

---

## 2. Accepted Starting State

Expected Release 1.8 lifecycle:

- #211–#222: CLOSED / Done;
- #223: OPEN / Backlog;
- milestone #56: OPEN, 12 closed / 1 open;
- Project #2: 13 Release 1.8 items, no duplicates;
- WP12→WP13 dependency present.

Expected technical baseline:

- official CPython 3.13.15 amd64;
- machine runtime available deterministically;
- `.venv` project environment established, isolated, reproducible, and ignored;
- Python dependency governance established;
- exact governed direct dependencies:
  - NumPy 2.5.1;
  - pandas 3.0.5;
  - scikit-learn 1.9.0;
  - Streamlit 1.61.1;
- Microsoft Python VS Code extension governed;
- WP08 scientific validation:
  - 4/4 PASS;
- WP09 interoperability architecture:
  - local one-shot out-of-process Python;
  - versioned JSON-over-stdio;
  - `.venv` interpreter;
- WP10 interoperability infrastructure implemented;
- WP11 permanent interoperability coverage implemented;
- WP12 architecture/documentation/developer-environment alignment complete;
- permanent .NET baseline:
  - Domain.Tests: 11;
  - Application.Tests: 121;
  - Infrastructure.Tests: 136;
  - Architecture.Tests: 13;
  - total: 281/281;
  - skipped: 0;
- schema remains v3.

Reconcile these expectations against live repository/GitHub truth before relying on them.

---

## 3. Acceptance Philosophy

Release acceptance requires evidence, not inference.

WP13 must independently re-prove the Release 1.8 chain:

**machine runtime → project environment → governed dependencies → scientific validation → interoperability architecture → infrastructure execution → permanent tests → documentation/developer reproducibility → repository-wide regression safety**

Do not accept Release 1.8 merely because predecessor issues are closed.

Every material acceptance claim must have current evidence.

---

## 4. Authoritative Inputs

Read completely before mutation:

### Release authority

- `docs/roadmap/release-1.8/RELEASE_1.8_DEFINITION.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.8/RELEASE_1.8_FILE_MANIFEST.md`

### Engineering selections/governance

Read all Release 1.8 foundational selection records, including:

- Python runtime selection/compatibility;
- Python dependency governance;
- NumPy selection;
- pandas selection;
- scikit-learn selection;
- Streamlit selection;
- VS Code Python extension selection.

### Architecture/implementation

- `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
- WP10 Application contracts;
- WP10 Infrastructure implementation;
- WP10 Python protocol endpoint;
- WP11 permanent tests and test fixtures;
- WP08 validation scripts.

### Aligned documentation

Read WP12-aligned current-state documentation and the portable Python developer environment guide.

### Existing platform governance

Read relevant:

- dependency rules;
- boundary definitions;
- testing strategy;
- error handling/failure classification;
- timeout/resilience;
- logging/observability;
- dependency injection;
- configuration;
- security/secret-scanning guidance.

### GitHub

Read:

- issue #223;
- milestone #56;
- Project #2 Release 1.8 items/fields/dependencies.

---

## 5. Mandatory Repository Preflight

Before any mutation verify:

- repository identity;
- expected remote;
- branch `main`;
- local HEAD;
- `origin/main`;
- local HEAD equals `origin/main`;
- ahead/behind `0/0`;
- staged paths: 0;
- no unexplained tracked changes;
- no merge/rebase/cherry-pick in progress;
- no conflict markers;
- Release 1.8 governed artifacts present;
- `.venv/` ignored;
- `requirements.txt` present;
- WP08 scripts present;
- WP10 endpoint present;
- WP11 tests present;
- WP12 documentation present.

Record the exact accepted HEAD SHA.

Do not use a stale historical SHA gate if live accepted Release 1.8 work has legitimately advanced `main`.

Stop on unexplained repository drift.

---

## 6. Mandatory GitHub Preflight

Verify read-only before lifecycle mutation:

- #211–#222: CLOSED;
- Project Status for #211–#222: Done;
- #223: OPEN / Backlog;
- milestone #56: OPEN;
- milestone counts: 12 closed / 1 open;
- all 13 WP issues belong to #56;
- all 13 exist exactly once in Project #2;
- Release field: 1.8 for all;
- Priority: P1 for all;
- Areas match authoritative planning;
- dependency graph contains exactly the governed 12-edge WP01→...→WP13 chain;
- no duplicate Release 1.8 issue/project item;
- historical milestones remain unchanged;
- Release 1.9/2.0 planning remains untouched.

If GitHub API rate limiting prevents authoritative read-back, stop rather than infer.

---

## 7. Release 1.8 Scope Acceptance

Reconcile the delivered repository against the accepted Release 1.8 definition and execution plan.

For every WP01–WP12 outcome classify:

- DELIVERED;
- DELIVERED WITH DOCUMENTED BOUNDARY;
- NOT APPLICABLE;
- MISSING/BLOCKING.

No required outcome may remain MISSING/BLOCKING for acceptance.

Do not silently waive a planned outcome.

If planning and implementation disagree materially, stop.

---

## 8. File Manifest Acceptance

Reconcile `RELEASE_1.8_FILE_MANIFEST.md` against the live repository.

Verify:

- every required Release 1.8 file exists;
- no required file is missing;
- paths match authority;
- WP08 validation paths are correct;
- WP10 integration endpoint path is correct;
- selection records exist;
- developer environment documentation exists;
- no accidental alternate Python project structure was introduced;
- `.venv` is not tracked.

Identify any Release 1.8-created file not represented by the governed manifest where the manifest requires enumeration.

Do not mutate the manifest merely to hide unexplained drift.

---

## 9. Foundational Selection Record Acceptance

Enforce the standing rule:

> Every foundational external runtime, library, framework, or tool introduced into the platform must have an explicit engineering selection record describing why it was selected, alternatives considered, accepted trade-offs, version policy, architectural boundaries, and conditions that would cause the decision to be revisited.

Verify selection coverage for every foundational external technology introduced by Release 1.8.

At minimum verify governed records for:

- CPython;
- NumPy;
- pandas;
- scikit-learn;
- Streamlit;
- Microsoft Python VS Code extension.

If Release 1.8 introduced another foundational external technology, verify its record too.

Do not accept undocumented foundational technology.

---

## 10. Machine Python Acceptance

Verify current machine runtime:

- executable exists;
- official CPython provenance consistent with WP03 evidence;
- version: 3.13.15;
- architecture: x64;
- deterministic resolution;
- real Python precedes WindowsApps alias where applicable;
- `py` capability available;
- pip capability available;
- venv capability available;
- `PYTHONHOME` does not contaminate execution;
- `PYTHONPATH` does not contaminate execution.

Do not reinstall or modify machine Python during WP13.

If machine state drifted, report it.

---

## 11. Project `.venv` Acceptance

Verify:

- `.venv` exists;
- `.venv` is ignored by Git;
- interpreter is CPython 3.13.15;
- `sys.prefix != sys.base_prefix`;
- project invocation uses `.venv`;
- no committed machine-specific interpreter path;
- environment can be recreated from governed repository inputs.

Do not delete/recreate `.venv` yet unless the acceptance plan explicitly requires a reproducibility proof and it can be done safely.

If a clean recreation is performed, first account for legitimate editor-owned Python processes and never kill Python by process name.

---

## 12. Dependency Acceptance

Verify exact direct pins:

- NumPy 2.5.1;
- pandas 3.0.5;
- scikit-learn 1.9.0;
- Streamlit 1.61.1.

Require:

- `requirements.txt` matches governance;
- packages installed only in `.venv`;
- `pip check`: PASS;
- imports succeed;
- runtime versions equal pins;
- no machine-global installation of the four direct packages;
- no unexplained direct dependency;
- transitive dependencies are not incorrectly treated as direct architectural selections.

Do not upgrade dependencies in WP13.

---

## 13. Scientific Stack Acceptance

Run:

`python/validation/scientific_stack_validation.py`

and the governed Streamlit validation path.

Require all four WP08 validation capabilities to pass, including governed evidence for:

- NumPy;
- pandas;
- scikit-learn fit/predict;
- Streamlit AppTest.

Repeat the complete WP08 validation **3 consecutive times**.

Require:

- 4/4 each run;
- deterministic outcome;
- no network;
- no persistent server;
- no residual listener;
- no persistent process.

Do not convert WP08 into product behavior.

---

## 14. Interoperability Architecture Acceptance

Re-read `DOTNET_PYTHON_INTEROPERABILITY.md` and prove implementation conformity.

Verify the selected mechanism remains exactly:

- local;
- one-shot;
- out-of-process;
- governed `.venv` interpreter;
- versioned JSON-over-stdio;
- stdout protocol;
- stderr diagnostics.

Verify alternatives were not accidentally introduced:

- no Python.NET/embedded runtime;
- no HTTP/gRPC local service;
- no persistent Python daemon;
- no arbitrary script runner.

Stop on architectural drift.

---

## 15. Layer Boundary Acceptance

Verify:

### Domain

- no Python dependency;
- no process API dependency;
- no JSON transport implementation dependency;
- no interpreter path knowledge.

### Application

- contracts remain technology-neutral;
- no concrete Infrastructure dependency;
- no `.venv` path ownership;
- no process-launch implementation ownership.

### Infrastructure

Owns:

- interpreter resolution;
- entrypoint resolution;
- process launch;
- stdin/stdout/stderr;
- JSON transport implementation;
- timeout/cancellation mechanics;
- process cleanup;
- failure translation.

### Composition

Only governed registration/configuration.

Require dependency graph to remain acyclic.

---

## 16. Production Interoperability Acceptance

Exercise the neutral WP10 endpoint through the actual .NET integration path.

Require current proof for:

- health/handshake;
- UTF-8 structured echo;
- correct request version;
- correct response version;
- deterministic `.venv` resolution;
- deterministic endpoint resolution;
- clean process exit;
- no owned-process residue.

Do not invoke Release 1.9 ML behavior.

---

## 17. Permanent Interoperability Test Acceptance

Run the WP11 interoperability subset.

Require:

- 11/11 PASS;
- 0 skipped.

Repeat **3 consecutive times**.

Coverage must still prove:

- interpreter/entrypoint resolution;
- success path;
- UTF-8;
- stdout/stderr separation;
- versioning;
- malformed response;
- non-zero exit;
- concurrent I/O;
- timeout;
- cancellation;
- child cleanup;
- unrelated-Python-process safety.

If the live subset count legitimately differs, reconcile and report exact governed count.

No flaky run is acceptable.

---

## 18. Process Ownership Acceptance

Before and after process-based acceptance tests:

- identify relevant Python processes;
- distinguish platform-owned child processes from editor/user processes;
- preserve VS Code Python extension/Jedi/Pylance/helper processes;
- preserve unrelated user Python processes.

Require zero platform-owned orphan process after each bounded scenario.

Never:

- `Stop-Process -Name python`;
- `taskkill /IM python.exe`;
- terminate all Python processes;
- infer ownership solely from executable name.

A failure here blocks acceptance.

---

## 19. Timeout & Cancellation Acceptance

Use permanent WP11 tests as primary evidence.

Verify current behavior for:

- timeout;
- caller cancellation;
- child termination;
- process-tree cleanup where applicable;
- failure semantics;
- unrelated-process preservation.

Do not add broad destructive probes.

Require deterministic bounded execution.

---

## 20. Failure Semantics Acceptance

Verify permanent evidence remains for governed failures:

- interpreter unavailable;
- entrypoint unavailable;
- unsupported version;
- malformed response;
- Python-reported/controlled failure where applicable;
- non-zero exit;
- timeout;
- cancellation.

Ensure no parallel Python-specific failure taxonomy escaped architectural governance.

Unknown defects must remain distinguishable according to platform policy.

---

## 21. Security Acceptance

Verify:

- no arbitrary executable selection;
- no arbitrary script execution;
- no shell-string invocation in normal integration path;
- no path traversal through public contract;
- no credentials required;
- no secret in repository;
- no raw environment dump;
- no unrestricted payload logging;
- local integration requires no network;
- Gitleaks passes.

Do not perform offensive security testing.

---

## 22. Developer Reproducibility Acceptance

Validate the WP12 portable Python environment guide against current behavior.

Where safe, execute the documented non-destructive commands exactly as written.

Verify the documented workflow correctly covers:

1. Python prerequisite;
2. VS Code extension;
3. `.venv`;
4. dependency installation policy;
5. `pip check`;
6. scientific validation;
7. .NET verification;
8. global-install prohibition;
9. interpreter selection boundary.

Do not make undocumented workstation changes merely to satisfy the guide.

If a documented command is false or stale, acceptance is blocked unless a narrow documentation correction is clearly authorized by the defect rule below.

---

## 23. Documentation Truth Acceptance

Search current-state documentation for contradictions involving:

- Python absence/presence;
- runtime version;
- package versions;
- `.venv`;
- VS Code extension;
- integration mechanism;
- test totals;
- schema version;
- Release 1.8 completion state;
- Release 1.9 ML capabilities.

Historical records may preserve historical facts if clearly contextualized.

Current-state docs must not claim capabilities that do not exist.

---

## 24. Release 1.9 Boundary Acceptance

Explicitly prove Release 1.8 did **not** prematurely implement Release 1.9.

Search for unexplained production behavior related to:

- ML training pipelines;
- feature engineering pipelines;
- model persistence;
- inference services;
- model registry;
- ML experiment orchestration;
- explainability;
- ML observability;
- product Streamlit dashboard behavior.

Existing scientific validation and interoperability foundation do not count as premature Release 1.9 work.

Do not modify Release 1.9 objects.

---

## 25. Database / Schema Acceptance

Verify:

- schema remains v3;
- no Release 1.8 migration exists unless explicitly governed;
- existing prior-release database behavior remains intact;
- Python integration does not mutate SQLite as part of acceptance;
- `experiment_results` remains consistent with prior accepted foundation.

Do not migrate or rewrite databases.

---

## 26. Full .NET Acceptance

Run canonical full repository verification.

Require:

- Domain.Tests: 11/11;
- Application.Tests: 121/121;
- Infrastructure.Tests: 136/136;
- Architecture.Tests: 13/13;
- total: 281/281;
- skipped: 0.

Also require:

- restore: PASS;
- build: PASS;
- warnings/errors: 0/0;
- format: PASS.

If legitimate live counts differ, reconcile from repository truth and explain the delta.

Do not accept unexplained test-count drift.

---

## 27. Full Engineering Verification

Run all canonical repository engineering gates applicable to the current platform, including:

- restore;
- build;
- tests;
- formatting;
- Gitleaks;
- Markdown/link validation;
- terminal newline validation;
- trailing-whitespace validation;
- conflict-marker scan;
- `git diff --check`;
- `git diff --cached --check`;
- architecture/dependency checks;
- schema/version checks.

Use existing `eng/` verification scripts where authoritative.

Do not weaken gates to obtain success.

---

## 28. Repeated Stability Gate

After all individual gates pass, execute a final repeated stability sequence.

Minimum:

### Run 1
- WP08 scientific validation;
- WP11 interoperability subset;
- full .NET suite.

### Run 2
- WP08 scientific validation;
- WP11 interoperability subset.

### Run 3
- WP08 scientific validation;
- WP11 interoperability subset.

Require all runs PASS with:

- 0 skipped;
- no orphan platform Python processes;
- no new listeners;
- no temp residue;
- no `.venv` mutation;
- no global-package mutation;
- no schema/database mutation.

If execution cost is materially high, do not omit the three-run WP08/WP11 requirement; the full .NET suite is required at least once after final repository state.

---

## 29. Defect Rule

WP13 may discover defects but must not become a broad corrective implementation package.

If a defect is found:

### Narrow correction allowed only when all are true

- the expected behavior is already explicitly governed by WP01–WP12;
- the defect is unambiguous;
- the fix is small;
- no new architectural decision is required;
- no new foundational dependency/tool is required;
- no schema change is required;
- no Release 1.9 behavior is introduced;
- file ownership is clear.

Then:

1. record failing evidence;
2. make smallest correction;
3. add/update regression evidence if needed;
4. rerun all affected gates;
5. report exact correction.

### Stop instead when

- architecture must change;
- contract semantics must change materially;
- a new package/tool is required;
- planning authority is ambiguous;
- schema must change;
- Release 1.9 scope is required;
- correction is not clearly narrow.

End blocked and request the smallest corrective authority.

---

## 30. No Release Closure Before Evidence

Do not close #223 or milestone #56 until:

- every acceptance gate passes;
- no unexplained partial state remains;
- final repository validation passes;
- GitHub read-back is possible;
- Release 1.8 scope is fully reconciled.

GitHub issue closure is the consequence of acceptance, not evidence of acceptance.

---

## 31. Acceptance Matrix

Report PASS/FAIL/NOT-APPLICABLE for every gate:

- ACC1 — repository preflight;
- ACC2 — GitHub planning/lifecycle reconciliation;
- ACC3 — Release 1.8 scope reconciliation;
- ACC4 — file manifest reconciliation;
- ACC5 — foundational selection-record coverage;
- ACC6 — machine Python 3.13.15 verification;
- ACC7 — `.venv` isolation/reproducibility;
- ACC8 — exact dependency pins / `pip check` / global cleanliness;
- ACC9 — WP08 scientific validation 3×;
- ACC10 — WP09 architecture conformity;
- ACC11 — Domain/Application/Infrastructure boundary conformity;
- ACC12 — production health/echo interoperability proof;
- ACC13 — WP11 permanent interoperability subset 3×;
- ACC14 — timeout/cancellation/process ownership and zero-orphan proof;
- ACC15 — failure/security invariants;
- ACC16 — developer environment guide reproducibility;
- ACC17 — documentation truth/current-state consistency;
- ACC18 — schema v3 / database preservation / Release 1.9 boundary;
- ACC19 — canonical 281/281 .NET and engineering verification;
- ACC20 — final repeated stability and zero-residue acceptance.

All applicable ACC gates must PASS.

---

## 32. Mutation Accounting

Report exact deltas for:

- Domain production;
- Application production;
- Infrastructure production;
- Worker/composition;
- Python production endpoint;
- WP08 validation;
- permanent tests;
- test fixtures;
- documentation;
- selection records;
- Release 1.8 manifest;
- `requirements.txt`;
- `.venv`;
- machine/global Python;
- VS Code;
- schema/database;
- .NET packages/projects/references;
- processes/ports/temp files;
- Git;
- GitHub.

Expected repository content mutation in a clean WP13 acceptance run:

`0`

except a narrowly justified defect correction under Section 29.

---

## 33. Git Discipline

WP13 does not authorize:

- staging;
- committing;
- pushing;
- branch creation;
- PR creation;
- merging;
- tagging;
- GitHub Release creation.

Repository tracked state should remain unchanged during a clean acceptance run.

If existing legitimate uncommitted Release 1.8 work is present, stop unless the governing workflow explicitly authorizes acceptance from that state.

---

## 34. GitHub Completion Lifecycle

Only after ACC1–ACC20 pass:

1. transition #223 to In Progress if needed;
2. add a concise acceptance evidence comment containing:
   - accepted HEAD SHA;
   - Python 3.13.15;
   - exact four dependency pins;
   - WP08 3× result;
   - WP11 interoperability 3× result;
   - full .NET 281/281;
   - 0 skipped;
   - build 0 warnings/errors;
   - Gitleaks PASS;
   - schema v3 unchanged;
   - zero unauthorized production/package/reference mutation;
   - zero platform-owned process residue;
3. close #223;
4. set #223 Project Status to Done;
5. verify #223 CLOSED / Done;
6. verify milestone #56 now has 13 closed / 0 open;
7. close milestone #56;
8. verify milestone #56 CLOSED;
9. read back Project #2:
   - 13 Release 1.8 items;
   - all Done;
   - no duplicates;
   - Release/Priority/Area unchanged;
   - dependency chain unchanged.

Do not modify Release 1.9 or Release 2.0.

---

## 35. Release 1.8 Acceptance State

After successful GitHub lifecycle reconciliation, verify:

- WP01–WP13: CLOSED / Done;
- milestone #56: CLOSED;
- open issues in #56: 0;
- Project #2 Release 1.8 items: 13;
- Done: 13;
- duplicate items: 0;
- governed dependency chain: 12 edges;
- repository HEAD unchanged from accepted preflight unless a narrow defect correction was explicitly authorized;
- no staged paths;
- no unexplained tracked changes.

This is the authoritative Release 1.8 planning/execution acceptance state.

---

## 36. Release Artifact Boundary

WP13 does not authorize:

- Git tag;
- GitHub Release;
- release notes publication;
- PR;
- merge;
- Release 1.9 planning mutation.

If a later release-publication/closure workflow is desired, it requires separate authority.

Do not infer that closing milestone #56 authorizes a tag or GitHub Release.

---

## 37. Stop Conditions

Stop immediately with:

`RELEASE 1.8 WP13 BLOCKED`

if any of the following occurs:

- repository starting state cannot be reconciled;
- GitHub planning state is inconsistent;
- API rate limiting prevents required authoritative read-back;
- a Release 1.8 planned outcome is missing;
- file manifest cannot be reconciled;
- foundational selection record is missing;
- machine/runtime/dependency state violates governance;
- WP08 is non-deterministic;
- implementation diverges from WP09;
- permanent interoperability tests fail/flap;
- timeout/cancellation/process ownership cannot be proven;
- unrelated Python processes are affected;
- developer guide is materially false;
- documentation materially contradicts implementation;
- schema drift exists;
- premature Release 1.9 implementation exists;
- canonical .NET/engineering verification fails;
- residue remains;
- required correction exceeds Section 29;
- GitHub final read-back cannot prove completion.

Report:

- exact failed gate;
- evidence;
- partial mutations, if any;
- preserved state;
- smallest corrective authority required.

Do not continue through a failed mandatory acceptance gate.

---

## 38. Required Execution Report

Report:

### Accepted Baseline
- repository;
- branch;
- HEAD;
- origin;
- divergence;
- tracked/staged state.

### GitHub Starting State
- #211–#223;
- milestone #56;
- Project #2;
- dependency chain.

### Release Scope
For WP01–WP12:
- required outcome;
- delivered evidence;
- acceptance classification.

### Runtime & Environment
- Python;
- PATH/provenance;
- `.venv`;
- pip/venv;
- environment contamination checks.

### Dependencies
- exact pins;
- `pip check`;
- import/version proof;
- global cleanliness;
- selection records.

### Scientific Validation
- WP08 run 1;
- run 2;
- run 3;
- 4/4 each.

### Interoperability
- WP09 architecture;
- WP10 production proof;
- WP11 subset run 1;
- run 2;
- run 3;
- process ownership;
- timeout/cancellation;
- failures/security.

### Documentation & Developer Environment
- guide reproducibility;
- current-state truth;
- Release 1.9 boundary.

### Repository Regression
- test counts by project;
- 281/281;
- skipped;
- restore/build/format;
- Gitleaks;
- docs/diff;
- schema/dependency graph.

### ACC1–ACC20
Report each gate explicitly.

### Mutation Accounting
Report every repository/workstation/GitHub delta.

### Final GitHub State
- #223;
- milestone #56;
- Project #2;
- Release 1.9/2.0 unchanged.

---

## 39. Completion Markers

On full success end exactly:

`RELEASE 1.8 WP13 COMPLETE`

`RELEASE 1.8 FULL VALIDATION, INTEGRATION & ACCEPTANCE: PASS`

`RELEASE 1.8 ACCEPTED`

`MILESTONE #56: CLOSED — 13/13 WORK PACKAGES COMPLETE`

`NEXT AUTHORIZED ACTION: Define the separate Release 1.8 closure/publication boundary or begin Release 1.9 planning only under explicit new authority.`

Do not execute the next action automatically.

If blocked end exactly:

`RELEASE 1.8 WP13 BLOCKED`

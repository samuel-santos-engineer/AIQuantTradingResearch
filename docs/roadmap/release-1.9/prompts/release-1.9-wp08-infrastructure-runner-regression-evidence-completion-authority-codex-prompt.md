# Release 1.9 — WP08 Infrastructure Runner Regression-Evidence Completion Authority

## Model
Use **GPT-5.6 Terra**.

## Sole authority
This is a **narrow regression-evidence completion authority** for Release 1.9 WP08, canonical issue **#233**.

The WP08 implementation is now **frozen and technically passing at the focused level**.

This authority must not reopen or modify WP08 implementation unless new evidence proves an actual implementation defect.

Its purpose is only to:

1. obtain a trustworthy terminal Infrastructure test result;
2. complete the deferred full .NET regression;
3. complete the deferred Python regression;
4. verify final process/listener/handoff/database residue;
5. prepare the final #233 acceptance matrix.

This authority performs **zero GitHub lifecycle mutation**.

A later final lifecycle authority may close #233 only after this authority completes successfully.

WP09 / #234 remains Open / Backlog and unstarted.

---

# Frozen accepted WP08 implementation

Treat all of the following as binding predecessor evidence.

## Root cause/fix
- restart root cause: `RR-HANDOFF`;
- Worker startup already owns prior canonical-handoff cleanup via:
  `VisualizationReadModelFilePublisher.StartSession()`;
- no production change was warranted;
- harness incorrectly accepted stale Worker A handoff content as Worker B readiness;
- canonical R1 now requires changed handoff payload;
- RF-HANDOFF readiness additionally requires actual atomic replacement/last-write transition when byte-identical valid payloads are possible.

## Focused validation
- canonical shared-runtime R1: **3/3 passed**;
- RF-HANDOFF: passed;
- focused WP08: **18/18 passed**;
- build: **0 warnings / 0 errors**;
- Domain: **11/11 passed**;
- Application: **125/125 passed**;
- Architecture: **13/13 passed**.

## Environment
- Smart App Control local-development signing remediation is complete;
- test assemblies load successfully;
- no current owned Worker, Python, Streamlit, or stale testhost process was reported at the latest blocker.

## Frozen paths
Do not modify:
- `WP08LifecycleDemonstrationTests.cs`;
- `WindowsIsolatedProcessGroup.cs`;
- Worker production;
- Python probe;
- signing setup;
- Replay;
- WP05/WP06/WP07.

---

# Current blocker

Infrastructure/full-solution acceptance remains incomplete because the Infrastructure invocation did not emit a trustworthy terminal summary.

A previously stale owned `testhost` process was terminated, but a subsequent `--no-build` run also failed to produce the required summary.

No actual Infrastructure test failure has been established.

Therefore treat this as a **runner/result-evidence problem**, not an implementation regression, unless contrary evidence appears.

---

# Mutation scope

Repository mutation under this authority:

`ZERO`

No source/test/script/project/document/package changes.

Environment/test-runner actions only.

---

# Phase 0 — Entry verification

Read-only verify:

- branch;
- HEAD;
- origin/main;
- ahead/behind;
- current worktree;
- frozen WP08 diff remains unchanged;
- #233 Open / Backlog;
- #234 Open / Backlog;
- WP09 unstarted.

Do not require a clean worktree.

---

# Phase 1 — Test-runner process audit

Before Infrastructure execution:

Enumerate:
- `dotnet`;
- `testhost`;
- `vstest.console`;
- related test-runner child processes.

Identify only processes attributable to:
- current repository;
- stale prior AIQuantTradingResearch test invocations.

Use factual evidence where available:
- command line;
- parent PID;
- executable path;
- start time.

Do not terminate unrelated IDE/Visual Studio/testhost processes.

If an owned stale process is found:
- terminate only that PID;
- verify exit.

Record actions.

---

# Phase 2 — Build-output lock sanity

Verify the Infrastructure test output DLLs are not locked by stale owned processes.

If a lock is present:
- identify owner factually;
- terminate only owned stale process;
- verify lock released.

Do not broadly delete build output while active test processes exist.

---

# Phase 3 — Infrastructure invocation baseline

Run the canonical Infrastructure test project using the repository-governed command.

Require:
- process exit;
- terminal summary;
- passed/failed/skipped count;
- exit code.

Capture stdout/stderr to bounded console/log output if needed, but do not create persistent repository artifacts.

If terminal summary appears:
- proceed.

If not:
- continue to diagnostic phases.

---

# Phase 4 — One safe retry

Only one clean retry is authorized after runner diagnostics.

Before retry:
- ensure no owned stale testhost/dotnet/vstest remains;
- confirm build artifacts available;
- confirm signing state remains valid.

Retry using the minimal alternate invocation supported by `dotnet test`, such as:
- `--no-build` if build is already proven;
- explicit console verbosity/logger if needed to force terminal evidence.

Do not change tests.

No infinite retry loop.

---

# Phase 5 — Runner diagnostics

If summary is still absent, capture factual runner evidence:

- dotnet/testhost PID;
- parent/child relationship;
- command line;
- process exit state;
- stdout/stderr tail;
- elapsed time;
- whether TRX/result files were emitted in normal temporary result locations;
- whether the testhost actually exited;
- whether the outer `dotnet test` process exited;
- exit codes if available.

Do not install new tools/packages.

---

# Phase 6 — Result-file fallback evidence

If `dotnet test` fails to print a terminal summary but exits cleanly and produces a standard test result artifact (for example TRX), inspect that artifact as **secondary evidence**.

It must contain:
- exact total;
- passed;
- failed;
- skipped;
- outcome;
- completion state.

Use standard runner output only.

Do not invent a custom result parser if standard XML parsing is sufficient.

A TRX PASS may support diagnosis, but a normal terminal result remains preferred.

---

# Phase 7 — Hang classification

Classify any runner issue as:

## IR-OUTPUT
Tests complete but console terminal summary is suppressed/missing.

## IR-RESULT
Tests complete and result artifact exists, outer presentation is defective.

## IR-TESTHOST
Owned testhost does not exit.

## IR-DOTNET
Outer `dotnet test` does not exit despite child completion.

## IR-LOCK
Build/test output lock disrupts runner lifecycle.

## IR-TESTFAIL
A real Infrastructure test failure exists.

## IR-UNRESOLVED
Evidence remains insufficient.

Do not patch repository code.

---

# Phase 8 — Infrastructure acceptance gate

Infrastructure acceptance requires one of:

### Preferred
Normal terminal PASS summary with:
- 0 failed;
- exact passed;
- exact skipped.

### Exceptional runner-evidence path
Only if classified IR-OUTPUT or IR-RESULT:
- runner process exits successfully;
- authoritative standard result artifact proves all tests passed;
- no testhost residue;
- no ambiguity.

If IR-TESTHOST/IR-DOTNET/IR-LOCK persists after the one safe retry:
- BLOCK.

If IR-TESTFAIL:
- BLOCK and report exact test failure.

---

# Phase 9 — Full solution .NET regression

Only after Infrastructure acceptance.

Run:
- full governed solution regression.

Require:
- completion;
- exact totals;
- 0 failed;
- terminal summary or the same narrowly justified standard-result fallback if runner output defect is already proven systemic.

Also reconfirm:
- Application;
- Domain;
- Architecture counts if full solution does not expose per-project detail clearly.

Build remains:
- 0 warnings / 0 errors.

---

# Phase 10 — Python regressions

After .NET passes:

Run:
- WP05: **3/3**;
- WP06: **6/6**;
- WP07 semantic: **2/2**;
- WP07 presentation: **2/2**;
- Python compile/import;
- Streamlit **1.61.1**;
- `pip check`.

Require all pass.

No Python mutation.

---

# Phase 11 — Final process residue

Verify zero owned:
- Worker;
- Streamlit;
- Python probe;
- testhost;
- dotnet/vstest runner;
- listener.

Do not terminate unrelated processes.

---

# Phase 12 — Final filesystem residue

Verify exact governed WP08 final state:

- canonical handoff;
- temp handoff siblings;
- runtime directory;
- temporary DB;
- WAL/SHM/journal sidecars.

No cleanup outside harness-owned paths.

---

# Phase 13 — Signing/App Control sanity

Read-only verify:
- test assembly remains loadable;
- no new Code Integrity Event 3077 blocked the governed regression.

Do not change SAC/signing configuration.

---

# Phase 14 — Frozen-scope audit

Prove repository mutation under this authority is zero.

Verify frozen implementation remains unchanged:
- RR-HANDOFF fix;
- changed-payload readiness;
- atomic-replacement readiness;
- helper;
- Worker;
- probe;
- signing setup;
- WP05/WP06/WP07.

---

# Phase 15 — #233 technical acceptance matrix

Produce:

`#233 requirement → final evidence → result`

Include:
- P1/P2 bounded refresh;
- graceful cancellation;
- restart;
- shared-runtime handoff cleanup/readiness;
- Streamlit independence;
- listener ownership;
- real WP05→WP06→WP07 chain;
- process ownership;
- handoff residue;
- DB residue;
- focused WP08 18/18;
- Infrastructure regression;
- full .NET regression;
- Python regressions.

Every technical row must PASS.

---

# GitHub boundary

This authority performs:

`GITHUB MUTATIONS: ZERO`

Keep:
- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 unchanged.

Do not close #233.

A fresh final lifecycle completion authority is required after this technical evidence authority succeeds.

---

# Required completion report

## Frozen WP08 state
Confirm accepted fix and focused results.

## Runner audit
Owned processes and any cleanup.

## Infrastructure
Invocation, classification, final evidence, counts.

## Full .NET
Exact totals.

## Python
Exact results.

## Residue
Processes/listeners/handoff/DB.

## Signing/App Control
Read-only sanity.

## Scope
Repository mutations zero.

## #233 technical matrix
All rows.

## Lifecycle
#233/#234 unchanged.

## Mutation statements

`WP08 INFRASTRUCTURE RUNNER REGRESSION-EVIDENCE REPOSITORY MUTATIONS: ZERO`

`WP08 INFRASTRUCTURE RUNNER REGRESSION-EVIDENCE GITHUB MUTATIONS: ZERO`

## Next step

On success:

`WP08 TECHNICAL ACCEPTANCE COMPLETE — FINAL GITHUB LIFECYCLE AUTHORITY REQUIRED`

---

# Stop conditions

Stop if:
- Infrastructure has a real test failure;
- Infrastructure runner still hangs after one clean retry;
- authoritative result evidence is unavailable;
- full .NET regression fails;
- Python regression fails;
- residue unsafe;
- frozen implementation would need modification.

Do not patch under this authority.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 INFRASTRUCTURE RUNNER REGRESSION-EVIDENCE COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 INFRASTRUCTURE RUNNER REGRESSION-EVIDENCE COMPLETION BLOCKED`

Do not emit COMPLETE unless Infrastructure, full .NET, Python, residue, and frozen-scope evidence are all technically complete.

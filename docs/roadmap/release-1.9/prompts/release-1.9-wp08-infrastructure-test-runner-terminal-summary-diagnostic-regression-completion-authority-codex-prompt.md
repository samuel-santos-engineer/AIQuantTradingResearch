# Release 1.9 — WP08 Infrastructure Test-Runner Terminal-Summary Diagnostic + Regression Completion Authority

## Model

Use **GPT-5.6 Luna**.

## Sole authority

This is a narrow, verification-first authority for Release 1.9 WP08, issue
**#233**. Its only purpose is to diagnose and resolve the missing terminal
summary from the governed Infrastructure test run, then complete the deferred
non-mutating regression evidence for the already accepted WP08 restart fix.

It does not reopen the restart implementation, broaden WP08, close #233, or
begin WP09.

## Binding predecessor state

Treat the following as accepted and read-only:

- classification `R-RUNTIME`, exact culprit `RR-HANDOFF`;
- Worker-startup ownership of prior canonical-handoff cleanup;
- the test-only readiness correction in
  `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`;
- canonical shared-runtime R1 restart: Worker B exited `0` three consecutive
  times;
- one-factor controls: RF-HANDOFF, RF-DB, RF-RUNTIME, and R1F pass;
- focused WP08 lifecycle suite: 18/18 pass;
- solution build: 0 warnings / 0 errors;
- Domain 11/11, Application 125/125, Architecture 13/13 pass;
- the Windows helper, signing setup, Worker cancellation path, Python probe,
  Replay, and WP05–WP07 semantics remain frozen.

The outstanding evidence gap is only that the Infrastructure suite and the
parallel full-solution run started but did not emit an Infrastructure terminal
summary. A later process inventory found no owned `testhost`, Worker, Python,
or Streamlit process.

Lifecycle remains:

- #233 Open / Backlog;
- #234 Open / Backlog;
- milestone #58 Open;
- GitHub mutations: **ZERO**.

## Mutation boundary

Repository mutation is not authorized. Do not edit, create, delete, stage,
commit, restore, reset, or clean files. Do not modify GitHub.

The only allowed state-changing action is termination of a process that is
proven both stale and owned by this repository's prior/current governed
Infrastructure test invocation. Never use image-name-wide termination or act
on an unproven PID.

## Phase 0 — Read-only entry inventory

Record:

1. current branch, HEAD, upstream and ahead/behind;
2. staged, unstaged and untracked paths, preserving the existing dirty tree;
3. exact status of the WP08 lifecycle test file and frozen predecessor paths;
4. active `dotnet`, `vstest`, `testhost`, Worker, Python and Streamlit process
   candidates, including executable path, command line and parent when the
   operating system permits factual observation.

An inability to query a protected process is not evidence of ownership.

## Phase 1 — Terminal-summary diagnostic

Run the complete Infrastructure suite once using the canonical project
invocation and a console logger that can emit a terminal summary.

Capture only bounded factual evidence:

- command and exit code;
- elapsed time;
- test-runner stdout/stderr terminal output;
- process state immediately after return;
- whether a terminal passed/failed/skipped summary exists.

If it emits a terminal summary with zero failures, proceed without cleanup.

If the command hangs, ends without a terminal summary, or a build-output lock
is proved, identify the exact stale owned process. The proof must connect the
PID to this repository path or governed test invocation. Terminate at most
that exact proven stale PID, verify it exited, and rerun the Infrastructure
suite exactly once with `--no-build` only when the build is already proven.

Do not terminate IDE, unrelated testhost, or unrelated Python/Streamlit
processes. Do not loop indefinitely.

If the retry lacks a terminal summary, stop as blocked with its exact output,
exit code, process evidence and reason that ownership/remediation is not
safe or sufficient.

## Phase 2 — Regression completion

Only after Infrastructure has a terminal zero-failure summary, complete these
read-only gates:

1. full governed solution test run with terminal summary and zero failures;
2. WP05 Python 3/3;
3. WP06 Python 6/6;
4. WP07 semantic Python 2/2;
5. WP07 presentation Python 2/2;
6. governed `.venv` Streamlit version `1.61.1`;
7. governed `.venv` `pip check` clean;
8. final owned-process/listener residue inventory.

Use existing interpreter-qualified commands only. Do not install, upgrade,
remove, configure, or create Python environments/packages.

If any test or validation fails, stop with exact evidence. No repair is
authorized under this authority.

## Phase 3 — Preservation and lifecycle read-back

Prove:

- no repository mutation occurred;
- no helper/Worker/Python/signing/Replay/WP05–WP07 path changed;
- no owned Worker, Streamlit, probe, testhost, or listener residue remains;
- #233 and #234 remain Open / Backlog by preserving accepted predecessor
  lifecycle state; GitHub mutations are zero;
- WP09 remains unstarted.

Do not query or mutate GitHub unless a read-only lifecycle check is already
available without changing state. A network or authentication failure must not
be treated as permission to modify GitHub.

## Completion gate

Complete only when:

1. Infrastructure has a terminal zero-failure summary;
2. full solution has a terminal zero-failure summary;
3. all specified Python gates pass with exact counts and versions;
4. no unsafe or unexplained owned residue remains;
5. repository mutations are zero; and
6. GitHub mutations are zero.

## Required completion report

Report:

- Infrastructure command(s), terminal summary and any owned-PID action;
- full .NET terminal summary;
- WP05/WP06/WP07 Python results, Streamlit version and `pip check`;
- process/listener residue result;
- repository and GitHub mutation accounting;
- #233/#234/WP09 preservation.

Include exactly one of:

`WP08 INFRASTRUCTURE TERMINAL-SUMMARY DIAGNOSTIC GITHUB MUTATIONS: ZERO`

`WP08 INFRASTRUCTURE TERMINAL-SUMMARY DIAGNOSTIC REPOSITORY MUTATIONS: ZERO`

## Stop conditions

Stop immediately if:

- Infrastructure still lacks a terminal summary after one proven-safe retry;
- an owned stale process cannot be identified safely;
- a full .NET or Python gate fails;
- a repair, test change, production change, package change, GitHub mutation,
  or WP09 work appears necessary.

## Terminal markers

Success:

`RELEASE 1.9 WP08 INFRASTRUCTURE TERMINAL-SUMMARY DIAGNOSTIC AND REGRESSION COMPLETION COMPLETE`

Blocked:

`RELEASE 1.9 WP08 INFRASTRUCTURE TERMINAL-SUMMARY DIAGNOSTIC AND REGRESSION COMPLETION BLOCKED`

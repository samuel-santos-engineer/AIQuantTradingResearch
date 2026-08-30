# Release 1.10 — Infrastructure Full-Suite Runner/Hang Diagnostic & Recovery Reconciliation Authority

## Model assignment

- **GPT-5.6 Luna** — PRIMARY contract, policy, diagnostic reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — implementation, validation execution, approved Git/GitHub mutations; NOT selected for this authority.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

**Selected execution model: GPT-5.6 Luna.**

---

# Purpose

Diagnose and reconcile the Release 1.10 full Infrastructure test-suite non-termination that blocked publication after the local Windows App Control execution environment was restored.

This authority must determine whether the hang is:

1. a product defect;
2. a test defect;
3. a test-runner/process-lifecycle defect;
4. an environment/security-control interaction;
5. a deterministic cleanup/teardown issue;
6. another exact governed cause.

It must then freeze a deterministic recovery and validation procedure that GPT-5.6 Terra can execute without making product-policy choices.

This authority is diagnostic/reconciliation-only.

---

# Accepted entry state

Treat as accepted unless authoritative inspection directly contradicts it:

- canonical base and `origin/main`:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- repaired canonical publication manifest remains intact at exactly 103 candidate paths;
- candidate content was not edited;
- staging is empty;
- no commit, push, PR, issue, Project, milestone, tag, or release mutation occurred in the blocked publication attempt;
- focused suites passed:
  - Application 5/5
  - Infrastructure 4/4
  - Architecture 6/6
- Domain full suite passed:
  - 11/11
- Application full suite passed:
  - 136/136
- initial full Infrastructure/Architecture failures were Windows App Control `0x800711C7`;
- documented local execution environment was restored by signing only first-party Debug DLL outputs using the local `AIQuantTradingDev` certificate;
- subsequent full Infrastructure run did not terminate;
- six owned test-runner processes were left behind;
- exactly those six owned processes were terminated;
- no unrelated process was touched.

Emit:

`RELEASE 1.10 INFRASTRUCTURE HANG RECONCILIATION ENTRY: PASS`

---

# Mutation boundary

Allowed repository mutations:

- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md` only if required to persist the recovery/validation procedure.
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md` only if required to classify a newly-created diagnostic authority artifact for publication exclusion; avoid otherwise.

Avoid changing:
- `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`

Forbidden repository content mutations:

- production source;
- tests;
- WP07 docs;
- package/project files;
- schema/migrations;
- signing configuration;
- authority prompt content other than planning classification if necessary.

Forbidden Git/GitHub publication mutations:

- staging;
- commit;
- branch movement;
- push;
- PR create/update;
- issue/Project/milestone mutation;
- tag/version;
- GitHub Release.

Allowed environment actions:

- read-only inspection;
- running governed tests/diagnostic commands;
- restoring the already-documented local signing environment;
- terminating only processes proven to be owned by this diagnostic/test invocation;
- collecting process trees, test logs, dump/diagnostic output where safe and repository-external;
- deleting only repository-external temporary diagnostic artifacts created by this authority if needed.

Git mutations: ZERO.
GitHub mutations: ZERO.

---

# Phase 1 — Re-establish authoritative preconditions

Verify:

1. `HEAD` and `origin/main` remain:
   `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
2. staging is empty;
3. repaired manifest candidate list remains exactly 103 unique valid paths;
4. no candidate content changed;
5. no merge/rebase/cherry-pick is in progress;
6. no stale owned test process from the prior blocked attempt remains;
7. any first-party Debug DLL signing state matches the documented local developer setup and is environment-only.

Emit:

`RELEASE 1.10 INFRASTRUCTURE HANG PRECONDITIONS: PASS`

---

# Phase 2 — Reproduce with bounded diagnostics

Run the full Infrastructure suite under a bounded diagnostic procedure.

Requirements:

- use the repository-governed test command unless a diagnostic wrapper is necessary;
- capture start time, command line, PID/process tree, and terminal state;
- do not rely on an unbounded wait;
- define a diagnostic timeout/window that is long enough to distinguish slow from hung based on historical suite duration/repository evidence;
- if the suite terminates, record exact result and duration;
- if it does not terminate, record:
  - last observed test/output;
  - active process tree;
  - child processes;
  - CPU/activity state where available;
  - file/socket/listener state where relevant;
  - whether testhost/dotnet/VSTest remains active;
  - whether any Python/Worker/Streamlit child remains;
  - exact process ownership evidence.

Do not kill anything until process ownership is established.

Emit one:

`RELEASE 1.10 INFRASTRUCTURE FULL-SUITE REPRODUCTION: TERMINAL`

or

`RELEASE 1.10 INFRASTRUCTURE FULL-SUITE REPRODUCTION: HANG CONFIRMED`

---

# Phase 3 — Isolate the hang boundary

If the full suite hangs, isolate deterministically.

Use progressively narrower partitions without editing tests.

Preferred approaches, where supported:

- test class filters;
- namespace filters;
- category/trait filters;
- binary search over deterministic test subsets;
- single-test repetition;
- test ordering investigation;
- runner verbosity/logging;
- testhost process-tree observation.

Freeze:

- smallest reproducible test set;
- whether a single test hangs;
- whether hang depends on prior tests/order;
- whether hang occurs only in full-suite aggregation;
- whether parallelism contributes;
- whether process cleanup/teardown is the boundary;
- whether Windows App Control/signing state changes behavior;
- whether child-process lifecycle is involved.

No speculative attribution.

Emit:

`RELEASE 1.10 INFRASTRUCTURE HANG BOUNDARY: ISOLATED`

---

# Phase 4 — Classify ownership and cause

Classify the cause into exactly one primary category:

A. Product runtime defect.
B. Test implementation defect.
C. Test-runner/host lifecycle defect.
D. Environment/App Control/signing interaction.
E. External toolchain defect.
F. Mixed/other exact cause.

For the chosen category provide:

- concrete evidence;
- counter-evidence for adjacent categories;
- whether candidate content is implicated;
- whether a repository code/test change is required;
- whether environment-only recovery is sufficient;
- whether this authority can safely hand back to Terra without implementation.

If a product/test code change is required, this authority must BLOCK and request a separate Terra implementation authority with exact paths and acceptance criteria.

Emit:

`RELEASE 1.10 INFRASTRUCTURE HANG ROOT-CAUSE CLASSIFICATION: FROZEN`

---

# Phase 5 — Owned-process cleanup contract

Freeze exact process ownership/cleanup rules.

A process may be terminated only when all of the following are proven:

- it is descended from or directly created by the diagnostic/test invocation;
- PID/process relationship is captured;
- command line/executable identity matches the owned test execution;
- it is not a pre-existing unrelated user/system process.

Freeze exact cleanup sequence, including:

- graceful wait/termination first where possible;
- force termination only for confirmed owned residue;
- post-cleanup verification that no owned testhost/dotnet/Python/Worker/Streamlit/listener residue remains;
- never terminate unrelated processes.

Emit:

`RELEASE 1.10 INFRASTRUCTURE OWNED-PROCESS CLEANUP CONTRACT: FROZEN`

---

# Phase 6 — Recovery procedure

Freeze the exact recovery procedure Terra must follow.

It must state literally, as applicable:

1. signing/environment prerequisite;
2. exact test command;
3. exact timeout/bounded observation policy;
4. exact cleanup steps for owned residue;
5. exact retry count, if any;
6. exact condition under which a retry is legitimate;
7. exact condition under which repeated non-termination is a hard BLOCK;
8. exact logs/evidence Terra must retain/report;
9. whether test-runner settings/parallelism may be changed;
10. whether any such setting change is diagnostic-only or an accepted validation method.

Do not authorize arbitrary retries until green.

Emit:

`RELEASE 1.10 INFRASTRUCTURE HANG RECOVERY PROCEDURE: FROZEN`

---

# Phase 7 — Publication validation consequence

Define what counts as a valid full Infrastructure result for publication.

Required default:

- the full Infrastructure suite must reach a terminal result;
- success requires all expected Infrastructure tests passing;
- cleanup after a terminal PASS may remove only owned residue;
- a timeout/hang is not equivalent to PASS;
- partial/focused passes do not substitute for full-suite terminal PASS.

Determine whether full Architecture must be rerun after Infrastructure recovery due to prior App Control failure; if yes, make this explicit.

Preserve all previously frozen publication validation requirements unless directly affected by this diagnostic.

Emit:

`RELEASE 1.10 INFRASTRUCTURE PUBLICATION VALIDATION GATE: FROZEN`

---

# Phase 8 — Planning reconciliation

If necessary, update only the Release 1.10 execution plan to persist:

- root-cause classification;
- exact recovery procedure;
- exact owned-process cleanup rule;
- exact Infrastructure terminal-pass requirement;
- any Architecture rerun consequence;
- exact Terra resumption handoff.

If this diagnostic authority's two prompt files appear in the local worktree, classify them consistently with existing publication-control policy. Do not silently alter the 103-path candidate.

Emit:

`RELEASE 1.10 INFRASTRUCTURE HANG PLANNING RECONCILIATION: PASS`

---

# Phase 9 — Terra recovery simulation

Simulate the next Terra publication validation run with ZERO Git/GitHub mutation.

Required question:

> Can Terra execute the Infrastructure recovery and obtain either a terminal PASS or a deterministic BLOCK without choosing diagnostic or content policy?

Required answer:

**YES**

Also prove:

- no ambiguity about timeout;
- no ambiguity about retry count;
- no ambiguity about process ownership;
- no ambiguity about cleanup;
- no ambiguity about whether a code/test change is allowed;
- no ambiguity about the terminal acceptance gate.

Emit:

`RELEASE 1.10 INFRASTRUCTURE HANG RECOVERY SIMULATION: PASS — TERRA-READY`

---

# Phase 10 — Terra publication resumption handoff

If no repository implementation change is required, produce an exact handoff to:

**Release 1.10 — Git Candidate Publication & Pull Request Authority — GPT-5.6 Terra**

State literally:

- selected model: GPT-5.6 Terra;
- canonical base/parent:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- repaired manifest remains the sole 103-path staging authority;
- current candidate content remains unchanged;
- exact Infrastructure recovery procedure;
- exact terminal PASS requirement;
- whether Architecture must rerun;
- full remaining validation sequence;
- no content edits unless a separate authority is created;
- no merge;
- no milestone close;
- no tag/version;
- no GitHub Release.

If a repository implementation/test change is required, do NOT hand back to publication Terra. Instead freeze the minimum exact implementation authority required.

Emit one:

`RELEASE 1.10 INFRASTRUCTURE HANG → TERRA PUBLICATION RESUMPTION HANDOFF: PASS`

or

`RELEASE 1.10 INFRASTRUCTURE HANG → IMPLEMENTATION AUTHORITY REQUIRED`

---

# Phase 11 — Mutation audit

Report exact actions and mutations.

Expected repository mutations:

- execution plan only if needed;
- manifest only if diagnostic-control artifact classification is required;
- production/tests/packages/schema/signing config: ZERO.

Expected environment actions:

- test execution;
- possible documented local first-party Debug signing;
- owned-process cleanup only.

Git: ZERO.
GitHub: ZERO.

Reverify:

- staging empty;
- no commit/push/PR mutation;
- #242–#249 Closed/Done;
- milestone #59 Open, 0 open / 8 closed;
- no tag/release mutation.

Emit:

`RELEASE 1.10 INFRASTRUCTURE HANG RECONCILIATION MUTATION AUDIT: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

---

# Required success markers

`RELEASE 1.10 INFRASTRUCTURE HANG RECONCILIATION ENTRY: PASS`

`RELEASE 1.10 INFRASTRUCTURE HANG PRECONDITIONS: PASS`

`RELEASE 1.10 INFRASTRUCTURE FULL-SUITE REPRODUCTION: TERMINAL`
or
`RELEASE 1.10 INFRASTRUCTURE FULL-SUITE REPRODUCTION: HANG CONFIRMED`

`RELEASE 1.10 INFRASTRUCTURE HANG BOUNDARY: ISOLATED`

`RELEASE 1.10 INFRASTRUCTURE HANG ROOT-CAUSE CLASSIFICATION: FROZEN`

`RELEASE 1.10 INFRASTRUCTURE OWNED-PROCESS CLEANUP CONTRACT: FROZEN`

`RELEASE 1.10 INFRASTRUCTURE HANG RECOVERY PROCEDURE: FROZEN`

`RELEASE 1.10 INFRASTRUCTURE PUBLICATION VALIDATION GATE: FROZEN`

`RELEASE 1.10 INFRASTRUCTURE HANG PLANNING RECONCILIATION: PASS`

`RELEASE 1.10 INFRASTRUCTURE HANG RECOVERY SIMULATION: PASS — TERRA-READY`

and exactly one:
`RELEASE 1.10 INFRASTRUCTURE HANG → TERRA PUBLICATION RESUMPTION HANDOFF: PASS`
or
`RELEASE 1.10 INFRASTRUCTURE HANG → IMPLEMENTATION AUTHORITY REQUIRED`

`RELEASE 1.10 INFRASTRUCTURE HANG RECONCILIATION MUTATION AUDIT: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Exact success terminal

`RELEASE 1.10 — INFRASTRUCTURE FULL-SUITE RUNNER/HANG DIAGNOSTIC & RECOVERY RECONCILIATION AUTHORITY COMPLETE`

---

# Block conditions

BLOCK if:

- base/candidate governance is no longer intact;
- process ownership cannot be established safely;
- reproducing the issue would require unsafe termination of unrelated processes;
- root cause remains materially ambiguous after bounded isolation;
- a product/test change is required but exact implementation ownership cannot be frozen;
- publication acceptance would require treating a hang/timeout as PASS;
- environment restoration requires unauthorized repository/signing configuration mutation;
- Git/GitHub publication mutation would be required.

On BLOCK:

- preserve staging empty;
- do not commit/push/create PR;
- do not merge/tag/close milestone/publish release;
- clean only owned residue;
- report the minimum unresolved diagnostic/governance choice.

# Exact blocked terminal

`RELEASE 1.10 — INFRASTRUCTURE FULL-SUITE RUNNER/HANG DIAGNOSTIC & RECOVERY RECONCILIATION AUTHORITY BLOCKED`

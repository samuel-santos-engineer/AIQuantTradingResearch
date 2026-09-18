# Codex Authority — Release 1.12 WP04 W5 Execution-Budget Diagnostic

## Authority

**Selected execution model: GPT-5.6 Terra**

This is a narrow **diagnostic-only authority** for Release 1.12 WP04 W5.

It exists because repeated fresh-session attempts have ended before the governed W5 execution could begin, including after the available execution budget was increased.

This authority does **not** grant W5 acceptance, does **not** modify the W5 P01–P20 contract, and does **not** authorize production, Azure, GitHub, Docker/GHCR, publication, or lifecycle mutations.

## Canonical starting state

```text
W1–W4 = PASS — carry forward
W5 = NOT_GRANTED
W6–W8 = NOT_RUN

Latest fresh W5 execution = NOT_COMPLETED
Latest fresh sandbox = NOT_CREATED
Latest fresh RunId = NOT_ALLOCATED
Latest wrapper execution = NONE

Additional production mutations = 0
Additional external mutations = 0
PUBLICATION BLOCKER = UNRESOLVED
```

The governing W5 execution authority remains:

```text
release-1.12-wp04-codex-fresh-session-execute-w5-end-to-end.md
```

Do not execute that full authority during this diagnostic.

# Objective

Determine why Codex repeatedly reaches a session stop before creating the fresh W5 sandbox and invoking the wrapper.

Classify the blocker using observable evidence rather than assumption.

The diagnostic must distinguish at least:

```text
D1 SESSION_EXECUTION_BUDGET_LIMIT
D2 TOOL_OR_COMMAND_TIMEOUT_LIMIT
D3 EXCESSIVE_PREEXECUTION_ORCHESTRATION
D4 INTERACTIVE/SESSION_CONTROL_STOP
D5 ENVIRONMENT_OR_PROCESS_LAUNCH_LIMIT
D6 OTHER_OBSERVED_BLOCKER
D7 NO_TECHNICAL_BLOCKER_OBSERVED
```

Do not infer a production defect merely because W5 has not executed.

# Read-only inspection

Inspect the current repository and relevant W5 authority/harness context needed to understand the execution path.

Allowed:

- read files;
- inspect Git status/diff without mutation;
- inspect existing local scripts and authorities;
- inspect PowerShell version;
- parser-check existing disposable/harness text if available;
- measure or estimate the number and shape of commands required before wrapper invocation;
- identify expensive/redundant pre-wrapper work;
- inspect previous local diagnostic evidence if already present.

Forbidden:

- creating the fresh governed W5 sandbox;
- allocating a W5 RunId;
- invoking the governed W5 wrapper;
- modifying tracked or untracked project files;
- staging/commit/push;
- Azure calls that mutate or qualify state;
- Docker/GHCR mutation;
- GitHub mutation;
- Twelve Data configuration;
- changing production source/helper code;
- weakening or aggregating P01–P20.

If temporary diagnostic data is unavoidable, keep it outside the repository, sanitized, minimal, and delete it before completion. It must not resemble or be counted as a governed W5 scenario.

# Required diagnostic questions

Answer each explicitly.

## Q1 — Where does execution stop?

Identify the latest observable step reached before each recent no-execution stop, as far as evidence permits.

Determine whether the stop occurred:

```text
before any shell execution
during authority interpretation/planning
during harness construction
during parser/environment preparation
during process launch
because of a hard session/tool timeout
for another observable reason
```

Do not invent missing telemetry.

## Q2 — Is the increased budget actually reaching the execution phase?

Report whether there is evidence that the larger budget changed:

- available execution duration;
- number of commands possible;
- tool timeout;
- child-process duration;
- or none of these can be proven.

Separate fact from inference.

## Q3 — What work occurs before the first governed W5 action?

Enumerate the actual pre-wrapper execution path.

Identify redundant work that is repeated every fresh session.

Do not optimize it yet; diagnose only.

## Q4 — Can the existing current approach finish in one session?

Classify:

```text
PROVEN_YES
PROVEN_NO
PLAUSIBLE_BUT_UNPROVEN
INSUFFICIENT_EVIDENCE
```

Support the classification with observed execution constraints.

Do not grant acceptance or change governance.

## Q5 — Would a durable runner materially reduce session-bound orchestration?

Assess only architecture/execution mechanics.

Classify:

```text
MATERIALLY_REDUCES_SESSION_WORK
DOES_NOT_MATERIALLY_REDUCE_SESSION_WORK
INSUFFICIENT_EVIDENCE
```

If it would help, identify the minimum runner responsibility needed.

The runner must never duplicate production archive/extraction policy and must preserve independent P01–P20 accounting.

Do not create the runner under this authority.

## Q6 — Is any other narrow correction preferable?

Identify any observed lower-mutation solution, such as:

- execute an already-existing complete harness directly;
- eliminate repeated authority reconstruction;
- invoke one existing script rather than many interactive commands;
- adjust only invocation mechanics;
- another evidence-backed option.

Do not implement it.

# Required invariants

Before completing, prove:

```text
W5 governed wrapper invoked = NO
New W5 RunId allocated = NO
Fresh governed W5 sandbox created = NO
Tracked project mutations = 0
Staged paths = 0
Production mutations = 0
External mutations = 0
W5 acceptance = NOT_GRANTED
W6–W8 = NOT_RUN
```

If the repository already contains authorized pre-existing modified paths, report them as pre-existing and prove this authority introduced zero additional changes. Do not clean or alter them.

# Required final report

Emit a concise evidence-backed report containing:

```text
RELEASE 1.12 WP04 — W5 EXECUTION-BUDGET DIAGNOSTIC: COMPLETE

PRIMARY CLASSIFICATION: <D1-D7>
SECONDARY CLASSIFICATION: <D1-D7 or NONE>

OBSERVED STOP BOUNDARY: <specific evidence-backed boundary>
INCREASED BUDGET EFFECT: <proven observation or UNKNOWN>

CURRENT APPROACH ONE-SESSION COMPLETION:
<PROVEN_YES | PROVEN_NO | PLAUSIBLE_BUT_UNPROVEN | INSUFFICIENT_EVIDENCE>

DURABLE RUNNER EFFECT:
<MATERIALLY_REDUCES_SESSION_WORK | DOES_NOT_MATERIALLY_REDUCE_SESSION_WORK | INSUFFICIENT_EVIDENCE>

MINIMUM NEXT-AUTHORITY RECOMMENDATION:
<one narrow evidence-backed execution architecture; no implementation>

W5 GOVERNED WRAPPER INVOKED: NO
NEW W5 RUNID ALLOCATED: NO
FRESH GOVERNED W5 SANDBOX CREATED: NO
TRACKED PROJECT MUTATIONS INTRODUCED: 0
STAGED PATHS: 0
PRODUCTION MUTATIONS: 0
EXTERNAL MUTATIONS: 0

W5 GOVERNED ACCEPTANCE: NOT_GRANTED
W6/W7/W8: NOT_RUN
PUBLICATION BLOCKER: UNRESOLVED
```

Also provide the evidence supporting the primary classification and the minimum next-authority recommendation.

# Stop condition

**STOP AFTER THE DIAGNOSTIC.**

Do not execute W5.
Do not create a durable runner.
Do not remediate production code.
Do not publish anything.
Do not mutate Git/GitHub/Azure/Docker/GHCR.

The purpose of this authority is to obtain enough evidence to choose the next execution architecture without weakening W5 acceptance.

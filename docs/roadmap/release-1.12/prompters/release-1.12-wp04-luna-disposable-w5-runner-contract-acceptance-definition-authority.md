# Release 1.12 WP04 — Disposable W5 Runner Contract & Acceptance Definition Authority

## Authority identity

**Selected authority model: GPT-5.6 Luna**

This is a **definition, governance, architecture, and acceptance-contract authority only**.

GPT-5.6 Luna owns the contract, policy boundary, architecture, reconciliation criteria, and acceptance definition in this authority.

GPT-5.6 Terra remains the only model authorized by a later, separate authority to implement, validate, or execute the disposable W5 runner or perform approved mutations.

GPT-5.6 Sol may support analysis or alternatives, but it is non-authoritative and must not replace Luna or Terra.

## Purpose

Define the minimum disposable, non-production W5 runner architecture needed to eliminate repeated interactive reconstruction of the W5 harness while preserving the existing governed W5 acceptance contract exactly.

This authority follows the completed execution-budget diagnostic:

```text
RELEASE 1.12 WP04 — W5 EXECUTION-BUDGET DIAGNOSTIC: COMPLETE

Primary classification   = D3 EXCESSIVE_PREEXECUTION_ORCHESTRATION
Secondary classification = D4 INTERACTIVE_SESSION_CONTROL_STOP
Increased-budget effect  = UNKNOWN
Current one-session completion = PLAUSIBLE_BUT_UNPROVEN
Durable runner effect    = MATERIALLY_REDUCES_SESSION_WORK
```

The diagnostic did not execute W5 and introduced no project, production, or external mutation.

# Canonical starting state

```text
W1 = PASS — carry forward
W2 = PASS — carry forward
W3 = PASS — carry forward
W4 = PASS — carry forward

W5 governed acceptance = NOT_GRANTED
W6 = NOT_RUN
W7 = NOT_RUN
W8 = NOT_RUN

Production mutations = 0 from diagnostic
External mutations = 0 from diagnostic
PUBLICATION BLOCKER = UNRESOLVED
```

Existing governed WP04 tracked modifications are pre-existing and must not be attributed to the diagnostic or this Luna definition authority.

PowerShell compatibility is binding:

```text
Windows PowerShell 5.1.26100.9444
Language/runtime target: Windows PowerShell 5.1
```

Do not assume PowerShell 7.

# Luna mission

Perform read-only reconciliation and define a precise runner contract for a later Terra implementation.

The runner must reduce session-bound orchestration. It must **not** change W5 semantics, production policy, acceptance thresholds, or evidence requirements.

Luna must decide and record:

1. the permitted runner location and lifecycle;
2. whether it is tracked or disposable/local-only;
3. the runner's exact responsibilities;
4. prohibited responsibilities;
5. production-wrapper identity rules;
6. shim/interception rules;
7. RunId allocation boundary;
8. P01–P20 accounting contract;
9. durable evidence layout;
10. P19 cleanup ordering;
11. error-precedence behavior;
12. secret hygiene;
13. repository invariants;
14. implementation-validation gates for Terra;
15. the later execution boundary.

# Required architectural preference

Prefer the **lowest-mutation architecture** that materially removes repeated inline orchestration.

Evaluate at least:

```text
R1 — disposable/local-only runner outside tracked repository content
R2 — ignored project-local runner
R3 — tracked project-local test/validation runner
```

Select exactly one architecture based on the existing repository/harness evidence.

Do not select a tracked runner merely for convenience if a disposable/local-only runner can be made reproducible and reviewable.

If selecting a non-tracked runner, define how Terra must durably preserve:

- runner source/hash;
- parser result;
- contract-validation evidence;
- execution ledger;

without converting the runner itself into product code.

# Non-production boundary

The runner is validation orchestration only.

It MUST NOT become:

- application runtime code;
- deployment runtime code;
- a second persistence implementation;
- an archive/extraction policy implementation;
- an Azure-specific persistence implementation;
- a replacement for the production wrapper;
- a production evidence endpoint;
- a new product capability.

It must invoke the actual governed production wrapper as the system under test.

# Exact-byte production-wrapper contract

The runner must execute an exact-byte copy of the governed production wrapper.

Before execution it must prove:

```text
governed source wrapper SHA-256
copied execution wrapper SHA-256
source == execution copy
```

After execution it must prove:

```text
source wrapper SHA-256 unchanged
execution wrapper SHA-256 unchanged
pre == post
```

The runner must never patch, rewrite, preprocess, or generate a semantically equivalent replacement for the wrapper.

# Shim/interception contract

The runner may provide only narrow fail-closed test shims needed to induce the W5 retrieval-failure scenario and prevent real external mutation.

Shims must:

- intercept only the specifically governed external operations;
- record every intercepted invocation;
- fail closed on unexpected external operations;
- never duplicate production archive/extraction classification logic;
- never manufacture final production lifecycle state;
- allow the production wrapper to derive `RETRIEVAL_FAILED + NOT_APPLICABLE` itself;
- make unexpected real external invocation detectable and acceptance-failing.

Luna must identify the minimum shim surface from existing harness evidence.

# Fresh-scenario contract

Every governed W5 execution must create a new disposable scenario.

Before any W5 RunId allocation:

```text
S = absolute fresh disposable sandbox path
W = absolute exact-byte copied-wrapper path
```

The actual Windows PowerShell 5.1 child must prove visibility of both.

Fail closed before RunId allocation if S/W validation, parser validation, or exact-byte validation fails.

A pre-wrapper harness failure grants:

```text
W5 acceptance credit = NONE
production defect = NO unless independently proven
```

# RunId contract

A fresh W5 RunId may be allocated only after:

```text
PowerShell version gate = PASS
complete runner parser gate = PASS
S validation = PASS
W validation = PASS
child S/W visibility = PASS
exact-byte pre-execution gate = PASS
```

Never reuse any historical, diagnostic, failed, accepted, malformed, or aborted RunId.

Explicitly forbidden includes:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
initialize-w3-cd08d2233c41405287f66f00069ff605
```

All historical Azure qualification/diagnostic RunIds remain forbidden.

# Canonical W5 production-derived result

The runner must allow the production wrapper to derive:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

These values may be asserted after execution but may not be injected as substitutes for production behavior.

# P01–P20 contract — immutable

The durable runner does not alter or aggregate away any predicate.

Every governed execution must independently record:

```text
P01 Windows PowerShell version == 5.1.26100.9444
P02 complete runner/harness parser errors == 0
P03 exact-byte source/pre-execution wrapper identity == PASS
P04 exact-byte pre/post-execution wrapper identity == PASS
P05 production wrapper execution completed == PASS
P06 ArchiveRetrieval == RETRIEVAL_FAILED
P07 FreshExtraction == NOT_APPLICABLE
P08 EvidenceCheckpoint == PASS
P09 FinalLifecycleResult == SUCCESS
P10 RestoreOnly count == 1
P11 unexpected real external invocations == 0
P12 external-call ledger contains only governed intercepted calls == PASS
P13 repository modified-path scope unchanged == PASS
P14 staged path count == 0
P15 git diff --check == PASS
P16 secret hygiene == PASS
P17 durable sanitized evidence retention == PASS
P18 complete durable W5 scenario ledger retention == PASS
P19 disposable sandbox cleanup == PASS
P20 error-precedence accounting == PASS
```

For each predicate require durable recording of:

```text
Predicate ID
Requirement
Expected value
Observed value/evidence
PASS/FAIL
Evidence location
```

`P01–P20 = ALL_PASS` remains the only W5 acceptance gate.

# P19 ordering — immutable

The runner architecture must make the following ordering structural and difficult to misuse:

```text
1. Evaluate/record P01–P18.
2. Evaluate/record P20.
3. Persist durable sanitized evidence outside sandbox.
4. Delete disposable sandbox.
5. Verify sandbox absence.
6. Evaluate P19 from post-cleanup observation.
7. Persist/finalize P19 outside sandbox without recreating sandbox.
8. Aggregate P01–P20.
```

P19 evaluated before verified cleanup is invalid.

The runner must not recreate the sandbox while finalizing its ledger.

# P20 error precedence

The runner must prove rather than assume:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible under production policy
EvidenceCheckpoint = PASS
RestoreOnly executes exactly once
No later restoration/lifecycle condition incorrectly replaces SUCCESS
```

Do not clone the production policy function merely to make P20 pass.

# Durable evidence contract

Define a durable evidence root outside the disposable sandbox.

At minimum retain sanitized:

- scenario identity;
- fresh RunId;
- runner identity/hash;
- production wrapper source/pre/post hashes;
- PowerShell version;
- parser result;
- child S/W visibility evidence;
- wrapper result;
- intercepted-call ledger;
- P01–P20 individual records;
- cleanup evidence;
- exact mutation accounting;
- final aggregate result.

Evidence must not contain secrets or temporary evidence tokens.

# Repository invariants

The runner must measure and preserve the authorized repository baseline.

Current known state from diagnostic:

```text
modified tracked paths = two pre-existing governed WP04 scripts
staged paths = 0
git diff --check = PASS
```

A later Terra implementation/execution authority must distinguish:

```text
PRE_EXISTING_AUTHORIZED_MODIFICATIONS
vs
NEW_RUNNER_IMPLEMENTATION_MUTATIONS
vs
UNAUTHORIZED_MUTATIONS
```

No mutation may be silently attributed to the wrong authority.

# README preservation

If Luna selects any tracked implementation architecture that could touch README documentation, the active front-door README information-preservation policy applies:

```text
EXISTING README INFORMATION → PRESERVE
PRESERVE INFORMATION; UPDATE STATUS
SUMMARY ≠ AUTHORITY TO DELETE DETAIL
README PRESERVATION — ZERO UNAUTHORIZED INFORMATION LOSS: PASS
```

Prefer no README mutation for this runner.

# Luna read-only validation

Luna may inspect existing repository scripts/harness evidence to determine the smallest runner contract.

Luna must not:

- create or edit runner files;
- execute governed W5;
- create a fresh governed W5 sandbox;
- allocate a W5 RunId;
- mutate production files;
- stage/commit/push;
- mutate Azure;
- mutate Docker/GHCR;
- mutate GitHub;
- configure Twelve Data;
- close #263;
- start WP05.

# Required Luna decision record

Emit:

```text
RELEASE 1.12 WP04 — DISPOSABLE W5 RUNNER CONTRACT RECONCILIATION: PASS|FAIL

SELECTED RUNNER ARCHITECTURE:
<R1 | R2 | R3>

SELECTED RUNNER LOCATION/LIFECYCLE:
<exact definition>

TRACKED RUNNER MUTATION REQUIRED:
<YES | NO>

RUNNER RESPONSIBILITIES:
<bounded set>

PROHIBITED RESPONSIBILITIES:
<bounded set>

MINIMUM SHIM SURFACE:
<evidence-backed definition>

PRODUCTION POLICY DUPLICATION:
FORBIDDEN

EXACT-BYTE PRODUCTION WRAPPER:
REQUIRED

WINDOWS POWERSHELL 5.1.26100.9444:
REQUIRED

PRE-RUNID FAIL-CLOSED GATES:
DEFINED

P01-P20 INDIVIDUAL ACCOUNTING:
UNCHANGED / REQUIRED

P19 POST-CLEANUP ORDER:
UNCHANGED / REQUIRED

P20 ERROR PRECEDENCE:
UNCHANGED / REQUIRED

DURABLE SANITIZED EVIDENCE:
REQUIRED

SECRET HYGIENE:
REQUIRED

W5 ACCEPTANCE THRESHOLD:
P01-P20 = ALL_PASS

W5 GOVERNED ACCEPTANCE:
NOT_GRANTED

W6/W7/W8:
NOT_RUN

PUBLICATION BLOCKER:
UNRESOLVED

LUNA AUTHORITY MUTATIONS:
0
```

# Next-authority definition

If and only if the Luna reconciliation passes, define the minimum next **GPT-5.6 Terra implementation authority**.

The next Terra authority should:

1. create the selected runner only;
2. parser-check it under Windows PowerShell 5.1.26100.9444;
3. structurally validate exact-byte, shim, P01–P20, P19, P20, evidence, secret, and repository contracts;
4. NOT execute the governed W5 wrapper;
5. NOT allocate a governed W5 RunId;
6. stop for Luna reconciliation of the runner implementation.

Governed W5 execution must remain a later separate Terra authority after Luna accepts the implemented runner.

# Final stop

**STOP AFTER LUNA CONTRACT DEFINITION AND RECONCILIATION.**

No runner implementation.
No governed W5 execution.
No W5 RunId.
No production/external mutation.
No W6–W8.
No publication.
No WP04 lifecycle closure.

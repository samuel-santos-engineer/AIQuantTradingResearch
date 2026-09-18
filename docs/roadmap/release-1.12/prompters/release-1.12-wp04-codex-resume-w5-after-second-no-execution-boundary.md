# Codex Execution Prompt — Release 1.12 WP04 W5 Resume After Second No-Execution Boundary

## Execution authority

**Selected execution model: GPT-5.6 Terra**

Continue under the existing:

**Release 1.12 WP04 — W5 P19 Ordering Correction & Fresh Rerun Authority**

This prompt is execution continuation only. It creates no new governance decision.

## Binding starting state

Two successive Codex turns ended before fresh W5 execution because insufficient tool time remained. Neither turn began the governed run.

Treat this state as authoritative:

```text
PRE-WRAPPER PARSER GATE = PASS
Windows PowerShell = 5.1.26100.9444
Complete disposable W5 harness parser errors = 0

Fresh W5 RunId allocated = NO
Fresh sandbox/scenario identity allocated = NO
Wrapper invoked = NO
Fresh P01–P20 execution = NOT_RUN
W5 governed acceptance = NOT_GRANTED

Production source/helper mutations = 0
External mutations = 0
```

There is no partial W5 run to resume and no RunId to recover.

Carry forward only:

```text
W1 = PASS
W2 = PASS
W3 = PASS
W4 = PASS
W5 = NOT_GRANTED
W6 = NOT_RUN
W7 = NOT_RUN
W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
```

## Resolved harness conditions

The following are already established:

```text
Every Test-Path Boolean is precomputed before predicate collection construction.
P19 is evaluated only after sandbox deletion and deletion verification.
Complete harness parser gate under Windows PowerShell 5.1.26100.9444 = PASS.
Parser errors = 0.
```

Do not re-litigate these resolved corrections absent contradictory execution evidence.

# Mission

Use this turn to execute **one complete fresh governed W5 run**.

Do not stop merely because previous turns lacked execution time. Begin execution promptly while preserving every safety gate.

## Fresh identity allocation

Only when ready to begin the governed wrapper run:

1. create a NEW disposable W5 scenario/sandbox identity;
2. generate a NEW synthetic W5 RunId;
3. persist both in the durable W5 ledger.

Never reuse any previous failed, diagnostic, accepted, or aborted identity/RunId.

Explicitly forbidden includes:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
initialize-w3-cd08d2233c41405287f66f00069ff605
```

All historical Azure diagnostic RunIds remain forbidden.

## Exact-byte gate

Before wrapper invocation:

- locate the governed production wrapper;
- hash governed source and execution copy;
- require exact-byte equality;
- retain SHA-256 evidence.

Do not mutate production source/helper code.

## Execute W5

Run the real exact-byte production wrapper through the corrected disposable harness.

Expected production-derived state:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

These states must be produced by the real wrapper behavior. Do not manufacture them in the harness or clone production policy.

## P01–P20 — all fresh

Independently prove:

```text
P01 Windows PowerShell version == 5.1.26100.9444
P02 complete disposable harness parser errors == 0
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

For each predicate record:

```text
Predicate ID
Requirement
Expected
Observed
PASS/FAIL
Evidence location
```

No aggregate assertion may replace individual evidence.

## Binding P19 ordering

Execute exactly:

```text
1. Execute/validate P01–P18.
2. Execute/derive/validate P20.
3. Persist durable sanitized audit evidence OUTSIDE the disposable sandbox.
4. Delete the disposable sandbox.
5. Verify deletion succeeded.
6. Evaluate the precomputed post-cleanup Test-Path Boolean.
7. ONLY NOW evaluate and emit P19.
8. Finalize P19 in the durable external ledger WITHOUT recreating the sandbox.
9. ONLY THEN aggregate P01–P20.
```

## P20

Explicitly prove:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
EvidenceCheckpoint = PASS
RestoreOnly executes exactly once
No later lifecycle/restoration condition incorrectly replaces SUCCESS
```

## Post-run integrity

After wrapper execution:

- hash the wrapper again;
- prove source/pre/post exact-byte identity;
- prove no production mutation occurred.

Require:

```text
repository modified-path scope = unchanged from authorized baseline
staged path count = 0
git diff --check = PASS
secret hygiene = PASS
unexpected real external invocations = 0
```

Forbidden:

```text
production source/helper mutation
new production seam
staging
commit/push
PR/merge
GitHub lifecycle mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
README mutation
```

## Failure boundary

After wrapper invocation, stop at the first governed failure.

Do not remediate production source under this authority.

If a production defect appears, preserve sanitized evidence and stop for reconciliation.

A pre-wrapper disposable-harness failure grants no W5 acceptance credit and must not be mislabeled as a production defect.

## Acceptance

Only:

```text
P01–P20 = ALL_PASS
```

grants W5 acceptance.

On success emit at least:

```text
RELEASE 1.12 WP04 — W5 P19 ORDERING-CORRECTED FRESH RERUN: PASS
RELEASE 1.12 WP04 — W5 POWERSHELL 5.1 VERSION: PASS
RELEASE 1.12 WP04 — W5 HARNESS PARSER: PASS
RELEASE 1.12 WP04 — W5 EXACT-BYTE PRE HASH: PASS
RELEASE 1.12 WP04 — W5 EXACT-BYTE POST HASH: PASS
RELEASE 1.12 WP04 — W5 ARCHIVE STATE: RETRIEVAL_FAILED
RELEASE 1.12 WP04 — W5 EXTRACTION STATE: NOT_APPLICABLE
RELEASE 1.12 WP04 — W5 CHECKPOINT STATE: PASS
RELEASE 1.12 WP04 — W5 FINAL LIFECYCLE RESULT: SUCCESS
RELEASE 1.12 WP04 — W5 RESTORE-ONLY COUNT: 1
RELEASE 1.12 WP04 — W5 UNEXPECTED REAL EXTERNAL INVOCATIONS: 0
RELEASE 1.12 WP04 — W5 REPOSITORY INVARIANTS: PASS
RELEASE 1.12 WP04 — W5 SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — W5 ERROR PRECEDENCE: PASS
RELEASE 1.12 WP04 — W5 DURABLE EVIDENCE RETENTION: PASS
RELEASE 1.12 WP04 — W5 SANDBOX CLEANUP: PASS
RELEASE 1.12 WP04 — W5 P19 EVALUATED AFTER VERIFIED CLEANUP: PASS
RELEASE 1.12 WP04 — W5 INDIVIDUAL ACCOUNTING PREDICATES P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED
RELEASE 1.12 WP04 — W1/W2/W3/W4 CARRY-FORWARD: AUTHORIZED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — NEXT HYBRID RESTART POINT: W6
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TRACKED PRODUCTION MUTATIONS: 0
RELEASE 1.12 WP04 — EXTERNAL MUTATIONS: 0
RELEASE 1.12 WP04 — TERRA W5 P19 ORDERING CORRECTION AND FRESH RERUN COMPLETE
```

Report the fresh scenario identity, RunId, durable ledger path, wrapper hashes, individual P01–P20 evidence, and exact mutation accounting.

# Final boundary

**STOP AFTER W5.**

Even after full W5 PASS:

```text
W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
```

Do not execute W6 under this prompt.

Begin now at fresh scenario/sandbox identity and W5 RunId allocation, then execute the complete governed W5 validation.

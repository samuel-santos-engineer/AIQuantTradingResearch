# Codex Execution Prompt — Release 1.12 WP04 Execute Ready W5 Fresh Run

## Authority

**Selected execution model: GPT-5.6 Terra**

Continue under the existing **Release 1.12 WP04 — W5 P19 Ordering Correction & Fresh Rerun Authority**.

This is an execution prompt only. No new governance decision is introduced.

## Binding state

```text
W1 = PASS — carry forward
W2 = PASS — carry forward
W3 = PASS — carry forward
W4 = PASS — carry forward

W5 authority = READY
W5 fresh execution = NOT_RUN
W5 governed acceptance = NOT_GRANTED

W6 = NOT_RUN
W7 = NOT_RUN
W8 = NOT_RUN

Windows PowerShell = 5.1.26100.9444
Complete disposable W5 harness parser gate = PASS
Parser errors = 0

Fresh W5 RunId allocated = NO
Fresh sandbox/scenario identity allocated = NO
Wrapper invoked = NO
Production mutations = 0
External mutations = 0
PUBLICATION BLOCKER = UNRESOLVED
```

The corrected harness already establishes:

```text
Every Test-Path Boolean is precomputed before predicate collection construction.
P19 is evaluated only after sandbox deletion and deletion verification.
```

Do not re-litigate resolved harness corrections unless new execution evidence contradicts them.

# Execute now

Perform one complete fresh governed W5 run.

## 1. Fresh identity

Only when ready to invoke the governed scenario:

- allocate a NEW disposable scenario/sandbox identity;
- allocate a NEW synthetic W5 RunId;
- persist both in the durable external W5 ledger.

Never reuse any previous failed, diagnostic, accepted, or aborted RunId, including:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
initialize-w3-cd08d2233c41405287f66f00069ff605
```

All historical Azure diagnostic RunIds are forbidden from reuse.

## 2. Exact-byte gate

Before wrapper execution:

- locate the governed production wrapper;
- compute SHA-256 of governed source;
- compute SHA-256 of the execution copy;
- require exact-byte equality;
- retain evidence.

No production source/helper mutation is authorized.

## 3. Run W5

Execute the real exact-byte production wrapper through the corrected disposable harness.

The production-derived result must naturally establish:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Do not manufacture these internal states or duplicate production policy in the harness.

## 4. Prove P01–P20 individually

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

For every predicate record:

```text
Predicate ID
Requirement
Expected
Observed
PASS/FAIL
Evidence location
```

Do not replace individual evidence with aggregate assertions.

## 5. Mandatory P19 ordering

Execute exactly:

```text
1. Execute and validate P01–P18.
2. Execute/derive and validate P20.
3. Persist durable sanitized audit evidence OUTSIDE the disposable sandbox.
4. Delete the disposable sandbox.
5. Verify deletion succeeded.
6. Evaluate the precomputed post-cleanup Test-Path Boolean.
7. ONLY NOW evaluate and emit P19.
8. Finalize P19 in the durable external ledger WITHOUT recreating the sandbox.
9. ONLY THEN aggregate P01–P20.
```

## 6. P20

Explicitly prove:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
EvidenceCheckpoint = PASS
RestoreOnly executes exactly once
No later lifecycle/restoration condition incorrectly replaces SUCCESS
```

## 7. Post-run invariants

Re-hash the wrapper after execution and prove exact-byte source/pre/post identity.

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
commit
push
PR creation/merge
GitHub issue/project/milestone lifecycle mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
README mutation
```

## 8. Failure boundary

After wrapper invocation, stop on the first governed failure.

Do not remediate production code under this authority.

If a production defect is exposed, preserve sanitized evidence and stop for reconciliation.

A disposable pre-wrapper harness failure grants no W5 acceptance credit and is not itself a production defect.

## 9. Acceptance

Only:

```text
P01–P20 = ALL_PASS
```

grants W5 acceptance.

If all pass, emit at least:

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

Also report the fresh scenario identity, fresh RunId, durable ledger path, wrapper SHA-256 before/after, individual P01–P20 accounting, and exact mutation accounting.

# Final boundary

**STOP AFTER W5.**

Even if W5 passes:

```text
W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
```

Do not execute W6.

Begin immediately with fresh scenario/RunId allocation and the complete governed W5 execution.

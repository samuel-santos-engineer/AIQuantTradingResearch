# Codex Execution Prompt — Resume Release 1.12 WP04 Fresh W5 After No-Execution Boundary

## Execution authority

**Selected execution model: GPT-5.6 Terra**

Continue under the existing:

**Release 1.12 WP04 — W5 P19 Ordering Correction & Fresh Rerun Authority**

This is a continuation execution prompt, not a new governance decision.

## Binding starting state

The immediately preceding Codex turn ended because insufficient execution time remained. It ended **before any fresh W5 execution began**.

Treat this state as binding:

```text
PRE-WRAPPER PARSER GATE = PASS
Windows PowerShell = 5.1.26100.9444
Complete disposable W5 harness parser errors = 0

Fresh W5 scenario/RunId allocated = NO
Wrapper invoked = NO
Fresh P01–P20 execution = NOT_RUN
W5 governed acceptance = NOT_GRANTED

Production source/helper mutations = 0
External mutations = 0
```

No RunId was consumed. No partial W5 execution evidence exists to promote.

Carry forward:

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

## Harness correction already established

The disposable orchestration has already been corrected so that:

```text
Every Test-Path Boolean is precomputed before predicate-collection construction.
P19 is evaluated only after sandbox deletion and deletion verification.
```

The complete harness already passed the Windows PowerShell 5.1 parser gate with zero errors.

Do not spend the execution window re-litigating the resolved syntax issue. You may verify necessary prerequisites, but do not treat the prior no-execution turn as a failed W5 attempt.

# Mission

Execute **one complete fresh governed W5 run now** and independently account for P01–P20.

Do not merely plan or describe the run.

## 1. Allocate the fresh identity

At the start of actual W5 execution:

- create a NEW disposable W5 scenario identity;
- generate a NEW synthetic W5 RunId;
- record both in the durable W5 evidence ledger.

Never reuse a previous failed, diagnostic, accepted, or aborted identity/RunId.

Explicitly forbidden includes:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
initialize-w3-cd08d2233c41405287f66f00069ff605
```

All historical Azure diagnostic RunIds are also forbidden.

## 2. Exact-byte production wrapper

Before invocation:

- locate the governed production wrapper;
- calculate source SHA-256;
- calculate execution-copy SHA-256;
- require exact-byte identity;
- retain the pre-execution proof.

Do not modify production source/helper code.

## 3. Execute governed W5

Invoke the real exact-byte production wrapper through the corrected disposable harness.

The production-derived W5 result must naturally be:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Do not manufacture these internal production states.

Do not clone production policy into the harness.

All external operations must remain governed/intercepted according to the active authority.

## 4. Fresh P01–P20 accounting

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

For every predicate retain and report:

```text
Predicate ID
Requirement
Expected value
Observed value/evidence
PASS/FAIL
Evidence location
```

No aggregate-only assertion is sufficient.

## 5. Binding P19 order

Use this exact order:

```text
1. Execute/validate P01–P18.
2. Execute/derive/validate P20.
3. Persist durable sanitized audit evidence OUTSIDE the disposable sandbox.
4. Delete the disposable sandbox.
5. Verify sandbox deletion succeeded.
6. Evaluate the already-precomputed post-cleanup Test-Path Boolean.
7. ONLY NOW evaluate and emit P19.
8. Finalize P19 in the durable external ledger WITHOUT recreating the sandbox.
9. ONLY THEN aggregate P01–P20.
```

The durable ledger must survive cleanup.

P19 must represent verified post-cleanup state.

## 6. P20 proof

Explicitly demonstrate:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
EvidenceCheckpoint = PASS
RestoreOnly executes exactly once
No later lifecycle/restoration condition incorrectly replaces SUCCESS
```

## 7. Post-execution wrapper integrity

After execution:

- hash the production wrapper again;
- prove source/pre/post exact-byte identity;
- prove the disposable harness did not mutate it.

## 8. Repository and mutation invariants

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
PR creation or merge
GitHub issue/project/milestone lifecycle mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
README mutation
```

## 9. Failure behavior

After wrapper invocation, stop at the first governed failure.

Examples include:

- predicate failure;
- wrapper exception;
- canonical-result divergence;
- exact-byte mismatch;
- unexpected external invocation;
- repository-scope violation;
- secret-hygiene failure;
- durable-evidence failure;
- cleanup failure;
- P19 ordering violation;
- requirement for unauthorized remediation.

Do not repair production source under this authority.

If a production defect is discovered, preserve sanitized evidence and stop for reconciliation.

If a disposable harness-only problem occurs before wrapper invocation, do not classify it as a production defect and do not grant W5 acceptance credit.

## 10. W5 acceptance

W5 governed acceptance requires:

```text
P01–P20 = ALL_PASS
```

If any predicate is not proven PASS:

```text
W5 GOVERNED ACCEPTANCE = NOT_GRANTED
W6/W7/W8 = NOT_RUN
```

If and only if all twenty pass, emit at least:

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

Also report:

- fresh scenario identity;
- fresh W5 RunId;
- durable evidence/ledger path;
- wrapper SHA-256 before and after;
- P01–P20 individual accounting;
- exact mutation accounting.

# Final boundary

**STOP AFTER W5.**

Even if W5 passes:

```text
W6 = NOT_RUN
W7 = NOT_RUN
W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
```

Do not begin W6 under this prompt.

Start now at **fresh W5 scenario identity and RunId allocation**, then execute the complete governed W5 run.

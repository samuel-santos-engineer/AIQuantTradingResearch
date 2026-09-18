# Codex Execution Prompt — Release 1.12 WP04 Fresh W5 After Parser-Gate PASS

## Execution authority

**Selected execution model: GPT-5.6 Terra**

Continue under the existing:

**Release 1.12 WP04 — W5 P19 Ordering Correction & Fresh Rerun Authority**

This prompt does not create a new governance decision. It executes the already-authorized fresh W5 run now that the complete disposable harness has passed its mandatory pre-wrapper gate.

## Confirmed pre-wrapper state

Treat the following as established evidence from the immediately preceding gate:

```text
Windows PowerShell = 5.1.26100.9444
Complete disposable W5 harness parser errors = 0
PRE-WRAPPER PARSER GATE = PASS

Wrapper invoked during parser gate = NO
W5 RunId allocated during parser gate = NO
W5 governed acceptance = NOT_GRANTED
Production source/helper mutations = 0
External mutations = 0
```

The harness correction is binding:

```text
Every Test-Path Boolean is precomputed before predicate-collection construction.
P19 is evaluated only after sandbox deletion and deletion verification.
```

Do not reopen the resolved syntax question unless execution produces new contradictory evidence.

# Mission

Execute **one fresh governed W5 validation run**.

Do not merely inspect, plan, or describe it.

## 1. Allocate fresh identity only now

Because the parser gate has passed, create:

- a NEW disposable W5 scenario identity; and
- a NEW synthetic W5 RunId.

Never reuse any prior failed, accepted, diagnostic, or aborted identity/RunId.

Explicitly forbidden includes:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
initialize-w3-cd08d2233c41405287f66f00069ff605
```

All historical Azure diagnostic RunIds are also forbidden.

Record the new identity and RunId in the durable W5 ledger.

## 2. Exact-byte production-wrapper boundary

Before invocation:

- identify the governed production wrapper;
- calculate its SHA-256;
- prove the execution copy is exact-byte identical to the governed source;
- preserve the pre-execution hash.

Do not modify production source/helper code.

Do not create a new production seam.

## 3. Execute W5

Execute the real exact-byte production wrapper through the corrected disposable harness.

The governed W5 retrieval-failure scenario must derive naturally from production behavior:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Do not assign these production results directly in the harness.

Do not clone the production archive/extraction policy.

External operations must remain governed/intercepted exactly as authorized.

## 4. Reprove P01–P20

This fresh run must independently account for every predicate.

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

For every predicate record individually:

```text
Predicate ID
Requirement
Expected
Observed
PASS/FAIL
Evidence location
```

Do not substitute an aggregate assertion for individual evidence.

## 5. Binding P19 execution order

The following order is mandatory:

```text
1. Execute and validate P01–P18.
2. Execute/derive and validate P20.
3. Persist durable sanitized audit evidence OUTSIDE the disposable sandbox.
4. Execute disposable sandbox deletion.
5. Verify the disposable sandbox no longer exists.
6. Evaluate the precomputed post-cleanup Test-Path Boolean.
7. ONLY NOW evaluate and emit P19.
8. Finalize P19 in the durable external ledger WITHOUT recreating the sandbox.
9. ONLY THEN aggregate P01–P20.
```

The durable ledger must survive sandbox deletion.

Do not evaluate P19 before cleanup.

Do not recreate the sandbox while finalizing the ledger.

## 6. P20 error-precedence proof

Explicitly prove:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
EvidenceCheckpoint = PASS
RestoreOnly executes exactly once
No later lifecycle/restoration condition incorrectly replaces the expected SUCCESS
```

## 7. Post-execution exact-byte proof

After wrapper execution:

- hash the governed wrapper again;
- prove pre/post exact-byte identity;
- prove the production wrapper was not mutated by the harness.

## 8. Repository and safety invariants

Require:

```text
repository modified-path scope = unchanged from authorized baseline
staged path count = 0
git diff --check = PASS
secret hygiene = PASS
unexpected real external invocations = 0
```

Forbidden mutations:

```text
production source/helper changes
staging
commit
push
PR creation/merge
GitHub issue/project/milestone lifecycle changes
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
```

Do not touch the README.

## 9. Failure behavior

After wrapper invocation, stop at the first governed failure, including:

- predicate failure;
- production-wrapper exception;
- unexpected production result;
- exact-byte mismatch;
- unexpected real external invocation;
- repository-scope violation;
- secret-hygiene failure;
- inability to preserve durable evidence;
- cleanup failure;
- P19 ordering violation;
- requirement for unauthorized remediation.

Do not modify production code to repair a failure under this authority.

If a new production defect is discovered, preserve sanitized evidence and stop for reconciliation.

## 10. Acceptance gate

W5 acceptance requires exactly:

```text
P01–P20 = ALL_PASS
```

Anything less leaves:

```text
W5 GOVERNED ACCEPTANCE = NOT_GRANTED
W6/W7/W8 = NOT_RUN
```

If and only if P01–P20 all pass, emit the complete result with at least these exact terminal markers:

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
- durable ledger path;
- wrapper SHA-256 before/after;
- P01–P20 individual accounting;
- exact mutation accounting.

# Final boundary

**STOP AFTER W5.**

Even on full success:

```text
W6 = NOT_RUN
W7 = NOT_RUN
W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
```

Do not proceed to W6 under this prompt.

Begin with fresh W5 scenario/RunId allocation and execute the governed wrapper now.

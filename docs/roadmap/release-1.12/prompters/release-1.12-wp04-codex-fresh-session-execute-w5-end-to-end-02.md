# Codex Execution Prompt — Release 1.12 WP04 Fresh-Session W5 End-to-End Execution

## Authority
**Selected execution model: GPT-5.6 Terra**

Continue under the existing **Release 1.12 WP04 — W5 P19 Ordering Correction & Fresh Rerun Authority**. This is execution continuation only; no new production, Azure, GitHub, or publication authority is created.

## Binding starting state

```text
W1–W4 = PASS — carry forward
W5 authority = READY
W5 fresh governed execution = NOT_COMPLETED
W5 governed acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
Production mutations = 0
External mutations = 0
PUBLICATION BLOCKER = UNRESOLVED
```

Previous sessions ended only because execution budget was insufficient. No partial W5 evidence from those stops is acceptance evidence.

Established gates:

```text
Windows PowerShell = 5.1.26100.9444
Complete disposable harness parser gate = PASS
Parser errors = 0
Test-Path Booleans are precomputed before predicate construction.
P19 is evaluated only after sandbox deletion and verified absence.
S = absolute fresh disposable sandbox path
W = absolute exact-byte copied-wrapper path
```

The prior null-environment launch did not invoke the wrapper and consumed no RunId.

# Mission

Execute one **complete fresh governed W5 run end-to-end**. Do not plan, defer, compress, combine, or skip P01–P20.

## Pre-wrapper execution

1. Create a NEW disposable sandbox/scenario; reuse no earlier sandbox.
2. Copy the governed production wrapper without changing bytes.
3. Before child launch explicitly set:
```powershell
$env:S = <absolute fresh disposable sandbox path>
$env:W = <absolute exact-byte copied-wrapper path>
```
4. Prove the actual Windows PowerShell 5.1 child receives both values.
5. Precompute all `Test-Path` Booleans before constructing predicate collections.
6. Require before RunId allocation:
```text
S_DEFINED/PRESENT/ABSOLUTE/EXISTS/FRESH = PASS
W_DEFINED/PRESENT/ABSOLUTE/EXISTS/IS_FILE/IN_FRESH_SCENARIO = PASS
CHILD_PROCESS_S_VISIBLE = PASS
CHILD_PROCESS_W_VISIBLE = PASS
W_EXACT_BYTE_COPY = PASS
Complete final harness parser errors = 0
```
7. Compute and retain SHA-256 for governed wrapper and execution copy; require exact-byte equality.

If any pre-wrapper gate fails: do not allocate a RunId, do not invoke the wrapper, grant no W5 acceptance credit, and do not classify it as a production defect absent independent production evidence.

## Fresh RunId and execution

Only after all pre-wrapper gates pass, allocate a NEW synthetic W5 RunId and persist it with the fresh scenario identity in durable evidence outside the sandbox.

Never reuse any prior failed/diagnostic/accepted RunId, including:
```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
initialize-w3-cd08d2233c41405287f66f00069ff605
```
All historical Azure diagnostic RunIds are also forbidden.

Execute the real exact-byte wrapper from `$env:S` via `$env:W`. Production behavior must naturally derive:
```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```
Do not manufacture these states or clone production policy.

## P01–P20 — independently record every predicate

```text
P01 PowerShell version == 5.1.26100.9444
P02 complete harness parser errors == 0
P03 exact-byte source/pre wrapper identity == PASS
P04 exact-byte pre/post wrapper identity == PASS
P05 production wrapper execution completed == PASS
P06 ArchiveRetrieval == RETRIEVAL_FAILED
P07 FreshExtraction == NOT_APPLICABLE
P08 EvidenceCheckpoint == PASS
P09 FinalLifecycleResult == SUCCESS
P10 RestoreOnly count == 1
P11 unexpected real external invocations == 0
P12 external ledger contains only governed intercepted calls == PASS
P13 repository modified-path scope unchanged == PASS
P14 staged path count == 0
P15 git diff --check == PASS
P16 secret hygiene == PASS
P17 durable sanitized evidence retention == PASS
P18 complete durable W5 scenario ledger retention == PASS
P19 disposable sandbox cleanup == PASS
P20 error-precedence accounting == PASS
```

For each record Predicate ID, requirement, expected, observed/evidence, PASS/FAIL, and evidence location. Aggregate-only assertions are forbidden.

## Binding P19 order

```text
1. Execute/validate P01–P18.
2. Execute/derive/validate P20.
3. Persist durable sanitized audit evidence OUTSIDE the sandbox.
4. Delete the disposable sandbox.
5. Verify deletion succeeded.
6. Use the precomputed post-cleanup Test-Path Boolean.
7. ONLY NOW evaluate and emit P19.
8. Finalize P19 in the durable external ledger WITHOUT recreating the sandbox.
9. ONLY THEN aggregate P01–P20.
```

P20 must explicitly prove:
```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
EvidenceCheckpoint = PASS
RestoreOnly = exactly once
No later lifecycle/restoration condition incorrectly replaces SUCCESS
```

After execution re-hash the wrapper and prove governed-source/pre/post exact-byte identity.

## Mutation boundary

Require:
```text
repository modified-path scope = unchanged from authorized baseline
staged path count = 0
git diff --check = PASS
secret hygiene = PASS
unexpected real external invocations = 0
```

Forbidden: production source/helper mutation, new production seam, staging, commit/push, PR/merge, GitHub lifecycle mutation, Azure mutation, Docker/GHCR mutation, Twelve Data configuration, README mutation.

After wrapper invocation stop at the first governed failure. Do not remediate production code. If a production defect appears, preserve sanitized evidence and stop for Luna reconciliation.

# Acceptance

Only `P01–P20 = ALL_PASS` grants W5 acceptance. Otherwise W5 remains NOT_GRANTED and W6–W8 remain NOT_RUN.

On success emit at least:
```text
RELEASE 1.12 WP04 — W5 HARNESS LAUNCH ENVIRONMENT: PASS
RELEASE 1.12 WP04 — W5 CHILD S/W ENVIRONMENT VISIBILITY: PASS
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

Report fresh scenario identity, fresh RunId, sanitized S/W identities, durable ledger path, wrapper hashes, all individual P01–P20 evidence, and exact mutation accounting.

# Final boundary

**STOP AFTER W5.** Even on success:
```text
W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
```

Begin immediately with the fresh sandbox and explicit child `S`/`W` validation, then execute the complete W5 run.

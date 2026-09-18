# Codex Prompt — Resume WP04 W5 After Pre-Wrapper Harness Syntax Failure

Continue execution under the already-loaded:

**GPT-5.6 Terra — Release 1.12 WP04 W5 P19 Ordering Correction & Fresh Rerun Authority**

The previous corrected-W5 attempt did **not** reach wrapper execution.

Binding classification:

```text
Latest failure = DISPOSABLE_HARNESS_SYNTAX_ONLY
Failure location = pre-wrapper disposable orchestration
Wrapper invoked = NO
Governed RunId consumed = NO
W5 acceptance credit = NONE
Production defect = NO
Production mutations = 0
External mutations = 0
```

The syntax error was in the disposable PowerShell orchestration around `Test-Path` predicate construction.

## Execute now

Do not plan or merely describe the correction. Continue the active Terra authority and perform the validation.

1. Correct **only** the disposable orchestration/harness PowerShell syntax responsible for the array-expression / `Test-Path` construction error.
2. Do not modify production source, production helper code, or the exact-byte production wrapper.
3. Parser-check the **entire corrected disposable orchestration/harness** under exactly:

```text
Windows PowerShell 5.1.26100.9444
```

4. Require **0 parser errors before any wrapper invocation**.
5. If parser validation fails again:
   - do not invoke the wrapper;
   - report the exact parser failure;
   - grant no W5 acceptance credit;
   - do not classify it as a production defect.
6. Once the complete harness parses successfully, create a **NEW disposable scenario identity**.
7. Generate a **NEW synthetic W5 RunId only when the harness is ready to invoke the wrapper**.
8. Do not reuse:
   - `initialize-w5-41ec52d151de47d3a3f3536ec0dd8976`;
   - any earlier failed/diagnostic RunId;
   - any identity allocated to the aborted syntax-failure attempt.

## Execute the governed fresh W5 scenario

Use the real exact-byte production wrapper.

Canonical production-derived result:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Do not manufacture these states in the harness and do not clone production policy.

Prove exact-byte source/pre/post wrapper identity.

Fail closed on all external operations.

## P01–P20

Reprove **all P01–P20** in this fresh run. Prior W5 predicate results cannot be promoted into this run.

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

Emit every predicate individually with:
- Predicate ID;
- governed requirement;
- expected value;
- observed value/evidence;
- PASS/FAIL;
- evidence location.

## Binding P19 ordering correction

Preserve this exact ordering:

```text
1. Execute/validate P01–P18.
2. Execute/derive/validate P20.
3. Persist durable sanitized audit evidence OUTSIDE the disposable sandbox.
4. Execute disposable sandbox cleanup.
5. Verify cleanup succeeded.
6. ONLY NOW evaluate and emit P19.
7. Finalize P19 in the retained external ledger WITHOUT recreating the sandbox.
8. ONLY THEN evaluate aggregate P01–P20 acceptance.
```

P19 must never again be evaluated before cleanup.

## P20

Explicitly prove:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
EvidenceCheckpoint = PASS
RestoreOnly = exactly once
no later lifecycle/restoration condition incorrectly replaces SUCCESS
```

## Mutation boundary

Forbidden:

```text
production source/helper mutation
new production seam
staging
commit/push
PR/merge
Azure mutation
Docker/GHCR mutation
GitHub lifecycle mutation
```

Repository modified-path scope must remain unchanged.

Require:

```text
staged path count = 0
git diff --check = PASS
secret hygiene = PASS
unexpected real external invocations = 0
```

## Stop conditions

After wrapper invocation, stop immediately on the first governed predicate failure, exact-byte failure, unexpected real external invocation, production behavior divergence, production defect/exception, repository-scope change, or requirement for unauthorized state manipulation.

Do not remediate production source under this authority.

A harness-only failure occurring before wrapper invocation grants no W5 acceptance credit and does not establish a production defect.

## Success boundary

W5 governed acceptance may be granted **ONLY** if the fresh run proves:

```text
P01–P20 = ALL_PASS
```

If all pass, emit every terminal marker required by the loaded Terra authority, including:

```text
RELEASE 1.12 WP04 — W5 P19 ORDERING-CORRECTED FRESH RERUN: PASS
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

## Final boundary

**STOP AFTER W5.**

Do not execute W6, W7, or W8.

Begin by correcting the disposable syntax and running the full Windows PowerShell 5.1 parser gate now.

# Codex Prompt — Run WP04 W5 After `Test-Path` Syntax Correction

Continue execution under the already-loaded:

**GPT-5.6 Terra — Release 1.12 WP04 W5 P19 Ordering Correction & Fresh Rerun Authority**

Do not plan or merely describe the next steps. Perform the authorized validation.

## Confirmed pre-wrapper defect

The prior disposable orchestration failed before wrapper invocation because `Test-Path` was used as a command token inside an array expression.

Binding classification:

```text
Failure class = DISPOSABLE_HARNESS_SYNTAX_ONLY
Wrapper invoked = NO
Governed W5 RunId consumed = NO
W5 acceptance credit = NONE
Production defect = NO
Production/helper mutations = 0
External mutations = 0
Fresh W5 authorization = PRESERVED
```

## Narrow correction

Correct only the disposable harness/orchestration syntax.

For every `Test-Path` value needed inside predicate construction, either:

1. evaluate it first and assign the Boolean result to a variable, then place that variable in the predicate structure; or
2. use an explicitly parenthesized PowerShell expression where valid.

Prefer precomputed Boolean variables when this makes Windows PowerShell 5.1 parsing/evaluation unambiguous.

Do not change production source, helper code, wrapper bytes, production policy, or governed W5 semantics.

## Mandatory parser gate

Before allocating the governed fresh W5 RunId or invoking the wrapper:

1. parser-check the **entire corrected disposable orchestration/harness**;
2. execute the parser gate under exactly:

```text
Windows PowerShell 5.1.26100.9444
```

3. require:

```text
Parser errors = 0
```

If the complete harness still has any parser error:
- do not invoke the wrapper;
- do not consume a governed W5 RunId;
- report the exact parser error;
- grant no W5 acceptance credit;
- do not classify the failure as a production defect.

## Fresh execution identity

Only after the full parser gate passes:

- create a NEW disposable W5 scenario identity;
- generate a NEW synthetic W5 RunId;
- do not reuse any prior W3/W5 failed or diagnostic RunId;
- do not reuse any identity associated with the aborted syntax attempt.

In particular, never reuse:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
```

## Execute real W5

Execute the **real exact-byte production wrapper** under the governed retrieval-failure fixture.

The production wrapper must derive:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Do not manufacture these internal states in the harness.

Do not clone production policy.

Prove source/pre/post wrapper SHA256 exact-byte identity.

Fail closed on every external operation.

## Reprove P01–P20

This fresh run must independently prove all twenty predicates:

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

For every predicate emit individually:

```text
Predicate ID
Governed requirement
Expected value
Observed value/evidence
PASS/FAIL
Evidence location
```

Do not aggregate until all individual predicate observations are correctly available.

## Binding P19 ordering

The previous P19 accounting failure must not recur.

Use exactly:

```text
1. Execute/validate P01–P18.
2. Execute/derive/validate P20.
3. Persist durable sanitized audit evidence OUTSIDE the disposable sandbox.
4. Execute disposable sandbox cleanup.
5. Verify cleanup succeeded.
6. ONLY AFTER VERIFIED CLEANUP evaluate and emit P19.
7. Finalize P19 in the retained external ledger without recreating the sandbox.
8. ONLY THEN evaluate aggregate P01–P20 acceptance.
```

P19 must observe the post-cleanup state.

## P20 proof

Explicitly prove:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
EvidenceCheckpoint = PASS
RestoreOnly = exactly once
No later lifecycle/restoration condition incorrectly replaces SUCCESS
```

## Repository and mutation boundary

Require:

```text
repository modified-path scope = unchanged
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
Azure mutation
Docker/GHCR mutation
GitHub lifecycle mutation
```

## Stop conditions

After wrapper invocation, stop immediately at the first:
- governed predicate failure;
- exact-byte failure;
- unexpected real external invocation;
- canonical W5 behavior divergence;
- production defect/exception;
- repository-scope change;
- need for unauthorized state manipulation.

Do not remediate production source under this authority.

## Acceptance

Grant W5 governed acceptance only when:

```text
P01–P20 = ALL_PASS
```

If all pass, emit the loaded authority's complete success markers, including:

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

## Final boundary

**STOP AFTER W5.**

Do not execute W6, W7, or W8.

Begin execution now with the narrow `Test-Path` syntax correction and complete Windows PowerShell 5.1 parser gate.

# Codex Execution Prompt — Release 1.12 WP04 W5 Fresh Launch with Sandbox Environment

## Authority

**Selected execution model: GPT-5.6 Terra**

Continue under the existing **Release 1.12 WP04 — W5 P19 Ordering Correction & Fresh Rerun Authority**.

This is a narrow harness-launch correction plus execution continuation. It introduces no new production or governance authority.

## Binding state

The latest attempt stopped before wrapper invocation because the disposable harness process was launched without its required environment variables:

```text
S = disposable sandbox path
W = exact-byte copied-wrapper path
```

Consequently:

```text
Set-Location $env:S = failed
& $env:W = failed
```

Binding classification:

```text
Failure = HARNESS_LAUNCH_ENVIRONMENT_MISSING
Production wrapper invoked = NO
Fresh W5 RunId allocated = NO
W5 acceptance credit = NONE
Production defect = NO
Production mutations = 0
External mutations = 0
```

Carry forward:

```text
W1 = PASS
W2 = PASS
W3 = PASS
W4 = PASS
W5 = NOT_GRANTED
W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
```

The complete corrected harness previously passed:

```text
Windows PowerShell = 5.1.26100.9444
Parser errors = 0
PRE-WRAPPER PARSER GATE = PASS
```

The `Test-Path`/P19 correction remains binding.

# Mission

Correct only the disposable harness launch environment, validate it fail-closed, then execute one complete fresh governed W5 run.

## 1. Establish launch environment before harness invocation

Create a fresh disposable scenario/sandbox.

Before launching the child harness process, explicitly set:

```powershell
$env:S = <absolute disposable sandbox path>
$env:W = <absolute exact-byte copied-wrapper path>
```

The values supplied to the child process must be the actual fresh scenario paths.

Do not rely on stale inherited values.

Do not hard-code repository-specific temporary identities.

## 2. Fail-closed pre-launch validation

Before allocating the governed W5 RunId or invoking the production wrapper, prove all of the following:

```text
S_DEFINED = PASS
S_NONEMPTY = PASS
S_ABSOLUTE = PASS
S_EXISTS = PASS
S_IS_EXPECTED_FRESH_DISPOSABLE_SANDBOX = PASS

W_DEFINED = PASS
W_NONEMPTY = PASS
W_ABSOLUTE = PASS
W_EXISTS = PASS
W_IS_FILE = PASS
W_IS_INSIDE_EXPECTED_DISPOSABLE_SCENARIO = PASS
W_EXACT_BYTE_COPY_OF_GOVERNED_WRAPPER = PASS
```

Evaluate all `Test-Path` calls before constructing predicate collections.

If any launch-environment predicate fails:

```text
DO NOT allocate W5 RunId
DO NOT invoke wrapper
DO NOT grant W5 acceptance credit
DO NOT classify as production defect
```

Report the failed launch predicate and stop.

## 3. Confirm PowerShell/harness gate

Use exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Require the complete disposable harness parser result to remain:

```text
Parser errors = 0
```

If any harness change was required solely to pass `S`/`W`, parser-check the complete final harness again before execution.

## 4. Fresh W5 identity

Only after the complete pre-launch environment and parser gates pass:

- allocate a NEW synthetic W5 RunId;
- persist the fresh scenario identity and RunId in the durable external ledger.

Never reuse any prior failed, diagnostic, accepted, or aborted RunId, including:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
initialize-w3-cd08d2233c41405287f66f00069ff605
```

All historical Azure diagnostic RunIds remain forbidden.

## 5. Execute exact-byte production wrapper

Invoke the real exact-byte copied production wrapper at `$env:W` from the fresh sandbox `$env:S`.

The production-derived W5 state must naturally be:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Do not manufacture these internal states.

Do not clone production policy into the harness.

## 6. Fresh P01–P20 proof

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

## 7. Binding P19 ordering

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

## 8. P20 proof

Explicitly prove:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
EvidenceCheckpoint = PASS
RestoreOnly executes exactly once
No later lifecycle/restoration condition incorrectly replaces SUCCESS
```

## 9. Integrity and mutation boundary

After wrapper execution, re-hash and prove source/pre/post exact-byte identity.

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

## 10. Failure behavior

Stop at the first governed failure after wrapper invocation.

Do not remediate production code under this authority.

If a production defect is exposed, retain sanitized evidence and stop for reconciliation.

Any pre-wrapper `S`/`W`, parser, or disposable-harness failure consumes no W5 acceptance credit and must not be mislabeled as a production defect.

## Acceptance

Only:

```text
P01–P20 = ALL_PASS
```

grants W5 governed acceptance.

On success emit at least:

```text
RELEASE 1.12 WP04 — W5 HARNESS LAUNCH ENVIRONMENT: PASS
RELEASE 1.12 WP04 — W5 S SANDBOX PATH VALIDATION: PASS
RELEASE 1.12 WP04 — W5 W WRAPPER PATH VALIDATION: PASS
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

- fresh sandbox/scenario identity;
- fresh W5 RunId;
- resolved `S` and `W` identities without exposing secrets;
- durable ledger path;
- wrapper SHA-256 before/after;
- P01–P20 individual accounting;
- exact mutation accounting.

# Final boundary

**STOP AFTER W5.**

Even on full success:

```text
W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
```

Do not execute W6 under this prompt.

Begin now by creating the fresh disposable scenario, setting and validating `S` and `W`, and only after those gates pass allocate the fresh W5 RunId and invoke the wrapper.

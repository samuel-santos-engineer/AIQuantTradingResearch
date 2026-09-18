# Codex Execution Prompt — Release 1.12 WP04 Resume W5 Sandbox-Environment Fresh Execution

## Authority

**Selected execution model: GPT-5.6 Terra**

Continue under the existing **Release 1.12 WP04 — W5 P19 Ordering Correction & Fresh Rerun Authority**.

This prompt is a continuation of the already-authorized disposable harness-launch correction. It creates no new production, Azure, GitHub, or release authority.

## Binding starting state

The prior attempt failed before wrapper invocation because the disposable child harness did not receive:

```text
S = disposable sandbox path
W = exact-byte copied-wrapper path
```

A subsequent turn performed no execution.

Therefore the current state is:

```text
Latest classified defect = HARNESS_LAUNCH_ENVIRONMENT_MISSING

Fresh sandbox after that failure = NOT_CREATED
Fresh W5 RunId = NOT_ALLOCATED
Production wrapper invoked = NO
Fresh P01-P20 execution = NOT_RUN
W5 acceptance credit = NONE
W5 governed acceptance = NOT_GRANTED

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
W6 = NOT_RUN
W7 = NOT_RUN
W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
```

Previously established harness gate:

```text
Windows PowerShell = 5.1.26100.9444
Complete disposable harness parser errors = 0
PRE-WRAPPER PARSER GATE = PASS
```

Previously established correction:

```text
Precompute every Test-Path Boolean before predicate collection construction.
Evaluate P19 only after sandbox deletion and verified absence.
```

# Mission

Perform the complete fresh W5 run now.

Do not merely restate the state or produce another execution plan.

## 1. Create a genuinely fresh disposable scenario

Create a new disposable sandbox/scenario identity.

Do not reuse any sandbox from the null-environment failure or earlier W5/W3 validation.

Copy the governed production wrapper into the fresh disposable scenario without changing its bytes.

## 2. Establish `S` and `W` before child-process launch

Before invoking the disposable child harness, explicitly establish:

```powershell
$env:S = <absolute fresh disposable sandbox path>
$env:W = <absolute exact-byte copied-wrapper path>
```

Ensure those values are actually inherited by the Windows PowerShell 5.1 child process that performs:

```powershell
Set-Location $env:S
& $env:W
```

Do not merely set local variables named `S` or `W`; prove the child receives the required environment values.

## 3. Fail-closed environment validation

Before W5 RunId allocation and before production-wrapper invocation, validate and retain evidence for:

```text
S_DEFINED = PASS
S_NONEMPTY = PASS
S_ABSOLUTE = PASS
S_EXISTS = PASS
S_EXPECTED_FRESH_SANDBOX = PASS

W_DEFINED = PASS
W_NONEMPTY = PASS
W_ABSOLUTE = PASS
W_EXISTS = PASS
W_IS_FILE = PASS
W_INSIDE_EXPECTED_FRESH_SCENARIO = PASS
W_EXACT_BYTE_COPY = PASS

CHILD_PROCESS_S_VISIBLE = PASS
CHILD_PROCESS_W_VISIBLE = PASS
```

Precompute every `Test-Path` result before constructing any predicate collection.

If any of these predicates fails:

```text
DO NOT allocate a W5 RunId
DO NOT invoke the wrapper
DO NOT grant W5 acceptance credit
DO NOT classify the failure as a production defect
```

Stop with exact sanitized evidence.

## 4. Parser/runtime gate

Use exactly:

```text
Windows PowerShell 5.1.26100.9444
```

The complete final disposable harness must have:

```text
Parser errors = 0
```

If the launch correction required any harness-text change, rerun the complete parser gate before wrapper execution.

## 5. Allocate the fresh W5 RunId only after all pre-wrapper gates pass

Generate a new synthetic W5 RunId only when:

```text
environment validation = PASS
child environment visibility = PASS
parser gate = PASS
exact-byte wrapper gate = PASS
```

Persist the fresh scenario identity and RunId in durable evidence outside the disposable sandbox.

Never reuse any previous failed, diagnostic, accepted, or aborted RunId.

Explicitly forbidden includes:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
initialize-w3-cd08d2233c41405287f66f00069ff605
```

All historical Azure diagnostic RunIds remain forbidden.

## 6. Execute the real exact-byte production wrapper

Execute `$env:W` from `$env:S` through the corrected disposable harness.

The production wrapper itself must derive:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Do not manufacture these states in the harness.

Do not duplicate production archive/extraction policy.

## 7. Independently prove P01-P20

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

Do not substitute aggregate assertions for individual predicate evidence.

## 8. Binding P19 ordering

Execute exactly:

```text
1. Execute/validate P01-P18.
2. Execute/derive/validate P20.
3. Persist durable sanitized audit evidence OUTSIDE the disposable sandbox.
4. Delete the disposable sandbox.
5. Verify deletion succeeded.
6. Use the precomputed post-cleanup Test-Path result.
7. ONLY NOW evaluate and emit P19.
8. Finalize P19 in the durable external ledger WITHOUT recreating the sandbox.
9. ONLY THEN aggregate P01-P20.
```

P19 evaluated before cleanup is invalid.

## 9. P20 proof

Explicitly prove:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible
EvidenceCheckpoint = PASS
RestoreOnly executes exactly once
No later lifecycle/restoration condition incorrectly replaces SUCCESS
```

## 10. Post-execution integrity

After wrapper execution:

- calculate wrapper SHA-256 again;
- prove governed source/pre/post exact-byte identity;
- prove the harness did not mutate production wrapper bytes.

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
GitHub issue/project/milestone mutation
Azure mutation
Docker/GHCR mutation
Twelve Data configuration
README mutation
```

## 11. Failure boundary

After wrapper invocation, stop at the first governed failure.

Do not remediate production code under this authority.

If execution exposes a production defect, preserve sanitized evidence and stop for Luna reconciliation.

A failure before wrapper invocation involving disposable environment, sandbox, parser, or harness mechanics:

```text
W5 acceptance credit = NONE
production defect = NO unless independent production evidence proves otherwise
```

## 12. Acceptance

Only:

```text
P01-P20 = ALL_PASS
```

grants W5 governed acceptance.

On full success emit at least:

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

Also report:

- fresh scenario/sandbox identity;
- fresh W5 RunId;
- sanitized resolved identities for `S` and `W`;
- durable ledger path;
- wrapper SHA-256 before and after;
- individual P01-P20 accounting;
- exact mutation accounting.

# Final boundary

**STOP AFTER W5.**

Even on full W5 success:

```text
W6 = NOT_RUN
W7 = NOT_RUN
W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
```

Do not execute W6.

Begin now with creation of the fresh disposable scenario and explicit child-process `S`/`W` environment validation. Allocate the W5 RunId only after all pre-wrapper gates pass.

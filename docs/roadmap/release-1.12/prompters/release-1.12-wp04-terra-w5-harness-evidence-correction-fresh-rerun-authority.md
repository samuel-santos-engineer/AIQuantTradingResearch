# GPT-5.6 Terra — Release 1.12 WP04 W5 Harness Evidence Correction & Fresh Rerun Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — governing reconciliation authority. Luna selected `E2`: production behavior expected, no production defect proven, retained evidence insufficient, fresh W5 rerun required.
- **GPT-5.6 Terra** — PRIMARY: correct disposable W5 harness/evidence instrumentation and execute one fresh W5 rerun.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

## 1. Binding ledger

```text
W1 = PASS — carry forward
W2 = PASS — carry forward
W3 = PASS — carry forward
W4 = PASS — carry forward
W5 = NOT_GRANTED — fresh rerun authorized
W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
```

The prior W5 production run had expected behavior but incomplete acceptance evidence. It grants no governed W5 PASS.

## 2. Canonical W5 scenario

Execute the real production wrapper under a controlled retrieval-failure fixture and prove production-derived:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Durable sanitized evidence and a complete scenario ledger must be retained.

## 3. Scope

This authority permits:
- disposable W5 harness/evidence-instrumentation correction only;
- one fresh W5 governed rerun;
- fresh sandbox/scenario identity;
- fresh synthetic RunId.

This authority does **not** permit production source/helper changes.

Do not run W6, W7, or W8 even if W5 passes.

## 4. Windows PowerShell gate

Target exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Before wrapper invocation:
1. record the actual PowerShell version;
2. parser-check the complete corrected disposable harness under Windows PowerShell 5.1;
3. require zero parser errors.

A harness-only construction/parser failure before wrapper invocation:
- grants no W5 acceptance credit;
- is not a production defect;
- does not consume the fresh-W5 authorization;
- may be corrected within this authority using a new fresh scenario identity.

## 5. Exact-byte requirement

Copy the current production wrapper byte-for-byte into the disposable sandbox.

Record and individually prove:
- source/current wrapper SHA256;
- sandbox wrapper pre-execution SHA256;
- sandbox wrapper post-execution SHA256;
- equality of all required identities.

No production-byte mutation is authorized.

## 6. Twenty canonical W5 predicates

Emit each predicate individually, with:

```text
Predicate ID
Governed requirement
Expected value
Observed value/evidence
PASS/FAIL
Evidence location
```

before any aggregate W5 result.

The 20 predicates are:

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

For P20, prove explicitly that the retrieval-failure path is eligible under the governed state machine, checkpoint success remains authoritative, restoration executes exactly once, and no later lifecycle/restoration condition incorrectly replaces the expected final `SUCCESS`.

Do not weaken, merge, or omit predicates merely because another predicate appears to imply them.

## 7. Harness/state boundary

Allowed:
- synthetic boundary fixture needed to induce retrieval failure;
- fail-closed interception of `git`, `az`, helper, or other external calls;
- disposable evidence/accounting instrumentation.

Forbidden:
- direct manufacture of production-derived archive/extraction/checkpoint/final states;
- cloned policy logic replacing the wrapper;
- production-source modification;
- new production seams;
- global behavior overrides used to force internal result states.

The real exact-byte wrapper must derive P06–P10 and the W5 lifecycle outcome.

## 8. Repository and mutation boundary

Before and after W5 prove:

```text
repository modified-path scope = unchanged
staged path count = 0
git diff --check = PASS
```

No tracked source mutation is authorized.

```text
tracked production mutations = 0
staging = 0
commit/push = 0
PR/merge = 0
Azure mutations = 0
Docker/GHCR mutations = 0
GitHub lifecycle mutations = 0
```

Unexpected real external invocation must remain zero.

## 9. Secret hygiene

Prove no secret/token/API key is introduced, persisted, printed, or retained by the W5 harness/evidence artifacts.

Twelve Data configuration remains outside WP04 and is not authorized here.

## 10. Durable evidence

Before sandbox cleanup, retain a sanitized durable evidence root containing sufficient material to independently audit all P01–P20, including:
- fresh RunId/scenario identity;
- hashes;
- parser/version evidence;
- wrapper output;
- production-derived states;
- interception ledger;
- repository predicates;
- secret-hygiene result;
- error-precedence accounting;
- cleanup plan/result;
- complete predicate ledger.

Report the durable evidence path.

## 11. Stop conditions

Stop immediately if:
- wrapper exact-byte identity fails;
- a real external invocation occurs;
- production behavior differs from the canonical W5 contract;
- a production exception/defect appears;
- a governed predicate fails after wrapper execution;
- the scenario requires unauthorized state manipulation;
- repository scope changes.

Do not remediate production source under this authority.

If a predicate is merely missing because disposable instrumentation failed to record it, stop with the exact missing predicate; do not claim W5 PASS.

## 12. Acceptance

W5 governed acceptance is granted only if **P01–P20 all PASS individually** before aggregate evaluation.

If all pass, emit:

```text
W5 = GOVERNED PASS
NEXT HYBRID RESTART POINT = W6
```

Then stop. W6–W8 require a separate continuation authority.

## 13. Publication/lifecycle boundary

Even after W5 PASS:

```text
PUBLICATION BLOCKER = UNRESOLVED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
NEW AZURE DIAGNOSTIC/ACCEPTANCE RUN = NOT_AUTHORIZED
REOPEN/REDEPLOY = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
MILESTONE #63 = OPEN
```

No publication or lifecycle mutation is authorized.

## 14. Required terminal markers

`RELEASE 1.12 WP04 — W5 FRESH RERUN: PASS`

`RELEASE 1.12 WP04 — W5 POWERSHELL 5.1 VERSION: PASS`

`RELEASE 1.12 WP04 — W5 HARNESS PARSER: PASS`

`RELEASE 1.12 WP04 — W5 EXACT-BYTE PRE HASH: PASS`

`RELEASE 1.12 WP04 — W5 EXACT-BYTE POST HASH: PASS`

`RELEASE 1.12 WP04 — W5 ARCHIVE STATE: RETRIEVAL_FAILED`

`RELEASE 1.12 WP04 — W5 EXTRACTION STATE: NOT_APPLICABLE`

`RELEASE 1.12 WP04 — W5 CHECKPOINT STATE: PASS`

`RELEASE 1.12 WP04 — W5 FINAL LIFECYCLE RESULT: SUCCESS`

`RELEASE 1.12 WP04 — W5 RESTORE-ONLY COUNT: 1`

`RELEASE 1.12 WP04 — W5 UNEXPECTED REAL EXTERNAL INVOCATIONS: 0`

`RELEASE 1.12 WP04 — W5 REPOSITORY INVARIANTS: PASS`

`RELEASE 1.12 WP04 — W5 STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — W5 GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — W5 SECRET HYGIENE: PASS`

`RELEASE 1.12 WP04 — W5 ERROR PRECEDENCE: PASS`

`RELEASE 1.12 WP04 — W5 DURABLE EVIDENCE RETENTION: PASS`

`RELEASE 1.12 WP04 — W5 SANDBOX CLEANUP: PASS`

`RELEASE 1.12 WP04 — W5 INDIVIDUAL ACCOUNTING PREDICATES P01-P20: ALL_PASS`

`RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED`

`RELEASE 1.12 WP04 — W1/W2/W3/W4 CARRY-FORWARD: AUTHORIZED`

`RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN`

`RELEASE 1.12 WP04 — NEXT HYBRID RESTART POINT: W6`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — TRACKED PRODUCTION MUTATIONS: 0`

`RELEASE 1.12 WP04 — EXTERNAL MUTATIONS: 0`

`RELEASE 1.12 WP04 — TERRA W5 HARNESS EVIDENCE CORRECTION AND FRESH RERUN COMPLETE`

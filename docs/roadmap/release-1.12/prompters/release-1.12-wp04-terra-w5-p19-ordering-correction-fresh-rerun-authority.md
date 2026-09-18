# GPT-5.6 Terra — Release 1.12 WP04 W5 P19 Ordering Correction & Fresh Rerun Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — governing reconciliation authority; selected `O1`.
- **GPT-5.6 Terra** — PRIMARY: correct disposable P19 ordering and execute one fresh W5 rerun.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

## Binding state

```text
W1/W2/W3/W4 = PASS — carry forward
W5 = NOT_GRANTED
W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED

P19 failure classification =
HARNESS_PREDICATE_EVALUATED_BEFORE_GOVERNED_ACTION

Production functional result = EXPECTED
Production defect proven = NO
Fresh W5 rerun = AUTHORIZED
```

Forbidden historical RunId:

```text
initialize-w5-41ec52d151de47d3a3f3536ec0dd8976
```

Do not reuse it or any earlier failed/diagnostic RunId.

## Mission

Correct **disposable harness ordering only** and execute one fresh W5 governed rerun.

No production source/helper modification is authorized.

The corrected ordering is binding:

```text
1. Execute/validate P01–P18.
2. Execute/derive/validate P20.
3. Persist durable sanitized audit evidence outside the disposable sandbox.
4. Execute disposable sandbox cleanup.
5. Verify cleanup succeeded.
6. Evaluate and emit P19 only now.
7. Finalize the retained predicate ledger with P19 without recreating the sandbox.
8. Only after P01–P20 are individually correct may aggregate W5 acceptance be evaluated.
```

## Fresh identities

Generate at execution time:
- NEW disposable scenario identity;
- NEW synthetic W5 RunId.

No identity from a prior attempt may be reused.

## PowerShell gate

Target exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Before wrapper invocation:
- record actual PowerShell version;
- parser-check the complete corrected harness;
- require zero parser errors.

A harness-only pre-wrapper failure grants no acceptance credit and is not a production defect. Correct only the disposable harness and retry with another fresh identity.

## Canonical W5 production result

The real exact-byte production wrapper must derive:

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Do not manufacture these states or clone production policy into the harness.

## Exact-byte contract

Copy the current production wrapper byte-for-byte and prove:
- source/current wrapper SHA256;
- sandbox pre-execution SHA256;
- sandbox post-execution SHA256;
- required equality.

No production-byte mutation.

## P01–P20 contract

Emit each predicate individually with requirement, expected value, observed evidence, PASS/FAIL, and evidence location.

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

P19 MUST NOT be evaluated or emitted until cleanup has executed and its success has been verified.

P20 must prove:
- `RETRIEVAL_FAILED + NOT_APPLICABLE` is eligible;
- checkpoint remains PASS;
- RestoreOnly executes exactly once;
- no later lifecycle/restoration condition incorrectly replaces expected SUCCESS.

## Durable-ledger rule

Because P19 occurs after sandbox deletion:
- retain audit evidence outside the disposable sandbox before cleanup;
- after successful cleanup, append/finalize P19 in that retained ledger;
- do not recreate the disposable sandbox to write P19;
- retain enough evidence to audit P01–P20 and event ordering.

## Fail-closed boundary

Fail closed on external operations. Unexpected real external invocations must equal zero.

Forbidden:
- production source/helper changes;
- new production seams;
- direct manufacture of internal result states;
- staging/commit/push;
- PR/merge;
- Azure mutation;
- Docker/GHCR mutation;
- GitHub lifecycle mutation.

## Repository invariants

Prove:
```text
repository modified-path scope = unchanged
staged path count = 0
git diff --check = PASS
secret hygiene = PASS
```

Preserve the existing governed modified tracked-path scope; add no tracked modification.

## Stop conditions

Stop immediately if:
- exact-byte identity fails;
- real external invocation occurs;
- production result differs from canonical W5;
- production defect/exception appears;
- a governed predicate fails after wrapper execution;
- unauthorized state manipulation would be required;
- repository scope changes.

Do not remediate production source.

## Acceptance and execution boundary

W5 acceptance is granted only if **P01–P20 individually ALL_PASS**.

If all pass:

```text
W5 = GOVERNED PASS
NEXT HYBRID RESTART POINT = W6
```

Then **STOP AFTER W5**.

Do not execute W6/W7/W8.

## Publication/lifecycle boundary

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

## Required terminal markers

`RELEASE 1.12 WP04 — W5 P19 ORDERING-CORRECTED FRESH RERUN: PASS`
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
`RELEASE 1.12 WP04 — W5 SECRET HYGIENE: PASS`
`RELEASE 1.12 WP04 — W5 ERROR PRECEDENCE: PASS`
`RELEASE 1.12 WP04 — W5 DURABLE EVIDENCE RETENTION: PASS`
`RELEASE 1.12 WP04 — W5 SANDBOX CLEANUP: PASS`
`RELEASE 1.12 WP04 — W5 P19 EVALUATED AFTER VERIFIED CLEANUP: PASS`
`RELEASE 1.12 WP04 — W5 INDIVIDUAL ACCOUNTING PREDICATES P01-P20: ALL_PASS`
`RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED`
`RELEASE 1.12 WP04 — W1/W2/W3/W4 CARRY-FORWARD: AUTHORIZED`
`RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN`
`RELEASE 1.12 WP04 — NEXT HYBRID RESTART POINT: W6`
`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED`
`RELEASE 1.12 WP04 — TRACKED PRODUCTION MUTATIONS: 0`
`RELEASE 1.12 WP04 — EXTERNAL MUTATIONS: 0`
`RELEASE 1.12 WP04 — TERRA W5 P19 ORDERING CORRECTION AND FRESH RERUN COMPLETE`

# GPT-5.6 Luna — Release 1.12 WP04 W3 Harness Aggregate-Assertion Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: reconcile the W3 harness-only aggregate assertion failure and decide whether W3 may be rerun without production remediation.
- **GPT-5.6 Terra** — may execute a later Luna-authorized harness correction/rerun.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

## Binding state

Post-W3-remediation Hybrid Revised-V2 restarted at W3 with:

```text
W1 = PASS — carry-forward authorized
W2 = PASS — carry-forward authorized
W4 = PASS — carry-forward authorized
W3 = NOT YET ACCEPTED
W5/W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
```

The latest W3 run correctly stopped before W5.

## Proven production-derived W3 records

The exact corrected production wrapper derived:

```text
ArchiveRetrieval = RETAINED
FreshExtraction = FAIL
EvidenceCheckpoint = FAIL
FinalLifecycleResult = EVIDENCE_PRESERVATION_FAILED
RestoreOnly = 1
```

All external operations were intercepted; no real external operation occurred.

Retained evidence:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-correlation-bc888e2dd0d147369abe65a7b5ed7cdb
```

These records demonstrate that the operation-scoped `Expand-Archive -ErrorAction Stop` remediation exercised the intended production failure path.

However, the disposable harness's final aggregate assertion failed. Therefore W3 acceptance credit remains **NOT_GRANTED** until all authority-required accounting assertions are proven as one completed scenario.

## Reconciliation task

Perform read-only inspection of:
1. the retained W3 evidence;
2. the disposable harness and its final aggregate assertion;
3. scenario logs/accounting produced before the aggregate assertion;
4. the exact-byte/hash evidence;
5. repository-invariant, external-call, cleanup, and lifecycle assertions expected by the active Hybrid Revised-V2 contract.

Identify the **exact failed aggregate predicate**.

Classify whether it is:
- a harness assertion/aggregation defect;
- missing harness evidence/accounting;
- a real production-wrapper defect;
- or an authority-required invariant that actually failed.

Do not infer PASS merely because the five production-derived records are correct.

## Required distinction

Keep these separate:

```text
PRODUCTION W3 FUNCTIONAL RESULT = EXPECTED
W3 GOVERNED ACCEPTANCE = NOT_GRANTED
```

A harness-only aggregate assertion failure does not authorize source remediation.

## Decision

Select exactly one:

### H1 — HARNESS-ONLY ASSERTION/ACCOUNTING DEFECT

Use only if:
- production wrapper output is correct;
- exact-byte identity remains valid;
- no real external invocation occurred;
- repository/source invariants remain valid;
- the failed predicate is confined to disposable harness assertion/accounting.

Then authorize a narrow Terra harness correction and **fresh W3 rerun only**.

No production-source modification.

### H2 — MISSING GOVERNED EVIDENCE REQUIRES FRESH W3 RERUN

Use if production behavior is correct but the existing run failed to capture enough evidence to establish one or more required invariants, without proving a harness defect.

Authorize fresh W3 rerun with corrected evidence capture only.

### P1 — PRODUCTION OR GOVERNED INVARIANT FAILURE

Use if the aggregate failure reveals an actual production defect or a required invariant failure.

Do not authorize W3 rerun as a mere harness correction. Require a new Luna defect reconciliation.

## Harness correction boundary

If H1/H2 is selected:
- disposable harness only;
- production wrapper bytes unchanged;
- no new production seam;
- no helper/source changes;
- no internal-state manufacture;
- fresh sandbox/scenario identity;
- fresh synthetic RunId;
- exact-byte pre/post hash proof;
- fail-closed external interception;
- durable sanitized evidence;
- prove every aggregate predicate individually before computing final W3 result;
- cleanup only after durable evidence is retained.

The corrected harness must make the failed predicate visible in its report rather than collapsing unexplained into a single aggregate false.

## W3 rerun boundary

If H1/H2:
- execute **W3 only** first;
- do not run W5 until W3 produces a complete governed PASS;
- prior W3 production-derived records may support diagnosis but grant no acceptance credit;
- W1/W2/W4 remain carry-forward unless reconciliation discovers evidence invalidating them.

## Mutation boundary

This Luna authority is read-only:

```text
tracked source mutations = 0
staging = 0
commit/push = 0
Azure = 0
Docker/GHCR = 0
Git/GitHub lifecycle = 0
```

## Required output

Return:
- exact aggregate assertion expression/predicate set;
- exact failed predicate(s);
- whether the predicate is harness-only, missing evidence, or production/invariant failure;
- exact-byte status;
- real-external-call status;
- repository-invariant status;
- lifecycle/accounting status;
- selected H1/H2/P1 decision;
- exact correction if H1/H2;
- whether W3 fresh rerun is authorized;
- whether W5 remains blocked;
- zero-mutation audit.

## Terminal markers

`RELEASE 1.12 WP04 — W3 HARNESS AGGREGATE-ASSERTION RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — W3 PRODUCTION FUNCTIONAL RESULT: EXPECTED`

`RELEASE 1.12 WP04 — W3 GOVERNED ACCEPTANCE: NOT_GRANTED`

`RELEASE 1.12 WP04 — FAILED AGGREGATE PREDICATE: <value>`

`RELEASE 1.12 WP04 — FAILURE CLASSIFICATION: <HARNESS_ONLY|MISSING_EVIDENCE|PRODUCTION_OR_INVARIANT>`

`RELEASE 1.12 WP04 — EXACT-BYTE IDENTITY: <PASS|FAIL|NOT_PROVEN>`

`RELEASE 1.12 WP04 — REAL EXTERNAL INVOCATIONS: <0|value|NOT_PROVEN>`

`RELEASE 1.12 WP04 — REPOSITORY INVARIANTS: <PASS|FAIL|NOT_PROVEN>`

`RELEASE 1.12 WP04 — RESTORE-ONLY COUNT: <1|value|NOT_PROVEN>`

`RELEASE 1.12 WP04 — RECONCILIATION DECISION: <H1|H2|P1>`

`RELEASE 1.12 WP04 — PRODUCTION SOURCE REMEDIATION AUTHORIZED: NO`

`RELEASE 1.12 WP04 — W1 CARRY-FORWARD: <AUTHORIZED|REVOKED>`

`RELEASE 1.12 WP04 — W2 CARRY-FORWARD: <AUTHORIZED|REVOKED>`

`RELEASE 1.12 WP04 — W4 CARRY-FORWARD: <AUTHORIZED|REVOKED>`

`RELEASE 1.12 WP04 — FRESH W3 RERUN: <AUTHORIZED|NOT_AUTHORIZED>`

`RELEASE 1.12 WP04 — W5/W6/W7/W8: NOT_RUN`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED`

`RELEASE 1.12 WP04 — TRACKED SOURCE MUTATIONS: 0`

`RELEASE 1.12 WP04 — EXTERNAL MUTATIONS: 0`

`RELEASE 1.12 WP04 — W3 HARNESS RECONCILIATION MUTATION AUDIT: PASS`

If H1/H2:

`RELEASE 1.12 WP04 — TERRA W3 HARNESS CORRECTION AND FRESH RERUN AUTHORITY: READY`

If P1:

`RELEASE 1.12 WP04 — LUNA W3 DEFECT RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA W3 HARNESS AGGREGATE-ASSERTION RECONCILIATION COMPLETE`

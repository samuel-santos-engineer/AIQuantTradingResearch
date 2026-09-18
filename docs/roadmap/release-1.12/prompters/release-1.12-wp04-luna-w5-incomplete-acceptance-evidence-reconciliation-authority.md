# GPT-5.6 Luna — Release 1.12 WP04 W5 Incomplete-Acceptance-Evidence Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: reconcile W5's completed production-wrapper execution against the governed W5 acceptance contract and determine the next validation boundary.
- **GPT-5.6 Terra** — may perform only a later Luna-authorized evidence correction/fresh rerun or other implementation/validation action.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never replaces Luna/Terra.

## Binding state

```text
W1 = PASS — preserved
W2 = PASS — preserved
W3 = PASS — governed fresh acceptance
W4 = PASS — authorized carry-forward
W5 production wrapper execution = COMPLETED
W5 production lifecycle result = EXPECTED
W5 individual acceptance evidence = INCOMPLETE
W5 governed acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
```

The Terra W5–W8 continuation authority correctly stopped before W6. Do not grant W5 acceptance merely because the lifecycle result was expected.

## Mission

Perform a read-only reconciliation of the completed W5 run. Determine:
1. the canonical governed W5 scenario contract;
2. every acceptance predicate required for W5;
3. which predicates the completed run proves;
4. which are missing, ambiguous, or failed;
5. whether missing evidence is solely disposable-harness/accounting deficiency;
6. whether any production-wrapper defect or governed invariant failure is proven;
7. whether retained W5 evidence can directly close every missing predicate without rerunning;
8. otherwise, whether a fresh W5 rerun with corrected disposable instrumentation is required.

Do **not** silently substitute the W3 P01–P14 matrix for W5. Derive W5 predicates from the existing Hybrid Revised-V2 W5 definition and actual retained W5 evidence.

## Evidence to inspect

Inspect, where available:
- active Terra Hybrid Revised-V2 W5–W8 continuation authority;
- earlier Hybrid Revised-V2 scenario definition establishing W5 semantics;
- W5 disposable harness and output;
- exact-byte/hash records;
- production-derived lifecycle/state records;
- external-call interception ledger;
- repository-state evidence;
- RestoreOnly accounting where governed;
- secret-hygiene evidence;
- durable-evidence retention and cleanup records;
- retained W5 scenario ledger.

Prefer retained evidence over recollection. Do not mutate or rerun anything under Luna.

## Required W5 predicate ledger

Construct:

```text
Predicate ID | Governed requirement | Observed evidence | Status | Evidence location
```

Statuses:

```text
PASS | FAIL | NOT_PROVEN | NOT_APPLICABLE
```

Use `NOT_APPLICABLE` only when the canonical W5 contract makes it inapplicable.

Cover every W5 requirement, including as applicable:
- exact-byte pre/post identity;
- W5 production-derived archive/extraction/checkpoint states;
- expected final lifecycle result;
- RestoreOnly count;
- unexpected real external invocation count;
- repository modified-path scope;
- staged path count;
- git diff --check;
- secret hygiene;
- durable sanitized evidence retention;
- sandbox cleanup;
- W5-specific lifecycle/error-precedence predicates.

## Critical distinction

```text
W5 PRODUCTION FUNCTIONAL RESULT = EXPECTED
W5 GOVERNED ACCEPTANCE = NOT_GRANTED
```

until every governed predicate is proven.

## Decision

Select exactly one:

### E1 — EXISTING RETAINED EVIDENCE IS COMPLETE WHEN RECONCILED
Use only if every W5 predicate is directly proven by retained evidence with unambiguous provenance. Luna may grant W5 acceptance without rerun.

### E2 — HARNESS/EVIDENCE ACCOUNTING INCOMPLETE; FRESH W5 RERUN REQUIRED
Use when production behavior is expected, no production/invariant defect is proven, but one or more predicates remain `NOT_PROVEN`. Authorize disposable-harness evidence correction and **fresh W5 only**.

### E3 — PRODUCTION OR GOVERNED INVARIANT FAILURE
Use when evidence proves an actual production-wrapper defect or required invariant failure. Require a new Luna defect reconciliation.

### E4 — CANONICAL W5 CONTRACT AMBIGUOUS/INSUFFICIENT
Use only if existing authorities cannot define W5 acceptance without inventing policy. Require Luna W5 contract re-governance.

## E2 execution boundary

If E2 is selected, later Terra must:
- change disposable harness/evidence instrumentation only;
- emit every canonical W5 predicate individually before aggregation;
- parser-check under Windows PowerShell 5.1.26100.9444;
- use fresh sandbox/scenario identity and fresh synthetic RunId;
- copy current wrapper exact bytes and prove pre/post hash equality;
- fail closed on all external operations;
- exercise real production wrapper;
- never manufacture internal production states;
- retain durable sanitized evidence before cleanup;
- grant W5 acceptance only if every required predicate passes;
- stop after W5.

W6 remains blocked until W5 has governed acceptance.

## Carry-forward

Unless contradictory evidence is discovered:

```text
W1 = PASS — CARRY FORWARD
W2 = PASS — CARRY FORWARD
W3 = PASS — CARRY FORWARD
W4 = PASS — CARRY FORWARD
```

Do not rerun W1–W4.

## Mutation boundary

Read-only:

```text
tracked source mutations = 0
staging = 0
commit/push = 0
PR/merge = 0
Azure = 0
Docker/GHCR = 0
GitHub lifecycle = 0
```

No production remediation is authorized.

## Publication/lifecycle boundary

```text
PUBLICATION BLOCKER = UNRESOLVED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
NEW AZURE DIAGNOSTIC/ACCEPTANCE RUN = NOT_AUTHORIZED
REOPEN/REDEPLOY = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
MILESTONE #63 = OPEN
```

## Required output

Return:
- canonical W5 scenario definition;
- complete predicate ledger;
- proven/missing/failed predicates;
- production lifecycle-result classification;
- whether a production defect is proven;
- whether retained evidence alone suffices;
- E1/E2/E3/E4;
- exact next authority;
- W1–W4 carry-forward;
- W6–W8 block status;
- zero-mutation audit.

## Terminal markers

`RELEASE 1.12 WP04 — W5 INCOMPLETE-ACCEPTANCE-EVIDENCE RECONCILIATION: PASS`
`RELEASE 1.12 WP04 — W5 PRODUCTION FUNCTIONAL RESULT: <EXPECTED|UNEXPECTED|NOT_PROVEN>`
`RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE BEFORE RECONCILIATION: NOT_GRANTED`
`RELEASE 1.12 WP04 — W5 CANONICAL PREDICATE COUNT: <value>`
`RELEASE 1.12 WP04 — W5 PROVEN PREDICATE COUNT: <value>`
`RELEASE 1.12 WP04 — W5 NOT-PROVEN PREDICATE COUNT: <value>`
`RELEASE 1.12 WP04 — W5 FAILED PREDICATE COUNT: <value>`
`RELEASE 1.12 WP04 — W5 MISSING PREDICATES: <value>`
`RELEASE 1.12 WP04 — W5 PRODUCTION DEFECT PROVEN: <YES|NO>`
`RELEASE 1.12 WP04 — EXISTING EVIDENCE SUFFICIENT FOR W5 ACCEPTANCE: <YES|NO>`
`RELEASE 1.12 WP04 — RECONCILIATION DECISION: <E1|E2|E3|E4>`
`RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE AFTER RECONCILIATION: <GRANTED|NOT_GRANTED>`
`RELEASE 1.12 WP04 — W1 CARRY-FORWARD: <AUTHORIZED|REVOKED>`
`RELEASE 1.12 WP04 — W2 CARRY-FORWARD: <AUTHORIZED|REVOKED>`
`RELEASE 1.12 WP04 — W3 CARRY-FORWARD: <AUTHORIZED|REVOKED>`
`RELEASE 1.12 WP04 — W4 CARRY-FORWARD: <AUTHORIZED|REVOKED>`
`RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN`
`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED`
`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`
`RELEASE 1.12 WP04 — TRACKED SOURCE MUTATIONS: 0`
`RELEASE 1.12 WP04 — EXTERNAL MUTATIONS: 0`
`RELEASE 1.12 WP04 — W5 RECONCILIATION MUTATION AUDIT: PASS`

If E1:
`RELEASE 1.12 WP04 — NEXT HYBRID RESTART POINT: W6`
`RELEASE 1.12 WP04 — TERRA HYBRID REVISED-V2 W6-W8 CONTINUATION AUTHORITY: READY`

If E2:
`RELEASE 1.12 WP04 — FRESH W5 RERUN: AUTHORIZED`
`RELEASE 1.12 WP04 — TERRA W5 HARNESS EVIDENCE CORRECTION AND FRESH RERUN AUTHORITY: READY`

If E3:
`RELEASE 1.12 WP04 — LUNA W5 DEFECT RECONCILIATION AUTHORITY: READY`

If E4:
`RELEASE 1.12 WP04 — LUNA W5 CONTRACT RE-GOVERNANCE AUTHORITY: READY`

Final:
`RELEASE 1.12 WP04 — LUNA W5 INCOMPLETE-ACCEPTANCE-EVIDENCE RECONCILIATION COMPLETE`

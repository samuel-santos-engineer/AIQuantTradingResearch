# Codex Prompt — Execute Release 1.12 WP04 Fresh W5 Rerun

Continue execution under the loaded:

**GPT-5.6 Terra — Release 1.12 WP04 W5 Harness Evidence Correction & Fresh Rerun Authority**

Execute the authorized fresh W5 rerun now.

Do not plan or merely describe the execution. Perform the validation.

## Binding requirements

1. Create a **NEW** disposable W5 scenario identity.
2. Generate a **NEW** fresh synthetic RunId at execution time.
3. Do not reuse any previous W3/W5 scenario identity or RunId.
4. Construct the complete W5 P01–P20 disposable validation harness required by the loaded authority.
5. Target exactly **Windows PowerShell 5.1.26100.9444**.
6. Before invoking the wrapper:
   - record the actual PowerShell version;
   - parser-check the complete harness under Windows PowerShell 5.1;
   - require 0 parser errors.
7. Copy the current production wrapper byte-for-byte into the disposable sandbox.
8. Record and prove the required source/pre/post SHA256 identities.
9. Execute the **REAL exact-byte production wrapper** under the governed W5 retrieval-failure fixture.
10. The production wrapper—not the harness—must derive the lifecycle states.

## Canonical W5 production-derived result

```text
ArchiveRetrieval = RETRIEVAL_FAILED
FreshExtraction = NOT_APPLICABLE
EvidenceCheckpoint = PASS
FinalLifecycleResult = SUCCESS
RestoreOnly count = 1
Unexpected real external invocations = 0
```

Emit **EVERY P01–P20 predicate individually BEFORE aggregate evaluation**.

For every predicate print:

```text
Predicate ID
Governed requirement
Expected value
Observed value/evidence
PASS/FAIL
Evidence location
```

## Required predicates

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

For P20 explicitly prove that:

- `RETRIEVAL_FAILED + NOT_APPLICABLE` is eligible under the governed state machine;
- `EvidenceCheckpoint` remains `PASS`;
- `RestoreOnly` executes exactly once;
- no later lifecycle/restoration condition incorrectly replaces the expected `SUCCESS` result.

## Fail-closed and production-boundary requirements

Fail closed on all external operations.

Do not manufacture `ArchiveRetrieval`, `FreshExtraction`, `EvidenceCheckpoint`, `FinalLifecycleResult`, or `RestoreOnly` results in the harness.

Do not clone production policy into the harness.

Do not modify production source or helper code.

Do not stage, commit, push, create/merge a PR, mutate Azure, mutate Docker/GHCR, or perform GitHub lifecycle mutations.

Retain a sanitized durable evidence root sufficient to audit P01–P20 before disposable cleanup.

## Stop conditions

- If a harness construction/parser problem occurs before wrapper invocation, it grants no W5 acceptance credit and is not a production defect. Correct only the disposable harness within this authority and retry using another fresh scenario identity/RunId.
- If wrapper execution begins and any governed predicate fails, stop on that exact predicate.
- If exact-byte identity fails, stop.
- If any unexpected real external invocation occurs, stop.
- If production behavior differs from the canonical W5 result, stop.
- If a production defect appears, stop without remediation.
- If satisfying W5 would require unauthorized internal-state manipulation, stop.

W5 acceptance may be granted **ONLY if P01–P20 are individually ALL_PASS**.

## Required success markers

If P01–P20 all pass, emit all terminal markers required by the loaded authority, including:

```text
RELEASE 1.12 WP04 — W5 FRESH RERUN: PASS
RELEASE 1.12 WP04 — W5 INDIVIDUAL ACCOUNTING PREDICATES P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED
RELEASE 1.12 WP04 — W1/W2/W3/W4 CARRY-FORWARD: AUTHORIZED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — NEXT HYBRID RESTART POINT: W6
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TRACKED PRODUCTION MUTATIONS: 0
RELEASE 1.12 WP04 — EXTERNAL MUTATIONS: 0
RELEASE 1.12 WP04 — TERRA W5 HARNESS EVIDENCE CORRECTION AND FRESH RERUN COMPLETE
```

## Final execution boundary

**STOP AFTER W5.**

Do not execute W6, W7, or W8 even if W5 passes.

**Begin execution now.**

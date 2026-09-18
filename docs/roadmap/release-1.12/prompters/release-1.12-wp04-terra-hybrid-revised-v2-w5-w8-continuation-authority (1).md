# GPT-5.6 Terra — Release 1.12 WP04 Hybrid Revised-V2 W5–W8 Continuation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract/policy/reconciliation authority; W1/W2/W4 carry-forward and W3 restart were authorized.
- **GPT-5.6 Terra** — PRIMARY: execute the remaining exact-byte sandbox scenarios W5–W8.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

## Binding ledger

Fresh W3 has governed acceptance:

```text
W1 = PASS — carry forward
W2 = PASS — carry forward
W3 = PASS — fresh governed acceptance
W4 = PASS — authorized policy carry-forward
W5 = NEXT
W6 = PENDING
W7 = PENDING
W8 = PENDING
```

Fresh W3 RunId:

```text
initialize-w3-cd08d2233c41405287f66f00069ff605
```

Retain its durable ledger:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04-post-remediation-hybrid-v2-ledger\1d3550063962489c870ea7db5ae7a9e9
```

Do not rerun W1–W4.

## Execution sequence

Execute exactly:

```text
W5 -> W6 -> W7 -> W8
```

Stop immediately at the first authority stop condition. Do not remediate source under this validation authority.

## Binding environment

Use:

```text
Windows PowerShell 5.1.26100.9444
```

For each scenario:
- fresh disposable sandbox/scenario identity;
- fresh synthetic RunId;
- copy current production wrapper byte-for-byte;
- verify pre/post SHA256 equality;
- verify accepted `.ToArray()` and `Expand-Archive -ErrorAction Stop` remediations remain present;
- execute actual copied wrapper;
- fail-closed intercept all external operations;
- retain sanitized durable evidence before cleanup;
- remove only disposable scenario artifacts afterward.

## Harness discipline

Run a Windows PowerShell 5.1 parser check against each disposable harness before wrapper invocation and require zero parser errors.

The harness must expose required accounting predicates individually before aggregate evaluation. An unexplained aggregate false is not acceptance evidence.

Harness-only failures before wrapper invocation grant no scenario acceptance credit and do not by themselves establish a production defect.

## State-manipulation boundary

Synthetic boundary inputs are allowed; production policy/state derivation remains owned by the exact-byte wrapper.

Forbidden:
- production-byte mutation;
- direct manufacture of archive/extraction/checkpoint/final states;
- policy clones replacing wrapper behavior;
- broad command overrides used to force internal state.

For W7 only, the already-governed S2 `PersistEvidenceCheckpointCallback` fault-injection seam is authorized.

No new production seam is authorized.

## Scenario contracts

Execute W5, W6, W7, and W8 according to their existing Hybrid Revised-V2 governed scenario definitions and expected state/result matrix. Do not reinterpret or weaken any expected result.

For each scenario prove all authority-required:
- production-derived states;
- lifecycle/final-result semantics;
- RestoreOnly count where governed;
- exact-byte pre/post identity;
- unexpected real external invocations = 0;
- repository scope unchanged;
- staged count = 0;
- `git diff --check` PASS;
- secret hygiene PASS;
- durable sanitized evidence retained;
- disposable sandbox cleanup PASS.

## Repository/external mutation boundary

No tracked source modification is authorized.

```text
staging = 0
commit/push = 0
PR/merge = 0
Azure = 0
Docker/GHCR = 0
GitHub lifecycle = 0
```

Preserve the existing two governed modified tracked WP04 paths exactly; do not add another modified tracked path.

## Stop conditions

Stop immediately if:
- exact-byte identity fails;
- any real external invocation occurs;
- a new production-source defect/exception appears;
- a scenario requires unauthorized internal-state manipulation;
- repository scope changes;
- a governed predicate fails;
- a scenario cannot be naturally induced within the existing authority.

On stop, preserve prior evidence and the failing scenario's sanitized evidence, report the deepest proven boundary, and do not execute later scenarios.

## Aggregate completion rule

Only if W5, W6, W7, and W8 all pass may Terra combine them with W1–W4 and emit:

```text
W1-W8 = GOVERNED PASS
PUBLICATION BLOCKER = RESOLVED_PENDING_LUNA_FINAL_RECONCILIATION
```

This does not authorize publication, Azure acceptance/reopen/redeploy, source publication, WP04 closure, or WP05.

## Required final markers

`RELEASE 1.12 WP04 — HYBRID REVISED-V2 W5-W8 CONTINUATION: PASS`

`RELEASE 1.12 WP04 — W1: PASS_CARRY_FORWARD`

`RELEASE 1.12 WP04 — W2: PASS_CARRY_FORWARD`

`RELEASE 1.12 WP04 — W3: PASS`

`RELEASE 1.12 WP04 — W4: PASS_CARRY_FORWARD`

`RELEASE 1.12 WP04 — W5: PASS`

`RELEASE 1.12 WP04 — W6: PASS`

`RELEASE 1.12 WP04 — W7: PASS`

`RELEASE 1.12 WP04 — W8: PASS`

`RELEASE 1.12 WP04 — EXACT-BYTE VALIDATION: PASS`

`RELEASE 1.12 WP04 — UNEXPECTED REAL EXTERNAL INVOCATIONS: 0`

`RELEASE 1.12 WP04 — REPOSITORY INVARIANTS: PASS`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE: PASS`

`RELEASE 1.12 WP04 — HYBRID W1-W8 GOVERNED EVIDENCE: COMPLETE`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: RESOLVED_PENDING_LUNA_FINAL_RECONCILIATION`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — NEW AZURE DIAGNOSTIC/ACCEPTANCE RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — WP04 #263: OPEN`

`RELEASE 1.12 WP04 — WP05: NOT_STARTED`

`RELEASE 1.12 WP04 — NEXT AUTHORITY: LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION`

`RELEASE 1.12 WP04 — TERRA HYBRID REVISED-V2 W5-W8 CONTINUATION COMPLETE`

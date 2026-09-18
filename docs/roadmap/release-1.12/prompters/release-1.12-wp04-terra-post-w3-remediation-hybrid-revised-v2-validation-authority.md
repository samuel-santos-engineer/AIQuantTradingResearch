# GPT-5.6 Terra — Release 1.12 WP04 Post-W3-Remediation Hybrid Revised-V2 Validation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract/policy/reconciliation authority; has authorized restart at W3 with W1/W2/W4 carry-forward.
- **GPT-5.6 Terra** — PRIMARY: execute remaining exact-byte sandbox validation.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

## Binding carry-forward

Luna post-W3-remediation reconciliation passed:

```text
W1 = PASS — CARRY FORWARD; DO NOT RERUN
W2 = PASS — CARRY FORWARD; DO NOT RERUN
W3 = NO ACCEPTANCE CREDIT — EXECUTE NOW
W4 = PASS — AUTHORIZED POLICY CARRY-FORWARD; DO NOT MANUFACTURE THROUGH WRAPPER
W5 = PENDING
W6 = PENDING
W7 = PENDING
W8 = PENDING
```

Current production correction is operation-scoped:

```powershell
Expand-Archive ... -Force -ErrorAction Stop
```

Successful/empty extraction semantics are reconciled as preserved.

## Execution sequence

Execute sequentially:

```text
W3 -> W5 -> W6 -> W7 -> W8
```

Do not skip a scenario. Stop immediately at the first authority stop condition.

## Environment and exact-byte boundary

Use exactly:

```text
Windows PowerShell 5.1.26100.9444
```

For each scenario:
1. create a fresh unique disposable sandbox under the existing WP04 post-remediation validation temp convention;
2. copy the current production wrapper byte-for-byte;
3. prove source SHA256 equals sandbox SHA256 before execution;
4. prove the copied wrapper contains both accepted remediations:
   - `return $observations.ToArray()`
   - operation-scoped `Expand-Archive ... -ErrorAction Stop`;
5. execute the actual copied wrapper, not a rewritten policy clone;
6. prove source SHA256 equals sandbox SHA256 after execution;
7. retain sanitized durable scenario evidence before cleanup;
8. clean only disposable scenario artifacts.

## External interception

Fail closed.

Intercept every external operation required by the wrapper, including `git`, `az`, helper invocation, and any other external executable path. Synthetic helper/external results may supply boundary inputs only; the production wrapper must own policy/state derivation.

```text
REAL EXTERNAL INVOCATIONS = 0
```

No pass-through.

Use fresh synthetic RunIds. Never reuse any historical Azure qualification RunId.

## State-manipulation boundary

Forbidden:
- changing production bytes in the sandbox;
- injecting final archive/extraction/checkpoint/lifecycle classifications directly;
- replacing production policy with harness policy;
- manufacturing success/failure markers;
- overriding broad production commands merely to force internal state.

Allowed:
- disposable fixture creation;
- fail-closed external-boundary stubs;
- W7 only: the already-governed S2 `PersistEvidenceCheckpointCallback` fault-injection seam.

If a scenario cannot be naturally induced under these rules, STOP.

## W3

Exercise a genuine post-retention extraction failure using a fresh invalid-entry archive fixture that reaches the real production `Expand-Archive`.

Required production-derived result:

```text
Archive = RETAINED
Extraction = FAIL
Final = EVIDENCE_PRESERVATION_FAILED
RestoreOnly = 1
Real external calls = 0
```

The failure must be caught because `Expand-Archive` is terminating via operation-scoped `-ErrorAction Stop`.

## W5/W6/W7/W8

Execute each scenario exactly according to the active Hybrid Revised-V2 scenario contract already established for WP04. Preserve the original expected state/result matrix and lifecycle assertions. Do not reinterpret a scenario merely to obtain PASS.

For W7, use only the S2 callback seam for checkpoint-persistence fault injection.

For W5/W6/W8, no internal-state injection seam is authorized.

Require `RestoreOnly = 1` for every scenario for which the existing Hybrid Revised-V2 contract requires restoration.

## Repository invariants

Before and after each scenario prove:
- only the existing governed WP04 modified tracked paths are present;
- staged path count = 0;
- no unauthorized tracked path appears;
- `git diff --check` passes;
- no secret material is emitted or persisted.

No staging, commit, push, PR, merge, issue/milestone/project mutation.

No Azure, Docker, or GHCR mutation.

## Stop conditions

STOP immediately if:
- exact-byte identity fails;
- a real external call occurs;
- a new production-source exception/defect appears;
- a scenario requires unauthorized internal-state manipulation;
- production source changes during validation;
- repository scope changes unexpectedly;
- a scenario result differs from its governed contract.

On stop:
- preserve all prior passing evidence;
- preserve sanitized evidence for the failing scenario;
- report the deepest proven boundary;
- do not remediate under this authority;
- do not execute later scenarios.

## Aggregate completion rule

Only if W3, W5, W6, W7, and W8 all pass may Terra combine them with carried-forward W1/W2/W4 and report:

```text
W1-W8 = GOVERNED PASS
PUBLICATION BLOCKER = RESOLVED_PENDING_LUNA_FINAL_RECONCILIATION
```

This does **not** authorize publication, Azure acceptance, reopen/redeploy, WP04 closure, or WP05.

## Required scenario reporting

For every executed scenario report:
- fresh scenario identity/RunId;
- exact-byte pre/post hash result;
- archive/extraction/checkpoint states as applicable;
- RestoreOnly count;
- final lifecycle result;
- unexpected real external invocation count;
- durable sanitized evidence location;
- cleanup result;
- repository invariant result.

## Final markers — only after all remaining scenarios pass

`RELEASE 1.12 WP04 — POST-W3-REMEDIATION HYBRID REVISED-V2 VALIDATION: PASS`

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

`RELEASE 1.12 WP04 — TERRA POST-W3-REMEDIATION HYBRID REVISED-V2 VALIDATION COMPLETE`

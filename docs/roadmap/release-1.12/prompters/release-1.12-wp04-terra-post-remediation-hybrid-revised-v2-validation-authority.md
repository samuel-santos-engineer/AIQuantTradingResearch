# GPT-5.6 Terra — Release 1.12 WP04 Post-Remediation Hybrid Revised-V2 Validation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — owns the reconciled validation contract, acceptance criteria, and final pre-publication reconciliation.
- **GPT-5.6 Terra** — PRIMARY: execute local-only post-remediation hybrid validation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never replaces Luna/Terra.

## 1. Binding state

The PowerShell 5.1 poll-observation defect is reconciled as remediated.

Current production wrapper contains:

```powershell
return $observations.ToArray()
```

in `Get-Wp04PollObservations`.

P1-P5 passed under Windows PowerShell 5.1. W4 regression is none. W4 carry-forward is authorized.

The prior failed W1 attempt grants no acceptance credit.

Hybrid validation restarts at W1.

## 2. Hybrid partition

Execute:

```text
W1,W2,W3,W5,W6,W7,W8
  -> exact-byte sandbox execution of CURRENT POST-REMEDIATION wrapper

W4
  -> carry-forward from reconciled actual production-policy validation
```

Do not manufacture W4 through wrapper state injection.

## 3. Absolute mutation boundary

This authority permits:

```text
tracked source changes = 0
staged paths = 0
commits/pushes = 0
PRs/merges = 0
Azure mutations = 0
Docker/GHCR mutations = 0
GitHub lifecycle mutations = 0
```

If another production-source defect is found, STOP and report it. Do not repair it under this authority.

## 4. Windows PowerShell baseline

All wrapper validation must use:

```text
Windows PowerShell 5.1.26100.9444
```

Record the actual version.

## 5. Exact-byte sandbox

Create a unique non-repository sandbox:

```text
$env:TEMP\AIQuantTradingResearch\wp04-post-remediation-hybrid-v2\<validation-id>
```

Copy current:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

byte-for-byte.

Before execution:

```text
SHA256(tracked wrapper) == SHA256(sandbox wrapper)
```

After all scenarios, recheck both hashes and require equality.

The copied bytes must include the reconciled:

```powershell
return $observations.ToArray()
```

## 6. Fail-closed external interception

Intercept every external command used by the wrapper, including at minimum:

```text
git
az
helper
```

Inspect current wrapper for any additional external command and intercept it too.

Rules:

```text
no real pass-through
unexpected external invocation -> scenario failure
real external invocation count = 0
```

Retain an invocation ledger.

## 7. Synthetic helper requirements

Synthetic helper may provide external results only:

```text
Deferred
RestoreOnly
LifecycleAction=None
terminal telemetry
poll observations
D3 state
synthetic wrapper-owned actual RunId
```

It must record invocations.

Preserve RB2.

Use fresh synthetic IDs; never reuse historical Azure RunIds.

The mock must not decide:
- archive/extraction truth table;
- checkpoint eligibility;
- error precedence;
- final lifecycle result;
- RestoreOnly ordering/count.

Those decisions must remain in the exact production wrapper.

## 8. W1 — retained/pass success

Run exact wrapper.

Require:

```text
poll observations parsed successfully through remediated Get-Wp04PollObservations
RETAINED + PASS
checkpoint = PASS
RestoreOnly count = 1
final lifecycle = success
```

If the prior `ArgumentException` recurs, fail immediately.

## 9. W2 — retained/empty

Require:

```text
RETAINED + EMPTY
checkpoint eligible/PASS
RestoreOnly count = 1
```

## 10. W3 — retained/extraction failure

Through permitted synthetic external/filesystem inputs, cause a real extraction exception after archive retention.

Production wrapper must normalize:

```text
RETAINED + FAIL
```

Require:

```text
final = EVIDENCE_PRESERVATION_FAILED
RestoreOnly count = 1
```

No internal state injection.

## 11. W4 — carry-forward

Do not execute W4 through sandbox lifecycle.

Carry forward reconciled proof:

```text
validation layer = ACTUAL_PRODUCTION_POLICY_FUNCTION
production reachability = R1_UNREACHABLE_DEFENSIVE
RETAINED + NOT_APPLICABLE
Eligible = False
Classification = EVIDENCE_STATE_INCONSISTENT
truth table = PASS
```

Record W4 as PASS by authorized carry-forward.

## 12. W5 — retrieval failure

Induce archive retrieval failure through the external shim.

Production wrapper must produce:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE
```

Require durable retrieval-failure evidence, correct eligibility/checkpoint behavior, and RestoreOnly count = 1.

## 13. W6 — qualification failure

Synthetic Deferred helper returns governed qualification failure.

Require:
- underlying qualification failure retained;
- evidence-preservation path follows production logic;
- correct final precedence;
- RestoreOnly count = 1.

## 14. W7 — checkpoint persistence failure

Use only the reconciled S2 seam:

```text
PersistEvidenceCheckpointCallback
```

Inject a throwing scriptblock.

Require:

```text
all prior evidence writes succeed
callback invocation count = 1
final = EVIDENCE_PRESERVATION_FAILED
underlying qualification result retained
RestoreOnly count = 1
```

Forbidden:

```text
global Set-Content override
unrelated filesystem-write interception
new seam
```

## 15. W8 — restoration failure

Synthetic `RestoreOnly` fails exactly once.

Require:

```text
RestoreOnly invocation count = 1
final = RESTORATION_FAILED
underlying qualification/evidence result retained
```

## 16. Cross-scenario lifecycle invariants

For W1,W2,W3,W5,W6,W7,W8 require:

```text
RestoreOnly exactly once
actual wrapper decides final result
RB2 preserved
no explicit Azure restart
pre-restoration evidence checkpoint ordering preserved
```

Required aggregate:

```text
RESTORE-ONLY EXACTLY-ONCE VALIDATION = PASS
ERROR PRECEDENCE = PROVEN
PRE-RESTORATION EVIDENCE CHECKPOINT = PROVEN_COMPLETE
ARCHIVE/EVIDENCE CONTRACT = PASS
```

## 17. Scenario ledger

Retain local evidence for each executed scenario:

```text
scenario
tracked wrapper SHA256
sandbox wrapper SHA256
PowerShell version
synthetic actual RunId
git shim ledger
az shim ledger
helper ledger
poll observations
archive state
extraction state
checkpoint state
deepest boundary
S2 callback count
RestoreOnly count
qualification result
evidence result
restoration result
final lifecycle result
exit code
unexpected real external invocation count
```

For W4 retain/cite the reconciled carry-forward evidence record.

## 18. Secret hygiene

Synthetic values only.

Do not access or persist real secrets.

Require:

```text
SECRET HYGIENE = PASS
REAL SECRET FINDINGS = 0
```

## 19. Working-tree audit

Before and after:

```text
git status --short
git diff --check
```

Expected governed modified tracked paths remain exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Require:

```text
new tracked source changes during validation = 0
implemented tracked path count = 2
unauthorized tracked path count = 0
staged path count = 0
git diff --check = PASS
```

## 20. Sandbox cleanup

After retaining required validation evidence:

```text
remove only the temporary sandbox/harness
```

Do not delete previously retained WP04 diagnostic evidence.

## 21. Stop conditions

STOP without continuing later scenarios if:
- exact-byte identity fails;
- real external command pass-through occurs;
- unexpected production-source exception appears;
- a required scenario cannot be induced without altering production bytes/internal state beyond S2;
- working-tree scope changes unexpectedly.

Report the deepest proven boundary and leave publication blocked.

## 22. Success consequence

Only if W1-W8 hybrid validation all pass:

```text
PUBLICATION BLOCKER = RESOLVED_PENDING_LUNA_FINAL_RECONCILIATION
```

Do not publish.

Next authority:

```text
LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY
```

## 23. Acceptance boundary

Preserve:

```text
NEW IMAGE REQUIRED = NO
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
RETAINED LOCAL EVIDENCE = PRESERVE
```

## 24. Required terminal markers on complete success

`RELEASE 1.12 WP04 — POST-REMEDIATION HYBRID REVISED-V2 VALIDATION: PASS`

`RELEASE 1.12 WP04 — SELECTED VALIDATION MODEL: POST_REMEDIATION_HYBRID_REVISED_V2`

`RELEASE 1.12 WP04 — TRACKED SOURCE CHANGES DURING VALIDATION: 0`

`RELEASE 1.12 WP04 — EXACT-BYTE POST-REMEDIATION WRAPPER IDENTITY: PASS`

`RELEASE 1.12 WP04 — PS5.1 POLL-OBSERVATION REMEDIATION PRESENT: PASS`

`RELEASE 1.12 WP04 — EXTERNAL OPERATION INTERCEPTION: PASS`

`RELEASE 1.12 WP04 — UNEXPECTED REAL EXTERNAL INVOCATIONS: 0`

`RELEASE 1.12 WP04 — W1 RESULT: PASS`

`RELEASE 1.12 WP04 — W2 RESULT: PASS`

`RELEASE 1.12 WP04 — W3 RESULT: PASS`

`RELEASE 1.12 WP04 — W4 RESULT: PASS`

`RELEASE 1.12 WP04 — W5 RESULT: PASS`

`RELEASE 1.12 WP04 — W6 RESULT: PASS`

`RELEASE 1.12 WP04 — W7 RESULT: PASS`

`RELEASE 1.12 WP04 — W8 RESULT: PASS`

`RELEASE 1.12 WP04 — W4 VALIDATION LAYER: ACTUAL_PRODUCTION_POLICY_FUNCTION_CARRY_FORWARD`

`RELEASE 1.12 WP04 — W4 PRODUCTION REACHABILITY: R1_UNREACHABLE_DEFENSIVE`

`RELEASE 1.12 WP04 — W4 ELIGIBLE: FALSE`

`RELEASE 1.12 WP04 — W4 CLASSIFICATION: EVIDENCE_STATE_INCONSISTENT`

`RELEASE 1.12 WP04 — W7 CHECKPOINT CALLBACK INVOCATION COUNT: 1`

`RELEASE 1.12 WP04 — RESTORE-ONLY EXACTLY-ONCE VALIDATION: PASS`

`RELEASE 1.12 WP04 — ERROR PRECEDENCE: PROVEN`

`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: PROVEN_COMPLETE`

`RELEASE 1.12 WP04 — ARCHIVE/EVIDENCE CONTRACT: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 HYBRID VALIDATION: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE VALIDATION: PASS`

`RELEASE 1.12 WP04 — FULL TRACKED DIFF SCOPE: PASS`

`RELEASE 1.12 WP04 — IMPLEMENTED TRACKED PATH COUNT: 2`

`RELEASE 1.12 WP04 — UNAUTHORIZED TRACKED PATH COUNT: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — SANDBOX CLEANUP: PASS`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: RESOLVED_PENDING_LUNA_FINAL_RECONCILIATION`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — POST-REMEDIATION HYBRID VALIDATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA POST-REMEDIATION HYBRID REVISED-V2 VALIDATION COMPLETE`

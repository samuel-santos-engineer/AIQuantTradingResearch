# GPT-5.6 Terra — Release 1.12 WP04 Revised Exact-Byte Sandbox Validation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — validation contract, acceptance criteria, reconciliation, publication decision.
- **GPT-5.6 Terra** — execute revised V2 local-only exact-byte validation.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

## Governed baseline
Repository: `C:\projects\github\AIQuantTradingResearch`

Target: `eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1`

PowerShell: `Windows PowerShell 5.1.26100.9444`

State: `BLOCKED_PENDING_REVISED_V2`

The current wrapper includes the reconciled S2 `PersistEvidenceCheckpointCallback` seam. Its production default and isolation are accepted.

## Absolute boundary
During this authority: 0 tracked edits, 0 staging/commit/push/PR/merge, 0 Azure mutations, 0 Docker/GHCR mutations, 0 GitHub lifecycle mutations. Do not modify either governed tracked script.

## Exact-byte sandbox
Create a unique non-repository sandbox under `$env:TEMP\AIQuantTradingResearch\wp04-revised-v2\<validation-id>`. Copy the **current tracked wrapper** byte-for-byte and only strictly necessary support files. No secrets.

Compute SHA-256 of tracked and sandbox wrappers before execution. They must be identical or STOP. Recompute the sandbox hash after W1–W8; it must remain identical.

## Fail-closed interception
Prepend temporary shims to sandbox `PATH`. Intercept at minimum `git`, `az`, and the helper surface. Inspect for any additional external commands and intercept them too. Unexpected calls fail closed and are safely logged; never pass through to real commands.

The helper mock may synthesize Deferred/RestoreOnly behavior, terminal telemetry, poll observations, D3 state, and unique synthetic RunIds. It records `RestorationMode`, `LifecycleAction`, Deferred count, RestoreOnly count, and emitted RunId. Preserve RB2. Never reuse historical Azure RunIds.

Mocks return external results only. They must not decide checkpoint truth, precedence, restoration ordering/count, or final lifecycle classification.

## W7 seam rule
Only W7 may supply a throwing `PersistEvidenceCheckpointCallback`. It must fail **only final checkpoint persistence**. Never globally override `Set-Content`. W1–W6/W8 use production-default checkpoint persistence.

## W1–W8
Execute the unmodified exact-byte sandbox wrapper under Windows PowerShell 5.1.

- **W1 success:** RETAINED + PASS; checkpoint PASS; qualification success; RestoreOnly=1.
- **W2 empty:** RETAINED + EMPTY; checkpoint eligible/PASS; RestoreOnly=1.
- **W3 extraction failure:** RETAINED + FAIL; checkpoint FAIL; final `EVIDENCE_PRESERVATION_FAILED`; RestoreOnly=1.
- **W4 inconsistent:** RETAINED + NOT_APPLICABLE; checkpoint FAIL; inconsistency retained; RestoreOnly=1.
- **W5 retrieval failure:** RETRIEVAL_FAILED + NOT_APPLICABLE; durable retrieval failure; checkpoint eligible/PASS when other gates pass; RestoreOnly=1.
- **W6 qualification failure:** qualification failure retained; governed evidence path executes; precedence correct; RestoreOnly=1.
- **W7 checkpoint failure:** all earlier writes succeed; S2 callback throws exactly once; final `EVIDENCE_PRESERVATION_FAILED`; qualification result retained; RestoreOnly=1.
- **W8 restoration failure:** RestoreOnly invoked exactly once and fails; final `RESTORATION_FAILED`; underlying qualification/evidence result retained.

## Scenario evidence
For every W1–W8 durably retain as needed for Luna reconciliation: scenario ID, tracked/sandbox hashes, PowerShell version, synthetic actual RunId, helper/az/git ledgers, archive/extraction/checkpoint states, deepest boundary, S2 callback count, RestoreOnly count, qualification/evidence/restoration/final results, exit code, and unexpected external invocation count.

Required globally: unexpected real external invocations=0. W7 callback count=1. W1–W6/W8 callback count=0/default. RestoreOnly=1 for every scenario.

## PowerShell 5.1
Require parser errors=0, wrapper parameter binding PASS, W7 callback binding PASS, and W1–W8 wrapper execution PASS under `Windows PowerShell 5.1.26100.9444`. No PS7-only behavior.

## Secret hygiene
Synthetic values only. Do not access/output real Twelve Data keys, evidence tokens, auth headers, registry credentials, connection strings, or cookies. Real secret findings must be 0.

## Working tree and cleanup
Record `git status --short` before/after. Require the same two governed modified tracked paths, no additional tracked modifications, and 0 staged paths. Run `git diff --check` => PASS.

After durable evidence is retained, delete only the temporary revised-V2 sandbox. Preserve historical retained diagnostic evidence.

## Publication/acceptance boundary
If W1–W8 pass: `PUBLICATION BLOCKER = RESOLVED_PENDING_LUNA_RECONCILIATION`.

Still forbidden: staging, commit, push, PR, merge, Azure diagnostic/acceptance retry, reopen, Docker/GHCR, GitHub lifecycle. `INITIALIZE D3 ACCEPTANCE = NOT_PROVEN`; #263 remains Open; WP05 remains not started; new image not required.

## Required terminal markers
`RELEASE 1.12 WP04 — REVISED EXACT-BYTE SANDBOX VALIDATION: PASS`
`RELEASE 1.12 WP04 — SELECTED VALIDATION MODEL: REVISED_V2`
`RELEASE 1.12 WP04 — TRACKED SOURCE CHANGES DURING VALIDATION: 0`
`RELEASE 1.12 WP04 — TRACKED WRAPPER SHA256: <value>`
`RELEASE 1.12 WP04 — SANDBOX WRAPPER SHA256 BEFORE: <value>`
`RELEASE 1.12 WP04 — SANDBOX WRAPPER SHA256 AFTER: <value>`
`RELEASE 1.12 WP04 — EXACT-BYTE WRAPPER IDENTITY: PASS`
`RELEASE 1.12 WP04 — EXTERNAL OPERATION INTERCEPTION: PASS`
`RELEASE 1.12 WP04 — UNEXPECTED REAL EXTERNAL INVOCATIONS: 0`
`RELEASE 1.12 WP04 — ACTUAL PRODUCTION CONTROL-FLOW VALIDATION: PASS`
`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 FULL WRAPPER LIFECYCLE W1-W8: PASS`
`RELEASE 1.12 WP04 — W1 RESULT: PASS`
`RELEASE 1.12 WP04 — W2 RESULT: PASS`
`RELEASE 1.12 WP04 — W3 RESULT: PASS`
`RELEASE 1.12 WP04 — W4 RESULT: PASS`
`RELEASE 1.12 WP04 — W5 RESULT: PASS`
`RELEASE 1.12 WP04 — W6 RESULT: PASS`
`RELEASE 1.12 WP04 — W7 RESULT: PASS`
`RELEASE 1.12 WP04 — W8 RESULT: PASS`
`RELEASE 1.12 WP04 — W7 CHECKPOINT CALLBACK INVOCATION COUNT: 1`
`RELEASE 1.12 WP04 — RESTORE-ONLY EXACTLY-ONCE VALIDATION: PASS`
`RELEASE 1.12 WP04 — ERROR PRECEDENCE: PROVEN`
`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: PROVEN_COMPLETE`
`RELEASE 1.12 WP04 — ARCHIVE/EVIDENCE CONTRACT: PASS`
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
`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: RESOLVED_PENDING_LUNA_RECONCILIATION`
`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`
`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`
`RELEASE 1.12 WP04 — REVISED EXACT-BYTE SANDBOX VALIDATION MUTATION AUDIT: PASS`
`RELEASE 1.12 WP04 — LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:
`RELEASE 1.12 WP04 — TERRA REVISED EXACT-BYTE SANDBOX VALIDATION COMPLETE`

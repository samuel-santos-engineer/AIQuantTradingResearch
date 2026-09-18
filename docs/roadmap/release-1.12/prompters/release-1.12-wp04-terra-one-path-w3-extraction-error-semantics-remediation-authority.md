# GPT-5.6 Terra — Release 1.12 WP04 One-Path W3 Extraction Error-Semantics Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract/policy/reconciliation authority. D1 has selected the correction below.
- **GPT-5.6 Terra** — PRIMARY: implement and validate the authorized one-path production remediation.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

## Binding Luna decision

W3 reconciliation passed and selected **D1**.

Exact defect:

```text
Operation:
Expand-Archive -LiteralPath $archivePath -DestinationPath $extractRoot -Force

Windows PowerShell 5.1 behavior:
invalid-entry extraction error is non-terminating by default;
surrounding try/catch therefore does not catch it.
```

Authorized correction:

```powershell
Expand-Archive -LiteralPath $archivePath -DestinationPath $extractRoot -Force -ErrorAction Stop
```

No global `$ErrorActionPreference` change. No new seam. No helper modification.

## Scope

Modify exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Expected tracked source change count for this remediation: **1 path**.

Do not modify the other governed WP04 helper path or any other tracked file.

## Implementation

Locate the exact production `Expand-Archive` operation reconciled by Luna and add only:

```powershell
-ErrorAction Stop
```

Preserve all other bytes/semantics except unavoidable formatting directly associated with that command.

Do not:
- alter archive/extraction truth-table policy;
- alter S2 checkpoint persistence seam;
- alter Deferred/RestoreOnly lifecycle;
- alter RB2 RunId ownership;
- alter restoration precedence;
- alter `Get-Wp04PollObservations` or its `.ToArray()` fix;
- add W3-specific production test hooks;
- change helper source;
- change README;
- change deployment/image/runtime configuration.

## Required validation — Windows PowerShell 5.1.26100.9444

Prove E1–E5 against the production logic/source:

```text
E1 successful extraction with qualifying records => PASS
E2 successful extraction with no qualifying records => EMPTY
E3 genuine post-retention invalid archive-entry extraction failure
   => Expand-Archive terminates
   => existing catch executes
   => Extraction=FAIL
E4 W3 final lifecycle result => EVIDENCE_PRESERVATION_FAILED
E5 restoration failure remains dominant over evidence-preservation failure
```

The E3 fixture must induce a genuine `Expand-Archive` extraction failure. Do not manufacture `Extraction=FAIL` or inject internal state.

Also prove:
- PowerShell parser errors = 0;
- no global `$ErrorActionPreference='Stop'` introduced;
- no new injection seam;
- helper unchanged;
- W4 policy truth table unchanged;
- S2 seam unchanged;
- `.ToArray()` poll-observation remediation preserved;
- `git diff --check` passes;
- no secret material appears in diff/output.

## Repository/mutation accounting

Before mutation, capture repository state and the existing governed modified tracked paths.

After implementation, prove the only newly changed production path attributable to this authority is:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Do not stage, commit, push, create PRs, merge, or alter GitHub lifecycle.

No Azure, Docker, or GHCR operations.

## Hybrid-validation boundary

This authority **does not execute or claim W3 Hybrid Revised-V2 acceptance**. It implements and validates the correction only.

Preserve:

```text
W1 = PASS — retained
W2 = PASS — retained
W3 = NO ACCEPTANCE CREDIT / remediation pending reconciliation
W4 = PASS — authorized carry-forward
W5/W6/W7/W8 = NOT_RUN
```

Luna selected the recommended later hybrid restart point as **W3**, subject to post-remediation Luna reconciliation.

After E1–E5 pass, stop. Do not resume W3/W5-W8 under this remediation authority.

## Stop conditions

Stop immediately if:
- more than the authorized one production path must change;
- the exact correction does not make genuine extraction failure terminating;
- E1 or E2 behavior changes;
- E5 precedence changes;
- helper modification becomes necessary;
- a new source defect appears;
- repository scope changes unexpectedly;
- any external mutation would be required.

Do not broaden remediation.

## Required output

Report:
- exact diff;
- parser result;
- E1–E5 results;
- proof the W3 error reached the existing catch;
- global ErrorActionPreference status;
- seam/helper/policy/poll-fix preservation;
- repository/diff/secret-hygiene results;
- exact mutation accounting;
- deepest proven boundary.

## Required terminal markers

`RELEASE 1.12 WP04 — W3 ONE-PATH EXTRACTION ERROR-SEMANTICS REMEDIATION: PASS`

`RELEASE 1.12 WP04 — AUTHORIZED PRODUCTION PATH CHANGED: initialize-qualification.ps1`

`RELEASE 1.12 WP04 — EXPAND-ARCHIVE ERRORACTION STOP: PRESENT`

`RELEASE 1.12 WP04 — POWERSHELL 5.1 PARSER: PASS`

`RELEASE 1.12 WP04 — E1 SUCCESSFUL QUALIFYING EXTRACTION: PASS`

`RELEASE 1.12 WP04 — E2 SUCCESSFUL EMPTY EXTRACTION: PASS`

`RELEASE 1.12 WP04 — E3 GENUINE EXTRACTION FAILURE CAUGHT: PASS`

`RELEASE 1.12 WP04 — E3 EXTRACTION STATE: FAIL`

`RELEASE 1.12 WP04 — E4 FINAL RESULT: EVIDENCE_PRESERVATION_FAILED`

`RELEASE 1.12 WP04 — E5 RESTORATION FAILURE PRECEDENCE: PASS`

`RELEASE 1.12 WP04 — GLOBAL ERRORACTIONPREFERENCE CHANGE: NO`

`RELEASE 1.12 WP04 — NEW W3 INJECTION SEAM: NO`

`RELEASE 1.12 WP04 — HELPER MODIFICATION: NO`

`RELEASE 1.12 WP04 — W4 POLICY: PRESERVED`

`RELEASE 1.12 WP04 — S2 CHECKPOINT SEAM: PRESERVED`

`RELEASE 1.12 WP04 — POLL OBSERVATION TOARRAY FIX: PRESERVED`

`RELEASE 1.12 WP04 — GIT DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE: PASS`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — EXTERNAL MUTATIONS: 0`

`RELEASE 1.12 WP04 — W3 ACCEPTANCE CREDIT: NO`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED`

`RELEASE 1.12 WP04 — HYBRID VALIDATION RESUME: NOT_AUTHORIZED_UNTIL_LUNA_RECONCILIATION`

`RELEASE 1.12 WP04 — NEXT AUTHORITY: LUNA POST-W3-REMEDIATION RECONCILIATION`

`RELEASE 1.12 WP04 — TERRA ONE-PATH W3 EXTRACTION ERROR-SEMANTICS REMEDIATION COMPLETE`

# GPT-5.6 Luna — Release 1.12 WP04 Tracked Lifecycle Remediation Governance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: define the exact tracked helper/procedure contract needed to preserve qualification evidence before restoration.
- **GPT-5.6 Terra** — implement and validate only the exact paths/behavior subsequently authorized.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never replaces Luna/Terra authority.

## 1. Governed baseline

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

PowerShell:

```text
Windows PowerShell 5.1.26100.9444
```

Current source:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Current deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current Azure state remains:

```text
qualification settings = ABSENT
logging = DISABLED
WEBSITES_PORT = 8501
startup override = none
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

Retained evidence must remain preserved:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

## 2. Binding prior reconciliation

```text
O3 — container/listener lifetime failure proven
L2 — qualified listener/container stopped during polling
app-settings writes = restart/recycle triggers
explicit restart after settings write = redundant
M1 — remove explicit restart
V3 — procedure-only remediation invalid
single-trigger invariant = PASS
qualification settings single-write = POSSIBLE
RB2 procedure = PASS
pre-restoration evidence checkpoint = INVALID
restoration sequencing = FAIL
M1 lifecycle model = INVALID under current helper
procedure-only remediation sufficient = NO
source remediation = NO
helper remediation = YES
new image = NO
```

The reason V3 is binding:

```text
verify-persistent-sqlite-webapp.ps1 restores/deletes qualification settings in its finally path
before an outer procedure can durably retrieve/extract Azure logs and complete the evidence checkpoint.
```

## 3. Known restart inventory

Current known call sites:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
  line ~151: invokes helper with -LifecycleAction Restart

initialize-qualification-authority.ps1
  also passes Restart

eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
  line ~241: conditionally invokes az webapp restart
```

The helper already supports:

```text
-LifecycleAction None
```

Therefore no helper change is needed merely to suppress explicit restart.

## 4. Governance objective

Define the narrowest tracked remediation that creates a deterministic boundary:

```text
qualification activation
→ terminal polling result
→ durable evidence preservation
→ explicit restoration
```

Restoration must no longer be inseparably coupled to the helper's polling `finally` block.

The remediation must preserve fail-safe restoration capability without restoring prematurely.

## 5. Ownership model

Select and govern this two-phase model:

### Phase A — qualification/evidence phase

The helper:

```text
snapshots pre-state
applies qualification settings once
uses LifecycleAction=None
allows App Settings write to trigger the qualification recycle
polls to terminal state
returns/persists terminal result
DOES NOT restore qualification settings when deferred restoration is explicitly selected
```

### Phase B — evidence checkpoint/restoration phase

The outer governed procedure:

```text
captures actual wrapper-owned RunId (RB2)
retains helper transcript
retrieves/hashes raw Azure archive
extracts fresh-window evidence
persists D3 payload if observed
completes pre-restoration evidence checkpoint
invokes explicit governed restoration
verifies all six qualification settings ABSENT
restores logging
verifies final state
```

## 6. Fail-safe restoration requirement

Deferred restoration must not mean "leave settings indefinitely."

The tracked remediation must provide an explicit restoration operation that can be invoked from the outer procedure's `finally` block.

Required invariant:

```text
OUTER FINALLY ALWAYS ATTEMPTS RESTORATION
```

even when:

```text
polling fails
archive retrieval fails
evidence extraction fails
unexpected exception occurs
```

The evidence checkpoint determines whether diagnostic/acceptance evidence is valid; it does not remove the restoration obligation.

## 7. Required helper contract

Govern a minimal explicit mode/parameter rather than silently changing existing behavior.

Preferred contract:

```text
-RestorationMode Immediate | Deferred | RestoreOnly
```

or a semantically equivalent explicit enum/switch design.

Required semantics:

### `Immediate`
Preserve current default behavior for existing callers:

```text
helper restores in its own finally block
```

### `Deferred`
Qualification execution:

```text
snapshot/apply/poll
do not restore in helper finally
emit enough sanitized restoration metadata for later governed restore
```

### `RestoreOnly`
Perform only the exact governed restoration from a previously captured safe pre-state/restoration descriptor.

Do not:

```text
apply qualification settings
poll endpoint
restart explicitly
generate a new qualification RunId
```

If source structure supports a safer equivalent split, Luna may select it, but semantics must remain explicit and fail closed.

## 8. Restoration descriptor

Deferred mode must make restoration deterministic without persisting secrets.

Define the minimal safe restoration descriptor needed to restore the six governed qualification setting names to their pre-state.

Current pre-state is expected:

```text
all six = ABSENT
```

but the helper contract must not silently assume that for generic reuse unless the WP04 caller explicitly binds it.

Do not persist:

```text
HttpEvidenceToken value
TwelveData__ApiKey
registry secrets
connection strings
```

If a pre-existing governed qualification value would need secret persistence to restore exactly, fail closed and return to Luna rather than writing the secret to disk.

## 9. Exact qualification setting scope

The governed six names remain:

```text
Worker__Mode
PersistentSqliteQualification__Phase
PersistentSqliteQualification__HttpEvidenceEnabled
PersistentSqliteQualification__EvidenceOutputPath
PersistentSqliteQualification__RunId
PersistentSqliteQualification__HttpEvidenceToken
```

No new Azure configuration names are authorized by this governance.

Twelve Data configuration remains forbidden.

## 10. Lifecycle action

For the remediated WP04 flow:

```text
LifecycleAction=None
```

Binding:

```text
explicit az webapp restart before evidence checkpoint = 0
```

The qualification app-settings write is the single activation/recycle trigger.

Restoration app-settings mutation occurs only after terminal evidence preservation and may trigger the post-qualification normal-mode recycle.

## 11. RB2 binding

Preserve:

```text
RB2 — wrapper/helper owns actual qualification RunId
```

The outer procedure must not pre-generate a competing qualification RunId.

A separate correlation ID may exist only if clearly named `CorrelationId` or equivalent and never used as target attribution.

Once actual RunId is emitted:

```text
metadata
transcript attribution
archive filtering
fresh-window evidence
D3 payload attribution
```

must all bind to that actual RunId.

## 12. Evidence checkpoint

Before calling `RestoreOnly`, require durable status for:

```text
actual RunId known
helper terminal state retained
helper transcript retained
poll observations retained
D3 payload retained if observed
raw Azure archive retained OR terminal archive-retrieval failure durably recorded
archive SHA-256/size/path recorded if retrieved
fresh-window extraction retained
deepest execution boundary derivable
```

Return:

```text
EVIDENCE_CHECKPOINT=PASS|FAIL
```

Restoration occurs in either case, but acceptance/diagnostic credit is denied if the checkpoint fails.

## 13. Error semantics

Required:

```text
qualification/poll failure must remain distinguishable from restoration failure
evidence-preservation failure must remain distinguishable from restoration failure
restoration failure must dominate final lifecycle safety result
```

Do not mask the original terminal result.

Return sanitized classifications.

## 14. Null/absent restoration semantics

Preserve prior governance:

```text
current six-setting target = ABSENT
helper must not write null/empty to represent absence
delete governed names
bounded read-only convergence checks allowed
no repeated blind delete/write
```

If `PRESENT_NULL` appears transiently:

```text
read-only rechecks up to 120 seconds
minimum 5-second interval
```

No second mutation solely for the transient representation.

## 15. Candidate tracked paths

Luna must inspect and freeze the exact path allowlist before Terra implementation.

Expected narrow candidates:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Include `initialize-qualification-authority.ps1` only if it is a tracked canonical execution surface that must remain consistent.

Do not authorize unrelated source/runtime/Docker changes.

Return exact:

```text
TRACKED PATH ALLOWLIST
CREATE count
MODIFY count
DELETE count
```

before implementation authority.

## 16. Compatibility validation

Any later implementation must prove under:

```text
Windows PowerShell 5.1.26100.9444
```

at minimum:

```text
parse success
parameter binding success
Immediate mode preserves existing behavior
Deferred mode does not restore prematurely
RestoreOnly does not activate qualification or restart
LifecycleAction=None performs no explicit restart
secret-safe output
failure classifications preserved
```

Prefer local mocks/static harnesses; no Azure mutation is required for implementation validation.

## 17. Existing behavior compatibility

Default behavior for existing non-remediated callers must remain compatible unless Luna explicitly governs migration.

Required:

```text
no silent default change from Immediate to Deferred
```

The WP04 remediated caller must opt into deferred restoration explicitly.

## 18. New image decision

These are `eng/azure-cli` host-side scripts.

Binding unless repository inspection contradicts it:

```text
NEW IMAGE REQUIRED = NO
```

Do not rebuild/publish the runtime image for host-side orchestration-only changes.

## 19. Publication boundary

This Luna authority is governance/read-only only.

It does not authorize:

```text
file edits
staging
commit
push
PR
merge
Azure mutation
Docker/GHCR mutation
issue/Project lifecycle mutation
```

After Luna freezes the exact contract and allowlist, generate a separate Terra implementation authority.

## 20. Acceptance boundary

Preserve:

```text
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
```

## 21. Governance decisions

Return exactly one helper split:

```text
H1 — Immediate/Deferred/RestoreOnly explicit helper contract
H2 — equivalent explicit two-phase helper contract
H3 — helper split not safely achievable; broader redesign required
```

Return exactly one path decision:

```text
P1 — helper + initialize wrapper only
P2 — helper + initialize wrapper + tracked authority wrapper
P3 — other exact narrow allowlist
```

Return implementation readiness:

```text
I1 — exact tracked remediation ready for Terra
I2 — more Luna read-only source reconciliation required
```

## 22. Required output

Return:

- exact helper restoration control contract;
- exact restoration descriptor semantics;
- outer-finally fail-safe behavior;
- exact RB2 flow;
- exact evidence checkpoint;
- exact error precedence;
- exact path allowlist and mutation counts;
- Windows PowerShell 5.1 validation gates;
- compatibility/default behavior;
- H1–H3;
- P1–P3;
- I1/I2;
- new-image decision;
- zero-mutation audit;
- exact next authority.

## 23. Terminal markers

`RELEASE 1.12 WP04 — TRACKED LIFECYCLE REMEDIATION GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — GOVERNING VALIDATION RESULT: V3`

`RELEASE 1.12 WP04 — HELPER REMEDIATION REQUIRED: YES`

`RELEASE 1.12 WP04 — HELPER RESTORATION CONTRACT: <H1|H2|H3>`

`RELEASE 1.12 WP04 — DEFAULT RESTORATION BEHAVIOR: IMMEDIATE`

`RELEASE 1.12 WP04 — DEFERRED RESTORATION: EXPLICIT_OPT_IN`

`RELEASE 1.12 WP04 — RESTORE-ONLY OPERATION: <REQUIRED|EQUIVALENT_CONTRACT>`

`RELEASE 1.12 WP04 — OUTER FINALLY RESTORATION ATTEMPT: REQUIRED`

`RELEASE 1.12 WP04 — LIFECYCLE ACTION FOR QUALIFICATION: NONE`

`RELEASE 1.12 WP04 — EXPLICIT RESTART BEFORE EVIDENCE CHECKPOINT: 0`

`RELEASE 1.12 WP04 — RUNID BINDING MODEL: RB2`

`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: REQUIRED`

`RELEASE 1.12 WP04 — NULL/ABSENT RESTORATION SEMANTICS: PRESERVED`

`RELEASE 1.12 WP04 — TWELVE DATA SECRET CONFIGURATION: FORBIDDEN`

`RELEASE 1.12 WP04 — TRACKED PATH DECISION: <P1|P2|P3>`

`RELEASE 1.12 WP04 — TRACKED PATH COUNT: <n>`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 VALIDATION: REQUIRED`

`RELEASE 1.12 WP04 — SOURCE RUNTIME REMEDIATION REQUIRED: NO`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — IMPLEMENTATION READINESS: <I1|I2>`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — TRACKED LIFECYCLE REMEDIATION GOVERNANCE MUTATION AUDIT: PASS`

Then exactly one:

`RELEASE 1.12 WP04 — TERRA TRACKED LIFECYCLE REMEDIATION IMPLEMENTATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA TRACKED LIFECYCLE SOURCE RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA TRACKED LIFECYCLE REMEDIATION GOVERNANCE COMPLETE`

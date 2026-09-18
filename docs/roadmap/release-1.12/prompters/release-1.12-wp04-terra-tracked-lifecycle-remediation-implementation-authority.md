# GPT-5.6 Terra — Release 1.12 WP04 Tracked Lifecycle Remediation Implementation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — owns contract, architecture, governance, reconciliation, acceptance criteria, and path authorization.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the exact two-path helper lifecycle remediation governed by Luna.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed baseline

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

PowerShell target:

```text
Windows PowerShell 5.1.26100.9444
```

Starting source commit:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Current deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Retained diagnostic evidence must remain untouched:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

## 2. Governing Luna contract

Binding:

```text
V3 — tracked helper remediation required
H1 — Immediate / Deferred / RestoreOnly
P1 — helper + initialize wrapper only
I1 — implementation ready
DEFAULT RESTORATION = Immediate
DEFERRED RESTORATION = explicit opt-in
RestoreOnly = required
OUTER FINALLY restoration attempt = required
LifecycleAction for qualification = None
explicit restart before evidence checkpoint = 0
RunId binding = RB2
pre-restoration evidence checkpoint = required
null/absent restoration semantics = preserved
Twelve Data secret configuration = forbidden
source runtime remediation = no
new image = no
```

## 3. Exact tracked-path allowlist

Only these tracked paths may change:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Mutation counts:

```text
CREATE = 0
MODIFY = 2
DELETE = 0
TRACKED PATH COUNT = 2
```

No other tracked file may change.

`initialize-qualification-authority.ps1` is excluded because it is not tracked/canonical.

## 4. Implementation objective

Implement an explicit two-phase restoration contract that allows:

```text
qualification activation/polling
→ durable evidence preservation outside helper
→ explicit restoration
```

without premature helper restoration.

The implementation must preserve existing caller behavior by default.

## 5. Helper restoration parameter

In:

```text
verify-persistent-sqlite-webapp.ps1
```

add an explicit restoration control.

Preferred interface:

```powershell
[ValidateSet('Immediate','Deferred','RestoreOnly')]
[string]$RestorationMode = 'Immediate'
```

Equivalent naming is allowed only if semantics remain exactly the same and explicit.

Do not silently infer mode from unrelated parameters.

## 6. Immediate mode

`Immediate` must preserve existing behavior.

Required:

```text
default mode = Immediate
qualification settings snapshot = existing behavior
qualification settings apply = existing behavior
polling = existing behavior
helper finally = restores exact pre-state
```

Existing callers that do not pass the new parameter must remain behaviorally compatible.

## 7. Deferred mode

`Deferred` semantics:

```text
snapshot exact governed pre-state
apply qualification settings once
perform governed polling
do not restore qualification settings in helper finally
return/persist enough safe restoration state for later RestoreOnly
```

Binding:

```text
LifecycleAction=None
explicit az webapp restart = 0
```

Deferred mode must still execute helper cleanup unrelated to Azure qualification-setting restoration where safe/required.

It must not:

```text
delete qualification settings
restore qualification settings
generate an additional RunId
restart the web app explicitly
```

## 8. RestoreOnly mode

`RestoreOnly` must:

```text
perform only governed restoration of the six qualification settings
use a previously captured safe pre-state/restoration descriptor
perform no qualification activation
perform no polling
perform no evidence endpoint call
generate no qualification RunId
perform no explicit restart
```

If required restoration input is incomplete or malformed:

```text
fail closed
perform no partial speculative restore
```

## 9. Governed six-setting scope

Only these setting names are in the restoration contract:

```text
Worker__Mode
PersistentSqliteQualification__Phase
PersistentSqliteQualification__HttpEvidenceEnabled
PersistentSqliteQualification__EvidenceOutputPath
PersistentSqliteQualification__RunId
PersistentSqliteQualification__HttpEvidenceToken
```

No new Azure configuration names are authorized.

## 10. Restoration descriptor

Implement the minimum safe restoration descriptor needed by the outer wrapper.

The descriptor must support exact pre-state semantics without writing secrets to disk.

For the current WP04 canonical pre-state:

```text
all six settings = ABSENT
```

the descriptor may encode only presence/absence and non-secret values where safe.

Never persist:

```text
PersistentSqliteQualification__HttpEvidenceToken value
TwelveData__ApiKey
registry password
connection strings
Authorization header
cookies
```

If exact restoration would require persisting a pre-existing secret value:

```text
fail closed
```

Do not silently degrade exact restoration.

## 11. Null/absent semantics

Preserve the established contract:

```text
absence = delete setting name
do not write null
do not write empty string as surrogate for absence
```

RestoreOnly must preserve exact absence for absent pre-state entries.

If the current helper already owns safe delete/restore routines, reuse them rather than duplicating semantics.

## 12. Error/result separation

Preserve distinct classifications for:

```text
qualification/poll terminal result
evidence/poll transport result
restoration result
```

Deferred mode must not falsely report restoration success because restoration has not yet occurred.

Return a clear state such as:

```text
RESTORATION=DEFERRED
```

or equivalent sanitized terminal marker.

RestoreOnly must return explicit:

```text
RESTORATION=PASS|FAIL
```

Restoration failure must remain distinguishable and later dominate lifecycle safety.

## 13. Terminal telemetry

Preserve the governed helper terminal fields:

```text
WP04_HELPER_TERMINAL_RESULT
WP04_HELPER_TERMINAL_CLASS
WP04_HELPER_TERMINAL_PHASE
WP04_HELPER_TERMINAL_RUN_ID
WP04_HELPER_TERMINAL_EXIT_CODE
WP04_HELPER_TERMINAL_SETTINGS_RESTORATION
WP04_HELPER_TERMINAL_ELAPSED_MS
```

For Deferred mode:

```text
WP04_HELPER_TERMINAL_SETTINGS_RESTORATION=DEFERRED
```

or semantically equivalent explicit value.

For RestoreOnly:

```text
phase/result must clearly identify restoration-only execution
```

Do not emit secrets.

## 14. RunId ownership

Preserve RB2:

```text
helper owns/generates actual qualification RunId
outer wrapper consumes actual helper-generated RunId
```

The outer wrapper must not pre-generate a competing qualification RunId.

If it needs a pre-run local identifier, name it:

```text
CorrelationId
```

or another non-RunId term.

## 15. initialize-qualification.ps1 orchestration

Modify:

```text
initialize-qualification.ps1
```

to implement the governed two-phase sequence.

Qualification phase:

```text
invoke helper with:
  RestorationMode=Deferred
  LifecycleAction=None
```

Then:

```text
capture actual RunId from helper terminal output/result
bind evidence metadata to actual RunId
preserve helper transcript/poll observations
perform/retain raw Azure archive retrieval
record hash/size/path
perform fresh-window extraction
derive deepest boundary
set EVIDENCE_CHECKPOINT=PASS|FAIL
```

Finally:

```text
always invoke helper with RestorationMode=RestoreOnly
verify all six settings converge to exact pre-state
then restore logging if this wrapper owns logging
```

If current script architecture delegates some evidence functions to the caller, do not invent unsupported behavior. Preserve the contract by exposing the necessary deterministic boundary and restoration descriptor/results to the existing outer execution surface.

## 16. Outer finally requirement

The canonical wrapper must structurally guarantee:

```text
try
{
    qualification/evidence phase
}
finally
{
    RestoreOnly attempted exactly once
}
```

or semantically equivalent Windows PowerShell 5.1-compatible structure.

RestoreOnly must be attempted even if:

```text
helper poll fails
D3 not observed
archive retrieval fails
fresh extraction fails
unexpected exception occurs
```

No second RestoreOnly attempt unless a later Luna authority explicitly governs it.

## 17. Explicit restart removal

For the WP04 qualification flow:

```text
-LifecycleAction None
```

must be passed.

Remove/replace the canonical `Restart` invocation from `initialize-qualification.ps1`.

Required static result:

```text
explicit restart before evidence checkpoint = 0
```

Do not remove generic restart support from the helper itself; other callers may still use it.

## 18. Evidence checkpoint

The wrapper must not invoke RestoreOnly until it has determined a durable checkpoint state.

Checkpoint inputs:

```text
actual RunId known
helper terminal result retained
helper transcript retained
poll observations retained
D3 payload retained if observed
raw archive retained OR terminal retrieval failure durably recorded
archive SHA-256/size/path recorded if retrieved
fresh-window extraction retained
deepest boundary derivable
```

Return:

```text
EVIDENCE_CHECKPOINT=PASS|FAIL
```

Restoration occurs regardless.

Acceptance/diagnostic credit is denied later if checkpoint is FAIL.

## 19. Current evidence-directory behavior

Do not delete the existing retained evidence directory.

Any local validation artifacts created by this implementation authority must be:

```text
outside tracked repository paths
or ignored/untracked
```

and must not contain secrets.

## 20. No Azure execution

This implementation authority is local-only.

Forbidden:

```text
az webapp config appsettings set/delete
az webapp restart
Azure logging changes
new diagnostic RunId
qualification run
acceptance run
```

Mock/stub Azure commands for validation if needed.

## 21. Windows PowerShell 5.1 validation

Mandatory:

```text
parse both modified scripts under Windows PowerShell 5.1.26100.9444
```

Validate parameter binding for:

```text
Immediate
Deferred
RestoreOnly
LifecycleAction None
```

No PowerShell 7-only syntax.

## 22. Local behavioral validation matrix

Using local mocks/stubs only, prove:

### T1 — default compatibility

```text
no RestorationMode supplied
→ Immediate
→ existing restoration path executes
```

### T2 — Deferred

```text
qualification apply/poll executes
restoration does not execute in helper finally
terminal restoration state = DEFERRED
```

### T3 — RestoreOnly

```text
no qualification apply
no polling
no RunId generation
no explicit restart
restoration executes exactly once
```

### T4 — LifecycleAction None

```text
no az webapp restart invocation
```

### T5 — outer finally

Inject qualification/evidence failure and prove:

```text
RestoreOnly still invoked exactly once
```

### T6 — restoration failure precedence

Inject RestoreOnly failure and prove final lifecycle status reports restoration failure distinctly/dominantly.

### T7 — absent/null semantics

Prove absent settings restore by deletion, never null/empty write.

### T8 — secret hygiene

Search validation output/artifacts for:

```text
HttpEvidenceToken value
TwelveData key
Authorization values
registry passwords
```

Expected:

```text
0 findings
```

## 23. Regression validation

Run existing relevant PowerShell/unit/static tests if present.

Do not broaden the validation scope into unrelated repository tests unless required by repository conventions.

If repository signing/build gates are triggered by the chosen validation commands, preserve the existing signing contract and do not bypass it.

## 24. Tracked mutation audit

At completion prove:

```text
exactly 2 tracked modified paths
0 tracked creates
0 tracked deletes
0 unauthorized tracked paths
0 staged paths
```

Unrelated untracked `prompters/` or other user files remain untouched.

## 25. No publication under this authority

Do not:

```text
git add
git commit
git push
open PR
merge
build/publish Docker image
publish GHCR
mutate GitHub issue/Project/milestone
```

A separate Terra publication authority is required after Luna reconciliation of implementation results.

## 26. Required output

Return:

- exact two changed paths;
- concise diff behavior summary;
- restoration parameter contract;
- descriptor contract;
- Immediate/Deferred/RestoreOnly behavior;
- explicit restart removal proof;
- RB2 proof;
- outer-finally proof;
- evidence-checkpoint proof;
- T1–T8 results;
- PowerShell 5.1 parse/binding results;
- secret scan;
- tracked mutation audit;
- whether implementation is ready for Luna reconciliation;
- zero Azure/Git/GitHub/Docker/GHCR mutation audit.

## 27. Terminal markers

`RELEASE 1.12 WP04 — TRACKED LIFECYCLE REMEDIATION IMPLEMENTATION: PASS`

`RELEASE 1.12 WP04 — HELPER RESTORATION CONTRACT: H1`

`RELEASE 1.12 WP04 — DEFAULT RESTORATION BEHAVIOR: IMMEDIATE`

`RELEASE 1.12 WP04 — DEFERRED RESTORATION: IMPLEMENTED`

`RELEASE 1.12 WP04 — RESTORE-ONLY OPERATION: IMPLEMENTED`

`RELEASE 1.12 WP04 — OUTER FINALLY RESTORATION ATTEMPT: IMPLEMENTED`

`RELEASE 1.12 WP04 — LIFECYCLE ACTION FOR QUALIFICATION: NONE`

`RELEASE 1.12 WP04 — EXPLICIT RESTART BEFORE EVIDENCE CHECKPOINT: 0`

`RELEASE 1.12 WP04 — RUNID BINDING MODEL: RB2`

`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: IMPLEMENTED`

`RELEASE 1.12 WP04 — NULL/ABSENT RESTORATION SEMANTICS: PRESERVED`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 PARSE VALIDATION: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 PARAMETER BINDING: PASS`

`RELEASE 1.12 WP04 — LOCAL BEHAVIORAL VALIDATION T1-T8: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE VALIDATION: PASS`

`RELEASE 1.12 WP04 — AUTHORIZED TRACKED MODIFY COUNT: 2`

`RELEASE 1.12 WP04 — UNAUTHORIZED TRACKED PATH COUNT: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — TRACKED LIFECYCLE REMEDIATION IMPLEMENTATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA POST-IMPLEMENTATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA TRACKED LIFECYCLE REMEDIATION IMPLEMENTATION COMPLETE`

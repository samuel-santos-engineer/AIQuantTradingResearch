# GPT-5.6 Terra — Release 1.12 WP04 Procedure-Only Lifecycle Remediation Validation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: validate the M1 procedure-only lifecycle correction without executing a new Azure qualification run.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never replaces Luna/Terra authority.

## 1. Governed state

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

PowerShell:

```text
Windows PowerShell 5.1.26100.9444
```

Source:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current Azure state must remain:

```text
qualification settings = ABSENT
logging = DISABLED
WEBSITES_PORT = 8501
startup override = none
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

Retained evidence remains preserved at:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

Do not modify or delete it.

## 2. Binding Luna reconciliation

```text
O3 — CONTAINER_OR_LISTENER_LIFETIME_FAILURE_PROVEN
L2 — listener/container stopped during polling
C3 — normal-mode start vs restoration ordering not proven
APP SETTINGS WRITE RESTART SEMANTICS = PROVEN_RESTART_TRIGGER
EXPLICIT RESTART AFTER SETTINGS WRITE = REDUNDANT
NORMAL-MODE START CAUSAL CLASS = N5
QUALIFICATION LIFECYCLE CONTRACT = VIOLATED_BY_PROCEDURE
SELECTED LIFECYCLE REMEDIATION = M1
PROCEDURE-ONLY REMEDIATION SUFFICIENT = YES
SOURCE REMEDIATION = NO
HELPER REMEDIATION = NO
NEW IMAGE = NO
RB2 — wrapper owns RunId; metadata follows wrapper
```

Twelve Data secret configuration remains forbidden in WP04.

## 3. Mission

Validate, without Azure mutation, that the next qualification procedure can implement M1 safely:

```text
REMOVE EXPLICIT az webapp restart
```

The qualification app-settings write is itself the single activation/recycle trigger.

The validation must prove that the resulting procedure has exactly one intentional activation trigger before evidence polling and that restoration cannot begin until the diagnostic/acceptance evidence phase reaches a terminal checkpoint.

This authority does **not** authorize a new diagnostic run or acceptance retry.

## 4. Procedure model to validate

The corrected lifecycle must be:

```text
1. establish durable evidence root and preflight
2. enable temporary logging if the later execution authority requires it
3. prove logging readiness
4. apply qualification app settings exactly once
5. DO NOT call az webapp restart
6. observe the Azure-triggered recycle caused by the settings write
7. obtain the actual wrapper-owned RunId and bind metadata/evidence to it (RB2)
8. poll qualification evidence through the governed terminal deadline
9. preserve helper transcript and raw Azure archive/evidence
10. reach explicit evidence terminal checkpoint
11. only then restore/delete qualification app settings
12. observe that restoration itself may trigger the normal-mode recycle
13. restore temporary logging after evidence preservation
14. verify exact final Azure state
```

No restoration may occur concurrently with active evidence polling.

## 5. Single-trigger invariant

Before the evidence terminal checkpoint:

```text
QUALIFICATION ACTIVATION TRIGGERS = 1
```

That trigger is:

```text
qualification app-settings mutation
```

Forbidden before the checkpoint:

```text
az webapp restart
second app-settings qualification write
qualification-settings restoration
other restart/recycle mutation
```

Return:

```text
SINGLE-TRIGGER INVARIANT = PASS | FAIL
```

## 6. Explicit restart removal proof

Inspect the exact existing execution procedure/wrapper orchestration that would be used for the next run.

Identify every occurrence capable of invoking:

```text
az webapp restart
Restart-AzWebApp
ARM restart action
equivalent restart operation
```

Determine which occurrence belongs to the outer procedure versus the existing wrapper/helper.

The M1 correction is valid only if the next execution can guarantee:

```text
EXPLICIT RESTART COUNT BEFORE EVIDENCE CHECKPOINT = 0
```

without tracked source/helper edits.

If an existing tracked helper unavoidably performs restart internally, stop and return that procedure-only remediation is not sufficient.

## 7. App-settings mutation inventory

Read-only enumerate the planned mutations that can trigger recycle:

```text
logging enablement
qualification settings application
qualification settings restoration/deletion
logging restoration
```

Classify which are App Service application-settings mutations versus diagnostic-log configuration mutations.

The next execution must avoid accidentally treating logging configuration as qualification activation.

Return a chronological trigger inventory.

## 8. Qualification settings application

Validate that all required qualification values can be applied in one logical app-settings mutation:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
PersistentSqliteQualification__HttpEvidenceEnabled=true
PersistentSqliteQualification__RunId=<wrapper-governed semantics>
PersistentSqliteQualification__HttpEvidenceToken=<secret>
PersistentSqliteQualification__EvidenceOutputPath=<only if governed/required>
```

Do not print or generate the token here.

No Twelve Data setting.

Return:

```text
QUALIFICATION SETTINGS SINGLE-WRITE = POSSIBLE | NOT_POSSIBLE | NOT_PROVEN
```

## 9. RB2 validation

The prior procedure incorrectly pre-created metadata around a different RunId.

Validate the future RB2 flow:

```text
wrapper owns/generates actual RunId
procedure captures exact actual RunId
durable evidence metadata is finalized/rebound to actual RunId
all target-attributable helper/archive filtering uses actual RunId
```

No second RunId may be generated by the outer procedure for qualification identity.

A pre-run correlation identifier may exist only if explicitly named something other than `RunId` and never used as qualification attribution.

Return:

```text
RB2 PROCEDURE = PASS | FAIL
```

## 10. Evidence terminal checkpoint

Define one exact gate that must complete before qualification settings restoration.

Required components:

```text
helper terminal state reached
actual RunId durably known
helper transcript retained
poll observations retained
D3 payload retained if observed
raw Azure log archive retained or terminal retrieval failure durably recorded
fresh-window extraction retained
deepest diagnostic boundary derivable
```

For a later acceptance authority, Luna may strengthen this gate further.

Return:

```text
PRE-RESTORATION EVIDENCE CHECKPOINT = VALID | INVALID
```

## 11. Restoration sequencing

Validate strict ordering:

```text
polling terminal
→ evidence preservation checkpoint
→ qualification settings restoration
→ bounded ABSENT read-back
→ logging restoration
```

Restoration app-settings mutation is expected to trigger another recycle, but only **after** evidence acquisition is terminal.

Therefore this later recycle must not invalidate the completed qualification evidence.

Return:

```text
RESTORATION SEQUENCING = PASS | FAIL
```

## 12. Lifecycle expectation

Under M1, the expected lifecycle is:

```text
qualification-settings write
→ Azure-triggered qualification recycle/container
→ listener/evidence window
→ terminal evidence checkpoint
→ qualification-settings restoration
→ Azure-triggered normal-mode recycle
```

This is not "one container forever." It is:

```text
one qualification activation lifecycle before evidence completion
```

Return:

```text
M1 LIFECYCLE MODEL = VALID | INVALID | NOT_PROVEN
```

## 13. Normal-mode Twelve Data exit

The post-restoration normal-mode container may still exit 64 because WP05 has not configured `TwelveData__ApiKey`.

That is acceptable for this WP04 procedure boundary after qualification evidence has been safely preserved.

Do not use normal public-root health as a WP04 persistence gate.

Return:

```text
POST-RESTORATION NORMAL-MODE 503 = EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER
```

## 14. No tracked remediation

Prove the M1 correction can be implemented by execution orchestration alone.

Required:

```text
tracked source edits = 0
tracked helper edits = 0
new image = no
GHCR publication = no
```

If not, fail this validation and return to Luna.

## 15. Future execution classification

This authority must decide whether M1 is ready for one later controlled execution.

Select:

```text
V1 — M1 PROCEDURE VALID; LUNA MAY GOVERN FRESH INITIALIZE ACCEPTANCE RETRY
V2 — M1 PROCEDURE VALID; ONE DIAGNOSTIC-ONLY VALIDATION STILL REQUIRED
V3 — M1 PROCEDURE INVALID; TRACKED REMEDIATION REQUIRED
V4 — M1 PROCEDURE NOT YET PROVEN; MORE READ-ONLY RECONCILIATION REQUIRED
```

Prefer V1 if the procedure-only correction is fully deterministic and the existing diagnostic evidence already proved runtime capability through B6 plus a valid diagnostic D3 payload.

Do not automatically require another diagnostic run merely because M1 has not yet been executed.

## 16. Acceptance boundary

Under this authority:

```text
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
REOPEN = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
```

WP04 #263 remains open and WP05 remains blocked.

## 17. Mutation boundary

Exactly zero:

```text
Azure mutations
Azure restarts
qualification attempts
diagnostic attempts
repository edits
helper edits
Git mutations
Docker builds
GHCR publications
GitHub/Project/issue/milestone mutations
```

Read-only local/source/Azure inspection only.

## 18. Required output

Return:

- exact existing restart invocation inventory;
- app-settings/recycle-trigger inventory;
- single-trigger invariant;
- qualification single-write feasibility;
- RB2 procedure validation;
- pre-restoration evidence checkpoint validation;
- restoration sequencing;
- M1 lifecycle validity;
- procedure-only sufficiency;
- V1–V4;
- exact next authority;
- zero-mutation audit.

## 19. Terminal markers

`RELEASE 1.12 WP04 — PROCEDURE-ONLY LIFECYCLE REMEDIATION VALIDATION: PASS`

`RELEASE 1.12 WP04 — SELECTED LIFECYCLE REMEDIATION: M1`

`RELEASE 1.12 WP04 — EXPLICIT RESTART REMOVAL: REQUIRED`

`RELEASE 1.12 WP04 — EXPLICIT RESTART COUNT BEFORE EVIDENCE CHECKPOINT: 0`

`RELEASE 1.12 WP04 — SINGLE-TRIGGER INVARIANT: <PASS|FAIL>`

`RELEASE 1.12 WP04 — QUALIFICATION SETTINGS SINGLE-WRITE: <POSSIBLE|NOT_POSSIBLE|NOT_PROVEN>`

`RELEASE 1.12 WP04 — RUNID BINDING MODEL: RB2`

`RELEASE 1.12 WP04 — RB2 PROCEDURE: <PASS|FAIL>`

`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: <VALID|INVALID>`

`RELEASE 1.12 WP04 — RESTORATION SEQUENCING: <PASS|FAIL>`

`RELEASE 1.12 WP04 — M1 LIFECYCLE MODEL: <VALID|INVALID|NOT_PROVEN>`

`RELEASE 1.12 WP04 — POST-RESTORATION NORMAL-MODE 503: EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER`

`RELEASE 1.12 WP04 — PROCEDURE-ONLY REMEDIATION SUFFICIENT: <YES|NO|NOT_PROVEN>`

`RELEASE 1.12 WP04 — SOURCE REMEDIATION REQUIRED: <YES|NO|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HELPER REMEDIATION REQUIRED: <YES|NO|NOT_PROVEN>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO|NOT_PROVEN>`

`RELEASE 1.12 WP04 — LIFECYCLE VALIDATION RESULT: <V1|V2|V3|V4>`

`RELEASE 1.12 WP04 — TWELVE DATA SECRET CONFIGURATION: FORBIDDEN`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY UNDER THIS AUTHORITY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — PROCEDURE-ONLY LIFECYCLE REMEDIATION VALIDATION MUTATION AUDIT: PASS`

Then exactly one matching marker:

`RELEASE 1.12 WP04 — LUNA FRESH INITIALIZE ACCEPTANCE RETRY GOVERNANCE AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA M1 DIAGNOSTIC-ONLY VALIDATION GOVERNANCE AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA TRACKED LIFECYCLE REMEDIATION GOVERNANCE AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA LIFECYCLE EVIDENCE-GAP RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA PROCEDURE-ONLY LIFECYCLE REMEDIATION VALIDATION COMPLETE`

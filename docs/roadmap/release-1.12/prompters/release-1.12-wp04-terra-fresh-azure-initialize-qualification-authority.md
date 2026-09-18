# GPT-5.6 Terra — Release 1.12 WP04 Fresh Azure Persistence Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, acceptance criteria, dependency regovernance.
- **GPT-5.6 Terra** — PRIMARY: execute one fresh WP04 Azure persistence qualification attempt within the corrected G1 acceptance boundary.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current source commit:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Current governed deployed image:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Dependency regovernance result:

```text
G1 — WP04 ACCEPTANCE-BOUNDARY CORRECTION
```

Binding interpretation:

```text
WP04 acceptance = persistence-specific qualification
TwelveData__ApiKey ownership = WP05
normal public runtime health = downstream WP05/WP06 acceptance
WP04 qualification mode = independent of TwelveData__ApiKey
normal root HTTP 503 = EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER
```

No secret configuration is authorized.

## 2. Objective

Execute exactly one **fresh initialize-phase WP04 qualification attempt** against the governed Azure App Service and governed image.

The attempt must prove, for a new RunId:

```text
application-owned qualification mode entered
SQLite path = /home/data/aiquant.db
schema version = 4
journal mode = delete
accepted evidence count = 1
accepted evidence identity is present
integrity_check = ok
quick_check = ok
persistence continuity field is truthful for initialize phase
HTTP evidence record is attributable to this exact fresh RunId
settings restoration succeeds
```

This authority does **not** authorize reopen/redeploy continuity yet.

A separate Luna/Terra authority is required after successful initialize evidence reconciliation.

## 3. Corrected acceptance boundary

Do not use normal public root health as a WP04 gate.

The current normal root:

```text
HTTP 503
```

is already reconciled as:

```text
EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER
```

because `TwelveData__ApiKey` is canonically owned by WP05.

Therefore:

```text
normal root 503 != WP04 qualification failure
```

Do not configure `TwelveData__ApiKey`.

Do not change entrypoint semantics.

Do not start WP05.

## 4. Windows PowerShell binding

All PowerShell execution must target:

```text
Windows PowerShell 5.1.26100.9444
```

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

Do not use PowerShell 7-only syntax or APIs.

## 5. Qualification implementation boundary

Use the already-governed WP04 qualification mechanism.

Canonical activation:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__HttpEvidenceEnabled=true
```

Qualification-mode runtime contract:

```text
Worker owns 0.0.0.0:8501
Streamlit is suppressed
qualification endpoint is enabled temporarily
container remains alive only for bounded evidence retrieval
container exits after retrieval or timeout
```

Canonical evidence endpoint:

```text
GET /internal/wp04/persistence-qualification?runId=<exact-run-id>
```

Required header:

```text
X-WP04-Evidence-Token
```

The token must be high entropy, temporary, and never printed.

## 6. Fresh RunId

Generate exactly one new initialize RunId:

```text
initialize-<fresh-guid-N>
```

It must not match any prior failed RunId.

Never reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

Malformed historical identifier is also forbidden if encountered:

```text
initialize-50a6837f6cb849...
```

## 7. Mandatory preflight before mutation

Before setting qualification controls, prove all of the following read-only:

```text
current source HEAD = 2532f6abd4677edfb205c26c083a534783038979
wrapper provenance gate = PASS
wrapper tracked at HEAD = PASS
wrapper worktree content equals committed content = PASS
HEAD descends from governed wrapper baseline
linuxFxVersion = exact governed digest
WEBSITES_PORT = 8501
startup override = none
registry username/password = absent
SCM basic auth = false
FTP basic auth = false
App Service state = Running
diagnostic logging = disabled
```

Also prove that no qualification-specific settings are already present before this attempt.

At minimum check:

```text
Worker__Mode
PersistentSqliteQualification__Phase
PersistentSqliteQualification__HttpEvidenceEnabled
PersistentSqliteQualification__EvidenceOutputPath
PersistentSqliteQualification__RunId
PersistentSqliteQualification__HttpEvidenceToken
```

If any qualification setting is already present:

```text
PRECONDITION = FAIL
```

STOP before mutation.

Do not remove unknown/stale qualification settings under this authority.

## 8. Image preflight

The deployed `linuxFxVersion` must match exactly:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Fail closed for:

```text
blank
null
tag-only
malformed
multiple values
partial digest
wrong digest
Azure query failure
```

No qualification mutation if image proof fails.

## 9. Wrapper provenance gate

Use the already-governed non-self-invalidating provenance model.

Required:

```text
governed wrapper baseline = 4822f9847a90a7d86c6bf771603defe9d7abf258
current HEAD descends from baseline
initialize-qualification.ps1 is tracked at HEAD
worktree wrapper content equals committed wrapper at HEAD
```

Current expected HEAD:

```text
2532f6abd4677edfb205c26c083a534783038979
```

If provenance fails, STOP.

Do not edit or republish the wrapper under this authority.

## 10. Temporary qualification settings

Only after every preflight gate passes, temporarily apply the minimum qualification settings required by the governed helper/runtime.

The exact settings must include only those required by the existing qualification contract, such as:

```text
Worker__Mode = PersistentSqliteQualification
PersistentSqliteQualification__Phase = initialize
PersistentSqliteQualification__HttpEvidenceEnabled = true
PersistentSqliteQualification__RunId = <fresh-run-id>
PersistentSqliteQualification__HttpEvidenceToken = <temporary-secret>
```

If the existing governed helper requires an evidence-output path, use the already-governed path under persistent `/home`.

Do not introduce any new setting names.

Do not configure:

```text
TwelveData__ApiKey
```

## 11. Qualification attempt

Use the existing governed WP04 Azure qualification helper/wrapper.

Preferred execution surface:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Do not manually reconstruct a second qualification implementation if the governed helper is valid.

Do not bypass the helper with direct Azure mutations unless the helper itself explicitly performs those governed mutations.

Do not use direct SQLite shell or Python inspection.

Do not use Kudu `/home` retrieval.

## 12. HTTP evidence retrieval

Retrieve only the record attributable to the exact fresh RunId.

Canonical payload fields must be exactly:

```text
RecordVersion
Phase
RunId
DatabasePathIdentity
SchemaVersion
JournalMode
AcceptedEvidenceIdentity
AcceptedEvidenceCount
IntegrityCheck
QuickCheck
PersistenceContinuity
```

Required initialize-phase values:

```text
Phase = initialize
RunId = exact fresh RunId
DatabasePathIdentity = aiquant.db
SchemaVersion = 4
JournalMode = delete
AcceptedEvidenceCount = 1
IntegrityCheck = ok
QuickCheck = ok
```

`AcceptedEvidenceIdentity` must be non-empty and must be preserved for later continuity comparison.

Do not assign reopen/redeploy continuity credit yet.

## 13. HTTP retry/timeout contract

Preserve the already-governed retry semantics.

Retryable HTTP statuses:

```text
404
503
```

Transport state may be retried only if already governed by the helper.

Generic 5xx retry is forbidden.

Timeout must remain bounded by the already-governed hard deadline.

Do not extend the timeout ad hoc.

## 14. Success requirement

Initialize qualification passes only if:

```text
exact fresh RunId record retrieved
payload shape valid
phase = initialize
database identity = aiquant.db
schema = 4
journal = delete
accepted evidence count = 1
accepted evidence identity non-empty
integrity = ok
quick-check = ok
helper exits success
qualification settings restored
normal configuration restored
```

If any condition fails:

```text
INITIALIZE_D3 = NOT_PROVEN
```

Return the exact sanitized failure class.

Do not retry with a second RunId under this authority.

## 15. Restoration

Restoration is mandatory after the attempt, whether success or failure.

Restore the exact pre-attempt app-setting state.

Required post-state:

```text
Worker__Mode = absent
PersistentSqliteQualification__Phase = absent
PersistentSqliteQualification__HttpEvidenceEnabled = absent
PersistentSqliteQualification__EvidenceOutputPath = absent unless it existed in pre-state
PersistentSqliteQualification__RunId = absent
PersistentSqliteQualification__HttpEvidenceToken = absent
TwelveData__ApiKey = absent
WEBSITES_PORT = 8501
linuxFxVersion = exact governed digest
startup override = none
SCM basic auth = false
FTP basic auth = false
registry username/password = absent
diagnostic logging = disabled
```

Do not attempt to make the normal public root healthy.

The expected downstream `503` may return after restoration and is not a WP04 failure.

## 16. Secret hygiene

Never print:

```text
qualification token
TwelveData key
registry password
connection strings
Authorization headers
cookies
raw app-settings dump
environment dump
```

For the qualification token, print only:

```text
generated = true
length/entropy-safe metadata if needed
restored/removed = true
```

Do not persist the token in repository files or prompt artifacts.

## 17. Mutation scope

This authority permits only the exact Azure mutations required by the existing governed qualification helper for one initialize attempt and its restoration.

No source mutation.

No image mutation.

No Git/GitHub lifecycle mutation.

Count every actual Azure mutation exactly.

At minimum distinguish:

```text
qualification settings application
restart(s) actually performed by governed helper
qualification settings restoration
```

Do not claim a fixed mutation count before execution; return the actual count.

## 18. Explicitly forbidden

Do not:

```text
configure TwelveData__ApiKey
run reopen
run redeploy continuity
reuse a failed RunId
create a second RunId after failure
change source
change Dockerfile
build image
push GHCR image
change WEBSITES_PORT
change linuxFxVersion
change startup command
change registry credentials
enable SCM basic auth
enable FTP basic auth
enable diagnostic logging unless already required by the governed helper contract
query SQLite directly
use Python to inspect SQLite
use Kudu /home
open/merge a PR
close #263
set Project #2 Done
start WP05
```

## 19. Initialize result classification

Return exactly one:

```text
Q1 — INITIALIZE_D3_PROVEN
Q2 — PRECONDITION_BLOCKED
Q3 — QUALIFICATION_EXECUTION_FAILED
Q4 — EVIDENCE_RETRIEVAL_FAILED
Q5 — RESTORATION_FAILED
```

### Q1

Requires exact D3 record plus successful restoration.

### Q2

Any fail-closed precondition before mutation.

### Q3

Qualification runtime failed before valid evidence retrieval.

### Q4

Runtime may have started but exact fresh RunId evidence was not validly retrieved.

### Q5

Any failure to restore exact pre-state.

If Q5 occurs, restoration takes priority over further work.

## 20. Post-success boundary

If and only if:

```text
Q1 — INITIALIZE_D3_PROVEN
```

then:

```text
fresh initialize acceptance credit = granted
reopen/redeploy continuity = still NOT_PROVEN
WP04 final acceptance = NOT_YET_COMPLETE
```

Next authority must be a separate Luna reconciliation or Terra continuity qualification authority, according to project governance.

Do not automatically run reopen.

## 21. Lifecycle boundary

Issue #263 remains open.

Project #2 remains not Done.

Milestone remains open.

No PR is authorized yet.

WP05 does not begin yet.

WP04 may close only after the exact final WP04 acceptance marker and lifecycle authority.

## 22. Required return evidence

Return:

- current HEAD;
- wrapper baseline/ancestry/tracked/worktree proof;
- image preflight result;
- port/startup/registry/SCM/FTP/logging pre-state;
- qualification-setting absence pre-state;
- fresh RunId;
- sanitized actual Azure mutation list/count;
- restart count;
- exact HTTP evidence payload with no secrets;
- initialize D3 proof;
- accepted-evidence identity for future continuity comparison;
- helper terminal result/class/exit code/elapsed if emitted;
- settings-restoration proof;
- final image/port/startup/auth/logging state;
- expected downstream normal-root status if observed, clearly non-gating;
- exact Q1–Q5 classification;
- qualification attempts = 1;
- source/Git/GitHub mutations = 0.

## 23. Terminal markers

Required:

`RELEASE 1.12 WP04 — FRESH AZURE INITIALIZE QUALIFICATION: <PASS|BLOCKED|FAIL>`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 2532f6abd4677edfb205c26c083a534783038979`

`RELEASE 1.12 WP04 — CURRENT DEPLOYED IMAGE: sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f`

`RELEASE 1.12 WP04 — ACCEPTANCE BOUNDARY: PERSISTENCE_SPECIFIC`

`RELEASE 1.12 WP04 — TWELVE DATA SECRET CONFIGURATION: FORBIDDEN`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR 503: EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER`

`RELEASE 1.12 WP04 — WRAPPER PROVENANCE GATE: <PASS|FAIL>`

`RELEASE 1.12 WP04 — DEPLOYED IMAGE PREFLIGHT: <PASS|FAIL>`

`RELEASE 1.12 WP04 — QUALIFICATION PRE-STATE: <PASS|FAIL>`

`RELEASE 1.12 WP04 — FRESH INITIALIZE RUN ID: <run-id|NONE>`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — SCHEMA VERSION: <4|NOT_PROVEN>`

`RELEASE 1.12 WP04 — JOURNAL MODE: <delete|NOT_PROVEN>`

`RELEASE 1.12 WP04 — INTEGRITY CHECK: <ok|NOT_PROVEN>`

`RELEASE 1.12 WP04 — QUICK CHECK: <ok|NOT_PROVEN>`

`RELEASE 1.12 WP04 — ACCEPTED EVIDENCE COUNT: <1|NOT_PROVEN>`

`RELEASE 1.12 WP04 — ACCEPTED EVIDENCE IDENTITY: <sanitized-identity|NOT_PROVEN>`

`RELEASE 1.12 WP04 — QUALIFICATION SETTINGS RESTORATION: <PASS|FAIL>`

`RELEASE 1.12 WP04 — QUALIFICATION ATTEMPTS: <0|1>`

`RELEASE 1.12 WP04 — AZURE MUTATION COUNT: <actual-count>`

`RELEASE 1.12 WP04 — SOURCE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — INITIALIZE QUALIFICATION RESULT: <Q1|Q2|Q3|Q4|Q5>`

`RELEASE 1.12 WP04 — REOPEN/REDEPLOY CONTINUITY: NOT_PROVEN`

`RELEASE 1.12 WP04 — FINAL ACCEPTANCE: NOT_YET_COMPLETE`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — FRESH AZURE INITIALIZE QUALIFICATION MUTATION AUDIT: PASS`

If Q1:

`RELEASE 1.12 WP04 — LUNA POST-INITIALIZE QUALIFICATION RECONCILIATION AUTHORITY: READY`

Otherwise:

`RELEASE 1.12 WP04 — LUNA INITIALIZE QUALIFICATION FAILURE RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA FRESH AZURE INITIALIZE QUALIFICATION COMPLETE`

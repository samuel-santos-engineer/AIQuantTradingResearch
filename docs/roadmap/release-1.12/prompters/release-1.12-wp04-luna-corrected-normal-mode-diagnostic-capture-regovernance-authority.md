# GPT-5.6 Luna — Release 1.12 WP04 Corrected Normal-Mode Diagnostic Capture Regovenance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the contaminated C6 capture and define a corrected, normal-mode-only diagnostic capture contract.
- **GPT-5.6 Terra** — execute only the later explicitly authorized pre-state proof, bounded diagnostic mutation, normal-mode restart/capture, retrieval, and restoration.
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

Governed deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current normal front-door state:

```text
HTTP 503
origin = NOT_PROVEN
```

Previous diagnostic result:

```text
C6 — EVIDENCE INSUFFICIENT / CAPTURE CONTAMINATED BY QUALIFICATION MODE
```

Confirmed from that capture:

```text
image pull = succeeded
container creation/start = succeeded
Azure warm-up probe = succeeded
container startup failure = NOT_PROVEN
normal Streamlit startup/bind = NOT_PROVEN
normal runtime evidence = NOT_PROVEN
diagnostic restoration = PASS
final logging state = disabled
new Azure resources = 0
recurring cost = $0.00
restart count = 1
```

No source/image remediation is justified by the contaminated capture.

## 2. Contamination finding

The prior archive contains qualification-mode evidence, including:

```text
qualification HTTP evidence mode; Streamlit suppressed
Worker qualification events
HTTP evidence listener on port 8501
historical qualification runs
multiple historical image identities
```

Therefore:

```text
PRIOR ARCHIVE ≠ NORMAL-RUNTIME STARTUP EVIDENCE
```

The fact that the instrumented image started and passed Azure warm-up in qualification mode must not be promoted to proof of normal Streamlit startup.

## 3. Corrected objective

Define a fresh diagnostic capture that proves, **before capture**, that all qualification-mode controls are absent and then captures only a newly bounded normal-mode startup window.

The corrected capture must answer:

```text
Did the normal-mode container start?
Did entrypoint remain in normal mode?
Did Worker start normally?
Did Streamlit launch?
Did Streamlit bind/listen on 0.0.0.0:8501?
Did either process fail?
Did Azure warm-up succeed?
Did the normal public root become healthy or remain 503?
```

No qualification execution is permitted.

## 4. Mandatory qualification-setting absence gate

Before enabling diagnostics or restarting, the future Terra authority must read the exact current app-setting state and prove absence of **all** WP04 qualification controls.

At minimum prove absent/unset:

```text
Worker__Mode
PersistentSqliteQualification__HttpEvidenceEnabled
PersistentSqliteQualification__EvidenceOutputPath
PersistentSqliteQualification__RunId
PersistentSqliteQualification__HttpEvidenceToken
```

Also inspect the current source/helper definitions for any additional qualification-specific setting names introduced by WP04 and include every such setting in the gate.

For every qualification setting return only:

```text
SETTING_NAME = ABSENT
```

Do not print secret values.

If any qualification setting is present, even blank unless source semantics explicitly prove blank is equivalent to absent:

```text
NORMAL_MODE_PRECONDITION = FAIL
```

STOP before logging mutation/restart.

Do not remove the setting under this capture authority. Return for Luna recovery governance.

## 5. Normal-mode precondition

Before diagnostic mutation require all:

```text
qualification settings = absent
WEBSITES_PORT = 8501
linuxFxVersion = exact governed digest
startup override = none
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
App Service state = Running
```

Classify:

```text
NORMAL_MODE_PRECONDITION = PASS | FAIL
```

Only `PASS` permits capture.

## 6. Fresh capture isolation

The corrected capture must not rely on historical archive content as primary evidence.

It must establish a fresh lower time boundary immediately before the governed normal-mode restart.

Required:

```text
CAPTURE_START_UTC = <timestamp>
```

Then perform at most one restart and retrieve only records attributable to:

```text
timestamp >= CAPTURE_START_UTC
```

If the retrieval surface cannot reliably isolate new records from historical records, the future Terra authority must use another safe isolation mechanism supported by the no-cost filesystem logging surface, or STOP.

Do not classify historical qualification records as part of the new capture.

## 7. Image identity isolation

Within the fresh capture, only runtime records attributable to the governed image are acceptance evidence:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Historical `17da...` records are explicitly excluded.

If log lines cannot be associated with the new restart/image/window sufficiently to avoid ambiguity:

```text
CAPTURE_ISOLATION = FAIL
```

Return C6 again rather than guessing.

## 8. Diagnostic surface

Governed primary surface remains:

```text
temporary App Service filesystem container/application logging
```

Defaults:

```text
HTTP logging = disabled
detailed errors = disabled
failed-request tracing = disabled
new Azure resources = 0
new recurring cost = $0.00
```

Do not broaden diagnostics unless a later Luna authority explicitly does so.

## 9. Capture window

Maximum active diagnostic window:

```text
15 minutes
```

Expected flow:

```text
1. capture pre-state
2. prove qualification settings absent
3. prove normal-mode precondition
4. enable only governed filesystem/container application logging
5. record fresh capture boundary
6. perform exactly one restart
7. observe startup
8. issue bounded normal root request(s)
9. retrieve only fresh-window logs
10. classify evidence
11. restore logging
12. prove restoration
```

## 10. Restart contract

If preconditions pass:

```text
restart count = exactly 1
```

Purpose:

```text
normal-mode startup diagnostic capture
```

It is not a recovery restart and must not be repeated if the app remains 503.

## 11. Normal public HTTP observation

After restart, perform bounded read-only normal-root checks.

Allowed:

```text
GET /
```

Do not call:

```text
/internal/wp04/persistence-qualification
```

Do not send an evidence token.

Capture:

```text
status
safe headers
duration
timestamp
```

Do not dump cookies/secrets.

## 12. Normal-mode evidence targets

Look for fresh-window evidence of:

```text
entrypoint normal-mode path
Worker normal startup
Streamlit launch
Streamlit server startup
Streamlit bind/listen on 0.0.0.0:8501
Azure warm-up request/result
process exit
entrypoint failure
filesystem permission failure
Worker fatal error
Streamlit fatal error
port bind error
container termination/recycle
```

Qualification-mode markers in the fresh window are a hard failure:

```text
qualification HTTP evidence mode
Streamlit suppressed
qualification execution markers
qualification evidence listener
```

If any appear after `CAPTURE_START_UTC`:

```text
NORMAL_MODE_CAPTURE = CONTAMINATED
```

STOP classification at C6.

## 13. Interpretation classes

Return exactly one:

### C2 — ENTRYPOINT_OR_FILESYSTEM_FAILURE_PROVEN

Fresh normal-mode evidence proves entrypoint or `/home/data` setup failure.

### C3 — WORKER_FAILURE_PROVEN

Fresh normal-mode evidence proves Worker failure that disrupts runtime/container lifetime.

### C4 — STREAMLIT_STARTUP_OR_BIND_FAILURE_PROVEN

Fresh normal-mode evidence proves Streamlit launch/bind/listener failure.

### C5 — NORMAL_CONTAINER_AND_STREAMLIT_HEALTHY_FRONT_DOOR_503_PERSISTS

Require fresh proof that:

```text
normal mode active
Streamlit launched
Streamlit listening on 8501
container remains alive
```

while public root still returns 503.

### C7 — NORMAL_RUNTIME_RECOVERED

Require fresh proof that:

```text
normal mode active
Streamlit launched/listening
public root becomes non-503 healthy response
```

This does not authorize qualification.

### C6 — EVIDENCE_INSUFFICIENT_OR_CONTAMINATED

Use if:

```text
qualification settings not proven absent
fresh-window isolation fails
qualification markers appear
normal Streamlit state remains ambiguous
logs are unavailable/incomplete
```

Do not infer a cause.

## 14. Secret hygiene

Never print or return:

```text
qualification token
Twelve Data API key
registry password
connection strings
Authorization headers
cookies
raw app-settings dump
environment dump
secret-bearing exception payload
```

Presence checks only for sensitive settings.

Redact retrieved logs before returning them.

## 15. Restoration contract

Restore every diagnostic mutation to exact pre-state immediately after retrieval, even if capture fails.

Expected final state:

```text
filesystem/container application logging = disabled
HTTP logging = disabled
detailed errors = disabled
failed-request tracing = disabled
SCM basic auth = false
FTP basic auth = false
WEBSITES_PORT = 8501
linuxFxVersion = governed digest
startup override = none
registry credentials = absent
qualification settings = absent
```

Do not change qualification settings during restoration; they must have been absent before capture.

## 16. Restoration failure policy

If logging restoration fails:

```text
DIAGNOSTIC RESTORATION = FAIL
```

Stop. Only minimum restoration activity is permitted. No additional restart or diagnostic expansion.

## 17. Cost and resource gate

Require:

```text
new Azure resources = 0
new recurring cost = $0.00
plan/SKU changes = 0
```

If not provable, STOP before mutation.

## 18. Mutation accounting for future Terra execution

Expected maximum:

```text
logging enable = 1 logical diagnostic mutation
restart = 1
logging restore = 1 logical diagnostic mutation
```

Count actual Azure API/CLI mutations precisely if the logical operations expand into multiple calls.

Required zero:

```text
qualification settings mutation = 0
source mutation = 0
Git mutation = 0
Docker build = 0
GHCR publication = 0
image mutation = 0
port mutation = 0
startup-command mutation = 0
SCM/FTP mutation = 0
registry credential mutation = 0
qualification attempt = 0
```

## 19. Qualification retry boundary

Even if result is `C7`:

```text
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

A separate Luna authority is required after normal-runtime evidence is reconciled.

## 20. Failed RunId preservation

Never reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

No fresh RunId is generated because qualification mode is forbidden.

## 21. Windows PowerShell compatibility

Any future PowerShell must target:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7-only syntax/APIs.

## 22. Luna governance decision

Selected:

```text
G2 — FRESH NORMAL-MODE-ONLY FILESYSTEM DIAGNOSTIC CAPTURE
```

Binding:

```text
qualification-setting absence gate = mandatory
fresh timestamp isolation = mandatory
governed image isolation = mandatory
normal-mode proof = mandatory
filesystem/container application logging only
restart = exactly 1 after precondition PASS
capture window <= 15 minutes
historical qualification logs = excluded
qualification markers in fresh window = contamination/C6
restoration = mandatory
```

## 23. Required next Terra authority

The next authority must provide exact executable commands/procedure for:

```text
qualification-setting absence proof
normal-mode precondition proof
logging pre-state
logging enable
fresh timestamp boundary
one restart
bounded root observation
fresh log retrieval/filtering
secret redaction
C2/C3/C4/C5/C6/C7 classification
logging restoration
post-restoration proof
mutation accounting
```

It must not bundle remediation.

## 24. Terminal markers

Required:

`RELEASE 1.12 WP04 — CORRECTED NORMAL-MODE DIAGNOSTIC CAPTURE REGOVERNANCE: PASS`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 2532f6abd4677edfb205c26c083a534783038979`

`RELEASE 1.12 WP04 — CURRENT DEPLOYED IMAGE: sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f`

`RELEASE 1.12 WP04 — PRIOR DIAGNOSTIC RESULT: C6_CONTAMINATED`

`RELEASE 1.12 WP04 — NORMAL RUNTIME EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — DIAGNOSTIC GOVERNANCE: G2`

`RELEASE 1.12 WP04 — QUALIFICATION-SETTING ABSENCE GATE: MANDATORY`

`RELEASE 1.12 WP04 — FRESH CAPTURE TIME ISOLATION: MANDATORY`

`RELEASE 1.12 WP04 — FRESH CAPTURE IMAGE ISOLATION: MANDATORY`

`RELEASE 1.12 WP04 — DIAGNOSTIC PRIMARY SURFACE: APP_SERVICE_FILESYSTEM_CONTAINER_APPLICATION_LOGS`

`RELEASE 1.12 WP04 — DIAGNOSTIC CAPTURE MAX WINDOW: 15_MINUTES`

`RELEASE 1.12 WP04 — DIAGNOSTIC RESTART COUNT: 1_AFTER_PRECONDITION_PASS`

`RELEASE 1.12 WP04 — QUALIFICATION MODE DURING DIAGNOSTICS: FORBIDDEN`

`RELEASE 1.12 WP04 — HISTORICAL QUALIFICATION LOGS: EXCLUDED`

`RELEASE 1.12 WP04 — DIAGNOSTIC NEW AZURE RESOURCES: 0`

`RELEASE 1.12 WP04 — DIAGNOSTIC RECURRING COST: $0.00`

`RELEASE 1.12 WP04 — SCM BASIC AUTH: PRESERVE_FALSE`

`RELEASE 1.12 WP04 — FTP BASIC AUTH: PRESERVE_FALSE`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — CORRECTED NORMAL-MODE DIAGNOSTIC REGOVERNANCE MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA FRESH NORMAL-MODE DIAGNOSTIC CAPTURE AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA CORRECTED NORMAL-MODE DIAGNOSTIC CAPTURE REGOVERNANCE COMPLETE`

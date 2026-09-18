# GPT-5.6 Luna — Release 1.12 WP04 Bounded Diagnostic Capture Regovenance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: define the exact temporary diagnostic-capture contract required to distinguish the current normal-runtime HTTP `503`.
- **GPT-5.6 Terra** — execute only the later explicitly authorized Azure diagnostic mutation/capture/restoration procedure.
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

Current deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current normal runtime evidence:

```text
WEBSITES_PORT = 8501
CURRENT_AZURE_PORT_CONFIG = PROVEN_MATCH
IMAGE_IDENTITY = MATCH
STARTUP_OVERRIDE = NONE
REGISTRY_CREDENTIAL_STATE = ABSENT
APP_SERVICE_STATE = Running
RESOURCE_HEALTH = Unknown
NORMAL_FRONT_DOOR_HTTP = 503
HTTP_503_ORIGIN = NOT_PROVEN
CONTAINER_STARTUP_STATE = NOT_PROVEN
EXISTING_DIAGNOSTIC_LOGGING = DISABLED
```

Current qualification state:

```text
INITIALIZE D3 = NOT_PROVEN
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

Selected prior decision:

```text
R2 — BOUNDED_DIAGNOSTIC_CAPTURE_REGOVERNANCE_REQUIRED
```

## 2. Regovenance objective

Define the narrowest temporary Azure diagnostic mutation that can determine which runtime boundary fails during the current normal-mode `503`, while preserving:

```text
strict $0 infrastructure cost
SCM basic auth = false
FTP basic auth = false
public GHCR
exact deployed image digest
WEBSITES_PORT = 8501
no source mutation
no qualification attempt
no SQLite direct access
```

This Luna authority does **not** itself mutate Azure.

It must produce a complete Terra execution contract for:

1. exact diagnostic switch(es);
2. exact sink(s);
3. exact retention/duration;
4. exact capture window;
5. exact retrieval method;
6. exact redaction/secret rules;
7. exact interpretation boundary;
8. exact restoration procedure;
9. exact restoration proof;
10. exact mutation accounting.

## 3. Diagnostic question to answer

The bounded capture must discriminate among:

```text
N2 — container startup failure
N3 — entrypoint failure before Streamlit
N4 — Streamlit startup/bind failure
N5 — Worker/runtime failure terminates container
N6 — persistent /home/data ownership/preparation failure
N7 — image pull/runtime launch failure
N8 — Azure front-end/routing platform condition
N9 — App Service recycle/startup not complete
N10 — stale/broken current configuration unrelated to source
```

Do not broaden diagnostics beyond what materially helps distinguish these classes.

## 4. Preferred diagnostic surface

Prefer **container/application stdout/stderr capture** exposed through supported App Service diagnostics, because the instrumented image already emits bounded `WP04_DIAG_*` markers and normal runtime startup errors may surface there.

Preferred target order:

```text
1. App Service Linux container/application log capture to filesystem
2. supported read-only log retrieval surface for that capture
3. minimal HTTP/platform log capture only if container logs alone cannot answer routing/startup status
```

Avoid:

```text
broad Azure Monitor diagnostic export
paid Log Analytics
Application Insights
new storage account
new paid service
persistent long-retention logging
```

Strict `$0` remains binding.

## 5. Allowed temporary Azure diagnostic mutations

The future Terra authority may authorize only the smallest necessary diagnostic toggles.

Preferred mutation set:

```text
A. enable Linux/container application logging to filesystem
B. set a bounded retention/size policy if supported and required
```

Optional only if Luna proves necessary:

```text
C. enable minimal HTTP logging
D. enable detailed error messages
E. enable failed-request tracing
```

Do not enable C/D/E by default.

If container/application logging is sufficient to distinguish startup/listener/runtime failure, keep C/D/E disabled.

## 6. Explicitly forbidden diagnostic changes

Do not enable or alter:

```text
Application Insights
Log Analytics workspace
Azure Monitor paid export
blob diagnostics requiring a storage account
SCM basic auth
FTP basic auth
registry credentials
Always On
health check configuration
WEBSITES_PORT
linuxFxVersion
startup command
container image
App Service plan/SKU
resource group
region
```

Do not introduce recurring infrastructure cost.

## 7. Diagnostic sink

Preferred sink:

```text
App Service filesystem-backed application/container logs
```

Requirements:

```text
temporary
bounded
same App Service
no new Azure resource
no paid external sink
```

If Azure CLI/API exposes only a different no-cost native sink, Luna must explicitly document it before execution.

## 8. Capture duration

The logging window must be bounded.

Default maximum active diagnostic window:

```text
15 minutes
```

The future Terra authority should:

1. record pre-state;
2. enable the authorized diagnostic switch(es);
3. perform exactly one bounded runtime observation cycle;
4. retrieve logs;
5. restore all changed diagnostic settings immediately;
6. prove restoration.

Do not leave diagnostics enabled after evidence retrieval.

## 9. Runtime stimulus

The capture needs a deterministic, bounded stimulus.

Preferred:

```text
one App Service restart
```

only if required to observe container startup.

Because current failure is a persistent normal root `503`, a restart is justified **only** as part of the explicitly governed diagnostic capture—not as a blind recovery attempt.

If current platform diagnostics can capture startup without restart, prefer zero restart.

The future Terra authority must state exactly:

```text
RESTART_REQUIRED = YES | NO
```

If YES:

```text
restart count = exactly 1
```

No second restart.

No qualification mode.

No initialize/reopen RunId.

## 10. Normal-runtime-only requirement

The diagnostic capture must keep the application in normal runtime mode.

Explicitly forbidden:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__HttpEvidenceEnabled=true
temporary evidence token
qualification endpoint invocation
qualification artifact retrieval
```

The objective is to diagnose the normal front-door `503`, not to rerun WP04 qualification.

## 11. Required log markers / evidence targets

The capture should look for evidence such as:

```text
container image pull success/failure
container process start
entrypoint execution
non-root transition
/home/data preparation failure
Worker startup
Streamlit startup command
Streamlit bind/listen on 0.0.0.0:8501
process exit code
shell/entrypoint error
runtime exception
port/listener error
container recycle/termination
Azure startup timeout/routing error
```

Existing `WP04_DIAG_*` markers may be used if present, but absence does not itself prove failure unless source ordering makes that conclusion valid.

## 12. Secret hygiene

Never print, persist, or return:

```text
evidence tokens
Authorization headers
cookies
query strings containing secrets
registry passwords
connection strings
Twelve Data API keys
environment dumps
full app settings
raw secret-bearing exception content
```

If logs contain secrets, redact before returning evidence.

Allowed returned content:

```text
timestamps
safe marker names
safe process names
safe exit codes
safe HTTP status
safe Azure platform error class
safe source file/line only if no secret content
```

## 13. Retrieval method

The future Terra authority must use a supported read-only retrieval surface after logging is temporarily enabled.

Preferred:

```text
Azure CLI-supported App Service log retrieval
```

or another native App Service log retrieval method that does not require:

```text
SCM basic auth
FTP basic auth
Kudu /home access
```

Do not use Kudu to inspect custom-container `/home`.

Do not use direct SQLite/Python shell access.

## 14. Pre-state capture

Before any diagnostic mutation, Terra must record:

```text
application/container logging state
filesystem logging state
HTTP logging state
detailed-error state
failed-request tracing state
SCM basic auth
FTP basic auth
WEBSITES_PORT
linuxFxVersion
startup override
registry credential presence
App Service state
```

This forms the restoration baseline.

## 15. Restoration contract

Every diagnostic setting changed must be restored exactly to its pre-state.

Known expected final state:

```text
application/container logging = disabled
HTTP logging = disabled
detailed errors = disabled
failed request tracing = disabled
diagnostic settings = empty
SCM basic auth = false
FTP basic auth = false
WEBSITES_PORT = 8501
linuxFxVersion = governed digest
startup override = none
registry credentials = absent
```

Do not assume—prove final state.

## 16. Restoration failure policy

If restoration fails:

```text
do not perform further diagnosis
do not retry qualification
do not create additional Azure mutations except the minimum required to restore known pre-state
return BLOCKED
```

Restoration takes priority over further evidence gathering.

## 17. Mutation accounting

The future Terra authority must count each Azure mutation precisely.

Expected maximum shape if filesystem logging + one restart are required:

```text
diagnostic-enable mutation = 1
restart mutation = 1
diagnostic-restore mutation = 1
```

If logging configuration requires more than one API mutation, count exact actual operations.

No hidden “best effort” changes.

## 18. Cost gate

Before execution, confirm the chosen diagnostic path introduces:

```text
new recurring cost = $0.00
new Azure resources = 0
plan/SKU changes = 0
```

If cost cannot be proven zero, STOP.

## 19. Diagnostic interpretation matrix

After capture, classify:

### C1 — image pull / launch failure proven

Examples:

```text
manifest/image pull error
container create/start failure
runtime launch failure
```

Next likely governance:

```text
bounded Azure/runtime recovery or image publication reconciliation
```

### C2 — entrypoint / filesystem setup failure proven

Examples:

```text
/home/data permission failure
entrypoint shell failure
user transition failure
```

Next likely governance:

```text
source/runtime remediation
```

### C3 — Worker failure proven

Examples:

```text
Worker fatal exception
Worker exits and container terminates
```

Next likely governance:

```text
source/runtime remediation
```

### C4 — Streamlit startup/bind failure proven

Examples:

```text
Streamlit launch error
port bind failure
process exits
```

Next likely governance:

```text
source/runtime remediation
```

### C5 — container healthy/listening but front door still 503

Examples:

```text
container startup success
Streamlit listener confirmed
no process exit
front-door 503 persists
```

Next likely governance:

```text
Azure routing/platform reconciliation
```

### C6 — evidence insufficient

If logs still cannot distinguish failure boundary:

```text
diagnostic capture insufficient
```

Return for Luna re-governance; do not widen automatically.

## 20. Next-decision options after capture

The future Terra authority must return one evidence classification only:

```text
C1 | C2 | C3 | C4 | C5 | C6
```

Then Luna will govern the next action.

No remediation is bundled into the capture authority.

## 21. Qualification retry gate

Even if diagnostics reveal and later fix the normal-runtime issue:

```text
FRESH QUALIFICATION RETRY remains NOT_AUTHORIZED
```

until a separate Luna authority confirms:

```text
normal front-door health restored
provenance gate pass
image identity match
routing config match
no unresolved critical runtime defect
```

## 22. No source/Git/GitHub mutation

This Luna regovenance authority authorizes no mutation.

Future Terra diagnostic execution must also preserve:

```text
repository edits = 0
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
PR mutations = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
WP05 start = 0
```

## 23. Windows PowerShell compatibility

Any future PowerShell execution must target:

```text
Windows PowerShell 5.1.26100.9444
```

No PS7-only syntax/APIs.

## 24. Failed RunId preservation

Forbidden forever:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

No fresh RunId is part of this diagnostic work.

## 25. Luna decision

Selected diagnostic governance:

```text
G1 — TEMPORARY APP SERVICE FILESYSTEM CONTAINER/APPLICATION LOG CAPTURE
```

Binding defaults:

```text
filesystem/container application logging = enable temporarily
HTTP logging = remain disabled unless execution proves required
detailed errors = remain disabled
failed-request tracing = remain disabled
new Azure resources = 0
new recurring cost = $0.00
capture duration <= 15 minutes
restart = at most 1, only if required for startup capture
qualification mode = disabled
restore all changed logging state immediately after retrieval
```

## 26. Required Terra execution contract

The next authority must explicitly include:

```text
exact Azure CLI commands or equivalent APIs
pre-state queries
exact logging-enable mutation
whether one restart is required
capture timing
safe log retrieval
redaction
C1-C6 classification
exact restoration commands
post-restoration proof
exact mutation accounting
```

No generic “enable diagnostics.”

## 27. Terminal markers

Required for this Luna regovenance:

`RELEASE 1.12 WP04 — BOUNDED DIAGNOSTIC CAPTURE REGOVERNANCE: PASS`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 2532f6abd4677edfb205c26c083a534783038979`

`RELEASE 1.12 WP04 — CURRENT DEPLOYED IMAGE: sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR HTTP: 503`

`RELEASE 1.12 WP04 — DIAGNOSTIC GOVERNANCE: G1`

`RELEASE 1.12 WP04 — DIAGNOSTIC PRIMARY SURFACE: APP_SERVICE_FILESYSTEM_CONTAINER_APPLICATION_LOGS`

`RELEASE 1.12 WP04 — DIAGNOSTIC CAPTURE MAX WINDOW: 15_MINUTES`

`RELEASE 1.12 WP04 — DIAGNOSTIC NEW AZURE RESOURCES: 0`

`RELEASE 1.12 WP04 — DIAGNOSTIC RECURRING COST: $0.00`

`RELEASE 1.12 WP04 — HTTP LOGGING DEFAULT: DISABLED`

`RELEASE 1.12 WP04 — DETAILED ERROR LOGGING DEFAULT: DISABLED`

`RELEASE 1.12 WP04 — FAILED REQUEST TRACING DEFAULT: DISABLED`

`RELEASE 1.12 WP04 — DIAGNOSTIC RESTART MAXIMUM: 1`

`RELEASE 1.12 WP04 — QUALIFICATION MODE DURING DIAGNOSTICS: DISABLED`

`RELEASE 1.12 WP04 — SCM BASIC AUTH: PRESERVE_FALSE`

`RELEASE 1.12 WP04 — FTP BASIC AUTH: PRESERVE_FALSE`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — BOUNDED DIAGNOSTIC CAPTURE REGOVERNANCE MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA BOUNDED NORMAL-RUNTIME DIAGNOSTIC CAPTURE AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA BOUNDED DIAGNOSTIC CAPTURE REGOVERNANCE COMPLETE`

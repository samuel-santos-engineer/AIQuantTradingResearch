# GPT-5.6 Terra — Release 1.12 WP04 Read-Only Azure Runtime Investigation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: perform a strictly read-only Azure/runtime investigation to determine where the qualification request path fails.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Current governed source/tooling commit:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Runtime image:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Public hostname:

```text
aiqr112wp035ec325382770.azurewebsites.net
```

Container/public routing facts already reconciled:

```text
qualification mode = Worker only
Streamlit = suppressed
qualification HTTP listener = http://0.0.0.0:8501
route = GET /internal/wp04/persistence-qualification
WEBSITES_PORT = 8501
Docker EXPOSE = 8501
alwaysOn = false
health-check path = unset
App Service state = Running/Normal
```

Current qualification policy:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
outer evidence-poll wall-clock budget <= 180 seconds
```

Latest failed RunId:

```text
initialize-01512ec42d444a68b315c7f7eecbcfa4
```

Observed result:

```text
poll attempt = 1
HTTP status = NONE
failure class = Timeout
elapsed wrapper time = 40.481 seconds
evidence record = absent
```

## 2. Purpose

Perform the narrowest possible **read-only Azure/runtime investigation** to determine which of the following can be proven or excluded without mutation:

1. Azure is routing public HTTPS to the configured container port as expected.
2. The deployed container configuration matches the intended image/runtime contract.
3. The container process is starting and remaining healthy long enough for qualification mode.
4. Any currently available read-only platform metadata reveals restart/recycle/startup failures.
5. Any existing read-only deployment/runtime state contradicts the current listener assumptions.
6. A future source instrumentation step is necessary because platform evidence is insufficient.

This authority does not authorize a new qualification run.

## 3. Binding no-mutation rule

This authority is strictly read-only.

Do not perform any command that changes:

```text
App Service configuration
application settings
container settings
image
registry credentials
publishing policies
SCM/FTP authentication
logging
health check
Always On
startup command
port
plan/SKU
region
networking
deployment state
runtime state
```

Do not restart, stop, start, or redeploy the app.

Do not mutate repository/Git/GitHub state.

## 4. Allowed read-only Azure evidence surfaces

Use Azure CLI/REST only where the operation is demonstrably read-only.

At minimum inspect the following.

### A. Web app general state

Capture:

```text
az webapp show
```

Relevant fields only:

```text
state
availabilityState if exposed
defaultHostName
httpsOnly
hostNames
kind
reserved/Linux indicators
siteConfig metadata exposed read-only
```

Do not dump secret-bearing structures.

### B. Container configuration

Use Azure's documented read-only command:

```text
az webapp config container show
```

Confirm:

```text
image/repository reference
container mode
registry server if present
whether any registry username/password fields are configured
```

If the CLI response exposes secret placeholders/credentials, redact them completely.

Do not change container configuration.

### C. App settings inventory

Read current app settings only to verify expected routing/runtime keys.

Inspect only names and safe values needed for this investigation, including:

```text
WEBSITES_PORT
WEBSITES_CONTAINER_START_TIME_LIMIT if present
DOCKER_REGISTRY_SERVER_URL if present
qualification-mode temporary keys should be absent
```

Never print secret-bearing app-setting values.

For any setting with unknown sensitivity, print only:

```text
<SETTING_NAME>=PRESENT
```

or:

```text
<SETTING_NAME>=ABSENT
```

### D. Publishing policy state

Read the current basic publishing credentials policies for:

```text
scm
ftp
```

Confirm both remain disabled.

Do not change them.

### E. Deployment/runtime metadata

Use only currently available read-only surfaces to inspect:

```text
latest deployment metadata
container/image identity
restart/recycle indicators if exposed
platform status metadata
recent deployment timestamps
```

Do not enable diagnostics.

Do not trigger log streaming if that operation changes state.

If a read-only log endpoint exists but logging is disabled and returns no historical entries, record:

```text
NO_EXISTING_SERVER_SIDE_REQUEST_TELEMETRY
```

Do not enable logging.

### F. Resource / plan facts

Verify read-only:

```text
App Service Plan SKU = F1 / Free
region = West Central US
Always On = false
health-check path = unset
```

If exact field names differ, report the actual source fields.

## 5. Repository/runtime composition verification

Perform read-only repository inspection from commit:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Confirm:

- qualification mode suppresses Streamlit;
- Worker owns port 8501;
- listener starts only after SQLite qualification and artifact write;
- listener bind address is `0.0.0.0:8501`;
- route is `/internal/wp04/persistence-qualification`;
- bounded post-qualification listener lifetime;
- container entrypoint behavior in qualification mode;
- process exit behavior after retrieval/timeout.

No repository mutation.

## 6. Request-path reasoning to produce

Based only on read-only evidence, determine which statements are:

```text
PROVEN
NOT_PROVEN
CONTRADICTED
```

for:

```text
A. Azure public hostname resolves to this App Service.
B. Azure is configured to route container traffic to port 8501.
C. The deployed image is the expected exact runtime image.
D. No registry credentials are configured.
E. Qualification mode would suppress Streamlit.
F. Qualification listener would bind 0.0.0.0:8501 if process reaches listener start.
G. SQLite qualification must complete before listener start.
H. Latest failed request reached the container.
I. Latest failed request reached the listener.
J. Latest failed request reached the handler.
K. Latest qualification artifact was successfully written.
L. Container recycled during the latest attempt.
M. Worker exited before the client timeout.
N. Azure front-end timed out before listener availability.
O. 20-second per-request timeout is too short.
```

Do not mark H–O as proven without direct evidence.

## 7. Timing analysis

Reconcile the observed:

```text
40.481 seconds
```

with the known helper/runtime sequence.

State what can and cannot be inferred from:

```text
restart command/setup overhead
platform restart latency
qualification execution before listener start
single 20-second request timeout
wrapper cleanup/restoration
```

Do not convert consistency into proof.

## 8. Candidate outcomes

At completion, select exactly one:

### I1 — Platform evidence reveals a specific configuration/runtime defect

Use only if a precise defect is proven read-only.

Then return:

```text
NEXT = LUNA AZURE CONFIGURATION/RUNTIME CORRECTION RECONCILIATION
```

No correction is authorized under this authority.

### I2 — Platform state appears consistent; request-path boundary remains unobservable

Use if routing/image/settings appear correct but no existing telemetry proves whether request/listener/handler/artifact events occurred.

Then return:

```text
NEXT = LUNA DIAGNOSTIC INSTRUMENTATION RECONCILIATION
```

This is the expected result if evidence remains insufficient.

### I3 — Existing evidence proves request reached listener/handler/artifact boundary

Return the exact proven boundary and:

```text
NEXT = LUNA TARGETED REMEDIATION RECONCILIATION
```

Do not remediate yet.

## 9. Explicitly forbidden

Do not:

- rerun initialize;
- issue restart;
- issue redeploy;
- enable logs;
- alter log retention;
- enable health check;
- enable Always On;
- change request timeout;
- change retry policy;
- modify source;
- build/publish image;
- enable SCM/FTP;
- create registry credentials;
- query SQLite directly;
- use Kudu VFS for `/home`;
- create PR;
- mutate issue/Project/milestone.

## 10. Failed RunId disposition

All failed RunIds remain forbidden for reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
```

## 11. Exact mutation audit

Required zero:

```text
Azure mutations = 0
App Service lifecycle actions = 0
container configuration mutations = 0
app-setting mutations = 0
publishing-policy mutations = 0
logging mutations = 0
health-check mutations = 0
plan/SKU mutations = 0
Git mutations = 0
Docker/GHCR mutations = 0
PR mutations = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
```

## 12. Required return evidence

Return:

- exact Azure CLI/REST read-only commands used;
- App Service general-state summary;
- container configuration summary;
- safe app-setting/routing summary;
- SCM/FTP policy state;
- plan/region/Always-On/health-check facts;
- exact deployed image reference/digest evidence available from Azure;
- repository listener lifecycle summary;
- A–O proof table;
- timing reconciliation;
- selected outcome `I1`, `I2`, or `I3`;
- exact next Luna authority type;
- zero-mutation confirmation.

Never return secrets.

## 13. Terminal markers

Required:

`RELEASE 1.12 WP04 — READ-ONLY AZURE RUNTIME INVESTIGATION: PASS`

`RELEASE 1.12 WP04 — DEPLOYED IMAGE IDENTITY: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — PUBLIC-TO-CONTAINER PORT 8501 ROUTING CONFIGURATION: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — REQUEST ARRIVAL EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — LISTENER ARRIVAL EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HANDLER INVOCATION EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — QUALIFICATION ARTIFACT CREATION EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — CONTAINER RECYCLE EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_GOOD_STATE`

`RELEASE 1.12 WP04 — READ-ONLY AZURE RUNTIME INVESTIGATION MUTATION AUDIT: PASS`

Then exactly one:

`RELEASE 1.12 WP04 — READ-ONLY AZURE RUNTIME INVESTIGATION OUTCOME: I1`

or

`RELEASE 1.12 WP04 — READ-ONLY AZURE RUNTIME INVESTIGATION OUTCOME: I2`

or

`RELEASE 1.12 WP04 — READ-ONLY AZURE RUNTIME INVESTIGATION OUTCOME: I3`

And corresponding next-authority marker:

`RELEASE 1.12 WP04 — LUNA AZURE CONFIGURATION/RUNTIME CORRECTION RECONCILIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA DIAGNOSTIC INSTRUMENTATION RECONCILIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA TARGETED REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA READ-ONLY AZURE RUNTIME INVESTIGATION COMPLETE`

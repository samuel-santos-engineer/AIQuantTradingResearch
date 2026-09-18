# GPT-5.6 Terra — Release 1.12 WP04 Read-Only Normal-Runtime 503 Investigation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: execute the approved read-only normal-runtime investigation using corrected Azure queries and safe HTTP metadata inspection.
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

Current normal front-door state:

```text
DNS = PASS
TLS = PASS
normal root HTTP = 503
503 origin = NOT_PROVEN
```

Current reconciliation state:

```text
NORMAL_RUNTIME_SOURCE_PATH = PROVEN_CORRECT
DEPLOYED_IMAGE_RUNTIME_SOURCE = CONSISTENT
INTENDED_PORT_CONTRACT = PROVEN
CURRENT_AZURE_PORT_CONFIG = NOT_PROVEN
NORMAL_CONTAINER_LIFETIME_SOURCE = PROVEN_CORRECT
READ_ONLY_EVIDENCE_SUFFICIENT = YES
EXISTING_DIAGNOSTIC_LOGGING = DISABLED
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

Selected Luna decision:

```text
D1 — R1_READ_ONLY_NORMAL_RUNTIME_INVESTIGATION
```

## 2. Investigation objective

Use read-only evidence only to narrow the current normal-runtime `503`.

Specifically determine:

1. exact current `WEBSITES_PORT`;
2. exact current container image/configuration shape;
3. whether registry credentials are currently configured;
4. current resource health / operational state;
5. relevant recent activity/restart/configuration metadata;
6. safe HTTP response headers that may distinguish Azure front-end/platform behavior from application behavior;
7. whether any current read-only metadata indicates startup/recycle/container-launch failure.

No mutation is authorized.

## 3. Corrected app-settings query requirements

The prior presence logic based on:

```text
contains(value,'')
```

is invalid and must not be reused.

For `WEBSITES_PORT`, use a corrected query that distinguishes:

```text
setting absent
setting present with null
setting present with empty string
setting present with concrete value
```

Capture:

```text
WEBSITES_PORT_PRESENT = True|False
WEBSITES_PORT_VALUE = <value|NONE>
```

Required classification:

```text
CURRENT_AZURE_PORT_CONFIG = PROVEN_MATCH | PROVEN_MISMATCH | NOT_PROVEN
```

Expected intended value:

```text
8501
```

Do not mutate if missing or mismatched.

## 4. Registry credential state query

Read only current registry-related application/container settings.

Determine presence of any credentials such as:

```text
DOCKER_REGISTRY_SERVER_USERNAME
DOCKER_REGISTRY_SERVER_PASSWORD
DOCKER_REGISTRY_SERVER_URL
```

or the current equivalent Azure CLI/API properties.

Do not print secret values.

Safe output:

```text
REGISTRY_USERNAME_PRESENT = True|False
REGISTRY_PASSWORD_PRESENT = True|False
REGISTRY_URL_PRESENT = True|False
```

If a secret exists, report only presence.

Classify:

```text
REGISTRY_CREDENTIAL_STATE = ABSENT | PRESENT | NOT_PROVEN
```

## 5. Current container configuration

Read only and capture:

```text
siteConfig.linuxFxVersion
container-related siteConfig fields
startup command if any
Always On
health-check path if any
HTTPS-only
defaultHostName
```

Require exact image identity classification against:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Classify:

```text
IMAGE_IDENTITY = MATCH | MISMATCH | NOT_PROVEN
STARTUP_OVERRIDE = NONE | PRESENT | NOT_PROVEN
```

Do not alter configuration.

## 6. Resource health and operational state

Read only:

```text
App Service state
availabilityState / resource health if exposed
operationalState if exposed
plan SKU/tier
region
recent health/state transitions
```

Safe classifications:

```text
APP_SERVICE_STATE = Running|Stopped|Other|NOT_PROVEN
RESOURCE_HEALTH = Available|Degraded|Unavailable|Unknown|NOT_PROVEN
```

Do not treat `Running` alone as proof the container is serving traffic.

## 7. Activity metadata

Inspect read-only Azure Activity Log and directly exposed site metadata for a relevant recent window.

Capture, as available:

```text
configuration writes
restart requests
restart completion
container/recycle-related events
site state transitions
deployment/configuration changes
platform errors
```

Do not infer events not present.

Classify each:

```text
CONFIG_UPDATE = PROVEN | NOT_PROVEN
RESTART_ACCEPTED = PROVEN | NOT_PROVEN
RESTART_COMPLETED = PROVEN | NOT_PROVEN
CONTAINER_RECYCLE = PROVEN | NOT_PROVEN
PLATFORM_ERROR_EVENT = PROVEN | NOT_PROVEN
```

## 8. Normal front-door HTTP metadata

Perform only read-only GET/HEAD requests against normal public paths.

Allowed:

```text
/
```

and, if already part of normal runtime and non-mutating, one lightweight normal public health/readiness path.

Do not call:

```text
/internal/wp04/persistence-qualification
```

Do not send evidence token.

Capture safe metadata:

```text
HTTP status
Server header if present
Date header
Retry-After if present
x-ms-* headers if present
ARR-affinity-related headers if present
request duration
redirect behavior
```

Do not log cookies or tokens.

Do not dump raw bodies unless a tiny generic platform error body is clearly safe; prefer headers/status only.

## 9. 503 source hints

Using headers/status only, classify whether the response provides evidence of:

```text
Azure front-end generated response
application-generated response
platform-routing failure
unknown origin
```

Allowed result:

```text
HTTP_503_ORIGIN_HINT = AZURE_FRONT_END | APPLICATION | PLATFORM_ROUTING | UNKNOWN
```

This remains a hint unless direct proof exists.

Required separate authoritative classification:

```text
HTTP_503_ORIGIN = PROVEN | NOT_PROVEN
```

## 10. Read-only startup/container metadata

Inspect any current Azure fields that may expose:

```text
container start time
last restart time
container status
container instance identity
image pull state
startup failure
crash/recycle count
worker process state
```

Only use surfaces already available read-only.

Do not enable logging.

Do not use Kudu `/home`.

Do not use direct SQLite/Python shell evidence.

Classify:

```text
CONTAINER_STARTUP_STATE = HEALTHY | FAILED | RECYCLING | NOT_PROVEN
```

## 11. Failure class refinement

Refine these prior plausible classes:

```text
N1 — current WEBSITES_PORT missing or wrong
N2 — container startup failure
N3 — entrypoint failure before Streamlit
N4 — Streamlit startup/bind failure
N5 — Worker/runtime failure terminates container
N6 — persistent /home/data ownership/preparation failure in normal mode
N7 — image pull/runtime launch failure
N8 — Azure front-end/routing platform condition
N9 — App Service recycle/startup not complete
N10 — stale/broken current configuration unrelated to source
N11 — other source-proven defect
```

Classify each:

```text
PROVEN
PLAUSIBLE
NOT_SUPPORTED
```

Any `PROVEN` class must cite direct evidence from this read-only investigation.

## 12. Decision output

Select exactly one recommendation.

### R1 — CORRECTED_AZURE_CONFIGURATION_RECONCILIATION_REQUIRED

Use if a specific configuration mismatch is proven, e.g.:

```text
WEBSITES_PORT wrong/missing
startup override unexpected
image identity mismatch
```

Next step:

```text
Luna bounded Azure recovery governance
```

### R2 — BOUNDED_DIAGNOSTIC_CAPTURE_REGOVERNANCE_REQUIRED

Use if configuration is consistent but `503` origin remains unproven and existing read-only evidence cannot distinguish startup/listener/runtime failure.

Next step:

```text
Luna bounded diagnostic capture regovernance
```

### R3 — READ_ONLY_PLATFORM_STATE_RECHECK_REQUIRED

Use if current evidence indicates a transient/recycle condition that should be rechecked without mutation before escalating.

Next step:

```text
Terra read-only health recheck
```

### R4 — SOURCE_RUNTIME_REMEDIATION_REQUIRED

Use only if a concrete source/runtime defect is proven.

Next step:

```text
Terra exact-path source remediation
```

### R5 — NORMAL_RUNTIME_HEALTH_RESTORED

Use only if current root/front-door health has recovered read-only and no unresolved blocker remains.

Even then:

```text
fresh qualification retry still requires separate Luna authority
```

## 13. Decision preference

Prefer:

```text
R1 > R2 > R3 > R4 > R5
```

unless direct evidence establishes another order.

Do not perform a blind restart.

Do not mutate merely to test a hypothesis.

## 14. Qualification retry boundary

Regardless of read-only findings, this authority does not authorize qualification.

Required current state unless explicitly proven otherwise by later Luna governance:

```text
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

Normal-runtime health must be restored first.

## 15. PowerShell compatibility

If PowerShell is used, binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Avoid PowerShell 7-only syntax/APIs.

## 16. Forbidden actions

Required zero:

```text
source edits
staging
commit
push
Docker build
GHCR publication
Azure settings changes
Azure restart
Azure redeploy
image change
logging enablement/change
SCM auth change
FTP auth change
registry credential change
initialize attempt
reopen attempt
PR creation
issue mutation
Project #2 mutation
milestone mutation
tag/release mutation
WP05 start
```

## 17. Failed RunId preservation

Forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

No new RunId is authorized.

## 18. Required return evidence

Return:

- exact corrected `WEBSITES_PORT` presence/value proof;
- registry credential presence proof;
- exact `linuxFxVersion`;
- startup override state;
- App Service/resource health state;
- recent activity/restart findings;
- safe normal-front-door response headers/status/timing;
- `HTTP_503_ORIGIN_HINT`;
- authoritative `HTTP_503_ORIGIN` proof state;
- container startup metadata state;
- refined N1–N11 classifications;
- selected R1–R5 recommendation;
- exact next authority type;
- whether next step would mutate Azure;
- whether new source commit/image is required;
- qualification retry remains blocked;
- zero-mutation audit.

## 19. Terminal markers

Required:

`RELEASE 1.12 WP04 — READ-ONLY NORMAL-RUNTIME 503 INVESTIGATION: PASS`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 2532f6abd4677edfb205c26c083a534783038979`

`RELEASE 1.12 WP04 — CURRENT IMAGE IDENTITY: <MATCH|MISMATCH|NOT_PROVEN>`

`RELEASE 1.12 WP04 — WEBSITES_PORT PRESENCE: <True|False|NOT_PROVEN>`

`RELEASE 1.12 WP04 — WEBSITES_PORT VALUE: <8501|OTHER|NONE|NOT_PROVEN>`

`RELEASE 1.12 WP04 — CURRENT AZURE PORT CONFIG: <PROVEN_MATCH|PROVEN_MISMATCH|NOT_PROVEN>`

`RELEASE 1.12 WP04 — REGISTRY CREDENTIAL STATE: <ABSENT|PRESENT|NOT_PROVEN>`

`RELEASE 1.12 WP04 — STARTUP OVERRIDE: <NONE|PRESENT|NOT_PROVEN>`

`RELEASE 1.12 WP04 — APP SERVICE STATE: <Running|Stopped|Other|NOT_PROVEN>`

`RELEASE 1.12 WP04 — RESOURCE HEALTH: <Available|Degraded|Unavailable|Unknown|NOT_PROVEN>`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR HTTP: <status>`

`RELEASE 1.12 WP04 — HTTP 503 ORIGIN HINT: <AZURE_FRONT_END|APPLICATION|PLATFORM_ROUTING|UNKNOWN>`

`RELEASE 1.12 WP04 — HTTP 503 ORIGIN: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — CONTAINER STARTUP STATE: <HEALTHY|FAILED|RECYCLING|NOT_PROVEN>`

`RELEASE 1.12 WP04 — EXISTING DIAGNOSTIC LOGGING: DISABLED`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — READ-ONLY NORMAL-RUNTIME 503 INVESTIGATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — READ-ONLY NORMAL-RUNTIME 503 DECISION: <R1|R2|R3|R4|R5>`

Then exactly one matching next-authority marker:

`RELEASE 1.12 WP04 — LUNA CORRECTED AZURE CONFIGURATION RECOVERY AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA BOUNDED DIAGNOSTIC CAPTURE REGOVERNANCE AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA READ-ONLY NORMAL-RUNTIME HEALTH RECHECK AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA SOURCE RUNTIME REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA QUALIFICATION RETRY GOVERNANCE AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA READ-ONLY NORMAL-RUNTIME 503 INVESTIGATION COMPLETE`

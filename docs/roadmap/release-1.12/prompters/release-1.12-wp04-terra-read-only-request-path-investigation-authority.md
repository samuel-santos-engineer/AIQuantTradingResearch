# GPT-5.6 Terra — Release 1.12 WP04 Read-Only Request-Path Investigation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: perform the approved read-only Azure/request-path investigation and return evidence without mutating source, Azure, Docker, GHCR, or lifecycle state.
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
4822f9847a90a7d86c6bf771603defe9d7abf258
```

Current published wrapper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Current qualification helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Expected deployed instrumented image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Latest failed initialize RunId:

```text
initialize-94c5801cdc0b49e5933760c02313d486
```

It is permanently non-reusable.

## 2. Luna-selected decision

Selected:

```text
D1 — R1_READ_ONLY_REQUEST_PATH_INVESTIGATION
```

Known unresolved facts:

```text
TIMEOUT_CAUSE = NOT_PROVEN
REQUEST_REACHED_LISTENER = NOT_PROVEN
HANDLER_ENTERED = NOT_PROVEN
D3 = NOT_PROVEN
QUALIFICATION_READINESS_GATE = INADEQUATE
ENTRYPOINT_QUALIFICATION_RUNTIME = PROVEN_CORRECT
```

Known plausible classes:

```text
F1 — request before listener readiness
F4 — Azure front end does not route during startup
F5 — per-request timeout too short for cold-start path
F9 — Azure startup/recycle delay
```

Known proven source issue:

```text
F10 — published wrapper embeds expectedCommit = 047e2ce8...
while current source commit = 4822f984...
```

This source-commit self-check mismatch must be reconciled before any future qualification attempt.

## 3. Investigation objective

Collect only read-only evidence that can narrow the request-path boundary without:

```text
restart
settings change
logging change
image change
redeploy
qualification run
source mutation
Git mutation
GitHub lifecycle mutation
```

The investigation must determine what can be proven today about:

1. current exact `linuxFxVersion`;
2. App Service runtime and startup/restart metadata;
3. current hostname/TLS/front-door behavior;
4. current port/routing configuration;
5. any read-only container/startup metadata exposed by Azure;
6. whether any read-only log/diagnostic surface already exists without enabling logging;
7. whether the wrapper self-check mismatch is confined to source identity validation or can affect runtime qualification flow.

## 4. Read-only Azure preflight

Read only and capture:

```text
resource group
web app name
subscription/account context
App Service state
location
SKU/tier
defaultHostName
HTTPS-only
WEBSITES_PORT
Always On
health-check configuration
linuxFxVersion
container settings exposed read-only
SCM basic auth state
FTP basic auth state
registry credential presence/absence
```

Required expected facts:

```text
App Service = Running/Normal
SKU = F1 / Free
region = West Central US
WEBSITES_PORT = 8501
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

Do not change anything if one differs.

Return the observed value and stop escalation.

## 5. Exact image identity proof

Read:

```text
siteConfig.linuxFxVersion
```

Require read-only capture of the full configured value.

Expected normalized form:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Expected raw form may be:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Classify:

```text
IMAGE_IDENTITY = MATCH | MISMATCH | NOT_PROVEN
```

Do not mutate if mismatched.

## 6. Activity/restart metadata investigation

Use only existing read-only Azure metadata.

Inspect, as available:

```text
Azure Activity Log
web app configuration update events
restart/restart-like events
site lifecycle timestamps
container instance/recycle metadata
deployment/config change timestamps
resource health/state transitions
```

Focus on the time window around the latest failed initialize attempt:

```text
initialize-94c5801cdc0b49e5933760c02313d486
```

Do not assume the exact wall-clock timestamp if not already recorded; derive only from available command history/activity evidence.

Classify whether there is read-only evidence for:

```text
restart accepted
restart completed
container recycle
configuration update
platform recovery
site state transition
```

Each classification:

```text
PROVEN
NOT_PROVEN
```

## 7. Current hostname/TLS/front-door checks

Perform read-only network observations against the normal public hostname only.

Allowed checks:

```text
DNS resolution
TCP/TLS reachability on HTTPS/443
certificate/TLS handshake metadata
HTTP response status for normal root or another non-mutating public normal-runtime path
redirect behavior
response timing
```

Do not call the governed internal WP04 qualification endpoint because qualification mode is not active.

Do not send an evidence token.

Do not attempt to infer listener status from normal runtime.

Return:

```text
DNS_RESOLUTION = PASS | FAIL
TLS_HANDSHAKE = PASS | FAIL
NORMAL_FRONT_DOOR_HTTP = PASS | FAIL
NORMAL_FRONT_DOOR_STATUS = <safe status>
```

These prove only normal front-door reachability.

## 8. Port/routing configuration audit

Read only the currently configured routing/runtime settings relevant to container ingress:

```text
WEBSITES_PORT
linuxFxVersion
siteConfig properties related to container/Linux runtime
public hostname configuration
HTTPS-only
health check path if any
Always On
```

Determine whether current configuration is structurally consistent with:

```text
container listener port = 8501
Azure front end forwarding to 8501
```

Classify:

```text
PORT_ROUTING_CONFIG = CONSISTENT | DEFECT_PROVEN | NOT_PROVEN
```

This classification is configuration-only, not proof that the prior request reached the listener.

## 9. Existing diagnostic surfaces

Without enabling anything, inspect whether current read-only surfaces expose:

```text
container stdout/stderr
application logs
filesystem logs
HTTP logs
detailed errors
failed request tracing
diagnostic settings
log stream availability metadata
container startup logs
```

Classify each:

```text
AVAILABLE
CONFIGURED_BUT_EMPTY
DISABLED
NOT_PROVEN
```

If a surface is available read-only, retrieve only safe relevant lines and redact secrets.

Do not enable logging.

Do not change retention.

Do not use Kudu to retrieve custom-container `/home`.

## 10. Source-commit self-check mismatch audit

Read-only inspect the published wrapper at:

```text
4822f9847a90a7d86c6bf771603defe9d7abf258
```

Capture the exact embedded expected commit.

Known expected finding:

```text
embedded expectedCommit = 047e2ce8e3c614495f8c05cbe01d0b75617d56a7
actual current source commit = 4822f9847a90a7d86c6bf771603defe9d7abf258
```

Determine whether this mismatch:

```text
A — blocks wrapper before Azure mutation
B — is only advisory/printed
C — can be bypassed or ignored
D — other
```

Do not fix it here.

Classify:

```text
WRAPPER_SOURCE_COMMIT_SELF_CHECK = BLOCKING | NON_BLOCKING | NOT_PROVEN
```

## 11. Qualification readiness evidence review

Read source only.

Reconfirm:

```text
helper begins request loop immediately after restart flow
no separate qualification-listener readiness gate exists
listener starts only after SQLite qualification/artifact write
```

Classify:

```text
READINESS_RACE_POSSIBILITY = PLAUSIBLE | NOT_SUPPORTED
```

Do not mutate code.

Do not run qualification.

## 12. Timing evidence

Using source and prior observed evidence only, correlate:

```text
per-request timeout = 20 seconds
helper elapsed = 36.282 seconds
restart/setup occurs before request
```

Return:

```text
OBSERVED_36S_CONSISTENT_WITH_RESTART_PLUS_ONE_TIMEOUT = YES | NO | INDETERMINATE
TIMEOUT_CAUSE = NOT_PROVEN
```

Do not upgrade consistency to causation.

## 13. Read-only boundaries

Explicitly preserve:

```text
REQUEST_REACHED_AZURE_FRONT_END = NOT_PROVEN unless direct existing evidence proves it
REQUEST_REACHED_CONTAINER = NOT_PROVEN unless direct existing evidence proves it
LISTENER_BOUND_IN_AZURE = NOT_PROVEN unless direct existing evidence proves it
HANDLER_ENTERED = NOT_PROVEN unless direct existing evidence proves it
D3_ARTIFACT_EXISTED = NOT_PROVEN unless direct existing evidence proves it
```

Normal front-door HTTP success does not prove qualification-mode routing.

## 14. Forbidden actions

Required zero:

```text
Azure settings mutations
Azure restart
Azure redeploy
Azure image change
logging enablement/change
SCM/FTP policy change
registry credential change
qualification helper execution
initialize attempt
reopen attempt
source edits
staging
commit
push
Docker build
GHCR publication
PR creation
issue mutation
Project #2 mutation
milestone mutation
tag/release mutation
WP05 start
```

## 15. Windows PowerShell compatibility

If PowerShell is used for read-only inspection, use:

```text
Windows PowerShell 5.1.26100.9444
```

Do not introduce PS7-only syntax in any transient validation snippets intended to represent project-compatible commands.

## 16. Failed RunId preservation

Forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

Do not generate a fresh RunId.

## 17. Investigation decision output

At the end, classify the evidence and choose one recommendation only:

### R1 — SOURCE_COMMIT_SELF_CHECK_REMEDIATION_REQUIRED

Use if the wrapper's stale expected commit blocks or invalidates future qualification.

Expected next step:

```text
Terra one-path wrapper source-commit self-check remediation
```

### R2 — QUALIFICATION_READINESS_REMEDIATION_REQUIRED

Use if source evidence plus current platform facts make the missing qualification-listener readiness gate the narrowest concrete defect.

Expected next step:

```text
Terra one-path readiness remediation
```

### R3 — DIAGNOSTIC_CAPTURE_REGOVERNANCE_REQUIRED

Use if existing read-only evidence cannot distinguish front-end/container/listener boundaries and temporary bounded logging is the narrowest next evidence source.

Expected next step:

```text
Luna diagnostic capture regovernance
```

### R4 — FRESH_INITIALIZE_RETRY_CAN_BE_GOVERNED

Use only if:

```text
wrapper self-check is reconciled
exact image identity is proven
current routing config is consistent
no concrete source defect remains
no higher-value read-only investigation remains
```

A retry is still separate authority.

### R5 — OTHER_PROVEN_DEFECT

Use only with a specific concrete proven defect and exact remediation boundary.

## 18. Decision preference

Prefer:

```text
R1 > R2 > R3 > R5 > R4
```

when multiple blockers exist.

The proven stale wrapper self-check should not be ignored merely because the current investigation is otherwise read-only.

## 19. Required return evidence

Return:

- current exact `linuxFxVersion`;
- image identity classification;
- current App Service state/SKU/region;
- current WEBSITES_PORT;
- SCM/FTP state;
- registry credential state;
- restart/activity metadata findings;
- DNS/TLS/normal front-door findings;
- port/routing configuration classification;
- current diagnostic-surface availability;
- exact embedded wrapper expected commit;
- wrapper self-check blocking/non-blocking classification;
- readiness-race classification;
- 36-second timing consistency classification;
- explicit request/listener/handler/D3 proof states;
- selected R1–R5 recommendation;
- exact next authority type;
- zero-mutation audit.

## 20. Terminal markers

Required:

`RELEASE 1.12 WP04 — READ-ONLY REQUEST-PATH INVESTIGATION: PASS`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 4822f9847a90a7d86c6bf771603defe9d7abf258`

`RELEASE 1.12 WP04 — CURRENT IMAGE IDENTITY: <MATCH|MISMATCH|NOT_PROVEN>`

`RELEASE 1.12 WP04 — PORT ROUTING CONFIG: <CONSISTENT|DEFECT_PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR REACHABILITY: <PASS|FAIL|NOT_PROVEN>`

`RELEASE 1.12 WP04 — EXISTING DIAGNOSTIC SURFACE: <AVAILABLE|DISABLED|NOT_PROVEN>`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-COMMIT SELF-CHECK: <BLOCKING|NON_BLOCKING|NOT_PROVEN>`

`RELEASE 1.12 WP04 — QUALIFICATION READINESS RACE: <PLAUSIBLE|NOT_SUPPORTED>`

`RELEASE 1.12 WP04 — OBSERVED 36S CONSISTENT WITH RESTART PLUS ONE TIMEOUT: <YES|NO|INDETERMINATE>`

`RELEASE 1.12 WP04 — TIMEOUT CAUSE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REQUEST REACHED LISTENER: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HANDLER ENTERED: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — READ-ONLY REQUEST-PATH INVESTIGATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — READ-ONLY REQUEST-PATH INVESTIGATION DECISION: <R1|R2|R3|R4|R5>`

Then exactly one matching next-authority marker:

`RELEASE 1.12 WP04 — TERRA WRAPPER SOURCE-COMMIT SELF-CHECK REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA QUALIFICATION READINESS REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA DIAGNOSTIC CAPTURE REGOVERNANCE AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA FRESH INITIALIZE RETRY AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA OTHER PROVEN DEFECT REMEDIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA READ-ONLY REQUEST-PATH INVESTIGATION COMPLETE`

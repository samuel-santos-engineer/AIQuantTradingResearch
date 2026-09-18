# GPT-5.6 Luna — Release 1.12 WP04 Post-Preflight Timeout Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the remaining initialize-timeout path now that fail-closed exact-image preflight is tracked and published.
- **GPT-5.6 Terra** — execute only the later explicitly authorized investigation/remediation/qualification.
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

Parent:

```text
047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

Current published fail-closed wrapper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Current expected deployed instrumented image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current helper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Latest failed initialize RunId:

```text
initialize-94c5801cdc0b49e5933760c02313d486
```

It is permanently non-reusable.

## 2. Preconditions now established

The previously defective image-preflight surface is now governed and tracked.

Established contract:

```text
image source = siteConfig.linuxFxVersion
accepted identity = exactly one DOCKER|…@sha256:<64-lowercase-hex>
digest comparison = ordinal exact equality
blank/null/whitespace/tag-only/malformed/multiple/partial/wrong digest = fail closed
Azure query failure = fail closed
failed preflight helper/settings/restart calls = 0
```

Publication:

```text
commit = 4822f9847a90a7d86c6bf771603defe9d7abf258
payload = exactly one CREATE path
remote branch tip = commit
new runtime image required = NO
```

This reconciliation therefore treats future exact-image preflight as an enforceable gate, subject to normal execution proof.

## 3. Remaining unresolved failure

The last initialize attempt completed internally and failed cleanly:

```text
HELPER_TERMINAL_RESULT = FAILURE
HELPER_TERMINAL_CLASS = Timeout
HELPER_TERMINAL_ELAPSED_MS = 36282
HELPER_EXIT = 1
SETTINGS_RESTORATION = PASS
EXTERNAL_T6 = NOT_INDICATED
INITIALIZE_D3 = NOT_PROVEN
```

Because that run had an invalid preflight surface, it receives no initialize acceptance credit.

However, its clean helper terminal telemetry remains valid evidence that:

```text
an internally completed request path terminated as Timeout
```

The cause remains:

```text
NOT_PROVEN
```

## 4. Reconciliation objective

Determine the narrowest evidence-producing next step required before another initialize qualification.

Luna must specifically resolve whether the next step should be:

1. a read-only/zero-mutation inspection of existing runtime/request-path mechanics;
2. source-only diagnostic improvement;
3. a single controlled Azure qualification retry under the newly published fail-closed wrapper;
4. another architecture change.

No Azure mutation is authorized by this Luna authority.

## 5. Mandatory source inspection scope

Read the exact source at commit:

```text
4822f9847a90a7d86c6bf771603defe9d7abf258
```

Inspect at minimum:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
container/entrypoint.sh
src/AIQuantTradingResearch.Worker/Program.cs
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
```

Inspect any directly referenced project files needed to prove listener/runtime behavior.

Do not mutate.

## 6. Required timeout-path reconstruction

Reconstruct the exact sequence from wrapper start through helper timeout:

```text
preflight image read
preflight digest equality
temporary settings application
restart
post-restart wait/readiness behavior
qualification helper invocation
HTTP request construction
request timeout
poll loop behavior
terminal Timeout mapping
settings restoration
normal runtime restoration
```

For each step, classify:

```text
PROVEN_FROM_SOURCE
PROVEN_FROM_PRIOR_EXECUTION
NOT_PROVEN
```

Do not fill gaps by assumption.

## 7. HTTP request mechanics audit

Determine exactly:

```text
request URL shape
scheme
hostname source
port behavior
path
required query parameter
required evidence-token header
Invoke-WebRequest/HTTP API used
per-request timeout value
redirect behavior
proxy behavior if any
TLS behavior
expected status handling
```

Preserve secret hygiene; do not output token values.

Determine whether a single request can consume approximately the observed timeout window.

Classify:

```text
REQUEST_TIMEOUT_CONFIGURATION = PROVEN | NOT_PROVEN
OBSERVED_36S_CONSISTENT_WITH_CONFIGURED_REQUEST_TIMEOUT = YES | NO | INDETERMINATE
```

Consistency is not causation.

## 8. Listener startup mechanics audit

Determine exactly when qualification mode:

```text
suppresses Streamlit
starts Worker-owned listener
binds 0.0.0.0:8501
writes/opens D3 artifact
transitions to LISTENER_STARTED
accepts request
shuts down
```

Classify whether source proves any dependency ordering such as:

```text
SQLite qualification completes before listener starts
artifact write completes before listener starts
listener starts before Azure front-end readiness can succeed
```

If ordering can itself create a race, describe it precisely.

Do not call it the timeout cause unless proven.

## 9. Entrypoint/runtime audit

Determine whether `container/entrypoint.sh`:

```text
correctly detects qualification HTTP mode
suppresses Streamlit only under the two required activation conditions
execs or launches Worker in a way that makes Worker the container lifetime owner
preserves port 8501
drops privileges as designed
has any shell wait/background behavior that could delay/break listener availability
```

Classify:

```text
ENTRYPOINT_QUALIFICATION_RUNTIME = PROVEN_CORRECT | DEFECT_PROVEN | NOT_PROVEN
```

## 10. Azure front-end path assumptions audit

Separate source/runtime facts from Azure platform assumptions.

Determine whether current evidence proves:

```text
front-end routed request to current container
container was ready at request time
port 8501 was actually accepting
HTTP request reached listener
listener returned any status
```

Expected default absent direct evidence:

```text
REQUEST_REACHED_LISTENER = NOT_PROVEN
HANDLER_ENTERED = NOT_PROVEN
```

Historical local Docker evidence may prove capability, not the failed Azure request path.

## 11. Diagnostic instrumentation availability

The instrumented image contains safe markers including:

```text
QUALIFICATION_ENTERED
SQLITE_QUALIFICATION_STARTED
ARTIFACT_WRITE_SUCCEEDED
LISTENER_STARTING
LISTENER_STARTED
REQUEST_ARRIVED
HANDLER_ENTERED
EVIDENCE_RETRIEVAL_SUCCEEDED
LISTENER_STOPPING
LISTENER_STOPPED
WORKER_EXITING
```

Existing Azure state historically had application/container logging disabled.

Determine:

1. whether any currently available zero-mutation/read-only Azure surface can retrieve container stdout/stderr for the failed or next run;
2. whether enabling diagnostic logging would be required;
3. whether logging enablement is a bounded reversible mutation;
4. whether an alternative application-owned evidence surface can prove startup/listener state without logging mutation.

Do not authorize logging here.

## 12. Readiness/startup interaction audit

Determine whether the wrapper/helper currently waits for a meaningful qualification-mode readiness condition after restart.

Specifically distinguish:

```text
App Service resource state = Running
front-end hostname responds
normal Streamlit readiness
qualification endpoint readiness
qualification listener startup completion
```

If current code proceeds from `Running` or generic availability directly into the evidence request, classify whether that creates an unobserved startup race.

Classification:

```text
QUALIFICATION_READINESS_GATE = ADEQUATE | INADEQUATE | NOT_PROVEN
```

## 13. Possible failure classes

Evaluate each as:

```text
PROVEN
PLAUSIBLE
NOT_SUPPORTED
```

Classes:

```text
F1 — request issued before qualification listener is ready
F2 — qualification listener never binds
F3 — Worker/container fails before listener startup
F4 — Azure front end does not route to qualification listener during restart/startup window
F5 — HTTP request timeout too short for cold-start path
F6 — hostname/scheme/path construction defect
F7 — evidence-token/query mismatch
F8 — container exits before request
F9 — Azure platform startup/recycle delay
F10 — other source-proven defect
```

No class may be labeled PROVEN without direct supporting evidence.

## 14. Candidate decisions

Select exactly one.

### D1 — R1_READ_ONLY_REQUEST_PATH_INVESTIGATION

Use when current evidence is insufficient to distinguish startup/routing/listener boundaries and useful Azure facts can be collected without mutation.

Next authority:

```text
GPT-5.6 Terra — read-only request-path/runtime investigation
```

No restart, no settings changes, no logging changes.

### D2 — R2_ONE_PATH_QUALIFICATION_READINESS_REMEDIATION

Use only if source proves the wrapper/helper lacks a required readiness gate and one source path can correct it without changing runtime image.

Expected mutation allowlist:

```text
one eng/** PowerShell path
```

No image rebuild.

### D3 — R3_DIAGNOSTIC_CAPTURE_REGOVERNANCE

Use if distinguishing the boundary requires temporary Azure diagnostic logging or another bounded observability mutation.

Next authority must be Luna governance defining exact settings, duration, restoration, and secret constraints before Terra acts.

### D4 — R4_FRESH_INITIALIZE_RETRY

Use only if all are established:

```text
fail-closed image preflight now proven
no source defect found
qualification readiness gate adequate
request construction correct
listener/runtime source path correct
no useful zero-mutation investigation remains
one fresh retry would materially discriminate the remaining boundary
```

Retry must use a new RunId and separate Terra authority.

### D5 — R5_RUNTIME_SOURCE_REMEDIATION

Use only if a concrete Worker/entrypoint/listener defect is proven from source.

This may require a new Docker image and therefore must be separately governed.

## 15. Decision preference

Prefer evidence over retry.

Use this order when applicable:

```text
D1 > D2 > D3 > D5 > D4
```

Do not choose D4 merely because preflight is now fixed.

## 16. Read-only Azure investigation content if D1 selected

Define exact future read-only queries.

Potentially useful facts include:

```text
current linuxFxVersion exact digest
App Service resource state
WEBSITES_PORT
container startup-related platform metadata
recent restart timestamps
activity-log entries already available without enabling logging
container instance/recycle metadata exposed read-only
hostname DNS resolution
TLS/HTTP front-door behavior without qualification settings
```

Do not use Kudu to retrieve custom-container `/home`; that path is already invalid for this architecture.

Do not query SQLite directly.

## 17. Readiness remediation constraints if D2 selected

Any source remediation must preserve:

```text
HTTP retry = {404,503}
transport retry = {NONE}
Timeout = terminal
total wall-clock <= 180 seconds
Windows PowerShell 5.1
secret hygiene
exact fail-closed image preflight
```

Do not convert startup readiness into broad generic 5xx retry without Luna approval.

Do not silently extend timeouts.

## 18. Diagnostic regovernance constraints if D3 selected

Any later Azure diagnostic mutation must be:

```text
temporary
minimal
reversible
bounded
secret-safe
restored in same authority
```

It must define exactly:

```text
which logging switch
which sink
retention/duration
retrieval command
redaction rules
restoration proof
mutation count
```

No broad “turn on all logging.”

## 19. Retry constraints if D4 selected

A later retry must:

```text
use commit 4822f9847a90a7d86c6bf771603defe9d7abf258 or later explicitly governed source
prove exact deployed digest before mutation
use a fresh never-before-used initialize RunId
perform exactly one initialize attempt
not perform reopen unless initialize succeeds
restore temporary settings
preserve SCM/FTP false
preserve absent registry credentials
```

No failed RunId may be reused.

## 20. Runtime remediation constraints if D5 selected

If source proves a runtime defect, specify:

```text
exact files
exact behavior change
whether schema changes = NO unless separately proven
whether Docker rebuild = YES/NO
whether GHCR publication = YES/NO
whether Azure deployment = separate authority
```

Do not mix remediation and Azure qualification in one authority.

## 21. Current mutation prohibition

Under this Luna reconciliation:

```text
repository edits = 0
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
restarts = 0
logging changes = 0
PR/lifecycle mutations = 0
```

## 22. Failed RunId preservation

Forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

The malformed historical identifier remains forbidden if encountered.

## 23. Required output

Return:

- source reconstruction of the complete qualification request path;
- exact HTTP request timeout configuration;
- qualification listener startup ordering;
- entrypoint qualification-mode result;
- readiness-gate classification;
- request/listener/handler proof states;
- F1–F10 classification;
- diagnostic-capture availability result;
- D1–D5 comparison;
- exact selected decision;
- exact next authority type;
- exact path allowlist if source remediation is selected;
- whether new source commit is required;
- whether new image is required;
- whether next step mutates Azure;
- retry/timing preservation statement;
- failed RunId preservation statement;
- zero-mutation audit.

## 24. Terminal markers

Required:

`RELEASE 1.12 WP04 — POST-PREFLIGHT TIMEOUT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — FAIL-CLOSED IMAGE PREFLIGHT: PUBLISHED`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 4822f9847a90a7d86c6bf771603defe9d7abf258`

`RELEASE 1.12 WP04 — LAST HELPER TERMINAL CLASS: Timeout`

`RELEASE 1.12 WP04 — TIMEOUT CAUSE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — REQUEST TIMEOUT CONFIGURATION: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — QUALIFICATION READINESS GATE: <ADEQUATE|INADEQUATE|NOT_PROVEN>`

`RELEASE 1.12 WP04 — REQUEST REACHED LISTENER: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HANDLER ENTERED: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — ENTRYPOINT QUALIFICATION RUNTIME: <PROVEN_CORRECT|DEFECT_PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — POST-PREFLIGHT TIMEOUT RECONCILIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — POST-PREFLIGHT TIMEOUT DECISION: <D1|D2|D3|D4|D5>`

Then exactly one matching next-authority marker:

`RELEASE 1.12 WP04 — TERRA READ-ONLY REQUEST-PATH INVESTIGATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA QUALIFICATION READINESS REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA DIAGNOSTIC CAPTURE REGOVERNANCE AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA FRESH INITIALIZE RETRY AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA RUNTIME SOURCE REMEDIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA POST-PREFLIGHT TIMEOUT RECONCILIATION COMPLETE`

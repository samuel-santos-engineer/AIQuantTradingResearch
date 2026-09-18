# GPT-5.6 Luna — Release 1.12 WP04 Repeated Azure Timeout Failure Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the repeated Azure `Timeout` failure after the hard-deadline remediation and determine the narrowest next governed investigation.
- **GPT-5.6 Terra** — execute only the later explicitly authorized investigation/remediation/publication/qualification.
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

Runtime image remains:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Binding shell:

```text
Windows PowerShell 5.1.26100.9444
```

Current helper contract:

```text
outer evidence-poll wall-clock budget <= 180 seconds
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
```

## 2. Newly observed failure

Fresh failed RunId:

```text
initialize-01512ec42d444a68b315c7f7eecbcfa4
```

Observed terminal result:

```text
poll attempt = 1
HTTP status = NONE
failure class = Timeout
helper exit = 1
elapsed wrapper time = 40.481 seconds
evidence record = absent
transport retry = none
```

Post-failure restoration:

```text
temporary qualification settings remaining = 0
App Service = Running/Normal
runtime digest unchanged
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
```

Mutation audit:

```text
temporary settings application = 1
Restart lifecycle action = 1
temporary settings restoration = 1
all other Azure/Git/Docker/GHCR/PR/lifecycle mutations = 0
```

Disposition:

```text
NOT ACCEPTED
FORBIDDEN TO REUSE
```

## 3. Historical pattern now established

The following governed attempts have failed before returning an attributable D3 evidence record:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
  historical 503-classification blocker

initialize-3dab96f04cb84902b64df269ebae3459
  historical opaque terminal/transport blocker

initialize-50a6837f6cb649ed98481a3c26831961
  classified Timeout before hard-deadline remediation

initialize-01512ec42d444a68b315c7f7eecbcfa4
  classified Timeout after hard-deadline remediation
```

The latest post-remediation failure demonstrates that the prior timing defect was corrected, but Azure still returned no HTTP response to the first evidence request.

Therefore a second identical initialize attempt is not authorized absent new evidence.

## 4. Reconciliation objective

Determine the narrowest **read-only-first** investigation needed to distinguish among:

1. application listener never becoming reachable during qualification mode;
2. App Service/container startup taking longer than the per-request timeout before first usable response;
3. front-end routing/proxy behavior preventing the qualification listener from receiving the request;
4. container/process exiting or recycling before evidence retrieval;
5. qualification listener binding/configuration defect on Azure;
6. other Azure transport/connectivity behavior that can be diagnosed without changing auth or runtime architecture.

Do not infer any of these as true without evidence.

## 5. Binding prohibitions

This Luna authority is read-only.

Do not:

```text
rerun initialize
restart App Service
redeploy image
change application settings
enable SCM basic auth
enable FTP
add registry credentials
change port configuration
change image
change logging configuration
query SQLite directly
use Kudu VFS for /home evidence
create/merge PR
change issue/Project/milestone lifecycle
begin WP05
```

No mutations are authorized.

## 6. Required read-only evidence inventory

Inspect current repository/runtime design and existing Azure-visible evidence surfaces sufficient to answer the questions below.

### A. Qualification-mode process lifecycle

Determine from committed source/container composition:

- exact process launched in qualification mode;
- whether Streamlit is suppressed;
- which process owns port `8501`;
- when the HTTP listener starts relative to SQLite qualification execution;
- whether the listener starts before, during, or only after evidence artifact creation;
- whether the worker can exit before the listener becomes externally reachable;
- exact bounded lifetime after qualification;
- whether first request can race listener startup.

### B. Listener binding

Determine:

```text
scheme
host binding
port
path
```

for:

```text
GET /internal/wp04/persistence-qualification
```

Confirm whether it binds to:

```text
0.0.0.0:8501
```

or another interface.

Confirm whether any host filtering / URL prefix / ASP.NET/Kestrel configuration could reject Azure front-end traffic before handler execution.

### C. Azure routing contract

Using read-only Azure configuration, verify:

- App Service container port configuration;
- exposed container port;
- public hostname/HTTPS routing path;
- whether Azure forwards public HTTPS traffic to container port 8501;
- whether any health-check/probe configuration affects startup/recycling;
- whether Always On is available/enabled on F1;
- whether startup timeouts or platform warm-up limits are relevant;
- whether the app is restarting/recycling during the attempt.

Do not mutate anything.

### D. Existing logs/telemetry

Use only already-enabled, read-only log/telemetry surfaces.

Determine whether the latest RunId or qualification mode startup left evidence showing:

- container start;
- worker qualification-mode activation;
- SQLite qualification execution;
- evidence artifact write success/failure;
- HTTP evidence listener start;
- listener shutdown;
- process exit code;
- container restart/recycle;
- request arrival;
- handler invocation;
- timeout before request arrival.

Do not enable new logging.

Do not expose secrets.

### E. Timing reconciliation

Explain the observed:

```text
40.481 second wrapper duration
```

against:

- helper request timeout;
- Azure restart delay;
- first HTTP request timing;
- listener startup timing;
- outer 180-second deadline.

Determine whether the ~40-second duration is consistent with a single terminal request timeout plus restart/setup overhead.

## 7. Candidate decisions

Evaluate exactly these options.

### R1 — Read-only Azure/runtime investigation only

Choose when current evidence can likely distinguish routing/startup/process behavior without source or Azure mutation.

### R2 — One-path diagnostic source instrumentation

Choose only if current logs cannot prove where the request path stops and the narrowest safe remediation is to add sanitized lifecycle markers to existing app/container output.

If selected, instrumentation must not expose:

```text
token
header
URL query secret
response body
credentials
SQLite contents
```

### R3 — Request-timeout adjustment only

Choose only if evidence proves the endpoint/listener becomes reachable later than the current request timeout but within the governed 180-second bound.

Do not choose based only on the existence of a Timeout.

### R4 — Qualification listener/runtime composition correction

Choose only if evidence proves the listener lifecycle/binding is defective on Azure.

### R5 — Azure configuration correction

Choose only if read-only evidence proves a specific App Service routing/port/startup configuration defect.

### R6 — Repeat identical initialize

Reject unless new evidence makes repetition diagnostically meaningful.

## 8. Required decision

Select exactly one:

```text
D1 — R1_READ_ONLY_AZURE_RUNTIME_INVESTIGATION
D2 — R2_ONE_PATH_DIAGNOSTIC_INSTRUMENTATION
D3 — R3_REQUEST_TIMEOUT_ADJUSTMENT
D4 — R4_LISTENER_RUNTIME_COMPOSITION_CORRECTION
D5 — R5_AZURE_CONFIGURATION_CORRECTION
D6 — R6_REPEAT_IDENTICAL_INITIALIZE
```

Prefer the narrowest evidence-producing step.

`D6` requires extraordinary justification and should not be selected merely because the app returned to `Running/Normal`.

## 9. Source scope if later source change is selected

If `D2`, `D3`, or `D4` is selected, Luna must define an exact path allowlist and operation count.

Do not authorize a multi-path change unless the inspected architecture proves it is necessary.

If diagnostic instrumentation is selected, prefer the smallest existing path that can emit sanitized lifecycle markers.

Any later PowerShell change must remain compatible with:

```text
Windows PowerShell 5.1.26100.9444
```

## 10. Azure scope if later Azure correction is selected

If `D5` is selected, define exactly:

- setting/resource to change;
- old value;
- new value;
- why it is necessary;
- restoration/rollback plan;
- mutation count;
- why it preserves strict $0 F1 constraints.

Do not authorize broad configuration changes.

## 11. Qualification policy preservation

Until Luna explicitly changes it:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
outer evidence-poll bound = 180 seconds max
```

Do not silently broaden retries.

## 12. Runtime/image preservation

Unless the selected decision explicitly proves a runtime code correction is required:

```text
runtime digest remains
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

If only `eng/` changes later:

```text
new source commit = yes
new image = no
```

If application/container code changes later:

Luna must explicitly decide whether a new image is required.

## 13. Failed RunId disposition

All failed RunIds remain forbidden for reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
```

No future attempt may reuse them.

## 14. Mutation prohibition under this Luna authority

Required zero:

```text
Repository mutations = 0
Git mutations = 0
Docker/GHCR mutations = 0
Azure mutations = 0
Provider mutations = 0
PR mutations = 0
Issue mutations = 0
Project #2 mutations = 0
Milestone mutations = 0
Tag/release mutations = 0
```

## 15. Required output

Return:

- exact qualification-mode process/listener lifecycle;
- exact listener host/port/path;
- exact Azure public-to-container routing contract;
- existing read-only logs/telemetry relevant to the latest RunId;
- whether request arrival can be proven;
- whether handler invocation can be proven;
- whether evidence artifact creation can be proven;
- whether process/container exit/recycle can be proven;
- timing reconciliation for 40.481 seconds;
- R1–R6 comparison;
- exact D1–D6 decision;
- exact next Terra scope;
- exact path/config allowlist if applicable;
- whether new source commit is required;
- whether new image is required;
- retry policy preservation statement;
- failed RunId disposition;
- zero-mutation confirmation.

## 16. Terminal markers

Required:

`RELEASE 1.12 WP04 — REPEATED TIMEOUT FAILURE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — REPEATED TIMEOUT DECISION: <D1|D2|D3|D4|D5|D6>`

`RELEASE 1.12 WP04 — REQUEST ARRIVAL EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HANDLER INVOCATION EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — QUALIFICATION ARTIFACT CREATION EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — PROCESS/CONTAINER EXIT-RECYCLE EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_GOOD_STATE`

`RELEASE 1.12 WP04 — REPEATED TIMEOUT RECONCILIATION MUTATION AUDIT: PASS`

Then exactly one next-authority marker:

`RELEASE 1.12 WP04 — TERRA READ-ONLY AZURE RUNTIME INVESTIGATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA DIAGNOSTIC INSTRUMENTATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA REQUEST TIMEOUT REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA LISTENER RUNTIME CORRECTION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA AZURE CONFIGURATION CORRECTION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA REPEATED TIMEOUT FAILURE RECONCILIATION COMPLETE`

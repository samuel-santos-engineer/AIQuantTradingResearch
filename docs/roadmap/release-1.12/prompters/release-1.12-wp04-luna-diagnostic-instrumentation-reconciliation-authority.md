# GPT-5.6 Luna — Release 1.12 WP04 Diagnostic Instrumentation Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the minimum diagnostic instrumentation required to make the Azure qualification request path observable without widening architecture or retry behavior.
- **GPT-5.6 Terra** — execute only the later explicitly authorized implementation, validation, publication, image publication if required, and Azure qualification.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current governed source/tooling commit:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Current deployed runtime image:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Current branch:

```text
release/1.12-wp04-persistent-sqlite
```

Current PowerShell compatibility baseline:

```text
Windows PowerShell 5.1.26100.9444
```

Current qualification policy:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
outer evidence-poll wall-clock budget <= 180 seconds
```

## 2. Proven read-only Azure/runtime state

The preceding Terra read-only investigation established:

```text
App Service = Linux container
state = Running/Normal
HTTPS only = true
hostname = aiqr112wp035ec325382770.azurewebsites.net
WEBSITES_PORT = 8501
Docker EXPOSE = 8501
plan = Linux F1 / Free
region = West Central US
alwaysOn = false
health-check path = unset
startup command override = absent
storage enabled = true
temporary qualification settings = absent
registry credential settings = absent
SCM basic publishing policy = false
FTP basic publishing policy = false
existing application logging = disabled
existing HTTP logging = disabled
existing detailed-error logging = disabled
existing failed-request logging = disabled
```

Exact deployed image identity is proven by App Service configuration:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Repository lifecycle is also proven:

```text
qualification mode starts Worker only
Streamlit is suppressed
Worker performs SQLite qualification
Worker atomically writes qualification evidence artifact
Worker then starts HTTP listener
listener binds 0.0.0.0:8501
route = GET /internal/wp04/persistence-qualification
listener lifetime after qualification = bounded to 180 seconds
entrypoint exits with Worker
```

But the following remain unobservable:

```text
request arrival = NOT_PROVEN
listener arrival = NOT_PROVEN
handler invocation = NOT_PROVEN
qualification artifact creation = NOT_PROVEN
container recycle = NOT_PROVEN
Worker early exit = NOT_PROVEN
front-end timeout boundary = NOT_PROVEN
20-second request timeout insufficiency = NOT_PROVEN
```

Read-only investigation outcome:

```text
I2 — Platform state appears consistent; request-path boundary remains unobservable
```

## 3. Failed RunIds — forbidden forever

Do not reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
```

If the duplicated historical identifier above does not match repository evidence, report the discrepancy but do not mutate anything.

Canonical known failed identifiers that must remain forbidden include at minimum:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
```

## 4. Reconciliation objective

Determine the narrowest safe source instrumentation that can distinguish, on one later Azure qualification attempt, at least these boundaries:

```text
A. qualification process entered
B. SQLite qualification started
C. qualification artifact write succeeded
D. HTTP listener startup was attempted
E. HTTP listener successfully bound/listened on 0.0.0.0:8501
F. HTTP request reached application process
G. HTTP request reached qualification handler
H. evidence retrieval succeeded
I. listener shutdown reason
J. Worker/container process exit reason
```

The instrumentation must be:

```text
sanitized
deterministic
run-attributable where safe
minimal
application-owned
stdout/stderr compatible
non-secret-bearing
independent of disabled Azure logging settings
```

The objective is observability only. Do not change qualification semantics, persistence semantics, routing, retry policy, timeout policy, or ownership boundaries unless the code inspection proves instrumentation cannot be added without doing so.

## 5. Binding security and privacy boundary

Diagnostic output must never include:

```text
evidence token
X-WP04-Evidence-Token header value
request headers
full request URL including query string
response body
SQLite contents
raw evidence JSON
connection strings
registry credentials
Azure credentials
environment-variable dumps
raw exception messages if they may contain sensitive content
stack traces by default
```

Safe fields may include:

```text
fixed event name
phase
sanitized RunId
listener host
listener port
route template without query string
HTTP method
sanitized outcome class
process exit reason
elapsed milliseconds
artifact-write boolean
listener-start boolean
request-arrival boolean
handler-invocation boolean
retrieval-success boolean
```

RunId itself is not a secret, but instrumentation must not combine it with token/header/query output.

## 6. Architecture preservation

Preserve exactly:

```text
Domain/Application ownership
Infrastructure SQLite/provider mechanics
Worker outer composition
Python/Streamlit no SQLite ownership
schema v4
SQLite DELETE journal
/home/data/aiquant.db
application-owned evidence
Q1 qualification-mode single-listener substitution
0.0.0.0:8501 listener
GET /internal/wp04/persistence-qualification
Streamlit suppressed during qualification HTTP mode
```

Do not introduce:

```text
direct SQLite shell/Python evidence
Kudu VFS evidence
new persistence implementation
Azure-specific second persistence layer
new telemetry service
paid Azure service
new network listener
new route
new port
new authentication mechanism
```

## 7. Required code inspection

Inspect only the committed source at:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

At minimum inspect:

```text
src/AIQuantTradingResearch.Worker/Program.cs
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
container/entrypoint.sh
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Inspect tests relevant to endpoint/execution lifecycle.

Determine exact current locations for:

```text
qualification mode activation
qualification start
artifact write completion
listener construction
listener Start()/bind success
request receive
route validation
token validation
RunId validation
response serialization
retrieval completion signal
listener stop
timeout stop
exception exit
Worker process exit
```

## 8. Instrumentation design candidates

Evaluate exactly these options.

### O1 — One-path endpoint instrumentation only

Instrument only:

```text
PersistentSqliteQualificationEvidenceEndpoint.cs
```

Choose only if this single path can prove:

```text
listener start/bind
request arrival
handler invocation
retrieval outcome
listener shutdown
```

and if artifact creation/process-entry evidence already exists elsewhere in safe deterministic output.

### O2 — One-path execution/orchestration instrumentation only

Instrument only:

```text
PersistentSqliteQualificationExecution.cs
```

Choose only if this path owns enough lifecycle to prove artifact creation, listener startup result, retrieval outcome, and shutdown reason without editing endpoint implementation.

### O3 — Two-path execution + endpoint instrumentation

Instrument exactly:

```text
PersistentSqliteQualificationExecution.cs
PersistentSqliteQualificationEvidenceEndpoint.cs
```

Choose if this is the minimum required to prove both pre-listener lifecycle and request/handler lifecycle.

### O4 — Program + execution + endpoint instrumentation

Choose only if `Program.cs` is required to prove process-entry or process-exit boundaries that cannot be observed safely from the other two paths.

### O5 — Container entrypoint + Worker instrumentation

Choose only if container-level exit/start evidence is impossible to distinguish from Worker-level lifecycle without modifying `container/entrypoint.sh`.

### O6 — Azure logging enablement instead of source instrumentation

Reject unless repository instrumentation cannot provide the required boundary evidence and an Azure logging mutation is demonstrably narrower and fully $0-compatible.

Given the preceding read-only outcome, source instrumentation is preferred over enabling broad Azure logging unless code inspection proves otherwise.

## 9. Required decision

Select exactly one:

```text
D1 — O1_ONE_PATH_ENDPOINT_INSTRUMENTATION
D2 — O2_ONE_PATH_EXECUTION_INSTRUMENTATION
D3 — O3_TWO_PATH_EXECUTION_ENDPOINT_INSTRUMENTATION
D4 — O4_PROGRAM_EXECUTION_ENDPOINT_INSTRUMENTATION
D5 — O5_CONTAINER_AND_WORKER_INSTRUMENTATION
D6 — O6_AZURE_LOGGING_ENABLEMENT
```

Prefer the smallest path count that produces all necessary boundary evidence.

Do not choose a broader option for convenience.

## 10. Diagnostic event contract

If source instrumentation is selected, define an exact finite event vocabulary.

Prefer fixed markers with machine-readable key/value fields, for example:

```text
WP04_DIAG_EVENT=<FIXED_EVENT_NAME>
WP04_DIAG_RUN_ID=<RUN_ID>
WP04_DIAG_PHASE=<PHASE>
WP04_DIAG_ELAPSED_MS=<INTEGER>
WP04_DIAG_OUTCOME=<SAFE_CLASS>
```

Luna must define the exact required event names.

The event vocabulary should distinguish, at minimum where supported:

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

If a specific event cannot be emitted from the selected minimum path set, say so explicitly and explain which equivalent boundary evidence replaces it.

## 11. Exception/failure classification contract

If instrumentation captures failures, use sanitized fixed classes only.

Examples:

```text
ArtifactWriteFailure
ListenerBindFailure
ListenerStartFailure
RequestReceiveFailure
UnauthorizedRequest
RunIdMismatch
MalformedRequest
RetrievalTimeout
ListenerShutdownFailure
UnhandledQualificationFailure
```

Do not print raw exception messages.

Do not alter current helper-side classifications.

## 12. Test requirements to define

Luna must specify exact tests required for the later Terra implementation.

At minimum require:

```text
event sequence on successful local qualification
event sequence on missing token
event sequence on wrong RunId
event sequence on retrieval timeout
event sequence on listener bind/start failure where locally injectable
no token/header/query leakage
no raw exception-message leakage
existing endpoint behavior unchanged
existing qualification evidence JSON unchanged
existing exit codes unchanged
existing 180-second listener lifetime unchanged
```

If path scope affects test project files, Luna must explicitly authorize them.

A path allowlist must include test modifications if new assertions cannot be added without them.

Do not silently omit test-path accounting.

## 13. Runtime image implication

For each decision, state whether the change affects image contents.

Rules:

```text
src/ change -> new source commit required = YES
src/ change -> new runtime image required = YES
container/ change -> new runtime image required = YES
eng/ only -> new runtime image required = NO
Azure setting only -> new source commit required = NO unless policy artifact is also changed
```

If source instrumentation is selected, current image finality becomes:

```text
SUPERSEDED_AFTER_INSTRUMENTATION_PUBLICATION
```

Do not rebuild under this Luna authority.

## 14. README preservation

No README mutation is authorized or expected.

If code inspection suggests README changes, defer them. The active front-door preservation policy remains binding:

```text
EXISTING README INFORMATION → PRESERVE
PRESERVE INFORMATION; UPDATE STATUS
SUMMARY ≠ AUTHORITY TO DELETE DETAIL
README PRESERVATION — ZERO UNAUTHORIZED INFORMATION LOSS: PASS
```

## 15. Retry/timing policy preservation

Unless Luna produces direct evidence requiring later separate re-governance, preserve:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
helper outer evidence-poll budget <= 180 seconds
listener bounded lifetime = 180 seconds
```

Diagnostic instrumentation must not change these semantics.

## 16. Azure state preservation

This authority is read-only.

Preserve:

```text
Running/Normal
exact current deployed digest
SCM basic auth = false
FTP basic auth = false
registry credentials absent
temporary qualification settings absent
logging configuration unchanged
health check unchanged
Always On unchanged
```

## 17. Mutation prohibition

Required zero:

```text
repository mutations = 0
Git mutations = 0
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
provider mutations = 0
PR mutations = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
```

## 18. Required output

Return:

- exact current lifecycle ownership by file;
- whether safe pre-listener events already exist;
- whether safe listener/request events already exist;
- O1–O6 comparison;
- exact D1–D6 decision;
- exact source/test/container path allowlist;
- exact path count and operation count;
- exact diagnostic event vocabulary;
- exact sanitized failure classes;
- exact local test matrix;
- secret-hygiene assertions;
- whether new source commit is required;
- whether new runtime image is required;
- current source finality after selected remediation;
- current image finality after selected remediation;
- retry/timing preservation statement;
- Azure preservation statement;
- failed RunId disposition;
- zero-mutation confirmation.

## 19. Terminal markers

Required:

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION DECISION: <D1|D2|D3|D4|D5|D6>`

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION PATH COUNT: <N>`

`RELEASE 1.12 WP04 — DIAGNOSTIC EVENT VOCABULARY: DEFINED`

`RELEASE 1.12 WP04 — DIAGNOSTIC SECRET-HYGIENE BOUNDARY: DEFINED`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_GOOD_STATE`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION RECONCILIATION MUTATION AUDIT: PASS`

If source instrumentation is selected:

`RELEASE 1.12 WP04 — TERRA DIAGNOSTIC INSTRUMENTATION IMPLEMENTATION AUTHORITY: READY`

If Azure logging is selected:

`RELEASE 1.12 WP04 — TERRA AZURE LOGGING DIAGNOSTIC AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA DIAGNOSTIC INSTRUMENTATION RECONCILIATION COMPLETE`

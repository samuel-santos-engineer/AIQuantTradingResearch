# GPT-5.6 Terra — Release 1.12 WP04 Diagnostic Instrumentation Implementation & Validation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the approved diagnostic instrumentation on the exact four-path allowlist.
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

Selected Luna decision:

```text
D3 — O3_TWO_PATH_EXECUTION_ENDPOINT_INSTRUMENTATION
```

Binding runtime compatibility:

```text
Windows PowerShell 5.1.26100.9444
```

Binding qualification policy:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
client evidence-poll budget <= 180 seconds
listener lifetime <= 180 seconds
```

## 2. Exact authorized mutation allowlist

Exactly four tracked paths may be modified:

```text
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/PersistentSqliteQualificationEvidenceEndpointTests.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
```

Required counts:

```text
instrumentation source paths = 2
test paths = 2
total tracked mutation paths = 4
operations = 4 MODIFY
```

No other tracked path is authorized.

Preserve all unrelated untracked files untouched.

## 3. Explicitly excluded paths

Do not modify:

```text
src/AIQuantTradingResearch.Worker/Program.cs
src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
container/entrypoint.sh
Dockerfile
eng/**
python/**
README*
docs/**
```

No project/package/schema change is authorized.

## 4. Instrumentation objective

Add sanitized, deterministic, application-owned lifecycle diagnostics sufficient to distinguish:

```text
A. qualification process entered
B. SQLite qualification started
C. qualification artifact write succeeded
D. HTTP listener startup attempted
E. HTTP listener successfully started/bound
F. HTTP request arrived at application process
G. qualification handler entered
H. evidence retrieval succeeded
I. listener shutdown began
J. listener shutdown completed
K. Worker qualification execution is exiting
```

Instrumentation must not alter:

```text
qualification semantics
persistence semantics
SQLite ownership
artifact schema
endpoint response schema
endpoint status-code behavior
routing
listener port
listener lifetime
retry behavior
timeout behavior
exit-code behavior
```

## 5. Exact diagnostic event vocabulary

Use exactly these fixed event names:

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

Do not add ad hoc event names without stopping for Luna re-governance.

## 6. Diagnostic field contract

Each emitted diagnostic record/event must use fixed `WP04_DIAG_*` fields only.

Allowed fields:

```text
WP04_DIAG_EVENT
WP04_DIAG_RUN_ID
WP04_DIAG_PHASE
WP04_DIAG_ELAPSED_MS
WP04_DIAG_OUTCOME
```

Where applicable, safe fixed listener metadata may also be emitted only if already covered by test assertions and never includes secrets:

```text
WP04_DIAG_LISTENER_HOST=0.0.0.0
WP04_DIAG_LISTENER_PORT=8501
WP04_DIAG_ROUTE=/internal/wp04/persistence-qualification
WP04_DIAG_HTTP_METHOD=GET
```

Do not emit query strings.

RunId is allowed only as the sanitized exact run identifier.

Phase is limited to governed qualification phases.

Elapsed time must be integer milliseconds from a monotonic source where practical.

Outcome must be a fixed safe class/string, never raw exception text.

## 7. Sanitized failure-class vocabulary

When a diagnostic event must describe failure, use only the approved fixed classes:

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

No raw exception message is authorized.

No stack trace is authorized.

## 8. Secret-hygiene boundary

The implementation and all tests must prove zero output of:

```text
evidence token
X-WP04-Evidence-Token header value
request headers
query string
full request URL with query
response body
raw qualification evidence JSON
SQLite contents
connection strings
registry credentials
Azure credentials
environment-variable dumps
raw exception messages
stack traces
```

Do not log request headers even in failure cases.

Do not serialize request objects.

Do not serialize exception objects.

## 9. Execution-path instrumentation requirements

### PersistentSqliteQualificationExecution.cs

This path must own, where architecturally accurate:

```text
QUALIFICATION_ENTERED
SQLITE_QUALIFICATION_STARTED
ARTIFACT_WRITE_SUCCEEDED
WORKER_EXITING
```

It may emit `LISTENER_STARTING` only if listener creation/start is orchestrated from this path and doing so avoids duplication.

Requirements:

```text
QUALIFICATION_ENTERED precedes SQLite work
SQLITE_QUALIFICATION_STARTED precedes governed persistence qualification call
ARTIFACT_WRITE_SUCCEEDED occurs only after atomic artifact publication succeeds
WORKER_EXITING occurs from a deterministic finalization path
```

On artifact failure:

```text
do not emit ARTIFACT_WRITE_SUCCEEDED
emit safe failure outcome/class only
preserve existing exit behavior
```

### PersistentSqliteQualificationEvidenceEndpoint.cs

This path must own, where architecturally accurate:

```text
LISTENER_STARTING
LISTENER_STARTED
REQUEST_ARRIVED
HANDLER_ENTERED
EVIDENCE_RETRIEVAL_SUCCEEDED
LISTENER_STOPPING
LISTENER_STOPPED
```

Requirements:

```text
LISTENER_STARTING before bind/start call
LISTENER_STARTED only after bind/start succeeds
REQUEST_ARRIVED as soon as an HTTP request is received safely
HANDLER_ENTERED only when request reaches qualification handler logic
EVIDENCE_RETRIEVAL_SUCCEEDED only after valid authenticated exact-RunId evidence retrieval succeeds
LISTENER_STOPPING when shutdown begins
LISTENER_STOPPED after successful shutdown completion
```

Unauthorized/mismatched/malformed requests must retain current HTTP behavior.

Instrumentation must not make those requests successful.

## 10. Sequence expectations

### Successful qualification/retrieval

Expected relative sequence:

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

If current architecture necessarily emits `HANDLER_ENTERED` before a specific auth/RunId branch, tests must document the exact semantics without widening exposure.

### Missing token

Must prove:

```text
REQUEST_ARRIVED present
HANDLER_ENTERED present if handler owns auth check
EVIDENCE_RETRIEVAL_SUCCEEDED absent
safe outcome = UnauthorizedRequest where emitted
status behavior unchanged
no token/header leakage
```

### Wrong RunId

Must prove:

```text
REQUEST_ARRIVED present
HANDLER_ENTERED present
EVIDENCE_RETRIEVAL_SUCCEEDED absent
safe outcome = RunIdMismatch where emitted
status behavior unchanged
```

### Retrieval timeout

Must prove:

```text
LISTENER_STARTED present
EVIDENCE_RETRIEVAL_SUCCEEDED absent
LISTENER_STOPPING present
LISTENER_STOPPED present on successful shutdown
safe timeout class = RetrievalTimeout where emitted
listener lifetime semantics unchanged
```

### Listener start/bind failure

Where locally injectable without architecture change, prove:

```text
LISTENER_STARTING present
LISTENER_STARTED absent
safe class = ListenerBindFailure or ListenerStartFailure
raw exception text absent
existing failure/exit behavior preserved
```

## 11. Evidence JSON preservation

The canonical qualification JSON record must remain unchanged.

It must still contain exactly the governed evidence fields:

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

Instrumentation must not add diagnostic fields to that record.

## 12. Endpoint behavior preservation

Preserve all existing endpoint semantics, including:

```text
required route
required HTTP method
required token header
required exact RunId
status-code behavior
one successful governed retrieval
bounded listener lifetime
response JSON shape
```

Tests must prove existing status outcomes remain unchanged.

## 13. Test implementation requirements

Modify only the two authorized test files.

Required assertions include:

```text
successful event sequence
missing-token event behavior
wrong-RunId event behavior
retrieval-timeout event behavior
injectable listener start/bind failure where possible
token/header/query leakage = 0
raw exception leakage = 0
response body leakage = 0
evidence JSON schema unchanged
endpoint status behavior unchanged
exit behavior unchanged
listener 180-second contract unchanged
```

Use deterministic capture of stdout/stderr or the existing testable diagnostic sink pattern if one already exists.

Do not add packages.

Do not modify project files.

If the required tests cannot be implemented without another path/package/project change, STOP.

## 14. Required local validation

Run the narrow relevant tests first, then the governing regression gates.

At minimum:

```text
dotnet test tests/AIQuantTradingResearch.Infrastructure.Tests/... --filter relevant WP04 tests
```

Then run the repository’s existing required build/test gates appropriate to WP04.

Binding signing contract:

```text
Release build required
Release build warnings = 0
Release build errors = 0
Release Authenticode signature = NOT required
Debug local signing contract = required
expected signer = CN=AIQuantTradingDev
existing Debug-only AutoSignTestBinaries mechanism preserved
```

Do not alter signing configuration.

If Debug signing cannot run because the local dev certificate is absent, repair only the already-established local developer signing prerequisite without tracked policy changes; do not bypass signing.

## 15. Static/security validation

Require:

```text
diagnostic event names = exact approved vocabulary
WP04_DIAG fields = only approved safe fields
token literal/value leakage = 0
header dump logic = 0
query-string logging = 0
raw URL logging = 0
response-body logging = 0
raw evidence JSON logging = 0
SQLite content logging = 0
environment dump logic = 0
raw exception-message logging = 0
stack-trace logging = 0
direct SQLite evidence bypass = 0
Kudu VFS evidence path = 0
retry policy change = 0
timeout policy change = 0
route change = 0
port change = 0
schema change = 0
```

Run:

```text
Gitleaks
git diff --check
```

Both must pass.

## 16. Git/worktree gate

Before completion require:

```text
tracked changed paths = exactly 4
tracked changed paths = exact allowlist
staged paths = 0
unauthorized tracked paths = 0
```

Preserve unrelated untracked files.

Do not stage anything.

## 17. Runtime image implication

Because authorized source paths under `src/` are modified:

```text
new source commit required later = YES
new runtime image required later = YES
```

Current deployed image remains unchanged during this authority:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

Current image finality after successful implementation:

```text
SUPERSEDED_AFTER_INSTRUMENTED_IMAGE_PUBLICATION
```

No Docker build is authorized here.

No GHCR publication is authorized here.

## 18. Azure preservation

No Azure action is authorized.

Preserve:

```text
App Service = Running/Normal
current image digest unchanged
SCM basic auth = false
FTP basic auth = false
registry credentials absent
temporary qualification settings absent
logging settings unchanged
health check unchanged
Always On unchanged
```

Do not rerun initialize.

## 19. Failed RunId disposition

The following remain forbidden for reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
```

Additionally, the malformed historical identifier previously listed by Luna must not be reused if encountered:

```text
initialize-50a6837f6cb849...
```

Do not normalize or recycle failed identities.

## 20. Explicitly forbidden mutations

This authority does not authorize:

```text
git add
git commit
git push
Docker build
Docker image publication
GHCR mutation
Azure mutation
App Service restart
App Service redeploy
logging changes
PR creation
PR merge
issue mutation
Project #2 mutation
milestone mutation
tag/release mutation
WP05 start
README mutation
```

## 21. Stop conditions

STOP if:

- more than four tracked paths are required;
- any project/package file must change;
- Program.cs becomes necessary;
- container/entrypoint.sh becomes necessary;
- Dockerfile becomes necessary;
- a new diagnostic route/port is required;
- endpoint semantics must change;
- evidence JSON must change;
- retry policy must change;
- timeout policy must change;
- raw exception text appears necessary;
- secret-bearing data would be logged;
- source instrumentation cannot remain deterministic;
- local tests cannot prove event ordering;
- Azure mutation appears necessary;
- Docker build appears necessary before local source/test validation completes.

Do not widen scope.

## 22. Expected mutation accounting

Authorized local repository mutation:

```text
4 tracked paths modified
```

Required zero:

```text
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
PR mutations = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
```

Count actual operations precisely.

## 23. Required return evidence

Return:

- exact four modified paths;
- implementation summary by path;
- exact emitted event vocabulary;
- exact safe diagnostic fields;
- successful sequence result;
- missing-token result;
- wrong-RunId result;
- retrieval-timeout result;
- listener failure injection result if supported;
- secret-hygiene scan results;
- evidence JSON preservation proof;
- endpoint status preservation proof;
- exit-code preservation proof;
- listener-lifetime preservation proof;
- Release build result;
- Debug signing/test result;
- relevant tests result;
- full regression result required by repository gate;
- Gitleaks result;
- `git diff --check`;
- tracked/staged path counts;
- Azure preservation confirmation;
- exact mutation audit.

## 24. Terminal markers

Full success requires:

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION IMPLEMENTATION: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION PATH GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTIC EVENT VOCABULARY IMPLEMENTATION: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTIC EVENT SEQUENCE VALIDATION: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTIC SECRET-HYGIENE IMPLEMENTATION: PASS`

`RELEASE 1.12 WP04 — QUALIFICATION EVIDENCE JSON PRESERVATION: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE ENDPOINT BEHAVIOR PRESERVATION: PASS`

`RELEASE 1.12 WP04 — LISTENER LIFETIME PRESERVATION: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: YES`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: YES`

`RELEASE 1.12 WP04 — AZURE STATE: PRESERVE_CURRENT_GOOD_STATE`

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA DIAGNOSTIC INSTRUMENTATION IMPLEMENTATION COMPLETE`

`RELEASE 1.12 WP04 — TERRA DIAGNOSTIC INSTRUMENTATION PUBLICATION AUTHORITY: READY`

Blocked:

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION IMPLEMENTATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

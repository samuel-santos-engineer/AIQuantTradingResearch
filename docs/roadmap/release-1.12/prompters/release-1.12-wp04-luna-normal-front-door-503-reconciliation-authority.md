# GPT-5.6 Luna — Release 1.12 WP04 Normal Front-Door 503 Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the current normal-runtime HTTP `503` and determine the narrowest next governed action before any new WP04 qualification attempt.
- **GPT-5.6 Terra** — execute only the later explicitly authorized read-only investigation, source remediation, bounded Azure diagnostic action, recovery action, or qualification retry.
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

Parent:

```text
4822f9847a90a7d86c6bf771603defe9d7abf258
```

Current published wrapper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Current provenance contract:

```text
pattern = A
baseline = 4822f9847a90a7d86c6bf771603defe9d7abf258
HEAD must descend from baseline
wrapper must be tracked at HEAD
worktree wrapper must equal committed wrapper at HEAD
```

Current expected deployed instrumented image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Most recent read-only Azure observation:

```text
linuxFxVersion = exact governed digest
App Service = Running/Normal
SKU = F1/Free
region = West Central US
HTTPS-only = enabled
DNS = resolves
TLS 1.3 = succeeds
normal root request = 503
diagnostic settings = empty
application/HTTP/filesystem/detailed-error/failed-request logging = disabled
```

Current qualification state:

```text
INITIALIZE D3 = NOT_PROVEN
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
TIMEOUT CAUSE = NOT_PROVEN
```

## 2. Reconciliation objective

Determine the cause class and narrowest governed next step for the current normal-runtime HTTP `503`.

The `503` must be treated as a normal-runtime availability blocker independent of the earlier qualification timeout.

This Luna authority must answer:

1. whether the `503` is attributable to current app/container startup, routing, runtime crash, port binding, or another platform-visible condition;
2. whether useful zero-mutation evidence remains available;
3. whether bounded temporary diagnostic logging is required;
4. whether a source/runtime defect is already proven;
5. whether a recovery mutation is justified;
6. whether a future qualification retry can remain blocked until normal front-door health is restored.

No mutation is authorized by this Luna reconciliation.

## 3. Mandatory source/runtime contract review

Read-only inspect the exact source at:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Inspect at minimum:

```text
Dockerfile
container/entrypoint.sh
src/AIQuantTradingResearch.Worker/Program.cs
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
```

Inspect Streamlit startup/composition files directly referenced by the entrypoint.

Determine the normal-mode contract:

```text
qualification mode disabled
Streamlit starts
Streamlit owns 0.0.0.0:8501
Worker/runtime composition remains as designed
container lifetime remains valid
```

Classify:

```text
NORMAL_RUNTIME_SOURCE_PATH = PROVEN_CORRECT | DEFECT_PROVEN | NOT_PROVEN
```

Do not infer Azure runtime success from source correctness.

## 4. Current image/source relationship

Reconcile the source commit with the deployed instrumented image.

Known:

```text
deployed image digest =
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Determine:

- which source commit produced this image;
- whether commits after that image are `eng/**`-only and therefore do not alter runtime content;
- whether runtime source corresponding to the deployed image is still the intended current runtime source.

Expected known image source candidate:

```text
d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Classify:

```text
DEPLOYED_IMAGE_RUNTIME_SOURCE = CONSISTENT | MISMATCH | NOT_PROVEN
```

Do not rebuild or redeploy.

## 5. Normal front-door 503 semantics

Treat the observed root `503` as:

```text
front door reachable
TLS reachable
application availability failed
```

Do not treat `503` as proof of any one internal cause.

Distinguish:

```text
Azure front-end generated 503
container/app generated 503
routing target unavailable
listener unavailable
container starting/recycling
container crashed/exited
platform health failure
other
```

Classify each only as:

```text
PROVEN
PLAUSIBLE
NOT_SUPPORTED
```

## 6. Existing read-only Azure evidence review

Without mutation, determine what current surfaces can still provide useful evidence.

Review, as available:

```text
az webapp show
az webapp config show
az webapp config container show or equivalent current CLI shape
Azure Activity Log
resource health/state
deployment metadata
container/recycle metadata exposed read-only
site properties/timestamps
hostname response headers
SCM metadata endpoints that do not require enabling basic auth
existing diagnostics metadata
```

Do not use Kudu to retrieve custom-container `/home`.

Do not enable logging.

Do not restart.

Do not change settings.

## 7. Correct prior settings-query defect

The prior read-only settings query used:

```text
contains(value,'')
```

which is not a valid presence test for strings.

This reconciliation must explicitly recognize:

```text
WEBSITES_PORT current value = NOT_PROVEN from prior output
registry credential state = NOT_PROVEN from prior output
```

If source review or a future read-only Terra investigation is recommended, require correct exact queries that:

- distinguish missing/null/blank from actual values;
- inspect command exit codes;
- do not rely on `contains(value,'')`.

No mutation is authorized here.

## 8. Port/routing reconciliation

Determine from source plus any reliable current evidence whether the intended normal runtime requires:

```text
WEBSITES_PORT = 8501
Streamlit bind = 0.0.0.0:8501
Docker EXPOSE = 8501
```

Classify:

```text
INTENDED_PORT_CONTRACT = PROVEN | NOT_PROVEN
CURRENT_AZURE_PORT_CONFIG = PROVEN_MATCH | PROVEN_MISMATCH | NOT_PROVEN
```

If current Azure port config remains unproven, do not claim routing correctness.

## 9. Container lifecycle reconciliation

Determine whether current source proves normal mode should keep the container alive.

Review:

```text
entrypoint foreground/background process ownership
Worker startup behavior
Streamlit startup behavior
shell wait/exec semantics
error handling
non-root transition
persistent parent preparation
```

Classify:

```text
NORMAL_CONTAINER_LIFETIME_SOURCE = PROVEN_CORRECT | DEFECT_PROVEN | NOT_PROVEN
```

## 10. Diagnostic gap assessment

Current logging state is historically:

```text
application logs = disabled
HTTP logs = disabled
filesystem logs = disabled
detailed errors = disabled
failed-request tracing = disabled
diagnostic settings = empty
```

Determine whether current zero-mutation surfaces can distinguish:

```text
container never started
container started then exited
Streamlit failed to bind
Worker failed
entrypoint failed
Azure failed to route
```

Classify:

```text
READ_ONLY_EVIDENCE_SUFFICIENT = YES | NO
```

If `NO`, determine whether the narrowest next step is bounded temporary diagnostic capture.

## 11. Candidate failure classes

Evaluate each:

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

No cause may be `PROVEN` without direct evidence.

## 12. Decision options

Select exactly one.

### D1 — R1_READ_ONLY_NORMAL_RUNTIME_INVESTIGATION

Use when useful zero-mutation evidence remains and can narrow the `503`.

Expected next authority:

```text
GPT-5.6 Terra — read-only normal-runtime 503 investigation
```

No restart, settings, logging, or qualification attempt.

### D2 — R2_BOUNDED_DIAGNOSTIC_CAPTURE_REGOVERNANCE

Use when existing read-only evidence is insufficient and temporary logging is the narrowest path to identify container/startup failure.

Expected next authority:

```text
GPT-5.6 Luna — bounded diagnostic capture governance
```

It must later define exact settings, sink, duration, retrieval, redaction, and restoration.

### D3 — R3_SOURCE_RUNTIME_REMEDIATION

Use only if a concrete source/runtime defect is proven.

Expected next authority:

```text
GPT-5.6 Terra — exact-path runtime remediation
```

Must state whether new image/GHCR publication will be required.

### D4 — R4_BOUNDED_AZURE_RECOVERY

Use only if a specific current Azure configuration/runtime condition is proven and a minimal reversible recovery action is justified.

Expected next authority:

```text
GPT-5.6 Terra — bounded Azure normal-runtime recovery
```

Do not authorize generic restart/redeploy merely because root returns `503`.

### D5 — R5_NORMAL_RUNTIME_HEALTH_RECHECK_ONLY

Use only if evidence shows the `503` was transient and current normal front-door health has already recovered without mutation.

Expected next step:

```text
read-only health confirmation, then separate Luna qualification-governance decision
```

## 13. Decision preference

Prefer evidence over mutation:

```text
D1 > D2 > D3 > D4 > D5
```

except where a concrete source/configuration defect is already proven.

Do not use a blind restart as a diagnostic technique.

## 14. Qualification retry gate

A future initialize qualification remains blocked until:

```text
normal front-door health = PASS
wrapper provenance gate = PASS
exact image digest = MATCH
current routing configuration = sufficiently proven
no unresolved normal-runtime critical defect remains
```

Required current marker:

```text
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

## 15. Diagnostic capture governance requirements if D2 selected

The next Luna authority must define exactly:

```text
which Azure logging switch(es)
which log sink(s)
maximum active duration
retention
retrieval method
safe marker vocabulary
secret redaction rules
restoration commands
restoration proof
mutation count
```

It must preserve:

```text
SCM basic auth = false
FTP basic auth = false
registry credential policy
strict $0 constraint
```

No broad “enable all diagnostics.”

## 16. Azure recovery constraints if D4 selected

Any later recovery authority must be exact and bounded.

Examples only if proven necessary:

```text
correct one app setting
one restart
one image re-assertion
one startup configuration correction
```

It must not mix:

```text
diagnostic discovery
source remediation
qualification
lifecycle completion
```

unless explicitly governed.

## 17. Current zero-mutation boundary

Under this Luna authority:

```text
repository edits = 0
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
Azure settings changes = 0
Azure restart = 0
Azure redeploy = 0
logging changes = 0
qualification attempts = 0
PR/lifecycle mutations = 0
```

## 18. Failed RunId preservation

Forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

No fresh RunId is authorized.

## 19. Required output

Return:

- normal runtime source-path classification;
- deployed-image/runtime-source reconciliation;
- intended port contract;
- current Azure port-config proof state;
- container lifetime classification;
- corrected assessment of prior settings-query evidence;
- current read-only evidence availability;
- N1–N11 classifications;
- whether current `503` origin is proven or not;
- whether read-only evidence is sufficient;
- D1–D5 comparison;
- exact selected decision;
- exact next authority type;
- whether next step mutates Azure;
- whether new source commit is required;
- whether new image is required;
- qualification retry remains blocked;
- zero-mutation audit.

## 20. Terminal markers

Required:

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR 503 RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 2532f6abd4677edfb205c26c083a534783038979`

`RELEASE 1.12 WP04 — CURRENT DEPLOYED IMAGE: sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR HTTP: 503`

`RELEASE 1.12 WP04 — NORMAL RUNTIME SOURCE PATH: <PROVEN_CORRECT|DEFECT_PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — DEPLOYED IMAGE RUNTIME SOURCE: <CONSISTENT|MISMATCH|NOT_PROVEN>`

`RELEASE 1.12 WP04 — INTENDED PORT CONTRACT: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — CURRENT AZURE PORT CONFIG: <PROVEN_MATCH|PROVEN_MISMATCH|NOT_PROVEN>`

`RELEASE 1.12 WP04 — NORMAL CONTAINER LIFETIME SOURCE: <PROVEN_CORRECT|DEFECT_PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — 503 ORIGIN: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — READ-ONLY EVIDENCE SUFFICIENT: <YES|NO>`

`RELEASE 1.12 WP04 — EXISTING DIAGNOSTIC LOGGING: DISABLED`

`RELEASE 1.12 WP04 — TIMEOUT CAUSE: NOT_PROVEN`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR 503 RECONCILIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR 503 DECISION: <D1|D2|D3|D4|D5>`

Then exactly one matching next-authority marker:

`RELEASE 1.12 WP04 — TERRA READ-ONLY NORMAL-RUNTIME 503 INVESTIGATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA BOUNDED DIAGNOSTIC CAPTURE REGOVERNANCE AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA SOURCE RUNTIME REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA BOUNDED AZURE NORMAL-RUNTIME RECOVERY AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — NORMAL-RUNTIME HEALTH RECHECK AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA NORMAL FRONT-DOOR 503 RECONCILIATION COMPLETE`

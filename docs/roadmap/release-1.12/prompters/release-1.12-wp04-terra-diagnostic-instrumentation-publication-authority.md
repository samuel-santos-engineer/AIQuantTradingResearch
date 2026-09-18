# GPT-5.6 Terra — Release 1.12 WP04 Diagnostic Instrumentation Source & Runtime Publication Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: publish the already-validated four-path diagnostic instrumentation as one source commit and one new GHCR runtime image.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current governed parent commit:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Current branch:

```text
release/1.12-wp04-persistent-sqlite
```

Current deployed runtime image:

```text
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03
```

That deployed image remains active until a later explicitly authorized Azure deployment/qualification action.

## 2. Validated implementation state

Exactly four tracked instrumentation paths are modified and unstaged:

```text
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/PersistentSqliteQualificationEvidenceEndpointTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
```

Governance state:

```text
tracked modified paths = 4
unauthorized tracked paths = 0
staged paths = 0
```

Passed validation already reported:

```text
Release build = 0 warnings, 0 errors
Domain tests = 11 passed
Application tests = 136 passed
Architecture tests = 27 passed
Infrastructure tests = 201 passed
HTTP integration = PASS
git diff --check = PASS
Gitleaks = PASS
Debug signing gate = PASS
Debug signer subject = CN=AIQuantTradingDev
```

Local signing prerequisite:

```text
certificate subject = CN=AIQuantTradingDev
certificate thumbprint = F6ED66CA6599BE403DBFF3CEB644DC7F625A08F2
store = Cert:\CurrentUser\My
private key = present
Directory.Build.local.props = ignored/untracked local-only override
```

The local certificate and local override are not publication payload.

## 3. Publication objective

Publish the validated instrumentation as:

1. exactly one Git commit;
2. exactly one non-force push to the governed branch;
3. exactly one new runtime image built from that source commit;
4. exactly one immutable GHCR candidate tag for that commit;
5. capture the published image digest;
6. verify anonymous/public manifest access if repository policy requires it.

Do not deploy the new image to Azure under this authority.

## 4. Exact source allowlist

Stage exactly these four tracked paths:

```text
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/PersistentSqliteQualificationEvidenceEndpointTests.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
```

Required staged path count:

```text
4
```

Required unauthorized staged path count:

```text
0
```

Do not stage:

```text
Directory.Build.local.props
certificate material
unrelated untracked files
README*
docs/**
eng/**
container/**
Dockerfile
any other path
```

## 5. Pre-publication validation

Before staging, re-run the minimum required proof that publication payload still matches validated state.

Required:

```text
git status --short
git diff --check
Gitleaks on changed paths
Release build: 0 warnings, 0 errors
Debug signing gate: PASS with CN=AIQuantTradingDev
targeted WP04 instrumentation/integration tests: PASS
```

Full test rerun is optional only if repository policy does not require it at publication time; otherwise run the full required repository gate.

Stop if any validation regresses.

## 6. Commit contract

Create exactly one commit.

Parent must be exactly:

```text
579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Commit payload must contain exactly the four authorized paths.

Use a concise repository-consistent message describing WP04 diagnostic instrumentation.

After commit capture:

```text
SOURCE_COMMIT=<new exact SHA>
SOURCE_PARENT=579bbbe3f24de13f87c9e94c9b480e029e70e709
```

Do not amend historical commits.

## 7. Push contract

Push exactly once, non-force, to:

```text
origin/release/1.12-wp04-persistent-sqlite
```

After push require:

```text
origin/release/1.12-wp04-persistent-sqlite = SOURCE_COMMIT
local HEAD = SOURCE_COMMIT
```

No force push.

No second source commit.

## 8. Runtime image build

Because `src/` changed, a new runtime image is required.

Build from the exact published source commit:

```text
SOURCE_COMMIT
```

Do not build from a dirty worktree.

The image must preserve all existing Release 1.12 runtime architecture:

```text
Linux custom container
Worker + Streamlit normal composition
qualification-mode single-listener substitution
port 8501
schema v4
SQLite DELETE
/home/data/aiquant.db
application-owned evidence
no direct SQLite bypass
no new packages beyond already-governed repository state
```

The ignored local signing override and local certificate must not enter the image.

## 9. Candidate image tag

Publish exactly one immutable candidate tag of the form:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp04-<SOURCE_COMMIT>
```

where `<SOURCE_COMMIT>` is the full exact commit SHA unless existing repository publication tooling canonically uses a shorter immutable form.

Prefer full SHA if supported.

Do not mutate `latest`.

Do not create a release tag.

## 10. GHCR publication validation

After publication capture:

```text
IMAGE_REFERENCE=<exact candidate reference>
IMAGE_DIGEST=sha256:<exact digest>
```

Verify:

- candidate tag resolves to that digest;
- image manifest is retrievable;
- public/anonymous manifest read succeeds if this repository remains public/free GHCR;
- architecture/platform matches intended Linux runtime;
- no registry credentials are introduced into Azure.

Do not deploy it yet.

## 11. Diagnostic instrumentation preservation

Before publication completion, verify the image source contains the approved diagnostic vocabulary:

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

And preserves the safe field contract:

```text
WP04_DIAG_EVENT
WP04_DIAG_RUN_ID
WP04_DIAG_PHASE
WP04_DIAG_ELAPSED_MS
WP04_DIAG_OUTCOME
```

Allowed safe listener metadata remains:

```text
WP04_DIAG_LISTENER_HOST
WP04_DIAG_LISTENER_PORT
WP04_DIAG_ROUTE
WP04_DIAG_HTTP_METHOD
```

## 12. Secret-hygiene publication gate

Require zero inclusion/output of:

```text
evidence token
X-WP04-Evidence-Token value
query string
raw request headers
raw response body
raw qualification evidence JSON
SQLite contents
connection strings
registry credentials
Azure credentials
environment dumps
raw exception messages
stack traces
local signing certificate/private key
Directory.Build.local.props
```

## 13. Qualification policy preservation

The new image must preserve:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
client evidence-poll wall-clock budget <= 180 seconds
listener lifetime <= 180 seconds
```

No helper policy change is authorized.

## 14. Azure preservation

No Azure mutation is authorized.

Preserve current Azure state:

```text
App Service = Running/Normal
currently deployed digest =
sha256:17da5a99fffa26bf0bc6dd9417d66400a7e0ee64492d24fa745728b4714c1a03

SCM basic auth = false
FTP basic auth = false
registry credentials = absent
temporary qualification settings = absent
logging configuration unchanged
health check unchanged
Always On unchanged
```

The newly published instrumented image must not be deployed here.

## 15. GitHub/lifecycle prohibition

Required zero:

```text
PR creation = 0
PR merge = 0
issue mutation = 0
Project #2 mutation = 0
milestone mutation = 0
tag/release mutation = 0
WP05 start = 0
```

## 16. Failed RunId preservation

Do not reuse any failed RunId, including:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
```

The malformed historical identifier previously listed by Luna also remains prohibited if encountered:

```text
initialize-50a6837f6cb849...
```

## 17. Post-publication worktree gate

After commit/push/image publication require:

```text
tracked unstaged paths = 0
staged paths = 0
local HEAD = SOURCE_COMMIT
remote branch tip = SOURCE_COMMIT
unrelated untracked files preserved
ignored Directory.Build.local.props preserved as local-only if still needed
```

No cleanup of unrelated untracked files is authorized.

## 18. Mutation accounting

Authorized maximum:

```text
tracked paths staged = 4
Git commits = 1
non-force pushes = 1
Docker image builds = 1
GHCR candidate publications = 1
```

Required zero:

```text
Azure mutations = 0
Azure restarts = 0
Azure redeploys = 0
SCM policy mutations = 0
FTP policy mutations = 0
registry credential mutations = 0
PR mutations = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
```

Count actual operations precisely.

## 19. Stop conditions

STOP if:

- parent commit differs;
- more than four tracked paths enter the commit;
- local signing override is staged;
- local certificate material is included anywhere;
- validation regresses;
- image cannot be built from exact published commit;
- image tag is mutable/non-unique;
- GHCR publication requires changing Azure registry credentials;
- secret hygiene fails;
- any Azure mutation would be required;
- any second commit/push appears necessary.

Do not widen scope.

## 20. Required return evidence

Return:

- pre-publication validation summary;
- exact commit SHA;
- exact parent SHA;
- exact commit path list;
- push result;
- remote branch tip;
- exact image reference;
- exact image digest;
- manifest/public-read result;
- diagnostic vocabulary preservation result;
- secret-hygiene result;
- qualification-policy preservation result;
- final worktree/staging state;
- Azure preservation confirmation;
- exact mutation audit.

## 21. Terminal markers

On success:

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION PUBLICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION SOURCE COMMIT: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION SOURCE PUSH: PASS`

`RELEASE 1.12 WP04 — INSTRUMENTED RUNTIME IMAGE BUILD: PASS`

`RELEASE 1.12 WP04 — INSTRUMENTED GHCR PUBLICATION: PASS`

`RELEASE 1.12 WP04 — INSTRUMENTED IMAGE SECRET-HYGIENE: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — CURRENT AZURE RUNTIME IMAGE: PRESERVED`

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA INSTRUMENTED IMAGE AZURE QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA DIAGNOSTIC INSTRUMENTATION PUBLICATION COMPLETE`

Blocked:

`RELEASE 1.12 WP04 — DIAGNOSTIC INSTRUMENTATION PUBLICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

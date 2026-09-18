# GPT-5.6 Terra — Release 1.12 WP04 Helper Terminal Telemetry Source Publication Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: publish the already-validated one-path helper terminal-telemetry remediation as one source commit and one non-force push.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current parent commit:

```text
d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Current branch:

```text
release/1.12-wp04-persistent-sqlite
```

Current deployed Azure image remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current validated tracked mutation:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Current governance state:

```text
tracked changed paths = 1
staged paths = 0
unauthorized tracked paths = 0
```

## 2. Validated remediation state

The one-path remediation has already passed local validation.

Reported passing evidence:

```text
Windows PowerShell 5.1 AST parse = 0 errors
built-in fixture matrix = 18 cases
successful terminal path = exactly one terminal block
success result = SUCCESS
success class = Success
success exit = 0
omitted RunId = exactly one sanitized failure block
invalid Phase = exactly one sanitized failure block
Azure calls for invalid local inputs = 0
Gitleaks = PASS
secret-output scan = 0 matches
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
outer evidence-poll budget = 180 seconds
git diff --check = PASS
tracked mutation paths = 1
staged paths = 0
```

Terminal field vocabulary:

```text
WP04_HELPER_TERMINAL_RESULT
WP04_HELPER_TERMINAL_CLASS
WP04_HELPER_TERMINAL_PHASE
WP04_HELPER_TERMINAL_RUN_ID
WP04_HELPER_TERMINAL_EXIT_CODE
WP04_HELPER_TERMINAL_SETTINGS_RESTORATION
WP04_HELPER_TERMINAL_ELAPSED_MS
```

Internal T5 coverage:

```text
uncovered internally controlled T5 paths = 0
```

Residual external boundary:

```text
T6 external process/session termination = preserved as external risk
```

## 3. Publication objective

Publish exactly the already-validated helper-only remediation as:

```text
1 source commit
1 non-force push
0 Docker builds
0 GHCR publications
0 Azure mutations
```

Because only `eng/**` changed and the established Docker context excludes `eng/**`, the runtime image is unchanged.

## 4. Exact source allowlist

Stage exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Required staged counts:

```text
staged paths = 1
unauthorized staged paths = 0
```

Do not stage any other tracked or untracked path.

Preserve unrelated untracked files.

## 5. Pre-publication validation

Before staging, re-run the minimum publication proof:

```text
git status --short
git diff --check
Windows PowerShell 5.1 AST parse
built-in helper fixture matrix
Gitleaks on changed path
secret-output scan
```

Require:

```text
AST parse errors = 0
fixture matrix = PASS
git diff --check = PASS
Gitleaks = PASS
secret-output scan matches = 0
retry policy unchanged
180-second bound unchanged
```

Do not invoke Azure.

## 6. Commit contract

Create exactly one commit.

Parent must be exactly:

```text
d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Commit payload must contain exactly one path:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Use a concise repository-consistent commit message describing helper terminal telemetry hardening.

Capture:

```text
SOURCE_COMMIT=<new exact SHA>
SOURCE_PARENT=d0f72660610b9b479f437fb69182cb1cc1a0a31f
```

Do not amend prior commits.

## 7. Push contract

Push exactly once, non-force, to:

```text
origin/release/1.12-wp04-persistent-sqlite
```

After push require:

```text
local HEAD = SOURCE_COMMIT
remote branch tip = SOURCE_COMMIT
```

No force push.

No second commit.

No second push.

## 8. Runtime image invariance proof

Because this remediation touches only:

```text
eng/**
```

prove the existing Docker contract still excludes it.

Read-only verify the repository's established Docker context rules and Dockerfile copy sources.

Required conclusion:

```text
helper-only source change affects Azure-side execution tooling only
runtime image payload unchanged
new Docker build required = NO
new GHCR publication required = NO
current instrumented Azure image remains valid
```

Current deployed image remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Do not build or republish Docker.

## 9. Helper contract preservation

Publication must preserve:

```text
terminal telemetry exactly once per internally completed invocation
internal T5 uncovered paths = 0
T6 external termination remains residual external risk
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
evidence-poll outer wall-clock budget <= 180 seconds
Windows PowerShell 5.1 compatibility
```

No semantic widening.

## 10. Secret-hygiene publication gate

Require zero committed output of:

```text
evidence token
X-WP04-Evidence-Token value
request headers
query string
full authenticated URL
response body
raw evidence JSON
SQLite contents
connection strings
registry credentials
Azure credentials
environment dumps
raw exception messages
stack traces
certificate/private-key material
```

## 11. Azure preservation

No Azure mutation is authorized.

Preserve:

```text
deployed digest =
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f

App Service = Running/Normal
temporary qualification settings = none
SCM basic auth = false
FTP basic auth = false
registry credentials = absent
logging configuration unchanged
```

No restart.

No redeploy.

No initialize attempt.

## 12. GitHub lifecycle prohibition

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

Issue `#263` remains open.

Milestone `#63` remains open.

## 13. Failed RunId preservation

All failed initialize RunIds remain forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
```

Do not generate or consume a fresh RunId under this publication authority.

## 14. Post-publication worktree gate

After commit/push require:

```text
tracked unstaged paths = 0
staged paths = 0
local HEAD = SOURCE_COMMIT
remote branch tip = SOURCE_COMMIT
unrelated untracked files preserved
```

No cleanup of unrelated untracked files is authorized.

## 15. Mutation accounting

Authorized maximum:

```text
tracked paths staged = 1
Git commits = 1
non-force pushes = 1
```

Required zero:

```text
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
Azure restarts = 0
Azure redeploys = 0
logging mutations = 0
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

## 16. Stop conditions

STOP if:

- parent commit differs;
- more than one tracked path would be committed;
- validation regresses;
- helper fixture matrix fails;
- PowerShell 5.1 compatibility fails;
- retry or timeout policy changes;
- Docker rebuild appears necessary;
- Azure mutation appears necessary;
- any second commit/push would be required.

Do not widen scope.

## 17. Required return evidence

Return:

- pre-publication validation summary;
- exact source commit SHA;
- exact parent SHA;
- exact commit path list;
- push result;
- remote branch tip;
- Docker-context exclusion proof;
- new image required = NO;
- current deployed image digest;
- terminal telemetry preservation result;
- retry/timing preservation result;
- secret-hygiene result;
- final tracked/staged state;
- Azure preservation confirmation;
- exact mutation audit.

## 18. Terminal markers

On success:

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY PUBLICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY SOURCE COMMIT: PASS`

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY SOURCE PUSH: PASS`

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY COVERAGE: COMPLETE_FOR_INTERNAL_PATHS`

`RELEASE 1.12 WP04 — HELPER INTERNAL T5 UNCOVERED PATH COUNT: 0`

`RELEASE 1.12 WP04 — HELPER EXTERNAL T6 BOUNDARY: PRESERVED_AS_EXTERNAL_RISK`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 TERMINAL TELEMETRY VALIDATION: PASS`

`RELEASE 1.12 WP04 — TERMINAL RESULT EXACTLY-ONCE VALIDATION: PASS`

`RELEASE 1.12 WP04 — HELPER TERMINAL SECRET-HYGIENE: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — CURRENT INSTRUMENTED AZURE IMAGE: PRESERVED`

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA FRESH INITIALIZE QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA HELPER TERMINAL TELEMETRY SOURCE PUBLICATION COMPLETE`

On block:

`RELEASE 1.12 WP04 — HELPER TERMINAL TELEMETRY SOURCE PUBLICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

# GPT-5.6 Terra — Release 1.12 WP04 Preflight Image-Proof Source Publication Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: publish the already-validated fail-closed preflight wrapper as one new tracked source file, one commit, and one non-force push.
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
047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

Current deployed instrumented Azure image expected by governance:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current authorized candidate path:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Current repository state:

```text
tracked modifications = 0
staged paths = 0
authorized untracked candidate count = 1
```

Repository operation classification:

```text
CREATE
```

## 2. Validated remediation state

The untracked wrapper candidate has already passed local validation.

Established behavior:

```text
image identity source = siteConfig.linuxFxVersion
accepted identity shape = exactly one DOCKER|…@sha256:<64-lowercase-hex>
digest comparison = ordinal exact equality
blank/null/whitespace = fail closed
tag-only = fail closed
malformed = fail closed
multiple values = fail closed
partial digest = fail closed
wrong digest = fail closed
Azure query failure = fail closed
failed preflight downstream helper/settings/restart calls = 0
```

Validated locally:

```text
V1–V10 = PASS
Windows PowerShell 5.1 AST errors = 0
Gitleaks = PASS
static secret-output matches = 0
trailing whitespace count = 0
tracked modifications = 0
staged paths = 0
authorized untracked candidate count = 1
```

## 3. Publication objective

Promote exactly the validated untracked wrapper candidate into the repository as one tracked file.

Authorized publication:

```text
1 CREATE path staged
1 source commit
1 non-force push
```

Required zero:

```text
other staged paths = 0
other commits = 0
Docker builds = 0
GHCR publications = 0
Azure mutations = 0
GitHub lifecycle mutations = 0
```

## 4. Exact publication allowlist

Stage exactly:

```text
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Before staging, prove:

```text
git ls-files --error-unmatch <path>
```

still does not resolve it as tracked.

If it is already tracked unexpectedly, STOP.

## 5. Pre-publication validation

Before staging, re-run the minimum local publication proof:

```text
Windows PowerShell 5.1 AST parse
local preflight fixture matrix
candidate whitespace/content validation
Gitleaks candidate scan
static secret-output scan
```

Require:

```text
AST errors = 0
fixture matrix = PASS
blank/malformed/mismatch failure paths = PASS
downstream mutation/helper calls for failed preflight = 0
Gitleaks = PASS
secret-output matches = 0
trailing whitespace = 0
```

Do not query or mutate Azure for this publication gate.

## 6. Fail-closed contract preservation

The committed file must preserve:

```text
image source = siteConfig.linuxFxVersion
non-empty image identity required
exactly one identity value required
DOCKER| prefix normalization only
immutable @sha256:<64-lowercase-hex> required
digest equality = ordinal exact equality
expected digest =
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
blank/malformed/mismatch/query-failure => local exit before Azure mutation
```

Required failure barrier:

```text
temporary settings application = 0
restart = 0
qualification helper invocation = 0
```

for every failed preflight.

## 7. PowerShell compatibility

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7-only constructs.

Require:

```text
AST parse errors = 0
```

## 8. Commit contract

Create exactly one commit.

Parent must be exactly:

```text
047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

Commit payload must contain exactly one path:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Use a concise repository-consistent commit message describing fail-closed Azure image preflight qualification.

Capture:

```text
SOURCE_COMMIT=<new exact SHA>
SOURCE_PARENT=047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

Do not amend prior commits.

## 9. Push contract

Push exactly once, non-force, to the governed WP04 branch.

After push require:

```text
local HEAD = SOURCE_COMMIT
remote branch tip = SOURCE_COMMIT
```

No force push.

No second commit.

No second push.

## 10. Runtime image invariance proof

The new tracked wrapper remains under:

```text
eng/**
```

and does not enter the established Docker runtime context.

Read-only prove:

```text
.dockerignore excludes eng/**
Dockerfile runtime COPY sources do not include eng/**
```

Required conclusion:

```text
new runtime image required = NO
new GHCR publication required = NO
current deployed instrumented image remains valid
```

Do not build Docker.

Do not publish GHCR.

## 11. Timeout/retry policy preservation

Do not modify:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Preserve:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
evidence-poll total wall-clock budget <= 180 seconds
listener lifetime <= 180 seconds
timeout cause = NOT_PROVEN
```

## 12. Secret-hygiene publication gate

The committed wrapper must not expose:

```text
evidence token
X-WP04-Evidence-Token value
headers
query strings
authenticated URLs
credentials
registry secrets
connection strings
environment dumps
raw exception messages
stack traces
raw evidence payloads
```

Safe preflight identity output is limited to the configured image identity/digest already intended for governance proof.

## 13. Azure preservation

No Azure mutation is authorized.

Required zero:

```text
settings changes = 0
restarts = 0
redeploys = 0
image configuration changes = 0
logging changes = 0
SCM policy changes = 0
FTP policy changes = 0
registry credential changes = 0
initialize attempts = 0
reopen attempts = 0
```

Preserve current normal runtime.

## 14. GitHub lifecycle preservation

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

## 15. Failed RunId preservation

All failed RunIds remain permanently forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

No fresh RunId is authorized under this publication authority.

## 16. Post-publication worktree gate

After commit/push require:

```text
tracked unstaged paths = 0
staged paths = 0
local HEAD = SOURCE_COMMIT
remote branch tip = SOURCE_COMMIT
unrelated untracked files preserved
```

Do not delete unrelated untracked files.

## 17. Mutation accounting

Authorized maximum:

```text
new tracked paths staged = 1
Git commits = 1
non-force pushes = 1
```

Required zero:

```text
other tracked path mutations = 0
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

## 18. Stop conditions

STOP if:

- the candidate path is unexpectedly already tracked;
- parent commit differs;
- more than one path would be staged;
- validation regresses;
- fail-closed barrier is not preserved;
- PowerShell 5.1 validation fails;
- secret-hygiene fails;
- Docker rebuild appears necessary;
- Azure mutation appears necessary;
- second commit/push appears necessary.

Do not widen scope.

## 19. Required return evidence

Return:

- tracking-state proof;
- pre-publication validation summary;
- exact source commit SHA;
- exact parent SHA;
- exact commit path list;
- push result;
- remote branch tip;
- Docker-context exclusion proof;
- new image required = NO;
- current deployed image digest;
- fail-closed contract preservation result;
- timeout/retry preservation result;
- secret-hygiene result;
- final tracked/staged/untracked state;
- Azure zero-mutation confirmation;
- exact mutation audit.

## 20. Terminal markers

On success:

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE-PROOF PUBLICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT WRAPPER REPOSITORY OPERATION: CREATE`

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE-PROOF SOURCE COMMIT: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE-PROOF SOURCE PUSH: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE IDENTITY SOURCE: siteConfig.linuxFxVersion`

`RELEASE 1.12 WP04 — PREFLIGHT FAIL-CLOSED ENFORCEMENT: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT FAILURE AZURE MUTATION COUNT: 0`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 PREFLIGHT VALIDATION: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT SECRET-HYGIENE: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — TIMEOUT CAUSE: NOT_PROVEN`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — CURRENT INSTRUMENTED AZURE IMAGE: PRESERVED`

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE-PROOF PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA POST-PREFLIGHT TIMEOUT RECONCILIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA PREFLIGHT IMAGE-PROOF SOURCE PUBLICATION COMPLETE`

On block:

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE-PROOF SOURCE PUBLICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

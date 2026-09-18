# GPT-5.6 Terra — Release 1.12 WP04 Wrapper Source-Commit Self-Check Publication Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: publish the already-validated one-path wrapper provenance remediation as exactly one source commit and one non-force push.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current source commit before publication:

```text
4822f9847a90a7d86c6bf771603defe9d7abf258
```

Authorized modified path:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Current expected deployed instrumented image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current normal front-door observation:

```text
HTTP root = 503
NORMAL FRONT-DOOR 503 = UNRESOLVED
```

No qualification retry is authorized by this publication authority.

## 2. Validated remediation state

Selected provenance pattern:

```text
A — governed baseline ancestry + committed-wrapper proof
```

Validated behavior:

```text
baseline commit = 4822f9847a90a7d86c6bf771603defe9d7abf258
HEAD must descend from baseline
wrapper must be tracked at HEAD
working-tree wrapper content must equal committed wrapper at HEAD
provenance failure => fail closed before Azure mutation
```

Validated failure classes include:

```text
SourceCommitUnavailable
SourceCommitMalformed
SourceBaselineNotAncestor
WrapperNotCommittedAtHead
```

Local validation already passed:

```text
V1–V10 = PASS
Windows PowerShell 5.1 AST errors = 0
Gitleaks = PASS
secret-output matches = 0
image-preflight contract = PRESERVED
retry/timeout policy = PRESERVED
tracked modified paths = 1
staged paths = 0
Azure mutations = 0
Docker/GHCR/GitHub mutations = 0
```

## 3. Publication objective

Publish exactly the validated one-path remediation.

Authorized:

```text
1 tracked path staged
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
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Before staging, prove:

```text
git status --short
```

shows exactly one tracked modification in the authorized path, plus any unrelated untracked paths that must remain untouched.

If another tracked path is modified, STOP.

## 5. Pre-publication validation

Re-run the minimum local proof before staging:

```text
Windows PowerShell 5.1 AST parse
V1–V10 provenance/image fixture matrix
git diff --check
Gitleaks
static secret-output scan
```

Require:

```text
AST errors = 0
V1–V10 = PASS
git diff --check = PASS
Gitleaks = PASS
secret-output matches = 0
```

No Azure access is required for publication validation.

## 6. Provenance contract preservation

The committed wrapper must preserve:

```text
baseline = 4822f9847a90a7d86c6bf771603defe9d7abf258
current HEAD must descend from baseline
wrapper tracked at HEAD
working-tree wrapper equals committed wrapper at HEAD
failure => local exit before Azure mutation
```

Post-publication stability must remain valid:

```text
new commit descends from baseline
committed wrapper at new HEAD equals worktree wrapper
```

This is the core acceptance condition.

## 7. Image preflight preservation

Preserve exactly:

```text
image source = siteConfig.linuxFxVersion
exactly one DOCKER|…@sha256:<64-lowercase-hex>
ordinal exact digest equality
expected digest =
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
blank/null/whitespace/tag-only/malformed/multiple/partial/wrong digest = fail closed
Azure image query failure = fail closed
```

No weakening is permitted.

## 8. Retry/timing policy preservation

Do not modify:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Preserve:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
per-request timeout = 20 seconds
Timeout = terminal
evidence-poll total wall-clock budget <= 180 seconds
listener lifetime <= 180 seconds
```

## 9. Commit contract

Create exactly one commit.

Parent must be exactly:

```text
4822f9847a90a7d86c6bf771603defe9d7abf258
```

Commit payload must contain exactly one path:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Use a concise repository-consistent message describing provenance/self-check stabilization.

Capture:

```text
SOURCE_COMMIT=<new exact SHA>
SOURCE_PARENT=4822f9847a90a7d86c6bf771603defe9d7abf258
```

Do not amend prior commits.

## 10. Push contract

Push exactly once, non-force, to the governed WP04 branch.

After push require:

```text
local HEAD = SOURCE_COMMIT
remote branch tip = SOURCE_COMMIT
```

No force push.

No second commit.

No second push.

## 11. Post-publication provenance proof

After commit and before or after push, re-run the wrapper's provenance-only local validation without invoking Azure mutation.

Prove:

```text
HEAD descends from baseline 4822f984...
wrapper is tracked at HEAD
working-tree wrapper equals committed wrapper at HEAD
provenance gate passes
```

Required marker:

```text
POST_PUBLICATION_PROVENANCE = PASS
```

This is mandatory because the remediation was specifically designed to remain valid after its own publication.

## 12. Runtime image invariance proof

Read-only prove:

```text
eng/** excluded from Docker context
Dockerfile runtime COPY does not include this wrapper
```

Required conclusion:

```text
new runtime image required = NO
GHCR publication required = NO
current deployed instrumented image remains valid
```

Do not build Docker.

## 13. Normal front-door 503 preservation

Current normal root behavior:

```text
503
```

remains unresolved.

This publication authority must not:

```text
restart
change settings
redeploy
change image
enable logging
run initialize
run reopen
attempt to clear 503
```

Required conclusion:

```text
NORMAL FRONT-DOOR 503 = UNRESOLVED
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

After publication, the next governed step must reconcile the normal-runtime `503` before any initialize attempt.

## 14. Windows PowerShell compatibility

Binding runtime:

```text
Windows PowerShell 5.1.26100.9444
```

Require:

```text
AST parse errors = 0
```

No PS7-only syntax.

## 15. Azure preservation

Required zero:

```text
settings changes = 0
restarts = 0
redeploys = 0
image changes = 0
logging changes = 0
SCM/FTP changes = 0
registry credential changes = 0
initialize attempts = 0
reopen attempts = 0
```

## 16. GitHub lifecycle preservation

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

## 17. Failed RunId preservation

All failed RunIds remain forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

No fresh RunId is authorized.

## 18. Post-publication worktree gate

After commit/push require:

```text
tracked unstaged paths = 0
staged paths = 0
local HEAD = SOURCE_COMMIT
remote branch tip = SOURCE_COMMIT
unrelated untracked files preserved
```

Do not delete unrelated untracked paths.

## 19. Mutation accounting

Authorized maximum:

```text
tracked paths staged = 1
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

## 20. Stop conditions

STOP if:

- parent commit differs;
- more than one tracked path would be staged;
- pre-publication validation regresses;
- post-publication provenance gate fails;
- baseline ancestry no longer holds;
- image-preflight contract changes;
- timeout/retry policy changes;
- Docker rebuild appears necessary;
- Azure mutation appears necessary;
- second commit/push appears necessary.

Do not widen scope.

## 21. Required return evidence

Return:

- pre-publication status;
- exact staged path list;
- pre-publication validation summary;
- exact source commit SHA;
- exact parent SHA;
- exact commit payload;
- push result;
- remote branch tip;
- post-publication provenance proof;
- image-preflight preservation result;
- retry/timing preservation result;
- Docker-context exclusion proof;
- new image required = NO;
- current front-door `503` preservation statement;
- final tracked/staged/untracked state;
- Azure zero-mutation confirmation;
- exact mutation audit.

## 22. Terminal markers

On success:

`RELEASE 1.12 WP04 — WRAPPER SOURCE-COMMIT SELF-CHECK PUBLICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-PROVENANCE PATTERN: A`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-COMMIT SELF-CHECK SOURCE COMMIT: PASS`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-COMMIT SELF-CHECK SOURCE PUSH: PASS`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-PROVENANCE POST-PUBLICATION STABILITY: PASS`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-PROVENANCE FAIL-CLOSED GATE: PASS`

`RELEASE 1.12 WP04 — IMAGE PREFLIGHT CONTRACT: PRESERVED`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 WRAPPER VALIDATION: PASS`

`RELEASE 1.12 WP04 — WRAPPER SECRET-HYGIENE: PASS`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — PER-REQUEST TIMEOUT: 20_SECONDS`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR 503: UNRESOLVED`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — CURRENT INSTRUMENTED AZURE IMAGE: PRESERVED`

`RELEASE 1.12 WP04 — WRAPPER SOURCE-COMMIT SELF-CHECK PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA NORMAL FRONT-DOOR 503 RECONCILIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA WRAPPER SOURCE-COMMIT SELF-CHECK PUBLICATION COMPLETE`

On block:

`RELEASE 1.12 WP04 — WRAPPER SOURCE-COMMIT SELF-CHECK SOURCE PUBLICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

# GPT-5.6 Terra — Release 1.12 WP04 Application-Owned HTTP Evidence Endpoint Implementation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the exact E1 application-owned HTTP evidence-channel remediation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release: **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue: `#263`

Accepted architecture: `E1 — H1_APPLICATION_HTTP_EVIDENCE`  
Source of truth: `H1-S2 — the .NET application reads its own atomic durable JSON artifact`  
Lifetime: `H1-L1 — the Worker remains alive in bounded evidence-serving mode after qualification and exits after successful retrieval or timeout`

Superseded historical provenance:

```text
Source commit:
2add79d2063292687d9813f9022206e84b664627

Image digest:
sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378
```

The original WP04 persistence acceptance boundary remains preserved. Schema and README changes are forbidden.

## 2. Exact implementation allowlist

Exactly these 7 literal paths are authorized:

```text
MODIFY src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
CREATE src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
CREATE tests/AIQuantTradingResearch.Infrastructure.Tests/PersistentSqliteQualificationEvidenceEndpointTests.cs
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Path count: `7`  
Operations: `5 MODIFY`, `2 CREATE`

No other tracked file is authorized.

## 3. HTTP endpoint contract

Implement exactly one qualification-only endpoint:

```text
GET /internal/wp04/persistence-qualification
```

Required query:

```text
runId=<exact-run-id>
```

Required header:

```text
X-WP04-Evidence-Token
```

Activation:

```text
PersistentSqliteQualification__HttpEvidenceEnabled=true
```

Use a narrowly named qualification-only token setting, e.g.:

```text
PersistentSqliteQualification__HttpEvidenceToken
```

The endpoint must be unavailable unless explicitly enabled.

## 4. Capability-token contract

The token must be high-entropy and temporary.

It must never be:
- persisted in the JSON artifact;
- logged;
- printed;
- committed;
- returned in response payloads.

It must be removed/restored after qualification.

Do not introduce a generic authentication framework or paid service.

## 5. Payload contract

Return only:

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

Do not expose secrets, connection strings, environment dumps, auth headers, publish credentials, arbitrary filesystem paths, arbitrary file contents, or SQLite query results.

## 6. Source of truth

Implement H1-S2.

The endpoint must read the application-owned atomic durable qualification artifact. The application may read its own artifact. The helper must not read `/home` directly and must not inspect SQLite.

Preserve existing atomic-write/failure semantics.

## 7. Exact-run attribution

Acceptance requires exact equality:

```text
request runId == artifact RunId
```

Wrong-run or stale evidence must fail. Never return “latest” evidence implicitly.

## 8. Bounded evidence-serving lifetime

After successful qualification and durable-artifact write:

1. enter evidence-serving mode;
2. expose only the governed endpoint;
3. serve only the exact expected run;
4. exit after successful exact-run retrieval or timeout.

Maximum evidence window:

```text
180 seconds
```

The Worker must not remain alive indefinitely.

## 9. Readiness semantics

Use deterministic readiness semantics.

Before evidence is ready, use one governed retryable status from:

```text
404
409
425
```

Choose the narrowest semantically correct status and test it.

Exact valid record: `200`.

Malformed artifact, wrong run, invalid token, failed payload, internal qualification failure, and timeout are terminal.

Do not return `200` for not-ready or stale evidence.

## 10. Normal runtime

When HTTP evidence is not explicitly enabled, the endpoint must not exist/be reachable.

After temporary qualification settings are restored:
- endpoint unavailable;
- token absent;
- qualification Worker no longer serves evidence;
- ordinary runtime resumes.

Do not expose the endpoint through Streamlit or Python.

## 11. Worker hosting boundary

The current Worker is a console process. Add only the minimum HTTP hosting capability needed for this qualification endpoint.

Do not convert the application into a general-purpose Web API. Do not introduce unrelated controllers, routes, middleware, or a second persistence implementation.

## 12. Verification helper remediation

Modify:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Replace Kudu VFS retrieval with public application HTTP polling.

Required flow:

1. generate/accept fresh RunId;
2. generate high-entropy temporary token;
3. snapshot temporary settings;
4. set Worker mode, phase, RunId, durable output path, HTTP evidence enabled, and token;
5. perform caller-authorized lifecycle action;
6. poll:

```text
GET https://<public-app-host>/internal/wp04/persistence-qualification?runId=<exact-run-id>
```

with:

```text
X-WP04-Evidence-Token: <temporary token>
```

7. poll every `5 seconds`;
8. stop after at most `180 seconds`;
9. retry only governed not-ready statuses;
10. validate exactly the eleven governed fields;
11. require exact RunId equality;
12. restore temporary settings;
13. fail nonzero on timeout, malformed response, stale/wrong run, terminal HTTP status, invalid field, or restoration failure.

The helper must never print the token. Remove Kudu VFS from the active evidence path. Direct SQLite access remains forbidden.

## 13. Required tests

Create focused endpoint tests for at least:

- disabled by default;
- activation required;
- missing token rejected;
- incorrect token rejected;
- correct token accepted;
- exact RunId accepted;
- stale/wrong RunId rejected;
- malformed artifact rejected;
- exact eleven-field payload;
- no token/secret leakage;
- non-ready state is retryable and non-200;
- successful retrieval completes bounded serving lifecycle;
- timeout produces bounded failure;
- no arbitrary file access;
- no SQLite access.

Use the smallest test-host mechanism consistent with the existing repository.

## 14. Build/test gates

Require:

```text
dotnet build -c Release
```

with:

```text
0 warnings
0 errors
```

Run and pass the existing:
- Domain tests;
- Application tests;
- Architecture tests;
- Infrastructure tests;
- targeted endpoint tests.

Use the repository's established exact commands where available.

## 15. Signing contract

Preserve:

```text
Release build: required
Release Authenticode signature: not required
Debug local signing contract: required
Expected signer: CN=AIQuantTradingDev
```

Do not manually sign Release binaries. Do not weaken tracked signing policy.

Required marker:

`RELEASE 1.12 WP04 — CORRECTED SIGNING GATE: PASS`

## 16. Static/local validation

Require:

- PowerShell AST parse: 0 errors;
- `git diff --check`: pass;
- Gitleaks: pass for all 7 authorized paths;
- no direct SQLite access from helper;
- no active Kudu VFS retrieval in helper;
- no Streamlit/Python persistence access;
- exact tracked diff equals the 7-path allowlist.

Perform a local qualification proving:
- artifact is produced atomically;
- HTTP evidence mode starts;
- wrong token fails;
- wrong run fails;
- exact token + RunId succeeds;
- exact payload validates;
- successful retrieval causes bounded completion;
- endpoint is unavailable when disabled.

Required:

`RELEASE 1.12 WP04 — LOCAL HTTP EVIDENCE QUALIFICATION: PASS`

## 17. Mutation boundary

This authority permits local repository implementation and local validation only.

It does **not** authorize:
- staging;
- commit;
- push;
- Docker build/publication;
- GHCR publication;
- Azure mutation;
- SCM-policy mutation;
- PR creation;
- issue closure;
- Project #2 mutation;
- milestone mutation.

Stop after exact 7-path implementation and local validation pass.

A separate Terra candidate-publication authority is required.

## 18. SCM policy

Current Azure SCM basic-auth remains `allow=true`, but this authority does not mutate Azure.

Because E1 does not need Kudu, a later Terra Azure authority must restore it to:

```text
allow=false
```

FTP remains `allow=false`.

## 19. Mutation accounting

Authorized:

```text
Repository working-tree mutations: exactly 7 governed paths
```

Expected zero:

```text
Git staging: 0
Git commits: 0
Git pushes: 0
Git tags: 0
Docker/GHCR mutations: 0
Azure mutations: 0
Provider mutations: 0
PR mutations: 0
Issue mutations: 0
Project #2 mutations: 0
Milestone mutations: 0
```

Required:

`RELEASE 1.12 WP04 — HTTP EVIDENCE IMPLEMENTATION MUTATION AUDIT: PASS`

## 20. Stop conditions

STOP if:
- any eighth tracked path is required;
- schema/README change is needed;
- Streamlit/Python persistence ownership appears;
- arbitrary file access is needed;
- endpoint cannot be disabled by default;
- token secrecy cannot be maintained;
- exact-run attribution cannot be enforced;
- bounded lifetime cannot be implemented narrowly;
- persistence semantics regress;
- tracked signing policy would need change;
- paid Azure dependency appears.

Do not widen scope without Luna reconciliation.

## 21. Required return evidence

Return:
- exact changed paths and operation count;
- endpoint behavior and selected not-ready status;
- lifetime/timeout behavior;
- token-handling proof;
- local qualification evidence;
- Release build result;
- test counts;
- PowerShell AST result;
- Gitleaks result;
- direct-SQLite scan result;
- Kudu-VFS active-path scan result;
- Debug signing result;
- `git diff --check`;
- Git status showing no staged changes;
- exact mutation accounting.

Do not return any token value.

## 22. Terminal markers

Full success:

`RELEASE 1.12 WP04 — HTTP EVIDENCE ENDPOINT IMPLEMENTATION: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE ENDPOINT DISABLED-BY-DEFAULT: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE EXACT-RUN ATTRIBUTION: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CAPABILITY-TOKEN CONTRACT: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE BOUNDED LIFETIME: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE DETERMINISTIC READINESS: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE HELPER REMEDIATION: PASS`

`RELEASE 1.12 WP04 — LOCAL HTTP EVIDENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — CORRECTED SIGNING GATE: PASS`

`RELEASE 1.12 WP04 — SEVEN-PATH IMPLEMENTATION PAYLOAD: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE IMPLEMENTATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA HTTP EVIDENCE IMPLEMENTATION COMPLETE`

`RELEASE 1.12 WP04 — TERRA CANDIDATE PUBLICATION AUTHORITY: READY`

Blocked:

`RELEASE 1.12 WP04 — HTTP EVIDENCE IMPLEMENTATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

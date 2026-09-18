# GPT-5.6 Terra — Release 1.12 WP04 HTTP Evidence Candidate Publication Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: perform exact candidate publication after all local implementation/validation gates are proven.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Accepted architecture:

```text
E1 — H1_APPLICATION_HTTP_EVIDENCE
R1 — Q1_QUALIFICATION_SINGLE_LISTENER_SUBSTITUTION
```

Accepted exact implementation payload:

```text
MODIFY src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
CREATE src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
CREATE tests/AIQuantTradingResearch.Infrastructure.Tests/PersistentSqliteQualificationEvidenceEndpointTests.cs
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
MODIFY container/entrypoint.sh
```

Exact path count:

```text
8
```

Operations:

```text
6 MODIFY
2 CREATE
```

Accepted Docker qualification evidence from the immediately preceding Terra implementation authority:

```text
missing-token initialize response: 401
missing-token reopen response: 401
authorized initialize retrieval: succeeded on first poll
authorized reopen retrieval: succeeded on first poll
initialize integrityCheck: ok
reopen integrityCheck: ok
initialize journalMode: delete
reopen journalMode: delete
initialize schemaVersion: 4
reopen schemaVersion: 4
initialize AcceptedEvidenceCount: 1
reopen AcceptedEvidenceCount: 1
reopen AcceptedEvidenceIdentity: same as initialize
initialize container exit: 0
reopen container exit: 0
image cleanup: complete
volume cleanup: complete
container cleanup: complete
residue checks: false
```

Accepted marker:

`RELEASE 1.12 WP04 — LOCAL HTTP EVIDENCE DOCKER VALIDATION: PASS`

Historical superseded candidate provenance remains immutable:

```text
Source commit:
2add79d2063292687d9813f9022206e84b664627

Image digest:
sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378
```

## 2. Publication precondition: all prior local gates must already pass

Before any Git mutation, verify and report all required local gates from the prior implementation authority.

Required:

```text
RELEASE 1.12 WP04 — QUALIFICATION SINGLE-LISTENER IMPLEMENTATION: PASS
RELEASE 1.12 WP04 — NORMAL RUNTIME COMPOSITION PRESERVATION: PASS
RELEASE 1.12 WP04 — QUALIFICATION PORT-8501 OWNERSHIP: PASS
RELEASE 1.12 WP04 — STREAMLIT SUPPRESSION IN QUALIFICATION MODE: PASS
RELEASE 1.12 WP04 — HTTP EVIDENCE ENDPOINT IMPLEMENTATION: PASS
RELEASE 1.12 WP04 — HTTP EVIDENCE ENDPOINT DISABLED-BY-DEFAULT: PASS
RELEASE 1.12 WP04 — HTTP EVIDENCE EXACT-RUN ATTRIBUTION: PASS
RELEASE 1.12 WP04 — HTTP EVIDENCE CAPABILITY-TOKEN CONTRACT: PASS
RELEASE 1.12 WP04 — HTTP EVIDENCE BOUNDED LIFETIME: PASS
RELEASE 1.12 WP04 — HTTP EVIDENCE DETERMINISTIC READINESS: PASS
RELEASE 1.12 WP04 — HTTP EVIDENCE HELPER REMEDIATION: PASS
RELEASE 1.12 WP04 — LOCAL HTTP EVIDENCE DOCKER VALIDATION: PASS
RELEASE 1.12 WP04 — CORRECTED SIGNING GATE: PASS
RELEASE 1.12 WP04 — EIGHT-PATH IMPLEMENTATION PAYLOAD: PASS
RELEASE 1.12 WP04 — QUALIFICATION RUNTIME IMPLEMENTATION MUTATION AUDIT: PASS
```

If any required marker/evidence is missing or not reproducible, STOP before staging.

## 3. Exact repository precheck

Require:

- branch is the governed WP04 branch;
- working tree contains exactly the 8 authorized tracked paths and no unrelated tracked mutation;
- no staged changes exist before this authority begins;
- untracked unrelated material remains untouched;
- `Dockerfile` unchanged;
- README unchanged;
- schema/migrations unchanged;
- no Streamlit/Python persistence code changed.

Run:

```text
git diff --check
```

Must pass.

Run exact path inventory and prove:

```text
8 authorized tracked paths
0 unauthorized tracked paths
```

## 4. Re-run publication validation gates

Before staging, re-run:

```text
dotnet build -c Release
```

Required:

```text
0 warnings
0 errors
```

Re-run and pass:

- Domain tests;
- Application tests;
- Architecture tests;
- Infrastructure tests;
- targeted qualification endpoint tests.

Also require:

- PowerShell AST parse: 0 errors;
- shell syntax validation for `container/entrypoint.sh`;
- Gitleaks: no leaks on all 8 authorized paths;
- helper direct-SQLite scan: 0;
- helper active-Kudu-VFS evidence-path scan: 0;
- corrected Debug signing gate:
  - expected signer `CN=AIQuantTradingDev`;
- `git diff --check`: pass.

Do not manually sign Release binaries.

## 5. Stage exact payload

Stage exactly the 8 authorized paths and nothing else.

After staging require:

```text
staged path count: 8
staged unauthorized paths: 0
unstaged tracked changes: 0
```

If any ninth tracked path is required, STOP.

## 6. Candidate commit

Create exactly one candidate commit for the HTTP evidence/runtime-composition remediation.

Commit message should clearly identify Release 1.12 WP04 qualification HTTP evidence remediation.

Capture exact full commit SHA:

```text
<NEW_CANDIDATE_COMMIT>
```

Required:

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE COMMIT: PASS`

## 7. Exact branch push

Push the exact candidate commit non-force to:

```text
release/1.12-wp04-persistent-sqlite
```

Require remote branch tip to equal:

```text
<NEW_CANDIDATE_COMMIT>
```

No force push.

No other branch mutation.

Required:

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE PUSH: PASS`

## 8. Build exact candidate image

Build from the exact candidate commit.

Use candidate tag:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp04-<FULL_COMMIT_SHA>
```

Build with repository Dockerfile unchanged.

Require source/image provenance to point to the exact candidate commit.

Do not publish a floating release tag.

Required:

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE IMAGE BUILD: PASS`

## 9. Publish exact candidate image to GHCR

Push exactly the candidate tag:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp04-<FULL_COMMIT_SHA>
```

Capture immutable digest:

```text
sha256:<NEW_DIGEST>
```

Require anonymous manifest read to succeed and return the same digest.

Do not expose credentials.

Clean any temporary anonymous Docker config created solely for the manifest read.

Required markers:

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE IMAGE ANONYMOUS READ: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE SOURCE-IMAGE PROVENANCE: PASS`

## 10. No Azure mutation under this authority

This authority does **not** authorize:

- App Service image update;
- restart;
- redeploy;
- D3 initialize/reopen;
- temporary app settings;
- SCM basic-auth restoration;
- FTP changes;
- Azure port setting changes;
- PR creation;
- issue closure;
- Project #2 mutation;
- milestone mutation.

Stop after exact Git/GHCR publication.

A separate Terra Azure qualification authority is required.

## 11. SCM basic-auth state

Current Azure SCM basic-auth remains historically:

```text
allow=true
```

Do not mutate it here.

The next Azure qualification authority must restore it to:

```text
allow=false
```

before final WP04 acceptance because E1/R1 does not depend on Kudu.

FTP must remain:

```text
allow=false
```

## 12. Exact mutation accounting

Authorized mutation categories:

```text
Git staging:
  exactly 8 paths

Git commits:
  exactly 1 candidate commit

Git pushes:
  exactly 1 non-force branch update

Local Docker:
  candidate image build

GHCR:
  exactly 1 exact candidate-tag publication
```

Expected zero:

```text
Azure mutations: 0
Provider mutations: 0
PR mutations: 0
Issue mutations: 0
Project #2 mutations: 0
Milestone mutations: 0
Git tag mutations: 0
Force pushes: 0
```

Report any temporary local Docker resources and cleanup status.

## 13. Stop conditions

STOP if:

- prior local validation gates are incomplete;
- exact diff is not 8 paths;
- any unauthorized tracked path exists;
- build/test/static/signing gates fail;
- push would require force;
- candidate source commit cannot be proven exact;
- image digest cannot be retrieved;
- anonymous GHCR read fails;
- any Azure mutation would be needed.

Do not widen scope.

## 14. Required return evidence

Return:

- exact 8 staged paths;
- Release build result;
- all test counts;
- PowerShell AST result;
- shell syntax result;
- Gitleaks result;
- Debug signing result;
- `git diff --check`;
- new exact candidate commit SHA;
- remote branch tip;
- exact GHCR candidate tag;
- immutable digest;
- anonymous manifest read result;
- source/image provenance proof;
- local Docker cleanup state;
- exact mutation accounting.

Do not return credentials/tokens.

## 15. Terminal markers

Full success requires:

`RELEASE 1.12 WP04 — HTTP EVIDENCE PUBLICATION PRECHECK: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE PUBLICATION VALIDATION GATES: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE COMMIT: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE PUSH: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE IMAGE BUILD: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE IMAGE PUBLICATION: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE IMAGE ANONYMOUS READ: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE SOURCE-IMAGE PROVENANCE: PASS`

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE PUBLICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA EXACT-DIGEST AZURE QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA HTTP EVIDENCE CANDIDATE PUBLICATION COMPLETE`

Blocked:

`RELEASE 1.12 WP04 — HTTP EVIDENCE CANDIDATE PUBLICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

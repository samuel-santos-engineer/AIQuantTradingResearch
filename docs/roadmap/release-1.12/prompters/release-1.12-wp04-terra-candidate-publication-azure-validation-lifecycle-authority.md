# GPT-5.6 Terra --- Release 1.12 WP04 Candidate Publication, Azure Validation & Lifecycle Completion Authority

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

-   **GPT-5.6 Luna** --- contract, policy, architecture, reconciliation,
    acceptance criteria, governance, read-only/planning.
-   **GPT-5.6 Terra** --- PRIMARY for this prompt: final validation
    execution, approved Git/GitHub/Azure mutations, PR
    publication/merge, post-merge verification, and WP04 lifecycle
    completion.
-   **GPT-5.6 Sol** --- supporting analysis/synthesis only; never
    silently replaces Luna or Terra.

------------------------------------------------------------------------

## 1. Governed starting point

Resume:

**Phase 4 --- Release 1.12 WP04: Persistent SQLite Initialization, Data
Update & Recovery**

Issue:

`#263`

Canonical historical base for the WP04 work:

`40a9a236dae789864f35e64bb5c1afd358e7b3db`

Current working branch:

`release/1.12-wp04-persistent-sqlite`

The governed implementation path set is exactly eleven tracked paths:

``` text
MODIFY Dockerfile
MODIFY container/entrypoint.sh
MODIFY src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs
MODIFY src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite.ps1
CREATE src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqlitePersistenceDiagnostics.cs
CREATE src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
CREATE tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceDiagnosticsTests.cs
```

A local untracked operator helper exists:

``` text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/validate-docker-qualification.ps1
```

It is **not** part of the governed implementation payload and MUST NOT
be staged or published unless Luna separately re-governs it.

Unrelated untracked `prompters/` files remain outside scope and MUST
remain untouched.

------------------------------------------------------------------------

## 2. Proven Docker qualification state

The final clean-recreate local Docker qualification passed.

Evidence:

-   fresh image build: PASS;
-   initialize runtime UID: `1000`;
-   initialize persistence owner: `1000:1000`;
-   initialize persistence mode: `750`;
-   reopen runtime UID: `1000`;
-   reopen persistence owner: `1000:1000`;
-   reopen persistence mode: `750`;
-   both qualification records:
    -   schema `4`;
    -   journal `delete`;
    -   integrity `ok`;
    -   quick-check `ok`;
-   accepted evidence identity/count preserved across container
    recreate;
-   fresh image, volume, and both containers removed;
-   no Docker residue remains.

Accepted markers:

``` text
WP04_LOCAL_PERSISTENCE_RECREATE_PASS=True
WP04_LOCAL_NONROOT_RUNTIME_PASS=True
WP04_LOCAL_STORAGE_OWNER_PASS=True
WP04_LOCAL_STORAGE_MODE_PASS=True
WP04_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=False
WP04_LOCAL_VOLUME_PRESENT_AFTER_CLEANUP=False
WP04_LOCAL_CLEANUP_COMPLETE=True
```

Prior Docker mutation accounting:

``` text
Pre-remediation image removed: 1
Pre-remediation volume removed: 1
Fresh images created/removed: 1/1
Fresh volumes created/removed: 1/1
Containers created/removed: 2/2
Repository mutations: 0
Azure mutations: 0
GHCR mutations: 0
Provider mutations: 0
GitHub/lifecycle mutations: 0
```

Accepted governance markers:

`RELEASE 1.12 WP04 — E2-A NON-ROOT STORAGE REMEDIATION: PASS`

`RELEASE 1.12 WP04 — DOCKER QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — PERSISTENCE RECREATE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — DOCKER QUALIFICATION MUTATION AUDIT: PASS`

------------------------------------------------------------------------

## 3. First action --- fresh reconciliation

Before any mutation:

1.  `git fetch --all --prune`;
2.  verify current branch;
3.  verify local/remote `main`;
4.  prove canonical WP04 base remains contained in current
    `origin/main`;
5.  verify whether `origin/main` advanced since the original WP04
    anchor;
6.  inspect issue `#263`;
7.  inspect WP05 issue `#264`;
8.  inspect milestone `#63`;
9.  inspect Project #2 status for #263/#264;
10. verify no existing WP04 PR already supersedes this authority;
11. inspect staged/unstaged/untracked state.

If `origin/main` advanced, do not silently assume compatibility.
Reconcile the branch against current `origin/main` and prove that the
eleven-path payload remains semantically and textually valid. If
conflict or scope drift requires any additional governed path, STOP for
Luna re-governance.

Required:

`RELEASE 1.12 WP04 — PRE-PUBLICATION RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — CURRENT ORIGIN/MAIN: <sha>`

`RELEASE 1.12 WP04 — WP04 BASE CONTAINED IN MAIN: PASS`

`RELEASE 1.12 WP04 — EXISTING WP04 PR: <NONE|number>`

------------------------------------------------------------------------

## 4. Validate the eleven-path candidate payload

Before staging:

-   verify exactly the eleven governed tracked paths differ from current
    `origin/main`;
-   verify no additional tracked path differs;
-   verify the untracked Docker qualification helper is excluded;
-   verify `prompters/` remains excluded;
-   verify README unchanged;
-   verify historical Initiative-1.11 artifacts unchanged;
-   verify WP03 paths unchanged;
-   verify schema migration absent.

Required:

`RELEASE 1.12 WP04 — ELEVEN-PATH CANDIDATE PAYLOAD: PASS`

`RELEASE 1.12 WP04 — UNTRACKED HELPER EXCLUDED: PASS`

`RELEASE 1.12 WP04 — README MUTATION: ABSENT`

`RELEASE 1.12 WP04 — SCHEMA MIGRATION: ABSENT`

------------------------------------------------------------------------

## 5. Fresh local validation gate

Do not rely solely on earlier validation if it cannot be proven from
current working-tree state.

Re-run fresh validation against the exact candidate payload after
reconciliation and before commit.

At minimum:

1.  `dotnet build`
2.  Domain tests
3.  Application tests
4.  Architecture tests
5.  Infrastructure tests
6.  preserved unavailable-path regression
7.  targeted SQLite persistence tests
8.  diagnostics tests
9.  PowerShell parsing/static validation for both governed WP04 scripts
10. shell syntax validation for `container/entrypoint.sh`
11. Gitleaks against the exact eleven-path payload
12. `git diff --check`
13. exact eleven-path payload audit

The local Authenticode development signing prerequisite must use the
already-governed local certificate/ignored override mechanism. Do not
weaken or bypass signing.

Report fresh exact counts.

Required:

`RELEASE 1.12 WP04 — BUILD VALIDATION: PASS`

`RELEASE 1.12 WP04 — DOMAIN TESTS: PASS`

`RELEASE 1.12 WP04 — APPLICATION TESTS: PASS`

`RELEASE 1.12 WP04 — ARCHITECTURE TESTS: PASS`

`RELEASE 1.12 WP04 — INFRASTRUCTURE TESTS: PASS`

`RELEASE 1.12 WP04 — REGRESSION TEST: PASS`

`RELEASE 1.12 WP04 — SQLITE TARGETED TESTS: PASS`

`RELEASE 1.12 WP04 — DIAGNOSTICS TESTS: PASS`

`RELEASE 1.12 WP04 — POWERSHELL VALIDATION: PASS`

`RELEASE 1.12 WP04 — ENTRYPOINT SHELL VALIDATION: PASS`

`RELEASE 1.12 WP04 — GITLEAKS: PASS`

`RELEASE 1.12 WP04 — DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — ELEVEN-PATH PAYLOAD AUDIT: PASS`

If any gate fails, STOP before staging.

------------------------------------------------------------------------

## 6. Candidate commit

Only after all local gates pass:

1.  stage exactly the eleven governed tracked paths;
2.  prove staged path equality against the allowlist;
3.  ensure the untracked qualification helper remains unstaged;
4.  ensure `prompters/` remains unstaged;
5.  commit once with a narrowly scoped WP04 message.

Suggested commit subject:

``` text
feat(release-1.12): stabilize persistent SQLite runtime
```

Record candidate SHA.

Required:

`RELEASE 1.12 WP04 — STAGED PATH EQUALITY: PASS`

`RELEASE 1.12 WP04 — CANDIDATE COMMIT: <sha>`

`RELEASE 1.12 WP04 — CANDIDATE PAYLOAD 11/11: PASS`

------------------------------------------------------------------------

## 7. Push and PR publication

Push the existing WP04 branch.

Create one PR targeting `main`.

PR scope must describe:

-   explicit SQLite storage initialization flag;
-   default no-creation semantics;
-   DELETE journal enforcement;
-   application-owned bounded persistence qualification;
-   E2-A bounded root startup storage preparation;
-   permanent privilege drop to `aiq`;
-   Docker recreate persistence proof;
-   no schema migration;
-   no provider behavior;
-   no direct-SQL deployment bypass;
-   no README change.

Do not include the local untracked qualification helper in the PR
payload.

Capture:

-   PR number;
-   PR URL;
-   head SHA;
-   exact changed path count.

Required:

`RELEASE 1.12 WP04 — PR CREATED: PASS`

`RELEASE 1.12 WP04 — PR NUMBER: <number>`

`RELEASE 1.12 WP04 — PR HEAD: <sha>`

`RELEASE 1.12 WP04 — PR PAYLOAD 11/11: PASS`

------------------------------------------------------------------------

## 8. Azure App Service F1 deployed validation

Before merge, perform the governed WP04 deployed persistence
qualification against the existing Release 1.12 Azure reference
deployment boundary.

Target contract:

-   Azure App Service Linux F1;
-   West Central US;
-   custom Docker;
-   public/free GHCR;
-   persistent `/home`;
-   `Persistence__DatabasePath=/home/data/aiquant.db`;
-   explicit application-owned storage initialization enabled;
-   SQLite schema v4;
-   SQLite DELETE journal;
-   bounded/non-root runtime;
-   no Azure SQL;
-   no paid dependency;
-   no production/SLA claim.

### 8.1 Image publication boundary

If deployed validation requires a candidate container image:

-   publish only the exact candidate commit;
-   use the existing governed public/free GHCR path;
-   capture immutable digest;
-   do not overwrite historical evidence tags unless current repository
    scripts explicitly govern that behavior;
-   do not introduce ACR or paid registry dependency.

Required:

`RELEASE 1.12 WP04 — CANDIDATE GHCR IMAGE: <image@sha256:digest>`

### 8.2 Configure persistent SQLite

Use the governed WP04 deployment/configuration mechanism.

Prove deployed configuration resolves to:

`/home/data/aiquant.db`

and explicit initialization is enabled.

Do not expose secrets.

Required:

`RELEASE 1.12 WP04 — AZURE SQLITE PATH: /home/data/aiquant.db`

`RELEASE 1.12 WP04 — AZURE EXPLICIT INITIALIZATION: ENABLED`

### 8.3 Deployment/runtime proof

Prove:

-   container starts successfully;
-   long-running application runtime is non-root;
-   storage parent preparation succeeds;
-   `/home/data` remains persistent;
-   application-owned SQLite initialization succeeds;
-   schema remains v4;
-   journal remains DELETE;
-   no direct-SQL deployment bypass is used.

### 8.4 Persistence continuity

Perform bounded persistence continuity validation through approved
application-owned mechanisms.

At minimum prove continuity across:

1.  initial deployment/start;
2.  app restart;
3.  redeployment/restart of the candidate image/configuration boundary.

Use application-owned qualification/evidence mechanisms. Do not use
shell/Python direct SQLite domain-row inspection.

Prove:

-   accepted evidence survives;
-   identity/count remains coherent;
-   schema remains v4;
-   journal remains delete;
-   integrity remains ok;
-   quick-check remains ok.

Required:

`RELEASE 1.12 WP04 — AZURE PERSISTENT INITIALIZATION: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE REDEPLOYMENT PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE SCHEMA V4: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE DELETE JOURNAL: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE INTEGRITY: PASS`

`RELEASE 1.12 WP04 — AZURE NON-ROOT RUNTIME: PASS`

`RELEASE 1.12 WP04 — AZURE DIRECT-SQL BYPASS: ABSENT`

### 8.5 Cost and scope guard

Confirm:

-   F1 remains selected;
-   no paid Azure dependency was added;
-   no Azure SQL;
-   no ACR requirement;
-   no Container Apps/Azure Files addition.

Required:

`RELEASE 1.12 WP04 — AZURE STRICT-ZERO-COST BOUNDARY: PRESERVED`

If Azure validation cannot complete without expanding
architecture/scope, STOP before merge.

------------------------------------------------------------------------

## 9. PR merge gate

Only after:

-   fresh local validation passes;
-   exact PR payload is 11/11;
-   candidate GHCR/deployed validation passes;
-   no scope expansion exists;
-   GitHub checks required by repository policy are green;

merge the PR using the repository's normal governed merge method.

Capture:

-   PR number;
-   merge SHA;
-   merge timestamp;
-   final payload.

Required:

`RELEASE 1.12 WP04 — PR MERGE: PASS`

`RELEASE 1.12 WP04 — MERGE SHA: <sha>`

------------------------------------------------------------------------

## 10. Post-merge verification

After merge:

1.  fetch/prune;
2.  checkout/update local `main`;
3.  prove local `main == origin/main` at 0/0;
4.  prove merge SHA contained at HEAD/current main;
5.  prove exact eleven-path merged payload;
6.  rerun the appropriate post-merge validation set;
7.  confirm Azure deployed state still satisfies WP04 persistence
    contract;
8.  confirm no unintended Docker residue locally;
9.  confirm no secret leakage;
10. confirm no unexpected paid/cloud resource.

Required:

`RELEASE 1.12 WP04 — POST-MERGE MAIN 0/0: PASS`

`RELEASE 1.12 WP04 — POST-MERGE PAYLOAD 11/11: PASS`

`RELEASE 1.12 WP04 — POST-MERGE VALIDATION: PASS`

------------------------------------------------------------------------

## 11. WP04 lifecycle completion

Only after all WP04 acceptance evidence is complete:

1.  close GitHub issue `#263`;
2.  set Project #2 WP04 Status to **Done** if closure automation does
    not already do so;
3.  do not make a redundant Project mutation if issue closure
    automatically transitions it to Done;
4.  verify milestone `#63` remains **Open**;
5.  verify WP05 issue `#264` remains **Open / Todo** and is next.

Do not close milestone #63.

Required:

`RELEASE 1.12 WP04 — ISSUE #263 CLOSED: PASS`

`RELEASE 1.12 WP04 — PROJECT STATUS DONE: PASS`

`RELEASE 1.12 WP04 — MILESTONE #63 REMAINS OPEN: PASS`

`RELEASE 1.12 WP04 — WP05 #264 NEXT: PASS`

`RELEASE 1.12 WP04 — LIFECYCLE COMPLETION: PASS`

------------------------------------------------------------------------

## 12. Mutation accounting

Report exact actual mutations.

### Repository/Git

-   staged paths: exactly 11
-   commits: 1 candidate commit
-   pushes: as actually performed
-   PRs created: 1
-   PR merges: 1

### GHCR

-   candidate images/tags/digests created: exact count
-   historical tags changed: expected 0 unless governed by existing
    script behavior
-   final retained candidate/evidence image state: report exactly

### Azure

Report every explicit mutation, including:

-   app setting/config changes;
-   image reference changes;
-   restarts;
-   redeployments;
-   any temporary validation configuration;
-   rollback/restoration actions if performed.

### GitHub lifecycle

-   issue #263 close: 1
-   Project #2 Status mutation: `0 or 1`, depending on automation
-   milestone #63 mutation: 0

### Local Docker

No new local Docker mutation is required by this authority unless Terra
must reproduce a failed local proof. If unnecessary, report 0.

### Exclusions

-   provider mutations: 0
-   schema migrations: 0
-   README mutations: 0
-   WP05 implementation mutations: 0

Required:

`RELEASE 1.12 WP04 — FINAL MUTATION AUDIT: PASS`

------------------------------------------------------------------------

## 13. Stop conditions

STOP immediately if any of the following occurs:

-   current `origin/main` introduces unresolved semantic conflict;
-   candidate differs outside the eleven tracked paths;
-   local helper or `prompters/` becomes staged;
-   signing contract must be weakened;
-   tests/build fail;
-   Azure validation requires direct SQL domain inspection;
-   Azure deployment requires paid infrastructure;
-   Azure validation requires schema migration;
-   PR payload differs from 11/11;
-   merge would include unrelated work;
-   issue closure would occur before acceptance completion.

------------------------------------------------------------------------

## 14. Required terminal output

On full success:

`RELEASE 1.12 WP04 — PERSISTENT SQLITE INITIALIZATION, DATA UPDATE & RECOVERY: PASS`

`RELEASE 1.12 WP04 — LOCAL DOCKER QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE DEPLOYED PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — PR PUBLICATION AND MERGE: PASS`

`RELEASE 1.12 WP04 — POST-MERGE VALIDATION: PASS`

`RELEASE 1.12 WP04 — ISSUE #263 CLOSED: PASS`

`RELEASE 1.12 WP04 — PROJECT STATUS DONE: PASS`

`RELEASE 1.12 WP04 — LIFECYCLE COMPLETION: PASS`

`RELEASE 1.12 WP05 — EXECUTION AUTHORITY: READY`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY COMPLETE`

If blocked before completion:

`RELEASE 1.12 WP04 — FINALIZATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

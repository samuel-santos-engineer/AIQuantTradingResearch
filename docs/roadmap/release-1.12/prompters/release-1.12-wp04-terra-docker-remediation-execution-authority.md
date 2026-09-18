# GPT-5.6 Terra --- Release 1.12 WP04 Docker Remediation Execution Authority

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

-   **GPT-5.6 Luna** --- contract, policy, architecture, reconciliation,
    acceptance criteria, governance, read-only planning.
-   **GPT-5.6 Terra** --- PRIMARY for this prompt: implementation,
    validation execution, approved Docker mutations, and exact mutation
    accounting.
-   **GPT-5.6 Sol** --- supporting analysis/synthesis only; never
    silently replaces Luna or Terra.

------------------------------------------------------------------------

## 1. Governed starting state

Resume:

**Phase 4 --- Release 1.12 WP04: Persistent SQLite Initialization, Data
Update & Recovery**

Issue:

`#263`

Canonical base:

`40a9a236dae789864f35e64bb5c1afd358e7b3db`

Current branch:

`release/1.12-wp04-persistent-sqlite`

The prior Option C remediation is already present locally and unstaged.

Fresh Luna amendment established:

`RELEASE 1.12 WP04 — DOCKER VALIDATION CONTRACT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — ENTRYPOINT BYPASS IDENTIFIED: PASS`

`RELEASE 1.12 WP04 — SELECTED ENTRYPOINT OPTION: E1`

`RELEASE 1.12 WP04 — SELECTED DOCKER EVIDENCE OPTION: D3`

`RELEASE 1.12 WP04 — APPLICATION-OWNED EVIDENCE BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — DIRECT-SQL DOCKER BYPASS: ABSENT`

`RELEASE 1.12 WP04 — ORIGINAL DOCKER JSON FORMAT: NOT REQUIRED`

`RELEASE 1.12 WP04 — ENTRYPOINT PATH ADDITION REQUIRED: YES`

`RELEASE 1.12 WP04 — DIAGNOSTIC PRODUCTION PATH ADDITION REQUIRED: YES`

`RELEASE 1.12 WP04 — DIAGNOSTIC TEST PATH ADDITION REQUIRED: YES`

`RELEASE 1.12 WP04 — README MUTATION REQUIRED: NO`

`RELEASE 1.12 WP04 — SCHEMA MIGRATION REQUIRED: NO`

`RELEASE 1.12 WP04 — TERRA DOCKER-REMEDIATION AUTHORITY: READY`

------------------------------------------------------------------------

## 2. Exact authorized path set

Terra is authorized to mutate exactly these ten paths:

``` text
MODIFY src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs
MODIFY src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite.ps1
MODIFY container/entrypoint.sh
CREATE src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqlitePersistenceDiagnostics.cs
CREATE src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
CREATE tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceDiagnosticsTests.cs
```

No other repository path may be modified.

Required before mutation:

`RELEASE 1.12 WP04 — TEN-PATH ALLOWLIST VERIFIED: PASS`

If any additional path is required, STOP for Luna re-governance.

------------------------------------------------------------------------

## 3. Entry-point remediation --- E1

Modify only:

`container/entrypoint.sh`

Required behavior:

-   remove unconditional SQLite parent-directory creation;
-   retain only visualization/runtime directory preparation that is
    still required;
-   retain process supervision/startup behavior;
-   do not create the directory derived from
    `Persistence__DatabasePath`;
-   do not create or mutate SQLite directly;
-   do not add direct SQL;
-   do not weaken non-root runtime behavior;
-   do not introduce Azure-specific hard-coded application logic.

The explicit application-owned SQLite initialization capability must
become the sole authority for missing-parent creation.

Required markers:

`RELEASE 1.12 WP04 — ENTRYPOINT SQLITE-PARENT CREATION REMOVED: PASS`

`RELEASE 1.12 WP04 — APPLICATION-OWNED INITIALIZATION SOLE AUTHORITY: PASS`

`RELEASE 1.12 WP04 — ENTRYPOINT PROCESS SUPERVISION PRESERVED: PASS`

------------------------------------------------------------------------

## 4. Application-owned persistence diagnostics --- D3

Create:

`src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqlitePersistenceDiagnostics.cs`

The capability must be narrowly scoped to WP04 qualification.

Requirements:

-   bounded read-only diagnostics for SQLite metadata;
-   use existing Infrastructure persistence mechanics;
-   expose no arbitrary SQL surface;
-   no shell/Python ownership;
-   no direct domain-row writes;
-   no schema migration;
-   no provider behavior;
-   no secret handling;
-   no generic debug API.

It may inspect only the bounded metadata required for qualification:

-   database path identity without secrets;
-   schema version;
-   journal mode;
-   integrity result;
-   quick-check result;
-   accepted evidence identity/count or equivalent bounded continuity
    evidence through approved abstractions.

If any information must be retrieved from application persistence state,
use existing application/persistence abstractions rather than bypassing
ownership.

Required markers:

`RELEASE 1.12 WP04 — BOUNDED SQLITE DIAGNOSTICS: IMPLEMENTED`

`RELEASE 1.12 WP04 — ARBITRARY SQL SURFACE: ABSENT`

`RELEASE 1.12 WP04 — DIRECT-SQL BYPASS: ABSENT`

------------------------------------------------------------------------

## 5. Worker qualification execution

Create:

`src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs`

The qualification execution must:

-   be explicit and bounded to persistence qualification;
-   support two phases:
    -   `initialize`
    -   `reopen`
-   use normal application-owned persistence APIs for accepted/synthetic
    evidence;
-   never write domain rows through raw SQL;
-   retrieve accepted evidence through existing application/persistence
    abstractions;
-   invoke bounded diagnostics only for metadata/integrity proof;
-   produce a versioned qualification record;
-   be deterministic and suitable for local Docker evidence;
-   use no real provider;
-   use no provider secret;
-   make no Azure API calls;
-   not become a general runtime/debug command.

The versioned qualification record must contain at least:

-   record/schema version for the qualification output;
-   qualification phase (`initialize` or `reopen`);
-   database path identity without secrets;
-   SQLite schema version;
-   SQLite journal mode;
-   accepted evidence identity;
-   accepted evidence count or equivalent deterministic continuity
    count;
-   integrity result;
-   quick-check result;
-   persistence continuity result.

The output format may be JSON if convenient, but the old Docker JSON
shape is not required.

Required:

`RELEASE 1.12 WP04 — APPLICATION-OWNED QUALIFICATION COMMAND: IMPLEMENTED`

`RELEASE 1.12 WP04 — QUALIFICATION RECORD VERSIONED: PASS`

`RELEASE 1.12 WP04 — QUALIFICATION WRITE PATH GOVERNED: PASS`

`RELEASE 1.12 WP04 — QUALIFICATION READ PATH GOVERNED: PASS`

------------------------------------------------------------------------

## 6. Worker composition integration

Modify only:

`src/AIQuantTradingResearch.Worker/Program.cs`

Requirements:

-   wire the qualification execution through the Worker composition
    root;
-   preserve normal Worker execution;
-   qualification mode must be explicit and bounded;
-   no default behavior change for normal runtime;
-   no provider access in qualification mode;
-   no Azure-specific hard-coded path;
-   preserve existing configuration ownership;
-   preserve the explicit SQLite parent-initialization flag default of
    false outside explicit deployment configuration.

Required:

`RELEASE 1.12 WP04 — QUALIFICATION MODE EXPLICIT: PASS`

`RELEASE 1.12 WP04 — NORMAL WORKER MODE PRESERVED: PASS`

------------------------------------------------------------------------

## 7. Existing Option C paths

Preserve and, only where needed, refine the already-authorized
implementation in:

``` text
src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs
src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite.ps1
```

Invariants:

-   explicit storage-initialization flag remains default `false`;
-   parent creation only when explicitly enabled;
-   `PRAGMA journal_mode = DELETE`;
-   schema version remains 4;
-   unavailable-path no-creation contract remains intact;
-   scripts configure/read back `/home/data/aiquant.db`;
-   scripts enable the explicit initialization capability;
-   scripts do not execute domain SQL;
-   scripts do not handle provider credentials.

Required:

`RELEASE 1.12 WP04 — OPTION C CONTRACT PRESERVED: PASS`

------------------------------------------------------------------------

## 8. Diagnostic tests

Create:

`tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceDiagnosticsTests.cs`

Required coverage:

-   schema version reporting;
-   DELETE journal mode reporting;
-   integrity check reporting;
-   quick-check reporting;
-   bounded behavior;
-   no schema mutation;
-   no arbitrary SQL surface;
-   deterministic diagnostics against an initialized test database.

Also preserve:

`tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentDiscoveryTests.cs`

unchanged.

Required:

`RELEASE 1.12 WP04 — DIAGNOSTICS TEST COVERAGE: PASS`

`RELEASE 1.12 WP04 — DISCOVERY REGRESSION CONTRACT UNCHANGED: PASS`

------------------------------------------------------------------------

## 9. Full local validation

After implementation, rerun fresh validation from zero.

At minimum:

1.  `dotnet build`
2.  Domain tests
3.  Application tests
4.  Architecture tests
5.  Infrastructure tests
6.  preserved unavailable-path regression test
7.  targeted SQLite persistence tests
8.  diagnostics tests
9.  PowerShell parse/static validation for both WP04 scripts
10. shell syntax validation for `container/entrypoint.sh`
11. Gitleaks across all ten authorized paths
12. `git diff --check`
13. exact ten-path payload audit

Report exact fresh counts.

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

`RELEASE 1.12 WP04 — TEN-PATH PAYLOAD AUDIT: PASS`

If any gate fails, STOP before Docker.

------------------------------------------------------------------------

## 10. Interactive Docker validation handoff

Only after all local validation passes, prepare an exact
requester-executed PowerShell block for interactive user `sabsf`.

Classification:

-   local Docker build/run/stop/remove only;
-   no Azure;
-   no GHCR;
-   no Twelve Data;
-   no GitHub;
-   no repository mutation;
-   no direct SQLite inspection from shell/Python.

Expected Docker mutations:

-   1 temporary image;
-   1 named persistent volume;
-   2 sequential containers;
-   all removed during cleanup.

### First container --- initialize

The first container must:

-   use the named volume mounted to the governed persistence location;
-   explicitly configure
    `Persistence__DatabasePath=/home/data/aiquant.db`;
-   explicitly enable the application-owned parent initialization flag;
-   run the bounded qualification command in `initialize` phase;
-   use synthetic/offline deterministic evidence only;
-   emit the versioned application-owned qualification record.

### Second container --- reopen

After stopping/removing the first container, the second container must:

-   use the same named volume;
-   run qualification in `reopen` phase;
-   retrieve the previously accepted evidence through application-owned
    abstractions;
-   emit a qualification record proving continuity.

### Required semantic Docker evidence

Across the two records prove:

-   schema version = 4;
-   journal mode = `delete`;
-   integrity result = healthy/ok;
-   quick-check result = healthy/ok;
-   accepted evidence identity is stable/expected;
-   accepted evidence count or deterministic continuity evidence is
    preserved;
-   persistence continuity result passes;
-   no provider access occurred;
-   no direct SQL shell/Python inspection occurred.

Cleanup must prove:

`WP04_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=False`

`WP04_LOCAL_VOLUME_PRESENT_AFTER_CLEANUP=False`

`WP04_LOCAL_CLEANUP_COMPLETE=True`

A final semantic marker should be printed by the interactive script:

`WP04_LOCAL_PERSISTENCE_RECREATE_PASS=True`

Terra must provide the exact PowerShell block and then STOP.

Do not infer Docker success.

Required handoff marker:

`RELEASE 1.12 WP04 — INTERACTIVE DOCKER HANDOFF: READY`

------------------------------------------------------------------------

## 11. Repository/GitHub boundary

This authority does **not** authorize:

-   staging;
-   commit;
-   push;
-   PR creation;
-   PR merge;
-   Azure mutation;
-   GHCR mutation;
-   provider mutation;
-   issue closure;
-   Project #2 mutation;
-   milestone mutation;
-   WP05 execution.

After the interactive Docker evidence returns and is reconciled, a
separate Terra continuation authority will govern commit/PR/Azure
validation/lifecycle completion.

Required:

`RELEASE 1.12 WP04 — REPOSITORY PUBLICATION BOUNDARY PRESERVED: PASS`

------------------------------------------------------------------------

## 12. Mutation accounting

Report exact actual mutations.

Expected repository mutation set:

-   6 existing authorized files modified/created as already governed;
-   plus:
    -   1 modified entrypoint;
    -   2 created production files;
    -   1 created test file;
-   total governed payload: 10 paths.

Expected non-repository mutation state before requester Docker
execution:

-   commits: 0
-   pushes: 0
-   PRs: 0
-   Azure: 0
-   GHCR: 0
-   provider: 0
-   GitHub lifecycle: 0
-   schema migrations: 0

Required:

`RELEASE 1.12 WP04 — DOCKER-REMEDIATION MUTATION AUDIT: PASS`

------------------------------------------------------------------------

## 13. Required output

If implementation and all local gates pass:

`RELEASE 1.12 WP04 — E1 ENTRYPOINT REMEDIATION: PASS`

`RELEASE 1.12 WP04 — D3 APPLICATION-OWNED QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — FULL LOCAL VALIDATION: PASS`

`RELEASE 1.12 WP04 — INTERACTIVE DOCKER HANDOFF: READY`

Then print the exact PowerShell block and STOP.

If any required gate fails:

`RELEASE 1.12 WP04 — DOCKER REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

Terminal on successful handoff preparation:

`RELEASE 1.12 WP04 — TERRA DOCKER REMEDIATION EXECUTION COMPLETE`

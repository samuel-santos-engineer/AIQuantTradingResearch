# GPT-5.6 Terra --- Release 1.12 WP04 Non-Root Docker Remediation Execution Authority

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

-   **GPT-5.6 Luna** --- contract, policy, architecture, reconciliation,
    acceptance criteria, governance, read-only planning.
-   **GPT-5.6 Terra** --- PRIMARY for this prompt: implementation,
    validation execution, approved local Docker mutations, and exact
    mutation accounting.
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

Fresh Luna reconciliation completed:

`RELEASE 1.12 WP04 — NON-ROOT STORAGE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — ROOT-OWNED VOLUME CONSTRAINT: CONFIRMED`

`RELEASE 1.12 WP04 — SELECTED STORAGE PREPARATION OPTION: E2-A`

`RELEASE 1.12 WP04 — FILESYSTEM PREPARATION / SQLITE INITIALIZATION BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — NON-ROOT LONG-RUNNING RUNTIME: PRESERVED`

`RELEASE 1.12 WP04 — WORLD-WRITABLE STORAGE: ABSENT`

`RELEASE 1.12 WP04 — BOUNDED OWNERSHIP PREPARATION: PASS`

`RELEASE 1.12 WP04 — LOCAL/AZURE STORAGE PREPARATION CONTRACT: RECONCILED`

`RELEASE 1.12 WP04 — EXISTING DOCKER ARTIFACT DISPOSITION: CLEAN_RECREATE`

`RELEASE 1.12 WP04 — DOCKERFILE MUTATION REQUIRED: YES`

`RELEASE 1.12 WP04 — ENTRYPOINT MUTATION REQUIRED: YES`

`RELEASE 1.12 WP04 — ADDITIONAL TEST PATH REQUIRED: NO`

`RELEASE 1.12 WP04 — WP04 SCRIPT MUTATION REQUIRED: NO`

`RELEASE 1.12 WP04 — README MUTATION REQUIRED: NO`

`RELEASE 1.12 WP04 — SCHEMA MIGRATION REQUIRED: NO`

`RELEASE 1.12 WP04 — TERRA NON-ROOT REMEDIATION AUTHORITY: READY`

Observed permission constraint:

``` text
uid=1000(aiq) gid=1000(aiq)
/home/data = root:root, drwxr-xr-x
DATA_WRITABLE=False
WP04_PERMISSION_RUN_EXIT_CODE=0
WP04_PERMISSION_REMOVE_EXIT_CODE=0
```

The previous temporary diagnostic container was removed.

The pre-remediation WP04 image and named volume remain and must be
**cleanly removed/recreated**, not reused for final qualification.

------------------------------------------------------------------------

## 2. Exact authorized path set

Terra is authorized to mutate exactly these eleven repository paths:

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

Declared path count:

-   11 total
-   5 MODIFY
-   6 CREATE

No other repository path may be changed.

Required before mutation:

`RELEASE 1.12 WP04 — ELEVEN-PATH ALLOWLIST VERIFIED: PASS`

If any additional path is required, STOP for Luna re-governance.

------------------------------------------------------------------------

## 3. E2-A design contract

Implement:

**Root startup preparation, then permanent privilege drop**

The container startup sequence must:

1.  begin with sufficient startup privilege to prepare only the
    configured persistence parent;
2.  determine the parent directory from the configured
    `Persistence__DatabasePath`;
3.  create that parent directory if absent;
4.  set ownership specifically to the application runtime identity:
    -   UID `1000`
    -   GID `1000`
    -   user/group `aiq`
5.  set bounded permissions:
    -   mode `0750` for the persistence parent;
6.  avoid recursive broad-volume ownership changes unless the exact
    configured parent itself requires it and the operation remains
    narrowly bounded;
7.  permanently drop to `aiq`;
8.  launch all long-running Worker/Streamlit/runtime processes as
    non-root.

The entrypoint must NOT:

-   create the SQLite database file;
-   execute SQLite;
-   execute SQL;
-   bootstrap schema;
-   enforce journal mode;
-   insert domain rows;
-   inspect domain rows;
-   decide semantic persistence initialization;
-   run provider calls;
-   expose arbitrary commands.

Required markers:

`RELEASE 1.12 WP04 — ROOT STARTUP PREPARATION: PASS`

`RELEASE 1.12 WP04 — PERSISTENCE PARENT MODE 0750: PASS`

`RELEASE 1.12 WP04 — PERSISTENCE PARENT OWNER AIQ: PASS`

`RELEASE 1.12 WP04 — SQLITE SEMANTIC INITIALIZATION IN ENTRYPOINT: ABSENT`

`RELEASE 1.12 WP04 — PERMANENT PRIVILEGE DROP: PASS`

`RELEASE 1.12 WP04 — LONG-RUNNING ROOT PROCESS: ABSENT`

------------------------------------------------------------------------

## 4. Dockerfile contract

Modify only:

`Dockerfile`

The Dockerfile must support controlled root startup followed by
permanent drop to `aiq`.

Requirements:

-   preserve existing application/runtime dependencies;
-   preserve the `aiq` account as the long-running identity;
-   provide a narrowly controlled privilege-drop utility such as `gosu`,
    or an equivalent mechanism justified by repository/runtime
    constraints;
-   pin/install the privilege-drop mechanism in a deterministic and
    appropriately validated way;
-   do not add unnecessary package surface;
-   do not leave the final application processes running as root;
-   preserve container health/runtime behavior;
-   preserve custom Docker deployment compatibility with Azure App
    Service Linux F1;
-   do not add provider credentials or secrets.

If `gosu` is selected, validate that the installed binary/package is
appropriate for the base image architecture and comes from a governed
package source already compatible with repository practices.

Required:

`RELEASE 1.12 WP04 — PRIVILEGE-DROP UTILITY: VALIDATED`

`RELEASE 1.12 WP04 — DOCKERFILE NON-ROOT RUNTIME CONTRACT: PASS`

------------------------------------------------------------------------

## 5. Entrypoint contract

Modify only:

`container/entrypoint.sh`

Required behavior:

### Startup as root

If effective UID is root:

-   read the configured SQLite database path;
-   derive only its parent;
-   prepare exactly that parent;
-   ensure ownership `aiq:aiq`;
-   ensure mode `0750`;
-   retain required visualization-directory preparation;
-   then exec/relaunch the runtime under `aiq`.

### Startup as non-root

If the entrypoint is invoked already as `aiq`:

-   do not attempt privileged ownership changes;
-   verify required paths are writable where appropriate;
-   fail explicitly if required persistence preparation was not already
    satisfied;
-   do not silently fall back to another persistence path.

### After drop

All application processes must execute as `aiq`.

Required:

`RELEASE 1.12 WP04 — ENTRYPOINT BOUNDED STORAGE PREPARATION: PASS`

`RELEASE 1.12 WP04 — NON-ROOT FALLBACK BYPASS: ABSENT`

`RELEASE 1.12 WP04 — VISUALIZATION DIRECTORY PREPARATION PRESERVED: PASS`

------------------------------------------------------------------------

## 6. Preserve Option C

The following earlier WP04 contract remains binding:

-   explicit SQLite initialization flag defaults to false;
-   `SqliteConnectionFactory` creates a missing parent only when
    explicitly enabled;
-   schema remains v4;
-   SQLite journal mode remains DELETE;
-   unavailable discovery/read paths remain non-creating by default;
-   no interface broadening;
-   no hidden fallback database path.

The root entrypoint preparation makes the filesystem writable.

It does **not** authorize SQLite semantic initialization.

The application remains the sole owner of:

-   database file creation;
-   schema bootstrap;
-   journal-mode enforcement;
-   governed writes.

Required:

`RELEASE 1.12 WP04 — OPTION C CONTRACT PRESERVED: PASS`

`RELEASE 1.12 WP04 — FILESYSTEM PREPARATION / SQLITE INITIALIZATION BOUNDARY: PASS`

------------------------------------------------------------------------

## 7. Preserve D3 application-owned qualification

The bounded qualification implementation remains governed by the prior
D3 decision.

Preserve/create only within the allowlist:

-   `SqlitePersistenceDiagnostics.cs`
-   `PersistentSqliteQualificationExecution.cs`
-   `SqlitePersistenceDiagnosticsTests.cs`

Requirements:

-   no arbitrary SQL surface;
-   no shell/Python SQLite ownership;
-   governed application abstractions for accepted evidence;
-   bounded read-only SQLite metadata diagnostics;
-   versioned qualification output;
-   phases `initialize` and `reopen`;
-   deterministic offline/synthetic evidence;
-   no provider access;
-   no Azure API access;
-   no secret handling.

Required:

`RELEASE 1.12 WP04 — D3 APPLICATION-OWNED QUALIFICATION PRESERVED: PASS`

------------------------------------------------------------------------

## 8. Scripts

The existing WP04 scripts remain in the authorized path set but Luna
determined no new script-scope expansion is required.

Do not broaden them beyond current governed behavior.

They must continue to:

-   configure/read back `/home/data/aiquant.db`;
-   explicitly enable the application initialization capability;
-   avoid domain SQL;
-   avoid provider credentials;
-   avoid schema migration;
-   avoid direct persistence writes.

Required:

`RELEASE 1.12 WP04 — WP04 SCRIPT BOUNDARY PRESERVED: PASS`

------------------------------------------------------------------------

## 9. Full local validation

After implementation, rerun all relevant local validation from zero.

At minimum:

1.  `dotnet build`
2.  Domain tests
3.  Application tests
4.  Architecture tests
5.  Infrastructure tests
6.  preserved unavailable-path regression test
7.  targeted SQLite persistence tests
8.  diagnostics tests
9.  PowerShell parsing/static validation for both WP04 scripts
10. shell syntax validation for `container/entrypoint.sh`
11. Dockerfile/static inspection sufficient to prove
    entrypoint/root-drop intent
12. Gitleaks across all 11 authorized paths
13. `git diff --check`
14. exact eleven-path payload audit

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

If any local gate fails, STOP before Docker.

------------------------------------------------------------------------

## 10. Existing Docker artifact cleanup

Only after local validation passes, the next Docker qualification must
begin by cleaning the failed pre-remediation artifacts.

The existing temporary WP04 image and named volume are not authoritative
final evidence.

Use exact identity checks before removal.

Required behavior:

1.  identify the existing failed-test WP04 image;
2.  identify the existing failed-test WP04 named volume;
3.  prove they are the intended temporary WP04 artifacts;
4.  remove them;
5.  confirm absence;
6.  build/create fresh artifacts for the remediation qualification.

Do not remove unrelated Docker resources.

Required:

`RELEASE 1.12 WP04 — PRE-REMEDIATION IMAGE CLEANED: PASS`

`RELEASE 1.12 WP04 — PRE-REMEDIATION VOLUME CLEANED: PASS`

`RELEASE 1.12 WP04 — CLEAN RECREATE BOUNDARY: PASS`

------------------------------------------------------------------------

## 11. Interactive Docker qualification

After local gates pass, prepare an exact PowerShell block for requester
execution under interactive Windows user `sabsf`.

Terra cannot infer Docker success.

The handoff must be copy/paste-ready and must STOP for returned
evidence.

### Classification

Authorized local Docker mutations:

-   remove the identified failed pre-remediation WP04 image;
-   remove the identified failed pre-remediation WP04 volume;
-   build 1 fresh remediation image;
-   create 1 fresh named persistence volume;
-   run 2 sequential qualification containers;
-   remove both containers;
-   remove fresh volume;
-   remove fresh image.

No Azure.

No GHCR.

No Twelve Data/provider.

No GitHub.

No repository mutation.

No direct shell/Python SQLite query.

### Fresh first container --- initialize

The first container must prove:

-   startup begins with required preparation privilege;
-   `/home/data` becomes owned by `aiq:aiq`;
-   mode is `0750`;
-   application processes run as `uid=1000/gid=1000`;
-   application-owned initialization creates/opens the SQLite database;
-   qualification `initialize` succeeds;
-   qualification record reports:
    -   schema version 4;
    -   journal mode delete;
    -   integrity ok;
    -   quick-check ok;
    -   deterministic accepted evidence identity/count;
    -   persistence continuity initialization success.

### Fresh second container --- reopen

After first container stop/removal, run a second container against the
same fresh named volume.

It must prove:

-   preparation remains bounded;
-   long-running runtime is non-root;
-   no duplicate/broken initialization;
-   qualification `reopen` retrieves the existing accepted evidence
    through governed abstractions;
-   same evidence identity/count or approved deterministic continuity
    proof;
-   schema/journal/integrity/quick-check remain valid;
-   continuity result passes.

### Cleanup

Remove both containers, the fresh volume, and the fresh image.

Required final script evidence:

`WP04_LOCAL_PERSISTENCE_RECREATE_PASS=True`

`WP04_LOCAL_NONROOT_RUNTIME_PASS=True`

`WP04_LOCAL_STORAGE_OWNER_PASS=True`

`WP04_LOCAL_STORAGE_MODE_PASS=True`

`WP04_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=False`

`WP04_LOCAL_VOLUME_PRESENT_AFTER_CLEANUP=False`

`WP04_LOCAL_CLEANUP_COMPLETE=True`

Required handoff marker:

`RELEASE 1.12 WP04 — INTERACTIVE DOCKER HANDOFF: READY`

Then STOP and wait for complete stdout/stderr + exit codes.

------------------------------------------------------------------------

## 12. Azure parity boundary

This authority does not authorize Azure mutation.

However, before declaring local remediation ready for Docker handoff,
inspect the implementation and report whether the same
root-preparation + privilege-drop flow remains compatible with the
governed Azure App Service Linux F1 custom-container deployment
contract.

Do not claim deployed proof.

Required:

`RELEASE 1.12 WP04 — AZURE COMPATIBILITY STATIC RECONCILIATION: PASS`

This means only that no repository-level contradiction was found.

It does not mean Azure execution has been validated.

------------------------------------------------------------------------

## 13. Repository and lifecycle boundary

This authority does **not** authorize:

-   staging;
-   commit;
-   push;
-   PR creation;
-   PR merge;
-   GitHub issue closure;
-   Project #2 mutation;
-   milestone #63 mutation;
-   Azure mutation;
-   GHCR mutation;
-   provider mutation;
-   WP05 execution.

After Docker evidence is returned and accepted, a separate Terra
continuation authority will govern candidate commit, PR, Azure
persistence validation, post-merge checks, and WP04 lifecycle
completion.

Required:

`RELEASE 1.12 WP04 — PUBLICATION/LIFECYCLE BOUNDARY PRESERVED: PASS`

------------------------------------------------------------------------

## 14. Mutation accounting

Report exact actual mutations.

Repository maximum under this authority:

-   11 governed paths total
-   5 modified
-   6 created
-   0 deletes

Before requester Docker execution:

-   commits: 0
-   pushes: 0
-   PRs: 0
-   GitHub issue mutations: 0
-   Project mutations: 0
-   milestone mutations: 0
-   Azure mutations: 0
-   GHCR mutations: 0
-   provider mutations: 0
-   schema migrations: 0

Docker mutation accounting must distinguish:

### Cleanup mutations

-   failed pre-remediation image removed: 1
-   failed pre-remediation volume removed: 1

### Fresh qualification mutations

-   fresh image created: 1
-   fresh volume created: 1
-   containers created: 2
-   containers removed: 2
-   fresh volume removed: 1
-   fresh image removed: 1

Count only actual successful mutations.

Required:

`RELEASE 1.12 WP04 — NON-ROOT REMEDIATION MUTATION AUDIT: PASS`

------------------------------------------------------------------------

## 15. Required output

If implementation and local gates pass:

`RELEASE 1.12 WP04 — E2-A NON-ROOT STORAGE REMEDIATION: PASS`

`RELEASE 1.12 WP04 — FULL LOCAL VALIDATION: PASS`

`RELEASE 1.12 WP04 — AZURE COMPATIBILITY STATIC RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — INTERACTIVE DOCKER HANDOFF: READY`

Then print the exact PowerShell handoff and STOP.

If any gate fails:

`RELEASE 1.12 WP04 — NON-ROOT REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

Terminal on successful handoff preparation:

`RELEASE 1.12 WP04 — TERRA NON-ROOT DOCKER REMEDIATION EXECUTION COMPLETE`

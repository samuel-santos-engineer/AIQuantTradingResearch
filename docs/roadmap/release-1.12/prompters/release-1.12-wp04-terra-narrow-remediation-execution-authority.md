# GPT-5.6 Terra --- Release 1.12 WP04 Narrow Remediation Execution Authority

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

-   **GPT-5.6 Luna** --- contract, policy, architecture, definition,
    reconciliation, acceptance criteria, governance, read-only planning.
-   **GPT-5.6 Terra** --- PRIMARY for this prompt: implementation,
    validation execution, approved Git/GitHub/Docker mutations, and
    preparation for subsequent Azure validation.
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

Branch:

`release/1.12-wp04-persistent-sqlite`

Luna reconciliation completed:

`RELEASE 1.12 WP04 — REGRESSION CONTRACT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — UNAVAILABLE-PATH NO-CREATION CONTRACT: PRESERVED`

`RELEASE 1.12 WP04 — PERSISTENT INITIALIZATION CONTRACT: PRESERVED`

`RELEASE 1.12 WP04 — SELECTED REMEDIATION OPTION: C`

Selected design:

> Add an explicit SQLite storage-initialization configuration flag,
> defaulting to false. The Worker composition root binds that flag;
> `SqliteConnectionFactory` creates a missing parent only when
> explicitly enabled. The existing discovery test remains unchanged and
> proves the default no-creation behavior.

------------------------------------------------------------------------

## 2. Exact authorized path set

Terra is authorized to mutate exactly these six paths:

``` text
MODIFY src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs
MODIFY src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite.ps1
```

No other path may be modified.

Required before edit:

`RELEASE 1.12 WP04 — SIX-PATH ALLOWLIST VERIFIED: PASS`

If any required implementation change falls outside this set, STOP for
re-governance.

------------------------------------------------------------------------

## 3. Explicit deny set

The following paths MUST NOT be modified under this authority:

``` text
README.md
src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/ISqliteConnectionFactory.cs
src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteSchemaBootstrapper.cs
tests/AIQuantTradingResearch.Infrastructure.Tests/ExperimentDiscoveryTests.cs
src/AIQuantTradingResearch.Domain/Experiments/Experiment.cs
src/AIQuantTradingResearch.Application/Experiments/ExperimentService.cs
eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/deploy-f1-reference.ps1
eng/azure-cli/r1.11-qualification/wp03-azure-cli-docker-sqlite/check-sqlite-01.ps1
```

The existing `ExperimentDiscoveryTests` behavior is a preserved
regression contract and MUST remain unchanged.

Required:

`RELEASE 1.12 WP04 — DENY SET PRESERVED: PASS`

------------------------------------------------------------------------

## 4. Implementation contract

Implement Option C exactly.

### 4.1 Configuration

In `SqliteStorageConfiguration.cs` add an explicit configuration
property representing whether missing parent-directory creation is
authorized for SQLite storage initialization.

Requirements:

-   default value is `false`;
-   name must clearly express storage-initialization/create-parent
    intent;
-   no ambiguous behavior such as inferring creation from database path
    alone;
-   no schema-version change;
-   no public interface change.

Required:

`RELEASE 1.12 WP04 — EXPLICIT INITIALIZATION FLAG: IMPLEMENTED`

### 4.2 Factory behavior

In `SqliteConnectionFactory.cs`:

-   default behavior MUST NOT create missing parent directories;
-   create the configured database parent only when the explicit
    initialization flag is enabled;
-   directory creation must be derived only from the configured SQLite
    database path;
-   preserve existing connection semantics;
-   preserve explicit `PRAGMA journal_mode = DELETE`;
-   journal-mode failure must remain explicit;
-   no direct domain-data writes;
-   no hidden fallback path;
-   no creation behavior for discovery/read callers using the default
    configuration.

Required:

`RELEASE 1.12 WP04 — DEFAULT NO-CREATION SEMANTICS: PASS`

`RELEASE 1.12 WP04 — EXPLICIT PARENT INITIALIZATION: PASS`

`RELEASE 1.12 WP04 — SQLITE DELETE JOURNAL MODE: PASS`

### 4.3 Worker composition

In `Worker/Program.cs` bind the explicit configuration flag through the
existing Worker configuration/composition boundary.

Requirements:

-   preserve Worker as outer composition owner;
-   no Azure-specific hard-coded path in application code;
-   configuration remains environment/appsettings driven;
-   no secret behavior;
-   no provider behavior;
-   no application/domain contract change.

Required:

`RELEASE 1.12 WP04 — WORKER CONFIGURATION BINDING: PASS`

### 4.4 Tests

In `SqlitePersistenceTests.cs`:

-   preserve existing tests;
-   add/adjust coverage proving the explicit initialization flag allows
    missing-parent creation;
-   prove schema version remains `4`;
-   prove `journal_mode=delete`;
-   prove persistence survives reopen;
-   prove accepted-history retrieval still succeeds;
-   prove default configuration does not silently create missing parents
    where appropriate.

Do NOT modify `ExperimentDiscoveryTests.cs`.

Required:

`RELEASE 1.12 WP04 — INITIALIZATION TEST COVERAGE: PASS`

`RELEASE 1.12 WP04 — DISCOVERY REGRESSION TEST UNCHANGED: PASS`

### 4.5 Release 1.12 scripts

For:

-   `configure-persistent-sqlite.ps1`
-   `verify-persistent-sqlite.ps1`

Requirements:

-   configure/read back `/home/data/aiquant.db`;
-   explicitly enable the new initialization capability through
    deployment configuration;
-   no provider secret behavior;
-   no domain SQL;
-   no schema mutation;
-   no direct SQLite application-data writes;
-   no WP03 or Initiative-1.11 historical artifact mutation.

Required:

`RELEASE 1.12 WP04 — DEPLOYMENT CONFIGURATION FLAG: PASS`

`RELEASE 1.12 WP04 — SCRIPT NO-DOMAIN-SQL BOUNDARY: PASS`

------------------------------------------------------------------------

## 5. Preserve existing contracts

The remediation MUST preserve:

-   unavailable dependency =\> failure rather than silent creation;
-   no fallback persistence;
-   schema v4;
-   Infrastructure SQLite ownership;
-   Application/Domain ownership boundaries;
-   Worker composition ownership;
-   no Python/Streamlit SQLite access;
-   no provider-secret scope expansion;
-   no README mutation;
-   no historical qualification mutation.

Required:

`RELEASE 1.12 WP04 — UNAVAILABLE-PATH CONTRACT PRESERVED: PASS`

`RELEASE 1.12 WP04 — NO PERSISTENCE BYPASS: PASS`

`RELEASE 1.12 WP04 — SCHEMA VERSION 4 PRESERVED: PASS`

`RELEASE 1.12 WP04 — ARCHITECTURE PRESERVATION: PASS`

------------------------------------------------------------------------

## 6. Local validation

After implementation, run fresh validation.

At minimum:

1.  `dotnet build`
2.  full Domain tests
3.  full Application tests
4.  full Architecture tests
5.  full Infrastructure tests
6.  targeted reproduction of:
    `ExperimentDiscoveryTests.DiscoverMapsUnavailableAndSchemaValidationFailuresWithoutCreationOrFallback`
7.  relevant targeted SQLite persistence tests
8.  `git diff --check`
9.  PowerShell syntax/static validation for both WP04 scripts
10. Gitleaks
11. exact six-path diff/status audit

Report fresh counts only.

Required:

`RELEASE 1.12 WP04 — BUILD VALIDATION: PASS`

`RELEASE 1.12 WP04 — DOMAIN TESTS: PASS`

`RELEASE 1.12 WP04 — APPLICATION TESTS: PASS`

`RELEASE 1.12 WP04 — ARCHITECTURE TESTS: PASS`

`RELEASE 1.12 WP04 — INFRASTRUCTURE TESTS: PASS`

`RELEASE 1.12 WP04 — REGRESSION TEST: PASS`

`RELEASE 1.12 WP04 — SQLITE TARGETED TESTS: PASS`

`RELEASE 1.12 WP04 — POWERSHELL VALIDATION: PASS`

`RELEASE 1.12 WP04 — GITLEAKS: PASS`

`RELEASE 1.12 WP04 — DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — SIX-PATH PAYLOAD AUDIT: PASS`

If any validation fails, STOP. Do not proceed to Docker.

------------------------------------------------------------------------

## 7. Interactive Docker handoff

Only if all local validation passes, prepare the previously approved
requester-run interactive `sabsf` PowerShell Docker validation.

Classification:

-   Docker build/run/stop/remove only;
-   no Azure;
-   no GHCR;
-   no provider;
-   no GitHub;
-   no repository mutation.

Expected Docker mutations:

-   1 image;
-   1 volume;
-   2 containers;
-   all removed by cleanup.

Terra must provide the exact PowerShell block, then STOP and wait for
returned stdout/stderr and exit codes.

Do not infer execution.

Expected acceptance includes both JSON integrity results with:

-   `integrityCheck = ok`
-   `journalMode = delete`
-   `schemaVersion = 4`
-   preserved `historicalObservationCount`

and:

`WP04_LOCAL_PERSISTENCE_RECREATE_PASS=True`

`WP04_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=False`

`WP04_LOCAL_VOLUME_PRESENT_AFTER_CLEANUP=False`

`WP04_LOCAL_CLEANUP_COMPLETE=True`

Required before stopping:

`RELEASE 1.12 WP04 — INTERACTIVE DOCKER HANDOFF: READY`

------------------------------------------------------------------------

## 8. Git boundary

This remediation authority does **not** yet authorize commit, push, PR
creation, merge, issue closure, Project mutation, milestone mutation,
Azure mutation, or GHCR mutation.

After successful local + Docker reconciliation, a subsequent Terra
continuation authority will govern candidate commit/PR/Azure validation.

Do not stage or commit during this prompt unless explicitly required for
a non-mutating local validation operation, which should normally not be
necessary.

Required:

`RELEASE 1.12 WP04 — REPOSITORY MUTATION BOUNDARY PRESERVED: PASS`

------------------------------------------------------------------------

## 9. Mutation accounting

Report exact actual mutations from this authority:

-   files modified;
-   files created;
-   files deleted;
-   Docker mutations, if requester later executes the handoff;
-   commits: expected `0`;
-   pushes: expected `0`;
-   PRs: expected `0`;
-   GitHub issue/Project/milestone mutations: expected `0`;
-   Azure mutations: expected `0`;
-   GHCR mutations: expected `0`;
-   provider mutations: expected `0`;
-   schema mutations: expected `0`.

Required:

`RELEASE 1.12 WP04 — NARROW REMEDIATION MUTATION AUDIT: PASS`

------------------------------------------------------------------------

## 10. Required output

After implementation and local validation, return concise evidence and
exact counts.

If local validation passes and Docker handoff is ready:

`RELEASE 1.12 WP04 — OPTION C REMEDIATION: PASS`

`RELEASE 1.12 WP04 — FULL LOCAL VALIDATION: PASS`

`RELEASE 1.12 WP04 — INTERACTIVE DOCKER HANDOFF: READY`

Then print the exact approved Docker PowerShell block and STOP.

If any required gate fails:

`RELEASE 1.12 WP04 — OPTION C REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

Terminal on successful handoff preparation:

`RELEASE 1.12 WP04 — TERRA NARROW REMEDIATION EXECUTION COMPLETE`

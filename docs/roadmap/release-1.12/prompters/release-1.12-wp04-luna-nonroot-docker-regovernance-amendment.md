# GPT-5.6 Luna --- Release 1.12 WP04 Non-Root Docker Persistence Re-Governance Amendment

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

-   **GPT-5.6 Luna** --- PRIMARY: contract, architecture, scope
    amendment, non-root runtime design, allowlist governance, acceptance
    criteria, read-only reconciliation.
-   **GPT-5.6 Terra** --- implementation and validation only after this
    amendment authorizes the revised boundary.
-   **GPT-5.6 Sol** --- supporting analysis/synthesis only; never
    silently replaces Luna or Terra.

## 1. Current governed state

Resume **Phase 4 --- Release 1.12 WP04: Persistent SQLite
Initialization, Data Update & Recovery**.

Issue: `#263`\
Canonical base: `40a9a236dae789864f35e64bb5c1afd358e7b3db`\
Branch: `release/1.12-wp04-persistent-sqlite`

Observed Docker permission evidence:

``` text
uid=1000(aiq) gid=1000(aiq)
/home/data = root:root, drwxr-xr-x
DATA_WRITABLE=False
WP04_PERMISSION_RUN_EXIT_CODE=0
WP04_PERMISSION_REMOVE_EXIT_CODE=0
```

Therefore the non-root application user cannot create
`/home/data/aiquant.db` on the named volume under the current E1
contract.

Current markers:

`RELEASE 1.12 WP04 — DOCKER VALIDATION: BLOCKED`

`RELEASE 1.12 WP04 — APPLICATION-OWNED INITIALIZATION: UNPROVEN`

`RELEASE 1.12 WP04 — PERSISTENCE RECREATE QUALIFICATION: BLOCKED`

The temporary diagnostic container was removed. The WP04 image and named
volume remain. No Azure, GHCR, provider, GitHub, staging, commit, PR,
issue, Project, or milestone mutation occurred.

## 2. Binding prior decisions

**Option C:** SQLite missing-parent creation is an explicit
application-owned initialization capability, default false. No generic
connection-factory filesystem creation. Discovery no-creation semantics
remain preserved.

**E1:** unconditional SQLite-parent creation was removed from
`container/entrypoint.sh`, but the root-owned volume proves pure E1 is
insufficient.

**D3:** Docker qualification must use an application-owned bounded
persistence qualification capability. No shell/Python direct SQLite
inspection and no arbitrary SQL surface.

## 3. Newly proven constraint

A fresh Docker named volume at `/home/data` is `root:root` with mode
`0755`, while the runtime user is `aiq` uid/gid 1000. The application
cannot create the database file unless the mounted filesystem is made
writable by the non-root runtime user.

This is a container-runtime ownership/preparation concern and must
remain distinct from application semantic SQLite initialization.

## 4. Luna mission

Perform a narrow, read-only re-governance amendment for non-root
persistent-storage preparation. Determine the smallest correct design
preserving:

1.  non-root long-running application execution;
2.  application-owned SQLite semantic initialization;
3.  no unconditional persistence-creation bypass;
4.  no direct SQL shell/Python bypass;
5.  Docker named-volume compatibility;
6.  Azure App Service Linux F1 `/home` compatibility;
7.  schema v4;
8.  SQLite DELETE journal mode;
9.  existing D3 qualification boundary;
10. least-privilege filesystem preparation.

Do not mutate files or run Docker.

## 5. Mandatory inspection

Read and reconcile:

-   `Dockerfile`
-   `container/entrypoint.sh`
-   current Worker composition/qualification implementation
-   `SqliteStorageConfiguration.cs`
-   `SqliteConnectionFactory.cs`
-   current WP04 scripts
-   relevant WP02 container/runtime design
-   relevant WP03 Azure deployment automation
-   Initiative-1.11 persistence qualification evidence
-   Release 1.12 definition/execution plan/file manifest
-   issue `#263`

Trace Docker image user transitions, entrypoint effective user, mount
ownership, whether privilege-drop tooling exists, whether image is
permanently `USER aiq`, Azure `/home` expectations from repository
evidence, and whether a safe startup-preparation boundary already
exists.

Repository evidence controls. Material state mismatch =\> STOP.

## 6. Options to evaluate

### E2-A --- Root startup preparation, then privilege drop

Start entrypoint with sufficient startup privilege to prepare only the
configured persistence parent and set bounded ownership/permissions for
`aiq`, then permanently drop privileges before Worker/Streamlit
processes launch.

Evaluate current Dockerfile compatibility, tooling, security, exact
required paths, Azure compatibility, and whether filesystem preparation
remains separate from SQLite semantic initialization.

### E2-B --- Writable mount arrangement

Use a mount layout where the application-visible persistence directory
is writable by `aiq` without privileged startup. Do not assume image
ownership survives a fresh named-volume mount; prove it.

Evaluate Docker/Azure parity.

### E2-C --- Bounded entrypoint ownership preparation only

Permit `container/entrypoint.sh` to prepare ownership/permissions only.
It MUST NOT create the SQLite database, execute SQL, initialize schema,
insert domain rows, or decide semantic persistence initialization.

The application flag remains the sole SQLite initialization authority.

### E2-D --- Alternative non-root-compatible design

Consider another design only if smaller/safer while preserving the same
boundary.

## 7. Required architectural distinction

**Filesystem/runtime preparation** may narrowly establish directory
ownership/permissions necessary for the non-root process.

**Application persistence initialization** owns database creation,
schema bootstrap, journal-mode enforcement, and governed writes.

Required:

`RELEASE 1.12 WP04 — FILESYSTEM PREPARATION / SQLITE INITIALIZATION BOUNDARY: PRESERVED`

## 8. Security requirements

Selected design must preserve:

-   non-root long-running processes;
-   no `0777` workaround unless explicitly proven unavoidable;
-   no broad recursive `chown` outside configured persistence scope;
-   no Docker socket interaction;
-   no privilege escalation after drop;
-   no secret/provider changes;
-   no arbitrary command surface.

Required:

`RELEASE 1.12 WP04 — NON-ROOT LONG-RUNNING RUNTIME: PRESERVED`

`RELEASE 1.12 WP04 — WORLD-WRITABLE STORAGE: ABSENT`

`RELEASE 1.12 WP04 — BOUNDED OWNERSHIP PREPARATION: PASS`

## 9. Docker/Azure parity

Reconcile the selected design against both:

-   local Docker fresh named volume;
-   Azure App Service Linux F1 persistent `/home`.

Determine whether the same preparation is valid on Azure, whether
required startup privileges are available, and whether any operational
difference belongs in WP04 deployment configuration rather than
application code.

Required:

`RELEASE 1.12 WP04 — LOCAL/AZURE STORAGE PREPARATION CONTRACT: RECONCILED`

## 10. Existing Docker artifact disposition

Existing temporary artifacts remain: - one WP04 image; - one WP04
persistence volume.

Choose:

`RELEASE 1.12 WP04 — EXISTING DOCKER ARTIFACT DISPOSITION: <REUSE|CLEAN_RECREATE>`

## 11. Revised exact allowlist

Current governed set:

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

Determine whether the selected design additionally requires
`Dockerfile`, another exact runtime script, another exact test path, or
an exact WP04 deployment script.

No wildcards. No README mutation. No Initiative-1.11 historical
mutation. No WP03 mutation unless strictly proven necessary.

## 12. Forbidden actions

Do not edit files, run Docker, remove existing image/volume, stage,
commit, push, create/merge PRs, use Azure/GHCR/providers, mutate
secrets, close #263, change Project #2/milestone #63, start WP05, or use
a world-writable workaround merely to make Docker pass.

## 13. Required output

Return concise evidence and:

`RELEASE 1.12 WP04 — NON-ROOT STORAGE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — ROOT-OWNED VOLUME CONSTRAINT: CONFIRMED`

`RELEASE 1.12 WP04 — SELECTED STORAGE PREPARATION OPTION: <E2-A|E2-B|E2-C|E2-D>`

`RELEASE 1.12 WP04 — FILESYSTEM PREPARATION / SQLITE INITIALIZATION BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — NON-ROOT LONG-RUNNING RUNTIME: PRESERVED`

`RELEASE 1.12 WP04 — WORLD-WRITABLE STORAGE: ABSENT`

`RELEASE 1.12 WP04 — BOUNDED OWNERSHIP PREPARATION: PASS`

`RELEASE 1.12 WP04 — LOCAL/AZURE STORAGE PREPARATION CONTRACT: RECONCILED`

`RELEASE 1.12 WP04 — EXISTING DOCKER ARTIFACT DISPOSITION: <REUSE|CLEAN_RECREATE>`

Then:

``` text
RELEASE 1.12 WP04 — REVISED EXACT PATH ALLOWLIST:
<CLASSIFICATION> <exact path>
...
```

Then:

`RELEASE 1.12 WP04 — DOCKERFILE MUTATION REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — ENTRYPOINT MUTATION REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — ADDITIONAL TEST PATH REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — WP04 SCRIPT MUTATION REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — README MUTATION REQUIRED: NO`

`RELEASE 1.12 WP04 — SCHEMA MIGRATION REQUIRED: NO`

If governed:

`RELEASE 1.12 WP04 — TERRA NON-ROOT REMEDIATION AUTHORITY: READY`

Otherwise:

`RELEASE 1.12 WP04 — TERRA NON-ROOT REMEDIATION AUTHORITY: BLOCKED`

## 14. Stop boundary

After reconciliation and revised allowlist: **STOP**.

Do not implement. The result will be reviewed and used to create a
separate GPT-5.6 Terra WP04 Non-Root Docker Remediation Execution
Authority.

Terminal:

`RELEASE 1.12 WP04 — LUNA NON-ROOT DOCKER RE-GOVERNANCE AMENDMENT COMPLETE`

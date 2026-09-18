# GPT-5.6 Luna --- Release 1.12 WP04 Narrow Remediation / Allowlist Re-Governance

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

-   **GPT-5.6 Luna** --- PRIMARY: contract, policy, architecture,
    reconciliation, acceptance criteria, governance, read-only planning.
-   **GPT-5.6 Terra** --- implementation/validation and approved
    mutations only after this reconciliation.
-   **GPT-5.6 Sol** --- supporting analysis only; never silently
    replaces Luna/Terra.

## 1. Current governed state

Resume **Phase 4 --- Release 1.12 WP04: Persistent SQLite
Initialization, Data Update & Recovery**.

Issue: `#263`\
Canonical base: `40a9a236dae789864f35e64bb5c1afd358e7b3db`\
Branch: `release/1.12-wp04-persistent-sqlite`

Established re-entry:
`RELEASE 1.12 WP04 — RE-ENTRY RECONCILIATION: PASS`
`RELEASE 1.12 WP04 — CANONICAL MAIN RESOLVED: 40a9a236dae789864f35e64bb5c1afd358e7b3db`
`RELEASE 1.12 WP04 — LOCAL/REMOTE MAIN: 0/0`
`RELEASE 1.12 WP04 — EXISTING IMPLEMENTATION STATE: LOCAL`

Preserved local implementation:

``` text
MODIFY src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite.ps1
```

No README or unrelated path is changed.

## 2. Validation blocker

Already passed:
`RELEASE 1.12 WP04 — FOUR-PATH IMPLEMENTATION RECONCILIATION: PASS`
`RELEASE 1.12 WP04 — ARCHITECTURE PRESERVATION: PASS`
`RELEASE 1.12 WP04 — NO PERSISTENCE BYPASS: PASS`
`RELEASE 1.12 WP04 — SCHEMA MIGRATION: ABSENT`
`RELEASE 1.12 WP04 — SECRET-AUTOMATION SCOPE: ABSENT`

Fresh build: **0 warnings / 0 errors**.

Passing: - Domain 11/11 - Application 136/136 - Architecture 27/27

Infrastructure failed:

``` text
ExperimentDiscoveryTests.DiscoverMapsUnavailableAndSchemaValidationFailuresWithoutCreationOrFallback
Expected: DependencyUnavailable
Actual: null
WP04_REGRESSION_TEST_EXIT_CODE=1
```

Observed cause: generic missing-parent creation in
`SqliteConnectionFactory` makes an intentionally unavailable nested
SQLite path usable, violating the existing no-creation/no-fallback
discovery contract.

Therefore: `RELEASE 1.12 WP04 — BUILD VALIDATION: PASS`
`RELEASE 1.12 WP04 — DOTNET TESTS: FAIL`
`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

Docker was correctly not run. Gitleaks/downstream validation are not
claimed.

## 3. Governing principle

Do **not** assume `ExperimentDiscoveryTests.cs` should change. The
failing test is an established behavioral contract and is presumed
correct until repository evidence proves otherwise.

> Creating the parent directory is an initialization capability, not an
> unconditional side effect of obtaining a SQLite connection.

WP04 must allow legitimate writable `/home/data/aiquant.db`
initialization while an intentionally unavailable discovery/read
dependency remains unavailable.

## 4. Luna mission

Perform a **narrow read-only contract reconciliation**. Do not mutate
repository files.

Determine the smallest correct remediation preserving: 1. Release 1.12
persistent SQLite initialization. 2. Existing
unavailable-path/no-creation/no-fallback semantics. 3. Persistence
ownership. 4. SQLite schema v4. 5. Existing deterministic
failure/evidence semantics. 6. No Azure-specific application hack. 7. No
direct-SQL deployment bypass. 8. No PowerShell-owned domain
initialization. 9. No provider-secret scope expansion. 10. Narrowest
possible WP04 allowlist.

## 5. Mandatory inspection

Read/trace: -
`src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs` -
`tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs` -
exact `ExperimentDiscoveryTests.cs` containing the failure - production
code exercised by the failing test - SQLite connection
construction/composition - `Persistence:DatabasePath` handling -
existing initialization/bootstrap abstractions -
`docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md` -
`docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md` -
`docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md` - GitHub issue
`#263` - relevant Initiative-1.11 persistence qualification evidence -
relevant Release 1.12 WP03 deployment composition.

Repository evidence controls. Material state discrepancy =\> STOP.

## 6. Required contract trace

Determine the architectural distinction between:

**A. Writable initialization path** --- application-owned persistence
initialization is authorized to establish required filesystem/database
state.

**B. Dependency/discovery/read path** --- probing/opening must not
create missing storage or convert an unavailable dependency into
success.

Trace callers, interfaces, factory usage, initialization order,
read/write intent, failure mapping, directory/database creation, schema
initialization, and discovery behavior.

A request for a SQLite connection is not automatically authority to
create filesystem state.

## 7. Remediation options

Evaluate at minimum:

**Option A --- Restrict creation in existing factory.** Determine
whether existing intent/contracts can distinguish authorized
initialization from discovery/read access.

**Option B --- Move parent creation to an existing
initialization/bootstrap boundary.** Determine whether an existing
runtime/application boundary should establish the configured database
parent before writable store initialization.

**Option C --- Explicit initialization/create contract.** Use a new
explicit option/method/distinct factory contract only if architecture
genuinely requires it.

**Option D --- Change discovery test/contract.** Least preferred. Select
only if repository/release evidence proves the prior no-creation
behavior obsolete/incorrect. Never select merely to make tests green.

For each viable option report semantic correctness, affected production
paths, exact required files, contract changes, regression risk,
schema-v4 compatibility, Azure `/home/data/aiquant.db` compatibility,
preservation of unavailable-path behavior, initialization success, and
required tests.

Select exactly one preferred remediation. Prefer the smallest production
correction preserving both contracts.

## 8. Exact allowlist re-governance

Produce revised exact WP04 allowlist. Classify every path: `CREATE` /
`MODIFY` / `DELETE` / `NOT_REQUIRED`

No wildcards.

If original four paths suffice, state no expansion. If additional paths
are genuinely required, identify and justify each exact path.

Do not add `ExperimentDiscoveryTests.cs` merely because its test fails.

No repository mutation is authorized.

## 9. README policy

Standing Front-Door README Information Preservation Policy remains
active: `EXISTING README INFORMATION → PRESERVE`
`PRESERVE INFORMATION; UPDATE STATUS`
`SUMMARY ≠ AUTHORITY TO DELETE DETAIL`

No README mutation is authorized.

## 10. Forbidden actions

Do not edit/stage/commit/push; create/modify/merge PRs; run Docker
mutation validation; mutate Azure/GHCR/provider/secrets; close #263;
change Project #2; change milestone #63; start WP05; broaden WP05--WP08
scope; or weaken tests solely for green status.

Read-only inspection and narrowly necessary reproduction are allowed.

## 11. Required output

Return concise evidence and:

`RELEASE 1.12 WP04 — REGRESSION CONTRACT RECONCILIATION: PASS`
`RELEASE 1.12 WP04 — UNAVAILABLE-PATH NO-CREATION CONTRACT: PRESERVED`
`RELEASE 1.12 WP04 — PERSISTENT INITIALIZATION CONTRACT: PRESERVED`
`RELEASE 1.12 WP04 — SELECTED REMEDIATION OPTION: <A|B|C|D>`
`RELEASE 1.12 WP04 — REMEDIATION DESIGN: <description>`

Then:

``` text
RELEASE 1.12 WP04 — REVISED EXACT PATH ALLOWLIST:
<CLASSIFICATION> <exact path>
...
```

Then: `RELEASE 1.12 WP04 — ALLOWLIST EXPANSION REQUIRED: <YES|NO>`
`RELEASE 1.12 WP04 — TEST CONTRACT CHANGE REQUIRED: <YES|NO>`
`RELEASE 1.12 WP04 — SCHEMA MIGRATION REQUIRED: NO`
`RELEASE 1.12 WP04 — README MUTATION REQUIRED: NO`

If governed: `RELEASE 1.12 WP04 — TERRA REMEDIATION AUTHORITY: READY`

Otherwise: `RELEASE 1.12 WP04 — TERRA REMEDIATION AUTHORITY: BLOCKED`

## 12. Stop boundary

After reconciliation, revised allowlist, and authority decision:
**STOP**.

Do not implement. The result will be reviewed and used for a separate
GPT-5.6 Terra narrow remediation execution authority.

Terminal:
`RELEASE 1.12 WP04 — LUNA NARROW REMEDIATION RE-GOVERNANCE COMPLETE`

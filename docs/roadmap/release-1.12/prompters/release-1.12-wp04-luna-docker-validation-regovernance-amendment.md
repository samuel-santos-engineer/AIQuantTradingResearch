# GPT-5.6 Luna --- Release 1.12 WP04 Docker Validation Re-Governance Amendment

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

-   **GPT-5.6 Luna** --- PRIMARY: contract, architecture, scope
    amendment, allowlist governance, acceptance criteria, read-only
    reconciliation.
-   **GPT-5.6 Terra** --- implementation and validation only after this
    amendment authorizes the revised boundary.
-   **GPT-5.6 Sol** --- supporting analysis/synthesis only; never
    silently replaces Luna or Terra.

------------------------------------------------------------------------

## 1. Current governed state

Resume:

**Phase 4 --- Release 1.12 WP04: Persistent SQLite Initialization, Data
Update & Recovery**

Issue:

`#263`

Canonical base:

`40a9a236dae789864f35e64bb5c1afd358e7b3db`

Branch:

`release/1.12-wp04-persistent-sqlite`

The Option C remediation is implemented locally and validated inside the
six-path boundary.

Current authorized paths:

``` text
MODIFY src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs
MODIFY src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1
CREATE eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite.ps1
```

Validated evidence:

-   Build: 0 warnings / 0 errors.
-   Domain: 11 passed.
-   Application: 136 passed.
-   Architecture: 27 passed.
-   Infrastructure: 192 passed.
-   Preserved unavailable-path regression: 1 passed.
-   Targeted SQLite persistence tests: 17 passed.
-   PowerShell parsing: both WP04 scripts passed.
-   Gitleaks: 6/6 authorized paths passed.
-   `git diff --check`: passed.
-   Exact payload: 6/6, unstaged.
-   Deny-set paths unchanged.

Established markers:

`RELEASE 1.12 WP04 — SIX-PATH ALLOWLIST VERIFIED: PASS`

`RELEASE 1.12 WP04 — DENY SET PRESERVED: PASS`

`RELEASE 1.12 WP04 — EXPLICIT INITIALIZATION FLAG: IMPLEMENTED`

`RELEASE 1.12 WP04 — DEFAULT NO-CREATION SEMANTICS: PASS`

`RELEASE 1.12 WP04 — EXPLICIT PARENT INITIALIZATION: PASS`

`RELEASE 1.12 WP04 — SQLITE DELETE JOURNAL MODE: PASS`

`RELEASE 1.12 WP04 — WORKER CONFIGURATION BINDING: PASS`

`RELEASE 1.12 WP04 — INITIALIZATION TEST COVERAGE: PASS`

`RELEASE 1.12 WP04 — DISCOVERY REGRESSION TEST UNCHANGED: PASS`

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

Current blocker:

`RELEASE 1.12 WP04 — OPTION C REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

No Docker, Azure, GHCR, provider, GitHub, staging, commit, or lifecycle
mutation occurred.

------------------------------------------------------------------------

## 2. Newly discovered architectural conflict

Read-only inspection found two independent blockers to the previously
requested Docker acceptance:

### Blocker A --- container-side directory creation bypass

`container/entrypoint.sh` unconditionally creates the parent directory
of `Persistence__DatabasePath`.

That behavior independently creates storage state before the
application-owned explicit initialization capability is evaluated.

This undermines the Option C contract:

> Missing-parent creation must occur only when explicitly enabled
> through the governed SQLite initialization configuration.

Therefore Docker evidence obtained without reconciling
`container/entrypoint.sh` would not prove the application-owned
initialization contract.

### Blocker B --- prohibited direct-SQL evidence path

The runtime image currently exposes no application-owned
synthetic/offline persistence-and-integrity command or endpoint capable
of producing the originally requested Docker evidence:

-   integrity result;
-   journal mode;
-   schema version;
-   preserved historical-observation count across recreation.

Producing those results via shell/Python direct SQLite queries would
bypass the application boundary and conflict with the
no-direct-SQL/no-persistence-bypass contract.

Therefore the previous Docker script must not be executed as originally
written.

------------------------------------------------------------------------

## 3. Luna mission

Perform a narrow read-only re-governance amendment answering two
questions:

1.  How should `container/entrypoint.sh` be reconciled so container
    startup no longer preempts or bypasses the explicit
    application-owned initialization contract?

2.  What is the smallest truthful Docker acceptance mechanism that
    proves persistence/recreation without introducing a direct-SQL
    bypass or broadening WP04 into unrelated diagnostic infrastructure?

Do not mutate repository files.

------------------------------------------------------------------------

## 4. Mandatory inspection

Read and trace:

-   `container/entrypoint.sh`
-   `Dockerfile`
-   `src/AIQuantTradingResearch.Worker/Program.cs`
-   `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs`
-   `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs`
-   relevant persistence repositories/services
-   relevant Worker replay/synthetic execution path
-   relevant visualization/System Health/read-model outputs
-   `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs`
-   Release 1.12 definition/execution plan/file manifest
-   issue `#263`
-   WP03 container/runtime composition
-   Initiative-1.11 persistence qualification evidence.

Repository evidence controls.

If current Git state or the six-path implementation differs materially
from the recorded state, STOP and report the discrepancy.

------------------------------------------------------------------------

## 5. Container-entrypoint governance question

Determine whether `container/entrypoint.sh` should:

### Option E1 --- Stop creating the SQLite parent entirely

Container startup would no longer establish the persistence directory,
leaving creation exclusively to the application-owned explicit
initialization contract.

Evaluate whether the non-root runtime can still create the required
persistent directory under `/home` and whether any container bootstrap
ownership/permissions setup is still required.

### Option E2 --- Restrict entrypoint behavior to ownership/permission preparation only

If container startup must prepare filesystem ownership/permissions,
determine whether it can do so without creating the application database
parent as an application-storage semantic side effect.

### Option E3 --- Retain directory creation

Select only if repository architecture proves entrypoint creation is
intentionally part of the persistence initialization contract and can
coexist truthfully with Option C.

Do not select merely because it makes Docker startup convenient.

Report the selected option and exact rationale.

------------------------------------------------------------------------

## 6. Docker acceptance mechanism governance question

The goal is to prove, without direct SQL bypass:

1.  first container can initialize the persistent SQLite database
    through application-owned behavior;
2.  accepted/synthetic evidence exists through an application-owned
    path;
3.  first container stops/removes;
4.  second container using the same volume recovers the same accepted
    evidence;
5.  runtime remains healthy;
6.  the persistent database contract remains schema v4 / DELETE journal
    mode;
7.  cleanup removes all temporary Docker artifacts.

Evaluate, at minimum:

### Option D1 --- Existing application-owned observable surface

Determine whether existing Worker/Streamlit/System
Health/visualization/read-model output already truthfully exposes
sufficient evidence to prove persistence continuity and database health
without adding new diagnostic code.

Prefer this option if sufficient.

### Option D2 --- Existing application-owned CLI/runtime command

Determine whether an existing Worker or application command can
exercise/retrieve the required persistence evidence without direct
SQLite access.

Prefer this over adding new infrastructure.

### Option D3 --- Narrow application-owned diagnostic capability

Only if D1/D2 are insufficient, determine the smallest application-owned
diagnostic path needed for WP04.

Such a path must:

-   remain read-only with respect to accepted evidence except for normal
    governed application execution;
-   use existing persistence/application abstractions;
-   not expose arbitrary SQL;
-   not transfer persistence ownership to shell/Python;
-   not become a broad debug API;
-   not broaden into WP06 System Health redesign;
-   be explicitly limited to truthful persistence/integrity evidence.

If required, identify exact production and test paths.

### Option D4 --- Direct shell/Python SQLite query

This option is explicitly disfavored and may be selected only if Luna
proves that the existing Release 1.12 architecture explicitly permits
direct read-only SQLite diagnostics for deployment verification.

Do not select merely because it is convenient.

------------------------------------------------------------------------

## 7. Acceptance evidence reconsideration

The original requirement for "both JSON integrity results" came from the
prior Docker handoff design, not from a higher-level product
requirement.

Luna must distinguish:

-   **required semantic evidence**, from
-   **incidental evidence format**.

If the product/release contract only requires truthful proof of
persistence, schema/journal correctness, restart continuity, and no
bypass, Luna may replace the exact JSON-output requirement with an
application-owned evidence format that proves the same semantics.

Do not preserve a lower-level evidence format if doing so forces an
architectural violation.

Required decision:

`RELEASE 1.12 WP04 — ORIGINAL DOCKER JSON FORMAT: <REQUIRED|NOT REQUIRED>`

If `NOT REQUIRED`, define the replacement acceptance evidence precisely.

------------------------------------------------------------------------

## 8. Revised allowlist governance

Produce a revised exact path allowlist.

The current six paths remain presumed authorized unless this amendment
proves otherwise.

At minimum, decide whether this path must be added:

``` text
container/entrypoint.sh
```

If an application-owned diagnostic path is required, identify every
exact additional production/test path.

No wildcard directories.

No README mutation.

No modification to historical Initiative-1.11 evidence.

No modification to WP03 deployment scripts unless Luna proves current
Release 1.12 composition requires it.

Do not modify `ExperimentDiscoveryTests.cs`.

------------------------------------------------------------------------

## 9. Explicit deny principles

Regardless of selected design, preserve:

-   schema v4;
-   SQLite DELETE journal mode;
-   unavailable-path default no-creation contract;
-   explicit initialization flag default false;
-   Worker composition ownership;
-   Infrastructure persistence ownership;
-   no direct domain SQL in scripts;
-   no Python/Streamlit persistence ownership;
-   no provider-secret expansion;
-   no Azure-specific hard-coded application path;
-   no README mutation;
-   no historical evidence rewriting;
-   no WP05--WP08 scope absorption.

------------------------------------------------------------------------

## 10. Required output

Return concise evidence and the following markers:

`RELEASE 1.12 WP04 — DOCKER VALIDATION CONTRACT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — ENTRYPOINT BYPASS IDENTIFIED: PASS`

`RELEASE 1.12 WP04 — SELECTED ENTRYPOINT OPTION: <E1|E2|E3>`

`RELEASE 1.12 WP04 — SELECTED DOCKER EVIDENCE OPTION: <D1|D2|D3|D4>`

`RELEASE 1.12 WP04 — APPLICATION-OWNED EVIDENCE BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — DIRECT-SQL DOCKER BYPASS: ABSENT`

`RELEASE 1.12 WP04 — ORIGINAL DOCKER JSON FORMAT: <REQUIRED|NOT REQUIRED>`

Then print:

``` text
RELEASE 1.12 WP04 — REVISED EXACT PATH ALLOWLIST:
<CLASSIFICATION> <exact path>
...
```

Then:

`RELEASE 1.12 WP04 — ENTRYPOINT PATH ADDITION REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — DIAGNOSTIC PRODUCTION PATH ADDITION REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — DIAGNOSTIC TEST PATH ADDITION REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — README MUTATION REQUIRED: NO`

`RELEASE 1.12 WP04 — SCHEMA MIGRATION REQUIRED: NO`

If the revised boundary is sufficiently governed:

`RELEASE 1.12 WP04 — TERRA DOCKER-REMEDIATION AUTHORITY: READY`

Otherwise:

`RELEASE 1.12 WP04 — TERRA DOCKER-REMEDIATION AUTHORITY: BLOCKED`

------------------------------------------------------------------------

## 11. Stop boundary

After the reconciliation and revised allowlist:

**STOP.**

Do not edit files.

Do not run Docker.

Do not stage, commit, push, create a PR, use Azure/GHCR/provider
services, or mutate issue/Project/milestone state.

The result will be reviewed and used to create the next downloadable
**GPT-5.6 Terra WP04 Docker Remediation Execution Authority**.

Terminal:

`RELEASE 1.12 WP04 — LUNA DOCKER VALIDATION RE-GOVERNANCE AMENDMENT COMPLETE`

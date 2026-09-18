# GPT-5.6 Terra --- Release 1.12 WP04 Azure Validation Continuation Authority

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

-   **GPT-5.6 Luna** --- contract, policy, architecture, reconciliation,
    acceptance criteria, governance, read-only/planning.
-   **GPT-5.6 Terra** --- PRIMARY for this prompt: Azure/GHCR validation
    execution, exact mutation accounting, and preparation of the
    PR-publication continuation boundary.
-   **GPT-5.6 Sol** --- supporting analysis/synthesis only; never
    silently replaces Luna or Terra.

------------------------------------------------------------------------

## 1. Governed starting point

Resume:

**Phase 4 --- Release 1.12 WP04: Persistent SQLite Initialization, Data
Update & Recovery**

Issue:

`#263`

Canonical `origin/main`:

`40a9a236dae789864f35e64bb5c1afd358e7b3db`

Candidate commit already created and pushed:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Current known publication state:

-   local/main ahead-behind: `0/0`
-   exact WP04 candidate payload: `11/11`
-   Gitleaks: PASS
-   PowerShell validation: PASS
-   diff check: PASS
-   non-force push: PASS
-   PR created: NO
-   GitHub lifecycle mutation: NO
-   Azure mutation during last attempt: NO
-   Docker mutation during last attempt: NO
-   provider mutation during last attempt: NO

The branch and candidate commit remain pushed.

PR creation was intentionally blocked because required WP04 Azure
validation evidence was not yet established.

Required starting marker:

`RELEASE 1.12 WP04 — AZURE VALIDATION CONTINUATION START: PASS`

------------------------------------------------------------------------

## 2. Exact candidate payload remains frozen

The candidate implementation is exactly these eleven tracked paths:

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

No repository edits are authorized under this continuation authority.

The local untracked helper:

``` text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/validate-docker-qualification.ps1
```

remains excluded from the candidate payload.

Unrelated untracked `prompters/` files remain excluded.

Required:

`RELEASE 1.12 WP04 — CANDIDATE PAYLOAD FROZEN 11/11: PASS`

------------------------------------------------------------------------

## 3. Azure validation objective

Establish the missing deployed WP04 evidence against the governed
Release 1.12 public-reference deployment boundary.

Target contract:

-   Azure App Service Linux F1
-   West Central US
-   custom Docker
-   public/free GHCR
-   persistent `/home`
-   SQLite database path `/home/data/aiquant.db`
-   explicit application-owned storage initialization enabled
-   schema v4
-   journal mode DELETE
-   E2-A bounded root filesystem preparation
-   permanent privilege drop to `aiq`
-   D3 application-owned qualification evidence
-   no direct shell/Python SQLite domain inspection
-   no Azure SQL
-   no paid dependency
-   no production/SLA claim

------------------------------------------------------------------------

## 4. Fresh Azure/GHCR reconciliation before mutation

Before changing anything:

1.  inspect existing Azure resource state;
2.  confirm target resource group/app/plan are the governed Release 1.12
    reference-deployment resources;
3.  confirm App Service plan SKU remains F1;
4.  confirm region remains West Central US;
5.  inspect current container image reference;
6.  inspect current relevant app settings;
7.  inspect current persistence configuration;
8.  inspect current GHCR state;
9.  verify no paid Azure dependency exists;
10. confirm no existing PR has appeared since the last attempt.

Do not mutate during this reconciliation.

Required:

`RELEASE 1.12 WP04 — AZURE PRE-MUTATION RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — AZURE PLAN SKU F1: PASS`

`RELEASE 1.12 WP04 — AZURE REGION WEST CENTRAL US: PASS`

`RELEASE 1.12 WP04 — STRICT-ZERO-COST BASELINE: PASS`

If the deployed environment materially diverged or requires architecture
changes, STOP.

------------------------------------------------------------------------

## 5. Candidate image publication

If the exact candidate image is not already available, publish the
candidate commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

to the existing governed public/free GHCR namespace.

Requirements:

-   image must correspond exactly to candidate commit;
-   capture immutable digest;
-   no mandatory ACR;
-   no private paid registry dependency;
-   do not overwrite historical release evidence tags unless existing
    governed scripts explicitly require it;
-   no secrets in build arguments.

Required:

`RELEASE 1.12 WP04 — CANDIDATE GHCR PUBLICATION: PASS`

`RELEASE 1.12 WP04 — CANDIDATE GHCR IMAGE: <image@sha256:digest>`

------------------------------------------------------------------------

## 6. Deploy candidate to Azure F1

Use the existing governed Release 1.12 deployment boundary and scripts.

Configure:

``` text
Persistence__DatabasePath=/home/data/aiquant.db
```

and the explicit application-owned storage-initialization flag required
by Option C.

Preserve:

-   `/home` persistence;
-   App Service storage enabled;
-   HTTPS/default DNS;
-   F1 SKU;
-   non-root long-running runtime;
-   no provider credential expansion;
-   no schema migration;
-   no direct SQL bypass.

Record every explicit Azure mutation.

Required:

`RELEASE 1.12 WP04 — AZURE CANDIDATE DEPLOYMENT: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE PATH: /home/data/aiquant.db`

`RELEASE 1.12 WP04 — AZURE EXPLICIT INITIALIZATION: ENABLED`

------------------------------------------------------------------------

## 7. Runtime ownership qualification

Prove in the deployed environment:

-   startup preparation can establish the configured persistence parent;
-   persistence parent ownership resolves to the `aiq` runtime identity;
-   mode is equivalent to the governed `0750` intent;
-   long-running application process executes non-root;
-   no long-running root application process remains;
-   filesystem preparation is separate from SQLite semantic
    initialization.

Do not use direct SQL for this proof.

Required:

`RELEASE 1.12 WP04 — AZURE E2-A STORAGE PREPARATION: PASS`

`RELEASE 1.12 WP04 — AZURE NON-ROOT RUNTIME: PASS`

`RELEASE 1.12 WP04 — AZURE FILESYSTEM/SQLITE BOUNDARY: PASS`

------------------------------------------------------------------------

## 8. Application-owned persistence qualification

Use the D3 application-owned qualification mechanism.

Do not inspect domain rows with shell/Python SQLite tools.

Prove initial qualification:

-   schema version = 4;
-   journal mode = delete;
-   integrity = ok;
-   quick-check = ok;
-   accepted evidence identity/count recorded;
-   qualification phase succeeds.

Required:

`RELEASE 1.12 WP04 — AZURE INITIAL PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE SCHEMA V4: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE DELETE JOURNAL: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE INTEGRITY: PASS`

------------------------------------------------------------------------

## 9. Restart persistence validation

Perform a governed App Service restart.

After restart, prove through the application-owned qualification path:

-   accepted evidence remains present;
-   identity/count continuity is preserved;
-   schema remains v4;
-   journal remains delete;
-   integrity remains ok;
-   quick-check remains ok.

Required:

`RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS`

------------------------------------------------------------------------

## 10. Redeployment persistence validation

Perform a governed candidate-image redeployment/restart boundary without
deleting `/home`.

After redeployment, prove:

-   accepted evidence survives;
-   identity/count continuity remains correct;
-   schema remains v4;
-   journal remains delete;
-   integrity remains ok;
-   quick-check remains ok;
-   no new persistence fallback path appears.

Required:

`RELEASE 1.12 WP04 — AZURE REDEPLOYMENT PERSISTENCE: PASS`

------------------------------------------------------------------------

## 11. No-bypass and cost validation

Prove:

-   no direct SQL deployment diagnostic bypass;
-   no shell/Python domain-row inspection;
-   no Azure SQL;
-   no Container Apps;
-   no Azure Files;
-   no paid SKU;
-   no mandatory ACR;
-   no production/SLA claim introduced.

Required:

`RELEASE 1.12 WP04 — AZURE DIRECT-SQL BYPASS: ABSENT`

`RELEASE 1.12 WP04 — AZURE STRICT-ZERO-COST BOUNDARY: PRESERVED`

------------------------------------------------------------------------

## 12. Validation cleanup/restoration

If temporary validation settings/tags/restarts were introduced, restore
the deployment to the intended Release 1.12 candidate configuration.

Do not remove persisted evidence required for continuity proof until
evidence is captured.

Report exact final Azure state.

Required:

`RELEASE 1.12 WP04 — AZURE VALIDATION CLEANUP: PASS`

------------------------------------------------------------------------

## 13. PR boundary

This authority does **not** authorize PR creation or merge.

Its purpose is to establish the previously missing deployed evidence.

After Azure qualification succeeds, STOP and return evidence for
creation of a separate PR-publication/merge/lifecycle continuation
authority.

Do not:

-   create PR;
-   merge;
-   close #263;
-   mutate Project #2;
-   mutate milestone #63;
-   start WP05.

Required:

`RELEASE 1.12 WP04 — PR PUBLICATION BOUNDARY PRESERVED: PASS`

------------------------------------------------------------------------

## 14. Mutation accounting

Report exact actual mutations.

### GHCR

-   candidate image/tag/digest creations: exact count
-   historical tags changed: exact count
-   retained candidate image state: exact result

### Azure

Count every explicit mutation, including:

-   app-setting changes;
-   image-reference changes;
-   restarts;
-   redeployments;
-   temporary validation configuration;
-   cleanup/restoration actions.

### Repository/Git

-   repository file mutations: 0
-   staged paths: 0
-   new commits: 0
-   new pushes: 0 unless strictly needed for validation metadata;
    otherwise 0
-   PRs: 0

### Lifecycle

-   issue mutations: 0
-   Project mutations: 0
-   milestone mutations: 0

### Provider

-   provider mutations: 0

Required:

`RELEASE 1.12 WP04 — AZURE VALIDATION MUTATION AUDIT: PASS`

------------------------------------------------------------------------

## 15. Stop conditions

STOP if:

-   F1 is no longer available or would incur paid infrastructure;
-   `/home` persistence cannot be preserved;
-   candidate requires architecture expansion;
-   schema migration becomes necessary;
-   direct SQL domain inspection would be required;
-   privilege-drop flow fails;
-   persisted evidence does not survive restart/redeploy;
-   candidate image cannot be tied exactly to
    `ef4a5caf4768ab82c66f0e539d92c1631761b500`;
-   unrelated repository mutation becomes necessary.

------------------------------------------------------------------------

## 16. Required terminal output

On full success:

`RELEASE 1.12 WP04 — AZURE DEPLOYED PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE REDEPLOYMENT PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE NON-ROOT RUNTIME: PASS`

`RELEASE 1.12 WP04 — AZURE STRICT-ZERO-COST BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — AZURE VALIDATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — PR PUBLICATION CONTINUATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA AZURE VALIDATION CONTINUATION COMPLETE`

If blocked:

`RELEASE 1.12 WP04 — AZURE VALIDATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

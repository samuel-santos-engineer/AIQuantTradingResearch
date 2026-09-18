# GPT-5.6 Terra — Release 1.12 WP04 Azure Application-Owned Persistence Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY for this prompt: application-owned Azure persistence qualification execution and exact mutation accounting.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting point

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**.

Issue: `#263`

Candidate commit: `ef4a5caf4768ab82c66f0e539d92c1631761b500`

Canonical `origin/main` at prior reconciliation: `40a9a236dae789864f35e64bb5c1afd358e7b3db`

Known Azure configuration state is established:

- Web App Running
- West Central US
- Linux F1
- HTTPS-only enabled
- persistent App Service storage enabled
- SQLite path `/home/data/aiquant.db`
- parent-directory initialization enabled
- owned-resource inventory correct

Still unproven: schema version, journal mode, integrity, quick-check, accepted evidence identity/count, restart continuity, and redeployment continuity.

Required:

`RELEASE 1.12 WP04 — AZURE CONFIGURATION VERIFICATION: PASS`

## 2. Scope

Authorize only the application-owned persistence qualification needed to establish the remaining Azure evidence.

No repository edits. No PR creation/merge. No issue/Project/milestone mutation. No WP05 execution.

## 3. Application-owned boundary

Use the governed D3 application-owned qualification mechanism.

Preserve ownership:

- .NET application owns persistence semantics;
- Infrastructure owns SQLite/provider mechanics;
- Worker owns composition/execution;
- Python/Streamlit do not own SQLite;
- no direct SQL deployment bypass;
- no shell/Python SQLite domain-row inspection;
- no schema migration.

Required:

`RELEASE 1.12 WP04 — APPLICATION-OWNED QUALIFICATION BOUNDARY: PASS`

## 4. Initial qualification

Execute application-owned qualification against `/home/data/aiquant.db`.

Capture the full qualification record and prove:

- database path identity is `/home/data/aiquant.db`;
- schema version = `4`;
- journal mode = `delete`;
- integrity = `ok`;
- quick-check = `ok`;
- accepted evidence identity/count is reported;
- no fallback path;
- no direct SQL bypass.

Required:

`RELEASE 1.12 WP04 — AZURE INITIAL PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE SCHEMA V4: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE DELETE JOURNAL: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE INTEGRITY: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE QUICK-CHECK: PASS`

Record initial accepted evidence identity/count.

## 5. Restart continuity

Perform one governed Azure Web App restart and count it as an Azure mutation.

After health is restored, rerun application-owned qualification and prove:

- same database path;
- schema `4`;
- journal `delete`;
- integrity `ok`;
- quick-check `ok`;
- accepted evidence identity/count coherent with initial run;
- no destructive reinitialization;
- no fallback database path.

Required:

`RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS`

## 6. Redeployment continuity

Perform one governed candidate redeployment/restart boundary using the exact candidate image or exact candidate-commit-derived image.

Do not delete persistent `/home`.

After health is restored, rerun application-owned qualification and prove:

- `/home/data/aiquant.db` remains active;
- accepted evidence survives;
- identity/count continuity is preserved;
- schema remains `4`;
- journal remains `delete`;
- integrity remains `ok`;
- quick-check remains `ok`;
- no fallback path.

Required:

`RELEASE 1.12 WP04 — AZURE REDEPLOYMENT PERSISTENCE: PASS`

## 7. Runtime truth

If the existing qualification surface exposes runtime identity/storage diagnostics, capture and prove:

- long-running runtime is non-root;
- E2-A storage preparation completed;
- filesystem ownership preparation remains distinct from SQLite semantic initialization.

Do not introduce a bypass just to obtain this evidence.

Required if supported by the existing qualification surface:

`RELEASE 1.12 WP04 — AZURE NON-ROOT RUNTIME: PASS`

`RELEASE 1.12 WP04 — AZURE FILESYSTEM/SQLITE BOUNDARY: PASS`

If not exposed, report that fact explicitly rather than inventing evidence.

## 8. Zero-cost / no-bypass guard

Confirm:

- no direct shell/Python SQL inspection;
- no Azure SQL;
- no Container Apps;
- no Azure Files;
- no paid SKU;
- no ACR requirement;
- App Service plan remains F1.

Required:

`RELEASE 1.12 WP04 — AZURE DIRECT-SQL BYPASS: ABSENT`

`RELEASE 1.12 WP04 — AZURE STRICT-ZERO-COST BOUNDARY: PRESERVED`

## 9. Mutation accounting

Expected Azure mutations:

- Web App restart: `1`
- candidate redeployment/restart boundary: `1`
- app setting changes: expected `0`
- plan/SKU changes: `0`
- resource creations: `0`
- resource deletions: `0`

GHCR mutations: expected `0` if exact candidate image already exists; otherwise report only the exact additional candidate-image publication mutation if strictly necessary.

Repository/Git:

- file mutations: `0`
- staged paths: `0`
- commits: `0`
- pushes: `0`
- PRs: `0`

GitHub lifecycle:

- issue mutations: `0`
- Project mutations: `0`
- milestone mutations: `0`

Provider mutations: `0`

Required:

`RELEASE 1.12 WP04 — AZURE APPLICATION QUALIFICATION MUTATION AUDIT: PASS`

## 10. Stop conditions

STOP if:

- application-owned qualification cannot access the governed database path;
- schema is not v4;
- journal is not DELETE;
- integrity or quick-check fails;
- accepted evidence continuity breaks;
- direct SQL inspection becomes necessary;
- schema migration becomes necessary;
- persistent `/home` would be deleted;
- paid infrastructure would be required;
- candidate identity cannot be proven.

Do not change the eleven-path implementation without Luna re-governance.

## 11. Return evidence

Return:

- full initial qualification record;
- full post-restart qualification record;
- full post-redeployment qualification record;
- all `WP04_*` markers;
- command exit codes;
- exact Azure/GHCR mutation accounting;
- any error text.

Do not return secrets.

## 12. Terminal markers

On success:

`RELEASE 1.12 WP04 — AZURE DEPLOYED PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE REDEPLOYMENT PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE SCHEMA V4: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE DELETE JOURNAL: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE INTEGRITY: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE QUICK-CHECK: PASS`

`RELEASE 1.12 WP04 — AZURE STRICT-ZERO-COST BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — AZURE APPLICATION QUALIFICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — PR PUBLICATION CONTINUATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA AZURE APPLICATION QUALIFICATION COMPLETE`

If blocked:

`RELEASE 1.12 WP04 — AZURE APPLICATION QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

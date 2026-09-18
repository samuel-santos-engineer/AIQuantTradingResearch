# GPT-5.6 Terra — Release 1.12 WP04 Post-Reset Application-Owned SQLite Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: execute the approved Azure qualification mutations and validation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Accepted post-reset state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**.

Issue: `#263`

Candidate commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Candidate image digest:

`sha256:1507a90c5fc8882bd633cdda549dd5a9282277b4e417645696b89adc79939998`

The quota-reset gate is accepted as passed from operator evidence:

- App Service plan: F1 / Free
- region: West Central US
- Web App: `Running`
- exact candidate digest remains configured
- persistent App Service storage remains enabled
- database path remains `/home/data/aiquant.db`
- parent initialization remains enabled
- registry username/password remain absent
- `WP04_RESET_PERSISTENCE_EXIT_CODE=0`
- `WP04_RESET_REGISTRY_CREDENTIALS_EXIT_CODE=0`

Required starting markers:

`RELEASE 1.12 WP04 — F1 QUOTA RESET OBSERVED: PASS`

`RELEASE 1.12 WP04 — AZURE APP RUNNING POST-RESET: PASS`

## 2. Mission

Complete only the remaining application-owned Azure SQLite qualification:

1. initial D3 qualification;
2. restart continuity;
3. redeployment continuity;
4. exact mutation accounting.

No repository mutation, PR publication, merge, or lifecycle mutation is authorized here.

## 3. Initial D3 qualification

Use the existing application-owned qualification mechanism against:

`/home/data/aiquant.db`

Do not use direct shell/Python SQLite domain inspection.

Capture the complete qualification JSON record and exit code.

Prove:

- database path identity is correct;
- schema version = `4`;
- journal mode = `delete`;
- integrity = `ok`;
- quick-check = `ok`;
- accepted evidence identity/count is present;
- qualification succeeds without fallback.

Required:

`RELEASE 1.12 WP04 — APPLICATION-OWNED QUALIFICATION BOUNDARY: PASS`

`RELEASE 1.12 WP04 — AZURE INITIAL PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE SCHEMA V4: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE DELETE JOURNAL: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE INTEGRITY: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE QUICK-CHECK: PASS`

Record the accepted evidence identity/count as the continuity baseline.

## 4. Restart continuity

Perform exactly one governed Web App restart.

Wait for `Running`/healthy state, then execute D3 again.

Capture the complete post-restart qualification JSON record and exit code.

Prove:

- same database path;
- accepted evidence identity/count preserved;
- schema remains `4`;
- journal remains `delete`;
- integrity remains `ok`;
- quick-check remains `ok`;
- no fallback database path.

Required:

`RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS`

## 5. Redeployment continuity

Perform exactly one governed redeployment/restart boundary using the same exact candidate digest.

Do not delete or replace persistent `/home`.

Wait for healthy `Running` state, then execute D3 a third time.

Capture the complete post-redeployment qualification JSON record and exit code.

Prove:

- `/home/data/aiquant.db` remains active;
- accepted evidence identity/count survives;
- schema remains `4`;
- journal remains `delete`;
- integrity remains `ok`;
- quick-check remains `ok`;
- no fallback database appears.

Required:

`RELEASE 1.12 WP04 — AZURE REDEPLOYMENT PERSISTENCE: PASS`

## 6. Runtime and architectural truth

Where exposed by the existing governed qualification/runtime evidence, prove:

- long-running runtime is non-root;
- E2-A storage preparation is effective;
- filesystem ownership preparation remains distinct from application-owned SQLite semantic initialization.

Do not create a new bypass to obtain this evidence.

Required when supported by existing evidence:

`RELEASE 1.12 WP04 — AZURE NON-ROOT RUNTIME: PASS`

`RELEASE 1.12 WP04 — AZURE FILESYSTEM/SQLITE BOUNDARY: PASS`

## 7. Zero-cost and no-bypass guard

Confirm after qualification:

- plan remains F1 / Free;
- no Azure SQL;
- no Container Apps;
- no Azure Files;
- no ACR requirement;
- no registry credentials added;
- no paid resource added;
- no direct SQL diagnostic bypass.

Required:

`RELEASE 1.12 WP04 — AZURE DIRECT-SQL BYPASS: ABSENT`

`RELEASE 1.12 WP04 — AZURE STRICT-ZERO-COST BOUNDARY: PRESERVED`

## 8. Mutation accounting

Report actual mutations only.

Expected:

- Web App restart: `1`
- candidate redeployment/restart boundary: `1`
- image-reference change: expected `0` because exact digest is already configured
- app-setting mutations: expected `0`, except temporary qualification-mode settings if the governed mechanism strictly requires them; if used, count both set and removal and prove restoration
- registry credential mutations: `0`
- plan/SKU mutations: `0`
- resource creates/deletes: `0`
- GHCR mutations: `0`
- repository/Git mutations: `0`
- PR/GitHub lifecycle mutations: `0`
- provider mutations: `0`

Required:

`RELEASE 1.12 WP04 — POST-RESET AZURE MUTATION AUDIT: PASS`

## 9. Stop conditions

STOP if:

- D3 cannot run through the application-owned mechanism;
- schema != 4;
- journal != delete;
- integrity/quick-check fails;
- accepted evidence identity/count continuity breaks;
- `/home` persistence is lost;
- direct SQL inspection becomes necessary;
- schema migration becomes necessary;
- paid infrastructure becomes necessary;
- candidate digest changes unexpectedly.

Do not change the eleven-path candidate.

## 10. Return evidence

Return:

- complete initial D3 JSON record;
- complete post-restart D3 JSON record;
- complete post-redeployment D3 JSON record;
- all `WP04_*` lines;
- relevant exit codes;
- exact mutation accounting;
- error text, if any.

Do not return secrets.

## 11. Terminal markers

On full success:

`RELEASE 1.12 WP04 — AZURE DEPLOYED PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE REDEPLOYMENT PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE SCHEMA V4: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE DELETE JOURNAL: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE INTEGRITY: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE QUICK-CHECK: PASS`

`RELEASE 1.12 WP04 — AZURE STRICT-ZERO-COST BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — POST-RESET AZURE MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — PR PUBLICATION CONTINUATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA POST-RESET APPLICATION QUALIFICATION COMPLETE`

If blocked:

`RELEASE 1.12 WP04 — AZURE APPLICATION QUALIFICATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

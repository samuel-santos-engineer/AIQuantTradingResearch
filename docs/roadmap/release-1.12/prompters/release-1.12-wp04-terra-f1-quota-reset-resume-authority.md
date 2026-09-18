# GPT-5.6 Terra — Release 1.12 WP04 Azure F1 Quota-Reset Resume Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: post-quota Azure resume validation, application-owned qualification execution, exact mutation accounting.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting point

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**.

Issue: `#263`

Candidate commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Candidate image:

`ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:1507a90c5fc8882bd633cdda549dd5a9282277b4e417645696b89adc79939998`

Accepted diagnosis:

- App Service plan SKU: `F1`
- tier: `Free`
- location: `West Central US`
- Web App availability state: `Normal`
- Web App state: `QuotaExceeded`
- log stream: `403 Site Disabled`
- exact candidate digest configured
- anonymous GHCR manifest access independently passed
- registry username/password absent
- container never started
- no application-owned D3 qualification record exists
- temporary `Worker__Mode` / qualification-phase settings were removed
- intended persistence settings remain:
  - `Persistence__DatabasePath=/home/data/aiquant.db`
  - `Persistence__CreateParentDirectoryForInitialization=true`

Classification:

`AZURE APP SERVICE F1 QUOTA SUSPENSION`

Do **not** classify this as a GHCR credential defect unless new post-reset evidence proves otherwise.

Required:

`RELEASE 1.12 WP04 — F1 QUOTA SUSPENSION DIAGNOSIS: PASS`

## 2. Authority boundary

This authority is only for resuming qualification **after the F1 quota resets**.

Until the quota is reset, do not:

- change registry credentials;
- add GHCR credentials;
- change image digest;
- change SKU;
- move region;
- create paid resources;
- recreate the Web App;
- modify repository files;
- create a PR;
- close issue #263;
- mutate Project #2;
- mutate milestone #63.

## 3. Read-only reset confirmation

At the next execution session, first perform read-only checks.

Prove:

- plan remains F1 / Free;
- app remains in West Central US;
- availability remains Normal;
- state is no longer `QuotaExceeded`;
- candidate image still points to the exact digest above;
- intended persistence settings remain unchanged;
- no registry username/password has appeared.

Required:

`RELEASE 1.12 WP04 — F1 QUOTA RESET OBSERVED: PASS`

If state is still `QuotaExceeded` or logs still show `403 Site Disabled`, STOP with no mutation.

## 4. Start/resume app only after reset

Once quota reset is proven, start the existing app if necessary.

Count only the explicit start/restart mutation actually performed.

Wait until the app reports `Running`.

Do not alter image, persistence, registry, SKU, or resource topology.

Required:

`RELEASE 1.12 WP04 — AZURE APP RUNNING POST-RESET: PASS`

If startup again reports `ImagePullUnauthorizedFailure`, collect fresh post-reset evidence before changing anything. Do not assume registry credentials are required.

## 5. Retry application-owned persistence qualification

Once the candidate container is actually running, resume the previously authorized D3 application-owned qualification.

Execute the initial qualification against:

`/home/data/aiquant.db`

Prove:

- database path identity correct;
- schema version = `4`;
- journal mode = `delete`;
- integrity = `ok`;
- quick-check = `ok`;
- accepted evidence identity/count recorded;
- no fallback path;
- no direct shell/Python SQLite inspection.

Required:

`RELEASE 1.12 WP04 — AZURE INITIAL PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE SCHEMA V4: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE DELETE JOURNAL: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE INTEGRITY: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE QUICK-CHECK: PASS`

## 6. Restart continuity

Perform one governed Web App restart.

After health returns, rerun the D3 qualification and prove:

- same database path;
- accepted evidence identity/count preserved;
- schema remains 4;
- journal remains delete;
- integrity remains ok;
- quick-check remains ok.

Required:

`RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS`

## 7. Redeployment continuity

Perform one governed candidate-image redeployment/restart boundary without deleting `/home`.

After health returns, rerun D3 and prove:

- accepted evidence survives;
- identity/count remains coherent;
- schema remains 4;
- journal remains delete;
- integrity remains ok;
- quick-check remains ok;
- no fallback database appears.

Required:

`RELEASE 1.12 WP04 — AZURE REDEPLOYMENT PERSISTENCE: PASS`

## 8. Zero-cost/no-bypass guard

Confirm:

- plan remains F1;
- no paid resource was added;
- no Azure SQL;
- no Container Apps;
- no Azure Files;
- no ACR requirement;
- no direct SQL diagnostic bypass;
- no registry credential addition unless separately re-governed by new evidence.

Required:

`RELEASE 1.12 WP04 — AZURE STRICT-ZERO-COST BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — AZURE DIRECT-SQL BYPASS: ABSENT`

## 9. Mutation accounting

Report exact actual mutations.

Expected possibilities:

- Web App start: `0 or 1`
- Web App restart for continuity: `1`
- candidate redeployment/restart boundary: `1`
- app-setting mutations: expected `0`
- registry credential mutations: `0`
- SKU/plan mutations: `0`
- resource creations: `0`
- resource deletions: `0`
- GHCR mutations: expected `0`
- repository/Git mutations: `0`
- PR mutations: `0`
- GitHub lifecycle mutations: `0`
- provider mutations: `0`

Required:

`RELEASE 1.12 WP04 — POST-RESET AZURE MUTATION AUDIT: PASS`

## 10. Stop conditions

STOP immediately if:

- quota is not reset;
- state remains `QuotaExceeded`;
- app cannot become Running;
- candidate image identity changes;
- persistence settings drift;
- startup fails after reset and root cause is not yet proven;
- schema != 4;
- journal != delete;
- integrity/quick-check fails;
- accepted evidence continuity breaks;
- paid infrastructure would be required;
- direct SQL inspection would be required.

Do not compensate by changing architecture or the eleven-path candidate.

## 11. Terminal markers

On success:

`RELEASE 1.12 WP04 — F1 QUOTA RESET OBSERVED: PASS`

`RELEASE 1.12 WP04 — AZURE APP RUNNING POST-RESET: PASS`

`RELEASE 1.12 WP04 — AZURE DEPLOYED PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE RESTART PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE REDEPLOYMENT PERSISTENCE: PASS`

`RELEASE 1.12 WP04 — AZURE STRICT-ZERO-COST BOUNDARY: PRESERVED`

`RELEASE 1.12 WP04 — POST-RESET AZURE MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — PR PUBLICATION CONTINUATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA F1 QUOTA-RESET RESUME AUTHORITY COMPLETE`

If quota has not reset:

`RELEASE 1.12 WP04 — AZURE APPLICATION QUALIFICATION: WAITING_FOR_F1_QUOTA_RESET`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY PAUSED`

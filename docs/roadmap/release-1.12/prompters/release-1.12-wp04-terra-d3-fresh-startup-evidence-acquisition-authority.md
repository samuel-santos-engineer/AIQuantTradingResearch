# GPT-5.6 Terra — Release 1.12 WP04 Fresh D3 Startup Evidence Acquisition Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: execute the narrowly approved Azure startup/log-capture evidence acquisition and exact mutation accounting.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting point

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**.

Issue: `#263`

Candidate commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Candidate image digest remains:

`sha256:1507a90c5fc8882bd633cdda549dd5a9282277b4e417645696b89adc79939998`

Accepted Luna reconciliation:

- D3 activation: `Worker__Mode=PersistentSqliteQualification`
- phase: `PersistentSqliteQualification__Phase=initialize` or `reopen`
- database path: `Persistence__DatabasePath=/home/data/aiquant.db`
- success output: one JSON qualification record to stdout
- success exit code: `0`
- failure output: stderr
- failure exit code: `1`
- App Service entrypoint launches Worker and Streamlit as child processes
- current archived logs do not prove D3 execution or a pre-emission failure
- failure classification: `INSUFFICIENT_EVIDENCE`
- remediation decision: `D`
- repository change required: `UNDETERMINED`

Required starting markers:

`RELEASE 1.12 WP04 — D3 RECONCILIATION: INCOMPLETE`

`RELEASE 1.12 WP04 — D3 REMEDIATION DECISION: D`

## 2. Mission

Acquire the minimum new evidence needed to determine whether the existing candidate can emit D3 correctly in Azure.

This authority permits only:

1. temporary qualification-mode settings;
2. one controlled Web App restart;
3. fresh startup/log capture;
4. temporary-setting removal/restoration;
5. post-run read-only verification.

It does **not** authorize any repository implementation change.

## 3. Pre-mutation verification

Before mutation, verify:

- Web App is `Running`;
- plan remains F1 / Free;
- region remains West Central US;
- exact candidate digest remains configured;
- persistent App Service storage remains enabled;
- `Persistence__DatabasePath=/home/data/aiquant.db`;
- `Persistence__CreateParentDirectoryForInitialization=true`;
- registry credentials remain absent.

If any of these drifted, STOP.

Required:

`RELEASE 1.12 WP04 — D3 EVIDENCE ACQUISITION PRECHECK: PASS`

## 4. Enable fresh D3 initialize attempt

Temporarily set exactly:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
```

Do not change persistence settings, image reference, registry settings, SKU, or resource topology.

Count the app-setting mutation exactly.

Required:

`RELEASE 1.12 WP04 — D3 TEMP SETTINGS APPLIED: PASS`

## 5. Controlled restart

Perform exactly one Web App restart.

Immediately begin fresh evidence capture.

Wait until one of the following is proven:

### Success path
A complete D3 JSON qualification record is emitted to stdout.

### Failure path
A concrete Worker/startup/exception record proves why D3 did not reach JSON emission.

Do not infer from `Running` alone.

Required:

`RELEASE 1.12 WP04 — D3 FRESH STARTUP ATTEMPT EXECUTED: PASS`

## 6. Evidence capture requirements

Capture Azure-accessible application/container/worker logs for the fresh attempt, including enough context to establish:

- restart/start timestamp;
- candidate image identity if available;
- Worker process startup;
- observed Worker mode/configuration if logged;
- whether `PersistentSqliteQualificationExecution` was entered;
- stdout D3 JSON record if emitted;
- stderr/exception path if emission failed;
- process exit behavior;
- whether App Service restarted/recycled the child process.

Prefer fresh log download/archive plus live tail if needed.

Do not use direct SQL inspection.

### If JSON is emitted

Return the complete JSON record and classify:

`D3_EMISSION_CONFIRMED`

Then evaluate the record only for facts it actually contains.

### If no JSON is emitted but a concrete exception/startup path exists

Return the exact relevant error/exception evidence and classify one of:

- `INVOCATION_CONFIGURATION_MISMATCH`
- `QUALIFICATION_PATH_NOT_REACHED`
- `QUALIFICATION_PRE_EMISSION_FAILURE`
- `APPLICATION_HOSTING_LIFECYCLE_MISMATCH`
- `OTHER_PROVEN_CAUSE`

### If neither JSON nor a concrete failure path is captured

Classify:

`INSUFFICIENT_EVIDENCE`

Required:

`RELEASE 1.12 WP04 — D3 FRESH LOG EVIDENCE CAPTURE: PASS`

`RELEASE 1.12 WP04 — D3 FRESH STARTUP CLASSIFICATION: <classification>`

## 7. Restore normal settings

After evidence capture, remove the temporary settings:

```text
Worker__Mode
PersistentSqliteQualification__Phase
```

Then verify preservation of:

```text
WEBSITES_ENABLE_APP_SERVICE_STORAGE=true
Persistence__DatabasePath=/home/data/aiquant.db
Persistence__CreateParentDirectoryForInitialization=true
```

Do not alter unrelated settings.

Required:

`RELEASE 1.12 WP04 — D3 TEMP SETTINGS REMOVED: PASS`

`RELEASE 1.12 WP04 — PERSISTENCE SETTINGS PRESERVED: PASS`

## 8. Decision boundary

### Case A — D3 JSON emitted successfully

If a valid application-owned JSON record is captured:

- repository change required = `NO`;
- record its schema/journal/integrity/quick-check/evidence identity values;
- do **not** perform restart continuity or redeployment continuity under this authority;
- STOP and return evidence for the next qualification-continuation authority.

Required:

`RELEASE 1.12 WP04 — D3 EMISSION CONFIRMED: PASS`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — AZURE QUALIFICATION CONTINUATION AUTHORITY: READY`

### Case B — concrete no-code invocation/capture defect

If evidence proves a no-code correction:

- repository change required = `NO`;
- identify exact correction;
- STOP for a separate Terra qualification-retry authority.

### Case C — implementation defect proven

If evidence proves the existing candidate cannot emit D3 correctly under App Service:

- repository change required = `YES`;
- identify exact defect and minimum path delta;
- do not modify source;
- STOP for Luna narrow remediation re-governance.

### Case D — still insufficient

If evidence remains insufficient:

- repository change required = `UNDETERMINED`;
- return exactly what remains missing;
- do not repeat mutations automatically.

## 9. Mutation accounting

Report exact actual mutations.

Expected maximum:

- temporary app-setting set/update operation(s): exact count
- Web App restart: `1`
- temporary app-setting removals: exact count

Expected zero:

- image reference changes: `0`
- registry credential changes: `0`
- SKU changes: `0`
- resource creates/deletes: `0`
- GHCR mutations: `0`
- repository/Git mutations: `0`
- PR/GitHub lifecycle mutations: `0`
- provider mutations: `0`
- Docker mutations: `0`

Required:

`RELEASE 1.12 WP04 — D3 EVIDENCE ACQUISITION MUTATION AUDIT: PASS`

## 10. Stop conditions

STOP if:

- app is not Running before the attempt;
- quota suspension returns;
- candidate digest changes;
- persistence settings drift;
- paid infrastructure would be required;
- direct SQL inspection would be required;
- source changes appear necessary before evidence capture completes.

## 11. Return evidence

Return:

- precheck markers;
- setting/restart exit codes;
- fresh log capture exit codes;
- complete D3 JSON if emitted;
- otherwise exact startup/exception evidence;
- fresh startup classification;
- settings restoration proof;
- exact mutation accounting;
- repository-change determination.

Do not return secrets.

## 12. Terminal markers

Successful D3 emission:

`RELEASE 1.12 WP04 — D3 FRESH STARTUP EVIDENCE: PASS`

`RELEASE 1.12 WP04 — D3 EMISSION CONFIRMED: PASS`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — AZURE QUALIFICATION CONTINUATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA D3 EVIDENCE ACQUISITION COMPLETE`

Concrete implementation defect:

`RELEASE 1.12 WP04 — D3 FRESH STARTUP EVIDENCE: PASS`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES`

`RELEASE 1.12 WP04 — LUNA NARROW REMEDIATION RE-GOVERNANCE: REQUIRED`

`RELEASE 1.12 WP04 — TERRA D3 EVIDENCE ACQUISITION COMPLETE`

Still insufficient:

`RELEASE 1.12 WP04 — D3 FRESH STARTUP EVIDENCE: INCOMPLETE`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: UNDETERMINED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

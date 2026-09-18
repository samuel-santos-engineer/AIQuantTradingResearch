# GPT-5.6 Terra — Release 1.12 WP04 D3 Evidence Reacquisition Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: execute the narrowly approved D3 evidence reacquisition and exact mutation accounting.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**.

Issue: `#263`

Candidate commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Candidate image digest remains:

`sha256:1507a90c5fc8882bd633cdda549dd5a9282277b4e417645696b89adc79939998`

Accepted Luna reconciliation:

- a valid historical D3 JSON record exists at `2026-09-13T02:02:49.7400966Z`, but it is **not attributable** to the governed restart at `2026-09-13T02:25:43.2425842Z`;
- the interrupted-attempt classification is `CAPTURE_ONLY_INTERRUPTED_EVIDENCE_NOT_RETAINED`;
- the next decision is `R3`;
- repository change required: `NO`;
- the existing candidate/runtime was not disproven;
- one controlled evidence reacquisition is justified;
- the reacquisition must use a safer persisted-log workflow and must not depend on one long-lived tail session as the sole evidence channel.

Required starting markers:

`RELEASE 1.12 WP04 — INTERRUPTED D3 RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — INTERRUPTED D3 NEXT DECISION: R3`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

## 2. Mission

Perform exactly one D3 `initialize` evidence reacquisition using this sequence:

1. verify starting state;
2. enable only the minimum free filesystem/container logging;
3. apply temporary D3 settings;
4. restart exactly once;
5. wait a bounded interval without relying on a continuous tail session;
6. retrieve persisted container logs in a separate command;
7. prove attribution to the target restart using timestamp/context;
8. capture the D3 JSON or a concrete fresh Worker failure;
9. remove temporary settings;
10. restore logging exactly;
11. verify persistence settings and final state;
12. report exact mutations.

Do not perform restart-continuity or redeployment-continuity qualification here.

## 3. Pre-mutation verification

Verify:

- Web App = `Running`;
- plan = Linux F1 / Free;
- region = West Central US;
- exact candidate digest unchanged;
- `appCommandLine=""`;
- `WEBSITES_ENABLE_APP_SERVICE_STORAGE=true`;
- `Persistence__DatabasePath=/home/data/aiquant.db`;
- `Persistence__CreateParentDirectoryForInitialization=true`;
- `Worker__Mode` absent;
- `PersistentSqliteQualification__Phase` absent;
- registry credentials absent;
- filesystem/container logging currently restored to the expected disabled state.

If any critical drift exists, STOP.

Required:

`RELEASE 1.12 WP04 — D3 REACQUISITION PRECHECK: PASS`

## 4. Enable minimum free container logging

Enable only the minimum filesystem/container logging needed for Linux custom-container stdout/stderr capture.

Do not enable:

- Blob logging;
- Table logging;
- Application Insights;
- Log Analytics;
- paid diagnostics;
- unrelated HTTP tracing unless technically inseparable from the container-log switch.

Record exact pre-state and command exit code.

Required:

`RELEASE 1.12 WP04 — D3 REACQUISITION LOGGING ENABLED: PASS`

## 5. Apply temporary qualification settings

Set exactly:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
```

Do not alter persistence settings or image configuration.

Required:

`RELEASE 1.12 WP04 — D3 REACQUISITION TEMP SETTINGS APPLIED: PASS`

## 6. Perform exactly one restart

Immediately before restart, record a fresh UTC timestamp:

`WP04_D3_REACQUISITION_RESTART_UTC=<timestamp>`

Perform exactly one Web App restart.

Record:

- restart exit code;
- post-restart state;
- first confirmed `Running` timestamp.

Do not start `az webapp log tail` as the sole evidence path.

Required:

`RELEASE 1.12 WP04 — D3 REACQUISITION RESTART: PASS`

## 7. Bounded wait

Wait a bounded interval sufficient for:

- container startup;
- Worker one-shot qualification execution;
- persisted container log flush.

Use a finite, explicit wait/poll strategy.

Do not issue a second restart if the record is not immediately visible.

Record:

- poll attempts;
- elapsed time;
- final app state.

## 8. Retrieve persisted logs in a separate command

After the bounded wait, use a **separate** log-download/retrieval command to collect the persisted container stdout/stderr.

Capture:

- download/retrieval exit code;
- number of returned files;
- filenames/paths relevant to the target restart;
- timestamps.

The target evidence must be attributable to the new restart window.

Search specifically for:

- `RecordVersion`
- `PersistentSqliteQualification`
- `initialize`
- `/home/data/aiquant.db`
- Worker startup
- Worker stderr
- Worker exit/failure
- restart/container timestamps.

Required:

`RELEASE 1.12 WP04 — D3 REACQUISITION PERSISTED LOG DOWNLOAD: PASS`

## 9. Attribution gate

Do not credit any historical D3 record merely because it exists.

A qualifying record must be attributable to the new governed restart using one or more of:

- log-file timestamp;
- entry timestamp;
- restart-adjacent context;
- container instance/session context;
- other direct temporal evidence.

If the only record found is the historical one at `2026-09-13T02:02:49.7400966Z`, it does **not** pass.

Required:

`RELEASE 1.12 WP04 — D3 REACQUISITION TARGET ATTRIBUTION: PASS`

## 10. D3 outcome evaluation

### Success path

If a complete target-attributable D3 JSON record is found, capture it verbatim.

Expected factual fields:

```text
RecordVersion=1
Phase=initialize
DatabasePathIdentity=aiquant.db
SchemaVersion=4
JournalMode=delete
AcceptedEvidenceIdentity=<value>
AcceptedEvidenceCount=<value>
IntegrityCheck=ok
QuickCheck=ok
PersistenceContinuity=true
```

If present and valid, credit:

`RELEASE 1.12 WP04 — AZURE INITIAL PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE SCHEMA V4: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE DELETE JOURNAL: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE INTEGRITY: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE QUICK-CHECK: PASS`

Record:

- `AcceptedEvidenceIdentity`
- `AcceptedEvidenceCount`

as the continuity baseline for the next authority.

### Failure path

If no JSON exists but fresh target-attributable Worker stderr/startup/exception evidence exists, capture it and classify:

- `INVOCATION_CONFIGURATION_MISMATCH`
- `QUALIFICATION_PATH_NOT_REACHED`
- `QUALIFICATION_PRE_EMISSION_FAILURE`
- `APPLICATION_HOSTING_LIFECYCLE_MISMATCH`
- `OTHER_PROVEN_CAUSE`

Do not infer SQLite success/failure beyond the evidence.

### Still no target evidence

If neither a target-attributable JSON record nor a concrete fresh Worker failure is present after the safer persisted-log workflow:

- classify `INSUFFICIENT_EVIDENCE`;
- do not repeat again automatically;
- stop for Luna re-governance.

Required:

`RELEASE 1.12 WP04 — D3 REACQUISITION OUTCOME: <PASS|CONCRETE_FAILURE|INSUFFICIENT_EVIDENCE>`

## 11. Restore temporary state

After evidence retrieval/evaluation:

1. remove:
   - `Worker__Mode`
   - `PersistentSqliteQualification__Phase`
2. restore logging to the exact pre-run values;
3. re-read final configuration.

Verify:

```text
WEBSITES_ENABLE_APP_SERVICE_STORAGE=true
Persistence__DatabasePath=/home/data/aiquant.db
Persistence__CreateParentDirectoryForInitialization=true
Worker__Mode absent
PersistentSqliteQualification__Phase absent
```

and logging returns to the original disabled state.

Required:

`RELEASE 1.12 WP04 — D3 REACQUISITION TEMP SETTINGS REMOVED: PASS`

`RELEASE 1.12 WP04 — D3 REACQUISITION LOGGING RESTORED: PASS`

`RELEASE 1.12 WP04 — D3 REACQUISITION PERSISTENCE SETTINGS PRESERVED: PASS`

## 12. Mutation accounting

Report exact mutations.

Expected maximum:

- filesystem/container logging enable operations: exact count
- temporary D3 settings update operations: exact count
- Web App restarts: `1`
- temporary D3 settings removals: exact count
- logging restoration operations: exact count

Expected zero:

- repository mutations: `0`
- Git mutations: `0`
- Docker mutations: `0`
- GHCR mutations: `0`
- image reference changes: `0`
- registry credential changes: `0`
- SKU/resource changes: `0`
- provider mutations: `0`
- GitHub/lifecycle mutations: `0`

Required:

`RELEASE 1.12 WP04 — D3 REACQUISITION MUTATION AUDIT: PASS`

## 13. Stop conditions

STOP if:

- quota suspension returns;
- app cannot return to `Running`;
- candidate digest changes;
- persistence settings drift;
- paid logging is required;
- direct SQLite inspection would be required;
- logging cannot be restored exactly;
- a second restart would be required.

## 14. Return evidence

Return:

- precheck outputs;
- logging enable output;
- temporary-setting output;
- exact restart UTC;
- restart exit code;
- bounded-wait/poll evidence;
- persisted log-download output;
- target-attribution evidence;
- complete D3 JSON if found;
- otherwise exact fresh Worker failure;
- restoration outputs;
- exact mutation accounting;
- next-authority readiness.

Do not return secrets.

## 15. Terminal markers

### Successful reacquisition

`RELEASE 1.12 WP04 — D3 REACQUISITION PERSISTED LOG DOWNLOAD: PASS`

`RELEASE 1.12 WP04 — D3 REACQUISITION TARGET ATTRIBUTION: PASS`

`RELEASE 1.12 WP04 — D3 REACQUISITION OUTCOME: PASS`

`RELEASE 1.12 WP04 — D3 EMISSION CONFIRMED: PASS`

`RELEASE 1.12 WP04 — AZURE INITIAL PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — AZURE RESTART/REDEPLOY CONTINUITY AUTHORITY: READY`

`RELEASE 1.12 WP04 — D3 REACQUISITION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA D3 EVIDENCE REACQUISITION COMPLETE`

### Concrete failure

`RELEASE 1.12 WP04 — D3 REACQUISITION TARGET ATTRIBUTION: PASS`

`RELEASE 1.12 WP04 — D3 REACQUISITION OUTCOME: CONCRETE_FAILURE`

`RELEASE 1.12 WP04 — LUNA NARROW RECONCILIATION: REQUIRED`

`RELEASE 1.12 WP04 — TERRA D3 EVIDENCE REACQUISITION COMPLETE`

### Still insufficient

`RELEASE 1.12 WP04 — D3 REACQUISITION OUTCOME: INSUFFICIENT_EVIDENCE`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: UNDETERMINED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

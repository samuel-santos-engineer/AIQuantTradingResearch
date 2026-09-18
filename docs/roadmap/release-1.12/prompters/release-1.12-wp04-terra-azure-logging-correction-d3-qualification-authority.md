# GPT-5.6 Terra — Release 1.12 WP04 Azure Logging-Correction D3 Qualification Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: execute the narrowly approved Azure logging correction, one D3 qualification attempt, evidence capture, restoration, and mutation accounting.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**.

Issue: `#263`

Candidate commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Candidate image digest:

`sha256:1507a90c5fc8882bd633cdda549dd5a9282277b4e417645696b89adc79939998`

Accepted Luna reconciliation:

- Azure application/container filesystem logging is currently disabled.
- Existing downloaded archive is limited to platform/startup diagnostics and cannot prove current container stdout/stderr.
- `container/entrypoint.sh` is the image `ENTRYPOINT`.
- Worker and Streamlit inherit stdout/stderr without redirection.
- A successful one-shot D3 Worker can emit one JSON record to stdout and exit `0` while Streamlit remains alive.
- `appCommandLine=""`; no Azure startup override bypasses the entrypoint.
- D3 classification: `AZURE_CONTAINER_LOGGING_DISABLED`.
- Remediation decision: `L1`.
- Repository change required: `NO`.

Required starting markers:

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT REMEDIATION DECISION: L1`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

## 2. Mission

Temporarily enable only the minimum free App Service filesystem/container logging needed to capture current container stdout/stderr, then perform exactly one controlled D3 `initialize` attempt, capture fresh evidence, restore all temporary settings, and stop.

This authority does **not** authorize:

- repository mutation;
- image rebuild/publish;
- image reference change;
- redeployment continuity;
- WP04 PR creation;
- merge;
- issue/project/milestone lifecycle changes;
- paid logging services;
- direct SQLite inspection.

## 3. Pre-mutation checks

Before any mutation, verify:

- Web App state = `Running`;
- plan = Linux F1 / Free;
- region = West Central US;
- exact candidate digest remains configured;
- `appCommandLine=""`;
- persistent storage remains enabled;
- `Persistence__DatabasePath=/home/data/aiquant.db`;
- `Persistence__CreateParentDirectoryForInitialization=true`;
- registry username/password absent;
- current logging state still matches the reconciled disabled state.

If any critical state drifted, STOP.

Required:

`RELEASE 1.12 WP04 — LOGGING-CORRECTION PRECHECK: PASS`

## 4. Enable only free filesystem/container logging

Enable only the minimum App Service filesystem/container logging setting needed for Linux custom-container stdout/stderr capture.

Constraints:

- filesystem/local only;
- no Azure Blob Storage;
- no Azure Table Storage;
- no Application Insights;
- no Log Analytics;
- no paid service;
- no unrelated HTTP/detailed-error/failed-request tracing unless specifically required by the container log mechanism;
- preserve exact pre-run logging values for restoration.

Capture exact command and exit code.

Required:

`RELEASE 1.12 WP04 — FREE CONTAINER LOGGING ENABLED: PASS`

## 5. Apply temporary D3 settings

Temporarily apply exactly:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__Phase=initialize
```

Do not alter:

```text
WEBSITES_ENABLE_APP_SERVICE_STORAGE=true
Persistence__DatabasePath=/home/data/aiquant.db
Persistence__CreateParentDirectoryForInitialization=true
```

Required:

`RELEASE 1.12 WP04 — D3 TEMP SETTINGS APPLIED: PASS`

## 6. Execute exactly one controlled restart

Perform exactly one Web App restart.

Immediately capture fresh container stdout/stderr using the now-enabled surface.

Use `az webapp log tail`, freshly downloaded container logs, or both, provided the evidence is attributable to this restart.

Capture:

- restart timestamp;
- restart exit code;
- post-restart state;
- log-tail/download exit codes;
- fresh Worker stdout/stderr.

Do not perform another restart automatically.

Required:

`RELEASE 1.12 WP04 — D3 CONTROLLED RESTART: PASS`

## 7. D3 evidence gate

### Success path

If a complete D3 JSON qualification record is emitted, capture it verbatim and verify only the fields actually present.

Expected qualification facts include:

- phase = initialize;
- database path identity = `/home/data/aiquant.db`;
- schema version = `4`;
- journal mode = `delete`;
- integrity = `ok`;
- quick-check = `ok`;
- accepted evidence identity/count.

If the record proves those values:

`RELEASE 1.12 WP04 — AZURE INITIAL PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE SCHEMA V4: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE DELETE JOURNAL: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE INTEGRITY: PASS`

`RELEASE 1.12 WP04 — AZURE SQLITE QUICK-CHECK: PASS`

Record the accepted evidence identity/count as the continuity baseline.

### Failure path

If no JSON is emitted, capture the exact fresh stderr/startup/exception/process evidence from this attempt.

Classify one of:

- `INVOCATION_CONFIGURATION_MISMATCH`
- `QUALIFICATION_PATH_NOT_REACHED`
- `QUALIFICATION_PRE_EMISSION_FAILURE`
- `APPLICATION_HOSTING_LIFECYCLE_MISMATCH`
- `OTHER_PROVEN_CAUSE`
- `INSUFFICIENT_EVIDENCE`

Do not infer SQLite success or failure without application-owned evidence.

Required in either case:

`RELEASE 1.12 WP04 — D3 LOGGING-CORRECTED EVIDENCE CAPTURE: PASS`

## 8. Restore temporary state

After evidence capture:

1. remove:
   - `Worker__Mode`
   - `PersistentSqliteQualification__Phase`
2. restore logging configuration to the exact pre-run state;
3. re-read settings and logging configuration.

Verify:

```text
WEBSITES_ENABLE_APP_SERVICE_STORAGE=true
Persistence__DatabasePath=/home/data/aiquant.db
Persistence__CreateParentDirectoryForInitialization=true
```

and temporary D3 settings are absent.

Required:

`RELEASE 1.12 WP04 — D3 TEMP SETTINGS REMOVED: PASS`

`RELEASE 1.12 WP04 — AZURE LOGGING SETTINGS RESTORED: PASS`

`RELEASE 1.12 WP04 — PERSISTENCE SETTINGS PRESERVED: PASS`

## 9. Decision boundary

### If valid D3 JSON passes

Do not perform restart continuity or redeployment continuity in this authority.

Return:

`RELEASE 1.12 WP04 — D3 EMISSION CONFIRMED: PASS`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — AZURE RESTART/REDEPLOY CONTINUITY AUTHORITY: READY`

### If a concrete runtime/application defect is proven

Return the exact defect and minimum next scope.

Do not modify source.

If source remediation is needed:

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES`

`RELEASE 1.12 WP04 — LUNA NARROW REMEDIATION RE-GOVERNANCE: REQUIRED`

### If evidence is still insufficient

Do not repeat the attempt automatically.

Return exactly what remains missing.

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: UNDETERMINED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

## 10. Mutation accounting

Report exact mutations.

Expected maximum:

- free filesystem/container logging enable operation(s): exact count
- temporary D3 app-setting update operation(s): exact count
- Web App restarts: `1`
- temporary D3 setting removals: exact count
- logging restoration operation(s): exact count

Expected zero:

- repository mutations: `0`
- Git mutations: `0`
- Docker mutations: `0`
- GHCR mutations: `0`
- image reference changes: `0`
- registry credential changes: `0`
- SKU changes: `0`
- resource creates/deletes: `0`
- provider mutations: `0`
- GitHub/lifecycle mutations: `0`

Required:

`RELEASE 1.12 WP04 — LOGGING-CORRECTION QUALIFICATION MUTATION AUDIT: PASS`

## 11. Stop conditions

STOP immediately if:

- F1 quota suspension returns;
- Web App cannot reach `Running`;
- exact candidate digest changes;
- persistence settings drift;
- paid logging/storage would be required;
- direct SQL inspection would be required;
- logging cannot be restored;
- the run would require a second restart.

## 12. Return evidence

Return:

- precheck output;
- exact pre-run logging state;
- logging enable command and exit code;
- D3 temporary-setting command and exit code;
- restart exit code and timestamp;
- post-restart state;
- fresh log-tail/download evidence;
- complete D3 JSON if emitted;
- otherwise exact fresh failure evidence and classification;
- D3/settings removal output;
- logging restoration output;
- persistence re-verification;
- exact mutation accounting.

Do not return secrets.

## 13. Terminal markers

### Successful initial D3 qualification

`RELEASE 1.12 WP04 — D3 LOGGING-CORRECTED EVIDENCE CAPTURE: PASS`

`RELEASE 1.12 WP04 — D3 EMISSION CONFIRMED: PASS`

`RELEASE 1.12 WP04 — AZURE INITIAL PERSISTENCE QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — AZURE RESTART/REDEPLOY CONTINUITY AUTHORITY: READY`

`RELEASE 1.12 WP04 — LOGGING-CORRECTION QUALIFICATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — TERRA LOGGING-CORRECTION QUALIFICATION COMPLETE`

### Concrete source/runtime defect

`RELEASE 1.12 WP04 — D3 LOGGING-CORRECTED EVIDENCE CAPTURE: PASS`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES`

`RELEASE 1.12 WP04 — LUNA NARROW REMEDIATION RE-GOVERNANCE: REQUIRED`

`RELEASE 1.12 WP04 — TERRA LOGGING-CORRECTION QUALIFICATION COMPLETE`

### Still insufficient

`RELEASE 1.12 WP04 — D3 LOGGING-CORRECTED EVIDENCE CAPTURE: INCOMPLETE`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: UNDETERMINED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

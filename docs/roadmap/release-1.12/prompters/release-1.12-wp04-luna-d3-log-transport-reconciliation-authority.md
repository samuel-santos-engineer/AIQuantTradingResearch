# GPT-5.6 Luna — Release 1.12 WP04 Azure D3 Log-Transport Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: contract, architecture, reconciliation, failure classification, acceptance criteria, and governance.
- **GPT-5.6 Terra** — implementation, validation execution, and approved Azure/Git/GitHub mutations only after Luna authorization.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Accepted state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**.

Issue: `#263`

Candidate commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Candidate image digest:

`sha256:1507a90c5fc8882bd633cdda549dd5a9282277b4e417645696b89adc79939998`

Two governed Azure qualification attempts have now failed to produce any fresh D3 JSON or fresh Worker qualification output, despite:

- Web App `Running`;
- Linux F1 / Free;
- West Central US;
- exact candidate digest configured;
- persistent `/home` and `/home/data/aiquant.db` settings intact;
- temporary D3 settings applied successfully;
- restart succeeded;
- temporary settings restored;
- no repository/GHCR/SKU/resource mutation.

Latest accepted markers:

`RELEASE 1.12 WP04 — D3 FRESH STARTUP EVIDENCE: INCOMPLETE`

`RELEASE 1.12 WP04 — D3 FRESH STARTUP CLASSIFICATION: INSUFFICIENT_EVIDENCE`

`RELEASE 1.12 WP04 — D3 REMEDIATION DECISION: D`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: UNDETERMINED`

Do **not** run a third qualification restart under the current evidence state.

## 2. Mission

Perform a **zero-mutation Luna reconciliation** of the Azure/App Service log transport and container child-process output path.

The objective is to determine whether the missing D3 record is most likely caused by:

1. Azure container logging not being enabled/configured for the relevant stdout/stderr stream;
2. log retrieval using the wrong Azure surface/archive;
3. `container/entrypoint.sh` not preserving child stdout/stderr to PID 1 / container output;
4. the Worker process not being started in the effective runtime path;
5. an App Service hosting lifecycle mismatch;
6. or a still-unproven application execution defect.

This authority does not authorize repository edits, Azure configuration changes, restarts, redeployments, or qualification attempts.

## 3. Read-only Azure logging-state reconciliation

Read the existing Web App logging configuration using read-only Azure CLI/API commands.

Establish, where exposed:

- whether container/Docker logging is enabled;
- whether filesystem/container log collection is enabled;
- retention/quota settings if relevant;
- whether application logging is enabled or applicable for this Linux custom-container path;
- whether the SCM/Kudu log endpoint is enabled/reachable;
- whether `az webapp log download` is expected to include current container stdout/stderr;
- whether `az webapp log tail` is expected to expose the same stream;
- whether the archive being downloaded is stale/static or refreshed on demand.

Do not change log settings.

Required:

`RELEASE 1.12 WP04 — AZURE LOGGING STATE RECONCILIATION: PASS`

## 4. Entrypoint stdout/stderr transport reconciliation

Inspect the exact candidate source at commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Review at minimum:

- `Dockerfile`
- `container/entrypoint.sh`
- Worker launch command
- Streamlit launch command
- process supervision/wait behavior
- any redirections (`>`, `2>`, `&>`, pipes, `nohup`, `tee`, subshells)
- backgrounding behavior (`&`)
- whether stdout/stderr remain inherited by PID 1
- whether Worker exit is observed/reaped
- whether Worker can emit one-shot JSON and exit while Streamlit keeps the container alive.

Determine whether a successful D3 JSON write to Worker stdout should be visible in the container log stream without any source change.

Required:

`RELEASE 1.12 WP04 — ENTRYPOINT STDOUT STDERR TRANSPORT RECONCILIATION: PASS`

## 5. Effective runtime path reconciliation

Read-only reconcile the effective App Service container startup path:

- confirm whether App Service uses the image's configured ENTRYPOINT/CMD or an overriding startup command;
- confirm no Azure startup-command override bypasses `container/entrypoint.sh`;
- confirm no app setting or platform setting changes the Worker invocation path;
- confirm the exact candidate image remains configured.

Required:

`RELEASE 1.12 WP04 — AZURE EFFECTIVE STARTUP PATH RECONCILIATION: PASS`

## 6. Determine the minimum observable surface

Identify the **narrowest existing Azure-accessible surface** capable of proving one of:

- the Worker process was launched in `PersistentSqliteQualification` mode;
- the D3 JSON record reached stdout;
- the Worker emitted stderr/exception output;
- the Worker exited with code `0` or `1`.

Candidate surfaces may include, only if already available read-only:

- current container stdout/stderr stream;
- App Service log stream;
- downloaded container logs;
- SCM/Kudu diagnostics/log files;
- platform container events.

Do not use:

- direct SQLite inspection;
- direct database shell/Python queries;
- new application telemetry code;
- paid services.

Required:

`RELEASE 1.12 WP04 — MINIMUM D3 OBSERVABILITY SURFACE IDENTIFIED: PASS`

## 7. Failure classification

Select exactly one primary classification:

- `AZURE_CONTAINER_LOGGING_DISABLED`
- `AZURE_LOG_RETRIEVAL_SURFACE_MISMATCH`
- `ENTRYPOINT_CHILD_OUTPUT_NOT_PROPAGATED`
- `STARTUP_COMMAND_BYPASSES_ENTRYPOINT`
- `APPLICATION_HOSTING_LIFECYCLE_MISMATCH`
- `APPLICATION_EXECUTION_DEFECT_NOT_YET_PROVEN`
- `OTHER_PROVEN_CAUSE`
- `INSUFFICIENT_EVIDENCE`

Required:

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT CLASSIFICATION: <classification>`

## 8. Remediation decision

Choose exactly one:

### L1 — No-code Azure logging correction

Choose only if a specific existing App Service logging setting is proven to be preventing capture and a temporary/free logging configuration change would expose the existing container stdout/stderr.

Repository change required: `NO`.

A separate Terra authority must perform the Azure logging mutation and one controlled D3 attempt.

### L2 — Retrieval-only correction

Choose only if logging already captures the data but the previous command/archive surface was wrong or stale.

Repository change required: `NO`.

A separate Terra authority may perform one controlled D3 attempt using the corrected capture surface.

### L3 — Narrow entrypoint/application remediation

Choose only if source inspection proves Worker stdout/stderr is not propagated, the runtime path bypasses D3 execution, or the existing candidate cannot expose D3 truthfully under App Service.

Repository change required: `YES`.

Identify the exact defect and minimum path delta, but do not implement.

### L4 — Still insufficient

Use only if the read-only evidence cannot establish any of the above.

Repository change required: `UNDETERMINED`.

State the exact missing evidence and why another restart would not resolve it without a new observability mechanism.

Required:

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT REMEDIATION DECISION: <L1|L2|L3|L4>`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO|UNDETERMINED>`

## 9. Architecture constraints

Any recommended next action must preserve:

- .NET ownership of persistence semantics;
- Infrastructure SQLite/provider mechanics;
- Worker outer composition;
- Python/Streamlit no SQLite ownership;
- schema v4;
- SQLite DELETE journal;
- E2-A bounded root storage preparation and non-root long-running runtime;
- no direct SQL deployment bypass;
- no Azure-specific second persistence implementation;
- no Azure SQL;
- no paid resources;
- no registry credentials merely for observability;
- exact candidate provenance.

## 10. Mutation audit

This authority permits **zero mutations**.

Required:

```text
Repository mutations: 0
Git mutations: 0
Docker mutations: 0
GHCR mutations: 0
Azure mutations: 0
Provider mutations: 0
GitHub/lifecycle mutations: 0
```

Required:

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT RECONCILIATION MUTATION AUDIT: PASS`

## 11. Return evidence

Return:

- exact Azure logging state;
- exact entrypoint stdout/stderr behavior;
- exact effective App Service startup path;
- minimum viable existing observability surface;
- primary classification;
- remediation decision;
- repository-change determination;
- exact next-authority scope.

Do not return secrets.

## 12. Terminal markers

If no-code Azure logging correction is proven:

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT REMEDIATION DECISION: L1`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — TERRA LOGGING-CORRECTION QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA D3 LOG-TRANSPORT RECONCILIATION COMPLETE`

If retrieval-only correction is proven:

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT REMEDIATION DECISION: L2`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — TERRA CORRECTED-CAPTURE QUALIFICATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA D3 LOG-TRANSPORT RECONCILIATION COMPLETE`

If code remediation is proven:

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT REMEDIATION DECISION: L3`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES`

`RELEASE 1.12 WP04 — TERRA NARROW REMEDIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA D3 LOG-TRANSPORT RECONCILIATION COMPLETE`

If still insufficient:

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT RECONCILIATION: INCOMPLETE`

`RELEASE 1.12 WP04 — D3 LOG-TRANSPORT REMEDIATION DECISION: L4`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

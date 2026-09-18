# GPT-5.6 Luna — Release 1.12 WP04 Azure D3 Evidence-Channel Re-Governance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: architecture/governance reconciliation, acceptance-boundary decision, and remediation selection.
- **GPT-5.6 Terra** — implementation/validation/Azure/Git/GitHub mutations only after Luna authorizes a specific path.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Accepted state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**, issue `#263`.

Candidate commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Candidate image digest:

`sha256:1507a90c5fc8882bd633cdda549dd5a9282277b4e417645696b89adc79939998`

A valid application-owned D3 record is known to have been emitted by this candidate at:

`2026-09-13T02:02:49.7400966Z`

Recovered record:

```json
{"RecordVersion":1,"Phase":"initialize","DatabasePathIdentity":"aiquant.db","SchemaVersion":4,"JournalMode":"delete","AcceptedEvidenceIdentity":"WP04-QUALIFICATION:638397504000000000","AcceptedEvidenceCount":1,"IntegrityCheck":"ok","QuickCheck":"ok","PersistenceContinuity":true}
```

That record proves that the candidate can execute the application-owned D3 path and emit the expected qualification payload.

However, repeated later governed Azure attempts could not obtain **target-attributable** container stdout/stderr from App Service, including after temporarily enabling free filesystem/container logging and using persisted log download.

Latest governed reacquisition:

```text
WP04_D3_REACQUISITION_RESTART_UTC=2026-09-13T02:37:02.4383383Z
WP04_REACQ_RESTART_EXIT_CODE=0
state=Running
availabilityState=Normal
WP04_REACQ_LOG_DOWNLOAD_EXIT_CODE=0
WP04_REACQ_LOG_FILE_COUNT=36
WP04_REACQ_RELEVANT_LINE_COUNT=34
```

No target-attributable D3 JSON, Worker startup, Worker exit, or Worker error was retained/retrieved.

Restoration passed. No repository/Git/Docker/GHCR/image/SKU/resource/provider/GitHub mutation occurred.

Accepted markers:

`RELEASE 1.12 WP04 — D3 REACQUISITION PERSISTED LOG DOWNLOAD: PASS`

`RELEASE 1.12 WP04 — D3 REACQUISITION OUTCOME: INSUFFICIENT_EVIDENCE`

`RELEASE 1.12 WP04 — D3 REACQUISITION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

## 2. Governance conclusion to test

Do **not** authorize another blind restart/logging retry.

The current evidence separates two facts:

1. the candidate's D3 application-owned qualification path is functional;
2. Azure App Service's current stdout/stderr retrieval surface is not dependable enough to serve as the sole acceptance evidence channel for restart/redeploy continuity.

This authority must determine whether WP04 should re-govern the **qualification evidence transport** while preserving the existing persistence architecture.

## 3. Mission

Perform a **zero-mutation Luna architecture/acceptance reconciliation** and select exactly one durable next path.

The preferred problem statement is:

> The persistence implementation is not disproven. The acceptance evidence transport is inadequate for deterministic Azure qualification.

Do not change schema, SQLite ownership, provider behavior, or persistence semantics merely to compensate for Azure log retrieval.

## 4. Reconcile the historical D3 record

Determine precisely what the recovered `02:02:49Z` D3 record can and cannot prove.

It may prove, if candidate provenance is intact:

- the candidate image can execute D3;
- schema version `4`;
- journal mode `delete`;
- integrity `ok`;
- quick-check `ok`;
- application-owned evidence identity/count creation;
- D3 output contract validity.

It does **not** by itself prove:

- attribution to any later governed restart;
- restart continuity;
- redeployment continuity;
- persistence continuity across a specific governed boundary.

Required:

`RELEASE 1.12 WP04 — HISTORICAL D3 EVIDENCE SCOPE RECONCILIATION: PASS`

## 5. Evaluate evidence-channel options

Evaluate these options against correctness, auditability, narrowness, and architecture preservation.

### Option E1 — Continue relying on App Service stdout/stderr

Select only if there is a newly proven deterministic Azure-native capture path not already exhausted.

Repeated generic log retries are forbidden.

### Option E2 — Application-owned durable qualification artifact

Introduce a narrow application-owned qualification evidence file written by the Worker in addition to stdout.

Candidate design constraints:

- D3 remains application-owned;
- no direct SQL diagnostic consumer is introduced;
- JSON payload remains the canonical D3 record;
- stdout emission remains intact;
- optional configured evidence-output path;
- atomic file replacement/write;
- evidence file may live under persistent `/home`, e.g. a dedicated qualification/evidence directory;
- include an invocation/run identifier supplied by configuration so a record can be attributed to a specific governed attempt;
- include phase (`initialize` / `reopen`);
- never write secrets;
- failure remains explicit;
- normal runtime is unaffected when qualification mode is inactive.

The evidence artifact is **qualification output**, not a new persistence implementation.

### Option E3 — Azure/SCM direct process invocation

Evaluate whether invoking the existing Worker qualification command directly inside the running container through an Azure-supported console/SSH surface would preserve the required runtime and persistence boundary.

Reject if it bypasses the deployment lifecycle being qualified or cannot prove restart/redeploy continuity.

### Option E4 — Re-scope acceptance to historical capability evidence only

Select only if Luna can justify that restart/redeploy continuity is no longer required by WP04's accepted contract.

Because restart/redeploy persistence was an explicit acceptance gate, this option should be rejected unless the governing contract itself is intentionally amended with a strong rationale.

## 6. Required selection

Select exactly one:

- `E1_EXISTING_AZURE_CAPTURE`
- `E2_DURABLE_APPLICATION_EVIDENCE_ARTIFACT`
- `E3_AZURE_DIRECT_WORKER_INVOCATION`
- `E4_ACCEPTANCE_SCOPE_AMENDMENT`
- `NO_VALID_PATH`

Required:

`RELEASE 1.12 WP04 — D3 EVIDENCE CHANNEL SELECTION: <selection>`

## 7. Preferred architecture if E2 is selected

If evidence supports E2, define the minimum implementation contract.

At minimum reconcile whether these elements are needed:

1. **Configuration**
   - optional evidence output path;
   - explicit qualification run ID / attempt ID.

2. **Worker qualification execution**
   - produce one canonical record object;
   - serialize once;
   - write same JSON to stdout;
   - optionally persist same JSON atomically to configured evidence path;
   - surface write failure as qualification failure, not silent success.

3. **Attribution**
   - persisted record must contain the governed run ID or an equivalent direct-attribution field;
   - timestamp may be included but must not be the sole attribution mechanism.

4. **Storage**
   - Azure candidate path must be under persistent `/home`;
   - evidence artifact must not replace SQLite persistence;
   - no world-writable directory;
   - E2-A non-root ownership boundary remains intact.

5. **Tests**
   - stdout contract preserved;
   - file artifact exact-content parity with stdout payload;
   - atomic overwrite/replacement behavior;
   - run-ID attribution;
   - no artifact when output path is unset;
   - failure behavior for invalid/unwritable output path.

6. **Azure validation**
   - initialize run writes attributable evidence file;
   - one restart/reopen run writes new attributable record and preserves accepted evidence identity/count;
   - one same-digest redeployment/reopen boundary writes new attributable record and preserves identity/count;
   - retrieve evidence file through a supported Azure file/SCM surface without querying SQLite directly.

## 8. Path-governance decision

If source remediation is selected, identify the **minimum exact repository path delta**.

Prefer modifying existing WP04 paths where possible.

Potential candidates to evaluate, not automatically authorize:

- `src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs`
- `src/AIQuantTradingResearch.Worker/Program.cs`
- a narrowly scoped configuration type only if required
- existing/new Worker test file(s) only as needed
- existing WP04 verification script only if it must retrieve the durable artifact

Do not widen to README, schema, Domain, unrelated Application services, Streamlit/Python, WP03 deployment scripts, or Initiative-1.11 scripts unless separately proven necessary.

Required if code change is selected:

`RELEASE 1.12 WP04 — NARROW REMEDIATION PATH SET: <exact paths>`

## 9. Acceptance-boundary preservation

Unless explicitly amended, preserve all original WP04 acceptance requirements:

- schema v4;
- DELETE journal;
- integrity/quick-check;
- application-owned qualification;
- governed data/evidence identity;
- restart persistence;
- redeployment persistence;
- no direct SQL deployment bypass;
- strict-zero-cost boundary;
- candidate provenance;
- no production claim.

Required:

`RELEASE 1.12 WP04 — ORIGINAL PERSISTENCE ACCEPTANCE BOUNDARY: PRESERVED`

## 10. Decision outputs

Choose one remediation class:

### G1 — No code change
Use only if E1 or E3 is proven sufficient and architecture-valid.

### G2 — Narrow evidence-channel code remediation
Use if E2 is selected.

Repository change required: `YES`.

This authority must define the exact minimum path set and acceptance tests, but **must not implement**.

### G3 — Contract amendment
Use only if E4 is intentionally selected.

Requires explicit acceptance-contract rationale.

### G4 — No valid path
Stop and identify the unresolved blocker.

Required:

`RELEASE 1.12 WP04 — EVIDENCE CHANNEL REMEDIATION DECISION: <G1|G2|G3|G4>`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO|UNDETERMINED>`

## 11. Mutation audit

This authority permits **zero mutations**:

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

`RELEASE 1.12 WP04 — EVIDENCE CHANNEL RE-GOVERNANCE MUTATION AUDIT: PASS`

## 12. Return evidence

Return:

- historical D3 evidence scope;
- evaluation of E1–E4;
- selected evidence channel;
- remediation class;
- exact repository-change determination;
- if G2: exact path allowlist, implementation contract, test contract, and Azure validation contract;
- exact next Terra authority scope.

Do not return secrets.

## 13. Terminal markers

### If E2 / G2 is selected

`RELEASE 1.12 WP04 — D3 EVIDENCE CHANNEL RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — D3 EVIDENCE CHANNEL SELECTION: E2_DURABLE_APPLICATION_EVIDENCE_ARTIFACT`

`RELEASE 1.12 WP04 — EVIDENCE CHANNEL REMEDIATION DECISION: G2`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES`

`RELEASE 1.12 WP04 — TERRA NARROW EVIDENCE-CHANNEL REMEDIATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA D3 EVIDENCE-CHANNEL RE-GOVERNANCE COMPLETE`

### If a no-code path is selected

`RELEASE 1.12 WP04 — D3 EVIDENCE CHANNEL RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — EVIDENCE CHANNEL REMEDIATION DECISION: G1`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — TERRA QUALIFICATION CONTINUATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA D3 EVIDENCE-CHANNEL RE-GOVERNANCE COMPLETE`

### If blocked

`RELEASE 1.12 WP04 — D3 EVIDENCE CHANNEL RECONCILIATION: INCOMPLETE`

`RELEASE 1.12 WP04 — EVIDENCE CHANNEL REMEDIATION DECISION: G4`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

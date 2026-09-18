# GPT-5.6 Terra — Release 1.12 WP04 Narrow Durable D3 Evidence-Channel Remediation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: implement and locally validate the Luna-approved narrow evidence-channel remediation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**, issue `#263`.

Current candidate commit:

`ef4a5caf4768ab82c66f0e539d92c1631761b500`

Current deployed candidate digest:

`sha256:1507a90c5fc8882bd633cdda549dd5a9282277b4e417645696b89adc79939998`

Accepted Luna decision:

`RELEASE 1.12 WP04 — D3 EVIDENCE CHANNEL SELECTION: E2_DURABLE_APPLICATION_EVIDENCE_ARTIFACT`

`RELEASE 1.12 WP04 — EVIDENCE CHANNEL REMEDIATION DECISION: G2`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES`

`RELEASE 1.12 WP04 — ORIGINAL PERSISTENCE ACCEPTANCE BOUNDARY: PRESERVED`

The historical D3 record proves candidate D3 capability but does not satisfy later restart/redeployment attribution.

## 2. Exact path allowlist

Only these four tracked paths may be modified:

```text
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

No other tracked path is authorized.

Before editing, verify all four paths exist at the current candidate lineage. If the verification script path does not exist exactly as governed, STOP; do not substitute another path.

Explicit deny set includes:

- `README.md`
- `Dockerfile`
- `container/entrypoint.sh`
- schema/bootstrap files
- Domain
- unrelated Application services
- Python/Streamlit
- WP03 scripts
- Initiative-1.11 scripts
- package/dependency files
- project files unless a compile requirement is separately re-governed.

Untracked operator helper files and `prompters/` remain untouched and excluded.

## 3. Implementation contract

Implement the durable D3 evidence channel without changing persistence semantics.

### Configuration

Support optional configuration for:

- qualification evidence output path;
- explicit qualification run ID.

Use configuration names consistent with the existing `PersistentSqliteQualification` namespace and repository conventions.

When evidence output path is unset, preserve existing behavior: stdout D3 record remains available and no evidence file is created.

### Canonical record

Produce one canonical qualification record object.

Add the governed run identifier to the canonical record so the artifact can be directly attributed to one execution. Preserve existing fields and semantics.

Serialize the canonical record exactly once for success output.

The same serialized JSON payload must be:

1. written to stdout;
2. when configured, written atomically to the evidence artifact.

Do not create two independently serialized payloads that could diverge.

### Durable evidence artifact

When configured:

- path must be treated as an output artifact path, not as a database path;
- Azure target will be under persistent `/home`;
- create only the necessary parent for this explicitly configured output;
- write atomically using temporary-file + replace/move semantics appropriate to the existing runtime;
- do not leave a partially written canonical artifact;
- overwrite/replace a prior artifact deterministically;
- evidence write failure must cause qualification failure;
- failure must be visible through the existing stderr/nonzero-exit contract;
- do not write secrets.

This artifact is qualification evidence only. It must not become a second persistence implementation or SQLite consumer.

## 4. Program/composition contract

`Program.cs` may be changed only as necessary to bind/pass the new qualification evidence settings into the existing qualification execution.

Do not alter normal Worker ownership/composition or unrelated modes.

Qualification behavior must remain inactive unless:

`Worker__Mode=PersistentSqliteQualification`

## 5. Verification-script contract

Modify only the governed WP04 Web App verification script as necessary to support the durable evidence artifact.

The script may:

- configure an explicit run ID;
- configure the evidence-output path under persistent `/home`;
- retrieve/read the resulting JSON artifact through a supported Azure/SCM/file surface;
- verify run attribution and expected D3 fields;
- remove temporary qualification settings after each governed attempt.

The script must not:

- query SQLite directly;
- use Python/sqlite3 as a diagnostic bypass;
- change schema;
- add paid infrastructure;
- change SKU;
- add registry credentials;
- silently accept a historical run ID.

## 6. Test contract

Add/modify tests within the single authorized test path to prove at minimum:

1. existing stdout qualification contract remains valid;
2. stdout and evidence-file payload are byte-for-byte or canonical-string identical;
3. configured run ID is present and exact;
4. evidence artifact is absent when output path is unset;
5. configured artifact is written successfully;
6. repeated write atomically replaces the prior artifact;
7. invalid/unwritable evidence path yields qualification failure;
8. existing schema v4 / DELETE / integrity / quick-check / evidence semantics remain intact;
9. no direct-SQL diagnostic ownership is introduced.

Do not weaken existing tests.

## 7. Local validation

Run the repository's established validation gates appropriate to the changed paths.

At minimum:

- `dotnet build` with existing signing contract intact;
- Domain tests;
- Application tests;
- Architecture tests;
- Infrastructure tests;
- targeted D3/persistence tests;
- PowerShell parse/validation for the modified script;
- Gitleaks;
- exact diff/path audit.

Use the current canonical expected test counts only if the repository itself establishes them after the change; do not hard-code stale totals as success.

Signing bypass is forbidden.

Required:

`RELEASE 1.12 WP04 — DURABLE D3 REMEDIATION BUILD: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 REMEDIATION TESTS: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 SCRIPT VALIDATION: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 GITLEAKS: PASS`

## 8. Local functional qualification

Using an isolated local temporary evidence path, prove:

- initialize qualification emits stdout JSON;
- evidence file contains the same JSON;
- run ID matches;
- schema = `4`;
- journal = `delete`;
- integrity = `ok`;
- quick-check = `ok`;
- evidence identity/count is present;
- atomic replacement works on a second run;
- no stale temporary evidence file remains.

Do not use this local run as Azure restart/redeploy acceptance evidence.

Required:

`RELEASE 1.12 WP04 — DURABLE D3 LOCAL ARTIFACT QUALIFICATION: PASS`

## 9. Exact diff audit

Before any publication mutation, prove:

- tracked diff is exactly the four-path allowlist;
- no README mutation;
- no schema migration;
- no package/dependency mutation;
- no Docker/entrypoint mutation;
- no unrelated test weakening;
- untracked helper and `prompters/` remain excluded.

Required:

`RELEASE 1.12 WP04 — DURABLE D3 FOUR-PATH PAYLOAD: PASS`

## 10. Publication boundary

This authority permits implementation and local validation only.

It does **not** authorize:

- staging/commit;
- push;
- PR creation;
- GHCR image publication;
- Azure image update;
- Azure deployment/restart qualification;
- issue closure;
- Project #2 status mutation;
- milestone mutation.

After all local gates pass, STOP and return evidence for a separate candidate-publication/Azure-validation authority.

## 11. Mutation accounting

Report exact actual mutations.

Expected repository mutation set:

```text
Tracked paths modified: exactly 4
```

Expected zero:

```text
Git staging/commit/push mutations: 0
Docker image/volume/container mutations: 0
GHCR mutations: 0
Azure mutations: 0
Provider mutations: 0
GitHub/lifecycle mutations: 0
Schema migrations: 0
Package/dependency mutations: 0
```

Required:

`RELEASE 1.12 WP04 — DURABLE D3 REMEDIATION MUTATION AUDIT: PASS`

## 12. Stop conditions

STOP if:

- a fifth tracked path is required;
- the governed verification script path does not exist;
- schema migration appears necessary;
- package/dependency change appears necessary;
- Docker/entrypoint change appears necessary;
- direct SQLite diagnostic access appears necessary;
- signing bypass appears necessary;
- architecture tests expose an ownership violation;
- local validation cannot pass without widening scope.

Do not self-authorize a wider path set.

## 13. Return evidence

Return:

- exact four-path diff summary;
- configuration names chosen;
- canonical record/run-ID contract;
- atomic-write implementation behavior;
- all build/test/script/Gitleaks outputs and exit codes;
- local stdout/artifact parity evidence;
- exact mutation accounting;
- any blocker text.

Do not return secrets.

## 14. Terminal markers

On full local success:

`RELEASE 1.12 WP04 — DURABLE D3 EVIDENCE-CHANNEL REMEDIATION: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 FOUR-PATH PAYLOAD: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 LOCAL ARTIFACT QUALIFICATION: PASS`

`RELEASE 1.12 WP04 — DURABLE D3 REMEDIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — CANDIDATE PUBLICATION/AZURE VALIDATION AUTHORITY: READY`

`RELEASE 1.12 WP04 — TERRA NARROW EVIDENCE-CHANNEL REMEDIATION COMPLETE`

If blocked:

`RELEASE 1.12 WP04 — DURABLE D3 EVIDENCE-CHANNEL REMEDIATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

# GPT-5.6 Luna — Release 1.12 WP04 Signing-Gate Contract Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the repository signing contract and amend the WP04 publication gate if required.
- **GPT-5.6 Terra** — executes only the publication/validation mutations authorized after this reconciliation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Resume **Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**, issue `#263`.

The exact staged candidate remains preserved:

```text
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs
ADD eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Exact staging semantics remain:

```text
3 modified + 1 added = 4 governed paths
```

No commit, push, GHCR, Azure, PR, or lifecycle mutation occurred.

## 2. Proven signing-contract contradiction

Accepted evidence:

- `Directory.Build.local.props` is ignored/local-only and requests local signing using thumbprint `1138E…`.
- That certificate is not currently visible in the execution context's `Cert:\CurrentUser\My`.
- The tracked signing target executes only when:

```text
AutoSignTestBinaries == true
Configuration == Debug
```

Therefore the repository's tracked policy does **not** define automatic Release signing for the Worker binaries.

A Release build remaining unsigned is consistent with the tracked repository contract.

The previous WP04 publication authority nevertheless required signed Release `.exe` and `.dll` binaries. That requirement is now in conflict with the repository's established signing policy.

## 3. Mission

Perform **zero-mutation Luna reconciliation** and determine the correct signing gate for WP04 candidate publication.

Do not regenerate certificates.
Do not change local signing configuration.
Do not modify tracked signing targets.
Do not manually sign Release artifacts.
Do not alter the staged payload.

## 4. Read-only repository signing reconciliation

Inspect the tracked repository signing machinery and establish exactly:

1. which configurations are intended to be signed;
2. whether `AutoSignTestBinaries` is explicitly Debug-only;
3. whether any tracked Release-signing mechanism exists elsewhere;
4. whether CI/release packaging applies a distinct signing mechanism;
5. whether prior WP04/WP03 validation relied on Debug signing only;
6. whether the local ignored props file is intended only to activate the existing tracked Debug-signing path.

Required:

`RELEASE 1.12 WP04 — SIGNING CONTRACT RECONCILIATION: PASS`

## 5. Select exactly one signing-gate decision

### S1 — Align WP04 with the repository's Debug-only local signing contract

Select if no approved Release-signing mechanism exists.

Amend the WP04 publication gate so that:

- Release build must still pass with 0 warnings / 0 errors;
- Release binaries are **not required** to be Authenticode-signed;
- the repository's existing Debug signing contract must be validated separately;
- if the local dev certificate is missing, Terra may recover the expected `CN=AIQuantTradingDev` certificate and validate the Debug signing target;
- no manual Release signing is introduced.

This is a gate correction, not a signing bypass.

### S2 — Use an existing approved Release-signing mechanism

Select only if the repository already contains a specific, established Release-signing path.

Identify exactly:

- tracked mechanism;
- command/target;
- certificate expectations;
- whether local execution is valid;
- whether it changes the four-path publication payload.

Do not invent a new Release-signing path.

### S3 — Tracked policy must be changed

Select only if the product/repository contract requires Release Authenticode signing but the tracked policy is incomplete.

If selected:

- repository change required = `YES`;
- identify exact tracked signing-policy path(s);
- stop before implementation;
- four-path WP04 publication remains blocked pending separate governance.

### S4 — Insufficient evidence

Stop and state exactly what is missing.

Required:

`RELEASE 1.12 WP04 — SIGNING GATE DECISION: <S1|S2|S3|S4>`

## 6. Preferred gate if S1 is selected

If S1 is selected, define the corrected publication gate as:

```text
Release build:
  required = PASS
  warnings = 0
  errors = 0

Release Authenticode signature:
  required = NO

Debug local signing contract:
  required = PASS
  expected signer subject = CN=AIQuantTradingDev
  tracked signing target = existing Debug-only AutoSignTestBinaries path
```

If the certificate is absent, future Terra authority may perform only local certificate/trust/ignored-props recovery needed to validate the Debug signing contract.

Required:

`RELEASE 1.12 WP04 — RELEASE SIGNING REQUIREMENT CORRECTION: PASS`

## 7. Candidate-integrity preservation

Confirm that this reconciliation does not alter:

- the exact four-path staged candidate;
- WP04 persistence architecture;
- D3 durable-evidence architecture;
- schema;
- Docker/entrypoint;
- package dependencies;
- README;
- Azure deployment design.

Required:

`RELEASE 1.12 WP04 — FOUR-PATH CANDIDATE INTEGRITY: PRESERVED`

## 8. Mutation audit

This authority permits zero mutations:

```text
Repository mutations: 0
Git mutations: 0
Certificate-store mutations: 0
Local config mutations: 0
Docker mutations: 0
GHCR mutations: 0
Azure mutations: 0
Provider mutations: 0
GitHub/lifecycle mutations: 0
```

Required:

`RELEASE 1.12 WP04 — SIGNING-GATE RECONCILIATION MUTATION AUDIT: PASS`

## 9. Return evidence

Return:

- exact tracked signing conditions;
- whether any approved Release-signing mechanism exists;
- S1/S2/S3/S4 decision;
- corrected signing gate;
- repository-change determination;
- exact next Terra authority scope.

Do not return secrets.

## 10. Terminal markers

### If S1 is selected

`RELEASE 1.12 WP04 — SIGNING CONTRACT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — SIGNING GATE DECISION: S1`

`RELEASE 1.12 WP04 — RELEASE SIGNING REQUIREMENT CORRECTION: PASS`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — TERRA DEBUG-SIGNING RECOVERY/PUBLICATION RESUME AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA SIGNING-GATE RECONCILIATION COMPLETE`

### If S2 is selected

`RELEASE 1.12 WP04 — SIGNING CONTRACT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — SIGNING GATE DECISION: S2`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — TERRA APPROVED RELEASE-SIGNING/PUBLICATION RESUME AUTHORITY: READY`

`RELEASE 1.12 WP04 — LUNA SIGNING-GATE RECONCILIATION COMPLETE`

### If S3 is selected

`RELEASE 1.12 WP04 — SIGNING CONTRACT RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — SIGNING GATE DECISION: S3`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES`

`RELEASE 1.12 WP04 — SIGNING-POLICY REGOVERNANCE REQUIRED`

### If unresolved

`RELEASE 1.12 WP04 — SIGNING CONTRACT RECONCILIATION: INCOMPLETE`

`RELEASE 1.12 WP04 — SIGNING GATE DECISION: S4`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

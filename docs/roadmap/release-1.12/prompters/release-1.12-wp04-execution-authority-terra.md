# GPT-5.6 Terra --- Release 1.12 WP04 Execution Authority

**Authority state:** `READY`\
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

-   **GPT-5.6 Luna** --- contract, policy, architecture, definition,
    reconciliation, acceptance criteria, governance, read-only/planning.
-   **GPT-5.6 Terra** --- PRIMARY: implementation, validation, approved
    Git/GitHub/Azure mutations, merge/publication, lifecycle completion.
-   **GPT-5.6 Sol** --- supporting analysis/synthesis only; never
    silently replaces Luna or Terra.

## 1. Mission

Execute **Phase 4 --- Release 1.12 WP04: Persistent SQLite
Initialization, Data Update & Recovery**.

WP04 productionizes the already-qualified Azure App Service F1
persistence behavior as a repeatable Release 1.12 capability while
preserving application architecture and SQLite semantics.

It must prove: - persistent SQLite initialization under `/home`; -
governed application/provider data update; - persistence across restart
and redeployment; - SQLite `DELETE` journal mode; - deterministic
retrieval/fidelity/idempotency/conflict semantics; - bounded recovery
and integrity; - fail-closed behavior with no ephemeral or direct-SQL
bypass.

WP04 does not complete WP05--WP08.

## 2. Canonical anchor

Accepted starting evidence:

`RELEASE 1.12 WP04 — PR #276 MERGE VERIFIED: PASS`

`RELEASE 1.12 WP04 — CANONICAL BASE SHA: 40a9a236dae789864f35e64bb5c1afd358e7b3db`

`RELEASE 1.12 WP04 — CANONICAL ANCHOR RESOLUTION: PASS`

`RELEASE 1.12 WP04 — LOCAL/REMOTE MAIN 0/0: PASS`

Before mutation, refresh Git/GitHub and prove current `origin/main`
still contains this anchor. If `main` advanced, STOP and reconcile the
newer commits before implementation.

Required:

`RELEASE 1.12 WP04 — EXECUTION BASE RECONCILIATION: PASS`

## 3. Mandatory governing-source reconciliation

Before design/editing, read current canonical: -
`docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md` -
`docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md` -
`docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md` - GitHub issue
`#263` - relevant WP03 scripts under `eng/azure-cli/r1.12-deployment/` -
relevant Initiative-1.11 qualification evidence under
`eng/azure-cli/r1.11-qualification/` - persistence/Application/Worker
code and tests relevant to the governed behavior.

Repository documents and #263 control over assumptions in this prompt.
Material conflict =\> STOP.

Required:

`RELEASE 1.12 WP04 — GOVERNING CONTRACT RECONCILIATION: PASS`

## 4. Architecture invariants

Preserve: - Domain/Application ownership; - Infrastructure-owned
SQLite/provider mechanics; - Application-owned persistence/use-case
contracts; - Worker outer composition/execution; - Python 3.13 governed
interoperability/presentation only; - Release 1.8 JSON-over-stdio
boundary; - separate canonical visualization JSON handoff; - Streamlit
read-only presentation; - SQLite schema v4 unless canonical WP04
governance explicitly proves otherwise; - deterministic/replay/simulated
provenance; - immutable accepted evidence and deterministic
identities; - explicit idempotency/conflict behavior.

Forbidden: - Python/Streamlit opening SQLite; - deployment scripts
writing domain rows directly; - fabricated accepted evidence; -
Azure-specific second persistence implementation; - provider calls from
presentation; - direct SQL replacing the application update path; -
application semantics moved into PowerShell/shell; - weakened
persistence semantics.

Required:

`RELEASE 1.12 WP04 — ARCHITECTURE PRESERVATION: PASS`

`RELEASE 1.12 WP04 — NO PERSISTENCE BYPASS: PASS`

## 5. Deployment/persistence contract

Preserve: - Azure App Service Linux F1; - West Central US; - custom
Docker; - public/free GHCR; - public/default HTTPS/DNS; - persistent
`/home`; - Azure database candidate `/home/data/aiquant.db`; - writable
SQLite; - SQLite `DELETE` journal mode; - reference/demo scope, no
production SLA; - strict-zero-cost posture.

No successful deployment may silently fall back to ephemeral
persistence.

Required:

`RELEASE 1.12 WP04 — PERSISTENT DATABASE PATH: PASS`

`RELEASE 1.12 WP04 — SQLITE DELETE JOURNAL MODE: PASS`

`RELEASE 1.12 WP04 — EPHEMERAL FALLBACK: ABSENT`

## 6. Exact path allowlist

Derive the exact WP04 path allowlist from the canonical Release 1.12
file manifest and #263 **before first edit**.

Classify each path: `CREATE` / `MODIFY` / `DELETE` / `NOT_REQUIRED`

No other repository path may be mutated. A required unlisted path =\>
STOP for Luna re-governance.

Required:

`RELEASE 1.12 WP04 — EXACT PATH ALLOWLIST: ESTABLISHED`

### README policy

`README.md` is not a WP04 implementation surface unless explicitly
required by the canonical manifest.

If it must change, the standing Front-Door README Information
Preservation Policy controls:

`EXISTING README INFORMATION → PRESERVE`

`PRESERVE INFORMATION; UPDATE STATUS`

`SUMMARY ≠ AUTHORITY TO DELETE DETAIL`

Required:

`README PRESERVATION — ZERO UNAUTHORIZED INFORMATION LOSS: PASS`

## 7. Implementation acceptance

### Persistent initialization

Prove absent-database first start, persistent parent creation,
application-owned schema initialization, expected schema version,
non-root writability, repeatability, and no overwrite of accepted
history.

`RELEASE 1.12 WP04 — PERSISTENT SQLITE INITIALIZATION: PASS`

### Governed data update

Use only the existing provider adapter + Application use-case path.
Preserve timestamp/offset and decimal fidelity, deterministic ordering,
atomic acceptance, idempotency, and explicit conflicts. No
deployment-script domain SQL.

`RELEASE 1.12 WP04 — GOVERNED DATA UPDATE: PASS`

`RELEASE 1.12 WP04 — DATA FIDELITY: PASS`

`RELEASE 1.12 WP04 — IDEMPOTENCY/CONFLICT SEMANTICS: PASS`

### Restart persistence

Record identifiable evidence, restart the runtime, recover boundedly,
retrieve through the governed boundary, and prove unchanged continuity.

`RELEASE 1.12 WP04 — RESTART PERSISTENCE: PASS`

### Redeployment persistence

Record evidence, redeploy using governed immutable-image deployment
without deleting `/home`, recover, and prove evidence survives without
duplication/corruption.

`RELEASE 1.12 WP04 — REDEPLOYMENT PERSISTENCE: PASS`

### Recovery

Exercise only canonically required recovery scenarios. Expected
interruption must not corrupt SQLite and must not require direct-SQL
repair.

`RELEASE 1.12 WP04 — RECOVERY: PASS`

### Integrity

After lifecycle validation prove expected schema, SQLite integrity,
`journal_mode=delete`, and governed read/write behavior.

`RELEASE 1.12 WP04 — SQLITE INTEGRITY: PASS`

`RELEASE 1.12 WP04 — SCHEMA VERSION: PASS`

`RELEASE 1.12 WP04 — SQLITE JOURNAL MODE DELETE: PASS`

## 8. Initiative-1.11 reuse boundary

Reuse proven Initiative-1.11 techniques/evidence patterns where useful,
but Release 1.12 must own its operational implementation. Do not mutate
historical qualification evidence to make WP04 pass.

`RELEASE 1.12 WP04 — INITIATIVE-1.11 EVIDENCE REUSE BOUNDARY: PASS`

## 9. Interactive Windows/Azure/Docker handoff

When Docker Desktop/WSL2, Azure CLI authentication, or Windows-user
context is required and unavailable to Terra:

1.  provide exact copy/paste PowerShell commands;
2.  state expected evidence and mutations;
3.  STOP;
4.  wait for actual stdout/stderr and exit codes;
5.  independently evaluate returned evidence.

Never infer execution.

Do not weaken Docker pipe ACL/security or transfer Azure auth
cache/tokens/profiles.

Requester-executed authorized mutations count in the mutation audit.

`RELEASE 1.12 WP04 — INTERACTIVE EXECUTION HANDOFF: RECONCILED`

## 10. Validation

Run all canonical WP04 validation plus affected suites. At minimum: -
build; - relevant .NET tests; - Architecture tests; - relevant Python
tests if affected; - `git diff --check`; - Gitleaks; - required
environment/package integrity; - deployment-script validation; - Azure
persistence lifecycle validation; - residue checks where applicable.

Use fresh counts only.

Required:

`RELEASE 1.12 WP04 — BUILD VALIDATION: PASS`

`RELEASE 1.12 WP04 — DOTNET TESTS: PASS`

`RELEASE 1.12 WP04 — ARCHITECTURE TESTS: PASS`

`RELEASE 1.12 WP04 — PYTHON VALIDATION: PASS` or `NOT APPLICABLE`

`RELEASE 1.12 WP04 — GITLEAKS: PASS`

`RELEASE 1.12 WP04 — DIFF CHECK: PASS`

`RELEASE 1.12 WP04 — DEPLOYED PERSISTENCE VALIDATION: PASS`

## 11. Secrets

A provider secret may be consumed only for bounded validation through
the existing provider path.

Never commit/log/persist credentials, copy Azure authentication
material, or broaden WP04 into WP05 secret automation.

`RELEASE 1.12 WP04 — SECRET HYGIENE: PASS`

## 12. Azure/cost boundary

Do not introduce Azure SQL, Container Apps, Azure Files, mandatory ACR,
paid App Service, paid monitoring/storage, or production-SLA claims.

Clean temporary resources according to the canonical plan. Do not claim
WP07 final cost qualification from WP04.

`RELEASE 1.12 WP04 — AZURE SCOPE BOUNDARY: PASS`

`RELEASE 1.12 WP04 — NO UNAUTHORIZED PAID DEPENDENCY: PASS`

## 13. Git/PR publication

Preferred branch:

`release/1.12-wp04-persistent-sqlite`

Preferred commit:

`Implement Release 1.12 persistent SQLite lifecycle`

Preferred PR:

`Release 1.12 WP04: persistent SQLite lifecycle`

Before commit/push prove exact allowlisted payload, no unrelated tracked
changes, requester untracked files preserved, fresh validation,
candidate SHA/parent, and candidate path equality.

Required:

`RELEASE 1.12 WP04 — CANDIDATE PAYLOAD: PASS`

`RELEASE 1.12 WP04 — COMMIT: PASS`

`RELEASE 1.12 WP04 — PUSH: PASS`

PR creation is authorized after implementation/validation acceptance.
Target `main`; reference #263 without prematurely closing it.

`RELEASE 1.12 WP04 — PR CREATED: PASS`

`RELEASE 1.12 WP04 — PR PAYLOAD: PASS`

## 14. Merge authority

Terra may merge the WP04 PR only after: - exact acceptance gates pass; -
payload equals allowlist; - validation is fresh; - deployed persistence
evidence passes; - no unresolved checks/review failures; - `main` has
not advanced incompatibly.

Required:

`RELEASE 1.12 WP04 — PRE-MERGE RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — ACCEPTANCE GATE: PASS`

`RELEASE 1.12 WP04 — PR MERGE: PASS`

Record merge SHA and authoritative parent/path comparison.

## 15. Mandatory WP lifecycle completion

WP04 is not complete merely because its PR merged.

After post-merge verification: 1. close issue `#263`; 2. set Project #2
Status to **Done**, unless issue closure automation already did so; 3.
do not make a redundant Project mutation if automation already set Done;
4. verify #263 Closed and Project Status Done; 5. keep milestone #63
Open; 6. verify WP05 #264 is next.

Required:

`RELEASE 1.12 WP04 — ISSUE #263 CLOSED: PASS`

`RELEASE 1.12 WP04 — PROJECT STATUS DONE: PASS`

`RELEASE 1.12 WP04 — LIFECYCLE COMPLETION: PASS`

`RELEASE 1.12 WP05 — EXECUTION AUTHORITY: READY`

## 16. Post-merge verification

Synchronize `main`, prove local/origin 0/0, prove accepted candidate =
merged path set, rerun required post-merge validation, prove clean
tracked/staged state, preserve requester untracked files, and record
actual milestone #63 counts.

Required:

`RELEASE 1.12 WP04 — POST-MERGE MAIN SYNCHRONIZATION: PASS`

`RELEASE 1.12 WP04 — HEAD/MERGE PATH SET EQUALITY: PASS`

`RELEASE 1.12 WP04 — POST-MERGE VALIDATION: PASS`

`RELEASE 1.12 WP04 — POST-MERGE CLEANLINESS: PASS`

## 17. Mutation audit

Report exact actual counts for repository creates/modifies/deletes,
commits, pushes, PRs, merges, issue/Project/milestone mutations,
Azure/GHCR/provider mutations, and package/schema mutations.

User-executed Terra-authorized mutations count. Automated Project
transitions do not count as explicit Terra mutations.

`RELEASE 1.12 WP04 — MUTATION AUDIT: PASS`

## 18. Explicit exclusions

No Product Release 1.11, Azure SQL, Container Apps, Azure Files,
mandatory ACR, paid infrastructure, production claims, ML, backtesting,
live trading, parallel pipeline, Python/Streamlit persistence ownership,
direct SQLite UI access, new provider architecture, WP05 secret
automation, WP06 final public System Health acceptance, WP07 final
stability/cost qualification, WP08 release acceptance, or schema
migration unless canonical WP04 governance separately requires and
proves it.

## 19. Final acceptance

All applicable gates above must have direct evidence.

Final:

`RELEASE 1.12 WP04 — PERSISTENT SQLITE INITIALIZATION, DATA UPDATE & RECOVERY: PASS`

`RELEASE 1.12 WP04 — ISSUE #263 CLOSED: PASS`

`RELEASE 1.12 WP04 — PROJECT STATUS DONE: PASS`

`RELEASE 1.12 WP04 — LIFECYCLE COMPLETION: PASS`

`RELEASE 1.12 WP05 — EXECUTION AUTHORITY: READY`

Terminal:

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY COMPLETE`

If any mandatory gate cannot be proven:

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

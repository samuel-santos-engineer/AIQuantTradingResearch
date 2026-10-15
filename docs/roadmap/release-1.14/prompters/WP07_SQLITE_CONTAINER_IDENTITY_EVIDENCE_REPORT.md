# WP07 — Repository-Only SQLite and Container Identity Evidence Report

Recorded: 2026-10-11 (America/Sao_Paulo), by subsequent owner authority to save the previously presented report. This records the preceding read-only inspection; it is not a new inspection or runtime qualification. The original milestone prohibited repository output; this later instruction authorizes this document only. Original findings and status below are historical to that inspection.

## A. Exact baseline and inspection coverage

Inspected the current local working tree, including uncommitted governance edits and untracked WP07 modules, not a remote snapshot.

- Branch: `draft/release-1.14-wp07-partial-public-disabled`
- HEAD: `67893f51931ee183f71050a83b81127e6e648b87`
- Local relationship to `origin/main`: 1 commit ahead, 0 behind. No fetch performed.
- `PUBLIC_RUNTIME_VERIFIED = False` at `python/presentation/historical_market_bridge.py:244`.

Inspection covered SQLite adapter/schema sources, Worker configuration/composition, Dockerfile, entrypoint, bridge, offline supervisor/broker/journal/identity modules, persistence deployment scripts, selected persistence tests and current governance documents.

Tracked SQLite sources, Dockerfile, entrypoint, Worker Program.cs, bridge, persistence deployment scripts and inspected SQLite tests had no local modifications. Four governance documents and two acceptance/isolation tests were modified. Offline public_runtime modules and their three focused tests were untracked. Exact original inventory appears in H.

docker-compose.yml contained no configuration. No tracked .github/workflows files were found. No live configuration, database file, database contents or secrets were inspected.

## B. SQLite adapter/schema/transaction evidence

The directory for the following relative filenames is exactly `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/`.

| Finding | Source and lines | Classification |
|---|---|---|
| Connection factory | SqliteConnectionFactory.cs:12–39 | PROVEN BY REPOSITORY: configured path becomes DataSource; explicit ReadWriteCreate; optional parent creation; open, journal-mode enforcement and schema bootstrap. |
| Configuration | SqliteStorageConfiguration.cs:5–18 | PROVEN BY REPOSITORY: Persistence:DatabasePath; rejects blank values. Parent creation defaults false. No absolute-path or symlink validation here. |
| Journal mode | SqliteConnectionFactory.cs:53–60 | PROVEN BY REPOSITORY: requests DELETE mode and rejects failure to establish it. |
| Schema/version handling | SqliteSchemaBootstrapper.cs:8–11,118–198 | PROVEN BY REPOSITORY: user_version target 4; transactional initialization, migration and validation; unsupported versions rejected. |
| Foreign keys | SqliteSchemaBootstrapper.cs:127–133,191–196,219–230 | PROVEN BY REPOSITORY: enabled, temporarily disabled for version-three migration and reenabled afterward. |
| Observation writes | SqliteHistoricalObservationStore.cs:164–196 | PROVEN BY REPOSITORY: non-deferred transaction, conflict rollback and commit; equivalent records produce idempotent results. |
| Other stores | SqliteDatasetSnapshotStore.cs:144–171; SqliteExperimentResultStore.cs:140–155 | PROVEN BY REPOSITORY: explicit transactional writes. |

| Table | Source lines | Important constraints |
|---|---|---|
| historical_observations | SqliteHistoricalObservationSchema.cs:9–16 | Primary key (target, instant_utc_ticks); bounded offset; nonempty price; STRICT, WITHOUT ROWID. |
| dataset_snapshots | SqliteDatasetSchema.cs:12–65 | Snapshot primary key, identity/check constraints, range/count/source constraints. |
| dataset_snapshot_observations | SqliteDatasetSchema.cs:68–81 | Composite primary key, unique snapshot/timestamp, snapshot foreign key and value checks. |
| experiment_results | SqliteExperimentResultSchema.cs:10–61 | Result primary key, snapshot foreign key, identity/count/aggregate constraints. |

No admission/occupation table was found in inspected production schema sources. Actual deployed tables were not inspected.

synchronous, busy_timeout, locking_mode, pooling and connection timeout are not explicitly configured in the inspected SQLite adapter. No runtime defaults are inferred. WAL is not the selected factory mode; no explicit checkpoint or sidecar-management implementation was identified there.

Tests such as `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs:57–126,325–386` describe bootstrap, reopen, DELETE mode and migration. They were read, not run, and do not establish Azure locking/crash durability.

Conceptual fit — DESIGN INFERENCE: one database could contain trusted admission state alongside private research tables. Existing schema/adapter do not implement it. Observation rows would not supply admission semantics. No schema migration or SQL was proposed.

## C. Database path and /home deployment evidence

Configuration chain:

1. `src/AIQuantTradingResearch.Worker/Program.cs:18–20` identifies persistence keys.
2. Program.cs:113–131 constructs SqliteStorageConfiguration; invalid mandatory configuration fails.
3. Program.cs:145–147 registers private Infrastructure and diagnostics.
4. `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs:94–113` registers factory/private stores.
5. SqliteConnectionFactory.cs:16–38 passes the path to SQLite and opens it.

- PROVEN BY REPOSITORY: Dockerfile:23–24 supplies `/runtime/aiq.sqlite`, not `/home/data/aiquant.db`.
- DOCUMENTED/CONFIGURATION INTENT ONLY: `eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1:5,17` constrains/configures `/home/data/aiquant.db`, with parent initialization enabled.
- CONFIGURATION INTENT ONLY: `eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/deploy-f1-reference.ps1:35` requests WEBSITES_ENABLE_APP_SERVICE_STORAGE=true.
- OWNER-STATED: approved existing /home SQLite database is this phase's sole storage candidate.
- REQUIRES AZURE QUALIFICATION: current settings/mount properties and whether running private research opens precisely that file.

Scripts were not executed; their presence cannot verify current Azure settings.

`container/entrypoint.sh:64–90,126–138` permits parents under /home or /runtime, creates/chowns/chmods the parent during root startup and checks writability after dropping privileges. The lexical check is not canonical traversal or symlink protection. It does not establish database/sidecar ownership or modes.

ReadWriteCreate can create a missing database. Existing initialization behavior is not automatically safe admission recovery: missing initialized admission state must deny, not create a clean budget.

DELETE-mode rollback sidecars must be protected with the database. Existing or alternative -wal, -shm and -journal files belong in the boundary assessment. No sidecar permissions were measured.

## D. Container identity, DB access and A2 authority map

| Role | Repository evidence | Static conclusion |
|---|---|---|
| Initial entrypoint | Dockerfile:42–51 | Explicit USER root; creates aiq and chowns /app and /runtime. Azure effective identity/overrides unverified. |
| Public UI | container/entrypoint.sh:5–24 | Root branch drops to aiq, clears environment and starts Streamlit. No distinct public UI account wired. |
| Private Worker | entrypoint.sh:86–97,131–157 | Parent assigned 1000:1000; drops to named aiq then launches dotnet. useradd does not explicitly pin numeric UID/GID. |
| Public Worker | python/presentation/historical_market_bridge.py:441–458 | Disabled. Popen has no user/group transition and ordinarily inherits UI credentials. |
| Supervisor/broker | python/public_runtime/bootstrap.py:13–45 | Offline plan: supervisor 1001, UI 1002, broker 1003; no credential switching/launch implemented. |
| Worker/guardian candidate | python/public_runtime/broker.py:4–5,22–24,39–48 | Metadata expects Worker 1003; live operations reject. A2 distinct broker/Worker identity not implemented. |

Public composition avoids SQLite registration: Program.cs:28–44 returns through synthetic composition; DependencyInjection.cs:22–36 registers synthetic/cache services only. This is application composition separation, not file-access isolation.

Child environment is built from scratch at historical_market_bridge.py:246–251. Popen uses stdin/stdout/stderr, close_fds=True and one launch-gate descriptor at 454–458. No production broker socket is wired.

Filesystem guard at 261–279 rejects /runtime, private cache paths and unapproved mounts including broad /home. Current public execution cannot simply be enabled with a broadly visible private /home database.

Source-level blocker: current public/private execution uses the same named aiq account and private persistence grants it access. If the DB is visible with those permissions, public exclusion is not established. No isolated Worker filesystem view resolves this conflict.

SQLite has no per-table OS-file boundary: Worker needs zero access to the entire DB and sidecars.

Offline controller settlement requires broker cleanup and independent verifier at python/public_runtime/supervisor.py:46–58; default verifier returns false at 10–13. It is not SQLite-backed. Combined broker/supervisor death has no implemented live independent recovery authority. Safe proposed behavior is continued occupation/denial.

## E. Admission crash-window/fail-closed table

DESIGN INFERENCE, not existing SQLite behavior.

| Window/failure | Required proposed outcome |
|---|---|
| Valid FREE, before reservation commit | No Worker; admission only through authoritative atomic transition. |
| Commit fails or acknowledgment uncertain | No spawn; resolve authoritative state or deny, never assume FREE. |
| OCCUPIED committed, before spawn | Budget charged; crash retains occupation. |
| Spawn attempted, acknowledgment lost | Worker may exist; full charge/occupation retained. |
| Running/timeout | Cancellation does not settle; independently verify full tree death. |
| Stale/reused PID | PID alone cannot clear occupation. |
| Broker/supervisor/guardian death, combined death | No reopening by lock release/time; independent recovery proof. |
| SQLite busy | Bounded denial; no bypass/alternative DB/empty state. |
| Corruption/missing DB or admission state | Deny; factory auto-creation must not become recovery. |
| /home interruption/uncertain persistence | Deny; commit success cannot be presumed. |
| Rollback/restart | Preserve occupation/history; no replenishment/midnight reset. |
| Tree death verified, settlement not committed | SETTLEMENT_PENDING still occupied for admission. |
| Settlement acknowledgment uncertain | Resolve durable state or deny. |
| Verified death and durable settlement | FREE may return; rolling budget history remains. |

Reservation transaction must finish before spawn. Holding a DB transaction throughout execution is not a durable occupied record. All enabled instances need the same authority; an instance-local mutex is insufficient.

## F. Ten-item evidence/qualification matrix

| Item | Classification/confidence | Gap/minimum next evidence |
|---|---|---|
| 1. Adapter/schema | PROVEN BY REPOSITORY — high | Source citations above; deployed schema uninspected. |
| 2. Approved /home path | OWNER-STATED + configuration intent — partial | Different image default; separately authorized sanitized live path/config evidence. |
| 3. Private isolation | BLOCKED — high confidence in source gap | Shared named account/no isolated view; role/path/sidecar design needed. |
| 4. Atomic admission | DESIGN INFERENCE — unimplemented | No admission schema/adapter; reviewed state/transaction ownership design. |
| 5. Azure durability | REQUIRES AZURE QUALIFICATION | Exact mount guarantees and bounded authorized durability evidence. |
| 6. Global topology | BLOCKED/UNKNOWN | Scale/recycle/region overlap; authoritative topology/shared scope. |
| 7. A2 authority | BLOCKED — unimplemented | Fixed-role launch/cross-UID cancellation argument. |
| 8. Independent death proof | BLOCKED — no live verifier | Observer/identity/recovery model then separately authorized evidence. |
| 9. Additional resources | OWNER-STATED constraint | Inspection added none; eventual candidate uses only approved DB/preserves recurring cost. |
| 10. C2 integration | PROVEN partial offline policy; integration blocked | supervisor.py:33 still calls QuotaGate; no C2 breaker/SQLite integration. Documentary reconciliation first. |

## G. Security blockers and minimum qualification work

Next documentary tasks:

1. Trusted DB-access authority/private-research coexistence model; resolve broker-only access versus supervisor-owned settlement without silently reallocating authority.
2. DB/sidecar exclusion for UI/Worker/descendants and A2 launch/cancellation model.
3. Admission state, initialization/recovery distinction, version compatibility and atomic transaction boundaries; no migration implementation.
4. Topology, C2 breaker thresholds and total-attributable CPU accounting.

Fresh authority would be required for sanitized target path/mount/identity verification, file-access negatives, bounded SQLite contention/crash/restart tests and independent combined-failure cleanup evidence. None performed.

## H. Original no-mutation/no-execution attestation and status

The original inspection changed no file, database connection, PR or Azure resource. No app, SQLite command/PRAGMA, build, test, container or deployment script ran. Status matched at inspection start/end:

```text
 M docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md
 M docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md
 M docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md
 M docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md
 M python/presentation/test_public_runtime_isolation.py
 M tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalProductionCompositionAcceptanceTests.cs
?? docs/roadmap/release-1.14/prompters/WP07_ARCHITECTURE_ALTERNATIVES_DECISION_RECORD.md
?? docs/roadmap/release-1.14/prompters/WP07_Azure_F1_Containment_Feasibility_Codex_Authority.md
?? docs/roadmap/release-1.14/prompters/WP07_BROKER_SUPERVISOR_IMPLEMENTATION_PREFLIGHT.md
?? docs/roadmap/release-1.14/prompters/WP07_C2_CONTRACT_AMENDMENT_CODEX_PROMPT.md
?? docs/roadmap/release-1.14/prompters/WP07_OFFLINE_POLICY_FROZEN_EVIDENCE_ASSESSMENT.md
?? docs/roadmap/release-1.14/prompters/WP07_OFFLINE_POLICY_IMPLEMENTATION_REVIEW.md
?? docs/roadmap/release-1.14/prompters/WP07_OPTION_B_CONSOLIDATED_PLANNING_AND_QUALIFICATION.md
?? docs/roadmap/release-1.14/prompters/WP07_OPTION_B_CONSOLIDATED_SECURITY_CONTRACT_AMENDMENT.md
?? docs/roadmap/release-1.14/prompters/WP07_READ_ONLY_ARCHITECTURE_CLOSURE.md
?? docs/roadmap/release-1.14/prompters/WP07_READ_ONLY_QUALIFICATION_PLAN.md
?? docs/roadmap/release-1.14/prompters/WP07_SQLITE_REPOSITORY_ONLY_EVIDENCE_MILESTONE_CODEX_PROMPT.md
?? python/presentation/test_public_broker.py
?? python/presentation/test_public_quota_evidence.py
?? python/presentation/test_public_supervisor.py
?? python/public_runtime/__init__.py
?? python/public_runtime/bootstrap.py
?? python/public_runtime/broker.py
?? python/public_runtime/ipc.py
?? python/public_runtime/journal.py
?? python/public_runtime/quota.py
?? python/public_runtime/supervisor.py
```

Remote PR/issue state and frozen hashes were not reverified.

**Decision: repository evidence sufficient for design planning / insufficient for runtime qualification.** Shared-account startup and absent SQLite admission integration block using the candidate as-is; they do not prove a separately designed isolated architecture infeasible.

The subsequent recording operation creates this document only. It does not change the original evidence classifications, frozen implementation, policy, database or public activation status.

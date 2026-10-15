# WP07 — A2 Distinct-UID + Existing SQLite Admission Architecture Decision Record

**Date:** 2026-10-11  
**Status:** **PROPOSED ARCHITECTURE — documentary only; not qualified, selected for implementation, or authorized for deployment**  
**Scope:** Release 1.14 / WP07, Azure Linux App Service F1, public synthetic-only BTC/USD and ETH/USD preview.  
**Owner constraint:** **Reuse only the existing approved SQLite database stored under `/home`. No auxiliary database, second SQLite file, Azure Blob, Azure SQL, new paid resource, or alternative journal backend.**  
**Runtime gate:** `PUBLIC_RUNTIME_VERIFIED=False` remains mandatory.

## 1. Purpose and authority

Resolve the *design* relationship between A2 distinct-UID isolation, the private research SQLite database, one durable global admission journal, and independently proven Worker-tree cleanup. This is a decision record for owner review, **not** permission to implement, migrate, inspect the live database, test, commit, deploy, change Azure settings, or enable public execution.

**Evidence baseline:** user-provided read-only Codex repository inspection of branch `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`, with local uncommitted and untracked files inspected. This is a dated snapshot, not a fresh working-tree verification. The earlier `WP07_ARCHITECTURE_ALTERNATIVES_DECISION_RECORD.md` compared A1/A2, B1/B2 and C1/C2; this record narrows the design to **A2 + the already approved SQLite file**, superseding its B2 exploration for this phase only if the owner adopts this proposal. C2 residual F1 quota-exhaustion risk has been accepted in principle; its numerical policy and integrated implementation are not qualified.

### Evidence classification

- **Repository-proven:** the inspected connection factory uses `ReadWriteCreate`, enforces SQLite **DELETE** journal mode, and bootstraps schema `user_version=4`; private observation/snapshot/experiment stores use transactions. There is **no production admission table**. `Persistence:DatabasePath` is externally configured. `Dockerfile` fallback is `/runtime/aiq.sqlite`; deployment scripts *intend* `/home/data/aiquant.db` and `WEBSITES_ENABLE_APP_SERVICE_STORAGE=true`. Current public and private startup uses named `aiq` identity; public Popen does not switch UID. Offline supervisor/broker role numbers and cleanup interfaces are not live. `PUBLIC_RUNTIME_VERIFIED=False` is present.
- **Owner-stated:** the existing approved SQLite database is under `/home`, and is the **only** permitted database for this phase.
- **Unverified in Azure:** active path, real UID/GID, filesystem/mount/sidecar permissions, Azure Files locking/fsync semantics, actual deployment topology/recycle overlap, cross-UID process control and recovery, and live admission durability.
- **Proposed below:** role ownership, journal state machine, trust boundaries and fail-closed rules. These are **not** descriptions of implemented behavior.

## 2. Proposed architecture choice

**Candidate decision A2-SQLite:** use the **same existing `/home` SQLite database file** for a trusted admission namespace (future schema-versioned tables), while enforcing separate operating-system identities and denying public Worker and descendants *all* access to the database, its directory and journal sidecars.

This is **conditional**: if actual F1 constraints cannot provide an enforceable identity/cleanup boundary or the `/home` backing store cannot prove safe admission transactions across the full deployment domain, **retain public execution disabled**. Do not add an alternative storage backend or relax no-overlap/confidentiality.

### Trust and process boundaries (logical, not implemented)

| Role | Proposed privilege boundary | SQLite access | Admission authority |
|---|---|---|---|
| Root bootstrap (finite) | Only at container startup for fixed account/directory/FD setup; drops privileges before serving traffic | No ongoing database transaction responsibility | Cannot be a public command surface or a persistent privileged helper by default |
| Trusted persistence/admission authority | Dedicated non-public principal or tightly scoped trusted private-side component; final UID assignment **TBD** | Read/write the **existing** SQLite file and all necessary sidecars | **Sole writer** of occupation, budget and settlement state; owns transactional admission decisions |
| Trusted private research Worker | Existing private data functionality must remain usable; its file-access model requires explicit review | May need authorized access to private research tables, without becoming an anonymous public entry point | No unilateral ability to clear public occupation |
| Public UI | Distinct unprivileged principal, no inherited private credentials/FDs | **None** | Requests only; cannot create Worker directly or alter journal |
| Public broker / Worker launcher | Distinct from public Worker; exact relation to trusted DB authority and supervisor **TBD** | Prefer **no raw SQLite access** unless explicitly justified and isolated | May launch only with an authenticated, one-use committed admission authorization |
| Public synthetic Worker and descendants | Distinct, least-privileged principal; no private mounts, credentials, control sockets or capabilities | **None**, including database, `-journal`, `-wal`, `-shm`, directory traversal or inherited open FD | None; synthetic-only computation |
| Independent termination verifier / recovery authority | Trusted identity independent of a dying Worker/broker; actual lifecycle and privilege mechanism **TBD** | No direct clearing unless routed through sole trusted journal writer | Supplies independently verifiable *whole-tree death* evidence; cannot clear by timeout/PID alone |

**Do not hardcode UID 1001/1002/1003 as a solution.** The offline bootstrap currently labels supervisor 1001, UI 1002 and broker 1003, while the offline broker metadata expects Worker 1003. This is internally insufficient for distinct broker/Worker identity and conflicts with existing `aiq` startup. Produce one collision-free numeric UID/GID and supplementary-group map only after verifying image and App Service capabilities. Account names, permissions and inherited groups matter as much as numeric labels.

### Architecture sketch

```text
anonymous request
     |
 public UI [no SQLite/private access]
     |
 authenticated, bounded request IPC (protocol/auth TBD)
     v
 trusted admission authority [only durable writer]
     |-- SQLite transaction in EXISTING /home database
     |      verify healthy state + C2 budgets + global FREE
     |      durably record reservation + OCCUPIED
     |      COMMIT acknowledged before any spawn
     |
     v
 trusted launcher / broker [fixed role, constrained authority]
     |
 public synthetic Worker [different UID, no /home DB access]
     |
 independently verified complete Worker-tree death
     v
 trusted authority commits settlement; only then FREE
```

**Critical unresolved design choice:** how the trusted persistence authority, supervisor, broker and independent verifier divide responsibilities without allowing an untrusted or compromised public-facing process to forge admission or cleanup evidence. The repository inspection explicitly found a supervisor-owned settlement model and a broker cleanup interface, but no live SQLite integration. No authority reassignment is silently approved here.

## 3. Existing SQLite compatibility and path contract

1. **Single file:** future admission tables would reside in the **same** existing database, not an adjacent admission `.db` file. The current schema version is 4; any future migration is separately authorized and must preserve all private research tables, identity constraints and data.
2. **Path:** approved target is the existing `/home` database. The script-intended `/home/data/aiquant.db` is **not** asserted to be the active runtime path. Docker's `/runtime/aiq.sqlite` fallback must not silently become a second admission authority. A future startup must verify canonical path, file identity, mount identity, symlink resolution and expected owner/permissions before admission.
3. **Sidecars:** DELETE-mode rollback `-journal` and any residual/legacy `-wal`/`-shm` are within the same confidentiality and integrity boundary. Protect parent directory traversal, creation and replacement, not only the main `.db` file.
4. **No accidental reinitialization:** current `ReadWriteCreate` is appropriate to some existing private initialization flows but **unsafe as a recovery default**. Missing DB, missing admission schema, mismatched identity, unknown migration state, corruption or unexpected reset must **deny public generation**, not initialize a fresh FREE state.
5. **Transactions:** SQLite's atomic transaction interface is a plausible pre-spawn occupation primitive, **provided** all competing writers share one correctly locked, durable database and the Azure mount actually honors required semantics. DELETE mode is repository-proven; `synchronous`, `busy_timeout`, `locking_mode`, connection timeout and pooling are not explicitly set in the inspected adapter. Do not infer runtime defaults or assume durability from `COMMIT` alone.
6. **Deployment scope:** if simultaneous app instances, slots, old/new revisions or overlapping recycle processes can admit jobs, every participant must share the *same* correctly functioning authority; a local mutex, PID file or process lock is insufficient. If the scope cannot be proven, deny admission.

## 4. Proposed admission state and transaction boundaries

These are **semantic states**, not authorized table definitions or SQL migrations.

- **UNINITIALIZED / INVALID:** no proven admission lineage, schema, identity or durable state. **Deny**. Only a separately approved controlled first-time initialization can establish an initial FREE lineage, after proving no prior public Worker can exist and no competing instance can admit.
- **FREE:** verified unoccupied state, intact durable history and healthy authority; only a single atomic transaction may transition to OCCUPIED.
- **OCCUPIED / RESERVED:** globally exclusive occupation with unique generation/attempt identity, durable pre-spawn budget charge, monotonic lineage/epoch and conservative timestamps. Occupation persists even when launch acknowledgement is missing.
- **CLEANUP_REQUIRED / UNCERTAIN:** Worker existence or descendants cannot be excluded; no admission. Expiration of a lease, process timeout, lock release, PID absence or reboot is not clearance.
- **SETTLEMENT_PENDING:** independent complete-tree death evidence has been received but durable settlement not yet confirmed; still blocks admission.
- **FREE after settlement:** allowed only after independent death proof is bound to the correct generation and a successful durable settlement transaction; rolling budget history persists.

**Proposed atomic admission:** trusted authority checks state, generation, C2 limits and health; atomically records occupation and full reservation in the existing SQLite file; obtains unambiguous durable commit success; only then authorizes a single launch. The transaction **does not remain open during execution**. Uncertain commit acknowledgment means no spawn and no optimistic retry; reread/reconcile safely or deny.

**Proposed settlement:** independent trusted verifier establishes full Worker *and descendant* death for the correct generation; trusted authority commits settlement and preserves all charged history. An uncertain settlement commit is treated as occupied until authoritative reconciliation.

**Crash windows:**

| Failure | Required behavior |
|---|---|
| Crash before committed occupation | No spawn; only independently confirmed FREE lineage may admit later |
| Occupation committed, crash before spawn | Stay occupied; recover only with proof no Worker/launch was possible or could remain |
| Spawn initiated, acknowledgment lost | Assume Worker tree may exist; stay occupied |
| Worker timeout or cancellation | Remain occupied until independent complete-tree death proof |
| Broker and supervisor die together | Fail closed across restart/recycle; no lock-release-based reopening |
| Stale/reused PID or orphan descendant | PID is not proof; no settlement |
| SQLite busy, interrupted mount, corruption, missing schema, uncertain fsync | Deny; never use fallback storage or reset counters |
| Clock rollback or app restart | No budget reset, refill or premature expiration |
| Settlement evidence exists but commit is uncertain | Stay occupied until durable state reconciled |

A robust recovery protocol must also address **the case where all supervising processes died and the platform restarted**. If an independent full-tree death proof is unavailable, indefinite denial is safer than speculative release. A manual operator override cannot be treated as a routine anonymous recovery mechanism; any exceptional reset requires a separate governed incident procedure and positive non-overlap evidence.

## 5. A2 isolation and cleanup design gates

A2 is **not** equivalent to merely running `setuid` or using different account names. The design must demonstrate:

1. Effective real/effective/saved UID/GID, supplementary groups, capabilities and permitted signal/ptrace relationships for each role, on the actual F1 container.
2. Public Worker cannot traverse `/home` to the SQLite directory, open/replace the DB or sidecars, read private caches, obtain provider secrets, inherit trusted file descriptors, inspect trusted process memory, or reach broker/admission control endpoints except through the minimal authenticated protocol.
3. Public UI cannot directly spawn privileged Workers or impersonate a trusted cleanup/verifier identity.
4. Trusted launcher can create the Worker under a genuinely distinct UID **without** an unbounded resident root helper, and can terminate/reap the *entire descendant tree* despite cross-UID restrictions.
5. Worker descendants cannot escape process tracking through daemonization, reparenting, process groups, double forks or PID reuse. A launch gate, PID1 `flock` or a released file lock is not proof of tree death.
6. The independent verifier survives or can conservatively recover from combined supervisor/broker/guardian death. If F1 kernel features cannot support the necessary observation/control, public generation remains disabled.
7. Any IPC credential, socket path, peer identity and replay/one-use token semantics are specified and tested before activation. UID equality alone is not authentication.

## 6. C2 budget and F1 availability relationship

C2 removes a proposed dependency on authoritative fresh quota telemetry **only for availability policy**. It cannot waive storage, confidentiality or complete-tree death gates. Local budgets are not hard CPU ceilings on F1. The previously discussed candidate bounds (one occupied job, full pre-spawn CPU reservation, rolling admissions and bounded abuse) require durable SQLite accounting and integrated health-breaker semantics. The 15 × 20s = 300s measured acceptance scenario versus 240s proposed reserved daily cap remains an unresolved numerical reconciliation. No limits are approved or changed by this ADR.

The accepted-in-principle residual risk is F1 quota exhaustion/unavailability, **not** public access to private data, overlapping Workers, or reset of durable budgets after crash.

## 7. Decision matrix and blockers

| Gate | Evidence today | Disposition |
|---|---|---|
| Reuse existing SQLite | Production adapter and schema identified; owner requires `/home` | **Candidate selected for design only** |
| Schema-compatible admission journal | No production admission tables or migration | **Design needed** |
| Active `/home` file identity | Deployment script intent; Docker fallback differs | **Azure verification needed** |
| Azure `/home` transactional locking/durability | Not tested or contractually established | **Q-ST blocked** |
| A2 UID/FD/private-file isolation | Current shared `aiq` identity; offline roles only | **Q-ID blocked** |
| Cross-UID launch and independent full-tree cleanup | No live verifier/cleanup implementation | **Q-CF blocked** |
| One global admission domain across topology | Actual instance/recycle overlap unknown | **Blocked** |
| C2 integrated accounting/health | Offline partial policy, no SQLite integration | **Blocked** |
| Zero incremental recurring cost | No new resource proposed; existing F1 only | **Must verify operational bounds** |
| Public activation | `PUBLIC_RUNTIME_VERIFIED=False` | **Remain disabled** |

## 8. Proposed next evidence packages (separate owner authority)

**D1 — Documentary role/ownership closure:** exact role matrix and UID/GID map; DB/sidecar ownership and path traversal; private Worker coexistence; authenticated IPC; single trusted admission writer; immutable launch/cleanup and settlement responsibility. **No edits required to implement this ADR.**

**D2 — Documentary SQLite contract:** proposed admission schema and migration/version strategy, startup identity checks, first initialization vs recovery, state lineage, atomic pre-spawn charge, global scope, bounded busy/uncertain commit, rolling C2 history and clock uncertainty. **Do not run migrations.**

**D3 — Documentary failure/qualification plan:** map each crash window to independent proof, combined-death recovery, cross-UID descendant handling, F1 topology and target evidence; reconcile Q-ID/Q-ST/Q-CF/Q-QF/Q-AB and current C2 policy contradictions. **No tests or Azure calls.**

**Q — Separately authorized runtime qualification:** sanitized Azure app settings and mount identity; exact SQLite DB and sidecar permissions; locking/fsync/atomicity and crash/restart behavior on the actual F1 `/home`; contention across every possible admitting process; UID/capability/IPC negative tests; complete-tree death after combined crashes; C2 accounting and health; zero incremental recurring charges. Use a controlled non-production or otherwise explicitly approved safety envelope; never destructively probe the private production DB without separate explicit approval.

**I — Separate implementation authority only after D/Q feasibility:** exhaustive literal file allowlist, migrations, source/tests, Dockerfile/entrypoint changes, security review, baseline revalidation and rollback plan. No such authority exists in this record.

## 9. Rejected shortcuts

- Separate SQLite journal file or alternate DB/service, even if convenient.
- Storing admission state in `historical_observations` rows or relying on an in-memory counter.
- Enabling public execution while UI/Worker and private persistence share unrestricted `aiq` filesystem rights.
- Treating `close_fds=True`, a clean environment, application composition or path-string guards as sufficient filesystem isolation.
- Assuming `/home` persistence means SQLite locking/fsync correctness across recycle or concurrent instances.
- Treating `ReadWriteCreate` and missing state as safe FREE initialization.
- Treating `flock` release, timeout, lease expiry, stale PID or a successful signal as independent complete-tree death proof.
- Assuming one instance, one process, or one durable writer without inspecting actual deployment topology.
- Using C2 budget limits as a hard CPU containment guarantee.

## 10. Owner decision requested

**Proposed for explicit approval (planning only):** “Adopt A2 distinct broker/Worker identities and the existing approved `/home` SQLite database as the **exclusive architecture candidates** for WP07; exclude auxiliary storage; proceed to documentary role/SQLite/failure design closure while keeping public runtime disabled.”

**Not decided here:** precise process/UID allocation, which trusted component owns DB writes, how independent cleanup is realized on F1, migration shape, target durability, qualification results, implementation authority or public activation. If any mandatory gate cannot be met, **the correct outcome is no public Worker generation**, not a substitute backend or weakened security condition.

## 11. No-action attestation and references

This file is an **off-repository draft** produced from the uploaded inspection report and existing supplied architecture alternatives record. It does **not** modify the user's repository, database, Azure deployment, GitHub issues/PRs, runtime or governance files. No live diagnostics, tests or migrations were performed.

Repository evidence locations cited by the inspection:

- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs:12–60`
- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteSchemaBootstrapper.cs:8–11,118–198`
- `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs:5–18`
- `src/AIQuantTradingResearch.Worker/Program.cs:18–44,113–147`
- `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs:22–36,94–113`
- `Dockerfile:23–24,42–51`; `container/entrypoint.sh:5–24,64–97,126–157`
- `python/presentation/historical_market_bridge.py:244–279,441–458`
- `python/public_runtime/bootstrap.py:13–45`, `broker.py:4–48`, `supervisor.py:10–58`
- `eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1:5,17`
- `eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/deploy-f1-reference.ps1:35`

These are **citations reported by the read-only Codex inspection**, not freshly verified line references against the current working tree. Revalidate before any further owner-authorized action.

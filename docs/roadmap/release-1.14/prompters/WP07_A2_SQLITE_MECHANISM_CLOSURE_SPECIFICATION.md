# WP07 A2 + SQLite Contract Closure Specification

**Status: proposed contract for review; NO-GO for implementation selection.** This specification adopts no architecture and authorizes no changes.

## A. Baseline, scope and no-mutation attestation

Current local baseline:

- Branch: `draft/release-1.14-wp07-partial-public-disabled`
- HEAD: `67893f51931ee183f71050a83b81127e6e648b87`
- `PUBLIC_RUNTIME_VERIFIED = False`: `python/presentation/historical_market_bridge.py:244`.

Starting and ending `git status --short` matched: six modified tracked files, sixteen untracked prompter documents, three untracked Python tests and untracked `python/public_runtime/`.

The inspected reports and ADR are untracked:

- `docs/roadmap/release-1.14/prompters/WP07_SQLITE_CONTAINER_IDENTITY_EVIDENCE_REPORT.md`
- `docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_ARCHITECTURE_DECISION_RECORD.md`
- `docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_MECHANISM_CLOSURE_SPECIFICATION.md`

The four governance documents contain local modifications. Their C2 approval does not imply that every uncommitted Option B proposal is approved.

No files were created or modified. No application, Python/.NET process, database connection, PRAGMA, build, test, container, Azure call or GitHub operation ran. No secrets or database contents were accessed.

## B. Source-evidence ledger


| Current fact                                                                                                               | Exact source reference                                                                                                                                        |
| ---------------------------------------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| SQLite uses configured`DataSource`, `ReadWriteCreate`, optional parent creation, schema bootstrap and verified DELETE mode | `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs:12–38,53–60`                                                       |
| Configuration rejects blank database path, without canonical-path or symlink validation                                    | `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteStorageConfiguration.cs:5–18`                                                            |
| Bootstrap targets version 4, initializes/migrates/validates transactionally                                                | `src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteSchemaBootstrapper.cs:8–11,118–198`                                                     |
| Existing observation, snapshot and experiment stores commit transactions                                                   | `SqliteHistoricalObservationStore.cs:164–196`, `SqliteDatasetSnapshotStore.cs:144–171`, `SqliteExperimentResultStore.cs:140–155`, in that SQLite directory |
| No production admission table identified                                                                                   | `SqliteHistoricalObservationSchema.cs:9–16`, `SqliteDatasetSchema.cs:12–81`, `SqliteExperimentResultSchema.cs:10–61`, in that directory                    |
| Public composition returns through synthetic-only services before private persistence composition                          | `src/AIQuantTradingResearch.Worker/Program.cs:28–44,113–147`; `src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs:22–36,94–113`             |
| Image fallback is`/runtime/aiq.sqlite`; named `aiq` owns `/app` and `/runtime`; runtime starts root                        | `Dockerfile:23–24,42–49`                                                                                                                                    |
| Public/private startup drops to`aiq`; private parent permissions use numeric 1000                                          | `container/entrypoint.sh:13–24,64–97,126–157`                                                                                                              |
| Public Popen does not switch UID/GID; environment is rebuilt; one launch-gate FD is passed                                 | `python/presentation/historical_market_bridge.py:246–251,454–458`                                                                                           |
| Offline broker and Worker both use 1003 metadata                                                                           | `python/public_runtime/bootstrap.py:13–14`; `python/public_runtime/broker.py:4–5,20–24`                                                                    |
| No live broker or independent verifier                                                                                     | `python/public_runtime/broker.py:39–48`; `python/public_runtime/supervisor.py:10–13,25–27,61–62`                                                          |
| Controller still calls strict`QuotaGate`                                                                                   | `python/public_runtime/supervisor.py:30–33`; `python/public_runtime/quota.py:55–84`                                                                         |
| Offline authority uses separate JSON/lock files                                                                            | `python/public_runtime/journal.py:141–222`                                                                                                                   |
| Image does not copy`python/public_runtime`                                                                                 | `Dockerfile:37–40`                                                                                                                                           |

These inspected mechanisms match the previous report. No runtime observations were obtained. Explicit SQLite synchronization, busy-timeout and pooling settings remain unidentified in the inspected adapter; runtime defaults are not inferred.

## C. Role/access and A2 launch-control contract

### Candidate role matrix

**DESIGN INFERENCE:** numeric UID/GID values and parentage are TBD pending a collision-free, qualified launch design. No unprivileged role receives supplementary groups granting private access.


| Role                          | Candidate responsibility and parentage                                                        | SQLite access                                                     | Other access and authority                                                                                     |
| ------------------------------- | ----------------------------------------------------------------------------------------------- | ------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------- |
| Finite root bootstrap         | Fixed startup preparation, then permanent privilege drop; creates only approved trusted roles | Controlled provisioning only                                      | No public request surface; no resident privileged helper; closes unrelated descriptors                         |
| Public UI                     | Unprivileged child; submits bounded requests                                                  | None: file, parent traversal, sidecars and alternate paths denied | Read-only application code; request socket only; no direct Worker creation or cleanup evidence                 |
| Trusted supervisor            | Coordinates admission, registration, execution and cancellation                               | Prefer no raw DB access                                           | Authenticated authority/broker/verifier channels; cannot assume cross-UID signaling                            |
| SQLite admission authority    | Trusted component owning admission transitions                                                | Existing approved DB and necessary sidecars                       | Fixed bounded operations; no arbitrary SQL or private-data response                                            |
| Broker                        | Trusted constrained launcher, distinct from Worker                                            | Prefer none                                                       | Fixed executable/environment; launch authorization and cleanup coordination only                               |
| Public Worker and descendants | Unprivileged execution boundary created only after reservation                                | None, including inherited DB handles                              | Synthetic cache and necessary runtime files only; no private credentials, provider network or broker authority |
| Private research Worker       | Trusted private execution preserved                                                           | Legitimate private database access                                | Existing private functionality; no anonymous admission/settlement commands                                     |
| Independent verifier          | Trusted observer outside Worker/broker failure boundary                                       | No direct state clearing                                          | Supplies generation-bound kernel evidence; control privileges separately justified                             |

Trusted code/configuration must be immutable to public identities. Public roles must not inspect trusted environment, memory or descriptors through `/proc`; access needed for their own runtime must not expose trusted processes.

Database protection covers canonical parent paths, directory traversal, file replacement, symlinks, rollback journal and residual WAL/SHM files. Startup ownership changes must not grant public roles access.

**Current mismatch:** shared `aiq` startup and `aiq`-writable application code do not establish this matrix. Different account names, clean environment and `close_fds=True` are insufficient.

A broker with raw private DB access cannot safely share unrestricted credentials/filesystem with Worker. The preferred documentary separation is a DB authority without public Worker privileges and a broker without raw DB access. This remains a proposal.

“Sole admission-state writer” does not mean sole SQLite-file writer. Legitimate private processes continue writing research tables. Because file permissions cannot enforce table-level isolation, all raw private DB writers remain trusted for database integrity.

### Candidate launch sequence

1. UI sends a strict bounded request over authenticated IPC.
2. Supervisor binds it to the admission domain and asks the authority to reserve.
3. Authority checks state, health and C2 history, then commits occupation and full charge.
4. Broker receives an authorization bound to generation, state revision, request digest, fixed executable and approved boundary.
5. A constrained launch mechanism registers the execution boundary before computation.
6. Authority records potential-start state before START delivery.
7. Broker permits exactly one creation/start for that generation.
8. Cancellation requests cleanup but does not clear occupation.
9. Independent verifier supplies whole-boundary death proof.
10. Authority commits settlement; rolling history remains.

Replay, duplicated delivery and acknowledgment loss cannot produce another launch. A restarted broker must deny ambiguous launch authorization rather than repeat it.

**BLOCKED:** no supported mechanism currently lets a distinct-UID unprivileged broker both create the Worker identity and terminate its full descendant boundary. A finite bootstrap could establish constrained launch/control components before dropping privilege, but their lifetime, authority and isolation are unresolved. No permanent root helper or capability is assumed.

Minimum decisive evidence: a reviewed fixed-role launch/control design, followed by separately authorized F1 evidence of credential drop, authorized cross-UID control, descendant containment and denial of unauthorized control.

## D. SQLite authority, states and failure-window contract

**OWNER CONSTRAINT:** the existing approved `/home` database is the only authoritative storage candidate. No auxiliary lock/JSON journal is allowed as production authority.

Candidate transition ownership:


| State/transition                           | Owner                                                                        | Verification requirement                                                    |
| -------------------------------------------- | ------------------------------------------------------------------------------ | ----------------------------------------------------------------------------- |
| UNINITIALIZED → FREE                      | Separately authorized trusted provisioning actor through admission authority | Approved DB identity/schema lineage; no prior or competing Worker can exist |
| FREE → OCCUPIED                           | Admission authority                                                          | Atomic exclusive occupation plus full charge; successful durable commit     |
| OCCUPIED launch-state updates              | Admission authority                                                          | Authenticated broker evidence bound to the same generation/revision         |
| OCCUPIED → QUARANTINED                    | Admission authority where writable                                           | Uncertainty remains denying even if quarantine cannot be committed          |
| OCCUPIED/QUARANTINED → SETTLEMENT_PENDING | Admission authority                                                          | Independent whole-tree death evidence bound to generation                   |
| SETTLEMENT_PENDING → FREE                 | Admission authority                                                          | Durable settlement commit; charge history preserved                         |

Conceptual durable record requirements—not schema definitions:

- Admission-domain and canonical database identity.
- Provisioning lineage and monotonically advancing state revision.
- Unique generation and authorized owner.
- Reservation/rolling history and clock high-water state.
- Launch intent/status and registered kernel boundary.
- Independent proof provenance and settlement status.

Normal recovery must not create or reset missing state. `ReadWriteCreate` and bootstrap behavior require a separate admission-safe startup contract; they cannot be reused as recovery defaults.


| Failure window                                                 | Contract outcome                                                    |
| ---------------------------------------------------------------- | --------------------------------------------------------------------- |
| Reservation commit fails/acknowledgment lost                   | No spawn; authoritative reread or denial                            |
| Committed occupation, crash before launch                      | Stay occupied; prove no possible surviving launch before settlement |
| Launch acknowledgment lost                                     | Assume tree may exist; full reservation retained                    |
| Duplicate token or broker restart                              | No second launch; ambiguous state denies                            |
| Timeout or cancellation                                        | Cleanup request only; independent proof required                    |
| Authority/broker/supervisor/verifier crash                     | No automatic release                                                |
| SQLite busy/private-write contention                           | Bounded controlled denial; no fallback or budget reset              |
| Missing/corrupt DB, missing admission state, unexpected schema | Deny; no clean initialization                                       |
| Mount/I/O or durability uncertainty                            | Deny; do not infer successful commit                                |
| Clock rollback/restart                                         | Preserve occupation/history; no replenishment                       |
| Settlement acknowledgment lost                                 | Resolve durable state or remain occupied                            |

Transactions finish before execution; no transaction is held for Worker lifetime. Schema-v4 compatibility, approved future migration discipline, DELETE-mode locking and durability must be specified and qualified. Private writers may cause contention; their legitimate behavior must remain preserved.

If all trusted actors die and only records remain, occupation remains. Reconstructed PIDs or elapsed time do not provide recovery proof.

## E. Independent whole-tree death-proof contract

**DESIGN INFERENCE:** proof must bind generation, database lineage/revision, execution-boundary identity, boot/start identity, observer identity and complete termination evidence. Worker/broker assertions alone are not independent proof.


| Candidate                      | Contribution                                                | Limitation                                                                                               |
| -------------------------------- | ------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------- |
| Private PID namespace          | Potential closed descendant boundary                        | Creation, escape prevention, destruction semantics and cross-UID control need F1 evidence                |
| pidfds                         | Stable identity and exit observation for specific processes | Root exit is not all descendants; lost handles cannot be reconstructed as equivalent proof automatically |
| cgroup boundary                | Potential membership/accounting/control mechanism           | Writable delegation was not demonstrated; cannot be assumed                                              |
| Process groups or`/proc` scans | Supplemental discovery                                      | Reparenting, escape and races prevent treating them alone as complete proof                              |

A candidate PID-namespace approach would keep an observer outside the Worker boundary, register it before START, prevent escape and verify complete boundary termination through trusted kernel evidence. It is **not a qualified mechanism**.

Broker death, combined broker/supervisor death, verifier loss, orphaning and recycle overlap must preserve occupation. If proof does not survive a failure, the safe outcome is quarantine/indefinite denial.

PID absence, successful signal, root-process exit, timeout, lease expiry and flock release never clear occupation. If allowed F1 facilities cannot supply complete independent proof, the design remains **BLOCKED**.

## F. Global topology and Azure F1 dependencies

Every enabled instance, slot, revision, process restart and recycle-overlap contender must participate in the same authoritative database lineage and exclusion protocol. Unsupported public entry points remain disabled.

Necessary evidence:

- Exact approved database/file and mount identity.
- Shared DELETE-mode locking and atomicity.
- Persistence of acknowledged commits after failure.
- Database/sidecar access exclusion.
- Cross-instance consistency and stale-mount/split-brain prevention.
- Deployment/recycle topology and common authority.
- No `/runtime/aiq.sqlite` admission fallback.

Deployment scripts show intent, not current settings:

- `eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/configure-persistent-sqlite.ps1:5,17`
- `eng/azure-cli/r1.12-deployment/wp03-ghcr-azure-f1/deploy-f1-reference.ps1:35`

A later sanitized inventory needs separate authority for nonsecret path/settings, topology, image and mount evidence. Any container diagnostic or durability/failure test needs its own explicit scope. No private database contents or destructive production test are implied.

## G. C2 accounting, health and sampling contract

Exact C2 wording is approved in the working-tree governance amendment, while implementation and qualification remain pending: strategy `309–341`; execution plan `164–180`; manifest `228–242`.

Frozen code still imposes C1-style quota evidence through `supervisor.py:33` and `quota.py:55–84`. This is a compatibility gap, not permission to bypass the validator.

Candidate limits remain:

- 16 reserved CPU-seconds per actual/uncertain spawn.
- Three admissions/900 seconds; fifteen/86,400 seconds.
- Reservation caps 60/240 seconds.
- Warm Worker spawns charged identically.
- Supplementary attempt/session and cached-read limits remain mandatory.

`15 × 16 = 240` reserved seconds; `15 × 20 = 300` measured job seconds, before additional attributable overhead not already included. These are different quantities. Qualification must avoid double counting and assess total Worker/setup/broker/guardian/supervision/cleanup/idle/orphan CPU plus uncertainty against ≤10% of a verified applicable daily allowance. Other app/ingress costs remain health risks. No hard CPU ceiling or actual quota headroom is claimed.

Candidate global health breaker:

- Inputs: independent local health latency/errors and admission/storage/accounting health.
- Missing required evidence or approved unsafe conditions deny generation.
- Freshness, thresholds, hysteresis and sustained-safe recovery remain TBD.
- Recovery does not clear occupation or replenish budgets.
- Unknown Azure quota telemetry alone does not deny qualified C2 admission.

Proposed correction only:

> Qualification observers issue at most one check per ten seconds per observer, within a finite declared batch. A five-minute single-observer batch schedules at most 30 checks. Loaded p95 must satisfy both ≤2× baseline and the absolute ≤1-second criterion. Report actual sample counts; inadequate evidence is inconclusive. Observer sampling does not define runtime-breaker freshness or throttle platform probes.

Controlling references: strategy `294–295`; definition `411–412`. The qualification plan `94` once/second proposal is outdated.

## H. Documentary discrepancy/correction ledger

All corrections require **separate exact-file documentation authority**.


| File/lines                                                                    | Proposed correction                                                                                                                            |
| ------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| `prompters/WP07_A2_SQLITE_ARCHITECTURE_DECISION_RECORD.md:5`                  | Replace BTC/USD/ETH/USD preview wording with approved BTC-like simulation identity and twelve selections. Governing strategy:154,181.          |
| Same ADR:13,166–170                                                          | A2 remains preferred for qualification, not exclusively adopted. Existing SQLite is sole candidate by owner constraint.                        |
| Same ADR:13,121–125                                                          | Exact C2 wording approved; integration/numerical qualification/activation pending. Governing strategy:309–328.                                |
| Same ADR:172–176                                                             | Record original off-repository origin and current untracked repository location.                                                               |
| `prompters/WP07_READ_ONLY_QUALIFICATION_PLAN.md:52–63,103`                   | Mandatory local safeguards remain; quota-source qualification is conditional on its use under C2.                                              |
| Same plan:94                                                                  | Apply proposed once/ten-seconds correction and absolute loaded p95 criterion.                                                                  |
| Frozen journal description,`python/public_runtime/journal.py:141–222`        | Document as historical offline scaffold, not permitted production authority. Any source replacement requires separate source/schema authority. |
| Packaging/identity descriptions,`Dockerfile:37–49`; entrypoint:13–24,86–97 | Record missing package integration/shared-account mismatch. Correcting runtime requires separate Docker/entrypoint/source authority.           |
| Governance references to conditional B2, strategy:313; manifest:232,240       | Clarify the later sole-existing-SQLite phase constraint; no alternate resource candidate is available in this phase.                           |

`prompters/` above means exactly `docs/roadmap/release-1.14/prompters/`. No proposed correction was applied.

## I. Feasibility and implementation-selection decision


| Mechanism                                | Classification                      | Failure mode / minimum decisive evidence                                                                                 |
| ------------------------------------------ | ------------------------------------- | -------------------------------------------------------------------------------------------------------------------------- |
| Synthetic-only composition               | **FEASIBLE BY REPOSITORY EVIDENCE** | Existing registration; filesystem confidentiality still separate                                                         |
| SQLite transaction interface             | **FEASIBLE BY REPOSITORY EVIDENCE** | Existing stores; not admission/durability proof                                                                          |
| SQLite admission protocol                | **CONDITIONALLY FEASIBLE**          | Duplicate launch/reset risk; complete lineage/transaction/replay contract, then separately authorized schema/source work |
| Private DB/sidecar isolation             | **BLOCKED**                         | Shared rights expose private data; role/mount/FD boundary and authorized negative qualification                          |
| A2 launch/cancellation                   | **BLOCKED**                         | No cross-UID control mechanism; reviewed constrained design and authorized F1 test                                       |
| Independent whole-tree proof             | **BLOCKED**                         | Orphans/combined death; closed-boundary observer/control proof                                                           |
| `/home` crash durability                 | **REQUIRES AZURE F1 EVIDENCE**      | Lost/split state; backend guarantees and authorized qualification                                                        |
| Deployment-global admission              | **REQUIRES AZURE F1 EVIDENCE**      | Concurrent/recycled contenders; topology/common authority evidence                                                       |
| C2 health/abuse integration              | **BLOCKED**                         | Limits/breaker absent; documentary decisions, separate source authority and qualification                                |
| CPU allocation and zero added cost       | **REQUIRES AZURE F1 EVIDENCE**      | Unmeasured overhead/cost; complete resource-specific evidence                                                            |
| Separate JSON/lock production authority  | **BLOCKED**                         | Violates sole-SQLite constraint; cannot be selected in this phase                                                        |
| Auxiliary database/external lock service | **BLOCKED**                         | Forbidden storage/resource fallback                                                                                      |

**NO-GO for implementation selection.** Mandatory blockers are private-file isolation, constrained A2 launch/control, independent whole-tree proof, complete admission/replay/recovery semantics and unresolved health/accounting decisions. Azure durability/topology must also be established.

This does not prove that every permitted design is infeasible. If the existing SQLite mount or allowed kernel facilities cannot meet mandatory gates, continued disabled generation is the outcome—no forbidden fallback.

## J. Minimal separately authorizable milestones

1. Documentary role/access and trusted-writer closure.
2. Documentary launch/replay, SQLite lineage and recovery specification.
3. Documentary independent-proof and combined-failure design; C2 health/accounting decisions.
4. Exact documentary conflict corrections.
5. Separately authorized sanitized Azure inventory.
6. Separately authorized bounded feasibility qualification protecting private data.
7. Only after feasibility: explicit source/schema/container mutation scope.

`PUBLIC_RUNTIME_VERIFIED=False` remains unchanged. No architecture adoption, implementation authority or public activation follows from this specificatio

# WP07 A2 + SQLite Mechanism Closure Specification

Recorded: 2026-10-11 (America/Sao_Paulo), under subsequent owner authority to save the previously presented response. This is a documentary candidate, not an adopted architecture. The findings below record the preceding read-only review; no fresh runtime qualification is implied. The original chat-only restriction was followed during that review; this later instruction authorizes this report file only.

## A. Baseline and no-mutation scope

- Branch: `draft/release-1.14-wp07-partial-public-disabled`
- HEAD: `67893f51931ee183f71050a83b81127e6e648b87`
- Local origin/main: 1 ahead, 0 behind; no fetch.
- `PUBLIC_RUNTIME_VERIFIED = False` at `python/presentation/historical_market_bridge.py:244`.

Inspected current tracked modifications and untracked WP07 records: A2/SQLite ADR, recorded SQLite report, qualification plan, four governance documents, Dockerfile, entrypoint, SQLite sources and offline modules.

Starting/ending git status --short matched: six modified tracked files, fourteen untracked prompter documents, three untracked Python tests and untracked python/public_runtime directory. No repository or local file changed during the original review.

## B. Source-indexed facts and differences


| Fact                                                                        | Current source evidence                                                                                                                             |
| ----------------------------------------------------------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------- |
| ReadWriteCreate, optional parent creation, DELETE journal, schema bootstrap | src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteConnectionFactory.cs:12–38,53–60                                               |
| Schema version 4; transactional initialization/migration                    | src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteSchemaBootstrapper.cs:8–11,118–198                                             |
| Non-deferred private observation transaction                                | src/AIQuantTradingResearch.Infrastructure/Persistence/Sqlite/SqliteHistoricalObservationStore.cs:164–196                                           |
| Existing tables; no admission table found                                   | SqliteHistoricalObservationSchema.cs:9–16; SqliteDatasetSchema.cs:12–81; SqliteExperimentResultSchema.cs:10–61, all in the same SQLite directory |
| /runtime/aiq.sqlite default; aiq owns application code                      | Dockerfile:23–24,42–49                                                                                                                            |
| Public/private startup drops to aiq; private parent uses numeric 1000       | container/entrypoint.sh:13–24,86–97,131–157                                                                                                      |
| Broker plan and Worker metadata both use 1003                               | python/public_runtime/bootstrap.py:13–14; broker.py:4–5,20–24                                                                                    |
| Live broker/verifier unavailable                                            | python/public_runtime/broker.py:39–48; supervisor.py:10–13,25–27,61–62                                                                          |
| Controller still requires strict quota validation                           | python/public_runtime/supervisor.py:30–33; quota.py:55–84                                                                                         |
| Separate admission.lock/admission.json journal                              | python/public_runtime/journal.py:141–222                                                                                                           |
| Image does not copy public_runtime package                                  | Dockerfile:37–40                                                                                                                                   |

No substantive source delta from the earlier SQLite report was found. Additional explicit gaps: filesystem journal does not satisfy sole-SQLite candidate; offline package is not copied into the image. Neither may be silently integrated. A2/SQLite ADR is currently an untracked repository document despite its historical off-repository statement.

## C. Role/credential/filesystem access matrix

DESIGN INFERENCE, not implementation. Numeric UID/GID map TBD; existing 1001/1002/1003 metadata is not adopted.


| Role                       | Candidate credentials/authority                             | DB/sidecars                             | Descriptors/trust                                                                  |
| ---------------------------- | ------------------------------------------------------------- | ----------------------------------------- | ------------------------------------------------------------------------------------ |
| Finite root bootstrap      | Startup-only privilege, no public parsing, permanent drop   | Separately authorized provisioning only | Fixed setup; close unintended FDs; immutable trusted code/config                   |
| Public UI                  | Dedicated unprivileged identity; no direct Worker launch    | None                                    | Bounded request IPC only; no private environment/handles/cleanup authority         |
| Supervisor                 | Dedicated trusted identity; coordinates lifecycle           | Prefer none directly                    | Authenticated channels; no assumed cross-UID signal permission                     |
| SQLite admission authority | Dedicated trusted principal; sole admission-state writer    | Existing DB and required sidecars       | Fixed bounded operations, no arbitrary SQL/private output                          |
| Broker                     | Distinct from Worker; fixed executable/invocation           | Prefer none                             | One-use authorization; no Worker-accessible control endpoints/FDs                  |
| Worker/descendants         | Dedicated least-privileged identity; controlled boundary    | None, including parent traversal        | Bounded request/output; no private mounts/provider network                         |
| Private research Worker    | Trusted private role, compatibility preserved               | Legitimate private tables               | No anonymous surface/admission clearing operation                                  |
| Independent verifier       | Trusted observer separate from Worker/broker failure domain | No direct settlement writes             | Kernel-backed evidence routed to authority; control privilege separately justified |

Current named aiq account is shared; public Popen has no credential transition and /app is aiq-owned. Sources: Dockerfile:42–49; entrypoint.sh:13–24,131–157; historical_market_bridge.py:454–458.

A broker with private SQLite access cannot safely share unrestricted identity/filesystem with Worker. Alternatives to investigate, not adopt: dedicated DB authority with no broker DB access; or trusted combined supervision/admission component with separate broker/Worker.

Private raw DB writers are within the trusted integrity boundary; SQLite file access is not per-table authorization. Sole admission writer means sole authorized transition component, not sole writer of the whole DB. Required protections: canonical file/path identity, public-inaccessible private directory, sidecar creation/replacement, symlink resistance and no startup chown granting public rights. Existing lexical checks/parent chown do not establish them (entrypoint.sh:64–90).

## D. Admission authority and SQLite contract

Candidate dedicated trusted authority owns admission transitions; supervisor requests, broker launches, verifier supplies proof. Private stores remain legitimate DB writers without public admission-modification exposure. No SQL/table/migration supplied.


| State                 | Permitted transition                                                                              |
| ----------------------- | --------------------------------------------------------------------------------------------------- |
| UNINITIALIZED         | Controlled first provisioning after exclusion of prior/competing Workers; never ordinary recovery |
| FREE                  | Atomic validation/occupation/full charge to OCCUPIED                                              |
| OCCUPIED              | Registration/start bookkeeping; no competing admission                                            |
| QUARANTINED           | Uncertain state/ownership/storage/cleanup; deny until authoritative reconciliation                |
| SETTLEMENT_PENDING    | Correct-generation independent death evidence; still occupied                                     |
| FREE after settlement | Durable commit, rolling history retained                                                          |

Occupation needs unique generation, lineage/revision, owner, launch status, registered boundary and proof reference. Restart never creates clean lineage.

Pre-spawn: verify initialized state, canonical DB identity, schema compatibility, topology/health/C2 limits; atomically occupy/charge; obtain unambiguous commit; authorize one-use launch; end transaction before execution. Token alone is insufficient: ambiguous delivery/restart/replay cannot create a second launch. Durable launch tracking and broker fail-closed protocol remain needed; SQLite/process creation are not one atomic operation.

Uncertain reservation commit means no spawn and reread/deny. Uncertain spawn means possible live tree/full charge. Cancellation does not settle. Missing DB/state, unexpected version, failed integrity or rollback denies. Uncertain settlement reconciles or remains occupied. Busy/private contention gets bounded denial, no fallback/reset.

ReadWriteCreate/bootstrap cannot serve as recovery unchanged. Version compatibility, private-write contention and explicit contention/durability configuration need design/authority. Source transactions do not prove Azure persistence.

## E. Independent tree-death proof and combined failures

Concrete investigation candidate — DESIGN INFERENCE:

- One isolated boundary per committed generation, potentially a private PID namespace.
- Register kernel identity before computation: generation, boot/start identity, namespace and available pidfds.
- Independent observer outside boundary; constrained cleanup authority despite distinct UIDs.
- Prevent escape, inherited control FDs and untracked execution outside boundary.
- Settle only from independently observed entire-boundary termination/destruction bound to generation.

Not implemented/qualified. Namespace creation/lifecycle, cross-UID control, escape resistance and observer access need actual F1 evidence. Proc snapshots are not race-free whole-tree proof.


| Failure                          | Treatment                                                                            |
| ---------------------------------- | -------------------------------------------------------------------------------------- |
| Worker exits, descendants remain | Occupied; root exit insufficient                                                     |
| Broker dies                      | Observer/control must survive; otherwise quarantine                                  |
| Supervisor/broker combined death | Occupation persists; independent recovery proof                                      |
| Verifier dies/loses handles      | No settlement; metadata cannot reconstruct equivalent proof automatically            |
| PID reuse                        | Reject generation/start mismatch                                                     |
| Restart/recycle overlap          | Shared occupation; no boot reset                                                     |
| All supervisors disappear        | Deny absent independently authoritative exclusion of prior boundary/competing launch |

Cross-UID launch/cleanup is BLOCKED. No persistent privileged helper, unrestricted capability or working guardian assumed. If permitted F1 facilities cannot supply independent proof, public runtime stays blocked.

## F. Deployment/global authority and Azure gaps

OWNER CONSTRAINT: existing approved /home SQLite is sole authoritative storage candidate. All entry points/launchers/instances/slots/revisions/recycle overlaps must share authority and lineage; unsupported paths disabled. Same path string is insufficient.

Require actual approved file/backend identity, shared locking, atomic commit, acknowledged persistence, no stale-mount split authority, fail-closed I/O and no /runtime fallback. Configuration intent only: configure-persistent-sqlite.ps1:5,17 and deploy-f1-reference.ps1:35 in their eng/azure-cli/r1.12-deployment directories. No live settings checked.

Future separately authorized sanitized inventory: app/slot/region/instance topology, recycle behavior, digest, nonsecret setting/path and observable mount identity. Container diagnostics need explicit scope; exclude secrets/rows. Later contention/crash/sidecar/recycle/combined-failure tests need separate authority; no destructive private production DB tests by inference.

## G. C2 accounting, health and sampling

Exact C2 approval is recorded in docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md:309–341. Frozen supervisor.py:33 and quota.py:55–84 still require authenticated <=60-second evidence and >20% headroom. Not C2 integration; no bypass without mandatory safeguards.

- 3 × 16 = 48 reserved seconds within 60 cap.
- 15 × 16 = 240 reserved seconds, exhausting daily cap.
- 15 × 20 = 300 measured job seconds before additional non-attributed costs.
- Different quantities, not an arithmetic contradiction. Total attributable CPU/uncertainty must fit <=10% of verified allowance; no allowance invented.

All actual spawns including warm reads charged. Attempts/cache limits supplement, never grant extra spawns.

Candidate global breaker uses independently obtained local health latency/errors and authority/storage/accounting health before costly work. Missing required evidence/unsafe conditions opens it. Approved freshness/hysteresis/sustained-safe recovery never clears occupation or replenishes history. Thresholds unresolved.

Proposed correction, not applied:

> Qualification observers SHALL issue at most one health check per ten seconds each, within a finite predeclared batch. For a five-minute single-observer profile, at most 30 probes are scheduled. Loaded health p95 SHALL meet both the two-times-baseline criterion and the absolute one-second criterion. Report actual samples; insufficient evidence is inconclusive. This sampling cap is neither a runtime-breaker freshness rule nor permission to throttle platform probes.

Strategy:294–295 and definition:411–412 take precedence over qualification plan:94 once/second proposal. No edit performed.

## H. Documentary conflict/correction matrix


| Claim                       | Controlling evidence               | Correction proposed                                                                     | Authority                           |
| ----------------------------- | ------------------------------------ | ----------------------------------------------------------------------------------------- | ------------------------------------- |
| ADR BTC/USD/ETH/USD         | Strategy:154,181; frozen contracts | Approved BTC-like simulation/twelve selections, no spot labels                          | Exact ADR edit                      |
| Exclusive A2 adoption       | ADR:13,166–170; strategy:313      | A2 preferred only; existing SQLite sole candidate by owner constraint                   | ADR edit; adoption separate         |
| C2 only in principle        | ADR:13,121–125; strategy:309–328 | Exact wording approved; qualification/integration/activation pending                    | ADR edit                            |
| Off-repository ADR          | ADR:172–176; current location     | Historical origin, now untracked repository proposal                                    | ADR edit                            |
| Mandatory C1 gate in plan   | Plan:52–63,103; strategy:323      | Quota-source qualification conditional on use; unknown alone does not deny qualified C2 | Plan edit                           |
| JSON journal vs sole SQLite | journal.py:141–222                | Frozen historical scaffold, not allowed production backend                              | Documentation then source authority |
| Package absent from image   | Dockerfile:37–40                  | Record integration gap, no implied runtime                                              | Documentation then Docker authority |

ADR references mean docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_ARCHITECTURE_DECISION_RECORD.md; plan references mean WP07_READ_ONLY_QUALIFICATION_PLAN.md in that directory. Strategy/definition references mean the exact governing files cited above and docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md.

## I. Mechanism feasibility/go-no-go


| Mechanism                      | Classification                  | Minimum decisive evidence                                   |
| -------------------------------- | --------------------------------- | ------------------------------------------------------------- |
| Synthetic composition          | FEASIBLE BY REPOSITORY EVIDENCE | Isolated registration; not filesystem proof                 |
| SQLite transaction interface   | FEASIBLE BY REPOSITORY EVIDENCE | Existing stores; not admission/durability proof             |
| SQLite occupation protocol     | CONDITIONALLY FEASIBLE          | Lineage/init/launch/replay/transaction design               |
| DB/sidecar exclusion           | BLOCKED                         | Role/mount/FD boundary resolving shared account             |
| A2 launch/cleanup              | BLOCKED                         | Constrained cross-UID mechanism                             |
| Independent whole-tree proof   | BLOCKED                         | Observer/control survives combined failure or safely denies |
| /home durability               | REQUIRES AZURE F1 EVIDENCE      | Backend guarantees/authorized qualification                 |
| Global exclusion               | REQUIRES AZURE F1 EVIDENCE      | Topology/common authority/recycle evidence                  |
| C2 integration                 | BLOCKED                         | SQLite admission/global breaker/abuse                       |
| CPU allocation                 | REQUIRES AZURE F1 EVIDENCE      | Complete attribution/uncertainty/verified allowance         |
| Zero additional recurring cost | REQUIRES AZURE F1 EVIDENCE      | Resource-specific preservation                              |
| Activation                     | BLOCKED                         | Mandatory gates/acceptance/activation authority             |

No complete candidate ready for implementation selection. Proven interfaces do not close confidentiality/no-overlap.

## J. Next separately authorizable milestones

1. Documentary exact roles, DB boundary/writer and A2 control argument.
2. Documentary SQLite lineage, recovery, replay/settlement/private compatibility.
3. Documentary independent proof/combined failure, C2 breaker/accounting and conflict correction.
4. Separate sanitized Azure inventory authority.
5. Separate bounded qualification authority protecting private DB.
6. Only after feasibility: literal implementation/schema/integration authority.

## K. Original final comparison and attestation

Original final git status --short matched initial status. No file/source/schema/database/Git/Azure/GitHub mutation. No build/test/SQLite/application/container/diagnostic ran. PUBLIC_RUNTIME_VERIFIED=False unchanged.

**Outcome: mechanism closure incomplete.** Public runtime blocked on private-file isolation, constrained cross-UID control and independent tree proof; Azure SQLite durability/global topology also unqualified.

This subsequent recording adds only this report. Historical inspection findings are not new verification and do not authorize implementation, tests, schema changes, cloud actions or activation.

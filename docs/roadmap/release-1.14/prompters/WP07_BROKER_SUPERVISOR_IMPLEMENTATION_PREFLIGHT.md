# WP07 broker-and-supervisor implementation preflight

**Date:**2026-10-10 (America/Sao_Paulo). **Scope:**read-only source/document inspection and official API documentation; no tests, probes, repository/cloud/GitHub changes. Existing four documentation modifications and three untracked prompters preserved. Owner conditional ADR decisions govern; proposed files/mechanisms below are not implementation permission or proven feasibility.

## 1. Exact literal-path mutation proposal

Propose exactly17paths for separately authorized bounded offline implementation, contingent on the mechanism decisions and blockers below. Existing files are modified only for their specified seams; new files contain no synthetic/indicator port.

| # | Literal path | Status / responsibility |
|---|---|---|
|1|`Dockerfile`|Existing: fixed public role accounts, immutable root-owned code, copy runtime package; preserve private aiq identity and package pins |
|2|`container/entrypoint.sh`|Existing: public-only one-time bootstrap dispatch; preserve private mode; no production activation |
|3|`python/presentation/historical_market_bridge.py`|Existing: bounded IPC client replacing public direct launch ownership; retain canonical response validator and disabled gate |
|4|`python/presentation/test_historical_market_bridge.py`|Existing: framing/denial/strict response/client regressions |
|5|`python/presentation/realtime_financial_visualization.py`|Existing: supplementary attempt/session controls, bounded public status; no UI redesign |
|6|`python/presentation/test_market_research_ui.py`|Existing: session/cooldown/TTL/denial/navigation negatives |
|7|`python/presentation/test_public_runtime_isolation.py`|Existing: replace sole cgroup policy assumptions with explicit supervisor/broker boundaries; retain real-cgroup evidence distinction |
|8|`python/public_runtime/__init__.py`|New: inert package marker, no startup/import side effects |
|9|`python/public_runtime/bootstrap.py`|New: finite fixed-role initial setup/fork/credential drop, no privileged resident launcher |
|10|`python/public_runtime/supervisor.py`|New: sole admission authority, capture/state/abuse coordination, bounded service lifecycle |
|11|`python/public_runtime/broker.py`|New: fixed Worker-identity launch/guardian/reap role; no independent budget authority |
|12|`python/public_runtime/journal.py`|New: bounded protected journal, atomic transitions/rolling histories/clock safeguards |
|13|`python/public_runtime/ipc.py`|New: role-checked Unix IPC/framing/schema/descriptor policy |
|14|`python/public_runtime/quota.py`|New: evidence schema/freshness/headroom validation; unavailable default; no owner token or assumed live backend |
|15|`python/presentation/test_public_supervisor.py`|New: state, journal, authorization, abusive/cache-read requests and fault-boundary tests |
|16|`python/presentation/test_public_broker.py`|New: bootstrap/roles/handshake/EOF/orphans/capture/isolation contracts; real Linux tests separately gated |
|17|`python/presentation/test_public_quota_evidence.py`|New: freshness/authenticated-source provenance/scope/reset/headroom/replay negatives |

No .NET source/test/project, generator/calculator, private provider, package requirement, CI, schema, deployment script or controlling-document edit is proposed in this implementation allowlist. Existing Program.cs already wires Console.OpenStandardInput; public DI already excludes private provider construction. Run .NET regressions without modifying them. If this17path scope cannot host meaningful tests or requires a dependency/security helper, stop rather than expand it.

Test files are proposed under the existing Python unittest discovery directory to avoid CI/workflow mutation. Runtime package imports must be qualified and resolved from a fixed application root; do not accept caller-controlled PYTHONPATH. Docker currently copies only presentation, so copying the new package requires path1.

## 2. Fixed-role launch sequence and privilege model

**Recommended candidate, not yet implementation-selected:**one-time existing container-root bootstrap, then unprivileged supervisor and unprivileged fixed Worker-role broker. No privileged resident daemon, setuid executable, runtime UID switching by supervisor, new Azure service or background generation.

Proposed numeric identities for review: retain private `aiq`UID/GID1000 unchanged; supervisor1001:1001; public UI1002:1002; broker and isolated Worker1003:1003. Verify absence of collisions at image build and startup; never assume these IDs are free on target. Supervisor/UI/Worker identities are distinct as conditionally approved. Broker sharing Worker host UID is an additional explicit trust/isolation assumption, not a fourth independently protected UID.

Sequence:
1. Root bootstrap chooses a fixed mode, clears inherited environment, verifies immutable code/paths, creates approved IPC directories and checks protected journal provisioning. No network/provider code or request parsing while root. Storage ownership/setup may fail closed; unverified backend never enables admission.
2. Bootstrap creates finite fixed-role control pipes/socketpairs, forks the broker and UI children, and sets each child's supplementary groups, real/effective/saved UID/GID to its fixed unprivileged role before executing immutable code. Root bootstrap then drops permanently to supervisor identity and execs supervisor, potentially retaining container PID1 and parent/reap ownership. No root process remains. Runtime capabilities/saved IDs/FD inventory must be verified; non-dumpable settings are candidates requiring tests.
3. Supervisor verifies fixed broker credentials/identity/readiness and journal state before serving bounded UI IPC. UI readiness alone never enables public generation. Supervisor PID1 behavior and signals require explicit handlers/reap loops, not reliance on default termination.
4. Before per-job helper creation: commit occupied job, reservation, topology and lifecycle state. Send authenticated PREPARE to the fixed broker. Broker creates only constrained setup/guardian code; no Worker before START.
5. Broker under1003 establishes supported user/mount/PID/network isolation, guardian becomes PID1, sends kernel-auditable READY/boundary identities and pidfds where available. Supervisor durably records registered boundary before START. Stream framing prevents duplicate/replayed START and arbitrary executable/path/environment selection.
6. Guardian starts only fixed dotnet Worker executable under the selected isolated identity/view. Supervisor/broker channel owns bounded raw input/output and deadlines. Broker/guardian control descriptors never enter Worker; stdin/stdout/stderr are the only request transport descriptors, plus explicitly necessary harmless runtime descriptors.
7. Broker/guardian perform same-identity cancellation/reap. Supervisor verifies boundary exit evidence and then durably settles/clears. Cross-UID supervisor signal attempts are not presumed permitted. Supervisor can reap direct child broker/UI when parenthood permits; that does not prove reaping arbitrary Worker descendants.

Permission model: UI may connect only to public request socket, no journal/broker control; supervisor alone changes journal and commands broker, but lacks automatic Worker kill permission; broker/guardian may control own1003process tree, cannot change journal; Worker gets only synthetic cache and approved read-only runtime. Worker sharing host UID with broker requires proof against ptrace/signals, outer /proc, control socket/FD access, mutable broker code, ancestor namespaces and filesystem escape. Kernel pidfd possession does not confer kill permission. Source: https://www.man7.org/linux/man-pages/man2/pidfd_send_signal.2.html

Broker death requires guardian EOF/self-exit behavior; supervisor cannot assume direct1003kill authority. Combined failures leave occupation blocked if cleanup is uncertain. Root/operator recovery cannot be invoked through public IPC. If shared broker/Worker identity cannot meet the confidentiality/control boundary, this candidate fails and needs owner-reviewed architecture amendment; do not add privilege or collapse roles.

## 3. Storage backend decision

**Candidate backend:**bounded filesystem journal on the existing Azure persistent /home storage, at proposed `/home/aiq-public-supervision`, owned1001:1001, outside Worker cache and hidden/inaccessible to UI/Worker. No new database, SQLite package, remote store or paid service. **Qualification status:blocked pending durability/permissions proof; not selected as proven production storage.** /tmp is only transient IPC, never authoritative history.

Use one authoritative bounded occupied/reservation/history representation with secure open/ownership/type validation, lock and atomic replacement/flush. Keep cached-read/limiter state bounded; specify restart semantics rather than resetting global counters to grant work. First-time provisioning must be an explicit trusted action; missing initialized state denies rather than creates a clean budget. Journal path/schema are not previously approved cache keys or a schema migration to existing market data.

Proof obligations: actual CIFS ownership/ACL effectiveness, cross-process flock and rename behavior, file+directory flush semantics, crash at every transition, remount/restart/instance-placement, missing/torn/corrupt history, symlink/path substitution, timestamp rollback/high-water safeguards, bounded growth, and preservation of occupation after final-holder death. A mode0700/chown/fsync call succeeding is not sufficient evidence. If the existing mount cannot meet these guarantees, stop; no alternative service or weakening is inferred.

## 4. Quota-evidence acquisition decision

**Initial implementation decision:**provide an evidence adapter/validator whose default source is UNAVAILABLE and therefore denies generation. No operational collector is selected or authorized. This permits focused offline implementation without falsely enabling production or inventing fresh telemetry.

**Preferred acquisition candidate for separate review:**existing Azure quota usage API accessed by a separately authorized least-privilege workload identity, with all credentials isolated from UI/Worker. Managed identity/RBAC enablement, endpoints and platform configuration are NOT authorized by this report or17path proposal. Owner CLI credentials must never be installed in the container. Operator-supplied evidence is an alternative only if its source/authenticity and real observation age can be proven; refreshing a file does not refresh the measurement.

Official quota schema documents currentValue,limit,name,nextResetTime,unit; it does not document an observation timestamp. nextResetTime is a reset time, not freshness. Therefore direct API response is insufficient by itself for the approved<=60second observation-age rule. Fetch/HTTPDate/current wall time cannot be used as fabricated observation time. This is a concrete blocker, not only an authentication task.

Source checked2026-10-10: https://learn.microsoft.com/en-us/rest/api/appservice/web-apps/list-usages?view=rest-appservice-2025-05-01

Required adapter evidence: authenticated authorized source, resource/application/instance scope where applicable, named daily+short quota, counter/unit/limit/reset, trustworthy observedAt, clock/replay sequence and uncertainty. Validate missing/future/stale/rollback/wrongscope/replayed evidence and20%headroom rule with fixed clocks; unknown values deny. Backend timestamps and60second freshness must be separately proven or explicitly reconciled by owner; no undocumented Azure guarantee is invented.

## 5. Dependencies and test impact

Candidate implementation uses CPython3.13 standard library only (Unix sockets/credentials, selectors, os/signal/pidfds, ctypes for Linux APIs, json/time/pathlib); existing gosu and .NET runtime reused. No requirements.txt/NuGet/project/pins changes. Linux-specific ctypes ABI/error/architecture handling and required syscalls need guarded interfaces, no mock-as-kernel-proof substitution. Security guarantees may ultimately require stronger syscall restrictions; no seccomp dependency/capability/helper is assumed available. Any such need stops this scope.

Docker/entrypoint ownership changes must preserve private1000mode, immutable public role code and existing private cache/provider semantics. Existing /app is aiq-owned; public trusted code must not remain writable by public roles. Snapshot/full regression evidence for private startup is mandatory; no live provider calls.

Focused offline coverage:
- Supervisor: state transitions and injected crash points before/after reservation, PREPARE/READY/START, settlement and clearing; token/owner mismatch; malformed/missing journal; rollback; concurrent admission processes; Phase1B final-holder failure stays occupied.
- IPC: kernel peer checks where supported; UI/broker/Worker role confusion; no RECOVER/RESET public command; oversized/duplicate/extra/malformed messages; partial reads, disconnect, deadline, flood; maximum response2MiB and stderr16KiB,8KiB capture chunks,4096B raw Worker request.
- Broker: finite bootstrap role topology and drops, immutable launch/environment, registration prevents Worker creation, EOF/death/replay, TERM-resistant/nested descendants, unrelated process preservation and verified cleanup. Separate policy tests from real UID/userns/kernel tests, which require explicit runtime privileges/probe authority; do not run root fixtures merely because tests exist.
- Quota: all source/age/auth/scope/reset/headroom/accounting negatives and unavailable-default launches zero Workers.
- Abuse/cache: global rolling3/15generation counts and16charges, client rotation/tabs,30globalcached reads/60s and2/1s,6/session/60s,one active cached read,1024records, denial launching no Worker, no queue/counter-eviction grants; bounded health independent of platform probe throttling.
- Preservation: bridge/UI tests plus full offline Application and Infrastructure suites; existing independent synthetic/TA vectors and all12projections without modifying .NET files. Windows unit-policy success is not Linux privilege/isolation evidence. Existing real-cgroup skip remains explicit; it is not a substitute for the new namespace/role tests.

**Cache-hit integration constraint:**existing read service may hit market cache inside a Worker; analytics are computed per response, and current bridge launches a child for service reads. No production Python analytics or new analytics-response cache is authorized. In this minimal scope, every launched Worker, warm or cold, must retain occupied/reservation admission; cached-read limiter additionally governs its work.30cached reads/minute is an upper limit, not a promise of that throughput when3jobs/15min applies. Session rerender may reuse its current validated response without service work. If the owner requires shared cache hits without launching a Worker, that requires a separately reviewed seam/contract/path amendment, not a hidden Python pipeline or startup fixture.

Proposed future commands (NOT RUN): qualified Python interpreter `-m unittest discover -s python/presentation -p test_*.py`, focused unittest modules above, and `dotnet test` existing Application/Infrastructure projects with offline configuration. Linux UID/namespace tests require their own exact bounded execution approval. No workflow/package/test-generated repository artifacts are authorized by this preflight.

## 6. Blockers and proposed implementation sequence

1. Owner explicitly selects fixed-role bootstrap/broker candidate and its trust model; grants exact17path authority (possibly narrower first stage). ADR approval alone does not select a proven broker.
2. Implement offline pure journal/IPC/quota/state validation plus focused tests, keeping production gate false and quota unavailable. No claim of working isolated launch.
3. Separately resolve distinct-identity bootstrap and shared broker/Worker boundary argument. Implement fixed-role startup/lifecycle only after explicit source/path authority, with no privileged resident process.
4. Prepare exact bounded local synthetic proof packet for real roles, permissions, handshake/death/Phase1B/escape and memory/CPU/capture. Obtain separate approval before running active role/namespace tests. Backend storage proof requires the appropriate target-runtime packet; local tempfs tests cannot qualify CIFS.
5. Resolve operational quota source authentication and authoritative timestamps, or generation stays unavailable. Any managed identity/RBAC/configuration or contract freshness change requires separate authority.
6. Integrate bridge/UI only once previous evidence supports it; preserve disabled gate. Verify C#/canonical/private regressions and cache-hit limitations.
7. Separately authorize target F1 lifecycle/isolation/backend/quota and resource/abuse qualification, with original-image restoration and temporary-image cleanup. G-B1–G-B7, owner acceptance and activation remain separate.

**Unresolved blockers:**distinct-identity launch/bootstrap permission selection; Worker-vs-broker same-host-UID isolation; independent cleanup after combined failure; persistent backend durability/ownership; quota API lacks documented observation timestamp and no authorized workload auth exists; broker/startup/health integration unimplemented; target CPU/memory/availability and global abuse guarantees unmeasured; potential syscall restriction need outside assumed dependencies; cache-hit-without-Worker requirement would expand scope.

**Decision:**bounded offline fail-closed implementation can be proposed under the17path allowlist, but operational containment/invocation is not established. Do not enable public generation, claim G-B1–G-B7 pass or deploy on the strength of this preflight. Report is stored outside repository; no implementation paths were created/edited.

# WP07-P3I — Native Interface and Lifecycle Composition Specification

Date: 2026-10-14. Status: design only; independent review and owner decision required.

`PUBLIC_RUNTIME_VERIFIED=False`. Production, public-runtime and Azure gates are **NO-GO**. Language selected: **NO**. Toolchain/ABI selected: **NO**. This document authorizes no execution or implementation.

## Evidence and interpretation

**MUST/STOP** identifies security obligations. **Candidate** identifies possible Linux mechanisms, not qualified ABI declarations. **GAP** identifies missing evidence. Every operation inherits the rule: its owner must verify prerequisites, obtain the specified success evidence, and latch failure on any uncertainty; subsequent readiness, exposure or clean-completion claims are forbidden. Failure containment never converts failure into success.

Local protected branch is `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. Cached `main` and `origin/main` are `dc5e6b4fcbd64865a5a1a71f398f04735ab961d9`; they are not fresh remote evidence. Static acceptance is owner/review history, not proof of merging or native behavior.

Paths below are repository-relative; line references identify inspected local source.

| Evidence | Classification | Source and significance |
|---|---|---|
| Launch topology, endpoint tables, trace | Accepted offline; local untracked | `container/admin_lifecycle_launch_plan.py:81`, `:104`, `:133`, `:366`; authoritative logical ownership and ordering |
| Native-boundary contracts | Accepted offline within standalone scope; local untracked | `container/admin_lifecycle_native_boundary_contracts.py:42`, `:63`, `:193`, `:309`, `:430`, `:440`, `:449` |
| Contract tests | Local offline evidence; not run for P3I | `python/presentation/test_admin_lifecycle_native_boundary_contracts.py:236`, `:249`, `:259`, `:278`, `:290`, `:304`, `:320`; 30 methods, previously reported passing |
| Frozen codec | Frozen design semantics; local untracked | `container/admin_lifecycle_protocol.py:62`, `:182`, `:271`, `:295`, `:324`, `:345`, `:470`; binary layout, bindings, sequence and framing |
| Entrypoint | Tracked, dirty; HEAD also contains this path | `container/entrypoint.sh:5`; disabled/public and private branches are existing alternatives, not P3 native composition |
| Image recipe | Tracked, dirty | `Dockerfile:1`, `:20`, `:22`, `:48`, `:52`, `:62`; mutable tags, legacy bootstrap packaging and partial identity setup |
| Legacy bootstrap | Local untracked; BLOCK for direct reuse unchanged | `container/admin_diagnostics_bootstrap.py:129`, `:145`, `:168`; configuration before fork and router replacing PID1 |
| Legacy Python agent | Local untracked; candidate reference only | `container/presentation_lifecycle_agent.py:147`, `:159`, `:180`; existing hardening/subprocess/relative-clock behavior is not the approved composition |
| Router project and lifecycle | Local untracked; packaging/native GAP | `src/AIQuantTradingResearch.AdminDiagnostics/AIQuantTradingResearch.AdminDiagnostics.csproj:1`; Web SDK/net10.0. `Program.cs:40` registers SIGCHLD; `PresentationLifecycle.cs:98` uses wildcard wait |
| Build/runtime declarations | Local source, not artifact qualification | `global.json:4`, `requirements.txt:6`, `Directory.Packages.props:13`; SDK roll-forward, direct Python pins and YARP declaration do not establish immutable artifact provenance |

Pre-authoring hashes verified: contracts `5231BD87F3ECF828ACD13609EB3A7E56980B4A94C6E3E797F56728984CF65AD2`; tests `B1639A4DC14671B04F664CE556C60E0E037ACCA0BC4DF9A6E6631378F33D4FEA`.

Pre-existing Git-listed inventory: 2,025 unique paths. SHA-256 `760634FB6BB1CF955E566AB8CD8208CF00162EC79204C78B75387D0D963DD7C4`, computed over UTF-8, LF-joined, PowerShell `Sort-Object -Unique` ordered `path:SHA256` entries from `git ls-files --cached --others --exclude-standard`. Final comparison excludes only this newly authorized document. Ignored files, directory attributes, ACLs and internal Git metadata are not exhaustively hashed. Branch, HEAD, full status and `show-ref` are separately captured in session evidence. No secrets are inventoried outside those Git-listed source paths.

## A. Trust graph, lineage and startup phases

| Owner/role | Parent and identity after hardening | Exclusive required-child status ownership |
|---|---|---|
| Root bootstrap, then S | PID1 remains PID1; root becomes S UID/GID1200 | S directly waits R and G |
| Router bootstrap, then R | Direct child of root/S; UID/GID1100 | No ownership of A/G wait status |
| Guardian bootstrap, then G | Direct child of root/S; UID/GID1300 | G directly waits A |
| Agent bootstrap, then A | Forked by G bootstrap before incompatible privilege drops; UID/GID1000 | A parents UI; UI completion is a prerequisite, not D1 |
| UI | Direct child of A; UID/GID1000 | No trusted control endpoints or reaper responsibility |

Allocations MUST remain conditional until the exact image's passwd/group tables and numeric collisions are checked. Root MUST be finite, secret-free and unthreaded or separately proven fork-safe; no privileged helper survives. No root or guardian fork may copy secret-bearing memory/environment. Clearing a secret after fork is insufficient proof. Candidate initialization libraries, logging, allocators and FFI runtimes require fork-safety review.

| Phase | Owner and prerequisites | Success proof | Failure behavior/model relation |
|---|---|---|---|
| Empty endpoint creation | Root; secret-free startup and known inherited inventory | Recorded endpoint identities and complete ownership | STOP on unknown inherited FD; launch trace `:148` |
| R/G forks | Root; no secret-bearing state | Actual positive distinct child PIDs; child receives full parent table | STOP on failed fork/ambiguous PID; `ForkBinding`, trace `:151` |
| G→A fork | G bootstrap; before permanent incompatible drops | Positive A PID bound to numeric G PID | STOP before metadata/exec on mismatch; trace `:162` |
| Metadata emission | Root/G; actual relevant child bindings exist | Exact bounded frozen records, writer ownership and EOF | STOP on partial/misbound data; trace `:155`, `:164` |
| Mapping/hardening | Each bootstrap; complete table and qualified identity operations | Verified FD map and irreversible identity/session state | STOP before managed startup; trace `:173`–`:176` |
| Router preparation | R; all forks complete and separation proved | Router-local synthetic-only credential preparation and closure; R_BOUND | No production credential ingress; trace `:177`–`:180` |
| Gate and agent exec | G/A bootstrap; G_RELEASE, gate metadata/sequence valid | A_EXEC then A_EXEC_ACK, explicit gate closures, exec handoff | ACK is receipt only; trace `:181`–`:184` |
| Readiness/exposure | Executed A/R/S; metadata validated/closed, UI ready | A_BIND/A_READY, UI_READY/R_UI_READY, then R_EXPOSE | No listener on partial readiness; trace `:185`–`:189` |

Privileged preparation MUST finish inside the original five-second deadline; overall startup MUST finish inside its original thirty-second deadline. Scheduling feasibility is GAP. Root's transition to S does not create another parent or reset clocks.

## B. Language-neutral candidate native interface catalog

The following are semantic signatures, not executable declarations. `FD`, `PID`, `UID`, `GID`, `Count`, signal sets/actions, timespecs and wait status are abstract target-dependent types. Headers/bindings MUST establish widths, alignment, calling conventions, constants and layouts before any prototype. libc wrappers versus direct syscall ABI MUST be recorded; no syscall numbers or unverified numeric constants are prescribed here.

| Candidate interface | Semantic inputs → outputs; owner | Error/retry/blocking requirements and portability GAP |
|---|---|---|
| pipe2 / socketpair | Flags; socket family/type/protocol → two owned FDs; root bootstrap | Check complete pair creation and flags. No secret content. Kernel/socket semantics and nonblocking/CLOEXEC support require qualification. Never blindly retry a partially successful allocation. |
| fcntl | FD, operation and operation-specific argument → flags/new FD/result; mapping owner | Distinguish read/modify/duplicate semantics, retain provenance of new alias. Variadic ABI and atomic duplication support are GAP. |
| dup2 / dup3 | Source FD, destination FD, qualified flags → mapped destination | Owner must prove destination vacant or explicitly closed. Same-FD/no-op behavior must be specified; do not assume CLOEXEC clearing. EINTR handling must preserve the ledger. |
| close / optional close_range | Owned FD or verified range/flags → closure result | Record exact ownership release and alias closure. EINTR close/retry semantics vary and require target proof; blind retry can close a reused FD. Unknown closure latches failure. close_range availability is optional/unqualified. |
| fork | Secret-free, fork-safe parent state → positive parent-side PID / child branch / failure | Record direct parent and PID before metadata. Child path must not enter unsafe runtime/library state. Failed fork never creates a binding. |
| execve | Trusted executable path, bounded argument/environment vectors → replacement or error | No success return to caller; error returns fail closed. Post-exec readiness supplies separate evidence. Verify survivor map, sanitization and signal dispositions; no fallback that opens exposure. |
| setgroups | Empty group vector/count → result; privileged role bootstrap | Before group privilege is lost; verify no supplementary groups. Argument element/count ABI must be established. Failure stops hardening. |
| setresgid / setresuid | Real/effective/saved target identities → result | Qualified irreversible ordering, followed by verification of all identities. Partial change cannot be rolled back into acceptance. UID/GID ranges and libc support are GAP. |
| Capabilities / prctl | Qualified operation, typed arguments/capability state → result | Drop retained/permitted/effective/inheritable/ambient capabilities and bounding authority as required by target design; disable regain paths and establish no-new-privileges. Target headers, variadic ABI, drop ordering and post-drop proof are GAP. No retained signal capability. |
| setsid / setpgid | Caller or bound PID and intended session/group → identifiers/result | Establish distinct sessions without cross-UID signaling dependency. PID1/session/group-leader legality and sequencing require proof; failure is not ignored. |
| sigaction / sigprocmask | Signal, qualified disposition/mask → previous state/result | Exclusive owner configures SIGCHLD without auto-reap. Mask changes and inherited exec state are explicit. Structure ABI and atomic poll/mask behavior require review. |
| waitpid / carefully qualified waitid | Exact direct child PID, nonblocking options → pending, status or error | G owns A; S owns G/R. Check returned identity and normal exit-zero. Bounded EINTR, ECHILD fatal for required child. waitid observation/reaping semantics and siginfo layout are unqualified. |
| clock_gettime | Qualified CLOCK_MONOTONIC identity → timestamp | Checked conversion into bounded nanoseconds. No wall clock substitution. clock/time_t ABI and overflow are GAP. |
| poll / ppoll | Bounded owned FD set, remaining original budget, optional mask → events/result | Recompute remaining time after EINTR; never reset deadlines. Readiness/HUP is not a child-exit proof. pollfd/timespec/signal-mask ABI and timeout rounding require qualification. |

For every call, its owner MUST capture errno only on a documented failure return, classify the operation without secret-bearing diagnostics, and stop on unknown outcomes. Candidate blocking filesystem/loader/library operations cannot be assumed bounded by a surrounding timer. Termination interfaces may be designed later for failure containment, but cross-UID kill privileges and namespace teardown are not success mechanisms.

## C. Descriptor provenance and collision-safe mapping

Logical IDs below follow launch plan `:81` and trace `:133`; standard streams and any native/library-created FDs must additionally appear in the actual inventory. Slots are process-local. Original allocation and scratch numbers are **unknown**.

| Endpoint identity | Creation / intended inheritors | Destination and exec state | Explicit closure |
|---|---|---|---|
| S-SR / S-SG | Root; all root-fork children initially copy them, unused copies close | S3 / S4; no managed S exec assumed | S-SR after HOST_DISPOSED; S-SG after positive G wait |
| S-meta | Root; retain root→S only after cleanup | S5; validate before runtime | Once validated, before runtime |
| G-SG | Root; retain G after complete inherited cleanup | G3 | After AGENT_RESULT |
| G-gate | Root; retain G | G4; CLOEXEC backstop | After ACK, before A exec |
| G-meta | Root; G retains; A initially inherits the G FD5 copy | G5; validation before runtime | G closes validated metadata; A closes inherited unused copy before gate FD5 assignment |
| R-key / R-key-writer | Root creates empty ends; retain R only | R3 reader; writer scratch unknown | R writer after router-local synthetic preparation; reader after bounded consumption, before listener |
| R-RA / R-SR / R-meta | Root; retain R | R4 / R5 / R6 | R-RA after valid A EOF; R-SR after HOST_DISPOSED; R-meta once validated before runtime |
| A-gate / A-gate-alias | Root creates base and duplicate; G inherits then A inherits | A base conditional FD5; alias scratch unknown; both CLOEXEC | All A copies explicitly close after ACK before exec; G closes its unused A copies |
| A-RA / A-meta | Root creates; G inherits then A inherits | A3 / A4 survive agent exec, CLOEXEC cleared intentionally | A-meta validates once post-exec then closes before A_BIND; A-RA closes after UI_STOPPED |
| S/G/R/A metadata writers | Root creates empty writers; ownership follows trace | No required exec survivor | Root closes own four copies after bound emission; G closes inherited G/A writers after A binding/emission; every other copy closes unused |

| Boundary / phase | Complete copied inventory and required cleanup |
|---|---|
| Pre-fork root | All endpoint IDs above, A-gate alias and all five writers; standard/native FDs must be reconciled separately |
| Root→R | Full root table is copied; retain R-key, R-RA, R-SR, R-meta, R-key-writer; explicitly close every other copied control/writer/alias |
| Root→G | Full then-current root table is copied; retain G-SG, G-gate, G-meta, A-gate, A-gate-alias, A-RA, A-meta, G-meta-writer, A-meta-writer |
| Root→S transition | Same PID; close own writers and all unused endpoints; retain S-SR, S-SG, S-meta; permanently harden and validate/close S-meta |
| G→A | Full actual G table copied, including occupied G-meta FD5 and writers; retain A-gate/alias, A-RA, A-meta; close all unused G endpoints/writers first |
| G after A fork | Emit only after positive A binding; close writer copies, validate/close G-meta; close G's copies of A endpoints; retain G-SG/G-gate until gate closes |
| A pre-exec | Reconcile closed inherited G-meta FD5, verified vacancy and conditional A-gate FD5 mapping; after ACK explicitly close base/aliases; only RA3/metadata4 survive |
| A post-exec / metadata phase | RA3 and metadata4 preserved exactly; consume/validate bounded metadata once, close metadata4 before binding/readiness |
| A→UI bootstrap | Full current A table copied, including RA3; close it and any other nonstandard control/credential/metadata/gate copy before UI exec |
| UI exec | Zero control/credential/metadata/gate FDs. Empty desired-retained list never proves the inherited table was empty |

Mapping owner MUST reconcile every shared identity's channel, creator, alias relation, position and flags against history (`ExecBoundary:245`). Moving a live source onto a live destination is forbidden. Cycles require evacuation into a **verified vacant**, provenance-recorded scratch slot, followed by ordered moves and explicit old-alias closure. No fixed scratch number is selected. Unknown aliases or inventory contradictions STOP mapping.

No other thread, signal handler, runtime loader or concurrent allocator may race inventory verification and remapping without a separately proved exclusion mechanism. Enumeration via a candidate kernel/filesystem interface is not automatically atomic. EOF proof requires closure of all writer copies in every inheritor; CLOEXEC alone does not prove explicit pre-exec closure. FD5 is a conditional numeric design slot, not native allocation evidence.

## D. Frozen protocol and launch-plan composition

Codec `:62` defines the frozen 64-byte big-endian frame. Native composition MUST use its unchanged roles, message layout, launch ID, bounded fields, reserved-field rules, role/PID bindings and per-direction sequence continuity. `Frames:345` and `Direction:182` provide offline constraints; native short reads/writes, partial EOF, timeout and framing bounds require separate qualification.

| Critical milestone | Responsible owner and native prerequisite | Composition proof / STOP boundary |
|---|---|---|
| Positive fork binding | Root for R/G; G for A | Actual positive distinct PIDs match lineage before relevant metadata emission; symbolic Step is not proof |
| Metadata streams | Emitting root/G; receiving bootstrap/agent | Frozen record validation, exact expected stream, writer-copy EOF, once-only consumption and closure; no readiness based on unvalidated metadata |
| C1 A_EXEC / A_EXEC_ACK | G sends; A bootstrap acknowledges after release | Frozen gate role/sequence/PID/launch/deadline validation plus GateHistory, explicit G/A gate closure; ACK proves receipt only |
| Agent exec and A_READY | A bootstrap execs; executed A validates metadata and reports | RA3/metadata4 survivors, metadata once-validation/closure, A_BIND before A_READY; gate ACK never substitutes |
| UI_READY / R_UI_READY / R_EXPOSE | A/R/S respectively | UI exec isolation and actual readiness precede reports; S exposure grant precedes listener; malformed/duplicate/early reports STOP |
| Shutdown completion | G/S status owners plus R disposal owner | Bound protocol history, explicit endpoint closure ledger, positive waits and timestamped shutdown history all refer to one launch and unchanged deadline |

**Composition requirement:** a future orchestrator MUST cross-bind launch identity, role/PID map, deadline, descriptor identities and phase transitions across codec validation, launch trace, metadata state, hardening/session evidence, exec boundaries and wait/chronology validation. Acceptance is their conjunction, never the union of independent passes. A readiness prefix proves ordered labels only; it cannot prove metadata or hardening. A shutdown ledger proves injected wait/timing consistency only; it cannot prove RA/SR/SG closure or kernel status. No composition implementation exists in this increment. Missing or contradictory evidence STOP the integrated claim.

## E. Identity, sessions, signals and waits

| Obligation | Owner / prerequisites | Required success proof | Failure behavior |
|---|---|---|---|
| Permanent drop | Each bootstrap after its privileged fork/mapping obligations | Real/effective/saved UID/GID match allocated role, empty supplementary groups and capabilities, no-new-privileges, no regain path | Partial drop or unverified capability state STOP; `Hardening:99` is declaration-only |
| Session separation | S/G/R/A/UI, with qualified ordering | Actual trusted/presentation session separation; accepted offline model requires five distinct session declarations | PID1/session-leader incompatibility or collision STOP; no session workaround is selected |
| SIGCHLD discipline | G for A; S for R/G; A for UI | Reviewed disposition/masks, no automatic reaping or competing handler/runtime reaper | ECHILD/status consumed elsewhere latches failure; local legacy wildcard wait is blocked for reuse |
| D1 | G; correct A direct-child binding and shutdown prerequisites | Actual positive child-specific kernel wait, returned A PID, normal exit-zero inside shared budget | EOF, notifier, pidfd readiness, absence, signal death or nonzero status cannot pass |
| D2 | S; D1/AGENT_RESULT and G closure obligations | Independently obtained positive G wait, normal exit-zero after A proof | Guardian report is not kernel proof; competing owners STOP |
| Router wait | S; HOST_DISPOSED and control closure prerequisites | Positive bound R normal exit-zero wait strictly after disposal | Disposal alone is not exit status; errors/signals STOP |
| T2 adopted-child drain | S/PID1 after all required positive waits | Qualified drain followed by terminal no-child evidence and empty required endpoint inventory | Cannot consume required statuses first; arbitrary wait(-1) is not D1/D2 |
| D3 | Containment owner under separate authority | Failure containment only | Namespace/container teardown never establishes normal exit-zero or clean acceptance |

No cross-UID signal capability may be retained. Shutdown cooperation uses owned protocol channels. Normal UI completion and A RA closure precede EOF; UI disappearance alone does not establish D1. Wait return zero is pending, not success. EINTR is bounded by retry cap and unchanged deadline. Required-child ECHILD, wrong target/returned PID or owner is terminal failure. Signal disposition inheritance across exec and any future .NET/CPython runtime reaper must be audited before qualification.

## F. Absolute clocks, shutdown and failures

Owner root establishes startup origin and checked absolute five-second privileged/thirty-second total deadlines. Owner S establishes the one shutdown origin and checked absolute four-second deadline; all roles propagate it unchanged. Conversions and additions MUST reject negative/noninteger/overflow values, backward observations and reset attempts. Equality with deadline is expired. A retry or phase change never receives a fresh relative budget. Candidate I/O polls recompute remaining original time with qualified rounding. Docker's five-second grace is external and does not extend the security deadline.

Shutdown success chain follows launch trace `:190`–`:198`:

1. S requests STOP_ROUTER and STOP_GUARDIAN; R requests STOP_UI using valid bound sequences and shared deadline.
2. A proves UI completion, reports UI_STOPPED and explicitly closes A-RA/all writer aliases. R validates exactly permitted EOF, closes R-RA, then reports R_PROTOCOL_DONE and HOST_DISPOSED.
3. R/S explicitly close their SR ends after disposal. G obtains D1 positive A exit-zero after relevant EOF, emits bound AGENT_RESULT, then closes G-SG.
4. S independently obtains D2 positive G exit-zero strictly after D1, closes S-SG, and independently obtains positive R exit-zero strictly after HOST_DISPOSED.
5. S drains adopted children only after mandatory waits and establishes terminal absence only after drain and endpoint closure. No namespace teardown is success evidence.

The offline timestamp fixture disposal=14, A/G/R waits=15/16/17, drain/absence=18/19 illustrates causal order only, not elapsed native feasibility. Protocol traffic and simultaneous independent activity must still compose with the accepted trace; no messages or deadline semantics are added here.

Fixed existing contract classifications include `GATE_HISTORY_MISSING`, `GATE_HISTORY_INCONSISTENT`, `GATE_EXPLICIT_CLOSE_REQUIRED`, `LIVE_DESTINATION`, `UNBOUND_ALIAS`, `INVALID_STARTUP_PREFIX`, `UNKNOWN_STARTUP_EVENT`, `STARTUP_PREFIX_ORDER`, `DUPLICATE_STARTUP_EVENT`, `STARTUP_PREFIX_TOO_LONG`, `DEADLINE_RESET`, `DEADLINE_OVERFLOW`, `CLOCK_REGRESSION`, `DEADLINE_EXPIRED_OR_REGRESSED`, `ECHILD_REQUIRED_CHILD`, `WRONG_RETURNED_PID`, `NONZERO_CHILD_EXIT`, `COMPETING_STATUS_OWNER`, `POSITIVE_BOUND_WAIT_MISSING`, `ROUTER_WAIT_BEFORE_DISPOSAL` and `DRAIN_BEFORE_WAITS`. Their wire representation is not changed. Native errno-to-internal-classification mapping remains an explicit future design obligation; unknown errno/outcome must fail closed rather than invent clean status. Every failure is sticky, including timeout and interop failure; no later ACK/readiness/absence clears it.

Loader/exec, identity operations, FD closure ambiguity, clock acquisition, partial channel I/O, native wait polling and managed host disposal all have unqualified blocking/termination behavior. A timer declaration does not prove completion inside four seconds.

## G. Candidate comparison and qualification matrix

| Candidate | Design fit | Audit/runtime tradeoff | Qualification status |
|---|---|---|---|
| Minimal C/Linux | Can express finite bootstrap and exact syscall ownership | Small runtime surface, but memory safety and error-path ownership require careful audit | GAP: headers, compiler/linker provenance, target libc/architecture and reproducible artifact |
| Minimal Rust/Linux | Can express same topology with memory safety outside unsafe boundary | Unsafe bindings, allocator/panic policy and dependency surface must remain bounded; fork path needs special review | GAP: binding/layout proof, locked dependencies, toolchain/runtime and target artifacts |
| Python/native FFI | Packaged interpreter exists; semantic interface coverage is possible | Larger privileged initialized runtime, FFI/errno signatures, fork safety and reaper interactions require proof | GAP: interpreter/FFI/runtime qualification; not preferred by authority or selected |
| Unchanged legacy Python/.NET | Does not match root→S/G→A design | Secrets before fork, router replacing PID1, relative timers and wildcard wait behavior | BLOCK for direct reuse unchanged; source remains protected |

| Evidence gate | Required later evidence | Current judgment |
|---|---|---|
| Headers/bindings and ABI | Exact architecture, libc/header versions, type/layout/constant/calling-convention audit; no inferred syscall numbers | GAP |
| Loader/linkage | Explicit glibc/musl choice only after owner decision; dynamic loader/library closure or reviewed static-link limitations | GAP |
| Toolchain/dependencies | Immutable compiler/runtime provenance, reproducible flags/artifacts, dependency/license inventory | GAP |
| Image provenance | Immutable base/final digests, architecture, package hashes; existing mutable tags are insufficient | GAP |
| Numeric identities | Exact image UID/GID1000/1100/1200/1300 collision and account/permission evidence | GAP; existing UID1100 recipe is partial |
| FD/fork safety | Actual inherited enumeration, race exclusion, verified scratch/FD5 vacancy, explicit alias/writer closure and secret-free initialized state | GAP |
| Wait/signal/session | Actual direct-parent exclusive wait status, no auto-reap, PID1 adoption and distinct sessions without privileged helper | GAP |
| Clock/shutdown | Actual monotonic arithmetic, bounded EINTR/I/O/disposal and four-second completion within five-second grace | GAP |
| Router/agent artifacts | .NET executable/runtimeconfig/deps/entrypoint and frozen binary lifecycle integration; Python post-exec metadata and UI handoff | GAP; Web SDK and Main are source declarations, no Library conflict is asserted without evaluated/artifact evidence |
| Validator composition | Cross-bound evidence ledger, all prerequisite validators and no standalone success promotion | GAP |
| Offline source constraints | P3G accepted validators; frozen codec/trace preserved | PASS within static scope only |

## H. Negative cases, traceability and staged decisions

These are future review/qualification requirements, not tests executed or authorized here.

| Counterexample | Responsible owner; rejection/proof expectation | Traceability |
|---|---|---|
| Inherited G-meta still occupies A5 | A bootstrap; close unused copy and prove vacancy before gate mapping; STOP occupied slot | Trace `:161`–`:173`; remap/live destination |
| Move overwrites live RA/meta or scratch is occupied | Mapping owner; evacuate into verified vacant scratch, no silent clobber | `remap:169`; `LIVE_DESTINATION` |
| Alias or writer copy survives exec/EOF | Inheriting owner; complete table reconciliation and explicit closure; reject leaked copy | `validate_inventory:139`, `ExecBoundary:223`; trace `:182`, `:187`, `:191` |
| RA3/meta4 supplied without gate/ACK history | A bootstrap; mandatory C1 evidence even when current gate inventory omitted | `GateHistory:193`, `ExecBoundary:231` |
| Shared gate IDs relabeled credential or alias changed | A bootstrap; reconcile every shared ID, reject contradictory provenance | `ExecBoundary:245`; test `:278` |
| Startup prefix bogus, duplicate, skipped or reordered | Readiness owner; constructor and transition validate exact prefix | `validate_startup_prefix:430`; tests `:290`, `:304` |
| Secret decoded before privileged fork | Root/G bootstrap; refuse fork; later environment clearing is insufficient | `ForkBinding:79`; legacy bootstrap `:129` is incompatible |
| Saved root ID/group/capability remains | Role bootstrap; reject partial drop and regain authority | `Hardening:99`; qualified verification required |
| Trusted/presentation sessions collide | Role bootstrap; reject shared session and cross-UID signal dependency | `validate_sessions:114`; actual sessions unqualified |
| A wrong numeric parent or A/G/R duplicate PID | G/S binding owners; reject complete-set contradiction | `ForkBinding:73`, `validate_wait_set:440` |
| Competing reaper or ECHILD promoted to success | G/S; latch failure, never accept absence/notifier as status | `WaitState:330`; legacy wildcard wait blocked |
| Reset deadline, overflow or retry past boundary | Each clock/I/O owner; reject unchanged-budget violation | `Deadline:48`, `WaitState:333` |
| Router wait before/equal disposal; guardian before/equal A wait | S; reject timestamped causal violation even if status declares exit-zero | `validate_shutdown_history:449`; test `:259` |
| Listener before metadata/hardening/UI/R_EXPOSE | R/S; conjunction of trace, codec, metadata and hardening evidence, not prefix count | Trace `:184`–`:189`, `:310`; standalone composition GAP |
| RA/SR/SG closure omitted but waits valid | Each endpoint owner; cannot claim integrated clean shutdown until ownership table empty | Trace `:191`–`:198`, `:282`, `:322`; standalone ledger cannot prove closure |
| Drain consumes required child status first | S; required positive waits precede adopted-child drain/absence | `Ledger:406`, trace `:317`; native reaper ordering GAP |

Decision sequence: **independent read-only design review → separate owner language/toolchain/ABI decision → separately authorized offline/native prototype → local Linux qualification → packaging/image qualification → Azure/public gates**. No transition is automatic. Prototype paths and procedures must receive a literal allowlist and bounded authority first. Production credential ingress, real authenticated clients and activation remain prohibited. Any required second path, frozen-contract amendment, native claim without evidence or prohibited action STOPs this documentation increment.

## Document exit boundary

This is a source-backed design dossier, not native proof or implementation acceptance. No tests, project imports, builds, compiler checks, dependency installs, syscalls, Docker, Azure, network, GitHub or Git mutations were performed. Only this document was created. Final hash and preservation comparison are supplied in the completion report so this file need not contain a self-referential hash. Next possible owner decision is independent read-only design review, with no implementation authority implied.

`PUBLIC_RUNTIME_VERIFIED=False`; production/public-runtime/Azure **NO-GO**.

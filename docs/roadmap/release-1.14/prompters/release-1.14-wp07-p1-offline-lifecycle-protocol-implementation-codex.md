# Codex Authority — Release 1.14 WP07-P1
## Offline Lifecycle Protocol Implementation and Focused Validation

**Status:** OWNER AUTHORIZED for this bounded work package only. **STOP at completion for owner review.**

### 1. Binding owner authority
The owner expressly authorizes WP07-P1: implement the frozen `aiq-admin-lifecycle-v1` binary protocol in standalone Python and .NET modules, and implement/run focused offline tests. This authority does **not** authorize integration with the enabled process topology, credential or identity changes, Docker, networking, GitHub mutation, Azure or deployment.

Binding prior decisions: A1/A2/A3, D1/strict D2/D3, T1–T11. Specifically T8 conditionally approves S=1200/1200 and G=1300/1300 (no allocation here); T9 freezes the wire contract; T10 approves type-specific byte-7 outcomes and distinct failure variants; T11 approves temporary agent metadata FD 4 across exec (no FD implementation here). Preserve disabled Option A, the four-second shared monotonic shutdown deadline, five-second Docker stop grace, five-second privileged startup limit, 30-second total startup limit, and all existing NO-GO gates.

### 2. Protected baseline and first gate
Expected branch: `draft/release-1.14-wp07-partial-public-disabled`

Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`

The working tree is intentionally dirty. Before writing anything, capture branch, HEAD, full `git status --porcelain=v1 -uall`, and the exact state and hashes of each allowlisted path. **STOP** if branch or HEAD differs, if an allowlisted path contains preexisting work that would be overwritten, or if the proposed changes cannot be isolated without touching other paths. Do not reset, clean, stash, checkout, commit, push, open PRs, or alter GitHub state. Preserve unrelated changes verbatim.

### 3. Exact mutation allowlist
Only these four paths may be created or modified:

1. `container/admin_lifecycle_protocol.py` (new Python protocol module)
2. `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs` (new .NET protocol module)
3. `python/presentation/test_admin_lifecycle_protocol.py` (new Python offline tests)
4. `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs` (new .NET offline tests)

Do not change project files, existing Python or .NET runtime files, entrypoints, Dockerfile, lockfiles, build scripts, documentation, configuration, or dependencies. If the .NET project does not automatically compile the new source or tests, or tests cannot run without a project-file edit, **STOP and report** rather than expanding the allowlist. No new packages.

### 4. Frozen contract to implement, without improvisation
Protocol: `aiq-admin-lifecycle-v1`, fixed **64-byte** records, unsigned big-endian multi-byte fields.

| Offset | Length | Field |
|---:|---:|---|
| 0 | 4 | Magic `AIQL` (`41 49 51 4c`) |
| 4 | 1 | Version `01` |
| 5 | 1 | Sender role |
| 6 | 1 | Message type |
| 7 | 1 | Type-specific outcome/flags |
| 8 | 16 | Nonzero per-launch ID |
| 24 | 4 | Sequence |
| 28 | 4 | Sender PID |
| 32 | 4 | Related PID |
| 36 | 8 | Absolute startup deadline (monotonic nanoseconds) |
| 44 | 8 | Absolute shutdown deadline (zero during startup) |
| 52 | 12 | Reserved, all zero |

PID bounds 1..2,147,483,647; related PID zero only where explicitly unused. Sequence 1..32, no wrap. Nonzero deadlines 1..2^63-1; reject arithmetic overflow. Startup limit `t0 + 30_000_000_000 ns`; privileged deadline `startup_deadline - 25_000_000_000 ns`. Shutdown initiation creates a single `now + 4_000_000_000 ns` deadline, propagated unchanged; no reset. For .NET, do not assume `Stopwatch` shares the Linux `CLOCK_MONOTONIC` epoch. A standalone codec may accept injected monotonic timestamps; any platform clock implementation must be demonstrably Linux CLOCK_MONOTONIC compatible. No live startup/shutdown behavior in P1.

**Roles:** `01` root bootstrap, `02` supervisor, `03` guardian bootstrap, `04` guardian, `05` router bootstrap, `06` router, `07` agent bootstrap, `08` agent. Bootstrap roles are forbidden as runtime-channel senders.

**Record types:**
- `01 META_ROUTER`, `02 META_GUARDIAN`, `03 META_AGENT`
- `10 G_BOUND`, `11 R_PREPARE`, `12 R_BOUND`, `13 G_RELEASE`, `14 A_EXEC`, `15 A_EXEC_ACK`, `16 A_BIND`, `17 A_READY`, `18 START_UI`, `19 UI_READY`, `1a R_UI_READY`, `1b R_EXPOSE`, `1c R_EXPOSED`
- `20 STOP_ROUTER`, `21 STOP_GUARDIAN`, `22 STOP_UI`, `23 UI_STOPPED`, `24 R_PROTOCOL_DONE`, `25 HOST_DISPOSED`
- `30 AGENT_RESULT`, `31 GUARDIAN_OBSERVED`, `32 ROUTER_OBSERVED`, `33 CHILDREN_ABSENT`
- `7f FAIL`
- `b0` agent signal result, `b1` agent wait failure, `b2/b3` local guardian observation signal/wait failure, `b4/b5` local router observation signal/wait failure.

Types `31`–`33` are **internal supervisor proof-ledger events only**, never acceptable as incoming IPC proof. `AGENT_RESULT` must never be treated as proof without the guardian's bound positive kernel wait; codec validity does not attest truthful process status.

**Outcome byte:** ordinary metadata/startup/control messages `00`; `UI_STOPPED=00` is UI policy completion only. Normal-exit result: `00` means exit zero, `01..ff` means nonzero exit only for normal-exit forms. Signal forms: `01..40` for signals 1–64. Wait-failure forms: `01 ECHILD`, `02 other native error`, `03 invalid returned child`, `04 interop failure`. `FAIL`: `01 identity`, `02 protocol`, `03 startup deadline`, `04 shutdown deadline`, `05 unexpected termination`, `06 confinement`, `07 host failure`, `08 descendant failure`. `EINTR` is a bounded local retry, not a terminal outcome.

**Metadata streams:** S receives `META_ROUTER` then `META_GUARDIAN`, exactly two then EOF; R receives `META_ROUTER`, exactly one then EOF; G receives `META_ROUTER` then `META_AGENT`, exactly two then EOF; agent post-exec receives `META_ROUTER` then `META_AGENT`, exactly two then EOF. Sender and related PID must be validated against established per-launch role bindings; a raw PID is not sufficient authority.

**Framing/sequence:** each channel direction starts sequence at 1, continues across startup and shutdown, never resets across agent exec on retained channels. Exact 64-byte framing, expected launch ID, expected sender/recipient roles and PIDs, state/type, deadline fields, zero reserved bytes, exact next sequence; no ancillary FD passing. Metadata streams enforce exact counts. Runtime directions permit at most 16 startup and 16 shutdown records. Partial/early EOF, extra bytes, duplicate/replay/reorder, unknown fields and wrong-state messages fail. No arbitrary text or payloads.

**Startup FSM (for offline state validation only):** metadata/fork graph → S/G hardened and bound → `R_PREPARE` → router-local synthetic generation/exec → `R_BOUND` → `G_RELEASE` → `A_EXEC` → `A_EXEC_ACK` → agent exec/hardening → `A_BIND` → `A_READY` → `START_UI` → `UI_READY` → `R_UI_READY` → `R_EXPOSE` → `R_EXPOSED` → RUNNING. Gate ACK is not exec proof. Do not implement actual forks, gates, credential generation or listener exposure.

**Shutdown FSM (offline validation only):** one supervisor-initiated stop deadline, `STOP_ROUTER`/`STOP_GUARDIAN`, router `STOP_UI`, `UI_STOPPED` and lifecycle EOF, `R_PROTOCOL_DONE`, `HOST_DISPOSED`, guardian `AGENT_RESULT` after positive wait, supervisor's own bound guardian/router waits and finally descendant absence. D1/D2/T2 are runtime proof obligations, not satisfied by decoding a record. Required-child `ECHILD` is FAIL; terminal absence is absence-only after positive statuses.

**Descriptor policy (model/validation only):** S FD3 S–R, FD4 S–G, FD5 metadata; G FD3 G–S, FD4 launch gate, FD5 metadata; R FD3 credential reader, FD4 R–A, FD5 R–S, FD6 metadata; A FD3 R–A, temporary FD4 metadata validated and closed; UI has no privileged/control FDs. No actual FD manipulation in P1.

### 5. Canonical conformance vectors
Canonical `META_ROUTER`: launch ID `00112233445566778899aabbccddeeff`, sequence 1, sender PID 1, related PID 42, startup deadline 30,000,000,000 ns, shutdown deadline 0:

```text
4149514c
01010100
00112233445566778899aabbccddeeff
00000001
00000001
0000002a
00000006fc23ac00
0000000000000000
000000000000000000000000
```

64 bytes total. Additional positive/negative vectors: agent zero result (`role=04`, type `30`, outcome `00`, sender PID 43, related PID 44, shutdown deadline 34,000,000,000 = `00000007ea8ed400`); agent nonzero 7; agent signal 15 (`b0/0f`); agent `ECHILD` (`b1/01`). Test each in correct bound context. Negative: 63-byte truncation, extra trailing byte, version 02, nonzero reserved byte, all-zero launch ID, sequence zero/duplicate, wrong result PID, ordinary readiness outcome 07, signal outcome 00, expired or overflowing deadline, wrong channel/state, spoofed internal-only proof events, stale/duplicate/reordered records. Verify Python/.NET exact byte parity.

### 6. Implementation/test guardrails
- Keep modules pure, independently callable, deterministic, with no runtime topology integration or side effects. Explicitly separate structural decode from channel/state/binding validation and from actual kernel wait evidence.
- Avoid logs that disclose credentials or arbitrary sensitive process state. No credential creation or authentication behavior.
- Focused offline Python and .NET tests only, using existing tools/dependencies. No Docker, network calls, Azure, GitHub actions, package restoration from network, or broad test suites.
- If a frozen detail is internally inconsistent or ambiguous, **STOP**, document the smallest precise question, and do not silently invent protocol semantics or alter T9–T11.
- Existing disabled/private runtime paths and intentional working-tree changes must remain untouched.

### 7. Required Codex report
1. Baseline branch, HEAD, full initial/final Git status and allowlisted file hashes/diffs; explicitly state that status alone does not prove unchanged content outside the allowlist.
2. File-by-file change inventory, exact scope and reason; verify only the four allowed paths changed.
3. Contract compliance matrix covering layout, roles/types, outcomes, framing, sequences, binding, deadlines, metadata, and proof-vs-report distinctions.
4. Focused test commands, actual results/counts and Python/.NET vector parity; label unrun tests honestly and explain constraints.
5. Security/integrity limits, missing Linux runtime qualifications, unresolved issues and any required owner decision.
6. Final verdict: **`WP07-P1 OFFLINE PROTOCOL COMPLETE — OWNER REVIEW REQUIRED`** or **`WP07-P1 BLOCKED — OWNER REVIEW REQUIRED`**.

**STOP — OWNER REVIEW REQUIRED.** No further work packages or runtime integration are authorized. `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**.

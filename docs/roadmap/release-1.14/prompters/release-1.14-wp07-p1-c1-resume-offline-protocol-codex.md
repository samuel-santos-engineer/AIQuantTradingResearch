# Codex Authority — Release 1.14 WP07-P1 Resume After C1

## Owner authorization

The owner explicitly authorizes resumption of **WP07-P1 offline lifecycle protocol implementation**, subject to the existing four-file allowlist and **C1** below. This is an implementation authority for those four files and focused offline tests only. It is **not** authority to modify GitHub, integrate runtime code, run Docker, allocate identities, or deploy.

### Binding clarification C1 (owner approved)

> The guardian–agent launch gate is a bootstrap-only channel. It permits `A_EXEC` from guardian role `04` and `A_EXEC_ACK` from agent-bootstrap role `07`. Bootstrap senders remain forbidden on runtime control/lifecycle channels.

Implement the channel-specific validation: the bootstrap-only guardian–agent gate accepts exactly these permitted messages and roles in their expected directions and states. Do not generalize this exception to any runtime channel. `A_EXEC_ACK` acknowledges gate receipt only; `A_READY` is still required after agent exec/hardening. The gate FD closes before agent exec. Temporary agent metadata FD 4 survives exec, is validated once, then closes (T11).

## Protected baseline and preflight

- Branch: `draft/release-1.14-wp07-partial-public-disabled`
- HEAD: `67893f51931ee183f71050a83b81127e6e648b87`
- The working tree is intentionally dirty; preserve all pre-existing changes exactly. Capture complete `git status` before and after and compare, accounting only for the four newly authorized files. Do not clean, reset, stash, checkout, commit, push, create PRs or mutate GitHub.
- Previous P1 attempt was blocked before changes: four allowlisted files absent; tests not run; parity not verified. Verify these facts rather than assuming them.
- If baseline differs, paths are unexpectedly occupied, or a required edit escapes the allowlist: **STOP** and report.

## Exact mutation allowlist (four files only)

1. `container/admin_lifecycle_protocol.py` (new)
2. `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs` (new)
3. `python/presentation/test_admin_lifecycle_protocol.py` (new)
4. `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs` (new)

No other source, project, lockfile, configuration, documentation, generated or repository file may be modified. Do not install packages or change project references. If the .NET module cannot be compiled/tested without editing a project file, STOP and report that limitation; do not silently expand scope.

## Frozen protocol authority

Follow owner-approved T8–T11 and the existing `aiq-admin-lifecycle-v1` freeze, including:

- Exactly 64 bytes; magic `AIQL` (`41 49 51 4c`); version `01`; one-byte sender role, message type, type-specific outcome; 16-byte nonzero launch ID; unsigned big-endian sequence, sender PID, related PID; unsigned big-endian absolute startup/shutdown `CLOCK_MONOTONIC` nanoseconds; 12 reserved zero bytes.
- Role IDs: `01` root bootstrap, `02` supervisor, `03` guardian bootstrap, `04` guardian, `05` router bootstrap, `06` router, `07` agent bootstrap, `08` agent.
- Message catalog, numeric types, outcomes, wait-failure/signal variants, metadata order, role binding, per-direction sequencing and counts, strict framing, channel ownership, deadline checks and startup/shutdown state constraints must match the previously frozen specification. **C1 is the only approved clarification.**
- Metadata pipes are bootstrap-only; bootstrap senders are not valid runtime control/lifecycle senders. Types `31`–`33` are supervisor-internal proof ledger entries, never incoming IPC proof. Endpoint possession alone is not proof of a truthful kernel wait.
- Enforce startup bounds: privileged phase ends by `t0+5s`; total startup ends by `t0+30s`; no clock reset. Shutdown evidence shares one four-second absolute monotonic deadline. Do not equate .NET `Stopwatch` timestamps with Linux `CLOCK_MONOTONIC` epochs.
- No process creation, credential generation, privilege transition, socket listener or integration into active runtime in P1.

## Required offline work and tests

- Implement self-contained Python and C# record types, exact serialization/deserialization, bounded framed readers or pure framing helpers, role/channel/state checks, sequence and launch binding, deadline validation and deterministic failures. Keep runtime side effects absent.
- Encode and decode the canonical 64-byte `META_ROUTER` record: launch `00112233445566778899aabbccddeeff`, seq 1, sender PID 1, related PID 42, startup deadline 30,000,000,000 ns, shutdown deadline 0; verify exact hex and length. Cover agent exit-zero/nonzero, signal 15 and `ECHILD` variants.
- Add negative tests for 63-byte truncation, trailing byte, wrong version, nonzero reserved, zero launch ID, invalid/duplicate sequences, wrong bound PID, wrong outcome for message type, invalid signal code, expired/overflowing deadline, wrong channel/role, premature message, and replay.
- **C1 regression tests:** guardian `04` `A_EXEC` and agent-bootstrap `07` `A_EXEC_ACK` accepted only on the dedicated bootstrap launch gate in correct order; role `07` rejected on all runtime control/lifecycle channels; `A_EXEC_ACK` not treated as `A_READY`; launch-gate FD lifetime modeled as bootstrap-only.
- Run only focused, offline tests available without modifying files outside the allowlist. If tests cannot run without side effects outside scope, do not run them; report exact blocker. Do not claim cross-language parity without observed results from both implementations.
- Record commands, exit codes, test counts, relevant byte vectors, changed-file diff summary, and unresolved limitations. Do not claim target Linux/kernel or Docker qualification.

## Hard prohibitions and stop gates

No Docker, Azure, network access, deployment, GitHub mutation, UID/GID allocation, production credentials, authenticated-client testing, router/bootstrap/agent integration, dependency installation, unrelated tests or file edits outside the allowlist. Stop on any new protocol ambiguity or apparent contradiction rather than guessing. Preserve A1/A2/A3, D1/strict D2/D3, T1–T11, disabled Option A, four-second shutdown deadline, five-second Docker grace, five-second privileged startup and 30-second total startup bounds.

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**.

## Required final report

Report initial/final branch, HEAD and full Git status; exact files changed; test commands/results; Python/.NET canonical-vector parity status; C1 channel validation evidence; remaining blockers and scope deviations. Conclude with exactly one:

- **WP07-P1 OFFLINE PROTOCOL COMPLETE — OWNER REVIEW REQUIRED**
- **WP07-P1 BLOCKED — OWNER REVIEW REQUIRED**

**STOP — OWNER REVIEW REQUIRED.** Do not start the next work package.

# Codex Authority — Release 1.14 WP07-P1C
## Offline Lifecycle Protocol Security Corrections — Four-File Mutation Envelope

**Owner authority:** The owner's request to create this next Codex authority follows the P2 finding `P1 REVIEW BLOCKED — CORRECTION REQUIRED`. This document authorizes **only WP07-P1C corrections and focused offline validation**, not integration or deployment. Stop for owner review after reporting results.

## 1. Protected baseline and preflight

- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Working tree is intentionally dirty; **do not** reset, clean, stash, checkout, switch branches, commit, push or alter GitHub state.
- Capture complete initial `git status`, verify branch/HEAD, and record SHA-256 hashes of the four P1 files before editing. Expected prior P1 hashes (verify; if different, STOP and report):
  - `container/admin_lifecycle_protocol.py`: `31BEE9CA95F1E84B0B0FBA8778CD70F9B078A35587628C1DACC4028ACC2E5062`
  - `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs`: `E26975F621870C21FFA8C8E0915952DC141CEDA5E0CA2E6A33CD884F78DC3863`
  - `python/presentation/test_admin_lifecycle_protocol.py`: `3A04C21C87BE31DD70AEA091368775109083E75A54CE32F6D9912B60D20152E8`
  - `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs`: `03CCF2D309B9339AF40EC8A45BE25C62BF09BBEEC3B0FF1BF1E73792CDE31702`
- If files are missing, hashes differ, unrelated changes appear, or the protected baseline does not match: **STOP**; do not overwrite or reconcile automatically.

## 2. Exclusive file allowlist

Only these existing four files may be modified:

1. `container/admin_lifecycle_protocol.py`
2. `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs`
3. `python/presentation/test_admin_lifecycle_protocol.py`
4. `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs`

No project files, dependency files, bootstrap, guardian, supervisor, agent runtime, router, entrypoint, Dockerfile, scripts, documentation or qualification artifacts may be changed. Keep test/build outputs outside the repository, with no incidental tracked/untracked files. If changes beyond this allowlist are required, STOP.

## 3. Binding design and security constraints

Preserve A1/A2/A3, D1/strict D2/D3, T1–T11, and C1. Preserve `aiq-admin-lifecycle-v1`: exact 64-byte big-endian record layout, magic/version, roles, message catalog, byte-7 type-specific outcomes and failure variants, sequence/deadline rules, metadata ordering, canonical vectors and bootstrap-only guardian–agent launch gate (`A_EXEC` guardian role 04, `A_EXEC_ACK` agent-bootstrap role 07). Bootstrap senders are prohibited on runtime control/lifecycle channels. Preserve T11 temporary agent metadata FD 4 contract; do not implement FD operations in this increment.

Preserve the 5-second privileged and 30-second total startup bounds; shared `CLOCK_MONOTONIC` 4-second shutdown deadline and 5-second Docker stop grace. Disabled Option A unchanged. `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**.

## 4. Required corrections — implement consistently in Python and C#

### P1C-01 — Eliminate generic result bypass (HIGH)
- Generic `Shutdown.event(AGENT_RESULT)` / `LifecycleShutdown.Event(AgentResult)` must never advance the model or satisfy agent evidence.
- Require a single explicit validated report path, with correctly bound identity, launch ID, startup deadline, allowed result type and **normal exit 0** before any corresponding model advancement.
- A parsed report is not itself a kernel wait. Maintain an explicit separation between untrusted report validation and independently supplied trusted kernel-wait observation; no offline helper may assert D1 or strict D2 merely from wire data.

### P1C-02 — Validate PID bindings (HIGH)
- At construction/binding, validate agent and guardian PIDs as strict positive integers in the frozen PID range `1..2,147,483,647`, mutually distinct. Reject zero, negatives, out-of-range, missing, booleans (Python), and nonintegral types.
- Validate every observation consistently in both languages; wrong child, zero, mismatched or malformed observation cannot advance state.

### P1C-03 — Sticky FAIL on rejected mutations (HIGH)
- Every rejected **mutating** operation, including malformed report, identity mismatch, deadline overflow, invalid state/sequence, or illegal event, transitions its applicable model/direction to sticky FAIL **before** returning/throwing the validation error.
- Once failed, later otherwise-valid inputs cannot recover the state or produce PASS.
- Construction-time validation may reject construction without creating a usable object; distinguish this from a failed mutation of an existing object.

### P1C-04 — Terminal EOF only, once (MEDIUM)
- `Direction.eof(permitted=True)` / `Eof(true)` is not a generic bypass. Accept EOF only on the specifically designated channel and only after that channel's required terminal protocol transition and evidence prerequisites.
- Reject premature, wrong-channel and duplicate EOF; these are sticky failures. `permitted` alone never proves readiness, exit status or completion.
- Preserve expected EOF semantics for metadata pipes and shutdown lifecycle; if the frozen contract does not resolve a necessary terminal prerequisite, STOP for owner clarification rather than inventing it.

### P1C-05 — Non-bypassable report composition (MEDIUM)
- Validate launch ID, expected agent/guardian PIDs, startup deadline, sender/channel binding, type/outcome and relevant state together before result advancement.
- Remove public API paths that permit separately validating fragments and then advancing without their combined binding.

### P1C-06 — Python/.NET input parity (MEDIUM)
- Enforce strict Python integer checks for timestamp, deadline and observation inputs (including rejecting `bool`, floats, strings and out-of-range values where C# numeric types are stricter).
- Add paired boundary/negative tests that establish equivalent behavior across languages without changing frozen wire semantics.

## 5. Focused offline validation

Update tests in the two allowlisted test files to demonstrate:

- Generic agent-result event cannot advance; dedicated valid path only.
- Constructor rejects invalid, duplicate or nonpositive PID bindings.
- Wrong/mismatched/zero report or observation; malformed type/outcome, launch or deadline; invalid numeric types; deadline overflow all reject and leave sticky FAIL when mutating an existing instance.
- Premature, wrong-channel or repeated EOF rejects; permitted terminal EOF succeeds exactly once.
- Existing C1 gate restrictions, canonical exact-byte vectors, metadata ordering, replay, framing and Python/.NET parity remain intact.
- Explicit assertions that report data does not constitute positive kernel-wait proof.

Run **only** focused offline Python and .NET tests and necessary build checks, with .NET outputs/intermediates redirected outside the repository; avoid restore, network, Docker and full-suite execution. If focused tests require new dependencies, project changes or network access, STOP. Report commands, counts, exit codes and warnings/errors. Do not claim runtime qualification.

## 6. Evidence and STOP conditions

Before and after changes, capture branch, HEAD, full Git status and hashes of the four allowlisted files. Produce an exact diff inventory and confirm no unrelated file content was modified; where not independently hash-verified, state the preservation limitation. Do not commit or push.

STOP immediately if: protected baseline or P1 hashes differ; wire specification ambiguity cannot be resolved without a new owner decision; a fix needs additional files, dependencies, privilege/identity changes, kernel waits, runtime integration, Docker, networking, Azure or GitHub actions; any requirement weakens proof boundaries, changes disabled mode or expands scope.

## 7. Required final report

Provide: (1) baseline/preservation ledger; (2) six finding-by-finding corrections with precise file/line references; (3) test matrix including negative cases and Python/.NET parity; (4) exact commands/results; (5) final hashes and scoped diff; (6) remaining limitations and next smallest authority. End with exactly one verdict:

- **`WP07-P1C OFFLINE CORRECTIONS COMPLETE — OWNER REVIEW REQUIRED`**, or
- **`WP07-P1C BLOCKED — OWNER REVIEW REQUIRED`**.

No integration, Linux kernel qualification, credential generation, identity allocation, Docker, networking, Azure, GitHub changes or public activation is authorized.

**STOP — OWNER REVIEW REQUIRED.**

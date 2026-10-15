# Codex Authority — Release 1.14 WP07-P2
## P1 Offline Protocol Security and Integration Readiness Review (READ-ONLY)

**Owner authorization:** Read-only review and planning only. **Do not implement, edit, test, build, run Docker, commit, push, open PRs, or access Azure/network services.** This authority does not approve P2 implementation.

## 1. Protected baseline and stop gates

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Working tree is intentionally dirty. Preserve it exactly; do not checkout, reset, clean, stash, stage, or mutate files.
- Record initial and final full Git status; report differences. Hash the four P1 files before and after review if accessible. If baseline or expected files do not match, **STOP** and report.
- `PUBLIC_RUNTIME_VERIFIED=False`; administrative API availability, production ingress TLS, Azure passive qualification and public Worker activation are **NO-GO**. Disabled Option A remains unchanged.
- Preserve A1/A2/A3, D1/strict D2/D3, T1–T11 and C1. Preserve the shared four-second `CLOCK_MONOTONIC` shutdown deadline, five-second Docker stop grace, five-second privileged startup bound and 30-second total startup bound.

## 2. Exact P1 review targets (read only)

1. `container/admin_lifecycle_protocol.py` — reported SHA-256 `31BEE9CA95F1E84B0B0FBA8778CD70F9B078A35587628C1DACC4028ACC2E5062`.
2. `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs` — reported SHA-256 `E26975F621870C21FFA8C8E0915952DC141CEDA5E0CA2E6A33CD884F78DC3863`.
3. `python/presentation/test_admin_lifecycle_protocol.py` — reported SHA-256 `3A04C21C87BE31DD70AEA091368775109083E75A54CE32F6D9912B60D20152E8`.
4. `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs` — reported SHA-256 `03CCF2D309B9339AF40EC8A45BE25C62BF09BBEEC3B0FF1BF1E73792CDE31702`.

The prior Codex report states 15/15 Python tests, 31/31 .NET tests, zero build warnings/errors, and identical canonical fixtures. Treat these as **reported**, not independently reproduced; do not run them under this authority.

## 3. Required read-only review

### A. Frozen wire-contract conformance

Review exact 64-byte big-endian `aiq-admin-lifecycle-v1` layout, magic `AIQL`, version, role/type/outcome fields, launch ID, per-direction sequences, sender/related PIDs, monotonic startup/shutdown deadlines and zero reserved bytes. Compare Python and C# implementations and tests to the owner-frozen specification, including T10 signal/wait-failure variants and T11 temporary agent metadata FD 4. Document deviations with file/line evidence.

### B. C1 bootstrap launch gate

Confirm `A_EXEC` guardian role `04` and `A_EXEC_ACK` agent-bootstrap role `07` are permitted **only** on the dedicated bootstrap-only launch gate. Confirm runtime channels reject bootstrap senders; gate acknowledgment does not prove exec, readiness, agent exit or kernel wait. Verify post-exec `A_READY` is separately required.

### C. Failure, spoofing and state handling

Inspect parsing/framing for short/extra records, EOF, wrong state/channel/role/PID/launch ID, sequence replay or wrap, reserved fields, invalid outcomes, expired/overflow deadlines and forged internal-only proof messages. Distinguish structural validation from channel provenance, kernel evidence and integrity. Identify any untested or inconsistent cases, including cross-language edge cases.

### D. Time and proof semantics

Confirm neither implementation assumes `.NET Stopwatch` and Linux `CLOCK_MONOTONIC` have the same epoch. Check absolute startup limits and nonresetting four-second shutdown bound. Verify that protocol-reported statuses never substitute for guardian's actual bound positive agent wait (D1), supervisor's actual bound positive guardian wait (strict D2), router positive exit/disposal, or PID 1 terminal descendant absence (T2). Required-child `ECHILD` is failure; terminal `ECHILD` is absence only.

### E. Integration readiness (no changes)

Read relevant existing bootstrap, router, agent, entrypoint and Docker sources solely to identify exact integration conflicts and prerequisites: current router PID 1 check, existing ASCII framing, `waitpid(-1)` ownership, process creation graph, role-specific FD maps, agent session isolation, router-local synthetic credential generation after separation, account collision checks and immutable trusted dependencies. Do not infer runtime qualification from source review.

## 4. Deliverables

Produce a structured report containing:

1. Baseline, hashes and preservation evidence, explicitly distinguishing status-level from content-level checks.
2. Evidence ledger: **CONFIRMED / SOURCE-SUPPORTED / INFERRED / UNPROVEN / UNAVAILABLE / OWNER DECISION REQUIRED**.
3. File-and-line conformance matrix for Python/C# and tests, with severity and exact corrections if needed.
4. Cross-language canonical vector and negative-case coverage assessment; do not claim tests executed.
5. Security analysis separating wire validity, endpoint provenance, process integrity and real kernel wait proof.
6. Exact **proposed** file-by-file next increment with dependency order, bounded acceptance criteria and STOP gates. This is not an implementation allowlist.
7. Missing Linux/image/native provenance and empirical qualification prerequisites.
8. One verdict: **`P1 REVIEW ACCEPTED — P2 IMPLEMENTATION PLANNING READY`** or **`P1 REVIEW BLOCKED — CORRECTION REQUIRED`**.
9. End with **`STOP — OWNER REVIEW REQUIRED`**.

## 5. Explicit prohibitions

No source or test modifications; no generated files in the repository; no tests, builds, package restore, Docker or Linux qualification; no UID/GID allocation; no runtime integration; no credential mechanism change; no network/GitHub/Azure actions. If a finding requires changing the frozen contract, expanding scope or resolving an ambiguity, report it as an owner decision and **STOP**. Do not silently amend T8–T11 or C1.

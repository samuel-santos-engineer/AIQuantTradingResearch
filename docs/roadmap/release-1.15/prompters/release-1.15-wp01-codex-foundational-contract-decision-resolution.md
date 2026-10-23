# Release 1.15 WP01 — Foundational Contract Decision Resolution

## Mission
Work in `samuel-santos-engineer/AIQuantTradingResearch` on the **existing PR #380**. Prepare a documentation-only amendment resolving proposed foundational D1, D3, D4 and D5 contracts. PR #379 was merged as `3880f5a`; PR #380 was last reviewed at `f3cf278`. Verify live GitHub state rather than assuming these are still current. Issue #367 remains open, WP02 unstarted and `PUBLIC_RUNTIME_VERIFIED=False`.

**Authorization:** Draft technically specific, owner-reviewable proposals; do not claim they are approved or FROZEN. No code, tests, CI, dependencies, deployment, provider calls, Azure changes, PR merge, issue closure or WP02 work.

## Preflight
1. Verify clean worktree, branch and remote, and PR #380's current head. Inspect `AGENTS.md`, Docker-first and historical-chart governance, LF policy, Release 1.15 definition, execution plan, manifest, acceptance and issue register; read all three PR #380 closeout documents and the S1/S2 evidence merged in PR #379.
2. Use the existing PR #380 branch if safe; do not open a competing PR or overwrite concurrent edits. If `main` is behind, fetch and fast-forward only if clean and safe. If an untracked prompt blocks preflight, **stop and ask before removing or relocating it**. No resets, rebases, force pushes or overwrites.
3. Limit changes to `docs/roadmap/release-1.15/RELEASE_1.15_WP01_DECISION_REGISTER.md`, `RELEASE_1.15_WP01_CONTRACT_CLOSEOUT.md`, `RELEASE_1.15_WP01_DOWNSTREAM_QUALIFICATION_MATRIX.md`, and `RELEASE_1.15_FILE_MANIFEST.md` (index only if necessary). Optionally add `RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md` in the same folder for detailed normative proposals. No other path changes without owner authorization.

## Decision proposals — explicit, testable, not silently approved
For each decision distinguish **inherited binding invariants**, **proposed exact contract**, **alternatives and tradeoffs**, **owner choice required**, **downstream owner**, and **independent conformance evidence**. Preserve OPEN/PROVISIONAL status until the owner explicitly accepts a decision.

### D1 — Runtime supervisor trust and lifecycle (WP02)
Propose a concrete Linux/amd64 single-image bootstrap/supervisor privilege model. Specify whether PID1 retains root, capability reduction, UID/GID/group/secret/env/FD/socket boundaries, separate non-root research/UI roles, loopback API, cross-UID signaling and wait/reap authority, process-group descendant termination, no-overlap restart, startup/shutdown deadlines, crash-loop backoff and peer isolation. Distinguish what S2 proved from what it did not. **Do not implicitly approve the fixture's root PID1 with default capabilities.** Provide adversarial Linux test criteria and Azure-specific residual risks.

### D3 — Deterministic research calculations (WP04)
Propose exact price basis, initialization, warmup/null rules, precision/rounding and update order for SMA20, EMA20, Bollinger20×2 (population vs sample variance), Wilder RSI14 (zero denominators), MACD12/26/9, ATR14 (true-range/gaps), trend score, normalized width/slope/ATR, percentile/equality cases, regime thresholds and transition events. Preserve 100 warmup + 300 calibration consecutive completed candles and first eligible classification on candle 401; address gaps, corrections, checkpoints, pruning, restart and no-lookahead. Require independent C# reference vectors including flat, extreme, gap and 400/401 boundary cases. Do not invent a production test pass.

### D4 — Canonical C#/Python wire protocol (WP05)
Propose a complete versioned JSON contract: exact field names/types; full, unchanged, zero-row and error envelopes; BTC/USD and ETH/USD identity; UTC candle-opening timestamps/precision; generation, version, display date, request epoch and D5 transform epoch; status dimensions and observation times. Specify numeric encoding, finite/overflow handling, null/missing/unknown fields, duplicate keys, ordering, collection/string/event/interval and request/response byte limits, bounded streaming/parser enforcement, timeouts/retries, stale/unchanged semantics and whole-response atomic validation before cache replacement. Supply illustrative positive/negative vectors independently implementable in C# and Python. Proposed bounds must be labeled as proposals, not inherited facts.

### D5 — Daily HMAC transformation and rotation (WP05)
Propose exact HMAC-SHA256 domain/message/date/key-epoch format, digest-to-integer mapping and factor endpoints approximately 0.90–1.10; specify decimal rounding, common daily factor across both instruments, OHLC and price-valued indicators, RSI/dimensionless outputs unchanged and explicit volume treatment. Define key rotation within a UTC day, transform-epoch and cache identity, rollover, `unchanged` collision prevention, and no raw prices/keys/factors in public DTOs/logs. Provide independent test-vector specification with deterministic **test-only** keys. D4 and D5 identities must agree.

### D2, D6, D7 — Reconciliation only
Retain the SQLite single-writer/atomic publication/continuity and 1,000 completed-minute-candle constraints; components-v2 with locally loaded Plotly, pending-intent/viewport preservation, session isolation; and Docker-first Linux/amd64 Azure F1 best-effort target. Defer actual storage durability to WP03, UI integration to WP06 and Azure resource qualification to WP07. Do not reopen chart technology, invent capacity measurements or introduce licensing approval gates.

## Update PR #380
- Update the D1–D7 register and closeout with exact **proposed** resolutions and an owner decision sheet. Keep WP01 BLOCKED pending explicit acceptance; no automatic FROZEN designation.
- Keep all 22 downstream obligations and their single primary WP02–WP07 owners; distinguish foundational owner decisions from implementation tests. WP08 remains final handoff, not an implementation owner.
- Link optional proposal specification from register/closeout/manifest as appropriate. Preserve historical failures and bounded evidence; do not rewrite the S1/S2 reports.
- Validate every reference/link, D1–D7 statuses, acceptance traceability and changed-file allowlist. UTF-8 LF only, no BOM/CRLF; run independent byte scan and `git diff --check`.
- Commit and push only authorized docs to the **existing** PR #380 branch without force. Leave PR open/unmerged and #367 open. Return URL, SHA, changed files, owner decision checklist, validation, and explicit tests-not-run disclosure.

## Stop conditions
Stop for dirty/diverged branch, conflicting authority, ambiguous branch ownership, unapproved path expansion, or a proposal that weakens isolation or requires code to evaluate. Ask for owner direction rather than invent approval. No deployment, WP02 start or `PUBLIC_RUNTIME_VERIFIED` change.

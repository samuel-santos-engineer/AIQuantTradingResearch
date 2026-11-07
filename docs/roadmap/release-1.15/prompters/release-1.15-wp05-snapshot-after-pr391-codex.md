# Release 1.15 — Codex: implement WP05 after merged PR #391

## Authority and baseline

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Begin from fresh `origin/main` containing merged PR #391 squash commit `20a8647c901d3bffc1516b80603e7dab30d01c68`, as well as merged #389 and #390. Confirm the actual GitHub state and HEAD before edits. PRs #386 and #387 were previously open; recheck them and do not silently import their unmerged code or docs.

**Goal:** Deliver a functional, offline-tested, authenticated C# `POST /v1/research/snapshot` endpoint and a strict Python HTTPS client that consume real WP03 committed BTC/USD and ETH/USD one-minute candles and WP04 private calculated state, applying durable D5 public transformation. Open **one implementation PR, OPEN/UNMERGED** for owner review. Do not create another planning-only PR.

## Keep the milestone simple

Use the existing .NET 10, Python, SQLite, research store, and dual-container direction. No new database, microservice, message bus, orchestration layer, dependency upgrades, automatic key rotation, DNS cancellation redesign, additional authentication scheme, paid service, live provider activation, or Azure deployment. Do not touch or inspect production. Staging deployment and Linux qualification belong to later separately authorized work. `PUBLIC_RUNTIME_VERIFIED=False`.

## Scope and owner checkout protection

1. Read `docs/roadmap/release-1.15/RELEASE_1.15_WP05_INTEGRATION_PREFLIGHT.md` if present, the WP05 plan in PR #387, `docs/roadmap/release-1.15/RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md`, the file manifest, and the merged WP03/WP04/D5 source. The previously proposed **14 WP05 paths** are the intended source/test scope; enumerate and verify their **literal names** before writing. If any do not exist in the plan, do not guess them.
2. Use an isolated branch/worktree from current main. Before changes, inventory the owner's original checkout HEAD, full status, and sizes/SHA-256 of **every dirty and untracked file**, including every Codex prompt. Do not reset, clean, stash, overwrite, rename, stage, or alter originals. Preserve all bytes and recheck after publication.
3. Confine implementation to the approved 14 WP05 source/test paths and append-only changes to the two previously approved evidence documents if still applicable. If actual merged interfaces demand any extra path, stop and ask for exact-path authorization; do not work around the boundary. Archive new prompts only if the destination path is separately approved, never overwrite existing archives.
4. Do not close WP issues or claim WP05 acceptance. Never merge your own PR.

## Implement the actual vertical slice

- **C# endpoint:** Authenticate server-to-server with one active environment-specific `X-API-Key`; constant-time comparison; no browser access to keys. Implement the D4 `POST /v1/research/snapshot` schema `1.0` against the **real** `IResearchTransformStateReader.UseTransformSnapshot` and `ResearchCommittedState` types. The callback must fully stage and serialize a whitelisted public response while the store publication lock is held. Do not return raw private objects or defer public transformation outside that boundary.
- **D5 transform:** Use the committed durable `ResearchTransformConfiguration` from #391. UTC display date; HMAC-SHA256 message `algorithm + LF + displayDate + LF + privateKeyEpoch + LF`; interpret digest as unsigned big-endian `D`; `S=10^12`; `F=900000000000 + floor(D * 200000000001 / 2^256)`; transform a private scaled price integer `x` to `R(x*F/S)` with ties-to-even. Share one daily factor across BTC/USD and ETH/USD. Transform OHLC and price-valued indicators **after** WP04 calculations. Keep base-asset volume, RSI and dimensionless values unscaled. Never expose private key, private key epoch, raw prices, checkpoint state or full SQLite rows.
- **D4 contract:** Respect exact six public collections (`candles`, `analytics`, `classifications`, `events`, `researchUnavailableIntervals`, `marketDataGapIntervals`), schema/version/algorithm identifiers, intervalSeconds=60, bounded payloads, canonical fixed-12 decimal strings and decimal-string counters. Only return `unchanged` when the complete request identity, generation, instrument versions, display date and public transform epoch match; omit collections entirely for unchanged, rather than returning null/empty placeholders. Fail closed on stale, inconsistent, unavailable, unconfigured, malformed or unauthenticated state. Avoid claiming research-ready merely because raw candles exist.
- **Python client:** Synchronous server-side HTTPS request, `X-API-Key`, bounded response bytes/timeouts, strict JSON duplicate-key/schema/types/counts/depth/decimal-string validation, explicit error handling and immutable public DTOs. Accept the known standard DNS-resolution timeout limitation without spawning subprocesses or introducing new cancellation machinery. No raw private credentials in UI/browser/logs.
- **Runtime composition:** Wire the smallest viable C# service and Python client integration **only within the already approved paths**. If a startup/DI/Docker file is outside those paths, do not edit it: build/test a real in-process endpoint with offline HTTP test server and explicitly report the remaining composition task. Never substitute fake successful snapshots for unavailable state.

## Required offline proof

- Actual committed WP03 BTC/ETH candles and WP04 calculated rows flow through the real C# endpoint and are parsed by Python. Test fully authenticated full/unchanged, wrong/missing key, invalid body, bounded payload, stale request, epoch/day change, recovery/unconfigured fail-closed, no leakage, exact D5 known vectors including negative/signed indicator cases, shared BTC/ETH daily factor and ties-to-even.
- C# and Python fixtures must agree byte-for-byte on public contract semantics. Avoid using production transformation output as the sole test oracle: independently derive at least one known-vector HMAC/factor/rounding case.
- Run available focused tests and relevant existing regressions; report commands, counts, failures, skips and environment honestly. Do not report CI/Linux/Azure proof unless actually executed.
- Verify exact changed-file scope, UTF-8 without BOM, LF, final newline, `git diff --check`, original owner checkout HEAD/status/hash preservation and prompt provenance.

## Deliverable

Commit and push the isolated implementation branch; open one PR for owner review, **OPEN/UNMERGED**. PR body: exact files, merged #391 baseline, implementation and contract choices, actual executed test results, known limitations and remaining runtime composition/staging work. Keep `PUBLIC_RUNTIME_VERIFIED=False`, WP05 issue open, and all production resources untouched.

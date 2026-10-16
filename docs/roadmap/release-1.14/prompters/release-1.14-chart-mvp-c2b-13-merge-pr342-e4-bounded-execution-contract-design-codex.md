# Release 1.14 Chart MVP — C2B-13 Codex authority
## Guarded merge of accepted PR #342; E4 bounded offline-proof execution-contract design (documentation only)

**Owner authority:** The owner explicitly accepted PR #342 and requested the next Codex file. This authorizes a guarded merge of the **exact reviewed PR #342**, then a new **documentation-only** C2B-13 proposal. It does **not** authorize test/source implementation, builds, restores, test/oracle/generator execution, snapshots, materialization/export/promotion, Azure/browser experiments, deployment or public activation.

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`.

**Observed reference (reverify before acting):** PR #342 open/unmerged/mergeable; base `fc3bd493009dc95ba0686b48733b4e3deecba4c2`; accepted head `0a5885f085db49ad7fe1a2db2633f98c839d1d6e`; exactly four new Markdown paths under `docs/roadmap/release-1.14/prompters/`, with C2B-12 prefix. No unresolved review threads observed. These observations are not substitutes for fresh checks.

## 1. Immutable boundaries

1. `PUBLIC_RUNTIME_VERIFIED=False`. Public `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` remain safely unavailable. Azure deployment and public activation **NO-GO**; advanced WP07 **ON HOLD**.
2. Preserve protected dirty WP07 worktree/archive and all private material. Never inspect, switch, reset, clean, stash or otherwise disturb them. Use a separately verified clean checkout.
3. Preserve `aiq-synthetic-ohlcv-v1`, seed 114, fixed `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, 1h/4h/1d × 1/7/30/90 days, 250 calculation-only warmup, all frozen canonical fingerprints and exact `aiq-ta-v1` C# indicator semantics. Public identity is **BTC-like Market Simulation — Synthetic Historical OHLCV**. No observed/provider/private data or fallback.
4. E1–E8 are **conditionally approved design directions**, not qualified mechanisms. T and all unmeasured resource/retention/durability/response thresholds remain **TBD**. No prior planning merge grants proof or implementation authority.
5. No source, tests, project files, packages, CI, Docker, schemas, generated data, snapshots, fixtures, credentials, GitHub protections, Project metadata or Azure changes. No runtime commands, benchmarks, native/job-object experiments, test runs or new service. Read-only repository inspection and ordinary Git/GitHub/document validation only.

## 2. Guarded merge of PR #342

1. Fetch and verify PR #342 current state, exact accepted head/base, four-file inventory and complete patch. Confirm no changed contents, added files, reviews/threads/check failures, merge conflict or protection blocker. Verify merge method is permitted and use exact-head SHA guard. If head or material content changed, **STOP for owner re-review**.
2. Merge #342 through normal protections only; no override, force push, branch deletion or unrelated cleanup. Record merge SHA and merge state.
3. Fresh fetch; verify clean `main == origin/main` with 0/0 ahead/behind, including ancestry of PR #341 (`fc3bd493009dc95ba0686b48733b4e3deecba4c2`). If inconsistent, STOP. Preserve any pre-existing untracked documents byte-for-byte; never silently stage them.
4. Create one C2B-13 planning branch from that verified merge baseline. Leave its PR open/unmerged for owner review.

## 3. C2B-13 design questions to resolve (proposals only)

### A. Process-tree containment on Windows

- Inventory actual .NET SDK, testhost, VSTest, CPython and possible helper processes, including descendants and stdout/stderr reader threads. Distinguish observed repository code from unverified host/kernel behavior.
- Compare candidate containment strategies (e.g. Windows Job Objects with kill-on-job-close and explicit child assignment; a qualified pre-existing bounded runner; alternative host mechanism). Discuss launch-to-assignment race, nested-job restrictions, breakaway, cancellation, descendants, resource accounting and safe failure if unavailable. **Do not assume a Job Object or hard CPU/RSS cap works on the user's machine.** No native experiment or harness implementation now.
- Specify how a future authorized runner will verify termination of every owned descendant, enforce bounded grace and final wait, and deny repeated runs after uncertain cleanup. Avoid claiming that PowerShell polling, `Process.Kill` or `-I -B` proves isolation.

### B. Finite execution ceilings and measurement discipline

- Propose a **finite candidate ceiling table** for process-tree count, per-phase and total wall time, aggregate CPU, peak RSS/commit, stdout/stderr frame and aggregate bytes, buffers, temporary/build/disk/log bytes, repetitions, negative-case count, concurrency, kill grace, cleanup deadline and evidence retention. Label each number **candidate / not measured / not owner-approved**, with its rationale and enforcement/overshoot caveat. If no credible value or enforcement exists, mark **TBD — blocking**, not a fabricated number.
- Separate proof-run *safety ceilings* from Release T production/REST/F1 acceptance budgets. Static row counts and previous 20-second test timeouts do not establish safe resource limits. Specify independent preflight, per-phase abort and fail-closed predicates.

### C. Literal source, executable, output and network contract

- Keep the proposed first proof limited to the two candidate test files: `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs` and `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`. **Do not create/edit either now.** If a runner/harness is needed, propose its exact separate path and require a new owner approval.
- Inventory toolchain resolution, exact executable identities, pinned SDK/interpreter, `--no-restore` limitations, build/testhost incidental `obj/bin` paths, package cache, environment variables, temporary directories, Python `-I -B`, subprocess argument transport and no-network behavior. Do not presume `--no-restore` prevents builds, disk writes or network in all circumstances.
- Propose exact allowed owned output locations and file names; include future evidence root candidate `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b12-e4-proof-candidate` only as a candidate, not a verified or authorized path. Resolve absent/empty-root checks, protected paths, file count/byte limits and non-destructive cleanup. No persistent candle/indicator rows, raw vectors, snapshots or private content.

### D. Independent oracle and E4 comparison contract

- Define an independently reviewed, specification-derived test-only CPython integer/rational oracle, reviewed **before** production comparison; name reviewer role, provenance evidence and independence criteria, but do not fabricate an assigned reviewer or accepted derived hash.
- Preserve all three complete datasets (8160/2040/340 rows), twelve calculation/visible windows, frozen dataset/window/visible hashes, full canonical bytes and full B1 indicators/nulls/identity. Retain exact SMA20, SMA-seeded EMA20, Wilder RSI14, MACD12/26/9, population Bollinger20×2, scale 10^12, ties-to-even and square-root midpoint. No floating tolerance, copied-output goldens, new Python production port or hidden warmup leak.
- Propose bounded frame protocol and independent review of serialization; reject malformed/oversize frames before allocation. List negative, cancellation, arithmetic-edge and provenance cases with finite enumerated scope. No oracle execution or comparison now.

### E. Explicit approval gates

- Produce a decision table separating (i) accepted C2B-12 planning, (ii) C2B-13 proposed containment/limits/paths/oracle review, (iii) future owner acceptance of those exact choices, (iv) separate narrowly scoped *test-file creation* authority if needed, (v) separate executable E4 proof authority with bounded commands, (vi) independent evidence acceptance, and (vii) later E1/E2/E3/E5–E8, export, integration, Azure/browser, deployment and activation authorities.
- State every blocking unknown and a STOP response. **Do not interpret this authority as permission to build a runner, create tests or measure ceilings by execution.**

## 4. Exact permitted C2B-13 files

Create only these **new Markdown** files under `docs/roadmap/release-1.14/prompters/`:

1. `release-1.14-chart-mvp-c2b-13-e4-bounded-execution-contract-design.md` — containment options, transport, exact paths, ownership, toolchain and abort protocol.
2. `release-1.14-chart-mvp-c2b-13-e4-resource-and-owner-decision-worksheet.md` — candidate finite ceilings, enforcement gaps, explicit owner choices and STOP conditions.
3. `release-1.14-chart-mvp-c2b-13-merge-and-design-evidence.md` — guarded merge, source-inspection provenance, preservation, unresolved risks and checks.
4. `release-1.14-chart-mvp-c2b-13-merge-pr342-e4-bounded-execution-contract-design-codex.md` — preserve this authority verbatim if practical; disclose any nonsemantic normalization.

No modifications to C2B-12, C2B-11, canonical governance, source/test code or any other tracked path. If more files are needed, STOP and request exact owner authority. Keep one PR open and unmerged.

## 5. Validation and final report

- Fresh PR #342 merge evidence: exact accepted head, four-file inventory, protections/checks/reviews/threads, merge SHA and clean main baseline.
- New PR base/head, four-file inventory, links/headings, `git diff --check`, no unauthorized changes, exact frozen identifiers and source preservation. Note any pre-existing untracked files preserved and excluded.
- Distinguish static observations, candidate mechanisms and unproven platform behavior. Do not claim measured CPU/RSS/kill behavior, independently qualified oracle, numerical parity or Azure F1 readiness.
- No tests/builds/restores/generator/oracle/snapshot output, no native/target/browser experiments. Inherited tests are **not fresh**.
- End with a precise owner decision request on process containment, proposed finite ceilings, exact output/toolchain paths, independent oracle reviewer and future scoped authority. `PUBLIC_RUNTIME_VERIFIED=False`; public activation/deployment NO-GO; advanced WP07 ON HOLD.

**Exit condition:** PR #342 safely merged and a reviewable C2B-13 **documentation-only** execution-contract proposal open/unmerged. No executable E4 proof, implementation or public runtime authority granted.

# C2B-18 — Independent E4 Oracle/Specification Review (Codex Authority)

Date: 2026-10-16
Project: `samuel-santos-engineer/AIQuantTradingResearch`
Purpose: close the **single outstanding independent numerical/specification review** for the E4 local proof, without expanding the showcase lab into production infrastructure.

## Owner context and baseline

- PR #344 merged at `3a4e6ef0e8a0ddf77776801cf87907927d8abc31`.
- Owner-reported C2B-17 local proof: .NET SDK 10.0.103, CPython 3.13.15, Release build exit 0 (0 errors, 5 warnings, 18.24 s), focused E4 tests 21/21 passed (7.76 s), two complete oracle passes, no numerical mismatch.
- Those are **reported local execution results**, not a substitute for independent oracle review. No extra run is required for this review.
- `PUBLIC_RUNTIME_VERIFIED=False`; public activation and deployment **NO-GO**; advanced WP07 **ON HOLD**.
- This is a **showcase lab with no production intention**. Optimize for finishing the chart demo, not adding layers of infrastructure.

## Exact authority

1. Inspect repository state and confirm the reviewed files and frozen specification at the merged baseline; if baseline differs, record the difference and STOP rather than silently changing scope.
2. Perform a **read-only, independently reasoned** review of:
   - `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`
   - `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
   - the relevant frozen Release 1.14 generator/canonical serialization/indicator contracts (including `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md` if present; resolve actual path read-only).
3. Independently check the reference oracle's derivation against the written contract, not merely against the C# implementation or its outputs. Focus on:
   - frozen `aiq-synthetic-ohlcv-v1`, seed 114, UTC `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`; hourly 8160, 4h 2040, daily 340 rows; deterministic counter, tick pricing, OHLCV aggregation;
   - exact canonical dataset/window/visible bytes, header/row order, precision, hashes, 1/7/30/90-day end-anchored selections and 250 hidden warmup rows;
   - SMA20, SMA-seeded EMA20, Wilder RSI14, MACD12/26/9, population Bollinger20×2, nullable first-valid positions, 10^12 scale, rational ties-to-even and integer square-root midpoint rounding;
   - whether the C# test really checks all claimed rows, byte envelopes, aligned values/nulls, selection identity, frozen fingerprints, and two complete passes;
   - whether Python reference is meaningfully specification-derived and does not import production code or accept production-generated expected values.
4. Compare findings to the owner-reported C2B-17 results and explicitly distinguish **review conclusion** from **observed test execution**.
5. Produce a concise owner-facing review report in the Codex response, with file/line references, material findings, remaining disclosed supplemental gaps, and one outcome: **E4 ACCEPTABLE FOR SHOWCASE**, **E4 BLOCKED — CORRECTION REQUIRED**, or **REVIEW INCONCLUSIVE**.
6. If acceptable, recommend moving directly to the smallest next implementation slice for a visible synthetic historical candlestick chart and indicators. Do not initiate that slice under this authority.

## Independence and judgment

- Evaluate formulas and serialization against the frozen written specification. A passing cross-runtime comparison alone does not establish oracle independence.
- Do not invent reviewer credentials or claim third-party review. State clearly that this is an independent Codex reasoning review, not an external audit.
- Treat already disclosed supplemental gaps (injected subprocess failures, in-progress calculator cancellation, direct negative rounding-helper access, exhaustive mutation cases) proportionately; do **not** block the showcase solely because production-style edge coverage is absent.
- Block only for a concrete material numerical/contract error, inability to establish the contract, or evidence that claimed complete comparisons are materially incomplete. Give the smallest proposed fix.

## Hard boundaries

- **Read-only review only**: no edits, commits, PRs, merges, generated evidence files, builds, tests, Python execution, generators, snapshots, exports, package installs, native helpers, Job Objects, cloud/browser probes or deployments.
- No changes to frozen constants, expected fingerprints, provider separation, licensing boundaries, WP07 worktree/archive or protected owner files.
- No additional governance or containment qualification phase. If a genuine material defect is found, request a narrowly scoped two-test-file correction authority from the owner.
- Never assert public runtime verification or lift deployment/activation NO-GO.

## Required final report format

- **Baseline:** inspected commit, files and frozen-spec path.
- **Contract review:** brief checklist of independently verified derivations and exact semantics, with source line references.
- **Material findings:** severity, file/line, why it matters, minimal fix; or `None found`.
- **Evidence boundary:** cite owner-reported 21/21 local pass and state no execution occurred under C2B-18.
- **Disposition:** exactly one of the three outcomes above, with rationale.
- **Next step:** if acceptable, propose the smallest chart-UI slice; otherwise smallest necessary correction only.

**Success criterion:** a short, credible review decision that lets this showcase project move forward without unnecessary infrastructure or procedural complexity.

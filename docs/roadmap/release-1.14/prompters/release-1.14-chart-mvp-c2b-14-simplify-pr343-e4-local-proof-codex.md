# Release 1.14 Chart MVP — C2B-14 Codex authority
## Simplify open PR #343's E4 local proof contract — documentation-only amendment

**Owner decision:** The owner explicitly rejects the current Job Object / native containment complexity as unnecessary for this developer-controlled, offline E4 proof. Replace it with ordinary, finite, supervised local-test safeguards. This is **not** authority to create tests, run tests, build, restore, generate candles, export snapshots, deploy or activate anything.

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`.

## 1. Preflight and scope

1. Freshly verify PR #342 merged at `614e5490e2aaf37c5264a0bf0afccbe6f462ea44`. Freshly verify PR #343 is **open and unmerged**, with accepted-for-revision base `614e5490e2aaf37c5264a0bf0afccbe6f462ea44`, prior head `790574711b0699167fd5c0b5b5fb8f39ed1cd20c`, and exactly four new C2B-13 Markdown files. If its head, contents, branch ownership or protection state differ, STOP for owner review; do not overwrite concurrent work.
2. Amend **the existing PR #343 branch**, not `main`; do not merge #343, open a duplicate PR, delete branches, force push or bypass protection. Use only a verified safe checkout. Do not touch the protected dirty WP07 worktree/archive, private data or unrelated untracked files.
3. Only edit the two C2B-13 proposal documents listed below and, if necessary for factual consistency, the existing C2B-13 evidence document. Preserve the existing C2B-13 authority as historical authorization; do not silently rewrite it. Do not modify C2B-12 or earlier accepted records.
4. `PUBLIC_RUNTIME_VERIFIED=False`; public dataset/candles/indicators unavailable; Azure deployment/public activation **NO-GO**; advanced WP07 **ON HOLD**. Preserve the frozen `aiq-synthetic-ohlcv-v1`, seed 114, UTC coverage `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, 1h/4h/1d × 1/7/30/90-day selections, 250 hidden warmup, fingerprints, `aiq-ta-v1` semantics and public label **BTC-like Market Simulation — Synthetic Historical OHLCV**.

## 2. Exact amendment allowlist

Under `docs/roadmap/release-1.14/prompters/`:

- `release-1.14-chart-mvp-c2b-13-e4-bounded-execution-contract-design.md`
- `release-1.14-chart-mvp-c2b-13-e4-resource-and-owner-decision-worksheet.md`
- `release-1.14-chart-mvp-c2b-13-merge-and-design-evidence.md` **only** for a clearly dated amendment note, evidence correction or links; retain historical merge facts.

Do not change `release-1.14-chart-mvp-c2b-13-merge-pr342-e4-bounded-execution-contract-design-codex.md`. This new C2B-14 authority is an **external operator instruction**, not a fifth PR file. If branch ownership or required edits cannot fit the allowlist, STOP and request a new scope.

## 3. Replace disproportionate containment requirements

- Remove Windows Job Objects, native helpers, kernel-enforced aggregate CPU/RSS/commit limits, launch-time process association, exhaustive descendant accounting, independent watchdog and native fault-injection qualification as **mandatory prerequisites** for the first supervised local E4 proof.
- Do not describe best-effort cancellation or process-tree kill as a hard guarantee. Record the residual risk: descendants can survive, RSS sampling can miss peaks, and timeouts are not OS isolation. A failed/uncertain termination blocks **further runs** until manually inspected; never assert verified cleanup without evidence.
- Prefer the existing .NET Infrastructure.Tests infrastructure and test-only CPython oracle. Use an explicit bounded subprocess timeout, redirected concurrently drained stdout/stderr with byte caps, cancellation, `Kill(entireProcessTree: true)` when supported, finite wait, and operator inspection if termination is uncertain. No shell interpolation, PATH fallback, provider calls, package installation, unbounded retries or unreviewed private inputs.
- A developer-supervised, offline local run may use **advisory** memory observation and finite elapsed-time ceilings; do not claim hard process-wide CPU/RSS enforcement. Keep ordinary disk/output ownership, no-network intent and non-destructive cleanup. If an unexpectedly complex runner or native helper becomes necessary, STOP and request explicit authority rather than reintroducing it by implication.

## 4. Make the execution proposal small and actionable

Revise the worksheet to distinguish: (a) modest candidate local timeouts, output byte caps, repetitions and cleanup practices; (b) observational CPU/memory measurements, not blocking kernel-enforced ceilings; (c) actual remaining prerequisites for a future run. Retain finite coverage: three complete datasets, twelve windows, all aligned indicators and nulls, independent canonical bytes/fingerprints, deterministic repeat and explicitly enumerated negative/edge cases. Review whether the existing proposed 40 supplemental cases and 660-second session budget are proportionate; recommend simplification if justified, **without reducing frozen correctness coverage**. Label every value candidate/not measured/not approved. No command is authorized now.

Keep the two proposed test-only source paths as future candidates, not edits:

- `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticSnapshotQualificationTests.cs`
- `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/synthetic_snapshot_reference.py`

Require a clear, separately authorized future creation/run plan that resolves exact installed .NET and CPython executables, offline/no-restore constraints, minimal build/test output paths, bounded logs and temporary files, finite timeouts and operator-supervised abort procedure. Avoid requiring an exhaustive generated-file inventory before ordinary local test development; require a controlled external output root or a reviewed safe existing test-output pattern, with no persistent candle/indicator datasets. Preserve independent oracle derivation/review before claiming parity; an independent reviewer must be identified before evidence acceptance, but a missing named reviewer need not block *documentation planning*.

## 5. Governance and validation

1. Clearly mark the simplified C2B-13 proposal as the **owner-directed revision** superseding its earlier Job Object preference and blocking native-bound table. Preserve the original PR #342 merge evidence and source observations as historical facts; do not misrepresent old text as currently approved.
2. Validate the exact changed-file allowlist, Markdown links/headings, `git diff --check`, frozen constants, and preservation of all unrelated paths and untracked material. Record before/after PR head SHAs, updated design choices, unresolved ordinary local-run details and any STOP findings.
3. Push a normal non-force update to **PR #343**, leave it **open and unmerged**, and report the new head and changed-file inventory for owner review. Do not treat the owner's request to simplify as approval to merge or run tests.
4. Explicitly report: no test/source/fixture implementation, builds/restores, Python/.NET test runs, candle generation, snapshot/export, native/Job Object experiment, Azure/browser probe or deployment.

**Exit:** PR #343 contains a proportionate, documentation-only local E4 proof proposal for owner review. Executable proof, implementation, export and public activation remain separately gated; `PUBLIC_RUNTIME_VERIFIED=False`.

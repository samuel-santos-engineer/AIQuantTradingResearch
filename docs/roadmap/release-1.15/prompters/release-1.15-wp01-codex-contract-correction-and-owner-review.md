# Release 1.15 WP01 — Contract Correction and Owner Review

## Authority and scope
Work in `samuel-santos-engineer/AIQuantTradingResearch` on existing open PR #380, last observed at head `2dc19d8e9dc0eba575e699928e14cc877c8c8dc6`. Verify live head and owner branch before any edits. This prompt authorizes a **documentation-only correction pass** responding to the review comment at https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/380#issuecomment-6364065366. The owner's approval to prepare this prompt and corrections is **not** acceptance of D1/D3/D4/D5 contract proposals, nor authority to merge, close #367, start WP02, implement source code, change governance, call a provider or deploy. `PUBLIC_RUNTIME_VERIFIED=False`.

Read `AGENTS.md`, Docker-first and historical crypto governance, LF policy, Release 1.15 execution/acceptance/manifest, and all PR #380 closeout/decision/qualification/proposal documents. Preserve merged PR #379 feasibility history, including previously observed failures. If the checkout is dirty, a prompt is untracked, or the PR head diverged, stop and request explicit recovery authorization; do not delete, move, reset, force-push, or overwrite user work.

## Allowed files
Only modify existing PR #380 documentation under `docs/roadmap/release-1.15/`:
- `RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md`
- `RELEASE_1.15_WP01_DECISION_REGISTER.md`
- `RELEASE_1.15_WP01_CONTRACT_CLOSEOUT.md`
- `RELEASE_1.15_WP01_DOWNSTREAM_QUALIFICATION_MATRIX.md` only if necessary for consistency
- `RELEASE_1.15_FILE_MANIFEST.md` only if index changes are necessary

Do not add an implementation, executable tests, dependencies, scripts or extra files without a separate owner decision. Use reasoning and independently calculated *documentation vectors*, clearly labeled as reviewed calculations rather than executed product tests.

## Correction A — D3 Bollinger variance/rounding and anchors
1. Independently derive population variance for integer close units `p_i`, window n=20: `A = n*sum(p_i^2) - (sum(p_i))^2`, so standard deviation in integer price units is `sqrt(A)/n`. Check the existing floor and nearest-even midpoint inequality algebraically. In particular, with `k=floor(sqrt(A)/20)`, compare `4*A` with `20^2*(2*k+1)^2`; preserve ties-to-even when equality holds. Do **not** change a mathematically correct expression merely because review requested verification. Document derivation and independently constructed exact-square, below/above-midpoint, and true-tie cases (if integer-window inputs permit a tie; otherwise explain feasibility and use a rational boundary proof). Verify no negative `A` for valid integer inputs.
2. Recalculate the flat, rising, falling and 400/401 anchors independently. The existing rising `close=open=high=low=t` ATR anchor may require correction because previous-close true range differs from same-bar range; verify `ATR14` and the stated EMA/MACD anchors precisely. Explicitly separate analytical expected values from executed regression results. Do not silently copy candidate expected numbers into a production golden fixture.
3. Reconcile indicator null/warmup, checkpoint and gap semantics if an anchor correction exposes an inconsistency. Keep no-lookahead and 100 warmup + 300 prior calibration, first eligible classification on consecutive candle 401.

## Correction B — D4/D5 positivity, rounding and protocol consistency
1. Evaluate minimum valid positive raw price at fixed-point scale `S=10^12` under daily factor `F/S` from 0.90 to 1.10 and ties-even integer rounding. A positive raw unit of 1 can round to 1 or 1? Determine exact lower endpoint and whether any valid positive OHLC can become zero; explicitly reconcile input minimum, transformed OHLC `>0`, and rejection/availability semantics. Do not invent a different transformation without documenting the tradeoff and owner choice.
2. Check all D4 zero-row `full` status fields and observation timestamps against the proposed status schema, integrity states and empty initialized DB behavior; ensure example JSON and prose match, and `unchanged` has correct identity/status semantics.
3. Cross-check D4 cache identity and D5 key rotation/UTC-day rollover: generation/version/displayDate/transformEpoch, monotonic counters, coherent two-instrument transform, stale in-flight responses, and whole-response replacement. Provide explicit illustrative old/new identity cases, and distinguish independently computed vectors from product tests.
4. Review schema field spelling, bounds, decimal encoding, null policy and error semantics for contradictions introduced by corrections. Preserve private raw data and keys from public DTOs/logs.

## Correction C — D1 privileged trust and portability
1. Make the proposed retained-root PID1 model an explicit owner trust decision, not an implied least-privilege proof. Document the reason SETUID/SETGID/SETPCAP/KILL remain, and the exact boundaries not proven by S2.
2. Review role-UID census, pidfd/wait/reaping, seccomp clone/clone3 and no_new_privs assumptions against the actual Linux mechanism requirements. Where implementability depends on kernel, container runtime, /proc visibility, App Service or available capabilities, state precise fail-closed prerequisites and WP02/WP07 tests; do not assert Azure F1 compatibility from a paper review.
3. Preserve the two non-root roles, private filesystem separation, loopback-only research API, no-overlap restart and no privilege escalation fallback.

## Owner review disposition
- Keep D1 PROVISIONAL, D3/D4/D5 OPEN and D2/D6/D7 PROVISIONAL until the owner **explicitly accepts the revised choices**. The user's instruction to create this prompt does not approve the proposed contract values.
- Keep WP01 BLOCKED and issue #367 OPEN; WP02 #368 unstarted. Preserve all 22 downstream qualification owners and 12 pending release acceptance rows.
- Add a concise decision-ready checklist identifying: proposed corrected choices, alternative where material, residual implementation/qualification obligations, and exact owner acceptance needed. Do not turn downstream production tests into WP01 planning acceptance gates.
- No provider-licensing implementation prerequisite, no new governance approval gate, no production verification claims.

## Validation and publication
Check exact changed-file allowlist, all relative links/anchors, JSON example syntax, D1–D7 status/owner consistency, Q01–Q22 and R115-01..12 mapping, UTF-8 without BOM, LF only, final newline, and `git diff --check`. Document what was calculated or reviewed versus **product tests NOT RUN**. Commit and push to the existing PR #380 branch without force only if branch remains safe; leave PR open/unmerged. Return PR URL, new full SHA, changed files, concise corrections (including numerical derivation and positive-minimum conclusion), unresolved owner decisions, validations, and unchanged `PUBLIC_RUNTIME_VERIFIED=False`. Stop and ask if the needed correction exceeds the documentation-only boundary.

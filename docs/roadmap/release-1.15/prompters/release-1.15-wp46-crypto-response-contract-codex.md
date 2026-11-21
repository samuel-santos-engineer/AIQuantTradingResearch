# Codex execution prompt — Release 1.15 WP46

## Mission

Work in `samuel-santos-engineer/AIQuantTradingResearch`. Baseline is PR #432 squash merge `819298b4f02258e4345e2ebd301cadff5bacbffa`. Establish the Twelve Data **BTC/USD and ETH/USD 1min** success-response contract and implement only evidence-backed parser compatibility improvements, to unblock the authentic historical candle chart. Create a new PR for owner review; do not merge it.

## Authority and hard limits

- **Offline/public-documentation research and synthetic testing only.** No real Twelve Data API request, DNS/HEAD/credit/account probe, acquisition invocation, retry, credential read or presence inspection, historical private response inspection, or access to private prepared artifacts. Do not use provider API example URLs that execute requests. No Azure, registry publishing, or production operations.
- No provider licensing/approval governance gates; focus on engineering. A future real Acquire needs a **new, explicit, single-attempt owner authorization**; no authorization is conferred by this prompt or a PR merge.
- Preserve owner checkout/untracked changes; work from an isolated clean branch/worktree. Respect AGENTS.md and repo governance. No force push to main.

## Research and decision

1. Verify merged baseline, inspect `TwelveDataMinuteCandleSource.cs`, Worker `ResearchLocalHistory.cs`, `eng/docker/release-1.15/wp39_local_history.py`, tests, and WP39–WP45 runbooks.
2. Examine authoritative Twelve Data public *documentation* for `/time_series` crypto BTC/USD and ETH/USD success envelopes, status, `meta`, `meta.symbol`, `meta.interval`, `meta.exchange_timezone`, `timezone=UTC` semantics, candle datetime/volume, and differences between exchange timezone and output timezone. Record exact source URLs, retrieval dates, and what each source **does and does not guarantee**. Distinguish examples from normative guarantees. Do not infer the content of WP44's private response.
3. Build a field-by-field contract/necessity matrix: required for identity, UTC interpretation, minute alignment, provenance, and safe publication vs merely formatting assumptions. Explicitly assess whether `status` and `exchange_timezone` can be absent on successful crypto responses and whether exchange timezone must equal UTC. Do not relax any check without credible evidence **and** an equivalent correctness guarantee. If sources remain inconclusive, leave acceptance unchanged and document the exact unanswered questions and a practical way to obtain authoritative clarification without consuming provider credits.
4. If a safe narrow compatibility change is justified, implement it with clear rationale and fail-closed handling of ambiguous payloads. Preserve requested BTC/USD then ETH/USD, `1min`, `outputsize=1000`, `timezone=UTC`, `order=ASC`, existing 24h completed-observation window/grace, strict symbol/interval and timestamps, bounded JSON/body, OHLCV/base-asset volume, duplicate/order/window checks, provenance, atomic publication, and no retry. Preserve the fixed version-1 ten-field diagnostic envelope and synchronized C# / Python allowlists. Avoid adding diagnostics merely for their own sake.
5. Add synthetic-only tests for documented success variants and rejection of missing identity, contradictory timezone, malformed metadata, error envelopes, forged diagnostics, and second-symbol failure without partial publication. Ensure actual C# Worker envelopes round-trip through the actual Python validator when codes change.

## Verification

- Run the focused C# acquisition/adapter/initializer tests, Python local-history workflow tests, and chart/client tests using existing cached dependencies only. Baseline: **200 passed** (116 C#, 23 Python workflow, 61 chart/client). Report actual counts and failures; do not claim tests you did not run.
- Run UTF-8/LF/no BOM and `git diff --check` checks. Avoid network package restore, provider traffic, Docker execution unless necessary and demonstrably offline, and any private artifact disclosure.
- Document exactly which payload shapes are now accepted/rejected and why; if unchanged, explicitly say so. Do not claim the WP44 `INVALID_PROVIDER_METADATA` root cause is identified: exact field and credits are unknown.

## Deliverables

- Add `docs/roadmap/release-1.15/prompters/release-1.15-wp46-crypto-response-contract.md` with authoritative source matrix, code decision, test evidence, unresolved uncertainties, and next implementation action.
- Append concise WP46 evidence to `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md` without rewriting historical records.
- Commit any narrowly justified code/tests, create a single WP46 PR against `main`, **OPEN/UNMERGED**, and report PR URL, full head SHA, changed paths, test counts, and precise outstanding blockers.
- State `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`. No live Acquire, import, local browser qualification, or Azure actions.

## Stop conditions

Stop rather than guessing if authoritative crypto-specific documentation is insufficient for a safe parser relaxation. A documentation-only PR with a concrete engineering decision and reproducible offline evidence is acceptable; do not invent provider guarantees or use a new live request to settle the ambiguity.

# Codex execution prompt — Release 1.15 WP55

## Mission

Advance AIQuantTradingResearch from merged PR #442's approved Twelve Data OHLC-only implementation toward **authentic BTC/USD and ETH/USD historical candles in the local chart**. Prioritize execution and narrowly necessary fixes over further governance or speculative diagnostics. Start from `main` at or after PR #442 squash commit `61a1f804526b145e8a3d47f1daf4795f3fbec375`. Create an **OPEN, UNMERGED PR** for owner review, not a direct merge.

## Immutable product contract

- Twelve Data is the **sole authoritative market-data provider**. No other providers, fallback sources, synthetic substitution, invented prices, or unverified exchange-volume conversions.
- Instruments BTC/USD and ETH/USD, authentic completed UTC-aligned 1-minute OHLC, existing bounds/window/grace/order checks, sequential BTC then ETH, first-failure stop, no retry/pagination, atomic two-symbol publication.
- Explicit `--ohlc-only` is the intended acquisition mode for these crypto feeds. Preserve the `volume` field throughout C# candle model, bundle, importer, SQLite, transformed API, Python client and chart. Missing/unavailable volume is JSON `null`, **not zero**. Preserve genuine numeric volume, including legitimate zero, in strict OHLCV mode for future instruments or Twelve Data feeds. In OHLC-only mode do not assign unverified volume units. Hide unavailable volume bars and show “Volume unavailable”; retain genuine price indicators.
- Strict OHLCV remains the default and its existing failure subtypes remain intact. Never bypass WP52 preparation identity checks or weaken security, HTTPS, authentication, source provenance, or data-integrity checks. Keep Docker-first **and** dual-process permitted.
- No new licensing or provider approval gates. Do not broaden scope into Azure deployment or unrelated full-suite fixes.

## Authority boundaries — stop and request owner approval

The user's instruction to create this prompt and the merge of PR #442 **do not authorize operational actions**. Codex may inspect code, Git metadata, nonsecret documentation, and run ordinary local offline tests in existing environments. Before Docker engine inspection, image builds, new containers, real `Prepare`, modification of owner-controlled resources, or other resource operations, **request a separate explicit, narrowly scoped Docker/local preparation authorization**. Before reading provider credential presence/value, sending any request to Twelve Data (including DNS/HEAD/test/catalog/account/credit endpoints), or running actual `Acquire`, **request a separate explicit single-attempt live acquisition authorization**. Do not assume earlier WP47/WP53 or 12-query diagnostic permissions remain available. Treat authorizations independently; record exactly what was approved and executed. Do not request or expose secrets in chat, logs, PRs, test artifacts, or process command lines. Never inspect unrelated private state, TLS private material or existing historical bundles.

## Phase A — safe offline preflight (no new operational authority needed)

1. Confirm clean source checkout, current branch, merged PR #442 commit, and that OHLC-only implementation and governance amendment are present. Preserve owner's checkout and private preparations; work on an isolated branch/worktree if necessary.
2. Review `eng/docker/release-1.15/wp39_local_history.py`, WP52 identity guard, WP53 evidence, PR #442 runbook, and current operator flow (`Plan`, `Prepare`, `Acquire`, `Import`, `Start`, `Verify`). Determine exact supported commands, prerequisites, ownership context and expected outputs. Do not invoke operational commands yet.
3. Run focused **offline** regression tests for the C# Application, affected Infrastructure tests, Python workflow and chart/client using existing cached tools only; do not install or download packages. Record exit codes and counts. The 29 baseline-matched Infrastructure failures (12 Debug Worker, 16 checkout-local Python, 1 E4 oracle) are a documented separate prerequisite; do not claim a green full suite, suppress tests, grant oracle authority, or refactor unrelated harnesses.
4. Identify whether any minimal production fix is required for the authentic acquisition path. If so, implement and test it with offline fixtures only, preserving the contract above. Avoid documentation-only churn.

## Phase B — fresh matching local preparation (only after explicit resource approval)

5. Request specific authorization for one new owned `Prepare` and the bounded Docker/cache-only operations it requires. No live provider request is included in this approval. Confirm Docker context/required cached inputs under approved scope; no pulls, network-enabled builds, registry access, Azure or unrelated resource mutation.
6. Run **one** fresh `Prepare` from the post-#442 source in a new owner-controlled directory. Do not reuse or rewrite the WP53 preparation: its identity is stale by design. Verify exact workflow SHA, full Worker publish/context match, manifest, ACL/ownership and intended TLS configuration using only permitted nonsecret checks. Retain owner-owned artifacts and record sanitized identity evidence. Stop on mismatch, denied access, missing prerequisites or unexpected behavior; no bypass.
7. If resource approval is not granted, stop at Phase A, document precise blocked steps, and still create the scoped review PR if useful. Do not claim readiness.

## Phase C — single controlled authentic acquisition (only after a NEW explicit live approval)

8. Before asking for approval, present the exact command, intended two instruments, expected maximum of two sequential provider requests (BTC first; ETH only if BTC succeeds), stop-on-first-failure behavior, no retry, anticipated data window and known unknowns. Ensure fresh identity, ownership, TLS/ACL and empty destination checks. Do not inspect credential presence before approval.
9. Ask for explicit owner authorization for **one** controlled Twelve Data `Acquire` invocation in `--ohlc-only` mode. Do not infer this approval from Docker permission or this prompt. On approval, use the existing private credential channel and existing operator command, e.g. `python -B eng/docker/release-1.15/wp39_local_history.py --action Acquire --directory <fresh-matching-preparation> --execute-provider-approved --ohlc-only`, with exact validated path substituted privately. No credential in CLI arguments. Do not perform separate exploratory queries.
10. Capture only the sanitized fixed diagnostic envelope and nonsecret outcome evidence: success/failure stage, instrument, request/response counts, failure subtype, publication state, and whether two-symbol atomic publication completed. No raw provider response, candle prices, secret fingerprints, private paths or payloads in PR. If first request fails, stop; no ETH call. If any uncertainty or partial result occurs, stop and report; no retries or second attempts without fresh authorization.
11. **Do not automatically import, start containers/application, or browse** merely because Acquire succeeded. Determine the exact next permitted owner-controlled actions and seek separate authorization as appropriate. If explicitly authorized for import/local runtime verification, use the existing authenticated local workflow, prove authentic provenance, both symbols, null volume and price indicators in the actual chart, and report the observed state. Otherwise leave import/chart as NOT_RUN and provide the precise next operator action.

## Deliverables and acceptance

- An OPEN/UNMERGED focused PR against `main` with minimal necessary code/tests and a concise WP55 execution/evidence note. If no code fix is necessary, an evidence-only PR is acceptable **only if it records meaningful executed qualification**, not repetitive planning.
- Record exact baseline/head SHA, changed paths, offline test commands/counts, relevant pre-existing failures, explicit approvals, commands actually executed, sanitized outcomes, retained resource ownership and all unexecuted steps.
- Distinguish `PREPARED`, `ACQUIRED`, `IMPORTED`, `LOCAL_CHART_VERIFIED`, and `PUBLIC_RUNTIME_VERIFIED`. Do not claim any milestone achieved without its actual evidence.
- Keep status flags honest: `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false` until evidence justifies a change. A successful acquisition alone does not mean imported data or public readiness.
- Keep UTF-8/LF/no BOM, `git diff --check`, and no sensitive data in commit/PR.
- **Stop at every authorization boundary** and wait for the owner; never self-approve. Preserve all existing private preparations and owner resources.

## End-of-run report

State what was actually executed, what passed/failed, what is still blocked, the next exact authorization request if any, PR URL/head SHA, and whether real BTC/USD and ETH/USD candles are imported and visible in a local chart. Keep Twelve Data the only truth source.

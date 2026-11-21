# Codex execution prompt — Release 1.15 WP59: local UI image build and browser qualification

You are working in `samuel-santos-engineer/AIQuantTradingResearch`. Continue draft PR #448 (head last observed `a8ce55ba0d1a81429f88df66436b97f2283631d4`) without merging it. Its WP59 source implements inclusive UTC historical range navigation in `python/presentation/research_chart.py`; 70 Python tests passed, but the new source has **not** been built into a UI image or browser-tested. Read the latest PR and repository state first; if head has advanced, reconcile rather than overwrite. Do not modify PRs #445/#446/#447.

## Goal

Build a **new local-only WP59 UI Docker image** from the PR #448 implementation, run it against the **existing accepted local .NET API container, imported authentic BTC/USD and ETH/USD volume, matching service key and recovered seven-day TLS pair**, and complete reproducible browser acceptance. Preserve the accepted historical images and retained data for rollback. The recovered baseline chart is `http://127.0.0.1:18555` (loopback on the operator machine); do not assume that the port is still active or that the certificate remains valid. Azure F1 quota is blocked: **no Azure, GHCR push, cloud, production, market-data provider, acquisition, or new resource operations**.

## Preconditions and protection

1. Inspect current branch, PR #448 diff, Dockerfiles and launchers (`container/release_1_15/research-chart.Dockerfile`, `eng/docker/release-1.15/wp39_local_history.py`, `wp07_local_historical_chart.py`), and current owned Docker state. Preserve the operator checkout and private preparation; use an isolated worktree if appropriate.
2. Verify existing accepted images by exact local IDs: UI `sha256:3270ce6ec33ba8cb7da27738ea6d9dc27a811a3270e7ecdef83f32e757a83ae1`; API `sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028`. These are local image identities, not registry manifest digests. Verify actual current containers and their source image IDs independently.
3. Check recovered TLS certificate validity and SAN `wp07-research`, consistent server/UI CA mounts, existing key equality without exposing values, existing authenticated BTC and ETH 1,000-row snapshots, and owned volume identity. Never print secrets, private paths, raw observations, TLS material or database contents. Do not regenerate keys, import data, or silently switch to the expired historical manifest.
4. Before changing any container, prepare a rollback ledger with exact owned container/network/volume identities, image IDs, port mapping, nonsecret environment names, and privately retained TLS/preparation references. Avoid destructive actions on the imported volume. Stop if ownership or rollback feasibility is ambiguous.

## Local image build

5. Build **only** the changed Python UI image, with the repository's supported Dockerfile, an audited allowlisted context, Linux/amd64, and cached dependencies where practical. Do not rebuild the API or alter its source. Verify the image contains the new historical navigation source and no private preparation, imported observations, SQLite, keys or TLS files. Record the new immutable local image ID, command/entrypoint, UID and port. Never overwrite the accepted UI image tag/ID; use a distinct WP59 local tag.
6. Run the existing focused offline regression suite on PR #448 (`PYTHONPATH=python/presentation`, `python -B -m unittest test_research_chart test_research_snapshot_client -q`) and report exact count/exit code. If build or tests fail, stop and document the failure; do not claim runtime acceptance.

## Bounded runtime/browser qualification

7. Using supported local launcher options and the retained **same** API/data volume/key/TLS configuration, replace or start only the owned UI session with the new WP59 image. Avoid API restart if possible. If launcher forces both containers to be replaced, first prove that the existing data volume, service-key equality and current TLS pair will be preserved; otherwise stop and report the unsupported launcher limitation. Keep the chart loopback-only. No uncontrolled restart loops; one bounded start and one bounded recovery attempt only if a precise, reversible failure is understood.
8. Verify authenticated API readiness and exact BTC/USD and ETH/USD 1,000-candle counts (without logging raw payloads), then Streamlit readiness. Do not confuse HTTP readiness with browser acceptance.
9. Use a real browser against the local UI. Exercise BTC/USD -> ETH/USD -> BTC/USD, apply a bounded inclusive UTC range, a single-candle range and full-range selection, Return to Live and candle presets. Verify viewport changes while loaded snapshots stay at 1,000 rows per instrument; SMA20 retains 19 null warmup and 981 computed values for each full snapshot, and volume remains null without fabricated bars. Check gaps/unavailable-state messaging where supported. Verify no stale pan/zoom gesture overrides an explicit range command. Record screenshots or compact sanitized observations without secrets, private file paths or raw historical data. If browser automation is unavailable, explicitly mark browser acceptance NOT_RUN rather than inferring it from unit tests.
10. Check that the API and retained volume remain unchanged and that the accepted baseline UI image is still available for rollback. If browser tests fail, report the exact failure and propose a minimal fix; only implement fixes within WP59 scope, rerun affected tests and acceptance, and do not silently merge or broaden scope.

## Evidence and PR

11. Append a concise build/runtime/browser qualification section to `docs/roadmap/release-1.15/prompters/release-1.15-wp59-local-history-navigation.md` on PR #448. Include exact image identity, test counts, startup checks, browser cases PASS/FAIL/NOT_RUN, evidence limitations and rollback status. Keep UTF-8/LF/no BOM and run `git diff --check`.
12. Push the focused update to **draft PR #448**, leaving it OPEN/UNMERGED. If and only if all required browser checks pass, recommend marking it ready for review; do not merge. Preserve PRs #445/#446/#447 unchanged.
13. End with a succinct outcome matrix and a single concrete next step. Maintain `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` regardless of local success; local success is not Azure proof. No Azure or provider operations.

Prioritize working local functionality and verifiable browser results. Do not introduce extra governance gates, new infrastructure, or unrelated refactoring.

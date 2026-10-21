# Codex execution authority — C2B-35 to C2B-37

**Owner approval:** Implement, test, create and merge three sequential indicator PRs, and deploy each accepted phase to the **existing Azure staging app only**, automatically until all three phases pass. This authority does **not** authorize production changes.

## Immutable product boundary

The product is **a publicly accessible, educational synthetic crypto-chart application—not a live or real historical market-data platform**. Follow `docs/governance/SYNTHETIC_PUBLIC_PRODUCT_SCOPE.md` (merged PR #352) and the consolidated baseline from PR #354. Use only deterministic synthetic OHLCV. No quote providers, external feeds, private-provider paths, new Azure services, live trading, or real-data licensing work. Preserve existing owner documents and archived WP07 material.

## Authorized sequential work

1. **C2B-35 / PR 1:** C# EMA(20) and Bollinger Bands(20, 2) on synthetic close; overlay on original Plotly candlestick chart. Maintain existing SMA(20), volume, ranges, and Technical Analysis controls.
2. **C2B-36 / PR 2:** C# RSI(14) and MACD(12, 26, 9) on synthetic close; separate, labeled Plotly lower panels with RSI 0–100 and MACD line/signal/histogram. Preserve overlays and usable chart layout.
3. **C2B-37 / PR 3:** C# ATR(14) using synthetic high/low/close; separate, labeled volatility panel. Preserve all prior indicators.

Use standard, documented formulas and explicit warm-up/null handling. Ensure calculations are deterministic, finite and aligned to visible timestamps across all supported timeframe/range combinations. For Bollinger Bands specify population vs sample standard deviation in code and tests; prefer population. For RSI document Wilder smoothing and edge cases; for ATR document Wilder smoothing and true range. Document EMA/MACD seed conventions. No Python recalculation of authoritative C# indicators.

## Mandatory gate for EACH phase (do not skip)

A. Inspect current repository, GitHub PR status, existing CI, Azure deployment workflow and staging configuration. Verify the exact staging resource identity and current staging image/digest; verify production resource is distinct. Do not assume `main` deployment automatically targets staging. If any workflow can auto-deploy to production on merge, **STOP** and report; do not merge until production is protected from the trigger.

B. Make only bounded phase changes, with unit tests for canonical formulas, warm-up, constant/zero-volatility cases, missing/invalid data, finite outputs and all supported selection shapes. Preserve immutable synthetic dataset provenance and public API contracts. Do not modify provider licensing or public data readiness behavior.

C. Build the actual Linux/amd64 Docker image using the repository's Docker-first policy. Run tests and a real browser acceptance against that image with resource-constrained, non-root/read-only runtime where supported. Verify controls on/off, timestamp alignment, panel layout, chart behavior, labeling and preexisting SMA(20). Capture reproducible commands, results, image digest and evidence.

D. Open **one phase-specific PR** to `main`, include exact changed paths, test/evidence summary, image/digest and staging-only deployment plan. Check the diff for scope compliance and required checks. Merge **only after** all phase-local gates pass and production auto-deployment is proven disabled/isolated. If merge requirements or checks fail, STOP instead of bypassing protections.

E. Publish the tested immutable image to the approved existing registry only if required for staging; promote **the exact tested digest** to the existing Azure **staging** app `aiqr112wp05wus27f5eabb5` in West US 2. No rebuild after local certification, no mutable-tag promotion, no publication of unapproved real/provider data. No new registry, resource group, App Service plan, web app, secrets or paid infrastructure. Capture previous staging image/digest and configuration for rollback before mutation.

F. Verify staging by real HTTP/API and browser tests: synthetic labeling, candles, volume, indicator math/visibility, range selection, no real-data claims, expected 503 JSON for unavailable public data APIs and 404 JSON for unknown API routes, as applicable. Check health/logs/restarts and compare staging digest with certified digest. If any check fails, **rollback staging to the captured prior digest/config**, report failure and STOP; do not proceed to next phase.

G. Record final phase evidence and move to next phase only on full success. Keep three independent PRs and three separate staging promotions. Never deploy to production, change production settings, or assert production certification based on staging checks.

## Permissions and stop conditions

**Authorized without further owner prompts:** scoped code/tests/docs changes; local Docker build and tests; three PRs and merges when all checks pass; necessary existing-registry publication of certified synthetic-only images; deployment and rollback of the **existing staging app only**; evidence collection and completion reporting.

**Not authorized:** any production deployment or production configuration/secret changes; changing governance; enabling real historical/live data; adding cloud services or spending; modifying protected owner documents; bypassing branch protection; force pushes; destructive cleanup of unrelated work; merging unrelated PRs; changing production auto-deployment triggers without separate approval.

**Stop immediately and request a new decision** if the production deployment path cannot be isolated, resource identity or rollback cannot be verified, staging requires new paid infrastructure, required checks fail, the scope requires modifying synthetic-only governance, or permissions are insufficient. Do not silently replace a failed test with weaker evidence.

## Completion report

For each phase report PR URL/merge SHA, changed files, local Linux Docker test results, image manifest digest, staging deployment identity and digest, browser/API verification, rollback reference, and any deviations. Conclude with the final staging URL and explicitly confirm production was not modified.

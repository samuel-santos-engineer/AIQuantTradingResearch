# Codex implementation prompt — Release 1.15 WP38: Local Docker historical crypto chart

## Authority and starting point

Owner approved moving Release 1.15 implementation and milestone progress to local Docker, with Azure public deployment deferred because of recurring F1 stop-request quota exhaustion. PR #425 was squash-merged into `main` at `e1e02ae63e5d7a92a53dbc86c0496970aebe2974`. Begin from freshly synchronized `main`, confirm that commit is present, and create a dedicated WP38 branch and reviewable PR. Do not merge the PR.

Implement the smallest practical end-to-end **local historical cryptocurrency chart and research interface** using the repository's existing Docker-first and dual-process architecture. Preserve existing governance, including `docs/governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md`, and reconcile any roadmap wording that incorrectly makes Azure public qualification a prerequisite to local implementation. No new provider-licensing approval gates. Do not silently change established architectural or data-source decisions; inspect current governance and code before choosing implementation details.

## Hard boundaries

- **NO Azure calls or mutations** of any kind: no CLI/ARM reads, app health probes, restarts, settings writes, deployment, quota diagnostics or plan changes.
- **NO registry publication**, image push, Azure image replacement, or release readiness assertion. Local image builds are permitted when needed.
- Preserve existing research API key semantics; do not read, print, rotate, replace, export or commit the actual Azure key. Use a synthetic local-only key for development and tests, never embedded as a production default.
- Do not modify the existing Azure UI or research App Services, their settings, or unrelated workloads.
- Preserve the existing Docker-first and dual-process policy. Prefer existing app/API/chart components and tooling over new frameworks or duplicated services. Keep changes bounded to WP38.
- No fabricated historical market data presented as authentic. Clearly label fixtures as fixtures. Do not claim authenticated historical data has been imported unless verified by actual local evidence.
- Do not create speculative provider/licensing blockers. Follow the source and provenance approach already decided in Release 1.15 governance; if an essential source detail is genuinely absent, implement a clearly separated fixture-based vertical slice and document the precise missing implementation input rather than inventing it.
- Do not perform or request a new Azure deployment as part of WP38.

## Inspect before implementation

1. Review merged WP37 runbook and local harness, existing research API/bootstrap/health, historical candle or snapshot contracts, data import/storage code, web UI framework and routes, Dockerfiles/Compose/PowerShell launchers, tests, roadmap and governance. Identify reusable paths and actual gaps. Avoid unnecessary rewrites.
2. Record the local architecture and acceptance criteria in a short WP38 runbook. Make the milestone status distinction explicit: `implemented`, `locally qualified`, `publicly qualified`.
3. Check that the local solution can run without Azure credentials, Azure services or a real Azure key. Avoid silently assuming a cloud-hosted dataset is already available.

## Implementation goals

Deliver a user-visible browser interface that runs locally and shows a historical **candlestick chart** for **BTC/USD and ETH/USD**, with:

- Instrument selection (BTC/USD, ETH/USD), a practical initial timeframe selector consistent with available data, chronological OHLC candles, visible time/price axes and sensible empty/loading/error states.
- Historical navigation (pan/zoom or range selection appropriate to the existing chart library) and a clear indication of the selected instrument, timeframe, date range and dataset provenance/status.
- A narrow, validated local research API candle endpoint or adapter returning documented candle timestamps, open/high/low/close (and volume only when actually available), with deterministic ordering, UTC time semantics, bounded ranges and clear error responses. Reuse existing API conventions and authentication boundaries; never leak a secret into browser bundles or URLs.
- A reproducible local historical-data path using the previously approved data approach where feasible. If only fixtures can be qualified in this WP, ensure they are conspicuously marked synthetic/test-only in both UI and documentation, and do not mark authentic data imported.
- A straightforward local launch procedure (Docker-first; dual-process allowed when appropriate), clear ports, minimal nonsecret configuration, health checks, and clean shutdown. Do not create a second orchestration system if the repo already has one.

## Implementation sequencing and tests

A. First establish the smallest local vertical slice: launch backend and UI, request bounded candle data, render BTC/USD candles, switch to ETH/USD, and change timeframe/range. Prefer integration over extra scaffolding.

B. Validate OHLC invariants (`low <= min(open,close) <= max(open,close) <= high`), timestamp uniqueness/ordering, timeframes, empty/invalid ranges, instrument allowlist, API error paths, and fixture-vs-authentic provenance labeling. Add targeted automated tests to existing suites.

C. Run local Docker smoke checks: cold startup, anonymous `/health` exact `200 Alive` for the research container where applicable, browser/UI route availability, successful chart data request, instrument switching, timeframes and range navigation, then cleanup only WP38-owned resources. Do not claim tests that were not executed; if a local browser automation tool is unavailable, state exactly what was verified via HTTP/component tests versus manual visual checks.

D. Keep local credentials synthetic. If authenticated API access is needed, use an appropriate local server-side bridge/proxy so no API key is embedded in client-side JS, network URLs, screenshots or logs. Preserve existing API authorization rather than weakening it to make the chart work.

E. Prefer bounded changes and existing dependencies. No unrelated upgrades, Azure fixes, GHCR publication, paid services, speculative redesign or license-gate creation.

## Roadmap, governance, and completion evidence

- Update the Release 1.15 roadmap/runbook to reflect **local Docker implementation and local qualification as valid milestone progress**, while clearly identifying Azure F1 deployment as **deferred and unresolved**, not passed or waived.
- Keep `liveAttemptReady=false`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, and `PUBLIC_RUNTIME_VERIFIED=False` unless a flag's specific factual meaning is independently satisfied; never flip public qualification on local evidence. If authentic local data is actually imported and verified, describe that evidence precisely and update only the relevant flag if its contract explicitly permits it.
- Document any Azure-only gaps (App Service transport authorization, staging storage/role checks, stop-request attribution) as deferred, not local failures.
- Report changed files, exact test commands/results, whether actual or fixture candles were displayed, exact local launch commands, any unverified browser behavior, and remaining work for a later public deployment.
- Check UTF-8/LF/no BOM where required, formatting, tests and `git diff --check`. Commit the bounded implementation to the WP38 branch, open a reviewable PR, **leave it OPEN/UNMERGED**, and stop for owner review.

## Definition of done

A developer can follow the documented local launch instructions, open a browser, view a clearly sourced historical BTC/USD candle chart, switch to ETH/USD, use available timeframe/range controls, and retrieve the corresponding bounded candle series from the local API. The system does not depend on Azure runtime availability. All applicable tests pass and any fixture-only limitation is explicit. Public Azure readiness remains unverified.

If the scope is too large for one safe change, deliver the smallest **working end-to-end vertical slice** in WP38 and enumerate follow-up improvements; do not replace working delivery with a planning-only document.

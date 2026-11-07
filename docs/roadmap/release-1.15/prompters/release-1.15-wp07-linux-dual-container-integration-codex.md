# Codex prompt — Release 1.15 WP07 Linux dual-container integration

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Base: `origin/main` containing merged PR #394 squash `6d16a41edbd4a0a576f0d87cd62b0677f2218ccf`.

## Goal
Implement and qualify the minimum **real Linux/amd64 dual-container** local offline integration for Release 1.15: independent non-root C# research API and actual Streamlit/Plotly historical chart UI, authenticated server-side HTTPS `X-API-Key`, persistent private SQLite/WAL and D5 transformation. This is WP07/WP02 operational integration evidence, not release acceptance or staging deployment. Preserve the already-merged browser evidence and Return-to-Live repair. Keep `PUBLIC_RUNTIME_VERIFIED=False` until genuine staging runtime verification.

## First actions and boundaries
1. Work in a new isolated worktree from the exact merged main SHA above; fetch and verify. Inventory original owner's dirty/untracked checkout before/after, preserving its HEAD, bytes, hashes and status. Do not reset/stash/clean/edit it. Do not import open/unmerged PR #386 or #387.
2. Read governance `docs/governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md`, `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md`, Release 1.15 definition/execution plan/file manifest, issues #368/#369/#372/#373, merged WP02–WP07 evidence, existing Dockerfiles, existing Docker test runner and Streamlit entrypoint/component assets. Use current committed contracts over assumptions.
3. Produce a short **exact literal-path change inventory** before edits. Reuse current files where possible. The known likely new paths are a production-like UI Dockerfile under `container/release_1_15/` and a local qualification runner under `eng/docker/release-1.15/`; inspect and choose exact names. If repository governance requires separate owner path approval, stop and request approval for only the concrete paths. No broad source mutation.
4. Check Docker daemon/platform and the exact pinned image digests/tags **before** any build. Do not silently substitute versions, pull images, install packages, or change dependencies. If pulling exact pinned bases is required, record precise images, expected network activity, and request authorization if not already allowed. If prerequisites remain unavailable, implement only the reproducible runner/image definition that can be justified and report Linux runtime as BLOCKED, not PASS.

## Implementation scope
- Build **two independent Linux/amd64 images**, each one principal non-root process: research C# service and **actual** Streamlit UI. The existing `snapshot-client.Dockerfile` is a test driver, not the final UI image. Use existing .NET/Python/Streamlit/Plotly versions and locked dependency inputs. No supervisor, shared PID namespace, privileged root runtime or cross-container localhost assumptions.
- Include the real Streamlit entrypoint, `research_chart.py`, `research_snapshot_client.py`, the local component HTML/JS and required local Plotly assets in the UI image. Verify components-v2 registration and rendered chart offline without CDN.
- Research container alone owns private durable SQLite directory and keys. UI has no research DB mounts, provider key or HMAC secret. Only server-side HTTPS to research hostname/port with verified certificate/CA and `X-API-Key`. Test invalid/missing key and TLS failure closed; avoid leaking secrets in logs, commands, evidence, browser payload or Docker layers. Separate test-only service and D5 keys.
- Use an **offline deterministic completed-candle fixture** committed through the actual WP03/WP04 state store and explicit D5 activation; do not fake a public API response or invoke live Twelve Data. Validate both BTC/USD and ETH/USD, transformed OHLCV, indicators, gap handling and 401st-candle readiness, with full/unchanged behavior. If fixture provisioning is missing, add the smallest isolated test-only provisioning mechanism, not a general production admin surface.
- Exercise container startup/readiness, shutdown/restart, non-root UID/GID, filesystem permissions, SQLite/WAL recovery/atomicity and bounded 1000 retention. Verify the two app boundaries and ensure no credentials/private values enter UI responses or chart data. Collect measured startup, CPU/memory and response latency only when actually executed; distinguish local observations from Azure F1 qualification.
- Prefer local Docker networking and ephemeral loopback exposure; do not deploy to Azure, alter App Service configuration, activate providers, publish images, or touch production/staging resources.

## Evidence and validation
- Run focused existing offline tests and the merged WP07 browser qualification where practical; retain known legacy synthetic hover baseline failure without changing unrelated tests.
- Build and run exact Linux images only when pinned prerequisites are available/authorized. Capture image identities/digests, commands, runtime UIDs, mount permissions, HTTPS authentication/TLS negatives, browser chart smoke, restart persistence and bounded resource samples. Record each acceptance item PASS/FAIL/BLOCKED/NOT RUN with actual reproducible evidence, not inference.
- UTF-8 without BOM, LF, final newline, independent CRLF scan and `git diff --check`. Inventory exact changed paths, SHA-256 hashes, tests and limitations. Keep all owner files byte-for-byte intact.
- Create **one focused PR, OPEN and UNMERGED**, against merged main, linking #368/#369/#372/#373. Do not close issues or mark WP07/release accepted. If blocked by unavailable pinned bases or missing inputs, make that explicit and propose only the concrete next step; do not invent successful Linux results.

## Reporting
Report: PR URL/head, exact file list, what was actually built/run, tests and real measured evidence, unresolved Linux/staging gaps, owner checkout preservation, and `PUBLIC_RUNTIME_VERIFIED=False`.

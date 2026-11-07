# Codex execution prompt — Release 1.15 WP07 local browser + Linux qualification

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Verified merged baseline: PR #393 squash `3a068341509c78af548b734ace1de737cc8dc613` (includes #389–#392). Target WP07 issue #373, with residual WP02 #368, WP03 #369, WP06 #372 evidence. Keep `PUBLIC_RUNTIME_VERIFIED=False` unless actual separately authorized Azure staging verification occurs.

## Objective
Qualify the **actual merged** BTC/USD and ETH/USD historical chart through a real browser, and the two **independent, non-root, single-process Linux/amd64 containers** with server-side authenticated HTTPS between Streamlit and C# research. Use only deterministic offline, pre-provisioned, completed 1-minute candle fixtures; no provider activation, Azure operations, deployment, production inspection, image push, or paid resources. This is a qualification/repair PR, not another architecture/planning PR.

## Mandatory preflight
1. Work from a clean isolated worktree at the exact merged commit, without modifying/stashing/cleaning the owner's dirty checkout. Record original HEAD/status and sizes/SHA256 of every dirty/untracked owner file, including all prompts; verify unchanged at the end.
2. Read the actual governance and release contracts, especially `docs/governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md`, `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md`, `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`, the WP02/WP03/WP04 evidence, and the merged WP05/WP06 code. Inspect open PRs #386/#387 only as context; do not import/merge their branches or assume their unmerged plans are authority.
3. Inventory available tools and pinned cached images, Docker daemon, Linux/amd64 publish artifacts, Streamlit/Plotly assets, browser executable/automation, TLS fixtures and a **disposable, exclusively owned** WP03 database with a valid witnessed D5 tuple. Do not claim qualification from static checks or AppTest alone.
4. Before edits, present a concise exact-path implementation/evidence manifest. Reuse existing approved paths wherever possible. If a new or unapproved tracked path is required, request narrow owner authorization; do not silently expand scope. Evidence can be recorded in existing release evidence documents if authorized. Preserve UTF-8/no BOM/LF/final newline and verify independently of `git diff --check`.

## Execution priorities
### A. Real-browser WP06 acceptance
- Launch the real Streamlit entrypoint with `AIQ_RESEARCH_CHART=1`, using a local offline research HTTPS endpoint with a trusted test CA and fake high-entropy service key, or a deterministic test transport if HTTPS is unavailable (clearly label reduced coverage).
- In an actual browser, prove BTC/ETH switching, candle/volume and supplied indicator rendering, transformed-value disclosure, UTC gaps, no fabricated candles, 100/300/1000 viewport presets, RSI/MACD panels, optional transition markers, zoom/pan/modebar/hover, Return to Live, refresh/unchanged, correction/epoch/date/generation rebuild, disconnect/reconnect and per-session/instrument viewport isolation. Check narrow-screen layout and repeated remount/dispose without leaking listeners or timers.
- Record reproducible commands, screenshots/artifact hashes where appropriate, browser/runtime versions and concrete PASS/FAIL/NOT RUN per case. Browser tooling unavailable is **NOT RUN**, never PASS. Do not install or upgrade dependencies without scope approval.

### B. Local Linux dual-container qualification
- Use the D1-B design: research C# and Streamlit UI in **separate** linux/amd64 images, each a single non-root principal process, no shared application filesystem/loopback, with UI server-side HTTPS to the research service over a distinct network address and `X-API-Key`. The WP05 `snapshot-client.Dockerfile` is a test-only Python driver, **not** the final UI image; qualify/build a real Streamlit UI image only under approved exact paths.
- Inspect and, if authorized, run `eng/docker/release-1.15/Invoke-Wp05SnapshotQualification.ps1` and existing WP02 Docker fixtures. Verify local-only Docker context, cached pinned images/no pulls, bounded cleanup of only owned containers/networks/volumes, test-only certificates and secrets, explicit mount permissions and exclusive SQLite writer ownership.
- Prove health/startup, genuine committed 401+ rows per instrument, full/unchanged C#→Python/UI, TLS CA+hostname and wrong/missing key rejection, D5 transformation confidentiality, non-root UIDs, independent process lifecycle, restart/recovery/witness, no plaintext secrets in browser/logs, WAL/mount behavior and reproducible CPU/memory/latency observations. Clearly distinguish synthetic fixtures from real provider polling; do not invent provider quota or Linux power-loss proof.
- Do not assume shared F1 performance or staging behavior from local measurements. If Docker/browser is unavailable, stop that portion with a precise blocker and smallest next action; do not fake a run or open a speculative deployment PR.

### C. WP03 operational boundary and legacy test
- Reconcile which WP03 #369 acceptance cases are supported by existing offline fixtures and which still need provider/quota or native Linux evidence. Do not activate Twelve Data or claim production ingestion.
- Rerun the known legacy `test_market_research_ui` hover assertion against merged baseline, if feasible; preserve its documented `Volume` vs `Simulated activity` baseline failure and do not change unrelated tests merely to get a green gate.

## Deliverable
- A tightly scoped qualification/necessary-fix PR against current `main`, **OPEN/UNMERGED**, with exact changed paths, source hashes, test counts, reproducible commands, browser and Docker evidence, all remaining blockers and a WP02/WP03/WP06/WP07 acceptance matrix. Do not close issues, merge PRs, modify Azure, provision/rotate production credentials, use real provider credentials, or deploy anything.
- If no executable browser/Docker qualification is possible, do not claim completion: provide evidence of checked prerequisites, explicit NOT RUN results and a minimal concrete follow-up. Prefer implementation progress over new governance or licensing gates.
- Preserve the one-key-per-environment policy and D5 private key separation; no key rotation framework, additional service, DNS redesign, or licensing approval gate. `PUBLIC_RUNTIME_VERIFIED=False`.

# Codex Prompt — Release 1.15 / WP58 Phase G: Deploy Locally Accepted Images to Azure F1 Staging

## Mission
Move from repeated diagnosis to a **controlled staging deployment** of the **locally accepted** Python/Streamlit chart and C#/.NET research API implementations. Reproduce local application behavior on the two **existing** Azure Linux F1 staging apps. Do not redesign the architecture. Keep PR #445 and PR #446 open/unmerged.

## Known evidence and limitations
- Historical WP57 local browser acceptance: BTC → ETH → BTC, 1,000 authentic candles each, SMA20 (19 warmup / 981 calculated), null/unavailable volume, no browser errors. `LOCAL_CHART_VERIFIED=True` (historical).
- Accepted local pair used corrected UI source from merged #444 with retained WP55 research API image; these **local Docker image IDs are not registry OCI manifest digests**. Do not silently substitute the newer unmerged #446 initializer implementation.
- Azure research staging previously configured an older WP35/WP37 image digest `sha256:f1e4916ccad30c30a59e1aa2b99b8c77cbfb87da6509fb1b51e6e6526930987f`; configured reference is not proof of actual running image or cause of the HTTP 403 / later timeout.
- E1 configured-only PASS, E2 running identity UNKNOWN, E3 API HTTP headers timeout, E4/E5 NOT_RUN. No Azure `IOException` proven. The `System.IO.IOException` in #446 occurred in a **local Linux fake-test container**, not Azure.
- Docker images contain application code, **not** the retained authentic candle dataset. Azure ephemeral SQLite may be empty or lost after image replacement; do not claim a functioning populated public chart until separately verified. No new provider requests or fabricated observations.
- `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false` until real staging acceptance.

## Exact deployment targets (no production)
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`
- Subscription: `7f5eabb5-80f5-4369-88be-b14a6de763f1`
- Existing West US 2 Linux F1 plan: `asp-aiq-r112-wp05-wus2-7f5eabb5`
- UI staging app: `aiqr112wp05wus27f5eabb5` — `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`
- API staging app: `aiqr115researchstagingwus27f5eabb5` — `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/`
- **Excluded:** production `aiqr112wp035ec325382770`, all other apps and resources.

## Operational authorization boundary
This prompt authorizes **preparation, verification, and reviewable deployment artifacts only**. Do **not** infer authorization for Azure writes, registry push/pull, Docker builds, credential reads, private-data transfer, application restarts, live HTTP probes, or deployment merely from this prompt. Prepare a compact **exact-operation approval request** before each consequential live phase; execute only after the owner explicitly approves that phase. If the owner has separately given an explicit scoped operational authorization in the active Codex session, obey its exact limits and record it. Do not convert this into a broad approval questionnaire or additional governance/licensing gate.

## Work plan

### 1. Establish accepted artifacts and reproducible build inputs — offline
- Inspect repository history, merged #444, WP55/WP57 acceptance evidence and local Docker metadata **only within separately authorized local inspection scope**.
- Identify the exact accepted UI/API source commits, image IDs, Dockerfiles, entrypoints, Linux/amd64 requirements and dependency pins. Resolve whether accepted local image bytes can be promoted as-is or reproducibly rebuilt; clearly distinguish image ID, registry digest, and runtime digest.
- Do not use root synthetic-preview UI or the older Azure research digest as a substitute. Do not include private candles, SQLite/WAL/witness, keys, secrets, local certs or fixtures in build contexts/images.
- Inspect `container/release_1_15/research-chart.Dockerfile`, `container/release_1_15/research-api.Dockerfile`, and `container/release_1_15/research-bootstrap.sh`. UI listens on 8501; API on 52731. Do not change runtime design unless a specific defect is established.

### 2. Prepare minimal image publication — separate approval required to execute
- Produce exact commands for building/publishing the **accepted** source-only Linux/amd64 UI and API artifacts to the **existing** approved registry; require no new Azure resources or paid tier.
- Before publishing, validate image contents exclude private state and secrets. After publishing, record immutable OCI manifest digests and verify registry readback and platform compatibility. Never equate a local image ID to an OCI manifest digest.
- If registry credentials, target repository or build provenance cannot be established safely, STOP with one specific missing item. Never print tokens or entire environment/configuration.

### 3. Capture a reversible staging baseline — separate Azure read approval
- Read only exact two staging apps and relevant nonsecret image/startup/port/routing settings. Store a private rollback baseline preserving unrelated settings and redacting secret values; do not replace all app settings wholesale.
- Confirm resource identity, Linux F1 plan, HTTPS-only and expected app targets. Identify configuration required for UI-to-API HTTPS and server-side authentication without browser secret exposure.
- Document that rollback of image/settings **cannot restore ephemeral SQLite data**.

### 4. Promote API first — separate exact Azure write approval
- Set the accepted API image by **immutable digest**, preserving unrelated settings. Use the supported startup/port configuration for port 52731 and existing writable ephemeral overlay (UID1654). No new SSH daemon, public import endpoint, storage service or persistence layer.
- After promotion, perform bounded startup/HTTP checks and, if possible, establish the actual running image identity. Distinguish HTTP timeout/403, startup failure, and missing dataset. A `/health` response alone is **not** proof that BTC/ETH observations are present.
- If startup/serving fails, STOP before UI promotion and propose the smallest targeted diagnosis or a separately approved rollback. Do not repeat probes indefinitely.

### 5. Check SQLite state honestly; do not improvise data transfer
- With separately approved secure access, check whether both protected BTC/USD and ETH/USD datasets are available. Require authentic identity/provenance, 1,000 contiguous candles each, expected SMA20 warmup/calculation and unavailable-volume behavior.
- If state is missing, report `DATASET_NOT_READY` and **do not** upload raw observations through CLI args, app settings, logs, browser, public HTTP routes, Docker layers or unverified SCM/exec facilities.
- Prepare the **smallest secure one-time initialization procedure** using retained validated observations and existing ephemeral SQLite, but do not execute private-data operations until a supported same-container transport and lifecycle are demonstrated and explicitly authorized. No third service, provider calls, DB file migration or automatic recovery.
- This is a staging application deployment exercise; do not block an API-startup result merely because chart data is absent, but do not claim chart acceptance either.

### 6. Promote UI second — separate exact Azure write approval
- After API serving is established, promote the accepted Python image by immutable digest to the UI staging app; configure port 8501 and HTTPS API base URL with secrets remaining server-side.
- Check UI HTTP startup. If both authentic datasets are ready, run bounded real-browser BTC → ETH → BTC acceptance, 1,000 candles per instrument, SMA20 (19 null/981 calculated), unavailable volume, provenance disclosure and no JS/assets failures.
- If data is absent, demonstrate only truthful unavailable-state behavior; `PUBLIC_RUNTIME_VERIFIED` stays false.

### 7. Failure handling, evidence and PR discipline
- Stop on first material failure; do not retry, restart, roll back, clean up unknown resources, or broaden scope without explicit approval. If a rollback is separately authorized, change only confirmed-owned staging deltas; never assume database recovery.
- Report **configured image**, **running image evidence**, **HTTP serving**, **dataset readiness**, and **browser chart acceptance** as distinct statuses (PASS/FAIL/UNKNOWN/NOT_RUN).
- Keep a compact timestamped, sanitized execution ledger: approved operation, exact targets, command category, bounded duration, exit/result, rollback evidence and outstanding blockers. Do not publish secrets, candle payloads or sensitive configuration.
- Use a new small implementation/deployment PR if code or scripts must change; do not silently merge or mutate #445/#446. Documentation updates to existing PRs require explicit scope. Preserve UTF-8/LF and `git diff --check`; run targeted offline tests for any code change.

## Expected immediate Codex deliverable
Return a **deployment-ready checklist and exact first authorization request**, based on repository/local artifact evidence, showing:
1. The precise accepted UI/API image source and whether publication is required.
2. The existing registry and two target staging apps (without secrets).
3. The minimal API-first/UI-second command sequence with approval boundaries.
4. The rollback baseline plan and one-line stop conditions.
5. Which actions were actually performed vs only proposed.

**Success criterion:** both accepted implementations demonstrably run on Azure staging, and—only if authentic ephemeral SQLite state is initialized and verified—the same BTC/ETH browser chart is accepted. No new resources or architecture redesign.

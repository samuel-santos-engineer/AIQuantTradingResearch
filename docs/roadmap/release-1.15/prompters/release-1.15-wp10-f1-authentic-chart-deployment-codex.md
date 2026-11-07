# Codex execution prompt — Release 1.15 WP10: authentic BTC/ETH chart on existing Azure F1 staging

## Objective
Deliver the existing C# research service and Streamlit/Plotly UI on the **two existing Azure F1 staging App Service apps**, using real completed-minute BTC/USD and ETH/USD candles. Obtain actual browser acceptance. Prioritize implementation and execution, not another readiness-only PR.

## Baseline and authority
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Base `main` after merged PR #403, squash commit `894b075e65863c042e9c39167fdbd71612020c90`. PRs #401 and #402 are also merged.
- Use an isolated worktree; preserve every owner working-tree modification and untracked file. Never reset or clean the owner's checkout.
- Existing staging resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2.
- Existing UI app: `aiqr112wp05wus27f5eabb5`, URL `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`.
- Existing research app: `aiqr115researchstagingwus27f5eabb5`.
- Existing F1 plan only. **Do not create a VM, disk, IP, storage service, new App Service plan, or any paid infrastructure. Do not touch production.**
- WP09 introduced an explicit, exact-site, non-durable Azure F1 research storage exception. Preserve its restrictions and all existing WAL/witness/D5/single-writer, provenance, authentication and HTTPS checks. Do not spoof Azure identity or relax guards to make tests pass.
- Previously authorized acquisition: **at most two authentic provider requests total, one BTC/USD and one ETH/USD, up to 1,000 completed one-minute rows each, no retries, at most 16 account credits total**. This is a ceiling, not authorization to use unknown keys, perform account discovery calls, or exceed an unverified credit rate.
- No Azure deployment/mutation authorization has been given in this prompt. Read-only targeted staging inspection and local implementation/testing are allowed; prepare exact deployment steps and ask the owner for one narrow staging mutation approval before changing app configuration, pushing/publishing images, restarting apps, or running an Azure import. Do not interpret the goal as blanket approval for those actions.
- `PUBLIC_RUNTIME_VERIFIED=False` until actual staging browser acceptance is recorded.

## Implementation-first tasks

### 1. Prove F1 compatibility, using only the designated staging app
Read the merged WP09 `ResearchStorageBoundary`, its Linux smoke runner, and staging runbook. Identify the **minimum safe, read-only** way to inspect the actual research App Service container runtime (UID, `/proc/self/mountinfo`, overlay root, `/tmp` layout, configured container image/entrypoint, resource constraints). The research app was previously only an unconfigured shell: do not claim in-container verification from ARM metadata alone. If obtaining runtime evidence requires deployment/config changes, provide the exact minimal staging-only mutation request and stop for approval. Never print secrets or inspect unrelated Azure resources. If the real filesystem fails the exact WP09 guard, report the observed incompatibility and propose a narrow evidence-backed fix in a separate reviewable change; do not silently broaden the exception.

### 2. Finish secure authentic-data bootstrap
Implement a practical, reproducible **same-container** bootstrap for the existing C# initializer and research endpoint. The deployed image must not contain provider keys, HMAC secrets, candle exports or synthetic substitutes. No credentials or exports in Git, command arguments, logs, Docker layers or public routes. Maintain a single writer: initialize while the server is stopped, verify initialization, then start the research endpoint. Support a clean restart/replacement that has lost ephemeral state by explicitly reporting unavailable and requiring controlled reimport; do not silently auto-fetch or fabricate data.

Assess the secure delivery channel available for the two existing staging apps (for example, an operator-mediated private channel). Demonstrate its actual feasibility before claiming it works. Do not assume that SCM/Kudu file access is identical to the container's ephemeral filesystem or that an Azure Files mount qualifies under WP09. If an authenticated import endpoint would be needed, do not introduce a public credential/data ingestion surface without explicit design review and authorization. Keep all source observations private and auditable.

### 3. Acquire/import authentic completed-minute observations only when prerequisites exist
Check only explicitly authorized credential channels. Before any provider request, confirm the **account-specific credit charge** for both requests fits the 16-credit total and that the provider's returned volume is base-asset volume as required by the existing ingestion contract. If either fact or the private credential/export is unavailable, perform **zero** provider requests and report the exact missing input. Do not request account information via a billable API call. If prerequisites are present, execute at most one bounded call per instrument with no retry; validate timestamps, completed candles, UTC ordering, provenance, volume units, gap semantics and limits; retain private raw exports securely. Use the merged WP07 import wrapper and unchanged initializer contracts. Record request and credit counts accurately.

### 4. Package and prepare the two existing staging applications
Build and test the existing research and UI images from pinned repository sources. Prepare immutable image digests, exact target-specific configuration, private settings transfer, HTTPS/API-key handshake, safe rollout and rollback, with no new infrastructure. Respect Azure F1 capacity and idle/cold-start limitations; measure them rather than claiming they are solved. Avoid publishing images or mutating Azure without the explicit staging approval above. Ensure the UI never exposes the research API key, provider key, D5 keys or raw private export.

### 5. Execute real browser acceptance after explicit approval and prerequisites
Against the exact existing public staging URL, verify actual BTC/USD and ETH/USD completed candles and research-backed indicator results; instrument selection, Plotly zoom/pan/hover, gaps, warmup, stale/reconnect, API-key and TLS/hostname negatives, missing-state behavior, and realistic cold-start/session behavior. Confirm that the C# backend, not a local fixture or legacy Streamlit view, supplied the chart. Capture non-secret evidence with clear timestamps and pass/fail/not-run labels. HTTP 200 at `/` or `/_stcore/health` is not chart acceptance. Set `PUBLIC_RUNTIME_VERIFIED=True` only if the required actual browser acceptance passes.

## Engineering and review discipline
- Implement code, scripts and focused tests before producing more documentation. Avoid repeated readiness-only artifacts, redundant broad test matrices, or speculative hosting proposals.
- Retain WP09 exact-site guard and fail-closed defaults, including overlay-only ephemeral state, UID and identity requirements. No network SQLite, shared mounts, concurrency bypasses or storage-identity spoofing.
- Distinguish `PASS_LOCAL`, `VERIFIED_AZURE_STAGING`, `BLOCKED_INPUT`, and `NOT_RUN`; never turn one into another.
- Test on the actual available runtime; label Windows-only tests and local Docker simulations honestly. Verify PS5.1 compatibility and strict UTF-8/LF where applicable.
- Open **one focused implementation PR** for the bootstrap/deployment/acceptance tooling, leave it open and unmerged for owner review. Include concise changed-file inventory, commands, test results, actual acquisition count, staging mutation status, missing approvals and the next executable owner action.
- If blocked by secrets, credit/volume evidence, in-container runtime access or staging mutation approval, finish the nonblocked implementation and stop at the narrow boundary; ask for the specific missing item. Do not expand scope or silently declare Release 1.15 complete.

## Definition of done
A real user can open the existing Azure staging UI and interact with authentic BTC/USD and ETH/USD historical candles supplied by the existing authenticated C# research service, with verifiable browser evidence and no new Azure infrastructure. Until then, explicitly report `PUBLIC_RUNTIME_VERIFIED=False`.

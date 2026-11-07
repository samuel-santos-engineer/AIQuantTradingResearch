# Codex execution prompt — Release 1.15 WP15
## Deploy and validate HTTPS-first research integration on existing Azure F1 staging

You are working in `samuel-santos-engineer/AIQuantTradingResearch`. Start from current `origin/main`, which includes merged PR #406 at `d28cf8afce6d88430598d99fb39cf78d57d8d082`. Preserve all owner dirty/untracked files, branches and credentials. Use an isolated worktree if changes are necessary.

## Objective
Move from offline HTTPS integration to a functioning, independently observed Azure F1 staging HTTPS connection between the Streamlit UI and .NET 10 research API. Prioritize a real, authenticated API response and accurate missing-dataset UX, then identify the smallest safe route to authentic BTC/USD and ETH/USD chart acceptance. Do not revive WP11–WP13 interactive terminal/container-provenance qualification as a prerequisite for normal HTTPS service communication.

## Existing targets — do not create paid resources
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`.
- Existing F1 plan: `asp-aiq-r112-wp05-wus2-7f5eabb5` (West US 2).
- Streamlit UI app: `aiqr112wp05wus27f5eabb5`; public acceptance URL `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`.
- Research app: `aiqr115researchstagingwus27f5eabb5`.
- Research endpoint: authenticated HTTPS `POST /v1/research/snapshot`, with `GET /health` for process liveness only. Preserve existing API contracts; do not invent a replacement GET candles endpoint.
- Instruments: BTC/USD and ETH/USD, maximum 1,000 completed 1-minute candles each. Existing research indicator and integrity logic remains authoritative.

## Authorization boundary — important
The owner's latest instruction explicitly approves merging PR #406 and creating this prompt. **It does not by itself authorize Azure configuration changes, image publication, provider requests, dataset import, or staging probes.** Prepare exact deployment artifacts and a bounded approval request; do not perform any of those live actions until the owner separately authorizes the specific scope. Read-only local/repository inspection and offline tests are authorized. Do not infer authorization from previous one-time diagnostics.

## Work package
1. Inspect the actual merged implementation, current Dockerfiles, deployment scripts, staging runbooks, API key/TLS transport settings, UI opt-in configuration, SQLite boundary, and rollback mechanisms. Reuse them rather than adding another API or orchestration layer. Verify the current baseline; don't assume prior report details replace code review.
2. Prepare a **deployment-ready, minimal, reproducible procedure** for two existing staging apps. Identify exact source commit, Docker build contexts, immutable image digest verification, platform linux/amd64, required non-secret settings, HTTPS-only ingress, the research service's platform TLS-termination opt-in, existing F1 site-specific storage guards, and UI server-side HTTPS base URL/key handling. Ensure the API key is never in Git, console arguments, public logs, browser state or screenshots. Keep secret delivery in owner-controlled protected files or a previously approved secure channel; do not solicit or discover private secrets.
3. Define a bounded smoke sequence with explicit expected results: research `GET /health` returns 200/Alive (liveness only); unauthenticated snapshot POST returns 401; authenticated valid snapshot requests for both instruments return 503 `DATASET_NOT_READY` when SQLite is absent, or validated committed data when already initialized. Confirm UI distinguishes missing dataset, auth, timeout and transport failure without synthetic fallback. Observe HTTPS connectivity from the deployed Streamlit server, not just from an unrelated external client. Avoid exposing credentials in tests or diagnostic output.
4. Plan a safe order of operations: capture both app baselines first (images, settings, startup, state, HTTPS-only, logging and slots), identify rollback owner and recovery commands, verify resource IDs, stage research before UI, and restore precisely on failure. No new paid infrastructure, storage account, VM, app plan, or licensing/approval governance gate. Preserve research data integrity: missing SQLite must not auto-create or auto-seed; existing D5/HMAC, WAL, single-writer, UID/mount/site guard and fail-closed rules remain intact.
5. Identify the **actual private initialization path** needed after HTTPS smoke tests. Do not claim the existing API offers an import endpoint. Explicitly distinguish deployment, authenticated missing-data validation, authentic initialization, and full browser chart acceptance. If there is no secure viable way to initialize authentic data in the research app under the current constraints, report the specific technical blocker and propose the smallest concrete implementation change in a focused PR.
6. If code or script fixes are justified by direct evidence, implement them with focused offline tests, documentation and a reviewable PR. Otherwise avoid a documentation-only PR merely to repeat existing runbook content; produce a concise actionable deployment authorization checklist and report. Do not merge your PR.

## Data/provider boundaries
Previous user-approved data budget was at most **two Twelve Data requests total** (one per instrument), at most **1,000 completed 1-minute candles each**, no retries, and at most **16 credits total**. There have been no authorized live calls under this WP. Do not perform calls, infer credentials, or use fabricated/synthetic data as public acceptance. Provider account-specific limits and private input channel remain unverified. Do not add a licensing prerequisite.

## Evidence and completion
- Run focused .NET and Python tests offline, lint/format checks and Docker/static validations where available. Report exact commands and results; never represent offline tests as live staging acceptance.
- Protect owner checkout, credentials and preexisting app settings. No live Azure operation without separate explicit approval.
- If a PR is created, provide its URL, head SHA, scope, test results, unresolved blockers and exact approval request.
- Provide a concrete next-step decision: (A) ready for separately authorized staged image publication/deployment and HTTPS smoke, or (B) precise technical blocker and minimal code change required.
- Keep flags truthful: `AZURE_DEPLOYMENT_NOT_RUN`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED=False` until independently observed otherwise.

**Primary success criterion:** a safe, implementable path to the real BTC/USD and ETH/USD historical candlestick chart through the existing Streamlit ↔ .NET HTTPS/JSON connection, not another diagnostic-attestation exercise.

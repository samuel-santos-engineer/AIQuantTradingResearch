# Codex Assignment — Release 1.15 WP09: Existing Azure F1 Non-Durable Research Runtime

## Authority and objective

Baseline: `main` after merged PR #402, squash commit `d209f0ae5fb144ed3165719ebd3a400cb326e3ad`. Deliver a **focused implementation PR** enabling the existing .NET C# research service and SQLite WAL research store to operate in an **explicit, non-durable staging-only mode** on the existing Azure App Service F1 research app, supporting the authentic BTC/USD and ETH/USD chart. This is an intentional narrow change to the Release 1.15 storage restriction; do not disguise App Service identity, disable integrity protections, or weaken defaults. Open a PR for owner review; **do not merge it**.

Existing staging targets only:
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`
- Existing F1 plan: `asp-aiq-r112-wp05-wus2-7f5eabb5`
- Streamlit UI app: `aiqr112wp05wus27f5eabb5`
- Research app: `aiqr115researchstagingwus27f5eabb5`
- Public UI: `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`

No VM, managed disk, paid infrastructure, production deployment, new provider or second research writer. Keep owner's dirty/untracked files intact; use an isolated worktree. PR #402's read-only diagnostics are already merged.

## Required implementation

1. Inspect the actual `ResearchStorageBoundary`, `ResearchWriterOwnership`, research initialization, configuration, Docker images and relevant tests. Identify the smallest code change that supports a named staging-only non-durable mode. Do not assume a storage path or F1 mount behavior without checking.
2. Keep existing default **fail-closed** App Service rejection. Allow opt-in only when all of the following hold: an explicit dedicated staging flag/scope is present; Azure App Service identity matches the **exact designated research app**; execution is on Linux with the existing expected non-root identity; and the resolved database directory is inside a verified writable, private **ephemeral container-local** filesystem. Reject network-backed mounts (including Azure Files/CIFS/NFS), persistent/shared paths, symlinks and unexpected mounts. Do not use an `AIQ_RESEARCH_STORAGE_SCOPE=local-qualification` value or removal of `WEBSITE_*` variables to bypass the guard. Fail closed when filesystem identity cannot be established. Document any limits of mountinfo-based checks and enforce them as well as feasible.
3. Preserve SQLite WAL, single-process writer ownership, database/witness verification, D5 HMAC integrity, server-side API-key auth and HTTPS boundaries, source provenance, UTC completed-minute semantics, indicators, and explicit unavailable/stale behavior. Do not create an automatic second writer or silently seed fixture candles. Avoid introducing unsupported persistence claims. A restart/instance replacement may lose data; service must fail safely and support controlled reinitialization from an authorized authentic export.
4. Add focused automated tests covering default App Service denial; exact authorized staging identity and explicit opt-in; wrong site, missing markers, production-like identity, wrong scope, unsafe mount types, shared/persistent paths, symlinks, wrong UID, missing/invalid mount evidence, and startup without valid database. Include a bounded local/container smoke test if the existing environment permits it. Do not label local tests as Azure verification.
5. Make the **minimum necessary governance and operational documentation amendment** to authorize this exact non-durable staging exception, explaining loss-of-state/reimport, no production extension, and unchanged durable-storage policy. Update the existing staging runbook to distinguish verified versus not-run evidence. Avoid a new planning-only work package.
6. Provide an executable, tightly scoped deployment and rollback sequence for the **existing** F1 research app and UI, with protected settings and immutable image references; do not publish secrets, fake data, or fabricate image digests. Check the practical constraints of two apps on the shared F1 plan, including startup/idle/cold-start, but do not claim success without real tests.

## Authentic data and browser acceptance: next execution path

The prior acquisition authority remains: at most **two provider requests total**, one for BTC/USD and one for ETH/USD, at most **16 account credits total**, no retries, up to 1,000 completed 1-minute candles each. **Do not call the provider** until a designated secure credential channel or authentic export, verified account-specific credit rate, and base-asset-volume semantics are available. No secrets retrieval from unrelated Azure settings, private-file hunts, or speculative discovery calls. If unavailable, report `BLOCKED_INPUT` and preserve zero usage. Keep private data out of Git.

After the implementation PR is approved and merged, separately obtain explicit authorization before Azure configuration changes, image publication, deployment or restarts. Then import authentic observations, configure authenticated research HTTPS endpoint, deploy to existing staging apps, and run actual browser acceptance for both instruments, candle provenance, indicators, zoom/pan/hover, gaps/warmup, stale/reconnect, API-key/TLS negatives, and observed F1 cold-start behavior. No browser acceptance claim based on HTTP 200 alone. Until proven: `PUBLIC_RUNTIME_VERIFIED=False`.

## Scope, quality, and reporting

- No Azure mutation in this PR; read-only staging inspection only when useful and safely bounded. No paid resource creation.
- Keep changes small and reviewable; avoid unrelated refactors, broad dependencies or governance expansion.
- Run targeted tests, formatting/diff checks and relevant container checks; report exact commands, pass/fail and skipped work.
- Preserve all owner changes using isolated worktree and verify their hashes/status unchanged.
- Open a single focused PR, leave OPEN/UNMERGED, with changed files, rationale, negative safety cases, rollback instructions, unresolved assumptions, and explicit `PASS_LOCAL`, `VERIFIED_AZURE_STAGING` (only if actually tested), `BLOCKED_INPUT`, `NOT_RUN` statuses.
- Do not ask repeated architectural questions already settled: use existing F1 resources; no new VM; non-durable staging is acceptable; production remains protected.

## Definition of done for this PR

The implementation and tests show that the designated Azure F1 staging research app **can opt into a bounded non-durable local SQLite writer mode** while every other App Service environment remains rejected by default; safeguards remain intact; a clear staging deployment/rollback procedure exists; and a reviewable PR is open. **This is not equivalent to completed Azure deployment or browser acceptance**, which require subsequent explicit operational authority and authentic data prerequisites.

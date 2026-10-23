# Codex — Release 1.15 WP02 dual Docker implementation preflight and scoped PR

## Owner authority and precedence
Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Owner approved the dual-container architecture and Azure App Service API-key authentication. Issue #368 is the current work-package scope. PR #381 infrastructure evidence is merged; PR #382 governance amendments are pending reconciliation/merge. Do not treat unmerged governance as effective without resolving it; preserve its intended owner-approved D1-B direction. Keep `PUBLIC_RUNTIME_VERIFIED=False`.

**Objective:** prepare and, where repository authority permits, implement a narrowly scoped WP02 foundation for two independent Linux/amd64 Docker images, each with one non-root principal process, and a secure HTTPS API-key service boundary. Open a review PR; do not merge, deploy or activate any service.

## Mandatory repository preflight
1. Read root `AGENTS.md`, LF policy, current issue #368, WP01 accepted contracts, Release 1.15 execution plan, file manifest, PR #381 merged evidence and PR #382 changes/status. Verify main HEAD and open branches/PRs.
2. Inventory and preserve all tracked/untracked local work, especially pending WP02 plans/prompts/evidence. Do not reset, clean, stash, overwrite or cherry-pick them silently. Use an isolated worktree if necessary.
3. Resolve PR #382's merge conflict **without losing either** PR #381's Azure evidence/manifest entries or PR #382's dual-container amendments. Prefer a narrow, reviewable reconciliation on PR #382; if a safe resolution cannot be made with available tools, report it and stop before source implementation. Do not force-push, rewrite history or merge without owner authorization.
4. Confirm whether the approved governance is merged. If it is not, do only planning and exact-path preflight; do not begin source implementation on assumptions from an unmerged PR.
5. Inspect actual repository layout and tests. Produce an explicit exact-path mutation list and run-only regression paths before edits. Do not treat manifest directories as wildcard mutation authority.

## Target design
- **Research image:** one non-root C# process; sole owner of provider credentials, real OHLCV, SQLite/WAL, watermark, checkpoints, HMAC transformation key and read-only transformed snapshot API. No Streamlit process or custom native C/PID1 supervisor.
- **UI image:** one non-root Streamlit/Plotly process; only transformed snapshots and presentation state. No private DB, provider key, HMAC key or research filesystem. No Bash dual-process supervisor.
- Each image has a minimal process entrypoint, graceful SIGTERM handling, separate Docker build/digest provenance and health strategy. Build Linux/amd64 locally. Do not assume a local image proves Azure F1 readiness.
- Keep accepted D3 numerical semantics, D4 bounded full/unchanged/error snapshot schema and D5 transform/epoch semantics. WP02 must not silently implement or change WP03–WP06 algorithms/schema ownership.

## API-key security contract
- Two independent cryptographically random keys, one per environment, stored as secret Azure App Service application settings at **both** corresponding Web Apps: client (UI) and verifier (research). **Do not create, read, rotate or display actual Azure secret values in this task.** Use dummy keys for offline tests only. This is separate from Twelve Data and HMAC secrets.
- Streamlit server sends `X-API-Key` over HTTPS to the configured research service URL. Validate TLS certificate and hostname. Never send key to browser/Plotly, URL, query string, logs, image layers or GitHub.
- Research authenticates before transformed snapshot/status access. Missing/incorrect/revoked keys -> non-disclosing 401; use constant-time comparison with length-safe handling. Only bounded read-only transformed operations; no raw prices, admin, ingestion control or filesystem exposure.
- Design short, bounded active+previous key overlap for rotation, including expiry and rollback; avoid indefinite fallback. Do not invent a new secret store, paid Azure resource, VNet or private endpoint.
- The research Web App has a public DNS name; an API key is access control, **not** private networking. Provide negative direct-request tests. A minimal unauthenticated liveness route may exist only if required by platform health and must disclose no private status.
- API-key enforcement and HTTPS are necessary; actual Azure ingress controls and F1 compatibility remain WP07 acceptance obligations. Preserve UI last-known/disconnected semantics on failures.

## WP02 implementation and evidence boundaries
1. Implement only approved exact-path Docker/runtime foundation and isolated security fixtures; coordinate any D4 client/server production API code with WP05 #371. Do not claim WP05 is complete from WP02 fixtures.
2. Use local deterministic two-container tests, offline self-signed or locally trusted test TLS as appropriate, dummy keys, no live Twelve Data calls, no actual Azure keys, no paid infrastructure.
3. Test missing/wrong/rotated API keys; plaintext prohibition; direct anonymous access denial; UI environment/image isolation; absence of raw/provider/HMAC secrets in responses and logs; independent container startup/stop/failure; health; memory/CPU screening; compatibility of image pair.
4. Preserve research SQLite single-writer exclusion across overlapping deployments as WP03/WP07 work, not a false claim proven by one container per app.
5. Record exact image build inputs, source commits, image digests if built, reproducible commands, fixture/test outcomes, limitations and failures. Never fabricate tests or Azure runtime observations.
6. Keep production and staging original UI Web Apps and newly provisioned research shells unchanged. No `az webapp config appsettings set`, no image push, no deployment, no credentials, no provider activation.

## Publication
Track this executed prompt under `docs/roadmap/release-1.15/prompters/` and index it in `RELEASE_1.15_FILE_MANIFEST.md` only if authorized exact paths and conflict-free publication are established. Open a scoped implementation/planning PR with exact changed files and owner review; do not merge. Verify UTF-8 without BOM, LF/final newline, `git diff --check`, required test results and no secret values in changes.

## Return
Report: governance/PR #382 reconciliation state; exact-path scope; what was implemented versus deferred; tests and observed resource limits; API-key/TLS threat-model evidence; PR link/head; Azure non-mutation; unresolved WP03/WP05/WP07 obligations; `PUBLIC_RUNTIME_VERIFIED=False`.

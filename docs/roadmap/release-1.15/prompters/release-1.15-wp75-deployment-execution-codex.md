# Release 1.15 — WP75: Deploy the accepted BTC/ETH research application

## Mission

**Get the actual API + Streamlit BTC/USD and ETH/USD historical candlestick UI deployed and working in Azure staging.** This is an internal greenfield lab: prioritize the shortest reliable deployment path, not additional grammar, schema compatibility, governance, licensing, or speculative diagnostics. SHA256 is the sole supported digest algorithm. Start from current `main` including merged PR #470 (`35a77d663d454624b96219d4ca1a10617eaeac6f`). Work in an isolated clean branch; preserve unrelated work, including open PR #457. Produce a focused PR for owner review, leave it open and unmerged.

## Existing accepted facts; do not infer more

- Accepted WP61 UI local image ID: `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`; Linux/amd64, UID/GID 12002, Streamlit port 8501. **This local ID is not a proven published manifest/index digest.**
- GHCR repository: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch`.
- Accepted API Linux/amd64 child digest: `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`. Do not substitute the diagnostic child `sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c`.
- Azure subscription `7f5eabb5-80f5-4369-88be-b14a6de763f1`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2, F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5`.
- Azure API app `aiqr115researchstagingwus27f5eabb5` (port 52731); Azure UI app `aiqr112wp05wus27f5eabb5` (port 8501).
- Previously configured Azure UI digest `sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07` is historical and **not proven equivalent to WP61**.
- Keep Docker-first with permitted dual-process local development, API-first/UI-second Azure deployment, authentication/TLS/storage/key contracts and existing ports.
- Current flags: `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`. OCI provenance UNVERIFIED; publication necessity NOT PROVEN. Do not promote flags without direct browser/runtime acceptance.

## Execution strategy: progress to deployment, not another research loop

1. **Offline deployment inventory, short and decisive.** Read the merged deployment scripts, Dockerfiles, image build/publish workflows, Azure configuration instructions and relevant Release 1.15 evidence. Determine exactly which deployable artifacts/commands are ready, which immutable reference is missing, and the shortest concrete path to a running UI. Do not invent repository/registry evidence. Do not spend time extending the RepoDigest classifier or repeating its synthetic tests except as regression checks.
2. **Prepare a runnable, minimal deployment workflow.** Implement or correct the smallest cross-platform (Linux-first) scripts/configuration needed for: (a) the accepted UI image to have a verifiable published `ghcr.io/...@sha256:<64 lowercase hex>` reference, using an existing reference if verified, otherwise an explicit publish step; (b) configuring the accepted API image first; (c) configuring the UI second with the correct API endpoint, ports, secrets/configuration contracts; (d) bounded HTTPS health and authenticated browser acceptance. Avoid Windows-only deployment helpers and extra dependencies where possible. Do not confuse local image ID, config digest, manifest digest or index digest.
3. **Operational authorization boundary.** This prompt authorizes offline repository work, source changes, synthetic/local unit tests, and creating an open PR. **It does not authorize executing Docker daemon commands, GHCR/registry queries or pushes, Azure reads/writes, credential inspection, public probes, or actual deployment.** Prepare exact commands and expected effects as a concise owner-executable deployment runbook; clearly isolate each proposed live action with a specific approval request. Do not silently run live commands. If a required credential or permission is missing, identify only its name/role, never value, and continue implementing everything that does not depend on it. Do not create new governance or licensing approval gates.
4. **Tests and evidence.** Run targeted offline tests and static checks for changed scripts; default must be safe/PLAN_ONLY or dry-run. Validate command ordering, immutable SHA256 reference format, no secret printing, preservation of existing app settings, fail-closed behavior, and that public verification flags remain false until actual acceptance. If no runnable deployment is possible in this work package because live authorization is absent, state precisely what is ready and provide a short ordered set of concrete commands/actions for the next authorized execution, rather than substituting more diagnostics.
5. **Deliverables.** A focused PR (open, unmerged), deployment script/config updates as needed, a short Release 1.15 WP75 evidence appendix, and a practical operator runbook specifying exact API-first/UI-second sequence, rollback strategy, required approvals, and BTC/ETH browser acceptance criteria. Report test counts actually executed, paths changed, outstanding deployment blockers, and the **single next action** needed to move the real deployment forward.

## Definition of done

- No new RepoDigest grammar/category work; SHA256 only.
- Clear and executable path from accepted local UI image to immutable GHCR reference and Azure staging API/UI configuration.
- Existing secrets, TLS, storage, keys, port and authentication contracts preserved; no destructive Azure action or resource recreation by default.
- BTC/USD and ETH/USD chart acceptance explicitly specified, including authenticated access and real candles; never claim acceptance before it is actually performed.
- No fake readiness: `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false` until verified in the deployed runtime.
- PR remains open and unmerged for owner review.

**Priority: finish deployment. If a decision is needed, choose the simplest implementation consistent with the existing runtime contracts and identify only the concrete live operation requiring separate approval.**

# Codex continuation — Release 1.15 WP02 PR #384 disposition and next-phase preflight

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`
**Owner instruction:** PR #384's fixture-only foundation has been reviewed and approved. Continue to the next bounded step; do not construe approval as production integration, Azure deployment, secret creation, provider activation, or complete WP02 acceptance.
**Known PR #384 head:** `eeae40a2af7c6c75a4e457f53cde1d34abe9dce1`
**WP02 issue:** #368; `PUBLIC_RUNTIME_VERIFIED=False`.

## 1. Verify authority and protect local work

1. Inspect `AGENTS.md`, current `main`, governance, WP01 D1-B amendments, Release 1.15 roadmap, WP02 issue #368, merged PRs #381–#383, and the complete PR #384 patch, checks, reviews and head SHA.
2. Verify that PR #384 is still open, that its head is the approved commit (or document/review any newer commits), that its diff is confined to the eleven approved fixture implementation paths plus three documentation updates, and that no failing required checks or unresolved blockers exist. Do not equate absence of checks with successful CI.
3. Protect the owner's dirty/untracked WP02 files. Do not reset, clean, stash, delete or overwrite them. Prefer a separate worktree or GitHub-native operations.
4. If all preconditions pass, **merge PR #384 using the repository's standard method** and record the exact resulting merge/squash SHA. The owner's approval in this conversation authorizes this reviewed fixture-only PR; it does not authorize additional implementation or deployment. If any material difference or blocker appears, STOP before merge and report it.
5. Verify `main` includes the merged files, and WP02 #368 remains open. Never set `PUBLIC_RUNTIME_VERIFIED=True` based on fixture evidence.

## 2. Define the next bounded work package (planning only)

Create a **documentation-only, exact-path next-phase proposal** for closing the remaining WP02 fixture-level security qualification gaps and handing production integration to WP05. This is a **preflight/authority request**, not implementation authorization.

Review the actual code and existing qualification evidence before proposing any edits. Focus on:

- **Absolute end-to-end client deadline:** demonstrate/plan timeout coverage for DNS/connect, TLS handshake, request write, headers, slow-drip body and cancellation/cleanup. The current watchdog starts after `connection.request()`; do not claim earlier blocking stages are covered. Define deterministic fault-injection tests without real external network/provider access.
- **Key rotation and process restart:** define an explicit wall-clock absolute expiration contract and monotonic safeguards; verify a research restart or deployment rollback cannot extend the previous key's authorized expiry. The fixture's 300-second overlap and Base64-encoded 32-byte key are **candidates**, not approved WP05 production values. Test missing/wrong/duplicate/revoked/expired keys and absence of secret leakage.
- **Trust boundary:** HTTPS-only server-side `X-API-Key`, environment-specific secrets in Azure App Service application settings, separate production/staging values, no browser exposure, no raw/private/admin endpoints, no plaintext/redirect fallback. API keys authenticate requests but do **not** make the research hostname private. Azure ingress/TLS termination and actual F1 availability remain WP07 work.
- **Fixture vs production:** do not turn the research fixture into a production D4 server, fabricate snapshots, alter D3/D4/D5 contracts, or expand into WP03 SQLite/storage, WP04 calculations, WP06 UI product, or WP07 Azure deployment.
- **Qualification reproducibility:** preserve Linux/amd64 image provenance, pinned assets, local-only non-root Docker isolation, PowerShell 5.1 compatibility, deterministic negative tests, cleanup ownership and truthful distinction between local test evidence and CI/Azure/browser verification.

Produce a compact decision matrix: issue, observed code/evidence, proposed solution, exact candidate file paths, owning WP, test proof, and remaining risk. Separate WP02-owned fixture hardening from WP05-owned production transport semantics and WP07-owned Azure verification. Avoid inventing new approval/licensing gates or paid infrastructure.

## 3. Documentation PR boundaries

If existing Release 1.15 roadmap docs are the appropriate destination, propose **only exact existing documentation paths** such as:

- `docs/roadmap/release-1.15/RELEASE_1.15_WP02_DUAL_DOCKER_PREFLIGHT.md`
- `docs/roadmap/release-1.15/RELEASE_1.15_WP02_DUAL_DOCKER_PREFLIGHT_EVIDENCE.md`
- `docs/roadmap/release-1.15/RELEASE_1.15_FILE_MANIFEST.md`

If a new next-phase planning document or executed-prompt archive is needed, name its **exact path** and justify it before writing. Do not modify source/test/Docker/runner files in this step. Keep historical evidence append-only and avoid rewriting previously observed results as newly executed tests. Ensure UTF-8 without BOM, LF, terminal newline, valid relative links, and `git diff --check`.

Open a **new docs-only PR** for owner review; do not merge it. Link #368 and merged #384, identify any proposed subsequent source/test paths precisely, and clearly state that subsequent implementation needs separate owner authorization. Do not close WP02.

## 4. Explicit prohibitions

No Azure CLI writes, App Service settings/secret operations, actual API key creation or retrieval, image push, deployment, provider calls, paid services, production endpoint activation, broad dependency upgrades, release promotion, automatic merge of the new PR, or mutation of pending local WP02 work. Do not claim runtime qualification or public availability. `PUBLIC_RUNTIME_VERIFIED=False` throughout.

## 5. Return report

Return: PR #384 merge outcome and exact SHA; the new planning PR URL/head SHA and changed paths; current WP02 issue state; the precise next source/test paths proposed but **not yet authorized**; test evidence verified versus only reported; remaining WP05/WP07 handoffs and blockers. If #384 could not be safely merged, stop and explain rather than proceeding on a false merged baseline.

# Codex execution prompt — Release 1.15 WP07 Azure staging readiness and qualification

## Authority and starting point

Work on `samuel-santos-engineer/AIQuantTradingResearch` from current `origin/main`, which includes squash-merged PR #395 at `71e4f8ccce877547d11c965ded92280fb02e65de`. Read the canonical Release 1.15 governance, WP07 issue #373, WP02 #368, WP03 #369, WP06 #372, and the append-only qualification evidence before editing. PR #395 establishes **PASS_LOCAL_ONLY** for the actual Linux/amd64 C# research and Streamlit UI pair. Do not repeat that as an Azure PASS. `PUBLIC_RUNTIME_VERIFIED=False` until actual staging verification.

**Authorization boundary:** This prompt authorizes preparation, inspection, local/offline checks, staging readiness documentation, and a PR for review. It does **not** authorize Azure resource mutation, credentials acquisition, ACR/image push, remote container deployment, App Service configuration changes, provider activation, production activity, or public exposure. If live Azure staging operations are required, stop at a precise, executable operator checklist and identify the minimum explicit authorization needed. Do not infer permission from this prompt. Do not add new licensing or policy approval gates; the remaining boundary is deployment authorization and factual runtime prerequisites.

## Working method

1. Use a new isolated git worktree/branch from the merged main commit; never modify the owner's original dirty/untracked checkout. Inventory and hash all owner files before/after; preserve byte-for-byte.
2. Inspect actual source/config and current Azure definitions, governance, Dockerfiles, local qualification runner, and issue contracts. Do not assume unmerged PR #386/#387 is authoritative; do not import their changes silently.
3. Confirm the staging topology: two distinct non-root, single-principal-process Linux/amd64 images, Docker-first; UI's server-side HTTPS to research staging DNS with validated CA/hostname and `X-API-Key`; separate D5 secret; no browser access to research private API, raw OHLCV, service key, HMAC key, SQLite, provider credentials, or TLS private key. No fallback to a same-container or unencrypted design.
4. Respect known staging context (verify, do not blindly create): UI App Service `aiqr112wp05wus27f5eabb5`, research App Service `aiqr115researchstagingwus27f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, shared Linux F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5`, region West US 2. Never assume the shared F1 plan supports simultaneous always-on containers or that storage semantics are adequate; assess and record facts.
5. Preserve existing pinned dependencies and framework versions; no opportunistic upgrades, refactors, provider ingestion, schema changes, or broad governance rewrites.

## Deliverables in priority order

### A. Staging preflight, without mutations

- Inspect the repository's existing Azure infrastructure, workflows, deployment scripts, runtime Dockerfiles and configuration contracts. Enumerate exact images, ports, startup commands, environment variable names, certificate/CA mounting strategy, HTTPS endpoint hostname, storage mounts, D5 provisioning/recovery, API-key delivery, and logs needed by the two App Services.
- Check whether two independently deployed containers can actually run concurrently under the current **Linux F1** App Service plan, whether the research endpoint is reachable server-to-server from UI without becoming a public raw-data interface, and whether required private SQLite volume permissions/durability and startup budgets can be met. If cloud inspection requires unavailable credentials, report **UNVERIFIED**, not PASS.
- Resolve operational gaps with the smallest local-only changes that can be independently tested. Keep credentials out of git, logs, image layers, browser traffic, screenshots, and PR descriptions. Avoid copying private fixture material into image contexts.
- Produce a concise staging matrix: requirement, existing implementation, evidence source, result PASS_LOCAL / BLOCKED / NOT RUN / FAIL, exact remediation and operator action. Separate local evidence from actual Azure results.

### B. Reproducible staging packaging and deployment plan

- Prepare a minimal, reviewable operator runbook or dry-run-capable script for the *existing* two-image deployment and validation sequence: build provenance, registry digest capture, service image assignment, secret/CA configuration, D5 initialization, volume permissions, health/readiness, private API accessibility, UI rendering, and rollback.
- Default every script to non-mutating dry-run. Any Azure mutation path must require an explicit flag **and** remain unexecuted during this task. Do not create a public research endpoint that bypasses API-key authentication. Treat network restrictions and F1 limitations as technical findings rather than silently changing the architecture or subscription tier.
- Distinguish local Docker image IDs from pushed registry RepoDigests. Do not claim the latter unless verified.
- Include explicit stop conditions for inadequate F1 capacity, unavailable durable storage, certificate mismatch, secrets leakage, unqualified image, or provider dependency. No fake success or substitution of images/credentials.

### C. Local evidence and tests

- Run the relevant offline/Windows/local Linux regression checks where the environment allows, including the real two-container qualification from #395 if reproducible. Do not assume the Docker daemon or images exist. Record commands, tool versions, exit codes, timings, and resource measurements.
- Add narrowly targeted tests for any new staging preflight/script behavior; do not disturb the known legacy `Volume` versus `Simulated activity` hover assertion merely to produce a green count.
- Where possible, verify graceful restart, abrupt restart at quiescent state, witness identity, no competing writer, HTTPS CA/hostname/key rejection, and no private data in browser/network/logs. Interrupted-write/power-loss durability and Azure mount behavior remain NOT RUN unless actually executed.
- Preserve `PUBLIC_RUNTIME_VERIFIED=False` unless an explicitly authorized real Azure staging run actually succeeds; this prompt does not authorize one.

## PR and reporting contract

- Create **one focused new PR**, open and unmerged, based on merged #395. Do not close WP issues #368, #369, #372, #373, or #374 automatically.
- Propose an exact changed-path manifest before editing and keep the final diff tightly scoped to the smallest staging-readiness artifacts and tests. No arbitrary broad edits.
- Update the existing Release 1.15 evidence document append-only, with command transcripts summarized, relevant SHA-256 hashes, environment/prerequisites, objective pass/fail/block reasons, security boundaries, and remaining operator actions. Do not publish private paths or secrets.
- Include a **specific owner decision request only if needed to perform live Azure deployment**: what resource(s) would change, expected F1 limitations/cost implications, rollback, and exactly which operation is withheld. No speculative approval gates.
- Final response: PR URL/head SHA; exact files; executed vs unexecuted checks; concrete Azure/F1/storage blockers; minimal next action; owner checkout preservation; `PUBLIC_RUNTIME_VERIFIED=False`.

## Acceptance semantics

Success for this assignment is a truthful, reviewable staging-readiness PR and executable qualification procedure, **not** an Azure runtime acceptance claim. If Azure deployment remains unauthorized or infeasible, document the blocker and stop cleanly. Do not merge your own PR.

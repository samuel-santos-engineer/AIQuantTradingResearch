# Codex continuation — Release 1.15 WP05 transformed research API integration (staging only)

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Date: 2026-10-24
Target work package: WP05 #371, with WP02 #368 fixture handoff.

## Owner decisions (binding)

1. The existing **standard synchronous Python HTTPS timeout / native DNS limitation is accepted for this lab**. Do not add DNS worker threads, subprocess resolvers, cancellation frameworks, supervisors, or new qualification gates. Keep a modest configurable timeout, generic error handling, last-known valid snapshot preservation, and documentation of the limitation.
2. **One active service API key per environment**. No previous-key grace window, automatic rotation, expiry witness, rotation persistence or configuration rollback apparatus. A later key replacement may use a coordinated settings change/restart and a planned interruption. Keep production and staging keys distinct; never expose keys to browser, Git, images, logs or error messages.
3. **Two separate single-process non-root Linux/amd64 Docker images**: research C# and Streamlit UI, deployed as separate Azure Web Apps. Docker-first and D1-B governance remain effective. HTTPS plus `X-API-Key` protects the research endpoint; its public DNS name is not private networking.
4. **PRODUCTION IS STRICTLY OUT OF SCOPE.** Do not inspect, change, deploy, restart, configure, publish, query, or run validation against the production Web Apps, production resource group, production secrets, production slots, production URLs or production container registry tags. No production promotion, even for a successful staging result.
5. **Staging is the only permitted Azure target for eventual milestone publication/assessment**, and staging deployment is a distinct explicitly authorized step after local integration, owner review and credentials/preflight. This prompt alone does **not** authorize Azure writes, registry push, image deployment, App Service settings/secret access or live provider calls. Offline deterministic tests and local Docker runs are permitted within separately approved exact paths.
6. Do not reintroduce provider-licensing approval gates. Preserve D3/D4/D5 semantics, raw-private-data separation, historical BTC/USD and ETH/USD only, and `PUBLIC_RUNTIME_VERIFIED=False` until actual qualification and owner acceptance.

## 0. Verify current repository state and preserve owner work

- Read `AGENTS.md`, `.gitattributes`, governance (`DOCKER_FIRST_LINUX_RELEASE_POLICY.md`, `HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md`, `SYNTHETIC_PUBLIC_PRODUCT_SCOPE.md`), Release 1.15 execution plan, manifest, WP01 decisions and WP02 evidence. Read issues #368 (WP02), #369 (WP03), #370 (WP04), #371 (WP05), #372 (WP06), #373 (WP07), and applicable PRs.
- Confirm merged #385 at `4578f8645c9626f4daa7ec76923953e8892c93d6`. Inspect **PR #386#386** at expected head `1829b05cd627f53d556758d797d191af06807263` and current state. If still open, do not silently merge it: the owner reported it as open, and this prompt is not implicit merge approval. If it has since merged, use the verified merge commit as the WP02 fixture baseline. WP05 may be planned while #386 is pending, but source integration must not assume an unmerged fixture is on `main`.
- Inventory exact production/research/UI code paths, language boundaries, D4/D5 schema/fixtures and dependency versions. Confirm whether WP03 and WP04 implementation and acceptance evidence actually exist; **do not invent a completed backend**. If predecessors are missing, prepare a truthful integration plan and mock/contract tests only, without fabricating production readiness.
- Preserve every dirty/untracked owner file byte-for-byte. Prefer an isolated worktree/branch. Do not reset, clean, stash, overwrite, or mass-reformat the owner's working tree.

## 1. WP05 functional target (not a fixture masquerading as production)

Design the **real** authenticated read-only C# research snapshot endpoint and corresponding **server-side** Python/Streamlit HTTPS client for the existing D4 bounded transformed-snapshot contract, integrating with genuine WP03 committed state and WP04 calculated state when available.

- Only **BTC/USD** and **ETH/USD**. Research-side reads private data and transforms public-facing OHLC and price-valued indicators under existing D5 HMAC/UTC-day contract; never send raw OHLCV, provider key, HMAC secret, SQLite, checkpoints, admin/debug endpoints or research-only metadata to UI.
- Preserve D4 full/unchanged semantics, schema version, request epoch, generation/version/UTC display date, independent health/freshness/classification validity, bounded retained candles (up to 1,000), aligned indicators, transitions and validated gap/unavailable intervals. Do not invent completed candles or synthetic production data. Conditional `unchanged` only for exact matching accepted identity; otherwise full.
- C# authenticates all non-liveness routes using one active staging/test API key, constant-time verification where applicable, fail-closed invalid/missing/multiple headers, generic unauthorized responses, bounded request/response size, no sensitive logging. Research endpoint must be HTTPS for UI traffic; distinguish TLS termination at Azure ingress from local fixture TLS without assuming a private network.
- Python client validates HTTPS origin, TLS chain/hostname, rejects redirects/plaintext fallback, uses server-side key only, bounded synchronous timeouts (accepted native DNS limitation), strict whole-response validation and atomic state acceptance. On error, preserve the last complete valid snapshot with truthful disconnected status. No browser-side API key or direct research call.
- Do not modify WP03 storage writer/recovery, WP04 algorithms, WP06 browser UX, or WP07 Azure deployment without separately approved scope. Never copy fixture's deliberate HTTP 503 D4 stub into a production endpoint.

## 2. First deliverable: exact-path WP05 preflight and owner-review PR

**Planning and evidence only at this step.** Before editing production source, enumerate **literal existing/new paths** required for C# endpoint, Python client, D4/D5 cross-language fixture tests, minimal Docker integration, and documentation. No directory wildcard is authority. Identify path owners and concurrent PR conflicts. Propose the smallest end-to-end vertical slice, with no dependency upgrades unless essential and separately justified.

Create a compact decision matrix with: integration component, source-of-truth/predecessor, exact proposed path(s), expected behavior, deterministic offline tests, WP owner, risk/blocker. Include:

1. Baseline and predecessor readiness of WP03/WP04 and current D4/D5 contracts.
2. Single-key authentication, TLS trust/hostnames, staging environment isolation, denial cases.
3. Full/unchanged compatibility, malformed/oversized/identity-mismatch rejection, UTC rollover, no raw-data leakage, negative fixture corpus and C#→Python cross-language tests.
4. Independent Docker image startup, readiness, resource budget and one-process/non-root checks.
5. Separate local test evidence vs actual staging deployment/browser/F1 evidence.
6. **Staging-only publishing proposal** for a later separately authorized WP07 action, including paired image digests, staging app settings/URLs, reversible rollout and stop conditions; no actual Azure operation in this planning PR.

Prefer updating existing Release 1.15 WP05 planning/evidence/manifest paths discovered in repository. Name any new doc/prompt archive exact path **before writing** and justify it. If existing WP05 documents are not present, propose precise new paths. Do not assume a WP05 implementation path without repository inspection.

Open a **docs-only PR** for owner review and leave it **OPEN/UNMERGED**. Do not implement source changes until the owner approves the exact WP05 file paths and the predecessor baseline is confirmed. No broad issue closure, merge, staging deployment, or production change is inferred.

## 3. Subsequent implementation after explicit source-path approval

- Create a separate scoped WP05 implementation PR. Run deterministic offline unit and C#↔Python contract tests, malformed/unauthorized/secret-leakage negatives, local paired Docker smoke tests if dependencies are available. Record actual commands, hashes, versions, failures, limits and cleanup; never claim CI, staging, real-browser or Azure verification from local results.
- Avoid scope creep: no DNS cancellation redesign, key rotation, extra paid services, provider activation, fabricated history or unnecessary abstraction. Reuse the simple WP02 boundary where appropriate, while replacing the fixture-only status/503 path with genuine D4 integration.
- Leave the implementation PR open for owner review. A merged implementation PR still does not authorize staging publishing or WP07 acceptance.

## 4. Staging-only deployment boundary (future separate authorization)

**Permitted staging targets for a later explicitly approved deployment:**

- Staging UI: `aiqr112wp05wus27f5eabb5`
- Staging research: `aiqr115researchstagingwus27f5eabb5`
- Staging resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`
- Staging shared Linux F1 plan: `asp-aiq-r112-wp05-wus2-7f5eabb5`
- Azure region: West US 2

These identifiers are **planning locators only**. Do not run Azure CLI, modify settings, access staging secrets, push images, or deploy until separately authorized. Do not use production targets as fallbacks. Staging qualification will later need ingress/TLS, authentication denials, browser behavior, F1 resource pressure and paired-image rollback evidence. A successful staging assessment does not imply production release approval.

## 5. Documentation and report discipline

- All changed text UTF-8 without BOM, LF, terminal newline; independent CRLF scan, link checks and `git diff --check`. Preserve prior evidence as historical; append dated supersession notes rather than rewriting test history.
- Do not treat #386's reported **14 client + 10 paired tests** as CI or as newly rerun tests. Report whether #386 was merged and the exact baseline used.
- Return: verified branch/PR status, predecessor readiness, exact proposed WP05 paths, docs-only PR URL/head, deterministic test plan, blockers (if any), clear staging-only future rollout plan, and `PUBLIC_RUNTIME_VERIFIED=False`.

**Primary instruction:** Move toward real WP05 feature integration with minimal lab-appropriate engineering. Keep production completely untouched and reserve all future publishing/assessment for staging only.

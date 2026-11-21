# Codex execution prompt — Release 1.15 WP58 Phase B

## Objective
Qualify **existing Azure staging serving health and immutable container artifact identity**, using the smallest bounded read-only operations. Do not deploy, mutate Azure, or assume that `Running` metadata proves public readiness. Continue the existing Release 1.15 BTC/USD + ETH/USD historical research chart work; preserve the successful WP57 local acceptance.

## Known baseline (verify before acting)
- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- PR #444 and PR #443 merged, in that order. WP57 local browser BTC → ETH → BTC passed with authentic imported data, 1,000 candles per instrument, SMA20 (981 non-null), no volume bars, and unavailable-volume disclosure.
- PR #445 is OPEN/UNMERGED; head `e5481e0eb2c8664d3ed307587c9382d95af2db7f`. It contains WP58 documentation, including five separately approved ARM metadata GETs (two staging sites, two `config/web`, one shared plan). Both sites: Linux, Running/enabled, Normal usage/availability, HTTPS-only; classic image mode, empty startup override. Shared plan: Linux F1, West US 2.
- `AUTHENTIC_DATA_NOT_IMPORTED=False` **for the local imported data**; `LOCAL_CHART_VERIFIED=True` (WP57 historical acceptance); `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
- Do not infer that cloud dataset state, current HTTP health, quota headroom, registry digests, or authentication are verified.

## Nonnegotiable scope and authority
1. **Phase B planning and offline repository inspection are authorized by this prompt.** Do not treat this prompt as authorization to perform Azure, registry, public URL, DNS, network, browser, credential, private-data, Docker or other live operational calls. Before each live category, present an exact bounded plan and obtain fresh, explicit owner approval. If approval is absent, stop after documenting the proposed operation.
2. Staging only; **production excluded**, including production reads and probes. No app/plan creation, deployment, image build/push/pull, app settings changes, restarts, scale changes, cold-start loops, provider requests, acquisition/import, raw observation/SQLite upload, migration, or local runtime changes.
3. No secret/key/presence/equality inspection, private files/DB, raw historical values, screenshots, full HTTP bodies, response headers containing credentials, Azure settings collections, registry credentials, access tokens, or unsanitized CLI output. Use in-memory allowlisted projections and publish only sanitized status/identity metadata.
4. Preserve existing local data, images, volume, certificates, owner checkout, untracked files, and old preparation manifests. No new provider or licensing approval gates; Twelve Data remains sole upstream. No fabricated zero volume or synthetic fallback.
5. Use a clean isolated worktree/branch if edits are needed; do not merge PR #445 or any other PR without explicit instruction.

## Target staging identities (cross-check against repository authority before any live operation)
- Subscription: `7f5eabb5-80f5-4369-88be-b14a6de763f1`.
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`.
- Staging UI: `aiqr112wp05wus27f5eabb5`; public host `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`.
- Staging research API: `aiqr115researchstagingwus27f5eabb5`; public host `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/`.
- Shared plan: `asp-aiq-r112-wp05-wus2-7f5eabb5` (Linux F1, West US 2).
- Existing architecture: separate single-process UI and C# research API; HTTPS and server-side service authentication. No local-machine tunnel or paid-tier workaround.

## Phase B0 — offline-only inspection and precise approval request
1. Read `docs/roadmap/release-1.15/RELEASE_1.15_WP58_PUBLIC_RUNTIME_QUALIFICATION_PLAN.md`, execution plan, and applicable staging deployment helper/tests. Inspect existing repository scripts for exact read-only commands, guardrails, API routes and previous F1 quota failure. Confirm source/branch and existing PR #445 changes without overwriting them.
2. Determine whether `GET /health` is the supported **anonymous** endpoint for the research site. Do not assume the UI has a `/health` route; establish its supported safe GET path from source. Avoid calling authenticated `POST /v1/research/snapshot` in this phase.
3. Establish whether Azure `config/web` exposes only a mutable image reference and whether immutable OCI manifest digest resolution would require registry API access. Distinguish **configured tag/image reference**, **registry manifest digest**, and **local Docker image ID**. Do not report a tag as a verified digest.
4. Prepare two **separate explicit approval requests**, each with exact target, command/API type, allowed projected fields, maximum requests, timeout, retries, expected side effects, failure-stop rules, and no-credential policy:
   - **B1 serving:** at most one bounded anonymous HTTPS GET per exact staging site, to source-confirmed safe paths, e.g. research `/health` and UI `/` if supported; max 2 requests, 15 s each, no retries/redirects, no cache-busting or extra assets, no browser. Record only status class/code, timing, TLS validation outcome, content-type and a tightly constrained health/status classification if safe. If an HTTP response suggests quota exhaustion, startup failure, authentication redirect, non-healthy status, unexpected hostname, or malformed content, stop. Note that even a successful GET does not prove stable availability or quota headroom.
   - **B2 artifact identity:** bounded allowlisted metadata read(s) to identify current **configured** image references for the two exact staging apps, and (only if independently authorized and safely possible) registry **read-only manifest** resolution for those exact references. Project image registry/repository and digest identity without leaking embedded credentials, config secrets or raw JSON. Do not enumerate registry resources, use credential-list APIs, log in, pull, push, or inspect unrelated repositories. If immutable digest cannot be resolved without credentials or additional permission, record `UNKNOWN` and stop.
5. The owner may approve B1 only, B2 only, both, or neither. Do not conflate this prompt with the approvals. If the exact scope cannot be safely specified, report blocker instead of probing.

## Phase B1/B2 — execute only after respective fresh approvals
- Reconfirm exact staging identities and approved commands; use existing authenticated tooling only if needed and separately authorized. No interactive login or credentials discovery.
- Enforce the request count and timeout caps, with no retries, redirect following, warm-up loops, or unapproved requests. TLS verification must remain enabled.
- Sanitize results **before** storing in files or emitting console text. Never record raw HTTP bodies or configuration objects. For failed commands, report error category only, not raw error bodies.
- If any identity mismatch, unsafe redirect, unexpected resource, quota/staging health concern, unauthorized endpoint, or missing precondition occurs, **stop immediately**. Do not attempt corrective writes or restart.
- Record separate outcomes: `B1_ANONYMOUS_SERVING` and `B2_CONFIGURED_ARTIFACT_IDENTITY` / `B2_IMMUTABLE_OCI_DIGEST`. Explicitly distinguish `PASS`, `FAIL`, `UNKNOWN`, and `NOT_RUN`.

## Cloud dataset and authentication boundary
Do **not** check protected snapshots or keys in Phase B. If B1/B2 pass, propose a separately approved **Phase C** to verify that the research API has preexisting authorized initialized BTC/ETH state and service-authenticated transformed snapshots, with no acquisition/import/upload/migration. If cloud state is absent or unverified, mark deployment blocked under current constraints; never substitute synthetic data or copy the local database. Do not claim `PUBLIC_RUNTIME_VERIFIED=True` until actual authorized public browser and API acceptance passes.

## Evidence, PR and completion report
- Prefer appending a clearly labeled Phase B section to the existing WP58 qualification plan and execution plan **on PR #445's branch** only if safe to do so without overwriting concurrent changes; otherwise use a fresh documentation PR referencing #445. Preserve historical statements as historical, never retroactively claim earlier phases had authorization.
- Include source SHA, approvals and their exact scope, timestamps, projected outcomes, request counts, explicit NOT_RUNs, limitations and blockers. LF/UTF-8/no BOM and `git diff --check`; run focused offline checks only if relevant changes require them. No unsupported CI/full-suite claims.
- Keep PR OPEN/UNMERGED for owner review. Never silently modify production or existing staging configuration.
- Final status must include `AUTHENTIC_DATA_NOT_IMPORTED=False` (local), `LOCAL_CHART_VERIFIED=True` (WP57 historical), `PUBLIC_RUNTIME_VERIFIED=False` unless fully proven by a later separate scope, and `liveAttemptReady=false` until all prerequisites are established.
- End with the **smallest concrete next owner decision**, not a blanket deployment request.

## Success definition
A successful WP58 Phase B either (a) establishes bounded, approved, sanitized evidence of actual staging anonymous serving and exact configured/immutable artifact identities, with limitations, or (b) identifies the precise unresolved blocker without modifying Azure. **It is not a public chart acceptance or deployment milestone.**

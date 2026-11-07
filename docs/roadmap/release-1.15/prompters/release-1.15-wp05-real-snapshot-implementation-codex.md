# Codex continuation — Release 1.15 WP05 real research snapshot integration

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Date: 2026-10-24

## Binding owner decisions

Implement **working WP05**, not another planning-only PR. Prefer the smallest correct implementation, no speculative infrastructure. The owner approved and merged WP03 PR #389 (`bf4e744330986eac8234cda7a73ce866aa092242`) and WP04 PR #390 (`2ad60bf5c0c670d3f4934b5ee7100df7bf05234f`). WP05 planning PR #387 and WP02 fixture hardening PR #386 were last known OPEN/UNMERGED: verify live GitHub state, do not silently merge or adopt their unmerged branches. The owner approved WP05 planning, not automatic merges of #386/#387. Reconcile #387's exact-path proposal with merged WP03/WP04 source, especially immutable committed-state reader and fixed-12 string calculated outputs.

**Production is strictly off limits, including read-only inspection.** No Azure, provider calls, real credentials, registry pushes, deployment, App Service settings, or staging publication under this task. Staging only in a separately authorized future WP07 rollout. `PUBLIC_RUNTIME_VERIFIED=False`.

## Protect owner work and prompts

Before edits, inventory original checkout branch/HEAD, full dirty/untracked status, byte lengths and SHA-256 for **every** owner file, especially every untracked Codex `.md` prompt. Never stash, reset, clean, delete, overwrite or reformat originals. Use an isolated worktree based on current verified `main`. Preserve all original hashes/status afterward. Include all untracked prompts in the final report and their existing archive/pending dispositions. The untracked WP03 simple and WP04 simple prompts were previously pending separate archive-path approval; do **not** silently overwrite same-name archives or omit them. This task does not itself grant new archive-path edits. If a new WP05 prompt is found untracked, inventory it too.

## Scope and exact proposed implementation paths

Read `AGENTS.md`, `.gitattributes`, D1-B and historical chart governance, D3/D4/D5 contracts, Release 1.15 plan, #387 preflight and merged WP03/WP04 code/tests. Confirm actual files, types and schema before writing. **The following are the 14 literal paths proposed in approved WP05 planning #387, not a blanket permission to modify anything else.** Use only these source/test paths and minimal existing Release 1.15 WP05 evidence/manifest paths if explicitly authorized in the current Codex environment. If approval tooling requires exact-path grants, request approval for these literal paths; do not bypass denials. If one planned path cannot integrate correctly, identify the precise replacement path and ask for a narrow amendment rather than editing unlisted code.

1. `src/AIQuantTradingResearch.Application/MarketData/ResearchSnapshotContracts.cs` (new)
2. `src/AIQuantTradingResearch.Application/MarketData/ResearchSnapshotTransformer.cs` (new)
3. `src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs` (new)
4. `src/AIQuantTradingResearch.Worker/ResearchSnapshotSource.cs` (new)
5. `src/AIQuantTradingResearch.Worker/Program.cs` (existing; minimal composition only)
6. `python/presentation/research_snapshot_client.py` (new)
7. `python/presentation/test_research_snapshot_client.py` (new)
8. `tests/AIQuantTradingResearch.Application.Tests/ResearchSnapshotTransformerTests.cs` (new)
9. `tests/AIQuantTradingResearch.Infrastructure.Tests/ResearchSnapshotEndpointTests.cs` (new)
10. `tests/AIQuantTradingResearch.Infrastructure.Tests/PythonIntegrationFixtures/research_snapshot_reference.py` (new)
11. `tests/runtime/release_1_15/research_snapshot_contract_vectors.json` (new)
12. `container/release_1_15/research-api.Dockerfile` (new)
13. `container/release_1_15/snapshot-client.Dockerfile` (new; test-only driver, NOT the final Streamlit image)
14. `eng/docker/release-1.15/Invoke-Wp05SnapshotQualification.ps1` (new)

Do not modify WP03 writer/recovery, WP04 algorithm, schema, dependencies, existing WP02 fixture, WP06 UI, or WP07 Azure infrastructure under this task. If minimal runtime integration cannot fit these paths, document the exact blocker and seek narrow approval. No new project, package, hosted service or key-rotation framework.

## Functional acceptance target

Build a genuine **research-side** read-only `POST /v1/research/snapshot` backed by merged WP03/WP04 private committed state. Only BTC/USD and ETH/USD, completed one-minute bars, up to 1,000 retained per instrument. Research must be truthful about empty, not-yet-ready, gaps, unavailable intervals, freshness, integrity and recovery. Do not synthesize provider history, return fixture 503 as a working endpoint, or expose raw candles/checkpoints/SQLite/secret-bearing metadata to UI.

Preserve frozen D4 contract: schema `1.0` emission, compatible `1.m` handling per governance; algorithm IDs `aiq-r115-research-v1` and `aiq-r115-transform-v1`; `intervalSeconds=60`; exact bounded full response collections `candles`, `analytics`, `classifications`, `events`, `researchUnavailableIntervals`, `marketDataGapIntervals`; `unchanged` only when the complete request identity/generation/version/display UTC date/transformEpoch matches, and without arrays/null substitutes. Apply strict input/output bounds, JSON shape/number/string/depth validation and no unexpected fields. Consult actual D4 document rather than inventing payload keys.

Preserve frozen D5: **private** 32-byte HMAC-SHA256 key and private keyEpoch UUID; message `algorithm + LF + displayDate + LF + privateKeyEpoch + LF`; derive daily factor exactly as specified in governance; integer 10^12 scale and ties-to-even. Apply transform to price-valued OHLC and indicator values **after** private research calculation, using exact fixed-12 strings/integer arithmetic, never float/double. Volume, RSI and dimensionless metrics retain defined untransformed semantics. One factor shared across BTC/ETH for the same UTC day. Public transformEpoch is distinct from private keyEpoch and from service API-key identity. Make durable configuration/epoch restart behavior explicit and fail closed if not supported by currently authorized paths: do not fake persistence or readiness. Never log/return secrets or raw prices.

C# service-to-service authentication: one active environment-specific `X-API-Key`, constant-time comparison, fail-closed missing/duplicate/invalid keys, generic errors, no browser key. HTTPS to research from Python server-side, valid certificate and hostname, no redirects or HTTP fallback. Python client validates entire response before atomic acceptance, preserves last good snapshot on failures and reports disconnected truthfully. Use modest synchronous timeout with accepted native DNS limitation; **no** DNS workers/subprocesses or key rotation/grace windows. Two independent non-root single-process Linux/amd64 images remain eventual deployment architecture. Do not misrepresent Azure public DNS as private networking.

## Keep the milestone simple

1. First implement D4/D5 pure contract/transform logic with deterministic C#↔Python reference vectors and negative cases. Use independent derivations, not fixtures generated only by production code.
2. Connect real immutable WP03/WP04 reader to endpoint; authenticate, transform and serialize bounded full/unchanged responses. Reconcile exact fixed-12 strings and real generation/version/segments/events. Do not duplicate storage or introduce a second writer.
3. Add Python HTTPS client and strict validation; run offline tests including malformed, oversized, unauthorized, UTC rollover, identity mismatch, no raw-data leakage, and preserved last-good snapshot.
4. Where practical, run local paired Docker qualification using test-only credentials/fixtures and report actual evidence. If Docker unavailable, state this explicitly; do not make local Docker a prerequisite for a truthful source implementation.
5. If durable D5 key/config activation, runtime wiring or Linux mode cannot be completed within authorized paths, fail closed and report precise follow-up scope. Do not declare public runtime verified or invent passing end-to-end behavior.

No live Twelve Data call or real secret. No new licensing/approval gates, distributed locks, Azure provisioning or unrelated refactors. Keep prior numerical/store regressions passing. Record exact commands, pass/fail counts, build warnings, hashes and limitations. Distinguish local tests from CI/staging/browser proof.

## PR and report

Open one scoped **WP05 implementation PR**, leave **OPEN/UNMERGED** for owner review. Include only explicitly approved files, append-only evidence if authorized, exact change list, current #386/#387 disposition, owner-file hash preservation, prompt archive inventory, UTF-8 no BOM/LF/final newline, independent CRLF scan, link checks and `git diff --check`. Do not close issues or claim WP05 acceptance. No production or staging operations. `PUBLIC_RUNTIME_VERIFIED=False`.

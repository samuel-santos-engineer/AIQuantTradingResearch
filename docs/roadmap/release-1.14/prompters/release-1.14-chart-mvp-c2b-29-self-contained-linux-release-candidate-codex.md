# C2B-29 — Self-contained Linux Docker release-candidate implementation

**Authority:** Owner approved C2B-29 for bounded **local implementation and qualification only**. **Not authorized:** merge, registry push, Azure mutation, deployment, public activation, production data export/promotion, or relaxation of existing governance gates.

## Goal

Produce a reviewable, self-contained **Linux/amd64 Docker image candidate** that runs the existing Streamlit/Plotly synthetic BTC-like chart using a finite, verified, immutable C#-computed OHLCV + SMA(20) handoff, with no live C# process, runtime download, external market-data provider, or Windows runtime dependency. Qualify the **actual intended image** locally under Docker-first governance. Do not equate local qualification with Azure F1 readiness.

## Baselines and dependencies

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Merged governance PR #348: `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md` (main baseline previously observed `612483e570d4b8e1f99d512681ca26b1bc1320cd`; recheck current main).
- PR #346: documentation/evidence, open, last known head `0e568d208f2769037bf4a7265ea4ba6f4609aacc`.
- PR #347: immutable handoff producer/reader/chart candidate, open, last known head `8240d0878dea335c08ad3216600fc7e545446140`.
- PR #349: C2B-28 route/packaging assessment, open, last known head `c40ba4e1ba751b5097061d95d76ce81bbd019c0e`.
- Check these refs again before work. PR #347 is an **unmerged dependency**: explicitly record how its reviewed changes are incorporated into the new implementation branch (e.g., cherry-pick with provenance), avoid silently merging PRs or duplicating code. Do not modify #346/#347/#349. Create a **new focused implementation PR**, open and unmerged.
- Preserve all owner documents, untracked work, protected WP07/archive, current public gates and the accepted local Linux browser evidence.

## Hard boundaries and stop conditions

1. **No release authorization.** `PUBLIC_RUNTIME_VERIFIED=False`; public data APIs remain `503 DATASET_NOT_READY`; advanced WP07 remains **ON HOLD**; Azure deployment and public activation **NO-GO**. Do not flip readiness to display the chart publicly or repurpose provider/private paths.
2. **Gate check first.** Map the proposed local image packaging, offline synthetic-data generation, provenance and test exposure against applicable G-B0–G-B7, WP07 and export/promotion rules. Owner approves bounded local candidate generation/build/testing **only**, not release export, publication or promotion. If a gate prohibits even this bounded candidate, **STOP** and report the exact blocker; do not bypass it.
3. No credentials, private replay, external Twelve Data/provider calls, real-market historical data, arbitrary URLs/paths, additional Azure resources, paid plans, live C# worker, new runtime service or unnecessary packages.
4. Do not loosen Azure/Development/environment protections or use `env -i` stripping, localhost spoofing, or alternate route exposure as a workaround. Separate **candidate test eligibility** from **public release readiness**; defaults must fail closed.
5. No Windows-native certification requirement. All acceptance evidence must be from the **locally executed intended Linux Docker image**; test browser may be on host against narrowly controlled local-only container ingress.

## Implementation scope

### A. Finite offline immutable handoff

- Reuse the verified C# generator and SMA(20) computation, seed `114`, synthetic dataset `aiq-synthetic-ohlcv-v1`, UTC `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, intervals `1h/4h/1d`, windows `1/7/30/90 days`, 250 hidden warmup rows, only visible OHLCV and precomputed SMA20 serialized.
- Existing C2B-25 prototype inventory: manifest + 12 selection members, total `656176` bytes; known manifest SHA-256 `007b2c8a6ccec19ec54976b4baf735812b648d79294e6465df884c68537d3069`. **Verify independently** against reviewed generator, E4/oracle evidence, manifest/member hashes and provenance; do not blindly trust this description. If any byte changes, document why and stop before certifying unless explicitly dispositioned.
- Generate only in a bounded **local, offline Linux** staging step with explicit reviewed input allowlist. Do not commit generated market-data assets to Git or include arbitrary workspace/untracked files in build context.
- Make the exact finite handoff available to the final image at one fixed, non-public, read-only path, with a versioned manifest and trusted anchor. Prefer simple Docker build stages/explicit owned staging context, no new service or external artifact store.

### B. Runtime layout and startup

- Keep one Streamlit/Plotly public process on port `8501`, preserve existing UI and API boundary. C# is build-time/offline only, not a request-time process.
- Fix the C2B-28 observed `--read-only` startup exit `64` caused by `rmdir /runtime`: prepare necessary paths during build and use minimal bounded tmpfs for unavoidable writable state. Avoid broad runtime root cleanup, writable application code or writable immutable assets. Retain least privilege/non-root runtime, bounded CPU/memory/PIDs and existing sanitized environment protections.
- Adapt the reader's ownership/trust model to image-bundled **root-owned, non-writable, non-symlink** files and ancestors without weakening complete-set hash, schema, bounds, timestamp-grid, selection and SMA validation. Preserve coherent reads and fail-closed error behavior for missing, modified, unreadable or unexpected members.
- Do **not** enable public data APIs or public chart readiness. If the current public mode is deliberately unavailable, demonstrate the chart through a clearly isolated **local-only candidate qualification mode** that cannot activate in Azure or production; document exact gates and prove they reject Azure markers. Never fake public readiness.
- Use the existing dependency set where practical. Pin/reconcile base image identities and resolved build inputs; no silently floating dependency or replacement image in a purported certified digest.

### C. Route contract and security boundaries

- On the actual candidate image verify: `/` returns expected HTML, `/_stcore/health` expected text, `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` each return `503` JSON `DATASET_NOT_READY`, and nonexistent `/api/v1/...` returns `404` JSON `NOT_FOUND`. No API route may return the Streamlit SPA HTML fallback.
- Unknown asset/local source paths must not reveal manifest, member files, code, private content or filesystem paths. Admin/private routes remain unavailable as previously specified. Preserve `Cache-Control` and error contracts where applicable.
- Azure's current `200 HTML` API mismatch is **observed**, but root cause is unproven. Do not modify the correct local API contract to match Azure. No Azure writes or new probes requiring elevated authority.

## Local Docker-first qualification and acceptance

Run against the **final intended image**, not a temporary overlay or earlier image. Record image identity (platform manifest digest, OCI config/layers/index if present), exact source/dependency/manifest provenance, command lines, Docker version, Linux architecture, observed limits and failure evidence.

1. **Reproducibility:** fresh controlled build context; validate finite artifact inventory, bytes, hashes and 12 selection boundaries; independently map E4/SMA evidence; document any non-reproducible inputs.
2. **Startup:** default public startup and local-only candidate qualification; `--read-only` root, non-root UID, correct root-owned asset permissions, approved writable tmpfs only; startup/shutdown/restart/health; no C# worker; no unplanned network or host-exposed ports.
3. **Functional/negative tests:** all 12 selections, visible row counts and hidden warmup exclusion; 12 semantic mutations, five filesystem faults and other relevant integrity/ownership/link attacks; fail closed without partial data or provider fallback.
4. **Browser:** real browser against **this image**, 1D (24 rows) and 30D (720 rows), SMA20 off → on → off, exact displayed SMA-to-immutable-byte comparison, interval/range selectors, volume, native Technical Analysis popover, hover, toolbar, zoom and pan. Existing owner-accepted zoom reset on SMA toggle remains documented.
5. **API/security:** six-route matrix above, asset exclusion, private/admin denial, public gate rejection even when local qualification succeeds.
6. **Resources:** bounded cold start, CPU, peak RSS, 1 CPU/512 MiB/64 PID screening, concurrent readers/session behavior, cancellation and memory retention; distinguish exploratory measurements from actual Azure F1 capacity guarantees. Stop on OOM, missing tests or unknown effective runtime state.
7. **Cleanup:** remove only owned containers, temporary images, volumes, relays and scratch contexts; do not prune shared images or modify protected working trees.

If Docker or browser testing is unavailable, report **INCOMPLETE** with exact blocker. Do not substitute Windows-native evidence or declare release certification.

## GitHub deliverables

- One **new focused implementation PR**, open/unmerged, with minimal Dockerfile/entrypoint/reader/chart/test changes required for the candidate; include a compact design/contract document and a sanitized evidence report under `docs/roadmap/release-1.14/prompters/`.
- In PR body include: base/main and dependency SHAs; exact changed-file inventory; source-to-PR #347 provenance; gate disposition; Docker build/test commands; artifact manifest and hashes; final image identity/digests; test matrix/results; resource readings; route/browser screenshots or references; negative cases; unresolved issues and a separate future digest-promotion plan.
- Preserve #346/#347/#349 as-is. No merge, registry push, Azure mutation, image promotion or deployment. No change to public readiness flag or held WP07 state.

## Completion report

Return a succinct **PASS / PARTIAL / BLOCKED** for each acceptance category, the new PR URL/head SHA, exact image platform digest (if produced), and a clear **GO/NO-GO for owner review of a future registry-promotion authority only**. Never claim Azure readiness, deployment authorization, public activation or F1 certification from local evidence.

# C2B-28 — Azure F1 Image Packaging and Route-Contract Reconciliation

**Repository:** `samuel-santos-engineer/AIQuantTradingResearch`  
**Release:** 1.14 Chart MVP  
**Authority type:** Bounded Codex investigation, design and local Linux Docker qualification only  
**Disposition:** Owner-approved planning/local verification; **NOT** authorization to merge, promote an artifact, publish a registry image, modify Azure, deploy or activate public data routes.

## 1. Context and immutable boundaries

- Governance PR #348 was merged: Docker-first Linux qualification is the authoritative release standard. Windows-native runtime compatibility or Windows acceptance is **not** required.
- PR #346 remains open/unmerged at `0e568d208f2769037bf4a7265ea4ba6f4609aacc`, containing the preserved owner documents and C2B-26/C2B-27 qualification evidence.
- PR #347 remains open/unmerged at `8240d0878dea335c08ad3216600fc7e545446140`, containing the offline C# synthetic OHLCV/SMA producer, strict immutable reader and gated Streamlit/Plotly integration.
- Local Linux Docker browser acceptance is complete: 1D/30D SMA off → on → off; selectors, hover, zoom, pan, toolbar; 24/720 displayed SMA values matched the immutable C# artifacts.
- Local data API contract remains `503 DATASET_NOT_READY`. Azure has shown **200 HTML instead of the expected 503 JSON** on an API route; exact site, path, response provenance, digest and time must be re-established before concluding why.
- The immutable synthetic dataset is not yet authorized for production artifact promotion. `PUBLIC_RUNTIME_VERIFIED=False`; Azure deployment and public activation **NO-GO**; advanced WP07 **ON HOLD**. Preserve G-B0–G-B7, WP07 and other held gates.
- Existing Azure F1 Linux sites: production West Central US `https://aiqr112wp035ec325382770.azurewebsites.net/` and staging West US 2 `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`. Do not assume their current deployed images or settings remain identical without read-only verification.

## 2. Objective

Produce a reviewable, evidence-backed **single-image Linux F1 release-candidate design** and isolate the Azure API route-contract discrepancy. Keep the implementation minimal: existing Streamlit/Plotly UX, no live C# service in the request path, deterministic C#-generated immutable synthetic data, one deployable image and digest. **No deployment** in this task.

## 3. Required work

### A. Establish exact baseline and provenance

1. Confirm branch heads, merged Docker-first policy, PR relationships and clean working tree. Do not rewrite or merge PRs #346/#347.
2. Inspect `Dockerfile`, `container/entrypoint.sh`, public Streamlit entrypoint, routing and API handlers, ignore files, immutable producer/reader, relevant governance and prior evidence.
3. Record current image build context, user/permissions, startup command, exposed port, `env -i` behavior, public-vs-development gates, routing stack and where immutable assets would be read.
4. Obtain **read-only** Azure App Service and deployed digest/config evidence if access is available. If not, state precisely which facts are unverified. No Azure mutations.

### B. Reconcile the `200 HTML` versus `503 JSON` API contract

1. Define a small route matrix for `/`, `/_stcore/health`, `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` and a nonexistent API path. Record HTTP method, status, content type, response body signature and relevant headers.
2. Run the matrix against the **current local Linux Docker public-mode image**, without relaxing runtime gates or exposing the container publicly. Record image ID/digest and command line.
3. Where authorized read-only probes work, run the same matrix against **each Azure site**, with bounded timeouts, no secrets and no aggressive retries. Distinguish redirects, Streamlit SPA fallback, reverse-proxy behavior, container route handling, cached HTML and genuine JSON API responses. Do not infer that an HTML 200 proves the data API is healthy.
4. Determine whether the mismatch is explained by routing, an old image, entrypoint, mode, proxy or another evidenced cause. If unproven, document ranked hypotheses and the next non-mutating diagnostic. Do not silently change a public contract to make a probe pass.
5. Treat any unintentional exposure of synthetic/private/provider data as a **stop condition**.

### C. Design a self-contained immutable-data image

1. Specify the **smallest** deterministic, versioned, checksummed packaging path from the verified offline C# generator to a read-only path inside the final Linux image. The 12 approved selections and verified SMA values must remain exact; never expose warmup-only rows.
2. Explicitly separate (i) permission to build/test a **local candidate image** using approved synthetic inputs from (ii) owner authorization for public artifact export/promotion, registry publication and deployment. If existing governance forbids even local candidate packaging, stop and request owner disposition.
3. Specify build-stage trust, reproducibility, manifest/hash validation, no credentials or provider/private replay, no generated files accidentally committed, minimal runtime dependencies, fixed path, non-root/read-only filesystem behavior and fail-closed startup/read errors.
4. Preserve existing Streamlit chart controls and TA popover; do not add a live C# runtime worker, separate web service, new cloud resources or a Windows compatibility layer.
5. Define the **exact image digest handoff**: build → local Linux tests/browser → record immutable digest → separately authorized registry push of identical image → separately authorized staging deployment → per-site verification → owner-approved production deployment/rollback. No rebuild between certification and promotion.

### D. Bounded local qualification, only if governance allows

- Prefer **read-only analysis and a local throwaway image**. No registry push, Azure operation, public ingress, production artifact promotion or permanent image tag.
- If local image packaging is authorized under existing gates, exercise startup, port 8501, health, route matrix, 12 selections, SHA-256 and semantic mutation rejection, missing/corrupt bundle fail-closed behavior, filesystem ownership and permissions, no request-time C# worker, bounded CPU/RAM/PIDs and concurrency.
- Browser verification should target the actual Linux container image (browser may run on a host through controlled local-only ingress); verify 1D/30D SMA off → on → off and existing Plotly interactions. Prior C2B-27 browser acceptance can be reused **only when image contents and relevant execution path are demonstrably unchanged**.
- Record resource observations as local screening, **not** Azure F1 certification. If Docker or safe ingress is unavailable, record blockers; do not substitute Windows-native evidence.
- Clean up owned containers, temporary images, networks and test data; leave unrelated resources intact.

### E. Azure staging/production readiness and rollback

For **each** site, record current observed digest, container startup and port, configuration assumptions, API response matrix, readiness checks, expected immutable data location, rollback digest and remaining governance approvals. Keep staging/production roles consistent with repository README, but confirm live state rather than assuming it. Explicitly identify what requires a future owner-authorized staging deployment before any claim of `PUBLIC_RUNTIME_VERIFIED=True`.

## 4. Deliverables and PR discipline

- Create **one new, focused documentation/evidence PR** for C2B-28 against `main`, leaving it **OPEN/UNMERGED** for owner review. Prefer files under `docs/roadmap/release-1.14/prompters/` for the assessment and reproducible route/packaging evidence. Do not add C2B-28 changes to PR #346 or #347 unless the owner explicitly directs otherwise.
- Deliver an evidence matrix with source commit, Docker image identity, commands, results, hashes, route statuses/content types, site-specific observations, exact files changed, known unknowns and cleanup proof.
- Produce a clear **GO/NO-GO decision for the next bounded implementation milestone**, *not* an Azure deployment GO. Separate facts, hypotheses, owner decisions and gates.
- Do not edit or regenerate the 14 preserved owner documents, protected WP07/archive, prior qualification records or unrelated release material.
- If a code change is essential to demonstrate a route or packaging correction, **do not implement it in this documentation-only authority**. Specify a minimal follow-on PR with tests and request owner approval.

## 5. Mandatory stop conditions

Stop and report rather than bypass if: governance prevents local candidate artifact packaging; a private/provider dataset or credential might enter the image; route probing would require Azure mutation; protected files would change; public API status would be activated; tests require opening public ingress or relaxing safety gates; image provenance/digest cannot be established; or a deployment/promotion action would be necessary to complete the work.

## 6. Acceptance criteria

- Docker-first governance honored; no Windows runtime certification requested.
- `200 HTML` vs `503 JSON` discrepancy characterized with evidence or clearly bounded unresolved hypotheses.
- One minimal Linux image packaging plan and digest-preserving promotion sequence documented.
- Per-site Azure F1 prerequisites and rollback described without mutation.
- Local test evidence is reproducible and honestly scoped.
- PR #346/#347 remain untouched and unmerged; new C2B-28 PR remains open and unmerged.
- `PUBLIC_RUNTIME_VERIFIED=False`, public deployment **NO-GO**, WP07 **ON HOLD** throughout.

**Codex final report:** PR link and head SHA; exact changed paths; commands/tests and results; route matrix; image/provenance status; blockers; cleanup; explicit statement that no Azure/registry/deployment/merge/public activation occurred.

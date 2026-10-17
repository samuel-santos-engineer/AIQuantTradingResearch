# C2B-27 — Linux Browser Acceptance and Azure Publication Readiness (Codex Authority)

## Owner intent and fixed baseline

Continue Release 1.14's **synthetic-only showcase** without replacing the established Streamlit/Plotly interface or adding unnecessary infrastructure. This is a **qualification and planning** authority, **not** permission to merge, promote artifacts, deploy, change Azure configuration, or activate public data.

Before work, independently verify GitHub state and branch ancestry:
- PR #346 (documentation/qualification): expected OPEN, unmerged, head `0998f001afd5ffe505886695a0c6b7f686abe435`.
- PR #347 (immutable implementation): expected OPEN, unmerged, head `8240d0878dea335c08ad3216600fc7e545446140`.
- PR #345 merged at `33b941c28b0158810b4f406db965bf83ff92244a`.
If heads have changed, review the new changes and reconcile before proceeding. Do not overwrite either branch or owner work. PR #346 contains all 14 owner Markdown documents with byte-for-byte preservation and an inventory; retain them. PR #347 contains the candidate offline C# producer, immutable reader and chart adapter. Do not inadvertently base #346 on #347 or import implementation changes into the documentation PR.

## Task A — bounded Linux browser acceptance

Use the existing local Linux container qualification setup and existing dependencies where possible. Work with **only owned temporary resources**; do not alter unrelated containers, ports, credentials, volumes, images or processes. No package download or network-exposed test service without a separate owner decision.

1. Reproduce the reviewed immutable 12-selection dataset **offline**, using the verified C# producer; compare manifest and member bytes/hashes against C2B-25/C2B-26 evidence. Keep artifacts outside Git and Docker build context; do not commit/export/promote them.
2. Run the existing Streamlit/Plotly UI with the immutable read-only dataset in a Linux container using the intended bounded environment and filesystem controls. Preserve default public unavailability and avoid adding a live C# service, provider fallback or Python/JS indicator calculation.
3. Find a **minimal, temporary, isolated** browser-testing method that can reach the container UI without making it accessible to a public/LAN interface. Prefer an in-container browser or private, explicitly owned test network. Any port binding must be strictly loopback, short-lived and demonstrably restricted. If reaching the browser would require relaxing an established security/readiness gate, stop and report the blocker rather than bypass it.
4. Record **real browser evidence**, not only DOM assertions or unit tests, for both **1D (24 candles)** and **30D (720 candles)**: original candlesticks, volume, interval/range selectors, Technical Analysis popover, **SMA(20) off → on → off**, hover, zoom, pan and existing Plotly toolbar. Verify that the line's values originate in the immutable C# artifact. The owner has accepted viewport reset on SMA toggle; document it, do not attempt another compatibility fix.
5. Verify actual container health and `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` remain **503 `DATASET_NOT_READY`**; no unexpected artifact, internal endpoint, or private-file disclosure. Confirm startup and failure behavior with missing, tampered and unreadable artifacts. Capture only bounded nonsecret resource observations (CPU, RSS, startup/latency, admission), and distinguish local observations from Azure F1 qualification.
6. If a complete browser interaction cannot be proven, mark the outcome **INCOMPLETE**. Do not claim acceptance from Windows screenshots, an unclicked checkbox, a visible popover alone, or tests that only inspect figure metadata.

## Task B — Azure publication-readiness assessment (no deployment)

Produce a **concrete, minimal** release plan for the two owner-named Azure Linux F1 apps:
- `https://aiqr112wp035ec325382770.azurewebsites.net/`
- `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/`

Use PR #346's C2B-24 assessment and C2B-26 qualification evidence. Determine, without changing Azure, the exact remaining steps to make a **trusted immutable dataset** available at runtime while preserving current C# provenance, hashes, read-only permissions and the existing UI. Evaluate the smallest bounded artifact delivery mechanism compatible with the current image/startup; **do not assume** that excluded local files magically exist in the deployed image. Explicitly document what approval would be needed for artifact export, promotion, image build/push, per-site settings, runtime gate changes and public chart exposure. Do not propose the held private replay/provider path as a shortcut.

For each site, identify independently: configured pinned image/digest and rollback target; actual container runtime/UID/read-only paths and artifact availability evidence versus unknowns; single exposed Streamlit port; credentials/private-file isolation; health/readiness and route behavior; expected F1 CPU/memory/concurrency constraints; permission to deploy; and a rollback/verification sequence. If public HTTPS probing times out, record **unknown**, not pass/fail. Clearly distinguish *local synthetic chart visibility* from *public API data readiness*; `PUBLIC_RUNTIME_VERIFIED=False` is not permission to publish the chart by another path. Do not relax readiness or activate public data in this phase.

## Repository and PR boundaries

- **PR #346:** update its existing qualification report or add a concise C2B-27 evidence/decision document **on its documentation branch only**, preserving all 14 owner documents and the existing inventory. Keep documentation factual, including incomplete results. No generated datasets, credentials, private artifacts, or protected WP07/archive files.
- **PR #347:** leave implementation unchanged unless an **indispensable, narrowly scoped test-only** correction is required to perform the bounded verification. If any production code/startup/adapter/permissions change appears necessary, **stop and request owner approval before editing**. Record its exact head and changed-file inventory.
- No new framework, production endpoint, Azure resource, CI/CD workflow, package, chart redesign, indicator algorithm, snapshot promotion, provider integration or infrastructure expansion.
- Keep both PRs **OPEN and UNMERGED**. No Azure configuration, registry/image push, public rollout or deployment.

## Deliverables and acceptance

Post a concise evidence report to PR #346 with: exact refs and changed paths; reproducible offline/container/browser commands; actual browser results per range and interaction; immutable hashes/provenance; API/ingress/isolation and failure checks; local resource measurements and limits; environment cleanup; owner-file preservation; unresolved blockers; and a **separate per-site Azure publication checklist** with explicit approval gates. Cross-link PR #347. Never claim a test was run if it was not.

Outcome must be one of:
- **LOCAL LINUX BROWSER ACCEPTED / AZURE NOT YET AUTHORIZED** — only if all browser checks pass and public boundaries remain intact; or
- **INCOMPLETE / BLOCKED** — specify the precise missing evidence and minimal next owner decision.

At every point: `PUBLIC_RUNTIME_VERIFIED=False`; `/api/v1/dataset`, `/candles`, `/indicators` remain 503; public activation/deployment **NO-GO**; advanced WP07 **ON HOLD**. No merge or deployment is implied by this authority.

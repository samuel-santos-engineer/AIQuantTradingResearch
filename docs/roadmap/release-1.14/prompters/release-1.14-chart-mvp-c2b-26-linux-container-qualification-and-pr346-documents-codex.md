# C2B-26 — Immutable Synthetic Chart: Linux Container Qualification + PR #346 Document Inclusion

**Owner authority.** Planning and bounded local qualification only. Do not merge, deploy, publish artifacts, change Azure settings, or activate public data routes.

## Current baseline
- PR #345 merged to main at `33b941c28b0158810b4f406db965bf83ff92244a`.
- PR #346 is the **planning/documentation PR**, open and unmerged, previously observed head `79b175e666f17d1f7f70776516246f81e6cbc9e6` (recheck live).
- PR #347 is the **immutable handoff implementation PR**, open and unmerged, previously observed head `35e09dc4b98669111bb88cd83f6d1cf662d29f15` (recheck live). Its ten-file change includes offline C# producer, strict Python reader, gated chart integration, contract and evidence. Its base is merged PR #345, **not PR #346**.
- Prior evidence: 14 focused .NET and 19 Python tests passed; 12 selections and 12 semantic mutation rejects; local browser 1D/30D SMA off/on/off passed; generated candidate set 656,176 bytes. Docker Linux engine was unavailable. None of these establish Linux or Azure qualification.
- `PUBLIC_RUNTIME_VERIFIED=False`; public `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` remain `503 DATASET_NOT_READY`; Azure publication NO-GO; advanced WP07 ON HOLD.

## A. Include untracked owner documents in PR #346
The owner explicitly requests **all untracked documents** be included in PR #346. Treat this as a documentation inventory and inclusion task, not blanket permission to stage all untracked files.

1. Recheck the current worktree, branches, open PRs and `git status --porcelain=v1 --untracked-files=all`. Identify all incoming untracked **documents** with exact paths and content hashes, including those previously called owner documents. Check whether some are already tracked, ignored, duplicated, or belong to PR #347.
2. Inspect each candidate sufficiently to classify: safe project documentation vs. secrets/credentials, private data, generated datasets, build outputs, personal notes, protected WP07/archive materials, or other non-document artifacts. Do not print secret contents to logs or PR comments. **Do not automatically include** any sensitive or protected item; list excluded items and request owner disposition. Do not silently omit eligible documents.
3. On PR #346's existing branch, add **all eligible untracked documents**, preserving original bytes and paths. Include a manifest in the PR description or a documentation file showing included paths and SHA-256, plus excluded paths and reasons without exposing secrets. Do not use `git add -A` or broad globs; stage exact reviewed paths. Do not remove or overwrite owner files. If a document belongs to an incompatible protected boundary, stop for owner decision rather than reclassifying it.
4. Avoid accidentally importing PR #347's source changes into PR #346. Keep #346 documentation-only. If branch/worktree layout prevents safely accessing the owner documents, stop and report the exact blocker; never reset, clean, stash, cherry-pick, or move owner files without authorization.
5. Commit and push only the reviewed documentation inclusion to **PR #346**. Leave PR #346 OPEN/UNMERGED. Recheck PR diff and confirm no executable code, generated market dataset or sensitive file was introduced.

## B. Bounded Linux container qualification of PR #347
This is **local qualification**, not promotion or production readiness. Work from PR #347's exact head in an isolated owned worktree or container build context; keep the two PR histories separate. Do not merge PR #346 into PR #347 merely to obtain documentation.

1. Check availability of a **Linux Docker engine** and record Docker/client/server details. If unavailable, **stop container-specific claims** and report the precise blocker. Do not install Docker, change host configuration, or substitute Windows-only proof for Linux qualification.
2. If available, build or run a local test image from the reviewed PR #347 candidate with no Azure credentials, Twelve Data configuration, private replay data or inherited owner secrets. No remote push, no registry publish and no Azure mutation. Use explicit, bounded CPU/memory/PID and timeout limits appropriate for exploratory F1 sizing; record actual values and results, not invented F1 guarantees.
3. Generate immutable artifacts **offline using the existing C# producer** in an owned scratch location. Validate exact contract, provenance, 12 interval/range selections, full hidden 250-row warmup for analytics, SHA-256 manifest/member hashes, sizes, bounded read behavior and no Python/JS SMA recomputation. Keep generated artifacts outside Git and production image promotion. Use an explicitly temporary read-only container mount for qualification if necessary; document why this does not constitute an Azure artifact delivery mechanism.
4. Inspect actual Linux UID/GID, effective file and directory permissions, mount ownership, startup process tree, container network/port bindings, environment scrub, and whether private files/settings can be accessed. Demonstrate the intended fixed-path reader can consume only the qualified immutable set and fails closed on missing/tampered/truncated/extra or wrong-selection data. Do not loosen existing public-readiness guards to get a green test.
5. Exercise bounded concurrent reads and request-size/latency/memory/CPU behavior; record methodology and observed peaks, process exit and cleanup. Avoid abusive load or unlimited concurrency. A local pass is evidence for feasibility, **not** proof of Azure F1 resource adequacy.
6. Browser-smoke the existing Streamlit/Plotly chart inside the local container when practical: 1D and 30D candles/volume, selectors, native Technical Analysis popover, SMA(20) off → on → off, hover, zoom/pan and existing toolbar. Zoom reset on SMA toggle is an owner-accepted UX limitation. If browser automation fails, distinguish test-harness failure from app failure and report incomplete checks honestly.
7. Verify `/api/v1/dataset`, `/api/v1/candles`, `/api/v1/indicators` still return **503 `DATASET_NOT_READY`**, and that no local synthetic handoff or private endpoint is exposed via public ingress. If this cannot be established, mark qualification blocked.
8. Do not edit the production Dockerfile, `container/entrypoint.sh`, public startup, readiness flags or Azure settings merely to make the local test pass. If a small implementation correction is essential, propose it first or make it only within the already authorized immutable candidate scope on PR #347, with exact diff and tests; never silently widen the release boundary.

## Evidence and PR separation
- **PR #346:** add eligible owner documents and a concise qualification report (including blockers, environment, reproducible commands, measured observations, test counts, screenshots if safe, and explicit non-claims). Documentation-only; update PR description as needed.
- **PR #347:** leave implementation open/unmerged. Add code only if a narrowly necessary, clearly justified correction falls within prior authority; otherwise report suggested changes without making them. Link qualification evidence from PR #347 to #346.
- Include exact branch/head/base and changed-file inventories for both PRs after work, hashes for the documents, and proof that nonselected owner/protected files were untouched.
- Do not claim public runtime readiness, Azure compatibility, successful container testing or browser acceptance unless directly demonstrated.

## Stop conditions and owner gate
Stop and report if Docker Linux engine remains unavailable, if documents contain secrets/protected material, if the source branches cannot be isolated safely, if immutable artifacts lack trustworthy provenance, if container permission/resource isolation cannot be shown, or if any public data route becomes accessible contrary to the 503 contract.

**Completion target:** PR #346 contains all safely includable untracked project documents plus truthful C2B-26 qualification evidence; PR #347 remains the isolated implementation candidate. Both remain **OPEN/UNMERGED**. No Azure deployment, artifact promotion, public activation or WP07 restart. Separate owner approval required for those actions.

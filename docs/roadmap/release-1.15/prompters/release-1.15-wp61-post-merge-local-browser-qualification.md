# Release 1.15 WP61 - post-merge local browser qualification

Date: 2026-11-15 UTC. Result: **MERGED_SOURCE_LOCAL_QUALIFICATION_PASS**. This is fresh evidence for the selector-corrected merged implementation, distinct from the historical pre-fix acceptance and the reconciliation's NOT_RUN status. Earlier evidence is preserved unchanged.

## Source and preflight

Verified PR #450 MERGED with squash/main commit **`cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b`**. Its reviewed pre-merge head was `f5a0bd174a68385f1850f34e2897857abc230faa`. Fresh isolated clean worktree from fetched main; repository and merged callback/single-reconciliation code inspected. PR #446 independently verified OPEN at `187e6584571ed4d978511f0abfc7caa3021ea481`; untouched. No implementation change in this task.

Minimum local preflight passed before Stop: existing owned running containers/networks, exact API/pre-fix UI IDs, imported named volume/owner labels, matching existing shared keys privately in memory, loopback18555, consistent read-only API/server and UI/CA mounts, current public TLS validity with DNS SAN `wp07-research`, and all rollback images. Existing authenticated snapshots each contained1,000 rows. Protected task-owned rollback bookkeeping held resource IDs/environment names; temporary host-path references were removed after use. Current TLS references remain in the existing owned Docker configuration, not Git. No credentials, TLS private material, raw observations or private paths were published.

Previous UI **`sha256:79ed4ff5d92e11866d6d01d70f4b8fff8b597e4533c94103915f4796a18c0163` predates the selector fix** and is not the image qualified here. API remains `sha256:1edebb818affbd362385e8a69cddb52b8309265a902dec0f0f5da31da44ed028`.

## Fresh unit tests and image

Existing project venv/dependencies, merged source:

```text
PYTHONPATH=python/presentation
python -B -m unittest test_research_chart test_research_snapshot_client -q
```

**90 tests passed, exit0,23.477 seconds, no skips.** Includes six deterministic widget/model handshake cases, pruning/identity reset causes, callback user intent/session independence, exact Decimal/null semantics and all WP59/WP60 cases. Existing bare-mode Streamlit ScriptRunContext warnings occurred. This is a newly executed result, not the historical90 count substituted without running.

UI-only build:

```text
docker build --pull=false --network=none --platform=linux/amd64 --build-context qualified_python=docker-image://aiq-c2b36:candidate -f <owned-build-directory>/research-chart.Dockerfile -t aiq-wp61:post-merge-cca6a81 <owned-build-directory>/context
```

Build exit0. **New immutable local UI image ID:**

`sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`

Linux/amd64; UID/GID12002:12002; exposed port8501. Unmodified supported Dockerfile/entrypoint (`/opt/venv/bin/python -B -m streamlit run /app/python/presentation/realtime_financial_visualization.py` plus its existing fixed runtime flags). Exact cached dependencies `sha256:98501ff282a7b1fb053d3abd9653acff3dde8d4182bfdfc2a9c426de462168ed`; pinned Python base `sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024`. No package installation, pulls, API build, dependency upgrade or push.

Allowlisted context: ten existing public presentation source/assets plus favicon. No private preparation, datasets, SQLite, keys, TLS or fixtures. A disposable --rm/no-network/read-only/capability-dropped container matched all eleven `/app` file hashes to the context. Post-browser git-blob checks independently matched **all eleven** to merged commit `cca6a814...`. No inherited-layer universal audit is claimed; historical images/tags were preserved.

## Supported runtime qualification

The existing launcher cannot replace UI only. Verified ownership, asset identities, current TLS and rollback references made the explicitly authorized paired switch safe. One owned Stop and one Start, each exit0, recreated the pair/two networks with the new UI but the **same API image, imported named volume, shared key and recovered TLS pair**. No unexpected operation or recovery attempt. Startup authenticated TLS probe reported BTC1000/ETH1000, then Streamlit readiness passed. No initialize/import/migrate/delete/replace operation on the retained volume, no data transfer, key/TLS rotation or expired-manifest shortcut. Container/network identities changed; retained image/volume identity did not. No byte-for-byte SQLite preservation claim.

## Actual browser evidence

Actual installed Chrome, existing venv/websockets and the qualified local harness; owned profiles/loopback CDP, fixed1280x1200 viewport, disabled browser background networking/component updates/sync. Native visible/hit-testable dropdown/search/option interactions, keyboard range controls and real pointer pan; no injected Streamlit state or assigned control values. The authenticated local client supplied reference fields through a captured comparison-only in-memory pipe, never printed/saved as DTOs. Expected changes were independently calculated using Fraction plus integer half-even rounding, not the application Decimal helper. Only match results/counts/relative indices were reported; no screenshots or prices.

```text
<existing-project-venv-python> -B <owned-qualification-directory>/browser.py
```

Three bounded browser sessions are distinguished:

1. First full attempt passed BTC cases and initial/several ETH cases, then failed to change ETH B from index19 to0. The table retained prior indices999/19; that run was partial, not accepted. It did not prove incorrect values for a successfully changed selection.
2. One narrow ETH diagnostic passed exactly that transition and detected no visible exception/unexpected reset notice. This does not independently prove the original interaction's root cause.
3. Final full attempt used native press/release pacing and passed all cases below. No application correction, rebuild or runtime restart occurred between sessions. Browser-driver timing/interaction uncertainty remains; one pass does not prove exhaustive race freedom.

| Fresh final complete check on merged source | Result |
| --- | --- |
| BTC -> ETH -> BTC and independently retained A/B choices | PASS |
| BTC A0/B999, A0/B0, A0/B19, A999/B19, A19/B19, A19/B999 | Exact DTO + independent changes PASS |
| ETH A0/B999, A0/B0, A0/B19, A999/B19, A999/B0 | Exact DTO + independent changes PASS |
| Native selection callbacks/reruns reflected in model and table | PASS |
| First/last/same/reversed/warmup/populated SMA20 fields | PASS |
| Signed B-minus-A and A-denominator percent, half-even formatting | PASS |
| Null volume/SMA warmup explicitly Unavailable | PASS |
| WP60 inspection values/selection independent of comparison | PASS |
| WP59 bounded16/single1/full1,000 ranges and preset100 | PASS |
| Return to Live restores100; real pan/explicit command coexistence | PASS |
| Explicit Refresh retains A/B and inspection; final full preset | PASS |
| Visible widget exception or unexpected reset notice | None detected |

Destructive pruning, generation/date/transform changes, missing/empty/single-candle states and zero denominator were **not induced in authentic data**. Their90-test fake evidence remains separate; the browser's no-unexpected-notice check is not a live forced-reset test. Retained values remain transformed educational API values, not actual/live provider quotes. No provider acquisition or freshness relabeling.

## Final retained-assets verification and cleanup

At **2026-11-15T02:13:59Z**, final authenticated invariants passed: **BTC1,000 and ETH1,000; all volume fields null; SMA20 null19/computed981 each**. API image, imported named volume identity/ownership labels, existing key equality, TLS read-only references and unchanged public CA, all rollback images and loopback-only18555 mapping passed independent post-browser checks. API container was recreated, not its image/data volume. No private PFX content read/hash or database byte audit.

All three owned Chrome processes/profiles were closed/cleaned; cleanup verified absolute targets stayed within the unique task-owned temporary directory. Temporary task-pointer/host-path bookkeeping was removed. Retained runtime, TLS, keys, images and volume were untouched by cleanup. Nonsecret audit results and rollback IDs remain; rollback was not needed or exercised, so recoverability evidence is identity/reference availability, not a performed restoration.

The merged-source chart is left running at **http://127.0.0.1:18555** for owner review. Current temporary seven-day TLS still needs validity checks before future startup; old preparation manifests must not silently select old TLS/UI. Historical acceptance is not erased or confused with this new image.

## Handoff

Unit tests, UI build/source alignment, local runtime, final actual browser acceptance and retained-asset checks **PASS**, subject to the explicit fake-only/race/byte-audit limits above. This task changes **only this documentation file**, against verified main; no source/API/schema/Dockerfile/dependency changes. UTF-8/LF/no BOM and git diff --check required before publication. Open a documentation-only PR for owner review and leave OPEN/UNMERGED. No PR is merged by this task; #446 untouched.

No Azure/F1 quota/public deployment/production/provider/acquisition/import/registry/storage-migration/credential-or-TLS-rotation operation. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`. Local qualification does not establish Azure readiness.

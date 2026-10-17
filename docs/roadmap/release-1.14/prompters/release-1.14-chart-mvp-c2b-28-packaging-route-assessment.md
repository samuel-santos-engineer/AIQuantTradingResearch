# C2B-28 — image packaging and route-contract assessment

2026-10-16. **GO for owner review of the next bounded packaging/startup implementation milestone; execution needs new authority. NO-GO for release certification, artifact promotion, registry publication, Azure deployment or activation.** No production correction implemented in this documentation-only task.

## Baselines and governing policy

New isolated documentation branch from `main` at `612483e570d4b8e1f99d512681ca26b1bc1320cd`. PR #348 is MERGED at that commit; its only change is `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md`. Its effective-after-merge condition is satisfied despite historical proposed/checklist wording. Linux Docker intended-image acceptance and exact-digest promotion govern; Windows-native runtime certification is not required. Existing export/promotion, G-B0–G-B7 and WP07 gates are not waived.

Read-only live checks: PR #346 OPEN at `0e568d208f2769037bf4a7265ea4ba6f4609aacc`; PR #347 OPEN at `8240d0878dea335c08ad3216600fc7e545446140`, both based on `33b941c28b0158810b4f406db965bf83ff92244a`. Their histories/content were not imported or modified. Original checkout stayed on #346; its fourteen preserved owner documents/inventory, later evidence and incoming C2B-28 authority were not edited. New work used an owned temporary worktree; no protected WP07/archive checkout, stash/reset/clean or owner-file movement.

## Source architecture and trust inputs

| Input at main612483e | SHA-256 of Git blob |
|---|---|
| Dockerfile | 9d23347aa87d53684551d9d7642dd85ce4b4c7d5745f6303f51f8ad1a6ac3a6f |
| container/entrypoint.sh | 41a08358241de32a9a02c720a5d2e670beb4f16059e1d39a6abf787c19e3237a |
| python/presentation/public_preview_app.py | 47b78ab86f15e1ae89673ff888c46d3adebe5c4b663d3ca6ad3489eb1d62beff |
| python/presentation/public_preview_api.py | 752dd4d62e95fee7526c177ada3a4af326602f6c0ef3f2053bafbcc5fea84fef |
| requirements.txt | 73ff4d4afd1d070f6c12c0d5b19b662176ed6ef44c7d0ff548e6a13a17d873aa |
| .dockerignore | 9e9a23ceb5f94e385fbdd3ff5b2fae98297fd210a10930e08116f89fce6872e4 |

Main Dockerfile: .NET SDK10 build/restore/publish, ASP.NET10 runtime copied into Python3.13-slim; gosu/libicu plus existing requirements; copies Python and Worker; app/runtime chowned to aiq, image USERroot, EXPOSE8501. Base tags float rather than pinning immutable digests. It is not yet the single certified immutable-data release image.

Public entrypoint defaults to `AIQ_RUNTIME_MODE=public`, removes empty/runtime and checks private directories while root, creates cache/temp directories, gosu-drops to aiq then env-i launches **only** `streamlit run /app/python/presentation/public_preview_app.py --server.address 0.0.0.0 --server.port 8501 ... --secrets.files []`. Provider/replay/candidate environment is not forwarded. Private mode is separate; no private mode was selected or qualified here.

The public script instantiates Streamlit's App with `PublicBoundary`; API/admin paths are reserved before SPA fallback. Data readiness remains false, so three data routes503 JSON; unknown API404 JSON. `/` is Streamlit HTML and health is independent. Main has only the Windows Development local HTTP adapter, not the unmerged immutable integration. #347 adds a fixed `_immutable_chart_candidate` directory reader, externally pinned candidate manifest and Development/loopback/no-Azure gate; it also excludes that directory from Git and Docker context. Neither ignored files nor environment opt-ins create a production artifact path. The #347 ownership check currently requires the effective UID, conflicting with a future root-owned non-public-writable release layout. No gate or owner rule was relaxed.

## Route matrix — fresh direct evidence

Every request GET, no redirects followed, host proxies disabled, request Cache-Control:no-cache,8s timeout,700ms spacing,131072-byte response limit. No retries, auth headers, cookies or raw market-data output. All eighteen requests completed without redirect/error/truncation. JSON with non-null data would stop probing. Full UTC/status/content-type/body-size/hash and selected nonsecret response headers are in [route evidence](release-1.14-chart-mvp-c2b-28-route-evidence.json), SHA256 `24ed4716a0cd72bd448e14a1353f483eaaf4096d4bcf9d4ab5fc6c2d7956c7fd`.

| Path | Current-source local public mode | Production WCUS | Staging WUS2 |
|---|---|---|---|
| / | 200 text/html | 200 text/html | 200 text/html |
| /_stcore/health | 200 text/plain, ok | 200 text/plain, ok | 200 text/plain, ok |
| /api/v1/dataset | 503 application/json, DATASET_NOT_READY | 200 text/html | 200 text/html |
| /api/v1/candles | 503 application/json, DATASET_NOT_READY | 200 text/html | 200 text/html |
| /api/v1/indicators | 503 application/json, DATASET_NOT_READY | 200 text/html | 200 text/html |
| /api/v1/nonexistent-c2b28 | 404 application/json, NOT_FOUND | 200 text/html | 200 text/html |

Azure production probes13:18:16–24UTC, staging13:18:25–33UTC; local13:20:26–31UTC on2026-10-16. Each Azure API and unknown-API body is **byte-identical to root HTML**,10951 bytes, SHA256 `2fab6bc0f84df613fc198156c44674f6be1b0999cb39c29b3698647b9598dd3b`, ETag `3bcb2d75098a5b87db5019cd2b012faa`, Server:uvicorn, Cache-Control:no-cache. Same frontend HTML hash locally. Local API JSON uses Cache-Control:no-store; sizes742/94/94, unknown API86. No redirect, Age or X-Cache header observed. Absence of cache headers is not proof of no intermediate cache. Health200 is not API correctness or chart readiness.

**Fact:** Azure API paths are receiving the Streamlit SPA fallback rather than the required JSON boundary. No synthetic/private/provider response observed, no public data activation attempted. **Unproven:** exactly where the API boundary is lost or which source produced the configured deployed image.

Ranked hypotheses and next non-mutating evidence:

1. Deployed image content/selected entrypoint predates or bypasses `public_preview_app.py`/PublicBoundary. Leading hypothesis: current-source public startup passes locally, while Azure matches generic SPA fallback. Need existing-registry OCI config/revision provenance and actual selected process command/module hashes, not an image rebuild.
2. Actual running container differs from ARM's configured digest, or startup/mode differs from assumptions. Need read-only effective running digest/startup evidence per site, with no secret values. Empty ARM startup override does not establish an image's ENTRYPOINT/CMD.
3. Ingress/proxy rewrites /api to SPA/root, or caches HTML. Possible, not established. Need separately authorized read-only comparison of container-internal route responses and public ingress, plus relevant routing config. No-cache requests/fresh Dates/absent Age make simple stale-cache explanation less compelling, not impossible.

No API schema rewrite or application proxy is justified by these observations. Current source handler logic already produces the expected contract. Do not change status codes to match the deployment or declare either Azure API healthy.

## Local screening image and limitations

Docker Linux29.7.2. No dependency/image downloads: reused existing local runtime `sha256:56a9bab04e7b03441f6b811fd6afbc67b18203c7e0ebf0540b69298d19f66cd5`. Temporary derived image replaced old `/app`/`/runtime`, copied **current-main Python and exact entrypoint**, and reproduced public default startup/port8501. It did not contain a dataset or Worker output. Local image identity/index `sha256:62521a204a64756174de15dcffc3aef0f685bfa1d792a1008e2f01a4472dc909`; platform manifest `sha256:44a270681256185f7957f891cff5c66aea3e9244f851f587e8b11d05cb5bb71b`; config `sha256:84520f04d3a1323f69d9bf75554591f60f6c9705c64e1e50d791018cc7813d6f`.

This is **routing/startup component screening**, not a fresh full build of production Dockerfile, not the deployed Azure digest, not the intended data-bearing RC. Source/Dockerfile provenance is explicit; do not substitute this image for exact-image release certification. No C# compile, package restore, unit/.NET suite, twelve-selection/semantic requalification or browser run in C2B-28. Prior C2B-27 accepted Linux chart path remains historical prototype evidence; it cannot certify this data-free main image or a future changed packaging/startup path.

Workloads network none, no published ports, oneCPU,512MiB/equal swap,64PIDs/no-new-privileges. Initial public root setup used normal Docker capabilities, then PID1 was Python/Streamlit UID1000; no C# Worker. Matching-UID inspection confirmed env-i retained only explicit PATH/locale/temp/.NET telemetry/process-count controls, no provider/private environment. Initial root-default docker exec could not read the dropped-UID process environment; repeated as UID1000, printing names only. No credential/private data inspected.

Actual app and API files were UID1000 mode0755 and writable by runtime on writable rootfs; entrypoint UID0 mode0555 not writable. Artifact directory absent. With `--read-only`, exact entrypoint failed immediately: `rmdir /runtime: Read-only file system`, exit64, no OOM. No correction/bypass attempted. A future readonly immutable-image layout needs build-time directory preparation/non-root startup and reader ownership changes under new authority.

Public-mode container process cgroup peak135286784 bytes, CPU usage3140871 microseconds at sample; one throttled period448 microseconds. These include screening/inspection, not F1 load, concurrency or startup performance certification. Stopped public container exit0; readonly-startup container exit64. Temporary image and both containers removed, pre-existing runtime preserved; no data/network/volume/host-port resources created. Build cache not broadly pruned.

## One minimal self-contained image design — not implemented

Select **one image-bundled eligible immutable release**, no runtime download/share/SQLite/live C# service. Keep the established Streamlit/Plotly application and existing dependency set; C# generation/calculation remains an offline operation. Preserve private code boundaries and do not repurpose replay/provider startup. This proposal does not require a Windows certification layer or a new service/workflow.

1. Owner first disposes the #347 candidate baseline and authorizes the exact production handoff context/ownership and local packaging inputs. Existing candidate-v1/pin/656176-byte set is qualified as a temporary prototype, not an accepted promoted release. C2B-28 permits design/local screening, not production export/promotion. Linux packaging itself is not categorically forbidden; no data-bearing image was built here because production context/ownership/gates need essential code changes prohibited by this task.
2. Separately authorized offline Linux producer reuses unchanged generator seed114, frozen `[2025-10-26T00:00:00Z,2026-10-01T00:00:00Z)`, twelve selections and C# aiq-ta-v1 SMA. Exactly250 hidden warmup rows, only visible rows serialized. Verify existing manifest/member hashes, independent proof mapping, licensing and operator/reviewer provenance before accepting bytes. No build/startup repair/regeneration or provider input.
3. Create an **owned temporary staging build context outside Git** from an exact reviewed source archive plus an explicitly eligible `_release-input/` directory containing exactly manifest+twelve members. Validate finite names/counts/bytes/checksums and trusted manifest anchor before build. No copy of the live workspace/untracked files, credentials, private replay, arbitrary URLs or ignored local candidate directory. A reviewed staging-context allowlist permits only these source/build inputs. Existing primary `.dockerignore` exclusions must not be silently removed to smuggle candidate files in.
4. Final Dockerfile copies eligible `_release-input/` to one fixed app-owned release path (proposed `/app/python/presentation/immutable_release`), root-owned files0444/directories0555, with root-owned/non-writable code/anchor and all replaceable ancestors. Prepare required writable scratch/home paths during build or in narrowly scoped tmpfs, and start the public process non-root with no runtime root cleanup. Preserve env-i and a single exposed Streamlit8501. No request-time C# child; existing runtime dependencies may remain for minimal compatibility rather than adding anything. No claim that unneeded .NET files are themselves a live service.
5. New reviewed reader accepts the explicitly trusted root-owned image assets with strict no-link/non-writable ownership checks, bounded complete-set integrity/schema/selection/grid/visible/SMA validation, and coherent retained bytes. Existing UID-equality/candidate path checks cannot simply be assumed compatible. Gate defaults unavailable; local qualification context and separately approved public chart/API readiness are explicit. Do not use Development or strip Azure markers to bypass the current candidate gate. No public exposure through a side route while readiness is false.
6. Test the **actual intended final image**, not this overlay: readonly non-root startup, missing/corrupt/unreadable bundle and generic unavailable behavior; all12 selections/semantic faults; port8501 six-route matrix/admin/asset exclusion; bounded concurrency/CPU/RSS/health/session/cancellation behavior; real1D/30D browser off/on/off and line-to-artifact comparison. No automatic reuse of prior browser results when ownership/path/startup/image changes. Qualify all applicable G-B gates; no durability/abuse waiver from process-local semaphore.

No synthetic assets were committed or baked into a public production image. Production export/promotion and independently trusted release identity remain separate approvals, not effects of this design PR. Freeze/accept finite schema, ownership, retained allocations/session budgets and release/revocation semantics before implementing them.

## Exact digest handoff — future authorized lifecycle

Build once for Linux/amd64 using reviewed source, eligible bytes, pinned resolved base-image digests and recorded dependency/toolchain versions. Record the OCI platform manifest digest **D**, config/layer digests and, if present, enclosing index/attestation digests distinctly; a mutable tag/local image ID alone is insufficient. Preserve the complete OCI artifact/content locally through certification. Run Linux/API/browser/security tests on that platform manifest's exact contents, record inputs and outcomes, then freeze it. No rebuild or file mutation afterward.

With **separate registry publication authority**, tag/push that existing artifact, never re-run the build or generator. Read back registry manifests/config/layers and verify platform digestD and all eligible handoff hashes exactly; if the client rewrites manifests or D differs, **stop**, do not substitute a new digest or claim prior certification. Prefer explicitly deploying the recorded Linux/amd64 platform digestD rather than silently selecting another platform from an index. No actual registry handoff was exercised here; tool/content-preservation behavior must be demonstrated before deployment.

With **separate staging deployment authority**, point the staging site at repository@D with only reviewed configuration. Verify effective running digest, source/startup/UID/readonly paths, health/routes/bundle exclusion/resource and real-browser behavior on staging. PUBLIC_RUNTIME_VERIFIED remains false until explicit gate review accepts the required evidence; local UI visibility is not public permission. Registry push, staging deployment, public chart exposure and public API readiness are distinct owner decisions.

After owner staging acceptance and **separate production authorization**, production receives the same already-certified D, no rebuild. Independently verify production. Roll back to an explicitly qualified unavailable image/configuration on any failure, including unknown verification state. Preserve exact old digest/configuration and revoke/retire releases coherently. Never copy provider credentials between sites.

## Independent site readiness and rollback

README roles confirmed: **production WCUS** `aiqr112wp035ec325382770`; **staging WUS2** `aiqr112wp05wus27f5eabb5` (independent apps, not assumed deployment slots). Each ARM config queried independently around the matrix time:

| Fact/gate | Production WCUS | Staging WUS2 |
|---|---|---|
| Resource group | rg-aiq-r112-wp03-wcus-5ec325382770 | rg-aiq-r112-wp05-wus2-7f5eabb5 |
| Configured digest | Shared reference below, independently read | Shared reference below, independently read |
| Startup override / healthCheckPath / AlwaysOn | empty / null / false | empty / null / false |
| Nonsecret settings | port8501, storage=true, no explicit AIQ_RUNTIME_MODE | port8501, storage=true, no explicit AIQ_RUNTIME_MODE |
| Effective running image/entrypoint/UID/readonly assets | UNKNOWN; ARM config is not execution evidence | UNKNOWN, independently unverified |
| Current route prerequisite | API paths200 HTML, not503 JSON | API paths200 HTML, not503 JSON |
| Immutable path/bytes | UNKNOWN/never promoted by this task | UNKNOWN/never promoted by this task |
| Rollback reference | observed144de4... only; not qualified503 fallback | same observed digest, independently recorded; not qualified fallback |
| Deployment/exposure permission | NOT granted; read access proves no write authority | NOT granted; must precede production under new authority |

Both configured: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:144de4a9c393f07d2249f450b9d5ab2af34f36b941897a31bd20d72ac325959a`. Actual immutable image available locally differs from this digest; Azure image source provenance is unverified. Empty override plus matching digest is not proof that startup/config/runtime are identical. No secret appsettings, filesystem/private data or console sessions accessed.

Before **staging**: approve input export/promotion eligibility and narrow source changes; qualify the final image; authorize registry push/pull authentication and staging settings/restart/deployment scope; capture secure per-site old configuration; select a contract-compliant rollback target (current144de4... fails API contract); establish fresh effective-image/entrypoint evidence. Deploy only after separate authority. Then verify digestD, no private settings/files, only8501, health/API JSON/asset exclusion and authorized chart behavior, resource/idle/cold/session/recovery limits. Any timeout ->UNKNOWN/stop, not pass.

Before **production**: require owner acceptance of staging and artifact/gate evidence, explicit production configuration/deployment/chart-exposure permission and an independently recorded rollback target. Apply the same D and verify this resource separately; no automatic rollout from staging. On failure restore the preapproved unavailable digest/config under authorized procedure, verify health503-boundary/no-data exposure, account for active sessions/revocation, and stop if verification is unknown. An old digest that serves HTML200 is not a proved compliant rollback.

F1 remains the owner-required existing Linux target; no new paid resource proposed. Actual current plan CPU allowance, shared headroom, memory, session/concurrency capacity and cold/idle behavior are not certified by oneCPU/512MiB local caps or README planning numbers. Obtain resource-specific evidence under future authority, preserve zero-recurring-cost and all G-B obligations. No credentials/RBAC changes or new monitoring/service proposed.

## Reproduction and next bounded authority

Owned run: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b28-d8974bfd005c47e59c00ebbdb3e3b981`. PowerShell5.1 orchestration; Linux is runtime. Temporary routing Dockerfile only: existing pinned local runtime; remove old app/runtime; copy current Python/favicon/entrypoint; chmod entrypoint0555; USERroot; EXPOSE8501; exact entrypoint. Not committed and not a replacement production Dockerfile.

```dockerfile
FROM ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp07-pid-retry-7e857b38
USER root
RUN rm -rf /app /runtime && mkdir -p /app /runtime && chown aiq:aiq /app /runtime
COPY --chown=1000:1000 repo/python/presentation/ /app/python/presentation/
COPY repo/favicon.ico /app/favicon.ico
COPY repo/container/entrypoint.sh /usr/local/bin/aiq-entrypoint
RUN chmod 0555 /usr/local/bin/aiq-entrypoint
WORKDIR /app
EXPOSE 8501
ENTRYPOINT ["/usr/local/bin/aiq-entrypoint"]
```

The local base tag resolved to the recorded56a9bab... image; stop if it resolves differently, rather than pulling a substitute silently. `repo` was the isolated current-main worktree. This exact temporary setup deliberately excludes Worker/data and does not establish full production-image equivalence.

```powershell
docker build --pull=false --network none -f "$run\Dockerfile.routes" -t aiq-c2b28-routes:612483e "$run"
docker run -d --name aiq-c2b28-public --network none --cpus 1 --memory 512m --memory-swap 512m --pids-limit 64 --security-opt no-new-privileges --env AIQ_RUNTIME_MODE=public --mount "type=bind,source=$run\proof,target=/proof,readonly" aiq-c2b28-routes:612483e
docker exec aiq-c2b28-public /usr/bin/env -i PATH=/opt/venv/bin:/usr/bin:/bin timeout 20 python -B /proof/probe_matrix.py http://127.0.0.1:8501 local-public /tmp/c2b28-local-routes.json
# Host invocation, once per named HTTPS URL, same six GETs:
.\.venv\Scripts\python.exe -u -B "$run\proof\probe_matrix.py" https://aiqr112wp035ec325382770.azurewebsites.net production "$run\azure-production.json"
docker run --name aiq-c2b28-readonly-startup --network none --read-only --cpus 1 --memory 512m --memory-swap 512m --pids-limit 64 --security-opt no-new-privileges --env AIQ_RUNTIME_MODE=public aiq-c2b28-routes:612483e
```

Probe implementation is a temporary standard-library urllib script implementing the methodology above; results are the committed sanitized JSON. All containers/image cleaned, no published ports/network/volumes or data generated. Main checkout still preserves its original authority input. `git diff --check` and exact changed-path inventory verified before PR creation. No Windows-native runtime acceptance claimed.

Proposed follow-on implementation scope, **not authorized here**: existing `Dockerfile`, `container/entrypoint.sh`, #347's `python/presentation/immutable_synthetic_chart.py`, `python/presentation/realtime_financial_visualization.py`, `python/presentation/test_immutable_synthetic_chart.py`, `python/presentation/test_public_preview_shell.py`, `python/presentation/test_public_preview_api.py`, plus a separately reviewed bounded packaging command/script only if needed. Freeze the exact allowlist and accepted #347 baseline first. No generator/calculator/provider/E4 changes. Tests must verify root-owned/read-only/non-root startup, default unavailability, packaged provenance and real final-image behavior. No route-handler rewrite without further evidence; diagnose effective deployed startup/image first.

**Decision:** next bounded implementation design is reviewable (GO for owner authorization review). Execution, self-contained RC certification and all publication/deployment stages remain NO-GO until their explicit approvals/evidence. PR #346/#347 untouched/unmerged; this new documentation PR OPEN/UNMERGED. `PUBLIC_RUNTIME_VERIFIED=False`; public activation/deployment NO-GO; advanced WP07 ON HOLD.

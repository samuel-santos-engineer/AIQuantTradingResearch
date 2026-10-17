# C2B-24 — Azure synthetic chart runtime feasibility

Date: 2026-10-16. Outcome: planning-only candidate; runtime implementation blocked on contract disposition. No deployment or activation authority is requested by merging this document.

## Baseline and evidence scope

`origin/main` fetched and PR #345 confirmed merged at `33b941c28b0158810b4f406db965bf83ff92244a`. C2B-24 uses a new branch from that baseline. All twelve incoming untracked owner documents are excluded and hash-preserved. No protected WP07/archive checkout was accessed. No source, dependency, startup, configuration or readiness edits.

Read-only Azure CLI inspected the two named sites, their container configuration, setting names, and only three nonsecret setting values: runtime mode, routing port and storage enablement. No credential values, application logs or private data were read. ARM metadata describes configuration, not a running container's installed binaries or effective UID/filesystem boundary.

| Observation | `aiqr112wp035ec325382770` | `aiqr112wp05wus27f5eabb5` |
|---|---|---|
| Resource group | `rg-aiq-r112-wp03-wcus-5ec325382770` | `rg-aiq-r112-wp05-wus2-7f5eabb5` |
| Hosting | Running Linux custom container; F1 Free | Running Linux custom container; F1 Free |
| Configured image | Same pinned digest below | Same pinned digest below |
| Startup override | Empty | Empty |
| Routing port / App Service storage | 8501 / true | 8501 / true |
| Explicit `AIQ_RUNTIME_MODE` | Absent | Absent |
| Always On / health-check path | false / null | false / null |
| Setting-name inventory | Includes legacy provider/replay settings | Includes legacy provider/replay settings |

Configured image: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:144de4a9c393f07d2249f450b9d5ab2af34f36b941897a31bd20d72ac325959a`.

Both sites have equivalent configured presentation-container topology; neither name establishes a distinct API role. Whether both should publish the chart remains an owner decision. Repository Dockerfile contains Python 3.13, ASP.NET/.NET 10 and the Worker DLL. This does not prove the deployed digest matches today's Dockerfile or has the candidate DLL. No console execution, image pull/build or deployment was performed. There is no tracked `.github/workflows` directory at this baseline; do not assume an existing deployment workflow or invoke one.

Four bounded public HTTPS reads (12-second timeout each, 700ms spacing) attempted `/_stcore/health` and `/api/v1/dataset` on each URL. All timed out from this execution environment. This establishes neither site failure nor successful public health/503 behavior; effective exposed UI/API roles remain unverified. ARM `Running` is not an HTTP health result.

## Minimal selected design proposal — not implemented or qualified

Retain the existing multi-runtime container and Streamlit UI. A synthetic-only C# data source would use the existing generator and `TechnicalIndicatorCalculator.CalculatePublic`, with full 250-row warmup and visible-only projection. An explicitly separate Linux adapter would consume a fixed server-side loopback endpoint. No browser access to that endpoint, arbitrary upstream URL, private Worker DI, provider configuration, data persistence or Python/JavaScript analytics. Keep the Windows Development adapter unchanged.

Only Streamlit port 8501 would be exposed by App Service. Internal loopback binding alone does not establish confidentiality or prevent public proxying: the complete ingress route and process/environment model needs verification. A future implementation must use a scrubbed synthetic environment, fixed allowed selections, bounded response bytes/deadlines/concurrency, fail-closed provenance and visible-row checks, and safe child failure/shutdown. An internal launch flag must not enable public reads or bypass readiness. `PUBLIC_RUNTIME_VERIFIED=False` and the existing public 503 contracts stay unchanged throughout candidate qualification.

This is the smallest plausible reuse of current components, not an assertion that a second runtime process is already safe on F1. No new service/database/queue/framework is proposed.

## Concrete implementation blockers

1. `container/entrypoint.sh` public mode uses `env -i` and starts only `public_preview_app.py`; it intentionally starts no C# Worker. Its private branch is unsuitable: it requires private replay/provider configuration and suppresses public presentation. `LocalSyntheticChart.IsAllowed` rejects Linux and Azure. Simply relaxing that guard would violate the explicit separation required by C2B-24.
2. Live generation needs process ownership, admission, CPU/resource, denial/health and recovery qualification. The controlling business strategy retains G-B0–G-B7 and advanced WP07 ON HOLD. C2B-24 does not waive those requirements or authorize a replacement lifecycle/security contract. A generic subprocess timeout, loopback listener or two-child shell is not evidence of those properties.
3. An immutable precomputed alternative avoids request-time generation but requires its own frozen handoff format, offline export/promotion authority, integrity and resource bounds. The strategy explicitly distinguishes finite offline proof from production/export/promotion. No snapshot/export is performed or implicitly selected here.
4. Effective installed runtime, credential removal, private-filesystem protection, internal endpoint reachability and F1 resource costs on each actual image remain unverified. ARM settings and an existing local screenshot cannot close them.

These are contract prerequisites, not a request for a larger implementation phase. Therefore no speculative Azure adapter or startup modification is included. Owner disposition must select a narrowly scoped qualification/contract mapping for the live synthetic-only source, or explicitly authorize the immutable handoff alternative. Do not resume advanced WP07 automatically.

## Site-specific deployment assessment — no changes made

| Requirement | WP03 URL | WP05 URL |
|---|---|---|
| Proposed role | Confirm whether public showcase replica is intended | Confirm whether public showcase replica is intended |
| Runtime/startup prerequisite | Newly reviewed multi-runtime image; explicit synthetic-only startup and environment scrub; existing blank override must not conceal a second command | Independently verify the same prerequisites; do not infer qualification from WP03 |
| Exposure | Retain 8501 as sole public container port; prove internal source inaccessible | Same, independently verified |
| Settings | Separate reviewed allowlist; no inherited provider/replay/owner credentials; no readiness changes in this phase | Same; independently review current configuration |
| Permission | Future authorized image publisher/pull access and narrowly scoped site configuration/deployment access | Separate resource-group/site permission; WP03 permission is insufficient evidence |
| Rollback | Record prior digest and safe configuration before any future mutation; restore pinned image/config under explicit authority, verify unavailable routes | Independently record/restore this site's own configuration; never copy credentials between sites |

No new paid resource is proposed. Existing F1 does not establish sufficient CPU/memory/headroom or zero incremental cost for an unqualified configuration. No Azure configuration, RBAC, registry, GitHub Actions, slot, startup or readiness changes were made.

Before any future deployment approval: accept the precise security/readiness applicability mapping; authorize literal implementation paths; prove synthetic-only source and credential/private-file isolation; qualify bounded execution/admission/failure cleanup; verify all twelve selections and frozen provenance/SMA; measure payload/resource/health limits; demonstrate real 1D/30D browser off/on/off; prove public 503 remains until separate activation authority; qualify each site's runtime/image/ingress and rollback. Public publication requires its own explicit decision after independent readiness review.

## Verification and reproduction

Planning-only change: no new C# behavior; no build/restore or generator requalification. Focused existing Python checks run from `python/presentation`:

```powershell
..\..\.venv\Scripts\python.exe -B -m unittest test_local_synthetic_chart test_public_preview_shell test_public_preview_api.PublicApiTests.test_six_exact_routes_and_liveness_readiness_are_distinct -v
```

Result: 7 passed, exit 0 (0.706 seconds). Bare-mode Streamlit `missing ScriptRunContext` warnings occurred. These tests establish existing local gate/default shell/public-boundary behavior, not Azure runtime or browser acceptance. The UI metadata test does not prove viewport retention.

Existing local demonstration commands remain those in merged PR #345: build the Infrastructure test project into an owned temporary OutDir with `--no-restore`, set `DOTNET_ENVIRONMENT=Development`, launch its Worker DLL with `--local-synthetic-chart`; separately set `AIQ_LOCAL_CHART=1` and run `public_preview_app.py` bound to `127.0.0.1:8502`. Use Windows PowerShell 5.1. Do not use these Windows-only commands as Azure startup commands. Never stop an unrelated process to free a port.

No Linux/container smoke, new browser acceptance, Azure runtime execution, or production qualification performed in C2B-24. Earlier C2B-23 browser off/on/off remained incomplete. Owner accepts zoom/pan reset on SMA toggle as a documented local UX limitation; no compatibility fix is pursued.

Exact changed-file inventory: this assessment only. The owner authority file and all other incoming untracked documents are preserved outside the PR.

`PUBLIC_RUNTIME_VERIFIED=False`. Public activation/deployment **NO-GO**. Advanced WP07 **ON HOLD**. Leave the planning PR **OPEN/UNMERGED**. This document proves no public runtime readiness.

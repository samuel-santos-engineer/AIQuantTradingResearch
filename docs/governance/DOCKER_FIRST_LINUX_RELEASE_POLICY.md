# Docker-first Linux release qualification policy

**Status:** Proposed governance change — owner review required.  
**Scope:** AIQuantTradingResearch public application and Release 1.14 chart showcase; subsequent public-runtime release candidates unless explicitly superseded by an owner-approved governance change.  
**Target:** Azure App Service **Linux F1** custom container.  
**Effective:** Only after this PR is reviewed and merged; this document alone does not authorize deployment or override existing release gates.

## Decision and intent

The canonical runtime and release-qualification environment is the **local Linux Docker container image intended for deployment**. Windows is an optional developer workstation, not a supported production runtime or certification target. Minimize operational complexity: qualify the actual deployable artifact rather than maintaining parallel Windows and Linux runtime paths.

## Binding rules

1. **Container-only release acceptance.** Functional, integration, chart/browser, security-boundary, filesystem and startup acceptance evidence for the public runtime MUST be collected against a locally executed Linux Docker image. Host-only Windows results may inform debugging, but MUST NOT count as release certification.
2. **One immutable deployable image.** Build and qualify one image, record its immutable digest, and promote **that exact digest** to the registry and Azure Linux F1. Do not rebuild, mutate or substitute image contents between qualification and deployment. Registry promotion and deployment remain separately authorized actions.
3. **Single-process presentation by default.** Prefer the existing Streamlit/Plotly presentation runtime, with no live C# worker or second HTTP service in the Azure request path. C# calculations may run **offline during an authorized artifact-generation/build step**; the running public container reads only the approved immutable output.
4. **Self-contained immutable synthetic data.** Where compatible with licensing, provenance, and explicit owner authorization, package a deterministic, versioned, checksummed synthetic OHLCV/SMA handoff inside the image as read-only assets. Verify manifest, hashes, fixed selections, bounds, permissions, and failure-closed behavior. Do not package provider credentials, private replay, warmup-only rows or unapproved data.
5. **Linux runtime requirements.** Validate the deployed entrypoint, port 8501, non-root/read-only permissions where applicable, startup and health behavior, bounded memory/CPU/PID use, concurrent access, and no unplanned outbound network or secondary processes. Local F1-like limits are **screening evidence only**, not proof of Azure F1 capacity.
6. **Browser acceptance on the container.** Run a real browser against the locally running Linux image to verify candles, volume, supported interval/range selectors, Technical Analysis popover, SMA(20) off/on/off, hover, zoom, pan and toolbar. A browser may execute on a test host if it exercises the **containerized Linux application** through explicitly controlled local-only ingress; this does not make the host a certification runtime. Document any narrowly scoped ingress and confirm it is not published publicly.
7. **No Windows parity mandate.** Do not require native Windows execution, Windows-specific packaging, Windows service behavior or Windows browser acceptance as conditions for public release. Existing offline Windows development evidence can remain historical; it does not substitute for Linux-image evidence.
8. **Provenance and repeatability.** Record source commit, Dockerfile/build inputs, image digest, dataset manifest/hashes, commands, test results, runtime constraints, observed failures and cleanup. Preserve the same data and application bits through promotion; document any unavoidable environment configuration separately.
9. **Per-site Azure verification.** After separately authorized deployment, check the exact digest, configuration, startup/health, resource behavior, browser UX, API status and rollback for **each** Azure F1 site. Passing local Docker qualification does not imply public runtime verification.
10. **Minimalism and change control.** Do not add new services, paid Azure resources, Windows compatibility work, new provider dependencies or production data access merely to satisfy this policy. Exceptions require an explicit owner decision, a documented rationale and a narrowly scoped PR.

## Release gates and non-authorizations

This policy changes **where qualification occurs**, not **who may authorize release**. It does not relax existing G-B0–G-B7, WP07, artifact export/promotion, security, provenance, licensing or readiness controls. If a gate conflicts with a proposed packaging or test technique, stop and request owner disposition rather than bypassing it.

For the current Release 1.14 candidate, PR #346 (documentation) and PR #347 (immutable handoff implementation) remain independently reviewable. Their acceptance is not implied by this governance PR. Linux browser acceptance and authorized immutable-artifact packaging/promotion still need evidence. Until expressly changed through the existing process: `PUBLIC_RUNTIME_VERIFIED=False`, public data routes remain `503 DATASET_NOT_READY`, Azure publication is **NO-GO**, and advanced WP07 is **ON HOLD**.

## Review checklist

- [ ] Owner approves Linux Docker image as the exclusive public-runtime certification target
- [ ] Owner accepts that Windows host/native parity is not a release requirement
- [ ] Existing protected governance and release gates are preserved
- [ ] CI/release instructions are aligned in a separately scoped implementation PR if needed
- [ ] No image build, Azure change, merge, deployment or artifact promotion occurred in this governance PR

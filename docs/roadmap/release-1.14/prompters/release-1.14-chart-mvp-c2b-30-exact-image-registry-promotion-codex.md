# C2B-30 — Exact-image registry publication and digest readback (Codex authority)

**Owner authorization:** C2B-29 approved; C2B-30 authorized for bounded registry promotion. **Not authorized:** merge, Azure mutation/deployment, staging/production activation, public data enablement, new paid resources, or changes to protected gates. `PUBLIC_RUNTIME_VERIFIED=False`; deployment **NO-GO**; advanced WP07 **ON HOLD**.

## Objective

Publish the **already locally qualified** C2B-29 Linux/amd64 image to the existing approved registry without rebuilding, regenerating, or changing any bytes, and prove by registry readback that the published Linux/amd64 platform manifest, config and layers are identical to the certified candidate. If any prerequisite fails, **stop without publishing** and document the blocker.

## Authoritative inputs (verify, do not assume)

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- C2B-29: PR #350, owner-approved, reported head `e909b3ef1ade83959f41dd15600937e3fdb4ed9d`, **open/unmerged unless independently verified otherwise**.
- Local certified Linux/amd64 **platform manifest** digest `sha256:55d142b57d0df9953f01350263369ee2ecd9a66019d1844d1f424ea91e0999d5` (**D**).
- Certified config digest `sha256:27ec5f7297205ccc4d0ba5c7987effc2c8aa710904f6850ea63d16d51e69f56d`.
- Recorded saved image archive SHA-256 `700b49d2f0bacb7db74b5520faabed3b94519355cd54daabc3f6be11bf0ea7a0`.
- Immutable bundle: 13 files, 656176 bytes; candidate manifest SHA-256 `007b2c8a6ccec19ec54976b4baf735812b648d79294e6465df884c68537d3069`.
- C2B-29 evidence/contract and machine-readable layer descriptors in PR #350; review them as the authority for exact source, archive, paths, image format and build provenance. **Do not infer archive path from this prompt.**
- Existing registry target, subject to verification and least-privilege authorization: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch`. Existing Azure configured digest `sha256:144de4a9c393f07d2249f450b9d5ab2af34f36b941897a31bd20d72ac325959a` is **not** a qualified rollback image and must not be overwritten or repointed.
- PRs #346, #347 and #349 are independent, historically open/unmerged; PR #350 cherry-picked #347 commits. Do not duplicate, silently merge, rebase, delete or modify their branches. Confirm current status and document integration/review dependencies.
- Effective governance: `docs/governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md` (PR #348 merged), plus G-B0–G-B7 and all existing export/promotion/security/owner controls.

## Preflight — mandatory stop conditions

1. Read C2B-29 evidence, changed-file inventory, OCI descriptors and promotion/eligibility requirements. Verify owner approval scope and the registry repository/account permissions. **No secret values in output, logs or PR.**
2. Confirm the **exact retained archive exists**, hash equals the recorded archive hash, is a complete valid OCI/Docker artifact as documented, and can be mapped unambiguously to platform `linux/amd64` manifest **D**, config digest and every expected layer descriptor. Distinguish image ID/config digest, platform manifest digest, enclosing OCI index digest and mutable tags. Do not substitute one for another.
3. Independently check archive file sizes and digests, 13-member immutable data inventory, manifest anchor, image startup/read-only/non-root attributes and the recorded C2B-29 acceptance provenance. Confirm any required export/promotion review/approval is actually satisfied. C2B-29 approval is not permission to waive G-B gates.
4. Check GHCR repository existence, write authorization and nonsecret package visibility policy. Choose a **new immutable, unique candidate tag** (e.g. `c2b30-<short-D>`), not `latest`, not an existing deployment tag; verify it is unused and avoid overwrite. Registry push must not alter Azure config or trigger automatic deployment; verify there is no automatic deployment hook or workflow before proceeding.
5. Confirm the available OCI transfer tooling can preserve the exact manifest/config/layers **without a build or manifest conversion**. If it would change the platform manifest digest, do not proceed. Never use `docker build`, `buildx build`, `docker commit`, the C# producer or artifact regeneration to compensate.
6. Record clean scope, UTC timestamps, source commit, image provenance, sanitized tool versions and exact commands. Avoid accessing protected private replay/provider material. Do not touch owner files or change global Git settings.

## Bounded authorized execution

- Prefer a content-preserving copy/import/push of the retained archive into the **existing GHCR package** using approved, least-privilege credentials. Keep credentials out of shell history, arguments, screenshots, logs and commits. No new registry or service.
- Publish only the previously qualified Linux/amd64 content. If an OCI index is needed, explicitly document it and preserve platform digest **D**. A new index digest must **never** be presented as D.
- If registry client rewrites the platform manifest or any descriptor, stop; mark promotion **FAIL/UNKNOWN**. Do not claim equivalence based on visual chart behavior or matching filesystem content alone.
- Read back from GHCR using registry API/OCI inspection, ideally independently from the upload mechanism: resolve the new tag, identify the `linux/amd64` platform manifest, and compare its raw digest **D**, config digest and complete ordered layer descriptors (digest, size, media type) to C2B-29 evidence. Recheck image asset manifest and metadata through safe, non-deploying inspection where possible.
- Test registry pull/read access under the intended future deployment authorization model **without altering Azure**; if permissions cannot be verified safely, record UNKNOWN and stop. Do not expose the artifact publicly without explicit approval of package visibility and data-publication rights.
- **Do not** run `az webapp ...` write commands, edit container settings, trigger restarts, deploy, expose chart/API data or modify production/staging resources. No release activation, no tag repoint of existing deployed images.

## Evidence and PR

Create a **new scoped C2B-30 evidence PR**, open/unmerged, with only necessary sanitized Markdown/JSON evidence and, if needed, a minimal reproducible nonsecret script. Preserve all existing owner files. Include:

- Exact registry repository and unique tag, registry URL and UTC push/readback times; package visibility without credentials.
- Original archive SHA-256, platform **D**, config digest, layer-by-layer comparison, index digest if any, media types, sizes, immutable data manifest anchor and source/provenance chain.
- Sanitized command transcript/tool versions, checks/negative cases, explicit pass/fail/unknown decisions and cleanup/retention of the source archive.
- Current PR #346/#347/#349/#350 dependency and review disposition; no unauthorized merge.
- Proof no rebuild, regeneration, deployment hook, Azure mutation, public activation or owner-file modification occurred.
- Future staging prerequisites: separate owner authorization, immutable `repository@D` reference, site-specific effective digest/entrypoint verification, 503 JSON boundary (Azure currently 200 HTML), approved unavailable rollback target, F1 resource/cold-start/abuse/session screening and independently reviewed G-B gates.
- Clear final decision: **GO/NO-GO for owner review of separate staging authority**, not staging approval. `PUBLIC_RUNTIME_VERIFIED=False`, Azure deployment NO-GO, WP07 ON HOLD.

## Failure handling

If archive missing, digest differs, OCI descriptors are incomplete, registry rights/visibility are uncertain, promotion eligibility is unresolved, a deployment automation could fire, or registry readback differs: **STOP**. Do not reconstruct or rebuild the image, do not weaken gates and do not silently choose a new candidate. Record the precise blocker and proposed smallest next action in the evidence PR. If publication already occurred before a readback failure, do not deploy; document exact artifact/tag and request owner decision before any deletion or remediation.

## Completion report

Report PR link/head/status, archive/manifest/config digests, registry tag and readback verdict, complete descriptor match verdict, whether any bytes were published, any blocked prerequisites, cleanup, and an explicit statement that **Azure staging and production were not modified**. Do not claim release certification beyond the verified local C2B-29 boundary.

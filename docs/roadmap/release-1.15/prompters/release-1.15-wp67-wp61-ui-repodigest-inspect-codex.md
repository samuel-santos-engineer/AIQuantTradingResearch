# Codex prompt — Release 1.15 WP67: bounded WP61 UI RepoDigest observation

## Authority and objective

Owner approved this next Codex work package following the merge of PR #462. Base the work on current `main` containing squash commit `27875afd437846a1eaeae84ea7470ddf955eb1b6`. The sole live operation authorized by this prompt is **one read-only, exact-image Docker inspection requesting only image ID and RepoDigests**, after the same local-host/context preflight used in WP66. No other Docker daemon/image operations are authorized. Do not merge your PR.

Objective: determine whether the already accepted WP61 local UI image advertises one or more safely formatted RepoDigests for the existing GHCR application repository. This is metadata discovery, **not** proof of publication, manifest/index/config equivalence, Azure readiness, or a mandate to republish.

## Fixed image and context

- Accepted WP61 local Docker image ID: `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`.
- Exact Docker context: `desktop-linux`, on the same previously qualified original Windows host and existing local Docker Desktop Linux named-pipe endpoint.
- Existing GHCR repository name for **allowlist-only classification**: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch`.
- Do not treat local image ID as an OCI manifest/index/config digest. Do not infer registry availability from a RepoDigest alone.

## Before the single inspect

1. Confirm `main` and the WP66 evidence are present, the worktree is clean, and PR #462 is merged. Create a new focused WP67 branch; do not modify or reconcile unrelated PR #457.
2. Review merged `eng/docker/release-1.15/wp65_local_image_metadata.py`, its tests, WP64 OCI verifier, and `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`. Reuse existing Python-only parsing, direct-argument invocation and previously qualified owned-process capture. Do not introduce Go, packages, tools, CI dependencies, or a new framework.
3. Prepare and run **offline synthetic tests first** for the exact fixed projection and strict, fail-closed RepoDigest classification. Confirm no untrusted/raw digest strings, credentials, full metadata, command arguments, Env, labels, history, layers or paths can leak into evidence. Preserve WP66 historical observations unchanged.
4. Preflight original host identity (comparison only, do not print private identifier), absence of Docker override variable names `DOCKER_HOST`, `DOCKER_TLS_VERIFY`, `DOCKER_CERT_PATH` (do not read values), and verify current context and endpoint via the same two bounded local metadata-only context commands as WP66. No context switch or daemon fallback. Stop on mismatch.

## The one authorized daemon operation

Invoke exactly one command with a direct argument array (no shell), explicit context and accepted image ID:

```text
docker --context desktop-linux image inspect sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 --format '{"Id":{{json .Id}},"RepoDigests":{{json .RepoDigests}}}'
```

This is the **only** approved image/daemon operation. The projection contains only root `Id` and `RepoDigests`; no nested Config traversal or whole-object formatting. Apply the established **10-second request deadline**, **1 MiB combined stdout/stderr cap**, bounded owned-process/pipe cleanup, numeric exit capture and fixed sanitized stderr categories. **No retry** if it fails, including no alternate template, context, image ID or Docker call. Do not display or persist raw output or raw stderr. Classify only the fixed exact ID, a bounded count of allowed RepoDigests, and allowlisted full `repository@sha256:<64 lowercase hex>` references if permitted by existing safe-output policy; reject/withhold any other string rather than echo it. If unexpected/unsafe references are present, record a safe categorical outcome without their values. A null/empty RepoDigests field must be distinguished from an inspection failure, and neither proves that the image was never published.

## Interpret conservatively

- If successful and the exact ID matches, report whether safe RepoDigests were observed. RepoDigests are Docker-local metadata and **do not alone attest** current registry availability or the OCI index/child/config chain.
- If the exact ID mismatches, malformed data appears, or any bounded capture/preflight fails, stop and record `UNVERIFIED` or a precise safe failure category. Do not make up a missing image or publication conclusion.
- Do not claim the runtime UI contract (UID, port, entrypoint), OCI provenance, Azure equivalence or public readiness from this projection.
- `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false` throughout.

## Prohibited operations

No image export/save, build, pull, push, retag, container/volume operations, image inventory, daemon start, extra image inspection, registry HTTP/CLI access, Azure reads or writes, credential reads, private runtime probes, public probes, deployment or release flag changes. No Go installation/execution or toolchain expansion. Do not create new licensing or governance approval gates.

## Deliverable and acceptance

- Create a **small focused PR**, leave **open/unmerged**. Prefer evidence-only if existing helper/tests can safely support this observation; if a minimal Python-only helper/test change is necessary, justify and tightly scope it. Do not change unrelated files.
- Preserve prior WP65/WP66 evidence verbatim; append WP67 with exact operation count, timestamp, fixed image/context, numeric exit/duration, sanitized result, cleanup accounting and clear UNKNOWN/UNVERIFIED boundaries. No raw stdout/stderr or sensitive values.
- Run the existing WP65, WP64 and setting-name parser/collector Python suites before and after the permitted inspect, plus any new synthetic tests. Report real counts and skips accurately; validate syntax, UTF-8/LF, Markdown and `git diff --check`.
- In PR description state exact base/head, paths changed, tests, one-inspect result, prohibited operations not performed, and smallest **separately approvable** next step. Do not request or perform a registry lookup without fresh explicit owner authorization.

Stop and report rather than broaden scope if any precondition cannot be met.

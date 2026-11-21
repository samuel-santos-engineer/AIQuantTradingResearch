# Codex work package — Release 1.15 WP71: bounded WP61 RepoDigest shape observation

## Authority and objective

Owner authorizes this **single, bounded, read-only Docker observation** under the exact conditions below, as part of executing this prompt. No other Docker, registry, Azure, credential, runtime, deployment, or network operations are authorized. The objective is to apply the merged WP70 offline classifier to one fresh, exact-image `RepoDigests` response and record **fixed structural category counts only**, to learn whether an explicit-port shape is recognized. The previous WP69 discarded value cannot be retroactively reclassified. A category result is not proof of registry publication or OCI provenance.

## Repository and prerequisites

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Verify current `main` contains merged PR #465 squash `a88e8a4c880a38e969b6017dcd24037bd649b339` and merged PR #466 squash `c04fad45c59d5988e79d3c56dc501dc8ea594725`. Start a clean focused branch from fresh `main`; preserve owner worktree changes.
- Accepted WP61 UI local image ID: `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`.
- Use the merged Python-only helper `eng/docker/release-1.15/wp65_local_image_metadata.py`, its `repodigest_command` and `project_repodigests` / `reference_category` implementation. Do not change the formatter, allowlist, or actual image ID.
- Preserve WP65–WP70 evidence verbatim in `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`. In particular, WP69's one `MALFORMED_REFERENCE` under the old grammar remains historical and unchanged.

## Preflight: no daemon operation unless qualified

1. Confirm the **original authorized Windows host** through the previously established in-memory exact host comparison. Do not print, store, or reveal its private identifier. If the comparison mechanism is unavailable, stop; do not substitute a new host.
2. Confirm `DOCKER_HOST`, `DOCKER_TLS_VERIFY`, and `DOCKER_CERT_PATH` are absent **by name only**, without reading their values. If present, stop.
3. Permit at most the same **two local context metadata commands** previously qualified: `docker context show` and `docker context inspect desktop-linux --format '{{json .Endpoints.docker.Host}}'`. Verify current context is `desktop-linux` and endpoint is the exact existing local Docker Desktop Linux named pipe. Do not print or persist endpoint details. No context switching, daemon start, fallback, `docker info`, `docker version`, image inventory, or other probes.
4. If any preflight fails, stop without executing the image inspect; report only safe fixed failure categories.

## Exactly one authorized image inspection

Use the existing helper-generated direct argument array (no shell):

```text
docker --context desktop-linux image inspect sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 --format '{"Id":{{json .Id}},"RepoDigests":{{json .RepoDigests}}}'
```

- **At most one** daemon/image inspect, no retry or fallback, no second formatter or richer metadata request.
- Enforce **10-second request deadline**, **1 MiB combined stdout/stderr cap**, numeric exit capture, sanitized fixed error categories, and the previously qualified suspended owned Windows Job / bounded pipe reader cleanup. Verify zero owned active processes, closed pipes, and no orphan descendants.
- Process output **transiently in memory** using the existing exact-ID, duplicate-key, size and count guards. Do not print, persist, log, echo, or attach raw stdout/stderr, unapproved reference strings, repository names, registry hosts, paths, credentials, or partial lists. Do not claim secure erasure of process memory.
- Record only exact-image-ID match, numeric exit, duration, cleanup status, classifier result/state, and the bounded fixed counts for `EXPECTED_REPOSITORY_DIGEST`, `OTHER_REPOSITORY_DIGEST`, `UNSUPPORTED_REFERENCE_SHAPE`, `MALFORMED_REFERENCE`, and `UNEXPECTED_VALUE_TYPE`, or existing `NULL`, `EMPTY`, `UNKNOWN`, `COUNT_LIMIT_STOP` states. If the collection is not entirely the exact allowlisted application repository, **withhold all reference values**, including otherwise safe members.
- Even if `EXPECTED_REPOSITORY_DIGEST` appears, do not conclude registry reachability, index/manifest/config digest kind, publication, deployment equivalence, or Azure health. A valid-looking `UNSUPPORTED_REFERENCE_SHAPE` is a conservative syntax category, not an authoritative OCI validation.

## Validation and evidence

- Run the existing Python-only suites **before and after** the single possible observation:
  - `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q`
  - `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q`
  - `python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q`
- Run the helper with no arguments to confirm `PLAN_ONLY` and zero Docker operations. These tests do not authorize extra live inspections.
- Prefer an **evidence-only append** to `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`. Change helper/tests only if a concrete defect prevents correct fixed-category reporting, and then justify each change and use synthetic tests only.
- Report actual test counts, failures/skips, observed operation counts, safe classification, limitations and next decision. Check syntax, UTF-8/LF/no BOM, diff --check, exact changed-file scope and historical evidence preservation.
- Create one focused GitHub PR against current `main`; **leave OPEN and UNMERGED**. Provide PR URL, base/head SHAs, changed paths, validation results, and precise observed command count.

## Hard prohibitions and next decision

No registry HTTP, GHCR lookup, Azure commands/reads, credentials, image export, build, pull, push, tag, container/volume operations, deployment, public probe, Go installation or execution, or additional Docker inspection. No change to auth/TLS/storage/ports or API-first/UI-second deployment order. Do not infer a rebuild or publication requirement from an unexpected reference.

Any further inspection or exact registry metadata verification requires **separate explicit owner approval**. If no safe immutable mapping emerges, propose the smallest **offline** diagnostic refinement rather than looping on Docker calls. Preserve:

- `OCI provenance: UNVERIFIED`
- `Publication necessity: NOT PROVEN`
- `PUBLIC_RUNTIME_VERIFIED=False`
- `liveAttemptReady=false`

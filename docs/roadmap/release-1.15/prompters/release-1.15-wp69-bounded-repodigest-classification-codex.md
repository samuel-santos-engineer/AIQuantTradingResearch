# Codex work package — Release 1.15 WP69: bounded WP61 RepoDigest structural observation

## Authority and objective

The owner requests this next Codex work package after squash merge of PR #464 at `3ea0804e9819c8d073131bd79d699cb417e7b7ac`. This work package authorizes **at most one** narrowly bounded, read-only Docker `image inspect` of the exact accepted WP61 local UI image, solely to classify `RepoDigests` using the merged WP68 Python-only classifier. No other daemon/image operations, retries, fallbacks or richer projections are authorized. This is **not** authority for registry access, Azure access, deployment, credentials, export, rebuild, pull, push, tag, container or volume operations, public probes, or Go/toolchain installation.

## Repository and prerequisites

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Verify fresh `main` contains squash commit `3ea0804e9819c8d073131bd79d699cb417e7b7ac` (merged #464). Work on a focused clean branch, open **one PR against current main**, and **leave it open/unmerged**.
- Preserve the accepted WP61 UI local image ID: `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`.
- Preserve WP66 successful identity/Linux-amd64 evidence and WP67's real `UNSAFE_REFERENCE_WITHHELD` outcome. WP67 raw values were discarded; **do not retroactively reclassify them**. WP68 classifications were synthetic only.
- Prefer an **append-only** evidence update to `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`. Change helper/tests only if a narrowly justified bug fix is essential; otherwise leave code unchanged. Do not touch PR #457 or alter API/UI behavior.

## Host qualification and exact operation

1. On the **original qualified Windows host only**, verify the original host identity privately/in memory, without printing host identifiers. Verify Docker override environment variable **names** (`DOCKER_HOST`, `DOCKER_TLS_VERIFY`, `DOCKER_CERT_PATH`) are absent; do not read or output their values.
2. Permit exactly two **local context-metadata** commands for qualification, with no context changes: `docker context show` and `docker context inspect desktop-linux --format '{{json .Endpoints.docker.Host}}'`. Require current `desktop-linux` and the exact previously qualified local Docker Desktop Linux named-pipe endpoint. If any preflight fails, **STOP with zero image/daemon operations**.
3. After successful qualification, permit **exactly one** read-only daemon/image operation, using a direct argument array (no shell):

   ```text
   docker --context desktop-linux image inspect sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 --format '{"Id":{{json .Id}},"RepoDigests":{{json .RepoDigests}}}'
   ```

4. Use the previously qualified bounded, owned Windows process/Job and pipe-capture mechanism. Enforce a **10-second request deadline**, **1 MiB combined stdout/stderr cap**, numeric exit code, fixed sanitized error categories, and verified descendant/pipe cleanup. No retry, alternate context, daemon start, broader inspect, Docker version/info, image inventory, export or fallback. If process qualification or bounded cleanup cannot be guaranteed, **do not execute the inspection**.
5. Keep raw stdout/stderr and all unapproved reference strings **transient in memory only**, and clear them after classification. Never print, persist, log, attach, or echo raw RepoDigests, other repository names, userinfo, credentials, error text, Docker endpoint/host identifiers or partial unapproved refs. Never treat inspection success as publication verification.

## Classification and privacy

- Parse the transient exact-ID response with the merged WP68 `project_repodigests` classifier. Preserve duplicate-key rejection, 1 MiB cap, 16-reference cap, strict expected-repository allowlist and fail-closed semantics.
- Record only safe fixed categories and counts: `EXPECTED_REPOSITORY_DIGEST`, `OTHER_REPOSITORY_DIGEST`, `MALFORMED_REFERENCE`, `UNEXPECTED_VALUE_TYPE`, or the established `NULL`, `EMPTY`, `COUNT_LIMIT_STOP`, `UNKNOWN` states. The exact WP61 ID may be recorded.
- If **every** reference belongs to the unchanged expected GHCR application repository, only those already-allowlisted immutable `repository@sha256:<64 lowercase hex>` references may be recorded. If any entry is other-repository, malformed or unexpected-type, **withhold the entire reference collection**, including expected-repository entries; report only fixed category counts and `UNVERIFIED`.
- A conservative grammar may classify some legitimate but unsupported OCI reference syntax as malformed. Report this limitation; do not infer corruption, credential presence, nonpublication or need to republish.
- `RepoDigests` are local metadata, **not** proof of registry reachability, current publication, OCI index/child/config chain, deployment equivalence or Azure runtime health. Do not reinterpret local image ID as an OCI digest.

## Validation and evidence

- Run existing offline suites before and after the potential observation: WP65 metadata helper tests, WP64 OCI provenance tests, and setting-name parser/collector tests. Report actual counts, failures, skips and exit codes; do not double-count repeated tests as distinct cases.
- Confirm default helper still reports `PLAN_ONLY` with no Docker side effects. Verify Python syntax, UTF-8/LF/no BOM, Markdown consistency, exact changed-file scope and `git diff --check`.
- Append a dated WP69 evidence section documenting preflight outcome, whether the single inspect actually ran, numeric exit/duration, bounded capture/cleanup, exact-ID match, **safe** category counts or safe allowlisted references only, redaction, test results and operation counts. Preserve earlier evidence verbatim.
- If the host is unavailable or any preflight fails, record `NOT_RUN` and stop. Do not simulate a real observation or substitute synthetic fixtures.

## Next decision, not authority

If an allowed immutable GHCR mapping is observed, propose a **separately approved, exact, read-only registry metadata verification** for index/manifest/config provenance. If only other-repository or malformed categories are seen, propose the smallest offline interpretation or narrowly scoped next evidence step. Do not rebuild or publish by assumption. Preserve accepted API-first/UI-second deployment order and existing authentication, TLS, storage and ports.

## Deliverables

Open one focused PR **without merging it**. Provide PR URL, base/head SHAs, changed paths, test counts, preflight and inspect outcome, sanitized category summary, any safe allowlisted references, and a precise operation inventory. State explicitly: **OCI provenance UNVERIFIED; publication necessity NOT PROVEN; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`**. No registry, Azure, credential or deployment activity is permitted.

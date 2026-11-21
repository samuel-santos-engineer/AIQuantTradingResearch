# Codex prompt — Release 1.15 / WP73: Authoritative RepoDigest grammar review (offline implementation)

## Starting point and authority

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

PR #468 (WP72) was squash-merged into `main` at `d72f7e5d417b5c6805ea3de7fadd1354931ddf1e`. Verify live `main` and the relevant merged evidence before beginning. Start from current `main` in a clean isolated branch, preserving the owner's worktree and all unrelated changes. Do not merge any PR; publish one focused PR **OPEN/UNMERGED** for owner review.

This work package authorizes **offline research and local source/test/documentation changes only**, plus ordinary Git/GitHub PR operations. No Docker commands (including context checks), no image inspection, no registry/GHCR HTTP requests, no Azure commands or reads, no credentials, no runtime/public probes, no provider/API calls, no image build/pull/export/push/tag, no deployment, no container/volume operations. No new package installations, downloads, dependency upgrades, or network-dependent tests. Public documentation browsing to identify a canonical reference grammar is allowed only as a narrowly scoped documentation lookup if a browsing tool is available; never query a registry or send image/repository-specific metadata. If authoritative material cannot be obtained, record that limitation and **do not change the classifier**. Do not use real or previously discarded RepoDigest values.

## Objective

Review a canonical, source-backed Docker/distribution reference grammar and identify whether existing WP68/WP70 conservative classification incorrectly conflates recognizable unsupported syntax with malformed syntax. Prior WP72 documented a 16-case synthetic matrix, 79 passing offline tests and no classifier changes. WP71's real observation remains **one `MALFORMED_REFERENCE` under the then-current grammar**, with no raw value retained; it cannot be retroactively reclassified.

The goal is to reduce ambiguity **without increasing exposure** or pretending that local metadata proves registry publication.

## Work plan

1. Inspect `eng/docker/release-1.15/wp65_local_image_metadata.py`, `test_wp65_local_image_metadata.py`, the existing WP65–WP72 evidence appendix at `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`, and any already-vendored canonical grammar material. Record exactly which source/grammar revision is available and which portions are authoritative. Prefer Docker/distribution `reference` grammar and upstream OCI image reference documentation; distinguish normative syntax, implementation behavior, and local conservative policy. Do not claim that an excerpt alone establishes complete validation.
2. Compare the canonical rules to synthetic WP72 cases: `localhost`, single-label registries, explicit ports, bracketed IPv6, tags with digests, repository separators, uppercase hosts/paths, canonical versus leading-zero ports, schemes/userinfo, and digest algorithm/hex constraints. Record which judgments are sourced and which remain uncertain. In particular, do not assume every syntactically parseable reference is allowed for emission.
3. Decide on the **smallest justified code change**. It is acceptable and preferred to leave production unchanged if authoritative evidence is insufficient. If changing categories, keep a fixed bounded vocabulary, preserve existing output shape compatibility where feasible, and add synthetic regression cases for each changed interpretation. Never emit raw unexpected reference strings, partial matching values, hashes, lengths, positions, snippets, userinfo or endpoint information. A new category must be necessary and non-sensitive, not an excuse to expose detail.
4. Preserve the **exact allowlisted emission** rule: only a collection entirely matching `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:<64 lowercase hex>` may yield `safeRepoDigests`. Any other, unsupported, malformed, unexpected-type or mixed collection must remain `UNVERIFIED` and withhold all values. Preserve exact WP61 ID checks, duplicate-key rejection, 1 MiB raw cap, 16-entry cap, null/empty/unknown states, sanitized errors, default `PLAN_ONLY`, existing inspect argv/formatters, 10-second bounds, Windows Job/pipe cleanup and no retries/fallbacks.
5. Expand synthetic tests only as justified. Verify mixed-collection redaction, category totals and count limits, identical aggregate output for different raw inputs in the same categories, and zero process/network actions on import/default execution. Do not introduce a dependency or change production Docker execution behavior.
6. Append a clearly labeled **WP73** section to the existing evidence document, preserving every prior byte of WP65–WP72 chronology. Include source identification and version/commit/date if known, direct links or citation details, a compact sourced-versus-uncertain grammar comparison, exact implementation decision, synthetic test results, safety/operation inventory and limitations. Historical WP67/WP69/WP71 results are not reinterpreted. If browsing was unavailable or evidence insufficient, state that explicitly rather than inventing authority.
7. Run the existing offline suites:
   - `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q`
   - `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q`
   - `python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q`
   - `python -B eng/docker/release-1.15/wp65_local_image_metadata.py` (must remain `PLAN_ONLY`, `dockerOperations=0`)
   - Python syntax compilation, UTF-8/LF/no BOM, Markdown link/table sanity, prior-evidence prefix comparison, `git diff --check`, exact path-scope review.
   Report actual distinct tests and skips; do not claim tests ran if the environment prevents execution.
8. Publish one narrow PR against current `main`, leave **OPEN/UNMERGED**. Report base/head SHAs, changed paths, test evidence, exact source provenance, remaining uncertainty and explicit inventory of operations not performed. Do not automatically proceed to a real inspection, registry verification, rebuild or deployment.

## Decision criteria

A good outcome may be **documentation plus tests only**. Do not change the classifier merely to force progress. If authoritative source material justifies a narrowly safer distinction, implement it with synthetic coverage and conservative withholding. If it does not, describe a precise next offline question rather than proposing repetitive Docker inspection.

## Invariants

- OCI provenance **UNVERIFIED**; publication necessity **NOT PROVEN**.
- `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.
- No inference that local Docker image ID is an OCI index, manifest, child or config digest.
- No inference of registry reachability, publication, Azure health, or public UI readiness.
- Preserve API-first/UI-second deployment order and existing auth/TLS/storage/ports.
- PR remains open until owner explicitly authorizes merging.

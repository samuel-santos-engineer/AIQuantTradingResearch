# Codex work package — Release 1.15 WP68: offline RepoDigest structural classifier

## Authority and objective

Owner has authorized creation of this Codex prompt following squash merge of PR #463 at `9f6fa36cee1282d17016835bfb7da6a0787b661f`. Work **offline only** on the existing Python RepoDigest projection/classifier. Improve the classification of a *synthetic or explicitly injected* `RepoDigests` collection to distinguish expected-repository immutable references, syntactically valid other-repository immutable references, malformed references, and unexpected types. **Do not inspect the actual image again** in this work package. No Docker commands of any kind (including context or version), no registry HTTP, no Azure commands or reads, no credential access, no Go/toolchain, no exports/builds/pulls/pushes/tags/containers/volumes, no deployment or public probes. No new libraries or governance gates.

## Repository and boundaries

- Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
- Confirm fresh `main` includes merged #463 commit `9f6fa36cee1282d17016835bfb7da6a0787b661f` before starting. Use a clean focused branch and open one PR against current main; **leave open/unmerged**.
- Preserve accepted WP61 local image ID `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69` and WP66 identity/platform success. WP67 real inspection returned exit 0, exact ID, but `UNSAFE_REFERENCE_WITHHELD`; its raw references were discarded. Do not infer their contents, repository, syntax, credential status, or publication.
- Keep scope to `eng/docker/release-1.15/wp65_local_image_metadata.py`, `eng/docker/release-1.15/test_wp65_local_image_metadata.py`, and an appended entry in `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`, unless a strictly necessary scoped change is justified. Preserve all previous evidence verbatim.

## Implementation

1. Retain the exact-ID guard, duplicate-key rejection, 1 MiB raw cap, bounded reference count, fail-closed handling, default PLAN_ONLY/no daemon behavior, and exact existing application-repository allowlist for **emitted reference values**. Do not broaden what values may be returned.
2. Add a small, deterministic **Python-only** structural classification over the transient list. Report fixed categories only: `EXPECTED_REPOSITORY_DIGEST`, `OTHER_REPOSITORY_DIGEST`, `MALFORMED_REFERENCE`, `UNEXPECTED_VALUE_TYPE`; retain `NULL`, `EMPTY`, `COUNT_LIMIT_STOP`, `UNKNOWN` or equivalent existing semantics. Use a deliberately conservative anchored grammar for registry/repository@sha256:64 lowercase hex, and reject userinfo, URL schemes, whitespace, tags without digests, incomplete digests, and ambiguous forms. No parsing by network or filesystem. If mixed categories occur, return aggregate flags/counts or a fixed mixed category; never leak reference strings or partial unapproved refs. Cap output and avoid echoing raw exception text.
3. Distinguish **structurally valid** other-repository references from **confirmed publication**: neither local RepoDigests nor any classifier result proves registry reachability, manifest/index/config kind, deployable equivalence, or Azure state. Maintain `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false`.
4. Keep existing `safeRepoDigests` output only when the **entire collection** is valid and belongs to the exact allowed application repository. Any mixed, malformed or other-repository collection must withhold all reference values. Preserve conservative `UNVERIFIED` result for such cases.
5. Add synthetic regression tests for exact allowed references, structurally valid other repositories, mixed lists, malformed/userinfo/scheme/tag/incomplete/uppercase or ambiguous values, nonstring values, duplicate JSON keys, wrong image ID, missing/null/empty, oversized input and count cap, and redaction. Test default CLI/import causes no Docker or network operations. Do not use raw WP67 output or assert that the original observed values fall into a newly introduced category.
6. Keep the existing inspect command unchanged and **do not execute it**. Do not build a live switch or introduce a dependency.

## Validation and evidence

Run the existing Python-only WP65, WP64, and setting-name parser/collector unittest suites, plus default helper PLAN_ONLY. Verify syntax, UTF-8/LF, diff --check, and changed-file scope. In the appended WP68 evidence, report actual test counts and any failures, describe the synthetic-only classifier and strict redaction behavior, and state clearly that WP67 `UNSAFE_REFERENCE_WITHHELD` cannot be retroactively reclassified. Report that no actual Docker/registry/Azure operations occurred.

## Next decision, not authority

After owner review/merge, the *possible* next step is a **separately authorized** single bounded exact-image `RepoDigests` inspection using the same qualified original host/context, 10-second request limit, combined 1 MiB cap, numeric exit/sanitized error categories, owned-process cleanup, and no retries/fallback. Do not execute or request credentials as part of WP68. If a safe immutable mapping is eventually observed, propose a separately approved exact registry metadata verification rather than assuming publication or rebuilding.

## Deliverables

Open one focused PR and provide its URL, base/head SHAs, changed files, test counts, and clear statement: **offline classifier only; no new Docker inspection; OCI provenance UNVERIFIED; publication necessity NOT PROVEN; `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`**. Do not merge the PR.

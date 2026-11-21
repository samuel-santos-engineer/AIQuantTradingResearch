# Codex prompt — Release 1.15 WP74: bounded RepoDigest diagnostic design (offline only)

Repository: `samuel-santos-engineer/AIQuantTradingResearch`.

## Authority and baseline

Start from fresh `origin/main` containing squash merge of PR #469 at `3c340b8fecef248f48c8613fc0044dd791d9602f`. Work on a clean isolated branch; preserve the owner's worktree and unrelated PRs. Do not merge your PR. This prompt authorizes only offline source review, local synthetic tests, narrow code changes if proven necessary, documentation, and Git/PR publication. **No Docker commands (even context checks), registry/GHCR calls, Azure, credentials, deployment, public runtime probes, image builds/pulls/pushes, or access to historical raw RepoDigest strings.** No external package installation. Do not infer publication necessity.

## Goal

Design and, only if defensible, implement a **minimal source-backed aggregate diagnostic refinement** that can distinguish a meaningful subset of currently locally rejected RepoDigest reference shapes without disclosing raw values. Use the authoritative versioned sources reviewed in WP73 (`distribution/reference` v0.6.0 and `opencontainers/go-digest` v1.0.0). Do **not** port a general parser or loosen the exact repository/digest emission allowlist. The existing `MALFORMED_REFERENCE` category means *local-policy rejection*, not upstream invalidity.

## Tasks

1. Read `eng/docker/release-1.15/wp65_local_image_metadata.py`, `eng/docker/release-1.15/test_wp65_local_image_metadata.py`, and `docs/roadmap/release-1.15/prompters/release-1.15-wp65-local-image-metadata.md`, including WP69–WP73 appendices. Verify the baseline and immutable command/timeout/capture/cleanup contracts.
2. Write a small decision table identifying candidate **non-overlapping** fixed structural subcategories that would actually affect a later decision (for example, source-backed authority/name versus digest-algorithm constraints). Explain for each its exact syntactic predicate, upstream basis, ambiguity, privacy impact, and why aggregate counts add value beyond WP70/WP73. Avoid speculative categories or signals that are too broad to be actionable.
3. Select **at most one** narrowly scoped new category or refinement if the evidence supports it. If no category has a concrete decision benefit, leave production code unchanged and record a no-change decision. Never claim a category proves Docker parser acceptance, image validity, OCI provenance, or registry publication.
4. Preserve the exact expected immutable-reference allowlist and all-or-nothing `safeRepoDigests` withholding. Preserve aggregate-only outputs, no sample strings, substrings, positions, lengths, hashes, raw stderr/stdout, or derived identifying data. Maintain duplicate-key, exact image-ID, type, 1 MiB raw, 16-reference, null/empty/unknown, PLAN_ONLY and zero-network/process default safeguards. Do not alter Docker argv, Windows Job/pipe lifecycle, or 10-second deadline.
5. Add synthetic-only regression tests: positive and negative predicate boundaries; overlap/precedence against expected, other, unsupported-port, malformed and unexpected-type categories; mixed-reference withholding; equal-aggregate-output noninterference across different synthetic values and ordering; count/size caps; and no extra output keys. If no code change, test the strongest documented invariant rather than inflating cases without benefit.
6. Run and report the three existing offline suites (WP65, WP64, setting-name), default PLAN_ONLY invocation, syntax compilation, UTF-8/LF/no BOM, `git diff --check`, and exact diff scope. Distinguish distinct tests from subtests; report failures honestly.
7. Append a WP74 evidence section to the existing WP65 metadata evidence document; preserve all historical evidence verbatim. Explain the decision and limitations. Never retrospectively reclassify WP67/WP69/WP71 observations or imply the actual discarded value's shape is known.
8. Open a focused GitHub PR against current `main`, leave it **open and unmerged**, and report base/head SHAs, changed paths, test results, source references, risk assessment, and the smallest separately approvable next step.

## Acceptance / stop conditions

- A demonstrable decision benefit and precise conservative predicate are prerequisites to changing production code. **No-change is a valid successful result.**
- No real image inspection is authorized in WP74. Do not propose repeating a live observation unless a new diagnostic has first passed synthetic/privacy tests and the owner separately approves a single bounded operation.
- Keep `OCI provenance: UNVERIFIED`, `Publication necessity: NOT PROVEN`, `PUBLIC_RUNTIME_VERIFIED=False`, `liveAttemptReady=false`.
- Preserve Docker-first and dual-process governance, API-first/UI-second sequencing, and existing authentication, TLS, storage, ports and image provenance boundaries.

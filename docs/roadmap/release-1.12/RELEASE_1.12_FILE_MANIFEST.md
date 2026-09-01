# Release 1.12 File Manifest

## Planning-authority paths

The initial planning mutation is limited to these three new files:

- `docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md`
- `docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md`

The existing `docs/roadmap/release-1.12/prompters/` authority/bootstrap prompts are control records and are not silently counted as implementation payload.

## Future WP ownership

No future implementation path is authorized by this manifest. Each WP authority must replace this section with a literal path allowlist before mutation. Expected ownership is:

- WP01: planning/architecture contract paths only.
- WP02: container/runtime paths only.
- WP03: deployment automation and evidence paths only.
- WP04: persistence paths only.
- WP05: runtime configuration/secret-safe automation paths only.
- WP06: public presentation/health paths only.
- WP07: validation and no-bypass test paths only.
- WP08: documentation/runbook/release acceptance paths only.

Forbidden: unrelated source/tests, package or schema changes, Azure auth/profile material, credentials/API keys, private certificates, machine-local configuration, caches/build outputs, production deployment claims, tags, releases, and Release 2.0/ Product Release 1.11 reassignment.

## Acceptance evidence

Every future candidate must prove exact path count and set, no duplicates, no origin overlap, no forbidden paths, whitespace cleanliness, secret scan, relevant tests, no-bypass invariants, strict `$0.00`, and preserved `1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3` sequencing.

## Mutation boundary

This manifest is planning governance only. It does not authorize implementation, Azure/Docker/provider operations, staging, commits, pushes, tags, GitHub Releases, or merge.

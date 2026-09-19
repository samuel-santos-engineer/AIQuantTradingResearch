# Release 1.13 File Manifest

## Planning-authority mutation set

This authority may add or modify only these governance records:

- `docs/project/ROADMAP.md`
- `docs/roadmap/release-1.13/RELEASE_1.13_DEFINITION.md`
- `docs/roadmap/release-1.13/RELEASE_1.13_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.13/RELEASE_1.13_FILE_MANIFEST.md`
- `docs/roadmap/release-1.13/prompters/release-1.13-historical-market-visualization-provider-abstraction-governance-authority-luna.md`

`README.md` is forbidden and must remain unchanged. Historical Release 1.12 records are excluded because they are historical authority records, not current-roadmap surfaces.

## Future ownership

No source, test, dependency, configuration, deployment, or UI path is authorized now. Each Release 1.13 WP must supply its own exact literal path allowlist before implementation. Expected ownership is WP01 contract/selection/test-plan records; WP02-WP04 provider/adapter/cache/read-model paths; WP05-WP06 presentation/read-model consumer paths; WP07 validation/deployment-evidence paths; and WP08 documentation/runbook/acceptance records.

## Forbidden paths and artifacts

Forbidden: root `README.md`; historical Release 1.12 records; credentials, API keys, certificates, machine-local configuration, caches/build outputs; Azure profile material; unrelated packages; database migrations; Docker/GHCR publication artifacts; tags/releases; Release 1.14 implementation; and Release 2.0 implementation.

## Validation

Before a planning PR is proposed, prove the exact path set, Markdown formatting, no secrets, unchanged README, all `release-1.13-` prompter naming, preserved Release 2.0/2.1/2.2/2.3 identities, no Product Release 1.11 resurrection, and coherent sequence `1.10 -> 1.12 -> 1.13 -> 1.14 -> 2.0`.

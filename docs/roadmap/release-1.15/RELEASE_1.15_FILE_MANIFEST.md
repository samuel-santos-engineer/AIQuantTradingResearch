# Release 1.15 File Manifest — Planning Scope and Future Ownership

## Current authorized publication set
This roadmap planning PR creates only the following Release 1.15 documentation files; the list is **not** an implementation mutation allowlist:
- `docs/roadmap/release-1.15/RELEASE_1.15_DEFINITION.md`
- `docs/roadmap/release-1.15/RELEASE_1.15_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.15/RELEASE_1.15_FILE_MANIFEST.md`
- `docs/roadmap/release-1.15/RELEASE_1.15_ACCEPTANCE.md`
- `docs/roadmap/release-1.15/RELEASE_1.15_ISSUE_REGISTER.md`
- `docs/roadmap/release-1.15/prompters/release-1.15-wp01-codex-preflight.md` through `release-1.15-wp08-codex-preflight.md`

## Release 1.14 structural compatibility
Preserve the uppercase `RELEASE_1.15_*.md` root planning artifacts and lowercase hyphenated `prompters/release-1.15-wpNN-...-codex-*.md` prompts. Do not copy Release 1.14 synthetic artifacts, historical workstream IDs, snapshots or milestone numbers as new Release 1.15 evidence.

## Future proposed path ownership (NOT authorized mutations)
| WP | Proposed component ownership | Literal paths required at implementation |
| --- | --- | --- |
| WP01 | Documentation, architecture/contracts, feasibility test plan | Resolve from current repository tree and owner approval |
| WP02 | Docker build/supervisor/runtime, Linux security tests | Enumerate actual Docker/entrypoint/CI/test files |
| WP03 | C# ingestion, provider adapter, SQLite storage/recovery and tests | Enumerate existing/new C# paths after repository inspection |
| WP04 | C# indicator/regime engine and independent tests | Enumerate algorithm and test paths |
| WP05 | C# localhost presentation API, Python contract client, schema/fixtures | Enumerate server/client/fixture/test paths |
| WP06 | Streamlit/Plotly chart, UI state and browser tests | Enumerate existing presentation and UI test paths |
| WP07 | Container qualification, CI evidence, F1 readiness documents | Enumerate scripts/workflows/evidence files |
| WP08 | Acceptance ledger, runbook, evidence closeout | Enumerate approved documentation paths |

## Shared-file and preservation policy
WP02 owns container runtime contract before WP05 integration; WP03 owns private storage and research continuity before WP04; WP05 owns snapshot schema before WP06; WP07 integrates without unreviewed contract rewrites. Existing Release 1.14 code and tests are preservation/regression baselines unless an implementation PR explicitly lists exact mutations. Keep historical evidence and digest records unchanged. Do not infer source paths from old manifests.

## Verification
Root `.gitattributes`, `AGENTS.md` and `docs/governance/REPOSITORY_LF_LINE_ENDINGS_POLICY.md` control LF normalization. Check each changed text blob for CRLF, run `git diff --check`, and disclose any pre-existing line-ending inconsistencies without bulk rewrites.

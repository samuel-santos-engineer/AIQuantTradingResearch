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

## WP01 contract-closeout assessment index

Documentation-only supplement after merged planning PR #378 and feasibility PR #379. These paths index a blocked closeout assessment; they are not production mutation authority or owner acceptance:

- [WP01 contract closeout](RELEASE_1.15_WP01_CONTRACT_CLOSEOUT.md)
- [WP01 D1-D7 decision register](RELEASE_1.15_WP01_DECISION_REGISTER.md)
- [WP01 downstream qualification matrix](RELEASE_1.15_WP01_DOWNSTREAM_QUALIFICATION_MATRIX.md)

Exact publication scope: these three new documents and this index addition only. Existing governance, implementation, tests, historical evidence, acceptance statuses and issue dependencies remain unchanged. `PUBLIC_RUNTIME_VERIFIED=False`; issue #367 remains open and WP02 is not started. UTF-8/LF applies to every changed text file.

## WP01 foundational proposal amendment index

- [Detailed D1/D3/D4/D5 candidate contracts and owner decision sheet](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md)
- [Owner decision-resolution prompt](prompters/release-1.15-wp01-codex-foundational-contract-decision-resolution.md), explicitly authorized for inclusion after it was found untracked; wording retained, UTF-8/LF normalized.

This amendment changes the existing register/closeout/qualification matrix and this index, adds only the proposal specification and the authorized prompt, and preserves all22 downstream owners/statuses. It proposes no automatic FROZEN designation, implementation, dependency, test/CI execution, provider/Azure operation, merge or issue closure. `PUBLIC_RUNTIME_VERIFIED=False`.

## WP01 contract correction review index

- [Owner-authorized contract correction and owner-review prompt](prompters/release-1.15-wp01-codex-contract-correction-and-owner-review.md), included after explicit authorization of the untracked prompt; wording preserved as UTF-8/LF.

Documentation-only corrections update the existing proposal, register and closeout; qualification rows/owners and acceptance statuses remain unchanged. No application/test/governance or historical evidence mutation is authorized. `PUBLIC_RUNTIME_VERIFIED=False`.

## Azure existing-F1-plan infrastructure assessment - 2026-10-23

- [Executed conditional provisioning authority](prompters/release-1.15-codex-azure-existing-f1-plans-additional-apps.md)
- [Read-only capacity assessment and provisioning stop](RELEASE_1.15_AZURE_EXISTING_F1_PLANS_CAPACITY_EVIDENCE.md)

Infrastructure evidence only: no resource creation, deployment, architecture/governance or WP02 implementation-plan change. Separate evidence PR; preserve all pending local work. UTF-8/LF applies; `PUBLIC_RUNTIME_VERIFIED=False`.

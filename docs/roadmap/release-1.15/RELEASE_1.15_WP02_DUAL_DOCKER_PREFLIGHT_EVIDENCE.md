# Release 1.15 WP02 - Dual Docker preflight evidence

Date: 2026-10-23. **PLANNING DELIVERABLE; SOURCE IMPLEMENTATION BLOCKED pending merged governance and literal-path approval.** UTF-8/no BOM/LF/final newline applies. `PUBLIC_RUNTIME_VERIFIED=False`.

## Repository and PR observations

- Original checkout branch feat/release-1.15-wp02-runtime-isolation at6f76697707e2363cc52820e0c805172815b92159. Dirty manifest plus six untracked WP02/Azure/prompt artifacts inventoried and hash-snapshotted. None overwritten, staged, reset, stashed or silently imported into implementation. Separately named new plan supersedes the old supervisor proposal only as intended D1-B planning, not an edit of that owner file.
- PR #381 verified MERGED at968ed80302feb33e32ad65e191971691537362bf; live main matches. #368 OPEN and revised to D1-B/API-key direction. PR #382 OPEN/CONFLICTING at17436b937580c8a8b4c8e9d177399bb498822b9f; its seven governance/roadmap changes were inspected, not treated as merged policy.
- Separate isolated worktree merged origin/main into the #382 branch using a normal merge commit. Conflict only in RELEASE_1.15_FILE_MANIFEST.md. Resolution preserves main's complete Azure evidence/authority index and #382's complete dual-container section; its reference to open #381 corrected to merged. Merge commitdee115556d450a0717a110e6131bafd58b830254 pushed without force to existing PR #382. No PR merge performed.
- Second clean worktree from current main publishes this four-file planning PR. It does not contain #382 governance changes; adoption is a dependency, not silently bundled authorization. Executed prompt preserved as an artifact and indexed.

## Exact scope and result ledger

[Literal preflight plan](RELEASE_1.15_WP02_DUAL_DOCKER_PREFLIGHT.md) proposes eleven future Docker/fixture/test/runner paths and exactly four current documentation paths. Two direct-entrypoint non-root images; no C/native/Bash dual-process supervisor. Production D4/auth integration is deferred to WP05; fixture key format/300s overlap are proposals for review, not accepted new contract values.

| Category | Observed result |
| --- | --- |
| Main and PR381/382/issue live state | Read via GitHub CLI and fetched refs |
| PR382 merge conflict | Resolved and normal merge commit pushed; PR still OPEN/unmerged |
| Azure evidence retention | Main manifest prefix retained; merged Azure evidence/prompts unchanged |
| Source implementation | NOT STARTED; governing PR unmerged |
| Product tests/builds/Docker/TLS/security fixtures | NOT RUN; zero test passes/digests/resource benchmarks claimed |
| API-key/TLS threat model | Documented proposal; no executed transport/security evidence |
| Azure/app settings/secrets/provider/registry/deployment | NOT CALLED / no mutation |
| Original dirty work | Preserved by SHA-256/status verification |

One diagnostic console print hit Windows cp1252 encoding on existing Unicode; repository bytes were not changed by that read failure. Subsequent validation uses UTF-8 file reads/byte scans. A guessed existing public_preview_shell.py path was absent and was not added; actual run-only test paths were resolved from the repository.

## Outstanding obligations

PR #382 must be owner-reviewed/merged before D1-B source implementation. Scoped path/native-free design approval remains required by existing WP02 preflight. WP03 must prove writer exclusivity/durable recovery across deployments; WP05 owns production API-key/HTTPS/schema/rotation integration; WP07 actual F1 ingress/TLS/cold start/resource/secret-rotation and paired-image verification. No Azure key creation/read/rotation/display, no provider/HMAC secret transfer to UI, no readiness/acceptance claim. `PUBLIC_RUNTIME_VERIFIED=False`.

Documentation validation: four exact changed paths, 20 relative Markdown targets, all five run-only paths, UTF-8/no BOM/LF/final newline and git diff --check PASS. Governance manifest retains the full merged-main prefix plus D1-B section; merged Azure artifacts byte-preserved. Original dirty checkout status/hashes unchanged. Staged blobs separately checked before commit; no executable tests run.

## Owner continuation and reconciliation - 2026-10-24

PR #382 merged as49966b38e1228d4eb336cb1676d4ac3e41c716a4; D1-B governance is now effective. Owner explicitly authorizes merge of this documentation plan and its eleven exact implementation paths in a separate implementation PR. Earlier unmerged/approval-pending labels are historical preflight observations, superseded by this note. Manifest reconciliation preserves both merged governance/Azure entries and this plan's index. Review confirms fixture-only scope, unchanged D3/D4/D5 semantics, no new dependency or production API authority. No tests/images/security behaviors are claimed by plan acceptance. Rotation/key-format candidates remain isolated fixture choices for WP05 review. `PUBLIC_RUNTIME_VERIFIED=False`; no deployment/merge of the future implementation PR is authorized.

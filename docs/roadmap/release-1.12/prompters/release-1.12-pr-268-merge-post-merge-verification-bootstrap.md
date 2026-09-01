# GPT-5.6 Terra Bootstrap — PR #268 Merge + Post-Merge Verification

Execute:
`release-1.12-pr-268-merge-post-merge-verification-authority-terra.md`

**Selected execution model: GPT-5.6 Terra**

Repository:
`C:\projects\github\AIQuantTradingResearch`

Target:
`#268 — Docs: publish Release 1.12 planning artifacts`

Expected pre-merge identity:
- Open
- non-draft
- base `main`
- head `docs/release-1.12-planning-artifacts`
- head SHA `917207333f133b961f72b94525a17ed0d0aae954`
- original parent `20a8fccd6e7a5b895e717f946f4501edd7ab8ffa`
- exact payload 3 Markdown planning artifacts

Exact paths:
- `docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md`
- `docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md`

Freshly reconcile before mutation. If head SHA or payload differs, BLOCK.

This authority authorizes exactly one PR merge mutation plus the Git synchronization/verification required afterward. It does not authorize WP02 implementation or closing #261.

After merge:
- record merge SHA
- fetch origin
- synchronize local `main` with `origin/main`
- prove 0/0 ahead-behind
- prove authoritative Git payload exactly 3/3
- verify all three files exist on main
- verify #260 Closed/Done, #261 Open/Todo, milestone #63 Open expected 7/1
- verify Project, Initiative-1.11, Release 1.10, tags/releases, Azure, Docker/GHCR, provider state unchanged
- verify staged count 0 and unrelated `prompters/` remains excluded

Required final:
`RELEASE 1.12 — THREE PLANNING ARTIFACTS PUBLICATION MERGE: PASS`

`RELEASE 1.12 WP02 — PUBLICATION PREREQUISITE: SATISFIED`

`RELEASE 1.12 WP02 — EXECUTION AUTHORITY: READY TO RESUME`

`RELEASE 1.12 PR #268 — MERGE + POST-MERGE VERIFICATION AUTHORITY COMPLETE`

Blocked:
`RELEASE 1.12 PR #268 — MERGE + POST-MERGE VERIFICATION AUTHORITY BLOCKED`

For authenticated execution, provide exact bounded PowerShell batches with purpose, classification, expected mutations, requested stdout/stderr/exit codes, then STOP.

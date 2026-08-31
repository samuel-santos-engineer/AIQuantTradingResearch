Execute `phase-5-milestone-governance-baseline-reconciliation-authority-luna-codex-prompt.md`.

Use **GPT-5.6 Luna**.

This is a **read-only governance reconciliation**.

The user manually changed the other open numbered Release milestone prefixes from `Phase 4` to `Phase 5`.

Your task:
1. inspect current GitHub milestone state;
2. absorb the user's legitimate Phase 5 prefix changes as the new canonical baseline;
3. preserve milestone #62 as:
   `Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`;
4. preserve `Initiative-1.11 ≠ Product Release 1.11`;
5. verify the numbered release sequence remains `1.10 → 2.0 → 2.1 → 2.2 → 2.3`;
6. verify #252 Closed and #253–#257 Open under #62 unless legitimate later progress exists;
7. verify Project #2 taxonomy and Release assignments;
8. scan repository documentation for stale Phase 4 references;
9. report whether a separate documentation amendment authority is required.

Do not mutate GitHub.
Do not modify repository files.
Do not stage/commit/push.
Do not execute Azure.
Do not alter releases, issues, milestones, Project fields, tags, or PRs.

All mutation counts must be zero.

If the GitHub state is coherent, emit:
`PHASE 5 MILESTONE PREFIX BASELINE: ACCEPTED`

End only with the exact COMPLETE or BLOCKED terminal from the authority.

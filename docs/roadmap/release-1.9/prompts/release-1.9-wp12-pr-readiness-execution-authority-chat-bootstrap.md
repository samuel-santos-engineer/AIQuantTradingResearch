Execute `release-1.9-wp12-pr-readiness-execution-authority-codex-prompt.md` as the sole authority using GPT-5.6 Terra.

Read `RELEASE_1.9_WP12_CLOSURE_PR_READINESS_GIT_GITHUB_LIFECYCLE_CONTRACT_AUTHORITY.md` completely first. WP12 is fixed as role A / PR-READY-ONLY.

Perform a strictly non-mutating PR-readiness audit: verify the contract's R1–R5 classification against the current dirty worktree, compute the exact hypothetical R1 PR include set without staging, inspect all R1 diffs for provenance/scope, prove `Directory.Build.local.props` and all signing secrets/local configuration are excluded, run the existing approved security gate, execute the contract-required technical and focused validation, audit docs and residue, and prove final Git/GitHub preservation.

Do not stage, commit, branch, push, create/update/merge a PR, mutate #237, close milestone #58, tag, or publish a Release. #237 must remain Open/Backlog and milestone #58 Open even if readiness passes.

On success report the exact R1 include manifest, exclusions, hypothetical staging/PR package as informational evidence only, zero repository/Git/GitHub mutations, and require a separate narrow `WP12 #237 lifecycle / post-readiness transition authority`.

End with the exact terminal marker from the authority.

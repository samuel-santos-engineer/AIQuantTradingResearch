Execute `release-1.9-pr-creation-execution-authority-codex-prompt.md` as the sole authority using GPT-5.6 Terra.

Read `RELEASE_1.9_FINALIZATION_PR_GIT_MILESTONE_TAG_RELEASE_CONTRACT_AUTHORITY.md` completely first. Finalization model is F-SPLIT. This authority covers only the PR-creation slice: exact R1 verification, approved security preflight, branch preparation, exact-path staging, commit, push, PR creation, and PR read-back.

Use only the frozen Release 1.9 R1 manifest; do not use `git add .` or `git add -A`. Preserve `Directory.Build.local.props`, signing secrets/local configuration, generated/test/runtime artifacts, and any excluded local work. Stop if R1 drifts or R5 appears.

Follow the contract's exact branch, commit, push, PR base/head/title/body/state rules. After PR creation, verify the PR changed-file set matches the intended R1 set and that #233–#237 remain Closed/Done with milestone #58 still Open.

Do not merge the PR, close the milestone, tag, publish a GitHub Release, delete branches, or mutate issues/Project items.

End with the exact terminal marker from the authority.

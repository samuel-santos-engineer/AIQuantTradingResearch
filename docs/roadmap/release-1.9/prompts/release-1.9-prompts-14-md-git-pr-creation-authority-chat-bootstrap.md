Execute `release-1.9-prompts-14-md-git-pr-creation-authority-codex-prompt.md` as the sole authority using GPT-5.6 Terra.

Discover the exact currently unmerged Markdown files under:

`docs/roadmap/release-1.9/prompts`

Expected count: exactly 14.

Before any Git mutation, freeze and print the exact 14-path manifest. Every path must be under that folder and end in `.md`. If the count is not exactly 14, BLOCK.

Review the 14 files only for scope/classification: they must be Release 1.9 prompt/authority/governance documentation. Do not rewrite them.

Create/use branch `docs/release-1.9-prompts` from current `origin/main`, stage exactly the frozen 14 paths, create one commit `docs: add remaining Release 1.9 prompt authorities`, push only that branch, and create one PR targeting `main` titled `Docs: Add remaining Release 1.9 prompt authorities`.

After PR creation, use authoritative paginated file enumeration to prove the PR contains exactly the frozen 14 Markdown paths and nothing else.

Preserve `v1.9.0`, the published Release, milestone #58, milestone #59, #233–#237, PR #240, and unrelated user work.

Do not force push, push tags, merge, delete branches, or mutate Release/milestone/issues/Project state.

STOP after PR creation and 14/14 frozen-payload verification. End with the exact terminal marker.

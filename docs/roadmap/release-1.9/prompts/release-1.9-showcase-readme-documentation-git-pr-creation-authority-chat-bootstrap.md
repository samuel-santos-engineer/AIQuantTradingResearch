Execute `release-1.9-showcase-readme-documentation-git-pr-creation-authority-codex-prompt.md` as the sole authority using GPT-5.6 Terra.

Package exactly these two already accepted documentation paths:

- `README.md`
- `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

Create/use branch `docs/release-1.9-showcase-readme` from current `origin/main`, stage exactly those two paths, create one commit with preferred message `docs: showcase completed Release 1.9`, push only that branch, and create one PR targeting `main` with preferred title `Docs: Showcase completed Release 1.9`.

After PR creation, read back the PR and prove the frozen payload is exactly 2/2 paths using the paginated files API or equivalent authoritative source.

Preserve `v1.9.0`, the published GitHub Release, milestone #58, #233–#237, and unrelated user work. Do not force push, push tags, merge, delete branches, or mutate milestone/issues/Project/Release state.

STOP after PR creation and frozen-payload verification. End with the exact terminal marker.

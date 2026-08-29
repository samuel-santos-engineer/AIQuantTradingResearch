Execute `release-1.9-showcase-readme-documentation-git-pr-creation-authority-v2-codex-prompt.md` as the sole authority using GPT-5.6 Terra.

Use the newly accepted current README baseline:
- Current closed milestone = Release 1.9 / #58.
- Current accepted milestone = Release 1.10 / #59.
- reviewed table-formatting changes accepted.
- Python badge, completed 1.8/1.9 descriptions, showcase-guide link, deterministic/replay disclosure, and governed .NET → canonical JSON → Python/Streamlit boundary remain accepted.

Package exactly:
- `README.md`
- `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

Create/use `docs/release-1.9-showcase-readme` from current `origin/main`, stage exactly those two paths, create one commit `docs: showcase completed Release 1.9`, push only that branch, and create one PR targeting `main` titled `Docs: Showcase completed Release 1.9`.

After PR creation, read back the PR and prove the frozen payload is exactly 2/2 paths using the paginated files API or equivalent authoritative source.

Preserve `v1.9.0`, the published GitHub Release, milestone #58, milestone #59, #233–#237, and unrelated user work. Do not force push, push tags, merge, delete branches, or mutate milestone/issues/Project/Release state.

STOP after PR creation and frozen-payload verification. End with the exact V2 terminal marker.

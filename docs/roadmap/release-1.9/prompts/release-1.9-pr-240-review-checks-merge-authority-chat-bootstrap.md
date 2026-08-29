Execute `release-1.9-pr-240-review-checks-merge-authority-codex-prompt.md` as the sole authority using GPT-5.6 Terra.

Review PR #240 — `Docs: Showcase completed Release 1.9`.

Require:
- base `main`;
- head `docs/release-1.9-showcase-readme`;
- frozen head SHA `77fcbc59b01b12626e0b49c09a9fa30bc872116f`;
- exactly one commit;
- exactly two changed paths:
  - `README.md`
  - `docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md`

Review the diff against the newly accepted README baseline: Current closed milestone 1.9/#58, Current accepted milestone 1.10/#59, accepted table formatting, Python badge, completed 1.8/1.9 descriptions, showcase link, and deterministic/replay governed .NET → JSON → Python/Streamlit semantics.

Require all repository-required checks/reviews and mergeability gates to pass. Recheck `v1.9.0`, the published Release, milestone #58, milestone #59, and #233–#237 before merge.

If every gate passes, merge PR #240 using repository policy. Do not bypass protection, rewrite the branch, delete the branch, push tags, or mutate Release/milestone/issues/Project state.

After merge, perform idempotent verification of the merged PR, exact 2/2 payload, resulting `origin/main`, merged README/guide state, and unchanged Release 1.9 lifecycle state.

End with the exact authority terminal marker.

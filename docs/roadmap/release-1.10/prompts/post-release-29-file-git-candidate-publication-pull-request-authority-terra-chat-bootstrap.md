Execute `post-release-29-file-git-candidate-publication-pull-request-authority-terra-codex-prompt.md`.

Use **GPT-5.6 Terra**.

This authority publishes the currently intended **29 local files** as a dedicated branch and GitHub PR.

Binding release baseline:
- Release 1.10 is published/lifecycle-complete.
- `v1.10.0` resolves to `eb9601596d9a9dd68f1f8a7c963906a76e5a2833`.
- Milestone #59 is Closed.
- #242–#249 remain Closed/Done.
- The most recent local report had staging 0, 2 tracked signing-related changes, and 25 untracked local/control files = 29 intended merge files.

The user explicitly states all 29 files should be merged.

Before any staging:
1. independently inventory Git state;
2. freeze a literal sorted 29-path manifest;
3. classify every path;
4. reject secrets/private-key material/temp outputs;
5. verify authoritative current `main`;
6. validate the exact candidate.

Then:
- create a dedicated branch;
- stage exactly 29/29;
- create one candidate commit;
- push only that branch;
- open one PR against `main`;
- verify PR changed files are exactly 29/29.

Do NOT edit candidate content.
Do NOT merge the PR.
Do NOT mutate milestone/issues/Project.
Do NOT tag/version.
Do NOT alter the existing GitHub Release.
Do NOT force-push.

If the observed candidate is not exactly 29 paths, or validation/content-integrity fails, BLOCK without trying to repair it.

End only with the exact COMPLETE or BLOCKED terminal.

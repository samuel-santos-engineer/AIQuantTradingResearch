Execute `release-1.10-publication-manifest-literal-list-repair-resumption-artifact-classification-reconciliation-authority-codex-prompt.md`.

Use **GPT-5.6 Luna**.

This is planning/governance reconciliation only.

Known state:
- publication branch already safely re-anchored at:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- `origin/main` matches.
- current raw worktree: 107 paths = 21 tracked + 86 untracked.
- prior intended canonical candidate: 103 paths = 21 tracked + 82 untracked.
- manifest's claimed 103-path literal list is malformed: it actually contains 110 entries, with entries 104–110 being Git CRLF warning text rather than paths.
- two newly created Terra publication-resumption authority prompt files are unclassified.
- prior Git-publication authority pair is included.
- prior remote-base reconciliation authority pair is excluded.
- #242–#249 Closed/Done.
- milestone #59 Open, 0 open / 8 closed.
- no staging, commit, push, PR, issue/Project, milestone, tag, or release mutation occurred.

Repair the manifest to a path-only literal list whose declared count exactly equals its actual entries. Classify the two Terra resumption prompt files IN or OUT. Freeze exact raw/candidate/excluded arithmetic, preserve base/parent `5cc2...`, preserve or explicitly reconcile the validation requirement, and produce a deterministic Terra staging handoff.

Allowed mutations: Release 1.10 manifest, and execution plan only if required.
Git mutations: ZERO.
GitHub mutations: ZERO.

Do not repeat/re-anchor the branch. Do not stage, commit, push, create/update PR, merge, close milestone #59, tag/version, or publish a GitHub Release.

End only with the exact COMPLETE or BLOCKED terminal.

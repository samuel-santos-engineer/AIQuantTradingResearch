Execute `release-1.10-git-candidate-publication-repaired-manifest-terra-authority-codex-prompt.md`.

Use **GPT-5.6 Terra**.

Binding repaired state:
- canonical base and required candidate parent:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- repaired manifest is the sole staging authority.
- raw inventory at Luna handoff: 109 = 21 tracked + 88 untracked.
- canonical publication candidate: exactly 103 = 21 tracked + 82 untracked.
- candidate prompt artifacts: 70.
- six execution-control files are explicitly excluded by the manifest:
  remote-base reconciliation pair, Terra publication-resumption pair, and manifest-repair authority pair.
- candidate presence 103/103; duplicates 0; non-path entries 0.
- staging empty.
- #242–#249 Closed/Done.
- milestone #59 Open, 0 open / 8 closed.

First verify manifest integrity and run the full re-anchor/publication validation frozen in the execution plan. Then create/reuse the governed Release 1.10 branch from `5cc2...`, stage exactly the manifest's literal 103 paths, commit once, push only that branch, and create/reuse exactly one PR against main.

Do not stage any of the six exclusions. Do not edit candidate content. Do not merge, force-push, close milestone #59, tag/version, or publish a GitHub Release.

End only with the exact COMPLETE or BLOCKED terminal.

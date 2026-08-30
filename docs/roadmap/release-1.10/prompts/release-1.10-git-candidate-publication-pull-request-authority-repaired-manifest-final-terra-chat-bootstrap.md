Execute `release-1.10-git-candidate-publication-pull-request-authority-repaired-manifest-final-terra-codex-prompt.md`.

Use **GPT-5.6 Terra**.

Binding publication state:
- canonical base and required candidate parent:
  `5cc2d17d3d05f84911eca98d3b7b7a9b33f55a33`
- repaired manifest is the sole authoritative staging boundary.
- canonical candidate: exactly 103 paths = 21 tracked + 82 untracked.
- candidate prompt artifacts: 70.
- all publication-control exclusions are exactly those persisted in the repaired manifest.
- `OPEN_TELEMETRY_SELECTION.md` content-hygiene repair is complete:
  exactly 3 trailing-whitespace defects removed, line count 188, semantic content unchanged.
- staging is empty.

Known terminal validation evidence:
- Infrastructure 191/191 PASS
- WP08 lifecycle 18/18 PASS
- Architecture 27/27 PASS
- Application 136/136 PASS
- Domain 11/11 PASS
- Python 25/25 PASS
- build 0 errors
- Python 3.13.15
- Streamlit 1.61.1
- pip check clean
- Gitleaks 8.30.1 clean across 112 commits
- two known local certificate-selector warnings are environment-only.

#242–#249 remain Closed/Done.
Milestone #59 remains Open, 0 open / 8 closed.

Verify fresh state and final publication gates. Create/reuse the governed Release 1.10 branch from `5cc2...`. Stage exactly the manifest's literal 103 paths. Run `git diff --cached --check` and exact staged-set verification. Create exactly one candidate commit, push only the governed branch, and create/reuse exactly one PR against main.

Do NOT edit candidate content.
Do NOT stage excluded control artifacts.
Do NOT force-push.
Do NOT merge.
Do NOT close milestone #59.
Do NOT tag/version.
Do NOT publish a GitHub Release.

End only with the exact COMPLETE or BLOCKED terminal.

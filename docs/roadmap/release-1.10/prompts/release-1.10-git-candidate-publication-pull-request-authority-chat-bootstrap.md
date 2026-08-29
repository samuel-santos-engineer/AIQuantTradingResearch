Execute `release-1.10-git-candidate-publication-pull-request-authority-codex-prompt.md`.

## Model assignment
- **GPT-5.6 Luna** — contract/policy/architecture/reconciliation/governance.
- **GPT-5.6 Terra** — PRIMARY Git/GitHub publication execution.
- **GPT-5.6 Sol** — supporting analysis only.

Use **GPT-5.6 Terra**.

Binding WP08 handoff:

- base/local main/origin main at freeze:
  `35ec644576275570aee522872c770e6c06e7879d`
- ahead/behind: 0/0
- frozen Release 1.10 candidate: **101 paths**
  - 21 tracked WP01–WP07 changes
  - 80 untracked Release 1.10 artifacts
- #242–#249 Closed/Done
- milestone #59 Open, 0 open / 8 closed
- Project Done for #249 was automated
- WP08 repository mutations: ZERO
- WP08 Git mutations: ZERO

Validation carried from WP08:
- .NET 365/365
- Python 25/25
- build 0 errors
- Streamlit 1.61.1
- pip check clean
- Gitleaks 8.30.1 clean across 112 commits
- schema v4 preserved
- zero package/project/schema diff
- residue clean
- two documented local certificate-selector warnings are environment-only.

Authority:
1. verify fresh Git/GitHub state;
2. prove exact frozen 101-path candidate;
3. preserve content unchanged;
4. create/reuse the governed Release 1.10 branch;
5. stage exactly the 101 frozen paths;
6. create one governed candidate commit;
7. push only that branch;
8. create/reuse exactly one Release 1.10 PR against main;
9. post-verify remote branch, commit, PR, issues, project, and milestone;
10. produce exact mutation accounting and downstream merge/release handoff.

Do NOT:
- modify candidate content;
- merge;
- force-push;
- close milestone #59;
- tag/version;
- publish GitHub Release;
- mutate WP issues/Project unless required to correct an authority-caused inconsistency.

If the base has advanced or the 101-path freeze cannot be proven exactly, BLOCK instead of rebasing/repairing by invention.

End only with the exact COMPLETE or BLOCKED terminal from the authority.

Execute `release-1.10-post-merge-windows-app-control-validation-gate-reconciliation-authority-luna-codex-prompt.md`.

Use **GPT-5.6 Luna**.

This is a narrow reconciliation authority, not implementation.

Binding state:
- PR #250 merged.
- accepted candidate `7148c9b347b5b7f0a162157e6c8dee25fdee372c`
- merge commit `eb9601596d9a9dd68f1f8a7c963906a76e5a2833`
- exact 103-path payload verified.
- local `main` = `origin/main`.
- staging empty; tracked changes zero.
- 16 untracked execution-control prompts preserved.
- #242–#249 Closed/Done.
- milestone #59 Open, 0 open / 8 closed.

Current blocker:
freshly rebuilt, validly signed first-party DLLs still fail Windows App Control loading with `0x800711C7` during required post-merge Infrastructure validation. Certificate and Authenticode signatures are valid. No repository mutation or product regression is proven.

Diagnose the environment gate, prove or reject repository innocence, capture Windows security evidence, run only bounded reversible environment-only recovery experiments, and freeze an exact deterministic Terra recovery/validation contract.

Do NOT edit repository content.
Do NOT weaken/disable Windows App Control or WDAC.
Do NOT mutate Git/GitHub.
Do NOT close milestone #59.
Do NOT tag/version.
Do NOT publish a GitHub Release.

If successful, hand off to GPT-5.6 Terra for post-merge validation resumption.

End only with the exact COMPLETE or BLOCKED terminal.

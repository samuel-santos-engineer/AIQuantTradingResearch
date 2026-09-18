# GPT-5.6 Terra Bootstrap --- Release 1.12 WP04

Execute `release-1.12-wp04-execution-authority-terra.md`.

**Selected execution model:** GPT-5.6 Terra

Repository: `C:\projects\github\AIQuantTradingResearch`

WP: `#263 — Persistent SQLite Initialization, Data Update & Recovery`

Verified starting anchor:

`40a9a236dae789864f35e64bb5c1afd358e7b3db`

First: refresh Git/GitHub, reconcile current `main`, read the Release
1.12 definition/execution plan/file manifest and #263, then establish
the exact WP04 path allowlist before editing.

Core proof: persistent SQLite under `/home`, application-owned
initialization/update, DELETE journal mode, no ephemeral/direct-SQL
bypass, restart persistence, redeployment persistence, recovery,
integrity, fidelity/idempotency/conflict preservation, and fresh
validation.

Use exact PowerShell handoff and STOP/wait when authenticated
Windows/Azure/Docker context is required.

PR creation and merge are authorized only after acceptance gates pass.

After successful merge/post-merge validation, close #263 and set Project
#2 Status Done unless automation already did so. Keep milestone #63
Open.

Final:

`RELEASE 1.12 WP04 — PERSISTENT SQLITE INITIALIZATION, DATA UPDATE & RECOVERY: PASS`

`RELEASE 1.12 WP04 — LIFECYCLE COMPLETION: PASS`

`RELEASE 1.12 WP05 — EXECUTION AUTHORITY: READY`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY COMPLETE`

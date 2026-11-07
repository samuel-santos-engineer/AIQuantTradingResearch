# Release 1.15 — WP19: Operator-executed PowerShell handoff

## Context
Repository: `samuel-santos-engineer/AIQuantTradingResearch`.
PR #408 merged into `main` at `87fae322f3b0ab8f7f0ac31d7e27d34bf2efa5d4`.
WP18 added `eng/azure-cli/r1.15-qualification/wp18_research_launcher.py`, which requires a real offline PowerShell 5.1 script-launch preflight. The operator host's effective policy was `Restricted`, so the real script was blocked before any HTTPS probe. Previous research-only deployment was rolled back and independently verified. `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED`, `PUBLIC_RUNTIME_VERIFIED=False`.

## Objective
Implement a safe **human-operator execution handoff**: when Codex cannot run a PowerShell command in its own sandbox/session, Codex writes complete, inspectable PowerShell scripts and concise operator instructions for the owner to execute in their own authorized session. Do not confuse a Codex tool permission restriction with the operator's Windows PowerShell execution policy; diagnose each separately. Do not bypass either restriction.

## Deliverables
1. Add a small set of version-controlled, Windows PowerShell 5.1-compatible scripts under `eng/docker/release-1.15/` (or an existing suitable operations directory), with a clear separation between:
   - **Offline, read-only local preflight**: report `Get-ExecutionPolicy -List`, effective policy, PowerShell version, Python availability, script provenance/signature (`Get-AuthenticodeSignature` where relevant), parse readiness script, and invoke the existing WP18 launcher in its default *offline-only* mode. Never change execution policy or invoke a live probe.
   - **Optional operator-controlled deployment handoff**: a separate script that can invoke the existing WP18 guarded launcher with a privately supplied, absolute controller path **only after fresh explicit owner approval for a specific live attempt**. No auto-run, implicit confirmation, or default live mode. If the existing private controller is unavailable, fail clearly; do not fabricate one or weaken its safeguards.
2. Include a brief operator runbook showing the exact `powershell.exe -NoProfile -NonInteractive -File <script>` commands to execute from the repository root, expected sanitized results, and how to return results to Codex. State that with effective `Restricted` policy, `-File` is expected to be blocked; ask the owner/administrator to resolve policy/signature according to organizational rules, not via a bypass.
3. Add an offline evidence/report mode that produces a bounded, sanitized JSON report (no secrets, full environment dumps, registry exports, app settings, image credentials, private controller contents, or raw Azure responses). Document what to redact before sharing. If execution is blocked, give the owner a minimal list of safe interactive read-only commands to inspect policy, but do not present them as a way to bypass policy.
4. Add focused tests for argument validation, policy failures, missing interpreters, no network/Azure on offline path, no controller invocation on failed preflight, output redaction, and PowerShell 5.1 parsing where available. Preserve UTF-8 without BOM and LF, and `git diff --check`.
5. Open a focused PR for owner review; do not merge it. Preserve all existing dirty/untracked owner files and do not overwrite local operational evidence.

## Non-negotiable safety rules
- Never run `Set-ExecutionPolicy`, `-ExecutionPolicy Bypass`, process-scoped policy overrides, registry/profile edits, signing workarounds, `Invoke-Expression`, or alternate interpreter paths to evade restrictions.
- Never ask Codex to execute a blocked command by changing execution identity or escaping sandbox controls. Codex should write the script, explain its intended effects and let the owner choose whether to run it.
- A human-executed script must not silently deploy, publish, restart, stop, mutate Azure, query provider data, import candles, or send application HTTP requests. The optional live script must require explicit, fresh approval, the existing qualified controller and existing WP18 gate; do not treat this prompt as that approval.
- Maintain exact research-only target, immutable published image, private complete baseline and independent rollback verification in the separately authorized controller. No UI, production, paid infrastructure or provider activity.
- Do not add another terminal/image-attestation prerequisite to ordinary HTTPS research API operation.

## Acceptance and reporting
Report changed files, exact test commands/results, what Codex could not execute, what the owner should execute in their session, and what evidence should be returned. Explicitly distinguish `OFFLINE_PREFLIGHT_PASS` from `RESEARCH_PROCESS_ALIVE` and public chart acceptance. A Restricted-policy refusal is a correct safe outcome, not a successful preflight.

Do not perform any Azure deployment, application probe, provider request or import in WP19. `PUBLIC_RUNTIME_VERIFIED=False` until real browser acceptance with authentic BTC/USD and ETH/USD historical candles.

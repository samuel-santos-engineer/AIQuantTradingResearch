# Release 1.15 — WP32 Codex Prompt: Research API Service-Key Configuration

## Starting point and authority

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Start from clean `main` containing merged PR #419, squash commit `9e490158fb93b77a9080aabf8f7e80d26b8e5925`. Verify the commit and relevant source before modifying anything. Owner explicitly approved proceeding to WP32, but **did not provide a real key, authorize disclosure, or authorize unrestricted Azure mutations, deployment, live controller execution, or authenticated probes**. Do not infer those permissions. Where the exact scope of an Azure configuration mutation is not unambiguously approved, prepare a reviewable implementation and an exact operator approval request, then stop before mutation.

The research API owner has **never created `AIQ_RESEARCH_API_KEY`**. Prior operator read-only attempt `2683eeb5-903c-4434-b262-70452fb3040e` verified two matching captures and Azure baseline, but found the permitted service-key channel checked/unavailable. Preserve this and all other historical attempts untouched. PR #418 documented the bootstrap constraints; PR #419 added `Wp31-ServiceKey.ps1` and fake-input qualification (owner reports all 13 cases passed). Do not claim secure vault/transfer or real-key qualification from that test.

## Goal

Deliver a practical, minimal, secure, **research-app-only** procedure and, where justified, offline-tested operator tooling to configure one shared research API key, with exact preflight, secret-safe transfer, recovery, fresh read-only baseline and a clear approval boundary. Avoid another generic planning-only milestone if implementation can be completed safely offline.

Target **only** research App Service `aiqr115researchstagingwus27f5eabb5` in resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, West US 2, F1 plan `asp-aiq-r112-wp05-wus2-7f5eabb5`. Never touch UI app `aiqr112wp05wus27f5eabb5`. Setting name `AIQ_RESEARCH_API_KEY`; operator process channel `AIQ_WP21_SERVICE_KEY`. These are **two names for the SAME secret**. Neither is a Twelve Data credential, Azure token or registry password.

## Required source reconciliation

1. Inspect merged WP30/WP31 runbooks and actual `Wp31-ServiceKey.ps1`, `Test-Wp31ServiceKey.ps1`, `wp21_controller.py`, `wp21_runtime.py`, `wp22_preparation.py`, `Invoke-Wp29ReadOnlyQualification.ps1`, and `ResearchSnapshotEndpoint.cs`. Record the real API key validation/startup semantics, precedence, process inheritance, exact baseline capture, and current tests. Do not assume a hot-reload after App Service settings change.
2. Distinguish four claims: local key shape valid; Azure setting present; both channels contain identical value; actual authenticated API request succeeds. None follows automatically from the previous claim. `/health` is not an authentication check.
3. Verify whether a usable owner-approved encrypted local vault and private transfer path are actually established. **Do not invent or silently assume one.** If absent, implement only safely testable local and preflight pieces and state the exact missing owner choice, without generating or accepting a real key.

## Implementation guidance

- Prefer a narrow, fail-closed Windows PowerShell 5.1 operator workflow, building on WP31, with fake-secret tests. Do not create a new credential store or paid service. Use 32 cryptographically random bytes encoded as **64 lowercase hexadecimal characters** for a future owner-generated shared secret; shape alone does not prove entropy. Do not generate a real secret in Codex or ask the user to paste one.
- **Never** pass real secrets through Azure CLI command arguments (`--settings`), command history, script source, process command lines, logs, stdout/stderr, transcripts, clipboard, plaintext files, GitHub or ChatGPT. Do not disable policy, auditing or telemetry. Do not claim a portal input is secret-safe merely because the settings listing masks values. If no verified secure portal/secret-manager transfer exists, stop at an actionable operator handoff rather than improvising.
- Verify exact resource identity and prior setting **absence** before any authorized change, without printing or exporting existing values. If the setting exists with an unknown value, stop; do not overwrite. Preserve unrelated settings, sticky-slot metadata (including null vs `[]`), image/startup, HTTPS-only, logs, F1 plan and UI. A save restarts the research app and invalidates the old baseline.
- Design a reversible, research-only **single-setting addition**. If absence was established, rollback is removing only the newly added setting, subject to explicit owner authorization; do not run rollback automatically without authority. If the old setting exists, stop pending an approved secure restoration channel.
- If implementing an operator helper, make default execution **offline/no mutation**. Require an explicit, narrowly named action and independently verified preconditions before any future Azure mutation. Never hide a configuration write behind a read-only or presence-check switch. Tests must prove zero Azure calls for default/failure paths and no raw secret disclosure. No speculative use of `az rest` or command-line secret-bearing parameters.
- After a separately approved configuration change, require **one new disabled** WP29 read-only qualification from the merged checkout; do not reuse the successful historical UUID. Presence-only qualification is not cross-channel equality or authentication. An authenticated HTTPS probe, live controller, deployment and provider acquisition each need separate explicit owner authorization.

## Tests and deliverables

1. Implement the smallest feasible secure tooling with comprehensive **fake-secret only** tests, or document a concrete technical blocker if secure transfer cannot be implemented without unverified external tooling. Do not claim unexecuted PowerShell runtime tests passed; operator can run them under their existing authorized execution policy without bypass.
2. Run the existing WP31 fake-input and WP22/WP21/WP19/WP18 offline suites as available. Clearly distinguish passes, skips, static parsing, mocked network tests and operator-only checks. Validate PowerShell 5.1 syntax, Python syntax, UTF-8/LF/no-BOM and `git diff --check`.
3. Produce an operator runbook with exact resource scope, prerequisite checklist, nonsecret output fields, mutation approval boundary, safe stop conditions, rollback plan, post-change fresh qualification and evidence interpretation. No actual key, secret fingerprint/hash, setting value or private record contents in artifacts.
4. Open **one reviewable PR**, leave it unmerged, and report base/head SHA, changed files, tests with skips, actual Azure activity (expected none), precise owner decision required, and next action. If implementation is genuinely blocked, make the smallest useful reviewable change and explain why rather than fabricating qualification.

## Hard prohibitions

No real key generation/import/receipt, no Azure configuration writes or deployment from Codex, no live WP21 controller, no authenticated probe, no Twelve Data calls, no historical attempt access/reuse, no policy/identity/RBAC changes, no public runtime success claim. Preserve `approvalEnabled=false`, `liveControllerInvoked=false`, `liveAttemptReady=false`, `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`.

**Completion means a safe, reviewable WP32 implementation and precise operator handoff — not that Azure has been configured.**

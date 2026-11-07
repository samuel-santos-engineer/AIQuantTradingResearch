# Codex Prompt — Release 1.15 WP31: Secure Local Service-Key Bootstrap Implementation

## Starting point and mission

Work in `samuel-santos-engineer/AIQuantTradingResearch` from clean `main` including merged PR #418, squash commit `d20b4e3db3d472cb190cbdd8fcac857e4c9deeeb`. Read `docs/roadmap/release-1.15/prompters/release-1.15-wp30-secure-service-key-bootstrap.md` and the actual WP21/WP22/WP29 scripts, API authentication source, and tests before changing anything.

Implement a **practical, narrowly scoped, secure local Windows PowerShell 5.1 bootstrap helper**, with meaningful offline tests and operator documentation. Do not produce another documentation-only PR unless an unavoidable technical obstacle is demonstrated with specific evidence. Open one reviewable PR; do not merge it.

## Constraints and authority

- No Azure ARM or portal changes, no live controller, no deployment, no app restart, no provider calls, no actual HTTP authentication probes, no new private attempts, no historic private-attempt reads, no secret acquisition or generation by Codex. Do not arm any template.
- No production credentials in test fixtures, arguments, output, files, logs, GitHub, screenshots, chat, or exception strings. Never print a key or a digest/fingerprint that facilitates correlation. Never put secrets in Azure CLI `--settings` arguments.
- Preserve all existing WP21/WP22/WP29 authorization, rollback, immutable baseline, diagnostics, safety checks, identity semantics and original app settings. Do not modify controller/recovery or API authentication merely to simplify bootstrap.
- `AIQ_RESEARCH_API_KEY` is the research App Service setting; `AIQ_WP21_SERVICE_KEY` is the operator's **process-scoped** channel. They must hold one identical value, never independently generated values. Do not confuse them with Twelve Data or Azure tokens.
- Research API accepts printable ASCII keys of 16–256 chars; WP21 requires 32–256. Prefer exactly 32 CSPRNG bytes encoded as 64 lowercase hexadecimal ASCII chars; enforce that shape in the new helper.
- Prior operator read-only qualification passed with matching captures but service-key channel unavailable, attempt `2683eeb5-903c-4434-b262-70452fb3040e`. Preserve it and every historical attempt unchanged. Baseline becomes stale after any separately authorized Azure configuration mutation.
- No changes to PowerShell execution policy, no Bypass, no unreviewed dependencies, no paid services, no clipboard, no plaintext persistence, no secret-bearing command-line arguments or transcript output. If an owner-selected encrypted vault or secure transfer is not established, the helper must safely stop rather than invent an unsafe workaround.

## Implementation scope

1. Inspect exact caller process boundaries: a PowerShell child started with `-File` cannot modify its parent's environment. Design a supported **same-session** loading entry point (for example, a dot-sourced, explicitly reviewed helper with a function the owner invokes in the parent PowerShell session). Do not silently rely on child-to-parent mutation. Document invocation and cleanup exactly.
2. Provide a **masked-input import** of an existing owner-retained secret into `$env:AIQ_WP21_SERVICE_KEY` for the current PowerShell process only. Prefer `Read-Host -AsSecureString` or another built-in masked mechanism, with no echo or clipboard. Validate exactly 64 hex characters before assignment, reject empty/malformed inputs, and never expose the secret in returned objects, errors or streams. Handle BSTR cleanup in `finally` and dispose secure buffers where supported; document that managed strings and child inheritance cannot be perfectly zeroized. On failure, do not leave a newly set or partially overwritten channel. Avoid accidentally using a stale existing variable; explicitly define and test safe replacement/clearing behavior.
3. Provide an **explicit clear/unload operation** that removes only the process-scoped operator variable, with a fixed, nonsecret status. No User/Machine environment writes, `setx`, profiles, registry, or automatic process launches. Ensure scope and lifetime are unambiguous.
4. Optionally provide a **local-only generator** if it can be implemented securely without disclosing, persisting or losing the generated value. Generation must use OS CSPRNG, 32 bytes and hex encoding, but a generated secret with no verified recoverable vault/transfer path is not a usable bootstrap. Prefer safe import of a secret generated and retained in an owner-approved encrypted vault. Do not generate an actual secret during this Codex task.
5. Keep Azure configuration **manual and separately authorized**. Explain exact target `aiqr115researchstagingwus27f5eabb5` in resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`, exact setting `AIQ_RESEARCH_API_KEY`, expected restart, absence check, safe one-setting rollback if initially absent, preservation of unrelated settings, and limits of portal confidentiality. Do not claim a verified secure vault-to-portal transfer unless actually established. Do not implement Azure setting mutation in this PR.
6. Define presence-only checks for local process channel that reveal only boolean validity; never claim equality with Azure setting or successful API authentication. Ensure the operator knows that the research API reads its key at startup and `/health` does not authenticate the key.
7. Include clear operational stop conditions: no approved encrypted retention/recovery, no secure transfer, existing unknown Azure setting, policy or transcript risk, malformed input, mismatched environment, or any unexpected error. Never suggest printing/pasting the secret into the console, script source, CLI arguments, GitHub or chat.

## Offline verification

- Add automated tests with **fake** 64-character keys only, proving successful masked import through a mockable boundary, invalid length/characters rejection, no key leakage to stdout/stderr/errors, process-only scope, no stale key left after failure, explicit clear, no clipboard/registry/User/Machine writes, no Azure/network access, and no child-to-parent assumption. Mock secure input; tests must not require a human to type a key.
- Run existing WP22/WP21/WP19/WP18 focused suites plus new tests; report exact counts, skips and limitations. On Windows, parse and execute the PowerShell helper under the operator-compatible execution policy without changing that policy; if unavailable, clearly distinguish static checks from execution.
- UTF-8/LF/no BOM, `git diff --check`, PowerShell 5.1 compatibility, no leaked test canaries, and no changes to existing live controller/recovery paths.
- Test that failures are fail-closed and outputs are fixed, nonsecret status indicators. Never include raw exception details or secrets in evidence.

## Deliverables

- One or more minimal new PowerShell files under `eng/docker/release-1.15/` with a clearly documented safe parent-session invocation and explicit cleanup.
- Focused automated tests and a concise WP31 operator runbook under `docs/roadmap/release-1.15/prompters/`.
- One PR against `main` with exact base/head, changed files, offline evidence, risks and clearly separated **not yet authorized** Azure portal step. Do not merge.
- If a safe masked-input implementation truly cannot be established, stop at a precise blocker report and open only a reviewable documentation correction; do not claim a functioning bootstrap.

## Acceptance and reporting

Report only nonsecret results: helper available, offline tests passed/skipped, process channel valid/invalid (when tested with fake inputs), Azure setting **not changed**, authenticated API **not tested**, live controller **not invoked**, historical attempts **untouched**. No actual key should be requested or accepted by Codex. Separate explicit owner approval is required before Azure configuration, fresh read-only qualification after any change, authenticated probes, live deployment or rollback.

Keep `approvalEnabled=false`, `liveControllerInvoked=false`, `liveAttemptReady=false`, `RESEARCH_PROCESS_ALIVE=UNVERIFIED`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False`.

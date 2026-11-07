# Codex prompt — WP36 amendment: automate ten Azure research App Service settings

## Objective and authority

Amend existing open PR #424 (`samuel-santos-engineer/AIQuantTradingResearch`) on its current head. Do not create a competing PR. The owner approves an automated Azure CLI approach for saving exactly the ten nonsecret settings below on the existing research App Service. This authorization **does not** approve WP36 image/HTTPS/restart `Apply`, changing the API key, altering other resources, or a paid plan upgrade. A settings Save may cause an implicit App Service recycle; that consequence is accepted.

The previous no-settings-API boundary is superseded **only** to the extent necessary for this ten-setting operation. Be transparent about the fact that `az webapp config appsettings set` may internally retrieve and rewrite the whole collection; do not claim the API itself performs a server-side partial PATCH. The existing API key value must not be printed, changed, generated, copied into a command, committed, or exposed to Codex's output.

## Fixed target

- Subscription: use the exact subscription already pinned in WP36 code, not the ambient default.
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`
- Research app: `aiqr115researchstagingwus27f5eabb5`
- Existing Linux F1 plan in West US 2; never touch UI app `aiqr112wp05wus27f5eabb5`.
- Existing key-setting **name** `AIQ_RESEARCH_API_KEY` was confirmed present by owner; value is private and must remain unchanged.

## Exact desired nonsecret settings

| Setting | String value |
|---|---|
| `WEBSITES_PORT` | `52731` |
| `AIQ_RESEARCH_BOOTSTRAP_MODE` | `serve` |
| `AIQ_RESEARCH_BIND_ADDRESS` | `0.0.0.0` |
| `AIQ_RESEARCH_TRANSPORT` | `azure-app-service-http` |
| `AIQ_RESEARCH_PLATFORM_HTTPS_ONLY` | `1` |
| `AIQ_RESEARCH_PRIVATE_DIRECTORY` | `/tmp/aiq-research` |
| `AIQ_RESEARCH_STORAGE_SCOPE` | `azure-f1-staging-ephemeral` |
| `AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING` | `1` |
| `AIQ_RESEARCH_EXPECTED_MOUNT_PATH` | `/` |
| `WEBSITES_ENABLE_APP_SERVICE_STORAGE` | `false` |

For existing entries, preserve their existing `slotSetting` flags; for newly created entries use nonsticky/default flags. Preserve unrelated entries and all their flags. Do not change connection strings.

## Implementation instructions

1. Inspect current PR #424 and its WP36 script/tests/runbook. Update that same branch with the smallest maintainable implementation, ideally a distinct `SettingsPlan` / `SettingsApply` path rather than reusing the existing image deployment `Apply`. Keep image deployment approval switches separate. The default command must remain nonmutating.
2. Research/inspect the installed Azure CLI's actual `az webapp config appsettings set` semantics and output handling. Use explicit `--subscription`, `--resource-group`, `--name`, `--settings` and `--output none`. Avoid `--debug`, `--verbose`, command echo, and unfiltered JSON outputs. No key value may be passed as a command argument. **Do not assume** that `--output none` prevents Azure CLI from reading the settings collection internally.
3. Before any live save, establish that the operation preserves existing unrelated settings, key bytes and slot flags. A blind full-settings PUT or any method that could reset sticky flags is unacceptable. If the installed CLI version cannot provide adequate preservation assurance, stop and report `SETTINGS_PRESERVATION_UNVERIFIED`; do not experiment on the live app. Prefer a minimal operation that the CLI documents as merging named entries. The owner has approved use of that merge-style CLI behavior, including its internal read/merge/write mechanics, but not arbitrary full replacement from a partial set.
4. Use a targeted, sanitized preflight. Check exact app identity, plan, region, hosting mode, and key-setting name presence. If reading settings metadata is necessary to establish preservation, keep any sensitive values in-process only; never print/log/serialize them or upload them to external tools. Prefer verifying value preservation through non-disclosing in-process comparisons (boolean only) if feasible. If access to the secret value would be unavoidable and cannot be safely contained, stop rather than reading or disclosing it. No new secret storage, vault, key rotation or full-baseline export.
5. Add a separate explicit settings-only confirmation switch (e.g. `-ApproveResearchSettingsSave`) and exact target guard. Do not use `-ApproveExactResearchDeploy` or call the existing image/HTTPS/restart `Apply`. On approval, execute one bounded settings save; no automatic retry after timeout or uncertain mutation. Saving may implicitly recycle the research app; do not issue an explicit restart.
6. Verify only the ten named nonsecret values, existing key **name** presence, and preservation of slot flags. Where possible verify unchanged unrelated entries using private in-process comparison and output only a boolean/summary, never a complete settings listing. If a preservation check is inconclusive, report it explicitly; do not say `settings saved` unless Azure confirmed the save and required post-save checks pass.
7. Preserve the research app's current image mode, `httpsOnly`, plan, slots, logging and unrelated settings. Do not call the registry, deploy the image, probe `/health`, call authenticated endpoints, or touch provider data as part of settings save.
8. Update the WP36 runbook to distinguish settings-only authority and its real CLI merge semantics from subsequent separately approved image deployment. Document exact operator commands and nonsecret outcomes, and what to do on an uncertain save without complex automatic recovery.
9. Add fake-only tests for fixed target/subscription, ten exact values, no default mutation, separate approvals, preservation of key/unrelated settings/slot flags, no secret disclosure in stdout/stderr, failed/ambiguous save handling, and no image/HTTPS/restart operations. Run targeted tests and lint/diff checks. Do not create fake evidence of live Azure success.

## Execution and reporting

The owner has approved the ten-setting save as a standalone Azure change. You may execute it **only if** the implementation has passed preservation preflight and the execution environment's Azure identity is confirmed to target the exact research app; otherwise leave it unexecuted and report the blocker. Do not use a key-preservation assertion as a substitute for actual evidence. If the save succeeds and verification passes, report the exact phrase **`settings saved`**, along with a sanitized list of the ten nonsecret setting names and the checks performed. Never display the API key or unrelated settings values.

Commit changes to the existing PR #424 branch, update its description with factual tests/operations, and leave PR open for owner review. Do not merge. Do not run the separate WP36 image/HTTPS/restart Apply. Preserve the status flags `liveAttemptReady=false`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False` until actual evidence warrants a change.

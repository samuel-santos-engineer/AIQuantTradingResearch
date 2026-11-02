# Release 1.15 WP32 - Research-only service-key configuration preflight

Status: offline implementation for owner review. Azure configuration NOT authorized.
Base: `9e490158fb93b77a9080aabf8f7e80d26b8e5925` (merged PR #419).
UTF-8/LF/no-BOM applies to all new/modified text.

## Actual evidence and missing owner choice

Owner supplied WP31 OFFLINE_HELPER_TESTS_PASS with 13 fake-input cases, actualKeyUsed
false and azureInvoked false. That is owner runtime evidence for fake local input,
not proof of an encrypted vault, private transfer or real-key/API qualification.
Historical read-only qualification passed with key availability checked/unavailable;
it does not distinguish an absent Azure setting from an unusable one. Owner reports
never creating the setting; no Azure absence query is made by Codex.

An existing owner-approved encrypted local vault, recovery method and private
non-clipboard vault-to-input/portal transfer have NOT been established in the
conversation. The owner must name the existing tool and method only, NEVER a key.
Until then no real key is generated, imported or accepted and no Azure step runs.
This is the concrete boundary that prevents safe configuration automation. No
additional credential store, paid service, clipboard workaround or invented portal
feature is introduced. Shape validation does not establish 32 bytes of CSPRNG entropy.
The future owner-generated value should be 64 lowercase hex characters encoding
32 independent CSPRNG bytes in that approved encrypted vault.

## Source reconciliation - existing behavior unchanged

- ResearchSnapshotEndpoint.RunAsync reads AIQ_RESEARCH_API_KEY at startup.
  Create rejects empty values, lengths outside 16-256 and non-printable ASCII.
  It captures SHA-256 and uses fixed-time hash comparison for X-API-Key. There is
  no hot reload of the captured key. /health is not an authentication check.
- wp21_controller and Azure.deploy prefer inherited AIQ_WP21_SERVICE_KEY, then
  captured AIQ_RESEARCH_API_KEY, with 32-256 printable-character checks. The same
  value is used for the deployment setting; these are two names for ONE secret.
  They are not Twelve Data, Azure or registry credentials. A stale nonempty local
  value takes precedence; WP31 deliberately clears it on failed replacement.
- WP31 must be dot-sourced in the parent PowerShell 5.1 session. Loading functions
  changes no key. Masked import/clear affect Process scope only. -File children
  cannot set the parent's environment, but inherit a copy from it. Clearing the
  parent does not clear existing children. Managed strings/environment/dumps remain
  an exposure; BSTR zeroing cannot erase every copy. Stop on transcript/policy risk,
  never disable mandatory recording or change identity/policy to proceed.
- WP22 captures the exact research site and F1 plan; full web config, settings,
  connection strings, logging, slot flags and slots; two complete captures must
  match strictly. Required nullable slot lists are preserved, not normalized.
  Private ownership journals/observations contain sanitized metadata; baseline
  values remain in memory during read-only qualification. The future live WP21
  baseline persists full app settings under private ACL and may therefore contain
  a key: WP30's existing tradeoff remains; do not claim no secret-at-rest there.
- Invoke-Wp29ReadOnlyQualification is a child operator workflow and makes only the
  separately selected read-only inspection with a fresh disabled UUID. It neither
  configures a key nor grants approval. A process channel may make presence true
  without proving Azure equality. All controllers/recovery/API/diagnostics remain
  unchanged by this PR.

## Four separate claims

| Claim | WP32 evidence |
| --- | --- |
| Local key shape valid | Only if explicit local check runs; 64 lowercase hex, not entropy proof |
| Azure setting present/absent | Unverified; no Azure request implemented or run |
| Both channels identical | Unverified; presence is not equality |
| Authenticated API succeeds | Not tested; requires separate explicit probe authority |

No claim promotes another. Configuration permission and live readiness always
remain false in the preflight report, even after a valid fake local-channel check.

## Small implemented preflight

Test-Wp32ServiceKeyPreflight.ps1 defaults to offline/no secret check. It verifies
Windows PowerShell 5.1 and the reviewed local WP31 helper exists, emits fixed JSON
and performs ZERO Azure calls. It never imports, clears, generates or persists a
key and never creates/arms a private attempt. There is intentionally no Apply,
Configure or Rollback switch: secure transfer and mutation authority are unresolved.
The default result OFFLINE_PREFLIGHT_COMPLETE_CONFIGURATION_BLOCKED means local
preflight completed, NOT that a configuration change is permitted.

After review, run from the reviewed checkout:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp32ServiceKeyPreflight.ps1
```

An explicit presence-only check exists for a FUTURE authorized parent session whose
retained value was loaded through WP31 after secure local prerequisites are met:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp32ServiceKeyPreflight.ps1 -CheckLocalChannel -ApprovedRetentionAndTransfer -SessionSafetyReviewed
```

These switches are owner attestations, not verification of an external vault or
permission to mutate Azure. Missing confirmations block BEFORE reading the channel.
Invalid/missing local shape blocks with LOCAL_CHANNEL_UNAVAILABLE_OR_INVALID. A
valid check returns LOCAL_CHECK_COMPLETE_CONFIGURATION_BLOCKED. The child cannot
populate its parent; it only reads its inherited copy through Test-Wp31ServiceKey.
The preflight never changes or clears that copy. No actual local check is authorized
or claimed by this implementation; tests supply fake values in disposable children.

Share only result, localChannelChecked, localChannelShapeValid and the fixed safety
booleans/failureReason. No key, digest, value, private path, raw error or configuration
is output. Default checks and failures have no network or mutation path. Close/clear
parent channels using WP31 after later authorized work, including on failure.

## Exact future operator approval request - not granted by WP32

After tool/transfer review, fake runtime qualification and nonsecret preconditions,
the owner may separately authorize this narrowly bounded action:

> Authorize the owner to verify the exact research App Service identity/plan and
> prior absence of AIQ_RESEARCH_API_KEY without exporting existing values, then add
> only that setting using the one securely retained shared key and approved private
> transfer. Authorize recovery by removing only that newly added setting IF prior
> absence was verified. Preserve all unrelated settings and slot behavior, image,
> startup, HTTPS-only, logging, F1 plan and UI. Allow the configuration-save restart.
> If an unknown existing setting or any unrelated drift is found, stop without
> overwrite. This authorizes no deployment, authenticated probe, provider call,
> live controller or general rollback.

Exact research scope:

- App: aiqr115researchstagingwus27f5eabb5
- Resource group: rg-aiq-r112-wp05-wus2-7f5eabb5
- Plan: asp-aiq-r112-wp05-wus2-7f5eabb5, West US 2, existing F1
- Azure setting: AIQ_RESEARCH_API_KEY
- Operator Process channel: AIQ_WP21_SERVICE_KEY (SAME retained value)
- UI aiqr112wp05wus27f5eabb5 is excluded.

This request is a reviewable scope, not execution instructions to proceed now.
No Azure preflight, configuration query/write or portal access is performed here.

## Manual one-setting change/recovery design after that approval

1. In the owner's existing authorized Azure session, verify the resource ID, group,
   plan/F1/region and exact app; preserve image/startup, logging, HTTPS-only, full
   unrelated settings and slot flags without secret-bearing exports. Check actual
   prior setting absence, not merely empty/invalid presence. STOP if the name exists
   with an unknown value or if the exact resource cannot be independently verified.
   Such a replacement needs a separate secure original-value restoration channel.
2. Use the separately verified owner-only transfer to add the SAME retained key as
   the ONE setting in the exact research app's portal environment-variable editor.
   Do not use CLI --settings, secret-bearing arguments, clipboard, plaintext files,
   screenshots or chat. If secure transfer is unavailable, STOP. Portal listing
   masking does not prove edit-field/browser/screen confidentiality; no autofill or
   hidden-entry feature is invented. See Microsoft's App Service configuration
   guide: https://learn.microsoft.com/azure/app-service/configure-common?tabs=portal .
3. Preserve original sticky-slot metadata (including null versus []); do not change
   checkbox behavior or other settings as a side effect. Save only the one approved
   addition. Saving restarts the research app and invalidates the older baseline.
   This is a live Azure mutation, not a presence test. Record only sanitized facts.
4. If recovery becomes necessary, use ONLY the explicitly approved removal of the
   newly added setting whose prior absence was established; preserve all unrelated
   configuration. Recovery save restarts the app again. No automatic recovery or
   whole-settings replacement is implemented. On uncertain outcome, stop and report
   recovery unverified rather than fabricate success. Never use historical attempt
   records or assumed original secrets to reconstruct state.
5. Obtain separate authority for ONE new disabled WP29 read-only qualification
   AFTER the approved change, from reviewed merged code. Do not reuse prior success
   2683eeb5-903c-4434-b262-70452fb3040e or any other historical UUID. Return sanitized
   fields only. Presence does not establish equality or authentication. A later
   authenticated HTTPS test, live controller, deployment, rollback or provider
   acquisition requires its own explicit approval; /health does not validate a key.

Stop on vault/recovery/transfer absence, unknown Azure setting, identity/plan drift,
recording/policy risk, malformed local input, stale/mismatched environment or any
unexpected error. No permission escalation, identity switch, rotation, timeout
relaxation or extra resource creation is an automatic fallback.

## Offline evidence

Commands from reviewed repository root:

```powershell
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp32_service_key_preflight.py -v
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp31_service_key.py -v
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp22_preparation.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp21_controller.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp19_operator_handoff.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp18_research_launcher.py -q
```

Actual Codex results: new WP32 2 static passes, 4 -File runtime skips; WP31 2 static
passes, 1 runtime skip; existing 54+38+19+19 = 130 passed with no skips. Total 134
passed, 5 skipped, all suite exit codes 0. Runtime tests are fake-only: default
non-reading behavior, missing confirmations, valid fake shape with no promotion,
missing/malformed channel and no secret in JSON/error output. These have NOT run
here because host policy blocks -File; no bypass/script-text evaluation was used.
Static tests show no input/import/clear/generation/file/clipboard/network/mutation
surface; they are not runtime proof. The owner's earlier 13-case WP31 pass is
historical owner evidence, not new WP32 execution. Installed-CLI compatibility runs
only against mocked authentication/HTTP. PowerShell 5.1.26100.9711 parser: zero
errors for the new entry. UTF-8/LF/no-BOM, Python syntax and diff checks passed.

No real secret generated/imported/received, Azure activity, new private attempt,
provider acquisition, authentication probe or deployment. Existing source unchanged.
Precise remaining owner choice: identify the existing encrypted vault/recovery and
private non-clipboard transfer method by name only. Then runtime-qualify the new
preflight in the authorized session and review the exact mutation approval above.
No mutation approval is granted by this PR or a successful offline/local check.

`approvalEnabled=false`; `liveControllerInvoked=false`; `liveAttemptReady=false`;
`RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED=True`;
`PUBLIC_RUNTIME_VERIFIED=False`.

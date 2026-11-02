# Release 1.15 WP30 - Secure service-key bootstrap plan and constraints

Status: documentation-only proposal for owner review. NOT execution authority.
Base: `2d5169de33c295dacce7db576f4ed3497abb8621` (merged PR #417).
UTF-8/LF is mandatory for all future implementation and evidence.

## Evidence and source reconciliation

The owner reports no research API key has ever been created. Operator attempt
`2683eeb5-903c-4434-b262-70452fb3040e` passed two matching read-only captures and
baseline validation. Its key channel was checked and unavailable. This establishes
no usable value through the permitted channels; it does NOT independently establish
whether the Azure setting is absent, empty or malformed. No Azure query or private
record access was performed in WP30. Preserve this and all earlier disabled attempts;
never inspect again, reuse, arm, extend, rewrite or delete their records.

Source findings:

- `src/AIQuantTradingResearch.Worker/ResearchSnapshotEndpoint.cs`, RunAsync, reads
  AIQ_RESEARCH_API_KEY once on startup. Create rejects empty keys, keys shorter than
  16 or longer than 256 characters, or characters outside printable ASCII ! through ~.
  It captures SHA-256 of the ASCII key; requests compare their supplied X-API-Key
  hash using FixedTimeEquals. Updating an environment variable does not hot-reload
  the existing endpoint closure. Configuration failure yields a generic message
  and exit 1. /health is not an authenticated-key validation.
- `wp21_controller.py` requires 32-256 printable characters before arming its
  immutable baseline. It prefers AIQ_WP21_SERVICE_KEY from its inherited process
  environment, then AIQ_RESEARCH_API_KEY from captured app settings.
- `wp21_runtime.py`, Azure.deploy, uses that same precedence and puts the SAME value
  into AIQ_RESEARCH_API_KEY. It retains the other captured settings. It is a live
  deployment procedure, NOT a bootstrap command authorized by WP30.
- `wp22_preparation.py` uses the same channel precedence but reports presence/shape
  only. An invalid nonempty operator value takes precedence over a usable Azure
  value; operators must not leave stale process variables behind.
- WP21's live immutable baseline includes full app settings in private storage.
  Therefore, if a key exists in Azure, a later separately authorized live capture
  will contain it on disk in that ACL-protected baseline. This is an existing
  secret-at-rest tradeoff, not a no-persistence guarantee. WP30 does not invoke or
  change it; owner review must account for that before any live attempt.

AIQ_RESEARCH_API_KEY (research App Service) and AIQ_WP21_SERVICE_KEY (Windows operator
process) are two names for ONE shared secret. They must never be generated separately.
It is not a Twelve Data credential. Do not reuse provider, registry or Azure tokens.

## Why this PR is documentation-only

WP30 explicitly permits a documentation-only fallback when a demonstrably safe
bootstrap cannot be established. No existing reviewed end-to-end helper both creates
a recoverable local secret and transfers it into the portal without exposing it
through output, clipboard, files or arguments. Older check-conn-05.ps1 demonstrates
masked input and BSTR zeroing, but then passes its unrelated provider secret through
Azure CLI --settings arguments. That pattern is NOT safe for this task and is not
reused or executed. --output none does not hide process arguments or guarantee the
absence of CLI command-history/telemetry exposure.

A shell-only RNG-to-environment generator could avoid printing, but it would not
provide a verified secure portal handoff or recoverable retention. Adding clipboard
export, plaintext files or key display would violate the requested constraints.
No speculative generator/importer is added. No real or example key is generated.
No claim is made that manual transfer, RNG strength or a new helper was tested.

## Proposed owner-only local secret workflow - future explicit approval required

1. Use an EXISTING owner-approved local secret manager only if its documented
   generator can produce at least 32 independent CSPRNG bytes. A 64-character hex
   encoding of those bytes is compatible with both validators; encoding adds no
   entropy. A long human-selected password is not equivalent. Do not install a new
   tool, add a paid service or assume a tool's generator meets this requirement.
2. Generate exactly one value in that private local tool, after separate owner
   execution approval. WP30 authorizes neither generation nor receipt of it.
3. Retain it only in the owner's approved encrypted local vault, with owner-only
   access and a verified recovery mechanism. This is deliberate encrypted
   persistence, not plaintext key-file storage. If the owner's no-disk rule also
   excludes encrypted vault retention, STOP: secure recoverability and session-only
   storage cannot both be promised. Do not substitute a plaintext file, chat,
   repository, console output, screenshot or PR attachment.
4. Do not copy through the system clipboard or disclose the value on screen to
   bridge missing tooling. Use an independently verified private local transfer
   capability only if it avoids clipboard/history/transcripts and the owner has
   accepted its exposure model. No particular password-manager autofill or portal
   capability has been verified here. If no such path is available, STOP before
   generation/configuration; arrange an owner-reviewed secure manual method first.

These conditions are practical unresolved proof obligations, not additional
licensing or an assertion that an existing tool is unsafe. No tool is installed,
selected or used in this PR. Do not send key material to Codex for assistance.

## Proposed operator process channel

After separate approval and secure local retention/transfer are resolved, the owner
can load the SAME value through a reviewed masked-input helper in the current
PowerShell 5.1 process. The implementation should use Read-Host -AsSecureString,
convert only as required to set the process environment, zero/free any BSTR in a
finally block, dispose the SecureString and discard plaintext references. Validate
exactly the expected generated encoding/length before assigning the variable; on
failure leave no partially populated channel and return only a fixed status.

No such helper is introduced or qualified by this documentation-only PR. Do not
improvise by pasting a key into $env: assignments, command arguments or script text.
The existing single WP29 qualification script has no key-generation/input function;
do not silently add one or assume a -File child can update its parent environment.

The intended variable lifetime is PROCESS only: never User/Machine, setx, profile
or registry. Load it in the parent operator session immediately before the required
reviewed child workflow, then clear it and close the session afterward. Children
inherit a copy: clearing the parent does not clear an already-running child. Avoid
unrelated child launches during the loaded interval. Same-user/privileged tools,
debuggers, process dumps, paging/hibernation and managed string copies can expose
plaintext. BSTR zeroing is limited cleanup, not a guarantee that every copy is erased.
Do not turn off mandatory auditing/security controls to conceal a secret; stop if
local recording policies cannot safely accommodate masked entry.

## Separately approved Azure portal step - NOT authorized by WP30

Exact target: research app aiqr115researchstagingwus27f5eabb5 in resource group
rg-aiq-r112-wp05-wus2-7f5eabb5, existing West US 2 F1 plan
asp-aiq-r112-wp05-wus2-7f5eabb5. The UI app is out of scope.

Microsoft documents app settings under Settings > Environment variables. Values
in the listing can be hidden/revealed, but that does not prove the add/edit input,
browser memory, screen, extensions or operator transfer is confidential. Do not
claim an invented hidden-entry feature. See the official
[App Service configuration guide](https://learn.microsoft.com/azure/app-service/configure-common?tabs=portal).

Following explicit owner approval of this exact mutation and its recovery:

- Owner opens only the exact research app, identifies the proposed setting by name
  without exporting other settings, and confirms whether it is genuinely absent.
  If it already exists, STOP rather than overwrite an unknown secret. Any replacement
  needs an approved secret-safe backup/restore plan for the original value.
- Once secure local transfer is resolved, add ONLY AIQ_RESEARCH_API_KEY with the one
  retained value. Preserve all unrelated settings, sticky-slot flags (including
  null/empty distinctions), image/startup, logging, HTTPS-only, plan and UI. Record
  only nonsecret presence/absence and intended slot behavior; do not export values.
- Saving is an Azure configuration mutation and App Service triggers a restart.
  Even this research-only change invalidates the prior configuration baseline.
  Do not treat it as harmless preparation or combine it with a deployment.
- If prior absence was safely established, approved rollback removes only the newly
  added setting, preserving everything else, and saves (another restart). If an
  original value existed, restore only through the separately approved secure
  backup channel. Never reconstruct an original value from assumptions or remove
  unrelated settings. Failed recovery remains unverified and must be reported.

A portal entry avoids shell process arguments, but does not eliminate authorized
browser/process/control-plane access to secrets. No CLI automation is provided.
The portal step has not occurred and its exact private transfer method remains a
blocker. No blanket permission to change settings is implied by reviewing this plan.

## Post-change qualification and rotation

After an approved successful setting change, use the retained value for the process
channel if required, and obtain a FRESH read-only qualification using one NEW disabled
attempt and the reviewed single PowerShell entry. Never arm the old successful UUID.
Return only sanctioned booleans/rule/stage fields. A nonempty process channel can
make presence true without proving the Azure value matches; the existing presence
check is not a cross-channel equality or authenticated API test. Record that limit.

Presence is not key validity, process health, data readiness or browser acceptance.
Any authenticated API probe needs separate explicit authority and must not disclose
key material or response bodies. Even /health success would not establish key
acceptance. Live controller, rollback execution and deployment need their existing
separate named authority and a fresh immutable baseline.

If exposure is suspected, stop using the value, clear operator channels, retain
only sanitized incident facts and request approval to rotate this one shared key.
A future rotation must update the retained owner secret, exact Azure setting and
operator channel together, account for restart and invalidate affected old
baselines/attempts. Do not revert to a known compromised key or paste evidence of it
into tickets. Rotation/revocation is not automatically authorized by this plan.

## Offline verification and remaining blockers

Executed from this clean base, all exit 0 and zero skips:

```powershell
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp22_preparation.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp21_controller.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp19_operator_handoff.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp18_research_launcher.py -q
```

54 + 38 + 19 + 19 = 130 existing focused tests passed. They verify existing
missing-key fail-closed behavior, boolean-only channels, canary redaction, no
unapproved mutation, exact operations and controller/recovery boundaries. All Azure
and authentication boundaries are mocked, including the installed-CLI compatibility
case. They do NOT validate an unimplemented generator, vault, masked-input helper,
portal handoff, actual secret strength or live API acceptance. No new helper/test
claims or mirrored documentation tests are introduced. No PowerShell file changed;
PowerShell bootstrap runtime/parser tests are not applicable to this doc-only PR.
UTF-8/LF/no-BOM and git diff --check are verified before publication.

Remaining: owner-selected secure generation/retention/transfer method; any necessary
reviewed process-input helper with fake-secret tests; explicit research-only Azure
setting/recovery approval; new read-only baseline/key evidence; separately authorized
authenticated validation and live attempt. Existing controllers, auth code and
resources are unchanged. No real secret, private attempt, Azure call or provider
credential was accessed, generated or received.

`approvalEnabled=false`; `liveControllerInvoked=false`; `liveAttemptReady=false`;
`RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED=True`;
`PUBLIC_RUNTIME_VERIFIED=False`. Prior read-only qualification passed; service-key
availability remains unavailable until a future authorized observation establishes it.

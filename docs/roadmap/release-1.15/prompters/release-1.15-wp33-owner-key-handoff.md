# Release 1.15 WP33 - Existing owner key handoff and evidence

Status: offline implementation for review; PR must remain OPEN/UNMERGED.
Base: `6d9be25bddebc3c4a1331894368750793e32f4fb` (merged WP32).
UTF-8/LF/no BOM is required. No Azure or real-key execution is authorized here.

## Established facts and source review

Owner reports executing a local generate-key.ps1, verifying the existing $key has
64 lowercase hexadecimal characters and retaining a recoverable copy. Native Windows
PowerShell cryptographic RNG is the owner-selected method. Codex did not read that
script, its output, the retained copy or a real key; contents and generation strength
are not independently audited. These owner facts supersede the earlier lack of a
selected retention method; no new vault, paid tool or second generated secret is
required. This implementation uses no RNG.

One value belongs in AIQ_WP21_SERVICE_KEY (operator Process channel) and
AIQ_RESEARCH_API_KEY (research App Service). It is not an idempotency/provider/Azure
credential. WP21 prefers the inherited operator value over captured app settings,
requires 32-256 printable characters and uses the same value in deployment settings.
The API accepts 16-256 printable characters, reads its setting on startup and captures
a hash for fixed-time request comparison. Setting changes do not hot-reload that
closure. /health is not authenticated key verification. These sources are unchanged.

## Implemented local handoff

Wp33-OwnerKeyHandoff.ps1 is dot-sourced in the SAME parent session that already owns
$key. Loading definitions loads the reviewed WP31 functions but reads no key and
changes no environment. A -File invocation only returns DOT_SOURCE_REQUIRED; a
child cannot load its parent. Import-Wp33OwnerKey has NO key-valued parameter.

On explicit invocation with SessionSafetyReviewed, it clears any stale process
channel, reads the variable named key from exactly the immediate caller's scope,
requires a string of exactly 64 lowercase hex characters, copies it directly to the
Process environment, checks WP31 shape and ordinal equality with that local source,
and returns only fixed statuses/booleans. Missing/malformed input or exceptions clear
the channel without restoring a stale value. KEY_CHANNEL_STATE_UNVERIFIED means
clearance was not confirmed: stop, close the dedicated session, launch no child work.
No normalization, digest/fingerprint, clipboard, file, CLI argument or network path
is used. The caller's $key and unrelated variables are not altered. Do not invoke
from a nested function scope that lacks $key and expect an ancestor search.

Direct in-process reference copying avoids converting the same already-plaintext
value through another prompt/BSTR. It does NOT remove plaintext risk: $key,
environment copies, debuggers, same-user/privileged readers, dumps and paging may
expose it. History contains the function name, not the value; no claim is made that
tracing/recording/debuggers cannot observe memory or evaluated calls. Do not disable
mandatory auditing/policy. Stop if the owner's private session is not suitable.
Managed strings cannot be perfectly zeroized. Child processes inherit their own
copies, so clearing the parent does not clear existing children. Avoid unrelated
child launches while loaded; close the dedicated session after approved work.

## Fake-only qualification before real owner use

After review, in the existing authorized Windows PowerShell 5.1 environment:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp33OwnerKeyHandoff.ps1
```

This creates only repeated-character fake variables in a disposable child. It must
return OFFLINE_OWNER_HANDOFF_TESTS_PASS, fakeCases=14, actualKeyUsed=false and
azureInvoked=false. If blocked/failing, stop; no policy bypass and no real import.
WP31's owner-supplied 13-case pass remains historical evidence, not a WP33 pass.

## Same-session operator handoff - reviewed code only

In the original owner session where the already-generated $key still exists,
dot-source the reviewed helper. Do NOT use powershell.exe -File for loading:

```powershell
. .\eng\docker\release-1.15\Wp33-OwnerKeyHandoff.ps1
try {
    $handoff = Import-Wp33OwnerKey -SessionSafetyReviewed
    $handoff
    if (-not $handoff.processChannelValid) { throw 'Local key handoff blocked.' }
    Test-Wp31ServiceKey
    # Only a later separately authorized operation may be inserted here.
} finally {
    Clear-Wp31ServiceKey
}
```

This demonstration clears immediately and authorizes NO Azure operation. The same
finally cleanup must wrap any later explicitly approved operation. Never type the
key as a command/argument or print $key/the environment. If the variable is absent,
do not generate another secret: use the retained SAME value through WP31's reviewed
masked-input path under the owner's private-input conditions, or stop. Do not ask
Codex to recover or receive it. Retain $key only as long as required for the owner
workflow, then discard that variable and close the dedicated session after verifying
recoverability; reference removal does not guarantee memory erasure.

## Future manual Azure portal step - separate exact authorization required

No portal read/write is authorized or performed by WP33. Before any owner portal
step, request explicit permission limited to identity/absence verification, one
research-setting addition and narrowly bounded removal recovery if initially absent.
No live controller, authenticated probe, deployment, provider call or UI change is
included. Exact scope:

- App: aiqr115researchstagingwus27f5eabb5
- Resource group: rg-aiq-r112-wp05-wus2-7f5eabb5
- Plan: asp-aiq-r112-wp05-wus2-7f5eabb5, existing F1, West US 2
- Setting: AIQ_RESEARCH_API_KEY, the SAME recoverably retained owner value
- UI aiqr112wp05wus27f5eabb5 is excluded.

After that separate authority, the owner independently verifies exact app/resource
identity, F1 plan/region and actual setting-name absence in the existing authorized
portal session. Earlier key-unavailable evidence does not prove absence. If the
setting exists with an unknown value, STOP without overwrite; replacing it needs
an approved secure restoration plan. Do not export app settings to establish this.

Feasible owner-controlled transfer: privately read the owner's ALREADY retained
copy and manually transcribe its 64 characters directly into the portal setting
editor, in a private unrecorded viewing environment compatible with mandatory
security controls. Do not reveal $key in the console to obtain a visual copy, use
clipboard, screenshots, CLI arguments or this chat. This assumes the owner's retained
copy can be privately read; it does not assume a new vault, hidden portal input or
Auto-Type feature. If the retained copy cannot be read without prohibited exposure,
STOP for that specific access problem instead of inventing a transfer method.
Portal inputs/browser memory may expose the value on the owner's screen; this
method is private manual entry, not a claim of end-to-end secret isolation. Do not
turn off auditing or recording to make it feasible. Manual transcription can be
mistyped; without a separately approved equality/authentication check, do not claim
cross-channel equality. Codex does not see or assist with the actual entry.

Add only this one setting. Preserve every unrelated app setting, sticky-slot
metadata (including null versus []), image/startup, HTTPS-only, logging, F1 plan and
UI. Do not change slot behavior/checkboxes as a side effect. A save restarts research
and invalidates prior configuration baselines. Recovery, only if separately approved
and initial absence was established, removes ONLY the newly added setting and saves
(another restart). Do not reconstruct unknown prior values or perform automatic
whole-settings replacement. Any uncertain recovery remains unverified.

After an approved change, require one separately authorized NEW disabled read-only
qualification from reviewed code. Do not reuse any historical attempt, including
2683eeb5-903c-4434-b262-70452fb3040e. Presence is not equality or authentication.
WP21 later captures a fresh private immutable baseline before any approved write;
that baseline includes full app settings, so WP30's existing secret-at-rest tradeoff
still applies. Authenticated probing, live/deploy/provider/browser work each retain
separate approval. No operation is added automatically to this handoff.

## WP33 evidence summary

| Claim | Actual evidence |
| --- | --- |
| Owner key shape/recovery | Owner reported 64 lowercase hex and recoverable retention; not independently audited |
| Generator contents/CSPRNG strength | Not read or audited; native RNG approach owner-selected |
| WP31 fake runtime | Owner reported 13-case pass; no real key or Azure call |
| WP33 fake runtime | NOT RUN: Codex host policy blocked -File |
| WP33 static/parser | Two Python static tests pass; both scripts parse in 5.1.26100.9711 |
| Real owner channel imported | Not performed by Codex; no availability claim |
| Azure setting present/absent | Unverified by WP33; no portal/ARM request |
| Azure/operator channels equal | Unverified; local source-to-process check is not Azure equality |
| Authenticated API | Not tested |
| Live/browser readiness | Not tested; all public readiness gates remain false |

Offline commands:

```powershell
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp33_owner_key_handoff.py -v
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp32_service_key_preflight.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp31_service_key.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp22_preparation.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp21_controller.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp19_operator_handoff.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp18_research_launcher.py -q
```

Actual results: WP33 2 static passes/1 runtime skip; WP32 2 static passes/4 runtime
skips; WP31 2 static passes/1 runtime skip; existing 54+38+19+19 = 130 passes. Total
136 passed, 6 skipped, all suite exit codes 0. Runtime skips are policy limitations,
not fake helper successes; no bypass/script-text execution workaround was used.
The fake WP33 suite covers success/current-process/source preservation, explicit
clear, absent and nine malformed sources, stale clearing, session confirmation,
replacement and finally cleanup on a controlled failure, with fixed failure-stage
reporting and no raw errors. Static checks cover no secret arguments/generation/
file/clipboard/network/User/Machine paths. UTF-8/LF/no BOM, Python syntax, PowerShell
5.1 parsing and git diff --check pass. Existing source/API/controllers/recovery and
historical attempts remain untouched. No real key generated, imported, read or sent.

`approvalEnabled=false`; `liveControllerInvoked=false`; `liveAttemptReady=false`;
`RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED=True`;
`PUBLIC_RUNTIME_VERIFIED=False`.

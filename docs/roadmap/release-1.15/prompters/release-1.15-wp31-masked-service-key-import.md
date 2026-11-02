# Release 1.15 WP31 - Same-session masked service-key import

Status: implementation for owner review; local PowerShell runtime qualification
PENDING because Codex host policy blocks -File. No Azure or live execution authority.
Base: `d20b4e3db3d472cb190cbdd8fcac857e4c9deeeb` (merged WP30).
All authored files are UTF-8 without BOM and use LF.

## Implemented boundary

`eng/docker/release-1.15/Wp31-ServiceKey.ps1` is a dot-sourced function library.
Dot-sourcing loads definitions only: it does not read a key, change the environment,
launch a process or call Azure. Running it with -File returns DOT_SOURCE_REQUIRED
without loading the caller's environment; a child cannot update its parent.

Functions:

- Import-Wp31ServiceKey: masked Read-Host -AsSecureString input, EXACTLY 64 lowercase
  hexadecimal characters. There is NO plaintext key argument, generator, file or
  clipboard import/export. Import requires explicit confirmation that approved
  encrypted retention/recovery and secure transfer exist, and that the session's
  policy/transcript/recording risk has been reviewed. These confirmations do not
  detect or establish a vault/transfer method; do not claim them if unresolved.
- Test-Wp31ServiceKey: returns only a fixed status and boolean shape validity for
  the current process variable. No value, length, hash or fingerprint is printed.
- Clear-Wp31ServiceKey: removes ONLY AIQ_WP21_SERVICE_KEY in Process scope and
  returns a fixed status plus confirmation. No User/Machine setting is read/written.

Import deliberately clears any existing operator-process value BEFORE checking
confirmations or reading input. A failed replacement never restores a stale value.
Malformed input, missing confirmations, secure-input failure or cleanup error clears
again. KEY_IMPORT_BLOCKED_CHANNEL_CLEARED confirms removal. KEY_CHANNEL_STATE_UNVERIFIED
means clearance could not be confirmed: stop, close the dedicated session and do
not launch dependent work. Success is KEY_IMPORTED_PROCESS_ONLY, not live readiness.
Do not use this helper if discarding an existing process value is not acceptable;
retain the legitimate secret in the approved encrypted vault first.

A BSTR is converted only to satisfy the existing plaintext environment channel,
zero/freed in finally, and the SecureString is disposed. Plaintext references are
discarded. Managed strings, process environment copies, dumps, page/hibernation
files and child copies cannot be perfectly zeroized. Same-user/privileged tools can
read process memory/environment. Do not launch unrelated children while loaded;
clearing the parent does not clear existing child processes. The helper does not
change auditing, transcript, Group Policy, profile, registry or execution policy.

## Existing API and controller semantics - unchanged

AIQ_RESEARCH_API_KEY (research App Service) and AIQ_WP21_SERVICE_KEY (operator process)
must contain ONE identical owner-retained secret. Neither is a provider/Azure token.
Prefer a secret already generated from 32 CSPRNG bytes and encoded as 64 lowercase
hex characters in an owner-approved encrypted vault. Shape validity does not prove
entropy. No generator is provided: without secure recoverable retention/transfer,
generating a value would be an unusable and potentially unsafe bootstrap.

ResearchSnapshotEndpoint.RunAsync reads its key at startup; Create validates
16-256 printable ASCII characters and captures a SHA-256 hash for fixed-time request
comparison. WP21 requires 32-256 printable characters, prefers the operator-process
channel over captured Azure settings and uses that same value for deployment.
WP22 only checks presence/shape. The stronger new import format satisfies both
validators without modifying them. /health does not authenticate the API key.
Presence does not establish Azure equality, valid authentication, data or readiness.
WP21's future private immutable baseline stores full app settings, potentially
including the key. WP30 documents this existing secret-at-rest tradeoff. No such
baseline or historical record is accessed or changed by WP31.

## Fake-input qualification first - after review

Run ONLY in an authorized Windows PowerShell 5.1 environment under its existing
policy; never change policy/identity to make the test run. From reviewed repo root:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp31ServiceKey.ps1
```

This is a disposable child test process with fake repeated-character values, no
human input and no Azure commands. It refuses an inherited operator channel. Run
before loading a real channel. It should return OFFLINE_HELPER_TESTS_PASS with
fakeInputCases=13, actualKeyUsed=false and azureInvoked=false. Do not interpret the
child's fake environment mutations as a parent import. If blocked, stop and return
only the fixed/sanitized status; do not paste raw errors or secret-bearing dumps.

## Same-session owner invocation - after review and runtime qualification

No actual secret is requested or accepted by Codex. Before import, the owner must
have approved encrypted retention/recovery, a private input/transfer method without
clipboard or key display, and a session safe for masked input. Do not use console
plaintext, command arguments, history, files, screenshots, chat or PRs for a key.
Do not turn off mandatory recording/security controls; stop if they conflict.

In the SAME parent Windows PowerShell 5.1 session that will later launch a separately
authorized child workflow, load the helper and invoke import interactively:

```powershell
. .\eng\docker\release-1.15\Wp31-ServiceKey.ps1
Import-Wp31ServiceKey -ApprovedRetentionAndTransfer -SessionSafetyReviewed
Test-Wp31ServiceKey
```

Enter the existing retained value ONLY into the masked prompt, using the approved
private input method. Do not paste or type it as a PowerShell command. No real key
may be supplied to Codex. Missing confirmations cause an immediate safe stop without
prompting. Do not call via a new powershell.exe -File child expecting parent changes.

After the separately authorized work, or immediately on any failure, run in that
same parent session:

```powershell
Clear-Wp31ServiceKey
```

Then close the dedicated session. Use try/finally around any later approved workflow
so Clear-Wp31ServiceKey runs even on failure. No such Azure workflow is authorized
here. Do not print the environment or inspect the variable directly. Only the
helper's fixed status/boolean outputs are shareable. No actual channel is loaded or
qualified by this PR; real availability remains unavailable until owner evidence.

## Manual Azure step - NOT authorized by WP31

Target only aiqr115researchstagingwus27f5eabb5 in
rg-aiq-r112-wp05-wus2-7f5eabb5; setting AIQ_RESEARCH_API_KEY. The existing UI is out
of scope. Obtain separate explicit owner approval before any portal read/change or
configuration/recovery action. Follow WP30's portal plan: establish prior absence
without exporting values; STOP on an existing unknown value. Use the SAME retained
secret via an independently established secure transfer method. Portal listing
masking does not guarantee confidentiality while adding/editing or in browser memory.

Add only this setting; preserve unrelated app settings and slot behavior (including
null/empty metadata), image/startup, HTTPS-only, logging and F1 plan. Saving restarts
the app and invalidates the old baseline. If initially absent, separately approved
rollback removes only this newly added setting; existing values require an approved
secure restoration channel. Never reconstruct or overwrite unknown original values.
No Azure automation or --settings secret arguments are implemented here.

After an approved change, require a new read-only qualification with a NEW disabled
attempt. Preserve historical attempt 2683eeb5-903c-4434-b262-70452fb3040e and every
other private record; do not reuse, inspect, arm, extend or reset them. An authenticated
API probe, live controller, deployment and rollback each retain their separate owner
authority. Suspected exposure requires stopping, clearing channels and separately
approved rotation of the shared value across vault/Azure/operator channel; do not
paste evidence of a compromised value into chat or revert to it.

## Actual offline evidence

```powershell
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp31_service_key.py -v
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp22_preparation.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp21_controller.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp19_operator_handoff.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp18_research_launcher.py -q
```

New Python runner: 3 tests, 2 static passes and 1 runtime skip because host execution
policy blocked -File; no bypass attempted. Existing suites: 54+38+19+19 = 130 passes,
no skips. Total: 132 passed, 1 skipped, exit 0. The installed-CLI compatibility test
ran against fake authentication/network boundaries. Both new PowerShell files parsed
with zero errors in 5.1.26100.9711; build 5.1.26100.9444 is unavailable. No runtime
helper success is claimed. The executable fake-input suite remains owner-pending.

Its 13 cases cover masked import through a mocked input boundary, exact current
process assignment, no key in returned/error streams, buffer disposal, clear,
invalid/empty/uppercase/newline inputs, input exception, clearing stale values when
prerequisites fail, valid replacement and preservation of an unrelated sentinel.
Static checks verify no clipboard/file/network/process-launch/User/Machine mutation
surface and no generation path. Static checks do not establish runtime behavior;
the skipped fake-input suite must pass before importing a real value. UTF-8/LF/no-BOM,
Python syntax, PowerShell parsing and git diff --check passed.

No actual secret generated/acquired, no Azure setting changed, authenticated API not
tested, live controller not invoked, historical attempts untouched. Existing API,
controller/recovery and diagnostic code is unchanged. Owner-selected vault/transfer
and runtime qualification remain necessary. `approvalEnabled=false`;
`liveControllerInvoked=false`; `liveAttemptReady=false`;
`RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED=True`;
`PUBLIC_RUNTIME_VERIFIED=False`.

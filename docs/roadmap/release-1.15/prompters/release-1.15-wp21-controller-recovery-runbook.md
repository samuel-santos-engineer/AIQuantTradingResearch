# Release 1.15 WP21: concrete controller and independent recovery

WP21 is implementation and offline qualification only. No Azure management request,
application HTTP, deployment, Start/Stop, image publication, provider request,
import, UI or production activity is authorized or performed. All new text is
UTF-8 without BOM with LF. Current-main baseline is merged WP19 commit
914c7bb8937c0d7c92fa62f590cfeb570981a9b9; WP20 PR #410 is still open and is not a
runtime dependency. This report supersedes WP20's unqualified-controller result;
it does not change that older offline script or authorize a live attempt.

## What is implemented

- `wp21_controller.py`: default offline qualification; explicit future `--execute`
  still requires a fresh private authorization file and mandatory WP18 real-script
  gate, then concrete Azure operations, immediate health, gated one-time snapshot
  workers and unconditional restoration.
- `wp21_runtime.py`: exact research resource/plan/image allowlists; private ACL and
  local-directory checks; exclusive fsynced baseline/consumed records; management
  transport; full capture/comparison; existing WP16 health launch; recovery logic.
- `wp21_recovery.py`: standalone recovery, importing runtime only. It works without
  the controller executable. Default does nothing; `--recover` requires the original
  private baseline/authority and matching recovery/runtime/entry code identities.
- Three PowerShell 5.1 operator entries: offline qualification/preparation, a guarded
  future live entry, and independent recovery. No execution-policy override exists.

Exact scope is research app `aiqr115researchstagingwus27f5eabb5`, resource group
`rg-aiq-r112-wp05-wus2-7f5eabb5`, existing West US 2 plan
`asp-aiq-r112-wp05-wus2-7f5eabb5`, SKU F1. The subscription/resource IDs are fixed
from committed provisioning evidence. The approved registry repository is established
in the committed Release 1.14 publication evidence; the reviewed digest is pinned
in merged WP19. Together they identify the existing approved research artifact:
`sha256:d3221dbe0a4ef6dfba37e0c61c06bb3bc9d9ee2b6f776d7738c35aece32be26f`.
WP20's earlier local registry readback verified Linux/amd64 and reviewed UID/entry;
WP21 performs no fresh registry or Azure request and does not claim present image
availability or runtime identity from offline source evidence.

The historical WP17 private helper/attempt records remain inaccessible under the
current Codex filesystem permissions. No identity/ACL workaround was used to read
them. Earlier owner-supplied evidence says the attempt is consumed and exact rollback
was independently verified. These new controllers do not import or reference those
files. Their state root is exclusively `%LOCALAPPDATA%\AIQ-WP21\<UUID>`; they cannot
load the historical WP17 state location through the supported CLI. Historical
private hashes could not be independently rechecked here; no write targeted them.

## Offline operator commands: safe to run after review in the intended session

Use the reviewed WP21 checkout root. Do not run an unreviewed live command.

```powershell
$qualification = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1 | ConvertFrom-Json
$qualification | ConvertTo-Json -Depth 8
```

This runs the fixed offline suite (all Azure/HTTP boundaries mocked), checks its
successful test count, and invokes merged WP19/WP18 real no-network preflight.
It reports code fingerprints, sanitized operator policy/provenance information,
`privateControllerQualified` with its mocked-test basis, and `liveAttemptReady`.
The latter remains false: no private authorization, service-key channel or fresh
live baseline can be presumed from these tests. Do not treat exit 0 as live authority.
Only a future valid HTTPS 200/Alive can establish process health for that attempt.

Optional offline preparation creates only a fresh private directory and a **disabled**
authorization template. It does not query Azure, arm an attempt or deploy:

```powershell
$prepared = powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1 -PreparePrivateAttempt | ConvertFrom-Json
$prepared | ConvertTo-Json
$env:AIQ_WP21_ATTEMPT_DIRECTORY = Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $prepared.attemptId
```

Preparation creates a new UUID directory with owner/System-only ACL, no inherited
broader access, and an `approval.json` with `explicitLiveApproval=false` and
`allowRecovery=false`. It never modifies an existing attempt or historical evidence.
The runtime independently checks local fixed location, no symlink/junction/UNC,
owner identity and all allow ACEs before private input or Azure operations. Do not
share that directory, its baseline, approval file or raw transport output.

If PowerShell `-File` is blocked, stop. Use only the safe interactive read-only
inspection from WP19. A Codex sandbox denial is distinct from Windows policy.
Do not change policy, switch identity/interpreter, evaluate script text, or bypass
Group Policy to make these entries execute. Exact requested PowerShell build 9444
is not installed; available 5.1.26100.9711 parses all three entries. Successful full
WP21 operator-session execution remains pending until the owner returns evidence.

## Future live authorization and private prerequisites ? NOT AUTHORIZED BY WP21

After PR review/merge and owner offline qualification, request explicit authority
for **one named UUID**, the exact research resource/image, the bounded health and
two unauthenticated instrument checks, and unconditional independently verified
rollback. Keep the exact code under review unchanged. The owner must privately
review/edit only the unused authorization template: `attemptId`, exact `target`,
`plan`, `image`, all `code` hashes, UTC `issuedUtc`/`expiresUtc` (window <=1 hour),
`explicitLiveApproval=true`, `allowRecovery=true`. Approval is rechecked immediately
before persistence/mutation, so expiration or code changes during capture stop the
attempt. A generic `--execute`, report, merge or approval-reference string is not
sufficient. The record is a human authorization statement, not an authentication
mechanism against its owner.

No private key is supplied in arguments or reports. The controller requires either
an already configured research-only service key in the privately captured baseline,
or an existing secure operator-to-process memory channel `AIQ_WP21_SERVICE_KEY`.
Never supply provider credentials or enter literal keys in shell history. No new
key generation or persistent service-key file occurs. If neither safe channel is
available, stop before mutation. Existing baseline settings/connection-string values
are necessarily captured privately for exact comparison/restoration; they never
enter source, shareable reports, new request-body files or command-line arguments.
The operator is responsible for retention/deletion of these private records under
existing policy; no automatic cleanup touches them.

Before any mutation the controller captures and independently recaptures: exact
image/startup and full web properties, all settings, connection strings, slot-setting
name lists, full logging configuration, HTTPS-only/network metadata, slot inventory,
Running state and existing F1 plan identity. Missing fields, unexpected slots,
unsupported host/region, drift or any query failure stop before arming. Only after
both captures agree are new immutable `baseline.json` and exclusive `consumed.json`
created/fsynced. The fixed UUID/path and exclusive files reject replay; never delete,
reset or choose another directory to retry under the same approval.

The future live entry uses WP19, which invokes the WP18 guarded launcher; the
controller repeats the same real gate:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp21ResearchAttempt.ps1 -ApproveNamedAttempt
```

Candidate application can change only this research app's image/startup, settings,
HTTPS-only and state. Recovery also restores original kind and any logging/slot-flag
drift to the captured original values.
UID 1654:1654, port 52731, all-interface binding, platform HTTPS termination, exact
non-durable staging/storage/role/mount guards and private SQLite behavior are
preserved. No auto-created database, data seed, provider composition or UI update.
Immediately after Azure Start, the existing collector runs with <=3 attempts,
<=60 seconds each, 180-second total and existing 190-second inner parent limit
(200-second outer launch ceiling). TLS and exact 200/Alive remain mandatory.

Only after valid health does a fresh immutable health marker permit one bounded
unauthenticated POST each for BTC/USD and ETH/USD. Each worker claims its one-time
marker before sending, has a 10-second owned parent limit and 5-second response
budget, sends no key, follows no redirects and validates the existing 401 DTO.
No authenticated snapshots are authorized or implemented in WP21. A 503 cannot
count as successful unauthenticated rejection; authenticated 503 DATASET_NOT_READY
would mean reachable guarded API with absent data, but requires separate future
scope and is not exercised here. Do not weaken authentication to obtain 503.

## Independent recovery: preserve this command before any live attempt

The original authorization explicitly permits recovery, including after expiry.
Use the same private attempt directory. This command does not import the controller:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Restore-Wp21ResearchBaseline.ps1 -ApproveNamedAttempt
```

It validates original target, image, attempt/baseline hash and pinned runtime,
recovery and recovery-entry identities, stops only the research app, restores the
original image/startup/settings/HTTPS-only/kind and state, conditionally restores
captured logging/slot flags if drifted, and independently recaptures all categories.
Unchanged connection strings/slot inventory/plan metadata must still match;
unexpected external drift there is critical rather than deleting resources or
changing unrelated configuration. Audit files are new-only per recovery call.
Repeated recovery uses the same immutable baseline safely and does not deploy the
candidate. A current controller PID blocks external recovery to avoid racing its
writes. After a hard crash the PID must be inactive; PID reuse intentionally fails
closed for owner review. Do not edit the record to defeat that check.

The controller catches timeout, partial-deploy and interrupt paths and always calls
recovery. This cannot survive a hard process/host kill: independent recovery is
mandatory in that case. Do not stop unrelated processes. A Stop acknowledgement is
not kernel termination proof. Private file fsync and ACL checks are safeguards, not
proof of host power-loss durability. Exact configuration rollback cannot restore
lost ephemeral datasets. `CRITICAL_RECOVERY_UNVERIFIED` overrides apparent health
success; fixed `unrestoredFields` categories identify remaining mismatches without
exposing values. Retain private evidence and stop further probes/deployments.

## Evidence and remaining limits

```powershell
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp21_controller.py -v
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp19_operator_handoff.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp18_research_launcher.py -q
# From tests/runtime/release_1_15:
python -B -m unittest test_wp07_staging_readiness.ResearchHealthWaitTests -q
```

37 WP21, 19 WP19, 19 WP18 and 12 WP16 Python tests passed on the main-based checkout
(87 total). Separately, all 18 tests on the still-unmerged WP20 branch passed without
modifying that branch. They are not represented as merged-main tests. Earlier test
assertions expected a different exception/redaction string and were corrected to
check actual validator rejection/fixed output; a WP16 test invocation from the wrong
working directory failed import and passed when run from its documented directory.
No test was skipped or policy override used for these qualifications.

The installed Azure CLI transport signature and bytecode were inspected locally,
without profile/token acquisition or requests. Runtime uses its authenticated REST
transport in the installed interpreter, with request arguments/bodies over stdin,
no command-history invoke and no printed tokens. Live SDK transport, Azure schemas,
private ACL preparation, baseline capture/restoration, service-key delivery and
actual HTTPS remain unverified; mocks do not prove those facilities. No dependency
was installed. Source image identity is pinned from offline repository/prior evidence,
not freshly verified by network in WP21.

Return only the sanitized offline JSON and optional prepared UUID. Review/redact
unexpected personal identifiers/paths first; never return approval/baseline contents,
keys, environment dumps or raw Azure/PowerShell errors. Operator-session execution
and private prerequisites are genuine remaining inputs, not flags to manually flip.

`AZURE_DEPLOYMENT_NOT_RUN` (WP21); `privateControllerQualified=true` on the documented
offline mocked-test basis; `liveAttemptReady=false` until the stated prerequisites
and separate live authority are satisfied; `RESEARCH_PROCESS_ALIVE=UNVERIFIED`;
`AUTHENTIC_DATA_NOT_IMPORTED`; `PUBLIC_RUNTIME_VERIFIED=False`.


Management worker crash safety: the parent records each owned worker PID and Windows
process creation time before supplying its stdin request. A journal failure kills
that owned child before any request is supplied. Each child has a 40-second internal
exit watchdog; the parent has a 45-second communicate limit and kills on errors or
interrupts. Independent recovery quiesces journaled workers using stable kernel
handles and creation times before restoration; it never kills an unrelated reused
PID. Native ownership is tested on one bounded, harmless owned local Python sleeper,
plus mocked PID-reuse cases; no live management worker is started. A locally terminated request does not prove a remote
control-plane operation was cancelled; exact post-restore comparison remains required
and live timing/recovery behavior remains unverified. Run recovery commands serially;
concurrent operator recovery invocations are not qualified by these tests.


For a later separately authorized attempt, `result.json` and `recovery-<UUID>.json`
contain only sanitized outcomes, timestamps, attempt identity and mismatch category
names. The owner may return those after inspection. Never return `baseline.json`,
`approval.json`, worker journals, settings/connection values or the process environment.
A recorded process-alive result describes the candidate at its health timestamp;
after unconditional rollback it is not a claim about the currently deployed image.


The operator Python must be 3.12+ (existing host 3.13); the version is reported.
This supports explicit junction rejection using the standard library. No package
or interpreter was installed. Unsupported versions fail closed with a fixed reason.

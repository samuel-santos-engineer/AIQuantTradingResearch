# Release 1.15 WP22 - Disabled named attempt and read-only baseline validation

Status: implementation for owner review; not live deployment authority.
Baseline: main `386be2a0a6758522f2c73c138df0bff10a61d943` (merged PR #411).
All authored text is UTF-8 with LF line endings. Windows PowerShell 5.1 only.

## Boundary

WP22 adds no deployment mode. The default entry runs only offline unit tests.
Preparation delegates to WP21's existing fresh UUID/owner-and-System ACL path.
Approval flags remain false. No historical attempt is reused or reset. The live
controller and independent recovery files are unchanged.

Explicit read-only inspection requires an existing authorized Azure CLI session
and a newly prepared private attempt. It uses the installed Azure CLI REST bridge,
with a second allowlist permitting exactly eight ARM operations per capture:

| Method | Exact resource suffix | Resource |
| --- | --- | --- |
| GET | empty | fixed research site |
| GET | empty | fixed existing F1 plan |
| GET | /config/web | fixed research site |
| POST | /config/appsettings/list | fixed research site |
| POST | /config/connectionstrings/list | fixed research site |
| GET | /config/logs | fixed research site |
| GET | /config/slotConfigNames | fixed research site |
| GET | /slots | fixed research site |

POST list operations read secrets but do not modify Azure. Request bodies,
stop/start, PUT, PATCH, substituted resources and pagination are rejected before
transport. No automatic login, role changes, provider requests, application HTTPS
probes, registry activity or UI operations occur. Authentication/RBAC failure
blocks the operation with a fixed sanitized failure category.

## Coverage and evidence limits

Two captures must match completely. They include original image/startup and full
web properties, full settings, connection values and types, slot flags, logging,
HTTPS-only and public-network state, empty slot inventory, plan identity/full SKU,
site kind/region and original Running state. Plan location is also checked. Missing,
malformed, paginated, wrong-target, non-F1, non-running or drifting captures block.
Strict equality can also reject benign changing web properties; do not weaken it
without reviewing actual sanitized failure evidence and the required contract.

Full configuration remains in memory. Only WP21 management-worker ownership records
and new-only sanitized `wp22-observation-<UUID>.json` records are stored under the
private ACL directory. No WP21 `baseline.json` or `consumed.json` is written. No
configuration hash or secret-bearing raw exception is published. Observation IDs
are separate from the single-use live attempt. A present printable 32-256 character
service key in the captured baseline or existing process environment produces only
a boolean; key material is neither copied nor persisted. It does not prove live
authentication and does not enable approval.

Each of 16 sequential ARM calls inherits WP21's 40-second worker watchdog and
45-second parent timeout; inspection is bounded by those limits, ACL/preflight
limits and the disabled template's 15-minute initial freshness window. Freshness,
code identities, disabled approval and absence of consumed/live baseline files are
rechecked after reads. A partially completed inspection is not a live attempt;
there has been no configuration mutation to roll back. Do not delete old evidence.

Real ARM schemas have NOT been observed in this Codex session. Microsoft REST
reference retrieval was unavailable during implementation; no schema compatibility
claim is based solely on fixtures. WP22 reuses the existing WP21 capture schema and
fails closed on any mismatch. Live service-key availability and full rollback
sufficiency remain UNVERIFIED until the operator returns actual read-only evidence.
This preparation observation is not the live immutable baseline: WP21 must capture
and compare a fresh baseline again immediately before any separately approved write.

## Operator commands from repository root

Use the reviewed PR checkout, not a different checkout with missing WP21 files.
Do not use execution-policy switches or change policy, profile, registry or identity.
If policy refuses the entry, stop and report the block in your authorized session.

Offline WP22 checks:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1
```

WP21 controller qualification (offline tests and real operator preflight):

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp21ControllerQualification.ps1
```

Optional fresh disabled attempt preparation (no Azure operation):

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -PreparePrivateAttempt
```

Keep the returned UUID. Set the existing WP21 directory variable locally without
printing its resolved value. Replace the placeholder with that UUID:

```powershell
$attemptId='<returned UUID>'
$env:AIQ_WP21_ATTEMPT_DIRECTORY=Join-Path (Join-Path $env:LOCALAPPDATA 'AIQ-WP21') $attemptId
```

In the already authenticated, owner-authorized Azure session, explicitly request
read-only control-plane validation within the template freshness window:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp22AttemptPreparation.ps1 -InspectAzureReadOnly
```

Do not print approval/configuration files or tokens. Return only sanitized JSON.
A failure reason covers prerequisite/auth/schema/drift failures without revealing
settings names or raw Azure errors. If expired, prepare a new disabled attempt;
do not silently update approval/code identities or recycle a consumed UUID. If the
service-key availability boolean is false, leave it false and request owner review;
never generate or paste a key into command arguments, reports or source files.

Even a successful read-only report has `approvalEnabled=false`,
`liveControllerInvoked=false` and `liveAttemptReady=false`. A later owner decision
must approve one specific UUID, exact image and research app, bounded health and
unauthenticated checks, and unconditional independent rollback. The owner may then
privately arm it following the WP21 runbook. No such live command is authorized here.
Independent recovery must be available before any future attempt.

## Recorded offline evidence

- WP22: 19 no-network tests passed; every Azure and operator-policy boundary mocked.
- WP21: 38 tests passed, including owned local Worker termination qualification.
- WP19: 19 tests passed. WP18: 19 tests passed. Total: 95 focused tests.
- Windows PowerShell 5.1.26100.9711 parser: zero new-entry parse errors.
  Requested build 5.1.26100.9444 was not available for execution.
- Actual new PowerShell `-File` execution was refused by this Codex session's
  execution policy. No bypass or alternate identity/interpreter was attempted.
- No private UUID was prepared and no real Azure read was performed by Codex.
- Owner-provided WP21 operator result: 38 tests and real preflight passed under
  Process RemoteSigned; this is historical operator evidence, not WP22 execution.
- No application code, dependencies, WP21 files, Azure state or public gates changed.
- Preservation: implementation lives in an isolated worktree; the original dirty
  checkout was neither switched nor edited. Its pre-existing untracked WP22 prompt
  remains there and is not swept into this PR.

Tests cover read-only inventory, body/mutation/substitution rejection, pagination,
plan region, disabled/fresh template, code mismatch, consumed/missing prerequisites,
policy/auth failures, malformed/missing capture categories, drift, post-read expiry,
boolean-only service-key channels, secret redaction/report bounds, new-only evidence
and absence of live baseline/consumed creation. The existing WP21 suite retains the
fresh UUID and ACL-preparation tests; WP22 delegates to that implementation.

Remaining obligations: real operator WP22 preflight and preparation, authenticated
read-only schema/baseline validation, secure key-channel evidence, PR review and
separate named live approval. Mocked captures do not prove Azure rollback behavior,
process health, authentic data, filesystem durability or production readiness.

`RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED=True`;
`PUBLIC_RUNTIME_VERIFIED=False`; live deployment/public activation remain NO-GO.

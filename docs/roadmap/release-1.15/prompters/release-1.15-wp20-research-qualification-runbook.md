# Release 1.15 WP20: research-only qualification handoff

Status: offline preparation only. No live authority is granted by WP20, a merged
PR, an approval reference, a passing preflight or these commands. UTF-8/no BOM/LF.

## Offline owner command

From the reviewed WP20 checkout in the intended authorized PowerShell 5.1 session:

```powershell
$report = Join-Path $env:TEMP ('aiq-wp20-' + [guid]::NewGuid().ToString('N') + '.json')
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp20ResearchQualification.ps1 -ReportPath $report
```

This uses merged WP19/WP18 metadata, signatures, Internet marks, parse checks,
normal first-Python PATH resolution and the real offline readiness script/helper.
It includes provenance for the WP20 entry. No live mode exists on this script or
its Python CLI. JSON is sanitized, at most 8 KiB, new-file-only UTF-8/LF. Return
only reviewed JSON, never raw errors, personal paths, keys or controller contents.
`LOCAL_PREFLIGHT_PASS` only means local execution succeeded. This handoff explicitly
reports `privateControllerQualified=false` and `liveAttemptReady=false` until a
fresh private-controller/recovery review is completed outside this offline CLI.
Do not treat exit 0 as permission or readiness to deploy.

The owner previously returned a WP19 pass under process RemoteSigned. This does
not establish the policy of a different session or Codex tool account. The available
Codex session remains Restricted and refuses real .ps1 execution. No policy change,
identity switch, bypass or alternative-interpreter workaround is permitted. If the
entry is blocked, follow the safe interactive inspection in the merged WP19 runbook;
ask the administrator to resolve signing/policy through organizational procedures.
Do not invoke the helper directly to evade the blocked PowerShell route.

## Artifact and controller dependencies

Existing approved research artifact:
`sha256:d3221dbe0a4ef6dfba37e0c61c06bb3bc9d9ee2b6f776d7738c35aece32be26f`.
On 2026-10-29, registry manifest and config bytes were read back and SHA-256
verified; config is Linux/amd64, UID 1654:1654, entrypoint
`/bin/sh /app/research-bootstrap.sh`. Exact repository reference is retained in
historical private operational records, not republished here. No rebuild, retag,
push or Azure observation occurred. Registry availability is not runtime attestation.

Codex's current filesystem permissions deny access to the historical private WP17
helper modules. No identity change or permission workaround was used to retrieve
them. Earlier reviewed responsibilities were baseline capture/comparison, exact
research configuration, Start, health, gated snapshots and independent restoration.
The WP17 consumed record is immutable historical evidence and must never be reset,
deleted or copied into a new attempt as approval.

A fresh, owner-accessible private controller and separate recovery entry must be
reviewed before any live execution. None is fabricated or claimed qualified here.
`wp20_research_qualification.qualify` supplies a tested sequencing/one-record guard
for that future controller; it contains no Azure adapter and has no live CLI.
Its injected operations must be concretely implemented/reviewed. Mocked sequencing
tests do not prove Azure capture, storage protection, actual HTTP or recovery.
The exact-resource checks, restricted storage, real collector/DTO wiring and
independent recovery therefore remain explicit unresolved integration obligations.

The qualified controller must use one fixed, new WP20 record in a restricted private
directory, exclusive creation after baseline verification, and refuse reuse. It
must not generate another record to retry under the same approval. Record paths,
controller source, current service configuration and any key channel stay private.
Do not expose credentials in arguments, logs or new service-key files. A secure
existing server-side service-key channel is a prerequisite even if authenticated
snapshot checks are not approved; if unavailable, stop before deployment.

## Proposed single live procedure: requires separate approval

1. Obtain fresh explicit owner authority identifying target, artifact, one WP20
   attempt, health/optional snapshot permissions and unconditional rollback. Run
   the offline command above in the same permitted operator session. Review the
   new private controller and independent recovery entry before proceeding.
2. Capture full current baseline privately: exact research image/startup, all app
   settings and slot flags, logging, HTTPS-only, slot metadata and Running state.
   Verify exact existing research resource/region/F1 plan and immutable artifact.
   Independently re-read/compare every captured category. Any unavailable query,
   mismatch or unsafe credential/baseline storage stops before mutation.
3. Create the fresh exclusive WP20 record only after those checks. Apply only the
   already published research image and strictly required protected configuration,
   preserving unrelated settings and all existing UID/mount/role/storage/auth/TLS
   guards, port 52731, all-interface bind and platform HTTPS termination. No UI.
4. Immediately after Start, invoke the merged WP16 collector: at most three anonymous
   HTTPS GET /health requests, at most 60 seconds each within 180 seconds total,
   with the existing 190-second owned-child parent bound. Require valid TLS, no
   redirect and exact 200/Alive. A local launcher failure is not an Azure crash.
5. Only after valid health and separately approved snapshot scope: at most one
   unauthenticated POST expecting a validated 401, and at most one authenticated
   request each for BTC/USD and ETH/USD if the existing secure key channel is
   available. Use existing strict schema/client validation. Do not log candle
   payloads or keys. A validated 503 DATASET_NOT_READY is reachable guarded API,
   not chart acceptance. Stop further checks on any malformed response/failure.
6. Restore the exact original baseline and Running state even after success.
   Independently re-read and compare all categories. No second deployment, timeout
   change or guard weakening. On uncertainty, use the separately reviewed recovery
   entry and report residual differences; never infer recovery from Stop alone.
   Stop acknowledgement is not kernel termination proof; lost ephemeral data is
   not recovered by configuration rollback.

Only after fresh live approval and all dependencies are qualified, the existing
merged WP19 entry is the supported live launch route (no live command is run here):

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp19ResearchHandoff.ps1 -ApproveLiveAttempt -ApprovalReference $freshApprovalReference -ApprovedImageDigest $approvedResearchDigest -ControllerPath $freshQualifiedController -ReportPath $freshReport
```

The private controller must use the reviewed sequencing guard or independently
prove equivalent enforcement, while WP19/WP18 repeat the local gate. The guard
accepts only explicit approval for the exact research app/digest; optional snapshot
permissions default off. Its result includes only sanitized outcome enums. It
restores after partial deploy failures, retains the consumed marker, compares
baseline after recovery failure, and never labels unverified recovery successful.
These are library guarantees conditional on honest, correctly qualified operations,
not an Azure authorization boundary or replacement for private adapter review.

## Offline evidence and next approval

Commands from repository root:

```powershell
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp20_research_qualification.py -v
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp19_operator_handoff.py -q
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp18_research_launcher.py -q
```

18 WP20, 19 WP19 and 19 WP18 tests passed (56 total). Windows PowerShell
5.1.26100.9711 parses the scripts; exact build 9444 is not installed. Actual current
Restricted-policy refusal is verified. No end-to-end WP20 owner execution, actual
baseline capture, snapshot credential channel, Azure deployment or HTTP probe is
claimed. No new private controller was tested. Owner files and historical evidence
are preserved. The focused PR must remain open/unmerged for review.

Separate requested live scope, conditional on new controller/recovery qualification:
one attempt on research app aiqr115researchstagingwus27f5eabb5 in
rg-aiq-r112-wp05-wus2-7f5eabb5, existing West US 2 F1 plan
asp-aiq-r112-wp05-wus2-7f5eabb5, the exact artifact above, complete private baseline,
bounded health observation, optionally one unauthenticated 401 check, and exact
independently verified rollback even after success. Authenticated snapshots remain
a separate optional permission and require a verified secure channel. No UI,
provider/import, synthetic initialization, production, publication or paid resources.
Approval alone does not resolve missing controller/recovery prerequisites.

`RESEARCH_PROCESS_ALIVE=UNVERIFIED`; `AUTHENTIC_DATA_NOT_IMPORTED`;
`PUBLIC_RUNTIME_VERIFIED=False`; `AZURE_DEPLOYMENT_NOT_RUN` (WP20).

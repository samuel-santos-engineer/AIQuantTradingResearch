# Release 1.15 WP19: human operator handoff

These Windows PowerShell 5.1 scripts do not alter execution policy. Use the reviewed
PR checkout/repository root. All authored text is UTF-8 without BOM with LF.

## Offline only

Inspect the scripts first. In the owner's intended authorized session:

```powershell
$report = Join-Path $env:TEMP ('aiq-wp19-' + [guid]::NewGuid().ToString('N') + '.json')
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Test-Wp19OperatorPreflight.ps1 -ReportPath $report
```

This reports policy scopes/precedence and effective policy, PowerShell version,
Python availability, signature status, Internet-zone-mark presence, SHA-256 and
parse-error counts for the two handoff scripts and readiness entry. It runs the
existing WP18 real offline preflight, including the readiness script and Python
helper without live flags. It performs no Azure, application HTTP, credential,
provider or import operation. Report files are new-only, UTF-8/LF, at most 8 KiB;
existing files are never overwritten. The same sanitized JSON is printed.

`OFFLINE_PREFLIGHT_PASS` means local execution prerequisites passed. It does not
establish research process health, dataset availability or public chart acceptance.
`OFFLINE_PREFLIGHT_BLOCKED` or `HANDOFF_BLOCKED` is a safe refusal, exit 1.
A PowerShell entry rejection may occur before a report can be written.

With effective Restricted policy, `-File` is expected to be blocked. Do not change
policy or switch interpreter/identity to run around the restriction. The owner or
administrator may inspect these read-only commands interactively:

```powershell
$PSVersionTable.PSVersion
Get-ExecutionPolicy -List
Get-ExecutionPolicy
[bool](Get-Command python -CommandType Application -ErrorAction SilentlyContinue)
Get-AuthenticodeSignature -LiteralPath .\eng\docker\release-1.15\Test-Wp19OperatorPreflight.ps1 | Select-Object Status
```

These commands inspect prerequisites; they do not execute the blocked script.
Ask the administrator to review organizational Group Policy, script provenance and
required signing through approved procedures. No policy/signature remediation is
authorized or automated here. Rerun the exact offline script only when permitted.
A Codex tool/sandbox denial is separate from Windows execution policy: report its
stated reason, preserve the scripts and let the owner choose whether to run them.
Do not ask Codex to change identity or escape sandbox controls. Do not invoke the
shared Python helper directly to evade a blocked PowerShell entry.

## Optional live handoff: not authorized by WP19

Only after fresh explicit owner approval of one specific research-only attempt,
inspect and qualify the existing private controller and retain its separate
recovery invocation. The consumed WP17 controller must not be reused by changing
or deleting its attempt record. If no qualified, unused controller is available,
stop; these scripts do not fabricate one or qualify its deployment behavior.

The approved controller must enforce the exact existing research staging target,
reviewed immutable image, complete privately captured/currently compared baseline,
unchanged UID/storage/authentication/transport guards, one attempt, bounded health
observation (three requests/180 seconds, 190-second parent), snapshots only after
valid health, and exact independent rollback including original state. No UI,
production, publication, paid resources, provider requests or imports. No terminal
or image-attestation gate is added. Controller-owned private evidence and recovery
remain mandatory; this wrapper does not replace them or interrupt rollback.

After that separate approval, with operator-supplied variables (never API keys):

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp19ResearchHandoff.ps1 -ApproveLiveAttempt -ApprovalReference $approvalReference -ApprovedImageDigest $approvedImageDigest -ControllerPath $approvedController -ReportPath $report
```

Use a fresh report filename. Approval reference is a nonpersonal uppercase
alphanumeric/hyphen identifier, 4-64 characters. The digest must match the existing
reviewed research artifact; controller path must be an existing absolute `.py`
path. These arguments are the operator's explicit statement of fresh approval,
not authentication or proof of controller qualification. There is no default live
mode. The offline report must pass before the existing WP18 guarded launcher is
called; WP18 then repeats its gate. Controller stdout/stderr is not copied into
the shareable report; inspect its own restricted evidence for outcomes/recovery.
`CONTROLLER_RETURNED_REVIEW_PRIVATE_EVIDENCE` does not claim health or acceptance.

## Return evidence and qualification limits

Return the sanitized JSON report only. Inspect it first; redact any unexpected
personal identifiers, filesystem paths, secrets or private resource details. Do
not share raw PowerShell/Azure errors, controller contents, app settings, environment
dumps or credential files. If blocked before JSON exists, return only effective
policy, scope values, PowerShell version, Python available/not available, signature
status and a short sanitized error category. Remove paths/usernames from screenshots
or copied errors. Signatures/zone marks are provenance observations, not trust proof.

WP19 offline verification: 18 handoff tests and 19 existing WP18 tests passed.
Commands from repository root:

```powershell
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp19_operator_handoff.py -v
python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp18_research_launcher.py -q
```

Available host: Windows PowerShell 5.1.26100.9711 (requested build 9444 unavailable).
All five scopes Undefined, effective Restricted. All three scripts parse; actual
handoff `-File` attempts are refused by execution policy before their bodies run.
No successful full operator entry-to-Python report run is claimed. Success/live
paths are mocked offline; human execution under an allowed policy remains pending.
An initial test expected a different policy error identifier; it was corrected to
classify the observed disabled-scripts/UnauthorizedAccess refusal without exposing
raw stderr. No policy change, identity change or alternative execution route was
used to make scripts run. Scripts remain for owner review in an open/unmerged PR.

`AZURE_DEPLOYMENT_NOT_RUN` (WP19); `RESEARCH_PROCESS_ALIVE=UNVERIFIED`;
`AUTHENTIC_DATA_NOT_IMPORTED`; `PUBLIC_RUNTIME_VERIFIED=False`.


Operator follow-up: the original wrapper returned LOCAL_ARGUMENT_OR_INTERPRETER_ERROR.
A read-only command-resolution check found two Python application matches, making
`$python.Source` an array that PowerShell cannot invoke with `&`. Both wrappers now
select the first application in ordinary PATH order, consistent with normal Python
resolution; no interpreter/policy override is introduced. A regression check verifies
one string executable equal to the first normal match. Updated qualification:
19 handoff tests plus 19 WP18 tests pass. The operator must rerun the offline entry;
successful full script execution remains unverified under the Codex host's Restricted
policy. No live action was performed for this correction.

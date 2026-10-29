#Requires -Version 5.1
# UTF-8/LF. Human operator entry; never changes policy.
[CmdletBinding()]
param([switch]$ApproveLiveAttempt,[string]$ApprovalReference,[string]$ApprovedImageDigest,[string]$ControllerPath,[string]$ReportPath)
$ErrorActionPreference='Stop'
Set-StrictMode -Version 2.0
try {
    $helper=Join-Path $PSScriptRoot '../../azure-cli/r1.15-qualification/wp19_operator_handoff.py'
    $python=Get-Command python -CommandType Application -ErrorAction Stop | Select-Object -First 1
    $arguments=@('-B',$helper)
    if(-not $ApproveLiveAttempt -or [string]::IsNullOrWhiteSpace($ApprovalReference) -or [string]::IsNullOrWhiteSpace($ControllerPath) -or [string]::IsNullOrWhiteSpace($ApprovedImageDigest)){throw 'Live approval incomplete.'}
    $arguments+=@('--approve-live-attempt','--approval-reference',$ApprovalReference,'--approved-image-digest',$ApprovedImageDigest,'--controller',$ControllerPath)
    if($ReportPath){$arguments+=@('--report-path',$ReportPath)}
    & $python.Source @arguments
    exit $LASTEXITCODE
} catch {
    Write-Output '{"result":"HANDOFF_BLOCKED","reason":"LOCAL_ARGUMENT_OR_INTERPRETER_ERROR","PUBLIC_RUNTIME_VERIFIED":false}'
    exit 1
}

#Requires -Version 5.1
# UTF-8/LF. Human operator entry; never changes policy.
[CmdletBinding()]
param([string]$ReportPath)
$ErrorActionPreference='Stop'
Set-StrictMode -Version 2.0
try {
    $helper=Join-Path $PSScriptRoot '../../azure-cli/r1.15-qualification/wp19_operator_handoff.py'
    $python=Get-Command python -CommandType Application -ErrorAction Stop
    $arguments=@('-B',$helper)
    if($ReportPath){$arguments+=@('--report-path',$ReportPath)}
    & $python.Source @arguments
    exit $LASTEXITCODE
} catch {
    Write-Output '{"result":"HANDOFF_BLOCKED","reason":"LOCAL_ARGUMENT_OR_INTERPRETER_ERROR","PUBLIC_RUNTIME_VERIFIED":false}'
    exit 1
}

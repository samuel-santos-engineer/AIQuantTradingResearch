#Requires -Version 5.1
# UTF-8/LF. Offline only; no live mode and no policy changes.
[CmdletBinding()]
param([string]$ReportPath)
$ErrorActionPreference='Stop'
Set-StrictMode -Version 2.0
try {
    $helper=Join-Path $PSScriptRoot '../../azure-cli/r1.15-qualification/wp20_research_qualification.py'
    $python=Get-Command python -CommandType Application -ErrorAction Stop | Select-Object -First 1
    $arguments=@('-B',$helper)
    if($ReportPath){$arguments+=@('--report-path',$ReportPath)}
    & $python.Source @arguments
    exit $LASTEXITCODE
} catch {
    Write-Output '{"result":"LOCAL_PREFLIGHT_BLOCKED","reason":"LOCAL_ARGUMENT_OR_INTERPRETER_ERROR","PUBLIC_RUNTIME_VERIFIED":false}'
    exit 1
}

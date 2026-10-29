#Requires -Version 5.1
# UTF-8/LF. No execution-policy changes; operator session only.
[CmdletBinding()]
param([switch]$PreparePrivateAttempt)
$ErrorActionPreference='Stop'
Set-StrictMode -Version 2.0
try {
    $python=Get-Command python -CommandType Application -ErrorAction Stop | Select-Object -First 1
    $operations=Join-Path $PSScriptRoot '../../azure-cli/r1.15-qualification'
    if($PreparePrivateAttempt){
        & $python.Source -B (Join-Path $operations 'wp21_controller.py') --prepare-private-attempt
    } else {
        & $python.Source -B (Join-Path $operations 'wp21_controller.py')
    }
    exit $LASTEXITCODE
} catch {
    Write-Output '{"result":"HANDOFF_BLOCKED","PUBLIC_RUNTIME_VERIFIED":false}'
    exit 1
}

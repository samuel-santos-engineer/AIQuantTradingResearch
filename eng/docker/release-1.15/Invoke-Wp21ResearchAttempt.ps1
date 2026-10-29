#Requires -Version 5.1
# UTF-8/LF. No execution-policy changes; operator session only.
[CmdletBinding()]
param([switch]$ApproveNamedAttempt)
$ErrorActionPreference='Stop'
Set-StrictMode -Version 2.0
try {
    $python=Get-Command python -CommandType Application -ErrorAction Stop | Select-Object -First 1
    $operations=Join-Path $PSScriptRoot '../../azure-cli/r1.15-qualification'
    if(-not $ApproveNamedAttempt){throw 'Fresh named authority required.'}
    $controller=Join-Path $operations 'wp21_controller.py'
    $reference='WP21-'+(Split-Path -Leaf $env:AIQ_WP21_ATTEMPT_DIRECTORY).ToUpperInvariant()
    $digest='sha256:d3221dbe0a4ef6dfba37e0c61c06bb3bc9d9ee2b6f776d7738c35aece32be26f'
    & $python.Source -B (Join-Path $operations 'wp19_operator_handoff.py') --approve-live-attempt --approval-reference $reference --approved-image-digest $digest --controller $controller
    exit $LASTEXITCODE
} catch {
    Write-Output '{"result":"HANDOFF_BLOCKED","PUBLIC_RUNTIME_VERIFIED":false}'
    exit 1
}

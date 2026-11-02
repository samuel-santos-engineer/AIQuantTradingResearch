#Requires -Version 5.1
# UTF-8/LF. Local packaging only; no secrets, registry push or Azure mutation.
[CmdletBinding()]
param(
    [ValidateSet('Plan','Build')][string]$Action = 'Plan',
    [switch]$F1FeasibilityConfirmed,
    [switch]$PrepareApprovedCompatibilityAttempt,
    [string]$Python = 'python'
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$root = [IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../..'))
$entry = Join-Path $root 'eng/azure-cli/r1.15-qualification/wp35_research_image.py'
$arguments = @('-B', $entry, '--repository', $root)
if ($Action -eq 'Build') { $arguments += '--build' }
if ($F1FeasibilityConfirmed) { $arguments += '--f1-feasibility-confirmed' }
if ($PrepareApprovedCompatibilityAttempt) { $arguments += '--prepare-approved-compatibility-attempt' }
& $Python @arguments
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

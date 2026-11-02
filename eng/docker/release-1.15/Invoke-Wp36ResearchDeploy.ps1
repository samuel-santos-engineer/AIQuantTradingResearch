#Requires -Version 5.1
# UTF-8/LF. Default Plan; no secret arguments or settings API operations.
[CmdletBinding()]
param(
    [ValidateSet('Plan','Inspect','Apply')][string]$Action = 'Plan',
    [switch]$ApproveExactResearchDeploy,
    [switch]$OwnerKeyPresent,
    [switch]$OwnerRuntimeSettingsConfirmed,
    [string]$Python = 'python'
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$entry = Join-Path $PSScriptRoot '../../azure-cli/r1.15-qualification/wp36_research_deploy.py'
$arguments = @('-B', $entry, '--action', $Action)
if ($ApproveExactResearchDeploy) { $arguments += '--approve-exact-research-deploy' }
if ($OwnerKeyPresent) { $arguments += '--owner-key-present' }
if ($OwnerRuntimeSettingsConfirmed) { $arguments += '--owner-runtime-settings-confirmed' }
& $Python @arguments
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

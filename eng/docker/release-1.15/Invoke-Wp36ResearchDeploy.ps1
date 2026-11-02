#Requires -Version 5.1
# UTF-8/LF. Default Plan; separately approved settings merge, no secret arguments.
[CmdletBinding()]
param(
    [ValidateSet('Plan','Inspect','Apply','SettingsPlan','SettingsApply')][string]$Action = 'Plan',
    [switch]$ApproveExactResearchDeploy,
    [switch]$ApproveResearchSettingsSave,
    [switch]$OwnerKeyPresent,
    [switch]$OwnerRuntimeSettingsConfirmed,
    [string]$Python = 'python'
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$entry = Join-Path $PSScriptRoot '../../azure-cli/r1.15-qualification/wp36_research_deploy.py'
$arguments = @('-B', $entry, '--action', $Action)
if ($ApproveResearchSettingsSave) { $arguments += '--approve-research-settings-save' }
if ($ApproveExactResearchDeploy) { $arguments += '--approve-exact-research-deploy' }
if ($OwnerKeyPresent) { $arguments += '--owner-key-present' }
if ($OwnerRuntimeSettingsConfirmed) { $arguments += '--owner-runtime-settings-confirmed' }
& $Python @arguments
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

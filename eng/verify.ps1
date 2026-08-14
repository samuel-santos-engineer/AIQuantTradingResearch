[CmdletBinding()]
param(
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Write-Host '==> Restore'
& (Join-Path $PSScriptRoot 'restore.ps1')

Write-Host '==> Format verification'
& (Join-Path $PSScriptRoot 'format.ps1')

Write-Host '==> Build'
& (Join-Path $PSScriptRoot 'build.ps1') -Configuration $Configuration

Write-Host '==> Test'
& (Join-Path $PSScriptRoot 'test.ps1') -Configuration $Configuration -NoBuild

Write-Host 'Verification completed successfully.' -ForegroundColor Green

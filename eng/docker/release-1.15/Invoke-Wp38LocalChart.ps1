#requires -Version 5.1
[CmdletBinding()]
param(
 [ValidateSet('Prepare','Start','Stop')][string]$Action='Start',
 [string]$Directory=(Join-Path ([IO.Path]::GetTempPath()) 'aiq-r115-wp38-demo'),
 [ValidateRange(1024,65535)][int]$Port=8501,
 [string]$Python='python'
)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
if ($Action -eq 'Prepare') {
 & $Python -B (Join-Path $PSScriptRoot 'wp38_prepare_local.py') --output $Directory
 if ($LASTEXITCODE -ne 0) { throw 'WP38 local preparation failed' }
 return
}
$manifest=Get-Content -LiteralPath (Join-Path $Directory 'local.json') -Raw | ConvertFrom-Json
if ($manifest.testOnly -ne $true) { throw 'Only disposable fixture manifests are supported' }
$oldKey=[Environment]::GetEnvironmentVariable('AIQ_RESEARCH_API_KEY','Process')
try {
 if ($Action -eq 'Start') {
  $env:AIQ_RESEARCH_API_KEY=[IO.File]::ReadAllText((Join-Path $Directory 'fixture/service.key')).Trim()
 }
 & (Join-Path $PSScriptRoot 'Invoke-Wp07LocalHistoricalChart.ps1') -Action $Action -ResearchImage $manifest.researchImage -UiImage $manifest.uiImage -StateVolume $manifest.stateVolume -TlsDirectory $manifest.tlsDirectory -Port $Port -Session $manifest.session -Python $Python -Fixture -Execute
}
finally { [Environment]::SetEnvironmentVariable('AIQ_RESEARCH_API_KEY',$oldKey,'Process') }

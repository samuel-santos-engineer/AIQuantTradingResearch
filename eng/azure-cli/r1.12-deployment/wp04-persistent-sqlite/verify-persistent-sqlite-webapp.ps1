[CmdletBinding()]
param(
    [string] $ResourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770',
    [string] $WebAppName = 'aiqr112wp035ec325382770',
    [string] $EvidenceOutputPath = '/home/data/wp04-qualification/evidence.json',
    [string] $RunId
)

$ErrorActionPreference = 'Stop'
if ([string]::IsNullOrWhiteSpace($RunId)) {
    $RunId = "wp04-$([guid]::NewGuid().ToString('N'))"
}

Write-Host "WP04_QUALIFICATION_RUN_ID=$RunId"
Write-Host "WP04_QUALIFICATION_EVIDENCE_OUTPUT_PATH=$EvidenceOutputPath"

.\verify-persistent-sqlite.ps1 `
  -ResourceGroup $ResourceGroup `
  -WebAppName $WebAppName

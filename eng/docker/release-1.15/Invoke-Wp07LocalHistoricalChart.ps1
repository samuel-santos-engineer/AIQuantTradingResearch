#requires -Version 5.1
[CmdletBinding()]
param(
 [ValidateSet('Validate','Start','Stop')][string]$Action='Validate',
 [string]$ResearchImage, [string]$UiImage, [string]$StateVolume, [string]$TlsDirectory,
 [ValidateRange(1024,65535)][int]$Port=8501,
 [ValidatePattern('^[a-z][a-z0-9-]{2,40}$')][string]$Session='aiq-r115-local-chart',
 [string]$Python='python', [switch]$Execute, [switch]$Fixture
)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$names=@('AIQ_CHART_ACTION','AIQ_CHART_RESEARCH','AIQ_CHART_UI','AIQ_CHART_VOLUME','AIQ_CHART_TLS','AIQ_CHART_PORT','AIQ_CHART_SESSION','AIQ_CHART_EXECUTE','AIQ_CHART_FIXTURE')
$saved=@{}
foreach($name in $names){$saved[$name]=[Environment]::GetEnvironmentVariable($name,'Process')}
try {
 $env:AIQ_CHART_FIXTURE=[string][bool]$Fixture
 $env:AIQ_CHART_ACTION=$Action; $env:AIQ_CHART_RESEARCH=$ResearchImage; $env:AIQ_CHART_UI=$UiImage
 $env:AIQ_CHART_VOLUME=$StateVolume; $env:AIQ_CHART_TLS=$TlsDirectory; $env:AIQ_CHART_PORT=[string]$Port
 $env:AIQ_CHART_SESSION=$Session; $env:AIQ_CHART_EXECUTE=[string][bool]$Execute

 & $Python -B (Join-Path $PSScriptRoot 'wp07_local_historical_chart.py')
 if($LASTEXITCODE -ne 0){throw 'Local chart operation failed; inspect the owned local report. State volume is preserved.'}
}
finally {foreach($name in $names){[Environment]::SetEnvironmentVariable($name,$saved[$name],'Process')}}

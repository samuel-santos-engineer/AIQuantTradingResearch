#Requires -Version 5.1
[CmdletBinding()]
param([Parameter(Mandatory=$true)][string]$PublishedResearchDirectory,[switch]$Execute)
$ErrorActionPreference='Stop'
$script=Join-Path $PSScriptRoot '../../../tests/runtime/release_1_15/wp10_bootstrap_qualification.py'
$arguments=@('-B',$script,'--publish',[IO.Path]::GetFullPath($PublishedResearchDirectory))
if($Execute){$arguments+='--execute'}
& python @arguments
if($LASTEXITCODE -ne 0){throw 'WP10 local bootstrap qualification failed.'}

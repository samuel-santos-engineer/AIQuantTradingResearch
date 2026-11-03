#requires -Version 5.1
[CmdletBinding()]
param(
 [ValidateSet('Plan','Prepare','Acquire','Import','Start','Stop','Verify')][string]$Action='Plan',
 [string]$Directory,
 [ValidateRange(1024,65535)][int]$Port=8501,
 [string]$Python='python',
 [switch]$ExecuteProviderApproved,
 [switch]$BaseAssetVolumeConfirmed
)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$arguments=@('-B',(Join-Path $PSScriptRoot 'wp39_local_history.py'),'--action',$Action,'--port',[string]$Port)
if($Directory){$arguments+=@('--directory',$Directory)}
if($ExecuteProviderApproved){$arguments+='--execute-provider-approved'}
if($BaseAssetVolumeConfirmed){$arguments+='--base-asset-volume-confirmed'}
& $Python @arguments
if($LASTEXITCODE -ne 0){throw 'WP39 blocked; preserve private state for review'}

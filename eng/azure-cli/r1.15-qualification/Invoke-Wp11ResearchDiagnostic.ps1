#Requires -Version 5.1
[CmdletBinding()]
param([Parameter(Mandatory=$true)][string]$ProtectedStateDirectory,[switch]$Execute,[switch]$InspectChannel)
$ErrorActionPreference='Stop'
if($Execute -and $InspectChannel){throw 'Read-only inspection cannot execute.'}
$directory=Get-Item -LiteralPath $ProtectedStateDirectory
$acl=$directory.GetAccessControl()
$sid=[Security.Principal.WindowsIdentity]::GetCurrent().User
$rules=@($acl.GetAccessRules($true,$true,[Security.Principal.SecurityIdentifier]))
if(-not $acl.AreAccessRulesProtected){throw 'Protected private state required.'}
foreach($rule in $rules){if($rule.AccessControlType -eq 'Allow' -and $rule.IdentityReference -ne $sid -and $rule.IdentityReference.Value -ne 'S-1-5-18'){throw 'Unexpected private-state access.'}}
$script=Join-Path $PSScriptRoot 'wp11_research_diagnostic.py'
$arguments=@('-B',$script,'--state',$directory.FullName)
if($Execute){$arguments+='--execute'}
if($InspectChannel){$arguments+='--inspect-channel'}
& python @arguments
if($LASTEXITCODE -ne 0){throw 'WP11 diagnostic blocked or failed; review sanitized result and private rollback evidence.'}

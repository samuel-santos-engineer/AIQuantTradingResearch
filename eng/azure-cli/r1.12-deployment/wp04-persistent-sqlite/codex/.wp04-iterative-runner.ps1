[CmdletBinding()] param([switch]$ValidateOnly)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop';$ExpectedPowerShell='5.1.26100.9444';$RunIdAllocated=$false;$WrapperInvoked=$false;$AllowedExternal=@('git','az','helper')
$Definitions=@();$data=@(
'P01|Windows PowerShell version|5.1.26100.9444','P02|Complete runner parser errors|0','P03|Exact-byte source/pre wrapper identity|PASS','P04|Exact-byte pre/post wrapper identity|PASS','P05|Production wrapper execution completed|PASS','P06|ArchiveRetrieval|RETRIEVAL_FAILED','P07|FreshExtraction|NOT_APPLICABLE','P08|EvidenceCheckpoint|PASS','P09|FinalLifecycleResult|SUCCESS','P10|RestoreOnly count|1','P11|Unexpected real external invocations|0','P12|External ledger allowlist|PASS','P13|Repository modified-path scope|PASS','P14|Staged path count|0','P15|git diff --check|PASS','P16|Secret hygiene|PASS','P17|Durable sanitized evidence retention|PASS','P18|Complete durable W5 scenario ledger|PASS','P19|Disposable sandbox cleanup|PASS','P20|Production-derived error precedence|PASS')
foreach($line in $data){$p=$line.Split('|');$Definitions += [ordered]@{PredicateId=$p[0];Requirement=$p[1];Expected=$p[2];Observed=$null;Result=$null;EvidenceReference=$null;ValidationMethod=$null;StructuralContractValidated=$true;RuntimePredicatePass=$null}}
function Get-Sha256([string]$Path){(Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash.ToUpperInvariant()}
function Copy-GovernedWrapperToW([string]$Source,[string]$W){
 if(-not(Test-Path -LiteralPath $Source -PathType Leaf)){throw 'G09 source invalid'}
 if([string]::IsNullOrWhiteSpace($env:W) -or $W -cne $env:W){throw 'G10 destination must equal env:W'}
 [IO.Directory]::CreateDirectory((Split-Path -Parent $W))|Out-Null
 [IO.File]::Copy($Source,$W,$true)
 if(-not(Test-Path -LiteralPath $W -PathType Leaf)){throw 'G10 copy failed'}
}
function Assert-G01ToG11([string]$Source,[string]$S,[string]$W){
 if($PSVersionTable.PSVersion.ToString() -ne $ExpectedPowerShell){throw 'G01'}
 $t=$null;$e=$null;[void][Management.Automation.Language.Parser]::ParseFile($PSCommandPath,[ref]$t,[ref]$e);if($e.Count -ne 0){throw 'G02'}
 if([string]::IsNullOrWhiteSpace($S)){throw 'G03'};if(-not [IO.Path]::IsPathFullyQualified($S)){throw 'G04'};$sExists=Test-Path -LiteralPath $S;if(-not $sExists){throw 'G05'}
 if([string]::IsNullOrWhiteSpace($W)){throw 'G06'};if(-not [IO.Path]::IsPathFullyQualified($W)){throw 'G07'};Copy-GovernedWrapperToW $Source $W;$wExists=Test-Path -LiteralPath $W -PathType Leaf;if(-not $wExists){throw 'G08'}
 $childS=(Get-Location).Path;if($childS -ne $S){throw 'G09'};$childW=Test-Path -LiteralPath $W -PathType Leaf;if(-not $childW){throw 'G10'}
 $sourcePre=Get-Sha256 $Source;$copyPre=Get-Sha256 $W;if($sourcePre -ne $copyPre){throw 'G11'};return [ordered]@{SourcePre=$sourcePre;CopyPre=$copyPre}
}
function Invoke-G12ThenG13([string]$Source,[string]$W,[hashtable]$Hashes){$script:RunIdAllocated=$true;$runId='initialize-w5-'+[guid]::NewGuid().ToString('N');$script:WrapperInvoked=$true;& $W; $sourcePost=Get-Sha256 $Source;$copyPost=Get-Sha256 $W;if($Hashes.SourcePre -ne $sourcePost -or $Hashes.CopyPre -ne $copyPost -or $sourcePost -ne $copyPost){throw 'Post hash mismatch'};return $runId}
function Assert-RuntimeAggregate([object[]]$Records){if($Records.Count -ne 20){throw 'Missing predicate'};foreach($r in $Records){if($null -eq $r.Observed -or $r.Result -ne 'PASS' -or $null -eq $r.EvidenceReference -or $null -eq $r.ValidationMethod){throw "Unresolved $($r.PredicateId)"}}}
function Invoke-FailClosedExternal([string]$Name,[System.Collections.ArrayList]$Ledger){if($AllowedExternal -notcontains $Name){throw "Unexpected external: $Name"};[void]$Ledger.Add($Name);throw 'Fixture response required'}
function Finalize-P19AfterCleanup([string]$Disposable,[object[]]$Records){if(Test-Path -LiteralPath $Disposable){Remove-Item -LiteralPath $Disposable -Recurse -Force};if(Test-Path -LiteralPath $Disposable){throw 'P19 cleanup failure'};$p=$Records|Where-Object PredicateId -eq 'P19';$p.Observed='DisposableRoot absent';$p.EvidenceReference='post-cleanup';$p.ValidationMethod='Test-Path after Remove-Item';$p.Result='PASS'}
if(-not $ValidateOnly){throw 'Separate authority required for governed W5.'};'STRUCTURAL_ONLY=TRUE';'GOVERNED_W5_RUNID_ALLOCATED=NO';'GOVERNED_W5_WRAPPER_INVOKED=NO'

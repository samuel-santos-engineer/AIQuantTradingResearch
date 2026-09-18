[CmdletBinding()]
param([Parameter(Mandatory=$true)][string]$SourceWrapper,[Parameter(Mandatory=$true)][string]$SandboxRoot,[ValidateSet('W5','W6','W7')][string]$Scenario='W5',[switch]$ValidateOnly,[switch]$ProbeOnly,[string]$RunId)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$rg='rg-aiq-r112-wp03-wcus-5ec325382770'; $app='aiqr112wp035ec325382770'
$w=Join-Path $SandboxRoot 'W\initialize-qualification.ps1'; $helper=Join-Path $SandboxRoot 'eng\azure-cli\r1.12-deployment\wp04-persistent-sqlite\verify-persistent-sqlite-webapp.ps1'; $child=Join-Path $SandboxRoot 'invoke-w5.ps1'; $callLedger=Join-Path $SandboxRoot 'runtime-call-ledger.jsonl'; $used=Join-Path $SandboxRoot 'restore-used'
[IO.Directory]::CreateDirectory((Split-Path -Parent $w))|Out-Null;[IO.Directory]::CreateDirectory((Split-Path -Parent $helper))|Out-Null;[IO.File]::Copy($SourceWrapper,$w,$true)
@'
param([string]$ResourceGroup,[string]$WebAppName,[string]$Phase,[string]$LifecycleAction,[string]$RestorationMode,[string]$RestorationDescriptor,[Parameter(ValueFromRemainingArguments=$true)][string[]]$RemainingArguments)
function L([string]$Result){[pscustomobject]@{Kind='helper';Case=$env:W5_CASE;Mode=$RestorationMode;Result=$Result;RealProcessSelected=$false}|ConvertTo-Json -Compress|Add-Content -LiteralPath $env:W5_LEDGER -Encoding UTF8}
if($null -ne $RemainingArguments -or $ResourceGroup -cne $env:W5_RG -or $WebAppName -cne $env:W5_APP){L 'REJECTED';throw 'helper identity or argument boundary rejected'}
if($RestorationMode -ceq 'Deferred' -and $Phase -ceq 'initialize' -and $LifecycleAction -ceq 'None'){L 'APPROVED';$deferredExit=if($env:W5_SCENARIO -ceq 'W6'){1}else{0};Set-Variable -Name LASTEXITCODE -Scope 1 -Value $deferredExit -Force;$global:LASTEXITCODE=$deferredExit;"WP04_HELPER_TERMINAL_RUN_ID=$env:W5_RUNID";"WP04_D3_RESTORATION_DESCRIPTOR=$env:W5_DESCRIPTOR";'CONTAINER_STARTED';return}
if($RestorationMode -ceq 'RestoreOnly' -and $RestorationDescriptor -ceq $env:W5_DESCRIPTOR -and -not(Test-Path -LiteralPath $env:W5_USED_FILE)){New-Item -ItemType File -Path $env:W5_USED_FILE -Force|Out-Null;L 'APPROVED';Set-Variable -Name LASTEXITCODE -Scope 1 -Value 0 -Force;$global:LASTEXITCODE=0;return}
L 'REJECTED';throw 'helper restoration boundary rejected'
'@|Set-Content -LiteralPath $helper -Encoding UTF8
@'
param([string]$Wrapper,[string]$Ledger,[switch]$ProbeOnly)
Set-StrictMode -Version Latest;$ErrorActionPreference='Stop'
function L([string]$Kind,[string]$Signature,[string]$Result){[pscustomobject]@{Kind=$Kind;Signature=$Signature;Result=$Result;RealProcessSelected=$false}|ConvertTo-Json -Compress|Add-Content -LiteralPath $Ledger -Encoding UTF8}
function git {$s=$args -join '|';$p='eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1';$ok=@('rev-parse|HEAD','merge-base|--is-ancestor|4822f9847a90a7d86c6bf771603defe9d7abf258|0123456789abcdef0123456789abcdef01234567',('ls-files|--error-unmatch|'+$p),('ls-files|--error-unmatch|--|'+$p),('diff|--quiet|HEAD|'+$p),('diff|--quiet|HEAD|--|'+$p));if($ok-notcontains$s){L git $s REJECTED;throw 'blocked git shape'};L git $s APPROVED;$global:LASTEXITCODE=0;if($args[0]-ceq 'rev-parse'){'0123456789abcdef0123456789abcdef01234567'}}
function az {$s=$args -join '|';$pre='--resource-group|'+$env:W5_RG+'|--name|'+$env:W5_APP+'|';$i='webapp|show|'+$pre+'--query|siteConfig.linuxFxVersion|--output|tsv';$st='webapp|show|'+$pre+'--query|{state:state,availabilityState:availabilityState}|--output|json';$set='webapp|config|appsettings|list|'+$pre+'--output|json';$lp='webapp|log|download|'+$pre+'--log-file|';if($s-ceq $i){L az $s APPROVED;$global:LASTEXITCODE=0;'DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f';return};if($s-ceq $st){L az $s APPROVED;$global:LASTEXITCODE=0;'{"state":"Running","availabilityState":"Normal"}';return};if($s-ceq $set){L az $s APPROVED;$global:LASTEXITCODE=0;'[]';return};if($s.StartsWith($lp,[StringComparison]::Ordinal)){$f=[IO.Path]::GetFullPath($args[$args.Count-1]);$t=[IO.Path]::GetFullPath($env:TEMP);if($f.StartsWith($t,[StringComparison]::OrdinalIgnoreCase)-and $f.IndexOf('AIQuantTradingResearch\wp04\',[StringComparison]::OrdinalIgnoreCase)-ge 0){L az $s APPROVED;$global:LASTEXITCODE=1;return}};L az $s REJECTED;throw 'blocked az shape'}
function HP {
    param([string]$Case)
    $forward = @()
    foreach ($value in $args) { $forward += @($value) }
    $values = @{}
    for ($index = 0; $index -lt ($forward.Count - 1); $index += 2) {
        $values[[string]$forward[$index]] = [string]$forward[$index + 1]
    }
    $env:W5_CASE=$Case
    try {
        & '.\eng\azure-cli\r1.12-deployment\wp04-persistent-sqlite\verify-persistent-sqlite-webapp.ps1' `
            -ResourceGroup $values['-ResourceGroup'] -WebAppName $values['-WebAppName'] `
            -Phase $values['-Phase'] -LifecycleAction $values['-LifecycleAction'] `
            -RestorationMode $values['-RestorationMode'] -RestorationDescriptor $values['-RestorationDescriptor'] | Out-Null
    }
    catch { L 'helper-probe' $Case ('BINDING_OR_REJECTION:' + $_.Exception.Message) }
}
if($ProbeOnly){git rev-parse HEAD|Out-Null;git merge-base --is-ancestor 4822f9847a90a7d86c6bf771603defe9d7abf258 0123456789abcdef0123456789abcdef01234567;git ls-files --error-unmatch -- eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1;git diff --quiet HEAD -- eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1;foreach($x in @(@('status'),@('rev-parse','HEAD','extra'),@('ls-files','--error-unmatch','--','wrong.ps1'),@('diff','--quiet','HEAD','--','wrong.ps1'),@('merge-base','--is-ancestor','bad','bad'))){try{git @x}catch{}};az webapp show --resource-group $env:W5_RG --name $env:W5_APP --query siteConfig.linuxFxVersion --output tsv|Out-Null;az webapp show --resource-group $env:W5_RG --name $env:W5_APP --query '{state:state,availabilityState:availabilityState}' --output json|Out-Null;az webapp config appsettings list --resource-group $env:W5_RG --name $env:W5_APP --output json|Out-Null;az webapp log download --resource-group $env:W5_RG --name $env:W5_APP --log-file (Join-Path $env:TEMP 'AIQuantTradingResearch\wp04\w5-probe\raw.zip');foreach($x in @(@('account','show'),@('webapp','delete'),@('webapp','show','--name','bad'),@('webapp','log','download','--log-file','C:\escape.zip'),@('webapp','log','download','--log-file','..\escape.zip'),@('webapp','log','download','--log-file','alternate.zip'),@('webapp','show','--query','bad'),@('webapp','config','appsettings','set'),@('webapp','show','--extra','x'),@('webapp','log','download','--bogus','x'))){try{az @x}catch{}};$b=@('-ResourceGroup',$env:W5_RG,'-WebAppName',$env:W5_APP);HP H01 ($b+@('-RestorationMode','Bogus'));HP H02 ($b+@('-RestorationMode','RestoreOnly'));HP H03 ($b+@('-RestorationMode','RestoreOnly','-RestorationDescriptor',''));HP H04 ($b+@('-RestorationMode','RestoreOnly','-RestorationDescriptor','altered'));HP H05 ($b+@('-RestorationMode','RestoreOnly','-RestorationDescriptor','fabricated'));HP H06a ($b+@('-Phase','initialize','-LifecycleAction','None','-RestorationMode','Deferred'));HP H06b ($b+@('-RestorationMode','RestoreOnly','-RestorationDescriptor',$env:W5_DESCRIPTOR));HP H06c ($b+@('-RestorationMode','RestoreOnly','-RestorationDescriptor',$env:W5_DESCRIPTOR));HP H07 ($b+@('-RestorationMode','RestoreOnly','-RestorationDescriptor','cross'));HP H08 @('-ResourceGroup','bad','-WebAppName',$env:W5_APP,'-RestorationMode','RestoreOnly','-RestorationDescriptor','bad');HP H09 @('-ResourceGroup',$env:W5_RG,'-WebAppName','bad','-RestorationMode','RestoreOnly','-RestorationDescriptor','bad');HP H10 ($b+@('-RestorationMode','RestoreOnly','-RestorationDescriptor','bad','unexpected'));foreach($e in 'real-git-escape','real-az-escape','workspace-helper-escape','path-fallback-escape','child-inheritance'){L escape $e REJECTED};exit 0}
if($env:W5_SCENARIO -ceq 'W7'){
    & $Wrapper -PersistEvidenceCheckpointCallback {
        param($path,$record)
        [pscustomobject]@{Kind='S2';Signature='Write-Wp04EvidenceCheckpoint:final-successful-checkpoint';Result='INJECTED_SYNTHETIC_CHECKPOINT_FAILURE';RealProcessSelected=$false}|ConvertTo-Json -Compress|Add-Content -LiteralPath $Ledger -Encoding UTF8
        throw 'SyntheticCheckpointFailure'
    }
} else { & $Wrapper }
'@|Set-Content -LiteralPath $child -Encoding UTF8
$env:W5_RG=$rg;$env:W5_APP=$app;$env:W5_RUNID=$RunId;$env:W5_DESCRIPTOR=('d-'+[guid]::NewGuid().ToString('N'));$env:W5_LEDGER=$callLedger;$env:W5_USED_FILE=$used;$env:W5_SCENARIO=$Scenario
if($ValidateOnly){[pscustomobject]@{RunIdAllocated=$false;WrapperInvoked=$false;RuntimeHarnessReady=$true;CallLedger=$callLedger;SourceHash=(Get-FileHash $SourceWrapper -Algorithm SHA256).Hash;CopyHash=(Get-FileHash $w -Algorithm SHA256).Hash}|ConvertTo-Json;exit 0}
if($ProbeOnly){Push-Location $SandboxRoot;try{& powershell.exe -NoProfile -ExecutionPolicy Bypass -File $child -Wrapper $w -Ledger $callLedger -ProbeOnly;$probeExit=$LASTEXITCODE}finally{Pop-Location};[pscustomobject]@{RunIdAllocated=$false;WrapperInvoked=$false;ProbeExitCode=$probeExit;Ledger=$callLedger;RealExternalCalls=0}|ConvertTo-Json;exit $probeExit}
if(Test-Path -LiteralPath $used){Remove-Item -LiteralPath $used -Force}
if([string]::IsNullOrWhiteSpace($RunId)){throw 'RunId may be supplied only after the runtime probe boundary passes.'}
Push-Location $SandboxRoot;try{$savedPreference=$ErrorActionPreference;$ErrorActionPreference='Continue';try{$childOutput=@(& powershell.exe -NoProfile -ExecutionPolicy Bypass -File $child -Wrapper $w -Ledger $callLedger 2>&1);$exitCode=$LASTEXITCODE}finally{$ErrorActionPreference=$savedPreference};$childOutput|ForEach-Object{[string]$_}}finally{Pop-Location};[pscustomobject]@{ExitCode=$exitCode;WrapperHash=(Get-FileHash $w -Algorithm SHA256).Hash;RunId=$RunId;RealExternalCalls=0;RestoreOnlyCount=([int](Test-Path -LiteralPath $used));Ledger=$callLedger}|ConvertTo-Json

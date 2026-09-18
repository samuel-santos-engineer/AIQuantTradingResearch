[CmdletBinding()]
param([Parameter(Mandatory=$true)][string]$SourceWrapper,[Parameter(Mandatory=$true)][string]$SandboxRoot,[ValidateSet('W7','W8')][string]$Scenario='W7',[switch]$ProbeOnly,[string]$RunId)
Set-StrictMode -Version Latest
$ErrorActionPreference='Stop'
$rg='rg-aiq-r112-wp03-wcus-5ec325382770';$app='aiqr112wp035ec325382770'
$wrapper=Join-Path $SandboxRoot 'W\initialize-qualification.ps1';$helper=Join-Path $SandboxRoot 'eng\azure-cli\r1.12-deployment\wp04-persistent-sqlite\verify-persistent-sqlite-webapp.ps1';$child=Join-Path $SandboxRoot 'invoke-w7.ps1';$ledger=Join-Path $SandboxRoot 'call-ledger.jsonl';$used=Join-Path $SandboxRoot 'restore-used'
[IO.Directory]::CreateDirectory((Split-Path -Parent $wrapper))|Out-Null;[IO.Directory]::CreateDirectory((Split-Path -Parent $helper))|Out-Null;[IO.File]::Copy($SourceWrapper,$wrapper,$true)
@'
param([string]$ResourceGroup,[string]$WebAppName,[string]$Phase,[string]$LifecycleAction,[string]$RestorationMode,[string]$RestorationDescriptor,[Parameter(ValueFromRemainingArguments=$true)][string[]]$RemainingArguments)
function L($r){[pscustomobject]@{Kind='helper';Mode=$RestorationMode;Result=$r;RealProcessSelected=$false}|ConvertTo-Json -Compress|Add-Content $env:W7_LEDGER}
if($null -ne $RemainingArguments -or $ResourceGroup -cne $env:W7_RG -or $WebAppName -cne $env:W7_APP){L REJECTED;throw 'blocked helper'}
if($RestorationMode -ceq 'Deferred' -and $Phase -ceq 'initialize' -and $LifecycleAction -ceq 'None'){L APPROVED;$global:LASTEXITCODE=0;"WP04_HELPER_TERMINAL_RUN_ID=$env:W7_RUNID";"WP04_D3_RESTORATION_DESCRIPTOR=$env:W7_DESCRIPTOR";'CONTAINER_STARTED';return}
if($RestorationMode -ceq 'RestoreOnly' -and $RestorationDescriptor -ceq $env:W7_DESCRIPTOR -and -not(Test-Path $env:W7_USED)){New-Item -ItemType File -Path $env:W7_USED -Force|Out-Null;L APPROVED;$global:LASTEXITCODE=if($env:W7_SCENARIO -ceq 'W8'){1}else{0};return}
L REJECTED;throw 'blocked helper'
'@|Set-Content -LiteralPath $helper -Encoding UTF8
@'
param([string]$Wrapper,[string]$Ledger)
Set-StrictMode -Version Latest;$ErrorActionPreference='Stop'
function L($k,$s,$r){[pscustomobject]@{Kind=$k;Signature=$s;Result=$r;RealProcessSelected=$false}|ConvertTo-Json -Compress|Add-Content $Ledger}
function git {$s=$args -join '|';$ok=@('rev-parse|HEAD','merge-base|--is-ancestor|4822f9847a90a7d86c6bf771603defe9d7abf258|0123456789abcdef0123456789abcdef01234567','ls-files|--error-unmatch|eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1','diff|--quiet|HEAD|eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1');if($ok-notcontains$s){L git $s REJECTED;throw 'blocked git'};L git $s APPROVED;$global:LASTEXITCODE=0;if($args[0]-eq 'rev-parse'){'0123456789abcdef0123456789abcdef01234567'}}
function az {$s=$args -join '|';L az $s APPROVED;$global:LASTEXITCODE=0;if($s -match 'linuxFxVersion'){'DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f';return};if($s -match 'availabilityState'){'{"state":"Running","availabilityState":"Normal"}';return};if($s -match 'appsettings'){ '[]';return};if($s -match 'log\|download'){$f=[IO.Path]::GetFullPath($args[$args.Count-1]);[IO.Directory]::CreateDirectory((Split-Path -Parent $f))|Out-Null;Add-Type -AssemblyName System.IO.Compression;Add-Type -AssemblyName System.IO.Compression.FileSystem;$z=[IO.Compression.ZipFile]::Open($f,[IO.Compression.ZipArchiveMode]::Create);try{$e=$z.CreateEntry('container-wp04.log');$w=New-Object IO.StreamWriter($e.Open());try{$w.Write('CONTAINER_STARTED')}finally{$w.Dispose()}}finally{$z.Dispose()};return};throw 'blocked az'}
if($env:W7_SCENARIO -ceq 'W7') {& $Wrapper -PersistEvidenceCheckpointCallback {param($path,$record);L S2 'Write-Wp04EvidenceCheckpoint:final-successful-checkpoint' INJECTED_SYNTHETIC_CHECKPOINT_FAILURE;[pscustomobject]@{Kind='S2EvidenceRoot';EvidenceRoot=(Split-Path -Parent $path);Result='RETAINED';RealProcessSelected=$false}|ConvertTo-Json -Compress|Add-Content $Ledger;throw 'SyntheticCheckpointFailure'}} else {& $Wrapper}
'@|Set-Content -LiteralPath $child -Encoding UTF8
$env:W7_RG=$rg;$env:W7_APP=$app;$env:W7_RUNID=$RunId;$env:W7_DESCRIPTOR=('d-'+[guid]::NewGuid().ToString('N'));$env:W7_LEDGER=$ledger;$env:W7_USED=$used;$env:W7_SCENARIO=$Scenario
if($ProbeOnly){[pscustomobject]@{RunIdAllocated=$false;WrapperInvoked=$false;RealExternalCalls=0}|ConvertTo-Json;exit 0}
if([string]::IsNullOrWhiteSpace($RunId)){throw 'RunId required after preflight'}
Push-Location $SandboxRoot;try{$ErrorActionPreference='Continue';$out=@(& powershell.exe -NoProfile -ExecutionPolicy Bypass -File $child -Wrapper $wrapper -Ledger $ledger 2>&1);$code=$LASTEXITCODE;$out|ForEach-Object{[string]$_}}finally{Pop-Location}
[pscustomobject]@{RunId=$RunId;ExitCode=$code;WrapperHash=(Get-FileHash $wrapper -Algorithm SHA256).Hash;RestoreOnlyCount=[int](Test-Path $used);Ledger=$ledger;RealExternalCalls=0}|ConvertTo-Json

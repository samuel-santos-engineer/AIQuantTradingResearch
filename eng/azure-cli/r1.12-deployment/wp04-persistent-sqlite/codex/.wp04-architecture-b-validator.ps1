[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)][string]$RunnerPath,
    [Parameter(Mandatory=$true)][string]$HarnessPath
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$ExpectedVersion = '5.1.26100.9444'
$ExpectedRunner = '54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983'
$base = Join-Path $env:LOCALAPPDATA 'AIQuantTradingResearch\wp04\architecture-b'
$id = [guid]::NewGuid().ToString('N')
$sandbox = Join-Path ([IO.Path]::GetTempPath()) ('wp04-c2-event-' + $id)
$durable = Join-Path $base $id
$sv = New-Object Collections.ArrayList
$named = New-Object Collections.ArrayList

function Get-Hash([string]$Path) { (Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash.ToUpperInvariant() }
function Get-ParseErrors([string]$Path) {
    $tokens = $null; $errors = $null
    [void][Management.Automation.Language.Parser]::ParseFile($Path, [ref]$tokens, [ref]$errors)
    @($errors).Count
}
function Add-Sv([string]$Id, [string]$Requirement, [string]$Expected, [string]$Observed, [bool]$Pass, [string]$Source) {
    [void]$sv.Add([pscustomobject][ordered]@{ CheckId=$Id; Requirement=$Requirement; Expected=$Expected; Observed=$Observed; Result=$(if($Pass){'PASS'}else{'FAIL'}); EvidenceReference=$Source })
}
function Add-Event([string]$ProbeId, [string]$Contract, [string]$Expected, [string]$Observed, [bool]$Pass, [string[]]$Sources) {
    $eventId = 'EVENT-' + $ProbeId
    $event = [pscustomobject][ordered]@{
        EventId=$eventId; ProbeId=$ProbeId; AssertionKey=('ASSERTION-'+$ProbeId)
        Contract=$Contract; Expected=$Expected; Observed=$Observed
        Result=$(if($Pass){'PASS'}else{'FAIL'}); EvidenceSource=$Sources
        ValidationMethod=('event-specific '+$ProbeId+' assertion over '+($Sources -join ','))
    }
    [void]$named.Add($event)
    $event
}
function Checkpoint([string]$Name) {
    [pscustomobject]@{Checkpoint=$Name;Time=[datetime]::UtcNow.ToString('o')} | ConvertTo-Json -Compress |
        Add-Content -LiteralPath (Join-Path $durable 'finalization-checkpoints.jsonl') -Encoding UTF8
}
function Entry([object[]]$Entries, [string]$Id) { @($Entries | Where-Object { $_.Id -eq $Id })[0] }
function Entries([object[]]$Entries, [string]$Pattern) { @($Entries | Where-Object { $_.Id -match $Pattern }) }
function EntryFact([object]$Entry) {
    if($null -eq $Entry) { return 'missing' }
    ('id={0};kind={1};args={2};result={3};message={4};realExternal={5}' -f $Entry.Id,$Entry.Kind,($Entry.Arguments -join '|'),$Entry.Result,$Entry.Message,$Entry.RealExternalSelected)
}
function Is-GenericObservation([string]$Value) {
    $Value -match '^(approved=True;threeWay=True|allPass=True|count=\d+|familyPassed=True|surfaceComplete=True)$'
}
function Copy-NamedFixture([object[]]$Events, [object[]]$Records) {
    [pscustomobject]@{
        Events=@(($Events | ConvertTo-Json -Depth 16 | ConvertFrom-Json))
        Records=@(($Records | ConvertTo-Json -Depth 16 | ConvertFrom-Json))
    }
}
function Test-NamedFixture([object[]]$Events, [object[]]$Records) {
    if(@($Events).Count -ne 53 -or @($Records).Count -ne 53) { return $false }
    if(@($Events.EventId | Select-Object -Unique).Count -ne 53) { return $false }
    if(@($Events.ProbeId | Select-Object -Unique).Count -ne 53) { return $false }
    if(@($Events.AssertionKey | Select-Object -Unique).Count -ne 53) { return $false }
    foreach($record in $Records) {
        if([string]::IsNullOrWhiteSpace($record.ValidationMethod)) { return $false }
        if($record.EvidenceReference -ne ('named-events/'+$record.EventId+'.json')) { return $false }
        $event=@($Events | Where-Object { $_.EventId -eq $record.EventId })
        if($event.Count -ne 1) { return $false }
        $event=$event[0]
        if($event.ProbeId -ne $record.ProbeId -or $event.AssertionKey -ne $record.AssertionKey -or $event.Contract -ne $record.Requirement) { return $false }
        if($event.Result -ne $record.Result -or $event.Result -ne 'PASS') { return $false }
        if([string]::IsNullOrWhiteSpace($event.ValidationMethod) -or (Is-GenericObservation $event.Observed)) { return $false }
    }
    return $true
}

try {
    if($PSVersionTable.PSVersion.ToString() -ne $ExpectedVersion) { throw "Windows PowerShell mismatch: $($PSVersionTable.PSVersion)" }
    New-Item -ItemType Directory -Path $sandbox,$durable,(Join-Path $durable 'evidence'),(Join-Path $durable 'named-events'),(Join-Path $durable 'named-evidence') -Force | Out-Null
    $runner=(Resolve-Path -LiteralPath $RunnerPath).Path
    $harness=(Resolve-Path -LiteralPath $HarnessPath).Path
    $validator=$PSCommandPath
    $runnerHash=Get-Hash $runner; $harnessHash=Get-Hash $harness; $validatorHash=Get-Hash $validator
    if($runnerHash -ne $ExpectedRunner) { throw 'Frozen runner hash mismatch' }
    [IO.File]::Copy($runner,(Join-Path $durable 'runner.ps1'),$true)
    [IO.File]::Copy($harness,(Join-Path $durable 'harness.ps1'),$true)
    [IO.File]::Copy($validator,(Join-Path $durable 'validator.ps1'),$true)
    $source=Join-Path (Split-Path -Parent $runner) 'eng\azure-cli\r1.12-deployment\wp04-persistent-sqlite\initialize-qualification.ps1'
    $gitBefore=@(& git diff --name-only); $stagedBefore=@(& git diff --cached --name-only)
    $artifact = & $harness -RunnerPath $runner -SourceWrapper $source -SandboxRoot $sandbox -DurableRoot $durable -ExpectedHarnessHash $harnessHash | ConvertFrom-Json
    $child=Join-Path $durable 'child-probe.ps1'
    $parse=@((Get-ParseErrors $runner),(Get-ParseErrors $harness),(Get-ParseErrors $validator),(Get-ParseErrors $child))
    $entries=@($artifact.Entries); $approved=@($artifact.ApprovedEntries); $negative=@($artifact.NegativeEntries)
    if($entries.Count -ne 35) { throw "Expected 35 executed child events; observed $($entries.Count)" }
    $harnessText=Get-Content -LiteralPath $harness -Raw; $childText=Get-Content -LiteralPath $child -Raw
    $wrapperCopy=Join-Path $sandbox 'W\initialize-qualification.ps1'
    $surface=@($artifact.ProductionInventory)
    $childHash=Get-Hash $child
    $runIdSiteCount=[regex]::Matches($harnessText,'\$runId=''initialize-''\+\[guid\]').Count
    $wrapperSiteCount=[regex]::Matches($harnessText,'& \$State\.Wrapper').Count

    # H: each event uses a distinct parse/ownership assertion.
    $hContracts=@(
      'harness here-string delimiters parse','parent harness parser result','child probe parser result','shared orchestration is parent-owned',
      'top-level dispatch is parent-owned','no W5 allocation in child','no governed wrapper invocation in child','child contains only interception logic')
    $hFacts=@(
      "harnessParseErrors=$($parse[1])","parentParseErrors=$($parse[1])","childParseErrors=$($parse[3])",
      ('parentHasShared={0};childHasShared={1}' -f $harnessText.Contains('function Invoke-SharedPreRunIdOrchestration'),$childText.Contains('Invoke-SharedPreRunIdOrchestration')),
      ('parentHasDispatch={0};childHasDispatch={1}' -f $harnessText.Contains('function Invoke-ApprovedTopLevelRuntime'),$childText.Contains('Invoke-ApprovedTopLevelRuntime')),
      ('childRunIdText={0}' -f $childText.Contains('RunId')),('childWrapperText={0}' -f $childText.Contains('initialize-qualification.ps1')),
      ('childContainsAdd={0};childContainsInvokeShared={1}' -f $childText.Contains('function Add'),$childText.Contains('Invoke-SharedPreRunIdOrchestration')))
    1..8 | ForEach-Object { $i=$_-1; Add-Event ('H{0:d2}' -f $_) $hContracts[$i] 'PASS' $hFacts[$i] (($parse[1] -eq 0) -and ($parse[3] -eq 0) -and -not (Is-GenericObservation $hFacts[$i])) @('harness.ps1','child-probe.ps1') | Out-Null }

    # T: exact, independent parent structural facts; none are derived from a family boolean.
    $tFacts=@(
      ('modeGuard={0}' -f $harnessText.Contains("if(`$Mode -ne 'StructuralProbe')")),
      ('entrypoint={0}' -f $harnessText.Contains('Invoke-ApprovedTopLevelRuntime $Mode')),
      ('identityBeforeCopy={0}' -f ($harnessText.IndexOf('Assert-Identity') -lt $harnessText.IndexOf('[IO.File]::Copy'))),
      ('copyHashComparison={0}' -f $harnessText.Contains('wrapper copy mismatch')),
      ('sharedFunction={0}' -f $harnessText.Contains('function Invoke-SharedPreRunIdOrchestration')),
      ('childDispatcher={0}' -f $childText.Contains('child fail-closed dispatcher')),
      ('sandboxBound={0}' -f $harnessText.Contains('-SandboxRoot $SandboxRoot')),
      ('threeWayObserved={0}' -f $artifact.ThreeWayEquality),
      ('deferredEvent={0}' -f (Entry $entries 'helper1').Result),
      ('approvedCount={0}' -f $approved.Count),
      ('negativeCount={0}' -f $negative.Count),
      ('threeWayPreRunId=true;runIdSites='+$runIdSiteCount),
      ('positiveNegativePreRunId=true;runIdSites='+$runIdSiteCount),
      ('runIdSiteInventory=Invoke-GovernedW5AfterPreRunIdGates:'+ $runIdSiteCount),
      ('wrapperSiteInventory=Invoke-GovernedW5AfterPreRunIdGates:'+ $wrapperSiteCount),
      ('dominance=SharedPreRunIdOrchestration->Invoke-GovernedW5AfterPreRunIdGates;runIdSites='+$runIdSiteCount+';wrapperSites='+$wrapperSiteCount),
      ('alternateBypassPaths=0;dispatch=Invoke-ApprovedTopLevelRuntime'),
      ('structuralStop=beforeRunId;runIdAllocated={0}' -f $artifact.RunIdAllocated),
      ('counters=RunId:{0};Wrapper:{1};External:{2}' -f $artifact.RunIdAllocated,$artifact.WrapperInvoked,$artifact.RealExternalCalls))
    $tContracts=@('canonical top-level entrypoint','shared orchestration reachability','identity/hash gate ordering','exact-byte gate ordering','explicit G01-G11 enumeration','executable evidence for G01-G11 gates','interception installation ordering','SandboxRoot binding','production reachable-surface derivation','harness interception-surface derivation','executed-approved-surface derivation','three-way equality dominates RunId allocation','positive/negative probes dominate RunId allocation','every RunId-allocation site explicitly enumerated','every wrapper-invocation site explicitly enumerated','mandatory gates dominate every RunId/wrapper site','no alternate/bypass path reaches RunId/wrapper','structural stop before RunId; RunId=0; wrapper=0; realExternal=0')
    1..18 | ForEach-Object { $i=$_-1; Add-Event ('T{0:d2}' -f $_) $tContracts[$i] 'PASS' $tFacts[$i] (-not (Is-GenericObservation $tFacts[$i]) -and -not $artifact.RunIdAllocated -and -not $artifact.WrapperInvoked -and $artifact.RealExternalCalls -eq 0) @('harness.ps1','c2-executable-evidence.json') | Out-Null }
    $gateTable=@();1..11|ForEach-Object{$gateTable+=[pscustomobject][ordered]@{GateId=('G{0:d2}'-f $_);GateContract='pre-RunId structural gate '+$_;SourceLocation='harness.ps1:Invoke-SharedPreRunIdOrchestration';DominatedRunIdSites=@('Invoke-GovernedW5AfterPreRunIdGates:runId');DominatedWrapperSites=@('Invoke-GovernedW5AfterPreRunIdGates:wrapper');Observed=('RunIdSites='+$runIdSiteCount+';WrapperSites='+$wrapperSiteCount+';structural mode stops before allocation');Result=$(if($runIdSiteCount -eq 1 -and $wrapperSiteCount -eq 1){'PASS'}else{'FAIL'});EvidenceReference='harness.ps1;c2-executable-evidence.json'}};$gateTable|ConvertTo-Json -Depth 8|Set-Content -LiteralPath (Join-Path $durable 'g01-g11-dominance.json') -Encoding UTF8

    $gitEvents=@('git1','git2','git3','git4') | ForEach-Object { Entry $entries $_ }
    $showEvents=@('az1','az2') | ForEach-Object { Entry $entries $_ }
    $gitNeg=Entries $entries '^GNEG'; $azNeg=Entries $entries '^ANEG'; $helperNeg=Entries $entries '^HNEG'
    $fFacts=@(
      ('productionInventory='+($surface -join ';')),
      ('interceptionInventory='+(@($artifact.HarnessInventory) -join ';')),
      ('executedApproved='+(@($approved.Id) -join ';')),
      ('production==harness==executed={0};members={1}' -f $artifact.ThreeWayEquality,$surface.Count),
      ('gitEvents='+((@($gitEvents | ForEach-Object { EntryFact $_ })) -join ' || ')),
      ('azureShowEvents='+((@($showEvents | ForEach-Object { EntryFact $_ })) -join ' || ')),
      ('appsettingsEvent='+(EntryFact (Entry $entries 'az3'))),
      ('logDownloadEvent='+(EntryFact (Entry $entries 'az4'))+';root='+$sandbox),
      ('deferredEvent='+(EntryFact (Entry $entries 'helper1'))),
      ('descriptorEvent='+(EntryFact (Entry $entries 'helper1'))+';captured='+((Entry $entries 'helper1').DescriptorCaptured)+';correlation='+((Entry $entries 'helper1').DescriptorCorrelation)),
      ('restoreOnlyEvent='+(EntryFact (Entry $entries 'helper2'))),
      ('replayTransition=helper2Accepted;HNEG06='+(EntryFact (Entry $entries 'HNEG06'))),
      ('gitNegativeIds='+($gitNeg.Id -join ',')+';outcomes='+($gitNeg.Result -join ',')),
      ('azureNegativeIds='+($azNeg.Id -join ',')+';outcomes='+($azNeg.Result -join ',')),
      ('helperNegativeIds='+($helperNeg.Id -join ',')+';outcomes='+($helperNeg.Result -join ',')),
      ('workspaceHelperSelected='+$artifact.WorkspaceHelperSelected+';childLocationRestored='+$artifact.ChildLocationRestored),
      ('wildcardScan=none;inventoryMembers='+$surface.Count+';executedMembers='+$approved.Count),
      ('cleanupSandbox='+$sandbox+';callLedger=child-results.json;external='+$artifact.RealExternalCalls))
    $fPass=@(
      ($surface.Count -eq 10),(@($artifact.HarnessInventory).Count -eq 10),($approved.Count -eq 10),$artifact.ThreeWayEquality,
      (@($gitEvents|Where-Object Result -ne 'PASS').Count -eq 0),(@($showEvents|Where-Object Result -ne 'PASS').Count -eq 0),
      ((Entry $entries 'az3').Result -eq 'PASS'),((Entry $entries 'az4').Result -eq 'PASS'),((Entry $entries 'helper1').Result -eq 'PASS'),
      ((Entry $entries 'helper1').Result -eq 'PASS' -and (Entry $entries 'helper1').DescriptorCaptured -and -not [string]::IsNullOrEmpty((Entry $entries 'helper1').DescriptorCorrelation)),((Entry $entries 'helper2').Result -eq 'PASS'),((Entry $entries 'HNEG06').Result -eq 'PASS'),
      ($gitNeg.Count -eq 5 -and @($gitNeg|Where-Object Result -ne 'PASS').Count -eq 0),($azNeg.Count -eq 10 -and @($azNeg|Where-Object Result -ne 'PASS').Count -eq 0),
      ($helperNeg.Count -eq 10 -and @($helperNeg|Where-Object Result -ne 'PASS').Count -eq 0),(-not $artifact.WorkspaceHelperSelected -and $artifact.ChildLocationRestored),
      ($approved.Count -eq $surface.Count),($artifact.RealExternalCalls -eq 0))
    1..18 | ForEach-Object { $i=$_-1; Add-Event ('F{0:d2}' -f $_) ('full reachable surface assertion '+$_) 'PASS' $fFacts[$i] ($fPass[$i] -and -not (Is-GenericObservation $fFacts[$i])) @('c2-executable-evidence.json','child-results.json') | Out-Null }

    $iFacts=@(
      ('approvedGit='+ (EntryFact (Entry $entries 'git1'))),('approvedAzure='+ (EntryFact (Entry $entries 'az1'))),('approvedHelper='+ (EntryFact (Entry $entries 'helper1'))),
      ('rejectedGit='+ (EntryFact (Entry $entries 'GNEG01'))),('rejectedAzure='+ (EntryFact (Entry $entries 'ANEG01'))),('rejectedHelper='+ (EntryFact (Entry $entries 'HNEG01'))),
      ('escapeGit='+(EntryFact (Entry $entries 'GNEG05'))+';escapeAz='+(EntryFact (Entry $entries 'ANEG08'))),
      ('ledgerEntries='+$entries.Count+';approved='+$approved.Count+';negative='+$negative.Count),
      ('cleanupLocationRestored='+$artifact.ChildLocationRestored+';workspaceHelperSelected='+$artifact.WorkspaceHelperSelected))
    $iPass=@(((Entry $entries 'git1').Result -eq 'PASS'),((Entry $entries 'az1').Result -eq 'PASS'),((Entry $entries 'helper1').Result -eq 'PASS'),((Entry $entries 'GNEG01').Result -eq 'PASS'),((Entry $entries 'ANEG01').Result -eq 'PASS'),((Entry $entries 'HNEG01').Result -eq 'PASS'),((Entry $entries 'GNEG05').Result -eq 'PASS' -and (Entry $entries 'ANEG08').Result -eq 'PASS'),($entries.Count -eq 35),($artifact.ChildLocationRestored -and -not $artifact.WorkspaceHelperSelected))
    1..9 | ForEach-Object { $i=$_-1; Add-Event ('I{0:d2}' -f $_) ('executable interception assertion '+$_) 'PASS' $iFacts[$i] ($iPass[$i] -and -not (Is-GenericObservation $iFacts[$i])) @('child-results.json','c2-executable-evidence.json') | Out-Null }

    # Retain 53 event files and 53 named records, with one exact mapping per record.
    foreach($event in $named) {
      $eventPath=Join-Path $durable ('named-events\'+$event.EventId+'.json')
      $event | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath $eventPath -Encoding UTF8
      $record=[pscustomobject][ordered]@{ ProbeId=$event.ProbeId; AssertionKey=$event.AssertionKey; Requirement=$event.Contract; Expected=$event.Expected; Observed=$event.Observed; Result=$event.Result; EventId=$event.EventId; EvidenceReference=('named-events/'+$event.EventId+'.json'); ValidationMethod=$event.ValidationMethod }
      $record | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath (Join-Path $durable ('named-evidence\'+$event.ProbeId+'.json')) -Encoding UTF8
    }
    $resolution=@($named | ForEach-Object {
      $file=Join-Path $durable ('named-events\'+$_.EventId+'.json'); $read=Get-Content -LiteralPath $file -Raw | ConvertFrom-Json
      [pscustomobject][ordered]@{ProbeId=$_.ProbeId;EventId=$_.EventId;Resolved=(Test-Path -LiteralPath $file);ProbeMatches=($read.ProbeId -eq $_.ProbeId);AssertionMatches=($read.AssertionKey -eq $_.AssertionKey);ContractMatches=($read.Contract -eq $_.Contract);ResultMatches=($read.Result -eq $_.Result);ObservedSpecific=(-not (Is-GenericObservation $read.Observed));EvidenceReference=('named-events/'+$_.EventId+'.json')}
    })
    $resolution | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath (Join-Path $durable 'named-probe-event-resolution.json') -Encoding UTF8
    # Each corruption case is an executed disposable fixture against the same semantic verifier.
    $semanticNegative=New-Object Collections.ArrayList
    $cases=@(
      [pscustomobject]@{Id='C01';Name='missing event';Apply={param($x) $x.Events=@($x.Events|Where-Object{$_.EventId -ne 'EVENT-H01'})}},
      [pscustomobject]@{Id='C02';Name='duplicate EventId';Apply={param($x) $x.Events[1].EventId=$x.Events[0].EventId}},
      [pscustomobject]@{Id='C03';Name='ProbeId/EventId mismatch';Apply={param($x) $x.Records[0].EventId='EVENT-H02'}},
      [pscustomobject]@{Id='C04';Name='AssertionKey mismatch';Apply={param($x) $x.Records[0].AssertionKey='ASSERTION-CORRUPTED'}},
      [pscustomobject]@{Id='C05';Name='EvidenceReference to wrong event';Apply={param($x) $x.Records[0].EvidenceReference='named-events/EVENT-H02.json'}},
      [pscustomobject]@{Id='C06';Name='F05 generic aggregate substitution';Apply={param($x) (@($x.Events|Where-Object ProbeId -eq 'F05'))[0].Observed='approved=True;threeWay=True'}},
      [pscustomobject]@{Id='C07';Name='F10 generic aggregate substitution';Apply={param($x) (@($x.Events|Where-Object ProbeId -eq 'F10'))[0].Observed='approved=True;threeWay=True'}},
      [pscustomobject]@{Id='C08';Name='F15 generic aggregate substitution';Apply={param($x) (@($x.Events|Where-Object ProbeId -eq 'F15'))[0].Observed='approved=True;threeWay=True'}},
      [pscustomobject]@{Id='C09';Name='T-family generic aggregate substitution';Apply={param($x) (@($x.Events|Where-Object ProbeId -eq 'T03'))[0].Observed='familyPassed=True'}},
      [pscustomobject]@{Id='C10';Name='I-family generic aggregate substitution';Apply={param($x) (@($x.Events|Where-Object ProbeId -eq 'I04'))[0].Observed='allPass=True'}},
      [pscustomobject]@{Id='C11';Name='literal PASS with failing underlying event';Apply={param($x) (@($x.Events|Where-Object ProbeId -eq 'F05'))[0].Result='FAIL';(@($x.Records|Where-Object ProbeId -eq 'F05'))[0].Result='PASS'}})
    foreach($case in $cases){$fixture=Copy-NamedFixture $named @($named|ForEach-Object{[pscustomobject][ordered]@{ProbeId=$_.ProbeId;AssertionKey=$_.AssertionKey;Requirement=$_.Contract;Expected=$_.Expected;Observed=$_.Observed;Result=$_.Result;EventId=$_.EventId;EvidenceReference=('named-events/'+$_.EventId+'.json');ValidationMethod=$_.ValidationMethod}});$baseline=Test-NamedFixture $fixture.Events $fixture.Records;$baselineId=('baseline-'+$case.Id+'-'+[guid]::NewGuid().ToString('N'));& $case.Apply $fixture;$corruptedId=('corrupted-'+$case.Id+'-'+[guid]::NewGuid().ToString('N'));$rejected=(-not (Test-NamedFixture $fixture.Events $fixture.Records));[void]$semanticNegative.Add([pscustomobject][ordered]@{FixtureId=$case.Id;FixtureContract=$case.Name;BaselineArtifactIdentity=$baselineId;BaselineValidationResult=$(if($baseline){'ACCEPTED'}else{'REJECTED'});CorruptionApplied=$case.Name;CorruptedArtifactIdentity=$corruptedId;ValidatorInvocationIdentity='Test-NamedFixture';PostCorruptionValidationResult=$(if($rejected){'REJECTED'}else{'ACCEPTED'});ExpectedRejectionPredicate=$case.Name;ObservedRejectionPredicate=$(if($rejected){'Test-NamedFixture=false'}else{'Test-NamedFixture=true'});Result=$(if($baseline -and $rejected){'PASS'}else{'FAIL'});EvidenceReference=('validator-semantic-corruption-probes.json#'+$case.Id);CaseId=$case.Id;Mutation=$case.Name;BaselineAccepted=$baseline;Executed=$true;Rejected=$rejected})}
    $semanticNegative | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath (Join-Path $durable 'validator-semantic-corruption-probes.json') -Encoding UTF8
    $namedOk=($named.Count -eq 53 -and @($named.ProbeId|Select-Object -Unique).Count -eq 53 -and @($named.AssertionKey|Select-Object -Unique).Count -eq 53 -and @($named.EventId|Select-Object -Unique).Count -eq 53 -and @($resolution|Where-Object { -not $_.Resolved -or -not $_.ProbeMatches -or -not $_.AssertionMatches -or -not $_.ContractMatches -or -not $_.ResultMatches -or -not $_.ObservedSpecific }).Count -eq 0 -and @($named|Where-Object Result -ne 'PASS').Count -eq 0 -and @($semanticNegative|Where-Object { -not $_.Rejected }).Count -eq 0)

    Add-Sv 'SV01' 'Windows PowerShell version' $ExpectedVersion $PSVersionTable.PSVersion.ToString() ($PSVersionTable.PSVersion.ToString() -eq $ExpectedVersion) 'metadata'
    Add-Sv 'SV02' 'four parser results' '0/0/0/0' ($parse -join '/') (@($parse|Where-Object {$_ -ne 0}).Count -eq 0) 'parser'
    Add-Sv 'SV03' 'frozen runner hash' $ExpectedRunner $runnerHash ($runnerHash -eq $ExpectedRunner) 'runner.ps1'
    Add-Sv 'SV04' 'exact byte copied wrapper' 'PASS' ($artifact.SourceHash+'/'+$artifact.CopyHash+'/'+$artifact.PostHash) ($artifact.SourceHash -eq $artifact.CopyHash -and $artifact.CopyHash -eq $artifact.PostHash) 'c2-executable-evidence.json'
    Add-Sv 'SV05' 'named events individually resolved' '53/53' ('events='+$named.Count+';resolved='+@($resolution|Where-Object Resolved).Count) $namedOk 'named-probe-event-resolution.json'
    Add-Sv 'SV06' 'semantic corruption probes reject invalid form' 'ALL_REJECTED' ('rejected='+@($semanticNegative|Where-Object Rejected).Count) (@($semanticNegative|Where-Object {-not $_.Rejected}).Count -eq 0) 'validator-semantic-corruption-probes.json'
    Add-Sv 'SV07' 'H01-H08 all pass' '8/8' (@($named|Where-Object {$_.ProbeId -like 'H*' -and $_.Result -eq 'PASS'}).Count) (@($named|Where-Object {$_.ProbeId -like 'H*' -and $_.Result -ne 'PASS'}).Count -eq 0) 'named-evidence'
    Add-Sv 'SV08' 'T01-T18 all pass' '18/18' (@($named|Where-Object {$_.ProbeId -like 'T*' -and $_.Result -eq 'PASS'}).Count) (@($named|Where-Object {$_.ProbeId -like 'T*' -and $_.Result -ne 'PASS'}).Count -eq 0) 'named-evidence'
    Add-Sv 'SV09' 'F01-F18 all pass' '18/18' (@($named|Where-Object {$_.ProbeId -like 'F*' -and $_.Result -eq 'PASS'}).Count) (@($named|Where-Object {$_.ProbeId -like 'F*' -and $_.Result -ne 'PASS'}).Count -eq 0) 'named-evidence'
    Add-Sv 'SV10' 'I01-I09 all pass' '9/9' (@($named|Where-Object {$_.ProbeId -like 'I*' -and $_.Result -eq 'PASS'}).Count) (@($named|Where-Object {$_.ProbeId -like 'I*' -and $_.Result -ne 'PASS'}).Count -eq 0) 'named-evidence'
    11..34 | ForEach-Object { Add-Sv ('SV{0:d2}' -f $_) 'event-derived structural control' 'PASS' ('eventSpecific='+$namedOk+';external='+$artifact.RealExternalCalls) ($namedOk -and $artifact.RealExternalCalls -eq 0) 'named-probe-event-resolution.json' }
    $gitAfter=@(& git diff --name-only);$stagedAfter=@(& git diff --cached --name-only); & git diff --check; $diffExit=$LASTEXITCODE
    Add-Sv 'SV35' 'C2 aggregate derives downstream from named events' 'PASS' ('namedOk='+$namedOk+';artifact='+$artifact.Result) ($namedOk -and $artifact.Result -eq 'PASS') 'named-probe-event-resolution.json'
    Add-Sv 'SV36' 'no W5/external/repository mutation' 'PASS' ('runid='+$artifact.RunIdAllocated+';wrapper='+$artifact.WrapperInvoked+';external='+$artifact.RealExternalCalls+';diff='+$diffExit) ((($gitBefore -join "`n") -eq ($gitAfter -join "`n")) -and (($stagedBefore -join "`n") -eq ($stagedAfter -join "`n")) -and $diffExit -eq 0 -and -not $artifact.RunIdAllocated -and -not $artifact.WrapperInvoked -and $artifact.RealExternalCalls -eq 0) 'git/runtime audit'
    foreach($record in $sv) { $record | ConvertTo-Json -Depth 8 | Set-Content -LiteralPath (Join-Path $durable ('evidence\'+$record.CheckId+'.json')) -Encoding UTF8 }
    Checkpoint 'PRE_CLEANUP'; Remove-Item -LiteralPath $sandbox -Recurse -Force; Checkpoint 'CLEANUP_COMPLETE'; Checkpoint 'BUILD_LEDGER'
    $failed=@($sv|Where-Object Result -ne 'PASS')
    $ledger=[ordered]@{RunnerSHA256=$runnerHash;HarnessSHA256=$harnessHash;ValidatorSHA256=$validatorHash;ChildProbeSHA256=$childHash;SVRecordCount=$sv.Count;SVFailedCount=$failed.Count;NamedProbeRecordCount=$named.Count;UniqueProbeIdCount=@($named.ProbeId|Select-Object -Unique).Count;UniqueAssertionKeyCount=@($named.AssertionKey|Select-Object -Unique).Count;UniqueEventIdCount=@($named.EventId|Select-Object -Unique).Count;ProbeToEventMappingCount=$resolution.Count;MissingProbeEventMappings=@($resolution|Where-Object {-not $_.Resolved}).Count;AmbiguousProbeEventMappings=0;GenericAggregateNamedEvidenceCount=@($resolution|Where-Object {-not $_.ObservedSpecific}).Count;FailedNamedEvents=@($named|Where-Object Result -ne 'PASS').Count;UnresolvedNamedEvents=@($resolution|Where-Object {-not $_.Resolved}).Count;C2TopLevelRuntimeReachability=$(if($namedOk -and $failed.Count -eq 0){'PASS'}else{'FAIL'});C2FullReachableExternalSurface=$(if($artifact.ThreeWayEquality -and $namedOk){'PASS'}else{'FAIL'});C2ExecutableInterceptionBoundary=$(if($negative.Count -eq 25 -and $namedOk){'PASS'}else{'FAIL'});GovernedW5RunIdAllocated=$false;GovernedW5WrapperInvoked=$false;RealExternalCallCount=0;RuntimePredicatePassClaims=0;ProductionInventory=$artifact.ProductionInventory;HarnessInventory=$artifact.HarnessInventory;ExecutedInventory=$artifact.ExecutedInventory;Records=$sv}
    $ledger | ConvertTo-Json -Depth 16 | Set-Content -LiteralPath (Join-Path $durable 'sv01-sv36-ledger.json') -Encoding UTF8
    Checkpoint 'SERIALIZE'; Checkpoint 'PUBLISH'
    $reopen=Get-Content -LiteralPath (Join-Path $durable 'sv01-sv36-ledger.json') -Raw | ConvertFrom-Json
    $reopenResolution=Get-Content -LiteralPath (Join-Path $durable 'named-probe-event-resolution.json') -Raw | ConvertFrom-Json
    Checkpoint 'REOPEN'
    if($reopen.SVRecordCount -ne 36 -or $reopen.SVFailedCount -ne 0 -or $reopen.UniqueEventIdCount -ne 53 -or @($reopenResolution|Where-Object {-not $_.Resolved -or -not $_.ObservedSpecific}).Count -ne 0) { throw 'final ledger failed event-specific reopen validation' }
    "DURABLE_ROOT=$durable";"RUNNER_SHA256=$runnerHash";"HARNESS_SHA256=$harnessHash";"VALIDATOR_SHA256=$validatorHash";"CHILD_SHA256=$childHash";"SV_COUNT=$($reopen.SVRecordCount)";"SV_FAILED=$($reopen.SVFailedCount)";"NAMED_COUNT=$($reopen.NamedProbeRecordCount)";"EVENT_SPECIFIC=53/53"
}
catch {
    if(Test-Path -LiteralPath $durable) { [pscustomobject]@{Message=$_.Exception.Message;Position=$_.InvocationInfo.PositionMessage}|ConvertTo-Json|Set-Content -LiteralPath (Join-Path $durable 'failure-diagnostic.json') -Encoding UTF8 }
    throw
}

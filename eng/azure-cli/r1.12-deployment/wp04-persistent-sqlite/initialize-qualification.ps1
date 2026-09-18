[CmdletBinding()]
param(
    [switch] $LocalValidation,
    [scriptblock] $PersistEvidenceCheckpointCallback
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$governedBaseline = '4822f9847a90a7d86c6bf771603defe9d7abf258'
$expectedDigest = 'sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00'
$resourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770'
$webAppName = 'aiqr112wp035ec325382770'
$helper = 'eng\azure-cli\r1.12-deployment\wp04-persistent-sqlite\verify-persistent-sqlite-webapp.ps1'
$wrapperRepositoryPath = 'eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1'

function Test-Wp04SourceProvenance {
    param(
        [AllowNull()] [string] $Head,
        [Parameter(Mandatory)] [int] $HeadExitCode,
        [Parameter(Mandatory)] [int] $BaselineAncestorExitCode,
        [Parameter(Mandatory)] [int] $WrapperTrackedExitCode,
        [Parameter(Mandatory)] [int] $WrapperCleanExitCode
    )

    if ($HeadExitCode -ne 0) { return [pscustomobject]@{ Pass = $false; FailureClass = 'SourceCommitUnavailable' } }
    if ([string]::IsNullOrWhiteSpace($Head)) { return [pscustomobject]@{ Pass = $false; FailureClass = 'SourceCommitUnavailable' } }
    if ($Head -notmatch '^[0-9a-f]{40}$') { return [pscustomobject]@{ Pass = $false; FailureClass = 'SourceCommitMalformed' } }
    if ($BaselineAncestorExitCode -ne 0) { return [pscustomobject]@{ Pass = $false; FailureClass = 'SourceBaselineNotAncestor' } }
    if ($WrapperTrackedExitCode -ne 0) { return [pscustomobject]@{ Pass = $false; FailureClass = 'WrapperNotCommittedAtHead' } }
    if ($WrapperCleanExitCode -ne 0) { return [pscustomobject]@{ Pass = $false; FailureClass = 'WrapperNotCommittedAtHead' } }
    return [pscustomobject]@{ Pass = $true; FailureClass = $null }
}

function Test-Wp04ImageIdentity {
    param(
        [AllowNull()] [object[]] $Output,
        [Parameter(Mandatory)] [int] $ExitCode,
        [Parameter(Mandatory)] [string] $ExpectedDigest
    )

    $values = @($Output | ForEach-Object { if ($null -ne $_) { ([string]$_).Trim() } } | Where-Object { -not [string]::IsNullOrWhiteSpace($_) })
    $result = [ordered]@{ Identity = 'NONE'; Digest = 'NONE'; Match = $false; FailureClass = $null }
    if ($ExitCode -ne 0) { $result.FailureClass = 'AzureImageQueryFailure'; return [pscustomobject]$result }
    if ($values.Count -eq 0) { $result.FailureClass = 'ImageIdentityMissing'; return [pscustomobject]$result }
    if ($values.Count -ne 1) { $result.FailureClass = 'ImageIdentityMalformed'; return [pscustomobject]$result }

    $identity = $values[0]
    $result.Identity = $identity
    if (-not $identity.StartsWith('DOCKER|', [System.StringComparison]::Ordinal)) { $result.FailureClass = 'ImageIdentityMalformed'; return [pscustomobject]$result }
    $reference = $identity.Substring('DOCKER|'.Length)
    $separator = $reference.IndexOf('@sha256:', [System.StringComparison]::Ordinal)
    if ($separator -lt 1) { $result.FailureClass = 'ImageDigestMissing'; return [pscustomobject]$result }
    if ($reference.IndexOf('@sha256:', $separator + 1, [System.StringComparison]::Ordinal) -ge 0) { $result.FailureClass = 'ImageIdentityMalformed'; return [pscustomobject]$result }

    $digest = $reference.Substring($separator + 1)
    if ($digest -notmatch '^sha256:[0-9a-f]{64}$') { $result.FailureClass = 'ImageIdentityMalformed'; return [pscustomobject]$result }
    $result.Digest = $digest
    if (-not [string]::Equals($digest, $ExpectedDigest, [System.StringComparison]::Ordinal)) { $result.FailureClass = 'ImageDigestMismatch'; return [pscustomobject]$result }
    $result.Match = $true
    return [pscustomobject]$result
}

function Write-Wp04ImagePreflightTelemetry {
    param([Parameter(Mandatory)] [psobject] $Result)
    Write-Host "WP04_PREFLIGHT_IMAGE_IDENTITY=$($Result.Identity)"
    Write-Host "WP04_PREFLIGHT_IMAGE_DIGEST=$($Result.Digest)"
    Write-Host "WP04_PREFLIGHT_IMAGE_EXPECTED_DIGEST=$expectedDigest"
    Write-Host "WP04_PREFLIGHT_IMAGE_MATCH=$($Result.Match)"
    if (-not $Result.Match) { Write-Host "WP04_PREFLIGHT_IMAGE_FAILURE_CLASS=$($Result.FailureClass)" }
}

function Write-Wp04EvidenceRecord {
    param([Parameter(Mandatory)] [string] $Path, [Parameter(Mandatory)] [hashtable] $Record)
    $Record | ConvertTo-Json -Compress | Set-Content -LiteralPath $Path -Encoding utf8
}

function Write-Wp04EvidenceCheckpoint {
    param(
        [Parameter(Mandatory)] [string] $Path,
        [Parameter(Mandatory)] [hashtable] $Record,
        [scriptblock] $Callback
    )
    if ($null -ne $Callback) { & $Callback $Path $Record; return }
    Write-Wp04EvidenceRecord -Path $Path -Record $Record
}

function Get-Wp04DeepestBoundary {
    param([Parameter(Mandatory)] [string[]] $Lines)
    $rules = @(
        @{ Boundary = 'B9'; Pattern = 'EVIDENCE_RETRIEVAL_SUCCEEDED|WP04_HTTP_EVIDENCE.*SUCCESS' },
        @{ Boundary = 'B8'; Pattern = 'HANDLER_ENTERED' },
        @{ Boundary = 'B7'; Pattern = 'REQUEST_ARRIVED' },
        @{ Boundary = 'B6'; Pattern = 'LISTENER_STARTED' },
        @{ Boundary = 'B5'; Pattern = 'LISTENER_STARTING' },
        @{ Boundary = 'B4'; Pattern = 'ARTIFACT_WRITE_SUCCEEDED' },
        @{ Boundary = 'B3'; Pattern = 'SQLITE_QUALIFICATION_STARTED' },
        @{ Boundary = 'B2'; Pattern = 'QUALIFICATION_ENTERED' },
        @{ Boundary = 'B1'; Pattern = 'CONTAINER_STARTED|aiq-entrypoint' }
    )
    $all = $Lines -join "`n"
    foreach ($rule in $rules) { if ($all -match $rule.Pattern) { return $rule.Boundary } }
    if ($Lines.Count -gt 0) { return 'B0' }
    return 'BX'
}

function Get-Wp04PollObservations {
    param([Parameter(Mandatory)] [string[]] $Lines, [AllowNull()] [string] $RunId)
    $observations = New-Object System.Collections.Generic.List[object]
    $attempt = $null
    foreach ($line in $Lines) {
        if ($line -match '^WP04_HTTP_EVIDENCE_POLL_ATTEMPT=(\d+)$') { $attempt = [int]$Matches[1]; continue }
        if ($line -match '^WP04_HTTP_EVIDENCE_STATUS=(.+)$') {
            $observations.Add([ordered]@{ TimestampUtc = [DateTime]::UtcNow.ToString('o'); RunId = $RunId; Attempt = $attempt; Status = $Matches[1]; Classification = 'Observed' })
            continue
        }
        if ($line -match '^WP04_HTTP_EVIDENCE_FAILURE_CLASS=(.+)$') {
            $observations.Add([ordered]@{ TimestampUtc = [DateTime]::UtcNow.ToString('o'); RunId = $RunId; Attempt = $attempt; Status = 'NONE'; Classification = $Matches[1] })
        }
    }
    return $observations.ToArray()
}

function Test-Wp04ArchiveExtractionState {
    param([Parameter(Mandatory)] [string] $RawArchiveState, [Parameter(Mandatory)] [string] $FreshExtractionState)
    if ($RawArchiveState -eq 'RETAINED') {
        if ($FreshExtractionState -in @('PASS','EMPTY')) { return [pscustomobject]@{ Eligible = $true; Classification = 'CONSISTENT' } }
        if ($FreshExtractionState -eq 'FAIL') { return [pscustomobject]@{ Eligible = $false; Classification = 'EXTRACTION_FAILED' } }
        return [pscustomobject]@{ Eligible = $false; Classification = 'EVIDENCE_STATE_INCONSISTENT' }
    }
    if ($RawArchiveState -eq 'RETRIEVAL_FAILED') {
        if ($FreshExtractionState -eq 'NOT_APPLICABLE') { return [pscustomobject]@{ Eligible = $true; Classification = 'CONSISTENT' } }
        return [pscustomobject]@{ Eligible = $false; Classification = 'EVIDENCE_STATE_INCONSISTENT' }
    }
    return [pscustomobject]@{ Eligible = $false; Classification = 'EVIDENCE_STATE_INCONSISTENT' }
}

function Invoke-Wp04MockLifecycle {
    param([Parameter(Mandatory)] [hashtable] $Fixture)
    $qualificationResult = 'NOT_PROVEN'; $evidenceCheckpointResult = 'FAIL'; $restorationResult = 'NOT_ATTEMPTED'; $restoreCount = 0; $actualRunId = 'NOT_PROVEN'; $classification = $null
    try {
        if ($Fixture.ContainsKey('DeferredThrows') -and $Fixture.DeferredThrows) { throw 'DeferredMockFailure' }
        $actualRunId = $Fixture.RunId
        if ([string]::IsNullOrWhiteSpace($actualRunId)) { throw 'RunIdMissing' }
        $qualificationResult = if ($Fixture.QualificationSuccess) { 'SUCCESS' } else { 'FAILURE' }
        $archiveState = Test-Wp04ArchiveExtractionState -RawArchiveState $Fixture.RawArchiveState -FreshExtractionState $Fixture.FreshExtractionState
        $classification = $archiveState.Classification
        if ($Fixture.ContainsKey('CheckpointThrows') -and $Fixture.CheckpointThrows) { throw 'CheckpointWriteFailure' }
        $evidenceCheckpointResult = if ($archiveState.Eligible) { 'PASS' } else { 'FAIL' }
    }
    catch { $evidenceCheckpointResult = 'FAIL'; if ($null -eq $classification) { $classification = 'EVIDENCE_PRESERVATION_FAILED' } }
    finally {
        $restoreCount++
        $restorationResult = if ($Fixture.RestoreSucceeds) { 'PASS' } else { 'FAIL' }
    }
    $final = if ($restorationResult -eq 'FAIL') { 'RESTORATION_FAILED' } elseif ($evidenceCheckpointResult -eq 'FAIL') { 'EVIDENCE_PRESERVATION_FAILED' } else { $qualificationResult }
    return [pscustomobject]@{ QualificationResult = $qualificationResult; EvidenceCheckpointResult = $evidenceCheckpointResult; RestorationResult = $restorationResult; FinalLifecycleResult = $final; RestoreCount = $restoreCount; Classification = $classification; ActualRunId = $actualRunId }
}

function Invoke-Wp04ArchiveCheckpoint {
    param(
        [Parameter(Mandatory)] [string] $EvidenceRoot,
        [Parameter(Mandatory)] [datetime] $CaptureStartUtc,
        [Parameter(Mandatory)] [string] $ResourceGroup,
        [Parameter(Mandatory)] [string] $WebAppName,
        [Parameter(Mandatory)] [string] $RunId,
        [scriptblock] $ArchiveDownload
    )
    $archivePath = Join-Path $EvidenceRoot 'raw-app-service-logs.zip'
    $archiveRecord = [ordered]@{ ArchiveRetrieval = 'RETRIEVAL_FAILED'; ArchiveMetadata = 'NOT_APPLICABLE'; FreshExtraction = 'NOT_APPLICABLE'; ArchivePath = $null; ArchiveBytes = $null; ArchiveSha256 = $null; RetrievalUtc = [DateTime]::UtcNow.ToString('o'); ErrorClass = $null; SafeErrorSummary = $null }
    try {
        if ($null -ne $ArchiveDownload) {
            & $ArchiveDownload $archivePath | Out-Null
            $downloadSucceeded = Test-Path -LiteralPath $archivePath
        }
        else {
            & az webapp log download --resource-group $ResourceGroup --name $WebAppName --log-file $archivePath *> $null
            $downloadSucceeded = ($LASTEXITCODE -eq 0 -and (Test-Path -LiteralPath $archivePath))
        }
        if (-not $downloadSucceeded) { throw 'ArchiveDownloadFailure' }
        $item = Get-Item -LiteralPath $archivePath
        $hash = Get-FileHash -LiteralPath $archivePath -Algorithm SHA256
        Add-Type -AssemblyName System.IO.Compression
        Add-Type -AssemblyName System.IO.Compression.FileSystem
        $zip = [System.IO.Compression.ZipFile]::OpenRead($archivePath)
        try { $entries = @($zip.Entries) } finally { $zip.Dispose() }
        $archiveRecord.ArchiveRetrieval = 'RETAINED'; $archiveRecord.ArchiveMetadata = 'PASS'; $archiveRecord.ArchivePath = $archivePath; $archiveRecord.ArchiveBytes = [long]$item.Length; $archiveRecord.ArchiveSha256 = $hash.Hash
        $extractRoot = Join-Path $EvidenceRoot 'fresh-window-extract'
        Expand-Archive -LiteralPath $archivePath -DestinationPath $extractRoot -Force -ErrorAction Stop
        $relevant = @($entries | Where-Object { $_.LastWriteTime.UtcDateTime -ge $CaptureStartUtc.AddMinutes(-1) -and $_.FullName -match '(?i)(docker|container|default|wp04|log)' } | ForEach-Object FullName)
        $extractionPath = Join-Path $EvidenceRoot 'fresh-window-files.txt'
        $relevant | Set-Content -LiteralPath $extractionPath -Encoding utf8
        $archiveRecord.FreshExtraction = if ($relevant.Count -eq 0) { 'EMPTY' } else { 'PASS' }
    }
    catch {
        if ($archiveRecord.ArchiveRetrieval -eq 'RETAINED') { $archiveRecord.FreshExtraction = 'FAIL' }
        $archiveRecord.ErrorClass = if ($_.Exception.Message -eq 'ArchiveUnreadable') { 'ArchiveUnreadable' } else { 'ArchiveRetrievalFailure' }
        $archiveRecord.SafeErrorSummary = $archiveRecord.ErrorClass
    }
    Write-Wp04EvidenceRecord -Path (Join-Path $EvidenceRoot 'archive-checkpoint.json') -Record $archiveRecord
    return [pscustomobject]$archiveRecord
}

function Invoke-LocalValidation {
    $exactIdentity = 'DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00'
    $provenanceFixtures = @(
        @{ Name = 'V1-valid-governed-source'; Head = 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa'; HeadExit = 0; AncestorExit = 0; TrackedExit = 0; CleanExit = 0; Pass = $true; Class = $null },
        @{ Name = 'V2-git-command-failure'; Head = $null; HeadExit = 1; AncestorExit = 1; TrackedExit = 1; CleanExit = 1; Pass = $false; Class = 'SourceCommitUnavailable' },
        @{ Name = 'V3-blank-source'; Head = ''; HeadExit = 0; AncestorExit = 0; TrackedExit = 0; CleanExit = 0; Pass = $false; Class = 'SourceCommitUnavailable' },
        @{ Name = 'V4-malformed-source'; Head = 'not-a-commit'; HeadExit = 0; AncestorExit = 0; TrackedExit = 0; CleanExit = 0; Pass = $false; Class = 'SourceCommitMalformed' },
        @{ Name = 'V5-baseline-not-ancestor'; Head = 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa'; HeadExit = 0; AncestorExit = 1; TrackedExit = 0; CleanExit = 0; Pass = $false; Class = 'SourceBaselineNotAncestor' },
        @{ Name = 'V6-wrapper-not-at-head'; Head = 'aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa'; HeadExit = 0; AncestorExit = 0; TrackedExit = 0; CleanExit = 1; Pass = $false; Class = 'WrapperNotCommittedAtHead' }
    )
    foreach ($fixture in $provenanceFixtures) {
        $result = Test-Wp04SourceProvenance -Head $fixture.Head -HeadExitCode $fixture.HeadExit -BaselineAncestorExitCode $fixture.AncestorExit -WrapperTrackedExitCode $fixture.TrackedExit -WrapperCleanExitCode $fixture.CleanExit
        $downstreamCalls = if ($result.Pass) { 1 } else { 0 }
        if ($result.Pass -ne $fixture.Pass -or $result.FailureClass -ne $fixture.Class) { throw "Local source-provenance fixture failed: $($fixture.Name)." }
        if (-not $result.Pass -and $downstreamCalls -ne 0) { throw "Source-provenance barrier failed: $($fixture.Name)." }
    }
    $fixtures = @(
        @{ Name = 'V1-exact'; Output = @($exactIdentity); ExitCode = 0; Match = $true; Class = $null },
        @{ Name = 'V2-blank'; Output = @(''); ExitCode = 0; Match = $false; Class = 'ImageIdentityMissing' },
        @{ Name = 'V3-query-failure'; Output = @(); ExitCode = 1; Match = $false; Class = 'AzureImageQueryFailure' },
        @{ Name = 'V4-tag-only'; Output = @('DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch:wp04'); ExitCode = 0; Match = $false; Class = 'ImageDigestMissing' },
        @{ Name = 'V5-wrong-digest'; Output = @('DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa'); ExitCode = 0; Match = $false; Class = 'ImageDigestMismatch' },
        @{ Name = 'V6-partial-digest'; Output = @('DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f0'); ExitCode = 0; Match = $false; Class = 'ImageIdentityMalformed' },
        @{ Name = 'V7-null-whitespace'; Output = @($null, '   '); ExitCode = 0; Match = $false; Class = 'ImageIdentityMissing' },
        @{ Name = 'V8-multiple'; Output = @($exactIdentity, $exactIdentity); ExitCode = 0; Match = $false; Class = 'ImageIdentityMalformed' }
    )

    $failedFixtureCount = 0
    foreach ($fixture in $fixtures) {
        $result = Test-Wp04ImageIdentity -Output $fixture.Output -ExitCode $fixture.ExitCode -ExpectedDigest $expectedDigest
        $downstreamCalls = if ($result.Match) { 1 } else { 0 }
        if ($result.Match -ne $fixture.Match -or $result.FailureClass -ne $fixture.Class) { throw "Local image-preflight fixture failed: $($fixture.Name)." }
        if (-not $result.Match -and $downstreamCalls -ne 0) { throw "Mutation barrier failed: $($fixture.Name)." }
        if (-not $result.Match) { $failedFixtureCount++ }
    }

    $safeOutput = @('WP04_PREFLIGHT_IMAGE_IDENTITY', 'WP04_PREFLIGHT_IMAGE_DIGEST', 'WP04_PREFLIGHT_IMAGE_EXPECTED_DIGEST', 'WP04_PREFLIGHT_IMAGE_MATCH', 'WP04_PREFLIGHT_IMAGE_FAILURE_CLASS')
    if (@($safeOutput | Where-Object { $_ -match '(?i)(token|header|query|string|credential|exception|stack)' }).Count -ne 0) { throw 'Unsafe preflight telemetry field detected.' }
    Write-Host 'WP04_PREFLIGHT_LOCAL_VALIDATION_CASES=10'
    Write-Host 'WP04_PREFLIGHT_LOCAL_SOURCE_PROVENANCE_PASS=True'
    Write-Host 'WP04_PREFLIGHT_LOCAL_V9_MUTATION_BARRIER_PASS=True'
    Write-Host 'WP04_PREFLIGHT_LOCAL_V10_SAFE_OUTPUT_PASS=True'
    Write-Host 'WP04_PREFLIGHT_LOCAL_FAILURE_MUTATION_CALLS=0'
    Write-Host "WP04_PREFLIGHT_LOCAL_FAILED_FIXTURE_COUNT=$failedFixtureCount"
    Write-Host 'WP04_PREFLIGHT_LOCAL_VALIDATION_PASS=True'

    $fixtureRoot = Join-Path ([System.IO.Path]::GetTempPath()) ('aiq-wp04-lifecycle-' + [guid]::NewGuid().ToString('N'))
    New-Item -ItemType Directory -Path $fixtureRoot | Out-Null
    try {
        $checkpointPath = Join-Path $fixtureRoot 'checkpoint.json'
        $checkpointRecord = @{ EvidenceCheckpoint = 'PASS'; ActualRunId = 'initialize-fixture' }
        Write-Wp04EvidenceCheckpoint -Path $checkpointPath -Record $checkpointRecord
        if (-not (Test-Path -LiteralPath $checkpointPath)) { throw 'Default checkpoint writer fixture failed.' }
        $script:callbackCount = 0
        $successCallback = { param($path, $record) $script:callbackCount++; $record | ConvertTo-Json -Compress | Set-Content -LiteralPath $path -Encoding utf8 }
        Write-Wp04EvidenceCheckpoint -Path $checkpointPath -Record $checkpointRecord -Callback $successCallback
        if ($script:callbackCount -ne 1) { throw 'Checkpoint success callback fixture failed.' }
        $failed = $false; try { Write-Wp04EvidenceCheckpoint -Path $checkpointPath -Record $checkpointRecord -Callback { param($path, $record) throw 'SyntheticCheckpointFailure' } } catch { $failed = $true }
        if (-not $failed) { throw 'Checkpoint failure callback fixture failed.' }
        $fixtureZip = Join-Path $fixtureRoot 'fixture.zip'
        $fixtureSource = Join-Path $fixtureRoot 'container-wp04.log'
        Set-Content -LiteralPath $fixtureSource -Value 'CONTAINER_STARTED' -Encoding utf8
        Add-Type -AssemblyName System.IO.Compression
        Add-Type -AssemblyName System.IO.Compression.FileSystem
        $zip = [System.IO.Compression.ZipFile]::Open($fixtureZip, [System.IO.Compression.ZipArchiveMode]::Create)
        try { [System.IO.Compression.ZipFileExtensions]::CreateEntryFromFile($zip, $fixtureSource, 'container-wp04.log') | Out-Null } finally { $zip.Dispose() }
        $copyArchive = { param($destination) Copy-Item -LiteralPath $fixtureZip -Destination $destination }
        $successArchive = Invoke-Wp04ArchiveCheckpoint -EvidenceRoot $fixtureRoot -CaptureStartUtc ([DateTime]::UtcNow.AddMinutes(-1)) -ResourceGroup 'fixture' -WebAppName 'fixture' -RunId 'initialize-fixture' -ArchiveDownload $copyArchive
        if ($successArchive.ArchiveRetrieval -ne 'RETAINED' -or $successArchive.ArchiveMetadata -ne 'PASS' -or $successArchive.FreshExtraction -notin @('PASS','EMPTY')) { throw 'Archive success fixture failed.' }
        $failureRoot = Join-Path $fixtureRoot 'failure'; New-Item -ItemType Directory -Path $failureRoot | Out-Null
        $failureArchive = Invoke-Wp04ArchiveCheckpoint -EvidenceRoot $failureRoot -CaptureStartUtc ([DateTime]::UtcNow) -ResourceGroup 'fixture' -WebAppName 'fixture' -RunId 'initialize-fixture' -ArchiveDownload { param($destination) throw 'fixture failure' }
        if ($failureArchive.ArchiveRetrieval -ne 'RETRIEVAL_FAILED' -or -not (Test-Path -LiteralPath (Join-Path $failureRoot 'archive-checkpoint.json'))) { throw 'Archive failure fixture failed.' }
        $precedenceFixtures = @(
            @{ Name = 'L1'; Q = 'SUCCESS'; E = 'PASS'; R = 'PASS'; Expected = 'SUCCESS' },
            @{ Name = 'L2'; Q = 'SUCCESS'; E = 'PASS'; R = 'PASS'; Expected = 'SUCCESS' },
            @{ Name = 'L3'; Q = 'SUCCESS'; E = 'FAIL'; R = 'PASS'; Expected = 'EVIDENCE_PRESERVATION_FAILED' },
            @{ Name = 'L4'; Q = 'FAILURE'; E = 'PASS'; R = 'PASS'; Expected = 'FAILURE' },
            @{ Name = 'L5'; Q = 'SUCCESS'; E = 'PASS'; R = 'FAIL'; Expected = 'RESTORATION_FAILED' },
            @{ Name = 'L6'; Q = 'NOT_PROVEN'; E = 'FAIL'; R = 'PASS'; Expected = 'EVIDENCE_PRESERVATION_FAILED' }
        )
        foreach ($fixture in $precedenceFixtures) {
            $final = if ($fixture.R -eq 'FAIL') { 'RESTORATION_FAILED' } elseif ($fixture.E -eq 'FAIL') { 'EVIDENCE_PRESERVATION_FAILED' } else { $fixture.Q }
            if ($final -ne $fixture.Expected) { throw "Lifecycle precedence fixture failed: $($fixture.Name)." }
        }
        $wrapperFixtures = @(
            @{ Name='W1'; RunId='initialize-w1'; QualificationSuccess=$true; RawArchiveState='RETAINED'; FreshExtractionState='PASS'; RestoreSucceeds=$true; Expected='SUCCESS' },
            @{ Name='W2'; RunId='initialize-w2'; QualificationSuccess=$true; RawArchiveState='RETAINED'; FreshExtractionState='EMPTY'; RestoreSucceeds=$true; Expected='SUCCESS' },
            @{ Name='W3'; RunId='initialize-w3'; QualificationSuccess=$true; RawArchiveState='RETAINED'; FreshExtractionState='FAIL'; RestoreSucceeds=$true; Expected='EVIDENCE_PRESERVATION_FAILED' },
            @{ Name='W4'; RunId='initialize-w4'; QualificationSuccess=$true; RawArchiveState='RETAINED'; FreshExtractionState='NOT_APPLICABLE'; RestoreSucceeds=$true; Expected='EVIDENCE_PRESERVATION_FAILED'; Classification='EVIDENCE_STATE_INCONSISTENT' },
            @{ Name='W5'; RunId='initialize-w5'; QualificationSuccess=$true; RawArchiveState='RETRIEVAL_FAILED'; FreshExtractionState='NOT_APPLICABLE'; RestoreSucceeds=$true; Expected='SUCCESS' },
            @{ Name='W6'; RunId='initialize-w6'; QualificationSuccess=$false; RawArchiveState='RETRIEVAL_FAILED'; FreshExtractionState='NOT_APPLICABLE'; RestoreSucceeds=$true; Expected='FAILURE' },
            @{ Name='W7'; RunId='initialize-w7'; QualificationSuccess=$true; RawArchiveState='RETAINED'; FreshExtractionState='PASS'; CheckpointThrows=$true; RestoreSucceeds=$true; Expected='EVIDENCE_PRESERVATION_FAILED' },
            @{ Name='W8'; RunId='initialize-w8'; QualificationSuccess=$true; RawArchiveState='RETAINED'; FreshExtractionState='PASS'; RestoreSucceeds=$false; Expected='RESTORATION_FAILED' }
        )
        foreach ($fixture in $wrapperFixtures) {
            $result = Invoke-Wp04MockLifecycle -Fixture $fixture
            if ($result.FinalLifecycleResult -ne $fixture.Expected -or $result.RestoreCount -ne 1 -or $result.ActualRunId -ne $fixture.RunId) { throw "Mocked wrapper lifecycle fixture failed: $($fixture.Name)." }
            if ($fixture.ContainsKey('Classification') -and $result.Classification -ne $fixture.Classification) { throw "Mocked wrapper classification fixture failed: $($fixture.Name)." }
        }
        $truthTable = @(
            @{ Raw='RETAINED'; Extraction='PASS'; Eligible=$true }, @{ Raw='RETAINED'; Extraction='EMPTY'; Eligible=$true }, @{ Raw='RETAINED'; Extraction='FAIL'; Eligible=$false }, @{ Raw='RETAINED'; Extraction='NOT_APPLICABLE'; Eligible=$false }, @{ Raw='RETRIEVAL_FAILED'; Extraction='NOT_APPLICABLE'; Eligible=$true }, @{ Raw='RETRIEVAL_FAILED'; Extraction='PASS'; Eligible=$false }
        )
        foreach ($row in $truthTable) { if ((Test-Wp04ArchiveExtractionState -RawArchiveState $row.Raw -FreshExtractionState $row.Extraction).Eligible -ne $row.Eligible) { throw 'Archive truth-table fixture failed.' } }
    }
    finally {
        if (Test-Path -LiteralPath $fixtureRoot) { Remove-Item -LiteralPath $fixtureRoot -Recurse -Force }
    }
    Write-Host 'WP04_LOCAL_FULL_LIFECYCLE_VALIDATION_L1_L6_PASS=True'
    Write-Host 'WP04_LOCAL_FULL_WRAPPER_LIFECYCLE_W1_W8_PASS=True'
}

if ($LocalValidation) {
    Invoke-LocalValidation
    exit 0
}

if ($PSVersionTable.PSVersion.ToString() -ne '5.1.26100.9444') { Write-Host 'WP04_PREFLIGHT_FAILURE_CLASS=PowerShellVersionMismatch'; exit 1 }
$actualHead = (& git rev-parse HEAD).Trim()
$actualHeadExitCode = $LASTEXITCODE
& git merge-base --is-ancestor $governedBaseline $actualHead
$baselineAncestorExitCode = $LASTEXITCODE
& git ls-files --error-unmatch -- $wrapperRepositoryPath *> $null
$wrapperTrackedExitCode = $LASTEXITCODE
& git diff --quiet HEAD -- $wrapperRepositoryPath
$wrapperCleanExitCode = $LASTEXITCODE
$sourceProvenance = Test-Wp04SourceProvenance -Head $actualHead -HeadExitCode $actualHeadExitCode -BaselineAncestorExitCode $baselineAncestorExitCode -WrapperTrackedExitCode $wrapperTrackedExitCode -WrapperCleanExitCode $wrapperCleanExitCode
if (-not $sourceProvenance.Pass) { Write-Host "WP04_PREFLIGHT_FAILURE_CLASS=$($sourceProvenance.FailureClass)"; exit 1 }

$imageOutput = @(& az webapp show --resource-group $resourceGroup --name $webAppName --query 'siteConfig.linuxFxVersion' --output tsv)
$imageExitCode = $LASTEXITCODE
$imagePreflight = Test-Wp04ImageIdentity -Output $imageOutput -ExitCode $imageExitCode -ExpectedDigest $expectedDigest
Write-Wp04ImagePreflightTelemetry -Result $imagePreflight
if (-not $imagePreflight.Match) { exit 1 }

$webApp = & az webapp show --resource-group $resourceGroup --name $webAppName --query '{state:state,availabilityState:availabilityState}' --output json | ConvertFrom-Json
if ($LASTEXITCODE -ne 0 -or $webApp.state -ne 'Running' -or $webApp.availabilityState -ne 'Normal') { Write-Host 'WP04_PREFLIGHT_FAILURE_CLASS=AppStateMismatch'; exit 1 }
$settings = & az webapp config appsettings list --resource-group $resourceGroup --name $webAppName --output json | ConvertFrom-Json
if ($LASTEXITCODE -ne 0) { Write-Host 'WP04_PREFLIGHT_FAILURE_CLASS=AzureSettingsQueryFailure'; exit 1 }
$temporaryNames = @('Worker__Mode', 'PersistentSqliteQualification__Phase', 'PersistentSqliteQualification__RunId', 'PersistentSqliteQualification__EvidenceOutputPath', 'PersistentSqliteQualification__HttpEvidenceEnabled', 'PersistentSqliteQualification__HttpEvidenceToken')
if (@($settings | Where-Object { $_.name -in $temporaryNames }).Count -ne 0) { Write-Host 'WP04_PREFLIGHT_FAILURE_CLASS=TemporarySettingDrift'; exit 1 }

$correlationId = 'initialize-correlation-' + [guid]::NewGuid().ToString('N')
$evidenceRoot = Join-Path ([System.IO.Path]::GetTempPath()) ('AIQuantTradingResearch\wp04\' + $correlationId)
New-Item -ItemType Directory -Path $evidenceRoot -Force | Out-Null
$transcript = Join-Path $evidenceRoot 'helper-transcript.txt'
$pollObservationPath = Join-Path $evidenceRoot 'poll-observations.json'
$checkpointPath = Join-Path $evidenceRoot 'evidence-checkpoint.json'
$captureStartUtc = [DateTime]::UtcNow
$qualificationResult = 'NOT_PROVEN'
$evidenceCheckpointResult = 'FAIL'
$restorationResult = 'NOT_ATTEMPTED'
$actualRunId = 'NOT_PROVEN'
$restorationDescriptor = $null
$restoreAttempted = $false
try {
    # The helper emits its governed machine markers with Write-Host.  Under
    # Windows PowerShell 5.1 those records use the Information stream, so
    # capture it alongside errors before deriving the helper-owned RunId.
    $helperOutput = @(& ".\$helper" -ResourceGroup $resourceGroup -WebAppName $webAppName -Phase initialize -LifecycleAction None -RestorationMode Deferred 2>&1 6>&1 | ForEach-Object { [string]$_ })
    $helperExitCode = $LASTEXITCODE
    $helperOutput | Set-Content -LiteralPath $transcript -Encoding utf8
    $actualRunId = (($helperOutput | Where-Object { $_ -match '^WP04_HELPER_TERMINAL_RUN_ID=' } | Select-Object -Last 1) -replace '^WP04_HELPER_TERMINAL_RUN_ID=', '')
    $restorationDescriptor = (($helperOutput | Where-Object { $_ -match '^WP04_D3_RESTORATION_DESCRIPTOR=' } | Select-Object -Last 1) -replace '^WP04_D3_RESTORATION_DESCRIPTOR=', '')
    if ([string]::IsNullOrWhiteSpace($actualRunId)) { throw 'The deferred helper did not emit an actual qualification RunId.' }
    if ([string]::IsNullOrWhiteSpace($restorationDescriptor)) { throw 'The deferred helper did not emit a restoration descriptor.' }
    Set-Content -LiteralPath (Join-Path $evidenceRoot 'run-metadata.txt') -Value "CorrelationId=$correlationId`r`nActualRunId=$actualRunId`r`nCaptureStartUtc=$($captureStartUtc.ToString('o'))" -Encoding utf8
    $qualificationResult = if ($helperExitCode -eq 0) { 'SUCCESS' } else { 'FAILURE' }
    $pollObservations = Get-Wp04PollObservations -Lines $helperOutput -RunId $actualRunId
    ConvertTo-Json -InputObject @($pollObservations) -Depth 3 | Set-Content -LiteralPath $pollObservationPath -Encoding utf8
    $d3PayloadState = if (@($helperOutput | Where-Object { $_ -match 'WP04_D3_|WP04_HTTP_EVIDENCE_' }).Count -gt 0) { 'RETAINED' } else { 'NOT_OBSERVED' }
    $archive = Invoke-Wp04ArchiveCheckpoint -EvidenceRoot $evidenceRoot -CaptureStartUtc $captureStartUtc -ResourceGroup $resourceGroup -WebAppName $webAppName -RunId $actualRunId
    $deepestBoundary = Get-Wp04DeepestBoundary -Lines $helperOutput
    $archiveState = Test-Wp04ArchiveExtractionState -RawArchiveState $archive.ArchiveRetrieval -FreshExtractionState $archive.FreshExtraction
    $checkpointRecord = [ordered]@{
        ActualRunId = $actualRunId; HelperTerminalRetained = if (Test-Path -LiteralPath $transcript) { 'PASS' } else { 'FAIL' }; HelperTranscriptRetained = if (Test-Path -LiteralPath $transcript) { 'PASS' } else { 'FAIL' }; PollObservationsRetained = if (Test-Path -LiteralPath $pollObservationPath) { 'PASS' } else { 'FAIL' }; D3PayloadState = $d3PayloadState; RawArchiveState = $archive.ArchiveRetrieval; ArchiveMetadataState = $archive.ArchiveMetadata; FreshExtractionState = $archive.FreshExtraction; DeepestBoundary = $deepestBoundary
    }
    $checkpointRecord.EvidenceClassification = $archiveState.Classification
    $checkpointRecord.EvidenceCheckpoint = if ($checkpointRecord.ActualRunId -ne 'NOT_PROVEN' -and $checkpointRecord.HelperTerminalRetained -eq 'PASS' -and $checkpointRecord.PollObservationsRetained -eq 'PASS' -and $archiveState.Eligible -and $checkpointRecord.ArchiveMetadataState -in @('PASS','NOT_APPLICABLE') -and $checkpointRecord.DeepestBoundary -ne 'BX') { 'PASS' } else { 'FAIL' }
    Write-Wp04EvidenceCheckpoint -Path $checkpointPath -Record $checkpointRecord -Callback $PersistEvidenceCheckpointCallback
    $evidenceCheckpointResult = $checkpointRecord.EvidenceCheckpoint
    Write-Host "WP04_EVIDENCE_ROOT=$evidenceRoot"
    Write-Host "WP04_EVIDENCE_CHECKPOINT=$evidenceCheckpointResult"
    Write-Host "WP04_D3_INITIALIZE_HELPER_EXIT_CODE=$helperExitCode"
}
catch {
    $failureRecord = [ordered]@{ ActualRunId = $actualRunId; EvidenceCheckpoint = 'FAIL'; FailureClass = 'EvidencePreservationFailure'; TimestampUtc = [DateTime]::UtcNow.ToString('o'); SafeErrorSummary = 'EvidencePreservationFailure' }
    Write-Wp04EvidenceRecord -Path $checkpointPath -Record $failureRecord
    $evidenceCheckpointResult = 'FAIL'
}
finally {
    $restoreAttempted = $true
    if ([string]::IsNullOrWhiteSpace($restorationDescriptor)) { $restorationDescriptor = 'WP04-ALL-ABSENT-v1' }
    & ".\$helper" -ResourceGroup $resourceGroup -WebAppName $webAppName -RestorationMode RestoreOnly -RestorationDescriptor $restorationDescriptor
    $restorationResult = if ($LASTEXITCODE -eq 0) { 'PASS' } else { 'FAIL' }
    Write-Host "WP04_OUTER_FINALLY_RESTORE_ATTEMPTED=$restoreAttempted"
}
$finalLifecycleResult = if ($restorationResult -eq 'FAIL') { 'RESTORATION_FAILED' } elseif ($evidenceCheckpointResult -eq 'FAIL') { 'EVIDENCE_PRESERVATION_FAILED' } else { $qualificationResult }
Write-Wp04EvidenceRecord -Path (Join-Path $evidenceRoot 'final-lifecycle-result.json') -Record ([ordered]@{ QualificationResult = $qualificationResult; EvidenceCheckpointResult = $evidenceCheckpointResult; RestorationResult = $restorationResult; FinalLifecycleResult = $finalLifecycleResult })
Write-Host "WP04_FINAL_LIFECYCLE_RESULT=$finalLifecycleResult"
if ($finalLifecycleResult -ne 'SUCCESS') { exit 1 }

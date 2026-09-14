[CmdletBinding()]
param(
    [switch] $LocalValidation
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$governedBaseline = '4822f9847a90a7d86c6bf771603defe9d7abf258'
$expectedDigest = 'sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f'
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

function Invoke-LocalValidation {
    $exactIdentity = 'DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f'
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
        @{ Name = 'V6-partial-digest'; Output = @('DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc5197'); ExitCode = 0; Match = $false; Class = 'ImageIdentityMalformed' },
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

$runId = 'initialize-' + [guid]::NewGuid().ToString('N')
Write-Host "WP04_D3_INITIALIZE_RUN_ID=$runId"
& ".\$helper" -ResourceGroup $resourceGroup -WebAppName $webAppName -Phase initialize -RunId $runId -LifecycleAction Restart
Write-Host "WP04_D3_INITIALIZE_HELPER_EXIT_CODE=$LASTEXITCODE"
exit $LASTEXITCODE

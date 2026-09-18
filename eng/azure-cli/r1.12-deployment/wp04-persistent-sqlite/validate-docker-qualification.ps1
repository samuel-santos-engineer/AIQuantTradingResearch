Set-Location C:\projects\github\AIQuantTradingResearch
$ErrorActionPreference = 'Stop'

$oldImage = 'aiq-r112-wp04:82cfad99e7024f07a84a287302bf4f11'
$oldVolume = 'aiq-r112-wp04-82cfad99e7024f07a84a287302bf4f11'
$runId = [guid]::NewGuid().ToString('N')
$image = "aiq-r112-wp04:$runId"
$volume = "aiq-r112-wp04-$runId"
$initializeContainer = "aiq-r112-wp04-initialize-$runId"
$reopenContainer = "aiq-r112-wp04-reopen-$runId"
$abortedInitializeContainer = 'aiq-r112-wp04-initialize-b09109943214425b909e583b15f3d4e6'
$abortedReopenContainer = 'aiq-r112-wp04-reopen-b09109943214425b909e583b15f3d4e6'
$localNonRootRuntimePass = $false
$localStorageOwnerPass = $false
$localStorageModePass = $false

function Invoke-Docker {
    param([Parameter(ValueFromRemainingArguments = $true)][string[]]$Arguments)
    & docker @Arguments
    if ($LASTEXITCODE -ne 0) {
        throw "Docker failed: docker $($Arguments -join ' ')"
    }
}

function Test-DockerContainer {
    param([string]$Name)
    $previousErrorActionPreference = $ErrorActionPreference
    try {
        $ErrorActionPreference = 'Continue'
        & docker container inspect $Name 2>$null 1>$null
        return ($LASTEXITCODE -eq 0)
    }
    finally {
        $ErrorActionPreference = $previousErrorActionPreference
    }
}

function Test-DockerImage {
    param([string]$Name)
    $previousErrorActionPreference = $ErrorActionPreference
    try {
        $ErrorActionPreference = 'Continue'
        & docker image inspect $Name 2>$null 1>$null
        return ($LASTEXITCODE -eq 0)
    }
    finally {
        $ErrorActionPreference = $previousErrorActionPreference
    }
}

function Test-DockerVolume {
    param([string]$Name)
    $previousErrorActionPreference = $ErrorActionPreference
    try {
        $ErrorActionPreference = 'Continue'
        & docker volume inspect $Name 2>$null 1>$null
        return ($LASTEXITCODE -eq 0)
    }
    finally {
        $ErrorActionPreference = $previousErrorActionPreference
    }
}

function Get-QualificationRecord {
    param(
        [string]$Container,
        [string]$Phase
    )

    $previousErrorActionPreference = $ErrorActionPreference
    try {
        $ErrorActionPreference = 'Continue'
        $logs = (& docker logs $Container 2>&1 | ForEach-Object { $_.ToString() } | Out-String)
        $logsExit = $LASTEXITCODE
    }
    finally {
        $ErrorActionPreference = $previousErrorActionPreference
    }
    if ($logsExit -ne 0) {
        throw "Docker logs failed for phase $Phase."
    }
    $recordLine = @(
        $logs -split "`r?`n" |
        Where-Object { $_.TrimStart().StartsWith('{') -and $_ -match '"RecordVersion"' }
    ) | Select-Object -Last 1

    if ([string]::IsNullOrWhiteSpace($recordLine)) {
        throw "No qualification JSON record was emitted for phase $Phase."
    }

    $record = $recordLine | ConvertFrom-Json
    Write-Host "WP04_$($Phase.ToUpperInvariant())_QUALIFICATION_JSON=$($record | ConvertTo-Json -Compress)"

    $valid =
        $record.Phase -eq $Phase -and
        $record.SchemaVersion -eq 4 -and
        $record.JournalMode -eq 'delete' -and
        $record.IntegrityCheck -eq 'ok' -and
        $record.QuickCheck -eq 'ok' -and
        $record.PersistenceContinuity -eq $true -and
        -not [string]::IsNullOrWhiteSpace($record.AcceptedEvidenceIdentity) -and
        $record.AcceptedEvidenceCount -ge 1

    if (-not $valid) {
        throw "Qualification record for phase $Phase did not satisfy the WP04 contract."
    }

    return $record
}

function Assert-ContainerRuntime {
    param(
        [string]$Container,
        [string]$Phase
    )

    $top = (& docker top $Container -eo user,pid,ppid,args | Out-String)
    $identity = (& docker exec --user 1000:1000 $Container id 2>&1 | Out-String)
    $identityExit = $LASTEXITCODE
    $ownership = (& docker exec --user 1000:1000 $Container stat '-c' 'owner=%u:%g mode=%a' /home/data 2>&1 | Out-String)
    $ownershipExit = $LASTEXITCODE
    & docker exec --user 1000:1000 $Container test -w /home/data 2>$null 1>$null
    $writableExit = $LASTEXITCODE
    $storage = "$identity`n$ownership"
    $storageExit = if ($identityExit -eq 0 -and $ownershipExit -eq 0) { $writableExit } else { 1 }

    Write-Host "WP04_$($Phase.ToUpperInvariant())_PROCESS_TOPOLOGY=$($top.Trim())"
    Write-Host "WP04_$($Phase.ToUpperInvariant())_STORAGE_EVIDENCE=$($storage.Trim())"
    Write-Host "WP04_$($Phase.ToUpperInvariant())_STORAGE_EXEC_EXIT_CODE=$storageExit"

    $nonRootRuntime = $top -match '(?m)^(aiq|1000)\s+' -and -not ($top -match '(?m)^root\s+')
    $ownerPass = $storage -match 'owner=1000:1000'
    $modePass = $storage -match 'mode=750'
    $writablePass = $storageExit -eq 0

    Write-Host "WP04_$($Phase.ToUpperInvariant())_NONROOT_RUNTIME_PASS=$nonRootRuntime"
    Write-Host "WP04_$($Phase.ToUpperInvariant())_STORAGE_OWNER_PASS=$ownerPass"
    Write-Host "WP04_$($Phase.ToUpperInvariant())_STORAGE_MODE_PASS=$modePass"
    Write-Host "WP04_$($Phase.ToUpperInvariant())_STORAGE_WRITABLE_PASS=$writablePass"

    if (-not ($nonRootRuntime -and $ownerPass -and $modePass -and $writablePass)) {
        throw "Runtime or storage contract failed for phase $Phase."
    }

    return [pscustomobject]@{
        NonRootRuntime = $nonRootRuntime
        StorageOwner = $ownerPass
        StorageMode = $modePass
    }
}

try {
    Write-Host '=== RECONCILE PRE-REMEDIATION CLEANUP STATE ==='
    $oldImagePresent = Test-DockerImage $oldImage
    $oldVolumePresent = Test-DockerVolume $oldVolume
    Write-Host "WP04_PRE_REMEDIATION_IMAGE_PRESENT=$oldImagePresent"
    Write-Host "WP04_PRE_REMEDIATION_VOLUME_PRESENT=$oldVolumePresent"
    if ($oldImagePresent -or $oldVolumePresent) {
        throw 'Pre-remediation artifacts must already be absent before this clean-recreate rerun.'
    }

    foreach ($staleContainer in @($abortedInitializeContainer, $abortedReopenContainer)) {
        if (Test-DockerContainer $staleContainer) {
            Invoke-Docker rm --force $staleContainer
            Write-Host "WP04_ABORTED_CONTAINER_REMOVED[$staleContainer]=True"
        }
        else {
            Write-Host "WP04_ABORTED_CONTAINER_PRESENT[$staleContainer]=False"
        }
    }
    Write-Host 'WP04_PRE_REMEDIATION_CLEANUP_STATE_RECONCILED=True'

    Write-Host '=== BUILD FRESH E2-A IMAGE ==='
    Invoke-Docker build --label 'aiq.wp=release-1.12-wp04' --tag $image .
    Invoke-Docker volume create $volume
    Write-Host "WP04_FRESH_IMAGE=$image"
    Write-Host "WP04_FRESH_VOLUME=$volume"

    $commonArguments = @(
        'run', '--detach',
        '--mount', "type=volume,source=$volume,target=/home/data,volume-nocopy",
        '--env', 'Worker__Mode=PersistentSqliteQualification',
        '--env', 'Persistence__DatabasePath=/home/data/aiquant.db',
        '--env', 'Persistence__CreateParentDirectoryForInitialization=true',
        '--env', 'Visualization__HandoffPath=/runtime/visualization-read-model.json',
        '--env', 'STREAMLIT_SERVER_ADDRESS=0.0.0.0',
        '--env', 'STREAMLIT_SERVER_PORT=8501'
    )

    Write-Host '=== INITIALIZE QUALIFICATION ==='
    Invoke-Docker @commonArguments --name $initializeContainer --env 'PersistentSqliteQualification__Phase=initialize' $image
    Start-Sleep -Seconds 4
    $initializeRuntime = Assert-ContainerRuntime -Container $initializeContainer -Phase 'initialize'
    $initializeRecord = Get-QualificationRecord -Container $initializeContainer -Phase 'initialize'
    Invoke-Docker stop $initializeContainer
    Invoke-Docker rm $initializeContainer
    Write-Host 'WP04_INITIALIZE_CONTAINER_REMOVE_EXIT_CODE=0'

    Write-Host '=== REOPEN QUALIFICATION ==='
    Invoke-Docker @commonArguments --name $reopenContainer --env 'PersistentSqliteQualification__Phase=reopen' $image
    Start-Sleep -Seconds 4
    $reopenRuntime = Assert-ContainerRuntime -Container $reopenContainer -Phase 'reopen'
    $reopenRecord = Get-QualificationRecord -Container $reopenContainer -Phase 'reopen'

    $continuityPass =
        $initializeRecord.AcceptedEvidenceIdentity -eq $reopenRecord.AcceptedEvidenceIdentity -and
        $initializeRecord.AcceptedEvidenceCount -eq $reopenRecord.AcceptedEvidenceCount

    Write-Host "WP04_LOCAL_PERSISTENCE_RECREATE_PASS=$continuityPass"
    if (-not $continuityPass) {
        throw 'Reopen qualification did not preserve deterministic accepted evidence.'
    }

    $localNonRootRuntimePass = $initializeRuntime.NonRootRuntime -and $reopenRuntime.NonRootRuntime
    $localStorageOwnerPass = $initializeRuntime.StorageOwner -and $reopenRuntime.StorageOwner
    $localStorageModePass = $initializeRuntime.StorageMode -and $reopenRuntime.StorageMode

    Invoke-Docker stop $reopenContainer
    Invoke-Docker rm $reopenContainer
    Write-Host 'WP04_REOPEN_CONTAINER_REMOVE_EXIT_CODE=0'
}
finally {
    foreach ($container in @($initializeContainer, $reopenContainer)) {
        if (Test-DockerContainer $container) {
            & docker rm --force $container *> $null
            Write-Host "WP04_LOCAL_CONTAINER_CLEANUP_EXIT_CODE[$container]=$LASTEXITCODE"
        }
    }

    if (Test-DockerVolume $volume) {
        & docker volume rm $volume *> $null
        Write-Host "WP04_LOCAL_VOLUME_CLEANUP_EXIT_CODE=$LASTEXITCODE"
    }

    if (Test-DockerImage $image) {
        & docker image rm $image *> $null
        Write-Host "WP04_LOCAL_IMAGE_CLEANUP_EXIT_CODE=$LASTEXITCODE"
    }

    Write-Host "WP04_LOCAL_NONROOT_RUNTIME_PASS=$localNonRootRuntimePass"
    Write-Host "WP04_LOCAL_STORAGE_OWNER_PASS=$localStorageOwnerPass"
    Write-Host "WP04_LOCAL_STORAGE_MODE_PASS=$localStorageModePass"
    Write-Host "WP04_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=$(Test-DockerImage $image)"
    Write-Host "WP04_LOCAL_VOLUME_PRESENT_AFTER_CLEANUP=$(Test-DockerVolume $volume)"
    Write-Host "WP04_LOCAL_CLEANUP_COMPLETE=$(-not (Test-DockerImage $image) -and -not (Test-DockerVolume $volume))"
}

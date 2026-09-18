Set-Location C:\projects\github\AIQuantTradingResearch
$ErrorActionPreference = 'Stop'

$runId = [guid]::NewGuid().ToString('N')
$image = "aiq-r112-wp04:$runId"
$volume = "aiq-r112-wp04-$runId"
$first = "aiq-r112-wp04-initialize-$runId"
$second = "aiq-r112-wp04-reopen-$runId"
$port = 18112
$createdImage = $false
$createdVolume = $false

function Test-DockerObject {
    param([string[]] $Arguments)
    & docker @Arguments *> $null
    return $LASTEXITCODE -eq 0
}

function Wait-QualificationRecord {
    param([string] $ContainerName, [string] $ExpectedPhase)

    $record = $null
    for ($attempt = 1; $attempt -le 20; $attempt++) {
        Write-Host "WP04_${ExpectedPhase}_RECORD_POLL_ATTEMPT=$attempt"
        $lines = @(& docker logs $ContainerName 2>&1)
        $jsonLine = $lines |
            Where-Object { $_ -match '^\{"RecordVersion":1,' } |
            Select-Object -Last 1

        if ($null -ne $jsonLine) {
            $record = $jsonLine | ConvertFrom-Json
            break
        }

        Start-Sleep -Seconds 1
    }

    if ($null -eq $record) {
        throw "No application-owned qualification record was emitted for phase $ExpectedPhase."
    }

    Write-Host "WP04_${ExpectedPhase}_QUALIFICATION_RECORD=$($record | ConvertTo-Json -Compress)"
    return $record
}

function Wait-StreamlitHealth {
    for ($attempt = 1; $attempt -le 20; $attempt++) {
        Write-Host "WP04_STREAMLIT_HEALTH_POLL_ATTEMPT=$attempt"
        try {
            $response = Invoke-WebRequest -Uri "http://localhost:$port/_stcore/health" -UseBasicParsing -TimeoutSec 3
            Write-Host "WP04_STREAMLIT_HEALTH_STATUS_CODE=$($response.StatusCode)"
            if ($response.StatusCode -eq 200) {
                return
            }
        }
        catch {
            Write-Host "WP04_STREAMLIT_HEALTH_ERROR=$($_.Exception.Message)"
        }

        Start-Sleep -Seconds 1
    }

    throw 'Streamlit did not become healthy.'
}

try {
    Write-Host '=== BUILD TEMPORARY WP04 IMAGE ==='
    & docker build --tag $image .
    Write-Host "WP04_LOCAL_IMAGE_BUILD_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Docker build failed.' }
    $createdImage = $true

    Write-Host '=== CREATE NAMED PERSISTENCE VOLUME ==='
    & docker volume create $volume | Out-Null
    Write-Host "WP04_LOCAL_VOLUME_CREATE_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Docker volume creation failed.' }
    $createdVolume = $true

    Write-Host '=== RUN INITIALIZE CONTAINER ==='
    & docker run --detach --name $first --publish "${port}:8501" `
        --mount "type=volume,source=$volume,target=/home/data,volume-nocopy" `
        --env 'Worker__Mode=PersistentSqliteQualification' `
        --env 'PersistentSqliteQualification__Phase=initialize' `
        --env 'Persistence__DatabasePath=/home/data/aiquant.db' `
        --env 'Persistence__CreateParentDirectoryForInitialization=true' `
        $image
    Write-Host "WP04_LOCAL_INITIALIZE_RUN_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Initialize container did not start.' }

    Wait-StreamlitHealth
    $initializeRecord = Wait-QualificationRecord -ContainerName $first -ExpectedPhase 'INITIALIZE'

    Write-Host '=== STOP AND REMOVE INITIALIZE CONTAINER ==='
    & docker stop $first | Out-Null
    Write-Host "WP04_LOCAL_INITIALIZE_STOP_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Initialize container stop failed.' }

    & docker rm $first | Out-Null
    Write-Host "WP04_LOCAL_INITIALIZE_REMOVE_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Initialize container removal failed.' }

    Write-Host '=== RUN REOPEN CONTAINER WITH SAME VOLUME ==='
    & docker run --detach --name $second --publish "${port}:8501" `
        --mount "type=volume,source=$volume,target=/home/data,volume-nocopy" `
        --env 'Worker__Mode=PersistentSqliteQualification' `
        --env 'PersistentSqliteQualification__Phase=reopen' `
        --env 'Persistence__DatabasePath=/home/data/aiquant.db' `
        --env 'Persistence__CreateParentDirectoryForInitialization=true' `
        $image
    Write-Host "WP04_LOCAL_REOPEN_RUN_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Reopen container did not start.' }

    Wait-StreamlitHealth
    $reopenRecord = Wait-QualificationRecord -ContainerName $second -ExpectedPhase 'REOPEN'

    $semanticPass =
        $initializeRecord.RecordVersion -eq 1 -and
        $reopenRecord.RecordVersion -eq 1 -and
        $initializeRecord.Phase -eq 'initialize' -and
        $reopenRecord.Phase -eq 'reopen' -and
        $initializeRecord.SchemaVersion -eq 4 -and
        $reopenRecord.SchemaVersion -eq 4 -and
        $initializeRecord.JournalMode -eq 'delete' -and
        $reopenRecord.JournalMode -eq 'delete' -and
        $initializeRecord.IntegrityCheck -eq 'ok' -and
        $reopenRecord.IntegrityCheck -eq 'ok' -and
        $initializeRecord.QuickCheck -eq 'ok' -and
        $reopenRecord.QuickCheck -eq 'ok' -and
        $initializeRecord.AcceptedEvidenceIdentity -eq $reopenRecord.AcceptedEvidenceIdentity -and
        $initializeRecord.AcceptedEvidenceCount -eq $reopenRecord.AcceptedEvidenceCount -and
        $initializeRecord.AcceptedEvidenceCount -eq 1 -and
        $initializeRecord.PersistenceContinuity -eq $true -and
        $reopenRecord.PersistenceContinuity -eq $true

    Write-Host "WP04_LOCAL_PERSISTENCE_RECREATE_PASS=$semanticPass"
    if (-not $semanticPass) { throw 'Application-owned persistence continuity evidence did not satisfy WP04.' }
}
finally {
    foreach ($container in @($first, $second)) {
        if (Test-DockerObject -Arguments @('container', 'inspect', $container)) {
            & docker rm --force $container | Out-Null
            Write-Host "WP04_LOCAL_CONTAINER_CLEANUP_EXIT_CODE[$container]=$LASTEXITCODE"
        }
    }

    if ($createdVolume -and (Test-DockerObject -Arguments @('volume', 'inspect', $volume))) {
        & docker volume rm $volume | Out-Null
        Write-Host "WP04_LOCAL_VOLUME_REMOVE_EXIT_CODE=$LASTEXITCODE"
    }

    if ($createdImage -and (Test-DockerObject -Arguments @('image', 'inspect', $image))) {
        & docker image rm --force $image | Out-Null
        Write-Host "WP04_LOCAL_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"
    }

    Write-Host "WP04_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=$(Test-DockerObject -Arguments @('image', 'inspect', $image))"
    Write-Host "WP04_LOCAL_VOLUME_PRESENT_AFTER_CLEANUP=$(Test-DockerObject -Arguments @('volume', 'inspect', $volume))"
    Write-Host "WP04_LOCAL_CLEANUP_COMPLETE=$(-not (Test-DockerObject -Arguments @('image', 'inspect', $image)) -and -not (Test-DockerObject -Arguments @('volume', 'inspect', $volume)))"
}
Set-Location C:\projects\github\AIQuantTradingResearch
$ErrorActionPreference = 'Stop'

$run = [guid]::NewGuid().ToString('N')
$image = "aiq-r112-wp04-http:$run"
$volume = "aiq-r112-wp04-http-$run"
$initialize = "aiq-r112-wp04-http-initialize-$run"
$reopen = "aiq-r112-wp04-http-reopen-$run"
$tokenBytes = New-Object byte[] 32
$tokenGenerator = [System.Security.Cryptography.RandomNumberGenerator]::Create()
try {
    $tokenGenerator.GetBytes($tokenBytes)
}
finally {
    $tokenGenerator.Dispose()
}
$token = [Convert]::ToBase64String($tokenBytes)
$headers = @{ 'X-WP04-Evidence-Token' = $token }

function Invoke-HttpProbe {
    param([Parameter(Mandatory)] [string] $Uri, [hashtable] $Headers)
    try {
        $parameters = @{ Uri = $Uri; TimeoutSec = 10; UseBasicParsing = $true; ErrorAction = 'Stop' }
        if ($null -ne $Headers) { $parameters.Headers = $Headers }
        $response = Invoke-WebRequest @parameters
        return [pscustomobject]@{ StatusCode = [int]$response.StatusCode; Content = [string]$response.Content }
    }
    catch {
        $webResponse = $_.Exception.Response
        if ($null -eq $webResponse) {
            return [pscustomobject]@{ StatusCode = $null; Content = $null }
        }
        $reader = New-Object System.IO.StreamReader($webResponse.GetResponseStream())
        try {
            return [pscustomobject]@{ StatusCode = [int]$webResponse.StatusCode; Content = $reader.ReadToEnd() }
        }
        finally {
            $reader.Dispose()
            $webResponse.Dispose()
        }
    }
}

function Invoke-Qualification {
    param(
        [string] $Container,
        [string] $Phase,
        [string] $RunId
    )

    & docker run --detach --name $Container --publish 18501:8501 --volume "${volume}:/home/data" `
        --env 'Worker__Mode=PersistentSqliteQualification' `
        --env "PersistentSqliteQualification__Phase=$Phase" `
        --env "PersistentSqliteQualification__RunId=$RunId" `
        --env 'PersistentSqliteQualification__EvidenceOutputPath=/home/data/wp04-qualification/evidence.json' `
        --env 'PersistentSqliteQualification__HttpEvidenceEnabled=true' `
        --env "PersistentSqliteQualification__HttpEvidenceToken=$token" `
        --env 'Persistence__DatabasePath=/home/data/aiquant.db' `
        --env 'Persistence__CreateParentDirectoryForInitialization=true' `
        --env 'Visualization__HandoffPath=/runtime/visualization-read-model.json' `
        $image
    if ($LASTEXITCODE -ne 0) { throw "Docker run failed for $Phase." }

    $uri = "http://127.0.0.1:18501/internal/wp04/persistence-qualification?runId=$RunId"
    $unauthorized = $null
    for ($attempt = 1; $attempt -le 36; $attempt++) {
        $unauthorized = Invoke-HttpProbe -Uri $uri
        if ($unauthorized.StatusCode -eq 401) { break }
        Start-Sleep -Seconds 5
    }
    Write-Host "WP04_${Phase}_UNAUTHORIZED_HTTP_STATUS=$($unauthorized.StatusCode)"
    if ($unauthorized.StatusCode -ne 401) { throw "Qualification HTTP endpoint did not reject a missing token for $Phase." }

    $response = $null
    for ($attempt = 1; $attempt -le 36; $attempt++) {
        $response = Invoke-HttpProbe -Uri $uri -Headers $headers
        Write-Host "WP04_${Phase}_HTTP_POLL_ATTEMPT=$attempt"
        if ($response.StatusCode -eq 200) { break }
        if ($response.StatusCode -ne 404 -and $null -ne $response.StatusCode) { throw "Qualification HTTP endpoint returned unexpected status $($response.StatusCode) for $Phase." }
        Start-Sleep -Seconds 5
    }

    if ($null -eq $response -or $response.StatusCode -ne 200) {
        throw "Qualification HTTP retrieval did not return 200 for $Phase."
    }

    $record = $response.Content | ConvertFrom-Json
    $record | ConvertTo-Json -Compress
    Write-Host "WP04_${Phase}_INTEGRITY_CHECK=$($record.IntegrityCheck)"
    Write-Host "WP04_${Phase}_JOURNAL_MODE=$($record.JournalMode)"
    Write-Host "WP04_${Phase}_SCHEMA_VERSION=$($record.SchemaVersion)"
    Write-Host "WP04_${Phase}_ACCEPTED_EVIDENCE_COUNT=$($record.AcceptedEvidenceCount)"

    & docker wait $Container | Out-Null
    Write-Host "WP04_${Phase}_CONTAINER_EXIT_CODE=$LASTEXITCODE"
    & docker inspect $Container --format '{{.State.ExitCode}}'
    Write-Host "WP04_${Phase}_INSPECT_EXIT_CODE=$LASTEXITCODE"
}

try {
    & docker build --tag $image .
    Write-Host "WP04_LOCAL_IMAGE_BUILD_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Docker build failed.' }

    & docker volume create $volume | Out-Null
    Write-Host "WP04_LOCAL_VOLUME_CREATE_EXIT_CODE=$LASTEXITCODE"

    Invoke-Qualification -Container $initialize -Phase 'initialize' -RunId "initialize-$run"
    Invoke-Qualification -Container $reopen -Phase 'reopen' -RunId "reopen-$run"

    Write-Host 'WP04_LOCAL_PERSISTENCE_RECREATE_PASS=True'
}
finally {
    $originalErrorActionPreference = $ErrorActionPreference
    $ErrorActionPreference = 'Continue'
    foreach ($container in @($initialize, $reopen)) {
        & docker container rm --force $container *> $null
        Write-Host "WP04_LOCAL_CONTAINER_CLEANUP_EXIT_CODE[$container]=$LASTEXITCODE"
    }

    & docker volume rm $volume *> $null
    Write-Host "WP04_LOCAL_VOLUME_CLEANUP_EXIT_CODE=$LASTEXITCODE"

    & docker image rm $image *> $null
    Write-Host "WP04_LOCAL_IMAGE_CLEANUP_EXIT_CODE=$LASTEXITCODE"

    & docker image inspect $image *> $null
    $imagePresent = $LASTEXITCODE -eq 0
    Write-Host "WP04_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=$imagePresent"

    & docker volume inspect $volume *> $null
    $volumePresent = $LASTEXITCODE -eq 0
    Write-Host "WP04_LOCAL_VOLUME_PRESENT_AFTER_CLEANUP=$volumePresent"

    $ErrorActionPreference = $originalErrorActionPreference
    Write-Host 'WP04_LOCAL_CLEANUP_COMPLETE=True'
}

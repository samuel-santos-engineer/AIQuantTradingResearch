$ErrorActionPreference = 'Stop'

$runId = [guid]::NewGuid().ToString('N')
$imageTag = "aiq-release-112-wp02-postmerge:$runId"
$normalContainer = "aiq-wp02-normal-$runId"
$missingContainer = "aiq-wp02-missing-$runId"
$childFailureContainer = "aiq-wp02-child-fail-$runId"
$port = 18501
$syntheticValue = 'container-validation-placeholder'

function Show-Code([string]$name) {
    Write-Host "$name=$LASTEXITCODE"
}

try {
    Write-Host '=== CLEAN POST-MERGE IMAGE BUILD ==='
    & docker build --pull --tag $imageTag .
    Show-Code 'WP02_POSTMERGE_BUILD_EXIT_CODE'

    Write-Host '=== NORMAL RUNTIME: WORKER + STREAMLIT + LISTENER ==='
    & docker run --detach --name $normalContainer --publish "${port}:8501" `
      -e "TwelveData__ApiKey=$syntheticValue" `
      -e 'Dataset__Target=SIMULATED-USD' `
      -e 'Dataset__From=2024-01-01T00:00:00.0000000+00:00' `
      -e 'Dataset__To=2024-01-01T00:03:00.0000000+00:00' `
      -e 'Worker__Replay__ReplayIdentity=simulated-live-replay-v1' `
      -e 'Worker__Replay__Target=SIMULATED-USD' `
      -e 'Worker__Replay__StartingTick=0' `
      -e 'Worker__Replay__RequestedObservationCount=3' `
      $imageTag
    Show-Code 'WP02_POSTMERGE_NORMAL_RUN_EXIT_CODE'

    $healthy = $false
    for ($attempt = 1; $attempt -le 20; $attempt++) {
        Write-Host "WP02_POSTMERGE_HEALTH_ATTEMPT=$attempt"
        try {
            $response = Invoke-WebRequest -UseBasicParsing -Uri "http://127.0.0.1:$port/" -TimeoutSec 5
            Write-Host "WP02_POSTMERGE_LISTENER_STATUS_CODE=$($response.StatusCode)"
            if ($response.StatusCode -eq 200) {
                $healthy = $true
                break
            }
        } catch {
            Write-Host "WP02_POSTMERGE_LISTENER_ERROR=$($_.Exception.Message)"
        }
        Start-Sleep -Seconds 1
    }
    Write-Host "WP02_POSTMERGE_LISTENER_REACHABLE=$healthy"
    if (-not $healthy) { throw 'Normal Streamlit listener did not become reachable.' }

    & docker top $normalContainer
    Show-Code 'WP02_POSTMERGE_NORMAL_PROCESS_TOPOLOGY_EXIT_CODE'

    & docker logs $normalContainer
    Show-Code 'WP02_POSTMERGE_NORMAL_LOGS_EXIT_CODE'

    Write-Host '=== NORMAL GRACEFUL STOP ==='
    & docker stop --time 10 $normalContainer
    Show-Code 'WP02_POSTMERGE_NORMAL_STOP_EXIT_CODE'

    $normalExit = & docker wait $normalContainer
    Write-Host "WP02_POSTMERGE_NORMAL_CONTAINER_EXIT_CODE=$normalExit"
    Show-Code 'WP02_POSTMERGE_NORMAL_WAIT_EXIT_CODE'

    Write-Host '=== MISSING REQUIRED CONFIGURATION: FAIL CLOSED ==='
    & docker run --detach --name $missingContainer `
      -e 'Dataset__Target=SIMULATED-USD' `
      -e 'Dataset__From=2024-01-01T00:00:00.0000000+00:00' `
      -e 'Dataset__To=2024-01-01T00:03:00.0000000+00:00' `
      -e 'Worker__Replay__ReplayIdentity=simulated-live-replay-v1' `
      -e 'Worker__Replay__Target=SIMULATED-USD' `
      -e 'Worker__Replay__StartingTick=0' `
      -e 'Worker__Replay__RequestedObservationCount=3' `
      $imageTag
    Show-Code 'WP02_POSTMERGE_MISSING_RUN_EXIT_CODE'

    $missingExit = & docker wait $missingContainer
    Write-Host "WP02_POSTMERGE_MISSING_CONTAINER_EXIT_CODE=$missingExit"
    Show-Code 'WP02_POSTMERGE_MISSING_WAIT_EXIT_CODE'

    & docker logs $missingContainer
    Show-Code 'WP02_POSTMERGE_MISSING_LOGS_EXIT_CODE'

    Write-Host '=== REQUIRED STREAMLIT CHILD FAILURE ==='
    & docker run --detach --name $childFailureContainer `
      -e "TwelveData__ApiKey=$syntheticValue" `
      -e 'Dataset__Target=SIMULATED-USD' `
      -e 'Dataset__From=2024-01-01T00:00:00.0000000+00:00' `
      -e 'Dataset__To=2024-01-01T00:03:00.0000000+00:00' `
      -e 'Worker__Replay__ReplayIdentity=simulated-live-replay-v1' `
      -e 'Worker__Replay__Target=SIMULATED-USD' `
      -e 'Worker__Replay__StartingTick=0' `
      -e 'Worker__Replay__RequestedObservationCount=3' `
      -e 'STREAMLIT_SERVER_PORT=not-a-port' `
      $imageTag
    Show-Code 'WP02_POSTMERGE_CHILD_FAILURE_RUN_EXIT_CODE'

    $childExit = & docker wait $childFailureContainer
    Write-Host "WP02_POSTMERGE_CHILD_FAILURE_CONTAINER_EXIT_CODE=$childExit"
    Show-Code 'WP02_POSTMERGE_CHILD_FAILURE_WAIT_EXIT_CODE'

    & docker logs $childFailureContainer
    Show-Code 'WP02_POSTMERGE_CHILD_FAILURE_LOGS_EXIT_CODE'

    Write-Host '=== NON-PRINTING IMAGE SECRET ISOLATION ==='
    $historyText = (& docker history --no-trunc $imageTag 2>&1 | Out-String)
    Write-Host "WP02_POSTMERGE_HISTORY_EXIT_CODE=$LASTEXITCODE"
    Write-Host "WP02_POSTMERGE_HISTORY_CONTAINS_SYNTHETIC_VALUE=$($historyText.Contains($syntheticValue))"
    Write-Host "WP02_POSTMERGE_HISTORY_CONTAINS_API_KEY_NAME=$($historyText.Contains('TwelveData__ApiKey'))"

    $inspectText = (& docker image inspect $imageTag 2>&1 | Out-String)
    Write-Host "WP02_POSTMERGE_IMAGE_INSPECT_EXIT_CODE=$LASTEXITCODE"
    Write-Host "WP02_POSTMERGE_CONFIG_CONTAINS_SYNTHETIC_VALUE=$($inspectText.Contains($syntheticValue))"
    Write-Host "WP02_POSTMERGE_CONFIG_CONTAINS_API_KEY_NAME=$($inspectText.Contains('TwelveData__ApiKey'))"
}
finally {
    Write-Host '=== EXPLICIT WP02 TEMPORARY DOCKER CLEANUP ==='
    foreach ($container in @($normalContainer, $missingContainer, $childFailureContainer)) {
        & docker rm --force $container *> $null
        Write-Host "WP02_POSTMERGE_CONTAINER_REMOVE_EXIT_CODE=$LASTEXITCODE"
    }

    & docker image rm $imageTag *> $null
    Write-Host "WP02_POSTMERGE_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"

    $ownedContainers = @(
        & docker ps --all --filter 'name=aiq-wp02-' --format '{{.Names}}' |
        Where-Object { $_ -and $_.Trim() }
    ).Count
    $ownedImages = @(
        & docker image ls --format '{{.Repository}}:{{.Tag}}' |
        Where-Object { $_ -like 'aiq-release-112-wp02-postmerge:*' }
    ).Count
    $listener = Get-NetTCPConnection -LocalPort $port -State Listen -ErrorAction SilentlyContinue

    Write-Host "WP02_POSTMERGE_OWNED_CONTAINERS_REMAINING=$ownedContainers"
    Write-Host "WP02_POSTMERGE_OWNED_IMAGES_REMAINING=$ownedImages"
    Write-Host "WP02_POSTMERGE_PORT_18501_LISTENER_PRESENT=$([bool]$listener)"
}
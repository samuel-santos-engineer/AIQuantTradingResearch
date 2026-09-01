$ErrorActionPreference = 'Stop'

$imageTag = 'aiq-release-112-wp02:0d334987effa480f8176abff08f8d7a5'
$runId = [guid]::NewGuid().ToString('N')
$missingSecretContainer = "aiq-release-112-wp02-missing-$runId"
$streamlitFailureContainer = "aiq-release-112-wp02-streamlit-fail-$runId"

function Show-ExitCode([string]$name) {
    Write-Host "$name=$LASTEXITCODE"
}

& docker image inspect $imageTag *> $null
Show-ExitCode 'WP02_FAILURE_IMAGE_PRECHECK_EXIT_CODE'

Write-Host '=== MISSING REQUIRED SECRET: MUST FAIL CLOSED ==='
& docker run --detach --name $missingSecretContainer `
  -e 'Dataset__Target=SIMULATED-USD' `
  -e 'Dataset__From=2024-01-01T00:00:00.0000000+00:00' `
  -e 'Dataset__To=2024-01-01T00:03:00.0000000+00:00' `
  -e 'Worker__Replay__ReplayIdentity=simulated-live-replay-v1' `
  -e 'Worker__Replay__Target=SIMULATED-USD' `
  -e 'Worker__Replay__StartingTick=0' `
  -e 'Worker__Replay__RequestedObservationCount=3' `
  $imageTag
Show-ExitCode 'WP02_MISSING_SECRET_RUN_EXIT_CODE'

$missingExit = & docker wait $missingSecretContainer
$missingWaitInvocationExit = $LASTEXITCODE
Write-Host "WP02_MISSING_SECRET_CONTAINER_EXIT_CODE=$missingExit"
Write-Host "WP02_MISSING_SECRET_WAIT_EXIT_CODE=$missingWaitInvocationExit"

& docker logs $missingSecretContainer
Show-ExitCode 'WP02_MISSING_SECRET_LOGS_EXIT_CODE'

& docker rm $missingSecretContainer
Show-ExitCode 'WP02_MISSING_SECRET_REMOVE_EXIT_CODE'

Write-Host '=== REQUIRED STREAMLIT CHILD FAILURE: MUST TERMINATE CONTAINER ==='
& docker run --detach --name $streamlitFailureContainer `
  -e 'TwelveData__ApiKey=container-validation-placeholder' `
  -e 'Dataset__Target=SIMULATED-USD' `
  -e 'Dataset__From=2024-01-01T00:00:00.0000000+00:00' `
  -e 'Dataset__To=2024-01-01T00:03:00.0000000+00:00' `
  -e 'Worker__Replay__ReplayIdentity=simulated-live-replay-v1' `
  -e 'Worker__Replay__Target=SIMULATED-USD' `
  -e 'Worker__Replay__StartingTick=0' `
  -e 'Worker__Replay__RequestedObservationCount=3' `
  -e 'STREAMLIT_SERVER_PORT=not-a-port' `
  $imageTag
Show-ExitCode 'WP02_STREAMLIT_FAILURE_RUN_EXIT_CODE'

$streamlitExit = & docker wait $streamlitFailureContainer
$streamlitWaitInvocationExit = $LASTEXITCODE
Write-Host "WP02_STREAMLIT_FAILURE_CONTAINER_EXIT_CODE=$streamlitExit"
Write-Host "WP02_STREAMLIT_FAILURE_WAIT_EXIT_CODE=$streamlitWaitInvocationExit"

& docker logs $streamlitFailureContainer
Show-ExitCode 'WP02_STREAMLIT_FAILURE_LOGS_EXIT_CODE'

& docker rm $streamlitFailureContainer
Show-ExitCode 'WP02_STREAMLIT_FAILURE_REMOVE_EXIT_CODE'

$missingPresent = (& docker ps --all --filter "name=$missingSecretContainer" --format '{{.ID}}').Length -gt 0
$streamlitPresent = (& docker ps --all --filter "name=$streamlitFailureContainer" --format '{{.ID}}').Length -gt 0
Write-Host "WP02_MISSING_SECRET_CONTAINER_PRESENT_AFTER_CLEANUP=$missingPresent"
Write-Host "WP02_STREAMLIT_FAILURE_CONTAINER_PRESENT_AFTER_CLEANUP=$streamlitPresent"
$ErrorActionPreference = 'Stop'

$runId = [guid]::NewGuid().ToString('N')
$imageTag = "aiq-release-112-wp02:$runId"
$containerName = "aiq-release-112-wp02-$runId"
$port = 18501

Write-Host "WP02_IMAGE=$imageTag"
Write-Host "WP02_CONTAINER=$containerName"
Write-Host "WP02_PORT=$port"

Write-Host '=== BUILD IMAGE ==='
docker build --tag $imageTag .
Write-Host "WP02_DOCKER_BUILD_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Docker build failed.' }

Write-Host '=== START REPLAY/STREAMLIT COMPOSITION ==='
docker run --detach --name $containerName --publish "127.0.0.1:${port}:8501" `
  --env 'TwelveData__ApiKey=container-validation-placeholder' `
  --env 'Dataset__Target=SIMULATED-USD' `
  --env 'Dataset__From=2024-01-01T00:00:00.0000000+00:00' `
  --env 'Dataset__To=2024-01-01T00:03:00.0000000+00:00' `
  --env 'Worker__Mode=Replay' `
  --env 'Worker__Replay__ReplayIdentity=simulated-live-replay-v1' `
  --env 'Worker__Replay__Target=SIMULATED-USD' `
  --env 'Worker__Replay__StartingTick=0' `
  --env 'Worker__Replay__RequestedObservationCount=3' `
  $imageTag
Write-Host "WP02_DOCKER_RUN_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Container start failed.' }

Write-Host '=== WAIT FOR STREAMLIT LISTENER ==='
$listenerReady = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
  try {
    $response = Invoke-WebRequest -UseBasicParsing -TimeoutSec 5 "http://127.0.0.1:$port/"
    Write-Host "WP02_LISTENER_ATTEMPT=$attempt"
    Write-Host "WP02_LISTENER_STATUS_CODE=$($response.StatusCode)"
    $listenerReady = $response.StatusCode -eq 200
    if ($listenerReady) { break }
  }
  catch {
    Write-Host "WP02_LISTENER_ATTEMPT=$attempt"
    Write-Host "WP02_LISTENER_ERROR=$($_.Exception.Message)"
  }
  Start-Sleep -Seconds 2
}
Write-Host "WP02_LISTENER_READY=$listenerReady"
if (-not $listenerReady) { throw 'Streamlit listener did not become reachable.' }

Write-Host '=== PROCESS TOPOLOGY ==='
docker top $containerName -eo pid,ppid,comm,args
Write-Host "WP02_DOCKER_TOP_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== SECRET-SAFE RUNTIME LOGS ==='
docker logs $containerName
Write-Host "WP02_DOCKER_LOGS_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== GRACEFUL STOP ==='
docker stop --time 15 $containerName
Write-Host "WP02_DOCKER_STOP_EXIT_CODE=$LASTEXITCODE"

$exitCode = docker inspect --format '{{.State.ExitCode}}' $containerName
Write-Host "WP02_CONTAINER_EXIT_CODE=$exitCode"
docker rm $containerName
Write-Host "WP02_DOCKER_REMOVE_EXIT_CODE=$LASTEXITCODE"

$containerAfter = docker ps --all --quiet --filter "name=^/$containerName$"
Write-Host "WP02_CONTAINER_PRESENT_AFTER_CLEANUP=$(-not [string]::IsNullOrWhiteSpace($containerAfter))"
docker image inspect $imageTag *> $null
Write-Host "WP02_IMAGE_RETAINED_FOR_FAILURE_BATCH=$($LASTEXITCODE -eq 0)"
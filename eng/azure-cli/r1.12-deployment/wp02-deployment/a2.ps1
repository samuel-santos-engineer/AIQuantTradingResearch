$containerName = 'aiq-release-112-wp02-d6199dd0478343b7ad81a414143704d5'

Write-Host '=== CONTAINER STATE ==='
docker inspect --format '{{json .State}}' $containerName
Write-Host "WP02_DIAG_INSPECT_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== PROCESS TOPOLOGY ==='
docker top $containerName -eo pid,ppid,stat,comm,args
Write-Host "WP02_DIAG_TOP_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== COMPLETE CONTAINER LOGS ==='
docker logs $containerName
Write-Host "WP02_DIAG_LOGS_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== IMAGE ENTRYPOINT / ENVIRONMENT ==='
docker image inspect --format '{{json .Config}}' 'aiq-release-112-wp02:d6199dd0478343b7ad81a414143704d5'
Write-Host "WP02_DIAG_IMAGE_INSPECT_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== PORT PUBLISHING ==='
docker port $containerName
Write-Host "WP02_DIAG_PORT_EXIT_CODE=$LASTEXITCODE"
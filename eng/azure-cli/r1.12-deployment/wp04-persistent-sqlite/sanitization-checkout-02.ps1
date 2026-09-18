Set-Location C:\projects\github\AIQuantTradingResearch
$ErrorActionPreference = 'Continue'

Write-Host '=== DOCKER RESIDUE ==='
docker ps -a --filter 'name=aiq-r112-wp04' --format '{{.ID}} {{.Names}} {{.Status}}'
Write-Host "WP04_DIAGNOSTIC_CONTAINER_LIST_EXIT_CODE=$LASTEXITCODE"

docker volume ls --filter 'name=aiq-r112-wp04' --format '{{.Name}}'
Write-Host "WP04_DIAGNOSTIC_VOLUME_LIST_EXIT_CODE=$LASTEXITCODE"

docker image ls 'aiq-r112-wp04' --format '{{.Repository}}:{{.Tag}} {{.ID}}'
Write-Host "WP04_DIAGNOSTIC_IMAGE_LIST_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== RETAINED CONTAINER LOGS ==='
$containers = @(docker ps -a --filter 'name=aiq-r112-wp04' --format '{{.Names}}')
foreach ($container in $containers) {
    Write-Host "WP04_DIAGNOSTIC_CONTAINER=$container"
    docker inspect $container --format '{{json .State}}'
    Write-Host "WP04_DIAGNOSTIC_INSPECT_EXIT_CODE[$container]=$LASTEXITCODE"
    docker logs $container 2>&1
    Write-Host "WP04_DIAGNOSTIC_LOGS_EXIT_CODE[$container]=$LASTEXITCODE"
}
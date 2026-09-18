Set-Location C:\projects\github\AIQuantTradingResearch
$ErrorActionPreference = 'Continue'

$image = 'aiq-r112-wp04:82cfad99e7024f07a84a287302bf4f11'
$volume = 'aiq-r112-wp04-82cfad99e7024f07a84a287302bf4f11'
$container = "aiq-r112-wp04-diagnostic-$([guid]::NewGuid().ToString('N'))"

Write-Host "WP04_DIAGNOSTIC_CONTAINER=$container"

docker run --detach --name $container `
  --mount "type=volume,source=$volume,target=/home/data,volume-nocopy" `
  --env 'Worker__Mode=PersistentSqliteQualification' `
  --env 'PersistentSqliteQualification__Phase=initialize' `
  --env 'Persistence__DatabasePath=/home/data/aiquant.db' `
  --env 'Persistence__CreateParentDirectoryForInitialization=true' `
  $image

Write-Host "WP04_DIAGNOSTIC_RUN_EXIT_CODE=$LASTEXITCODE"

Start-Sleep -Seconds 5

Write-Host '=== CONTAINER STATE ==='
docker inspect $container --format '{{json .State}}'
Write-Host "WP04_DIAGNOSTIC_INSPECT_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== CONTAINER LOGS ==='
docker logs $container 2>&1
Write-Host "WP04_DIAGNOSTIC_LOGS_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== STOP DIAGNOSTIC CONTAINER ==='
docker stop $container
Write-Host "WP04_DIAGNOSTIC_STOP_EXIT_CODE=$LASTEXITCODE"

docker rm $container
Write-Host "WP04_DIAGNOSTIC_REMOVE_EXIT_CODE=$LASTEXITCODE"
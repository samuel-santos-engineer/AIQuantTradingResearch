Set-Location C:\projects\github\AIQuantTradingResearch
$ErrorActionPreference = 'Continue'

$image = 'aiq-r112-wp04:82cfad99e7024f07a84a287302bf4f11'
$volume = 'aiq-r112-wp04-82cfad99e7024f07a84a287302bf4f11'
$container = "aiq-r112-wp04-permission-check-$([guid]::NewGuid().ToString('N'))"

Write-Host "WP04_PERMISSION_CONTAINER=$container"

docker run --name $container `
  --mount "type=volume,source=$volume,target=/home/data,volume-nocopy" `
  --entrypoint /bin/sh `
  $image `
  -c 'id; printf "HOME_MODE="; stat -c "%A %u:%g" /home; printf "DATA_MODE="; stat -c "%A %u:%g" /home/data; test -w /home/data; printf "DATA_WRITABLE=$?`n"'

Write-Host "WP04_PERMISSION_RUN_EXIT_CODE=$LASTEXITCODE"

docker rm $container
Write-Host "WP04_PERMISSION_REMOVE_EXIT_CODE=$LASTEXITCODE"

docker volume inspect $volume --format '{{json .}}'
Write-Host "WP04_PERMISSION_VOLUME_INSPECT_EXIT_CODE=$LASTEXITCODE"
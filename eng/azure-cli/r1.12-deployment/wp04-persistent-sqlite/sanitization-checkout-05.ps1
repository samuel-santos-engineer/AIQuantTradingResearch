Set-Location C:\projects\github\AIQuantTradingResearch
$ErrorActionPreference = 'Continue'

$image = 'aiq-r112-wp04:82cfad99e7024f07a84a287302bf4f11'
$volume = 'aiq-r112-wp04-82cfad99e7024f07a84a287302bf4f11'
$container = "aiq-r112-wp04-permission-check-$([guid]::NewGuid().ToString('N'))"

docker run --name $container `
  --mount "type=volume,source=$volume,target=/home/data,volume-nocopy" `
  --entrypoint /bin/sh `
  $image `
  -c 'id; echo === DIRECTORIES ===; ls -ld / /home /home/data; echo === WRITABLE ===; if [ -w /home/data ]; then echo DATA_WRITABLE=True; else echo DATA_WRITABLE=False; fi'

Write-Host "WP04_PERMISSION_RUN_EXIT_CODE=$LASTEXITCODE"

docker rm $container
Write-Host "WP04_PERMISSION_REMOVE_EXIT_CODE=$LASTEXITCODE"
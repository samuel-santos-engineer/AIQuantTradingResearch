$ErrorActionPreference = 'Continue'

Write-Host '=== WP02 LOCAL IMAGE INVENTORY ==='
& docker image ls --format '{{.Repository}}:{{.Tag}} {{.ID}}' | Select-String 'aiq-release-112-wp02'
Write-Host "WP02_IMAGE_LIST_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== WP02 CONTAINER INVENTORY ==='
& docker ps --all --format '{{.Names}} {{.Image}} {{.Status}}' | Select-String 'aiq-release-112-wp02'
Write-Host "WP02_CONTAINER_LIST_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== PORT 18501 LISTENER ==='
$listener = Get-NetTCPConnection -LocalPort 18501 -State Listen -ErrorAction SilentlyContinue
Write-Host "WP02_PORT_18501_LISTENER_PRESENT=$([bool]$listener)"
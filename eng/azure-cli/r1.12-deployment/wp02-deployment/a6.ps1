$ErrorActionPreference = 'Stop'
$imageTag = 'aiq-release-112-wp02:0d334987effa480f8176abff08f8d7a5'
$syntheticValue = 'container-validation-placeholder'

& docker image inspect $imageTag *> $null
Write-Host "WP02_SECURITY_IMAGE_PRECHECK_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== IMAGE HISTORY: NON-PRINTING SYNTHETIC-SECRET SCAN ==='
$history = & docker history --no-trunc $imageTag 2>&1
$historyExit = $LASTEXITCODE
$historyText = $history | Out-String
Write-Host "WP02_SECURITY_HISTORY_EXIT_CODE=$historyExit"
Write-Host "WP02_SECURITY_HISTORY_CONTAINS_SYNTHETIC_VALUE=$($historyText.Contains($syntheticValue))"
Write-Host "WP02_SECURITY_HISTORY_CONTAINS_API_KEY_NAME=$($historyText.Contains('TwelveData__ApiKey'))"

Write-Host '=== IMAGE CONFIGURATION: NON-PRINTING SYNTHETIC-SECRET SCAN ==='
$imageInspect = & docker image inspect $imageTag 2>&1
$imageInspectExit = $LASTEXITCODE
$imageInspectText = $imageInspect | Out-String
Write-Host "WP02_SECURITY_IMAGE_INSPECT_EXIT_CODE=$imageInspectExit"
Write-Host "WP02_SECURITY_IMAGE_CONFIG_CONTAINS_SYNTHETIC_VALUE=$($imageInspectText.Contains($syntheticValue))"
Write-Host "WP02_SECURITY_IMAGE_CONFIG_CONTAINS_API_KEY_NAME=$($imageInspectText.Contains('TwelveData__ApiKey'))"

Write-Host '=== OWNED CONTAINER / LISTENER RESIDUE ==='
$ownedContainers = & docker ps --all --filter 'name=aiq-release-112-wp02-' --format '{{.Names}}'
$ownedContainersExit = $LASTEXITCODE
$ownedContainerCount = @($ownedContainers | Where-Object { $_ -and $_.Trim() }).Count
Write-Host "WP02_SECURITY_OWNED_CONTAINER_LIST_EXIT_CODE=$ownedContainersExit"
Write-Host "WP02_SECURITY_OWNED_CONTAINER_COUNT=$ownedContainerCount"

$listener = Get-NetTCPConnection -LocalPort 18501 -State Listen -ErrorAction SilentlyContinue
Write-Host "WP02_SECURITY_PORT_18501_LISTENER_PRESENT=$([bool]$listener)"

Write-Host '=== REMOVE ONLY THE WP02 LOCAL IMAGE ==='
& docker image rm $imageTag
Write-Host "WP02_SECURITY_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"

& docker image inspect $imageTag *> $null
$imagePresent = $LASTEXITCODE -eq 0
Write-Host "WP02_SECURITY_IMAGE_PRESENT_AFTER_CLEANUP=$imagePresent"
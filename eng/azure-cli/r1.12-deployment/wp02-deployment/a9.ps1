$ErrorActionPreference = 'Stop'

$imageTags = @(
  'aiq-release-112-wp02:3bb3532ad1b94a74a08db8cc3593332d',
  'aiq-release-112-wp02:6fe0ffdfd7a1412ca6bc94ec9f9f9a6b',
  'aiq-release-112-wp02:d6199dd0478343b7ad81a414143704d5'
)

foreach ($imageTag in $imageTags) {
    & docker image rm $imageTag
    Write-Host "WP02_LOCAL_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"
}

$remaining = @(
    & docker image ls --format '{{.Repository}}:{{.Tag}}' |
    Where-Object { $_ -like 'aiq-release-112-wp02:*' }
).Count

$ownedContainers = @(
    & docker ps --all --filter 'name=aiq-release-112-wp02-' --format '{{.Names}}' |
    Where-Object { $_ -and $_.Trim() }
).Count

$listener = Get-NetTCPConnection -LocalPort 18501 -State Listen -ErrorAction SilentlyContinue

Write-Host "WP02_LOCAL_IMAGES_REMAINING=$remaining"
Write-Host "WP02_OWNED_CONTAINERS_REMAINING=$ownedContainers"
Write-Host "WP02_PORT_18501_LISTENER_PRESENT=$([bool]$listener)"
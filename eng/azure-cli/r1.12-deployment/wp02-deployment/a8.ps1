$ErrorActionPreference = 'Stop'

$imageTags = @(
  'aiq-release-112-wp02:3bb3532ad1b94a74a08db8cc3593332d',
  'aiq-release-112-wp02:6fe0ffdfd7a1412ca6bc94ec9f9f9a6b',
  'aiq-release-112-wp02:d6199dd0478343b7ad81a414143704d5'
)

$syntheticValue = 'container-validation-placeholder'

foreach ($imageTag in $imageTags) {
    Write-Host "=== $imageTag ==="

    $historyText = (& docker history --no-trunc $imageTag 2>&1 | Out-String)
    $historyExit = $LASTEXITCODE
    Write-Host "WP02_SECURITY_HISTORY_EXIT_CODE=$historyExit"
    Write-Host "WP02_SECURITY_HISTORY_CONTAINS_SYNTHETIC_VALUE=$($historyText.Contains($syntheticValue))"
    Write-Host "WP02_SECURITY_HISTORY_CONTAINS_API_KEY_NAME=$($historyText.Contains('TwelveData__ApiKey'))"

    $inspectText = (& docker image inspect $imageTag 2>&1 | Out-String)
    $inspectExit = $LASTEXITCODE
    Write-Host "WP02_SECURITY_IMAGE_INSPECT_EXIT_CODE=$inspectExit"
    Write-Host "WP02_SECURITY_CONFIG_CONTAINS_SYNTHETIC_VALUE=$($inspectText.Contains($syntheticValue))"
    Write-Host "WP02_SECURITY_CONFIG_CONTAINS_API_KEY_NAME=$($inspectText.Contains('TwelveData__ApiKey'))"
}
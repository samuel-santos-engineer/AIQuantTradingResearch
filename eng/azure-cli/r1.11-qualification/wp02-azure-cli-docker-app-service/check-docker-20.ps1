$ErrorActionPreference = 'Continue'

$baseUrl = 'https://aiqwp02wcusfd81a595e4e2.azurewebsites.net'
$marker = 'wp02-wcus-marker-20260830-r1'

Write-Host "MARKER_VALUE=$marker"

Write-Host '=== WRITE MARKER ==='
try {
    $write = Invoke-WebRequest `
      -Uri "$baseUrl/marker?value=$marker" `
      -Method Post `
      -UseBasicParsing `
      -TimeoutSec 120

    Write-Host "MARKER_WRITE_STATUS_CODE=$($write.StatusCode)"
    Write-Host "MARKER_WRITE_BODY=$($write.Content)"
} catch {
    Write-Host "MARKER_WRITE_ERROR=$($_.Exception.Message)"
}
Write-Host "MARKER_WRITE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== READ MARKER ==='
try {
    $read = Invoke-WebRequest `
      -Uri "$baseUrl/state" `
      -UseBasicParsing `
      -TimeoutSec 120

    Write-Host "MARKER_READ_STATUS_CODE=$($read.StatusCode)"
    Write-Host "MARKER_READ_BODY=$($read.Content)"
} catch {
    Write-Host "MARKER_READ_ERROR=$($_.Exception.Message)"
}
Write-Host "MARKER_READ_EXIT_CODE=$LASTEXITCODE"
$app = 'aiqwp03wcus3d25217bd701'
$baseUrl = "https://$app.azurewebsites.net"

Write-Host '=== R4 HEALTHZ ==='
curl.exe --silent --show-error --fail --max-time 30 "$baseUrl/healthz"
Write-Host "WP03_R4_HEALTHZ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== R4 PERSISTED SQLITE STATE ==='
curl.exe --silent --show-error --fail --max-time 30 "$baseUrl/state"
Write-Host "WP03_R4_STATE_EXIT_CODE=$LASTEXITCODE"
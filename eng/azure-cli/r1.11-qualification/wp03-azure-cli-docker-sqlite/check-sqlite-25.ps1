$rg = 'rg-aiq-wp03-wcus-3d25217bd701'

Write-Host '=== PRE-DELETE IDENTITY ==='
az group show --name $rg --query '{name:name,location:location}' --output json
Write-Host "WP03_CLEANUP_PREDELETE_READ_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Expected WP03 resource group is not present.' }

Write-Host '=== DELETE ONLY WP03 RESOURCE GROUP ==='
az group delete --name $rg --yes --no-wait
Write-Host "WP03_CLEANUP_DELETE_REQUEST_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'WP03 resource-group deletion request failed.' }

Write-Host '=== WAIT FOR DELETION ==='
$deleted = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
    Start-Sleep -Seconds 10
    $exists = az group exists --name $rg --output tsv
    $exitCode = $LASTEXITCODE
    Write-Host "WP03_CLEANUP_DELETE_POLL_ATTEMPT=$attempt"
    Write-Host "WP03_CLEANUP_DELETE_POLL_EXISTS=$exists"
    Write-Host "WP03_CLEANUP_DELETE_POLL_EXIT_CODE=$exitCode"
    if ($exitCode -eq 0 -and $exists -eq 'false') {
        $deleted = $true
        break
    }
}
Write-Host "WP03_CLEANUP_RESOURCE_GROUP_DELETED=$deleted"
if (-not $deleted) { throw 'WP03 resource group was not deleted within the bounded wait.' }

Write-Host '=== CLEANUP READ-BACK ==='
$existsAfter = az group exists --name $rg --output tsv
Write-Host "WP03_CLEANUP_RESOURCE_GROUP_EXISTS_AFTER=$existsAfter"
Write-Host "WP03_CLEANUP_READBACK_EXIT_CODE=$LASTEXITCODE"
if ($existsAfter -ne 'false') { throw 'WP03 resource group remains after cleanup.' }
$rg = 'rg-aiq-wp04-wcus-c1e9a49dadf6'

Write-Host '=== CONFIRM OWNED GROUP BEFORE DELETE ==='
az group show --name $rg --query '{name:name,location:location,tags:tags}' --output json
Write-Host "WP04_CLEANUP_PREDELETE_READ_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'Expected owned WP04 group is absent.' }

Write-Host '=== DELETE ONLY WP04 RESOURCE GROUP ==='
az group delete --name $rg --yes --no-wait
Write-Host "WP04_CLEANUP_DELETE_REQUEST_EXIT_CODE=$LASTEXITCODE"
if ($LASTEXITCODE -ne 0) { throw 'WP04 resource-group deletion request failed.' }

Write-Host '=== WAIT FOR DELETION ==='
$deleted = $false
for ($attempt = 1; $attempt -le 30; $attempt++) {
    Start-Sleep -Seconds 10
    $exists = az group exists --name $rg --output tsv
    $exitCode = $LASTEXITCODE
    Write-Host "WP04_CLEANUP_DELETE_POLL_ATTEMPT=$attempt"
    Write-Host "WP04_CLEANUP_DELETE_POLL_EXISTS=$exists"
    Write-Host "WP04_CLEANUP_DELETE_POLL_EXIT_CODE=$exitCode"
    if ($exitCode -eq 0 -and $exists -eq 'false') {
        $deleted = $true
        break
    }
}
Write-Host "WP04_CLEANUP_RESOURCE_GROUP_DELETED=$deleted"
if (-not $deleted) { throw 'WP04 resource group was not deleted within the bounded wait.' }

$existsAfter = az group exists --name $rg --output tsv
Write-Host "WP04_CLEANUP_RESOURCE_GROUP_EXISTS_AFTER=$existsAfter"
Write-Host "WP04_CLEANUP_READBACK_EXIT_CODE=$LASTEXITCODE"
if ($existsAfter -ne 'false') { throw 'WP04 resource group remains after cleanup.' }
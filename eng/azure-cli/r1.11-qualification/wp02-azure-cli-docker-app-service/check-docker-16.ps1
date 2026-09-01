Write-Host '=== WEST CENTRAL US APP SERVICE USAGES ==='

$subscriptionId = az account show --query id --output tsv
Write-Host "SUBSCRIPTION_LOOKUP_EXIT=$LASTEXITCODE"

az rest --method get `
  --url "https://management.azure.com/subscriptions/$subscriptionId/providers/Microsoft.Web/locations/westcentralus/usages?api-version=2025-05-01" `
  --output json

Write-Host "WEB_USAGE_RAW_EXIT=$LASTEXITCODE"
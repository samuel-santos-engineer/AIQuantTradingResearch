Set-Location C:\projects\github\AIQuantTradingResearch
$ErrorActionPreference = 'Continue'

dotnet build .\src\AIQuantTradingResearch.Worker\AIQuantTradingResearch.Worker.csproj --no-restore
$buildExit = $LASTEXITCODE
Write-Host "WP04_SIGNING_BUILD_EXIT_CODE=$buildExit"

if ($buildExit -eq 0) {
    $workerDll = '.\src\AIQuantTradingResearch.Worker\bin\Debug\net10.0\AIQuantTradingResearch.Worker.dll'
    $signature = Get-AuthenticodeSignature -FilePath $workerDll
    Write-Host "WP04_SIGNING_SIGNATURE_STATUS=$($signature.Status)"
    Write-Host "WP04_SIGNING_SIGNATURE_SUBJECT=$($signature.SignerCertificate.Subject)"
    Write-Host "WP04_SIGNING_SIGNATURE_THUMBPRINT=$($signature.SignerCertificate.Thumbprint)"
}

git status --short
Write-Host "WP04_SIGNING_BUILD_GATE_COMPLETE=True"
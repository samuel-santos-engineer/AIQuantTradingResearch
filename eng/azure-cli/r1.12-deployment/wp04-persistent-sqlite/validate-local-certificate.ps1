Set-Location C:\projects\github\AIQuantTradingResearch

$configured = ([xml](Get-Content -Raw -LiteralPath '.\Directory.Build.local.props')).
    Project.PropertyGroup.LocalDevSigningCertificateThumbprint

$certificates = Get-ChildItem Cert:\CurrentUser\My |
    Where-Object { $_.Subject -eq 'CN=AIQuantTradingDev' } |
    ForEach-Object {
        [pscustomobject]@{
            Subject = $_.Subject
            Thumbprint = $_.Thumbprint
            NotBefore = $_.NotBefore
            NotAfter = $_.NotAfter
            HasPrivateKey = $_.HasPrivateKey
            CodeSigningEku = [bool]($_.EnhancedKeyUsageList |
                Where-Object ObjectId -eq '1.3.6.1.5.5.7.3.3')
            ConfiguredInLocalProps = $_.Thumbprint -eq $configured
        }
    }

$certificates | Format-Table -AutoSize
Write-Host "WP04_SIGNING_CONFIGURED_CERT_PRESENT=$([bool]($certificates | Where-Object ConfiguredInLocalProps))"
Write-Host "WP04_SIGNING_USABLE_CERT_COUNT=$(@($certificates | Where-Object { $_.HasPrivateKey -and $_.CodeSigningEku -and $_.NotAfter -gt (Get-Date) }).Count)"
Write-Host "WP04_SIGNING_CERT_READBACK_EXIT_CODE=0"
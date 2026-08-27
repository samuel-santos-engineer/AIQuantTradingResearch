# 1. Create the self-signed code-signing certificate
$cert = New-SelfSignedCertificate -Type CodeSigningCert -Subject "CN=AIQuantTradingDev" -CertStoreLocation "Cert:\CurrentUser\My"

# 2. Export it to a temporary file
Export-Certificate -Cert $cert -FilePath "$env:TEMP\AIQuantTradingDev.cer"

# 3. Import it into your Trusted Root Certification Authorities to make Windows trust it
Import-Certificate -FilePath "$env:TEMP\AIQuantTradingDev.cer" -CertStoreLocation "Cert:\CurrentUser\Root"

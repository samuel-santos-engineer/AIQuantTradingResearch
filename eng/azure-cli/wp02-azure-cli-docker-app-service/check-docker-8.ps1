$ErrorActionPreference = 'Continue'

$packageName = 'aiq-azure-f1-wp02-probe'
$imageReference = 'ghcr.io/samuel-santos-engineer/aiq-azure-f1-wp02-probe:wp02-r1-93e7a0e5d0954256bf6b4740fc5af238'
$tempRoot = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$dockerProbeConfig = Join-Path $tempRoot ("aiq-ghcr-public-" + [guid]::NewGuid().ToString('N'))
$tokenPointer = [IntPtr]::Zero

try {
    $secureToken = Read-Host 'GitHub PAT with packages access (input is hidden)' -AsSecureString
    $tokenPointer = [Runtime.InteropServices.Marshal]::SecureStringToBSTR($secureToken)
    $env:GH_TOKEN = [Runtime.InteropServices.Marshal]::PtrToStringBSTR($tokenPointer)

    $visibilityBefore = (& gh api "/user/packages/container/$packageName" --jq '.visibility')
    Write-Host "GHCR_PACKAGE_VISIBILITY_BEFORE=$visibilityBefore"
    Write-Host "GHCR_PACKAGE_READ_EXIT_CODE=$LASTEXITCODE"
    if ($LASTEXITCODE -ne 0) { throw 'Unable to read GHCR package metadata.' }

    if ($visibilityBefore -ne 'public') {
        & gh api --method PATCH "/user/packages/container/$packageName" -f visibility=public
        Write-Host "GHCR_PACKAGE_VISIBILITY_UPDATE_EXIT_CODE=$LASTEXITCODE"
        if ($LASTEXITCODE -ne 0) { throw 'Unable to set GHCR package visibility to public.' }
    }
    else {
        Write-Host "GHCR_PACKAGE_VISIBILITY_UPDATE_EXIT_CODE=NOT_APPLICABLE_ALREADY_PUBLIC"
    }

    $visibilityAfter = (& gh api "/user/packages/container/$packageName" --jq '.visibility')
    Write-Host "GHCR_PACKAGE_VISIBILITY_AFTER=$visibilityAfter"
    Write-Host "GHCR_PACKAGE_READBACK_EXIT_CODE=$LASTEXITCODE"

    New-Item -ItemType Directory -Path $dockerProbeConfig | Out-Null
    & docker --config $dockerProbeConfig manifest inspect $imageReference *> $null
    Write-Host "GHCR_ANONYMOUS_MANIFEST_INSPECT_EXIT_CODE=$LASTEXITCODE"
}
catch {
    Write-Host "GHCR_PUBLIC_VISIBILITY_ERROR=$($_.Exception.Message)"
}
finally {
    Remove-Item Env:GH_TOKEN -ErrorAction SilentlyContinue
    if ($tokenPointer -ne [IntPtr]::Zero) {
        [Runtime.InteropServices.Marshal]::ZeroFreeBSTR($tokenPointer)
    }
    Remove-Item -LiteralPath $dockerProbeConfig -Recurse -Force -ErrorAction SilentlyContinue
    Write-Host "GHCR_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $dockerProbeConfig)"
}
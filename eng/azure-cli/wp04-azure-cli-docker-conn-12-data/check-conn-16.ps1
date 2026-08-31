$image = 'aiq-wp04-twelve-probe:wp04-r1-285950909c3b43b8be1b643f73b28b11'
$tempRoot = 'C:\Users\sabsf\AppData\Local\Temp\aiq-wp04-285950909c3b43b8be1b643f73b28b11'

$imageRemoved = 0
docker image inspect $image *> $null
if ($LASTEXITCODE -eq 0) {
    docker image rm --force $image
    if ($LASTEXITCODE -ne 0) { throw 'Failed to remove local WP04 probe image.' }
    $imageRemoved = 1
}
Write-Host "WP04_LOCAL_IMAGE_REMOVALS=$imageRemoved"

$tempRemoved = 0
if (Test-Path -LiteralPath $tempRoot) {
    $resolved = [System.IO.Path]::GetFullPath($tempRoot)
    if (-not $resolved.StartsWith('C:\Users\sabsf\AppData\Local\Temp\aiq-wp04-', [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Refusing unsafe temporary path: $resolved"
    }
    Remove-Item -LiteralPath $resolved -Recurse -Force
    $tempRemoved = 1
}
Write-Host "WP04_TEMP_SOURCE_REMOVALS=$tempRemoved"

docker image inspect $image *> $null
Write-Host "WP04_LOCAL_IMAGE_PRESENT_AFTER_CLEANUP=$($LASTEXITCODE -eq 0)"
Write-Host "WP04_TEMP_SOURCE_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $tempRoot)"
if ($LASTEXITCODE -eq 0 -or (Test-Path -LiteralPath $tempRoot)) {
    throw 'WP04 local cleanup is incomplete.'
}
Write-Host 'WP04_LOCAL_CLEANUP_EXIT_CODE=0'
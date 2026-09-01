$images = @(
  'aiq-wp03-sqlite-probe:wp03-r1-86df07f8b09844ddbb8e0501df8a358a',
  'aiq-wp03-sqlite-probe:wp03-r2-3f99757f018b46698e6c89d589867a03',
  'aiq-wp03-sqlite-probe:wp03-r3-fc006171dbf241b78775e7dd6d5e8a73',
  'aiq-wp03-sqlite-probe:wp03-r4-aa549fe26e4b464db2111df3fc0cf793',
  'aiq-wp03-sqlite-probe:wp03-r4-e361ba0a4ab546f3bdcf69f033dc419f'
)

$tempRoots = @(
  'C:\Users\sabsf\AppData\Local\Temp\aiq-wp03-86df07f8b09844ddbb8e0501df8a358a',
  'C:\Users\sabsf\AppData\Local\Temp\aiq-wp03-r2-3f99757f018b46698e6c89d589867a03',
  'C:\Users\sabsf\AppData\Local\Temp\aiq-wp03-r3-fc006171dbf241b78775e7dd6d5e8a73',
  'C:\Users\sabsf\AppData\Local\Temp\aiq-wp03-r4-aa549fe26e4b464db2111df3fc0cf793',
  'C:\Users\sabsf\AppData\Local\Temp\aiq-wp03-r4-e361ba0a4ab546f3bdcf69f033dc419f'
)

$imageRemovals = 0
foreach ($image in $images) {
    docker image inspect $image *> $null
    if ($LASTEXITCODE -eq 0) {
        docker image rm --force $image
        if ($LASTEXITCODE -ne 0) { throw "Failed to remove WP03 local image: $image" }
        $imageRemovals++
    }
}
Write-Host "WP03_LOCAL_IMAGE_REMOVALS=$imageRemovals"

$tempRemovals = 0
foreach ($path in $tempRoots) {
    if (Test-Path -LiteralPath $path) {
        $resolved = [System.IO.Path]::GetFullPath($path)
        if (-not $resolved.StartsWith('C:\Users\sabsf\AppData\Local\Temp\aiq-wp03-', [System.StringComparison]::OrdinalIgnoreCase)) {
            throw "Refusing unsafe temporary path: $resolved"
        }
        Remove-Item -LiteralPath $resolved -Recurse -Force
        $tempRemovals++
    }
}
Write-Host "WP03_TEMP_SOURCE_REMOVALS=$tempRemovals"

$imagesRemaining = 0
foreach ($image in $images) {
    docker image inspect $image *> $null
    if ($LASTEXITCODE -eq 0) { $imagesRemaining++ }
}
Write-Host "WP03_LOCAL_IMAGES_REMAINING=$imagesRemaining"

$tempRemaining = 0
foreach ($path in $tempRoots) {
    if (Test-Path -LiteralPath $path) { $tempRemaining++ }
}
Write-Host "WP03_TEMP_SOURCES_REMAINING=$tempRemaining"

if ($imagesRemaining -ne 0 -or $tempRemaining -ne 0) {
    throw 'WP03 local cleanup is incomplete.'
}
Write-Host 'WP03_LOCAL_CLEANUP_EXIT_CODE=0'
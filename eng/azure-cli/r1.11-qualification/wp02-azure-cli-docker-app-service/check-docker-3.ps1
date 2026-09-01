$ErrorActionPreference = 'Continue'

$runId = [guid]::NewGuid().ToString('N')
$tempRoot = [System.IO.Path]::GetFullPath([System.IO.Path]::GetTempPath())
$probeRoot = Join-Path $tempRoot "aiq-wp02-docker-$runId"
$imageTag = "aiq-wp02-tooling-probe:$runId"
$baseImage = 'alpine:3.21'
$scriptExit = 0

try {
    $baseImageId = (& docker image ls --quiet $baseImage)
    $baseWasPresent = -not [string]::IsNullOrWhiteSpace($baseImageId)
    Write-Host "DOCKER_BASE_IMAGE_PREEXISTING=$baseWasPresent"

    New-Item -ItemType Directory -Path $probeRoot -ErrorAction Stop | Out-Null

    @'
FROM alpine:3.21
CMD ["sh", "-c", "printf 'AIQ_WP02_LINUX_CONTAINER_OK\n'"]
'@ | Set-Content -LiteralPath (Join-Path $probeRoot 'Dockerfile') -Encoding ascii -ErrorAction Stop

    & docker build --tag $imageTag $probeRoot
    $buildExit = $LASTEXITCODE
    Write-Host "DOCKER_BUILD_EXIT_CODE=$buildExit"
    if ($buildExit -ne 0) { throw 'Docker build failed.' }

    & docker run --rm $imageTag
    $runExit = $LASTEXITCODE
    Write-Host "DOCKER_RUN_EXIT_CODE=$runExit"
    if ($runExit -ne 0) { throw 'Docker run failed.' }
}
catch {
    $scriptExit = 1
    Write-Host "PROBE_ERROR=$($_.Exception.Message)"
}
finally {
    $temporaryImageId = (& docker image ls --quiet $imageTag)
    if (-not [string]::IsNullOrWhiteSpace($temporaryImageId)) {
        & docker image rm --force $imageTag
        Write-Host "DOCKER_TEMP_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"
    }
    else {
        Write-Host "DOCKER_TEMP_IMAGE_REMOVE_EXIT_CODE=NOT_APPLICABLE"
    }

    if (-not $baseWasPresent) {
        $newBaseImageId = (& docker image ls --quiet $baseImage)
        if (-not [string]::IsNullOrWhiteSpace($newBaseImageId)) {
            & docker image rm --force $baseImage
            Write-Host "DOCKER_NEW_BASE_IMAGE_REMOVE_EXIT_CODE=$LASTEXITCODE"
        }
        else {
            Write-Host "DOCKER_NEW_BASE_IMAGE_REMOVE_EXIT_CODE=NOT_APPLICABLE"
        }
    }
    else {
        Write-Host "DOCKER_NEW_BASE_IMAGE_REMOVE_EXIT_CODE=NOT_APPLICABLE_PREEXISTING"
    }

    Remove-Item -LiteralPath $probeRoot -Recurse -Force -ErrorAction SilentlyContinue

    $imageAfterCleanup = (& docker image ls --quiet $imageTag)
    $imageStillPresent = -not [string]::IsNullOrWhiteSpace($imageAfterCleanup)
    $directoryStillPresent = Test-Path -LiteralPath $probeRoot

    Write-Host "DOCKER_TEMP_IMAGE_PRESENT_AFTER_CLEANUP=$imageStillPresent"
    Write-Host "TEMP_DIRECTORY_PRESENT_AFTER_CLEANUP=$directoryStillPresent"
    Write-Host "SCRIPT_EXIT_CODE=$scriptExit"
}
[CmdletBinding()]
param(
    [Parameter(Mandatory)] [ValidatePattern('^[a-z0-9][a-z0-9._-]*$')] [string] $GitHubOwner,
    [Parameter(Mandatory)] [ValidatePattern('^[a-z0-9][a-z0-9._-]*$')] [string] $ImageName,
    [Parameter(Mandatory)] [ValidatePattern('^[A-Za-z0-9][A-Za-z0-9._-]*$')] [string] $Tag,
    [switch] $InteractiveLogin
)

$ErrorActionPreference = 'Stop'
$image = "ghcr.io/$GitHubOwner/$ImageName`:$Tag"

function Write-ExitCode([string] $Name) { Write-Host "$Name=$LASTEXITCODE" }

if ($InteractiveLogin) {
    Write-Host '=== INTERACTIVE GHCR LOGIN ==='
    Write-Host 'Docker will prompt locally. Do not provide credentials to this script or chat.'
    & docker login ghcr.io
    Write-ExitCode 'WP03_GHCR_LOGIN_EXIT_CODE'
    if ($LASTEXITCODE -ne 0) { throw 'GHCR login failed.' }
}

Write-Host '=== BUILD RELEASE IMAGE ==='
& docker build --pull --tag $image .
Write-ExitCode 'WP03_GHCR_BUILD_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'Docker build failed.' }

Write-Host '=== PUSH GHCR IMAGE ==='
& docker push $image
Write-ExitCode 'WP03_GHCR_PUSH_EXIT_CODE'
if ($LASTEXITCODE -ne 0) { throw 'GHCR push failed.' }

Write-Host '=== READ IMMUTABLE DIGEST ==='
$digest = (& docker buildx imagetools inspect $image --format '{{json .Manifest.Digest}}').Trim('"')
Write-ExitCode 'WP03_GHCR_DIGEST_READ_EXIT_CODE'
if ([string]::IsNullOrWhiteSpace($digest) -or -not $digest.StartsWith('sha256:')) { throw 'Published image digest was not available.' }
$immutableImage = "ghcr.io/$GitHubOwner/$ImageName@$digest"
Write-Host "WP03_GHCR_IMAGE=$image"
Write-Host "WP03_GHCR_IMMUTABLE_IMAGE=$immutableImage"

Write-Host '=== VERIFY ANONYMOUS MANIFEST ==='
$configRoot = Join-Path ([System.IO.Path]::GetTempPath()) ('aiq-wp03-ghcr-' + [guid]::NewGuid().ToString('N'))
try {
    New-Item -ItemType Directory -Path $configRoot | Out-Null
    $previousDockerConfig = $env:DOCKER_CONFIG
    $env:DOCKER_CONFIG = $configRoot
    & docker manifest inspect $immutableImage | Out-Null
    Write-ExitCode 'WP03_GHCR_ANONYMOUS_MANIFEST_EXIT_CODE'
    if ($LASTEXITCODE -ne 0) { throw 'Image is not anonymously available; make the GHCR package public before Azure deployment.' }
}
finally {
    $env:DOCKER_CONFIG = $previousDockerConfig
    if (Test-Path -LiteralPath $configRoot) { Remove-Item -LiteralPath $configRoot -Recurse -Force }
    Write-Host "WP03_GHCR_TEMP_CONFIG_PRESENT_AFTER_CLEANUP=$(Test-Path -LiteralPath $configRoot)"
}

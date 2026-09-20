[CmdletBinding()]
param([string] $ImageTag = ('aiq-wp07-' + [guid]::NewGuid().ToString('N')))

$ErrorActionPreference = 'Stop'
docker build --tag $ImageTag .
if ($LASTEXITCODE -ne 0) { throw 'WP07 container build failed.' }

docker run --rm --entrypoint sh $ImageTag -ec @'
test -f /app/worker/AIQuantTradingResearch.Worker.dll
command -v dotnet
test -x /usr/local/bin/aiq-entrypoint
sh -n /usr/local/bin/aiq-entrypoint
grep -F '/home/aiq-market-cache' /usr/local/bin/aiq-entrypoint
'@
if ($LASTEXITCODE -ne 0) { throw 'WP07 image composition validation failed.' }

Write-Host 'WP07_CONTAINER_BUILD=PASS'
Write-Host 'WP07_WORKER_DLL_PATH=PASS'
Write-Host 'WP07_DOTNET_PATH=PASS'
Write-Host 'WP07_ENTRYPOINT_SYNTAX=PASS'

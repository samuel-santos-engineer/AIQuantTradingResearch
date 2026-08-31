whoami

az version
Write-Host "AZ_EXIT_CODE=$LASTEXITCODE"

docker version
Write-Host "DOCKER_VERSION_EXIT_CODE=$LASTEXITCODE"

docker info
Write-Host "DOCKER_INFO_EXIT_CODE=$LASTEXITCODE"

wsl --status
Write-Host "WSL_STATUS_EXIT_CODE=$LASTEXITCODE"

wsl -l -v
Write-Host "WSL_LIST_EXIT_CODE=$LASTEXITCODE"
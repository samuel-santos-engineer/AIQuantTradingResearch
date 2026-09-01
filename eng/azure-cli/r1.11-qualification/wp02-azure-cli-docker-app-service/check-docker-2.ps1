az version *> $null
Write-Host "AZ_EXIT_CODE=$LASTEXITCODE"

docker version *> $null
Write-Host "DOCKER_VERSION_EXIT_CODE=$LASTEXITCODE"

docker info *> $null
Write-Host "DOCKER_INFO_EXIT_CODE=$LASTEXITCODE"

wsl --status *> $null
Write-Host "WSL_STATUS_EXIT_CODE=$LASTEXITCODE"

wsl -l -v *> $null
Write-Host "WSL_LIST_EXIT_CODE=$LASTEXITCODE"
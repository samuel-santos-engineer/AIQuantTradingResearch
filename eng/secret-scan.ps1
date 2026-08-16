[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repositoryRoot = Split-Path $PSScriptRoot -Parent

function Get-GitleaksCommand {
    Get-Command 'gitleaks' -ErrorAction SilentlyContinue
}

function Refresh-ProcessPath {
    $machinePath = [Environment]::GetEnvironmentVariable(
        'Path',
        [EnvironmentVariableTarget]::Machine
    )

    $userPath = [Environment]::GetEnvironmentVariable(
        'Path',
        [EnvironmentVariableTarget]::User
    )

    $paths = @(
        $machinePath
        $userPath
    ) | Where-Object { -not [string]::IsNullOrWhiteSpace($_) }

    $env:Path = $paths -join ';'
}

function Install-Gitleaks {
    Write-Host 'Gitleaks was not found. Ensuring Gitleaks is installed...'

    $winget = Get-Command 'winget' -ErrorAction SilentlyContinue

    if (-not $winget) {
        throw @'
Gitleaks is required for secret scanning, but neither Gitleaks nor winget was found.

Install Gitleaks manually and ensure the 'gitleaks' command is available on PATH.

Then run:

    ./eng/secret-scan.ps1
'@
    }

    & $winget.Source `
        install `
        --id Gitleaks.Gitleaks `
        --exact `
        --no-upgrade `
        --accept-package-agreements `
        --accept-source-agreements

    $wingetExitCode = $LASTEXITCODE

    Write-Host 'Refreshing process PATH...'
    Refresh-ProcessPath

    $gitleaks = Get-GitleaksCommand

    if ($gitleaks) {
        if ($wingetExitCode -ne 0) {
            Write-Host "WinGet returned exit code $wingetExitCode, but Gitleaks is installed and available. Continuing."
        }

        return $gitleaks
    }

    throw @"
Gitleaks could not be located after the WinGet install/check operation.

WinGet exit code: $wingetExitCode

Open a new PowerShell session and verify:

    gitleaks version

If the command is still unavailable, reinstall Gitleaks manually:

    winget uninstall --id Gitleaks.Gitleaks --exact
    winget install --id Gitleaks.Gitleaks --exact

Then run:

    ./eng/secret-scan.ps1
"@
}

Write-Host '==> Secret scanning'

$gitleaks = Get-GitleaksCommand

if (-not $gitleaks) {
    Write-Host 'Refreshing process PATH...'
    Refresh-ProcessPath
    $gitleaks = Get-GitleaksCommand
}

if (-not $gitleaks) {
    $gitleaks = Install-Gitleaks
}

Write-Host "Using Gitleaks: $($gitleaks.Source)"

Push-Location $repositoryRoot

try {
    & $gitleaks.Source `
        git `
        . `
        --redact `
        --verbose

    if ($LASTEXITCODE -ne 0) {
        throw "Gitleaks secret scan failed with exit code $LASTEXITCODE."
    }
}
finally {
    Pop-Location
}

Write-Host 'Secret scanning completed successfully.' -ForegroundColor Green

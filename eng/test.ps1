[CmdletBinding()]
param(
    [string]$SolutionPath = 'AIQuantTradingResearch.slnx',
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug',
    [switch]$NoBuild
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$repositoryRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$resolvedSolutionPath = if ([System.IO.Path]::IsPathRooted($SolutionPath)) {
    $SolutionPath
}
else {
    Join-Path $repositoryRoot $SolutionPath
}

if (-not (Test-Path -LiteralPath $resolvedSolutionPath -PathType Leaf)) {
    throw "Solution file was not found: $resolvedSolutionPath"
}

$arguments = @(
    'test'
    $resolvedSolutionPath
    '--configuration'
    $Configuration
    '--no-restore'
    '--nologo'
)

if ($NoBuild) {
    $arguments += '--no-build'
}

Write-Host "Testing AIQuantTradingResearch solution ($Configuration)..."
& dotnet @arguments
if ($LASTEXITCODE -ne 0) {
    throw "dotnet test failed with exit code $LASTEXITCODE."
}

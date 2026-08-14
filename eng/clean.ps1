[CmdletBinding()]
param(
    [string]$SolutionPath = 'AIQuantTradingResearch.slnx',
    [ValidateSet('Debug', 'Release')]
    [string]$Configuration = 'Debug'
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

Write-Host "Cleaning AIQuantTradingResearch solution ($Configuration)..."
& dotnet clean $resolvedSolutionPath --configuration $Configuration --nologo
if ($LASTEXITCODE -ne 0) {
    throw "dotnet clean failed with exit code $LASTEXITCODE."
}

[CmdletBinding()]
param(
    [string]$SolutionPath = 'AIQuantTradingResearch.slnx'
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

Write-Host 'Verifying AIQuantTradingResearch formatting...'
& dotnet format $resolvedSolutionPath --verify-no-changes --no-restore --verbosity minimal
if ($LASTEXITCODE -ne 0) {
    throw "dotnet format failed with exit code $LASTEXITCODE."
}

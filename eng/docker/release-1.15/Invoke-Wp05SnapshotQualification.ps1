#requires -Version 5.1
<#
Future, separately authorized local Linux screening only. UTF-8/LF.
Requires cached pinned images, offline linux/amd64 publish, and a disposable test-only
closed, exclusively owned SQLite fixture already provisioned through WP03 with both instruments and D5 tuple.
TLS fixture: server.pfx (test private key), ca.pem; certificate SAN includes wp05-research.
Does not provision, repair, acquire provider data, install packages, pull or publish.
Default is a read-only input preflight. -Execute requires separate Linux-test authority.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][string]$PublishedResearchDirectory,
    [Parameter(Mandatory = $true)][string]$PrivateFixtureDirectory,
    [Parameter(Mandatory = $true)][string]$TlsFixtureDirectory,
    [switch]$Execute
)
Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
foreach ($pair in @(
    @($PublishedResearchDirectory, 'AIQuantTradingResearch.Worker.dll'),
    @($PrivateFixtureDirectory, 'research.sqlite'),
    @($PrivateFixtureDirectory, 'watermark.json'),
    @($TlsFixtureDirectory, 'server.pfx'),
    @($TlsFixtureDirectory, 'ca.pem'))) {
    if (-not (Test-Path -LiteralPath (Join-Path $pair[0] $pair[1]) -PathType Leaf)) {
        throw 'Required offline test fixture unavailable.'
    }
}
if (-not $Execute) {
    Write-Output 'Input preflight PASS; Docker NOT RUN; separate Linux qualification authority required; PUBLIC_RUNTIME_VERIFIED=False'
    return
}
$owner = 'aiq-wp05-' + [Guid]::NewGuid().ToString('N')
$tempRoot = [IO.Path]::GetFullPath([IO.Path]::GetTempPath())
$work = [IO.Path]::GetFullPath((Join-Path $tempRoot $owner))
if (-not $work.StartsWith($tempRoot, [StringComparison]::OrdinalIgnoreCase)) { throw 'Unsafe owned output path.' }
[void](New-Item -ItemType Directory -Path $work)
$sequence = 0
$oldKey = [Environment]::GetEnvironmentVariable('AIQ_RESEARCH_API_KEY', 'Process')
$owned = New-Object 'System.Collections.Generic.List[object]'
function ConvertTo-NativeArgument([string]$Value) {
    $builder = New-Object Text.StringBuilder
    [void]$builder.Append('"')
    $slashes = 0
    foreach ($character in $Value.ToCharArray()) {
        if ($character -eq '\') { $slashes++; continue }
        if ($character -eq '"') {
            [void]$builder.Append(('\' * (2 * $slashes + 1)))
            [void]$builder.Append('"')
        }
        else {
            [void]$builder.Append(('\' * $slashes))
            [void]$builder.Append($character)
        }
        $slashes = 0
    }
    [void]$builder.Append(('\' * (2 * $slashes)))
    [void]$builder.Append('"')
    return $builder.ToString()
}
function Invoke-OwnedDocker([string[]]$Arguments, [int]$Seconds = 60) {
    $script:sequence++
    $stdout = Join-Path $work ($script:sequence.ToString() + '.out')
    $stderr = Join-Path $work ($script:sequence.ToString() + '.err')
    $native = ($Arguments | ForEach-Object { ConvertTo-NativeArgument $_ }) -join ' '
    $process = Start-Process -FilePath 'docker' -ArgumentList $native -WindowStyle Hidden -PassThru -RedirectStandardOutput $stdout -RedirectStandardError $stderr
    try {
        if (-not $process.WaitForExit($Seconds * 1000)) {
            $process.Kill()
            throw 'Owned Docker CLI timed out; daemon resource reconciliation may still be required.'
        }
        $process.WaitForExit()
        if ($process.ExitCode -ne 0) { throw 'Offline Docker operation failed; inspect owned local logs.' }
        return [IO.File]::ReadAllText($stdout).Trim()
    }
    finally { $process.Dispose() }
}
try {
    $endpoint = Invoke-OwnedDocker @('context', 'inspect', '--format', '{{.Endpoints.docker.Host}}')
    if ($endpoint -notmatch '^npipe://') { throw 'Only the local Windows Docker named-pipe context is permitted.' }
    [void](Invoke-OwnedDocker @('info', '--format', '{{.ServerVersion}}'))
    foreach ($base in @(
        'mcr.microsoft.com/dotnet/aspnet:10.0@sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f',
        'python:3.13-slim@sha256:8fb4cfa1a2616d7b8e0c2175cc6ad68f5729c34ea8488c0b360d2934b7be9024')) {
        [void](Invoke-OwnedDocker @('image', 'inspect', '--format', '{{.Id}}', $base))
    }
    foreach ($name in @('research', 'client', 'private', 'tls')) { [void](New-Item -ItemType Directory -Path (Join-Path $work $name)) }
    Copy-Item -LiteralPath (Join-Path $repoRoot 'python\presentation\research_snapshot_client.py') -Destination (Join-Path $work 'client\research_snapshot_client.py')
    foreach ($entry in Get-ChildItem -LiteralPath $PublishedResearchDirectory -Force) {
        Copy-Item -LiteralPath $entry.FullName -Destination (Join-Path $work 'research') -Recurse
    }
    foreach ($entry in Get-ChildItem -LiteralPath $PrivateFixtureDirectory -Force) {
        Copy-Item -LiteralPath $entry.FullName -Destination (Join-Path $work 'private') -Recurse
    }
    foreach ($name in @('server.pfx', 'ca.pem')) { Copy-Item -LiteralPath (Join-Path $TlsFixtureDirectory $name) -Destination (Join-Path $work ('tls\' + $name)) }
    $bytes = New-Object byte[] 32
    $random = New-Object Security.Cryptography.RNGCryptoServiceProvider
    try { $random.GetBytes($bytes) } finally { $random.Dispose() }
    [Environment]::SetEnvironmentVariable('AIQ_RESEARCH_API_KEY', [Convert]::ToBase64String($bytes), 'Process')
    foreach ($role in @('research-api', 'snapshot-client')) {
        $tag = $owner + ':' + $role
        # Build context contains only copied offline code; no private fixture/certificate directory.
        $context = Join-Path $work ('context-' + $role)
        [void](New-Item -ItemType Directory -Path $context)
        $folder = if ($role -eq 'research-api') { 'research' } else { 'client' }
        Copy-Item -LiteralPath (Join-Path $work $folder) -Destination $context -Recurse
        $dockerfile = Join-Path $repoRoot ('container\release_1_15\' + $role + '.Dockerfile')
        [void](Invoke-OwnedDocker @('build', '--platform', 'linux/amd64', '--pull=false', '--network=none', '--label', ('aiq.owner=' + $owner), '-f', $dockerfile, '-t', $tag, $context) 120)
        $owned.Add(@('image', $tag))
    }
    [void](Invoke-OwnedDocker @('network', 'create', '--internal', '--label', ('aiq.owner=' + $owner), $owner))
    $owned.Add(@('network', $owner))
    $researchName = $owner + '-research'
    [void](Invoke-OwnedDocker @('create', '--name', $researchName, '--network', $owner, '--network-alias', 'wp05-research', '--label', ('aiq.owner=' + $owner),
        '--read-only', '--tmpfs', '/tmp:rw,nosuid,noexec,size=32m', '--cap-drop=ALL', '--security-opt=no-new-privileges', '--memory=256m', '--pids-limit=64',
        '--mount', ('type=bind,source=' + (Join-Path $work 'private') + ',target=/private'),
        '--mount', ('type=bind,source=' + (Join-Path $work 'tls') + ',target=/tls,readonly'),
        '--env', 'AIQ_RESEARCH_API_KEY', '--env', 'AIQ_RESEARCH_PRIVATE_DIRECTORY=/private', '--env', 'AIQ_RESEARCH_TLS_PFX=/tls/server.pfx', ($owner + ':research-api')))
    $owned.Add(@('container', $researchName))
    [void](Invoke-OwnedDocker @('start', $researchName))
    $ready = $false
    for ($i = 0; $i -lt 20; $i++) {
        $state = Invoke-OwnedDocker @('inspect', '--format', '{{.State.Running}}', $researchName)
        if ($state -ne 'true') { throw 'Research fixture exited; do not invent readiness.' }
        # Bounded startup allowance only, not endpoint readiness evidence.
        if ($i -ge 2) { $ready = $true; break }
        Start-Sleep -Milliseconds 250
    }
    if (-not $ready) { throw 'Research startup screening unavailable.' }
    $clientName = $owner + '-client'
    [void](Invoke-OwnedDocker @('create', '--name', $clientName, '--network', $owner, '--label', ('aiq.owner=' + $owner),
        '--read-only', '--cap-drop=ALL', '--security-opt=no-new-privileges', '--memory=128m', '--pids-limit=32',
        '--mount', ('type=bind,source=' + (Join-Path $work 'tls\ca.pem') + ',target=/ca.pem,readonly'),
        '--env', 'AIQ_RESEARCH_API_KEY', '--env', 'AIQ_RESEARCH_BASE_URL=https://wp05-research:52731', '--env', 'AIQ_RESEARCH_CA_FILE=/ca.pem', ($owner + ':snapshot-client')))
    $owned.Add(@('container', $clientName))
    [void](Invoke-OwnedDocker @('start', '-a', $clientName) 30)
    $code = Invoke-OwnedDocker @('inspect', '--format', '{{.State.ExitCode}}', $clientName)
    if ($code -ne '0') { throw 'Strict snapshot client rejected the response.' }
    Write-Output 'Local paired screening PASS; not production qualification; PUBLIC_RUNTIME_VERIFIED=False'
}
finally {
    [Environment]::SetEnvironmentVariable('AIQ_RESEARCH_API_KEY', $oldKey, 'Process')
    for ($i = $owned.Count - 1; $i -ge 0; $i--) {
        $kind, $name = $owned[$i]
        try {
            $format = if ($kind -eq 'network') { '{{index .Labels "aiq.owner"}}' } else { '{{index .Config.Labels "aiq.owner"}}' }
            $label = Invoke-OwnedDocker @($kind, 'inspect', '--format', $format, $name)
            if ($label -eq $owner) { [void](Invoke-OwnedDocker @($kind, 'rm', '-f', $name)) }
        }
        catch { Write-Warning 'Owned resource cleanup incomplete; preserve local evidence for review.' }
    }
    # Logs and disposable copies intentionally retained for review. No recursive deletion or owner-fixture mutation.
}

#Requires -Version 5.1
# UTF-8/LF. Offline operator wrapper; no provider, Azure or Docker commands.
[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)][string]$PublishedResearchDirectory,
    [Parameter(Mandatory=$true)][string]$ImportFile,
    [Parameter(Mandatory=$true)][string]$ConfigurationFile,
    [Parameter(Mandatory=$true)][string]$PrivateDirectory,
    [ValidateSet('Validate','Initialize')][string]$Action='Validate',
    [switch]$Execute
)
$ErrorActionPreference='Stop'
Set-StrictMode -Version 2.0
$stage='preflight'
function Resolve-PlainPath([string]$Value) {
    $full=[IO.Path]::GetFullPath($Value)
    $cursor=$full
    while($cursor) {
        if(Test-Path -LiteralPath $cursor) {
            if(((Get-Item -LiteralPath $cursor -Force).Attributes -band [IO.FileAttributes]::ReparsePoint) -ne 0) { throw 'Unsafe input.' }
        }
        $parent=[IO.Path]::GetDirectoryName($cursor)
        if($parent -eq $cursor){break}
        $cursor=$parent
    }
    return $full
}
function Read-BoundedUtf8([string]$File,[int]$Limit) {
    $path=Resolve-PlainPath $File
    $stream=[IO.File]::Open($path,[IO.FileMode]::Open,[IO.FileAccess]::Read,[IO.FileShare]::Read)
    try {
        if($stream.Length -lt 1 -or $stream.Length -gt $Limit){throw 'Input bounds.'}
        $bytes=New-Object byte[] ($Limit+1)
        $n=0
        while($n -lt $bytes.Length) {
            $read=$stream.Read($bytes,$n,$bytes.Length-$n)
            if($read -eq 0){break}
            $n+=$read
        }
        if($n -gt $Limit -or ($n -ge 3 -and $bytes[0] -eq 239 -and $bytes[1] -eq 187 -and $bytes[2] -eq 191)){throw 'Input encoding.'}
        $utf8=New-Object Text.UTF8Encoding($false,$true)
        return $utf8.GetString($bytes,0,$n)
    } finally { $stream.Dispose() }
}
try {
    if($Action -eq 'Initialize' -and -not $Execute){throw 'Explicit execution required.'}
    foreach($marker in @('WEBSITE_INSTANCE_ID','WEBSITE_SITE_NAME','WEBSITE_HOSTNAME','WEBSITE_ROLE_INSTANCE_ID')) {
        if($null -ne [Environment]::GetEnvironmentVariable($marker)){throw 'Unsupported host.'}
    }
    $published=Resolve-PlainPath $PublishedResearchDirectory
    $dll=Join-Path $published 'AIQuantTradingResearch.Worker.dll'
    if(-not (Test-Path -LiteralPath $dll -PathType Leaf)){throw 'Missing published worker.'}
    $null=Resolve-PlainPath $dll
    $target=Resolve-PlainPath $PrivateDirectory
    $inputPath=Resolve-PlainPath $ImportFile
    $configPath=Resolve-PlainPath $ConfigurationFile
    # Reject private inputs/state in the repository, even in an isolated worktree.
    $repo=[IO.Path]::GetFullPath((Join-Path $PSScriptRoot '../../..')).TrimEnd('\','/')
    foreach($path in @($target,$inputPath,$configPath)) {
        if($path -eq $repo -or $path.StartsWith($repo+[IO.Path]::DirectorySeparatorChar,[StringComparison]::OrdinalIgnoreCase)){throw 'Private input in repository.'}
    }
    $config=Read-BoundedUtf8 $configPath 4096
    $null=Read-BoundedUtf8 $inputPath 1048576
    # No JSON rewrite, candle construction or alternate validator. C# validates all
    # fields, source, 1..1000 rows/symbol, completed UTC minutes, OHLCV and D5.
    if($Action -eq 'Validate') {
        Write-Output 'PASS_LOCAL_INPUT_PREFLIGHT; C# semantic validation NOT_RUN; no process/state mutation; PUBLIC_RUNTIME_VERIFIED=False'
        exit 0
    }
    $dotnet=(Get-Command dotnet -CommandType Application -ErrorAction Stop).Source
    $psi=New-Object Diagnostics.ProcessStartInfo
    $psi.FileName=$dotnet
    $psi.Arguments='"'+$dll+'" --research-initialize'
    $psi.UseShellExecute=$false
    $psi.CreateNoWindow=$true
    $psi.WindowStyle=[Diagnostics.ProcessWindowStyle]::Hidden
    $psi.RedirectStandardInput=$true
    $psi.RedirectStandardOutput=$true
    $psi.RedirectStandardError=$true
    $psi.EnvironmentVariables['AIQ_RESEARCH_PRIVATE_DIRECTORY']=$target
    $psi.EnvironmentVariables['AIQ_RESEARCH_IMPORT_FILE']=$inputPath
    $process=[Diagnostics.Process]::Start($psi)
    $stage='initializer'
    try {
        $stdout=$process.StandardOutput.ReadToEndAsync()
        $stderr=$process.StandardError.ReadToEndAsync()
        $utf8=New-Object Text.UTF8Encoding($false)
        $payload=$utf8.GetBytes($config)
        $process.StandardInput.BaseStream.Write($payload,0,$payload.Length)
        $process.StandardInput.BaseStream.Flush()
        $process.StandardInput.Close()
        if(-not $process.WaitForExit(35000)) {
            $process.Kill()
            if(-not $process.WaitForExit(5000)){throw 'Owned initializer termination unverified.'}
            throw 'Owned initializer timed out.'
        }
        if($process.ExitCode -ne 0){throw 'Initializer rejected input/state.'}
        # Never echo child diagnostics, private paths, configurations or observations.
        Write-Output 'INITIALIZATION_COMPLETE; preserve authoritative state; staging acceptance NOT_RUN; PUBLIC_RUNTIME_VERIFIED=False'
    } finally {
        try {
            if(-not $process.HasExited) {
                $process.Kill()
                if(-not $process.WaitForExit(5000)){throw 'Owned initializer termination unverified.'}
            }
        } finally { $process.Dispose() }
    }
    exit 0
} catch {
    Write-Output ('IMPORT_BLOCKED; stage='+$stage+'; preserve inputs/state for operator review; no secret diagnostics; PUBLIC_RUNTIME_VERIFIED=False')
    exit 1
} finally { $config=$null }

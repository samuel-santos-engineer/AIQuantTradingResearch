$bytes = New-Object byte[] 32
$rng = [System.Security.Cryptography.RandomNumberGenerator]::Create()
try {
    $rng.GetBytes($bytes)
    $key = [System.BitConverter]::ToString($bytes).Replace('-', '').ToLowerInvariant()
} finally {
    $rng.Dispose()
    [Array]::Clear($bytes, 0, $bytes.Length)
}

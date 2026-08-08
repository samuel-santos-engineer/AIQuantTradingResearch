# Define the root folders for the repository architecture
$directories = @(
    "docs",
    "eng",
    "src",
    "tests",
    "samples",
    "benchmarks",
    "assets",
    "tools"
)

foreach ($dir in $directories) {
    if (-not (Test-Path $dir)) {
        New-Item -Path $dir -ItemType Directory | Out-Null
        Write-Host "✅ Created: /$dir" -ForegroundColor Green
    } else {
        Write-Host "ℹ️ Already Exists: /$dir" -ForegroundColor Yellow
    }
}

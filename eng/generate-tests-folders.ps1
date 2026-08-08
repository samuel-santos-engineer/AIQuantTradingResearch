# Define the root folders for the repository architecture
$directories = @(
    "Unit",
    "Integration",
    "Contract",
    "Performance",
    "Resilience",
    "EndToEnd"
)

foreach ($dir in $directories) {
    $src = "tests/$dir"
    if (-not (Test-Path $src)) {
        New-Item -Path $src -ItemType Directory | Out-Null
        New-Item -Path "$src/.gitkeep" -ItemType File | Out-Null
        Write-Host "✅ Created: /$src" -ForegroundColor Green
    } else {
        Write-Host "ℹ️ Already Exists: /$src" -ForegroundColor Yellow
    }
}

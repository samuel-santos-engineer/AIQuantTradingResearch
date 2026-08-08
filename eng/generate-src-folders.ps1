# Define the root folders for the repository architecture
$directories = @(
    "Core",
    "Abstractions",
    "Domain",
    "Data",
    "Infrastructure",
    "Plugins",
    "Host",
    "Shared"
)

foreach ($dir in $directories) {
    $src = "src/$dir"
    if (-not (Test-Path $src)) {
        New-Item -Path $src -ItemType Directory | Out-Null
        New-Item -Path "$src/.gitkeep" -ItemType File | Out-Null
        Write-Host "✅ Created: /$src" -ForegroundColor Green
    } else {
        Write-Host "ℹ️ Already Exists: /$src" -ForegroundColor Yellow
    }
}

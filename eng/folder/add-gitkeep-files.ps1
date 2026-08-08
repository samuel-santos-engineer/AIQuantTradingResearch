# Define target architecture folders
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
    if (Test-Path $dir) {
        $keepFile = Join-Path $dir ".gitkeep"
        
        # Only create the file if it doesn't already exist
        if (-not (Test-Path $keepFile)) {
            New-Item -Path $keepFile -ItemType File | Out-Null
            Write-Host "[ADDED .gitkeep] /$dir" -ForegroundColor Green
        } else {
            Write-Host "[SKIPPED] .gitkeep already exists in /$dir" -ForegroundColor Yellow
        }
    } else {
        Write-Host "[WARNING] Directory /$dir does not exist" -ForegroundColor Red
    }
}

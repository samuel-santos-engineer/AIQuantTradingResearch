# Define the target root directory
$baseDir = "AI-Engineering-Toolkit/playbooks/powershell"

# Array of markdown files to create
$files = @(
    "$baseDir/README.md",
    "$baseDir/01-script-architecture.md",
    "$baseDir/02-script-structure.md",
    "$baseDir/03-parameter-design.md",
    "$baseDir/04-error-handling.md",
    "$baseDir/05-logging.md",
    "$baseDir/06-validation.md",
    "$baseDir/07-testing.md",
    "$baseDir/08-documentation.md",
    "$baseDir/09-security.md",
    "$baseDir/10-script-review.md"
)

# Ensure the parent directory exists
if (-not (Test-Path $baseDir)) {
    New-Item -ItemType Directory -Path $baseDir -Force | Out-Null
}

# Create blank markdown files safely
foreach ($file in $files) {
    if (-not (Test-Path $file)) {
        New-Item -ItemType File -Path $file -Force | Out-Null
    }
}

Write-Host "Bootstrap playbook structure successfully added to $baseDir!" -ForegroundColor Green

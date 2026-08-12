# Define the target root directory
$baseDir = "AI-Engineering-Toolkit/playbooks/dotnet"

# Array of markdown files to create
$files = @(
    "$baseDir/README.md",
    "$baseDir/01-solution-architecture.md",
    "$baseDir/02-project-structure.md",
    "$baseDir/03-domain-driven-design.md",
    "$baseDir/04-dependency-management.md",
    "$baseDir/05-coding-standards.md",
    "$baseDir/06-error-handling.md",
    "$baseDir/07-logging.md",
    "$baseDir/08-testing.md",
    "$baseDir/09-security.md",
    "$baseDir/10-performance.md",
    "$baseDir/11-documentation.md",
    "$baseDir/12-project-review.md"
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

# Define the target root directory
$baseDir = "AI-Engineering-Toolkit/playbooks/bootstrap"

# Array of markdown files to create
$files = @(
    "$baseDir/README.md",
    "$baseDir/01-create-solution.md",
    "$baseDir/02-create-directory-structure.md",
    "$baseDir/03-create-build-assets.md",
    "$baseDir/04-create-github-assets.md",
    "$baseDir/05-create-documentation.md",
    "$baseDir/06-create-development-environment.md",
    "$baseDir/07-validate-bootstrap.md"
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

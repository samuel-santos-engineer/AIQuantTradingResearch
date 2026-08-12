# Define the target root directory
$baseDir = "AI-Engineering-Toolkit/playbooks/github"

# Array of markdown files to create
$files = @(
    "$baseDir/README.md",
    "$baseDir/01-repository-architecture.md",
    "$baseDir/02-repository-structure.md",
    "$baseDir/03-branching-strategy.md",
    "$baseDir/04-issue-management.md",
    "$baseDir/05-pull-request.md",
    "$baseDir/06-project-management.md",
    "$baseDir/07-release-management.md",
    "$baseDir/08-documentation.md",
    "$baseDir/09-security.md",
    "$baseDir/10-repository-review.md"
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

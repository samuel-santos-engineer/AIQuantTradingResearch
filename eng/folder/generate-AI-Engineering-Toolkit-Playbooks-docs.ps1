# Define the target root directory
$baseDir = "AI-Engineering-Toolkit/playbooks/docs"

# Array of markdown files to create
$files = @(
    "$baseDir/README.md",
    "$baseDir/01-toolkit-overview.md",
    "$baseDir/02-getting-started.md",
    "$baseDir/03-toolkit-architecture.md",
    "$baseDir/04-repository-organization.md",
    "$baseDir/05-engineering-workflow.md",
    "$baseDir/06-ai-assisted-engineering.md",
    "$baseDir/07-playbook-usage.md",
    "$baseDir/08-prompt-usage.md",
    "$baseDir/09-reference-implementation-usage.md",
    "$baseDir/10-validation-and-quality.md",
    "$baseDir/11-governance-and-maintenance.md",
    "$baseDir/12-contribution-guide.md"
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

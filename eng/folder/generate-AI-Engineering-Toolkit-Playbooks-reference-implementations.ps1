# Define the target root directory
$baseDir = "AI-Engineering-Toolkit/playbooks/reference-implementations"

# Array of markdown files to create
$files = @(
    "$baseDir/README.md",
    "$baseDir/01-repository-bootstrap.md",
    "$baseDir/02-powershell-engineering.md",
    "$baseDir/03-github-governance.md",
    "$baseDir/04-dotnet-solution.md",
    "$baseDir/05-domain-driven-design.md",
    "$baseDir/06-testing-strategy.md",
    "$baseDir/07-observability.md",
    "$baseDir/08-security.md",
    "$baseDir/09-ai-assisted-engineering.md",
    "$baseDir/10-end-to-end-reference.md"
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

# Define the target root directory
$baseDir = "AI-Engineering-Toolkit/playbooks/prompt-quality"

# Array of markdown files to create
$files = @(
    "$baseDir/README.md",
    "$baseDir/01-prompt-quality-principles.md",
    "$baseDir/02-prompt-clarity.md",
    "$baseDir/03-context-management.md",
    "$baseDir/04-scope-and-boundaries.md",
    "$baseDir/05-instruction-design.md",
    "$baseDir/06-output-contracts.md",
    "$baseDir/07-validation-and-acceptance.md",
    "$baseDir/08-error-and-ambiguity-handling.md",
    "$baseDir/09-security-and-safety.md",
    "$baseDir/10-prompt-review.md"
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

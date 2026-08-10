# Define the root directory
$rootDir = "AI-Engineering-Toolkit"

# Array of all directories to create
$directories = @(
    "$rootDir",
    "$rootDir/playbooks",
    "$rootDir/architecture",
    "$rootDir/bootstrap",
    "$rootDir/documentation",
    "$rootDir/dotnet",
    "$rootDir/github",
    "$rootDir/powershell",
    "$rootDir/devops",
    "$rootDir/testing",
    "$rootDir/code-review",
    "$rootDir/refactoring",
    "$rootDir/security",
    "$rootDir/ai",
    "$rootDir/templates",
    "$rootDir/templates/prompts",
    "$rootDir/standards",
    "$rootDir/examples/bootstrap",
    "$rootDir/examples/architecture",
    "$rootDir/examples/documentation",
    "$rootDir/reference-implementations/AIQuantTradingResearch",
    "$rootDir/assets/diagrams",
    "$rootDir/assets/images"
)

# Array of empty files to create
$files = @(
    "$rootDir/README.md",
    "$rootDir/LICENSE",
    "$rootDir/CHANGELOG.md",
    "$rootDir/CONTRIBUTING.md",
    "$rootDir/templates/prompts/prompt-template.md",
    "$rootDir/templates/prompts/playbook-template.md",
    "$rootDir/templates/prompts/review-template.md",
    "$rootDir/templates/prompts/validation-template.md",
    "$rootDir/standards/prompt-quality.md",
    "$rootDir/standards/prompt-lifecycle.md",
    "$rootDir/standards/naming-conventions.md",
    "$rootDir/standards/repository-guidelines.md"
)

# Create folders
foreach ($dir in $directories) {
    if (-not (Test-Path $dir)) {
        New-Item -ItemType Directory -Path $dir -Force | Out-Null
    }
}

# Create files
foreach ($file in $files) {
    if (-not (Test-Path $file)) {
        New-Item -ItemType File -Path $file -Force | Out-Null
    }
}

Write-Host "Directory structure created successfully!" -ForegroundColor Green

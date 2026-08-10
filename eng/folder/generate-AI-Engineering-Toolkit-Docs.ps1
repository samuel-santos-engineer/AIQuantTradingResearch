# Define the target root directory
$baseDir = "AI-Engineering-Toolkit/docs"

# Array of all subdirectories to create
$directories = @(
    "$baseDir/architecture",
    "$baseDir/standards",
    "$baseDir/framework",
    "$baseDir/prompt-framework",
    "$baseDir/bootstrap",
    "$baseDir/guides",
    "$baseDir/roadmap"
)

# Array of markdown files to create
$files = @(
    "$baseDir/architecture/PROMPT_ARCHITECTURE.md",
    "$baseDir/standards/PROMPT_METADATA.md",
    "$baseDir/standards/PROMPT_LIFECYCLE.md",
    "$baseDir/standards/QUALITY_GUIDELINES.md",
    "$baseDir/standards/NAMING_CONVENTIONS.md",
    "$baseDir/framework/PLAYBOOK_TEMPLATE.md",
    "$baseDir/framework/REVIEW_TEMPLATE.md",
    "$baseDir/framework/VALIDATION_TEMPLATE.md",
    "$baseDir/prompt-framework/PROMPT_ARCHITECTURE.md",
    "$baseDir/prompt-framework/PROMPT_METADATA.md",
    "$baseDir/prompt-framework/PROMPT_LIFECYCLE.md",
    "$baseDir/bootstrap/BOOTSTRAP_PROMPT_ARCHITECTURE.md",
    "$baseDir/bootstrap/BOOTSTRAP_EXECUTION_MODEL.md",
    "$baseDir/bootstrap/BOOTSTRAP_STATE_MACHINE.md",
    "$baseDir/bootstrap/BOOTSTRAP_ORCHESTRATION.md",
    "$baseDir/guides/GETTING_STARTED.md",
    "$baseDir/guides/AUTHORING_PLAYBOOKS.md",
    "$baseDir/guides/CONTRIBUTING.md",
    "$baseDir/roadmap/ROADMAP.md",
    "$baseDir/roadmap/PROJECT_STATUS.md"
)

# Create folders safely
foreach ($dir in $directories) {
    if (-not (Test-Path $dir)) {
        New-Item -ItemType Directory -Path $dir -Force | Out-Null
    }
}

# Create blank markdown files safely
foreach ($file in $files) {
    if (-not (Test-Path $file)) {
        New-Item -ItemType File -Path $file -Force | Out-Null
    }
}

Write-Host "New document structure successfully added to $baseDir!" -ForegroundColor Green

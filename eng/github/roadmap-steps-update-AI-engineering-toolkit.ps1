# ==============================================================================
# AIQuantTradingResearch - Project Board Roadmap Importer (Verified Native Engine)
# ==============================================================================

# 1. Define your exact raw text block input
$rawTextData = @"

| Roadmap Step                   | Summary                                                                                                                                                                                                                                                                                    | Priority | Release | Area          | Label                      |
| ------------------------------ | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ | :------: | :-----: | ------------- | -------------------------- |
| **Toolkit Repository**         | Create a dedicated repository to host the AI Engineering Toolkit, including repository structure, governance, documentation, contribution guidelines, licensing, and versioning. Establish the foundation for reusable engineering playbooks independent of any specific software project. |  **P0**  | **0.7** | Repository    | feature |
| **Prompt Architecture**        | Define the prompt taxonomy, folder hierarchy, naming conventions, metadata model, lifecycle, and versioning strategy to ensure prompts remain consistent, discoverable, reusable, and maintainable as the toolkit grows.                                                                   |  **P0**  | **0.7** | Architecture  | feature  |
| **Prompt Template**            | Design the canonical prompt template defining objective, context, requirements, constraints, expected deliverables, acceptance criteria, validation checklist, and usage guidance. This template becomes the standard for every engineering playbook.                                      |  **P0**  | **0.7** | Standards     | feature |
| **Bootstrap Playbooks**        | Create the initial engineering playbooks for solution creation, project scaffolding, repository initialization, folder structure, and engineering assets. These playbooks will drive the first implementation activities of AIQuantTradingResearch.                                        |  **P0**  | **0.7** | Bootstrap     | feature,        |
| **PowerShell Playbooks**       | Develop reusable playbooks focused on generating high-quality, idempotent PowerShell automation following engineering standards for validation, logging, error handling, modularity, and maintainability.                                                                                  |  **P1**  | **0.7** | Automation    | feature,        |
| **GitHub Playbooks**           | Create standardized playbooks for generating GitHub workflows, issue templates, pull request templates, project boards, labels, milestones, and repository automation while following governance and engineering standards.                                                                |  **P1**  | **0.7** | GitHub        | devops        |
| **.NET Engineering Playbooks** | Build reusable prompts for generating .NET solutions, projects, dependency injection, configuration, logging, testing, package management, and architectural patterns aligned with enterprise engineering practices.                                                                       |  **P1**  | **0.7** | .NET Platform | architecture  |
| **Prompt Quality Guidelines**  | Define quality standards covering clarity, determinism, context completeness, validation criteria, expected outputs, and review processes to ensure engineering prompts remain reliable and reusable over time.                                                                            |  **P1**  | **0.7** | Quality       | tests   |
| **Reference Implementations**  | Validate the toolkit by using its own playbooks to generate the initial implementation assets for AIQuantTradingResearch. Confirm that prompts are complete, reproducible, and produce results aligned with the documented architecture.                                                   |  **P0**  | **0.7** | Validation    | tests         |
| **Toolkit Documentation**      | Produce comprehensive documentation describing the toolkit architecture, repository organization, usage patterns, contribution workflow, prompt lifecycle, and best practices for AI-assisted software engineering.                                                                        |  **P2**  | **0.7** | Documentation | documentation            |
"@

# Configuration metrics pointing to your target roadmap name
$projectName = "AIQuantTradingResearch Engineering Roadmap"
$milestone = "Phase 2 - Release 0.7: AI Engineering Toolkit"
$projectWebNumber = 2
$owner = "@me"
$ghExe = "C:\Program Files\GitHub CLI\gh.exe"

# 2. Parse the markdown table text data line by line
$lines = $rawTextData -split "`r?`n"

function Set-Field-Value {
  param(
    [Parameter(Mandatory = $true)][string]$issueNumber,
    [Parameter(Mandatory = $true)][string]$fieldName,
    [Parameter(Mandatory = $true)][string]$textValue
  )

  Write-Host "   [INFO] Setting project field '$fieldName' to '$textValue' for issue #$issueNumber..." -ForegroundColor Yellow
  $ProjectId = (& $ghExe project view $projectWebNumber --owner $owner --format json | ConvertFrom-Json).id
  Write-Host "   [INFO] Resolved Project ID: $ProjectId" -ForegroundColor Yellow

  Write-Host "Searching for Issue #$IssueNumber inside the project layout..." -ForegroundColor Cyan

  # 2. Query to pull items along with their repository issue number numbers
  $query = @"
query(`$projectId: ID!) {
  node(id: `$projectId) {
    ... on ProjectV2 {
      items(first: 100) {
        nodes {
          id
          content {
            ... on Issue {
              number
              title
            }
            ... on PullRequest {
              number
              title
            }
          }
        }
      }
    }
  }
}
"@

  # 3. Fetch from GitHub API using pure variables to avoid malformed string bugs
  $response = & $ghExe api graphql -f query=$query -F projectId=$ProjectId | ConvertFrom-Json

  # 4. Filter nodes by matching issue/PR chronological code
  $projectItem = $response.data.node.items.nodes | Where-Object { $_.content.number -eq $issueNumber }

  # 5. Extract and print the Target ID
  if ($projectItem) {
    $ItemId = $projectItem.id
    Write-Host "Success! Found Matching Item Record:" -ForegroundColor Green
    [PSCustomObject]@{
      "Issue Code"  = "#$issueNumber"
      "Title"       = $projectItem.content.title
      "Internal ID" = $ItemId
    } | Format-List
  }
  else {
    Write-Error "Could not find Issue #$issueNumber associated with this project card layout."
  }

  $query = @"
query(`$projectId: ID!) {
  node(id: `$projectId) {
    ... on ProjectV2 {
      fields(first: 100) {
        nodes {
          __typename
          ... on ProjectV2SingleSelectField { id name options { id name } }
        }
      }
    }
  }
}
"@

  $response = & $ghExe api graphql -f query=$query -F projectId=$ProjectId | ConvertFrom-Json

  $propertyField = $response.data.node.fields.nodes | Where-Object { $_.name -eq $FieldName }
  $propertyFieldid = $($propertyField.id)

  if ($propertyField) {
    Write-Host "Property Field ID: $propertyFieldid" -ForegroundColor Cyan
    $p0Option = $propertyField.options | Where-Object { $_.name -eq $textValue }
    $p0Optionid = $($p0Option.id)
    Write-Host "Option ID for '$textValue': $p0Optionid" -ForegroundColor Cyan

    if ($p0Option) {
      # 6. Execute the mutation to set the field value
      $mutationQuery = 'mutation SetPriority($projectId: ID!, $itemId: ID!, $fieldId: ID!, $optionId: String!) {
  updateProjectV2ItemFieldValue(
    input: {
      projectId: $projectId
      itemId: $itemId
      fieldId: $fieldId
      value: { singleSelectOptionId: $optionId }
    }
  ) {
    projectV2Item {
      id
    }
  }
}'
      $response = & $ghExe api graphql -f query=$mutationQuery -F projectId=$ProjectId -F itemId=$ItemId -F fieldId=$propertyFieldid -F optionId=$p0Optionid

      if ($LASTEXITCODE -eq 0) {
        Write-Host "   [SUCCESS] Field '$fieldName' set to '$textValue' for issue #$issueNumber." -ForegroundColor Green
      }
      else {
        Write-Host "   [ERROR] Failed to set field '$fieldName' for issue #$issueNumber." -ForegroundColor Red
      }

    }
    else {
      Write-Warning "Could not find an option named '$textValue' for field '$fieldName'. Double-check your option casing."
    }
  }
  else {
    Write-Warning "Could not find a project field named '$fieldName'. Double-check your column casing."
  }


}

foreach ($line in $lines) {
  # Isolate row elements containing valid table data
  if ($line -notmatch '^\|\s*\*\*.*\|') { continue }
    
  # Split fields based on pipe delimiters and trim whitespace properties
  $parts = $line.Split('|') | ForEach-Object { $_.Trim() }
    
  # Extract values cleanly and strip syntax markers out
  $title = $parts[1] -replace '\*\*|\*', ''
  $summary = $parts[2] -replace '\*\*|\*', ''
  $priority = $parts[3] -replace '\*\*|\*', ''
  $release = $parts[4] -replace '\*\*|\*', ''
  $area = $parts[5] -replace '\*\*|\*', ''
  $label = $parts[6] -replace '\*\*|\*', ''

  $title = $("[" + $label.Substring(0, 1).ToUpper() + $label.Substring(1).ToLower() + "]: " + $title)
  Write-Host "   [INFO] Processing roadmap step: $title" -ForegroundColor Cyan

  # Build out clean markdown issue body texts for high-scannability
  $bodyText = "### 📝 Objective Summary`n$summary"

  Write-Host " ➕ Provisioning Issue: $title..." -ForegroundColor Blue
    
  $issueOutput = & $ghExe issue create --title $title --body $bodyText --project $projectName | Out-String
  $issueId = ($issueOutput -split '/')[-1].Trim()
  #$issueId = 21
  Write-Host "   [INFO] Created issue URL: $issueOutput >> issue ID: $issueId" -ForegroundColor Yellow
  
  Write-Host "   [INFO] Linking issue #$issueId to milestone '$milestone'..." -ForegroundColor Yellow
  & $ghExe issue edit $issueId --milestone "$milestone"

  Write-Host "   [INFO] Assigning issue #$issueId to the current user..." -ForegroundColor Yellow
  & $ghExe issue edit $issueId --add-assignee "@me"

  Write-Host "   [INFO] Adding label '$label' to issue #$issueId..." -ForegroundColor Yellow
  & $ghExe issue edit $issueId --add-label "$label"

  if ($LASTEXITCODE -eq 0) {
    Write-Host "   [SUCCESS] '$title' (ID: $issueId) generated and linked to the roadmap board!" -ForegroundColor Green
        
    Set-Field-Value -issueNumber $issueId -fieldName "Priority" -textValue $priority
    Set-Field-Value -issueNumber $issueId -fieldName "Release" -textValue $release
    Set-Field-Value -issueNumber $issueId -fieldName "Area" -textValue $area

  }
  else {
    Write-Host "   [ERROR] '$title' (ID: $issueId) Failed to push item tracking link." -ForegroundColor Red
  }
}

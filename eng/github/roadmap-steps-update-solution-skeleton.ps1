# ==============================================================================
# AIQuantTradingResearch - Project Board Roadmap Importer (Verified Native Engine)
# ==============================================================================

# 1. Define your exact raw text block input
$rawTextData = @"

| Roadmap Step                | Summary                                                                                                                                                                                                                                                                                     | Priority | Release | Area                       | Label                            |
| --------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | :------: | :-----: | -------------------------- | ---------------------------------|
| **Solution Skeleton**       | Create the initial .NET solution implementing the documented architectural layers (Core, Abstractions, Domain, Data, Infrastructure, Plugins and Host). Establish project references, namespaces, and dependency flow to transform the architectural blueprint into an executable solution. |  **P0**  | **0.7** | Architecture               | feature                          |
| **Project Bootstrap**       | Create the foundational projects, shared build configuration, centralized package management, analyzers, nullable reference types, solution folders, and repository configuration required to establish a consistent engineering baseline.                                                  |  **P0**  | **0.7** | Engineering Infrastructure | feature                          |
| **Application Host**        | Implement the application composition root responsible for startup, configuration loading, dependency injection, logging initialization, and lifecycle management while keeping business functionality independent from infrastructure concerns.                                            |  **P0**  | **0.7** | Host                       | feature                          |
| **Configuration Framework** | Implement strongly typed configuration, validation, environment-aware settings, configuration providers, and centralized configuration management aligned with the documented architecture.                                                                                                 |  **P1**  | **0.7** | Configuration              | feature                          |
| **Build Automation**        | Complete cross-platform build automation supporting restore, build, formatting, testing, verification, packaging, and developer workflows using standardized engineering scripts and repository tooling.                                                                                    |  **P0**  | **0.8** | DevOps                     | feature                          |
| **Continuous Integration**  | Configure GitHub Actions to automatically validate formatting, compilation, testing, dependency consistency, architecture verification, and engineering quality gates for every commit and pull request.                                                                                    |  **P0**  | **0.8** | DevOps                     | feature                          |
| **Quality Gates**           | Enforce engineering quality through static analysis, warnings as errors, formatting validation, dependency verification, architectural consistency checks, and automated repository validation.                                                                                             |  **P1**  | **0.8** | Quality Assurance          | feature                          |
| **Engineering Tooling**     | Complete the engineering toolchain by integrating repository scripts, local validation commands, developer utilities, and automation that simplify daily development while maintaining consistency across contributors.                                                                     |  **P2**  | **0.8** | Engineering Infrastructure | enhancement                      |
| **Plugin Infrastructure**   | Implement the extensibility framework including plugin discovery, registration, lifecycle management, dependency injection integration, and contract-based module loading while preserving architectural boundaries.                                                                        |  **P0**  | **0.9** | Plugin Architecture        | feature                          |
| **Reference Plugin**        | Develop the first reference plugin demonstrating recommended implementation patterns, dependency registration, configuration integration, lifecycle management, and extensibility best practices.                                                                                           |  **P1**  | **0.9** | Plugin Architecture        | feature                          |
| **Bootstrap Validation**    | Validate the complete platform bootstrap by confirming successful builds, automated pipelines, dependency composition, plugin loading, application startup, and engineering workflows before beginning core platform development.                                                           |  **P0**  | **0.9** | Validation                 | tests                            |
"@

# Configuration metrics pointing to your target roadmap name
$projectName = "AIQuantTradingResearch Engineering Roadmap"
$milestone = "Phase 2 - Release 0.7: Solution Skeleton"
$projectWebNumber = 2
$owner = "@me"
$ghExe = "C:\Program Files\GitHub CLI\gh.exe"

# 2. Parse the markdown table text data line by line
$lines = $rawTextData -split "`r?`n"
$parsedItems = @()


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

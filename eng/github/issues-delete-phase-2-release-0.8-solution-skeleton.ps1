[CmdletBinding(SupportsShouldProcess, ConfirmImpact = 'Medium')]
param(
    [switch]$Force
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

$owner = 'samuel-santos-engineer'
$repository = 'AIQuantTradingResearch'
$milestoneNumber = 39
$expectedMilestoneTitle = 'Phase 2 - Release 0.8: Solution Skeleton'
$expectedMilestoneUrl = "https://github.com/$owner/$repository/milestone/$milestoneNumber"

$ghCommand = Get-Command gh -ErrorAction SilentlyContinue
if ($null -eq $ghCommand) {
    $defaultGhPath = 'C:\Program Files\GitHub CLI\gh.exe'
    if (-not (Test-Path -LiteralPath $defaultGhPath -PathType Leaf)) {
        throw 'GitHub CLI (gh) was not found. Install it and authenticate with "gh auth login".'
    }

    $ghExe = $defaultGhPath
}
else {
    $ghExe = $ghCommand.Source
}

function Invoke-GitHubApi {
    param(
        [Parameter(Mandatory)]
        [string[]]$Arguments
    )

    $output = & $ghExe api @Arguments 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw "GitHub API request failed: $($output -join [Environment]::NewLine)"
    }

    return $output
}

Write-Host "Verifying milestone $milestoneNumber in $owner/$repository..." -ForegroundColor Cyan
$milestoneJson = Invoke-GitHubApi -Arguments @(
    "repos/$owner/$repository/milestones/$milestoneNumber"
)
$milestone = ($milestoneJson -join [Environment]::NewLine) | ConvertFrom-Json

if ($milestone.title -cne $expectedMilestoneTitle) {
    throw "Safety check failed. Milestone $milestoneNumber is '$($milestone.title)', not '$expectedMilestoneTitle'."
}

if ($milestone.html_url -cne $expectedMilestoneUrl) {
    throw "Safety check failed. The milestone URL is '$($milestone.html_url)', not '$expectedMilestoneUrl'."
}

$issues = [System.Collections.Generic.List[object]]::new()
$page = 1

do {
    $pageJson = Invoke-GitHubApi -Arguments @(
        "repos/$owner/$repository/issues?milestone=$milestoneNumber&state=all&per_page=100&page=$page"
    )
    # Windows PowerShell can preserve a JSON array as one nested pipeline item.
    # Keep the parsed array directly so foreach enumerates each issue separately.
    $pageItems = ($pageJson -join [Environment]::NewLine) | ConvertFrom-Json
    $pageItemCount = @($pageItems).Count

    # GitHub's issues endpoint also returns pull requests. Never delete those.
    foreach ($item in $pageItems) {
        if ($null -eq $item.PSObject.Properties['pull_request']) {
            if ($item.number -is [array] -or $item.node_id -is [array]) {
                throw 'Safety check failed: GitHub returned a nested issue collection.'
            }

            if ([string]::IsNullOrWhiteSpace([string]$item.node_id)) {
                throw "Safety check failed: issue #$($item.number) has no GraphQL node ID."
            }

            $issues.Add($item)
        }
    }

    $page++
} while ($pageItemCount -eq 100)

if ($issues.Count -eq 0) {
    Write-Host "No issues are assigned to '$expectedMilestoneTitle'. Nothing to delete." -ForegroundColor Yellow
    exit 0
}

Write-Host "Found $($issues.Count) issue(s) assigned to '$expectedMilestoneTitle':" -ForegroundColor Yellow
foreach ($issue in $issues) {
    Write-Host ("  #{0} {1}" -f $issue.number, $issue.title)
}

if (-not $Force) {
    $confirmation = Read-Host "Type DELETE $milestoneNumber to permanently delete these issues"
    if ($confirmation -cne "DELETE $milestoneNumber") {
        Write-Host 'Deletion cancelled.' -ForegroundColor Yellow
        exit 0
    }
}

$deleteMutation = @'
mutation DeleteIssue($issueId: ID!) {
  deleteIssue(input: { issueId: $issueId }) {
    repository { nameWithOwner }
  }
}
'@

$deletedCount = 0
foreach ($issue in $issues) {
    $target = "#$($issue.number) $($issue.title)"
    if ($PSCmdlet.ShouldProcess($target, "Permanently delete from $owner/$repository")) {
        Invoke-GitHubApi -Arguments @(
            'graphql',
            '-f', "query=$deleteMutation",
            '-f', "issueId=$($issue.node_id)",
            '--silent'
        ) | Out-Null

        $deletedCount++
        Write-Host "Deleted $target" -ForegroundColor Green
    }
}

Write-Host "Completed. Permanently deleted $deletedCount of $($issues.Count) issue(s)." -ForegroundColor Green

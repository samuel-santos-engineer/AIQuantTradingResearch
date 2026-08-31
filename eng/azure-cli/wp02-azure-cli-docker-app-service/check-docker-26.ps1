$ErrorActionPreference = 'Continue'

Write-Host '=== ISSUE #253 ==='
gh issue view 253 --json number,state,title,milestone,projectItems
Write-Host "ISSUE_253_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== PROJECT #2 ITEMS ==='
gh project item-list 2 --owner samuel-santos-engineer --format json --limit 100
Write-Host "PROJECT_2_READ_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== MILESTONE #62 ==='
gh api repos/{owner}/{repo}/milestones/62 `
  --jq "{number:number,title:title,state:state,openIssues:open_issues,closedIssues:closed_issues}"
Write-Host "MILESTONE_62_READ_EXIT_CODE=$LASTEXITCODE"
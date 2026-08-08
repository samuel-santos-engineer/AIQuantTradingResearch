# 1. Assign the absolute verified binary path
$ghExe = "C:\Program Files\GitHub CLI\gh.exe"

Write-Host "🔍 Fetching all existing milestones from remote repository..." -ForegroundColor Cyan

# 2. Query both 'open' and 'closed' milestones to ensure a total reset
$openMilestones   = & $ghExe api repos/:owner/:repo/milestones?state=open | ConvertFrom-Json
$closedMilestones = & $ghExe api repos/:owner/:repo/milestones?state=closed | ConvertFrom-Json
$allMilestones    = $openMilestones + $closedMilestones

if ($null -eq $allMilestones -or $allMilestones.Count -eq 0) {
    Write-Host "ℹ️ No milestones found. The repository space is already clean." -ForegroundColor Yellow
    exit
}

Write-Host "⚠️ Found $($allMilestones.Count) milestone(s) to remove." -ForegroundColor Yellow

# 3. Iterate through and delete each item using its distinct ID number
foreach ($ms in $allMilestones) {
    Write-Host " [DELETING] ID: $($ms.number) - Title: $($ms.title)..." -ForegroundColor Red
    
    # Fire the REST API DELETE instruction against the milestone identifier
    & $ghExe api repos/:owner/:repo/milestones/$($ms.number) -X DELETE | Out-Null
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host " [REMOVED] Successfully wiped!" -ForegroundColor Green
    } else {
        Write-Host " [FAILED] Could not purge milestone ID $($ms.number)." -ForegroundColor Red
    }
}

Write-Host "✨ Repository milestone cleanup complete!" -ForegroundColor Green

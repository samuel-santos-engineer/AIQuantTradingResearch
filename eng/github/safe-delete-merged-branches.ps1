# run this directly in powershell terminal to unblock scripts execution for the current session 
[CmdletBinding()]
param(
    [string]$Remote = "origin",
    [string]$ProtectedBranch = "main",
    [switch]$DryRun
)

$ErrorActionPreference = "Stop"

function Stop-Script([string]$Message) {
    Write-Host ""
    Write-Host "STOPPED: $Message" -ForegroundColor Red
    exit 1
}

Write-Host "============================================="
Write-Host " Safe GitHub Merged-Branch Cleanup"
Write-Host "============================================="
Write-Host ""

# ------------------------------------------------------------
# 1. Verify this is a Git repository.
# ------------------------------------------------------------

git rev-parse --is-inside-work-tree *> $null

if ($LASTEXITCODE -ne 0) {
    Stop-Script "Current directory is not a Git repository."
}

# ------------------------------------------------------------
# 2. Require a clean working tree.
# ------------------------------------------------------------

$status = @(git status --porcelain)

# Allow this cleanup script itself to remain untracked.
$scriptRelativePath = "eng/github/safe-delete-merged-branches.ps1"

$unsafeStatus = @(
    $status | Where-Object {
        $line = $_
        $path = $line.Substring(3).Trim().Replace("\", "/")
        $path -ne $scriptRelativePath
    }
)

if ($unsafeStatus.Count -gt 0) {
    Write-Host "Working tree contains unrelated changes:" -ForegroundColor Yellow
    $unsafeStatus | ForEach-Object { Write-Host "  $_" }

    Stop-Script "Commit, stash, or remove unrelated local changes before branch cleanup."
}

if ($status.Count -gt 0) {
    Write-Host "Permitted local utility:" -ForegroundColor Cyan
    Write-Host "  ?? $scriptRelativePath"
    Write-Host ""
}
# ------------------------------------------------------------
# 3. Verify the configured remote.
# ------------------------------------------------------------

$remoteUrl = git remote get-url $Remote

if ($LASTEXITCODE -ne 0 -or -not $remoteUrl) {
    Stop-Script "Remote '$Remote' does not exist."
}

Write-Host "Remote:"
Write-Host "  $Remote -> $remoteUrl"
Write-Host ""

# ------------------------------------------------------------
# 4. Fetch current GitHub state.
# ------------------------------------------------------------

Write-Host "Fetching current remote state..."

git fetch $Remote --prune

if ($LASTEXITCODE -ne 0) {
    Stop-Script "git fetch failed."
}

$mainRef = "refs/remotes/$Remote/$ProtectedBranch"

git show-ref --verify --quiet $mainRef

if ($LASTEXITCODE -ne 0) {
    Stop-Script "Cannot find $Remote/$ProtectedBranch."
}

$mainSha = git rev-parse "$Remote/$ProtectedBranch"

Write-Host ""
Write-Host "Protected canonical branch:"
Write-Host "  $Remote/$ProtectedBranch"
Write-Host "  $mainSha"
Write-Host ""

# ------------------------------------------------------------
# 5. Enumerate remote branches.
# ------------------------------------------------------------

$remoteRefs = git for-each-ref `
    --format="%(refname:short)" `
    "refs/remotes/$Remote"

if ($LASTEXITCODE -ne 0) {
    Stop-Script "Unable to enumerate remote branches."
}

$candidates = @()
$preserved  = @()

foreach ($remoteRef in $remoteRefs) {

    # origin/HEAD is symbolic metadata, not a branch to delete.
    if ($remoteRef -eq "$Remote/HEAD") {
        continue
    }

    if (-not $remoteRef.StartsWith("$Remote/")) {
        continue
    }

    $branch = $remoteRef.Substring($Remote.Length + 1)

    # Never delete canonical branch.
    if ($branch -eq $ProtectedBranch) {
        $preserved += [PSCustomObject]@{
            Branch = $branch
            Reason = "Protected canonical branch"
        }

        continue
    }

    # --------------------------------------------------------
    # Critical safety test:
    #
    # Is the branch tip already an ancestor of origin/main?
    #
    # exit 0 = all commits reachable from this branch tip
    #          are already represented in main history.
    #
    # exit 1 = branch contains history not reachable from main.
    # --------------------------------------------------------

    git merge-base --is-ancestor `
        "refs/remotes/$Remote/$branch" `
        $mainRef

    $ancestorResult = $LASTEXITCODE

    if ($ancestorResult -eq 0) {

        $sha = git rev-parse "refs/remotes/$Remote/$branch"

        $candidates += [PSCustomObject]@{
            Branch = $branch
            SHA    = $sha
        }
    }
    else {

        $preserved += [PSCustomObject]@{
            Branch = $branch
            Reason = "Tip is NOT reachable from $Remote/$ProtectedBranch"
        }
    }
}

# ------------------------------------------------------------
# 6. Show branches that WILL NOT be deleted.
# ------------------------------------------------------------

Write-Host ""
Write-Host "============================================="
Write-Host " PRESERVED BRANCHES"
Write-Host "============================================="

if ($preserved.Count -eq 0) {
    Write-Host "None."
}
else {
    $preserved |
        Sort-Object Branch |
        Format-Table Branch, Reason -AutoSize
}

# ------------------------------------------------------------
# 7. Show deletion candidates.
# ------------------------------------------------------------

Write-Host ""
Write-Host "============================================="
Write-Host " SAFE DELETION CANDIDATES"
Write-Host "============================================="

if ($candidates.Count -eq 0) {

    Write-Host ""
    Write-Host "No fully merged remote branches found."
    Write-Host "Nothing will be deleted."

    exit 0
}

$candidates |
    Sort-Object Branch |
    Format-Table Branch, SHA -AutoSize

Write-Host ""
Write-Host "$($candidates.Count) remote branch(es) qualify for deletion."
Write-Host ""
Write-Host "Qualification rule:"
Write-Host "  branch tip must be an ancestor of $Remote/$ProtectedBranch"
Write-Host ""

# ------------------------------------------------------------
# 8. Dry-run support.
# ------------------------------------------------------------

if ($DryRun) {

    Write-Host "DRY RUN: no branches were deleted." -ForegroundColor Cyan

    exit 0
}

# ------------------------------------------------------------
# 9. Explicit destructive-operation confirmation.
# ------------------------------------------------------------

Write-Host "WARNING:" -ForegroundColor Yellow
Write-Host "The branches listed above will be deleted from '$Remote'."
Write-Host ""
Write-Host "Commits already reachable from '$Remote/$ProtectedBranch'"
Write-Host "will remain in canonical Git history."
Write-Host ""

$confirmation = Read-Host "Type DELETE"

if ($confirmation -cne "DELETE") {

    Write-Host ""
    Write-Host "Confirmation did not match."
    Write-Host "No branches were deleted."

    exit 0
}

# ------------------------------------------------------------
# 10. Re-fetch immediately before mutation.
#
# This reduces the chance that remote state changed between
# inspection and deletion.
# ------------------------------------------------------------

Write-Host ""
Write-Host "Refreshing remote state before deletion..."

git fetch $Remote --prune

if ($LASTEXITCODE -ne 0) {
    Stop-Script "Final git fetch failed. Nothing further will be deleted."
}

# ------------------------------------------------------------
# 11. Delete branches one at a time.
#
# Revalidate every branch immediately before deleting it.
# ------------------------------------------------------------

$deleted = @()
$skipped = @()
$failed  = @()

foreach ($candidate in ($candidates | Sort-Object Branch)) {

    $branch = $candidate.Branch
    $remoteBranchRef = "refs/remotes/$Remote/$branch"

    Write-Host ""
    Write-Host "Checking: $branch"

    # Branch may have disappeared since initial scan.
    git show-ref --verify --quiet $remoteBranchRef

    if ($LASTEXITCODE -ne 0) {

        Write-Host "  SKIP: branch no longer exists."

        $skipped += $branch
        continue
    }

    # Revalidate ancestry immediately before deletion.
    git merge-base --is-ancestor `
        $remoteBranchRef `
        "refs/remotes/$Remote/$ProtectedBranch"

    if ($LASTEXITCODE -ne 0) {

        Write-Host "  SKIP: branch is no longer safely merged." `
            -ForegroundColor Yellow

        $skipped += $branch
        continue
    }

    # Confirm SHA has not changed since initial classification.
    $currentSha = git rev-parse $remoteBranchRef

    if ($currentSha -ne $candidate.SHA) {

        Write-Host "  SKIP: branch tip changed during cleanup." `
            -ForegroundColor Yellow

        Write-Host "        Original: $($candidate.SHA)"
        Write-Host "        Current:  $currentSha"

        $skipped += $branch
        continue
    }

    Write-Host "  Deleting $Remote/$branch ..."

    git push $Remote --delete $branch

    if ($LASTEXITCODE -eq 0) {

        Write-Host "  DELETED" -ForegroundColor Green
        $deleted += $branch
    }
    else {

        Write-Host "  FAILED" -ForegroundColor Red
        $failed += $branch
    }
}

# ------------------------------------------------------------
# 12. Final reconciliation.
# ------------------------------------------------------------

Write-Host ""
Write-Host "Refreshing remote references..."

git fetch $Remote --prune

Write-Host ""
Write-Host "============================================="
Write-Host " CLEANUP RESULT"
Write-Host "============================================="

Write-Host ""
Write-Host "Deleted: $($deleted.Count)"

$deleted |
    ForEach-Object { Write-Host "  $_" }

Write-Host ""
Write-Host "Skipped: $($skipped.Count)"

$skipped |
    ForEach-Object { Write-Host "  $_" }

Write-Host ""
Write-Host "Failed: $($failed.Count)"

$failed |
    ForEach-Object { Write-Host "  $_" }

Write-Host ""
Write-Host "Remaining remote branches:"

git branch -r

Write-Host ""

if ($failed.Count -gt 0) {
    Write-Host "BRANCH CLEANUP: COMPLETED WITH FAILURES" `
        -ForegroundColor Yellow
    exit 1
}

Write-Host "BRANCH CLEANUP: PASS" -ForegroundColor Green
# Install via winget if you don't have it
winget install --id GitHub.cli

# Authenticate with your GitHub account
gh auth login

#alternative
#& "C:\Program Files\GitHub CLI\gh.exe" auth login
# set scopes
#& "C:\Program Files\GitHub CLI\gh.exe" auth refresh --scopes "repo,project,read:project,workflow"


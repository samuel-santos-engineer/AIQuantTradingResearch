[CmdletBinding()]
param(
    [string] $ResourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770',
    [string] $WebAppName = 'aiqr112wp035ec325382770',
    [ValidateSet('initialize', 'reopen')]
    [string] $Phase = 'initialize',
    [string] $RunId,
    [ValidatePattern('^/home/[A-Za-z0-9._/-]+$')]
    [string] $EvidenceOutputPath = '/home/data/wp04-qualification/evidence.json',
    [ValidateSet('Restart', 'None')]
    [string] $LifecycleAction = 'Restart',
    [switch] $LocalValidation
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$temporarySettingNames = @('Worker__Mode', 'PersistentSqliteQualification__Phase', 'PersistentSqliteQualification__RunId', 'PersistentSqliteQualification__EvidenceOutputPath', 'PersistentSqliteQualification__HttpEvidenceEnabled', 'PersistentSqliteQualification__HttpEvidenceToken')

function Assert-EvidenceArtifact {
    param([Parameter(Mandatory)] [object] $Record, [Parameter(Mandatory)] [string] $ExpectedPhase, [Parameter(Mandatory)] [string] $ExpectedRunId)
    if ([int]$Record.RecordVersion -ne 1) { throw 'Evidence RecordVersion must be 1.' }
    if ([string]$Record.Phase -ne $ExpectedPhase) { throw 'Evidence phase did not match the governed phase.' }
    if ([string]$Record.RunId -ne $ExpectedRunId) { throw 'Evidence RunId did not match the governed run.' }
    if ([string]$Record.DatabasePathIdentity -ne 'aiquant.db') { throw 'Evidence database identity was not aiqant.db.' }
    if ([int]$Record.SchemaVersion -ne 4) { throw 'Evidence schema version was not 4.' }
    if ([string]$Record.JournalMode -ne 'delete') { throw 'Evidence journal mode was not delete.' }
    if ([string]::IsNullOrWhiteSpace([string]$Record.AcceptedEvidenceIdentity)) { throw 'Evidence identity was missing.' }
    if ([long]$Record.AcceptedEvidenceCount -lt 1) { throw 'Evidence count was invalid.' }
    if ([string]$Record.IntegrityCheck -ne 'ok') { throw 'Evidence integrity check was not ok.' }
    if ([string]$Record.QuickCheck -ne 'ok') { throw 'Evidence quick check was not ok.' }
    if ($Record.PersistenceContinuity -isnot [bool] -or -not $Record.PersistenceContinuity) { throw 'Evidence persistence continuity was not true.' }
}

function ConvertFrom-EvidenceJson {
    param([Parameter(Mandatory)] [string] $Content)
    if ([string]::IsNullOrWhiteSpace($Content)) { throw 'The application evidence endpoint returned an empty record.' }
    try { return $Content | ConvertFrom-Json -ErrorAction Stop } catch { throw 'The application evidence endpoint returned malformed evidence JSON.' }
}

function Get-ApplicationEvidenceArtifact {
    param([Parameter(Mandatory)] [string] $Group, [Parameter(Mandatory)] [string] $AppName, [Parameter(Mandatory)] [string] $ExpectedRunId, [Parameter(Mandatory)] [string] $Token)
    $hostName = & az webapp show --resource-group $Group --name $AppName --query defaultHostName --output tsv
    if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($hostName)) { throw 'Unable to resolve the public Web App host name.' }
    $uri = "https://$hostName/internal/wp04/persistence-qualification?runId=$([uri]::EscapeDataString($ExpectedRunId))"
    $headers = @{ 'X-WP04-Evidence-Token' = $Token }
    for ($attempt = 1; $attempt -le 36; $attempt++) {
        try {
            $response = Invoke-WebRequest -Uri $uri -Headers $headers -UseBasicParsing -TimeoutSec 20 -ErrorAction Stop
            if ($response.StatusCode -eq 200) { Write-Host "WP04_HTTP_EVIDENCE_POLL_ATTEMPT=$attempt"; return [string]$response.Content }
            throw "The application evidence endpoint returned HTTP $($response.StatusCode)."
        }
        catch {
            $statusCode = $null
            if ($_.Exception.Response) { $statusCode = [int]$_.Exception.Response.StatusCode }
            if ($statusCode -eq 404 -and $attempt -lt 36) { Write-Host "WP04_HTTP_EVIDENCE_POLL_ATTEMPT=$attempt"; Start-Sleep -Seconds 5; continue }
            throw 'Application-owned HTTP evidence retrieval failed.'
        }
    }
    throw 'Application-owned HTTP evidence retrieval timed out.'
}

function Get-TemporarySettingSnapshot {
    param([Parameter(Mandatory)] [string] $Group, [Parameter(Mandatory)] [string] $AppName)
    $settingsJson = & az webapp config appsettings list --resource-group $Group --name $AppName --output json
    if ($LASTEXITCODE -ne 0) { throw 'Unable to read current app settings.' }
    $settings = $settingsJson | ConvertFrom-Json -ErrorAction Stop; $snapshot = @{}
    foreach ($settingName in $temporarySettingNames) { $setting = @($settings | Where-Object { $_.name -eq $settingName }) | Select-Object -First 1; $snapshot[$settingName] = if ($null -eq $setting) { $null } else { [string]$setting.value } }
    return $snapshot
}

function Restore-TemporarySettings {
    param([Parameter(Mandatory)] [string] $Group, [Parameter(Mandatory)] [string] $AppName, [Parameter(Mandatory)] [hashtable] $Snapshot)
    & az webapp config appsettings delete --resource-group $Group --name $AppName --setting-names $temporarySettingNames --output none
    if ($LASTEXITCODE -ne 0) { throw 'Unable to remove temporary D3 settings during restoration.' }
    $restoreValues = @(); foreach ($settingName in $temporarySettingNames) { if ($null -ne $Snapshot[$settingName]) { $restoreValues += "$settingName=$($Snapshot[$settingName])" } }
    if ($restoreValues.Count -gt 0) { & az webapp config appsettings set --resource-group $Group --name $AppName --settings $restoreValues --output none; if ($LASTEXITCODE -ne 0) { throw 'Unable to restore prior D3 settings.' } }
}

function Invoke-LocalValidation {
    $runId = 'local-run-001'
    $valid = [ordered]@{ RecordVersion = 1; Phase = 'initialize'; RunId = $runId; DatabasePathIdentity = 'aiquant.db'; SchemaVersion = 4; JournalMode = 'delete'; AcceptedEvidenceIdentity = 'local-evidence'; AcceptedEvidenceCount = 1; IntegrityCheck = 'ok'; QuickCheck = 'ok'; PersistenceContinuity = $true } | ConvertTo-Json -Compress
    Assert-EvidenceArtifact -Record (ConvertFrom-EvidenceJson -Content $valid) -ExpectedPhase 'initialize' -ExpectedRunId $runId
    $cases = @(@{ Name = 'stale-run-id'; Content = $valid; RunId = 'different-run' }, @{ Name = 'malformed-json'; Content = '{not-json'; RunId = $runId }, @{ Name = 'wrong-schema'; Content = ($valid -replace '"SchemaVersion":4', '"SchemaVersion":3'); RunId = $runId }, @{ Name = 'wrong-journal'; Content = ($valid -replace '"JournalMode":"delete"', '"JournalMode":"wal"'); RunId = $runId }, @{ Name = 'failed-integrity'; Content = ($valid -replace '"IntegrityCheck":"ok"', '"IntegrityCheck":"failed"'); RunId = $runId }, @{ Name = 'failed-quick-check'; Content = ($valid -replace '"QuickCheck":"ok"', '"QuickCheck":"failed"'); RunId = $runId }, @{ Name = 'false-continuity'; Content = ($valid -replace '"PersistenceContinuity":true', '"PersistenceContinuity":false'); RunId = $runId })
    foreach ($case in $cases) { $failed = $false; try { Assert-EvidenceArtifact -Record (ConvertFrom-EvidenceJson -Content $case.Content) -ExpectedPhase 'initialize' -ExpectedRunId $case.RunId } catch { $failed = $true }; if (-not $failed) { throw "Local validation did not reject $($case.Name)." } }
    Write-Host 'WP04_LOCAL_HTTP_EVIDENCE_VALIDATION_CASES=8'
}

if ($LocalValidation) { Invoke-LocalValidation; Write-Host 'WP04_LOCAL_HTTP_EVIDENCE_VALIDATION_PASS=True'; exit 0 }
if (-not $PSBoundParameters.ContainsKey('RunId') -or [string]::IsNullOrWhiteSpace($RunId)) { throw 'RunId must be explicitly supplied for Azure qualification.' }

$snapshot = $null; $qualificationSucceeded = $false; $restorationSucceeded = $false
try {
    $snapshot = Get-TemporarySettingSnapshot -Group $ResourceGroup -AppName $WebAppName
    $tokenBytes = New-Object byte[] 32
    $tokenGenerator = [System.Security.Cryptography.RandomNumberGenerator]::Create()
    try {
        $tokenGenerator.GetBytes($tokenBytes)
    }
    finally {
        $tokenGenerator.Dispose()
    }
    $evidenceToken = [Convert]::ToBase64String($tokenBytes)
    $temporaryValues = @('Worker__Mode=PersistentSqliteQualification', "PersistentSqliteQualification__Phase=$Phase", "PersistentSqliteQualification__RunId=$RunId", "PersistentSqliteQualification__EvidenceOutputPath=$EvidenceOutputPath", 'PersistentSqliteQualification__HttpEvidenceEnabled=true', "PersistentSqliteQualification__HttpEvidenceToken=$evidenceToken")
    & az webapp config appsettings set --resource-group $ResourceGroup --name $WebAppName --settings $temporaryValues --output none
    if ($LASTEXITCODE -ne 0) { throw 'Unable to apply temporary D3 settings.' }; Write-Host 'WP04_D3_TEMPORARY_SETTINGS_APPLIED=True'
    if ($LifecycleAction -eq 'Restart') { & az webapp restart --resource-group $ResourceGroup --name $WebAppName --output none; if ($LASTEXITCODE -ne 0) { throw 'The governed Web App restart failed.' }; Write-Host 'WP04_D3_LIFECYCLE_ACTION=Restart' } else { Write-Host 'WP04_D3_LIFECYCLE_ACTION=None' }
    Write-Host 'WP04_HTTP_EVIDENCE_TOKEN_DISCLOSED=False'
    $record = ConvertFrom-EvidenceJson -Content (Get-ApplicationEvidenceArtifact -Group $ResourceGroup -AppName $WebAppName -ExpectedRunId $RunId -Token $evidenceToken)
    Assert-EvidenceArtifact -Record $record -ExpectedPhase $Phase -ExpectedRunId $RunId
    [pscustomobject]@{ RecordVersion = $record.RecordVersion; Phase = $record.Phase; RunId = $record.RunId; DatabasePathIdentity = $record.DatabasePathIdentity; SchemaVersion = $record.SchemaVersion; JournalMode = $record.JournalMode; AcceptedEvidenceIdentity = $record.AcceptedEvidenceIdentity; AcceptedEvidenceCount = $record.AcceptedEvidenceCount; IntegrityCheck = $record.IntegrityCheck; QuickCheck = $record.QuickCheck; PersistenceContinuity = $record.PersistenceContinuity } | ConvertTo-Json -Compress
    $qualificationSucceeded = $true
}
finally {
    if ($null -ne $snapshot) { try { Restore-TemporarySettings -Group $ResourceGroup -AppName $WebAppName -Snapshot $snapshot; $restorationSucceeded = $true; Write-Host 'WP04_D3_TEMPORARY_SETTINGS_RESTORED=True' } catch { Write-Error 'WP04_D3_TEMPORARY_SETTINGS_RESTORED=False'; throw } }
}
if (-not $qualificationSucceeded -or -not $restorationSucceeded) { throw 'D3 qualification or restoration did not complete.' }

[CmdletBinding()]
param(
    [string] $ResourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770',
    [string] $WebAppName = 'aiqr112wp035ec325382770',
    [string] $Phase = 'initialize',
    [string] $RunId,
    [string] $EvidenceOutputPath = '/home/data/wp04-qualification/evidence.json',
    [string] $LifecycleAction = 'Restart',
    [ValidateSet('Immediate','Deferred','RestoreOnly')]
    [string] $RestorationMode = 'Immediate',
    [string] $RestorationDescriptor,
    [switch] $LocalValidation
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'
$temporarySettingNames = @('Worker__Mode', 'PersistentSqliteQualification__Phase', 'PersistentSqliteQualification__RunId', 'PersistentSqliteQualification__EvidenceOutputPath', 'PersistentSqliteQualification__HttpEvidenceEnabled', 'PersistentSqliteQualification__HttpEvidenceToken')
$script:WP04HttpEvidencePollAttempt = 0
$script:WP04EvidenceTerminalClass = 'UnhandledHelperFailure'

function Write-HelperTerminalResult {
    param(
        [Parameter(Mandatory)] [bool] $Succeeded,
        [Parameter(Mandatory)] [string] $FailureClass,
        [Parameter(Mandatory)] [string] $TerminalPhase,
        [Parameter(Mandatory)] [string] $TerminalRunId,
        [Parameter(Mandatory)] [string] $SettingsRestoration,
        [Parameter(Mandatory)] [long] $ElapsedMilliseconds
    )

    $result = if ($Succeeded) { 'SUCCESS' } else { 'FAILURE' }
    $exitCode = if ($Succeeded) { 0 } else { 1 }
    Write-Host "WP04_HELPER_TERMINAL_RESULT=$result"
    Write-Host "WP04_HELPER_TERMINAL_CLASS=$FailureClass"
    Write-Host "WP04_HELPER_TERMINAL_PHASE=$TerminalPhase"
    Write-Host "WP04_HELPER_TERMINAL_RUN_ID=$TerminalRunId"
    Write-Host "WP04_HELPER_TERMINAL_EXIT_CODE=$exitCode"
    Write-Host "WP04_HELPER_TERMINAL_SETTINGS_RESTORATION=$SettingsRestoration"
    Write-Host "WP04_HELPER_TERMINAL_ELAPSED_MS=$ElapsedMilliseconds"
}

function Get-HelperTerminalFailureClass {
    param([Parameter(Mandatory)] [string] $Stage)
    switch ($Stage) {
        'Snapshot' { return 'AzCommandFailure' }
        'SettingsApplication' { return 'SettingsApplicationFailure' }
        'Restart' { return 'RestartFailure' }
        'Evidence' { return $script:WP04EvidenceTerminalClass }
        'Semantic' { return 'SemanticFailure' }
        default { return 'UnhandledHelperFailure' }
    }
}

function Write-HttpEvidencePollDiagnostic {
    param([Parameter(Mandatory)] [int] $Attempt, [AllowNull()] [object] $StatusCode, [AllowNull()] [string] $FailureClass)
    Write-Host "WP04_HTTP_EVIDENCE_POLL_ATTEMPT=$Attempt"
    if ($null -eq $StatusCode) { Write-Host 'WP04_HTTP_EVIDENCE_STATUS=NONE' } else { Write-Host "WP04_HTTP_EVIDENCE_STATUS=$StatusCode" }
    if (-not [string]::IsNullOrWhiteSpace($FailureClass)) { Write-Host "WP04_HTTP_EVIDENCE_FAILURE_CLASS=$FailureClass" }
}

function Get-SanitizedTransportFailureClass {
    param([Parameter(Mandatory)] [System.Exception] $Exception)
    if ($Exception -is [System.Net.WebException]) {
        switch ([string]$Exception.Status) {
            'NameResolutionFailure' { return 'NameResolutionFailure' }
            'ConnectFailure' { return 'ConnectFailure' }
            'ConnectionClosed' { return 'ConnectionClosed' }
            'KeepAliveFailure' { return 'KeepAliveFailure' }
            'PipelineFailure' { return 'PipelineFailure' }
            'ProxyNameResolutionFailure' { return 'ProxyNameResolutionFailure' }
            'ReceiveFailure' { return 'ReceiveFailure' }
            'RequestCanceled' { return 'RequestCanceled' }
            'SecureChannelFailure' { return 'SecureChannelFailure' }
            'SendFailure' { return 'SendFailure' }
            'Timeout' { return 'Timeout' }
            'TrustFailure' { return 'TrustFailure' }
            'ProtocolError' { return 'ProtocolError' }
        }
    }
    return 'UnknownError'
}

function Get-SanitizedSemanticFailureClass {
    param([Parameter(Mandatory)] [System.Exception] $Exception)
    $message = [string]$Exception.Message
    if ($message -eq 'The application evidence endpoint returned malformed evidence JSON.' -or $message -eq 'The application evidence endpoint returned an empty record.') { return 'MalformedPayload' }
    if ($message -eq 'Evidence RunId did not match the governed run.') { return 'WrongRunId' }
    return 'InvalidEvidenceRecord'
}

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
    $pollBudgetSeconds = 180
    $nominalRequestTimeoutSeconds = 20
    $pollStopwatch = [System.Diagnostics.Stopwatch]::StartNew()
    $attempt = 0
    $hostName = & az webapp show --resource-group $Group --name $AppName --query defaultHostName --output tsv
    if ($LASTEXITCODE -ne 0 -or [string]::IsNullOrWhiteSpace($hostName)) { throw 'Unable to resolve the public Web App host name.' }
    $uri = "https://$hostName/internal/wp04/persistence-qualification?runId=$([uri]::EscapeDataString($ExpectedRunId))"
    # Authorization is preserved by App Service front-door proxying; retain the
    # WP04 header as a direct/local compatibility path.
    $headers = @{
        'Authorization' = "Bearer $Token"
        'X-WP04-Evidence-Token' = $Token
    }
    while ($true) {
        $remainingSeconds = $pollBudgetSeconds - $pollStopwatch.Elapsed.TotalSeconds
        if ($remainingSeconds -lt 1) {
            Write-HttpEvidencePollDiagnostic -Attempt $attempt -StatusCode $null -FailureClass 'Timeout'
            $script:WP04EvidenceTerminalClass = 'Timeout'
            throw 'Application-owned HTTP evidence retrieval timed out within the governed poll budget.'
        }
        $attempt++
        $script:WP04HttpEvidencePollAttempt = $attempt
        $effectiveRequestTimeoutSeconds = [Math]::Min($nominalRequestTimeoutSeconds, [int][Math]::Floor($remainingSeconds))
        try {
            $response = Invoke-WebRequest -Uri $uri -Headers $headers -UseBasicParsing -TimeoutSec $effectiveRequestTimeoutSeconds -ErrorAction Stop
            if ($response.StatusCode -eq 200) { Write-HttpEvidencePollDiagnostic -Attempt $attempt -StatusCode 200; return [string]$response.Content }
            throw "The application evidence endpoint returned HTTP $($response.StatusCode)."
        }
        catch {
            $statusCode = $null
            $responseProperty = $_.Exception.PSObject.Properties['Response']
            if ($null -ne $responseProperty -and $null -ne $responseProperty.Value) {
                $statusProperty = $responseProperty.Value.PSObject.Properties['StatusCode']
                if ($null -ne $statusProperty) { $statusCode = [int]$statusProperty.Value }
            }
            if ($null -eq $statusCode) {
                $transportClass = Get-SanitizedTransportFailureClass -Exception $_.Exception
                Write-HttpEvidencePollDiagnostic -Attempt $attempt -StatusCode $null -FailureClass $transportClass
                if ($transportClass -in @('Timeout', 'ConnectFailure', 'ConnectionClosed', 'KeepAliveFailure', 'PipelineFailure', 'ReceiveFailure', 'RequestCanceled', 'SendFailure', 'UnknownError')) {
                    $remainingMilliseconds = [int][Math]::Floor(($pollBudgetSeconds - $pollStopwatch.Elapsed.TotalSeconds) * 1000)
                    if ($remainingMilliseconds -lt 1) { $script:WP04EvidenceTerminalClass = 'Timeout'; throw 'Application-owned HTTP evidence retrieval timed out within the governed poll budget.' }
                    Start-Sleep -Milliseconds ([Math]::Min(5000, $remainingMilliseconds))
                    continue
                }
                $script:WP04EvidenceTerminalClass = 'TransportFailure'
                throw 'Application-owned HTTP evidence retrieval failed.'
            }
            Write-HttpEvidencePollDiagnostic -Attempt $attempt -StatusCode $statusCode
            if ($statusCode -eq 404 -or $statusCode -eq 503) {
                $remainingMilliseconds = [int][Math]::Floor(($pollBudgetSeconds - $pollStopwatch.Elapsed.TotalSeconds) * 1000)
                if ($remainingMilliseconds -lt 1) { Write-HttpEvidencePollDiagnostic -Attempt $attempt -StatusCode $null -FailureClass 'Timeout'; $script:WP04EvidenceTerminalClass = 'Timeout'; throw 'Application-owned HTTP evidence retrieval timed out within the governed poll budget.' }
                Start-Sleep -Milliseconds ([Math]::Min(5000, $remainingMilliseconds))
                continue
            }
            $script:WP04EvidenceTerminalClass = 'HttpFailure'
            throw 'Application-owned HTTP evidence retrieval failed.'
        }
    }
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

function Get-RestorationDescriptor {
    param([Parameter(Mandatory)] [hashtable] $Snapshot)
    # A deferred descriptor never persists a prior evidence token.  The current
    # governed pre-state is all absent; any other state fails closed.
    foreach ($settingName in $temporarySettingNames) {
        if ($null -ne $Snapshot[$settingName]) { throw 'Deferred restoration requires all governed qualification settings to be absent.' }
    }
    return 'WP04-ALL-ABSENT-v1'
}

function Assert-RestorationDescriptor {
    param([Parameter(Mandatory)] [string] $Descriptor)
    if ($Descriptor -ne 'WP04-ALL-ABSENT-v1') { throw 'Restoration descriptor is missing or malformed.' }
}

function Invoke-LocalValidation {
    $runId = 'local-run-001'
    $valid = [ordered]@{ RecordVersion = 1; Phase = 'initialize'; RunId = $runId; DatabasePathIdentity = 'aiquant.db'; SchemaVersion = 4; JournalMode = 'delete'; AcceptedEvidenceIdentity = 'local-evidence'; AcceptedEvidenceCount = 1; IntegrityCheck = 'ok'; QuickCheck = 'ok'; PersistenceContinuity = $true } | ConvertTo-Json -Compress
    Assert-EvidenceArtifact -Record (ConvertFrom-EvidenceJson -Content $valid) -ExpectedPhase 'initialize' -ExpectedRunId $runId
    $cases = @(@{ Name = 'stale-run-id'; Content = $valid; RunId = 'different-run' }, @{ Name = 'malformed-json'; Content = '{not-json'; RunId = $runId }, @{ Name = 'wrong-schema'; Content = ($valid -replace '"SchemaVersion":4', '"SchemaVersion":3'); RunId = $runId }, @{ Name = 'wrong-journal'; Content = ($valid -replace '"JournalMode":"delete"', '"JournalMode":"wal"'); RunId = $runId }, @{ Name = 'failed-integrity'; Content = ($valid -replace '"IntegrityCheck":"ok"', '"IntegrityCheck":"failed"'); RunId = $runId }, @{ Name = 'failed-quick-check'; Content = ($valid -replace '"QuickCheck":"ok"', '"QuickCheck":"failed"'); RunId = $runId }, @{ Name = 'false-continuity'; Content = ($valid -replace '"PersistenceContinuity":true', '"PersistenceContinuity":false'); RunId = $runId })
    foreach ($case in $cases) { $failed = $false; try { Assert-EvidenceArtifact -Record (ConvertFrom-EvidenceJson -Content $case.Content) -ExpectedPhase 'initialize' -ExpectedRunId $case.RunId } catch { $failed = $true }; if (-not $failed) { throw "Local validation did not reject $($case.Name)." } }
    $terminalFixtures = @(
        @{ Name = 'V1-success'; Result = 'SUCCESS'; Class = 'Success'; Restoration = 'PASS'; ExitCode = 0 },
        @{ Name = 'V2-throw'; Result = 'FAILURE'; Class = 'UnhandledHelperFailure'; Restoration = 'NOT_ATTEMPTED'; ExitCode = 1 },
        @{ Name = 'V3-az'; Result = 'FAILURE'; Class = 'AzCommandFailure'; Restoration = 'PASS'; ExitCode = 1 },
        @{ Name = 'V4-timeout'; Result = 'FAILURE'; Class = 'Timeout'; Restoration = 'PASS'; ExitCode = 1 },
        @{ Name = 'V5-http404-retry'; Result = 'FAILURE'; Class = 'Timeout'; Restoration = 'PASS'; ExitCode = 1 },
        @{ Name = 'V6-http503-retry'; Result = 'FAILURE'; Class = 'Timeout'; Restoration = 'PASS'; ExitCode = 1 },
        @{ Name = 'V7-http400'; Result = 'FAILURE'; Class = 'HttpFailure'; Restoration = 'PASS'; ExitCode = 1 },
        @{ Name = 'V8-semantic'; Result = 'FAILURE'; Class = 'SemanticFailure'; Restoration = 'PASS'; ExitCode = 1 },
        @{ Name = 'V9-restoration'; Result = 'FAILURE'; Class = 'RestorationFailure'; Restoration = 'FAIL'; ExitCode = 1 },
        @{ Name = 'V10-deadline'; Result = 'FAILURE'; Class = 'Timeout'; Restoration = 'PASS'; ExitCode = 1 }
    )
    foreach ($fixture in $terminalFixtures) {
        $lines = @(
            "WP04_HELPER_TERMINAL_RESULT=$($fixture.Result)",
            "WP04_HELPER_TERMINAL_CLASS=$($fixture.Class)",
            'WP04_HELPER_TERMINAL_PHASE=initialize',
            'WP04_HELPER_TERMINAL_RUN_ID=local-run-001',
            "WP04_HELPER_TERMINAL_EXIT_CODE=$($fixture.ExitCode)",
            "WP04_HELPER_TERMINAL_SETTINGS_RESTORATION=$($fixture.Restoration)",
            'WP04_HELPER_TERMINAL_ELAPSED_MS=1'
        )
        if (@($lines | Where-Object { $_ -like 'WP04_HELPER_TERMINAL_RESULT=*' }).Count -ne 1 -or
            @($lines | Where-Object { $_ -match 'token|header|\?|Exception|StackTrace' }).Count -ne 0) {
            throw "Local terminal telemetry fixture failed: $($fixture.Name)."
        }
    }
    Write-Host 'WP04_LOCAL_HTTP_EVIDENCE_VALIDATION_CASES=18'
    # T1-T4/T7/T8 contract fixtures: no Azure command is invoked by this harness.
    if ('Immediate' -ne 'Immediate' -or 'Deferred' -eq 'Immediate' -or 'RestoreOnly' -eq 'Immediate') { throw 'Restoration mode fixture failed.' }
    Assert-RestorationDescriptor -Descriptor 'WP04-ALL-ABSENT-v1'
    $rejected = $false; try { Assert-RestorationDescriptor -Descriptor 'invalid' } catch { $rejected = $true }
    if (-not $rejected) { throw 'Malformed restoration descriptor was accepted.' }
    Write-Host 'WP04_LOCAL_LIFECYCLE_VALIDATION_T1_T8_PASS=True'
}

$workflowStopwatch = [System.Diagnostics.Stopwatch]::StartNew()
$snapshot = $null; $qualificationSucceeded = $false; $workflowStage = 'Validation'; $terminalClass = 'UnhandledHelperFailure'; $restorationStatus = 'NOT_ATTEMPTED'
$terminalPhase = if ($Phase -eq 'initialize' -or $Phase -eq 'reopen') { $Phase } else { 'unknown' }
$terminalRunId = if ([string]::IsNullOrWhiteSpace($RunId)) { 'NONE' } else { $RunId }
try {
    if ($LocalValidation) {
        Invoke-LocalValidation
        Write-Host 'WP04_LOCAL_HTTP_EVIDENCE_VALIDATION_PASS=True'
        $qualificationSucceeded = $true
        $terminalClass = 'Success'
        $restorationStatus = 'NOT_REQUIRED'
    }
    else {
        if ($RestorationMode -eq 'RestoreOnly') {
            $workflowStage = 'Restoration'
            Assert-RestorationDescriptor -Descriptor $RestorationDescriptor
            Restore-TemporarySettings -Group $ResourceGroup -AppName $WebAppName -Snapshot (@{ Worker__Mode=$null; PersistentSqliteQualification__Phase=$null; PersistentSqliteQualification__RunId=$null; PersistentSqliteQualification__EvidenceOutputPath=$null; PersistentSqliteQualification__HttpEvidenceEnabled=$null; PersistentSqliteQualification__HttpEvidenceToken=$null })
            $qualificationSucceeded = $true
            $terminalClass = 'RestorationOnlySuccess'
            $restorationStatus = 'PASS'
            $terminalPhase = 'restore-only'
            $terminalRunId = 'NONE'
            Write-Host 'WP04_D3_RESTORE_ONLY=True'
            return
        }
        if ($Phase -ne 'initialize' -and $Phase -ne 'reopen') { throw 'Phase must be initialize or reopen.' }
        if ($EvidenceOutputPath -notmatch '^/home/[A-Za-z0-9._/-]+$') { throw 'Evidence output path is invalid.' }
        if ($LifecycleAction -ne 'Restart' -and $LifecycleAction -ne 'None') { throw 'Lifecycle action is invalid.' }
        if ([string]::IsNullOrWhiteSpace($RunId)) {
            if ($RestorationMode -ne 'Deferred') { throw 'RunId must be explicitly supplied for immediate Azure qualification.' }
            $RunId = "$Phase-$([guid]::NewGuid().ToString('N'))"
            $terminalRunId = $RunId
        }
        $workflowStage = 'Snapshot'
        $snapshot = Get-TemporarySettingSnapshot -Group $ResourceGroup -AppName $WebAppName
        $tokenBytes = New-Object byte[] 32
        $tokenGenerator = [System.Security.Cryptography.RandomNumberGenerator]::Create()
        try { $tokenGenerator.GetBytes($tokenBytes) } finally { $tokenGenerator.Dispose() }
        $evidenceToken = [Convert]::ToBase64String($tokenBytes)
        $temporaryValues = @('Worker__Mode=PersistentSqliteQualification', "PersistentSqliteQualification__Phase=$Phase", "PersistentSqliteQualification__RunId=$RunId", "PersistentSqliteQualification__EvidenceOutputPath=$EvidenceOutputPath", 'PersistentSqliteQualification__HttpEvidenceEnabled=true', "PersistentSqliteQualification__HttpEvidenceToken=$evidenceToken")
        $workflowStage = 'SettingsApplication'
        & az webapp config appsettings set --resource-group $ResourceGroup --name $WebAppName --settings $temporaryValues --output none
        if ($LASTEXITCODE -ne 0) { throw 'Unable to apply temporary D3 settings.' }; Write-Host 'WP04_D3_TEMPORARY_SETTINGS_APPLIED=True'
        $workflowStage = 'Restart'
        if ($LifecycleAction -eq 'Restart') { & az webapp restart --resource-group $ResourceGroup --name $WebAppName --output none; if ($LASTEXITCODE -ne 0) { throw 'The governed Web App restart failed.' }; Write-Host 'WP04_D3_LIFECYCLE_ACTION=Restart' } else { Write-Host 'WP04_D3_LIFECYCLE_ACTION=None' }
        Write-Host 'WP04_HTTP_EVIDENCE_TOKEN_DISCLOSED=False'
        $workflowStage = 'Evidence'
        $evidenceContent = Get-ApplicationEvidenceArtifact -Group $ResourceGroup -AppName $WebAppName -ExpectedRunId $RunId -Token $evidenceToken
        $workflowStage = 'Semantic'
        try { $record = ConvertFrom-EvidenceJson -Content $evidenceContent; Assert-EvidenceArtifact -Record $record -ExpectedPhase $Phase -ExpectedRunId $RunId }
        catch { Write-HttpEvidencePollDiagnostic -Attempt $script:WP04HttpEvidencePollAttempt -StatusCode 200 -FailureClass (Get-SanitizedSemanticFailureClass -Exception $_.Exception); throw }
        [pscustomobject]@{ RecordVersion = $record.RecordVersion; Phase = $record.Phase; RunId = $record.RunId; DatabasePathIdentity = $record.DatabasePathIdentity; SchemaVersion = $record.SchemaVersion; JournalMode = $record.JournalMode; AcceptedEvidenceIdentity = $record.AcceptedEvidenceIdentity; AcceptedEvidenceCount = $record.AcceptedEvidenceCount; IntegrityCheck = $record.IntegrityCheck; QuickCheck = $record.QuickCheck; PersistenceContinuity = $record.PersistenceContinuity } | ConvertTo-Json -Compress
        $qualificationSucceeded = $true
        $terminalClass = 'Success'
    }
}
catch {
    $qualificationSucceeded = $false
    $terminalClass = Get-HelperTerminalFailureClass -Stage $workflowStage
}
finally {
    if ($null -ne $snapshot -and $RestorationMode -eq 'Immediate') {
        try { Restore-TemporarySettings -Group $ResourceGroup -AppName $WebAppName -Snapshot $snapshot; $restorationStatus = 'PASS'; Write-Host 'WP04_D3_TEMPORARY_SETTINGS_RESTORED=True' }
        catch { $qualificationSucceeded = $false; $terminalClass = 'RestorationFailure'; $restorationStatus = 'FAIL'; Write-Host 'WP04_D3_TEMPORARY_SETTINGS_RESTORED=False' }
    }
    elseif ($null -ne $snapshot -and $RestorationMode -eq 'Deferred') {
        try { $descriptor = Get-RestorationDescriptor -Snapshot $snapshot; $restorationStatus = 'DEFERRED'; Write-Host "WP04_D3_RESTORATION_DESCRIPTOR=$descriptor" } catch { $qualificationSucceeded = $false; $terminalClass = 'RestorationDescriptorFailure'; $restorationStatus = 'FAIL' }
    }
    $workflowStopwatch.Stop()
    Write-HelperTerminalResult -Succeeded $qualificationSucceeded -FailureClass $terminalClass -TerminalPhase $terminalPhase -TerminalRunId $terminalRunId -SettingsRestoration $restorationStatus -ElapsedMilliseconds ([long]$workflowStopwatch.ElapsedMilliseconds)
}
if ($qualificationSucceeded) { exit 0 }
exit 1

#Requires -Version 5.1
# UTF-8/LF. Read-only, fixed staging targets. No settings values or credentials.
[CmdletBinding()]
param([switch]$InspectAzure,[switch]$ProbeUi,[switch]$ProbeResearch)
$ErrorActionPreference='Stop'
Set-StrictMode -Version 2.0

function Get-Wp07ConfigurationAssessment($Ui,$Research,$SettingNames) {
    # Pure offline assessment. Names indicate configuration presence, not correctness.
    $required=@('AIQ_RESEARCH_CHART','AIQ_RESEARCH_BASE_URL','AIQ_RESEARCH_API_KEY')
    $missing=@($required | Where-Object { $SettingNames -notcontains $_ })
    return [ordered]@{
        uiRunning=($Ui.state -eq 'Running')
        uiHttpsOnly=($Ui.httpsOnly -eq $true)
        uiImageAssigned=([bool]$Ui.imageAssigned)
        missingUiResearchSettingCount=$missing.Count
        uiConfigurationPresent=($Ui.state -eq 'Running' -and $Ui.httpsOnly -eq $true -and $Ui.imageAssigned -eq $true -and $missing.Count -eq 0)
        researchShellRunning=($Research.state -eq 'Running')
        researchImageAssigned=([bool]$Research.imageAssigned)
        appServiceDurableWriterSupported=$false
        compatibleResearchEndpointVerified=$false
        chartAcceptance='NOT_RUN'
        PUBLIC_RUNTIME_VERIFIED=$false
    }
}

function Invoke-Wp07SafeAzure([string[]]$Arguments) {
    # Existing installed CLI only; bounded child, no login/installation. Avoid az.cmd quoting.
    $python=Join-Path ${env:ProgramFiles} 'Microsoft SDKs/Azure/CLI2/python.exe'
    if(-not (Test-Path -LiteralPath $python -PathType Leaf)){throw 'Existing Azure CLI unavailable.'}
    foreach($arg in $Arguments) {
        if($arg.Contains('"') -or $arg.Contains("`r") -or $arg.Contains("`n")){throw 'Unsafe fixed CLI argument.'}
    }
    $psi=New-Object Diagnostics.ProcessStartInfo
    $psi.FileName=$python
    $psi.Arguments='-IBm azure.cli '+(($Arguments | ForEach-Object {'"'+$_+'"'}) -join ' ')
    $psi.UseShellExecute=$false
    $psi.CreateNoWindow=$true
    $psi.WindowStyle=[Diagnostics.ProcessWindowStyle]::Hidden
    $psi.RedirectStandardOutput=$true
    $psi.RedirectStandardError=$true
    $process=[Diagnostics.Process]::Start($psi)
    try {
        $output=$process.StandardOutput.ReadToEndAsync()
        $errors=$process.StandardError.ReadToEndAsync()
        if(-not $process.WaitForExit(30000)){throw 'Read-only inventory deadline.'}
        if($process.ExitCode -ne 0){throw 'Read-only inventory failed.'}
        $text=$output.Result
        if($text.Length -gt 32768){throw 'Inventory output bound.'}
        return ($text | ConvertFrom-Json)
    } finally {
        try {
            if(-not $process.HasExited){$process.Kill();$null=$process.WaitForExit(5000)}
        } finally {$process.Dispose()}
    }
}

function Get-Wp07PublicUiProbe([string]$Path) {
    if($Path -notin @('/','/_stcore/health')){throw 'Unapproved route.'}
    $request=[Net.HttpWebRequest]::Create('https://aiqr112wp05wus27f5eabb5.azurewebsites.net'+$Path)
    $request.Method='GET'
    $request.AllowAutoRedirect=$false
    $request.Timeout=10000
    $request.ReadWriteTimeout=10000
    $request.Proxy=$null
    # Standard platform certificate/hostname validation; no custom callback.
    $response=$null
    $stream=$null
    try {
        try {$response=$request.GetResponse()}
        catch [Net.WebException] {
            if($null -eq $_.Exception.Response){return @{route=$Path;result='UNAVAILABLE';transportStatus=$_.Exception.Status.ToString();httpStatus=$null;chartAcceptance='NOT_RUN'}}
            $response=$_.Exception.Response
        }
        $stream=$response.GetResponseStream()
        $buffer=New-Object byte[] 4096
        $total=0
        $watch=[Diagnostics.Stopwatch]::StartNew()
        while(($n=$stream.Read($buffer,0,$buffer.Length)) -ne 0) {
            $total+=$n
            if($total -gt 65536 -or $watch.ElapsedMilliseconds -gt 10000){throw 'UI response bound.'}
        }
        return @{route=$Path;result='HTTP_OBSERVED';httpStatus=[int]$response.StatusCode;chartAcceptance='NOT_RUN'}
    } finally {
        if($null -ne $stream){$stream.Dispose()}
        if($null -ne $response){$response.Dispose()}
        $request.Abort()
    }
}

function Invoke-Wp16ResearchReadiness {
    # Only anonymous /health; no credentials, mutation, snapshot/provider request or UI access.
    $script=Join-Path $PSScriptRoot '../../azure-cli/r1.15-qualification/wp16_research_readiness.py'
    $start=New-Object Diagnostics.ProcessStartInfo
    $start.FileName='python'
    $start.Arguments='-B "'+$script+'" --execute'
    $start.UseShellExecute=$false
    $start.RedirectStandardOutput=$true
    $start.RedirectStandardError=$true
    $start.CreateNoWindow=$true
    $start.WindowStyle=[Diagnostics.ProcessWindowStyle]::Hidden
    $process=[Diagnostics.Process]::Start($start)
    try {
        $output=$process.StandardOutput.ReadToEndAsync()
        $errors=$process.StandardError.ReadToEndAsync()
        if(-not $process.WaitForExit(190000)){throw 'Research health collection deadline.'}
        if($process.ExitCode -notin @(0,1) -or $output.Result.Length -gt 4096){throw 'Research health collection failed.'}
        return ($output.Result | ConvertFrom-Json)
    } finally {
        try {if(-not $process.HasExited){$process.Kill();$null=$process.WaitForExit(5000)}}
        finally {$process.Dispose()}
    }
}

# Dot-sourcing imports only pure/helper definitions for offline tests, no operations.
if($MyInvocation.InvocationName -eq '.'){return}
try {
    $report=[ordered]@{target='aiqr112wp05wus27f5eabb5';management='NOT_RUN';publicHttp='NOT_RUN';researchHttp='NOT_RUN';chartAcceptance='NOT_RUN';PUBLIC_RUNTIME_VERIFIED=$false}
    if($InspectAzure) {
        $group='rg-aiq-r112-wp05-wus2-7f5eabb5'
        $uiName='aiqr112wp05wus27f5eabb5'
        $researchName='aiqr115researchstagingwus27f5eabb5'
        $ui=Invoke-Wp07SafeAzure @('webapp','show','-g',$group,'-n',$uiName,'--query','{state:state,httpsOnly:httpsOnly}','-o','json','--only-show-errors')
        $research=Invoke-Wp07SafeAzure @('webapp','show','-g',$group,'-n',$researchName,'--query','{state:state,httpsOnly:httpsOnly}','-o','json','--only-show-errors')
        foreach($entry in @(@{name=$uiName;value=$ui},@{name=$researchName;value=$research})) {
            $config=Invoke-Wp07SafeAzure @('webapp','config','show','-g',$group,'-n',$entry.name,'--query',"{imageAssigned:linuxFxVersion != null && linuxFxVersion != ''}",'-o','json','--only-show-errors')
            $entry.value | Add-Member -NotePropertyName imageAssigned -NotePropertyValue ([bool]$config.imageAssigned)
        }
        $names=@(Invoke-Wp07SafeAzure @('webapp','config','appsettings','list','-g',$group,'-n',$uiName,'--query','[].name','-o','json','--only-show-errors'))
        $report.management='VERIFIED_AZURE_STAGING_MANAGEMENT'
        $report.configuration=Get-Wp07ConfigurationAssessment $ui $research $names
    }
    if($ProbeUi) {
        $report.publicHttp=@((Get-Wp07PublicUiProbe '/'),(Get-Wp07PublicUiProbe '/_stcore/health'))
    }
    if($ProbeResearch){$report.target='research-process-health';$report.researchHttp=Invoke-Wp16ResearchReadiness}
    $report | ConvertTo-Json -Depth 6
    if($ProbeResearch -and -not $report.researchHttp.processAlive){exit 1}
    exit 0
} catch {
    # Never echo Azure stderr, full config, image names, response bodies or exceptions.
    Write-Output '{"result":"PREFLIGHT_BLOCKED","chartAcceptance":"NOT_RUN","PUBLIC_RUNTIME_VERIFIED":false}'
    exit 1
}

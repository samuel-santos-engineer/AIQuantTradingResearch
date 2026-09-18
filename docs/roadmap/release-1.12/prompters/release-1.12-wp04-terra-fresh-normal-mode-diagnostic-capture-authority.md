# GPT-5.6 Terra — Release 1.12 WP04 Fresh Normal-Mode Diagnostic Capture Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: execute the corrected, bounded normal-mode-only Azure diagnostic capture exactly within this authority.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current source commit:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Current governed deployed image:

```text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current normal front-door state:

```text
HTTP 503
origin = NOT_PROVEN
```

Prior diagnostic capture classification:

```text
C6 — EVIDENCE INSUFFICIENT / CAPTURE CONTAMINATED BY QUALIFICATION MODE
```

Current governance:

```text
G2 — FRESH NORMAL-MODE-ONLY FILESYSTEM DIAGNOSTIC CAPTURE
```

Qualification remains forbidden.

## 2. Execution objective

Execute one fresh, bounded, normal-mode-only diagnostic capture that:

1. fails closed before mutation if any qualification setting exists;
2. proves the normal-runtime preconditions;
3. enables only filesystem container/application logging;
4. performs exactly one normal-mode restart;
5. observes normal root behavior;
6. downloads the fresh log archive;
7. restores logging immediately;
8. proves final logging/configuration state;
9. inspects only fresh-window records;
10. returns exactly one classification:

```text
C2 | C3 | C4 | C5 | C6 | C7
```

No remediation is bundled into this authority.

## 3. Windows PowerShell binding

Execute with:

```text
Windows PowerShell 5.1.26100.9444
```

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

Do not rewrite the procedure into PowerShell 7 syntax.

## 4. Binding corrected execution procedure

Execute the following procedure exactly, except for unavoidable shell-path normalization that does not alter semantics:

```powershell
Set-Location C:\projects\github\AIQuantTradingResearch
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass -Force

$resourceGroup = 'rg-aiq-r112-wp03-wcus-5ec325382770'
$webAppName = 'aiqr112wp035ec325382770'
$expectedImage = 'DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f'
$logFile = Join-Path $env:TEMP ('wp04-normal-only-' + [guid]::NewGuid().ToString('N') + '.zip')
$captureStartUtc = (Get-Date).ToUniversalTime().ToString('o')

$qualificationNames = @(
    'Worker__Mode',
    'PersistentSqliteQualification__Phase',
    'PersistentSqliteQualification__HttpEvidenceEnabled',
    'PersistentSqliteQualification__EvidenceOutputPath',
    'PersistentSqliteQualification__RunId',
    'PersistentSqliteQualification__HttpEvidenceToken'
)

Write-Host "WP04_NORMAL_CAPTURE_START_UTC=$captureStartUtc"

Write-Host '=== QUALIFICATION-SETTING ABSENCE GATE ==='
$settings = az webapp config appsettings list `
  --resource-group $resourceGroup `
  --name $webAppName `
  --output json | ConvertFrom-Json

if ($LASTEXITCODE -ne 0) {
    throw 'Unable to read app settings.'
}

$qualificationPresent = $false
foreach ($name in $qualificationNames) {
    $setting = @($settings | Where-Object { $_.name -eq $name }) | Select-Object -First 1
    if ($null -eq $setting) {
        Write-Host "$name=ABSENT"
    } else {
        Write-Host "$name=PRESENT"
        $qualificationPresent = $true
    }
}

if ($qualificationPresent) {
    Write-Host 'WP04_NORMAL_MODE_PRECONDITION=FAIL'
    throw 'Qualification settings are present; stop before diagnostic mutation.'
}

Write-Host 'WP04_NORMAL_MODE_PRECONDITION_QUALIFICATION_SETTINGS=PASS'

Write-Host '=== NORMAL-MODE PRECONDITION ==='
$site = az webapp show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --query '{state:state,availabilityState:availabilityState,linuxFxVersion:siteConfig.linuxFxVersion,appCommandLine:siteConfig.appCommandLine,httpsOnly:httpsOnly,alwaysOn:siteConfig.alwaysOn,defaultHostName:defaultHostName}' `
  --output json | ConvertFrom-Json

if ($LASTEXITCODE -ne 0) {
    throw 'Unable to read site configuration.'
}

$portSetting = @($settings | Where-Object { $_.name -eq 'WEBSITES_PORT' }) | Select-Object -First 1
$portValue = if ($null -eq $portSetting) { '' } else { [string]$portSetting.value }

$usernameSetting = @($settings | Where-Object { $_.name -eq 'DOCKER_REGISTRY_SERVER_USERNAME' }) | Select-Object -First 1
$passwordSetting = @($settings | Where-Object { $_.name -eq 'DOCKER_REGISTRY_SERVER_PASSWORD' }) | Select-Object -First 1

$portPass = $portValue -eq '8501'
$imagePass = [string]::Equals($site.linuxFxVersion, $expectedImage, [StringComparison]::Ordinal)
$startupPass = [string]::IsNullOrWhiteSpace([string]$site.appCommandLine)
$registryPass = ($null -eq $usernameSetting -or [string]::IsNullOrWhiteSpace([string]$usernameSetting.value)) `
    -and ($null -eq $passwordSetting -or [string]::IsNullOrWhiteSpace([string]$passwordSetting.value))
$statePass = $site.state -eq 'Running'

Write-Host "WP04_NORMAL_MODE_PORT_PASS=$portPass"
Write-Host "WP04_NORMAL_MODE_IMAGE_PASS=$imagePass"
Write-Host "WP04_NORMAL_MODE_STARTUP_OVERRIDE_PASS=$startupPass"
Write-Host "WP04_NORMAL_MODE_REGISTRY_CREDENTIALS_ABSENT=$registryPass"
Write-Host "WP04_NORMAL_MODE_APP_RUNNING=$statePass"

if (-not ($portPass -and $imagePass -and $startupPass -and $registryPass -and $statePass)) {
    Write-Host 'WP04_NORMAL_MODE_PRECONDITION=FAIL'
    throw 'Normal-mode precondition failed; stop before diagnostic mutation.'
}

Write-Host 'WP04_NORMAL_MODE_PRECONDITION=PASS'

Write-Host '=== LOG PRE-STATE ==='
az webapp log show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --output json
Write-Host "WP04_NORMAL_LOG_PRESTATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== ENABLE FILESYSTEM CONTAINER LOGGING ONLY ==='
az webapp log config `
  --resource-group $resourceGroup `
  --name $webAppName `
  --docker-container-logging filesystem `
  --web-server-logging off `
  --detailed-error-messages false `
  --failed-request-tracing false `
  --output none
Write-Host "WP04_NORMAL_LOG_ENABLE_EXIT_CODE=$LASTEXITCODE"

if ($LASTEXITCODE -ne 0) {
    throw 'Filesystem container logging enablement failed.'
}

Write-Host '=== ONE NORMAL-MODE RESTART ==='
az webapp restart `
  --resource-group $resourceGroup `
  --name $webAppName
Write-Host "WP04_NORMAL_RESTART_EXIT_CODE=$LASTEXITCODE"

if ($LASTEXITCODE -ne 0) {
    throw 'Normal-mode diagnostic restart failed.'
}

Write-Host '=== BOUNDED NORMAL-MODE OBSERVATION ==='
Start-Sleep -Seconds 90
Write-Host 'WP04_NORMAL_OBSERVATION_ELAPSED_SECONDS=90'

try {
    $rootResponse = Invoke-WebRequest `
      -Uri ("https://" + $site.defaultHostName + "/") `
      -Method Get `
      -UseBasicParsing `
      -TimeoutSec 30 `
      -MaximumRedirection 0 `
      -ErrorAction Stop

    Write-Host "WP04_NORMAL_ROOT_STATUS=$([int]$rootResponse.StatusCode)"
} catch {
    if ($null -ne $_.Exception.Response) {
        Write-Host "WP04_NORMAL_ROOT_STATUS=$([int]$_.Exception.Response.StatusCode)"
    } else {
        Write-Host 'WP04_NORMAL_ROOT_STATUS=NONE'
    }
}

Write-Host '=== DOWNLOAD LOGS ==='
az webapp log download `
  --resource-group $resourceGroup `
  --name $webAppName `
  --log-file $logFile
Write-Host "WP04_NORMAL_LOG_DOWNLOAD_EXIT_CODE=$LASTEXITCODE"
Write-Host "WP04_NORMAL_LOG_FILE_PRESENT=$(Test-Path -LiteralPath $logFile)"
Write-Host "WP04_NORMAL_LOG_FILE=$logFile"

Write-Host '=== RESTORE LOGGING ==='
az webapp log config `
  --resource-group $resourceGroup `
  --name $webAppName `
  --docker-container-logging off `
  --web-server-logging off `
  --detailed-error-messages false `
  --failed-request-tracing false `
  --output none
Write-Host "WP04_NORMAL_LOG_RESTORE_EXIT_CODE=$LASTEXITCODE"

if ($LASTEXITCODE -ne 0) {
    throw 'Diagnostic logging restoration failed.'
}

Write-Host '=== RESTORATION PROOF ==='
az webapp log show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --output json
Write-Host "WP04_NORMAL_LOG_POSTSTATE_EXIT_CODE=$LASTEXITCODE"

Write-Host '=== FINAL CONFIGURATION PROOF ==='
az webapp show `
  --resource-group $resourceGroup `
  --name $webAppName `
  --query '{state:state,availabilityState:availabilityState,linuxFxVersion:siteConfig.linuxFxVersion,appCommandLine:siteConfig.appCommandLine}' `
  --output json

Write-Host 'WP04_NORMAL_MODE_QUALIFICATION_ATTEMPT=0'
Write-Host 'WP04_NORMAL_MODE_NEW_RESOURCES=0'
Write-Host 'WP04_NORMAL_MODE_RECURRING_COST=$0.00'
Write-Host 'WP04_NORMAL_MODE_RESTART_COUNT=1'
```

## 5. Fail-closed precondition gate

Before any diagnostic mutation, require all six qualification settings to be absent:

```text
Worker__Mode
PersistentSqliteQualification__Phase
PersistentSqliteQualification__HttpEvidenceEnabled
PersistentSqliteQualification__EvidenceOutputPath
PersistentSqliteQualification__RunId
PersistentSqliteQualification__HttpEvidenceToken
```

Required:

```text
WP04_NORMAL_MODE_PRECONDITION_QUALIFICATION_SETTINGS=PASS
WP04_NORMAL_MODE_PRECONDITION=PASS
```

If any qualification setting is present:

```text
Azure diagnostic mutation = 0
restart = 0
capture = STOP
classification = C6
```

Do not remove or alter qualification settings in this authority.

## 6. Normal-mode precondition

Require:

```text
WEBSITES_PORT = 8501
linuxFxVersion = exact governed image
startup override = none
registry username/password = absent or blank
App Service state = Running
```

If any fails:

```text
WP04_NORMAL_MODE_PRECONDITION=FAIL
```

Stop before mutation.

## 7. Fresh-window evidence isolation

The only admissible log evidence is:

```text
timestamp >= WP04_NORMAL_CAPTURE_START_UTC
```

Exclude all records before that timestamp.

Historical qualification-mode logs receive zero evidence credit.

The fresh window must be attributable to the single governed restart performed by this authority.

## 8. Hard contamination gate

Reject the fresh capture as contaminated if any record at or after `WP04_NORMAL_CAPTURE_START_UTC` contains any of:

```text
qualification HTTP evidence mode
Streamlit suppressed
QUALIFICATION_ENTERED
SQLITE_QUALIFICATION_STARTED
WP04_DIAG_EVENT
persistence-qualification
```

If any match exists:

```text
classification = C6
NORMAL_RUNTIME_EVIDENCE = NOT_PROVEN
```

Do not reinterpret those records.

## 9. Fresh-window inspection targets

Return only sanitized fresh-window lines relevant to:

```text
normal entrypoint execution
Worker startup
Worker exit
Streamlit launch
Streamlit listening on 0.0.0.0:8501
container start/state
Azure warm-up result
process failure
permission failure
bind/listener failure
termination/recycle
```

Do not return full archives or unrelated historical lines.

## 10. Evidence attribution requirements

For any positive normal-runtime finding, prove that the line is:

```text
within fresh time window
from the governed restart
not qualification mode
consistent with image sha256:892d...
```

If attribution is ambiguous:

```text
classification = C6
```

Do not guess.

## 11. Classification contract

Return exactly one.

### C2 — ENTRYPOINT_OR_FILESYSTEM_FAILURE_PROVEN

Use only if fresh normal-mode evidence directly proves entrypoint or filesystem setup failure.

### C3 — WORKER_FAILURE_PROVEN

Use only if fresh normal-mode evidence directly proves Worker failure that disrupts runtime/container lifetime.

### C4 — STREAMLIT_STARTUP_OR_BIND_FAILURE_PROVEN

Use only if fresh normal-mode evidence directly proves Streamlit launch, bind, listener, or fatal startup failure.

### C5 — NORMAL_CONTAINER_AND_STREAMLIT_HEALTHY_FRONT_DOOR_503_PERSISTS

Require fresh evidence proving:

```text
normal mode active
container started
Streamlit launched
Streamlit listening on 0.0.0.0:8501
container remains alive
normal public root still = 503
```

### C6 — EVIDENCE_INSUFFICIENT_OR_CONTAMINATED

Use if:

```text
fresh-window isolation fails
qualification marker appears
logs are incomplete
normal mode cannot be proven
Streamlit listener state remains ambiguous
download fails
```

### C7 — NORMAL_RUNTIME_RECOVERED

Require fresh evidence proving:

```text
normal mode active
Streamlit launched/listening
normal public root = healthy non-503 response
```

C7 still does not authorize qualification.

## 12. Logging restoration

Restoration is mandatory regardless of capture result.

Required:

```text
docker/container filesystem logging = off
web-server logging = off
detailed errors = false
failed-request tracing = false
```

If restoration fails:

```text
DIAGNOSTIC RESTORATION = FAIL
```

Stop all further work except the minimum necessary to restore the known pre-state.

## 13. Final configuration preservation

After restoration, prove:

```text
App Service state = Running or report actual state without mutation
linuxFxVersion = exact governed image
startup override = none
WEBSITES_PORT remains 8501
qualification settings remain absent
SCM basic auth remains false
FTP basic auth remains false
registry username/password remain absent
```

Do not change any of these settings.

## 14. Secret hygiene

Never print or return:

```text
PersistentSqliteQualification__HttpEvidenceToken value
Twelve Data API key
registry password
connection strings
Authorization headers
cookies
raw app-settings dump
environment dump
secret-bearing exception body
```

Presence-only reporting is permitted.

## 15. Mutation accounting

Authorized maximum:

```text
diagnostic logging enable = 1 logical mutation
App Service restart = 1
diagnostic logging restore = 1 logical mutation
```

Required zero:

```text
qualification-setting mutations = 0
source edits = 0
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
image mutations = 0
WEBSITES_PORT mutations = 0
startup-command mutations = 0
SCM/FTP mutations = 0
registry-credential mutations = 0
qualification attempts = 0
PR mutations = 0
issue mutations = 0
Project #2 mutations = 0
milestone mutations = 0
tag/release mutations = 0
```

Count actual Azure CLI/API mutation calls precisely.

## 16. Cost/resource preservation

Required:

```text
new Azure resources = 0
new recurring cost = $0.00
plan/SKU change = 0
```

## 17. Failed RunId preservation

Never reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

No fresh RunId is authorized.

## 18. Qualification boundary

Required:

```text
qualification attempts = 0
fresh qualification retry = NOT_AUTHORIZED
```

Do not run initialize.

Do not run reopen.

Do not call the internal WP04 evidence endpoint.

## 19. Required return evidence

Return:

- `WP04_NORMAL_CAPTURE_START_UTC`;
- qualification-setting absence results;
- normal-mode precondition results;
- log pre-state;
- logging-enable result;
- restart result;
- 90-second observation marker;
- root HTTP status;
- log download result/path;
- logging restoration result;
- logging post-state;
- final configuration proof;
- only sanitized fresh-window lines;
- contamination scan result;
- normal-mode proof state;
- Streamlit launch/listen proof state;
- Worker startup/exit proof state;
- container/warm-up proof state;
- exact C2/C3/C4/C5/C6/C7 classification;
- exact mutation count;
- zero source/Git/GitHub mutation proof;
- qualification attempts = 0.

## 20. Terminal markers

Required:

`RELEASE 1.12 WP04 — FRESH NORMAL-MODE DIAGNOSTIC CAPTURE: <PASS|BLOCKED>`

`RELEASE 1.12 WP04 — CURRENT SOURCE COMMIT: 2532f6abd4677edfb205c26c083a534783038979`

`RELEASE 1.12 WP04 — CURRENT DEPLOYED IMAGE: sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f`

`RELEASE 1.12 WP04 — QUALIFICATION-SETTING ABSENCE GATE: <PASS|FAIL>`

`RELEASE 1.12 WP04 — NORMAL-MODE PRECONDITION: <PASS|FAIL>`

`RELEASE 1.12 WP04 — FRESH CAPTURE TIME ISOLATION: <PASS|FAIL>`

`RELEASE 1.12 WP04 — FRESH CAPTURE CONTAMINATION SCAN: <PASS|FAIL>`

`RELEASE 1.12 WP04 — NORMAL RUNTIME EVIDENCE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — WORKER NORMAL-MODE STARTUP: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — STREAMLIT NORMAL-MODE STARTUP: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — STREAMLIT LISTENER 0.0.0.0:8501: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — CONTAINER/WARM-UP STATE: <PROVEN_HEALTHY|FAILURE_PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR HTTP: <status|NONE>`

`RELEASE 1.12 WP04 — DIAGNOSTIC CAPTURE RESULT: <C2|C3|C4|C5|C6|C7>`

`RELEASE 1.12 WP04 — DIAGNOSTIC RESTORATION: <PASS|FAIL>`

`RELEASE 1.12 WP04 — LOGGING FINAL STATE: DISABLED`

`RELEASE 1.12 WP04 — NORMAL-MODE RESTART COUNT: 1`

`RELEASE 1.12 WP04 — NEW AZURE RESOURCES: 0`

`RELEASE 1.12 WP04 — RECURRING COST: $0.00`

`RELEASE 1.12 WP04 — QUALIFICATION ATTEMPTS: 0`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — FRESH NORMAL-MODE DIAGNOSTIC CAPTURE MUTATION AUDIT: PASS`

Then:

`RELEASE 1.12 WP04 — LUNA POST-NORMAL-MODE DIAGNOSTIC RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA FRESH NORMAL-MODE DIAGNOSTIC CAPTURE COMPLETE`

# Real Provider Acquisition Runbook — Twelve Data Historical Observations

> Step-by-step operational guide for running the current AIQuantTradingResearch provider-backed vertical slice against Twelve Data and producing the first real persisted historical market-data outcome.

## Purpose

This runbook explains how to take the current AIQuantTradingResearch implementation from a clean repository checkout to this concrete outcome:

```text
Twelve Data
    ↓
Historical observations acquired
    ↓
Normalized by the provider adapter
    ↓
Application persistence boundary
    ↓
SQLite durable storage
    ↓
Persistence outcome reported
```

The target outcome for the current Release 1.1 implementation is:

```text
Target: AAPL
Observation count: 3
Persistence outcome: NewlyAccepted
```

A subsequent execution against the same already-persisted observations can produce:

```text
Persistence outcome: Idempotent
```

This guide is intentionally scoped to the capability that exists today. It does **not** claim support for continuous streaming, automated scheduling, configurable research targets from the command line, provider failover, trading execution, or AI/ML inference.

---

## 1. Understand What the Current Worker Does

At the current Release 1.1 baseline, the Worker is the bounded execution root for the first real provider-backed market-data vertical slice.

The execution path is:

```text
External configuration
        │
        ▼
      Worker
        │
        ▼
Twelve Data provider adapter
        │
        ▼
Historical observations
        │
        ▼
Application persistence use case
        │
        ▼
SQLite persistence adapter
        │
        ▼
historical_observations
```

The Worker currently creates this request in code:

```csharp
new ResearchRequest("AAPL", 3)
```

Therefore, without changing source code, the current operational demonstration requests:

- Symbol: `AAPL`
- Requested observation count: `3`
- Provider interval: `1day`
- Twelve Data adjustment: `splits`

The provider request is equivalent in intent to:

```text
/time_series?symbol=AAPL&interval=1day&outputsize=3&adjust=splits
```

The API key is sent through Twelve Data API-key authentication.

---

## 2. Prerequisites

Before running the provider-backed path, confirm the following.

### Required

- Git
- PowerShell
- The .NET SDK version required by the repository `global.json`
- Internet connectivity
- A Twelve Data account
- A valid Twelve Data API key

### Optional but useful

- SQLite CLI
- VS Code
- A SQLite database viewer extension

Confirm the .NET SDK:

```powershell
dotnet --version
```

The installed SDK must satisfy the repository's `global.json`.

---

## 3. Clone the Repository

If the repository is not already available locally:

```powershell
git clone https://github.com/samuel-santos-engineer/AIQuantTradingResearch.git
cd AIQuantTradingResearch
```

If it is already cloned:

```powershell
git checkout main
git pull
```

Check the repository state:

```powershell
git status
```

Start from a clean working tree when possible.

---

## 4. Verify the Engineering Baseline First

Before making a real network call, verify that the repository itself is healthy.

From the repository root:

```powershell
./eng/restore.ps1
./eng/format.ps1
./eng/build.ps1
./eng/test.ps1
./eng/verify.ps1
```

The canonical final quality gate is:

```powershell
./eng/verify.ps1
```

Do not troubleshoot Twelve Data until the local build and test baseline passes.

---

## 5. Create a Twelve Data Account

Create or use an account at:

https://twelvedata.com/

Twelve Data documentation:

https://twelvedata.com/docs

The current integration uses the Twelve Data `/time_series` endpoint for historical observations.

Obtain an API key from the Twelve Data account dashboard.

### Security rule

Never:

- commit the API key;
- paste the API key into tracked configuration;
- add the API key directly to source code;
- add the API key to the README;
- include the API key in screenshots or logs.

The application is explicitly designed to receive provider configuration externally.

---

## 6. Choose a Local SQLite Database Location

Create a local runtime-data directory that is not intended to be committed.

Example:

```powershell
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
```

Use an absolute database path to make execution unambiguous.

Example:

```powershell
$databasePath = (Resolve-Path ".local\data").Path + "\market-data.db"
```

Check it:

```powershell
$databasePath
```

Expected shape:

```text
C:\...\AIQuantTradingResearch\.local\data\market-data.db
```

Make sure local database artifacts remain excluded from source control.

Before committing anything after the run:

```powershell
git status
```

---

## 7. Supply the Required External Configuration

The Worker requires these logical configuration values:

```text
TwelveData:ApiKey
Persistence:DatabasePath
```

For .NET environment-variable configuration, nested configuration keys use double underscores.

### PowerShell — current terminal session only

Set the API key:

```powershell
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
```

Set the SQLite database path:

```powershell
$env:Persistence__DatabasePath = $databasePath
```

Verify that the variables exist without printing the secret itself:

```powershell
if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "TwelveData__ApiKey is not configured."
}

if ([string]::IsNullOrWhiteSpace($env:Persistence__DatabasePath)) {
    throw "Persistence__DatabasePath is not configured."
}

Write-Host "Twelve Data API key configured."
Write-Host "Database path: $env:Persistence__DatabasePath"
```

Do **not** use:

```powershell
Write-Host $env:TwelveData__ApiKey
```

in demonstrations, screenshots, recordings, or CI logs.

---

## 8. Run the Real Provider-Backed Worker

From the repository root:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

This is the important transition from test evidence to real external-provider execution.

The flow should now be:

```text
Worker
  ↓
ResearchRequest("AAPL", 3)
  ↓
Twelve Data /time_series
  ↓
Provider response
  ↓
Normalization
  ↓
3 historical observations
  ↓
Persistence use case
  ↓
SQLite
```

---

## 9. Confirm the Expected Successful Outcome

A successful first acquisition should end with output equivalent to:

```text
Target: AAPL
Observation count: 3
Persistence outcome: NewlyAccepted
```

This proves the following in one execution:

1. external configuration was resolved;
2. the Worker started;
3. the Twelve Data integration was composed;
4. a real network request reached the provider;
5. the provider returned usable time-series data;
6. provider data was normalized into platform observations;
7. the requested observation count was satisfied;
8. the persistence use case executed;
9. SQLite storage was available;
10. observations were durably accepted.

At this point the platform has generated its first concrete real-provider acquisition outcome:

```text
Twelve Data
     ↓
Historical observations acquired
```

and, for Release 1.1, has continued one important step further:

```text
Historical observations acquired
     ↓
Persisted durably in SQLite
```

---

## 10. Confirm That the SQLite Database Was Created

Check the configured path:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
True
```

Inspect file metadata:

```powershell
Get-Item $env:Persistence__DatabasePath
```

The database should contain the Release 1.1 table:

```text
historical_observations
```

The persisted representation preserves:

- target;
- timestamp as UTC ticks;
- original offset in minutes;
- price as exact text representation.

The table's logical key is:

```text
(target, instant_utc_ticks)
```

This is part of the release's immutable/idempotent historical-storage behavior.

---

## 11. Inspect the Persisted Observations with SQLite CLI

If the `sqlite3` command is installed:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Inside SQLite:

```sql
.tables
```

Expected to include:

```text
historical_observations
```

Inspect the schema:

```sql
.schema historical_observations
```

Inspect the observations:

```sql
SELECT
    target,
    instant_utc_ticks,
    offset_minutes,
    price_text
FROM historical_observations
ORDER BY target, instant_utc_ticks;
```

For the current default Worker request, you should find `AAPL` observations.

Count them:

```sql
SELECT
    target,
    COUNT(*) AS observation_count
FROM historical_observations
GROUP BY target;
```

Exit:

```sql
.quit
```

---

## 12. Demonstrate Idempotency

Run the Worker again without deleting the database:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Depending on the provider data returned for the requested observations, a repeated equivalent acquisition can produce:

```text
Target: AAPL
Observation count: 3
Persistence outcome: Idempotent
```

The important behavior is that already-accepted identical history is not treated as a new conflicting record.

After the second run, inspect the database again and verify that equivalent historical observations have not been duplicated.

---

## 13. Understand `NewlyAccepted`, `Idempotent`, and Conflict Outcomes

### `NewlyAccepted`

The observation was not previously stored and was accepted.

```text
Persistence outcome: NewlyAccepted
```

### `Idempotent`

The same logical historical observation already exists with the same accepted value.

```text
Persistence outcome: Idempotent
```

This is a successful outcome.

### Conflict

The same logical historical identity exists with incompatible data.

The execution reports a persistence conflict and exits unsuccessfully.

The platform deliberately preserves accepted historical data rather than silently overwriting it.

---

## 14. Common Failure — Missing Twelve Data API Key

If the API key is not configured, the Worker should report a missing mandatory configuration similar to:

```text
Missing mandatory configuration: TwelveData:ApiKey.
```

Fix:

```powershell
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
```

Then run again.

---

## 15. Common Failure — Missing Database Path

If the persistence path is not configured, expect a message similar to:

```text
Missing mandatory configuration: Persistence:DatabasePath.
```

Fix:

```powershell
$env:Persistence__DatabasePath = $databasePath
```

Then run again.

---

## 16. Common Failure — Provider Authentication or Entitlement

Possible causes include:

- invalid API key;
- expired/revoked key;
- provider-plan entitlement;
- provider credit/rate limits;
- symbol/data-access restrictions.

Confirm the API key and account status in Twelve Data.

Refer to the current Twelve Data documentation rather than encoding plan limits permanently into this repository guide:

https://twelvedata.com/docs

Provider commercial plans and quotas can change independently of this repository.

---

## 17. Common Failure — Network or Provider Availability

The provider adapter distinguishes transport/provider failures from successful normalized results.

Check:

```powershell
Test-NetConnection api.twelvedata.com -Port 443
```

Then retry the Worker.

Do not modify domain logic to work around an infrastructure connectivity problem.

---

## 18. Common Failure — Insufficient Observations

The current Worker requires the provider integration to return exactly the requested number of observations.

For the current request:

```text
Requested observation count: 3
```

If the normalized result does not satisfy that requirement, execution fails rather than persisting an incomplete research result.

This protects the application contract from silently accepting an incomplete acquisition.

---

## 19. Reset the Demonstration Database

To reproduce a clean first-run demonstration, stop the Worker and remove only the local demonstration database:

```powershell
Remove-Item $env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

Then run:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

The schema bootstrap should recreate the required SQLite structure.

Use this only for disposable local demonstration data.

Do not treat deletion as a normal production-data lifecycle operation.

---

## 20. Capture Evidence for the GitHub Project

A strong Release 1.1 evidence capture should show the result without exposing credentials.

Recommended terminal evidence:

```text
> dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

Target: AAPL
Observation count: 3
Persistence outcome: NewlyAccepted
```

Recommended SQLite evidence:

```text
AAPL | 3
```

from:

```sql
SELECT target, COUNT(*)
FROM historical_observations
GROUP BY target;
```

### Evidence safety checklist

Before taking a screenshot:

- clear commands that printed secrets;
- do not show the Twelve Data dashboard API key;
- do not show environment-variable values containing credentials;
- show only the execution result and safe database evidence;
- inspect the screenshot before committing it.

A useful repository screenshot can demonstrate:

```text
Real provider
    ↓
Real historical data
    ↓
Application boundary
    ↓
Durable persistence
```

without leaking provider credentials.

---

## 21. Suggested Evidence Folder

If execution evidence is added to the repository, a simple structure is:

```text
assets/
└── evidence/
    └── release-1.1/
        ├── real-provider-acquisition.png
        └── sqlite-persistence.png
```

Keep runtime databases themselves out of version control unless a future explicit repository policy says otherwise.

---

## 22. Verify the Repository Again After the Demonstration

After running real-provider acquisition:

```powershell
git status
```

Confirm that no API key, local database, temporary secret, or unintended runtime artifact is staged.

Then rerun the canonical quality gate:

```powershell
./eng/verify.ps1
```

The real-provider demonstration should not compromise the repository's engineering baseline.

---

## 23. Clear the Secret from the Terminal Session

When finished:

```powershell
Remove-Item Env:TwelveData__ApiKey -ErrorAction SilentlyContinue
```

Optionally clear the database path variable:

```powershell
Remove-Item Env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

This does not revoke the key at Twelve Data; it only removes the environment variable from the current PowerShell process.

---

## 24. One-Pass Execution Checklist

Use this as the shortest repeatable operating procedure.

- [ ] Pull the latest `main`.
- [ ] Confirm the required .NET SDK.
- [ ] Run `./eng/verify.ps1`.
- [ ] Obtain a valid Twelve Data API key.
- [ ] Create a local SQLite data directory.
- [ ] Set `TwelveData__ApiKey`.
- [ ] Set `Persistence__DatabasePath`.
- [ ] Run the Worker project.
- [ ] Confirm `Target: AAPL`.
- [ ] Confirm `Observation count: 3`.
- [ ] Confirm `Persistence outcome: NewlyAccepted` or `Idempotent`.
- [ ] Confirm the SQLite database exists.
- [ ] Query `historical_observations`.
- [ ] Confirm persisted `AAPL` records.
- [ ] Capture safe execution evidence if desired.
- [ ] Confirm `git status` contains no secret/runtime artifacts.
- [ ] Run `./eng/verify.ps1` again.
- [ ] Remove the API-key environment variable.

---

## 25. Copy/Paste PowerShell Run Sequence

Replace only the API-key placeholder.

```powershell
# Repository root
./eng/verify.ps1

# Local runtime storage
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
$databasePath = (Resolve-Path ".local\data").Path + "\market-data.db"

# External configuration
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
$env:Persistence__DatabasePath = $databasePath

# Safe configuration validation
if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "TwelveData__ApiKey is not configured."
}

if ([string]::IsNullOrWhiteSpace($env:Persistence__DatabasePath)) {
    throw "Persistence__DatabasePath is not configured."
}

Write-Host "Twelve Data API key configured."
Write-Host "Database path: $env:Persistence__DatabasePath"

# Real provider-backed acquisition
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

# Confirm durable artifact
Get-Item $env:Persistence__DatabasePath

# Repository safety check
git status

# Revalidate engineering baseline
./eng/verify.ps1

# Remove secret from current shell
Remove-Item Env:TwelveData__ApiKey -ErrorAction SilentlyContinue
```

---

## 26. Current Definition of Done

The real-provider acquisition demonstration is complete when all of the following are true:

```text
[PASS] Repository verification succeeds
[PASS] Twelve Data API key is supplied externally
[PASS] Worker starts successfully
[PASS] Real Twelve Data request succeeds
[PASS] Three AAPL daily observations are acquired
[PASS] Observations are normalized successfully
[PASS] Persistence use case succeeds
[PASS] SQLite database is created/available
[PASS] historical_observations contains persisted data
[PASS] Persistence reports NewlyAccepted or Idempotent
[PASS] No secrets or runtime database artifacts are committed
```

At that point, the platform has demonstrated the Release 1.1 real-world vertical slice:

```text
Twelve Data
     ↓
Historical observations acquired
     ↓
Normalized platform observations
     ↓
Durable SQLite persistence
     ↓
Observable persistence outcome
```

---

## 27. What This Proves Architecturally

This run is more than an HTTP request demonstration.

It proves that the current architecture can carry real external market data through independently owned boundaries:

```text
Provider technology
      │
      ▼
Infrastructure
      │
      ▼
Application contracts
      │
      ▼
Domain-compatible observations
      │
      ▼
Persistence abstraction
      │
      ▼
Infrastructure storage
```

The provider and SQLite details remain infrastructure concerns rather than leaking into the core model.

This is the correct foundation for later capabilities such as:

- configurable symbols and acquisition windows;
- larger historical backfills;
- scheduled acquisition;
- intraday acquisition;
- streaming feeds;
- data-quality pipelines;
- quantitative analytics;
- feature engineering;
- AI/ML research;
- observability and operational metrics;
- cloud deployment.

Those are future platform increments. This runbook should remain focused on proving the real provider-backed Release 1.1 vertical slice that exists today.

---

## References

AIQuantTradingResearch repository:

https://github.com/samuel-santos-engineer/AIQuantTradingResearch

Twelve Data API documentation:

https://twelvedata.com/docs

Twelve Data `/time_series` endpoint documentation is part of the official API documentation above.

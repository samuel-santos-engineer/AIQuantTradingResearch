# LOCAL PLATFORM EXECUTION

> **Start here.** Run AIQuantTradingResearch locally against real market data and verify that the platform works on your machine.

## Goal

This is the AIQuantTradingResearch **Hello World** guide.

The goal is intentionally simple:

```text
Clone
  ↓
Configure
  ↓
Run
  ↓
Real market data processed
  ↓
SUCCESS
```

At the end, you should have executed the platform locally using the real Twelve Data integration and SQLite persistence.

---

## What You Will Run

```text
Twelve Data
     ↓
Historical market observations
     ↓
AIQuantTradingResearch
     ↓
SQLite
```

For the current bounded vertical slice, the Worker uses an AAPL historical-data request.

A successful run should produce output equivalent to:

```text
Target: AAPL
Observation count: 3
Persistence outcome: NewlyAccepted
```

The exact observation count may evolve with the platform. Use the current Worker output as the authority.

---

## Prerequisites

You need:

- Git
- PowerShell
- the .NET SDK required by the repository `global.json`
- internet access
- a Twelve Data account and API key

Twelve Data:

https://twelvedata.com/

Check .NET:

```powershell
dotnet --version
```

---

## 1. Clone the Repository

```powershell
git clone https://github.com/samuel-santos-engineer/AIQuantTradingResearch.git
cd AIQuantTradingResearch
```

If you already have the repository:

```powershell
git checkout main
git pull
```

---

## 2. Verify the Repository

From the repository root:

```powershell
./eng/verify.ps1
```

Continue when verification succeeds.

---

## 3. Create Local Runtime Storage

Create a local directory for the SQLite database:

```powershell
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
```

Configure the database path:

```powershell
$databasePath = (Resolve-Path ".local\data").Path + "\market-data.db"
$env:Persistence__DatabasePath = $databasePath
```

---

## 4. Configure Twelve Data

Get an API key from your Twelve Data account.

Set it for the current PowerShell session:

```powershell
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
```

Do not commit the API key to the repository.

Quick configuration check:

```powershell
if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "Twelve Data API key is not configured."
}

if ([string]::IsNullOrWhiteSpace($env:Persistence__DatabasePath)) {
    throw "SQLite database path is not configured."
}

Write-Host "Runtime configuration ready."
```

---

## 5. Run AIQuantTradingResearch

From the repository root:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

That's it.

You are now running the real provider-backed platform locally.

---

## 6. Look for Success

A successful execution should look similar to:

```text
Target: AAPL
Observation count: 3
Persistence outcome: NewlyAccepted
```

This means the platform completed the basic vertical slice:

```text
Real provider
     ↓
Historical observations
     ↓
Application
     ↓
Persistence
     ↓
SQLite
```

If you run it again against equivalent already-persisted history, the persistence outcome may be:

```text
Persistence outcome: Idempotent
```

That is also a successful result.

---

## 7. Confirm the SQLite Database Exists

Run:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
True
```

You can also inspect the file:

```powershell
Get-Item $env:Persistence__DatabasePath
```

You now have a real local platform outcome:

```text
Twelve Data
     ↓
AIQuantTradingResearch
     ↓
SQLite database
```

---

# Hello, AIQuantTradingResearch

If you reached this point:

```text
[PASS] Repository builds and tests
[PASS] Platform starts locally
[PASS] Twelve Data is reached
[PASS] Real historical observations are processed
[PASS] SQLite persistence succeeds
[PASS] Durable database exists
```

Your local platform is working.

```text
╔══════════════════════════════════════╗
║                                      ║
║     AIQuantTradingResearch           ║
║                                      ║
║     LOCAL EXECUTION: SUCCESS         ║
║                                      ║
╚══════════════════════════════════════╝
```

---

## What Just Happened?

In one execution you exercised several real platform boundaries:

```text
Twelve Data
     │
     ▼
Provider integration
     │
     ▼
Historical observations
     │
     ▼
Application workflow
     │
     ▼
Persistence boundary
     │
     ▼
SQLite
```

This guide intentionally stops here.

The other guides explain how to prove each capability in depth.

---

## Next Guides

### 1. Real Provider Acquisition

```text
REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
```

Proves:

```text
Twelve Data
     ↓
Real historical observations acquired
```

### 2. Durable Persistence

```text
DURABLE_PERSISTENCE_SQLITE.md
```

Proves:

```text
Historical observations
     ↓
SQLite
     ↓
Data survives process termination
```

### 3. Idempotency

```text
IDEMPOTENCY_PROOF.md
```

Proves:

```text
Same history replayed
     ↓
Idempotent
     ↓
0 duplicate history
```

### 4. Data Integrity

```text
DATA_INTEGRITY_PROOF.md
```

Proves:

```text
Acquired data
     ↓
Persisted data
     ↓
Expected identity and values preserved
```

### 5. Restart Recovery

```text
RESTART_RECOVERY_PROOF.md
```

Proves:

```text
Persist
   ↓
Stop
   ↓
Restart
   ↓
Existing state recovered
   ↓
Continue safely
```

---

## Quick Troubleshooting

### Missing API key

Set:

```powershell
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
```

### Missing database path

Run:

```powershell
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
$databasePath = (Resolve-Path ".local\data").Path + "\market-data.db"
$env:Persistence__DatabasePath = $databasePath
```

### Build or test failure

Run:

```powershell
./eng/verify.ps1
```

Resolve the repository baseline before troubleshooting the provider.

### Provider failure

Confirm:

- internet connectivity;
- Twelve Data API key validity;
- account/provider availability.

For detailed provider troubleshooting, use:

```text
REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
```

---

## Clean Up the Current Shell

When finished:

```powershell
Remove-Item Env:TwelveData__ApiKey -ErrorAction SilentlyContinue
Remove-Item Env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

This removes the runtime configuration from the current PowerShell session.

It does not delete the SQLite database.

---

## Five-Minute Path

If the repository is already cloned and the required SDK is installed, the complete path is:

```powershell
# Verify
./eng/verify.ps1

# Local SQLite
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
$databasePath = (Resolve-Path ".local\data").Path + "\market-data.db"
$env:Persistence__DatabasePath = $databasePath

# Real provider
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"

# Run
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Look for:

```text
Target: AAPL
Observation count: 3
Persistence outcome: NewlyAccepted
```

Then:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
True
```

Done.

---

## Definition of Done

This Hello World guide is complete when:

```text
[PASS] ./eng/verify.ps1 succeeds
[PASS] Twelve Data configuration is supplied externally
[PASS] SQLite path is supplied externally
[PASS] Worker runs successfully
[PASS] Real historical observations are processed
[PASS] Persistence succeeds
[PASS] SQLite database exists
```

Final result:

```text
Twelve Data
     ↓
AIQuantTradingResearch
     ↓
SQLite
     ↓
SUCCESS
```

---

## References

AIQuantTradingResearch:

https://github.com/samuel-santos-engineer/AIQuantTradingResearch

Twelve Data:

https://twelvedata.com/

Guide index:

```text
docs/guides/README.md
```

# RESTART RECOVERY PROOF

> Step-by-step operational guide for proving that AIQuantTradingResearch can stop, restart against an existing SQLite database, recover durable historical state, and continue acquisition without losing or duplicating previously accepted history.

## Purpose

This guide demonstrates the next operational property of the AIQuantTradingResearch real-data vertical slice:

```text
Acquire real observations
        ↓
Persist to SQLite
        ↓
Stop application
        ↓
Durable state remains
        ↓
Start application again
        ↓
Existing database reopened
        ↓
Previously accepted history recognized
        ↓
Acquisition continues safely
        ↓
RESTART RECOVERY PROVEN
```

The objective is to prove more than persistence durability.

`DURABLE_PERSISTENCE_SQLITE.md` proves that observations survive process termination.

This guide proves that the **application itself can return after termination, reuse that durable state, and continue operating without treating existing history as new duplicate history**.

The desired recovery behavior is:

```text
Execution 1
-----------
Historical observations acquired
Historical observations persisted
Application exits

Execution 2
-----------
Same database reused
Application starts successfully
Existing historical state recognized
Equivalent history handled idempotently
No duplicate history introduced
Application completes successfully
```

---

## 1. Target Outcome

For a stable 100-observation demonstration:

```text
FIRST EXECUTION

Input observations:              100
NewlyAccepted:                   100
Database historical rows:        100
Duplicate logical history:         0

APPLICATION STOPS

Database historical rows:        100

SECOND EXECUTION AFTER RESTART

Same database reopened
Equivalent observations:         100
Idempotent:                      100
Database historical rows:        100
Duplicate logical history:         0

RESULT: RESTART RECOVERY PASS
```

If the current Worker requests a smaller bounded count, use the actual count.

For example:

```text
First execution:  3 NewlyAccepted
Restart
Second execution: 3 Idempotent
Database:          3 rows
Duplicates:        0
```

The recovery property is identical.

---

## 2. What This Guide Proves

Completing this guide should provide evidence that:

- SQLite state survives Worker termination;
- the Worker can start with a pre-existing database;
- persistence schema/bootstrap logic tolerates an already initialized database;
- previously accepted historical observations remain readable;
- equivalent observations are recognized after restart;
- restart does not reset or truncate accepted history;
- restart does not create duplicate logical observations;
- persistence configuration can point successive Worker processes to the same durable state;
- the platform can continue its bounded acquisition workflow after process loss/restart.

The core property is:

```text
Process lifetime
    ≠
Data lifetime
```

and:

```text
New process
    +
Existing durable state
    =
Valid continuation
```

---

## 3. What This Guide Does Not Claim

This guide proves **process restart recovery** for the currently implemented local SQLite-backed vertical slice.

It does not claim:

- distributed failover;
- high availability;
- automatic process supervision;
- container orchestration recovery;
- node replacement;
- database replication;
- disaster recovery;
- point-in-time restore;
- crash-consistent recovery from arbitrary filesystem failure;
- exactly-once distributed delivery;
- resumable multi-batch backfill checkpoints;
- cloud deployment recovery.

Those require additional platform capabilities and separate proofs.

---

## 4. Relationship to Existing Guides

The verification chain before this guide is:

```text
REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
        ↓
Can real market observations be acquired?

DURABLE_PERSISTENCE_SQLITE.md
        ↓
Do accepted observations survive process termination?

IDEMPOTENCY_PROOF.md
        ↓
Can equivalent observations be replayed safely?

DATA_INTEGRITY_PROOF.md
        ↓
Does persisted history preserve expected identity and values?

RESTART_RECOVERY_PROOF.md
        ↓
Can a new application process reuse that durable state safely?
```

This guide intentionally combines durability and idempotency across an explicit application restart boundary.

---

## 5. Recovery Invariants

The demonstration uses these invariants.

### Invariant 1 — Same database

Both executions must use the exact same:

```text
Persistence:DatabasePath
```

### Invariant 2 — State survives process termination

After execution 1 terminates:

```text
historical_observations > 0
```

must remain true.

### Invariant 3 — Startup does not erase state

After execution 2 starts:

```text
existing history before restart
```

must not disappear.

### Invariant 4 — Equivalent history remains idempotent

For equivalent observations already accepted before restart:

```text
same identity + same value
        ↓
Idempotent
```

### Invariant 5 — No duplicate logical history

After restart:

```text
duplicate_history = 0
```

### Invariant 6 — Database remains valid

SQLite:

```sql
PRAGMA integrity_check;
```

must return:

```text
ok
```

---

## 6. Prerequisites

Before starting:

- the repository is available locally;
- the .NET SDK required by `global.json` is installed;
- `./eng/verify.ps1` passes;
- Twelve Data acquisition works;
- SQLite persistence works;
- a valid Twelve Data API key is available;
- SQLite CLI or another SQLite inspection tool is available.

Recommended preceding guides:

```text
docs/guides/REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
docs/guides/DURABLE_PERSISTENCE_SQLITE.md
docs/guides/IDEMPOTENCY_PROOF.md
docs/guides/DATA_INTEGRITY_PROOF.md
```

---

## 7. Verify the Repository Baseline

From the repository root:

```powershell
git status
```

Then:

```powershell
./eng/verify.ps1
```

Expected:

```text
PASS
```

Do not begin restart-recovery diagnosis from an unhealthy repository state.

---

## 8. Prepare a Dedicated Recovery Database

Create a local runtime directory:

```powershell
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
```

Create a dedicated database path:

```powershell
$databasePath = (Resolve-Path ".local\data").Path + "\restart-recovery-proof.db"
```

Configure it:

```powershell
$env:Persistence__DatabasePath = $databasePath
```

Display it:

```powershell
Write-Host "Recovery database: $env:Persistence__DatabasePath"
```

This exact path must remain unchanged for both executions.

---

## 9. Configure Twelve Data

Set the provider API key:

```powershell
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
```

Validate without printing the key:

```powershell
if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "TwelveData__ApiKey is not configured."
}

Write-Host "Twelve Data API key configured."
```

Never commit or expose the secret.

---

## 10. Record the Expected Demonstration

For example:

```powershell
$expectedTarget = "AAPL"
$expectedObservationCount = 100
```

If the current Worker requests another count, change only the expectation to match the executable platform.

The evidence must reflect what the Worker actually does.

---

## 11. Start From a Clean State

Delete the disposable recovery database **before execution 1 only**:

```powershell
Remove-Item $env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

Confirm:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
False
```

The initial state is:

```text
Application stopped
Database absent
Historical observations = 0
```

---

# Part I — Establish Durable State

## 12. Run Execution 1

Run:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

Expected high-level flow:

```text
Worker starts
     ↓
Twelve Data acquisition
     ↓
Historical observations normalized
     ↓
SQLite initialized
     ↓
Observations persisted
     ↓
Worker exits
```

For a clean 100-observation demonstration:

```text
Target: AAPL
Observation count: 100
Persistence outcome: NewlyAccepted
```

Use the actual output produced by the current platform.

---

## 13. Confirm Execution 1 Completed

Record:

```text
Execution:             1
Target:                AAPL
Observation count:     100
Persistence:           NewlyAccepted
Process state:         terminated normally
```

The Worker should no longer be running after the bounded execution completes.

The recovery test begins from this stopped state.

---

## 14. Confirm the Database Exists After Execution 1

Run:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
True
```

Inspect:

```powershell
Get-Item $env:Persistence__DatabasePath
```

This proves the durable artifact remains after execution 1.

---

## 15. Verify Historical State Before Restart

Open SQLite:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Configure readable output:

```sql
.headers on
.mode column
```

Run:

```sql
SELECT
    target,
    COUNT(*) AS historical_rows
FROM historical_observations
GROUP BY target
ORDER BY target;
```

For the 100-observation AAPL demonstration:

```text
target  historical_rows
------  ---------------
AAPL    100
```

---

## 16. Verify Zero Duplicate History Before Restart

Run:

```sql
SELECT COUNT(*) AS duplicate_history
FROM (
    SELECT
        target,
        instant_utc_ticks
    FROM historical_observations
    GROUP BY target, instant_utc_ticks
    HAVING COUNT(*) > 1
);
```

Expected:

```text
duplicate_history
-----------------
0
```

This establishes the pre-restart baseline:

```text
Historical rows:   100
Duplicate history:   0
```

---

## 17. Record a Recovery Baseline

Run:

```sql
SELECT
    COUNT(*) AS total_rows,
    MIN(instant_utc_ticks) AS earliest_instant,
    MAX(instant_utc_ticks) AS latest_instant
FROM historical_observations;
```

Record the values.

For stronger evidence, save them in your terminal notes:

```text
BEFORE RESTART

total_rows      = ...
earliest_instant = ...
latest_instant   = ...
```

These values allow post-restart comparison.

---

## 18. Verify SQLite Integrity Before Restart

Run:

```sql
PRAGMA integrity_check;
```

Expected:

```text
ok
```

Exit:

```sql
.quit
```

---

# Part II — Establish the Restart Boundary

## 19. Confirm the Application Is Stopped

The bounded Worker execution should already have terminated.

The state must now be:

```text
Worker process: stopped
SQLite CLI:     stopped
Database file:  present
```

This is the recovery boundary.

The historical data exists independently of any running application process.

---

## 20. Do Not Reset the Database

Before execution 2:

**DO NOT run:**

```powershell
Remove-Item $env:Persistence__DatabasePath
```

**DO NOT change:**

```powershell
$env:Persistence__DatabasePath
```

**DO NOT create another database path.**

The proof requires:

```text
Execution 1 ─┐
             ├── SAME SQLITE DATABASE
Execution 2 ─┘
```

---

## 21. Confirm the Same Database Path

Run:

```powershell
Write-Host $env:Persistence__DatabasePath
```

Confirm it still points to:

```text
restart-recovery-proof.db
```

Then:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
True
```

---

# Part III — Restart the Application

## 22. Run Execution 2

Start a new Worker process:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

This is the actual restart.

The new process must:

```text
start
  ↓
resolve the same persistence configuration
  ↓
open the existing SQLite database
  ↓
operate against existing historical state
  ↓
complete without destructive reinitialization
```

---

## 23. Expected Recovery Behavior

If execution 2 receives the same logical observations:

```text
Existing database
        ↓
100 accepted observations

New Worker process
        ↓
Same 100 observations acquired

Persistence
        ↓
Existing identities found
        ↓
Equivalent values recognized

Outcome
        ↓
100 Idempotent
```

The database should remain:

```text
100 historical rows
```

not:

```text
0
```

and not:

```text
200
```

---

## 24. Record Execution 2

For a stable 100-observation demonstration:

```text
Execution:             2
Restart:               yes
Database:              same
Target:                AAPL
Observation count:     100
Persistence:           Idempotent
Process state:         completed normally
```

If the provider dataset changed between runs, some observations may legitimately be `NewlyAccepted`.

That situation is addressed later in this guide.

---

# Part IV — Verify Recovery

## 25. Confirm the Database Still Exists

Run:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected:

```text
True
```

---

## 26. Reopen SQLite After Restart

Run:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Then:

```sql
.headers on
.mode column
```

This independently verifies the state produced by the restarted process.

---

## 27. Verify Historical Count After Restart

Run:

```sql
SELECT
    target,
    COUNT(*) AS historical_rows
FROM historical_observations
GROUP BY target
ORDER BY target;
```

For an equivalent 100-observation replay:

```text
target  historical_rows
------  ---------------
AAPL    100
```

The critical comparison is:

```text
Before restart: 100
After restart:  100
```

---

## 28. Verify Zero Duplicate History After Restart

Run:

```sql
SELECT COUNT(*) AS duplicate_history
FROM (
    SELECT
        target,
        instant_utc_ticks
    FROM historical_observations
    GROUP BY target, instant_utc_ticks
    HAVING COUNT(*) > 1
);
```

Expected:

```text
duplicate_history
-----------------
0
```

This proves that restart did not duplicate accepted logical history.

---

## 29. Verify Distinct Logical History

Run:

```sql
SELECT COUNT(*) AS distinct_logical_history
FROM (
    SELECT
        target,
        instant_utc_ticks
    FROM historical_observations
    GROUP BY target, instant_utc_ticks
);
```

For the stable 100-observation demonstration:

```text
distinct_logical_history
------------------------
100
```

Compare:

```text
total rows               = 100
distinct logical history = 100
duplicate history        = 0
```

---

## 30. Compare the Recovery Baseline

Run:

```sql
SELECT
    COUNT(*) AS total_rows,
    MIN(instant_utc_ticks) AS earliest_instant,
    MAX(instant_utc_ticks) AS latest_instant
FROM historical_observations;
```

For an exactly equivalent replay, compare with the values recorded before restart.

Expected:

```text
BEFORE RESTART
total_rows       = 100
earliest_instant = T1
latest_instant   = T100

AFTER RESTART
total_rows       = 100
earliest_instant = T1
latest_instant   = T100
```

This provides additional evidence that the existing durable historical set remained stable.

---

## 31. Run SQLite Integrity Check After Restart

Run:

```sql
PRAGMA integrity_check;
```

Expected:

```text
ok
```

Exit:

```sql
.quit
```

---

# Part V — Compact Recovery Proof

## 32. Run the Combined Verification Query

After execution 2:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Then:

```sql
.headers on
.mode column

SELECT
    (SELECT COUNT(*)
     FROM historical_observations) AS total_rows,

    (SELECT COUNT(*)
     FROM (
         SELECT
             target,
             instant_utc_ticks
         FROM historical_observations
         GROUP BY target, instant_utc_ticks
     )) AS distinct_logical_history,

    (SELECT COUNT(*)
     FROM (
         SELECT
             target,
             instant_utc_ticks
         FROM historical_observations
         GROUP BY target, instant_utc_ticks
         HAVING COUNT(*) > 1
     )) AS duplicate_history,

    MIN(instant_utc_ticks) AS earliest_instant,
    MAX(instant_utc_ticks) AS latest_instant
FROM historical_observations;
```

For the stable 100-observation demonstration:

```text
total_rows  distinct_logical_history  duplicate_history  earliest_instant  latest_instant
----------  ------------------------  -----------------  ----------------  --------------
100         100                       0                  ...               ...
```

This is strong compact evidence.

---

## 33. Recovery Proof Summary

The desired proof can now be written as:

```text
RESTART RECOVERY PROOF

Execution 1
-----------
100 observations acquired
100 NewlyAccepted
SQLite rows: 100
Duplicate history: 0

Application stopped
-------------------
Worker: stopped
SQLite database: retained
SQLite rows: 100

Execution 2
-----------
New Worker process
Same database reopened
100 equivalent observations acquired
100 Idempotent
SQLite rows: 100
Duplicate history: 0

SQLite integrity
----------------
ok

RESULT: PASS
```

---

# Part VI — When New Market Data Appears

## 34. Restart Recovery Does Not Require the Database to Remain Frozen Forever

A real platform should eventually continue acquiring new data after restart.

Suppose execution 1 persists:

```text
T1 ... T100
```

After time passes, execution 2 acquires:

```text
T2 ... T101
```

Then the correct outcome is conceptually:

```text
T2 ... T100
        ↓
Already accepted
        ↓
Idempotent

T101
        ↓
New observation
        ↓
NewlyAccepted
```

Final durable history:

```text
T1 ... T101
```

This is valid restart recovery.

The important properties remain:

```text
Old history preserved
New history accepted
Overlapping history idempotent
Duplicate history = 0
```

---

## 35. Stronger Continuation Proof

When the current platform supports a stable way to acquire a later observation window, the stronger recovery demonstration becomes:

```text
BEFORE RESTART

100 durable observations
T1 ... T100

APPLICATION RESTART

AFTER RESTART INPUT

100 observations
T2 ... T101

EXPECTED

99 Idempotent
1 NewlyAccepted

FINAL DATABASE

101 total logical observations
0 duplicate history
```

This is stronger than replaying an identical set because it proves both:

```text
recovery of old state
        +
continuation with new state
```

Do not claim this exact `99 + 1` outcome unless the current acquisition window actually produces that deterministic overlap.

---

# Part VII — Recovery Failure Modes

## 36. Failure — Database Is Recreated or Truncated on Startup

If execution 2 results in:

```text
Before restart: 100
After restart:  0
```

or the application recreates an empty database, restart recovery has failed.

Investigate:

- schema initialization;
- database path resolution;
- startup bootstrap behavior;
- accidental delete/recreate logic;
- environment configuration.

Startup initialization must distinguish:

```text
database absent
```

from:

```text
database already initialized
```

---

## 37. Failure — Different Database Path Used After Restart

Check:

```powershell
$env:Persistence__DatabasePath
```

If execution 2 points somewhere else, the test is invalid.

Example invalid proof:

```text
Execution 1 → database-A.db
Execution 2 → database-B.db
```

That tests two independent executions, not recovery.

---

## 38. Failure — Equivalent History Becomes Duplicated

If the second equivalent execution results in:

```text
200 rows
```

instead of:

```text
100 rows
```

run:

```sql
SELECT
    target,
    instant_utc_ticks,
    COUNT(*) AS occurrences
FROM historical_observations
GROUP BY target, instant_utc_ticks
HAVING COUNT(*) > 1;
```

Any result requires investigation.

Potential causes include:

- logical identity mismatch;
- timestamp normalization instability;
- target normalization differences;
- missing database uniqueness enforcement;
- persistence lookup failure.

---

## 39. Failure — Equivalent History Becomes Conflict

If the same logical observations become conflicts after restart, compare persisted and incoming semantic values.

Potential causes include:

- nondeterministic normalization;
- price representation changes;
- timestamp/offset conversion differences;
- provider corrections;
- different acquisition data.

A true provider correction is different from a restart-recovery defect.

Preserve the evidence before changing the database.

---

## 40. Failure — SQLite Cannot Be Reopened

Possible causes:

- invalid database path;
- permissions;
- corrupted file;
- incomplete filesystem operation;
- incompatible manual modification;
- external process lock behavior.

Run:

```powershell
Get-Item $env:Persistence__DatabasePath
```

Then use SQLite:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

and:

```sql
PRAGMA integrity_check;
```

Do not delete the database until the failure is understood if the goal is diagnosis.

---

## 41. Failure — Provider Dataset Changed

A changed provider window can produce some `NewlyAccepted` observations after restart.

That is not automatically a recovery failure.

Determine whether:

```text
existing logical history
```

remained preserved and whether:

```text
new logical history
```

was accepted without duplication.

The recovery invariants are stronger than expecting every restart to be 100% idempotent forever.

---

# Part VIII — Evidence Capture

## 42. Recommended Evidence 1 — First Execution

Capture:

```text
FIRST EXECUTION

Target: AAPL
Observation count: 100
Persistence: NewlyAccepted
```

Use the actual count/output of the current Worker.

---

## 43. Recommended Evidence 2 — Durable State While Application Is Stopped

After execution 1 has terminated, capture:

```sql
SELECT target, COUNT(*)
FROM historical_observations
GROUP BY target;
```

Expected:

```text
AAPL  100
```

This visually establishes:

```text
application stopped
data still exists
```

---

## 44. Recommended Evidence 3 — Restarted Execution

Capture execution 2:

```text
SECOND EXECUTION / RESTART

Target: AAPL
Observation count: 100
Persistence: Idempotent
```

or the actual aggregate outcome.

---

## 45. Recommended Evidence 4 — Post-Restart Database State

Capture:

```text
total_rows  distinct_logical_history  duplicate_history
----------  ------------------------  -----------------
100         100                       0
```

This independently verifies safe recovery.

---

## 46. Recommended Evidence 5 — Integrity Check

Capture:

```text
PRAGMA integrity_check;

ok
```

---

## 47. Portfolio-Quality Summary

A concise repository/demo summary can be:

```text
RESTART RECOVERY

Before restart
--------------
100 durable historical observations
0 duplicate history

Process boundary
----------------
Worker terminated
SQLite retained

After restart
-------------
Existing database reopened
100 equivalent observations recognized
100 Idempotent
100 durable historical observations
0 duplicate history

SQLite integrity
----------------
ok

RESULT: PASS
```

This demonstrates a meaningful reliability property rather than simply showing two successful application runs.

---

# Part IX — Repeatable Procedure

## 48. One-Pass Checklist

- [ ] Run `./eng/verify.ps1`.
- [ ] Create a dedicated recovery database path.
- [ ] Configure `Persistence__DatabasePath`.
- [ ] Configure `TwelveData__ApiKey`.
- [ ] Record the expected target/count.
- [ ] Delete the disposable database before execution 1 only.
- [ ] Run execution 1.
- [ ] Confirm acquisition succeeds.
- [ ] Confirm persistence accepts new observations.
- [ ] Confirm Worker terminates.
- [ ] Open SQLite independently.
- [ ] Record pre-restart total rows.
- [ ] Record pre-restart earliest/latest identities.
- [ ] Confirm duplicate history = 0.
- [ ] Confirm `PRAGMA integrity_check` = `ok`.
- [ ] Close SQLite.
- [ ] Do not delete the database.
- [ ] Do not change the database path.
- [ ] Start a new Worker process.
- [ ] Confirm execution 2 starts successfully.
- [ ] Confirm existing state is reused.
- [ ] Confirm equivalent observations are idempotent where applicable.
- [ ] Reopen SQLite independently.
- [ ] Confirm old historical state remains.
- [ ] Confirm expected new history is present if the input advanced.
- [ ] Confirm duplicate history = 0.
- [ ] Confirm SQLite integrity = `ok`.
- [ ] Capture safe evidence.
- [ ] Check `git status`.
- [ ] Run `./eng/verify.ps1` again.
- [ ] Clear secret environment variables.

---

## 49. Compact PowerShell Recovery Demonstration

Replace only the API-key placeholder and adjust the expected count to the current Worker configuration.

```powershell
# Repository baseline
./eng/verify.ps1

# Demonstration expectations
$expectedTarget = "AAPL"
$expectedObservationCount = 100

# Dedicated durable database
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
$databasePath = (Resolve-Path ".local\data").Path + "\restart-recovery-proof.db"

$env:Persistence__DatabasePath = $databasePath
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"

if ([string]::IsNullOrWhiteSpace($env:Persistence__DatabasePath)) {
    throw "Persistence__DatabasePath is not configured."
}

if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "TwelveData__ApiKey is not configured."
}

Write-Host "Target: $expectedTarget"
Write-Host "Expected observations: $expectedObservationCount"
Write-Host "Recovery database: $env:Persistence__DatabasePath"
Write-Host "Twelve Data API key configured."

# CLEAN ONLY BEFORE EXECUTION 1
Remove-Item $env:Persistence__DatabasePath -ErrorAction SilentlyContinue

Write-Host ""
Write-Host "================================="
Write-Host " EXECUTION 1 — ESTABLISH STATE"
Write-Host "================================="

dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

if (-not (Test-Path $env:Persistence__DatabasePath)) {
    throw "Recovery database was not created."
}

Write-Host ""
Write-Host "Execution 1 complete."
Write-Host "Worker process has terminated."
Write-Host "Durable database remains:"
Get-Item $env:Persistence__DatabasePath

# IMPORTANT:
# DO NOT DELETE THE DATABASE.
# DO NOT CHANGE Persistence__DatabasePath.

Write-Host ""
Write-Host "================================="
Write-Host " EXECUTION 2 — RESTART"
Write-Host "================================="

dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

Write-Host ""
Write-Host "Execution 2 complete."
Write-Host "Same durable database remains:"
Get-Item $env:Persistence__DatabasePath

# Repository safety
git status

# Revalidate baseline
./eng/verify.ps1

# Clear runtime configuration from this shell
Remove-Item Env:TwelveData__ApiKey -ErrorAction SilentlyContinue
Remove-Item Env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

---

## 50. Compact SQLite Recovery Verification

After execution 2:

```powershell
sqlite3 $databasePath
```

Run:

```sql
.headers on
.mode column

SELECT
    (SELECT COUNT(*)
     FROM historical_observations) AS total_rows,

    (SELECT COUNT(*)
     FROM (
         SELECT
             target,
             instant_utc_ticks
         FROM historical_observations
         GROUP BY target, instant_utc_ticks
     )) AS distinct_logical_history,

    (SELECT COUNT(*)
     FROM (
         SELECT
             target,
             instant_utc_ticks
         FROM historical_observations
         GROUP BY target, instant_utc_ticks
         HAVING COUNT(*) > 1
     )) AS duplicate_history,

    MIN(instant_utc_ticks) AS earliest_instant,
    MAX(instant_utc_ticks) AS latest_instant
FROM historical_observations;

SELECT
    target,
    COUNT(*) AS historical_rows
FROM historical_observations
GROUP BY target
ORDER BY target;

PRAGMA integrity_check;

.quit
```

For an equivalent 100-observation replay, the desired result is:

```text
total_rows               = 100
distinct_logical_history = 100
duplicate_history        = 0
AAPL historical rows     = 100
SQLite integrity         = ok
```

---

## 51. Definition of Done

The restart-recovery proof is complete when:

```text
[PASS] Repository verification succeeds

[PASS] Execution 1 starts from a clean dedicated database
[PASS] Execution 1 acquires real historical observations
[PASS] Execution 1 persists new observations
[PASS] Worker terminates normally
[PASS] Database remains after Worker termination
[PASS] Historical observations remain queryable while Worker is stopped
[PASS] Duplicate history before restart = 0
[PASS] SQLite integrity before restart = ok

[PASS] Execution 2 uses the exact same database
[PASS] Execution 2 starts as a new Worker process
[PASS] Existing SQLite state is reopened successfully
[PASS] Previously accepted history remains present
[PASS] Equivalent overlapping history is handled idempotently
[PASS] Legitimately new history can be accepted when present
[PASS] Existing history is not truncated or reset
[PASS] Duplicate history after restart = 0
[PASS] SQLite integrity after restart = ok

[PASS] No credentials are committed
[PASS] Runtime database is not accidentally committed
[PASS] Repository verification still succeeds
```

---

## 52. Final Proof

The final evidence should tell this story:

```text
              EXECUTION 1
                  │
                  ▼
          Real observations
                  │
                  ▼
              SQLite
                  │
                  ▼
        Durable history exists
                  │
                  ▼
          WORKER TERMINATES
                  │
                  │
        process state is lost
                  │
        durable state remains
                  │
                  ▼
              EXECUTION 2
                  │
                  ▼
          New Worker process
                  │
                  ▼
       Same SQLite database
                  │
                  ▼
      Existing history recovered
                  │
                  ├───────────────┐
                  │               │
                  ▼               ▼
        Equivalent history     New history
                  │               │
                  ▼               ▼
             Idempotent      NewlyAccepted
                  │               │
                  └───────┬───────┘
                          ▼
                 No duplicate history
                          │
                          ▼
                RESTART RECOVERY
                       PROVEN
```

---

## 53. Platform Verification Chain

With this guide, the executable assessment chain becomes:

```text
1. REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
   │
   └── Real external data can enter the platform.
                    ↓

2. DURABLE_PERSISTENCE_SQLITE.md
   │
   └── Accepted data survives the process.
                    ↓

3. IDEMPOTENCY_PROOF.md
   │
   └── Equivalent retries do not duplicate history.
                    ↓

4. DATA_INTEGRITY_PROOF.md
   │
   └── Durable history preserves expected identity and values.
                    ↓

5. RESTART_RECOVERY_PROOF.md
   │
   └── A new process can safely continue from existing durable state.
```

Together:

```text
REAL DATA
    ↓
DURABLE DATA
    ↓
RETRY-SAFE DATA
    ↓
VERIFIED DATA
    ↓
RECOVERABLE DATA PLATFORM
```

This is a progressively stronger demonstration of production-oriented behavior using the currently implemented local vertical slice.

---

## References

Related guides:

```text
docs/guides/REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
docs/guides/DURABLE_PERSISTENCE_SQLITE.md
docs/guides/IDEMPOTENCY_PROOF.md
docs/guides/DATA_INTEGRITY_PROOF.md
```

AIQuantTradingResearch repository:

https://github.com/samuel-santos-engineer/AIQuantTradingResearch

Twelve Data documentation:

https://twelvedata.com/docs

SQLite documentation:

https://www.sqlite.org/docs.html

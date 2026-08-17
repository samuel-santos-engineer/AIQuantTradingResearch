# IDEMPOTENCY PROOF

> Step-by-step operational guide for proving that repeated persistence of the same historical observations is idempotent and does not create duplicate history.

## Purpose

This guide demonstrates a critical persistence property of AIQuantTradingResearch:

```text
Same historical observations
        ↓
Persist twice
        ↓
First execution: accepted as new
        ↓
Second execution: recognized as already accepted
        ↓
No duplicate historical records
```

The target demonstration is:

```text
First execution
100 NewlyAccepted

Second execution
100 Idempotent
0 duplicate history
```

This guide is intentionally focused on **proof of idempotency**.

Related guides cover the preceding capabilities:

```text
REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
        ↓
Real historical observations acquired

DURABLE_PERSISTENCE_SQLITE.md
        ↓
Historical observations persisted durably
```

This guide proves the next property:

```text
Same observations replayed
        ↓
No duplicate history
```

---

## 1. Target Outcome

The target evidence is:

```text
FIRST EXECUTION

Input observations:      100
NewlyAccepted:           100
Idempotent:                0
Duplicate history:         0


SECOND EXECUTION

Input observations:      100
NewlyAccepted:             0
Idempotent:              100
Duplicate history:         0
```

The SQLite database should contain exactly:

```text
100 logical historical observations
```

after both executions.

It must **not** contain:

```text
200 rows
```

for the same 100 logical historical observations.

The essential equation is:

```text
100 observations
    ×
2 executions
    ≠
200 historical records
```

Instead:

```text
100 observations
    ×
2 equivalent executions
    =
100 durable historical records
```

---

## 2. Why This Matters

Historical acquisition workflows are naturally retried.

Retries can happen because of:

- transient provider failures;
- network failures;
- application restarts;
- scheduled re-acquisition;
- recovery procedures;
- operator re-execution;
- pipeline retries;
- overlapping historical backfills.

Without idempotency, a retry could produce:

```text
Execution 1 → 100 records
Execution 2 → 100 more records
Execution 3 → 100 more records
...
```

That would corrupt the historical dataset with duplicates.

The desired behavior is:

```text
Execution 1
    ↓
100 new historical facts accepted

Execution 2
    ↓
The same 100 facts recognized

Execution 3
    ↓
The same 100 facts recognized

Database
    ↓
Still exactly 100 logical historical facts
```

Idempotency makes retries safe.

---

## 3. Important Current-Implementation Note

Before attempting the exact `100 / 100` demonstration, confirm that the current Worker can request 100 observations.

Earlier versions of the vertical slice used a bounded request similar to:

```csharp
new ResearchRequest("AAPL", 3)
```

If the current Worker still requests only three observations, the same proof can be performed immediately with:

```text
First execution
3 NewlyAccepted

Second execution
3 Idempotent
0 duplicate history
```

The persistence property is identical.

To obtain the exact evidence requested by this guide:

```text
100 NewlyAccepted
100 Idempotent
0 duplicate history
```

configure or update the acquisition request through the repository's supported configuration/implementation path so that **exactly 100 stable historical observations** are presented to persistence on both executions.

Do not modify persistence semantics merely to manufacture the demonstration.

The important invariant is:

```text
Same logical input set
        ↓
Same database
        ↓
Repeated persistence
        ↓
No duplicate logical history
```

---

## 4. Prerequisites

Before starting:

- the repository builds and tests successfully;
- Twelve Data acquisition is working;
- SQLite durable persistence is working;
- the Worker can acquire the desired observation count;
- the same database path can be reused between executions;
- the SQLite CLI is available, or another SQLite inspection tool is installed. If you use `sqlite3`, install and verify it with the [SQLite CLI Installation](SQLITE_CLI_INSTALLATION.md) guide.

Follow these guides first if necessary:

```text
docs/guides/REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
docs/guides/DURABLE_PERSISTENCE_SQLITE.md
```

---

## 5. Verify the Engineering Baseline

From the repository root:

```powershell
git status
```

Then:

```powershell
./eng/verify.ps1
```

The quality gate should pass before performing the demonstration.

This prevents unrelated build or test failures from being confused with persistence behavior.

---

## 6. Prepare the Runtime Directory

Create a disposable local runtime-data directory:

```powershell
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null
```

Resolve the database path:

```powershell
$databasePath = (Resolve-Path ".local\data").Path + "\idempotency-proof.db"
```

Configure it:

```powershell
$env:Persistence__DatabasePath = $databasePath
```

Display the safe path:

```powershell
Write-Host "Database path: $env:Persistence__DatabasePath"
```

The **same database path must be used for both executions**.

That is essential to the proof.

---

## 7. Configure the Observation Source

For the current real-provider vertical slice:

```powershell
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"
```

Validate without printing the secret:

```powershell
if ([string]::IsNullOrWhiteSpace($env:TwelveData__ApiKey)) {
    throw "TwelveData__ApiKey is not configured."
}

Write-Host "Twelve Data API key configured."
```

Never commit or display the API key.

---

## 8. Establish the Test Dataset

For the target demonstration, use:

```text
Target: AAPL
Observation count: 100
```

Both executions must represent the same logical historical dataset.

The persistence identity is based on the historical observation's logical identity, conceptually:

```text
target + historical instant
```

Therefore, the proof requires equivalent observations for those identities on the second execution.

### Important

If new market data becomes part of the provider's latest 100-observation window between executions, the two acquisition sets may not be identical.

For the cleanest demonstration:

1. run both executions close together;
2. use a historical interval/window that remains stable;
3. verify the exact persisted identities before interpreting the result.

The strongest automated proof eventually uses a fixed deterministic set of 100 observations presented twice to the persistence boundary.

This operational guide demonstrates the same property through the runnable platform.

---

## 9. Start From an Empty Database

Delete only the disposable proof database:

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

The initial state is now:

```text
Database historical observation count = 0
```

This makes the first execution unambiguous.

---

# Part I — First Execution

## 10. Run the First Acquisition

Run:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

For the exact 100-observation demonstration, the acquisition should provide:

```text
Observation count: 100
```

The persistence result should represent:

```text
100 NewlyAccepted
```

If the application reports aggregate persistence status rather than per-observation counters, verify the exact count independently through SQLite in the next steps.

---

## 11. First-Execution Expected State

Conceptually:

```text
Database before execution
        ↓
0 observations

Incoming
        ↓
100 observations

Persistence
        ↓
100 observations do not exist yet

Outcome
        ↓
100 NewlyAccepted

Database after execution
        ↓
100 observations
```

This establishes the baseline.

---

## 12. Verify the First Database Count

Open SQLite:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Run:

```sql
SELECT COUNT(*) AS total_history
FROM historical_observations;
```

Expected:

```text
100
```

Then verify the target:

```sql
SELECT
    target,
    COUNT(*) AS observation_count
FROM historical_observations
GROUP BY target;
```

Expected for this demonstration:

```text
AAPL|100
```

---

## 13. Verify Zero Duplicate History After the First Execution

Run:

```sql
SELECT
    target,
    instant_utc_ticks,
    COUNT(*) AS occurrences
FROM historical_observations
GROUP BY target, instant_utc_ticks
HAVING COUNT(*) > 1;
```

Expected:

```text
0 rows
```

For an explicit numeric result, use:

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
0
```

The first proof checkpoint is now:

```text
First execution
100 NewlyAccepted
100 total history
0 duplicate history
```

Exit:

```sql
.quit
```

---

# Part II — Second Execution

## 14. Do Not Reset the Database

This is the most important operational instruction in the guide.

Before the second execution:

**DO NOT run:**

```powershell
Remove-Item $env:Persistence__DatabasePath
```

**DO NOT change:**

```text
Persistence__DatabasePath
```

The second execution must target the exact same database.

The state before execution 2 must be:

```text
Database
    ↓
100 already accepted historical observations
```

---

## 15. Run the Same Acquisition Again

Run the exact same Worker command:

```powershell
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
```

The second execution should present the same logical observations to persistence.

Conceptually:

```text
Incoming
    ↓
100 observations

Database
    ↓
Same 100 observations already accepted

Persistence comparison
    ↓
Same identity + same accepted value

Outcome
    ↓
100 Idempotent
```

No new logical historical state should be created.

---

## 16. Second-Execution Expected State

The expected behavior is:

```text
Database before execution
        ↓
100 observations

Incoming
        ↓
Same 100 observations

Persistence
        ↓
100 already exist identically

Outcome
        ↓
100 Idempotent

Database after execution
        ↓
Still 100 observations
```

This is the central idempotency proof.

---

## 17. Verify the Database Still Contains 100 Observations

Open SQLite again:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

Run:

```sql
SELECT COUNT(*) AS total_history
FROM historical_observations;
```

Expected:

```text
100
```

The critical comparison is:

```text
After first execution:  100
After second execution: 100
```

Not:

```text
After first execution:  100
After second execution: 200
```

---

## 18. Verify Zero Duplicate History After the Second Execution

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
0
```

This produces the requested final evidence:

```text
Second execution
100 Idempotent
0 duplicate history
```

---

## 19. Produce a Compact Proof Query

For screenshot/demo purposes, run:

```sql
SELECT
    COUNT(*) AS total_history,
    (
        SELECT COUNT(*)
        FROM (
            SELECT
                target,
                instant_utc_ticks
            FROM historical_observations
            GROUP BY target, instant_utc_ticks
            HAVING COUNT(*) > 1
        )
    ) AS duplicate_history
FROM historical_observations;
```

Expected:

```text
100|0
```

Meaning:

```text
total_history     = 100
duplicate_history = 0
```

---

## 20. Produce Human-Readable SQLite Output

Inside SQLite:

```sql
.headers on
.mode column
```

Then:

```sql
SELECT
    COUNT(*) AS total_history,
    (
        SELECT COUNT(*)
        FROM (
            SELECT
                target,
                instant_utc_ticks
            FROM historical_observations
            GROUP BY target, instant_utc_ticks
            HAVING COUNT(*) > 1
        )
    ) AS duplicate_history
FROM historical_observations;
```

Expected presentation:

```text
total_history  duplicate_history
-------------  -----------------
100            0
```

This is useful screenshot evidence.

---

## 21. Complete Proof

The complete proof should now read:

```text
FIRST EXECUTION

Input:               100 observations
NewlyAccepted:       100
Database history:    100
Duplicate history:     0


SECOND EXECUTION

Input:               100 equivalent observations
Idempotent:          100
Database history:    100
Duplicate history:     0
```

Therefore:

```text
100 historical observations
        ↓
Persist
        ↓
100 NewlyAccepted
        ↓
Persist same observations again
        ↓
100 Idempotent
        ↓
Database remains at 100
        ↓
0 duplicate history
```

---

# Part III — Stronger Verification

## 22. Verify Unique Logical Identities

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

Expected:

```text
100
```

Now compare:

```text
Total rows:                 100
Distinct logical history:   100
Duplicate logical history:    0
```

All three numbers together provide strong evidence.

---

## 23. Combined Verification Query

Run:

```sql
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
     )) AS duplicate_history;
```

Expected:

```text
total_rows  distinct_logical_history  duplicate_history
----------  ------------------------  -----------------
100         100                       0
```

This is the preferred database screenshot for the proof.

---

## 24. Verify Database Integrity

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

# Part IV — Understanding the Result

## 25. What `NewlyAccepted` Means

For an observation whose logical historical identity does not already exist:

```text
Incoming observation
        ↓
No accepted record with same identity
        ↓
Persist
        ↓
NewlyAccepted
```

For the first clean execution:

```text
100 incoming new observations
        ↓
100 NewlyAccepted
```

---

## 26. What `Idempotent` Means

For an observation whose logical identity and accepted value already exist:

```text
Incoming observation
        ↓
Existing identical historical fact
        ↓
No new historical state required
        ↓
Idempotent
```

For the second equivalent execution:

```text
100 incoming observations
        ↓
100 already accepted identically
        ↓
100 Idempotent
```

`Idempotent` is a successful persistence result.

It is not an error and it is not data loss.

It means:

```text
The requested final state already exists.
```

---

## 27. What Would Constitute Failure

The demonstration fails if the second equivalent execution results in:

```text
200 historical rows
```

or if the duplicate query reports:

```text
duplicate_history > 0
```

It also fails if equivalent already-accepted observations are treated as incompatible conflicts.

For an exact equivalent replay, the desired semantics are:

```text
First write  → NewlyAccepted
Second write → Idempotent
```

---

## 28. Conflict Is Different From Idempotency

Consider:

```text
Existing:
AAPL + instant T + price 100

Incoming:
AAPL + instant T + price 100
```

Expected:

```text
Idempotent
```

But:

```text
Existing:
AAPL + instant T + price 100

Incoming:
AAPL + instant T + price 101
```

represents different information for the same logical historical identity.

Expected semantics:

```text
Conflict
```

The platform should not silently convert conflicting history into an idempotent result.

The distinction is:

```text
Same identity + same value
        ↓
Idempotent

Same identity + different value
        ↓
Conflict
```

---

# Part V — Evidence Capture

## 29. Recommended Proof Screenshot 1

Capture the first execution showing:

```text
Observation count: 100
Persistence outcome: NewlyAccepted
```

If the current application emits per-observation counters, capture:

```text
NewlyAccepted: 100
Idempotent: 0
```

Do not expose the API key.

---

## 30. Recommended Proof Screenshot 2

Capture the second execution showing:

```text
Observation count: 100
Persistence outcome: Idempotent
```

or equivalent aggregate evidence.

The screenshot should clearly show that the same database was reused.

---

## 31. Recommended Proof Screenshot 3

Capture the combined SQLite query:

```text
total_rows  distinct_logical_history  duplicate_history
----------  ------------------------  -----------------
100         100                       0
```

This is the independent proof that persistence did not duplicate history.

---

## 32. Ideal Portfolio Evidence

The strongest concise evidence can be presented as:

```text
IDEMPOTENCY PROOF

First execution
----------------
100 observations
100 NewlyAccepted

Second execution
-----------------
100 observations
100 Idempotent

SQLite verification
-------------------
100 total rows
100 distinct logical observations
0 duplicate history

RESULT: PASS
```

This demonstrates a meaningful production-oriented persistence property rather than merely showing that an API request returned data.

---

# Part VI — Troubleshooting

## 33. Second Execution Produces New Records

If the second execution contains `NewlyAccepted` observations, first determine whether the input dataset actually changed.

Possible reason:

```text
Execution 1:
latest observations T1 ... T100

time passes

Execution 2:
latest observations T2 ... T101
```

The second provider response is no longer exactly the same dataset.

This is not necessarily an idempotency failure.

Compare the logical identities stored in each run.

For a deterministic proof, use a fixed historical range or a deterministic fixture at the persistence boundary.

---

## 34. Database Count Becomes 200

If the same exact 100 logical observations produce 200 rows, investigate:

- logical key enforcement;
- persistence lookup behavior;
- transaction behavior;
- schema constraints;
- timestamp normalization;
- target normalization;
- mapping differences between executions.

Run:

```sql
SELECT
    target,
    instant_utc_ticks,
    COUNT(*) AS occurrences
FROM historical_observations
GROUP BY target, instant_utc_ticks
HAVING COUNT(*) > 1;
```

Any returned rows represent duplicate logical history according to the current identity model.

---

## 35. Second Run Uses a Different Database

Check:

```powershell
$env:Persistence__DatabasePath
```

The value must be identical for both executions.

Do not recreate `$databasePath` to point somewhere else between runs.

The proof requires:

```text
Execution 1 ─┐
             ├──► SAME DATABASE
Execution 2 ─┘
```

---

## 36. Database Was Accidentally Deleted

If the database is removed between executions:

```text
Execution 1
    ↓
100 NewlyAccepted

Database deleted

Execution 2
    ↓
100 NewlyAccepted
```

That does not test idempotency.

It tests two independent first-time persistence operations.

Restart the proof from Step 9.

---

## 37. Provider Dataset Changed Between Runs

For real-provider acquisition, "latest N observations" can change over time.

If exact reproducibility is required, the preferred future operational capability is:

```text
Fixed target
+
Fixed historical start/end
+
Fixed interval
        ↓
Deterministic acquisition set
```

Alternatively, persistence idempotency can be proven directly through an integration test that supplies the exact same 100 observations twice.

That is a stronger automated regression test.

---

# Part VII — Repeatable Demonstration

## 38. One-Pass Checklist

- [ ] Run `./eng/verify.ps1`.
- [ ] Configure a disposable SQLite database path.
- [ ] Configure the provider API key.
- [ ] Ensure the acquisition produces 100 observations.
- [ ] Delete the proof database before the **first** execution only.
- [ ] Run the Worker.
- [ ] Verify 100 observations were presented.
- [ ] Verify 100 were newly accepted.
- [ ] Query SQLite.
- [ ] Verify total history = 100.
- [ ] Verify duplicate history = 0.
- [ ] Keep the database unchanged.
- [ ] Run the same acquisition again.
- [ ] Verify the same 100 logical observations were presented.
- [ ] Verify 100 are idempotent.
- [ ] Query the same SQLite database again.
- [ ] Verify total history remains 100.
- [ ] Verify distinct logical history = 100.
- [ ] Verify duplicate history = 0.
- [ ] Run `PRAGMA integrity_check;`.
- [ ] Capture safe evidence.
- [ ] Check `git status`.
- [ ] Run `./eng/verify.ps1` again.

---

## 39. Compact PowerShell Execution Sequence

```powershell
# Repository baseline
./eng/verify.ps1

# Runtime directory
New-Item -ItemType Directory -Force -Path ".local\data" | Out-Null

# IMPORTANT: both executions use this exact database
$databasePath = (Resolve-Path ".local\data").Path + "\idempotency-proof.db"

$env:Persistence__DatabasePath = $databasePath
$env:TwelveData__ApiKey = "YOUR_TWELVE_DATA_API_KEY"

# Clean state BEFORE FIRST EXECUTION ONLY
Remove-Item $env:Persistence__DatabasePath -ErrorAction SilentlyContinue

Write-Host ""
Write-Host "=== FIRST EXECUTION ==="
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

Write-Host ""
Write-Host "Database after first execution:"
Get-Item $env:Persistence__DatabasePath

# DO NOT DELETE OR CHANGE THE DATABASE HERE

Write-Host ""
Write-Host "=== SECOND EXECUTION ==="
dotnet run --project ./src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj

Write-Host ""
Write-Host "Database after second execution:"
Get-Item $env:Persistence__DatabasePath

git status
./eng/verify.ps1

Remove-Item Env:TwelveData__ApiKey -ErrorAction SilentlyContinue
Remove-Item Env:Persistence__DatabasePath -ErrorAction SilentlyContinue
```

---

## 40. Compact SQLite Proof

After the second execution:

```powershell
sqlite3 $databasePath
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
     )) AS duplicate_history;

PRAGMA integrity_check;

.quit
```

Target result:

```text
total_rows  distinct_logical_history  duplicate_history
----------  ------------------------  -----------------
100         100                       0

integrity_check
---------------
ok
```

---

## 41. Definition of Done

The proof is complete when:

```text
[PASS] First execution receives 100 observations
[PASS] First execution classifies 100 as NewlyAccepted
[PASS] Database contains 100 historical observations
[PASS] First execution leaves 0 duplicate logical history

[PASS] Second execution targets the same database
[PASS] Second execution receives the same 100 logical observations
[PASS] Second execution classifies 100 as Idempotent
[PASS] Database still contains exactly 100 historical observations
[PASS] Database contains exactly 100 distinct logical observations
[PASS] Database contains 0 duplicate logical history

[PASS] SQLite integrity check succeeds
[PASS] No credentials are committed
[PASS] Repository verification still succeeds
```

The final proof is:

```text
┌─────────────────────────────┐
│       FIRST EXECUTION       │
│                             │
│  100 observations           │
│          ↓                  │
│  100 NewlyAccepted          │
└─────────────┬───────────────┘
              │
              ▼
        SQLite: 100
              │
              ▼
┌─────────────────────────────┐
│       SECOND EXECUTION      │
│                             │
│  Same 100 observations      │
│          ↓                  │
│  100 Idempotent             │
└─────────────┬───────────────┘
              │
              ▼
        SQLite: 100
              │
              ▼
      Duplicate history: 0
              │
              ▼
        IDEMPOTENCY PROVEN
```

---

## 42. Result

This guide proves a production-relevant property of the AIQuantTradingResearch persistence architecture:

```text
Retries are safe.
```

More precisely:

```text
Equivalent historical observations
        ↓
Repeated persistence attempts
        ↓
Same durable final state
```

The progression of operational evidence is now:

```text
1. REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md

Twelve Data
     ↓
Historical observations acquired


2. DURABLE_PERSISTENCE_SQLITE.md

Historical observations
     ↓
SQLite
     ↓
Durability verified


3. IDEMPOTENCY_PROOF.md

Same observations persisted twice
     ↓
First: NewlyAccepted
     ↓
Second: Idempotent
     ↓
0 duplicate history
```

Together, these guides demonstrate that the platform does not merely acquire real market data—it begins to provide the reliability properties required for repeatable quantitative research.

---

## References

Related guides:

```text
docs/guides/REAL_PROVIDER_ACQUISITION_TWELVE_DATA.md
docs/guides/DURABLE_PERSISTENCE_SQLITE.md
```

AIQuantTradingResearch repository:

https://github.com/samuel-santos-engineer/AIQuantTradingResearch

SQLite documentation:

https://www.sqlite.org/docs.html

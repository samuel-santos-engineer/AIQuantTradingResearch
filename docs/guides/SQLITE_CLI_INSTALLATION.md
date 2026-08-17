# SQLite CLI Installation

This guide installs and verifies the SQLite command-line interface (`sqlite3`) used by the AIQuantTradingResearch verification guides to inspect persisted SQLite databases independently of the application.

> [!IMPORTANT]
> The SQLite CLI is a **developer verification tool**. AIQuantTradingResearch does not require the CLI to run its SQLite persistence implementation, and SQLite CLI binaries should **not** be committed to this repository.

## When You Need This Guide

Follow this guide before running any verification guide that invokes:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

The CLI provides independent storage-level evidence for persistence properties such as idempotency, data integrity, durability, and restart recovery.

## Windows x64 Installation

### 1. Download the official SQLite tools

Open the official SQLite download page:

[Official SQLite Download Page](https://sqlite.org/download.html)

Under **Precompiled Binaries for Windows**, download the current package matching:

```text
sqlite-tools-win-x64-*.zip
```

Use the official SQLite distribution rather than committing a downloaded tools archive or executable to this repository.

### 2. Extract the tools outside the repository

Create a local tools directory, for example:

```text
C:\tools\sqlite
```

Extract the downloaded ZIP into that directory.

After extraction, confirm that `sqlite3.exe` is present. Depending on how the ZIP was extracted, it may be directly under `C:\tools\sqlite` or inside a versioned subdirectory.

Example:

```text
C:\tools\sqlite\sqlite3.exe
```

> [!NOTE]
> Keep the SQLite tools outside the AIQuantTradingResearch repository. They are machine-specific developer tooling, not repository source or a Release 1.1 runtime artifact.

### 3. Add SQLite to the current PowerShell session

If `sqlite3.exe` is located directly in `C:\tools\sqlite`, run:

```powershell
$env:Path += ";C:\tools\sqlite"
```

This updates `PATH` only for the current PowerShell session.

If you extracted the tools to a different directory, substitute that directory in the command above.

### 4. Verify the installation

Run:

```powershell
sqlite3 --version
```

The exact version can change as SQLite publishes new releases. The important result is that PowerShell recognizes `sqlite3` and prints version information instead of reporting that the command is not recognized.

You can also confirm which executable PowerShell resolves:

```powershell
Get-Command sqlite3
```

## Opening the AIQuantTradingResearch Database

The verification guides configure the active SQLite database through:

```text
Persistence__DatabasePath
```

Confirm that the variable is populated:

```powershell
$env:Persistence__DatabasePath
```

Confirm that the configured database exists:

```powershell
Test-Path $env:Persistence__DatabasePath
```

Expected result after the platform has created the database:

```text
True
```

Then open the configured database:

```powershell
sqlite3 $env:Persistence__DatabasePath
```

The SQLite interactive prompt should appear.

At the prompt, you can verify the database connection with:

```text
.databases
```

Exit the CLI with:

```text
.quit
```

Return to the verification guide you were following and continue with its documented SQL inspection steps.

## Optional: Make SQLite Available in Future PowerShell Sessions

The session-only `PATH` change above is sufficient for the project verification guides.

If you choose to add SQLite permanently to your Windows user `PATH`, add the directory containing `sqlite3.exe` through Windows **Environment Variables** rather than copying the executable into the repository.

After changing the persistent `PATH`, open a new PowerShell window and verify again:

```powershell
sqlite3 --version
```

## Troubleshooting

### `sqlite3` is not recognized

Check that `sqlite3.exe` exists:

```powershell
Test-Path "C:\tools\sqlite\sqlite3.exe"
```

If the result is `True`, add that directory to the current session:

```powershell
$env:Path += ";C:\tools\sqlite"
```

Then retry:

```powershell
sqlite3 --version
```

If the executable is in another directory, use that directory instead.

### The database path is empty

If this returns no value:

```powershell
$env:Persistence__DatabasePath
```

return to the verification guide you are following and execute its database-path configuration step before opening SQLite.

### The database does not exist

If:

```powershell
Test-Path $env:Persistence__DatabasePath
```

returns `False`, do not create an empty database merely to continue the proof. Return to the verification guide and complete the application execution step that is expected to create or populate the database.

## Security and Repository Hygiene

- Download SQLite CLI binaries from the official SQLite distribution.
- Keep downloaded archives and executables outside this repository.
- Do not commit `sqlite3.exe`, `sqlite-tools-*.zip`, or other SQLite tool binaries.
- Do not commit API keys or other secrets while configuring or verifying the platform.
- Use the database path defined by the verification guide so that the evidence corresponds to the intended proof scenario.

## References

- [SQLite Download Page](https://sqlite.org/download.html)
- [SQLite Command-Line Shell](https://sqlite.org/cli.html)
- [SQLite Quick Start](https://sqlite.org/quickstart.html)

---

**Next step:** return to the AIQuantTradingResearch verification guide that directed you here and continue from its SQLite inspection step.

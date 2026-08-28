# Release 1.9 Showcase — Real-Time Financial Data Visualization

> **Release 1.9 is a milestone release for AIQuantTradingResearch.**  
> It connects the existing deterministic .NET research pipeline to a governed Python/Streamlit presentation layer without weakening the architecture boundaries established by earlier releases.

This guide shows how to understand, verify, and run the Release 1.9 visualization path locally.

---

## 1. What Release 1.9 delivers

Release 1.9 adds a real-time-style financial visualization experience on top of the existing research platform while preserving the project's explicit layer ownership.

The production boundary is:

```text
Deterministic replay / historical composition
                |
                v
        .NET Application pipeline
                |
                v
      Visualization read model
                |
                v
     Atomic JSON file publication
                |
                v
          Python WP05 parser
                |
                v
          WP06 frame projection
                |
                v
      WP07 presentation projection
                |
                v
             Streamlit
```

The important architectural rule is simple:

- **.NET produces the canonical visualization handoff.**
- **Python/Streamlit consumes and presents it.**
- Streamlit does not read SQLite directly.
- Streamlit does not call market-data providers directly.
- The presentation layer does not supervise the Worker process.
- The Release 1.8 JSON-over-stdio capability endpoint is separate from the Release 1.9 visualization transport.

Release 1.9 therefore demonstrates a cross-language system without introducing an ad-hoc application API or collapsing infrastructure boundaries.

---

## 2. Important data disclaimer

Release 1.9 visualization and demonstration flows use **deterministic simulated/replay data where applicable**.

They are intended for:

- local demonstration;
- deterministic testing;
- presentation development;
- integration validation;
- architecture verification.

They are **not a live market-data feed**, broker connection, or production trading-execution system.

The dashboard should be understood as a governed presentation of research-state evidence, not as a claim of live trading connectivity.

---

## 3. Release 1.9 at a glance

The accepted Release 1.9 baseline is:

| Area | Accepted state |
|---|---:|
| .NET tests | 339 / 339 |
| Python tests | 17 / 17 |
| WP08 lifecycle | 18 / 18 |
| WP09 permanent integration states | Ready, WarmUp, Empty, Failed |
| Architecture no-bypass rules | 8 / 8 |
| SQLite persistence schema | v4 |
| Python | 3.13.15 |
| Streamlit | 1.61.1 |
| Build warnings | 0 |
| Build errors | 0 |
| `pip check` | clean |

The published release is tagged as `v1.9.0`.

---

## 4. Repository areas involved

The main Release 1.9 areas are:

```text
AIQuantTradingResearch/
|
|-- src/
|   |-- AIQuantTradingResearch.Domain/
|   |-- AIQuantTradingResearch.Application/
|   |-- AIQuantTradingResearch.Infrastructure/
|   `-- AIQuantTradingResearch.Worker/
|
|-- tests/
|   |-- AIQuantTradingResearch.Domain.Tests/
|   |-- AIQuantTradingResearch.Application.Tests/
|   |-- AIQuantTradingResearch.Infrastructure.Tests/
|   `-- AIQuantTradingResearch.Architecture.Tests/
|
|-- python/
|   `-- presentation/
|
|-- docs/
|   |-- architecture/design/
|   |-- development/
|   |-- guides/
|   `-- roadmap/release-1.9/
|
`-- eng/
```

For deeper details, read:

- `docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md`
- `docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md`
- `docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md`
- `docs/roadmap/release-1.9/RELEASE_1.9_DEFINITION.md`

---

## 5. Prerequisites

Use a PowerShell terminal from the repository root.

### .NET

The repository pins its .NET SDK through `global.json`.

Verify:

```powershell
dotnet --version
```

Use the SDK required by the repository rather than installing an arbitrary newer SDK.

### Python

Release 1.9 was accepted with:

```text
Python 3.13.15
```

Verify:

```powershell
.\.venv\Scripts\python.exe --version
```

Then follow the canonical Python setup instructions in:

```text
docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md
```

Do not improvise package versions if that guide defines exact pins.

### Streamlit

Verify the accepted version:

```powershell
python -m streamlit version
```

Release 1.9 was accepted with:

```text
Streamlit 1.61.1
```

### Python dependency health

```powershell
python -m pip check
```

Expected:

```text
No broken requirements found.
```

---

## 6. Windows Smart App Control note

Some Windows systems with Smart App Control / Application Control enabled can block locally built unsigned .NET test assemblies.

The repository contains a dedicated guide:

```text
docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md
```

The local signing mechanism is:

- opt-in;
- Debug/local-development only;
- based on local Authenticode signing;
- not production code signing;
- not public trust;
- not a recommendation to disable Windows security.

`Directory.Build.local.props` is intentionally local/ignored configuration and should not be committed.

If you do not encounter App Control blocking, you do not need to enable local signing.

---

## 7. Verify the repository before running the showcase

From the repository root, run the normal repository quality workflow.

On Windows:

```powershell
.\eng\restore.ps1
.\eng\format.ps1
.\eng\build.ps1
.\eng\test.ps1
```

The repository also provides its canonical verification entry point:

```powershell
.\eng\verify.ps1
```

For the published Release 1.9 baseline, the expected result is:

```text
Build:          0 warnings / 0 errors
.NET tests:     339 / 339
Python tests:   17 / 17
pip check:      clean
```

If your branch contains later work, exact counts may legitimately be higher. What matters is that the Release 1.9 tests remain green and no predecessor test is removed or skipped.

---

## 8. The four presentation states

Release 1.9 permanently proves four deterministic states.

### Ready

`Ready` means the read model contains sufficient deterministic evidence for the normal visualization.

The accepted source is the real replay-origin path.

Conceptually:

```text
Replay
  -> pipeline
  -> visualization read model
  -> canonical JSON handoff
  -> Python parser
  -> frame
  -> presentation sections
  -> Streamlit
```

### WarmUp

`WarmUp` represents a valid publication that does not yet contain enough evidence for the fully ready presentation.

It also originates through the normal replay flow.

### Empty

`Empty` is a valid semantic state rather than a transport failure.

Its canonical source is the existing historical-composition path, which can validly produce an empty presentation result.

### Failed

`Failed` represents the canonical safe presentation of a failed pipeline result.

It is intentionally different from:

- malformed JSON;
- a missing handoff file;
- transport warnings;
- a Streamlit rendering exception.

The failure remains a semantic read-model state.

---

## 9. The atomic handoff

The .NET side publishes the visualization read model through the governed file publisher.

Publication is atomic: the consumer should see either the previous complete publication or the next complete publication, not partially written JSON.

Release 1.9 also includes the Windows replacement-contention robustness fix. The publisher retries only transient:

- `UnauthorizedAccessException`;
- `IOException`;

during replacement contention, with a bounded retry window of approximately 200 ms.

Persistent replacement failures still surface.

This is important on Windows because a reader may briefly retain an old file handle while the producer atomically replaces the canonical handoff.

The retry improves OS-level robustness without changing the observable presentation contract.

---

## 10. Start the Python presentation environment

Open a terminal in the repository root and activate the Python environment described in:

```text
docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md
```

Verify from the repository root:

```powershell
.\.venv\Scripts\python.exe --version
.\.venv\Scripts\python.exe -m streamlit version
.\.venv\Scripts\python.exe -m pip check
```

Before launching Streamlit directly, run the permanent presentation tests:

```powershell
Push-Location .\python\presentation
..\..\.venv\Scripts\python.exe -m unittest discover -p "test_*.py"
Pop-Location
```

If the guide defines narrower canonical test commands, prefer those commands.

Release 1.9's accepted governed Python total is 17 tests.

---

## 11. Launch the Streamlit presentation

The Release 1.9 Streamlit application lives under:

```text
python/presentation/
```

Use the presentation entry point defined in the current repository.

For the Release 1.9 layout, the expected command shape is:

```powershell
.\.venv\Scripts\python.exe -m streamlit run python/presentation/realtime_financial_visualization.py
```

If the current checkout uses a renamed presentation entry point, use the exact application file documented in:

```text
docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md
```

Streamlit normally reports a loopback URL such as:

```text
http://localhost:8501
```

Keep this terminal open.

The Streamlit process is an **independent consumer**. It does not own or start the .NET Worker.

---

## 12. Start the .NET producer

Open a second PowerShell terminal at the repository root.

The Worker project is:

```text
src/AIQuantTradingResearch.Worker/
```

The executable entry point is the normal Worker project:

```powershell
dotnet run --project .\src\AIQuantTradingResearch.Worker\AIQuantTradingResearch.Worker.csproj
```

Release 1.9 configuration determines the selected deterministic/replay execution and the canonical visualization handoff location.

Use the normal Release 1.9 configuration described by the repository documentation. The Worker and Streamlit consumer must point at the same governed handoff location.

Do **not** use WP08 acceptance-only command-line seams as ordinary application startup commands. Those switches exist for governed lifecycle testing rather than normal user operation.

---

## 13. What you should see

Once a canonical publication exists, Streamlit consumes it through the Python presentation chain.

A successful demonstration should make the architecture visible:

```text
Worker produces publication P1
        |
        v
Streamlit renders P1
        |
        v
Worker replay progresses
        |
        v
Worker publishes newer P2
        |
        v
Streamlit refreshes
        |
        v
P2 is rendered
```

The important evidence is not simply that “the page changes.”

Release 1.9 acceptance proved:

- the publication is produced by the real Worker flow;
- the newer publication is genuinely newer;
- the presentation chain consumes the same publication identity;
- the refresh is bounded;
- the consumer does not bypass the handoff;
- shutdown and restart do not accept stale Worker-A evidence as Worker-B readiness.

---

## 14. Demonstrating refresh behavior

Keep Streamlit open while the Worker produces the deterministic replay publications.

Observe the presentation as the read model moves between governed states.

The Release 1.9 lifecycle contract proved a bounded newer-publication observation rather than an arbitrary infinite refresh loop.

For demo purposes, focus on:

1. initial presentation availability;
2. a genuine newer publication;
3. Streamlit observing the newer publication;
4. stable rendering;
5. clean Worker shutdown.

Avoid turning the demo into a high-frequency polling benchmark. Release 1.9 establishes deterministic refresh semantics, not a market-data streaming engine.

---

## 15. Worker shutdown and restart

Release 1.9 includes explicit lifecycle acceptance.

The important restart sequence is:

```text
Worker A
   |
   | produces accepted publication
   v
graceful cancellation
   |
   v
Worker A exit = 0
   |
   v
Worker A process resources disposed
   |
   v
Worker B starts
   |
   v
new publication must be observed
```

A stale Worker-A handoff must not be mistaken for Worker-B readiness.

The publisher owns startup/session cleanup of the canonical handoff according to the accepted lifecycle contract.

On Windows, the WP08 acceptance harness also proves targeted `CTRL_BREAK` graceful cancellation for the isolated Worker process group.

That harness behavior is test infrastructure, not a requirement that end users manually reproduce native console-control mechanics for every local demo.

---

## 16. Stop the showcase

Stop the Worker first using its normal graceful console termination path.

Then stop Streamlit with:

```text
Ctrl+C
```

After a normal local run, check that you do not leave unexpected instances running.

PowerShell examples:

```powershell
Get-Process dotnet -ErrorAction SilentlyContinue
Get-Process python -ErrorAction SilentlyContinue
```

Do not terminate unrelated `dotnet` or Python processes simply because they exist.

The governed automated lifecycle tests perform much stricter process/listener/residue ownership checks.

---

## 17. Architecture boundaries worth demonstrating

Release 1.9 is particularly useful as an engineering showcase because the UI was added **without bypassing the architecture**.

### The presentation does not read SQLite

Bad architecture would be:

```text
Streamlit -> SQLite
```

Release 1.9 keeps:

```text
SQLite / pipeline evidence
        |
        v
.NET-owned read model
        |
        v
canonical JSON handoff
        |
        v
Python presentation
```

### The presentation does not call the provider

Bad architecture would be:

```text
Streamlit -> Twelve Data
```

The presentation consumes already-governed research evidence.

### Streamlit does not supervise the Worker

The two processes are independently owned.

This avoids turning a presentation framework into process orchestration infrastructure.

### The Release 1.8 endpoint is not reused as a generic bridge

Release 1.8's governed JSON-over-stdio capability remains separate.

Release 1.9 did not turn it into an arbitrary .NET/Python RPC channel.

---

## 18. Why the WP05 -> WP06 -> WP07 split matters

The Python presentation path intentionally separates concerns.

### WP05 — parse

Transforms the canonical handoff envelope into governed Python semantic input.

### WP06 — visualization frame

Projects the parsed evidence into a stable visualization-oriented frame.

### WP07 — presentation sections

Projects the frame into the exact presentation structure consumed by Streamlit.

This separation gives the UI a semantic pipeline of its own:

```text
Transport
   |
   v
Parse
   |
   v
Frame
   |
   v
Presentation
   |
   v
Render
```

That keeps Streamlit code from becoming a mixture of:

- schema parsing;
- business-state interpretation;
- persistence access;
- formatting;
- lifecycle logic.

---

## 19. Troubleshooting

### Streamlit does not start

Check:

```powershell
.\.venv\Scripts\python.exe --version
python -m streamlit version
python -m pip check
```

Then review:

```text
docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md
```

### Streamlit starts but shows no current publication

Check that:

- the Worker has actually published the canonical handoff;
- Worker and presentation are configured for the same handoff location;
- the handoff is from the current Worker session rather than stale evidence;
- the JSON is complete and valid.

Do not work around the issue by reading SQLite directly from Streamlit.

### Windows blocks a .NET test assembly

Typical symptom:

```text
System.IO.FileLoadException
An Application Control policy has blocked this file.
0x800711C7
```

Read:

```text
docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md
```

Use the opt-in local-development signing workflow if appropriate.

Do not commit local certificate material or `Directory.Build.local.props`.

### Atomic replacement occasionally fails on Windows

Release 1.9 includes a bounded retry for transient replacement contention.

If failures persist beyond the bounded window, treat them as a real file-system/access problem rather than increasing retry duration casually.

Check for:

- antivirus/security software;
- unexpected readers;
- permissions;
- files opened without delete sharing;
- non-governed tools holding the handoff.

### `pip check` fails

Do not “fix” the environment with arbitrary upgrades.

Return to the pinned Python environment instructions and restore the governed dependency set.

### Dashboard appears to show old data

A valid file is not automatically a new publication.

Restart/readiness semantics require the new Worker session to produce genuinely new publication evidence.

---

## 20. Recommended showcase script

For a technical demonstration, use this narrative.

### Step 1 — explain the architecture

Show:

```text
.NET producer -> atomic JSON handoff -> Python/Streamlit consumer
```

Emphasize no direct SQLite/provider UI bypass.

### Step 2 — show verification

Run or show the accepted repository validation evidence:

```text
.NET: 339/339
Python: 17/17
Architecture: 8/8 no-bypass
Schema: v4
```

### Step 3 — launch Streamlit

Open the presentation and show that it is an independent process.

### Step 4 — launch the Worker

Run the deterministic/replay flow that publishes the read model.

### Step 5 — show state transition

Point out a publication becoming visible and then a genuine newer publication being rendered.

### Step 6 — discuss Ready/WarmUp/Empty/Failed

Explain that they are semantic states, not four unrelated UI hacks.

### Step 7 — stop and restart the Worker

Explain that stale prior-session content is not valid new-session readiness.

### Step 8 — finish with the engineering story

Release 1.9 is not primarily “we added a dashboard.”

The achievement is:

> A deterministic .NET research pipeline can now publish a stable, versioned presentation boundary to an independently owned Python/Streamlit UI, with permanent end-to-end tests, lifecycle/restart acceptance, architecture enforcement, schema preservation, Windows file-publication robustness, and reproducible developer documentation.

---

## 21. Release 1.9 quality story

The release was accepted with:

```text
Domain          11 / 11
Application    125 / 125
Infrastructure 182 / 182
Architecture    21 / 21
-----------------------
.NET total      339 / 339

Python           17 / 17
WP08 lifecycle   18 / 18
WP09 states       4 / 4
No-bypass rules   8 / 8
Schema suites    23 / 23

Build warnings     0
Build errors       0
pip check       clean
```

This makes Release 1.9 a useful reference for both quantitative-research architecture and AI-assisted software-engineering governance.

---

## 22. What Release 1.9 deliberately does not claim

Release 1.9 does not mean the platform now has:

- live streaming market feeds;
- broker order execution;
- production trading;
- direct Streamlit provider integration;
- direct Streamlit SQLite access;
- a general-purpose .NET/Python RPC framework;
- distributed process orchestration;
- cloud deployment;
- high-frequency data transport.

Those can be addressed by later releases without compromising the boundaries established here.

---

## 23. Useful follow-up reading

Start with:

```text
README.md
```

Then:

```text
docs/architecture/design/DOTNET_PYTHON_INTEROPERABILITY.md
docs/guides/PYTHON_DEVELOPER_ENVIRONMENT.md
docs/development/WINDOWS_SMART_APP_CONTROL_LOCAL_SIGNING.md
docs/project/ROADMAP.md
```

For engineering/governance history:

```text
docs/roadmap/release-1.9/
```

The roadmap artifacts are intentionally detailed: Release 1.9 was built through explicit contracts, bounded work-package authorities, executable acceptance gates, and independently verified lifecycle transitions.

---

## 24. Suggested destination

Save this guide in the repository as:

```text
docs/guides/RELEASE_1.9_SHOWCASE_AND_LOCAL_RUN_GUIDE.md
```

Then add a concise link from the root `README.md` or the guides index in a separate documentation change if desired.

---

# Release 1.9 in one sentence

**Release 1.9 turns the deterministic AIQuantTradingResearch pipeline into an independently consumable, cross-language financial visualization system while preserving the architecture, lifecycle, security, reproducibility, and testability of the underlying research platform.**

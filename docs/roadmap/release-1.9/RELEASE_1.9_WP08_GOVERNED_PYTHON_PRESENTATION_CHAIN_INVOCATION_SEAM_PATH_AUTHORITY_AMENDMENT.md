# Release 1.9 WP08 — Governed Python Presentation-Chain Invocation Seam

Status: normative definition-only path amendment. Implementation requires a
fresh WP08 authority. This amendment adds one bounded acceptance probe and no
production transport or presentation semantics.

## Purpose

The WP08 C# harness currently proves the real Worker handoff but cannot invoke
the existing Python presentation chain. The authorized probe closes only that
test/demo evidence gap:

`real Worker handoff → WP05 parser → WP06 frame projection → WP07 section projection → one stdout JSON document`

The probe is not imported by Worker or Streamlit, is not part of the Release 1.8
JSON-over-stdio capability endpoint, and is not a general Python bridge.

## Exact probe path and command

The sole new path is:

`python/presentation/wp08_presentation_chain_probe.py`

The only supported invocation is:

`<repository>/.venv/Scripts/python.exe python/presentation/wp08_presentation_chain_probe.py --handoff <absolute-harness-owned-handoff-path>`

The probe accepts exactly one required `--handoff` value. It rejects missing,
relative, nonexistent, directory, extra, source, module, function, provider,
database, or expected-value arguments. The harness supplies the already-owned
absolute canonical handoff path and uses the existing `.venv` interpreter
resolution. No new configuration or environment variable is introduced.

The probe performs one read/project/emit operation and exits within a 2-second
probe bound. It never polls, waits for revisions, launches Streamlit or Worker,
supervises a process, accesses SQLite/provider state, writes files, or uses
`eval`, `exec`, dynamic imports, or shell execution.

## Exact delegation

The probe must delegate without duplication:

1. `visualization_read_model.parse_envelope(text)` reads and validates the
   canonical handoff using WP05 semantics;
2. `realtime_financial_visualization.project_visualization_frame(envelope)`
   performs the accepted WP06 projection; and
3. `realtime_financial_visualization.project_wp07_presentation_sections(frame)`
   performs the accepted WP07 projection.

The probe may adapt returned objects only for evidence serialization. It may not
reimplement parser validation, revision comparison, frame construction, feature
logic, formatting, section labels, or status semantics.

## Evidence JSON

On success stdout contains exactly one UTF-8 JSON object followed by a newline,
with no prose or second document:

```json
{
  "contract": "aiq-wp08-presentation-chain-probe-v1",
  "source": {
    "contractVersion": "aiq-visualization-read-model-v1",
    "revisionKind": "ReplayLogicalTick",
    "revisionValue": 1,
    "revisionIdentity": "<existing envelope revision identity>",
    "sourceMode": "Replay",
    "state": "WarmUp",
    "snapshotIdentity": "<existing snapshot identity or null>",
    "datasetVersion": "<existing dataset version or null>"
  },
  "frame": {
    "revisionKind": "ReplayLogicalTick",
    "revisionValue": 1,
    "revisionIdentity": "<same existing identity>",
    "state": "WarmUp",
    "pointCount": 1,
    "observationCount": 1,
    "windowCount": 1,
    "windowCapacity": 64,
    "latestSourceTime": "<existing latest source time or null>",
    "latestPrice": "<exact invariant decimal or null>",
    "idempotencyStatus": "<existing frame value>",
    "dataQualityStatus": "<existing frame value>"
  },
  "sections": [
    {"label": "Feature", "rows": [["Feature identity", "..."], ["Feature value", "Unavailable"], ["Observed / required", "1 / 2"]]},
    {"label": "Snapshot", "rows": [["Snapshot identity", "Unavailable"], ["Snapshot version", "Unavailable"]]},
    {"label": "Data Quality", "rows": [["Validation status", "Unavailable"]]},
    {"label": "Pipeline", "rows": [["Pipeline status", "Success"], ["Backend state", "WarmUp"], ["Failure category", "Unavailable"]]},
    {"label": "Idempotency", "rows": [["Persistence disposition", "Unavailable"]]}
  ]
}
```

The example values are illustrative only. The implementation serializes the
actual delegated values and must not inject defaults except those already
returned by the existing modules. `sections` preserves the exact five-section
and row order returned by WP07. Decimal values are invariant strings using the
frame’s exact value; absent optional values use the existing `Unavailable`
representation or JSON `null` only where the source/frame has a nullable
identity/value field and the evidence schema explicitly names `null`.

The `source` and `frame` revision/identity fields must match, and source/frame
snapshot values must be copied from the same parsed envelope/frame. No new
correlation ID is created. The evidence object is test output, not a production
schema or handoff format.

## Exit and diagnostics

- exit `0`: one valid delegated result was emitted;
- exit `2`: invalid CLI/path/input;
- exit `3`: parser/read/projection failure;
- exit `4`: serialization or unexpected probe failure.

For every non-zero exit, stdout contains no successful result. Bounded safe
diagnostics may be written to stderr only. The C# harness captures both streams,
enforces the 2-second timeout, waits for exit, and disposes the owned process.

## Exact C# harness amendment

Only this existing path is amended:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

It may invoke the fixed probe command with the real P1 or P2 handoff path,
capture stdout/stderr, enforce the bound, parse the one evidence object, assert
source/frame identity correlation, and assert the delegated WP05/WP06/WP07
facts. It must not create a generic Python process helper, pass arbitrary module
or function names, use fabricated JSON as the primary input, or change Worker,
Streamlit, or the Windows signal helper.

No dedicated Python probe test path is authorized. The existing WP08 .NET test
fully covers the fixed CLI, success/failure exits, deterministic stdout, actual
handoff input, delegation result, identity correlation, timeout, and process
cleanup. Existing WP05/WP06/WP07 Python test paths remain exclusive to their
owners.

## Security, residue, and protected boundaries

The probe is read-only and leaves no process, listener, temporary output, cache
file, database artifact, or persistent evidence file. It does not expand the
Release 1.8 endpoint, add transport, or change Streamlit. It does not access
provider/database state or reconstruct presentation facts. The existing Worker,
Windows group/CTRL_BREAK helper, Replay/P1/P2, WP05 parser, WP06 frame, WP07
projection, exact pins, schema, and WP09 boundaries remain unchanged.

## Path matrix

| Path | Owner | Amendment | Forbidden |
| --- | --- | --- | --- |
| `python/presentation/wp08_presentation_chain_probe.py` | WP08 | Fixed one-shot CLI probe delegating to the three existing Python symbols and emitting bounded stdout JSON | Generic bridge, duplicated logic, production import, Streamlit/Worker launch, file/database writes |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs` | WP08 | Invoke fixed probe with real handoff, assert JSON correlation/facts, timeout, exit, and residue | New helper path, arbitrary Python execution, WP09, changed predecessor semantics |

Protected unchanged paths include `visualization_read_model.py`,
`realtime_financial_visualization.py`, the Release 1.8 capability endpoint,
Worker JSON/handoff, Windows process-group helper, and all WP05–WP07 exclusive
test paths.

`WP08 PYTHON PRESENTATION-CHAIN INVOCATION SEAM/PATH AMENDMENT MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

`WP08 GOVERNED PYTHON PRESENTATION-CHAIN INVOCATION SEAM DEFINED — FINITE-DEMONSTRATION IMPLEMENTATION MAY RESUME`

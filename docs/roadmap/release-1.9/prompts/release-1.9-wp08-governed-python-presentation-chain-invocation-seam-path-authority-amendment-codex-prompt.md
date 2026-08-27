# Release 1.9 — WP08 Governed Python Presentation-Chain Invocation Seam + Path-Authority Amendment

## Model
Use **GPT-5.6 Luna**.

## Authority
This is a **narrow definition/documentation-only authority** for Release 1.9 WP08, canonical issue **#233**.

It exists solely to define one governed **test/demo-only cross-language invocation seam** by which the existing .NET WP08 lifecycle harness may execute the already-implemented Python presentation chain against the **real Worker-produced canonical handoff**.

No implementation is authorized here.
No GitHub mutation.
No WP09.

---

# Proven blocker

The remaining WP08 finite-demonstration contract requires real execution of:

`Worker-produced handoff`
→ existing WP05 parser
→ existing WP06 frame projection
→ existing WP07 presentation-section projection

The current WP08 .NET harness has no authorized way to invoke those Python presentation modules.

The repository's governed Release 1.8 JSON-over-stdio capability endpoint does not expose the presentation modules and must not be expanded merely for WP08 acceptance.

Ad-hoc Python commands, bridges, fixtures, or ungoverned helpers are forbidden.

Therefore define one narrow acceptance-only seam.

---

# Accepted predecessor state

Preserve as binding predecessor evidence:

## WP08
- focused WP08: **4/4 passed**;
- P1 pass-through;
- genuine P2 from existing Replay;
- P2 within fixed 8-second bound;
- post-P2 hold;
- targeted Windows `CTRL_BREAK_EVENT`;
- graceful Worker exit code 0;
- Windows isolated process-group helper validated.

## .NET
- Application 125;
- Infrastructure 164;
- Domain 11;
- Architecture 13;
- full **313/313**;
- build 0 warnings / 0 errors.

## Python
- WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- Streamlit 1.61.1;
- `pip check` clean.

No existing accepted implementation above is reopened.

---

# Lifecycle state

Expected:
- #233 Open / Backlog.
- #234 Open / Backlog.
- no GitHub mutations from blocked pass.

Verify read-only.

---

# Binding artifacts

Read completely:

1. WP08 lifecycle/bounded-demonstration/process/residue contract.
2. WP08 cancellation/liveness contract.
3. WP08 Windows process-group/CTRL_BREAK helper authority.
4. WP08 liveness-seam/bounded-refresh reconciliation amendment.
5. WP08 finite-demonstration harness completion authority.
6. accepted WP05 parser contract/implementation.
7. accepted WP06 visualization-frame contract/implementation.
8. accepted WP07 semantic exposure and presentation contract/implementation.
9. Release 1.9 manifest/path ownership.
10. Release 1.8 JSON-over-stdio boundary, only to preserve its non-expansion.

---

# Objective

Define exactly one minimal test/demo-only Python invocation seam that:

1. accepts the harness-owned path to a real canonical WP05 handoff file;
2. executes the existing WP05 parser;
3. executes the existing WP06 frame projection;
4. executes the existing WP07 presentation projection;
5. emits one bounded deterministic JSON result over stdout;
6. emits diagnostics only to stderr if needed;
7. exits deterministically;
8. is directly invokable by `WP08LifecycleDemonstrationTests.cs`;
9. introduces no new production transport or presentation semantics.

---

# Phase 0 — Read-only presentation API inspection

Inspect actual Python symbols and signatures in:

- `python/presentation/visualization_read_model.py`;
- `python/presentation/realtime_financial_visualization.py`;
- WP07 presentation projection symbols/tests.

Determine exact existing functions for:

- loading/parsing the canonical handoff;
- obtaining current parsed envelope/read model;
- projecting WP06 `VisualizationFrame`;
- projecting WP07 sections.

Do not duplicate any of them.

No mutation.

---

# Phase 1 — Existing invocation conventions

Inspect repository conventions for invoking Python from .NET tests:

- Python executable resolution;
- working directory;
- environment;
- JSON stdout parsing;
- stderr capture;
- timeout;
- exit code;
- quoting;
- process cleanup.

Reuse conventions where possible.

Do not expand the Release 1.8 capability protocol.

---

# Phase 2 — Seam placement decision

Choose exactly one test/demo-only Python entry point.

Preferred path, subject to manifest conventions:

`python/presentation/wp08_presentation_chain_probe.py`

Alternative exact path may be selected only if existing repository naming/ownership conventions clearly require it.

The seam must be:
- WP08-owned;
- acceptance/demo-only;
- not imported by production Worker;
- not the Streamlit production entry point;
- not part of the Release 1.8 capability endpoint.

Authorize exactly one new Python seam path.

---

# Phase 3 — Invocation contract

Preferred command shape:

`python <probe-path> --handoff <absolute-harness-owned-handoff-path>`

Define exact:
- argument names;
- required/optional arguments;
- working directory assumptions;
- Python executable resolution;
- timeout.

No arbitrary code/module/function arguments.

The seam must not become a general Python execution bridge.

---

# Phase 4 — Input authority

The only semantic input is:

- absolute path to the harness-owned canonical handoff.

Optional non-semantic process inputs may include:
- repository root if unavoidable;
- encoding/timeout only if needed.

Forbidden semantic inputs:
- fabricated envelope JSON;
- frame JSON;
- WP07 section JSON;
- expected values;
- alternate provider/database paths used to reconstruct presentation state.

The probe must read the actual Worker-produced handoff through the existing WP05 parser.

---

# Phase 5 — Existing WP05 parser usage

Define the exact existing parser function/class to call.

Requirements:
- no JSON schema reimplementation;
- no independent field validation;
- no duplicate cache semantics;
- no direct manual `json.load` as a substitute for WP05 parser;
- absent/invalid field behavior comes from WP05.

The probe may adapt the parser's returned object only for passing it to existing WP06 code.

---

# Phase 6 — Existing WP06 projection usage

Define the exact existing `VisualizationFrame` projection symbol.

Requirements:
- call existing implementation;
- no duplicated price/time/window/latest logic;
- no alternate frame type;
- no chart rendering requirement;
- no mutation of WP06 code unless a separate authority is later required.

---

# Phase 7 — Existing WP07 projection usage

Define exact existing:

`project_wp07_presentation_sections(...)`

or actual canonical symbol.

Requirements:
- call existing implementation;
- preserve exact five-section labels/order;
- preserve `Unavailable`;
- preserve state behavior;
- preserve idempotency/data-quality semantics;
- preserve transport-warning separation.

No duplicate presentation formatter.

---

# Phase 8 — Deterministic output schema

Define one narrow JSON stdout result.

It should expose only enough facts to prove chain execution and identity.

Prefer conceptual shape:

{
  "contract": "aiq-wp08-presentation-chain-probe-v1",
  "source": {
    "revision": ...,
    "state": ...,
    "snapshotId": ...,
    "snapshotVersion": ...
  },
  "frame": {
    "revision": ...,
    "state": ...,
    "pointCount": ...,
    "latest": ...,
    "windowCapacity": ...,
    "idempotencyStatus": ...,
    "dataQualityStatus": ...
  },
  "sections": [
    {
      "label": ...,
      "rows": [...]
    }
  ]
}

But do not invent fields.

Derive exact fields from accepted WP05/WP06/WP07 contracts and the minimum WP08 assertions.

The output schema is **test evidence only**, not a production schema.

---

# Phase 9 — Identity correlation

The probe result must expose the minimum existing identifiers needed for the .NET harness to prove that:

- the handoff observed by WP05;
- the frame projected by WP06;
- the sections projected by WP07

all derive from the same real Worker-produced publication.

Prefer existing:
- revision;
- snapshot identity/version;
- Replay logical tick if already present.

Do not add a new correlation ID.

---

# Phase 10 — Serialization rules

Define exact deterministic JSON serialization:

- UTF-8;
- one JSON document on stdout;
- no prose on stdout;
- stable property names;
- stable section/row ordering inherited from WP07;
- invariant numeric formatting;
- null vs `Unavailable` exactly as existing contracts require.

Diagnostics:
- stderr only.

No JSON Lines stream.
No persistent output file.

---

# Phase 11 — Exit-code contract

Define exact exits, preferably:

- `0` = probe completed and emitted valid result;
- non-zero = invocation/parser/projection failure.

If distinguishing failure classes is useful, define a tiny fixed domain.

Do not mirror or create a broad capability protocol.

On non-zero:
- stdout must not contain a successful result;
- stderr may contain bounded safe diagnostics.

---

# Phase 12 — Bounded execution

Define a short probe timeout compatible with the overall WP08 finite demonstration.

The probe:
- reads one current canonical handoff;
- projects once;
- exits.

It must not:
- poll indefinitely;
- run Streamlit;
- wait for future revisions;
- supervise Worker.

The .NET harness remains responsible for choosing the P1/P2 publication to probe.

---

# Phase 13 — .NET harness invocation authority

Amend exact path authority for:

`tests/AIQuantTradingResearch.Infrastructure.Tests/WP08LifecycleDemonstrationTests.cs`

to:
- launch the exact WP08 Python probe;
- pass the harness-owned handoff path;
- capture stdout/stderr;
- enforce timeout;
- parse the probe's narrow evidence JSON;
- assert correlation and accepted WP05/WP06/WP07 facts.

No arbitrary Python invocation helper.

If an existing generic process helper can be used inside the same test file, use it.

No new .NET helper path unless already authorized.

---

# Phase 14 — Python seam test authority

Decide whether the new probe requires one dedicated Python test path.

Preferred if needed:

`python/presentation/test_wp08_presentation_chain_probe.py`

Authorize it only if necessary to deterministically prove:
- CLI argument handling;
- stdout-only JSON;
- non-zero failure behavior;
- exact delegation to existing chain.

If the existing WP08 .NET lifecycle test fully covers the seam and repository governance does not require a Python unit test, explicitly authorize **no additional Python test path**.

Choose one exact rule.

Do not use WP05/WP06/WP07 exclusive test files for WP08 seam tests.

---

# Phase 15 — No production semantic expansion

Explicitly forbid modifications to:

- `visualization_read_model.py` semantics;
- `realtime_financial_visualization.py` WP06/WP07 semantics;
- WP07 canonical statuses;
- Worker JSON;
- handoff contract;
- Streamlit entry point behavior;
- Release 1.8 capability endpoint.

If actual APIs cannot be invoked without changing production symbols, STOP and identify the minimum symbol-access amendment needed.

---

# Phase 16 — Import boundary

The probe may import existing presentation modules.

If current symbols are importable:
- use them unchanged.

If a symbol is nested/private solely inside `main()` and cannot be reused:
- STOP unless existing accepted path authority already permits extracting it without semantic change.

Do not silently refactor WP05/WP06/WP07 under this definition authority.

---

# Phase 17 — Security/scope constraints

The probe must:
- accept only a file path, not Python source;
- never `eval`/`exec`;
- never dynamically import a user-supplied module;
- never invoke shell=True;
- never access network;
- never access provider;
- never query SQLite for reconstruction;
- never write production state.

Read-only handoff consumption only.

---

# Phase 18 — Residue behavior

Probe must leave:
- no process residue;
- no listener;
- no temp output;
- no cache file beyond existing in-memory parser behavior;
- no DB mutation.

The .NET harness must wait for probe exit and dispose the process.

---

# Phase 19 — Platform behavior

The probe itself should remain normal Python and platform-neutral if existing modules permit it.

The enclosing WP08 lifecycle acceptance remains Windows-specific because graceful console-signal proof is Windows-specific.

Do not add Windows logic to the Python probe.

---

# Phase 20 — Path matrix

Produce exact table:

| Path | Owner | Amendment | Allowed symbols/concern | Forbidden concern |

Must include:
1. exact new Python probe path;
2. `WP08LifecycleDemonstrationTests.cs`;
3. optional exact dedicated WP08 Python probe test path, if authorized.

Also list protected unchanged WP05/WP06/WP07 paths.

No wildcard grants.

---

# Phase 21 — Future implementation acceptance

A later Terra implementation must prove:

## Probe focused
- valid real/representative handoff delegates through actual WP05/WP06/WP07 code;
- deterministic JSON stdout;
- bounded stderr;
- exit 0;
- invalid/missing handoff non-zero;
- no residue.

## Real WP08 demonstration
- Worker creates P1/P2;
- harness selects real canonical handoff;
- probe executes;
- result correlates to Worker publication;
- WP05 facts asserted;
- WP06 frame facts asserted;
- WP07 sections asserted;
- Streamlit remains independent;
- lifecycle continues to graceful cancellation/restart/residue.

## Regression
- existing WP05 3/3;
- WP06 6/6;
- WP07 semantic 2/2;
- WP07 presentation 2/2;
- WP08 focused predecessor 4/4 plus authorized seam/harness delta;
- .NET predecessor baseline 313/313 plus only authorized delta;
- build clean;
- Streamlit/pip clean.

---

# Non-goals

Do not authorize:

- Release 1.8 endpoint expansion;
- production .NET→Python API;
- generic Python execution bridge;
- arbitrary module/function invocation;
- new network transport;
- named pipe;
- socket;
- HTTP;
- persistent output/evidence file;
- fabricated fixture as sole WP08 evidence;
- duplicate parser;
- duplicate frame projection;
- duplicate WP07 formatter;
- WP05/WP06/WP07 semantic changes;
- package changes;
- WP09.

---

# Documentation artifact

If governed, create only:

`docs/roadmap/release-1.9/RELEASE_1.9_WP08_GOVERNED_PYTHON_PRESENTATION_CHAIN_INVOCATION_SEAM_PATH_AUTHORITY_AMENDMENT.md`

No production/test/GitHub mutation.

Otherwise return normative definition in chat.

---

# Required completion report

## Blocker confirmation
Why existing .NET harness cannot currently execute the real Python chain.

## Selected seam
Exact Python path and command.

## Existing symbol delegation
Exact WP05/WP06/WP07 symbols called.

## Input contract
Exact handoff-path semantics.

## Output contract
Exact deterministic evidence JSON.

## Identity correlation
Exact existing fields.

## Exit/timeout/stderr
Exact rules.

## .NET path amendment
Exact allowed invocation/assertion surface.

## Python test path
Authorized or explicitly unnecessary.

## Protected paths
WP05/WP06/WP07/Release 1.8 unchanged.

## Path matrix
Exact ownership.

## Future acceptance
Exact tests/regressions.

## Mutation statement

If doc created:

`WP08 PYTHON PRESENTATION-CHAIN INVOCATION SEAM/PATH AMENDMENT MUTATIONS: ZERO production/test/GitHub mutations; one authorized documentation artifact created`

Otherwise:

`WP08 PYTHON PRESENTATION-CHAIN INVOCATION SEAM/PATH AMENDMENT MUTATIONS: ZERO`

## Next step

On success:

`WP08 GOVERNED PYTHON PRESENTATION-CHAIN INVOCATION SEAM DEFINED — FINITE-DEMONSTRATION IMPLEMENTATION MAY RESUME`

---

# Stop conditions

Stop if:

- existing WP05/WP06/WP07 code cannot be invoked without semantic production refactoring;
- a general-purpose bridge is required;
- Release 1.8 endpoint must change;
- a new transport beyond bounded process stdout is required;
- probe must reconstruct from SQLite/provider;
- output requires new presentation semantics;
- package addition is required.

Report the minimum next authority if blocked.

---

# Terminal markers

Success:

`RELEASE 1.9 WP08 GOVERNED PYTHON PRESENTATION-CHAIN INVOCATION SEAM AND PATH-AUTHORITY AMENDMENT COMPLETE`

Blocked:

`RELEASE 1.9 WP08 GOVERNED PYTHON PRESENTATION-CHAIN INVOCATION SEAM AND PATH-AUTHORITY AMENDMENT BLOCKED`

Do not emit COMPLETE unless exactly one bounded acceptance-only Python invocation seam is fully defined, delegates to the existing WP05/WP06/WP07 implementation, and does not create a second production transport or presentation implementation.

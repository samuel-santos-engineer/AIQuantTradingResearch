# Release 1.9 — WP05 Manifest / Path-Authority Amendment — Codex Authority

## Authority

This document grants a **narrow, definition-only manifest/path-authority amendment** for Release 1.9 WP05, canonical GitHub issue **#230**.

WP05 implementation is blocked before mutation because the accepted manifest currently authorizes only:

- `python/presentation/realtime_financial_visualization.py`
- `python/presentation/visualization_read_model.py`
- narrow `Program.cs` composition work

However, the already-defined WP05 contracts require additional implementation surfaces for:

- Worker handoff-path configuration;
- Worker path resolution;
- Worker startup cleanup;
- Worker atomic JSON publisher/integration;
- focused .NET transport tests;
- Python consumer/configuration tests.

Those required paths are not currently authorized by the accepted WP05 manifest, and some may overlap areas otherwise owned by later work packages.

This authority exists only to define the **minimum additional WP05-owned path authority** necessary to implement the already-fixed WP05 design.

It does **not** authorize implementation.

It does **not** authorize WP06 or later work.

It does **not** authorize GitHub lifecycle mutation.

---

# Fixed WP05 Design Inputs

Do not reopen these design decisions.

## Transport

- local atomic JSON file;
- no HTTP/sockets/queues/shared memory;
- Worker writes;
- Streamlit reads only.

## Runtime path

Default:

`<LocalApplicationData>\AIQuantTradingResearch\Release1.9\runtime\visualization-read-model.json`

Override:

`Visualization:HandoffPath`

Environment mapping:

`Visualization__HandoffPath`

## Lifecycle

- Worker and Streamlit independently launched;
- Worker startup removes prior-session canonical handoff;
- Worker owns runtime directory creation and atomic writes;
- Streamlit never writes/deletes canonical file.

## Refresh

- automatic + manual;
- default 2 seconds;
- key `Visualization:RefreshIntervalSeconds`;
- environment `Visualization__RefreshIntervalSeconds`;
- bounds 1–60;
- max 2 reads per cycle;
- one 50 ms retry.

## Consumer boundary

WP05 UI must not:

- access SQLite;
- call providers;
- recompute features;
- mutate producer state.

## Predecessor baseline

Accepted current regression:

- Infrastructure 155/155
- Application 122/122
- Domain 11/11
- Architecture 13/13
- full 301/301
- build 0 errors / 0 warnings

---

# Objective

Define the smallest additional manifest/path scope required for WP05 to implement the fixed design without:

- taking ownership of unrelated Worker architecture;
- stealing later-WP paths;
- broadening into WP06;
- creating generalized runtime infrastructure;
- modifying predecessor contracts unnecessarily.

The amendment must produce an exact allowlist of:

1. production paths;
2. test paths;
3. configuration/composition paths;
4. any explicitly shared path;
5. any path that remains forbidden.

The resulting manifest must be precise enough for a later Terra implementation authority.

---

# Core Principle

Authorize **capability-specific files**, not broad directories.

Prefer:

- one narrow Worker handoff configuration/path file;
- one narrow Worker atomic publisher file;
- one narrow integration/composition touchpoint;
- one focused .NET test file or narrowly scoped existing test file;
- the two already-authorized Python presentation files;
- one narrow Python test file or existing test module.

Do not authorize entire Infrastructure/Application/Worker directories unless the repository manifest convention already requires directory-level ownership.

---

# Permitted Scope

This authority may read:

- #230;
- Release 1.9 manifest/definition;
- WP06–WP12 path ownership manifests;
- current Worker project layout;
- current Infrastructure/Application project layout;
- current test project layout;
- current Python test layout;
- current `Program.cs` composition;
- naming conventions for config/path/publisher classes.

It may define a revised WP05 manifest/path allowlist.

If governance permits a manifest amendment artifact, create only that artifact.

Otherwise return the full normative amended manifest in the completion report.

---

# Explicitly Forbidden

Do not:

- modify production files;
- modify tests;
- modify manifest files in-repo unless this definition authority explicitly owns one amendment artifact;
- change WP05 design semantics;
- reassign later-WP ownership broadly;
- authorize schema/persistence paths;
- authorize provider/data-source paths;
- authorize pipeline algorithm paths;
- authorize WP06 UI/controls/strategy work;
- modify GitHub;
- close #230.

This is definition-only authority.

---

# Phase 0 — Read Current Manifest Ownership

Before defining anything:

1. Read #230 completely.
2. Read the accepted Release 1.9 WP05 manifest.
3. Read WP06–WP12 manifests/path ownership.
4. Identify current ownership of:
   - Worker composition;
   - Worker configuration;
   - runtime path helpers;
   - Infrastructure file IO;
   - transport serialization;
   - test projects;
   - Python presentation tests.
5. Identify any files already existing that are the natural narrow extension points.

Do not mutate anything.

---

# Phase 1 — Required Capability-to-Path Map

Map each fixed WP05 capability to the minimum file surface.

At minimum:

## Handoff configuration

Need:
- path override;
- refresh config where Worker side applies;
- absolute-path validation.

Determine whether this belongs in:
- existing Worker configuration file;
- new WP05-specific configuration type;
- narrow `Program.cs` binding only.

## Path resolution

Need:
- LocalApplicationData default;
- absolute override normalization.

Determine narrow owner/path.

## Atomic JSON publisher

Need:
- serialization;
- temp sibling;
- flush/close;
- atomic replace;
- cleanup.

Determine whether this belongs in:
- Worker project;
- Infrastructure project;
- existing presentation/runtime adapter location.

Prefer the layer consistent with current repository architecture.

## Startup cleanup

Need:
- delete prior canonical file;
- clean owned temp files.

Determine whether this is:
- publisher responsibility;
- Worker startup composition;
- dedicated narrow lifecycle helper.

## .NET tests

Need:
- path/config tests;
- atomic publication tests;
- startup cleanup tests;
- Historical/Replay handoff tests.

Determine exact existing test project/file ownership.

## Python tests

Need:
- parser;
- path config;
- refresh config;
- retry/cache/revision;
- missing/corrupt/version handling.

Determine exact existing Python test location or one new WP05-specific test module.

---

# Phase 2 — Reuse Existing Files Before Adding New Ones

For each capability, prefer an existing file only if:

- its existing responsibility naturally includes the change;
- adding WP05 behavior does not broaden that file into unrelated ownership;
- later-WP ownership is not violated.

Do not overload `Program.cs` with transport implementation.

`Program.cs` should remain narrow composition/wiring only.

If a new file is cleaner and more ownership-safe, define that new path explicitly.

---

# Phase 3 — Shared-Path Rules

If a required file is also legitimately owned by another WP:

- do not transfer ownership;
- define a narrow **shared-path exception** for WP05;
- specify exactly which symbols/sections WP05 may modify;
- state later WP ownership remains intact.

Examples may include:

- existing Worker configuration file;
- existing DI/composition extension;
- existing test fixture.

Shared authority must be symbol/concern-specific where possible.

Do not grant blanket shared-file editing rights.

---

# Phase 4 — Candidate Path Structure

Evaluate repository-consistent candidates for new WP05-specific files.

Examples of semantic roles, not pre-authorized names:

- `VisualizationHandoffOptions.cs`
- `VisualizationHandoffPathResolver.cs`
- `VisualizationReadModelFilePublisher.cs`
- `VisualizationReadModelFilePublisherTests.cs`

Use actual repository naming/layering conventions.

For Python, preserve already-authorized:

- `python/presentation/realtime_financial_visualization.py`
- `python/presentation/visualization_read_model.py`

Add only the narrowest test path needed, such as repository-consistent:

- `python/tests/presentation/test_visualization_read_model.py`
- `tests/python/...`

Do not invent a test tree inconsistent with the repo.

---

# Phase 5 — Define Exact Production Allowlist

Produce an exact list of WP05-authorized production paths.

Each entry must include:

- path;
- ownership type:
  - exclusive WP05;
  - shared narrow exception;
- authorized concern;
- forbidden adjacent concerns.

At minimum the list must cover:

- existing two Python presentation files;
- narrow Program.cs composition touch;
- Worker handoff path/configuration;
- atomic publisher;
- startup cleanup.

If some capabilities can safely coexist in one file, prefer fewer files.

Do not authorize broad wildcard paths unless repository manifest convention requires them.

---

# Phase 6 — Define Exact Test Allowlist

Produce exact WP05-authorized test paths.

Must cover:

## .NET

- configuration/path;
- atomic file publication;
- startup cleanup;
- Historical/Replay transport.

## Python

- parser;
- configuration;
- revision/cache/retry;
- transport error handling;
- Streamlit-consumer logic.

Prefer one or two focused test files rather than broad test-directory ownership.

---

# Phase 7 — Define Explicitly Forbidden Paths

State paths/categories WP05 still may not touch.

At minimum:

- schema/migrations;
- persistence repositories except no changes;
- provider/data-source implementations;
- pipeline algorithm files;
- WP06+ presentation/control files;
- package manifests unless already explicitly WP05-owned;
- Python requirements/pins;
- Streamlit version pin;
- JSON-over-stdio protocol;
- Release planning/manifest files outside this narrow amendment.

---

# Phase 8 — Define Manifest Compatibility

The amended manifest must preserve:

- WP01–WP04 completed ownership;
- WP06–WP12 future ownership;
- release taxonomy/planning;
- dependency chain;
- schema v4 boundary;
- package/Python foundations.

No path amendment may silently move a future deliverable into WP05.

---

# Phase 9 — Implementation Authority Requirements

Define what the later Terra authority may do under the amended manifest.

It may:

- implement only the allowlisted paths;
- add no other files without stopping;
- modify shared paths only within authorized concern;
- run focused/full tests;
- close #230 only after technical acceptance.

If implementation discovers a missing required path not in this amendment:

> stop and request another path amendment.

Do not allow "closest matching file" improvisation.

---

# Phase 10 — Required Future Scope Audit

The later implementation must prove:

- every changed file appears in the amended allowlist;
- every shared-path modification stays within concern;
- no later-WP file changed;
- no schema/persistence/provider/pipeline algorithm change;
- no package/pin change;
- no extra runtime subsystem introduced.

This audit is mandatory before #230 closure.

---

# Non-Goals

This amendment does not authorize:

- implementation;
- new transport semantics;
- new lifecycle semantics;
- new refresh semantics;
- new dependencies;
- broad Infrastructure ownership;
- broad Worker ownership;
- WP06 work;
- schema/persistence changes;
- GitHub lifecycle mutation.

---

# Stop Conditions

Stop if:

- repository path ownership for required capabilities cannot be resolved;
- a required path is exclusively owned by a later WP and no narrow shared exception is governance-safe;
- atomic publisher placement requires broader architecture ownership;
- test path conventions are ambiguous enough to create incompatible layouts;
- more than one materially different manifest structure remains equally valid.

On stop:

- make zero production/test/GitHub changes;
- report exact unresolved path/ownership conflict;
- identify the minimum additional governance decision required.

---

# Success Criteria

This amendment succeeds only when one unambiguous WP05 manifest/path authority is established that defines:

- exact production allowlist;
- exact test allowlist;
- exact shared-path exceptions;
- exact forbidden paths;
- concern ownership per path;
- later-WP ownership preservation;
- implementation stop rule for missing paths;
- mandatory final scope audit.

No implementation occurs.

No GitHub mutation occurs.

#229 remains Closed / Done.

#230 remains Open / Backlog.

WP06 remains unstarted.

---

# Required Completion Report

Return:

## Existing authorized paths
List current WP05 paths retained.

## Added production paths
For each:
- exact path;
- exclusive/shared;
- authorized concern.

## Added test paths
For each:
- exact path;
- authorized concern.

## Shared-path exceptions
For each:
- path;
- exact symbol/concern allowed;
- ownership preserved for other WPs.

## Explicitly forbidden paths
List categories/paths still outside WP05.

## Later implementation rule
State exact stop behavior for any non-allowlisted required file.

## Mutation proof

Expected:

`WP05 MANIFEST/PATH-AUTHORITY AMENDMENT MUTATIONS: ZERO`

## Next step

On success state exactly:

`WP05 MANIFEST/PATH AUTHORITY AMENDED — IMPLEMENTATION REQUIRES FRESH CONSOLIDATED AUTHORITY`

Do not implement here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP05 MANIFEST/PATH-AUTHORITY AMENDMENT COMPLETE`

On blocker:

`RELEASE 1.9 WP05 MANIFEST/PATH-AUTHORITY AMENDMENT BLOCKED`

Emit success only if the amended path authority is fully unambiguous.

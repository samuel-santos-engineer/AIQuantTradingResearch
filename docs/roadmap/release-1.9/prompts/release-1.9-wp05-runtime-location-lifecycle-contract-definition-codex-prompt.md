# Release 1.9 — WP05 Runtime-Location / Lifecycle Contract Definition — Codex Authority

## Authority

This document grants a **very narrow, definition-only authority** for Release 1.9 WP05, canonical GitHub issue **#230**.

The prior WP05 cross-process definition established one clear transport choice:

> **Model A — local atomic JSON file handoff**

However, that definition remained blocked because the repository has no governed convention for:

- runtime handoff directory;
- handoff file name;
- path override/configuration key;
- directory ownership;
- Worker/Streamlit startup relationship;
- shutdown/file-retention behavior;
- stale prior-session file handling;
- cross-process lifecycle ownership.

Current proven state:

- no WP05 repository mutation occurred;
- no runtime file was created;
- no configuration was changed;
- no tests were changed;
- no GitHub mutation occurred;
- #230 remains Open / Backlog;
- WP06 remains unstarted;
- WP04 envelope/version/revision/state semantics remain fixed;
- local atomic JSON remains the selected transport model.

This authority exists only to define the missing **runtime location and lifecycle contract**.

It does **not** authorize implementation.

It does **not** authorize Streamlit coding.

It does **not** authorize Worker coding.

It does **not** authorize GitHub lifecycle mutation.

---

# Fixed Inputs

The following decisions are already settled and must not be reopened.

## Transport

Local atomic JSON file handoff.

## Serialization

The cross-process file transports the existing WP04 envelope.

Contract version:

`aiq-visualization-read-model-v1`

No second presentation schema.

## Publication protocol

Conceptually:

1. serialize complete envelope;
2. write sibling temp file;
3. flush;
4. close;
5. atomically replace canonical destination;
6. consumer sees old complete or new complete only.

No in-place overwrite.

## Consumer

Streamlit is read-only.

It must not:

- modify handoff;
- write acknowledgement;
- mutate Worker state;
- access SQLite;
- call providers;
- recompute features;
- synthesize revisions.

## No bidirectional IPC

No commands, sockets, HTTP API, queues, or acknowledgements.

---

# Objective

Define one explicit runtime contract covering only:

1. canonical handoff directory;
2. canonical handoff filename;
3. exact path derivation;
4. exact override/configuration key, if any;
5. Worker directory ownership;
6. Streamlit directory/file ownership;
7. startup relationship;
8. shutdown relationship;
9. file retention;
10. stale prior-session file handling;
11. new Worker session behavior;
12. missing-file startup behavior;
13. process-independence assumptions;
14. required future tests;
15. explicit non-goals.

The resulting definition must be specific enough for a later WP05 implementation authority.

---

# Core Design Principles

## Local and deterministic

The path must be deterministic on one machine.

## No machine-specific developer path

Do not hard-code a repository clone path or personal directory.

## Explicit ownership

Only one process owns writes and directory lifecycle.

## Independent process safety

A separate Worker and Streamlit process must be able to resolve the same canonical path.

## Session safety

A file from a prior Worker session must not be misinterpreted as current state.

## Minimal configuration

Do not create a broad runtime-path subsystem.

---

# Permitted Scope

This authority may read:

- #230;
- WP05 Release 1.9 definition/manifest;
- Worker startup code;
- Streamlit project layout;
- current appsettings/environment-variable conventions;
- current application-data/temp-directory conventions;
- filesystem helper conventions;
- project/application naming constants;
- process-launch scripts, if any;
- tests revealing runtime-path conventions.

It may define one normative runtime-location/lifecycle contract.

If governance permits one WP05-owned definition artifact, create only that artifact.

Otherwise return the full normative contract in the completion report.

---

# Explicitly Forbidden

Do not:

- create directories;
- create handoff files;
- modify appsettings;
- modify environment files;
- modify Worker;
- modify Streamlit;
- modify tests;
- change WP04 envelope;
- change transport;
- add session identity to WP04 envelope unless this authority proves it is absolutely required and then stops for separate authority;
- modify schema/persistence;
- modify packages/Python/Streamlit version;
- modify GitHub;
- close #230;
- start WP06.

This is definition-only authority.

---

# Phase 0 — Read Existing Path and Lifecycle Conventions

Before defining anything:

1. Read #230 completely.
2. Read WP05 manifest/definition.
3. Read Worker startup/shutdown code.
4. Read Streamlit project/entry structure.
5. Read existing configuration key naming conventions.
6. Read existing filesystem path conventions.
7. Identify whether repository uses:
   - current working directory;
   - app base directory;
   - OS temp directory;
   - local application data;
   - user profile application data;
   - repository-relative runtime folders.
8. Read existing process launch scripts/tools if any.
9. Determine whether Worker and Streamlit are expected to be launched independently.

Do not mutate anything.

---

# Phase 1 — Choose Canonical Base Directory Model

Evaluate narrowly:

## Model A — OS-local application data/runtime directory

Example semantic class:

- per-user local application data;
- stable across process starts;
- writable without repository mutation.

Assess:
- cross-process discoverability;
- cleanup;
- supported OS behavior;
- repository conventions.

## Model B — OS temporary directory with application subfolder

Assess:
- cross-process discoverability;
- lifetime/cleanup;
- stale-file risk;
- deterministic path.

## Model C — Repository-relative runtime directory

Assess:
- repository cleanliness;
- packaging/runtime assumptions;
- working-directory dependence.

## Model D — Existing governed application-data location

Use if repository already defines one.

### Decision rule

Prefer an existing governed location.

If none exists, choose the narrowest OS-standard location suitable for both Worker and Streamlit.

Do not choose based on developer convenience.

### Hard stop

If OS support requirements make the choice materially platform-specific and repository target platforms are unclear, stop.

---

# Phase 2 — Define Canonical Directory and Filename

Define exact semantic values.

The contract must specify:

- application subdirectory name;
- optional release/runtime subdirectory;
- canonical filename;
- temp filename pattern.

Preferred qualities:

- stable;
- filesystem-safe;
- clearly application-owned;
- not user-specific beyond base directory;
- no random canonical filename.

Example shape only, not pre-authorized:

`<base>/<application>/runtime/visualization-read-model.json`

Do not adopt without evidence/rationale.

---

# Phase 3 — Define Path Override Contract

Determine whether override is needed.

If yes, define exactly one configuration key.

Potential semantic shape:

`Visualization:HandoffPath`

or:

`Worker:Visualization:HandoffPath`

Choose based on repository configuration ownership conventions.

Define:

- exact key;
- whether value points to file or directory;
- environment-variable mapping under standard .NET conventions;
- whether relative paths are allowed;
- normalization rules;
- validation behavior;
- fallback to canonical default.

Prefer a single **full file path override** if it minimizes ambiguity.

Do not create multiple aliases.

If no override is needed for #230, state that explicitly.

---

# Phase 4 — Define Directory Ownership

Specify exactly:

## Worker

Worker owns:

- resolving canonical/overridden path;
- creating parent directory if absent;
- ensuring directory exists before first publish;
- writing temp sibling;
- atomic replace;
- cleanup of its own temp artifacts.

## Streamlit

Streamlit:

- resolves the same path;
- never creates the canonical file;
- does not delete it;
- does not modify it;
- may treat missing parent directory/file as "producer not yet available" according to consumer contract.

Decide whether Streamlit may create parent directory merely to avoid errors.

Preferred: **no** — Worker owns creation.

---

# Phase 5 — Define Startup Relationship

Choose exactly one lifecycle model.

## Model A — Independently launched peers

Worker and Streamlit are launched independently by user/external launcher.

Neither starts the other.

## Model B — Worker owns Streamlit startup

Use only if #230 explicitly requires it.

## Model C — Streamlit owns Worker startup

Use only if #230 explicitly requires it.

## Model D — Dedicated launcher owns both

Use only if repository already has such launcher or #230 requires one.

### Preferred direction

Prefer independently launched peers unless accepted Release 1.9 evidence explicitly requires process ownership.

Define whether startup order matters.

For independent peers:

- Worker may start first or second;
- Streamlit must tolerate missing handoff until first publication;
- Worker does not depend on Streamlit availability.

---

# Phase 6 — Define Shutdown Relationship

For the selected lifecycle model, define:

- whether either process signals the other;
- whether Streamlit exit affects Worker;
- whether Worker exit affects Streamlit process lifetime;
- whether Worker deletes handoff file on graceful shutdown;
- whether last valid file remains.

Preferred for independent peers:

- no mutual shutdown signaling;
- Streamlit exit does not stop Worker;
- Worker exit does not forcibly stop Streamlit;
- last valid handoff remains unless session-safety rules require startup replacement/cleanup.

Do not add process control channel.

---

# Phase 7 — Define Prior-Session File Safety

This is critical because HistoricalPresentationRevision resets to `1` per Worker/read-model session.

A persisted handoff file may outlive its producer session.

Choose one model:

## Model A — Worker startup removes canonical prior-session file before any new publication

Rules:
- Worker owns deletion at startup;
- Streamlit sees missing/unavailable until first new envelope;
- new Historical revision `1` cannot be compared against old revision `N` because old file is gone.

## Model B — Worker startup atomically replaces canonical file with explicit startup marker

Use only if startup marker already exists in WP04 states/contract.

Do not invent a new backend state.

## Model C — Add transport session identity

This materially extends cross-process metadata.

Use only if deletion cannot provide safe semantics.

### Preferred direction

Prefer startup removal of the canonical file if compatible with ownership and #230.

Do not add session identity unless necessary.

Define exact behavior if deletion fails.

---

# Phase 8 — Define Missing File Semantics

Before first Worker publication or after startup cleanup:

Streamlit must distinguish transport unavailability from backend `Empty`.

Define one consumer-local transport status, conceptually:

- ProducerUnavailable
- AwaitingFirstPublication
- another narrow name

This is **not** a new WP04 presentation state.

It belongs to WP05 transport/UI layer.

Specify:

- whether last-good file may be retained in memory after canonical file disappears;
- whether UI renders an unavailable banner;
- whether it retries on next refresh.

Do not map missing file to WP04 Empty.

---

# Phase 9 — Define Graceful Shutdown File Retention

Choose exact rule:

## Option A — Leave last valid envelope

Advantages:
- user may still inspect last state.

Risk:
- stale prior-session comparison on restart.

If selected, Worker startup must clear prior-session file before new session.

## Option B — Delete canonical file on graceful shutdown

Advantages:
- no stale residue.

Risk:
- abrupt termination still leaves file.

### Preferred direction

Either can work if startup cleanup is authoritative.

Choose based on #230/repository expectations.

Define abrupt/crash behavior separately:
- stale file may remain;
- next Worker startup cleanup resolves it.

---

# Phase 10 — Define Path Resolution Symmetry

Worker and Streamlit must resolve the **same** path independently.

Define exact algorithm order:

1. explicit override, if configured;
2. canonical default;
3. normalize to absolute path;
4. no fallback to current working directory.

If override differs between processes, behavior is configuration error.

Define whether each side validates and reports resolved path.

Do not add path discovery through scanning.

---

# Phase 11 — Define Refresh Ownership Hook

This authority does not redefine full polling cadence unless needed.

It must, however, define lifecycle assumptions for refresh:

- Streamlit owns reading/checking the canonical file;
- Worker never signals Streamlit;
- Worker publication is push-to-file only;
- missing file is retried by future Streamlit refresh cycles.

If exact cadence remains separately unresolved, state that a later refresh-definition authority is still required.

If #230 already requires bounded refresh and this contract can safely define a cadence, specify it only if supported by repository/Streamlit conventions.

Do not invent arbitrary timing here unless necessary.

---

# Phase 12 — Define Resource Bounds

Define:

- one canonical handoff file;
- one sibling temp file per active publish attempt;
- Worker removes stale temp artifacts it owns at startup if safe;
- no accumulating per-revision files;
- Streamlit caches at most one last-good parsed envelope if consumer contract later permits it;
- no directory scanning for history.

---

# Phase 13 — Required Future Tests

Define tests for implementation.

At minimum:

## Path resolution
- default path deterministic;
- Worker and Streamlit resolve same path;
- override path honored;
- relative-path behavior exact;
- no current-working-directory fallback.

## Ownership
- Worker creates parent directory;
- Streamlit does not write/delete canonical file.

## Startup
- prior-session canonical file handled exactly;
- first new session starts without invalid revision comparison.

## Shutdown
- chosen retention/deletion rule;
- abrupt stale-file behavior;
- next startup cleanup.

## Missing file
- consumer-local unavailable state;
- never mapped to backend Empty.

## Atomic file
- temp sibling same directory;
- no canonical partial file;
- old-or-new complete only.

## Resource bounds
- one canonical file;
- no unbounded per-revision files;
- temp cleanup bounded.

## Process independence
- Streamlit can start before Worker;
- Worker can start before Streamlit;
- neither launches/stops the other if independent-peer model selected.

Do not implement tests here.

---

# Non-Goals

This definition must not authorize:

- HTTP/WebSocket/API server;
- queue/socket/shared memory;
- bidirectional IPC;
- process supervision framework;
- multi-user runtime directory;
- distributed locking;
- schema changes;
- WP04 envelope changes;
- WP06 work.

---

# Stop Conditions

Stop if:

- supported OS/platform set is unclear and materially affects base directory choice;
- path ownership cannot be assigned unambiguously;
- #230 explicitly requires one process to launch the other but mechanism is undefined;
- safe prior-session handling cannot be achieved without session identity;
- multiple materially different lifecycle models remain equally valid.

On stop:

- make zero production/config/test/GitHub changes;
- report exact unresolved runtime choice;
- identify minimum additional governance authority required.

---

# Success Criteria

This definition authority succeeds only when one complete runtime-location/lifecycle contract is established that specifies:

- canonical base directory model;
- canonical directory;
- canonical filename;
- temp filename pattern;
- exact override key or explicit no-override rule;
- path normalization;
- Worker ownership;
- Streamlit ownership;
- startup relationship;
- shutdown relationship;
- graceful file retention/deletion;
- abrupt-stop behavior;
- prior-session file safety;
- missing-file consumer semantics;
- refresh ownership assumptions;
- resource bounds;
- required future tests;
- non-goals.

No implementation occurs.

No GitHub mutation occurs.

WP06 remains unstarted.

---

# Required Completion Report

Return:

## Canonical runtime location
- base directory model;
- exact path derivation;
- filename;
- temp pattern.

## Override
- exact key;
- environment mapping;
- validation;
- fallback.

## Ownership
- Worker responsibilities;
- Streamlit responsibilities.

## Startup
- process relationship;
- startup order;
- prior-session cleanup.

## Shutdown
- mutual signaling or none;
- file retention/deletion;
- crash behavior.

## Missing file
- exact consumer-local semantics.

## Session safety
- how Historical revision reset is protected from prior-session file.

## Refresh ownership
- who polls/reads;
- whether cadence remains to be defined separately.

## Required future tests
List exact scenarios.

## Non-goals
List exclusions.

## Mutation proof

Expected:

`WP05 RUNTIME-LOCATION/LIFECYCLE DEFINITION MUTATIONS: ZERO`

## Next step

If complete:

`WP05 RUNTIME-LOCATION/LIFECYCLE CONTRACT DEFINED — IMPLEMENTATION OR REMAINING REFRESH DEFINITION REQUIRES FRESH AUTHORITY`

Do not implement here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP05 RUNTIME-LOCATION/LIFECYCLE DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP05 RUNTIME-LOCATION/LIFECYCLE DEFINITION BLOCKED`

Emit success only if the runtime-location/lifecycle contract is fully unambiguous.

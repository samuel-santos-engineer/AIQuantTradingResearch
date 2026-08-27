# Release 1.9 — WP05 Cross-Process Handoff / Startup-Refresh Contract Definition — Codex Authority

## Authority

This document grants a **narrow, definition-only authority** for Release 1.9 WP05, canonical GitHub issue **#230**.

WP05 is blocked before mutation because #230 requires a separate Streamlit entry point that consumes the Worker-produced visualization read model, with bounded refresh and owned startup/shutdown behavior.

Current WP04 producer state is in-process only:

- `AtomicVisualizationReadModelStore` is in-memory;
- no cross-process serialization contract exists;
- no governed runtime handoff location/path exists;
- no Worker→Streamlit access contract exists;
- no atomic file publication protocol is fixed for cross-process use;
- no startup/shutdown ownership model is fixed;
- no refresh cadence/trigger is fixed.

Neither #230 nor the accepted Release 1.9 definition resolves those material choices.

Current proven state:

- no WP05 repository mutation occurred;
- no WP05 GitHub mutation occurred;
- #230 remains Open / Backlog;
- WP06 remains unstarted;
- WP04 read-model contract and 297/297 predecessor baseline remain preserved.

This authority exists only to define the missing **cross-process handoff, lifecycle, and refresh contract**.

It does **not** authorize implementation.

It does **not** authorize Streamlit coding.

It does **not** authorize Worker production changes.

It does **not** authorize GitHub lifecycle mutation.

---

# Objective

Produce one explicit, minimal, cross-process consumer contract that a later WP05 implementation authority can apply without inventing semantics.

The contract must define exactly:

1. serialization format;
2. contract-version handling;
3. handoff path/location;
4. ownership of that location;
5. atomic Worker publication protocol;
6. Streamlit read protocol;
7. missing-file behavior;
8. corrupt/partial-file behavior;
9. unknown-version behavior;
10. startup ownership;
11. shutdown ownership;
12. stale-file/session behavior;
13. refresh/polling cadence or trigger;
14. refresh bounds;
15. read-only consumer rules;
16. failure/retry behavior;
17. whether Streamlit may start Worker;
18. whether Worker may start Streamlit;
19. required future tests;
20. explicit non-goals.

The result must be specific enough for a later Terra implementation authority.

---

# Fixed Predecessor Contract

Do not reopen WP04 semantics.

## Envelope

Contract version:

`aiq-visualization-read-model-v1`

The envelope already carries:

- tagged revision;
- source mode;
- source authority;
- target;
- snapshot identity/version when available;
- Ready / Empty / WarmUp / Stale / Failed;
- bounded 64-row window;
- latest observation/count;
- feature identity/value/warm-up metadata;
- pipeline/status evidence;
- validation/quality state;
- safe failure metadata;
- stale metadata.

## Atomic in-memory producer

WP04 already owns:

- read-model construction;
- boundedness;
- revision assignment;
- state transitions;
- immutable publication;
- safe failure/stale encoding.

WP05 must consume this state truthfully.

## Revision semantics

Historical:
- `HistoricalPresentationRevision`
- session-local publication order

Replay:
- real WP02 logical tick

No cross-mode numeric ordering.

Do not redefine these.

---

# Core Architecture Principle

The cross-process boundary must transport the already-complete WP04 envelope.

It must not create a second presentation model.

The Worker is the producer.

Streamlit is a read-only consumer.

The transport layer must preserve:

- atomic complete-envelope visibility;
- contract version;
- revision identity;
- state identity;
- bounded window;
- safe failure semantics.

---

# Permitted Scope

This authority may read:

- #230;
- Release 1.9 WP05 definition/manifest;
- current Worker startup/shutdown code;
- current Streamlit entry point/project structure;
- existing filesystem/runtime-directory conventions;
- serialization utilities;
- JSON conventions;
- temp-file/atomic-replace helpers;
- process-launch patterns, if any;
- environment/config conventions;
- tests around file handoff or process ownership.

It may define one normative cross-process contract.

If governance permits one WP05-owned definition artifact, create only that artifact.

Otherwise return the full normative contract in the completion report.

---

# Explicitly Forbidden

Do not:

- modify Worker code;
- modify Streamlit code;
- modify tests;
- modify configuration files;
- create runtime directories/files;
- change WP04 envelope fields;
- change schema;
- change persistence;
- change package pins;
- change Python version;
- change Streamlit version;
- add network APIs;
- add sockets;
- add HTTP servers;
- add databases/queues;
- modify GitHub;
- close #230;
- start WP06.

This is definition-only authority.

---

# Phase 0 — Read Existing Runtime Conventions

Before defining anything:

1. Read #230.
2. Read WP05 manifest/definition.
3. Read Worker process entry/startup/shutdown.
4. Read Streamlit project entry point.
5. Identify any existing runtime-data directory convention.
6. Identify temp-file and atomic-replace utilities.
7. Identify JSON serialization conventions.
8. Identify environment-variable/configuration conventions for paths.
9. Identify whether Streamlit currently launches independently or through another script/process.
10. Identify whether Worker lifetime is expected to outlive UI lifetime.

Do not mutate anything.

---

# Phase 1 — Choose Transport Medium

Evaluate the narrowest viable transport.

At minimum consider:

## Model A — Atomic JSON file handoff

Worker serializes the WP04 envelope to a local file using atomic replacement.

Assess:
- compatibility with separate Streamlit process;
- boundedness;
- simplicity;
- no new service/dependency;
- atomicity;
- contract version handling.

## Model B — Another existing governed local IPC mechanism

Use only if repository already has one.

## Model C — Network/API transport

Only if #230 explicitly requires it.

Do not choose network transport merely for convenience.

### Preferred direction

Prefer local atomic file handoff if repository evidence supports it.

### Hard stop

If multiple transport choices remain equally valid and materially affect architecture, stop rather than guessing.

---

# Phase 2 — Define Serialization Format

If file transport is selected, define:

- exact encoding;
- exact serialization format;
- whether pretty-printing is allowed;
- newline behavior;
- number/date formatting conventions;
- contract version field usage;
- unknown-field behavior;
- missing-field behavior.

Preferred candidate:

- UTF-8 JSON;
- one complete envelope per file;
- deterministic property names matching contract;
- no arbitrary polymorphic payloads.

Use existing repository serialization conventions where available.

Do not create a second schema unrelated to the WP04 envelope.

---

# Phase 3 — Define Handoff Path

Define one canonical runtime path.

The contract must specify:

- base directory source;
- file name;
- whether path is absolute or derived;
- whether environment/config override is allowed;
- ownership of directory creation;
- permissions expectation;
- cleanup behavior.

Evaluate sources such as:

- repository/runtime-local directory;
- OS temp/runtime directory;
- existing app data directory;
- configured path.

Do not hard-code a machine-specific developer path.

Prefer an existing runtime-directory convention if one exists.

If a config key is required, define the exact key/path.

---

# Phase 4 — Define Path Ownership

Specify:

## Worker

- owns creation of the handoff directory if needed;
- owns writing/replacing the handoff file;
- owns temp-file cleanup related to publication.

## Streamlit

- read-only access;
- must not create/modify the canonical handoff file except perhaps directory existence checks if contract allows;
- must never rewrite or acknowledge state through the file.

No bidirectional command channel is authorized.

---

# Phase 5 — Define Atomic Publication Protocol

For file handoff, define exact publication steps.

Required conceptual protocol:

1. Worker serializes a complete immutable envelope.
2. Worker writes to a sibling temporary file in the same filesystem/directory.
3. Worker flushes file contents.
4. Worker closes the temp file.
5. Worker atomically replaces/renames the canonical destination.
6. Destination always represents one complete envelope.
7. On publication failure, prior valid destination remains consumer-visible where filesystem semantics permit.
8. Temp artifacts are cleaned up safely.

Define whether file-system metadata flush is required by repository conventions.

Do not allow in-place overwrite of the destination.

---

# Phase 6 — Define Streamlit Read Protocol

Streamlit must:

1. check for canonical file;
2. open/read complete file;
3. decode UTF-8 JSON;
4. validate contract version;
5. parse immutable envelope;
6. compare revision only within valid mode/context if local UI cache exists;
7. render state truthfully;
8. never mutate the handoff.

Define whether Streamlit should retry once if replace races with open/read, though atomic replace should minimize this.

Do not make Streamlit reconstruct backend state.

---

# Phase 7 — Missing / Corrupt / Unknown-Version Semantics

Define exact behavior for:

## File missing

Examples:
- Worker not started yet;
- Worker has not published first envelope;
- file removed during shutdown.

Choose exact UI-consumer state:
- startup/not-available;
- Empty;
- Failed;
- another explicit consumer-local status.

Do not falsely map missing transport to backend Empty if they are semantically different.

## Corrupt/unparseable file

Define:
- safe error state;
- whether last-good parsed envelope is retained;
- whether retry is permitted.

## Unknown contract version

Must fail safely.
Do not reinterpret unknown versions.

Define whether last-good known-version envelope remains renderable with a consumer warning/status.

---

# Phase 8 — Startup Ownership

Define process ownership.

At minimum answer:

- Does Streamlit start Worker?
- Does Worker start Streamlit?
- Are they started independently by an external launcher/script/user?
- Which process is authoritative for lifecycle?

Preferred architecture unless #230 says otherwise:

> Worker and Streamlit are independently started processes; neither owns launching the other.

If #230 requires owned startup/shutdown, define the exact owner.

Do not invent a process supervisor unless required.

---

# Phase 9 — Shutdown Ownership

Define:

- what Worker does with handoff file on graceful shutdown;
- whether last valid envelope remains;
- whether Worker publishes a final stale/failed/shutdown marker;
- what Streamlit does if Worker disappears;
- whether Streamlit ever deletes the file.

Prefer preserving last valid envelope unless #230 requires cleanup.

If stale-after-shutdown semantics require a new backend state, stop; do not redefine WP04 state machine casually.

Instead distinguish transport-unavailable from producer Stale if needed.

---

# Phase 10 — Session / Stale File Semantics

Because Historical revisions reset per Worker session, define how Streamlit avoids comparing a new session's revision `1` against an old session's higher revision.

The current WP04 in-memory contract has no session identity because it did not need cross-process persistence.

The cross-process file **can outlive a Worker session**, so this definition must decide whether:

## Option A — File is removed/replaced before new session publication

Worker startup owns clearing/replacing stale prior-session file before publishing revision 1.

## Option B — Cross-process envelope adds a transport/session discriminator

This would be a new cross-process metadata field, not a WP04 semantic revision.

If needed, define a minimal session/run identity for transport safety.

Do not change Historical revision semantics themselves.

### Hard stop

If safe cross-session handling cannot be achieved without modifying the fixed WP04 envelope contract materially, stop and identify the required amendment.

---

# Phase 11 — Refresh / Polling Semantics

Define a bounded refresh contract.

The definition must specify:

- trigger: periodic polling, manual refresh, or both;
- exact default cadence if periodic;
- allowed minimum/maximum if configurable;
- behavior when no new revision exists;
- behavior on read/parse failure;
- whether UI reruns only on new revision or on every poll.

Do not invent an arbitrary cadence unless #230 or repository conventions support one.

If #230 requires bounded refresh but no cadence is fixed, choose the narrowest reasonable value only if this definition authority is explicitly empowered to make that normative choice.

Every timing choice must include rationale.

---

# Phase 12 — Refresh Ownership

Define whether refresh is:

- Streamlit-owned polling of the handoff file;
- external browser/UI rerun mechanism;
- another existing repository mechanism.

Worker must not poll Streamlit.

No bidirectional signaling.

---

# Phase 13 — Bounded Resource Semantics

Define bounds for:

- maximum handoff file count;
- temp-file cleanup;
- retry count;
- polling frequency;
- local cached envelope count.

Preferred:
- one canonical file;
- at most one temp sibling per publish attempt;
- one last-good envelope cached in Streamlit at most;
- bounded retry behavior;
- no accumulating historical files.

---

# Phase 14 — Consumer Read-Only Rules

Streamlit may:

- read current envelope;
- cache one last-good envelope if contract says so;
- compare revisions within valid context;
- render state.

Streamlit must not:

- alter file;
- write acknowledgements;
- modify Worker state;
- access SQLite;
- call providers;
- recompute features;
- infer missing backend state from transport errors;
- synthesize revisions.

---

# Phase 15 — Required Future Tests

Define future implementation tests.

At minimum:

## Serialization
- valid envelope round-trip;
- exact contract version;
- unknown version rejected;
- corrupt JSON handled safely.

## Atomic publication
- reader sees old or new complete file only;
- partial temp content never appears as canonical destination;
- failed replace preserves last valid destination where supported.

## Path
- canonical path derivation;
- no machine-specific hardcoding;
- directory ownership.

## Missing file
- exact consumer behavior before first Worker publication.

## Session restart
- old-session file cannot cause invalid Historical revision comparison;
- new Worker session begins safely.

## Refresh
- bounded cadence/trigger;
- no new revision => no false state mutation;
- new revision => consumer updates;
- read failure behavior bounded.

## Ownership
- Worker writes;
- Streamlit read-only;
- no SQLite/provider access;
- no bidirectional IPC.

## Lifecycle
- startup ownership;
- shutdown behavior;
- last-good/stale-file semantics.

Do not implement tests here.

---

# Non-Goals

This definition must not authorize:

- HTTP API;
- WebSocket;
- database polling;
- message queues;
- sockets;
- shared memory;
- process supervisor;
- distributed coordination;
- bidirectional UI commands;
- schema changes;
- WP04 semantic redesign;
- WP06 work.

---

# Stop Conditions

Stop if:

- transport medium cannot be chosen unambiguously;
- canonical runtime path has no governed basis;
- startup/shutdown ownership remains ambiguous;
- refresh cadence requires an unsupported product-policy choice;
- cross-session safety requires a material WP04 envelope redesign;
- atomic replace semantics are not available on supported runtime platform;
- multiple materially different contracts remain equally valid.

On stop:

- make zero production/test/GitHub changes;
- report exact unresolved cross-process contract choice;
- identify minimum additional governance authority required.

---

# Success Criteria

This definition authority succeeds only when one complete cross-process contract is established that specifies:

- transport medium;
- serialization;
- contract-version handling;
- canonical path;
- path ownership;
- atomic publication;
- Streamlit read protocol;
- missing/corrupt/unknown-version behavior;
- startup ownership;
- shutdown behavior;
- cross-session safety;
- refresh trigger/cadence/bounds;
- read-only consumer rules;
- resource bounds;
- required future tests;
- non-goals.

No implementation occurs.

No GitHub mutation occurs.

WP06 remains unstarted.

---

# Required Completion Report

Return:

## Transport
- selected model
- rationale

## Serialization
- exact format/encoding
- contract-version handling

## Handoff path
- exact canonical path derivation
- config key if any
- ownership

## Atomic publication
- temp path
- flush/close/replace sequence
- failure behavior

## Streamlit read protocol
- open/read/parse/validate behavior
- last-good behavior if any

## Missing/corrupt/unknown-version
Exact semantics.

## Startup/shutdown
- who starts whom
- file lifecycle
- process ownership

## Session safety
- stale prior-session file behavior
- session identity or startup clearing rule

## Refresh
- trigger
- cadence
- bounds
- no-new-revision behavior

## Required future tests
List exact scenarios.

## Non-goals
List exclusions.

## Mutation proof

Expected:

`WP05 CROSS-PROCESS HANDOFF/REFRESH DEFINITION MUTATIONS: ZERO`

## Next step

State:

`WP05 CROSS-PROCESS HANDOFF/REFRESH CONTRACT DEFINED — IMPLEMENTATION REQUIRES FRESH AUTHORITY`

Do not implement here.

---

# Terminal Markers

On success:

`RELEASE 1.9 WP05 CROSS-PROCESS HANDOFF/REFRESH DEFINITION COMPLETE`

On blocker:

`RELEASE 1.9 WP05 CROSS-PROCESS HANDOFF/REFRESH DEFINITION BLOCKED`

Emit success only if the contract is fully unambiguous.

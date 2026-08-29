# Release 1.10 WP03 — Infrastructure Instrumentation Contract & Path Reconciliation Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — PRIMARY contract, architecture, vocabulary, path ownership, dependency selection, acceptance, reconciliation, and governance authority.
- **GPT-5.6 Terra** — reserved for resumed WP03 implementation, validation, and approved post-acceptance GitHub lifecycle mutations.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, and non-authoritative review; Sol does not replace Luna or Terra.

**Selected execution model: GPT-5.6 Luna.**

---

# Authority identity

Release: **1.10 — OpenTelemetry / Pipeline Observability**

Target work package:

**WP03 — Infrastructure Provider, Persistence & Failure Instrumentation**

Canonical issue:

**#244**

Milestone:

**#59**

Project:

**#2**

This is a narrow planning/contract reconciliation authority caused by the deterministic allowlist gate in the first WP03 Terra authority.

It is NOT an implementation authority.

---

# Confirmed blocker

The WP03 Terra entry audit passed, then blocked before implementation because the accepted Release 1.10 planning artifacts and #244 defer exact WP03 production symbols/test paths and concrete API/package authority.

Current plausible Infrastructure candidates include:

- `SqliteHistoricalObservationStore`
- `SqliteDatasetSnapshotStore`
- `SqliteDatasetCatalog`
- multiple existing Infrastructure test suites.

Terra correctly refused to choose among these or invent:

- writable symbols;
- dedicated tests;
- Infrastructure ActivitySource/Meter ownership;
- activity/metric names;
- bounded failure semantics;
- BCL/package/API surface.

No repository, Git, or GitHub mutation occurred.
No WP03 baseline tests were run.

---

# Canonical upstream state

Require and preserve:

- WP01 #242: Closed / Done
- WP02 #243: Closed / Done
- WP03 #244: Open / Backlog / Release 1.10 / milestone #59
- milestone #59: Open
- WP04–WP08 #245–#249: Open / unchanged

WP02 accepted immutable semantics include:

- root `pipeline.execute`;
- five truthful Application stages;
- `HistoricalObservationRetrieval` times only `IHistoricalObservationStore.Retrieve(...)`;
- `DatasetMaterialization` begins after retrieval;
- WP03 may nest actual Infrastructure operations beneath ambient Application activities through `Activity.Current`;
- no duplicated semantic timing.

---

# Canonical inputs

Read in full before any planning mutation:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. GitHub issue #244
6. accepted WP02 implementation
7. all implementations of relevant Application persistence/retrieval interfaces
8. `SqliteHistoricalObservationStore`
9. `SqliteDatasetSnapshotStore`
10. `SqliteDatasetCatalog`
11. relevant Infrastructure project/package files
12. existing Infrastructure test tree and architecture tests
13. any provider/repository abstractions reached by these stores.

Inspect actual call graphs and responsibilities before selecting paths.

---

# Reconciliation objectives

Freeze all information Terra needs without invention:

1. exact WP03 production paths;
2. exact writable symbols/members;
3. exact dedicated test paths;
4. exact Infrastructure ActivitySource/Meter ownership;
5. exact activity names;
6. exact metric names/types/units;
7. exact parent/child topology with WP02;
8. exact success/failure/cancellation semantics;
9. exact bounded attribute vocabulary;
10. exact failure sanitization/category contract;
11. exact BCL/package/API decision;
12. exact project/package paths if package mutation is selected;
13. exact WP04 handoff.

---

# Mutation boundary

## Repository

Planning/contract documentation only.

Authorized candidate paths:

- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md` only if the Infrastructure telemetry/dependency contract must be reconciled there.

No other repository path is writable.

## Production/test/package

ZERO mutation.

## Git

ZERO mutation.

## GitHub

ZERO mutation.

Do not close/update #244.
Do not change Project #2.
Do not change milestone #59.

---

# Decision principles

## Truthful ownership

Instrument a symbol only if it actually observes the Infrastructure operation.

Do not choose a class merely because its name sounds relevant.

## Minimal authority

Authorize the smallest exact path/symbol set that fulfills WP03.

Do not authorize whole directories, broad wildcard surfaces, or unrelated methods.

## No duplicate timing ownership

Infrastructure activities may nest beneath WP02 Application activities but must measure the real Infrastructure sub-operation only.

## Dependency direction

Do not move Infrastructure concerns into Application or presentation.

## Package minimality

Prefer BCL primitives if they fully satisfy WP03's contract and preserve the planned WP04 SDK/exporter boundary.

Select an OpenTelemetry package/API surface only if WP03 genuinely requires it and the architectural ownership is explicit.

---

# Phase 0 — Entry audit

Record:

- branch;
- local HEAD;
- remote main if available without mutation;
- `git status --short`;
- pre-existing WP01/WP02/planning residue;
- #244 state/milestone/Project/Release/Status if available;
- milestone #59 state;
- #245–#249 state.

Require no WP03 implementation residue from the blocked Terra attempt.

Emit:

`RELEASE 1.10 WP03 RECONCILIATION ENTRY BASELINE: READ-ONLY`

---

# Phase 1 — Reproduce the ambiguity

From the current plan, manifest, and #244, quote/paraphrase the exact deferred ownership language.

List all plausible production/test/package candidates.

Explain why Terra cannot deterministically select among them.

Emit:

`RELEASE 1.10 WP03 CONTRACT AMBIGUITY: CONFIRMED`

---

# Phase 2 — Trace Infrastructure call graphs

Trace canonical flows from the accepted WP02 Application boundaries into Infrastructure.

At minimum investigate:

## Historical retrieval

`IHistoricalObservationStore.Retrieve(...)`
→ concrete implementation(s)
→ SQLite/provider operations.

## Dataset snapshot persistence

Application persistence/catalog abstractions
→ concrete snapshot/catalog implementations
→ actual SQLite operations.

For each relevant symbol record:

- exact path;
- exact member/signature;
- architectural layer;
- semantic responsibility;
- actual observable start/end;
- failure boundary;
- ambient WP02 parent;
- whether it is required by WP03.

Emit:

`RELEASE 1.10 WP03 INFRASTRUCTURE OWNERSHIP ANALYSIS: COMPLETE`

---

# Phase 3 — Freeze production path/symbol ownership

Select exact production paths.

For every path specify:

- exact path;
- `MODIFY` or `ADD`;
- exact writable symbol/member;
- exact telemetry responsibility;
- forbidden unrelated modifications.

Explicitly resolve whether each candidate is:

- `WP03 REQUIRED`
- `WP03 NOT AUTHORIZED`
- `LATER WP OWNER`

At minimum classify:

- `SqliteHistoricalObservationStore`
- `SqliteDatasetSnapshotStore`
- `SqliteDatasetCatalog`

No vague language such as “related Infrastructure stores” is permitted.

Emit:

`RELEASE 1.10 WP03 PRODUCTION PATH OWNERSHIP: FROZEN`

---

# Phase 4 — Freeze Infrastructure source/meter contract

Decide exact Infrastructure telemetry ownership.

Specify:

- whether WP03 owns a dedicated `ActivitySource`;
- exact source name;
- whether WP03 owns a dedicated `Meter`;
- exact meter name;
- exact source code path/symbol that owns these instances;
- disposal/lifetime semantics;
- whether static/shared ownership is required;
- relationship to WP02 Application source/meter.

If no new dedicated helper/source file is needed, state that explicitly.

If a new file is required, name its exact path and authorize it.

Emit:

`RELEASE 1.10 WP03 INFRASTRUCTURE SOURCE/METER CONTRACT: FROZEN`

---

# Phase 5 — Freeze activity contract

For every WP03 activity define:

- exact canonical name;
- exact owning method;
- exact measured interval;
- expected ambient parent;
- success status;
- failure status;
- cancellation behavior;
- whether exception event/recording is authorized;
- whether activity is required when no listener is present.

At minimum resolve truthful activities for:

- historical retrieval/provider boundary;
- dataset snapshot persistence if WP03-owned;
- catalog persistence/read boundary if WP03-owned.

Do not create activities solely to satisfy a count.

Emit:

`RELEASE 1.10 WP03 ACTIVITY CONTRACT: FROZEN`

---

# Phase 6 — Freeze metric contract

For every WP03 metric define:

- exact name;
- instrument type;
- unit;
- semantic meaning;
- recording boundary;
- success/failure behavior;
- allowed attributes.

Explicitly state whether latency, operation count, failure count, or other metrics are accepted.

No dynamic instrument names.

No uncontrolled dimensions.

Emit:

`RELEASE 1.10 WP03 METRIC CONTRACT: FROZEN`

---

# Phase 7 — Freeze bounded attribute/failure contract

Define exact allowed Infrastructure attributes.

Classify candidate attributes:

- required;
- optional bounded;
- forbidden.

Explicitly resolve:

- operation kind;
- storage/provider kind;
- outcome;
- failure category;
- exception type/category;
- database/table identity if relevant;
- symbol/ticker;
- row/item counts;
- retry count;
- cancellation/timeout.

Security rules must prohibit:

- connection strings;
- SQL containing user/business data;
- raw provider payload;
- credentials/tokens;
- arbitrary exception messages as metric labels;
- stack traces as metric dimensions;
- GUIDs/request IDs as metric dimensions;
- timestamps as metric dimensions;
- raw local paths;
- uncontrolled symbol/ticker dimensions.

Define a bounded failure-category vocabulary if failure category is authorized.

Emit:

`RELEASE 1.10 WP03 BOUNDED FAILURE/ATTRIBUTE CONTRACT: FROZEN`

`RELEASE 1.10 WP03 TELEMETRY SECURITY CONTRACT: PASS`

---

# Phase 8 — BCL/package/API decision

Inspect current project dependencies and WP04 ownership.

Choose exactly one.

## Preferred outcome

If WP03 can emit its ActivitySource/Meter contract using BCL only:

`RELEASE 1.10 WP03 INFRASTRUCTURE OBSERVABILITY API: BCL-ONLY ACCEPTED`

Then freeze:

- no `.csproj` mutation;
- no package mutation;
- SDK/provider/exporter remains WP04-owned.

## Package outcome

Only if genuinely required:

`RELEASE 1.10 WP03 INFRASTRUCTURE OBSERVABILITY API: PACKAGE SURFACE ACCEPTED`

Then freeze:

- exact package(s);
- exact version(s);
- exact project/package file path(s);
- reason BCL is insufficient;
- why package belongs to WP03 rather than WP04;
- exact permitted API surface.

Do not select exporter packages in WP03 unless the canonical architecture explicitly assigns them here.

Emit exactly one API decision marker.

---

# Phase 9 — Freeze test ownership

Inspect existing Infrastructure tests.

Select exact test paths.

For each specify:

- `ADD` or `MODIFY`;
- exact path;
- exact scenarios;
- whether focused ActivityListener/MeterListener tests are needed;
- whether existing store tests are sufficient;
- whether integration topology with WP02 belongs here.

Tests must cover as applicable:

- activity identity/name;
- truthful interval;
- parent under WP02;
- success;
- failure;
- exception propagation;
- metrics;
- bounded attributes;
- no-listener behavior;
- schema/functional preservation;
- no sensitive/high-cardinality data.

No vague “relevant tests”.

Emit:

`RELEASE 1.10 WP03 TEST PATH OWNERSHIP: FROZEN`

---

# Phase 10 — Architecture/no-bypass reconciliation

Prove the frozen contract preserves:

- Application → abstraction direction;
- Infrastructure implementation ownership;
- WP02 immutable source/meter/stage semantics;
- SQLite schema v4;
- canonical JSON handoff;
- Streamlit independence;
- Worker independence;
- no parallel pipeline;
- no presentation database/provider access.

Emit:

`RELEASE 1.10 WP03 ARCHITECTURE CONTRACT: PASS`

---

# Phase 11 — WP02 parent topology reconciliation

For each WP03 activity state its exact expected parent.

Require historical Infrastructure retrieval to nest beneath the ambient WP02 `HistoricalObservationRetrieval` activity during canonical pipeline execution.

For persistence activities, name the actual WP02 parent if one exists; otherwise state the truthful root/parent behavior.

No WP03 activity may duplicate the full WP02 Application stage interval.

Emit:

`RELEASE 1.10 WP03 ← WP02 ACTIVITY TOPOLOGY: FROZEN`

---

# Phase 12 — WP04 handoff

Freeze exactly what WP04 may consume:

- Infrastructure ActivitySource name;
- Meter name;
- activity names;
- metric names;
- provider/exporter/SDK ownership still deferred to WP04 if BCL-only;
- lifecycle/disposal expectations;
- failure isolation expectations.

WP04 must not need to rename or redesign WP03 instrumentation.

Emit:

`RELEASE 1.10 WP03 → WP04 CONTRACT HANDOFF: PASS`

---

# Phase 13 — Reconcile execution plan

Update `RELEASE_1.10_EXECUTION_PLAN.md` with the exact resolved WP03 contract.

Remove deferred/ambiguous phrases for WP03.

Include:

- exact production symbols;
- exact tests;
- source/meter;
- activities;
- metrics;
- attributes/failures;
- API/package decision;
- topology;
- WP04 handoff.

---

# Phase 14 — Reconcile file manifest

Update `RELEASE_1.10_FILE_MANIFEST.md`.

For WP03 list every writable path deterministically.

For modified files, list exact symbols.

For added files, list exact paths.

For package/project files, list them only if Phase 8 selected package authority.

No wildcard or “as needed” path authority.

---

# Phase 15 — Selection-record reconciliation

Modify `OPEN_TELEMETRY_SELECTION.md` only if needed to keep the canonical selection/security/vocabulary contract consistent.

If no modification is required, explicitly report:

`OPEN_TELEMETRY_SELECTION.md: NO CHANGE REQUIRED`

Do not duplicate WP03 manifest details unnecessarily.

---

# Phase 16 — Terra materialization simulation

Simulate the resumed Terra WP03 authority.

Terra must answer without invention:

1. What exact production paths may be changed?
2. What exact symbols in each path may be changed?
3. Is a new Infrastructure observability helper/source file authorized?
4. What is the exact ActivitySource name?
5. What is the exact Meter name?
6. What activities are emitted, by which methods, around which real intervals?
7. What metrics are emitted, with what types/units?
8. What exact attributes/failure categories are allowed?
9. What exact test paths may be changed/added?
10. Is WP03 BCL-only or package-backed?
11. If package-backed, exactly which files/packages/versions?
12. How does historical retrieval parent beneath WP02?
13. Which persistence operations are WP03-owned?
14. Which plausible candidates are explicitly NOT authorized?
15. What does WP04 inherit without redesign?

All answers must be deterministic.

Emit:

`RELEASE 1.10 WP03 MATERIALIZATION SIMULATION: PASS — TERRA-READY`

---

# Phase 17 — Acceptance

PASS only if:

- ambiguity is fully removed;
- exact production paths/symbols are frozen;
- exact test paths are frozen;
- source/meter/activity/metric contract is frozen;
- bounded failure/attribute contract is frozen;
- exact BCL/package decision is frozen;
- WP02 topology is preserved;
- WP04 handoff is deterministic;
- Terra simulation passes;
- only authorized planning/contract paths changed;
- Git/GitHub mutations are zero.

Emit:

`RELEASE 1.10 WP03 CONTRACT/PATH RECONCILIATION: PASS`

---

# Phase 18 — Mutation accounting

Report exact changed planning paths.

Required:

`RELEASE 1.10 WP03 RECONCILIATION REPOSITORY MUTATIONS: PLANNING/CONTRACT PATHS ONLY`

`RELEASE 1.10 WP03 RECONCILIATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 RECONCILIATION GITHUB MUTATIONS: ZERO`

Issue #244 remains Open / Backlog.

---

# Phase 19 — Next authority

On PASS, regenerate/resume:

**Release 1.10 WP03 — Infrastructure Provider, Persistence & Failure Instrumentation Authority — GPT-5.6 Terra**

That Terra authority must reread the reconciled planning artifacts and enforce the frozen allowlist/API contract.

After future WP03 implementation acceptance passes, that Terra authority must:

- close #244;
- set its Project #2 item to Done;
- preserve Release=1.10;
- preserve milestone #59 as Open;
- leave #245–#249 unchanged.

Do not execute WP03 implementation here.

---

# Required final report

Report:

1. model assignment;
2. entry baseline;
3. ambiguity evidence;
4. Infrastructure call graphs;
5. production path/symbol ownership;
6. source/meter contract;
7. activity contract;
8. metric contract;
9. bounded attribute/failure contract;
10. BCL/package/API decision;
11. test ownership;
12. architecture contract;
13. WP02 topology;
14. WP04 handoff;
15. changed planning paths;
16. Terra simulation;
17. acceptance;
18. mutation accounting;
19. exact next authority.

---

# Success markers

`RELEASE 1.10 WP03 CONTRACT AMBIGUITY: CONFIRMED`

`RELEASE 1.10 WP03 INFRASTRUCTURE OWNERSHIP ANALYSIS: COMPLETE`

`RELEASE 1.10 WP03 PRODUCTION PATH OWNERSHIP: FROZEN`

`RELEASE 1.10 WP03 INFRASTRUCTURE SOURCE/METER CONTRACT: FROZEN`

`RELEASE 1.10 WP03 ACTIVITY CONTRACT: FROZEN`

`RELEASE 1.10 WP03 METRIC CONTRACT: FROZEN`

`RELEASE 1.10 WP03 BOUNDED FAILURE/ATTRIBUTE CONTRACT: FROZEN`

`RELEASE 1.10 WP03 TELEMETRY SECURITY CONTRACT: PASS`

One of:

`RELEASE 1.10 WP03 INFRASTRUCTURE OBSERVABILITY API: BCL-ONLY ACCEPTED`

or

`RELEASE 1.10 WP03 INFRASTRUCTURE OBSERVABILITY API: PACKAGE SURFACE ACCEPTED`

Then:

`RELEASE 1.10 WP03 TEST PATH OWNERSHIP: FROZEN`

`RELEASE 1.10 WP03 ARCHITECTURE CONTRACT: PASS`

`RELEASE 1.10 WP03 ← WP02 ACTIVITY TOPOLOGY: FROZEN`

`RELEASE 1.10 WP03 → WP04 CONTRACT HANDOFF: PASS`

`RELEASE 1.10 WP03 MATERIALIZATION SIMULATION: PASS — TERRA-READY`

`RELEASE 1.10 WP03 CONTRACT/PATH RECONCILIATION: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

`RELEASE 1.10 WP03 RECONCILIATION REPOSITORY MUTATIONS: PLANNING/CONTRACT PATHS ONLY`

`RELEASE 1.10 WP03 RECONCILIATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP03 RECONCILIATION GITHUB MUTATIONS: ZERO`

Terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE INSTRUMENTATION CONTRACT & PATH RECONCILIATION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- truthful Infrastructure ownership cannot be determined;
- required paths cannot be narrowed deterministically;
- WP03 requires architecture distortion;
- package ownership conflicts with WP04 and cannot be reconciled;
- failure vocabulary cannot be bounded safely;
- WP02 topology would need redesign;
- test ownership remains ambiguous;
- Terra simulation requires invention;
- any production/test/package/Git/GitHub mutation occurs.

Report exact unresolved contract question and smallest next reconciliation.

Terminal:

`RELEASE 1.10 WP03 — INFRASTRUCTURE INSTRUMENTATION CONTRACT & PATH RECONCILIATION AUTHORITY BLOCKED`

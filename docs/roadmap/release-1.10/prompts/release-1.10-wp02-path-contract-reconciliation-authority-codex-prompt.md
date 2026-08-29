# Release 1.10 WP02 — Path Contract Reconciliation Authority

## Model assignment

Always define all three GPT-5.6 roles:

- **GPT-5.6 Luna** — PRIMARY for contract reconciliation, path ownership, architecture boundaries, additive contract shape, acceptance criteria, and governance.
- **GPT-5.6 Terra** — RESERVED for subsequent WP02 implementation, validation execution, approved source/test/package mutations, and later Git/GitHub lifecycle work.
- **GPT-5.6 Sol** — RESERVED for supporting analysis, alternatives, synthesis, and non-authoritative review. Sol does not replace Luna or Terra for assigned authorities.

**Selected execution model: GPT-5.6 Luna.**

---

# Authority identity

Release: **1.10 — OpenTelemetry / Pipeline Observability**

Target work package:

**WP02 — Application Pipeline Observability Contract**

Canonical issue:

**#243**

This authority exists solely to resolve the narrow execution-contract gap that blocked the prior GPT-5.6 Terra WP02 implementation authority.

It is NOT a WP02 implementation authority.

---

# Blocking condition being resolved

The prior WP02 Terra authority blocked before mutation because the accepted planning artifacts did not deterministically name:

1. exact writable Application symbols/files;
2. exact dedicated WP02 test paths;
3. whether BCL `System.Diagnostics` is the accepted no-package Application-layer API surface;
4. the exact additive application observability contract shape exposed to WP03.

The existing planning text was insufficiently deterministic:

- Application: “existing Application contract symbols named by a later WP02 authority”
- Tests: “exact dedicated Application tests only if later named”

Terra was required to treat the file manifest as a hard path boundary and correctly refused to invent `PipelineExecutionUseCase.cs`, a new abstraction file, or any test location.

This authority must remove only that ambiguity.

---

# Canonical inputs

Read in full before any mutation:

1. `docs/roadmap/release-1.10/RELEASE_1.10_DEFINITION.md`
2. `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
4. `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md`
5. GitHub issue #243
6. relevant current Application source tree
7. relevant current Application test tree
8. current dependency/project files needed to determine whether BCL diagnostics are already available without package changes.

If issue #243 and the planning artifacts materially disagree on WP02 scope:
BLOCK.

Do not expand WP02 beyond its accepted capability.

---

# Scope of this reconciliation

The only questions this authority may decide are:

## A. Exact Application path ownership
Freeze the exact existing Application file(s) and symbol(s) WP02 may modify, and/or the exact new Application file(s) WP02 may add.

## B. Exact WP02 test path ownership
Freeze the exact dedicated test file(s)/path(s) WP02 may modify/add.

## C. Application-layer observability API surface
Decide whether WP02 Application code should use only BCL:

- `System.Diagnostics.ActivitySource`
- `System.Diagnostics.Activity`
- `System.Diagnostics.Metrics.Meter`
- related BCL primitives

with no OpenTelemetry SDK/package dependency in the Application project.

If not, identify the exact accepted package/API dependency and why it is architecturally necessary.

## D. Additive contract shape exposed to WP03
Freeze the exact application-owned instrumentation contract WP03 may later consume/nest beneath without rewriting WP02.

Do not design or implement WP03 instrumentation.

---

# Mutation boundary

## Repository

Planning/contract documentation mutations only.

Authorized candidates, subject to current accepted manifest ownership:

- `docs/roadmap/release-1.10/RELEASE_1.10_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.10/RELEASE_1.10_FILE_MANIFEST.md`
- `docs/architecture/implementation/OPEN_TELEMETRY_SELECTION.md` only if its contract must be clarified to remain consistent.

Do not create a new duplicate source of truth unless the existing manifest explicitly requires one.

## Forbidden repository mutations

Do NOT modify:

- Application production source;
- tests;
- `.csproj`/package references;
- Python code/dependencies;
- schema/migrations;
- Streamlit code;
- Worker code;
- configuration/runtime files;
- CI/workflows.

## Git

ZERO mutations.

Do not:

- checkout/switch;
- stage;
- commit;
- amend;
- merge/rebase;
- push;
- tag;
- delete refs;
- alter index.

## GitHub

ZERO mutations.

Do not edit/close #243 or any issue.
Do not modify milestone #59 or Project #2.

---

# Phase 0 — Entry audit

Record:

- repository identity;
- current branch;
- local HEAD;
- authoritative remote `main` SHA if available without mutation;
- `git status --short`;
- staged/untracked paths;
- WP01 selection artifact status;
- issue #243 state/title/body;
- milestone #59 state/counts if available.

Preserve all local residue exactly.

Emit:

`RELEASE 1.10 WP02 PATH RECONCILIATION ENTRY BASELINE: READ-ONLY`

---

# Phase 1 — Reconcile current WP02 contract

Extract from issue #243 and planning artifacts:

- objective;
- in scope;
- out of scope;
- architecture contract;
- path ownership language;
- acceptance criteria;
- validation requirements;
- security requirements;
- completion boundary.

Identify exactly which fields are already deterministic and which four execution-contract gaps remain.

Emit:

`RELEASE 1.10 WP02 PATH RECONCILIATION CONTRACT: PASS`

---

# Phase 2 — Inspect current Application structure

Read the relevant Application source tree and identify the smallest existing symbols/files that actually own the accepted pipeline execution semantics.

Do NOT choose based on convenience.

For each candidate file/symbol report:

- path;
- symbol/type/member;
- current responsibility;
- why it is or is not the correct observability owner;
- whether instrumentation can be added without changing business ownership;
- whether modification would preserve dependency direction.

Prefer instrumenting the real application pipeline orchestration/use-case boundary over introducing an unnecessary parallel abstraction.

If a new application observability helper/contract file is needed, justify why it is smaller/safer than modifying existing ownership directly.

No code mutation.

---

# Phase 3 — Freeze exact Application writable paths

Produce the exact WP02 Application allowlist.

Each path must be one of:

- `MODIFY`
- `ADD`

For every path specify:

- exact path;
- exact symbol(s) owned by WP02;
- permitted change shape;
- forbidden change shape.

Example form only:

`MODIFY: src/.../PipelineExecutionUseCase.cs`
- allowed symbols: `<exact symbols>`
- allowed: additive ActivitySource/Meter instrumentation hooks
- forbidden: provider/persistence implementation, exporter/provider composition, business behavior changes

Do not use globs unless the accepted repository convention requires them and they remain deterministic.

Emit:

`RELEASE 1.10 WP02 APPLICATION PATH OWNERSHIP: FROZEN`

---

# Phase 4 — Inspect and freeze exact test paths

Read current test structure and determine the narrowest dedicated WP02 test location.

Prefer existing Application test project conventions.

Freeze exact paths as:

- `MODIFY`
- `ADD`

For each test path specify:

- exact file path;
- test class/suite intent;
- accepted WP02 behaviors to validate;
- explicitly excluded WP03+ responsibilities.

The test contract must cover only WP02-owned semantics, such as:

- canonical ActivitySource/name;
- application activity emission;
- no-listener behavior;
- parent/child application-stage semantics;
- success/failure outcome;
- exception propagation;
- metric definitions/recording;
- attribute/cardinality constraints.

Emit:

`RELEASE 1.10 WP02 TEST PATH OWNERSHIP: FROZEN`

---

# Phase 5 — Decide BCL vs package surface

Inspect current target framework and project dependencies.

Make one deterministic decision:

## Preferred outcome
`BCL-ONLY APPLICATION SURFACE`

Meaning:

- Application code uses `System.Diagnostics.ActivitySource`, `Activity`, `Meter`, and related BCL primitives only;
- no OpenTelemetry SDK/exporter/provider package is referenced by the Application project;
- SDK/provider/exporter composition remains outside Application and is owned by later WP(s).

OR

## Exception outcome
`APPLICATION PACKAGE REQUIRED`

Only if the accepted architecture truly requires it.

If package-required, specify:

- exact package family;
- why BCL primitives are insufficient;
- which Application project receives it;
- exact version-selection ownership;
- why this does not violate dependency direction.

Do not add the package.

Emit one exact marker:

`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: BCL-ONLY ACCEPTED`

or

`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: PACKAGE SURFACE ACCEPTED`

---

# Phase 6 — Freeze additive contract shape

Define the exact application-owned observability shape Terra will implement.

The contract must be additive and must not change functional business outputs.

Freeze:

## Activity source ownership
- exact source constant/name owner;
- exact application type/file that owns it.

## Meter ownership
- exact meter constant/name owner;
- exact application type/file that owns it.

## Application operation boundaries
For each operation/stage:
- exact application method/symbol;
- canonical activity/span name;
- parent/child relationship;
- success semantics;
- failure semantics;
- whether metric recording occurs there.

## Allowed attributes
Reference the WP01 allowlist and specify which subset is legal at each application boundary.

## Metrics
For each WP02-owned metric:
- exact canonical name;
- instrument type;
- unit;
- recording point;
- allowed attributes.

## Error behavior
- telemetry must not swallow exceptions;
- functional output must remain unchanged;
- no listener/provider must be a no-op for business behavior.

## Downstream extension points
Specify exactly how WP03 may nest/correlate provider/persistence instrumentation beneath WP02 without changing WP02's public/business contract.

Do not define exporter composition or System Health implementation.

Emit:

`RELEASE 1.10 WP02 ADDITIVE OBSERVABILITY CONTRACT: FROZEN`

---

# Phase 7 — Security/cardinality reconciliation

Ensure the frozen Application paths/shape are consistent with WP01:

- no secrets;
- no raw provider payload;
- no connection strings;
- no uncontrolled exception messages as metric labels;
- no GUID/request IDs as metric dimensions;
- no timestamps as dimensions;
- no raw file paths as dimensions;
- no uncontrolled symbol/ticker dimensions;
- bounded attribute domains only.

If the exact Application implementation shape would require violating WP01:
BLOCK.

Emit:

`RELEASE 1.10 WP02 PATH CONTRACT SECURITY: PASS`

---

# Phase 8 — Downstream WP03 consumption contract

Define the exact immutable handoff to WP03.

WP03 must be able to:

- create provider/infrastructure child spans beneath application spans;
- add persistence instrumentation beneath the accepted application operation;
- preserve parent/child semantics;
- consume WP02 correlation context without Application → Infrastructure dependency;
- preserve failure propagation rules.

WP03 must NOT need to:

- rename WP02 sources/meters;
- change WP02 span names;
- relocate WP02 instrumentation;
- alter Application business outputs;
- add Application SDK/exporter ownership.

Emit:

`RELEASE 1.10 WP02 → WP03 CONTRACT HANDOFF: PASS`

---

# Phase 9 — Persist the reconciliation

Update only the canonical planning/contract docs necessary to make the above deterministic.

At minimum, ensure `RELEASE_1.10_FILE_MANIFEST.md` now names:

- exact WP02 Application writable paths;
- exact WP02 test writable paths;
- path classifications;
- symbol ownership;
- forbidden boundaries.

Ensure `RELEASE_1.10_EXECUTION_PLAN.md` records:

- BCL-vs-package decision;
- additive contract shape;
- WP03 handoff.

Update `OPEN_TELEMETRY_SELECTION.md` only if needed to clarify consistency.

Do not create contradictory duplicate text.

---

# Phase 10 — Materialization simulation

Simulate re-running the existing Terra WP02 authority.

Verify Terra can now answer without invention:

1. Which Application files may I modify/add?
2. Which symbols may I instrument?
3. Which tests may I add/modify?
4. May I add an Application OpenTelemetry package?
5. What exact ActivitySource/Meter ownership applies?
6. Which operations/stages are instrumented?
7. Which metrics/attributes are allowed?
8. What may WP03 extend later?
9. Which paths remain forbidden?

Require deterministic answers to all nine.

Emit:

`RELEASE 1.10 WP02 MATERIALIZATION SIMULATION: PASS — TERRA-READY`

---

# Phase 11 — Acceptance

PASS only if:

- exact Application writable paths are frozen;
- exact test writable paths are frozen;
- BCL/package decision is explicit;
- additive contract shape is frozen;
- WP03 handoff is deterministic;
- WP01 security/cardinality contract is preserved;
- no production/test/package mutation occurred;
- no Git/GitHub mutation occurred.

Emit:

`RELEASE 1.10 WP02 PATH CONTRACT RECONCILIATION: PASS`

---

# Phase 12 — Mutation accounting

Enumerate exact planning/contract paths changed.

Required:

`RELEASE 1.10 WP02 PATH RECONCILIATION REPOSITORY MUTATIONS: PLANNING/CONTRACT PATHS ONLY`

`RELEASE 1.10 WP02 PATH RECONCILIATION GIT MUTATIONS: ZERO`

`RELEASE 1.10 WP02 PATH RECONCILIATION GITHUB MUTATIONS: ZERO`

No source/test/package/runtime mutation is permitted.

---

# Phase 13 — Next authority

On PASS, resume the existing implementation authority:

**Release 1.10 WP02 — Application Pipeline Observability Contract Authority — GPT-5.6 Terra**

Do not create a new WP identity.

The resumed Terra authority must reread the updated execution plan, file manifest, and WP01 selection record before mutation.

Do not execute WP02 here.

---

# Required final report

Report:

1. model assignment;
2. entry baseline;
3. exact blocking-gap reconciliation;
4. Application ownership analysis;
5. exact writable Application paths/symbols;
6. exact test paths;
7. BCL/package decision;
8. additive observability contract;
9. security/cardinality result;
10. WP03 handoff;
11. updated planning/contract paths;
12. Terra materialization simulation;
13. acceptance;
14. mutation accounting;
15. exact resumed authority.

---

# Success markers

`RELEASE 1.10 WP02 PATH RECONCILIATION CONTRACT: PASS`

`RELEASE 1.10 WP02 APPLICATION PATH OWNERSHIP: FROZEN`

`RELEASE 1.10 WP02 TEST PATH OWNERSHIP: FROZEN`

`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: BCL-ONLY ACCEPTED`
or
`RELEASE 1.10 WP02 APPLICATION OBSERVABILITY API: PACKAGE SURFACE ACCEPTED`

`RELEASE 1.10 WP02 ADDITIVE OBSERVABILITY CONTRACT: FROZEN`

`RELEASE 1.10 WP02 PATH CONTRACT SECURITY: PASS`

`RELEASE 1.10 WP02 → WP03 CONTRACT HANDOFF: PASS`

`RELEASE 1.10 WP02 MATERIALIZATION SIMULATION: PASS — TERRA-READY`

`RELEASE 1.10 WP02 PATH CONTRACT RECONCILIATION: PASS`

`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Terminal:

`RELEASE 1.10 WP02 — PATH CONTRACT RECONCILIATION AUTHORITY COMPLETE`

---

# Blocked outcome

BLOCK if:

- exact Application ownership cannot be determined without changing product scope;
- multiple candidate paths remain equally authoritative;
- exact test paths cannot be made deterministic from repository conventions;
- BCL-vs-package decision cannot be made safely;
- additive contract shape would violate WP01 or dependency direction;
- planning docs cannot be reconciled without contradiction;
- any source/test/package/Git/GitHub mutation occurs.

Report the exact unresolved decision.

Terminal:

`RELEASE 1.10 WP02 — PATH CONTRACT RECONCILIATION AUTHORITY BLOCKED`

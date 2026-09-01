# GPT-5.6 Luna — Release 1.12 WP02 Repository Path Designation Authority

**Selected execution model: GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: architecture/path ownership decision, repository-structure reconciliation, literal path designation, governance amendment definition, acceptance criteria.
- **GPT-5.6 Terra** — implementation and separately authorized Git/GitHub/Docker mutations only after this designation is frozen and embedded in a Terra authority.
- **GPT-5.6 Sol** — supporting analysis only; never silently replaces Luna or Terra.

## 1. Mission

Resolve the remaining architecture/governance blocker for:

`Release 1.12 WP02 — #261 Productionized Container & Runtime Composition`

The merged planning package is published, but its manifest intentionally authorizes no future implementation paths. The prior literal-allowlist reconciliation correctly BLOCKED because no literal WP02 paths existed from which an allowlist could be mechanically derived.

This authority therefore makes the missing **Luna architecture/path-ownership decision**.

Luna must inspect the frozen Release 1.12 contract and empirical repository structure, designate the exact repository paths WP02 is permitted to own, and produce a closed literal path contract suitable for direct embedding in a replacement GPT-5.6 Terra WP02 implementation authority.

This authority is **read-only**. It designates paths; it does not implement or publish them.

## 2. Canonical starting state

Expected canonical base:

`d63f8748772f579f2c46cf79df3563627b31a958`

Expected governance:
- Release 1.12 planning artifacts are present on `main`.
- #260 = Closed/Done.
- #261 = Open/Todo.
- #262 = Open/Todo but dependency-gated.
- milestone #63 = Open, 7 open / 1 closed.
- Initiative-1.11 unchanged.
- Product Release 1.11 remains abandoned/nonexistent.
- release sequence remains `1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3`.

Fresh read-only evidence controls if state differs.

## 3. Binding planning sources

Read in full:
1. `docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md`
2. `docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md`

Treat the manifest's no-future-path authorization clause as binding.

This authority is the explicit separate architecture decision required to move from category-level WP responsibility to literal path ownership.

## 4. Empirical repository inspection

Inspect enough of the current repository, read-only, to understand the actual implementation topology, including:
- root build/container/deployment files;
- .NET solution/project layout;
- Worker/pipeline startup and configuration;
- JSON handoff implementation;
- Python package/application layout;
- Streamlit startup/presentation boundary;
- SQLite configuration/path handling;
- observability/System Health surfaces;
- existing scripts;
- existing tests/architecture tests;
- existing ignore/configuration files.

Use actual repository evidence, not conventional filenames or hypothetical structure.

## 5. Architecture constraints

Path designation MUST preserve:
- Azure App Service Linux F1 reference target.
- custom Linux Docker.
- persistent `/home` deployment boundary.
- configuration-driven writable SQLite.
- SQLite DELETE journal selection.
- public/free GHCR downstream publication.
- strict recurring infrastructure cost `$0.00`.
- reference/demo-only scope and no SLA/HA claim.
- .NET canonical pipeline ownership.
- atomic JSON handoff.
- Python parser → frame → presentation → Streamlit.
- Streamlit no SQLite/provider/Worker-supervision ownership.
- Release 1.8 JSON-over-stdio separation.
- Release 1.10 observability/System Health truthfulness.
- deterministic/replay/simulated provenance.

Do not designate paths that introduce Azure SQL, Azure Files, Container Apps, mandatory ACR, paid services, ML, backtesting, live trading, schema migration, or ownership bypass.

## 6. Luna designation authority

Unlike the prior mechanical allowlist reconciliation, this authority MAY make the missing architecture decision about where WP02-owned container/runtime composition belongs in the repository.

For every designated path, Luna must decide:
- exact repository-relative path;
- current existence;
- operation authorization:
  - `CREATE`
  - `MODIFY`
  - `DELETE`
  - `RENAME`
- ownership rationale;
- runtime responsibility;
- why WP02 rather than WP03–WP08 owns it;
- production/source/test/docs classification;
- whether the path is mandatory or optional within the frozen design.

Do not authorize broad directories, globs, or unspecified future paths.

## 7. Minimal-path principle

Designate the smallest coherent path set that can implement and validate WP02.

Do not authorize paths merely because they might be convenient.

Prefer:
- deployment-composition boundaries over business-logic changes;
- existing configuration extension points over architectural rewrites;
- narrow entrypoint/supervision artifacts over new frameworks;
- existing test projects over unnecessary new test infrastructure.

If a production-source modification is avoidable through container/runtime composition, do not designate it.

## 8. Required explicit decisions

Luna must explicitly decide whether WP02 owns a literal path for each of these categories:

- Dockerfile/container definition;
- `.dockerignore`;
- container entrypoint/supervision;
- Streamlit container/server configuration;
- .NET runtime configuration needed only for container execution;
- JSON handoff path configuration;
- SQLite path configuration;
- health/readiness support at container/runtime level;
- tests for entrypoint/process supervision;
- tests for configuration/filesystem behavior;
- documentation directly required to make WP02 reproducible.

For every category, output either:
- `DESIGNATED: <literal path(s)>`, or
- `NO WP02 PATH REQUIRED`, with rationale.

## 9. WP boundary protection

Explicitly distinguish and exclude path ownership for:

### WP03
GHCR publication and Azure F1 deployment automation.

### WP04
Persistent SQLite initialization, data update, recovery, and persistence workflows.

### WP05
Twelve Data runtime secret/provider automation and bounded scheduling.

### WP06
Public Streamlit/System Health deployment/presentation and truthful public diagnostics.

### WP07
Deployment stability, recovery, cost, and no-bypass validation.

### WP08
Operational runbook, final documentation, release acceptance/publication.

A path must not be assigned to WP02 if its primary purpose belongs to one of these WPs.

## 10. Literal designation table

Produce a table titled exactly:

`WP02 REPOSITORY PATH DESIGNATION`

Columns:
- `Path`
- `Exists`
- `Authorized Operation`
- `Classification`
- `Mandatory/Optional`
- `WP02 Responsibility`
- `Boundary Rationale`

Every row must contain one literal path.

No wildcard/glob row is valid.

## 11. Closed authorized path set

Produce a sorted deterministic block titled:

`WP02_AUTHORIZED_PATH_SET`

Every path from the designation table that Terra may mutate must appear exactly once.

Report:
- `AUTHORIZED_PATH_COUNT`
- `EXISTING_PATH_COUNT`
- `CREATE_PATH_COUNT`
- `MODIFY_PATH_COUNT`
- `DELETE_PATH_COUNT`
- `RENAME_PATH_COUNT`
- `PRODUCTION_SOURCE_PATH_COUNT`
- `TEST_PATH_COUNT`
- `CONFIG_PATH_COUNT`
- `SCRIPT_PATH_COUNT`
- `DOC_PATH_COUNT`

If a rename is authorized, both literal source and destination must be represented and the semantics explained.

## 12. Explicit denylist

Produce:

`WP02_EXPLICIT_DENIED_PATH_SET`

Include literal plausible-neighbor paths inspected during reconciliation that must not be mutated by WP02 because they belong to another WP or violate architecture.

Supplementary directory/category denials are allowed, but cannot replace literal decisions for empirically plausible candidate paths.

## 13. Governance amendment contract

Because the frozen manifest itself contains no literal paths, this designation is a new Luna governance decision.

The report must state:

`RELEASE 1.12 WP02 PATH DESIGNATION: LUNA GOVERNANCE AMENDMENT`

and:

`THE FROZEN RELEASE 1.12 MANIFEST REMAINS HISTORICAL; THIS AUTHORITY SUPPLIES THE SEPARATELY REQUIRED WP02 LITERAL PATH CONTRACT`

Do not silently rewrite history or claim the original manifest contained these paths.

## 14. Publication decision

Luna must decide whether this designation must itself be committed/published before Terra implementation.

Output exactly one:

`WP02 PATH DESIGNATION PUBLICATION REQUIREMENT: REQUIRED`

or

`WP02 PATH DESIGNATION PUBLICATION REQUIREMENT: NOT REQUIRED`

with governance rationale.

If publication is REQUIRED:
- this authority still performs no mutation;
- specify the exact future governance-artifact path to be published;
- Terra implementation remains blocked until a separate publication authority completes.

If publication is NOT REQUIRED:
- explain why the authority output itself is sufficient as the separately required literal contract;
- a replacement Terra authority may then embed it verbatim.

Do not leave publication requirement ambiguous.

## 15. Terra replacement-authority contract

If path designation is complete and any required publication prerequisite is satisfied/not required, emit:

`GPT-5.6 TERRA WP02 MUTATION CONTRACT: ONLY WP02_AUTHORIZED_PATH_SET MAY BE MUTATED`

The replacement Terra authority must:
- embed the entire path set verbatim;
- preserve operation restrictions per path;
- block on any required path outside the set;
- block on generated tracked files outside the set;
- block on architecture changes beyond this designation;
- require a new Luna amendment to expand the set.

The prior category-based Terra WP02 authority must not be treated as sufficient by itself.

## 16. Read-only prohibition

Authorized:
- repository reads;
- Git reads;
- GitHub reads;
- read-only filesystem inspection.

Not authorized:
- repository edits/creates/deletes;
- staging;
- commits;
- branches;
- pushes;
- PR creation/merge;
- issue/Project/milestone mutation;
- Docker build/run;
- GHCR;
- Azure;
- provider requests;
- package changes/installations;
- schema changes;
- production execution.

Avoid temporary files. If unavoidable, create them outside the repository, remove them, and report them.

## 17. Governance verification

Before completion verify:
- #261 remains Open/Todo.
- #262 remains dependency-gated.
- milestone #63 remains Open.
- no lifecycle mutation occurred.
- canonical base did not change due to this authority.
- working tree was not altered.
- pre-existing unrelated untracked `prompters/` remains outside the designated set unless Luna explicitly and justifiably designates a literal path there; default expectation is exclusion.

## 18. Required output

Final report must contain:
1. canonical base SHA;
2. frozen-contract findings;
3. empirical repository findings;
4. category-by-category designation decisions;
5. `WP02 REPOSITORY PATH DESIGNATION`;
6. `WP02_AUTHORIZED_PATH_SET`;
7. counts;
8. `WP02_EXPLICIT_DENIED_PATH_SET`;
9. governance-amendment statement;
10. publication requirement decision;
11. read-only mutation audit;
12. Terra replacement-authority handoff or exact blocker.

## 19. Acceptance gates

### Gate A — base/planning reconciliation
Published contract and canonical base proven.

### Gate B — empirical topology
Actual repository structure inspected sufficiently to make path decisions.

### Gate C — architecture designation
Missing path ownership is explicitly decided by Luna rather than inferred by Terra.

### Gate D — literal closure
Every authorized mutation path is literal and the set is deterministic/closed.

### Gate E — WP boundary preservation
WP03–WP08 ownership is not preempted.

### Gate F — governance amendment truthfulness
Original manifest is not retroactively misrepresented.

### Gate G — publication requirement
Required/not-required decision is explicit.

### Gate H — read-only audit
No mutation occurred.

## 20. Required markers

`RELEASE 1.12 WP02 — PATH DESIGNATION BASE RECONCILIATION: PASS`

`RELEASE 1.12 WP02 — EMPIRICAL REPOSITORY TOPOLOGY: PASS`

`RELEASE 1.12 WP02 — LUNA ARCHITECTURE PATH DESIGNATION: PASS`

`RELEASE 1.12 WP02 — AUTHORIZED PATH SET CLOSURE: PASS`

`RELEASE 1.12 WP02 — CROSS-WP PATH OWNERSHIP: PASS`

`RELEASE 1.12 WP02 — PATH DESIGNATION GOVERNANCE AMENDMENT: PASS`

`RELEASE 1.12 WP02 — PATH DESIGNATION PUBLICATION DECISION: PASS`

`RELEASE 1.12 WP02 — PATH DESIGNATION MUTATION AUDIT: PASS`

Acceptance:

`RELEASE 1.12 WP02 — REPOSITORY PATH DESIGNATION: PASS`

Governance amendment:

`RELEASE 1.12 WP02 PATH DESIGNATION: LUNA GOVERNANCE AMENDMENT`

`THE FROZEN RELEASE 1.12 MANIFEST REMAINS HISTORICAL; THIS AUTHORITY SUPPLIES THE SEPARATELY REQUIRED WP02 LITERAL PATH CONTRACT`

If ready for Terra:

`GPT-5.6 TERRA WP02 MUTATION CONTRACT: ONLY WP02_AUTHORIZED_PATH_SET MAY BE MUTATED`

`RELEASE 1.12 WP02 — REPLACEMENT TERRA IMPLEMENTATION AUTHORITY: READY TO CREATE`

Terminal:

`RELEASE 1.12 WP02 — REPOSITORY PATH DESIGNATION AUTHORITY COMPLETE`

If unresolved:

`RELEASE 1.12 WP02 — REPOSITORY PATH DESIGNATION AUTHORITY BLOCKED`

State the exact unresolved architecture/path/publication decision and do not authorize implementation.

## 21. Completion boundary

This authority completes only after Luna makes the missing architecture/path-ownership decision, freezes a closed literal WP02 path set, explicitly resolves whether that designation requires publication, and preserves all WP boundaries without mutation.

It does not implement WP02 and does not close #261.

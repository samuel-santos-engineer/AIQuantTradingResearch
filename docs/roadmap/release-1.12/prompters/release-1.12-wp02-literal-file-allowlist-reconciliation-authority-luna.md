# GPT-5.6 Luna — Release 1.12 WP02 Literal File-Allowlist Reconciliation Authority

**Selected execution model: GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY for this authority: contract interpretation, repository/path reconciliation, architecture ownership, literal allowlist/denylist definition, acceptance criteria, and governance.
- **GPT-5.6 Terra** — implementation, validation execution, and separately authorized Git/GitHub/Docker mutations only after Luna freezes the literal path contract.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Mission

Resolve the exact blocker preventing Release 1.12 WP02 implementation:

`#261 — Productionized Container & Runtime Composition`

The merged frozen Release 1.12 manifest states that no future implementation path is authorized merely by planning and requires each implementation WP authority to replace that boundary with a **literal file allowlist before mutation**.

This Luna authority must inspect the frozen Release 1.12 planning contract and the current repository tree, then freeze the exact literal repository paths that WP02 may create, modify, delete, or rename.

This is a **read-only governance/reconciliation authority**.

It performs no implementation.

## 2. Canonical starting boundary

Expected implementation/planning base:

`d63f8748772f579f2c46cf79df3563627b31a958`

Expected state:
- local `main` = `origin/main` at that SHA, 0/0.
- PR #268 merged with exact 3/3 planning artifacts.
- #260 WP01 Closed/Done.
- #261 WP02 Open/Todo.
- #262 WP03 Open/Todo but dependency-gated.
- milestone #63 Open, 7 open / 1 closed.
- Product Release 1.11 abandoned/nonexistent.
- sequence `1.10 → 1.12 → 2.0 → 2.1 → 2.2 → 2.3`.

Fresh evidence controls if any expected read-only fact has changed.

## 3. Binding source documents

Read in full:

1. `docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md`
2. `docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md`
3. `docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md`

Also inspect, read-only, the repository paths necessary to understand existing:
- container definitions;
- build/runtime composition;
- .NET Worker/application startup;
- Python/Streamlit startup;
- configuration;
- JSON handoff;
- SQLite path handling;
- observability/System Health boundaries;
- tests and architecture tests;
- deployment/runtime scripts.

Do not infer path ownership from filenames alone.

## 4. Binding architecture constraints

The literal allowlist must preserve:
- Azure App Service Linux F1 target.
- custom Docker.
- public/default HTTPS/DNS.
- persistent `/home`.
- configuration-driven SQLite with DELETE journal selected.
- public/free GHCR.
- strict recurring infrastructure cost `$0.00`.
- reference/demo-only scope.
- .NET canonical pipeline ownership.
- atomic JSON handoff.
- Python parser → frame → presentation → Streamlit.
- Streamlit has no SQLite/provider/Worker supervision ownership.
- Release 1.8 JSON-over-stdio remains separate.
- Release 1.10 observability/System Health truthfulness.
- deterministic/replay/simulated provenance.
- no Azure SQL, Azure Files, Container Apps, mandatory ACR, paid service, ML, backtesting, live trading, or architecture bypass.

## 5. Required reconciliation method

Luna must derive the allowlist from **both**:
1. the frozen WP02 responsibilities/acceptance contract; and
2. the empirical current repository structure at the canonical base.

For every proposed path, state:
- literal repository-relative path;
- whether it currently exists;
- permitted operation:
  - `CREATE`
  - `MODIFY`
  - `DELETE`
  - `RENAME`
- exact WP02 responsibility requiring it;
- why the change does not belong to WP03–WP08;
- whether production/source code is touched;
- whether tests are required.

No wildcard, glob, directory-only, “such as”, “as needed”, “related files”, or category authorization is sufficient.

## 6. Literal allowlist requirements

The final contract must contain a table titled exactly:

`WP02 LITERAL FILE ALLOWLIST`

Every authorized path must appear as one literal repository-relative path.

If a new file is authorized, name the exact future path.

If an existing file is not necessary, do not authorize it prophylactically.

If Luna cannot determine a necessary exact path without making an implementation-level architecture choice unsupported by the frozen contract, mark that path decision `UNRESOLVED` and BLOCK rather than guessing.

## 7. Explicit denylist

The final contract must also contain:

`WP02 EXPLICIT FILE DENYLIST`

At minimum, explicitly deny literal paths discovered during reconciliation that belong to:
- WP03 GHCR/Azure deployment automation;
- WP04 SQLite initialization/update/recovery;
- WP05 Twelve Data runtime automation/secrets;
- WP06 public Streamlit/System Health presentation;
- WP07 stability/cost/no-bypass acceptance;
- WP08 runbook/final release acceptance;
- Release 2.0+;
- Initiative-1.11 historical evidence;
- unrelated trading/domain/ML/backtesting/schema surfaces.

Directory/category statements may supplement the denylist, but may not replace literal path decisions for plausible nearby implementation candidates.

## 8. Tests and validation-path ownership

Luna must identify exact test paths Terra may modify or create for WP02.

For each test path:
- literal path;
- operation;
- behavior it validates;
- production path(s) it covers.

If existing test projects can validate WP02 without file mutation, state that explicitly and do not add unnecessary test paths to the allowlist.

## 9. Docker/runtime path decision

Luna must explicitly decide the exact repository paths, if any, for:
- Dockerfile/container definition;
- `.dockerignore`;
- entrypoint/supervision script;
- runtime configuration;
- startup/configuration source changes;
- container-specific test/support files.

Do not authorize GHCR publication/deployment automation merely because Docker is involved.

## 10. Path-set closure rule

At the end of reconciliation, produce a sorted deterministic set:

`WP02_AUTHORIZED_PATH_SET`

The set must be closed: Terra may not mutate any repository path absent from it.

Also report:
- `AUTHORIZED_PATH_COUNT`
- `EXISTING_PATH_COUNT`
- `CREATE_PATH_COUNT`
- `PRODUCTION_SOURCE_PATH_COUNT`
- `TEST_PATH_COUNT`
- `DOC_PATH_COUNT`

A rename counts as both the explicitly named source and destination for authorization purposes.

## 11. Terra handoff contract

If reconciliation passes, Luna must state verbatim:

`GPT-5.6 TERRA WP02 MUTATION CONTRACT: ONLY WP02_AUTHORIZED_PATH_SET MAY BE MUTATED`

The next Terra authority must embed the complete literal set verbatim before implementation.

Terra must block on:
- any needed path outside the set;
- any rename destination outside the set;
- any generated tracked file outside the set;
- any architecture decision that materially changes the frozen contract.

A later Luna amendment is required to expand the set.

## 12. Read-only mutation prohibition

This authority authorizes:
- repository reads;
- Git reads;
- GitHub reads;
- local inspection commands that do not alter tracked/untracked repository state.

It does NOT authorize:
- editing/creating/deleting repository files;
- staging;
- commit;
- push;
- branch creation;
- PR creation/merge;
- issue closure;
- Project mutation;
- milestone mutation;
- Docker build/run;
- GHCR mutation;
- Azure mutation;
- provider request;
- package installation/change;
- schema change;
- production execution.

Temporary read-back files should be avoided. If unavoidable, they must be outside the repository or removed before completion and explicitly accounted for.

## 13. Governance verification

Before completion verify:
- #261 remains Open/Todo.
- #262 remains Open/Todo and dependency-gated.
- milestone #63 remains Open.
- no lifecycle mutation occurred.
- canonical base remains unchanged unless an external mutation is discovered.
- working tree state was not altered by this authority.

Pre-existing unrelated untracked `prompters/` files are not WP02-authorized merely because they exist locally.

## 14. Required output

The final Luna report must include:
1. canonical base SHA;
2. planning-contract interpretation;
3. empirical repository-path findings;
4. `WP02 LITERAL FILE ALLOWLIST`;
5. `WP02 EXPLICIT FILE DENYLIST`;
6. sorted `WP02_AUTHORIZED_PATH_SET`;
7. path counts;
8. unresolved decisions, if any;
9. read-only mutation audit;
10. exact Terra handoff statement.

## 15. Acceptance gates

### Gate A — base reconciliation
Canonical base and planning publication proven.

### Gate B — frozen contract interpretation
The manifest's no-future-path authorization rule is explicitly honored.

### Gate C — repository inspection
Current implementation structure is empirically inspected.

### Gate D — literal allowlist
Every authorized mutation path is literal, justified, and operation-scoped.

### Gate E — literal denylist
Plausible adjacent/out-of-scope paths are explicitly excluded.

### Gate F — path-set closure
Sorted deterministic authorized set and counts are produced.

### Gate G — architecture preservation
No path authorization silently moves responsibilities across WP boundaries.

### Gate H — read-only audit
No repository/Git/GitHub lifecycle/Docker/Azure/provider mutation occurred.

## 16. Required markers

`RELEASE 1.12 WP02 — LITERAL ALLOWLIST BASE RECONCILIATION: PASS`

`RELEASE 1.12 WP02 — FROZEN MANIFEST PATH-AUTHORIZATION RULE: RECONCILED`

`RELEASE 1.12 WP02 — REPOSITORY PATH INSPECTION: PASS`

`RELEASE 1.12 WP02 — LITERAL FILE ALLOWLIST: PASS`

`RELEASE 1.12 WP02 — EXPLICIT FILE DENYLIST: PASS`

`RELEASE 1.12 WP02 — AUTHORIZED PATH SET CLOSURE: PASS`

`RELEASE 1.12 WP02 — ARCHITECTURE & WP OWNERSHIP PRESERVATION: PASS`

`RELEASE 1.12 WP02 — ALLOWLIST RECONCILIATION MUTATION AUDIT: PASS`

Acceptance:

`RELEASE 1.12 WP02 — LITERAL FILE-ALLOWLIST RECONCILIATION: PASS`

Terra handoff:

`GPT-5.6 TERRA WP02 MUTATION CONTRACT: ONLY WP02_AUTHORIZED_PATH_SET MAY BE MUTATED`

`RELEASE 1.12 WP02 — RECONCILED TERRA IMPLEMENTATION AUTHORITY: READY TO CREATE`

Terminal:

`RELEASE 1.12 WP02 — LITERAL FILE-ALLOWLIST RECONCILIATION AUTHORITY COMPLETE`

On any unresolved path/architecture decision:

`RELEASE 1.12 WP02 — LITERAL FILE-ALLOWLIST RECONCILIATION AUTHORITY BLOCKED`

Do not emit PASS or authorize Terra implementation if any necessary mutation path remains unresolved.

## 17. Completion boundary

This authority completes only when Luna has frozen a literal, closed, deterministic WP02 path set supported by both the frozen planning contract and empirical repository structure.

It does not implement WP02 and does not alter #261 lifecycle state.

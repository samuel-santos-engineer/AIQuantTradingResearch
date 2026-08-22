# Release 1.5 WP04 — Experiment Model & Contracts

## GitHub Issue
`#171 — Release 1.5 WP04 — Experiment Model & Contracts`

## 1. Authority

This prompt is the authoritative execution instruction for Release 1.5 WP04 of `samuel-santos-engineer/AIQuantTradingResearch`.

Release 1.5 is:

**Phase 4 — Release 1.5: Deterministic Research Experiment Foundation**

Selected experiment:

`simple-return-descriptive-summary-v1`

Identity scheme:

`aiq-experiment-identity-v1`

WP04 translates the frozen WP02 experiment semantics and WP03 identity/provenance/evidence semantics into the minimum immutable Application-owned production model and contract surface required by later Release 1.5 work packages.

Read completely before mutation:

- `docs/roadmap/release-1.5/RELEASE_1.5_DEFINITION.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.5/RELEASE_1.5_FILE_MANIFEST.md`
- `docs/architecture/data/EXPERIMENT_SEMANTICS.md`
- `docs/architecture/data/EXPERIMENT_IDENTITY_PROVENANCE_EVIDENCE.md`
- accepted Release 1.4 feature model/contracts and feature identity implementation
- accepted Release 1.4 feature-generation contracts
- relevant Release 1.2 dataset/snapshot contracts
- relevant Release 1.3 pipeline contracts
- Application project conventions, naming, nullability, analyzers, and immutable-model patterns
- WP01–WP03 completion evidence
- this WP04 authority and its five-line companion

Repository truth and accepted authorities take precedence over assumptions.

If WP02/WP03 semantics cannot be represented without inventing a new semantic decision, stop and report the smallest corrective authority required.

---

## 2. Objective

Implement exactly the minimum Application-owned experiment model and contracts required to represent the already-frozen Release 1.5 semantics.

The implementation must provide, as required by the accepted manifest and repository conventions:

- typed Experiment Definition Identity;
- typed Experiment Result Identity;
- the sole built-in Experiment Definition;
- immutable descriptive-summary evidence;
- immutable successful experiment-result evidence;
- exact Feature Set identity/provenance binding;
- request contract;
- bounded result/failure contract;
- experiment-computation seam;
- experiment-use-case seam if assigned to WP04 by the manifest;
- validation/construction invariants needed to prevent impossible semantic states.

WP04 must represent semantics, not implement the deterministic summary algorithm. Summary computation belongs to WP05.

---

## 3. Expected Starting State

Reconcile rather than assume:

- repository: `samuel-santos-engineer/AIQuantTradingResearch`;
- branch: `main`;
- `HEAD == origin/main`;
- expected baseline SHA: `2fa88ff70e8a772b2d10bfab0f550f4cd66dd504`;
- ahead/behind: `0/0`;
- staged paths: `0`;
- tracked modifications: `0`.

Expected lifecycle:

- #168 WP01: CLOSED / Done;
- #169 WP02: CLOSED / Done;
- #170 WP03: CLOSED / Done;
- #171 WP04: OPEN / Backlog;
- #172 WP05: OPEN / Backlog;
- #173–#180: OPEN / Backlog;
- milestone #46: OPEN with 10 open / 3 closed;
- Release 1.5 integration branch/PR: none;
- Release 1.6 implementation: none.

Expected technical baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- total: 214/214;
- SQLite schema: v2.

Accepted untracked Release 1.5 governance/semantic artifacts and manifest-defined out-of-band execution inputs are expected and are not implementation drift.

If #170 is not Closed/Done or #172 has started, stop before mutation.

---

## 4. WP04 Lifecycle Start

After starting-state gates pass:

- move only #171 Project #2 Status from Backlog to In Progress.

Read back the state.

If #171 is already In Progress solely because this exact WP04 execution partially started, continue idempotently if no unauthorized mutation occurred.

Do not mutate #172.

#172 must remain OPEN / Backlog throughout WP04.

---

## 5. Required Implementation Discovery

Before writing code, inspect existing accepted patterns rather than creating parallel abstractions.

At minimum inspect:

- Release 1.4 `FeatureIdentity` model;
- Release 1.4 `FeatureDefinition`;
- Release 1.4 `FeatureEvidence`;
- Release 1.4 feature-generation contracts;
- dataset/snapshot identity and evidence types consumed by features;
- Application namespace/folder conventions;
- immutable collection patterns;
- factory/constructor validation conventions;
- result/failure patterns;
- nullable-reference conventions;
- analyzer requirements.

Reuse established structural patterns when they preserve the frozen experiment semantics.

Do not refactor predecessor code merely to make Release 1.5 look more generic.

---

## 6. Application Ownership

All WP04 production changes must remain in:

`src/AIQuantTradingResearch.Application/**`

unless the accepted file manifest explicitly states otherwise.

Expected WP04 ownership:

- Domain delta: 0;
- Infrastructure delta: 0;
- Worker delta: 0.

The production graph must remain:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure.

No new project/reference edge is authorized.

---

## 7. Experiment Identity Types

Implement strongly typed semantic identity value types for:

- Experiment Definition Identity;
- Experiment Result Identity.

They must enforce the frozen WP03 external identity requirements:

- scheme: `aiq-experiment-identity-v1`;
- SHA-256 fingerprint form;
- exactly 64 lowercase hexadecimal characters;
- no malformed or alternate uppercase form accepted if predecessor conventions reject it.

Follow established Release 1.4 identity-type patterns where compatible.

WP04 owns the model representation and validation of identity values.

Do not yet implement canonical hashing unless the accepted execution plan/file manifest explicitly assigns canonical identity computation to WP04. If ownership is ambiguous, stop rather than duplicating future WP05 responsibility.

---

## 8. Built-In Experiment Definition

Represent exactly one built-in experiment definition:

`simple-return-descriptive-summary-v1`

The model must not expose arbitrary:

- statistic lists;
- formulas;
- scripts;
- expressions;
- configurable aggregation sets;
- plugins;
- strategy definitions.

The definition must be immutable.

Its semantic representation must be sufficient for the WP03 Experiment Definition Identity rules without adding ungoverned fields.

---

## 9. Descriptive Summary Evidence

Represent exactly the WP02 summary semantics:

- count;
- arithmetic mean;
- minimum;
- maximum.

The model must make the following states representable:

### Successful empty summary

- count = 0;
- mean absent;
- minimum absent;
- maximum absent.

### Successful non-empty summary

- count >= 1;
- mean present;
- minimum present;
- maximum present.

For one value, all three aggregates may be equal.

Impossible mixed states must be rejected, including examples such as:

- count 0 with any aggregate present;
- count > 0 with any aggregate absent;
- negative count;
- partial aggregate presence.

Do not represent absence using decimal zero, NaN, infinity, or sentinel values.

Use repository-compatible nullable/optional semantics without introducing a generalized option framework.

---

## 10. Decimal Evidence

Aggregate values must remain `decimal` semantic evidence.

Do not expose double/float aggregate values.

Do not round or normalize numerical values in the model merely for display.

Canonical decimal encoding remains governed by WP03; model storage must preserve exact decimal evidence required by later identity computation.

---

## 11. Experiment Result Evidence

Represent immutable successful Experiment Result evidence bound to:

- exact Experiment Definition Identity;
- exact Feature Set Identity;
- Experiment Result Identity;
- complete descriptive-summary evidence;
- required provenance/lineage references defined by WP03.

Do not create an Experiment Result identity for failure evidence.

Do not mix operational invocation metadata into the semantic result model.

The model must support deterministic successful empty results without a global empty singleton/sentinel identity.

---

## 12. Feature Set Binding

Use the accepted Release 1.4 Feature Set identity/evidence abstractions rather than duplicating them.

The Release 1.5 result/request boundary must preserve exact binding to the accepted Feature Set evidence required by WP02/WP03.

Do not:

- rebuild feature identity from raw decimals;
- weaken Feature Set identity to a string if a typed accepted abstraction exists;
- introduce storage/provider identifiers;
- substitute snapshot identity for Feature Set identity.

Feature Set identity is the direct semantic predecessor for the experiment result.

---

## 13. Provenance and Lineage Representation

Represent only the minimum provenance/lineage references needed by the frozen WP03 semantics.

Conceptual lineage remains:

`source state → dataset definition/research dataset → snapshot/version → feature definition → feature set → experiment definition → experiment result`

Prefer references to accepted predecessor semantic evidence over copied mutable structures.

Do not create:

- generalized graph infrastructure;
- persistence entities;
- registry records;
- run-history models;
- provider-specific provenance.

No cyclic reference may be introduced.

---

## 14. Request Contract

Implement the minimum immutable request needed to ask for the built-in experiment over exact accepted predecessor evidence.

The request must be semantically coherent with the accepted Release 1.5 execution plan.

At minimum it must identify:

- the experiment definition;
- the exact predecessor Feature Set target/evidence boundary required by the plan.

If the accepted plan specifies that Release 1.5 obtains Feature Set evidence through exact snapshot-based Release 1.4 feature generation rather than accepting an already-materialized Feature Set directly, preserve that boundary exactly.

Do not guess between those orchestration forms. Reconcile the definition, plan, manifest, and existing feature-generation contracts first.

If they materially conflict, stop.

---

## 15. Use-Case Contract

If the manifest assigns the Release 1.5 use-case interface to WP04, create only the minimum synchronous Application contract required by later WP07 integration.

Follow existing repository style.

Do not implement orchestration in WP04.

Do not add:

- async merely for future-proofing;
- cancellation semantics not already required;
- retry;
- scheduling;
- loops;
- persistence;
- provider acquisition.

If the manifest assigns the use-case contract to a later WP, do not create it here.

---

## 16. Computation Seam

Create the minimum computation seam assigned by the accepted plan/manifest for WP05 to implement.

The seam must consume already-valid semantic input appropriate to the computation boundary and return evidence sufficient for later successful Experiment Result construction.

Do not put the WP05 formula/aggregation algorithm into WP04.

Critically, avoid forcing WP05 to fabricate identities before the identity-computation ownership is available.

Reconcile the accepted plan/manifest to determine whether:

- the computation seam returns summary evidence only; or
- it returns a complete Experiment Result through an already-authorized identity facility.

Prefer the smallest contract consistent with the authorities.

If the current authorities require a complete identity-bearing result but do not assign identity computation to WP04/WP05 unambiguously, stop and report the ownership ambiguity before creating a broken seam.

---

## 17. Failure Contract

Represent the bounded Release 1.5 failure distinctions required by accepted semantics and plan.

At minimum reconcile support for:

- invalid request;
- unsupported experiment definition;
- predecessor Feature Set not found where applicable;
- dependency unavailable;
- invalid Feature Set evidence;
- invalid numeric evidence/computation;
- integrity conflict.

Use exact names already governed by the Release 1.5 definition/plan if present.

Do not invent duplicate synonyms.

Successful empty and successful non-empty results are not failures.

Unknown programming/system defects must remain outside bounded normalization and propagate later.

---

## 18. Result Contract

Provide a result contract that cleanly distinguishes:

- successful immutable Experiment Result evidence;
- bounded governed failure evidence.

The API must not allow a semantically successful result and failure to coexist.

The API must not allow success without complete required result evidence.

Follow established repository result/factory patterns when compatible.

Do not introduce exception-based control flow for governed semantic failures if predecessor Application patterns use explicit result evidence.

---

## 19. Validation Invariants

WP04 model/contracts must enforce structural invariants that are knowable without performing WP05 computation or WP06 orchestration validation.

Examples:

- identity format;
- required non-null references;
- built-in definition coherence;
- count/aggregate-presence coherence;
- immutable snapshots of supplied collections if collections exist;
- result identity/reference coherence that can be checked structurally;
- no partial successful result.

Do not prematurely implement WP06 validation precedence or dependency-failure mapping.

Separate model impossibility from orchestration validation.

---

## 20. Immutability

All semantic model evidence must be immutable after construction.

Protect against mutable collection aliasing where relevant.

Do not expose setters or mutable collections that can change semantic content after identity establishment.

Use repository-established immutable patterns; do not add a new package.

---

## 21. Identity Computation Ownership Gate

This gate is mandatory because Release 1.4 previously exposed an ownership ambiguity around computation versus identity construction.

Before implementing the WP04 contract surface, explicitly determine from the Release 1.5 execution plan and file manifest which WP owns canonical `aiq-experiment-identity-v1` computation.

Record that ownership in the execution report.

WP04 may model identities regardless.

WP04 must not implement the canonical identity computer unless clearly authorized.

The WP04 computation seam must not make the next WP impossible to implement without violating identity semantics.

If identity-computation ownership is missing or contradictory and the chosen contract depends on it, stop with:

`RELEASE 1.5 WP04 BLOCKED`

and request the smallest authority clarification.

---

## 22. Release 1.4 Preservation

Do not modify Release 1.4 feature semantics or behavior.

Preserve:

- `simple-return-lag-1-v1`;
- `aiq-feature-identity-v1`;
- exact snapshot/version lookup;
- feature computation;
- feature validation/failure mapping;
- Feature Set identity/provenance;
- one-shot feature Worker behavior;
- no feature persistence.

Experiment abstractions must remain downstream and separate.

---

## 23. Release 1.3 Pipeline Protection

Do not modify the fixed Release 1.3 five-stage pipeline.

Experiment model/contracts must not become:

- a sixth pipeline stage;
- a generalized pipeline node;
- a configurable DAG;
- an automatic pipeline continuation.

---

## 24. Persistence and Provider Protection

WP04 must introduce no:

- SQL;
- SQLite implementation;
- migration;
- schema change;
- experiment table;
- experiment registry/history/cache;
- HTTP/provider code;
- provider credentials;
- filesystem persistence.

SQLite remains schema v2.

Infrastructure production delta remains zero.

---

## 25. Explicit Deferrals

Do not implement placeholders or abstractions for:

- experiment persistence/registry/history;
- run identity;
- additional experiments/statistics;
- configurable formulas;
- feature persistence expansion;
- notebooks/workspaces;
- visualization/API;
- strategies/signals/backtesting;
- portfolio/risk;
- AI/ML/MLOps;
- acquisition orchestration;
- scheduling/retries/checkpoints;
- plugins/generalized DAGs;
- distributed execution;
- telemetry backends;
- Release 1.6 work.

Implement only what Release 1.5 WP04 requires now.

---

## 26. Authorized File Mutation

Use `RELEASE_1.5_FILE_MANIFEST.md` as the exact path authority.

WP04 may create/modify only the production Application paths assigned to WP04.

Do not assume filenames from this prompt if the manifest differs.

Before mutation, enumerate the exact authorized WP04 paths from the manifest.

After mutation, reconcile actual delta against those paths.

Expected category deltas:

- Application production: minimum manifest-authorized WP04 files;
- Domain production: 0;
- Infrastructure production: 0;
- Worker production: 0;
- permanent tests: 0;
- current-state docs: 0;
- semantic docs: 0;
- packages/projects/references/schema: 0/0/0/0.

Do not stage or commit.

---

## 27. No Permanent Tests in WP04

WP04 must not add permanent tests unless the accepted execution plan/file manifest explicitly assigns them here.

The accepted plan reserves permanent Application experiment tests for WP10.

Use compiler/analyzer feedback and, only if necessary, a removable offline probe to validate construction/invariants.

Any temporary probe must:

- be offline;
- avoid provider/network activity;
- avoid real credentials;
- be removed before final validation;
- leave zero generated/database residue.

Do not increase the 214 permanent-test baseline in WP04.

---

## 28. Technical Validation

Run:

`eng/verify.ps1 -Configuration Release`

Expected final baseline:

- Domain.Tests: 11/11;
- Application.Tests: 86/86;
- Infrastructure.Tests: 104/104;
- Architecture.Tests: 13/13;
- permanent total: 214/214;
- skipped: 0;
- build warnings/errors: 0/0;
- formatting: PASS;
- Gitleaks: PASS.

Also run:

- `git diff --check`;
- `git diff --cached --check`;
- direct whitespace inspection of newly created untracked WP04 production files and relevant governance artifacts.

Require:

- database/WAL/SHM/journal residue: 0;
- generated probe residue: 0;
- provider/network calls: 0;
- real credentials: 0.

---

## 29. Architecture Validation

Confirm after WP04:

- Domain → none;
- Application → Domain;
- Infrastructure → Application;
- Worker → Application, Infrastructure;
- unexpected edges: 0;
- cycles: 0.

No new package/project/reference edge.

No Infrastructure implementation for experiments.

---

## 30. Semantic Acceptance Matrix

Before closure, explicitly validate that the model/contracts can faithfully represent:

- sole built-in experiment definition;
- valid typed definition/result identities;
- successful empty summary;
- successful single-value summary;
- successful non-empty summary;
- exact decimal aggregate evidence;
- exact Feature Set binding;
- immutable result evidence;
- required provenance/lineage references;
- bounded success/failure distinction;
- no result identity on failure;
- no partial success;
- no operational metadata in semantic evidence.

Also confirm the contracts do not themselves compute mean/min/max.

---

## 31. Repository and Git Protection

Do not:

- stage;
- commit;
- create/switch an integration branch;
- push;
- create/merge PRs;
- tag;
- release;
- mutate schema/packages/projects/references;
- alter predecessor semantics;
- begin WP05;
- begin Release 1.6.

Git transport mutation budget:

`0`

Repository mutation budget:

only manifest-authorized WP04 Application production paths.

---

## 32. Authorized GitHub Mutation Budget

At WP04 start after gates pass:

1. #171 Project Status: Backlog → In Progress.

At successful completion only:

2. post one concise WP04 completion-evidence comment to #171;
3. close #171 as completed;
4. set #171 Project Status to Done.

Do not mutate #172.

Milestone #46 remains OPEN.

---

## 33. Completion Gate

WP04 may close only if:

- #170 is Closed/Done;
- #171 was In Progress during execution;
- #172 remains Open/Backlog;
- actual repository delta exactly matches manifest-authorized WP04 paths;
- model/contracts faithfully represent WP02/WP03;
- identity-computation ownership was explicitly reconciled;
- no WP05 computation was implemented;
- Domain/Infrastructure/Worker/test deltas are zero;
- package/project/reference/schema deltas are zero;
- SQLite remains v2;
- production graph is unchanged and acyclic;
- permanent tests remain 214/214;
- Architecture.Tests remain 13/13;
- build warnings/errors are 0/0;
- canonical verification passes;
- formatting/Gitleaks/whitespace pass;
- residue is zero;
- provider/network execution is zero;
- no Release 1.6 work exists.

If any gate fails, do not close #171 or mark it Done.

---

## 34. Completion Evidence Comment

On success, post concise evidence to #171 covering:

- exact Application files created/modified;
- typed Experiment Definition/Result identities;
- built-in `simple-return-descriptive-summary-v1`;
- immutable summary/result evidence;
- empty/non-empty invariant representation;
- exact Feature Set/provenance binding;
- request/result/computation/use-case contracts actually authorized;
- bounded failure contract;
- canonical identity-computation ownership decision;
- explicit confirmation that WP05 computation was not implemented;
- zero Domain/Infrastructure/Worker/test/package/reference/schema delta;
- SQLite v2;
- 214/214 tests and 13/13 Architecture.Tests;
- canonical verification/Gitleaks/whitespace PASS;
- #172 preserved Open/Backlog.

---

## 35. Final Read-Back

After successful closure verify:

- #171: CLOSED / Done;
- #172: OPEN / Backlog;
- #173–#180: unchanged Open / Backlog;
- milestone #46: OPEN;
- milestone counts: 9 open / 4 closed;
- staged paths: 0;
- commits/branches/pushes/PRs: 0;
- Release 1.6 work: 0.

Report cumulative accepted Release 1.5 untracked candidate/governance artifacts accurately.

---

## 36. Stop Conditions

Stop without unauthorized repair if:

- repository/account is wrong;
- #170 is not Closed/Done;
- #172+ started unexpectedly;
- WP04 file-manifest ownership is ambiguous;
- WP02/WP03 semantics conflict with implementation requirements;
- identity-computation ownership is ambiguous and blocks a valid contract;
- satisfying WP04 requires WP05 computation;
- premature WP05+ implementation exists;
- Release 1.6 implementation exists;
- architecture/schema baseline drifted;
- canonical verification fails;
- security/whitespace/residue gates fail;
- a new package/project/reference/schema change is required.

Report the smallest corrective authority required.

---

## 37. Required Execution Report

Report:

1. executive summary;
2. authorities reviewed;
3. starting Git/GitHub state;
4. lifecycle reconciliation;
5. implementation patterns inspected;
6. exact authorized paths;
7. identity-computation ownership decision;
8. identity types;
9. built-in experiment definition;
10. summary evidence model;
11. empty/non-empty invariants;
12. Experiment Result evidence;
13. Feature Set/provenance/lineage binding;
14. request contract;
15. computation seam;
16. use-case contract if authorized;
17. result/failure contract;
18. immutability/invariants;
19. predecessor/pipeline/schema/provider protection;
20. repository delta;
21. semantic acceptance matrix;
22. validation/test counts;
23. architecture/security/whitespace/residue;
24. GitHub lifecycle mutations;
25. final #171/#172/milestone state;
26. findings/blockers;
27. next authorized WP.

---

## 38. Required Terminal Marker

On success, end exactly:

`RELEASE 1.5 WP04 COMPLETE`

Then:

`NEXT AUTHORIZED WORK PACKAGE: WP05 — Deterministic Summary Computation — GitHub issue #172`

Do not begin WP05.

If blocked, end:

`RELEASE 1.5 WP04 BLOCKED`

and identify the smallest corrective authority required.

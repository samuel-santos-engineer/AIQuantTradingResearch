# INIT-1.11 WP02 — Execution Authority Amendment

## Model assignment
- **GPT-5.6 Luna** — contract, policy, architecture, reconciliation, acceptance criteria, governance.
- **GPT-5.6 Terra** — PRIMARY: WP02 implementation/probe execution, validation, approved Azure/Git/GitHub lifecycle mutations.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

# Purpose
Amend and supersede the operational bindings of the previously generated WP02 authority for:

**WP02 — Minimal Docker + App Service F1 Execution Probe**

This amendment does not broaden WP02 scope. It binds execution to the current published Initiative-1.11 governance baseline, GitHub issue lifecycle, strict-zero-cost controls, and prerequisite gates.

Execute this amendment together with the substantive WP02 probe contract. Where an older WP02 authority conflicts with this amendment, **this amendment controls**.

# Canonical initiative identity
Initiative:

`Phase 4 - Initiative-1.11: Public Reference Deployment / Azure App Service F1 Feasibility Qualification`

Milestone:
- GitHub milestone **#62**
- remains Open until initiative completion.

WP02:
- GitHub issue **#253**
- expected pre-execution lifecycle: **Open**
- Project #2 expected Status: **Todo**
- Project Release: **unset**

Binding:
`Initiative-1.11 ≠ Product Release 1.11`

Product Release 1.11 remains abandoned.

Product sequence remains:
`1.10 → 2.0 → 2.1 → 2.2 → 2.3`

# Published governance baseline
The four Initiative-1.11 planning artifacts were published through PR **#258**:

`Docs: publish Initiative-1.11 Azure F1 feasibility governance`

Publication commit before merge:
`6547d600fe29d1c025ca75a0202ac052f78e74fe`

The user reports PR #258 has been merged.

Before Azure mutation, independently verify:
- PR #258 is merged;
- current `origin/main` contains the four governance artifacts;
- identify and report the actual PR #258 merge commit;
- local repository baseline is reconcilable with `origin/main`;
- the WP02 contract in the published execution plan remains consistent with this amendment.

Do not assume the pre-merge commit is the main merge SHA.

Required marker:
`AZURE F1 WP02 — PUBLISHED GOVERNANCE BASELINE: VERIFIED`

# Governing predecessor
WP01 acceptance is already established:

`AZURE F1 WP01 — FEASIBILITY CONTRACT & RESOURCE PLAN: PASS`

WP01 GitHub lifecycle:
- #252 Closed;
- Project #2 Status Done.

WP02 must not execute unless WP01 remains accepted and #253 is the active next WP.

Required marker:
`AZURE F1 WP02 — PREDECESSOR & LIFECYCLE GATE: PASS`

# Frozen WP02 mission
Empirically determine whether the sole feasibility candidate can satisfy the minimal execution boundary:

**Azure App Service Linux F1 + custom Docker + persistent `/home`**

WP02 proves only:
1. F1 availability/eligibility for the actual subscription/region path;
2. custom Docker execution on F1;
3. public HTTPS reachability;
4. `WEBSITES_ENABLE_APP_SERVICE_STORAGE=true`;
5. `/home` writable;
6. ordinary marker-file persistence across app restart;
7. ordinary marker-file persistence across container recycle/restart boundary that can be validly induced/observed;
8. ordinary marker-file persistence across image redeployment;
9. truthful resource inventory/disposition.

WP02 does **not** prove SQLite filesystem correctness. That belongs to WP03.

# Strict-$0 invariant
Recurring infrastructure cost must remain exactly:

`$0.00`

Before creating any Azure resource:
- authenticate to the intended Azure account/subscription;
- identify the subscription used;
- verify App Service Linux F1 availability/eligibility;
- verify selected resources/SKUs do not introduce recurring infrastructure charges;
- prefer a free registry path if an external registry is needed;
- **do not make Azure Container Registry mandatory**;
- do not create paid ACR or any paid supporting resource;
- do not upgrade from F1;
- do not create Azure Files;
- do not create Container Apps;
- do not create paid monitoring/telemetry resources;
- do not enable optional paid features.

If strict `$0.00` cannot be established before mutation, STOP as NOT FEASIBLE or BLOCKED according to causality.

Required marker:
`AZURE F1 WP02 — STRICT-ZERO-COST PREFLIGHT: PASS`

# Tooling/authentication prerequisite gate
Before Azure mutation verify:
- Azure CLI or another explicitly supported Azure control plane tool is installed and executable;
- authentication succeeds;
- intended subscription is accessible;
- tenant/subscription context is unambiguous;
- required resource-provider/control-plane operations are available;
- GitHub/registry authentication required for the chosen free image path is available;
- secrets will not be printed or committed.

Do not install broad unrelated tooling or alter machine policy without explicit need.

If interactive authentication is required, perform only the minimum safe authentication flow permitted by the environment. Never expose credentials/tokens in output.

Required marker:
`AZURE F1 WP02 — TOOLING & AUTHENTICATION: PASS`

# Official-documentation refresh
Before Azure resource creation, refresh current official Microsoft documentation for:
- App Service custom Linux containers;
- F1/Free App Service limits;
- persistent `/home` behavior for Linux custom containers;
- `WEBSITES_ENABLE_APP_SERVICE_STORAGE`;
- deployment/redeployment behavior relevant to the probe.

Use authoritative Microsoft documentation.

If current documentation materially contradicts the frozen feasibility assumptions, STOP before mutation and report the conflict for Luna reconciliation.

Required marker:
`AZURE F1 WP02 — OFFICIAL DOCUMENTATION REFRESH: PASS`

# Isolation boundary
Use the smallest isolated probe possible.

Do not modify production architecture.

Allowed repository changes only if the published WP02 contract explicitly authorizes probe/evidence paths. Before writing anything into the repository, validate the published file manifest and allowed/forbidden paths.

Prefer temporary/external probe construction when it avoids contaminating product code.

Forbidden unless explicitly authorized by the published contract:
- production application architecture changes;
- schema migrations;
- direct UI/SQLite bypasses;
- provider integration;
- ML/backtesting/trading;
- Release 2.0 work;
- telemetry dependency changes;
- Twelve Data calls.

Twelve Data requests for WP02 must equal **0**.

# Empirical probe protocol
Use a unique non-secret marker value and record timestamps/evidence without sensitive identifiers.

## Probe 1 — F1 creation
Prove the selected App Service plan is actually Free/F1.

Record sanitized:
- region;
- OS;
- SKU/tier;
- relevant plan/app configuration;
- resource inventory.

Marker:
`AZURE F1 WP02 PROBE 1 — F1 AVAILABILITY: PASS`

## Probe 2 — custom Docker execution
Deploy the minimal custom Docker image through an approved zero-cost registry/image path.

Prove the app is actually executing the custom container.

Marker:
`AZURE F1 WP02 PROBE 2 — CUSTOM DOCKER: PASS`

## Probe 3 — HTTPS
Prove public HTTPS returns the expected probe response.

Do not claim SLA/production readiness.

Marker:
`AZURE F1 WP02 PROBE 3 — PUBLIC HTTPS: PASS`

## Probe 4 — persistent /home
Set/verify:

`WEBSITES_ENABLE_APP_SERVICE_STORAGE=true`

Prove `/home` is writable.

Write the unique ordinary marker file under `/home` and read it back.

Marker:
`AZURE F1 WP02 PROBE 4 — PERSISTENT HOME: PASS`

## Probe 5 — app restart
Perform an App Service restart through a valid control-plane operation.

After restart:
- prove app recovers;
- prove the exact marker remains.

Marker:
`AZURE F1 WP02 PROBE 5 — APP RESTART PERSISTENCE: PASS`

## Probe 6 — container recycle/restart
Induce or observe a valid container lifecycle boundary distinct enough from the prior proof to support the contract.

Do not fabricate platform internals. Record exactly what operation/event was used and what it proves.

Afterward prove the marker remains.

Marker:
`AZURE F1 WP02 PROBE 6 — CONTAINER RECYCLE PERSISTENCE: PASS`

## Probe 7 — image redeployment
Deploy a distinguishable second probe image/revision without changing the persistence location.

Prove:
- new image/version is executing;
- same `/home` marker survives.

Marker:
`AZURE F1 WP02 PROBE 7 — IMAGE REDEPLOYMENT PERSISTENCE: PASS`

## Probe 8 — inventory
Inventory every resource created/used for WP02 and classify:
- resource type;
- SKU/tier;
- recurring-cost expectation;
- whether retained or removed;
- reason.

Marker:
`AZURE F1 WP02 PROBE 8 — RESOURCE INVENTORY: PASS`

# Evidence and redaction
Evidence must be sufficient to support the conclusion but must not disclose:
- access tokens;
- passwords;
- publish profiles;
- registry credentials;
- connection strings;
- secret environment values;
- unnecessary tenant/subscription identifiers;
- personally identifying account data.

Sanitize resource identifiers when reporting publicly if the governance contract requires it.

Do not commit secrets.

# Result classification
Choose exactly one substantive result.

## PASS
Only if all eight probes pass and strict-zero-cost controls remain satisfied.

Required exact acceptance marker:

`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`

## NOT FEASIBLE
Use when empirical evidence shows the frozen candidate cannot satisfy WP02 under the strict constraints, for example:
- F1 cannot run the required custom container path;
- persistent `/home` requirement cannot be satisfied;
- required operation necessarily creates recurring infrastructure cost;
- platform behavior invalidates the frozen candidate.

A truthful NOT FEASIBLE result is valid and must not be converted into a workaround that violates governance.

## BLOCKED
Use when the candidate has not been disproven but execution cannot proceed/complete due to external prerequisite or evidence limitation, such as:
- authentication unavailable;
- subscription inaccessible;
- transient Azure outage;
- required control tooling unavailable;
- permissions insufficient.

Do not emit PASS when blocked.

# Resource disposition
Default: clean up WP02 resources after evidence collection.

Retention for WP03 is allowed only if:
- the published WP01 resource plan explicitly permits retaining the minimum zero-cost resources;
- all retained resources are verified compatible with strict recurring `$0.00`;
- retention materially supports WP03;
- exact retained inventory is reported.

Emit exactly one:
`AZURE F1 WP02 — RESOURCE DISPOSITION: CLEANED UP`

or

`AZURE F1 WP02 — RESOURCE DISPOSITION: RETAINED FOR WP03`

Never leave ambiguous resources behind.

# GitHub WP lifecycle — mandatory on PASS
**Only after** the exact acceptance marker:

`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`

perform WP02 lifecycle completion:
1. close GitHub issue **#253**;
2. ensure Project #2 Status becomes **Done**.

If closing #253 automatically changes Project Status to Done:
- do not perform a redundant explicit Project Status mutation;
- count only mutations actually executed.

If automation does not update Project Status:
- explicitly set #253 Project #2 Status to Done if authorized and available.

Then verify:
- #253 Closed;
- Project #2 Status Done;
- milestone #62 remains Open;
- #254 remains Open/Todo as the next WP;
- Project Release remains unset.

Required marker:
`AZURE F1 WP02 — GITHUB LIFECYCLE: CLOSED/DONE`

Never close #253 on NOT FEASIBLE or BLOCKED unless a separate Luna governance decision explicitly says the initiative terminates and defines lifecycle handling.

# WP03 handoff
Only after PASS + lifecycle completion:

`AZURE F1 WP02 — WP03 HANDOFF: READY`

WP03 remains separately governed and must not be executed under this authority.

# Git boundary
This authority does not automatically authorize a repository commit/PR for probe artifacts.

If the published execution plan explicitly requires repository evidence changes during WP02:
- make only those authorized working-tree changes;
- validate them;
- leave publication to a separate Git/PR authority unless the published plan explicitly grants publication authority.

Do not mix Azure feasibility execution with unrelated Git publication.

# Mutation audit
Report exact counts for:
- repository files modified/created;
- staging operations;
- commits;
- pushes;
- PRs;
- Azure resources created;
- Azure resources modified;
- Azure resources deleted;
- registry repositories/packages created/pushed if applicable;
- Twelve Data requests;
- issue mutations;
- milestone mutations;
- Project mutations;
- Release-field mutations;
- tags;
- GitHub Releases.

For successful PASS, expected GitHub lifecycle mutation:
- issue #253 close: 1;
- Project Status explicit mutation: 0 or 1 depending on automation;
- milestone mutations: 0;
- Release-field mutations: 0.

# Required PASS sequence
`AZURE F1 WP02 — PUBLISHED GOVERNANCE BASELINE: VERIFIED`
`AZURE F1 WP02 — PREDECESSOR & LIFECYCLE GATE: PASS`
`AZURE F1 WP02 — STRICT-ZERO-COST PREFLIGHT: PASS`
`AZURE F1 WP02 — TOOLING & AUTHENTICATION: PASS`
`AZURE F1 WP02 — OFFICIAL DOCUMENTATION REFRESH: PASS`
`AZURE F1 WP02 PROBE 1 — F1 AVAILABILITY: PASS`
`AZURE F1 WP02 PROBE 2 — CUSTOM DOCKER: PASS`
`AZURE F1 WP02 PROBE 3 — PUBLIC HTTPS: PASS`
`AZURE F1 WP02 PROBE 4 — PERSISTENT HOME: PASS`
`AZURE F1 WP02 PROBE 5 — APP RESTART PERSISTENCE: PASS`
`AZURE F1 WP02 PROBE 6 — CONTAINER RECYCLE PERSISTENCE: PASS`
`AZURE F1 WP02 PROBE 7 — IMAGE REDEPLOYMENT PERSISTENCE: PASS`
`AZURE F1 WP02 PROBE 8 — RESOURCE INVENTORY: PASS`
`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`
`AZURE F1 WP02 — RESOURCE DISPOSITION: CLEANED UP|RETAINED FOR WP03`
`AZURE F1 WP02 — GITHUB LIFECYCLE: CLOSED/DONE`
`AZURE F1 WP02 — WP03 HANDOFF: READY`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/POLICY/GOVERNANCE | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

# Success terminal
`AZURE F1 WP02 — EXECUTION AUTHORITY AMENDMENT COMPLETE`

# Non-PASS terminal
If NOT FEASIBLE, report evidence, disposition, zero/actual lifecycle mutations, and end:

`AZURE F1 WP02 — EXECUTION AUTHORITY AMENDMENT NOT FEASIBLE`

If BLOCKED, report exact blocker, resource disposition, mutation audit, and end:

`AZURE F1 WP02 — EXECUTION AUTHORITY AMENDMENT BLOCKED`

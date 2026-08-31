# Azure F1 WP02 — Minimal Docker + App Service F1 Execution Probe Authority

## Model assignment
- **GPT-5.6 Luna** — contract, policy, architecture, definition, reconciliation, acceptance criteria, governance, read-only/planning.
- **GPT-5.6 Terra** — PRIMARY: implementation, empirical validation execution, approved Git/GitHub/Azure mutations, cleanup.
- **GPT-5.6 Sol** — supporting analysis, synthesis, alternatives, exploratory/non-authoritative review; never silently replaces Luna/Terra.

**Selected execution model: GPT-5.6 Terra.**

# Mission
Execute **WP02 — Minimal Docker + App Service F1 Execution Probe** for the non-release initiative:

**Public Reference Deployment / Azure App Service F1 Feasibility Qualification**

This authority exists only to empirically qualify the hosting substrate.

It must prove or disprove that the operator's actual Azure environment can run a minimal custom Linux Docker container on App Service F1 with public HTTPS and persistent writable `/home`, while preserving the strict `$0.00` recurring-cost contract and leaving no unintended resources behind.

This is NOT:
- Release 1.11;
- Release 2.0 implementation;
- production deployment;
- production Dockerization;
- SQLite qualification WP03;
- Twelve Data connectivity WP04;
- resource-envelope acceptance WP05;
- Phase B.

# Binding predecessor gate
Before any Azure/resource mutation, independently verify WP01 has passed its acceptance gate:

`AZURE F1 WP01 — FEASIBILITY CONTRACT & RESOURCE PLAN: PASS`

Also verify the initiative planning authority completed:

`AZURE F1 PUBLIC REFERENCE DEPLOYMENT — FEASIBILITY GOVERNANCE & EXECUTION PLANNING AUTHORITY COMPLETE`

If WP01 is represented by a GitHub issue:
- verify it is Closed;
- verify Project #2 Status is Done if the issue is tracked there;
- do not redundantly set Done if closure automation already did so.

If the non-release governance intentionally uses no GitHub issue for WP01:
- verify the planning artifacts explicitly record WP01 acceptance;
- do not invent a fake Release value or milestone solely for lifecycle tracking.

If WP01 acceptance cannot be proven, BLOCK before mutation.

Emit:
`AZURE F1 WP02 PREDECESSOR GATE: PASS`

# Canonical version-policy guardrail
Binding:

`RELEASE VERSION-SEQUENCING POLICY: PRESERVE 1.10 → 2.0; RELEASE 1.11 ABANDONED`

Therefore:
- do not create Release 1.11;
- do not alter milestone #60;
- do not alter Release 2.0 scope;
- do not add Project Release option 1.11;
- do not associate WP02 with Release 2.0 merely for tracking convenience.

Emit:
`AZURE F1 WP02 NON-RELEASE IDENTITY: PRESERVED`

# Repository baseline
Before mutation:
- fetch/prune as appropriate;
- verify local `main`;
- verify `origin/main`;
- record divergence;
- verify staging/worktree;
- preserve unrelated local prompt/control files;
- record current canonical development commit.

Known prior verified baseline:
`fe74af1d8dc59d8e381d3e27fe7a0885ee7f6468`

If main legitimately advanced:
- record exact new commit;
- prove the prior known baseline remains an ancestor.

Do not reset or discard unrelated local work.

Emit:
`AZURE F1 WP02 REPOSITORY BASELINE: VERIFIED`

# Planning-artifact baseline
Read the four initiative planning artifacts under the canonical non-release initiative directory and extract:
- WP02 objective;
- WP02 evidence requirements;
- allowed probe paths;
- forbidden production paths;
- resource-plan constraints;
- cleanup contract;
- evidence/redaction rules.

Do not silently broaden authority beyond those artifacts.

Emit:
`AZURE F1 WP02 PLANNING CONTRACT: LOADED`

# Authoritative Azure documentation refresh
Because Azure behavior/pricing changes, re-check current official Microsoft/Azure documentation before execution.

Record:
- source title;
- official URL;
- access date;
- relevant claim.

At minimum re-check:
- App Service Linux custom containers;
- F1 hosting-plan eligibility/limits/pricing;
- persistent storage for Linux custom containers;
- `WEBSITES_ENABLE_APP_SERVICE_STORAGE`;
- custom-container registry configuration;
- restart/redeployment semantics where documented.

Documentation is contextual evidence only.

Empirical PASS requires actual execution in the operator's Azure environment.

Emit:
`AZURE F1 WP02 DOCUMENTATION REFRESH: COMPLETE`

# Strict-zero-cost preflight
Before creating any Azure resource:
- inspect subscription/account context;
- identify intended region;
- confirm an F1 App Service plan is actually selectable/available;
- identify every resource that would be created;
- identify its pricing tier;
- prove the plan contains no intentionally paid resource.

Forbidden unless a later explicit authority overrides:
- paid App Service SKU;
- Azure Files;
- paid ACR tier;
- paid database;
- paid monitoring product;
- any resource whose recurring charge cannot be shown as $0 under the chosen configuration.

If F1 is unavailable to the actual subscription/region and no other region can satisfy the same strict-$0 contract without changing the test objective, return NOT FEASIBLE/BLOCKED rather than substituting a paid tier.

Emit:
`AZURE F1 WP02 ZERO-COST PREFLIGHT: PASS`

# Registry/image rule
Use the smallest truthful registry/image path available under the strict-$0 contract.

Preferred:
- GHCR or another already-free registry path available to the operator;
- or another documented zero-cost mechanism accepted by App Service.

Do not make Azure Container Registry mandatory.

Do not create a paid ACR resource.

Any external registry mutation must be separately counted.

# Probe design
The probe must be minimal, isolated, deterministic, and non-production.

It should expose only enough behavior to prove:
- container boot;
- process health;
- public HTTPS reachability;
- writable `/home`;
- persisted marker/file state;
- restart/recycle persistence;
- image-redeployment persistence.

The probe must not:
- run production market-data pipelines;
- call Twelve Data;
- use production SQLite schema;
- mutate production application architecture;
- introduce Azure SDK dependencies into Domain/Application;
- act as a production frontend;
- claim WP03 results.

Prefer a tiny dedicated probe image/application over production-container adaptation.

# Allowed repository mutation
Only if the initiative file manifest explicitly allows probe files.

Allowed categories may include:
- isolated probe Dockerfile;
- minimal probe application/script;
- narrow probe configuration;
- probe documentation/evidence metadata;
- narrowly scoped ignore rules if required.

Before editing:
- freeze exact allowed paths;
- reject any path outside the manifest.

If the manifest does not authorize repository probe files, perform the probe from temporary/local tooling without repository-content mutation.

Emit:
`AZURE F1 WP02 PROBE PATH ALLOWLIST: FROZEN`

# Azure resource naming/inventory
Before creation, freeze an inventory template containing for every resource:
- exact name;
- type;
- subscription;
- resource group;
- region;
- SKU/tier;
- purpose;
- creation command/action;
- expected recurring cost;
- cleanup command/action;
- retention status.

Prefer one dedicated temporary resource group for atomic cleanup if compatible with strict-$0 and governance.

Do not reuse unrelated production resources unless the planning contract explicitly permits it.

# Required empirical tests

## Test 1 — F1 availability
Prove App Service Linux F1 is available to the actual subscription and selected region.

Evidence:
- command/API/portal output;
- selected SKU;
- region;
- resource inventory.

Emit:
`AZURE F1 WP02 TEST 1 — F1 AVAILABILITY: PASS`

## Test 2 — Custom Linux Docker execution
Deploy the minimal custom Linux image to App Service F1.

Prove:
- app reaches Running/healthy state;
- expected probe response is returned;
- image version/digest is recorded.

Emit:
`AZURE F1 WP02 TEST 2 — CUSTOM DOCKER EXECUTION: PASS`

## Test 3 — Public HTTPS
Prove the application is reachable via its App Service HTTPS endpoint.

Record:
- HTTPS status;
- expected response;
- TLS/public-host behavior sufficient for a reference deployment probe.

Do not require a custom domain.

Emit:
`AZURE F1 WP02 TEST 3 — PUBLIC HTTPS: PASS`

## Test 4 — Persistent App Service storage enablement
Set and verify the documented persistent-storage setting for Linux custom containers, including:

`WEBSITES_ENABLE_APP_SERVICE_STORAGE=true`

Prove:
- the setting is effective;
- `/home` is writable from inside the running container.

Write only probe-specific marker data.

Emit:
`AZURE F1 WP02 TEST 4 — PERSISTENT /home WRITABLE: PASS`

## Test 5 — App restart persistence
Write a unique marker under `/home`.

Restart the App Service application using an approved restart action.

After restart, prove:
- the container returns;
- the marker remains;
- its exact content/hash remains intact.

Emit:
`AZURE F1 WP02 TEST 5 — APP RESTART PERSISTENCE: PASS`

## Test 6 — Container restart/recycle persistence
Trigger a container restart/recycle mechanism distinct enough from the prior simple app restart to test the hosting boundary, using the safest supported method available.

After recycle, prove:
- the container returns;
- `/home` marker persists unchanged.

Document the exact mechanism used and why it constitutes a recycle/container restart test.

Emit:
`AZURE F1 WP02 TEST 6 — CONTAINER RECYCLE PERSISTENCE: PASS`

## Test 7 — Image redeployment persistence
Build/publish a second distinguishable probe image version without changing the persisted marker.

Redeploy the app to the new image version.

Prove:
- the new image is running;
- persisted `/home` marker survives unchanged.

Do not infer persistence merely from the same image restarting.

Emit:
`AZURE F1 WP02 TEST 7 — IMAGE REDEPLOYMENT PERSISTENCE: PASS`

## Test 8 — Resource inventory truth
Enumerate all resources created or mutated by WP02.

Verify:
- no hidden paid supporting resource;
- no Azure Files;
- no paid ACR;
- no unintended paid logging/monitoring resource;
- registry usage remains within the approved zero-cost path.

Emit:
`AZURE F1 WP02 TEST 8 — RESOURCE INVENTORY: PASS`

# Storage-test boundary
WP02 tests persistence of ordinary probe files under `/home`.

WP02 must NOT claim:
- SQLite concurrency correctness;
- SQLite journal-mode suitability;
- WAL suitability;
- database recovery correctness;
- production DB persistence acceptance.

Those belong to WP03.

A tiny non-database marker/file is sufficient for WP02 persistence proof.

# Security and evidence rules
Do not commit:
- Azure credentials;
- access tokens;
- registry tokens;
- Twelve Data API keys;
- subscription secrets;
- publish profiles;
- environment dumps containing secrets.

Sanitize:
- subscription identifiers where not needed;
- tenant identifiers;
- principal IDs;
- registry credentials;
- signed URLs;
- cookies/auth headers.

Evidence should remain sufficiently precise to reproduce and audit the experiment.

Emit:
`AZURE F1 WP02 EVIDENCE REDACTION: PASS`

# Failure semantics
WP02 may end in one of three states:

## PASS
All mandatory tests pass.

## NOT FEASIBLE
A real platform constraint falsifies the candidate, for example:
- F1 unavailable;
- custom container cannot run on F1;
- persistent `/home` cannot satisfy required persistence semantics;
- strict-$0 cannot be maintained.

A NOT FEASIBLE result is valid and must not trigger paid fallback or architecture compromise.

## BLOCKED
Execution cannot reach a truthful conclusion because of:
- missing Azure authorization/credentials;
- ambiguous account/subscription context;
- external outage;
- missing registry access;
- predecessor gate not satisfied;
- evidence cannot be safely captured.

Do not classify operator-access failure as platform NOT FEASIBLE.

# Repository validation if probe files were changed
If repository-content mutation occurred:
- inspect exact diff;
- prove all paths are within WP02 allowlist;
- run applicable lint/build/tests for the probe only;
- verify no production source/test behavior changed;
- verify no package/schema migration occurred;
- run Gitleaks on staged/final candidate as appropriate;
- verify no secrets in diff.

Do not run unrelated invasive test suites unless required by existing governance.

Emit:
`AZURE F1 WP02 REPOSITORY VALIDATION: PASS`

If no repository-content mutation occurred, emit:
`AZURE F1 WP02 REPOSITORY VALIDATION: NOT APPLICABLE — ZERO REPOSITORY MUTATION`

# Cleanup/retention contract
Default: temporary resources must be cleaned up after evidence capture unless the planning contract or an explicit later authority authorizes retention for WP03.

Because WP03 is the immediate successor and may require the same App Service environment, Terra may retain the minimum zero-cost resources only if:
- retention is explicitly permitted by the WP01 resource plan;
- all retained resources remain `$0.00`;
- every retained resource is inventoried;
- ownership and cleanup deadline/trigger are recorded;
- no unnecessary resources remain.

If retention is not explicitly authorized, clean up all WP02-created Azure resources.

After cleanup or authorized retention, prove final disposition.

Emit exactly one:
`AZURE F1 WP02 RESOURCE DISPOSITION: CLEANED UP`

or

`AZURE F1 WP02 RESOURCE DISPOSITION: RETAINED FOR WP03 UNDER EXPLICIT ZERO-COST AUTHORITY`

# GitHub lifecycle
If WP02 has a GitHub issue:
- do not close it before the exact acceptance marker;
- after PASS, close the issue;
- set Project #2 Status to Done if applicable;
- if closure automation already sets Done, do not redundantly mutate Status.

If no GitHub issue exists by design:
- record that lifecycle is governed by the initiative artifacts;
- do not create a false Release assignment.

On NOT FEASIBLE/BLOCKED:
- do not mark WP02 Done unless governance explicitly defines that terminal result as completed work;
- report exact lifecycle state.

# Acceptance gate
WP02 passes only if ALL are true:
- predecessor gate passed;
- non-release identity preserved;
- strict-zero-cost preflight passed;
- F1 availability empirically passed;
- custom Docker execution passed;
- public HTTPS passed;
- `/home` writable persistence passed;
- app restart persistence passed;
- container recycle persistence passed;
- image redeployment persistence passed;
- resource inventory passed;
- evidence redaction passed;
- repository validation passed or is N/A with zero repository mutation;
- final resource disposition is proven;
- no WP03/WP04/WP05/Phase B work was claimed;
- no production architecture change occurred.

Then emit:

`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`

# Mutation audit
Report exact counts for:
- repository-content edits;
- files created/modified/deleted;
- commits;
- pushes;
- PRs;
- GitHub issue mutations;
- Project mutations;
- milestone mutations;
- Release-option mutations;
- Azure resource-group creations/deletions;
- App Service plan creations/deletions;
- Web App creations/deletions;
- App settings mutations;
- restarts/recycles;
- image builds;
- registry pushes/tags;
- deployment mutations;
- external registry mutations;
- Azure paid-resource creations;
- live Twelve Data requests.

Count only actions actually performed.

Require:
`Azure paid-resource creations: 0`
`Live Twelve Data requests: 0`

Emit:
`AZURE F1 WP02 MUTATION AUDIT: PASS`

# Handoff
If PASS:
- WP03 becomes eligible;
- do not execute WP03 under this authority;
- produce exact next-authority handoff:

**GPT-5.6 Terra — Azure F1 WP03 Persistent SQLite Filesystem, Locking & Journal Qualification Authority**

The handoff must include:
- resource disposition;
- App Service/resource identifiers in safely redacted form;
- selected image version/digest;
- persistent-storage setting;
- `/home` probe result;
- exact region/SKU;
- zero-cost evidence;
- anything WP03 must not assume.

Emit:
`AZURE F1 WP02 → WP03 HANDOFF: PASS`

# Required PASS markers
`AZURE F1 WP02 PREDECESSOR GATE: PASS`
`AZURE F1 WP02 NON-RELEASE IDENTITY: PRESERVED`
`AZURE F1 WP02 REPOSITORY BASELINE: VERIFIED`
`AZURE F1 WP02 PLANNING CONTRACT: LOADED`
`AZURE F1 WP02 DOCUMENTATION REFRESH: COMPLETE`
`AZURE F1 WP02 ZERO-COST PREFLIGHT: PASS`
`AZURE F1 WP02 PROBE PATH ALLOWLIST: FROZEN`
`AZURE F1 WP02 TEST 1 — F1 AVAILABILITY: PASS`
`AZURE F1 WP02 TEST 2 — CUSTOM DOCKER EXECUTION: PASS`
`AZURE F1 WP02 TEST 3 — PUBLIC HTTPS: PASS`
`AZURE F1 WP02 TEST 4 — PERSISTENT /home WRITABLE: PASS`
`AZURE F1 WP02 TEST 5 — APP RESTART PERSISTENCE: PASS`
`AZURE F1 WP02 TEST 6 — CONTAINER RECYCLE PERSISTENCE: PASS`
`AZURE F1 WP02 TEST 7 — IMAGE REDEPLOYMENT PERSISTENCE: PASS`
`AZURE F1 WP02 TEST 8 — RESOURCE INVENTORY: PASS`
`AZURE F1 WP02 EVIDENCE REDACTION: PASS`
`AZURE F1 WP02 MUTATION AUDIT: PASS`
`AZURE F1 WP02 — APP SERVICE F1 EXECUTION PROBE: PASS`
`AZURE F1 WP02 → WP03 HANDOFF: PASS`
`GPT-5.6 MODEL MAP: LUNA=CONTRACT/PLANNING | TERRA=IMPLEMENTATION/EXECUTION | SOL=SUPPORTING ANALYSIS`

Plus exactly one resource-disposition marker.

# Exact success terminal
`AZURE F1 WP02 — MINIMAL DOCKER + APP SERVICE F1 EXECUTION PROBE AUTHORITY COMPLETE`

# NOT FEASIBLE terminal
If empirical platform evidence proves the candidate fails:

Emit:
`AZURE APP SERVICE F1 REFERENCE DEPLOYMENT: NOT FEASIBLE`

Do not attempt paid fallback.
Do not modify product architecture.
Clean up temporary resources unless explicit governance requires retaining evidence resources.

End exactly:
`AZURE F1 WP02 — MINIMAL DOCKER + APP SERVICE F1 EXECUTION PROBE AUTHORITY NOT FEASIBLE`

# BLOCK conditions
BLOCK before or during mutation if:
- WP01 acceptance cannot be proven;
- required Azure authorization is unavailable;
- subscription/region identity is ambiguous;
- strict-$0 preflight cannot be proven;
- only a paid SKU/resource would allow continuation;
- safe evidence capture is impossible;
- registry access cannot be established without paid infrastructure;
- planned probe requires production architecture change;
- probe file path would violate manifest;
- cleanup ownership is unresolved.

On BLOCK:
- perform no further mutation;
- clean up any safely removable temporary resources already created;
- report residual resources exactly;
- do not claim NOT FEASIBLE unless platform evidence actually proves it.

# Exact blocked terminal
`AZURE F1 WP02 — MINIMAL DOCKER + APP SERVICE F1 EXECUTION PROBE AUTHORITY BLOCKED`

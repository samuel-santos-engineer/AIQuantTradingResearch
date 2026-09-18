# GPT-5.6 Luna — Release 1.12 WP04/WP05 Dependency Regovenance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the proven WP04/WP05 dependency contradiction and redefine the minimum valid acceptance/sequencing boundary.
- **GPT-5.6 Terra** — execute only a later explicitly authorized planning mutation, qualification attempt, WP05 configuration action, or lifecycle mutation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12: Public Reference Deployment Implementation & Stabilization**

Canonical work packages:

```text
#260 WP01 Release Contract, Deployment Architecture & Reproducibility Boundary
#261 WP02 Productionized Container & Runtime Composition
#262 WP03 GHCR Publication & Azure F1 Deployment Automation
#263 WP04 Persistent SQLite Initialization, Data Update & Recovery
#264 WP05 Twelve Data Runtime Configuration, Secrets & Bounded Automation
#265 WP06 Public Streamlit/System Health Deployment & Truthful Diagnostics
#266 WP07 Deployment Stability, Recovery, Cost & No-Bypass Validation
#267 WP08 Documentation, Operational Runbook & Release Acceptance
```

Canonical dependency graph:

```text
WP01 → WP02 → WP03 → WP04 → WP05 → WP06 → WP07 → WP08
```

Current source commit:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Current deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Current WP04 issue:

```text
#263 = Open
```

WP05 issue:

```text
#264 = not yet canonically started
```

## 2. Proven contradiction

Fresh normal-mode diagnostics proved:

```text
aiq-entrypoint: required environment variable TwelveData__ApiKey is not set
Container has finished running with exit code: 64
Site container terminated during site startup
Site startup probe failed
```

Source/contract reconciliation proved:

```text
TwelveData__ApiKey is intentionally required in every non-qualification mode
normal execution binds TwelveData:ApiKey
Twelve Data runtime configuration/secrets are canonically owned by WP05
WP04 owns persistence
WP04 precedes WP05
```

Therefore:

```text
CURRENT WP04 NORMAL-RUNTIME GATE → REQUIRES DOWNSTREAM WP05 SECRET
```

Canonical classification:

```text
I2 — SECRET CONFIGURATION BELONGS EXCLUSIVELY TO WP05
D2 — RELEASE_DEPENDENCY_REGOVERNANCE_REQUIRED
```

## 3. Regovenance objective

Resolve the contradiction without:

```text
silently moving WP05 work into WP04
silently starting WP05 out of order
weakening secret ownership
inventing a fake Twelve Data key
changing entrypoint semantics without architectural reason
claiming normal-runtime health while the secret is absent
```

The selected solution must preserve a truthful release dependency model.

No mutation is authorized by this Luna authority.

## 4. WP04 core acceptance scope

Re-evaluate WP04 against its canonical mission:

```text
Persistent SQLite Initialization, Data Update & Recovery
```

Its acceptance must prove at minimum:

```text
persistent /home/data SQLite initialization
schema v4
DELETE journal mode
governed data update
evidence fidelity
idempotency/conflict semantics
restart/redeploy persistence
recovery/integrity
application-owned evidence
zero bypass
strict $0 target compatibility
```

Determine whether **general normal-mode Streamlit availability** is intrinsically part of WP04 acceptance, or was introduced only as a diagnostic/precondition while investigating the Azure qualification channel.

Classify:

```text
WP04_NORMAL_RUNTIME_HEALTH_IS_CORE_ACCEPTANCE =
YES | NO | PARTIAL
```

## 5. Qualification-mode independence

Current qualification mode is architecturally designed to:

```text
suppress Streamlit
let Worker own port 8501
perform application-owned SQLite qualification
serve only the governed evidence endpoint
exit after retrieval or bounded timeout
```

Determine from source whether qualification mode intentionally does **not** require `TwelveData__ApiKey`.

Classify:

```text
WP04_QUALIFICATION_INDEPENDENT_OF_TWELVE_DATA_SECRET =
PROVEN | NOT_PROVEN
```

If `PROVEN`, determine whether WP04 can truthfully complete its persistence acceptance before WP05 while normal public runtime remains unavailable solely because the downstream WP05 secret is not yet configured.

## 6. Ownership invariant

Binding invariant:

```text
TwelveData__ApiKey configuration ownership = WP05
```

No selected option may silently reassign it to WP04.

Any change to this invariant requires explicit release-plan amendment and a stated rationale.

## 7. Candidate governance models

Evaluate exactly these.

### G1 — WP04 ACCEPTANCE-BOUNDARY CORRECTION

Definition:

```text
WP04 acceptance is persistence-specific.
WP04 qualification mode may be used while TwelveData__ApiKey is absent.
Normal public runtime health dependent on Twelve Data configuration is deferred to WP05/WP06.
```

Under G1:

```text
WP04 may proceed to fresh initialize/reopen/redeploy persistence qualification
normal root 503 caused solely by absent WP05 secret is documented, not treated as WP04 failure
WP04 must not configure TwelveData__ApiKey
WP05 remains next after WP04
WP05 must restore normal-mode startup as part of its own acceptance
WP06 still owns public Streamlit/System Health deployment truth
```

Required condition:

```text
qualification mode is proven independent of TwelveData__ApiKey
```

This option preserves the canonical dependency graph.

### G2 — SPLIT WP04 ACCEPTANCE AROUND WP05

Definition:

```text
WP04 implementation/qualification subset
→ WP05 secret/runtime configuration
→ return to WP04 final acceptance
```

This creates an interleaved dependency:

```text
WP04-A → WP05 → WP04-B
```

Use only if normal-mode health is intrinsically required to accept WP04.

Consequences:

```text
canonical linear dependency graph must be amended
issue/lifecycle semantics must be explicitly defined
WP04 cannot close before WP05
```

### G3 — MOVE MINIMAL SECRET BOOTSTRAP INTO WP04

Definition:

```text
a narrow TwelveData__ApiKey configuration prerequisite is reassigned to WP04
```

This conflicts with current ownership and should be selected only if release planning proves the existing WP05 assignment was erroneous.

Requires explicit file-manifest/definition amendment.

### G4 — SOURCE CONTRACT CHANGE

Definition:

```text
normal mode no longer hard-requires TwelveData__ApiKey before WP05
```

Use only if product/runtime architecture permits a truthful provider-disabled normal mode.

Consequences may include:

```text
source remediation
new image
runtime behavior change
additional acceptance work
```

Do not select merely to avoid dependency re-governance.

## 8. Preferred governance principle

Prefer the option that:

```text
preserves canonical WP ownership
preserves the WP01→...→WP08 sequence
keeps WP04 acceptance scoped to persistence
keeps WP05 responsible for Twelve Data secrets/runtime
keeps WP06 responsible for public Streamlit/System Health
requires the fewest architectural mutations
```

Therefore evaluate **G1 first**, but select it only if supported by the release contract and source.

## 9. Normal-root 503 treatment under G1

If G1 is selected, classify the current root `503` precisely as:

```text
EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER
```

Not:

```text
WP04 persistence failure
Azure routing failure
image startup defect
Streamlit defect
```

This classification is valid only because fresh diagnostics proved the missing WP05-owned secret is the startup boundary.

Required truthful statement:

```text
Normal public runtime is not yet expected to be healthy until WP05 configures the required Twelve Data secret.
```

## 10. Qualification retry gate under G1

If G1 is selected, Luna must decide whether the prior blanket gate:

```text
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

may now be replaced with:

```text
FRESH WP04 QUALIFICATION RETRY = ELIGIBLE_FOR_SEPARATE_TERRA_AUTHORITY
```

Only if all are true:

```text
qualification settings currently absent before mutation
wrapper provenance gate passes
exact image digest matches
WEBSITES_PORT = 8501
qualification mode is independent of TwelveData__ApiKey
normal root 503 is fully explained by absent WP05-owned secret
no other unresolved WP04 runtime blocker remains
```

This Luna authority must not itself authorize the actual Azure qualification attempt unless the output explicitly selects G1 and marks a separate Terra qualification authority as READY.

## 11. Initialize/reopen/redeploy acceptance sequence

If G1 is selected, define the remaining WP04 acceptance order:

```text
1. fresh initialize qualification with new RunId
2. exact D3 initialize evidence
3. normal settings restoration
4. governed persistence continuity step
5. fresh reopen/redeploy qualification with distinct RunId
6. exact D3 continuity evidence
7. integrity/schema/journal/idempotency/conflict acceptance
8. final WP04 acceptance
9. PR/merge
10. #263 Closed / Project #2 Done
11. WP05 begins
```

Do not close WP04 before exact acceptance marker.

## 12. WP05 acceptance obligation under G1

If G1 is selected, explicitly assign to WP05:

```text
configure TwelveData__ApiKey through secret-safe Azure mechanism
prove secret presence without disclosure
prove normal entrypoint passes
prove Worker normal startup
prove Streamlit normal startup/listen
prove normal public root recovers from 503
prove bounded Twelve Data connectivity/automation
preserve $0 constraint
```

WP05 must not claim the earlier 503 as an unexplained platform failure.

## 13. WP06 boundary

Preserve WP06 ownership:

```text
Public Streamlit/System Health Deployment & Truthful Diagnostics
```

WP05 may prove normal startup/connectivity required for its configuration acceptance, but full public presentation/System Health truth remains WP06.

## 14. Planning artifact impact

Determine whether the selected governance model requires tracked planning-document changes.

Classify:

```text
PLANNING_DOC_MUTATION_REQUIRED = YES | NO
```

If G1 merely clarifies an already intended boundary and existing docs support it, mutation may be unnecessary.

If the existing Release 1.12 definition/execution plan explicitly requires normal public runtime health in WP04, then a tracked planning correction is required before qualification.

Do not silently contradict committed planning docs.

## 15. GitHub issue/dependency impact

Determine whether the selected model requires:

```text
issue #263 body update
issue #264 body update
Project dependency metadata update
milestone mutation
```

Classify each separately:

```text
REQUIRED | NOT_REQUIRED
```

No GitHub mutation is authorized here.

## 16. Decision options

Select exactly one:

```text
D1 — G1_WP04_ACCEPTANCE_BOUNDARY_CORRECTION
D2 — G2_INTERLEAVED_WP04_WP05_SEQUENCE
D3 — G3_SECRET_BOOTSTRAP_OWNERSHIP_TRANSFER
D4 — G4_SOURCE_CONTRACT_CHANGE
D5 — INSUFFICIENT_EVIDENCE_FOR_REGOVERNANCE
```

## 17. Decision consequences

### If D1

Next authority is one of:

```text
Terra planning clarification publication authority
```

if tracked planning changes are required, otherwise:

```text
Terra fresh WP04 Azure qualification authority
```

No WP05 work starts yet.

### If D2

Next authority:

```text
Luna release-plan dependency amendment authority
```

No Azure mutation until amended sequencing is published.

### If D3

Next authority:

```text
Luna release-plan ownership amendment authority
```

No secret configuration until published.

### If D4

Next authority:

```text
Terra exact-path source remediation authority
```

Likely new image required.

### If D5

Return blocked with exact missing evidence.

## 18. Secret hygiene

Throughout:

```text
never print TwelveData__ApiKey
never commit TwelveData__ApiKey
never embed it in prompt artifacts
never use a fake value unless separately governed
```

## 19. Current mutation boundary

Under this Luna authority:

```text
repository edits = 0
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
Azure settings mutations = 0
Azure restarts = 0
qualification attempts = 0
secret configuration = 0
PR mutations = 0
issue mutations = 0
Project mutations = 0
milestone mutations = 0
```

## 20. Failed RunId preservation

Never reuse:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

A future qualification attempt must generate a fresh RunId.

## 21. Required output

Return:

- whether WP04 normal-runtime health is core acceptance;
- whether qualification mode is independent of TwelveData secret;
- exact ownership invariant;
- G1/G2/G3/G4 evaluation;
- selected D1–D5;
- planning-document mutation requirement;
- GitHub metadata mutation requirement;
- exact treatment of current root 503;
- whether fresh WP04 qualification becomes eligible for a separate Terra authority;
- remaining WP04 acceptance sequence;
- WP05 downstream obligations;
- whether new source commit is required;
- whether new image is required;
- zero-mutation audit.

## 22. Terminal markers

Required:

`RELEASE 1.12 WP04/WP05 — DEPENDENCY REGOVERNANCE: PASS`

`RELEASE 1.12 WP04 — TWELVE DATA CONFIG OWNERSHIP: WP05`

`RELEASE 1.12 WP04 — PROVEN NORMAL-MODE BLOCKER: TwelveData__ApiKey_ABSENT`

`RELEASE 1.12 WP04 — WP04 NORMAL-RUNTIME HEALTH IS CORE ACCEPTANCE: <YES|NO|PARTIAL>`

`RELEASE 1.12 WP04 — QUALIFICATION INDEPENDENT OF TWELVE DATA SECRET: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04/WP05 — SELECTED GOVERNANCE MODEL: <G1|G2|G3|G4|NONE>`

`RELEASE 1.12 WP04/WP05 — DEPENDENCY REGOVERNANCE DECISION: <D1|D2|D3|D4|D5>`

`RELEASE 1.12 WP04 — NORMAL FRONT-DOOR 503 CLASSIFICATION: <EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER|WP04_BLOCKER|NOT_PROVEN>`

`RELEASE 1.12 WP04 — PLANNING DOC MUTATION REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — ISSUE #263 UPDATE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP05 — ISSUE #264 UPDATE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: <ELIGIBLE_FOR_SEPARATE_TERRA_AUTHORITY|NOT_AUTHORIZED>`

`RELEASE 1.12 WP04/WP05 — DEPENDENCY REGOVERNANCE MUTATION AUDIT: PASS`

Then exactly one matching next-authority marker:

`RELEASE 1.12 WP04 — TERRA PLANNING CLARIFICATION PUBLICATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA FRESH AZURE QUALIFICATION AUTHORITY: READY`

or

`RELEASE 1.12 — LUNA INTERLEAVED WP04/WP05 DEPENDENCY AMENDMENT AUTHORITY: READY`

or

`RELEASE 1.12 — LUNA SECRET OWNERSHIP AMENDMENT AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA SOURCE CONTRACT REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04/WP05 — REGOVERNANCE BLOCKED`

Final:

`RELEASE 1.12 WP04/WP05 — LUNA DEPENDENCY REGOVERNANCE COMPLETE`

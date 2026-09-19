# Release 1.12 WP07 --- Terra Resume: Deployment Stability, Recovery, Cost & No-Bypass Validation

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract/policy/architecture/governance/reconciliation/final substantive acceptance
GPT-5.6 Terra = selected validation execution, bounded Azure mutation, evidence, and in-scope corrective implementation authority
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Binding Luna schema reconciliation

The prior WP07 attempt stopped before mutation because its authority
incorrectly required schema v3.

Luna has now established:

``` text
WP04AcceptedSchemaResult=PASS
WP04AcceptedSchemaVersion=4
WP07SchemaV3RequirementResult=CONTRADICTS_ACCEPTED_PREDECESSOR_STATE
WP07ExpectedPersistenceSchemaVersion=4
SchemaMigrationRequired=false
SchemaMigrationAuthorized=false
WP04VerifierReuseResult=AUTHORIZED
WP04HistoricalAcceptancePreservationResult=PASS
WP07DeploymentContinuityOwnershipResult=PRESERVED
ArchitectureBoundaryResult=PASS
PlanningAmendmentRequired=false
RoadmapArchitectureChange=false
TerraWP07ResumeAuthorized=true
```

Exact reconciliation markers:

``` text
RELEASE 1.12 WP07 — PERSISTENCE SCHEMA RECONCILIATION: PASS
RELEASE 1.12 WP07 — ACCEPTED SQLITE SCHEMA V4: PASS
RELEASE 1.12 WP07 — SCHEMA MIGRATION: NOT_REQUIRED
RELEASE 1.12 WP07 — WP04 VERIFIER REUSE: AUTHORIZED
RELEASE 1.12 WP07 — DEPLOYMENT CONTINUITY OWNERSHIP: PRESERVED
RELEASE 1.12 WP07 — ARCHITECTURE BOUNDARY: PASS
```

## Supersession

For this WP07 execution, every prior WP07 authority reference to:

``` text
schema v3
```

is superseded by:

``` text
SQLite persistence schema v4
SchemaVersion == 4
```

No schema downgrade/migration/rewrite/compatibility shim is authorized.

All other valid requirements of the prior WP07 Terra authority remain
binding.

## Work package

``` text
Release=1.12
WP=07
Issue=#266
Title=Deployment Stability, Recovery, Cost & No-Bypass Validation
Milestone=#63
```

Dependencies WP01--WP06 are lifecycle-complete.

Canonical accepted runtime target:

``` text
Region=West US 2
ResourceGroup=rg-aiq-r112-wp05-wus2-7f5eabb5
WebApp=aiqr112wp05wus27f5eabb5
ExpectedImageDigest=sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
```

## Prior blocked attempt

The blocked attempt consumed no runtime/evidence identity and performed
no mutation:

``` text
AzureMutationCount=0
ExplicitRestartCount=0
RecycleScenarioMutationCount=0
ImageDeploymentMutationCount=0
AzureAppSettingMutationCount=0
RunIdAllocationCount=0
SourceMutationCount=0
GitMutationCount=0
SecretDisclosureCount=0
```

Therefore resume directly; no cleanup cycle is required.

## Mission

Resume and converge the full WP07 contract in one Terra authority.

Validate deployment stability, restart/recycle/redeploy recovery,
persistent SQLite continuity, cost, no-bypass invariants, System Health
truth preservation, secret hygiene, and predecessor acceptance
preservation.

Use SQLite `SchemaVersion == 4`.

## WP04 verifier reuse

The accepted WP04 verifier is authorized for fresh WP07 evidence of:

``` text
/home persistence
SchemaVersion == 4
SQLite journal_mode == delete
integrity_check == ok
quick_check == ok
fresh continuity record/readback
```

Reuse of the verifier does not convert WP04 historical evidence into
WP07 acceptance.

WP07 must execute fresh deployment-level continuity validation around
its owned scenarios.

Any verifier execution requiring an identity must use a fresh
WP07-specific RunId. Never reuse a consumed historical RunId.

## Scenario execution

Derive the exact required scenarios from issue #266 and tracked Release
1.12 planning. Evaluate all required gates.

Unless tracked planning narrows them, validate:

``` text
S1 = steady-state baseline
S2 = explicit App Service restart
S3 = platform/container recycle or accepted equivalent bounded restart condition
S4 = redeploy SAME accepted immutable digest to SAME West US 2 target
```

Before each mutating scenario capture authoritative pre-state.

After each scenario prove: - App Service returns to Running/Normal; -
public HTTPS/Streamlit recovers; - configured/observed image identity is
correct; - `/home` persistence continuity survives; -
`SchemaVersion == 4`; - journal mode DELETE; - integrity/quick check
OK; - fresh pre-event continuity record remains readable; - no
unintended reinitialization/data loss; - System Health remains
truthful; - no secret disclosure; - \$0/F1 architecture remains
intact; - no architecture bypass occurred.

Do not invent an SLA threshold. Record observed recovery timing against
tracked requirements.

## Image/no-bypass invariant

Expected image remains:

``` text
sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
```

A same-digest redeploy must deploy that exact immutable digest.

Do not rebuild or publish a new image unless a genuine correctable WP07
source defect requires byte changes.

If bytes change, invalidate prior candidate evidence, perform full local
validation/publication, obtain a new immutable digest, and restart the
complete affected WP07 cycle.

## Cost boundary

Require:

``` text
ACTUAL_RECURRING_INFRASTRUCTURE_COST_TARGET=$0.00
```

Verify F1/free architecture and absence of newly introduced paid
companion resources.

No automatic paid upgrade or paid recovery service is authorized.

## Secret boundary

Never retrieve, print, copy, or persist the value of
`TwelveData__ApiKey`.

Only presence/non-empty metadata may be used.

## Bounded convergence

Do not return after each ordinary correctable defect.

For all defects inside this authority:

``` text
evaluate ALL gates
→ collect complete failure matrix
→ fix all in-scope defects
→ fresh hashes/digest if bytes changed
→ restart complete affected validation cycle
→ repeat until PASS
```

Stop only at a genuine governance/security/cost/architecture/user-input
boundary.

## PowerShell

All PowerShell remains compatible with:

``` text
Windows PowerShell 5.1.26100.9444
```

## Lifecycle boundary

Do not close #266.

Do not set WP07 Project Status to Done.

Do not close milestone #63.

Those mutations require Luna substantive acceptance first.

## Required output

Return the full output/mutation/scenario matrices required by the
original WP07 Terra authority, plus:

``` text
SchemaReconciliationApplied=true
WP04AcceptedSchemaVersion=4
WP07ExpectedPersistenceSchemaVersion=4
SchemaMigrationPerformed=false
WP04VerifierReuseResult
FreshWP07RunIds
```

## Exact PASS gate

Only after every tracked WP07 gate passes:

``` text
RELEASE 1.12 WP07 — PERSISTENCE SCHEMA RECONCILIATION: PASS
RELEASE 1.12 WP07 — ACCEPTED SQLITE SCHEMA V4: PASS
RELEASE 1.12 WP07 — DEPLOYMENT STABILITY: PASS
RELEASE 1.12 WP07 — RESTART CONTINUITY: PASS
RELEASE 1.12 WP07 — RECYCLE CONTINUITY: PASS
RELEASE 1.12 WP07 — REDEPLOY CONTINUITY: PASS
RELEASE 1.12 WP07 — PERSISTENT SQLITE CONTINUITY: PASS
RELEASE 1.12 WP07 — RECOVERY VALIDATION: PASS
RELEASE 1.12 WP07 — COST VALIDATION: PASS
RELEASE 1.12 WP07 — NO-BYPASS VALIDATION: PASS
RELEASE 1.12 WP07 — SYSTEM HEALTH TRUTH PRESERVATION: PASS
RELEASE 1.12 WP07 — SECRET HYGIENE: PASS
RELEASE 1.12 WP07 — HISTORICAL ACCEPTANCE PRESERVATION: PASS
RELEASE 1.12 WP07 — $0 ARCHITECTURE BOUNDARY: PASS

WP04AcceptedSchemaVersion=4
WP07ExpectedPersistenceSchemaVersion=4
SchemaMigrationPerformed=false
WP07ValidationResult=PASS
WP07Result=READY_FOR_LUNA_ACCEPTANCE
BoundaryBlocker=NONE
NextAuthorizedAction=GPT-5.6 Luna performs final read-only WP07 substantive acceptance reconciliation
```

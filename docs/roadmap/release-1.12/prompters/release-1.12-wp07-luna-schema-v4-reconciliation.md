# Release 1.12 WP07 --- Luna Schema Reconciliation: Adopt Accepted SQLite Schema v4

**Selected execution model: GPT-5.6 Luna**

## Authority roles

``` text
GPT-5.6 Luna = selected contract/policy/architecture/governance reconciliation authority
GPT-5.6 Terra = implementation/validation/runtime mutation authority after reconciliation
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Mode

STRICTLY READ-ONLY. No Azure mutation, restart, recycle, redeploy, App
Setting change, RunId allocation, source/schema migration, Git/GitHub
mutation, Docker/GHCR mutation, lifecycle mutation, or secret retrieval.

## Blocking ambiguity

The WP07 authority says `schema v3`, but accepted WP04 persistence
evidence/verifier requires `SchemaVersion == 4`, and the tracked WP04
execution contract defines SQLite schema v4.

Terra correctly stopped before mutation.

## User-controlled contract decision

``` text
WP07_EXPECTED_PERSISTENCE_SCHEMA=4
USE_CURRENT_ACCEPTED_WP04_SCHEMA=true
SCHEMA_V3_MIGRATION_INTENDED=false
NEW_SCHEMA_MIGRATION_AUTHORIZED=false
```

Luna must reconcile this decision into WP07.

## Required reconciliation

Inspect tracked Release 1.12 planning, issue #266, and accepted WP04
contract/evidence/verifier. Confirm:

1.  WP04 established schema v4 as the current accepted persistence
    schema.
2.  WP07 is deployment stability/recovery validation, not schema
    migration.
3.  Requiring schema v3 would contradict accepted predecessor state.
4.  WP07 must validate preservation of schema v4 across its
    restart/recycle/redeploy scenarios.
5.  No downgrade, migration, rewrite, or compatibility shim is
    authorized or required.
6.  The accepted WP04 verifier may be reused to prove `/home`
    persistence, `SchemaVersion == 4`, SQLite DELETE journal mode,
    integrity, quick check, and fresh continuity evidence, subject to
    WP07 deployment-level scenario requirements.
7.  WP04 application-level acceptance remains historical; WP07 must
    independently prove deployment-level continuity.
8.  All other WP07 architecture, cost, secret, and no-bypass
    requirements remain unchanged.

## Canonical correction

For WP07 persistence validation, supersede the erroneous phrase:

``` text
schema v3
```

with:

``` text
SQLite persistence schema v4
SchemaVersion == 4
```

This reconciles WP07 to already accepted predecessor state. It does
**not** authorize a schema migration.

## RunId governance

No RunId was allocated during the blocked attempt. On resume, allocate
fresh WP07-specific RunIds for verifier executions as required. Never
reuse consumed historical RunIds.

## Blocked-attempt mutation state

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

No candidate/evidence cycle was consumed.

## Required output

``` text
SelectedModel
WP04AcceptedSchemaResult
WP04AcceptedSchemaVersion
WP07SchemaV3RequirementResult
WP07ExpectedPersistenceSchemaVersion
SchemaMigrationRequired
SchemaMigrationAuthorized
WP04VerifierReuseResult
WP04HistoricalAcceptancePreservationResult
WP07DeploymentContinuityOwnershipResult
ArchitectureBoundaryResult
PlanningAmendmentRequired
RoadmapArchitectureChange
TerraWP07ResumeAuthorized
NextAuthorizedAction
```

## Exact PASS gate

``` text
RELEASE 1.12 WP07 — PERSISTENCE SCHEMA RECONCILIATION: PASS
RELEASE 1.12 WP07 — ACCEPTED SQLITE SCHEMA V4: PASS
RELEASE 1.12 WP07 — SCHEMA MIGRATION: NOT_REQUIRED
RELEASE 1.12 WP07 — WP04 VERIFIER REUSE: AUTHORIZED
RELEASE 1.12 WP07 — DEPLOYMENT CONTINUITY OWNERSHIP: PRESERVED
RELEASE 1.12 WP07 — ARCHITECTURE BOUNDARY: PASS

WP04AcceptedSchemaVersion=4
WP07ExpectedPersistenceSchemaVersion=4
SchemaMigrationRequired=false
SchemaMigrationAuthorized=false
PlanningAmendmentRequired=false
RoadmapArchitectureChange=false
TerraWP07ResumeAuthorized=true
NextAuthorizedAction=GPT-5.6 Terra resumes WP07 Deployment Stability, Recovery, Cost & No-Bypass Validation using SQLite SchemaVersion == 4 and the accepted WP04 verifier
```

If authoritative tracked planning explicitly and intentionally requires
a schema-v3 migration despite accepted WP04 schema-v4 state, return the
exact conflict and keep Terra blocked for separate
architecture/schema-change authority. Otherwise PASS this reconciliation
and authorize Terra to resume under schema v4.

# Release 1.12 WP08 --- Luna Documentation, Operational Runbook & Release Acceptance

**Selected execution model: GPT-5.6 Luna**

## Authority roles

``` text
GPT-5.6 Luna = selected read-only contract, documentation-gap, operational-runbook, governance, and release-acceptance authority
GPT-5.6 Terra = implementation, validation, publication, Git/GitHub/Azure mutations, and lifecycle authority after Luna authorization
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Mode

STRICTLY READ-ONLY.

Do not edit files, stage/commit/push, create/merge PRs, mutate GitHub
issues/Project/milestone, mutate Azure, restart/redeploy, build/publish
images, allocate RunIds, or access secret values.

## Work package

``` text
Release=1.12
WP=08
Issue=#267
Title=Documentation, Operational Runbook & Release Acceptance
Milestone=#63
```

## Entry condition

Require and independently verify:

``` text
WP01ThroughWP07LifecycleComplete=true
Issue266=CLOSED
ProjectWP07Status=Done
Issue267=OPEN
Milestone63=OPEN
WP08DependencyResult=PASS
```

Current accepted technical anchor supplied by predecessor evidence:

``` text
origin/main=48ade16c26799553986b892facd7d24dfa9d6a73
AcceptedImageDigest=sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
Region=West US 2
Plan=Linux F1/Free
SQLiteSchemaVersion=4
SQLiteJournalMode=delete
RecurringInfrastructureCostTarget=$0.00
```

If `origin/main` advanced, inspect and classify the delta before relying
on the predecessor anchor.

## Mission

Perform the complete read-only WP08 contract/repository reconciliation
and determine exactly what remains to make Release 1.12 documentation,
operational runbook, and final release acceptance truthful and complete.

This authority must: 1. derive the exact WP08 acceptance contract from
tracked Release 1.12 planning and issue #267; 2. reconcile all existing
documentation/runbook/README material against the accepted WP01--WP07
state; 3. identify every missing, stale, contradictory, or unsupported
statement; 4. determine the minimal Terra documentation
implementation/validation scope; 5. determine whether any
non-documentation technical work is actually required; 6. preserve all
historical acceptance evidence and architecture boundaries; 7. produce
an exhaustive acceptance/gap matrix, not first-failure output.

Do not perform lifecycle completion or release publication in Luna.

## Binding accepted release state

Reconcile documentation against the accepted implementation, including
at minimum:

``` text
Azure App Service Linux F1
West US 2 current accepted deployment target
custom Docker
public/default HTTPS/DNS
persistent /home
SQLite schema v4
SQLite journal_mode=delete
public/free GHCR
accepted immutable image digest
bounded Twelve Data configuration
TwelveData__ApiKey presence/non-empty without disclosure
truthful Streamlit/System Health diagnostics
deployment restart/recycle/same-digest redeploy continuity
$0.00 recurring-infrastructure architecture target
no production SLA/HA claim
no paid fallback
no architecture bypass
```

Preserve Initiative-1.11 as feasibility history. Do not relabel it as
Product Release 1.11.

## Project-wide quota recovery governance

Require documentation to remain compatible with the active
constrained-infrastructure recovery policy and Release 1.12
deterministic regional order:

``` text
West Central US
→ West US 2
→ Central US
→ South Central India
```

Current accepted deployment is West US 2. Historical West Central US
evidence must remain historical, not rewritten as if it never existed.

Do not imply automatic paid upgrade, unbounded resource creation, secret
copying between targets, production HA, or WP07 acceptance merely from
failover.

## README information-preservation policy --- binding

The front-door README policy is ACTIVE:

``` text
EXISTING README INFORMATION → PRESERVE
PRESERVE INFORMATION; UPDATE STATUS
SUMMARY ≠ AUTHORITY TO DELETE DETAIL
README PRESERVATION — ZERO UNAUTHORIZED INFORMATION LOSS: PASS
```

If WP08 requires README changes: - perform a semantic inventory of
existing substantive information; - identify only
additive/corrective/status updates; - do not authorize
deletion/compression of substantive detail without explicit user
approval; - require Terra post-edit semantic reconciliation proving zero
unauthorized information loss.

Luna must explicitly report whether README changes are required and the
exact information-preservation constraints.

## Operational runbook contract

Derive exact tracked requirements and reconcile whether existing docs
adequately explain, without exposing secrets: - deployment
prerequisites; - Azure F1/Free constraints; - current accepted target
and deterministic quota-recovery topology; - GHCR immutable image
usage; - required App Service settings by name only; -
`TwelveData__ApiKey` safe configuration expectations without value
disclosure; - persistent `/home/data/aiquant.db`; - SQLite schema v4 and
DELETE journal mode; - startup/initialization expectations; - public
root and Streamlit health verification; - System Health truth/provenance
semantics; - restart/recycle/redeploy recovery verification; -
persistence verification/recovery procedure; - bounded Twelve Data
behavior; - cost verification and `$0.00` target boundaries; - failure
diagnosis and escalation boundaries; - quota-exhaustion governance
boundary; - rollback/recovery using accepted immutable artifacts; -
explicit non-production/non-SLA limitations.

Do not invent operational procedures unsupported by tracked
implementation/evidence. Mark unsupported requirements as gaps for
Terra/documentation work.

## Release acceptance documentation

Determine whether Release 1.12 documentation can truthfully state
completion of WP01--WP07 and what WP08 must add/update before final
acceptance.

Require exact separation between:

``` text
documented accepted facts
historical evidence
current deployment state
operational instructions
limitations/non-goals
future work
```

No claim of Release 1.12 publication/completion is allowed before WP08
implementation/validation, Luna final acceptance, Terra lifecycle
completion, and separately authorized release publication if the roadmap
requires it.

## No technical rework by default

WP08 is documentation/runbook/release acceptance.

If all accepted technical state is already sufficient, set:

``` text
TechnicalImplementationRequired=false
ImageRebuildRequired=false
AzureRedeployRequired=false
SchemaMigrationRequired=false
```

Only report technical implementation as required if tracked WP08
acceptance exposes a real unproven technical defect. Do not create
technical work merely to refresh evidence.

## PowerShell compatibility

Any runbook commands/scripts must target:

``` text
Windows PowerShell 5.1.26100.9444
```

Do not require PowerShell 7-only syntax or APIs unless separately proven
compatible and explicitly authorized.

## Secret/security boundary

Documentation and proposed evidence must never disclose: - Twelve Data
API key values; - tokens; - credentials; - connection secrets.

Configuration names and presence/non-empty checks are allowed.

## Documentation source-of-truth reconciliation

Inspect all relevant tracked Release 1.12 planning, architecture,
deployment, operational, README, and prompter/evidence documents.

Identify:

``` text
DocumentsAlreadySufficient
DocumentsRequiringUpdate
DocumentsRequiringCreation
Contradictions
StaleStatements
UnsupportedClaims
READMEChangeRequired
```

For every required change, provide: - exact path; - reason; -
authoritative accepted fact it must reflect; - whether
additive/corrective/status-only; - validation required after Terra
edits.

## Exhaustive gate matrix

Evaluate all WP08 requirements. Classify:

``` text
PASS
FAIL
NOT_PROVEN
NOT_APPLICABLE
IMPLEMENTATION_REQUIRED
```

Do not stop after the first gap.

This Luna authority is expected to produce either:

``` text
WP08DefinitionResult=PASS
TerraWP08ImplementationAuthorized=true
```

with a complete bounded implementation plan,

or:

``` text
WP08DefinitionResult=NOT_READY
TerraWP08ImplementationAuthorized=false
```

only when a genuine architecture/policy/user-decision boundary prevents
a safe implementation authority.

Ordinary missing/stale documentation is not a Luna blocker; classify it
as `IMPLEMENTATION_REQUIRED` and authorize Terra to converge it.

## Required output

``` text
SelectedModel
EntryHEAD
EntryOriginMain
OriginMainPlanningAnchor
MainAdvanceClassification

Issue267State
ProjectWP08Status
Milestone63State
WP01ThroughWP07LifecycleComplete
WP08DependencyResult

WP08IssueContractResult
WP08PlanningContractResult
AcceptedTechnicalStateReconciliationResult
ArchitectureBoundaryResult
QuotaRecoveryGovernanceResult
READMEPreservationPolicyResult
PowerShell51CompatibilityResult
SecretHygieneContractResult

OperationalRunbookResult
ReleaseAcceptanceDocumentationResult
CurrentDeploymentDocumentationResult
PersistenceDocumentationResult
SystemHealthDocumentationResult
RecoveryDocumentationResult
CostDocumentationResult
LimitationsDocumentationResult

DocumentsAlreadySufficient
DocumentsRequiringUpdate
DocumentsRequiringCreation
Contradictions
StaleStatements
UnsupportedClaims
READMEChangeRequired
READMEAuthorizedChangeType

TechnicalImplementationRequired
ImageRebuildRequired
AzureRedeployRequired
SchemaMigrationRequired

AcceptanceGateCount
AcceptancePassCount
AcceptanceFailCount
AcceptanceNotProvenCount
ImplementationRequiredCount
DefectCount
Defects

TerraAuthorizedPaths
TerraRequiredValidation
TerraProhibitedMutations

WP08DefinitionResult
TerraWP08ImplementationAuthorized
NextAuthorizedAction
```

## Required decision semantics

If gaps are documentation-only and bounded:

``` text
TechnicalImplementationRequired=false
ImageRebuildRequired=false
AzureRedeployRequired=false
SchemaMigrationRequired=false
WP08DefinitionResult=PASS
TerraWP08ImplementationAuthorized=true
NextAuthorizedAction=GPT-5.6 Terra implements and validates the bounded WP08 documentation/runbook/release-acceptance changes, preserving README information and all accepted WP01-WP07 technical state
```

If no tracked documentation changes are required, still return a
complete matrix and explicitly state whether Terra needs
validation/publication evidence only.

If a genuine architecture/policy contradiction exists, return
`NOT_READY`, identify the exact boundary, and do not authorize Terra
across it.

## Lifecycle boundary

Do not close issue #267. Do not set Project #2 WP08 to Done. Do not
close milestone #63. Do not tag/publish Release 1.12. Do not begin
Release 2.0.

Those require subsequent authorities after WP08 substantive acceptance.

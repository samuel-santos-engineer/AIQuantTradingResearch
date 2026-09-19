# Release 1.12 WP08 --- Terra Documentation / Runbook / Acceptance Implementation & Validation

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract/policy/architecture/governance/definition/final substantive acceptance
GPT-5.6 Terra = selected bounded documentation implementation, validation, Git/GitHub publication authority
GPT-5.6 Sol = supporting analysis/synthesis only; non-authoritative
```

## Binding Luna definition

``` text
WP08DefinitionResult=PASS
TerraWP08ImplementationAuthorized=true

TechnicalImplementationRequired=false
ImageRebuildRequired=false
AzureRedeployRequired=false
SchemaMigrationRequired=false

READMEChangeRequired=true
READMEAuthorizedChangeType=ADDITIVE/CORRECTIVE/STATUS_ONLY

AcceptanceGateCount=23
AcceptancePassCount=15
AcceptanceFailCount=0
AcceptanceNotProvenCount=0
ImplementationRequiredCount=8
DefectCount=0
```

WP08 is documentation-only. Do not create technical work.

## Work package

``` text
Release=1.12
WP=08
Issue=#267
Title=Documentation, Operational Runbook & Release Acceptance
Milestone=#63
```

Entry governance:

``` text
Issue267=OPEN
ProjectWP08Status=Todo
Milestone63=OPEN
WP01ThroughWP07LifecycleComplete=true
```

Planning/implementation anchor:

``` text
origin/main=48ade16c26799553986b892facd7d24dfa9d6a73
```

Fetch/re-read current `origin/main` before editing. If main advanced,
inspect the delta. Continue only if the delta does not invalidate Luna's
WP08 definition; otherwise stop at the governance boundary.

## Authorized paths

Only these tracked documentation paths may change:

``` text
README.md
docs/guides/RELEASE_1.12_OPERATIONS_RUNBOOK.md
docs/roadmap/release-1.12/RELEASE_1.12_ACCEPTANCE.md
docs/project/ROADMAP.md
```

`docs/project/ROADMAP.md` may change only if its current-status section
is confirmed stale.

No other tracked path is authorized.

## Mission

Implement and converge all eight Luna-identified documentation
requirements in one Terra authority:

``` text
OperationalRunbookResult
ReleaseAcceptanceDocumentationResult
CurrentDeploymentDocumentationResult
PersistenceDocumentationResult
SystemHealthDocumentationResult
RecoveryDocumentationResult
CostDocumentationResult
LimitationsDocumentationResult
```

Do not return after each ordinary documentation defect. Inspect all
gates, implement all bounded corrections, validate exhaustively, and
repeat until PASS.

## README preservation --- mandatory

Before editing `README.md`, create a semantic inventory of all
substantive existing information.

Binding policy:

``` text
EXISTING README INFORMATION → PRESERVE
PRESERVE INFORMATION; UPDATE STATUS
SUMMARY ≠ AUTHORITY TO DELETE DETAIL
README PRESERVATION — ZERO UNAUTHORIZED INFORMATION LOSS: PASS
```

Authorized README changes are only:

``` text
ADDITIVE
CORRECTIVE
STATUS_ONLY
```

Required correction: - update stale Release 1.12 release
badge/current-status representation; - replace stale "planned
stabilization" semantics with truthful current status consistent with
completed WP01--WP07 and WP08 still in progress; - do not prematurely
claim WP08/release publication/lifecycle completion.

After edits, perform semantic reconciliation against the pre-edit
inventory and prove zero unauthorized information loss.

Do not delete/compress away substantive README detail.

## Create operations runbook

Create:

``` text
docs/guides/RELEASE_1.12_OPERATIONS_RUNBOOK.md
```

It must truthfully cover, as supported by tracked
implementation/evidence:

-   purpose and reference/demo/non-production boundary;
-   deployment prerequisites;
-   Azure App Service Linux F1/Free constraints;
-   current accepted deployment target: West US 2;
-   deterministic quota-recovery topology:
    `West Central US → West US 2 → Central US → South Central India`;
-   historical West Central US target remains historical evidence;
-   custom Docker and public/free GHCR;
-   accepted immutable image:
    `sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1`;
-   public/default HTTPS/DNS;
-   persistent `/home`;
-   database path `/home/data/aiquant.db`;
-   SQLite schema v4;
-   journal mode DELETE;
-   initialization/startup expectations;
-   App Service configuration names required by tracked implementation;
-   `TwelveData__ApiKey` configuration expectations by name/presence
    only;
-   never disclose/copy secret values;
-   bounded Twelve Data behavior;
-   provider deadline/default behavior where operationally relevant;
-   public root verification;
-   Streamlit health verification;
-   System Health truth/provenance semantics;
-   restart recovery;
-   bounded recycle recovery;
-   same-digest redeploy recovery;
-   persistence continuity verification;
-   integrity/quick-check expectations;
-   failure diagnosis;
-   quota/capacity attribution requirements;
-   deterministic regional recovery;
-   no secret copying between regional targets;
-   rollback/recovery using accepted immutable artifacts;
-   cost/SKU verification;
-   `$0.00` recurring-infrastructure architecture target and its limits;
-   no automatic paid upgrade;
-   no unbounded resource creation;
-   no production SLA/HA implication;
-   escalation/governance boundaries.

Commands must be compatible with:

``` text
Windows PowerShell 5.1.26100.9444
```

Do not invent commands/procedures unsupported by repository
implementation or accepted evidence.

## Create Release 1.12 acceptance document

Create:

``` text
docs/roadmap/release-1.12/RELEASE_1.12_ACCEPTANCE.md
```

Clearly separate: 1. accepted facts; 2. historical evidence; 3. current
deployment state; 4. operational verification references; 5.
limitations/non-goals; 6. remaining WP08 lifecycle/publication work; 7.
future work.

Record WP01--WP07 as accepted/lifecycle-complete.

Do not claim WP08 complete yet.

Do not claim Release 1.12 tagged/published/closed yet.

Preserve:

``` text
Initiative-1.11 != Product Release 1.11
```

Document accepted current state:

``` text
Region=West US 2
Plan=Linux F1/Free
ImageDigest=sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
SQLiteSchemaVersion=4
SQLiteJournalMode=delete
DatabasePath=/home/data/aiquant.db
RecurringInfrastructureCostTarget=$0.00
```

Document WP07 restart/recycle/same-digest-redeploy continuity without
inventing an SLA.

## ROADMAP conditional update

Inspect:

``` text
docs/project/ROADMAP.md
```

If current-status wording is stale relative to accepted
WP01--WP07/WP08-in-progress state, make the minimum
additive/corrective/status-only update.

If not stale:

``` text
ROADMAPChangeRequired=false
```

and leave it byte-identical.

## Documentation truth constraints

Never state or imply: - production readiness; - production SLA; - HA
guarantee; - paid fallback; - Azure SQL; - Container Apps; - Azure
Files; - mandatory ACR; - automatic Key Vault paid dependency; - load
balancer/Traffic Manager/Front Door failover; - schema migration; -
automatic secret copying; - Release 1.12 publication before publication
authority; - Release 2.0 work.

The `$0.00` statement is the accepted recurring-infrastructure
architecture target, not an unsupported universal billing guarantee.

## Technical immutability

Require:

``` text
TechnicalImplementationRequired=false
SourceCodeMutationCount=0
SchemaMutationCount=0
ImageBuildCount=0
ImagePublicationCount=0
AzureMutationCount=0
RunIdAllocationCount=0
SecretAccessCount=0
```

No source, schema, image, Azure, deployment, or runtime changes.

## Validation

After implementation, run an exhaustive documentation validation cycle.

At minimum: 1. exact changed-path audit; 2. README pre/post semantic
reconciliation; 3. Markdown structural validation; 4. local
relative-link validation; 5. stale/contradictory statement scan; 6.
accepted-fact reconciliation against WP01--WP07 evidence; 7. PowerShell
5.1 compatibility review for commands/examples; 8. secret scan; 9.
unsupported SLA/HA/paid-fallback claim scan; 10.
release-publication-prematurity scan; 11. `git diff --check`; 12.
repository documentation tests/linters if tracked and applicable.

Evaluate all failures before correction.

For correctable documentation defects:

``` text
collect all failures
→ correct all authorized-path defects
→ rerun complete validation
→ repeat until PASS
```

Stop only if correction requires a path outside authority, technical
implementation, architecture/policy decision, secret access, Azure
mutation, or other governance boundary.

## Git publication

Once all local validation passes:

1.  verify only authorized paths changed;
2.  create one focused WP08 documentation commit;
3.  push one focused branch;
4.  create one focused PR referencing #267;
5.  allow required CI/checks to complete;
6.  if CI reveals an in-scope documentation defect, fix it on the same
    branch/PR and rerun validation;
7.  merge only after all required checks pass;
8.  verify `origin/main` contains exactly the accepted documentation
    changes.

Do not close #267 in this authority.

Do not set WP08 Project Status to Done.

Do not close milestone #63.

If repository automation unexpectedly closes/mutates lifecycle state,
stop and report the exact unexpected mutation.

## Mutation accounting

Report actual counts:

``` text
READMEChanged
OperationsRunbookCreated
ReleaseAcceptanceDocumentCreated
ROADMAPChangeRequired
ROADMAPChanged
TrackedDocumentationPathMutationCount
SourceCodeMutationCount
SchemaMutationCount
ImageBuildCount
ImagePublicationCount
AzureMutationCount
RunIdAllocationCount
SecretAccessCount
IssueMutationCount
ProjectMutationCount
MilestoneMutationCount
CommitCount
PushCount
PRCreateCount
PRMergeCount
```

## Required output

``` text
SelectedModel
EntryOriginMain
ImplementationBase
MainAdvanceClassification

READMEInventoryResult
READMEChanged
READMEAuthorizedChangeType
READMEInformationPreservationResult

OperationsRunbookPath
OperationsRunbookResult
ReleaseAcceptancePath
ReleaseAcceptanceDocumentationResult
ROADMAPChangeRequired
ROADMAPChanged

CurrentDeploymentDocumentationResult
PersistenceDocumentationResult
SystemHealthDocumentationResult
RecoveryDocumentationResult
CostDocumentationResult
LimitationsDocumentationResult
QuotaRecoveryDocumentationResult
PowerShell51CompatibilityResult
SecretHygieneResult
UnsupportedClaimsResult

ChangedPaths
UnauthorizedChangedPathCount
MarkdownValidationResult
RelativeLinkValidationResult
GitDiffCheckResult
DocumentationTestResult

TechnicalImplementationRequired
SourceCodeMutationCount
SchemaMutationCount
ImageBuildCount
ImagePublicationCount
AzureMutationCount
RunIdAllocationCount
SecretAccessCount

CommitSHA
PRNumber
PRMergeSHA
PostMergeOriginMain
Issue267After
ProjectWP08StatusAfter
Milestone63After

ValidationGateCount
ValidationPassCount
ValidationFailCount
ValidationNotProvenCount
DefectCount
Defects

WP08DocumentationImplementationResult
WP08Result
BoundaryBlocker
NextAuthorizedAction
```

## Exact PASS gate

Only after documentation implementation, validation, PR merge, and
post-merge verification all pass:

``` text
RELEASE 1.12 WP08 — README INFORMATION PRESERVATION: PASS
RELEASE 1.12 WP08 — OPERATIONS RUNBOOK: PASS
RELEASE 1.12 WP08 — RELEASE ACCEPTANCE DOCUMENTATION: PASS
RELEASE 1.12 WP08 — CURRENT DEPLOYMENT DOCUMENTATION: PASS
RELEASE 1.12 WP08 — PERSISTENCE DOCUMENTATION: PASS
RELEASE 1.12 WP08 — SYSTEM HEALTH DOCUMENTATION: PASS
RELEASE 1.12 WP08 — RECOVERY DOCUMENTATION: PASS
RELEASE 1.12 WP08 — COST DOCUMENTATION: PASS
RELEASE 1.12 WP08 — LIMITATIONS DOCUMENTATION: PASS
RELEASE 1.12 WP08 — POWERSHELL 5.1 COMPATIBILITY: PASS
RELEASE 1.12 WP08 — SECRET HYGIENE: PASS
RELEASE 1.12 WP08 — EXACT PATH/SCOPE AUDIT: PASS
RELEASE 1.12 WP08 — DOCUMENTATION VALIDATION: PASS
RELEASE 1.12 WP08 — PUBLICATION PR MERGE: PASS
RELEASE 1.12 WP08 — POST-MERGE VERIFICATION: PASS

WP08DocumentationImplementationResult=PASS
WP08Result=READY_FOR_LUNA_FINAL_ACCEPTANCE
BoundaryBlocker=NONE
NextAuthorizedAction=GPT-5.6 Luna performs fresh read-only WP08 final substantive acceptance reconciliation against the merged documentation and accepted WP01-WP07 state
```

Lifecycle remains explicitly deferred.

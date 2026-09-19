# Release 1.12 WP05 — Terra Resume After User-Controlled Credential

**Selected execution model: GPT-5.6 Terra**

## Entry state

Prior WP05 execution correctly stopped at:

```text
WP05Result=NOT_READY
BoundaryBlocker=USER_CONTROLLED_TWELVE_DATA_CREDENTIAL_REQUIRED
RELEASE 1.12 WP05 — SECRET HYGIENE: PASS
RELEASE 1.12 WP05 — IMPLEMENTATION: NOT_STARTED
RELEASE 1.12 WP05 — RUNTIME VALIDATION: NOT_PERFORMED
```

No source, Git, GitHub, Azure App Settings, restart, Docker/GHCR, image, WP06, WP07, README, or paid-resource mutation occurred.

This authority is a continuation of the same WP05 boundary **only after the user has made the real Twelve Data credential available through the established secure mechanism**.

## Model roles

```text
GPT-5.6 Luna = contract/policy/architecture/governance
GPT-5.6 Terra = selected implementation/validation/mutation authority
GPT-5.6 Sol = supporting analysis only
```

## Critical secret rule

Do not ask the user to paste the API key into Codex/chat output.

Never print, echo, serialize, hash for disclosure, persist in evidence, commit, place in PR text, or otherwise expose the credential.

Evidence may contain only safe metadata:

```text
SettingName=TwelveData__ApiKey
Present=true|false
NonEmpty=true|false
SecretValueDisclosed=false
```

If the credential is still unavailable through the established secure mechanism, stop again with exactly:

```text
BoundaryBlocker=USER_CONTROLLED_TWELVE_DATA_CREDENTIAL_REQUIRED
```

Do not manufacture a value.

## Canonical source anchor

WP04 merge/main anchor entering WP05:

```text
002da352a5e8ebf0ad2565a45cbde28de8c09c3f
```

Fetch and reconcile current `origin/main`. If main legitimately advanced, record the actual anchor and ensure WP05 dependencies/contracts remain intact.

Preserve untracked disposable artifacts.

Require before tracked mutation:

```text
TrackedModifications=0
StagedPaths=0
git diff --check=PASS
```

## Mission

Resume the canonical WP05 contract already extracted:

```text
Mission=secret-safe bounded Twelve Data runtime configuration and deterministic failure isolation
Owner=WP05
```

Out of scope:

```text
WP06 presentation/System Health implementation
WP07 deployment stability/restart/recycle/redeploy acceptance
persistence/schema changes
paid resources
README changes
architecture changes
```

Accepted image remains unchanged unless the canonical WP05 plan explicitly and unambiguously requires otherwise.

## Credential preflight

Using only the established secure source, verify:

```text
CredentialAvailable=true
CredentialNonEmpty=true
SecretValueDisclosed=false
```

Do not copy the secret into a tracked file.

If Azure configuration is part of the canonical WP05 plan, capture sanitized pre-state before mutation.

## Implementation and convergence

Read/reconfirm:

```text
docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md
docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md
docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md
GitHub issue #264
```

Implement only already-defined WP05 paths/behavior.

Use bounded convergence:

```text
evaluate ALL gates
→ collect ALL in-scope failures
→ fix all authorized local defects
→ fresh hashes/evidence
→ rerun complete affected validation
→ repeat until PASS
```

Do not return after each correctable implementation/harness/PowerShell/evidence defect.

Every byte-changing tracked correction invalidates prior candidate hashes and affected PASS evidence.

Hard stop only for a true governance boundary: new architecture/policy decision, WP06/WP07 implementation, paid resource, persistence/schema semantic change outside plan, destructive README change, or unplanned deployment/image architecture change.

## PowerShell/build/test

Binding baseline:

```text
Windows PowerShell 5.1.26100.9444
```

Require:
- all changed PowerShell parses under WinPS 5.1;
- Release build 0 warnings/errors;
- relevant/full test suite PASS;
- signing contract preserved;
- `git diff --check` PASS;
- no WP04 regression.

Never bypass signing.

## Azure/App Settings mutation

If canonical WP05 requires setting `TwelveData__ApiKey`, mutate only the authorized setting through the established secure mechanism.

Do not expose its value in command output/evidence.

Truthfully account for an App Settings write causing a platform-triggered restart:

```text
PlatformTriggeredRestartCount=<observed>
ExplicitRestartCount=0
```

Do not add a redundant explicit restart unless the canonical WP05 contract explicitly requires one.

Do not perform WP07 restart/recycle/redeploy validation.

## Runtime validation

After secure configuration, perform only the canonical WP05 runtime validation.

Require evidence for:
- required setting present/nonempty without disclosure;
- normal runtime no longer fails specifically because `TwelveData__ApiKey` is absent;
- bounded Twelve Data automation behavior;
- deterministic terminal behavior;
- failure isolation;
- no unbounded retry/polling;
- secret hygiene;
- exact target attribution;
- no persistence/architecture bypass;
- accepted image identity retained unless plan explicitly says otherwise.

Do not claim WP06 public UI/System Health acceptance.
Do not claim WP07 stability/recovery acceptance.

## Repository boundary

Require:

```text
UnrelatedTrackedPathCount=0
UnauthorizedREADMEInformationLoss=0
WP06ImplementationIncluded=0
WP07ImplementationIncluded=0
PaidResourceMutationCount=0
PersistenceBypassCount=0
SecretDisclosureCount=0
```

README preservation remains binding.

## Candidate commit/push

Only after all implementation/local gates pass:
- enumerate exact changed paths;
- confirm every path is WP05-authorized;
- confirm no secret material tracked;
- capture hashes;
- commit/push the WP05 candidate branch if committed provenance is required by the canonical validation flow.

Do not create/merge the final WP05 PR or close #264 unless separately authorized by an already-explicit canonical WP05 lifecycle rule. Default handoff after substantive implementation/runtime validation is Luna acceptance.

## Mutation accounting

Return exact actual counts:

```text
TrackedSourcePathCount
GitCommitCount
GitPushCount
AzureAppSettingMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
PaidResourceMutationCount
WP06ActionCount
WP07ActionCount
PRCount
MergeCount
IssueLifecycleMutationCount
ProjectMutationCount
MilestoneMutationCount
READMETrackedMutationCount
SecretDisclosureCount
```

Count only actions actually performed.

## Success markers

Use the canonical WP05 acceptance markers from the planning artifacts/issue and additionally emit:

```text
RELEASE 1.12 WP05 — IMPLEMENTATION: PASS
RELEASE 1.12 WP05 — SECRET HYGIENE: PASS
RELEASE 1.12 WP05 — BOUNDED AUTOMATION: PASS
RELEASE 1.12 WP05 — RUNTIME VALIDATION: PASS
RELEASE 1.12 WP05 — ARCHITECTURE BOUNDARY: PASS
RELEASE 1.12 WP05 — READY_FOR_LUNA_ACCEPTANCE: YES
```

Then:

```text
WP05Result=READY_FOR_LUNA_ACCEPTANCE
NextAuthorizedAction=GPT-5.6 Luna final read-only WP05 substantive acceptance reconciliation
```

## Required handoff

Return:

```text
SelectedModel
EntryMain
EntryOriginMain
WP05Branch
WP05Mission
WP05AcceptanceCriteria
PlannedPaths
AllowedAzureMutationClasses
SecretHandlingContract
BoundedAutomationContract

CredentialAvailable
CredentialNonEmpty
TwelveDataSettingPresent
TwelveDataSettingNonEmpty
SecretValueDisclosed

ChangedPathCount
ChangedPaths
CandidateCommit
RemoteCandidateTip
CandidateHashes

ReleaseBuildResult
ReleaseBuildWarningCount
ReleaseBuildErrorCount
TestResult
TestPassedCount
TestFailedCount
PowerShellVersion
PowerShellValidationResult
SigningValidationResult
GitDiffCheckResult

RuntimeValidationPerformed
RuntimeValidationResult
BoundedAutomationValidationResult
NormalRuntimeReadinessResult
FailureIsolationResult

FinalImageDigest
FinalAppServiceState
FinalAppMode

UnrelatedTrackedPathCount
UnauthorizedREADMEInformationLoss
WP06ImplementationIncluded
WP07ImplementationIncluded
PaidResourceMutationCount
PersistenceBypassCount

TrackedSourcePathCount
GitCommitCount
GitPushCount
AzureAppSettingMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
WP06ActionCount
WP07ActionCount
PRCount
MergeCount
IssueLifecycleMutationCount
ProjectMutationCount
MilestoneMutationCount
READMETrackedMutationCount
SecretDisclosureCount

FinalHEAD
FinalRemoteBranchTip
FinalTrackedClean
FinalStagedPathCount
FinalGitDiffCheck

WP05Result
BoundaryBlocker
NextAuthorizedAction
```

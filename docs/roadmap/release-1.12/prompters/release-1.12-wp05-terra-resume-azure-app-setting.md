# Release 1.12 WP05 — Terra Resume After Azure App Setting Credential

**Selected execution model: GPT-5.6 Terra**

## Authority roles

```text
GPT-5.6 Luna = contract, policy, architecture, governance, acceptance
GPT-5.6 Terra = selected implementation, validation, and authorized mutation executor
GPT-5.6 Sol = supporting analysis only; no replacement of Luna/Terra authority
```

## User architecture decision

For Release 1.12 WP05, the Twelve Data credential mechanism is:

```text
CredentialStore=Azure App Service application setting
ApplicationSettingName=TwelveData__ApiKey
KeyVaultIntroduced=false
GitHubSecretRequired=false
PaidResourceIntroduced=false
```

This preserves the Release 1.12 $0 reference-deployment boundary.

The user must enter the real credential directly through their authenticated Azure mechanism. The credential MUST NOT be pasted into Codex/chat or committed to the repository.

## Entry boundary

Previous executions correctly stopped with:

```text
WP05Result=NOT_READY
BoundaryBlocker=USER_CONTROLLED_TWELVE_DATA_CREDENTIAL_REQUIRED
```

Resume only after the user confirms that `TwelveData__ApiKey` is configured and non-empty in the target Azure App Service.

If it is still absent, stop with the same boundary marker. Never invent a credential.

## Secret hygiene — mandatory

Never print, echo, fetch for display, serialize, persist in evidence, hash for disclosure, commit, place in GitHub issue/PR text, or otherwise expose the secret value.

Permitted evidence:

```text
SettingName=TwelveData__ApiKey
Present=true|false
NonEmpty=true|false
SecretValueDisclosed=false
```

All command/log handling must be designed so the value itself cannot appear in captured output.

## Canonical reconciliation

Start by fetching/reconciling current `origin/main`.

Historical WP05 entry anchor after WP04:

```text
002da352a5e8ebf0ad2565a45cbde28de8c09c3f
```

If `origin/main` has legitimately advanced, record the live SHA and confirm the Release 1.12/WP05 contract remains intact before mutation.

Read/reconcile:

```text
docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md
docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md
docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md
GitHub issue #264
```

## Mission

```text
Mission=secret-safe bounded Twelve Data runtime configuration and deterministic failure isolation
Owner=WP05
```

Out of scope:

```text
WP06 presentation/System Health implementation
WP07 restart/recycle/redeploy acceptance
SQLite/persistence/schema semantic changes
Key Vault
paid Azure resources
README changes
architecture bypasses
```

## Azure setting verification

Verify only safe metadata for the target App Service:

```text
TwelveDataSettingPresent=true
TwelveDataSettingNonEmpty=true
SecretValueDisclosed=false
```

Do not retrieve the setting value for evidence.

If the user has just changed the App Setting, truthfully account for the platform-triggered application restart. Do not perform a redundant explicit restart merely to apply the setting.

```text
PlatformTriggeredRestartCount=<observed/attributable count>
ExplicitRestartCount=0
```

This is configuration application, NOT WP07 restart/recovery acceptance.

## Implementation and bounded convergence

Implement only the already-defined WP05 scope.

Use the convergence loop:

```text
evaluate ALL authorized gates
→ collect ALL in-scope failures
→ fix ALL locally correctable WP05 defects
→ refresh hashes/evidence
→ rerun the complete affected validation cycle
→ repeat until PASS
```

Do not return after each locally correctable defect.

Every byte-changing tracked correction invalidates affected candidate hashes/PASS evidence and requires fresh validation.

Stop only if correction would cross a governance boundary: WP06/WP07 implementation, paid resources, new architecture/policy, persistence/schema changes outside contract, README information loss, unplanned image/deployment architecture change, or another user-controlled secret requirement.

## Platform/runtime constraints

Binding PowerShell baseline:

```text
Windows PowerShell 5.1.26100.9444
```

Require:
- changed PowerShell parses under WinPS 5.1;
- signing policy preserved;
- Release build PASS with 0 warnings and 0 errors;
- required tests PASS;
- `git diff --check` PASS;
- no WP04 regression;
- accepted image identity remains unchanged unless the canonical WP05 contract explicitly requires otherwise.

## WP05 runtime validation

Once the setting is confirmed present/non-empty, execute the canonical WP05 runtime validation.

Require exhaustive evidence that:
- missing-key blocker is removed;
- credential remains undisclosed;
- Twelve Data access follows the canonical application boundary;
- automation is bounded;
- retry/poll behavior is bounded;
- terminal failure behavior is deterministic;
- failure isolation works;
- no direct/bypass data-access path is introduced;
- no persistence/schema bypass occurs;
- no WP06 presentation acceptance is claimed;
- no WP07 stability/recovery acceptance is claimed;
- no paid resource is introduced.

Do not interpret a Twelve Data service/API failure as permission to weaken secret hygiene or bypass architecture.

## Repository boundary

Before tracked mutation require:

```text
TrackedModifications=0
StagedPaths=0
git diff --check=PASS
```

Preserve unrelated untracked files.

Require final:

```text
UnrelatedTrackedPathCount=0
UnauthorizedREADMEInformationLoss=0
WP06ImplementationIncluded=0
WP07ImplementationIncluded=0
PaidResourceMutationCount=0
PersistenceBypassCount=0
SecretDisclosureCount=0
```

## Candidate provenance

After all WP05 implementation/local/runtime gates pass:
- enumerate exact changed paths;
- verify every path is WP05-authorized;
- verify no credential material is tracked;
- capture hashes;
- commit/push the WP05 candidate branch if required by the canonical flow.

Default handoff is Luna substantive acceptance. Do not merge final WP05 publication or close #264 unless explicitly authorized by the canonical lifecycle authority.

## Mutation accounting

Return exact counts for actions actually performed:

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

If the user manually created the Azure setting before this authority, do not falsely count that as a Terra mutation. Record it as pre-existing entry state.

## Success

Emit:

```text
RELEASE 1.12 WP05 — IMPLEMENTATION: PASS
RELEASE 1.12 WP05 — SECRET HYGIENE: PASS
RELEASE 1.12 WP05 — BOUNDED AUTOMATION: PASS
RELEASE 1.12 WP05 — RUNTIME VALIDATION: PASS
RELEASE 1.12 WP05 — ARCHITECTURE BOUNDARY: PASS
RELEASE 1.12 WP05 — READY_FOR_LUNA_ACCEPTANCE: YES
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
CredentialStore
SettingName
TwelveDataSettingPresent
TwelveDataSettingNonEmpty
SecretValueDisclosed

WP05Mission
WP05AcceptanceCriteria
PlannedPaths
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

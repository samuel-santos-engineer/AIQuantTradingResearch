# Release 1.12 WP05 — Terra Runtime Configuration, Secrets & Bounded Automation

**Selected execution model: GPT-5.6 Terra**

## 0. Entry gate

WP04 is complete:

```text
PR #277=MERGED
WP04MergeSHA=002da352a5e8ebf0ad2565a45cbde28de8c09c3f
origin/main=002da352a5e8ebf0ad2565a45cbde28de8c09c3f
Issue263=CLOSED
Project2WP04Status=DONE
Milestone63=OPEN
WP04Lifecycle=COMPLETE
WP05State=NOT_STARTED_READY_FOR_NEXT_AUTHORITY
WP07State=NOT_STARTED
```

WP05 issue:

```text
#264 WP05 Twelve Data Runtime Configuration, Secrets & Bounded Automation
```

Dependency `WP04 → WP05` is satisfied.

---

## 1. Model roles

```text
GPT-5.6 Luna
  release contract/policy/architecture/governance

GPT-5.6 Terra
  selected:
  inspect established WP05 contract
  implement only already-defined WP05 scope
  validate
  perform explicitly governed runtime/configuration mutations
  converge internally on in-scope defects

GPT-5.6 Sol
  supporting analysis only
```

This is NOT authority to invent a new architecture. If the existing planning artifacts do not define a material WP05 policy needed for implementation, stop for a narrow Luna decision.

---

## 2. Canonical source anchor

Start from fresh synchronized `main`.

Expected entry anchor:

```text
002da352a5e8ebf0ad2565a45cbde28de8c09c3f
```

Verify live `origin/main` before creating any WP05 branch.

If `main` has legitimately advanced, record the new anchor and reconcile that it does not invalidate WP05 planning/dependencies.

Require:
- tracked clean;
- staged paths 0;
- `git diff --check` PASS;
- existing untracked disposable artifacts preserved and untouched.

Create/use the repository-conventional WP05 release/work branch only after entry reconciliation.

---

## 3. Read the canonical WP05 contract first

Before implementation, inspect:

```text
docs/roadmap/release-1.12/RELEASE_1.12_DEFINITION.md
docs/roadmap/release-1.12/RELEASE_1.12_EXECUTION_PLAN.md
docs/roadmap/release-1.12/RELEASE_1.12_FILE_MANIFEST.md
GitHub issue #264
relevant existing Twelve Data configuration/runtime code
WP03 deployment automation
WP04 merged runtime/persistence behavior
```

Extract and durably report:

```text
WP05Mission
WP05InScope
WP05OutOfScope
WP05AcceptanceCriteria
PlannedPaths
AllowedAzureMutationClasses
SecretHandlingContract
BoundedAutomationContract
NormalRuntimeReadinessContract
WP06Boundary
WP07Boundary
```

Planning artifacts are authoritative. Do not silently expand scope.

---

## 4. Known binding release architecture

Preserve:

```text
Azure App Service Linux F1
West Central US
custom Docker
public/free GHCR
public/default HTTPS/DNS
persistent /home
SQLite /home/data/aiquant.db
SQLite DELETE
strict $0 reference/demo
no production SLA
```

Current accepted image entering WP05:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

WP04 established that normal mode requires:

```text
TwelveData__ApiKey
```

and absence of that key was:

```text
EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER
Owner=WP05
```

WP05 must resolve only the WP05-owned configuration/secrets/runtime boundary defined by planning.

---

## 5. Secret safety — non-negotiable

Never:
- print the Twelve Data API key;
- persist it in tracked files;
- place it in evidence artifacts;
- expose it in command transcripts;
- include it in PR text/issues/comments;
- commit `.env`/secret material;
- echo complete App Settings values if that would disclose it.

Use the repository's established secure/user-controlled secret mechanism.

If the required real Twelve Data credential is not available through an already-authorized environment/secret source, STOP at the credential-required boundary with:

```text
BoundaryBlocker=USER_CONTROLLED_TWELVE_DATA_CREDENTIAL_REQUIRED
```

Do not ask Codex to manufacture a credential.

Configuration evidence must retain only safe facts such as:

```text
SettingName
Present=true|false
NonEmpty=true|false
SourceClass
SecretValueDisclosed=false
```

No secret value or reversible derivative.

---

## 6. Scope discipline

WP05 owns only the planned Twelve Data runtime configuration, secret handling, and bounded automation.

Do NOT implement:
- WP06 public Streamlit/System Health deployment/diagnostics;
- WP07 restart/recycle/redeploy stability acceptance;
- new persistence architecture;
- schema migration unless explicitly planned/proven;
- Azure SQL;
- Container Apps;
- Azure Files;
- mandatory ACR;
- paid services;
- ML/backtesting/live trading;
- production SLA/security claims;
- README information deletion/compression.

WP07 lifecycle actions remain unauthorized unless WP05 planning explicitly contains a narrower configuration-triggered behavior and Luna has already classified it as WP05 rather than WP07.

Remember: App Settings writes can trigger an App Service restart. Account truthfully for platform-triggered effects; do not add redundant explicit restart.

---

## 7. Implementation convergence

Implement only the paths/classes permitted by the WP05 contract.

Use the user's preferred convergence model:

```text
evaluate ALL gates
→ collect ALL in-scope defects
→ correct all authorized defects
→ rerun complete affected validation
→ repeat until PASS
```

Do not return after each local defect.

For every tracked byte change:
- invalidate prior candidate hashes;
- recompute hashes;
- rerun complete affected structural/build/test validation.

Correct internally:
- source/config defects within planned WP05 paths;
- PowerShell 5.1 compatibility defects;
- bounded automation defects;
- safe secret-redaction/evidence defects;
- local harness defects;
- manifest/reference defects.

Hard stop for:
- new Luna architecture/policy decision;
- WP06/WP07 implementation;
- paid-resource requirement;
- schema/persistence semantic change outside plan;
- README destructive change;
- unplanned image/deployment architecture change.

---

## 8. Windows PowerShell baseline

Binding:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7 assumptions.

Parse every changed `.ps1` under Windows PowerShell 5.1 and execute the relevant local tests/harnesses.

---

## 9. Build/test/signing

Use established repository commands.

Require:
- Release build 0 warnings, 0 errors;
- complete relevant test suite PASS;
- no regression to accepted WP04 behavior;
- `git diff --check` PASS.

Signing contract:

```text
Release Authenticode signature not required
Debug local signing required where applicable
Expected signer CN=AIQuantTradingDev
existing Debug-only AutoSignTestBinaries
```

Never bypass signing.

---

## 10. Bounded automation acceptance

From the planning artifacts, instantiate exact bounded behavior.

At minimum reconcile:
- bounded execution;
- deterministic terminal status;
- fail-closed behavior;
- no unbounded polling/retry;
- no secret disclosure;
- exact target/environment attribution;
- no architecture bypass;
- truthful mutation accounting.

If the plan defines a specific schedule, timeout, retry count, trigger, or command shape, use exactly that contract rather than inventing alternatives.

Retain sanitized evidence.

---

## 11. Runtime/configuration validation

Only after structural/local validation passes and the necessary user-controlled credential is securely available, execute the planned WP05 target validation.

Before Azure mutation, capture sanitized pre-state.

Any App Settings mutation must:
- touch only WP05-authorized setting names;
- never log secret values;
- account for the platform-triggered restart caused by settings writes;
- avoid an additional explicit restart unless the canonical plan explicitly requires it;
- preserve the accepted immutable image unless WP05 planning explicitly authorizes a new image.

Validate the exact WP05 acceptance surface defined by the roadmap/issue.

Do not claim WP06 System Health/public UI acceptance.
Do not claim WP07 restart/recycle/redeploy acceptance.

---

## 12. Repository and architecture invariants

After implementation/runtime validation require:

```text
UnrelatedTrackedPathCount=0
UnauthorizedREADMEInformationLoss=0
WP06ImplementationIncluded=0
WP07ImplementationIncluded=0
PaidResourceMutationCount=0
PersistenceBypassCount=0
```

README preservation policy:

```text
EXISTING README INFORMATION → PRESERVE
PRESERVE INFORMATION; UPDATE STATUS
SUMMARY ≠ AUTHORITY TO DELETE DETAIL
README PRESERVATION — ZERO UNAUTHORIZED INFORMATION LOSS: PASS
```

---

## 13. Candidate publication boundary

This authority MAY commit/push the completed WP05 implementation to its governed WP05 branch only after all implementation/local validation gates pass.

Before commit:
- enumerate exact changed paths;
- verify every path is WP05-authorized;
- capture candidate hashes;
- build/test/PowerShell validation PASS;
- no secret material tracked.

Commit/push only the accepted WP05 paths.

If runtime target validation depends on committed provenance, publish the candidate branch first, then validate against that exact committed identity.

Do not create/merge the final WP05 PR or close #264 in this authority unless the canonical WP05 plan explicitly defines those actions as part of the same already-authorized boundary. Default next gate is Luna substantive acceptance after Terra implementation/validation.

---

## 14. Mutation accounting

Report exact actual counts:

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

Never count inferred/automatic behavior as an explicit Terra mutation.

---

## 15. Completion condition

On successful implementation + required target validation, emit the exact WP05 acceptance markers found in the canonical planning artifacts/issue.

Also emit:

```text
RELEASE 1.12 WP05 — IMPLEMENTATION: PASS
RELEASE 1.12 WP05 — SECRET HYGIENE: PASS
RELEASE 1.12 WP05 — BOUNDED AUTOMATION: PASS
RELEASE 1.12 WP05 — ARCHITECTURE BOUNDARY: PASS
RELEASE 1.12 WP05 — READY_FOR_LUNA_ACCEPTANCE: YES
```

If the canonical contract requires a real credential that is unavailable, return NOT_READY without weakening the gate.

If a true governance boundary is encountered, return one exhaustive defect/boundary matrix.

---

## 16. Required handoff

Return:

```text
SelectedModel
EntryMain
EntryOriginMain
WP05Branch
WP05Mission
WP05InScope
WP05OutOfScope
WP05AcceptanceCriteria
PlannedPaths
AllowedAzureMutationClasses
SecretHandlingContract
BoundedAutomationContract

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

TwelveDataCredentialAvailable
TwelveDataSettingPresent
TwelveDataSettingNonEmpty
SecretValueDisclosed

RuntimeValidationPerformed
RuntimeValidationResult
BoundedAutomationValidationResult
NormalRuntimeReadinessResult

FinalImageDigest
FinalAppServiceState
FinalAppMode

UnrelatedTrackedPathCount
UnauthorizedREADMEInformationLoss
WP06ImplementationIncluded
WP07ImplementationIncluded

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

FinalHEAD
FinalRemoteBranchTip
FinalTrackedClean
FinalStagedPathCount
FinalGitDiffCheck

WP05Result
BoundaryBlocker
NextAuthorizedAction
```

Success:

```text
WP05Result=READY_FOR_LUNA_ACCEPTANCE
NextAuthorizedAction=GPT-5.6 Luna final read-only WP05 substantive acceptance reconciliation
```

Credential boundary:

```text
WP05Result=NOT_READY
BoundaryBlocker=USER_CONTROLLED_TWELVE_DATA_CREDENTIAL_REQUIRED
NextAuthorizedAction=User supplies/configures the required credential through the established secure mechanism, then resume this same Terra WP05 authority
```

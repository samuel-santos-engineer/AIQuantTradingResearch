# Release 1.12 WP04 — Terra Three-Value Digest Rebind, Publication & Fresh Qualification

**Selected execution model: GPT-5.6 Terra**

## 0. Binding reconciliation

The previous two-occurrence authority is superseded.

The wrapper contains three linked digest roles:

```text
$expectedDigest
  production/runtime preflight expected image identity

$exactIdentity
  exact-current-digest positive fixture used by V1 and V8

V6-partial-digest
  deliberately truncated negative fixture derived from the same exact identity
```

Canonical decision:

```text
THREE_VALUE_REBIND=AUTHORIZED

$expectedDigest
  → rebind to the exact newly deployed immutable digest

$exactIdentity
  → rebind to the same exact newly deployed immutable digest

V6-partial-digest
  → rebase to an intentionally truncated/partial form derived from the new
    $exactIdentity, preserving rejection semantics

V1-exact
  → must remain PASS

V8
  → must remain PASS under its existing exact-identity semantics

V6-partial-digest
  → must remain a negative/rejected case

ExactDigestValidation
  → MUST NOT be weakened
```

This is one coherent digest-identity maintenance change, not three independent product changes.

No other wrapper semantic change is authorized.

---

## 1. Mission

Starting from the restored clean repository state at:

```text
13b085fec8af4b746b8ff0579b611451e8429a27
```

perform the three-value wrapper rebind, validate the complete wrapper fixture matrix, commit and non-force push the one-file change, and then continue directly into fresh WP04 qualification.

Current deployed image:

```text
ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

No Docker build, GHCR publication, or image deployment is required or authorized.

---

## 2. Model roles

```text
GPT-5.6 Luna
  contract, policy, architecture, governance, final acceptance

GPT-5.6 Terra
  implementation, validation, approved Git and temporary Azure mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected execution model: **GPT-5.6 Terra**.

---

## 3. Entry gate

Before editing require:

```text
HEAD=13b085fec8af4b746b8ff0579b611451e8429a27
origin/release/1.12-wp04-persistent-sqlite=13b085fec8af4b746b8ff0579b611451e8429a27
CurrentBranch=release/1.12-wp04-persistent-sqlite
TrackedModifications=0
StagedPaths=0
git diff --check=PASS
```

Read-only verify deployed image:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

Binding PowerShell:

```text
Windows PowerShell 5.1.26100.9444
```

If entry state differs, STOP before mutation and report the complete mismatch matrix.

---

## 4. Pre-edit wrapper role inventory

Before editing, inspect the wrapper and identify the exact definitions/usages of:

```text
$expectedDigest
$exactIdentity
V1-exact
V6-partial-digest
V8
```

Return:

```text
ExpectedDigestDefinition
ExactIdentityDefinition
V1ExactUsage
V6PartialDigestConstruction
V8ExactIdentityUsage
```

Prove:

```text
ExpectedDigestRole=RUNTIME_PREFLIGHT
ExactIdentityRole=POSITIVE_CURRENT_DIGEST_FIXTURE
V6Role=TRUNCATED_NEGATIVE_CURRENT_DIGEST_FIXTURE
```

If these roles differ materially from the user-reported mapping, STOP for Luna rather than guessing.

---

## 5. Exact authorized tracked path

Only:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

No endpoint edit.
No verification-helper edit.
No other tracked path.

---

## 6. Exact authorized semantic edit

Canonical deployed digest:

```text
$newDigest = sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

### 6.1 Runtime identity

Rebind `$expectedDigest` to the exact full new digest.

Require:

```text
$expectedDigest == $newDigest
```

### 6.2 Positive fixture identity

Rebind `$exactIdentity` to the exact full new digest.

Require:

```text
$exactIdentity == $newDigest
$exactIdentity == $expectedDigest
```

Do not change the V1/V8 test intent.

### 6.3 V6 negative fixture

Rebase the V6 input so it remains deliberately partial/truncated relative to the new `$exactIdentity`.

Require:

```text
V6InputDerivedFromNewExactIdentity=true
V6InputIsTruncatedOrPartial=true
V6Input != $exactIdentity
V6Input != $expectedDigest
```

Do not replace V6 with the full digest.

Do not weaken the exact comparison.

Prefer preserving the existing construction/truncation style. Change only the digest-derived value needed to keep the same test semantics.

---

## 7. Forbidden wrapper changes

Require all:

```text
PersistenceSemanticsChanged=false
AuthenticationSemanticsChanged=false
EvidencePayloadContractChanged=false
RunIdSemanticsChanged=false
TokenSemanticsChanged=false
AzureMutationSurfaceChanged=false
ArchiveTruthPolicyChanged=false
H1LifecycleChanged=false
WP04WP07OwnershipChanged=false
ExactDigestComparisonWeakened=false
OtherValidationFixtureSemanticsChanged=false
```

No broad refactor.
No formatting-only churn outside the necessary lines.
No unrelated cleanup.

---

## 8. Local convergence and complete fixture validation

After the edit, evaluate the complete applicable wrapper validation matrix, not only V1/V6/V8.

At minimum explicitly report:

```text
V1ExactResult
V6PartialDigestResult
V8Result
CurrentExactDigestPositiveCase
PartialDigestRejected
WrongDigestRejected
```

Require:

```text
V1ExactResult=PASS
V6PartialDigestResult=PASS
V8Result=PASS
CurrentExactDigestPositiveCase=PASS
PartialDigestRejected=PASS
WrongDigestRejected=PASS
```

Also run:

```text
PowerShell AST parse
wrapper local validation
helper compatibility validation
digest/provenance positive cases
digest/provenance negative cases
git diff --check
```

Require:

```text
WrapperParserErrors=0
HelperParserErrors=0
```

### Bounded local convergence

If a failure is caused solely by incomplete rebinding of these three linked values or their direct digest-derived fixture expression, Terra may correct it in this same wrapper file.

For every byte-changing correction:

```text
invalidate candidate wrapper SHA
recompute wrapper SHA
restart the complete wrapper validation matrix
```

Continue until PASS.

Hard STOP if correction requires any semantic change outside the three linked digest roles or another tracked path.

---

## 9. Post-edit diff proof

Before staging require:

```text
ChangedPathCount=1
ChangedPath=eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Inspect the entire diff.

Require:

```text
ExpectedDigestRebound=true
ExactIdentityRebound=true
V6PartialFixtureRebased=true
OnlyThreeLinkedDigestRolesChanged=true
ExactDigestValidationPreserved=true
```

Record:

```text
PreviousWrapperSHA256
NewWrapperSHA256
HelperSHA256
```

The helper must remain unchanged and clean relative to HEAD.

---

## 10. Git publication

After all local validation passes authorize exactly:

```text
git add eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
git commit
git push origin release/1.12-wp04-persistent-sqlite
```

Push MUST be non-force.

Commit purpose:

```text
Rebind WP04 qualification digest fixtures to authentication-fix image.
```

After push require:

```text
WrapperDigestCommit=<new SHA>
LocalHEAD=WrapperDigestCommit
RemoteReleaseBranchTip=WrapperDigestCommit
CommitChangedPathCount=1
CommitChangedPath=<wrapper>
WorkingTreeClean=true
StagedPaths=0
git diff --check=PASS
```

No PR.
No merge.
No GitHub lifecycle mutation.

---

## 11. No image mutation

The runtime source image is already correct and deployed.

Require:

```text
DockerBuildCount=0
GhcrPublicationCount=0
ImageDeploymentMutationCount=0
```

Read-only verify deployed digest remains exactly:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

Require:

```text
WrapperExpectedDigest == ExactIdentity == DeployedImageDigest
```

If not, STOP before RunId.

---

## 12. Fresh qualification preflight

From the newly committed wrapper anchor require:

```text
HEAD == origin/release/1.12-wp04-persistent-sqlite
WorkingTreeClean=true
StagedPaths=0
git diff --check=PASS
WrapperWorkingTreeSHA256 == WrapperHeadSHA256 == NewWrapperSHA256
HelperWorkingTreeSHA256 == HelperHeadSHA256
WrapperCleanHead=PASS
HelperCleanHead=PASS
WrapperExpectedDigest == DeployedImageDigest
ExactIdentity == DeployedImageDigest
V1ExactResult=PASS
V6PartialDigestResult=PASS
V8Result=PASS
WindowsPowerShellVersion=5.1.26100.9444
WrapperParserErrors=0
HelperParserErrors=0
```

Frozen runner remains:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Only after every gate above passes may a fresh RunId be allocated.

---

## 13. Canonical WP04 ownership

Binding:

```text
CanonicalOwnershipDecision=WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
WP04_DEPLOYMENT_RECOVERY_REQUIRED_FOR_CLOSURE=false
WP07_DEPLOYMENT_RECOVERY_REQUIRED_FOR_RELEASE_ACCEPTANCE=true
```

Do NOT execute:

```text
restart persistence
recycle persistence
redeploy persistence
WP07 deployment recovery qualification
```

The prior authentication-fix image deployment receives no WP07 acceptance credit.

---

## 14. Fresh governed qualification

Allocate fresh helper-owned RB2 RunId(s).

Never reuse any historical RunId.

Every allocated RunId is permanently consumed, including failed attempts.

Execute only:

```text
initialize/reopen
canonical temporary qualification App Settings
H1 Deferred
application-owned HTTP evidence
durable evidence checkpoint
RestoreOnly in finally
```

M1:

```text
No redundant explicit restart
```

Evidence endpoint:

```text
GET /internal/wp04/persistence-qualification?runId=<fresh exact RunId>
X-WP04-Evidence-Token
```

No token persistence or printing.

No direct SQLite shell.
No Python DB access.
No Kudu /home.
No Twelve Data secret configuration.

---

## 15. D01-D19 acceptance contract

Fresh target required:

```text
D01 D02 D03
D05 D06 D07 D08 D09 D10 D11
D12 D13 D14 D15
D17 D19
```

Carry-forward with fresh non-contradiction:

```text
D04 D16 D18
```

Definitions:

```text
D01 initialize invokes application-owned persistence use case
D02 canonical observation accepted or idempotently recognized
D03 canonical observation read back unchanged
D04 conflicting existing evidence rejected/non-overwriting
D05 database identity=/home/data/aiquant.db
D06 schema version=4
D07 journal mode=DELETE
D08 integrity check=ok
D09 quick check=ok
D10 accepted evidence identity/count correct
D11 HTTP evidence attributable to exact fresh RunId
D12 evidence checkpoint before RestoreOnly
D13 RestoreOnly exactly once
D14 temporary qualification settings restored
D15 token/secret hygiene
D16 no direct SQLite/Kudu/Python deployment bypass
D17 F1/West Central US/$0 architecture unchanged
D18 repository/external mutation boundaries exactly authorized
D19 durable evidence reopens with all references resolved
```

D11 must prove the authentication/request-path fix works on the deployed `aad23c8...` image.

D15 must prove authentication/token hygiene remains intact.

No fresh Azure conflict operation for D04.

---

## 16. H1 lifecycle

Require:

```text
Deferred
→ durable evidence checkpoint
→ RestoreOnly in finally
```

and:

```text
CheckpointBeforeRestoreOnly=PASS
RestoreOnlyCount=1
PreStateRestoredExactly=PASS
```

Restoration failure takes precedence over otherwise-successful qualification.

---

## 17. Durable evidence

Create a fresh durable evidence root tied to:

```text
WrapperDigestCommit
NewWrapperSHA256
HelperSHA256
FrozenRunnerSHA256
DeployedImageDigest=aad23c8...
FreshRunId(s)
```

Persist/index:

```text
three-role digest reconciliation
pre/post wrapper values
V1/V6/V8 results
complete local validation matrix
commit/push provenance
qualification preflight
Azure pre-state
fresh RunIds
mutation ledger
HTTP observations
application-owned persistence record
checkpoint/restoration evidence
D01-D19 records
carry-forward evidence
final target/repository invariants
manifest and hashes
```

Every D record:

```text
PredicateId
EvidenceClass
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
ReferenceResolved
```

Independently reopen.

Require:

```text
PredicateCount=19
FailedPredicateCount=0
UnresolvedPredicateEvidenceReferenceCount=0
UnresolvedEvidenceReferenceCount=0
MissingRequiredEvidenceClassCount=0
DurableReopen=PASS
```

---

## 18. Archive truth policy

Preserve:

```text
RETAINED + PASS|EMPTY                 eligible
RETAINED + FAIL|NOT_APPLICABLE       fail
RETRIEVAL_FAILED + NOT_APPLICABLE    eligible
other combinations                    inconsistent/fail
```

No Kudu `/home` retry.

---

## 19. Runtime bounded convergence

Evaluate all gates exhaustively.

If a defect is correctable entirely within disposable/local harness/evidence machinery:

```text
collect all failures
fix all in-scope disposable defects
invalidate affected candidate evidence
consume failed RunIds
allocate fresh RunIds as required
create fresh durable root
restart complete affected validation cycle
```

Continue internally until PASS.

### Hard stop boundaries

STOP if correction requires:

```text
tracked edit outside the already-authorized three-role wrapper rebind
endpoint/helper source edit
frozen runner mutation
image build/publication/deployment
new product/persistence/schema semantics
new Azure mutation category
WP07 action
paid resource
Twelve Data secret
PR/merge/lifecycle
new Luna decision
```

After an Azure-mutating failed attempt, perform canonical restoration before returning.

---

## 20. Mutation accounting

Authorized mutations:

```text
Git:
  one wrapper-file digest-role edit
  stage
  commit
  non-force push

Azure:
  canonical temporary qualification settings/H1 lifecycle only
```

Forbidden:

```text
Docker/GHCR
image deployment
README
PR/merge
issue/project/milestone lifecycle
WP05
WP07
```

Count actual mutations only.

---

## 21. Final state

Require:

```text
LocalHEAD == RemoteReleaseBranchTip == WrapperDigestCommit
WorkingTreeClean=true
StagedPaths=0
git diff --check=PASS

DeployedImageDigest=
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00

WrapperExpectedDigest == DeployedImageDigest
ExactIdentity == DeployedImageDigest
V1ExactResult=PASS
V6PartialDigestResult=PASS
V8Result=PASS

QualificationSettingsRestoredExactly=true
D01-D19=19/19 PASS_OR_ACCEPTED_CARRY_FORWARD
WP07AcceptanceActionCount=0
PRCount=0
MergeCount=0
WP04Issue263=OPEN
WP05=NOT_STARTED
WP07=NOT_STARTED
```

---

## 22. Success markers

Only on complete success emit:

```text
RELEASE 1.12 WP04 — THREE-VALUE DIGEST RECONCILIATION: PASS
RELEASE 1.12 WP04 — EXPECTED DIGEST REBIND: PASS
RELEASE 1.12 WP04 — EXACT IDENTITY POSITIVE FIXTURE: PASS
RELEASE 1.12 WP04 — V6 PARTIAL-DIGEST NEGATIVE FIXTURE: PASS
RELEASE 1.12 WP04 — WRAPPER DIGEST COMMIT/PUSH: PASS
RELEASE 1.12 WP04 — DEPLOYED IMAGE DIGEST MATCH: PASS
RELEASE 1.12 WP04 — IMAGE REBUILD: NOT_REQUIRED
RELEASE 1.12 WP04 — FRESH QUALIFICATION: PASS
RELEASE 1.12 WP04 — D01-D19: 19/19 PASS_OR_ACCEPTED_CARRY_FORWARD
RELEASE 1.12 WP04 — HTTP AUTHENTICATION/RUNID ATTRIBUTION: PASS
RELEASE 1.12 WP04 — SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — FINAL TARGET STATE: RESTORED
RELEASE 1.12 WP04 — WP07 ACCEPTANCE ACTIONS: 0
RELEASE 1.12 WP04 — PR/MERGE: DEFERRED_UNTIL_FINAL_ACCEPTANCE
RELEASE 1.12 WP04 — WP04 #263: OPEN
RELEASE 1.12 WP04 — SUBSTANTIVE ACCEPTANCE CANDIDATE: READY_FOR_FINAL_LUNA
RELEASE 1.12 WP04 — WP05: NOT_STARTED
RELEASE 1.12 WP04 — WP07: NOT_STARTED
```

Terra MUST NOT claim final WP04 acceptance.

---

## 23. Required handoff

Return:

```text
EntryHEAD
EntryRemoteTip
ExpectedDigestDefinition
ExactIdentityDefinition
V1ExactUsage
V6PartialDigestConstruction
V8ExactIdentityUsage

PreviousWrapperSHA256
NewWrapperSHA256
HelperSHA256

ExpectedDigestRebound
ExactIdentityRebound
V6PartialFixtureRebased
OnlyThreeLinkedDigestRolesChanged
ExactDigestValidationPreserved

RuntimeExpectedDigest
ExactIdentity
V6FixtureObserved
V6FixtureEqualsExpectedDigest

V1ExactResult
V6PartialDigestResult
V8Result
CurrentExactDigestPositiveCase
PartialDigestRejected
WrongDigestRejected
ValidationMatrix

WindowsPowerShellVersion
WrapperParserErrorCount
HelperParserErrorCount

WrapperDigestCommit
FinalRemoteTip
CommitChangedPathCount
CommitChangedPaths
PushResult

DeployedImageDigest
DigestMatchResult
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount

TargetValidationRoot
AllocatedRunIds
ConsumedFailedRunIds
AuthorizedAzureMutationCount
MutationLedger

D01Result
D02Result
D03Result
D04Result
D05Result
D06Result
D07Result
D08Result
D09Result
D10Result
D11Result
D12Result
D13Result
D14Result
D15Result
D16Result
D17Result
D18Result
D19Result

PredicateCount
FailedPredicateCount
UnresolvedPredicateEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
MissingRequiredEvidenceClassCount
DurableReopenResult

QualificationResult
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
WrapperExitCode

FinalWorkingTreeClean
FinalStagedPathCount
FinalGitDiffCheck
FinalTargetStateRestored

PRCount
MergeCount
GitHubLifecycleMutationCount
WP07AcceptanceActionCount
TwelveDataSecretConfigured

BoundaryBlocker
FinalAcceptance
NextAuthorizedAction
```

On success:

```text
FinalAcceptance=READY_FOR_FINAL_LUNA
NextAuthorizedAction=GPT-5.6 Luna final read-only WP04 substantive acceptance reconciliation; final PR/merge/lifecycle remain unauthorized
```

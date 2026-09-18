# Release 1.12 WP04 — Terra Helper Retry Publication & Final Target Cycle

**Selected execution model: GPT-5.6 Terra**

## Reconciliation

The fresh initialize attempt is **diagnostically successful but not yet final acceptance evidence**, because the helper bytes that produced the successful bounded-retry behavior remain tracked, unstaged, and uncommitted.

Successful consumed RunId:

```text
initialize-41d15357bc4747428f94a9e245d7a138
```

Observed recovery sequence:

```text
Attempt 1 = HTTP 503
Attempt 2 = transport Timeout
Attempt 3 = HTTP 200
Exact RunId evidence = returned
SchemaVersion = 4
JournalMode = delete
IntegrityCheck = ok
QuickCheck = ok
AcceptedEvidenceCount = 1
PersistenceContinuity = true
EvidenceCheckpoint = PASS
RestoreOnly = completed
FinalLifecycle = SUCCESS
TokenDisclosure = false
```

This proves the request path can recover after App Service convergence delay. However, the successful attempt ran with an uncommitted helper identity. Therefore:

```text
HelperRetryBehavior=PROVEN_CAPABILITY
FinalGovernedTargetAcceptanceCredit=DEFERRED_UNTIL_HELPER_PUBLICATION_AND_FRESH_CYCLE
RunId=PERMANENTLY_CONSUMED
```

Do not reuse `initialize-41d15357bc4747428f94a9e245d7a138`.

---

## Mission

1. Reconcile the current repository state and identify the exact helper-only bounded-retry diff.
2. Validate that the helper change only extends the already-governed retry behavior needed to survive transient HTTP 503/transport timeout during App Service convergence.
3. Commit and non-force push that exact helper change.
4. Re-establish a clean committed execution anchor and fresh helper SHA.
5. Run a **fresh complete WP04 target qualification cycle** with a new RunId and durable root.
6. Reconcile D01-D19 exhaustively.
7. Return READY_FOR_FINAL_LUNA only if all final evidence is attributable to the clean committed helper identity.

No image rebuild/republication/redeployment is required or authorized unless inspection proves the helper is runtime-image content. Under the canonical architecture it is deployment-side validation tooling; verify this before proceeding.

---

## Roles

```text
GPT-5.6 Luna
  contract/policy/architecture/governance/final acceptance

GPT-5.6 Terra
  implementation/validation/approved Git and temporary Azure mutations

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Terra**.

---

## Entry-state gate

Record:

```text
EntryHEAD
EntryRemoteTip
TrackedModifiedPaths
StagedPaths
UntrackedPaths
git diff --check
```

Expected:
- local HEAD == remote release branch tip;
- exactly one tracked modified path: `eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1`;
- staged paths = 0;
- diff-check PASS;
- wrapper clean relative to HEAD;
- endpoint clean relative to HEAD.

If additional tracked modifications exist, STOP before publication.

Record the current committed wrapper SHA and deployed image digest.

Expected deployed digest:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

---

## Helper retry-contract inspection

Inspect the entire helper diff.

The authorized semantic purpose is:

```text
survive bounded transient qualification-startup failures while App Service
converges after temporary qualification settings
```

The successful diagnostic sequence demonstrates the required class:

```text
503 → Timeout → 200
```

The helper may retry only within the existing bounded poll/qualification window.

Require:

```text
RetryIsBounded=true
TerminalSuccessRequiresHTTP200=true
TerminalSuccessRequiresExactRunIdEvidence=true
AuthenticationTokenRequirementPreserved=true
TokenDisclosureIntroduced=false
TimeoutDoesNotBecomeSuccess=true
503DoesNotBecomeSuccess=true
NonRetryableFailureStillFailsClosed=true
MaximumQualificationWindowNotUnbounded=true
PersistenceSemanticsChanged=false
EvidencePayloadContractChanged=false
AzureMutationSurfaceChanged=false
WP04WP07OwnershipChanged=false
```

The change must not convert failures into PASS. It only permits continued polling/retry for explicitly transient observations until the existing terminal success contract is actually met.

If the diff does more than this, STOP for Luna.

---

## Local validation and bounded convergence

Binding:

```text
Windows PowerShell 5.1.26100.9444
```

Run:
- helper AST parse;
- wrapper AST parse;
- helper retry positive tests;
- transient `503 → eventual 200` test;
- transient timeout → eventual 200 test;
- mixed `503 → timeout → 200` test if supported;
- terminal timeout/exhaustion negative test;
- terminal 503/exhaustion negative test;
- wrong RunId negative test;
- authentication/token negative test;
- applicable existing helper/wrapper validation matrix;
- `git diff --check`.

Require parser errors 0.

Evaluate all failures exhaustively.

Within this authority, Terra may correct defects **only in the same helper file and only within the bounded-retry semantics above**. For every byte change:
- invalidate helper candidate SHA;
- rerun the complete local validation matrix;
- continue until PASS.

Hard STOP if correction requires wrapper, endpoint, runtime source, schema/persistence semantics, another tracked path, image rebuild, new Azure mutation category, WP07, or Luna policy.

---

## Freeze helper identity

After local PASS:

```text
PreviousHelperSHA256=<committed helper SHA>
NewHelperSHA256=<final modified helper SHA>
WrapperSHA256=<current committed wrapper SHA>
```

Require:
- only helper is modified;
- staged 0;
- diff-check PASS.

---

## Commit and publication

Authorize exactly:

```text
git add eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
git commit
git push origin release/1.12-wp04-persistent-sqlite
```

Push MUST be non-force.

Commit purpose:

```text
Preserve bounded WP04 qualification polling across transient App Service convergence failures.
```

After push require:

```text
HelperRetryCommit=<new SHA>
LocalHEAD=HelperRetryCommit
RemoteReleaseBranchTip=HelperRetryCommit
CommitChangedPathCount=1
CommitChangedPath=verify-persistent-sqlite-webapp.ps1
WorkingTreeClean=true
StagedPaths=0
git diff --check=PASS
HelperWorkingTreeSHA256=HelperHeadSHA256=NewHelperSHA256
```

No PR/merge/lifecycle mutation.

---

## Image identity

Verify helper is not runtime-image content and the already deployed image remains:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

Require:

```text
DockerBuildCount=0
GhcrPublicationCount=0
ImageDeploymentMutationCount=0
```

If helper is actually copied into the runtime image, STOP before qualification and report `IMAGE_REBUILD_REQUIRED`.

---

## Fresh final preflight

Only from the clean committed helper anchor require:

```text
HEAD == remote release tip
working tree clean
staged=0
diff-check PASS
helper clean-HEAD PASS
wrapper clean-HEAD PASS
helper SHA == NewHelperSHA256
wrapper expected digest == deployed digest
deployed digest == aad23c8...fe07f00
PowerShell=5.1.26100.9444
parser errors=0
```

Only then allocate a new RunId.

The diagnostic RunId `initialize-41d15357bc4747428f94a9e245d7a138` is consumed and forbidden for reuse.

---

## Fresh governed qualification

Canonical boundary:

```text
WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
```

Execute only:

```text
initialize/reopen
canonical temporary qualification App Settings
H1 Deferred
application-owned HTTP evidence
durable checkpoint
RestoreOnly in finally
```

No redundant explicit restart.
No WP07 restart/recycle/redeploy acceptance.
No Twelve Data secret.
No direct SQLite/Python/Kudu `/home`.

Fresh helper-owned RB2 RunId(s) only.

The helper may observe/retry bounded transient 503/timeout states, but final success requires HTTP 200 plus exact fresh RunId evidence.

---

## D01-D19

Fresh:
`D01-D03, D05-D15, D17, D19`

Carry-forward/non-contradiction:
`D04, D16, D18`

```text
D01 app-owned persistence use case invoked
D02 canonical observation accepted/idempotently recognized
D03 app-owned reopen/readback unchanged
D04 conflict rejected/non-overwriting
D05 /home/data/aiquant.db
D06 schema v4
D07 journal DELETE
D08 integrity ok
D09 quick-check ok
D10 accepted identity/count correct
D11 HTTP evidence exact fresh RunId
D12 checkpoint before RestoreOnly
D13 RestoreOnly exactly once
D14 temporary settings restored
D15 token/secret hygiene
D16 no direct SQLite/Kudu/Python bypass
D17 F1/West Central US/$0 unchanged
D18 exact authorized mutation boundaries
D19 durable evidence reopens/all refs resolve
```

D11 must include the full fresh observation sequence and terminal exact-RunId evidence.

Transient 503/timeout observations are diagnostic observations, not failures, provided the bounded helper eventually reaches valid terminal evidence within contract.

---

## Durable evidence

Create a fresh durable root tied to:
- HelperRetryCommit;
- NewHelperSHA256;
- wrapper SHA;
- deployed image digest;
- fresh RunId(s).

Persist/index:
- diagnostic prior RunId as consumed/non-acceptance evidence;
- helper diff and retry-contract proof;
- local validation;
- commit/push provenance;
- fresh preflight;
- Azure pre-state;
- fresh poll observations;
- exact terminal HTTP evidence;
- application-owned persistence evidence;
- H1 checkpoint/restoration;
- D01-D19;
- carry-forward references;
- mutation ledger;
- final repo/target invariants;
- manifest/hashes.

Every predicate record:

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

## Runtime bounded convergence

Do not return after every correctable disposable defect.

For disposable evidence/harness defects:
- collect all failures;
- fix all in-scope disposable defects;
- invalidate affected evidence;
- consume failed RunIds;
- use fresh RunIds/root;
- rerun the complete affected target cycle until PASS.

Hard STOP for:
- tracked source/helper change after publication;
- wrapper/endpoint edit;
- image rebuild/publication/deployment;
- new Azure mutation category;
- WP07 operation;
- paid resource;
- Twelve Data secret;
- PR/merge/lifecycle;
- Luna decision.

Always restore temporary settings after an Azure-mutating failed attempt.

---

## Mutation accounting

Authorized:

```text
Git:
  helper-only edit/convergence
  stage
  commit
  non-force push

Azure:
  canonical temporary qualification settings/H1 lifecycle
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

## Success markers

Only on complete success:

```text
RELEASE 1.12 WP04 — BOUNDED RETRY HELPER CONTRACT: PASS
RELEASE 1.12 WP04 — HELPER RETRY COMMIT/PUSH: PASS
RELEASE 1.12 WP04 — CLEAN COMMITTED HELPER IDENTITY: PASS
RELEASE 1.12 WP04 — FRESH QUALIFICATION: PASS
RELEASE 1.12 WP04 — TRANSIENT STARTUP RECOVERY: PASS
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

Terra must not claim final WP04 acceptance.

---

## Required handoff

Return:

```text
EntryHEAD
EntryRemoteTip
EntryModifiedPaths
DiagnosticConsumedRunId

RetryDiffPurpose
RetryIsBounded
TerminalSuccessRequiresHTTP200
TerminalSuccessRequiresExactRunIdEvidence
AuthenticationTokenRequirementPreserved
TokenDisclosureIntroduced
NonRetryableFailureStillFailsClosed
MaximumQualificationWindowNotUnbounded

PreviousHelperSHA256
NewHelperSHA256
WrapperSHA256
WindowsPowerShellVersion
WrapperParserErrorCount
HelperParserErrorCount
LocalValidationMatrix

HelperRetryCommit
FinalRemoteTip
CommitChangedPathCount
CommitChangedPaths
PushResult

DeployedImageDigest
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount

TargetValidationRoot
AllocatedRunIds
ConsumedFailedRunIds
PollObservationSequence
TerminalHttpStatus
TerminalRunIdAttribution

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

AuthorizedAzureMutationCount
MutationLedger
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

# Release 1.12 WP04 — Terra Two-Occurrence Digest Rebind, Publication & Fresh Qualification

**Selected execution model: GPT-5.6 Terra**

## Decision

The two occurrences have different roles and MUST NOT be bulk-replaced.

```text
Line 11  = production/runtime expected-image preflight identity
Line 226 = local negative-test fixture V6-partial-digest
```

Canonical decision:

```text
RuntimeExpectedDigest=UPDATE_TO_NEW_DEPLOYED_DIGEST
V6PartialDigestFixture=REBASE_TO_NEW_EXPECTED_DIGEST_DERIVATION
HistoricalOldDigestLiteral=REMOVE_IF_NO_LONGER_REQUIRED_BY_TEST_SEMANTICS
BulkReplace=FORBIDDEN
```

The V6 fixture is not historical evidence. It is an executable negative test intended to prove that a partial/truncated digest does NOT satisfy the wrapper's current exact-digest contract. Therefore it must remain semantically derived from the **current expected digest**, not remain coupled to the superseded image identity.

If inspection proves line 226 intentionally tests a fixed historical literal for a materially different reason, STOP for Luna rather than applying this decision.

---

## Mission

From clean anchor:

```text
13b085fec8af4b746b8ff0579b611451e8429a27
```

perform one narrowly governed wrapper update so:

1. `$expectedDigest` equals the currently deployed immutable authentication-fix image:
   `sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00`
2. `V6-partial-digest` remains a valid negative fixture against that current expected digest;
3. no other wrapper semantics change;
4. validate the wrapper;
5. commit and non-force push the one-file change;
6. without another handoff, execute fresh WP04 qualification and D01-D19.

No image rebuild/republication/redeployment is required or authorized.

---

## 1. Roles

```text
GPT-5.6 Luna  contract/policy/architecture/governance/final acceptance
GPT-5.6 Terra implementation/validation/approved Git and temporary Azure mutations
GPT-5.6 Sol   supporting analysis only
```

Selected model: **GPT-5.6 Terra**.

---

## 2. Entry gate

Require before edit:

```text
HEAD=13b085fec8af4b746b8ff0579b611451e8429a27
origin/release/1.12-wp04-persistent-sqlite=13b085fec8af4b746b8ff0579b611451e8429a27
tracked modifications=0
staged paths=0
git diff --check=PASS
deployed image digest=sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

PowerShell:

```text
Windows PowerShell 5.1.26100.9444
```

Before editing, inspect and record every occurrence of:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Expected:

```text
OldDigestOccurrenceCount=2
Occurrence1Role=RuntimeExpectedDigest
Occurrence2Role=V6PartialDigestNegativeFixture
```

If count or roles differ, STOP.

---

## 3. Exact one-file edit authority

Only tracked path authorized:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

### Runtime occurrence

Change:

```text
$expectedDigest = 'sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f'
```

to:

```text
$expectedDigest = 'sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00'
```

### V6-partial-digest fixture

Preserve the fixture's purpose:

```text
a deliberately incomplete/partial digest must fail exact digest validation
```

Rebase the fixture to the new current expected digest using the existing fixture construction style.

Preferred semantics:

```text
V6 input is a proper prefix/truncation or otherwise intentionally partial form
of sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
and remains != $expectedDigest.
```

Do NOT replace the fixture with the full new digest.

Do NOT leave it tied to the old digest merely as historical data.

Do NOT weaken the exact equality check.

Do NOT alter any other V fixture semantics.

---

## 4. Post-edit semantic gates

Require:

```text
ChangedPathCount=1
ChangedPath=initialize-qualification.ps1
RuntimeExpectedDigest=new exact digest
V6FixtureReferencesCurrentDigestSemantics=true
V6FixtureIsPartialOrInvalid=true
V6FixtureEqualsExpectedDigest=false
ExactDigestValidationStillRequired=true
```

Also require:

```text
PersistenceSemanticsChanged=false
AuthenticationSemanticsChanged=false
EvidencePayloadContractChanged=false
AzureMutationSurfaceChanged=false
WP04WP07OwnershipChanged=false
```

Inspect the complete diff. No unrelated byte/semantic change.

The old full digest may remain zero times after this edit. That is expected if it had no independent historical-test purpose.

---

## 5. Local validation

Run exhaustively:

```text
PowerShell AST parse
existing wrapper local validation
V6-partial-digest negative test
all digest/provenance positive and negative cases
helper compatibility checks
git diff --check
```

Explicitly prove:

```text
CurrentExactDigestPositiveCase=PASS
V6PartialDigestRejected=PASS
WrongDigestRejected=PASS
WrapperParserErrors=0
```

Run any established WP04 local validation suite required by the current publication gate.

No Docker build.

If any failure requires anything beyond this one wrapper file or beyond the two digest-related semantics above, STOP.

Within those exact semantics, Terra may correct the wrapper and rerun the entire local validation set until PASS.

Every byte-changing correction invalidates the prior candidate SHA.

---

## 6. Freeze new wrapper identity

After all local gates PASS:

```text
PreviousWrapperSHA256=<compute from 13b085f HEAD>
NewWrapperSHA256=<compute from final candidate>
HelperSHA256=5ED81167F81287AC2614546C063C938F8F747DE664190CF95BF0E2D125C9B690
```

Require helper unchanged and clean.

Record the final V6 fixture value/derivation in sanitized evidence.

---

## 7. Commit and non-force push

Authorize:

```text
git add eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
git commit
git push origin release/1.12-wp04-persistent-sqlite
```

Commit purpose:

```text
Bind WP04 qualification digest validation to authentication-fix image.
```

Require:

```text
CommitChangedPathCount=1
LocalHEAD=RemoteReleaseBranchTip=<new commit>
working tree clean
staged paths=0
git diff --check=PASS
```

No force push.
No PR/merge.

---

## 8. No image mutation

Require exactly:

```text
DockerBuildCount=0
GhcrPublicationCount=0
ImageDeploymentMutationCount=0
```

Read-only verify deployed image remains:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

Wrapper expected digest must exactly equal it.

---

## 9. Fresh qualification preflight

From the new committed anchor require:

```text
HEAD == remote release tip
working tree clean
staged=0
diff-check PASS
wrapper working-tree SHA == HEAD SHA == NewWrapperSHA256
helper SHA == accepted helper SHA
wrapper/helper clean-HEAD PASS
wrapper expected digest == deployed digest
PowerShell 5.1.26100.9444
parser errors=0
```

Frozen runner:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Only then allocate a fresh helper-owned RunId.

---

## 10. WP04 qualification boundary

Binding:

```text
WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
```

Execute only:

```text
initialize/reopen
temporary qualification App Settings
H1 Deferred
application-owned HTTP evidence
durable checkpoint
RestoreOnly in finally
```

No explicit redundant restart.
No WP07 restart/recycle/redeploy acceptance.
No Twelve Data secret.
No direct SQLite/Python/Kudu `/home`.

Endpoint requires exact fresh RunId and evidence token.

---

## 11. D01-D19

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
D18 exact mutation boundary
D19 durable evidence reopen/all refs resolved
```

D11 must prove the deployed authentication-path image succeeds through the governed wrapper.

D15 must remain PASS.

---

## 12. Durable evidence

Create a fresh root tied to:
- new wrapper commit;
- NewWrapperSHA256;
- helper/frozen identities;
- deployed `aad23c8...` digest;
- fresh RunId(s).

Persist the two-occurrence reconciliation, V6 negative-test evidence, local validation, commit/push, preflight, qualification, H1 restoration, D01-D19, mutation ledger, final invariants, manifest/hashes.

Every predicate record must include:

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

Independently reopen and require:

```text
PredicateCount=19
FailedPredicateCount=0
UnresolvedPredicateEvidenceReferenceCount=0
UnresolvedEvidenceReferenceCount=0
MissingRequiredEvidenceClassCount=0
DurableReopen=PASS
```

---

## 13. Bounded convergence

Do not return after each disposable defect.

For local evidence/harness defects:
collect all failures → correct all in-scope disposable defects → invalidate evidence → consume failed RunIds → fresh RunId/root → rerun complete affected cycle until PASS.

Hard STOP for:
- tracked edit outside wrapper;
- wrapper semantic edit beyond digest binding/V6 fixture;
- endpoint/helper edit;
- frozen-runner change;
- image build/publish/deploy;
- new Azure mutation category;
- WP07 action;
- paid resource;
- Twelve Data secret;
- PR/merge/lifecycle;
- Luna decision.

Always restore temporary Azure settings after an Azure-mutating failed attempt.

---

## 14. Mutation accounting

Authorized new mutations:

```text
Git:
  one wrapper edit
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
GitHub lifecycle
WP05
WP07
```

Ledger every actual mutation.

---

## 15. Success markers

Only on full success:

```text
RELEASE 1.12 WP04 — TWO-OCCURRENCE DIGEST RECONCILIATION: PASS
RELEASE 1.12 WP04 — V6 PARTIAL-DIGEST NEGATIVE FIXTURE: PASS
RELEASE 1.12 WP04 — WRAPPER IMAGE-DIGEST REBIND: PASS
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

---

## 16. Required handoff

Return:

```text
EntryHEAD
EntryRemoteTip
OldDigestOccurrenceCount
Occurrence1Role
Occurrence2Role
V6FixtureDecision
PreviousWrapperSHA256
NewWrapperSHA256
HelperSHA256

OnlyAuthorizedDigestSemanticsChanged
RuntimeExpectedDigest
V6FixtureObserved
V6FixtureEqualsExpectedDigest
CurrentExactDigestPositiveCase
V6PartialDigestRejected
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
WrapperExpectedDigest
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

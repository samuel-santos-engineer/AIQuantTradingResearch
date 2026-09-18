# Release 1.12 WP04 — Terra Authentication-Path Fix Publication, Image Rebuild, Deployment & Fresh Validation

**Selected execution model: GPT-5.6 Terra**

## Mission

Publish and validate the already-introduced WP04 authentication/request-path fix that changed exactly:

```text
src/.../PersistentSqliteQualificationEvidenceEndpoint.cs
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

IMPORTANT: resolve the exact repository path of `PersistentSqliteQualificationEvidenceEndpoint.cs` from Git before staging; do not guess it.

The prior target-validation authority anchored to:

```text
3e42a776ef77dcfab8fbbd39f8e7f51e777eadc8
```

is superseded because tracked production/helper bytes changed.

This authority intentionally combines, within one governance boundary:

```text
source-fix validation
→ exact two-file commit
→ non-force push
→ new runtime image build
→ GHCR publication
→ Azure deployment of that exact image
→ fresh WP04 qualification
→ D01-D19 reconciliation
```

Do not return between these phases for defects correctable within this authority. Evaluate gates exhaustively and converge internally.

This authority does NOT authorize:
- PR creation/merge;
- #263 closure or Project Done;
- milestone mutation;
- WP05;
- WP07 restart/recycle/redeploy qualification;
- unrelated source changes.

---

## 1. Model roles

```text
GPT-5.6 Luna
  contract/policy/architecture/governance/final acceptance

GPT-5.6 Terra
  implementation validation, approved Git/GHCR/Azure mutations, governed runtime execution

GPT-5.6 Sol
  supporting analysis only
```

Selected model: **GPT-5.6 Terra**.

---

## 2. Entry-state reconciliation

Expected committed base before the two current tracked edits:

```text
3e42a776ef77dcfab8fbbd39f8e7f51e777eadc8
```

Before any mutation, record:

```text
EntryHEAD
RemoteReleaseBranchTip
CurrentBranch
git status --short
staged paths
tracked modified paths
untracked paths
git diff --check
```

Require:
- current branch is `release/1.12-wp04-persistent-sqlite`;
- `EntryHEAD=3e42a776ef77dcfab8fbbd39f8e7f51e777eadc8`;
- remote release branch tip equals EntryHEAD;
- staged paths = 0;
- tracked modifications are exactly the endpoint file and helper file identified above;
- no unrelated tracked modification;
- `git diff --check=PASS`.

If the entry state differs, STOP before commit/build/Azure and report the complete mismatch matrix.

---

## 3. Exact change-scope inspection

Read the two-file diff and classify each change.

Return:

```text
EndpointExactPath
HelperExactPath
EndpointFixPurpose
HelperFixPurpose
AuthenticationPathFailureBeingCorrected
RequestPathBefore
RequestPathAfter
```

Prove the changes are narrowly limited to the qualification evidence authentication/request path.

Require:

```text
PersistenceSemanticsChanged=false
SchemaChanged=false
DatabasePathChanged=false
JournalPolicyChanged=false
EvidencePayloadContractChanged=false
WP04WP07OwnershipChanged=false
NormalRuntimeAuthenticationChanged=false
AzureMutationSurfaceChanged=false
```

The endpoint may change how the already-governed qualification evidence token/request is accepted/validated, but must not broaden the endpoint beyond qualification-mode activation or weaken token enforcement.

Explicitly prove:

```text
QualificationEndpointStillRequiresExplicitActivation=true
QualificationEndpointStillRequiresEvidenceToken=true
QualificationEndpointStillRequiresExactRunId=true
QualificationTokenStillNeverPersistedOrPrinted=true
```

If any contract above cannot be proven, STOP for Luna before publication.

---

## 4. PowerShell and source validation

Binding PowerShell:

```text
Windows PowerShell 5.1.26100.9444
```

Require:
- helper AST parser errors = 0;
- wrapper AST parser errors = 0;
- applicable helper/wrapper local validation PASS;
- endpoint/application tests PASS;
- targeted qualification tests PASS;
- Domain/Application/Architecture/Infrastructure established WP04 suites PASS;
- Release build PASS, 0 warnings, 0 errors;
- signing contract preserved.

Signing:

```text
Release Authenticode signature not required
Debug local signing required where Debug tests are invoked
Expected signer CN=AIQuantTradingDev
No signing bypass
```

Evaluate all validation failures before deciding whether publication can proceed.

### In-authority source convergence

This authority authorizes corrections ONLY within these same two files when necessary to make the already-defined authentication-path fix satisfy the existing contract:

```text
PersistentSqliteQualificationEvidenceEndpoint.cs
verify-persistent-sqlite-webapp.ps1
```

No third tracked path may change.

For each byte-changing correction:
- invalidate previous hashes/validation;
- rerun the complete source validation set;
- recompute exact hashes;
- continue until PASS.

STOP if correction requires product/persistence semantics, schema, wrapper changes, another tracked path, WP07, or Luna policy.

---

## 5. Final source candidate identity

After local convergence and before staging record:

```text
EndpointSHA256
HelperSHA256
WrapperSHA256
```

Wrapper was not authorized to change in this fix; therefore prove wrapper bytes equal committed EntryHEAD wrapper bytes.

Require:
- exactly two tracked modified paths;
- staged paths 0;
- diff-check PASS;
- all tests/build PASS.

Freeze these exact candidate hashes for downstream publication/build/runtime attribution.

---

## 6. Git publication authority

Authorize exactly:

```text
git add <exact endpoint path> <exact helper path>
git commit
git push origin release/1.12-wp04-persistent-sqlite
```

Push must be non-force.

Commit purpose:

```text
Fix governed WP04 qualification evidence authentication/request path.
```

No README mutation.

No PR or merge.

After push require:

```text
LocalHEAD=<new commit>
RemoteReleaseBranchTip=<same new commit>
CommitChangedPathCount=2
CommitChangedPaths=<endpoint,helper>
working tree clean
staged paths=0
git diff --check=PASS
```

If commit/push fails for a correctable local/transient reason that does not require scope expansion, diagnose and retry within this authority. Never force-push.

---

## 7. Runtime image rebuild requirement

Because `PersistentSqliteQualificationEvidenceEndpoint.cs` is runtime source, the old image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

is superseded for fresh target validation.

Build a new image from the exact new committed source anchor.

Require reproducible attribution:

```text
ImageSourceCommit=<new commit SHA>
EndpointSHA256=<frozen candidate hash>
HelperSHA256=<frozen candidate hash>
WrapperSHA256=<frozen unchanged hash>
```

Use the existing governed Release 1.12 Docker/GHCR publication mechanism and public/free GHCR architecture.

Do not alter registry credentials.

No paid registry/service.

Record:
- image tag(s);
- immutable published digest;
- build result;
- publication result.

If build requires tracked-source change outside the two authorized files, STOP.

---

## 8. Azure deployment authority

Deploy only the newly published immutable image digest to the existing governed Release 1.12 reference web app.

Before deployment capture sanitized pre-state.

Authorized deployment mutation:

```text
update existing web app image identity to the new exact immutable digest
```

This deployment is necessary to make runtime source match the new committed endpoint fix.

Do NOT perform separate restart/recycle/redeploy persistence testing. The image update's platform restart is an implementation deployment consequence, not WP07 acceptance evidence and receives no WP07 credit.

Require architecture unchanged:

```text
App Service Linux F1
West Central US
custom Docker
public/free GHCR
persistent /home
$0.00 recurring infrastructure cost
no Azure SQL
no Azure Files
no ACR requirement
```

Record the deployed digest and verify it equals the newly published digest before qualification.

---

## 9. Fresh qualification preflight

After deployment stabilizes, re-run the full structural preflight against the new committed anchor.

Require:
- local HEAD == remote release tip == new commit;
- working tree clean;
- staged paths 0;
- diff-check PASS;
- wrapper/helper clean-HEAD PASS;
- endpoint/helper/wrapper hashes equal frozen post-fix hashes;
- deployed image digest equals new published digest;
- PowerShell 5.1.26100.9444;
- parser errors 0;
- sanitized Azure pre-state captured.

Only then allocate fresh RunId(s).

Never reuse any historical RunId.

---

## 10. WP04 qualification lifecycle

Canonical ownership remains:

```text
WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
```

Execute only:

```text
initialize/reopen
H1 Deferred
application-owned HTTP evidence
durable checkpoint
RestoreOnly in finally
```

M1: no redundant explicit restart.

Evidence endpoint:
`GET /internal/wp04/persistence-qualification?runId=<fresh RunId>`
with `X-WP04-Evidence-Token`.

No direct SQLite, Python DB access, or Kudu `/home`.

No Twelve Data secret configuration.

Normal-runtime missing Twelve Data key remains a WP05 downstream blocker.

---

## 11. D01-D19

Fresh target:
D01-D03, D05-D15, D17, D19.

Carry-forward with non-contradiction:
D04, D16, D18.

Definitions:

```text
D01 application-owned persistence use case invoked
D02 canonical observation accepted/idempotently recognized
D03 application-owned reopen/readback unchanged
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
D14 temporary qualification settings restored
D15 token/secret hygiene
D16 no direct SQLite/Kudu/Python bypass
D17 F1/West Central US/$0 unchanged
D18 mutation boundaries exactly authorized
D19 durable evidence reopens/all refs resolve
```

The authentication-path fix must be specifically evidenced by successful D11 without weakening D15.

No fresh Azure conflict operation for D04.

---

## 12. Durable evidence

Create a fresh durable root after the final source/image identities are fixed.

Persist/index:
- authority identity;
- entry state and two-file diff;
- source convergence ledger;
- final commit and remote tip;
- endpoint/helper/wrapper hashes;
- build/test evidence;
- image tag/digest/source commit;
- Azure pre/deployment/post state;
- fresh RunIds;
- qualification HTTP observations;
- application-owned record;
- H1 checkpoint/restoration;
- D01-D19 records;
- carry-forward references;
- mutation ledger;
- final repo/target invariants;
- manifest/hashes.

Every D record must include:

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
- PredicateCount=19;
- FailedPredicateCount=0;
- unresolved predicate refs=0;
- unresolved evidence refs=0;
- missing evidence classes=0.

---

## 13. Archive truth policy

Preserve:

```text
RETAINED + PASS|EMPTY                 eligible
RETAINED + FAIL|NOT_APPLICABLE       fail
RETRIEVAL_FAILED + NOT_APPLICABLE    eligible
other combinations                    fail/inconsistent
```

No Kudu `/home` retry.

---

## 14. Exhaustive convergence

Do not return after each correctable defect.

### Source phase
Within the two authorized files only:
collect all failures → fix all in-scope issues → fresh hashes → rerun complete local validation until PASS.

### Build/publication phase
For disposable/build-command issues that do not change tracked source:
diagnose → correct → rebuild from the same committed source → republish as needed → freeze final immutable digest.

### Runtime phase
For disposable local evidence/harness defects:
collect all failures → fix in-scope disposable defects → consume failed RunIds → fresh root/RunIds → rerun full affected qualification cycle until PASS.

### Hard stops
STOP if correction requires:
- a third tracked path;
- wrapper edit;
- schema/persistence/product semantic change;
- frozen-runner mutation;
- new Azure mutation category;
- WP07 acceptance operation;
- paid resource;
- Twelve Data secret;
- PR/merge/lifecycle;
- Luna policy decision.

After any Azure-mutating failure, restore canonical temporary settings before returning.

---

## 15. Mutation accounting

Maintain exact ledgers.

Authorized Git mutations:
- stage two files;
- one or more local corrective commits only if required by in-scope convergence, but prefer one final commit;
- non-force push.

Authorized external mutations:
- GHCR publish new image;
- existing web-app image update;
- canonical temporary qualification settings/H1 lifecycle.

Count only actual mutations.

Do not attribute user/manual actions to Terra.

Forbidden:
- PR/merge;
- #263 closure;
- Project Done;
- milestone mutation;
- WP05/WP07;
- README mutation.

---

## 16. Final state

Require:
- source commit pushed non-force;
- remote tip == local HEAD;
- working tree clean/staged 0/diff-check PASS;
- deployed image == final new immutable digest;
- qualification settings restored exactly;
- D01-D19 all PASS/carry-forward;
- D11 proves fixed authentication/request path;
- D15 secret hygiene PASS;
- WP07 lifecycle acceptance actions=0;
- README mutations=0;
- PR/merge=0;
- #263 remains OPEN.

Do not roll back the new application image after successful validation; it is the candidate image for final WP04 publication unless Luna later decides otherwise.

---

## 17. Success markers

Only on full success:

```text
RELEASE 1.12 WP04 — AUTHENTICATION-PATH FIX SOURCE VALIDATION: PASS
RELEASE 1.12 WP04 — AUTHENTICATION-PATH FIX COMMIT/PUSH: PASS
RELEASE 1.12 WP04 — NEW IMAGE BUILD/PUBLICATION: PASS
RELEASE 1.12 WP04 — NEW IMAGE DEPLOYMENT: PASS
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

Terra must not claim final WP04 acceptance.

---

## 18. Required handoff

Return:

```text
EntryHEAD
EntryRemoteTip
EndpointExactPath
HelperExactPath
EntryModifiedPaths

EndpointFixPurpose
HelperFixPurpose
AuthenticationPathFailureBeingCorrected
PersistenceSemanticsChanged
SchemaChanged
EvidencePayloadContractChanged
WP04WP07OwnershipChanged
QualificationEndpointStillRequiresExplicitActivation
QualificationEndpointStillRequiresEvidenceToken
QualificationEndpointStillRequiresExactRunId
QualificationTokenStillNeverPersistedOrPrinted

WindowsPowerShellVersion
WrapperParserErrorCount
HelperParserErrorCount
ValidationMatrix
ReleaseBuildResult
ReleaseBuildWarnings
ReleaseBuildErrors
SigningContractResult

FinalSourceCommit
FinalRemoteTip
CommitChangedPathCount
CommitChangedPaths
EndpointSHA256
HelperSHA256
WrapperSHA256

ImageSourceCommit
ImageTag
PublishedImageDigest
DockerBuildCount
GhcrPublicationCount
DeploymentImageDigest
ImageDeploymentMutationCount

TargetValidationRoot
AllocatedRunIds
ConsumedFailedRunIds
AuthorizedAzureMutationCount
AzureMutationLedger

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
ReadmeMutationCount
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

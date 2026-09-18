# Release 1.12 WP04 — Terra Converge-to-Completion Evidence-Retention-First Authority

**Selected execution model: GPT-5.6 Terra**

## 0. Intent

This authority is deliberately designed to avoid repeated Codex handoffs.

Terra SHALL continue internally through every correction/retry that remains inside the authorized WP04 evidence/runtime boundary until either:

```text
A. WP04 substantive acceptance evidence is COMPLETE and ready for Luna; or
B. a true governance/security/source boundary is reached that Terra is not authorized to cross.
```

Do **not** return merely because an individual disposable harness/evidence defect, transient App Service condition, missing manifest row, broken evidence reference, or failed fresh RunId occurs.

Collect failures exhaustively, correct all in-scope defects, consume failed RunIds, create fresh roots/RunIds as necessary, and repeat the complete affected cycle.

---

# 1. Model authority

```text
GPT-5.6 Luna
  contract, policy, architecture, governance, final substantive acceptance

GPT-5.6 Terra
  selected here:
  governed runtime validation
  evidence-retention implementation
  disposable/local harness correction
  canonical temporary Azure qualification mutations
  evidence reconstruction/validation

GPT-5.6 Sol
  supporting analysis only
```

Selected execution model: **GPT-5.6 Terra**.

Terra MUST NOT itself emit Luna's final substantive acceptance marker.

---

# 2. Luna decision being executed

Binding:

```text
D14Decision=RERUN_REQUIRED
D18Decision=RERUN_REQUIRED
D19Decision=FRESH_RUNTIME_REQUIRED

RELEASE 1.12 WP04 — D14/D18 RETAINED EVIDENCE: INSUFFICIENT
RELEASE 1.12 WP04 — FRESH GOVERNED TARGET CYCLE: REQUIRED
```

Reason:

```text
D14:
  prior summaries report reopen restoration/final setting count,
  but no attributable retained reopen restoration-completion artifact

D18:
  Git publication provenance verified,
  but no complete final-cycle mutation ledger proving reopen restoration
  and required zero-mutation categories

D19:
  cannot pass until D14/D18 and all durable references are complete
```

Therefore a new evidence-retention-first initialize+reopen cycle is authorized.

---

# 3. Immutable/current execution identities

Read-only verify before any RunId allocation.

Expected final source/helper branch tip:

```text
e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
```

Expected wrapper SHA256:

```text
2DF9173EAE15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
```

Expected helper SHA256:

```text
7916106C11BEF92251EE49C7E0C0650744BE728CD42EE4071BF9D33E013F0541
```

Expected deployed image:

```text
sha256:aad23c8bc65529ad6500aaa5fde36969df239268b992e85f50211b814fe07f00
```

Binding PowerShell:

```text
Windows PowerShell 5.1.26100.9444
```

Frozen runner SHA256:

```text
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
```

Require:

```text
HEAD == expected full commit
origin release tip == expected full commit
working tree clean
staged paths = 0
git diff --check = PASS
wrapper SHA == expected
helper SHA == expected
deployed image == expected
```

If live remote access transiently fails, retry read-only verification boundedly and also inspect cached remote ref. Do not mutate merely to verify.

A true identity mismatch is a HARD STOP.

---

# 4. Permanently consumed RunIds

Never reuse any historical RunId.

In particular:

```text
initialize-41d15357bc4747428f94a9e245d7a138
initialize-c13b371cfb8443cfb8289dd426c13bc4
reopen-c471772b41b942ecb0ee9bc043122fde
```

All previously recorded WP04/W5-W8 RunIds remain consumed.

Every new RunId allocated under this authority becomes permanently consumed immediately, whether its attempt succeeds or fails.

---

# 5. Canonical ownership

Binding:

```text
CanonicalOwnershipDecision=WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY
WP04_DEPLOYMENT_RECOVERY_REQUIRED_FOR_CLOSURE=false
WP07_DEPLOYMENT_RECOVERY_REQUIRED_FOR_RELEASE_ACCEPTANCE=true
```

This authority MUST NOT perform:

```text
Azure restart persistence acceptance
Azure recycle persistence acceptance
Azure redeploy persistence acceptance
WP07 deployment recovery
```

WP04 recovery here means:

```text
application-owned initialize/update/reopen/readback
durable checkpointing
temporary qualification-setting restoration
schema/journal/integrity continuity
```

---

# 6. Evidence-retention-first design gate

Before the fresh initialize RunId is allocated, establish a NEW durable root.

It must be unique and attributable to:

```text
full 40-char commit
wrapper SHA256
helper SHA256
frozen runner SHA256
deployed image digest
authority identity
cycle identifier
```

Example:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\target-validation-final\<fresh-guid>
```

Do not overwrite historical roots.

Create the evidence schema/index **before** runtime execution.

Required evidence classes:

```text
E01 preflight identity
E02 repository state
E03 Azure architecture/read-only state
E04 per-phase pre-state
E05 per-phase authorized mutation request/result
E06 poll observations
E07 terminal exact-RunId HTTP evidence
E08 application persistence record
E09 checkpoint-completion evidence
E10 RestoreOnly invocation
E11 RestoreOnly completion/result
E12 post-restoration App Settings state
E13 pre-state/post-state equality
E14 final-cycle mutation ledger
E15 zero-mutation-category observations
E16 D01-D19 ledger
E17 carry-forward evidence index
E18 repository/external boundary evidence
E19 final target state
E20 complete manifest/hashes
```

The cycle MUST NOT begin until the retention mechanism can persist these classes.

---

# 7. D14 mandatory capture — per phase

For BOTH initialize and reopen, durably retain:

```text
Phase
RunId
PreStateArtifact
DeferredSettingsMutationArtifact
CheckpointArtifact
RestoreOnlyInvocationArtifact
RestoreOnlyCompletionArtifact
PostRestoreSettingsArtifact
PreStatePostStateComparisonArtifact
FinalTemporaryQualificationSettingCount
```

D14 acceptance requires especially for **reopen**:

```text
RestoreOnlyInvoked=true
RestoreOnlyCount=1
RestoreOnlyCompleted=true
RestoreOnlyResult=SUCCESS
PostRestoreTemporaryQualificationSettingCount=0
RestoredStateEqualsCapturedPreState=true
FinalTargetStateRestored=true
```

Do not infer any of these from wrapper exit code alone.

Every assertion must reference a retained artifact.

---

# 8. D18 mandatory mutation ledger

Create the ledger prospectively as mutations/read-only zero-category observations occur.

Every row:

```text
MutationId
Timestamp
Phase
Category
Operation
Target
RunIdOrCommit
Authorization
Expected
Observed
Result
EvidenceReference
ReferenceResolved
```

Positive mutation classes expected in this fresh cycle:

```text
initialize temporary qualification-setting application
initialize RestoreOnly restoration
reopen temporary qualification-setting application
reopen RestoreOnly restoration
```

Do not count the historical helper commit as a new mutation; reference it as provenance.

Required zero-mutation classes must have explicit observation evidence:

```text
TrackedSourceMutation=0
GitCommit=0
GitPush=0
DockerBuild=0
GhcrPublication=0
ImageDeployment=0
ExplicitAzureRestart=0
WP07RestartRecycleRedeploy=0
PR=0
Merge=0
GitHubIssueMutation=0
ProjectMutation=0
MilestoneMutation=0
TwelveDataSecretConfiguration=0
PaidResourceCreation=0
DirectSQLiteBypass=0
PythonDatabaseBypass=0
KuduHomeBypass=0
```

Do not infer a required zero merely from missing logs.

Record the actual observation/validation basis.

---

# 9. Fresh initialize phase

Allocate a fresh helper-owned RB2 RunId only after all preflight/retention gates pass.

Execute canonical:

```text
H1 Deferred
→ application-owned qualification
→ bounded HTTP polling
→ exact fresh RunId terminal evidence
→ durable checkpoint
→ RestoreOnly in finally
→ durable restoration proof
```

No explicit restart.

Expected transient recovery may include:

```text
503
transport Timeout
```

These are observations, not success.

Terminal success requires:

```text
HTTP 200
exact fresh RunId
schema=4
journal=delete
integrity=ok
quick-check=ok
accepted evidence count=1
persistence continuity=true
token disclosure=false
```

Persist every poll observation.

If a transient failure occurs within the governed bounded polling window, continue internally.

If the phase fails for a disposable/evidence defect, restore state, consume RunId, repair in-scope defect, allocate a fresh RunId/root lineage, and rerun.

---

# 10. Fresh reopen phase

Only after initialize has:
- terminal evidence PASS;
- checkpoint PASS;
- RestoreOnly completion PASS;
- pre/post restoration equality PASS;
- durable artifact references PASS.

Allocate a fresh reopen RunId.

Execute the same evidence-retention-first sequence.

The reopen phase MUST independently retain its restoration-completion evidence.

Terminal evidence requires the same exact contract and persistence continuity.

The reopen phase is the decisive missing D14/D18 boundary from the prior cycle.

---

# 11. Application persistence acceptance

For the fresh cycle prove:

```text
D01 initialize invokes application-owned persistence use case
D02 canonical observation accepted/idempotently recognized
D03 canonical observation read back unchanged
```

Canonical path:

```text
PersistentSqliteQualificationExecution
→ PersistHistoricalObservationsUseCase.Execute(...)
→ IHistoricalObservationStore.Persist(...)
```

No direct SQLite shell.
No Python DB access.
No Kudu `/home`.

Binding existing semantics:

```text
GovernedDataUpdate=EXISTING_SEMANTICS
Fidelity=EXISTING_SEMANTICS
Idempotency=EXISTING_SEMANTICS
Conflict=EXISTING_SEMANTICS
```

D04 conflict remains accepted carry-forward unless current evidence contradicts it.

---

# 12. D01-D19 ledger

Construct the 19-row ledger DURING the cycle, not afterward.

Every row:

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
FreshOrCarryForward
```

Definitions:

```text
D01 initialize invokes application-owned persistence use case
D02 canonical observation accepted/idempotently recognized
D03 canonical observation read back unchanged
D04 conflicting evidence rejected/non-overwriting
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
D18 repository/external mutation boundaries clean/exactly governed
D19 durable evidence reopens/all references resolved
```

Fresh:

```text
D01-D03
D05-D15
D17
D18 final-cycle component
D19
```

Carry-forward with current non-contradiction:

```text
D04
D16
D18 prior boundary evidence where applicable
```

No aggregate PASS may substitute for individual rows.

---

# 13. D17 architecture

Read-only retain evidence that the target remains:

```text
Azure App Service Linux F1
West Central US
custom Docker
public/free GHCR
persistent /home
/home/data/aiquant.db
SQLite DELETE
$0.00 recurring infrastructure cost
```

No paid service/resource mutation is authorized.

---

# 14. D15 / secret hygiene

Qualification evidence token:
- temporary;
- never persisted in evidence;
- never printed;
- only sanitized disclosure indicators retained.

Twelve Data secret:
- MUST NOT be configured by WP04.

The known normal-runtime missing Twelve Data key remains:

```text
EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER
Owner=WP05
WP04ClosureEffect=NONE
```

---

# 15. Complete post-cycle repository/scope reconciliation

After runtime restoration, read-only verify:

```text
HEAD
cached remote tip
live remote tip
working tree clean
staged paths=0
git diff --check=PASS
```

Also recompute current release→main topology and changed-path scope.

Persist:

```text
CurrentMain
CurrentOriginMain
CurrentReleaseTip
MergeBase
ReleaseOnlyCommitCount
MainOnlyCommitCount
ChangedPathCount
ChangedPathList
PerPathWP04Classification
UnrelatedContaminationCount
README preservation result
WP05 implementation count
WP07 implementation count
```

No source mutation is authorized to fix scope. Any actual unrelated contamination is a HARD STOP.

---

# 16. Durable finalization — D19

After both phases and restoration:

1. close writers;
2. write full manifest with full 40-character commit;
3. hash every retained artifact;
4. independently reopen the entire root;
5. recompute every hash;
6. resolve every D01-D19 reference;
7. resolve every mutation-ledger reference;
8. resolve every initialize reference;
9. resolve every reopen reference;
10. resolve every restoration reference;
11. resolve every repository-scope reference.

Require:

```text
PredicateCount=19
FailedPredicateCount=0
UnresolvedPredicateEvidenceReferenceCount=0
UnresolvedMutationEvidenceReferenceCount=0
UnresolvedInitializeEvidenceReferenceCount=0
UnresolvedReopenEvidenceReferenceCount=0
UnresolvedRestorationEvidenceReferenceCount=0
UnresolvedEvidenceReferenceCount=0
MissingRequiredEvidenceClassCount=0
ManifestHashMismatchCount=0
DurableReopen=PASS
```

D19 cannot PASS until D14 and D18 PASS.

---

# 17. CONVERGE UNTIL COMPLETE

This is the core execution instruction.

Do not return to the user/Codex caller after a correctable defect inside this authority.

For each iteration:

```text
1. Evaluate ALL gates.
2. Produce a complete defect matrix.
3. Classify every defect:
   A = transient target condition
   B = disposable/local harness defect
   C = evidence-retention/index/manifest defect
   D = runtime attempt failed but state can be canonically restored
   E = hard governance/source/security boundary
4. Correct all A-D defects that are within this authority.
5. Restore target state after every Azure-mutating failed attempt.
6. Permanently consume every failed RunId.
7. Invalidate affected candidate evidence.
8. Use fresh RunIds and fresh evidence root/lineage where runtime attribution requires it.
9. Restart the complete affected validation cycle.
10. Continue until all D01-D19 gates and durable-reopen gates PASS.
```

Examples of defects Terra MUST fix/retry internally:
- transient 503;
- transient transport timeout;
- bounded poll timing;
- disposable evidence capture omission discovered before/within a rerunnable cycle;
- manifest/index defect;
- broken evidence reference;
- incomplete mutation-ledger serialization;
- abbreviated commit in evidence manifest;
- hash/index mismatch caused by evidence packaging;
- local harness-only evidence formatting issue.

Do not weaken acceptance requirements to converge.

---

# 18. Hard-stop boundaries

Terra MUST stop only if completion requires any of:

```text
tracked production/source edit
wrapper source edit
helper source edit
endpoint source edit
frozen runner edit
schema/persistence semantic change
new application behavior
new authentication policy
new evidence security policy
image rebuild
GHCR publication
image deployment
new Azure mutation category outside canonical qualification settings/restoration
WP07 restart/recycle/redeploy acceptance
paid Azure resource
Twelve Data secret
README mutation
PR
merge
GitHub issue/project/milestone lifecycle
new Luna architecture/policy decision
```

Also hard-stop if canonical restoration cannot be completed safely.

Return the complete defect matrix, not only the first failure.

---

# 19. Mutation accounting

Authorized:

```text
Azure:
  canonical temporary qualification settings for fresh initialize/reopen
  canonical RestoreOnly restoration

Local disposable/durable evidence:
  create/update fresh evidence roots
  local harness/evidence-only corrections within existing untracked/disposable scope
```

Expected zero:

```text
TrackedSourceMutation
GitCommit
GitPush
DockerBuild
GhcrPublication
ImageDeployment
ExplicitRestart
WP07Action
PR
Merge
GitHubLifecycle
TwelveDataSecret
PaidResource
```

Count actual operations exactly.

---

# 20. Hybrid W1-W8

Do not rerun.

Binding:

```text
HybridW1W8=ACCEPTED
```

Only perform non-contradiction reconciliation.

---

# 21. Completion gate

Terra may finish successfully only when:

```text
InitializeFreshCycle=PASS
ReopenFreshCycle=PASS
D14=PASS
D18=PASS
D19=PASS
D01-D19=19/19 PASS_OR_ACCEPTED_CARRY_FORWARD
DurableReopen=PASS
FinalTargetStateRestored=PASS
RepositoryClean=PASS
UnrelatedContaminationCount=0
WP07ActionCount=0
TrackedSourceMutationCount=0
```

Then emit:

```text
RELEASE 1.12 WP04 — EVIDENCE-RETENTION-FIRST INITIALIZE: PASS
RELEASE 1.12 WP04 — EVIDENCE-RETENTION-FIRST REOPEN: PASS
RELEASE 1.12 WP04 — D14 RESTORATION EVIDENCE: PASS
RELEASE 1.12 WP04 — D18 MUTATION ACCOUNTING: PASS
RELEASE 1.12 WP04 — D19 DURABLE EVIDENCE: PASS
RELEASE 1.12 WP04 — D01-D19: 19/19 PASS_OR_ACCEPTED_CARRY_FORWARD
RELEASE 1.12 WP04 — DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — FINAL TARGET STATE: RESTORED
RELEASE 1.12 WP04 — TRACKED SOURCE MUTATIONS: 0
RELEASE 1.12 WP04 — WP07 ACTIONS: 0
RELEASE 1.12 WP04 — READY_FOR_FINAL_LUNA: YES
```

Do not emit:

```text
RELEASE 1.12 WP04 — FINAL SUBSTANTIVE ACCEPTANCE: PASS
```

That remains Luna authority.

---

# 22. Required final handoff

Return one consolidated report only after convergence or hard stop:

```text
SelectedModel

EntryHEAD
EntryRemoteTip
WrapperSHA256
HelperSHA256
FrozenRunnerSHA256
DeployedImageDigest

FinalEvidenceRoot
EvidenceSchemaReadyBeforeRun
EvidenceClassCount

AllocatedInitializeRunIds
AllocatedReopenRunIds
ConsumedFailedRunIds
SuccessfulInitializeRunId
SuccessfulReopenRunId

InitializePollSequence
InitializeTerminalEvidence
InitializeCheckpoint
InitializeRestoreOnlyCount
InitializeRestoreCompletion
InitializePostRestoreSettingCount
InitializePrePostStateEquality

ReopenPollSequence
ReopenTerminalEvidence
ReopenCheckpoint
ReopenRestoreOnlyCount
ReopenRestoreCompletion
ReopenPostRestoreSettingCount
ReopenPrePostStateEquality

MutationLedgerRowCount
PositiveMutationRowCount
ZeroMutationCategoryCount
UnresolvedMutationEvidenceReferenceCount

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
UnresolvedInitializeEvidenceReferenceCount
UnresolvedReopenEvidenceReferenceCount
UnresolvedRestorationEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
MissingRequiredEvidenceClassCount
ManifestHashMismatchCount
DurableReopenResult

CurrentMain
CurrentOriginMain
CurrentReleaseTip
MergeBase
ReleaseOnlyCommitCount
MainOnlyCommitCount
ChangedPathCount
ChangedPathList
UnrelatedContaminationCount
UnauthorizedREADMEInformationLoss
WP05ImplementationIncluded
WP07ImplementationIncluded

TrackedSourceMutationCount
GitCommitCount
GitPushCount
AzureMutationCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
ExplicitRestartCount
WP07ActionCount
PRCount
MergeCount
GitHubLifecycleMutationCount
TwelveDataSecretConfigured
PaidResourceMutationCount

FinalHEAD
FinalRemoteTip
FinalWorkingTreeClean
FinalStagedPathCount
FinalGitDiffCheck
FinalTemporaryQualificationSettingCount
FinalTargetStateRestored

ConvergenceIterationCount
CorrectedInScopeDefectCount
HardBoundaryBlocker

FinalAcceptance
NextAuthorizedAction
```

On success:

```text
FinalAcceptance=READY_FOR_FINAL_LUNA
NextAuthorizedAction=GPT-5.6 Luna final read-only WP04 substantive acceptance reconciliation using the complete fresh evidence-retention-first package
```

On hard stop:

```text
FinalAcceptance=NOT_READY
HardBoundaryBlocker=<complete exact boundary>
NextAuthorizedAction=<narrow Luna decision authority>
```

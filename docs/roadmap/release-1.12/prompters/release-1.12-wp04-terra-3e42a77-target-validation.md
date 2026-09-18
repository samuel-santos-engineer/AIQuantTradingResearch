# Release 1.12 WP04 — Terra Fresh Target Validation After Wrapper Capture Fix

**Selected execution model: GPT-5.6 Terra**

## Mission
Execute fresh WP04 existing-contract Azure target validation from:
`3e42a776ef77dcfab8fbbd39f8e7f51e777eadc8`.

The previous `bf91221...` authority is superseded. Bind the new wrapper SHA256 before any RunId or Azure mutation.

## Roles
- GPT-5.6 Luna: contract/policy/architecture/governance/final acceptance.
- GPT-5.6 Terra: governed validation and explicitly authorized temporary Azure mutations.
- GPT-5.6 Sol: supporting analysis only.

## Ownership boundary
`WP04_APPLICATION_PERSISTENCE__WP07_DEPLOYMENT_RECOVERY`.
WP04 closure does NOT require restart/recycle/redeploy. WP07 owns those operations. Do not execute them.

## Pre-RunId Git gate
Require:
- HEAD = `3e42a776ef77dcfab8fbbd39f8e7f51e777eadc8`
- `origin/release/1.12-wp04-persistent-sqlite` = same SHA
- working tree clean; staged paths 0; `git diff --check` PASS
- commit delta from parent is exactly one path:
  `eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1`
- Windows PowerShell exactly `5.1.26100.9444`
- wrapper/helper AST parser errors = 0.

If any gate fails: STOP before RunId/Azure.

## New wrapper identity
Compute and record the SHA256 of:
`eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1`.

Require byte-preserving equality:
`WorkingTreeWrapperSHA256 == HeadWrapperSHA256 == NewWrapperSHA256`
and `git diff --quiet HEAD -- <wrapper>` PASS.

Historical wrapper SHA `61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C` is superseded for execution identity.

Helper must remain:
`5ED81167F81287AC2614546C063C938F8F747DE664190CF95BF0E2D125C9B690`.

Frozen runner must remain:
`54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983`.

## Capture-fix reconciliation
Inspect the one-file delta and report:
- CaptureFixPurpose
- CaptureFixChangedBehavior
- CaptureFixDoesNotChangePersistenceSemantics
- CaptureFixDoesNotChangeAzureMutationSurface
- CaptureFixDoesNotChangeWP04WP07Ownership
- CaptureFixLocalValidation

Require all three non-change predicates true and local validation PASS. Otherwise STOP for Luna.

Also prove `HybridW1W8NonContradiction=PASS`; do not rerun W5-W8 solely because of this capture fix.

## D01-D19
Fresh target required: D01-D03, D05-D15, D17, D19.
Carry forward: D04, D16, D18.

Definitions:
D01 app-owned persistence use case invoked.
D02 canonical observation accepted/idempotently recognized.
D03 application-owned reopen/readback unchanged.
D04 conflict rejected/non-overwriting.
D05 `/home/data/aiquant.db`.
D06 schema v4.
D07 journal DELETE.
D08 integrity ok.
D09 quick-check ok.
D10 accepted identity/count correct.
D11 HTTP evidence exact fresh RunId.
D12 checkpoint before RestoreOnly.
D13 RestoreOnly exactly once.
D14 temporary settings restored.
D15 token/secret hygiene.
D16 no SQLite/Kudu/Python bypass.
D17 F1 / West Central US / $0 unchanged.
D18 mutation boundaries clean.
D19 durable evidence reopens and every reference resolves.

## Azure pre-state
Before RunId, read-only capture sanitized target state. Require Linux App Service F1, West Central US, custom Docker, persistent `/home`, public/free GHCR architecture, `$0.00` recurring cost, expected qualification-setting pre-state, SCM basic auth false, FTP false.

Expected deployed image remains:
`sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f`.

No image build/GHCR publication. No Twelve Data secret configuration.

## RunId and Azure mutation
Only after all structural gates PASS, allocate fresh helper-owned RB2 RunId(s). Never reuse historical IDs; permanently consume failed IDs.

Only authorized Azure mutations:
temporary qualification App Settings → H1 Deferred → durable evidence checkpoint → RestoreOnly in finally.

M1: no redundant explicit restart.

Forbidden: restart/recycle/redeploy experiments, WP07 actions, paid resources, registry credential changes, Twelve Data secret, diagnostic logging experiments, resource creation.

## Qualification
Use only:
`Worker__Mode=PersistentSqliteQualification`
and `PersistentSqliteQualification__HttpEvidenceEnabled=true`.

Evidence endpoint:
`GET /internal/wp04/persistence-qualification?runId=<fresh RunId>`
with `X-WP04-Evidence-Token`.

Never print/persist token. No direct SQLite shell, Python DB access, Kudu `/home`, or deployment SQL bypass.

Prove D01-D03 through the existing application-owned path:
`PersistentSqliteQualificationExecution → PersistHistoricalObservationsUseCase.Execute → IHistoricalObservationStore.Persist`
plus application-owned reopen/readback.

No fresh Azure conflict operation for D04.

## H1 / restoration
Require:
`Deferred → durable checkpoint → RestoreOnly in finally`.
Checkpoint-before-restore PASS; RestoreOnlyCount=1; exact pre-state restoration PASS.
Restoration failure takes precedence.

## Durable evidence
Create a fresh durable evidence root for this wrapper identity. Include anchor/hashes, capture-fix evidence, pre/post target state, RunIds, mutation ledger, HTTP/application evidence, checkpoint/restoration, D01-D19 records, carry-forward refs, repo invariants, manifest/hashes.

Every D predicate record must contain:
`PredicateId, EvidenceClass, Requirement, Expected, Observed, Result, EvidenceReference, ValidationMethod, ReferenceResolved`.

Independently reopen. Require:
- PredicateCount=19
- FailedPredicateCount=0
- unresolved predicate refs=0
- unresolved evidence refs=0
- missing required evidence classes=0.

Preserve archive truth policy:
RETAINED+PASS|EMPTY eligible;
RETAINED+FAIL|NOT_APPLICABLE fail;
RETRIEVAL_FAILED+NOT_APPLICABLE eligible;
all other combinations fail/inconsistent.
No Kudu `/home` retry.

## Bounded convergence
Evaluate ALL gates, not first-failure only.

For defects correctable solely in disposable local harness/evidence machinery: collect all failures, fix all in-scope defects, rehash, invalidate affected evidence, consume failed RunIds, use fresh RunIds/root, restart the full affected cycle, and continue internally until PASS.

Hard STOP if correction needs tracked/production source change, wrapper/helper edit, frozen-runner change, image rebuild/publication, new semantics/Azure mutation category, WP07 action, PR/merge/lifecycle, paid resource, Twelve Data secret, or Luna policy decision.

After any Azure-mutating failed attempt, perform canonical restoration before returning.

## Final state
Require:
- target settings restored exactly
- HEAD and remote tip remain `3e42a776ef77dcfab8fbbd39f8e7f51e777eadc8`
- working tree clean; staged 0; diff-check PASS
- new wrapper/helper clean-HEAD PASS
- tracked source mutations 0
- GitHub mutations 0
- Docker/GHCR 0
- WP07 lifecycle actions 0
- Twelve Data secret not configured.

## Exhaustive sections
A NewAnchorAndIdentity
B CaptureFixNonContradiction
C AzurePreState
D GovernedUpdate
E FidelityAndIdempotency
F ConflictCarryForward
G PersistenceIdentityIntegrity
H RunIdAttribution
I CheckpointAndRestoration
J SecretHygieneNoBypass
K ZeroCostArchitecture
L MutationBoundary
M DurableEvidenceReopen
N FinalTargetAndRepositoryState

All A-N must PASS.

## Success markers
`RELEASE 1.12 WP04 — 3e42a77 TARGET VALIDATION: PASS`
`RELEASE 1.12 WP04 — EXECUTION ANCHOR: 3e42a776ef77dcfab8fbbd39f8e7f51e777eadc8`
`RELEASE 1.12 WP04 — NEW WRAPPER IDENTITY: VERIFIED`
`RELEASE 1.12 WP04 — CAPTURE FIX NON-CONTRADICTION: PASS`
`RELEASE 1.12 WP04 — D01-D19: 19/19 PASS_OR_ACCEPTED_CARRY_FORWARD`
`RELEASE 1.12 WP04 — DURABLE EVIDENCE REOPEN: PASS`
`RELEASE 1.12 WP04 — EVIDENCE REFERENCES: ALL_RESOLVED`
`RELEASE 1.12 WP04 — WP07 LIFECYCLE ACTIONS: 0`
`RELEASE 1.12 WP04 — AUTHORITY TRACKED SOURCE MUTATIONS: 0`
`RELEASE 1.12 WP04 — FINAL TARGET STATE: RESTORED`
`RELEASE 1.12 WP04 — SUBSTANTIVE ACCEPTANCE CANDIDATE: READY_FOR_FINAL_LUNA`
`RELEASE 1.12 WP04 — PR/MERGE: DEFERRED_UNTIL_FINAL_ACCEPTANCE`
`RELEASE 1.12 WP04 — WP04 #263: OPEN`
`RELEASE 1.12 WP04 — WP05: NOT_STARTED`
`RELEASE 1.12 WP04 — WP07: NOT_STARTED`

## Required handoff
Return exact anchor/tip/parent/delta; previous/new/head/working-tree wrapper hashes; helper/runner hashes; capture-fix fields; PowerShell/parser results; fresh evidence root and RunIds; Azure target/pre-state; mutation ledger; D01-D19 individual results; predicate/ref counts; qualification/archive/extraction/checkpoint/lifecycle/RestoreOnly/exit-code results; durable reopen; mutation counts; final restoration; A-N results; BoundaryBlocker; FinalAcceptance; NextAuthorizedAction.

On success:
`FinalAcceptance=READY_FOR_FINAL_LUNA`
`NextAuthorizedAction=GPT-5.6 Luna final read-only WP04 substantive acceptance reconciliation; final PR/merge/lifecycle remain unauthorized`

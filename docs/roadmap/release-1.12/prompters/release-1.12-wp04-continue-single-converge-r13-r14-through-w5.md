# Release 1.12 WP04 — Continue Single Converge: R13/R14 → Full Reconciliation → W5

**Selected execution model: GPT-5.6 Terra**

## Continuation authority

Continue the already-authorized single-converge execution model. Do **not** stop merely because R07 is now remediated.

Fresh structural root:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\d28fc11136b04d5aa0fc1924d723e57f
```

Known identities:

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

HarnessSHA256 =
F0A69A2AF8DD112FABBFA85CCB6BA0855BAE648058F4CF46981ED8D10E741EAA

ValidatorSHA256 =
32FC1234F3BF34AE5E2F7D06E875FC18E066C03356292205F7C7A946D031AC17
```

Derive and report the fresh child-probe hash from retained bytes.

## Accepted fresh R07 facts to reverify, not blindly trust

```text
G01-G11 records = 11
G01-G11 failures = 0
RunId allocation sites = 1
wrapper invocation sites = 1
every G01-G11 gate identifies/dominates both sites
T01-T18 = 18 explicit distinct contracts
generic T contracts = 0
StructuralProbe stops before future W5 route
W5 RunIds allocated = 0
W5 wrapper invocations = 0
real external calls = 0
SV01-SV36 = 36/0
```

Future W5 route:

```text
Invoke-SharedPreRunIdOrchestration
→ Invoke-GovernedW5AfterPreRunIdGates
→ fresh RunId allocation
→ copied-wrapper invocation
```

## Required behavior

Do not return after R07.

Continue exhaustively through R13, R14, the complete R01-R14 adversarial reconciliation, and—if ALL_PASS—W5 in this same Codex execution.

For any harness/validator/child-local defect:

```text
diagnose ALL current defects
→ repair ALL in-scope defects
→ re-hash
→ fresh durable root
→ restart complete C2 cycle
→ re-evaluate ALL R01-R14
→ repeat until ALL_PASS
```

Every byte change invalidates the current tuple/root for acceptance.

Stop only for a genuine frozen/production/external/policy governance boundary.

## R13 must be fully durable

Require exactly 11 corruption fixtures:

```text
01 Missing event
02 Duplicate EventId
03 ProbeId/EventId mismatch
04 AssertionKey mismatch
05 EvidenceReference to wrong event
06 F05 generic aggregate substitution
07 F10 generic aggregate substitution
08 F15 generic aggregate substitution
09 T-family generic aggregate substitution
10 I-family generic aggregate substitution
11 Literal PASS with failing underlying event
```

Each retained record must contain and resolve:

```text
FixtureId
FixtureContract
BaselineArtifactIdentity
BaselineValidationResult
CorruptionApplied
CorruptedArtifactIdentity
ValidatorInvocationIdentity
PostCorruptionValidationResult
ExpectedRejectionPredicate
ObservedRejectionPredicate
Result
EvidenceReference
```

Require:

```text
FixtureCount=11
BaselineAccepted=11
CorruptedExecuted=11
Rejected=11
UnexpectedAccepted=0
MissingRequiredFields=0
UnresolvedEvidenceReferences=0
```

Do not accept an aggregate 11/11 without those records.

## R14 must derive from valid R01-R13

Require the complete chain:

```text
raw events
→ 53 event-specific named records
→ H/T/F/I aggregates
→ positive/negative manifests
→ G01-G11
→ RunId/wrapper site inventories + dominance
→ 11 durable corruption records
→ three-way external-surface equality
→ three C2 summaries
→ SV01-SV36
→ BUILD_LEDGER
→ SERIALIZE
→ PUBLISH
→ REOPEN
```

After REOPEN independently resolve all acceptance evidence from disk.

Require:

```text
SV01-SV36=36/0
36 unique SV IDs
36/36 SV evidence references resolved
PRE_CLEANUP=1
CLEANUP_COMPLETE=1
BUILD_LEDGER=1
SERIALIZE=1
PUBLISH=1
REOPEN=1
runtime P01-P20 PASS claims=0
W5 RunIds=0
W5 wrapper invocations=0
real external calls=0
authority-introduced tracked changes=0
staged paths=0
git diff --check=PASS
```

## Complete adversarial structural reconciliation

Before W5, independently evaluate **every** R01-R14. Never stop diagnostic evaluation at first failure.

Require:

```text
R01 exact tuple/root
R02 exact WinPS 5.1.26100.9444 + parser 0/0/0/0
R03 53 physical/event mappings
R04 53 semantic individual records; generic aggregate named evidence=0
R05 F05/F10/F15/HNEG06 event-specific
R06 H01-H08 ALL_PASS
R07 explicit T01-T18 + G01-G11 + complete site/dominance proof
R08 complete independently derived production external surface
R09 exact three-way surface equality
R10 F01-F18 ALL_PASS
R11 Git 5 / Azure 10 / Helper 10 negatives, failures=0, escapes=0
R12 I01-I09 ALL_PASS
R13 11/11 durable corruption fixtures
R14 full derivation/SV/reopen/invariants
```

If any non-PASS is locally correctable, loop internally. Do not return to the user.

Only `R01-R14=ALL_PASS` authorizes W5.

## W5 direct continuation

Once structural acceptance is ALL_PASS, do not ask for another prompt.

Reverify the accepted tuple hashes. Execute W5 pre-RunId gates. Only then allocate exactly one fresh never-used W5 RunId.

Canonical W5 requires:

```text
P01 WinPS 5.1.26100.9444
P02 parser 0 errors
P03 source/pre exact-byte
P04 pre/post exact-byte
P05 wrapper execution completed
P06 ArchiveRetrieval RETRIEVAL_FAILED
P07 FreshExtraction NOT_APPLICABLE
P08 EvidenceCheckpoint PASS
P09 Final SUCCESS
P10 RestoreOnly 1
P11 real external calls 0
P12 external ledger only governed intercepted calls
P13 repo modified scope unchanged
P14 staged 0
P15 git diff --check PASS
P16 secret hygiene PASS
P17 durable sanitized evidence retention
P18 complete durable W5 scenario ledger retention
P19 disposable sandbox cleanup PASS
P20 error precedence PASS
```

Acceptance only `P01-P20=ALL_PASS`.

P19 is evaluated after cleanup.

Every allocated W5 RunId is permanently single-use.

If a W5 attempt finds a disposable harness/validator-only defect, consume that RunId, repair locally, invalidate structural acceptance, rerun complete C2 convergence/adversarial R01-R14, then allocate a new RunId and retry.

## Forbidden boundary crossings

STOP if correction/action requires:

```text
frozen runner mutation
tracked/production source mutation
new Luna architecture/policy decision
real external invocation
Azure call/mutation
GitHub mutation
Docker/GHCR call/mutation
Twelve Data call/configuration
secret-contract change
W6/W7/W8
publication/lifecycle mutation
```

## Final success markers

Only after complete convergence:

```text
RELEASE 1.12 WP04 — SINGLE CONVERGE C2→W5 AUTHORITY: PASS
RELEASE 1.12 WP04 — C2 R01-R14: ALL_PASS
RELEASE 1.12 WP04 — R07 TOP-LEVEL RUNTIME REACHABILITY: ACCEPTED
RELEASE 1.12 WP04 — R13 VALIDATOR SEMANTIC-CORRUPTION DEFENSES: ACCEPTED
RELEASE 1.12 WP04 — R14 FINAL STRUCTURAL DERIVATION: ACCEPTED
RELEASE 1.12 WP04 — C2 SV01-SV36: 36/0
RELEASE 1.12 WP04 — GOVERNED W5 RUNID: FRESH_SINGLE_USE
RELEASE 1.12 WP04 — W5 P01-P20: ALL_PASS
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP.

## Consolidated handoff

Return:

```text
StructuralRetryAttemptCount
StructuralSupersededRoots
FinalStructuralDurableRoot
RunnerSHA256
HarnessSHA256
ValidatorSHA256
ChildProbeSHA256
R01-R14FinalMatrix
G01-G11Results
RunIdAllocationSiteInventory
WrapperInvocationSiteInventory
UncoveredRunIdSiteCount
UncoveredWrapperSiteCount
AlternateBypassPathCount
CorruptionFixtureResults
CorruptionFixtureMissingFieldCount
SVRecordCount
SVFailedCount
ResolvedSVEvidenceCount
StructuralCheckpointCounts
StructuralAdversarialReconciliationResult
W5AttemptCount
ConsumedFailedW5RunIds
AcceptedW5RunId
W5DurableEvidenceRoot
P01-P20Results
W5AllPass
ArchiveRetrieval
FreshExtraction
EvidenceCheckpoint
FinalLifecycle
RestoreOnlyCount
RealExternalCallCount
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedModificationCount
StagedPathCount
GitDiffCheckResult
SecretHygieneResult
ExactMutationAccounting
BoundaryBlocker
NextAuthorizedAction
```

If successful:

```text
NextAuthorizedAction=GPT-5.6 Luna final W5 acceptance reconciliation / W6 sequencing authority
```

If blocked, report all known blockers, not only the first.

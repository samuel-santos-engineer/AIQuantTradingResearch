# Release 1.12 WP04 — W6 Single-Converge Authority

**Selected execution model: GPT-5.6 Terra**

## Mission
Execute WP04 hybrid W6 from the accepted restart point. Evaluate every W6 gate, collect all in-scope defects, repair every disposable harness/validator defect, restart invalidated validation, and repeat internally until ALL_PASS. Do not return after the first locally correctable defect.

## Accepted predecessor
```text
W5RunId=initialize-ee04814e110848b28095069cd0a009e1
W5Root=C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\w5\42cdd0171dae4757b2b3dec8ec9944d0
WrapperSHA256=61552F5FC0576B267F354F46E59CE124BBF1155310A1223DE799C556FA03C92C
W5 P01-P20=ALL_PASS
Lifecycle=RETRIEVAL_FAILED → NOT_APPLICABLE → checkpoint PASS → RestoreOnly 1 → SUCCESS
RealExternalCalls=0
SandboxCleanup=PASS
DurableReopen=PASS
StagedPaths=0
GitDiffCheck=PASS
TrackedProductionMutations=0
ExternalMutations=0
```
The W5 RunId is permanently consumed. W1/W2/W3/W4 carry-forward is authorized.

## Recover canonical W6 contract
Before execution, independently recover the exact W6 scenario contract from retained project-local/durable WP04 authority/evidence. Print the recovered contract and sources before allocating any W6 attempt identity.

Do not invent or broaden W6 semantics. If it cannot be recovered unambiguously, STOP:
`W6_CONTRACT_RECOVERY=NOT_PROVEN`, identify all missing/conflicting sources, and do not guess.

## Preflight
Require before W6:
```text
Windows PowerShell=5.1.26100.9444
parser errors=0
frozen runner unchanged
accepted W5 root unchanged/readable
production wrapper/helper bytes unchanged
tracked modified scope=known pre-existing two WP04 paths
staged paths=0
git diff --check=PASS
real external calls=0
```
No W5 replay.

## Boundaries
Use only the canonical disposable/local hybrid harness recovered from retained authority. No real external invocation. No Azure, GitHub, Docker/GHCR, Twelve Data, staging/commit/push, publication/lifecycle mutation. No tracked production-source mutation. No W7/W8.

## Convergence
For every W6 attempt evaluate the entire recovered acceptance matrix. For disposable harness/validator-only defects:
1. retain all failures;
2. repair all in-scope defects;
3. re-hash affected disposable artifacts;
4. create fresh W6 root/attempt identity;
5. restart complete W6 validation;
6. repeat until ALL_PASS.

Never reuse failed W6 attempt identities. Never change frozen runner.

## Durable evidence
Final W6 root must retain enough evidence for independent Luna reconciliation:
```text
canonical W6 contract identity/source
attempt identity
artifact hashes
WinPS/parser proof
authority-entry repository state
individual W6 predicate records
governed-call/interception ledger if applicable
scenario events
expected/observed values
cleanup evidence
post-run repository state
secret-hygiene evidence
error-precedence evidence
derived aggregate
manifest/index/hashes
reopen validation
```
Every predicate record:
```text
PredicateId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
ObservationPhase
```
No literal PASS disconnected from evidence.

Cleanup predicates must be evaluated after cleanup. Preserve required evidence before deleting disposable state, then reopen the durable root and resolve every acceptance reference.

## Final invariants
```text
AuthorityIntroducedTrackedProductionMutations=0
StagedPaths=0
RealExternalCalls=0
ExternalMutations=0
GitDiffCheck=PASS
SecretHygiene=PASS
```
Identify the two pre-existing tracked WP04 paths from Git.

## Stop conditions
STOP if completion requires frozen-runner mutation, tracked/production mutation, new architecture/policy, real external invocation, Azure/GitHub/Docker/GHCR/Twelve Data action, secret-contract change, W7/W8, publication, or lifecycle mutation. Report all blockers.

## Success markers
Only on complete W6 acceptance + durable reopen:
```text
RELEASE 1.12 WP04 — W6 SINGLE-CONVERGE EXECUTION: PASS
RELEASE 1.12 WP04 — W6 CANONICAL ACCEPTANCE MATRIX: ALL_PASS
RELEASE 1.12 WP04 — W6 DURABLE EVIDENCE REOPEN: PASS
RELEASE 1.12 WP04 — W6 REAL EXTERNAL CALLS: 0
RELEASE 1.12 WP04 — W6 TRACKED PRODUCTION MUTATIONS: 0
RELEASE 1.12 WP04 — W6 EXTERNAL MUTATIONS: 0
RELEASE 1.12 WP04 — W1/W2/W3/W4/W5 CARRY-FORWARD: AUTHORIZED
RELEASE 1.12 WP04 — W7/W8: NOT_RUN
RELEASE 1.12 WP04 — NEXT HYBRID RESTART POINT: W7
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```
Do not execute W7.

## Handoff
Return:
```text
RecoveredW6Contract
RecoveredW6ContractSources
W6RetryAttemptCount
ConsumedFailedW6AttemptIds
AcceptedW6AttemptId
W6DurableEvidenceRoot
RunnerSHA256
HarnessSHA256
ValidatorSHA256
ChildProbeSHA256
RelevantRuntimeHarnessSHA256
WindowsPowerShellVersion
ParserErrorCounts
W6PredicateResults
W6PredicateCount
W6FailedPredicateCount
W6ResolvedEvidenceCount
W6AggregateResult
CleanupResult
DurableReopenResult
RealExternalCallCount
UnexpectedCallCount
SecretHygieneResult
ErrorPrecedenceResult
PreExistingTrackedModifiedPaths
AuthorityIntroducedTrackedMutationCount
StagedPathCount
GitDiffCheckResult
ExternalMutationCount
BoundaryBlocker
NextAuthorizedAction
```
On success:
`NextAuthorizedAction=GPT-5.6 Luna read-only W6 reconciliation / W7 sequencing authority`

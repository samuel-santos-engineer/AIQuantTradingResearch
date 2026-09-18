# Release 1.12 WP04 — Terra Complete Fresh SV01–SV36 Durable-Ledger Rerun

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this disposable validation execution and durable evidence generation.

GPT-5.6 Luna owns subsequent reconciliation and acceptance.

GPT-5.6 Sol is supporting analysis only and must not replace Terra or Luna.

## Binding current state

The immediately preceding Luna reconciliation stopped correctly:

```text
RELEASE 1.12 WP04 — LUNA R1 RECONCILIATION: NOT_READY
```

Reason:

```text
No finalized fresh SV01-SV36 durable observation ledger from the resumed Terra rerun was supplied.
```

Newest historical candidate currently available:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\56109295ac4448498a49a8605c5fdcbc
```

Its `structural-validation.json` is a FAILED/HISTORICAL artifact and is not sufficient for Luna acceptance.

Do not overwrite or reinterpret it as fresh acceptance evidence.

Current governance:

```text
R1 structural contract: NOT_ACCEPTED
Governed W5 execution: NOT_AUTHORIZED
W5 governed acceptance: NOT_GRANTED
W6/W7/W8: NOT_RUN
Publication blocker: UNRESOLVED
WP04 #263: OPEN
Milestone #63: OPEN
WP05: NOT_STARTED
```

# Proven carry-forward fact: native Git capture mechanism

The corrected operation-scoped native capture has already been proven:

```text
git diff --check exit code: 0
output: only the two known CRLF advisory warnings
classification: ADVISORY_ONLY
scoped $ErrorActionPreference='Continue' restored immediately after Git invocation
wrapper invoked: NO
RunId allocated: NO
repository/external mutations: 0
```

This proves the capture mechanism.

The fresh rerun must still record its own actual `git diff --check` observation in SV36.

# Mission

Actually execute and complete the missing **fresh durable SV01–SV36 observation-ledger rerun**.

This authority is not another design exercise or diagnostic.

It must either:

1. produce a fresh finalized durable candidate with exactly 36 independently inspectable SV records; or
2. fail closed at the first real validation/evidence defect and retain sanitized failure evidence.

**Do not execute governed W5.**

**Do not allocate a governed W5 RunId.**

# Fresh candidate requirement

Create a new durable candidate root under the established durable evidence area, for example:

```text
%LOCALAPPDATA%\AIQuantTradingResearch\wp04\r1-runner\<fresh-guid>
```

Create an independent disposable validation root.

Requirements:

```text
FreshDurableRoot != historical durable roots
FreshDurableRoot != DisposableRoot
FreshDurableRoot is not a descendant of DisposableRoot
```

Preserve all historical roots, including:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\08106ad2e6e04389bb55633bd1c8bd65
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\cc40b3f0b1864f7daa2a2099bce8c1ed
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\56109295ac4448498a49a8605c5fdcbc
```

# Runner source/hash rule

First inspect the retained runner source from the newest historical candidate.

If that runner already contains the required executable R1 controls and no source correction is needed:

- copy it byte-for-byte into the fresh durable candidate;
- independently compute and record its SHA-256;
- validate all SV claims against that exact retained hash.

If any runner source correction is required:

- create corrected runner source only in disposable/local evidence space;
- create a new runner hash;
- retain the corrected runner in the fresh durable root;
- validate the complete contract against the new hash.

No cross-hash acceptance.

# Windows PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Do not use PowerShell 7-only syntax or behavior.

Complete final retained runner parser errors must equal zero.

# SV01–SV36 durable schema

Produce exactly 36 unique records:

```text
SV01
SV02
...
SV36
```

Each record must contain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

No missing/null/unresolved fields.

`Observed` must contain substantive evidence. It must not merely be:

```text
true
PASS
same text as Expected
```

when richer evidence exists.

Every `EvidenceReference` must resolve within the fresh retained evidence/source.

# Required checks

```text
SV01 Windows PowerShell version exactly 5.1.26100.9444
SV02 complete final retained runner parser errors = 0
SV03 exactly canonical P01-P20 definitions exist
SV04 required predicate fields complete
SV05 all P01-P20 participate in final aggregation
SV06 missing/unresolved predicates fail closed
SV07 structural validation claims zero runtime predicate PASS
SV08 executable source-wrapper SHA-256 calculation exists
SV09 executable byte-for-byte wrapper copy exists
SV10 executable copied-wrapper pre SHA-256 calculation exists
SV11 pre-RunId source/copy equality assertion exists
SV12 executable source/copy post hashes exist
SV13 post-execution equality assertions exist
SV14 hash mismatch fails closed
SV15 G01-G11 all precede RunId allocation
SV16 G01-G11 all precede wrapper invocation
SV17 RunId allocation precedes wrapper invocation
SV18 S validation is executable
SV19 W validation is executable
SV20 child S visibility proof is executable
SV21 child W visibility proof is executable
SV22 child S/W mismatch fails closed
SV23 P03 binds to exact-byte pre evidence
SV24 P04 binds to exact-byte post evidence
SV25 P19 post-cleanup ordering preserved
SV26 P20 remains observation-only / production-derived
SV27 minimum shim surface is only git, az, required helper calls
SV28 unexpected calls fail closed
SV29 production-policy duplication absent
SV30 secret hygiene
SV31 disposable and durable roots are independent
SV32 disposable root cleanup succeeds
SV33 retained runner survives cleanup
SV34 retained structural ledger survives cleanup
SV35 retained runner SHA-256 reverified after cleanup
SV36 repository invariants preserved, including fresh git diff --check evidence
```

# Independent evidence requirements

For executable/source/control-flow checks, retain actual source references and parsed/tokenized/AST-backed or equivalently inspectable observations.

Comments alone are insufficient.

For SV15–SV17 retain an observed sequence/source mapping proving:

```text
G01 ... G11 < G12 RunId allocation < G13 wrapper invocation
```

For SV18–SV22 retain actual executable references proving S/W validation, child visibility, exact comparison, and mismatch termination.

For SV23–SV26 retain actual P03/P04/P19/P20 binding references.

For SV27–SV30 retain actual shim allowlist, unexpected-call fail-closed mechanism, no-policy-duplication inspection, and secret-hygiene evidence.

# Canonical P01–P20 structural definitions

The same retained runner hash must structurally define exactly:

```text
P01 Windows PowerShell version == 5.1.26100.9444
P02 complete runner/harness parser errors == 0
P03 exact-byte source/pre-execution wrapper identity == PASS
P04 exact-byte pre/post-execution wrapper identity == PASS
P05 production wrapper execution completed == PASS
P06 ArchiveRetrieval == RETRIEVAL_FAILED
P07 FreshExtraction == NOT_APPLICABLE
P08 EvidenceCheckpoint == PASS
P09 FinalLifecycleResult == SUCCESS
P10 RestoreOnly count == 1
P11 unexpected real external invocations == 0
P12 external-call ledger contains only governed intercepted calls == PASS
P13 repository modified-path scope unchanged == PASS
P14 staged path count == 0
P15 git diff --check == PASS
P16 secret hygiene == PASS
P17 durable sanitized evidence retention == PASS
P18 complete durable W5 scenario ledger retention == PASS
P19 disposable sandbox cleanup == PASS
P20 error-precedence accounting == PASS
```

This is structural validation only.

Do not mark runtime P01–P20 results PASS.

# P19 ordering

Structurally prove:

```text
P01-P18
→ P20
→ persist durable evidence outside sandbox
→ delete disposable sandbox
→ verify sandbox absence
→ evaluate P19
→ finalize P19 outside sandbox
→ aggregate P01-P20
```

# P20 contract

P20 must observe/account for production-derived error precedence.

Do not duplicate production policy in the runner.

# Native `git diff --check` capture

Use the already-proven operation-scoped Windows PowerShell 5.1 behavior.

During this fresh rerun record:

```text
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
```

Rules:

- capture output;
- capture `$LASTEXITCODE` immediately;
- advisory text alone must not terminate the evidence writer;
- restore error handling immediately;
- nonzero exit code fails closed unless separately governed;
- do not discard real whitespace failures.

Known CRLF advisories may be classified `ADVISORY_ONLY` when exit code is 0 and no actual diff-check failure is present.

# Two-root finalization

The fresh rerun must complete this lifecycle:

1. create disposable validation root;
2. create independent fresh durable root;
3. retain runner source/hash;
4. produce all SV01–SV36 records;
5. persist complete ledger outside disposable root;
6. remove disposable root;
7. verify disposable root absent;
8. verify durable root present;
9. verify retained runner present;
10. verify retained ledger present;
11. re-open and parse retained ledger;
12. re-hash retained runner;
13. finalize post-cleanup lifecycle observations durably without recreating disposable root.

SV32–SV35 must reflect actual post-cleanup observations.

# SV36 repository evidence

Retain:

```text
ModifiedTrackedPathsBefore
ModifiedTrackedPathsAfter
StagedPathsBefore
StagedPathsAfter
GitDiffCheckCommand
GitDiffCheckExitCode
GitDiffCheckOutput
GitDiffCheckClassification
GitDiffCheckResult
AuthorityIntroducedTrackedMutations
```

Expected repository state:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

Do not stage, revert, clean, or edit them.

# Mutation boundary

This authority authorizes only disposable/local evidence-file creation and cleanup.

It authorizes:

```text
tracked repository mutations = 0
staged paths = 0
commits = 0
pushes = 0
GitHub mutations = 0
production mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
external mutations = 0
```

# Fresh ledger acceptance threshold

The Terra structural rerun itself may report PASS only if:

```text
fresh durable root exists
retained runner exists
retained ledger exists and parses
runner hash reverified after cleanup
SV record count = 36
SV IDs exactly SV01-SV36
duplicates = 0
missing = 0
required-field failures = 0
independent-observation failures = 0
evidence-reference failures = 0
validation-method failures = 0
SV Result failures = 0
SV_FAIL = 0
P01-P20 structural contract = PASS
runtime P01-P20 PASS claims = 0
two-root lifecycle = PASS
fresh git diff --check governed result = PASS
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
```

# Required success markers

Only on complete success emit:

```text
RELEASE 1.12 WP04 — R1 FRESH SV01-SV36 DURABLE-LEDGER RERUN: PASS
RELEASE 1.12 WP04 — R1 FRESH DURABLE CANDIDATE ROOT: CREATED
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 INDEPENDENT OBSERVATIONS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 EVIDENCE REFERENCES: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 VALIDATION METHODS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 ALL_PASS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 STRUCTURAL CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — R1 RUNNER SINGLE-ARTIFACT CONTRACT COMPLETENESS: PASS
RELEASE 1.12 WP04 — R1 RUNNER EXACT-BYTE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRE-RUNID FAIL-CLOSED BOUNDARY: PASS
RELEASE 1.12 WP04 — R1 RUNNER CHILD S/W VISIBILITY: PASS
RELEASE 1.12 WP04 — R1 P19 POST-CLEANUP ORDER: PASS
RELEASE 1.12 WP04 — R1 P20 EVIDENCE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHIM SURFACE: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRODUCTION-POLICY DUPLICATION: ABSENT
RELEASE 1.12 WP04 — R1 TWO-ROOT LIFECYCLE: PASS
RELEASE 1.12 WP04 — R1 DISPOSABLE ROOT CLEANUP: PASS
RELEASE 1.12 WP04 — R1 DURABLE EVIDENCE SURVIVES CLEANUP: PASS
RELEASE 1.12 WP04 — R1 RETAINED RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK NATIVE CAPTURE: PASS
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK EXIT CODE: 0
RELEASE 1.12 WP04 — R1 CRLF ADVISORY CLASSIFICATION: ADVISORY_ONLY
RELEASE 1.12 WP04 — R1 WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 RUNNER PARSER: PASS
RELEASE 1.12 WP04 — R1 SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — R1 TRACKED REPOSITORY MUTATIONS: 0
RELEASE 1.12 WP04 — R1 STAGED PATHS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TERRA FRESH SV01-SV36 LEDGER GENERATION COMPLETE
```

# Required report

Report:

```text
FreshDurableRoot
DisposableRoot
RetainedRunnerPath
RetainedRunnerSHA256
whether runner source changed
PowerShell version
parser error count
SV ledger path
SV record count
SV failed count
P01-P20 definition count
runtime predicate PASS claim count
git diff --check exit code
git diff --check output/classification
DisposableExistsAfterCleanup
DurableExistsAfterCleanup
RunnerExistsAfterCleanup
LedgerExistsAfterCleanup
RunnerHashAfterCleanup
modified tracked paths before/after
staged paths before/after
exact mutation accounting
```

# Failure boundary

At the first failure:

- persist sanitized durable failure evidence when possible;
- report the exact first failed gate;
- classify it as runner, harness, evidence, repository-invariant, or contract defect;
- do not execute W5;
- do not allocate a W5 RunId;
- do not mutate production/repository/external systems;
- STOP.

# Final stop

**STOP AFTER THE FRESH DURABLE SV01–SV36 LEDGER IS FINALIZED OR THE FIRST REAL FAILURE IS RETAINED.**

Even on PASS:

```text
R1 Luna acceptance = PENDING
Governed W5 execution = NOT_AUTHORIZED until Luna reconciliation
W5 acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
Publication blocker = UNRESOLVED
```

After a successful Terra result, execute the existing **GPT-5.6 Luna R1 Fresh SV01–SV36 Ledger Reconciliation Authority** against the fresh retained evidence.

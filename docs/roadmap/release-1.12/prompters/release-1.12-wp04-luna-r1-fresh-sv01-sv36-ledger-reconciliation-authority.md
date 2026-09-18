# Release 1.12 WP04 — Luna R1 Fresh SV01–SV36 Ledger Reconciliation Authority

## Authority identity

**Selected execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns contract interpretation, reconciliation, acceptance criteria, governance, and authorization of the next phase.

GPT-5.6 Terra owns implementation, validation execution, and explicitly authorized mutations.

GPT-5.6 Sol is supporting analysis only and must not silently replace Luna or Terra.

## Invocation precondition

Execute this authority **only after** GPT-5.6 Terra completes the already-issued:

```text
Release 1.12 WP04 — Terra Resume Fresh SV01–SV36 Durable-Ledger Rerun
```

and returns a finalized fresh durable candidate.

If Terra has not completed that run, STOP:

```text
RELEASE 1.12 WP04 — LUNA R1 RECONCILIATION: NOT_READY
REASON: FRESH SV01-SV36 DURABLE LEDGER NOT SUPPLIED
```

Do not infer or manufacture missing Terra results.

# Mission

Perform a read-only Luna reconciliation of the fresh R1 runner structural-evidence candidate.

Determine whether the retained evidence proves, for one runner artifact/hash:

1. exactly 36 independently evidenced SV01–SV36 checks;
2. complete P01–P20 structural accounting;
3. executable exact-byte pre/post controls;
4. ordered pre-RunId G01–G11 gates;
5. S/W child visibility and fail-closed behavior;
6. P19 post-cleanup ordering;
7. P20 observation-only/error-precedence contract;
8. minimum shim/no-policy-duplication boundaries;
9. two-root evidence lifecycle;
10. Windows PowerShell 5.1 compatibility;
11. secret hygiene;
12. repository invariants;
13. valid scoped `git diff --check` capture;
14. zero W5 execution and zero W5 RunId allocation.

This reconciliation is read-only.

# Binding project state

```text
Release: 1.12
WP: #263 WP04 Persistent SQLite Initialization, Data Update & Recovery
WP04 state: OPEN
W5 governed acceptance: NOT_GRANTED
W6/W7/W8: NOT_RUN
Publication blocker: UNRESOLVED
WP05: NOT_STARTED
Milestone #63: OPEN
```

PowerShell baseline:

```text
Windows PowerShell 5.1.26100.9444
```

Expected repository state:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

No Luna mutation is authorized.

# Historical evidence rule

Historical failed R1 roots must remain historical evidence only.

Do not combine structural PASS claims across different runner hashes.

The fresh candidate must prove the complete structural contract for the **same retained runner hash**.

Previously proven scoped Git-capture behavior may explain the mechanism, but the fresh candidate must still contain its own repository/SV36 observation from the completed rerun.

# Required Terra handoff

Require Terra to provide at least:

```text
fresh durable candidate root
disposable validation root
retained runner path
retained runner SHA-256
parser evidence
Windows PowerShell version evidence
fresh git diff --check command/output/exit-code/classification
SV01-SV36 durable ledger path
SV failure count
P01-P20 structural evidence
two-root lifecycle evidence
repository pre/post state
mutation accounting
governed W5 wrapper invoked status
governed W5 RunId allocated status
```

Missing required handoff evidence fails closed.

# Reconciliation procedure

## R01 — Candidate identity

Verify:

```text
durable root exists
retained runner exists
retained ledger exists
ledger parses
runner hash in ledger == independently recomputed retained runner hash
all structural claims bind to that same runner hash
```

No cross-hash carry-forward.

## R02 — SV cardinality and identity

Verify exactly:

```text
36 records
SV01 through SV36
no missing IDs
no duplicates
no extra/substitute IDs
```

## R03 — SV record schema

Every SV record must contain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Equivalent names require explicit lossless mapping.

Missing/null/unresolved fields fail.

## R04 — Independent observation quality

For every SV record determine whether `Observed` is independently inspectable.

Reject observations that are merely:

```text
true
PASS
expected-value restatement
aggregate-only assertion
comment-only assertion for executable behavior
```

when richer evidence is required.

## R05 — Evidence references

Verify every `EvidenceReference` resolves to retained durable evidence or retained runner source.

A reference must be sufficiently precise for independent inspection.

Broken/unresolvable references fail.

## R06 — Validation methods

Verify every SV record states how its observation was derived.

For executable source/control-flow properties, require source-backed, parsed/tokenized/AST-backed, or equivalently inspectable evidence.

Loose comments alone are insufficient.

## R07 — SV01/SV02 environment

Verify:

```text
SV01 actual Windows PowerShell = 5.1.26100.9444
SV02 parser errors = 0
```

## R08 — SV03–SV07 predicate structure

Verify retained evidence proves:

```text
exact P01-P20 ID set
20 definitions
required fields complete
all predicates in final aggregation
missing/unresolved runtime evidence fails closed
structural validation claims zero runtime predicate PASS
```

## R09 — SV08–SV14 exact-byte executable controls

Verify executable evidence proves:

```text
source-wrapper SHA-256 calculation
byte-for-byte copy
copy-pre SHA-256
pre-RunId source/copy equality assertion
source/copy post hashes
post equality assertions
hash mismatch fail-closed branch
```

Comments or predicate descriptions alone do not satisfy this gate.

## R10 — SV15–SV17 execution order

Verify executable control-flow evidence proves:

```text
G01 ... G11 < G12 RunId allocation < G13 wrapper invocation
```

All G01–G11 must terminate fail-closed before any governed RunId allocation or wrapper invocation.

## R11 — SV18–SV22 S/W boundary

Verify executable evidence proves:

```text
S defined/nonempty/absolute/fresh/existing
W defined/nonempty/absolute/existing/file/expected
child receives exact S
child receives exact W
child compares exact values
mismatch terminates before RunId/wrapper
```

## R12 — SV23/SV24 P03/P04 binding

Verify:

```text
P03 binds to actual exact-byte pre evidence
P04 binds to actual exact-byte post evidence
```

No synthetic replacement.

## R13 — SV25 P19 ordering

Verify:

```text
P01-P18
→ P20
→ durable evidence outside sandbox
→ delete sandbox
→ verify absence
→ evaluate P19
→ finalize P19 outside sandbox
→ aggregate P01-P20
```

No sandbox recreation after cleanup to manufacture P19.

## R14 — SV26 P20

Verify P20 observes/accounts for production-derived error precedence.

Reject cloned/reimplemented production policy.

## R15 — SV27/SV28 shim boundary

Verify minimum interception surface remains only:

```text
git
az
required helper calls
```

and unexpected calls fail closed.

## R16 — SV29 production-policy duplication

Verify the runner does not duplicate persistence/archive/lifecycle production policy merely to manufacture expected W5 results.

## R17 — SV30 secret hygiene

Verify retained runner/ledger/output do not expose secrets.

Do not print secret values during reconciliation.

## R18 — SV31–SV35 two-root lifecycle

Verify actual retained observations prove:

```text
DisposableRoot != DurableRoot
DurableRoot not descendant of DisposableRoot
DisposableExistsAfterCleanup = False
DurableExistsAfterCleanup = True
RunnerExistsAfterCleanup = True
LedgerExistsAfterCleanup = True
RunnerHashBeforeCleanup == RunnerHashAfterCleanup
```

## R19 — SV36 repository and Git evidence

Verify fresh evidence contains:

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

Expected governed interpretation:

```text
git diff --check exit code = 0
known CRLF advisory output may be retained
advisory-only output is not itself a diff failure
error-handling change was operation-scoped and restored
```

A nonzero fresh exit code fails unless separately governed; do not hide real whitespace failures.

## R20 — Mutation boundary

Verify the Terra rerun introduced:

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

The two pre-existing governed modified scripts are not new mutations.

## R21 — W5 boundary

Require explicit proof:

```text
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
runtime W5 acceptance predicates executed = NO
```

Structural validation must not be mislabeled as W5 runtime acceptance.

# Luna decision

## Decision A — ACCEPT R1 structural artifact

Select only if R01–R21 all PASS.

Then emit exactly:

```text
RELEASE 1.12 WP04 — LUNA R1 FRESH SV01-SV36 RECONCILIATION: PASS
RELEASE 1.12 WP04 — R1 SINGLE-ARTIFACT STRUCTURAL CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 SV01-SV36 INDEPENDENT EVIDENCE: ACCEPTED
RELEASE 1.12 WP04 — R1 P01-P20 STRUCTURAL ACCOUNTING: ACCEPTED
RELEASE 1.12 WP04 — R1 EXACT-BYTE EXECUTABLE CONTROLS: ACCEPTED
RELEASE 1.12 WP04 — R1 PRE-RUNID FAIL-CLOSED BOUNDARY: ACCEPTED
RELEASE 1.12 WP04 — R1 CHILD S/W VISIBILITY: ACCEPTED
RELEASE 1.12 WP04 — R1 P19 POST-CLEANUP ORDER: ACCEPTED
RELEASE 1.12 WP04 — R1 P20 EVIDENCE CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 TWO-ROOT DURABLE EVIDENCE LIFECYCLE: ACCEPTED
RELEASE 1.12 WP04 — R1 WINDOWS POWERSHELL 5.1 CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK CAPTURE CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 REPOSITORY MUTATION BOUNDARY: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_FOR_SEPARATE_TERRA_AUTHORITY
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

Important:

`AUTHORIZED_FOR_SEPARATE_TERRA_AUTHORITY` does not itself execute W5.

After Decision A, STOP. A separate Terra W5 execution authority must be created.

## Decision B — REJECT R1 structural artifact

If any R01–R21 gate fails, emit:

```text
RELEASE 1.12 WP04 — LUNA R1 FRESH SV01-SV36 RECONCILIATION: FAIL
RELEASE 1.12 WP04 — R1 SINGLE-ARTIFACT STRUCTURAL CONTRACT: NOT_ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

Then identify:

```text
first failed reconciliation gate
exact deficient retained evidence
classification:
  RUNNER_DEFECT
  STRUCTURAL_EVIDENCE_DEFECT
  HARNESS_DEFECT
  REPOSITORY_INVARIANT_DEFECT
  CONTRACT_AMBIGUITY
whether production defect exists: YES/NO
whether a fresh runner hash is required: YES/NO
narrowest authorized next correction
```

Do not authorize W5.

# Prohibitions

Luna must not:

- edit repository files;
- edit the retained runner;
- regenerate the ledger;
- execute W5;
- allocate a W5 RunId;
- invoke Azure/Docker/GHCR;
- stage/commit/push;
- mutate GitHub;
- close #263;
- set Project #2 Done;
- begin WP05;
- infer PASS from missing evidence;
- combine PASS evidence across different runner hashes.

# Final state

Regardless of reconciliation outcome:

```text
WP04 #263 remains OPEN
Milestone #63 remains OPEN
WP05 remains NOT_STARTED
W5 acceptance remains NOT_GRANTED until a later governed runtime W5 run passes P01-P20
W6/W7/W8 remain NOT_RUN
publication blocker remains UNRESOLVED
```

## Final stop

**STOP AFTER LUNA R1 RECONCILIATION.**

If Decision A is reached, the next artifact is a separate **GPT-5.6 Terra governed W5 execution authority**.

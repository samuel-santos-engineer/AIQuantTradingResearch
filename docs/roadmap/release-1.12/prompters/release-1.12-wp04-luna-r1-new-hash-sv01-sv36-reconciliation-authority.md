# Release 1.12 WP04 — Luna R1 New-Hash SV01–SV36 Reconciliation Authority

## Authority identity

**Selected execution model: GPT-5.6 Luna**

GPT-5.6 Luna owns contract interpretation, reconciliation, acceptance criteria, governance, and authorization of the next governed phase.

GPT-5.6 Terra owns implementation, validation execution, and explicitly authorized mutations.

GPT-5.6 Sol is supporting analysis only and must not silently replace Luna or Terra.

# Invocation precondition

Execute this authority **only after** GPT-5.6 Terra completes the loaded:

```text
Release 1.12 WP04 — Terra R1 SV09 Executable Wrapper-Copy Correction Authority
```

and supplies a new retained runner artifact/hash plus a complete fresh SV01–SV36 durable ledger.

If that Terra execution has not completed, STOP with:

```text
RELEASE 1.12 WP04 — LUNA R1 NEW-HASH RECONCILIATION: NOT_READY
REASON: SV09 RUNNER CORRECTION AND NEW-HASH SV01-SV36 LEDGER NOT SUPPLIED
```

Do not infer missing results from the authority text itself.

# Binding predecessor

Historical failed runner:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
```

Historical failed evidence:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\014e51fb8a30407b9e9ed325e2a53389\structural-observation-ledger.json
```

First proven defect:

```text
SV09 — executable byte-for-byte wrapper copy exists
Classification: RUNNER_DEFECT
Production defect: NO
Fresh runner hash required: YES
```

The old runner/hash is historical failure evidence only.

It cannot contribute structural PASS credit to the new candidate.

# Mission

Read-only reconcile the **new runner hash** and its complete fresh SV01–SV36 evidence.

Determine whether the new candidate proves the entire R1 single-artifact structural contract, including the corrected executable source-wrapper → `$env:W` byte-preserving copy.

No mutation is authorized.

# Required Terra handoff

Require:

```text
fresh durable candidate root
fresh disposable root identity
retained corrected runner path
old runner SHA-256
new runner SHA-256
exact runner-source correction
PowerShell version
parser evidence
SV01-SV36 ledger path
SV record count
SV failure count
P01-P20 structural evidence
runtime predicate PASS claim count
SV09 executable copy source references
SV08-SV14 coherence evidence
G01-G13 ordering evidence
P19/P20 evidence
shim/no-policy-duplication evidence
two-root lifecycle evidence
fresh git diff --check evidence
repository pre/post state
exact mutation accounting
governed W5 wrapper invocation status
governed W5 RunId allocation status
```

Missing required handoff evidence fails closed.

# R01 — New candidate identity

Verify:

```text
new runner hash is present
new runner hash != C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
retained runner exists
retained ledger exists and parses
ledger binds to new runner hash
independent retained-runner SHA-256 recomputation == ledger runner hash
```

No cross-hash carry-forward.

# R02 — Narrow source correction

Inspect the reported runner correction.

Verify it is narrowly attributable to the required executable wrapper-copy contract and any directly necessary structural integration.

Reject unexplained runner changes that materially expand policy or behavior beyond the R1 harness contract.

This does not require byte-minimality; it requires governed scope.

# R03 — SV cardinality

Require exactly:

```text
36 records
SV01-SV36
missing = 0
duplicates = 0
extra/substitute IDs = 0
```

# R04 — SV record completeness

Every record must contain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Missing/null/unresolved fields fail.

# R05 — Independent evidence quality

Every `Observed` value must provide independently inspectable evidence appropriate to its check.

Reject aggregate-only or generic assertions such as:

```text
true
PASS
topology present
contract satisfied
```

when source/control-flow/path/hash evidence is required.

# R06 — Evidence references

Every `EvidenceReference` must resolve against the retained new-hash runner or durable evidence.

For executable controls, require sufficiently precise source references or parsed structural evidence.

# R07 — Validation methods

Every check must identify how its observation was derived.

Comments alone cannot prove executable behavior.

# R08 — SV01/SV02 environment

Verify:

```text
Windows PowerShell = 5.1.26100.9444
parser errors = 0
```

# R09 — SV03–SV07 P01–P20 structural accounting

Verify:

```text
exact canonical P01-P20
20 unique definitions
required predicate fields complete
all participate in aggregation
missing/unresolved runtime evidence fails closed
runtime predicate PASS claims during structural validation = 0
```

# R10 — SV08 source hash

Verify executable source-wrapper SHA-256 calculation exists and references the governed production-wrapper source.

# R11 — SV09 executable byte-preserving copy

This is the formerly failed gate.

Require independently inspectable executable evidence proving all of:

```text
governed wrapper source path resolved
source validated
actual file-copy operation exists
copy operation is byte-preserving
copy destination is the exact path represented by $env:W
copy does not use text transformation
copy failure fails closed
```

Acceptable copy mechanisms include Windows PowerShell 5.1-compatible binary/file-copy semantics such as a suitable .NET file-copy API or `Copy-Item`.

Reject text-copy mechanisms such as:

```text
Get-Content | Set-Content
Out-File
encoding conversion
line-ending normalization
regex/string transformation
```

Generic topology text is insufficient.

# R12 — SV10/SV11 pre-execution exact-byte proof

Verify:

```text
SHA-256 computed for copied W
source hash == W pre hash asserted
assertion occurs before governed RunId allocation
mismatch fails closed
```

# R13 — SV12–SV14 post-execution exact-byte proof

Verify future governed execution structurally computes:

```text
source pre
W pre
source post
W post
```

and requires:

```text
source pre == W pre
source pre == source post
W pre == W post
source post == W post
```

Mismatch must fail closed.

# R14 — SV08–SV14 artifact coherence

Verify all source/copy/hash controls refer to the same governed source wrapper and same `$env:W` destination.

Reject split-path or second-copy designs that break artifact identity.

# R15 — SV15–SV17 pre-RunId order

Verify executable control-flow evidence proves:

```text
G01 ... G11 < G12 RunId allocation < G13 wrapper invocation
```

The corrected copy must be inside the pre-RunId gate sequence.

# R16 — SV18–SV22 S/W boundary

Verify executable evidence proves:

```text
S defined/nonempty/absolute/fresh/existing
W defined/nonempty/absolute/expected
W is the copied-wrapper destination
child receives exact S
child receives exact W
child compares exact values
mismatch fails before RunId/wrapper
```

# R17 — SV23/SV24 P03/P04 bindings

Verify:

```text
P03 <- actual source/W pre-execution exact-byte evidence
P04 <- actual source/W pre/post exact-byte evidence
```

No topology-only substitute.

# R18 — SV25 P19 order

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

# R19 — SV26 P20 contract

Verify P20 observes/accounts for production-derived error precedence and does not clone production policy.

# R20 — SV27/SV28 shim boundary

Require minimum interception surface only:

```text
git
az
required helper calls
```

Unexpected calls must fail closed.

# R21 — SV29 no production-policy duplication

Verify the runner does not implement persistence/archive/lifecycle policy to manufacture W5 outcomes.

# R22 — SV30 secret hygiene

Verify retained source/evidence contains no exposed secrets.

Do not print secret values during reconciliation.

# R23 — SV31–SV35 two-root lifecycle

Require actual evidence proving:

```text
DisposableRoot != DurableRoot
DurableRoot not descendant of DisposableRoot
DisposableExistsAfterCleanup = False
DurableExistsAfterCleanup = True
RunnerExistsAfterCleanup = True
LedgerExistsAfterCleanup = True
RunnerHashBeforeCleanup == RunnerHashAfterCleanup
```

# R24 — SV36 repository/Git evidence

Require fresh candidate evidence containing:

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

Expected baseline:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

Known CRLF advisories are acceptable only when the fresh command has:

```text
exit code = 0
classification = ADVISORY_ONLY
```

and the scoped error-handling state was restored.

# R25 — Mutation accounting

Require Terra introduced:

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

Disposable/local evidence artifacts are permitted.

# R26 — W5 boundary

Require:

```text
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
runtime W5 predicate execution = NO
```

Structural validation is not W5 acceptance.

# Luna decision

## Decision A — ACCEPT new-hash R1 structural artifact

Select only if R01–R26 all PASS.

Emit:

```text
RELEASE 1.12 WP04 — LUNA R1 NEW-HASH SV01-SV36 RECONCILIATION: PASS
RELEASE 1.12 WP04 — R1 NEW RUNNER ARTIFACT: ACCEPTED
RELEASE 1.12 WP04 — R1 SINGLE-ARTIFACT STRUCTURAL CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — R1 SV09 EXECUTABLE BYTE-PRESERVING WRAPPER COPY: ACCEPTED
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE CONTROL COHERENCE: ACCEPTED
RELEASE 1.12 WP04 — R1 SV01-SV36 INDEPENDENT EVIDENCE: ACCEPTED
RELEASE 1.12 WP04 — R1 P01-P20 STRUCTURAL ACCOUNTING: ACCEPTED
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

Then STOP.

Do not execute W5 under this Luna authority.

The next artifact after Decision A is a separate GPT-5.6 Terra governed W5 execution authority.

## Decision B — REJECT new-hash R1 structural artifact

If any R01–R26 gate fails, emit:

```text
RELEASE 1.12 WP04 — LUNA R1 NEW-HASH SV01-SV36 RECONCILIATION: FAIL
RELEASE 1.12 WP04 — R1 NEW RUNNER ARTIFACT: NOT_ACCEPTED
RELEASE 1.12 WP04 — R1 SINGLE-ARTIFACT STRUCTURAL CONTRACT: NOT_ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: NOT_AUTHORIZED
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

Report:

```text
first failed reconciliation gate
first failed SV if applicable
exact deficient retained evidence
classification:
  RUNNER_DEFECT
  STRUCTURAL_EVIDENCE_DEFECT
  HARNESS_DEFECT
  REPOSITORY_INVARIANT_DEFECT
  CONTRACT_AMBIGUITY
production defect: YES/NO
fresh runner hash required: YES/NO
narrowest authorized next correction
```

Do not authorize W5.

# Prohibitions

Luna must not:

- edit repository files;
- edit or regenerate the runner;
- regenerate the SV ledger;
- execute W5;
- allocate a W5 RunId;
- invoke Azure/Docker/GHCR;
- stage/commit/push;
- mutate GitHub;
- close #263;
- set Project #2 Done;
- begin WP05;
- infer missing evidence;
- combine structural acceptance across runner hashes.

# Final state

Regardless of outcome:

```text
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
W5 acceptance = NOT_GRANTED until a later governed W5 runtime run passes P01-P20
W6/W7/W8 = NOT_RUN
Publication blocker = UNRESOLVED
```

# Final stop

**STOP AFTER LUNA NEW-HASH R1 RECONCILIATION.**

If Decision A is reached, create a separate **GPT-5.6 Terra governed W5 execution authority** as the next Markdown artifact.

# Release 1.12 WP04 — Terra R1 SV Ledger CRLF-Capture Correction & Fresh Rerun Authority

## Authority identity

**Selected execution model: GPT-5.6 Terra**

This is a narrow disposable validation-harness correction and fresh structural-evidence rerun authority.

GPT-5.6 Luna owns reconciliation and acceptance. GPT-5.6 Terra may correct only the native-command capture behavior that prevented the SV01–SV36 evidence ledger from finalizing. GPT-5.6 Sol is supporting/non-authoritative only.

## Binding predecessor

```text
RELEASE 1.12 WP04 — R1 SV01-SV36 OBSERVATION-LEDGER CORRECTION: FAIL
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

Failure classification:

```text
DISPOSABLE_VALIDATION_HARNESS_NATIVE_COMMAND_CAPTURE_ERROR
```

Observed cause:

```text
git diff --check emitted known CRLF advisory warnings.
$ErrorActionPreference='Stop' caused the evidence-generation command to terminate
before a valid finalized SV01-SV36 ledger was produced.
```

This is NOT:

- a production defect;
- a W5 execution defect;
- a Git diff failure;
- authority to suppress real `git diff --check` failures.

# Mission

Correct only the disposable structural-evidence writer's invocation/capture of:

```text
git diff --check
```

so that stdout/stderr and the native exit code are captured explicitly under Windows PowerShell 5.1 without known CRLF advisory text itself terminating the writer.

Then perform a **fresh structural observation-ledger rerun** in a fresh durable candidate root.

Do not execute governed W5.
Do not allocate a governed W5 RunId.

# Windows PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7 syntax or assumptions.

# Native-command capture contract

The harness must invoke `git diff --check` in a Windows PowerShell 5.1-compatible manner that:

1. captures all emitted text durably;
2. captures `$LASTEXITCODE` immediately after the native command;
3. does not let advisory output alone abort the evidence writer;
4. restores any temporarily changed error-handling state immediately afterward;
5. evaluates PASS/FAIL from the actual native exit code and governed interpretation;
6. preserves the raw/sanitized command output in evidence;
7. does not globally weaken fail-closed behavior for the rest of the harness.

An operation-scoped pattern is required. For example, temporarily alter error handling only around the native invocation, or use another PS5.1-compatible explicit capture mechanism.

Do NOT globally set:

```text
$ErrorActionPreference = 'Continue'
```

for the whole validation run.

# `git diff --check` truth contract

Known CRLF advisory warnings may be retained as observations and are not, by themselves, a repository mutation.

However:

```text
native exit code != 0
```

must fail the repository/diff gate unless existing project governance explicitly proves a narrower interpretation from the actual output.

Do not discard the exit code.
Do not replace the command with a different Git check.
Do not regex away real whitespace errors merely because CRLF advisories are also present.

Retain:

```text
command
exit code
captured output
classification
PASS/FAIL
```

# Fresh-root rule

The failed evidence-generation attempt receives no structural acceptance credit.

Create a **fresh durable candidate root** for this rerun.

Preserve all historical durable roots unchanged.

If the runner source itself does not need modification, preserve the exact runner bytes/hash being validated.

If runner source changes for any reason, generate a new runner hash and revalidate the complete single-artifact contract.

# SV01–SV36 contract

The fresh ledger must satisfy the prior observation-ledger authority in full.

For every SV01–SV36 retain independently:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

`SV_FAIL=0` alone is insufficient.

All 36 records must have substantive independently inspectable observations.

# Required SV checks

Retain independent proof for:

```text
SV01 exact Windows PowerShell version
SV02 parser errors = 0
SV03 exactly P01-P20
SV04 predicate fields complete
SV05 P01-P20 aggregation membership
SV06 unresolved predicates fail closed
SV07 zero structural-time runtime PASS claims
SV08 executable source-wrapper SHA-256
SV09 executable byte-for-byte wrapper copy
SV10 executable copy-pre SHA-256
SV11 pre-RunId source/copy equality assertion
SV12 executable source/copy post hashes
SV13 post equality assertions
SV14 hash mismatch fails closed
SV15 G01-G11 precede RunId
SV16 G01-G11 precede wrapper invocation
SV17 RunId precedes wrapper invocation
SV18 executable S validation
SV19 executable W validation
SV20 executable child S visibility
SV21 executable child W visibility
SV22 child mismatch fails closed
SV23 P03 exact-byte-pre binding
SV24 P04 exact-byte-post binding
SV25 P19 post-cleanup ordering
SV26 P20 observation-only behavior
SV27 minimum shim surface
SV28 unexpected calls fail closed
SV29 production-policy duplication absent
SV30 secret hygiene
SV31 two-root independence
SV32 disposable cleanup
SV33 retained runner survives cleanup
SV34 retained ledger survives cleanup
SV35 retained runner hash reverified
SV36 repository invariants, including explicit git diff --check capture
```

# SV36 specific evidence

SV36 must now retain at least:

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

Known expected repository state remains:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

# P01–P20 contract

Preserve exactly the canonical P01–P20 definitions and required record fields.

No runtime W5 predicate PASS may be claimed under this authority.

# Two-root lifecycle

Preserve:

```text
DisposableRoot != DurableRoot
DurableRoot not descendant of DisposableRoot
persist evidence
delete disposable root
verify disposable absent
verify durable present
verify retained runner present
verify retained ledger present/parseable
re-hash retained runner
finalize durable lifecycle evidence without recreating disposable root
```

# Repository/mutation boundary

This authority introduces:

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

Do not stage, clean, revert, or edit the two pre-existing governed WP04 modified scripts.

# Acceptance threshold

Only accept if:

```text
native git diff --check capture completes
actual exit code retained
fresh SV ledger finalized
SV01-SV36 = exactly 36 unique records
all required fields present
all observations independently inspectable
all evidence references resolvable
all validation methods explicit
all Result = PASS
SV_FAIL = 0
durable evidence survives disposable cleanup
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
```

# Acceptance markers

Only on full PASS emit:

```text
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK NATIVE CAPTURE: PASS
RELEASE 1.12 WP04 — R1 CRLF ADVISORY NONTERMINATING CAPTURE: PASS
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK EXIT CODE RETAINED: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 OBSERVATION-LEDGER CORRECTION: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 INDEPENDENT OBSERVATIONS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 EVIDENCE REFERENCES: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 VALIDATION METHODS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 ALL_PASS: PASS
RELEASE 1.12 WP04 — R1 RUNNER SINGLE-ARTIFACT CONTRACT COMPLETENESS: PASS
RELEASE 1.12 WP04 — R1 RUNNER EXACT-BYTE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRE-RUNID FAIL-CLOSED BOUNDARY: PASS
RELEASE 1.12 WP04 — R1 RUNNER CHILD S/W VISIBILITY: PASS
RELEASE 1.12 WP04 — R1 CANONICAL PREDICATE IDS P01-P20: EXACT
RELEASE 1.12 WP04 — R1 P01-P20 INDIVIDUAL STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — R1 P19 POST-CLEANUP ORDER: PASS
RELEASE 1.12 WP04 — R1 P20 EVIDENCE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHIM SURFACE: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRODUCTION-POLICY DUPLICATION: ABSENT
RELEASE 1.12 WP04 — R1 TWO-ROOT LIFECYCLE: PASS
RELEASE 1.12 WP04 — R1 DURABLE EVIDENCE SURVIVES CLEANUP: PASS
RELEASE 1.12 WP04 — R1 RETAINED RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 RUNNER PARSER: PASS
RELEASE 1.12 WP04 — R1 RUNNER SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — R1 RUNNER TRACKED REPOSITORY MUTATIONS: 0
RELEASE 1.12 WP04 — R1 RUNNER STAGED PATHS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TERRA R1 SV LEDGER FRESH RERUN COMPLETE
```

Report:

- fresh durable candidate root;
- disposable validation root;
- whether runner source changed;
- final runner SHA-256;
- parser result;
- `git diff --check` exact exit code;
- retained CRLF advisory output/classification;
- all 36 SV records or durable ledger path;
- SV failure count;
- repository pre/post state;
- exact mutation accounting.

# Failure boundary

At first failure:

- retain sanitized evidence;
- do not execute W5;
- do not allocate a W5 RunId;
- do not modify production code;
- stop for Luna reconciliation.

# Final stop

**STOP AFTER THE FRESH SV01–SV36 OBSERVATION-LEDGER RERUN.**

Even on PASS:

```text
W5 = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
PUBLICATION BLOCKER = UNRESOLVED
```

A separate GPT-5.6 Luna reconciliation must inspect the finalized fresh structural ledger before any governed W5 execution authority is created.

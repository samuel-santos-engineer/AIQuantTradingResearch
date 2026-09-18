# Release 1.12 WP04 — Terra R1 Executable Controls Restoration Authority

## Authority identity

**Selected execution model: GPT-5.6 Terra**

This is a narrow R1 runner correction and structural revalidation authority.

GPT-5.6 Luna owns the R1 contract and later reconciliation. GPT-5.6 Terra may restore only the executable structural controls omitted from the latest candidate. GPT-5.6 Sol is supporting/non-authoritative only.

## Binding predecessor

Latest candidate:

```text
R1 P01-P20 STRUCTURAL-LEDGER CORRECTION: PASS
R1 CANONICAL PREDICATE IDS P01-P20: EXACT
R1 P01-P20 UNIQUE DEFINITIONS: PASS
R1 P01-P20 REQUIRED RECORD FIELDS: PASS
R1 P01-P20 INDIVIDUAL STRUCTURAL VALIDATION: PASS
R1 P01-P20 FINAL AGGREGATION MEMBERSHIP: PASS
R1 MISSING/UNRESOLVED PREDICATE FAIL-CLOSED: PASS
R1 STRUCTURAL-VS-RUNTIME EVIDENCE SEPARATION: PASS
R1 TWO-ROOT LIFECYCLE: PASS
R1 DISPOSABLE ROOT CLEANUP: PASS
R1 DURABLE ROOT SURVIVES CLEANUP: PASS
R1 RETAINED RUNNER SOURCE SURVIVES CLEANUP: PASS
R1 RETAINED STRUCTURAL LEDGER SURVIVES CLEANUP: PASS
R1 RETAINED RUNNER HASH REVERIFICATION: PASS

R1 RUNNER EXACT-BYTE CONTRACT: FAIL
R1 RUNNER PRE-RUNID FAIL-CLOSED BOUNDARY: FAIL

GOVERNED W5 WRAPPER INVOKED: NO
GOVERNED W5 RUNID ALLOCATED: NO
W5 GOVERNED ACCEPTANCE: NOT_GRANTED
W6/W7/W8: NOT_RUN
PUBLICATION BLOCKER: UNRESOLVED
```

Latest retained candidate:

```text
Durable root:
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\cc40b3f0b1864f7daa2a2099bce8c1ed

Runner SHA-256:
030EC94DF21832EACECFD8072C2ED38D6E23012FBA960DADF9F26A7F2697D583
```

Previous failed durable root must remain preserved:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\08106ad2e6e04389bb55633bd1c8bd65
```

## Proven defect

The latest runner correctly represents P01–P20 but omitted executable controls for:

1. exact-byte source/copy pre/post hash equality; and
2. ordered S/W child-visibility and pre-RunId fail-closed gates.

Because the runner has a new hash, these controls cannot be carried forward from an earlier candidate.

This is a runner structural defect, not a production defect and not a governed W5 failure.

# Mission

Create a NEW R1 candidate that **preserves the successful P01–P20 accounting and two-root lifecycle** while restoring the missing executable controls.

Do not redesign the runner.
Do not narrow or weaken P01–P20.
Do not execute governed W5.
Do not allocate a governed W5 RunId.

## New candidate identity

Do not overwrite either historical durable root.

Create a new durable candidate root and produce a new runner hash.

The new candidate must be self-contained: every required structural contract must be proven against this single new runner artifact/hash.

No contract may be accepted solely by carry-forward from a different runner hash.

# Executable exact-byte contract

The runner source must contain executable Windows PowerShell 5.1-compatible logic that, during a later governed run:

1. resolves the governed production wrapper source;
2. computes its SHA-256;
3. copies it byte-for-byte to `$env:W`;
4. computes `$env:W` pre-execution SHA-256;
5. requires source hash == copy pre hash before RunId allocation;
6. after wrapper execution computes source and copy post hashes;
7. requires:

```text
source pre == copy pre
source pre == source post
copy pre == copy post
source post == copy post
```

8. records hash observations durably for P03/P04;
9. fails closed on any mismatch.

Forbidden:

- patching;
- rewriting;
- preprocessing;
- content normalization;
- semantic replacement of the wrapper.

The structural validator must prove these are executable controls, not comments or predicate-description text.

# Ordered pre-RunId fail-closed boundary

The runner source must executable-enforce this order:

```text
G01 Windows PowerShell version == 5.1.26100.9444
G02 complete runner parser errors == 0
G03 S defined/nonempty
G04 S absolute
G05 S exists and is the expected fresh sandbox
G06 W defined/nonempty
G07 W absolute
G08 W exists and is a file in the expected fresh scenario
G09 child process sees exact S
G10 child process sees exact W
G11 source/copy exact-byte pre-execution hash equality
---------------------------------------------
ONLY AFTER G01–G11 PASS:
G12 allocate fresh governed W5 RunId
G13 invoke exact-byte production wrapper
```

Any failure G01–G11 must structurally terminate before both G12 and G13.

The runner must not allocate a placeholder/diagnostic governed RunId before these gates.

The structural validator must prove ordering from executable control flow, not merely string positions in comments.

# S/W child visibility

The later governed execution must launch the actual Windows PowerShell 5.1 child with:

```text
S = absolute fresh disposable sandbox path
W = absolute exact-byte copied-wrapper path
```

Before RunId allocation, child-side evidence must prove the received values exactly match the parent-established values.

Invalid/missing/mismatched S or W fails closed.

Precompute `Test-Path` Boolean observations before predicate-collection construction where applicable.

# Preserve P01–P20 accounting

The new runner must retain exactly the canonical twenty predicate definitions:

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

For every predicate preserve:

```text
PredicateId
Requirement
ExpectedValue
ObservedEvidence
Result
EvidenceLocation
```

Require exactly 20 unique IDs, P01–P20, and fail closed on missing/unresolved runtime evidence.

No runtime predicate PASS is authorized under this structural authority.

# Preserve P19/P20 contracts

P19 structural order remains:

```text
P01–P18
→ P20
→ durable evidence outside sandbox
→ delete sandbox
→ verify absence
→ evaluate P19
→ finalize P19 without recreating sandbox
→ aggregate P01–P20
```

P20 remains observation/accounting of production-derived error precedence. Do not clone production policy.

# Preserve minimum shim contract

Only the approved minimum interception surface:

```text
git
az
required helper calls
```

Every intercepted call ledgered.
Unexpected calls fail closed.
No production lifecycle state manufactured.

# Two-root lifecycle

Preserve and re-prove for the NEW candidate:

```text
DISPOSABLE ROOT != DURABLE ROOT
durable root not descendant of disposable root
retained runner copied to durable root
retained structural ledger in durable root
delete disposable root
verify disposable root absent
verify durable root present
verify retained runner present
verify retained ledger present and parseable
re-hash retained runner after cleanup
```

Do not delete the new durable root after PASS; Luna must inspect it.

# PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Complete final retained runner parser errors = 0.

# Structural validation

Validate the complete new artifact as one indivisible candidate.

At minimum prove:

```text
SV01 Windows PowerShell 5.1 exact version
SV02 parser errors = 0
SV03 exactly P01-P20 definitions
SV04 required predicate fields complete
SV05 all P01-P20 in final aggregation
SV06 missing/unresolved predicates fail closed
SV07 no structural validation claims runtime predicate PASS
SV08 executable source-wrapper SHA-256 calculation exists
SV09 executable byte-for-byte copy exists
SV10 executable copy-pre SHA-256 calculation exists
SV11 pre-RunId source/copy equality assertion exists
SV12 executable source/copy post hashes exist
SV13 post-execution equality assertions exist
SV14 hash mismatch fails closed
SV15 G01-G11 all precede RunId allocation
SV16 G01-G11 all precede wrapper invocation
SV17 RunId allocation precedes wrapper invocation
SV18 S validation executable
SV19 W validation executable
SV20 child S visibility executable
SV21 child W visibility executable
SV22 child mismatch fails closed
SV23 P03 binds to exact-byte pre evidence
SV24 P04 binds to exact-byte post evidence
SV25 P19 ordering preserved
SV26 P20 remains observation-only
SV27 minimum shim surface preserved
SV28 unexpected calls fail closed
SV29 production-policy duplication absent
SV30 secret hygiene
SV31 two-root independence
SV32 disposable cleanup
SV33 retained runner survives cleanup
SV34 retained ledger survives cleanup
SV35 retained runner hash reverified
SV36 repository invariants
```

More checks are allowed. No required check may be replaced by an aggregate assertion.

# Repository boundary

Expected state:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
git diff --check = PASS with possible CRLF advisories
```

This authority must introduce:

```text
tracked repository mutations = 0
staged paths = 0
commits = 0
pushes = 0
GitHub mutations = 0
production mutations = 0
external mutations = 0
```

Do not alter the pre-existing two governed scripts.

# Required retained evidence

The NEW durable candidate root must retain:

```text
runner.ps1
runner SHA-256 evidence
parser evidence
structural-validation ledger
P01-P20 structural definitions/checks
exact-byte executable-control validation evidence
pre-RunId gate/order validation evidence
two-root lifecycle evidence
```

# Acceptance markers

Only if every contract passes emit:

```text
RELEASE 1.12 WP04 — R1 EXECUTABLE CONTROLS RESTORATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SINGLE-ARTIFACT CONTRACT COMPLETENESS: PASS
RELEASE 1.12 WP04 — R1 RUNNER EXACT-BYTE PRE-RUN CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER EXACT-BYTE POST-RUN CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER HASH-MISMATCH FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 RUNNER S VALIDATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER W VALIDATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER CHILD S/W VISIBILITY: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRE-RUNID G01-G11 ORDER: PASS
RELEASE 1.12 WP04 — R1 RUNNER RUNID-BEFORE-WRAPPER ORDER: PASS
RELEASE 1.12 WP04 — R1 CANONICAL PREDICATE IDS P01-P20: EXACT
RELEASE 1.12 WP04 — R1 P01-P20 REQUIRED RECORD FIELDS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 INDIVIDUAL STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — R1 P01-P20 FINAL AGGREGATION MEMBERSHIP: PASS
RELEASE 1.12 WP04 — R1 MISSING/UNRESOLVED PREDICATE FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 STRUCTURAL-VS-RUNTIME EVIDENCE SEPARATION: PASS
RELEASE 1.12 WP04 — R1 P19 POST-CLEANUP ORDER: PASS
RELEASE 1.12 WP04 — R1 P20 EVIDENCE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHIM SURFACE: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRODUCTION-POLICY DUPLICATION: ABSENT
RELEASE 1.12 WP04 — R1 TWO-ROOT LIFECYCLE: PASS
RELEASE 1.12 WP04 — R1 DISPOSABLE ROOT CLEANUP: PASS
RELEASE 1.12 WP04 — R1 DURABLE ROOT SURVIVES CLEANUP: PASS
RELEASE 1.12 WP04 — R1 RETAINED RUNNER SOURCE SURVIVES CLEANUP: PASS
RELEASE 1.12 WP04 — R1 RETAINED STRUCTURAL LEDGER SURVIVES CLEANUP: PASS
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
RELEASE 1.12 WP04 — TERRA R1 EXECUTABLE CONTROLS RESTORATION COMPLETE
```

Report:

- all historical durable roots preserved;
- new durable candidate root;
- disposable root;
- new runner SHA-256;
- production wrapper SHA-256;
- parser result;
- structural check count and failures;
- explicit SV01–SV36 results;
- P01–P20 definition evidence;
- G01–G13 control-flow evidence;
- repository pre/post state;
- exact mutation accounting.

# Failure boundary

At first failure:

- preserve sanitized durable evidence;
- do not execute W5;
- do not allocate a W5 RunId;
- do not modify production code;
- stop for Luna reconciliation.

# Final stop

**STOP AFTER EXECUTABLE-CONTROLS RESTORATION AND STRUCTURAL VALIDATION.**

Even on PASS:

```text
W5 = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
PUBLICATION BLOCKER = UNRESOLVED
```

A separate GPT-5.6 Luna reconciliation must inspect this single complete retained artifact before governed W5 execution may be authorized.

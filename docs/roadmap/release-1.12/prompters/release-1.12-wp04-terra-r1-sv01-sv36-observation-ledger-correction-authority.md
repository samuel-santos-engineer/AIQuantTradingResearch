# Release 1.12 WP04 — Terra R1 SV01–SV36 Observation-Ledger Correction Authority

## Authority identity

**Selected execution model: GPT-5.6 Terra**

This is a narrow structural-evidence correction authority for the latest R1 runner candidate.

GPT-5.6 Luna owns contract reconciliation and acceptance. GPT-5.6 Terra may correct only the structural-validation evidence defect identified below. GPT-5.6 Sol is supporting/non-authoritative only.

## Binding predecessor

Latest result:

```text
RELEASE 1.12 WP04 — R1 EXECUTABLE CONTROLS RESTORATION: FAIL
RELEASE 1.12 WP04 — R1 RUNNER SINGLE-ARTIFACT CONTRACT COMPLETENESS: FAIL
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
```

Latest retained candidate:

```text
Durable root:
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\56109295ac4448498a49a8605c5fdcbc

Runner SHA-256:
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8

P01-P20 count: 20
Predicate structural-definition failures: 0
Disposable root removed: True
Durable root survives: True
Retained runner survives: True
Retained ledger survives: True
Staged paths: 0
Governed W5 wrapper invoked: NO
Governed W5 RunId allocated: NO
```

## Proven defect

The structural ledger represented SV01–SV36 as passing but did not retain an independently inspectable observed proof for each structural check.

Therefore:

```text
SV_FAIL=0
```

is insufficient.

This is a **structural-evidence ledger defect**. It is not a production defect and not a governed W5 failure.

# Mission

Correct the structural validator/evidence ledger so that **every SV01–SV36 check has its own durable observed proof**, then validate one complete R1 runner candidate.

Do not claim PASS merely because all check booleans aggregate to zero failures.

**Do not execute governed W5.**
**Do not allocate a governed W5 RunId.**

# Candidate-integrity rule

First inspect whether the retained runner at hash:

```text
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
```

already contains the required executable controls and whether only the structural validator/ledger is deficient.

If and only if the runner source itself requires **no change**, preserve that runner byte-for-byte and retain the same runner hash while generating a new validation-evidence candidate root.

If runner source must change for any reason:

- create a new durable candidate root;
- generate a new runner SHA-256;
- revalidate the entire single-artifact contract;
- do not carry structural acceptance from the old hash.

Do not overwrite historical durable roots.

# Required per-SV record schema

For every `SV01` through `SV36`, durable evidence must contain an independent record with at least:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

Equivalent field names are permitted only with an explicit lossless mapping.

Rules:

- `CheckId` must be exactly one of SV01–SV36.
- `Requirement` must describe the actual invariant.
- `Expected` must be concrete.
- `Observed` must contain the actual observation, not merely `true`, `pass`, or a restatement of Expected when richer evidence is available.
- `Result` must be PASS or FAIL.
- `EvidenceReference` must identify the retained source location, source lines/range, parsed structure, hash record, lifecycle record, or other durable evidence supporting the observation.
- `ValidationMethod` must state how the observation was derived.

Missing, null, unresolved, duplicate, or uninspectable records fail closed.

# Evidence quality rule

A valid structural observation must expose enough information for Luna to independently understand why the check passed.

Examples:

```text
BAD:
Observed = true

BAD:
Observed = PASS

BAD:
Observed = "G01-G11 precede RunId"

GOOD:
Observed = {
  GateSequence = ["G01",...,"G11","G12","G13"],
  RunIdAllocationStatement = "<source reference>",
  WrapperInvocationStatement = "<source reference>",
  EarliestRunIdIndex = ...,
  EarliestWrapperIndex = ...,
  AllRequiredGateIndexes = {...}
}
```

For source-code structural checks, prefer parsed/tokenized/AST-backed or exact source-reference evidence over comments or loose text matching.

Comments alone cannot prove executable control flow.

# Required SV01–SV36 checks

Retain independent observed proof for each:

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
SV36 repository invariants preserved
```

# Specific observation expectations

## SV01–SV02

Retain:

```text
actual $PSVersionTable.PSVersion
actual parser error count
parser evidence location
```

## SV03–SV07

Retain:

```text
actual canonical predicate ID list
count
duplicate/missing/extra analysis
field-presence matrix
aggregation membership list
fail-closed unresolved-evidence mechanism
runtime PASS claim count
```

## SV08–SV14

Retain source-backed evidence for executable hash/copy controls, including:

```text
source-hash operation reference
copy operation reference
copy-pre-hash reference
pre-equality assertion reference
source-post-hash reference
copy-post-hash reference
post-equality assertions
mismatch failure branch/reference
```

Do not accept comments as the sole evidence.

## SV15–SV17

Retain a control-order observation showing:

```text
G01 ... G11
G12 RunId allocation
G13 wrapper invocation
```

and the executable source references/order used to prove:

```text
all G01-G11 < G12 < G13
```

## SV18–SV22

Retain executable source evidence for:

```text
S nonempty/absolute/fresh/existing checks
W nonempty/absolute/existing/file/expected checks
child receives S
child receives W
child exact-value comparison
mismatch termination
```

## SV23–SV26

Retain the actual binding references proving:

```text
P03 <- exact-byte pre evidence
P04 <- exact-byte post evidence
P19 <- post-cleanup verification
P20 <- observed production-derived precedence evidence
```

No production policy clone for P20.

## SV27–SV30

Retain:

```text
actual shim allowlist
unexpected-call failure mechanism
production-policy-duplication inspection evidence
secret-hygiene inspection evidence
```

## SV31–SV35

Retain actual paths and lifecycle observations:

```text
DisposableRoot
DurableRoot
IsDurableDescendantOfDisposable = False
DisposableExistsAfterCleanup = False
DurableExistsAfterCleanup = True
RunnerExistsAfterCleanup = True
LedgerExistsAfterCleanup = True
RunnerHashBeforeCleanup
RunnerHashAfterCleanup
HashEqual = True
```

## SV36

Retain actual read-only repository observations:

```text
modified tracked paths before
modified tracked paths after
staged paths before/after
git diff --check result
authority-introduced tracked mutations
```

# Single-artifact completeness

The final durable ledger must allow Luna to inspect, for the SAME runner hash:

- P01–P20 definitions/accounting;
- executable exact-byte controls;
- G01–G13 ordering;
- S/W visibility;
- P19/P20 bindings;
- shim/fail-closed behavior;
- two-root lifecycle;
- parser/version;
- secret hygiene;
- repository invariants.

No cross-hash structural carry-forward.

# P01–P20 contract remains unchanged

Require exactly P01–P20 and their prior record schema.

No runtime predicate PASS may be claimed in this authority.

# Two-root lifecycle remains binding

Preserve historical durable evidence roots.

Use a disposable validation root independent from the durable evidence root.

After validation:

1. persist complete per-SV evidence durably;
2. remove disposable root;
3. verify removal;
4. verify durable evidence survives;
5. re-open and parse the durable ledger;
6. re-hash retained runner;
7. finalize lifecycle observations durably without recreating disposable root.

# PowerShell baseline

Exactly:

```text
Windows PowerShell 5.1.26100.9444
```

# Repository boundary

Expected predecessor:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
git diff --check = PASS with possible CRLF advisories
```

This authority introduces:

```text
tracked repository mutations = 0
staged paths = 0
commits = 0
pushes = 0
GitHub mutations = 0
production mutations = 0
external mutations = 0
```

# Acceptance threshold

Acceptance requires:

```text
SV01–SV36 = 36 unique records
all required fields present
all Observed values independently inspectable
all EvidenceReference values resolvable within retained durable evidence/source
all ValidationMethod values explicit
all Result = PASS
SV_FAIL = 0
```

`SV_FAIL=0` alone is never sufficient.

# Acceptance markers

Only if every gate passes emit:

```text
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
RELEASE 1.12 WP04 — TERRA R1 STRUCTURAL OBSERVATION CORRECTION COMPLETE
```

Report:

- whether runner source changed;
- prior runner hash;
- final runner hash;
- prior durable root preserved;
- final durable root;
- disposable validation root;
- parser evidence;
- all 36 SV records or a durable location containing them;
- structural failure count;
- P01–P20 definition evidence;
- two-root lifecycle evidence;
- repository pre/post state;
- exact mutation accounting.

# Failure boundary

At the first failure:

- retain sanitized evidence;
- do not execute W5;
- do not allocate a W5 RunId;
- do not modify production code;
- stop for Luna reconciliation.

# Final stop

**STOP AFTER SV01–SV36 OBSERVATION-LEDGER CORRECTION AND STRUCTURAL REVALIDATION.**

Even on PASS:

```text
W5 = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
PUBLICATION BLOCKER = UNRESOLVED
```

A separate GPT-5.6 Luna reconciliation must inspect the retained per-SV evidence before any governed W5 execution authority is created.

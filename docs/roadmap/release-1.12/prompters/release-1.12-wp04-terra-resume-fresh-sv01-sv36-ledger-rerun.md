# Release 1.12 WP04 — Terra Resume Fresh SV01–SV36 Durable-Ledger Rerun

## Authority identity

**Selected execution model: GPT-5.6 Terra**

This authority resumes the already-authorized structural-evidence work after the native `git diff --check` capture correction was proven.

GPT-5.6 Luna owns reconciliation and acceptance. GPT-5.6 Terra owns this fresh structural-ledger rerun only. GPT-5.6 Sol is supporting/non-authoritative only.

## Binding predecessor result

The native capture correction is proven:

```text
git diff --check exit code: 0
output retained: only the two known CRLF advisory warnings
classification: advisory-only; not a whitespace/diff failure
scoped $ErrorActionPreference='Continue': restored immediately after Git call

governed W5 wrapper invoked: NO
governed W5 RunId allocated: NO
repository mutation: 0
external mutation: 0
```

This capture proof is valid carry-forward evidence for the fresh structural-ledger rerun.

It does **not** grant SV01–SV36 acceptance by itself.

## Mission

Complete the fresh SV01–SV36 durable observation-ledger rerun required by the prior authority.

Do not redesign the runner.
Do not repeat the native-capture diagnostic merely for convenience.
Do not claim acceptance from aggregate booleans.

**Do not execute governed W5.**
**Do not allocate a governed W5 RunId.**

## Fresh candidate root

Use a fresh durable candidate root for this structural-ledger rerun.

Preserve every historical durable root.

The disposable validation root must be independent from the durable root and must be removed after durable evidence is complete.

If the runner source does not require modification, preserve its exact bytes/hash.

If runner source changes for any reason, generate a new hash and revalidate every structural contract against that new hash.

## Windows PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Complete final retained runner parser errors must equal 0.

## Native Git capture — binding implementation

Use the proven operation-scoped capture behavior:

- capture complete `git diff --check` output;
- capture `$LASTEXITCODE` immediately;
- known CRLF advisory text must not terminate the writer;
- restore error handling immediately;
- retain output and exit code;
- actual nonzero exit code fails closed.

Expected current observation:

```text
exit code = 0
output = two known CRLF advisory warnings
classification = ADVISORY_ONLY
```

If the fresh rerun observes materially different output or a nonzero exit code, do not silently carry forward the prior diagnostic result; evaluate the fresh observation and fail closed where required.

## SV01–SV36 durable record schema

Produce exactly 36 unique records, `SV01` through `SV36`.

Each must retain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

No field may be missing, null, or unresolved.

`Observed = true`, `Observed = PASS`, or restating Expected is insufficient when richer evidence exists.

Every EvidenceReference must resolve against retained durable evidence or the retained runner source.

## Required checks

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
SV36 repository invariants preserved, including explicit git diff --check evidence
```

## Evidence requirements

For source/control-flow checks, retain executable source references, parsed/tokenized/AST-backed evidence, or equivalent independently inspectable observations. Comments alone do not prove executable behavior.

For SV15–SV17, retain the actual observed executable order proving:

```text
G01 ... G11 < G12 RunId allocation < G13 wrapper invocation
```

For SV31–SV35 retain actual roots, existence observations, and pre/post cleanup hashes.

For SV36 retain:

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

## P01–P20 structural contract

The same runner hash must retain exactly canonical P01–P20 definitions and required accounting fields.

No runtime W5 predicate PASS may be claimed.

The structural ledger must distinguish structural contract validation from governed runtime predicate results.

## P19/P20 preservation

P19 remains:

```text
P01-P18
→ P20
→ durable evidence outside sandbox
→ delete sandbox
→ verify absence
→ evaluate P19
→ finalize P19 without recreating sandbox
→ aggregate P01-P20
```

P20 remains observation of production-derived error-precedence evidence; no production-policy cloning.

## Two-root completion order

1. create fresh disposable validation root;
2. use independent durable candidate root;
3. produce complete SV01–SV36 evidence;
4. persist retained runner/source/hash and ledger durably;
5. delete disposable root;
6. verify disposable root absent;
7. verify durable root present;
8. verify retained runner present;
9. re-hash retained runner;
10. verify retained ledger exists and parses;
11. finalize lifecycle observations in durable root without recreating disposable root.

## Repository boundary

Expected state:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

This authority must introduce:

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

Do not clean, revert, stage, or edit the two pre-existing governed scripts.

## Acceptance threshold

Acceptance requires all of:

```text
SV records = exactly 36 unique IDs SV01-SV36
all required fields complete
all Observed evidence independently inspectable
all EvidenceReference values resolvable
all ValidationMethod values explicit
all Result = PASS
SV_FAIL = 0
P01-P20 structural contract = PASS
two-root lifecycle = PASS
retained evidence survives cleanup = PASS
git diff --check fresh capture = exit 0 / governed PASS
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
```

## Acceptance markers

Only on full PASS emit:

```text
RELEASE 1.12 WP04 — R1 FRESH SV01-SV36 DURABLE-LEDGER RERUN: PASS
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK NATIVE CAPTURE: PASS
RELEASE 1.12 WP04 — R1 CRLF ADVISORY NONTERMINATING CAPTURE: PASS
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK EXIT CODE RETAINED: PASS
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
RELEASE 1.12 WP04 — TERRA R1 FRESH STRUCTURAL LEDGER RERUN COMPLETE
```

Report the fresh durable root, disposable root, runner hash, parser evidence, fresh Git capture, complete SV ledger location, SV failure count, repository pre/post state, and exact mutation accounting.

## Failure boundary

At first failure:

- retain sanitized durable evidence;
- do not execute W5;
- do not allocate a W5 RunId;
- do not modify production code;
- stop for Luna reconciliation.

## Final stop

**STOP AFTER THE FRESH SV01–SV36 DURABLE-LEDGER RERUN.**

Even on PASS:

```text
W5 = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
PUBLICATION BLOCKER = UNRESOLVED
```

A separate GPT-5.6 Luna reconciliation must inspect the finalized retained ledger before governed W5 execution may be authorized.

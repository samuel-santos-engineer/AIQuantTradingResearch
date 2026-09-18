# Release 1.12 WP04 — Terra R1 P01–P20 Structural-Ledger Correction Authority

## Authority identity

**Selected execution model: GPT-5.6 Terra**

This is a narrow R1 runner/ledger correction and structural revalidation authority.

The two-root durable-evidence lifecycle is already proven. Do not redesign it.

GPT-5.6 Luna owns the R1 contract and later reconciliation. GPT-5.6 Terra may correct only the proven P01–P20 structural-accounting gap. GPT-5.6 Sol is supporting/non-authoritative only.

## Binding predecessor

Latest Terra result:

```text
R1 DURABLE-EVIDENCE REMEDIATION: FAIL
R1 TWO-ROOT LIFECYCLE: PASS
R1 DISPOSABLE ROOT CLEANUP: PASS
R1 DURABLE ROOT SURVIVES CLEANUP: PASS
R1 RETAINED RUNNER SOURCE SURVIVES CLEANUP: PASS
R1 RETAINED STRUCTURAL LEDGER SURVIVES CLEANUP: PASS
R1 RETAINED RUNNER HASH REVERIFICATION: PASS
R1 RUNNER P01-P20 INDIVIDUAL ACCOUNTING: FAIL

GOVERNED W5 WRAPPER INVOKED: NO
GOVERNED W5 RUNID ALLOCATED: NO
W5 GOVERNED ACCEPTANCE: NOT_GRANTED
W6/W7/W8: NOT_RUN
PUBLICATION BLOCKER: UNRESOLVED
```

Durable evidence root:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\08106ad2e6e04389bb55633bd1c8bd65
```

Retained runner SHA-256:

```text
7E26985539912E7D5C462B311D3F86CE9642C3A41740CD6E2B3802DBE1F4FCAF
```

That hash identifies the failed structural candidate only. If runner source changes, generate and report a new hash.

## Proven defect

The retained structural ledger contains only 9 structural checks and does not independently record the complete P01–P20 predicate definitions with their required record fields.

This is a **runner/structural-ledger contract defect**, not a production defect and not a W5 execution failure.

## Mission

Correct only the R1 runner/structural-validation implementation necessary to make the complete P01–P20 accounting contract explicit, independent, durable, and machine-verifiable.

Preserve the already-proven two-root lifecycle.

**Do not execute governed W5.**
**Do not allocate a governed W5 RunId.**

## Required P01–P20 definitions

The retained runner source and durable structural ledger must independently represent all twenty predicates:

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

For **each** predicate, the runner's accounting schema must support and the structural ledger must explicitly validate:

```text
PredicateId
Requirement
ExpectedValue
ObservedEvidence
Result
EvidenceLocation
```

Equivalent field names are allowed only if the mapping is explicit and lossless.

`Result` must support at least PASS/FAIL and must not silently treat missing evidence as PASS.

## Structural ledger requirement

Do not merely add a single check saying "P01-P20 exist."

The durable structural ledger must contain independently inspectable validation for every predicate definition.

At minimum, for each P01 through P20 prove:

```text
definition exists
ID is unique and exact
requirement is non-empty
expected value is defined
observed/evidence slot exists
result slot exists
evidence-location slot exists
predicate participates in final aggregation
```

The structural ledger may summarize these subchecks, but it must retain per-predicate evidence sufficient for Luna to inspect all twenty individually.

Require exactly 20 unique canonical predicate IDs:

```text
P01 ... P20
```

No missing IDs.
No duplicate IDs.
No extra ID may substitute for a canonical predicate.

## Runtime-vs-structural distinction

This authority validates **representation and control flow only**.

Do not fabricate runtime W5 observations.

For structural validation:

- `ObservedEvidence` may be an explicit runtime-populated placeholder/schema binding;
- `Result` may be an explicit runtime-populated placeholder/schema binding;
- `EvidenceLocation` may be an explicit runtime-populated binding.

The ledger must distinguish:

```text
STRUCTURAL_CONTRACT_VALIDATED
```

from:

```text
RUNTIME_PREDICATE_PASS
```

No P01–P20 runtime PASS may be claimed under this authority.

## P01 correction preservation

P01 is runner/harness execution-environment evidence.

It must prove at governed runtime:

```text
Windows PowerShell 5.1.26100.9444
```

It is not required to be literal text/code in the production wrapper.

## P20 correction preservation

P20 is runner accounting of production-derived error-precedence evidence.

It must later observe evidence proving:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE = eligible under production behavior
EvidenceCheckpoint = PASS
RestoreOnly = exactly once
No later restoration/lifecycle condition incorrectly replaces SUCCESS
```

The runner must not clone production archive/extraction/error-precedence policy to generate P20.

## P19 ordering preservation

The correction must preserve structurally:

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

The final aggregation must require all 20 canonical predicate records to exist and be PASS at governed runtime.

Missing/null/unresolved predicate evidence must fail closed.

## Two-root lifecycle — carry forward and re-prove

Preserve:

```text
DISPOSABLE ROOT != DURABLE EVIDENCE ROOT
durable root is not descendant of disposable root
disposable root removed
durable root survives
retained runner source survives
retained structural ledger survives and parses
```

For this corrected candidate, create a new durable candidate evidence directory or otherwise preserve the failed candidate evidence without overwriting its historical record.

Do not delete the previous failed durable evidence root.

## PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

Complete final retained runner parser errors must equal 0.

## Other R1 contracts — preserve and revalidate

Revalidate:

```text
exact-byte production-wrapper contract
pre-RunId fail-closed boundary
S/W child visibility gates
minimum git/az/helper shim surface
unexpected-call fail-closed behavior
production-policy duplication absent
P19 ordering
P20 observation-only behavior
secret hygiene
durable evidence topology
repository invariants
```

No broad refactor.

## Repository boundary

Expected predecessor:

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

Do not alter, clean, revert, or stage the two pre-existing modified scripts.

## Required durable artifacts after cleanup

The corrected durable candidate root must survive and contain at least:

```text
runner.ps1
runner hash evidence
parser evidence
structural-validation ledger
per-predicate P01-P20 structural definitions/checks
durable-lifecycle validation evidence
```

The disposable implementation/validation root must be removed.

After removal, re-open/re-parse the durable structural ledger and re-hash the retained runner.

## Acceptance markers

Only if all gates pass emit:

```text
RELEASE 1.12 WP04 — R1 P01-P20 STRUCTURAL-LEDGER CORRECTION: PASS
RELEASE 1.12 WP04 — R1 CANONICAL PREDICATE IDS P01-P20: EXACT
RELEASE 1.12 WP04 — R1 P01-P20 UNIQUE DEFINITIONS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 REQUIRED RECORD FIELDS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 INDIVIDUAL STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — R1 P01-P20 FINAL AGGREGATION MEMBERSHIP: PASS
RELEASE 1.12 WP04 — R1 MISSING/UNRESOLVED PREDICATE FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 STRUCTURAL-VS-RUNTIME EVIDENCE SEPARATION: PASS
RELEASE 1.12 WP04 — R1 P01 ACCOUNTING CORRECTION: PASS
RELEASE 1.12 WP04 — R1 P20 ACCOUNTING CORRECTION: PASS
RELEASE 1.12 WP04 — R1 P19 POST-CLEANUP ORDER: PASS
RELEASE 1.12 WP04 — R1 TWO-ROOT LIFECYCLE: PASS
RELEASE 1.12 WP04 — R1 DISPOSABLE ROOT CLEANUP: PASS
RELEASE 1.12 WP04 — R1 DURABLE ROOT SURVIVES CLEANUP: PASS
RELEASE 1.12 WP04 — R1 RETAINED RUNNER SOURCE SURVIVES CLEANUP: PASS
RELEASE 1.12 WP04 — R1 RETAINED STRUCTURAL LEDGER SURVIVES CLEANUP: PASS
RELEASE 1.12 WP04 — R1 RETAINED RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 RUNNER PARSER: PASS
RELEASE 1.12 WP04 — R1 RUNNER EXACT-BYTE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRE-RUNID FAIL-CLOSED BOUNDARY: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHIM SURFACE: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRODUCTION-POLICY DUPLICATION: ABSENT
RELEASE 1.12 WP04 — R1 RUNNER SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — R1 RUNNER TRACKED REPOSITORY MUTATIONS: 0
RELEASE 1.12 WP04 — R1 RUNNER STAGED PATHS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TERRA R1 P01-P20 STRUCTURAL CORRECTION COMPLETE
```

Report:

- previous failed durable root (preserved);
- corrected candidate durable root;
- disposable root;
- corrected runner SHA-256;
- parser result;
- production-wrapper SHA-256;
- total structural-check count;
- per-predicate P01–P20 structural evidence;
- retained ledger path;
- lifecycle survival checks;
- repository pre/post state;
- exact mutation accounting.

## Failure boundary

On first failure:

- preserve sanitized durable evidence;
- do not execute W5;
- do not allocate a W5 RunId;
- do not modify production code;
- stop for Luna reconciliation.

## Final stop

**STOP AFTER THE P01–P20 STRUCTURAL-LEDGER CORRECTION AND REVALIDATION.**

Even on PASS:

```text
W5 = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
PUBLICATION BLOCKER = UNRESOLVED
```

A separate GPT-5.6 Luna reconciliation must inspect the corrected retained runner and complete per-predicate ledger before governed W5 execution may be authorized.

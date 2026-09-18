# Release 1.12 WP04 — Terra R1 Durable-Evidence Remediation & Reimplementation Authority

## Authority identity

**Selected execution model: GPT-5.6 Terra**

This is a narrow runner reimplementation and structural-validation authority following Luna's failed reconciliation.

GPT-5.6 Luna owns the accepted R1 contract and reconciliation. GPT-5.6 Terra may reimplement the disposable runner and correct the proven durable-evidence lifecycle defect. GPT-5.6 Sol is supporting/non-authoritative only.

## Binding Luna result

```text
RELEASE 1.12 WP04 — R1 RUNNER IMPLEMENTATION RECONCILIATION: FAIL

R1 RUNNER ARTIFACT IDENTITY: FAIL
R1 RUNNER DURABLE SOURCE SURVIVES CLEANUP: FAIL
R1 RUNNER DURABLE EVIDENCE CONTRACT: FAIL
R1 RUNNER REPOSITORY/MUTATION BOUNDARY: PASS

RECONCILED RUNNER SHA-256: NOT_PROVEN
GOVERNED W5 EXECUTION USING RECONCILED RUNNER: NOT_AUTHORIZED

W5 GOVERNED ACCEPTANCE: NOT_GRANTED
W6/W7/W8: NOT_RUN
PUBLICATION BLOCKER: UNRESOLVED
LUNA RECONCILIATION MUTATIONS: 0
```

Root defect:

```text
The runner source and structural ledger were placed beneath the same temporary root
that was subsequently deleted.

"durable evidence" therefore did not survive cleanup.
```

Do not treat the previous runner hash as a reconciled artifact. Reimplementation must produce a new artifact identity/hash.

## Mission

Reimplement the Luna-approved R1 runner with a corrected **two-root lifecycle**:

```text
DISPOSABLE ROOT
    runner working copy
    implementation-validation scratch
    later governed W5 sandbox

DURABLE EVIDENCE ROOT
    retained runner source
    retained runner SHA-256
    parser evidence
    structural-validation ledger
    contract-validation evidence
```

The durable evidence root MUST NOT be a descendant of the disposable root.

Deleting the disposable root MUST leave the durable evidence root and every required retained artifact intact.

**Do not execute governed W5.**
**Do not allocate a governed W5 RunId.**

## Durable-root selection

Use a stable local evidence location that is independent of the disposable scenario and is not removed by runner cleanup.

Prefer the existing project-governed WP04 durable evidence convention if one already exists and can be used without repository mutation.

Otherwise use a stable per-user local evidence location outside the disposable root, for example under:

```text
%LOCALAPPDATA%\AIQuantTradingResearch\wp04\...
```

Do not use a child of a disposable `%TEMP%\wp04-r1-<id>` root as the durable evidence root.

Record the exact durable root chosen and why it survives cleanup.

## R1 architecture remains unchanged

```text
Selected architecture = R1
Tracked runner mutation required = NO
Runner = disposable/local-only, outside tracked repository content
```

Do not convert this remediation into R2 or R3.

## Reimplementation requirements

Recreate the runner from the accepted Luna contract, not from an assumed surviving prior artifact.

The runner must structurally support:

- fresh disposable sandbox creation;
- exact-byte production-wrapper copy;
- Windows PowerShell 5.1 validation;
- complete runner parser validation;
- S/W validation and child visibility;
- fail-closed narrow shims;
- fresh RunId allocation only after pre-gates;
- exact production-wrapper invocation under a later authority;
- independent P01–P20 accounting;
- durable sanitized evidence outside sandbox;
- P19 only after verified sandbox deletion;
- final aggregation only after P01–P20 individual records exist.

No governed W5 execution is permitted here.

## PowerShell baseline

Require exactly:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7 assumptions.

## Minimum shim surface

Only:

```text
git
az
required helper calls
```

Every intercepted call must be ledgered.

Unexpected calls fail closed.

Do not duplicate production archive/extraction/lifecycle policy.

Do not manufacture:

```text
RETRIEVAL_FAILED
NOT_APPLICABLE
EvidenceCheckpoint PASS
SUCCESS
RestoreOnly=1
```

## Exact-byte contract

Structurally require:

```text
source wrapper SHA-256 == execution copy pre SHA-256
source wrapper SHA-256 == execution copy post SHA-256
execution pre SHA-256 == execution post SHA-256
```

No patching, rewriting, preprocessing, or semantic replacement.

## Pre-RunId boundary

RunId allocation must be structurally after:

```text
PowerShell version PASS
complete runner parser PASS
S validation PASS
W validation PASS
child S visibility PASS
child W visibility PASS
exact-byte pre-execution identity PASS
```

Pre-gate failure means:

```text
no W5 RunId
no wrapper invocation
no W5 acceptance credit
```

## P01–P20 contract

Preserve all twenty individual predicates exactly:

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

P01 and P20 belong to runner accounting/evidence; they need not be literal code/text in the production wrapper.

P20 must observe production-derived evidence and must not clone production policy.

## P19 ordering

Structurally enforce:

```text
P01–P18
→ P20
→ persist durable sanitized evidence to DURABLE ROOT
→ delete disposable sandbox/root as applicable
→ verify disposable path absence
→ evaluate P19
→ finalize P19 in DURABLE ROOT without recreating disposable path
→ aggregate P01–P20
```

## Critical remediation validation

This authority must prove the lifecycle defect is fixed by actually testing the storage topology without running W5:

1. create disposable implementation root;
2. create independent durable evidence root;
3. create runner;
4. copy finalized runner source to durable root;
5. hash both and require exact identity;
6. write parser/structural ledger to durable root;
7. delete disposable implementation root;
8. verify disposable root absent;
9. verify durable root still exists;
10. verify retained runner source still exists;
11. re-hash retained runner source and require equality;
12. verify structural ledger still exists and parses;
13. verify no cleanup step targets the durable root.

This is implementation-lifecycle validation, not governed W5 execution.

## Repository/mutation boundary

Expected repository predecessor:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
git diff --check = PASS except possible CRLF advisory warnings
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

Do not clean/revert/stage the pre-existing two modified scripts.

## Secret hygiene

Runner and retained evidence must not persist or print:

- secrets;
- Twelve Data API key;
- Azure credentials;
- registry credentials;
- temporary evidence tokens.

Use sanitized identities only.

## Required retained artifacts

At completion, the durable root MUST still contain at least:

```text
runner.ps1
runner SHA-256 evidence
parser evidence
structural-validation.json (or equivalent durable structured ledger)
durable-lifecycle-validation evidence
```

Do NOT delete these retained artifacts after reporting success. They are required for the subsequent Luna reconciliation.

The disposable implementation root MUST be deleted.

## Required structural validation

Require:

```text
Windows PowerShell 5.1.26100.9444 = PASS
complete retained runner parser errors = 0
exact-byte wrapper contract = PASS
pre-RunId fail-closed boundary = PASS
shim allowlist/fail-closed behavior = PASS
production-policy duplication = ABSENT
P01-P20 individually represented = PASS
P01/P20 accounting correction preserved = PASS
P19 structural ordering = PASS
P20 production-observation contract = PASS
secret hygiene = PASS
durable/disposable root independence = PASS
disposable cleanup = PASS
retained runner survives cleanup = PASS
retained structural ledger survives cleanup = PASS
repository invariants = PASS
```

## Acceptance markers

Only if every gate passes emit:

```text
RELEASE 1.12 WP04 — R1 DURABLE-EVIDENCE REMEDIATION: PASS
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
RELEASE 1.12 WP04 — R1 RUNNER P01-P20 INDIVIDUAL ACCOUNTING: PASS
RELEASE 1.12 WP04 — R1 RUNNER P01/P20 ACCOUNTING CORRECTION: PASS
RELEASE 1.12 WP04 — R1 RUNNER P19 POST-CLEANUP ORDER: PASS
RELEASE 1.12 WP04 — R1 RUNNER P20 EVIDENCE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — R1 RUNNER TRACKED REPOSITORY MUTATIONS: 0
RELEASE 1.12 WP04 — R1 RUNNER STAGED PATHS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TERRA R1 DURABLE-EVIDENCE REIMPLEMENTATION COMPLETE
```

Report:

- disposable root;
- durable root;
- retained runner source path;
- new runner SHA-256;
- retained structural ledger path;
- parser evidence;
- structural-check count and failures;
- lifecycle survival checks;
- production wrapper SHA-256 used for structural contract;
- repository pre/post state;
- exact mutation accounting.

## Failure boundary

On first failure:

- preserve whatever sanitized durable diagnostic evidence is valid;
- do not execute W5;
- do not allocate a W5 RunId;
- do not remediate production code;
- stop for Luna reconciliation.

## Final stop

**STOP AFTER R1 REIMPLEMENTATION AND DURABLE-EVIDENCE STRUCTURAL VALIDATION.**

Even on PASS:

```text
W5 = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
PUBLICATION BLOCKER = UNRESOLVED
```

A new GPT-5.6 Luna reconciliation must verify the retained runner and ledger actually exist after cleanup before governed W5 execution can be authorized.

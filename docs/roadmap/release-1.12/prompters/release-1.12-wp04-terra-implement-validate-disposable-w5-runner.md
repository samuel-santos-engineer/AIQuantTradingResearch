# Release 1.12 WP04 — Terra Disposable W5 Runner Implementation & Structural Validation Authority

## Authority identity

**Selected execution model: GPT-5.6 Terra**

This authority implements and structurally validates the Luna-approved **R1 disposable W5 runner only**.

GPT-5.6 Luna owns the accepted contract and later reconciliation. GPT-5.6 Terra owns the implementation and validation permitted here. GPT-5.6 Sol is supporting/non-authoritative only.

## Binding predecessor

Luna reconciliation:

```text
RELEASE 1.12 WP04 — DISPOSABLE W5 RUNNER CONTRACT RECONCILIATION: PASS
SELECTED RUNNER ARCHITECTURE: R1
TRACKED RUNNER MUTATION REQUIRED: NO
W5 GOVERNED ACCEPTANCE: NOT_GRANTED
W6/W7/W8: NOT_RUN
PUBLICATION BLOCKER: UNRESOLVED
LUNA AUTHORITY MUTATIONS: 0
```

R1 means:

```text
Disposable local-only runner outside tracked repository content.
Retain runner source/hash and sanitized validation ledger under durable evidence root.
Remove disposable runner and validation sandbox after evidence retention.
```

## Mission

Create the R1 runner outside tracked repository content and prove that its structure satisfies the Luna contract.

**Do not execute governed W5 under this authority.**
**Do not allocate a governed W5 RunId.**

## PowerShell contract

Target exactly:

```text
Windows PowerShell 5.1.26100.9444
```

No PowerShell 7 assumptions or PS7-only syntax/APIs.

## Runner responsibilities

The runner implementation must be capable, under a later execution authority, of:

- creating a fresh sandbox;
- copying the governed production wrapper byte-for-byte;
- validating PowerShell/parser/S-W child visibility;
- installing only narrow fail-closed W5 shims;
- allocating a fresh W5 RunId only after all pre-gates pass;
- invoking the exact wrapper;
- recording P01–P20 independently;
- persisting durable sanitized evidence outside the sandbox;
- deleting the sandbox;
- evaluating P19 only after verified deletion;
- aggregating only after all individual predicates are recorded.

Implementation here must structurally support these operations without performing the governed wrapper execution.

## Prohibited responsibilities

The runner must not:

- duplicate production archive/extraction policy;
- manufacture lifecycle results;
- implement a second persistence path;
- modify production source/helper code;
- patch/rewrite the production wrapper;
- perform Azure/Docker/GHCR/GitHub mutations;
- configure Twelve Data;
- stage, commit, push, create/merge PRs;
- execute W6/W7/W8.

## Minimum shim surface

Implement only fail-closed interception/ledger mechanics needed for the later W5 fixture:

```text
git
az
required helper calls
```

Requirements:

- every governed intercepted call is ledgered;
- unexpected calls fail closed;
- unexpected real external calls are acceptance-failing;
- shims do not derive or inject `RETRIEVAL_FAILED`, `NOT_APPLICABLE`, `PASS`, `SUCCESS`, or `RestoreOnly=1`;
- production wrapper remains responsible for those results.

## Exact-byte contract

Structurally implement pre/post SHA-256 validation for the governed wrapper and execution copy:

```text
source hash == copied-wrapper pre hash
source hash == copied-wrapper post hash
pre hash == post hash
```

No wrapper preprocessing or semantic replacement.

## Pre-RunId fail-closed boundary

Runner source must make RunId allocation unreachable until all of these succeed:

```text
PowerShell version gate
complete runner parser gate
S absolute/fresh/existing validation
W absolute/existing/file/expected-location validation
child S visibility
child W visibility
exact-byte pre-execution gate
```

For any pre-gate failure, source must structurally provide:

```text
no W5 RunId
no wrapper invocation
no W5 acceptance credit
```

## P01–P20 implementation contract

Implement separate durable accounting for:

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

Each record must have:

```text
Predicate ID
Requirement
Expected value
Observed value/evidence
PASS/FAIL
Evidence location
```

No aggregate result may substitute for an individual predicate.

## P19 structural order

The implementation must enforce:

```text
P01–P18
→ P20
→ durable sanitized evidence outside sandbox
→ delete sandbox
→ verify absence
→ evaluate P19
→ persist/finalize P19 without recreating sandbox
→ aggregate P01–P20
```

Prove structurally that P19 cannot be evaluated before cleanup.

## P20 structural contract

Do not duplicate the production policy. Implement evidence/accounting sufficient for a later run to prove:

```text
RETRIEVAL_FAILED + NOT_APPLICABLE is production-derived and eligible
EvidenceCheckpoint = PASS
RestoreOnly = exactly once
later lifecycle/restoration does not replace SUCCESS
```

## Durable evidence

Use a durable evidence root outside the disposable runner and sandbox. Retain sanitized:

- runner source;
- runner SHA-256;
- parser evidence;
- structural-validation ledger;
- contract-check results.

Do not retain secrets or temporary evidence tokens.

After durable evidence is complete, remove the disposable runner and any implementation-validation sandbox. The retained source copy/hash becomes the implementation artifact to be reconciled by Luna.

## Repository invariants

Before and after implementation validation, record repository state.

Expected predecessor state includes exactly the two pre-existing governed WP04 modified tracked scripts and zero staged paths.

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

Do not clean, stage, revert, or otherwise alter the two pre-existing governed modifications.

Run `git diff --check` read-only and record the result. CRLF advisory warnings are not source mutations.

## Required validation

Without invoking governed W5:

1. prove Windows PowerShell version exactly `5.1.26100.9444`;
2. parse the complete final runner with Windows PowerShell 5.1 and require zero parser errors;
3. structurally inspect/validate all required gates and ordering;
4. prove RunId allocation is after every required pre-gate;
5. prove wrapper invocation is after RunId allocation;
6. prove exact-byte pre/post checks exist;
7. prove shim allowlist/fail-closed behavior;
8. prove P01–P20 are individually represented;
9. prove P19 ordering;
10. prove P20 does not clone production policy;
11. prove durable evidence is outside disposable paths;
12. prove secret hygiene;
13. prove repository invariants;
14. remove disposable implementation/validation paths after durable retention;
15. prove no governed W5 RunId was allocated and no wrapper was invoked.

Do not fake runtime W5 evidence to satisfy structural checks.

## Acceptance markers

Only if all implementation and structural validation gates pass, emit:

```text
RELEASE 1.12 WP04 — R1 DISPOSABLE W5 RUNNER IMPLEMENTATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 RUNNER PARSER: PASS
RELEASE 1.12 WP04 — R1 RUNNER EXACT-BYTE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRE-RUNID FAIL-CLOSED BOUNDARY: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHIM SURFACE: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRODUCTION-POLICY DUPLICATION: ABSENT
RELEASE 1.12 WP04 — R1 RUNNER P01-P20 INDIVIDUAL ACCOUNTING: PASS
RELEASE 1.12 WP04 — R1 RUNNER P19 POST-CLEANUP ORDER: PASS
RELEASE 1.12 WP04 — R1 RUNNER P20 EVIDENCE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER DURABLE SANITIZED EVIDENCE: PASS
RELEASE 1.12 WP04 — R1 RUNNER SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — R1 RUNNER DISPOSABLE CLEANUP: PASS
RELEASE 1.12 WP04 — R1 RUNNER TRACKED REPOSITORY MUTATIONS: 0
RELEASE 1.12 WP04 — R1 RUNNER STAGED PATHS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TERRA R1 RUNNER IMPLEMENTATION AND STRUCTURAL VALIDATION COMPLETE
```

Report:

- disposable runner original location;
- durable retained runner-source location;
- runner SHA-256;
- structural-validation ledger location;
- parser evidence;
- every structural contract check;
- pre/post repository state;
- exact mutation accounting.

## Failure boundary

If implementation or structural validation fails:

- retain sanitized diagnostic evidence;
- do not execute W5;
- do not allocate a W5 RunId;
- do not remediate production code;
- stop at the first governed implementation/contract failure for Luna reconciliation.

## Final stop

**STOP AFTER R1 RUNNER IMPLEMENTATION AND STRUCTURAL VALIDATION.**

Even on PASS:

```text
W5 = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
WP05 = NOT_STARTED
Milestone #63 = OPEN
PUBLICATION BLOCKER = UNRESOLVED
```

A separate GPT-5.6 Luna reconciliation must accept the implemented runner before a later GPT-5.6 Terra authority may execute governed W5.

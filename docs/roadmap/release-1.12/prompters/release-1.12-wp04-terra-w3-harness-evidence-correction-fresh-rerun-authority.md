# GPT-5.6 Terra — Release 1.12 WP04 W3 Harness Evidence Correction & Fresh Rerun Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map
- **GPT-5.6 Luna** — contract/reconciliation authority; selected H2.
- **GPT-5.6 Terra** — PRIMARY: correct disposable W3 evidence accounting and execute one fresh W3 rerun.
- **GPT-5.6 Sol** — supporting analysis only.

## Binding Luna decision

```text
H2 = MISSING GOVERNED EVIDENCE
PRODUCTION SOURCE REMEDIATION = NOT AUTHORIZED
FRESH W3 RERUN = AUTHORIZED
```

Preserve:

```text
W1 = PASS — carry forward
W2 = PASS — carry forward
W3 = acceptance NOT_GRANTED
W4 = PASS — carry forward
W5/W6/W7/W8 = NOT_RUN
PUBLICATION BLOCKER = UNRESOLVED
```

Previous W3 proved expected production behavior but the harness aggregate result did not expose its failing sub-predicate.

## Authorized change

Modify **disposable validation harness/evidence accounting only** so every authority-required W3 predicate is emitted individually before aggregate evaluation.

Do not modify:
- production wrapper;
- helper;
- tracked repository source;
- S2;
- W4 policy;
- RB2;
- Deferred/RestoreOnly;
- lifecycle precedence;
- poll-observation `.ToArray()` remediation.

The harness must report each predicate's observed value and PASS/FAIL, then compute the aggregate solely from those visible predicates.

## Fresh W3 execution

Execute W3 only using:
- Windows PowerShell 5.1.26100.9444;
- fresh sandbox/scenario identity;
- fresh synthetic RunId;
- current production wrapper copied byte-for-byte;
- pre/post SHA256 equality;
- fail-closed interception of git, az, helper, and all external operations;
- genuine invalid-entry archive fixture reaching production `Expand-Archive`;
- no internal-state manufacture.

Required production-derived state:

```text
ArchiveRetrieval = RETAINED
FreshExtraction = FAIL
EvidenceCheckpoint = FAIL
FinalLifecycleResult = EVIDENCE_PRESERVATION_FAILED
RestoreOnly = 1
RealExternalInvocations = 0
```

## Individually visible accounting predicates

At minimum emit and evaluate separately:

```text
P01 exact-byte pre-execution identity
P02 exact-byte post-execution identity
P03 ArchiveRetrieval == RETAINED
P04 FreshExtraction == FAIL
P05 EvidenceCheckpoint == FAIL
P06 FinalLifecycleResult == EVIDENCE_PRESERVATION_FAILED
P07 RestoreOnly count == 1
P08 unexpected real external invocations == 0
P09 repository modified-path scope unchanged
P10 staged path count == 0
P11 git diff --check == PASS
P12 secret hygiene == PASS
P13 durable sanitized evidence retained == PASS
P14 disposable sandbox cleanup == PASS
```

If the established Hybrid Revised-V2 W3 contract contains additional predicates, expose those individually too. Do not weaken or omit them.

Only after all required predicates are individually PASS may the harness emit aggregate W3 PASS.

## Stop boundary

If any individual predicate fails:
- STOP;
- report the exact predicate and observed value;
- preserve sanitized evidence;
- do not run W5;
- do not remediate production source.

If all W3 predicates pass:
- grant W3 governed acceptance;
- stop this authority after W3;
- do **not** run W5-W8 yet;
- report that the next authority may resume at W5.

## Mutation boundary

```text
tracked source mutations attributable to this authority = 0
staging = 0
commit/push = 0
Azure = 0
Docker/GHCR = 0
Git/GitHub lifecycle = 0
```

Disposable harness files are not production/tracked-source mutations.

## Required success markers

`RELEASE 1.12 WP04 — W3 FRESH RERUN: PASS`

`RELEASE 1.12 WP04 — W3 EXACT-BYTE PRE HASH: PASS`

`RELEASE 1.12 WP04 — W3 EXACT-BYTE POST HASH: PASS`

`RELEASE 1.12 WP04 — W3 ARCHIVE STATE: RETAINED`

`RELEASE 1.12 WP04 — W3 EXTRACTION STATE: FAIL`

`RELEASE 1.12 WP04 — W3 CHECKPOINT STATE: FAIL`

`RELEASE 1.12 WP04 — W3 FINAL LIFECYCLE RESULT: EVIDENCE_PRESERVATION_FAILED`

`RELEASE 1.12 WP04 — W3 RESTORE-ONLY COUNT: 1`

`RELEASE 1.12 WP04 — W3 UNEXPECTED REAL EXTERNAL INVOCATIONS: 0`

`RELEASE 1.12 WP04 — W3 REPOSITORY INVARIANTS: PASS`

`RELEASE 1.12 WP04 — W3 SECRET HYGIENE: PASS`

`RELEASE 1.12 WP04 — W3 DURABLE EVIDENCE RETENTION: PASS`

`RELEASE 1.12 WP04 — W3 SANDBOX CLEANUP: PASS`

`RELEASE 1.12 WP04 — W3 INDIVIDUAL ACCOUNTING PREDICATES: ALL_PASS`

`RELEASE 1.12 WP04 — W3 GOVERNED ACCEPTANCE: GRANTED`

`RELEASE 1.12 WP04 — W1 CARRY-FORWARD: AUTHORIZED`

`RELEASE 1.12 WP04 — W2 CARRY-FORWARD: AUTHORIZED`

`RELEASE 1.12 WP04 — W4 CARRY-FORWARD: AUTHORIZED`

`RELEASE 1.12 WP04 — W5/W6/W7/W8: NOT_RUN`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED`

`RELEASE 1.12 WP04 — TRACKED SOURCE MUTATIONS: 0`

`RELEASE 1.12 WP04 — EXTERNAL MUTATIONS: 0`

`RELEASE 1.12 WP04 — NEXT HYBRID RESTART POINT: W5`

`RELEASE 1.12 WP04 — TERRA W3 HARNESS EVIDENCE CORRECTION AND FRESH RERUN COMPLETE`

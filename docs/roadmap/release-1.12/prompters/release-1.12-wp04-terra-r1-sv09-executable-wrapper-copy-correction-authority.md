# Release 1.12 WP04 — Terra R1 SV09 Executable Wrapper-Copy Correction Authority

## Authority identity

**Selected execution model: GPT-5.6 Terra**

GPT-5.6 Terra owns this narrow disposable/local runner correction and structural validation execution.

GPT-5.6 Luna owns subsequent reconciliation, contract acceptance, and authorization of governed W5 execution.

GPT-5.6 Sol is supporting analysis only and must not replace Terra or Luna.

## Binding predecessor

The fresh durable SV01–SV36 rerun failed closed at the first real runner defect:

```text
RELEASE 1.12 WP04 — R1 FRESH SV01-SV36 DURABLE-LEDGER RERUN: FAIL
First failed gate: SV09 — executable byte-for-byte wrapper copy exists
Classification: RUNNER_DEFECT
Production defect: NO
Fresh runner hash required: YES
Governed W5 wrapper invoked: NO
Governed W5 RunId allocated: NO
W5 governed acceptance: NOT_GRANTED
W6/W7/W8: NOT_RUN
Publication blocker: UNRESOLVED
```

Failed runner:

```text
SHA-256:
C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
```

Retained failure evidence:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\r1-runner\014e51fb8a30407b9e9ed325e2a53389\structural-observation-ledger.json
```

Preserve that root and all earlier historical roots unchanged.

## Proven defect

SV09 cannot be satisfied because the retained runner does not contain an executable source-wrapper → `$env:W` byte-for-byte copy operation.

Generic topology text is not acceptable evidence.

The runner must be corrected.

Because runner source changes, a **new runner hash is mandatory**, and the complete single-artifact structural contract must be revalidated against that new hash.

This is not a production defect.

# Mission

Make the narrowest runner-source correction necessary to add the missing executable byte-for-byte wrapper copy.

Then create a fresh durable candidate root and revalidate the **entire SV01–SV36 single-artifact contract** against the new runner hash.

Do not patch only the SV09 ledger.

Do not fabricate a source reference.

Do not execute governed W5.

Do not allocate a governed W5 RunId.

# Required SV09 runner behavior

The corrected runner must contain executable Windows PowerShell 5.1-compatible logic that:

1. resolves the governed production wrapper source path;
2. validates the source path before copying;
3. copies the wrapper bytes to the exact path represented by `$env:W`;
4. performs no text transformation, newline normalization, templating, patching, or preprocessing;
5. fails closed if the copy fails;
6. computes source and copied-wrapper hashes as required by the existing exact-byte contract;
7. requires source hash == copied-wrapper pre-execution hash before RunId allocation;
8. retains evidence supporting P03/SV08–SV11.

## Preferred copy semantics

Use a binary/file copy operation with byte-preserving semantics supported by Windows PowerShell 5.1, such as an appropriate .NET file-copy API or `Copy-Item` file copy.

Do not implement the copy by reading/writing text.

Do not use:

```text
Get-Content | Set-Content
Out-File
string replacement
regex replacement
encoding conversion
line-ending normalization
```

for wrapper copying.

# `$env:W` binding

The executable copy destination must be the same exact path later exposed to the child as `W`.

The runner must prove structurally:

```text
copy destination == $env:W
hash target == $env:W
child-visible W == $env:W
wrapper invocation target == the governed copied wrapper represented by W
```

No second ungoverned wrapper-copy path is permitted.

# Pre-RunId ordering

The corrected runner must preserve and re-prove:

```text
G01 Windows PowerShell == 5.1.26100.9444
G02 parser errors == 0
G03 S defined/nonempty
G04 S absolute
G05 S fresh/exists
G06 W defined/nonempty
G07 W absolute
G08 W destination belongs to expected fresh scenario
G09 source wrapper validated
G10 byte-for-byte source → W copy completed
G11 source SHA-256 == W pre-execution SHA-256
------------------------------------------------
G12 allocate fresh governed W5 RunId
G13 invoke exact copied production wrapper
```

The exact gate numbering may be represented differently internally only if the retained structural evidence provides an explicit lossless mapping back to G01–G13.

Any failure before G12 must prevent both RunId allocation and wrapper invocation.

# Exact-byte post-run contract

Preserve the existing requirement that a future governed run computes and compares:

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

Mismatch fails closed.

# P03/P04 binding

Preserve:

```text
P03 <- actual source/W pre-execution exact-byte evidence
P04 <- actual source/W pre/post exact-byte evidence
```

No synthetic or topology-only evidence.

# Fresh runner hash

Because source changes are required:

```text
OLD HASH C419AD...5FA8 = HISTORICAL FAILED ARTIFACT
NEW HASH = REQUIRED
```

Retain:

```text
corrected runner source
new SHA-256
source references for copy/hash/order controls
```

in a new durable candidate root.

No structural PASS may be carried across the old/new hash boundary.

# Complete SV01–SV36 revalidation

After the narrow source correction, re-run all 36 structural checks against the new runner artifact.

Produce exactly 36 independently inspectable records:

```text
SV01 ... SV36
```

Each must contain:

```text
CheckId
Requirement
Expected
Observed
Result
EvidenceReference
ValidationMethod
```

All EvidenceReference values must resolve within retained durable evidence/source for the new candidate.

## SV09 specific acceptance

SV09 must retain, at minimum:

```text
source wrapper path-resolution source reference
source validation source reference
byte-preserving copy statement source reference
copy destination expression
proof destination binds to W
copy-failure branch/source reference
validation method proving executable rather than comment-only behavior
```

The `Observed` field must expose these concrete observations.

Generic text such as:

```text
"wrapper copied into sandbox"
"copy topology present"
"exact-byte contract"
```

is insufficient.

# SV08–SV14 consistency

The new SV09 copy operation must reconcile coherently with:

```text
SV08 source SHA-256
SV10 W pre SHA-256
SV11 pre-RunId equality assertion
SV12 source/W post hashes
SV13 post equality assertions
SV14 hash mismatch fail closed
```

Reject a runner where these controls refer to different paths/artifacts.

# Other structural contracts remain binding

Revalidate, do not assume:

- SV01/SV02 PowerShell/parser;
- SV03–SV07 P01–P20 accounting;
- SV15–SV17 pre-RunId ordering;
- SV18–SV22 S/W validation and child visibility;
- SV23/SV24 P03/P04 binding;
- SV25 P19 post-cleanup order;
- SV26 P20 observation-only contract;
- SV27/SV28 minimum shim/unexpected-call behavior;
- SV29 no production-policy duplication;
- SV30 secret hygiene;
- SV31–SV35 two-root lifecycle;
- SV36 repository invariants and scoped Git capture.

# Native Git capture

Use the already-proven operation-scoped Windows PowerShell 5.1 behavior:

```text
capture git diff --check output
capture $LASTEXITCODE immediately
known CRLF advisories do not terminate evidence generation
restore error handling immediately
retain actual output/exit code
nonzero exit code fails closed
```

The fresh candidate must retain its own SV36 observation.

# Two-root lifecycle

Create:

```text
fresh disposable root
fresh durable candidate root
```

They must be independent.

Then:

1. retain corrected runner and new hash durably;
2. generate all SV01–SV36 evidence;
3. persist complete ledger durably;
4. delete disposable root;
5. verify disposable absent;
6. verify durable root present;
7. verify retained runner present;
8. verify retained ledger present/parseable;
9. re-hash retained runner;
10. finalize post-cleanup evidence without recreating disposable root.

# Repository boundary

Expected repository state remains:

```text
two pre-existing governed WP04 modified tracked scripts
staged paths = 0
```

The runner correction is disposable/local evidence work only.

This authority authorizes:

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

Do not edit/stage/revert/clean the pre-existing governed scripts.

# Acceptance threshold

PASS requires all of:

```text
new runner hash != C419AD777CCB1B50E6D68780F849401CBDE1442C5E6409D52F0E19C584A75FA8
SV01-SV36 = exactly 36 unique records
all required fields complete
all observations independently inspectable
all evidence references resolve to new candidate
all validation methods explicit
SV01-SV36 all PASS
SV09 contains executable source→W byte-preserving copy evidence
SV08-SV14 path/artifact consistency PASS
G01-G13 ordering PASS
P01-P20 structural accounting PASS
runtime P01-P20 PASS claims = 0
two-root lifecycle PASS
fresh git diff --check PASS
governed W5 wrapper invoked = NO
governed W5 RunId allocated = NO
repository/external mutation boundary PASS
```

# Required success markers

Only on full success emit:

```text
RELEASE 1.12 WP04 — R1 SV09 EXECUTABLE WRAPPER-COPY CORRECTION: PASS
RELEASE 1.12 WP04 — R1 NEW RUNNER HASH: PROVEN
RELEASE 1.12 WP04 — R1 SV09 BYTE-PRESERVING SOURCE-TO-W COPY: PASS
RELEASE 1.12 WP04 — R1 SV09 COPY DESTINATION BINDS TO W: PASS
RELEASE 1.12 WP04 — R1 SV09 COPY FAILURE FAIL-CLOSED: PASS
RELEASE 1.12 WP04 — R1 SV08-SV14 EXACT-BYTE CONTROL COHERENCE: PASS
RELEASE 1.12 WP04 — R1 FRESH SV01-SV36 DURABLE-LEDGER RERUN: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 REQUIRED FIELDS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 INDEPENDENT OBSERVATIONS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 EVIDENCE REFERENCES: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 VALIDATION METHODS: PASS
RELEASE 1.12 WP04 — R1 SV01-SV36 ALL_PASS: PASS
RELEASE 1.12 WP04 — R1 P01-P20 STRUCTURAL CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNTIME P01-P20 PASS CLAIMS: 0
RELEASE 1.12 WP04 — R1 PRE-RUNID FAIL-CLOSED BOUNDARY: PASS
RELEASE 1.12 WP04 — R1 CHILD S/W VISIBILITY: PASS
RELEASE 1.12 WP04 — R1 P19 POST-CLEANUP ORDER: PASS
RELEASE 1.12 WP04 — R1 P20 EVIDENCE CONTRACT: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHIM SURFACE: PASS
RELEASE 1.12 WP04 — R1 RUNNER PRODUCTION-POLICY DUPLICATION: ABSENT
RELEASE 1.12 WP04 — R1 TWO-ROOT LIFECYCLE: PASS
RELEASE 1.12 WP04 — R1 DURABLE EVIDENCE SURVIVES CLEANUP: PASS
RELEASE 1.12 WP04 — R1 RETAINED RUNNER HASH REVERIFICATION: PASS
RELEASE 1.12 WP04 — R1 GIT DIFF CHECK NATIVE CAPTURE: PASS
RELEASE 1.12 WP04 — R1 WINDOWS POWERSHELL 5.1: PASS
RELEASE 1.12 WP04 — R1 RUNNER PARSER: PASS
RELEASE 1.12 WP04 — R1 SECRET HYGIENE: PASS
RELEASE 1.12 WP04 — R1 TRACKED REPOSITORY MUTATIONS: 0
RELEASE 1.12 WP04 — R1 STAGED PATHS: 0
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — TERRA R1 SV09 RUNNER CORRECTION COMPLETE
```

# Required report

Report:

```text
historical failed root preserved
fresh disposable root
fresh durable candidate root
old runner SHA-256
new runner SHA-256
exact runner-source change
SV09 source references
SV08-SV14 coherence evidence
PowerShell version
parser error count
SV ledger path
SV record count
SV failed count
runtime predicate PASS claim count
git diff --check exit code/output/classification
post-cleanup disposable/durable/runner/ledger existence
post-cleanup runner hash
repository pre/post state
exact mutation accounting
```

# Failure boundary

At the first failure:

- retain sanitized durable failure evidence;
- identify the exact first failed SV/gate;
- do not continue manufacturing later PASS claims;
- do not execute W5;
- do not allocate a W5 RunId;
- do not mutate repository/production/external systems;
- STOP for Luna reconciliation.

# Final stop

**STOP AFTER THE NEW-HASH COMPLETE SV01–SV36 REVALIDATION.**

Even on PASS:

```text
R1 Luna acceptance = PENDING
Governed W5 execution = NOT_AUTHORIZED until Luna reconciliation
W5 acceptance = NOT_GRANTED
W6/W7/W8 = NOT_RUN
WP04 #263 = OPEN
Milestone #63 = OPEN
WP05 = NOT_STARTED
Publication blocker = UNRESOLVED
```

After a full Terra PASS, execute the existing GPT-5.6 Luna R1 Fresh SV01–SV36 Ledger Reconciliation Authority against the new retained runner/hash and fresh durable ledger.

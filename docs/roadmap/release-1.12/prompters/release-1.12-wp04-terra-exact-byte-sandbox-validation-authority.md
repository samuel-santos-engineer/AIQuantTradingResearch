# GPT-5.6 Terra — Release 1.12 WP04 Exact-Byte Sandbox Validation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Terra**

## Model authority map

- **GPT-5.6 Luna** — owns validation contract, acceptance criteria, publication readiness, and all later governance.
- **GPT-5.6 Terra** — PRIMARY: execute local-only V2 exact-byte sandbox validation of the actual production wrapper.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed baseline

Release:

```text
Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery
```

Issue:

```text
#263
```

Repository:

```text
C:\projects\github\AIQuantTradingResearch
```

PowerShell target:

```text
Windows PowerShell 5.1.26100.9444
```

Selected validation model:

```text
V2 — exact-byte sandbox execution
```

Current publication readiness:

```text
BLOCKED pending actual production control-flow validation
```

Current deployed image remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Retained diagnostic evidence must remain preserved:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

## 2. Binding re-governance result

The remaining blocker does **not** prove a production-code defect.

Binding:

```text
FUNCTIONAL SOURCE DEFECT PROVEN = NO
STRUCTURAL REFACTOR REQUIRED BY DEFAULT = NO
TRACKED SOURCE CHANGES REQUIRED = NO
SINGLE ORCHESTRATION FUNCTION AS PUBLICATION GATE = REMOVED
ACTUAL PRODUCTION CONTROL-FLOW VALIDATION = REQUIRED
```

## 3. Validation target

Validate the actual tracked wrapper:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

without modifying its bytes.

The sandbox copy must be byte-identical to the tracked wrapper before every scenario execution.

## 4. Zero tracked mutation boundary

This authority authorizes:

```text
TRACKED SOURCE MODIFICATIONS = 0
STAGED PATHS = 0
COMMITS = 0
PUSHES = 0
PRS = 0
MERGES = 0
```

Do not edit either governed tracked path:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

## 5. Sandbox creation

Create a temporary non-repository sandbox, for example beneath:

```text
$env:TEMP\AIQuantTradingResearch\wp04-v2\<validation-id>
```

The validation ID must be local-only and must not masquerade as a qualification RunId.

Copy the tracked wrapper into the sandbox using a byte-preserving method.

Also copy only any strictly necessary non-mutated local support files if required for wrapper startup; record every copied path.

Do not copy secrets.

## 6. Exact-byte identity proof

Before execution:

```text
TRACKED_WRAPPER_SHA256 = <hash>
SANDBOX_WRAPPER_SHA256 = <hash>
```

Required:

```text
TRACKED_WRAPPER_SHA256 == SANDBOX_WRAPPER_SHA256
```

If hashes differ:

```text
STOP
DO NOT EXECUTE
```

Recompute/verify identity before each scenario or prove the sandbox wrapper remained unchanged across the full suite with a final hash check.

## 7. Mock interception boundary

Every external operation that could mutate real state must be intercepted.

Required mock/shim surfaces include at minimum:

```text
git
az
qualification helper surface
```

Use Windows PowerShell 5.1-compatible temporary PATH-prepended shims/scripts.

If the wrapper calls additional external executables/commands, intercept them too or STOP.

No call may reach:

```text
real Azure
real Git remote mutation
real Docker/GHCR
real network side effect
real secret source
```

## 8. Mock command provenance

Record for each shim:

```text
shim path
command name intercepted
expected invocation patterns
unexpected invocation policy
```

Unexpected invocation must:

```text
fail closed
exit nonzero
record the attempted command safely
```

Do not silently pass through to the real command.

## 9. Synthetic helper contract

Provide a temporary mock helper surface that reproduces only the safe contract needed by the wrapper.

It may synthesize:

```text
Deferred terminal telemetry
actual synthetic RunId
poll observations
D3 payload/state
RestoreOnly success/failure
```

It must record invocation parameters sufficient to prove:

```text
RestorationMode=Deferred
LifecycleAction=None
RB2 actual RunId handling
RestoreOnly invocation count
```

No real evidence token or secret.

## 10. Synthetic archive/filesystem fixtures

Use only temporary sandbox fixtures.

Cover:

```text
archive retrieval success
archive retrieval terminal failure
fresh extraction PASS
fresh extraction EMPTY
fresh extraction FAIL
invalid NOT_APPLICABLE after retained archive
checkpoint persistence success/failure
```

All fixture content must be synthetic and secret-free.

## 11. Required W1–W8 scenarios

Execute the unmodified sandbox wrapper under Windows PowerShell 5.1 for all scenarios.

### W1 — complete success

Expected:

```text
archive = RETAINED
extraction = PASS
checkpoint = PASS
final lifecycle = qualification success
RestoreOnly count = 1
```

### W2 — retained + EMPTY

Expected:

```text
archive = RETAINED
extraction = EMPTY
checkpoint eligible/PASS
RestoreOnly count = 1
```

### W3 — retained + FAIL

Expected:

```text
checkpoint = FAIL
final lifecycle = EVIDENCE_PRESERVATION_FAILED
RestoreOnly count = 1
```

### W4 — retained + NOT_APPLICABLE

Expected:

```text
checkpoint = FAIL
inconsistent evidence state retained
RestoreOnly count = 1
```

### W5 — retrieval failed + NOT_APPLICABLE

Expected:

```text
retrieval failure durably represented
checkpoint may remain reconcilable/PASS
RestoreOnly count = 1
```

### W6 — qualification/poll failure

Expected:

```text
qualification failure retained
evidence preservation path still executes as governed
RestoreOnly count = 1
final precedence correct
```

### W7 — checkpoint persistence failure

Expected:

```text
evidence preservation failure
RestoreOnly count = 1
final lifecycle = EVIDENCE_PRESERVATION_FAILED unless restoration also fails
```

### W8 — restoration failure

Expected:

```text
RestoreOnly count = 1
final lifecycle = RESTORATION_FAILED
original qualification/evidence result retained
```

## 12. Scenario ledger

For each W1–W8 retain a durable local record containing:

```text
scenario id
tracked wrapper SHA256
sandbox wrapper SHA256
PowerShell version
mock PATH
helper invocation ledger
az invocation ledger
git invocation ledger
synthetic actual RunId
archive state
extraction state
checkpoint result
deepest boundary
final lifecycle result
exit code
RestoreOnly invocation count
unexpected external invocation count
```

Expected:

```text
unexpected external invocation count = 0
```

## 13. PowerShell 5.1 gate

Run using:

```text
Windows PowerShell 5.1.26100.9444
```

Required:

```text
parser errors = 0
wrapper execution = PASS for all expected scenarios
mock parameter binding = PASS
W1-W8 = PASS
```

Do not use PowerShell 7-only syntax/APIs.

## 14. External-side-effect proof

At completion prove:

```text
AZURE MUTATIONS = 0
DOCKER/GHCR MUTATIONS = 0
GIT/GITHUB LIFECYCLE MUTATIONS = 0
REAL NETWORK MUTATIONS = 0
TRACKED SOURCE MUTATIONS = 0
STAGED PATHS = 0
```

If any mock misses an external operation:

```text
STOP
VALIDATION = FAIL
```

## 15. Actual-production-control-flow proof

This validation is acceptable only if:

```text
sandbox wrapper bytes equal tracked wrapper bytes
sandbox wrapper executed unmodified
all external effects were intercepted
W1-W8 exercised the wrapper's own lifecycle logic
no separate mock lifecycle implementation determined checkpoint/precedence outcomes
```

Mocks may return external results but must not decide lifecycle policy.

## 16. Secret hygiene

Synthetic values only.

Scan sandbox outputs for accidental secret literals/patterns.

Expected:

```text
real secret findings = 0
```

Do not access or emit:

```text
TwelveData__ApiKey
real HttpEvidenceToken
Authorization headers
registry credentials
connection strings
cookies
```

## 17. Sandbox cleanup

After evidence is durably retained outside the ephemeral sandbox as needed for Luna reconciliation:

```text
remove only the temporary V2 sandbox
```

Do not delete:

```text
retained historical diagnostic evidence
repository files
user files
```

If evidence files are retained, record their exact non-repository paths.

## 18. Working-tree audit

Before and after validation record:

```text
git status --short
```

Expected after:

```text
same governed two modified tracked paths as before
0 new tracked modifications
0 staged paths
```

Unrelated untracked user content must remain untouched.

## 19. Publication boundary

Even if V2 passes:

```text
DO NOT stage
DO NOT commit
DO NOT push
DO NOT create PR
DO NOT merge
```

Publication still requires Luna reconciliation.

## 20. Acceptance boundary

Preserve:

```text
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
NEW IMAGE REQUIRED = NO
```

## 21. Success condition

Authority succeeds only if all are proven:

```text
exact-byte wrapper identity PASS
all external operations intercepted
W1-W8 PASS on actual wrapper path
RestoreOnly exactly once in every applicable scenario
error precedence correct
archive/extraction truth table correct
PowerShell 5.1 gate PASS
0 real external mutations
0 tracked source mutations
```

## 22. Required output

Return:

- sandbox path;
- tracked wrapper hash;
- sandbox wrapper hash;
- identity result;
- complete mock/shim inventory;
- proof all external operations were intercepted;
- W1–W8 scenario ledger;
- RestoreOnly counts;
- precedence results;
- PowerShell 5.1 version/parse/execution results;
- secret scan;
- before/after working-tree audit;
- sandbox cleanup result;
- whether the remaining publication blocker is resolved;
- readiness for Luna final pre-publication reconciliation.

## 23. Terminal markers

`RELEASE 1.12 WP04 — EXACT-BYTE SANDBOX VALIDATION: PASS`

`RELEASE 1.12 WP04 — SELECTED VALIDATION MODEL: V2`

`RELEASE 1.12 WP04 — TRACKED SOURCE CHANGES REQUIRED: NO`

`RELEASE 1.12 WP04 — TRACKED WRAPPER SHA256: <value>`

`RELEASE 1.12 WP04 — SANDBOX WRAPPER SHA256: <value>`

`RELEASE 1.12 WP04 — EXACT-BYTE WRAPPER IDENTITY: PASS`

`RELEASE 1.12 WP04 — EXTERNAL OPERATION INTERCEPTION: PASS`

`RELEASE 1.12 WP04 — UNEXPECTED REAL EXTERNAL INVOCATIONS: 0`

`RELEASE 1.12 WP04 — ACTUAL PRODUCTION CONTROL-FLOW VALIDATION: PASS`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1 FULL WRAPPER LIFECYCLE W1-W8: PASS`

`RELEASE 1.12 WP04 — W1 RESULT: PASS`

`RELEASE 1.12 WP04 — W2 RESULT: PASS`

`RELEASE 1.12 WP04 — W3 RESULT: PASS`

`RELEASE 1.12 WP04 — W4 RESULT: PASS`

`RELEASE 1.12 WP04 — W5 RESULT: PASS`

`RELEASE 1.12 WP04 — W6 RESULT: PASS`

`RELEASE 1.12 WP04 — W7 RESULT: PASS`

`RELEASE 1.12 WP04 — W8 RESULT: PASS`

`RELEASE 1.12 WP04 — RESTORE-ONLY EXACTLY-ONCE VALIDATION: PASS`

`RELEASE 1.12 WP04 — ERROR PRECEDENCE: PROVEN`

`RELEASE 1.12 WP04 — PRE-RESTORATION EVIDENCE CHECKPOINT: PROVEN_COMPLETE`

`RELEASE 1.12 WP04 — ARCHIVE/EVIDENCE CONTRACT: PASS`

`RELEASE 1.12 WP04 — SECRET HYGIENE VALIDATION: PASS`

`RELEASE 1.12 WP04 — TRACKED SOURCE MUTATIONS: 0`

`RELEASE 1.12 WP04 — STAGED PATH COUNT: 0`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — SANDBOX CLEANUP: PASS`

`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: RESOLVED`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — EXACT-BYTE SANDBOX VALIDATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — LUNA FINAL PRE-PUBLICATION LIFECYCLE REMEDIATION RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — TERRA EXACT-BYTE SANDBOX VALIDATION COMPLETE`

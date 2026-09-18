# GPT-5.6 Luna — Release 1.12 WP04 Structural Refactor Re-Governance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: re-govern the wrapper refactor boundary, decompose the work safely, define exact acceptance criteria, and determine whether a broader tracked scope is justified.
- **GPT-5.6 Terra** — may implement only a later explicitly authorized refactor after Luna fixes the architecture and mutation boundary.
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

Current deployed runtime image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Retained diagnostic evidence:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

Retain it unchanged.

## 2. Current implementation state

The governed working-tree remediation currently spans exactly:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Already proven:

```text
H1 helper contract = PASS
Immediate compatibility = PASS
Deferred mode = PASS
RestoreOnly = PASS
LifecycleAction=None = PASS
RB2 = PASS
explicit restart remediation = PASS
null/absent restoration semantics = PASS
archive/extraction truth table = PASS
pre-restoration evidence checkpoint = PROVEN_COMPLETE
checkpoint/restoration order = PASS
error precedence = PASS
secret hygiene = PASS
git diff --check = PASS
```

Still blocked:

```text
PRODUCTION WRAPPER CONTROL FLOW MOCKED = NOT_PROVEN
WINDOWS POWERSHELL 5.1 FULL WRAPPER LIFECYCLE W1-W8 = NOT_PROVEN
PUBLICATION READINESS = BLOCKED
```

## 3. Reason prior Terra authority stopped

Terra correctly stopped without mutation because the remaining change is structural, not a safe incremental seam addition.

The required refactor must move the **entire** current top-level lifecycle policy into one callable orchestration function, including:

```text
Deferred helper invocation
RunId binding
transcript handling
poll-observation retention
D3 state handling
archive retrieval
archive metadata
fresh-window extraction
deepest-boundary derivation
checkpoint evaluation
checkpoint persistence
RestoreOnly in finally
final lifecycle precedence
```

and then replace only external operations with optional callbacks while preserving production defaults.

Partial extraction is forbidden because it would leave duplicate lifecycle policy.

## 4. Re-governance objective

Determine the safest refactor shape before any additional implementation.

Luna must answer:

```text
What is the single orchestration boundary?
Which logic remains inside that function?
Which operations become injectable callbacks?
Which functions/helpers may be introduced?
Which tracked paths may change?
How is production behavior proven unchanged?
How is the refactor validated in PowerShell 5.1?
Should implementation occur in one authority or staged authorities?
```

No implementation is authorized by this authority.

## 5. Read-only source review

Inspect the current full contents/diff of:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Also inspect any existing local validation harness code already embedded in or invoked by the wrapper.

Do not edit.

Return a structural map with exact function/region boundaries.

## 6. Required orchestration boundary

Luna must define one authoritative function, conceptually:

```text
Invoke-Wp04QualificationLifecycle
```

Name may differ.

The function must own lifecycle policy and sequencing.

It must not delegate policy decisions to callbacks.

It must receive enough state/parameters to execute both:

```text
production mode
injected local validation mode
```

## 7. Logic that must remain inside orchestration

At minimum:

```text
try/finally structure
RB2 actual RunId adoption
checkpoint truth table
archive/extraction consistency rules
error classification
error precedence
RestoreOnly exactly-once rule
final lifecycle result
terminal marker construction
```

These must not be duplicated elsewhere.

## 8. External-effect seams

Luna must decide the exact callback set.

Expected minimum:

```text
InvokeDeferredQualification
RetrieveRawArchive
ExtractFreshWindow
DeriveDeepestBoundary
PersistEvidenceCheckpoint
InvokeRestoreOnly
```

Potential optional seams only if necessary:

```text
GetUtcNow
Sleep
ComputeSha256
WriteDurableFile
```

Luna must reject unnecessary seams that make production behavior harder to reason about.

## 9. Production-default adapter design

For every callback seam, Luna must define the default production adapter.

Required property:

```text
callbacks omitted => current real behavior
```

No environment-variable test switch.

No global mutable test hook.

No PowerShell 7-only implementation.

## 10. Wrapper top-level responsibility after refactor

Luna must reduce top-level responsibility to configuration/input setup plus one orchestration call.

Target conceptual shape:

```text
parse/validate parameters
construct production defaults
invoke one lifecycle orchestration function
emit final result / exit
```

Top-level must not retain a second copy of lifecycle control flow.

## 11. Validation strategy

Luna must define authoritative tests that invoke the exact orchestration function used by production.

The existing separate fixture lifecycle must be:

```text
removed
or
reduced to callback fixtures only
```

It must not retain lifecycle policy.

Required cases remain W1-W8:

```text
W1 complete success
W2 retained + EMPTY
W3 retained + FAIL
W4 retained + NOT_APPLICABLE
W5 retrieval failed + NOT_APPLICABLE
W6 qualification failure
W7 checkpoint persistence failure
W8 restoration failure
```

## 12. Equivalence proof

Luna must define how Terra proves production behavior did not change except for testability structure.

Required dimensions:

```text
same helper invocation parameters
same LifecycleAction=None
same Deferred behavior
same RB2 binding
same evidence root semantics
same archive retrieval semantics
same extraction semantics
same checkpoint truth table
same RestoreOnly behavior
same precedence
same terminal result mapping
same exit behavior
```

## 13. Scope decision

Luna must choose exactly one scope model.

### S1 — one-path refactor

Only:

```text
initialize-qualification.ps1
```

may change.

Use only if all required orchestration extraction and authoritative validation can be implemented cleanly in that path.

### S2 — two-path refactor

Permit:

```text
initialize-qualification.ps1
```

plus exactly one dedicated local validation script under the existing WP04 deployment directory.

Use only if separating validation fixtures materially reduces risk and the validation script contains **no lifecycle policy**, only callback fixtures/assertions.

### S3 — broader scope

Only if source review proves S1/S2 structurally unsafe or impossible.

If selected, Luna must enumerate every additional tracked path and justify each one.

Do not authorize broad scope by default.

## 14. Implementation phasing decision

Luna must choose exactly one:

### P1 — single implementation authority

Use if the refactor can be safely performed and validated atomically.

### P2 — two-step implementation

Example:

```text
Step A: extract orchestration with production adapters; local equivalence only
Step B: replace old fixture lifecycle with injected W1-W8 validation
```

Use only if intermediate state can remain locally valid and publication remains blocked between steps.

### P3 — design-only follow-up required

Use if source structure is too complex to authorize implementation safely yet.

## 15. Mutation boundary

This Luna authority performs zero mutations:

```text
0 repository edits
0 staged paths
0 commit/push
0 PR/merge
0 Azure
0 Docker/GHCR
0 GitHub lifecycle
```

## 16. Acceptance boundary

Preserve:

```text
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
NEW IMAGE REQUIRED = NO
```

## 17. Required output

Return:

- current wrapper structural map;
- exact orchestration function boundary;
- exact lifecycle logic retained inside it;
- exact callback seam set;
- production-default adapter mapping;
- top-level post-refactor responsibility;
- treatment of existing mock lifecycle;
- W1-W8 authoritative validation design;
- production-equivalence proof plan;
- scope decision S1/S2/S3;
- implementation phasing P1/P2/P3;
- exact next Terra/Luna authority;
- zero-mutation audit.

## 18. Terminal markers

`RELEASE 1.12 WP04 — STRUCTURAL REFACTOR RE-GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — CURRENT PUBLICATION READINESS: BLOCKED`

`RELEASE 1.12 WP04 — STRUCTURAL REFACTOR REQUIRED: YES`

`RELEASE 1.12 WP04 — PARTIAL EXTRACTION: FORBIDDEN`

`RELEASE 1.12 WP04 — SINGLE LIFECYCLE POLICY OWNER: REQUIRED`

`RELEASE 1.12 WP04 — PRODUCTION DEFAULT BEHAVIOR PRESERVATION: REQUIRED`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1: REQUIRED`

`RELEASE 1.12 WP04 — SELECTED REFACTOR SCOPE: <S1|S2|S3>`

`RELEASE 1.12 WP04 — SELECTED IMPLEMENTATION PHASING: <P1|P2|P3>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — STRUCTURAL REFACTOR RE-GOVERNANCE MUTATION AUDIT: PASS`

Then exactly one matching next-authority marker, for example:

`RELEASE 1.12 WP04 — TERRA GOVERNED STRUCTURAL REFACTOR IMPLEMENTATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA STRUCTURAL REFACTOR STEP-A AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA STRUCTURAL REFACTOR DESIGN AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA STRUCTURAL REFACTOR RE-GOVERNANCE COMPLETE`

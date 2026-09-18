# GPT-5.6 Luna — Release 1.12 WP04 Blocker-Only Validation Re-Governance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: re-govern the remaining publication blocker so validation does not require a production-code refactor unless evidence proves one is necessary.
- **GPT-5.6 Terra** — may later execute a local validation harness or narrow correction explicitly authorized by Luna.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governing principle

Do **not** refactor production code merely to satisfy a test-structure preference when the currently proven functional contracts already pass.

The remaining blocker is validation evidence:

```text
PRODUCTION WRAPPER CONTROL FLOW MOCKED = NOT_PROVEN
WINDOWS POWERSHELL 5.1 FULL WRAPPER LIFECYCLE W1-W8 = NOT_PROVEN
```

No current evidence proves that the inline production lifecycle is itself functionally defective.

Therefore:

```text
STRUCTURAL REFACTOR = NOT REQUIRED BY DEFAULT
```

A structural refactor is justified only if the actual production wrapper cannot be exercised safely by an external/local harness.

## 2. Current proven contracts

Preserve all already-proven results:

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
RestoreOnly exactly-once validation = PASS
secret hygiene = PASS
git diff --check = PASS
full tracked diff scope = PASS
```

## 3. Re-governed objective

Resolve the remaining validation blocker with the smallest-risk method.

Preferred order:

### V1 — external black-box execution of the actual wrapper

Run the actual tracked:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

under Windows PowerShell 5.1 while intercepting external effects through process/session-level mocks or command shims.

The production file itself remains unchanged.

### V2 — exact-byte sandbox execution

If V1 cannot safely intercept all effects, copy the exact tracked wrapper bytes into an isolated temporary sandbox and prove:

```text
SHA256(sandbox wrapper) = SHA256(tracked wrapper)
```

Then provide mocked sibling commands/helper surfaces in the sandbox and execute the copied wrapper unmodified.

This validates the actual production control flow without changing tracked source.

### V3 — structural refactor

Only if V1 and V2 are demonstrably infeasible.

Do not select V3 merely for test convenience.

## 4. Zero-source-change preference

Luna must explicitly determine whether the blocker can be resolved with:

```text
0 tracked source changes
```

If yes:

```text
SOURCE REFACTOR REQUIRED = NO
```

## 5. Validation target

The required proof is behavioral, not architectural.

Validation must prove the production wrapper's actual lifecycle ordering and precedence for W1-W8:

```text
W1 success
W2 retained + EMPTY
W3 retained + FAIL
W4 retained + NOT_APPLICABLE
W5 retrieval failed + NOT_APPLICABLE
W6 qualification failure
W7 checkpoint persistence failure
W8 restoration failure
```

## 6. Acceptable mock/shim mechanisms

Windows PowerShell 5.1-compatible local-only techniques may include:

```text
session-scoped function shadowing
PATH-prepended command shims
temporary mock executables/scripts
temporary exact-byte sandbox copy
temporary mock helper sibling
filesystem fixtures
synthetic archives
synthetic HTTP/poll responses
```

No repo modification is required for the harness unless Luna separately proves otherwise.

## 7. Hard safety boundary

The harness must guarantee:

```text
Azure mutations = 0
Docker/GHCR mutations = 0
Git/GitHub mutations = 0
real network side effects = 0 where mockable
real secrets = 0
tracked repo mutations = 0
```

If any external operation cannot be intercepted safely, STOP rather than execute it.

## 8. Actual-wrapper proof

For V1:

```text
tracked wrapper path executed directly
```

For V2:

```text
tracked wrapper SHA256 recorded
sandbox wrapper SHA256 recorded
hashes identical
sandbox copy not edited
```

A separate reimplementation of lifecycle logic does not qualify.

## 9. Behavioral acceptance

For each W1-W8, record:

```text
scenario
wrapper identity/hash
external mock invocation ledger
actual/synthetic RunId
archive state
extraction state
checkpoint result
final lifecycle result
RestoreOnly invocation count
exit code
```

Required:

```text
RestoreOnly = exactly 1
```

in every applicable scenario.

## 10. Re-governed publication criterion

Publication readiness no longer requires extracting a shared orchestration function.

It requires proof that the actual production wrapper path satisfies W1-W8.

Therefore:

```text
SINGLE ORCHESTRATION FUNCTION = NOT A PUBLICATION REQUIREMENT
ACTUAL PRODUCTION CONTROL-FLOW VALIDATION = REQUIRED
```

## 11. Scope decision

Luna must select exactly one:

```text
V1 — DIRECT BLACK-BOX ACTUAL WRAPPER
V2 — EXACT-BYTE SANDBOX ACTUAL WRAPPER
V3 — STRUCTURAL REFACTOR REQUIRED
```

Prefer V1, then V2.

## 12. Mutation boundary

This Luna authority is read-only:

```text
repository edits = 0
staged paths = 0
commit/push = 0
PR/merge = 0
Azure = 0
Docker/GHCR = 0
GitHub lifecycle = 0
```

## 13. Existing tracked working tree

Do not alter the existing two governed modified tracked paths:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

## 14. Acceptance boundary

Preserve:

```text
PUBLICATION READINESS = BLOCKED pending validation
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
NEW IMAGE REQUIRED = NO
```

## 15. Required output

Return:

- whether a source refactor is actually required;
- V1/V2/V3 selection;
- exact interception strategy;
- proof no real external mutation can occur;
- exact W1-W8 harness plan;
- actual-wrapper identity proof method;
- PowerShell 5.1 validation plan;
- whether tracked source changes are required;
- exact next Terra authority;
- zero-mutation audit.

## 16. Terminal markers

`RELEASE 1.12 WP04 — BLOCKER-ONLY VALIDATION RE-GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — FUNCTIONAL SOURCE DEFECT PROVEN: NO`

`RELEASE 1.12 WP04 — STRUCTURAL REFACTOR REQUIRED BY DEFAULT: NO`

`RELEASE 1.12 WP04 — SELECTED VALIDATION MODEL: <V1|V2|V3>`

`RELEASE 1.12 WP04 — TRACKED SOURCE CHANGES REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — ACTUAL PRODUCTION CONTROL-FLOW VALIDATION: REQUIRED`

`RELEASE 1.12 WP04 — SINGLE ORCHESTRATION FUNCTION AS PUBLICATION GATE: REMOVED`

`RELEASE 1.12 WP04 — WINDOWS POWERSHELL 5.1: REQUIRED`

`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`

`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`

`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — BLOCKER-ONLY VALIDATION RE-GOVERNANCE MUTATION AUDIT: PASS`

Then exactly one appropriate next marker, such as:

`RELEASE 1.12 WP04 — TERRA ACTUAL-WRAPPER BLACK-BOX VALIDATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA EXACT-BYTE SANDBOX VALIDATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA GOVERNED STRUCTURAL REFACTOR IMPLEMENTATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA BLOCKER-ONLY VALIDATION RE-GOVERNANCE COMPLETE`

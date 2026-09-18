# GPT-5.6 Luna — Release 1.12 WP04 W4 Reachability Re-Governance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: reconcile whether W4 is a legitimate production-path validation case or only a defensive invariant that should be tested at the policy-function level.
- **GPT-5.6 Terra** — executes only the later validation/correction explicitly authorized by Luna.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

## 1. Binding current state

Revised V2 is blocked before sandbox execution.

No repository or external-state mutation occurred.

The current production archive path establishes:

```text
retrieval success + extraction success with records -> RETAINED + PASS
retrieval success + extraction success without records -> RETAINED + EMPTY
retrieval success + extraction exception -> RETAINED + FAIL
retrieval failure -> RETRIEVAL_FAILED + NOT_APPLICABLE
```

Therefore:

```text
RETAINED + NOT_APPLICABLE
```

is not naturally reachable through the production archive function.

The only current S2 seam is final checkpoint persistence. It must not be expanded under this read-only authority.

## 2. Re-governance question

Determine whether W4:

```text
RETAINED + NOT_APPLICABLE => checkpoint FAIL
```

must be induced end-to-end through the exact wrapper process, or whether it is a defensive impossible-state invariant already adequately validated by the wrapper's archive/extraction state-policy function.

Do not add a seam merely to manufacture a state the production archive function itself prevents.

## 3. Required source review

Read-only inspect the current wrapper and identify:

```text
Test-Wp04ArchiveExtractionState
Invoke-Wp04ArchiveCheckpoint
archive retrieval/extraction state assignment
existing local validation for RETAINED + NOT_APPLICABLE
```

Determine whether the inconsistent-state truth table is already tested directly against the actual production policy function rather than a duplicated fixture implementation.

## 4. Reachability classification

Classify W4 exactly one way:

### R1 — UNREACHABLE DEFENSIVE INVARIANT

Select if production control flow cannot produce `RETAINED + NOT_APPLICABLE`, but the actual production policy function explicitly rejects it.

Then:

```text
end-to-end W4 injection is not required
policy-level W4 validation is required
no new source seam is required
```

### R2 — REACHABLE PRODUCTION STATE

Select only if source review finds a real production path that can yield `RETAINED + NOT_APPLICABLE`.

Then revised V2 must exercise that real path without new arbitrary state injection.

### R3 — REQUIRED BUT UNTESTABLE DEFENSIVE STATE

Select only if W4 must remain an end-to-end publication gate and existing production policy cannot be invoked/tested directly.

Then Luna may govern a narrow state seam, but must justify why manufacturing an impossible production state improves assurance.

## 5. Preferred validation architecture

If R1 is selected, partition validation:

```text
W1,W2,W3,W5,W6,W7,W8 = exact-byte wrapper/process validation
W4 = direct validation of actual production archive/extraction state-policy function
```

This is not a waiver of W4. It validates W4 at the layer where the invariant exists.

Required W4 proof:

```text
actual production policy function invoked
input = RETAINED + NOT_APPLICABLE
result = FAIL
no duplicated truth-table implementation used
Windows PowerShell 5.1
```

## 6. Important distinction

Do not confuse:

```text
production-state reachability
```

with:

```text
defensive-state validation
```

An unreachable inconsistent state may still deserve a fail-closed assertion, but it does not automatically justify adding production injection seams solely for an end-to-end test.

## 7. Existing S2 seam

Preserve the reconciled checkpoint seam exactly:

```text
PersistEvidenceCheckpointCallback
scope = final checkpoint persistence only
production default equivalence = PASS
```

No changes authorized.

## 8. Publication criterion re-governance

Luna must decide whether the publication gate should become:

```text
reachable lifecycle cases -> exact-byte sandbox
defensive impossible-state truth table -> actual production policy-function validation
```

If yes, W4 no longer blocks V2 process execution.

## 9. Zero mutation boundary

This authority is read-only:

```text
tracked edits = 0
staging = 0
commit/push = 0
PR/merge = 0
Azure = 0
Docker/GHCR = 0
GitHub lifecycle = 0
sandbox execution = 0
```

## 10. Acceptance boundary

Preserve:

```text
PUBLICATION READINESS = BLOCKED
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
NEW IMAGE REQUIRED = NO
RETAINED LOCAL EVIDENCE = PRESERVE
```

## 11. Decision

Select exactly one:

```text
D1 — W4 IS UNREACHABLE DEFENSIVE INVARIANT; POLICY-LEVEL VALIDATION SUFFICIENT
D2 — W4 IS REACHABLE; REVISED V2 MUST EXERCISE REAL PATH
D3 — W4 REQUIRES A NEW NARROW INJECTION SEAM
```

Prefer D1 if supported by source.

## 12. Next authority

If D1:

```text
TERRA HYBRID REVISED-V2 VALIDATION AUTHORITY
```

It must run:

```text
W1,W2,W3,W5,W6,W7,W8 through exact-byte sandbox wrapper
W4 against actual production policy function under Windows PowerShell 5.1
```

If D2:

```text
TERRA REVISED EXACT-BYTE SANDBOX VALIDATION AUTHORITY
```

If D3:

```text
TERRA NARROW W4 STATE-INJECTION SEAM IMPLEMENTATION AUTHORITY
```

## 13. Required output

Return:
- exact W4 reachability proof;
- production function owning the inconsistent-state rule;
- whether existing W4 validation invokes that actual function;
- R1/R2/R3;
- D1/D2/D3;
- revised publication validation partition;
- whether any tracked change is required;
- exact next authority;
- zero-mutation audit.

## 14. Terminal markers

`RELEASE 1.12 WP04 — W4 REACHABILITY RE-GOVERNANCE: PASS`
`RELEASE 1.12 WP04 — W4 PRODUCTION REACHABILITY: <R1_UNREACHABLE_DEFENSIVE|R2_REACHABLE|R3_REQUIRED_UNTESTABLE>`
`RELEASE 1.12 WP04 — W4 DEFENSIVE INVARIANT: <YES|NO>`
`RELEASE 1.12 WP04 — W4 ACTUAL POLICY FUNCTION VALIDATION: <SUFFICIENT|INSUFFICIENT|NOT_APPLICABLE>`
`RELEASE 1.12 WP04 — NEW W4 SOURCE SEAM REQUIRED: <YES|NO>`
`RELEASE 1.12 WP04 — REVISED VALIDATION PARTITION: <HYBRID|EXACT_BYTE_ONLY|NEW_SEAM>`
`RELEASE 1.12 WP04 — RE-GOVERNANCE DECISION: <D1|D2|D3>`
`RELEASE 1.12 WP04 — TRACKED SOURCE MUTATIONS: 0`
`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`
`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`
`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`
`RELEASE 1.12 WP04 — PUBLICATION READINESS: BLOCKED_PENDING_VALIDATION`
`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`
`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`
`RELEASE 1.12 WP04 — W4 REACHABILITY RE-GOVERNANCE MUTATION AUDIT: PASS`

Then exactly one:
`RELEASE 1.12 WP04 — TERRA HYBRID REVISED-V2 VALIDATION AUTHORITY: READY`
or
`RELEASE 1.12 WP04 — TERRA REVISED EXACT-BYTE SANDBOX VALIDATION AUTHORITY: READY`
or
`RELEASE 1.12 WP04 — TERRA NARROW W4 STATE-INJECTION SEAM IMPLEMENTATION AUTHORITY: READY`

Final:
`RELEASE 1.12 WP04 — LUNA W4 REACHABILITY RE-GOVERNANCE COMPLETE`

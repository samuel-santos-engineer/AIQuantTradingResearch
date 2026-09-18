# GPT-5.6 Luna — Release 1.12 WP04 W7 Checkpoint-Persistence Seam Re-Governance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the V2 blocker, decide whether a narrowly scoped checkpoint-persistence seam is justified, and define the minimum safe validation path.
- **GPT-5.6 Terra** — may later implement only the explicitly authorized narrow seam and/or execute the resulting local validation.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governing context

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

Current publication readiness:

```text
BLOCKED
```

Current deployed image remains:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Retained diagnostic evidence remains preserved:

```text
C:\Users\sabsf\AppData\Local\Temp\AIQuantTradingResearch\wp04\initialize-c704973bd0194938ba9e0751e6e91a57
```

## 2. Binding V2 result

V2 exact-byte sandbox validation is blocked.

Reason:

```text
W7 requires failure of checkpoint persistence only.
Checkpoint persistence is an internal direct Set-Content operation.
The exact current wrapper exposes no isolated external surface for failing that one write.
A global Set-Content override would also intercept unrelated transcript/poll/archive/result writes.
Therefore W7 cannot be isolated reliably under exact-byte V2.
```

No sandbox was executed.

No source or external state mutation occurred.

Binding:

```text
V2 = BLOCKED
ACTUAL PRODUCTION CONTROL-FLOW VALIDATION = NOT_PROVEN
PUBLICATION BLOCKER = UNRESOLVED
```

## 3. Re-governance objective

Determine whether the remaining blocker should be resolved by the smallest possible source seam:

```text
one explicit checkpoint-persistence seam
```

rather than:

```text
full lifecycle structural refactor
or
unsafe global Set-Content interception
```

## 4. Governing principle

Do not refactor lifecycle ownership unless required.

The proven defect is not in production lifecycle behavior.

The proven validation limitation is narrowly localized to:

```text
checkpoint persistence fault injection for W7
```

Therefore Luna must prefer the smallest isolated seam capable of making W7 authoritative.

## 5. Candidate seam

Preferred candidate:

```text
Write-Wp04EvidenceCheckpoint
```

or semantically equivalent.

This function/seam must wrap only the current checkpoint-persistence operation(s) that are logically part of the final evidence checkpoint write.

It must not absorb:

```text
transcript persistence
poll-observation persistence
raw archive persistence
fresh extraction persistence
final lifecycle-result persistence
unrelated Set-Content calls
```

## 6. Production behavior requirement

The seam must preserve production behavior byte-for-byte semantically:

```text
same checkpoint file path
same content
same encoding
same overwrite/append semantics
same error behavior
same timing/order
same caller-visible exception behavior
```

The only purpose is:

```text
isolated fault injection for W7
```

No runtime policy change.

## 7. Validation-only fault injection

Luna must choose a PowerShell 5.1-compatible design that allows W7 to fail only the checkpoint write.

Preferred order:

### S1 — local function shadowing

If the wrapper resolves an internal checkpoint writer by function name and that function can be overridden safely in sandbox/session validation without source-level production branching.

### S2 — optional scriptblock parameter narrowly for checkpoint persistence

Only if S1 is not cleanly possible.

The parameter must default to the production writer and must affect only checkpoint persistence.

### S3 — other isolated seam

Only if S1/S2 are unsuitable.

Global `Set-Content` override is explicitly rejected.

## 8. Exact source scope

Luna must determine whether the seam can be added with:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

only.

Preferred:

```text
tracked path count = 1
```

The helper remains untouched unless Luna proves otherwise.

## 9. No broader lifecycle refactor

Unless source review proves otherwise, the following remain out of scope:

```text
extracting entire lifecycle into one orchestration function
moving all external effects behind callbacks
changing helper architecture
changing archive/extraction policy
changing error precedence
changing RB2
changing RestoreOnly behavior
```

## 10. W7 acceptance target

After the seam exists, W7 must prove:

```text
all prior evidence writes succeed
checkpoint persistence alone fails
wrapper classifies evidence preservation failure
RestoreOnly still executes exactly once
restoration failure, if separately induced, still dominates
original qualification result remains retained
```

Required final:

```text
W7 = PASS
```

## 11. Reuse of V2 for W1-W6/W8

Luna must decide whether:

```text
W1-W6 and W8 remain valid for exact-byte/sandbox external interception
```

and only W7 requires the new seam.

Preferred result:

```text
V2+S = exact-byte/sandbox validation plus one isolated checkpoint seam
```

If so, avoid introducing additional seams.

## 12. Validation identity after source change

Because adding the seam changes wrapper bytes, any later exact-byte sandbox validation must hash the **new current tracked wrapper** after the seam implementation.

Required:

```text
tracked current wrapper hash == sandbox copy hash
```

The fact that the wrapper gained a testability seam does not invalidate V2, provided:

```text
production default behavior is unchanged
sandbox copy is exact-byte identical to the new tracked wrapper
```

## 13. Mutation boundary under this Luna authority

Exactly zero:

```text
repository edits
staging
commit
push
PR/merge
Azure
Docker/GHCR
GitHub lifecycle
```

This is read-only governance.

## 14. Acceptance boundary

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

## 15. Required Luna decision

Select exactly one:

### D1 — NARROW CHECKPOINT-PERSISTENCE SEAM REQUIRED

Use if one isolated seam can safely unlock W7.

Expected next authority:

```text
TERRA NARROW CHECKPOINT-PERSISTENCE SEAM IMPLEMENTATION AUTHORITY
```

### D2 — V2 CAN STILL PROVE W7 WITHOUT SOURCE CHANGE

Use only if Luna finds a safe, isolated, reliable interception strategy that does not globally alter unrelated writes.

Expected next authority:

```text
TERRA REVISED EXACT-BYTE SANDBOX VALIDATION AUTHORITY
```

### D3 — BROADER STRUCTURAL CHANGE REQUIRED

Use only if a narrow seam cannot safely isolate W7.

Expected next authority:

```text
LUNA STRUCTURAL REFACTOR DESIGN AUTHORITY
```

## 16. Required output

Return:

- exact reason V2 W7 is blocked;
- whether production defect is proven;
- selected seam design S1/S2/S3 if applicable;
- exact tracked scope;
- production-behavior preservation requirements;
- whether W1-W6/W8 can remain V2-style;
- how W7 will be isolated;
- D1/D2/D3;
- exact next authority;
- zero-mutation audit.

## 17. Terminal markers

`RELEASE 1.12 WP04 — W7 CHECKPOINT-PERSISTENCE SEAM RE-GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — V2 EXACT-BYTE VALIDATION STATUS: BLOCKED_ON_W7`

`RELEASE 1.12 WP04 — FUNCTIONAL PRODUCTION DEFECT PROVEN: NO`

`RELEASE 1.12 WP04 — GLOBAL SET-CONTENT OVERRIDE: REJECTED`

`RELEASE 1.12 WP04 — STRUCTURAL REFACTOR REQUIRED BY DEFAULT: NO`

`RELEASE 1.12 WP04 — SELECTED CHECKPOINT SEAM DESIGN: <S1|S2|S3|NONE>`

`RELEASE 1.12 WP04 — TRACKED SOURCE CHANGE COUNT IF IMPLEMENTED: <0|1>`

`RELEASE 1.12 WP04 — HELPER MODIFICATION REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — W1-W6/W8 V2 VALIDATION MODEL: <PRESERVE|REVISE>`

`RELEASE 1.12 WP04 — W7 ISOLATED FAULT INJECTION: <PROVABLE|NOT_PROVEN>`

`RELEASE 1.12 WP04 — RE-GOVERNANCE DECISION: <D1|D2|D3>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`

`RELEASE 1.12 WP04 — W7 CHECKPOINT-PERSISTENCE SEAM RE-GOVERNANCE MUTATION AUDIT: PASS`

Then exactly one:

`RELEASE 1.12 WP04 — TERRA NARROW CHECKPOINT-PERSISTENCE SEAM IMPLEMENTATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA REVISED EXACT-BYTE SANDBOX VALIDATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA STRUCTURAL REFACTOR DESIGN AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA W7 CHECKPOINT-PERSISTENCE SEAM RE-GOVERNANCE COMPLETE`

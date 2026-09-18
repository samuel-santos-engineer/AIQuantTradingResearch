# GPT-5.6 Luna — Release 1.12 WP04 W3 Non-Terminating Extraction Failure Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map
- **GPT-5.6 Luna** — PRIMARY: reconcile the W3 production-wrapper failure and govern the narrow remediation.
- **GPT-5.6 Terra** — implements only a later Luna-authorized remediation.
- **GPT-5.6 Sol** — supporting analysis only; never replaces Luna/Terra.

## Binding state

Post-remediation Hybrid Revised-V2 stopped at W3:

```text
W1 = PASS
W2 = PASS
W3 = BLOCKED / PRODUCTION DEFECT EXPOSED
W4 = PASS — authorized policy carry-forward
W5/W6/W7/W8 = NOT_RUN
```

Preserve W1/W2/W4 evidence. The authority correctly stopped before W5.

## Proven W3 symptom

A genuine post-retention extraction error occurs in the exact production wrapper under Windows PowerShell 5.1, but it is non-terminating. The existing catch is therefore bypassed and the wrapper records `EMPTY/PASS/SUCCESS` instead of the governed `FAIL/EVIDENCE_PRESERVATION_FAILED`.

Classify, if confirmed:

```text
FUNCTIONAL PRODUCTION DEFECT PROVEN = YES
DEFECT CLASS = POWERSHELL_NONTERMINATING_EXTRACTION_ERROR_NOT_CAUGHT
```

## Required read-only inspection

Inspect the current `initialize-qualification.ps1` around archive retention and fresh-window extraction. Identify:
- the exact operation emitting the W3 error;
- its PowerShell 5.1 terminating/non-terminating behavior;
- the intended `try/catch` boundary;
- any local `-ErrorAction` or `$ErrorActionPreference`;
- the narrowest way to make that genuine extraction failure terminate into the existing catch.

Do not assume every PowerShell error is terminating.

## Remediation rule

The production contract is:

```text
successful archive retention + genuine extraction failure
=> Extraction=FAIL
=> evidence preservation fails
=> Final=EVIDENCE_PRESERVATION_FAILED
   unless restoration failure takes precedence
```

Prefer an operation-scoped `-ErrorAction Stop` or equally narrow mechanism if supported by the exact source. Do not globally set `$ErrorActionPreference='Stop'` unless separately proven necessary and safe.

Do not:
- change the truth table;
- assign `FAIL` artificially;
- add a W3 injection seam;
- refactor lifecycle orchestration;
- change S2;
- change Deferred/RestoreOnly/RB2;
- modify the helper unless it owns the failing operation.

## Preferred tracked scope

Expected if D1:

```text
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
CREATE 0
DELETE 0
```

No other tracked path.

## Required later Terra validation

Under Windows PowerShell 5.1.26100.9444 prove:

```text
E1 successful extraction with records => PASS
E2 successful extraction with no records => EMPTY
E3 genuine post-retention extraction error => caught => FAIL
E4 W3 final result => EVIDENCE_PRESERVATION_FAILED
E5 restoration failure still dominates evidence failure
```

Also require parser clean, poll-observation `.ToArray()` fix preserved, W4 unchanged, S2 unchanged, and no regression to W1/W2 behavior.

## Hybrid consequence

Current W3 receives no acceptance credit. W5-W8 remain unrun.

Luna must decide the post-remediation hybrid restart point. Because W1 and W2 both exercise extraction behavior, default conservatively to `W1` unless source proof establishes the remediation is failure-path-only and cannot affect PASS/EMPTY behavior.

W4 remains authorized carry-forward unless the remediation touches its policy function.

## Mutation boundary

This Luna authority is read-only:

```text
repository mutations = 0
Azure = 0
Docker/GHCR = 0
Git/GitHub lifecycle = 0
```

Preserve:

```text
PUBLICATION BLOCKER = UNRESOLVED
NEW IMAGE REQUIRED = NO
NEW DIAGNOSTIC RUN = NOT_AUTHORIZED
ACCEPTANCE RETRY = NOT_AUTHORIZED
INITIALIZE D3 ACCEPTANCE = NOT_PROVEN
REOPEN = NOT_AUTHORIZED
WP04 #263 = OPEN
WP05 = NOT_STARTED
RETAINED LOCAL EVIDENCE = PRESERVE
```

## Decision

Select exactly one:

```text
D1 — NARROW ONE-PATH EXTRACTION ERROR-SEMANTICS REMEDIATION
D2 — EXTRACTION CONTRACT REQUIRES BROADER RE-GOVERNANCE
D3 — W3 DEFECT NOT ATTRIBUTABLE TO PRODUCTION SOURCE
```

D1 next authority:

```text
TERRA ONE-PATH W3 EXTRACTION ERROR-SEMANTICS REMEDIATION AUTHORITY
```

## Required output

Return the exact failing operation, PowerShell 5.1 error semantics, catch boundary, selected correction, helper decision, E1-E5 contract, restart point, W4 carry-forward decision, D1/D2/D3, next authority, and zero-mutation audit.

## Terminal markers

`RELEASE 1.12 WP04 — W3 NON-TERMINATING EXTRACTION FAILURE RECONCILIATION: PASS`
`RELEASE 1.12 WP04 — FUNCTIONAL PRODUCTION DEFECT PROVEN: YES`
`RELEASE 1.12 WP04 — DEFECT CLASS: POWERSHELL_NONTERMINATING_EXTRACTION_ERROR_NOT_CAUGHT`
`RELEASE 1.12 WP04 — W3 FAILING EXTRACTION OPERATION: <value>`
`RELEASE 1.12 WP04 — W3 ERROR SEMANTICS: <value>`
`RELEASE 1.12 WP04 — EXISTING EXTRACTION CATCH BOUNDARY: <value>`
`RELEASE 1.12 WP04 — SELECTED EXTRACTION REMEDIATION: <value>`
`RELEASE 1.12 WP04 — GLOBAL ERRORACTIONPREFERENCE CHANGE REQUIRED: <YES|NO>`
`RELEASE 1.12 WP04 — NEW W3 INJECTION SEAM REQUIRED: NO`
`RELEASE 1.12 WP04 — TRACKED SOURCE CHANGE COUNT IF IMPLEMENTED: <value>`
`RELEASE 1.12 WP04 — HELPER MODIFICATION REQUIRED: <YES|NO>`
`RELEASE 1.12 WP04 — W1 PRESERVED EVIDENCE: PASS`
`RELEASE 1.12 WP04 — W2 PRESERVED EVIDENCE: PASS`
`RELEASE 1.12 WP04 — W3 ACCEPTANCE CREDIT: NO`
`RELEASE 1.12 WP04 — W4 CARRY-FORWARD: <AUTHORIZED|REVALIDATE>`
`RELEASE 1.12 WP04 — W5/W6/W7/W8: NOT_RUN`
`RELEASE 1.12 WP04 — POST-REMEDIATION HYBRID RESTART POINT: <W1|W3>`
`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`
`RELEASE 1.12 WP04 — RECONCILIATION DECISION: <D1|D2|D3>`
`RELEASE 1.12 WP04 — TRACKED SOURCE MUTATIONS: 0`
`RELEASE 1.12 WP04 — AZURE MUTATIONS: 0`
`RELEASE 1.12 WP04 — GIT/GITHUB LIFECYCLE MUTATIONS: 0`
`RELEASE 1.12 WP04 — DOCKER/GHCR MUTATIONS: 0`
`RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED`
`RELEASE 1.12 WP04 — NEW DIAGNOSTIC RUN: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — ACCEPTANCE RETRY: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — INITIALIZE D3 ACCEPTANCE: NOT_PROVEN`
`RELEASE 1.12 WP04 — REOPEN: NOT_AUTHORIZED`
`RELEASE 1.12 WP04 — RETAINED LOCAL EVIDENCE: PRESERVE`
`RELEASE 1.12 WP04 — W3 RECONCILIATION MUTATION AUDIT: PASS`

Then exactly one next-authority marker and finish with:

`RELEASE 1.12 WP04 — LUNA W3 NON-TERMINATING EXTRACTION FAILURE RECONCILIATION COMPLETE`

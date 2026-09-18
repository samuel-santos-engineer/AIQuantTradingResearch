# GPT-5.6 Luna — Release 1.12 WP04 Null-Semantics Read-Only Investigation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: perform read-only investigation of Azure App Service null/deleted app-setting semantics and reconcile the prior `PRESENT_NULL` observation.
- **GPT-5.6 Terra** — may execute only a later explicitly authorized Azure cleanup, helper remediation, timeout diagnostic, or fresh qualification attempt.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

```text
#263
```

Current source commit:

```text
2532f6abd4677edfb205c26c083a534783038979
```

Current deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Failed initialize RunId:

```text
initialize-fa16107e99d640d88bb9de91ef0da8ab
```

This RunId is permanently forbidden from reuse.

Prior qualification result:

```text
Q5 — RESTORATION_FAILED
```

Initialize D3:

```text
NOT_PROVEN
```

Fresh qualification retry:

```text
NOT_AUTHORIZED
```

## 2. Current reconciled facts

Current ARM `appsettings/list` and current Azure CLI reads show:

```text
all six qualification setting names = ABSENT
```

No current tombstone/persisted-null state is proven.

The earlier post-attempt read-back showed:

```text
all six qualification setting names = PRESENT_NULL
```

The cause of that earlier representation remains unresolved.

Current possible explanations:

```text
A. transient Azure consistency lag after delete
B. Azure CLI/ARM serialization artifact for recently deleted entries
C. a persisted null-valued representation that later converged to absence
D. another Azure control-plane behavior
```

## 3. Helper behavior

The current governed helper behavior is classified by the completed read-only reconciliation as:

```text
H2
```

Binding observed semantics:

```text
delete all six governed qualification setting names
restore only pre-snapshot values that were non-null
do not intentionally write null
do not intentionally write empty string
```

This authority must preserve that observed behavior definition.

Do not infer helper defect merely from the earlier `PRESENT_NULL` read-back.

## 4. Runtime source semantics

Source review has established:

```text
null/absent qualification values do not activate qualification mode
```

However:

```text
direct runtime equivalence of Azure's earlier PRESENT_NULL representation = NOT_PROVEN
```

Do not promote source-level null handling into proof of Azure representation semantics.

## 5. Investigation objective

Determine, without mutation, what Azure App Service control-plane semantics can explain:

```text
DELETE requested
→ helper reports restoration success
→ immediate/near-immediate read-back = PRESENT_NULL
→ later ARM/CLI read-back = ABSENT
```

The investigation must answer whether the prior Q5 should remain historically valid and whether any helper remediation or cleanup is actually required now.

No Azure, repository, helper, Git, GitHub, image, or qualification mutation is authorized.

## 6. Read-only Azure evidence scope

Use only read-only operations.

Allowed surfaces include:

```text
az webapp config appsettings list
az webapp show
az resource show / ARM GET equivalents
Azure Activity Log read-only queries
Azure CLI debug metadata only if sanitized
official Azure App Service/ARM documentation
```

Do not call any mutating endpoint.

Do not enable logging.

Do not restart.

Do not change app settings.

## 7. Six governed names

Investigate exactly:

```text
Worker__Mode
PersistentSqliteQualification__Phase
PersistentSqliteQualification__HttpEvidenceEnabled
PersistentSqliteQualification__EvidenceOutputPath
PersistentSqliteQualification__RunId
PersistentSqliteQualification__HttpEvidenceToken
```

Current required state:

```text
ABSENT
```

Return only presence/value-state metadata, never secret values.

## 8. Control-plane consistency question

Determine whether Azure App Service app-setting deletion is documented or empirically observable as:

```text
strongly consistent
eventually consistent
projection-dependent
CLI-cache/projection dependent
not documented
```

If official documentation does not state consistency semantics, say:

```text
CONTROL_PLANE_CONSISTENCY = NOT_DOCUMENTED
```

Do not infer eventual consistency from one observation without evidence.

## 9. Serialization/projection question

Determine whether Azure CLI or ARM response shapes can return:

```text
name present + value null
```

for:

```text
deleted settings
redacted settings
write-only secret values
slot-setting metadata
transient control-plane state
```

Distinguish each possibility.

Required output per possibility:

```text
SUPPORTED
NOT_SUPPORTED
NOT_PROVEN
```

## 10. Secret-redaction question

Specifically determine whether App Service app-setting APIs intentionally redact secret-bearing values as null while preserving names.

If this behavior exists, determine whether it applies generically to all settings or only specific provider/API surfaces.

Do not assume `PersistentSqliteQualification__HttpEvidenceToken=PRESENT_NULL` means the token remained.

Current safe fact:

```text
HTTP evidence token secret remnant = ABSENT_OR_NULL
```

## 11. Activity Log correlation

If available read-only, inspect Activity Log around the failed attempt.

Goal:

```text
prove whether a delete/app-setting write operation completed successfully
prove whether a later app-setting write could have reintroduced names
```

Do not require Activity Log to reveal secret values.

Return:

```text
delete/write control-plane operation = PROVEN | NOT_PROVEN
later reintroduction operation = PROVEN | NOT_PROVEN
```

## 12. Temporal evidence limitation

If the exact earlier `PRESENT_NULL` response payload and timestamp were not preserved sufficiently, state:

```text
HISTORICAL REPRESENTATION CAUSE = NOT_PROVEN
```

Do not reconstruct missing evidence from current state.

Current absence does not retroactively prove the immediate earlier read-back was semantically absent.

## 13. Historical Q5 treatment

Evaluate separately:

### HISTORICAL CONTRACT FIDELITY

At the time of the failed attempt:

```text
pre-state = ABSENT
immediate read-back = PRESENT_NULL
```

Under the strict then-binding contract:

```text
exact restoration fidelity = FAIL
```

Unless evidence proves the earlier response was only a non-persisted projection artifact, preserve:

```text
Q5 historical classification = VALID
```

A later converged `ABSENT` state does not automatically rewrite the historical result.

## 14. Current cleanup requirement

Because the current state is:

```text
all six names = ABSENT
```

default:

```text
CURRENT AZURE TOMBSTONE CLEANUP REQUIRED = NO
```

Change this only if read-only evidence proves hidden/persisted tombstone state still exists.

Do not authorize a cleanup mutation merely to "make sure."

## 15. Helper remediation requirement

The helper deletes names and restores only original non-null values.

Therefore helper remediation is justified only if evidence proves one of:

```text
delete semantics are wrong
restore ordering causes recurrence
Azure CLI command choice predictably yields persistent null tombstones
strict restoration verification is insufficient and must wait/poll for convergence
```

Possible result:

```text
HELPER REMEDIATION REQUIRED = YES | NO | VERIFICATION_ONLY | NOT_PROVEN
```

`VERIFICATION_ONLY` means helper mutation, if later authorized, should change only post-restore verification/convergence handling, not delete semantics.

## 16. Convergence verification option

Evaluate whether the narrowest future helper correction is:

```text
after delete, poll read-only until all originally absent names are actually absent
bounded timeout
fail closed if names remain present
```

This is a verification-semantic change, not a runtime/source/image change.

If supported, classify:

```text
BOUNDED RESTORATION CONVERGENCE POLL = RECOMMENDED
```

Otherwise:

```text
NOT_REQUIRED | NOT_PROVEN
```

## 17. Timeout independence

The ~38-second qualification evidence timeout remains a separate unresolved issue.

Do not infer that `PRESENT_NULL` caused the timeout.

Required:

```text
TIMEOUT CAUSALLY LINKED TO PRESENT_NULL = NOT_PROVEN
```

unless direct evidence proves otherwise.

No timeout remediation is authorized under this authority.

## 18. Candidate investigation outcomes

Select exactly one:

### N1 — TRANSIENT_OR_PROJECTION_ARTIFACT_PROVEN

Use only if evidence proves the earlier `PRESENT_NULL` did not represent persisted app-setting names.

Consequence:

```text
historical Q5 may be eligible for reclassification
current cleanup = no
helper remediation = no or verification-only
```

### N2 — CONTROL_PLANE_CONVERGENCE_BEHAVIOR_PROVEN

Use if deletion succeeded, names temporarily appeared null, then converged to absence as a documented/proven control-plane behavior.

Consequence:

```text
historical strict Q5 remains valid unless contract explicitly allowed convergence
current cleanup = no
helper verification-only remediation likely required
```

### N3 — PERSISTED_NULL_TOMBSTONES_PROVEN

Use if names truly persisted null and later disappeared through another proven operation or mechanism.

Consequence:

```text
historical Q5 remains valid
current cleanup depends on current state
helper remediation may be required
```

### N4 — CAUSE NOT PROVEN; CURRENT STATE CLEAN

Use if:

```text
current names are absent
historical cause cannot be proven
no evidence of current secret/configuration residue
```

Consequence:

```text
historical Q5 remains valid
current cleanup = no
helper remediation = NOT_PROVEN or verification-only
timeout reconciliation remains next
```

## 19. Decision options

Select exactly one:

```text
D1 — RECLASSIFY_HISTORICAL_RESTORATION_PASS
D2 — PRESERVE_Q5_AND_ADD_BOUNDED_RESTORATION_CONVERGENCE_VERIFICATION
D3 — PRESERVE_Q5_AND_REQUIRE_HELPER_DELETE_SEMANTICS_REMEDIATION
D4 — PRESERVE_Q5_NO_CURRENT_CLEANUP; PROCEED_TO_TIMEOUT_RECONCILIATION
D5 — MORE_READ_ONLY_EVIDENCE_REQUIRED
```

### D1

Only with N1.

### D2

Use when helper deletion is correct but immediate strict read-back can transiently differ from final absence.

### D3

Use only if helper deletion behavior itself is proven defective.

### D4

Use when current state is clean, historical cause is not proven, and there is no sufficient basis to mutate helper behavior before reconciling the timeout.

### D5

Use only if another narrow read-only evidence source is concretely identified and likely to resolve the ambiguity.

## 20. Qualification retry gate

This authority does not authorize retry.

Required:

```text
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

The next step after this investigation must be separately governed.

## 21. No mutation boundary

Under this authority:

```text
Azure mutations = 0
Azure restarts = 0
repository edits = 0
helper edits = 0
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
qualification attempts = 0
GitHub mutations = 0
```

## 22. Required output

Return:

- current six-setting state;
- exact read-only Azure surfaces inspected;
- control-plane consistency finding;
- serialization/projection finding;
- secret-redaction finding;
- Activity Log finding if available;
- historical representation cause;
- historical Q5 validity;
- current cleanup requirement;
- helper remediation requirement;
- convergence-poll recommendation;
- timeout causal relationship;
- N1/N2/N3/N4;
- D1/D2/D3/D4/D5;
- fresh qualification remains blocked;
- zero-mutation audit.

## 23. Terminal markers

Required:

`RELEASE 1.12 WP04 — NULL-SEMANTICS READ-ONLY INVESTIGATION: PASS`

`RELEASE 1.12 WP04 — FAILED RUN ID: initialize-fa16107e99d640d88bb9de91ef0da8ab`

`RELEASE 1.12 WP04 — FAILED RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — CURRENT QUALIFICATION SETTINGS STATE: ABSENT`

`RELEASE 1.12 WP04 — HISTORICAL QUALIFICATION SETTING READ-BACK: PRESENT_NULL`

`RELEASE 1.12 WP04 — HELPER DELETE SEMANTICS: PROVEN`

`RELEASE 1.12 WP04 — HELPER WRITES NULL: NO`

`RELEASE 1.12 WP04 — CONTROL-PLANE CONSISTENCY: <STRONG|EVENTUAL|PROJECTION_DEPENDENT|NOT_DOCUMENTED|NOT_PROVEN>`

`RELEASE 1.12 WP04 — PRESENT_NULL SERIALIZATION CAUSE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HISTORICAL REPRESENTATION CAUSE: <TRANSIENT|PROJECTION_ARTIFACT|PERSISTED_NULL|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HISTORICAL Q5 CLASSIFICATION: <VALID|RECLASSIFY_PASS>`

`RELEASE 1.12 WP04 — CURRENT AZURE TOMBSTONE CLEANUP REQUIRED: <YES|NO|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HELPER REMEDIATION REQUIRED: <YES|NO|VERIFICATION_ONLY|NOT_PROVEN>`

`RELEASE 1.12 WP04 — BOUNDED RESTORATION CONVERGENCE POLL: <RECOMMENDED|NOT_REQUIRED|NOT_PROVEN>`

`RELEASE 1.12 WP04 — TIMEOUT CAUSALLY LINKED TO PRESENT_NULL: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — NULL-SEMANTICS INVESTIGATION OUTCOME: <N1|N2|N3|N4>`

`RELEASE 1.12 WP04 — NULL-SEMANTICS INVESTIGATION DECISION: <D1|D2|D3|D4|D5>`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — NULL-SEMANTICS READ-ONLY INVESTIGATION MUTATION AUDIT: PASS`

Then exactly one:

`RELEASE 1.12 WP04 — TERRA RESTORATION VERIFICATION-ONLY HELPER REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA HELPER DELETE-SEMANTICS REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA TIMEOUT RECONCILIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA ADDITIONAL READ-ONLY NULL-SEMANTICS INVESTIGATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — RESTORATION RECLASSIFIED PASS; LUNA TIMEOUT RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA NULL-SEMANTICS READ-ONLY INVESTIGATION COMPLETE`

# GPT-5.6 Luna — Release 1.12 WP04 Azure Null-Valued Qualification-Setting Restoration Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile Azure `PRESENT_NULL` qualification-setting read-back against the strict WP04 restoration contract and select the narrowest valid next action.
- **GPT-5.6 Terra** — execute only a later explicitly authorized restoration cleanup, helper remediation/publication, or fresh qualification action.
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

Current governed deployed image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Acceptance boundary:

```text
PERSISTENCE_SPECIFIC
```

Twelve Data secret ownership:

```text
WP05
```

Normal root `503`:

```text
EXPECTED_DOWNSTREAM_CONFIGURATION_BLOCKER
```

## 2. Failed initialize attempt

Fresh failed RunId:

```text
initialize-fa16107e99d640d88bb9de91ef0da8ab
```

This RunId is now permanently forbidden from reuse.

Attempt facts:

```text
wrapper provenance gate = PASS
deployed image preflight = PASS
qualification pre-state = PASS
App Service = Running/Normal
WEBSITES_PORT = 8501
registry credentials = absent
SCM basic auth = disabled
FTP basic auth = disabled
diagnostic logging = disabled
authorized qualification attempts = 1
authorized Azure mutation count = 3
qualification attempt timeout ~= 38 seconds
D3 evidence = NOT_PROVEN
helper-reported restoration = success
Azure read-back restoration = FAIL
all six temporary qualification setting names = PRESENT_NULL
source mutations = 0
Git/GitHub lifecycle mutations = 0
```

Canonical result:

```text
Q5 — RESTORATION_FAILED
```

No further Azure mutation occurred.

## 3. Reconciliation objective

Determine whether Azure App Service app settings that remain as named entries with null values after helper restoration are:

```text
A. semantically equivalent to absence for the deployed runtime;
B. operationally harmless but non-compliant with the strict restoration contract;
C. runtime-significant and therefore an active qualification-mode/configuration risk;
D. insufficiently understood without further read-only proof.
```

Then select the narrowest valid next action.

No mutation is authorized under this Luna authority.

## 4. Six governed temporary setting names

Reconcile exactly:

```text
Worker__Mode
PersistentSqliteQualification__Phase
PersistentSqliteQualification__HttpEvidenceEnabled
PersistentSqliteQualification__EvidenceOutputPath
PersistentSqliteQualification__RunId
PersistentSqliteQualification__HttpEvidenceToken
```

Pre-attempt state was:

```text
ABSENT
```

Post-attempt Azure read-back is:

```text
PRESENT_NULL
```

Strict prior restoration contract required:

```text
ABSENT
```

Do not silently redefine `PRESENT_NULL` as `ABSENT`.

## 5. Read-only source semantics review

Inspect current source and scripts read-only.

At minimum inspect:

```text
container/entrypoint.sh
src/AIQuantTradingResearch.Worker/Program.cs
qualification configuration binding
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
any shared Azure app-setting restore helper used by the qualification flow
```

For each of the six names determine how these runtime states are interpreted:

```text
missing
null
empty string
non-empty value
```

Especially prove whether:

```text
Worker__Mode = null
```

can activate, alter, or interfere with qualification/normal mode.

Also prove whether null-valued `PersistentSqliteQualification__*` names can change .NET configuration binding or entrypoint branching.

## 6. Azure representation review

Using read-only Azure CLI/API evidence only, determine whether `PRESENT_NULL` means:

```text
a persisted named app-setting entry whose value is null;
a CLI serialization artifact for an actually absent setting;
a slot-setting metadata tombstone;
another Azure representation.
```

Use presence-sensitive queries, not truthiness-only tests.

Do not print secret values.

Required distinction:

```text
NAME EXISTS = true|false
VALUE STATE = null|empty|non-empty|not-returned
SLOT SETTING METADATA = true|false|not-applicable|not-proven
```

## 7. Effective runtime contract vs exact restoration contract

Evaluate two independent questions.

### RUNTIME EQUIVALENCE

```text
Are PRESENT_NULL entries semantically equivalent to absent for application/entrypoint execution?
```

Return:

```text
PROVEN_EQUIVALENT
NOT_EQUIVALENT
NOT_PROVEN
```

### RESTORATION FIDELITY

```text
Did post-state exactly equal pre-state?
```

Because pre-state was `ABSENT` and post-state is `PRESENT_NULL`, default classification is:

```text
EXACT_RESTORATION = FAIL
```

Only change that if Azure evidence proves the apparent entries are not actually persisted names.

Operational equivalence does not automatically satisfy exact restoration fidelity.

## 8. Helper behavior review

Determine exactly how the helper restores app settings.

Classify the current implementation as one of:

```text
H1 — correctly deletes settings that were originally absent
H2 — writes null for settings that were originally absent
H3 — restores through an Azure command whose semantics produce tombstones
H4 — behavior not proven
```

If H2 or H3, identify the exact helper path(s) responsible.

No source edit under this authority.

## 9. Security review

Because one of the names is:

```text
PersistentSqliteQualification__HttpEvidenceToken
```

prove only:

```text
value is null/not returned
no secret value remains
```

Never print the prior token.

If any non-null token value remains:

```text
SECURITY RESTORATION = FAIL
```

and restoration cleanup becomes highest priority.

## 10. Qualification-mode contamination risk

Determine whether the six `PRESENT_NULL` entries could cause a future preflight to fail or contaminate a future qualification/normal-mode run.

Existing strict preflight semantics treat any named setting as present.

Therefore unless the contract is deliberately changed:

```text
PRESENT_NULL => FUTURE STRICT PRECONDITION FAIL
```

This is independently important even if runtime semantics treat null as absent.

## 11. Candidate reconciliation outcomes

Evaluate exactly these.

### O1 — AZURE_READBACK_ARTIFACT; EXACT ABSENCE ACTUALLY PRESERVED

Use only if read-only Azure evidence proves the six names do not actually persist and `PRESENT_NULL` is merely a projection/serialization artifact.

Consequence:

```text
restoration may be reclassified PASS
helper remediation not required
```

### O2 — RUNTIME-EQUIVALENT NULL TOMBSTONES; STRICT RESTORATION STILL FAILED

Use if:

```text
names persist
values are null
runtime semantics treat them as absent
but exact pre-state fidelity is not preserved
```

Consequence:

```text
Q5 remains valid
bounded Azure cleanup is required before any new qualification
helper must be evaluated for remediation to prevent recurrence
```

### O3 — NULL TOMBSTONES ARE RUNTIME-SIGNIFICANT

Use if null presence changes mode/binding/entrypoint behavior.

Consequence:

```text
immediate bounded restoration cleanup required
helper remediation required
no qualification until both are resolved
```

### O4 — INSUFFICIENT EVIDENCE

Use if Azure/source semantics cannot be proven read-only.

Consequence:

```text
no qualification
next authority = narrowly scoped read-only semantics investigation
```

## 12. Remediation decision options

Select exactly one.

### D1 — RECLASSIFY_RESTORATION_PASS

Allowed only with O1.

No cleanup/helper mutation.

### D2 — BOUNDED_AZURE_TOMBSTONE_CLEANUP_THEN_HELPER_REMEDIATION

Preferred for O2 when helper is proven to recreate tombstones.

Sequence:

```text
1. Terra deletes exactly the six null-valued qualification setting names
2. prove exact ABSENT read-back
3. no restart unless source/Azure semantics prove required
4. Terra/Luna separately remediate helper restoration semantics
5. publish helper source if changed
6. only then authorize another fresh qualification attempt
```

### D3 — BOUNDED_AZURE_TOMBSTONE_CLEANUP_ONLY

Use only if O2 is proven but helper already has correct delete semantics and tombstones arose from a one-off/external mechanism.

### D4 — URGENT_CLEANUP_AND_HELPER_REMEDIATION

Use for O3.

### D5 — READ_ONLY_SEMANTICS_INVESTIGATION_REQUIRED

Use for O4.

## 13. Cleanup contract if later authorized

A later Terra cleanup authority may delete only the six governed qualification names.

It must not modify:

```text
TwelveData__ApiKey
WEBSITES_PORT
linuxFxVersion
startup command
registry settings
SCM/FTP auth
diagnostic logging
plan/SKU
other app settings
```

Required target state:

```text
all six names = ABSENT
```

No secret values may be printed.

## 14. Restart policy for cleanup

Default:

```text
cleanup restart = 0
```

A restart may be authorized only if Luna proves Azure App Service requires one for the deleted settings to take effect and that runtime verification materially requires it.

Do not use a restart as blind recovery.

## 15. Helper remediation principle

If helper remediation is required, the invariant is:

```text
ORIGINAL ABSENT → RESTORE BY DELETE
ORIGINAL PRESENT → RESTORE ORIGINAL VALUE/METADATA
```

Never restore an originally absent setting by writing null or empty merely because runtime semantics are equivalent.

The helper must preserve exact pre-state, not just approximate runtime behavior.

## 16. Timeout scope

This reconciliation does not yet attempt to explain the ~38-second evidence timeout.

Because restoration failed, restoration fidelity is the immediate blocker.

After exact restoration is recovered and helper behavior is corrected, Luna must separately decide whether the timeout needs additional reconciliation before another attempt.

Do not bundle timeout remediation here.

## 17. Qualification boundary

Required:

```text
FRESH QUALIFICATION RETRY = NOT_AUTHORIZED
```

until:

```text
exact six-setting absence restored
helper recurrence risk reconciled
Q5 resolved
```

The failed RunId:

```text
initialize-fa16107e99d640d88bb9de91ef0da8ab
```

must never be reused.

## 18. Mutation boundary

Under this Luna authority:

```text
Azure mutations = 0
Azure restarts = 0
source edits = 0
staging = 0
commits = 0
pushes = 0
Docker builds = 0
GHCR publications = 0
qualification attempts = 0
PR mutations = 0
issue mutations = 0
Project mutations = 0
milestone mutations = 0
```

## 19. Required output

Return:

- exact Azure meaning of each `PRESENT_NULL` entry;
- runtime semantics of null vs absent for each relevant setting;
- whether secret token value is fully absent/null;
- exact restoration fidelity classification;
- helper behavior classification H1–H4;
- future preflight impact;
- O1/O2/O3/O4;
- D1/D2/D3/D4/D5;
- whether immediate Azure cleanup is required;
- whether helper source remediation is required;
- whether restart is required;
- whether a new image would be required;
- whether timeout reconciliation remains pending;
- fresh qualification remains blocked;
- zero-mutation audit.

## 20. Terminal markers

Required:

`RELEASE 1.12 WP04 — NULL-VALUED RESTORATION RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — FAILED RUN ID: initialize-fa16107e99d640d88bb9de91ef0da8ab`

`RELEASE 1.12 WP04 — FAILED RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — PRIOR QUALIFICATION RESULT: Q5_RESTORATION_FAILED`

`RELEASE 1.12 WP04 — QUALIFICATION SETTING POST-STATE: PRESENT_NULL`

`RELEASE 1.12 WP04 — NULL RUNTIME EQUIVALENCE: <PROVEN_EQUIVALENT|NOT_EQUIVALENT|NOT_PROVEN>`

`RELEASE 1.12 WP04 — EXACT RESTORATION FIDELITY: <PASS|FAIL>`

`RELEASE 1.12 WP04 — HTTP EVIDENCE TOKEN SECRET REMNANT: <ABSENT_OR_NULL|PRESENT|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HELPER RESTORATION BEHAVIOR: <H1|H2|H3|H4>`

`RELEASE 1.12 WP04 — FUTURE STRICT PREFLIGHT WITH PRESENT_NULL: <PASS|FAIL>`

`RELEASE 1.12 WP04 — RESTORATION RECONCILIATION OUTCOME: <O1|O2|O3|O4>`

`RELEASE 1.12 WP04 — RESTORATION RECONCILIATION DECISION: <D1|D2|D3|D4|D5>`

`RELEASE 1.12 WP04 — AZURE TOMBSTONE CLEANUP REQUIRED: <YES|NO|NOT_PROVEN>`

`RELEASE 1.12 WP04 — HELPER REMEDIATION REQUIRED: <YES|NO|NOT_PROVEN>`

`RELEASE 1.12 WP04 — CLEANUP RESTART REQUIRED: <YES|NO|NOT_PROVEN>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: NO`

`RELEASE 1.12 WP04 — TIMEOUT RECONCILIATION: PENDING`

`RELEASE 1.12 WP04 — FRESH QUALIFICATION RETRY: NOT_AUTHORIZED`

`RELEASE 1.12 WP04 — NULL-VALUED RESTORATION RECONCILIATION MUTATION AUDIT: PASS`

Then exactly one:

`RELEASE 1.12 WP04 — TERRA BOUNDED QUALIFICATION TOMBSTONE CLEANUP AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA HELPER RESTORATION REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA CLEANUP THEN HELPER REMEDIATION SEQUENCE: READY`

or

`RELEASE 1.12 WP04 — LUNA NULL-SEMANTICS READ-ONLY INVESTIGATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — RESTORATION RECLASSIFIED PASS; LUNA TIMEOUT RECONCILIATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA NULL-VALUED RESTORATION RECONCILIATION COMPLETE`

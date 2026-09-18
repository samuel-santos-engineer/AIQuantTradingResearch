# GPT-5.6 Luna — Release 1.12 WP04 Terminal Timeout & Preflight Image-Proof Reconciliation Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the clean terminal `Timeout` result together with the failed deployed-image preflight proof and select the narrowest next governed step.
- **GPT-5.6 Terra** — execute only the later explicitly authorized remediation/validation/publication/qualification.
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
047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

Expected deployed instrumented image:

```text
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

Fresh failed RunId:

```text
initialize-94c5801cdc0b49e5933760c02313d486
```

This RunId is permanently forbidden for reuse.

## 2. Established execution result

The one authorized initialize attempt completed internally.

Helper terminal result:

```text
WP04_HELPER_TERMINAL_RESULT=FAILURE
WP04_HELPER_TERMINAL_CLASS=Timeout
WP04_HELPER_TERMINAL_EXIT_CODE=1
WP04_HELPER_TERMINAL_SETTINGS_RESTORATION=PASS
WP04_HELPER_TERMINAL_ELAPSED_MS=36282
```

Established facts:

```text
external T6 termination = NOT_INDICATED
temporary qualification settings application = 1
explicit qualification restart = 1
temporary qualification settings restoration = PASS
D3 initialize evidence = NOT_PROVEN
retry/reopen = NOT_AUTHORIZED
normal runtime restoration = PASS
```

## 3. Critical preflight defect

The qualification authority required proof of the exact deployed image digest before Azure mutation.

Observed preflight marker:

```text
WP04_PREFLIGHT_IMAGE=
```

was blank.

Therefore this attempt **does not satisfy the authority's full preflight gate**.

Binding consequence:

```text
FRESH INITIALIZE QUALIFICATION PRECHECK = NOT_PROVEN
```

The attempt may be used as evidence of helper behavior and a clean terminal `Timeout`, but it must not receive WP04 initialize acceptance credit.

Do not retroactively infer the pre-mutation digest from later/post-run Azure state.

## 4. Reconciliation objective

Determine why the committed helper/preflight surface emitted a blank image identity and whether that defect is independent from, causally related to, or merely concurrent with the terminal `Timeout`.

Luna must answer:

1. What exact command/expression populates `WP04_PREFLIGHT_IMAGE`?
2. Which Azure CLI property is expected?
3. Does the command work under Windows PowerShell 5.1?
4. Can the returned object/property shape differ across:
   - `az webapp show`
   - `az webapp config container show`
   - `az webapp config show`
   - Linux Web App configuration API versions?
5. Can JMESPath/property casing/array shape cause a blank scalar without nonzero `$LASTEXITCODE`?
6. Does the helper validate non-empty image identity before mutation?
7. Does it require exact digest equality?
8. If image proof is blank, why did execution continue?
9. Is this a helper preflight-governance defect that must be corrected before any further Azure qualification?
10. Does the 36.282-second terminal `Timeout` reveal any new behavior relative to earlier ~40-second attempts?
11. Is one request still consuming a roughly 20-second timeout after restart/setup?
12. Is there any evidence that the request reached the listener or handler? If not, keep those boundaries `NOT_PROVEN`.

## 5. Required read-only inspection scope

Inspect at minimum:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
```

Inspect the exact source at:

```text
047e2ce8e3c614495f8c05cbe01d0b75617d56a7
```

Inspect all code related to:

```text
WP04_PREFLIGHT_IMAGE
linuxFxVersion
container image retrieval
az webapp show
az webapp config container show
az webapp config show
--query
-o tsv
$LASTEXITCODE
empty/null validation
digest comparison
preflight stop conditions
```

No mutation.

## 6. Preflight-governance audit

Classify the preflight image check into exactly one:

```text
P1 — exact immutable digest is correctly retrieved and compared, but execution evidence was captured incorrectly
P2 — retrieval command can return blank while reporting command success
P3 — exact digest comparison is missing/incomplete
P4 — preflight result is advisory and does not block mutation
P5 — another concrete defect
```

If any of `P2`, `P3`, `P4`, or `P5` is proven, another Azure attempt is forbidden until remediation.

## 7. Timeout evidence classification

The clean terminal result proves:

```text
helper internal telemetry = COMPLETE
terminal class = Timeout
helper restoration = PASS
external T6 = NOT_INDICATED
```

It does **not** prove:

```text
exact deployed digest before mutation
request reached Azure front end
request reached container listener
handler executed
D3 artifact existed
Azure front-end timeout cause
listener bind failure
container recycle
```

Keep unproven boundaries unproven.

## 8. Timing comparison

Compare the current elapsed time:

```text
36.282 seconds
```

against prior attempts:

```text
~40.481 seconds hard-deadline initialize failure
prior one-request terminal Timeout behavior
```

Determine whether the evidence remains consistent with:

```text
restart/setup latency + one request timeout
```

but do not elevate consistency to causation.

Explicitly classify:

```text
TIMEOUT_CAUSE = PROVEN | NOT_PROVEN
```

Expected default absent new evidence:

```text
TIMEOUT_CAUSE = NOT_PROVEN
```

## 9. Candidate decisions

Select exactly one.

### D1 — P1_ONE_PATH_PREFLIGHT_IMAGE_PROOF_CORRECTION

Use if the helper can continue when exact image proof is blank or malformed.

Expected next step:

```text
Terra one-path helper remediation
```

Required behavior after remediation:

```text
non-empty exact linuxFxVersion/image identity
exact digest equality check
blank/malformed/mismatch => terminal preflight failure
Azure mutation count = 0
```

### D2 — P2_LOCAL_PREFLIGHT_VALIDATION_ONLY

Use if helper logic is already correct and the blank value arose only from invocation/output capture outside the committed helper.

Expected next step:

```text
Terra local preflight execution-observability authority
```

No Azure mutation.

### D3 — P3_AZURE_READ_ONLY_IMAGE_IDENTITY_PROOF

Use if helper code is correct but Azure CLI/API shape is uncertain and must be resolved read-only before code changes.

Expected next step:

```text
Terra read-only Azure image-identity investigation
```

No restart, no settings mutation.

### D4 — P4_PRECHECK_FIXED_THEN_TIMEOUT_RECONCILIATION

Use only if preflight defect is already fully explained and a separate timeout-specific Luna reconciliation can proceed without another Azure attempt.

Expected next step:

```text
Luna timeout path reconciliation
```

### D5 — P5_REPEAT_INITIALIZE

Use only if all are proven before execution:

```text
exact image preflight proof works
blank image cannot recur
helper blocks before mutation on blank/mismatch
no source remediation required
```

Absence of a T6 failure is not enough.

## 10. Decision preference

Prefer:

```text
D1 > D2 > D3 > D4 > D5
```

when the exact digest preflight gate itself is not enforceably proven.

Do not authorize another Azure initialize attempt while `WP04_PREFLIGHT_IMAGE` can be blank and execution can continue.

## 11. Required fail-closed preflight contract

If source remediation is selected, the future helper must enforce before any Azure mutation:

```text
PRECHECK_IMAGE_IDENTITY_PRESENT = true
PRECHECK_IMAGE_IDENTITY_IMMUTABLE_DIGEST = true
PRECHECK_IMAGE_DIGEST_MATCH =
sha256:892d246e6c5e0665edc26d4cbbfc187a4f0a7ffb03cd2dffcd802efc51978d1f
```

If any condition is false:

```text
temporary settings application = 0
restart = 0
terminal result = FAILURE
terminal class = PreflightImageIdentityFailure
```

or another Luna-approved fixed safe class.

Preflight must be fail-closed.

## 12. Retry/timing policy preservation

Do not change:

```text
retryable HTTP statuses = {404,503}
retryable transport classes = {NONE}
Timeout = terminal
evidence-poll total wall-clock budget <= 180 seconds
listener lifetime <= 180 seconds
```

This reconciliation does not authorize altering request timeout values.

## 13. Current Azure/source preservation

Under this Luna authority:

```text
repository mutation = 0
Git mutation = 0
Docker build = 0
GHCR publication = 0
Azure mutation = 0
restart = 0
logging mutation = 0
GitHub lifecycle mutation = 0
```

Preserve current normal runtime.

Do not infer or overwrite the deployed digest under this authority; only read it if needed.

## 14. Failed RunIds

Forbidden:

```text
initialize-dd7679f16cef4765a9c9d12f4b72bf76
initialize-3dab96f04cb84902b64df269ebae3459
initialize-50a6837f6cb649ed98481a3c26831961
initialize-01512ec42d444a68b315c7f7eecbcfa4
initialize-392a46ae50404c5182f5d76515d8b71e
initialize-94c5801cdc0b49e5933760c02313d486
```

The malformed historical identifier remains forbidden if encountered.

## 15. Required output

Return:

- exact code that populates `WP04_PREFLIGHT_IMAGE`;
- exact Azure CLI/API property path;
- PowerShell 5.1 behavior;
- empty/null handling;
- `$LASTEXITCODE` handling;
- whether exact digest equality is enforced;
- whether blank proof can pass into mutation;
- P1–P5 preflight classification;
- timeout evidence classification;
- `TIMEOUT_CAUSE`;
- D1–D5 comparison;
- exact selected decision;
- exact next authority type;
- exact mutation allowlist if source remediation is selected;
- whether new source commit is required;
- whether new runtime image is required;
- whether Azure mutation is required in next step;
- retry/timing preservation statement;
- failed RunId preservation statement;
- zero-mutation audit.

## 16. Terminal markers

Required:

`RELEASE 1.12 WP04 — TERMINAL TIMEOUT AND PREFLIGHT IMAGE-PROOF RECONCILIATION: PASS`

`RELEASE 1.12 WP04 — FRESH INITIALIZE PRECHECK ACCEPTANCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — HELPER TERMINAL RESULT: FAILURE`

`RELEASE 1.12 WP04 — HELPER TERMINAL CLASS: Timeout`

`RELEASE 1.12 WP04 — HELPER EXTERNAL T6 BOUNDARY: NOT_INDICATED`

`RELEASE 1.12 WP04 — PREFLIGHT IMAGE PROOF CLASSIFICATION: <P1|P2|P3|P4|P5>`

`RELEASE 1.12 WP04 — PREFLIGHT FAIL-CLOSED ENFORCEMENT: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — TIMEOUT CAUSE: <PROVEN|NOT_PROVEN>`

`RELEASE 1.12 WP04 — INITIALIZE D3 EVIDENCE: NOT_PROVEN`

`RELEASE 1.12 WP04 — RETRYABLE HTTP STATUS SET: {404,503}`

`RELEASE 1.12 WP04 — RETRYABLE TRANSPORT CLASS SET: {NONE}`

`RELEASE 1.12 WP04 — TIMEOUT TERMINAL POLICY: PRESERVED`

`RELEASE 1.12 WP04 — EVIDENCE POLL TOTAL WALL-CLOCK BOUND: 180_SECONDS_MAX`

`RELEASE 1.12 WP04 — FAILED INITIALIZE RUN REUSE: FORBIDDEN`

`RELEASE 1.12 WP04 — TERMINAL TIMEOUT AND PREFLIGHT IMAGE-PROOF RECONCILIATION MUTATION AUDIT: PASS`

`RELEASE 1.12 WP04 — PREFLIGHT/TIMEOUT RECONCILIATION DECISION: <D1|D2|D3|D4|D5>`

Then exactly one matching next-authority marker:

`RELEASE 1.12 WP04 — TERRA PREFLIGHT IMAGE-PROOF REMEDIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA LOCAL PREFLIGHT OBSERVABILITY AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA READ-ONLY AZURE IMAGE-IDENTITY INVESTIGATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — LUNA TIMEOUT PATH RECONCILIATION AUTHORITY: READY`

or

`RELEASE 1.12 WP04 — TERRA REPEAT INITIALIZE AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA TERMINAL TIMEOUT AND PREFLIGHT IMAGE-PROOF RECONCILIATION COMPLETE`

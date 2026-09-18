# Release 1.12 WP04 — Terra Freeze JavaScriptSerializer Validator & Full Supervised Rerun

**Selected execution model: GPT-5.6 Terra**

## Reconciled change

The validator-only final-ledger serializer has changed from the non-returning PowerShell `ConvertTo-Json` path to:

```text
System.Web.Script.Serialization.JavaScriptSerializer
```

The corrected flow must remain:

```text
same normalized ledger object
→ JavaScriptSerializer serialization
→ same-directory temporary ledger file
→ reopen/parse temporary JSON
→ canonical SERIALIZE
→ existing atomic publication
→ final-ledger reopen
```

This is a validator-only implementation change. It grants no acceptance credit until a complete fresh single-hash supervised SV01-SV36 run passes.

## Frozen invariants

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Windows PowerShell =
5.1.26100.9444
```

No runner, production, W5, or external-service mutation is authorized.

## Pre-freeze source verification

Before execution, prove from the current validator source that:

1. `ConvertTo-Json` is no longer the final-ledger serialization operation;
2. `JavaScriptSerializer` receives the same normalized ledger data contract;
3. each normalized SV record still contains exactly:
   - `CheckId`
   - `Requirement`
   - `Expected`
   - `Observed`
   - `Result`
   - `EvidenceReference`
   - `ValidationMethod`
4. no SV predicate, evidence-resolution rule, or acceptance requirement changed;
5. serialization still targets a same-directory temporary ledger file;
6. temporary JSON is reopened/parsed before canonical `SERIALIZE`;
7. atomic publication semantics are unchanged;
8. final published-ledger reopen semantics are unchanged;
9. canonical checkpoint semantics are unchanged;
10. diagnostic serializer markers remain diagnostic-only.

Retain sanitized source-location evidence.

## WinPS 5.1 compatibility gate

Because this project is bound to Windows PowerShell 5.1.26100.9444, prove the exact `JavaScriptSerializer` construction and invocation works in that runtime before structural acceptance.

Require parser errors = 0.

Do not assume PowerShell 7 or newer .NET APIs.

## Fresh identity

The serializer change modifies validator bytes.

Therefore:

```text
compute fresh ValidatorSHA256
retain exact validator bytes/hash
verify frozen RunnerSHA256
create fresh DisposableRoot
create fresh DurableRoot
create fresh supervisor root
```

All earlier validator hashes are historical for acceptance.

Cross-hash PASS carry-forward is forbidden.

## Diagnostic marker semantics

The existing diagnostic marker pair that previously bracketed `ConvertTo-Json` must now truthfully bracket the replacement serialization operation.

If marker names remain:

```text
CONVERTJSON_ENTER
CONVERTJSON_RETURN
```

retain an explicit metadata/source-location note that, for this validator hash, those historical diagnostic labels bracket the `JavaScriptSerializer.Serialize(...)` operation. Do not silently misrepresent the implementation.

Prefer correcting diagnostic-only marker names to serializer-neutral names if doing so is necessary for truthful retained evidence. Any such validator byte change must be included before freezing the fresh hash.

Canonical checkpoints remain unchanged regardless of diagnostic marker naming.

## Complete fresh supervised structural run

Run the frozen validator from SV01 under the detached artifact-first supervisor.

No acceptance evidence may be carried from a prior validator hash.

Use a finite bounded supervisor execution and record its exact timeout.

Recover durable:

```text
supervisor lifecycle
stdout
stderr
canonical checkpoint artifact
diagnostic marker artifact
failure diagnostic if any
final ledger
evidence manifest
evidence-reference resolution report
```

## Replacement serializer proof

The run must prove the replacement serializer operation both enters and returns.

Then prove:

```text
temporary write returns
temporary JSON reopen/parse returns
canonical SERIALIZE occurs
atomic publication returns
canonical PUBLISH occurs
final ledger reopen returns
canonical REOPEN occurs
```

If any operation non-returns, use the existing detached-supervisor evidence model to isolate it and continue validator-only remediation.

## Canonical finalization gate

Require exactly one durable canonical checkpoint each:

```text
PRE_CLEANUP
CLEANUP_COMPLETE
BUILD_LEDGER
SERIALIZE
PUBLISH
REOPEN
```

No diagnostic marker substitutes for a canonical checkpoint.

## SV01-SV36 gate

For one fresh validator hash require:

```text
SVRecordCount = 36
SVFailedCount = 0
EvidenceReferenceCount = 36
ResolvedEvidenceReferenceCount = 36
UnresolvedEvidenceReferenceCount = 0
RuntimePredicatePassClaims = 0
```

Every retained SV record must contain exactly the canonical seven fields.

Every evidence reference must resolve after disposable cleanup.

## Final ledger gate

Require:

```text
final sv01-sv36-ledger.json exists
atomic publication = PASS
final ledger independently reopens/parses = PASS
required ledger metadata present
GitDiffCheckOutput present
GitDiffCheckExitCode = 0
evidence manifest retained
resolution report retained
```

## Cleanup and identity gate

Require:

```text
DisposableRoot absent after cleanup
DurableRoot survives
runner survives
validator survives
checkpoint artifact survives
diagnostic marker artifact survives
final ledger survives
manifest survives
resolution report survives

RunnerHashAfterCleanup =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

ValidatorHashAfterCleanup = fresh ValidatorSHA256
```

## Governance invariants

Require:

```text
authority-introduced tracked repository mutations = 0
staged paths = 0
commits = 0
pushes = 0
GitHub mutations = 0
Azure mutations = 0
Docker/GHCR mutations = 0
production mutations = 0
external-service mutations = 0
W5 wrapper invoked = NO
W5 RunId allocated = NO
```

## Autonomous validator-only remediation

If another ordinary validator/finalizer defect occurs:

```text
diagnose retained evidence
→ narrow validator-only correction
→ WinPS 5.1 parser PASS
→ fresh ValidatorSHA256
→ fresh roots
→ full SV01-SV36 from SV01
```

Continue until complete structural PASS or mandatory governance BLOCKED.

Do not return merely because another validator-local defect is found.

## Mandatory blocker boundary

STOP only if the next required action is:

```text
runner modification
tracked production-source modification
Architecture-B/P01-P20/SV01-SV36/G01-G13 contract change
acceptance weakening
P19/P20 reinterpretation
cross-hash PASS composition
Azure/GitHub/Docker/GHCR/external-service mutation
Twelve Data secret configuration
W5 execution
W5 RunId allocation
```

## Required handoff

Return:

```text
RunnerSHA256
HistoricalValidatorSHA256
FreshJavaScriptSerializerValidatorSHA256
ParserResult
SerializerSourceProofPath
DiagnosticMarkerNaming/Mapping
DisposableRoot
DurableRoot
SupervisorRoot
SupervisorTimeoutSeconds
SupervisorTerminal
ChildPID
ExitObserved
ExitCode
TimeoutObserved
KillRequired
KillResult
SerializerEnterCount
SerializerReturnCount
DiagnosticMarkerSequence
CanonicalCheckpointCounts
SVRecordCount
SVFailedCount
EvidenceReferenceCount
ResolvedEvidenceReferenceCount
UnresolvedEvidenceReferenceCount
RuntimePredicatePassClaims
FinalLedgerPath
EvidenceManifestPath
ResolutionReportPath
GitDiffCheckExitCode
GitDiffCheckOutput
FinalLedgerReopenParseResult
RunnerHashAfterCleanup
ValidatorHashAfterCleanup
CorrectionsApplied
SubsequentValidatorSHA256s
FreshStructuralRunAttempts
FinalStructuralResult
GovernedW5WrapperInvoked
GovernedW5RunIdAllocated
ExactMutationAccounting
FirstBlockingDefect
FirstProhibitedRequiredAction
```

## PASS markers

On complete structural PASS:

```text
RELEASE 1.12 WP04 — TERRA JAVASCRIPTSERIALIZER ARCHITECTURE-B R1 STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — FINAL-LEDGER SERIALIZER REMEDIATION: PASS
RELEASE 1.12 WP04 — R1 RUNNER SHA256: 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
RELEASE 1.12 WP04 — R1 FINAL VALIDATOR SHA256: <FRESH_HASH>
RELEASE 1.12 WP04 — R1 RETAINED FINALIZATION CHECKPOINTS: 6/6
RELEASE 1.12 WP04 — R1 RETAINED CHECKPOINT CARDINALITY: EXACTLY_ONE_EACH
RELEASE 1.12 WP04 — R1 SV01-SV36 UNIQUE RECORDS: 36
RELEASE 1.12 WP04 — R1 SV01-SV36 FAILED RECORDS: 0
RELEASE 1.12 WP04 — R1 RESOLVED EVIDENCE REFERENCES: 36
RELEASE 1.12 WP04 — R1 UNRESOLVED EVIDENCE REFERENCES: 0
RELEASE 1.12 WP04 — R1 FINAL ATOMIC LEDGER PUBLICATION: PASS
RELEASE 1.12 WP04 — R1 FINAL LEDGER REOPEN/PARSE: PASS
RELEASE 1.12 WP04 — R1 READY FOR FRESH LUNA RECONCILIATION: YES
RELEASE 1.12 WP04 — GOVERNED W5 WRAPPER INVOKED: NO
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATED: NO
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

## Next gate

On complete structural PASS, STOP for fresh **GPT-5.6 Luna Architecture-B Final R1 Structural Reconciliation** bound to the final validator hash and frozen runner hash.

W5 remains prohibited until Luna accepts that structural gate.

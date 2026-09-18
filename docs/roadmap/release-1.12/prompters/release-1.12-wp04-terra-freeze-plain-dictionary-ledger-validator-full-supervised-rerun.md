# Release 1.12 WP04 — Terra Freeze Plain-Dictionary Ledger Validator & Full Supervised Rerun

**Selected execution model: GPT-5.6 Terra**

## Reconciled root cause

The serializer defect is now specifically identified as:

```text
VALIDATOR_DEFECT / POWERSHELL_WRAPPER_CYCLE
JavaScriptSerializer input contained PSCustomObject wrapper state
→ circular PSParameterizedProperty encountered
```

The validator-only correction materializes the final ledger into:

```text
plain ordered dictionary — top level
plain ordered dictionary — each of all 36 SV records
only required ledger values and metadata
```

The serializer must receive no `PSCustomObject`, `PSObject`, `PSParameterizedProperty`, or other live PowerShell wrapper graph.

This changes validator bytes. No acceptance evidence may carry forward from the prior validator hash.

## Frozen invariants

```text
RunnerSHA256 =
54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983

Windows PowerShell =
5.1.26100.9444
```

No runner, production, W5, or external-service mutation is authorized.

## Source/contract verification before freeze

Inspect the exact corrected source and retain sanitized proof that:

1. the top-level final ledger is a plain ordered dictionary;
2. all 36 serialized SV records are plain ordered dictionaries;
3. every SV record contains exactly:
   - `CheckId`
   - `Requirement`
   - `Expected`
   - `Observed`
   - `Result`
   - `EvidenceReference`
   - `ValidationMethod`;
4. all required ledger metadata is preserved;
5. no `PSCustomObject`, `PSObject`, `PSParameterizedProperty`, `ErrorRecord`, `InvocationInfo`, process, runspace, stream, or other live wrapper object is intentionally supplied to `JavaScriptSerializer`;
6. SV predicates and results are unchanged;
7. evidence-reference semantics are unchanged;
8. canonical checkpoint semantics are unchanged;
9. same-directory temporary-file semantics are unchanged;
10. temporary JSON reopen/parse remains before canonical `SERIALIZE`;
11. atomic final publication is unchanged;
12. final published-ledger reopen is unchanged;
13. diagnostic markers remain diagnostic-only.

Do not weaken the frozen contract to obtain serialization success.

## WinPS 5.1 and fresh identity gate

Require:

```text
Windows PowerShell version = 5.1.26100.9444
parser errors = 0
fresh ValidatorSHA256 computed
exact validator bytes/hash retained
RunnerSHA256 reverified exact
staged paths = 0
fresh DisposableRoot
fresh DurableRoot
fresh supervisor root
```

All previous validator hashes are historical for acceptance.

## Fresh supervised SV01-SV36 execution

Execute the complete structural validator from SV01 under the detached artifact-first supervisor.

No cross-hash PASS composition.

Use a finite bounded supervisor budget and record the exact bound.

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

## Serializer correction proof

The run must prove that `JavaScriptSerializer.Serialize(...)` both enters and returns.

If historical diagnostic marker names remain, explicitly map them to the replacement serializer operation.

Then prove in order:

```text
serializer ENTER
serializer RETURN
temporary-file write ENTER
temporary-file write RETURN
temporary-file parse ENTER
temporary-file parse RETURN
canonical SERIALIZE
publication ENTER
publication RETURN
canonical PUBLISH
final reopen ENTER
final reopen RETURN
canonical REOPEN
```

If another ordinary validator-local defect appears, diagnose and remediate under this authority.

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

Diagnostic markers do not substitute for canonical checkpoints.

## SV01-SV36 acceptance

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

## Serialized ledger shape verification

After publication/reopen, inspect the parsed final ledger and prove:

```text
36 SV records retained
seven canonical fields retained per SV record
required top-level metadata retained
no missing acceptance fields
no additional wrapper-derived fields
no serialized PowerShell runtime-object metadata
```

This verification is about the resulting JSON contract, not the in-memory implementation type.

## Final ledger acceptance

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

No PASS evidence may be composed across hashes.

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
FreshPlainDictionaryValidatorSHA256
ParserResult
LedgerMaterializationProofPath
SerializerInputShapeProof
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
SerializedLedgerShapeVerification
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
RELEASE 1.12 WP04 — TERRA PLAIN-DICTIONARY ARCHITECTURE-B R1 STRUCTURAL VALIDATION: PASS
RELEASE 1.12 WP04 — POWERSHELL-WRAPPER SERIALIZER DEFECT REMEDIATION: PASS
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

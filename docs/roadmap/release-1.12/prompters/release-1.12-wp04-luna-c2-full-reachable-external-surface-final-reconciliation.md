# Release 1.12 WP04 — Luna C2 Full Reachable External-Surface Final Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority

Perform the final independent read-only reconciliation of the remediated C2 full reachable external-surface structural contract. Do not mutate runner, harness, validator, production/tracked source, repository, GitHub, Azure, Docker/GHCR, or external services. Do not allocate a W5 RunId or invoke the governed wrapper.

## Exact bound tuple

```text
RunnerSHA256 = 54449AFC3987D6EAE641A43E7ACE9BF9D63501D12854849025EDE99F10901983
HarnessSHA256 = F80C9F8C9D4EB0570AEDE97F5A77B46DF4B34B6C15F20CFC16A3BC7183381915
ValidatorSHA256 = 6A65A8BD18E80592C7364624B3E0F71ED7457193C9AD3602A10AC2A2AE1531A0
FinalDurableRoot = C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\d0f09b1de28f46f195a1726cc820948b
FinalLedger = sv01-sv36-ledger.json
```

Superseded root `C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\architecture-b\83150fb419454aa1b89ab86e0a3f186a` is historical only. Do not compose evidence across tuples.

## R01–R14

**R01 — Identities/root.** Recompute all three hashes and prove every acceptance artifact belongs to the exact final tuple/root.

**R02 — PowerShell/parser.** Require Windows PowerShell `5.1.26100.9444` and parser errors `0/0/0`.

**R03 — Production reachable surface.** Independently inspect the production wrapper across nominal, failure, finally, and restoration paths. Require complete exact inventory of:
- git: `rev-parse HEAD`; `merge-base --is-ancestor ...`; `ls-files --error-unmatch -- <wrapper>`; `diff --quiet HEAD -- <wrapper>`.
- az: image `webapp show`; state `webapp show`; app-settings list; `webapp log download --log-file <exact evidence-root>/raw-app-service-logs.zip`.
- helper Deferred: ResourceGroup, WebAppName, Phase initialize, LifecycleAction None, RestorationMode Deferred.
- helper RestoreOnly: ResourceGroup, WebAppName, RestorationMode RestoreOnly, RestorationDescriptor.
Verify local archive/filesystem/JSON/callback operations add no external process surface. Any omitted reachable external call = FAIL.

**R04 — Surface equality.** Require `ProductionReachableExternalSurface == HarnessGovernedInterceptionSurface`; subset or unexplained superset = FAIL. Require `C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS`.

**R05 — Deferred.** Verify exact Deferred shape, absent RestorationDescriptor, and non-empty sanitized opaque descriptor generation without recreating lifecycle policy.

**R06 — RestoreOnly.** Verify exact preceding descriptor, same synthetic identity/RG/app, one-time use, and rejection of missing/empty/altered/arbitrary/cross-attempt/mismatched/replayed descriptors and unsupported shapes.

**R07 — Finally-path scope.** Require copy-equivalent child scope at SandboxRoot; relative helper resolves to sandbox shadow; Deferred emits descriptor; RestoreOnly consumes it; workspace helper not selected; location restored. Direct harness-only invocation is insufficient.

**R08 — Exact git/az.** Require exact normalized contracts, no broad wildcards. Dynamic log path must normalize to the exact governed evidence-root destination; reject traversal, alternate roots, workspace paths, arbitrary paths, and extras.

**R09 — F01-F18.** Independently reconcile all F01-F18 and require `ALL_PASS`, covering inventory completeness/equality, both helper shapes, descriptor correlation/replay rejection, finally path, exact git/az, wildcard absence, no omitted surface, and cleanup/location restoration.

**R10 — I01-I09.** Require `ALL_PASS`, child/copy-equivalent executable proof, `C2_EXECUTABLE_INTERCEPTION_BOUNDARY = PASS`, and real git/az/workspace-helper escape blocked.

**R11 — SV01-SV36.** Require exactly 36 unique records, 0 failures, 36/36 resolved evidence references, 0 runtime P01-P20 PASS claims. Each record fields exactly: CheckId, Requirement, Expected, Observed, Result, EvidenceReference, ValidationMethod.

**R12 — Durable summary round trip.** This is mandatory. Prove the actual validated predicate flows:
```text
in-memory C2_FULL_REACHABLE_EXTERNAL_SURFACE = PASS
→ BUILD_LEDGER
→ C2FullReachableExternalSurface = PASS
→ SERIALIZE
→ PUBLISH
→ REOPEN
→ C2FullReachableExternalSurface = PASS
```
The reopened `sv01-sv36-ledger.json` must explicitly contain both:
```text
C2FullReachableExternalSurface = PASS
C2ExecutableInterceptionBoundary = PASS
```
Reject hard-coded PASS disconnected from validation, or any missing/unresolved/omitted field.

**R13 — Pre-RunId ordering/runtime exclusion.** Require all identity/G01-G11/interception/surface-equality/helper/git/az/negative/escape gates and both summary predicates before future RunId allocation and wrapper invocation. During this reconciliation require W5 RunIds `0`, wrapper invocations `0`, real external calls `0`, runtime P01-P20 claims `0`.

**R14 — Finalization/repo invariants.** Require exactly once and ordered: PRE_CLEANUP, CLEANUP_COMPLETE, BUILD_LEDGER, SERIALIZE, PUBLISH, REOPEN. Evidence remains resolvable. Only the two pre-existing WP04 tracked modifications may remain; authority-introduced tracked modifications `0`, staged `0`, `git diff --check` `0`, GitHub/Azure/Docker/GHCR/W5 mutations `0`.

Classify each R01-R14 as PASS, FAIL, or NOT_PROVEN. Any FAIL/NOT_PROVEN blocks W5. Do not repair artifacts.

## PASS markers

Only if all material gates pass:

```text
RELEASE 1.12 WP04 — LUNA C2 FULL REACHABLE EXTERNAL-SURFACE FINAL RECONCILIATION: PASS
RELEASE 1.12 WP04 — C2 FINAL THREE-ARTIFACT CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — C2 DEFERRED/RESTOREONLY HELPER CONTRACT: ACCEPTED
RELEASE 1.12 WP04 — C2 FINALLY-PATH HELPER INTERCEPTION: ACCEPTED
RELEASE 1.12 WP04 — C2 FULL-SURFACE PROBES F01-F18: ACCEPTED
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE: ACCEPTED
RELEASE 1.12 WP04 — C2 FULL REACHABLE EXTERNAL SURFACE LEDGER SUMMARY: ACCEPTED
RELEASE 1.12 WP04 — C2 EXECUTABLE INTERCEPTION BOUNDARY: ACCEPTED
RELEASE 1.12 WP04 — C2 INTERCEPTION PROBES I01-I09: ACCEPTED
RELEASE 1.12 WP04 — GOVERNED W5 EXECUTION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — GOVERNED W5 RUNID ALLOCATION: AUTHORIZED_NEXT
RELEASE 1.12 WP04 — W5 GOVERNED ACCEPTANCE: NOT_GRANTED
RELEASE 1.12 WP04 — W6/W7/W8: NOT_RUN
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: UNRESOLVED
RELEASE 1.12 WP04 — WP04 #263: OPEN
```

Then STOP. W5 requires a separate GPT-5.6 Terra authority.

## Failure handoff

On FAIL/NOT_PROVEN identify the first failing gate, exact evidence, defect owner, whether harness-only/validator-only/harness+validator remediation suffices, and whether runner/production/tracked change is required. No W5 authorization.

## Required handoff

Return the observed tuple/root/ledger; PowerShell/parser results; production and harness surface inventories/equality; Deferred/RestoreOnly and descriptor results; finally-path/escape results; exact git/az results; F01-F18 aggregate; I01-I09 aggregate; both C2 summary predicates; durable summary values at in-memory/build/serialized/published/reopened stages; SV counts and evidence resolution; runtime exclusion counts; checkpoint counts; superseded-root isolation; repository/mutation invariants; R01-R14 summary; final reconciliation result; first failing predicate if any; defect classification; next authorized action.

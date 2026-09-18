# Release 1.12 WP04 — Luna Wrapper-Hash Record Reconciliation

**Selected execution model: GPT-5.6 Luna**

## Authority type

READ-ONLY narrow contract/evidence reconciliation.

No Git, GitHub, Azure, Docker/GHCR, source, evidence-package, PR, merge, issue, Project, or milestone mutation is authorized.

```text
GPT-5.6 Luna = selected contract/evidence/governance authority
GPT-5.6 Terra = implementation/publication authority; not selected
GPT-5.6 Sol = supporting analysis only
```

---

## 1. Mission

Resolve one publication blocker: the accepted wrapper SHA-256 record differs by one character from the SHA-256 independently recomputed from the exact accepted committed wrapper bytes.

Do not reopen WP04 substantive behavior unless this discrepancy proves actual byte/provenance corruption.

Determine whether this is:

```text
A. RECORDING_TRANSCRIPTION_ERROR
B. COMMITTED_BYTE_IDENTITY_CONTRADICTION
C. EVIDENCE_PACKAGE_CORRUPTION
D. UNRESOLVED
```

If A is proven, establish the actual committed SHA as the canonical wrapper identity for publication without changing source bytes.

---

## 2. Accepted commit and path

Binding accepted commit:

```text
e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
```

Wrapper path:

```text
eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/initialize-qualification.ps1
```

Publication preflight reports:

```text
HEAD=accepted commit
live release remote=accepted commit
tracked modifications=0
staged paths=0
working-tree wrapper bytes == Git index == accepted commit bytes
```

Read-only independently verify all of these.

---

## 3. Conflicting records

Previously recorded/authority hash:

```text
2DF9173EAE15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
```

Publication-preflight independently computed actual hash:

```text
2DF9173EAEF15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
```

The difference is:

```text
recorded: ...EAE15D3...
actual:   ...EAEF15D3...
```

Do not choose either value by assumption.

---

## 4. Independent byte/hash proof

Using read-only operations, obtain the wrapper bytes from at least these identities:

```text
A. working-tree file
B. Git index
C. git show e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff:<wrapper-path>
```

Compute SHA-256 independently for each byte stream.

Prefer two independent hashing mechanisms where practical under the existing environment, e.g.:

```text
Get-FileHash -Algorithm SHA256
and
System.Security.Cryptography.SHA256
```

For `git show`, hash the exact emitted blob bytes without newline/text normalization. A temporary binary file is acceptable if untracked/disposable and deleted afterward.

Return:

```text
WorkingTreeSHA256
IndexBlobSHA256
AcceptedCommitBlobSHA256
IndependentSecondMethodSHA256
```

Require exact four-way equality before classifying this as a record-only defect.

Also record Git blob object identity if useful:

```text
AcceptedCommitBlobObjectId
IndexBlobObjectId
```

---

## 5. Commit/source identity proof

Read-only verify:

```text
HEAD=e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
live remote release tip=e163c2a7a4b0301df9d4320d7f7eda1ff3f30dff
working tree tracked clean
staged paths=0
git diff --check=PASS
```

Require the wrapper to have no post-acceptance byte mutation.

Helper expected/actual identity remains:

```text
7916106C11BEF92251EE49C7E0C0650744BE728CD42EE4071BF9D33E013F0541
```

Verify read-only as a non-contradiction control.

---

## 6. Durable package reconciliation

Authoritative accepted package:

```text
C:\Users\sabsf\AppData\Local\AIQuantTradingResearch\wp04\target-validation-final\890ac450f6324238b3dcc21c03bf677e
```

The package previously passed:

```text
ArtifactCount=28
Missing=0
HashMismatchCount=0
UnresolvedReferences=0
D01-D19=19/19
DurableReopen=PASS
```

Inspect how the wrapper identity is represented.

Distinguish:

```text
1. package hashes the wrapper artifact bytes and those bytes/hash agree with actual commit;
2. package contains only a metadata field carrying the mistyped wrapper SHA;
3. package's own artifact hash table uses the mistyped SHA as if it were a content hash;
4. package contains contradictory wrapper byte identities.
```

This distinction is decisive.

Do not mutate the package.

Return:

```text
ManifestRecordedWrapperSHA256
ManifestWrapperArtifactPresent
ManifestWrapperArtifactSHA256
ManifestInternalHashValidationResult
ManifestWrapperIdentityRole
```

---

## 7. Acceptance-history reconciliation

Review the final Terra/Luna acceptance chain only for effects of this hash discrepancy.

The runtime/evidence acceptance included:
- exact accepted commit identity;
- clean committed helper/source identity;
- immutable image identity;
- fresh initialize/reopen exact RunIds;
- D01-D19 19/19;
- D14 restoration;
- D18 mutation accounting;
- D19 durable reopen;
- zero source mutation during final evidence cycle.

Determine whether any substantive acceptance predicate depended on comparing wrapper bytes against the **mistyped literal** rather than against the actual committed wrapper bytes/commit provenance.

Return:

```text
SubstantiveAcceptanceDependedOnMistypedLiteral=true|false
RuntimeExecutionUsedActualCommittedWrapperBytes=true|false
EvidenceAttributableToAcceptedCommit=true|false
```

If substantive acceptance depended on the wrong literal in a way that could admit different bytes, do NOT classify as a harmless transcription error.

---

## 8. Decision rule — recording error

Classify:

```text
WRAPPER_HASH_RECORDING_ERROR
```

only if ALL are true:

```text
WorkingTreeSHA256
== IndexBlobSHA256
== AcceptedCommitBlobSHA256
== IndependentSecondMethodSHA256
== 2DF9173EAEF15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A

accepted commit exact and live remote exact
tracked/staged clean
helper identity non-contradicted
runtime execution used the accepted committed wrapper bytes
D01-D19 evidence remains attributable to accepted commit
no substantive predicate depended unsafely on the mistyped literal
durable package has no actual byte corruption
```

Then establish:

```text
CanonicalAcceptedWrapperSHA256=
2DF9173EAEF15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A

SupersededMistypedWrapperSHA256=
2DF9173EAE15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
```

The correction is a governance/evidence identity correction only:

```text
SourceByteChangeRequired=false
RuntimeRerunRequired=false
ImageRebuildRequired=false
SubstantiveAcceptanceRevoked=false
```

---

## 9. Durable-manifest consequence

If the durable manifest itself contains the mistyped metadata value but its retained wrapper bytes/other provenance prove the actual committed identity, determine whether publication may proceed using a Luna correction record without rewriting the already-accepted immutable package.

Preferred if safe:

```text
OriginalPackage=IMMUTABLE
CorrectionRecord=AUTHORITATIVE_SUPPLEMENT
```

The correction record must identify:
- original package root;
- exact metadata field being superseded;
- mistyped value;
- actual independently proven value;
- accepted commit;
- proof hashes/methods;
- no source/runtime mutation;
- Luna decision.

If such a supplement is required, authorize Terra to create it as a new local durable reconciliation artifact before publication; do not alter the original package.

If repository policy requires tracked publication of the correction, report that as a separate boundary rather than silently committing it.

---

## 10. Decision rule — actual contradiction

If any independent committed-byte hash differs, or the durable package contains actual conflicting wrapper bytes, or substantive acceptance depended unsafely on the wrong literal, return:

```text
Decision=ACTUAL_IDENTITY_CONTRADICTION
PublicationAuthorized=false
```

Provide one exhaustive defect matrix and the narrow next authority required.

Do not authorize PR creation.

---

## 11. PASS markers

If this is proven to be a record-only transcription defect, emit exactly:

```text
RELEASE 1.12 WP04 — WRAPPER HASH RECONCILIATION: PASS
RELEASE 1.12 WP04 — WRAPPER COMMITTED BYTE IDENTITY: PASS
RELEASE 1.12 WP04 — CANONICAL WRAPPER SHA256: 2DF9173EAEF15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
RELEASE 1.12 WP04 — SUPERSEDED HASH RECORD: 2DF9173EAE15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
RELEASE 1.12 WP04 — SOURCE BYTE CHANGE: NOT_REQUIRED
RELEASE 1.12 WP04 — RUNTIME RERUN: NOT_REQUIRED
RELEASE 1.12 WP04 — SUBSTANTIVE ACCEPTANCE: PRESERVED
RELEASE 1.12 WP04 — PUBLICATION BLOCKER: CLEARED
```

Then:

```text
Decision=WRAPPER_HASH_RECORDING_ERROR
CanonicalAcceptedWrapperSHA256=2DF9173EAEF15D3E966A666F57AF39598A6CA563B67734EF519D35062E5DF70A
PublicationAuthorized=true
NextAuthorizedAction=GPT-5.6 Terra resume final WP04 publication/merge/lifecycle using the corrected canonical wrapper hash, with immutable-package correction supplement if required
```

---

## 12. Required handoff

Return:

```text
SelectedModel
AcceptedCommit
LiveRemoteTip
TrackedModifications
StagedPaths
GitDiffCheck

WorkingTreeSHA256
IndexBlobSHA256
AcceptedCommitBlobSHA256
IndependentSecondMethodSHA256
AcceptedCommitBlobObjectId
IndexBlobObjectId

HelperSHA256
HelperIdentityResult

DurableRoot
ManifestRecordedWrapperSHA256
ManifestWrapperArtifactPresent
ManifestWrapperArtifactSHA256
ManifestInternalHashValidationResult
ManifestWrapperIdentityRole

RuntimeExecutionUsedActualCommittedWrapperBytes
EvidenceAttributableToAcceptedCommit
SubstantiveAcceptanceDependedOnMistypedLiteral

CanonicalAcceptedWrapperSHA256
SupersededMistypedWrapperSHA256
SourceByteChangeRequired
RuntimeRerunRequired
ImageRebuildRequired
SubstantiveAcceptanceRevoked

CorrectionSupplementRequired
OriginalPackageMutationRequired
TrackedCorrectionPublicationRequired

Decision
PublicationAuthorized
NextAuthorizedAction
```

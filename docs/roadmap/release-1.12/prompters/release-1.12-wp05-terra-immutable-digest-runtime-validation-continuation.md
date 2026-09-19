# Release 1.12 WP05 --- Terra Immutable Digest Runtime Validation Continuation

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract/policy/architecture/governance/final acceptance
GPT-5.6 Terra = selected runtime validation and in-scope convergence authority
GPT-5.6 Sol = supporting analysis only; non-authoritative
```

## Confirmed deployment identity

The West US 2 Web App is configured to use exactly:

``` text
DOCKER|ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
```

Canonical candidate digest:

``` text
sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
```

Do not rebuild or republish merely to reconfirm identity.

## Mission

Continue the existing WP05 Terra convergence from the immutable-digest
deployment checkpoint.

Verify that Azure is not only configured for the digest but is actually
running the corrected candidate successfully, then complete all
remaining affected WP05 runtime/bounded-provider gates.

Do not reopen already-passed planning/source contracts without
contradictory evidence.

## Required runtime gates

Verify exhaustively:

``` text
WestUS2AppServiceState=Running
WestUS2Availability=Normal
HttpsResult=200

ConfiguredImageDigest=sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
ObservedRunningImageDigest=sha256:6fd55ef1d407a47ba8e33e19d083e76e7c8a1cac7f31d9d2090b31838b19ccb1
ImageIdentityMatch=true

TwelveData__ApiKey Present=true
TwelveData__ApiKey NonEmpty=true
SecretValueDisclosed=false

TwelveData__RequestTimeoutSeconds configured consistently with accepted implementation
ProviderDeadlineSeconds=10
ProviderTimeoutRange=1..30
AutomaticRetryCount=0
MaximumRequestAttempts=1
```

Use only safe secret-presence metadata. Never retrieve/display the
API-key value.

## Bounded-provider validation

Establish that the deployed candidate preserves the Luna-accepted
contract:

``` text
finite authoritative linked deadline
caller cancellation distinct from provider deadline
zero retries
one request attempt
no synchronous provider blocking
deterministic failure isolation
```

Do not intentionally consume excessive Twelve Data quota to manufacture
negative live cases already proven deterministically by tests.

Use runtime evidence for target/deployment truth and deterministic test
evidence for failure modes where live destructive/quota-consuming probes
add no value.

## Infrastructure-test gate

The previous continuation reported that the full Infrastructure assembly
had not produced a final result.

Before final WP05 PASS, require an authoritative disposition:

``` text
FullInfrastructureTestsResult=PASS
```

OR, if baseline-equivalent pre-existing harness behavior was proven
under the previous continuation, report the exact accepted
baseline-equivalence evidence and governance basis.

Do not silently omit this gate.

If it remains unresolved, WP05 cannot reach final runtime-ready
acceptance.

## Convergence

For any correctable WP05 defect within the accepted contract:

``` text
collect ALL failures
→ fix ALL in-scope failures locally
→ rerun complete affected local validation
→ new commit/PR if bytes change
→ new immutable image digest if source/image bytes change
→ deploy
→ rerun complete affected runtime validation
```

Never reuse digest `6fd55e...` as final evidence after a
source/image-changing correction.

Do not return after each correctable defect.

Stop only for a genuine governance/security/user-controlled boundary.

## No lifecycle mutation

Do not close issue #264 or mark Project #2 WP05 Done in this authority.

Do not claim WP06 or WP07 acceptance.

## Required output

``` text
SelectedModel
ConfiguredImageReference
ConfiguredImageDigest
ObservedRunningImageDigest
ImageIdentityMatch

WestUS2ResourceGroup
WestUS2WebApp
WestUS2State
WestUS2Availability
WestUS2HttpsResult

TwelveDataSettingPresent
TwelveDataSettingNonEmpty
SecretValueDisclosed
RequestTimeoutSettingPresent
ConfiguredRequestTimeoutSeconds

ProviderDeadlineResult
CallerCancellationIsolationResult
ZeroRetryResult
MaximumRequestAttempts
SynchronousBlockingResult
FailureIsolationResult
BoundedAutomationValidationResult

ReleaseBuildResult
DomainTestsResult
ApplicationTestsResult
ArchitectureTestsResult
FocusedTwelveDataInfrastructureTestsResult
FullInfrastructureTestsResult
InfrastructureGateDisposition
GitDiffCheckResult

WP03HistoricalEvidencePreserved
WP04HistoricalEvidencePreserved
WP06AcceptanceClaimed
WP07AcceptanceClaimed

SourceMutationCount
GitCommitCount
GitPushCount
PRCreateCount
PRMergeCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
AzureAppSettingMutationCount
PlatformTriggeredRestartCount
ExplicitRestartCount
AzureResourceCreateCount
AzureResourceDeleteCount
PaidResourceMutationCount
SecretDisclosureCount
IssueMutationCount
ProjectMutationCount
MilestoneMutationCount

WP05ImplementationResult
WP05RuntimeValidationResult
WP05Result
BoundaryBlocker
NextAuthorizedAction
```

## PASS gate

Only after the immutable candidate is observed running and every
affected local/runtime gate is resolved:

``` text
RELEASE 1.12 WP05 — IMMUTABLE IMAGE IDENTITY: PASS
RELEASE 1.12 WP05 — WEST US 2 DEPLOYMENT: PASS
RELEASE 1.12 WP05 — PROVIDER DEADLINE: PASS
RELEASE 1.12 WP05 — ZERO-RETRY / ONE-ATTEMPT BOUND: PASS
RELEASE 1.12 WP05 — FAILURE ISOLATION: PASS
RELEASE 1.12 WP05 — BOUNDED AUTOMATION: PASS
RELEASE 1.12 WP05 — RUNTIME VALIDATION: PASS
RELEASE 1.12 WP05 — SECRET HYGIENE: PASS
RELEASE 1.12 WP05 — $0 ARCHITECTURE BOUNDARY: PASS

WP05ImplementationResult=PASS
WP05RuntimeValidationResult=PASS
WP05Result=READY_FOR_LUNA_ACCEPTANCE
BoundaryBlocker=NONE
NextAuthorizedAction=GPT-5.6 Luna performs final read-only WP05 substantive acceptance reconciliation
```

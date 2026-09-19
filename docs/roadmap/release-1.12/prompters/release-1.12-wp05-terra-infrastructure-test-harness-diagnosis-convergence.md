# Release 1.12 WP05 --- Terra Infrastructure Test-Harness Diagnosis and Convergence Continuation

**Selected execution model: GPT-5.6 Terra**

## Authority roles

``` text
GPT-5.6 Luna = contract/policy/architecture/governance/acceptance
GPT-5.6 Terra = selected implementation and validation-convergence authority
GPT-5.6 Sol = supporting analysis only; non-authoritative
```

## Entry state

The WP05 bounded-provider correction exists locally and is intentionally
unpublished/undeployed.

Established local results:

``` text
ReleaseBuild=PASS (0 warnings, 0 errors)
DomainTests=PASS (11)
ApplicationTests=PASS (136)
ArchitectureTests=PASS (27)
FocusedTwelveDataInfrastructureTests=PASS (58)
ProductionProviderBlockingScan=PASS
GitDiffCheck=PASS

FullInfrastructureTestAssembly=NO_FINAL_RESULT
PublicationPerformed=false
ImageBuiltOrPublished=false
AzureMutationPerformed=false
```

Implemented contract includes: - async provider path through
`IObservationSource`, research use case, worker; - no synchronous
blocking on production Twelve Data path; - authoritative linked provider
deadline = 10 seconds; - `TwelveData:RequestTimeoutSeconds`, valid range
1--30 seconds; - `HttpClient.Timeout = Infinite`; - caller cancellation
distinguished from provider deadline; - zero retries / one request
attempt; - deterministic deadline/timeout-bound tests; - configuration
documentation update.

These implementation choices are inside the prior Luna contract. Do not
reopen them unless the full-suite evidence proves a defect.

## Mission

Determine why the **full Infrastructure test assembly does not terminate
within the available command window and leaves test-host processes
active**, then converge the complete required test gate to an
authoritative PASS or FAIL.

This is a continuation of the existing Terra source-convergence
authority. Do not publish, build an image, or mutate Azure until the
complete required local test gate passes.

## Diagnostic rule

Do NOT treat command-window expiration as a test failure.

Classify the behavior as one or more of:

``` text
TEST_SUITE_LEGITIMATELY_LONG_RUNNING
TEST_DEADLOCK
ASYNC_OVER_SYNC_DEADLOCK
UNCANCELLED_ASYNC_OPERATION
HTTP_HANDLER_OR_STREAM_NOT_COMPLETING
BACKGROUND_TASK_LEAK
TEST_HOST_PROCESS_LEAK
FIXTURE_LIFETIME_DEFECT
PARALLELIZATION_INTERFERENCE
EXTERNAL_RESOURCE_WAIT
WP05_REGRESSION
PREEXISTING_UNRELATED_TEST_DEFECT
HARNESS_COMMAND_TIMEOUT_TOO_SHORT
UNKNOWN
```

Evaluate all relevant classes before changing code.

## Baseline discrimination

First determine whether the non-termination is caused by the WP05
changes.

Use the repository's legitimate pre-change/base commit or equivalent
clean baseline and run the same Infrastructure test assembly under the
same harness constraints where feasible.

Do not mutate the working candidate to establish the baseline. Use a
safe separate worktree or equivalent isolated checkout if necessary.

Return:

``` text
CandidateFullInfrastructureBehavior
BaselineFullInfrastructureBehavior
WP05CausalityClassification
```

If both baseline and candidate exhibit the same long-running behavior,
do not "fix" unrelated production/test code under WP05 merely to make
the harness terminate.

## Exhaustive test localization

Run the Infrastructure assembly with sufficient diagnostic visibility to
identify: - last started/completed test(s); - tests that never report
completion; - test-host PID(s); - whether the test process is
CPU-active, idle, waiting, or blocked; - open child processes where
observable; - assembly/test duration; - whether parallel execution
changes the behavior.

Prefer deterministic localization techniques: - list tests; -
divide/filter test groups; - binary-search filters where useful; -
diagnostic verbosity/loggers; - blame/hang diagnostics supported by the
repository's installed .NET SDK/test platform; - bounded
per-test/per-group execution.

Do not repeatedly run the entire hanging suite without gaining new
diagnostic information.

## Process hygiene

Before each diagnostic cycle, identify only test-host processes
attributable to the current run.

After a timed-out/hung diagnostic run, terminate only those attributable
test processes before the next cycle.

Do not kill unrelated `dotnet`/IDE/build processes.

Record process IDs and cleanup counts without leaking sensitive
environment information.

## Correction scope

If the hang/non-termination is caused by the WP05 implementation and is
correctable within the already accepted bounded-provider contract, fix
it.

Examples include: - test fake never honoring cancellation; - leaked
task/response/stream; - cancellation token not propagated; - fixture
disposal issue introduced by WP05; - async test incorrectly blocking; -
provider deadline test leaving work alive.

For every byte-changing correction:

``` text
invalidate prior candidate hashes/evidence
→ rerun build
→ rerun focused WP05 tests
→ rerun Domain/Application/Architecture gates
→ rerun complete Infrastructure assembly
→ rerun blocking scan
→ rerun git diff --check
```

Continue until all required gates PASS.

If the issue is pre-existing/unrelated, do not modify unrelated code.
Establish authoritative baseline equivalence and determine whether
project governance permits the full-suite gate to be satisfied by the
canonical baseline behavior. If not, stop with the exact governance
blocker.

## Harness timeout

If the only defect is that the command wrapper/window is shorter than
the legitimate suite duration, increase the **local validation harness
timeout only** to a bounded value sufficient to obtain the real test
result.

This is not permission to weaken application/provider deadlines.

Do not change production timeout values merely to accommodate the test
harness.

## No premature publication

Until the complete required local gate is resolved:

``` text
GitCommitAllowed=false
GitPushAllowed=false
PRAllowed=false
DockerBuildAllowed=false
GhcrPublicationAllowed=false
AzureDeploymentAllowed=false
```

Once the complete local gate reaches PASS, resume the original Terra
authority **without another handoff**:

``` text
branch/commit/push
→ PR/merge
→ new immutable candidate image
→ GHCR publication
→ West US 2 deployment
→ affected WP05 runtime validation
→ bounded convergence until PASS
```

The user-controlled `TwelveData__ApiKey` must remain undisclosed.

## Required output

Return:

``` text
SelectedModel
CandidateHEAD
BaselineCommit
CandidateFullInfrastructureBehavior
BaselineFullInfrastructureBehavior
WP05CausalityClassification

InfrastructureTestCount
InfrastructureCompletedTestCount
InfrastructureNonCompletedTests
LastObservedTests
TestHostProcessCount
AttributedProcessesCleaned
DiagnosticClassification
DiagnosticEvidence

HarnessTimeoutBefore
HarnessTimeoutAfter
HarnessTimeoutChanged

WP05CorrectionRequired
WP05CorrectionChangedPaths
UnrelatedCorrectionRequired
GovernanceBoundaryEncountered

ReleaseBuildResult
DomainTestsResult
ApplicationTestsResult
ArchitectureTestsResult
FocusedTwelveDataInfrastructureTestsResult
FullInfrastructureTestsResult
ProductionProviderBlockingScanResult
GitDiffCheckResult

GitCommitCount
GitPushCount
PRCreateCount
PRMergeCount
DockerBuildCount
GhcrPublicationCount
ImageDeploymentMutationCount
AzureMutationCount
SecretDisclosureCount

WP05LocalValidationResult
WP05ImplementationResult
WP05RuntimeValidationResult
WP05Result
BoundaryBlocker
NextAuthorizedAction
```

## PASS continuation

If the complete Infrastructure assembly obtains PASS and all other local
gates remain PASS, immediately continue the already authorized
publication/image/deployment/runtime sequence from the parent Terra
authority.

Do not return merely to report that the harness issue was fixed.

Final desired state remains:

``` text
RELEASE 1.12 WP05 — BOUNDED PROVIDER IMPLEMENTATION: PASS
RELEASE 1.12 WP05 — ASYNC/CANCELLATION PROPAGATION: PASS
RELEASE 1.12 WP05 — PROVIDER DEADLINE: PASS
RELEASE 1.12 WP05 — ZERO-RETRY / ONE-ATTEMPT BOUND: PASS
RELEASE 1.12 WP05 — FAILURE ISOLATION: PASS
RELEASE 1.12 WP05 — NEW CANDIDATE IMAGE: PASS
RELEASE 1.12 WP05 — WEST US 2 DEPLOYMENT: PASS
RELEASE 1.12 WP05 — BOUNDED AUTOMATION: PASS
RELEASE 1.12 WP05 — RUNTIME VALIDATION: PASS

WP05LocalValidationResult=PASS
WP05ImplementationResult=PASS
WP05RuntimeValidationResult=PASS
WP05Result=READY_FOR_LUNA_ACCEPTANCE
BoundaryBlocker=NONE
NextAuthorizedAction=GPT-5.6 Luna performs final read-only WP05 substantive acceptance reconciliation
```

If the full suite produces a genuine WP05-related FAIL, correct all
in-scope failures and repeat the complete cycle. If it exposes a true
unrelated governance boundary, stop with the complete evidence matrix.

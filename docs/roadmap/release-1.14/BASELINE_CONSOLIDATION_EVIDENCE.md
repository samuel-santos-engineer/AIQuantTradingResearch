# Release 1.14 baseline consolidation evidence

Status: local consolidation for owner review; no deployment or activation. Recorded 2026-10-15.

## Scope and provenance

Integration starts from freshly cloned main `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8` (PR #331). Protected original remains on `draft/release-1.14-wp07-partial-public-disabled` at `67893f51931ee183f71050a83b81127e6e648b87`.

A private external recovery snapshot preserves 25,814 files including tracked/staged state, untracked/ignored content and Git metadata. Its JSON manifest SHA-256 is `A336D657495D5C9FE078D0381909698A6347A48A4DE598A20C3BFC7AF6FB8023`. Every copied file hash was verified. The first long-path snapshot was not accepted; a short-path snapshot passed verification. Backup contents, ignored environments, secrets, binaries, databases and caches are never publication inputs. Exact local backup location is supplied privately in the completion response.

GitHub reports PR #330 already merged on 2026-10-13; issue #322 remains open. This consolidation changes neither lifecycle state. Archived prompts preserve their original dates, proposals, approvals and restrictions; their inclusion does not grant new authority or make proposed policies effective.

## Inclusion and deferral decisions

Include offline launch-plan/native-boundary contracts and tests, preserved planning/authority records, governance reconciliation and completed test-race corrections. Existing PR #331 codec/test files differ from snapshot only in line endings; retain main's versions. Exclude unfinished broker/supervisor, legacy administrative startup/runtime, qualification scripts, solution/dependency wiring and unrelated terminal preferences. They remain in the private recovery snapshot and original checkout.

Dockerfile and entrypoint remain byte-for-byte as fresh main. The reviewed admin enable branch is not brought into main. AdminDiagnostics remains the already merged offline protocol-only library with explicitly limited compile items; no executable router/guardian is introduced. Existing runtime selection and public/private guards are preserved.

The strategy document carries original governance history plus an explicit consolidation clarification. REST requirements describe future routes; none is implemented or tested as an HTTP endpoint here. Rolling last-90-completed-day coverage is deferred because the generator/fingerprint contract remains fixed. No read-only API, operational chart or Azure recovery is claimed.

## Verification and limits

Python: qualified existing CPython 3.13 virtual environment; `python -B -m unittest discover -s python/presentation -p 'test_*.py' -v`: 157 total, 146 passed, 11 platform skips, exit 0. Initial system-Python attempt lacked Streamlit (134 total, 6 import errors, 11 skips); rerun used the existing dependency environment. The ignored environment was copied privately for .NET fixtures and is not committed.

.NET final full Debug suite: Domain 11, Application 215, Architecture 27 and Infrastructure 317 passed (570 total), zero failures/skips, exit 0. Initial Release run: Domain 11/Application 215/Architecture 27 passed; Infrastructure 288 passed/29 failed due to missing local .venv and Debug Worker fixtures. With prerequisites prepared, full Debug Infrastructure reached 316 passed/1 timeout. The same HTTP test passed alone, then timed out in the full suite again. A scoped test-only correction retains 500 ms startup polling and uses a separate five-second client for post-readiness semantic HTTP assertions. An intermediate attempt to change an already-used HttpClient timeout was rejected by HttpClient and corrected before final verification. All 401/409/200, secret-redaction and lifecycle assertions remain intact. No runtime or production timeout changes.

Commands: `dotnet test AIQuantTradingResearch.slnx --configuration Debug --no-restore --nologo -m:1`; initial Debug preparation used `dotnet test AIQuantTradingResearch.slnx --configuration Debug --nologo -p:NuGetAudit=false`. Release Worker fixtures were built by the initial Release test run. Offline protocol validation used `dotnet test tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AIQuantTradingResearch.AdminDiagnostics.Tests.csproj --configuration Debug --nologo -p:NuGetAudit=false`. The final solution test command includes compilation/build verification. Python discovery used the existing qualified `.venv/Scripts/python.exe` with `-B` and `PYTHONDONTWRITEBYTECODE=1`.

Offline .NET protocol project (not a runtime executable): 50 passed, exit 0. Restore initially warned NU1900 because vulnerability metadata was unavailable; later restore used `-p:NuGetAudit=false` with existing cached packages. This is a local validation limitation, not a repository/package change or completed vulnerability review.

No signing exception or certificate configuration was copied; tests execute without enabling local signing. Linux/cgroup tests skip on this Windows host; their skips are not kernel-isolation acceptance. No Docker/container, browser, Azure, provider-network, credential or production tests were run. Generator tests use the embedded independent CPython reference, not live market acquisition.

`PUBLIC_RUNTIME_VERIFIED=False`. Advanced WP07 remains ON HOLD. Production deployment is NO-GO. Confidentiality, exclusive admission, verified cleanup and resource qualification remain unmet runtime gates; passing offline tests cannot replace them.

## Fixed generator and protected overlaps

Generator coverage remains `[2025-10-26T00:00:00Z, 2026-10-01T00:00:00Z)`, version `aiq-synthetic-ohlcv-v1`, seed 114; twelve selections and 250 calculation-only warmup candles are unchanged. Generator, fingerprint expectations, canonical publication policy and public runtime gate source must remain unchanged against main. The full Infrastructure suite includes complete-byte/hash CPython comparison, all twelve windows/visible-row hashes, reproducibility, rejection/cancellation and public publication negatives. No synthetic data is generated by public requests under this consolidation.

| Sensitive overlap | Snapshot SHA-256 | Consolidated SHA-256 | Disposition |
|---|---|---|---|
| `Dockerfile` | `AC50203521C491B0A1FDDEFE1214A3EA2001F7CF3C15704E3A512717CCD314E6` | `B7496B7DB7DBE8CCDADA7E9E7A9226125754EDC9FD8E5094668FEBB1F956B21A` | Dirty admin wiring deferred; main retained |
| `container/entrypoint.sh` | `C3C146F27EB8B7CF0493F0FBAA136A85CE644580530C825D2E0220BCBEA05CF7` | `B245139B1732E84450F00250931E239DCC15FDC761C68D8DA34EC7B981C4172F` | Dirty admin wiring deferred; main retained |
| `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` | `9F57488929F7EA285E6F9E7C4F7F3298E1D57ED86D87D293057BB98CA93B694E` | `86A0AE9347378212171EBF6CCABF03C25090EF52717FD5F677C9E078E9B2D854` | Reconciled history; no activation |
| `src/AIQuantTradingResearch.Infrastructure/MarketData/Synthetic/SyntheticHistoricalMarketDataProvider.cs` | `6E0DFAAE34695047C1DE75588FFF0DFD32A34375B58FA04AEEB4C94EF4579041` | `6E0DFAAE34695047C1DE75588FFF0DFD32A34375B58FA04AEEB4C94EF4579041` | Unchanged fixed contract/gate |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/SyntheticHistoricalMarketDataProviderTests.cs` | `9B0943C446C8668A549356B9015AFE9D58AFCE5133ADC193B498CC4111937AA2` | `9B0943C446C8668A549356B9015AFE9D58AFCE5133ADC193B498CC4111937AA2` | Unchanged fixed contract/gate |
| `python/presentation/historical_market_bridge.py` | `DE618D4404B4BFAA43DAD1A78351EFB65A63C572394E86D82C123B50DF64F565` | `0C9DF0E3321521E2F58AA9AC47E80E8CC30D03B1E760CACBA2302CD2AF20D7E7` | Unchanged fixed contract/gate |

## Every original changed/untracked path

Ignored content is preserved in the private 25,814-file snapshot and excluded from Git. The following accounts for every original tracked modification and untracked path. `CompareMain` means no semantic change, line-ending normalization only. Inclusion of historical documents does not activate their requested actions.

| Path | Disposition | Reason |
|---|---|---|
| `AIQuantTradingResearch.slnx` | Deferred | Held admin wiring/dependency; retain main unchanged |
| `container/admin_diagnostics_bootstrap.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `container/admin_lifecycle_launch_plan.py` | Include | Inert offline contracts/tests or bounded test-race correction |
| `container/admin_lifecycle_native_boundary_contracts.py` | Include | Inert offline contracts/tests or bounded test-race correction |
| `container/admin_lifecycle_protocol.py` | CompareMain | Already packaged by PR331; require byte comparison |
| `container/entrypoint.sh` | Deferred | Held admin wiring/dependency; retain main unchanged |
| `container/presentation_lifecycle_agent.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `Directory.Packages.props` | Deferred | Held admin wiring/dependency; retain main unchanged |
| `Dockerfile` | Deferred | Held admin wiring/dependency; retain main unchanged |
| `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md` | IncludeReconciled | Governance history; rolling coverage explicitly deferred |
| `docs/roadmap/release-1.14/prompters/release-1.14-baseline-consolidation-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c1-read-only-application-recovery-assessment-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2a-2-read-only-azure-runtime-evidence-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2a-read-only-public-startup-diagnosis-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-continuous-implementation-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-diagnostic-rest-api-feasibility-security-contract-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-rest-api-narrow-offline-implementation-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-rest-architecture-design-closure-approval-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-rest-disabled-mode-bounded-runtime-observation-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-rest-lifecycle-descriptor-read-only-diagnosis-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-rest-local-container-qualification-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-rest-routing-azure-app-settings-credential-confinement-design-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-rest-startup-disabled-route-read-only-diagnosis-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-rest-targeted-local-container-requalification-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-rest-targeted-startup-repair-option-a-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-administrative-uid-gid-authority-amendment-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-agent-termination-instrumentation-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-agent-termination-read-only-diagnosis-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-application-container-console-access-repository-assessment-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-architecture-options-constraint-tradeoffs-read-only-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-azure-console-access-capability-assessment-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-azure-f1-container-capability-sqlite-mount-evidence-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-disabled-mode-measurement-only-qualification-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-distinct-supervisor-identity-guardian-trust-read-only-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-docker-engine-access-preflight-and-measurement-resumption-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-enabled-mode-shutdown-semantics-targeted-repair-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-enabled-shutdown-host-executed-requalification-script-preparation-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-enabled-shutdown-qualification-script-hardening-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-guardian-isolation-d1-integrity-read-only-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-guardian-lifecycle-exit-provenance-read-only-design-closure-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-host-executed-disabled-mode-qualification-script-preparation-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-identity-allocation-wire-protocol-freeze-read-only-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-license-notice-literal-allowlist-amendment-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-native-interface-composition-specification.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-offline-validation-collector-extension-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-owner-trust-decisions-platform-evidence-read-only-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p1-c1-resume-offline-protocol-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p1c-offline-lifecycle-protocol-security-corrections-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p1d-conditional-review-pr-merge-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p1d-resume-after-baseline-merge-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p1e-build-dependency-inventory-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p1f-offline-protocol-packaging-validation-conditional-pr-merge-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p1-offline-lifecycle-protocol-implementation-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2a-offline-launch-plan-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2b-launch-plan-security-fd-analysis-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2c-endpoint-lifetime-model-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2d-fd5-descriptor-security-review-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2e-endpoint-handoff-fd5-shutdown-model-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2e-f1-test-helper-correction-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2f-independent-handoff-security-review-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2g-metadata-binding-inherited-endpoint-correction-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2h-independent-metadata-fd5-security-review-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2i-phase-trace-ui-exec-correction-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2j-final-static-launch-plan-security-review-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2-p1-security-integration-readiness-review-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p2-runtime-integration-architecture-inventory-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3a-native-boundary-readonly-investigation-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3b-offline-native-boundary-contracts-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3c-independent-native-boundary-contract-review-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3d-offline-native-boundary-contract-corrections-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3e-independent-offline-contract-reassessment-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3f-targeted-offline-contract-validation-corrections-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3g-independent-offline-contract-security-reassessment-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3h-bootstrap-language-abi-feasibility-investigation-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3i-native-interface-composition-specification-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3j-independent-native-interface-composition-design-review-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-p3k-native-abi-toolchain-evidence-dossier-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-process-ownership-read-only-design-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-runtime-reaper-trust-contract-read-only-authority-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-shutdown-security-contract-owner-decision-planning-only-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-startup-protocol-synthetic-credential-boundary-read-only-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-strict-guardian-exit-status-feasibility-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-t4-distinct-identity-design-closure-read-only-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-t5-t7-implementation-readiness-qualification-planning-codex.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-trust-descendants-credential-ingress-read-only-codex-authority.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-unprivileged-supervisor-design-closure-codex-authority.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/release-1.14-wp07-yarp-dependency-cross-uid-lifecycle-design-closure-codex-prompt.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_ARCHITECTURE_DECISION_RECORD.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_CONTRACT_CLOSURE_CODEX_PROMPT.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_MECHANISM_CLOSURE_CODEX_PROMPT.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_A2_SQLITE_MECHANISM_CLOSURE_SPECIFICATION.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_ARCHITECTURE_ALTERNATIVES_DECISION_RECORD.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_Azure_F1_Containment_Feasibility_Codex_Authority.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_AZURE_F1_READ_ONLY_FEASIBILITY_INVENTORY_CODEX_PROMPT.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_BROKER_SUPERVISOR_IMPLEMENTATION_PREFLIGHT.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_C2_CONTRACT_AMENDMENT_CODEX_PROMPT.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_OFFLINE_POLICY_FROZEN_EVIDENCE_ASSESSMENT.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_OFFLINE_POLICY_IMPLEMENTATION_REVIEW.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_OPTION_B_CONSOLIDATED_PLANNING_AND_QUALIFICATION.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_OPTION_B_CONSOLIDATED_SECURITY_CONTRACT_AMENDMENT.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_READ_ONLY_ARCHITECTURE_CLOSURE.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_READ_ONLY_QUALIFICATION_PLAN.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_SQLITE_CONTAINER_IDENTITY_EVIDENCE_REPORT.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/prompters/WP07_SQLITE_REPOSITORY_ONLY_EVIDENCE_MILESTONE_CODEX_PROMPT.md` | Include | Historical authority/design evidence; no activation |
| `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md` | IncludeReconciled | Governance history; rolling coverage explicitly deferred |
| `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md` | IncludeReconciled | Governance history; rolling coverage explicitly deferred |
| `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md` | IncludeReconciled | Governance history; rolling coverage explicitly deferred |
| `eng/azure-cli/r1.14-qualification/wp07-azure-startup-evidence/aiqr-azure-startup-chronology.ps1` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `eng/azure-cli/r1.14-qualification/wp07-azure-startup-evidence/aiqr-azure-startup-evidence.ps1` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `eng/azure-cli/r1.14-qualification/wp07-azure-startup-evidence/aiqr-public-connectivity-c2a6.ps1` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `eng/azure-cli/r1.14-qualification/wp07-azure-startup-evidence/aiqr-public-http-diagnostic.ps1` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `eng/azure-cli/r1.14-qualification/wp07-disabled-mode/Invoke-DisabledModeQualification.ps1` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `eng/azure-cli/r1.14-qualification/wp07-disabled-mode/README.md` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `eng/azure-cli/r1.14-qualification/wp07-enabled-shutdown/release-1.14-wp07-enabled-shutdown-qualification.ps1` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `eng/azure-cli/r1.14-qualification/wp07-enabled-shutdown/release-1.14-wp07-enabled-shutdown-qualification-RUNBOOK.md` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `eng/github/unblock-scripts-terminal.ps1` | Deferred | Unrelated local terminal preference |
| `python/presentation/test_admin_diagnostics_bootstrap.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/presentation/test_admin_lifecycle_launch_plan.py` | Include | Inert offline contracts/tests or bounded test-race correction |
| `python/presentation/test_admin_lifecycle_native_boundary_contracts.py` | Include | Inert offline contracts/tests or bounded test-race correction |
| `python/presentation/test_admin_lifecycle_protocol.py` | CompareMain | Already packaged by PR331; require byte comparison |
| `python/presentation/test_presentation_lifecycle_agent.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/presentation/test_public_broker.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/presentation/test_public_quota_evidence.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/presentation/test_public_runtime_isolation.py` | Include | Inert offline contracts/tests or bounded test-race correction |
| `python/presentation/test_public_supervisor.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/public_runtime/__init__.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/public_runtime/bootstrap.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/public_runtime/broker.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/public_runtime/ipc.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/public_runtime/journal.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/public_runtime/quota.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `python/public_runtime/supervisor.py` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/AdminDiagnosticsAuthentication.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/AdminDiagnosticsConfiguration.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/AdminDiagnosticsContracts.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/AdminDiagnosticsRouting.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/AdminLifecycleProtocol.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/AIQuantTradingResearch.AdminDiagnostics.csproj` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/EnvironmentMetadataCollector.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/Notices/SYSTEM-IO-HASHING-LICENSE.txt` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/Notices/SYSTEM-IO-HASHING-THIRD-PARTY-NOTICES.txt` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/Notices/YARP-LICENSE.txt` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/PresentationLifecycle.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `src/AIQuantTradingResearch.AdminDiagnostics/Program.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminDiagnosticsRoutingTests.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminDiagnosticsSecurityTests.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AdminLifecycleProtocolTests.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/AIQuantTradingResearch.AdminDiagnostics.Tests.csproj` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `tests/AIQuantTradingResearch.AdminDiagnostics.Tests/PresentationLifecycleTests.cs` | Deferred | Unfinished WP07 runtime/qualification; preserve without activation |
| `tests/AIQuantTradingResearch.Infrastructure.Tests/HistoricalProductionCompositionAcceptanceTests.cs` | Include | Inert offline contracts/tests or bounded test-race correction |

Additional integration-only files: this evidence report, and a bounded post-readiness timeout correction in `tests/AIQuantTradingResearch.Infrastructure.Tests/SqlitePersistenceTests.cs`. The original checkout is not edited.

## Rollback and next work

If merged changes need reversal, use a separately reviewed revert PR against the recorded consolidation commit; do not reset shared main or discard the protected checkout. Restore original local work only from the verified private snapshot after owner direction. Create a new clean checkout of merged main for C2B; the protected original remains intentionally dirty.

C2B still needs isolated shell/API implementation, routing compatibility proof, bounded unavailable responses, a separately resolved rolling-window contract, runtime isolation/resource qualification, browser/HTTP/container evidence and separately authorized deployment. No Azure or activation permission follows from consolidation.

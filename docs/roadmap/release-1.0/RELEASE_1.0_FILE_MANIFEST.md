# Release 1.0 File Manifest

## Metadata

  -----------------------------------------------------------------------
  Field                               Value
  ----------------------------------- -----------------------------------
  Project                             AIQuantTradingResearch

  Phase                               Phase 3

  Release                             1.0 --- Market Data Foundation

  Purpose                             Authoritative expected/allowed
                                      Release 1.0 artifact inventory and
                                      mutation boundary

  Companion Authority                 `RELEASE_1.0_EXECUTION_PLAN.md`
  -----------------------------------------------------------------------

## 1. Purpose

This manifest defines the expected Release 1.0 artifact surface: which
files may be created or modified, which are expected by work packages,
which areas are protected, and which files are unexpected unless
separately authorized.

The manifest does not itself authorize implementation beyond the active
work package in `RELEASE_1.0_EXECUTION_PLAN.md`.

## 2. Manifest Principles

1.  Existing files are modified only when a WP proves need.
2.  Listed path patterns are allowed boundaries, not mandatory edits.
3.  No new project is expected.
4.  No new package is automatically authorized.
5.  Provider-specific artifacts remain in Infrastructure.
6.  Prompt-chat companions are intentional governance artifacts.
7.  Closure-unblock artifacts are created only after an actual blocker.
8.  Any changed/untracked file outside this manifest and active-WP
    authority is `UNEXPECTED`.

## 3. Release Governance Artifacts

### Mandatory

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
```

### Expected prompt directory

``` text
docs/roadmap/release-1.0/prompts/
```

### Expected WP prompt pairs

``` text
01-release-repository-preflight-codex-prompt.md
01-release-repository-preflight-codex-prompt-chat.md
02-market-data-provider-discovery-codex-prompt.md
02-market-data-provider-discovery-codex-prompt-chat.md
03-market-data-domain-evolution-codex-prompt.md
03-market-data-domain-evolution-codex-prompt-chat.md
04-market-data-application-contracts-codex-prompt.md
04-market-data-application-contracts-codex-prompt-chat.md
05-historical-market-data-use-case-integration-codex-prompt.md
05-historical-market-data-use-case-integration-codex-prompt-chat.md
06-provider-transport-model-codex-prompt.md
06-provider-transport-model-codex-prompt-chat.md
07-provider-http-client-codex-prompt.md
07-provider-http-client-codex-prompt-chat.md
08-market-data-normalization-codex-prompt.md
08-market-data-normalization-codex-prompt-chat.md
09-market-data-validation-failure-mapping-codex-prompt.md
09-market-data-validation-failure-mapping-codex-prompt-chat.md
10-dependency-registration-configuration-codex-prompt.md
10-dependency-registration-configuration-codex-prompt-chat.md
11-worker-market-data-execution-codex-prompt.md
11-worker-market-data-execution-codex-prompt-chat.md
12-domain-application-tests-codex-prompt.md
12-domain-application-tests-codex-prompt-chat.md
13-infrastructure-provider-tests-codex-prompt.md
13-infrastructure-provider-tests-codex-prompt-chat.md
14-architecture-evolution-codex-prompt.md
14-architecture-evolution-codex-prompt-chat.md
15-documentation-alignment-codex-prompt.md
15-documentation-alignment-codex-prompt-chat.md
16-full-validation-integration-acceptance-codex-prompt.md
16-full-validation-integration-acceptance-codex-prompt-chat.md
```

### Expected lifecycle prompt pairs

``` text
release-1.0-github-integration-codex-prompt.md
release-1.0-github-integration-codex-prompt-chat.md
release-1.0-closure-codex-prompt.md
release-1.0-closure-codex-prompt-chat.md
```

### Conditional only after a real blocker

``` text
release-1.0-*-unblock-codex-prompt.md
release-1.0-*-unblock-codex-prompt-chat.md
```

Do not pre-create hypothetical unblock artifacts.

## 4. Market Data Architecture Documents

### Mandatory from WP02

``` text
docs/architecture/market-data/MARKET_DATA_PROVIDER_ASSESSMENT.md
docs/architecture/market-data/MARKET_DATA_PROVIDER_DECISION.md
```

### Conditionally modifiable existing architecture docs under WP15

``` text
docs/architecture/solution/SOLUTION_ARCHITECTURE.md
docs/architecture/solution/DEPENDENCY_RULES.md
docs/architecture/solution/BOUNDARY_DEFINITIONS.md
docs/architecture/design/MODULE_INTERACTIONS.md
docs/architecture/design/PUBLIC_CONTRACTS.md
docs/architecture/implementation/PROJECT_STRUCTURE.md
docs/architecture/implementation/DEPENDENCY_INJECTION.md
docs/architecture/implementation/TESTING_STRATEGY.md
docs/architecture/implementation/CONFIGURATION_MODEL.md
docs/architecture/implementation/LOGGING_STRATEGY.md
docs/architecture/implementation/OBSERVABILITY_MODEL.md
```

Another existing architecture document may be modified only when WP15
explicitly proves relevance.

## 5. Domain Project

Allowed project:

``` text
src/AIQuantTradingResearch.Domain/
```

Only files required by WP03 may be created/modified. Zero Domain changes
is acceptable.

Prohibited Domain content:

``` text
HttpClient
HTTP concepts
JSON concepts
provider DTOs/namespaces
provider URLs/API keys/error codes
database/storage concepts
persistence attributes
```

No new Domain project.

## 6. Application Project

Allowed project:

``` text
src/AIQuantTradingResearch.Application/
```

WP04/WP05 may create/modify only files needed for historical request
contracts, `IObservationSource` evolution or equivalent
Application-owned boundary, research result/failure contracts, and
`ResearchUseCase` orchestration.

Likely area:

``` text
src/AIQuantTradingResearch.Application/Research/
```

WP10 may modify:

``` text
src/AIQuantTradingResearch.Application/DependencyInjection.cs
```

Prohibited Application content:

``` text
provider DTOs
JSON models
HttpClient implementation
provider URLs/authentication logic
provider namespace dependencies
Infrastructure references
```

## 7. Infrastructure Project

Allowed project:

``` text
src/AIQuantTradingResearch.Infrastructure/
```

Provider-specific implementation belongs here.

Allowed provider-owned structure may include:

``` text
src/AIQuantTradingResearch.Infrastructure/MarketData/
src/AIQuantTradingResearch.Infrastructure/MarketData/<ProviderName>/
```

Potential artifacts:

``` text
provider request DTOs
provider response DTOs
HTTP client/transport
provider adapter implementing Application boundary
JSON mapping
normalization/mapping
validation/failure mapping
provider options/configuration
```

WP10 may modify:

``` text
src/AIQuantTradingResearch.Infrastructure/DependencyInjection.cs
```

Prohibited:

``` text
database/storage
cache
streaming/WebSocket
message broker
multiple-provider failover
plugin framework
trading execution
AI/ML
cloud deployment
background ingestion
```

## 8. Worker Project

Allowed project:

``` text
src/AIQuantTradingResearch.Worker/
```

WP11 may minimally modify `Program.cs` and existing Worker-local
configuration, or add minimum configuration artifacts required by the
selected provider.

Worker must not own provider DTOs, HTTP business logic, normalization,
Domain calculations, provider failure mapping, storage, scheduling,
streaming, or trading.

No new host project.

## 9. Domain Tests

Allowed:

``` text
tests/AIQuantTradingResearch.Domain.Tests/
```

WP12 may create/modify only Release 1.0 Domain behavioral tests.

No live provider or Infrastructure dependency.

## 10. Application Tests

Allowed:

``` text
tests/AIQuantTradingResearch.Application.Tests/
```

WP12 may test historical request semantics, `ResearchUseCase`
orchestration, test-owned observation-source doubles, and Application
failure propagation.

No concrete provider or live HTTP dependency.

Existing Application friend-assembly boundary may remain. No additional
friend assembly is automatically authorized.

## 11. Infrastructure Tests

Allowed:

``` text
tests/AIQuantTradingResearch.Infrastructure.Tests/
```

WP13 may create/modify provider transport, DTO deserialization,
normalization, failure mapping, fake HTTP, DI tests, and provider
fixtures.

Potential fixture locations:

``` text
tests/AIQuantTradingResearch.Infrastructure.Tests/Fixtures/
tests/AIQuantTradingResearch.Infrastructure.Tests/MarketData/
tests/AIQuantTradingResearch.Infrastructure.Tests/MarketData/<ProviderName>/
```

Canonical tests must not require live internet.

## 12. Architecture Tests

Allowed:

``` text
tests/AIQuantTradingResearch.Architecture.Tests/
```

WP14 may modify/create only tests required for stable Release 1.0
structural boundaries, including dependency directions, cycles,
Application-owned abstractions, provider implementation visibility,
provider DTO visibility, and provider-specific dependency isolation.

No brittle file/folder/source-text rules without explicit justification.

## 13. Provider Fixture Files

Conditionally authorized under WP13.

Allowed content:

``` text
sanitized provider response examples
valid historical response fixtures
provider error fixtures
empty response fixtures
malformed/invalid-value fixtures
```

Fixtures must contain no secrets, tokens, personal information, or
prohibited redistribution content, and must live under the relevant test
project.

## 14. Configuration Artifacts

Configuration evolution is conditionally authorized under WP10/WP11 only
when required by the selected provider.

Allowed non-secret configuration may include provider base endpoint,
provider identifier, public options, and default historical request
parameters.

Secrets must not be committed. If credentials are required, use safe
local/environment configuration consistent with repository guidance.

This does not authorize a secret-management platform.

## 15. Package / Project Manifest Policy

Protected unless explicit later authority proves need:

``` text
Directory.Packages.props
Directory.Build.props
global.json
AIQuantTradingResearch.slnx
```

No new .NET project is expected.

No new NuGet package is automatically authorized. If one becomes
necessary, the active WP must stop for explicit authorization.

## 16. Engineering Scripts

Protected by default:

``` text
eng/**
```

Release 1.0 does not currently authorize changes to
restore/build/format/test/verify/clean scripts.

## 17. Repository Root Policy Files

Protected by default:

``` text
.editorconfig
.gitattributes
.gitignore
```

Release 1.0 does not authorize changes unless a specific WP proves a
release-blocking repository-policy requirement and separate authority is
granted.

The accepted Release 0.9 LF checkout policy remains baseline.

## 18. GitHub / Workflow Assets

Protected by default:

``` text
.github/**
```

Implementation WPs do not authorize workflow, issue-template,
PR-template, label, or automation changes.

## 19. Explicitly Prohibited New Repository Areas

Do not create production areas for:

``` text
storage/
database/
persistence/
streaming/
websocket/
trading/
orders/
broker/
portfolio/
risk/
backtesting/
ml/
ai/
llm/
pipelines/
scheduler/
messaging/
cloud/
plugins/
```

Equivalent functionality is equally prohibited.

## 20. File Ownership by Work Package

  ---------------------------------------------------------------------------
  WP                                  Expected Mutation Areas
  ----------------------------------- ---------------------------------------
  WP01                                none

  WP02                                `docs/architecture/market-data/**`

  WP03                                `src/...Domain/**` only if needed

  WP04                                `src/...Application/Research/**`

  WP05                                `src/...Application/Research/**`

  WP06                                `src/...Infrastructure/MarketData/**`

  WP07                                `src/...Infrastructure/MarketData/**`

  WP08                                `src/...Infrastructure/MarketData/**`

  WP09                                Infrastructure MarketData +
                                      already-authorized Application failure
                                      contracts if required

  WP10                                Application/Infrastructure DI + minimum
                                      configuration

  WP11                                Worker only

  WP12                                Domain.Tests + Application.Tests

  WP13                                Infrastructure.Tests + deterministic
                                      fixtures

  WP14                                Architecture.Tests

  WP15                                proven-relevant docs only

  WP16                                no persistent mutation expected

  Git/GitHub Integration              integration prompt/chat + Git
                                      history/PR only

  Closure                             closure prompt/chat + narrowly
                                      authorized governance reconciliation
                                      only
  ---------------------------------------------------------------------------

## 21. Expected Release 1.0 Prompt/Governance Artifact Count

``` text
16 WP prompt pairs = 32 files
Git/GitHub integration pair = 2 files
Closure pair = 2 files
Baseline prompt total = 36 files
```

This excludes the execution plan, file manifest, conditional unblock
artifacts, and pre-execution planning/governance-integration prompts.

Counts are reconciliation aids, not architecture.

## 22. Unexpected File Rule

Any changed/untracked Release 1.0 file not covered by this manifest, the
active WP, or separate unblock/corrective authority is:

``` text
UNEXPECTED
```

Execution must stop before integration until explained.

Do not delete unexplained files automatically.

## 23. Release Acceptance Reconciliation

WP16 must reconcile every actual changed/untracked file with:

``` text
path
status
owning WP
manifest category
authority
assessment
```

For mandatory expected artifacts:

``` text
present
missing
not applicable
```

No unexplained file may exist in an accepted candidate.

## 24. Integration Reconciliation

The Git/GitHub Integration Gate must prove:

``` text
accepted candidate files
+
authorized integration prompt/chat
=
committed integration candidate
```

No product drift may occur during transport.

## 25. Closure Reconciliation

Closure must prove merged `main` contains:

``` text
accepted Release 1.0 production artifacts
accepted tests
accepted architecture evolution
accepted documentation
Release 1.0 governance prompts
integration governance prompts
closure governance prompts
any explicitly authorized corrective/unblock artifacts
```

and no unexplained Release 1.1 implementation artifact.

## 26. Release 1.1 Boundary

Release 1.1 artifacts must not be created during Release 1.0
implementation, integration, or closure except when closure explicitly
returns:

``` text
RELEASE 1.0 CLOSED
RELEASE 1.1 GOVERNANCE DESIGN AUTHORIZED
```

Even then, only Release 1.1 governance-design artifacts are authorized
until Release 1.1 governance is reviewed and accepted.

## 27. Final Manifest Rule

Release 1.0 is manifest-compliant only when:

``` text
all changed files have authority
all mandatory artifacts are present
conditional artifacts are justified
protected assets remain unchanged unless separately authorized
provider-specific implementation remains in Infrastructure
no prohibited future-scope area exists
no secret is committed
no unexplained file remains
```

This manifest is authoritative together with
`RELEASE_1.0_EXECUTION_PLAN.md`.

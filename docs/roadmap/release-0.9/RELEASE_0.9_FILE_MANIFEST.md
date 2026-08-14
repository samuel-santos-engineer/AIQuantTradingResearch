# Release 0.9 File Manifest

## Phase 2 --- Release 0.9: Research Platform

## 1. Purpose

This is the authoritative file-scope manifest for Release 0.9. The
execution plan defines **what** each work package may accomplish; this
manifest defines **where** artifacts may be created or modified and
which areas remain protected.

## 2. Manifest Principles

-   **No premature naming:** WP02 discovers the research vocabulary;
    bounded directories are authorized where exact filenames cannot yet
    be justified.
-   **Existing files before new files:** update the correct existing
    authority rather than create parallel artifacts.
-   **No new projects:** Release 0.9 uses the existing 8-project
    solution.
-   **Minimal delta:** directory authorization is never blanket
    permission.
-   **Historical immutability:** accepted Release 0.8 prompts/history
    remain protected.

## 3. Governance Structure

``` text
docs/roadmap/release-0.9/
├── RELEASE_0.9_EXECUTION_PLAN.md
├── RELEASE_0.9_FILE_MANIFEST.md
└── prompts/
    ├── 01-repository-release-preflight-codex-prompt.md
    ├── 02-research-domain-discovery-codex-prompt.md
    ├── 03-research-domain-model-codex-prompt.md
    ├── 04-research-application-contracts-codex-prompt.md
    ├── 05-research-execution-use-case-codex-prompt.md
    ├── 06-research-infrastructure-adapter-codex-prompt.md
    ├── 07-dependency-registration-codex-prompt.md
    ├── 08-worker-research-execution-codex-prompt.md
    ├── 09-domain-tests-codex-prompt.md
    ├── 10-application-tests-codex-prompt.md
    ├── 11-infrastructure-tests-codex-prompt.md
    ├── 12-architecture-evolution-codex-prompt.md
    ├── 13-documentation-alignment-codex-prompt.md
    └── 14-full-validation-integration-acceptance-codex-prompt.md
```

When intentionally preserved, each WP may also have the corresponding
`*-codex-prompt-chat.md`. Prompts are created progressively. A later
closure prompt is optional, not WP15.

## 4. Production Boundaries

  ------------------------------------------------------------------------------------------------------------------------------------
  Project root                                   Authorized Release 0.9  Protected from
                                                 responsibility
  ---------------------------------------------- ----------------------- -------------------------------------------------------------
  `src/AIQuantTradingResearch.Domain/`           WP03 approved pure      Application/Infrastructure/Worker concerns,
                                                 research Domain         DI/config/HTTP/persistence/logging/provider-specific/future
                                                 concepts/invariants     abstractions

  `src/AIQuantTradingResearch.Application/`      WP04 contracts, WP05    Concrete Infrastructure, HTTP/persistence, Worker runtime,
                                                 use case, WP07          plugins, provider/vendor-specific contracts
                                                 Application
                                                 registration

  `src/AIQuantTradingResearch.Infrastructure/`   WP06 deterministic      Real providers, HTTP, database/filesystem persistence,
                                                 adapter, WP07           cache/broker/plugins/random/current-time behavior
                                                 Infrastructure
                                                 registration

  `src/AIQuantTradingResearch.Worker/`           WP08 thin               Domain/research algorithms, provider/data-generation logic,
                                                 composition/reference   business validation leakage, REST/UI/scheduler
                                                 execution
  ------------------------------------------------------------------------------------------------------------------------------------

No new production project is authorized.

## 5. Test Boundaries

  ------------------------------------------------------------------------------------------
  Test root                                              Authorized responsibility
  ------------------------------------------------------ -----------------------------------
  `tests/AIQuantTradingResearch.Domain.Tests/`           WP09 meaningful Domain behavioral
                                                         tests

  `tests/AIQuantTradingResearch.Application.Tests/`      WP10 deterministic Application
                                                         orchestration tests/test doubles

  `tests/AIQuantTradingResearch.Infrastructure.Tests/`   WP11 deterministic adapter
                                                         contract/behavior tests

  `tests/AIQuantTradingResearch.Architecture.Tests/`     WP12 preserve Release 0.8 rules and
                                                         add justified Release 0.9 rules
  ------------------------------------------------------------------------------------------

No new test project is authorized.

## 6. Research Documentation

WP02 is expected to create:

``` text
docs/architecture/research/RESEARCH_DOMAIN_MODEL.md
```

It records discovered terminology, ownership, invariants, deterministic
reference scenario, non-goals, rejected/future concepts, and
implementation constraints. Additional files under this area require
demonstrated durable engineering need; they are not automatically
authorized.

## 7. Existing Documentation Potentially Modifiable by WP13

WP13 may inspect relevant current-state responsibilities under
`docs/architecture/**`, `docs/design/**`, `docs/implementation/**`, and
`README.md`. Before mutation each candidate must be classified
`CURRENT-STATE`, `HISTORICAL`, `PLANNED`, or `UNRELATED`. Only relevant
current-state authority may be updated by default. Historical Release
0.8 governance is protected.

## 8. Protected Root/Build Assets

Protected by default:

``` text
AIQuantTradingResearch.slnx
Directory.Build.props
Directory.Packages.props
global.json
docker-compose.yml
eng/**
.github/**
```

Release 0.9 does not currently authorize new projects,
SDK/package-policy changes, Docker changes, engineering-script changes,
or GitHub Actions. If an active WP discovers a required protected-asset
mutation, it must stop/report unless its prompt explicitly grants that
exact change.

## 9. Work Package → Artifact Matrix

  ------------------------------------------------------------------------------------------------------------
                            WP Work Package          Authorized artifact boundary
  ---------------------------- --------------------- ---------------------------------------------------------
                            01 Repository & Release  No implementation/documentation artifact by default;
                               Preflight             authoritative WP prompt and Codex report provide
                                                     governance evidence.

                            02 Research Domain       `docs/architecture/research/RESEARCH_DOMAIN_MODEL.md`.
                               Discovery

                            03 Research Domain Model `src/AIQuantTradingResearch.Domain/**` only as required.

                            04 Research Application  `src/AIQuantTradingResearch.Application/**` only as
                               Contracts             required.

                            05 Research Execution    `src/AIQuantTradingResearch.Application/**` only as
                               Use Case              required.

                            06 Research              `src/AIQuantTradingResearch.Infrastructure/**` only as
                               Infrastructure        required.
                               Adapter

                            07 Dependency            Minimal existing Application/Infrastructure
                               Registration          registration-file changes.

                            08 Worker Research       Minimal changes under
                               Execution             `src/AIQuantTradingResearch.Worker/**`.

                            09 Domain Tests          `tests/AIQuantTradingResearch.Domain.Tests/**`.

                            10 Application Tests     `tests/AIQuantTradingResearch.Application.Tests/**`.

                            11 Infrastructure Tests  `tests/AIQuantTradingResearch.Infrastructure.Tests/**`.

                            12 Architecture          `tests/AIQuantTradingResearch.Architecture.Tests/**`.
                               Evolution

                            13 Documentation         `docs/architecture/research/RESEARCH_DOMAIN_MODEL.md`
                               Alignment             plus only relevant existing current-state docs.

                            14 Full Validation,      Governance prompt/chat artifacts and GitHub
                               Integration &         issue/PR/milestone evidence; separate closure prompt only
                               Acceptance            if needed and never as WP15.
  ------------------------------------------------------------------------------------------------------------

Every WP also has its authoritative prompt under
`docs/roadmap/release-0.9/prompts/`; an intentionally preserved
prompt-chat file follows the same slug.

## 10. Explicitly Forbidden New Artifact Categories

Unless authority is amended, do not create: new production/test
projects; new solution files; real-provider/HTTP clients; database
contexts/migrations/repositories; caches/message buses; plugin
projects/loaders; strategy/backtesting projects; AI/ML/model artifacts;
cloud/deployment assets; new Docker topology; GitHub Actions; REST/UI
projects; generic framework projects; or per-WP execution-report
Markdown files.

## 11. Generated/Temporary Files

Do not commit generated/local output such as `**/bin/**`, `**/obj/**`,
`TestResults/**`, `coverage/**`, `*.trx`, temporary logs, IDE-local
state, secrets, credentials, tokens, or scratch files unless existing
repository policy explicitly requires them.

## 12. Package Boundary

No new NuGet package is pre-authorized. If genuinely required, stop
unless the active prompt explicitly authorizes evaluation; justify why
BCL/existing dependencies are insufficient, identify affected
ownership/project, evaluate implications, and obtain explicit authority
before package-management mutation.

## 13. Project Reference Boundary

Preserve:

``` text
Domain          -> none
Application     -> Domain
Infrastructure  -> Application
Worker          -> Application + Infrastructure
```

Test references may evolve only as needed to test their owned production
boundary and must never normalize an invalid production dependency.

## 14. Scope Classification Before Mutation

Every candidate artifact must be classified:

``` text
AUTHORIZED
PROTECTED
UNRELATED USER WORK
GENERATED
AMBIGUOUS
```

Only `AUTHORIZED` files required by the active WP may be changed.
Preserve unrelated user work. Investigate ambiguity before mutation.

## 15. WP14 Manifest Validation

WP14 must prove: no new project; no unauthorized
root/build/script/`.github` changes; no real
provider/persistence/plugin/AI artifact; no generated output;
Domain/Application/Infrastructure/Worker changes respect ownership;
tests are in correct projects; documentation changes are relevant
current-state authority; governance artifacts are under Release 0.9
hierarchy; unexpected files are classified and resolved.

## 16. Expected High-Level Shape

``` text
AIQuantTradingResearch/
├── [existing root/build assets — protected]
├── eng/                                      # existing workflow
├── src/
│   ├── AIQuantTradingResearch.Domain/        # approved research Domain
│   ├── AIQuantTradingResearch.Application/   # contracts/use case/registration
│   ├── AIQuantTradingResearch.Infrastructure/# deterministic adapter/registration
│   └── AIQuantTradingResearch.Worker/        # thin reference execution
├── tests/
│   ├── AIQuantTradingResearch.Domain.Tests/
│   ├── AIQuantTradingResearch.Application.Tests/
│   ├── AIQuantTradingResearch.Infrastructure.Tests/
│   └── AIQuantTradingResearch.Architecture.Tests/
└── docs/
    ├── architecture/research/RESEARCH_DOMAIN_MODEL.md
    └── roadmap/release-0.9/
        ├── RELEASE_0.9_EXECUTION_PLAN.md
        ├── RELEASE_0.9_FILE_MANIFEST.md
        └── prompts/
```

## 17. File Acceptance Criteria

-   Governance artifacts live under `docs/roadmap/release-0.9/`.
-   Research-domain authority lives under `docs/architecture/research/`.
-   No new production/test project or solution file exists.
-   Accepted production graph remains intact.
-   Domain/Application/Infrastructure/Worker changes stay in owned
    boundaries.
-   Behavioral and architecture tests stay in their correct projects.
-   Existing Release 0.8 architecture protection remains.
-   Documentation changes are relevant current-state authority.
-   Historical Release 0.8 governance remains unchanged.
-   Protected root/build/scripts/`.github` remain unchanged unless
    separately authorized.
-   No new NuGet dependency appears without explicit authority.
-   No generated output is committed.
-   No real provider, persistence, plugin, AI/ML, or later-release
    artifact exists.
-   WP14 final manifest validation passes.

## 18. Conclusion

This manifest controls boundaries before filenames. WP02 deliberately
owns the important research naming/model decisions. Release 0.9 may
evolve the existing four production and four test projects, create
durable research-domain authority, preserve governance prompts, and
align relevant current-state documentation. Everything else remains
protected unless explicitly authorized.

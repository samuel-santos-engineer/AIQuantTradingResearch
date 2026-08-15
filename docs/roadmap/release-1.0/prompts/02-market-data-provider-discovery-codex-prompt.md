# Release 1.0 WP02 --- Market Data Provider Discovery --- Codex Prompt

## Role

Act as the **WP02 Market Data Provider Discovery Executor** for Release
1.0 of `AIQuantTradingResearch`.

This is a research, evidence, and architecture-decision work package.
Its purpose is to identify and select exactly one viable external
historical market-data provider for the first Release 1.0 vertical
slice, without beginning implementation.

Do not start WP03.

------------------------------------------------------------------------

## 1. Mandatory Authorities

Read completely before acting:

``` text
docs/roadmap/release-1.0/RELEASE_1.0_EXECUTION_PLAN.md
docs/roadmap/release-1.0/RELEASE_1.0_FILE_MANIFEST.md
docs/roadmap/release-1.0/prompts/01-release-repository-preflight-codex-prompt.md
docs/roadmap/release-1.0/prompts/02-market-data-provider-discovery-codex-prompt.md
```

Read GitHub issues:

``` text
#86 — WP01 — Release & Repository Preflight
#87 — WP02 — Market Data Provider Discovery
```

Read the completed WP01 execution evidence available in the current
context and verify that WP01's predecessor gate passed before
proceeding.

Read existing repository data-platform and architecture authorities that
constrain provider selection, especially provider abstraction, data
lifecycle, data quality, storage/data-pipeline boundaries, public
contracts, dependency rules, configuration, resilience, and
implementation guidance where relevant.

Authority precedence:

``` text
1. RELEASE_1.0_EXECUTION_PLAN.md
2. RELEASE_1.0_FILE_MANIFEST.md
3. GitHub issue #87 as the GitHub projection of those authorities
4. Existing repository architecture/data/engineering authorities
5. This execution prompt
```

If authorities materially conflict, stop rather than silently redesign
the release.

------------------------------------------------------------------------

## 2. Objective

Produce an evidence-backed decision for the **single provider used by
the Release 1.0 historical market-data slice**.

The decision must establish enough factual provider knowledge for
WP03--WP13 to proceed without embedding provider-specific assumptions
into Domain or Application.

At minimum determine:

``` text
selected provider
why it is selected
viable alternatives considered
free/zero-cost access feasibility
account/API-key requirements
historical market-data capability
asset/instrument coverage relevant to the release
historical interval/granularity supported
request/response mechanics at discovery level
authentication model
documented rate/usage limits where available
pagination/range limitations where available
response format
timestamp/time-zone semantics where documented
price/volume fields needed for normalization
error/failure behavior relevant to later mapping
licensing/terms constraints relevant to a public research project
known operational limitations
provider-specific facts that Infrastructure must own
provider-independent facts that Domain/Application may rely upon
```

The result must be a bounded Release 1.0 decision, not a general
market-data-platform design.

------------------------------------------------------------------------

## 3. Required Artifacts

Derive the exact authoritative paths from
`RELEASE_1.0_FILE_MANIFEST.md`.

Create only the WP02 artifacts explicitly authorized there.

The expected semantic artifacts are:

``` text
provider assessment / discovery evidence
provider decision record
```

Use the exact filenames and directories defined by the manifest. Do not
invent alternate paths if the manifest already specifies them.

If the manifest does not authorize a required artifact path clearly
enough to execute WP02, stop and report the authority gap.

Do not create implementation source or test files.

------------------------------------------------------------------------

## 4. Research Method

This work package requires current external evidence.

Use authoritative sources first:

``` text
official provider documentation
official API/reference documentation
official pricing/free-tier documentation
official authentication documentation
official rate-limit/usage-limit documentation
official terms/licensing documentation where relevant
```

Use reputable secondary sources only when official documentation does
not answer a necessary discovery question, and clearly label secondary
evidence.

Do not treat marketing claims, snippets, stale blog posts, or
undocumented community assumptions as authoritative API contracts.

Record URLs/source titles and the factual claims they support inside the
assessment artifact so the decision remains auditable.

Record the research date.

When evidence is ambiguous or unavailable, write `UNKNOWN` or explicitly
qualify the conclusion rather than guessing.

------------------------------------------------------------------------

## 5. Candidate Discovery

Do not assume the provider before research.

Identify a small, defensible candidate set suitable for the Release 1.0
objective.

Prefer candidates that can support:

``` text
zero-cost development/research
historical market data
simple HTTP-based retrieval
stable documented API
usable response schema
reasonable access from a C#/.NET application
a public portfolio/research project
a narrow first vertical slice without paid infrastructure
```

Do not optimize for every future platform capability.

The Release 1.0 provider does not need to satisfy future real-time,
institutional, exchange-direct, storage, streaming, plugin, AI/ML, or
production-scale requirements unless the execution plan explicitly
requires them.

Avoid an unnecessarily large comparison. Use enough candidates to make
the decision credible and evidence-based.

------------------------------------------------------------------------

## 6. Assessment Matrix

Create a normalized comparison covering the decision criteria required
by the authorities.

At minimum compare:

``` text
Provider
Official API/docs
Zero-cost feasibility
Account required
API key/auth required
Historical data supported
Relevant asset coverage
Intervals/granularity
Range/history limitations
Rate/usage limits
Pagination mechanics
Response format
Timestamp semantics
OHLC/price fields
Volume availability
Error model/documentation
Terms/licensing considerations
Operational constraints
.NET integration complexity
Release 1.0 fit
Evidence confidence
```

Use factual values rather than scoring theater.

If you introduce scores, the underlying facts and weighting must remain
visible and the score must not override a material hard constraint.

------------------------------------------------------------------------

## 7. Selection Constraints

The selected provider must satisfy the hard constraints defined by
Release 1.0 authorities.

In addition, prefer the smallest provider integration that demonstrates
a real external historical market-data path while keeping architecture
boundaries clean.

Selection must not require:

``` text
paid cloud services
paid market-data subscriptions for the required slice
browser automation/scraping
undocumented private APIs
embedding secrets in source
provider-specific Domain types
provider-specific Application contracts
database/storage implementation
real-time streaming infrastructure
new plugin architecture
```

If no candidate satisfies the mandatory Release 1.0 constraints, do not
select a provider merely to complete WP02. Return `BLOCKED` with the
evidence and minimum governance decision required.

------------------------------------------------------------------------

## 8. Provider Boundary Classification

For the selected provider, explicitly separate:

### Provider-independent semantics

Facts that future Domain/Application work may safely model without
naming the vendor, such as concepts already authorized by the Release
1.0 plan.

### Provider-specific mechanics

Facts that must remain owned by Infrastructure, such as:

``` text
base URL
endpoint paths
query parameter names
authentication headers/query mechanics
provider symbols
transport DTO field names
provider pagination tokens
provider status/error payloads
provider-specific timestamps
provider-specific limits
```

Do not design the actual Domain model in WP02. This classification is
evidence for WP03 and later packages.

------------------------------------------------------------------------

## 9. Representative Retrieval Feasibility

Where permitted by the selected provider and without introducing
credentials or implementation:

-   Validate the documented request shape against official
    documentation.
-   If the provider supports a credential-free public request suitable
    for the release, a minimal read-only manual/CLI probe may be used to
    confirm reachability and schema.
-   If credentials are required, do not request, create, store, or
    expose a secret merely for WP02 unless the execution-plan authority
    explicitly requires it.
-   Do not commit captured provider payloads unless the file manifest
    explicitly authorizes them.
-   Do not build a client.

Any live probe is supporting evidence only; official documentation
remains the contract authority.

Record whether live retrieval was performed, not performed, or blocked
by authentication.

------------------------------------------------------------------------

## 10. Security and Secret Handling

Never:

``` text
commit an API key
print a credential in the report
place secrets in Markdown
modify user/system environment variables
create secret files
change repository secrets
change GitHub Actions secrets
```

If documentation shows that credentials will be needed in later work,
document only the configuration concept and safe secret requirement.
Actual configuration belongs to the authorized downstream package.

------------------------------------------------------------------------

## 11. Licensing / Public-Repository Suitability

Because this is a public research/portfolio repository, explicitly
assess whether the selected provider's documented terms create obvious
constraints on:

``` text
source-code publication
API usage
display/republication of retrieved data
committing raw provider payloads
caching/storage
commercial vs research usage
attribution
```

Do not provide legal conclusions beyond the provider's published terms.
Record relevant restrictions and design implications.

If terms are unclear, say so and avoid authorizing data redistribution
by assumption.

------------------------------------------------------------------------

## 12. Repository Mutation Rules

WP02 may modify only the WP02 documentation/research artifacts
authorized by the Release 1.0 file manifest.

Do not modify:

``` text
src/
tests/
*.csproj
AIQuantTradingResearch.slnx
Directory.Build.props
Directory.Packages.props
global.json
.editorconfig
.gitattributes
eng/
.github/
existing architecture documents unless explicitly listed for WP02
Release 1.0 execution plan
Release 1.0 file manifest
existing prompts
```

The current Release 1.0 governance files may remain untracked. Preserve
them.

Do not stage, commit, push, create a branch, or create a PR.

------------------------------------------------------------------------

## 13. GitHub Protection

GitHub planning is read-only during WP02.

Do not:

``` text
edit or close issue #87
edit issue #86
edit issues #88–#101
change milestone #41
change labels
change Project fields/status
create issues
create milestones
create Release 1.1 planning
```

The existing `In Review` Project automation observation is not WP02
scope.

------------------------------------------------------------------------

## 14. Validation

After creating the authorized WP02 artifacts:

### Content validation

Prove:

``` text
candidate comparison is evidence-backed
selected provider is explicit
hard constraints are addressed
alternatives and rejection reasons are explicit
official sources are recorded
unknowns are identified
provider-independent/provider-specific boundary is explicit
no implementation design has leaked into Domain/Application
WP03 receives enough evidence to proceed
```

### Repository validation

Run:

``` text
git diff --check
```

Inspect `git status`.

If WP02 changes are Markdown-only and the Release 1.0 authorities do not
require a full build, do not run expensive unrelated validation merely
for ceremony. If the execution plan requires canonical verification for
WP02, run it exactly.

No validation command may rewrite repository content.

### Scope validation

Prove:

``` text
production changes = 0
test changes = 0
project/package changes = 0
GitHub mutations = 0
WP03 implementation = 0
provider client implementation = 0
secrets introduced = 0
```

------------------------------------------------------------------------

## 15. WP03 Handoff Contract

The WP02 decision must give WP03 a stable factual basis while leaving
WP03 free to design provider-independent Domain evolution.

At minimum the handoff should identify:

``` text
the selected provider
the market-data shape the release needs
the minimum provider-independent semantics observed
timestamp/price/volume considerations
important validation constraints
important provider limitations
what must remain Infrastructure-specific
open questions that genuinely remain for later WPs
```

Do not prescribe concrete Domain class names unless already mandated by
Release 1.0 authority.

Do not begin WP03.

------------------------------------------------------------------------

## 16. Acceptance / Exit Criteria

WP02 may be complete only when:

``` text
WP01 predecessor gate = satisfied
current authoritative sources were researched
viable candidates were compared
hard constraints were evaluated
exactly one provider was selected, or a genuine blocker was proven
selection rationale is evidence-backed
assessment artifact is complete
decision artifact is complete
official evidence is traceable
provider boundary classification is explicit
public-repository/licensing implications are recorded
no secret is exposed
no implementation was started
git diff --check = PASS
working tree is fully classified
unauthorized mutations = 0
WP03 started = NO
```

If a mandatory provider requirement cannot be satisfied, return
`BLOCKED`; do not lower the requirement silently.

------------------------------------------------------------------------

## 17. Working-Tree Classification

At the end classify non-clean state as:

``` text
EXPECTED GOVERNANCE
WP02 AUTHORIZED
EXPECTED GENERATED/IGNORED
PRE-EXISTING AUTHORIZED
UNEXPECTED
```

For WP02-authorized files, list exact paths.

For unexpected files, report whether they predated WP02 and why they
matter.

Do not stage or delete files.

------------------------------------------------------------------------

## 18. Required Execution Report

Return:

``` text
# Release 1.0 WP02 — Market Data Provider Discovery Execution Report

## 1. Executive Summary
## 2. Authorities Reviewed
## 3. Initial Repository State
## 4. WP01 Predecessor Gate
## 5. Research Method and Evidence Sources
## 6. Candidate Set
## 7. Provider Assessment Matrix
## 8. Selected Provider
## 9. Selection Rationale
## 10. Rejected Alternatives
## 11. Selected Provider Technical Facts
## 12. Provider-Independent vs Provider-Specific Boundary
## 13. Authentication / Limits / Operational Constraints
## 14. Licensing and Public-Repository Considerations
## 15. Representative Retrieval Feasibility
## 16. WP02 Artifacts Created
## 17. WP03 Handoff
## 18. Validation Evidence
## 19. Working-Tree Classification
## 20. Scope Protection
## 21. Findings / Unknowns
## 22. Exit-Criteria Assessment
## 23. Final Repository State
## 24. Final Decision
## 25. Next Authorized Action
```

For external facts, identify the official source used.

Never claim a provider capability that the evidence did not establish.

------------------------------------------------------------------------

## 19. Final Decision Vocabulary

Finish with exactly one:

``` text
RELEASE 1.0 WP02 PROVIDER DISCOVERY COMPLETE
RELEASE 1.0 WP02 PROVIDER DISCOVERY COMPLETE WITH OBSERVATIONS
RELEASE 1.0 WP02 PROVIDER DISCOVERY BLOCKED
```

Use `COMPLETE WITH OBSERVATIONS` only if all mandatory criteria pass and
remaining unknowns do not compromise WP03.

------------------------------------------------------------------------

## 20. Next Authorized Action

If and only if WP02 completes successfully, the next work package in the
Release 1.0 dependency sequence is:

``` text
WP03 — Market Data Domain Evolution
GitHub issue #88
```

Do not execute WP03.

Do not modify Domain code.

Stop after the WP02 report.

------------------------------------------------------------------------

## Execution Instruction

Read the authorities, verify the WP01 predecessor gate, perform current
evidence-based provider discovery, compare a bounded viable candidate
set, select exactly one provider only if all hard constraints are
satisfied, create only the manifest-authorized WP02 research/decision
artifacts, validate the result and repository scope, return the required
report, and stop before WP03.

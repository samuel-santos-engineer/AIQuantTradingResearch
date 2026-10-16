# C2B-10 — Merge and engineering-design evidence

Date: 2026-10-16 (America/Sao_Paulo). Documentation only.
`PUBLIC_RUNTIME_VERIFIED=False`. Public dataset/candles/indicators remain safely
unavailable; deployment and activation **NO-GO**; advanced WP07 **ON HOLD**.

## Observed guarded merge

[PR #339](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/339)
was re-read against owner-accepted head `eedcb2660e011fdcccb2a9195f5020a2229fa2ad`
and base predecessor `b1fad6255919936dc604888be57d87229a5d910e`. Its exact scope
was seven Markdown files: four canonical governance documents and three C2B-9 prompters.
Exact paths:

- `docs/architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_DEFINITION.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_EXECUTION_PLAN.md`
- `docs/roadmap/release-1.14/RELEASE_1.14_FILE_MANIFEST.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-9-canonical-governance-reconciliation-evidence.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-9-engineering-decision-backlog.md`
- `docs/roadmap/release-1.14/prompters/release-1.14-chart-mvp-c2b-9-merge-pr337-pr338-canonical-governance-reconciliation-codex.md`

Prerequisite PR #337 merge `a9d1ca05b7c5d097dd5cd094bf94fc70c4dfe4c2` and PR #338
merge `b1fad6255919936dc604888be57d87229a5d910e` were verified merged and ancestors.

Live protection checks showed enforced administrators, required conversation resolution,
zero required approving reviews, no required status checks and no additional rulesets.
Review list, check rollup and unresolved review threads were empty (review-thread
pagination exhausted). The exact reviewed patch was mergeable. Ordinary merge with
an exact-head guard succeeded without override, force, protection changes or branch deletion.

Merge commit: **`705ec39bb85f3bc85e2827c31b88c6e6a20568ce`**.
GitHub mergedAt: `2026-10-16T04:09:23Z` (`2026-10-16T01:09:23-03:00`).
Merged tree matched the reviewed head. Fresh fetch/synchronization verified clean
`main == origin/main == 705ec39bb85f3bc85e2827c31b88c6e6a20568ce`, ahead/behind
`0/0`, before creating `planning/release-1.14-chart-mvp-c2b-10` from that exact commit.
The protected relocated dirty WP07 checkout/archive was not accessed or changed.

Owner acceptance applies to that exact PR and this documentation task. Existing canonical
pending-effectiveness labels are not rewritten or interpreted as wider implementation
approval. No execution relies on resolving those labels by implication.

## Exact four-file proposal

All are new Markdown files under `docs/roadmap/release-1.14/prompters/`:

1. [Authority](release-1.14-chart-mvp-c2b-10-merge-pr339-engineering-design-decisions-codex.md)
   — attachment preserved byte-for-byte in the working copy, SHA-256
   `DBF361DF5DB5FD60A5023DC50D3D83F199A525B3A3AD31BCD44110B8CF4FD968`.
   No textual normalization; normal Git line-ending handling applies.
2. [Engineering options and owner decisions](release-1.14-chart-mvp-c2b-10-engineering-options-and-owner-decisions.md)
   — alternatives, conditional recommendations and pending approve/revise/defer E1-E8 table.
3. [Finite proof and qualification plan](release-1.14-chart-mvp-c2b-10-finite-proof-and-qualification-plan.md)
   — candidate paths, non-executable stages, exact evidence, G-B0-G-B7 mapping and TBD ceilings.
4. This merge/design evidence record.

The incoming local authority copy was preserved outside the repository before branch
creation; the attached authority is the controlling copy. No historical authority,
verbatim C2B-8 owner register, canonical C2B-9 document or implementation file is edited.

## Sources and observed facts versus unproven assumptions

Sources inspected include the canonical strategy Section 12, release definition,
execution plan and file manifest; C2B-8 owner decisions; C2B-9 engineering backlog;
C2B-7 snapshot specification; generator/calculator contracts; Infrastructure project and
friend-assembly declaration; central packages; public API/UI and Docker ownership setup.

| Observed repository fact | Design implication | Not established by this review |
| --- | --- | --- |
| Generator full-dataset/window serialization methods are internal; Infrastructure.Tests already has friendship | First finite in-memory proof can use that assembly without new production APIs | Standalone exporter seam or runtime export authority |
| Existing Microsoft.Data.Sqlite dependency, central version 10.0.11 | SQLite is a candidate for a security-only local proof, not an added package proposal | Target filesystem transaction durability, storage protection, shared-instance suitability |
| Docker setup recursively assigns `/app` and `/runtime` ownership to public `aiq` | Code/assets/pin there cannot simply be asserted immutable against that UID | Actual target mounts/UID/ancestors, protected promotion or zero-cost alternative |
| Existing API uses process-local limits and releases active slot before ASGI send | Complete-read concurrency and durable global protection need separate design/proof | Slow-reader/cancellation health protection or quota enforcement |
| Existing unavailable/error response limit 4096 bytes; separate bridge cap 2 MiB | Preserve their distinct meanings | Ready-response acceptance budget or full payload performance |
| Frozen generator identities, fingerprints, warmup and calculator semantics exist | Independent complete B1 qualification must preserve them | New B1 exact parity, derived member hashes, release pin or qualified snapshot |
| Repository inspection identifies no qualified shared transactional resource | Compare journal/SQLite with an unverified already-available resource | Claim that Azure has no resource, or selecting a paid/new service |

All Azure F1 mount/durability/multiplicity/network/identity facts remain UNRESOLVED in
this task. No target probe, browser session, provider invocation or generation was run.
Read-only source inspection is not effective kernel isolation evidence.

## Decisions preserved and decisions still pending

P1-P7, security-only persistence exception S and selected B1 are preserved. Numerical
T ceilings remain TBD. E1-E8 are recommendations only: reviewed digest pin, image-bound
ownership, conditional SQLite proof/domain, independent friend-test oracle, full visible
response design, operator lifecycle, explicit no-spawn gate mapping and staged finite proof.
None is selected as an effective mechanism by this packet. Illustrative six-endpoint
shapes and proposed canonical manifest encoding/member names are not accepted schemas.

UNRESOLVED STOPs: writable trust anchor/ancestors; target private exclusion and no-provider
capability; backend durability and authoritative topology; exact independent B1 parity;
complete-send/health/resource/retention limits; conservative accounting and quota-headroom
predicates; target/browser availability and zero incremental cost. Each has a proposed
narrow later proof stage; none permits experiments under this authority.

## Validation and limits

Fresh documentation validation passed for exact four new Markdown paths, staged whitespace,
relative links/headings, E1-E8 decision coverage, G-B0-G-B7 matrix, frozen identities,
candidate-path/non-authorization wording and preservation against the merged base.
Relative links resolve, all E1-E8 decisions remain pending and all eight gates are mapped.
Authority byte comparison matched the attachment. Tracked files outside these four paths
have no diff against the merged base, preserving canonical and owner-register content.
The final staged-scope checks are also recorded in the PR validation report.
No production hash/pin/key/schema/artifact is created.

**Inherited, not rerun:** Python 162 passed / 11 skipped; .NET 620 passed; C2B-7
16 focused local tests. No new tests/builds/restores, generation/export, Docker,
Azure, networking experiments, native descriptor/process/privilege tests or load/fault
measurements were authorized or executed for the design packet. GitHub reads, the
guarded PR #339 merge and this new documentation PR are the authorized lifecycle actions.

The C2B-10 PR must remain open and unmerged for owner review. Acceptance of documents,
static comparisons or a future offline proof cannot establish runtime isolation, kernel
waits, native closure, production size enforcement or public readiness. Further source
changes, snapshot production/export/promotion, deployment and activation require distinct
owner authority. `PUBLIC_RUNTIME_VERIFIED=False`; all public runtime gates remain NO-GO.

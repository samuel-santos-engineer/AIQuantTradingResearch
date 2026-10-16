# C2B-11 — Owner decision registration evidence

Date: 2026-10-16 (America/Sao_Paulo). **Documentation only.**
`PUBLIC_RUNTIME_VERIFIED=False`; public dataset/candles/indicators remain safely
unavailable (`503 DATASET_NOT_READY` for valid unqualified requests). Implementation,
snapshot generation/export/promotion, integration, target qualification, public activation
and Azure deployment remain **NO-GO**. Advanced WP07 remains **ON HOLD**.

## Guarded merge and clean baseline

[PR #340](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/340)
was verified OPEN/MERGEABLE at accepted head
`a633cb2966383cf5efa6166fed7dadd22a99f6bb`, base
`705ec39bb85f3bc85e2827c31b88c6e6a20568ce`, with exactly the four accepted new
C2B-10 Markdown files. Fetched head matched local accepted content; patch scope remained
unchanged. No PR content was updated to obtain a merge.

Live protection findings: administrators enforced, conversation resolution required,
required approving reviews zero, no required status checks, no additional rulesets.
Reviews and status-check rollup were empty. Review threads were empty, with pagination
exhausted. An initial GraphQL shell-quoting error was corrected using a structured JSON
request; the successful query supplied conversation evidence before merging.

Ordinary merge with exact-head guard completed without bypass, force, protection changes,
branch deletion or issue closure. Final state MERGED; merge SHA
**`fda6b1b3df0bcaf2ada1f50f72d1d050605d88f6`**;
mergedAt `2026-10-16T04:30:24Z` (`2026-10-16T01:30:24-03:00`).
An explicit main fetch and fast-forward verified clean
`main == origin/main == fda6b1b3df0bcaf2ada1f50f72d1d050605d88f6`, ahead/behind
`0/0`, and a merged tree identical to the reviewed head. A default fetch had populated
FETCH_HEAD without a matching remote-tracking branch; that absent ref was not used as
evidence. The subsequent explicit main fetch supplied the verified baseline.

New branch: `planning/release-1.14-chart-mvp-c2b-11`, created only after clean-main
verification. No access to the relocated protected WP07 dirty checkout or archive.

## Immutable owner source and chronology

Owner source was the existing untracked local file at:
`C:\projects\github\AIQuantTradingResearch\docs\roadmap\release-1.14\prompters\release-1.14-chart-mvp-c2b-11-engineering-owner-decision-register.md`.
It was safely located in this checkout; provenance was corroborated against the owner's
attached decision register at
`C:\Users\sabsf\.codex\attachments\4fd25c4f-51d9-4634-9d3b-9ff974b2d840\Pasted text.txt`.
Differences are nonsemantic Markdown table/whitespace formatting and correction of the
duplicated display label `#340#340`. No owner choice or condition differs.

The local register and pre-existing local authority were moved to the newly created
`C:\Users\sabsf\AppData\Local\Temp\aiq-c2b11-preserved` holding directory before
clean-main verification, then copied back only after creating the new branch. Destination
absence and byte hashes were checked; originals remain preserved in that directory.
No untracked file was silently overwritten or discarded.

| Preserved file | SHA-256 before move and after restore |
| --- | --- |
| Owner register | `EA32C9664F1AC7FF386ABD89D934C9FF8DE76F5C043174413642532A4FE6735D` |
| C2B-11 authority | `D730AC82904E5D3EB253740217A8BCF75564580EE5F72D4853369C031BC2AD61` |

The authority matches the new attachment's meaning, with disclosed nonsemantic whitespace,
table formatting and duplicate `#340#340` display-label normalization. Its pre-existing
bytes were preserved. Git's normal line-ending handling may normalize repository blobs.
No textual changes were made to either local source during this task.

The [register](release-1.14-chart-mvp-c2b-11-engineering-owner-decision-register.md)
historically says PR #340 was open/unmerged and decisions alone did not authorize merge.
That dated wording remains intact. The later explicit acceptance and separate
[C2B-11 authority](release-1.14-chart-mvp-c2b-11-merge-pr340-register-decisions-codex.md)
authorized the guarded merge; no retroactive authority is inferred from conditional E choices.

## Conditional decisions and outstanding evidence

All eight owner selections are **Approve recommendation (conditional)**; comments **None**.
The mapping below records design direction, not mechanism acceptance or feasibility.
Sources: [C2B-10 options](release-1.14-chart-mvp-c2b-10-engineering-options-and-owner-decisions.md),
[finite proof plan](release-1.14-chart-mvp-c2b-10-finite-proof-and-qualification-plan.md),
and C2B-9 [canonical strategy Section 12](../../../architecture/business/PUBLIC_PREVIEW_AND_PRIVATE_RESEARCH_DATA_STRATEGY.md).

| Item | Conditional direction | Outstanding condition / proof; no gate passed |
| --- | --- | --- |
| E1 | Reviewed digest pin, deterministic manifest core, no compression/initial signing secret | Accept trusted revision, producer/reviewer, actual schema and threat assumptions; no pin/key/manifest created |
| E2 | Image-bundled code/pin/assets not serving-UID writable; whole-image promotion/rollback | Actual UID, ancestors, mounts, immutable release consistency and Azure behavior unproven |
| E3 | Security-only SQLite local proof candidate, protected-journal comparator, single-instance domain to verify | No effective production backend/topology/ownership/retention or paid service selected; durability unproven |
| E4 | Existing friend test assembly with independent test-only CPython integer/rational oracle | Exact complete three datasets/twelve windows, values/nulls/fingerprints unverified; no production Python port |
| E5 | Complete fixed-end windows, no pagination/truncation; illustrative version-bound response direction | Actual ready schemas, maximum bytes, full-send/cancellation and resource ceilings unapproved |
| E6 | Operator whole-image promotion/revocation/eligible rollback; separate minimal fail-closed control recovery | No automatic fallback/reset/repair; retention/capacity/atomicity/recovery budgets TBD |
| E7 | Dedicated no-spawn/no-provider public reader and applicable G-B0-G-B7 negative evidence | No blanket WP07 waiver; target private exclusion/isolation and zero-added-cost F1 feasibility unproven |
| E8 | Future first finite in-memory friend-test/oracle proof | Candidate paths/commands only; exact scope, independent oracle provenance and numeric safety ceilings require separate authority; no exporter/materialization/integration |

P1-P7, S and B1 remain unchanged. T is **TBD — measurement required**. No numerical
limit is invented and no approved design satisfies its own evidence obligation. G-B0-G-B7
retain the C2B-10 proposed mapping and C2B-9 policy requirements; no gate passage is claimed.
Historical C2B-8/9/10 records and canonical files remain unchanged.

## Documentation checks and PR boundary

### Owner-authorized four-line whitespace exception

The owner's continuation authority dated 2026-10-16 expressly preserves four intentional
two-ASCII-space Markdown hard breaks at owner-register lines 3, 4, 5 and 6. Normal
`git diff --cached --check` exits 2 with exactly those four trailing-whitespace findings;
this is NOT an overall clean whitespace result. The other two exact staged paths pass
their independent whitespace check (exit 0). No Git whitespace setting is changed.

A byte-aware read of the independent preserved original, working copy and staged Git
blob confirms all three are identical, including LF line endings and terminal newline.
Each affected line has exactly two terminal ASCII spaces before LF; no other register
line has trailing whitespace. SHA-256 remains
`EA32C9664F1AC7FF386ABD89D934C9FF8DE76F5C043174413642532A4FE6735D`.
The register is not normalized or edited. This is solely a documentation exception.

One additional untracked owner-supplied continuation document,
`release-1.14-chart-mvp-c2b-11-four-line-whitespace-exception-continuation-codex.md`,
was observed before resuming. It is preserved unchanged and excluded from staging,
commit and PR; no fourth path is authorized. Final status must disclose this retained
untracked file rather than claim a clean checkout.

Exactly three new Markdown paths under `docs/roadmap/release-1.14/prompters/`:

1. `release-1.14-chart-mvp-c2b-11-engineering-owner-decision-register.md`
2. `release-1.14-chart-mvp-c2b-11-owner-decision-registration-evidence.md`
3. `release-1.14-chart-mvp-c2b-11-merge-pr340-register-decisions-codex.md`

Documentation-only validation checks staged whitespace (`git diff --check`), exact literal
scope, relative links/headings, source hashes and absence of changes outside this list.
The PR creation report records the new PR URL and final head SHA against base
`fda6b1b3df0bcaf2ada1f50f72d1d050605d88f6`; final commit identity cannot be embedded
in its own committed evidence without a circular reference. PR metadata and final report
provide that immutable association. The new PR must remain **open and unmerged**.

**No tests run.** Python 162 passed / 11 skipped, .NET 620 passed and C2B-7 16 focused
tests are inherited only, not fresh. No restore/build/test/generator/provider, active
network experiment, load/fault/native process/FD/privilege, Docker, Azure or browser
commands executed. Authorized Git transport/GitHub governance reads and PR lifecycle
actions are distinct from prohibited runtime network experiments.

No Azure/isolation/parity/durability/resource evidence is produced. Future executable
proof needs separate exact paths, independent verification, finite CPU/RSS/wall/disk/log/
output/repetition/shutdown ceilings, cleanup and abort authority. Export/promotion,
integration, target/browser qualification, deployment and activation need further distinct
authority. Unknown mandatory evidence fails closed. `PUBLIC_RUNTIME_VERIFIED=False`.

# Release 1.15 WP01 - Engineering contract closeout assessment

Date: 2026-10-22. Issue [#367](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/issues/367), milestone #66. Recommendation: **WP01 BLOCKED**. This is a documentation-only stop report for owner review, not acceptance or authority to implement unresolved contracts. `PUBLIC_RUNTIME_VERIFIED=False`.

## Baseline and authority

Preflight found local main behind GitHub and one untracked owner prompt. With explicit owner authority, preserved that prompt outside the checkout, verified SHA-256 `e73c07e785955d9f31784e6e6579e3c49757429cdebf0b200b17beedf67a3f14`, and fast-forwarded main without overwriting tracked work. The publication branch starts at `3880f5afee59e3d970ceacd6c3e9abfd796a0105`, matching origin/main. PR [#378](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/378) is merged at `455b80d0e50c17791653457a4ebd3001d3629a6e`; feasibility PR [#379](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/379) at the current baseline. Authenticated repository permissions verified. Issues #367-#374 are all OPEN in milestone #66; dependencies agree with the [execution plan](RELEASE_1.15_EXECUTION_PLAN.md).

The closeout authority permits these new assessments and indexing documentation only. On discovering foundational ambiguities, stop contract freezing and dependent execution rather than choose undocumented values. This PR reports those blockers; it does not resolve them by decree. Root AGENTS.md, [LF policy](../../governance/REPOSITORY_LF_LINE_ENDINGS_POLICY.md), [architecture](../../governance/HISTORICAL_CRYPTO_CHART_AND_RESEARCH_INTERFACE.md) and [Docker policy](../../governance/DOCKER_FIRST_LINUX_RELEASE_POLICY.md) remain controlling. No governance amendment is made.

## Four distinct kinds of evidence

| Category | Meaning here | Current finding |
| --- | --- | --- |
| Architecture decision | Owner-selected direction and inherited constraints | Historical educational BTC/USD and ETH/USD; private research/public transformed chart; one Linux/amd64 image; components-v2 preferred for viewport |
| Feasibility demonstrated | Bounded observations on named fixtures/images | S1 browser mechanism and corrected cold-start cases supported; S2 partially supported |
| Production contract frozen | Exact accepted semantics sufficient for independent implementations | No complete D1-D7 production decision is newly frozen by this assessment; existing invariants remain binding |
| Downstream qualification outstanding | Proof of a separately implemented production contract | Assigned to one primary WP per obligation in the [matrix](RELEASE_1.15_WP01_DOWNSTREAM_QUALIFICATION_MATRIX.md); not demanded as WP01 runtime acceptance |

The [decision register](RELEASE_1.15_WP01_DECISION_REGISTER.md) uses one explicit status for each complete decision. D6 PROVISIONAL does not reopen the accepted mechanism direction or initiate alternative-technology research: the production adapter/protocol remains unqualified and depends on D4/D5.

## Architecture reconciliation

Preserve actual completed UTC one-minute BTC/USD and ETH/USD candles, at most 1,000 retained per instrument. Twelve Data Basic is the initial configurable candidate; measure actual credits, symbols, completeness and recovery capability rather than assume them. No fabricated gap rows or public raw quotes. Private SQLite is the authoritative single writer, with atomic publication and lineage/recovery evidence; research uses raw private values. Public prices and price-valued indicators are transformed educational values, not actual quotes.

A minimal trusted supervisor manages distinct non-root research and Streamlit children in one immutable Linux image. Research hosts the read-only loopback API; Streamlit alone serves public ingress and cannot read private files, sidecars, checkpoints, watermarks, credentials or keys. A loopback listener alone is not process/filesystem confidentiality. Root-supervisor trust remains an explicit decision, not a permission inferred from S2.

Preserve 100/300/1,000 retained-candle views, default 300, nominal 60-second refresh, independent status dimensions and user-controlled Live/Historical modes. No licensing-specific engineering prerequisite is added; independent legal obligations are unchanged. Release 1.14 history, existing security gates, default application and readiness remain untouched.

## S1 evidence reconciliation

Authoritative report: [component evidence](prompters/release-1.15-wp01-component-feasibility-evidence.md). Its appended sections supersede earlier verdicts for their tested source; historical FAIL/INCOMPLETE observations remain valid history. The earlier [native report](prompters/release-1.15-wp01-viewport-feasibility-evidence.md) did not establish native relayout forwarding. The [component proposal](prompters/release-1.15-wp01-supported-viewport-component-proposal.md) was subsequently authorized; its old proposal label is not a current rejection.

| Evidence section in component report | Supported observation | Limit |
| --- | --- | --- |
| Loading proof before implementation; Actual viewport evidence | Supported components-v2 owns its Plotly element; trusted locally loaded 4,815,814-byte bundle, no CDN/global native-chart assumption; actual candles/volume/hover/modebar and 26 browser cases | Fixture chart, not production UI; pinned Streamlit 1.61.1 / Plotly 7.1.0 |
| Residual verification; Debounce, refresh, acknowledgment and detachment results | Two pending-intent loss vectors reproduced; stale-event overwrite and loss of pending intent distinguished | Earlier failures are not retrospectively counted as passes |
| Pending-intent correction - final review, 2026-10-22; Correction and ordering contract; Pending gesture races and exact ranges | Capture manual intent at relayout, retain across refresh/remount, rebind existing revision to current identity/command, commit before ack; nine command races with 72-107 ms release/input bounds; explicit reset fences | Finite schedules; no arbitrary network reordering or production transport-size proof |
| Final bounded resource screen | 30 refresh/pan cycles, six remounts, 45.122 s; one relayout/two resize handlers; retained heap +296,836 bytes | Not long-soak, combined private-service load or F1 capacity |
| Bounded cold-load investigation - reproduced, 2026-10-22 | Real two-session pandas/Plotly startup failure reproduced; standalone controls did not reproduce | Recovery reload did not establish reliable startup |
| Authorized cold-load correction and regression - 2026-10-22 | Existing pandas import completed before fixture UI/Plotly; six fresh-container pairs/12 sessions with no reload, 36 focused tests on each host/Linux runtime and nine browser smoke checks | Same 36 tests on two runtimes, not 72 distinct tests; no universal import-schedule or production-reliability proof |

S1 preserves absolute UTC x and applicable price/volume y ranges. Instrument, generation/version/display date, mount token, interaction revision, command epoch and separate explicit-reset epoch order intent. Programmatic restores do not invent gestures. Ordinary changed/unchanged refresh cannot silently discard a pending manual gesture; Return to Live and preset changes intentionally reset. Corrections/date/generation changes remount/reconcile eligible intent; partial retention preserves overlap, full expiry keeps the historical view with honest expiry messaging. Instrument/session state is independent. Two browser contexts verify tested isolation.

The bounded sessionStorage handoff is a feasibility implementation, not a frozen production storage/security policy. Denial/corruption, abandoned-session reclamation, real scheduler disconnect/reconnect and production schema/cache integration remain downstream obligations. The fixture's parsed-event 4,096-byte serialization check is not request-stream enforcement.

## S2 evidence reconciliation

Authoritative report: [runtime evidence](prompters/release-1.15-wp01-runtime-feasibility-evidence.md), sections Executed profile and boundary observations; Startup, interruptions and resource samples; Remaining recovery and security obligations.

S2 demonstrated PID1 UID 0 with distinct UID/GID 12001 research and 12002 UI children, empty supplementary groups and child effective capabilities 0. The public identity was denied tested SQLite/checkpoint/watermark/canary and selected process-channel reads. Research bound 127.0.0.1; UI bound its internal public port. Four supervisor policy tests passed. One interruption per fixed-role child showed reaping before replacement and independent peer health; three starts used the same container/volume with warm filesystem cache. Raw memory peak was 215,736,320 bytes under the local 512 MiB limit. None of these are new results from this documentation PR.

Root PID1 retained default capabilities and is not an accepted least-privilege production trust model. Fixed-role restart does not prove arbitrary descendant closure or complete recovery. Local DELETE-journal integrity and ordinary file replacement do not establish crash-safe lineage or Azure locking/fsync/rename durability. S2 did not execute 1/3/5 real-browser combined-load capacity tests; S1's two UI sessions do not fill that gap. Azure F1 remains an unqualified target, with no SLA.

## WP01 exit recommendation

**WP01 BLOCKED.** The original [exit criteria](RELEASE_1.15_EXECUTION_PLAN.md) require baseline/contract/acceptance reconciliation and authorized feasibility evidence. Baseline and bounded S1/S2 evidence now exist, but a foundation for safe independent implementations remains incomplete:

1. D1: approve a concrete supervisor trust/privilege/lifecycle boundary before WP02 runtime implementation; do not silently adopt fixture root PID1/default capabilities.
2. D3: settle unapproved numerical definitions, persistence/replay state and independent-vector specification before affected calculation contracts are frozen.
3. D4: settle canonical wire spelling, encoding/precision, complete bounds, zero-row and error semantics. A 1,000-row fake endpoint is not this foundational cross-language interface.
4. D5: settle exact derivation/mapping/rounding and rotation/transform-epoch identity so distinct transformed arrays cannot share accepted cache identity.

D2 storage mechanisms and D6 production integration also need the register's scoped dispositions; D7 actual platform measurements remain downstream qualification. These are not excuses to demand completed production implementation inside WP01. Conversely, assigning a missing foundational interface to a later WP does not satisfy WP01 contract closeout. The 2026-10-23 amendment supplies exact proposed values in a separate review specification; none is approved or labeled FROZEN.

Owner review should decide the register's questions in an explicit documentation/contract disposition, then reassess closeout. This assessment does not accept WP01, close #367 or begin WP02. WP08 accepts evidence/runbooks after implementation qualification; it cannot implement or waive open technical work.

## Documentation validation and publication boundary

The initial four-document assessment is supplemented on 2026-10-23 by the authorized detailed proposal specification and owner prompt. Current amendment scope is the same four documentation/index paths, the new specification and the explicitly owner-authorized prompt; no other path changes. The existing acceptance checklist stays Pending and the issue register's dependencies stay intact. Relative file/heading links, one D-status/owner per decision, one primary owner per matrix row, exact path scope, UTF-8/LF and git diff --check are checked before publication. Historical reports, source/tests/Docker/governance, README and other releases are not edited. Product tests/builds/browser/Docker/provider/Azure operations are NOT RUN for this documentation-only PR. A non-draft review PR is an assessment publication, not merge or release authority.

Historical validation of the initial assessment: exact four-document inventory PASS; 40 relative Markdown targets resolved; seven status/owner pairs consistent; 22 unique primary-owner qualification rows and all 12 release acceptance IDs present; manifest append-only; 29 tracked prompt/evidence/fixture files byte-identical to baseline. Independent UTF-8/LF/no-BOM/final-LF scan and git diff --check PASS. Staged blobs are independently checked again before commit. These are documentation checks, not new product test results.

## Foundational proposal amendment - 2026-10-23

Under new documentation-only authority, the [foundational proposal specification](RELEASE_1.15_WP01_FOUNDATIONAL_CONTRACT_PROPOSALS.md) now supplies concrete D1-A/D3-A/D4-A/D5-A candidates, alternatives/tradeoffs, independent conformance requirements and an owner decision sheet. The owner additionally authorized inclusion of the previously untracked [decision-resolution prompt](prompters/release-1.15-wp01-codex-foundational-contract-decision-resolution.md); wording preserved with UTF-8/LF normalization. This explicit scope addition is not an automatic expansion of source authority.

D1-A proposes retained-root PID1 with sharply reduced capabilities and child restrictions, independent role closure and no restart on uncertain death; this trust choice is not accepted merely because S2 used root. D3-A specifies exact integer seeds/rounding/formulas/calibration/replay. D4-A specifies full/unchanged/empty/error wire fields, decimal encoding, parser/stream bounds and atomic acceptance. D5-A specifies HMAC bytes/mapping/rounding, unchanged volume and positive public transformEpoch counter for rotation. D4/D5 identity is the same generation/version/displayDate/transformEpoch tuple; private keyEpoch never enters public DTOs.

**Recommendation remains WP01 BLOCKED pending explicit owner acceptance/amendment of the four candidates.** This is progress from unspecified choices to reviewable proposals, not approval, a production contract freeze or passed implementation. The register's statuses and all22 qualification owners remain unchanged. Actual durability/UI/cloud proof remains downstream; WP08 cannot implement/waive it. No new application tests, cryptographic/vector execution, Docker/browser experiments, provider/Azure operations or deployment were run. Only documentation/reference validation applies. PR#380 remains open/unmerged, #367 open, WP02 unstarted and `PUBLIC_RUNTIME_VERIFIED=False`.

## Contract correction and owner review - 2026-10-23

Responding to [PR #380 review](https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/380#issuecomment-6364065366), the proposal now records an independent derivation of population variance and preserves the correct `4*A` versus `400*(2*k+1)^2` rule. Exact-square, midpoint-neighbor and true tie windows, flat/rising/falling seeds, ATR14/15, EMA/MACD and prior-only candle401 regime comparisons were calculated using inline standard-library integer arithmetic without product imports. ATR14=0.928571428571 for either unit ramp is retained. These are reviewed documentation calculations, not executed product regression tests or approved golden fixtures.

Minimum valid raw OHLC remains positive after transformation: one unit rounds to one throughout factors0.90..1.10. Initial-empty status is distinguished from a successful poll that returns no rows; literal unchanged and explicit version/rotation/midnight/stale-response identities agree. Private raw data, factors and keys remain excluded. Already committed in-flight replies cannot be recalled; no buffer may commit an obsolete tuple after activation.

Retained-root PID1 is an explicit pending trust decision, with each capability justified. pidfd signaling, child wait ownership, proc/thread visibility, seccomp/clone3 compatibility and no_new_privs limitations are stated. Two empty UID scans are only screening evidence, not a complete descendant-closure proof. The independent closure witness remains unresolved: WP02 must qualify one or disable replacement/fail closed, and WP07 must verify actual F1 facilities. No new helper, privilege fallback, cgroup assumption or platform compatibility is approved.

The owner authorized inclusion of the [correction prompt](prompters/release-1.15-wp01-codex-contract-correction-and-owner-review.md) after the untracked-file preflight stop; wording is preserved as UTF-8/LF. This pass modifies only the proposal, register, closeout and manifest index plus that prompt. All22 qualification rows/primary owners and all12 pending acceptance rows remain unchanged. D1/D2/D6/D7 PROVISIONAL; D3/D4/D5 OPEN; WP01 BLOCKED; #367 OPEN; WP02 unstarted. Product tests/builds/browser/Docker/provider/Azure/native experiments and HMAC digest generation NOT RUN. `PUBLIC_RUNTIME_VERIFIED=False`; PR #380 remains open/unmerged for explicit owner disposition.

Correction validation: exact five-path inventory PASS; 52 relative Markdown targets/anchors and four JSON examples PASS; seven status/owner pairs unchanged; qualification matrix byte-identical with22 rows and12 Pending acceptance mappings; acceptance/risk files unchanged; manifest append-only; owner prompt wording matches the attachment (existing blank-line layout differs). Independent changed-file UTF-8/no-BOM/LF/final-newline scans and git diff --check PASS. A first validation assertion compared attachment blank-line formatting literally and was corrected to compare preserved wording; this was a documentation-check issue, not a product failure. Staged blobs are separately checked before commit. No product tests were run.

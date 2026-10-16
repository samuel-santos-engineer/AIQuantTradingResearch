# C2B-13 — E4 resource and owner-decision worksheet

Date: 2026-10-16. **All numbers below are candidates, not measured or owner-approved.**
`PUBLIC_RUNTIME_VERIFIED=False`; public data unavailable; deployment/activation NO-GO;
advanced WP07 ON HOLD. This is not test/harness creation or execution authority.

## Finite safety contract proposal

Rationale for numeric proposals is a deliberately finite small batch, not a prediction
of performance. Reject/revise them before authority if not credible for the selected host.
CPU/RSS/commit, process inventory and generated build outputs lack sufficient static basis
or qualified enforcement, so remain TBD **blocking**. No run until every mandatory bound
and its enforcement/overshoot is independently reviewed and accepted. Release T, REST
bytes, F1 quotas/headroom and production budgets remain separate TBD values.

| Dimension | Candidate / not measured / not approved | Rationale, enforcement and STOP |
| --- | --- | --- |
| Preflight wall | 60 seconds | Fixed governance/toolchain validation window; no work launched on incomplete prerequisites |
| Build wall | 180 seconds | Finite provisional operator stop, not expected build speed; root/helpers controlled from launch |
| Comparison wall | 120 seconds per full pass | Complete three datasets/twelve selections; no extra time/retry on mismatch |
| Negative/edge wall | 120 seconds total | Enumerated scope below, not an unbounded property/fuzz loop |
| Total session wall | 660 seconds including cleanup | Sum of 60+180+240+120+60; independent watchdog must bound coordinator; unavailable watchdog STOP |
| Aggregate CPU | TBD — blocking | Include all descendants and coordinator overhead; user+kernel measurement/abort semantics unresolved |
| Peak RSS and aggregate commit | TBD — blocking, separately defined | Job commit limit does not equal RSS; sampling overshoot and native limits must be qualified |
| Process count | One oracle at a time; total tree count TBD — blocking | Actual .NET/MSBuild/VSTest/testhost/server/helper graph unobserved; unexpected child STOP |
| Concurrency | One session, one phase/selection and one oracle frame in flight | Explicit runner/test parallelism restriction; no simultaneous copies or retries |
| Passes | Two full positive passes total | One comparison plus one deterministic repeat; no automatic retry; all twelve windows each |
| Edge/negative scope | Exactly 40 named cases below, once each | Review exact inputs/assertions before source creation; no skip/helper-level false success |
| Frame body | 256 KiB maximum; header 4 bytes | Chunk rows; check declared bytes before allocation; exact encoded worst-case still needs review |
| Aggregate oracle stdout | 32 MiB/session | Finite provisional stream cap for exact canonical+analytics chunks; reject excess, no spill to disk |
| Oracle stderr | 16 KiB/session | Bounded concurrent drain; sanitized category only persisted; overflow aborts |
| Transport buffers | 1 MiB aggregate including parser/copy allocations | Provisional cap, not total RSS; inability to account parser copies blocks approval |
| Evidence/log | 64 KiB each for two JSON summaries; 256 KiB runner.log; 384 KiB aggregate | Counters/categories/revisions only; no vectors; reserve before writing, fail on overflow |
| Temp/build/disk/file count | TBD — blocking | Exact project-generated inventory and bounded external mapping unresolved; no broad directory permission |
| Cancellation grace | 2 seconds cooperative, then 5 seconds final termination verification | Included within phase/session envelope; no unbounded WaitForExit; unknown membership STOP |
| Cleanup wall | 60 seconds within total session | Exact owned outputs only; independent termination first; failure denies repeat |
| Failure evidence retention | At most 7 days, no subsequent run until disposition | Candidate operator retention, not automatic deletion of unknown files/state; no retention beyond approved bytes |

Phase deadlines and total deadline both apply; final-wait grace must fit their envelope,
not extend it. No numeric table entry claims kernel enforcement. If measured overshoot
or unavailable control defeats a ceiling, fail closed; polling alone is not proof of a
hard bound. Cancellation itself requires future qualification, not an experiment now.

## Fixed 40-case supplemental inventory

Positive full comparisons cover every row of three datasets and twelve calculation/visible
selections on each of two passes, as specified in C2B-12. The following are separate,
finite supplemental cases. Exact literals/expected rejections require independent review
before implementation; no fixture/vector is generated here. A case reaching only a helper
exception is a failure of the test design, not successful validator rejection.

| IDs | Once-only cases |
| --- | --- |
| A01–A10 | Constant; rising; falling; alternating; mixed; positive even/odd midpoint ties; negative even/odd midpoint ties; sqrt below/at/above midpoint as one finite three-vector case; large intermediates/maximum valid close; minimum valid precision |
| N01–N05 | Empty input; null row; zero close; negative close; close above 10^9 |
| N06–N10 | Excess precision; >2410 inputs; gap; duplicate timestamp; reversed order |
| N11–N15 | Non-UTC/off-grid time; calculation start/count mismatch; invalid interval; invalid symbol/range; stale/nonclosed provenance |
| N16–N20 | Wrong source/context; wrong generator/seed; wrong units/coverage/instrument; dataset/window fingerprint mismatch; synthetic/private mixture or non-null candle CalculationVersion |
| F01–F05 | Oversize frame length; truncated header/body; duplicate/unknown key; wrong frame order/selection/count; extra frame or aggregate-byte overrun |
| C01–C05 | Pre-cancel; bounded in-progress cancel; oracle nonzero exit; oracle timeout; uncertain descendant termination produces blocked-session result |

Grouped labels are finite reviewed composite cases, not parameter spaces or open fuzzing.
The implementation proposal must expand A01–A10 into ten exact named test cases in the
listed order (ties split into positive and negative cases; sqrt's three vectors remain
one case). Rejection combinations need exact paired input mutations and assertion targets
reviewed so later predicates are not masked by an earlier failure. If more cases or native
fault tests become necessary, request separate scope instead of silently expanding 40.
Cases C/F describe future owned local test behavior only; this table authorizes no native
or runtime fault injection. First-valid samples remain SMA/EMA/Bollinger20, RSI15,
MACD26, signal/histogram34; all earlier nullable alignment must match.

## Approval gates and owner choices

| Gate | Current status / exact owner decision needed | STOP if absent |
| --- | --- | --- |
| Accepted C2B-12 planning | Exact PR #342 accepted and merged; proposal only | No inferred execution authority |
| C2B-13 containment direction | Approve/revise/defer launch-time Job Object candidate versus independently qualified existing runner | No creation/assignment experiment; unsupported/uncertain boundary stays blocked |
| C2B-13 limits | Approve/revise/defer numeric candidates; supply/require defensible CPU/RSS/commit/process/output bounds and watchdog proof | All TBD mandatory cells block execution |
| Literal tools/output | Accept exact executable versions/identities, offline caches/environment, build-output file mapping and candidate root ownership | No PATH fallback, implicit restore or directory wildcard rights |
| Independent oracle | Owner assigns independent numerical/spec reviewer and accepts provenance/review requirements before comparison | No fabricated reviewer/hash/golden; insufficient independence blocks proof |
| Test-file creation | Separate explicit authority for only the two candidate files after exact edge/negative serialization review | Design approval alone creates no test files |
| Harness creation, if needed | Separate literal runner/helper authority, preserving Windows PowerShell 5.1 compatibility | Proposed runner path is not permission; no helper project inferred |
| Executable local E4 proof | Separate bounded commands/operations, enforced ceilings, offline/private-free context, abort/cleanup and output authority | No run before qualified containment and complete allowlist |
| Independent evidence acceptance | Review all bytes/values/nulls/identities, negatives, resources, termination and preservation | Static plan or mutual agreement is not accepted parity |
| Later E1/E2/E3/E5-E8 | Separate actual trust/schema/ownership/accounting/domain/response/lifecycle/isolation qualification | E4 success does not discharge these obligations |
| Export/integration/target/deploy/activate | Each distinct authority after applicable qualification and owner acceptance | No inherited permission or advanced WP07 resumption |

Immediate owner request: approve/revise/defer the containment proposal and finite candidate
limits; provide or require design closure for remaining mandatory bounds, exact toolchain/
output paths, and assign an independent oracle reviewer. Then request separate test-file
and, if needed, runner creation authority; executable proof is still a later decision.

# Release 1.15 WP01 - Foundational contract proposals

Date: 2026-10-23. **PROPOSALS FOR OWNER CHOICE; NOT APPROVED OR FROZEN.** Refs #367 and existing PR #380. Baseline `f3cf2787d7bb53bcaba236d67b41531d0cb52fde`, parent main `3880f5afee59e3d970ceacd6c3e9abfd796a0105`. Read with the [decision register](RELEASE_1.15_WP01_DECISION_REGISTER.md), [closeout](RELEASE_1.15_WP01_CONTRACT_CLOSEOUT.md) and [qualification matrix](RELEASE_1.15_WP01_DOWNSTREAM_QUALIFICATION_MATRIX.md).

Every exact value, formula, bound, deadline and wire spelling below is a proposed resolution unless explicitly called inherited. Within a proposed contract, must/shall defines the candidate to review, not current implementation authority. No code/test/vector generator is run to evaluate it; illustrative mathematical values are not test passes. All new/modified text is UTF-8/LF. `PUBLIC_RUNTIME_VERIFIED=False`; WP01 remains BLOCKED, #367 OPEN and WP02 unstarted.

## Owner decision sheet

| Choice | Exact proposal | Alternative / tradeoff | Decision required |
| --- | --- | --- | --- |
| D1-A | Trusted retained-root PID1, sharply reduced capabilities, separate capability-free non-root roles, role-UID descendant census plus process groups/pidfds, fail-closed restart | Unprivileged PID1 plus privileged role-launch helper reduces main supervisor privilege but adds another privileged boundary/service mechanism; reject silent helper/capability additions | Accept/amend/reject D1-A; explicitly accept retained-root trust and fail-closed availability cost |
| D3-A | Scale 10^12 integer arithmetic, specified seeds/population bands/Wilder rules, type-7 percentile and exact replay | Decimal/floating arithmetic or different seeds/percentiles change vectors, restart results and classification boundaries | Accept/amend/reject the complete numerical policy, including flat-series Undetermined |
| D4-A | Schema 1.0, bounded JSON POST query, decimal strings, explicit zero/full/unchanged/error shapes, atomic validation | JSON binary numbers simplify payloads but lose cross-runtime decimal/counter exactness; GET conditional headers complicate complete request identity | Accept/amend/reject wire, error, compatibility and proposed size limits |
| D5-A | Exact daily HMAC mapping and rounding; public monotonic transformEpoch separate from private keyEpoch; unchanged volume | Generation change on every rotation simplifies identity but falsely couples data lineage to presentation keys; scaling volume distorts base-asset units | Accept/amend/reject derivation, unchanged-volume policy and durable epoch coupling |

Decisions are not inferred from PR merge or acceptance of feasibility. Owner may amend individual clauses, requiring cross-document D4/D5 identity consistency before acceptance. D2 durability remains WP03 with actual Azure evidence WP07; D6 remains components-v2/local Plotly, not a technology search; D7 remains best-effort Linux/amd64 Azure F1. WP08 consolidates final acceptance/runbook evidence, not implementation work.

## D1 proposed runtime contract

### D1 inherited invariants and evidence

Inherited: one immutable Linux/amd64 image, minimal PID1, private C# research and public Streamlit as distinct non-root roles; private loopback API, private storage/secrets denied to public identity, bounded restart and no overlap. No privileged deployment, extra paid service, cgroup delegation assumption or advanced Release 1.14 guardian revival.

[Runtime evidence](prompters/release-1.15-wp01-runtime-feasibility-evidence.md), Executed profile and boundary observations and Remaining recovery and security obligations, proved limited UID/file/loopback/fixed-role restart observations. It did not approve root PID1/default capabilities, close arbitrary escaped descendants, prove complete lineage recovery, or measure actual F1 capability/mount behavior. This proposal is more restrictive than that fixture and has not run.

### D1-A privilege, files and launch sequence

1. Require PID1 in a dedicated container PID namespace and reserved role identities: research UID/GID 12001, public UID/GID 12002, no supplementary groups. If IDs are already occupied by unrelated tasks, /proc role census is unavailable or required kernel operations are denied, fail startup. No host PID namespace, privileged container or capability additions as fallback.
2. Capability names here abbreviate CAP_ names. Trusted bootstrap initially limits its effective/permitted capabilities to CHOWN, FOWNER, DAC_OVERRIDE, SETUID, SETGID, SETPCAP and KILL. Before any child exec, initialize/verify only the configured private mount and per-role scratch directories using no-symlink traversal. `/private` is research-owned 0700; DB/journal/WAL/checkpoint/watermark/config files are 0600, umask 0077; public scratch is disjoint 0700. Do not recursively chown unknown mounts or repair unverifiable ownership by widening permissions.
3. Then drop bootstrap-only powers permanently. PID1 remains UID/GID 0 with effective/permitted/bounding sets **exactly SETUID, SETGID, SETPCAP, KILL**, empty inheritable/ambient sets and no_new_privs=1. SETPCAP is retained specifically so each new child can empty its own bounding set before exec; it is not permission to add capabilities. No SYS_ADMIN, SYS_PTRACE, DAC_OVERRIDE, CHOWN, NET_ADMIN or NET_RAW at runtime. Verify every thread's sets; UID 0 is still trusted and able to impersonate roles using SETUID. This is an explicit owner trust decision, not an unprivileged-supervisor claim.
4. PID1 uses fixed executable/argument/role definitions, never user-supplied commands, shell strings or recovery HTTP commands. Each forked child creates its role session/process group, empties supplementary groups and capability bounding/inheritable/ambient sets while privileged, sets all real/effective/saved GID/UID values to its role, and clears all remaining capabilities. Set PID1/research dumpable=0. Before fixed exec, each child additionally installs an inherited x86-64 seccomp filter: reject other syscall architectures and x32 syscall numbers; deny unshare/setns/ptrace/process_vm_readv/process_vm_writev with EPERM; deny clone flags for NEWUSER/NEWPID/NEWNS/NEWNET/NEWIPC/NEWUTS/NEWCGROUP; return ENOSYS for clone3 (its flags live behind a pointer). Ordinary fork/clone/thread operations otherwise remain subject to kernel/container policy. Verify .NET/Python threading fallback and fail qualification rather than remove this restriction if incompatible. Assert no_new_privs=1 and empty effective/permitted/bounding sets before fixed exec. Any failure aborts that launch; do not execute with partial privilege reduction.
5. Close all inherited FDs >=3, including private file handles and peer sockets, before exec; FD0 is /dev/null, FD1/2 dedicated bounded per-role log pipes. No supervisor control socket or descriptor is inherited. Research reads `/private/config/runtime.json`; public has no path traversal/read permission. Trusted PID1/config loader may see secrets; that trust is declared. Clearing a language environment map is not claimed to erase a kernel initial environment or memory.
6. Construct both environments from empty maps. Common allowlist: PATH=/usr/bin:/bin, LANG=C.UTF-8, TZ=UTC and a role-private HOME. Research additionally gets AIQ_RESEARCH_CONFIG=/private/config/runtime.json and DOTNET_EnableDiagnostics=0; public gets AIQ_RESEARCH_API=http://127.0.0.1:52731 and STREAMLIT_BROWSER_GATHER_USAGE_STATS=false. Credentials/HMAC material reside only in private config or research memory, never public env, argv, logs or assets. Any future variable requires a reviewed allowlist amendment; no blanket Azure app-settings inheritance to public.
7. Research binds only 127.0.0.1:52731 and offers only bounded transformed read queries/health. Public binds 0.0.0.0:8501 inside the container. No raw-data/admin/write API or external private listener. Loopback is a network boundary, not role authentication: public is intentionally allowed to query transformed snapshots, never private raw storage or privileged commands.

### D1-A termination, restart and peer independence

Process groups are the first termination target, not complete ownership proof: an unprivileged descendant can fork/setsid away. Reserve each role UID solely for that role for the entire container lifetime. Before launch and after role failure, census all tasks of that UID in the namespace, not only the recorded PGID. Each census is bounded to256 tasks and the closure deadline; exceeding either is ContainmentFailed, not a truncated success. Track stable task identity with pidfds plus start-time/UID checks; do not signal a reused numeric PID or infer death from kill success. If any task changes to an unexpected identity or cannot be attributed/observed, disable replacement and enter containment failure, not a guessed restart.

Launch role state machine: Stopped -> Starting -> Running -> Stopping -> Stopped, or ContainmentFailed. Only one role incarnation may occupy Starting/Running/Stopping. Record root PID, pidfd, PGID and monotonic launch sequence before health admission. Health probe every 30 s, 2 s per probe, startup grace 90 s; three consecutive post-grace failures initiate stop. Research and UI have independent health/backoff counters. A UI failure does not stop healthy research; research unavailability leaves UI serving truthful disconnected/last-known state.

On stop, prohibit replacement, TERM the recorded role group and all positively identified role tasks. Give 10 s graceful time, then KILL remaining known tasks; spend at most 5 additional seconds on rescan/reap. PID1 drains wait results for its direct/adopted children without SIGCHLD=SIG_IGN or NOCLDWAIT, associates exit evidence with the role and rescans for escaped same-UID tasks. Replacement requires root exit/wait evidence and two complete role-UID-empty scans >=100 ms apart, no pending launch and all observed role children reaped. It never equates an empty PGID with an empty role. No complete census/death proof by deadline means ContainmentFailed: no same-role replacement, no availability fallback; terminate the container after bounded peer shutdown if scope/ownership is uncertain.

Unexpected root exit immediately enters the same closure protocol. After confirmed closure, delays are 1,2,4,8,16,30,30... seconds; reset the delay index only after five continuous healthy minutes. More than ten failures in any rolling ten minutes disables that role until container restart (UI displays unavailable if research disabled). At most one scheduled replacement per role, no unbounded queue. Container TERM/INT disables all replacements and gives both roles the same parallel 10+5 s closure window; exit 0 only after verified clean shutdown, nonzero on failure. External platform hard-kill may interrupt this protocol: persistent recovery still belongs to WP03, not a promise of graceful shutdown.

No-overlap here is within one verified container PID namespace. PID1 failure admits no local restart; actual container recreation/old-instance termination and single-writer exclusion across deployment overlap/replicas require WP02/WP03/WP07 proof. A nominal single F1 instance is not evidence that rolling old/new containers cannot overlap. No public activation until private writer ownership is exclusive.

Use fail-closed startup if pidfds, role census, capability reduction, ownership/permissions or role signal/wait authority cannot be verified on the target kernel. No writable-cgroup dependency is proposed. User-namespace creation must be denied by the child filter, not assumed blocked by no_new_privs: a new user namespace can confer capabilities inside that namespace. Same-UID public descendants must not switch role/UID or acquire privilege in the container namespace; this assumption requires adversarial proof, not merely a configuration comment. PID1/native launcher post-fork behavior must be safe for its chosen runtime; no language/toolchain selection or new helper dependency is authorized here.

### D1 alternatives, owner choice and conformance

Owner choice: D1-A explicitly trusts a minimized retained-root supervisor and accepts service unavailability when closure cannot be verified. An unprivileged supervisor would need a separate reviewed privileged launcher/termination boundary to manage two UIDs; it is not silently interchangeable. Group-only cleanup is rejected because it cannot cover setsid escape. A cgroup-only design is not assumed feasible on F1.

WP02 independently tests exact caps/UIDs/groups/FDs/env/private-sidecar denials, setuid/file-capability escalation attempts, fork/setsid descendants, pid reuse, interrupted launch, failed census/signal/wait, parent death, peer isolation, crash-loop bounds and no-overlap. Tests use fake secrets and a real Linux image; observed child death must be independent of supervisor logs. WP07 separately checks Azure PID namespace, /proc visibility, capability/seccomp policy, mount permissions, stop grace and actual resource limits. Missing platform facilities keep activation NO-GO; no deployment or capability exception is approved here.

Kernel mechanisms were checked read-only against primary documentation: [capabilities](https://man7.org/linux/man-pages/man7/capabilities.7.html) for per-thread sets/bounding behavior, [no_new_privs](https://www.kernel.org/doc/html/latest/userspace-api/no_new_privs.html) for exec privilege constraints, [setsid](https://man7.org/linux/man-pages/man2/setsid.2.html) for group escape, [pidfd signaling](https://man7.org/linux/man-pages/man2/pidfd_send_signal.2.html) for stable signal targets, and [wait](https://man7.org/linux/man-pages/man2/waitpid.2.html) for reaping. The [kernel seccomp guide](https://www.kernel.org/doc/html/latest/userspace-api/seccomp_filter.html) explains syscall-argument filtering and pointer limits; [user namespaces](https://man7.org/linux/man-pages/man7/user_namespaces.7.html) explains namespace-local capabilities. These references support mechanism reasoning, not a tested Azure configuration. Seccomp is not claimed to be a complete sandbox.

## D3 proposed deterministic research contract

### D3 inherited invariants and scalar arithmetic

Inherited: private raw completed OHLCV only; SMA20, EMA20, Bollinger20x2, Wilder RSI14, MACD12/26/9 and ATR14; no fabricated gaps, 100 warmup plus 300 prior valid calibration observations, first eligible classification candle 401, no lookahead. Source: architecture section 6 and engineering proposal section 3. WP04 owns numerical conformance; WP03 consumes the checkpoint-state interface.

D3-A proposes S=10^12. Parse raw prices as exact decimal multiples of 1/S, 0 < price <=10^9; volume in base-asset units is 0..10^12 with <=12 fractional digits. Reject unsupported precision/units instead of guessing a conversion or fabricating volume. OHLC relationships: 0 < low <= open,close <= high; completed UTC minute timestamps strictly increase, one observation per instrument/minute. The newest opening plus60 seconds cannot exceed the trusted observation time. Integer units and arbitrary-precision integer intermediates are mandatory; no binary float. Prices enter unrounded; derived outputs round only at the stated recurrence/output steps.

Define R(a,b), b>0: round signed rational a/b to nearest integer, ties to even, symmetric about zero. Canonical zero has no minus sign. Examples: R(1,2)=0, R(3,2)=2, R(-1,2)=0, R(-3,2)=-2. Every stored indicator/dimensionless output is an integer in units 1/S. Out-of-contract bounds reject/quarantine the candle transaction, not clamp values. Do not use display-transformed values as recurrence inputs.

### D3-A indicator initialization and update order

Segment index t starts at 1. Closing-price integer p[t] is the price basis except true range. All unavailable outputs are null. Compute in this order: validated candle -> rolling price/TR/gain/loss state -> each indicator -> trend/features -> regime using prior calibration only -> append current valid feature to the bounded calibration ring -> transition events -> atomic persistence -> publish. No reader sees partially advanced state.

| Output | Exact proposed definition | First non-null |
| --- | --- | --- |
| SMA20 | R(sum of last 20 closes,20) | t=20 |
| EMA(N), N=12,20,26 | Seed R(sum first N closes,N); later R(2*p[t]+(N-1)*EMA[t-1],N+1), each step rounded once | t=N |
| Bollinger20 middle/upper/lower | middle=SMA20. A=20*sum(p^2)-(sum p)^2 over last20; sd is nearest-even sqrt(A)/20 in price units; upper=middle+2*sd, lower=middle-2*sd | t=20 |
| Wilder RSI14 | Changes t=2..15; gain=max(delta,0), loss=max(-delta,0); seed each mean with R(sum,14). Later each mean=R(13*previous+current,14). RSI=R(100*S*meanGain,meanGain+meanLoss) | t=15 |
| RSI zero cases | Both means zero ->50*S; only loss zero ->100*S; only gain zero ->0 | Same |
| MACD line | EMA12-EMA26, signed | t=26 |
| MACD signal9 | Seed R(sum MACD[t=26..34],9); later R(2*MACD+8*previousSignal,10) | t=34 |
| MACD histogram | MACD-signal | t=34 |
| ATR14 | TR[1]=high-low; later max(high-low,abs(high-previousClose),abs(low-previousClose)). Seed R(sum TR[1..14],14), then R(13*previousATR+TR,14) | t=14 |

For Bollinger sd, k=floor(sqrt(A)/20) by integer search; compare 4*A with 400*(2*k+1)^2. Lower chooses k, higher k+1; equality chooses even k or k+1. Never take an approximate floating square root. Population variance is selected; sample variance is the alternative, not silently interchangeable. Negative lower bands and signed MACD outputs are valid.

A missing minute begins a new continuity segment; TR for its first candle uses high-low, not a previous-segment close. A correction triggers replay from a sufficiently early valid checkpoint or complete segment origin. Insufficient evidence starts a new segment and truthful recalibration; do not approximate a seed from only the visible 1,000 rows. Duplicate identical data is idempotent, conflicting duplicate follows correction protocol and increments committed version. Out-of-order acquisition is staged/replayed by WP03, not fed into a forward recurrence unchecked.

### D3-A trend, normalization and regime

Compute private trend votes when EMA20, MACD histogram and RSI14 are available (t>=34): close>EMA ->+1, close<EMA ->-1, equality 0; histogram positive/negative ->+1/-1, zero 0; RSI>55 ->+1, RSI<45 ->-1, inclusive 45..55 ->0. Sum is -3..3; >=2 Bullish, <=-2 Bearish, otherwise Neutral. Scores can be retained privately before regime readiness; public classification/score fields remain null until classification is valid.

Dimensionless integer features: normalizedAtr14=R(ATR14*S,close); bollingerWidth=R((upper-lower)*S,middle); emaSlope5=R((EMA20[t]-EMA20[t-5])*S,5*EMA20[t-5]). Slope first exists at t=25; denominators positive because raw closes are positive. Threshold slope uses abs(emaSlope5), alignment uses its sign. Ratio units, not percentage points; RSI alone uses 0..100. Intermediates are arbitrary precision; finite feature magnitudes must be <=10^24, otherwise invalid research and recovery, not saturation.

For t=401 use exactly feature samples 101..400; at general t>=401 use t-300..t-1. First100 are warmup only; calibration never includes current t. All 300 samples must be consecutive, valid and from the same segment/algorithm. A gap/invalidated interval without exact reconstruction resets continuity/readiness; it cannot be skipped to retain a misleading 300-sample calibration.

For Pq, q=40,70,80, sort 300 feature integers ascending. Type-7 rule: z=299*q; j=floor(z/100), r=z mod100; percentile=R((100-r)*sorted[j]+r*sorted[j+1],100), using zero-based indices. Volatile if current normalized ATR > its prior P80 AND width > its prior P80. Else Trending if abs(score)>=2 AND abs(slope)>prior P70(abs slope) AND score/slope signs agree strictly. Else Ranging if abs(score)<=1 AND abs(slope)<prior P40(abs slope). Else Undetermined. Threshold equality does not pass a strict comparison. Before t401 or on missing evidence, trend/regime/score public classification is unavailable, not Undetermined; Undetermined is a valid eligible result.

### D3-A events, checkpoint and independent vectors

At each segment's first actual candle (segmentIndex1) emit SegmentStart with null trend/regime. At first valid classification emit TrendTransition with previousTrend null; later emit only on Bullish/Bearish/Neutral change, with previous/new trend and current regime. Regime-only change does not create a trend marker. Identity is generation/instrument/segment/time/type. Retain an event while its own candle is retained even after its predecessor is pruned. Replay reconstructs/replaces affected events/validity, never blindly appends; invalidate stale research during correction recovery.

Checkpoint state must include algorithm version, segment UUID and index, last opening timestamp/previous close, last20 closes, for each EMA12/20/26 its seeded flag, pre-seed sum/count and last value; Wilder gain/loss/ATR seeded flags, current means and pre-seed sums/counts, MACD signal seed sum/count, last six EMA20 values, prior300 feature triples with validity/timestamps, last valid trend and lineage/commit checksum. Private persisted integer values use exact decimal integer encoding. Snapshot/prune does not reseed this state. Replay under the same version must be bit-exact (zero integer tolerance); an algorithm change requires explicit versioned replay/segment policy. WP03 owns flush/cadence/lineage/crash durability, not this proposal's acceptance.

WP04 must author a separate C# reference independent of production and literal mathematical vectors, before production golden expectations. Required families: flat, rising, falling, alternating/mixed, extreme valid magnitudes, ties/square-root midpoint boundaries, null positions, gaps/corrections, checkpoint restart/pruning and 400/401. Hand-derived anchors (proposed expectations, NOT RUN): flat O=H=L=C=100, volume1 gives SMA/EMA/Bollinger100, ATR0, RSI50, MACD/signal/histogram0 at their stated starts; candle400 unavailable, candle401 Neutral/Undetermined (strict flat slope 0<0 is false). Rising close=high=low=open=t gives SMA20/EMA20=10.5 at20, RSI100 at15, MACD7 at26 and signal7/histogram0 at34; ATR14=0.928571428571 at14. Falling close=1000-t gives RSI0 at15. Verify anchors independently rather than copy calculator output.

Owner choice: accept D3-A population variance, first-bar ATR, midpoint rounding, RSI50 flat case, type-7 percentiles, private pre-readiness score/public null policy and exact replay. Alternatives change results and must carry explicit owner edits/vector updates. No Release 1.14 hour-grid calculator or its fixed-point identities is silently reused as the one-minute contract.

## D4 proposed canonical wire contract

### D4 inherited invariants and proposed boundary

Inherited: bounded, whole-response validated, versioned read-only loopback API; actual retained rows, full/unchanged, generation/version/display-date identity, independent status dimensions, no raw/secret fields, no mixing snapshots. WP05 owns wire/conformance; WP06 consumes it. Everything more specific in D4-A is a proposal, including the limits below. No schema file or API is implemented by this document.

D4-A uses POST `/v1/research/snapshot` at 127.0.0.1:52731 as a read-only query (no ingestion/storage/config mutation). No query parameters, range fetch or public proxy route. Content-Type application/json; charset absent or utf-8, UTF-8 only, no BOM. Content-Encoding identity only, including responses: no decompression loophole. Only POST is supported (405 otherwise). Connection health is separate and returns no raw/config data. Client total connect+write+read deadline is 5 s, server processing budget 4 s; slow chunks do not reset deadlines. No immediate retry; normal 60-second reconciliation resumes. One in-flight request and one coalesced pending tick per instrument/session; viewport interaction never fetches.

### D4-A scalars, schema and bounds

| Scalar | Exact proposed form |
| --- | --- |
| schemaVersion | ASCII `1.0` emitted; consumer accepts `1.m` for integer m=0..99 with unchanged required semantics; breaking change requires major 2 and is rejected by this client |
| instrument | Exactly BTC/USD or ETH/USD |
| generation, segment | Canonical lowercase UUID text 8-4-4-4-12, nonzero; new generation for new dataset/verified rollback, persistent across ordinary restart |
| version, requestEpoch, transformEpoch | Unsigned counters as ASCII decimal strings, `0` or nonzero digit followed by digits, <=9223372036854775807; no plus, leading zero, exponent or JSON numeric form; transformEpoch >=1 |
| displayDate | Valid Gregorian `YYYY-MM-DD`, UTC transformation day, year 2000..2099 |
| candle/event/interval time | Valid UTC opening/boundary `YYYY-MM-DDTHH:mm:00Z`, year 2000..2099; exact minute, no offsets/leap seconds |
| observation/commit/poll time | Valid UTC `YYYY-MM-DDTHH:mm:ss.ffffffZ`, exactly six fraction digits, seconds 00..59 |
| decimal quantity | ASCII string `-?(0|[1-9][0-9]*)` plus literal dot and exactly 12 digits; no negative zero/plus/exponent/NaN/infinity; abs <=10^24 globally; null only where specified |
| integer/bool | Only explicitly listed small JSON integers or booleans; bool is never a counter/score |

Field-specific bounds: transformed OHLC 0..4*10^9 strictly positive; every price-valued indicator abs<=4*10^9 (negative Bollinger lower/MACD allowed), volume 0..10^12 base-asset units unchanged, RSI 0..100, normalized features abs<=10^24. These are candidate parser/domain ceilings, not measured provider guarantees. Reject unrepresentable/unsupported input, do not clamp or cast through double. D3 calculation bounds are private and distinct from transformed output bounds.

Proposed hard bounds: request 4,096 UTF-8 body bytes; success response 4 MiB (4,194,304) bytes; error body 4,096 bytes; nesting depth 8 (root object depth1); 32 members per object; 64 UTF-8 bytes per key, 256 per string value (decimal <=64 ASCII bytes), 250,000 JSON lexical tokens per success body, 2,048 elements per otherwise unknown array. Known arrays obey stricter limits: candles/analytics/classifications <=1,000 each; events <=2,000; researchUnavailableIntervals <=1,000; marketDataGapIntervals <=999. No truncation to meet a bound. Four MiB replaces the earlier unapproved 2 MiB candidate, not an inherited governance value; WP05 must validate adequacy and WP07 memory cost before acceptance.

Enforce byte limits while reading the stream before complete allocation/decoding; reject declared oversize early, but independently count actual chunks even without Content-Length. Validate UTF-8 incrementally, reject malformed sequences, duplicate keys at every depth (including ignored fields), trailing documents, invalid escapes, token/depth/member/string limits and unsupported media encoding. Number lexemes at every depth are <=64 ASCII bytes; unknown optional numbers are parsed as exact finite decimal rationals with exponent -24..24 and abs<=10^24, not coerced to double. Known small-integer fields require an integer lexeme with no exponent/fraction/negative zero. Reject unpaired Unicode surrogate escapes as well as malformed raw UTF-8. Do not read an unbounded string then validate size. Server serializes the complete whitelisted DTO within the response bound before sending success; client stages bounded validated immutable state then replaces cache/session atomically. A partial network body never alters accepted data.

Consumer ignores bounded unknown optional response fields for compatible 1.x, including their contents after bounded parsing; missing required or malformed known fields reject the whole response. Server emits only the whitelist below, never arbitrary model extensions. Request fields are exact; unknown request fields reject. Known full-only fields are forbidden, even null, in unchanged/error. Empty and missing differ. UUID/counter/decimal types are not inferred from JSON coercion. No dynamic HTML or payload logging from unknown fields.

### D4-A request and shared objects

Request exact fields: schemaVersion, instrument, requestEpoch, knownIdentity. knownIdentity is null for first/forced-full request, otherwise exactly the identity object below. An empty object is invalid. Every retry/tick gets a strictly newer per-instrument requestEpoch; older replies are discarded before acceptance. Epoch overflow ends that session request stream; do not wrap. Wrong instrument is never accepted.

```json
{"schemaVersion":"1.0","instrument":"BTC/USD","requestEpoch":"1","knownIdentity":null}
```

Identity exact fields: generation (UUID), version (counter), displayDate (date), transformEpoch (counter >=1 from D5). The identity is the full four-field tuple; an unchanged request matches all four, not only version/date. Cache keys additionally include schemaVersion, instrument and both contract algorithm names. Versions do not decrease within a generation; transformEpoch and displayDate do not decrease within a generation. New generation requires validated full replacement and lineage-data clearing, preserving only eligible viewport/preferences. Same identity with differing arrays/contracts is a collision, rejected and reported without publishing mixed data. Different fresh status/observedAt/requestEpoch values are allowed under unchanged data identity. Shared caches contain only immutable validated data/contract collections: never per-session requestEpoch, connectivity, health/status observations, accepted viewport or preferences; each response computes status afresh.

Contract exact fields: researchAlgorithm=`aiq-r115-research-v1`, transformAlgorithm=`aiq-r115-transform-v1`, intervalSeconds=60 (JSON integer). Algorithm names bind all D3 periods/seeds/readiness/formulas and D5 derivation; they cannot silently change under compatible schema. No optional user parameters in this version. A change to required numerical meanings requires an explicit version/compatibility decision and new fixtures.

Status exact fields:

| Field | Type / semantics |
| --- | --- |
| observedAt | Observation timestamp, exactly equals envelope observedAt |
| research | Ready, Recalibrating, Recovering or Unavailable |
| freshness | Current, Stale or Unavailable |
| integrity | Protected, Degraded or Recovery |
| classificationValidity | Valid, Invalidated or Unavailable for latest row |
| lastCompletedCandleAt | Last retained candle opening or null if no rows; full must equal last candle time; unchanged must equal cached last candle |
| lastSuccessfulProviderPollAt | Observation timestamp or null; success alone does not establish freshness |
| lastCommitAt | Observation timestamp or null for no commits |
| researchAsOfCandleAt | Latest retained valid classification time or null |

Freshness Current requires a validated last completed candle whose closing time is <=observedAt and age from that close is <=120 s; >120 s is Stale; no candle/untrusted clock is Unavailable. A close at12:01 and observation12:03 is Current; 12:03:00.000001 is Stale. No future candle, poll or commit times. Full displayDate equals envelope observedAt UTC day; do not replace old arrays under a new date without full transformation. A backward trusted-date/epoch within a generation is rejected, not relabeled current. Ready requires valid eligible latest classification; integrity Recovery requires research Recovering and non-Valid latest classification. Degraded watermark protection may coexist with independently validated Ready research as allowed by WP03's accepted lineage protocol. Server never supplies session connectivity: client derives Connected/Reconnecting/Disconnected, last successful sync and age separately. Error/timeout does not invent new status observations.

### D4-A response shapes and collections

All success responses require schemaVersion, type, instrument, requestEpoch, observedAt, identity, contract and status. full additionally requires exactly six collections: candles, analytics, classifications, events, researchUnavailableIntervals, marketDataGapIntervals. unchanged forbids all six. Status is fresh even if data unchanged; a changed key epoch/date/version/generation requires full. HTTP 200 only for validated full/unchanged. No 304 body ambiguity.

| Collection / object | Exact fields and consistency |
| --- | --- |
| candles | Rows {time,segment,segmentIndex,open,high,low,close,volume}; all required/non-null; decimal strings. Strict increasing unique minutes, 0<low<=open,close<=high; no fabricated gaps. segment UUID changes at continuity starts; segmentIndex is a counter string >=1, increasing by one inside a segment, resets to1 at a new segment. The first retained index may exceed1 after pruning |
| analytics | Exactly one row per candle, matching time/order: {time,sma20,ema20,bollingerLower,bollingerMiddle,bollingerUpper,rsi14,macd,macdSignal,macdHistogram,atr14,normalizedAtr14,bollingerWidth,emaSlope5}; each numeric field decimal string or null according to D3 seed/validity, never missing |
| classifications | One per candle, matching time/segment: {time,segment,validity,trendScore,trend,regime}; validity=Valid/Invalidated/Unavailable. Valid requires candle segmentIndex>=401 and all required analytics non-null; score JSON integer -3..3, trend Bullish/Bearish/Neutral matching score thresholds, regime Volatile/Trending/Ranging/Undetermined. Non-Valid requires all three null, not stale research |
| events | {time,segment,type,previousTrend,trend,regime}; type SegmentStart or TrendTransition. SegmentStart has three null classification fields; TrendTransition agrees with Valid classification at its own candle, prior trend enum or null only for first eligible classification. At most one event of each type per candle; sorted by time then SegmentStart before TrendTransition, unique tuple identity. Events never outside retained candle times |
| researchUnavailableIntervals | {start,end}, merged half-open union of retained non-Valid classification candle spans [time,time+60s). Merge touching/overlapping spans; do not cover missing minutes as if they were known rows |
| marketDataGapIntervals | {start,end}, exactly missing minute spans between adjacent retained openings >60s apart; merged/nonoverlapping; no inferred gap before first or after last retained candle |

Intervals sorted by start and bounded by first retained opening and last opening+60s. Collections replace completely, not append. Gaps require a new continuity segment at the following actual candle; a correction/reset can start a new segment without a time gap. Transition predecessor pruning does not delete a still-retained event. The consumer validates structural/membership/status consistency but never recomputes research from rounded transformed values.

Zero-row full is HTTP200 only when a valid empty dataset identity/transform configuration exists: version0, all six collections empty, research/freshness/classificationValidity Unavailable, last candle/poll/commit/research timestamps null, integrity Protected for a verified initialized empty DB. Missing/unverifiable keys/storage use error, not a false empty dataset. There is no fabricated seed candle. Full with 1..999 actual retained rows is valid with honest readiness; 1,000 is a cap, not a requirement to hide startup history.

### D4-A illustrative positive and negative vectors

The following empty full is an illustrative wire/validation vector, not output of an executed API or D5 computation:

```json
{"schemaVersion":"1.0","type":"full","instrument":"BTC/USD","requestEpoch":"1","observedAt":"2026-10-23T12:01:15.000000Z","identity":{"generation":"11111111-1111-4111-8111-111111111111","version":"0","displayDate":"2026-10-23","transformEpoch":"1"},"contract":{"researchAlgorithm":"aiq-r115-research-v1","transformAlgorithm":"aiq-r115-transform-v1","intervalSeconds":60},"status":{"observedAt":"2026-10-23T12:01:15.000000Z","research":"Unavailable","freshness":"Unavailable","integrity":"Protected","classificationValidity":"Unavailable","lastCompletedCandleAt":null,"lastSuccessfulProviderPollAt":null,"lastCommitAt":null,"researchAsOfCandleAt":null},"candles":[],"analytics":[],"classifications":[],"events":[],"researchUnavailableIntervals":[],"marketDataGapIntervals":[]}
```

Positive unchanged vector: take the literal empty full above with an already accepted exact identity, change type to unchanged, requestEpoch to2 and both observedAt values to `2026-10-23T12:01:16.000000Z`, and remove all six collection keys. It must update status/connectivity without redraw. With no accepted identity, that same reply is invalid and schedules a full request on the next normal tick, not an immediate retry.

Positive one-candle vector: use the same envelope, version1, candle time `2026-10-23T12:00:00Z`, segment `22222222-2222-4222-8222-222222222222`, segmentIndex `1`, OHLC each `100.000000000000`, volume `1.000000000000`; analytics at that time with all thirteen outputs null; classification Unavailable with score/trend/regime null; one SegmentStart; researchUnavailableIntervals `[12:00:00Z,12:01:00Z)`; gaps empty. Set research Recalibrating, freshness Current, lastCompletedCandleAt=12:00, lastCommitAt and lastSuccessfulProviderPollAt=12:01:15 with six fractional digits, researchAsOf null. This vector tests accepted startup shape and does not assert these transformed numbers arise from the test HMAC key.

Independent negative mutations (one at a time, expected whole-response rejection): empty full missing analytics; unknown compatible optional root field over byte bound; duplicate identity.version keys; version JSON number or `01`; transformEpoch0; known same identity but changed candles; unchanged carrying candles:null; requestEpoch not current; wrong instrument; local-offset/non-minute candle time; NaN or numeric decimal; null required OHLC; mismatch collection length/time; 1,001 candles; 2,001 events; interval outside retained bounds or falsely spanning a gap; trendScore=true; Valid classification before segment candle401; lastCompleted newer than cached rows on unchanged; stale display date/new observation day; rotated transformEpoch with unchanged type. Compatible bounded unknown optional response fields alone must remain accepted. Broken UTF-8, depth/member/token overflow and trailing JSON must fail at parsing before publication.

### D4-A error envelope and conformance

Error exact fields: schemaVersion, type=error, instrument, requestEpoch, observedAt, error={code,message,retryAfterSeconds}. Echo instrument/requestEpoch only if parsed/validated; otherwise null. observedAt uses trusted UTC or null if time is untrusted. No identity/contract/status/collections or stack/internal paths/raw values. message is the fixed string `Request unavailable.` for all error codes (ASCII, <=64 bytes), never exception text. retryAfterSeconds is null except bounded JSON integer 1..60 permitted for RATE_LIMITED or DATA_UNAVAILABLE; the client still has no immediate-retry loop.

| HTTP | code |
| --- | --- |
| 400 | MALFORMED_REQUEST |
| 405 | METHOD_NOT_ALLOWED |
| 413 | REQUEST_TOO_LARGE |
| 415 | UNSUPPORTED_MEDIA_TYPE |
| 422 | UNSUPPORTED_SCHEMA, UNSUPPORTED_INSTRUMENT, INVALID_IDENTITY or INVALID_EPOCH |
| 429 | RATE_LIMITED |
| 503 | DATA_UNAVAILABLE, RECOVERY_IN_PROGRESS or UNTRUSTED_TIME |
| 504 | DEADLINE_EXCEEDED |
| 500 | INTERNAL_ERROR or RESPONSE_TOO_LARGE |

```json
{"schemaVersion":"1.0","type":"error","instrument":"BTC/USD","requestEpoch":"2","observedAt":"2026-10-23T12:01:16.000000Z","error":{"code":"DATA_UNAVAILABLE","message":"Request unavailable.","retryAfterSeconds":null}}
```

HTTP error, malformed/oversized response or timeout preserves the complete prior snapshot and last-sync timestamp; mark connectivity Disconnected, with reconnecting only while a later scheduled request is active. Do not accept a positive body under a non-200 status or expose raw response text. Invalidated research is not inferred from a transport outage alone. A syntactically valid error is still not snapshot data.

WP05 must separately author independent C#/Python fixtures for every shape/bound/type/identity/status/interval/error case, with identical decisions and byte-stream negatives split across chunk boundaries. Tests must observe no partial session/cache publication; serialization must whitelist fields. Owner choice is required for schema-minor compatibility, POST read-only transport, fixed-decimal strings, limits/error meanings and all zero-history rules. Actual adequacy/resource enforcement remains Q12/Q15/Q21, not a documentation test pass.

## D5 proposed daily transform and rotation contract

### D5 inherited invariants and exact derivation

Inherited: research uses raw private OHLCV; transform only after private calculations; one positive UTC-day factor shared by both instruments, approximately0.90..1.10, with transformed educational disclosure. Scale OHLC/all price-valued indicators consistently, not RSI; date rollover replaces arrays. Do not expose raw prices, secret keys or factor. WP05 owns D5 and the D4 identity amendment; WP03 supplies durable config/epoch lineage. HMAC transformation is not an anonymity, rights or confidentiality substitute for role isolation.

D5-A proposes researchAlgorithm `aiq-r115-research-v1` and transformAlgorithm `aiq-r115-transform-v1`, S=10^12 and D3 signed ties-even R. The private key is exactly32 random bytes, stored as canonical padded base64 in research-only config; never obtain it from public requests. Private keyEpoch is a nonzero canonical lowercase UUID changed whenever key bytes or derivation policy changes. It is private metadata and not serialized into public status, errors or logs.

HMAC-SHA256 message is the UTF-8/ASCII byte concatenation below, with literal LF separators and a final LF, no BOM, spaces or locale formatting:

```text
message = ASCII("AIQ-R115-TRANSFORM-V1") || 0A || ASCII(displayDate) || 0A || ASCII(privateKeyEpoch) || 0A
```

Here `||` denotes byte concatenation, `0A` one LF byte and the quotes/operators are not message bytes. No blank lines are inserted. Neither instrument, market-data generation nor candle time enters the message: both instruments share the same factor for the active keyEpoch and UTC displayDate. Use displayDate, not date extracted independently from every historical candle.

Let D be the unsigned big-endian integer of all32 digest bytes (0..2^256-1). Compute factor numerator F as:

```text
F = 900000000000 + floor(D * 200000000001 / 2^256)
factor = F / 1000000000000
```

This gives inclusive endpoints0.900000000000..1.100000000000 in steps10^-12; no modulo reduction or binary float. It is a specified discrete mapping, not a claim of perfectly uniform bounded sampling. For each private price-valued integer x, transformed units=R(x*F,S). Output decimal strings with exactly12 fractional digits. Transform raw candle OHLC, SMA/EMA/Bollinger, ATR and MACD line/signal/histogram, including signed/negative lower-band values. Preserve quantized OHLC ordering; do not recalculate research from transformed rounded outputs.

RSI, normalized ATR/width/slope, scores, trend/regime/validity and timestamps are unchanged. **Volume is unchanged**, remains actual base-asset units (BTC or ETH) in12-place decimal strings, not quote-price units; unsupported/unknown provider volume units are an input capability failure, never fabricated zero or silent scaling. This explicit volume policy needs owner acceptance. No field transmits F, private keyEpoch, key bytes, raw prices or raw/private checkpoint state. Public transformEpoch below is an integer counter, not the private UUID or key fingerprint.

### D5-A durable epoch, rollover and unchanged rules

Public identity is exactly D4's {generation,version,displayDate,transformEpoch}; transformEpoch is a durable positive counter string within a generation, initially1. Store an active private configuration tuple (key bytes, private keyEpoch, public transformEpoch, transformAlgorithm) with the dataset lineage outside public access. Normal restart reuses it exactly. A new dataset generation may initialize public epoch1; a key rotation does not by itself invent a new market-data generation or increment its commit version.

Rotation within the same UTC day: prepare the new key/private epoch and public epoch+1, stop affected publication, durably activate the tuple with a recovery witness, discard old transformed-cache entries, rebuild both instruments with the new factor and expose only fully validated responses under the new public epoch. Never mix old/new transformed rows inside a response, and never emit unchanged to an old epoch. If either active config/epoch witness is uncertain after failure, enter Recovery and error or retain independently validated prior history according to WP03; do not guess/reuse an identity. Counter overflow stops rotation/publication until explicit new-generation recovery; no wrap. An identical key-byte reinstall with unchanged policy is a no-op only if private/public epoch tuple is unchanged; an explicit rotation always increments public epoch even if bytes or rounded factor coincide.

D4 response acquisition reads one coherent active tuple and snapshot. If it changes before serialization commits, discard staged output and return DATA_UNAVAILABLE (normal scheduled retry later), not arrays under obsolete identity. Every success uses a coherent tuple; a single old response already in flight cannot overwrite a later accepted epoch because session request ordering and nondecreasing epoch validation apply. Two instruments may have distinct data versions, but the active transform epoch/key policy is shared; no success after activation uses the previous tuple. Cache key: schemaVersion/researchAlgorithm/transformAlgorithm/instrument/generation/version/displayDate/transformEpoch. No immutable entry is mutated for new keys or dates.

At trusted UTC midnight, recompute all retained price-valued arrays under the new displayDate, keeping data version/transformEpoch unless data/key changed. Old-day unchanged is forbidden. An in-flight construction crossing midnight is discarded and re-evaluated for the new day or returns DATA_UNAVAILABLE within the existing deadline. No partial re-transform. Backward trusted date or epoch within a generation triggers Recovery/UNTRUSTED_TIME, not silent acceptance of older arrays. Last-known disconnected charts retain their old identity/date and truthful label until validated replacement.

Atomic key activation/witness/fsync/rollback detection is a WP03 implementation obligation, not proven by this identity specification. Coordinated rollback of all private evidence may remain undetectable under the inherited best-effort lineage model; never claim cryptographic rollback proof. WP07 verifies actual Azure config/mount/restart behavior separately. No owner credentials, real keys or Azure operations are needed to review these proposals.

### D5 independent test-vector specification and alternatives

Only deterministic **test-only** keys are specified here; never deploy them. Required independent WP05 vector corpus:

| Vector family | Exact inputs | Independently checked expectation |
| --- | --- | --- |
| H1 normal date | Key hex `000102030405060708090a0b0c0d0e0f101112131415161718191a1b1c1d1e1f`; private keyEpoch `11111111-1111-4111-8111-111111111111`; public epoch1; date2026-10-23 | Independently publish exact message hex, full digest hex, D, F and transformed OHLC/all indicator outputs before accepting implementation golden vectors |
| H2 rollover/instrument sharing | H1 with date2026-10-24; BTC/USD and ETH/USD inputs | One common factor per date/key; different date message; never include instrument; full replacement required, no mixed dates |
| H3 same-day rotation | Key hex `1f1e1d1c1b1a191817161514131211100f0e0d0c0b0a09080706050403020100`; private epoch `22222222-2222-4222-8222-222222222222`; public epoch2; date2026-10-23 | New identity, old known epoch gets full; exact digest/F separately derived; distinct rounded factor is not a correctness assumption |
| M0/Mmax mapping only | Synthetic digest all00 / allff, not claimed HMAC outputs | F=900000000000 /1100000000000 respectively |
| Mhalf mapping only | Synthetic digest80 followed by31 zero bytes | F=1000000000000 exactly |
| Rounding only | F=900000000000, x integer units5 /15 /-5 /-15 | transformed units4 /14 /-4 /-14 by symmetric ties-even |
| Price/shape | Positive low/open/close/high, negative BB lower and signed MACD; RSI50, normalized features and volume1 | Same F on every price-valued output; RSI/dimensionless/volume unchanged; quantized OHLC ordering preserved |
| Identity/recovery negatives | Key/config change without epoch increment, epoch decrement/overflow, duplicate cache identity with different arrays, midnight serialization race, crash at config/witness activation | Reject/quarantine rather than unchanged, partial data or guessed epoch; independently defined fault outcomes |

H1/H2/H3 are vector-generation specifications, not fabricated digest values or tests reported passed. Independently author C# and a separately derived language-neutral/reference corpus under future test authority; do not copy production output as expected data. Assert raw bytes as well as decimal outputs and whole-response/cache behavior. All test-only material must be labeled and isolated from image production configuration.

Alternative D5 choices include changing dataset generation on rotation, scaling volume, or mapping fewer digest bits; they change cache/lineage/units properties. Recommended D5-A keeps market-data generation separate, includes a public monotonic epoch and uses every digest byte. Owner must explicitly accept/amend the key format, message, endpoints, rounding, volume policy and durable epoch protocol; there is no automatic contract freeze from this document.

## Review boundary and downstream conformance

These four proposals replace unspecified choices with reviewable candidate semantics only. D1 remains PROVISIONAL; D3/D4/D5 remain OPEN until explicit owner decisions. D2/D6/D7 remain PROVISIONAL. Preserve all22 rows and primary owners in the qualification matrix; Q01/Q10/Q12/Q13 require owner contract acceptance before implementation/conformance execution. WP03 durability, WP06 UI integration and WP07 actual Azure/resource evidence are still downstream work, not invented passes or a licensing approval gate.

No application code, tests, CI, dependency, historical S1/S2 evidence or governance was changed by this amendment. Documentation validation may parse illustrative JSON and verify links/identities; it does not run a HMAC/calculator/supervisor or assert production test results. PR #380 stays open/unmerged, issue #367 open, WP02 unstarted and `PUBLIC_RUNTIME_VERIFIED=False`.

## Documentation validation disclosure

Validation for this amendment is limited to Markdown paths/anchors, D-status/owner consistency, unchanged Q/acceptance rows, illustrative JSON syntax, test-only key byte lengths, exact authorized paths and UTF-8/LF. It does not execute proposed research/HMAC/kernel behavior, independently calculate digest vectors, run product tests or qualify Linux/Azure. A first protected-file raw-byte comparison flagged existing CRLF checkout forms of three untouched governance files; Git content comparison confirmed no change. Those files were not normalized. Changed/new files and staged blobs are scanned independently for no BOM/CRLF/lone CR. Historical failed experiments remain unchanged.

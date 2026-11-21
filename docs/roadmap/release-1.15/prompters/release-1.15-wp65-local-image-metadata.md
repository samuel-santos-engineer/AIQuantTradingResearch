# Release 1.15 WP65 ? accepted WP61 local image metadata

Date: 2026-11-16 (America/Sao_Paulo). **STOPPED: one authorized inspect attempted, CLI_FAILED; identity UNVERIFIED.** No export authorized/performed.

Base main verified `ed39e0a0ca18e6316a5738651fce88aa28f2990f`, merged #460. New clean isolated branch; #457 remains OPEN and untouched. Read [WP61 receipt](release-1.15-wp61-post-merge-local-browser-qualification.md), [WP64 offline verifier/evidence](release-1.15-wp64-wp61-ui-oci-provenance.md) and [merged provenance plan](release-1.15-setting-name-presence-and-image-provenance.md). Existing OCI/parser/collector semantics and Windows live block unchanged.

## Exact authority and preflight

One read-only daemon/image command only, fixed accepted ID `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`. No image save/export, enumeration, container/volume inspection, fallback context, pull/build/publish/retag or daemon start. Docker override variable PRESENCE checked only for DOCKER_HOST/DOCKER_TLS_VERIFY/DOCKER_CERT_PATH; none present. No values exposed.

Two local context-metadata commands (not daemon/image operations) established current context and endpoint without changing either:

```text
docker context show
docker context inspect desktop-linux --format '{{json .Endpoints.docker.Host}}'
```

Both completed with exit0. Context `desktop-linux`; endpoint exactly the existing local Docker Desktop Linux named-pipe contract (not a remote host or another daemon). No endpoint/private path persisted. Strict mismatch/override would have stopped before image call. Capture reused the merged, previously genuine-Windows-qualified standalone capture primitive/Job Object only, not the blocked Azure adapter: direct argument arrays/no shell; each command10-second request deadline, combined stdout/stderr1MiB cap, suspended-process owned-job attachment and bounded descendant/pipe cleanup. Cleanup can add a bounded tail outside the request deadline; no uncontrolled background process was reported. No Azure CLI/network module invoked by importing that helper. Linux ACL qualification was not a prerequisite to this Windows-host local metadata read.

[Projection/command helper](../../../../eng/docker/release-1.15/wp65_local_image_metadata.py) constructs this exact attempted daemon command:

```text
docker --context desktop-linux image inspect sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 --format <fixed_allowlisted_Go_template>
```

Fixed template in source requests only Id/Os/Architecture/Config.User/Config.ExposedPorts/Config.Entrypoint/Config.Cmd/RepoDigests. Docker may receive richer image JSON internally from the daemon before formatting; this cannot be prevented by a CLI projection. No richer/raw payload is persisted or displayed. Entrypoint/Cmd bytes, if returned, would remain in memory only and become fixed contract booleans; ports only validated names, RepoDigests only safe exact existing-repository@fullSHA256 references. No Env/labels/history/filesystem/layer/path inspection or content export. Projection schema/duplicate/cap/wrong-ID errors return UNVERIFIED without raw values.

## Actual single-attempt result

Observation start **2026-11-17T01:46:31.966175Z / 2026-11-16T22:46:31.966175-03:00 Sao Paulo**. Exactly **1 image inspect attempt**, plus the2 local context commands above. Inspect capture returned fixed category **CLI_FAILED** (nonzero process status). The reused wrapper did not retain numeric exit code/stderr; exact numeric exit and reason remain UNKNOWN, not fabricated. No timeout/overflow/cleanup failure category emerged, but no usable image metadata was received. No retry, alternative template invocation, image inventory or remedial action followed.

| Field | Actual WP65 evidence |
| --- | --- |
| Current context | desktop-linux, local Docker Desktop Linux endpoint verified |
| Exact image queried | Recorded WP61 local ID3e323b0d...02bbd69 above |
| Fresh returned image ID/platform/user/ports/startup | UNVERIFIED, no projected response obtained |
| Image present versus absent | UNKNOWN; CLI_FAILED does not distinguish missing image/daemon/format/permission failure |
| Local identifier kind | Recorded local Docker image ID only; OCI/config/child kind UNKNOWN |
| Fresh RepoDigests | UNKNOWN, not an empty-list observation |
| OCI index / runtime child / config digest | UNKNOWN; inspect failed, and inspect alone would not prove full raw chain |
| WP64 real-chain input | None; only synthetic regression fixtures used |
| Registry publication necessary? | NOT PROVEN NECESSARY, still UNKNOWN |
| Azure configured UIa06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07 relationship | UNVERIFIED, no same-kind verified mapping |

Projection MATCH would mean only returned ID and checked Linux/amd64/UID12002:12002/exposed/listening8501/Streamlit prefix/bind0.0.0.0 contract, not source/OCI/publication attestation. MISMATCH would mean a checked contract contradiction, not proof about Azure. Actual result is UNVERIFIED. No deployable UI registry reference supplied. No rebuild/publication recommendation solely for missing metadata.

## Smallest next action ? separately approved only

Since inspect failed, first propose **one fresh exact-ID inspect with fixed stderr category capture**, rather than export. Same context/ID/no-shell/10-second/1MiB/no-retry/owned-cleanup limits; if separately approved, retain numeric exit plus only fixed image-not-found/daemon-unavailable/template-error/permission-denied/unclassified booleans, never raw stderr. No other image/context enumeration or fallback. This consumed attempt cannot be rerun automatically. If image absence is established, stop and obtain owner disposition; no pull/rebuild. No registry lookup inferred from absence.

Only after correct image availability/contract are established may an owner separately authorize export. Export is **NOT authorized here** and copies ALL layers, not merely metadata. Proposed future maximum1 docker image save of exact accepted ID,120-second deadline, hard pre-write2GiB output limit (e.g. qualified Linux RLIMIT_FSIZE plus bounded process/session handling), new verified-owned0700 Linux directory and0600 output verified BEFORE potentially sensitive bytes. No post-write truncation substituted for a cap. Read only raw OCI index/manifest/config metadata objects <=1MiB from archive without extraction/execution or opening layer contents; verify SHA256/descriptors with WP64 before safe-kind output. Clean only positively identified owned file/directory after safe closure. No real export, permissions implementation or RLIMIT qualification occurred in WP65. If these bounds cannot be guaranteed on the available host, prefer a metadata-only exact local inspect/error classification or explicitly approved existing-receipt review, not export. Do not demand Windows ACL privilege as a Linux deployment gate.

Deployment sequence unchanged: accepted API child2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 first, accepted WP61 UI only after verified provenance and API health/authenticated BTC/ETH/data readiness. Diagnostic d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c not substituted. No port/auth/storage/TLS/data changes.

## Offline implementation and validation

New helper is plan-only by default, with no runtime switch and no Docker invocation on import/default CLI. It accepts only explicit projected bytes for validation; malformed/missing/wrong-ID responses fail closed. New [synthetic tests](../../../../eng/docker/release-1.15/test_wp65_local_image_metadata.py) cover exact command/no-rich-fields, matching contract without digest-kind claims, malformed/duplicate/over-cap/wrong-ID input, contract mismatch, argument-value redaction, safe/unsafe RepoDigest and no-network/no-daemon default. Initial7-test run6passed/1failed because the test searched for Entrypoint anywhere, including permitted streamlitEntrypoint boolean; corrected to reject only the raw JSON field, preserving content non-disclosure checks. No real inspect was spent before projection tests passed.

Commands actually run:

```text
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q
python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q
python -B eng/docker/release-1.15/wp65_local_image_metadata.py
```

Final **7 new +18 WP64 +31 parser/collector =56 passed,0 failed/skipped**, each suite exit0. Default plan exit0/no daemon call. Actual Windows host; no Linux export/permissions qualification claimed. Python compile-only syntax, UTF-8/LF/no BOM, Markdown links/tables, exact three-file scope and git diff --check validated before publication. Prior helper/verifier/parser/collector files/tests unchanged. No raw inspect output or raw stderr stored; only safe observation metadata retained. PR OPEN/UNMERGED.

**ZERO Azure/registry/credential operations. Exactly one authorized Docker image-inspect attempt; ZERO exports/builds/pulls/pushes/tags/container/volume operations.** `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## Separately approved single retry ? 2026-11-16 Sao Paulo

Owner explicitly approved the next one-inspect numeric-exit/sanitized-error scope. Exactly one new image inspect against the same fixed WP61 ID and explicit existing desktop-linux context, no context switch or other daemon/image call. Docker override presence check still passed.10-second request cap/combined1MiB capture, direct argument array, suspended owned Windows Job; cleanup accounting verified0active processes and pipe readers joined. No export.

Start2026-11-17T01:52:13.303181Z /2026-11-16T22:52:13.303181-03:00. Numeric inspect exit **1**, captured operation duration0.395seconds. Fixed stderr categories: templateError=true; imageNotFound=false; daemonUnavailable=false; permissionDenied=false. No raw stderr or sensitive field values persisted/displayed. Classifier is limited phrase evidence, not proof every unrecognized condition is absent. The specific template field/message was not retained, so exact defect UNKNOWN. This supersedes only the earlier unclassified numeric-exit gap; prior single-attempt history stays unchanged.

Observed failure is in local formatting/projection. It does NOT establish image missing, corrupted, unpublished or runtime failure. No safe image response yet; accepted local-kind/OCI/index/child/config/RepoDigest and Azure relationship remain UNVERIFIED. No extra inspect, fallback, export, registry or Azure action followed. Smallest next step is offline review/correction of the projection template and safe error-field classification, then separate authorization for a single corrected inspect if needed. Do not rebuild/export/redeploy to address a formatting error. Current helper template must not be considered live-qualified.

Only this evidence document changed for the retry record; UTF-8/LF/no BOM and git diff --check passed. Existing56 offline tests remain preceding results, not rerun for this documentary addendum. PR OPEN/UNMERGED. ZERO Azure/registry/credential operations; PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## Owner scope correction and Python-only simplification ? 2026-11-18

Owner explicitly removed Go from WP65 scope. The previous offline revision's embedded harness, discovery/fixtures, temporary cache/environment settings, skipped compiler test and qualification flag have been removed, along with its superseded toolchain-run instructions. No Go installation/execution/download/dependency/CI or acceptance requirement exists. That revision never performed actual template evaluation or a successful Docker inspection; original real-attempt evidence above is preserved verbatim, including the retry exit1/0.395seconds/templateError=true and cleanup success. No exact failing expression is inferred retroactively.

Fresh references before correction matched mained39e0a0ca18e6316a5738651fce88aa28f2990f and #461 OPEN headfcd59834e2cdce4eda9e6b6c002d9ddb986a9c8b, CLEAN/MERGEABLE. Existing branch only, no force push/history rewrite or unrelated #457 change.

Next proposed daemon command is deliberately minimal, eliminating nested Config traversal and custom type guards:

```text
docker --context desktop-linux image inspect sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 --format '{"Id":{{json .Id}},"Os":{{json .Os}},"Architecture":{{json .Architecture}}}'
```

**NOT EXECUTED.** This uses Docker's formatting json function and three root fields only; no full-object dump, nested Config, Env/labels/history/layers/filesystem or raw command arguments requested. Actual installed formatter root/null semantics remain **UNVERIFIED**: Python command-construction tests do not evaluate Docker's formatter and are not presented as doing so. No substitute evaluator or toolchain added. Default helper reports formatterValidation=UNVERIFIED_NO_DOCKER_RUN and no daemon operation.

A minimal response with exact WP61 ID/Linux/amd64 may establish only those local metadata assertions; helper deliberately returns overall UNVERIFIED because user/port/startup/RepoDigest evidence is absent. A separately approved later richer projection could be proposed only if needed, rather than forcing all fields through one fragile expression. No local ID or RepoDigest alone is an index/child/config mapping, publication proof or deployable reference.

project(raw) retains1MiB cap, duplicate-key rejection, exact WP61 ID and redaction. Optional allowlisted richer fixture fields are checked when present: Linux/amd64, UID12002 (or12002:12002), valid exposed port names, explicit8501/listen0.0.0.0 flags and Streamlit prefix, safe exact application-repository@fullSHA256 references. Required missing/nil/unsupported evidence stays UNVERIFIED. Valid conflicting contract values produce MISMATCH; invalid types or unsafe refs do not escape as raw values. Missing listen flags are not called a contradictory port. Nil Cmd may be empty; false/string/dict is malformed. Extra Env/labels/history fields never returned; raw args/paths/unsafe refs/errors never output. These Python tests qualify parser behavior only, not real image/formatter semantics.

Actual offline-only commands on Windows:

```text
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -v
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q
python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q
python -B eng/docker/release-1.15/wp65_local_image_metadata.py
```

Final **13 WP65 +18 WP64 +31 parser/collector =62 passed,0 failed/skipped**, suites exit0. Default helper exit0/no daemon. New Python coverage includes minimal command fields, partial evidence, optional/nil/wrong types, exact/wrong ID, duplicates, port/entrypoint contradiction versus absence, safe/unsafe RepoDigests, redaction and no-network/default execution. Existing WP64/parser/collector sources/tests unchanged. Syntax, UTF-8/LF/no BOM, Markdown links/tables, exact three-path inventory and git diff --check verified before publication. No skipped toolchain check used as qualification.

Next owner decision is **one new separately authorized bounded exact-image inspect** using the minimal projection above: current desktop-linux/local-pipe identity qualification,10second request deadline, combined1MiB capture, numeric exit/fixed stderr categories, owned-process cleanup, no retry/fallback. No prior attempt approval is reused. This correction authorizes ZERO Docker commands of any kind, ZERO daemon/image/export/container/volume/build/pull/push/tag operations, ZERO registry/Azure/credential/private-runtime/public-probe/deployment operations; none occurred. No requirement to install or run another language first.

Accepted API-first/UI-second sequence and auth/TLS/storage/ports unchanged. **WP61 UI OCI provenance UNVERIFIED**; publication necessity **NOT PROVEN**. No image presence/RepoDigest/index/child/config/registry-availability/runtime-readiness claim. PR #461 OPEN/UNMERGED; PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## WP66 ? one authorized minimal identity/platform observation ? 2026-11-18

Base current main4a36cd983c7730d89736b7d0f0db0da122f7411b contains merged #461. New focused evidence branch/PR only; prior WP65 original CLI_FAILED and approved retry exit1/0.395seconds/templateError=true remain verbatim above. WP61/WP64 receipts and merged Python-only helper/tests reviewed. No Go/toolchain activity or new dependencies. #457 untouched.

Preflight verified original Windows host by in-memory exact host comparison (machine identifier not output), absent DOCKER_HOST/DOCKER_TLS_VERIFY/DOCKER_CERT_PATH override names (values not read), current contextdesktop-linux and exact existing local Docker Desktop Linux named-pipe endpoint. Two local metadata-only commands: docker context show; docker context inspect desktop-linux --format '{{json .Endpoints.docker.Host}}'. Both exit0; no context change/other daemon contacted. No docker version/info, image inventory or other daemon probing.

Exactly **one** authorized daemon/image operation, helper-generated direct argument array:

```text
docker --context desktop-linux image inspect sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 --format '{"Id":{{json .Id}},"Os":{{json .Os}},"Architecture":{{json .Architecture}}}'
```

Observation start **2026-11-19T02:28:01.896395Z /2026-11-18T23:28:01.896395-03:00 Sao Paulo**. Numeric exit **0**; request duration **0.244seconds**.10second request deadline/combined1MiB stdout+stderr cap; previously qualified suspended owned Windows Job/capture primitive used without Azure adapter. Job accounting0active processes and pipe-reader cleanup verified, no orphan reported. Raw command output/stderr transient only, cleared without persistence/display; only safe booleans/recorded exact ID retained. Error categories not applicable to successful return. No retry or richer projection followed.

| Safe WP66 assertion | Result |
| --- | --- |
| Exact local Docker image ID equals accepted WP61 receipt | MATCH: sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 |
| OS / architecture | Linux / amd64 verified in minimal response |
| Original host/current local context/endpoint | Preflight PASS |
| Owned descendant / pipe cleanup | VERIFIED |
| UID/ports/entrypoint/Cmd/RepoDigests | NOT_REQUESTED, UNKNOWN |
| Config/index/linux child kind and digests | UNVERIFIED |
| Publication / deployable registry reference / Azure equivalence | UNVERIFIED; none established |

Existing project(raw) parsed the transient three-field JSON: contract.linuxAmd64=true and exact localId match; overall result **UNVERIFIED**, intentionally. This is a successful evaluation of this minimal formatter on the actual local host for this one image, not evidence richer/nested formatting works. Earlier formatting failures remain historical, not evidence of image absence/corruption. A local ID is not promoted to a config/index/child digest by this success; no raw OCI metadata was collected or fed to the offline verifier. Local image existence/platform now observed, not current container/chart health. Docker CLI may internally obtain richer metadata from daemon; formatter emitted only the three requested fields, no sensitive output captured.

Required offline suites ran BEFORE and AFTER this single possible inspect: each run13WP65+18WP64+31parser/collector=**62passed,0failed/skipped**, exit0. No source/test changes necessary; same62 cases repeated for before/after, not124distinct tests. Python syntax, UTF-8/LF/no BOM, links/tables, exact one-file diff and git diff --check validated before publication. Previous source/helpers/tests unchanged.

Smallest separate next proposal: one new bounded exact-image inspect requesting only root Id and RepoDigests with safe existing-repository@fullSHA256 projection,10seconds/1MiB/numeric-exit/sanitized-error/owned-cleanup/no-retry limits, same qualified host/context. No nested Config/richer private fields. If no safe RepoDigest exists, retain publication UNKNOWN rather than assume nonpublication; propose separately bounded local OCI metadata resolution later. A RepoDigest would still require exact registry index/child/config chain verification under future explicit registry scope before a deployable reference is accepted. No new inspection/export/registry approval inferred here.

Preserve accepted API-first/UI-second order: accepted research child2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3, diagnostic d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c remains separate. Historical Azure UIa06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07 not proved equivalent to WP61. No auth/TLS/storage/port/data behavior changed.

**ZERO Azure/registry/credential/runtime/volume/public-probe operations. Exactly1 daemon image inspect plus2 local context-metadata commands; ZERO exports/builds/pulls/pushes/tags/container operations.** PR OPEN/UNMERGED; PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## WP67 ? one authorized RepoDigest-only observation ? 2026-11-18

Current main27875afd437846a1eaeae84ea7470ddf955eb1b6 verified with #462 merged; focused new branch. WP65/WP66 history preserved verbatim above. #457 untouched. Existing helper's general project parser deliberately does not distinguish RepoDigest-null/empty/failure, so a small explicit RepoDigest-only command/classifier plus4 synthetic tests were necessary. No existing OCI/provenance semantics changed.

Before the single inspect, Python-only tests passed:17WP65 (13existing+4new),18WP64,31parser/collector, total66. Classifier requires exact WP61 ID,1MiB input cap, duplicate/schema rejection; RepoDigests null/empty/safe reference states distinct; at most16refs accepted for classification. Any non-string/incomplete/malformed/non-allowlisted reference withholds the entire set (no safe partial list). Only fixed application repository@full lowercaseSHA256 values may ever be output. A local RepoDigest never proves OCI kind/publication/current availability.

Original Windows host compared in memory (private machine identifier not output); Docker override variable names absent without reading values; two local metadata commands verified current desktop-linux and exact existing local Docker Desktop Linux pipe: docker context show; docker context inspect desktop-linux --format '{{json .Endpoints.docker.Host}}'. Both exit0; no context changes/fallback/daemon start or probes. Exactly one daemon/image operation:

```text
docker --context desktop-linux image inspect sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 --format '{"Id":{{json .Id}},"RepoDigests":{{json .RepoDigests}}}'
```

Direct argument array/no shell;10second request deadline, combined stdout/stderr1MiB cap, qualified suspended owned Windows Job and bounded pipe cleanup. Observation start **2026-11-19T02:44:28.565405Z /2026-11-18T23:44:28.565405-03:00 Sao Paulo**. Numeric exit **0**, request0.174seconds. Job accounting0active processes/pipe cleanup VERIFIED. No raw stdout/stderr/unknown refs persisted or displayed; buffers cleared. Fixed error categories not applicable to zero exit. No other image/daemon operation, no retry/richer query/export.

| Safe classification | Result |
| --- | --- |
| Exact accepted WP61 local ID | MATCH |
| Inspect command outcome | Success, exit0 |
| RepoDigest collection | UNVERIFIED; UNSAFE_REFERENCE_WITHHELD |
| Meaning of unsafe label | At least one value did not meet strict existing-repository@fullSHA256 string allowlist; not proof of a credential, malicious content or image corruption |
| Permitted safe refs/count | UNKNOWN/not disclosed; entire set withheld, no partial list retained |
| Null versus empty collection | Neither reported by classifier; unexpected-reference branch triggered, not inspection failure |
| Runtime UI contract/user/ports/entrypoint | NOT_REQUESTED; historical WP61 evidence only |
| Config/index/child/registry publication/current availability/Azure equivalence | UNVERIFIED |

The classifier processed an exact-ID response and an in-cap reference list, but intentionally withheld all values after an unexpected member. No raw image/ENV/labels/history/layers/filesystem or paths requested. Docker CLI can receive richer daemon metadata internally before --format; only the fixed two fields were captured transiently. This establishes no safe GHCR immutable mapping, and no requirement to republish/rebuild. No deployable UI reference emitted. Older Azure UIa06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07 remains historical configuration, not verified equivalence.

Smallest separately approvable next step: first offline review of the strict classifier's distinction between non-allowlisted repository and malformed reference, without changing the allowed-output repository. If owner then authorizes one new exact-ID RepoDigest inspect, classify only fixed structural categories (such as complete digest, application-repository match, other-repository present, malformed/userinfo present) without exposing any other repository/value or broadening the allowlist. This is a proposal, NOT RUN; consumed authority does not permit reinspection or using discarded raw output. Only after a trustworthy immutable mapping exists may an exact separate registry metadata footprint be proposed; no registry call requested/performed now. Image export remains outside this scope.

Post-inspect suites also passed:17WP65+18WP64+31parser/collector=**66passed,0failed/skipped**, exit0, same66cases repeated before/after (not132distinct tests). Actual Windows host, no Go/toolchain/runtime dependency. Python syntax, UTF-8/LF/no BOM, Markdown links/tables, exact three-path scope and git diff --check validated before publication. Existing WP64/parser/collector source/tests unchanged. Helper default remains plan-only/no Docker. No synthetic result used as actual OCI/provenance evidence.

**Exactly1 authorized image inspect plus2 local context metadata commands. ZERO registry/Azure/credential/public-probe/private-runtime operations; ZERO exports/builds/pulls/pushes/tags/containers/volumes.** Preserve accepted API-first/UI-second order, accepted API2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3, diagnostic d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c separate. PR OPEN/UNMERGED; PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.


## WP68 ? offline RepoDigest structural classifier ? 2026-11-19

Base: merged PR #463, `9f6fa36cee1282d17016835bfb7da6a0787b661f`.
The isolated branch preserves unrelated owner checkout changes. Only the existing
Python helper, its synthetic tests and this appended evidence entry change.
All authored text is UTF-8/LF without BOM; previous evidence is preserved verbatim.

The transient, bounded list now produces four fixed category counts:
`EXPECTED_REPOSITORY_DIGEST`, `OTHER_REPOSITORY_DIGEST`, `MALFORMED_REFERENCE`
and `UNEXPECTED_VALUE_TYPE`. The deliberately conservative full-match grammar
accepts dotted lowercase DNS registry names and lowercase repository components
with single internal separators, followed by `@sha256:` and 64 lowercase hex
characters; it caps each accepted reference at 1024 characters. Ports, schemes,
userinfo, whitespace, uppercase, bare tags, incomplete digests and ambiguous
forms are rejected. Valid references outside this subset can be classified as
malformed: this is a conservative syntax classifier, not a complete OCI parser.

Exact image ID, duplicate-key rejection, 1 MiB input limit, 16-reference cap,
NULL/EMPTY/UNKNOWN semantics, command construction and plan-only default remain.
Reference values are emitted only when the entire collection belongs to the
unchanged exact application-repository allowlist. Mixed, other-repository,
malformed and unexpected-type collections stay UNVERIFIED with all values
withheld; only fixed counts are added. No raw exception text is returned.
WP66 identity/platform evidence is unchanged. WP67's discarded real values cannot
be retroactively reclassified; its UNSAFE_REFERENCE_WITHHELD result remains the
only supported classification of that historical observation.

| Actual offline validation | Result |
| --- | --- |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q` | 23 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q` | 18 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q` | 31 passed, exit 0 |
| `python -B eng/docker/release-1.15/wp65_local_image_metadata.py` | PLAN_ONLY, exit 0, zero Docker operations |

Total: 72 tests passed, zero skips and zero failures. New synthetic cases cover
category separation, mixed-list redaction, malformed and ambiguous strings,
unexpected types, early guards, count boundaries, and import/default execution
under process/network-failure mocks. Syntax compilation, UTF-8/LF/no BOM,
Markdown links/tables, evidence-prefix preservation, exact three-path scope and
`git diff --check` were checked before publication of the PR.

Offline classifier only: no new Docker inspection or Docker command of any kind;
no registry/Azure/provider/public HTTP operations, credentials, private baseline,
exports, builds or runtime actions. Git remote/PR operations only publish the
reviewable repository delta. OCI provenance UNVERIFIED; publication necessity
NOT PROVEN; registry reachability, manifest/index/config kind, deployable
equivalence and Azure runtime state remain unknown.
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

Possible next decision after review/merge: separately authorize one bounded
exact-image RepoDigest inspection on the qualified original host/context, with
the existing 10-second/1 MiB limits, numeric exit, sanitized errors, owned-process
cleanup and no retry/fallback. No such inspection is authorized or executed here.
A future safe mapping would require separately approved exact registry metadata
verification, not an assumption of publication or a rebuild.


## WP69 ? bounded real RepoDigest structural observation ? 2026-11-19

Base: merged PR #464, `3ea0804e9819c8d073131bd79d699cb417e7b7ac`.
Observation UTC: 2026-11-19T23:30:24.455955+00:00;
America/Sao_Paulo: 2026-11-19 20:30:24.455955-03:00.
The original Windows host identity was compared privately in memory and matched.
The three prohibited Docker override names were absent; values were not read.
Both authorized local context commands passed, verifying the qualified context
and local named-pipe endpoint without publishing identifiers.

Exactly one daemon/image inspect ran with the unchanged two-field command and
accepted WP61 local ID
`sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`.
It returned numeric exit **0** in **0.358 seconds**, within the 10-second request
limit and 1 MiB combined stdout/stderr cap. The existing suspended-process
Windows Job ownership and bounded pipe-capture mechanism was used. All owned
Jobs verified zero active processes after termination; pipe readers joined and
pipes closed before classification returned. Exact-ID match: **true**.

| Safe structural category | Count |
| --- | --- |
| EXPECTED_REPOSITORY_DIGEST | 0 |
| OTHER_REPOSITORY_DIGEST | 0 |
| MALFORMED_REFERENCE | 1 |
| UNEXPECTED_VALUE_TYPE | 0 |

Projection result: **UNVERIFIED**, state **UNSAFE_REFERENCE_WITHHELD**.
No reference values or partial expected-repository collection were retained or
published. Raw stdout/stderr remained transient in memory and references to
those buffers were cleared after classification; no raw-output artifact was
written. This does not claim secure erasure of Python process memory.
The conservative grammar may reject legitimate unsupported OCI syntax.
This outcome does not establish corruption, credential presence, nonpublication,
registry availability or a need to rebuild/republish. No safe immutable mapping
was established. This new observation does not reclassify WP67's discarded raw
values. WP66 identity/Linux-amd64 and prior evidence remain unchanged.

| Offline command (before and after observation) | Each run |
| --- | --- |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q` | 23 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q` | 18 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q` | 31 passed, exit 0 |

72 distinct tests passed before and again after, zero failures/skips; repeated
runs are not 144 distinct cases. Default helper execution returned PLAN_ONLY,
exit 0, dockerOperations 0. Helper/tests were not changed. Syntax compilation,
UTF-8/LF/no BOM, Markdown links/tables, exact one-path diff, prior evidence byte
prefix and `git diff --check` passed before PR publication.

Operation inventory: two local context-metadata commands, one exact image
inspect, zero retries/fallbacks/other Docker operations. Zero registry, Azure,
credential, provider, public-probe, export, build, pull, push/tag image,
container/volume or deployment operations. Repository Git/PR publication only;
PR #457 untouched. Owner checkout and retained runtime resources unchanged.

Smallest next step: offline review of the conservative grammar and synthetic
unsupported-form cases to decide whether a narrower fixed-category diagnostic
would be useful. That would not identify this discarded value. Any future real
observation needs separate exact approval; do not broaden the value allowlist,
query a registry, rebuild or publish by assumption. Preserve API-first/UI-second
order and existing auth/TLS/storage/ports.

OCI provenance **UNVERIFIED**; publication necessity **NOT PROVEN**.
Local RepoDigests do not prove registry reachability, current publication,
index/child/config chain, deployment equivalence or Azure health; the local image
ID is not reinterpreted as an OCI digest.
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## WP70 ? offline RepoDigest grammar review ? 2026-11-19

Fresh main: `3ea0804e9819c8d073131bd79d699cb417e7b7ac` (merged #464).
Live GitHub verification found #465 OPEN/UNMERGED at
`f40c5ce24ac5fc73a5a1bfb17064411fcb155ca2`, based on that same main.
WP70 branches independently from main; no #465 evidence was copied, rewritten
or cherry-picked. Both append to this document's former end, so an append-hunk
conflict is possible after either merges. Reconcile by retaining both complete,
separately dated WP69 and WP70 sections; never replace either chronology.
No merge of #465 is performed or authorized by this work package.

Offline review of the existing Python expressions found deliberate exclusions:
explicit registry ports, localhost/single-label registries, IPv6, repeated
repository separators, uppercase names/digests and tagged references. Local
repository material does not supply an authoritative vendored Docker reference
parser/specification for validating all these forms. No external documentation
was fetched. This review therefore does not claim authoritative Docker/OCI
format validation, and does not label a value UNSUPPORTED_VALID_SHAPE.

The smallest refinement is **UNSUPPORTED_REFERENCE_SHAPE** for a dotted lowercase
DNS authority with an explicit canonical decimal TCP port in 1..65535, the
existing conservative repository subset, and full lowercase sha256 hex. Offline
reasoning: the colon before the first slash unambiguously separates a decimal
port from this DNS authority; the digest separator remains after the path. This
recognizes an explicit-port shape without claiming a registry exists, accepts
that reference, or has published content. Ports with leading zeroes, signs,
zero/out-of-range values, userinfo, schemes or ambiguous forms remain malformed.
Single-label/localhost, IPv6, repeated separators and tag-plus-digest remain
conservatively malformed pending reliable source-backed reasoning. Legitimate
unsupported syntax may still fall into MALFORMED_REFERENCE.

| Synthetic fixture only | Fixed classification |
| --- | --- |
| Exact allowed application repository plus lowercase sha256 | EXPECTED_REPOSITORY_DIGEST |
| `registry.example.org/team/repo@sha256:` plus 64 lowercase hex | OTHER_REPOSITORY_DIGEST |
| Same authority with explicit `:5000` before repository path | UNSUPPORTED_REFERENCE_SHAPE |
| Scheme/userinfo, bare tag, incomplete/uppercase hex or invalid port | MALFORMED_REFERENCE |
| Nonstring entry | UNEXPECTED_VALUE_TYPE |

All category counts remain fixed/bounded by the 16-entry limit; raw input is
capped at 1 MiB. The exact application-repository emission allowlist is unchanged.
Any unsupported/other/malformed/unexpected or mixed collection stays UNVERIFIED
and withholds every reference value. Exact-ID/duplicate-key guards, null/empty/
unknown/count-limit states, sanitized errors and default PLAN_ONLY remain.
The inspect argv and both formatter constants are byte-for-byte unchanged.

| Actual validation command | Result |
| --- | --- |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q` | 26 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q` | 18 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q` | 31 passed, exit 0 |
| `python -B eng/docker/release-1.15/wp65_local_image_metadata.py` | PLAN_ONLY, exit 0, dockerOperations 0 |

Total 75 tests passed, zero failures/skips (prior baseline 72). Three new tests
cover port boundaries/ambiguities and mixed redaction/count bounds. Existing
synthetic port expectation was updated to the new fixed category; other negative
intentions and assertions remain. Import/default tests deny process/network
creation. Syntax, UTF-8/LF/no BOM, Markdown links/tables, exact three-path scope,
prior evidence prefix, unchanged command construction and git diff --check
passed before PR publication. Owner worktree changes are preserved.

WP69 remains historically one MALFORMED_REFERENCE under its then-current WP68
grammar; WP67 remains UNSAFE_REFERENCE_WITHHELD. Their discarded values were not
accessed and cannot be retroactively classified. WP66 accepted identity/platform
and WP61 local ID are unchanged. No rebuild/publication requirement is inferred.

Offline grammar review only: zero Docker commands/inspection, registry HTTP,
Azure, credentials, Go/toolchain, image/runtime/data or deployment operations.
Only authorized Git remote/PR lifecycle operations publish this source delta.
OCI provenance UNVERIFIED; publication necessity NOT PROVEN.
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

Any future real observation requires separate explicit authorization with original
host/context qualification, one exact-image operation, 10-second/1 MiB bounds,
numeric exit, sanitized errors, owned-process cleanup and no retries/fallback.
No such operation is authorized or executed here. No classification proves a
reachable/published index, child/config chain, deployable equivalence, Azure
health or working public UI. Preserve API-first/UI-second order and existing
auth/TLS/storage/ports.


## WP71 ? bounded WP61 RepoDigest shape observation ? 2026-11-19

Fresh main `c04fad45c59d5988e79d3c56dc501dc8ea594725` contains both required
merges: #465 `a88e8a4c880a38e969b6017dcd24037bd649b339` and #466
`c04fad45c59d5988e79d3c56dc501dc8ea594725`. Work used a clean isolated branch;
owner changes and all WP65?WP70 evidence are preserved. Helper/tests unchanged.

UTC: **2026-11-19T23:46:32.846963+00:00**;
America/Sao_Paulo: **2026-11-19 20:46:32.846963-03:00**.
The established in-memory original Windows host comparison passed without
publishing or persisting its identifier. Prohibited Docker override names were
absent; their values were not read. The two authorized local context metadata
commands verified the expected current context and local pipe, without storing
or exposing endpoint values. No daemon operation preceded qualification.

Exactly one helper-generated direct-argv image inspect ran with the unchanged
Id/RepoDigests formatter and accepted WP61 local image ID
`sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`.
Numeric exit **0**, duration **0.416 seconds**; exact-ID match **true**.
The existing suspended-process Windows Job and bounded pipe capture enforced a
10-second request deadline and 1 MiB combined stdout/stderr cap. All owned Jobs
verified zero active processes; pipe readers joined and pipes closed before
return. No orphan descendants remained in the owned Jobs.

| Fixed structural category | Observed count |
| --- | --- |
| EXPECTED_REPOSITORY_DIGEST | 0 |
| OTHER_REPOSITORY_DIGEST | 0 |
| UNSUPPORTED_REFERENCE_SHAPE | 0 |
| MALFORMED_REFERENCE | 1 |
| UNEXPECTED_VALUE_TYPE | 0 |

Classifier result **UNVERIFIED**, state **UNSAFE_REFERENCE_WITHHELD**.
No explicit-port shape was recognized by this grammar. All reference strings
were withheld, including any potentially allowlisted members; only fixed counts
and execution metadata were retained. Raw stdout/stderr stayed transient in
memory, buffer references were cleared after classification, and no raw-output
artifact was written. This is not a claim of secure process-memory erasure.
The conservative classifier can reject legitimate unsupported syntax. No
corruption, credential presence, nonpublication, registry reachability or
rebuild/publication need is inferred. This is a fresh observation, not a
retroactive reclassification of WP69's discarded value or WP67's observation.

| Offline suite command, before and after | Each run |
| --- | --- |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q` | 26 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q` | 18 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q` | 31 passed, exit 0 |

75 distinct tests passed before and again after; zero failures/skips. Repeated
runs are not counted as 150 distinct cases. Default helper returned PLAN_ONLY,
exit 0 and dockerOperations 0. Syntax compilation, UTF-8/LF/no BOM, Markdown
links/tables, prior evidence byte-prefix preservation, unchanged helper/tests,
exact one-path diff and git diff --check passed before PR publication.

Operation inventory: **2 local context commands, 1 exact image inspect, 0
retries/fallbacks/other Docker operations**. Zero registry/GHCR HTTP, Azure,
credential, provider, public probe, image export/build/pull/push/tag,
container/volume, runtime or deployment activity. Authorized Git/PR lifecycle
only publishes this append-only evidence; no auth/TLS/storage/port or deployment
sequence changes. PR remains OPEN/UNMERGED.

Next smallest decision: offline-only review of additional bounded structural
predicates using synthetic fixtures and source-backed syntax reasoning, without
broadening value output. This work package does not authorize that follow-up or
another Docker call. No safe immutable mapping emerged; any future observation
or exact registry verification requires separate explicit approval. Preserve
API-first/UI-second sequencing. Local metadata is not proof of index/manifest/
config provenance, deployment equivalence, Azure health or a working public UI.

**OCI provenance UNVERIFIED; publication necessity NOT PROVEN.**
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## WP72 ? offline RepoDigest structural diagnostics ? 2026-11-19

Fresh main `94029c6036435efb6a85af5c5dcade14f38d59db` contains required merged
#467/WP71, #466/WP70 and #465/WP69 commits. Work used an isolated clean branch;
owner changes, PR #457 and prior WP65?WP71 evidence remain untouched.

Reviewed the existing IMMUTABLE_REFERENCE, PORT_REFERENCE, EXPECTED_REFERENCE,
reference_category and project_repodigests implementation, existing synthetic
tests and prior evidence. Repository filename search for reference/regexp/
grammar/distribution material found no authoritative Docker/OCI grammar/parser;
matching repository reference scripts and application fixtures are not format
specifications. No external source, package/credential cache or raw observation
was accessed. This is a bounded repository review, not a claim that no such
material exists anywhere on the host. No rule below claims universal OCI validity.

Decision: **no helper/production change**. Reliable local evidence does not
justify expanding the grammar or adding another category. Added four tests and
a compact 16-case synthetic matrix to the existing test file. Categories below
are deliberately conservative current behavior, not authoritative validity:

| Synthetic candidate shape | Current category and limitation |
| --- | --- |
| localhost; single-label authority; localhost with port | MALFORMED_REFERENCE; unsupported authority recognition, actual validity not established |
| bracketed IPv6, with and without port | MALFORMED_REFERENCE; unsupported, validity not established |
| tag plus immutable digest | MALFORMED_REFERENCE; tag/digest combinations not recognized |
| repeated slash, double underscore, double hyphen in path | MALFORMED_REFERENCE; separator exclusions may reject legitimate syntax |
| uppercase host; uppercase path | MALFORMED_REFERENCE; case behavior not authoritatively validated |
| dotted lowercase DNS with canonical port 5000 | UNSUPPORTED_REFERENCE_SHAPE; existing heuristic only |
| leading-zero port | MALFORMED_REFERENCE; deliberately ambiguous/unsupported |
| URL scheme; synthetic userinfo | MALFORMED_REFERENCE; never treated as safe image references |
| dotted lowercase DNS other repository with full digest | OTHER_REPOSITORY_DIGEST; structural only, no publication claim |

Every matrix string uses a synthetic lowercase full sha256 suffix. All 16 cases
are tested individually and mixed with the exact expected repository. A full
16-entry collection proves bounded aggregate counts; a seventeenth entry stops
before classification. Another test compares different synthetic lengths,
characters and ordering with equal categories: outputs are identical, with no
substrings, hashes, lengths, samples or per-position diagnostics. Output keys
and fixed category keys are asserted exactly. Existing tests retain malformed/
uppercase digest, duplicate JSON key, exact-ID, null/empty/schema, raw-size,
count-cap, nonstring and import/default process/network denial coverage.

| Actual offline validation | Result |
| --- | --- |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q` | 30 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q` | 18 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q` | 31 passed, exit 0 |
| `python -B eng/docker/release-1.15/wp65_local_image_metadata.py` | PLAN_ONLY, exit 0, dockerOperations 0 |

Total **79 tests passed, zero failures/skips**, including four new tests (matrix
subcases are not separately counted). Syntax compilation, UTF-8/LF/no BOM,
Markdown links/tables, git diff --check, exact two-path test/documentation scope,
unchanged helper bytes/command construction and prior evidence byte-prefix
preservation passed before PR publication. No new dependency or category.
All-or-nothing exact allowlist, duplicate/ID/size/count guards, plan default,
10-second request and Windows Job/pipe cleanup implementation remain unchanged.

Actual WP67/WP69/WP71 strings were not accessed, inferred or retrospectively
reclassified. Their historical outcomes remain unchanged. Synthetic coverage
cannot determine a safe immutable mapping from discarded metadata. No rebuild
or publication requirement is established.

Smallest separately approvable next action with a new benefit: offline review of
an owner-supplied public authoritative reference-grammar excerpt, without any
real RepoDigest value. It could resolve selected unsupported syntax accurately
with zero runtime/secret exposure. No new image observation is proposed merely
to repeat the same fixed-category result. If later a distinct bounded diagnostic
is justified, a real operation would still need separate explicit approval.

**Offline structural diagnostics only.** Zero Docker commands (including context),
registry/GHCR HTTP, Azure, credentials, providers, public probes, runtime,
export/build/pull/image-push/tag/container/volume or deployment operations.
Only ordinary Git/GitHub branch and PR publication occurred. API-first/UI-second
order and existing auth/TLS/storage/ports are unchanged.
**OCI provenance UNVERIFIED; publication necessity NOT PROVEN.**
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.


## WP73 ? authoritative reference grammar review ? 2026-11-20

Fresh main `d72f7e5d417b5c6805ea3de7fadd1354931ddf1e` includes merged #468.
Isolated branch; owner changes and all previous evidence preserved byte-for-byte.
No vendored canonical grammar was identified by the repository filename search.
Four narrowly scoped public source-document lookups were performed through the
browser tool, without image/repository-specific query metadata or registry calls.
No source files/packages were downloaded into the checkout or installed.

Source provenance (retrieved 2026-11-20; version tags, not independently resolved
commit hashes or release dates; no claim of latest-version behavior):

- [R: distribution/reference v0.6.0 regexp.go](https://github.com/distribution/reference/blob/v0.6.0/regexp.go), constants and referencePat, lines 37?119.
- [P: distribution/reference v0.6.0 reference.go](https://github.com/distribution/reference/blob/v0.6.0/reference.go), Parse, lines 173?215.
- [N: distribution/reference v0.6.0 normalize.go](https://github.com/distribution/reference/blob/v0.6.0/normalize.go), ParseNormalizedNamed/ParseDockerRef/splitDockerDomain, lines 50?158.
- [D: opencontainers/go-digest v1.0.0 algorithm.go](https://github.com/opencontainers/go-digest/blob/v1.0.0/algorithm.go), algorithm regexes and Validate, lines 29?57 and 163?178.

These are authoritative implementation sources for those versions, not a claim
of universal normative OCI validity or the installed daemon's serialization.
P applies path-length checks and digest.Parse after regex matching; an excerpt
or regex match alone is not complete validation. No Go parser was executed.

| Candidate | Source-backed distinction | Unchanged local policy |
| --- | --- | --- |
| localhost / single-label | N treats localhost specially; a lowercase bare first component may normalize as a namespace | malformed category, withheld |
| ports | R accepts decimal port digits without local numeric/canonical restrictions | selected canonical ports unsupported; others malformed |
| bracketed IPv6 | R has bracketed hex/colon syntax with exclusions | malformed, withheld |
| tag plus digest | N accepts and removes tag in ParseDockerRef | malformed, withheld |
| double underscore / repeated hyphen | R permits both within components | malformed, withheld |
| repeated slash | R requires nonempty components | malformed, withheld |
| uppercase host / path | R allows domain case but lowercase path atoms; N checks remote-path lowercase | malformed, withheld |
| schemes / userinfo | R reference shape provides neither URL scheme nor credentials | malformed, withheld |
| digest syntax | R regex is broader than algorithm validation | exact sha256 lowercase emission only |

D requires exact encoded length and lowercase hex for supported SHA algorithms;
algorithm availability is another check. Thus uppercase sha256 hex may match R's
broad regex but fail D validation. SHA512 is recognized by D with a different
length; that does not make it acceptable under this repository's emission rule.

Decision: **helper unchanged**. Sources establish that MALFORMED_REFERENCE is a
local-policy rejection bucket and may contain upstream-recognizable syntax.
Automatically relabeling it would require a defined subset and semantic checks;
porting a full parser or inventing a broad new category is unnecessary here.
Existing OTHER_REPOSITORY_DIGEST also remains structural, not upstream parser
attestation. No real discarded value is identified by the comparison.

Added two synthetic test methods: eight upstream-recognizable candidate shapes
mixed with the allowed repository remain fully withheld; six digest variants
(uppercase, short/long sha256, sha512, uppercase/unknown algorithm) remain
UNVERIFIED with no emitted values. No actual values or upstream code copied.
Existing matrix/count-limit/aggregate-equality/import-default denial coverage
continues to pass; subcases are not separately counted as tests.

| Actual offline command | Result |
| --- | --- |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -q` | 32 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q` | 18 passed, exit 0 |
| `python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q` | 31 passed, exit 0 |
| `python -B eng/docker/release-1.15/wp65_local_image_metadata.py` | PLAN_ONLY, exit 0, dockerOperations 0 |

**81 distinct tests passed, zero failures/skips.** Syntax compilation,
UTF-8/LF/no BOM, Markdown link/table sanity, prior evidence byte-prefix,
unchanged helper bytes/inspect argv/formatters and exact two-path scope plus
`git diff --check` passed. No command/deadline/Windows Job/pipe behavior changed.
Exact allowlist, all-or-nothing output, ID/duplicate/1 MiB/16-entry/null/empty/
unknown guards and flags remain unchanged.

Operation inventory: four public documentation/source-browser lookups plus
ordinary Git/GitHub publication. Zero Docker commands, registry/GHCR API,
Azure, credential, provider/API, public runtime probe, Go/toolchain, package
install/dependency upgrade, image/runtime/data or deployment operation.
Offline tests require no network. No raw WP67/WP69/WP71 values accessed or
reclassified; their historical outcomes are untouched. No OCI-chain, registry
reachability, publication, Azure or UI acceptance established. API-first/UI-second
order and auth/TLS/storage/ports preserved.

Next offline question: if distinguishing specific rejected syntax is necessary,
define a minimal source-backed subset with digest/name semantic constraints and
prove bounded aggregate-only behavior before any separately authorized real
observation. No inspection, registry query, rebuild or publication follows
from this review. It cannot determine the discarded value's syntax.

**OCI provenance UNVERIFIED; publication necessity NOT PROVEN.**
`PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

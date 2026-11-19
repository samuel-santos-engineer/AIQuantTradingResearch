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

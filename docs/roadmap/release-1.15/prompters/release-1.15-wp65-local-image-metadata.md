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


## Offline projection correction ? 2026-11-16

Owner authorized repository/synthetic work only on existing PR #461. Fresh references matched expected head509371be1c195b4e0b5456f46d43ca99816bd6c9 and mained39e0a0ca18e6316a5738651fce88aa28f2990f; PR OPEN/CLEAN/MERGEABLE, no reconciliation/history rewrite needed. Original CLI_FAILED and separately approved exit1/0.395second/templateError evidence above preserved unchanged. ZERO Docker commands (including context/version), daemon/image/registry/Azure/credential/private-file/runtime operations in this correction phase.

The exact original failing expression remains UNKNOWN: sanitized prior attempt retained category only. Original template directly traversed .Config.User/.Config.ExposedPorts/.Config.Entrypoint/.Config.Cmd and unguarded top-level fields. Static review identifies nil/absent/wrong-shaped Config, absent optional keys under strict missing-key behavior, and incompatible root representations as potential failure classes; it does not establish which occurred on the real image. No image absence/corruption/nonpublication inferred. Actual Go evaluation was NOT RUN because go was absent on PATH and standard local installation locations. No toolchain installation/download attempted. A Python string assertion alone is not counted as Go-template qualification.

Correction replaces unchecked traversal with fixed map index lookups and explicit root/Config type guards. Only Id/Os/Architecture/RepoDigests and the four Config-derived User/ExposedPorts/Entrypoint/Cmd fields are serialized; no full object, Env/labels/history/filesystem/layers. Unsupported roots become an eight-null-field object; missing/nil/wrong-shaped Config becomes null Config-derived fields, which parser rejects as UNVERIFIED. A typed root is not assumed compatible with decoded-map representation; fallback remains fail-closed. Wrong/missing required string/port/array types yield UNVERIFIED; a genuine checked contract contradiction remains MISMATCH. Nil Cmd remains valid optional empty command, but false/empty string/dict no longer coerced to an empty list. Port names must be1?65535/tcp|udp. Safe RepoDigest rules unchanged. Raw arguments stay in memory only; output contract booleans and known full refs only. TEMPLATE_GO_EVALUATED=False remains explicit; current corrected template is NOT claimed real-Docker-qualified.

A self-contained standard-library **Go text/template harness is embedded in test_wp65_local_image_metadata.py as GO_HARNESS**. It registers Docker-like json serialization, uses strict missingkey=error, executes both original and corrected templates, and compares rendered JSON objects without printing fixture/raw error content. Fixtures cover decoded Docker-like inspect objects, absent/nil/wrong-type Config, missing/nil optional fields, missing root fields, wrong value types, non-object root and a representative typed root. Go struct-versus-map coverage is synthetic, not proof of the installed Docker formatter implementation. Harness reports only aggregate evaluation/failure counts. It is designed to demonstrate original semantic failure cases and corrected safe execution; until executed, these remain expected tests, not observed Go results.

Runnable qualification: use the existing WP65 unittest command below on a host with an already available local Go toolchain. The Go test writes a temporary main.go from GO_HARNESS, invokes installed go run using standard library only, GOPROXY=off/GOSUMDB=off/GOTOOLCHAIN=local/GO111MODULE=off/CGO_ENABLED=0/GOTELEMETRY=off and a new owned temp GOCACHE,90second cap. No module downloads/toolchain switching, Docker or network. It removes only its temporary harness/cache. If Go remains unavailable, the test explicitly SKIPS; do not treat that skip as success or set qualification flag. No runtime dependency added to the application/deployment images.

Actual Windows-host checks:

```text
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp65_local_image_metadata.py -v
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -q
python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q
python -B eng/docker/release-1.15/wp65_local_image_metadata.py
```

WP65:12 discovered,11passed/1skipped (GO_TOOLCHAIN_UNAVAILABLE: actual text/template evaluation NOT RUN), exit0. WP64:18passed; parser/collector:31passed, unchanged; total **60passed,1skipped,0failed**,61discovered. Default plan exit0, templateGoEvaluated=false/no daemon call. New Python cases verify absent/nil/wrong-type fail-closed behavior, optional Cmd distinction, non-assumed flat Config, invalid ports and safe harness/template scope. Syntax, UTF-8/LF/no BOM, Markdown links/tables, exact helper/test/evidence scope and git diff --check validated before publication. Existing OCI/parser/collector semantics unchanged.

Next smallest action: offline execution of the supplied Go harness using an already available qualified toolchain, not another Docker attempt. After successful actual evaluation and owner review, separately request one exact-ID desktop-linux inspect with10second/1MiB/numeric-exit/sanitized-error/owned-cleanup limits. That future inspect remains NOT authorized by this correction, and exports/registry/Azure operations remain out of scope. No safe metadata was obtained from a real image in this task. **WP61 UI OCI provenance UNVERIFIED**, no deployable registry reference or publication necessity established. PR #461 OPEN/UNMERGED; PUBLIC_RUNTIME_VERIFIED=False; liveAttemptReady=false.

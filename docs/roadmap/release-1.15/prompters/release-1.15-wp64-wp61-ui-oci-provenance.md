# Release 1.15 WP64 ? accepted WP61 UI OCI provenance

Date: 2026-11-16 (America/Sao_Paulo). Result: **UNVERIFIED ? no deployable UI registry reference established**.

**ZERO Azure operations; ZERO registry HTTP requests; ZERO Docker daemon/image operations.** No credentials/tokens/SCM/logs/public probes, image build/pull/push/tag, retained runtime/volume/private-data access or deployment. Ordinary repository/GitHub reads and PR delivery only. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

## Baseline and evidence inventory

Verified current main `2e918ed1443194ffc97a29ed7eaf767ac6b4e26f` includes merged #459; #458 is5217e1959a3a5ed5283c9874f2fd79b3d1a51ec3. Fresh isolated branch. Separate PR #457 remains OPEN at7b3fdabf620619139ea26ac53dbabdb40bea3d6d; not modified, merged or used as a dependency. Existing parser/collector files/tests and Windows LIVE_ADAPTER_QUALIFIED=False unchanged.

Reviewed [WP61 accepted image/build/acceptance](release-1.15-wp61-post-merge-local-browser-qualification.md), [historical Phase G publication chain](release-1.15-wp58-phase-g-staging-deployment-evidence.md), [prior provenance/collector evidence](release-1.15-setting-name-presence-and-image-provenance.md), [WP62 artifact records](release-1.15-wp62b-bounded-quota-event-observation.md), [UI Dockerfile](../../../../container/release_1_15/research-chart.Dockerfile), [configured UI historical metadata](../RELEASE_1.15_WP58_PUBLIC_RUNTIME_QUALIFICATION_PLAN.md). Recorded observations are not fresh Docker/registry attestations.

| Identity kind | Accepted WP61 UI | Evidence and limit |
| --- | --- | --- |
| Local Docker image identifier | sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 | Exact WP61 receipt. Do not presume config, index or child kind until raw metadata links it. |
| Source commit | cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b | WP61 build/merged-source receipt; not a current image revision-label read. |
| OS/architecture/user/port | Linux/amd64, UID/GID12002:12002,8501 | Recorded local image contract and Dockerfile. No new runtime inspection. |
| OCI index digest | UNKNOWN | No complete latest WP61 index receipt in reviewed tracked evidence. |
| Linux/amd64 child digest | UNKNOWN | Cannot derive cryptographic child digest from local image ID alone. |
| Image config digest | UNKNOWN | Same-format local ID is not proof of config kind. |
| Published repository | ghcr.io/samuel-santos-engineer/aiquanttradingresearch is existing application repository | Source/publication evidence; accepted WP61 published mapping UNKNOWN. |
| Published tag / RepoDigest | UNKNOWN | Old Phase G UI tag/index3270ce6e belongs to WP57, not proof of latest WP61. Do not invent a WP61 registry tag. |
| Azure configured UI reference | sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07 | Full exact historical configured reference/older synthetic publication; actual running bytes and current state UNKNOWN. |
| Relationship configured reference to WP61 | UNVERIFIED | Different digest kinds/unknown mapping, not a proved mismatch. |

Local WP61 receipt records90 fake tests and browser BTC/ETH candle/SMA/navigation/inspection/comparison acceptance; that proves neither registry publication nor current Azure data. The manifest/config chain recorded in Phase G for its old UI cannot be relabelled WP61. Accepted API child2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3 remains preferred behavioral baseline; diagnostic d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c stays separate.

## Pure offline verifier

[Verifier](../../../../eng/docker/release-1.15/wp64_oci_provenance.py) accepts an explicit root digest plus a dictionary of digest-to-raw-bytes fixtures/streams already supplied by the caller; no file discovery, daemon, registry, CLI or credential code. Python API: verify(root_digest, blobs, expected_local_id, publication_receipt). Default expected_local_id is the recorded WP61 identifier; tests explicitly use synthetic fixture IDs, never pretending to reproduce real WP61 bytes. CLI default prints no reference; no live switches.

Raw SHA256 checked BEFORE decoding JSON or trusting index/manifest/config descriptors. Cap1MiB per supplied metadata byte object, duplicate JSON fields rejected, schemaVersion2 and recognized OCI/Docker media types required. Index must select exactly one linux/amd64 child; direct runtime manifest is separately supported. Descriptor media/digest/size checked, config raw bytes verified against descriptor, foreign URLs rejected. Layer descriptors validated but layer contents neither read nor hashed. Config requires Linux/amd64, UID12002 (optional matching group12002), exposed8501/tcp, exact reviewed Streamlit Python/script prefix, unique explicit listen8501/address0.0.0.0 flags, and research mode ENV marker1. Missing/duplicate listen evidence stays UNVERIFIED; verified conflicting platform/user/port/startup contract yields MISMATCH. Source revision label checked against accepted commit only if present; absent label stays UNKNOWN. No config ENV/history/arbitrary labels/values/paths or raw errors emitted.

A local image ID maps only if it equals the verified digest of exactly one recognized object in the supplied chain: index, runtime manifest or config. An arbitrary similar-formatted ID is not a chain. A complete local chain without publication evidence remains UNVERIFIED for deployable registry reference. Publication receipt must explicitly bind existing exact repository/root/runtime/config digests and recorded-readback kind; incomplete receipt remains UNKNOWN, explicit conflicting receipt MISMATCH. Receipt authenticity/provenance is an external trust input, not cryptographically authenticated by a JSON field: synthetic/forged fixtures do not prove a real registry event. MATCH means supplied metadata and a separately trusted recorded receipt are internally consistent, NOT fresh pull availability, signature verification, layer completeness or public runtime readiness. An established reference is returned only for that qualified recorded case. No real WP61 chain/receipt was available or verified in this task.

Status semantics: MISMATCH may describe a verified UI-contract or explicit receipt contradiction; it must not be confused with proving that the historical configured UI differs from actual WP61 when its chain is absent. Missing/malformed/oversize/tampered/incomplete/ambiguous metadata yields UNVERIFIED, not nonpublication. Current default result remains UNVERIFIED with deployableReference=null.

## Unresolved links and minimal separately approvable observation

| Missing evidence | Smallest useful observation | Interpretation boundary |
| --- | --- | --- |
| What kind of digest is WP61 local ID? | Exact local metadata/export described below | No daemon action performed now; absent image stops, no pull/rebuild. |
| Complete local index/child/config bytes | Read metadata only from a separately approved export; hash raw bytes with this verifier | No layer/private-content inspection needed for this metadata task; full export itself copies layers and must be explicitly approved. |
| Published immutable mapping | Only after local kinds resolve, exact repository metadata readback under separate approval | Missing RepoDigest/404 is not proof of nonpublication. |
| Current Azure running UI identity | Outside WP64; requires separately bounded attributable evidence | Configuration reference alone does not attest runtime. |

**Preferred next exact footprint, NOT EXECUTED:** separate approval for at most two local Docker operations against only sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69. (1) One docker image inspect,10seconds, project only Id/Os/Architecture/User/ExposedPorts/Entrypoint and safe RepoDigests (no ENV/history/secret labels). Stop if absent, wrong Linux/amd64/UI contract, unexpected identity, output unsafe or timeout. (2) If still needed, one docker image save of that exact ID to a NEW owned Linux0700 directory, export file0600,120seconds and hard2GiB file/output cap enforced before writing beyond the limit; no pulls/builds/retag/container/mount/volume/private-source access. If a hard cap/ownership cannot be guaranteed, do not export. Export copies the whole accepted image including layers; owner must approve that footprint explicitly, though offline inspection reads only OCI/Docker manifest/index/config metadata without extraction/execution or opening layer file contents. Metadata objects capped1MiB, archive paths never used for extraction. Verify exact raw chain, match the accepted local identifier by recognized kind, record safe hashes/contract checks only. Remove only verified-owned export/temporary artifacts after safe completion; retain no raw config/history. No registry access in this local phase. This resolves the local-kind uncertainty before a speculative public lookup. No Windows ACL qualification requirement for Linux.

The prior #459 candidate GET /manifests/sha256:3e323... is still only a candidate local-ID lookup, not a known published manifest. Prefer the local footprint first. A future separately approved registry footprint can then be maximum3 metadata GETs (exact known root index or runtime, selected child if index, config blob),10seconds/1MiB each30seconds total, exact existing repository only/no layer download.401 means authentication/access unresolved, no token/login fallback;404 means no manifest at that exact requested candidate, not proof image never published; redirect means original source did not provide permitted bytes directly, no automatic host traversal; all stop without retry/discovery/tag enumeration. Recognized schema and exact raw hashes mandatory. If anonymous token or redirected config delivery is unavoidable, obtain exact extra-request/host authorization rather than hide it in the budget. No operation executed here.

**Publication decision: NOT PROVEN NECESSARY / still UNKNOWN.** Prefer an already published immutable equivalent if a complete chain and recorded/readback link establish it. Do not rebuild merely to recreate missing provenance; do not publish before establishing the exact accepted artifact/payload and obtaining separate authority. Current exact deployable WP61 UI reference: NONE ESTABLISHED.

## Deployment boundary, unchanged

Accepted API first (port52731), accepted UI only after API runtime identity/health/authenticated BTC and ETH and data readiness pass. UI Azure route WEBSITES_PORT=8501, image Streamlit0.0.0.0:8501, server-side exact research HTTPS endpoint/shared API key/default public CA contract preserved. No assumed portability of Docker local DNS, private volume/CA/PFX mounts or host paths. No real key reads/rotation, Azure imports/data mutation or volume deletion. Missing Azure state stops chart acceptance. No UI/config/port/startup/storage/security code edits, Windows helper activation or diagnostic API substitution.

## Actual offline validation

On genuine Windows host, no Linux/daemon/network test invoked:

```text
python -B -m unittest discover -s eng/docker/release-1.15 -p test_wp64_oci_provenance.py -v
python -B -m unittest discover -s eng/docker/release-1.15 -p 'test_setting_name_*.py' -q
python -B eng/docker/release-1.15/wp64_oci_provenance.py
```

**18 WP64 tests passed +31 existing parser/collector tests passed =49 passed,0 failed/skipped**, both suites exit0; default verifier exit0/UNVERIFIED/no deployable reference. Synthetic coverage: direct vs index, child/config hashes/sizes, platform ambiguity, raw tampering, wrong platform/user/exposed/listen/entrypoint/research mode, absent or contradictory revision evidence, missing/contradictory publication receipt, misleading local IDs, malformed/duplicate/oversize/media JSON, redaction, default no-network/no-Docker and no-execute option. No real artifact cryptographic claim inferred from synthetic tests. Python compile-only syntax, UTF-8/LF/no BOM, Markdown links/tables, git diff --check and three-file scope verified before publication. Existing parser/collector source/tests byte-identical to main. Prior chronological evidence and separate #457 unchanged.

PR OPEN/UNMERGED for review. **ZERO Azure/registry/Docker/credential operations.** `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false`.

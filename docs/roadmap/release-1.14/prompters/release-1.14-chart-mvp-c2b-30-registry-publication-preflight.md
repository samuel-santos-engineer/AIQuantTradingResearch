# C2B-30 ? Exact-image registry publication and digest readback

**PASS. GO for owner review of separate staging authority only.** Image bytes were published to the owner-approved existing public GHCR package; this does not authorize staging, production, merge or public application activation. PUBLIC_RUNTIME_VERIFIED=False; Azure deployment NO-GO; advanced WP07 ON HOLD. Azure staging and production were not modified.

## Owner disposition and provenance

Initial preflight stopped correctly at public visibility, recorded in this PR's commit a52b6326f03364804e3831ce58a72cb286e7d1c1. Owner then replied **approved. continue** in direct response to the request for explicit permission to publish this exact synthetic candidate in the existing public GHCR package, including visibility/data-publication rights. That specific blocker is resolved; no package-visibility change, new registry, production export beyond this candidate, or release-gate waiver is inferred.

C2B-29 PR #350 remains OPEN/unmerged at e909b3ef1ade83959f41dd15600937e3fdb4ed9d. Owner approval is from conversation/C2B-30, not an invented GitHub review; reviewDecision remains empty. Runtime source commit 346841f6478d21d145b850272a5e0d3af3dcc1fb, with later #350 evidence-only changes. Main baseline 612483e570d4b8e1f99d512681ca26b1bc1320cd. #350 explicitly cherry-picked #347 with provenance; no duplicate implementation, merge or rebase here.

Independent PRs #346 (0e568d208f2769037bf4a7265ea4ba6f4609aacc), #347 (8240d0878dea335c08ad3216600fc7e545446140), #349 (c40ba4e1ba751b5097061d95d76ce81bbd019c0e) and #350 were rechecked OPEN and remain untouched. This continuation updates only the two C2B-30 Markdown/JSON evidence paths on the same evidence PR, preserving its earlier blocked history. Original checkout branch/HEAD and owner documents are preserved.

## Exact publication identity

Repository: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch` (existing visibility **public**, explicitly approved).

New tag: `c2b30-55d142b57d0d-20261016t210908z`. Verified absent with authenticated preflight and an anonymous registry 404 immediately before push; not latest or an existing deployment tag. This is a unique candidate label, not a claim that GHCR enforces tag immutability. Future reference must use repository@D.

- Platform D, Linux/amd64: `sha256:55d142b57d0df9953f01350263369ee2ecd9a66019d1844d1f424ea91e0999d5`.
- Media type: `application/vnd.oci.image.manifest.v1+json`; raw size 2574 bytes.
- Config: `sha256:27ec5f7297205ccc4d0ba5c7987effc2c8aa710904f6850ea63d16d51e69f56d`.
- No enclosing registry index was needed or published; resolved tag is D directly. Local archive index SHA is separately labeled in JSON and must not be substituted for D/config/image ID.
- Push UTC: 2026-10-16T21:10:38.2990372Z through 2026-10-16T21:11:03.6915988Z. Independent readback UTC: 2026-10-16T21:11:19.746426+00:00 through 2026-10-16T21:11:47.229465+00:00.

Archive path was obtained from C2B-29 evidence:
`C:\Users\sabsf\AppData\Local\Temp\aiq-c2b29-d76483136fbf46e79029e45d38c83807\qualified-image.tar`.

Archive SHA-256: `700b49d2f0bacb7db74b5520faabed3b94519355cd54daabc3f6be11bf0ea7a0`, 250158592 bytes. Rehashed unchanged; complete platform/config/layer blobs independently verified for digest and descriptor size. Retained intact. No rebuild, Docker commit, producer, generation, manifest conversion or runtime launch occurred.

## Preflight, transfer and independent readback

| Check | Verdict |
|---|---|
| Archive/config/all twelve ordered layer descriptors | PASS digest, size and media type against accepted #350 evidence |
| Fresh immutable bundle inspection | PASS exactly 13 files, 656176 bytes; every member hash/size matches; manifest anchor `007b2c8a6ccec19ec54976b4baf735812b648d79294e6465df884c68537d3069` |
| Asset metadata | PASS root-owned UID 0, 0444 files, 0555 directories, no links, finite in-memory inspection of original archive layers |
| Startup provenance | Accepted C2B-29 read-only/non-root tests retained; config independently confirms linux/amd64, 1000:1000, /usr/local/bin/aiq-entrypoint. Read-only root is a launch restriction, not an OCI config attribute; no C2B-30 container started |
| Registry authorization | Existing package owner matches authenticated Docker/GitHub account; write:packages verified, values never logged. Docker uses its existing credentials; no login or credential-store change. Repository pull/push token requested in preflight; GHCR token is opaque, no JWT-grant claim fabricated |
| Tool preserves exact bytes | Docker 29.7.2 OCI import preserved descriptor D/media type; tagging preserved D; push reported D; independent raw readback equals saved platform manifest bytes |
| Independent registry readback | PASS anonymous registry API fetch of raw platform manifest, config and all twelve layer blobs; all raw SHA-256 and byte counts match (250138989 config/layer bytes streamed, not retained) |
| Intended pull model | PASS registry-side anonymous full-image read, appropriate to approved public package. Actual Azure networking/credentials/pull behavior remains untested and requires separate staging authority |
| Publication scope | Only new candidate tag, no existing tag repointed, package visibility unchanged, no image index conversion |

Upload used Docker; readback used independent Python standard-library Registry API requests and SHA-256. Anonymous token scope is repository pull only; cross-host blob redirects strip Authorization and require HTTPS. Secrets stayed in process memory or existing credential helper; no secret arguments, saved credential files, screenshots or log values. Existing broader owner credential was not replaced; registry operations are confined to this repository/tag. Readback config confirms the same non-root entrypoint; exact raw layer equality binds the freshly inspected source assets to registry contents without executing them.

| Order | Digest | Bytes | Media type | Archive | Registry bytes |
|---|---|---:|---|---|---|
| 1 | `sha256:ecc510c1e359bdc007b39802570bb7e4dec7d6ebccf357c3201a74299113e05e` | 29843350 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 2 | `sha256:65a67aec859c5fafe6ca38c2ed07825c97acf3febdcf62b79441a1bfca4f3a4a` | 1294038 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 3 | `sha256:3a5db7051419a594a866378c5d2c422e3d282d69e6792b3213772cb1349da446` | 11923091 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 4 | `sha256:2f5b06adfd9138ef928aaf8b34f3fecf122b137a537133dfe421509dd57b4a47` | 250 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 5 | `sha256:339a6880d01fa9aea11f4c30fe31f11d1f2c2d840ef821b59371d571e70a8842` | 93 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 6 | `sha256:160bacecf7a44fcd8b753ef1d8f483b00bb62a26ac7f6529617820cda1804940` | 902 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 7 | `sha256:d632e47ebcd5fe89f2e5d468c0819f905f7df3b82c87651b987244e18b4785b9` | 206527386 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 8 | `sha256:7de1f9253283aaad74381b8a6b6d0d6bfbcd9a28012a80a3bcd555decf7ed289` | 63578 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 9 | `sha256:43afc06075e26de4563ab5f540daff18d42c6b80135f1c21e85ec0d679fcdb83` | 7691 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 10 | `sha256:ca98ee862b4d6e3645000e4803a481181427b99c4b74430cc08832ceabf83eef` | 2242 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 11 | `sha256:0e17b7cb9edfe3363b73080b01629e3e126e51b99869ef0f8d829fd8818b5f90` | 122804 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |
| 12 | `sha256:2f0b2bcde2bf94bc119a79ce772c2a70e29561bd6f38901cc757c30b56c86e83` | 345057 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS | PASS |

## Deployment automation and Azure preservation

Repository API: zero Actions workflows and zero repository webhooks. Read-only checks on aiqr112wp035ec325382770 and aiqr112wp05wus27f5eabb5: scmType=None; DOCKER_ENABLE_CI absent; both effective configured image references remain pinned before and after publication to `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:144de4a9c393f07d2249f450b9d5ab2af34f36b941897a31bd20d72ac325959a`. No enabled deployment automation was observed in these configured mechanisms; publishing a new unused tag does not repoint those digest references. No Azure write, restart, deployment, RBAC, billing/resource or application request test occurred.

Microsoft documents custom-container CD as explicit enablement/webhook setup ([App Service CD](https://learn.microsoft.com/en-us/azure/app-service/deploy-ci-cd-custom-container), [CLI enable-cd defaults false](https://learn.microsoft.com/en-us/cli/azure/webapp/deployment/container?view=azure-cli-latest)). These references support interpreting the inspected configuration; they are not a proof that every possible third-party external observer is absent. No configured trigger that could deploy this candidate was found. The old Azure digest is not qualified as a rollback.

## Sanitized commands and limits

GitHub CLI 2.97.0, Docker Engine 29.7.2, Python 3.13.15, Windows PowerShell 5.1. Complete timestamps/descriptors/results in [JSON](release-1.14-chart-mvp-c2b-30-registry-publication-preflight.json).

```powershell
# Windows PowerShell 5.1; paths from retained C2B-29 evidence.
gh api users/samuel-santos-engineer/packages/container/aiquanttradingresearch --jq .visibility
gh api repos/samuel-santos-engineer/AIQuantTradingResearch/actions/workflows --jq .total_count
gh api repos/samuel-santos-engineer/AIQuantTradingResearch/hooks --jq length
# Read-only per existing site: config show projected linuxFxVersion/scmType;
# appsettings list projected ONLY DOCKER_ENABLE_CI; no credential values output.
docker image load --input <verified-qualified-image.tar>
docker image inspect aiq-c2b29:qualified --format '{{json .Descriptor}}'
docker image tag aiq-c2b29:qualified ghcr.io/samuel-santos-engineer/aiquanttradingresearch:c2b30-55d142b57d0d-20261016t210908z
docker push ghcr.io/samuel-santos-engineer/aiquanttradingresearch:c2b30-55d142b57d0d-20261016t210908z
# Independent bounded urllib registry readback: raw manifest/config/all 12 blobs,
# anonymously, each streaming hash checked against certified descriptor size/digest.
[DateTime]::UtcNow.ToString('o')
```

The first token-permission helper assumed JWT, but GHCR returned opaque data. It failed before upload; corrected to verified package ownership + write:packages and did not infer opaque token claims. Initial historical jq/PowerShell timestamp helper failures are preserved in prior commit; neither changed resources. No runtime numerical requalification, package install or unnecessary active experiment was substituted for exact content readback.

Cleanup: no containers, runtime services, ports, build contexts or credentials created. New local registry transfer tag removed; imported exact-image cache retained as aiq-c2b29:qualified (bounded local cache, no process). Source archive and sanitized evidence retained unchanged. No registry deletion or remediation; new published tag retained as authorized. No owner file or global Git setting changed; only this scoped evidence branch is committed/pushed.

## Future staging prerequisites and disposition

**GO for owner review of a separate staging authority, not staging approval.** Use only `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:55d142b57d0df9953f01350263369ee2ecd9a66019d1844d1f424ea91e0999d5`. No rebuild/substitution; recheck registry manifest/config/layers immediately before any separately authorized deployment.

Owner must separately authorize staging and specify site/configuration/rollback. Require target effective digest/entrypoint and intended anonymous pull validation, correct 503 JSON API boundaries (earlier Azure 200-HTML mismatch remains unresolved), an independently approved unavailable rollback image, F1 cold-start/resource/abuse/session screening and independently reviewed applicable G-B0?G-B7 gates. C2B-29 cooperative cancellation, zoom-reset and resource-screening limitations remain; registry equality does not cure them or establish Azure/public-runtime readiness. No production/staging mutation or public application activation is authorized or performed. PR remains OPEN/unmerged; PUBLIC_RUNTIME_VERIFIED=False, Azure deployment NO-GO, WP07 ON HOLD.

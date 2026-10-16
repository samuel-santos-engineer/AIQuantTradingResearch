# C2B-30 ? Exact-image publication preflight: blocked before push

**BLOCKED / NO-GO for owner review of separate staging authority.** No image bytes published, no tag created/repointed, no registry push or readback performed. Azure staging and production were not accessed or modified. PUBLIC_RUNTIME_VERIFIED=False; deployment NO-GO; advanced WP07 ON HOLD.

## Controlling stop condition

C2B-30 authorizes conditional bounded registry promotion, but explicitly requires:

> Do not expose the artifact publicly without explicit approval of package visibility and data-publication rights.

GitHub package API confirms existing `ghcr.io/samuel-santos-engineer/aiquanttradingresearch` visibility is **public**. The supplied authority approves C2B-29 and bounded promotion subject to the visibility/publication prerequisite; it does not explicitly approve public visibility and public publication of this synthetic image. Owner approval of a local candidate is not treated as a waiver. Therefore publication stops at preflight, with no push, visibility change, package creation/deletion or alternative target.

Smallest next action: owner explicitly approves publication of this exact synthetic-only candidate into the existing **public** GHCR package, including its visibility and data-publication rights. Do not change visibility or choose a new registry to work around this boundary. Once separately resolved, resume remaining preflight before any push; that approval alone would not authorize staging, deployment or public data activation.

## Verified provenance and archive

PR #350 is OPEN/unmerged at `e909b3ef1ade83959f41dd15600937e3fdb4ed9d`; GitHub reviewDecision is empty. The supplied C2B-30 authority separately records owner approval of C2B-29; no GitHub review or merge is invented. Main remains `612483e570d4b8e1f99d512681ca26b1bc1320cd`.

Read C2B-29 contract, full changed-file inventory, evidence and JSON descriptors from the preserved #350 worktree. Its runtime source is `346841f6478d21d145b850272a5e0d3af3dcc1fb`; subsequent source PR edits are evidence-only. #350 incorporates #347 using provenance-bearing cherry-picks, without merging its branch. PRs #346 (0e568d208f2769037bf4a7265ea4ba6f4609aacc), #347 (8240d0878dea335c08ad3216600fc7e545446140), #349 (c40ba4e1ba751b5097061d95d76ce81bbd019c0e) remain independently OPEN and untouched. This new PR contains only this Markdown and its JSON companion, based on main; it does not duplicate implementation or import dependency branches.

Archive path was obtained from C2B-29's evidence, not guessed from C2B-30:
`C:\Users\sabsf\AppData\Local\Temp\aiq-c2b29-d76483136fbf46e79029e45d38c83807\qualified-image.tar`.

| Identity/check | Result |
|---|---|
| Archive SHA-256 | PASS `700b49d2f0bacb7db74b5520faabed3b94519355cd54daabc3f6be11bf0ea7a0` |
| Archive size | PASS 250158592 bytes |
| Linux/amd64 platform manifest D | PASS `sha256:55d142b57d0df9953f01350263369ee2ecd9a66019d1844d1f424ea91e0999d5` |
| Config | PASS `sha256:27ec5f7297205ccc4d0ba5c7987effc2c8aa710904f6850ea63d16d51e69f56d` |
| Config platform/user/entrypoint | PASS linux/amd64, 1000:1000, `/usr/local/bin/aiq-entrypoint` |
| Ordered twelve layer descriptors | PASS exact digest, size, media type against #350; archive bytes independently rehashed |
| Immutable bundle | C2B-29 accepted 13 files/656176 bytes, manifest `007b2c8a6ccec19ec54976b4baf735812b648d79294e6465df884c68537d3069`; unchanged archive/layers verified, fresh filesystem extraction not performed after stop |
| Read-only/non-root startup | Prior C2B-29 acceptance retained; config non-root rechecked, read-only mount is a launch setting, not an OCI config attribute; no runtime launched in C2B-30 |
| Registry tag / push time / readback time | None / not run / not run |
| Registry platform/config/layer equality | UNKNOWN, no upload/readback |

D is the raw platform manifest digest, not config/image ID, local archive SHA, enclosing index or mutable tag. The local archive index SHA is recorded separately in [JSON](release-1.14-chart-mvp-c2b-30-registry-publication-preflight.json); no published index digest is claimed.

| Layer order | Digest | Bytes | Media type | Archive comparison |
|---|---|---:|---|---|
| 1 | `sha256:ecc510c1e359bdc007b39802570bb7e4dec7d6ebccf357c3201a74299113e05e` | 29843350 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 2 | `sha256:65a67aec859c5fafe6ca38c2ed07825c97acf3febdcf62b79441a1bfca4f3a4a` | 1294038 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 3 | `sha256:3a5db7051419a594a866378c5d2c422e3d282d69e6792b3213772cb1349da446` | 11923091 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 4 | `sha256:2f5b06adfd9138ef928aaf8b34f3fecf122b137a537133dfe421509dd57b4a47` | 250 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 5 | `sha256:339a6880d01fa9aea11f4c30fe31f11d1f2c2d840ef821b59371d571e70a8842` | 93 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 6 | `sha256:160bacecf7a44fcd8b753ef1d8f483b00bb62a26ac7f6529617820cda1804940` | 902 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 7 | `sha256:d632e47ebcd5fe89f2e5d468c0819f905f7df3b82c87651b987244e18b4785b9` | 206527386 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 8 | `sha256:7de1f9253283aaad74381b8a6b6d0d6bfbcd9a28012a80a3bcd555decf7ed289` | 63578 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 9 | `sha256:43afc06075e26de4563ab5f540daff18d42c6b80135f1c21e85ec0d679fcdb83` | 7691 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 10 | `sha256:ca98ee862b4d6e3645000e4803a481181427b99c4b74430cc08832ceabf83eef` | 2242 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 11 | `sha256:0e17b7cb9edfe3363b73080b01629e3e126e51b99869ef0f8d829fd8818b5f90` | 122804 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |
| 12 | `sha256:2f0b2bcde2bf94bc119a79ce772c2a70e29561bd6f38901cc757c30b56c86e83` | 345057 | `application/vnd.oci.image.layer.v1.tar+gzip` | PASS |

## Remaining preflight and automation disposition

Repository API reports zero Actions workflows and zero repository webhooks. This rules out those repository mechanisms at observation time, not external registry/Azure continuous-deployment hooks. Azure/external deployment hooks remain UNKNOWN; no Azure probes or writes were performed. The existing configured Azure digest `sha256:144de4a9c393f07d2249f450b9d5ab2af34f36b941897a31bd20d72ac325959a` was not repointed and is not a qualified rollback.

Package existence/name/public visibility verified with existing GitHub authentication. Write authorization, least-privilege scope, approved public visibility/publication rights, unused unique candidate tag, content-preserving transfer tool behavior and intended future deployment pull authorization remain unresolved/not run after the mandatory stop. No token values or Docker credential stores were read or output. No package visibility mutation, login, credential export or write probe.

After explicit owner disposition, verify deployment automation cannot fire, write/pull authorization, unused new tag and transfer-tool exact-media preservation. Publish only existing D if eligible, then independently fetch raw registry manifest/config/ordered descriptors and compare bytes/digests/sizes/media types. Any conversion/mismatch stops promotion; do not rebuild/regenerate or infer equality from chart appearance. If publication precedes a failure, preserve the tag and request owner direction rather than deleting it.

## Sanitized commands, scope and cleanup

GitHub CLI 2.97.0; Python 3.13.15; Windows PowerShell 5.1. UTC evidence-record time in JSON; no push/readback timestamps exist. An initial compound jq object expression failed quoting; scalar read-only queries below succeeded. A timestamp command used unavailable PowerShell `Get-Date -AsUTC`; evidence time instead uses Python UTC datetime. Neither failure changed any resource.

```powershell
gh pr view 350 --repo samuel-santos-engineer/AIQuantTradingResearch --json state,headRefOid,reviewDecision,baseRefOid
gh api users/samuel-santos-engineer/packages/container/aiquanttradingresearch --jq .visibility
gh api users/samuel-santos-engineer/packages/container/aiquanttradingresearch --jq .name
gh api repos/samuel-santos-engineer/AIQuantTradingResearch/actions/workflows --jq .total_count
gh api repos/samuel-santos-engineer/AIQuantTradingResearch/hooks --jq length
# Individual dependency PR checks: gh pr view 346 / 347 / 349 --json state,headRefOid
# Read-only Python hashlib/tarfile verification of retained archive and all descriptors.
# PowerShell 5.1 UTC equivalent if needed:
[DateTime]::UtcNow.ToString('o')
```

No Docker import/build/run/push/commit, C# producer, artifact regeneration, new runtime resources, Azure commands, deployment, registry mutation, merge, package cleanup or source-archive deletion occurred. Archive retained unchanged; isolated documentation worktree retained for review. Original checkout branch/HEAD and all owner untracked documents preserved. Git mutations are limited to the separately authorized evidence branch/PR. No global Git settings changed. No additional active runtime tests were necessary or performed after the stop.

## Separate staging prerequisites

NO-GO now. Future staging requires: resolved visibility/publication rights and all remaining preflight; successful exact registry readback and intended pull authorization; separate owner staging authority naming immutable `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@D`; site-specific effective digest/entrypoint verification; correct 503 JSON routes (Azure's earlier 200 HTML mismatch remains unresolved); an approved unavailable rollback target; bounded F1 cold-start/resource/abuse/session qualification and independent applicable G-B0?G-B7 reviews. No local acceptance or promotion result can waive those gates. Staging and production remain unchanged and unauthorized.

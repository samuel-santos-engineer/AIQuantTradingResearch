# Codex Prompt — Release 1.15 WP76: Execute Azure staging deployment

## Owner direction and priority

The owner explicitly directs us to **proceed with actual deployment**, not another diagnostic work package. This is a greenfield personal research lab. SHA256 is the only supported digest algorithm; internal schema compatibility and additional classifier/governance/licensing work are out of scope. Make measurable progress toward a working authenticated BTC/USD and ETH/USD historical candle chart in Azure. Do not merely write another deployment plan.

## Starting point and repository

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Start from current `main` after merged PR #470 (`35a77d663d454624b96219d4ca1a10617eaeac6f`) and inspect the latest open WP75 PR #471 (`https://github.com/samuel-santos-engineer/AIQuantTradingResearch/pull/471`) and its latest append-only evidence before doing anything. Do not overwrite its branch, discard owner changes, or claim #471 is merged. If WP75's approved UI logging-only retry is still pending, treat that as a separate, narrowly authorized operation; do not multiply it into unrelated metadata calls. Reuse the existing WP75 planner/runbook and accepted deployment artifacts. Prefer a small continuation on PR #471 if compatible; otherwise create a narrowly scoped follow-on PR based on a clean current main and reconcile WP75 dependencies explicitly.

## Fixed deployment targets

- Subscription: `7f5eabb5-80f5-4369-88be-b14a6de763f1`
- Resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`
- Shared plan: `asp-aiq-r112-wp05-wus2-7f5eabb5`, West US 2 Linux F1
- API staging app: `aiqr115researchstagingwus27f5eabb5`, port 52731
- UI staging app: `aiqr112wp05wus27f5eabb5`, port 8501
- **Never target** historical production app `aiqr112wp035ec325382770`.
- Registry: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch`
- Accepted API Linux/amd64 child: `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`
- **Verified published WP61 UI Linux/amd64 child**: `sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde`
- Full UI reference: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch@sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde`
- WP61 local ID `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69` was verified as the published **index**, not the runtime child; do not configure Azure to use it.
- Older UI configured image `sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07` must ultimately be replaced by the verified WP61 UI child. Empty startup is correct: preserve it.
- Exclude the diagnostic API child `sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c`.

## Deployment execution authority and boundaries

Owner authorizes proceeding with **actual staging deployment** in this request. Interpret that as authorization to carry out the narrowly identified necessary staging configuration changes **after** fresh, safe verification of targets, preserved settings and rollback state; do not treat it as blanket permission to perform unrelated operations. Execute API-first/UI-second, with explicit stop conditions below. Do not wait for a new planning phase or ask hypothetical compatibility questions.

**Permitted within this deployment goal, subject to preflight and exact target checks:** read-only staging resource metadata needed to confirm target identity and quota; minimal private in-memory baseline of settings and rollback state; merge-only nonsecret staging settings updates; exact accepted API image configuration; exact verified WP61 UI image configuration after API/data acceptance; retain empty startup and HTTPS-only; bounded HTTPS health and authenticated BTC/ETH verification where existing safe credentials and datasets are already available. Do not print or persist secrets, credentials, private datasets or complete settings collections. Preserve existing key, TLS, auth, slot flags, storage settings and unrelated app settings. Avoid replacing whole appsettings collections. Keep all commands exact and logged with sanitized results. Treat App Service saves as potentially recycling containers; do not add explicit restart unless essential and approved.

**Not authorized by this request:** production app mutation, deleting/recreating apps/resources, paid plan upgrade, quota-bypassing actions, new Docker build/push, changing digest algorithm, registry publication, credential rotation, exposing private data, broad data transfer or new public import endpoints, disabling auth/TLS, blind rollback, unbounded probes/retries, unrelated Azure changes. If the required private data transfer/initialization or credential access is not already available through an established approved method, **stop only at that concrete blocker** and report the smallest precise approval/input needed. Do not invent a private-data transport. Do not perform API or UI configuration mutations if the prerequisite rollback baseline cannot be safely captured and checked.

## Required execution sequence

1. **Reconcile latest WP75 evidence:** Confirm whether PR #471 has newer UI logging retry evidence, whether the existing staging API remains configured to the accepted API digest, and whether the verified UI registry child mapping remains as documented. No more digest taxonomy work.
2. **Preflight:** Use existing authenticated Azure CLI/ARM transport, exact subscription/group/app identifiers, and bounded projected reads. Check Linux F1 plan, quota/stop budget, Running/Normal/HTTPS-only, current image/startup, relevant logging, and target identity. The previously recorded UI logging GET transport error is not a deployment failure. If an already approved one-shot UI logging retry is outstanding, respect its single-attempt scope and stop on failure; do not let optional logging metadata cause endless retry loops. Capture a minimal **private** rollback baseline for both apps and verify merge semantics for retained keys/settings before mutation. If CLI transport or baseline is uncertain, stop with exact evidence.
3. **API first:** If API already points to the accepted child, do not needlessly repatch it; otherwise apply the existing WP75 exact nonsecret API settings and accepted image, preserving empty startup and HTTPS-only. Use `eng/azure-cli/r1.15-qualification/wp75_staging_plan.py --phase API` as the reviewed command source, but independently verify installed CLI semantics before applying. Do not overwrite secrets or unrelated configuration. Record before/after sanitized state.
4. **API runtime and authentic data:** Check `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/health` with TLS verification, bounded attempts (maximum 9 over 180 seconds, 8 seconds/request, <=4 KiB body), no redirects. Health `200 Alive` alone does not prove chart readiness. Verify existing private Azure state and the retained same service key without disclosing either. Perform bounded authenticated `/v1/research/snapshot` checks for BTC/USD and ETH/USD with the established client contract. If `DATASET_NOT_READY`, missing UID1654-owned private state, missing key, or inaccessible data is observed, **do not fabricate candles**. Stop with the exact required secure data/initializer step and minimal owner approval needed; do not prematurely switch UI if API data acceptance has not passed.
5. **UI second:** After authentic API BTC/ETH acceptance, use the verified WP61 immutable child digest above, not the old `a06baf…` image or the index/local ID. The WP75 planner command is `python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py --phase UI --ui-digest sha256:6b59d2052fdd99d77aae248aa159a0cadba740a91094fbeb0872935a96877dde`. Merge only the reviewed nonsecret UI settings (port 8501, chart enabled, exact API HTTPS URL), preserve the same existing service key privately, auth/TLS and CA behavior, and configure the immutable UI image with **empty startup**. Do not use the old image after successful promotion. Confirm actual Azure readback.
6. **End-to-end browser acceptance:** Visit staging UI `https://aiqr112wp05wus27f5eabb5.azurewebsites.net/` (not production). Verify authenticated session, BTC/USD -> ETH/USD -> BTC/USD selection, real historical candles and SMA, UTC labels, C# indicators and warmup, honest volume-unavailable disclosure, interval/range and zoom/pan, Return to Live, and no synthetic fallback. Capture only sanitized acceptance evidence. A Running site, HTML shell or successful health endpoint is not sufficient.
7. **Failure handling:** Stop on target drift, quota pressure, ambiguous credentials, transport failures, data-not-ready, TLS/auth failure, or unverifiable preservation. No automatic retries, no speculative fixes, no silent rollback. Record exact step, observed fixed-category error, bounded operation count, cleanup, retained state, and next concrete action. Rollback requires exact previously captured baseline and specific authorization if it changes configuration.

## Evidence, tests and PR lifecycle

Use the existing WP75 runbook at `docs/roadmap/release-1.15/prompters/release-1.15-wp75-staging-deployment.md`. Append chronological evidence; never rewrite earlier observations. If code is necessary, keep changes minimal and focused on actual deployment execution or safe testing; avoid new generic abstractions and classifiers. Run relevant offline tests, at least:

```sh
python -B -m unittest discover -s eng/azure-cli/r1.15-qualification -p test_wp75_staging_plan.py -q
python -B eng/azure-cli/r1.15-qualification/wp75_staging_plan.py
```

Also check Python syntax, UTF-8/LF/no BOM, `git diff --check`, and scope. Distinguish Codex-reported tests from independently observed cloud results. Record exact UTC timestamps, bounded calls, numeric statuses where known, image readback, retained setting preservation, API data result, UI result, and unresolved blockers. Update `PUBLIC_RUNTIME_VERIFIED` / `liveAttemptReady` **only if the actual full end-to-end acceptance passes**, otherwise keep false. Keep PR open/unmerged for owner review; report the PR URL, head/base SHA, changed files, tests, actual operations and result. Do not merge without owner authorization.

**Definition of done:** preferred outcome is staging API serving genuine BTC/ETH snapshots and staging UI running the verified WP61 image with real browser chart acceptance. If a genuine missing private-data transport or secret blocks that, stop precisely there and present one executable next step, not another speculative investigation.

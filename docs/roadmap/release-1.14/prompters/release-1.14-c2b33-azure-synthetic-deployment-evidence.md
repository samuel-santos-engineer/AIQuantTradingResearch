# C2B-33 Azure-compatible synthetic deployment evidence

Owner authorized explicit Azure-compatible immutable synthetic chart mode. Source commit f238ea8 atop #350 plus the disclosure fix. Public data APIs and advanced WP07 remain disabled; PUBLIC_RUNTIME_VERIFIED=False.

Image platform manifest: `sha256:bc88785e108b3b32d02cecc9be84b29e02acf31cabbfda7dfd17bd43c65d45ab`; config `sha256:461568ac33df08c998f8ab0f0fce75c7acbea0660c577859c70c7881598669d3`. Existing immutable manifest and data unchanged.

33 focused Linux tests passed (exit 0): `python -B -m unittest test_local_synthetic_chart test_immutable_synthetic_chart test_public_preview_api -v`. Actual browser 1D/30D SMA off/on/off, hover, zoom/pan PASS in public-synthetic mode. Routes remain 503. A nonsecret inherited provider marker was absent from running Streamlit process environment. Runtime remains nonroot with root-owned read-only assets; no Worker/provider acquisition.

Read-only target verified: `aiqr112wp035ec325382770`, resource group `rg-aiq-r112-wp03-wcus-5ec325382770`. Existing image `sha256:144de4a9c393f07d2249f450b9d5ab2af34f36b941897a31bd20d72ac325959a`; empty startup override. Existing private settings must remain stripped from child environment. Planned deployment disables App Service storage mount, uses explicit public-synthetic mode and port 8501.

Publication BLOCKED by automatic approval review pending explicit owner approval of exporting new repository payload to `ghcr.io/samuel-santos-engineer/aiquanttradingresearch` with unique c2b33-disclosure tag. No registry push or Azure mutation occurred. No production/public verification claim. Local evidence directory: `C:\Users\sabsf\AppData\Local\Temp\aiq-c2b33-fix-57251c2e453f4476ade2996f9805bcf2`.

## Owner-approved publication and Azure deployment

Owner explicitly approved registry publication and deployment. Published tag `ghcr.io/samuel-santos-engineer/aiquanttradingresearch:c2b33-disclosure-20261016t234733z`; anonymous GHCR manifest/config readback matched the qualified digest above. Azure configured image readback exactly matched `bc88785e108b3b32d02cecc9be84b29e02acf31cabbfda7dfd17bd43c65d45ab`. Public-synthetic mode, port 8501 and disabled App Service storage mount applied; app restarted. No secret setting values retrieved or changed.

Azure reports Running/Normal; startup logs show successful container startup. Live root and health 200; all three public data endpoints return 503 DATASET_NOT_READY; unknown API 404. However actual browser shows temporarily unavailable and no chart, including after one native retry. Initial cold-start HTTP/browser checks timed out; longer bounded route retry passed but chart retry failed. Live functional acceptance FAIL/INCOMPLETE, not production verification. Existing validations were not relaxed.

A local-only correction at commit `041a020` adds warning logging for fixed categories only. Diagnostic image `sha256:fde5fb59335b33d1d24da7063fefb23cddc84f816571ddc8500fd1f9eec53ce1` remains unpublished: automatic approval review rejected the distinct payload pending explicit owner approval. Current Azure remains on the approved bc88785e image. PUBLIC_RUNTIME_VERIFIED=False. No speculative timeout, ownership or integrity changes were made. Live evidence: `live-state.json`, `live-chart.png`, `live-routes-retry.txt`, and protected local log archive under the evidence directory.

## Development repair authorization and final successful deployment

Owner authorized continued scoped code fixes and deployments until the failure was resolved. Diagnostic fde5fb identified `ownership`; diagnostic f926b identified **filesystem-root UID 1000, mode 0755** on Azure. The root was writable by the server identity, so accepting it would have weakened the application-directory replacement boundary. No ownership or integrity checks were bypassed; the two-second reader budget remained unchanged.

Correction commit `845235d`: the synthetic-candidate Docker target starts a fixed isolated Python bootstrap as root, checks root-directory owner/mode, clears supplementary groups, sets GID then UID 1000 permanently, verifies all real/effective IDs and empty groups, and execs the existing entrypoint. No server executes as root. This uses installed standard-library functions, no new package/service and no advanced WP07 guardian work. The legacy private target is unchanged. Image Config.User is now root for startup; this is distinct from the running server identity. Local runtime `/proc/1/status` proved all UIDs/GIDs 1000, no groups, CapEff zero and NoNewPrivs=1, with `/` owned by UID 0. Startup requires SETUID/SETGID capabilities until identity drop; full cap-drop without those capabilities deliberately cannot start. Independent live process inspection was not performed; Azure functional success plus the enforced bootstrap/reader checks is the available live evidence.

Final image: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch:c2b33-root-boundary-4bac63a31091`

- Platform manifest: `sha256:4bac63a310918c197216796b201acc58d53edf9b7de884795e888372f002b92d`
- Config: `sha256:bcb184349eaa0eef47242b0ac6c6525385795ebb15fb47c6b7f273a329331ffe`
- Anonymous GHCR manifest/config readback PASS; Azure linuxFxVersion readback matched exact final digest.
- Final focused tests: 33 passed, exit 0, running as UID 1000: `python -B -m unittest test_local_synthetic_chart test_immutable_synthetic_chart test_public_preview_api`.
- Full pinned local read validated 720 visible 30D candles, retaining hidden warmup exclusion and C#-provided SMA.
- Actual Azure browser: 30D 720 rows and 1D 24 rows; native popover SMA false/true/false on both views PASS. Interval 4h and return 1h PASS. Zoom, pan, modebar and synthetic/UTC hover PASS on both views.
- Live native PNG downloaded through Plotly modebar and visually inspected: complete SYNTHETIC / SIMULATED - NOT REAL MARKET DATA title visible. PNG SHA-256 `5b27dd30bf94a098a11d639cb9be7be41e110b48be1b4d370d9ada969e7e48eb`.
- Live root/health 200, three data APIs 503 DATASET_NOT_READY, unknown API 404, reserved admin blocked, no immutable asset/source leak on inspected routes. Cold-start browser/timeouts occurred before app settled; settled final checks passed. This is development smoke acceptance, not a cold-start SLA or broad release/security certification.

Known owner-accepted SMA zoom-reset limitation remains. Data generation, SMA calculation, dataset pin, private credentials and provider integrations unchanged. Existing app setting secret values were not retrieved or changed; child environment sanitization was proven locally with a nonsecret marker. App Service storage mounting is disabled, not deleted. Docker log collection enabled for diagnosis remains available. No PR merged, no paid resource created. PUBLIC_RUNTIME_VERIFIED=False; public raw APIs/advanced WP07 remain disabled/on hold.

Final evidence files: `root-boundary-tests.txt`, `root-boundary-routes.txt`, `final-browser.txt`, `final-controls.txt`, `final-routes.txt`, `final-registry-manifest.json`, `final-figure-label-browser.json`, `final-downloads/newplot.png`. Cold-start and intermediate failure logs are retained separately as historical failures, not overwritten as passes. An initial controls-helper invocation used the wrong filename and exited 2 before running; the correct helper then passed with exit 0. Browser screenshots in this owned folder were refreshed by final live runs.

Exact source changes relative to #350: Dockerfile; container/entrypoint.sh; python/presentation/immutable_synthetic_chart.py; python/presentation/test_immutable_synthetic_chart.py; python/presentation/realtime_financial_visualization.py; python/presentation/test_local_synthetic_chart.py. This evidence document is the only additional repository path. Existing #353 remains open with its labels-only review; the Azure integration source is retained separately on the local repair worktree. Original checkout and owner-untracked documents remain preserved.

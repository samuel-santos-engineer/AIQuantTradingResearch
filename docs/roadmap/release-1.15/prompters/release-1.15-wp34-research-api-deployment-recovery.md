# Release 1.15 WP34 - Research API deployment diagnosis and recovery preparation

Status: offline diagnosis complete; actual Azure runtime diagnosis incomplete. Review only; no deployment or merge authorized.

Baseline: main after PR #421, `9adc53869b1d258beaff63760aed217cd7abe15d`.

## Findings and evidence boundaries

| Evidence | Supported conclusion | What remains unknown |
| --- | --- | --- |
| Owner reports Running/Healthy, Publish=Code, Runtime Stack `-`, Health Check Not Configured | Platform state is not application health evidence | Actual configured image, publishing mode and running executable |
| Owner reports anonymous `/health` returns nginx/1.30.5 HTML 404 | The observed response does not satisfy the research API contract | Whether nginx is a built-in runtime, an application image or another routing layer |
| Owner reports empty Containers (new) and unfinished Deployment Center GitHub form | Neither view establishes a successful research deployment | Legacy custom-container configuration and prior deployment history |
| Repository API registers anonymous GET `/health` returning 200, literal `Alive` | No key is needed for this health route; the reported response differs from this handler | Which handler currently receives Azure traffic |
| Reviewed Docker bootstrap selects the research API explicitly | Default Worker startup is insufficient to select the API | Whether Azure runs that bootstrap/artifact |

Most likely cause: a deployed runtime/image/entrypoint mismatch, rather than a missing route in the reviewed API. This is a hypothesis supported by the response mismatch and Code overview, not a definitive diagnosis. Do not replace authentication code, regenerate a key, or add a route to mask it. A wrong port, failed startup or hosting-mode mismatch remains possible.

No Azure query, endpoint request, registry operation, private baseline read, real-key operation or deployment was executed for WP34. Prior sanitized controller records describe procedures and earlier attempts, not current deployed state. No historical private records were opened.

## Repository contract inventory

- `src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj`: .NET 10 Worker with ASP.NET framework reference and Application/Infrastructure references.
- `Program.cs`: `--research-snapshot-api` selects `ResearchSnapshotEndpoint.RunAsync`. Default Worker startup is a different execution path. Operator initialization and storage-probe modes are not deployment health checks.
- `ResearchSnapshotEndpoint.cs`: anonymous GET `/health` returns 200 `Alive`; the remaining API requires `X-API-Key`. POST `/v1/research/snapshot` validates requests and uses the existing store. Health does not prove authentication, stored data or provider acquisition. Missing/unavailable data may still fail independently.
- `container/release_1_15/research-api.Dockerfile`: pinned ASP.NET 10 base, Linux/amd64 previously published framework-dependent `research/` context, UID/GID 1654:1654, exposed port 52731, bind `0.0.0.0`, `/bin/sh /app/research-bootstrap.sh` entrypoint. It is not a standalone source-build Dockerfile.
- `research-bootstrap.sh`: normal serve mode execs `dotnet /app/AIQuantTradingResearch.Worker.dll --research-snapshot-api`. No automatic provider import. Diagnostic/import modes must not be selected for the serving candidate.
- `eng/docker/release-1.15/Invoke-Wp07DualContainerQualification.ps1`: existing staged-context/offline image assembly example. Its fixture and Docker actions were not invoked. Preserve the separate research/UI development option.
- `eng/azure-cli/r1.15-qualification/wp21_runtime.py`: existing research-only candidate/rollback implementation and exact registry image reference. Candidate digest: `sha256:d3221dbe0a4ef6dfba37e0c61c06bb3bc9d9ee2b6f776d7738c35aece32be26f`. This identifies the previously approved candidate, not its current availability, present deployment or fresh qualification. No tag is an immutable acceptance substitute.
- No checked-in `.github/workflows` directory was found at this baseline. This does not prove that no external/manual deployment occurred. New GitHub authorization or CI is unnecessary for diagnosis.
- WP18-WP33 handoff/controller runbooks preserve bounded attempts, private fresh baselines and rollback. WP31/WP33 fake key tests and WP32 offline checks do not establish an Azure setting, channel equality or authenticated acceptance.

## Hosting-mode reconciliation

Microsoft documents that sidecar-enabled apps use `LinuxFxVersion=sitecontainers`; classic custom containers use a Docker image configuration. Consequently an empty new Containers view is insufficient to exclude a classic container. See [sidecar configuration](https://learn.microsoft.com/azure/app-service/configure-sidecar) and the [App Service team's hosting explanation](https://azure.github.io/AppService/2025/03/06/Sidecars-Deep-Dive-Part1.html).

For classic custom containers, the internal listening port must be routed correctly; see [Microsoft's custom-container configuration](https://learn.microsoft.com/azure/app-service/configure-custom-container). Repository intent is `WEBSITES_PORT=52731`, not a public port change. Do not apply classic settings blindly to a sidecar main-container configuration.

Linux F1 capability for this particular resource and a Code-to-container conversion are **not verified**. Public documentation and older repository instructions cannot establish current resource support, permissions, registry access or successful startup. No paid tier, plan replacement, new app or sidecar migration is proposed. If the existing F1 app cannot accept the reviewed classic container in place, stop for owner disposition; do not substitute infrastructure.

## Smallest next owner read-only inspection

Requested next authority: metadata-only inspection of the exact research app and its existing plan, without mutation, endpoint probes, app-setting value APIs or private attempts. Until granted, the following is a checklist, not an executed inspection.

1. Confirm exact app `aiqr115researchstagingwus27f5eabb5`, group `rg-aiq-r112-wp05-wus2-7f5eabb5`, plan `asp-aiq-r112-wp05-wus2-7f5eabb5`, Linux, F1, West US 2 and state. Return only match booleans and state, not a full resource export.
2. Inspect only hosting metadata: classify `siteConfig.linuxFxVersion` as classic `DOCKER|...`, `sitecontainers`, built-in/code, empty or unknown. Privately compare any image to the exact reference in `wp21_runtime.py`; return only match/unknown. Inspect whether `appCommandLine` is empty; do not paste its contents. Do not retrieve publishing credentials or registry passwords.
3. If sidecar-enabled, privately identify whether a main container exists and its configured image/target port. Stop before proposing a classic-mode conversion. Empty sidecar entries alone do not resolve classic mode.
4. Owner-only portal inspection of the three known non-secret settings may establish `WEBSITES_PORT=52731`, `AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0`, `AIQ_RESEARCH_TRANSPORT=azure-app-service-http`. Record booleans only. Do not reveal or screenshot any other values, especially `AIQ_RESEARCH_API_KEY`. If the portal reveals unrelated secrets, stop and leave these checks unknown. No full app-settings/list call is part of the requested metadata scope.
5. Classify: built-in/code or wrong image supports deployment mismatch; correct image plus startup override supports startup mismatch; correct image and port mismatch supports routing mismatch. Matching metadata still requires separately authorized startup/health observation. An absent application requires deployment evidence, not inference from a single 404.

No service-key value or even its presence was verified here. Preserve an existing setting. Do not add/replace it under WP34.

## Offline diagnostic

`eng/azure-cli/r1.15-qualification/wp34_runtime_diagnosis.py` consumes only a closed, owner-curated JSON object through explicit `--stdin`. It reads at most 4,097 bytes to reject payloads over 4,096, requires strict UTF-8, unique keys, exact field names and boolean/null values. Default invocation does not read input or perform operations. Output is fixed classifications, never input values. It does not access environment variables, files, Azure or keys, and never authorizes deployment. A matching report still says runtime unverified. A review-required result exits 0 for valid input; invalid/missing input exits 2. Exit 0 is not a release gate.

Required fields (all boolean or null): `targetMatches`, `planMatches`, `linuxF1WestUs2`, `siteRunning`, `classicContainerConfigured`, `sidecarConfigured`, `approvedImageConfigured`, `startupCommandEmpty`, `portMatches`, `bindMatches`, `transportMatches`. Use null for unknown; never fill from assumptions. This input is not an Azure export or a signed attestation. No input fixture is supplied as if it described the real app.

## Future deployment and rollback plan - separately authorized only

1. Complete the read-only mode/F1 checks above. Confirm the exact candidate digest remains available and reviewed; any rebuild or publication needs its own authority and immutable identity. Use existing staged Linux publish packaging, not root UI Dockerfile or research test-fixture image. No new pipeline or dual-process removal.
2. Obtain a fresh, complete, private baseline and independent recapture immediately before mutation: image/mode/startup, complete settings and connection strings, slot inventory and sticky-name metadata (preserve null versus empty), HTTPS-only/network metadata, logging, plan identity and Running state. This sensitive capture needs separate approval and protected storage; never print or commit it. Stop on missing fields, uncertainty, drift or unsupported mode. Do not reuse historical attempts. Confirm recovery access before changing anything.
3. If classic mode is supported, propose assigning only the exact research image reference from `wp21_runtime.py`, with an empty startup override so the reviewed bootstrap runs. A change from built-in Code publishing requires an explicit in-place mode/image configuration change and restart; it is not accomplished by GitHub authorization or uploading the DLL. Exact mode-conversion operations remain pending actual metadata. Sidecar conversion is not preapproved.
4. Candidate non-secret environment requirements from the reviewed code/controller are: `WEBSITES_PORT=52731`; `AIQ_RESEARCH_BOOTSTRAP_MODE=serve`; `AIQ_RESEARCH_BIND_ADDRESS=0.0.0.0`; `AIQ_RESEARCH_TRANSPORT=azure-app-service-http`; `AIQ_RESEARCH_PLATFORM_HTTPS_ONLY=1`; `AIQ_RESEARCH_PRIVATE_DIRECTORY=/tmp/aiq-research`; `AIQ_RESEARCH_STORAGE_SCOPE=azure-f1-staging-ephemeral`; `AIQ_RESEARCH_ALLOW_NON_DURABLE_STAGING=1`; `AIQ_RESEARCH_EXPECTED_MOUNT_PATH=/`; `WEBSITES_ENABLE_APP_SERVICE_STORAGE=false`. Platform site/host/instance/role markers must satisfy existing guards, including role `0`; do not fabricate platform markers to bypass them. Private directory/kernel ownership and writable mount checks still apply. This is explicitly non-durable storage, not a durability claim.
5. Preserve the SAME existing `AIQ_RESEARCH_API_KEY` byte-for-byte and all unrelated settings/slot flags. If absent or unknown and needed for startup, stop for the separate owner key workflow; never generate or infer a value. The existing controller's full settings-write/key path is **not** a safe WP34 one-command fix and was not invoked. Any later change should preserve rather than overwrite unrelated configuration.
6. Under named live authority, stop/apply reviewed delta/start only the research app. Preserve HTTPS-only, logging, F1 plan and UI app. Observe anonymous HTTPS GET `/health` with normal TLS validation for at most 180 seconds and bounded per-request timeouts; accept only 200 with literal `Alive`, no redirects/HTML. No API key, authenticated snapshot probe or provider call belongs to this health check. This step is not authorized now.
7. On failure or expired observation, restore only authorized changed properties to the fresh baseline, including original hosting mode, exact image/startup, settings and original state; restore any observed logging/HTTPS/slot drift using exact captured semantics. Verify independently against the baseline. If mode restoration is uncertain, do not begin the deployment. Do not claim rollback proven before execution. Ephemeral research data lost on restart cannot be restored by configuration rollback. Preserve operator-held key recovery separately; no historical records may be mutated.

Excluded throughout: UI `aiqr112wp05wus27f5eabb5`, provider acquisition, new services/plans, public activation and authenticated API acceptance.

## Offline evidence and changed inventory

Changed paths are exactly this document, `eng/azure-cli/r1.15-qualification/wp34_runtime_diagnosis.py`, and `tests/runtime/release_1_15/test_wp34_runtime_diagnosis.py`. No application, Docker, controller or key-helper changes.

Commands (Python offline, no bytecode artifacts):

- Repository root: `python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp34_runtime_diagnosis.py -v`: 9 passed, exit 0. Covers every missing/false required fact, unknown/contradictory hosting mode, closed schema, strict types, duplicate keys, malformed UTF-8/JSON, oversize/deep input, fixed non-disclosing output and no default input read.
- From `tests/runtime/release_1_15`: `python -B -m unittest test_wp07_staging_readiness.ResearchHealthWaitTests -v`: 12 passed, exit 0. Existing mocked timeout/TLS/status/body tests; no real HTTPS request.
- UTF-8/no BOM/LF scan and `git diff --check`: required before publication.

No .NET build was needed for this Python/document-only change. No PowerShell runtime test was attempted; no execution-policy bypass. Actual F1 compatibility, image availability, startup, key presence/equality, authenticated API acceptance, rollback and live/browser readiness remain unverified.

`liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.

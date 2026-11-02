# Release 1.15 WP35 - Research API image packaging and deployment preparation

Status: reviewable implementation; build/publication/deployment blocked before mutation. UTF-8/LF/no BOM is mandatory for all changed text.

Baseline: merged WP34 PR #422, `4bee26f5c41c11679dedc3bf2d0ec11f8cfbe7a4`. Original dirty checkout and owner files are preserved in place. No generator, real key, private attempt or historical baseline was read.

## Actual read-only Azure evidence

Metadata-only Azure CLI queries were executed against the exact research app and plan using existing authenticated access. Captured during WP35 on 2026-11-02; this is a point-in-time observation, not a future deployment baseline. Each subprocess had an 18-second timeout, fixed target, JSON projection and suppressed raw failure output. No app-settings/list, connection-string API, publishing credentials, authenticated research request or public endpoint probe occurred.

| Check | Actual result |
| --- | --- |
| App | `aiqr115researchstagingwus27f5eabb5`, Running, `kind=app,linux`, West US 2 |
| Plan assignment | Exact existing `asp-aiq-r112-wp05-wus2-7f5eabb5` in `rg-aiq-r112-wp05-wus2-7f5eabb5` |
| Hosting | `linuxFxVersion` empty; classic `DOCKER|` false; `sitecontainers` false |
| Startup override | Empty |
| HTTPS-only | **False**; enabling it would be a separately approved change, not an already satisfied guard |
| Plan | F1 / Free, West US 2; direct ARM plan metadata confirms `properties.reserved=true` (Linux) |
| API key | Not queried; presence, value and equality unverified |

The first CLI plan projection returned a null Linux field; a bounded direct ARM GET of the same plan resolved it to true. Null was not interpreted as false or proof of compatibility.

This establishes that the reviewed custom image is not currently assigned through classic `linuxFxVersion`, and the app is not in sidecar mode. It supports the runtime mismatch hypothesis behind the owner-reported nginx 404. It does not establish that no files or application exist, or prove which process serves the response. No new `/health` request was made.

Inspection recipe, for later authorized repetition (metadata only, never full exports): `az webapp show` projected app name/state/kind/location/plan/httpsOnly; `az webapp config show` projected only classic/sidecar/empty-mode and startup-empty booleans; `az appservice plan show` projected name/location/sku/tier; direct plan ARM GET API `2023-12-01` projected `properties.reserved`, sku and location. No raw startup command or image configuration value was printed.

## F1 feasibility decision

Existing Linux F1 and empty code-mode configuration are confirmed. **In-place classic-container conversion support for this exact resource, and a viable reverse transition, remain unverified.** No configuration update was used as a feasibility probe.

Microsoft documents [classic versus sidecar hosting settings](https://learn.microsoft.com/azure/app-service/configure-sidecar) and [custom container configuration](https://learn.microsoft.com/azure/app-service/configure-custom-container). Microsoft's [training demonstration](https://microsoftlearning.github.io/AZ-104-MicrosoftAzureAdministrator/Instructions/Demos/09%20-%20Administer%20PaaS%20Compute%20Options.html) introduces a container example, but its actual creation steps select **Code** on Free F1. It is not evidence of F1 custom-container compatibility. The [Azure CLI reference](https://learn.microsoft.com/en-us/cli/azure/webapp/config/container) documents setting an existing app's container configuration, but does not establish this F1 resource's conversion or exact restoration capability. No claim that all F1 container hosting is unsupported is warranted.

The precise missing evidence is an applicable supported in-place conversion/restoration method for this resource's empty `linuxFxVersion` and `kind=app,linux`, retaining the exact F1 plan. Owner disposition: confirm that method with platform support/documentation, or separately authorize a bounded reversible compatibility attempt only after complete protected baseline/recovery review. No sidecar migration, paid upgrade, app recreation or UI change is an implicit fallback. Do not build/push unnecessarily before this decision.

## Serving artifact and repeatable local workflow

Unchanged source contract:

- Worker targets .NET 10. Default Worker mode does not select the research API.
- Reviewed `container/release_1_15/research-api.Dockerfile` consumes prepublished `research/`, pinned ASP.NET 10 base, Linux/amd64, UID/GID 1654:1654, port 52731 and bind `0.0.0.0`.
- `/bin/sh /app/research-bootstrap.sh` execs `dotnet /app/AIQuantTradingResearch.Worker.dll --research-snapshot-api` in serve mode. The script need only be readable by that entrypoint; no executable-bit workaround is necessary.
- Anonymous GET `/health` must return status 200 and exactly `Alive`. It does not prove authentication, data import or snapshot availability. No route/authentication/source changes were needed.

New single Windows PowerShell 5.1 entrypoint:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp35ResearchImage.ps1
```

Default `Plan` reads only the current Git commit and prints a nonsecret recipe. No Docker, restore, Azure or registry operation. The path must refer to the reviewed WP35 checkout; the original older checkout does not acquire these files automatically.

After reviewed F1 feasibility evidence, local build is explicitly selected:

```powershell
powershell.exe -NoProfile -NonInteractive -File .\eng\docker\release-1.15\Invoke-Wp35ResearchImage.ps1 -Action Build -F1FeasibilityConfirmed
```

`-F1FeasibilityConfirmed` is an operator assertion requiring real evidence; it is not proof and does not enable publication/deployment. Do not set it merely to bypass the current blocker. No execution-policy changes or bypass are part of this workflow.

The Python standard-library implementation exports the current committed tree with `git archive` into a new owned temporary directory, excluding all untracked owner files and the working directory's local edits. It rejects traversal/symlink archive entries. Review/commit intended source changes first. It stages only the published research files and exact bootstrap/Dockerfile into the image context.

It checks the cached pinned Linux/amd64 ASP.NET base, restores from an explicitly empty local package feed with cleared sources and NuGet auditing disabled (existing global package/runtime caches only), publishes Release `linux-x64` framework-dependent output with `--no-restore`, checks .NET 10 runtime metadata and Linux SQLite native library, and builds with `--platform linux/amd64 --pull=false --network=none`. Missing caches stop; no download fallback. Intermediate source/output is owned temporary storage, not owner state. No real key is an input, build argument or layer.

It writes a nonsecret context SHA-256 manifest and result, then validates image OS/architecture, UID, entrypoint, exposed port and default bind. The local commit-qualified tag is `aiq-wp35-research:<full-source-commit>`. Local image ID is a configuration digest, **not** a registry manifest digest. A built result deliberately remains `LOCAL_IMAGE_BUILT_RUNTIME_UNQUALIFIED`, with health unverified and no registry digest. Outputs/images are retained for review; no cleanup targets unrelated resources.

This is reproducible packaging intent (pinned base, source commit, closed context, offline inputs and manifest), not a proven bit-for-bit image reproducibility claim. No serving image was built in WP35 because feasibility is unresolved. Initial Docker inspection returned pipe access denied; the subsequent owner-authorized access retry succeeded (details below). SDK `10.0.103` is installed. No alternate identity, installation or policy bypass was attempted.

## Future local runtime qualification - pending, not executed

Before publication, use the built serving image with only a clearly fake 64-lowercase-hex test key and bounded owned resources. Existing WP07 local HTTPS fixtures and transport tests remain available; they are not Azure attestation. Alternatively a separately reviewed local emulation of the existing Azure HTTP/storage guards must be explicitly identified as emulation, never actual platform evidence.

Use a disposable writable private directory with correct Linux ownership, no real database/import, loopback-only published port and no provider acquisition. Verify PID command includes `--research-snapshot-api`, UID 1654, bind/port 52731 and anonymous HTTP health 200 `Alive`; enforce a bounded readiness deadline and remove only the created container/storage/network. An Azure HTTP guard cannot simply be omitted to force startup: normal local transport requires HTTPS certificate settings; the existing Azure transport also requires exact platform markers and private storage/kernel checks. This step is pending rather than replaced with fake test success. Preserve the existing separate UI/research development containers.

## Publication proposal - separate approval required

Registry identity remains the exact existing repository reference defined by `IMAGE` in `eng/azure-cli/r1.15-qualification/wp21_runtime.py`. Do not reuse its historical digest as the new build's identity. Do not request, print or synthesize registry credentials. Existing permitted registry login/package-write access and public readability or approved existing pull authentication must be established before push; no token command-line argument.

After local runtime qualification, the proposed commands are `docker tag <qualified-local-image-id> <existing-registry-repository>:wp35-<full-source-commit>`, then `docker push <that-exact-tag>`. Resolve the repository string from the reviewed `IMAGE` constant without its old digest. Present the resolved target and actual local image ID for explicit owner approval first. Read back the registry manifest, verify Linux/amd64 and exact published digest, then deploy only `<existing-registry-repository>@sha256:<verified-manifest>`. Do not treat a tag, local configuration ID or old digest as the published identity. No command was run, and no candidate digest is fabricated.

No push switch or Azure apply action is exposed by the new script. Until these preconditions and authority are established, publication remains a concrete conditional procedure rather than an untested mutating implementation.

## Future research-only deployment and rollback

Do not invoke the existing WP21 controller wholesale: its live path changes settings/key configuration and restarts the app. WP35 did not authorize that controller, key reads, or sensitive baseline access. No new deployment framework is added while the conversion/restoration contract is unresolved.

1. Before an authorized apply, approve the exact candidate digest and precise operation list for only the research app. Review the supported Code-to-classic conversion and reverse transition, including whether `kind` changes are required. Capture two matching fresh protected baselines and verify no intervening drift: hosting mode/image/startup/kind, complete app settings and connection strings, null-versus-empty slot metadata, slots, logging, HTTPS-only, F1 plan and original Running state. These secret-bearing captures require separate authority and protected storage; never display/commit/reuse historical baselines. Fail before mutation on any missing field or uncertain recovery.
2. Preserve the existing `AIQ_RESEARCH_API_KEY` byte-for-byte and all unrelated settings. No Codex key read/transfer. Presence/equality has not been proven. If a key is absent or cannot be preserved through an approved operation, stop for the owner's separate workflow; do not generate/rotate or rewrite it. A full settings PUT must not be synthesized from an incomplete nonsecret listing.
3. Proposed candidate delta is classic image assignment to the newly verified immutable digest, empty startup override and only necessary nonsecret settings: `WEBSITES_PORT=52731`, bootstrap `serve`, bind `0.0.0.0`, transport `azure-app-service-http`, platform HTTPS-only flag `1`, private directory `/tmp/aiq-research`, storage scope `azure-f1-staging-ephemeral`, non-durable opt-in `1`, expected mount `/`, App Service storage `false`. Exact names/guards are listed in the merged WP34 runbook and actual source. Platform site/host/instance/role markers must be genuine; do not fabricate Azure markers in deployment.
4. **HTTPS-only false was observed**. Enabling platform HTTPS-only must be part of the separately approved delta, and rollback must restore the original false value if required. Do not set the application HTTPS flag while claiming the platform is already HTTPS-only. Preserve logging/slot semantics/plan/UI. Explicitly authorize stop/start/restart before performing them.
5. Under that future authority, observe only anonymous HTTPS GET `/health` with normal TLS validation, bounded per-request timeout and at most 180 seconds total. Accept exclusively 200 and literal `Alive`; HTML/redirects/authentication challenges are failures. No API key or authenticated snapshot endpoint. Key acceptance and authentic data import remain separate.
6. On failure/timeout, the separately authorized recovery restores exact fresh original mode (including empty `linuxFxVersion`), kind if changed, image/startup, changed settings and original state, plus any permitted restoration of logging/HTTPS/slot drift. Verify independently against the complete baseline. Do not claim rollback verified until executed. A successful configuration rollback cannot recover ephemeral data lost on restart. Confirm the reverse transition before any deployment.

An executable Azure mutation/recovery path is intentionally **not implemented or validated**: current evidence cannot establish its exact safe operations. The delivery gap is explicit, not a claim that a dry-run plan deploys. The next approval must resolve conversion/restoration and sensitive baseline authority, then review exact operations. No plan upgrade, new resource, provider import, public activation, live attempt, UI update or merge.

## Actual qualification

- `python -B -m unittest discover -s tests/runtime/release_1_15 -p test_wp35_research_image.py -v`: 9 passed, exit 0. Fake metadata, mocked build/context/image verification, strict source identity, no default Docker execution, feasibility gate, offline command flags, suppressed tool failures and no push/Azure path. Mocked build is not image/runtime acceptance.
- `python -B eng/azure-cli/r1.15-qualification/wp35_research_image.py --repository .`: `PLAN_ONLY`, exit 0. No image built.
- Windows PowerShell `5.1.26100.9711` AST parse: zero errors. Exact older requested build `5.1.26100.9444` unavailable. `.ps1 -File` Plan runtime execution: blocked by execution policy (exit 1); no bypass. Python tests do not establish actual PowerShell runtime qualification.
- Initial Docker info: access denied to local engine pipe. After explicit owner session approval, retry succeeded: context `desktop-linux`, engine `29.7.2`, Linux `x86_64`. Exact pinned ASP.NET base inspect succeeded: Linux/amd64, image ID `sha256:e4b445f93a5deeb6bd7de300febceb8dd7e481ac65ccf1b474eddc3642a3010f`. Docker access and this cached input are no longer blockers. No build, publish, pull or container start occurred; the feasibility-before-build condition remains unresolved.
- UTF-8/no BOM/LF and `git diff --check`: checked before PR publication.

Changed files: `eng/docker/release-1.15/Invoke-Wp35ResearchImage.ps1`, `eng/azure-cli/r1.15-qualification/wp35_research_image.py`, `tests/runtime/release_1_15/test_wp35_research_image.py`, this runbook. No application, Dockerfile, controller, authentication or UI changes.

Image built: NO. Registry publication: NO. Azure deployment/restart: NO. `/health`: UNVERIFIED. F1 exact-resource conversion and rollback: UNVERIFIED.

`liveAttemptReady=false`; `AUTHENTIC_DATA_NOT_IMPORTED=True`; `PUBLIC_RUNTIME_VERIFIED=False`.

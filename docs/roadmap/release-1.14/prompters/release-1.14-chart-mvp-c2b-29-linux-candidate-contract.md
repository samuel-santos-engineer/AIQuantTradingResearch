# C2B-29 ? Local Linux candidate contract

Owner authority: C2B-29, 2026-10-16. Local implementation/build/qualification only. PUBLIC_RUNTIME_VERIFIED=False. Advanced WP07 ON HOLD. Public activation and Azure deployment NO-GO.

## Authority and dependency provenance

Main 612483e570d4b8e1f99d512681ca26b1bc1320cd. PRs #346 (0e568d208f2769037bf4a7265ea4ba6f4609aacc), #347 (8240d0878dea335c08ad3216600fc7e545446140), #349 (c40ba4e1ba751b5097061d95d76ce81bbd019c0e) verified OPEN before work; left unchanged. Isolated branch incorporates PR #347 commits 35e09dc4b98669111bb88cd83f6d1cf662d29f15 and 8240d0878dea335c08ad3216600fc7e545446140 with `cherry-pick -x`. This does not merge or accept that PR.

## Gate disposition before build

| Gate | Local disposition; no gate passage claimed |
|---|---|
| G-B0 / Section 12 export rules | C2B-29 explicitly authorizes bounded local synthetic generation, image build and testing; not production export, publication or promotion. Docker-first policy merged in #348. |
| G-B1 admission | No request-time Worker is included/launched; advanced admission implementation remains held, not waived for a future release. |
| G-B2 confidentiality | Fresh tracked-source context plus enumerated edits; root-owned finite assets; no credentials, private replay or provider acquisition. Target-image negative evidence required. |
| G-B3 resources | Local 1 CPU/512 MiB/64 PID screening; no Azure hard-quota or capacity claim. |
| G-B4 recovery | Local startup/shutdown/restart tested only; not advanced WP07 crash-recovery acceptance. |
| G-B5 semantics | Unchanged generator/calculator, seed 114, fixed coverage, 250 hidden warmup; all twelve immutable selections and prior hashes must agree. |
| G-B6 Azure | Held; no Azure probes or writes under this task. |
| G-B7 abuse | Bounded local reader/session checks only; durable global public abuse controls remain unqualified. |

No gate blocks the expressly authorized isolated local candidate; no local result authorizes serving a public chart or passing public release gates.

## Image and eligibility

Dockerfile's final `synthetic-candidate` target builds from pinned official Linux/amd64 SDK/Python images. Existing private packaging remains an explicit `private-runtime` target, outside this qualification. Build-stage C# generation runs with network disabled and a 60-second producer deadline; runtime has no .NET/Worker. Generated data stays outside Git.

Default startup is unavailable public presentation with held JSON API routes. Only explicit `AIQ_RUNTIME_MODE=local-candidate` plus `AIQ_LOCAL_IMAGE_QUALIFICATION=1` opts into local chart testing. Eligibility is checked against inherited environment before sanitization, rejects Azure/private-provider markers, production/staging environment markers and conflicting local flags. It never sets Development or public readiness. Local ingress must be explicitly published to host loopback only; do not deploy this qualification mode.

Runtime UID/GID 1000:1000, root-owned application/assets, immutable files 0444, directories 0555; reader validates root ownership, no writable asset files, no public-writable ancestors or links. Fixed candidate path and separately compiled manifest pin unchanged. Private test seams may supply temporary roots/pins to reach semantic negatives; public acquisition cannot. Root filesystem read-only; only bounded `/tmp` tmpfs writable. Supported `logger.hideWelcomeMessage=true` prevents startup external-IP discovery; telemetry and file watching are disabled. No `/runtime` cleanup at startup.

## Promotion boundary

Local qualification is not registry promotion. Record platform manifest digest separately from config/layer/index identities. A future owner authority must specify retaining and promoting the exact qualified bytes without rebuild, registry readback, source/dependency review and independent target-site qualification. No registry push, merge, deployment or activation here. Any missing acceptance evidence makes promotion recommendation NO-GO.

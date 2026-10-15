# AIQuantTradingResearch — Chart MVP C2A Codex Authority

## Authority and purpose

Owner authorizes **C2A: read-only public application startup diagnosis** only. Determine the most likely reasons the existing Azure web application fails to present its non-admin Streamlit page, distinguish source-proven conditions from runtime hypotheses, and propose the smallest separately authorized verification/remediation steps. **No changes or execution are authorized.** C1 is accepted as an assessment; advanced WP07 administrative lifecycle is ON HOLD.

Target public application (reference only; do not request it): `https://aiqr112wp035ec325382770.azurewebsites.net/`.

## Mandatory baseline and preservation

- Protected local branch: `draft/release-1.14-wp07-partial-public-disabled`; expected HEAD `67893f51931ee183f71050a83b81127e6e648b87`.
- Checkout is intentionally dirty and contains untracked alternatives. Do not normalize, clean, stash, reset, checkout, fetch, pull, or otherwise mutate it.
- Record **initial and final** branch, full HEAD, complete porcelain status (including untracked entries), and locally recorded refs. Compare exactly. Report any divergence and STOP.
- Optionally compute read-only source hashes, including aggregate Git-listed-file hashes; do not assume prior aggregates remain identical as the file inventory evolves. Clearly distinguish ignored files, untracked files, local cached refs, and fresh remote evidence.
- `PUBLIC_RUNTIME_VERIFIED=False` is an intentional security boundary, not a startup fix. Public administrative diagnostics remain disabled. No production qualification or activation is authorized.

## Scope and evidence collection (read-only inspection)

1. **Public entrypoint chain:** Trace Dockerfile `ENTRYPOINT`/`CMD`, `container/entrypoint.sh`, public-versus-admin branch conditions, privilege drop, environment handling, process/port binding, and Streamlit module invocation. Cite exact paths and line numbers. Confirm whether source defaults choose public mode; do not infer actual deployed configuration.
2. **Page render path:** Trace imports and startup-side effects in `python/presentation/realtime_financial_visualization.py` and its dependencies. Separate failure before server bind, after bind but before page render, and successful page with intentionally unavailable candle data. Explain why the `historical_market_bridge.py` gate may block candles without necessarily blocking the page.
3. **Container filesystem and build:** Inspect `/runtime` emptiness and protected-cache guards, copy order, ownership, working directory, dependency installs, Streamlit configuration, exposed ports, and build-time AdminDiagnostics restoration/publication. Identify which conditions could fail only in Azure versus in any container.
4. **Azure configuration requirements:** From checked-in configuration/docs only, identify expected App Service container port (`8501`), health/startup settings, environment flags, filesystem mounts, container command/image identity, resource quotas, and log locations. Mark actual Azure values and live state **UNVERIFIED**. Do not access Azure, app URL, APIs, credentials, or network.
5. **Failure-mode matrix:** For each plausible failure, include source evidence, trigger, expected observable symptom/log signature, confidence (`CONFIRMED SOURCE CONDITION`, `HYPOTHESIS`, or `UNVERIFIED DEPLOYMENT STATE`), and least-invasive next check. Do not assert a root cause without runtime evidence.
6. **Recovery decision tree:** Prioritize safe checks that separate (a) image/build failure, (b) container startup failure, (c) Streamlit process/bind failure, (d) Azure ingress/port mismatch, (e) page import/render exception, and (f) chart data intentionally blocked. Identify a minimal future read-only Azure evidence request and a distinct, separately authorized local smoke-test scope.
7. **Reuse and boundaries:** Retain Streamlit/Plotly; do not propose enabling the synthetic bridge, private Twelve Data display, Vike spot mislabeling, public administrative routes, privileged startup, or unrelated WP07 changes. Historical data licensing belongs to separate C2B.

## Absolute prohibitions

- **Empty mutation allowlist:** no edits, generated repository files, commits, branches, pushes, PRs, merges, or Git state changes.
- No tests, imports, Python execution, scripts, builds, Docker commands, installs, native probes, network calls, browser requests, Azure CLI/portal/API access, or runtime execution.
- No reading or printing secret values; inspect source references to secret names only where necessary. No environment dumps or protected credential/cache content.
- No changes to `PUBLIC_RUNTIME_VERIFIED`, admin feature flags, security gates, lifecycle protocol, Docker/Azure resources, or production settings.
- Do not treat source code, cached refs, or hypothetical logs as actual live deployment evidence.

## Deliverable — report in Codex response only

Provide: (A) baseline/preservation table; (B) line-cited public startup sequence; (C) ranked failure-mode matrix; (D) source-confirmed versus deployment-unverified facts; (E) smallest next evidence request for Azure and separate local smoke-check proposal, each with explicit owner approval gates; (F) recommendation whether C2A suffices for remediation planning or needs further read-only evidence; (G) final preservation proof and explicit no-operations statement.

Use one terminal disposition:

- `CHART-MVP-C2A READ-ONLY STARTUP DIAGNOSIS COMPLETE — OWNER DECISION REQUIRED`
- `CHART-MVP-C2A BLOCKED — OWNER REVIEW REQUIRED`

Stop on missing evidence, security-boundary ambiguity, or unexpected checkout drift. Completion **does not** authorize remediation, deployment, provider integration, admin activation, or public runtime qualification.

# Codex prompt — Release 1.15 WP65: Accepted WP61 UI local Docker metadata verification

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Base: current `main`, at least squash merge of PR #460, `ed39e0a0ca18e6316a5738651fce88aa28f2990f`.

## Goal
Resolve the **actual local Docker identity kind and metadata** of the WP61 accepted chart UI image, without rebuilding, pulling, publishing, deploying, or touching Azure. Feed trustworthy, safely redacted evidence into the merged WP64 OCI provenance verifier. This work should unblock a decision about whether a registry lookup or publication is necessary; do not assume either outcome.

Exact accepted WP61 local image ID:
`sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`

Recorded accepted UI: Linux/amd64, UID/GID `12002:12002`, port `8501`, Streamlit research chart, source commit `cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b`. Configured Azure UI digest `sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07` has **UNVERIFIED** relationship to WP61. Preferred accepted API child `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`; WP62 diagnostic child `sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c` is not a substitute.

## Authorization and scope
This prompt **authorizes one read-only local `docker image inspect` of exactly the accepted WP61 image ID**, on the operator's existing Docker context, if that context and command can be safely qualified. No other Docker daemon/image calls are authorized. In particular, **do not run `docker image save` or export any image** under this prompt. If inspection cannot be safely run or the image is absent, stop and report the evidence gap; do not pull, rebuild, enumerate other images, inspect containers, access volumes, or retry against a different Docker context. No Azure operations, registry HTTP, credentials, logs, public probes, image changes, or deployments. If the local Docker environment is inaccessible, do offline preparation only and clearly report that no real inspect occurred.

Use an exact-ID, no-shell-injection invocation; establish and report the current Docker context identity without contacting another daemon or changing context. Do not automatically start Docker or install packages. One attempt, with a 10-second wall-clock deadline and bounded stdout/stderr; stop if strict bounds or safe cleanup cannot be enforced. Inspect output must be projected to a minimal allowlist **before** display or persistence: `Id`, `Os`, `Architecture`, `Config.User`, `Config.ExposedPorts` (port names only), `Config.Entrypoint`/`Config.Cmd` (report contract booleans rather than raw potentially sensitive arguments), and `RepoDigests` (safe full immutable refs only). Avoid `Config.Env`, labels, history, filesystem, layer contents, paths and any secrets. If the Docker CLI necessarily receives richer JSON internally, never persist or echo it; state this limitation explicitly. Do not treat `RepoDigests` as fresh registry availability proof.

**No local image export is authorized.** If inspect alone cannot resolve the OCI index/child/config chain, write a concrete, separate proposal for a bounded image metadata export requiring explicit future owner approval. The proposal must note that `docker image save` copies layers, not just metadata; require verified-owned Linux `0700` directory, `0600` output, a real pre-write 2 GiB cap (not just post-write truncation), 120-second deadline, cleanup and no extraction/opening of layers. If these cannot be guaranteed, propose a safer metadata-only alternative rather than executing export.

## Implementation
1. Review merged WP64 `eng/docker/release-1.15/wp64_oci_provenance.py`, its tests, and WP61/WP64 evidence. Keep existing verification semantics and safety boundaries.
2. Add the smallest useful local inspect projection/validation helper and synthetic tests. It must be fail-closed on missing/malformed/wrong-ID data and distinguish `local Docker image ID`, `config digest`, `manifest digest`, `OCI index digest`, and `RepoDigest` without assuming they are interchangeable. Ensure output is redacted and avoids dumping Docker's full inspect JSON.
3. If the exact authorized inspect is feasible, run it once, capture **only safe projected evidence** and compare to recorded WP61 UI contract. If not, leave actual identity `UNVERIFIED` and document why.
4. Determine the precise next evidence requirement. Do not claim registry publication is required just because no RepoDigest was observed. Do not claim Azure's configured image is a match or mismatch without a verified same-kind chain.
5. Create a concise WP65 evidence document under `docs/roadmap/release-1.15/prompters/` and update only directly relevant files. Include exact commands executed, exit codes, test counts, safe identity observations, unverified fields and next bounded action.
6. Run focused synthetic tests and relevant unchanged WP64/parser/collector regression tests; check Python syntax, UTF-8/LF, Markdown links, and `git diff --check`.
7. Open a reviewable PR against current `main`; **leave it open and unmerged**. Do not modify PR #457 or unrelated work.

## Stop conditions and reporting
Stop on wrong Docker context, missing image, identity mismatch, unexpected output, timeout, uncontrolled subprocess, unsafe redaction, or any request for unauthorized export/registry/Azure access. Never auto-remediate. Preserve `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false`.

Final response: PR URL, base/head SHA, exact changed files, tests, whether the one authorized real local inspect was performed, safe metadata result, provenance conclusion (`MATCH` / `MISMATCH` / `UNVERIFIED` with precise meaning), and smallest remaining action toward deploying the accepted API first and WP61 UI second. Do not present an unverified registry reference as deployable.

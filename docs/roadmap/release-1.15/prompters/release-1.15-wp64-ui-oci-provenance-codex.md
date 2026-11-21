# Codex prompt — Release 1.15 WP64: accepted WP61 chart UI OCI provenance

## Objective

Resolve the exact identity and deployment-ready OCI reference of the **locally accepted WP61 Streamlit chart UI** (BTC/USD and ETH/USD), without conflating its local Docker image ID, OCI index digest, platform manifest digest or image configuration digest. Prefer an already published immutable GHCR artifact if verifiably equivalent. Prepare a reviewable PR with evidence and offline tests. **No live registry, Docker, Azure or credential operations in this work package.**

## Repository baseline and scope

Repository: `samuel-santos-engineer/AIQuantTradingResearch`. Start from current `origin/main`, including squash-merged PR #459 at `2e918ed1443194ffc97a29ed7eaf767ac6b4e26f` (and PR #458 at `5217e1959a3a5ed5283c9874f2fd79b3d1a51ec3`). Confirm clean tree and current head before creating a narrowly scoped branch. PR #457 is separate and should not be modified, merged or used as an unreviewed dependency. Existing accepted API is preferred; do not substitute the WP62 diagnostic API.

Known recorded evidence (verify against tracked receipts, do not treat as fresh registry attestations):

- WP61 accepted UI local image ID: `sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`; source commit `cca6a81429fcbc6906ee5e77fe5dd0a5a19d8b4b`; Linux/amd64, UID 12002, port 8501. Its exact OCI index, runtime manifest child and published RepoDigest are **UNKNOWN**.
- Azure configured UI digest: `sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07`. Relationship to WP61 accepted UI remains **UNVERIFIED**; this matches earlier synthetic publication evidence, not necessarily the WP61 artifact.
- GHCR repository: `ghcr.io/samuel-santos-engineer/aiquanttradingresearch`.
- Accepted research API linux/amd64 child: `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`. Diagnostic API child `sha256:d33626a975e9f134bc0089a0d7289ed2c3afaf681747ae8485064bfd3665ba8c` is not the behavioral baseline.
- Existing offline helpers: `eng/docker/release-1.15/setting_name_provenance.py` and `setting_name_collector.py`. Preserve their no-live execution restrictions and tests.

## Work to perform (offline only)

1. Review tracked WP61 acceptance, image build/publish receipts, Dockerfile, image configuration expectations, and prior WP58/Phase G publication records. Build an evidence table distinguishing local image ID, image config digest, OCI index digest, linux/amd64 child manifest digest, repository tag and Azure configured reference. Clearly mark source of each assertion and UNKNOWNs. Do not infer index/child equality from similarly formatted hashes.
2. Implement a **pure offline OCI provenance verifier** in `eng/docker/release-1.15/`, accepting explicit local fixture files or byte streams representing OCI index/manifest/config/recorded metadata. It must verify SHA-256 of raw bytes before trusting descriptors, enforce recognized media types and schema, exactly one selected linux/amd64 child (if an index), and validate config architecture/os, UID 12002, exposed/listening port 8501 and relevant startup/entrypoint evidence when actually present. Do not echo config ENV, history, labels containing secrets, local paths or raw registry responses. Distinguish `MATCH`, `MISMATCH`, `UNVERIFIED` with conservative semantics; absence of a published mapping is not proof of nonpublication.
3. Add synthetic offline tests covering direct manifest vs index, index-child-config digest chain, platform ambiguity, digest tampering, wrong config platform/user/port, incomplete receipts, misleading local IDs, malformed/oversize JSON, redaction and no-network/no-Docker default. Use standard-library-only code if practical. Preserve all prior parser/collector tests.
4. Review the **minimal future registry metadata lookup** described in the PR #459 evidence. Its candidate `GET /manifests/sha256:3e323...` may be a local image ID rather than a published manifest digest. Document precisely what 404/401/redirect mean and how to avoid treating them as definitive evidence. Prefer first resolving the local OCI metadata from existing tracked receipts; if no trustworthy mapping exists, propose a separate, explicitly approved, narrowly bounded local metadata inspection or registry lookup with exact operations, expected responses, caps, timeout, authentication limitations and stop conditions. **Do not perform either lookup.**
5. Produce a decision-ready artifact: (a) exact deployable immutable UI registry reference with verifiable chain, **only if established**, or (b) a concise unresolved-evidence matrix and smallest next authorized observation. State whether publication is proven necessary, not proven necessary or still unknown. Do not recommend a rebuild solely because provenance is missing.
6. Preserve deployment sequence: accepted API first, then accepted UI after API health/authenticated BTC and ETH and data readiness are proven. Document UI `WEBSITES_PORT=8501`, research HTTPS endpoint/shared API key contract, and no assumptions about local Docker mounts, private TLS or Azure data availability. No changes to runtime, images, secrets, ports, Azure configuration, storage or deployment code.

## Strict boundaries

- **ZERO Azure operations; ZERO registry HTTP requests; ZERO Docker daemon/image operations; ZERO builds, pulls, pushes, tags or deployments.** No credential/token retrieval, SCM/log access, public probes, volume inspection or private data access.
- No automatic approvals, bypassing gates, flipping `LIVE_ADAPTER_QUALIFIED`, or adding live execution switches.
- Do not call a UI digest equivalent to WP61 without a complete verified digest-kind mapping. Preserve `PUBLIC_RUNTIME_VERIFIED=False` and `liveAttemptReady=false`.
- Avoid modifying PR #457 or unrelated governance, licensing, Windows ACL, Azure quota, or staging code. Do not introduce new licensing gates.

## Deliverables and acceptance

- One narrowly scoped PR against current `main`, **OPEN/UNMERGED**, with the evidence document, offline verifier and focused synthetic tests. Use existing evidence document if appropriate and avoid duplicating conflicting sources.
- Execute verifier tests plus the existing setting-name parser/collector tests. Report exact commands and pass/fail/skip counts; run syntax, UTF-8/LF/no-BOM and `git diff --check` checks.
- PR description must state the exact file inventory, proven/unknown OCI links, whether an immutable UI deployable reference was actually established, next smallest separately approvable lookup if not, and explicitly **ZERO Azure/registry/Docker operations**.
- Final response to owner: PR URL, base/head SHA, concise result, tests, remaining unknowns, and stop-state flags. Do not merge.

The purpose is to remove the UI artifact identity uncertainty before attempting to deploy the already accepted containers to Azure Linux App Service—not to add another generic governance phase.

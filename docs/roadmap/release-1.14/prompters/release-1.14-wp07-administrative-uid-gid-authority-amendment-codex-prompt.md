# Release 1.14 WP07 — Administrative UID/GID Conditional Authority Amendment

## Purpose and authority

This is a **narrow amendment** to the previously issued **Release 1.14 WP07 — Administrative REST API Narrow Offline Implementation Authority**. It resolves only the STOP caused by the absence of a repository-proven collision-free administrative numeric identity. It does **not** expand the previously approved file envelope, execution permissions, deployment authority, or security claims.

**Owner decision:** Approve **UID 1100 / GID 1100** as the *conditional candidate* for the dedicated administrative router identity, **not** as a proven free identity. The presentation lifecycle agent must use the actual Streamlit/presentation identity, without silently changing that identity. Preserve the existing `aiq` identity and frozen public-runtime assignments/reservations (including 1001–1003). Do not assume that the entrypoint's `1000:1000` matches the base-image account inventory; reconcile this safely in the existing authorized scope.

## Required preconditions and fail-closed checks

1. Inspect the Dockerfile, entrypoint, frozen bootstrap, and relevant identity references before editing. Preserve existing tracked/untracked work; record baseline branch, HEAD, and `git status --short`.
2. Add **build-time collision rejection** within the previously authorized Dockerfile/build packaging scope. Before creating the administrative identity, inspect the actual build-stage image account/group inventory (`/etc/passwd`, `/etc/group` and appropriate account lookup tools). Reject if UID **1100** or GID **1100** is already assigned to another account/group, or if the intended administrative account/group names are already inconsistently assigned. Do not reuse, overwrite, rename, or renumber any existing account/group. Do not treat an unchecked mutable base-image tag as collision proof.
3. Require startup validation, before enabling the administrative listener, of effective administrative and presentation UID/GID separation, expected account mappings, supplementary-group exclusion, and required trusted-code ownership/modes. Any mismatch must fail closed: no administrative listener, no collection, no fallback to same-UID key sharing or privileged execution.
4. The presentation lifecycle agent remains under the Streamlit/presentation identity. It must not inherit the administrative key, credential pipe, or unrelated descriptors. Maintain the previously approved immutable-code, environment sanitization, and process-lifecycle requirements.
5. A build-time account collision check is **not** proof of live container isolation. Record actual image/runtime identity separation, capabilities, `/proc`/ptrace exposure, descriptors, and shutdown behavior as **NOT QUALIFIED** pending separately authorized local-container tests.

## Work permitted by this amendment

- Resume the **previously authorized narrow offline implementation and synthetic tests**, but only inside its original literal-path allowlist and restrictions.
- Implement the conditional UID/GID assignment and collision/startup checks only through already authorized paths. Any need for a new file, changed package scope, identity renumbering, or unexpected dependency requires **STOP and renewed owner approval**.
- Continue the previously bounded YARP 2.3.0 dependency restore/artifact review under the original authority, with its original security/licensing stop conditions.
- Use only synthetic test credentials and offline fixtures; do not retrieve or use actual Azure App Service setting values.

## Explicitly prohibited

No container execution or live image qualification under this amendment; no Azure, deployment, App Service configuration or secrets operations; no Worker execution or public activation; no SQLite, private database, sidecar, or provider-secret access; no GitHub mutations; no privilege expansion; no changes to the frozen public runtime safety gate. Do not interpret a successful offline test or build-stage collision check as production security qualification.

## Required Codex output

Report (a) baseline and exact file diff, (b) how the actual build-stage account/group collision checks work and how failures stop the build, (c) the startup identity and trusted-code validations, (d) results of authorized dependency checks and offline synthetic tests, (e) any unresolved conflicts or STOP triggers, and (f) a **GO/NO-GO recommendation for requesting separate local-container qualification authority**. Distinguish repository evidence, synthetic test results, and unverified live-runtime assumptions.

**If 1100:1100 collides, if the base-image inventory cannot be reliably checked in the authorized build workflow, or if the implementation cannot stay inside the original scope: STOP. Do not choose another UID/GID autonomously.**

`PUBLIC_RUNTIME_VERIFIED=False`. Public Worker activation remains **NO-GO**. Multiple-instance/revision-overlap analysis remains deferred for this diagnostic API phase.

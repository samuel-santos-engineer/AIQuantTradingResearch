# Codex prompt — Release 1.15 WP66: one bounded WP61 UI identity/platform inspection

You are working in `samuel-santos-engineer/AIQuantTradingResearch`. Begin from fresh `origin/main` containing the squash merge of PR #461, commit `4a36cd983c7730d89736b7d0f0db0da122f7411b`. Create a focused branch and a single PR, leave it OPEN/UNMERGED. Do not modify or reconcile unrelated PR #457.

## Objective and exact one-time authority

Resolve the *minimal* real local identity/platform observation for the accepted WP61 UI image using the Python-only WP65 helper. The owner authorizes **at most one new, read-only Docker daemon image inspect** of the exact image ID below, on the already-qualified local Docker Desktop Linux context, provided all preflight checks pass. This authority does not carry over to a retry, even if a template error occurs. No Go installation, execution, harness, dependency or acceptance gate.

Accepted local image ID:
`sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69`

The **only permitted daemon/image command** is the existing fixed helper-generated equivalent of:

```text
docker --context desktop-linux image inspect sha256:3e323b0d3008a1e7256877c4c9f87484c25eee51448f57ac2dddd2a9d02bbd69 --format '{"Id":{{json .Id}},"Os":{{json .Os}},"Architecture":{{json .Architecture}}}'
```

Use a direct argument array, never shell interpretation. The template must request only Id/Os/Architecture. No raw/full inspect JSON or sensitive fields may be captured, logged, printed or committed. This one inspect is authorized **only in Codex's verified original Windows host with the same local Docker Desktop Linux context/endpoint**. If the environment differs, STOP without executing Docker. Do not silently substitute a cloud runner, remote daemon, alternative context or a different image.

## Preflight and bounded execution

1. Read the merged WP61 acceptance receipt, WP64 OCI verifier/evidence and WP65 helper/tests/evidence. Confirm current `main` and the exact accepted ID. Preserve the two previous inspect failure records verbatim: first CLI_FAILED, then approved retry exit 1 in 0.395 seconds with `templateError=true`; neither proves image absence or corruption.
2. Confirm the local host, Docker context `desktop-linux` and expected Docker Desktop Linux named-pipe endpoint through the same previously qualified local metadata-only preflight, with no context change. Check only **presence** (not values) of `DOCKER_HOST`, `DOCKER_TLS_VERIFY`, `DOCKER_CERT_PATH` overrides; stop if any is present or endpoint/context is not exact. Preflight may use only the previously documented local context metadata commands, not `docker version`, image inventory or daemon probing.
3. Run the exact image inspect **once at most** using the already qualified Windows bounded subprocess/owned Job Object capture primitive: 10-second request deadline, combined stdout/stderr cap 1 MiB, bounded owned descendant/pipe cleanup; do not leave orphaned processes. Retain only numeric exit, duration, safe fixed error categories (`templateError`, `imageNotFound`, `daemonUnavailable`, `permissionDenied`, `unclassified`) and safe parsed projection booleans. Do not persist raw stderr or raw command output. Treat error classification as limited evidence, not proof that other causes are absent.
4. If command fails, times out, exceeds bounds, returns wrong ID, malformed JSON or unsupported output, stop **without retry**. Do not expand the template, inspect another image, list images, export, rebuild, pull or publish. Record the precise bounded observation and UNKNOWNs.
5. If command succeeds, run the existing Python `project(raw)` parser on the transient three-field JSON. Report exact-ID/Linux/amd64 checks only; the helper's overall result must remain UNVERIFIED for UID/port/entrypoint/repository/OCI provenance because those fields were not requested. Do not infer config/index/child digest, publication, deployable GHCR reference or Azure equivalence from this projection.

## Explicitly prohibited

No other Docker daemon/image/container/volume operations, no Docker image save/export, pull, build, tag, push, container execution, daemon start, or fallback. No registry HTTP, GHCR access, Azure reads/writes, credentials, tokens, private runtime files, public probes or deployment. No Go, extra language toolchain, packages, licensing/approval gates or Windows ACL prerequisite for Linux deployment. No changes to UI/API behavior, auth, TLS, secrets, ports, storage or data. `PUBLIC_RUNTIME_VERIFIED=False`; `liveAttemptReady=false` throughout.

## Offline regression and evidence

Before and after the one possible inspect, run existing Python-only tests for WP65, WP64 and setting-name parser/collector. Add only focused synthetic tests if required to make the new bounded observation recording robust; no Go or extra dependencies. Verify Python syntax, UTF-8/LF, Markdown links, and `git diff --check`. Preserve original WP65 failure history, and append a clearly labeled WP66 observation with timestamps, actual operation count, numeric exit, bounded cleanup, redacted error category and safe identity/platform result. Never claim OCI provenance or publication from synthetic tests. If the image is missing, stop and request owner disposition, not a rebuild.

## Deliverable

Create one focused PR against current `main`, OPEN/UNMERGED. Report PR URL, base/head SHA, files changed, tests passed/skipped, exact Docker operation count, preflight outcome, sanitized result and remaining UNKNOWNs. Explain the smallest *separately approvable* next step based on actual results, without executing it. Preserve API-first/UI-second Azure deployment order, accepted API child `sha256:2744c4dc3d8b73b30a59d008fdafd94384eec684ac194a7033b31e172475efc3`, and keep diagnostic API child separate. The Azure configured UI digest `sha256:a06baf213628603ab88d0fa0d4fff6977f3ae80c5e2114d174b98763bef6da07` is historical configuration evidence, not verified equivalence to WP61.

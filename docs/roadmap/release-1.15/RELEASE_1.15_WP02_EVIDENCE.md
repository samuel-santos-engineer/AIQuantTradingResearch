# Release 1.15 WP02 - Preflight evidence

Date: 2026-10-23. **BLOCKED at separate exact-path authorization; runtime implementation NOT STARTED.** UTF-8/no BOM/LF/final newline required. `PUBLIC_RUNTIME_VERIFIED=False`.

## Verified observations

- Live PR #380 MERGED at `6f76697707e2363cc52820e0c805172815b92159`; origin/main matches; #367 CLOSED and #368 OPEN. Explicit owner acceptance was read on PR #380: D1-A/D3-A/D4-A/D5-A accepted as engineering baseline, historical pre-acceptance labels superseded without inventing production evidence.
- No tracked edits existed. Sole untracked file was the executed WP02 prompt now included. Original SHA-256 `A670A7E32627C13DC45CC8C17A386AF39E94CE33DBE4EF6F505760EF22B18548`. Local owner file spells issue #368 once; the pasted attachment duplicates #368. Preserve the local file wording rather than overwrite it with the attachment; only UTF-8/LF normalization. No unrelated prompt deletion/move.
- Safe commands executed: git fetch origin; git switch main; git merge --ff-only origin/main; git switch -c feat/release-1.15-wp02-runtime-isolation. Main/origin align at the accepted merge. Prior WP01 branch preserved; no reset/rebase/force-push.
- docker context show returned desktop-linux. Initial sandboxed docker info failed with named-pipe permission denied. Approved retry outside the sandbox, `docker info --format '{{.ServerVersion}}'`, exited0 with29.7.2. This verifies daemon access only, not image/kernel/security compatibility. No daemon restart/permission weakening.
- A generic preflight filename search did not exist; inventory located the actual tracked [WP02 preflight](prompters/release-1.15-wp02-codex-docker-dual-process-runtime.md). Item2 requires approval of literal mutation paths before code. New authority item4 explicitly preserves that narrower gate. [Exact-path plan](RELEASE_1.15_WP02_IMPLEMENTATION_PLAN.md) is the next review deliverable.

## Qualification ledger

| Category | Result |
| --- | --- |
| Live predecessor/issues/main reconciliation | PASS |
| Authorized prompt inventory/preservation | PASS, one prompt only |
| Docker daemon access | PASS on approved retry; sandbox failure retained above |
| Ten proposed source paths/native approach | PENDING owner approval |
| Offline assets/compiler/header verification | NOT RUN |
| Image build / final digest | NOT RUN / none |
| Deterministic tests | NOT RUN, zero tests |
| Image negative/lifecycle tests | NOT RUN, zero tests |
| Capability/seccomp/namespace/closure compatibility | UNVERIFIED |
| CPU/RSS/startup/shutdown measurements | NOT RUN / none |
| Provider/Azure/registry/deployment | NOT RUN / not authorized |

No confidentiality, exact-capset, FD/environment sanitation, threading fallback, task death/closure or no-overlap proof is claimed. Same-role replacement remains disabled absent a qualified complete witness; proc scans alone are insufficient. WP03 storage/recovery, WP05 real protocol and WP07 actual F1/deployment overlap remain downstream. No source, executable tests, CI, production runtime or historical evidence changed. No PR opened before required literal-path approval and substantive implementation/evidence. Owner may approve/amend the exact plan and proposed native C approach; no merge or WP02 acceptance requested.

## Documentation validation

Four-path documentation inventory,20 relative Markdown targets, append-only manifest, independent UTF-8/no-BOM/LF/final-newline scan and git diff --check PASS. All run-only regression paths were checked against the actual layout; an initially proposed nonexistent preview test name was replaced with existing `python/presentation/test_public_runtime_isolation.py` before handoff. The executed local prompt remained byte-identical at its recorded SHA-256. No product tests or build performed; no commit/push/PR created at this approval stop.

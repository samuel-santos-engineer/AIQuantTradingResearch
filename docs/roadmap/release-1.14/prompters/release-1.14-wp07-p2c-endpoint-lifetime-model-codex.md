# Codex Authority — Release 1.14 WP07-P2C
## Offline Launch-Gate Descriptor and Endpoint-Lifetime Model

**Owner authorization:** P2-C3 approved. Implement only the bounded offline P2C model and focused tests described here. **STOP — OWNER REVIEW REQUIRED** after completion. This authority does **not** permit a PR, merge, or runtime integration.

### Protected baseline and preservation
- Remote `main` reported at PR #331 merge `e8ccd21fccf101847f93eac8670dd6ffc2bb76b8`; verify before work, do not assume refs are current.
- Protected dirty checkout: `draft/release-1.14-wp07-partial-public-disabled`, HEAD `67893f51931ee183f71050a83b81127e6e648b87`. Do not switch/reset/clean/stash or otherwise disturb it.
- Before mutation, record branch, HEAD, full `git status`, refs, and SHA-256 of the two allowlisted files. Expected P2B reviewed hashes:
  - `container/admin_lifecycle_launch_plan.py`: `E7EE689044F9079F75CE4DBBF306BC2577E3A5F98A57657E0C7C89F77CA74252`
  - `python/presentation/test_admin_lifecycle_launch_plan.py`: `D908839673F8C4E661FB0EEFFC440AD84E7B99175913C4296151B3F041BC2719`
- If these do not match, or source provenance is ambiguous, STOP. Preserve all other files, including untracked/ignored content; inventory and hash non-allowlisted Git-listed files before/after where feasible. Do not claim ignored-file preservation beyond evidence.

### Exact mutation allowlist — existing files only
1. `container/admin_lifecycle_launch_plan.py`
2. `python/presentation/test_admin_lifecycle_launch_plan.py`

No new files, project/dependency changes, generated repository outputs, GitHub operations, Docker, Azure, credentials, or production configuration. Use `python -B` and avoid cache writes.

### Frozen decisions — do not reinterpret
- A1–A3, D1 / strict D2 / D3, T1–T11, C1, P2-C2 and **approved P2-C3** remain binding. The offline model is an obligation checker, never a substitute for Linux evidence.
- P2-C2: **agent metadata FD4 survives agent exec**, is validated exactly once by the executed agent, then closes. The separate guardian–agent bootstrap launch gate closes **before** agent exec, following `A_EXEC` / `A_EXEC_ACK`.
- P2-C3: approve a **separate transient agent-bootstrap launch-gate descriptor**, distinct from agent runtime FD3 (RA) and metadata FD4. Require explicit closure after ACK and before exec, CLOEXEC as a backstop, no surviving gate aliases, and unchanged postexec FD map. **Do not assign a numeric agent-side gate FD**; preserve its symbolic/unassigned status for a separate owner decision.
- Guardian FD4 is its own launch-gate endpoint and does not imply agent-bootstrap FD4. Descriptor numbers are per-process.
- Preserve canonical process tree: root PID1 finite secret-free pre-fork → supervisor UID/GID1200 PID1 with router UID/GID1100 and guardian UID/GID1300 direct children; guardian is agent UID/GID1000 direct parent; agent owns UI UID/GID1000. Numeric identities remain conditional on image-specific collision checks; no allocation.
- Preserve isolation, permanent identity/capability hardening, router-only post-separation synthetic credential, no pre-fork secret, no public exposure before `R_EXPOSE`.
- Preserve 5-second privileged startup, 30-second total startup, shared 4-second `CLOCK_MONOTONIC` shutdown, 5-second Docker grace, positive bound kernel-wait obligations and T2 ordering. No deadline reset or EOF/report substitution.

### Required P2C model enhancements
1. Express phase-specific descriptor ownership for pre-fork, bootstrap, post-hardening, pre-exec, post-exec, metadata validation, and UI spawn as **static declarations**. Avoid asserting that any real descriptor was closed.
2. Represent each logical gate endpoint and any duplicate/alias distinctly; require closure of **all** gate endpoints/aliases before agent exec. Reject a lingering alias even when the nominal gate FD is closed.
3. Require `FD_CLOEXEC` as a modeled backstop on transient gate descriptors while also requiring **explicit close**; CLOEXEC alone cannot satisfy the obligation.
4. Preserve agent FD3 RA and temporary metadata FD4 without remapping or collision; reject any gate reuse of FD3/FD4 and any collision within a process. The gate's agent-side numeric FD must remain unspecified.
5. Validate metadata FD4 survives exec, is validated exactly once afterward, and then closes; reject premature closure, repeated validation, or metadata leakage to UI.
6. Model close-unused-end obligations and inheritance/alias checks across all relevant fork/exec boundaries; UI must inherit no lifecycle, credential, metadata, or gate descriptors.
7. Preserve sequence and milestone ordering: `A_EXEC` → `A_EXEC_ACK` → gate closure → agent exec → metadata validation → `A_READY`; prohibit premature `R_EXPOSE` and listener exposure. Keep all startup/shutdown deadline and wait obligations unchanged.
8. Preserve canonical session separation; add explicit rejection of session-alias mutations, including same-session `SIGCONT` risk. Do not pretend to enforce kernel sessions.
9. Keep immutable/fail-closed model inputs, reject invalid mutations and ambiguous phase/owner mappings. Favor the smallest coherent API changes and do not modify the merged protocol implementation.

### Focused offline tests (authorized)
- Preserve the 15 existing passing tests and add explicit negatives for: gate FD3/FD4 collision; unclosed gate and alias; CLOEXEC-only without explicit close; missing CLOEXEC; leaked unused pipe end; wrong endpoint owner; pre-exec metadata closure; metadata validation zero/twice; UI inheritance; descriptor remap; session alias; `R_EXPOSE` before prerequisites; deadline reset; missing positive bound-wait obligations.
- Run only focused tests, e.g. `python -B -m unittest python.presentation.test_admin_lifecycle_launch_plan -v`, and any non-mutating syntax check that does not generate repository artifacts. Record commands, counts and exit codes. No native FD calls, fork/exec, `setuid`, signals, real waits, credential generation, listener binding, build/restore, or live qualification.

### Stop gates
STOP immediately on: need to allocate a numeric agent gate FD; modify frozen runtime maps or wire protocol; need a third file or dependency; ambiguity about metadata/gate lifetimes; unexpected baseline/hash mismatch; inability to preserve dirty checkout; failing focused tests that cannot be corrected within scope; any required native/runtime action. Do not silently broaden scope or claim static checks prove confinement.

### Final report
Provide initial/final branch, HEAD, full status and preservation evidence; before/after hashes of the two edited files; concise diff summary; test commands/results; exact new negative cases; unresolved risks and proposed next owner decision. Verdict exactly one of:
- `WP07-P2C OFFLINE ENDPOINT MODEL COMPLETE — OWNER REVIEW REQUIRED`
- `WP07-P2C BLOCKED — OWNER REVIEW REQUIRED`

`PUBLIC_RUNTIME_VERIFIED=False`. Administrative API availability, ingress TLS, Azure passive qualification and public Worker activation remain **NO-GO**.

# Release 1.14 WP07 — Azure console-access capability assessment (Codex prompt)

## Authority and purpose

This authority is **planning and non-invasive capability assessment only**. Determine whether either existing Azure App Service F1 Linux-container application offers an **already available, authenticated console path into the actual running application container**, without modifying infrastructure or application state. Do not treat this authority as approval to connect to a console or execute the previously conditionally approved C1–C3 diagnostics.

**Mandatory current state:** `PUBLIC_RUNTIME_VERIFIED=False`; **NO-GO for implementation selection**. Preserve the synthetic BTC-like twelve-selection contract and the existing approved `/home` SQLite database as the sole permitted production storage candidate. No public Worker activation.

### Fixed targets (do not broaden scope)

| ID | App | Resource group | Region |
|---|---|---|---|
| T1 | `aiqr112wp035ec325382770` | `rg-aiq-r112-wp03-wcus-5ec325382770` | West Central US |
| T2 | `aiqr112wp05wus27f5eabb5` | `rg-aiq-r112-wp05-wus2-7f5eabb5` | West US 2 |

Subscription: `7f5eabb5-80f5-4369-88be-b14a6de763f1`.

Previously observed: both running Linux containers on F1/Free, one configured worker each, no slots returned, same configured image digest. This is **configuration evidence**, not proof of current process identity, actual image, or absence of recycle overlap.

## Permitted work — Stage A only, now

1. Read local repository source, deployment scripts, prior WP07 reports and nonsecret existing evidence. Capture branch, HEAD and before/after `git status --short`. Do not edit files or run builds/tests.
2. Consult **public, unauthenticated Microsoft/Azure documentation** about console availability and behavior for **custom Linux containers on App Service Free F1**, including distinctions between:
   - Portal SSH / `az webapp ssh` and their prerequisite SSH daemon or image setup;
   - SCM/Kudu console or command API versus application-container execution context;
   - Portal console / diagnostic interfaces, if applicable;
   - F1 tier limitations, permissions and any dependency on restart, deployment or configuration changes.
   Cite official URLs, page titles, and retrieval date. Mark undocumented or version-sensitive behavior **UNKNOWN**, not supported.
3. Review only previously approved, already captured Azure metadata. Do **not** call Azure APIs or CLI, authenticate, probe endpoints, fetch settings, enumerate resources or initiate console sessions in Stage A.
4. Produce a **candidate capability matrix** per T1/T2: documented support, prerequisite, target context (application container vs SCM/host), whether the existing image is shown to meet prerequisites, privilege/identity implications, and evidence needed to verify safely.
5. If a narrow live metadata check would materially resolve a question, prepare an **exact proposed Stage B manifest** with each command/API call, app, expected sanitized fields, credential scope, possible side effects, and why the call cannot expose secrets. Explicitly label every proposed command **NOT EXECUTED — SEPARATE OWNER APPROVAL REQUIRED**. Prefer no calls if capability can be assessed from existing evidence. Do not substitute commands or expand discovery if a call fails.
6. Provide a bounded, separate **Stage C session-verification proposal** only if a pre-existing authenticated route is plausibly available. Specify the exact method and how to prove application-container context using minimal, nonsecret observations. **Do not open the session or execute verification.** If proof requires application-container commands, identify them for a new owner decision, not as authorized commands.
7. End with an explicit STOP and a short owner decision request: approve/reject a specific Stage B manifest, or report that no safe existing route is demonstrated.

## Prohibited without new explicit authority

- **Any** live Azure operation, including `az account`, `az webapp`, ARM/SCM/Kudu requests, portal console actions, login, authentication tests, endpoint probes, and live resource discovery.
- SSH/Kudu/console connection or execution; retrieval of publishing profiles, tokens, credentials, app settings, environment variables, logs, filesystem contents, or database metadata.
- Installing or starting SSH, enabling remote debugging, changing RBAC, identities, permissions, container image, app configuration, startup command, networking or deployment.
- Restart, scale, slot operation, deployment, diagnostic image, resource creation, paid service, or recurring-cost change.
- Worker launch, SQLite open/read/write, process signal, namespace/cgroup experiment, active failure test, load probe or performance sampling.
- Git commits, branches, PRs, GitHub mutations, or any repository file writes.

## Separation from earlier conditional diagnostic approval

Owner previously **conditionally approved** the exact commands **C1a–C1d, C2a–C2b and C3a–C3d**, once per T1 and T2, **only after** an existing authenticated application-container console is independently confirmed and separately approved. That approval does **not** authorize transport discovery, console connection, or C1–C3 execution now. Do not reproduce or execute these commands as part of this assessment.

## Required report (chat only; no file writes)

A. Baseline and exact actions taken, including URLs of any public documentation consulted and confirmation no live Azure calls occurred.

B. Microsoft documentation evidence table: claim, applicability to Linux custom container/F1, URL, date, caveat.

C. T1/T2 candidate transport matrix with classifications **DOCUMENTED**, **REPOSITORY-PROVEN**, **PREVIOUSLY OBSERVED AZURE METADATA**, **UNKNOWN**, **BLOCKED**; do not conflate these evidence levels.

D. Risks and controls: SCM-versus-app-container ambiguity, credential exposure, root-versus-unprivileged identity, revision/recycle overlap, diagnostics resource cost, and region-specific divergence.

E. If justified, exact separately approvable Stage B live-metadata manifest (no settings dumps or generic enumeration) and proposed later Stage C console-verification gate. Otherwise explicitly state no safe command can yet be proposed.

F. Decision and STOP. Keep `PUBLIC_RUNTIME_VERIFIED=False`, **NO-GO for implementation selection** and the single existing SQLite storage constraint.

**Do not infer that F1, one configured worker, a shared image digest, or availability of Kudu automatically proves application-container console access or safe global admission.**

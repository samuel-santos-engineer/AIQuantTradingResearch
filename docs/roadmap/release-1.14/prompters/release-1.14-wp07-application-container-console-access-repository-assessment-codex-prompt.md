# Release 1.14 WP07 — Application-container console access: repository-only assessment

## Authority and hard stop

This is a **planning-only, repository-local** Codex assignment. The owner has conditionally approved the *exact* Stage B diagnostic commands C1a–C1d, C2a–C2b and C3a–C3d, once per app T1 and T2, **only after** an existing authenticated console into each actual application container is separately confirmed and approved. That condition is **not satisfied**. This prompt does **not** authorize transport discovery against Azure, any Azure CLI/API call, network request, authentication attempt, SSH/Kudu/SCM session, credential retrieval, container command, diagnostic execution, deployment, configuration change, database access, Worker execution, GitHub operation, or file mutation.

**Maintain `PUBLIC_RUNTIME_VERIFIED=False` and NO-GO for implementation selection.** Do not infer authorization from the owner's conditional approval. Stop and request separate owner approval before any interaction with Azure or a live container.

## Objective

Determine, from existing repository files and previously supplied approved evidence only, whether the project already documents an authenticated **application-container** console route that could be proposed for separately approved verification. Distinguish actual application-container execution from host, SCM/Kudu, build, or deployment environments. Do not establish or test a connection.

## Repository baseline

- Expected branch: `draft/release-1.14-wp07-partial-public-disabled`.
- Expected HEAD: `67893f51931ee183f71050a83b81127e6e648b87`.
- Working tree was already dirty (six modified tracked files and untracked WP07 materials). Preserve all existing work; no checkout, reset, clean, stash, commit or writes.
- Prior metadata: T1 `aiqr112wp035ec325382770`, resource group `rg-aiq-r112-wp03-wcus-5ec325382770`; T2 `aiqr112wp05wus27f5eabb5`, resource group `rg-aiq-r112-wp05-wus2-7f5eabb5`. Both were reported as Linux-container F1 apps, one configured worker each, no returned slots, same configured image digest. These facts do **not** establish live runtime topology, console access, or absence of recycle overlap.

## Permitted work

1. Inspect `git status --short`, `git branch --show-current`, `git rev-parse HEAD`, and relevant local repository content using non-mutating local commands. Record before/after status; do not fetch.
2. Review Dockerfile, `container/entrypoint.sh`, Azure deployment scripts, repository runbooks, existing WP07 governance documents, and any local documentation describing App Service container consoles, SSH setup, Kudu/SCM access, or container diagnostics. Search repository text only. Treat claims in documentation as hypotheses, not proof of live access.
3. Build an evidence-indexed inventory of *documented* console access paths. For each, identify prerequisites, whether it is an existing authenticated route or would require enabling/configuring something, whether it reaches the actual application container, and the evidence gap. Do not assume `az webapp ssh` is available; the current image has no repository-proven SSH server configuration. Do not assume Kudu shell is the app container.
4. Propose a minimal, explicit **future owner approval gate** for verifying the transport and container context. Describe what evidence would be necessary to bind an existing authenticated session to the correct app/container/revision and how to avoid exposing secrets. **Do not execute or authorize** the verification commands or propose retrieving credentials as a shortcut.
5. Cross-reference the frozen Stage B manifest exactly: C1a–C1d, C2a–C2b and C3a–C3d once on each of T1/T2. Explain that even a proven console route would require separate owner approval before any of those commands run.
6. Identify any contradictions between documented access methods and the immutable-image, UID-separation, `/home` SQLite, and zero-additional-recurring-cost constraints. Keep SQLite as the only approved storage candidate; do not recommend adding a privileged service or alternate database as an implicit solution.

## Forbidden work

- No Azure commands, API calls, portal actions, web browsing, external searches, DNS/network probes, log access, authentication, console opening, or transport discovery against running services.
- No app-settings/environment enumeration, secret material, database/sidecar inspection, `/proc` reads, filesystem probes inside containers, signals, namespace experiments, or active qualification.
- No changes to source, docs, tests, schema, image, resource configuration, Git state or GitHub.
- No Worker launches, public activation, performance probes or new recurring costs.
- No substitution of an unapproved transport or diagnostic command if a candidate fails.

## Deliverable — chat-only report

Provide sections:

A. **Baseline and exact actions** — branch, HEAD, unchanged working-tree status; all local commands actually executed.

B. **Source-indexed evidence** — repository paths and line numbers for each console/transport-related finding, including evidence of absence where relevant.

C. **Candidate transport matrix** — candidate, repository evidence, prerequisites, app-container vs SCM/host confidence, missing proof, whether any live action would be required. Label `REPOSITORY-PROVEN`, `DOCUMENTED ONLY`, `UNKNOWN`, or `BLOCKED` carefully.

D. **Proposed separate approval request** — smallest possible *future* console-context verification, with exact proposed actions and expected sanitized output if supported by repository evidence. If no safe existing route is demonstrated, explicitly recommend STOP and state what owner-supplied evidence is needed; do not invent a transport.

E. **Stage B dependency** — reproduce the approved diagnostic IDs and limits, and explain why conditional approval does not permit execution now.

F. **Decision** — `NO-GO for implementation selection`, `PUBLIC_RUNTIME_VERIFIED=False`, unresolved identity/mount/termination/durability/global-admission/C2 evidence.

End with **STOP — owner approval required**. Do not write a report file unless separately authorized; respond in chat only.

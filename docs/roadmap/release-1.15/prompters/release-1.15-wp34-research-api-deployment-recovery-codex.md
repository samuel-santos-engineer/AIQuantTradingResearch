# Codex implementation prompt — Release 1.15 WP34: Research API deployment diagnosis and recovery preparation

Repository: `samuel-santos-engineer/AIQuantTradingResearch`
Starting point: latest `main`, including merged WP33 PR #421 (squash `9adc53869b1d258beaff63760aed217cd7abe15d`). Create a focused feature branch and a reviewable PR. **Do not merge.**

## Owner evidence and objective

The owner inspected the existing Azure Linux F1 research App Service `aiqr115researchstagingwus27f5eabb5` in resource group `rg-aiq-r112-wp05-wus2-7f5eabb5` (West US 2). Azure Overview shows Running / Healthy, publishing model Code, Runtime Stack `-`, Health Check Not Configured. Browser GET `https://aiqr115researchstagingwus27f5eabb5.azurewebsites.net/health` displays **404 Not Found — nginx/1.30.5**. Deployment Center → Settings shows an uncompleted GitHub authorization/configuration form; Containers (new) lists no entries. These observations do **not** prove that the research API was deployed, nor that no application image exists: the Containers (new) tab may concern sidecars. Do not misrepresent them as a definitive deployment diagnosis.

Goal: establish the actual deployed runtime/configuration from repository and authorized read-only evidence; prepare the **smallest** Docker-first-compatible correction needed to serve the existing .NET research API `/health` endpoint (expected HTTP 200, body `Alive`) on the existing research app. Preserve the dual-process development option. Avoid new infrastructure, CI/CD authorization, new vaults, or unnecessary governance gates.

## Scope and permissions

**Authorized now:** offline repository inspection, read-only code changes, fake/mocked tests, local builds/tests where tools permit, documentation, PR. You may use existing read-only Azure tooling **only if already explicitly authorized for this exact inspection and it does not access sensitive app-setting values or private attempt records**; otherwise produce a precise owner-run read-only checklist and ask for narrow authorization. Do not infer permission to perform new Azure requests from prior qualifications.

**Not authorized:** any Azure configuration mutation, app restart, deployment, registry push, secret read/write/export, authenticated research API probe, provider calls, live controller, private baseline/attempt read/reuse/rearm, UI app changes, or merge. Do not access the owner's real key or local `generate-key.ps1`; do not request, print, copy, hash, log, or commit a real secret. Owner has generated and retained one key; never regenerate or overwrite it. Existing `AIQ_RESEARCH_API_KEY` status is not yet verified from the screenshots; preserve it if present.

Research app: `aiqr115researchstagingwus27f5eabb5`; resource group: `rg-aiq-r112-wp05-wus2-7f5eabb5`; plan: `asp-aiq-r112-wp05-wus2-7f5eabb5` F1, West US 2. **Excluded:** UI app `aiqr112wp05wus27f5eabb5`.

## Required investigation

1. Inventory actual research API source/project, `/health` route and `POST /v1/research/snapshot` behavior; confirm expected response from code. Check target framework, Dockerfile/build context, exposed/listening port, host binding (`0.0.0.0`), startup command and runtime environment expectations. Do not change API authentication semantics.
2. Trace repository's existing Docker-first build/deploy pipeline and WP18–WP33 deployment/controller/rollback documentation. Identify exact image provenance, registry, tag/digest conventions, required Azure Linux App Service image/startup/port settings, and whether F1 supports the proposed path. Distinguish image deployment from code deployment; reconcile Azure Overview "Code" with the Docker-first governance. Do not assume a container deployment is supported in the current configuration without evidence.
3. Inspect repository evidence for prior research-app deployment (workflows, scripts, captures, sanitized reports) without reading private historical attempts or secret-bearing snapshots. Separate confirmed facts, hypotheses and unknowns.
4. Identify minimally sufficient **read-only** owner checks to distinguish: (a) no deployed application, (b) incorrect deployment mode/image, (c) startup/port binding error, (d) route mismatch. Prefer sanitized Azure portal views or safe metadata queries, not dumps of full app settings, publishing profiles, or secrets. Explain what the nginx 404 establishes and does not establish.
5. Implement a narrow, testable offline deployment preflight/diagnostic or correction in repository only if justified by source findings. Include failure-closed behavior, no mutation by default, no key handling, and deterministic fake tests. Do not build a new deployment framework or change existing controllers simply to bypass authority.
6. Write a concrete deployment and rollback plan for **later explicit approval**, including exact resource, artifact identity/digest, runtime/port settings, any restart, and preservation of `AIQ_RESEARCH_API_KEY`, other settings, slot metadata, logging, HTTPS-only, F1 plan, UI app and baseline semantics. Never claim rollback verified without executing it under authorization. Specify whether the target Azure resource must change publishing mode and what that would entail; if not determinable, stop and report the missing evidence.

## Acceptance criteria and evidence

- PR identifies the most likely cause **with evidence**, not guesses, and lists the precise remaining owner-visible checks if diagnosis is incomplete.
- No Azure or other external mutations, real key exposure, or unapproved live attempt.
- Offline tests and any feasible build checks pass; report exact passes, skips, platform/policy limitations. Avoid bypassing execution policy.
- Owner-run future deployment instructions are explicit and limited to the existing research app, with precondition checks and narrowly scoped rollback. Do not present future deployment as already performed.
- Target acceptance for **future authorized deployment**: GET `/health` returns HTTP 200 and literal `Alive`; research API key setting is preserved; UI remains unchanged. `/health` is not proof of authentication or authentic data import.
- PR description records changed files, source evidence, tests, open questions, risk and requested exact next approval. Keep `liveAttemptReady=false`, `AUTHENTIC_DATA_NOT_IMPORTED=True`, `PUBLIC_RUNTIME_VERIFIED=False` until independently established.

## Deliverables

- Focused repository code/scripts/tests as evidence warrants; avoid speculative changes.
- `docs/roadmap/release-1.15/prompters/release-1.15-wp34-research-api-deployment-recovery.md` documenting diagnosis, evidence, precise read-only checks, future deploy/rollback and test results.
- Open PR for owner review. **Do not merge or deploy.**

Work pragmatically: prioritize a working research API and eventual public historical candles, without inventing licensing blockers or repeated approval gates for offline implementation. If Azure evidence is insufficient, make the next owner check simple and specific rather than claiming a deployment fix.

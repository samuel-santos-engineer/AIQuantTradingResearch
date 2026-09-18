# GPT-5.6 Luna — Release 1.12 WP04 Qualification Runtime Composition Re-Governance Authority

**Authority state:** `READY`  
**Selected execution model:** **GPT-5.6 Luna**

## Model authority map

- **GPT-5.6 Luna** — PRIMARY: reconcile the qualification HTTP evidence design with the existing single-port container/runtime composition. Read-only/planning only.
- **GPT-5.6 Terra** — implementation, validation execution, approved Git/GitHub/Azure mutations, image publication, deployment, merge, and lifecycle after Luna defines the revised executable contract.
- **GPT-5.6 Sol** — supporting analysis/synthesis only; never silently replaces Luna or Terra.

## 1. Governed starting state

Release:

**Phase 4 — Release 1.12 WP04: Persistent SQLite Initialization, Data Update & Recovery**

Issue:

`#263`

Accepted evidence-channel architecture:

```text
E1 — H1_APPLICATION_HTTP_EVIDENCE
```

Accepted endpoint contract:

```text
GET /internal/wp04/persistence-qualification
runId=<exact-run-id>
X-WP04-Evidence-Token: <temporary high-entropy token>
```

Accepted activation:

```text
PersistentSqliteQualification__HttpEvidenceEnabled=true
```

Accepted source of truth:

```text
H1-S2 — .NET application reads its own atomic durable JSON artifact
```

Accepted lifetime:

```text
H1-L1 — Worker remains alive in bounded evidence-serving mode after qualification
and exits after successful retrieval or timeout
```

Accepted readiness:

```text
poll every 5 seconds
maximum 180 seconds
retryable not-ready status only
exact RunId required
```

Superseded historical candidate provenance:

```text
Source commit:
2add79d2063292687d9813f9022206e84b664627

Image digest:
sha256:d4a6f51f0762b3ca500e6858510f9b042a3be0dc812cc0ad50815bc1ff733378
```

No implementation mutation occurred under the blocked Terra authority.

## 2. Newly discovered architecture conflict

Read-only inspection established:

```text
Dockerfile:
  configures/exposes only Streamlit port 8501

container/entrypoint.sh:
  always starts both Worker and Streamlit

Streamlit:
  binds 0.0.0.0:8501
```

Therefore:

- a Worker-hosted HTTP endpoint cannot bind externally routed port `8501` while Streamlit is also listening;
- binding the Worker to a second internal port does not make it externally reachable through the current App Service/container contract;
- the existing seven-path implementation allowlist is insufficient;
- `container/entrypoint.sh` is required for any composition change;
- `Dockerfile` and/or App Service port behavior may also require reconciliation.

Accepted blocker markers:

`RELEASE 1.12 WP04 — HTTP EVIDENCE IMPLEMENTATION: BLOCKED`

`RELEASE 1.12 WP04 — EXECUTION AUTHORITY BLOCKED`

## 3. Re-governance purpose

Determine whether **qualification mode may intentionally alter runtime composition** so the .NET Worker becomes the sole externally reachable listener on port `8501`, while ordinary runtime remains unchanged.

The problem to solve is transport/runtime composition only.

Do not weaken persistence acceptance semantics.

## 4. Binding invariants

The following remain binding:

- ordinary public reference runtime remains Streamlit on port `8501`;
- normal entrypoint behavior remains Worker + Streamlit unless qualification mode is explicitly activated;
- Python/Streamlit must not gain SQLite ownership;
- qualification evidence remains application-owned .NET output;
- direct SQLite inspection remains forbidden;
- exact-run attribution remains mandatory;
- durable artifact remains application-owned;
- schema remains v4;
- journal remains DELETE;
- persistent `/home` remains authoritative;
- strict F1 / $0 reference architecture remains unchanged;
- no production-SLA claim;
- no paid reverse proxy, sidecar, ingress service, or second App Service may be introduced.

## 5. Runtime-composition options

Evaluate exactly these options.

### Q1 — Qualification-mode single-listener substitution

When the process is launched in governed persistent-SQLite qualification mode with HTTP evidence explicitly enabled:

```text
Worker:
  starts qualification
  binds 0.0.0.0:8501
  serves only the governed evidence endpoint after artifact creation
  exits after successful retrieval or bounded timeout

Streamlit:
  is NOT started during this qualification lifecycle
```

Normal runtime remains unchanged:

```text
Worker + Streamlit
Streamlit owns 8501
```

Qualification mode therefore temporarily substitutes the Worker as the sole port-8501 listener.

### Q2 — Internal second port plus container reverse proxy

Add a proxy/router in the container so Streamlit and Worker can coexist on different internal ports.

This is a broader architecture change and must be rejected unless materially safer/smaller than Q1.

### Q3 — App Service port switching

Change App Service/container port configuration dynamically between normal runtime and qualification mode.

This adds Azure configuration mutation and risks runtime drift. Reject unless Q1 is technically impossible.

### Q4 — Separate qualification deployment/runtime

Use a distinct deployment slot/app/container boundary for qualification.

Reject if it introduces paid infrastructure, separate persistence semantics, or breaks the exact same-instance/same-storage acceptance boundary.

### Q5 — No safe runtime-composition solution

WP04 remains blocked.

## 6. Required decision

Select exactly one:

```text
R1 — Q1_QUALIFICATION_SINGLE_LISTENER_SUBSTITUTION
R2 — Q2_INTERNAL_PROXY
R3 — Q3_APP_SERVICE_PORT_SWITCHING
R4 — Q4_SEPARATE_QUALIFICATION_RUNTIME
R5 — Q5_NO_SAFE_COMPOSITION
```

Preferred decision, if technically valid, is the smallest architecture-preserving option.

Do not select Q1 merely because it is simple; prove compatibility with the repository/container/App Service contract.

## 7. Q1 contract if selected

If `R1` is selected, define all of the following explicitly.

### 7.1 Entry-point mode detection

`container/entrypoint.sh` may suppress Streamlit only when all required qualification signals are present.

At minimum require:

```text
Worker__Mode=PersistentSqliteQualification
PersistentSqliteQualification__HttpEvidenceEnabled=true
```

Do not infer qualification from a generic environment variable or image tag.

### 7.2 Port ownership

In qualification HTTP mode:

```text
Worker evidence host -> 0.0.0.0:8501
Streamlit -> not started
```

In normal runtime:

```text
Streamlit -> 0.0.0.0:8501
Worker -> existing non-HTTP behavior
```

No simultaneous bind conflict is allowed.

### 7.3 Dockerfile

Determine whether the existing:

```text
EXPOSE 8501
```

already satisfies Q1.

If yes, Dockerfile mutation is **not** required.

If another Dockerfile change is required, state exactly why.

Do not add a second exposed port unless selected architecture actually needs it.

### 7.4 App Service port contract

Determine whether the existing App Service routing to port `8501` can remain unchanged during qualification.

Preferred:

```text
Azure port configuration: unchanged
```

If an Azure port mutation would be required, Q1 is not the minimal solution and must be reconsidered.

### 7.5 Entrypoint lifetime

In qualification HTTP mode:

- start only Worker;
- propagate Worker exit code;
- do not start Streamlit;
- do not leave orphan/background processes;
- preserve signal forwarding/termination semantics;
- preserve non-root execution contract after governed filesystem preparation.

### 7.6 Post-qualification restoration

Restoration is configuration-driven, not container-script mutation:

- temporary D3 settings removed;
- subsequent ordinary restart/redeploy returns to normal Worker + Streamlit composition;
- Streamlit again owns port `8501`.

No persistent mode switch may remain in the image or filesystem.

## 8. Required path-governance decision

Reconcile the prior seven-path allowlist.

If Q1 is selected, determine the minimum exact implementation allowlist.

Candidate expanded allowlist:

```text
MODIFY src/AIQuantTradingResearch.Worker/AIQuantTradingResearch.Worker.csproj
MODIFY src/AIQuantTradingResearch.Worker/Program.cs
MODIFY src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationExecution.cs
CREATE src/AIQuantTradingResearch.Worker/PersistentSqliteQualificationEvidenceEndpoint.cs
MODIFY tests/AIQuantTradingResearch.Infrastructure.Tests/AIQuantTradingResearch.Infrastructure.Tests.csproj
CREATE tests/AIQuantTradingResearch.Infrastructure.Tests/PersistentSqliteQualificationEvidenceEndpointTests.cs
MODIFY eng/azure-cli/r1.12-deployment/wp04-persistent-sqlite/verify-persistent-sqlite-webapp.ps1
MODIFY container/entrypoint.sh
```

This is an **8-path candidate**.

Add `Dockerfile` only if Luna proves it is necessary.

Return exact path count and operation count.

README remains forbidden.

## 9. Dockerfile decision

Return exactly one:

```text
DOCKERFILE CHANGE REQUIRED: YES
```

or:

```text
DOCKERFILE CHANGE REQUIRED: NO
```

If `YES`, identify the exact required change.

If existing `EXPOSE 8501` remains valid for both normal Streamlit and qualification Worker listeners, expected decision is `NO`.

## 10. Azure configuration decision

Return exactly one:

```text
AZURE PORT CONFIGURATION CHANGE REQUIRED: YES
```

or:

```text
AZURE PORT CONFIGURATION CHANGE REQUIRED: NO
```

If `YES`, define exact setting/API/mutation and why Q1 cannot avoid it.

Do not authorize such mutation under this Luna authority.

## 11. Container/process validation contract

If Q1 is selected, future Terra implementation must prove locally:

### Normal mode

```text
Worker starts
Streamlit starts
Streamlit listens on 8501
qualification HTTP endpoint unavailable
```

### Qualification HTTP mode

```text
Worker starts
Streamlit does not start
Worker listens on 8501
qualification endpoint is reachable
exact token + RunId rules apply
Worker exits after retrieval or timeout
```

### Exit/signal behavior

Prove:

- qualification Worker exit becomes container exit;
- SIGTERM/stop behaves correctly;
- no orphan Streamlit process;
- no double listener;
- no process left alive after qualification timeout;
- normal runtime composition is unaffected.

## 12. Existing non-root filesystem contract

Preserve the accepted E2-A container contract:

- container may begin as root only for narrow `/home/data` parent preparation;
- owner/mode preparation remains narrow;
- permanently drop to `aiq`;
- Worker and Streamlit run non-root;
- qualification Worker HTTP host also runs non-root.

Do not regress this contract.

## 13. Repository-change decision

Return:

```text
RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: YES
```

if any executable Q1–Q4 option is selected.

Define exact allowlist.

Schema change expected:

```text
SCHEMA CHANGE REQUIRED: NO
```

README change:

```text
README CHANGE REQUIRED: NO
```

## 14. Candidate/image finality

Because runtime code and/or entrypoint composition will change under any executable solution:

```text
NEW SOURCE COMMIT REQUIRED: YES
NEW IMAGE REQUIRED: YES
CURRENT CANDIDATE FINALITY: SUPERSEDED_AFTER_REMEDIATION
```

Preserve historical provenance of the prior commit/digest.

## 15. SCM basic-auth policy

The selected application-owned HTTP endpoint does not require Kudu.

Therefore final desired SCM state remains:

```text
allow=false
```

A future Terra Azure qualification authority must restore SCM basic auth to false before final WP04 acceptance unless Luna later changes the evidence surface.

FTP remains:

```text
allow=false
```

## 16. Exact next Terra scope

If `R1` is selected, the next Terra authority should be **local implementation/validation only**.

It may authorize:

- exact revised path allowlist;
- endpoint implementation;
- entrypoint qualification-mode composition;
- local container validation in both normal and qualification modes;
- build/test/static/signing gates.

It must not yet authorize:

- staging;
- commit;
- push;
- Docker/GHCR publication;
- Azure mutation;
- SCM policy mutation;
- PR creation;
- issue/Project/milestone lifecycle.

A separate candidate-publication authority must follow successful local implementation.

## 17. Mutation prohibition

This Luna authority is read-only/planning only.

Expected mutations:

```text
Repository mutations: 0
Git mutations: 0
Docker/GHCR mutations: 0
Azure mutations: 0
Provider mutations: 0
PR mutations: 0
Issue mutations: 0
Project #2 mutations: 0
Milestone mutations: 0
```

Required:

`RELEASE 1.12 WP04 — QUALIFICATION RUNTIME COMPOSITION RE-GOVERNANCE MUTATION AUDIT: PASS`

## 18. Required output

Return:

- Q1–Q5 comparison;
- exact R1–R5 decision;
- exact normal-mode composition;
- exact qualification-mode composition;
- port ownership contract;
- entrypoint mode-detection contract;
- Dockerfile decision;
- Azure port-setting decision;
- exact path allowlist/count/operation count;
- schema/README decisions;
- candidate/image finality;
- SCM basic-auth restoration requirement;
- exact next Terra scope;
- confirmation of zero mutations.

## 19. Terminal markers

Required:

`RELEASE 1.12 WP04 — QUALIFICATION RUNTIME COMPOSITION RE-GOVERNANCE: PASS`

`RELEASE 1.12 WP04 — QUALIFICATION RUNTIME COMPOSITION DECISION: <R1|R2|R3|R4|R5>`

`RELEASE 1.12 WP04 — SINGLE-PORT CONTRACT: RECONCILED`

`RELEASE 1.12 WP04 — DOCKERFILE CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — AZURE PORT CONFIGURATION CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — REPOSITORY CHANGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — SCHEMA CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — README CHANGE REQUIRED: NO`

`RELEASE 1.12 WP04 — NEW SOURCE COMMIT REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — NEW IMAGE REQUIRED: <YES|NO>`

`RELEASE 1.12 WP04 — CURRENT CANDIDATE FINALITY: <PRESERVED|SUPERSEDED_AFTER_REMEDIATION>`

`RELEASE 1.12 WP04 — QUALIFICATION RUNTIME COMPOSITION RE-GOVERNANCE MUTATION AUDIT: PASS`

If executable:

`RELEASE 1.12 WP04 — TERRA QUALIFICATION RUNTIME IMPLEMENTATION AUTHORITY: READY`

Final:

`RELEASE 1.12 WP04 — LUNA QUALIFICATION RUNTIME COMPOSITION RE-GOVERNANCE COMPLETE`

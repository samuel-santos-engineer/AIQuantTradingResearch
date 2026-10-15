# WP07 — Read-Only Architecture Closure

Date: 2026-10-10. Status: documentary closure of three feasibility questions; production qualification remains blocked.

## Approval and scope

The owner approved [WP07_READ_ONLY_QUALIFICATION_PLAN.md](WP07_READ_ONLY_QUALIFICATION_PLAN.md) as a **planning and evidence framework only**, then authorized this narrow read-only architecture assessment. Approval does not accept the implementation, prove feasibility, amend security requirements or authorize the procedures in that plan.

This assessment used repository documents and public primary documentation. No authenticated Azure queries, local probes, tests, fault injection, harness development, implementation, identity/RBAC changes or production integration were performed. This Markdown is the only new artifact. The [frozen assessment](WP07_OFFLINE_POLICY_FROZEN_EVIDENCE_ASSESSMENT.md) remains the implementation snapshot; public invocation stays disabled. Historical target observations are not reverified here.

| Question | Documentary conclusion | Remaining disposition |
|---|---|---|
| Broker/Worker kernel separation | Same UID alone cannot protect the broker; stronger namespace/descriptor/filesystem restrictions are plausible Linux mechanisms | Candidate remains conditional; target availability and complete boundary unproven |
| Azure journal semantics | Persistence is documented; the required protected, crash-safe file-journal transaction is not established | Existing filesystem backend is not qualified; custom mount lock dependence conflicts with published guidance |
| Quota freshness and acquisition | Usage read permission and managed identity are documented; usage schema lacks observation time | Least-privilege authentication is a candidate, not a freshness solution; generation remains unavailable |

## 1. Broker/Worker kernel isolation feasibility

### What can be closed from Linux documentation

Linux signal permission includes matching sender/target credentials. Therefore a broker and Worker with matching host credentials, reachable in the same PID namespace, cannot treat UID equality as protection against Worker signals. The ptrace access algorithm additionally considers credential match, dumpability, capabilities and security modules. Disabling dumpability can restrict tracing but does not establish signal isolation. These are kernel rules, not results measured on Azure. Sources: [kill(2)](https://man7.org/linux/man-pages/man2/kill.2.html), [ptrace(2)](https://www.man7.org/linux/man-pages/man2/ptrace.2.html).

A descendant PID namespace restricts process visibility to that namespace and descendants. This provides a potential direction for keeping the broker outside the Worker's visible process tree while preserving ancestor supervision. It does not by itself hide filesystem paths, Unix sockets or inherited descriptors. Source: [pid_namespaces(7)](https://www.man7.org/linux/man-pages/man7/pid_namespaces.7.html).

Linux supports seccomp syscall filters with `no_new_privs` rather than namespace administrative privilege; filters can persist into descendants. Seccomp reduces syscall exposure, and is not a complete sandbox by itself. Its Linux availability does not demonstrate that the App Service runtime permits installation of the necessary policy or that .NET works under it. Source: [kernel seccomp documentation](https://kernel.org/doc/html/latest/userspace-api/seccomp_filter.html).

### Candidate argument and limits

Inference: a broker outside a private Worker PID namespace, coupled with an isolated filesystem/proc view, no accessible broker endpoint, no inherited control/pidfd/namespace descriptors, immutable trusted code and restrictive capabilities/syscalls could support the approved boundary. This is a mechanism argument, not a selected implementation or successful target proof. It must address signals, tracing/process-memory APIs, descriptor acquisition, namespace reentry, Unix socket impersonation and filesystem substitution together.

`SO_PEERCRED` checking only UID/GID cannot distinguish broker and Worker sharing those IDs. Public role authentication must not rely on that equality alone. A pre-established private channel unavailable to Worker is a possible additional authority boundary; its lifecycle and descriptor closure would need separate proof. The current handshake policy does not provide that operational proof.

Using a fourth, distinct broker identity is an alternative that would improve credential separation, but creates a launch/cancellation privilege problem. It is not authorized or selected here. Neither a permanent privileged launcher nor new helper/dependency is silently introduced. Distinct identity also cannot replace filesystem, network and descriptor controls.

**Closure:** reject “same UID plus non-dumpable plus credential checks” as a sufficient security argument. Retain external supervision as preferred conditional candidate. No evidence currently establishes all required unprivileged namespace operations on Azure F1. Earlier unavailable writable cgroup delegation remains a separate accounting/resource limitation; namespace plausibility does not restore hard CPU enforcement. Before any development, supply a complete boundary/ownership argument and seek explicit authority for the bounded Q-ID proof packet. Failure requires owner-reviewed architectural change, not relaxed Worker trust.

## 2. Supported Azure journal storage semantics

App Service documents `/home` persistence across restarts and sharing across instances when persistent storage is enabled; Linux custom containers disable that storage by default. This establishes a persistence feature, not a guarantee for journal ownership, flock, rename and directory-fsync transaction semantics. This review did not query the target's current setting. Source: [custom container persistent storage](https://learn.microsoft.com/azure/app-service/configure-custom-container).

For **custom-mounted Azure Storage**, Microsoft advises against applications/components depending on file handles and locks. Custom mounts are separate storage resources and are billed separately. This applies directly to evaluating a proposed custom mount, but must not be misrepresented as an explicit statement that every platform-managed `/home` mount has identical guarantees. Source: [App Service storage mounts](https://learn.microsoft.com/en-us/azure/app-service/configure-connect-to-azure-storage?pivots=container-linux&tabs=portal).

**Closure:** neither the persistence statement nor existing local filesystem tests establishes the frozen file journal's complete transaction contract on platform `/home`. The earlier CIFS observation identifies a risk to investigate, not a backend qualification. A custom mounted share is not a supported shortcut for the lock-dependent candidate under the cited App Service guidance. Do not claim crash durability from successful `fsync` calls, mode bits, reopen or restart alone.

The missing evidence remains exact backend/mount identity, enforceable UID ownership, lock scope across instances, atomic replacement visibility, acknowledged persistence after file/directory flush, and behavior under interruption/remount/placement changes. Resolve these with authoritative backend-specific guarantees and, only after separate authority, Q-ST target evidence. If ownership is mount-wide or permissions ineffective, isolation fails even if bytes persist.

Alternative for future architecture review: a storage service transaction/conditional-write protocol could replace filesystem assumptions. Azure Blob leases govern writes/deletes and require the matching lease ID; they can be finite or infinite. They do not independently prove Worker death or safely reopen admission after lease loss. A service-based journal would need fencing, atomic state-version transitions and conservative recovery. Source: [Lease Blob](https://learn.microsoft.com/en-us/rest/api/storageservices/lease-blob).

No remote backend is selected: it would require reviewing resource/cost availability, authentication, source paths and security-contract implications. `/tmp` and ephemeral container disk cannot be authoritative restart history. Under the existing no-new-resource/no-additional-service boundary, there is currently **no qualified journal backend**. Absence of proof does not establish that every possible Azure backend is infeasible.

## 3. Azure quota timestamps and least-privilege acquisition

### Observation-time closure

The documented app-scoped `List Usages` response has `currentValue`, `limit`, `name`, `nextResetTime` and `unit`. It has no documented measurement-observation timestamp or sequence and no documented <=60-second freshness guarantee. Its request filter does not add an observation field to that response schema. Source: [Web Apps — List Usages, API 2025-05-01](https://learn.microsoft.com/en-us/rest/api/appservice/web-apps/list-usages?view=rest-appservice-2025-05-01).

Consequently, retrieval time, HTTP Date, response signing/authentication and next reset time cannot be substituted for actual observation age. This is a documented-schema limitation; it does not claim undocumented fields could never exist. An undocumented sample field would still require supported semantics and lag guarantees before satisfying the contract.

Azure Monitor exposes app `CpuTime` in seconds with instance dimension and one-minute time grain. That is useful reconciliation evidence, but it is not itself the daily/short quota counter pair. Source: [Microsoft.Web/sites supported metrics](https://learn.microsoft.com/en-us/azure/azure-monitor/reference/supported-metrics/microsoft-web-sites-metrics).

Metric points are aggregated into time intervals; a timestamp attached to an aggregate is not automatically a fresh quota observation. Without proving equivalent scope, completeness, reset alignment and publication lag, substituting metrics for quota would change the approved contract. Source: [metrics aggregation](https://learn.microsoft.com/en-us/azure/azure-monitor/metrics/metrics-aggregation-explained).

**Closure:** direct List Usages acquisition cannot, from the reviewed public contract alone, satisfy the approved observation-age rule. Keep the unavailable source. The remaining routes are a supported authoritative timestamp/lag contract, or an explicit owner-reviewed security-contract amendment. Neither is supplied here. An operator file or signed relay only authenticates what it carries; it cannot manufacture source freshness.

### Least-privilege candidate and credential boundary

The resource-provider permissions catalogue includes `microsoft.web/sites/usages/read` (and the distinct slot usage-read operation). Candidate authorization is a custom role containing only the needed read action, assigned at the exact app or slot scope, subject to later validation of dependencies. Subscription Reader/Contributor and settings/secrets-list actions are not needed merely to propose usage retrieval. Source: [Microsoft.Web operations](https://learn.microsoft.com/en-us/azure/role-based-access-control/permissions/web-and-mobile#microsoftweb).

App Service supports system-assigned and user-assigned managed identity. Its token endpoint is exposed through `IDENTITY_ENDPOINT` and a rotating `IDENTITY_HEADER`; enabling identity and assigning resource access are configuration/RBAC operations. Source: [App Service managed identity](https://learn.microsoft.com/en-us/azure/app-service/overview-managed-identity).

Inference: a system-assigned identity plus app-scoped usage-read role is the smallest resource-creation candidate to assess, if separately approved; it avoids distributing owner credentials but does not prove Linux-role isolation. App identity is not automatically supervisor-UID identity. UI/Worker must not inherit the header, tokens or collector descriptors, inspect another role's environment, or reach acquisition through an exposed request path. Startup ownership and endpoint access must be incorporated into Q-ID. This review neither enables identity nor verifies that the existing app has one.

A pre-existing user-assigned identity is an alternative only if its broader attachments/permissions are acceptable. An external authenticated relay would add a service/trust/lifecycle boundary and remains outside the current scope. All options still face the timestamp blocker. Least privilege is therefore a conditional permission proposal, not a working quota adapter or authorization to create a role.

## Closed findings, unresolved decisions and next boundary

Documentary findings are closed: same-UID credentials alone are insufficient; App Service persistence does not establish this journal transaction; List Usages lacks documented observation time; usage-read authorization and managed identity are available concepts. Target kernel facilities, protected storage semantics, source freshness and credential isolation remain unproven.

The next review should resolve the complete broker/Worker boundary argument, obtain backend-specific supported guarantees, and obtain authoritative quota timestamp/lag semantics or decide whether to propose an explicit contract amendment. Those evidence requests can be drafted without active testing. Harness development, local failure/identity probes, Azure diagnostics, role changes and production integration each remain subject to their own explicit authority. No acceptance gate is marked passed by this document.

# .NET ↔ Python Interoperability Boundary

## Status and decision

Release 1.8 WP09 selects one mechanism: a **local, one-shot, out-of-process
Python capability process**, invoked by Infrastructure through argument-safe
.NET process APIs and a versioned JSON-over-stdio contract.

WP10 implements this boundary with the .NET BCL process/JSON facilities and
the Python standard library; it introduces no .NET package, Python package,
service, broker, or embedded runtime. WP11 permanently verifies the boundary
offline. The implemented endpoint is neutral (`health` and `echo` only), so it
is foundation behavior rather than Release 1.9 ML functionality.

## Context and criteria

The platform preserves `Domain → none`, `Application → Domain`,
`Infrastructure → Application`, and `Worker → Application, Infrastructure`.
Python 3.13.15 with the governed project `.venv` is a first-class platform
runtime, but Python mechanics must not enter Domain contracts or Application
technology ownership. The selected mechanism is assessed for isolation,
deterministic interpreter selection, failure containment, cancellation,
observability, security, Windows development, future Linux/container support,
testability, dependency footprint, and evolution cost.

## Alternatives evaluated

| Mechanism | Result | Rationale |
| --- | --- | --- |
| Local one-shot process with JSON-over-stdio | Selected | Isolates interpreter/native failures, permits deterministic repository-relative `.venv` resolution, has explicit lifecycle/exit semantics, needs no new library, and remains portable. |
| Embedded Python/runtime hosting | Rejected | Adds native/GIL/runtime-lifecycle coupling and a new foundational interoperability library; raises failure blast radius and deployment/testing complexity. |
| Local HTTP/service boundary | Rejected for now | Clarifies a network contract but adds ports, service lifecycle, local security, operational configuration, and likely framework dependencies before evidence requires them. |

The accepted trade-off is startup and serialization overhead per invocation.
No persistent worker is created: revisit only if measured, representative
workload evidence shows startup overhead materially violates a product
requirement, or deployment/scale requirements justify a separately governed
service boundary.

## Layer and ownership boundary

- **Domain** remains independent of Python, processes, JSON, filesystem
  discovery, ML libraries, and Streamlit.
- **Application** owns the technology-neutral capability port and
  request/result abstractions. It does not own executable paths, `Process`
  mechanics, OS launch details, or concrete JSON transport.
- **Infrastructure** owns interpreter/entrypoint resolution, invocation,
  JSON serialization, streams, timeout/cancellation, exit handling, cleanup,
  failure translation, and boundary telemetry.
- **Worker/composition root** continues to own validated configuration and DI
  wiring only; it does not manually construct the integration adapter. The
  delivered adapter is not a new Worker execution mode or product flow.
- Future production Python entrypoints are separate from `python/validation/`.
  WP08 scripts are scientific-stack evidence and are not protocol endpoints.
  The delivered production endpoint is `python/integration/protocol_endpoint.py`.

## Interpreter resolution and process lifecycle

The delivered Infrastructure adapter resolves the repository root from its
configured application context, then resolves the validated project-relative
interpreter: `./.venv/Scripts/python.exe` on Windows and the platform-equivalent
`.venv/bin/python` on non-Windows hosts. It must resolve a real executable,
  reject paths outside the expected repository environment, and report the
  bounded `ConfigurationUnavailable` failure category. Bare
`python`, user-profile paths, PATH mutation, and machine-global scientific
packages are not valid execution targets. Machine CPython is bootstrap/base
runtime only.

The adapter owns only processes it creates. It uses a deterministic working
directory, the allow-listed production endpoint, minimal inherited environment,
redirected stdin/stdout/stderr, and no shell command construction. Normal
success requires one valid response and expected exit code `0`. The adapter
closes streams, awaits process completion, and terminates only its own child on
timeout or cancellation; it never broadly kills Python or VS Code/Jedi
processes. No temporary files are required by the protocol.

## Technology-neutral contract and serialization

Protocol version `1` is independent of SQLite schema version. UTF-8 JSON is
the initial wire format, one bounded request and one bounded response per
one-shot process invocation. Numbers use JSON numeric values with invariant
culture handling; timestamps, if later required, use ISO-8601 UTC strings;
optional fields are explicitly nullable/omittable by contract.

```json
{
  "contractVersion": 1,
  "operation": "validation-oriented-capability",
  "correlationId": "caller-supplied-safe-id",
  "payload": { "bounded": "input" },
  "metadata": { "optional": "bounded" }
}
```

Success responses contain `contractVersion`, `status: "success"`, a structured
`result`, and safe bounded diagnostics. Failure responses contain
`contractVersion`, `status: "failure"`, a stable `code`, safe message,
`retryable` only when existing platform resilience policy supports it, and the
correlation identifier. They never emit credentials, environment secrets, or
unbounded raw payloads.

Stdout is reserved exclusively for the single structured protocol response.
Python diagnostics go to stderr; Infrastructure captures them only for safe,
bounded diagnostic handling and never allows them to corrupt the protocol.
Malformed JSON, multiple protocol messages, unknown/missing required fields,
and unknown contract versions are boundary failures. Additive compatible fields
retain version 1 semantics; a breaking field/meaning change requires a new
contract version, and an unknown version fails explicitly without execution.

## Timeout, cancellation, and failures

Caller cancellation is propagated to the adapter. A bounded configured timeout
also applies. Caller cancellation takes precedence when already signaled;
otherwise timeout produces a timeout category. Either condition causes the
adapter to terminate and await only the process it created, then classify the
result without leaking process details to higher layers.

Infrastructure translates these boundary conditions into the platform's
existing bounded failure style rather than exposing Python exceptions:

| Boundary condition | Stable boundary category | Retry posture |
| --- | --- | --- |
| Missing interpreter, invalid environment, missing entrypoint, malformed request, unsupported version | Configuration/validation failure | Not retryable until corrected |
| Launch failure, malformed response, serialization failure, non-zero exit, unexpected termination | Dependency/integration failure | Case-specific; not automatically retryable |
| Python structured failure | Mapped safe failure code | Defined by that code and existing policy |
| Timeout | Timeout | Not automatically retried |
| Caller cancellation | Cancelled | Never retried by the adapter |

The delivered Application vocabulary is `CapabilityInvocationFailure`:
`ConfigurationUnavailable`, `InvalidRequest`, `UnsupportedContractVersion`,
`MalformedResponse`, `DependencyFailure`, `Timeout`, and `Cancelled`.
Infrastructure maps protocol and process conditions to that vocabulary rather
than exposing Python exceptions or creating a parallel taxonomy.

## Observability and security

Boundary telemetry follows existing correlation/logging guidance: operation,
contract version, safe correlation identifier, duration, outcome, failure
category, process exit code, and timeout/cancellation indicator. It excludes
credentials, secret variables, interpreter arguments containing secrets, and
unrestricted payloads.

The local boundary is offline by default. It validates executable and script
paths, uses argument lists rather than shell strings, accepts no caller-selected
interpreter or arbitrary script, bounds input/output, uses deterministic
working directory/environment, and places no credentials on the command line.
Remote execution, ports, HTTP, and provider calls are outside this decision.

## Testing and portability

WP11 keeps layers distinct: Application unit tests cover the technology-neutral
contracts; Infrastructure tests exercise JSON/process contracts,
launch/failure/timeout/cancellation/concurrent-I/O/cleanup paths with
repository-owned fixtures; and the protocol endpoint is exercised through the
adapter. The interoperability subset passed three consecutive runs. WP08
validation scripts remain separate and are not integration tests.

The abstraction supports Windows development now and later Linux, containers,
CI, or a separately packaged runtime by resolving the platform-specific
repository-relative interpreter rather than committing absolute paths. Containers,
packaging, persistent workers, service deployment, and product ML behavior are
not implemented or selected here.

## Reconsideration triggers

Re-evaluate this choice only for measured startup-performance inadequacy,
required concurrency/throughput that a one-shot process cannot meet,
security/isolation requirements needing a stronger boundary, portable-runtime
packaging evidence, or an implementation finding that the standard-library
approach cannot meet the governed contract. Any new framework, embedded
runtime, service transport, or package requires separate foundational selection
authority before introduction.

"""Static obligations only: no execution, native calls, credentials or attestation."""
from dataclasses import dataclass

MAX_TIME = (1 << 63) - 1
PUBLIC_RUNTIME_VERIFIED = False
HARDENING = frozenset({"real_effective_saved_ids", "empty_groups", "zero_capabilities",
                      "permanent_drop", "no_new_privileges", "session_isolation"})


class PlanError(ValueError):
    """Invalid static launch obligations; no runtime verdict is produced."""


def _number(value, low=0):
    if type(value) is not int or not low <= value <= MAX_TIME:
        raise PlanError("Invalid integer or overflow.")


@dataclass(frozen=True)
class Role:
    name: str
    process: str
    parent: str | None
    uid: int
    gid: int
    session: str
    hardening: frozenset[str] = HARDENING


@dataclass(frozen=True)
class Descriptor:
    process: str
    channel: str
    fd: int | None
    lifetime: tuple[str, ...]


@dataclass(frozen=True)
class WaitObligation:
    owner: str
    child: str
    evidence: str = "actual_positive_bound_kernel_wait"
    normal_exit: bool = True
    exit_code: int = 0


PHASES = ("pre_fork", "bootstrap", "post_hardening", "pre_exec", "post_exec",
          "metadata_validation", "ui_spawn", "ui_child_bootstrap", "ui_exec")


@dataclass(frozen=True)
class Endpoint:
    """Logical endpoint/alias obligations, not a live FD observation."""
    identity: str
    owner: str
    channel: str
    fd: int | None
    live_phases: tuple[str, ...]
    alias_of: str | None = None
    cloexec: bool = False
    explicit_close: str | None = None
    validations: tuple[str, ...] = ()


@dataclass(frozen=True)
class Boundary:
    name: str
    parent: str
    child: str
    retained: tuple[str, ...]
    close_unused: bool = True
    reject_unlisted_aliases: bool = True


@dataclass(frozen=True)
class Phase:
    name: str
    ownership: tuple[tuple[str, str], ...]


def _endpoint_specs():
    """Non-temporal endpoint policy; phase membership is projected from trace."""
    runtime = metadata = gate = ()
    return (
        Endpoint("S-SR", "S", "SR", 3, runtime),
        Endpoint("S-SG", "S", "SG", 4, runtime),
        Endpoint("S-meta", "S", "S_metadata", 5, (), explicit_close="before_runtime", validations=("bootstrap_once",)),
        Endpoint("G-SG", "G", "SG", 3, runtime),
        Endpoint("G-gate", "G", "launch_gate", 4, gate, cloexec=True, explicit_close="after_ACK_before_agent_exec"),
        Endpoint("G-meta", "G", "G_metadata", 5, (), explicit_close="before_runtime", validations=("bootstrap_once",)),
        Endpoint("R-key", "R", "credential", 3, (), explicit_close="before_listener"),
        Endpoint("R-RA", "R", "RA", 4, runtime),
        Endpoint("R-SR", "R", "SR", 5, runtime),
        Endpoint("R-meta", "R", "R_metadata", 6, (), explicit_close="before_runtime", validations=("bootstrap_once",)),
        Endpoint("A-gate", "A", "launch_gate", None, gate, cloexec=True, explicit_close="after_ACK_before_agent_exec"),
        # Symbolic duplicate obligation; no numeric allocation or actual dup.
        Endpoint("A-gate-alias", "A", "launch_gate", None, gate, alias_of="A-gate", cloexec=True,
                 explicit_close="after_ACK_before_agent_exec"),
        Endpoint("A-RA", "A", "RA", 3, runtime),
        Endpoint("A-meta", "A", "A_metadata", 4, metadata, explicit_close="after_validation_before_A_BIND",
                 validations=("executed_agent_once",)))


def boundary_obligations():
    return (Boundary("root_fork_router", "root", "R", ("R-key", "R-RA", "R-SR", "R-meta")),
            Boundary("root_fork_guardian", "root", "G", ("G-SG", "G-gate", "G-meta", "A-gate", "A-gate-alias", "A-RA", "A-meta")),
            Boundary("root_becomes_supervisor", "root", "S", ("S-SR", "S-SG", "S-meta")),
            Boundary("guardian_fork_agent", "G", "A", ("A-gate", "A-gate-alias", "A-RA", "A-meta")),
            Boundary("agent_exec", "A", "A", ("A-RA", "A-meta")),
            Boundary("agent_spawn_ui", "A", "UI", ()))


@dataclass(frozen=True)
class PidBinding:
    """Fork-return positivity obligation, not a numeric PID or kernel observation."""
    role: str
    parent: str
    source: str = "positive_fork_return"
    positive: bool = True


@dataclass(frozen=True)
class Step:
    operation: str
    owner: str
    items: tuple[str, ...] = ()
    child: str | None = None
    fd: int | None = None
    binding: PidBinding | None = None
    message: str | None = None


def handoff_trace():
    """Candidate logical handoff; scratch FD numbers and native operations absent.

    Root creates empty endpoints, not credentials. Guardian bootstrap forks the
    agent while still in the finite root pre-fork phase, before permanent drop.
    Metadata writers represent pipe ends, and close before corresponding EOF.
    All stages are obligations, including kernel-wait labels, never observations.
    """
    endpoints = tuple(e.identity for e in _endpoint_specs())
    writers = ("S-meta-writer", "G-meta-writer", "R-meta-writer", "A-meta-writer", "R-key-writer")
    router = ("R-key", "R-RA", "R-SR", "R-meta", "R-key-writer")
    guardian = ("G-SG", "G-gate", "G-meta", "A-gate", "A-gate-alias", "A-RA", "A-meta", "A-meta-writer")
    agent = ("A-gate", "A-gate-alias", "A-RA", "A-meta")
    all_ends = endpoints + writers
    guardian += ("G-meta-writer",)
    steps = [Step("create_empty", "root", tuple(e for e in all_ends if e != "A-gate-alias")),
             Step("duplicate_endpoint", "root", ("A-gate", "A-gate-alias")),
             Step("prepare_metadata_templates", "root", writers[:4]),
             Step("fork", "root", router, "R", binding=PidBinding("R", "root")),
             Step("close_unused", "R", tuple(e for e in all_ends if e not in router)),
             Step("fork", "root", guardian, "G", binding=PidBinding("G", "root")),
             Step("close_unused", "G", tuple(e for e in all_ends if e not in guardian))]
    for writer in writers[:4]:
        steps.append(Step("emit_metadata", "root", (writer,), message="META_ROUTER"))
    steps += [Step("emit_metadata", "root", ("S-meta-writer",), message="META_GUARDIAN"),
              Step("close", "root", writers[:4]),
              Step("close_unused", "root", tuple(e for e in endpoints + ("R-key-writer",) if e not in ("S-SR", "S-SG", "S-meta"))),
              Step("become_supervisor", "root", ("S-SR", "S-SG", "S-meta"), "S"),
              Step("assign", "G", ("G-meta",), fd=5),
              Step("fork", "G", agent, "A", binding=PidBinding("A", "G")),
              Step("close_unused", "A", tuple(e for e in guardian if e not in agent)),
              Step("emit_metadata", "G", ("G-meta-writer",), message="META_AGENT"),
              Step("emit_metadata", "G", ("A-meta-writer",), message="META_AGENT"),
              Step("close", "G", ("G-meta-writer", "A-meta-writer")),
              Step("validate_metadata", "G", ("G-meta",)), Step("close", "G", ("G-meta",)),
              Step("close_unused", "G", agent)]
    for owner in ("S", "G", "R", "A"):
        for e in _endpoint_specs():
            if e.owner == owner and e.fd is not None and e.identity != "G-meta":
                steps.append(Step("assign", owner, (e.identity,), fd=e.fd))
    steps += [Step("require_free_slot", "A", fd=5), Step("assign", "A", ("A-gate",), fd=5),
              Step("permanent_hardening", "all"),
              Step("validate_metadata", "S", ("S-meta",)), Step("close", "S", ("S-meta",)),
              Step("validate_metadata", "R", ("R-meta",)), Step("close", "R", ("R-meta",)),
              Step("G_BOUND", "G"), Step("R_PREPARE", "S"),
              Step("router_local_synthetic_credential", "R", ("R-key-writer",)),
              Step("close", "R", ("R-key-writer",)), Step("read_credential", "R", ("R-key",)),
              Step("close", "R", ("R-key",)), Step("R_BOUND", "R"), Step("G_RELEASE", "S"),
              Step("A_EXEC", "G"), Step("A_EXEC_ACK", "A"),
              Step("close", "G", ("G-gate",)), Step("close", "A", ("A-gate", "A-gate-alias")),
              Step("exec", "A", ("A-RA", "A-meta")),
              Step("validate_metadata", "A", ("A-meta",)), Step("close", "A", ("A-meta",)),
              Step("A_BIND", "R"), Step("A_READY", "A"), Step("START_UI", "R"),
              Step("fork", "A", (), "UI", binding=PidBinding("UI", "A")),
              Step("close_unused", "UI", ("A-RA",)), Step("ui_exec", "UI"),
              Step("UI_READY", "A"), Step("R_UI_READY", "R"),
              Step("R_EXPOSE", "S"), Step("public_listener", "R"), Step("R_EXPOSED", "R"),
              Step("STOP_ROUTER", "S"), Step("STOP_GUARDIAN", "S"), Step("STOP_UI", "R"),
              Step("UI_STOPPED", "A"), Step("close", "A", ("A-RA",)),
              Step("agent_to_router_eof_once", "R"), Step("close", "R", ("R-RA",)),
              Step("R_PROTOCOL_DONE", "R"), Step("HOST_DISPOSED", "R"),
              Step("close", "R", ("R-SR",)), Step("close", "S", ("S-SR",)),
              Step("guardian_wait_agent", "G"), Step("AGENT_RESULT", "G"),
              Step("close", "G", ("G-SG",)), Step("supervisor_wait_guardian", "S"),
              Step("close", "S", ("S-SG",)), Step("supervisor_wait_router", "S"),
              Step("adopted_child_drain", "S"), Step("terminal_absence", "S")]
    return tuple(steps)


def validate_trace(trace, *, _require_canonical=True):
    """Derive immutable ownership snapshots from logical actions only."""
    if type(trace) is not tuple or any(type(s) is not Step for s in trace):
        raise PlanError("Invalid trace.")
    state, validated, snapshots = {}, set(), []
    bindings, pending_unused, emitted, milestones = {}, {}, {}, set()
    parents = {"R": "root", "G": "root", "A": "G", "UI": "A"}
    streams = {"S-meta": ("META_ROUTER", "META_GUARDIAN"), "R-meta": ("META_ROUTER",),
               "G-meta": ("META_ROUTER", "META_AGENT"), "A-meta": ("META_ROUTER", "META_AGENT")}
    for s in trace:
        if type(s.items) is not tuple or len(set(s.items)) != len(s.items):
            raise PlanError("Ambiguous endpoint list.")
        if s.operation == "create_empty":
            if state or s.owner != "root": raise PlanError("Duplicate creation.")
            state["root"] = dict.fromkeys(s.items)
        elif s.operation == "duplicate_endpoint":
            owned = state.get(s.owner, {})
            if len(s.items) != 2 or s.items[0] not in owned or s.items[1] in owned:
                raise PlanError("ALIAS_SOURCE_INVALID")
            # A new symbolic scratch slot, not the source's process-local number.
            owned[s.items[1]] = None
        elif s.operation in ("fork", "become_supervisor"):
            parent = state.get(s.owner)
            if parent is None or s.child in state or any(item not in parent for item in s.items):
                raise PlanError("PARENT_ENDPOINT_UNAVAILABLE")
            if s.operation == "fork":
                if parents.get(s.child) != s.owner: raise PlanError("WRONG_FORK_PARENT")
                if (type(s.binding) is not PidBinding or s.binding.role != s.child or s.binding.parent != s.owner
                        or s.binding.source != "positive_fork_return" or s.binding.positive is not True):
                    raise PlanError("PID_BINDING_INVALID")
                bindings[s.child] = s.binding
                state[s.child] = dict(parent)  # Every inherited copy remains visible.
                pending_unused[s.child] = set(parent) - set(s.items)
            else:
                if s.owner != "root" or s.child != "S" or set(parent) != set(s.items):
                    raise PlanError("INHERITED_COPY_NOT_CLOSED")
                state[s.child] = dict(parent); del state[s.owner]
        elif s.operation == "close_unused":
            owned = state.get(s.owner, {})
            if any(item not in owned for item in s.items): raise PlanError("PARENT_ENDPOINT_UNAVAILABLE")
            required = {e.identity for e in _endpoint_specs() if e.owner == s.owner}
            if s.owner == "root": required = {"S-SR", "S-SG", "S-meta"}
            if any(item in required for item in s.items): raise PlanError("REQUIRED_ENDPOINT_CLOSED")
            if s.owner in pending_unused and set(s.items) != pending_unused[s.owner]:
                raise PlanError("INHERITED_COPY_NOT_CLOSED")
            for item in s.items: del owned[item]
            pending_unused.pop(s.owner, None)
        elif s.operation == "assign":
            _number(s.fd, 3)
            owned = state.get(s.owner, {})
            if len(s.items) != 1 or s.items[0] not in owned:
                raise PlanError("Missing assignment source.")
            if any(fd == s.fd and item != s.items[0] for item, fd in owned.items()):
                raise PlanError("FD_SLOT_OCCUPIED")
            if pending_unused.get(s.owner): raise PlanError("INHERITED_COPY_NOT_CLOSED")
            owned[s.items[0]] = s.fd
        elif s.operation == "require_free_slot":
            _number(s.fd, 3)
            if s.fd in state.get(s.owner, {}).values(): raise PlanError("FD5_OCCUPIED")
            if pending_unused.get(s.owner): raise PlanError("INHERITED_COPY_NOT_CLOSED")
        elif s.operation == "emit_metadata":
            subject = {"META_ROUTER": "R", "META_GUARDIAN": "G", "META_AGENT": "A"}.get(s.message)
            if subject not in bindings: raise PlanError("METADATA_BINDING_MISSING")
            if (s.message == "META_AGENT" and s.owner != "G") or (s.message != "META_AGENT" and s.owner != "root"):
                raise PlanError("METADATA_SENDER_INVALID")
            if len(s.items) != 1 or s.items[0] not in state.get(s.owner, {}):
                raise PlanError("PARENT_ENDPOINT_UNAVAILABLE")
            reader = s.items[0].removesuffix("-writer")
            order = streams.get(reader)
            prior = emitted.setdefault(reader, [])
            if order is None or len(prior) >= len(order) or s.message != order[len(prior)]:
                raise PlanError("METADATA_ORDER_INVALID")
            prior.append(s.message)
        elif s.operation in ("close", "validate_metadata", "prepare_metadata_templates", "read_credential", "router_local_synthetic_credential"):
            owned = state.get(s.owner, {})
            if any(item not in owned for item in s.items): raise PlanError("Unavailable endpoint.")
            if s.operation == "close":
                gates = {"G-gate", "A-gate", "A-gate-alias"}
                if any(item in gates for item in s.items) and "A_EXEC_ACK" not in milestones:
                    raise PlanError("GATE_CLOSE_BEFORE_ACK")
                prerequisites = {"A-RA": "UI_STOPPED", "R-RA": "agent_to_router_eof_once",
                                 "R-SR": "HOST_DISPOSED", "S-SR": "HOST_DISPOSED",
                                 "G-SG": "AGENT_RESULT", "S-SG": "supervisor_wait_guardian"}
                for item in s.items:
                    if item in streams and (s.owner, item) not in validated:
                        raise PlanError("METADATA_CLOSE_BEFORE_VALIDATION")
                    if item in prerequisites and prerequisites[item] not in milestones:
                        raise PlanError("CONTROL_CLOSE_BEFORE_MILESTONE")
                for item in s.items: del owned[item]
            elif s.operation == "validate_metadata":
                for item in s.items:
                    required_roles = ("R", "G") if item == "S-meta" else ("R",) if item == "R-meta" else ("R", "A", "G")
                    if any(role not in bindings for role in required_roles): raise PlanError("METADATA_BINDING_MISSING")
                    if tuple(emitted.get(item, ())) != streams.get(item): raise PlanError("METADATA_NOT_EMITTED")
                    key = (s.owner, item)
                    if key in validated: raise PlanError("Repeated metadata validation.")
                    validated.add(key)
        elif s.operation == "exec":
            if pending_unused.get(s.owner): raise PlanError("INHERITED_COPY_NOT_CLOSED")
            if set(state.get(s.owner, {})) != set(s.items): raise PlanError("Alias escaped exec.")
        elif s.operation == "ui_exec":
            if s.owner != "UI" or "UI" not in state or "UI" not in bindings:
                raise PlanError("UI_EXEC_WITHOUT_CHILD_BOOTSTRAP")
            if state["UI"] or s.items or pending_unused.get("UI"):
                raise PlanError("UI_EXEC_ENDPOINT_LEAK")
            if "ui_exec" in milestones: raise PlanError("UI_EXEC_REPEATED")
        elif s.operation == "UI_READY":
            if "ui_exec" not in milestones: raise PlanError("UI_READY_BEFORE_EXEC")
        elif s.operation == "A_READY":
            if "exec" not in milestones or ("A", "A-meta") not in validated or "A-meta" in state.get("A", {}):
                raise PlanError("AGENT_NOT_READY")
        elif s.operation == "R_EXPOSE":
            if "R_UI_READY" not in milestones: raise PlanError("EXPOSURE_BEFORE_READINESS")
        elif s.operation == "public_listener":
            if "R_EXPOSE" not in milestones: raise PlanError("LISTENER_BEFORE_EXPOSURE")
        elif s.operation == "adopted_child_drain":
            if not {"guardian_wait_agent", "supervisor_wait_guardian", "supervisor_wait_router"} <= milestones:
                raise PlanError("DRAIN_BEFORE_BOUND_WAITS")
        elif s.operation == "terminal_absence":
            if "adopted_child_drain" not in milestones: raise PlanError("ABSENCE_BEFORE_DRAIN")
            if any(state.values()): raise PlanError("Shutdown endpoint leak.")
        milestones.add(s.operation)
        snapshots.append(tuple((owner, tuple(sorted(owned.items()))) for owner, owned in sorted(state.items())))
    if _require_canonical and trace != handoff_trace(): raise PlanError("Incorrect creation, closure, readiness or proof order.")
    return tuple(snapshots)


def project_phases(trace):
    """Exact checkpoint ownership derived from semantic transitions, not a table."""
    snapshots = validate_trace(trace, _require_canonical=False)
    selectors = {
        "pre_fork": ("duplicate_endpoint", "root", None, False),
        "bootstrap": ("assign", "G", ("G-meta",), False),
        "post_hardening": ("permanent_hardening", "all", None, False),
        "pre_exec": ("exec", "A", None, True),
        "post_exec": ("exec", "A", None, False),
        "metadata_validation": ("validate_metadata", "A", None, False),
        "ui_spawn": ("ui_exec", "UI", None, False),
        "ui_child_bootstrap": ("fork", "A", None, False),
        "ui_exec": ("ui_exec", "UI", None, False),
    }
    phases = []
    for name in PHASES:
        operation, owner, items, before = selectors[name]
        matches = [i for i, step in enumerate(trace) if step.operation == operation and step.owner == owner
                   and (items is None or step.items == items)]
        if len(matches) != 1 or (before and matches[0] == 0):
            raise PlanError("PHASE_CHECKPOINT_INVALID")
        snapshot = snapshots[matches[0] - int(before)]
        phases.append(Phase(name, tuple((identity, process) for process, owned in snapshot for identity, fd in owned)))
    return tuple(phases)


def phase_obligations():
    return project_phases(handoff_trace())


def endpoint_obligations():
    phases = phase_obligations()
    return tuple(Endpoint(e.identity, e.owner, e.channel, e.fd,
                          tuple(phase.name for phase in phases if (e.identity, e.owner) in phase.ownership),
                          e.alias_of, e.cloexec, e.explicit_close, e.validations) for e in _endpoint_specs())


STARTUP = ("secret_free_root_prefork", "permanent_role_hardening", "G_BOUND",
           "R_PREPARE", "router_local_synthetic_credential", "R_BOUND", "G_RELEASE",
           "A_EXEC", "A_EXEC_ACK", "close_bootstrap_gate", "agent_exec",
           "validate_agent_metadata_once", "close_agent_metadata", "A_BIND", "A_READY",
           "START_UI", "UI_READY", "R_UI_READY", "R_EXPOSE", "public_listener", "R_EXPOSED")
SHUTDOWN = ("shared_shutdown_deadline", "STOP_ROUTER", "STOP_GUARDIAN", "STOP_UI",
            "UI_STOPPED", "agent_to_router_eof_once", "R_PROTOCOL_DONE", "HOST_DISPOSED",
            "guardian_wait_agent", "AGENT_RESULT", "supervisor_wait_guardian",
            "supervisor_wait_router", "adopted_child_drain", "terminal_absence")


@dataclass(frozen=True)
class Plan:
    roles: tuple[Role, ...]
    descriptors: tuple[Descriptor, ...]
    waits: tuple[WaitObligation, ...]
    startup: tuple[str, ...] = STARTUP
    shutdown: tuple[str, ...] = SHUTDOWN
    clock: str = "CLOCK_MONOTONIC"
    startup_origin_ns: int = 0
    privileged_deadline_ns: int = 5_000_000_000
    startup_deadline_ns: int = 30_000_000_000
    shutdown_origin_ns: int = 30_000_000_000
    shutdown_deadline_ns: int = 34_000_000_000
    docker_grace_ns: int = 5_000_000_000
    deadline_resets: tuple[str, ...] = ()
    pid1_role: str = "supervisor"
    root_becomes: str = "supervisor"
    identity_allocation: str = "conditional_future_image_collision_verification"
    credential_policy: str = "router_local_synthetic_after_separation_only"
    privileged_managed_startup: bool = False
    retained_root_helper: bool = False
    cross_uid_signaling: bool = False
    descriptor_passing: bool = False
    eof_policy: str = "agent_to_router_after_UI_STOPPED_once_before_deadline"
    absence_policy: str = "terminal_only_after_all_positive_waits_and_drain"
    teardown_policy: str = "failure_containment_only"
    endpoints: tuple[Endpoint, ...] = endpoint_obligations()
    boundaries: tuple[Boundary, ...] = boundary_obligations()
    phases: tuple[Phase, ...] = phase_obligations()
    trace: tuple[Step, ...] = handoff_trace()


def canonical_plan():
    """Build immutable obligations. Process labels are not observed kernel PIDs."""
    roles = (Role("supervisor", "S", None, 1200, 1200, "trusted-S"),
             Role("router", "R", "S", 1100, 1100, "trusted-R"),
             Role("guardian", "G", "S", 1300, 1300, "trusted-G"),
             Role("agent", "A", "G", 1000, 1000, "presentation-agent"),
             Role("ui", "UI", "A", 1000, 1000, "presentation-ui"))
    descriptors = (
        Descriptor("S", "SR", 3, ("runtime",)), Descriptor("S", "SG", 4, ("runtime",)),
        Descriptor("S", "S_metadata", 5, ("validate_once", "close_before_runtime")),
        Descriptor("G", "SG", 3, ("runtime",)),
        Descriptor("G", "launch_gate", 4, ("A_EXEC", "A_EXEC_ACK", "close_before_agent_exec")),
        Descriptor("G", "G_metadata", 5, ("validate_once", "close_before_runtime")),
        Descriptor("R", "credential", 3, ("read_once", "close_before_listener")),
        Descriptor("R", "RA", 4, ("runtime",)), Descriptor("R", "SR", 5, ("runtime",)),
        Descriptor("R", "R_metadata", 6, ("validate_once", "close_before_runtime")),
        # Bootstrap endpoint number is deliberately unspecified, not allocated.
        Descriptor("A", "launch_gate", None, ("bootstrap_only", "A_EXEC_ACK", "close_before_agent_exec")),
        Descriptor("A", "RA", 3, ("survive_agent_exec", "runtime")),
        Descriptor("A", "A_metadata", 4, ("survive_agent_exec", "validate_once_after_exec", "close_before_A_BIND")))
    waits = (WaitObligation("G", "A"), WaitObligation("S", "G"), WaitObligation("S", "R"))
    return Plan(roles, descriptors, waits)


def validate(plan):
    """Validate static obligations only; never return a runtime proof/verdict."""
    if type(plan) is not Plan:
        raise PlanError("Invalid plan.")
    expected = canonical_plan()
    projected = project_phases(plan.trace)
    if plan.phases != projected:
        raise PlanError("PHASE_TRACE_MISMATCH")
    if type(plan.roles) is not tuple or len(plan.roles) != 5 or any(type(r) is not Role for r in plan.roles):
        raise PlanError("Invalid roles.")
    if len({r.name for r in plan.roles}) != 5 or len({r.process for r in plan.roles}) != 5:
        raise PlanError("Duplicate role or process.")
    for role, required in zip(plan.roles, expected.roles):
        _number(role.uid, 1); _number(role.gid, 1)
        if role != required or type(role.hardening) is not frozenset:
            raise PlanError("Invalid parentage, identity, session or hardening.")
    if type(plan.descriptors) is not tuple or any(type(d) is not Descriptor for d in plan.descriptors):
        raise PlanError("Invalid descriptors.")
    seen = set()
    for d in plan.descriptors:
        if d.fd is not None:
            _number(d.fd, 3)
            key = (d.process, d.fd)
            if key in seen:
                raise PlanError("Per-process descriptor collision.")
            seen.add(key)
    if plan.descriptors != expected.descriptors:
        raise PlanError("Descriptor ownership or lifetime violation.")
    if (type(plan.endpoints) is not tuple or any(type(e) is not Endpoint for e in plan.endpoints)
            or len({e.identity for e in plan.endpoints}) != len(plan.endpoints)):
        raise PlanError("Ambiguous endpoint identity.")
    endpoints = {e.identity: e for e in plan.endpoints}
    occupied = set()
    for e in plan.endpoints:
        expected_phases = tuple(phase.name for phase in projected if (e.identity, e.owner) in phase.ownership)
        if e.live_phases != expected_phases:
            raise PlanError("PHASE_TRACE_MISMATCH")
        if type(e.live_phases) is not tuple or any(phase not in PHASES for phase in e.live_phases):
            raise PlanError("Invalid endpoint phase.")
        if type(e.cloexec) is not bool:
            raise PlanError("Invalid CLOEXEC obligation.")
        if e.alias_of is not None:
            original = endpoints.get(e.alias_of)
            if original is None or original.alias_of is not None or (original.owner, original.channel) != (e.owner, e.channel):
                raise PlanError("Unbound endpoint alias.")
        if e.channel == "launch_gate":
            if (not e.cloexec or e.explicit_close != "after_ACK_before_agent_exec"
                    or any(phase in e.live_phases for phase in PHASES[3:])
                    or (e.owner == "A" and e.fd is not None)):
                raise PlanError("Gate closure, alias, CLOEXEC or allocation violation.")
        if e.fd is not None:
            _number(e.fd, 3)
            for phase in e.live_phases:
                key = (e.owner, phase, e.fd)
                if key in occupied:
                    raise PlanError("Phase descriptor collision.")
                occupied.add(key)
    if plan.endpoints != expected.endpoints:
        raise PlanError("Endpoint lifetime, validation or remapping changed.")
    if (type(plan.boundaries) is not tuple or any(type(b) is not Boundary or b.close_unused is not True
            or b.reject_unlisted_aliases is not True for b in plan.boundaries)
            or plan.boundaries != expected.boundaries):
        raise PlanError("Unused endpoints or fork/exec inheritance violation.")
    if (type(plan.phases) is not tuple or any(type(phase) is not Phase for phase in plan.phases)
            or plan.phases != expected.phases):
        raise PlanError("Ambiguous phase ownership or leaked endpoint.")
    if type(plan.waits) is not tuple or any(type(w) is not WaitObligation for w in plan.waits):
        raise PlanError("Invalid wait obligations.")
    for w in plan.waits:
        if type(w.normal_exit) is not bool or type(w.exit_code) is not int:
            raise PlanError("Invalid exit obligation.")
    if plan.waits != expected.waits:
        raise PlanError("Reports, EOF, ECHILD or injected observations are not kernel waits.")
    if plan.startup != STARTUP or type(plan.startup) is not tuple or plan.shutdown != SHUTDOWN or type(plan.shutdown) is not tuple:
        raise PlanError("Missing, reordered or premature milestone.")
    for value in (plan.startup_origin_ns, plan.privileged_deadline_ns, plan.startup_deadline_ns,
                  plan.shutdown_origin_ns, plan.shutdown_deadline_ns, plan.docker_grace_ns):
        _number(value)
    if (plan.clock != "CLOCK_MONOTONIC" or plan.deadline_resets != ()
            or plan.startup_origin_ns > MAX_TIME - 30_000_000_000
            or plan.shutdown_origin_ns > MAX_TIME - 4_000_000_000
            or plan.privileged_deadline_ns != plan.startup_origin_ns + 5_000_000_000
            or plan.startup_deadline_ns != plan.startup_origin_ns + 30_000_000_000
            or plan.shutdown_deadline_ns != plan.shutdown_origin_ns + 4_000_000_000
            or plan.docker_grace_ns != 5_000_000_000):
        raise PlanError("Clock, deadline or grace violation.")
    for name in ("pid1_role", "root_becomes", "identity_allocation", "credential_policy", "eof_policy",
                 "absence_policy", "teardown_policy"):
        if getattr(plan, name) != getattr(expected, name):
            raise PlanError("Security obligation changed.")
    for name in ("privileged_managed_startup", "retained_root_helper", "cross_uid_signaling", "descriptor_passing"):
        if getattr(plan, name) is not False:
            raise PlanError("Forbidden runtime policy.")
    if plan.trace != handoff_trace():
        raise PlanError("Incorrect creation, closure, readiness or proof order.")
    # None deliberately: successful validation attests no runtime fact.

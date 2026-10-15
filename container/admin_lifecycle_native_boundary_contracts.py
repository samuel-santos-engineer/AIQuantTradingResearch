"""Injected offline contracts only. No syscalls, ABI, wire codec or native proof."""
from dataclasses import dataclass, replace

PUBLIC_RUNTIME_VERIFIED = False
MAX_TIME = (1 << 63) - 1
MAX_PID = (1 << 31) - 1
ROLE_IDS = (("S", 1200), ("G", 1300), ("R", 1100), ("A", 1000), ("UI", 1000))
PARENTS = (("R", "S"), ("G", "S"), ("A", "G"), ("UI", "A"))


class BoundaryError(ValueError):
    """Stable, non-sensitive rejection classification."""


def require(condition, reason):
    if not condition:
        raise BoundaryError(reason)


def integer(value, low=0, high=MAX_TIME):
    require(type(value) is int and low <= value <= high, "INVALID_INTEGER")


def role(value):
    require(type(value) is str and value in ("ROOT", "S", "G", "R", "A", "UI"), "INVALID_ROLE")


def label(value):
    require(type(value) is str and value.isidentifier(), "INVALID_SYMBOL")


def slot(value):
    require(type(value) is str, "INVALID_SLOT")
    if value.startswith("scratch:"):
        label(value[8:])  # An unresolved name, never a real scratch number.
    else:
        require(value in ("fd3", "fd4", "fd5", "fd6", "fd5_candidate"), "INVALID_SLOT")
    return "fd5" if value == "fd5_candidate" else value


@dataclass(frozen=True)
class Deadline:
    start: int
    end: int
    kind: str
    clock: str = "CLOCK_MONOTONIC"

    def __post_init__(self):
        integer(self.start); integer(self.end)
        require(type(self.kind) is str, "INVALID_CLOCK_OR_BUDGET")
        duration = dict(privileged=5_000_000_000, startup=30_000_000_000, shutdown=4_000_000_000).get(self.kind)
        require(duration is not None and self.clock == "CLOCK_MONOTONIC", "INVALID_CLOCK_OR_BUDGET")
        require(self.start <= MAX_TIME - duration, "DEADLINE_OVERFLOW")
        require(self.end == self.start + duration, "DEADLINE_RESET")

    def check(self, now, propagated):
        integer(now); integer(propagated)
        require(propagated == self.end, "DEADLINE_RESET")
        require(self.start <= now < self.end, "DEADLINE_EXPIRED_OR_REGRESSED")


@dataclass(frozen=True)
class ForkBinding:
    creator: str
    child: str
    child_pid: int
    status_owners: tuple[str, ...]
    secret_free: bool = True
    finite_root_phase: bool = True
    privileged_managed: bool = False
    parent_pid: int | None = None

    def __post_init__(self):
        role(self.creator); role(self.child); integer(self.child_pid, 2, MAX_PID)
        parent = dict(PARENTS).get(self.child)
        creator = "ROOT" if self.child in ("R", "G") else parent
        require(self.creator == creator, "WRONG_PARENT")
        require(type(self.status_owners) is tuple and self.status_owners == (parent,), "COMPETING_STATUS_OWNER")
        require(self.secret_free is True and self.privileged_managed is False, "UNSAFE_PREFORK")
        require(type(self.finite_root_phase) is bool, "UNBOUNDED_ROOT_PHASE")
        require(self.finite_root_phase is True or self.child == "UI", "UNBOUNDED_ROOT_PHASE")
        integer(self.parent_pid, 1, MAX_PID)
        require(self.parent_pid != self.child_pid, "PARENT_CHILD_PID_COLLISION")
        require(self.creator != "ROOT" or self.parent_pid == 1, "WRONG_PARENT_PID")


@dataclass(frozen=True)
class Hardening:
    owner: str
    uids: tuple[int, int, int]
    gids: tuple[int, int, int]
    session: str
    groups: tuple[int, ...] = ()
    capabilities: frozenset[str] = frozenset()
    permanent: bool = True
    no_new_privileges: bool = True
    cross_uid_signals: bool = False

    def __post_init__(self):
        role(self.owner)
        target = dict(ROLE_IDS).get(self.owner)
        require(target is not None, "INVALID_ROLE")
        for ids in (self.uids, self.gids):
            require(type(ids) is tuple and len(ids) == 3, "PARTIAL_IDENTITY_DROP")
            for value in ids: integer(value, 0, MAX_PID)
            require(ids == (target,) * 3, "PARTIAL_IDENTITY_DROP")
        label(self.session)
        require(type(self.groups) is tuple and not self.groups, "SUPPLEMENTARY_GROUPS")
        require(type(self.capabilities) is frozenset and not self.capabilities, "CAPABILITIES_RETAINED")
        require(self.permanent is True and self.no_new_privileges is True, "INCOMPLETE_HARDENING")
        require(self.cross_uid_signals is False, "CROSS_UID_SIGNAL_DEPENDENCY")


def validate_sessions(observations):
    require(type(observations) is tuple and len(observations) == 5
            and all(type(o) is Hardening for o in observations), "INCOMPLETE_ROLE_SET")
    require({o.owner for o in observations} == {r for r, uid in ROLE_IDS}, "INCOMPLETE_ROLE_SET")
    require(len({o.session for o in observations}) == 5, "SHARED_SESSION")


@dataclass(frozen=True)
class Handle:
    endpoint: str
    creator: str
    owner: str
    position: str
    channel: str
    cloexec: bool
    alias_of: str | None = None
    required: bool = True

    def __post_init__(self):
        label(self.endpoint); role(self.creator); role(self.owner); slot(self.position)
        require(self.channel in ("SR", "SG", "RA", "metadata", "credential", "gate"), "INVALID_CHANNEL")
        require(type(self.cloexec) is bool and type(self.required) is bool, "INVALID_BOOLEAN")
        if self.alias_of is not None: label(self.alias_of)


def validate_inventory(handles):
    require(type(handles) is tuple and all(type(h) is Handle for h in handles), "INVALID_INVENTORY")
    require(len({h.endpoint for h in handles}) == len(handles), "DUPLICATE_ENDPOINT")
    require(len({(h.owner, slot(h.position)) for h in handles}) == len(handles), "SLOT_COLLISION")
    owners = {h.owner for h in handles}
    require(len(owners) <= 1, "MIXED_INVENTORY_OWNER")
    by_id = {h.endpoint: h for h in handles}
    for h in handles:
        if h.alias_of is not None:
            original = by_id.get(h.alias_of)
            require(original is not None and original.alias_of is None
                    and (original.creator, original.channel) == (h.creator, h.channel), "UNBOUND_ALIAS")


def inherit(handles, binding, retained):
    validate_inventory(handles)
    require(type(binding) is ForkBinding and type(retained) is frozenset, "INVALID_HANDOFF")
    require(all(h.owner == binding.creator for h in handles), "PARENT_ENDPOINT_UNAVAILABLE")
    require(retained <= {h.endpoint for h in handles}, "PARENT_ENDPOINT_UNAVAILABLE")
    # Full copied inventory; required marks the child's intended survivors only.
    return tuple(replace(h, owner=binding.child, required=h.endpoint in retained) for h in handles)


@dataclass(frozen=True)
class Move:
    source: str
    destination: str | None = None
    kind: str = "move"


def remap(handles, operations):
    """Sequential symbolic plan; cycles must first evacuate into named scratch."""
    validate_inventory(handles)
    require(type(operations) is tuple and all(type(o) is Move for o in operations), "INVALID_REMAP")
    current = list(handles)
    for operation in operations:
        source = slot(operation.source)
        matches = [i for i, h in enumerate(current) if slot(h.position) == source]
        require(len(matches) == 1, "SOURCE_SLOT_UNAVAILABLE")
        index = matches[0]; handle = current[index]
        if operation.kind == "close_unused":
            require(operation.destination is None and not handle.required, "LIVE_ENDPOINT_CLOSE")
            current.pop(index)
        else:
            require(operation.kind == "move" and operation.destination is not None, "INVALID_REMAP")
            destination = slot(operation.destination)
            require(destination != source, "NOOP_REMAP")
            require(all(slot(h.position) != destination for h in current), "LIVE_DESTINATION")
            current[index] = replace(handle, position=operation.destination)
    validate_inventory(tuple(current))
    return tuple(current)


@dataclass(frozen=True)
class GateHistory:
    """Mandatory synthetic pre-exec history, independent of current FD inventory."""
    endpoints: tuple[Handle, ...]
    explicitly_closed: frozenset[str]
    messages: tuple[str, ...] = ("A_EXEC", "A_EXEC_ACK", "close_all_gate_endpoints")
    guardian_role: str = "04"
    ack_role: str = "07"

    def validate(self):
        validate_inventory(self.endpoints)
        require(self.endpoints and all(h.owner == "A" and h.channel == "gate" for h in self.endpoints), "GATE_PROVENANCE_INVALID")
        bases = [h for h in self.endpoints if h.alias_of is None]
        require(len(bases) == 1 and slot(bases[0].position) == "fd5", "GATE_PROVENANCE_INVALID")
        require(self.messages == ("A_EXEC", "A_EXEC_ACK", "close_all_gate_endpoints")
                and type(self.messages) is tuple and self.guardian_role == "04" and self.ack_role == "07", "GATE_PROTOCOL_HISTORY_INVALID")
        require(all(h.cloexec for h in self.endpoints), "GATE_CLOEXEC_MISSING")
        require(type(self.explicitly_closed) is frozenset
                and self.explicitly_closed == frozenset(h.endpoint for h in self.endpoints), "GATE_EXPLICIT_CLOSE_REQUIRED")


@dataclass(frozen=True)
class ExecBoundary:
    owner: str
    inherited: tuple[Handle, ...]
    survivors: tuple[Handle, ...]
    explicitly_closed: frozenset[str]
    gate_ack: bool = False
    sanitized_environment: bool = True
    gate_history: GateHistory | None = None

    def validate(self):
        require(self.owner in ("A", "UI"), "EXEC_ROLE_NOT_MODELED")
        validate_inventory(self.inherited); validate_inventory(self.survivors)
        require(all(h.owner == self.owner for h in self.inherited + self.survivors), "WRONG_EXEC_OWNER")
        require(type(self.explicitly_closed) is frozenset, "INVALID_CLOSURE_SET")
        original = {h.endpoint: h for h in self.inherited}
        kept = {h.endpoint: h for h in self.survivors}
        historical = {}
        if self.owner == "A":
            require(type(self.gate_history) is GateHistory, "GATE_HISTORY_MISSING")
            require(self.gate_ack is True, "GATE_EXPLICIT_CLOSE_REQUIRED")
            self.gate_history.validate()
            historical = {h.endpoint: h for h in self.gate_history.endpoints}
        require(not (set(kept) & self.explicitly_closed)
                and set(original) | set(historical) == set(kept) | self.explicitly_closed, "UNCLOSED_INHERITED_COPY")
        require(all(original.get(k) == h for k, h in kept.items()), "EXEC_REMAP")
        require(self.sanitized_environment is True, "UNSANITIZED_EXEC")
        for h in self.inherited:
            if h.channel == "gate":
                require(h.cloexec, "GATE_CLOEXEC_MISSING")
                require(self.gate_ack is True and h.endpoint in self.explicitly_closed, "GATE_EXPLICIT_CLOSE_REQUIRED")
                if self.owner == "A": require(historical.get(h.endpoint) == h, "GATE_HISTORY_INCONSISTENT")
        require(all(original[k] == historical[k] for k in set(original) & set(historical)), "GATE_HISTORY_INCONSISTENT")
        require(not (set(historical) & set(kept)), "GATE_EXPLICIT_CLOSE_REQUIRED")
        if self.owner == "UI":
            require(not self.survivors, "UI_EXEC_ENDPOINT_LEAK")
        else:
            require({(slot(h.position), h.channel) for h in self.survivors} == {("fd3", "RA"), ("fd4", "metadata")}, "AGENT_EXEC_MAP")
            require(all(not h.cloexec for h in self.survivors), "REQUIRED_EXEC_SURVIVOR_CLOEXEC")


@dataclass(frozen=True)
class MetadataState:
    """Injected validation outcome only; wire parsing remains the frozen codec's job."""
    executed: bool = False
    validated: bool = False
    closed: bool = False
    failure: str | None = None

    def __post_init__(self):
        require(all(type(value) is bool for value in (self.executed, self.validated, self.closed)), "INVALID_BOOLEAN")
        require(not self.validated or self.executed, "METADATA_VALIDATION_ORDER")
        require(not self.closed or self.validated, "METADATA_CLOSE_ORDER")

    def advance(self, event, valid=True):
        if self.failure is not None: return self
        try:
            if event == "exec":
                require(not self.executed and not self.closed, "METADATA_EXEC_ORDER")
                return replace(self, executed=True)
            if event == "validate":
                require(self.executed and not self.validated and not self.closed and valid is True, "METADATA_VALIDATION_ORDER")
                return replace(self, validated=True)
            if event == "close":
                require(self.validated and not self.closed, "METADATA_CLOSE_ORDER")
                return replace(self, closed=True)
            if event == "ui_spawn":
                require(self.closed, "METADATA_UI_LEAK")
                return self
            raise BoundaryError("INVALID_EVENT")
        except BoundaryError as error:
            return replace(self, failure=str(error))


@dataclass(frozen=True)
class WaitOutcome:
    kind: str
    owner: str
    target_pid: int
    returned_pid: int
    exit_code: int | None = None
    normal: bool = False
    host_disposed: bool = False


@dataclass(frozen=True)
class WaitState:
    binding: ForkBinding
    deadline: Deadline
    max_eintr: int
    last_now: int = 0
    eintr_count: int = 0
    complete: bool = False
    failure: str | None = None
    proof: WaitOutcome | None = None

    def __post_init__(self):
        require(type(self.binding) is ForkBinding and type(self.deadline) is Deadline
                and self.deadline.kind == "shutdown", "INVALID_WAIT_CONTRACT")
        integer(self.max_eintr, 1); integer(self.last_now); integer(self.eintr_count)
        require(self.eintr_count <= self.max_eintr, "EINTR_LIMIT")
        require(type(self.complete) is bool, "INVALID_BOOLEAN")
        if self.complete:
            require(type(self.proof) is WaitOutcome, "POSITIVE_BOUND_WAIT_MISSING")
            integer(self.proof.target_pid, 2, MAX_PID); integer(self.proof.returned_pid, 2, MAX_PID)
            require(type(self.proof) is WaitOutcome and self.proof.kind == "exit"
                    and self.proof.owner == self.binding.status_owners[0]
                    and self.proof.target_pid == self.binding.child_pid
                    and self.proof.returned_pid == self.binding.child_pid
                    and type(self.proof.exit_code) is int and self.proof.exit_code == 0
                    and self.proof.normal is True, "POSITIVE_BOUND_WAIT_MISSING")
            require(self.binding.child != "R" or self.proof.host_disposed is True, "ROUTER_HOST_NOT_DISPOSED")
        else:
            require(self.proof is None, "PREMATURE_WAIT_PROOF")
        if self.complete or self.last_now != 0 or self.eintr_count:
            self.deadline.check(self.last_now, self.deadline.end)

    def observe(self, observation, now, propagated_deadline):
        if self.failure is not None: return self
        try:
            self.deadline.check(now, propagated_deadline)
            require(now >= self.last_now, "CLOCK_REGRESSION")
            require(not self.complete, "DUPLICATE_WAIT")
            require(type(observation) is WaitOutcome, "INVALID_OBSERVATION")
            require(type(observation.kind) is str, "INVALID_OBSERVATION")
            require(observation.owner == self.binding.status_owners[0], "COMPETING_STATUS_OWNER")
            integer(observation.target_pid, 2, MAX_PID)
            require(observation.target_pid == self.binding.child_pid, "WRONG_WAIT_TARGET")
            integer(observation.returned_pid, -1, MAX_PID)
            if observation.kind in ("running", "eintr"):
                require(observation.returned_pid == (0 if observation.kind == "running" else -1)
                        and observation.exit_code is None and observation.normal is False, "MALFORMED_PENDING_WAIT")
                count = self.eintr_count + int(observation.kind == "eintr")
                require(count <= self.max_eintr, "EINTR_LIMIT")
                return replace(self, last_now=now, eintr_count=count)
            if observation.kind != "exit":
                reasons = {"echild": "ECHILD_REQUIRED_CHILD", "signal": "SIGNALED_CHILD", "interop": "WAIT_INTEROP_ERROR"}
                raise BoundaryError(reasons.get(observation.kind, "NOT_POSITIVE_WAIT_PROOF"))
            require(observation.returned_pid == self.binding.child_pid, "WRONG_RETURNED_PID")
            integer(observation.exit_code, 0, 255)
            require(observation.normal is True, "NOT_NORMAL_EXIT")
            require(observation.exit_code == 0, "NONZERO_CHILD_EXIT")
            require(self.binding.child != "R" or observation.host_disposed is True, "ROUTER_HOST_NOT_DISPOSED")
            return replace(self, last_now=now, complete=True, proof=observation)
        except BoundaryError as error:
            return replace(self, failure=str(error))


@dataclass(frozen=True)
class Ledger:
    deadline: Deadline
    waits: tuple[WaitState, ...]
    startup_events: tuple[str, ...] = ()
    shutdown_events: tuple[str, ...] = ()
    failure: str | None = None
    last_now: int = 0
    shutdown_times: tuple[int, ...] = ()

    def __post_init__(self):
        require(type(self.deadline) is Deadline, "INVALID_LEDGER_DEADLINE")
        require(type(self.shutdown_events) is tuple, "INVALID_EVENT_SET")
        validate_startup_prefix(self.startup_events)
        integer(self.last_now)
        require(type(self.waits) is tuple and all(type(w) is WaitState for w in self.waits), "INVALID_WAIT_SET")
        if self.waits: validate_wait_set(self.waits)
        require(type(self.shutdown_times) is tuple, "INVALID_EVENT_TIMES")

    def advance(self, event, now, propagated_deadline):
        if self.failure is not None: return self
        try:
            require(type(event) is str, "INVALID_EVENT")
            self.deadline.check(now, propagated_deadline)
            require(now >= self.last_now, "CLOCK_REGRESSION")
            require(type(self.waits) is tuple and all(type(w) is WaitState for w in self.waits), "INVALID_WAIT_SET")
            require(all(w.failure is None for w in self.waits), "REQUIRED_WAIT_FAILED")
            if event in ("A_READY", "UI_READY", "R_UI_READY", "R_EXPOSE", "listener"):
                require(self.deadline.kind == "startup", "WRONG_DEADLINE_PHASE")
                validate_startup_prefix(self.startup_events)
                order = ("A_READY", "UI_READY", "R_UI_READY", "R_EXPOSE", "listener")
                require(len(self.startup_events) < len(order) and event == order[len(self.startup_events)], "PREMATURE_EXPOSURE")
                return replace(self, startup_events=self.startup_events + (event,), last_now=now)
            require(self.deadline.kind == "shutdown", "WRONG_DEADLINE_PHASE")
            validate_wait_set(self.waits)
            validate_shutdown_history(self.deadline, self.shutdown_events, self.shutdown_times, self.waits)
            order = ("UI_STOPPED", "EOF", "R_PROTOCOL_DONE", "HOST_DISPOSED", "agent_wait", "guardian_wait", "router_wait", "drain", "absence")
            require(len(self.shutdown_events) < len(order) and event == order[len(self.shutdown_events)], "SHUTDOWN_ORDER")
            if self.shutdown_times: require(now > self.shutdown_times[-1], "EVENT_TIME_NOT_STRICT")
            pairs = {"agent_wait": ("A", "G"), "guardian_wait": ("G", "S"), "router_wait": ("R", "S")}
            if event in pairs:
                child, owner = pairs[event]
                matches = [w for w in self.waits if w.binding.child == child and w.binding.status_owners == (owner,)]
                require(len(matches) == 1 and matches[0].complete and matches[0].deadline == self.deadline, "POSITIVE_BOUND_WAIT_MISSING")
                require(matches[0].last_now <= now, "WAIT_OBSERVATION_IN_FUTURE")
            if event in ("drain", "absence"):
                require(all(name in self.shutdown_events for name in pairs), "DRAIN_BEFORE_WAITS")
            events, times = self.shutdown_events + (event,), self.shutdown_times + (now,)
            validate_shutdown_history(self.deadline, events, times, self.waits)
            return replace(self, shutdown_events=events, shutdown_times=times, last_now=now)
        except BoundaryError as error:
            return replace(self, failure=str(error))

    @property
    def complete_model(self):
        order = ("UI_STOPPED", "EOF", "R_PROTOCOL_DONE", "HOST_DISPOSED", "agent_wait", "guardian_wait", "router_wait", "drain", "absence")
        if (self.failure is not None or self.deadline.kind != "shutdown" or self.shutdown_events != order
                or type(self.waits) is not tuple or len(self.waits) != 3 or not all(type(w) is WaitState for w in self.waits)):
            return False
        try:
            validate_wait_set(self.waits)
            validate_shutdown_history(self.deadline, self.shutdown_events, self.shutdown_times, self.waits)
        except BoundaryError:
            return False
        return all(sum(w.binding.child == child and w.binding.status_owners == (owner,) and w.complete
                       and w.failure is None and w.deadline == self.deadline for w in self.waits) == 1
                   for child, owner in (("A", "G"), ("G", "S"), ("R", "S")))


def validate_startup_prefix(events):
    """Validate this ledger's readiness projection; gate/metadata remain separate."""
    order = ("A_READY", "UI_READY", "R_UI_READY", "R_EXPOSE", "listener")
    require(type(events) is tuple and all(type(e) is str for e in events), "INVALID_STARTUP_PREFIX")
    require(len(events) <= len(order), "STARTUP_PREFIX_TOO_LONG")
    require(all(e in order for e in events), "UNKNOWN_STARTUP_EVENT")
    require(len(set(events)) == len(events), "DUPLICATE_STARTUP_EVENT")
    require(events == order[:len(events)], "STARTUP_PREFIX_ORDER")


def validate_wait_set(waits):
    require(type(waits) is tuple and len(waits) == 3 and all(type(w) is WaitState for w in waits), "INCOMPLETE_REQUIRED_CHILD_SET")
    require({w.binding.child for w in waits} == {"A", "G", "R"}, "INCOMPLETE_REQUIRED_CHILD_SET")
    require(len({w.binding.child_pid for w in waits}) == 3, "DUPLICATE_REQUIRED_CHILD_PID")
    by_role = {w.binding.child: w for w in waits}
    require(by_role["A"].binding.parent_pid == by_role["G"].binding.child_pid, "WRONG_PARENT_PID")
    require(all(w.binding.status_owners == (("G",) if w.binding.child == "A" else ("S",)) for w in waits), "COMPETING_STATUS_OWNER")


def validate_shutdown_history(deadline, events, times, waits):
    order = ("UI_STOPPED", "EOF", "R_PROTOCOL_DONE", "HOST_DISPOSED", "agent_wait", "guardian_wait", "router_wait", "drain", "absence")
    require(type(events) is tuple and type(times) is tuple and len(events) == len(times)
            and events == order[:len(events)], "INVALID_EVENT_TIMES")
    for time in times: deadline.check(time, deadline.end)
    require(all(a < b for a, b in zip(times, times[1:])), "EVENT_TIME_NOT_STRICT")
    stamps = dict(zip(events, times))
    by_role = {w.binding.child: w for w in waits}
    for event, child in (("agent_wait", "A"), ("guardian_wait", "G"), ("router_wait", "R")):
        if event in stamps:
            state = by_role.get(child)
            require(state is not None and state.complete and state.failure is None and state.deadline == deadline, "POSITIVE_BOUND_WAIT_MISSING")
            require(state.last_now <= stamps[event], "WAIT_OBSERVATION_IN_FUTURE")
            prior = stamps.get("EOF") if child == "A" else by_role["A"].last_now if child == "G" else stamps.get("HOST_DISPOSED")
            reason = {"A": "AGENT_WAIT_BEFORE_EOF", "G": "GUARDIAN_WAIT_BEFORE_AGENT_WAIT", "R": "ROUTER_WAIT_BEFORE_DISPOSAL"}[child]
            require(prior is not None and state.last_now > prior, reason)
    if "drain" in stamps:
        require(all(by_role[c].complete and by_role[c].last_now < stamps["drain"] for c in ("A", "G", "R")), "DRAIN_BEFORE_WAITS")

"""Pure aiq-admin-lifecycle-v1 codec/models; decoded reports are not kernel proof."""
from dataclasses import dataclass
from enum import IntEnum
import struct


class ProtocolError(ValueError):
    """Fixed non-sensitive failure; never includes record contents."""


class Role(IntEnum):
    ROOT = 1
    SUPERVISOR = 2
    GUARDIAN_BOOTSTRAP = 3
    GUARDIAN = 4
    ROUTER_BOOTSTRAP = 5
    ROUTER = 6
    AGENT_BOOTSTRAP = 7
    AGENT = 8


class Message(IntEnum):
    META_ROUTER = 0x01
    META_GUARDIAN = 0x02
    META_AGENT = 0x03
    G_BOUND = 0x10
    R_PREPARE = 0x11
    R_BOUND = 0x12
    G_RELEASE = 0x13
    A_EXEC = 0x14
    A_EXEC_ACK = 0x15
    A_BIND = 0x16
    A_READY = 0x17
    START_UI = 0x18
    UI_READY = 0x19
    R_UI_READY = 0x1a
    R_EXPOSE = 0x1b
    R_EXPOSED = 0x1c
    STOP_ROUTER = 0x20
    STOP_GUARDIAN = 0x21
    STOP_UI = 0x22
    UI_STOPPED = 0x23
    R_PROTOCOL_DONE = 0x24
    HOST_DISPOSED = 0x25
    AGENT_RESULT = 0x30
    GUARDIAN_OBSERVED = 0x31
    ROUTER_OBSERVED = 0x32
    CHILDREN_ABSENT = 0x33
    FAIL = 0x7f
    AGENT_SIGNAL = 0xb0
    AGENT_WAIT_FAILURE = 0xb1
    GUARDIAN_SIGNAL = 0xb2
    GUARDIAN_WAIT_FAILURE = 0xb3
    ROUTER_SIGNAL = 0xb4
    ROUTER_WAIT_FAILURE = 0xb5


MAX_TIME = (1 << 63) - 1
STARTUP_NS = 30_000_000_000
PRIVILEGED_NS = 5_000_000_000
SHUTDOWN_NS = 4_000_000_000
LAYOUT = struct.Struct("!4sBBBB16sIIIQQ12s")
INTERNAL = {Message.GUARDIAN_OBSERVED, Message.ROUTER_OBSERVED,
            Message.CHILDREN_ABSENT, Message.GUARDIAN_SIGNAL,
            Message.GUARDIAN_WAIT_FAILURE, Message.ROUTER_SIGNAL,
            Message.ROUTER_WAIT_FAILURE}
NORMAL = {Message.AGENT_RESULT, Message.GUARDIAN_OBSERVED, Message.ROUTER_OBSERVED}
SIGNAL = {Message.AGENT_SIGNAL, Message.GUARDIAN_SIGNAL, Message.ROUTER_SIGNAL}
WAIT = {Message.AGENT_WAIT_FAILURE, Message.GUARDIAN_WAIT_FAILURE, Message.ROUTER_WAIT_FAILURE}
METADATA = {Message.META_ROUTER, Message.META_GUARDIAN, Message.META_AGENT}
STARTUP = set(Message) - INTERNAL - NORMAL - SIGNAL - WAIT - METADATA - {
    Message.FAIL, Message.STOP_ROUTER, Message.STOP_GUARDIAN, Message.STOP_UI,
    Message.UI_STOPPED, Message.R_PROTOCOL_DONE, Message.HOST_DISPOSED}


def _integer(value, low, high):
    if type(value) is not int or not low <= value <= high:
        raise ProtocolError("Invalid numeric field.")


def deadline(now, duration):
    _integer(now, 0, MAX_TIME)
    _integer(duration, 1, MAX_TIME)
    if now > MAX_TIME - duration:
        raise ProtocolError("Deadline overflow.")
    return now + duration


def startup_deadlines(now):
    total = deadline(now, STARTUP_NS)
    return total - (STARTUP_NS - PRIVILEGED_NS), total


def check_privileged_deadline(startup_deadline, now):
    _integer(startup_deadline, STARTUP_NS, MAX_TIME)
    _integer(now, 0, MAX_TIME)
    if now >= startup_deadline - (STARTUP_NS - PRIVILEGED_NS):
        raise ProtocolError("Privileged deadline expired.")


@dataclass(frozen=True)
class Record:
    role: Role
    message: Message
    outcome: int
    launch: bytes
    sequence: int
    sender_pid: int
    related_pid: int
    startup_deadline: int
    shutdown_deadline: int = 0

    def validate(self):
        if type(self.role) is not Role or type(self.message) is not Message:
            raise ProtocolError("Unknown role or type.")
        if type(self.launch) is not bytes or len(self.launch) != 16 or not any(self.launch):
            raise ProtocolError("Invalid launch binding.")
        _integer(self.sequence, 1, 32)
        _integer(self.sender_pid, 1, 0x7fffffff)
        _integer(self.related_pid, 0 if self.message == Message.CHILDREN_ABSENT else 1, 0x7fffffff)
        _integer(self.startup_deadline, 1, MAX_TIME)
        _integer(self.shutdown_deadline, 0, MAX_TIME)
        _integer(self.outcome, 0, 255)
        if self.message in SIGNAL:
            _integer(self.outcome, 1, 64)
        elif self.message in WAIT:
            _integer(self.outcome, 1, 4)
        elif self.message == Message.FAIL:
            _integer(self.outcome, 1, 8)
        elif self.message not in NORMAL and self.outcome != 0:
            raise ProtocolError("Invalid outcome.")

    def encode(self):
        self.validate()
        return LAYOUT.pack(b"AIQL", 1, self.role, self.message, self.outcome,
                           self.launch, self.sequence, self.sender_pid, self.related_pid,
                           self.startup_deadline, self.shutdown_deadline, bytes(12))

    @staticmethod
    def decode(data):
        if type(data) is not bytes or len(data) != 64:
            raise ProtocolError("Invalid frame length.")
        magic, version, role, message, outcome, launch, seq, pid, related, start, stop, reserved = LAYOUT.unpack(data)
        if magic != b"AIQL" or version != 1 or any(reserved):
            raise ProtocolError("Invalid frame header.")
        try:
            record = Record(Role(role), Message(message), outcome, launch, seq, pid, related, start, stop)
        except ValueError:
            raise ProtocolError("Unknown role or type.") from None
        record.validate()
        return record


# Channel names denote bootstrap-confined endpoints, not network sockets.
CATALOG = {
    "sr": {Message.R_PREPARE: Role.SUPERVISOR, Message.R_BOUND: Role.ROUTER,
           Message.R_UI_READY: Role.ROUTER, Message.R_EXPOSE: Role.SUPERVISOR,
           Message.R_EXPOSED: Role.ROUTER, Message.STOP_ROUTER: Role.SUPERVISOR,
           Message.R_PROTOCOL_DONE: Role.ROUTER, Message.HOST_DISPOSED: Role.ROUTER},
    "sg": {Message.G_BOUND: Role.GUARDIAN, Message.G_RELEASE: Role.SUPERVISOR,
           Message.STOP_GUARDIAN: Role.SUPERVISOR, Message.AGENT_RESULT: Role.GUARDIAN,
           Message.AGENT_SIGNAL: Role.GUARDIAN, Message.AGENT_WAIT_FAILURE: Role.GUARDIAN},
    "ra": {Message.A_BIND: Role.ROUTER, Message.A_READY: Role.AGENT,
           Message.START_UI: Role.ROUTER, Message.UI_READY: Role.AGENT,
           Message.STOP_UI: Role.ROUTER, Message.UI_STOPPED: Role.AGENT},
    "gate": {Message.A_EXEC: Role.GUARDIAN, Message.A_EXEC_ACK: Role.AGENT_BOOTSTRAP},
}
PEERS = {"sr": {Role.SUPERVISOR, Role.ROUTER}, "sg": {Role.SUPERVISOR, Role.GUARDIAN},
         "ra": {Role.ROUTER, Role.AGENT}}
ORDER = {
    ("sr", Role.SUPERVISOR): (Message.R_PREPARE, Message.R_EXPOSE, Message.STOP_ROUTER),
    ("sr", Role.ROUTER): (Message.R_BOUND, Message.R_UI_READY, Message.R_EXPOSED, Message.R_PROTOCOL_DONE, Message.HOST_DISPOSED),
    ("sg", Role.SUPERVISOR): (Message.G_RELEASE, Message.STOP_GUARDIAN),
    ("sg", Role.GUARDIAN): (Message.G_BOUND, (Message.AGENT_RESULT, Message.AGENT_SIGNAL, Message.AGENT_WAIT_FAILURE)),
    ("ra", Role.ROUTER): (Message.A_BIND, Message.START_UI, Message.STOP_UI),
    ("ra", Role.AGENT): (Message.A_READY, Message.UI_READY, Message.UI_STOPPED),
    ("gate", Role.GUARDIAN): (Message.A_EXEC,),
    ("gate", Role.AGENT_BOOTSTRAP): (Message.A_EXEC_ACK,),
}


class Direction:
    """One direction, bound by caller's trusted metadata; failure is sticky."""
    def __init__(self, channel, role, launch, sender_pid, startup_deadline):
        _integer(sender_pid, 1, (1 << 31) - 1)
        _integer(startup_deadline, 1, MAX_TIME)
        if channel not in CATALOG:
            raise ProtocolError("Unknown channel.")
        self.channel, self.role, self.launch = channel, role, bytes(launch)
        self.sender_pid, self.startup_deadline = sender_pid, startup_deadline
        self.next_sequence = 1
        self.counts = [0, 0]
        self.stop_deadline = None
        self.failed = False
        self.finished = False
        self.position = 0

    def bind_shutdown(self, started_at):
        try:
            if self.failed or self.finished or self.stop_deadline is not None:
                raise ProtocolError("Shutdown already bound.")
            self.stop_deadline = deadline(started_at, SHUTDOWN_NS)
        except ProtocolError:
            self.failed = True
            raise

    def accept(self, record, now, expected_message, related_pid):
        try:
            _integer(related_pid, 1, (1 << 31) - 1)
            if self.failed or self.finished:
                raise ProtocolError("Closed direction.")
            if type(record) is not Record:
                raise ProtocolError("Invalid record.")
            record.validate()
            allowed = CATALOG[self.channel].get(record.message)
            if record.message == Message.FAIL and self.role in PEERS.get(self.channel, set()):
                allowed = self.role
            if allowed != self.role or record.role != self.role or record.message != expected_message:
                raise ProtocolError("Invalid channel or state.")
            order = ORDER.get((self.channel, self.role), ())
            if record.message != Message.FAIL:
                if self.position >= len(order):
                    raise ProtocolError("Invalid direction state.")
                expected = order[self.position]
                if record.message not in (expected if isinstance(expected, tuple) else (expected,)):
                    raise ProtocolError("Invalid direction state.")
            if (record.launch != self.launch or record.sender_pid != self.sender_pid
                    or record.related_pid != related_pid or record.sequence != self.next_sequence
                    or record.startup_deadline != self.startup_deadline):
                raise ProtocolError("Invalid binding or sequence.")
            _integer(now, 0, MAX_TIME)
            stopping = record.shutdown_deadline != 0
            if record.message in STARTUP and stopping:
                raise ProtocolError("Startup clock changed.")
            if record.message not in STARTUP and record.message != Message.FAIL and not stopping:
                raise ProtocolError("Missing shutdown clock.")
            if stopping:
                if self.stop_deadline is None or self.stop_deadline != record.shutdown_deadline:
                    raise ProtocolError("Shutdown clock changed.")
                if now < self.stop_deadline - SHUTDOWN_NS:
                    raise ProtocolError("Shutdown clock regressed.")
            elif self.stop_deadline is not None:
                raise ProtocolError("Shutdown clock reset.")
            if now >= (record.shutdown_deadline if stopping else record.startup_deadline):
                raise ProtocolError("Deadline expired.")
            index = int(stopping)
            if self.counts[index] >= 16:
                raise ProtocolError("Record limit exceeded.")
            self.counts[index] += 1
            self.next_sequence += 1
            self.position += 1
            if record.message == Message.FAIL:
                raise ProtocolError("Peer failed.")
        except ProtocolError:
            self.failed = True
            raise

    def eof(self, permitted=False, now=None):
        try:
            _integer(now, 0, MAX_TIME)
            if (self.failed or self.finished or permitted is not True or self.channel != "ra"
                    or self.role != Role.AGENT or self.position != len(ORDER[("ra", Role.AGENT)])
                    or self.stop_deadline is None or not self.stop_deadline - SHUTDOWN_NS <= now < self.stop_deadline):
                raise ProtocolError("Unexpected EOF.")
            self.finished = True
        except ProtocolError:
            self.failed = True
            raise


class LaunchGate:
    """C1: receipt acknowledgment is not agent readiness or exec evidence."""
    def __init__(self):
        self.state = 0
        self.failed = False

    def accept(self, record):
        try:
            if type(record) is not Record:
                raise ProtocolError("Invalid gate record.")
            record.validate()
            expected = [(Role.GUARDIAN, Message.A_EXEC), (Role.AGENT_BOOTSTRAP, Message.A_EXEC_ACK)]
            if self.failed or self.state >= 2 or (record.role, record.message) != expected[self.state]:
                raise ProtocolError("Invalid launch gate state.")
            self.state += 1
        except ProtocolError:
            self.failed = True
            raise

    @property
    def must_close_before_exec(self):
        return self.state == 2 and not self.failed


def metadata(data, receiver, launch, bindings, startup_deadline, now=0):
    _integer(startup_deadline, 1, MAX_TIME)
    _integer(now, 0, MAX_TIME)
    if now >= startup_deadline:
        raise ProtocolError("Metadata deadline expired.")
    order = {Role.SUPERVISOR: (Message.META_ROUTER, Message.META_GUARDIAN),
             Role.ROUTER: (Message.META_ROUTER,),
             Role.GUARDIAN: (Message.META_ROUTER, Message.META_AGENT),
             Role.AGENT: (Message.META_ROUTER, Message.META_AGENT)}.get(receiver)
    if order is None or len(data) != len(order) * 64:
        raise ProtocolError("Invalid metadata stream.")
    result = []
    for index, message in enumerate(order):
        record = Record.decode(data[index * 64:(index + 1) * 64])
        if message not in bindings:
            raise ProtocolError("Missing metadata binding.")
        sender, subject = bindings[message]
        _integer(sender, 1, (1 << 31) - 1)
        _integer(subject, 1, (1 << 31) - 1)
        expected_role = Role.GUARDIAN_BOOTSTRAP if message == Message.META_AGENT else Role.ROOT
        if (record.message != message or record.role != expected_role or record.sequence != index + 1
                or record.sender_pid != sender or record.related_pid != subject
                or record.launch != launch or record.startup_deadline != startup_deadline
                or record.shutdown_deadline != 0):
            raise ProtocolError("Invalid metadata binding.")
        result.append(record)
    return tuple(result)


class Startup:
    """Offline global transition model; hardening is a prerequisite, not codec proof."""
    ORDER = (Message.G_BOUND, Message.R_PREPARE, Message.R_BOUND, Message.G_RELEASE,
             Message.A_EXEC, Message.A_EXEC_ACK, Message.A_BIND, Message.A_READY,
             Message.START_UI, Message.UI_READY, Message.R_UI_READY, Message.R_EXPOSE, Message.R_EXPOSED)

    def __init__(self):
        self.position = 0
        self.failed = False

    def accept(self, message):
        if self.failed or self.position == len(self.ORDER) or message != self.ORDER[self.position]:
            self.failed = True
            raise ProtocolError("Invalid startup state.")
        self.position += 1

    @property
    def running(self):
        return not self.failed and self.position == len(self.ORDER)


class Frames:
    """Bounded pure incremental framing; transport/deadline I/O is caller-owned."""
    def __init__(self):
        self.pending = b""
        self.failed = False
        self.finished = False

    def feed(self, data):
        if self.failed or self.finished or type(data) is not bytes or len(data) > 64:
            self.failed = True
            raise ProtocolError("Invalid frame chunk.")
        buffer = self.pending + data
        try:
            records = () if len(buffer) < 64 else (Record.decode(buffer[:64]),)
            self.pending = buffer if len(buffer) < 64 else buffer[64:]
            return records
        except ProtocolError:
            self.failed = True
            raise

    def eof(self):
        if self.failed or self.finished or self.pending:
            self.failed = True
            raise ProtocolError("Partial frame EOF.")
        self.finished = True


class Shutdown:
    """Offline evidence-order model, not a source of independent kernel proof.

    wait_observation accepts injected observations ONLY for deterministic tests.
    No method performs a wait or qualifies the guardian's trust boundary.
    """
    def __init__(self, started_at, agent_pid, guardian_pid, router_pid, launch, startup_deadline):
        for pid in (agent_pid, guardian_pid, router_pid):
            _integer(pid, 1, 0x7fffffff)
        if len({agent_pid, guardian_pid, router_pid}) != 3:
            raise ProtocolError("Duplicate child binding.")
        if type(launch) is not bytes or len(launch) != 16 or not any(launch):
            raise ProtocolError("Invalid launch binding.")
        _integer(startup_deadline, 1, MAX_TIME)
        self.deadline = deadline(started_at, SHUTDOWN_NS)
        self.started_at, self.agent_pid = started_at, agent_pid
        self.guardian_pid, self.router_pid = guardian_pid, router_pid
        self.launch, self.startup_deadline = launch, startup_deadline
        self.report = None
        self.events = set()
        self.waits = set()
        self.failed = False

    def _check(self, now):
        try:
            _integer(now, 0, MAX_TIME)
            if self.failed or not self.started_at <= now < self.deadline:
                raise ProtocolError("Shutdown unavailable.")
        except ProtocolError:
            self.failed = True
            raise

    def event(self, message, now):
        self._check(now)
        requirements = {
            Message.STOP_ROUTER: set(), Message.STOP_GUARDIAN: set(),
            Message.STOP_UI: {Message.STOP_ROUTER},
            Message.UI_STOPPED: {Message.STOP_UI},
            "lifecycle_eof": {Message.UI_STOPPED},
            Message.R_PROTOCOL_DONE: {"lifecycle_eof"},
            Message.HOST_DISPOSED: {Message.R_PROTOCOL_DONE},
        }
        if (type(message) is not Message and message != "lifecycle_eof") or message not in requirements or message in self.events or not requirements[message] <= self.events:
            self.failed = True
            raise ProtocolError("Invalid shutdown state.")
        self.events.add(message)

    def agent_report(self, record, now, channel):
        """Stage a bound untrusted report; an injected positive wait is still required."""
        try:
            self._check(now)
            if type(record) is not Record:
                raise ProtocolError("Invalid agent report.")
            record.validate()
            if (self.report is not None or Message.STOP_GUARDIAN not in self.events or channel != "sg"
                    or record.role != Role.GUARDIAN or record.message != Message.AGENT_RESULT
                    or record.outcome != 0 or record.launch != self.launch or record.sequence != 2
                    or record.sender_pid != self.guardian_pid or record.related_pid != self.agent_pid
                    or record.startup_deadline != self.startup_deadline or record.shutdown_deadline != self.deadline):
                raise ProtocolError("Unsuccessful agent report.")
            self.report = record
        except ProtocolError:
            self.failed = True
            raise

    def wait_observation(self, subject, returned_pid, normal_exit, exit_code, now):
        try:
            self._check(now)
            _integer(returned_pid, 1, 0x7fffffff)
            _integer(exit_code, 0, 255)
            if type(subject) is not str or type(normal_exit) is not bool:
                raise ProtocolError("Malformed wait observation.")
            expected = {"agent": self.agent_pid, "guardian": self.guardian_pid, "router": self.router_pid}.get(subject)
            ready = (self.report is not None if subject == "agent" else
                     Message.AGENT_RESULT in self.events if subject == "guardian" else
                     Message.HOST_DISPOSED in self.events)
            if expected is None or returned_pid != expected or not normal_exit or exit_code != 0 or not ready or subject in self.waits:
                raise ProtocolError("Unverified child status.")
            self.waits.add(subject)
            if subject == "agent":
                self.events.add(Message.AGENT_RESULT)
        except ProtocolError:
            self.failed = True
            raise

    def absence(self, no_children, now):
        self._check(now)
        if (self.waits != {"agent", "guardian", "router"} or no_children is not True
                or "lifecycle_eof" not in self.events or "absence" in self.events):
            self.failed = True
            raise ProtocolError("Unverified descendant absence.")
        self.events.add("absence")

    @property
    def complete_model(self):
        return not self.failed and "absence" in self.events


DESCRIPTORS = {
    Role.SUPERVISOR: {3: "sr", 4: "sg", 5: "metadata"},
    Role.GUARDIAN: {3: "sg", 4: "gate", 5: "metadata"},
    Role.ROUTER: {3: "credential", 4: "ra", 5: "sr", 6: "metadata"},
    Role.AGENT: {3: "ra", 4: "metadata"},
}


def validate_descriptors(role, handles, metadata_consumed=False):
    """Pure descriptor-map check; never opens, reads, closes or duplicates an FD."""
    expected = dict(DESCRIPTORS.get(role, {}))
    if role not in DESCRIPTORS:
        raise ProtocolError("Unknown descriptor role.")
    if metadata_consumed:
        expected = {fd: kind for fd, kind in expected.items() if kind != "metadata"}
    if handles != expected:
        raise ProtocolError("Invalid descriptor map.")

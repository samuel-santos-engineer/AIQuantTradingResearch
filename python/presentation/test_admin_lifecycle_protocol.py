"""Focused offline conformance. No process, credential, socket or FD operations."""
import importlib.util
from pathlib import Path
import sys
import unittest
from dataclasses import replace

spec = importlib.util.spec_from_file_location("admin_lifecycle_protocol", Path(__file__).resolve().parents[2] / "container/admin_lifecycle_protocol.py")
p = importlib.util.module_from_spec(spec)
sys.modules[spec.name] = p
spec.loader.exec_module(p)
L = bytes.fromhex("00112233445566778899aabbccddeeff")
META = "4149514c0101010000112233445566778899aabbccddeeff00000001000000010000002a00000006fc23ac000000000000000000000000000000000000000000"
AGENT = "4149514c0104300000112233445566778899aabbccddeeff000000010000002b0000002c00000006fc23ac0000000007ea8ed400000000000000000000000000"


def record(**changes):
    return replace(p.Record(p.Role.ROOT, p.Message.META_ROUTER, 0, L, 1, 1, 42, 30_000_000_000), **changes)


class ProtocolTests(unittest.TestCase):
    def test_strict_context_bindings(self):
        bad_pids = (True, False, 1.0, 42.0, None, "42", 0, -1, 1 << 31)
        bad_times = (True, False, 30_000_000_000.0, None, "30000000000", 0, -1, p.MAX_TIME + 1)
        for value in bad_pids:
            with self.subTest(pid=value):
                with self.assertRaises(p.ProtocolError):
                    p.Direction("sr", p.Role.SUPERVISOR, L, value, 30_000_000_000)
                d = p.Direction("sr", p.Role.SUPERVISOR, L, 1, 30_000_000_000)
                r = record(role=p.Role.SUPERVISOR, message=p.Message.R_PREPARE)
                with self.assertRaises(p.ProtocolError): d.accept(r, 0, r.message, value)
                self.assertTrue(d.failed)
                with self.assertRaises(p.ProtocolError): d.accept(r, 0, r.message, 42)
                for pair in ((value, 42), (1, value)):
                    with self.assertRaises(p.ProtocolError):
                        p.metadata(record().encode(), p.Role.ROUTER, L,
                                   {p.Message.META_ROUTER: pair}, 30_000_000_000)
        for value in bad_times:
            with self.subTest(deadline=value):
                with self.assertRaises(p.ProtocolError):
                    p.Direction("sr", p.Role.SUPERVISOR, L, 1, value)
                with self.assertRaises(p.ProtocolError):
                    p.metadata(record().encode(), p.Role.ROUTER, L,
                               {p.Message.META_ROUTER: (1, 42)}, value)
        d = p.Direction("sr", p.Role.SUPERVISOR, L, 1, 30_000_000_000)
        r = record(role=p.Role.SUPERVISOR, message=p.Message.R_PREPARE)
        d.accept(r, 0, r.message, 42)
        self.assertFalse(d.failed)

    def test_canonical_metadata(self):
        self.assertEqual(META, record().encode().hex())
        self.assertEqual(64, len(record().encode()))
        self.assertEqual(record(), p.Record.decode(bytes.fromhex(META)))

    def test_canonical_agent_results(self):
        for kind, outcome in ((p.Message.AGENT_RESULT, 0), (p.Message.AGENT_RESULT, 7),
                              (p.Message.AGENT_SIGNAL, 15), (p.Message.AGENT_WAIT_FAILURE, 1)):
            with self.subTest(kind=kind, outcome=outcome):
                r = record(role=p.Role.GUARDIAN, message=kind, outcome=outcome, sender_pid=43,
                           related_pid=44, shutdown_deadline=34_000_000_000)
                expected = bytearray.fromhex(AGENT); expected[6] = kind; expected[7] = outcome
                self.assertEqual(bytes(expected), r.encode())
                direction = p.Direction("sg", p.Role.GUARDIAN, L, 43, r.startup_deadline)
                direction.accept(replace(r, message=p.Message.G_BOUND, outcome=0, shutdown_deadline=0),
                                 0, p.Message.G_BOUND, 44)
                direction.bind_shutdown(30_000_000_000)
                direction.accept(replace(p.Record.decode(bytes(expected)), sequence=2), 30_000_000_001, kind, 44)

    def test_invalid_bytes(self):
        original = bytes.fromhex(META)
        negatives = [original[:-1], original + b"x"]
        for offset, value in ((0, 0), (4, 2), (5, 9), (6, 0xff), (7, 7), (63, 1)):
            data = bytearray(original); data[offset] = value; negatives.append(bytes(data))
        for offset, count in ((8, 16), (24, 4), (28, 4), (36, 8)):
            data = bytearray(original); data[offset:offset + count] = bytes(count); negatives.append(bytes(data))
        for data in negatives:
            with self.subTest(data_length=len(data)):
                with self.assertRaises(p.ProtocolError): p.Record.decode(data)

    def test_numeric_bounds_and_outcomes(self):
        for changes in ({"sequence": 33}, {"sequence": True}, {"sender_pid": 1 << 31},
                        {"related_pid": 0}, {"startup_deadline": 1 << 63}, {"shutdown_deadline": 1 << 63},
                        {"message": p.Message.AGENT_SIGNAL, "outcome": 0},
                        {"message": p.Message.AGENT_SIGNAL, "outcome": 65},
                        {"message": p.Message.AGENT_WAIT_FAILURE, "outcome": 5},
                        {"message": p.Message.FAIL, "outcome": 0}):
            with self.subTest(changes=changes):
                with self.assertRaises(p.ProtocolError): record(**changes).encode()

    def test_deadline_boundaries(self):
        self.assertEqual((5_000_000_000, 30_000_000_000), p.startup_deadlines(0))
        self.assertEqual(p.MAX_TIME, p.deadline(p.MAX_TIME - 4_000_000_000, 4_000_000_000))
        with self.assertRaises(p.ProtocolError): p.deadline(p.MAX_TIME, 1)
        p.check_privileged_deadline(30_000_000_000, 4_999_999_999)
        with self.assertRaises(p.ProtocolError): p.check_privileged_deadline(30_000_000_000, 5_000_000_000)

    def test_c1_gate_and_exec_are_distinct(self):
        g = record(role=p.Role.GUARDIAN, message=p.Message.A_EXEC, sender_pid=43, related_pid=44)
        a = record(role=p.Role.AGENT_BOOTSTRAP, message=p.Message.A_EXEC_ACK, sender_pid=44, related_pid=43)
        gate = p.LaunchGate()
        for r in (g, a):
            direction = p.Direction("gate", r.role, L, r.sender_pid, r.startup_deadline)
            direction.accept(r, 0, r.message, r.related_pid)
            gate.accept(r)
        self.assertTrue(gate.must_close_before_exec)
        startup = p.Startup()
        for m in p.Startup.ORDER[:6]: startup.accept(m)
        self.assertFalse(startup.running)
        with self.assertRaises(p.ProtocolError): startup.accept(p.Message.START_UI)
        with self.assertRaises(p.ProtocolError): gate.accept(a)
        reversed_gate = p.LaunchGate()
        with self.assertRaises(p.ProtocolError): reversed_gate.accept(a)

    def test_bootstrap_and_internal_roles_rejected_on_runtime(self):
        for channel in ("sr", "sg", "ra"):
            for role, message in ((p.Role.AGENT_BOOTSTRAP, p.Message.A_EXEC_ACK),
                                  (p.Role.ROOT, p.Message.META_ROUTER),
                                  (p.Role.SUPERVISOR, p.Message.GUARDIAN_OBSERVED),
                                  (p.Role.SUPERVISOR, p.Message.GUARDIAN_SIGNAL)):
                with self.subTest(channel=channel, message=message):
                    r = record(role=role, message=message, outcome=1 if message in p.SIGNAL else 0)
                    d = p.Direction(channel, role, L, 1, r.startup_deadline)
                    with self.assertRaises(p.ProtocolError): d.accept(r, 0, message, 42)

    def test_sequence_binding_state_and_expiry_are_sticky(self):
        r = record(role=p.Role.GUARDIAN, message=p.Message.G_BOUND, sender_pid=43, related_pid=44)
        for changes, now, expected in (({"sender_pid": 45}, 0, r.message), ({"related_pid": 45}, 0, r.message),
                                      ({"launch": bytes([1]) * 16}, 0, r.message), ({"sequence": 2}, 0, r.message),
                                      ({}, r.startup_deadline, r.message), ({}, 0, p.Message.AGENT_RESULT)):
            d = p.Direction("sg", r.role, L, 43, r.startup_deadline)
            with self.assertRaises(p.ProtocolError): d.accept(replace(r, **changes), now, expected, 44)
            with self.assertRaises(p.ProtocolError): d.accept(r, 0, r.message, 44)
        d = p.Direction("sg", r.role, L, 43, r.startup_deadline)
        d.accept(r, 0, r.message, 44)
        with self.assertRaises(p.ProtocolError): d.accept(r, 0, r.message, 44)

    def test_counts_and_shutdown_cannot_reset(self):
        d = p.Direction("sg", p.Role.GUARDIAN, L, 43, 30_000_000_000)
        d.accept(record(role=p.Role.GUARDIAN, message=p.Message.G_BOUND, sender_pid=43), 0, p.Message.G_BOUND, 42)
        with self.assertRaises(p.ProtocolError):
            d.accept(record(role=p.Role.GUARDIAN, message=p.Message.G_BOUND, sender_pid=43, sequence=2), 0, p.Message.G_BOUND, 42)
        d = p.Direction("sg", p.Role.GUARDIAN, L, 43, 30_000_000_000)
        d.bind_shutdown(30_000_000_000)
        with self.assertRaises(p.ProtocolError): d.bind_shutdown(30_000_000_001)

    def test_metadata_exact_order_and_eof(self):
        a = record(); b = record(message=p.Message.META_GUARDIAN, sequence=2, related_pid=43)
        bindings = {p.Message.META_ROUTER: (1, 42), p.Message.META_GUARDIAN: (1, 43)}
        data = a.encode() + b.encode()
        self.assertEqual((a, b), p.metadata(data, p.Role.SUPERVISOR, L, bindings, a.startup_deadline))
        for invalid in (data[:-1], data + b"x", b.encode() + a.encode()):
            with self.assertRaises(p.ProtocolError): p.metadata(invalid, p.Role.SUPERVISOR, L, bindings, a.startup_deadline)
        with self.assertRaises(p.ProtocolError): p.metadata(data, p.Role.SUPERVISOR, L, bindings, a.startup_deadline, a.startup_deadline)

    def test_all_metadata_roles_and_descriptor_lifetime(self):
        a = record(); b = record(role=p.Role.GUARDIAN_BOOTSTRAP, message=p.Message.META_AGENT,
                                sequence=2, sender_pid=43, related_pid=44)
        bindings = {p.Message.META_ROUTER: (1, 42), p.Message.META_AGENT: (43, 44)}
        for role in (p.Role.GUARDIAN, p.Role.AGENT):
            self.assertEqual((a, b), p.metadata(a.encode() + b.encode(), role, L, bindings, a.startup_deadline))
        self.assertEqual((a,), p.metadata(a.encode(), p.Role.ROUTER, L, bindings, a.startup_deadline))
        for role, handles in p.DESCRIPTORS.items():
            p.validate_descriptors(role, handles)
            p.validate_descriptors(role, {fd: kind for fd, kind in handles.items() if kind != "metadata"}, True)
            wrong = dict(handles); wrong[9] = "credential"
            with self.assertRaises(p.ProtocolError): p.validate_descriptors(role, wrong)
        p.validate_descriptors(p.Role.AGENT, {3: "ra"}, True)
        with self.assertRaises(p.ProtocolError): p.validate_descriptors(p.Role.AGENT, {3: "ra", 4: "gate"})

    def test_complete_catalog_structural_roundtrip(self):
        for kind in p.Message:
            outcome = 1 if kind in p.SIGNAL | p.WAIT or kind == p.Message.FAIL else 0
            r = record(message=kind, outcome=outcome)
            self.assertEqual(r, p.Record.decode(r.encode()))

    def test_incremental_frames_and_partial_eof(self):
        f = p.Frames(); self.assertEqual((), f.feed(record().encode()[:31]))
        self.assertEqual((record(),), f.feed(record().encode()[31:])); f.eof()
        with self.assertRaises(p.ProtocolError): f.feed(record().encode())
        f = p.Frames(); f.feed(record().encode()[:63])
        with self.assertRaises(p.ProtocolError): f.eof()
        with self.assertRaises(p.ProtocolError): p.Frames().feed(record().encode() + b"x")

    def test_full_startup_order(self):
        model = p.Startup()
        for m in p.Startup.ORDER: model.accept(m)
        self.assertTrue(model.running)

    def test_shutdown_reports_do_not_prove_required_waits(self):
        model = self.shutdown(); now = 30_000_000_001
        for m in (p.Message.STOP_ROUTER, p.Message.STOP_GUARDIAN, p.Message.STOP_UI, p.Message.UI_STOPPED,
                  "lifecycle_eof", p.Message.R_PROTOCOL_DONE, p.Message.HOST_DISPOSED):
            model.event(m, now)
        model.agent_report(self.report(), now, "sg")
        self.assertFalse(model.complete_model)
        self.assertNotIn(p.Message.AGENT_RESULT, model.events)
        model.wait_observation("agent", 44, True, 0, now)
        model.wait_observation("guardian", 43, True, 0, now)
        model.wait_observation("router", 42, True, 0, now)
        model.absence(True, now); self.assertTrue(model.complete_model)
        for subject, pid, normal, code in (("guardian", 44, True, 0), ("guardian", 43, False, 0), ("guardian", 43, True, 7)):
            other = self.shutdown(); other.event(p.Message.STOP_GUARDIAN, now)
            other.agent_report(self.report(), now, "sg"); other.wait_observation("agent", 44, True, 0, now)
            with self.assertRaises(p.ProtocolError): other.wait_observation(subject, pid, normal, code, now)
        with self.assertRaises(p.ProtocolError): self.shutdown().absence(True, now)

    @staticmethod
    def shutdown(**changes):
        values = dict(started_at=30_000_000_000, agent_pid=44, guardian_pid=43, router_pid=42,
                      launch=L, startup_deadline=30_000_000_000)
        values.update(changes)
        return p.Shutdown(**values)

    @staticmethod
    def report(**changes):
        values = dict(role=p.Role.GUARDIAN, message=p.Message.AGENT_RESULT, sequence=2, sender_pid=43,
                      related_pid=44, shutdown_deadline=34_000_000_000)
        values.update(changes)
        return record(**values)

    def test_generic_result_is_terminal_failure(self):
        model = self.shutdown(); now = 30_000_000_001
        model.event(p.Message.STOP_GUARDIAN, now)
        with self.assertRaises(p.ProtocolError): model.event(p.Message.AGENT_RESULT, now)
        self.assertTrue(model.failed); self.assertNotIn(p.Message.AGENT_RESULT, model.events)
        with self.assertRaises(p.ProtocolError): model.agent_report(self.report(), now, "sg")

    def test_constructor_pid_and_time_type_parity(self):
        for field in ("agent_pid", "guardian_pid", "router_pid"):
            for value in (0, -1, 1 << 31, None, True, False, 44.0, "44"):
                with self.subTest(field=field, value=value):
                    with self.assertRaises(p.ProtocolError): self.shutdown(**{field: value})
        for values in ({"agent_pid": 43}, {"guardian_pid": 42}, {"router_pid": 44},
                       {"launch": bytes(16)}, {"started_at": p.MAX_TIME}, {"startup_deadline": 0}):
            with self.assertRaises(p.ProtocolError): self.shutdown(**values)

    def test_complete_report_binding_and_mutation_stickiness(self):
        now = 30_000_000_001
        variants = ({"sender_pid": 42}, {"related_pid": 43}, {"launch": bytes([1]) * 16},
                    {"startup_deadline": 30_000_000_001}, {"shutdown_deadline": 34_000_000_001},
                    {"sequence": 1}, {"sequence": 0}, {"outcome": 7},
                    {"role": p.Role.AGENT}, {"message": p.Message.AGENT_SIGNAL, "outcome": 15})
        for changes in variants:
            model = self.shutdown(); model.event(p.Message.STOP_GUARDIAN, now)
            with self.assertRaises(p.ProtocolError): model.agent_report(self.report(**changes), now, "sg")
            self.assertTrue(model.failed)
            with self.assertRaises(p.ProtocolError): model.agent_report(self.report(), now, "sg")
        for channel in ("sr", "ra", "gate"):
            model = self.shutdown(); model.event(p.Message.STOP_GUARDIAN, now)
            with self.assertRaises(p.ProtocolError): model.agent_report(self.report(), now, channel)
            self.assertTrue(model.failed)
        model = self.shutdown(); model.event(p.Message.STOP_GUARDIAN, now)
        with self.assertRaises(p.ProtocolError): model.agent_report(None, now, "sg")
        self.assertTrue(model.failed)
        model = self.shutdown()
        with self.assertRaises(p.ProtocolError): model.agent_report(self.report(), now, "sg")
        self.assertTrue(model.failed)
        model = self.shutdown(); model.event(p.Message.STOP_GUARDIAN, now)
        model.agent_report(self.report(), now, "sg")
        with self.assertRaises(p.ProtocolError): model.agent_report(self.report(), now, "sg")
        self.assertTrue(model.failed)

    def test_report_alone_cannot_advance_positive_agent_wait(self):
        now = 30_000_000_001; model = self.shutdown()
        model.event(p.Message.STOP_GUARDIAN, now); model.agent_report(self.report(), now, "sg")
        with self.assertRaises(p.ProtocolError): model.wait_observation("guardian", 43, True, 0, now)
        self.assertTrue(model.failed)

    def test_observation_numeric_types_and_required_echild_fail(self):
        now = 30_000_000_001
        for value in (0, -1, 1 << 31, None, True, 44.0, "44"):
            model = self.shutdown(); model.event(p.Message.STOP_GUARDIAN, now); model.agent_report(self.report(), now, "sg")
            with self.assertRaises(p.ProtocolError): model.wait_observation("agent", value, True, 0, now)
            self.assertTrue(model.failed)
            with self.assertRaises(p.ProtocolError): model.wait_observation("agent", 44, True, 0, now)
        for value in (False, None, 0.0, "0", -1, 256):
            model = self.shutdown(); model.event(p.Message.STOP_GUARDIAN, now); model.agent_report(self.report(), now, "sg")
            with self.assertRaises(p.ProtocolError): model.wait_observation("agent", 44, True, value, now)
            self.assertTrue(model.failed)
        for value in (True, 30_000_000_001.0, "30000000001", None, -1, p.MAX_TIME + 1, 29_999_999_999, 34_000_000_000):
            model = self.shutdown()
            with self.assertRaises(p.ProtocolError): model.event(p.Message.STOP_ROUTER, value)
            self.assertTrue(model.failed)

    def test_overflow_is_sticky_for_existing_direction(self):
        d = p.Direction("sg", p.Role.GUARDIAN, L, 43, 30_000_000_000)
        with self.assertRaises(p.ProtocolError): d.bind_shutdown(p.MAX_TIME)
        self.assertTrue(d.failed)
        with self.assertRaises(p.ProtocolError): d.bind_shutdown(30_000_000_000)

    def test_terminal_lifecycle_eof_once_and_other_channels_fail(self):
        def ready():
            d = p.Direction("ra", p.Role.AGENT, L, 44, 30_000_000_000)
            for seq, kind in enumerate((p.Message.A_READY, p.Message.UI_READY), 1):
                d.accept(record(role=p.Role.AGENT, message=kind, sequence=seq, sender_pid=44), 0, kind, 42)
            d.bind_shutdown(30_000_000_000)
            d.accept(record(role=p.Role.AGENT, message=p.Message.UI_STOPPED, sequence=3, sender_pid=44,
                            shutdown_deadline=34_000_000_000), 30_000_000_001, p.Message.UI_STOPPED, 42)
            return d
        d = ready(); d.eof(True, 30_000_000_001)
        with self.assertRaises(p.ProtocolError): d.eof(True, 30_000_000_001)
        self.assertTrue(d.failed)
        for channel, role in (("sr", p.Role.ROUTER), ("sg", p.Role.GUARDIAN), ("gate", p.Role.AGENT_BOOTSTRAP), ("ra", p.Role.AGENT)):
            d = p.Direction(channel, role, L, 44, 30_000_000_000)
            with self.assertRaises(p.ProtocolError): d.eof(True, 30_000_000_001)
            self.assertTrue(d.failed)
        for value in (None, True, 0, 34_000_000_000):
            d = ready()
            with self.assertRaises(p.ProtocolError): d.eof(True, value)
            self.assertTrue(d.failed)

    def test_malformed_gate_and_direction_mutations_are_sticky(self):
        g = record(role=p.Role.GUARDIAN, message=p.Message.A_EXEC, sender_pid=43, related_pid=44)
        gate = p.LaunchGate()
        with self.assertRaises(p.ProtocolError): gate.accept(replace(g, outcome=7))
        self.assertTrue(gate.failed)
        with self.assertRaises(p.ProtocolError): gate.accept(g)
        d = p.Direction("gate", p.Role.GUARDIAN, L, 43, 30_000_000_000)
        with self.assertRaises(p.ProtocolError): d.accept(None, 0, p.Message.A_EXEC, 44)
        self.assertTrue(d.failed)
        with self.assertRaises(p.ProtocolError): d.accept(g, 0, p.Message.A_EXEC, 44)


if __name__ == "__main__":
    unittest.main()
